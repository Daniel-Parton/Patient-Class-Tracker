namespace Patient_Database_and_Tracker
{
    partial class Generate_Letters
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
            System.Windows.Forms.Label rEFGP_ID_NumberLabel1;
            System.Windows.Forms.Label pAT_Last_NameLabel1;
            System.Windows.Forms.Label label7;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Generate_Letters));
            this.Back = new System.Windows.Forms.Button();
            this.UserLabel = new System.Windows.Forms.Label();
            this.Initial_Letter = new System.Windows.Forms.Button();
            this.Dropdown_1 = new System.Windows.Forms.ComboBox();
            this.Sort_Lists = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Input_5 = new System.Windows.Forms.TextBox();
            this.Input_4 = new System.Windows.Forms.TextBox();
            this.Input_3 = new System.Windows.Forms.TextBox();
            this.Input_2 = new System.Windows.Forms.TextBox();
            this.Input_1 = new System.Windows.Forms.TextBox();
            this.Initial_Letter_MHLP = new System.Windows.Forms.Button();
            this.No_Contact_Letter = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Accept_Consultation = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Dropdown_2 = new System.Windows.Forms.ComboBox();
            this.GP_Letter = new System.Windows.Forms.CheckBox();
            this.Input_6 = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Set_Initial_Location = new System.Windows.Forms.LinkLabel();
            this.Set_Initial_MHLP_Location = new System.Windows.Forms.LinkLabel();
            this.Set_Noc_Contact_Location = new System.Windows.Forms.LinkLabel();
            rEFGP_ID_NumberLabel1 = new System.Windows.Forms.Label();
            pAT_Last_NameLabel1 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // rEFGP_ID_NumberLabel1
            // 
            rEFGP_ID_NumberLabel1.AutoSize = true;
            rEFGP_ID_NumberLabel1.Location = new System.Drawing.Point(1, 186);
            rEFGP_ID_NumberLabel1.Name = "rEFGP_ID_NumberLabel1";
            rEFGP_ID_NumberLabel1.Size = new System.Drawing.Size(71, 13);
            rEFGP_ID_NumberLabel1.TabIndex = 118;
            rEFGP_ID_NumberLabel1.Text = "Reffering GP:";
            // 
            // pAT_Last_NameLabel1
            // 
            pAT_Last_NameLabel1.AutoSize = true;
            pAT_Last_NameLabel1.Location = new System.Drawing.Point(1, 159);
            pAT_Last_NameLabel1.Name = "pAT_Last_NameLabel1";
            pAT_Last_NameLabel1.Size = new System.Drawing.Size(61, 13);
            pAT_Last_NameLabel1.TabIndex = 117;
            pAT_Last_NameLabel1.Text = "Last Name:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(1, 212);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(95, 13);
            label7.TabIndex = 138;
            label7.Text = "Initial Consultation:";
            // 
            // Back
            // 
            this.Back.Image = ((System.Drawing.Image)(resources.GetObject("Back.Image")));
            this.Back.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Back.Location = new System.Drawing.Point(330, 295);
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
            this.UserLabel.Location = new System.Drawing.Point(10, 301);
            this.UserLabel.Name = "UserLabel";
            this.UserLabel.Size = new System.Drawing.Size(69, 13);
            this.UserLabel.TabIndex = 107;
            this.UserLabel.Text = "Current User:";
            // 
            // Initial_Letter
            // 
            this.Initial_Letter.Enabled = false;
            this.Initial_Letter.Image = ((System.Drawing.Image)(resources.GetObject("Initial_Letter.Image")));
            this.Initial_Letter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Initial_Letter.Location = new System.Drawing.Point(5, 262);
            this.Initial_Letter.Name = "Initial_Letter";
            this.Initial_Letter.Size = new System.Drawing.Size(110, 27);
            this.Initial_Letter.TabIndex = 2;
            this.Initial_Letter.Text = "Initial GP Letter";
            this.Initial_Letter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.Initial_Letter, "Generate initial gp letter");
            this.Initial_Letter.UseVisualStyleBackColor = true;
            this.Initial_Letter.Click += new System.EventHandler(this.Initial_Letter_Click);
            // 
            // Dropdown_1
            // 
            this.Dropdown_1.BackColor = System.Drawing.SystemColors.Window;
            this.Dropdown_1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Dropdown_1.FormattingEnabled = true;
            this.Dropdown_1.IntegralHeight = false;
            this.Dropdown_1.Location = new System.Drawing.Point(107, 55);
            this.Dropdown_1.Name = "Dropdown_1";
            this.Dropdown_1.Size = new System.Drawing.Size(296, 21);
            this.Dropdown_1.TabIndex = 1;
            this.toolTip1.SetToolTip(this.Dropdown_1, "Choose patient from patient list");
            this.Dropdown_1.DropDown += new System.EventHandler(this.Dropdown_1_DropDown);
            this.Dropdown_1.SelectionChangeCommitted += new System.EventHandler(this.Dropdown_1_SelectionChangeCommitted);
            this.Dropdown_1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Dropdown_1_KeyDown);
            // 
            // Sort_Lists
            // 
            this.Sort_Lists.Location = new System.Drawing.Point(12, 12);
            this.Sort_Lists.Name = "Sort_Lists";
            this.Sort_Lists.Size = new System.Drawing.Size(75, 39);
            this.Sort_Lists.TabIndex = 110;
            this.Sort_Lists.TabStop = false;
            this.Sort_Lists.Text = "Sort List by Name";
            this.toolTip1.SetToolTip(this.Sort_Lists, "Change way lists are sorted");
            this.Sort_Lists.UseVisualStyleBackColor = true;
            this.Sort_Lists.Click += new System.EventHandler(this.Sort_Lists_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(195, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 13);
            this.label1.TabIndex = 111;
            this.label1.Text = "Please Choose a Patent";
            // 
            // Input_5
            // 
            this.Input_5.Location = new System.Drawing.Point(107, 182);
            this.Input_5.Name = "Input_5";
            this.Input_5.ReadOnly = true;
            this.Input_5.Size = new System.Drawing.Size(296, 20);
            this.Input_5.TabIndex = 121;
            this.Input_5.TabStop = false;
            this.toolTip1.SetToolTip(this.Input_5, "Reffering gp");
            // 
            // Input_4
            // 
            this.Input_4.Location = new System.Drawing.Point(107, 157);
            this.Input_4.Name = "Input_4";
            this.Input_4.ReadOnly = true;
            this.Input_4.Size = new System.Drawing.Size(296, 20);
            this.Input_4.TabIndex = 120;
            this.Input_4.TabStop = false;
            this.toolTip1.SetToolTip(this.Input_4, "Last name");
            // 
            // Input_3
            // 
            this.Input_3.Location = new System.Drawing.Point(107, 132);
            this.Input_3.Name = "Input_3";
            this.Input_3.ReadOnly = true;
            this.Input_3.Size = new System.Drawing.Size(296, 20);
            this.Input_3.TabIndex = 114;
            this.Input_3.TabStop = false;
            this.toolTip1.SetToolTip(this.Input_3, "First name");
            // 
            // Input_2
            // 
            this.Input_2.Location = new System.Drawing.Point(107, 107);
            this.Input_2.Name = "Input_2";
            this.Input_2.ReadOnly = true;
            this.Input_2.Size = new System.Drawing.Size(296, 20);
            this.Input_2.TabIndex = 113;
            this.Input_2.TabStop = false;
            this.toolTip1.SetToolTip(this.Input_2, "Date reffered");
            // 
            // Input_1
            // 
            this.Input_1.Location = new System.Drawing.Point(107, 82);
            this.Input_1.Name = "Input_1";
            this.Input_1.ReadOnly = true;
            this.Input_1.Size = new System.Drawing.Size(296, 20);
            this.Input_1.TabIndex = 112;
            this.Input_1.TabStop = false;
            this.toolTip1.SetToolTip(this.Input_1, "Patient id number");
            // 
            // Initial_Letter_MHLP
            // 
            this.Initial_Letter_MHLP.Enabled = false;
            this.Initial_Letter_MHLP.Image = ((System.Drawing.Image)(resources.GetObject("Initial_Letter_MHLP.Image")));
            this.Initial_Letter_MHLP.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Initial_Letter_MHLP.Location = new System.Drawing.Point(125, 262);
            this.Initial_Letter_MHLP.Name = "Initial_Letter_MHLP";
            this.Initial_Letter_MHLP.Size = new System.Drawing.Size(139, 27);
            this.Initial_Letter_MHLP.TabIndex = 3;
            this.Initial_Letter_MHLP.Text = "Initial GP Letter MHLP";
            this.Initial_Letter_MHLP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.Initial_Letter_MHLP, "Generate initial gp letter for MHLP");
            this.Initial_Letter_MHLP.UseVisualStyleBackColor = true;
            this.Initial_Letter_MHLP.Click += new System.EventHandler(this.Initial_Letter_MHLP_Click);
            // 
            // No_Contact_Letter
            // 
            this.No_Contact_Letter.Enabled = false;
            this.No_Contact_Letter.Image = ((System.Drawing.Image)(resources.GetObject("No_Contact_Letter.Image")));
            this.No_Contact_Letter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.No_Contact_Letter.Location = new System.Drawing.Point(274, 262);
            this.No_Contact_Letter.Name = "No_Contact_Letter";
            this.No_Contact_Letter.Size = new System.Drawing.Size(128, 27);
            this.No_Contact_Letter.TabIndex = 4;
            this.No_Contact_Letter.Text = "No Contact Letter";
            this.No_Contact_Letter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.No_Contact_Letter, "Generate no contact letter");
            this.No_Contact_Letter.UseVisualStyleBackColor = true;
            this.No_Contact_Letter.Click += new System.EventHandler(this.No_Contact_Letter_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 126;
            this.label4.Text = "Patient ID:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1, 110);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 127;
            this.label5.Text = "Date Reffered:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(324, 13);
            this.label6.TabIndex = 131;
            this.label6.Text = "Please select the consultation you would like reffered to in the letter";
            this.label6.Visible = false;
            // 
            // Accept_Consultation
            // 
            this.Accept_Consultation.Image = ((System.Drawing.Image)(resources.GetObject("Accept_Consultation.Image")));
            this.Accept_Consultation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Accept_Consultation.Location = new System.Drawing.Point(20, 82);
            this.Accept_Consultation.Name = "Accept_Consultation";
            this.Accept_Consultation.Size = new System.Drawing.Size(112, 27);
            this.Accept_Consultation.TabIndex = 2;
            this.Accept_Consultation.Text = "Generate Letter";
            this.Accept_Consultation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Accept_Consultation.UseVisualStyleBackColor = true;
            this.Accept_Consultation.Visible = false;
            this.Accept_Consultation.Click += new System.EventHandler(this.Accept_Consultation_Click);
            // 
            // Cancel
            // 
            this.Cancel.Image = ((System.Drawing.Image)(resources.GetObject("Cancel.Image")));
            this.Cancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Cancel.Location = new System.Drawing.Point(236, 83);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(80, 25);
            this.Cancel.TabIndex = 3;
            this.Cancel.Text = "Cancel";
            this.Cancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Visible = false;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 135;
            this.label2.Text = "First Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(71, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(195, 13);
            this.label3.TabIndex = 137;
            this.label3.Text = "There is more than 1 Initial Consultation.";
            this.label3.Visible = false;
            // 
            // Dropdown_2
            // 
            this.Dropdown_2.BackColor = System.Drawing.SystemColors.Window;
            this.Dropdown_2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Dropdown_2.FormattingEnabled = true;
            this.Dropdown_2.IntegralHeight = false;
            this.Dropdown_2.Location = new System.Drawing.Point(20, 55);
            this.Dropdown_2.Name = "Dropdown_2";
            this.Dropdown_2.Size = new System.Drawing.Size(296, 21);
            this.Dropdown_2.TabIndex = 1;
            this.Dropdown_2.Visible = false;
            this.Dropdown_2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Dropdown_2_KeyDown);
            // 
            // GP_Letter
            // 
            this.GP_Letter.AutoSize = true;
            this.GP_Letter.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.GP_Letter.Enabled = false;
            this.GP_Letter.Location = new System.Drawing.Point(225, 300);
            this.GP_Letter.Name = "GP_Letter";
            this.GP_Letter.Size = new System.Drawing.Size(99, 17);
            this.GP_Letter.TabIndex = 6;
            this.GP_Letter.TabStop = false;
            this.GP_Letter.Text = "GP Letter Sent:";
            this.toolTip1.SetToolTip(this.GP_Letter, "GP letter sent");
            this.GP_Letter.UseVisualStyleBackColor = true;
            this.GP_Letter.Click += new System.EventHandler(this.GP_Letter_Click);
            // 
            // Input_6
            // 
            this.Input_6.Location = new System.Drawing.Point(107, 208);
            this.Input_6.Name = "Input_6";
            this.Input_6.ReadOnly = true;
            this.Input_6.Size = new System.Drawing.Size(296, 20);
            this.Input_6.TabIndex = 139;
            this.Input_6.TabStop = false;
            this.toolTip1.SetToolTip(this.Input_6, "Initial consultation");
            // 
            // Set_Initial_Location
            // 
            this.Set_Initial_Location.AutoSize = true;
            this.Set_Initial_Location.Location = new System.Drawing.Point(12, 246);
            this.Set_Initial_Location.Name = "Set_Initial_Location";
            this.Set_Initial_Location.Size = new System.Drawing.Size(97, 13);
            this.Set_Initial_Location.TabIndex = 140;
            this.Set_Initial_Location.TabStop = true;
            this.Set_Initial_Location.Text = "Set Letter Location";
            this.Set_Initial_Location.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Set_Initial_Location_LinkClicked);
            // 
            // Set_Initial_MHLP_Location
            // 
            this.Set_Initial_MHLP_Location.AutoSize = true;
            this.Set_Initial_MHLP_Location.Location = new System.Drawing.Point(149, 246);
            this.Set_Initial_MHLP_Location.Name = "Set_Initial_MHLP_Location";
            this.Set_Initial_MHLP_Location.Size = new System.Drawing.Size(97, 13);
            this.Set_Initial_MHLP_Location.TabIndex = 141;
            this.Set_Initial_MHLP_Location.TabStop = true;
            this.Set_Initial_MHLP_Location.Text = "Set Letter Location";
            this.Set_Initial_MHLP_Location.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Set_Initial_MHLP_Location_LinkClicked);
            // 
            // Set_Noc_Contact_Location
            // 
            this.Set_Noc_Contact_Location.AutoSize = true;
            this.Set_Noc_Contact_Location.Location = new System.Drawing.Point(290, 246);
            this.Set_Noc_Contact_Location.Name = "Set_Noc_Contact_Location";
            this.Set_Noc_Contact_Location.Size = new System.Drawing.Size(97, 13);
            this.Set_Noc_Contact_Location.TabIndex = 142;
            this.Set_Noc_Contact_Location.TabStop = true;
            this.Set_Noc_Contact_Location.Text = "Set Letter Location";
            this.Set_Noc_Contact_Location.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Set_Noc_Contact_Location_LinkClicked);
            // 
            // Generate_Letters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 331);
            this.Controls.Add(this.Set_Noc_Contact_Location);
            this.Controls.Add(this.Set_Initial_MHLP_Location);
            this.Controls.Add(this.Set_Initial_Location);
            this.Controls.Add(this.Input_6);
            this.Controls.Add(label7);
            this.Controls.Add(this.GP_Letter);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.No_Contact_Letter);
            this.Controls.Add(this.Initial_Letter_MHLP);
            this.Controls.Add(this.Input_5);
            this.Controls.Add(this.Input_4);
            this.Controls.Add(this.Input_3);
            this.Controls.Add(this.Input_2);
            this.Controls.Add(this.Input_1);
            this.Controls.Add(rEFGP_ID_NumberLabel1);
            this.Controls.Add(pAT_Last_NameLabel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Sort_Lists);
            this.Controls.Add(this.Dropdown_1);
            this.Controls.Add(this.Initial_Letter);
            this.Controls.Add(this.UserLabel);
            this.Controls.Add(this.Back);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Accept_Consultation);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Dropdown_2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Generate_Letters";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generate Letters";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Generate_Letters_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Back;
        private System.Windows.Forms.Label UserLabel;
        private System.Windows.Forms.Button Initial_Letter;
        private System.Windows.Forms.ComboBox Dropdown_1;
        private System.Windows.Forms.Button Sort_Lists;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Input_5;
        private System.Windows.Forms.TextBox Input_4;
        private System.Windows.Forms.TextBox Input_3;
        private System.Windows.Forms.TextBox Input_2;
        private System.Windows.Forms.TextBox Input_1;
        private System.Windows.Forms.Button Initial_Letter_MHLP;
        private System.Windows.Forms.Button No_Contact_Letter;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button Accept_Consultation;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox Dropdown_2;
        private System.Windows.Forms.CheckBox GP_Letter;
        private System.Windows.Forms.TextBox Input_6;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.LinkLabel Set_Initial_Location;
        private System.Windows.Forms.LinkLabel Set_Initial_MHLP_Location;
        private System.Windows.Forms.LinkLabel Set_Noc_Contact_Location;
    }
}