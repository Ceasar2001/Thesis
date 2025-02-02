using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace TeacherPortal
{
    public partial class frmAttendance : Form
    {
        private DBConnection dbConnection;

        public frmAttendance()
        {
            dbConnection = new DBConnection();
            InitializeComponent();
            LoadSections();
        }

        private void frmAttendance_Load(object sender, EventArgs e)
        {
            // Load all sections into the ComboBox
            LoadSections();
        }

        private void LoadSections()
        {
            try
            {
                using (SQLiteConnection connection = dbConnection.GetConnection)
                {
                    // Check if vwsection view exists
                    string checkViewQuery = "SELECT name FROM sqlite_master WHERE type='view' AND name='vwsection';";
                    using (SQLiteCommand checkViewCmd = new SQLiteCommand(checkViewQuery, connection))
                    {
                        if (connection.State != ConnectionState.Open)
                        {
                            connection.Open(); // Ensure the connection is open before executing the command
                        }

                        object result = checkViewCmd.ExecuteScalar();
                        if (result == null)
                        {
                            MessageBox.Show("The view 'vwsection' does not exist.");
                            return;
                        }
                    }

                    // Now proceed with the main query to fetch sections
                    string query = "SELECT section FROM vwsection";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        SQLiteDataReader reader = command.ExecuteReader();
                        List<string> sections = new List<string>();

                        while (reader.Read())
                        {
                            sections.Add(reader["section"].ToString());
                        }

                        // Check if sections were fetched
                        if (sections.Count == 0)
                        {
                            MessageBox.Show("No sections found in the database.");
                            return;
                        }

                        // Populate ComboBox with sections
                        ChooseSection.DataSource = sections;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading sections: " + ex.Message);
            }
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void saveAttendance_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = CurrentDate.Value.Date; // Get the selected date
            string selectedSection = ChooseSection.SelectedItem.ToString(); // Get selected section
            int sectionId = GetSectionId(selectedSection); // Get section ID based on section name
            string aycode = GetActiveAcademicYear(); // Fetch active academic year

            if (sectionId == -1)
            {
                MessageBox.Show("Invalid section selected.");
                return;
            }

            if (string.IsNullOrEmpty(aycode))
            {
                MessageBox.Show("No active academic year found.");
                return;
            }

            using (SQLiteConnection connection = dbConnection.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open(); // Ensure the connection is open
                    }

                    using (SQLiteTransaction transaction = connection.BeginTransaction())
                    {
                        foreach (DataGridViewRow row in dataGridViewAttendanceList.Rows)
                        {
                            string lrn = row.Cells["colLrn"].Value.ToString(); // Get LRN
                            string status = string.Empty;  // Initialize status as empty

                            // Check the attendance status for the selected day
                            status = GetStatusForDay(row, selectedDate); // Get status based on the selected date
                            SaveAttendance(connection, lrn, sectionId, selectedDate, status, aycode); // Save the status with academic year
                        }

                        // Commit the transaction after all insertions
                        transaction.Commit();
                    }

                    MessageBox.Show("Attendance saved successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving attendance: " + ex.Message);
                }
            }
        }


        //private void saveAttendance_Click(object sender, EventArgs e)
        //{
        //    DateTime selectedDate = CurrentDate.Value.Date; // Get the selected date
        //    string selectedSection = ChooseSection.SelectedItem.ToString(); // Get selected section
        //    int sectionId = GetSectionId(selectedSection); // Get section ID based on section name

        //    if (sectionId == -1)
        //    {
        //        MessageBox.Show("Invalid section selected.");
        //        return;
        //    }

        //    using (SQLiteConnection connection = dbConnection.GetConnection)
        //    {
        //        try
        //        {
        //            if (connection.State != ConnectionState.Open)
        //            {
        //                connection.Open(); // Ensure the connection is open
        //            }

        //            using (SQLiteTransaction transaction = connection.BeginTransaction())
        //            {
        //                foreach (DataGridViewRow row in dataGridViewAttendanceList.Rows)
        //                {
        //                    string lrn = row.Cells["colLrn"].Value.ToString(); // Get LRN
        //                    string status = string.Empty;  // Initialize status as empty

        //                    // Check the attendance status for the selected day
        //                    status = GetStatusForDay(row, selectedDate); // Get status based on the selected date
        //                    SaveAttendance(connection, lrn, sectionId, selectedDate, status); // Save the status for the selected date
        //                }

        //                // Commit the transaction after all insertions
        //                transaction.Commit();
        //            }

        //            MessageBox.Show("Attendance saved successfully.");
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Error saving attendance: " + ex.Message);
        //        }
        //    }
        //}


        private string GetStatusForDay(DataGridViewRow row, DateTime selectedDate)
        {
            string status = "Absent"; // Default status is absent

            // Get the column name corresponding to the selected date (e.g., Monday, Tuesday, etc.)
            string dayColumn = selectedDate.DayOfWeek.ToString();

            // Check if the checkbox for the selected day is checked
            if (row.Cells[dayColumn].Value != null && row.Cells[dayColumn].Value is bool isPresent)
            {
                status = isPresent ? "Present" : "Absent"; // Update status based on checkbox
            }

            return status;
        }

        private void SaveAttendance(SQLiteConnection connection, string lrn, int sectionId, DateTime attendanceDate, string status, string aycode)
        {
            string insertQuery = @"
    INSERT INTO tblattendance (lrn, sectionid, attendance_date, status, aycode)
    VALUES (@lrn, @sectionid, @attendance_date, @status, @aycode);";

            using (SQLiteCommand cmd = new SQLiteCommand(insertQuery, connection))
            {
                cmd.Parameters.AddWithValue("@lrn", lrn);
                cmd.Parameters.AddWithValue("@sectionid", sectionId);
                cmd.Parameters.AddWithValue("@attendance_date", attendanceDate.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@aycode", aycode); // Add aycode parameter

                cmd.ExecuteNonQuery(); // Execute the command for each attendance status
            }
        }


        //private void SaveAttendance(SQLiteConnection connection, string lrn, int sectionId, DateTime attendanceDate, string status)
        //{
        //    string insertQuery = @"
        //INSERT INTO tblattendance (lrn, sectionid, attendance_date, status)
        //VALUES (@lrn, @sectionid, @attendance_date, @status);";

        //    using (SQLiteCommand cmd = new SQLiteCommand(insertQuery, connection))
        //    {
        //        cmd.Parameters.AddWithValue("@lrn", lrn);
        //        cmd.Parameters.AddWithValue("@sectionid", sectionId);
        //        cmd.Parameters.AddWithValue("@attendance_date", attendanceDate.ToString("yyyy-MM-dd"));
        //        cmd.Parameters.AddWithValue("@status", status);

        //        cmd.ExecuteNonQuery(); // Execute the command for each attendance status
        //    }
        //}



        private int GetSectionId(string sectionName)
        {
            using (SQLiteConnection connection = dbConnection.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open(); // Ensure the connection is open
                    }

                    string query = "SELECT id FROM tblsection WHERE section = @section";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@section", sectionName);
                        object result = cmd.ExecuteScalar();
                        return result != null ? Convert.ToInt32(result) : -1; // Return -1 if section not found
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error getting section ID: " + ex.Message);
                    return -1;
                }
            }
        }


        private void ShowStudent_Click(object sender, EventArgs e)
        {
            string selectedSection = ChooseSection.SelectedItem.ToString();
            string currentAcademicYear = string.Empty;

            try
            {
                SQLiteDataReader dr;
                dataGridViewAttendanceList.Rows.Clear(); // Clear any previous data

                using (SQLiteConnection cn = dbConnection.GetConnection)
                {
                    // Step 1: Fetch the open academic year from tblacadyear
                    if (cn.State != ConnectionState.Open)
                    {
                        cn.Open();
                    }

                    string ayQuery = "SELECT aycode FROM tblacadyear WHERE status = 'Open' LIMIT 1";
                    using (SQLiteCommand ayCmd = new SQLiteCommand(ayQuery, cn))
                    {
                        var result = ayCmd.ExecuteScalar();
                        if (result != null)
                        {
                            currentAcademicYear = result.ToString();
                        }
                        else
                        {
                            MessageBox.Show("No open academic year found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    if (string.IsNullOrEmpty(currentAcademicYear))
                    {
                        return; // Exit if no open academic year was found
                    }

                    // Step 2: Fetch students for the selected section and current academic year
                    string query = @"
                SELECT e.lrn, s.lname || ', ' || s.fname || ' ' || s.mname AS student
                FROM vwenrollment e
                JOIN tblstudent s ON s.lrn = e.lrn
                WHERE e.section = @section
                AND e.status = 'Enrolled'
                AND e.aycode = @academicYear  -- Filter by current academic year
                ORDER BY s.lname, s.fname, s.mname;";

                    using (SQLiteCommand cm = new SQLiteCommand(query, cn))
                    {
                        cm.Parameters.AddWithValue("@section", selectedSection);
                        cm.Parameters.AddWithValue("@academicYear", currentAcademicYear);  // Bind academic year
                        dr = cm.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                // Add data for LRN and Student Name columns
                                dataGridViewAttendanceList.Rows.Add(dr["lrn"].ToString(), dr["student"].ToString());
                            }
                        }
                        else
                        {
                            MessageBox.Show("No students found in this section for the current academic year.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        dr.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading students: " + ex.Message);
            }
        }



        private void EnableTodayCheckbox()
        {
            DateTime selectedDate = CurrentDate.Value;

            DayOfWeek selectedDay = selectedDate.DayOfWeek;

            foreach (DataGridViewRow row in dataGridViewAttendanceList.Rows)
            {
                row.Cells["Monday"].ReadOnly = true;
                row.Cells["Tuesday"].ReadOnly = true;
                row.Cells["Wednesday"].ReadOnly = true;
                row.Cells["Thursday"].ReadOnly = true;
                row.Cells["Friday"].ReadOnly = true;
            }

            switch (selectedDay)
            {
                case DayOfWeek.Monday:
                    EnableCheckboxForDay("Monday");
                    break;
                case DayOfWeek.Tuesday:
                    EnableCheckboxForDay("Tuesday");
                    break;
                case DayOfWeek.Wednesday:
                    EnableCheckboxForDay("Wednesday");
                    break;
                case DayOfWeek.Thursday:
                    EnableCheckboxForDay("Thursday");
                    break;
                case DayOfWeek.Friday:
                    EnableCheckboxForDay("Friday");
                    break;
            }
        }

        private void EnableCheckboxForDay(string day)
        {
            foreach (DataGridViewRow row in dataGridViewAttendanceList.Rows)
            {
                row.Cells[day].ReadOnly = false;
            }
        }

        private void chooseDate_Click_1(object sender, EventArgs e)
        {
            CurrentDate.Value = DateTime.Now;

            EnableTodayCheckbox();
        }

        private void dataGridViewAttendanceList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private string GetActiveAcademicYear()
        {
            string aycode = string.Empty;
            using (SQLiteConnection connection = dbConnection.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open(); // Ensure the connection is open
                    }

                    string query = "SELECT aycode FROM tblacadyear WHERE status = 'Open' LIMIT 1";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            aycode = result.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching academic year: " + ex.Message);
                }
            }
            return aycode;
        }

    }
}
