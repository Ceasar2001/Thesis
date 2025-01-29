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
    public partial class frmSectionList : Form
    {
        private DBConnection dbConnection;
        public frmSectionList()
        {
            InitializeComponent();
            dbConnection = new DBConnection();
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void loadRecords()
        {
            try
            {
                SQLiteDataReader dr;
                dataGridViewSection.Rows.Clear();

                using (SQLiteConnection cn = dbConnection.GetConnection)
                {
                    // Check if vwsection view exists
                    string checkViewQuery = "SELECT name FROM sqlite_master WHERE type='view' AND name='vwsection';";
                    using (SQLiteCommand checkViewCmd = new SQLiteCommand(checkViewQuery, cn))
                    {
                        if (cn.State != ConnectionState.Open)
                        {
                            cn.Open(); // Ensure the connection is open before executing the command
                        }

                        object result = checkViewCmd.ExecuteScalar();
                        if (result == null)
                        {
                            MessageBox.Show("The view 'vwsection' does not exist.");
                            return;
                        }
                    }

                    // Now proceed with the main query
                    string query = "SELECT * FROM vwsection";
                    using (SQLiteCommand cm = new SQLiteCommand(query, cn))
                    {
                        dr = cm.ExecuteReader();
                        while (dr.Read())
                        {
                            dataGridViewSection.Rows.Add(dr["id"], dr["level"], dr["section"], dr["adviserID"], dr["adviser"]);
                        }
                        dr.Close();
                    }
                }
                dataGridViewSection.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading records: " + ex.Message);
            }
        }

        private void newsection_Click(object sender, EventArgs e)
        {
            frmSection frmSection = new frmSection(this);
            frmSection.ShowDialog(); // Keep only this call.
            frmSection.getAdviser();
        }

        private void dataGridViewSection_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string _column = dataGridViewSection.Columns[e.ColumnIndex].Name;
            if(_column == "colEdit")
            {
                frmSection frmSection = new frmSection(this);
                frmSection.getAdviser();
                frmSection._id = dataGridViewSection.Rows[e.RowIndex].Cells[0].Value.ToString();
                frmSection._adviserID = dataGridViewSection.Rows[e.RowIndex].Cells[3].Value.ToString();
                frmSection.cmbGradeLevel.Text = dataGridViewSection.Rows[e.RowIndex].Cells[1].Value.ToString();
                frmSection.textBoxSection.Text = dataGridViewSection.Rows[e.RowIndex].Cells[2].Value.ToString();
                frmSection.cboAdviser.Text = dataGridViewSection.Rows[e.RowIndex].Cells[4].Value.ToString();
                frmSection.ButtonSave.Enabled = false;
                frmSection.ShowDialog();
            }
            else if (_column == "colDelete")
            {
                if (MessageBox.Show("Are you sure you want to delete this record?", DBConnection._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (SQLiteConnection cn = dbConnection.GetConnection)
                    {
                        if (cn.State != ConnectionState.Open)
                        {
                            cn.Open();
                        }
                        string query = "DELETE FROM tblsection WHERE id = @id";
                        using (SQLiteCommand cm = new SQLiteCommand(query, cn))
                        {
                            cm.Parameters.AddWithValue("@id", dataGridViewSection.Rows[e.RowIndex].Cells[0].Value.ToString());
                            cm.ExecuteNonQuery();
                            MessageBox.Show("Section Successfully Deleted!", DBConnection._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    loadRecords();
                }
            }
        }
    }
}
 