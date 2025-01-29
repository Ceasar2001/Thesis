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

            LoadStudentsBySectionAndSubject(selectedSection, selectedSubject);
        }



        private void LoadStudentsBySectionAndSubject(string section, string subject)
        {
            try
            {
                SQLiteDataReader dr;
                dataGridStudentWithGrades.Rows.Clear();  // Clear any previous rows

                using (SQLiteConnection cn = dbConnection.GetConnection)
                {
                    using (SQLiteCommand cm = new SQLiteCommand(@"
                SELECT 
                    e.lrn, 
                    s.fname || ' ' || s.lname || ' ' || s.mname AS student_name
                FROM tblenrollment e
                JOIN tblstudent s ON e.lrn = s.lrn
                JOIN tblsection sec ON e.sectionid = sec.id
                JOIN tblEnrollmentSubjects es ON es.lrn = e.lrn
                WHERE sec.section = @section
                AND es.subject = @subject", cn))
                    {
                        cm.Parameters.AddWithValue("@section", section);
                        cm.Parameters.AddWithValue("@subject", subject);

                        cn.Open();
                        dr = cm.ExecuteReader();
                        while (dr.Read())
                        {
                            // Add rows to the dataGridStudentWithGrades with LRN and Student Name
                            dataGridStudentWithGrades.Rows.Add(dr["lrn"], dr["student_name"]);
                        }
                        dr.Close();
                        cn.Close();
                        dataGridStudentWithGrades.ClearSelection();  // Optional: to clear the selection after loading
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading student data: {ex.Message}", DBConnection._title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //private void btnGrade_Click(object sender, EventArgs e)
        //{
        //    frmEnterGrades enterGrades = new frmEnterGrades();
        //    enterGrades.ShowDialog();
        //}

        // Load all sections into comboSection
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

        // Load all subjects into comboSubject
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
                string lrn = dataGridStudentWithGrades.Rows[e.RowIndex].Cells["Column1"].Value.ToString(); // LRN (Column1)
                string studentName = dataGridStudentWithGrades.Rows[e.RowIndex].Cells["Column2"].Value.ToString(); // Student Name (Column2)
                string section = comboSection.SelectedItem.ToString(); // Get the selected section from comboSection
                string subject = comboSubject.SelectedItem.ToString(); // Get the selected subject from comboSubject

                // Create an instance of frmEnterGrades and pass the details
                frmEnterGrades enterGradesForm = new frmEnterGrades(lrn, studentName, section, subject);
                enterGradesForm.ShowDialog();  // Show the form as a modal dialog
            }
        }

    }
}
