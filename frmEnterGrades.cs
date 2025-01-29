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
    public partial class frmEnterGrades : Form
    {
        private DBConnection dbConnection;

        // New constructor with parameters to accept student details
        public frmEnterGrades(string lrn, string studentName, string section, string subject)
        {
            InitializeComponent();
            dbConnection = new DBConnection();

            // Populate the textboxes with the values passed to the form
            textBoxStudetname.Text = studentName;
            textBoxSection.Text = section;
            textBoxSubject.Text = subject;

            // If you need to save the LRN for later use (for example when saving grades), you can store it in a private field or directly use it
            //textBoxLrn.Text = lrn;  // Assuming you want to display LRN in a textbox (create it if not already done)
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }

}
