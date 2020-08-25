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
    public partial class Delete_Practice : Form
    {
        //********************Class Variables********************


        public Delete_Practice()
        {
            InitializeComponent();
            Global.testConnection(this);            //Tests connection
            UserLabel.Text = "Current User: " +
                Global.userFirstName +
                " " + Global.userLastName;          //Sets user label
        }

        //Shows list of practices to delete
        private void Delete_Existing_Dropdown_DropDown(object sender, EventArgs e)
        {
            Practice_Database.populatePracticeDropDown(Delete_Existing_Dropdown);
        }

        //Populates practices to textboxes
        private void Delete_Existing_Dropdown_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Practice_Database.PracticeSelected(Delete_Existing_Dropdown, Input_1, Input_2, Input_3, Input_4,
                Input_5, Input_6, Input_7, Input_8);

            //Allows user to delete record
            Delete.Enabled = true;
        }

        //Back to Add_Edit_Practice
        private void Back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Deletes Practice
        private void Delete_Click(object sender, EventArgs e)
        {
            Practice_Database.DeletePractice(Delete_Existing_Dropdown, Input_1);
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
    }
}
