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
    public partial class frmSection : Form
    {
        frmSectionList f;
        public static string _adviserID;
        public string _id;

        private DBConnection dbConnection;

        public frmSection(frmSectionList f)
        {
            InitializeComponent();
            dbConnection = new DBConnection();
            this.f = f;
        }

        private void cboAdviser_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to save this section? ", DBConnection._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Validate Grade Level
                if (string.IsNullOrEmpty(cmbGradeLevel.Text))
                {
                    MessageBox.Show("Grade Level is required.", DBConnection._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate Section Name
                if (string.IsNullOrEmpty(textBoxSection.Text))
                {
                    MessageBox.Show("Section name is required.", DBConnection._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate Adviser Selection
                if (cboAdviser.SelectedIndex == -1)
                {
                    MessageBox.Show("Adviser is required.", DBConnection._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Attempt to Save Section
                try
                {
                    using (SQLiteConnection cn = dbConnection.GetConnection)
                    {
                        if (cn.State != ConnectionState.Open)
                        {
                            cn.Open();
                        }

                        // Check if Section Already Exists
                        string checkQuery = "SELECT COUNT(*) FROM tblsection WHERE section = @section AND level = @level";
                        using (SQLiteCommand checkCmd = new SQLiteCommand(checkQuery, cn))
                        {
                            checkCmd.Parameters.AddWithValue("@section", textBoxSection.Text.Trim());
                            checkCmd.Parameters.AddWithValue("@level", Convert.ToInt32(cmbGradeLevel.Text));

                            int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                            if (count > 0)
                            {
                                MessageBox.Show("This section already exists for the selected grade level.", DBConnection._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }

                        // Insert Query
                        string query = "INSERT INTO tblsection (level, section, adviserID) VALUES (@level, @section, @adviserID)";
                        using (SQLiteCommand cm = new SQLiteCommand(query, cn))
                        {
                            cm.Parameters.AddWithValue("@level", Convert.ToInt32(cmbGradeLevel.Text));
                            cm.Parameters.AddWithValue("@section", textBoxSection.Text.Trim());
                            cm.Parameters.AddWithValue("@adviserID", _adviserID);

                            cm.ExecuteNonQuery();
                            MessageBox.Show("New Section saved successfully.", DBConnection._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Reset form
                            buttonCancel_Click(sender, e);
                            f.loadRecords(); // Reload records in the parent form
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", DBConnection._title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void frmSection_Load(object sender, EventArgs e)
        {
            getAdviser();
        }

        public void getAdviser()
        {
            try
            {
                cboAdviser.Items.Clear();

                using (SQLiteConnection cn = dbConnection.GetConnection)
                {
                    if (cn.State != ConnectionState.Open)
                    {
                        cn.Open();
                    }

                    string query = "SELECT lname || ', ' || fname || ' ' || mname AS adviser FROM tbluser";

                    using (SQLiteCommand cm = new SQLiteCommand(query, cn))
                    {
                        using (SQLiteDataReader dr = cm.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    string adviser = dr["adviser"]?.ToString();
                                    if (!string.IsNullOrEmpty(adviser))
                                    {
                                        cboAdviser.Items.Add(adviser);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("No records found in tbluser.", DBConnection._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", DBConnection._title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cboAdviser_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (SQLiteConnection cn = dbConnection.GetConnection)
                {
                    if (cn.State != ConnectionState.Open)
                    {
                        cn.Open();
                    }

                    string query = "SELECT username FROM tbluser WHERE lname || ', ' || fname || ' ' || mname = @adviser";

                    using (SQLiteCommand cm = new SQLiteCommand(query, cn))
                    {
                        cm.Parameters.AddWithValue("@adviser", cboAdviser.Text);

                        using (SQLiteDataReader dr = cm.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    _adviserID = dr["username"]?.ToString();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Adviser not found.", DBConnection._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", DBConnection._title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            cboAdviser.Text = "";
            cmbGradeLevel.Text = "";
            textBoxSection.Text = "";
            ButtonSave.Enabled = true;
            buttonUpdate.Enabled = false;
            cmbGradeLevel.Focus();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to update this section? ", DBConnection._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Attempt to Update Section
                try
                {
                    using (SQLiteConnection cn = dbConnection.GetConnection)
                    {
                        if (cn.State != ConnectionState.Open)
                        {
                            cn.Open();
                        }

                        // Check if Section Already Exists (excluding the current record being updated)
                        string checkQuery = "SELECT COUNT(*) FROM tblsection WHERE section = @section AND level = @level AND id != @id";
                        using (SQLiteCommand checkCmd = new SQLiteCommand(checkQuery, cn))
                        {
                            checkCmd.Parameters.AddWithValue("@section", textBoxSection.Text.Trim());
                            checkCmd.Parameters.AddWithValue("@level", Convert.ToInt32(cmbGradeLevel.Text));
                            checkCmd.Parameters.AddWithValue("@id", _id);

                            int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                            if (count > 0)
                            {
                                MessageBox.Show("This section already exists for the selected grade level.", DBConnection._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }

                        // Update Query
                        string query = "UPDATE tblsection SET level = @level, section = @section, adviserID = @adviserID WHERE id = @id";
                        using (SQLiteCommand cm = new SQLiteCommand(query, cn))
                        {
                            cm.Parameters.AddWithValue("@level", Convert.ToInt32(cmbGradeLevel.Text));
                            cm.Parameters.AddWithValue("@section", textBoxSection.Text.Trim());
                            cm.Parameters.AddWithValue("@adviserID", _adviserID);
                            cm.Parameters.AddWithValue("@id", _id);

                            cm.ExecuteNonQuery();
                            MessageBox.Show("Section successfully updated!", DBConnection._title, MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Reset form and refresh records
                            buttonCancel_Click(sender, e);
                            f.loadRecords();
                            this.Dispose();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", DBConnection._title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
