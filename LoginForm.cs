using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace TeacherPortal
{
    public partial class LoginForm : Form
    {
        private DBConnection dbConnection;

        public LoginForm()
        {
            InitializeComponent();
            dbConnection = new DBConnection();

            // Initially hide warning labels and picture boxes
            labelWarning.Visible = false;
            pictureBoxWarning.Visible = false;
            pictureBoxShow.Visible = false; // Initially show "hide" icon
        }

        private void Register_Click(object sender, EventArgs e)
        {
            RegisterForm register = new RegisterForm();
            register.Show();
            this.Hide();
        }

        private void login_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text.Trim();
            string password = textBoxPassword.Text.Trim();

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                string result = IsValidNamePass(username, password);

                if (!string.IsNullOrEmpty(result))
                {
                    // Hide the login form
                    this.Hide();

                    // Show the MainForm
                    MainForm mainform = new MainForm();
                    textBoxUsername.Clear();
                    textBoxPassword.Clear();
                    pictureBoxHide_Click(sender, e);
                    textBoxUsername.Focus();
                    pictureBoxWarning.Hide();
                    labelWarning.Hide();

                    // Show MainForm as a dialog
                    mainform.ShowDialog();

                    // Optionally, dispose of the login form after it is hidden and main form is shown
                    this.Close();
                }
                else
                {
                    pictureBoxWarning.Show();
                    labelWarning.Show();
                }
            }
            else
            {
                MessageBox.Show("Please enter both username and password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private string IsValidNamePass(string username, string password)
        {
            try
            {
                // Use 'using' statement to ensure the connection is properly disposed of
                using (SQLiteConnection conn = dbConnection.GetConnection)
                {
                    conn.Open();  // Explicitly open the connection

                    string query = "SELECT username FROM tbluser WHERE username = @username AND password = @password";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        // Execute the query and get the result
                        object result = cmd.ExecuteScalar();
                        return result?.ToString(); // Return the username if valid, otherwise null
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while validating credentials: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }




        private void pictureBoxShow_Click(object sender, EventArgs e)
        {
            textBoxPassword.UseSystemPasswordChar = false;
            pictureBoxShow.Visible = false;
            pictureBoxHide.Visible = true;
        }

        private void pictureBoxHide_Click(object sender, EventArgs e)
        {
            textBoxPassword.UseSystemPasswordChar = true;
            pictureBoxShow.Visible = true;
            pictureBoxHide.Visible = false;
        }

        private void labelForgetPassword_Click(object sender, EventArgs e)
        {
            ForgetPassword forgetPasswordForm = new ForgetPassword();
            forgetPasswordForm.ShowDialog();
        }

        private void Register_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Go to Register", Register);
        }

        private void labelForgetPassword_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Get Username and Password", labelForgetPassword);
        }

        private void textBoxUsername_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void textBoxUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SelectNextControl(ActiveControl, true, true, true, true);
                e.Handled = true;
            }
        }

        private void textBoxPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void textBoxPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                login.PerformClick();
                e.Handled = true;
            }
        }

        private void labelForgetPassword_Click_1(object sender, EventArgs e)
        {
            ForgetPassword forgetPassword = new ForgetPassword();
            forgetPassword.ShowDialog();
        }

        private void close_Click(object sender, EventArgs e)
        {
          
            this.Dispose();
        }
    }
}
