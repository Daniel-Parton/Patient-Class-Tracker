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
    public partial class Reset_User_Password : Form
    {
        public Reset_User_Password()
        {
            InitializeComponent();
            Global.testConnection(this);
        }

        private void Back_Click(object sender, EventArgs e)
        {
            this.Hide();
            Change_User_Details change = new Change_User_Details();
            change.ShowDialog();
            change.Focus();
        }

        private void Reset_User_Password_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Submit_1_Click(object sender, EventArgs e)
        {
            string username = null;
            string hashedUserOldPassword = null;
            string databaseOldPassword = null;
            string hashedNewPassword = null;
            Boolean passwordMatch = false;
  
            //Error message for not filling in mandatory fields
            if (string.IsNullOrWhiteSpace(Input_1.Text) == true || string.IsNullOrWhiteSpace(Input_2.Text) == true ||
                string.IsNullOrWhiteSpace(Input_3.Text) == true)
            {
                MessageBox.Show("All Fields Need to be Filled in", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Error message if password doesn't match re enter password
            if (Input_2.Text != Input_3.Text)
            {
                MessageBox.Show("Password and Re-Enter Password Don't Match", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            hashedUserOldPassword = Security.HashSHA1(Input_1.Text);
            hashedNewPassword = Security.HashSHA1(Input_2.Text);

            string query = "SELECT USER_Username, USER_Password FROM [USERS] WHERE USER_UserId = " + Global.userId + ";";
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {
                try
                {
                    Global.connection.Open();
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        databaseOldPassword = Convert.ToString(reader["USER_Password"]);
                        username = Convert.ToString(reader["USER_Username"]);
                        if (hashedUserOldPassword == databaseOldPassword)
                        {
                            passwordMatch = true;
                            break;
                        }
                    }
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                if (passwordMatch == true)
                {
                    if (UserDatabase.ResetPassword(username, hashedNewPassword) == true)
                    {
                        MessageBox.Show("Password Changed Successfully", "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        this.Hide();
                        Change_User_Details change = new Change_User_Details();
                        change.ShowDialog();
                        change.Focus();
                    }
                    else
                    {
                        MessageBox.Show("There was an Error Changing Your Password", "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("That is not your old password", "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                }


            }
        }
    }
}
