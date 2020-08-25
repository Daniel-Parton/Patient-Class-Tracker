using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using Finisar.SQLite;

namespace Patient_Database_and_Tracker
{
    public partial class Add_Edit_Consultation_1 : Form
    {


        //********************Class Constructor********************

        public Add_Edit_Consultation_1()
        {
            InitializeComponent();
            UserLabel.Text = "Current User: " + 
                Global.userFirstName +
                " " + Global.userLastName;                 //Sets user label 
            Global.testConnection(this);                   //Tests database connection
            SetSortedButtonText();                         //Sets the sort by button text
            Global.editingExistingConsulation = false;     //If an editing session is open it reverts to false
        }

        //********************Non Event Methods********************

        //Sets the text for the sort list button based on the global variable
        private void SetSortedButtonText()
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

        //********************Database Event Methods********************

        //Shows list of all existing patients
        private void Dropdown_1_DropDown(object sender, EventArgs e)
        {
            Patient_Database.PopulatePatientDropdown(Dropdown_1);
        }

        //When a user clicks a selection from the dropdown box all the values of the selected patient will
        //populate the fields.
        private void Dropdown_1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Patient_Database.PatientSelectedConsultation1(Dropdown_1, Input_1, Input_2, Input_3,
                Input_4, Input_5);
            Consultation_Database.PopulateConsultationDataGridView(Dropdown_1, Existing_Consultations);    //Poplulates existing consultations if any
            Dropdown_2.Enabled = true;      //Allows user to choose time period
            if(Dropdown_2.SelectedIndex != -1)
            {
                Add_Consultation.Enabled = true;
            }
        }

        //********************Other Form and Object Event Methods********************

        //Dropsdown the comboxbox when the down key is pressed
        private void Dropdown_1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                Dropdown_1.DroppedDown = true;
            }
        }

        //Dropsdown the comboxbox when the down key is pressed
        private void Dropdown_2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                Dropdown_2.DroppedDown = true;
            }
        }

        //Return to the previous screen
        private void Back_Click(object sender, EventArgs e)
        {
            Global.addConsultationOpen = false;
            this.Hide();
            Initial_Screen initial = new Initial_Screen();
            initial.ShowDialog();
            initial.Focus();
        }

        //Opens up form to add new patient
        private void Add_Dropdown_1_Click(object sender, EventArgs e)
        {
            Global.addConsultationOpen = true;
            Add_Edit_Patient addPatient = new Add_Edit_Patient();
            addPatient.ShowDialog();
            addPatient.Focus();
        }

        //Changes the way the lists are sorted
        private void Sort_Lists_Button_Click(object sender, EventArgs e)
        {
            if (Global.sortDropdown == Global.ID)
            {
                Global.sortDropdown = Global.NAME;
            }
            else if (Global.sortDropdown == Global.NAME)
            {
                Global.sortDropdown = Global.ID;
            }
            SetSortedButtonText();
        }

        //Loads the next form to add a new consultation
        private void Add_Consultation_Click(object sender, EventArgs e)
        {
            if (Dropdown_1.SelectedIndex == -1 || Dropdown_2.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a patient and a time period", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Global.consTimePeriod = Dropdown_2.SelectedItem.ToString();
            this.Hide();
            Add_Edit_Consultation_2 addCons2 = new Add_Edit_Consultation_2();
            addCons2.ShowDialog();
            addCons2.Focus();
        }

        //Opens the Add_Edit_Consultation_2 in an edit session
        private void Edit_Consultation_Click(object sender, EventArgs e)
        {
            //Checks if there are any consultations for the patient
            if (Existing_Consultations.Rows.Count == 0)
            {
                MessageBox.Show("There are no existing conultations for this patient", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Checks if the user has selected a consultation
            if (Existing_Consultations.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a consultation from the List To edit", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Getting the CONS_ID_Number, date and time period for the selected existing consultation
            int cellIndex = Existing_Consultations.SelectedCells[0].RowIndex;
            DataGridViewCell cellCollection = Existing_Consultations.Rows[cellIndex].Cells[0];
            Global.consID = cellCollection.Value.ToString();

            cellCollection = Existing_Consultations.Rows[cellIndex].Cells[1];
            Global.consTimePeriod = cellCollection.Value.ToString();

            cellCollection = Existing_Consultations.Rows[cellIndex].Cells[2];
            string tempDate = cellCollection.Value.ToString();
            Global.consDate = Convert.ToDateTime(tempDate);

            Global.editingExistingConsulation = true;

            //Loads Add_Edit_Consultation_2
            this.Hide();
            Add_Edit_Consultation_2 addCons2 = new Add_Edit_Consultation_2();
            addCons2.ShowDialog();
            addCons2.Focus();
        }

        //Deselects DGV when databinding complete
        private void Existing_Consultations_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Existing_Consultations.ClearSelection();
        }

        //Deletes selected consultation
        private void Delete_Click(object sender, EventArgs e)
        {
            bool deleteSuccessful = Consultation_Database.DeleteConsultation(Dropdown_1, Existing_Consultations); //Deletes Consultation
            //If user chooses to cancel delete or there is no consultations to delete it stops the method
            if(deleteSuccessful == false)
            {
                return;
            }
            Consultation_Database.PopulateConsultationDataGridView(Dropdown_1, Existing_Consultations); //Refreshes existing consultations
        }

        //Allows user to add new consultation
        private void Dropdown_2_SelectedValueChanged(object sender, EventArgs e)
        {
            Add_Consultation.Enabled = true;
        }

        //Closes all forms if this form is closed
        private void Add_or_Edit_Patient_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}