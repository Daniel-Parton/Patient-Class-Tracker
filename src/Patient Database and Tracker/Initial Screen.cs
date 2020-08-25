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
    public partial class Initial_Screen : Form
    {
        public Initial_Screen()
        {
            InitializeComponent();
            UserLabel.Text = "Current User: " + Global.userFirstName + " " + Global.userLastName;
        }
        //Closes all forms upon closing this form
        private void Initial_Screen_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // We use these three SQLite objects:
                SQLiteConnection sqlite_conn;
                SQLiteCommand sqlite_cmd;

                // create a new database connection:
                //Global.readFile("ConnectionString.txt");

                string connectionPath = "Data Source=Database.db;Version=3;New=True;";          //Second part of connection string used by reading ConnectionString.txt
                sqlite_conn = new SQLiteConnection(connectionPath);

                // open the connection:
                sqlite_conn.Open();

                // create a new SQL command:
                sqlite_cmd = sqlite_conn.CreateCommand();

                // Let the SQLiteCommand object know our SQL-Query:
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

                // Now lets execute the SQL ;D
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

                // We are ready, now lets cleanup and close our connection:
                sqlite_conn.Close();
                MessageBox.Show("Connection open");
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //Generates reports
        private void Generate_Reports_Button_Click(object sender, EventArgs e)
        {
            this.Hide();
            Generate_Reports reports = new Generate_Reports();
            reports.ShowDialog();
            reports.Focus();
        }

        //Allows user to configure database connection
        private void Configure_Database_Button_Click(object sender, EventArgs e)
        {
            this.Hide();
            Change_Connection change = new Change_Connection();
            change.ShowDialog();
            change.Focus();
        }

        //Opens Add_Edit_Patient
        private void Add_Patients_Button_Click(object sender, EventArgs e)
        {
            this.Hide();
            Add_Edit_Patient addPatient = new Add_Edit_Patient();
            addPatient.ShowDialog();
            addPatient.Focus();
        }

        //Opens Add_Edit_Consultation_1
        private void Add_Consultation_Button_Click(object sender, EventArgs e)
        {
            this.Hide();
            Add_Edit_Consultation_1 addConsultation1 = new Add_Edit_Consultation_1();
            addConsultation1.ShowDialog();
            addConsultation1.Focus();
        }

        //Opens Add_Edit_Reffering_GP
        private void Add_GP_Button_Click(object sender, EventArgs e)
        {
            this.Hide();
            Add_Edit_Reffering_GP addRefGP = new Add_Edit_Reffering_GP();
            addRefGP.ShowDialog();
            addRefGP.Focus();
        }

        //Opens Add_Edit_Practice
        private void Add_Practice_Button_Click(object sender, EventArgs e)
        {
            this.Hide();
            Add_Edit_Practice addPrac = new Add_Edit_Practice();
            addPrac.ShowDialog();
            addPrac.Focus();
        }

        //Opens Change_User_Details
        private void User_Details_Click(object sender, EventArgs e)
        {
            this.Hide();
            Change_User_Details changeDetails = new Change_User_Details();
            changeDetails.ShowDialog();
            changeDetails.Focus();
        }

        //Opens Login_Screen
        private void Logout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login_Screen login = new Login_Screen();
            login.ShowDialog();
            login.Focus();
        }

        //Allows user to set other users to master users or delete other users
        private void Change_Privileges_Click(object sender, EventArgs e)
        {
            int masterUser = 0;         //Used to hold boolean sqlite value
            string query = "SELECT USER_Master_User FROM [USERS] WHERE USER_UserId =" + Global.userId + ";";    //SQL Query
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {
                try
                {
                    Global.connection.Open();
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader["USER_Master_User"] == DBNull.Value)
                        {
                            masterUser = 0;
                        }
                        else
                        {
                            masterUser = Convert.ToInt32(reader["USER_Master_User"]);
                        }
                    }
                    Global.connection.Close();
                    //If user isn't a master user then error message
                    if (masterUser == 0)
                    {
                        MessageBox.Show("Only master users can alter user privileges", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                        //If master user then open Change_User_Privilleges
                    else
                    {
                        this.Hide();
                        Change_User_Privileges change = new Change_User_Privileges();
                        change.ShowDialog();
                        change.Focus();
                    }
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        //Opens Current_Patients
        private void Current_Non_Currrent_Patients_Click(object sender, EventArgs e)
        {
            this.Hide();
            Current_Patients current = new Current_Patients();
            current.ShowDialog();
            current.Focus();
        }

        //Loads form to generate letters
        private void Letters_Click(object sender, EventArgs e)
        {
            this.Hide();
            Generate_Letters generateLetters = new Generate_Letters();
            generateLetters.ShowDialog();
            generateLetters.Focus();
        }

        //Loads form to view patients
        private void View_Patients_Click(object sender, EventArgs e)
        {
            Global.view = Global.viewPatients;      //Sets Trigger for Viewing Patients
            this.Hide();
            View_Records viewPatients = new View_Records();
            viewPatients.ShowDialog();
            viewPatients.Focus();
        }

        //Opens form to view consultations
        private void View_Consultations_Click(object sender, EventArgs e)
        {
            Global.view = Global.viewConsultations;         //Sets Trigger for Viewing Consultations
            this.Hide();
            View_Records viewConsultations = new View_Records();
            viewConsultations.ShowDialog();
            viewConsultations.Focus();
        }

        //Loads form to view reffering gps
        private void View_Reffering_GPs_Click(object sender, EventArgs e)
        {
            Global.view = Global.viewRefGP;         //Sets Trigger for Viewing Reffering GPs
            this.Hide();
            View_Records viewRefferingGPs = new View_Records();
            viewRefferingGPs.ShowDialog();
            viewRefferingGPs.Focus();
        }

        //Loads form to view practices
        private void View_General_Practices_Click(object sender, EventArgs e)
        {
            Global.view = Global.viewPractices;         //Sets Trigger for Viewing Practices
            this.Hide();
            View_Records viewRPrac = new View_Records();
            viewRPrac.ShowDialog();
            viewRPrac.Focus();
        }

        //Opens Add_Edit_Class
        private void Edit_View_Classes_Click(object sender, EventArgs e)
        {
            this.Hide();
            Add_Edit_Class classes = new Add_Edit_Class();
            classes.ShowDialog();
            classes.Focus();
        }

        //Opens Add_Edit_Class_Times
        private void Class_Times_Click(object sender, EventArgs e)
        {
            Global.addEditTimeOpen = true;
            this.Hide();
            Add_Edit_Class_Times classTimes = new Add_Edit_Class_Times();
            classTimes.ShowDialog();
            Class_Times.Focus();
        }

        //Opens Mark_Class_attendance
        private void Mark_Attendance_Click(object sender, EventArgs e)
        {
            this.Hide();
            Mark_Class_Attendance attendance = new Mark_Class_Attendance();
            attendance.ShowDialog();
            attendance.Focus();
        }
    }
}
