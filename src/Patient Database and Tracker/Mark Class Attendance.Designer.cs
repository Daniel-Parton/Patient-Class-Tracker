namespace Patient_Database_and_Tracker
{
    partial class Mark_Class_Attendance
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mark_Class_Attendance));
            this.Dropdown_1 = new System.Windows.Forms.ComboBox();
            this.PatientsClassDGV = new System.Windows.Forms.DataGridView();
            this.Back = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ClassDetailsDGV = new System.Windows.Forms.DataGridView();
            this.UserLabel = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.PatientsClassDGV)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ClassDetailsDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // Dropdown_1
            // 
            this.Dropdown_1.BackColor = System.Drawing.SystemColors.Window;
            this.Dropdown_1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Dropdown_1.FormattingEnabled = true;
            this.Dropdown_1.IntegralHeight = false;
            this.Dropdown_1.Location = new System.Drawing.Point(6, 30);
            this.Dropdown_1.Name = "Dropdown_1";
            this.Dropdown_1.Size = new System.Drawing.Size(358, 21);
            this.Dropdown_1.TabIndex = 1;
            this.toolTip1.SetToolTip(this.Dropdown_1, "Choose class from class list");
            this.Dropdown_1.DropDown += new System.EventHandler(this.Dropdown_1_DropDown);
            this.Dropdown_1.SelectionChangeCommitted += new System.EventHandler(this.Dropdown_1_SelectionChangeCommitted);
            // 
            // PatientsClassDGV
            // 
            this.PatientsClassDGV.AllowUserToAddRows = false;
            this.PatientsClassDGV.AllowUserToDeleteRows = false;
            this.PatientsClassDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.PatientsClassDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PatientsClassDGV.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.PatientsClassDGV.Location = new System.Drawing.Point(6, 68);
            this.PatientsClassDGV.MultiSelect = false;
            this.PatientsClassDGV.Name = "PatientsClassDGV";
            this.PatientsClassDGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.PatientsClassDGV.Size = new System.Drawing.Size(911, 283);
            this.PatientsClassDGV.TabIndex = 3;
            this.PatientsClassDGV.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.PatientsClassDGV_CellBeginEdit);
            this.PatientsClassDGV.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.PatientsClassDGV_CellEndEdit);
            this.PatientsClassDGV.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.PatientsClassDGV_CellValidating);
            this.PatientsClassDGV.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.PatientsClassDGV_DataBindingComplete);
            // 
            // Back
            // 
            this.Back.Image = ((System.Drawing.Image)(resources.GetObject("Back.Image")));
            this.Back.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Back.Location = new System.Drawing.Point(868, 375);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(73, 25);
            this.Back.TabIndex = 4;
            this.Back.Text = "Back";
            this.Back.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.Back, "Back to previous screen");
            this.Back.UseVisualStyleBackColor = true;
            this.Back.Click += new System.EventHandler(this.Back_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ClassDetailsDGV);
            this.groupBox1.Controls.Add(this.PatientsClassDGV);
            this.groupBox1.Controls.Add(this.Dropdown_1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(929, 357);
            this.groupBox1.TabIndex = 133;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Choose Class";
            // 
            // ClassDetailsDGV
            // 
            this.ClassDetailsDGV.AllowUserToAddRows = false;
            this.ClassDetailsDGV.AllowUserToDeleteRows = false;
            this.ClassDetailsDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ClassDetailsDGV.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.ClassDetailsDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ClassDetailsDGV.Location = new System.Drawing.Point(370, 19);
            this.ClassDetailsDGV.Name = "ClassDetailsDGV";
            this.ClassDetailsDGV.ReadOnly = true;
            this.ClassDetailsDGV.Size = new System.Drawing.Size(547, 43);
            this.ClassDetailsDGV.TabIndex = 2;
            this.ClassDetailsDGV.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.ClassDetailsDGV_DataBindingComplete);
            // 
            // UserLabel
            // 
            this.UserLabel.AutoSize = true;
            this.UserLabel.Location = new System.Drawing.Point(9, 381);
            this.UserLabel.Name = "UserLabel";
            this.UserLabel.Size = new System.Drawing.Size(69, 13);
            this.UserLabel.TabIndex = 135;
            this.UserLabel.Text = "Current User:";
            // 
            // Mark_Class_Attendance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 404);
            this.Controls.Add(this.UserLabel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Back);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Mark_Class_Attendance";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mark Class Attendance";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Mark_Class_Attendance_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.PatientsClassDGV)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ClassDetailsDGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox Dropdown_1;
        private System.Windows.Forms.DataGridView PatientsClassDGV;
        private System.Windows.Forms.Button Back;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label UserLabel;
        private System.Windows.Forms.DataGridView ClassDetailsDGV;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}