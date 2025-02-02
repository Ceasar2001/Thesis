using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace TeacherPortal
{
    public partial class frmStudentList : Form
    {
        private DBConnection dbConnection;

        public frmStudentList()
        {
            InitializeComponent();
            dbConnection = new DBConnection();
            loadRecords();
        }

        // Ensure records are loaded when the form is shown
        private void frmStudentList_Load(object sender, EventArgs e)
        {
            // Call the method to load student records
            MessageBox.Show("Loading Records...");
            loadRecords();
        }

        public void loadRecords()
        {
            try
            {
                SQLiteDataReader dr;
                dataGridViewStudentList.Rows.Clear();  // Clear any previous data

                // Ensure the connection is established correctly
                using (SQLiteConnection cn = dbConnection.GetConnection)
                {
                    using (SQLiteCommand cm = new SQLiteCommand("SELECT * FROM tblstudent ORDER BY lname, fname, mname", cn))
                    {
                        cn.Open();
                        dr = cm.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                dataGridViewStudentList.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], DateTime.Parse(dr[5].ToString()).ToShortDateString(), dr[6], dr[7], dr[8], dr[9], dr[10], dr[11]);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Add Student to display records found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        dr.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                MessageBox.Show("Error loading records: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void newstudent_Click(object sender, EventArgs e)
        {
            frmStudent student = new frmStudent(this);
            student.buttonUpdate.Enabled = false;
            student.ShowDialog();
        }

        private void dataGridViewStudent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string _column = dataGridViewStudentList.Columns[e.ColumnIndex].Name;
            if(_column == "colEdit")
            {
                frmStudent student = new frmStudent(this);
                student.ButtonSave.Enabled = false;
                student.txtLrn.Enabled = false;
                student.txtLrn.Text = dataGridViewStudentList.Rows[e.RowIndex].Cells[0].Value.ToString();
                student.txtLname.Text = dataGridViewStudentList.Rows[e.RowIndex].Cells[1].Value.ToString();
                student.txtFname.Text = dataGridViewStudentList.Rows[e.RowIndex].Cells[2].Value.ToString();
                student.txtMname.Text = dataGridViewStudentList.Rows[e.RowIndex].Cells[3].Value.ToString();
                student.txtAddress.Text = dataGridViewStudentList.Rows[e.RowIndex].Cells[4].Value.ToString();

                string birthdateValue = dataGridViewStudentList.Rows[e.RowIndex].Cells[5].Value.ToString();
                DateTime parsedDate;
                if (DateTime.TryParse(birthdateValue, out parsedDate))
                {
                    student.birthdate.Text = parsedDate.ToString("yyyy-MM-dd");
                }
                else
                {
                    student.birthdate.Text = "";
                }

                student.txtContact.Text = dataGridViewStudentList.Rows[e.RowIndex].Cells[6].Value.ToString();
                student.txtFatherName.Text = dataGridViewStudentList.Rows[e.RowIndex].Cells[7].Value.ToString();
                student.txtFatherOccupation.Text = dataGridViewStudentList.Rows[e.RowIndex].Cells[8].Value.ToString();
                student.txtMotherName.Text = dataGridViewStudentList.Rows[e.RowIndex].Cells[9].Value.ToString();
                student.txtMotherOccupation.Text = dataGridViewStudentList.Rows[e.RowIndex].Cells[10].Value.ToString();
                student.ShowDialog();
            }
            else if (_column == "colDelete")
            {
                if(MessageBox.Show("Are you sure you want to delete this record?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (SQLiteConnection cn = dbConnection.GetConnection)
                    {
                        if (cn.State != ConnectionState.Open)
                        {
                            cn.Open();
                        }
                        string query = "DELETE FROM tblstudent WHERE lrn = @lrn";
                        using (SQLiteCommand cm = new SQLiteCommand(query, cn))
                        {
                            cm.Parameters.AddWithValue("@lrn", dataGridViewStudentList.Rows[e.RowIndex].Cells[0].Value.ToString());
                            cm.ExecuteNonQuery();
                            MessageBox.Show("Student record successfully deleted.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    loadRecords();
                }
            }
        }

        private bool isHidden = false; // Flag to track the state (hidden or shown)

        private void buttonShow_Click(object sender, EventArgs e)
        {
            try
            {
                if (isHidden)
                {
                    // Show the records
                    loadRecords();
                    buttonShow.Text = "Hide Students";
                    isHidden = false;
                }
                else
                {
                    //MessageBox.Show("Hiding all student records...");
                    dataGridViewStudentList.Rows.Clear();
                    buttonShow.Text = "Show Students";
                    isHidden = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
