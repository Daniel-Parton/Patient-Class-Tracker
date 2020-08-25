namespace Patient_Database_and_Tracker
{
    partial class Add_New_Time
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Add_New_Time));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Cancel = new System.Windows.Forms.Button();
            this.Create_Time = new System.Windows.Forms.Button();
            this.Dropdown_1 = new System.Windows.Forms.ComboBox();
            this.Dropdown_2 = new System.Windows.Forms.ComboBox();
            this.Class_Time_Groupbox = new System.Windows.Forms.GroupBox();
            this.Existing_Times = new System.Windows.Forms.DataGridView();
            this.Dropdown_3 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Add_Class = new System.Windows.Forms.Button();
            this.UserLabel = new System.Windows.Forms.Label();
            this.Edit_Time = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Class_Time_Groupbox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Existing_Times)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Start:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Finish:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(80, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Please pick a ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(69, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "start and finish time";
            // 
            // Cancel
            // 
            this.Cancel.Image = ((System.Drawing.Image)(resources.GetObject("Cancel.Image")));
            this.Cancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Cancel.Location = new System.Drawing.Point(11, 335);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(73, 25);
            this.Cancel.TabIndex = 6;
            this.Cancel.TabStop = false;
            this.Cancel.Text = "Cancel";
            this.Cancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.Cancel, "Back to previous screen");
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // Create_Time
            // 
            this.Create_Time.Image = ((System.Drawing.Image)(resources.GetObject("Create_Time.Image")));
            this.Create_Time.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Create_Time.Location = new System.Drawing.Point(97, 335);
            this.Create_Time.Name = "Create_Time";
            this.Create_Time.Size = new System.Drawing.Size(125, 25);
            this.Create_Time.TabIndex = 5;
            this.Create_Time.Text = "Create Class Time";
            this.Create_Time.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.Create_Time, "Create class time");
            this.Create_Time.UseVisualStyleBackColor = true;
            this.Create_Time.Click += new System.EventHandler(this.Create_Time_Click);
            // 
            // Dropdown_1
            // 
            this.Dropdown_1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Dropdown_1.FormattingEnabled = true;
            this.Dropdown_1.ItemHeight = 13;
            this.Dropdown_1.Location = new System.Drawing.Point(97, 62);
            this.Dropdown_1.Name = "Dropdown_1";
            this.Dropdown_1.Size = new System.Drawing.Size(69, 21);
            this.Dropdown_1.TabIndex = 1;
            this.toolTip1.SetToolTip(this.Dropdown_1, "Choose a start time");
            this.Dropdown_1.SelectionChangeCommitted += new System.EventHandler(this.Dropdown_1_SelectionChangeCommitted);
            // 
            // Dropdown_2
            // 
            this.Dropdown_2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Dropdown_2.FormattingEnabled = true;
            this.Dropdown_2.ItemHeight = 13;
            this.Dropdown_2.Location = new System.Drawing.Point(97, 89);
            this.Dropdown_2.Name = "Dropdown_2";
            this.Dropdown_2.Size = new System.Drawing.Size(69, 21);
            this.Dropdown_2.TabIndex = 2;
            this.toolTip1.SetToolTip(this.Dropdown_2, "Choose a finish time");
            // 
            // Class_Time_Groupbox
            // 
            this.Class_Time_Groupbox.Controls.Add(this.Existing_Times);
            this.Class_Time_Groupbox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Class_Time_Groupbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Class_Time_Groupbox.Location = new System.Drawing.Point(258, 21);
            this.Class_Time_Groupbox.Name = "Class_Time_Groupbox";
            this.Class_Time_Groupbox.Size = new System.Drawing.Size(160, 366);
            this.Class_Time_Groupbox.TabIndex = 138;
            this.Class_Time_Groupbox.TabStop = false;
            // 
            // Existing_Times
            // 
            this.Existing_Times.AllowUserToAddRows = false;
            this.Existing_Times.AllowUserToDeleteRows = false;
            this.Existing_Times.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Existing_Times.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Existing_Times.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Existing_Times.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Existing_Times.DefaultCellStyle = dataGridViewCellStyle2;
            this.Existing_Times.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Existing_Times.Enabled = false;
            this.Existing_Times.Location = new System.Drawing.Point(3, 16);
            this.Existing_Times.Name = "Existing_Times";
            this.Existing_Times.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Existing_Times.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.Existing_Times.RowHeadersVisible = false;
            this.Existing_Times.Size = new System.Drawing.Size(154, 347);
            this.Existing_Times.TabIndex = 0;
            this.toolTip1.SetToolTip(this.Existing_Times, "Existing class times");
            this.Existing_Times.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.Existing_Times_DataBindingComplete);
            // 
            // Dropdown_3
            // 
            this.Dropdown_3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Dropdown_3.FormattingEnabled = true;
            this.Dropdown_3.ItemHeight = 13;
            this.Dropdown_3.Location = new System.Drawing.Point(51, 214);
            this.Dropdown_3.Name = "Dropdown_3";
            this.Dropdown_3.Size = new System.Drawing.Size(132, 21);
            this.Dropdown_3.TabIndex = 3;
            this.toolTip1.SetToolTip(this.Dropdown_3, "Choose a class");
            this.Dropdown_3.DropDown += new System.EventHandler(this.Dropdown_3_DropDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(48, 190);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(135, 13);
            this.label5.TabIndex = 140;
            this.label5.Text = "Choose a class for this time";
            // 
            // Add_Class
            // 
            this.Add_Class.Image = ((System.Drawing.Image)(resources.GetObject("Add_Class.Image")));
            this.Add_Class.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Add_Class.Location = new System.Drawing.Point(59, 242);
            this.Add_Class.Name = "Add_Class";
            this.Add_Class.Size = new System.Drawing.Size(117, 25);
            this.Add_Class.TabIndex = 4;
            this.Add_Class.Text = "Add New Class";
            this.Add_Class.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.Add_Class, "Add/edit classes");
            this.Add_Class.UseVisualStyleBackColor = true;
            this.Add_Class.Click += new System.EventHandler(this.Add_Class_Click);
            // 
            // UserLabel
            // 
            this.UserLabel.AutoSize = true;
            this.UserLabel.Location = new System.Drawing.Point(8, 379);
            this.UserLabel.Name = "UserLabel";
            this.UserLabel.Size = new System.Drawing.Size(69, 13);
            this.UserLabel.TabIndex = 142;
            this.UserLabel.Text = "Current User:";
            // 
            // Edit_Time
            // 
            this.Edit_Time.Image = ((System.Drawing.Image)(resources.GetObject("Edit_Time.Image")));
            this.Edit_Time.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Edit_Time.Location = new System.Drawing.Point(109, 335);
            this.Edit_Time.Name = "Edit_Time";
            this.Edit_Time.Size = new System.Drawing.Size(113, 25);
            this.Edit_Time.TabIndex = 143;
            this.Edit_Time.Text = "Submit Change";
            this.Edit_Time.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.Edit_Time, "Confirm edit");
            this.Edit_Time.UseVisualStyleBackColor = true;
            this.Edit_Time.Visible = false;
            this.Edit_Time.Click += new System.EventHandler(this.Edit_Time_Click);
            // 
            // Add_New_Time
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 401);
            this.Controls.Add(this.UserLabel);
            this.Controls.Add(this.Add_Class);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Dropdown_3);
            this.Controls.Add(this.Class_Time_Groupbox);
            this.Controls.Add(this.Dropdown_2);
            this.Controls.Add(this.Dropdown_1);
            this.Controls.Add(this.Create_Time);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Edit_Time);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Add_New_Time";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add New Time";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Add_New_Time_FormClosed);
            this.Class_Time_Groupbox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Existing_Times)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button Create_Time;
        private System.Windows.Forms.ComboBox Dropdown_1;
        private System.Windows.Forms.ComboBox Dropdown_2;
        private System.Windows.Forms.GroupBox Class_Time_Groupbox;
        private System.Windows.Forms.DataGridView Existing_Times;
        private System.Windows.Forms.ComboBox Dropdown_3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button Add_Class;
        private System.Windows.Forms.Label UserLabel;
        private System.Windows.Forms.Button Edit_Time;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}