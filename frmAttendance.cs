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
                        // Set ComboBox's DataSource to the sections list
                        comboBoxSectionss.DataSource = null; // Reset ComboBox before setting the data
                        comboBoxSectionss.DataSource = sections;
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
            // Confirm if the user is sure about saving the attendance
            DialogResult result = MessageBox.Show("Are you sure you want to save the attendance?", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                return; // Exit if the user cancels the save
            }

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

            // Check if attendance has already been recorded for the selected date and section
            if (CheckIfAttendanceExists(selectedDate, sectionId, aycode))
            {
                MessageBox.Show("Attendance has already been recorded for this date.");

                foreach (DataGridViewRow row in dataGridViewAttendanceList.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell is DataGridViewCheckBoxCell)
                        {
                            cell.Value = false; // Uncheck all checkboxes
                        }
                    }
                }

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

                    // Clear all checkboxes after saving attendance
                    foreach (DataGridViewRow row in dataGridViewAttendanceList.Rows)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            if (cell is DataGridViewCheckBoxCell)
                            {
                                cell.Value = false; // Uncheck all checkboxes
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving attendance: " + ex.Message);
                }
            }
        }

        private bool CheckIfAttendanceExists(DateTime attendanceDate, int sectionId, string aycode)
        {
            using (SQLiteConnection connection = dbConnection.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open(); // Ensure the connection is open
                    }

                    // Query to check if attendance already exists for the given date, section, and academic year
                    string query = @"
                SELECT COUNT(*) 
                FROM tblattendance
                WHERE attendance_date = @attendanceDate
                AND sectionid = @sectionId
                AND aycode = @aycode";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@attendanceDate", attendanceDate.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@sectionId", sectionId);
                        cmd.Parameters.AddWithValue("@aycode", aycode);

                        int attendanceCount = Convert.ToInt32(cmd.ExecuteScalar());
                        return attendanceCount > 0; // Returns true if attendance already exists
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error checking attendance: " + ex.Message);
                    return false;
                }
            }
        }




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

        private int GetSectionId(string sectionName)
        {
            using (SQLiteConnection connection = dbConnection.GetConnection)
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    string query = "SELECT id FROM tblsection WHERE section = @section";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@section", sectionName);
                        object result = cmd.ExecuteScalar();
                        return result != null ? Convert.ToInt32(result) : -1;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error getting section ID: " + ex.Message);
                    return -1;
                }
            }
        }

        private void ChooseSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedSection = ChooseSection.SelectedItem.ToString();
            LoadStudentsForSection(selectedSection);
        }

        private void LoadStudentsForSection(string selectedSection)
        {
            string currentAcademicYear = GetActiveAcademicYear();
            if (string.IsNullOrEmpty(currentAcademicYear))
            {
                MessageBox.Show("No open academic year found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SQLiteConnection connection = dbConnection.GetConnection)
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open(); // Ensure the connection is open
                    }

                    string query = @"
                SELECT e.lrn, s.lname || ', ' || s.fname || ' ' || s.mname AS student
                FROM vwenrollment e
                JOIN tblstudent s ON s.lrn = e.lrn
                WHERE e.section = @section
                AND e.status = 'Enrolled'
                AND e.aycode = @academicYear  -- Filter by current academic year
                ORDER BY s.lname, s.fname, s.mname;";

                    using (SQLiteCommand cm = new SQLiteCommand(query, connection))
                    {
                        cm.Parameters.AddWithValue("@section", selectedSection);
                        cm.Parameters.AddWithValue("@academicYear", currentAcademicYear);  // Bind academic year
                        SQLiteDataReader dr = cm.ExecuteReader();

                        dataGridViewAttendanceList.Rows.Clear(); // Clear any previous data

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
        private void chooseDate_Click(object sender, EventArgs e)
        {
            // Hide chooseDate and show pictureBoxSelect
            chooseDate.Visible = false;
            pictureBoxSelect.Visible = true;

            // Set the DateTimePicker to the current date
            CurrentDate.Value = DateTime.Now;

            // Enable checkboxes for the current day
            EnableTodayCheckbox();
        }

        private void pictureBoxSelect_Click_1(object sender, EventArgs e)
        {
            // Hide pictureBoxSelect and show chooseDate
            pictureBoxSelect.Visible = false;
            chooseDate.Visible = true;
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


        //attendance summary


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


        private void comboBoxSectionss_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedSection = comboBoxSectionss.SelectedItem.ToString();
            LoadAttendanceSummary(selectedSection);  // Load attendance summary when section is changed
        }

        private void LoadAttendanceSummary(string selectedSection)
        {
            string currentAcademicYear = GetActiveAcademicYear();
            if (string.IsNullOrEmpty(currentAcademicYear))
            {
                MessageBox.Show("No open academic year found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SQLiteConnection connection = dbConnection.GetConnection)
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open(); // Ensure the connection is open
                    }

                    // Query to get attendance summary for the selected section
                    string query = @"
        SELECT s.lrn, s.lname || ', ' || s.fname || ' ' || s.mname AS student,
               COUNT(CASE WHEN a.status = 'Present' THEN 1 END) AS PresentCount,
               COUNT(CASE WHEN a.status = 'Absent' THEN 1 END) AS AbsentCount,
               strftime('%m', a.attendance_date) AS month_num
        FROM tblattendance a
        JOIN tblstudent s ON a.lrn = s.lrn
        JOIN tblsection sec ON a.sectionid = sec.id
        WHERE sec.section = @section
        AND a.aycode = @aycode
        GROUP BY s.lrn, month_num
        ORDER BY s.lname, s.fname, s.mname";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@section", selectedSection);
                        cmd.Parameters.AddWithValue("@aycode", currentAcademicYear);

                        SQLiteDataReader reader = cmd.ExecuteReader();
                        dataGridViewAttendanceSummary.Rows.Clear();  // Clear existing data

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                // Convert numeric month to name
                                string monthNum = reader["month_num"].ToString();
                                string monthName = GetMonthName(monthNum); // Helper method to get month name

                                // Add rows to DataGridView with Present and Absent counts
                                dataGridViewAttendanceSummary.Rows.Add(
                                    reader["lrn"].ToString(),
                                    reader["student"].ToString(),
                                    reader["PresentCount"].ToString(),
                                    reader["AbsentCount"].ToString(),
                                    monthName); // Display month as name
                            }
                        }
                        else
                        {
                            MessageBox.Show("No attendance records found for this section.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading attendance summary: " + ex.Message);
            }
        }

        // Helper method to convert month number to month name
        private string GetMonthName(string monthNum)
        {
            switch (monthNum)
            {
                case "01": return "January";
                case "02": return "February";
                case "03": return "March";
                case "04": return "April";
                case "05": return "May";
                case "06": return "June";
                case "07": return "July";
                case "08": return "August";
                case "09": return "September";
                case "10": return "October";
                case "11": return "November";
                case "12": return "December";
                default: return "Unknown";
            }
        }

       
    }
}
