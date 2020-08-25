using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using Finisar.SQLite;

namespace Patient_Database_and_Tracker
{
    class Patient_Database
    {
        //Shows list of all existing patients
        public static DataTable PopulatePatientDropdown(ComboBox patientDropDownList)
        {
            //Datatable used as datasource for combobox
            DataTable patientdt = new DataTable();
            //Will be used as the datatable columns
            int id = 0;
            string displayText = null;
            string query = null;

            //Resets the datasource if there is an existing datasource
            patientDropDownList.DataSource = null;

            //Depending on the sortDropdown variables the query will be different.  This is handled by the Sort_Lists_Click Event
            if (Global.sortDropdown == Global.ID)
            {
                query = "SELECT PAT_ID_Number, PAT_First_Name, PAT_Last_Name FROM [PATIENT] WHERE PAT_Current = 1 ORDER BY PAT_ID_Number DESC;";
            }
            else if (Global.sortDropdown == Global.NAME)
            {
                query = "SELECT PAT_ID_Number, PAT_First_Name, PAT_Last_Name FROM [PATIENT] WHERE PAT_Current = 1 ORDER BY PAT_First_Name, PAT_Last_Name;";
            }

            //Connects to database and runs query
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
            {
                try
                {
                    Global.connection.Open();
                    patientdt.Columns.Add("ID");
                    patientdt.Columns.Add("Display Text");
                    SQLiteDataReader reader = cmd.ExecuteReader();

                    //The dropdownbox will show the ID and the patients name
                    while (reader.Read())
                    {
                        id = reader.GetInt32(0);
                        if (Global.sortDropdown == Global.ID)
                        {
                            displayText = reader.GetInt32(0).ToString() + " *** Name: " + reader.GetString(1) +
                                " " + reader.GetString(2);
                            patientdt.Rows.Add(id, displayText);
                        }
                        else if (Global.sortDropdown == Global.NAME)
                        {
                            displayText = reader.GetString(1) + " " + reader.GetString(2) + " *** ID: " + reader.GetInt32(0).ToString() + " ";
                            patientdt.Rows.Add(id, displayText);
                        }
                    }

                    //Assign dgv to datasource
                    patientDropDownList.DataSource = patientdt;
                    patientDropDownList.ValueMember = "ID";
                    patientDropDownList.DisplayMember = "Display Text";
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return patientdt;
        }

        //Overloaded method used in Current_Patients
        //This method is the same but shows cuurent and non current patients
        public static DataTable PopulatePatientDropdown(ComboBox patientDropDownList, bool Current_Patients)
        {
            //Datatable used as datasource for combobox
            DataTable patientdt = new DataTable();
            //Will be used as the datatable columns
            int id = 0;
            string displayText = null;
            string query = null;

            //Resets the datasource if there is an existing datasource
            patientDropDownList.DataSource = null;

            //Depending on the sortDropdown variables the query will be different.  This is handled by the Sort_Lists_Click Event
            if (Global.sortDropdown == Global.ID)
            {
                query = "SELECT PAT_ID_Number, PAT_First_Name, PAT_Last_Name FROM [PATIENT] ORDER BY PAT_ID_Number DESC;";
            }
            else if (Global.sortDropdown == Global.NAME)
            {
                query = "SELECT PAT_ID_Number, PAT_First_Name, PAT_Last_Name FROM [PATIENT] ORDER BY PAT_First_Name, PAT_Last_Name;";
            }

            //Connects to database and runs query
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
            {
                try
                {
                    Global.connection.Open();
                    patientdt.Columns.Add("ID");
                    patientdt.Columns.Add("Display Text");
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    //The dropdownbox will show the ID and the patients name
                    while (reader.Read())
                    {
                        id = reader.GetInt32(0);
                        if (Global.sortDropdown == Global.ID)
                        {
                            displayText = reader.GetInt32(0).ToString() + " *** Name: " + reader.GetString(1) +
                                " " + reader.GetString(2);
                            patientdt.Rows.Add(id, displayText);
                        }
                        else if (Global.sortDropdown == Global.NAME)
                        {
                            displayText = reader.GetString(1) + " " + reader.GetString(2) + " *** ID: " + reader.GetInt32(0).ToString() + " ";
                            patientdt.Rows.Add(id, displayText);
                        }
                    }
                    patientDropDownList.DataSource = patientdt;
                    patientDropDownList.ValueMember = "ID";
                    patientDropDownList.DisplayMember = "Display Text";
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return patientdt;
        }

        //Overloaded Method to be used in Add_Edit_Group_Class
        //Shows current patients that are not currently in the selected class and wiaitng list
        public static DataTable PopulatePatientDropdown(ComboBox classDropDownList, ComboBox patientDropDownList)
        {
            //Datatable used as datasource for combobox
            DataTable patientdt = new DataTable();
            //Will be used as the datatable columns
            int id = 0;
            string displayText = null;
            string query = null;
            string query2 = null;
            string query3 = null;
            string notInPatientList = null;
            List<int> patient_ClassList = new List<int>();

            //Resets the datasource if there is an existing datasource
            patientDropDownList.DataSource = null;

            //Depending on the sortDropdown variables the query will be different.  This is handled by the Sort_Lists_Click Event

            query = "SELECT [PATIENT].PAT_ID_Number FROM [PATIENT] " +
                    "INNER JOIN [PATIENT_CLASS] ON [PATIENT].[PAT_ID_Number] = [PATIENT_CLASS].[PAT_ID_Number] " +
                    "WHERE [PATIENT_CLASS].[CLASS_Name] = '" + classDropDownList.SelectedValue + "' AND " +
                    "PAT_Current = 1;";

            query2 = "SELECT [PATIENT].PAT_ID_Number FROM [PATIENT] " +
                    "INNER JOIN [PATIENT_CLASS WAITING LIST] ON [PATIENT].[PAT_ID_Number] = [PATIENT_CLASS WAITING LIST].[PAT_ID_Number] " +
                    "WHERE [PATIENT_CLASS WAITING LIST].[CWLIST_Name] = '" + classDropDownList.SelectedValue + " (Waiting)' AND " +
                    "PAT_Current = 1;";


            //Connects to database and runs query
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
            {
                try
                {

                    Global.connection.Open();
                    SQLiteDataReader reader = cmd.ExecuteReader();

                    //Adds patients in selected class to a string list
                    while (reader.Read())
                    {
                        patient_ClassList.Add(reader.GetInt32(0));
                    }

                    reader.Dispose();
                    cmd.CommandText = query2;
                    reader = cmd.ExecuteReader();

                    //Adds patients in selected class waiting list to a string list
                    while (reader.Read())
                    {
                        patient_ClassList.Add(reader.GetInt32(0));
                    }

                    reader.Dispose();

                    int noComma = 0;            //No comma on the first patient

                    //Iterates through list to create a single string of numbers separated by a comma
                    foreach(int notInPatID in patient_ClassList)
                    {
                        //If first number then no comma
                        if (noComma == 0)
                        {
                            notInPatientList = notInPatID.ToString();
                            noComma++;
                        }
                        else
                        {
                            notInPatientList = notInPatientList + ", " + notInPatID.ToString();
                        }
                    }

                    //Queryes used to fill datasource of dropdown box
                    if (Global.sortDropdown == Global.ID)
                    {
                        query3 = "SELECT PAT_ID_Number, PAT_First_Name, PAT_Last_Name FROM [PATIENT] " +
                            "WHERE PAT_Current = 1 AND PAT_ID_Number NOT IN (" + notInPatientList + ") ORDER BY PAT_ID_Number DESC;";
                    }
                    else if (Global.sortDropdown == Global.NAME)
                    {
                        query3 = "SELECT PAT_ID_Number, PAT_First_Name, PAT_Last_Name FROM [PATIENT] " +
                            "WHERE PAT_Current = 1 AND PAT_ID_Number NOT IN (" + notInPatientList + ") ORDER BY PAT_First_Name, PAT_Last_Name;";
                    }
                    patientdt.Columns.Add("ID");
                    patientdt.Columns.Add("Display Text");
                    cmd.CommandText = query3;
                    reader = cmd.ExecuteReader();

                    //The dropdownbox will show the ID and the patients name
                    while (reader.Read())
                    {
                        id = reader.GetInt32(0);
                        if (Global.sortDropdown == Global.ID)
                        {
                            displayText = reader.GetInt32(0).ToString() + " *** Name: " + reader.GetString(1) +
                                " " + reader.GetString(2);
                            patientdt.Rows.Add(id, displayText);
                        }
                        else if (Global.sortDropdown == Global.NAME)
                        {
                            displayText = reader.GetString(1) + " " + reader.GetString(2) + " *** ID: " + reader.GetInt32(0).ToString() + " ";
                            patientdt.Rows.Add(id, displayText);
                        }
                    }
                    patientDropDownList.DataSource = patientdt;
                    patientDropDownList.ValueMember = "ID";
                    patientDropDownList.DisplayMember = "Display Text";
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return patientdt;
        }

        //Populates fields of exisiting patient details.  Only used in add/Edit Patient
        public static void PatientSelectedAddEdit(ComboBox patientDropDownList, TextBox id, DateTimePicker dateReffered,
            DateTimePicker dateBirth, ComboBox gender, TextBox firstName, TextBox lastName, TextBox address,
            TextBox suburb , TextBox postCode, TextBox state, TextBox homePhone, TextBox mobilePhone, TextBox email,
            TextBox notes, ComboBox refGPDropDownList, CheckBox gpLetterCB)
        {
            int refGPIDLocal = 0;               //Will Hold the id number of the selected patients Reffering GP
            int refgpSelectedIndex = 0;         //Will Hold the selected index of the Reffering_GP_Dropdown_DropDown

            string query = "SELECT * FROM [PATIENT] WHERE PAT_ID_Number =" + patientDropDownList.SelectedValue + ";";    //SQL Query

            //Connect to database and run query
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {
                try
                {
                    Global.connection.Open();
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    
                    //Populating the fields with the found patient details
                    while (reader.Read())
                    {
                        id.Text = reader.GetInt32(0).ToString();
                        firstName.Text = reader.GetString(1);
                        lastName.Text = reader.GetString(2);
                        address.Text = reader.GetString(3);
                        suburb.Text = reader.GetString(4);
                        postCode.Text = reader.GetString(5);
                        state.Text = reader.GetString(6);
                        homePhone.Text = reader.GetString(7);
                        mobilePhone.Text = reader.GetString(8);
                        email.Text = reader.GetString(9);
                        refGPIDLocal = reader.GetInt32(10);                     //Sets a local variable to hold the value of the REFGPID
                        dateReffered.Value = reader.GetDateTime(11);
                        dateBirth.Value = reader.GetDateTime(12);
                        gender.SelectedIndex = gender.FindString(reader.GetString(13));
                        notes.Text = reader.GetString(14);
                        int gpLetter = reader.GetInt32(15);
                        if (gpLetter == 0)
                        {
                            gpLetterCB.Checked = false;
                        }
                        else if (gpLetter == 1)
                        {
                            gpLetterCB.Checked = true;
                        }

                    }
                    Global.editingExistingPatient = true;     //Creates an edit session
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            DataTable refgpdt = Reffering_GP_Database.PopulateRefferingGPDropdown(refGPDropDownList);
            
            int counter = 0;                //Counter will be used to find refgpSelectedIndex

            //Loop through datatable to find the row with the selected ID
            foreach (DataRow row in refgpdt.Rows)
            {
                if (int.Parse(row[0].ToString()) == refGPIDLocal)
                {
                    refgpSelectedIndex = counter;
                    break;
                }
                counter++;
            }
            refGPDropDownList.SelectedIndex = refgpSelectedIndex;       //Selects the correct Reffering GP from Reffering_GP_Dropdown
        }

        //Populates fields of exisiting patient details.  Only used in Add_Edit_Consultation_1
        public static void PatientSelectedConsultation1(ComboBox patientDropDownList, TextBox id, TextBox date,
            TextBox firstName, TextBox lastName, TextBox refferingGP)
        {
            int refGPIDLocal = 0;               //Will Hold the id number of the selected patients Reffering GP

            string query = "SELECT PAT_ID_Number, PAT_Date_Reffered, PAT_First_Name, PAT_Last_Name, REFGP_ID_Number FROM [PATIENT] WHERE PAT_ID_Number =" +
                patientDropDownList.SelectedValue + ";";    //SQL Query

            //Connect to database and run query
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {
                try
                {
                    Global.connection.Open();
                    SQLiteDataReader reader = cmd.ExecuteReader();

                    //Populating the fields with the found patient details
                    while (reader.Read())
                    {
                        Global.consPatientID = reader.GetInt32(0);                      //Used to populate fields in Consultation_2
                        Global.consPatientName = reader.GetString(2) + " " +
                            reader.GetString(3);                                        //Used to populate fields in Consultation_2
                        id.Text = reader.GetInt32(0).ToString();
                        date.Text = reader.GetDateTime(1).ToShortDateString();
                        firstName.Text = reader.GetString(2);
                        lastName.Text = reader.GetString(3);
                        refGPIDLocal = reader.GetInt32(4);                              //Sets a local variable to hold the value of the REFGPID
                    }
                    reader.Dispose();

                    //Will populate the Reffering GP ID and Name
                    cmd.CommandText = "SELECT REFGP_ID_Number, REFGP_First_Name, REFGP_Last_Name FROM [REFFERING GP] WHERE REFGP_ID_Number =" +
                        refGPIDLocal + ";";
                    reader = cmd.ExecuteReader();

                    //Used to display reffering gp details
                    while (reader.Read())
                    {
                        refferingGP.Text = "GP ID: " + reader.GetInt32(0) + " *** " + reader.GetString(1) +
                            " " + reader.GetString(2);
                    }
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        //Populates fields of exisiting patient details.  Only used in Generate_Letters
        public static void patientSelectedGenerateLetter(ComboBox patientDropDownList, TextBox id, TextBox date,
            TextBox firstName, TextBox lastName, TextBox refferingGP, CheckBox gpLetterCB)
        {
            //Piggy backs on an existing method for patient selected
            PatientSelectedConsultation1(patientDropDownList, id, date, firstName, lastName, refferingGP);
            string query = "SELECT PAT_GP_Letter_Sent FROM [PATIENT] WHERE PAT_ID_Number =" +
                patientDropDownList.SelectedValue + ";";    //SQL Query

            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {
                try
                {
                    Global.connection.Open();
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    int gpLetter = 0;

                    //Populating the fields with the found patient details
                    while (reader.Read())
                    {
                        gpLetter = reader.GetInt32(0);
                    }
                    Global.connection.Close();
                    if (gpLetter == 0)
                    {
                        gpLetterCB.Checked = false;
                    }
                    else if (gpLetter == 1)
                    {
                        gpLetterCB.Checked = true;
                    }
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        //Populates fields of exisiting patient details.  Only used in Delete_Patient
        public static void PatientSelectedDelete(ComboBox patientDropDownList, TextBox id,
            TextBox date, TextBox dob, TextBox gender, TextBox firstName, TextBox lastName, TextBox address,
            TextBox suburb, TextBox postCode, TextBox state, TextBox homePhone, TextBox mobilePhone,
            TextBox email, TextBox refferingGP, TextBox notes, CheckBox currentPatientCB, CheckBox gpLetterCB)
        {
            int refGPIDLocal = 0;
            string query = "SELECT * FROM [PATIENT] WHERE PAT_ID_Number =" +
                patientDropDownList.SelectedValue + ";";       
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {
                try
                {
                    Global.connection.Open();
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    //Populating the fields with the found patient
                    while (reader.Read())
                    {
                        id.Text = reader.GetInt32(0).ToString();
                        firstName.Text = reader.GetString(1);
                        lastName.Text = reader.GetString(2);
                        address.Text = reader.GetString(3);
                        suburb.Text = reader.GetString(4);
                        postCode.Text = reader.GetString(5);
                        state.Text = reader.GetString(6);
                        homePhone.Text = reader.GetString(7);
                        mobilePhone.Text = reader.GetString(8);
                        email.Text = reader.GetString(9);
                        refGPIDLocal = reader.GetInt32(10);    //Sets a local variable to hold the value of the REFGPID
                        date.Text = reader.GetDateTime(11).ToShortDateString();
                        dob.Text = reader.GetDateTime(12).ToShortDateString();
                        gender.Text = reader.GetString(13);
                        notes.Text = reader.GetString(14);
                        int gpLetter = reader.GetInt32(15);
                        if (gpLetter == 1)
                        {
                            gpLetterCB.Checked = true;
                        }
                        else if (gpLetter == 0)
                        {
                            gpLetterCB.Checked = false;
                        }
                        int currentPatient = reader.GetInt32(16);
                        if (currentPatient == 1)
                        {
                            currentPatientCB.Checked = true;
                        }
                        else if (currentPatient == 0)
                        {
                            currentPatientCB.Checked = false;
                        }
                    }
                    reader.Dispose();
                    string query2 = "SELECT REFGP_ID_Number, REFGP_First_Name, REFGP_Last_Name FROM [REFFERING GP] WHERE REFGP_ID_Number =" + refGPIDLocal + ";";
                    cmd.CommandText = query2;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        refferingGP.Text = "ID: " + reader.GetInt32(0) + " *** " + reader.GetString(1) +
                            " " + reader.GetString(2);
                    }
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        //When a user clicks a selection from the dropdown box all the values of the selected patient will
        //populate the fields.  The program will also know that this is an edit session and not a new patient
        public static void PatientSelectedCurrentPatients(ComboBox patientDropDown, TextBox id, TextBox firstName, TextBox lastName,
            CheckBox currentPatient, Button saveButton)
        {
            string query = "SELECT PAT_ID_Number, PAT_First_Name, PAT_Last_Name, PAT_Current FROM [PATIENT] WHERE PAT_ID_Number = " +
                patientDropDown.SelectedValue + ";";    //SQL Query

            //Connect to database and run query
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {
                try
                {
                    Global.connection.Open();
                    SQLiteDataReader reader = cmd.ExecuteReader();

                    //Populating the fields with the found patient details
                    while (reader.Read())
                    {
                        id.Text = reader.GetInt32(0).ToString();
                        firstName.Text = reader.GetString(1);
                        lastName.Text = reader.GetString(2);
                        //Sets checkbox based on sqlite bool value
                        if (reader["PAT_Current"] == DBNull.Value || Convert.ToInt32(reader["PAT_Current"]) == 0)
                        {
                            currentPatient.Checked = false;
                        }
                        else if (Convert.ToInt32(reader["PAT_Current"]) == 1)
                        {
                            currentPatient.Checked = true;
                        }
                    }

                    currentPatient.Enabled = true;
                    saveButton.Enabled = true;
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        //Saves new patient or updates exisitng patient
        public static Boolean SavePatient(ComboBox patientDropDownList, TextBox id, DateTimePicker dateReffered, DateTimePicker dateBirth, ComboBox gender,
            TextBox firstName, TextBox lastName, TextBox address, TextBox suburb, TextBox postCode, TextBox state, TextBox homePhone, TextBox mobilePhone,
            TextBox email, TextBox notes, ComboBox refGPDropDownList, CheckBox currentPatientCB, CheckBox gpLetterCB)
        {
            //Setting variables for checkboxes in terms of integer
            //Used to determine if this is a current patient.  1 = true, 0 = false
            int currentPatient = 1;             
            if (currentPatientCB.Checked == false)
            {
                currentPatient = 0;
            }
            int gpLetter = 0;
            if (gpLetterCB.Checked == true)
            {
                gpLetter = 1;
            }

            //Can only save data if fields aren't read only
            //if user clicks save changes while fields are read only an error message will show
            if (firstName.ReadOnly == true)
            {
                MessageBox.Show("Click add new patient or edit existing patient to save changes", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //First and last name cannot be null.  Error message if they are not entered
            if (String.IsNullOrWhiteSpace(firstName.Text) || String.IsNullOrWhiteSpace(lastName.Text))
            {
                MessageBox.Show("Patient must have a first and last name", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Patient must have a gender.  Error message if they are not entered
            if (gender.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a gender", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (refGPDropDownList.SelectedIndex == -1)
            {
                MessageBox.Show("Patient must have a reffering gp", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            string query = "";              //Will be SQL Query
            string query2 = "";             //If a new patient is added a second query is used
            
            //query if adding a new patient
            if (Global.editingExistingPatient == false)
            {
                query = "INSERT INTO [PATIENT](PAT_First_Name, PAT_Last_Name, PAT_Address, PAT_Suburb, PAT_Post_Code, PAT_State, PAT_Home_Phone, PAT_Mobile_Phone, PAT_Email_Address, REFGP_ID_Number, PAT_Date_Reffered, PAT_Date_of_Birth, PAT_Gender, PAT_Notes, PAT_Current, PAT_GP_Letter_Sent) VALUES" +
                        "(@firstName, @lastName, @address, @suburb, @postCode, @state, @homePhone, @mobilePhone, @email, @gpid, @dateReffered, @dateBirth, @gender, @notes, @current, @gpLetter);";
                query2 = "SELECT last_insert_rowid() FROM PATIENT;";
            }
            
            //query if editing an existing patient
            else
            {
                query = "UPDATE [PATIENT] SET PAT_First_Name = @firstName, PAT_Last_Name = @lastName, PAT_Address = @address, PAT_Suburb = @suburb," +
                    "PAT_Post_Code = @postcode, PAT_State = @state, PAT_Home_Phone = @homePhone, PAT_Mobile_Phone = @mobilePhone," +
                    "PAT_Email_Address = @email, REFGP_ID_Number = @gpid, PAT_Date_Reffered = @dateReffered, PAT_Date_of_Birth = @dateBirth, PAT_Gender = @gender, PAT_Notes = @notes, PAT_Current = @current, PAT_GP_Letter_Sent = @gpLetter WHERE PAT_ID_Number =" + patientDropDownList.SelectedValue + ";";
            }
            
            //Connect to database and run query
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {

                //Set Parameters for input data
                //Handles if null values are in input fields
                cmd.Parameters.Add("@firstName", firstName.Text);
                cmd.Parameters.Add("@lastName", lastName.Text);
                cmd.Parameters.Add("@dateReffered", Global.getDateString(dateReffered));
                cmd.Parameters.Add("@dateBirth", Global.getDateString(dateBirth));
                cmd.Parameters.Add("@gpid", refGPDropDownList.SelectedValue);
                cmd.Parameters.Add("@gender", gender.SelectedItem.ToString());
                cmd.Parameters.Add("@current", currentPatient);
                cmd.Parameters.Add("@gpLetter", gpLetter);

                //Sets parameters of null fields
                if (String.IsNullOrWhiteSpace(address.Text))
                {
                    cmd.Parameters.Add("@address", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.Add("@address", address.Text);
                }
                if (String.IsNullOrWhiteSpace(suburb.Text))
                {
                    cmd.Parameters.Add("@suburb", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.Add("@suburb", suburb.Text);
                }
                if (String.IsNullOrWhiteSpace(postCode.Text))
                {
                    cmd.Parameters.Add("@postCode", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.Add("@postCode", postCode.Text);
                }
                if (String.IsNullOrWhiteSpace(state.Text))
                {
                    cmd.Parameters.Add("@state", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.Add("@state", state.Text);
                }
                if (String.IsNullOrWhiteSpace(homePhone.Text))
                {
                    cmd.Parameters.Add("@homePhone", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.Add("@homePhone", homePhone.Text);
                }
                if (String.IsNullOrWhiteSpace(mobilePhone.Text))
                {
                    cmd.Parameters.Add("@mobilePhone", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.Add("@mobilePhone", mobilePhone.Text);
                }
                if (String.IsNullOrWhiteSpace(email.Text))
                {
                    cmd.Parameters.Add("@email", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.Add("@email", email.Text);
                }
                if (String.IsNullOrWhiteSpace(notes.Text))
                {
                    cmd.Parameters.Add("@notes", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.Add("@notes", notes.Text);
                }

                try
                {
                    Global.connection.Open();

                    //Gets new ID if new patient
                    if (Global.editingExistingPatient == false)
                    {
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = query2;
                        id.Text = cmd.ExecuteScalar().ToString();
                    }

                    //executes non query if existing
                    else
                    {
                        cmd.ExecuteNonQuery();
                    }

                    //Clear selected IDs and the selection in the combo box
                    patientDropDownList.DataSource = null;

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

        //Saves changes to field Current Patient of patient
        public static void UpdateCurrentPatient(ComboBox patientDropDown, CheckBox currentPatientCB, Button saveButton)
        {
            //Holds sqlite value 1 = true, o = false
            int current;
            if (currentPatientCB.Checked == true)
            {
                current = 1;
            }
            else
            {
                current = 0;
            }

            string query = "UPDATE [PATIENT] SET PAT_Current = @current WHERE PAT_ID_Number = " +
                patientDropDown.SelectedValue + ";";    //SQL Query
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {
                try
                {
                    cmd.Parameters.Add("@current", current);

                    Global.connection.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Update successful", "Success", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                    //Clears datasource and disables controls
                    patientDropDown.DataSource = null;
                    currentPatientCB.Enabled = false;
                    saveButton.Enabled = false;
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        //Saves changes to field GP Letter Sent of patient
        public static void UpdateGPLetterSent(CheckBox gpLetterCB, ComboBox patientDropDownList)
        {
            //Setting variables for checkboxes in terms of integer
            //Used to determine if this is a current patient.  1 = true, 0 = false
            int gpLetter = 0;
            if (gpLetterCB.Checked == true)
            {
                gpLetter = 1;
            }
            string query = "UPDATE [PATIENT] SET PAT_GP_Letter_Sent = @gpLetter WHERE PAT_ID_Number =" +
                patientDropDownList.SelectedValue + ";";        //SQL Query

            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {
                try
                {
                    Global.connection.Open();
                    cmd.Parameters.Add("@gpLetter", gpLetter);
                    cmd.ExecuteNonQuery();
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        //Deletes patient from database
        public static void DeletePatient(ComboBox patientDropDownList, TextBox id)
        {
            //Shows error message if no patient selected
            if (id.Text == "")
            {
                MessageBox.Show("Please choose a patient from the dropdown list", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = "SELECT CONS_ID_Number FROM [CONSULTATION] WHERE PAT_ID_Number =" +
                patientDropDownList.SelectedValue + ";";        //SQL Query

            List<string> consultationList = new List<string>(); //Holds list of consultations
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {
                try
                {
                    Global.connection.Open();
                    SQLiteDataReader reader = cmd.ExecuteReader();

                    //Assignes consultation details to a list
                    while (reader.Read())
                    {
                        string consId = "Consultation ID: " + reader.GetString(0);
                        consultationList.Add(consId);
                    }
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                Global.connection.Close();

                //If consultations are found then generate a single string containing all consultations
                if (consultationList.Count > 0)
                {
                    string consultations = null;

                    //Iterate through list and generate single string
                    foreach (string consultation in consultationList)
                    {
                        consultations = consultations + consultation + "\n";
                    }
                    //Error message cannot delete if consultations are assigned to patients
                    MessageBox.Show("Unable to delete patient.  The following consultations have been assigned to this patient:\n" + consultations,
                        "Cannot Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            //Prompts user if they want to continue
            DialogResult areYouSure = MessageBox.Show("Are you sure you want to delete this patient?\nDeleting is permanent",
                "Are You Sure", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            //Cancel if user chooses no
            if (areYouSure == DialogResult.No)
            {
                return;
            }

            //Deletes patient and any patient_Class relationships
            else if (areYouSure == DialogResult.Yes)
            {
                //SQL Queries
                query = "DELETE FROM [PATIENT] WHERE PAT_ID_Number =" + patientDropDownList.SelectedValue + ";";
                string query2 = "DELETE FROM [PATIENT_CLASS] WHERE PAT_ID_Number =" + patientDropDownList.SelectedValue + ";";
                string query3 = "DELETE FROM [PATIENT_CLASS WAITING LIST] WHERE PAT_ID_Number =" + patientDropDownList.SelectedValue + ";";

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
                        Global.connection.Close();
                        MessageBox.Show("Patient deleted", "Delete Successful",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        patientDropDownList.DataSource = null;
                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }

        //Will be used with other methods to view all and view searched data
        private static void ViewPatients(DataGridView patientDataGridView, string query)
        {
            DataTable patientdt = new DataTable();
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            {
                    Global.connection.Open();
                    SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(query, Global.connection);
                    dataAdapter.Fill(patientdt);
                    CreateHeaders(patientdt);
                    patientDataGridView.DataSource = patientdt;
                    Global.connection.Close();
            }
        }
        //Used in ViewPatients Method.  Sets the headers of the datatable
        private static void CreateHeaders(DataTable patientdt)
        {
            patientdt.Columns[0].ColumnName = "Patient ID";
            patientdt.Columns[1].ColumnName = "Patient First Name";
            patientdt.Columns[2].ColumnName = "Patient Last Name";
            patientdt.Columns[3].ColumnName = "Patient Address";
            patientdt.Columns[4].ColumnName = "Patient Suburb";
            patientdt.Columns[5].ColumnName = "Patient Post Code";
            patientdt.Columns[6].ColumnName = "Patient State";
            patientdt.Columns[7].ColumnName = "Patient Home Phone";
            patientdt.Columns[8].ColumnName = "Patient Mobile";
            patientdt.Columns[9].ColumnName = "Patient Email";
            patientdt.Columns[10].ColumnName = "Patient Date Reffered";
            patientdt.Columns[11].ColumnName = "Patient DOB";
            patientdt.Columns[12].ColumnName = "Patient Gender";
            patientdt.Columns[13].ColumnName = "GP Letter Sent";
            patientdt.Columns[14].ColumnName = "Class Attendance";
            patientdt.Columns[15].ColumnName = "Patient Notes";
            patientdt.Columns[16].ColumnName = "GP ID";
            patientdt.Columns[17].ColumnName = "GP First Name";
            patientdt.Columns[18].ColumnName = "GP Last Name";
            patientdt.Columns[19].ColumnName = "Practice Name";
            patientdt.Columns[20].ColumnName = "Practice Address";
            patientdt.Columns[21].ColumnName = "Practice Suburb";
            patientdt.Columns[22].ColumnName = "Practice Post Code";
            patientdt.Columns[23].ColumnName = "Practice State";
        }

        //Creates sql query for current patients
        public static string CreateCurrentQuery(string columnName, string searchName, string searchOperator, bool stringSearch)
        {
            string query = null;

            //Searching strings
            if (stringSearch == true)
            {
                query = "SELECT " +
                    "PATIENT.[PAT_ID_Number], " +
                    "PATIENT.[PAT_First_Name], " +
                    "PATIENT.[PAT_Last_Name], " +
                    "PATIENT.[PAT_Address], " +
                    "PATIENT.[PAT_Suburb], " +
                    "PATIENT.[PAT_Post_Code], " +
                    "PATIENT.[PAT_State], " +
                    "PATIENT.[PAT_Home_Phone], " +
                    "PATIENT.[PAT_Mobile_Phone], " +
                    "PATIENT.[PAT_Email_Address], " +
                    "PATIENT.[PAT_Date_Reffered], " +
                    "PATIENT.[PAT_Date_of_Birth], " +
                    "PATIENT.[PAT_Gender], " +
                    "PATIENT.[PAT_GP_Letter_Sent], " +
                    "PATIENT.[PAT_Class_Attendance]," +
                    "PATIENT.[PAT_Notes], " +
                    "[REFFERING GP].[REFGP_ID_Number], " +
                    "[REFFERING GP].[REFGP_First_Name], " +
                    "[REFFERING GP].[REFGP_Last_Name], " +
                    "[GENERAL PRACTICE].PRAC_Name, " +
                    "PRAC_Address, " +
                    "PRAC_Suburb, " +
                    "PRAC_Post_Code, " +
                    "PRAC_State " +
                    "FROM [GENERAL PRACTICE] " +
                    "INNER JOIN [REFFERING GP] ON [GENERAL PRACTICE].[PRAC_Name] =  [REFFERING GP].[PRAC_Name]" +
                    "INNER JOIN PATIENT ON [REFFERING GP].[REFGP_ID_Number] = PATIENT.[REFGP_ID_Number]" +
                    "WHERE " +
                    columnName + " " + searchOperator + " '" + searchName + "' AND " +
                    "PAT_Current = 1 " +
                    "ORDER BY PAT_ID_Number;";
            }

            //Searching numbers
            else if (stringSearch == false)
            {
                query = "SELECT " +
                    "PATIENT.[PAT_ID_Number], " +
                    "PATIENT.[PAT_First_Name], " +
                    "PATIENT.[PAT_Last_Name], " +
                    "PATIENT.[PAT_Address], " +
                    "PATIENT.[PAT_Suburb], " +
                    "PATIENT.[PAT_Post_Code], " +
                    "PATIENT.[PAT_State], " +
                    "PATIENT.[PAT_Home_Phone], " +
                    "PATIENT.[PAT_Mobile_Phone], " +
                    "PATIENT.[PAT_Email_Address], " +
                    "PATIENT.[PAT_Date_Reffered], " +
                    "PATIENT.[PAT_Date_of_Birth], " +
                    "PATIENT.[PAT_Gender], " +
                    "PATIENT.[PAT_GP_Letter_Sent], " +
                    "PATIENT.[PAT_Class_Attendance]," +
                    "PATIENT.[PAT_Notes], " +
                    "[REFFERING GP].[REFGP_ID_Number], " +
                    "[REFFERING GP].[REFGP_First_Name], " +
                    "[REFFERING GP].[REFGP_Last_Name], " +
                    "[GENERAL PRACTICE].PRAC_Name, " +
                    "PRAC_Address, " +
                    "PRAC_Suburb, " +
                    "PRAC_Post_Code, " +
                    "PRAC_State " +
                    "FROM [GENERAL PRACTICE] " +
                    "INNER JOIN [REFFERING GP] ON [GENERAL PRACTICE].[PRAC_Name] =  [REFFERING GP].[PRAC_Name]" +
                    "INNER JOIN PATIENT ON [REFFERING GP].[REFGP_ID_Number] = PATIENT.[REFGP_ID_Number]" +
                    "WHERE " +
                    columnName + " " + searchOperator + " " + searchName + " AND " +
                    "PAT_Current = 1 " +
                    "ORDER BY PAT_ID_Number;";
            }
            return query;
        }
        
        //Creates sql query for current patients searching 2 where clauses
        public static string CreateCurrentQuery(string columnName, string searchName, string searchOperator, string searchName2, string searchOperator2)
        {
            string query = "SELECT " +
                "PATIENT.[PAT_ID_Number], " +
                "PATIENT.[PAT_First_Name], " +
                "PATIENT.[PAT_Last_Name], " +
                "PATIENT.[PAT_Address], " +
                "PATIENT.[PAT_Suburb], " +
                "PATIENT.[PAT_Post_Code], " +
                "PATIENT.[PAT_State], " +
                "PATIENT.[PAT_Home_Phone], " +
                "PATIENT.[PAT_Mobile_Phone], " +
                "PATIENT.[PAT_Email_Address], " +
                "PATIENT.[PAT_Date_Reffered], " +
                "PATIENT.[PAT_Date_of_Birth], " +
                "PATIENT.[PAT_Gender], " +
                "PATIENT.[PAT_GP_Letter_Sent], " +
                "PATIENT.[PAT_Class_Attendance]," +
                "PATIENT.[PAT_Notes], " +
                "[REFFERING GP].[REFGP_ID_Number], " +
                "[REFFERING GP].[REFGP_First_Name], " +
                "[REFFERING GP].[REFGP_Last_Name], " +
                "[GENERAL PRACTICE].PRAC_Name, " +
                "PRAC_Address, " +
                "PRAC_Suburb, " +
                "PRAC_Post_Code, " +
                "PRAC_State " +
                "FROM [GENERAL PRACTICE] " +
                "INNER JOIN [REFFERING GP] ON [GENERAL PRACTICE].[PRAC_Name] =  [REFFERING GP].[PRAC_Name]" +
                "INNER JOIN PATIENT ON [REFFERING GP].[REFGP_ID_Number] = PATIENT.[REFGP_ID_Number]" +
                "WHERE " +
                columnName + " " + searchOperator + " '" + searchName + "' AND " +
                columnName + " " + searchOperator2 + " '" + searchName2 + "' AND " +
                "PAT_Current = 1 " +
                "ORDER BY PAT_ID_Number;";
            return query;
        }

        //Creates sql query for non current patients
        public static string CreateNonCurrentQuery(string columnName, string searchName, string searchOperator, bool stringSearch)
        {
            string query = null;

            //search string
            if (stringSearch == true)
            {
                query = "SELECT " +
                    "PATIENT.[PAT_ID_Number], " +
                    "PATIENT.[PAT_First_Name], " +
                    "PATIENT.[PAT_Last_Name], " +
                    "PATIENT.[PAT_Address], " +
                    "PATIENT.[PAT_Suburb], " +
                    "PATIENT.[PAT_Post_Code], " +
                    "PATIENT.[PAT_State], " +
                    "PATIENT.[PAT_Home_Phone], " +
                    "PATIENT.[PAT_Mobile_Phone], " +
                    "PATIENT.[PAT_Email_Address], " +
                    "PATIENT.[PAT_Date_Reffered], " +
                    "PATIENT.[PAT_Date_of_Birth], " +
                    "PATIENT.[PAT_Gender], " +
                    "PATIENT.[PAT_GP_Letter_Sent], " +
                    "PATIENT.[PAT_Class_Attendance]," +
                    "PATIENT.[PAT_Notes], " +
                    "[REFFERING GP].[REFGP_ID_Number], " +
                    "[REFFERING GP].[REFGP_First_Name], " +
                    "[REFFERING GP].[REFGP_Last_Name], " +
                    "[GENERAL PRACTICE].PRAC_Name, " +
                    "PRAC_Address, " +
                    "PRAC_Suburb, " +
                    "PRAC_Post_Code, " +
                    "PRAC_State " +
                    "FROM [GENERAL PRACTICE] " +
                    "INNER JOIN [REFFERING GP] ON [GENERAL PRACTICE].[PRAC_Name] =  [REFFERING GP].[PRAC_Name]" +
                    "INNER JOIN PATIENT ON [REFFERING GP].[REFGP_ID_Number] = PATIENT.[REFGP_ID_Number]" +
                    "WHERE " +
                    columnName + " " + searchOperator + " '" + searchName + "' AND " +
                    "PAT_Current = 0 " +
                    "ORDER BY PAT_ID_Number;";
            }

            //search number
            else if (stringSearch == false)
            {
                query = "SELECT " +
                    "PATIENT.[PAT_ID_Number], " +
                    "PATIENT.[PAT_First_Name], " +
                    "PATIENT.[PAT_Last_Name], " +
                    "PATIENT.[PAT_Address], " +
                    "PATIENT.[PAT_Suburb], " +
                    "PATIENT.[PAT_Post_Code], " +
                    "PATIENT.[PAT_State], " +
                    "PATIENT.[PAT_Home_Phone], " +
                    "PATIENT.[PAT_Mobile_Phone], " +
                    "PATIENT.[PAT_Email_Address], " +
                    "PATIENT.[PAT_Date_Reffered], " +
                    "PATIENT.[PAT_Date_of_Birth], " +
                    "PATIENT.[PAT_Gender], " +
                    "PATIENT.[PAT_GP_Letter_Sent], " +
                    "PATIENT.[PAT_Class_Attendance]," +
                    "PATIENT.[PAT_Notes], " +
                    "[REFFERING GP].[REFGP_ID_Number], " +
                    "[REFFERING GP].[REFGP_First_Name], " +
                    "[REFFERING GP].[REFGP_Last_Name], " +
                    "[GENERAL PRACTICE].PRAC_Name, " +
                    "PRAC_Address, " +
                    "PRAC_Suburb, " +
                    "PRAC_Post_Code, " +
                    "PRAC_State " +
                    "FROM [GENERAL PRACTICE] " +
                    "INNER JOIN [REFFERING GP] ON [GENERAL PRACTICE].[PRAC_Name] =  [REFFERING GP].[PRAC_Name]" +
                    "INNER JOIN PATIENT ON [REFFERING GP].[REFGP_ID_Number] = PATIENT.[REFGP_ID_Number]" +
                    "WHERE " +
                    columnName + " " + searchOperator + " " + searchName + " AND " +
                    "PAT_Current = 0 " +
                    "ORDER BY PAT_ID_Number;";
            }
            return query;
        }

        //Creates sql query for non current patients with 2 where clauses
        public static string CreateNonCurrentQuery(string columnName, string searchName, string searchOperator, string searchName2, string searchOperator2)
        {
            string query = "SELECT " +
                "PATIENT.[PAT_ID_Number], " +
                "PATIENT.[PAT_First_Name], " +
                "PATIENT.[PAT_Last_Name], " +
                "PATIENT.[PAT_Address], " +
                "PATIENT.[PAT_Suburb], " +
                "PATIENT.[PAT_Post_Code], " +
                "PATIENT.[PAT_State], " +
                "PATIENT.[PAT_Home_Phone], " +
                "PATIENT.[PAT_Mobile_Phone], " +
                "PATIENT.[PAT_Email_Address], " +
                "PATIENT.[PAT_Date_Reffered], " +
                "PATIENT.[PAT_Date_of_Birth], " +
                "PATIENT.[PAT_Gender], " +
                "PATIENT.[PAT_GP_Letter_Sent], " +
                "PATIENT.[PAT_Class_Attendance]," +
                "PATIENT.[PAT_Notes], " +
                "[REFFERING GP].[REFGP_ID_Number], " +
                "[REFFERING GP].[REFGP_First_Name], " +
                "[REFFERING GP].[REFGP_Last_Name], " +
                "[GENERAL PRACTICE].PRAC_Name, " +
                "PRAC_Address, " +
                "PRAC_Suburb, " +
                "PRAC_Post_Code, " +
                "PRAC_State " +
                "FROM [GENERAL PRACTICE] " +
                "INNER JOIN [REFFERING GP] ON [GENERAL PRACTICE].[PRAC_Name] =  [REFFERING GP].[PRAC_Name]" +
                "INNER JOIN PATIENT ON [REFFERING GP].[REFGP_ID_Number] = PATIENT.[REFGP_ID_Number]" +
                "WHERE " +
                columnName + " " + searchOperator + " '" + searchName + "' AND " +
                columnName + " " + searchOperator2 + " '" + searchName2 + "' AND " +
                "PAT_Current = 0 " +
                "ORDER BY PAT_ID_Number;";
            return query;
        }

        //Creates sql query for all patients
        public static string CreateAllQuery(string columnName, string searchName, string searchOperator, bool stringSearch)
        {
            string query = null;

            //search string
            if (stringSearch == true)
            {
                query = "SELECT " +
                    "PATIENT.[PAT_ID_Number], " +
                    "PATIENT.[PAT_First_Name], " +
                    "PATIENT.[PAT_Last_Name], " +
                    "PATIENT.[PAT_Address], " +
                    "PATIENT.[PAT_Suburb], " +
                    "PATIENT.[PAT_Post_Code], " +
                    "PATIENT.[PAT_State], " +
                    "PATIENT.[PAT_Home_Phone], " +
                    "PATIENT.[PAT_Mobile_Phone], " +
                    "PATIENT.[PAT_Email_Address], " +
                    "PATIENT.[PAT_Date_Reffered], " +
                    "PATIENT.[PAT_Date_of_Birth], " +
                    "PATIENT.[PAT_Gender], " +
                    "PATIENT.[PAT_GP_Letter_Sent], " +
                    "PATIENT.[PAT_Class_Attendance]," +
                    "PATIENT.[PAT_Notes], " +
                    "[REFFERING GP].[REFGP_ID_Number], " +
                    "[REFFERING GP].[REFGP_First_Name], " +
                    "[REFFERING GP].[REFGP_Last_Name], " +
                    "[GENERAL PRACTICE].PRAC_Name, " +
                    "PRAC_Address, " +
                    "PRAC_Suburb, " +
                    "PRAC_Post_Code, " +
                    "PRAC_State " +
                    "FROM [GENERAL PRACTICE] " +
                    "INNER JOIN [REFFERING GP] ON [GENERAL PRACTICE].[PRAC_Name] =  [REFFERING GP].[PRAC_Name]" +
                    "INNER JOIN PATIENT ON [REFFERING GP].[REFGP_ID_Number] = PATIENT.[REFGP_ID_Number]" +
                    "WHERE " +
                    columnName + " " + searchOperator + " '" + searchName + "' " +
                    "ORDER BY PAT_ID_Number;";
            }

            //search number
            else if (stringSearch == false)
            {
                query = "SELECT " +
                    "PATIENT.[PAT_ID_Number], " +
                    "PATIENT.[PAT_First_Name], " +
                    "PATIENT.[PAT_Last_Name], " +
                    "PATIENT.[PAT_Address], " +
                    "PATIENT.[PAT_Suburb], " +
                    "PATIENT.[PAT_Post_Code], " +
                    "PATIENT.[PAT_State], " +
                    "PATIENT.[PAT_Home_Phone], " +
                    "PATIENT.[PAT_Mobile_Phone], " +
                    "PATIENT.[PAT_Email_Address], " +
                    "PATIENT.[PAT_Date_Reffered], " +
                    "PATIENT.[PAT_Date_of_Birth], " +
                    "PATIENT.[PAT_Gender], " +
                    "PATIENT.[PAT_GP_Letter_Sent], " +
                    "PATIENT.[PAT_Class_Attendance]," +
                    "PATIENT.[PAT_Notes], " +
                    "[REFFERING GP].[REFGP_ID_Number], " +
                    "[REFFERING GP].[REFGP_First_Name], " +
                    "[REFFERING GP].[REFGP_Last_Name], " +
                    "[GENERAL PRACTICE].PRAC_Name, " +
                    "PRAC_Address, " +
                    "PRAC_Suburb, " +
                    "PRAC_Post_Code, " +
                    "PRAC_State " +
                    "FROM [GENERAL PRACTICE] " +
                    "INNER JOIN [REFFERING GP] ON [GENERAL PRACTICE].[PRAC_Name] =  [REFFERING GP].[PRAC_Name]" +
                    "INNER JOIN PATIENT ON [REFFERING GP].[REFGP_ID_Number] = PATIENT.[REFGP_ID_Number]" +
                    "WHERE " +
                    columnName + " " + searchOperator + " " + searchName + " " +
                    "ORDER BY PAT_ID_Number;";
            }
            return query;
        }
        //Creates sql query for all patients with 2 wehere clauses
        public static string CreateAllQuery(string columnName, string searchName, string searchOperator, string searchName2, string searchOperator2)
        {
            string query = "SELECT " +
                "PATIENT.[PAT_ID_Number], " +
                "PATIENT.[PAT_First_Name], " +
                "PATIENT.[PAT_Last_Name], " +
                "PATIENT.[PAT_Address], " +
                "PATIENT.[PAT_Suburb], " +
                "PATIENT.[PAT_Post_Code], " +
                "PATIENT.[PAT_State], " +
                "PATIENT.[PAT_Home_Phone], " +
                "PATIENT.[PAT_Mobile_Phone], " +
                "PATIENT.[PAT_Email_Address], " +
                "PATIENT.[PAT_Date_Reffered], " +
                "PATIENT.[PAT_Date_of_Birth], " +
                "PATIENT.[PAT_Gender], " +
                "PATIENT.[PAT_GP_Letter_Sent], " +
                "PATIENT.[PAT_Class_Attendance]," +
                "PATIENT.[PAT_Notes], " +
                "[REFFERING GP].[REFGP_ID_Number], " +
                "[REFFERING GP].[REFGP_First_Name], " +
                "[REFFERING GP].[REFGP_Last_Name], " +
                "[GENERAL PRACTICE].PRAC_Name, " +
                "PRAC_Address, " +
                "PRAC_Suburb, " +
                "PRAC_Post_Code, " +
                "PRAC_State " +
                "FROM [GENERAL PRACTICE] " +
                "INNER JOIN [REFFERING GP] ON [GENERAL PRACTICE].[PRAC_Name] =  [REFFERING GP].[PRAC_Name]" +
                "INNER JOIN PATIENT ON [REFFERING GP].[REFGP_ID_Number] = PATIENT.[REFGP_ID_Number]" +
                "WHERE " +
                columnName + " " + searchOperator + " '" + searchName + "' AND " +
                columnName + " " + searchOperator2 + " '" + searchName2 + "' " +
                "ORDER BY PAT_ID_Number;";
            return query;
        }

        //Views all records no queries
        public static void ViewAllRecords(ComboBox dropDown, DataGridView patientDGV)
        {
            string query = null;
            switch (dropDown.SelectedItem.ToString())
            {
                case "All Patients":
                    query = "SELECT " +
                        "PATIENT.[PAT_ID_Number], " +
                        "PATIENT.[PAT_First_Name], " +
                        "PATIENT.[PAT_Last_Name], " +
                        "PATIENT.[PAT_Address], " +
                        "PATIENT.[PAT_Suburb], " +
                        "PATIENT.[PAT_Post_Code], " +
                        "PATIENT.[PAT_State], " +
                        "PATIENT.[PAT_Home_Phone], " +
                        "PATIENT.[PAT_Mobile_Phone], " +
                        "PATIENT.[PAT_Email_Address], " +
                        "PATIENT.[PAT_Date_Reffered], " +
                        "PATIENT.[PAT_Date_of_Birth], " +
                        "PATIENT.[PAT_Gender], " +
                        "PATIENT.[PAT_GP_Letter_Sent], " +
                        "PATIENT.[PAT_Class_Attendance]," +
                        "PATIENT.[PAT_Notes], " +
                        "[REFFERING GP].[REFGP_ID_Number], " +
                        "[REFFERING GP].[REFGP_First_Name], " +
                        "[REFFERING GP].[REFGP_Last_Name], " +
                        "[GENERAL PRACTICE].PRAC_Name, " +
                        "[GENERAL PRACTICE].PRAC_Address, " +
                        "[GENERAL PRACTICE].PRAC_Suburb, " +
                        "[GENERAL PRACTICE].PRAC_Post_Code, " +
                        "[GENERAL PRACTICE].PRAC_State " +
                        "FROM [GENERAL PRACTICE] " +
                        "INNER JOIN [REFFERING GP] ON [GENERAL PRACTICE].[PRAC_Name] =  [REFFERING GP].[PRAC_Name] " +
                        "INNER JOIN PATIENT ON [REFFERING GP].[REFGP_ID_Number] = PATIENT.[REFGP_ID_Number] " + 
                        "ORDER BY PAT_ID_Number;";
                    break;
                case "Current Patients":
                    query = CreateCurrentQuery("PAT_Current", "1", "=", false);
                    break;
                case "Non Current Patients":
                    query = CreateNonCurrentQuery("PAT_Current", "0", "=", false);
                    break;
            }
            Patient_Database.ViewPatients(patientDGV, query);
        }

        //Views Searched Records and Populates DataGridView
        public static void ViewSearchedRecords(DataGridView patientDGV, TextBox searchText, ComboBox viewRecordsDropDown, ComboBox searchDropDown,
            ComboBox searchChoiceDropDown, DateTimePicker searchDate1, DateTimePicker searchDate2)
        {
            //Shows error if nothing is written into the input box
            if (searchText.Visible == true && string.IsNullOrWhiteSpace(searchText.Text) == true)
            {
                MessageBox.Show("Please enter a string to search", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

                //Shows error if a selection isn't selected
            else if (searchChoiceDropDown.Visible == true && searchChoiceDropDown.SelectedIndex == -1)
            {
                MessageBox.Show("Please make a selection from the dropdown box", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string searchBy = searchDropDown.SelectedItem.ToString();    //Holds String of Currently selected search item
            string query = null;                                        //Holds string for the sql query
            bool searchInteger = false;                                 //trigger if user is searching for an integer and not a string

            //searchBy is the variable for the chosen search option
            switch (searchBy)
            {
                case "ID Number":
                    //Filters through the 3 options: All Patients, Current and Non Current Patients
                    //and sets the sql query accordingly
                    searchInteger = true;
                    switch (viewRecordsDropDown.SelectedItem.ToString())
                    {
                        case "All Patients":
                            query = CreateAllQuery("PAT_ID_Number", searchText.Text, "=", false);
                            break;
                        case "Current Patients":
                            query = CreateCurrentQuery("PAT_ID_Number", searchText.Text, "=", false);
                            break;
                        case "Non Current Patients":
                            query = CreateNonCurrentQuery("PAT_ID_Number", searchText.Text, "=", false);
                            break;
                    }
                    break;
                case "First Name":
                    switch (viewRecordsDropDown.SelectedItem.ToString())
                    {
                        case "All Patients":
                            query = CreateAllQuery("PAT_First_Name", "%" + searchText.Text + "%", "LIKE", true);
                            break;
                        case "Current Patients":
                            query = CreateCurrentQuery("PAT_First_Name", "%" + searchText.Text + "%", "LIKE", true);
                            break;
                        case "Non Current Patients":
                            query = CreateNonCurrentQuery("PAT_First_Name", "%" + searchText.Text + "%", "LIKE", true);
                            break;
                    }
                    break;
                case "Last Name":
                    switch (viewRecordsDropDown.SelectedItem.ToString())
                    {
                        case "All Patients":
                            query = CreateAllQuery("PAT_Last_Name", "%" + searchText.Text + "%", "LIKE", true);
                            break;
                        case "Current Patients":
                            query = CreateCurrentQuery("PAT_Last_Name", "%" + searchText.Text + "%", "LIKE", true);
                            break;
                        case "Non Current Patients":
                            query = CreateNonCurrentQuery("PAT_Last_Name", "%" + searchText.Text + "%", "LIKE", true);
                            break;
                    }
                    break;
                case "General Practice":
                    switch (viewRecordsDropDown.SelectedItem.ToString())
                    {
                        case "All Patients":
                            query = CreateAllQuery("[GENERAL PRACTICE].PRAC_Name", "%" + searchText.Text + "%", "LIKE", true);
                            break;
                        case "Current Patients":
                            query = CreateCurrentQuery("[GENERAL PRACTICE].PRAC_Name", "%" + searchText.Text + "%", "LIKE", true);
                            break;
                        case "Non Current Patients":
                            query = CreateNonCurrentQuery("[GENERAL PRACTICE].PRAC_Name", "%" + searchText.Text + "%", "LIKE", true);
                            break;
                    }
                    break;
                case "Reffering GP ID":
                    searchInteger = true;
                    switch (viewRecordsDropDown.SelectedItem.ToString())
                    {
                        case "All Patients":
                            query = CreateAllQuery("[PATIENT].REFGP_ID_Number",searchText.Text, "=", false);
                            break;
                        case "Current Patients":
                            query = CreateCurrentQuery("[PATIENT].REFGP_ID_Number", searchText.Text, "=", false);
                            break;
                        case "Non Current Patients":
                            query = CreateNonCurrentQuery("[PATIENT].REFGP_ID_Number", searchText.Text, "=", false);
                            break;
                    }
                    break;
                case "Address":
                    switch (viewRecordsDropDown.SelectedItem.ToString())
                    {
                        case "All Patients":
                            query = CreateAllQuery("PAT_Address", "%" + searchText.Text + "%", "LIKE", true);
                            break;
                        case "Current Patients":
                            query = CreateCurrentQuery("PAT_Address", "%" + searchText.Text + "%", "LIKE", true);
                            break;
                        case "Non Current Patients":
                            query = CreateNonCurrentQuery("PAT_Address", "%" + searchText.Text + "%", "LIKE", true);
                            break;
                    }
                    break;
                case "Suburb":
                    switch (viewRecordsDropDown.SelectedItem.ToString())
                    {
                        case "All Patients":
                            query = CreateAllQuery("PAT_Suburb", "%" + searchText.Text + "%", "LIKE", true);
                            break;
                        case "Current Patients":
                            query = CreateCurrentQuery("PAT_Suburb", "%" + searchText.Text + "%", "LIKE", true);
                            break;
                        case "Non Current Patients":
                            query = CreateNonCurrentQuery("PAT_Suburb", "%" + searchText.Text + "%", "LIKE", true);
                            break;
                    }
                    break;
                case "Post Code":
                    switch (viewRecordsDropDown.SelectedItem.ToString())
                    {
                        case "All Patients":
                            query = CreateAllQuery("PAT_Post_Code", "%" + searchText.Text + "%", "LIKE", true);
                            break;
                        case "Current Patients":
                            query = CreateCurrentQuery("PAT_Post_Code", "%" + searchText.Text + "%", "LIKE", true);
                            break;
                        case "Non Current Patients":
                            query = CreateNonCurrentQuery("PAT_Post_Code", "%" + searchText.Text + "%", "LIKE", true);
                            break;
                    }
                    break;
                case "State":
                    switch (viewRecordsDropDown.SelectedItem.ToString())
                    {
                        case "All Patients":
                            query = CreateAllQuery("PAT_State", "%" + searchText.Text + "%", "LIKE", true);
                            break;
                        case "Current Patients":
                            query = CreateCurrentQuery("PAT_State", "%" + searchText.Text + "%", "LIKE", true);
                            break;
                        case "Non Current Patients":
                            query = CreateNonCurrentQuery("PAT_State", "%" + searchText.Text + "%", "LIKE", true);
                            break;
                    }
                    break;
                case "Home Phone":
                    switch (viewRecordsDropDown.SelectedItem.ToString())
                    {
                        case "All Patients":
                            query = CreateAllQuery("PAT_Home_Phone", "%" + searchText.Text + "%", "LIKE", true);
                            break;
                        case "Current Patients":
                            query = CreateCurrentQuery("PAT_Home_Phone", "%" + searchText.Text + "%", "LIKE", true);
                            break;
                        case "Non Current Patients":
                            query = CreateNonCurrentQuery("PAT_Home_Phone", "%" + searchText.Text + "%", "LIKE", true);
                            break;
                    }
                    break;
                case "Mobile Phone":
                    switch (viewRecordsDropDown.SelectedItem.ToString())
                    {
                        case "All Patients":
                            query = CreateAllQuery("PAT_Mobile_Number", "%" + searchText.Text + "%", "LIKE", true);
                            break;
                        case "Current Patients":
                            query = CreateCurrentQuery("PAT_Mobile_Number", "%" + searchText.Text + "%", "LIKE", true);
                            break;
                        case "Non Current Patients":
                            query = CreateNonCurrentQuery("PAT_Mobile_Number", "%" + searchText.Text + "%", "LIKE", true);
                            break;
                    }
                    break;
                case "Email":
                    switch (viewRecordsDropDown.SelectedItem.ToString())
                    {
                        case "All Patients":
                            query = CreateAllQuery("PAT_Email", "%" + searchText.Text + "%", "LIKE", true);
                            break;
                        case "Current Patients":
                            query = CreateCurrentQuery("PAT_Email", "%" + searchText.Text + "%", "LIKE", true);
                            break;
                        case "Non Current Patients":
                            query = CreateNonCurrentQuery("PAT_Email", "%" + searchText.Text + "%", "LIKE", true);
                            break;
                    }
                    break;
                case "Date Reffered":
                    switch (viewRecordsDropDown.SelectedItem.ToString())
                    {
                        case "All Patients":
                            query = CreateAllQuery("PAT_Date_Reffered", Global.getDateString(searchDate1), ">=",
                                Global.getDateString(searchDate2), "<=");
                            break;
                        case "Current Patients":
                            query = CreateCurrentQuery("PAT_Date_Reffered", Global.getDateString(searchDate1), ">=",
                                Global.getDateString(searchDate2), "<=");
                            break;
                        case "Non Current Patients":
                            query = CreateNonCurrentQuery("PAT_Date_Reffered", Global.getDateString(searchDate1), ">=",
                                Global.getDateString(searchDate2), "<=");
                            break;
                    }
                    break;
                case "Date of Birth":
                    switch (viewRecordsDropDown.SelectedItem.ToString())
                    {
                        case "All Patients":
                            query = CreateAllQuery("PAT_Date_of_Birth", Global.getDateString(searchDate1), "=", true);
                            break;
                        case "Current Patients":
                            query = CreateCurrentQuery("PAT_Date_of_Birth", Global.getDateString(searchDate1), "=", true);
                            break;
                        case "Non Current Patients":
                            query = CreateNonCurrentQuery("PAT_Date_of_Birth", Global.getDateString(searchDate1), "=", true);
                            break;
                    }
                    break;
                case "Gender":
                    switch (viewRecordsDropDown.SelectedItem.ToString())
                    {
                        case "All Patients":
                            query = CreateAllQuery("PAT_Gender", searchChoiceDropDown.SelectedItem.ToString(), "=", true);
                            break;
                        case "Current Patients":
                            query = CreateCurrentQuery("PAT_Gender", searchChoiceDropDown.SelectedItem.ToString(), "=", true);
                            break;
                        case "Non Current Patients":
                            query = CreateNonCurrentQuery("PAT_Gender", searchChoiceDropDown.SelectedItem.ToString(), "=", true);
                            break;
                    }
                    break;
                case "GP Letter Sent":

                    //Conmverting selection text to integer of true/false.  0 = false, 1 = true
                    int gpLetterSent = 0;
                    if (searchChoiceDropDown.SelectedItem.ToString() == "Sent")
                    {
                        gpLetterSent = 1;
                    }
                    else if (searchChoiceDropDown.SelectedItem.ToString() == "Not Sent")
                    {
                        gpLetterSent = 0;
                    }
                    switch (viewRecordsDropDown.SelectedItem.ToString())
                    {
                        case "All Patients":
                            query = CreateAllQuery("PAT_GP_Letter_Sent", gpLetterSent.ToString(), "=", false);
                            break;
                        case "Current Patients":
                            query = CreateCurrentQuery("PAT_GP_Letter_Sent", gpLetterSent.ToString(), "=", false);
                            break;
                        case "Non Current Patients":
                            query = CreateNonCurrentQuery("PAT_GP_Letter_Sent", gpLetterSent.ToString(), "=", false);
                            break;
                    }
                    break;
                case "Class Attendance":
                    searchInteger = true;
                    switch (viewRecordsDropDown.SelectedItem.ToString())
                    {
                        case "All Patients":
                            query = CreateAllQuery("PAT_Class_Attendance", searchText.Text, "=", false);
                            break;
                        case "Current Patients":
                            query = CreateCurrentQuery("PAT_Class_Attendance", searchText.Text, "=", false);
                            break;
                        case "Non Current Patients":
                            query = CreateNonCurrentQuery("PAT_Class_Attendance", searchText.Text, "=", false);
                            break;
                    }
                    break;
            }
            //Sets dgv to datasource of sql query
            try
            {
                ViewPatients(patientDGV, query);
            }
            catch (SQLiteException ex)
            {
                //Error if in numbers search but didnt enter numbers
                if (searchInteger == true)
                {
                    MessageBox.Show("Only numbers can be entered", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        //Hides or shows search options depending on choice.
        //Used in View_Records
        public static void Show_HideSearchOptions(ComboBox searchSelectionDropDown, ComboBox searchChoiceDropDown,
            TextBox searchText, DateTimePicker searchDate1, DateTimePicker searchDate2, Label searchLabel1, Label searchLabel2)
        {
            searchLabel1.Visible = true;
            searchLabel2.Visible = false;
            searchDate1.Visible = false;
            searchDate2.Visible = false;
            searchText.ReadOnly = false;
            searchText.Visible = true;
            searchText.Text = null;
            searchChoiceDropDown.Visible = false;
            string item = searchSelectionDropDown.SelectedItem.ToString();
            string[] patSearchChoiceItems;          //Used twice in 2 cases.  Placed here to define scope
            switch (item)
            {
                case "ID Number":
                    searchLabel1.Text = "ID Number:";
                    break;
                case "First Name":
                    searchLabel1.Text = "First Name:";
                    break;
                case "Last Name":
                    searchLabel1.Text = "Last Name:";
                    break;
                case "General Practice":
                    searchLabel1.Text = "Practice:";
                    break;
                case "Reffering GP ID":
                    searchLabel1.Text = "Reffering GP ID:";
                    break;
                case "Address":
                    searchLabel1.Text = "Address:";
                    break;
                case "Suburb":
                    searchLabel1.Text = "Suburb:";
                    break;
                case "Post Code":
                    searchLabel1.Text = "Post Code:";
                    break;
                case "State":
                    searchLabel1.Text = "State:";
                    break;
                case "Home Phone":
                    searchLabel1.Text = "Home Phone:";
                    break;
                case "Mobile Phone":
                    searchLabel1.Text = "Mobile Phone:";
                    break;
                case "Email":
                    searchLabel1.Text = "Email:";
                    break;
                case "Date Reffered":
                    searchLabel1.Text = "From Date:";
                    searchLabel2.Visible = true;
                    searchLabel2.Text = "To Date;";
                    searchText.Visible = false;
                    searchDate1.Visible = true;
                    searchDate2.Visible = true;
                    break;
                case "Date of Birth":
                    searchLabel1.Text = "Date of Birth:";
                    searchText.Visible = false;
                    searchDate1.Visible = true;
                    break;
                case "Gender":
                    searchLabel1.Text = "Gender:";
                    searchText.Visible = false;
                    searchChoiceDropDown.Items.Clear();
                    searchChoiceDropDown.Visible = true;
                    patSearchChoiceItems = new string[2] {"Male", "Female"};
                    searchChoiceDropDown.Items.AddRange(patSearchChoiceItems);
                    break;
                case "GP Letter Sent":
                    searchLabel1.Text = "GP Letter Sent:";
                    searchText.Visible = false;
                    searchChoiceDropDown.Items.Clear();
                    searchChoiceDropDown.Visible = true;
                    patSearchChoiceItems = new string[2] {"Sent", "Not Sent"};
                    searchChoiceDropDown.Items.AddRange(patSearchChoiceItems);
                    break;
                case "Class Attendance":
                    searchLabel1.Text = "Attendance:";
                    break;
            }
        }

        //Saves class attendance of patient
        public static void SaveAttendance(DataGridView patientDGV)
        {
            DataTable patdt = (DataTable)patientDGV.DataSource;         //Gets datasource and sets to datatable
            //Iterates through each row and saves each attendance
            foreach (DataRow row in patdt.Rows)            
            {
                string patID = row[0].ToString();               //Cell 0 is patient id
                string attendance = row[5].ToString();          //cell 5 is attendance
                string query = "UPDATE [PATIENT] SET PAT_Class_Attendance = @attendance WHERE PAT_ID_Number = " +
                    patID + ";";                                //SQL Query
                using (Global.connection = new SQLiteConnection(Global.connectionString))
                using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
                {
                    cmd.Parameters.Add("@attendance", attendance);
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
            }
        }
    }
}
