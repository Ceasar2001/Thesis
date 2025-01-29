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
    public partial class frmEnrollmentList : Form
    {
        public string _aycode;
        private DBConnection dbConnection;
        public frmEnrollmentList()
        {
            InitializeComponent();
            dbConnection = new DBConnection();
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void loadRecord()
        {
            try
            {
                SQLiteDataReader dr;
                dataGridViewEnrollmentList.Rows.Clear();
                using (SQLiteConnection cn = dbConnection.GetConnection)
                {
                    using (SQLiteCommand cm = new SQLiteCommand("SELECT * FROM vwenrollment WHERE aycode = @aycode ", cn))
                    {
                        cn.Open();
                        cm.Parameters.AddWithValue("@aycode", _aycode);
                        dr = cm.ExecuteReader();
                        while (dr.Read())
                        {
                            dataGridViewEnrollmentList.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7], dr[8], dr[9]);
                        }
                        dr.Close();
                        cn.Close();
                        dataGridViewEnrollmentList.ClearSelection();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading enrollment records: {ex.Message}");
            }
        }

        public void getAY()
        {
            try
            {
                using (SQLiteConnection cn = dbConnection.GetConnection)
                {
                    using (SQLiteCommand cm = new SQLiteCommand("SELECT aycode FROM tblacadyear WHERE status = 'Open'", cn))
                    {
                        cn.Open();

                        using (SQLiteDataReader dr = cm.ExecuteReader())
                        {
                            if (dr.Read()) // Checks if there's at least one row
                            {
                                dbConnection._aycode = dr["aycode"].ToString();
                                this._aycode = dbConnection._aycode;
                            }
                        }

                        cn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error getting active academic year: {ex.Message}");
            }
        }

        private void newstudent_Click(object sender, EventArgs e)
        {
            frmEnroll frmenroll = new frmEnroll(this);
            frmenroll.lblAY.Text = _aycode;
            frmenroll.ShowDialog();
        }

        private void dataGridViewEnrollmentList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string _column = dataGridViewEnrollmentList.Columns[e.ColumnIndex].Name;

            if (_column == "colDrop")
            {
                if (MessageBox.Show("Do you want to Drop This Student?", DBConnection._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        using (SQLiteConnection cn = dbConnection.GetConnection)
                        {
                            using (SQLiteCommand cm = new SQLiteCommand("UPDATE tblenrollment set status = 'Dropped' WHERE enrollmentid like '" + dataGridViewEnrollmentList.Rows[e.RowIndex].Cells[0].Value.ToString() + "'", cn))
                            {
                                cn.Open();
                                cm.ExecuteNonQuery();

                                cn.Close();
                                MessageBox.Show("Student Successfully Dropped!", DBConnection._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                loadRecord();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error dropping student: {ex.Message}");
                    }
                }
            }
        }
    }
}
