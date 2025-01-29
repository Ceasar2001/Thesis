using System;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data;

namespace TeacherPortal
{
    public partial class frmAYList : Form
    {
        private DBConnection dbConnection;

        public frmAYList()
        {
            InitializeComponent();
            dbConnection = new DBConnection();
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void newAcadYear_Click(object sender, EventArgs e)
        {
            frmAY ay = new frmAY(this);
            ay.ShowDialog();
        }

        public void loadRecords()
        {
            try
            {
                dataGridViewAcadYear.Rows.Clear();

                using (SQLiteConnection cn = dbConnection.GetConnection)
                {
                    if (cn.State != ConnectionState.Open)
                    {
                        cn.Open();
                    }

                    using (SQLiteCommand cm = new SQLiteCommand("SELECT * FROM tblacadyear", cn))
                    {
                        using (SQLiteDataReader dr = cm.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    dataGridViewAcadYear.Rows.Add(dr["aycode"].ToString(), dr["status"].ToString());
                                }
                            }
                            else
                            {
                                MessageBox.Show("No records found.", DBConnection._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }

                dataGridViewAcadYear.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, DBConnection._title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewAcadYear_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                using (SQLiteConnection cn = dbConnection.GetConnection)
                {
                    if (cn.State != ConnectionState.Open)
                    {
                        cn.Open();
                    }

                    string _column = dataGridViewAcadYear.Columns[e.ColumnIndex].Name;
                    string aycode = dataGridViewAcadYear.Rows[e.RowIndex].Cells[0].Value.ToString();

                    if (_column == "colOpen" || _column == "colClose")
                    {
                        // Check the current status of the academic year
                        using (SQLiteCommand cm = new SQLiteCommand("SELECT status FROM tblacadyear WHERE aycode = @aycode", cn))
                        {
                            cm.Parameters.AddWithValue("@aycode", aycode);
                            string currentStatus = cm.ExecuteScalar()?.ToString();

                            if (_column == "colOpen")
                            {
                                if (currentStatus == "Open")
                                {
                                    MessageBox.Show($"The academic year {aycode} is already open.", DBConnection._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                                else
                                {
                                    if (MessageBox.Show($"Do you want to open the academic year {aycode}?", DBConnection._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        using (SQLiteCommand updateCloseCmd = new SQLiteCommand("UPDATE tblacadyear SET status = 'Close'", cn))
                                        {
                                            updateCloseCmd.ExecuteNonQuery();
                                        }

                                        using (SQLiteCommand updateOpenCmd = new SQLiteCommand("UPDATE tblacadyear SET status = 'Open' WHERE aycode = @aycode", cn))
                                        {
                                            updateOpenCmd.Parameters.AddWithValue("@aycode", aycode);
                                            updateOpenCmd.ExecuteNonQuery();
                                        }

                                        MessageBox.Show($"Academic Year {aycode} has been successfully opened.", DBConnection._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        loadRecords();
                                    }
                                }
                            }
                            else if (_column == "colClose")
                            {
                                if (currentStatus == "Close")
                                {
                                    MessageBox.Show($"The academic year {aycode} is already closed.", DBConnection._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                                else
                                {
                                    if (MessageBox.Show($"Do you want to close the academic year {aycode}?", DBConnection._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        using (SQLiteCommand cmUpdateClose = new SQLiteCommand("UPDATE tblacadyear SET status = 'Close' WHERE aycode = @aycode", cn))
                                        {
                                            cmUpdateClose.Parameters.AddWithValue("@aycode", aycode);
                                            cmUpdateClose.ExecuteNonQuery();
                                        }

                                        MessageBox.Show($"Academic Year {aycode} has been successfully closed.", DBConnection._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        loadRecords();
                                    }
                                }
                            }
                        }
                    }
                    else if (_column == "colDelete")
                    {
                        if (MessageBox.Show($"Do you want to delete the academic year {aycode}?", DBConnection._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            using (SQLiteCommand cm = new SQLiteCommand("DELETE FROM tblacadyear WHERE aycode = @aycode", cn))
                            {
                                cm.Parameters.AddWithValue("@aycode", aycode);
                                cm.ExecuteNonQuery();
                            }
                            MessageBox.Show($"Academic Year {aycode} has been successfully deleted.", DBConnection._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadRecords();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, DBConnection._title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
