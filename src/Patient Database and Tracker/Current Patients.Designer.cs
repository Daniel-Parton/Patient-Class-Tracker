namespace Patient_Database_and_Tracker
{
    partial class Current_Patients
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
            System.Windows.Forms.Label pAT_Last_NameLabel1;
            System.Windows.Forms.Label pAT_First_NameLabel1;
            System.Windows.Forms.Label pAT_ID_NumberLabel1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Current_Patients));
            this.Back = new System.Windows.Forms.Button();
            this.UserLabel = new System.Windows.Forms.Label();
            this.Current_Patient = new System.Windows.Forms.CheckBox();
            this.Input_3 = new System.Windows.Forms.TextBox();
            this.Input_2 = new System.Windows.Forms.TextBox();
            this.Input_1 = new System.Windows.Forms.TextBox();
            this.Dropdown_1 = new System.Windows.Forms.ComboBox();
            this.Save = new System.Windows.Forms.Button();
            this.Sort_Lists = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            pAT_Last_NameLabel1 = new System.Windows.Forms.Label();
            pAT_First_NameLabel1 = new System.Windows.Forms.Label();
            pAT_ID_NumberLabel1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pAT_Last_NameLabel1
            // 
            pAT_Last_NameLabel1.AutoSize = true;
            pAT_Last_NameLabel1.Location = new System.Drawing.Point(9, 102);
            pAT_Last_NameLabel1.Name = "pAT_Last_NameLabel1";
            pAT_Last_NameLabel1.Size = new System.Drawing.Size(61, 13);
            pAT_Last_NameLabel1.TabIndex = 107;
            pAT_Last_NameLabel1.Text = "Last Name:";
            // 
            // pAT_First_NameLabel1
            // 
            pAT_First_NameLabel1.AutoSize = true;
            pAT_First_NameLabel1.Location = new System.Drawing.Point(10, 76);
            pAT_First_NameLabel1.Name = "pAT_First_NameLabel1";
            pAT_First_NameLabel1.Size = new System.Drawing.Size(60, 13);
            pAT_First_NameLabel1.TabIndex = 106;
            pAT_First_NameLabel1.Text = "First Name:";
            // 
            // pAT_ID_NumberLabel1
            // 
            pAT_ID_NumberLabel1.AutoSize = true;
            pAT_ID_NumberLabel1.Location = new System.Drawing.Point(9, 50);
            pAT_ID_NumberLabel1.Name = "pAT_ID_NumberLabel1";
            pAT_ID_NumberLabel1.Size = new System.Drawing.Size(57, 13);
            pAT_ID_NumberLabel1.TabIndex = 105;
            pAT_ID_NumberLabel1.Text = "Patient ID:";
            // 
            // Back
            // 
            this.Back.Image = ((System.Drawing.Image)(resources.GetObject("Back.Image")));
            this.Back.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Back.Location = new System.Drawing.Point(352, 143);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(74, 25);
            this.Back.TabIndex = 5;
            this.Back.Text = "Back";
            this.Back.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.Back, "Back to previous screen");
            this.Back.UseVisualStyleBackColor = true;
            this.Back.Click += new System.EventHandler(this.Back_Click);
            // 
            // UserLabel
            // 
            this.UserLabel.AutoSize = true;
            this.UserLabel.Location = new System.Drawing.Point(12, 176);
            this.UserLabel.Name = "UserLabel";
            this.UserLabel.Size = new System.Drawing.Size(69, 13);
            this.UserLabel.TabIndex = 12;
            this.UserLabel.Text = "Current User:";
            // 
            // Current_Patient
            // 
            this.Current_Patient.AutoSize = true;
            this.Current_Patient.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Current_Patient.Enabled = false;
            this.Current_Patient.Location = new System.Drawing.Point(171, 148);
            this.Current_Patient.Name = "Current_Patient";
            this.Current_Patient.Size = new System.Drawing.Size(99, 17);
            this.Current_Patient.TabIndex = 108;
            this.Current_Patient.TabStop = false;
            this.Current_Patient.Text = "Current Patient:";
            this.toolTip1.SetToolTip(this.Current_Patient, "Current/non current patient");
            this.Current_Patient.UseVisualStyleBackColor = true;
            // 
            // Input_3
            // 
            this.Input_3.Location = new System.Drawing.Point(142, 102);
            this.Input_3.Name = "Input_3";
            this.Input_3.ReadOnly = true;
            this.Input_3.Size = new System.Drawing.Size(284, 20);
            this.Input_3.TabIndex = 104;
            this.toolTip1.SetToolTip(this.Input_3, "Last name of selected patient");
            // 
            // Input_2
            // 
            this.Input_2.Location = new System.Drawing.Point(142, 76);
            this.Input_2.Name = "Input_2";
            this.Input_2.ReadOnly = true;
            this.Input_2.Size = new System.Drawing.Size(284, 20);
            this.Input_2.TabIndex = 103;
            this.toolTip1.SetToolTip(this.Input_2, "First name of selected patient");
            // 
            // Input_1
            // 
            this.Input_1.Location = new System.Drawing.Point(142, 50);
            this.Input_1.Name = "Input_1";
            this.Input_1.ReadOnly = true;
            this.Input_1.Size = new System.Drawing.Size(284, 20);
            this.Input_1.TabIndex = 102;
            this.toolTip1.SetToolTip(this.Input_1, "Selected patient id");
            // 
            // Dropdown_1
            // 
            this.Dropdown_1.BackColor = System.Drawing.SystemColors.Window;
            this.Dropdown_1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Dropdown_1.FormattingEnabled = true;
            this.Dropdown_1.IntegralHeight = false;
            this.Dropdown_1.Location = new System.Drawing.Point(142, 15);
            this.Dropdown_1.Name = "Dropdown_1";
            this.Dropdown_1.Size = new System.Drawing.Size(284, 21);
            this.Dropdown_1.TabIndex = 101;
            this.toolTip1.SetToolTip(this.Dropdown_1, "Choose patient from patient list");
            this.Dropdown_1.DropDown += new System.EventHandler(this.Dropdown_1_DropDown);
            this.Dropdown_1.SelectionChangeCommitted += new System.EventHandler(this.Dropdown_1_SelectionChangeCommitted);
            // 
            // Save
            // 
            this.Save.Enabled = false;
            this.Save.Image = ((System.Drawing.Image)(resources.GetObject("Save.Image")));
            this.Save.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Save.Location = new System.Drawing.Point(12, 143);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(106, 25);
            this.Save.TabIndex = 109;
            this.Save.Text = "Save Changes";
            this.Save.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.Save, "Save changes to patient");
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // Sort_Lists
            // 
            this.Sort_Lists.Location = new System.Drawing.Point(12, 11);
            this.Sort_Lists.Name = "Sort_Lists";
            this.Sort_Lists.Size = new System.Drawing.Size(114, 26);
            this.Sort_Lists.TabIndex = 110;
            this.Sort_Lists.TabStop = false;
            this.Sort_Lists.Text = "Sort Lists by Name";
            this.toolTip1.SetToolTip(this.Sort_Lists, "Change way lists are sorted");
            this.Sort_Lists.UseVisualStyleBackColor = true;
            this.Sort_Lists.Click += new System.EventHandler(this.Sort_Lists_Click);
            // 
            // Current_Patients
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 200);
            this.Controls.Add(this.Sort_Lists);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.Current_Patient);
            this.Controls.Add(this.Input_3);
            this.Controls.Add(this.Input_2);
            this.Controls.Add(this.Input_1);
            this.Controls.Add(this.Dropdown_1);
            this.Controls.Add(pAT_Last_NameLabel1);
            this.Controls.Add(pAT_First_NameLabel1);
            this.Controls.Add(pAT_ID_NumberLabel1);
            this.Controls.Add(this.UserLabel);
            this.Controls.Add(this.Back);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Current_Patients";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Current/Non Current Patients";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Current_Patients_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Back;
        private System.Windows.Forms.Label UserLabel;
        private System.Windows.Forms.CheckBox Current_Patient;
        private System.Windows.Forms.TextBox Input_3;
        private System.Windows.Forms.TextBox Input_2;
        private System.Windows.Forms.TextBox Input_1;
        private System.Windows.Forms.ComboBox Dropdown_1;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Button Sort_Lists;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}