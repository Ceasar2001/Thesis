namespace TeacherPortal
{
    partial class frmGrading
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControlSubjects = new System.Windows.Forms.TabControl();
            this.Grading = new System.Windows.Forms.TabPage();
            this.dataGridStudentWithGrades = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboSubject = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.comboSection = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnshowstudent = new System.Windows.Forms.Button();
            this.close = new System.Windows.Forms.PictureBox();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Lrn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.studentname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.section = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quarter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WrittenWork = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PerformanceTask = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QuaterlyAsses = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QuarterlyGrade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEdit = new System.Windows.Forms.DataGridViewImageColumn();
            this.tabControlSubjects.SuspendLayout();
            this.Grading.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridStudentWithGrades)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.close)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControlSubjects
            // 
            this.tabControlSubjects.Controls.Add(this.Grading);
            this.tabControlSubjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlSubjects.ItemSize = new System.Drawing.Size(60, 25);
            this.tabControlSubjects.Location = new System.Drawing.Point(0, 0);
            this.tabControlSubjects.Name = "tabControlSubjects";
            this.tabControlSubjects.SelectedIndex = 0;
            this.tabControlSubjects.Size = new System.Drawing.Size(1113, 674);
            this.tabControlSubjects.TabIndex = 3;
            // 
            // Grading
            // 
            this.Grading.Controls.Add(this.dataGridStudentWithGrades);
            this.Grading.Controls.Add(this.panel1);
            this.Grading.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Grading.Location = new System.Drawing.Point(4, 29);
            this.Grading.Name = "Grading";
            this.Grading.Padding = new System.Windows.Forms.Padding(3);
            this.Grading.Size = new System.Drawing.Size(1105, 641);
            this.Grading.TabIndex = 0;
            this.Grading.Text = "Grading";
            this.Grading.UseVisualStyleBackColor = true;
            // 
            // dataGridStudentWithGrades
            // 
            this.dataGridStudentWithGrades.AllowUserToAddRows = false;
            this.dataGridStudentWithGrades.BackgroundColor = System.Drawing.Color.Snow;
            this.dataGridStudentWithGrades.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridStudentWithGrades.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridStudentWithGrades.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridStudentWithGrades.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridStudentWithGrades.ColumnHeadersHeight = 30;
            this.dataGridStudentWithGrades.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Lrn,
            this.studentname,
            this.section,
            this.subject,
            this.Quarter,
            this.WrittenWork,
            this.PerformanceTask,
            this.QuaterlyAsses,
            this.QuarterlyGrade,
            this.colEdit});
            this.dataGridStudentWithGrades.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridStudentWithGrades.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridStudentWithGrades.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridStudentWithGrades.EnableHeadersVisualStyles = false;
            this.dataGridStudentWithGrades.GridColor = System.Drawing.SystemColors.Control;
            this.dataGridStudentWithGrades.Location = new System.Drawing.Point(3, 47);
            this.dataGridStudentWithGrades.Name = "dataGridStudentWithGrades";
            this.dataGridStudentWithGrades.RowHeadersVisible = false;
            this.dataGridStudentWithGrades.RowTemplate.Height = 27;
            this.dataGridStudentWithGrades.Size = new System.Drawing.Size(1099, 591);
            this.dataGridStudentWithGrades.TabIndex = 1;
            this.dataGridStudentWithGrades.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridStudentWithGrades_CellContentClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboSubject);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.comboSection);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.btnshowstudent);
            this.panel1.Controls.Add(this.close);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1099, 44);
            this.panel1.TabIndex = 0;
            // 
            // comboSubject
            // 
            this.comboSubject.FormattingEnabled = true;
            this.comboSubject.Location = new System.Drawing.Point(566, 9);
            this.comboSubject.Name = "comboSubject";
            this.comboSubject.Size = new System.Drawing.Size(141, 27);
            this.comboSubject.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(507, 13);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 19);
            this.label9.TabIndex = 5;
            this.label9.Text = "Subject";
            // 
            // comboSection
            // 
            this.comboSection.FormattingEnabled = true;
            this.comboSection.Location = new System.Drawing.Point(345, 8);
            this.comboSection.Name = "comboSection";
            this.comboSection.Size = new System.Drawing.Size(141, 27);
            this.comboSection.TabIndex = 6;
            this.comboSection.SelectedIndexChanged += new System.EventHandler(this.comboSection_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(286, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 19);
            this.label8.TabIndex = 5;
            this.label8.Text = "Section";
            // 
            // btnshowstudent
            // 
            this.btnshowstudent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.btnshowstudent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnshowstudent.FlatAppearance.BorderSize = 0;
            this.btnshowstudent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnshowstudent.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnshowstudent.ForeColor = System.Drawing.Color.Snow;
            this.btnshowstudent.Location = new System.Drawing.Point(796, 3);
            this.btnshowstudent.Name = "btnshowstudent";
            this.btnshowstudent.Size = new System.Drawing.Size(132, 35);
            this.btnshowstudent.TabIndex = 1;
            this.btnshowstudent.Text = "Show Student";
            this.btnshowstudent.UseVisualStyleBackColor = false;
            this.btnshowstudent.Click += new System.EventHandler(this.btnshowstudent_Click);
            // 
            // close
            // 
            this.close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.close.Image = global::TeacherPortal.Properties.Resources.square;
            this.close.Location = new System.Drawing.Point(1062, 5);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(32, 30);
            this.close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.close.TabIndex = 0;
            this.close.TabStop = false;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewImageColumn1.HeaderText = "";
            this.dataGridViewImageColumn1.Image = global::TeacherPortal.Properties.Resources.editing;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.ToolTipText = "Close Academic Year";
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewImageColumn2.HeaderText = "";
            this.dataGridViewImageColumn2.Image = global::TeacherPortal.Properties.Resources.delete1;
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            // 
            // Lrn
            // 
            this.Lrn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Lrn.HeaderText = "Lrn";
            this.Lrn.Name = "Lrn";
            this.Lrn.Width = 52;
            // 
            // studentname
            // 
            this.studentname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.studentname.HeaderText = "Student Name";
            this.studentname.Name = "studentname";
            // 
            // section
            // 
            this.section.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.section.HeaderText = "Section";
            this.section.Name = "section";
            this.section.Visible = false;
            // 
            // subject
            // 
            this.subject.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.subject.HeaderText = "Subject";
            this.subject.Name = "subject";
            this.subject.Visible = false;
            // 
            // Quarter
            // 
            this.Quarter.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Quarter.HeaderText = "Quarter";
            this.Quarter.Name = "Quarter";
            // 
            // WrittenWork
            // 
            this.WrittenWork.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.WrittenWork.HeaderText = "Written Works";
            this.WrittenWork.Name = "WrittenWork";
            this.WrittenWork.Width = 126;
            // 
            // PerformanceTask
            // 
            this.PerformanceTask.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.PerformanceTask.HeaderText = "Performance Task";
            this.PerformanceTask.Name = "PerformanceTask";
            this.PerformanceTask.Width = 149;
            // 
            // QuaterlyAsses
            // 
            this.QuaterlyAsses.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.QuaterlyAsses.HeaderText = "Quarterly Assessment";
            this.QuaterlyAsses.Name = "QuaterlyAsses";
            this.QuaterlyAsses.Width = 173;
            // 
            // QuarterlyGrade
            // 
            this.QuarterlyGrade.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.QuarterlyGrade.HeaderText = "Quarterly Grade";
            this.QuarterlyGrade.Name = "QuarterlyGrade";
            this.QuarterlyGrade.Width = 137;
            // 
            // colEdit
            // 
            this.colEdit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colEdit.HeaderText = "";
            this.colEdit.Image = global::TeacherPortal.Properties.Resources.editing;
            this.colEdit.Name = "colEdit";
            this.colEdit.ToolTipText = "Close Academic Year";
            this.colEdit.Width = 5;
            // 
            // frmGrading
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 674);
            this.ControlBox = false;
            this.Controls.Add(this.tabControlSubjects);
            this.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmGrading";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tabControlSubjects.ResumeLayout(false);
            this.Grading.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridStudentWithGrades)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.close)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlSubjects;
        private System.Windows.Forms.TabPage Grading;
        private System.Windows.Forms.DataGridView dataGridStudentWithGrades;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox close;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboSubject;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboSection;
        private System.Windows.Forms.Button btnshowstudent;
        private System.Windows.Forms.DataGridViewTextBoxColumn Lrn;
        private System.Windows.Forms.DataGridViewTextBoxColumn studentname;
        private System.Windows.Forms.DataGridViewTextBoxColumn section;
        private System.Windows.Forms.DataGridViewTextBoxColumn subject;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quarter;
        private System.Windows.Forms.DataGridViewTextBoxColumn WrittenWork;
        private System.Windows.Forms.DataGridViewTextBoxColumn PerformanceTask;
        private System.Windows.Forms.DataGridViewTextBoxColumn QuaterlyAsses;
        private System.Windows.Forms.DataGridViewTextBoxColumn QuarterlyGrade;
        private System.Windows.Forms.DataGridViewImageColumn colEdit;
    }
}