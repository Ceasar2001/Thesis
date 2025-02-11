using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace TeacherPortal
{
    public partial class frmReport : Form
    {
        private DBConnection dbConnection;
        private string studentLrn;
        public frmReport()
        {
            InitializeComponent();
            dbConnection = new DBConnection();
            LoadSections();
        }
        private void close_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void LoadSections()
        {
            try
            {
                using (SQLiteConnection conn = dbConnection.GetConnection)
                {
                    conn.Open();
                    string query = "SELECT section FROM tblsection ORDER BY section";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            comboSection.Items.Clear();
                            while (reader.Read())
                            {
                                comboSection.Items.Add(reader["section"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading sections: " + ex.Message);
            }
        }

        private void btnshowstudent_Click(object sender, EventArgs e)
        {
            if (comboSection.SelectedItem == null)
            {
                MessageBox.Show("Please select a section.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedSection = comboSection.SelectedItem.ToString();

            try
            {
                using (SQLiteConnection conn = dbConnection.GetConnection)
                {
                    conn.Open();

                    string query = @"
               SELECT 
                    s.lrn, 
                    s.fullname AS studentname, 
                    u.contact AS contact, 
                    s.teacher_name AS Teacher, 
                    s.section_enrolled AS section, 
                    s.subjects AS Subject, 
                    s.absent_count AS AbsentCount, 
                    s.present_count AS PresentCount, 
                    g.quarter AS Quarter, 
                    g.totalGrade AS QuarterlyGrade
                FROM vwStudentReport1 s
                LEFT JOIN tblGrade g 
                    ON s.lrn = g.lrn 
                    AND g.quarter = (SELECT MAX(quarter) FROM tblGrade WHERE lrn = s.lrn)
                LEFT JOIN tbluser u 
                    ON u.username = s.teacher_name
                INNER JOIN tblenrollment e 
                    ON e.lrn = s.lrn
                INNER JOIN tblacadyear ay 
                    ON e.aycode = ay.aycode
                WHERE s.section_enrolled = @section
                AND ay.status = 'Open'  -- Only include students from the currently open academic year
                GROUP BY s.lrn
                ORDER BY s.fullname";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@section", selectedSection);
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            dataGridStudentWithGrades.Rows.Clear();
                            bool hasStudents = false;

                            while (reader.Read())
                            {
                                hasStudents = true;
                                int rowIndex = dataGridStudentWithGrades.Rows.Add();
                                DataGridViewRow row = dataGridStudentWithGrades.Rows[rowIndex];

                                row.Cells["Lrn"].Value = reader["lrn"].ToString();
                                row.Cells["studentname"].Value = reader["studentname"].ToString();
                                row.Cells["contact"].Value = reader["contact"].ToString();
                                row.Cells["Teacher"].Value = reader["Teacher"].ToString();
                                row.Cells["section"].Value = reader["section"].ToString();
                                row.Cells["AbsentCount"].Value = reader["AbsentCount"].ToString();
                                row.Cells["PresentCount"].Value = reader["PresentCount"].ToString();
                                row.Cells["Quarter"].Value = reader["Quarter"].ToString();
                                row.Cells["QuarterlyGrade"].Value = reader["QuarterlyGrade"].ToString();

                                // Populate Subject ComboBox Column
                                DataGridViewComboBoxCell subjectCell = row.Cells["Subject"] as DataGridViewComboBoxCell;
                                if (subjectCell != null)
                                {
                                    subjectCell.Items.Clear();
                                    string[] subjects = reader["Subject"].ToString().Split(',');
                                    foreach (string subject in subjects)
                                    {
                                        subjectCell.Items.Add(subject.Trim());
                                    }
                                }
                            }

                            if (!hasStudents)
                            {
                                MessageBox.Show("No students are enrolled in this section for the current academic year.",
                                                "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading students: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridStudentWithGrades_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridStudentWithGrades.Columns["Subject"].Index && e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridStudentWithGrades.Rows[e.RowIndex];
                string selectedSubject = row.Cells["Subject"].Value?.ToString();
                string studentLrn = row.Cells["Lrn"].Value?.ToString();

                if (!string.IsNullOrEmpty(selectedSubject) && !string.IsNullOrEmpty(studentLrn))
                {
                    UpdateGradeForSelectedSubject(studentLrn, selectedSubject, row);
                }
            }
        }

        private void UpdateGradeForSelectedSubject(string lrn, string subject, DataGridViewRow row)
        {
            try
            {
                using (SQLiteConnection conn = dbConnection.GetConnection)
                {
                    conn.Open();

                    string query = @"
                SELECT quarter, totalGrade 
                FROM tblGrade 
                WHERE lrn = @lrn AND subject = @subject
                ORDER BY quarter DESC 
                LIMIT 1"; // Get latest quarter grade

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@lrn", lrn);
                        cmd.Parameters.AddWithValue("@subject", subject);

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                row.Cells["Quarter"].Value = reader["quarter"].ToString();
                                row.Cells["QuarterlyGrade"].Value = reader["totalGrade"].ToString();
                            }
                            else
                            {
                                row.Cells["Quarter"].Value = "N/A";
                                row.Cells["QuarterlyGrade"].Value = "N/A";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching grade: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
