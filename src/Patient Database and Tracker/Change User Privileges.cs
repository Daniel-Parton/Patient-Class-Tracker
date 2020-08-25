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
    public partial class Change_User_Privileges : Form
    {
        DataTable userdt;

        public Change_User_Privileges()
        {
            InitializeComponent();
            Global.testConnection(this);    //Tests database connection
            UserLabel.Text = "Current User: " +
                Global.userFirstName +
                " " + Global.userLastName;                  //Sets user label
        }

        //Returns to Initial_Screen
        private void Back_Click(object sender, EventArgs e)
        {
            this.Hide();
            Initial_Screen initial = new Initial_Screen();
            initial.ShowDialog();
            initial.Focus();
        }

        //Shows lists of users
        private void Dropdown_1_DropDown(object sender, EventArgs e)
        {
            string query = "SELECT USER_UserId, USER_First_Name, USER_Last_Name FROM [USERS] WHERE USER_UserId NOT IN (1, " +
            Global.userId + ");";

            //Connects to database and runs query
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
            {
                try
                {
                    Global.connection.Open();
                    userdt = new DataTable();
                    userdt.Columns.Add("ID");
                    userdt.Columns.Add("Display Text");
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    //The dropdownbox will show the users name
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string displayText = reader.GetString(1) + " " + reader.GetString(2);
                        userdt.Rows.Add(id, displayText);
                    }
                    Dropdown_1.DataSource = userdt;
                    Dropdown_1.ValueMember = "ID";
                    Dropdown_1.DisplayMember = "Display Text";
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        //dropd down when down key is pressed
        private void Dropdown_1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                Dropdown_1.DroppedDown = true;
            }
        }

        //Sets checkbox for master user
        private void Dropdown_1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string query = "SELECT USER_Master_User FROM [USERS] WHERE USER_UserId = " + Dropdown_1.SelectedValue + ";";

            //Connects to database and runs query
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
            {
                try
                {
                    Global.connection.Open();
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    //Checks if user is a master user or not
                    while (reader.Read())
                    {
                        if (reader["USER_Master_User"] == DBNull.Value)
                        {
                            Master_User.Checked = false;
                        }
                        else if (Convert.ToInt32(reader["USER_Master_User"]) == 1)
                        {
                            Master_User.Checked = true;
                        }
                    }
                    //Enables controls to make changes
                    Save.Enabled = true;
                    Delete.Enabled = true;
                    Master_User.Enabled = true;
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        //Saves changes to master user
        //SQLite boolean held as in.  0 = false, 1 = true
        private void Save_Click(object sender, EventArgs e)
        {
            int master = 0;                                             //Used to hold sqlite bool figure
            int userId = Convert.ToInt32(Dropdown_1.SelectedValue);
            if (Master_User.Checked == true)
            {
                master = 1;
            }

            //Updates master user change
            if (UserDatabase.UpdateMasterUser(userId, master) == true)
            {
                MessageBox.Show("Update successful", "Success", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                Dropdown_1.DataSource = null;
                Master_User.Enabled = false;
                Delete.Enabled = false;
                Save.Enabled = false;
            }
        }

        //Deletes selected user
        private void Delete_Click(object sender, EventArgs e)
        {
            //Prompting user in case of mistake
            DialogResult areYouSure = MessageBox.Show("Are you sure you want to delete this user?\nDeleting is permanent",
                "Are You Sure", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (areYouSure == DialogResult.No)
            {
                return;
            }

            //Deletes user
            else if (areYouSure == DialogResult.Yes)
            {
                string query = "DELETE FROM [USERS] WHERE USER_UserId =" + Dropdown_1.SelectedValue + ";";      //SQL query

                using (Global.connection = new SQLiteConnection(Global.connectionString))
                using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
                {
                    try
                    {
                        Global.connection.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("User deleted", "Delete successful",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Dropdown_1.DataSource = null;
                        Master_User.Enabled = false;
                        Delete.Enabled = false;
                        Save.Enabled = false;
                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }

        //Closes all forms
        private void Change_User_Privileges_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
