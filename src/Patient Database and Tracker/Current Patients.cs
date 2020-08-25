using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Finisar.SQLite;

namespace Patient_Database_and_Tracker
{
    public partial class Current_Patients : Form
    {
        DataTable patdt;

        public Current_Patients()
        {
            InitializeComponent();
            Global.testConnection(this);            //Tests connection
            setSortedButtonText();                  //Sets sorted button text
            UserLabel.Text = "Current User: " +
                Global.userFirstName + " " +
                Global.userLastName;                //Sets user label
        }

        //Changes text on search by button
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

        //Returns to initial screen
        private void Back_Click(object sender, EventArgs e)
        {
            this.Hide();
            Initial_Screen initial = new Initial_Screen();
            initial.ShowDialog();
            initial.Focus();
        }

        //Shows list of all existing patients
        private void Dropdown_1_DropDown(object sender, EventArgs e)
        {
            Patient_Database.PopulatePatientDropdown(Dropdown_1, true);
        }

        //When a user clicks a selection from the dropdown box all the values of the selected patient will
        //populate the fields.  The program will also know that this is an edit session and not a new patient
        private void Dropdown_1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Patient_Database.PatientSelectedCurrentPatients(Dropdown_1, Input_1, Input_2, Input_3, Current_Patient, Save);
        }

        //Saves changes to current/noncurrent patient
        private void Save_Click(object sender, EventArgs e)
        {
            Patient_Database.UpdateCurrentPatient(Dropdown_1, Current_Patient, Save);
        }

        //Sorts lists by name/id
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

        //Closes all forms
        private void Current_Patients_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
         
    }
}
