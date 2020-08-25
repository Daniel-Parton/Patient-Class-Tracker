using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Finisar.SQLite;
using System.IO;

namespace Patient_Database_and_Tracker
{
        //SQL DDL
        //"CREATE TABLE [USERS]" +
        //"(USER_UserId INTEGER DEFAULT 1 PRIMARY KEY," +
        //"USER_First_Name TEXT NOT NULL," +
        //"USER_Last_Name TEXT NOT NULL," +
        //"USER_Username TEXT NOT NULL," +
        //"USER_Password TEXT NOT NULL," +
        //"USER_Secret_Question NOT NULL," +
        //"USER_Secret_Answer NOT NULL," +
        //"USER_Master_User BOOLEAN NOT NULL);";

    public partial class Change_User_Details : Form
    {
        public Change_User_Details()
        {
            InitializeComponent();
            Global.testConnection(this);        //Tests database connection
            FillDetails();                      //Fills patient details
        }

        //Loads userdetails into fields
        private void FillDetails()
        {
            int master = 0;                     //Used as a sqlite bool figure for master user
            string query = "SELECT USER_First_Name, USER_Last_Name, USER_Username," +
                "USER_Secret_Question, USER_Secret_Answer, USER_Master_User FROM [USERS] WHERE " +
                "USER_UserId = '" + Global.userId + "';";                   //SQL Query

            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {
                try
                {
                    Global.connection.Open();
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Input_1.Text =  Convert.ToString(reader["USER_First_Name"]);
                        Input_2.Text = Convert.ToString(reader["USER_Last_Name"]);
                        Input_3.Text = Convert.ToString(reader["USER_Username"]);
                        Input_4.Text = Convert.ToString(reader["USER_Secret_Question"]);
                        Input_5.Text = Convert.ToString(reader["USER_Secret_Answer"]);

                        //Sets checkbox for master user
                        //SQLite holds boolean as an integer 0 for false, 1 for true
                        if (reader["USER_Master_User"] == DBNull.Value)
                        {
                            master = 0;
                        }
                        else
                        {
                            master = Convert.ToInt32(reader["USER_Master_User"]);
                        }
                        if (master == 0)
                        {
                            Master_User.Checked = false;
                        }
                        else
                        {
                            Master_User.Checked = true;
                        }
                    }
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        //Loads Initial_Screen
        private void Back_Click(object sender, EventArgs e)
        {
            this.Hide();
            Initial_Screen initial = new Initial_Screen();
            initial.ShowDialog();
            initial.Focus();
        }

        //Allows user to edit details
        private void Edit_Details_Click(object sender, EventArgs e)
        {
            //if user is MASTER then user cannot edit
            if (Global.userId == 1)
            {
                MessageBox.Show("You cannot change details of master user", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Input_1.ReadOnly = false;
            Input_2.ReadOnly = false;
            Input_3.ReadOnly = false;
            Input_4.ReadOnly = false;
            Input_5.ReadOnly = false;
            Edit_Details.Enabled = false;
            Save.Visible = true;
            Input_1.Focus();
        }

        //Saves details of user
        private void Save_Click(object sender, EventArgs e)
        {
            //Error message for not filling in mandatory fields
            if (string.IsNullOrWhiteSpace(Input_1.Text) == true || string.IsNullOrWhiteSpace(Input_2.Text) == true ||
                string.IsNullOrWhiteSpace(Input_3.Text) == true || string.IsNullOrWhiteSpace(Input_4.Text) == true ||
                string.IsNullOrWhiteSpace(Input_5.Text) == true)
            {
                MessageBox.Show("All fields need to be filled in", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Error messages if the username is the same or if the first and last name are the same
            string query = "SELECT USER_First_Name, USER_Last_Name, USER_Username FROM [USERS] " +
                "WHERE USER_UserId NOT IN (" + Global.userId + ");";
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {
                Global.connection.Open();
                SQLiteDataReader reader = cmd.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        string firstName = Convert.ToString(reader["USER_First_Name"]);
                        string lastName = Convert.ToString(reader["USER_Last_Name"]);
                        string userName = Convert.ToString(reader["USER_Username"]);

                        //Error message if username exists
                        if (Input_3.Text == userName)
                        {
                            MessageBox.Show("This username already exists.  Please choose a different username", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        if (Input_1.Text == firstName && Input_2.Text == lastName)
                        {
                            DialogResult areYouSure = MessageBox.Show("There is already a user with the same name:\n" +
                                "UserName: " + userName + "\nName: " + firstName + " " + lastName + "\n\n" +
                                "Are you sure you would like to continue?", "Are You Sure", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (areYouSure == DialogResult.No)
                            {
                                return;     //cancels the event action
                            }
                        }
                    }
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                //Updates user
                if (UserDatabase.UpdateUser(Input_1.Text, Input_2.Text, Input_3.Text, Input_4.Text,
                    Input_5.Text) == true)
                {
                    MessageBox.Show("Update successful", "Success", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    Input_1.ReadOnly = true;
                    Input_2.ReadOnly = true;
                    Input_3.ReadOnly = true;
                    Input_4.ReadOnly = true;
                    Input_5.ReadOnly = true;
                    Edit_Details.Enabled = true;
                    Save.Visible = false;

                    Global.userFirstName = Input_1.Text;
                    Global.userLastName = Input_2.Text;
                }
            }
        }

        //Opens Reset_User_Password
        private void Reset_Password_Click(object sender, EventArgs e)
        {
            this.Hide();
            Reset_User_Password reset = new Reset_User_Password();
            reset.ShowDialog();
            reset.Focus();
        }

        //Closes all forms
        private void Change_User_Details_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
