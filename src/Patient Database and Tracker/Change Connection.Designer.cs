namespace Patient_Database_and_Tracker
{
    partial class Change_Connection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Change_Connection));
            this.label2 = new System.Windows.Forms.Label();
            this.Choose_Database_Button = new System.Windows.Forms.Button();
            this.Leave_Unchanged = new System.Windows.Forms.Button();
            this.UserLabel = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(26, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(219, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Please select a database to connect to:";
            // 
            // Choose_Database_Button
            // 
            this.Choose_Database_Button.Image = ((System.Drawing.Image)(resources.GetObject("Choose_Database_Button.Image")));
            this.Choose_Database_Button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Choose_Database_Button.Location = new System.Drawing.Point(73, 50);
            this.Choose_Database_Button.Name = "Choose_Database_Button";
            this.Choose_Database_Button.Size = new System.Drawing.Size(124, 25);
            this.Choose_Database_Button.TabIndex = 2;
            this.Choose_Database_Button.Tag = "Choose a database to connect to";
            this.Choose_Database_Button.Text = "Choose Database";
            this.Choose_Database_Button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.Choose_Database_Button, "Change database connection");
            this.Choose_Database_Button.UseVisualStyleBackColor = true;
            this.Choose_Database_Button.Click += new System.EventHandler(this.Choose_Database_Button_Click);
            // 
            // Leave_Unchanged
            // 
            this.Leave_Unchanged.Location = new System.Drawing.Point(73, 81);
            this.Leave_Unchanged.Name = "Leave_Unchanged";
            this.Leave_Unchanged.Size = new System.Drawing.Size(124, 25);
            this.Leave_Unchanged.TabIndex = 4;
            this.Leave_Unchanged.Tag = "Leave database connection unchanged";
            this.Leave_Unchanged.Text = "Leave Unchanged";
            this.toolTip1.SetToolTip(this.Leave_Unchanged, "Back to previous screen");
            this.Leave_Unchanged.UseVisualStyleBackColor = true;
            this.Leave_Unchanged.Click += new System.EventHandler(this.Leave_Click);
            // 
            // UserLabel
            // 
            this.UserLabel.AutoSize = true;
            this.UserLabel.Location = new System.Drawing.Point(12, 116);
            this.UserLabel.Name = "UserLabel";
            this.UserLabel.Size = new System.Drawing.Size(69, 13);
            this.UserLabel.TabIndex = 11;
            this.UserLabel.Text = "Current User:";
            // 
            // Change_Connection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(270, 138);
            this.Controls.Add(this.UserLabel);
            this.Controls.Add(this.Leave_Unchanged);
            this.Controls.Add(this.Choose_Database_Button);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Change_Connection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configure Database";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Change_Connection_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Choose_Database_Button;
        private System.Windows.Forms.Button Leave_Unchanged;
        private System.Windows.Forms.Label UserLabel;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}