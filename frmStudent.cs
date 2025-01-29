using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace TeacherPortal
{
    public partial class frmStudent : Form
    {
        private frmStudentList studentList; // Reference to the student list form
        private DBConnection dbConnection; // Shared database connection utility

        public frmStudent(frmStudentList studentList)
        {
            InitializeComponent();
            dbConnection = new DBConnection();
            this.studentList = studentList;
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void birthdate_ValueChanged(object sender, EventArgs e)
        {
            GetAge();
        }

        public void GetAge()
        {
            try
            {
                DateTime dob = birthdate.Value;
                DateTime today = DateTime.Today;
                int age = today.Year - dob.Year;
                if (dob > today.AddYears(-age)) age--; // Adjust for upcoming birthday
                txtAge.Text = age.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error calculating age: " + ex.Message, DBConnection._title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            //validation
            if (txtLrn.Text == String.Empty)
            {
                MessageBox.Show("This Field is Required!", DBConnection._title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Do you want to save this record?", DBConnection._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SQLiteConnection cn = dbConnection.GetConnection) // No need to open it here
                {
                    try
                    {
                        cn.Open(); // Open the connection here

                        // Insert command to save the student record
                        using (SQLiteCommand cm = new SQLiteCommand("INSERT INTO tblstudent(lrn, lname, fname, mname, address, bdate, age, contact, father, father_occupation, mother, mother_occupation) VALUES(@lrn, @lname, @fname, @mname, @address, @bdate, @age, @contact, @father, @father_occupation, @mother, @mother_occupation)", cn))
                        {
                            cm.Parameters.AddWithValue("@lrn", txtLrn.Text);
                            cm.Parameters.AddWithValue("@lname", txtLname.Text);
                            cm.Parameters.AddWithValue("@fname", txtFname.Text);
                            cm.Parameters.AddWithValue("@mname", txtMname.Text);
                            cm.Parameters.AddWithValue("@address", txtAddress.Text);
                            cm.Parameters.AddWithValue("@bdate", birthdate.Value.ToString("yyyy-MM-dd"));
                            cm.Parameters.AddWithValue("@age", txtAge.Text);
                            cm.Parameters.AddWithValue("@contact", txtContact.Text);
                            cm.Parameters.AddWithValue("@father", txtFatherName.Text);
                            cm.Parameters.AddWithValue("@father_occupation", txtFatherOccupation.Text);
                            cm.Parameters.AddWithValue("@mother", txtMotherName.Text);
                            cm.Parameters.AddWithValue("@mother_occupation", txtMotherOccupation.Text);

                            // Execute the command
                            cm.ExecuteNonQuery();
                        }

                        // Notify the user and refresh the list
                        MessageBox.Show("Record has been saved!", DBConnection._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Dispose();
                        buttonCancel_Click(sender, e); // Reset the fields
                        studentList.loadRecords(); // Refresh the student list
                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions
                        MessageBox.Show("Error saving record: " + ex.Message, DBConnection._title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        cn.Close(); // Ensure the connection is closed after operation
                    }
                }
            }
        }




        private void buttonCancel_Click(object sender, EventArgs e)
        {
            txtAddress.Clear();
            txtFatherName.Clear();
            txtFname.Clear();
            txtLname.Clear();
            txtLrn.Clear();
            txtMname.Clear();
            txtMotherName.Clear();
            txtFatherOccupation.Clear();
            txtMotherOccupation.Clear();
            birthdate.Value = DateTime.Now;
            ButtonSave.Enabled = true;
            buttonUpdate.Enabled = false;
            txtLrn.Enabled = true;
            txtLrn.Focus();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to update this record?", DBConnection._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SQLiteConnection cn = dbConnection.GetConnection) // No need to open it here
                {
                    try
                    {
                        cn.Open();

                        using (SQLiteCommand cm = new SQLiteCommand("UPDATE tblstudent SET lname=@lname, fname=@fname, mname=@mname, address=@address, bdate=@bdate, age=@age, contact=@contact, father=@father, father_occupation=@father_occupation, mother=@mother, mother_occupation=@mother_occupation WHERE lrn=@lrn", cn))
                        {
                            
                            cm.Parameters.AddWithValue("@lname", txtLname.Text);
                            cm.Parameters.AddWithValue("@fname", txtFname.Text);
                            cm.Parameters.AddWithValue("@mname", txtMname.Text);
                            cm.Parameters.AddWithValue("@address", txtAddress.Text);
                            cm.Parameters.AddWithValue("@bdate", birthdate.Value.ToString("yyyy-MM-dd"));
                            cm.Parameters.AddWithValue("@age", txtAge.Text);
                            cm.Parameters.AddWithValue("@contact", txtContact.Text);
                            cm.Parameters.AddWithValue("@father", txtFatherName.Text);
                            cm.Parameters.AddWithValue("@father_occupation", txtFatherOccupation.Text);
                            cm.Parameters.AddWithValue("@mother", txtMotherName.Text);
                            cm.Parameters.AddWithValue("@mother_occupation", txtMotherOccupation.Text);
                            cm.Parameters.AddWithValue("@lrn", txtLrn.Text);

                            // Execute the command
                            cm.ExecuteNonQuery();
                        }

                        MessageBox.Show("Record has been updated Successfully!", DBConnection._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Dispose();
                        buttonCancel_Click(sender, e);
                        studentList.loadRecords();
                        this.Dispose();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error saving record: " + ex.Message, DBConnection._title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        cn.Close();
                    }
                }
            }
            
        }
    }
}
