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
    public partial class Add_Edit_Practice : Form
    {
        //DDL for [GENERAL PRACTICE]
        //"CREATE TABLE [GENERAL PRACTICE]" +
        //"(PRAC_Name TEXT PRIMARY KEY," +
        //"PRAC_Address TEXT," +
        //"PRAC_Suburb TEXT," +
        //"PRAC_Post_Code TEXT," +
        //"PRAC_State TEXT," +
        //"PRAC_Phone TEXT," +
        //"PRAC_Fax TEXT," +
        //"PRAC_Notes TEXT);";

        //********************Class Constructor********************

        public Add_Edit_Practice()
        {
            InitializeComponent();
            Global.testConnection(this);                    //Tests database connection
            UserLabel.Text = "Current User: " +
                Global.userFirstName +
                " " + Global.userLastName;                  //Sets user label
            Global.editingExistingPractice = false;         //If an editing session is open it reverts to false
        }

        //********************Non Event Methods********************

        //Used when clciking to add/edit Practice and also when the practice details are saved
        //Forces the user to choose to add or edit beofre inputting data
        public void AddOrEditFields(Boolean editable)
        {
            if (editable == true)
            {
                Input_1.ReadOnly = false;
                Input_2.ReadOnly = false;
                Input_3.ReadOnly = false;
                Input_4.ReadOnly = false;
                Input_5.ReadOnly = false;
                Input_6.ReadOnly = false;
                Input_7.ReadOnly = false;
                Input_8.ReadOnly = false;
                Input_8.ReadOnly = false;

            }
            else
            {
                Input_1.ReadOnly = true;
                Input_2.ReadOnly = true;
                Input_3.ReadOnly = true;
                Input_4.ReadOnly = true;
                Input_5.ReadOnly = true;
                Input_6.ReadOnly = true;
                Input_7.ReadOnly = true;
                Input_8.ReadOnly = true;
                Input_8.ReadOnly = true;
            }
        }

        //********************Database Event Methods********************

        //Shows list of all existing practices
        private void Dropdown_1_DropDown(object sender, EventArgs e)
        {
            Practice_Database.populatePracticeDropDown(Dropdown_1);
        }

        //When a user clicks a selection from the dropdown box all the values of the selected practice will
        //populate the fields.  The program will also know that this is an edit session and not a new Practice
        private void Dropdown_1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Practice_Database.PracticeSelected(Dropdown_1, Input_1, Input_2, Input_3, Input_4,
                Input_5, Input_6, Input_7, Input_8);
            AddOrEditFields(true);             //Allows fields to be written in

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

        //Saves changes to new patient or existing practice
        private void Save_Click(object sender, EventArgs e)
        {
            bool saveSuccess = Practice_Database.SavePractice(Dropdown_1, Input_1, Input_2, Input_3, Input_4,
                Input_5, Input_6, Input_7, Input_8);
            if (saveSuccess == true)
            {
                AddOrEditFields(false);     //Changes fields to read only.  User needs to select
                //add new patient or edit existing if they want to continue
                Add_Dropdown_1.Focus();
            }
        }

        //********************Other Form and Object Event Methods********************

        //Return to the previous screen or to Add Reffering GP
        private void Back_Click(object sender, EventArgs e)
        {
            if (Global.addRefferingGPOpen == true)
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

        //Clears dropdown datasource
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
            Input_8.Clear();
            AddOrEditFields(true);          //Allows fields to be entered into
            Input_1.Focus();       //Moves cursor to first name

            Global.editingExistingPractice = false;

            Save.Enabled = true;
        }

        //Clears all fields and initializes delete practice form
        private void Delete_Click(object sender, EventArgs e)
        {
            Delete_Practice deletePractice = new Delete_Practice();
            deletePractice.ShowDialog();
            deletePractice.Focus();
        }

        //Closes all forms if this form is closed
        private void Add_or_Edit_Patient_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Global.addRefferingGPOpen == false)
            {
                Application.Exit();
            }
        }
    }
}