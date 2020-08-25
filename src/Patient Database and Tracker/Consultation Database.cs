using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using Finisar.SQLite;

namespace Patient_Database_and_Tracker
{
    //DDL for Consultation
                //     "CREATE TABLE [CONSULTATION]" +
                //    "(CONS_ID_Number TEXT NOT NULL PRIMARY KEY UNIQUE," +
                //    "PAT_ID_Number INT NOT NULL," +
                //    "CONS_Consultant TEXT NOT NULL," +
                //    "CONS_Time_Period TEXT NOT NULL," +
                //    "CONS_Date DATE NOT NULL," +
                //    "CONS_Health_Rating TEXT," +
                //    "CONS_Smoking_Status TEXT," +
                //    "CONS_Alcohol_Days NUMERIC," +
                //    "CONS_Alcohol_Drinks NUMERIC," +
                //    "CONS_Height NUMERIC," +
                //    "CONS_Weight NUMERIC," +
                //    "CONS_BMI NUMERIC," +
                //    "CONS_Waist NUMERIC," +
                //    "CONS_SBP NUMERIC," +
                //    "CONS_DBP NUMERIC," +
                //    "CONS_Body_Fat_Percentage NUMERIC," +
                //    "CONS_LBM NUMERIC," +
                //    "CONS_Visceral_Fat_Rating NUMERIC," +
                //    "CONS_CHO_TC NUMERIC," +
                //    "CONS_CHO_LDL NUMERIC," +
                //    "CONS_CHO_HDL NUMERIC," +
                //    "CONS_CHO_TG NUMERIC," +
                //    "CONS_Fasting_Glucose NUMERIC," +
                //    "CONS_HbA1c NUMERIC," +
                //    "CONS_Resistance INTEGER," +
                //    "CONS_Cardio INTEGER," +
                //    "CONS_Brisk_Walk INTEGER," +
                //    "CONS_Light_Activity INTEGER," +
                //    "CONS_Fruit_Serves NUMERIC," +
                //    "CONS_Fruit_Greater_2_Serves NUMERIC," +
                //    "CONS_Vegetable_Serves NUMERIC," +
                //    "CONS_Vegetable_Greater_5_Serves NUMERIC," +
                //    "CONS_Commercial_Meals INTEGER," +
                //    "CONS_Sweets INTEGER," +
                //    "CONS_Soft_Drinks INTEGER," +
                //    "CONS_Skip_Main_Meals INTEGER," +
                //    "CONS_Keep_Track_Of_Food INTEGER," +
                //    "CONS_Limit_Portions INTEGER," +
                //    "CONS_Eat_When_Upset INTEGER," +
                //    "CONS_Eat_In_Front_Of_TV INTEGER," +
                //    "CONS_Choose_Healthier_Foods INTEGER," +
                //    "CONS_Notes TEXT);";

    class Consultation_Database
    {
        //Used to refresh the datagridview objects after sql statements are executed
        //Only used in AddEditConsultation1.  Chooses consultations based on patientDropDownList
        public static void PopulateConsultationDataGridView(ComboBox patientDropDownList, DataGridView dgv)
        {
            DataTable consdt = new DataTable();     //Will hold datasource of dgv

            //SQL Query
            string query = "SELECT CONS_ID_Number, CONS_Time_Period, CONS_Date FROM [CONSULTATION] WHERE PAT_ID_Number =" +
                patientDropDownList.SelectedValue + " ORDER BY CONS_Date, CONS_Time_Period;";
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {
                try
                {
                    Global.connection.Open();
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    consdt.Columns.Add("Cons ID");
                    consdt.Columns.Add("Period");
                    consdt.Columns.Add("Date");
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string id = reader.GetString(0);
                        string period = reader.GetString(1);
                        string date = reader.GetDateTime(2).ToShortDateString();
                        string displayText = "CONS ID: " + reader.GetString(0) + " *** " + reader.GetString(1) +
                            " " + reader.GetDateTime(2).ToShortDateString();
                        consdt.Rows.Add(id, period, date);
                    }

                    dgv.DataSource = consdt;
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        //Used to refresh the datagridview objects after sql statements are executed
        //Only used in AddEditConsultation2.  Chooses consultations based on Global.consultationId
        //Overloaded Method
        public static void PopulateConsultationDataGridView(DataGridView dgv)
        {
            DataTable consdt = new DataTable();
            string query = "SELECT CONS_ID_Number, CONS_Time_Period, CONS_Date FROM [CONSULTATION] WHERE PAT_ID_Number =" + Global.consPatientID + " ORDER BY CONS_Date, CONS_Time_Period;";
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {
                try
                {
                    Global.connection.Open();
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    consdt.Columns.Add("Cons ID");
                    consdt.Columns.Add("Period");
                    consdt.Columns.Add("Date");
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string id = reader.GetString(0);
                        string period = reader.GetString(1);
                        string date = reader.GetDateTime(2).ToShortDateString();
                        string displayText = "CONS ID: " + reader.GetString(0) + " *** " + reader.GetString(1) +
                            " " + reader.GetDateTime(2).ToShortDateString();
                        consdt.Rows.Add(id, period, date);
                    }

                    dgv.DataSource = consdt;
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                //iF editing exisitng then select the id which is being edited
                if (Global.editingExistingConsulation == true)
                {
                    dgv.Select();
                    dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        if (row.Cells[0].Value.ToString().Equals(Global.consID))
                        {
                            dgv.CurrentCell = row.Cells[0];
                            break;
                        }
                        else
                            dgv.ClearSelection();
                    }
                }
            }
        }

        //Deletes consultation
        public static bool DeleteConsultation(ComboBox patientDropDownList, DataGridView dgv)
        {
            //Holds consultation ID to be deleted
            string consID = "";

            //Checks if there are any consultations for the patient
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("There are no existing conultations For this patient", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Checks if the user has selected a consultation
            if (dgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a consultation from the list To delete", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Gets consid
            int cellIndex = dgv.SelectedCells[0].RowIndex;
            DataGridViewCell cellCollection = dgv.Rows[cellIndex].Cells[0];
            consID = cellCollection.Value.ToString();

            //Gives user a chocie to continue
            DialogResult areYouSure = MessageBox.Show("Are you sure you want to delete this consultation?\n\n" +
                "ConsID: " + consID + "\n" + patientDropDownList.Text + "\nDeleting is permanent",
                "Are You Sure", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (areYouSure == DialogResult.No)
            {
                return false;
            }
            
            //Deletes Consultation
            else if (areYouSure == DialogResult.Yes)
            {
                string query = "DELETE FROM [CONSULTATION] WHERE CONS_ID_Number ='" + consID + "';";

                using (Global.connection = new SQLiteConnection(Global.connectionString))
                using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
                {
                    try
                    {
                        Global.connection.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Consultation deleted", "Delete Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            return true;
        }

        //Populates textboxes with fields from an exisitng consultation
        public static void EditConsultation(TextBox id, TextBox smoke, TextBox alcDays, TextBox alcDrinks,
            TextBox height, TextBox weight, TextBox bmi, TextBox waist, TextBox sbp, TextBox dbp, TextBox bodyFat, TextBox lbm,
            TextBox visceralFat, TextBox cho_tc, TextBox cho_ldl, TextBox cho_hdl, TextBox cho_tg, TextBox fastingGlucose,
            TextBox hba1c, TextBox resistance, TextBox cardio, TextBox briskWalk, TextBox lightAct, TextBox fruitServes,
            TextBox fruitServes2, TextBox vegServes, TextBox vegServes5, TextBox comMeals, TextBox sweets, TextBox sDrinks,
            TextBox skipMeals, TextBox keepTrack, TextBox limit, TextBox eatUpset, TextBox eatTV, TextBox chooseHealth,
            TextBox notes, DateTimePicker date, ComboBox healthRating, ComboBox userDropDownList)
        {
            string consName = null;        //Holds Consultant name
            string query = "SELECT * FROM [CONSULTATION] WHERE CONS_ID_Number = '" +    //SQL Query
                Global.consID + "';";
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {
                try
                {
                    Global.connection.Open();
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        id.Text = Global.consID;
                        date.Value = Global.consDate;

                        //Will populate all fields in Add_Edit_Consutlation_2
                        consName = reader.GetString(2);
                        healthRating.SelectedIndex = healthRating.FindString(reader.GetString(5));
                        smoke.Text = reader.GetString(6);
                        alcDays.Text = reader.GetDouble(7).ToString();
                        alcDrinks.Text = reader.GetDouble(8).ToString();
                        height.Text = reader.GetDouble(9).ToString();
                        weight.Text = reader.GetDouble(10).ToString();
                        bmi.Text = reader.GetDouble(11).ToString();
                        waist.Text = reader.GetDouble(12).ToString();
                        sbp.Text = reader.GetDouble(13).ToString();
                        dbp.Text = reader.GetDouble(14).ToString();
                        bodyFat.Text = reader.GetDouble(15).ToString();
                        lbm.Text = reader.GetDouble(16).ToString();
                        visceralFat.Text = reader.GetDouble(17).ToString();
                        cho_tc.Text = reader.GetDouble(18).ToString();
                        cho_ldl.Text = reader.GetDouble(19).ToString();
                        cho_hdl.Text = reader.GetDouble(20).ToString();
                        cho_tg.Text = reader.GetDouble(21).ToString();
                        fastingGlucose.Text = reader.GetDouble(22).ToString();
                        hba1c.Text = reader.GetDouble(23).ToString();
                        resistance.Text = reader.GetDouble(24).ToString();
                        cardio.Text = reader.GetDouble(25).ToString();
                        briskWalk.Text = reader.GetDouble(26).ToString();
                        lightAct.Text = reader.GetDouble(27).ToString();
                        fruitServes.Text = reader.GetDouble(28).ToString();
                        fruitServes2.Text = reader.GetDouble(29).ToString();
                        vegServes.Text = reader.GetDouble(30).ToString();
                        vegServes5.Text = reader.GetDouble(31).ToString();
                        comMeals.Text = reader.GetDouble(32).ToString();
                        sweets.Text = reader.GetDouble(33).ToString();
                        sDrinks.Text = reader.GetDouble(34).ToString();
                        skipMeals.Text = reader.GetDouble(35).ToString();
                        keepTrack.Text = reader.GetDouble(36).ToString();
                        limit.Text = reader.GetDouble(37).ToString();
                        eatUpset.Text = reader.GetDouble(38).ToString();
                        eatTV.Text = reader.GetDouble(39).ToString();
                        chooseHealth.Text = reader.GetDouble(40).ToString();
                        notes.Text = reader.GetString(41);
                    }
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                //Populates consultant dropdown box
                DataTable userdt = UserDatabase.PopulateUserDropdown(userDropDownList);
                int consSelectedIndex = 0;          //Holds the selected index for userDropDownList
                int counter = 0;                //Counter will be used to find refgpSelectedIndex

                //Loop through datatable to find the row with the selected ID
                foreach (DataRow row in userdt.Rows)
                {
                    if (row[0].ToString() == consName)
                    {
                        consSelectedIndex = counter;
                        break;
                    }
                    counter++;
                }
                userDropDownList.SelectedIndex = consSelectedIndex;       //Selects the correct Reffering GP from Reffering_GP_Dropdown
            }
        }

        //Saves consultation to database
        public static bool SaveConsultation(TextBox id,  TextBox timePeriod, TextBox smoke, TextBox alcDays, TextBox alcDrinks,
            TextBox height, TextBox weight, TextBox bmi, TextBox waist, TextBox sbp, TextBox dbp, TextBox bodyFat, TextBox lbm,
            TextBox visceralFat, TextBox cho_tc, TextBox cho_ldl, TextBox cho_hdl, TextBox cho_tg, TextBox fastingGlucose,
            TextBox hba1c, TextBox resistance, TextBox cardio, TextBox briskWalk, TextBox lightAct, TextBox fruitServes,
            TextBox fruitServes2, TextBox vegServes, TextBox vegServes5, TextBox comMeals, TextBox sweets, TextBox sDrinks,
            TextBox skipMeals, TextBox keepTrack, TextBox limit, TextBox eatUpset, TextBox eatTV, TextBox chooseHealth,
            TextBox notes, DateTimePicker date, ComboBox healthRating, ComboBox userDropDownList)
        {
            string query = null;
            int ConsultCount = 1;       //Used to maintain a unique consultation id
            string time = null;         //Time will be used as part of the Consultant ID

            //If New Consultation the following code creates the unique ID
            if (Global.editingExistingConsulation == false)
            {
                query = "SELECT CONS_Time_Period FROM [CONSULTATION] WHERE PAT_ID_Number =" +
                    Global.consPatientID + ";";         //SQL Query
                
                //Consultation ID uses the following C- Patient ID - Time Period - consult count
                //Example: Patient ID: 5, Initial, 1st = C5-I-1
                using (Global.connection = new SQLiteConnection(Global.connectionString))
                using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
                {
                    switch (timePeriod.Text)
                    {
                        case "Initial":
                            time = "I";
                            break;
                        case "3 Months":
                            time = "3";
                            break;
                        case "6 Months":
                            time = "6";
                            break;
                        case "12 Months":
                            time = "12";
                            break;
                    }
                    try
                    {
                        Global.connection.Open();
                        SQLiteDataReader reader = cmd.ExecuteReader();

                        //Consult count is the number of times a certain time period has been used by this patient
                        //It helps maintain a unique id
                        while (reader.Read())
                        {
                            if (reader.GetString(0) == timePeriod.Text)
                            {
                                ConsultCount++;
                            }
                        }
                        reader.Dispose();
                        Global.connection.Close();
                    //Consultations won't usually repeat in time periods so this messagebox
                    //will promt the user in case this is a mistake
                    if (ConsultCount > 1)
                    {
                        DialogResult dr = MessageBox.Show("There is already a previous consultation for this time period" +
                            "\nAre you sure you would like to continue?", "Are You Sure",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dr == DialogResult.No)
                        {
                            return false;
                        }
                    }

                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                    query = "INSERT INTO [CONSULTATION] VALUES(@consID, @patID, @consultant, @time, @date, @health, @smoke, @alcDays, @alcDrinks, @height, @weight, @bmi, @waist, @sbp, @dbp, @bodyFat, " +
                        "@lbm, @visceralFat, @chotc, @choldl, @chohdl, @chotg, @fastingGlucose, @hba1c, @resistance, @cardio, @briskWalk, @lightActivity, " +
                        "@fServes, @fServes_2, @vServes, @vServes_5, @comMeals, @sweets, @softDrinks, @skipMeals, @keepTrack, @limitPortions, @eatUpset, " +
                        "@eatTV, @chooseHealthier, @notes);";
            }
            else
            {
                query = "UPDATE [CONSULTATION] SET PAT_ID_Number = @patID, CONS_Consultant = @consultant, CONS_Time_Period = @time," +
                    "CONS_Date = @date, CONS_Health_Rating = @health, CONS_Smoking_Status = @smoke," +
                    "CONS_Alcohol_Days = @alcDays, CONS_Alcohol_Drinks = @alcDrinks, CONS_Height = @height, CONS_Weight = @weight, CONS_BMI = @bmi, CONS_Waist = @waist," +
                    "CONS_SBP = @sbp, CONS_DBP = @dbp, CONS_Body_Fat_Percentage = @bodyFat," +
                    "CONS_LBM = @lbm, CONS_Visceral_Fat_Rating = @visceralFat, CONS_CHO_TC = @chotc, CONS_CHO_LDL = @choldl," +
                    "CONS_CHO_HDL = @chohdl, CONS_CHO_TG = @chotg, CONS_Fasting_Glucose = @fastingGlucose, CONS_HbA1c = @hba1c," +
                    "CONS_Resistance  = @resistance, CONS_Cardio = @cardio, CONS_Brisk_Walk = @briskWalk," +
                    "CONS_Light_Activity = @lightActivity, CONS_Fruit_Serves = @fServes, CONS_Fruit_Greater_2_Serves = @fServes_2," +
                    "CONS_Vegetable_Serves = @vServes, CONS_Vegetable_Greater_5_Serves = @vServes_5, CONS_Commercial_Meals = @comMeals," +
                    "CONS_Sweets = @sweets, CONS_Soft_Drinks = @softDrinks, CONS_Skip_Main_Meals = @skipMeals, CONS_Keep_Track_Of_Food = @keepTrack," +
                    "CONS_Limit_Portions = @limitPortions, CONS_Eat_When_Upset = @eatUpset, CONS_Eat_In_Front_Of_TV = @eatTV," +
                    " CONS_Choose_Healthier_Foods = @chooseHealthier, CONS_Notes = @notes WHERE CONS_ID_Number ='" + Global.consID + "';";
            }
                using (Global.connection = new SQLiteConnection(Global.connectionString))
                using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
                {
        
                    //Setting parameters for input data
                    //Counsultation ID is as follows: Patient ID - Time Period - Number of Time Period
                    cmd.Parameters.Add("@consID", "C" + Global.consPatientID.ToString() + "-" + time + "-" + ConsultCount);
                    cmd.Parameters.Add("@patID", Global.consPatientID);
                    cmd.Parameters.Add("@time", timePeriod.Text);
                    cmd.Parameters.Add("@date", Global.getDateString(date));

                    //Following parameters can be null so needs if else to set to null if nothing input
                    if (healthRating.SelectedIndex == -1)
                    {
                        cmd.Parameters.Add("@health", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.Add("@health", healthRating.SelectedItem.ToString());
                    }

                    cmd.Parameters.Add("@consultant", userDropDownList.SelectedValue);

                    if (String.IsNullOrWhiteSpace(smoke.Text))
                    {
                        cmd.Parameters.Add("@smoke", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.Add("@smoke", smoke.Text);
                    }
                    if (String.IsNullOrWhiteSpace(alcDays.Text))
                    {
                        cmd.Parameters.Add("@alcDays", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.Add("@alcDays", double.Parse(alcDays.Text));
                    }
                    if (String.IsNullOrWhiteSpace(alcDrinks.Text))
                    {
                        cmd.Parameters.Add("@alcDrinks", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.Add("@alcDrinks", double.Parse(alcDrinks.Text));
                    }
                    if (String.IsNullOrWhiteSpace(height.Text))
                    {
                        cmd.Parameters.Add("@height", DBNull.Value);
                    }
                    else
                    {
                            cmd.Parameters.Add("@height", double.Parse(height.Text));
                    }
                    if (String.IsNullOrWhiteSpace(weight.Text))
                    {
                        cmd.Parameters.Add("@weight", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.Add("@weight", double.Parse(weight.Text));
                    }
                    if (String.IsNullOrWhiteSpace(bmi.Text))
                    {
                        cmd.Parameters.Add("@bmi", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.Add("@bmi", double.Parse(bmi.Text));
                    }
                    if (String.IsNullOrWhiteSpace(waist.Text))
                    {
                        cmd.Parameters.Add("@waist", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.Add("@waist", double.Parse(waist.Text));
                    }
                    if (String.IsNullOrWhiteSpace(sbp.Text))
                    {
                        cmd.Parameters.Add("@sbp", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.Add("@sbp", double.Parse(sbp.Text));
                    }
                    if (String.IsNullOrWhiteSpace(dbp.Text))
                    {
                        cmd.Parameters.Add("@dbp", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.Add("@dbp", double.Parse(dbp.Text));
                    }
                    if (String.IsNullOrWhiteSpace(bodyFat.Text))
                    {
                        cmd.Parameters.Add("@bodyFat", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.Add("@bodyFat", double.Parse(bodyFat.Text));
                    }
                    if (String.IsNullOrWhiteSpace(lbm.Text))
                    {
                        cmd.Parameters.Add("@lbm", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.Add("@lbm", double.Parse(lbm.Text));
                    }
                    if (String.IsNullOrWhiteSpace(visceralFat.Text))
                    {
                        cmd.Parameters.Add("@visceralFat", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.Add("@visceralFat", double.Parse(visceralFat.Text));
                    }
                    if (String.IsNullOrWhiteSpace(cho_tc.Text))
                    {
                        cmd.Parameters.Add("@chotc", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.Add("@chotc", double.Parse(cho_tc.Text));
                    }
                    if (String.IsNullOrWhiteSpace(cho_ldl.Text))
                    {
                        cmd.Parameters.Add("@choldl", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.Add("@choldl", double.Parse(cho_ldl.Text));
                    }
                    if (String.IsNullOrWhiteSpace(cho_hdl.Text))
                    {
                        cmd.Parameters.Add("@chohdl", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.Add("@chohdl", double.Parse(cho_hdl.Text));
                    }

                    if (String.IsNullOrWhiteSpace(cho_tg.Text))
                    {
                        cmd.Parameters.Add("@chotg", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.Add("@chotg", double.Parse(cho_tg.Text));
                    }
                    if (String.IsNullOrWhiteSpace(fastingGlucose.Text))
                    {
                        cmd.Parameters.Add("@fastingGlucose", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.Add("@fastingGlucose", double.Parse(fastingGlucose.Text));
                    }
                    if (String.IsNullOrWhiteSpace(hba1c.Text))
                    {
                        cmd.Parameters.Add("@hba1c", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.Add("@hba1c", double.Parse(hba1c.Text));
                    }
                    if (String.IsNullOrWhiteSpace(resistance.Text))
                    {
                        cmd.Parameters.Add("@resistance", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.Add("@resistance", double.Parse(resistance.Text));
                    }
                    if (String.IsNullOrWhiteSpace(cardio.Text))
                    {
                        cmd.Parameters.Add("@cardio", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.Add("@cardio", double.Parse(cardio.Text));
                    }
                    if (String.IsNullOrWhiteSpace(briskWalk.Text))
                    {
                        cmd.Parameters.Add("@briskWalk", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.Add("@briskWalk", double.Parse(briskWalk.Text));
                    }
                    if (String.IsNullOrWhiteSpace(lightAct.Text))
                    {
                        cmd.Parameters.Add("@lightActivity", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.Add("@lightActivity", double.Parse(lightAct.Text));
                    }
                    if (String.IsNullOrWhiteSpace(fruitServes.Text))
                    {
                        cmd.Parameters.Add("@fServes", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.Add("@fServes", double.Parse(fruitServes.Text));
                    }
                    if (String.IsNullOrWhiteSpace(fruitServes2.Text))
                    {
                        cmd.Parameters.Add("@fServes_2", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.Add("@fServes_2", double.Parse(fruitServes2.Text));
                    }
                    if (String.IsNullOrWhiteSpace(vegServes.Text))
                    {
                        cmd.Parameters.Add("@vServes", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.Add("@vServes", double.Parse(vegServes.Text));
                    }
                    if (String.IsNullOrWhiteSpace(vegServes5.Text))
                    {
                        cmd.Parameters.Add("@vServes_5", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.Add("@vServes_5", double.Parse(vegServes5.Text));
                    }
                    if (String.IsNullOrWhiteSpace(comMeals.Text))
                    {
                        cmd.Parameters.Add("@comMeals", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.Add("@comMeals", double.Parse(comMeals.Text));
                    }
                    if (String.IsNullOrWhiteSpace(sweets.Text))
                    {
                        cmd.Parameters.Add("@sweets", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.Add("@sweets", double.Parse(sweets.Text));
                    }
                    if (String.IsNullOrWhiteSpace(sDrinks.Text))
                    {
                        cmd.Parameters.Add("@softDrinks", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.Add("@softDrinks", double.Parse(sDrinks.Text));
                    }
                    if (String.IsNullOrWhiteSpace(skipMeals.Text))
                    {
                        cmd.Parameters.Add("@skipMeals", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.Add("@skipMeals", double.Parse(skipMeals.Text));
                    }
                    if (String.IsNullOrWhiteSpace(keepTrack.Text))
                    {
                        cmd.Parameters.Add("@keepTrack", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.Add("@keepTrack", double.Parse(keepTrack.Text));
                    }
                    if (String.IsNullOrWhiteSpace(limit.Text))
                    {
                        cmd.Parameters.Add("@limitPortions", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.Add("@limitPortions", double.Parse(limit.Text));
                    }
                    if (String.IsNullOrWhiteSpace(eatUpset.Text))
                    {
                        cmd.Parameters.Add("@eatUpset", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.Add("@eatUpset", double.Parse(eatUpset.Text));
                    }
                    if (String.IsNullOrWhiteSpace(eatTV.Text))
                    {
                        cmd.Parameters.Add("@eatTV", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.Add("@eatTV", double.Parse(eatTV.Text));
                    }
                    if (String.IsNullOrWhiteSpace(chooseHealth.Text))
                    {
                        cmd.Parameters.Add("@chooseHealthier", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.Add("@chooseHealthier", double.Parse(chooseHealth.Text));
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
                        if (Global.editingExistingConsulation == false)
                        {
                            id.Text = "C" + Global.consPatientID.ToString() + "-" + time + "-" + ConsultCount;
                            Global.consID = id.Text;
                        }
                        Global.connection.Close();
                        Global.editingExistingConsulation = false;
                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                return true;
        }

        //Checks if there is more than one initial consultation when generating letters
        public static DataTable CheckInitialConsultations(ComboBox patientDropDownList)
        {
            //SQL Query
            string query = "SELECT CONS_ID_Number, CONS_Date, CONS_Time_Period FROM [CONSULTATION] WHERE PAT_ID_Number =" +
                patientDropDownList.SelectedValue + ";";

            //Datatable used to hold data of initial Consultations
            DataTable initialConsultations = new DataTable();
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {
                try
                {
                    initialConsultations.Columns.Add("ID");
                    initialConsultations.Columns.Add("Date");
                    initialConsultations.Columns.Add("Display Text");
                    Global.connection.Open();
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader.GetString(2) == "Initial")
                        {
                            string id = reader.GetString(0);
                            string date = reader.GetDateTime(1).ToShortDateString();
                            string displayText = "CONS ID: " + id + " *** Date: " + date;
                            initialConsultations.Rows.Add(id, date, displayText);
                        }
                    }
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return initialConsultations;
        }

        //Will be used with other methods to view all and view searched data
        //Used in View_Records
        private static void ViewConsultations(DataGridView consDataGridView, string query)
        {
            DataTable consdt = new DataTable();
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            {

                Global.connection.Open();
                SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(query, Global.connection);
                dataAdapter.Fill(consdt);
                CreateHeaders(consdt);
                consDataGridView.DataSource = consdt;
                Global.connection.Close();
            }
        }
        //Used in ViewPatients Method.  Sets the headers of the datatable
        private static void CreateHeaders(DataTable patientdt)
        {
            patientdt.Columns[0].ColumnName = "Consultation ID";
            patientdt.Columns[1].ColumnName = "Patient ID";
            patientdt.Columns[2].ColumnName = "Patient First Name";
            patientdt.Columns[3].ColumnName = "Patient Last Name";
            patientdt.Columns[4].ColumnName = "Consultant";
            patientdt.Columns[5].ColumnName = "Time Period";
            patientdt.Columns[6].ColumnName = "Consultation Date";
            patientdt.Columns[7].ColumnName = "Health Rating";
            patientdt.Columns[8].ColumnName = "Smoking Status";
            patientdt.Columns[9].ColumnName = "Days P/W Consuming Alcohol";
            patientdt.Columns[10].ColumnName = "No. Drinks of Alcohol P/W";
            patientdt.Columns[11].ColumnName = "Height";
            patientdt.Columns[12].ColumnName = "Weight";
            patientdt.Columns[13].ColumnName = "BMI";
            patientdt.Columns[14].ColumnName = "Waist";
            patientdt.Columns[15].ColumnName = "SBP";
            patientdt.Columns[16].ColumnName = "DBP";
            patientdt.Columns[17].ColumnName = "Body Fat %";
            patientdt.Columns[18].ColumnName = "LBM";
            patientdt.Columns[19].ColumnName = "Visceral Fat Rating";
            patientdt.Columns[20].ColumnName = "CHO_TC";
            patientdt.Columns[21].ColumnName = "CHO_LDL";
            patientdt.Columns[22].ColumnName = "CHO_HDL";
            patientdt.Columns[23].ColumnName = "CHO_TG";
            patientdt.Columns[24].ColumnName = "Fasting Glucose";
            patientdt.Columns[25].ColumnName = "HbA1c";
            patientdt.Columns[26].ColumnName = "Resistance";
            patientdt.Columns[27].ColumnName = "Cardio";
            patientdt.Columns[28].ColumnName = "Brisk Walk";
            patientdt.Columns[29].ColumnName = "Light Activity";
            patientdt.Columns[30].ColumnName = "Fruit Sevres P/W";
            patientdt.Columns[31].ColumnName = "No. Days More than 2 Fruit Serves P/W";
            patientdt.Columns[32].ColumnName = "Vegetable Sevres P/W";
            patientdt.Columns[33].ColumnName = "No. Days More than 5 Vegetable Serves P/W";
            patientdt.Columns[34].ColumnName = "Commercial Meals";
            patientdt.Columns[35].ColumnName = "Sweets";
            patientdt.Columns[36].ColumnName = "Soft Drinks";
            patientdt.Columns[37].ColumnName = "Skip Main Meals";
            patientdt.Columns[38].ColumnName = "Keep Track of Food";
            patientdt.Columns[39].ColumnName = "Limit Portions";
            patientdt.Columns[40].ColumnName = "Eat When Upset";
            patientdt.Columns[41].ColumnName = "Eat in Front of TV";
            patientdt.Columns[42].ColumnName = "Choose Healthier Options";
            patientdt.Columns[43].ColumnName = "Notes";
        }

        //Used to create a query based on current patients
        public static string CreateCurrentQuery(string columnName, string searchName, string searchOperator, bool stringSearch)
        {
            string query = null;

            //If searching a string
            if (stringSearch == true)
            {
                query = "SELECT " +
                    "CONSULTATION.[CONS_ID_Number], " +
                    "CONSULTATION.[PAT_ID_Number], " +
                    "PATIENT.[PAT_First_Name], " +
                    "PATIENT.[PAT_Last_Name], " +
                    "CONSULTATION.[CONS_Consultant], " +
                    "CONSULTATION.[CONS_Time_Period], " +
                    "CONSULTATION.[CONS_Date], " +
                    "CONSULTATION.[CONS_Health_Rating], " +
                    "CONSULTATION.[CONS_Smoking_Status], " +
                    "CONSULTATION.[CONS_Alcohol_Days], " +
                    "CONSULTATION.[CONS_Alcohol_Drinks], " +
                    "CONSULTATION.[CONS_Height], " +
                    "CONSULTATION.[CONS_Weight], " +
                    "CONSULTATION.[CONS_BMI], " +
                    "CONSULTATION.[CONS_Waist], " +
                    "CONSULTATION.[CONS_SBP], " +
                    "CONSULTATION.[CONS_DBP], " +
                    "CONSULTATION.[CONS_Body_Fat_Percentage], " +
                    "CONSULTATION.[CONS_LBM], " +
                    "CONSULTATION.[CONS_Visceral_Fat_Rating], " +
                    "CONSULTATION.[CONS_CHO_TC], " +
                    "CONSULTATION.[CONS_CHO_LDL], " +
                    "CONSULTATION.[CONS_CHO_HDL], " +
                    "CONSULTATION.[CONS_CHO_TG], " +
                    "CONSULTATION.[CONS_Fasting_Glucose], " +
                    "CONSULTATION.[CONS_HbA1c], " +
                    "CONSULTATION.[CONS_Resistance], " +
                    "CONSULTATION.[CONS_Cardio], " +
                    "CONSULTATION.[CONS_Brisk_Walk], " +
                    "CONSULTATION.[CONS_Light_Activity], " +
                    "CONSULTATION.[CONS_Fruit_Serves], " +
                    "CONSULTATION.[CONS_Fruit_Greater_2_Serves], " +
                    "CONSULTATION.[CONS_Vegetable_Serves], " +
                    "CONSULTATION.[CONS_Vegetable_Greater_5_Serves], " +
                    "CONSULTATION.[CONS_Commercial_Meals], " +
                    "CONSULTATION.[CONS_Sweets], " +
                    "CONSULTATION.[CONS_Soft_Drinks], " +
                    "CONSULTATION.[CONS_Skip_Main_Meals], " +
                    "CONSULTATION.[CONS_Keep_Track_Of_Food], " +
                    "CONSULTATION.[CONS_Limit_Portions], " +
                    "CONSULTATION.[CONS_Eat_When_Upset], " +
                    "CONSULTATION.[CONS_Eat_In_Front_Of_TV], " +
                    "CONSULTATION.[CONS_Choose_Healthier_Foods], " +
                    "CONSULTATION.[CONS_Notes] " +
                    "FROM CONSULTATION " +
                    "INNER JOIN [PATIENT] ON [CONSULTATION].[PAT_ID_Number] =  [PATIENT].[PAT_ID_Number] " +
                    "WHERE " +
                    columnName + " " + searchOperator + " '" + searchName + "' AND " +
                    "PAT_Current = 1 " +
                    "ORDER BY [PATIENT].[PAT_ID_Number];";
            }

            //If searching a number
            else if (stringSearch == false)
            {
                query = "SELECT " +
                    "CONSULTATION.[CONS_ID_Number], " +
                    "CONSULTATION.[PAT_ID_Number], " +
                    "PATIENT.[PAT_First_Name], " +
                    "PATIENT.[PAT_Last_Name], " +
                    "CONSULTATION.[CONS_Consultant], " +
                    "CONSULTATION.[CONS_Time_Period], " +
                    "CONSULTATION.[CONS_Date], " +
                    "CONSULTATION.[CONS_Health_Rating], " +
                    "CONSULTATION.[CONS_Smoking_Status], " +
                    "CONSULTATION.[CONS_Alcohol_Days], " +
                    "CONSULTATION.[CONS_Alcohol_Drinks], " +
                    "CONSULTATION.[CONS_Height], " +
                    "CONSULTATION.[CONS_Weight], " +
                    "CONSULTATION.[CONS_BMI], " +
                    "CONSULTATION.[CONS_Waist], " +
                    "CONSULTATION.[CONS_SBP], " +
                    "CONSULTATION.[CONS_DBP], " +
                    "CONSULTATION.[CONS_Body_Fat_Percentage], " +
                    "CONSULTATION.[CONS_LBM], " +
                    "CONSULTATION.[CONS_Visceral_Fat_Rating], " +
                    "CONSULTATION.[CONS_CHO_TC], " +
                    "CONSULTATION.[CONS_CHO_LDL], " +
                    "CONSULTATION.[CONS_CHO_HDL], " +
                    "CONSULTATION.[CONS_CHO_TG], " +
                    "CONSULTATION.[CONS_Fasting_Glucose], " +
                    "CONSULTATION.[CONS_HbA1c], " +
                    "CONSULTATION.[CONS_Resistance], " +
                    "CONSULTATION.[CONS_Cardio], " +
                    "CONSULTATION.[CONS_Brisk_Walk], " +
                    "CONSULTATION.[CONS_Light_Activity], " +
                    "CONSULTATION.[CONS_Fruit_Serves], " +
                    "CONSULTATION.[CONS_Fruit_Greater_2_Serves], " +
                    "CONSULTATION.[CONS_Vegetable_Serves], " +
                    "CONSULTATION.[CONS_Vegetable_Greater_5_Serves], " +
                    "CONSULTATION.[CONS_Commercial_Meals], " +
                    "CONSULTATION.[CONS_Sweets], " +
                    "CONSULTATION.[CONS_Soft_Drinks], " +
                    "CONSULTATION.[CONS_Skip_Main_Meals], " +
                    "CONSULTATION.[CONS_Keep_Track_Of_Food], " +
                    "CONSULTATION.[CONS_Limit_Portions], " +
                    "CONSULTATION.[CONS_Eat_When_Upset], " +
                    "CONSULTATION.[CONS_Eat_In_Front_Of_TV], " +
                    "CONSULTATION.[CONS_Choose_Healthier_Foods], " +
                    "CONSULTATION.[CONS_Notes] " +
                    "FROM CONSULTATION " +
                    "INNER JOIN [PATIENT] ON [CONSULTATION].[PAT_ID_Number] =  [PATIENT].[PAT_ID_Number] " +
                    "WHERE " +
                    columnName + " " + searchOperator + " " + searchName + " AND " +
                    "PAT_Current = 1 " +
                    "ORDER BY [PATIENT].[PAT_ID_Number];";
            }
            return query;
        }

        //Creates a sql query for current patients
        //Overloaded method used for searching 2 where clauses
        public static string CreateCurrentQuery(string columnName, string searchName, string searchOperator, string searchName2, string searchOperator2)
        {
            string query = "SELECT " +
                    "CONSULTATION.[CONS_ID_Number], " +
                    "CONSULTATION.[PAT_ID_Number], " +
                    "PATIENT.[PAT_First_Name], " +
                    "PATIENT.[PAT_Last_Name], " +
                    "CONSULTATION.[CONS_Consultant], " +
                    "CONSULTATION.[CONS_Time_Period], " +
                    "CONSULTATION.[CONS_Date], " +
                    "CONSULTATION.[CONS_Health_Rating], " +
                    "CONSULTATION.[CONS_Smoking_Status], " +
                    "CONSULTATION.[CONS_Alcohol_Days], " +
                    "CONSULTATION.[CONS_Alcohol_Drinks], " +
                    "CONSULTATION.[CONS_Height], " +
                    "CONSULTATION.[CONS_Weight], " +
                    "CONSULTATION.[CONS_BMI], " +
                    "CONSULTATION.[CONS_Waist], " +
                    "CONSULTATION.[CONS_SBP], " +
                    "CONSULTATION.[CONS_DBP], " +
                    "CONSULTATION.[CONS_Body_Fat_Percentage], " +
                    "CONSULTATION.[CONS_LBM], " +
                    "CONSULTATION.[CONS_Visceral_Fat_Rating], " +
                    "CONSULTATION.[CONS_CHO_TC], " +
                    "CONSULTATION.[CONS_CHO_LDL], " +
                    "CONSULTATION.[CONS_CHO_HDL], " +
                    "CONSULTATION.[CONS_CHO_TG], " +
                    "CONSULTATION.[CONS_Fasting_Glucose], " +
                    "CONSULTATION.[CONS_HbA1c], " +
                    "CONSULTATION.[CONS_Resistance], " +
                    "CONSULTATION.[CONS_Cardio], " +
                    "CONSULTATION.[CONS_Brisk_Walk], " +
                    "CONSULTATION.[CONS_Light_Activity], " +
                    "CONSULTATION.[CONS_Fruit_Serves], " +
                    "CONSULTATION.[CONS_Fruit_Greater_2_Serves], " +
                    "CONSULTATION.[CONS_Vegetable_Serves], " +
                    "CONSULTATION.[CONS_Vegetable_Greater_5_Serves], " +
                    "CONSULTATION.[CONS_Commercial_Meals], " +
                    "CONSULTATION.[CONS_Sweets], " +
                    "CONSULTATION.[CONS_Soft_Drinks], " +
                    "CONSULTATION.[CONS_Skip_Main_Meals], " +
                    "CONSULTATION.[CONS_Keep_Track_Of_Food], " +
                    "CONSULTATION.[CONS_Limit_Portions], " +
                    "CONSULTATION.[CONS_Eat_When_Upset], " +
                    "CONSULTATION.[CONS_Eat_In_Front_Of_TV], " +
                    "CONSULTATION.[CONS_Choose_Healthier_Foods], " +
                    "CONSULTATION.[CONS_Notes] " +
                    "FROM CONSULTATION " +
                    "INNER JOIN [PATIENT] ON [CONSULTATION].[PAT_ID_Number] =  [PATIENT].[PAT_ID_Number] " +
                    "WHERE " +
                columnName + " " + searchOperator + " '" + searchName + "' AND " +
                columnName + " " + searchOperator2 + " '" + searchName2 + "' AND " +
                "PAT_Current = 1 " +
                "ORDER BY [PATIENT].[PAT_ID_Number];";
            return query;
        }

        //Creates sql query for non current patients
        public static string CreateNonCurrentQuery(string columnName, string searchName, string searchOperator, bool stringSearch)
        {
            string query = null;

            //searching a string
            if (stringSearch == true)
            {
                query = "SELECT " +
                    "CONSULTATION.[CONS_ID_Number], " +
                    "CONSULTATION.[PAT_ID_Number], " +
                    "PATIENT.[PAT_First_Name], " +
                    "PATIENT.[PAT_Last_Name], " +
                    "CONSULTATION.[CONS_Consultant], " +
                    "CONSULTATION.[CONS_Time_Period], " +
                    "CONSULTATION.[CONS_Date], " +
                    "CONSULTATION.[CONS_Health_Rating], " +
                    "CONSULTATION.[CONS_Smoking_Status], " +
                    "CONSULTATION.[CONS_Alcohol_Days], " +
                    "CONSULTATION.[CONS_Alcohol_Drinks], " +
                    "CONSULTATION.[CONS_Height], " +
                    "CONSULTATION.[CONS_Weight], " +
                    "CONSULTATION.[CONS_BMI], " +
                    "CONSULTATION.[CONS_Waist], " +
                    "CONSULTATION.[CONS_SBP], " +
                    "CONSULTATION.[CONS_DBP], " +
                    "CONSULTATION.[CONS_Body_Fat_Percentage], " +
                    "CONSULTATION.[CONS_LBM], " +
                    "CONSULTATION.[CONS_Visceral_Fat_Rating], " +
                    "CONSULTATION.[CONS_CHO_TC], " +
                    "CONSULTATION.[CONS_CHO_LDL], " +
                    "CONSULTATION.[CONS_CHO_HDL], " +
                    "CONSULTATION.[CONS_CHO_TG], " +
                    "CONSULTATION.[CONS_Fasting_Glucose], " +
                    "CONSULTATION.[CONS_HbA1c], " +
                    "CONSULTATION.[CONS_Resistance], " +
                    "CONSULTATION.[CONS_Cardio], " +
                    "CONSULTATION.[CONS_Brisk_Walk], " +
                    "CONSULTATION.[CONS_Light_Activity], " +
                    "CONSULTATION.[CONS_Fruit_Serves], " +
                    "CONSULTATION.[CONS_Fruit_Greater_2_Serves], " +
                    "CONSULTATION.[CONS_Vegetable_Serves], " +
                    "CONSULTATION.[CONS_Vegetable_Greater_5_Serves], " +
                    "CONSULTATION.[CONS_Commercial_Meals], " +
                    "CONSULTATION.[CONS_Sweets], " +
                    "CONSULTATION.[CONS_Soft_Drinks], " +
                    "CONSULTATION.[CONS_Skip_Main_Meals], " +
                    "CONSULTATION.[CONS_Keep_Track_Of_Food], " +
                    "CONSULTATION.[CONS_Limit_Portions], " +
                    "CONSULTATION.[CONS_Eat_When_Upset], " +
                    "CONSULTATION.[CONS_Eat_In_Front_Of_TV], " +
                    "CONSULTATION.[CONS_Choose_Healthier_Foods], " +
                    "CONSULTATION.[CONS_Notes] " +
                    "FROM CONSULTATION " +
                    "INNER JOIN [PATIENT] ON [CONSULTATION].[PAT_ID_Number] =  [PATIENT].[PAT_ID_Number] " +
                    "WHERE " +
                    columnName + " " + searchOperator + " '" + searchName + "' AND " +
                    "PAT_Current = 0 " +
                    "ORDER BY [PATIENT].[PAT_ID_Number];";
            }

                //searching a number
            else if (stringSearch == false)
            {
                query = "SELECT " +
                    "CONSULTATION.[CONS_ID_Number], " +
                    "CONSULTATION.[PAT_ID_Number], " +
                    "PATIENT.[PAT_First_Name], " +
                    "PATIENT.[PAT_Last_Name], " +
                    "CONSULTATION.[CONS_Consultant], " +
                    "CONSULTATION.[CONS_Time_Period], " +
                    "CONSULTATION.[CONS_Date], " +
                    "CONSULTATION.[CONS_Health_Rating], " +
                    "CONSULTATION.[CONS_Smoking_Status], " +
                    "CONSULTATION.[CONS_Alcohol_Days], " +
                    "CONSULTATION.[CONS_Alcohol_Drinks], " +
                    "CONSULTATION.[CONS_Height], " +
                    "CONSULTATION.[CONS_Weight], " +
                    "CONSULTATION.[CONS_BMI], " +
                    "CONSULTATION.[CONS_Waist], " +
                    "CONSULTATION.[CONS_SBP], " +
                    "CONSULTATION.[CONS_DBP], " +
                    "CONSULTATION.[CONS_Body_Fat_Percentage], " +
                    "CONSULTATION.[CONS_LBM], " +
                    "CONSULTATION.[CONS_Visceral_Fat_Rating], " +
                    "CONSULTATION.[CONS_CHO_TC], " +
                    "CONSULTATION.[CONS_CHO_LDL], " +
                    "CONSULTATION.[CONS_CHO_HDL], " +
                    "CONSULTATION.[CONS_CHO_TG], " +
                    "CONSULTATION.[CONS_Fasting_Glucose], " +
                    "CONSULTATION.[CONS_HbA1c], " +
                    "CONSULTATION.[CONS_Resistance], " +
                    "CONSULTATION.[CONS_Cardio], " +
                    "CONSULTATION.[CONS_Brisk_Walk], " +
                    "CONSULTATION.[CONS_Light_Activity], " +
                    "CONSULTATION.[CONS_Fruit_Serves], " +
                    "CONSULTATION.[CONS_Fruit_Greater_2_Serves], " +
                    "CONSULTATION.[CONS_Vegetable_Serves], " +
                    "CONSULTATION.[CONS_Vegetable_Greater_5_Serves], " +
                    "CONSULTATION.[CONS_Commercial_Meals], " +
                    "CONSULTATION.[CONS_Sweets], " +
                    "CONSULTATION.[CONS_Soft_Drinks], " +
                    "CONSULTATION.[CONS_Skip_Main_Meals], " +
                    "CONSULTATION.[CONS_Keep_Track_Of_Food], " +
                    "CONSULTATION.[CONS_Limit_Portions], " +
                    "CONSULTATION.[CONS_Eat_When_Upset], " +
                    "CONSULTATION.[CONS_Eat_In_Front_Of_TV], " +
                    "CONSULTATION.[CONS_Choose_Healthier_Foods], " +
                    "CONSULTATION.[CONS_Notes] " +
                    "FROM CONSULTATION " +
                    "INNER JOIN [PATIENT] ON [CONSULTATION].[PAT_ID_Number] =  [PATIENT].[PAT_ID_Number] " +
                    "WHERE " +
                    columnName + " " + searchOperator + " " + searchName + " AND " +
                    "PAT_Current = 0 " +
                    "ORDER BY [PATIENT].[PAT_ID_Number];";
            }
            return query;
        }

        //Overloaded method searching non current patients
        public static string CreateNonCurrentQuery(string columnName, string searchName, string searchOperator, string searchName2, string searchOperator2)
        {
            string query = "SELECT " +
                    "CONSULTATION.[CONS_ID_Number], " +
                    "CONSULTATION.[PAT_ID_Number], " +
                    "PATIENT.[PAT_First_Name], " +
                    "PATIENT.[PAT_Last_Name], " +
                    "CONSULTATION.[CONS_Consultant], " +
                    "CONSULTATION.[CONS_Time_Period], " +
                    "CONSULTATION.[CONS_Date], " +
                    "CONSULTATION.[CONS_Health_Rating], " +
                    "CONSULTATION.[CONS_Smoking_Status], " +
                    "CONSULTATION.[CONS_Alcohol_Days], " +
                    "CONSULTATION.[CONS_Alcohol_Drinks], " +
                    "CONSULTATION.[CONS_Height], " +
                    "CONSULTATION.[CONS_Weight], " +
                    "CONSULTATION.[CONS_BMI], " +
                    "CONSULTATION.[CONS_Waist], " +
                    "CONSULTATION.[CONS_SBP], " +
                    "CONSULTATION.[CONS_DBP], " +
                    "CONSULTATION.[CONS_Body_Fat_Percentage], " +
                    "CONSULTATION.[CONS_LBM], " +
                    "CONSULTATION.[CONS_Visceral_Fat_Rating], " +
                    "CONSULTATION.[CONS_CHO_TC], " +
                    "CONSULTATION.[CONS_CHO_LDL], " +
                    "CONSULTATION.[CONS_CHO_HDL], " +
                    "CONSULTATION.[CONS_CHO_TG], " +
                    "CONSULTATION.[CONS_Fasting_Glucose], " +
                    "CONSULTATION.[CONS_HbA1c], " +
                    "CONSULTATION.[CONS_Resistance], " +
                    "CONSULTATION.[CONS_Cardio], " +
                    "CONSULTATION.[CONS_Brisk_Walk], " +
                    "CONSULTATION.[CONS_Light_Activity], " +
                    "CONSULTATION.[CONS_Fruit_Serves], " +
                    "CONSULTATION.[CONS_Fruit_Greater_2_Serves], " +
                    "CONSULTATION.[CONS_Vegetable_Serves], " +
                    "CONSULTATION.[CONS_Vegetable_Greater_5_Serves], " +
                    "CONSULTATION.[CONS_Commercial_Meals], " +
                    "CONSULTATION.[CONS_Sweets], " +
                    "CONSULTATION.[CONS_Soft_Drinks], " +
                    "CONSULTATION.[CONS_Skip_Main_Meals], " +
                    "CONSULTATION.[CONS_Keep_Track_Of_Food], " +
                    "CONSULTATION.[CONS_Limit_Portions], " +
                    "CONSULTATION.[CONS_Eat_When_Upset], " +
                    "CONSULTATION.[CONS_Eat_In_Front_Of_TV], " +
                    "CONSULTATION.[CONS_Choose_Healthier_Foods], " +
                    "CONSULTATION.[CONS_Notes] " +
                    "FROM CONSULTATION " +
                    "INNER JOIN [PATIENT] ON [CONSULTATION].[PAT_ID_Number] =  [PATIENT].[PAT_ID_Number] " +
                    "WHERE " +
                columnName + " " + searchOperator + " '" + searchName + "' AND " +
                columnName + " " + searchOperator2 + " '" + searchName2 + "' AND " +
                "PAT_Current = 0 " +
                "ORDER BY [PATIENT].[PAT_ID_Number];";
            return query;
        }

        //Creates query searching all patients
        public static string CreateAllQuery(string columnName, string searchName, string searchOperator, bool stringSearch)
        {
            string query = null;

            //Searching string
            if (stringSearch == true)
            {
                query = "SELECT " +
                    "CONSULTATION.[CONS_ID_Number], " +
                    "CONSULTATION.[PAT_ID_Number], " +
                    "PATIENT.[PAT_First_Name], " +
                    "PATIENT.[PAT_Last_Name], " +
                    "CONSULTATION.[CONS_Consultant], " +
                    "CONSULTATION.[CONS_Time_Period], " +
                    "CONSULTATION.[CONS_Date], " +
                    "CONSULTATION.[CONS_Health_Rating], " +
                    "CONSULTATION.[CONS_Smoking_Status], " +
                    "CONSULTATION.[CONS_Alcohol_Days], " +
                    "CONSULTATION.[CONS_Alcohol_Drinks], " +
                    "CONSULTATION.[CONS_Height], " +
                    "CONSULTATION.[CONS_Weight], " +
                    "CONSULTATION.[CONS_BMI], " +
                    "CONSULTATION.[CONS_Waist], " +
                    "CONSULTATION.[CONS_SBP], " +
                    "CONSULTATION.[CONS_DBP], " +
                    "CONSULTATION.[CONS_Body_Fat_Percentage], " +
                    "CONSULTATION.[CONS_LBM], " +
                    "CONSULTATION.[CONS_Visceral_Fat_Rating], " +
                    "CONSULTATION.[CONS_CHO_TC], " +
                    "CONSULTATION.[CONS_CHO_LDL], " +
                    "CONSULTATION.[CONS_CHO_HDL], " +
                    "CONSULTATION.[CONS_CHO_TG], " +
                    "CONSULTATION.[CONS_Fasting_Glucose], " +
                    "CONSULTATION.[CONS_HbA1c], " +
                    "CONSULTATION.[CONS_Resistance], " +
                    "CONSULTATION.[CONS_Cardio], " +
                    "CONSULTATION.[CONS_Brisk_Walk], " +
                    "CONSULTATION.[CONS_Light_Activity], " +
                    "CONSULTATION.[CONS_Fruit_Serves], " +
                    "CONSULTATION.[CONS_Fruit_Greater_2_Serves], " +
                    "CONSULTATION.[CONS_Vegetable_Serves], " +
                    "CONSULTATION.[CONS_Vegetable_Greater_5_Serves], " +
                    "CONSULTATION.[CONS_Commercial_Meals], " +
                    "CONSULTATION.[CONS_Sweets], " +
                    "CONSULTATION.[CONS_Soft_Drinks], " +
                    "CONSULTATION.[CONS_Skip_Main_Meals], " +
                    "CONSULTATION.[CONS_Keep_Track_Of_Food], " +
                    "CONSULTATION.[CONS_Limit_Portions], " +
                    "CONSULTATION.[CONS_Eat_When_Upset], " +
                    "CONSULTATION.[CONS_Eat_In_Front_Of_TV], " +
                    "CONSULTATION.[CONS_Choose_Healthier_Foods], " +
                    "CONSULTATION.[CONS_Notes] " +
                    "FROM CONSULTATION " +
                    "INNER JOIN [PATIENT] ON [CONSULTATION].[PAT_ID_Number] =  [PATIENT].[PAT_ID_Number] " +
                    "WHERE " +
                    columnName + " " + searchOperator + " '" + searchName + "' " +
                    "ORDER BY [PATIENT].[PAT_ID_Number];";

            }

                //searching number
            else if (stringSearch == false)
            {
                query = "SELECT " +
                    "CONSULTATION.[CONS_ID_Number], " +
                    "CONSULTATION.[PAT_ID_Number], " +
                    "PATIENT.[PAT_First_Name], " +
                    "PATIENT.[PAT_Last_Name], " +
                    "CONSULTATION.[CONS_Consultant], " +
                    "CONSULTATION.[CONS_Time_Period], " +
                    "CONSULTATION.[CONS_Date], " +
                    "CONSULTATION.[CONS_Health_Rating], " +
                    "CONSULTATION.[CONS_Smoking_Status], " +
                    "CONSULTATION.[CONS_Alcohol_Days], " +
                    "CONSULTATION.[CONS_Alcohol_Drinks], " +
                    "CONSULTATION.[CONS_Height], " +
                    "CONSULTATION.[CONS_Weight], " +
                    "CONSULTATION.[CONS_BMI], " +
                    "CONSULTATION.[CONS_Waist], " +
                    "CONSULTATION.[CONS_SBP], " +
                    "CONSULTATION.[CONS_DBP], " +
                    "CONSULTATION.[CONS_Body_Fat_Percentage], " +
                    "CONSULTATION.[CONS_LBM], " +
                    "CONSULTATION.[CONS_Visceral_Fat_Rating], " +
                    "CONSULTATION.[CONS_CHO_TC], " +
                    "CONSULTATION.[CONS_CHO_LDL], " +
                    "CONSULTATION.[CONS_CHO_HDL], " +
                    "CONSULTATION.[CONS_CHO_TG], " +
                    "CONSULTATION.[CONS_Fasting_Glucose], " +
                    "CONSULTATION.[CONS_HbA1c], " +
                    "CONSULTATION.[CONS_Resistance], " +
                    "CONSULTATION.[CONS_Cardio], " +
                    "CONSULTATION.[CONS_Brisk_Walk], " +
                    "CONSULTATION.[CONS_Light_Activity], " +
                    "CONSULTATION.[CONS_Fruit_Serves], " +
                    "CONSULTATION.[CONS_Fruit_Greater_2_Serves], " +
                    "CONSULTATION.[CONS_Vegetable_Serves], " +
                    "CONSULTATION.[CONS_Vegetable_Greater_5_Serves], " +
                    "CONSULTATION.[CONS_Commercial_Meals], " +
                    "CONSULTATION.[CONS_Sweets], " +
                    "CONSULTATION.[CONS_Soft_Drinks], " +
                    "CONSULTATION.[CONS_Skip_Main_Meals], " +
                    "CONSULTATION.[CONS_Keep_Track_Of_Food], " +
                    "CONSULTATION.[CONS_Limit_Portions], " +
                    "CONSULTATION.[CONS_Eat_When_Upset], " +
                    "CONSULTATION.[CONS_Eat_In_Front_Of_TV], " +
                    "CONSULTATION.[CONS_Choose_Healthier_Foods], " +
                    "CONSULTATION.[CONS_Notes] " +
                    "FROM CONSULTATION " +
                    "INNER JOIN [PATIENT] ON [CONSULTATION].[PAT_ID_Number] =  [PATIENT].[PAT_ID_Number] " +
                    "WHERE " +
                    columnName + " " + searchOperator + " " + searchName + " " +
                    "ORDER BY [PATIENT].[PAT_ID_Number];";
            }
            return query;
        }

        //Overloaded method searching all patients
        public static string CreateAllQuery(string columnName, string searchName, string searchOperator, string searchName2, string searchOperator2)
        {
            string query = "SELECT " +
                    "CONSULTATION.[CONS_ID_Number], " +
                    "CONSULTATION.[PAT_ID_Number], " +
                    "PATIENT.[PAT_First_Name], " +
                    "PATIENT.[PAT_Last_Name], " +
                    "CONSULTATION.[CONS_Consultant], " +
                    "CONSULTATION.[CONS_Time_Period], " +
                    "CONSULTATION.[CONS_Date], " +
                    "CONSULTATION.[CONS_Health_Rating], " +
                    "CONSULTATION.[CONS_Smoking_Status], " +
                    "CONSULTATION.[CONS_Alcohol_Days], " +
                    "CONSULTATION.[CONS_Alcohol_Drinks], " +
                    "CONSULTATION.[CONS_Height], " +
                    "CONSULTATION.[CONS_Weight], " +
                    "CONSULTATION.[CONS_BMI], " +
                    "CONSULTATION.[CONS_Waist], " +
                    "CONSULTATION.[CONS_SBP], " +
                    "CONSULTATION.[CONS_DBP], " +
                    "CONSULTATION.[CONS_Body_Fat_Percentage], " +
                    "CONSULTATION.[CONS_LBM], " +
                    "CONSULTATION.[CONS_Visceral_Fat_Rating], " +
                    "CONSULTATION.[CONS_CHO_TC], " +
                    "CONSULTATION.[CONS_CHO_LDL], " +
                    "CONSULTATION.[CONS_CHO_HDL], " +
                    "CONSULTATION.[CONS_CHO_TG], " +
                    "CONSULTATION.[CONS_Fasting_Glucose], " +
                    "CONSULTATION.[CONS_HbA1c], " +
                    "CONSULTATION.[CONS_Resistance], " +
                    "CONSULTATION.[CONS_Cardio], " +
                    "CONSULTATION.[CONS_Brisk_Walk], " +
                    "CONSULTATION.[CONS_Light_Activity], " +
                    "CONSULTATION.[CONS_Fruit_Serves], " +
                    "CONSULTATION.[CONS_Fruit_Greater_2_Serves], " +
                    "CONSULTATION.[CONS_Vegetable_Serves], " +
                    "CONSULTATION.[CONS_Vegetable_Greater_5_Serves], " +
                    "CONSULTATION.[CONS_Commercial_Meals], " +
                    "CONSULTATION.[CONS_Sweets], " +
                    "CONSULTATION.[CONS_Soft_Drinks], " +
                    "CONSULTATION.[CONS_Skip_Main_Meals], " +
                    "CONSULTATION.[CONS_Keep_Track_Of_Food], " +
                    "CONSULTATION.[CONS_Limit_Portions], " +
                    "CONSULTATION.[CONS_Eat_When_Upset], " +
                    "CONSULTATION.[CONS_Eat_In_Front_Of_TV], " +
                    "CONSULTATION.[CONS_Choose_Healthier_Foods], " +
                    "CONSULTATION.[CONS_Notes] " +
                    "FROM CONSULTATION " +
                    "INNER JOIN [PATIENT] ON [CONSULTATION].[PAT_ID_Number] =  [PATIENT].[PAT_ID_Number] " +
                    "WHERE " +
                columnName + " " + searchOperator + " '" + searchName + "' AND " +
                columnName + " " + searchOperator2 + " '" + searchName2 + "' " +
                "ORDER BY [PATIENT].[PAT_ID_Number];";
            return query;
        }

        //Viewing all records no where clauses
        public static void ViewAllRecords(ComboBox dropDown, DataGridView patientDGV)
        {
            string query = null;
            switch (dropDown.SelectedItem.ToString())
            {
                case "All Patients":
                    query = "SELECT " +
                    "CONSULTATION.[CONS_ID_Number], " +
                    "CONSULTATION.[PAT_ID_Number], " +
                    "PATIENT.[PAT_First_Name], " +
                    "PATIENT.[PAT_Last_Name], " +
                    "CONSULTATION.[CONS_Consultant], " +
                    "CONSULTATION.[CONS_Time_Period], " +
                    "CONSULTATION.[CONS_Date], " +
                    "CONSULTATION.[CONS_Health_Rating], " +
                    "CONSULTATION.[CONS_Smoking_Status], " +
                    "CONSULTATION.[CONS_Alcohol_Days], " +
                    "CONSULTATION.[CONS_Alcohol_Drinks], " +
                    "CONSULTATION.[CONS_Height], " +
                    "CONSULTATION.[CONS_Weight], " +
                    "CONSULTATION.[CONS_BMI], " +
                    "CONSULTATION.[CONS_Waist], " +
                    "CONSULTATION.[CONS_SBP], " +
                    "CONSULTATION.[CONS_DBP], " +
                    "CONSULTATION.[CONS_Body_Fat_Percentage], " +
                    "CONSULTATION.[CONS_LBM], " +
                    "CONSULTATION.[CONS_Visceral_Fat_Rating], " +
                    "CONSULTATION.[CONS_CHO_TC], " +
                    "CONSULTATION.[CONS_CHO_LDL], " +
                    "CONSULTATION.[CONS_CHO_HDL], " +
                    "CONSULTATION.[CONS_CHO_TG], " +
                    "CONSULTATION.[CONS_Fasting_Glucose], " +
                    "CONSULTATION.[CONS_HbA1c], " +
                    "CONSULTATION.[CONS_Resistance], " +
                    "CONSULTATION.[CONS_Cardio], " +
                    "CONSULTATION.[CONS_Brisk_Walk], " +
                    "CONSULTATION.[CONS_Light_Activity], " +
                    "CONSULTATION.[CONS_Fruit_Serves], " +
                    "CONSULTATION.[CONS_Fruit_Greater_2_Serves], " +
                    "CONSULTATION.[CONS_Vegetable_Serves], " +
                    "CONSULTATION.[CONS_Vegetable_Greater_5_Serves], " +
                    "CONSULTATION.[CONS_Commercial_Meals], " +
                    "CONSULTATION.[CONS_Sweets], " +
                    "CONSULTATION.[CONS_Soft_Drinks], " +
                    "CONSULTATION.[CONS_Skip_Main_Meals], " +
                    "CONSULTATION.[CONS_Keep_Track_Of_Food], " +
                    "CONSULTATION.[CONS_Limit_Portions], " +
                    "CONSULTATION.[CONS_Eat_When_Upset], " +
                    "CONSULTATION.[CONS_Eat_In_Front_Of_TV], " +
                    "CONSULTATION.[CONS_Choose_Healthier_Foods], " +
                    "CONSULTATION.[CONS_Notes] " +
                    "FROM CONSULTATION " +
                    "INNER JOIN [PATIENT] ON [CONSULTATION].[PAT_ID_Number] =  [PATIENT].[PAT_ID_Number] " +
                    "ORDER BY [PATIENT].[PAT_ID_Number];";
                    break;
                case "Current Patients":
                    query = CreateCurrentQuery("PAT_Current", "1", "=", false);
                    break;
                case "Non Current Patients":
                    query = CreateNonCurrentQuery("PAT_Current", "0", "=", false);
                    break;
            }
            ViewConsultations(patientDGV, query);
        }

        //Views Searched Records and Populates DataGridView
        public static void ViewSearchedRecords(DataGridView consDGV, TextBox searchText, ComboBox viewRecordsDropDown, ComboBox searchDropDown,
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
                MessageBox.Show("Please select a time period to search", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string searchBy = searchDropDown.SelectedItem.ToString();    //Holds String of Currently selected search item
            string query = null;                                    //Holds string for the sql query

            bool searchInteger = false;                                 //trigger if user is searching for an integer and not a string

            //searchBy is the variable for the chosen search option
            switch (searchBy)
            {
                case "Consultation ID":
                    //Filters through the 3 options: All Patients, Current and Non Current Patients
                    //and sets the sql query accordingly
                    switch (viewRecordsDropDown.SelectedItem.ToString())
                    {
                        case "All Patients":
                            query = CreateAllQuery("CONS_ID_Number", "%" + searchText.Text + "%", "LIKE", true);
                            break;
                        case "Current Patients":
                            query = CreateCurrentQuery("CONS_ID_Number", "%" + searchText.Text + "%", "LIKE", true);
                            break;
                        case "Non Current Patients":
                            query = CreateNonCurrentQuery("CONS_ID_Number", "%" + searchText.Text + "%", "LIKE", true);
                            break;
                    }
                    break;
                case "Patient ID":
                    searchInteger = true;
                    switch (viewRecordsDropDown.SelectedItem.ToString())
                    {
                        case "All Patients":
                            query = CreateAllQuery("[CONSULTATION].[PAT_ID_Number]", searchText.Text, "=", false);
                            break;
                        case "Current Patients":
                            query = CreateCurrentQuery("[CONSULTATION].[PAT_ID_Number]", searchText.Text, "=", false);
                            break;
                        case "Non Current Patients":
                            query = CreateNonCurrentQuery("[CONSULTATION].[PAT_ID_Number]", searchText.Text, "=", false);
                            break;
                    }
                    break;
                case "Patient First Name":
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
                case "Patient Last Name":
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
                case "Consultant Full Name":
                    switch (viewRecordsDropDown.SelectedItem.ToString())
                    {
                        case "All Patients":
                            query = CreateAllQuery("CONS_Consultant", "%" + searchText.Text + "%", "LIKE", true);
                            break;
                        case "Current Patients":
                            query = CreateCurrentQuery("CONS_Consultant", "%" + searchText.Text + "%", "LIKE", true);
                            break;
                        case "Non Current Patients":
                            query = CreateNonCurrentQuery("CONS_Consultant", "%" + searchText.Text + "%", "LIKE", true);
                            break;
                    }
                    break;
                case "Time Period":
                    switch (viewRecordsDropDown.SelectedItem.ToString())
                    {
                        case "All Patients":
                            query = CreateAllQuery("CONS_Time_Period", searchChoiceDropDown.SelectedItem.ToString(), "=", true);
                            break;
                        case "Current Patients":
                            query = CreateCurrentQuery("CONS_Time_Period", searchChoiceDropDown.SelectedItem.ToString(), "=", true);
                            break;
                        case "Non Current Patients":
                            query = CreateNonCurrentQuery("CONS_Time_Period", searchChoiceDropDown.SelectedItem.ToString(), "=", true);
                            break;
                    }
                    break;
                case "Consultation Date":
                    switch (viewRecordsDropDown.SelectedItem.ToString())
                    {
                        case "All Patients":
                            query = CreateAllQuery("CONS_Date", Global.getDateString(searchDate1), ">=",
                                Global.getDateString(searchDate2), "<=");
                            break;
                        case "Current Patients":
                            query = CreateCurrentQuery("CONS_Date", Global.getDateString(searchDate1), ">=",
                                Global.getDateString(searchDate2), "<=");
                            break;
                        case "Non Current Patients":
                            query = CreateNonCurrentQuery("CONS_Date", Global.getDateString(searchDate1), ">=",
                                Global.getDateString(searchDate2), "<=");
                            break;
                    }
                    break;
            }
            //Populates datagridview with query
            try
            {
                ViewConsultations(consDGV, query);
            }

            //Catches incorrect input for searching numbers only
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

        //Changes search options for searching by:
        public static void Show_HideSearchOptions(ComboBox searchSelectionDropDown, ComboBox searchChoiceDropDown, TextBox searchText, DateTimePicker searchDate1,
            DateTimePicker searchDate2, Label searchLabel1, Label searchLabel2)
        {
            //Sets less common controls to not show and common controls to show
            searchText.Visible = true;
            searchDate1.Visible = false;
            searchDate2.Visible = false;
            searchText.ReadOnly = false;
            searchText.Text = null;
            searchChoiceDropDown.Visible = false;
            searchLabel2.Visible = false;
            string item = searchSelectionDropDown.SelectedItem.ToString();
            //Sets control visibility based on selected item in combobox
            switch (item)
            {
                case "Consultation ID":
                    searchLabel1.Text = "ID Number:";
                    break;
                case "Patient ID":
                    searchLabel1.Text = "ID Number:";
                    break;
                case "Patient First Name":
                    searchLabel1.Text = "First Name:";
                    break;
                case "Patient Last Name":
                    searchLabel1.Text = "Last Name:";
                    break;
                case "Consultant Full Name":
                    searchLabel1.Text = "Full Name:";
                    break;
                case "Time Period":
                    searchLabel1.Text = "Time Period:";
                    searchText.Visible = false;
                    searchChoiceDropDown.Items.Clear();
                    searchChoiceDropDown.Visible = true;
                    string[] consSearchChoiceItems = new string[4] {"Initial", "3 Months", "6 Months", "12 Months"};
                    searchChoiceDropDown.Items.AddRange(consSearchChoiceItems);
                    break;
                case "Consultation Date":
                    searchLabel1.Text = "From Date:";
                    searchLabel2.Visible = true;
                    searchLabel2.Text = "To Date;";
                    searchText.Visible = false;
                    searchDate1.Visible = true;
                    searchDate2.Visible = true;
                    break;
            }
        }
    }
}
