using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using Finisar.SQLite;

namespace Patient_Database_and_Tracker
{
    class Reffering_GP_Database
    {
        public static DataTable PopulateRefferingGPDropdown(ComboBox dropDownList)
        {
            DataTable refgpdt = new DataTable();
            //Will be used as the datatable columns
            int id = 0;
            string displayText = null;

            //Resets the datasource if there is an existing datasource
            dropDownList.DataSource = null;

            //Holds the query String
            string query = null;

            //Depending on the sortDropdown variables the query will be different
            if (Global.sortDropdown == Global.ID)
            {
                query = "SELECT REFGP_ID_Number, REFGP_First_Name, REFGP_Last_Name FROM [REFFERING GP] ORDER BY REFGP_ID_Number DESC;";
            }
            else if (Global.sortDropdown == Global.NAME)
            {
                query = "SELECT REFGP_ID_Number, REFGP_First_Name, REFGP_Last_Name FROM [REFFERING GP] ORDER BY REFGP_First_Name, REFGP_Last_Name;";
            }

            //Connects to database and runs query
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
            {
                try
                {
                    Global.connection.Open();
                    
                    //New datatable.  Will be used as Reffering GPs datasource
                    refgpdt.Columns.Add("ID");
                    refgpdt.Columns.Add("Display Text");
                    SQLiteDataReader reader = cmd.ExecuteReader();

                    //refgpdt datatable will be populated
                    while (reader.Read())
                    {
                        id = reader.GetInt32(0);
                        if (Global.sortDropdown == Global.ID)
                        {
                            displayText = reader.GetInt32(0).ToString() + " *** Name: " + reader.GetString(1) +
                                " " + reader.GetString(2);
                            refgpdt.Rows.Add(id, displayText);
                        }
                        else if (Global.sortDropdown == Global.NAME)
                        {
                            displayText = reader.GetString(1) + " " + reader.GetString(2) + " *** ID: " + reader.GetInt32(0).ToString();
                            refgpdt.Rows.Add(id, displayText);
                        }
                    }
                    dropDownList.DataSource = refgpdt;             //Sets Reffering_GP_Dropdown datasource
                    dropDownList.ValueMember = "ID";
                    dropDownList.DisplayMember = "Display Text";
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return refgpdt;
        }

        public static void RefferingGPSelectedAddEdit(ComboBox refGPDropDownList, TextBox id, TextBox firstName, TextBox lastName, TextBox directNumber,
            TextBox email, TextBox notes, ComboBox practiceDropDownList)
        {
            DataTable pracdt;                   //Will be used to search through the practice names
            string practiceName = null;        //Will Hold the name of the practice
            int pracSelectedIndex = 0;         //Will Hold the selected index of the Dropdown2

            string query = "SELECT * FROM [REFFERING GP] WHERE REFGP_ID_Number =" + refGPDropDownList.SelectedValue + ";";    //SQL Query

            //Connect to database and run query
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {
                try
                {
                    Global.connection.Open();
                    SQLiteDataReader reader = cmd.ExecuteReader();

                    //Populating the fields with the found Reffering GP details
                    while (reader.Read())
                    {
                        id.Text = reader.GetInt32(0).ToString();
                        firstName.Text = reader.GetString(1);
                        lastName.Text = reader.GetString(2);
                        directNumber.Text = reader.GetString(3);
                        email.Text = reader.GetString(4);
                        practiceName = reader.GetString(5);                     //Sets a local variable to hold the Practice Name
                        notes.Text = reader.GetString(6);
                    }
                    Global.editingExistingRefferingGP = true;     //Creates an edit session

                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            pracdt = Practice_Database.populatePracticeDropDown(practiceDropDownList);
            int counter = 0;                //Counter will be used to find pracSelectedIndex

            //Loop through datatable to find the row with the selected Practice Name
            foreach (DataRow row in pracdt.Rows)
            {
                if (row[0].ToString() == practiceName)
                {
                    pracSelectedIndex = counter;
                    break;
                }
                counter++;
            }
            practiceDropDownList.SelectedIndex = pracSelectedIndex;       //Selects the correct Reffering GP from Reffering_GP_Dropdown
        }

        public static void RefferingGPSelectedDelete(ComboBox refGPDropDownList, TextBox id, TextBox firstName, TextBox lastName, TextBox directNumber,
            TextBox email, TextBox practiceName, TextBox notes)
        {
            string query = "SELECT * FROM [REFFERING GP] WHERE REFGP_ID_Number =" + refGPDropDownList.SelectedValue + ";";
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
                        directNumber.Text = reader.GetString(3);
                        email.Text = reader.GetString(4);
                        practiceName.Text = reader.GetString(5); 
                        notes.Text = reader.GetString(6);
                    }
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public static bool SaveRefferingGP(ComboBox refGPDropDownList, TextBox id, TextBox firstName, TextBox lastName, TextBox directNumber,
            TextBox email, TextBox notes, ComboBox practiceDropDownList)
        {
            //Can only save data if fields aren't read only
            //if user clicks save changes while fields are read only an error message will show
            if (firstName.ReadOnly == true)
            {
                MessageBox.Show("Click Add New Reffering GP or Edit Existing Reffering GP to save changes", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //First and last name cannot be null.  Error message if they are not entered
            if (String.IsNullOrWhiteSpace(firstName.Text) || String.IsNullOrWhiteSpace(lastName.Text))
            {
                MessageBox.Show("Reffering GP must have a First and Last Name", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (practiceDropDownList.SelectedIndex == -1)
            {
                MessageBox.Show("Reffering GP must have a Practice", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            string query = "";              //Will be SQL Query
            string query2 = "";             //If a new Reffering GP is added a second query is used

            //query if adding a new Reffering GP
            if (Global.editingExistingRefferingGP == false)
            {
                query = "INSERT INTO [REFFERING GP] (REFGP_First_Name, REFGP_Last_Name, REFGP_Direct_Number, REFGP_Email_Address, PRAC_Name, REFGP_Notes) VALUES" +
                        "(@firstName, @lastName, @directNumber, @email, @practice, @notes);";
                query2 = "SELECT last_insert_rowid() FROM [REFFERING GP];";
            }

            //query if editing an existing Reffering GP
            else
            {
                query = "UPDATE [REFFERING GP] SET REFGP_First_Name = @firstName, REFGP_Last_Name = @lastName, REFGP_Direct_Number = @directNumber," +
                    "REFGP_Email_Address = @email, PRAC_Name = @practice, REFGP_Notes = @notes WHERE REFGP_ID_Number =" + refGPDropDownList.SelectedValue + ";";
            }

            //Connect to database and run query
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {

                //Setting parameters for input data
                cmd.Parameters.Add("@firstName", firstName.Text);
                cmd.Parameters.Add("@lastName", lastName.Text);
                cmd.Parameters.Add("@practice", practiceDropDownList.SelectedValue);

                if (String.IsNullOrWhiteSpace(directNumber.Text))
                {
                    cmd.Parameters.Add("@directNumber", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.Add("@directNumber", directNumber.Text);
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
                    //Gets new ID if new Reffering GP
                    if (Global.editingExistingRefferingGP == false)
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
                    refGPDropDownList.DataSource = null;

                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return true;
        }

        public static void DeleteRefferingGP(ComboBox refGPDropDownList, TextBox id)
        {
            //Shows error message if no Reffering GP selected
            if (id.Text == "")
            {
                MessageBox.Show("Please choose a Reffering GP from the dropdown list", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = "SELECT PAT_ID_Number, PAT_First_Name, PAT_Last_Name FROM [PATIENT] WHERE REFGP_ID_Number =" + refGPDropDownList.SelectedValue + ";";
            List<string> patientList = new List<string>();
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {
                try
                {
                    Global.connection.Open();
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    //The dropdownbox will show the ID and the patients name
                    while (reader.Read())
                    {
                        string patID = "Patient ID: " + reader.GetInt32(0).ToString() + " **** ";
                        string patName = reader.GetString(1) + " " + reader.GetString(2);
                        patientList.Add(patID + patName);
                    }
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                Global.connection.Close();
                if (patientList.Count > 0)
                {
                    string patients = null;
                    foreach (string patient in patientList)
                    {
                        patients = patients + patient + "\n";
                    }
                    MessageBox.Show("Unable to Delete Reffering GP.  The Following Patients Have Been Assigned to This GP:\n" + patients,
                        "Cannot Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            DialogResult areYouSure = MessageBox.Show("Are you sure you want to delete this Reffering GP?\nDeleting is Permanent",
                "Are You Sure", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (areYouSure == DialogResult.No)
            {
                return;
            }
            else if (areYouSure == DialogResult.Yes)
            {
                query = "DELETE FROM [REFFERING GP] WHERE REFGP_ID_Number =" + refGPDropDownList.SelectedValue + ";";

                using (Global.connection = new SQLiteConnection(Global.connectionString))
                using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
                {
                    try
                    {
                        Global.connection.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Reffering GP Deleted", "Delete Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        refGPDropDownList.DataSource = null;
                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }

        public static void ViewAllRecords(DataGridView refgpDataGridView)
        {
            string query = "SELECT " +
                "[REFFERING GP].[REFGP_ID_Number], " +
                "[REFFERING GP].[REFGP_First_Name], " +
                "[REFFERING GP].[REFGP_Last_Name], " +
                "[REFFERING GP].[REFGP_Direct_Number], " +
                "[REFFERING GP].[REFGP_Email_Address], " +
                "[REFFERING GP].[REFGP_Notes], " +
                "[GENERAL PRACTICE].PRAC_Name, " +
                "PRAC_Address, " +
                "PRAC_Suburb, " +
                "PRAC_Post_Code, " +
                "PRAC_State " +
                "FROM [GENERAL PRACTICE] " +
                "INNER JOIN [REFFERING GP] ON [GENERAL PRACTICE].[PRAC_Name] =  [REFFERING GP].[PRAC_Name]" +
                "ORDER BY REFGP_ID_Number;";
            ViewRefferingGPs(refgpDataGridView, query);
        }

        //Will be used with other methods to view all and view searched data
        private static void ViewRefferingGPs(DataGridView refgpDataGridView, string query)
        {
            DataTable refgpdt = new DataTable();
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            {
                    Global.connection.Open();
                    SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(query, Global.connection);
                    dataAdapter.Fill(refgpdt);
                    createHeaders(refgpdt);
                    refgpDataGridView.DataSource = refgpdt;
                    Global.connection.Close();
                //dgv.Select();
                //dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                //foreach (DataGridViewRow row in dgv.Rows)
                //{
                //    if (row.Cells[0].Value.ToString().Equals(Global.consID))
                //    {
                //        dgv.CurrentCell = row.Cells[0];
                //        break;
                //    }
                //    else
                //        dgv.ClearSelection();
                //}
            }
        }
        //Used in ViewPatients Method.  Sets the headers of the datatable
        private static void createHeaders(DataTable refgpdt)
        {
            refgpdt.Columns[0].ColumnName = "GP ID";
            refgpdt.Columns[1].ColumnName = "GP First Name";
            refgpdt.Columns[2].ColumnName = "GP Last Name";
            refgpdt.Columns[3].ColumnName = "GP Direct Number";
            refgpdt.Columns[4].ColumnName = "GP Email";
            refgpdt.Columns[5].ColumnName = "GP Notes";
            refgpdt.Columns[6].ColumnName = "Practice Name";
            refgpdt.Columns[7].ColumnName = "Practice Address";
            refgpdt.Columns[8].ColumnName = "Practice Suburb";
            refgpdt.Columns[9].ColumnName = "Practice Post Code";
            refgpdt.Columns[10].ColumnName = "Practice State";
        }

        public static void ViewSearchedRecords(DataGridView refgpDGV, TextBox searchText, ComboBox searchDropDown)
        {
            //Shows error if nothing is written into the input box
            if (string.IsNullOrWhiteSpace(searchText.Text) == true)
            {
                MessageBox.Show("Please Write a String to Search", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string searchBy = searchDropDown.SelectedItem.ToString();    //Holds String of Currently selected search item
            string query = null;                                    //Holds string for the sql query

            bool searchInteger = false;                                 //trigger if user is searching for an integer and not a string

            //searchBy is the variable for the chosen search option
            switch (searchBy)
            {
                case "ID Number":
                    searchInteger = true;
                    query = CreateQuery("REFGP_ID_Number", searchText.Text, "=", false);
                    break;

                case "First Name":
                    query = CreateQuery("REFGP_First_Name", "%" + searchText.Text + "%", "LIKE", true);
                    break;

                case "Last Name":
                    query = CreateQuery("REFGP_Last_Name", "%" + searchText.Text + "%", "LIKE", true);
                    break;

                case "Practice Name":
                    query = CreateQuery("[Reffering GP].PRAC_Name", "%" + searchText.Text + "%", "LIKE", true);
                    break;

                case "Direct Number":
                    query = CreateQuery("REFGP_Direct_Number", "%" + searchText.Text + "%", "LIKE", true);
                    break;

                case "Email":
                    query = CreateQuery("REFGP_Email_Address", "%" + searchText.Text + "%", "LIKE", true);
                    break;
            }
            try
            {
                ViewRefferingGPs(refgpDGV, query);
            }
            catch (SQLiteException ex)
            {
                if (searchInteger == true)
                {
                    MessageBox.Show("Only numbers can be entered", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public static string CreateQuery(string columnName, string searchName, string searchOperator, bool stringSearch)
        {
            string query = null;

            if (stringSearch == true)
            {
                query = "SELECT " +
                "[REFFERING GP].[REFGP_ID_Number], " +
                "[REFFERING GP].[REFGP_First_Name], " +
                "[REFFERING GP].[REFGP_Last_Name], " +
                "[REFFERING GP].[REFGP_Direct_Number], " +
                "[REFFERING GP].[REFGP_Email_Address], " +
                "[REFFERING GP].[REFGP_Notes], " +
                "[GENERAL PRACTICE].PRAC_Name, " +
                "PRAC_Address, " +
                "PRAC_Suburb, " +
                "PRAC_Post_Code, " +
                "PRAC_State " +
                "FROM [GENERAL PRACTICE] " +
                "INNER JOIN [REFFERING GP] ON [GENERAL PRACTICE].[PRAC_Name] =  [REFFERING GP].[PRAC_Name]" +
                "WHERE " +
                columnName + " " + searchOperator + " '" + searchName + "' " +
                "ORDER BY REFGP_ID_Number;";
            }
            else if (stringSearch == false)
            {
                query = "SELECT " +
                "[REFFERING GP].[REFGP_ID_Number], " +
                "[REFFERING GP].[REFGP_First_Name], " +
                "[REFFERING GP].[REFGP_Last_Name], " +
                "[REFFERING GP].[REFGP_Direct_Number], " +
                "[REFFERING GP].[REFGP_Email_Address], " +
                "[REFFERING GP].[REFGP_Notes], " +
                "[GENERAL PRACTICE].PRAC_Name, " +
                "PRAC_Address, " +
                "PRAC_Suburb, " +
                "PRAC_Post_Code, " +
                "PRAC_State " +
                "FROM [GENERAL PRACTICE] " +
                "INNER JOIN [REFFERING GP] ON [GENERAL PRACTICE].[PRAC_Name] =  [REFFERING GP].[PRAC_Name]" +
                "WHERE " +
                columnName + " " + searchOperator + " " + searchName + " " +
                "ORDER BY REFGP_ID_Number;";
            }
            return query;
        }

        public static void Show_HideSearchOptions(ComboBox searchSelectionDropDown, TextBox searchText, Label searchLabel1)
        {
            searchText.ReadOnly = false;
            searchText.Text = null;
            string item = searchSelectionDropDown.SelectedItem.ToString();
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
                case "Practice Name":
                    searchLabel1.Text = "Practice Name:";
                    break;
                case "Direct Number":
                    searchLabel1.Text = "Direct Number:";
                    break;
                case "Email":
                    searchLabel1.Text = "Email:";
                    break;
            }
        }
    }
}
