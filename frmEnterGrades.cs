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
    public partial class frmEnterGrades : Form
    {
        private DBConnection dbConnection;

        public frmEnterGrades(string lrn, string studentName, string section, string subject)
        {
            InitializeComponent();
            dbConnection = new DBConnection();

            // Populate the textboxes with the values passed to the form
            textBoxStudetname.Text = studentName;
            textBoxSection.Text = section;
            textBoxSubject.Text = subject;

            // If you need to save the LRN for later use (for example when saving grades), you can store it in a private field or directly use it
            //textBoxLrn.Text = lrn;  // Assuming you want to display LRN in a textbox (create it if not already done)
        }


        private void close_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }



        // Written Works
        // High Score for Written Works (Change this value accordingly)
        private int highScoreWrittenWorks = 40; // Example: High score is 40

        private void dataGridViewWrittenWorks_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            UpdateWrittenTotal();
        }

        private void dataGridViewWrittenWorks_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox textBox)
            {
                textBox.KeyPress -= TextBox_KeyPress;
                textBox.KeyPress += TextBox_KeyPress;
            }
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
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

            // Recalculate Grades
            ComputeGrades();
        }



        // Performance Task
        // High Score for Performance Task (Change this value accordingly)
        private int highScorePerformanceTask = 20; // Example: High score is 20

        private void dataGridViewPerformanceTask_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            UpdatePerformanceTotal();
        }

        private void dataGridViewPerformanceTask_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox textBox)
            {
                textBox.KeyPress -= TextBox_KeyPress;
                textBox.KeyPress += TextBox_KeyPress;
            }
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

            // Recalculate Grades
            ComputeGrades();
        }


        // Quarterly Assessment
        // High Score for Quarterly Assessment (Change if needed)
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

            // Recalculate Grades
            ComputeGrades();
        }


        private void textBoxQuarterly_TextChanged(object sender, EventArgs e)
        {
            UpdateQuarterlyScores();
        }





        //Initial Grade(IG) = WS of Written Works + WS of Performance Task + WS of Quarterly Assessment
        //


        // Function to Compute Initial and Quarterly Grades
        private void ComputeGrades()
        {
            // Parse the Weighted Scores (WS) from labels
            double wsWritten = double.TryParse(lblWrittenWs.Text, out double wsw) ? wsw : 0;
            double wsPerformance = double.TryParse(lblPerformWs.Text, out double wsp) ? wsp : 0;
            double wsQuarterly = double.TryParse(lblQuaterWs.Text, out double wsq) ? wsq : 0;

            // Compute Initial Grade (IG)
            double initialGrade = wsWritten + wsPerformance + wsQuarterly;
            lblInitialGrade.Text = initialGrade.ToString("0.00");

            // Compute Quarterly Grade (QG)
            double quarterlyGrade = (initialGrade * 0.6) + 40;
            lblQuaterGrade.Text = quarterlyGrade.ToString("0");
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to save the grades for this student?", DBConnection._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SQLiteConnection cn = dbConnection.GetConnection)
                    {
                        cn.Open();
                        using (SQLiteTransaction transaction = cn.BeginTransaction())
                        {
                            try
                            {
                                string studentName = textBoxStudetname.Text;
                                string section = textBoxSection.Text;
                                string subject = textBoxSubject.Text;
                                string quarter = comboBoxQuarter.SelectedItem.ToString();

                                if (quarter == null)
                                {
                                    MessageBox.Show("Please select a quarter.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }

                                double writtenScore = double.TryParse(lblWrittenWs.Text, out double wsWritten) ? wsWritten : 0;
                                double performanceScore = double.TryParse(lblPerformWs.Text, out double wsPerformance) ? wsPerformance : 0;
                                double quarterlyScore = double.TryParse(lblQuaterWs.Text, out double wsQuarterly) ? wsQuarterly : 0;
                                double totalGrade = writtenScore + performanceScore + quarterlyScore;

                                using (SQLiteCommand cmd = new SQLiteCommand("INSERT INTO tblGrade (studentName, section, subject, quarter, writtenScore, performanceScore, quarterlyScore, totalGrade) VALUES (@studentName, @section, @subject, @quarter, @writtenScore, @performanceScore, @quarterlyScore, @totalGrade)", cn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@studentName", studentName);
                                    cmd.Parameters.AddWithValue("@section", section);
                                    cmd.Parameters.AddWithValue("@subject", subject);
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
                                    gradingForm.LoadStudentsWithGrades(section, subject);
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
