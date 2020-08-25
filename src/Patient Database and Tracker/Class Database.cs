using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using Finisar.SQLite;

namespace Patient_Database_and_Tracker
{
    class Class_Database
    {

        //****************************Class Time Table****************************

        //DDL for [CLASS TIME]:
        //"CREATE TABLE [CLASS TIME]" +
        //"(CTIME_Start_Time DATETIME NOT NULL," +
        //"CTIME_Finish_Time DATETIME NOT NULL," +
        //"CTIME_Day TEXT NOT NULL," +
        //"CLASS_Name TEXT," +
        //"FOREIGN KEY (CLASS_Name) REFERENCES [CLASS](CLASS_Name) ON DELETE RESTRICT);";

        public static bool SaveTime(ComboBox startTime, ComboBox finishTime, string day, ComboBox className)
        {
            //Can only save data if fields aren't read only
            //if user clicks save changes while fields are read only an error message will show
            if (startTime.SelectedIndex == -1)
            {
                MessageBox.Show("Please choose a start time", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            string query = "";              //Will be SQL Query

            //query if adding a new patient
            if (Global.editingExistingClassTime == false)
            {
                query = "INSERT INTO [CLASS TIME](CTIME_Start_Time, CTIME_Finish_Time, CTIME_Day, CLASS_Name) VALUES" +
                        "(@startTime, @finishTime, @day, @class);";
            }

            //query if editing an existing patient
            else
            {
                query = "UPDATE [CLASS TIME] SET CTIME_Start_Time = @startTime, CTIME_Finish_Time = @finishTime, CLASS_Name = @class " +
                    "WHERE CTIME_Start_Time = '" + Global.editClassStartTimeSQLite + "' AND CTIME_Day = '" + day + "';";
            }

            //Connect to database and run query
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {

                //Setting parameters for input data
                //Converting a time to sqlite format
                DateTime startTimedate = Convert.ToDateTime(startTime.SelectedValue.ToString());
                string hours = startTimedate.Hour.ToString();
                string minutes = startTimedate.Minute.ToString();
                cmd.Parameters.Add("@startTime", Global.getDateString(hours, minutes));

                DateTime finishTimedate = Convert.ToDateTime(finishTime.SelectedValue.ToString());
                hours = finishTimedate.Hour.ToString();
                minutes = finishTimedate.Minute.ToString();
                cmd.Parameters.Add("@finishTime", Global.getDateString(hours, minutes));

                cmd.Parameters.Add("@day", day);
                //Adding parameter for class name.  This option can be null so if user has'nt chosen a class then
                //parameter is set to null
                if (className.SelectedIndex == -1)
                {
                    cmd.Parameters.Add("@class", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.Add("@class", className.SelectedValue.ToString());
                }

                try
                {
                    Global.connection.Open();
                    cmd.ExecuteNonQuery();

                    //Clear selected IDs and the selection in the combo box
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return true;
        }

        public static bool DeleteTime(DataGridView dayDGV, string day)
        {

            //Checks if there are any class times for the patient
            if (dayDGV.Rows.Count == 0)
            {
                MessageBox.Show("There are no class times for " + day, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Checks if the user has selected a class time
            if (dayDGV.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a class time from the " + day + " grid", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Getting the The start and finish time of selected row
            int cellIndex = dayDGV.SelectedCells[0].RowIndex;
            DataGridViewCell cellCollection = dayDGV.Rows[cellIndex].Cells[0];
            Global.editClassTimeDGVSelect = cellCollection.Value.ToString();  //In string format hh:mm - hh:mm

            //Used to convert the start time to an sqlite datetime format
            string startTimeString = Global.editClassTimeDGVSelect.Remove(5);   //Gets start time from string: Removes "hh:mm" from "hh:mm - hhmm"
            string hours = startTimeString.Remove(2);
            string minutes = startTimeString.Remove(0, 3);
            string startTimeSQLite = Global.getDateString(hours, minutes);      //Creates a date string in sqlite format to be used in next form

            //Prompts user if they want to continue
            DialogResult areYouSure = MessageBox.Show("Are you sure you want to delete this class time?",
                "Are You Sure", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            //Cancel if user chooses no
            if (areYouSure == DialogResult.No)
            {
                return false;
            }

            //Deletes patient and any patient_Class relationships
            else if (areYouSure == DialogResult.Yes)
            {

                //SQL Queries
                string query = "DELETE FROM [CLASS TIME] WHERE CTIME_Day ='" + day + "' AND CTIME_Start_Time = '" +
                    startTimeSQLite + "';";

                //Deletes class time
                using (Global.connection = new SQLiteConnection(Global.connectionString))
                using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
                {
                    try
                    {
                        Global.connection.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Class time deleted", "Delete Successful",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            return true;
        }

        //Used in Add_Edit_Class_Times to display class times and class names
        public static void PopulateTimeDataGridView(string day, DataGridView dgv)
        {
            DataTable timedt = new DataTable();         // Will hold datasource of dgv
            //sql query
            string query = "SELECT CTIME_Start_Time, CTIME_Finish_Time, CLASS_Name FROM [CLASS TIME] WHERE CTIME_Day = '" +
                day + "' ORDER BY CTIME_Start_Time;";

            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {
                try
                {
                    Global.connection.Open();
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    //dgv will display start time/finish time (Time) and the class name (Group)
                    timedt.Columns.Add("Time");
                    timedt.Columns.Add("Group");
                    reader = cmd.ExecuteReader();
                    //Loops throug results
                    while (reader.Read())
                    {
                        //stime is in format YYYY-MM-DD HH:MM
                        //I have used a datetime to hold just times.  
                        //All times have date 01-01-0001 and
                        //When comparing times the date is the same
                        string stime = reader.GetString(0);         //Gets start time
                        stime = stime.Remove(0, 11);                //Extracts  HH:MM

                        string ftime = reader.GetString(1);         //Gets finish time         
                        ftime = ftime.Remove(0, 11);                //Extracts HH:MM
                        string time = stime + " - " + ftime;        //Will hold display string
                        string group = reader.GetString(2);         //Gets class name
                        timedt.Rows.Add(time, group);               //Adds row to datasource
                    }
                    dgv.DataSource = null;
                    dgv.DataSource = timedt;
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                //Selects the row selected if editing in Add_New_Time
                if (Global.editingExistingClassTime == true)
                {
                    dgv.Select();
                    dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    //Checks if the datarow is the one that has been selected
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        //If found selects row
                        if (row.Cells[0].Value.ToString().Equals(Global.editClassTimeDGVSelect))
                        {
                            dgv.CurrentCell = row.Cells[0];
                            break;
                        }
                    }
                }
            }
        }

        //Used to generate start times in Add_New_Time
        public static DataTable GenerateStartTimes(string day)
        {
            //Datatable used to hold values of times already chosen for the day
            DataTable selectedDayTimesdt = new DataTable();

            //Will be used as the datatable columns
            int id = 0;
            DateTime startTime = Global.CreateTime(8, 0);
            DateTime finishTime = Global.CreateTime(8, 0);
            string query = "";

            //Different query depending if in edit session
            if (Global.editingExistingClassTime == false)
            {
                query = "SELECT CTIME_Start_Time, CTIME_Finish_Time FROM [CLASS TIME] WHERE CTIME_DAY = '" + day + "';";
            }
            else
            {
                query = "SELECT CTIME_Start_Time, CTIME_Finish_Time FROM [CLASS TIME] WHERE CTIME_DAY = '" + day + "' AND " +
                    "CTIME_START_TIME NOT IN ('" + Global.editClassStartTimeSQLite + "');";
            }

            //Connects to database and runs query
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
            {
                try
                {
                    Global.connection.Open();
                    //Column names for selectedDayTimesdt
                    selectedDayTimesdt.Columns.Add("ID");
                    selectedDayTimesdt.Columns.Add("Start Time");
                    selectedDayTimesdt.Columns.Add("Finish Time");
                    SQLiteDataReader reader = cmd.ExecuteReader();

                    //SelectedDayTimes datatable will be populated
                    while (reader.Read())
                    {
                        //stime is in format YYYY-MM-DD HH:MM
                        //Gets start time in string and generates a datetime object from the hours and minutes
                        string startTimeString = reader.GetString(0);
                        int[] startTimeArray = Global.getHoursAndMinutes(startTimeString);
                        startTime = Global.CreateTime(startTimeArray[0], startTimeArray[1]);

                        //Gets finish time in string and generates a datetime object from the hours and minutes
                        string finishTimeString = reader.GetString(1);
                        int[] finishTimeArray = Global.getHoursAndMinutes(finishTimeString);
                        finishTime = Global.CreateTime(finishTimeArray[0], finishTimeArray[1]);
                        selectedDayTimesdt.Rows.Add(id, startTime, finishTime);
                        id++;
                    }
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            //Datatable used for the times available in the add new time comboboxes
            DataTable startTimes = new DataTable();
            startTimes.Columns.Add("Time");
            startTimes.Columns.Add("Display Time");
            DateTime time = Global.CreateTime(8, 0);
            id = 0;     //Will bes uesd as a counter to find the selected index of the dropdown box in the add new time form

            //loops from start time 8:00am - 8.00 pm or 20:00 24hr time
            for (; time < Global.CreateTime(20, 0); )
            {
                //Trigger used to skip times already used in selectedDayTimesdt
                bool timeFound = false;

                //Iterates through times and sets trigger if found
                foreach (DataRow row in selectedDayTimesdt.Rows)
                {
                    DateTime sTime = DateTime.Parse(row["Start Time"].ToString());
                    //sTime = sTime.AddMinutes(-15);
                    DateTime fTime = DateTime.Parse(row["Finish Time"].ToString());

                    //If time is found then this time will not be added to the selection list
                    //breaks loop
                    if (time >= sTime && time < fTime)
                    {
                        timeFound = true;
                        break;
                    }
                }

                //If time isnt found then the time is added to be used in the add new times combobox
                if (timeFound == false)
                {
                    startTimes.Rows.Add(time, time.ToShortTimeString());
                }

                //Sets global variable to select the dropdown index of start time when the add new time form is opened
                if (Global.editingExistingClassTime == true && Global.startTimeSelectedDateTime == time)
                {
                    Global.startTimeSelectedIndex = id;
                }

                //Adds 15 minutes abd repeats loop
                time = time.AddMinutes(15);
                id++;
            }
            return startTimes;
        }

        //Generate finish times based on selection of start times
        public static DataTable GenerateFinishTimes(string day, ComboBox startTimesDropDown)
        {
            //Datatable used to hold values of times already chosen for the day
            DataTable selectedDayTimesdt = new DataTable();

            //Will be used as the datatable columns
            int id = 0;
            DateTime startTime = Global.CreateTime(8, 0);
            DateTime sqlQueryTime = Convert.ToDateTime(startTimesDropDown.SelectedValue.ToString());

            //Selects all start times greater than the start time chosen in the dropdown box
            string query = "SELECT CTIME_Start_Time FROM [CLASS TIME] WHERE CTIME_DAY = '" + day + "' AND " +
                "CTIME_Start_Time > '" + Global.getDateString(sqlQueryTime.Hour.ToString(), sqlQueryTime.Minute.ToString()) + 
                "' ORDER BY CTIME_Start_Time;;";

            //Connects to database and runs query
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
            {
                try
                {
                    Global.connection.Open();

                    //Column names for selectedDayTimesdt
                    selectedDayTimesdt.Columns.Add("ID");
                    selectedDayTimesdt.Columns.Add("Start Time");
                    SQLiteDataReader reader = cmd.ExecuteReader();

                    //SelectedDayTimes datatable will be populated
                    while (reader.Read())
                    {
                        //stime is in format YYYY-MM-DD HH:MM
                        //Gets start time in string and generates a datetime object from the hours and minutes
                        string startTimeString = reader.GetString(0);
                        int[] startTimeArray = Global.getHoursAndMinutes(startTimeString);
                        startTime = Global.CreateTime(startTimeArray[0], startTimeArray[1]);
                        selectedDayTimesdt.Rows.Add(id, startTime);
                        id++;
                    }
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                //Increasing time is what will be used as the loop variable.  Every 15 minutes
                DateTime increasingTime = Convert.ToDateTime(startTimesDropDown.SelectedValue.ToString());
                increasingTime = increasingTime.AddMinutes(15);

                //Used to hold datasource of finish time combobox
                DataTable finishTimes = new DataTable();
                finishTimes.Columns.Add("Time");
                finishTimes.Columns.Add("Display Time");

                //Will loop through all possible available times to 7.45PM
                for (; increasingTime < Global.CreateTime(20, 0); )
                {
                    //Trigger used to skip times already used in selectedDayTimesdt
                    bool timeFound = false;

                    //Iterates through times and sets trigger if found
                    foreach (DataRow row in selectedDayTimesdt.Rows)
                    {
                        DateTime sTime = DateTime.Parse(row["Start Time"].ToString());
                        //if found sets trigger and breaks loop
                        if (increasingTime > sTime)
                        {
                            timeFound = true;
                            break;
                        }
                    }
                    //Adds time if start time not found and moves on to next 15 minutes
                    if (timeFound == false)
                    {
                        finishTimes.Rows.Add(increasingTime, increasingTime.ToShortTimeString());
                        increasingTime = increasingTime.AddMinutes(15);
                    }

                    //Finishes adding times if nearest start time is found
                    else
                    {
                        break;
                    }
                }
                return finishTimes;
            }
        }

        //Used in Mark Attendance form to display how many times a class is shown and the days in a dgv
        public static void PopulateClassTimeDetailsDGV(ComboBox classNameDropDownList, DataGridView classDetailsDGV)
        {
            DataTable timedt = new DataTable();                 //Holds datasource
            int counter = 0;                                    //Holds total number of times class held
            List<string> dayList = new List<string>();          //Holds lsit of days in a list
            string days = null;                                 //Will hold a single string showing the different days
            string query = "SELECT CTIME_Day FROM [CLASS TIME] WHERE CLASS_Name = '"    
                + classNameDropDownList.SelectedValue + "';";   //SQL Query

            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {
                try
                {
                    Global.connection.Open();
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    timedt.Columns.Add("Class Days");
                    timedt.Columns.Add("Total Times Class is Run");
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        bool uniqueDayfound = true;                 //Trigger if a unique day is read
                        string dayread = reader.GetString(0);

                        //If the first day is read it is unique and dosn't need to be validated
                        if (dayList.Count > 0)
                        {
                            //Checks through list to validate unique days
                            foreach (string day in dayList)
                            {
                                if (dayread == day)
                                {
                                    uniqueDayfound = false;
                                }
                            }
                        }
                        //If unique day found then add to list
                        if (uniqueDayfound == true)
                        {
                            dayList.Add(dayread);
                        }
                        counter++;
                    }
                    //Creates a single string holding all days to be displayed
                    foreach (string day in dayList)
                    {
                        if (days == null)
                        {
                            days = day;
                        }
                        else
                        {
                            days = days + ", " + day;
                        }
                    }

                    //Adds row
                    timedt.Rows.Add(days, counter);
                    classDetailsDGV.DataSource = null;
                    classDetailsDGV.DataSource = timedt;
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        //****************************Class Table****************************
        //DDL for [CLASS TIME]:
        //"CREATE TABLE [CLASS]" +
        //"(CLASS_Name TEXT PRIMARY KEY);";

        //DDL for [CLASS WAITING LIST]:
        //"CREATE TABLE [CLASS WAITING LIST]" +
        //"(CWLIST_Name TEXT PRIMARY KEY);";

        //Saves a class and waiting list
        public static bool SaveClass(TextBox className, ComboBox existingName)
        {
            //Can only save data if fields aren't read only
            //if user clicks save changes while fields are read only an error message will show
            if (string.IsNullOrWhiteSpace(className.Text))
            {
                MessageBox.Show("Please enter a class name", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            string query = "";              //Will be SQL Query
            string query2 = "";
            string query3 = "";
            string query4 = "";
            string query5 = "";

            //query if adding a new class
            //Whenever adding a new class a Class Waiting list is created with the same name but
            //having (Waiting) added to the end of the name

            //If new class then just creates class and waiting list
            if (Global.editingExistingClass == false)
            {
                query = "INSERT INTO [CLASS] VALUES(@name);";
                query2 = "INSERT INTO [CLASS WAITING LIST] VALUES(@waitingName);";
            }

            //query if editing an existing class.  If existing list needs to update the name to all patients
            //currently in the class and waiting list and the class times
            else
            {
                query = "UPDATE [CLASS] SET CLASS_Name = @name WHERE CLASS_NAME = '" + existingName.SelectedValue.ToString() + "';";
                query2 = "UPDATE [CLASS WAITING LIST] SET CWLIST_Name = @waitingName WHERE CWLIST_NAME = '" +
                    existingName.SelectedValue.ToString() + " (Waiting)';";
                query3 = "UPDATE [PATIENT_CLASS] SET CLASS_Name = @name WHERE CLASS_NAME = '" + existingName.SelectedValue.ToString() + "';";
                query4 = "UPDATE [PATIENT_CLASS WAITING LIST] SET CWLIST_Name = @waitingName WHERE CWLIST_NAME = '" +
                    existingName.SelectedValue.ToString() + " (Waiting)';";
                query5 = "UPDATE [CLASS TIME] SET CLASS_Name = @name WHERE CLASS_Name = '" + existingName.SelectedValue.ToString() + "';";
            }

            //Connect to database and run query
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {

                //Setting parameters for input data
                cmd.Parameters.Add("@name", className.Text);
                cmd.Parameters.Add("@waitingName", className.Text + " (Waiting)");
                try
                {
                    Global.connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = query2;
                    cmd.ExecuteNonQuery();
                    if (Global.editingExistingClass == true)
                    {
                        cmd.CommandText = query3;
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = query4;
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = query5;
                        cmd.ExecuteNonQuery();
                    }

                    //Clear selected IDs and the selection in the combo box
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return true;
        }

        //Deletes class and waiting list
        public static bool DeleteClass(ComboBox classDropDownList)
        {
            string patientsInClass = null;                  //Will hold details of patients in class
            List<string> dayList = new List<string>();      //Holds list of days
            string days = null;                             //Holds days in one string

            //SQL Query
            string query = "SELECT " +
                "[PATIENT].PAT_ID_Number, " +
                "[PATIENT].PAT_First_Name, " + 
                "[PATIENT].PAT_Last_Name " +
                "FROM [PATIENT] " +
                "INNER JOIN [PATIENT_CLASS] ON [PATIENT].PAT_ID_Number = [PATIENT_CLASS].PAT_ID_Number " +
                "WHERE CLASS_Name = '" + classDropDownList.SelectedValue + "';";

            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {
                try
                {
                    Global.connection.Open();
                    SQLiteDataReader reader = cmd.ExecuteReader();

                    //Getting all names of patients in the class to display in a messagebox before deleting
                    while (reader.Read())
                    {
                        string id = "ID: " + reader.GetString(0);
                        string name = " *** Name: " + reader.GetString(1) + " " + reader.GetString(2);
                        string full = id + name;
                        patientsInClass = patientsInClass + full + "\n";
                    }

                    //If patients are found then the title is put at the front of the string to be displayed
                    //in the are you sure messagebox
                    if(patientsInClass != null)
                    {
                        patientsInClass = "Patients in class:\n" + patientsInClass;
                    }

                    reader.Dispose();       //Dispose reader to use again
                    
                    //SQL Query   
                    query = "SELECT CTIME_Day FROM [CLASS TIME] WHERE CLASS_Name = '" + classDropDownList.SelectedValue + "';";
                    cmd.CommandText = query;
                    reader = cmd.ExecuteReader();

                    //Finds days to display in are you sure messagebox before deleting
                    while (reader.Read())
                    {
                        bool uniqueDayfound = true;                 //Trigger if a unique day is read
                        string dayread = reader.GetString(0);

                        //If the first day is read it is unique and dosn't need to be validated
                        if (dayList.Count > 0)
                        {
                            //Checks through list to validate unique days
                            foreach (string day in dayList)
                            {
                                if (dayread == day)
                                {
                                    uniqueDayfound = false;
                                }
                            }
                        }
                        //If unique day found then add to list
                        if (uniqueDayfound == true)
                        {
                            dayList.Add(dayread);
                        }
                    }
                    //Creates single string holding all days class is on
                    foreach (string day in dayList)
                    {
                        if (days == null)
                        {
                            days = day;
                        }
                        else
                        {
                            days = days + ", " + day;
                        }
                    }

                    //If patients are found then the title is put at the front of the string
                    if (days != null)
                    {
                        days = "Days class is on:\n" + days + "\n\n";
                    }
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            //Prompts user to if delete was a mistake
            DialogResult areYouSure = MessageBox.Show(days + patientsInClass + "Are you sure you want to delete this class?" +
                "\nDeleting is permanent", "Are You Sure", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (areYouSure == DialogResult.No)
            {
                return false;
            }

            //If user chooses to delete the class, waiting list, class time, patient_class and patient_Class waiting list
            //needs to be deleted which ciontains that class name
            else if (areYouSure == DialogResult.Yes)
            {
                query = "DELETE FROM [CLASS] WHERE CLASS_Name = '" + classDropDownList.SelectedValue + "';";
                string query2 = "DELETE FROM [CLASS WAITING LIST] WHERE CWLIST_Name = '" + classDropDownList.SelectedValue + " (Waiting)';";
                string query3 = "DELETE FROM [PATIENT_CLASS] WHERE CLASS_Name = '" + classDropDownList.SelectedValue + "';";
                string query4 = "DELETE FROM [PATIENT_CLASS WAITING LIST] WHERE CWLIST_Name = '" + classDropDownList.SelectedValue +
                    " (Waiting)';";
                string query5 = "DELETE FROM [CLASS TIME] WHERE CLASS_Name = '" + classDropDownList.SelectedValue + "';";

                using (Global.connection = new SQLiteConnection(Global.connectionString))
                using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
                {
                    try
                    {
                        Global.connection.Open();
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = query2;
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = query3;
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = query4;
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = query5;
                        cmd.ExecuteNonQuery();
                        Global.connection.Close();
                        MessageBox.Show("Class deleted", "Delete Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            return true;
        }

        //Used to populate combobox with class names
        public static DataTable PopulateClassDropdown(ComboBox classDropDownList)
        {
            //Datatable used as datasource for combobox
            DataTable classdt = new DataTable();
            //Will be used as the datatable columns
            string className = "";

            //Resets the datasource if there is an existing datasource
            classDropDownList.DataSource = null;
            string query = "SELECT * FROM [CLASS];";    //SQL Query

            //Connects to database and runs query
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
            {
                try
                {
                    Global.connection.Open();
                    classdt.Columns.Add("Class Name");
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    //The dropdownbox will show the class name
                    while (reader.Read())
                    {
                        className = reader.GetString(0);
                        classdt.Rows.Add(className);
                    }
                    classDropDownList.DataSource = classdt;
                    classDropDownList.ValueMember = "Class Name";
                    classDropDownList.DisplayMember = "Class Name";
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return classdt;
        }

        //****************************PATIENT_CLASS & PATIENT_CLASS WAITING LIST Tables****************************
        //DDL for PATIENT_CLASS:
        //"CREATE TABLE [PATIENT_CLASS]" +
        //"(PAT_ID_Number INTEGER NOT NULL," +
        //"CLASS_Name TEXT," +
        //"PATCLASS_Start_Date DATE NOT NULL," +
        //"PATCLASS_Finish_Date DATE," +
        //"PATCLASS_Weeks_In_Class NUMERIC," +
        //"FOREIGN KEY (PAT_ID_Number) REFERENCES [PATIENT](PAT_ID_Number) ON DELETE RESTRICT, " +
        //"FOREIGN KEY (CLASS_Name) REFERENCES [CLASS](CLASS_Name) ON DELETE RESTRICT);";

        //DDL for PATIENT_CLASS WAITING LIST:
        //"CREATE TABLE [PATIENT_CLASS WAITING LIST]" +
        //"(PAT_ID_Number INTEGER NOT NULL," +
        //"CWLIST_Name TEXT, " +
        //"FOREIGN KEY (PAT_ID_Number) REFERENCES [PATIENT](PAT_ID_Number) ON DELETE RESTRICT, " +
        //"FOREIGN KEY (CWLIST_Name) REFERENCES [CLASS WAITING LIST](CWLIST_Name) ON DELETE RESTRICT);";

        //Used to change start date of a patient_class
        public static bool EditStartDate(string patientID, TextBox className, DateTimePicker newStartDate)
        {
            string query = "UPDATE [PATIENT_CLASS] SET PATCLASS_Start_Date = @date WHERE PAT_ID_Number = " + patientID +
                " AND CLASS_Name = '" + className.Text + "';";      //SQL Query

            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {
                cmd.Parameters.Add("@date", Global.getDateString(newStartDate));
                try
                {
                    Global.connection.Open();
                    cmd.ExecuteNonQuery();
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return true;
        }

        //Populates patients in class and patients in waiting list in 2 dgvs in Add_Edit_Class
        public static void PopulatePatient_ClassDGV(ComboBox ClassDropDownList, DataGridView classPatientDGV, DataGridView classPatientWaitingDGV)
        {
            //Query for displaying patients in class
            string query = "SELECT [PATIENT].PAT_ID_Number, [PATIENT].PAT_First_Name, [PATIENT].PAT_Last_Name, [PATIENT_CLASS].PATCLASS_Start_Date " +
                "FROM [PATIENT_CLASS] " +
                "INNER JOIN [PATIENT] ON [PATIENT_CLASS].[PAT_ID_Number] = [PATIENT].[PAT_ID_Number] " +
                "WHERE CLASS_Name ='" + ClassDropDownList.SelectedValue + "';";    //SQL Query

            //Query for patients in waiting list
            string query2 = "SELECT [PATIENT_CLASS WAITING LIST].PAT_ID_Number, [PATIENT].PAT_First_Name, [PATIENT].PAT_Last_Name FROM [PATIENT_CLASS WAITING LIST] " +
                "INNER JOIN [PATIENT] ON [PATIENT_CLASS WAITING LIST].[PAT_ID_Number] = [PATIENT].[PAT_ID_Number] " +
                "WHERE CWLIST_Name = '" + ClassDropDownList.SelectedValue + " (Waiting)';";

            DataTable patient_Classdt = new DataTable();                    //Used to hold patients in the selected class
            DataTable patient_ClassWaitingListdt = new DataTable();         //Used to hold patients in selected class waiting list

            //Connect to database and run query
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {
                try
                {
                    Global.connection.Open();
                    patient_Classdt.Columns.Add("Patient ID");
                    patient_Classdt.Columns.Add("First Name");
                    patient_Classdt.Columns.Add("Last Name");
                    patient_Classdt.Columns.Add("Start Date");
                    patient_Classdt.Columns.Add("Weeks in Class");
                    SQLiteDataReader reader = cmd.ExecuteReader();

                    //Populating the rows for the datasource
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string firstName = reader.GetString(1);
                        string lastName = reader.GetString(2);
                        DateTime startDate = reader.GetDateTime(3);

                        //Weeks in class isn't a column in the table but is easy to calulate from the start date
                        double weeksInClass = (DateTime.Now - startDate).TotalDays / 7;
                        weeksInClass = Math.Round(weeksInClass, 1);                 //Round Week to one decimal place
                        patient_Classdt.Rows.Add(id, firstName, lastName, startDate.ToShortDateString(), weeksInClass);
                    }
                    classPatientDGV.DataSource = patient_Classdt;
                    //Dispose of reader to create new reader with second SQL statement.
                    //This will populate the waiting list of the selected class

                    patient_ClassWaitingListdt.Columns.Add("Patient ID");
                    patient_ClassWaitingListdt.Columns.Add("First Name");
                    patient_ClassWaitingListdt.Columns.Add("Last Name");
                    reader.Dispose();
                    cmd.CommandText = query2;
                    reader = cmd.ExecuteReader();

                    //Populating the rows for the datasource
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string firstName = reader.GetString(1);
                        string lastName = reader.GetString(2);
                        patient_ClassWaitingListdt.Rows.Add(id, firstName, lastName);
                    }

                    classPatientWaitingDGV.DataSource = patient_ClassWaitingListdt;
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        //Overloaded method used in Mark Class Attendance
        public static void PopulatePatient_ClassDGV(ComboBox ClassDropDownList, DataGridView classPatientDGV)
        {

            string query = "SELECT [PATIENT].PAT_ID_Number, [PATIENT].PAT_First_Name, [PATIENT].PAT_Last_Name, [PATIENT_CLASS].PATCLASS_Start_Date, " +
                " [PATIENT].PAT_Class_Attendance FROM [PATIENT_CLASS] " +
                "INNER JOIN [PATIENT] ON [PATIENT_CLASS].[PAT_ID_Number] = [PATIENT].[PAT_ID_Number] " +
                "WHERE CLASS_Name ='" + ClassDropDownList.SelectedValue + "';";    //SQL Query

            DataTable patient_Classdt = new DataTable();                    //Used to hold patients in the selected class

            //Connect to database and run query
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {
                try
                {
                    Global.connection.Open();
                    patient_Classdt.Columns.Add("Patient ID");
                    patient_Classdt.Columns.Add("First Name");
                    patient_Classdt.Columns.Add("Last Name");
                    patient_Classdt.Columns.Add("Start Date");
                    patient_Classdt.Columns.Add("Weeks in Class");
                    patient_Classdt.Columns.Add("Classes Attendance");
                    SQLiteDataReader reader = cmd.ExecuteReader();

                    //Populating the fields with the found patient details
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string firstName = reader.GetString(1);
                        string lastName = reader.GetString(2);
                        DateTime startDate = reader.GetDateTime(3);
                        int attendance = reader.GetInt32(4);
                        double weeksInClass = (DateTime.Now - startDate).TotalDays / 7;
                        weeksInClass = Math.Round(weeksInClass, 1);                 //Round Week to one decimal place
                        patient_Classdt.Rows.Add(id, firstName, lastName, startDate.ToShortDateString(), weeksInClass, attendance);
                    }
                    classPatientDGV.DataSource = patient_Classdt;
                    classPatientDGV.Columns["Patient ID"].ReadOnly = true;
                    classPatientDGV.Columns["First Name"].ReadOnly = true;
                    classPatientDGV.Columns["Last Name"].ReadOnly = true;
                    classPatientDGV.Columns["Start Date"].ReadOnly = true;
                    classPatientDGV.Columns["Weeks in Class"].ReadOnly = true;
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        //Adds patient to a class
        public static bool SavePatient_Class(ComboBox classDropDownList, ComboBox patientDropDownList, DateTimePicker startDate)
        {
            //SQL Query
            string query = "INSERT INTO [PATIENT_CLASS](PAT_ID_Number, CLASS_Name, PATCLASS_Start_Date) VALUES" +
                    "(@id, @className, @startDate);";

            //Connect to database and run query
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {

                //Setting parameters for input data
                cmd.Parameters.Add("@id", patientDropDownList.SelectedValue);
                cmd.Parameters.Add("@className", classDropDownList.SelectedValue);
                cmd.Parameters.Add("@startDate", Global.getDateString(startDate));

                try
                {
                    Global.connection.Open();
                    cmd.ExecuteNonQuery();
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                    return false;
                }
            }
            return true;
        }

        //Deletes patient from class
        public static bool DeletePatient_Class(ComboBox classDropDownList, DataGridView patient_ClassDGV)
        {
            string patID = "";      //Holds the ID for the patient to be deleted

            //Checks if there are patients in the class
            if (patient_ClassDGV.Rows.Count == 0)
            {
                MessageBox.Show("There are no patients currently in this class", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Checks if the user has selected a patient from the class
            if (patient_ClassDGV.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a patient from the class grid", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Gets patient id from patient_ClassDGV
            int cellIndex = patient_ClassDGV.SelectedCells[0].RowIndex;
            DataGridViewCell cellCollection = patient_ClassDGV.Rows[cellIndex].Cells[0];
            patID = cellCollection.Value.ToString();

            //Gives user a chocie to continue
            DialogResult areYouSure = MessageBox.Show("Are you sure you want to remove this patient from the class?",
            "Are You Sure", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (areYouSure == DialogResult.No)
            {
                return false;
            }

            //Deletes Patient from class
            else if (areYouSure == DialogResult.Yes)
            {
                //SQL Query
                string query = "DELETE FROM [PATIENT_CLASS] WHERE PAT_ID_Number =" + patID + " AND CLASS_Name = '" +
                    classDropDownList.SelectedValue + "';";

                using (Global.connection = new SQLiteConnection(Global.connectionString))
                using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
                {
                    try
                    {
                        Global.connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            return true;
        }

        //Saves patient to waiting list
        public static bool SavePatient_ClassWaitingList(ComboBox classDropDownList, ComboBox patientDropDownList)
        {
            //SQL Query
            string query = "INSERT INTO [PATIENT_CLASS WAITING LIST](PAT_ID_Number, CWLIST_Name) VALUES" +
                    "(@id, @className);";

            //Connect to database and run query
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {
                //Setting parameters for input data
                cmd.Parameters.Add("@id", patientDropDownList.SelectedValue);
                cmd.Parameters.Add("@className", classDropDownList.SelectedValue + " (Waiting)");

                try
                {
                    Global.connection.Open();
                    cmd.ExecuteNonQuery();
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                    return false;
                }
            }
            return true;
        }

        //Delets patient from waiting list and adds to class
        public static bool MoveWaitingPatient(DataGridView patient_ClassWaitingListDGV, ComboBox classDropDownList, DateTimePicker startDate)
        {
            //holds patient id to be moved
            string patID = "";

            //Checks if there are patients in the class
            if (patient_ClassWaitingListDGV.Rows.Count == 0)
            {
                MessageBox.Show("There are no patients currently in this class waiting list", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Checks if the user has selected a patient from the class
            if (patient_ClassWaitingListDGV.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a patient from the class waiting list grid", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Gets Selected patient from datagridview
            int cellIndex = patient_ClassWaitingListDGV.SelectedCells[0].RowIndex;
            DataGridViewCell cellCollection = patient_ClassWaitingListDGV.Rows[cellIndex].Cells[0];
            patID = cellCollection.Value.ToString();

            //SQl Querys.  Deleting and adding
            string query = "INSERT INTO [PATIENT_CLASS](PAT_ID_Number, CLASS_Name, PATCLASS_Start_Date) VALUES" +
                    "(@id, @className, @startDate);";
            string query2 = "DELETE FROM [PATIENT_CLASS WAITING LIST] WHERE PAT_ID_Number =" + patID + " AND CWLIST_Name = '" +
                    classDropDownList.SelectedValue + " (Waiting)';";

            //Connect to database and run query
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {
                //Setting parameters for input data
                cmd.Parameters.Add("@id", patID);
                cmd.Parameters.Add("@className", classDropDownList.SelectedValue);
                startDate.Value = DateTime.Now.Date;
                cmd.Parameters.Add("@startDate", Global.getDateString(startDate));

                try
                {
                    Global.connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = query2;
                    cmd.ExecuteNonQuery();
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                    return false;
                }
            }
            return true;
        }

        //Deletes patient from waiting list
        public static bool DeletePatient_ClassWaitingList(ComboBox classDropDownList, DataGridView patient_ClassWaitingListDGV)
        {
            string patID = "";      //Holds the ID for the patient to be deleted
            //Checks if there are patients in the class
            if (patient_ClassWaitingListDGV.Rows.Count == 0)
            {
                MessageBox.Show("There are no patients currently in this class waiting list", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Checks if the user has selected a patient from the class
            if (patient_ClassWaitingListDGV.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a patient from the class waiting list grid", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Gets patient id from patient_ClassWaitingListDGV
            int cellIndex = patient_ClassWaitingListDGV.SelectedCells[0].RowIndex;
            DataGridViewCell cellCollection = patient_ClassWaitingListDGV.Rows[cellIndex].Cells[0];
            patID = cellCollection.Value.ToString();

            //Gives user a chocie to continue
            DialogResult areYouSure = MessageBox.Show("Are you sure you want to remove this patient from the class waiting list?",
            "Are You Sure", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (areYouSure == DialogResult.No)
            {
                return false;
            }

            //Deletes Patient from class waiting list
            else if (areYouSure == DialogResult.Yes)
            {
                //SQL Query
                string query = "DELETE FROM [PATIENT_CLASS WAITING LIST] WHERE PAT_ID_Number =" + patID + " AND CWLIST_Name = '" +
                    classDropDownList.SelectedValue + " (Waiting)';";

                using (Global.connection = new SQLiteConnection(Global.connectionString))
                using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
                {
                    try
                    {
                        Global.connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            return true;
        }

    }
}
