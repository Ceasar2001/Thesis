//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using System.Data.SQLite;

//namespace TeacherPortal
//{
//    public partial class frmEnroll : Form
//    {
//        private DBConnection dbConnection;
//        public string _sectionid = "";
//        public string _adviserid = "";

//        frmEnrollmentList fEnrollList;
//        public frmEnroll(frmEnrollmentList fEnrollList)
//        {
//            InitializeComponent();
//            dbConnection = new DBConnection();
//            this.fEnrollList = fEnrollList;

//        }

//        private void pictureBoxClose_Click(object sender, EventArgs e)
//        {
//            this.Dispose();
//        }

//        private void searchLrn_Click(object sender, EventArgs e)
//        {
//            frmSearchStudent Fs = new frmSearchStudent(this);
//            Fs.loadRecords();
//            Fs.ShowDialog();
//        }

//        private void searchLevel_Click(object sender, EventArgs e)
//        {
//            frmSearchSection Fs = new frmSearchSection(this);
//            Fs.loadRecords();
//            Fs.ShowDialog();
//        }

//        private void ButtonSave_Click(object sender, EventArgs e)
//        {
//            if (MessageBox.Show("Do you want to enroll this student?", DBConnection._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
//            {
//                try
//                {
//                    using (SQLiteConnection cn = dbConnection.GetConnection)
//                    {
//                        cn.Open();

//                        // Check if the student is already enrolled
//                        using (SQLiteCommand checkCommand = new SQLiteCommand("SELECT COUNT(*) FROM tblenrollment WHERE lrn = @lrn AND aycode = @aycode", cn))
//                        {
//                            checkCommand.Parameters.AddWithValue("@lrn", txtLrn.Text);
//                            checkCommand.Parameters.AddWithValue("@aycode", lblAY.Text);

//                            int count = Convert.ToInt32(checkCommand.ExecuteScalar());

//                            if (count > 0)
//                            {
//                                MessageBox.Show("This student is already enrolled in the current academic year                                                      .", DBConnection._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                                return;
//                            }
//                        }

//                        // Proceed with the enrollment
//                        using (SQLiteCommand cm = new SQLiteCommand("INSERT INTO tblenrollment (lrn, sectionid, aycode, edate) VALUES (@lrn, @sectionid, @aycode, @edate)", cn))
//                        {
//                            cm.Parameters.AddWithValue("@lrn", txtLrn.Text);
//                            cm.Parameters.AddWithValue("@sectionid", _sectionid);
//                            cm.Parameters.AddWithValue("@aycode", lblAY.Text);
//                            cm.Parameters.AddWithValue("@edate", DateTime.Now.ToString("yyyy-MM-dd"));

//                            cm.ExecuteNonQuery();
//                        }

//                        cn.Close();

//                        MessageBox.Show("Student Successfully Enrolled!", DBConnection._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
//                        fEnrollList.loadRecord();
//                        buttonCancel_Click(sender, e);
//                    }
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show($"Error saving enrollment record: {ex.Message}");
//                }
//            }
//        }

//        private void buttonCancel_Click(object sender, EventArgs e)
//        {
//            txtAdviser.Clear();
//            txtFname.Clear();
//            txtLname.Clear();
//            txtMname.Clear();
//            txtLevel.Clear();
//            txtSection.Clear();
//            txtLrn.Clear();

//        }
//    }
//}


using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace TeacherPortal
{
    public partial class frmEnroll : Form
    {
        private DBConnection dbConnection;
        public string _sectionid = "";
        public string _adviserid = "";

        frmEnrollmentList fEnrollList;

        // Dictionary to store subjects for each grade level
        private Dictionary<int, List<string>> gradeSubjects = new Dictionary<int, List<string>>()
        {
            { 1, new List<string> { "gmrc", "reading_literacy", "language", "math", "makabansa" } },
            { 2, new List<string> { "esp", "filipino", "math", "aralpan", "english", "makabayan" } },
            { 3, new List<string> { "esp", "filipino", "math", "aralpan", "english", "makabayan", "science" } },
            { 4, new List<string> { "esp", "filipino", "math", "aralpan", "english", "makabayan", "eep", "tle" } },
            { 5, new List<string> { "esp", "filipino", "math", "aralpan", "english", "makabayan", "eep", "tle" } },
            { 6, new List<string> { "esp", "filipino", "math", "aralpan", "english", "makabayan", "science", "eep", "tle" } }
        };

        public frmEnroll(frmEnrollmentList fEnrollList)
        {
            InitializeComponent();
            dbConnection = new DBConnection();
            this.fEnrollList = fEnrollList;

            // Set all checkboxes to disabled by default
            DisableAllSubjects();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void searchLrn_Click(object sender, EventArgs e)
        {
            frmSearchStudent Fs = new frmSearchStudent(this);
            Fs.loadRecords();
            Fs.ShowDialog();
        }

        private void searchLevel_Click(object sender, EventArgs e)
        {
            frmSearchSection Fs = new frmSearchSection(this);
            Fs.loadRecords();
            Fs.ShowDialog();
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to enroll this student?", DBConnection._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SQLiteConnection cn = dbConnection.GetConnection)
                    {
                        cn.Open();

                        // Check if the student is already enrolled in the current academic year
                        using (SQLiteCommand checkCommand = new SQLiteCommand("SELECT COUNT(*) FROM tblenrollment WHERE lrn = @lrn AND aycode = @aycode", cn))
                        {
                            checkCommand.Parameters.AddWithValue("@lrn", txtLrn.Text);
                            checkCommand.Parameters.AddWithValue("@aycode", lblAY.Text);

                            int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                            if (count > 0)
                            {
                                MessageBox.Show("This student is already enrolled in the current academic year.", DBConnection._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }

                        // Start transaction to ensure both student enrollment and subject insertion succeed together
                        using (SQLiteTransaction transaction = cn.BeginTransaction())
                        {
                            try
                            {
                                // Enroll the student
                                using (SQLiteCommand cm = new SQLiteCommand("INSERT INTO tblenrollment (lrn, sectionid, aycode, edate) VALUES (@lrn, @sectionid, @aycode, @edate)", cn, transaction))
                                {
                                    cm.Parameters.AddWithValue("@lrn", txtLrn.Text);
                                    cm.Parameters.AddWithValue("@sectionid", _sectionid);
                                    cm.Parameters.AddWithValue("@aycode", lblAY.Text);
                                    cm.Parameters.AddWithValue("@edate", DateTime.Now.ToString("yyyy-MM-dd"));
                                    cm.ExecuteNonQuery();
                                }

                                // Save subjects in tblEnrollmentSubjects
                                SaveSubjects(cn, txtLrn.Text, lblAY.Text, transaction);

                                // Commit transaction if everything is successful
                                transaction.Commit();

                                MessageBox.Show("Student Successfully Enrolled with Subjects!", DBConnection._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                fEnrollList.loadRecord();
                                buttonCancel_Click(sender, e);
                            }
                            catch (Exception ex)
                            {
                                // Rollback transaction in case of error
                                transaction.Rollback();
                                MessageBox.Show($"Error saving enrollment record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                        cn.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Database connection error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SaveSubjects(SQLiteConnection cn, string lrn, string aycode, SQLiteTransaction transaction)
        {
            if (int.TryParse(txtLevel.Text, out int gradeLevel) && gradeSubjects.ContainsKey(gradeLevel))
            {
                foreach (var subject in gradeSubjects[gradeLevel])
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("INSERT INTO tblEnrollmentSubjects (lrn, subject, aycode) VALUES (@lrn, @subject, @aycode)", cn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@lrn", lrn);
                        cmd.Parameters.AddWithValue("@subject", subject);
                        cmd.Parameters.AddWithValue("@aycode", aycode);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }



        private void buttonCancel_Click(object sender, EventArgs e)
        {
            txtAdviser.Clear();
            txtFname.Clear();
            txtLname.Clear();
            txtMname.Clear();
            txtLevel.Clear();
            txtSection.Clear();
            txtLrn.Clear();

            // Reset checkboxes
            DisableAllSubjects();
        }

        private void DisableAllSubjects()
        {
            // Disable all checkboxes by default
            english.Checked = false;
            math.Checked = false;
            science.Checked = false;
            filipino.Checked = false;
            eep.Checked = false;
            tle.Checked = false;
            aralpan.Checked = false;
            makabayan.Checked = false;
            esp.Checked = false;
            makabansa.Checked = false;
            language.Checked = false;
            reading_literacy.Checked = false;

            english.Enabled = false;
            math.Enabled = false;
            science.Enabled = false;
            filipino.Enabled = false;
            eep.Enabled = false;
            tle.Enabled = false;
            aralpan.Enabled = false;
            makabayan.Enabled = false;
            esp.Enabled = false;
            makabansa.Enabled = false;
            language.Enabled = false;
            reading_literacy.Enabled = false;
        }

        private void AssignSubjects()
        {
            // Convert the level text to an integer (assuming txtLevel contains the grade level)
            if (int.TryParse(txtLevel.Text, out int gradeLevel))
            {
                if (gradeSubjects.ContainsKey(gradeLevel))
                {
                    // Enable and check only the relevant subjects for the given grade level
                    foreach (var subject in gradeSubjects[gradeLevel])
                    {
                        switch (subject)
                        {
                            case "english":
                                english.Checked = true;
                                break;
                            case "math":
                                math.Checked = true;
                                break;
                            case "science":
                                science.Checked = true;
                                break;
                            case "filipino":
                                filipino.Checked = true;
                                break;
                            case "eep":
                                eep.Checked = true;
                                break;
                            case "tle":
                                tle.Checked = true;
                                break;
                            case "aralpan":
                                aralpan.Checked = true;
                                break;
                            case "makabayan":
                                makabayan.Checked = true;
                                break;
                            case "esp":
                                esp.Checked = true;
                                break;
                            case "makabansa":
                                makabansa.Checked = true;
                                break;
                            case "language":
                                language.Checked = true;
                                break;
                            case "reading_literacy":
                                reading_literacy.Checked = true;
                                break;
                        }
                    }
                }
            }
        }
    }
}
