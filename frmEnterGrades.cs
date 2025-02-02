using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace TeacherPortal
{
    public partial class frmEnterGrades : Form
    {
        private DBConnection dbConnection;
        private string studentLrn;  // Added LRN to associate with the student

        public frmEnterGrades(string lrn, string studentName, string section, string subject)
        {
            InitializeComponent();
            dbConnection = new DBConnection();
            studentLrn = lrn;  // Store the student's LRN

            // Populate the textboxes with the values passed to the form
            textBoxStudetname.Text = studentName;
            textBoxSection.Text = section;
            textBoxSubject.Text = subject;

            // You can also set other fields like LRN if needed
            // textBoxLrn.Text = lrn;
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        // Written Works Logic...
        private int highScoreWrittenWorks = 40;

        private void dataGridViewWrittenWorks_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            UpdateWrittenTotal();
        }

        private void UpdateWrittenTotal()
        {
            int total = 0;
            foreach (DataGridViewRow row in dataGridViewWrittenWorks.Rows)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (int.TryParse(row.Cells[i].Value?.ToString(), out int value))
                    {
                        total += value;
                    }
                }
            }

            lblWrittenTotal.Text = total.ToString();
            double ps = highScoreWrittenWorks > 0 ? ((double)total / highScoreWrittenWorks) * 100 : 0;
            lblWrittenPs.Text = ps.ToString("0.00");

            double ws = ps * 0.30;
            lblWrittenWs.Text = ws.ToString("0.00");

            ComputeGrades();
        }

        // Performance Task Logic...
        private int highScorePerformanceTask = 20;

        private void dataGridViewPerformanceTask_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            UpdatePerformanceTotal();
        }

        private void UpdatePerformanceTotal()
        {
            int total = 0;
            foreach (DataGridViewRow row in dataGridViewPerformanceTask.Rows)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (int.TryParse(row.Cells[i].Value?.ToString(), out int value))
                    {
                        total += value;
                    }
                }
            }

            lblperformanceTotal.Text = total.ToString();
            double ps = highScorePerformanceTask > 0 ? ((double)total / highScorePerformanceTask) * 100 : 0;
            lblPerformPs.Text = ps.ToString("0.00");

            double ws = ps * 0.50;
            lblPerformWs.Text = ws.ToString("0.00");

            ComputeGrades();
        }

        // Quarterly Assessment Logic...
        private int highScoreQuarterly = 40;

        private void UpdateQuarterlyScores()
        {
            if (int.TryParse(textBoxQuarterly.Text, out int userScore))
            {
                if (userScore > highScoreQuarterly)
                {
                    userScore = highScoreQuarterly;
                    textBoxQuarterly.Text = userScore.ToString();
                    textBoxQuarterly.SelectionStart = textBoxQuarterly.Text.Length;
                }

                double ps = ((double)userScore / highScoreQuarterly) * 100;
                lblQuaterPs.Text = ps.ToString("0.00");

                double ws = ps * 0.20;
                lblQuaterWs.Text = ws.ToString("0.00");
            }
            else
            {
                lblQuaterPs.Text = "0.00";
                lblQuaterWs.Text = "0.00";
            }

            ComputeGrades();
        }

        private void textBoxQuarterly_TextChanged(object sender, EventArgs e)
        {
            UpdateQuarterlyScores();
        }

        // Function to Compute Initial and Quarterly Grades
        private void ComputeGrades()
        {
            double wsWritten = double.TryParse(lblWrittenWs.Text, out double wsw) ? wsw : 0;
            double wsPerformance = double.TryParse(lblPerformWs.Text, out double wsp) ? wsp : 0;
            double wsQuarterly = double.TryParse(lblQuaterWs.Text, out double wsq) ? wsq : 0;

            double initialGrade = wsWritten + wsPerformance + wsQuarterly;
            lblInitialGrade.Text = initialGrade.ToString("0.00");

            double quarterlyGrade = (initialGrade * 0.6) + 40;
            lblQuaterGrade.Text = quarterlyGrade.ToString("0");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to save the grades for this student?", DBConnection._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    // Check if comboBoxQuarter is empty
                    if (comboBoxQuarter.SelectedItem == null || string.IsNullOrEmpty(comboBoxQuarter.SelectedItem.ToString()))
                    {
                        // Display warning picture and text
                        pictureWarning.Visible = true;
                        labelWarningText.Visible = true;
                        labelWarningText.Text = "Please select a quarter before saving.";

                        // Optionally, set focus back to the comboBoxQuarter
                        comboBoxQuarter.Focus();

                        return; // Prevent the save from happening
                    }
                    else
                    {
                        // Hide warning picture and text when a quarter is selected
                        pictureWarning.Visible = false;
                        labelWarningText.Visible = false;
                    }

                    using (SQLiteConnection cn = dbConnection.GetConnection)
                    {
                        cn.Open();
                        using (SQLiteTransaction transaction = cn.BeginTransaction())
                        {
                            try
                            {
                                string quarter = comboBoxQuarter.SelectedItem.ToString();

                                double writtenScore = double.TryParse(lblWrittenWs.Text, out double wsWritten) ? wsWritten : 0;
                                double performanceScore = double.TryParse(lblPerformWs.Text, out double wsPerformance) ? wsPerformance : 0;
                                double quarterlyScore = double.TryParse(lblQuaterWs.Text, out double wsQuarterly) ? wsQuarterly : 0;
                                double totalGrade = writtenScore + performanceScore + quarterlyScore;

                                // Insert grade into the database, including the student's LRN as a foreign key
                                using (SQLiteCommand cmd = new SQLiteCommand("INSERT INTO tblGrade (lrn, section, subject, quarter, writtenScore, performanceScore, quarterlyScore, totalGrade) VALUES (@lrn, @section, @subject, @quarter, @writtenScore, @performanceScore, @quarterlyScore, @totalGrade)", cn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@lrn", studentLrn);  // Use the student's LRN here
                                    cmd.Parameters.AddWithValue("@section", textBoxSection.Text);
                                    cmd.Parameters.AddWithValue("@subject", textBoxSubject.Text);
                                    cmd.Parameters.AddWithValue("@quarter", quarter);
                                    cmd.Parameters.AddWithValue("@writtenScore", writtenScore);
                                    cmd.Parameters.AddWithValue("@performanceScore", performanceScore);
                                    cmd.Parameters.AddWithValue("@quarterlyScore", quarterlyScore);
                                    cmd.Parameters.AddWithValue("@totalGrade", totalGrade);
                                    cmd.ExecuteNonQuery();
                                }

                                transaction.Commit();

                                MessageBox.Show("Grades successfully saved!", DBConnection._title, MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Notify frmGrading to refresh
                                if (Application.OpenForms["frmGrading"] is frmGrading gradingForm)
                                {
                                    gradingForm.LoadStudentsWithGrades(textBoxSection.Text, textBoxSubject.Text);
                                }

                                this.Dispose();
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                MessageBox.Show($"Error saving grade: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Database connection error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
