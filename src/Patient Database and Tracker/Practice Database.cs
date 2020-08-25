using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using Finisar.SQLite;

namespace Patient_Database_and_Tracker
{
    class Practice_Database
    {
        //"CREATE TABLE [GENERAL PRACTICE]" +
        //"(PRAC_Name TEXT NOT NULL PRIMARY KEY UNIQUE," +
        //"PRAC_Address TEXT," +
        //"PRAC_Suburb TEXT," +
        //"PRAC_Post_Code TEXT," +
        //"PRAC_State TEXT," +
        //"PRAC_Phone TEXT," +
        //"PRAC_Fax TEXT," +
        //"PRAC_Notes TEXT);";
        public static DataTable populatePracticeDropDown(ComboBox practiceDropDownList)
        {
            //New datatable.  Will be used as Dropdown2 datasource
            DataTable pracdt = new DataTable();
            //Will be used as the datatable columns
            string displayText = null;

            //Resets the datasource if there is an existing datasource
            practiceDropDownList.DataSource = null;

            string query = "SELECT PRAC_Name FROM [GENERAL PRACTICE];";

            //Connects to database and runs query
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
            {
                try
                {
                    Global.connection.Open();
                    pracdt.Columns.Add("Display Text");
                    SQLiteDataReader reader = cmd.ExecuteReader();

                    //pracdt datatable will be populated
                    while (reader.Read())
                    {
                        displayText = reader.GetString(0);
                        pracdt.Rows.Add(displayText);
                    }
                    practiceDropDownList.DataSource = pracdt;             //Sets Reffering_GP_Dropdown datasource
                    practiceDropDownList.ValueMember = "Display Text";
                    practiceDropDownList.DisplayMember = "Display Text";
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return pracdt;
        }

        public static void PracticeSelected(ComboBox practiceDropDownList, TextBox practiceName, TextBox address, TextBox suburb,
            TextBox postCode, TextBox state, TextBox phone, TextBox fax, TextBox notes)
        {
            string query = "SELECT * FROM [GENERAL PRACTICE] WHERE PRAC_Name ='" + practiceDropDownList.SelectedValue + "';";    //SQL Query

            //Connect to database and run query
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {
                try
                {
                    Global.connection.Open();
                    SQLiteDataReader reader = cmd.ExecuteReader();

                    //Populating the fields with the found practice details
                    while (reader.Read())
                    {
                        practiceName.Text = reader.GetString(0);
                        address.Text = reader.GetString(1);
                        suburb.Text = reader.GetString(2);
                        postCode.Text = reader.GetString(3);
                        state.Text = reader.GetString(4);
                        phone.Text = reader.GetString(5);
                        fax.Text = reader.GetString(6);
                        notes.Text = reader.GetString(7);
                    }
                    Global.editingExistingPractice = true;     //Creates an edit session
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public static bool SavePractice(ComboBox practiceDropDownList, TextBox practiceName, TextBox address, TextBox suburb,
            TextBox postCode, TextBox state, TextBox phone, TextBox fax, TextBox notes)
        {
            //Can only save data if fields aren't read only
            //if user clicks save changes while fields are read only an error message will show
            if (address.ReadOnly == true)
            {
                MessageBox.Show("Click Add New Practice or Edit Existing Practice to save changes", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //First and last name cannot be null.  Error message if they are not entered
            if (String.IsNullOrWhiteSpace(practiceName.Text))
            {
                MessageBox.Show("Practice must have a Name", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            string query = "";              //Will be SQL Query

            string query2 = "";

            //query if adding a new patient
            if (Global.editingExistingPractice == false)
            {
                query = "INSERT INTO [GENERAL PRACTICE](PRAC_Name, PRAC_Address, PRAC_Suburb, PRAC_Post_Code, PRAC_State, PRAC_Phone, PRAC_fax, PRAC_Notes) VALUES" +
                        "(@name, @address, @suburb, @postCode, @state, @phone, @fax, @notes);";
            }

            //query if editing an existing patient
            else
            {
                query = "UPDATE [GENERAL PRACTICE] SET PRAC_Name = @name, PRAC_Address = @address, PRAC_Suburb = @suburb," +
                    "PRAC_Post_Code = @postcode, PRAC_State = @state, PRAC_Phone = @phone, PRAC_Fax = @fax," +
                    "PRAC_Notes = @notes WHERE PRAC_Name ='" + practiceDropDownList.SelectedValue + "';";
                //Needs to update all reffering GPs linked to that practice
                query2 = "UPDATE [REFFERING GP] SET PRAC_Name = @name WHERE PRAC_Name ='" + practiceDropDownList.SelectedValue + "';";

            }

            //Connect to database and run query
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {
                //The following statements change any not null field:
                //Address, suburb, postcode, state, homephone, mobilePhone, email
                //to a string 'Not Provided' if they are empty

                //Setting parameters for input data
                cmd.Parameters.Add("@name", practiceName.Text);

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
                if (String.IsNullOrWhiteSpace(phone.Text))
                {
                    cmd.Parameters.Add("@phone", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.Add("@phone", phone.Text);
                }
                if (String.IsNullOrWhiteSpace(fax.Text))
                {
                    cmd.Parameters.Add("@fax", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.Add("@fax", fax.Text);
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
                    cmd.ExecuteNonQuery();
                    if (Global.editingExistingPractice == true)
                    {
                        cmd.CommandText = query2;
                        cmd.ExecuteNonQuery();
                    }

                    //Clear selected IDs and the selection in the combo box
                    practiceDropDownList.DataSource = null;
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return true;
        }

        public static void DeletePractice(ComboBox practiceDropDownList, TextBox practiceName)
        {
            //Shows error message if no patient selected
            if (practiceName.Text == "")
            {
                MessageBox.Show("Please choose a Practice from the dropdown list", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = "SELECT REFGP_ID_Number, REFGP_First_Name, REFGP_Last_Name FROM [REFFERING GP] WHERE PRAC_NAME ='" + practiceDropDownList.SelectedValue + "';";
            List<string> refgpList = new List<string>();
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
                        string GPid = "GP ID: " + reader.GetInt32(0).ToString() + " **** ";
                        string GPName = reader.GetString(1) + " " + reader.GetString(2);
                        refgpList.Add(GPid + GPName);
                    }
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                Global.connection.Close();
                if (refgpList.Count > 0)
                {
                    string refgps = null;
                    foreach (string refgp in refgpList)
                    {
                        refgps = refgps + refgp + "\n";
                    }
                    MessageBox.Show("Unable to Delete Practice.  The Following Reffering GPs Have Been Assigned to This Practice:\n" + refgps,
                        "Cannot Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            DialogResult areYouSure = MessageBox.Show("Are you sure you want to delete this Practice?\nDeleting is Permanent",
                "Are You Sure", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (areYouSure == DialogResult.No)
            {
                return;
            }
            else if (areYouSure == DialogResult.Yes)
            {
                query = "DELETE FROM [GENERAL PRACTICE] WHERE PRAC_Name ='" + practiceDropDownList.SelectedValue + "';";

                using (Global.connection = new SQLiteConnection(Global.connectionString))
                using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
                {
                    try
                    {
                        Global.connection.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Practice Deleted", "Delete Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        practiceDropDownList.DataSource = null;
                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }

        public static void ViewAllRecords(DataGridView pracDataGridView)
        {
            string query = "SELECT * FROM [GENERAL PRACTICE] " +
                "ORDER BY PRAC_Name;";
            ViewPractices(pracDataGridView, query);
        }

        //Will be used with other methods to view all and view searched data
        private static void ViewPractices(DataGridView pracDataGridView, string query)
        {
            DataTable pracdt = new DataTable();
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            {
                try
                {
                    Global.connection.Open();
                    SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(query, Global.connection);
                    dataAdapter.Fill(pracdt);
                    createHeaders(pracdt);
                    pracDataGridView.DataSource = pracdt;
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
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
        private static void createHeaders(DataTable pracdt)
        {
            pracdt.Columns[0].ColumnName = "Practice Name";
            pracdt.Columns[1].ColumnName = "Practice Address";
            pracdt.Columns[2].ColumnName = "Practice Suburb";
            pracdt.Columns[3].ColumnName = "Practice Post Code";
            pracdt.Columns[4].ColumnName = "Practice State";
            pracdt.Columns[5].ColumnName = "Practice Phone";
            pracdt.Columns[6].ColumnName = "Practice Fax";
            pracdt.Columns[7].ColumnName = "Practice Notes";
        }

        public static void ViewSearchedRecords(DataGridView pracDGV, TextBox searchText, ComboBox searchDropDown)
        {
            //Shows error if nothing is written into the input box
            if (string.IsNullOrWhiteSpace(searchText.Text) == true)
            {
                MessageBox.Show("Please Write a String to Search", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //string searchBy = searchDropDown.SelectedItem.ToString();    //Holds String of Currently selected search item
            string query = null;                                    //Holds string for the sql query

            //searchBy is the variable for the chosen search option
            switch (searchDropDown.SelectedItem.ToString())
            {
                case "Practice Name":
                    query = CreateQuery("PRAC_Name", "%" + searchText.Text + "%", "LIKE");
                    break;

                case "Address":
                    query = CreateQuery("PRAC_Address", "%" + searchText.Text + "%", "LIKE");
                    break;

                case "Suburb":
                    query = CreateQuery("PRAC_Suburb", "%" + searchText.Text + "%", "LIKE");
                    break;

                case "Post Code":
                    query = CreateQuery("PRAC_Post_Code", "%" + searchText.Text + "%", "LIKE");
                    break;

                case "State":
                    query = CreateQuery("PRAC_State", "%" + searchText.Text + "%", "LIKE");
                    break;

                case "Phone":
                    query = CreateQuery("PRAC_Phone", "%" + searchText.Text + "%", "LIKE");
                    break;

                case "Fax":
                    query = CreateQuery("PRAC_Fax", "%" + searchText.Text + "%", "LIKE");
                    break;
            }
            ViewPractices(pracDGV, query);
        }

        public static string CreateQuery(string columnName, string searchName, string searchOperator)
        {
            string query = "SELECT * FROM [GENERAL PRACTICE] " +
            "WHERE " +
            columnName + " " + searchOperator + " '" + searchName + "' " +
            "ORDER BY PRAC_Name;";
            return query;
        }

        public static void Show_HideSearchOptions(ComboBox searchSelectionDropDown, TextBox searchText, Label searchLabel1)
        {
            searchText.ReadOnly = false;
            searchText.Text = null;
            string item = searchSelectionDropDown.SelectedItem.ToString();
            switch (item)
            {
                case "Practice Name":
                    searchLabel1.Text = "Practice Name:";
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
                case "Phone":
                    searchLabel1.Text = "Phone:";
                    break;
                case "Fax":
                    searchLabel1.Text = "Fax:";
                    break;
            }
        }
    }
}
