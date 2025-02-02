using System;
using System.Data.SQLite;
using System.Drawing;
using System.Net.Mail;
using System.Windows.Forms;

namespace TeacherPortal
{
    public partial class ForgetPassword : Form
    {
        private DBConnection dbConnection;

        public ForgetPassword()
        {
            InitializeComponent();
            dbConnection = new DBConnection();
        }

        // Validates if the email format is correct
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // Close button click event
        private void close_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Tooltip on mouse hover over close button
        private void close_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Close", close);
        }

        // TextBox enter event for email input
        private void textBoxEmail_Enter(object sender, EventArgs e)
        {
            if (textBoxEmail.Text.Trim() == "Email")
            {
                textBoxEmail.Clear();
                textBoxEmail.ForeColor = Color.Black;
            }

            ValidateEmail();
        }

        // TextBox leave event for email input
        private void textBoxEmail_Leave(object sender, EventArgs e)
        {
            if (textBoxEmail.Text.Trim() == string.Empty)
            {
                textBoxEmail.Text = "Email";
                textBoxEmail.ForeColor = Color.Gray;
            }

            ValidateEmail();
        }

        // Validates if the email is in the correct format and shows/hides error message accordingly
        private void ValidateEmail()
        {
            // Only show the error if the email is not empty and is invalid
            if (!string.IsNullOrWhiteSpace(textBoxEmail.Text.Trim()) && !IsValidEmail(textBoxEmail.Text.Trim()))
            {
                // Show the error if the email is invalid
                pictureBoxError.Show();
                labelError.Show();
            }
            else
            {
                // Hide the error if the email is valid or empty
                pictureBoxError.Hide();
                labelError.Hide();
            }
        }



        // Verify button click event to fetch username and password
        // Verify button click event to fetch username and password
        private void Verify_Click(object sender, EventArgs e)
        {
            string email = textBoxEmail.Text.Trim();

            if (IsValidEmail(email))
            {
                try
                {
                    // Open the connection and execute the query
                    using (SQLiteConnection cn = dbConnection.GetConnection)
                    {
                        // Explicitly open the connection
                        cn.Open();

                        string query = "SELECT username, password FROM tbluser WHERE email = @Email";
                        using (SQLiteCommand cmd = new SQLiteCommand(query, cn))
                        {
                            cmd.Parameters.AddWithValue("@Email", email);

                            using (SQLiteDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    string username = reader["username"].ToString();
                                    string password = reader["password"].ToString();

                                    MessageBox.Show($"Your account details:\n\nUsername: {username}\nPassword: {password}",
                                                     "Account Retrieved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("Email address not found. Please try again.",
                                                     "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show($"Invalid operation: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show($"SQLite error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Close the ForgetPassword dialog after clicking OK on the message box
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter a valid email address.", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private void labelError_Click(object sender, EventArgs e)
        {

        }
    }
}
