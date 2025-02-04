namespace TeacherPortal
{
    partial class frmAttendance
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridViewAttendanceList = new System.Windows.Forms.DataGridView();
            this.colLrn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Monday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Tuesday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Wednesday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Thursday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Friday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.saveAttendance = new System.Windows.Forms.Button();
            this.ChooseSection = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CurrentDate = new System.Windows.Forms.DateTimePicker();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridViewAttendanceSummary = new System.Windows.Forms.DataGridView();
            this.slrn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PresentCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AbsentCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.month = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.comboBoxSectionss = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBoxSelect = new System.Windows.Forms.PictureBox();
            this.chooseDate = new System.Windows.Forms.PictureBox();
            this.close = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAttendanceList)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAttendanceSummary)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chooseDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1067, 658);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridViewAttendanceList);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1059, 626);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Attendance";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridViewAttendanceList
            // 
            this.dataGridViewAttendanceList.AllowUserToAddRows = false;
            this.dataGridViewAttendanceList.BackgroundColor = System.Drawing.Color.Snow;
            this.dataGridViewAttendanceList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewAttendanceList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridViewAttendanceList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle21.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle21.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle21.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle21.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle21.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewAttendanceList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle21;
            this.dataGridViewAttendanceList.ColumnHeadersHeight = 30;
            this.dataGridViewAttendanceList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colLrn,
            this.colName,
            this.Monday,
            this.Tuesday,
            this.Wednesday,
            this.Thursday,
            this.Friday});
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle22.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle22.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle22.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            dataGridViewCellStyle22.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle22.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewAttendanceList.DefaultCellStyle = dataGridViewCellStyle22;
            this.dataGridViewAttendanceList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewAttendanceList.EnableHeadersVisualStyles = false;
            this.dataGridViewAttendanceList.GridColor = System.Drawing.SystemColors.Control;
            this.dataGridViewAttendanceList.Location = new System.Drawing.Point(3, 44);
            this.dataGridViewAttendanceList.Name = "dataGridViewAttendanceList";
            this.dataGridViewAttendanceList.RowHeadersVisible = false;
            this.dataGridViewAttendanceList.RowTemplate.Height = 27;
            this.dataGridViewAttendanceList.Size = new System.Drawing.Size(1053, 579);
            this.dataGridViewAttendanceList.TabIndex = 2;
            // 
            // colLrn
            // 
            this.colLrn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colLrn.HeaderText = "Lrn";
            this.colLrn.Name = "colLrn";
            this.colLrn.ReadOnly = true;
            // 
            // colName
            // 
            this.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colName.HeaderText = "Student Name";
            this.colName.Name = "colName";
            // 
            // Monday
            // 
            this.Monday.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Monday.HeaderText = "Monday";
            this.Monday.Name = "Monday";
            this.Monday.ReadOnly = true;
            this.Monday.Width = 67;
            // 
            // Tuesday
            // 
            this.Tuesday.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Tuesday.HeaderText = "Tuesday";
            this.Tuesday.Name = "Tuesday";
            this.Tuesday.ReadOnly = true;
            this.Tuesday.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Tuesday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Tuesday.Width = 86;
            // 
            // Wednesday
            // 
            this.Wednesday.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Wednesday.HeaderText = "Wednesday";
            this.Wednesday.Name = "Wednesday";
            this.Wednesday.ReadOnly = true;
            this.Wednesday.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Wednesday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Wednesday.Width = 109;
            // 
            // Thursday
            // 
            this.Thursday.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Thursday.HeaderText = "Thursday";
            this.Thursday.Name = "Thursday";
            this.Thursday.ReadOnly = true;
            this.Thursday.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Thursday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Thursday.Width = 91;
            // 
            // Friday
            // 
            this.Friday.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Friday.HeaderText = "Friday";
            this.Friday.Name = "Friday";
            this.Friday.ReadOnly = true;
            this.Friday.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Friday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Friday.Width = 72;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chooseDate);
            this.panel1.Controls.Add(this.saveAttendance);
            this.panel1.Controls.Add(this.ChooseSection);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.CurrentDate);
            this.panel1.Controls.Add(this.close);
            this.panel1.Controls.Add(this.pictureBoxSelect);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1053, 41);
            this.panel1.TabIndex = 0;
            // 
            // saveAttendance
            // 
            this.saveAttendance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.saveAttendance.Cursor = System.Windows.Forms.Cursors.Hand;
            this.saveAttendance.FlatAppearance.BorderSize = 0;
            this.saveAttendance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveAttendance.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveAttendance.ForeColor = System.Drawing.Color.Snow;
            this.saveAttendance.Location = new System.Drawing.Point(6, 3);
            this.saveAttendance.Name = "saveAttendance";
            this.saveAttendance.Size = new System.Drawing.Size(144, 35);
            this.saveAttendance.TabIndex = 3;
            this.saveAttendance.Text = "Save Attendance";
            this.saveAttendance.UseVisualStyleBackColor = false;
            this.saveAttendance.Click += new System.EventHandler(this.saveAttendance_Click);
            // 
            // ChooseSection
            // 
            this.ChooseSection.FormattingEnabled = true;
            this.ChooseSection.Location = new System.Drawing.Point(700, 8);
            this.ChooseSection.Name = "ChooseSection";
            this.ChooseSection.Size = new System.Drawing.Size(168, 27);
            this.ChooseSection.TabIndex = 5;
            this.ChooseSection.SelectedIndexChanged += new System.EventHandler(this.ChooseSection_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(592, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 19);
            this.label2.TabIndex = 3;
            this.label2.Text = "Select Section";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(185, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 19);
            this.label1.TabIndex = 3;
            this.label1.Text = "Date Today";
            // 
            // CurrentDate
            // 
            this.CurrentDate.Location = new System.Drawing.Point(277, 8);
            this.CurrentDate.Name = "CurrentDate";
            this.CurrentDate.Size = new System.Drawing.Size(238, 27);
            this.CurrentDate.TabIndex = 2;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridViewAttendanceSummary);
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 28);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(1059, 626);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Summary";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridViewAttendanceSummary
            // 
            this.dataGridViewAttendanceSummary.AllowUserToAddRows = false;
            this.dataGridViewAttendanceSummary.BackgroundColor = System.Drawing.Color.Snow;
            this.dataGridViewAttendanceSummary.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewAttendanceSummary.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridViewAttendanceSummary.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle23.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle23.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle23.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle23.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle23.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewAttendanceSummary.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle23;
            this.dataGridViewAttendanceSummary.ColumnHeadersHeight = 30;
            this.dataGridViewAttendanceSummary.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.slrn,
            this.sname,
            this.PresentCount,
            this.AbsentCount,
            this.month});
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle24.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle24.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle24.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            dataGridViewCellStyle24.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle24.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle24.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewAttendanceSummary.DefaultCellStyle = dataGridViewCellStyle24;
            this.dataGridViewAttendanceSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewAttendanceSummary.EnableHeadersVisualStyles = false;
            this.dataGridViewAttendanceSummary.GridColor = System.Drawing.SystemColors.Control;
            this.dataGridViewAttendanceSummary.Location = new System.Drawing.Point(0, 41);
            this.dataGridViewAttendanceSummary.Name = "dataGridViewAttendanceSummary";
            this.dataGridViewAttendanceSummary.RowHeadersVisible = false;
            this.dataGridViewAttendanceSummary.RowTemplate.Height = 27;
            this.dataGridViewAttendanceSummary.Size = new System.Drawing.Size(1059, 591);
            this.dataGridViewAttendanceSummary.TabIndex = 3;
            // 
            // slrn
            // 
            this.slrn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.slrn.HeaderText = "Lrn";
            this.slrn.Name = "slrn";
            this.slrn.ReadOnly = true;
            // 
            // sname
            // 
            this.sname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.sname.HeaderText = "Student Name";
            this.sname.Name = "sname";
            // 
            // PresentCount
            // 
            this.PresentCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PresentCount.HeaderText = "Present";
            this.PresentCount.Name = "PresentCount";
            // 
            // AbsentCount
            // 
            this.AbsentCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.AbsentCount.HeaderText = "Absents";
            this.AbsentCount.Name = "AbsentCount";
            // 
            // month
            // 
            this.month.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.month.HeaderText = "Month";
            this.month.Name = "month";
            this.month.Width = 75;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.comboBoxSectionss);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1059, 41);
            this.panel2.TabIndex = 0;
            // 
            // comboBoxSectionss
            // 
            this.comboBoxSectionss.FormattingEnabled = true;
            this.comboBoxSectionss.Location = new System.Drawing.Point(786, 8);
            this.comboBoxSectionss.Name = "comboBoxSectionss";
            this.comboBoxSectionss.Size = new System.Drawing.Size(168, 27);
            this.comboBoxSectionss.TabIndex = 8;
            this.comboBoxSectionss.SelectedIndexChanged += new System.EventHandler(this.comboBoxSectionss_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(678, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 19);
            this.label3.TabIndex = 7;
            this.label3.Text = "Select Section";
            // 
            // pictureBoxSelect
            // 
            this.pictureBoxSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxSelect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxSelect.Image = global::TeacherPortal.Properties.Resources.select;
            this.pictureBoxSelect.Location = new System.Drawing.Point(521, 6);
            this.pictureBoxSelect.Name = "pictureBoxSelect";
            this.pictureBoxSelect.Size = new System.Drawing.Size(32, 30);
            this.pictureBoxSelect.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxSelect.TabIndex = 6;
            this.pictureBoxSelect.TabStop = false;
            this.pictureBoxSelect.Visible = false;
            this.pictureBoxSelect.Click += new System.EventHandler(this.pictureBoxSelect_Click_1);
            // 
            // chooseDate
            // 
            this.chooseDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chooseDate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chooseDate.Image = global::TeacherPortal.Properties.Resources.unselect;
            this.chooseDate.Location = new System.Drawing.Point(521, 6);
            this.chooseDate.Name = "chooseDate";
            this.chooseDate.Size = new System.Drawing.Size(32, 30);
            this.chooseDate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.chooseDate.TabIndex = 1;
            this.chooseDate.TabStop = false;
            this.chooseDate.Click += new System.EventHandler(this.chooseDate_Click);
            // 
            // close
            // 
            this.close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.close.Image = global::TeacherPortal.Properties.Resources.square;
            this.close.Location = new System.Drawing.Point(1016, 4);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(32, 30);
            this.close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.close.TabIndex = 1;
            this.close.TabStop = false;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::TeacherPortal.Properties.Resources.square;
            this.pictureBox1.Location = new System.Drawing.Point(1021, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 30);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewImageColumn1.HeaderText = "";
            this.dataGridViewImageColumn1.Image = global::TeacherPortal.Properties.Resources.check_box3;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.ToolTipText = "Close Academic Year";
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewImageColumn2.HeaderText = "Late";
            this.dataGridViewImageColumn2.Image = global::TeacherPortal.Properties.Resources.late;
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            // 
            // frmAttendance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 658);
            this.ControlBox = false;
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmAttendance";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAttendanceList)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAttendanceSummary)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chooseDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox close;
        private System.Windows.Forms.DataGridView dataGridViewAttendanceList;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker CurrentDate;
        private System.Windows.Forms.ComboBox ChooseSection;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button saveAttendance;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLrn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Monday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Tuesday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Wednesday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Thursday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Friday;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataGridViewAttendanceSummary;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox comboBoxSectionss;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn slrn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sname;
        private System.Windows.Forms.DataGridViewTextBoxColumn PresentCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn AbsentCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn month;
        private System.Windows.Forms.PictureBox chooseDate;
        private System.Windows.Forms.PictureBox pictureBoxSelect;
    }
}