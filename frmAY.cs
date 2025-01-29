using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SQLite;

namespace TeacherPortal
{
    public partial class frmAY : Form
    {
        private DBConnection dbConnection;
        frmAYList ayList;

        public frmAY(frmAYList ayList)
        {
            InitializeComponent();
            dbConnection = new DBConnection();
            this.ayList = ayList;
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void save_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to add a new academic year?", DBConnection._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (SQLiteConnection cn = dbConnection.GetConnection)
                    {

                        using (SQLiteCommand cmd = new SQLiteCommand("UPDATE tblacadyear SET status = 'Close'", cn))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        using (SQLiteCommand cmd = new SQLiteCommand("INSERT INTO tblacadyear(aycode, status) VALUES(@aycode, 'Open')", cn))
                        {
                            cmd.Parameters.AddWithValue("@aycode", academicYear.Text);
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Added New Academic Year Successfully", DBConnection._title, MessageBoxButtons.OK, MessageBoxIcon.Information);

                            academicYear.Clear();
                            academicYear.Focus();
                            ayList.loadRecords();
                        }
                    }
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, DBConnection._title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
