namespace Patient_Database_and_Tracker
{
    partial class Edit_Class_Start_Time
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
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label pAT_ID_NumberLabel1;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label3;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Edit_Class_Start_Time));
            this.UserLabel = new System.Windows.Forms.Label();
            this.Back = new System.Windows.Forms.Button();
            this.Date_1 = new System.Windows.Forms.DateTimePicker();
            this.Input_1 = new System.Windows.Forms.TextBox();
            this.Input_2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Save = new System.Windows.Forms.Button();
            this.Input_3 = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            label4 = new System.Windows.Forms.Label();
            pAT_ID_NumberLabel1 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(12, 148);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(58, 13);
            label4.TabIndex = 147;
            label4.Text = "Start Date:";
            // 
            // pAT_ID_NumberLabel1
            // 
            pAT_ID_NumberLabel1.AutoSize = true;
            pAT_ID_NumberLabel1.Location = new System.Drawing.Point(12, 14);
            pAT_ID_NumberLabel1.Name = "pAT_ID_NumberLabel1";
            pAT_ID_NumberLabel1.Size = new System.Drawing.Size(43, 13);
            pAT_ID_NumberLabel1.TabIndex = 149;
            pAT_ID_NumberLabel1.Text = "Patient:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 40);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(35, 13);
            label1.TabIndex = 151;
            label1.Text = "Class:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(12, 66);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(95, 13);
            label3.TabIndex = 155;
            label3.Text = "Current Start Date:";
            // 
            // UserLabel
            // 
            this.UserLabel.AutoSize = true;
            this.UserLabel.Location = new System.Drawing.Point(7, 226);
            this.UserLabel.Name = "UserLabel";
            this.UserLabel.Size = new System.Drawing.Size(69, 13);
            this.UserLabel.TabIndex = 145;
            this.UserLabel.Text = "Current User:";
            // 
            // Back
            // 
            this.Back.Image = ((System.Drawing.Image)(resources.GetObject("Back.Image")));
            this.Back.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Back.Location = new System.Drawing.Point(249, 198);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(73, 25);
            this.Back.TabIndex = 144;
            this.Back.Text = "Back";
            this.Back.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.Back, "Back to previous screen");
            this.Back.UseVisualStyleBackColor = true;
            this.Back.Click += new System.EventHandler(this.Back_Click);
            // 
            // Date_1
            // 
            this.Date_1.Location = new System.Drawing.Point(118, 142);
            this.Date_1.Name = "Date_1";
            this.Date_1.Size = new System.Drawing.Size(204, 20);
            this.Date_1.TabIndex = 146;
            this.Date_1.TabStop = false;
            this.toolTip1.SetToolTip(this.Date_1, "Choose new start date");
            this.Date_1.Value = new System.DateTime(2015, 1, 4, 0, 0, 0, 0);
            // 
            // Input_1
            // 
            this.Input_1.Location = new System.Drawing.Point(118, 11);
            this.Input_1.Name = "Input_1";
            this.Input_1.ReadOnly = true;
            this.Input_1.Size = new System.Drawing.Size(204, 20);
            this.Input_1.TabIndex = 148;
            this.Input_1.TabStop = false;
            this.toolTip1.SetToolTip(this.Input_1, "Patient details");
            // 
            // Input_2
            // 
            this.Input_2.Location = new System.Drawing.Point(118, 37);
            this.Input_2.Name = "Input_2";
            this.Input_2.ReadOnly = true;
            this.Input_2.Size = new System.Drawing.Size(204, 20);
            this.Input_2.TabIndex = 150;
            this.Input_2.TabStop = false;
            this.toolTip1.SetToolTip(this.Input_2, "Class name");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(171, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 152;
            this.label2.Text = "Choose a start date";
            // 
            // Save
            // 
            this.Save.Image = ((System.Drawing.Image)(resources.GetObject("Save.Image")));
            this.Save.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Save.Location = new System.Drawing.Point(10, 198);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(106, 25);
            this.Save.TabIndex = 153;
            this.Save.Text = "Save Changes";
            this.Save.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.Save, "Save changes to start date");
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // Input_3
            // 
            this.Input_3.Location = new System.Drawing.Point(118, 63);
            this.Input_3.Name = "Input_3";
            this.Input_3.ReadOnly = true;
            this.Input_3.Size = new System.Drawing.Size(204, 20);
            this.Input_3.TabIndex = 154;
            this.Input_3.TabStop = false;
            this.toolTip1.SetToolTip(this.Input_3, "Current start date");
            // 
            // Edit_Class_Start_Time
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 245);
            this.Controls.Add(this.Input_3);
            this.Controls.Add(label3);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Input_2);
            this.Controls.Add(label1);
            this.Controls.Add(this.Input_1);
            this.Controls.Add(pAT_ID_NumberLabel1);
            this.Controls.Add(label4);
            this.Controls.Add(this.Date_1);
            this.Controls.Add(this.UserLabel);
            this.Controls.Add(this.Back);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Edit_Class_Start_Time";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit Start Time";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label UserLabel;
        private System.Windows.Forms.Button Back;
        private System.Windows.Forms.DateTimePicker Date_1;
        private System.Windows.Forms.TextBox Input_1;
        private System.Windows.Forms.TextBox Input_2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.TextBox Input_3;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}