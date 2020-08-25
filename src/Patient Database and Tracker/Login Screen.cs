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
    public partial class Login_Screen : Form
    {

        public Login_Screen()
        {
            InitializeComponent();
        }

        //Tests the connection
        private bool testConnection()
        {
            bool testSuccessful = false;
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
                ofd.InitialDirectory = ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal).ToString();
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
                    return testSuccessful;
                }
            }
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            {
                try
                {
                    Global.connection.Open();
                    Global.connection.Close();
                    testSuccessful = true;

                }
                //Any other outlying exception will be caught here.
                //A new form will load and allow the user to choose a new database from a file dialog
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Error loading database.  Please choose another database\n\n" + ex.ToString(), "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.InitialDirectory = ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal).ToString();
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
                        return testSuccessful;
                    }
                }
            }
            return testSuccessful;
        }
        
        //Validates the username and password.  Launches initial screen if successful
        private void Login_Button_Click(object sender, EventArgs e)
        {
            //Stops method if testConnection Fails
            if (testConnection() == false)
            {
                return;
            }

            //Error if all fields are not filled in
            if(string.IsNullOrWhiteSpace(Username_Input.Text) || string.IsNullOrWhiteSpace(Password_Input.Text))
            {
                MessageBox.Show("All fields need to be filled in", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Gets username and password
            Global.userId = UserDatabase.GetUserIdByUsernameAndPassword(Username_Input.Text, Password_Input.Text);

            //Error if username and password dont match
            if (Global.userId == 0)
            {
                MessageBox.Show("The password or username was incorrect.  Check if caps lock is on.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Gets the users name and logs inot intial screen
            else
            {
                Initial_Screen firstScreen = new Initial_Screen();
                this.Hide();
                firstScreen.ShowDialog();
                firstScreen.Focus();
            }
        }

        //If user clicks enter within username textbox the login Button event is run
        private void Username_Input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Login_Button_Click(this, EventArgs.Empty);
            }
        }
        //If user clicks enter within password textbox the login Button event is run
        private void Password_Input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Login_Button_Click(this, EventArgs.Empty);
            }
        }

        //Opens form to register new user
        private void Register_Click(object sender, EventArgs e)
        {
            this.Hide();
            Add_New_User addUser = new Add_New_User();
            addUser.ShowDialog();
            addUser.Focus();
        }

        //Opens form to reset forgotten password
        private void Forgotten_Password_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Forgotten_Password forgot = new Forgotten_Password();
            forgot.ShowDialog();
            forgot.Focus();
        }

        //Closes all forms
        private void Login_Screen_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void New_Database_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal).ToString();
            sfd.Filter = "Database File (*.db)|*.db";
            sfd.Title = "Create new database";
            DialogResult result = sfd.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    SQLiteConnection sqlite_conn;
                    SQLiteCommand sqlite_cmd;

                    string connectionPath = "Data Source=" + sfd.FileName + ";Version=3;New=True;";          //Second part of connection string used by reading ConnectionString.txt
                    sqlite_conn = new SQLiteConnection(connectionPath);

                    // open the connection:
                    sqlite_conn.Open();

                    // create a new SQL command:
                    sqlite_cmd = sqlite_conn.CreateCommand();

                    // Creating database schema
                    sqlite_cmd.CommandText = "CREATE TABLE [PATIENT]" +
                        "(PAT_ID_Number INTEGER DEFAULT 1 PRIMARY KEY," +
                        "PAT_First_Name TEXT NOT NULL," +
                        "PAT_Last_Name TEXT NOT NULL," +
                        "PAT_Address TEXT," +
                        "PAT_Suburb TEXT," +
                        "PAT_Post_Code TEXT," +
                        "PAT_State TEXT," +
                        "PAT_Home_Phone TEXT," +
                        "PAT_Mobile_Phone TEXT," +
                        "PAT_Email_Address TEXT," +
                        "REFGP_ID_Number INTEGER NOT NULL," +
                        "PAT_Date_Reffered DATE," +
                        "PAT_Date_of_Birth DATE," +
                        "PAT_Gender TEXT," +
                        "PAT_Notes TEXT," +
                        "PAT_GP_Letter_Sent BOOLEAN," +
                        "PAT_Current BOOLEAN," +
                        "PAT_Class_Attendance INTEGER," +
                        "FOREIGN KEY (REFGP_ID_Number) REFERENCES [REFFERING GP](REFGP_ID_Number) ON DELETE RESTRICT);" +

                        "CREATE TABLE [REFFERING GP]" +
                        "(REFGP_ID_Number INTEGER DEFAULT 1 PRIMARY KEY," +
                        "REFGP_First_Name TEXT," +
                        "REFGP_Last_Name TEXT," +
                        "REFGP_Direct_Number TEXT," +
                        "REFGP_Email_Address TEXT," +
                        "PRAC_Name TEXT NOT NULL," +
                        "REFGP_Notes TEXT," +
                        "FOREIGN KEY (PRAC_Name) REFERENCES [GENERAL PRACTICE](PRAC_Name) ON DELETE RESTRICT);";

                    sqlite_cmd.ExecuteNonQuery();

                    sqlite_cmd.CommandText = "CREATE TABLE [GENERAL PRACTICE]" +
                        "(PRAC_Name TEXT PRIMARY KEY," +
                        "PRAC_Address TEXT," +
                        "PRAC_Suburb TEXT," +
                        "PRAC_Post_Code TEXT," +
                        "PRAC_State TEXT," +
                        "PRAC_Phone TEXT," +
                        "PRAC_Fax TEXT," +
                        "PRAC_Notes TEXT);";

                    sqlite_cmd.ExecuteNonQuery();


                    sqlite_cmd.CommandText = "CREATE TABLE [CONSULTATION]" +
                        "(CONS_ID_Number TEXT PRIMARY KEY," +
                        "PAT_ID_Number INTEGER NOT NULL," +
                        "CONS_Consultant TEXT NOT NULL," +
                        "CONS_Time_Period TEXT NOT NULL," +
                        "CONS_Date DATE NOT NULL," +
                        "CONS_Health_Rating TEXT," +
                        "CONS_Smoking_Status TEXT," +
                        "CONS_Alcohol_Days NUMERIC," +
                        "CONS_Alcohol_Drinks NUMERIC," +
                        "CONS_Height NUMERIC," +
                        "CONS_Weight NUMERIC," +
                        "CONS_BMI NUMERIC," +
                        "CONS_Waist NUMERIC," +
                        "CONS_SBP NUMERIC," +
                        "CONS_DBP NUMERIC," +
                        "CONS_Body_Fat_Percentage NUMERIC," +
                        "CONS_LBM NUMERIC," +
                        "CONS_Visceral_Fat_Rating NUMERIC," +
                        "CONS_CHO_TC NUMERIC," +
                        "CONS_CHO_LDL NUMERIC," +
                        "CONS_CHO_HDL NUMERIC," +
                        "CONS_CHO_TG NUMERIC," +
                        "CONS_Fasting_Glucose NUMERIC," +
                        "CONS_HbA1c NUMERIC," +
                        "CONS_Resistance INTEGER," +
                        "CONS_Cardio INTEGER," +
                        "CONS_Brisk_Walk INTEGER," +
                        "CONS_Light_Activity INTEGER," +
                        "CONS_Fruit_Serves NUMERIC," +
                        "CONS_Fruit_Greater_2_Serves NUMERIC," +
                        "CONS_Vegetable_Serves NUMERIC," +
                        "CONS_Vegetable_Greater_5_Serves NUMERIC," +
                        "CONS_Commercial_Meals INTEGER," +
                        "CONS_Sweets INTEGER," +
                        "CONS_Soft_Drinks INTEGER," +
                        "CONS_Skip_Main_Meals INTEGER," +
                        "CONS_Keep_Track_Of_Food INTEGER," +
                        "CONS_Limit_Portions INTEGER," +
                        "CONS_Eat_When_Upset INTEGER," +
                        "CONS_Eat_In_Front_Of_TV INTEGER," +
                        "CONS_Choose_Healthier_Foods INTEGER," +
                        "CONS_Notes TEXT," +
                        "FOREIGN KEY (PAT_ID_Number) REFERENCES [PATIENT](PAT_ID_Number) ON DELETE RESTRICT);";

                    sqlite_cmd.ExecuteNonQuery();

                    sqlite_cmd.CommandText = "CREATE TABLE [USERS]" +
                        "(USER_UserId INTEGER DEFAULT 1 PRIMARY KEY," +
                        "USER_First_Name TEXT NOT NULL," +
                        "USER_Last_Name TEXT NOT NULL," +
                        "USER_Username TEXT NOT NULL," +
                        "USER_Password TEXT NOT NULL," +
                        "USER_Secret_Question TEXT NOT NULL," +
                        "USER_Secret_Answer TEXT NOT NULL," +
                        "USER_Master_User BOOLEAN);";

                    sqlite_cmd.ExecuteNonQuery();

                    sqlite_cmd.CommandText = "CREATE TABLE [CLASS]" +
                        "(CLASS_Name TEXT PRIMARY KEY);";

                    sqlite_cmd.ExecuteNonQuery();

                    sqlite_cmd.CommandText = "CREATE TABLE [CLASS WAITING LIST]" +
                        "(CWLIST_Name TEXT PRIMARY KEY);";

                    sqlite_cmd.ExecuteNonQuery();

                    sqlite_cmd.CommandText = "CREATE TABLE [CLASS TIME]" +
                        "(CTIME_Start_Time DATETIME NOT NULL," +
                        "CTIME_Finish_Time DATETIME NOT NULL," +
                        "CTIME_Day TEXT NOT NULL," +
                        "CLASS_Name TEXT," +
                        "FOREIGN KEY (CLASS_Name) REFERENCES [CLASS](CLASS_Name) ON DELETE RESTRICT);";

                    sqlite_cmd.ExecuteNonQuery();

                    sqlite_cmd.CommandText = "CREATE TABLE [PATIENT_CLASS]" +
                        "(PAT_ID_Number INTEGER NOT NULL," +
                        "CLASS_Name TEXT," +
                        "PATCLASS_Start_Date DATE NOT NULL," +
                        "FOREIGN KEY (PAT_ID_Number) REFERENCES [PATIENT](PAT_ID_Number) ON DELETE RESTRICT, " +
                        "FOREIGN KEY (CLASS_Name) REFERENCES [CLASS](CLASS_Name) ON DELETE RESTRICT);";

                    sqlite_cmd.ExecuteNonQuery();

                    sqlite_cmd.CommandText = "CREATE TABLE [PATIENT_CLASS WAITING LIST]" +
                        "(PAT_ID_Number INTEGER NOT NULL," +
                        "CWLIST_Name TEXT, " +
                        "FOREIGN KEY (PAT_ID_Number) REFERENCES [PATIENT](PAT_ID_Number) ON DELETE RESTRICT, " +
                        "FOREIGN KEY (CWLIST_Name) REFERENCES [CLASS WAITING LIST](CWLIST_Name) ON DELETE RESTRICT);";

                    sqlite_cmd.ExecuteNonQuery();


                    sqlite_cmd.CommandText = "INSERT INTO [USERS] VALUES(1, 'MASTER', 'USER', 'MASTER','" +
                        Security.HashSHA1("MASTER") + "', 'HELLO','GOODBYE',1);";

                    sqlite_cmd.ExecuteNonQuery();

                    sqlite_conn.Close();

                    //Set new database to global variable and save connection string to datafile
                    Global.connectionPath = sfd.FileName;
                    Global.connectionString = Global.connection1 +
                        Global.connectionPath + Global.connection2;
                    Global.writeDatafile("ConnectionString.dat", Global.connectionPath);
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }
        }

        private void Change_Database_Click(object sender, EventArgs e)
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
        }
    }
}