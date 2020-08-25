namespace Patient_Database_and_Tracker
{
    partial class Initial_Screen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Initial_Screen));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Change_Privileges = new System.Windows.Forms.Button();
            this.User_Details = new System.Windows.Forms.Button();
            this.Configure_Database_Button = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.Generate_Reports_Button = new System.Windows.Forms.Button();
            this.Logout = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Current_Non_Currrent_Patients = new System.Windows.Forms.Button();
            this.Add_Practice_Button = new System.Windows.Forms.Button();
            this.Add_GP_Button = new System.Windows.Forms.Button();
            this.Add_Consultation_Button = new System.Windows.Forms.Button();
            this.Add_Patients_Button = new System.Windows.Forms.Button();
            this.UserLabel = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Letters = new System.Windows.Forms.Button();
            this.Edit_View_Classes = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.View_General_Practices = new System.Windows.Forms.Button();
            this.View_Reffering_GPs = new System.Windows.Forms.Button();
            this.View_Consultations = new System.Windows.Forms.Button();
            this.View_Patients = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.Mark_Attendance = new System.Windows.Forms.Button();
            this.Class_Times = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Change_Privileges);
            this.groupBox1.Controls.Add(this.User_Details);
            this.groupBox1.Controls.Add(this.Configure_Database_Button);
            this.groupBox1.Location = new System.Drawing.Point(15, 255);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(500, 89);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Database and User Settings";
            // 
            // Change_Privileges
            // 
            this.Change_Privileges.Location = new System.Drawing.Point(199, 28);
            this.Change_Privileges.Name = "Change_Privileges";
            this.Change_Privileges.Size = new System.Drawing.Size(119, 42);
            this.Change_Privileges.TabIndex = 16;
            this.Change_Privileges.Text = "Manage User Privileges";
            this.toolTip1.SetToolTip(this.Change_Privileges, "Manage user privileges");
            this.Change_Privileges.UseVisualStyleBackColor = true;
            this.Change_Privileges.Click += new System.EventHandler(this.Change_Privileges_Click);
            // 
            // User_Details
            // 
            this.User_Details.Location = new System.Drawing.Point(361, 38);
            this.User_Details.Name = "User_Details";
            this.User_Details.Size = new System.Drawing.Size(119, 23);
            this.User_Details.TabIndex = 17;
            this.User_Details.Text = "Change My Details";
            this.toolTip1.SetToolTip(this.User_Details, "Change my details");
            this.User_Details.UseVisualStyleBackColor = true;
            this.User_Details.Click += new System.EventHandler(this.User_Details_Click);
            // 
            // Configure_Database_Button
            // 
            this.Configure_Database_Button.Location = new System.Drawing.Point(17, 29);
            this.Configure_Database_Button.Name = "Configure_Database_Button";
            this.Configure_Database_Button.Size = new System.Drawing.Size(119, 41);
            this.Configure_Database_Button.TabIndex = 15;
            this.Configure_Database_Button.Text = "Change Database Connection";
            this.toolTip1.SetToolTip(this.Configure_Database_Button, "Change database connection");
            this.Configure_Database_Button.UseVisualStyleBackColor = true;
            this.Configure_Database_Button.Click += new System.EventHandler(this.Configure_Database_Button_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(313, 350);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Reset Database";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Generate_Reports_Button
            // 
            this.Generate_Reports_Button.Image = ((System.Drawing.Image)(resources.GetObject("Generate_Reports_Button.Image")));
            this.Generate_Reports_Button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Generate_Reports_Button.Location = new System.Drawing.Point(16, 24);
            this.Generate_Reports_Button.Name = "Generate_Reports_Button";
            this.Generate_Reports_Button.Size = new System.Drawing.Size(119, 25);
            this.Generate_Reports_Button.TabIndex = 13;
            this.Generate_Reports_Button.Text = "Generate Reports";
            this.Generate_Reports_Button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.Generate_Reports_Button, "Generate reports");
            this.Generate_Reports_Button.UseVisualStyleBackColor = true;
            this.Generate_Reports_Button.Click += new System.EventHandler(this.Generate_Reports_Button_Click);
            // 
            // Logout
            // 
            this.Logout.Image = ((System.Drawing.Image)(resources.GetObject("Logout.Image")));
            this.Logout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Logout.Location = new System.Drawing.Point(438, 350);
            this.Logout.Name = "Logout";
            this.Logout.Size = new System.Drawing.Size(77, 25);
            this.Logout.TabIndex = 18;
            this.Logout.Text = "Logout";
            this.Logout.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.Logout, "Logout");
            this.Logout.UseVisualStyleBackColor = true;
            this.Logout.Click += new System.EventHandler(this.Logout_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Current_Non_Currrent_Patients);
            this.groupBox2.Controls.Add(this.Add_Practice_Button);
            this.groupBox2.Controls.Add(this.Add_GP_Button);
            this.groupBox2.Controls.Add(this.Add_Consultation_Button);
            this.groupBox2.Controls.Add(this.Add_Patients_Button);
            this.groupBox2.Location = new System.Drawing.Point(15, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(150, 237);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Add/Edit Patient Database";
            // 
            // Current_Non_Currrent_Patients
            // 
            this.Current_Non_Currrent_Patients.Location = new System.Drawing.Point(20, 188);
            this.Current_Non_Currrent_Patients.Name = "Current_Non_Currrent_Patients";
            this.Current_Non_Currrent_Patients.Size = new System.Drawing.Size(113, 41);
            this.Current_Non_Currrent_Patients.TabIndex = 5;
            this.Current_Non_Currrent_Patients.Text = "Current/Non Currrent Patients";
            this.toolTip1.SetToolTip(this.Current_Non_Currrent_Patients, "Set patients to current/non current");
            this.Current_Non_Currrent_Patients.UseVisualStyleBackColor = true;
            this.Current_Non_Currrent_Patients.Click += new System.EventHandler(this.Current_Non_Currrent_Patients_Click);
            // 
            // Add_Practice_Button
            // 
            this.Add_Practice_Button.Location = new System.Drawing.Point(20, 148);
            this.Add_Practice_Button.Name = "Add_Practice_Button";
            this.Add_Practice_Button.Size = new System.Drawing.Size(113, 25);
            this.Add_Practice_Button.TabIndex = 4;
            this.Add_Practice_Button.Text = "General Practices";
            this.toolTip1.SetToolTip(this.Add_Practice_Button, "Add/edit practices");
            this.Add_Practice_Button.UseVisualStyleBackColor = true;
            this.Add_Practice_Button.Click += new System.EventHandler(this.Add_Practice_Button_Click);
            // 
            // Add_GP_Button
            // 
            this.Add_GP_Button.Location = new System.Drawing.Point(20, 108);
            this.Add_GP_Button.Name = "Add_GP_Button";
            this.Add_GP_Button.Size = new System.Drawing.Size(113, 25);
            this.Add_GP_Button.TabIndex = 3;
            this.Add_GP_Button.Text = "Reffering GPs";
            this.toolTip1.SetToolTip(this.Add_GP_Button, "Add/edit reffering gps");
            this.Add_GP_Button.UseVisualStyleBackColor = true;
            this.Add_GP_Button.Click += new System.EventHandler(this.Add_GP_Button_Click);
            // 
            // Add_Consultation_Button
            // 
            this.Add_Consultation_Button.Location = new System.Drawing.Point(20, 28);
            this.Add_Consultation_Button.Name = "Add_Consultation_Button";
            this.Add_Consultation_Button.Size = new System.Drawing.Size(113, 25);
            this.Add_Consultation_Button.TabIndex = 1;
            this.Add_Consultation_Button.Text = "Consultations";
            this.toolTip1.SetToolTip(this.Add_Consultation_Button, "Add/edit consultations");
            this.Add_Consultation_Button.UseVisualStyleBackColor = true;
            this.Add_Consultation_Button.Click += new System.EventHandler(this.Add_Consultation_Button_Click);
            // 
            // Add_Patients_Button
            // 
            this.Add_Patients_Button.Location = new System.Drawing.Point(20, 68);
            this.Add_Patients_Button.Name = "Add_Patients_Button";
            this.Add_Patients_Button.Size = new System.Drawing.Size(113, 25);
            this.Add_Patients_Button.TabIndex = 2;
            this.Add_Patients_Button.Text = "Patients";
            this.toolTip1.SetToolTip(this.Add_Patients_Button, "Add/edit patients");
            this.Add_Patients_Button.UseVisualStyleBackColor = true;
            this.Add_Patients_Button.Click += new System.EventHandler(this.Add_Patients_Button_Click);
            // 
            // UserLabel
            // 
            this.UserLabel.AutoSize = true;
            this.UserLabel.Location = new System.Drawing.Point(12, 355);
            this.UserLabel.Name = "UserLabel";
            this.UserLabel.Size = new System.Drawing.Size(69, 13);
            this.UserLabel.TabIndex = 10;
            this.UserLabel.Text = "Current User:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Letters);
            this.groupBox3.Controls.Add(this.Generate_Reports_Button);
            this.groupBox3.Location = new System.Drawing.Point(362, 151);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(153, 97);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Letters/Reports";
            // 
            // Letters
            // 
            this.Letters.Image = ((System.Drawing.Image)(resources.GetObject("Letters.Image")));
            this.Letters.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Letters.Location = new System.Drawing.Point(16, 64);
            this.Letters.Name = "Letters";
            this.Letters.Size = new System.Drawing.Size(119, 23);
            this.Letters.TabIndex = 14;
            this.Letters.Text = "Generate Letters";
            this.Letters.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.Letters, "Generate letters");
            this.Letters.UseVisualStyleBackColor = true;
            this.Letters.Click += new System.EventHandler(this.Letters_Click);
            // 
            // Edit_View_Classes
            // 
            this.Edit_View_Classes.Location = new System.Drawing.Point(14, 27);
            this.Edit_View_Classes.Name = "Edit_View_Classes";
            this.Edit_View_Classes.Size = new System.Drawing.Size(119, 25);
            this.Edit_View_Classes.TabIndex = 10;
            this.Edit_View_Classes.Text = "Classes";
            this.toolTip1.SetToolTip(this.Edit_View_Classes, "Add/edit classes");
            this.Edit_View_Classes.UseVisualStyleBackColor = true;
            this.Edit_View_Classes.Click += new System.EventHandler(this.Edit_View_Classes_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.View_General_Practices);
            this.groupBox4.Controls.Add(this.View_Reffering_GPs);
            this.groupBox4.Controls.Add(this.View_Consultations);
            this.groupBox4.Controls.Add(this.View_Patients);
            this.groupBox4.Location = new System.Drawing.Point(200, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(150, 237);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "View/Export to Excel";
            // 
            // View_General_Practices
            // 
            this.View_General_Practices.Location = new System.Drawing.Point(20, 202);
            this.View_General_Practices.Name = "View_General_Practices";
            this.View_General_Practices.Size = new System.Drawing.Size(113, 25);
            this.View_General_Practices.TabIndex = 9;
            this.View_General_Practices.Text = "General Practices";
            this.toolTip1.SetToolTip(this.View_General_Practices, "View practices");
            this.View_General_Practices.UseVisualStyleBackColor = true;
            this.View_General_Practices.Click += new System.EventHandler(this.View_General_Practices_Click);
            // 
            // View_Reffering_GPs
            // 
            this.View_Reffering_GPs.Location = new System.Drawing.Point(20, 144);
            this.View_Reffering_GPs.Name = "View_Reffering_GPs";
            this.View_Reffering_GPs.Size = new System.Drawing.Size(113, 25);
            this.View_Reffering_GPs.TabIndex = 8;
            this.View_Reffering_GPs.Text = "Reffering GPs";
            this.toolTip1.SetToolTip(this.View_Reffering_GPs, "View reffering gps");
            this.View_Reffering_GPs.UseVisualStyleBackColor = true;
            this.View_Reffering_GPs.Click += new System.EventHandler(this.View_Reffering_GPs_Click);
            // 
            // View_Consultations
            // 
            this.View_Consultations.Location = new System.Drawing.Point(20, 28);
            this.View_Consultations.Name = "View_Consultations";
            this.View_Consultations.Size = new System.Drawing.Size(113, 25);
            this.View_Consultations.TabIndex = 6;
            this.View_Consultations.Text = "Consultations";
            this.toolTip1.SetToolTip(this.View_Consultations, "View consultations");
            this.View_Consultations.UseVisualStyleBackColor = true;
            this.View_Consultations.Click += new System.EventHandler(this.View_Consultations_Click);
            // 
            // View_Patients
            // 
            this.View_Patients.Location = new System.Drawing.Point(20, 86);
            this.View_Patients.Name = "View_Patients";
            this.View_Patients.Size = new System.Drawing.Size(113, 25);
            this.View_Patients.TabIndex = 7;
            this.View_Patients.Text = "Patients";
            this.toolTip1.SetToolTip(this.View_Patients, "View patients");
            this.View_Patients.UseVisualStyleBackColor = true;
            this.View_Patients.Click += new System.EventHandler(this.View_Patients_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.Mark_Attendance);
            this.groupBox5.Controls.Add(this.Class_Times);
            this.groupBox5.Controls.Add(this.Edit_View_Classes);
            this.groupBox5.Location = new System.Drawing.Point(362, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(153, 133);
            this.groupBox5.TabIndex = 11;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Add/Edit Class Database";
            // 
            // Mark_Attendance
            // 
            this.Mark_Attendance.Location = new System.Drawing.Point(14, 97);
            this.Mark_Attendance.Name = "Mark_Attendance";
            this.Mark_Attendance.Size = new System.Drawing.Size(119, 25);
            this.Mark_Attendance.TabIndex = 12;
            this.Mark_Attendance.Text = "Mark Attendance";
            this.toolTip1.SetToolTip(this.Mark_Attendance, "Mark class attendance for patients");
            this.Mark_Attendance.UseVisualStyleBackColor = true;
            this.Mark_Attendance.Click += new System.EventHandler(this.Mark_Attendance_Click);
            // 
            // Class_Times
            // 
            this.Class_Times.Location = new System.Drawing.Point(14, 62);
            this.Class_Times.Name = "Class_Times";
            this.Class_Times.Size = new System.Drawing.Size(119, 25);
            this.Class_Times.TabIndex = 11;
            this.Class_Times.Text = "Class Times";
            this.toolTip1.SetToolTip(this.Class_Times, "Add/edit class times");
            this.Class_Times.UseVisualStyleBackColor = true;
            this.Class_Times.Click += new System.EventHandler(this.Class_Times_Click);
            // 
            // Initial_Screen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 384);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.UserLabel);
            this.Controls.Add(this.Logout);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Initial_Screen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Patient Database and Class Tracker";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Initial_Screen_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Generate_Reports_Button;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Configure_Database_Button;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button Add_Practice_Button;
        private System.Windows.Forms.Button Add_GP_Button;
        private System.Windows.Forms.Button Add_Consultation_Button;
        private System.Windows.Forms.Button Add_Patients_Button;
        private System.Windows.Forms.Button User_Details;
        private System.Windows.Forms.Button Logout;
        private System.Windows.Forms.Label UserLabel;
        private System.Windows.Forms.Button Change_Privileges;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button Letters;
        private System.Windows.Forms.Button Current_Non_Currrent_Patients;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button View_General_Practices;
        private System.Windows.Forms.Button View_Reffering_GPs;
        private System.Windows.Forms.Button View_Consultations;
        private System.Windows.Forms.Button View_Patients;
        private System.Windows.Forms.Button Edit_View_Classes;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button Mark_Attendance;
        private System.Windows.Forms.Button Class_Times;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}