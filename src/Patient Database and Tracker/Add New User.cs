using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Finisar.SQLite;

namespace Patient_Database_and_Tracker
{
    public partial class Add_New_User : Form
    {
        public Add_New_User()
        {
            InitializeComponent();
            testConnection();           //Tests connection
        }

        //********************Non Event Methods********************

        //There is a Global.TestConnection but this is used because the user hasn't logged in yet
        private void testConnection()
        {
            //Connection is read from a textfile in the app domain directory
            try
            {
                Global.setConnectionString("ConnectionString.dat");
            }
            //If file not found loads a new form where you can choose the database from a folder dialog
            //Text file is found in the app domain base directory
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("Error loading database.  Please choose another database\n\n" + ex.ToString(), "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory.ToString();
                ofd.Filter = "Database File (*.db)|*.db|MDF |*mdf";
                ofd.Title = "Choose Database to connect to";
                DialogResult result = ofd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Global.connectionPath = ofd.FileName;
                    Global.connectionString = Global.connection1 +
                        Global.connectionPath + Global.connection2;
                    Global.writeDatafile("ConnectionString.dat", Global.connectionPath);
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            {
                try
                {
                    Global.connection.Open();
                    Global.connection.Close();
                }
                //Any other outlying exception will be caught here.
                //A new form will load and allow the user to choose a new database from a file dialog
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Error loading database.  Please choose another database\n\n" + ex.ToString(), "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory.ToString();
                    ofd.Filter = "Database File (*.db)|*.db|MDF |*mdf";
                    ofd.Title = "Choose Database to connect to";
                    DialogResult result = ofd.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        Global.connectionPath = ofd.FileName;
                        Global.connectionString = Global.connection1 +
                            Global.connectionPath + Global.connection2;
                        Global.writeDatafile("ConnectionString.dat", Global.connectionPath);
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        return;
                    }
                }
            }
        }

        //********************Non Event Methods********************

        //Creates New user
        private void Register_Click(object sender, EventArgs e)
        {
            //Error message for not filling in mandatory fields
            if (string.IsNullOrWhiteSpace(Input_1.Text) == true || string.IsNullOrWhiteSpace(Input_2.Text) == true ||
                string.IsNullOrWhiteSpace(Input_3.Text) == true || string.IsNullOrWhiteSpace(Input_4.Text) == true || 
                string.IsNullOrWhiteSpace(Input_5.Text) == true || string.IsNullOrWhiteSpace(Input_6.Text) == true ||
                string.IsNullOrWhiteSpace(Input_7.Text) == true)
            {
                MessageBox.Show("All fields need to be filled in", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Error message if password doesn't match re enter password
            if (Input_4.Text != Input_5.Text)
            {
                MessageBox.Show("Password and re-enter password don't match", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Error messages if the username is the same or if the first and last name are the same
            string query = "SELECT USER_First_Name, USER_Last_Name, USER_Username FROM [USERS];";
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

                        if (Input_3.Text == userName)
                        {
                            MessageBox.Show("This username already exists.  Please choose a different username", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        //Users may have the same name but is unlikely.  Gives user the option to continue
                        if (Input_1.Text == firstName && Input_2.Text == lastName)
                        {
                            DialogResult areYouSure = MessageBox.Show("There is already a user with the same name:\n" +
                                "UserName: " + userName + "\nName: " + firstName + " " + lastName + "\n\n" +
                                "Are you sure you would like to continue?", "Are you sure", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
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
            }

            //If no error message is prompted user is created
            UserDatabase.AddUser(Input_1.Text, Input_2.Text, Input_3.Text, Input_4.Text, Input_6.Text, Input_7.Text);
            MessageBox.Show("User successully created\n\nUsername: " + Input_3.Text + "\nPassword: " + Input_4.Text,
                "Register successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            //returns to login screen
            this.Hide();
            Login_Screen login = new Login_Screen();
            login.ShowDialog();
            login.Focus();
        }

        //Returns to login screen
        private void Back_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login_Screen login = new Login_Screen();
            login.ShowDialog();
            login.Focus();
        }

        //Closes all open forms
        private void Add_New_User_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
