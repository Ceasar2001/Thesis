using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace TeacherPortal
{
    public partial class RegisterForm : Form
    {
        private DBConnection dbConnection;

        public RegisterForm()
        {
            InitializeComponent();
            dbConnection = new DBConnection();
        }

        private void register_Click(object sender, EventArgs e)
        {
            // Retrieve input from text boxes
            string username = txtboxUsername.Text.Trim();
            string password = textBoxPassword.Text.Trim();
            string lname = textBoxLname.Text.Trim();
            string fname = textBoxFname.Text.Trim();
            string mname = textBoxMidName.Text.Trim();
            string contact = textBoxContact.Text.Trim();
            string email = textBoxEmail.Text.Trim();

            // Validate inputs
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(lname) || string.IsNullOrEmpty(fname) ||
                string.IsNullOrEmpty(contact) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Please fill in all the fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SQLiteConnection cn = dbConnection.GetConnection)
                {
                    // Check if the username already exists
                    using (SQLiteCommand checkCmd = new SQLiteCommand("SELECT COUNT(*) FROM tbluser WHERE username = @username", cn))
                    {
                        checkCmd.Parameters.AddWithValue("@username", username);
                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (count > 0)
                        {
                            MessageBox.Show("Username already exists. Please choose a different username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Insert new user into tbluser
                    using (SQLiteCommand cmd = new SQLiteCommand("INSERT INTO tbluser (username, password, lname, fname, mname, contact, email) VALUES (@username, @password, @lname, @fname, @mname, @contact, @email)", cn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);
                        cmd.Parameters.AddWithValue("@lname", lname);
                        cmd.Parameters.AddWithValue("@fname", fname);
                        cmd.Parameters.AddWithValue("@mname", mname);
                        cmd.Parameters.AddWithValue("@contact", contact);
                        cmd.Parameters.AddWithValue("@email", email);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("User registered successfully! Redirecting to login.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Navigate to the LoginForm
                        LoginForm loginForm = new LoginForm();
                        loginForm.Show();
                        this.Hide();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void login_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            this.Hide();
        }
    }
}
