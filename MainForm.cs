using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeacherPortal
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnStudent_Click(object sender, EventArgs e)
        {
            frmStudentList std = new frmStudentList();
            std.TopLevel = false;
            mainPanel.Controls.Add(std);
            std.BringToFront();
            std.Show();
        }

        private void btnAY_Click(object sender, EventArgs e)
        {
            frmAYList ay = new frmAYList();
            ay.TopLevel = false;
            mainPanel.Controls.Add(ay);
            ay.BringToFront();
            ay.loadRecords();
            ay.Show();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBoxMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBoxClose_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Close Program", pictureBoxClose);
        }

        private void pictureBoxMinimize_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Minimize Program", pictureBoxMinimize);
        }

        private void btnSection_Click(object sender, EventArgs e)
        {
            frmSectionList frmSectionList = new frmSectionList();
            frmSectionList.TopLevel = false;
            mainPanel.Controls.Add(frmSectionList);
            frmSectionList.BringToFront();
            frmSectionList.loadRecords();
            frmSectionList.Show();
        }

        private void btnEnrollment_Click(object sender, EventArgs e)
        {
            frmEnrollmentList EnollmentList = new frmEnrollmentList();
            EnollmentList.TopLevel = false;
            mainPanel.Controls.Add(EnollmentList);
            EnollmentList.BringToFront();
            EnollmentList.getAY();
            EnollmentList.loadRecord();
            EnollmentList.Show();
        }

        private void btnAttendance_Click(object sender, EventArgs e)
        {
            frmAttendance attendance = new frmAttendance();
            attendance.TopLevel = false;
            mainPanel.Controls.Add(attendance);
            attendance.BringToFront();
            attendance.Show();
        }

        private void btnGrades_Click(object sender, EventArgs e)
        {
            frmGrading grade = new frmGrading();
            grade.TopLevel = false;
            mainPanel.Controls.Add(grade);
            grade.BringToFront();
            grade.Show();
        }

        private void profilebtn_Click(object sender, EventArgs e)
        {
            frmProfile profile = new frmProfile();
            profile.TopLevel = false;
            panel3.Controls.Add(profile);
            profile.BringToFront();
            profile.Show();
        }
    }
}
