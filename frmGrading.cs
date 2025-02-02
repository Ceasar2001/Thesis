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

    }
}
