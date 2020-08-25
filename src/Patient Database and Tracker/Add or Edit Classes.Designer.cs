namespace Patient_Database_and_Tracker
{
    partial class Add_Edit_Class
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
            System.Windows.Forms.Label Notes_Label;
            System.Windows.Forms.Label Label;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label Date_Label;
            System.Windows.Forms.Label label4;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Add_Edit_Class));
            this.Back = new System.Windows.Forms.Button();
            this.UserLabel = new System.Windows.Forms.Label();
            this.Remove = new System.Windows.Forms.Button();
            this.PatientsClassDGV = new System.Windows.Forms.DataGridView();
            this.Edit_Start_Time = new System.Windows.Forms.Button();
            this.Add_Patient = new System.Windows.Forms.Button();
            this.Dropdown_2 = new System.Windows.Forms.ComboBox();
            this.Add_Class = new System.Windows.Forms.Button();
            this.Sort_Lists = new System.Windows.Forms.Button();
            this.Input_1 = new System.Windows.Forms.TextBox();
            this.Dropdown_1 = new System.Windows.Forms.ComboBox();
            this.Save = new System.Windows.Forms.Button();
            this.PatientsClassWaitingDGV = new System.Windows.Forms.DataGridView();
            this.Date_1 = new System.Windows.Forms.DateTimePicker();
            this.Add_Patient_Waiting = new System.Windows.Forms.Button();
            this.Remove_Waiting = new System.Windows.Forms.Button();
            this.Move = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Delete_Class = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            Notes_Label = new System.Windows.Forms.Label();
            Label = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            Date_Label = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PatientsClassDGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PatientsClassWaitingDGV)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // Notes_Label
            // 
            Notes_Label.AutoSize = true;
            Notes_Label.Location = new System.Drawing.Point(7, 50);
            Notes_Label.Name = "Notes_Label";
            Notes_Label.Size = new System.Drawing.Size(48, 13);
            Notes_Label.TabIndex = 131;
            Notes_Label.Text = "Patients:";
            // 
            // Label
            // 
            Label.AutoSize = true;
            Label.Location = new System.Drawing.Point(10, 66);
            Label.Name = "Label";
            Label.Size = new System.Drawing.Size(66, 13);
            Label.TabIndex = 124;
            Label.Text = "Class Name:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(7, 139);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(130, 13);
            label2.TabIndex = 136;
            label2.Text = "Patients currently in Class:";
            // 
            // Date_Label
            // 
            Date_Label.AutoSize = true;
            Date_Label.Location = new System.Drawing.Point(7, 73);
            Date_Label.Name = "Date_Label";
            Date_Label.Size = new System.Drawing.Size(65, 13);
            Date_Label.TabIndex = 141;
            Date_Label.Text = "New Patient";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(7, 85);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(58, 13);
            label4.TabIndex = 142;
            label4.Text = "Start Date:";
            // 
            // Back
            // 
            this.Back.Image = ((System.Drawing.Image)(resources.GetObject("Back.Image")));
            this.Back.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Back.Location = new System.Drawing.Point(917, 536);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(73, 25);
            this.Back.TabIndex = 16;
            this.Back.Text = "Back";
            this.Back.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.Back, "Back to previous screen");
            this.Back.UseVisualStyleBackColor = true;
            this.Back.Click += new System.EventHandler(this.Back_Click);
            // 
            // UserLabel
            // 
            this.UserLabel.AutoSize = true;
            this.UserLabel.Location = new System.Drawing.Point(4, 548);
            this.UserLabel.Name = "UserLabel";
            this.UserLabel.Size = new System.Drawing.Size(69, 13);
            this.UserLabel.TabIndex = 135;
            this.UserLabel.Text = "Current User:";
            // 
            // Remove
            // 
            this.Remove.Image = ((System.Drawing.Image)(resources.GetObject("Remove.Image")));
            this.Remove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Remove.Location = new System.Drawing.Point(309, 351);
            this.Remove.Name = "Remove";
            this.Remove.Size = new System.Drawing.Size(162, 25);
            this.Remove.TabIndex = 12;
            this.Remove.TabStop = false;
            this.Remove.Text = "Remove Patient From Class";
            this.Remove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.Remove, "Remove patient from class");
            this.Remove.UseVisualStyleBackColor = true;
            this.Remove.Click += new System.EventHandler(this.Remove_Click);
            // 
            // PatientsClassDGV
            // 
            this.PatientsClassDGV.AllowUserToAddRows = false;
            this.PatientsClassDGV.AllowUserToDeleteRows = false;
            this.PatientsClassDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.PatientsClassDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PatientsClassDGV.Location = new System.Drawing.Point(10, 155);
            this.PatientsClassDGV.MultiSelect = false;
            this.PatientsClassDGV.Name = "PatientsClassDGV";
            this.PatientsClassDGV.ReadOnly = true;
            this.PatientsClassDGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.PatientsClassDGV.Size = new System.Drawing.Size(466, 190);
            this.PatientsClassDGV.TabIndex = 10;
            this.PatientsClassDGV.TabStop = false;
            this.toolTip1.SetToolTip(this.PatientsClassDGV, "Patients currently in class");
            this.PatientsClassDGV.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.PatientsClassDGV_DataBindingComplete);
            this.PatientsClassDGV.SelectionChanged += new System.EventHandler(this.PatientsClassDGV_SelectionChanged);
            // 
            // Edit_Start_Time
            // 
            this.Edit_Start_Time.Image = ((System.Drawing.Image)(resources.GetObject("Edit_Start_Time.Image")));
            this.Edit_Start_Time.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Edit_Start_Time.Location = new System.Drawing.Point(10, 351);
            this.Edit_Start_Time.Name = "Edit_Start_Time";
            this.Edit_Start_Time.Size = new System.Drawing.Size(141, 25);
            this.Edit_Start_Time.TabIndex = 11;
            this.Edit_Start_Time.Text = "Edit Patient Start Date";
            this.Edit_Start_Time.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.Edit_Start_Time, "Edit selected patient start date");
            this.Edit_Start_Time.UseVisualStyleBackColor = true;
            this.Edit_Start_Time.Click += new System.EventHandler(this.Edit_Start_Time_Click);
            // 
            // Add_Patient
            // 
            this.Add_Patient.Enabled = false;
            this.Add_Patient.Image = ((System.Drawing.Image)(resources.GetObject("Add_Patient.Image")));
            this.Add_Patient.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Add_Patient.Location = new System.Drawing.Point(333, 104);
            this.Add_Patient.Name = "Add_Patient";
            this.Add_Patient.Size = new System.Drawing.Size(138, 25);
            this.Add_Patient.TabIndex = 8;
            this.Add_Patient.Text = "Add Patient to Class";
            this.Add_Patient.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.Add_Patient, "Add patient to class");
            this.Add_Patient.UseVisualStyleBackColor = true;
            this.Add_Patient.Click += new System.EventHandler(this.Add_Patient_Click);
            // 
            // Dropdown_2
            // 
            this.Dropdown_2.BackColor = System.Drawing.SystemColors.Window;
            this.Dropdown_2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Dropdown_2.Enabled = false;
            this.Dropdown_2.FormattingEnabled = true;
            this.Dropdown_2.IntegralHeight = false;
            this.Dropdown_2.Items.AddRange(new object[] {
            "Initial",
            "3 Months",
            "6 Months",
            "12 Months"});
            this.Dropdown_2.Location = new System.Drawing.Point(113, 46);
            this.Dropdown_2.Name = "Dropdown_2";
            this.Dropdown_2.Size = new System.Drawing.Size(358, 21);
            this.Dropdown_2.TabIndex = 6;
            this.toolTip1.SetToolTip(this.Dropdown_2, "Choose from patient list");
            this.Dropdown_2.DropDown += new System.EventHandler(this.Dropdown_2_DropDown);
            this.Dropdown_2.SelectionChangeCommitted += new System.EventHandler(this.Dropdown_2_SelectionChangeCommitted);
            // 
            // Add_Class
            // 
            this.Add_Class.Image = ((System.Drawing.Image)(resources.GetObject("Add_Class.Image")));
            this.Add_Class.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Add_Class.Location = new System.Drawing.Point(10, 23);
            this.Add_Class.Name = "Add_Class";
            this.Add_Class.Size = new System.Drawing.Size(95, 25);
            this.Add_Class.TabIndex = 1;
            this.Add_Class.Text = "Add Class";
            this.Add_Class.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.Add_Class, "Add new class");
            this.Add_Class.UseVisualStyleBackColor = true;
            this.Add_Class.Click += new System.EventHandler(this.Add_Class_Click);
            // 
            // Sort_Lists
            // 
            this.Sort_Lists.Location = new System.Drawing.Point(6, 19);
            this.Sort_Lists.Name = "Sort_Lists";
            this.Sort_Lists.Size = new System.Drawing.Size(105, 25);
            this.Sort_Lists.TabIndex = 129;
            this.Sort_Lists.TabStop = false;
            this.Sort_Lists.Text = "Sort List by Name";
            this.toolTip1.SetToolTip(this.Sort_Lists, "Change way lists are sorted");
            this.Sort_Lists.UseVisualStyleBackColor = true;
            this.Sort_Lists.Click += new System.EventHandler(this.Sort_Lists_Click);
            // 
            // Input_1
            // 
            this.Input_1.Location = new System.Drawing.Point(113, 63);
            this.Input_1.Name = "Input_1";
            this.Input_1.ReadOnly = true;
            this.Input_1.Size = new System.Drawing.Size(358, 20);
            this.Input_1.TabIndex = 3;
            this.Input_1.TabStop = false;
            this.toolTip1.SetToolTip(this.Input_1, "Class Name");
            // 
            // Dropdown_1
            // 
            this.Dropdown_1.BackColor = System.Drawing.SystemColors.Window;
            this.Dropdown_1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Dropdown_1.FormattingEnabled = true;
            this.Dropdown_1.IntegralHeight = false;
            this.Dropdown_1.Location = new System.Drawing.Point(113, 25);
            this.Dropdown_1.Name = "Dropdown_1";
            this.Dropdown_1.Size = new System.Drawing.Size(358, 21);
            this.Dropdown_1.TabIndex = 2;
            this.toolTip1.SetToolTip(this.Dropdown_1, "Choose from class list");
            this.Dropdown_1.DropDown += new System.EventHandler(this.Dropdown_1_DropDown);
            this.Dropdown_1.SelectionChangeCommitted += new System.EventHandler(this.Dropdown_1_SelectionChangeCommitted);
            // 
            // Save
            // 
            this.Save.Enabled = false;
            this.Save.Image = ((System.Drawing.Image)(resources.GetObject("Save.Image")));
            this.Save.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Save.Location = new System.Drawing.Point(378, 89);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(93, 25);
            this.Save.TabIndex = 4;
            this.Save.Text = "Save Name";
            this.Save.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.Save, "Save class name");
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // PatientsClassWaitingDGV
            // 
            this.PatientsClassWaitingDGV.AllowUserToAddRows = false;
            this.PatientsClassWaitingDGV.AllowUserToDeleteRows = false;
            this.PatientsClassWaitingDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.PatientsClassWaitingDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PatientsClassWaitingDGV.Location = new System.Drawing.Point(6, 19);
            this.PatientsClassWaitingDGV.MultiSelect = false;
            this.PatientsClassWaitingDGV.Name = "PatientsClassWaitingDGV";
            this.PatientsClassWaitingDGV.ReadOnly = true;
            this.PatientsClassWaitingDGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.PatientsClassWaitingDGV.Size = new System.Drawing.Size(479, 448);
            this.PatientsClassWaitingDGV.TabIndex = 13;
            this.PatientsClassWaitingDGV.TabStop = false;
            this.toolTip1.SetToolTip(this.PatientsClassWaitingDGV, "Patients currently in class waiting list");
            this.PatientsClassWaitingDGV.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.PatientsClassWaitingDGV_DataBindingComplete);
            this.PatientsClassWaitingDGV.SelectionChanged += new System.EventHandler(this.PatientsClassWaitingDGV_SelectionChanged);
            // 
            // Date_1
            // 
            this.Date_1.Enabled = false;
            this.Date_1.Location = new System.Drawing.Point(113, 78);
            this.Date_1.Name = "Date_1";
            this.Date_1.Size = new System.Drawing.Size(358, 20);
            this.Date_1.TabIndex = 7;
            this.Date_1.TabStop = false;
            this.toolTip1.SetToolTip(this.Date_1, "Enter patient start date");
            this.Date_1.Value = new System.DateTime(2015, 1, 4, 0, 0, 0, 0);
            // 
            // Add_Patient_Waiting
            // 
            this.Add_Patient_Waiting.Enabled = false;
            this.Add_Patient_Waiting.Image = ((System.Drawing.Image)(resources.GetObject("Add_Patient_Waiting.Image")));
            this.Add_Patient_Waiting.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Add_Patient_Waiting.Location = new System.Drawing.Point(113, 104);
            this.Add_Patient_Waiting.Name = "Add_Patient_Waiting";
            this.Add_Patient_Waiting.Size = new System.Drawing.Size(166, 25);
            this.Add_Patient_Waiting.TabIndex = 9;
            this.Add_Patient_Waiting.Text = "Add Patient to Waiting List";
            this.Add_Patient_Waiting.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.Add_Patient_Waiting, "Add patient to class waiting list");
            this.Add_Patient_Waiting.UseVisualStyleBackColor = true;
            this.Add_Patient_Waiting.Click += new System.EventHandler(this.Add_Patient_Waiting_Click);
            // 
            // Remove_Waiting
            // 
            this.Remove_Waiting.Image = ((System.Drawing.Image)(resources.GetObject("Remove_Waiting.Image")));
            this.Remove_Waiting.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Remove_Waiting.Location = new System.Drawing.Point(292, 473);
            this.Remove_Waiting.Name = "Remove_Waiting";
            this.Remove_Waiting.Size = new System.Drawing.Size(193, 25);
            this.Remove_Waiting.TabIndex = 15;
            this.Remove_Waiting.TabStop = false;
            this.Remove_Waiting.Text = "Remove Patient From Waiting List";
            this.Remove_Waiting.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.Remove_Waiting, "Remove patient from class waiting list");
            this.Remove_Waiting.UseVisualStyleBackColor = true;
            this.Remove_Waiting.Click += new System.EventHandler(this.Remove_Waiting_Click);
            // 
            // Move
            // 
            this.Move.Image = ((System.Drawing.Image)(resources.GetObject("Move.Image")));
            this.Move.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Move.Location = new System.Drawing.Point(19, 473);
            this.Move.Name = "Move";
            this.Move.Size = new System.Drawing.Size(171, 25);
            this.Move.TabIndex = 14;
            this.Move.Text = "Add Waiting Patient to Class";
            this.Move.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.Move, "Move patient from waiting list to class");
            this.Move.UseVisualStyleBackColor = true;
            this.Move.Click += new System.EventHandler(this.Move_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Sort_Lists);
            this.groupBox1.Controls.Add(this.Add_Patient_Waiting);
            this.groupBox1.Controls.Add(label4);
            this.groupBox1.Controls.Add(Date_Label);
            this.groupBox1.Controls.Add(this.Date_1);
            this.groupBox1.Controls.Add(label2);
            this.groupBox1.Controls.Add(this.Remove);
            this.groupBox1.Controls.Add(this.PatientsClassDGV);
            this.groupBox1.Controls.Add(this.Edit_Start_Time);
            this.groupBox1.Controls.Add(this.Add_Patient);
            this.groupBox1.Controls.Add(this.Dropdown_2);
            this.groupBox1.Controls.Add(Notes_Label);
            this.groupBox1.Location = new System.Drawing.Point(7, 141);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(482, 389);
            this.groupBox1.TabIndex = 146;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Class Details";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Delete_Class);
            this.groupBox2.Controls.Add(this.Save);
            this.groupBox2.Controls.Add(this.Add_Class);
            this.groupBox2.Controls.Add(this.Input_1);
            this.groupBox2.Controls.Add(this.Dropdown_1);
            this.groupBox2.Controls.Add(Label);
            this.groupBox2.Location = new System.Drawing.Point(7, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(482, 123);
            this.groupBox2.TabIndex = 147;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Choose Class";
            // 
            // Delete_Class
            // 
            this.Delete_Class.Enabled = false;
            this.Delete_Class.Image = ((System.Drawing.Image)(resources.GetObject("Delete_Class.Image")));
            this.Delete_Class.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Delete_Class.Location = new System.Drawing.Point(113, 89);
            this.Delete_Class.Name = "Delete_Class";
            this.Delete_Class.Size = new System.Drawing.Size(99, 25);
            this.Delete_Class.TabIndex = 5;
            this.Delete_Class.TabStop = false;
            this.Delete_Class.Text = "Delete Class";
            this.Delete_Class.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.Delete_Class, "Delete selected class");
            this.Delete_Class.UseVisualStyleBackColor = true;
            this.Delete_Class.Click += new System.EventHandler(this.Delete_Class_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Remove_Waiting);
            this.groupBox3.Controls.Add(this.Move);
            this.groupBox3.Controls.Add(this.PatientsClassWaitingDGV);
            this.groupBox3.Location = new System.Drawing.Point(496, 19);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(494, 511);
            this.groupBox3.TabIndex = 148;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Waiting List";
            // 
            // Add_Edit_Class
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1014, 569);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.UserLabel);
            this.Controls.Add(this.Back);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Add_Edit_Class";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add/Edit Class";
            this.Activated += new System.EventHandler(this.Add_Edit_Class_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Add_or_Edit_Group_Class_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.PatientsClassDGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PatientsClassWaitingDGV)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Back;
        private System.Windows.Forms.Label UserLabel;
        private System.Windows.Forms.Button Remove;
        private System.Windows.Forms.DataGridView PatientsClassDGV;
        private System.Windows.Forms.Button Edit_Start_Time;
        private System.Windows.Forms.Button Add_Patient;
        private System.Windows.Forms.ComboBox Dropdown_2;
        private System.Windows.Forms.Button Add_Class;
        private System.Windows.Forms.Button Sort_Lists;
        private System.Windows.Forms.TextBox Input_1;
        private System.Windows.Forms.ComboBox Dropdown_1;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.DataGridView PatientsClassWaitingDGV;
        private System.Windows.Forms.DateTimePicker Date_1;
        private System.Windows.Forms.Button Add_Patient_Waiting;
        private System.Windows.Forms.Button Remove_Waiting;
        private System.Windows.Forms.Button Move;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button Delete_Class;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}