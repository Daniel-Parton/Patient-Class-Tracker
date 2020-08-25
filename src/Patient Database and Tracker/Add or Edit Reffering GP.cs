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
    public partial class Add_Edit_Reffering_GP : Form
    {

        //********************Class Constructor********************

        public Add_Edit_Reffering_GP()
        {
            InitializeComponent();
            Global.testConnection(this);                   //Tests database connection
            UserLabel.Text = "Current User: " +
                Global.userFirstName +
                " " + Global.userLastName;                  //Set user label
            SetSortedButtonText();                          //Sets the sort by button text
            Global.editingExistingRefferingGP = false;      //If an editing session is open it reverts to false
        }

        //********************Non Event Methods********************

        //Used when clciking to add/edit Reffering GP and also when the Reffering GP details are saved
        //Forces the user to choose to add or edit beofre inputting data
        public void AddOrEditFields(Boolean editable)
        {
            if (editable == true)
            {
                Input_2.ReadOnly = false;
                Input_3.ReadOnly = false;
                Input_4.ReadOnly = false;
                Input_5.ReadOnly = false;
                Dropdown_2.Enabled = true;
                Input_6.ReadOnly = false;

            }
            else
            {
                Input_2.ReadOnly = true;
                Input_3.ReadOnly = true;
                Input_4.ReadOnly = true;
                Input_5.ReadOnly = true;
                Dropdown_2.Enabled = false;
                Input_6.ReadOnly = true;
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
        //********************Database Event Methods********************

        //Shows list of all existing Reffering GPs
        private void Dropdown_1_DropDown(object sender, EventArgs e)
        {
            Reffering_GP_Database.PopulateRefferingGPDropdown(Dropdown_1);
        }

        //When a user clicks a selection from the dropdown box all the values of the selected Reffering GP will
        //populate the fields.  The program will also know that this is an edit session and not a new Reffering GP
        private void Dropdown_1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Reffering_GP_Database.RefferingGPSelectedAddEdit(Dropdown_1, Input_1, Input_2, Input_3, Input_4,
                Input_5, Input_6, Dropdown_2);
            AddOrEditFields(true);

            //Allows user to save record
            Save.Enabled = true;
        }

        //Dropsdown the comboxbox when the down key is pressed
        private void Dropdown_1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                Dropdown_1.DroppedDown = true;
            }
        }

        //Populates Practices
        private void Dropdown_2_DropDown(object sender, EventArgs e)
        {
            Practice_Database.populatePracticeDropDown(Dropdown_2);
        }

        //Dropsdown the comboxbox when the down key is pressed
        private void Dropdown_2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                Dropdown_2.DroppedDown = true;
            }
        }

        //Saves changes to new Reffering GP or existing Reffering GP
        private void Save_Click(object sender, EventArgs e)
        {
            bool saveSuccess = Reffering_GP_Database.SaveRefferingGP(Dropdown_1, Input_1, Input_2, Input_3, Input_4,
                Input_5, Input_6, Dropdown_2);
            if (saveSuccess == true)
            {
                AddOrEditFields(false);     //Changes fields to read only.  User needs to select
                                            //add new Reffering GP or edit existing if they want to continue
                Add_Dropdown_1.Focus();
            }

        }

        //********************Other Form and Object Event Methods********************

        //Return to the previous screen or to the add patient screen
        private void Back_Click(object sender, EventArgs e)
        {
            if (Global.addPatientOpen == true)
            {
                this.Close();
            }
            else
            {
                Global.addRefferingGPOpen = false;
                this.Hide();
                Initial_Screen initial = new Initial_Screen();
                initial.ShowDialog();
                initial.Focus();
            }
        }

        //Clears dropdown datasources
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
            Dropdown_2.DataSource = null;
            Input_6.Clear();
            AddOrEditFields(true);          //Allows fields to be entered into
            Input_2.Focus();                //Moves cursor to first name

            Global.editingExistingRefferingGP = false;

            //Forces user to choose a practice before saving
            Save.Enabled = false;
        }

        //Clears all fields and initializes delete Reffering GP form
        private void Delete_Click(object sender, EventArgs e)
        {
            Delete_Reffering_GP deleteRefGP = new Delete_Reffering_GP();
            deleteRefGP.ShowDialog();
            deleteRefGP.Focus();
        }

        //Open add a new practice form
        private void Add_Dropdown_2_Click(object sender, EventArgs e)
        {
            Global.addRefferingGPOpen = true;
            Add_Edit_Practice addPrac = new Add_Edit_Practice();
            addPrac.ShowDialog();
            addPrac.Focus();
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

        //Allows user to save record
        private void Dropdown_2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Save.Enabled = true;
        }

        //Closes all forms if this form is closed
        private void Add_or_Edit_Reffering_GP_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Global.addPatientOpen == false)
            {
                Application.Exit();
            }
        }
    }
}