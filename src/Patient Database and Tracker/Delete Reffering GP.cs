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
    public partial class Delete_Reffering_GP : Form
    {
        //********************Constructor********************

        public Delete_Reffering_GP()
        {
            InitializeComponent();
            Global.testConnection(this);            //Tests connection
            UserLabel.Text = "Current User: " +
                Global.userFirstName + " " +
                Global.userLastName;                //Sets user label
            setSortedButtonText();                  //Sets sort by button text
        }

        //Chanegs text of button
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
        //Shows list of patients to delete
        private void Delete_Existing_Dropdown_DropDown(object sender, EventArgs e)
        {
            Reffering_GP_Database.PopulateRefferingGPDropdown(Delete_Existing_Dropdown);
        }

        //Populates textboxes
        private void Delete_Existing_Dropdown_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Reffering_GP_Database.RefferingGPSelectedDelete(Delete_Existing_Dropdown, Input_1, Input_2,
                Input_3, Input_4, Input_5, Input_6, Input_7);

            //Allows user to delete record
            Delete.Enabled = true;
        }

        //Back to Add_Edit_Reffering_GP
        private void Back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Deletes Reffering GP
        private void Delete_Click(object sender, EventArgs e)
        {
            Reffering_GP_Database.DeleteRefferingGP(Delete_Existing_Dropdown, Input_1);
            if(Delete_Existing_Dropdown.DataSource == null)
            {
                Input_1.Clear();
                Input_2.Clear();
                Input_3.Clear();
                Input_4.Clear();
                Input_5.Clear();
                Input_6.Clear();
                Input_7.Clear();
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

        //Changes sorting of lists
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
            Input_2.Clear();
            Input_4.Clear();
            Input_5.Clear();
            Input_6.Clear();
            Input_7.Clear();
        }
    }
}
