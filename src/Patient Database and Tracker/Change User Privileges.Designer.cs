namespace Patient_Database_and_Tracker
{
    partial class Change_User_Privileges
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Change_User_Privileges));
            this.Back = new System.Windows.Forms.Button();
            this.Dropdown_1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Master_User = new System.Windows.Forms.CheckBox();
            this.Delete = new System.Windows.Forms.Button();
            this.Save = new System.Windows.Forms.Button();
            this.UserLabel = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // Back
            // 
            this.Back.Image = ((System.Drawing.Image)(resources.GetObject("Back.Image")));
            this.Back.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Back.Location = new System.Drawing.Point(236, 166);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(74, 25);
            this.Back.TabIndex = 4;
            this.Back.Text = "Back";
            this.Back.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.Back, "Back to previous screen");
            this.Back.UseVisualStyleBackColor = true;
            this.Back.Click += new System.EventHandler(this.Back_Click);
            // 
            // Dropdown_1
            // 
            this.Dropdown_1.BackColor = System.Drawing.SystemColors.Window;
            this.Dropdown_1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Dropdown_1.FormattingEnabled = true;
            this.Dropdown_1.IntegralHeight = false;
            this.Dropdown_1.Location = new System.Drawing.Point(45, 55);
            this.Dropdown_1.Name = "Dropdown_1";
            this.Dropdown_1.Size = new System.Drawing.Size(233, 21);
            this.Dropdown_1.TabIndex = 5;
            this.Dropdown_1.Tag = "";
            this.toolTip1.SetToolTip(this.Dropdown_1, "Choose user from user list");
            this.Dropdown_1.DropDown += new System.EventHandler(this.Dropdown_1_DropDown);
            this.Dropdown_1.SelectionChangeCommitted += new System.EventHandler(this.Dropdown_1_SelectionChangeCommitted);
            this.Dropdown_1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Dropdown_1_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(69, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 13);
            this.label1.TabIndex = 59;
            this.label1.Text = "Choose a User from the Dropdown Box";
            // 
            // Master_User
            // 
            this.Master_User.AutoSize = true;
            this.Master_User.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Master_User.Enabled = false;
            this.Master_User.Location = new System.Drawing.Point(120, 115);
            this.Master_User.Name = "Master_User";
            this.Master_User.Size = new System.Drawing.Size(83, 17);
            this.Master_User.TabIndex = 60;
            this.Master_User.TabStop = false;
            this.Master_User.Tag = "";
            this.Master_User.Text = "Master User";
            this.toolTip1.SetToolTip(this.Master_User, "Master user");
            this.Master_User.UseVisualStyleBackColor = true;
            // 
            // Delete
            // 
            this.Delete.Enabled = false;
            this.Delete.Image = ((System.Drawing.Image)(resources.GetObject("Delete.Image")));
            this.Delete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Delete.Location = new System.Drawing.Point(127, 166);
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(100, 25);
            this.Delete.TabIndex = 101;
            this.Delete.TabStop = false;
            this.Delete.Text = "Delete User";
            this.Delete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.Delete, "Delete selected user");
            this.Delete.UseVisualStyleBackColor = true;
            this.Delete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // Save
            // 
            this.Save.Enabled = false;
            this.Save.Image = ((System.Drawing.Image)(resources.GetObject("Save.Image")));
            this.Save.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Save.Location = new System.Drawing.Point(12, 166);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(106, 25);
            this.Save.TabIndex = 102;
            this.Save.Text = "Save Changes";
            this.Save.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.Save, "Save changes to user");
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // UserLabel
            // 
            this.UserLabel.AutoSize = true;
            this.UserLabel.Location = new System.Drawing.Point(9, 204);
            this.UserLabel.Name = "UserLabel";
            this.UserLabel.Size = new System.Drawing.Size(69, 13);
            this.UserLabel.TabIndex = 114;
            this.UserLabel.Text = "Current User:";
            // 
            // Change_User_Privileges
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 225);
            this.Controls.Add(this.UserLabel);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.Delete);
            this.Controls.Add(this.Master_User);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Dropdown_1);
            this.Controls.Add(this.Back);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Change_User_Privileges";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Change User Privileges";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Change_User_Privileges_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Back;
        private System.Windows.Forms.ComboBox Dropdown_1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox Master_User;
        private System.Windows.Forms.Button Delete;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Label UserLabel;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}