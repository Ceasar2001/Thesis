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
    public partial class frmSearchSection : Form
    {
        private DBConnection dbConnection;
        frmEnroll f;

        public frmSearchSection(frmEnroll f)
        {
            InitializeComponent();
            dbConnection = new DBConnection();
            this.f = f;
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

        private void dataGridViewSection_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string _column = dataGridViewSection.Columns[e.ColumnIndex].Name;

            if (_column == "colSelect")
            {
                f._sectionid = dataGridViewSection.Rows[e.RowIndex].Cells[0].Value.ToString();
                f.txtLevel.Text = dataGridViewSection.Rows[e.RowIndex].Cells[1].Value.ToString();
                f.txtSection.Text = dataGridViewSection.Rows[e.RowIndex].Cells[2].Value.ToString();
                f._adviserid = dataGridViewSection.Rows[e.RowIndex].Cells[3].Value.ToString();
                f.txtAdviser.Text = dataGridViewSection.Rows[e.RowIndex].Cells[4].Value.ToString();
                this.Dispose();
            }
        }
    }
}
