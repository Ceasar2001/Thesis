﻿using System;
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
    public partial class frmProfile : Form
    {
        private DBConnection dbConnection;
        public frmProfile()
        {
            InitializeComponent();
            dbConnection = new DBConnection();
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
