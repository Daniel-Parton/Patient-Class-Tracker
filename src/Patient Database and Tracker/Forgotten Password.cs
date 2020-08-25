using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Finisar.SQLite;
using System.Net.Mail;
using System.IO;

namespace Patient_Database_and_Tracker
{
    public partial class Forgotten_Password : Form
    {
        string newPassword = null;

        public Forgotten_Password()
        {
            InitializeComponent();
            testConnection();           //Tests database connection
        }

        //Used instead of the Global method to avoid loading change_connection screen
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
                    ofd.Title = "Choose database to connect to";
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

        //Return to Login_Screen
        private void Back_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login_Screen login = new Login_Screen();
            login.ShowDialog();
            login.Focus();
        }

        //Checks entered username and loads secret question for user to enter in secret answer
        private void Submit_1_Click(object sender, EventArgs e)
        {
            UserDatabase.LoadSecretQuestion(this, Input_1, Input_2, Input_3, Submit_1, Submit_2);
            this.CenterToScreen();
        }

        private void Submit_2_Click(object sender, EventArgs e)
        {
            if (Input_3.Text != Global.resetPasswordSecretAnswer)
            {
                MessageBox.Show("That is the incorrect answer", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //If answer matches the new password is generated
            newPassword = UserDatabase.ResetForgottenPassword(Input_1.Text);
            this.Height = 269;
            this.CenterToScreen();
            Input_4.Text = newPassword;
            Submit_2.Visible = false;
            MessageBox.Show("Reset password successful.  Once signed in I suggest resetting your password",
                "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //Closes all froms correctly
        private void Forgotten_Password_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
