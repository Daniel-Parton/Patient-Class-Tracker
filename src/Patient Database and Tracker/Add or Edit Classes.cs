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
    public partial class Add_Edit_Class : Form
    {
        public Add_Edit_Class()
        {
            InitializeComponent();
            Global.testConnection(this);            //Tests database connection
            UserLabel.Text = "Current User: " + 
                Global.userFirstName + 
                " " + Global.userLastName;          //Sets user label

            Date_1.Value = DateTime.Now.Date;       //Sets date to current date
            Date_1.MaxDate = DateTime.Now.Date;     //Doesn't allow user to choose a date later than today
            setSortedButtonText();                  //Sets sorted button text
        }

        //********************Non Event Methods********************

        //Sets the text for the sort list button based on the global variable
        private void setSortedButtonText()
        {
            if (Global.sortDropdown == Global.ID)
            {
                Sort_Lists.Text = "Sort Lists by Name";
            }
            else if (Global.sortDropdown == Global.NAME)
            {
                Sort_Lists.Text = "Sort Lists by ID";
            }
        }
        //Used to sort lists by name or ID
        private void Sort_Lists_Click(object sender, EventArgs e)
        {
            if (Global.sortDropdown == Global.ID)
            {
                Global.sortDropdown = Global.NAME;
            }
            else if (Global.sortDropdown == Global.NAME)
            {
                Global.sortDropdown = Global.ID;
            }
            setSortedButtonText();
        }

        //********************Event Methods********************

        //Loads eitther Initial_Screen or closes form
        private void Back_Click(object sender, EventArgs e)
        {
            Global.editingExistingClass = false;
            //If addnewTime is open then the form will just close as addnew time form will be visible in the background
            if (Global.addNewTimeOpen == true || Global.addEditTimeOpen == true)
            {
                this.Close();
            }
            else
            {
                this.Hide();
                Initial_Screen initial = new Initial_Screen();
                initial.ShowDialog();
                initial.Focus();
            }
        }

        //Enables controls to add a new class and if in edit session closes it
        private void Add_Class_Click(object sender, EventArgs e)
        {
            Global.editingExistingClass = false;
            Input_1.ReadOnly = false;
            Save.Enabled = true;
            Input_1.Text = null;
            Dropdown_1.DataSource = null;
            PatientsClassDGV.DataSource = null;
            PatientsClassWaitingDGV.DataSource = null;
            Add_Patient.Enabled = false;
            Add_Patient_Waiting.Enabled = false;
            Input_1.Focus();
        }

        //Creates new class or updates class name 
        private void Save_Click(object sender, EventArgs e)
        {
            bool saveSuccess = Class_Database.SaveClass(Input_1, Dropdown_1);
            if (saveSuccess == true)
            {
                Input_1.ReadOnly = true;
                Dropdown_1.DataSource = null;
                Save.Enabled = false;
                Add_Class.Focus();

            }
        }

        //Populates dropdown with list of classes
        private void Dropdown_1_DropDown(object sender, EventArgs e)
        {
            Class_Database.PopulateClassDropdown(Dropdown_1);
        }

        //Enables controls to add patients to class and populates existing class patients
        private void Dropdown_1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Global.editingExistingClass = true;
            Input_1.ReadOnly = false;
            Save.Enabled = true;
            Delete_Class.Enabled = true;
            Dropdown_2.Enabled = true;
            Input_1.Text = Dropdown_1.SelectedValue.ToString();
            Class_Database.PopulatePatient_ClassDGV(Dropdown_1, PatientsClassDGV, PatientsClassWaitingDGV);
        }

        //Adds patient selected in dropdown_2 to the selected class
        private void Add_Patient_Click(object sender, EventArgs e)
        {
            bool saveSuccess = Class_Database.SavePatient_Class(Dropdown_1, Dropdown_2, Date_1);
            if (saveSuccess == true)
            {
                Class_Database.PopulatePatient_ClassDGV(Dropdown_1, PatientsClassDGV, PatientsClassWaitingDGV);
                Dropdown_2.DataSource = null;
                Date_1.Enabled = false;
                Add_Patient.Enabled = false;
                Add_Patient_Waiting.Enabled = false;
                Dropdown_2.Focus();
            }
        }

        //Shows lists of patients.  Only shows current patients who are not already in the current class or waiting list
        private void Dropdown_2_DropDown(object sender, EventArgs e)
        {
            Patient_Database.PopulatePatientDropdown(Dropdown_1, Dropdown_2);
        }

        //Enables controls to add the patient which has been selected
        private void Dropdown_2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Add_Patient.Enabled = true;
            Add_Patient_Waiting.Enabled = true;
            Date_1.Enabled = true;
        }

        //Adds patient to the waiting list of the selected class
        private void Add_Patient_Waiting_Click(object sender, EventArgs e)
        {
            bool saveSuccess = Class_Database.SavePatient_ClassWaitingList(Dropdown_1, Dropdown_2);
            if (saveSuccess == true)
            {
                Class_Database.PopulatePatient_ClassDGV(Dropdown_1, PatientsClassDGV, PatientsClassWaitingDGV);
                Dropdown_2.DataSource = null;
                Date_1.Enabled = false;
                Add_Patient.Enabled = false;
                Add_Patient_Waiting.Enabled = false;
                Dropdown_2.Focus();
            }
        }

        //The following 2 event methods clears the autoselection which occurs when a datasource is set to a DGV
        private void PatientsClassDGV_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            PatientsClassDGV.ClearSelection();
        }

        private void PatientsClassWaitingDGV_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            PatientsClassWaitingDGV.ClearSelection();
        }

        //Deletes patient from class
        private void Remove_Click(object sender, EventArgs e)
        {
            bool deleteSuccess = Class_Database.DeletePatient_Class(Dropdown_1, PatientsClassDGV);
            if (deleteSuccess == true)
            {
                Class_Database.PopulatePatient_ClassDGV(Dropdown_1, PatientsClassDGV, PatientsClassWaitingDGV);
            }
        }

        //Deletes patient from waiting list
        private void Remove_Waiting_Click(object sender, EventArgs e)
        {
            bool deleteSuccess = Class_Database.DeletePatient_ClassWaitingList(Dropdown_1, PatientsClassWaitingDGV);
            if (deleteSuccess == true)
            {
                Class_Database.PopulatePatient_ClassDGV(Dropdown_1, PatientsClassDGV, PatientsClassWaitingDGV);
            }
        }

        //Only allows user to select from one datagridview at a time
        private void PatientsClassDGV_SelectionChanged(object sender, EventArgs e)
        {
            PatientsClassWaitingDGV.ClearSelection();
        }
        //Only allows user to select from one datagridview at a time
        private void PatientsClassWaitingDGV_SelectionChanged(object sender, EventArgs e)
        {
            PatientsClassDGV.ClearSelection();
        }

        //Deletes patient from waiting list and adds patient to class
        private void Move_Click(object sender, EventArgs e)
        {
            bool moveSuccess = Class_Database.MoveWaitingPatient(PatientsClassWaitingDGV, Dropdown_1, Date_1);
            if (moveSuccess == true)
            {
                Class_Database.PopulatePatient_ClassDGV(Dropdown_1, PatientsClassDGV, PatientsClassWaitingDGV);
            }
        }

        //Deletes class.  When a class is deleted it deletes the class, all Patient_Class records and CLass Time Records
        private void Delete_Class_Click(object sender, EventArgs e)
        {
            bool deleteSuccess = Class_Database.DeleteClass(Dropdown_1);
            //When delete has finished it disables all other controls and deselects everything
            if (deleteSuccess == true)
            {
                PatientsClassDGV.DataSource = null;
                PatientsClassWaitingDGV.DataSource = null;
                Input_1.ReadOnly = true;
                Input_1.Text = null;
                Dropdown_1.DataSource = null;
                Dropdown_2.DataSource = null;
                Dropdown_2.Enabled = false;
                Date_1.Enabled = false;
                Add_Patient.Enabled = false;
                Delete_Class.Enabled = false;
                Save.Enabled = false;
                Add_Patient_Waiting.Enabled = false;
                Dropdown_2.Focus();
            }
        }

        //Loads Edit_Class_Start_Time to change the start date of the selected patient
        private void Edit_Start_Time_Click(object sender, EventArgs e)
        {
            //Error message if there are no patients in the class
            if (PatientsClassDGV.Rows.Count == 0)
            {
                MessageBox.Show("There are no patients currently in this class", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Checks if the user has selected a patient from the class
            if (PatientsClassDGV.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a patient from the class grid", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Sets class name to global variable to be used in next form
            Global.changeStartDateClass = Dropdown_1.SelectedValue.ToString();
            //Gets Patient ID
            int cellIndex = PatientsClassDGV.SelectedCells[0].RowIndex;
            DataGridViewCell cellCollection = PatientsClassDGV.Rows[cellIndex].Cells[0];
            string patientID = "ID: " + cellCollection.Value.ToString();
            Global.changeStartDatePatientID = cellCollection.Value.ToString();
            //Gets First Name
            cellCollection = PatientsClassDGV.Rows[cellIndex].Cells[1];
            string firstName = " *** Name: " + cellCollection.Value.ToString();
            //Gets Last Name
            cellCollection = PatientsClassDGV.Rows[cellIndex].Cells[2];
            string lastName = " " + cellCollection.Value.ToString();
            //Sets Patient name and id to global variable to be used in next form
            Global.changeStartDatePatient = patientID + firstName + lastName;
            //Gets Current Start Date
            cellCollection = PatientsClassDGV.Rows[cellIndex].Cells[3];
            string currentStartDate = cellCollection.Value.ToString();
            //Sets current start date to global variable to be used in next form
            Global.changeStartDateCurrentDate = currentStartDate;

            //Load the change start date form
            Edit_Class_Start_Time editStart = new Edit_Class_Start_Time();
            editStart.ShowDialog();
            editStart.Focus();
        }

        //Used if the edit start date is successful
        private void Add_Edit_Class_Activated(object sender, EventArgs e)
        {
            if (Input_1.ReadOnly == false)
            {
                Class_Database.PopulatePatient_ClassDGV(Dropdown_1, PatientsClassDGV, PatientsClassWaitingDGV);
            }
        }

        //Closes all forms if all other forms are hidden
        private void Add_or_Edit_Group_Class_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Global.addNewTimeOpen == false && Global.addEditTimeOpen == false)
            {
                Application.Exit();
            }
        }
    }
}
