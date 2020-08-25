using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Patient_Database_and_Tracker
{

    public partial class View_Records : Form
    {
        public View_Records()
        {
            InitializeComponent();
            Global.testConnection(this);
            Dropdown_1.SelectedIndex = 0;
            Date_1.Value = DateTime.Now;
            Date_2.Value = DateTime.Now;

            //Sets the look of the form depending on what button was clicked in the Initial Screen
            switch (Global.view)
            {
                case "View Patients":
                    this.Text = "View Patients";
                    dropDown1Label.Visible = true;
                    Dropdown_1.Visible = true;
                    View_All.Location = new Point(190, 58);
                    View_All.Size = new Size(101, 25);
                    View_All.Text = "View All Patients";
                    dropDown2Label.Text = "Search Patient by:";
                    string[] patientSearchItems = new string[17] {"ID Number", "First Name", "Last Name", "General Practice", "Reffering GP ID", "Address",
                        "Suburb", "Post Code", "State", "Home Phone", "Mobile Phone", "Email", "Date Reffered",
                        "Date of Birth", "Gender", "GP Letter Sent", "Class Attendance"};
                    Dropdown_2.Items.AddRange(patientSearchItems);

                    break;

                case "View Consultations":
                    this.Text = "View Consultations";
                    dropDown1Label.Visible = true;
                    Dropdown_1.Visible = true;
                    View_All.Location = new Point(190, 58);
                    View_All.Size = new Size(101, 38);
                    View_All.Text = "View All Consultations";
                    dropDown2Label.Text = "Search Consultations by:";
                    dropDown2Label.Location = new Point(477, 9);
                    string[] consSearchItems = new string[7] {"Consultation ID", "Patient ID", "Patient First Name",
                        "Patient Last Name", "Consultant Full Name", "Time Period", "Consultation Date"};
                    Dropdown_2.Items.AddRange(consSearchItems);
                    break;

                case "View Reffering GPs":
                    this.Text = "View Reffering GPs";
                    View_All.Text = "View All Reffering GPs";
                    dropDown2Label.Text = "Search Reffering GP by:";
                    View_All.Location = new Point(190, 18);
                    View_All.Size = new Size(96, 38);
                    dropDown2Label.Location = new Point(477, 9);
                    string[] refgpSearchItems = new string[6] {"ID Number", "First Name", "Last Name", "Practice Name", "Direct Number",
                        "Email"};
                    Dropdown_2.Items.AddRange(refgpSearchItems);
                    Records_Gridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    break;

                case "View Practices":
                    this.Text = "View Practices";
                    View_All.Text = "View All Practices";
                    dropDown2Label.Text = "Search Practices by:";
                    View_All.Location = new Point(190, 18);
                    View_All.Size = new Size(96, 38);
                    string[] pracSearchItems = new string[7] {"Practice Name", "Address", "Suburb", "Post Code", "State",
                        "Phone", "Fax"};
                    Dropdown_2.Items.AddRange(pracSearchItems);
                    Records_Gridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    break;
            }
        }

        //Returns to intitial screen
        private void Back_Click(object sender, EventArgs e)
        {
            this.Hide();
            Initial_Screen initial = new Initial_Screen();
            initial.ShowDialog();
            initial.Focus();
        }

        //Exports the current view to am excel workbook
        private void Create_Excel_Click(object sender, EventArgs e)
        {
            Global.createExcelFromDataGridView(Records_Gridview);
        }

        //Sets the search input box and labels
        private void Dropdown_2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Search.Enabled = true;          //Allows user to search once a selection is made
            switch(Global.view)
            {
                case "View Patients":
                    Patient_Database.Show_HideSearchOptions(Dropdown_2, Dropdown_3, Input_1, Date_1,
                        Date_2, Search_Label, Search_Label2);
                    break;

                case "View Consultations":
                    Consultation_Database.Show_HideSearchOptions(Dropdown_2, Dropdown_3, Input_1, Date_1,
                        Date_2, Search_Label, Search_Label2);
                    break;

                case "View Reffering GPs":
                    Reffering_GP_Database.Show_HideSearchOptions(Dropdown_2, Input_1, Search_Label);
                    break;

                case "View Practices":
                    Practice_Database.Show_HideSearchOptions(Dropdown_2, Input_1, Search_Label);
                    break;
            }
        }

        private void Search_Click(object sender, EventArgs e)
        {
            switch (Global.view)
            {
                case "View Patients":
                    Patient_Database.ViewSearchedRecords(Records_Gridview, Input_1, Dropdown_1, Dropdown_2,
                        Dropdown_3, Date_1, Date_2);
                    break;

                case "View Consultations":
                    Consultation_Database.ViewSearchedRecords(Records_Gridview, Input_1, Dropdown_1, Dropdown_2,
                        Dropdown_3, Date_1, Date_2);
                    break;

                case "View Reffering GPs":
                    Reffering_GP_Database.ViewSearchedRecords(Records_Gridview, Input_1, Dropdown_2);
                    break;

                case "View Practices":
                    Practice_Database.ViewSearchedRecords(Records_Gridview, Input_1, Dropdown_2);
                    break;
            }
            Total_Records.Text = Records_Gridview.Rows.Count.ToString();
        }

        //Views all of the records determined by the dropdown1 selection
        private void View_All_Click(object sender, EventArgs e)
        {
            switch (Global.view)
            {
                case "View Patients":
                    Patient_Database.ViewAllRecords(Dropdown_1, Records_Gridview);
                    break;

                case "View Consultations":
                    Consultation_Database.ViewAllRecords(Dropdown_1, Records_Gridview);
                    break;

                case "View Reffering GPs":
                    Reffering_GP_Database.ViewAllRecords(Records_Gridview);
                    break;

                case "View Practices":
                    Practice_Database.ViewAllRecords(Records_Gridview);
                    break;
            }
            Total_Records.Text = Records_Gridview.Rows.Count.ToString();
        }

        private void View_Patients_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Input_1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Search_Click(this, EventArgs.Empty);
            }
        }
    }
}
