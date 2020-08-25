namespace Patient_Database_and_Tracker
{
    partial class Generate_Reports
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Generate_Reports));
            this.Date_1 = new System.Windows.Forms.DateTimePicker();
            this.Date_2 = new System.Windows.Forms.DateTimePicker();
            this.Search_Label2 = new System.Windows.Forms.Label();
            this.Search_Label = new System.Windows.Forms.Label();
            this.Create_Report = new System.Windows.Forms.Button();
            this.Back = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // Date_1
            // 
            this.Date_1.Location = new System.Drawing.Point(69, 9);
            this.Date_1.Name = "Date_1";
            this.Date_1.Size = new System.Drawing.Size(235, 20);
            this.Date_1.TabIndex = 113;
            this.toolTip1.SetToolTip(this.Date_1, "Enter start date");
            // 
            // Date_2
            // 
            this.Date_2.Location = new System.Drawing.Point(69, 52);
            this.Date_2.Name = "Date_2";
            this.Date_2.Size = new System.Drawing.Size(235, 20);
            this.Date_2.TabIndex = 114;
            this.toolTip1.SetToolTip(this.Date_2, "Enter finish date");
            // 
            // Search_Label2
            // 
            this.Search_Label2.AutoSize = true;
            this.Search_Label2.Location = new System.Drawing.Point(19, 52);
            this.Search_Label2.Name = "Search_Label2";
            this.Search_Label2.Size = new System.Drawing.Size(23, 13);
            this.Search_Label2.TabIndex = 117;
            this.Search_Label2.Text = "To:";
            this.Search_Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Search_Label
            // 
            this.Search_Label.AutoSize = true;
            this.Search_Label.Location = new System.Drawing.Point(9, 9);
            this.Search_Label.Name = "Search_Label";
            this.Search_Label.Size = new System.Drawing.Size(33, 13);
            this.Search_Label.TabIndex = 116;
            this.Search_Label.Text = "From:";
            this.Search_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Create_Report
            // 
            this.Create_Report.Location = new System.Drawing.Point(174, 90);
            this.Create_Report.Name = "Create_Report";
            this.Create_Report.Size = new System.Drawing.Size(130, 25);
            this.Create_Report.TabIndex = 118;
            this.Create_Report.Text = "Generate Report";
            this.toolTip1.SetToolTip(this.Create_Report, "Generate Report");
            this.Create_Report.UseVisualStyleBackColor = true;
            this.Create_Report.Click += new System.EventHandler(this.Create_Report_Click);
            // 
            // Back
            // 
            this.Back.Image = ((System.Drawing.Image)(resources.GetObject("Back.Image")));
            this.Back.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Back.Location = new System.Drawing.Point(12, 90);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(73, 25);
            this.Back.TabIndex = 120;
            this.Back.Text = "Back";
            this.Back.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.Back, "Back to previous screen");
            this.Back.UseVisualStyleBackColor = true;
            this.Back.Click += new System.EventHandler(this.Back_Click);
            // 
            // Generate_Reports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 125);
            this.Controls.Add(this.Back);
            this.Controls.Add(this.Create_Report);
            this.Controls.Add(this.Search_Label2);
            this.Controls.Add(this.Search_Label);
            this.Controls.Add(this.Date_1);
            this.Controls.Add(this.Date_2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Generate_Reports";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Please Choose Date Range for Report";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Generate_Reports_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker Date_1;
        private System.Windows.Forms.DateTimePicker Date_2;
        private System.Windows.Forms.Label Search_Label2;
        private System.Windows.Forms.Label Search_Label;
        private System.Windows.Forms.Button Create_Report;
        private System.Windows.Forms.Button Back;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}