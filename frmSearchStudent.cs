using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace TeacherPortal
{
    public partial class frmSearchStudent : Form
    {
        private DBConnection dbConnection;

        frmEnroll f;
        public frmSearchStudent(frmEnroll f)
        {
            InitializeComponent();
            dbConnection = new DBConnection();
            this.f = f;
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
                    using (SQLiteCommand cm = new SQLiteCommand("SELECT * FROM tblstudent WHERE lname LIKE '" + txtSearch.Text + "%' ORDER BY lname, fname, mname", cn))
                    {
                        cn.Open();
                        dr = cm.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                dataGridViewStudentList.Rows.Add(dr[0], dr[1], dr[2], dr[3]);
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            loadRecords();
        }

        private void dataGridViewStudentList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string _column = dataGridViewStudentList.Columns[e.ColumnIndex].Name;

            if(_column == "colSelect")
            {
                f.txtLrn.Text = dataGridViewStudentList.Rows[e.RowIndex].Cells[0].Value.ToString();
                f.txtLname.Text = dataGridViewStudentList.Rows[e.RowIndex].Cells[1].Value.ToString();
                f.txtFname.Text = dataGridViewStudentList.Rows[e.RowIndex].Cells[2].Value.ToString();
                f.txtMname.Text = dataGridViewStudentList.Rows[e.RowIndex].Cells[3].Value.ToString();
                this.Dispose();
            }
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {
            toolTip1.SetToolTip(txtSearch, "Search by Last Name");
        }
    }
}
