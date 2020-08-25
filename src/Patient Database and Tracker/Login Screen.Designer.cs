using System.Drawing;

namespace Patient_Database_and_Tracker
{
    partial class Login_Screen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login_Screen));
            this.Login_Button = new System.Windows.Forms.Button();
            this.Username_Label = new System.Windows.Forms.Label();
            this.Password_Label = new System.Windows.Forms.Label();
            this.Username_Input = new System.Windows.Forms.TextBox();
            this.Password_Input = new System.Windows.Forms.TextBox();
            this.Register = new System.Windows.Forms.Button();
            this.Forgotten_Password = new System.Windows.Forms.LinkLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.New_Database = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Change_Database = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Login_Button
            // 
            this.Login_Button.Image = ((System.Drawing.Image)(resources.GetObject("Login_Button.Image")));
            this.Login_Button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Login_Button.Location = new System.Drawing.Point(9, 64);
            this.Login_Button.Name = "Login_Button";
            this.Login_Button.Size = new System.Drawing.Size(75, 25);
            this.Login_Button.TabIndex = 3;
            this.Login_Button.Text = "Login";
            this.Login_Button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.Login_Button, "Login");
            this.Login_Button.UseVisualStyleBackColor = true;
            this.Login_Button.Click += new System.EventHandler(this.Login_Button_Click);
            // 
            // Username_Label
            // 
            this.Username_Label.AutoSize = true;
            this.Username_Label.Location = new System.Drawing.Point(6, 17);
            this.Username_Label.Name = "Username_Label";
            this.Username_Label.Size = new System.Drawing.Size(55, 13);
            this.Username_Label.TabIndex = 1;
            this.Username_Label.Text = "Username";
            // 
            // Password_Label
            // 
            this.Password_Label.AutoSize = true;
            this.Password_Label.Location = new System.Drawing.Point(6, 41);
            this.Password_Label.Name = "Password_Label";
            this.Password_Label.Size = new System.Drawing.Size(53, 13);
            this.Password_Label.TabIndex = 2;
            this.Password_Label.Text = "Password";
            // 
            // Username_Input
            // 
            this.Username_Input.Location = new System.Drawing.Point(80, 14);
            this.Username_Input.Name = "Username_Input";
            this.Username_Input.Size = new System.Drawing.Size(223, 20);
            this.Username_Input.TabIndex = 1;
            this.toolTip1.SetToolTip(this.Username_Input, "Enter username");
            this.Username_Input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Username_Input_KeyDown);
            // 
            // Password_Input
            // 
            this.Password_Input.Location = new System.Drawing.Point(80, 38);
            this.Password_Input.Name = "Password_Input";
            this.Password_Input.Size = new System.Drawing.Size(223, 20);
            this.Password_Input.TabIndex = 2;
            this.toolTip1.SetToolTip(this.Password_Input, "Enter password");
            this.Password_Input.UseSystemPasswordChar = true;
            this.Password_Input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Password_Input_KeyDown);
            // 
            // Register
            // 
            this.Register.Image = ((System.Drawing.Image)(resources.GetObject("Register.Image")));
            this.Register.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Register.Location = new System.Drawing.Point(119, 64);
            this.Register.Name = "Register";
            this.Register.Size = new System.Drawing.Size(83, 25);
            this.Register.TabIndex = 4;
            this.Register.Text = "Register";
            this.Register.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.Register, "Register new user");
            this.Register.UseVisualStyleBackColor = true;
            this.Register.Click += new System.EventHandler(this.Register_Click);
            // 
            // Forgotten_Password
            // 
            this.Forgotten_Password.AutoSize = true;
            this.Forgotten_Password.Location = new System.Drawing.Point(237, 64);
            this.Forgotten_Password.MaximumSize = new System.Drawing.Size(70, 0);
            this.Forgotten_Password.Name = "Forgotten_Password";
            this.Forgotten_Password.Size = new System.Drawing.Size(65, 26);
            this.Forgotten_Password.TabIndex = 5;
            this.Forgotten_Password.TabStop = true;
            this.Forgotten_Password.Text = "Forgot Your Password?";
            this.Forgotten_Password.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.Forgotten_Password, "Reset forgotten password");
            this.Forgotten_Password.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Forgotten_Password_LinkClicked);
            // 
            // New_Database
            // 
            this.New_Database.Image = ((System.Drawing.Image)(resources.GetObject("New_Database.Image")));
            this.New_Database.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.New_Database.Location = new System.Drawing.Point(9, 19);
            this.New_Database.Name = "New_Database";
            this.New_Database.Size = new System.Drawing.Size(143, 25);
            this.New_Database.TabIndex = 6;
            this.New_Database.Text = "Create New Database";
            this.New_Database.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.New_Database, "Create a new database");
            this.New_Database.UseVisualStyleBackColor = true;
            this.New_Database.Click += new System.EventHandler(this.New_Database_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Password_Input);
            this.groupBox1.Controls.Add(this.Username_Input);
            this.groupBox1.Controls.Add(this.Forgotten_Password);
            this.groupBox1.Controls.Add(this.Password_Label);
            this.groupBox1.Controls.Add(this.Register);
            this.groupBox1.Controls.Add(this.Username_Label);
            this.groupBox1.Controls.Add(this.Login_Button);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(317, 101);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "User Information";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Change_Database);
            this.groupBox2.Controls.Add(this.New_Database);
            this.groupBox2.Location = new System.Drawing.Point(12, 119);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(318, 55);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Database Configuration";
            // 
            // Change_Database
            // 
            this.Change_Database.Image = ((System.Drawing.Image)(resources.GetObject("Change_Database.Image")));
            this.Change_Database.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Change_Database.Location = new System.Drawing.Point(179, 19);
            this.Change_Database.Name = "Change_Database";
            this.Change_Database.Size = new System.Drawing.Size(124, 25);
            this.Change_Database.TabIndex = 7;
            this.Change_Database.Text = "Change Database";
            this.Change_Database.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.Change_Database, "Chnage database connection");
            this.Change_Database.UseVisualStyleBackColor = true;
            this.Change_Database.Click += new System.EventHandler(this.Change_Database_Click);
            // 
            // Login_Screen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 186);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Login_Screen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Login_Screen_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Login_Button;
        private System.Windows.Forms.Label Username_Label;
        private System.Windows.Forms.Label Password_Label;
        private System.Windows.Forms.TextBox Username_Input;
        private System.Windows.Forms.TextBox Password_Input;
        private System.Windows.Forms.Button Register;
        private System.Windows.Forms.LinkLabel Forgotten_Password;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button New_Database;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button Change_Database;
    }
}

