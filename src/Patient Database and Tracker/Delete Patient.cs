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
using Finisar.SQLite;

namespace Patient_Database_and_Tracker
{
    public partial class Delete_Patient : Form
    {

        //********************Class Constructor********************

        public Delete_Patient()
        {
            InitializeComponent();
            Global.testConnection(this);            //Tests connection
            UserLabel.Text = "Current User: " +
                Global.userFirstName + " " +
                Global.userLastName;                //Sets user label
            setSortedButtonText();                  //Sets sorted by button text
        }

        //********************Non Event Methods********************

        //Chanhges text of button
        private void setSortedButtonText()
        {
            if (Global.sortDropdown == Global.ID)
            {
                Sort_Lists.Text = "Sort List by Name";
            }
            else if (Global.sortDropdown == Global.NAME)
            {
                Sort_Lists.Text = "Sort List by ID";
            }
        }

        //********************Database Event Methods********************


        //Shows list of patients to delete
        private void Delete_Existing_Dropdown_DropDown(object sender, EventArgs e)
        {
            Patient_Database.PopulatePatientDropdown(Delete_Existing_Dropdown);

        }

        //Populates fields with patient details
        private void Delete_Existing_Dropdown_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Patient_Database.PatientSelectedDelete(Delete_Existing_Dropdown, Input_1, Input_2, Input_3, Input_4,
                Input_5, Input_6, Input_7, Input_8, Input_9, Input_10, Input_11, Input_12, Input_13, Input_14, Input_15, Current_Patient, GP_Letter);

            //Allows user to delete record
            Delete.Enabled = true;
        }

        //Returns to add Patient
        private void Back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Deletes patient
        private void Delete_Click(object sender, EventArgs e)
        {
            Patient_Database.DeletePatient(Delete_Existing_Dropdown, Input_1);

            if (Delete_Existing_Dropdown.DataSource == null)
            {
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
                Input_11.Clear();
                Input_12.Clear();
                Input_13.Clear();
                Input_14.Clear();
                Input_15.Clear();
                Current_Patient.Checked = false;
                GP_Letter.Checked = false;
            }
        }

        //Drops box down if user presses down key
        private void Delete_Existing_Dropdown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                Delete_Existing_Dropdown.DroppedDown = true;
            }
        }
        //Changes the way that the dropdown is sorted
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
            Delete_Existing_Dropdown.DataSource = null;
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
            Input_11.Clear();
            Input_12.Clear();
            Input_13.Clear();
            Input_14.Clear();
            Input_15.Clear();
            Current_Patient.Checked = false;
            GP_Letter.Checked = false;
        }
    }
}
