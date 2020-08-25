namespace Patient_Database_and_Tracker
{
    partial class View_Records
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(View_Records));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.UserLabel = new System.Windows.Forms.Label();
            this.Total_Records = new System.Windows.Forms.Label();
            this.Total_Label = new System.Windows.Forms.Label();
            this.Input_1 = new System.Windows.Forms.TextBox();
            this.View_All = new System.Windows.Forms.Button();
            this.Date_1 = new System.Windows.Forms.DateTimePicker();
            this.dropDown1Label = new System.Windows.Forms.Label();
            this.Dropdown_3 = new System.Windows.Forms.ComboBox();
            this.Dropdown_1 = new System.Windows.Forms.ComboBox();
            this.Back = new System.Windows.Forms.Button();
            this.Search_Label2 = new System.Windows.Forms.Label();
            this.Create_Excel = new System.Windows.Forms.Button();
            this.Date_2 = new System.Windows.Forms.DateTimePicker();
            this.Dropdown_2 = new System.Windows.Forms.ComboBox();
            this.Search = new System.Windows.Forms.Button();
            this.dropDown2Label = new System.Windows.Forms.Label();
            this.Search_Label = new System.Windows.Forms.Label();
            this.Records_Gridview = new System.Windows.Forms.DataGridView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Records_Gridview)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.UserLabel);
            this.splitContainer1.Panel1.Controls.Add(this.Total_Records);
            this.splitContainer1.Panel1.Controls.Add(this.Total_Label);
            this.splitContainer1.Panel1.Controls.Add(this.Input_1);
            this.splitContainer1.Panel1.Controls.Add(this.View_All);
            this.splitContainer1.Panel1.Controls.Add(this.Date_1);
            this.splitContainer1.Panel1.Controls.Add(this.dropDown1Label);
            this.splitContainer1.Panel1.Controls.Add(this.Dropdown_3);
            this.splitContainer1.Panel1.Controls.Add(this.Dropdown_1);
            this.splitContainer1.Panel1.Controls.Add(this.Back);
            this.splitContainer1.Panel1.Controls.Add(this.Search_Label2);
            this.splitContainer1.Panel1.Controls.Add(this.Create_Excel);
            this.splitContainer1.Panel1.Controls.Add(this.Date_2);
            this.splitContainer1.Panel1.Controls.Add(this.Dropdown_2);
            this.splitContainer1.Panel1.Controls.Add(this.Search);
            this.splitContainer1.Panel1.Controls.Add(this.dropDown2Label);
            this.splitContainer1.Panel1.Controls.Add(this.Search_Label);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.Records_Gridview);
            this.splitContainer1.Size = new System.Drawing.Size(1354, 733);
            this.splitContainer1.SplitterDistance = 160;
            this.splitContainer1.TabIndex = 123;
            // 
            // UserLabel
            // 
            this.UserLabel.AutoSize = true;
            this.UserLabel.Location = new System.Drawing.Point(10, 65);
            this.UserLabel.Name = "UserLabel";
            this.UserLabel.Size = new System.Drawing.Size(69, 13);
            this.UserLabel.TabIndex = 136;
            this.UserLabel.Text = "Current User:";
            // 
            // Total_Records
            // 
            this.Total_Records.AutoSize = true;
            this.Total_Records.Location = new System.Drawing.Point(88, 92);
            this.Total_Records.Name = "Total_Records";
            this.Total_Records.Size = new System.Drawing.Size(13, 13);
            this.Total_Records.TabIndex = 120;
            this.Total_Records.Text = "0";
            this.Total_Records.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Total_Label
            // 
            this.Total_Label.AutoSize = true;
            this.Total_Label.Location = new System.Drawing.Point(10, 92);
            this.Total_Label.Name = "Total_Label";
            this.Total_Label.Size = new System.Drawing.Size(80, 13);
            this.Total_Label.TabIndex = 119;
            this.Total_Label.Text = "Total Records: ";
            // 
            // Input_1
            // 
            this.Input_1.Location = new System.Drawing.Point(715, 31);
            this.Input_1.Name = "Input_1";
            this.Input_1.ReadOnly = true;
            this.Input_1.Size = new System.Drawing.Size(235, 20);
            this.Input_1.TabIndex = 4;
            this.toolTip1.SetToolTip(this.Input_1, "Enter query option");
            this.Input_1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Input_1_KeyDown);
            // 
            // View_All
            // 
            this.View_All.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.View_All.Location = new System.Drawing.Point(191, 21);
            this.View_All.Name = "View_All";
            this.View_All.Size = new System.Drawing.Size(96, 38);
            this.View_All.TabIndex = 2;
            this.View_All.Text = "View All Practices";
            this.toolTip1.SetToolTip(this.View_All, "View all");
            this.View_All.UseVisualStyleBackColor = true;
            this.View_All.Click += new System.EventHandler(this.View_All_Click);
            // 
            // Date_1
            // 
            this.Date_1.Location = new System.Drawing.Point(715, 32);
            this.Date_1.Name = "Date_1";
            this.Date_1.Size = new System.Drawing.Size(235, 20);
            this.Date_1.TabIndex = 4;
            this.toolTip1.SetToolTip(this.Date_1, "Enter query option");
            this.Date_1.Visible = false;
            // 
            // dropDown1Label
            // 
            this.dropDown1Label.AutoSize = true;
            this.dropDown1Label.Location = new System.Drawing.Point(196, 14);
            this.dropDown1Label.Name = "dropDown1Label";
            this.dropDown1Label.Size = new System.Drawing.Size(91, 13);
            this.dropDown1Label.TabIndex = 117;
            this.dropDown1Label.Text = "Currently Viewing:";
            this.dropDown1Label.Visible = false;
            // 
            // Dropdown_3
            // 
            this.Dropdown_3.BackColor = System.Drawing.SystemColors.Window;
            this.Dropdown_3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Dropdown_3.FormattingEnabled = true;
            this.Dropdown_3.IntegralHeight = false;
            this.Dropdown_3.ItemHeight = 13;
            this.Dropdown_3.Location = new System.Drawing.Point(715, 30);
            this.Dropdown_3.Name = "Dropdown_3";
            this.Dropdown_3.Size = new System.Drawing.Size(235, 21);
            this.Dropdown_3.TabIndex = 111;
            this.toolTip1.SetToolTip(this.Dropdown_3, "Enter query option");
            this.Dropdown_3.Visible = false;
            // 
            // Dropdown_1
            // 
            this.Dropdown_1.BackColor = System.Drawing.SystemColors.Window;
            this.Dropdown_1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Dropdown_1.FormattingEnabled = true;
            this.Dropdown_1.IntegralHeight = false;
            this.Dropdown_1.Items.AddRange(new object[] {
            "All Patients",
            "Current Patients",
            "Non Current Patients"});
            this.Dropdown_1.Location = new System.Drawing.Point(156, 31);
            this.Dropdown_1.Name = "Dropdown_1";
            this.Dropdown_1.Size = new System.Drawing.Size(171, 21);
            this.Dropdown_1.TabIndex = 1;
            this.toolTip1.SetToolTip(this.Dropdown_1, "Choose currently viewing option");
            this.Dropdown_1.Visible = false;
            // 
            // Back
            // 
            this.Back.Image = ((System.Drawing.Image)(resources.GetObject("Back.Image")));
            this.Back.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Back.Location = new System.Drawing.Point(13, 28);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(74, 25);
            this.Back.TabIndex = 9;
            this.Back.Text = "Back";
            this.Back.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.Back, "Back to previous screen");
            this.Back.UseVisualStyleBackColor = true;
            this.Back.Click += new System.EventHandler(this.Back_Click);
            // 
            // Search_Label2
            // 
            this.Search_Label2.AutoSize = true;
            this.Search_Label2.Location = new System.Drawing.Point(633, 64);
            this.Search_Label2.Name = "Search_Label2";
            this.Search_Label2.Size = new System.Drawing.Size(0, 13);
            this.Search_Label2.TabIndex = 115;
            this.Search_Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Create_Excel
            // 
            this.Create_Excel.Image = ((System.Drawing.Image)(resources.GetObject("Create_Excel.Image")));
            this.Create_Excel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Create_Excel.Location = new System.Drawing.Point(1173, 32);
            this.Create_Excel.Name = "Create_Excel";
            this.Create_Excel.Size = new System.Drawing.Size(178, 25);
            this.Create_Excel.TabIndex = 7;
            this.Create_Excel.Text = "Export Current View to Excel";
            this.Create_Excel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.Create_Excel, "Export current view to excel");
            this.Create_Excel.UseVisualStyleBackColor = true;
            this.Create_Excel.Click += new System.EventHandler(this.Create_Excel_Click);
            // 
            // Date_2
            // 
            this.Date_2.Location = new System.Drawing.Point(715, 58);
            this.Date_2.Name = "Date_2";
            this.Date_2.Size = new System.Drawing.Size(235, 20);
            this.Date_2.TabIndex = 5;
            this.toolTip1.SetToolTip(this.Date_2, "Enter query option");
            this.Date_2.Visible = false;
            // 
            // Dropdown_2
            // 
            this.Dropdown_2.BackColor = System.Drawing.SystemColors.Window;
            this.Dropdown_2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Dropdown_2.FormattingEnabled = true;
            this.Dropdown_2.IntegralHeight = false;
            this.Dropdown_2.ItemHeight = 13;
            this.Dropdown_2.Location = new System.Drawing.Point(450, 31);
            this.Dropdown_2.Name = "Dropdown_2";
            this.Dropdown_2.Size = new System.Drawing.Size(171, 21);
            this.Dropdown_2.TabIndex = 3;
            this.toolTip1.SetToolTip(this.Dropdown_2, "Choose query option");
            this.Dropdown_2.SelectionChangeCommitted += new System.EventHandler(this.Dropdown_2_SelectionChangeCommitted);
            // 
            // Search
            // 
            this.Search.Enabled = false;
            this.Search.Image = ((System.Drawing.Image)(resources.GetObject("Search.Image")));
            this.Search.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Search.Location = new System.Drawing.Point(956, 31);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(88, 25);
            this.Search.TabIndex = 6;
            this.Search.Text = "Search";
            this.Search.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.Search, "Search query option");
            this.Search.UseVisualStyleBackColor = true;
            this.Search.Click += new System.EventHandler(this.Search_Click);
            // 
            // dropDown2Label
            // 
            this.dropDown2Label.AutoSize = true;
            this.dropDown2Label.Location = new System.Drawing.Point(488, 14);
            this.dropDown2Label.Name = "dropDown2Label";
            this.dropDown2Label.Size = new System.Drawing.Size(94, 13);
            this.dropDown2Label.TabIndex = 108;
            this.dropDown2Label.Text = "Search Patient by:";
            // 
            // Search_Label
            // 
            this.Search_Label.AutoSize = true;
            this.Search_Label.Location = new System.Drawing.Point(633, 35);
            this.Search_Label.Name = "Search_Label";
            this.Search_Label.Size = new System.Drawing.Size(0, 13);
            this.Search_Label.TabIndex = 110;
            this.Search_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Records_Gridview
            // 
            this.Records_Gridview.AllowUserToAddRows = false;
            this.Records_Gridview.AllowUserToDeleteRows = false;
            this.Records_Gridview.AllowUserToOrderColumns = true;
            this.Records_Gridview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Records_Gridview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Records_Gridview.Location = new System.Drawing.Point(0, 0);
            this.Records_Gridview.Name = "Records_Gridview";
            this.Records_Gridview.ReadOnly = true;
            this.Records_Gridview.Size = new System.Drawing.Size(1354, 569);
            this.Records_Gridview.TabIndex = 8;
            // 
            // View_Records
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1354, 733);
            this.Controls.Add(this.splitContainer1);
            this.Name = "View_Records";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View Patients";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.View_Patients_FormClosed);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Records_Gridview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label Total_Records;
        private System.Windows.Forms.Label Total_Label;
        private System.Windows.Forms.TextBox Input_1;
        private System.Windows.Forms.Button View_All;
        private System.Windows.Forms.DateTimePicker Date_1;
        private System.Windows.Forms.Label dropDown1Label;
        private System.Windows.Forms.ComboBox Dropdown_3;
        private System.Windows.Forms.ComboBox Dropdown_1;
        private System.Windows.Forms.Button Back;
        private System.Windows.Forms.Label Search_Label2;
        private System.Windows.Forms.Button Create_Excel;
        private System.Windows.Forms.DateTimePicker Date_2;
        private System.Windows.Forms.ComboBox Dropdown_2;
        private System.Windows.Forms.Button Search;
        private System.Windows.Forms.Label dropDown2Label;
        private System.Windows.Forms.Label Search_Label;
        private System.Windows.Forms.DataGridView Records_Gridview;
        private System.Windows.Forms.Label UserLabel;
        private System.Windows.Forms.ToolTip toolTip1;

    }
}