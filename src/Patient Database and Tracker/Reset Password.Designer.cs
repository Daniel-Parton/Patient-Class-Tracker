namespace Patient_Database_and_Tracker
{
    partial class Reset_User_Password
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Reset_User_Password));
            this.Back = new System.Windows.Forms.Button();
            this.Input_2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Input_3 = new System.Windows.Forms.TextBox();
            this.Password_Label = new System.Windows.Forms.Label();
            this.Input_1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Submit_1 = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // Back
            // 
            this.Back.Image = ((System.Drawing.Image)(resources.GetObject("Back.Image")));
            this.Back.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Back.Location = new System.Drawing.Point(198, 101);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(74, 25);
            this.Back.TabIndex = 5;
            this.Back.Text = "Back";
            this.Back.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.Back, "Back to previous screen");
            this.Back.UseVisualStyleBackColor = true;
            this.Back.Click += new System.EventHandler(this.Back_Click);
            // 
            // Input_2
            // 
            this.Input_2.Location = new System.Drawing.Point(122, 44);
            this.Input_2.Name = "Input_2";
            this.Input_2.Size = new System.Drawing.Size(150, 20);
            this.Input_2.TabIndex = 2;
            this.toolTip1.SetToolTip(this.Input_2, "Enter new password");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Re-Enter Password";
            // 
            // Input_3
            // 
            this.Input_3.Location = new System.Drawing.Point(122, 70);
            this.Input_3.Name = "Input_3";
            this.Input_3.Size = new System.Drawing.Size(150, 20);
            this.Input_3.TabIndex = 3;
            this.toolTip1.SetToolTip(this.Input_3, "Re-enter new password");
            // 
            // Password_Label
            // 
            this.Password_Label.AutoSize = true;
            this.Password_Label.Location = new System.Drawing.Point(15, 47);
            this.Password_Label.Name = "Password_Label";
            this.Password_Label.Size = new System.Drawing.Size(81, 13);
            this.Password_Label.TabIndex = 13;
            this.Password_Label.Text = "New Password:";
            // 
            // Input_1
            // 
            this.Input_1.Location = new System.Drawing.Point(122, 17);
            this.Input_1.Name = "Input_1";
            this.Input_1.Size = new System.Drawing.Size(150, 20);
            this.Input_1.TabIndex = 1;
            this.toolTip1.SetToolTip(this.Input_1, "Enter old password");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Old Password:";
            // 
            // Submit_1
            // 
            this.Submit_1.Image = ((System.Drawing.Image)(resources.GetObject("Submit_1.Image")));
            this.Submit_1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Submit_1.Location = new System.Drawing.Point(18, 101);
            this.Submit_1.Name = "Submit_1";
            this.Submit_1.Size = new System.Drawing.Size(75, 25);
            this.Submit_1.TabIndex = 4;
            this.Submit_1.Text = "Submit";
            this.Submit_1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.Submit_1, "Submit password reset");
            this.Submit_1.UseVisualStyleBackColor = true;
            this.Submit_1.Click += new System.EventHandler(this.Submit_1_Click);
            // 
            // Reset_User_Password
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 136);
            this.Controls.Add(this.Submit_1);
            this.Controls.Add(this.Input_1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Input_2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Input_3);
            this.Controls.Add(this.Password_Label);
            this.Controls.Add(this.Back);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Reset_User_Password";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reset Password";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Reset_User_Password_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Back;
        private System.Windows.Forms.TextBox Input_2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Input_3;
        private System.Windows.Forms.Label Password_Label;
        private System.Windows.Forms.TextBox Input_1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Submit_1;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}