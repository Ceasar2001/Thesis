using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace TeacherPortal
{
    public partial class frmGrading : Form
    {
        private DBConnection dbConnection;

        public frmGrading()
        {
            InitializeComponent();
            dbConnection = new DBConnection();
            LoadSections();
            LoadSubjects();
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnshowstudent_Click(object sender, EventArgs e)
        {
            string selectedSection = comboSection.SelectedItem?.ToString();
            //string selectedSection = comboBoxSection.SelectedItem?.ToString();
            string selectedSubject = comboSubject.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedSection) || string.IsNullOrEmpty(selectedSubject))
            {
                MessageBox.Show("Please select both section and subject.", DBConnection._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LoadStudentsWithGrades(selectedSection, selectedSubject);
        }

        public void LoadStudentsWithGrades(string section, string subject)
        {
            try
            {
                SQLiteDataReader dr;
                dataGridStudentWithGrades.Rows.Clear(); // Clear previous rows

                using (SQLiteConnection cn = dbConnection.GetConnection)
                {
                    string currentAcademicYear = string.Empty;
                    string ayQuery = "SELECT aycode FROM tblacadyear WHERE status = 'Open' LIMIT 1";

                    using (SQLiteCommand ayCmd = new SQLiteCommand(ayQuery, cn))
                    {
                        cn.Open();
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
                        MessageBox.Show("No open academic year found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string query = @"
                SELECT DISTINCT
                    e.lrn, 
                    s.fname || ' ' || s.lname || ' ' || s.mname AS student_name,
                    COALESCE(g.subject, @subject) AS subject,
                    COALESCE(g.quarter, '') AS Quarter,
                    COALESCE(g.writtenScore, 0) AS WrittenWork, 
                    COALESCE(g.performanceScore, 0) AS PerformanceTask, 
                    COALESCE(g.quarterlyScore, 0) AS QuarterlyAsses, 
                    COALESCE(g.totalGrade, 0) AS QuarterlyGrade
                    
                    FROM tblenrollment e
                    JOIN tblstudent s ON e.lrn = s.lrn
                    JOIN tblsection sec ON e.sectionid = sec.id
                    JOIN tblEnrollmentSubjects es ON es.lrn = e.lrn
                    LEFT JOIN tblGrade g ON g.lrn = e.lrn AND g.subject = @subject
                    WHERE sec.section = @section
                    AND e.aycode = @academicYear;";

                    using (SQLiteCommand cm = new SQLiteCommand(query, cn))
                    {
                        cm.Parameters.AddWithValue("@section", section);
                        cm.Parameters.AddWithValue("@subject", subject);
                        cm.Parameters.AddWithValue("@academicYear", currentAcademicYear);

                        dr = cm.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                // Check if data is not null
                                object lrn = dr["lrn"] ?? DBNull.Value;
                                object studentName = dr["student_name"] ?? DBNull.Value;
                                object subjectName = dr["subject"] ?? DBNull.Value;
                                object quarter = dr["Quarter"] ?? DBNull.Value;
                                object writtenWork = dr["WrittenWork"] ?? 0;
                                object performanceTask = dr["PerformanceTask"] ?? 0;
                                object quarterlyAsses = dr["QuarterlyAsses"] ?? 0;
                                object quarterlyGrade = dr["QuarterlyGrade"] ?? 0;

                                dataGridStudentWithGrades.Rows.Add(
                                    lrn,
                                    studentName,
                                    section,
                                    subjectName,
                                    quarter,
                                    writtenWork,
                                    performanceTask,
                                    quarterlyAsses,
                                    quarterlyGrade
                                );
                            }
                        }
                        else
                        {
                            MessageBox.Show("No students found for this section in the current academic year.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        dr.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading student grades: {ex.Message}", DBConnection._title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dataGridStudentWithGrades.ClearSelection(); // Optional: Clear selection after loading
            }
        }






        private void LoadSections()
        {
            try
            {
                using (SQLiteConnection cn = dbConnection.GetConnection)
                {
                    using (SQLiteCommand cm = new SQLiteCommand("SELECT section FROM tblsection", cn))
                    {
                        cn.Open();
                        using (SQLiteDataReader dr = cm.ExecuteReader())
                        {
                            comboSection.Items.Clear();
                            while (dr.Read())
                            {
                                comboSection.Items.Add(dr["section"].ToString());
                                comboBoxSection.Items.Add(dr["section"].ToString());
                            }
                        }
                        cn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading sections: {ex.Message}", DBConnection._title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSubjects()
        {
            try
            {
                using (SQLiteConnection cn = dbConnection.GetConnection)
                {
                    using (SQLiteCommand cm = new SQLiteCommand("SELECT DISTINCT subject FROM tblEnrollmentSubjects", cn))
                    {
                        cn.Open();
                        using (SQLiteDataReader dr = cm.ExecuteReader())
                        {
                            comboSubject.Items.Clear();
                            while (dr.Read())
                            {
                                comboSubject.Items.Add(dr["subject"].ToString());
                            }
                        }
                        cn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading subjects: {ex.Message}", DBConnection._title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridStudentWithGrades_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if a valid row (other than headers) was clicked
            if (e.RowIndex >= 0)
            {
                // Retrieve values from the clicked row
                string lrn = dataGridStudentWithGrades.Rows[e.RowIndex].Cells["Lrn"].Value.ToString(); // LRN (Column1)
                string studentName = dataGridStudentWithGrades.Rows[e.RowIndex].Cells["studentname"].Value.ToString(); // Student Name (Column2)
                string section = comboSection.SelectedItem.ToString(); // Get the selected section from comboSection
                string subject = comboSubject.SelectedItem.ToString(); // Get the selected subject from comboSubject

                // Create an instance of frmEnterGrades and pass the details
                frmEnterGrades enterGradesForm = new frmEnterGrades(lrn, studentName, section, subject);
                enterGradesForm.ShowDialog();  // Show the form as a modal dialog
            }
        }

        private void comboSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected section
            string selectedSection = comboSection.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(selectedSection))
            {
                LoadSubjectsForSection(selectedSection);
            }
        }


        private void LoadSubjectsForSection(string section)
        {
            try
            {
                using (SQLiteConnection cn = dbConnection.GetConnection)
                {
                    // Query subjects based on section from tblEnrollmentSubjects
                    string query = @"
                SELECT DISTINCT es.subject 
                FROM tblEnrollmentSubjects es
                JOIN tblenrollment e ON es.lrn = e.lrn
                JOIN tblsection sec ON e.sectionid = sec.id
                WHERE sec.section = @section";

                    using (SQLiteCommand cm = new SQLiteCommand(query, cn))
                    {
                        cm.Parameters.AddWithValue("@section", section);

                        cn.Open();
                        using (SQLiteDataReader dr = cm.ExecuteReader())
                        {
                            comboSubject.Items.Clear();
                            while (dr.Read())
                            {
                                comboSubject.Items.Add(dr["subject"].ToString());
                            }
                        }
                        cn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading subjects: {ex.Message}", DBConnection._title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //Summary

        private void comboBoxSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedSection = comboBoxSection.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedSection))
            {
                // Check if the academic year is open
                using (SQLiteConnection cn = dbConnection.GetConnection)
                {
                    string query = "SELECT status FROM tblAcadYear WHERE status = 'Open' LIMIT 1";
                    using (SQLiteCommand cm = new SQLiteCommand(query, cn))
                    {
                        cn.Open();
                        object result = cm.ExecuteScalar();
                        cn.Close();

                        if (result != null)
                        {
                            UpdateDataGridViewHeaders(selectedSection);
                            LoadStudentGrades(selectedSection);
                        }
                        else
                        {
                            MessageBox.Show("The academic year is closed. Please try again later.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void LoadStudentGrades(string section)
        {
            try
            {
                dataGridViewGradeSummary.Rows.Clear();

                // Get the current academic year
                string currentAyCode = GetCurrentAcademicYear();

                // Check if any students are enrolled in the current academic year
                if (string.IsNullOrEmpty(currentAyCode) || !IsAnyStudentEnrolledInCurrentAY(currentAyCode, section))
                {
                    MessageBox.Show("No students found for the current academic year.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return; // Exit early as no students are available
                }

                using (SQLiteConnection cn = dbConnection.GetConnection)
                {
                    string query = @"
            SELECT g.lrn, 
                   s.lname || ', ' || s.fname || ' ' || s.mname AS student_name,
                   g.subject,
                   g.totalGrade
            FROM tblGrade g
            JOIN tblStudent s ON g.lrn = s.lrn
            WHERE g.section = @section
            ORDER BY g.lrn, g.subject";

                    using (SQLiteCommand cm = new SQLiteCommand(query, cn))
                    {
                        cm.Parameters.AddWithValue("@section", section);
                        cn.Open();

                        using (SQLiteDataReader dr = cm.ExecuteReader())
                        {
                            Dictionary<string, Dictionary<string, double>> studentGrades = new Dictionary<string, Dictionary<string, double>>();
                            Dictionary<string, string> studentNames = new Dictionary<string, string>();

                            // Process student grades dynamically
                            while (dr.Read())
                            {
                                string lrn = dr["lrn"].ToString();
                                string studentName = dr["student_name"].ToString();
                                string subject = dr["subject"].ToString();
                                double grade = Convert.ToDouble(dr["totalGrade"]);

                                if (!studentGrades.ContainsKey(lrn))
                                {
                                    studentGrades[lrn] = new Dictionary<string, double>();
                                    studentNames[lrn] = studentName;
                                }

                                studentGrades[lrn][subject] = grade;
                            }

                            cn.Close();

                            // Populate DataGridView
                            foreach (var student in studentGrades)
                            {
                                string lrn = student.Key;
                                string studentName = studentNames[lrn];

                                DataGridViewRow row = new DataGridViewRow();
                                row.CreateCells(dataGridViewGradeSummary);

                                row.Cells[0].Value = lrn;
                                row.Cells[1].Value = studentName;

                                double totalGrade = 0;
                                int subjectCount = 0;

                                for (int i = 2; i < dataGridViewGradeSummary.Columns.Count - 1; i++)
                                {
                                    string subjectName = dataGridViewGradeSummary.Columns[i].HeaderText;
                                    if (student.Value.ContainsKey(subjectName))
                                    {
                                        double grade = student.Value[subjectName];
                                        row.Cells[i].Value = grade;
                                        totalGrade += grade;
                                        subjectCount++;
                                    }
                                }

                                // Calculate the average
                                double average = (subjectCount > 0) ? (totalGrade / subjectCount) : 0;
                                row.Cells[dataGridViewGradeSummary.Columns.Count - 1].Value = Math.Round(average, 2);

                                dataGridViewGradeSummary.Rows.Add(row);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading student grades: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetCurrentAcademicYear()
        {
            string currentAyCode = string.Empty;

            try
            {
                using (SQLiteConnection cn = dbConnection.GetConnection)
                {
                    string query = "SELECT aycode FROM tblacadyear WHERE status = 'Open' LIMIT 1";
                    using (SQLiteCommand cm = new SQLiteCommand(query, cn))
                    {
                        cn.Open();
                        currentAyCode = cm.ExecuteScalar()?.ToString();
                        cn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching current academic year: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return currentAyCode;
        }

        private bool IsAnyStudentEnrolledInCurrentAY(string ayCode, string section)
        {
            bool isStudentEnrolled = false;

            try
            {
                using (SQLiteConnection cn = dbConnection.GetConnection)
                {
                    string query = @"
            SELECT COUNT(*) 
            FROM tblenrollment e
            JOIN tblsection s ON e.sectionid = s.id
            WHERE e.aycode = @ayCode AND s.section = @section AND e.status = 'Enrolled'";

                    using (SQLiteCommand cm = new SQLiteCommand(query, cn))
                    {
                        cm.Parameters.AddWithValue("@ayCode", ayCode);
                        cm.Parameters.AddWithValue("@section", section);
                        cn.Open();
                        isStudentEnrolled = Convert.ToInt32(cm.ExecuteScalar()) > 0;
                        cn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking student enrollment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return isStudentEnrolled;
        }





        private List<string> GetSubjectsForSection(string section)
        {
            List<string> subjects = new List<string>();

            try
            {
                using (SQLiteConnection cn = dbConnection.GetConnection)
                {
                    string query = @"
                SELECT DISTINCT subject 
                FROM tblGrade 
                WHERE section = @section";

                    using (SQLiteCommand cm = new SQLiteCommand(query, cn))
                    {
                        cm.Parameters.AddWithValue("@section", section);
                        cn.Open();

                        using (SQLiteDataReader dr = cm.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                subjects.Add(dr["subject"].ToString());
                            }
                        }

                        cn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading subjects: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return subjects;
        }

        private void UpdateDataGridViewHeaders(string section)
        {
            
            List<string> subjects = GetSubjectsForSection(section);

            dataGridViewGradeSummary.Columns.Clear();

            dataGridViewGradeSummary.Columns.Add("lrn", "Lrn");
            dataGridViewGradeSummary.Columns.Add("studentname", "Student Name");

            foreach (string subject in subjects)
            {
                dataGridViewGradeSummary.Columns.Add(subject, subject);
            }

            dataGridViewGradeSummary.Columns.Add("average", "Average");
        }




    }
}
