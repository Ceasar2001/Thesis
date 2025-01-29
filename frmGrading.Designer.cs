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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControlSubjects = new System.Windows.Forms.TabControl();
            this.Grading = new System.Windows.Forms.TabPage();
            this.dataGridStudentWithGrades = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEdit = new System.Windows.Forms.DataGridViewImageColumn();
            this.colDelete = new System.Windows.Forms.DataGridViewImageColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboSubject = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.comboSection = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnshowstudent = new System.Windows.Forms.Button();
            this.btnGrade = new System.Windows.Forms.Button();
            this.close = new System.Windows.Forms.PictureBox();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
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
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridStudentWithGrades.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridStudentWithGrades.ColumnHeadersHeight = 30;
            this.dataGridStudentWithGrades.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column9,
            this.Column8,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column11,
            this.Column7,
            this.Column10,
            this.colEdit,
            this.colDelete});
            this.dataGridStudentWithGrades.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridStudentWithGrades.DefaultCellStyle = dataGridViewCellStyle6;
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
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column1.HeaderText = "Lrn";
            this.Column1.Name = "Column1";
            this.Column1.Width = 52;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column2.HeaderText = "Student Name";
            this.Column2.Name = "Column2";
            this.Column2.Width = 127;
            // 
            // Column9
            // 
            this.Column9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column9.HeaderText = "Section";
            this.Column9.Name = "Column9";
            this.Column9.Visible = false;
            this.Column9.Width = 83;
            // 
            // Column8
            // 
            this.Column8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column8.HeaderText = "Subject";
            this.Column8.Name = "Column8";
            this.Column8.Visible = false;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column3.HeaderText = "Written Works";
            this.Column3.Name = "Column3";
            this.Column3.Width = 126;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column4.HeaderText = "Performance Task";
            this.Column4.Name = "Column4";
            this.Column4.Width = 149;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column5.HeaderText = "Quarterly Assessment";
            this.Column5.Name = "Column5";
            this.Column5.Width = 173;
            // 
            // Column6
            // 
            this.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column6.HeaderText = "Initial Grade";
            this.Column6.Name = "Column6";
            this.Column6.Width = 113;
            // 
            // Column11
            // 
            this.Column11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column11.HeaderText = "Quarter";
            this.Column11.Name = "Column11";
            this.Column11.Width = 82;
            // 
            // Column7
            // 
            this.Column7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column7.HeaderText = "Quarterly Grade";
            this.Column7.Name = "Column7";
            this.Column7.Width = 137;
            // 
            // Column10
            // 
            this.Column10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column10.HeaderText = "Status";
            this.Column10.Name = "Column10";
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
            // colDelete
            // 
            this.colDelete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colDelete.HeaderText = "";
            this.colDelete.Image = global::TeacherPortal.Properties.Resources.delete1;
            this.colDelete.Name = "colDelete";
            this.colDelete.Width = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboSubject);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.comboSection);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnshowstudent);
            this.panel1.Controls.Add(this.btnGrade);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(149, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 19);
            this.label1.TabIndex = 5;
            this.label1.Text = "subject";
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
            // btnGrade
            // 
            this.btnGrade.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.btnGrade.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGrade.FlatAppearance.BorderSize = 0;
            this.btnGrade.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGrade.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGrade.ForeColor = System.Drawing.Color.Snow;
            this.btnGrade.Location = new System.Drawing.Point(5, 3);
            this.btnGrade.Name = "btnGrade";
            this.btnGrade.Size = new System.Drawing.Size(123, 35);
            this.btnGrade.TabIndex = 1;
            this.btnGrade.Text = "Start Grading";
            this.btnGrade.UseVisualStyleBackColor = false;
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
        private System.Windows.Forms.Button btnGrade;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboSubject;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboSection;
        private System.Windows.Forms.Button btnshowstudent;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewImageColumn colEdit;
        private System.Windows.Forms.DataGridViewImageColumn colDelete;
    }
}