using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Patient_Database_and_Tracker
{
    public partial class Change_Connection : Form
    {
        public Change_Connection()
        {
            InitializeComponent();
            UserLabel.Text = "Current User: " +
                Global.userFirstName + " " +
                Global.userLastName;                //Sets user label
        }

        //Opens file dialog to choose database
        private void Choose_Database_Button_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal).ToString();
            ofd.Filter = "Database File (*.db)|*.db";
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
            //Once chosen returns to Initial_Screen
            this.Hide();
            Initial_Screen initial = new Initial_Screen();
            initial.ShowDialog();
            initial.Focus();
        }

        //Loads Initial_Screen
        private void Leave_Click(object sender, EventArgs e)
        {
            this.Hide();
            Initial_Screen initial = new Initial_Screen();
            initial.ShowDialog();
            initial.Focus();
        }

        //Closes all forms
        private void Change_Connection_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
