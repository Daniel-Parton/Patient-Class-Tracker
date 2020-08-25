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
    public partial class Add_Edit_Patient : Form
    {
        //DDL for Patient
        //"CREATE TABLE [PATIENT]" +
        //"(PAT_ID_Number INTEGER NOT NULL DEFAULT 1 PRIMARY KEY UNIQUE," +
        //"PAT_First_Name TEXT NOT NULL," +
        //"PAT_Last_Name TEXT NOT NULL," +
        //"PAT_Address TEXT," +
        //"PAT_Suburb TEXT," +
        //"PAT_Post_Code TEXT," +
        //"PAT_State TEXT," +
        //"PAT_Home_Phone TEXT," +
        //"PAT_Mobile_Phone TEXT," +
        //"PAT_Email_Address TEXT," +
        //"REFGP_ID_Number INTEGER," +
        //"PAT_Date_Reffered DATE," +
        //"PAT_Date_of_Birth DATE," +
        //PAT_Gender TEXT," +
        //"PAT_Notes TEXT," +
        //"PAT_Current BOOLEAN);"


        //********************Class Constructor********************

        public Add_Edit_Patient()
        {
            InitializeComponent();
            Global.testConnection(this);                //Tests database connection
            UserLabel.Text = "Current User: " +
                Global.userFirstName +
                " " + Global.userLastName;              //Sets user label
            SetSortedButtonText();                      //Sets the sort by button text
            Global.editingExistingPatient = false;      //If an editing session is open it reverts to false
            Date_1.Value = DateTime.Now;
            Date_2.Value = DateTime.Now;
        }

        //********************Non Event Methods********************

        //Used when clciking to add/edit patient and also when the patient details are saved
        //Forces the user to choose to add or edit beofre inputting data
        public void AddOrEditFields(Boolean editable)
        {
            if (editable == true)
            {
                Input_2.ReadOnly = false;
                Input_3.ReadOnly = false;
                Date_1.Enabled = true;
                Date_2.Enabled = true;
                Input_4.ReadOnly = false;
                Input_5.ReadOnly = false;
                Input_6.ReadOnly = false;
                Input_7.ReadOnly = false;
                Input_8.ReadOnly = false;
                Input_9.ReadOnly = false;
                Input_10.ReadOnly = false;
                Dropdown_2.Enabled = true;
                Dropdown_3.Enabled = true;
                Input_11.ReadOnly = false;
                Current_Patient.Enabled = true;
                GP_Letter.Enabled = true;
                
            }
            else
            {
                Input_2.ReadOnly = true;
                Input_3.ReadOnly = true;
                Date_1.Enabled = false;
                Date_2.Enabled = false;
                Input_4.ReadOnly = true;
                Input_5.ReadOnly = true;
                Input_6.ReadOnly = true;
                Input_7.ReadOnly = true;
                Input_8.ReadOnly = true;
                Input_9.ReadOnly = true;
                Input_10.ReadOnly = true;
                Dropdown_2.Enabled = false;
                Dropdown_3.Enabled = false;
                Input_11.ReadOnly = true;
                Current_Patient.Enabled = false;
                GP_Letter.Enabled = false;
            }
        }

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

        //********************Event Methods********************

        //Shows list of all existing patients
        private void Dropdown_1_DropDown(object sender, EventArgs e)
        {
            Patient_Database.PopulatePatientDropdown(Dropdown_1);
        }

        //When a user clicks a selection from the dropdown box all the values of the selected patient will
        //populate the fields.  The program will also know that this is an edit session and not a new patient
        private void Dropdown_1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Patient_Database.PatientSelectedAddEdit(Dropdown_1, Input_1, Date_1, Date_2, Dropdown_2, Input_2, Input_3, Input_4,
                Input_5, Input_6, Input_7, Input_8, Input_9, Input_10, Input_11, Dropdown_3, GP_Letter);
            AddOrEditFields(true);              //Changes read only on fields to false
            Current_Patient.Checked = true;     //Will alwasy be a current patient deemed by the sql statement
        }

        //Dropsdown the comboxbox when the down key is pressed
        private void Dropdown_1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                Dropdown_1.DroppedDown = true;
            }
        }

        //When dropdown occurs a list of GPS from the Reffering GP Table will populate
        //Reffering_GP_Dropdown
        private void Dropdown_2_DropDown(object sender, EventArgs e)
        {
            Reffering_GP_Database.PopulateRefferingGPDropdown(Dropdown_3);
        }

        //Dropsdown the comboxbox when the down key is pressed
        private void Dropdown_2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                Dropdown_2.DroppedDown = true;
            }
        }

        //Saves changes to new patient or existing patient
        private void Save_Click(object sender, EventArgs e)
        {
            if (Patient_Database.SavePatient(Dropdown_1, Input_1, Date_1, Date_2, Dropdown_2, Input_2, Input_3, Input_4,
                Input_5, Input_6, Input_7, Input_8, Input_9, Input_10, Input_11, Dropdown_3, Current_Patient, GP_Letter) == true)
            {
                
                AddOrEditFields(false);
                Add_Dropdown_1.Focus();
            }
        }

        //Return to the previous screen
        private void Back_Click(object sender, EventArgs e)
        {
            //Handles whether to revert back to initial sceen or back to consultation form
            Global.addPatientOpen = false;
            if (Global.addConsultationOpen == true)
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

        //Clears dropdown items if any
        //Also clears any text in the input fields
        private void Add_Dropdown_1_Click(object sender, EventArgs e)
        {
            Dropdown_1.DataSource = null;
            Input_1.Clear();
            Input_2.Clear();
            Input_3.Clear();
            Input_4.Clear();
            Input_5.Clear();
            Input_6.Clear();
            Input_7.Clear();
            Input_8.Clear();
            Input_9.Clear();
            Input_10.Clear();
            Dropdown_3.DataSource = null;
            Input_11.Clear();

            AddOrEditFields(true);              //Allows fields to be entered into
            Current_Patient.Checked = true;     //Changes current patient checkbox to true

            //When creating a Patient it would be impossible to have already sent the letter
            GP_Letter.Checked = false;          
            GP_Letter.Enabled = false;          
            Dropdown_2.Focus();                 //Focuses on gender dropdown
            Global.editingExistingPatient= false;

            //Sets Save button enabled to false forcing users to satisfy minimum requirements
            Save.Enabled = false;
        }

        //Clears all fields and initializes delete patient form
        private void Delete_Click(object sender, EventArgs e)
        {
            Delete_Patient deletePatient = new Delete_Patient();
            deletePatient.ShowDialog();
            deletePatient.Focus();
        }

        //Loads Add new Reffering GP Form
        private void Add_Dropdown_2_Click(object sender, EventArgs e)
        {
            Global.addPatientOpen = true;
            Add_Edit_Reffering_GP addRefGP = new Add_Edit_Reffering_GP();
            addRefGP.ShowDialog();
            addRefGP.Focus();
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

        //Enables save button if all dropdowns have been selected
        private void Dropdown_3_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Dropdown_2.SelectedIndex != -1)
            {
                Save.Enabled = true;
            }
        }


        private void Dropdown_3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                Dropdown_3.DroppedDown = true;
            }
        }

        //Enables save button if all dropdowns have been selected
        private void Dropdown_2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Dropdown_3.SelectedIndex != -1)
            {
                Save.Enabled = true;
            }
        }

        //Closes all forms if this form is closed
        private void Add_or_Edit_Patient_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Global.addConsultationOpen == false)
            {
                Application.Exit();
            }
        }
    }
}