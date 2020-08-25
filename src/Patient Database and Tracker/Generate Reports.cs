using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Finisar.SQLite;
using Excel = Microsoft.Office.Interop.Excel;

namespace Patient_Database_and_Tracker
{
    public partial class Generate_Reports : Form
    {
        int numberPatients;                         //Total Number of Patients
        Dictionary<string, int> uniquePractices;    //Holds all unique practices and how many patients reffered
        int[] totalConsultations;                   //Holds total number of consultations
        int[] multipleConsultationAttendance;       //Holds multiple consultation attendance
        int[] comparitiveDataCount = new int[3];                 //Holds count of different periods for comparing averages

        //Holds average differences over the 3 periods
        List<double> averageDifferencesInitial_3Months;
        List<double> averageDifferencesInitial_6Months;
        List<double> averageDifferencesInitial_12Months;

        public Generate_Reports()
        {
            InitializeComponent();

        }

        //Creates report on totals and on specific consultation data
        /*Overall Data:
         * Total Number of Patients Reffered
         * Breaks down how many patients from each Practice
         * Total number of consultations
         * Breaks down the number of initial, 3, 6 and 12 months
         * Breaks down how many patients have attended multiple consultations
         * Shows average differences of initial- 3 months, initial- 6 months and initial- 12 months
         */
        private void Create_Report_Click(object sender, EventArgs e)
        {
            //Patient and Practice data
            numberPatients = GetTotalPatients();                                            //Total Number of Patients
            uniquePractices = GetPatientsPerPractice();                                     //Holds all unique practices and how many patients reffered
            //Consultation Data
            totalConsultations = GetTotalConsultations();                                   //Holds total number of consultations
            multipleConsultationAttendance = GetMultipleConsultationAttendance();           //Holds multiple consultation attendance
            //Holds average differences over the 3 different periods
            List<double>[] averageDifferences = GenerateConsultComparrisions();
            averageDifferencesInitial_3Months = averageDifferences[0];
            averageDifferencesInitial_6Months = averageDifferences[1];
            averageDifferencesInitial_12Months = averageDifferences[2];
            GenerateExcelData();                                                            //After all data is collected the excel spreadsheet is created
        }

        //Returns total number of patients
        private int GetTotalPatients()
        {
            int numberPatients = 0;      //Will hold Number of Patients
            string query = "SELECT COUNT(*)" +
                "FROM PATIENT WHERE " +
                "PAT_Date_Reffered >= '" + Global.getDateString(Date_1) + "' AND " +
                "PAT_Date_Reffered <= '" + Global.getDateString(Date_2) + "';";

            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {
                try
                {
                    Global.connection.Open();
                    //Get number of patients over period
                    numberPatients = Convert.ToInt32(cmd.ExecuteScalar());
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                return numberPatients;
            }
        }

        //Returns a dictionary holding all practices and number of patients
        private Dictionary<string, int> GetPatientsPerPractice()
        {
            Dictionary<string, int> uniquePractices = new Dictionary<string, int>();        //Holds all unique practices and how many patients reffered
            string query = "SELECT " +
                "[REFFERING GP].PRAC_Name " +
                "FROM [REFFERING GP] " +
                "INNER JOIN PATIENT ON [REFFERING GP].[REFGP_ID_Number] = PATIENT.[REFGP_ID_Number]" +
                "WHERE " +
                "PAT_Date_Reffered >= '" + Global.getDateString(Date_1) + "' AND " +
                "PAT_Date_Reffered <= '" + Global.getDateString(Date_2) + "' " +
                "ORDER BY [REFFERING GP].PRAC_Name;";

            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {
                try
                {
                    Global.connection.Open();
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Boolean uniqueFound = true;                 //Trigger if a unique name is found
                        string pracName = reader.GetString(0);      //Pratice name in database

                        //Iterates through all entries already in dictionary
                        foreach (KeyValuePair<string, int> entry in uniquePractices)
                        {
                            //If matching name set trigger
                            if (pracName == entry.Key)
                            {
                                uniqueFound = false;
                            }
                        }
                        //If a unique name is found then adds new entry to dictionary
                        if (uniqueFound == true)
                        {
                            uniquePractices.Add(pracName, 1);
                        }
                        //If not then adds adds 1 to the corresponding practice
                        else
                        {
                            uniquePractices[pracName] = uniquePractices[pracName] + 1;
                        }
                    }

                    reader.Close();
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                return uniquePractices;
            }
        }

        //Returns an array holding all total number of consultations
        private int[] GetTotalConsultations()
        {
            int[] consultations = new int[5];

            //Total number of consultations
            string query1 = "SELECT COUNT(*) " +
                "FROM CONSULTATION WHERE " +
                "CONS_Date >= '" + Global.getDateString(Date_1) + "' AND " +
                "CONS_Date <= '" + Global.getDateString(Date_2) + "' AND " +
                "CONS_Time_Period = 'Initial';";

            string query2 = "SELECT COUNT(*) " +
                "FROM CONSULTATION WHERE " +
                "CONS_Date >= '" + Global.getDateString(Date_1) + "' AND " +
                "CONS_Date <= '" + Global.getDateString(Date_2) + "' AND " +
                "CONS_Time_Period = '3 Months';";

            string query3 = "SELECT COUNT(*) " +
                "FROM CONSULTATION WHERE " +
                "CONS_Date >= '" + Global.getDateString(Date_1) + "' AND " +
                "CONS_Date <= '" + Global.getDateString(Date_2) + "' AND " +
                "CONS_Time_Period = '6 Months';";

            string query4 = "SELECT COUNT(*) " +
                "FROM CONSULTATION WHERE " +
                "CONS_Date >= '" + Global.getDateString(Date_1) + "' AND " +
                "CONS_Date <= '" + Global.getDateString(Date_2) + "' AND " +
                "CONS_Time_Period = '12 Months';";

            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query1, Global.connection))
            {
                try
                {
                    Global.connection.Open();

                    //Total initial consultations
                    consultations[0] = Convert.ToInt32(cmd.ExecuteScalar());

                    //Total 3 month consulations
                    cmd.CommandText = query2;
                    consultations[1] = Convert.ToInt32(cmd.ExecuteScalar());

                    //Total 6 Month consultations
                    cmd.CommandText = query3;
                    consultations[2] = Convert.ToInt32(cmd.ExecuteScalar());

                    //Total 12 Month consultations
                    cmd.CommandText = query4;
                    consultations[3] = Convert.ToInt32(cmd.ExecuteScalar());

                    //Overall consultations
                    consultations[4] = consultations[0] + consultations[1] + consultations[2] + consultations[3];
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                return consultations;
            }
        }

        //Returns total number of patients who attend more than one time period for consultations
        private int[] GetMultipleConsultationAttendance()
        {
            //Will be return variable
            multipleConsultationAttendance = new int[3];

            //Finds all patients and their time periods
            string query = "SELECT PAT_ID_Number, " +
                "CONS_Time_Period " +
                "FROM CONSULTATION WHERE " +
                "CONS_Date >= '" + Global.getDateString(Date_1) + "' AND " +
                "CONS_Date <= '" + Global.getDateString(Date_2) + "' " +
                "ORDER BY CONS_Time_Period;";

            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {
                SQLiteDataReader reader;            //Used to iterate through database
                try
                {
                    Global.connection.Open();

                    //Holds number of patients that attended each time period
                    List<int> initialsList = new List<int>();
                    List<int> threeMonthsList = new List<int>();
                    List<int> sixMonthsList = new List<int>();
                    List<int> twelveMonthsList = new List<int>();

                    //Holds patients who attended multiple consultations
                    List<int> i_3_MonthsList = new List<int>();
                    List<int> i_3_6_MonthsList = new List<int>();
                    List<int> i_3_6_12_MonthsList = new List<int>();

                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        //Will hold all data for patients and time period
                        int patientID = reader.GetInt32(0);
                        string timePeriod = reader.GetString(1);

                        //Adds patients who attended each time period
                        switch (timePeriod)
                        {
                            case "Initial":
                                initialsList.Add(patientID);
                                break;
                            case "3 Months":
                                threeMonthsList.Add(patientID);
                                break;
                            case "6 Months":
                                sixMonthsList.Add(patientID);
                                break;
                            case "12 Months":
                                twelveMonthsList.Add(patientID);
                                break;
                        }
                    }

                    //Temp list to store matching patients
                    List<int> tempList = new List<int>();
                    //Finds all patients who did initial and 3 Months
                    foreach (int initialEntry in initialsList)
                    {
                        foreach (int threeEntry in threeMonthsList)
                        {
                            if (initialEntry == threeEntry)
                            {
                                tempList.Add(threeEntry);
                            }
                        }
                    }
                    //Assigns three monthlist to the templist
                    i_3_MonthsList.AddRange(tempList);
                    tempList.Clear();

                    //Finds all patients who did initial, 3 Months and 6 Months
                    foreach (int threeEntry in i_3_MonthsList)
                    {
                        foreach (int sixEntry in sixMonthsList)
                        {
                            if (threeEntry == sixEntry)
                            {
                                tempList.Add(sixEntry);
                            }
                        }
                    }
                    //Assigns six monthlist to the templist
                    i_3_6_MonthsList.AddRange(tempList);
                    tempList.Clear();

                    //Finds all patients who did initial, 3 Months, 6 and 12 Months
                    foreach (int sixEntry in i_3_6_MonthsList)
                    {
                        foreach (int twelveEntry in twelveMonthsList)
                        {
                            if (sixEntry == twelveEntry)
                            {
                                tempList.Add(twelveEntry);
                            }
                        }
                    }
                    //Assigns 12 monthlist to the templist
                    i_3_6_12_MonthsList.AddRange(tempList);
                    tempList.Clear();

                    Global.connection.Close();

                    //Assigns totals for reporting multiple consultation attendance
                    multipleConsultationAttendance[0] = i_3_MonthsList.Count;
                    multipleConsultationAttendance[1] = i_3_6_MonthsList.Count;
                    multipleConsultationAttendance[2] = i_3_6_12_MonthsList.Count;

                    List<int> initial_Six_MonthsList = new List<int>();
                    List<int> initial_Twelve_MonthsList = new List<int>();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                return multipleConsultationAttendance;
            }
        }

        //Used with GenerateConsultComparrisions() to find list of patient ids who fit in to more than 1 time period
        private List<int>[] GeneratePatientIDsForConsultComparrisions()
        {
            //Finds number of patients who attended more multiple consultations
            string query = "SELECT PAT_ID_Number, " +
                "CONS_Time_Period " +
                "FROM CONSULTATION WHERE " +
                "CONS_Date >= '" + Global.getDateString(Date_1) + "' AND " +
                "CONS_Date <= '" + Global.getDateString(Date_2) + "' " +
                "ORDER BY CONS_Time_Period;";


            //Holds number of patients that attended each time period
            List<int> initialsList = new List<int>();
            List<int> threeMonthsList = new List<int>();
            List<int> sixMonthsList = new List<int>();
            List<int> twelveMonthsList = new List<int>();

            //Will hold list of patients for comparative data
            List<int> i_3_MonthsList = new List<int>();
            List<int> i_6_MonthsList = new List<int>();
            List<int> i_12_MonthsList = new List<int>();
            List<int>[] patientIds = new List<int>[3];

            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {
                SQLiteDataReader reader;            //Used to iterate through database
                try
                {
                    Global.connection.Open();

                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        //Will hold all data for patients and time period
                        int patientID = reader.GetInt32(0);
                        string timePeriod = reader.GetString(1);

                        //Adds patients who attended each time period
                        switch (timePeriod)
                        {
                            case "Initial":
                                initialsList.Add(patientID);
                                break;
                            case "3 Months":
                                threeMonthsList.Add(patientID);
                                break;
                            case "6 Months":
                                sixMonthsList.Add(patientID);
                                break;
                            case "12 Months":
                                twelveMonthsList.Add(patientID);
                                break;
                        }
                    }

                    Global.connection.Close();

                    List<int> tempList = new List<int>();

                    //Wanting to get all patients who have attended initial and 3 month only
                    foreach (int initialEntry in initialsList)
                    {
                        foreach (int threeEntry in threeMonthsList)
                        {
                            if (initialEntry == threeEntry)
                            {
                                tempList.Add(threeEntry);
                            }
                        }
                    }

                    i_3_MonthsList.AddRange(tempList);
                    tempList.Clear();

                    //Wanting to get all patients who have attended initial and six month only
                    foreach (int initialEntry in initialsList)
                    {
                        foreach (int sixEntry in sixMonthsList)
                        {
                            if (initialEntry == sixEntry)
                            {
                                tempList.Add(sixEntry);
                            }
                        }
                    }

                    i_6_MonthsList.AddRange(tempList);
                    tempList.Clear();

                    //Wanting to get all patients who have attended initial and 12 month only
                    foreach (int initialEntry in initialsList)
                    {
                        foreach (int twelveEntry in twelveMonthsList)
                        {
                            if (initialEntry == twelveEntry)
                            {
                                tempList.Add(twelveEntry);
                            }
                        }
                    }

                    i_12_MonthsList.AddRange(tempList);
                    tempList.Clear();
                    patientIds[0] = i_3_MonthsList;
                    patientIds[1] = i_6_MonthsList;
                    patientIds[2] = i_12_MonthsList;

                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                return patientIds;
            }
        }

        //This method compares the average differences for consult data for different periods
        //Initial - 3 Months
        //Initial - 6 Months
        //Initial - 12 Months
        //Using 3 sql queries the data is extracted, assigned to arrays and then the difference between
        //the 2 periods are divided by the total number of patients in the periods.  This data will be displayed on
        //the final excel spreadsheet
        private List<double>[] GenerateConsultComparrisions()
        {
            //Will hold all petient ids which attend:
            //patientIDsConsultData[0] = initial & 3 Months,
            //patientIDsConsultData[1] = initial & 6 Months,
            //patientIDsConsultData[2] = initial & 12 Months,
            List<int>[] patientIDsConsultData = GeneratePatientIDsForConsultComparrisions();

            //Strings used to hold all patient ids seperated by commas to be used in sql statement
            string i_3_MonthsListString = null;
            string i_6_MonthsListString = null;
            string i_12_MonthsListString = null;

            //Holds number of patients in each period
            comparitiveDataCount[0] = patientIDsConsultData[0].Count;
            comparitiveDataCount[1] = patientIDsConsultData[1].Count;
            comparitiveDataCount[2] = patientIDsConsultData[2].Count;

            //Holds a list of all the average differences for each period
            List<double> initial_3Month_Difference = new List<double>();
            List<double> initial_6Month_Difference = new List<double>();
            List<double> initial_12Month_Difference = new List<double>();

            //Will be the return of this method
            List<double>[] averageDifferences = new List<double>[3];

            //Patient IDs will be used in an sql IN() statement.  The syntax is
            //(1, 2, 3, 4, etc).  Need to place commas in between each number in lists
            bool firstEntry = true;
            foreach (int number in patientIDsConsultData[0])
            {
                if (firstEntry)
                {
                    i_3_MonthsListString = number.ToString();
                    firstEntry = false;
                }
                else
                {
                    i_3_MonthsListString = i_3_MonthsListString + ", " + number;
                }
            }

            firstEntry = true;
            foreach (int number in patientIDsConsultData[1])
            {
                if (firstEntry)
                {
                    i_6_MonthsListString = number.ToString();
                    firstEntry = false;
                }
                else
                {
                    i_6_MonthsListString = i_6_MonthsListString + ", " + number;
                }
            }

            firstEntry = true;
            foreach (int number in patientIDsConsultData[2])
            {
                if (firstEntry)
                {
                    i_12_MonthsListString = number.ToString();
                    firstEntry = false;
                }
                else
                {
                    i_12_MonthsListString = i_12_MonthsListString + ", " + number;
                }
            }

            //constant string will be used in the 3 queries
            string basequery = "SELECT [PAT_ID_Number], " +
                "[CONS_Time_Period], " +
                "[CONS_Health_Rating], " +
                "[CONS_Alcohol_Days], " +
                "[CONS_Alcohol_Drinks], " +
                "[CONS_Weight], " +
                "[CONS_BMI], " +
                "[CONS_Waist], " +
                "[CONS_SBP], " +
                "[CONS_DBP], " +
                "[CONS_LBM], " +
                "[CONS_CHO_TC], " +
                "[CONS_CHO_LDL], " +
                "[CONS_CHO_HDL], " +
                "[CONS_CHO_TG], " +
                "[CONS_Fasting_Glucose], " +
                "[CONS_HbA1c], " +
                "[CONS_Resistance], " +
                "[CONS_Cardio], " +
                "[CONS_Brisk_Walk], " +
                "[CONS_Light_Activity], " +
                "[CONS_Fruit_Serves], " +
                "[CONS_Fruit_Greater_2_Serves], " +
                "[CONS_Vegetable_Serves], " +
                "[CONS_Vegetable_Greater_5_Serves], " +
                "[CONS_Commercial_Meals], " +
                "[CONS_Sweets], " +
                "[CONS_Soft_Drinks], " +
                "[CONS_Skip_Main_Meals], " +
                "[CONS_Keep_Track_Of_Food], " +
                "[CONS_Limit_Portions], " +
                "[CONS_Eat_When_Upset], " +
                "[CONS_Eat_In_Front_Of_TV], " +
                "[CONS_Choose_Healthier_Foods] " +
                "FROM CONSULTATION WHERE ";

            //These queries will be used to determine the average differences in the 3 different periods
            string query1 = basequery +
                "[PAT_ID_Number] IN (" + i_3_MonthsListString + ") AND " +
                "[CONS_Time_Period] IN ('Initial', '3 Months');";

            string query2 = basequery +
                "[PAT_ID_Number] IN (" + i_6_MonthsListString + ") AND " +
                "[CONS_Time_Period] IN ('Initial', '6 Months');";

            string query3 = basequery +
                "[PAT_ID_Number] IN (" + i_12_MonthsListString + ") AND " +
                "[CONS_Time_Period] IN ('Initial', '12 Months');";

            //Holds all the data extracted for the different periods. Will later be assined to an array
            double healthRatingInitial = 0, healthRating3Months = 0, healthRating6Months = 0, healthRating12Months = 0;
            double alcoholDaysInitial = 0, alcoholDays3Months = 0, alcoholDays6Months = 0, alcoholDays12Months = 0;
            double alcoholDrinksInitial = 0, alcoholDrinks3Months = 0, alcoholDrinks6Months = 0, alcoholDrinks12Months = 0;
            double weightInitial = 0, weight3Months = 0, weight6Months = 0, weight12Months = 0;
            double bmiInitial = 0, bmi3Months = 0, bmi6Months = 0, bmi12Months = 0;
            double waistInitial = 0, waist3Months = 0, waist6Months = 0, waist12Months = 0;
            double sbpInitial = 0, sbp3Months = 0, sbp6Months = 0, sbp12Months = 0;
            double dbpInitial = 0, dbp3Months = 0, dbp6Months = 0, dbp12Months = 0;
            double lbmInitial = 0, lbm3Months = 0, lbm6Months = 0, lbm12Months = 0;
            double CHO_TCInitial = 0, CHO_TC3Months = 0, CHO_TC6Months = 0, CHO_TC12Months = 0;
            double CHO_LDLInitial = 0, CHO_LDL3Months = 0, CHO_LDL6Months = 0, CHO_LDL12Months = 0;
            double CHO_HDLInitial = 0, CHO_HDL3Months = 0, CHO_HDL6Months = 0, CHO_HDL12Months = 0;
            double CHO_TGInitial = 0, CHO_TG3Months = 0, CHO_TG6Months = 0, CHO_TG12Months = 0;
            double FastingGlucoseInitial = 0, FastingGlucose3Months = 0, FastingGlucose6Months = 0, FastingGlucose12Months = 0;
            double HbA1cInitial = 0, HbA1c3Months = 0, HbA1c6Months = 0, HbA1c12Months = 0;
            double ResistanceInitial = 0, Resistance3Months = 0, Resistance6Months = 0, Resistance12Months = 0;
            double CardioInitial = 0, Cardio3Months = 0, Cardio6Months = 0, Cardio12Months = 0;
            double BriskWalkInitial = 0, BriskWalk3Months = 0, BriskWalk6Months = 0, BriskWalk12Months = 0;
            double LightActivityInitial = 0, LightActivity3Months = 0, LightActivity6Months = 0, LightActivity12Months = 0;
            double FruitServesInitial = 0, FruitServes3Months = 0, FruitServes6Months = 0, FruitServes12Months = 0;
            double FruitGreater2ServesInitial = 0, FruitGreater2Serves3Months = 0, FruitGreater2Serves6Months = 0, FruitGreater2Serves12Months = 0;
            double VegServesInitial = 0, VegServes3Months = 0, VegServes6Months = 0, VegServes12Months = 0;
            double VegGreater2ServesInitial = 0, VegGreater2Serves3Months = 0, VegGreater2Serves6Months = 0, VegGreater2Serves12Months = 0;
            double CommercialMealsInitial = 0, CommercialMeals3Months = 0, CommercialMeals6Months = 0, CommercialMeals12Months = 0;
            double SweetsInitial = 0, Sweets3Months = 0, Sweets6Months = 0, Sweets12Months = 0;
            double SoftDrinksInitial = 0, SoftDrinks3Months = 0, SoftDrinks6Months = 0, SoftDrinks12Months = 0;
            double SkipMainMealsInitial = 0, SkipMainMeals3Months = 0, SkipMainMeals6Months = 0, SkipMainMeals12Months = 0;
            double KeepTrackFoodInitial = 0, KeepTrackFood3Months = 0, KeepTrackFood6Months = 0, KeepTrackFood12Months = 0;
            double LimitPortionsInitial = 0, LimitPortions3Months = 0, LimitPortions6Months = 0, LimitPortions12Months = 0;
            double EatUpsetInitial = 0, EatUpset3Months = 0, EatUpset6Months = 0, EatUpset12Months = 0;
            double EatTVInitial = 0, EatTV3Months = 0, EatTV6Months = 0, EatTV12Months = 0;
            double ChooseHealthierInitial = 0, ChooseHealthier3Months = 0, ChooseHealthier6Months = 0, ChooseHealthier12Months = 0;



            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query1, Global.connection))
            {
                SQLiteDataReader reader;            //Used to iterate through database
                try
                {
                    Global.connection.Open();

                    //Following code extracts and compares initial and 3 months
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        //Will hold all data for patients and time period
                        int patientID = reader.GetInt32(0);
                        string timePeriod = reader.GetString(1);

                        //Switch statement for initial or 3 Months
                        switch (timePeriod)
                        {
                            case "Initial":
                                //As health option is a string it needs to be converted to a number for comparison.
                                //1 being poor and 4 being very good
                                string healthOption = reader.GetString(2);
                                switch (healthOption)
                                {
                                    case (null):
                                        healthRatingInitial = healthRatingInitial + 1;
                                        break;
                                    case ("Poor"):
                                        healthRatingInitial = healthRatingInitial + 1;
                                        break;
                                    case ("Fair"):
                                        healthRatingInitial = healthRatingInitial + 2;
                                        break;
                                    case ("Good"):
                                        healthRatingInitial = healthRatingInitial + 3;
                                        break;
                                    case ("Very Good"):
                                        healthRatingInitial = healthRatingInitial + 4;
                                        break;
                                }

                                //While looping through getting a accumulative total for each bit of data
                                alcoholDaysInitial = alcoholDaysInitial + reader.GetDouble(3);
                                alcoholDrinksInitial = alcoholDrinksInitial + reader.GetDouble(4);
                                weightInitial = weightInitial + reader.GetDouble(5);
                                bmiInitial = bmiInitial + reader.GetDouble(6);
                                waistInitial = waistInitial + reader.GetDouble(7);
                                sbpInitial = sbpInitial + reader.GetDouble(8);
                                dbpInitial = dbpInitial + reader.GetDouble(9);
                                lbmInitial = lbmInitial + reader.GetDouble(10);
                                CHO_TCInitial = CHO_TCInitial + reader.GetDouble(11);
                                CHO_LDLInitial = CHO_LDLInitial + reader.GetDouble(12);
                                CHO_HDLInitial = CHO_HDLInitial + reader.GetDouble(13);
                                CHO_TGInitial = CHO_TGInitial + reader.GetDouble(14);
                                FastingGlucoseInitial = FastingGlucoseInitial + reader.GetDouble(15);
                                HbA1cInitial = HbA1cInitial + reader.GetDouble(16);
                                ResistanceInitial = ResistanceInitial + reader.GetDouble(17);
                                CardioInitial = CardioInitial + reader.GetDouble(18);
                                BriskWalkInitial = BriskWalkInitial + reader.GetDouble(19);
                                LightActivityInitial = LightActivityInitial + reader.GetDouble(20);
                                FruitServesInitial = FruitServesInitial + reader.GetDouble(21);
                                FruitGreater2ServesInitial = FruitGreater2ServesInitial + reader.GetDouble(22);
                                VegServesInitial = VegServesInitial + reader.GetDouble(23);
                                VegGreater2ServesInitial = VegGreater2ServesInitial + reader.GetDouble(24);
                                CommercialMealsInitial = CommercialMealsInitial + reader.GetDouble(25);
                                SweetsInitial = SweetsInitial + reader.GetDouble(26);
                                SoftDrinksInitial = SoftDrinksInitial + reader.GetDouble(27);
                                SkipMainMealsInitial = SkipMainMealsInitial + reader.GetDouble(28);
                                KeepTrackFoodInitial = KeepTrackFoodInitial + reader.GetDouble(29);
                                LimitPortionsInitial = LimitPortionsInitial + reader.GetDouble(30);
                                EatUpsetInitial = EatUpsetInitial + reader.GetDouble(31);
                                EatTVInitial = EatTVInitial + reader.GetDouble(32);
                                ChooseHealthierInitial = ChooseHealthierInitial + reader.GetDouble(33);

                                break;
                            //Repeats the above process for 3 Months
                            case "3 Months":
                                //As health option is a string it needs to be converted to a number for comparison.
                                //1 being porr and 4 being very good
                                healthOption = reader.GetString(2);
                                switch (healthOption)
                                {
                                    case (null):
                                        healthRating3Months = healthRating3Months + 1;
                                        break;
                                    case ("Poor"):
                                        healthRating3Months = healthRating3Months + 1;
                                        break;
                                    case ("Fair"):
                                        healthRating3Months = healthRating3Months + 2;
                                        break;
                                    case ("Good"):
                                        healthRating3Months = healthRating3Months + 3;
                                        break;
                                    case ("Very Good"):
                                        healthRating3Months = healthRating3Months + 4;
                                        break;
                                }
                                alcoholDays3Months = alcoholDays3Months + reader.GetDouble(3);
                                alcoholDrinks3Months = alcoholDrinks3Months + reader.GetDouble(4);
                                weight3Months = weight3Months + reader.GetDouble(5);
                                bmi3Months = bmi3Months + reader.GetDouble(6);
                                waist3Months = waist3Months + reader.GetDouble(7);
                                sbp3Months = sbp3Months + reader.GetDouble(8);
                                dbp3Months = dbp3Months + reader.GetDouble(9);
                                lbm3Months = lbm3Months + reader.GetDouble(10);
                                CHO_TC3Months = CHO_TC3Months + reader.GetDouble(11);
                                CHO_LDL3Months = CHO_LDL3Months + reader.GetDouble(12);
                                CHO_HDL3Months = CHO_HDL3Months + reader.GetDouble(13);
                                CHO_TG3Months = CHO_TG3Months + reader.GetDouble(14);
                                FastingGlucose3Months = FastingGlucose3Months + reader.GetDouble(15);
                                HbA1c3Months = HbA1c3Months + reader.GetDouble(16);
                                Resistance3Months = Resistance3Months + reader.GetDouble(17);
                                Cardio3Months = Cardio3Months + reader.GetDouble(18);
                                BriskWalk3Months = BriskWalk3Months + reader.GetDouble(19);
                                LightActivity3Months = LightActivity3Months + reader.GetDouble(20);
                                FruitServes3Months = FruitServes3Months + reader.GetDouble(21);
                                FruitGreater2Serves3Months = FruitGreater2Serves3Months + reader.GetDouble(22);
                                VegServes3Months = VegServes3Months + reader.GetDouble(23);
                                VegGreater2Serves3Months = VegGreater2Serves3Months + reader.GetDouble(24);
                                CommercialMeals3Months = CommercialMeals3Months + reader.GetDouble(25);
                                Sweets3Months = Sweets3Months + reader.GetDouble(26);
                                SoftDrinks3Months = SoftDrinks3Months + reader.GetDouble(27);
                                SkipMainMeals3Months = SkipMainMeals3Months + reader.GetDouble(28);
                                KeepTrackFood3Months = KeepTrackFood3Months + reader.GetDouble(29);
                                LimitPortions3Months = LimitPortions3Months + reader.GetDouble(30);
                                EatUpset3Months = EatUpset3Months + reader.GetDouble(31);
                                EatTV3Months = EatTV3Months + reader.GetDouble(32);
                                ChooseHealthier3Months = ChooseHealthier3Months + reader.GetDouble(33);
                                break;
                        }
                    }

                    //Once reader has been used it needs to be disposed to read again
                    reader.Dispose();

                    //Assigns the accumulative data to arrays for comparing in a loop
                    double[] initialArray = {healthRatingInitial, alcoholDaysInitial, alcoholDrinksInitial, weightInitial, bmiInitial,
                        waistInitial, sbpInitial, dbpInitial, lbmInitial, CHO_TCInitial, CHO_LDLInitial, CHO_HDLInitial, CHO_TGInitial,
                        FastingGlucoseInitial, HbA1cInitial, ResistanceInitial, CardioInitial, BriskWalkInitial, LightActivityInitial, FruitServesInitial,
                        FruitGreater2ServesInitial, VegServesInitial, VegGreater2ServesInitial, CommercialMealsInitial, SweetsInitial, SoftDrinksInitial,
                        SkipMainMealsInitial, KeepTrackFoodInitial, LimitPortionsInitial, EatUpsetInitial, EatTVInitial, ChooseHealthierInitial};

                    double[] threeMonthArray = {healthRating3Months, alcoholDays3Months, alcoholDrinks3Months, weight3Months, bmi3Months,
                        waist3Months, sbp3Months, dbp3Months, lbm3Months, CHO_TC3Months, CHO_LDL3Months, CHO_HDL3Months, CHO_TG3Months,
                        FastingGlucose3Months, HbA1c3Months, Resistance3Months, Cardio3Months, BriskWalk3Months, LightActivity3Months, FruitServes3Months,
                        FruitGreater2Serves3Months, VegServes3Months, VegGreater2Serves3Months, CommercialMeals3Months, Sweets3Months, SoftDrinks3Months,
                        SkipMainMeals3Months, KeepTrackFood3Months, LimitPortions3Months, EatUpset3Months, EatTV3Months, ChooseHealthier3Months};

                    //foreach matching data calculates difference then divides by count.
                    //The averages are assigned to a list
                    for (int x = 0; x < initialArray.Count(); ++x)
                    {
                        double difference = (threeMonthArray[x] - initialArray[x]) / comparitiveDataCount[0];
                        initial_3Month_Difference.Add(difference);
                    }

                    //Following code is duplication to compare initial and 6 months
                    cmd.CommandText = query2;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        //Will hold all data for patients and time period

                        int patientID = reader.GetInt32(0);
                        string timePeriod = reader.GetString(1);

                        //Adds patients who attended each time period
                        switch (timePeriod)
                        {
                            case "Initial":
                                //As health option is a string it needs to be converted to a number for comparison.
                                //1 being porr and 4 being very good
                                string healthOption = reader.GetString(2);
                                switch (healthOption)
                                {
                                    case (null):
                                        healthRatingInitial = healthRatingInitial + 1;
                                        break;
                                    case ("Poor"):
                                        healthRatingInitial = healthRatingInitial + 1;
                                        break;
                                    case ("Fair"):
                                        healthRatingInitial = healthRatingInitial + 2;
                                        break;
                                    case ("Good"):
                                        healthRatingInitial = healthRatingInitial + 3;
                                        break;
                                    case ("Very Good"):
                                        healthRatingInitial = healthRatingInitial + 4;
                                        break;
                                }
                                alcoholDaysInitial = alcoholDaysInitial + reader.GetDouble(3);
                                alcoholDrinksInitial = alcoholDrinksInitial + reader.GetDouble(4);
                                weightInitial = weightInitial + reader.GetDouble(5);
                                bmiInitial = bmiInitial + reader.GetDouble(6);
                                waistInitial = waistInitial + reader.GetDouble(7);
                                sbpInitial = sbpInitial + reader.GetDouble(8);
                                dbpInitial = dbpInitial + reader.GetDouble(9);
                                lbmInitial = lbmInitial + reader.GetDouble(10);
                                CHO_TCInitial = CHO_TCInitial + reader.GetDouble(11);
                                CHO_LDLInitial = CHO_LDLInitial + reader.GetDouble(12);
                                CHO_HDLInitial = CHO_HDLInitial + reader.GetDouble(13);
                                CHO_TGInitial = CHO_TGInitial + reader.GetDouble(14);
                                FastingGlucoseInitial = FastingGlucoseInitial + reader.GetDouble(15);
                                HbA1cInitial = HbA1cInitial + reader.GetDouble(16);
                                ResistanceInitial = ResistanceInitial + reader.GetDouble(17);
                                CardioInitial = CardioInitial + reader.GetDouble(18);
                                BriskWalkInitial = BriskWalkInitial + reader.GetDouble(19);
                                LightActivityInitial = LightActivityInitial + reader.GetDouble(20);
                                FruitServesInitial = FruitServesInitial + reader.GetDouble(21);
                                FruitGreater2ServesInitial = FruitGreater2ServesInitial + reader.GetDouble(22);
                                VegServesInitial = VegServesInitial + reader.GetDouble(23);
                                VegGreater2ServesInitial = VegGreater2ServesInitial + reader.GetDouble(24);
                                CommercialMealsInitial = CommercialMealsInitial + reader.GetDouble(25);
                                SweetsInitial = SweetsInitial + reader.GetDouble(26);
                                SoftDrinksInitial = SoftDrinksInitial + reader.GetDouble(27);
                                SkipMainMealsInitial = SkipMainMealsInitial + reader.GetDouble(28);
                                KeepTrackFoodInitial = KeepTrackFoodInitial + reader.GetDouble(29);
                                LimitPortionsInitial = LimitPortionsInitial + reader.GetDouble(30);
                                EatUpsetInitial = EatUpsetInitial + reader.GetDouble(31);
                                EatTVInitial = EatTVInitial + reader.GetDouble(32);
                                ChooseHealthierInitial = ChooseHealthierInitial + reader.GetDouble(33);

                                break;
                            case "6 Months":
                                //As health option is a string it needs to be converted to a number for comparison.
                                //1 being porr and 4 being very good
                                healthOption = reader.GetString(2);
                                switch (healthOption)
                                {
                                    case (null):
                                        healthRating6Months = healthRating6Months + 1;
                                        break;
                                    case ("Poor"):
                                        healthRating6Months = healthRating6Months + 1;
                                        break;
                                    case ("Fair"):
                                        healthRating6Months = healthRating6Months + 2;
                                        break;
                                    case ("Good"):
                                        healthRating6Months = healthRating6Months + 3;
                                        break;
                                    case ("Very Good"):
                                        healthRating6Months = healthRating6Months + 4;
                                        break;
                                }
                                alcoholDays6Months = alcoholDays6Months + reader.GetDouble(3);
                                alcoholDrinks6Months = alcoholDrinks6Months + reader.GetDouble(4);
                                weight6Months = weight6Months + reader.GetDouble(5);
                                bmi6Months = bmi6Months + reader.GetDouble(6);
                                waist6Months = waist6Months + reader.GetDouble(7);
                                sbp6Months = sbp6Months + reader.GetDouble(8);
                                dbp6Months = dbp6Months + reader.GetDouble(9);
                                lbm6Months = lbm6Months + reader.GetDouble(10);
                                CHO_TC6Months = CHO_TC6Months + reader.GetDouble(11);
                                CHO_LDL6Months = CHO_LDL6Months + reader.GetDouble(12);
                                CHO_HDL6Months = CHO_HDL6Months + reader.GetDouble(13);
                                CHO_TG6Months = CHO_TG6Months + reader.GetDouble(14);
                                FastingGlucose6Months = FastingGlucose6Months + reader.GetDouble(15);
                                HbA1c6Months = HbA1c6Months + reader.GetDouble(16);
                                Resistance6Months = Resistance6Months + reader.GetDouble(17);
                                Cardio6Months = Cardio6Months + reader.GetDouble(18);
                                BriskWalk6Months = BriskWalk6Months + reader.GetDouble(19);
                                LightActivity6Months = LightActivity6Months + reader.GetDouble(20);
                                FruitServes6Months = FruitServes6Months + reader.GetDouble(21);
                                FruitGreater2Serves6Months = FruitGreater2Serves6Months + reader.GetDouble(22);
                                VegServes6Months = VegServes6Months + reader.GetDouble(23);
                                VegGreater2Serves6Months = VegGreater2Serves6Months + reader.GetDouble(24);
                                CommercialMeals6Months = CommercialMeals6Months + reader.GetDouble(25);
                                Sweets6Months = Sweets6Months + reader.GetDouble(26);
                                SoftDrinks6Months = SoftDrinks6Months + reader.GetDouble(27);
                                SkipMainMeals6Months = SkipMainMeals6Months + reader.GetDouble(28);
                                KeepTrackFood6Months = KeepTrackFood6Months + reader.GetDouble(29);
                                LimitPortions6Months = LimitPortions6Months + reader.GetDouble(30);
                                EatUpset6Months = EatUpset6Months + reader.GetDouble(31);
                                EatTV6Months = EatTV6Months + reader.GetDouble(32);
                                ChooseHealthier6Months = ChooseHealthier6Months + reader.GetDouble(33);
                                break;
                        }
                    }

                    reader.Dispose();

                    double[] initialArray2 = {healthRatingInitial, alcoholDaysInitial, alcoholDrinksInitial, weightInitial, bmiInitial,
                        waistInitial, sbpInitial, dbpInitial, lbmInitial, CHO_TCInitial, CHO_LDLInitial, CHO_HDLInitial, CHO_TGInitial,
                        FastingGlucoseInitial, HbA1cInitial, ResistanceInitial, CardioInitial, BriskWalkInitial, LightActivityInitial, FruitServesInitial,
                        FruitGreater2ServesInitial, VegServesInitial, VegGreater2ServesInitial, CommercialMealsInitial, SweetsInitial, SoftDrinksInitial,
                        SkipMainMealsInitial, KeepTrackFoodInitial, LimitPortionsInitial, EatUpsetInitial, EatTVInitial, ChooseHealthierInitial};

                    double[] sixMonthArray = {healthRating6Months, alcoholDays6Months, alcoholDrinks6Months, weight6Months, bmi6Months,
                        waist6Months, sbp6Months, dbp6Months, lbm6Months, CHO_TC6Months, CHO_LDL6Months, CHO_HDL6Months, CHO_TG6Months,
                        FastingGlucose6Months, HbA1c6Months, Resistance6Months, Cardio6Months, BriskWalk6Months, LightActivity6Months, FruitServes6Months,
                        FruitGreater2Serves6Months, VegServes6Months, VegGreater2Serves6Months, CommercialMeals6Months, Sweets6Months, SoftDrinks6Months,
                        SkipMainMeals6Months, KeepTrackFood6Months, LimitPortions6Months, EatUpset6Months, EatTV6Months, ChooseHealthier6Months};

                    for (int x = 0; x < initialArray2.Count(); ++x)
                    {
                        double difference = (sixMonthArray[x] - initialArray2[x]) / comparitiveDataCount[1];
                        initial_6Month_Difference.Add(difference);
                    }

                    //Following code is duplication to compare initial and 12 months
                    cmd.CommandText = query3;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        //Will hold all data for patients and time period

                        int patientID = reader.GetInt32(0);
                        string timePeriod = reader.GetString(1);

                        //Adds patients who attended each time period
                        switch (timePeriod)
                        {
                            case "Initial":
                                //As health option is a string it needs to be converted to a number for comparison.
                                //1 being porr and 4 being very good
                                string healthOption = reader.GetString(2);
                                switch (healthOption)
                                {
                                    case (null):
                                        healthRatingInitial = healthRatingInitial + 1;
                                        break;
                                    case ("Poor"):
                                        healthRatingInitial = healthRatingInitial + 1;
                                        break;
                                    case ("Fair"):
                                        healthRatingInitial = healthRatingInitial + 2;
                                        break;
                                    case ("Good"):
                                        healthRatingInitial = healthRatingInitial + 3;
                                        break;
                                    case ("Very Good"):
                                        healthRatingInitial = healthRatingInitial + 4;
                                        break;
                                }
                                alcoholDaysInitial = alcoholDaysInitial + reader.GetDouble(3);
                                alcoholDrinksInitial = alcoholDrinksInitial + reader.GetDouble(4);
                                weightInitial = weightInitial + reader.GetDouble(5);
                                bmiInitial = bmiInitial + reader.GetDouble(6);
                                waistInitial = waistInitial + reader.GetDouble(7);
                                sbpInitial = sbpInitial + reader.GetDouble(8);
                                dbpInitial = dbpInitial + reader.GetDouble(9);
                                lbmInitial = lbmInitial + reader.GetDouble(10);
                                CHO_TCInitial = CHO_TCInitial + reader.GetDouble(11);
                                CHO_LDLInitial = CHO_LDLInitial + reader.GetDouble(12);
                                CHO_HDLInitial = CHO_HDLInitial + reader.GetDouble(13);
                                CHO_TGInitial = CHO_TGInitial + reader.GetDouble(14);
                                FastingGlucoseInitial = FastingGlucoseInitial + reader.GetDouble(15);
                                HbA1cInitial = HbA1cInitial + reader.GetDouble(16);
                                ResistanceInitial = ResistanceInitial + reader.GetDouble(17);
                                CardioInitial = CardioInitial + reader.GetDouble(18);
                                BriskWalkInitial = BriskWalkInitial + reader.GetDouble(19);
                                LightActivityInitial = LightActivityInitial + reader.GetDouble(20);
                                FruitServesInitial = FruitServesInitial + reader.GetDouble(21);
                                FruitGreater2ServesInitial = FruitGreater2ServesInitial + reader.GetDouble(22);
                                VegServesInitial = VegServesInitial + reader.GetDouble(23);
                                VegGreater2ServesInitial = VegGreater2ServesInitial + reader.GetDouble(24);
                                CommercialMealsInitial = CommercialMealsInitial + reader.GetDouble(25);
                                SweetsInitial = SweetsInitial + reader.GetDouble(26);
                                SoftDrinksInitial = SoftDrinksInitial + reader.GetDouble(27);
                                SkipMainMealsInitial = SkipMainMealsInitial + reader.GetDouble(28);
                                KeepTrackFoodInitial = KeepTrackFoodInitial + reader.GetDouble(29);
                                LimitPortionsInitial = LimitPortionsInitial + reader.GetDouble(30);
                                EatUpsetInitial = EatUpsetInitial + reader.GetDouble(31);
                                EatTVInitial = EatTVInitial + reader.GetDouble(32);
                                ChooseHealthierInitial = ChooseHealthierInitial + reader.GetDouble(33);

                                break;
                            case "12 Months":
                                //As health option is a string it needs to be converted to a number for comparison.
                                //1 being porr and 4 being very good
                                healthOption = reader.GetString(2);
                                switch (healthOption)
                                {
                                    case (null):
                                        healthRating12Months = healthRating12Months + 1;
                                        break;
                                    case ("Poor"):
                                        healthRating12Months = healthRating12Months + 1;
                                        break;
                                    case ("Fair"):
                                        healthRating12Months = healthRating12Months + 2;
                                        break;
                                    case ("Good"):
                                        healthRating12Months = healthRating12Months + 3;
                                        break;
                                    case ("Very Good"):
                                        healthRating12Months = healthRating12Months + 4;
                                        break;
                                }
                                alcoholDays12Months = alcoholDays12Months + reader.GetDouble(3);
                                alcoholDrinks12Months = alcoholDrinks12Months + reader.GetDouble(4);
                                weight12Months = weight12Months + reader.GetDouble(5);
                                bmi12Months = bmi12Months + reader.GetDouble(6);
                                waist12Months = waist12Months + reader.GetDouble(7);
                                sbp12Months = sbp12Months + reader.GetDouble(8);
                                dbp12Months = dbp12Months + reader.GetDouble(9);
                                lbm12Months = lbm12Months + reader.GetDouble(10);
                                CHO_TC12Months = CHO_TC12Months + reader.GetDouble(11);
                                CHO_LDL12Months = CHO_LDL12Months + reader.GetDouble(12);
                                CHO_HDL12Months = CHO_HDL12Months + reader.GetDouble(13);
                                CHO_TG12Months = CHO_TG12Months + reader.GetDouble(14);
                                FastingGlucose12Months = FastingGlucose12Months + reader.GetDouble(15);
                                HbA1c12Months = HbA1c12Months + reader.GetDouble(16);
                                Resistance12Months = Resistance12Months + reader.GetDouble(17);
                                Cardio12Months = Cardio12Months + reader.GetDouble(18);
                                BriskWalk12Months = BriskWalk12Months + reader.GetDouble(19);
                                LightActivity12Months = LightActivity12Months + reader.GetDouble(20);
                                FruitServes12Months = FruitServes12Months + reader.GetDouble(21);
                                FruitGreater2Serves12Months = FruitGreater2Serves12Months + reader.GetDouble(22);
                                VegServes12Months = VegServes12Months + reader.GetDouble(23);
                                VegGreater2Serves12Months = VegGreater2Serves12Months + reader.GetDouble(24);
                                CommercialMeals12Months = CommercialMeals12Months + reader.GetDouble(25);
                                Sweets12Months = Sweets12Months + reader.GetDouble(26);
                                SoftDrinks12Months = SoftDrinks12Months + reader.GetDouble(27);
                                SkipMainMeals12Months = SkipMainMeals12Months + reader.GetDouble(28);
                                KeepTrackFood12Months = KeepTrackFood12Months + reader.GetDouble(29);
                                LimitPortions12Months = LimitPortions12Months + reader.GetDouble(30);
                                EatUpset12Months = EatUpset12Months + reader.GetDouble(31);
                                EatTV12Months = EatTV12Months + reader.GetDouble(32);
                                ChooseHealthier12Months = ChooseHealthier12Months + reader.GetDouble(33);
                                break;
                        }
                    }

                    reader.Dispose();

                    double[] initialArray3 = {healthRatingInitial, alcoholDaysInitial, alcoholDrinksInitial, weightInitial, bmiInitial,
                        waistInitial, sbpInitial, dbpInitial, lbmInitial, CHO_TCInitial, CHO_LDLInitial, CHO_HDLInitial, CHO_TGInitial,
                        FastingGlucoseInitial, HbA1cInitial, ResistanceInitial, CardioInitial, BriskWalkInitial, LightActivityInitial, FruitServesInitial,
                        FruitGreater2ServesInitial, VegServesInitial, VegGreater2ServesInitial, CommercialMealsInitial, SweetsInitial, SoftDrinksInitial,
                        SkipMainMealsInitial, KeepTrackFoodInitial, LimitPortionsInitial, EatUpsetInitial, EatTVInitial, ChooseHealthierInitial};

                    double[] twelveMonthArray = {healthRating12Months, alcoholDays12Months, alcoholDrinks12Months, weight12Months, bmi12Months,
                        waist12Months, sbp12Months, dbp12Months, lbm12Months, CHO_TC12Months, CHO_LDL12Months, CHO_HDL12Months, CHO_TG12Months,
                        FastingGlucose12Months, HbA1c12Months, Resistance12Months, Cardio12Months, BriskWalk12Months, LightActivity12Months, FruitServes12Months,
                        FruitGreater2Serves12Months, VegServes12Months, VegGreater2Serves12Months, CommercialMeals12Months, Sweets12Months, SoftDrinks12Months,
                        SkipMainMeals12Months, KeepTrackFood12Months, LimitPortions12Months, EatUpset12Months, EatTV12Months, ChooseHealthier12Months};

                    for (int x = 0; x < initialArray3.Count(); ++x)
                    {
                        double difference = (twelveMonthArray[x] - initialArray3[x]) / comparitiveDataCount[2];
                        initial_12Month_Difference.Add(difference);
                    }

                    reader.Dispose();
                    Global.connection.Close();

                    //Assigns differences to array for returning
                    averageDifferences[0] = initial_3Month_Difference;
                    averageDifferences[1] = initial_6Month_Difference;
                    averageDifferences[2] = initial_12Month_Difference;
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                return averageDifferences;
            }
        }

        //Used to create an excel file to display the report
        private void GenerateExcelData()
        {
            string data = null;                                             //Holds string for table data
            SaveFileDialog sf1 = new SaveFileDialog();                      //Save File Dialog
            sf1.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;   //Initial location is domain of application
            sf1.Filter = "Excel Workbook (*.xlsx)|*.xlsx|Excel 97-2003 Workbook (*xls)|*.xls|CSV (*.csv)|*.csv";    //Only save excel files
            sf1.Title = "Save Records as";
            sf1.OverwritePrompt = true;
            //If user chooses to save
            if (sf1.ShowDialog() == DialogResult.OK)
            {
                Excel.Application xlApp = new Excel.Application();

                //Stop program if excel is not installed on computer
                if (xlApp == null)
                {
                    MessageBox.Show("Excel is not properly installed!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Excel.Workbook xlWorkBook;      //Holds object for excel workbook
                Excel.Worksheet xlWorkSheet;    //Holds object for excel worksheet
                object misValue = System.Reflection.Missing.Value;

                //Colored Headings
                Color dateColor = Color.FromArgb(252, 213, 180);
                Color heading1 = Color.FromArgb(216, 228, 188);
                Color heading2 = Color.FromArgb(218, 238, 243);
                Color heading3 = Color.FromArgb(242, 220, 219);


                //Creates empty worksheet
                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                xlWorkSheet.Name = "Overall Data";

                //inserts Date from and date to ito workbook
                data = "Date From: " + Date_1.Value.ToShortDateString();
                xlWorkSheet.Cells[1, 1] = data;
                data = "Date To: " + Date_2.Value.ToShortDateString();
                xlWorkSheet.Cells[2, 1] = data;
                //Sets color and borders
                Excel.Range range = xlWorkSheet.Range["A1:A2"];
                range.Interior.Color = dateColor;
                SetRangeBorder(range);

                //inserts Overall patients reffered
                data = "Patients Reffered Over Period";
                xlWorkSheet.Cells[4, 1] = data;
                xlWorkSheet.Range["A4"].Interior.Color = heading1;
                data = numberPatients.ToString();
                xlWorkSheet.Cells[5, 1] = data;
                //Sets border
                range = xlWorkSheet.Range["A4:A5"];
                SetRangeBorder(range);

                //Inserts Number of Patients for each practice
                data = "Patients Reffered By Each Practice Over Period";
                xlWorkSheet.Cells[7, 1] = data;
                range = xlWorkSheet.Range["A7:B7"];
                //Centre abd nerge heading
                range.Interior.Color = heading1;
                range.Merge();
                range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                data = "Name";
                xlWorkSheet.Cells[8, 1] = data;
                data = "Number of Patients";
                xlWorkSheet.Cells[8, 2] = data;
                range = xlWorkSheet.Range["A8:B8"];
                range.Interior.Color = heading2;
                int rowCount = 9;
                //Populate practices and number of patients
                foreach (KeyValuePair<string, int> entry in uniquePractices)
                {
                    xlWorkSheet.Cells[rowCount, 1] = entry.Key;
                    xlWorkSheet.Cells[rowCount, 2] = entry.Value;
                    ++rowCount;
                }
                //Set Border
                Excel.Range startCell = xlWorkSheet.Cells[7, 1];
                Excel.Range endCell = xlWorkSheet.Cells[rowCount - 1, 2];
                range = xlWorkSheet.Range[startCell, endCell];
                SetRangeBorder(range);

                //Inserts total consultations over period
                rowCount++;     //Skips a row for easier reading
                data = "Total Consultations Over Period";
                xlWorkSheet.Cells[rowCount, 1] = data;
                startCell = xlWorkSheet.Cells[rowCount, 1];
                endCell = xlWorkSheet.Cells[rowCount, 2];
                range = xlWorkSheet.Range[startCell, endCell];
                //Centre and merge heading
                range.Interior.Color = heading1;
                range.Merge();
                range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                SetRangeBorder(range);
                rowCount++; //Next row
                data = "Time Period";
                xlWorkSheet.Cells[rowCount, 1] = data;
                data = "Number of Patients";
                xlWorkSheet.Cells[rowCount, 2] = data;
                startCell = xlWorkSheet.Cells[rowCount, 1];
                endCell = xlWorkSheet.Cells[rowCount, 2];
                range = xlWorkSheet.Range[startCell, endCell];
                range.Interior.Color = heading2;
                SetRangeBorder(range);
                rowCount++; //Next row
                //Assign Time Periods
                //Set borders beofre as I know the end cell
                startCell = xlWorkSheet.Cells[rowCount, 1];
                endCell = xlWorkSheet.Cells[rowCount + 4, 2];
                range = xlWorkSheet.Range[startCell, endCell];
                SetRangeBorder(range);
                data = "Initial";
                xlWorkSheet.Cells[rowCount, 1] = data;
                data = "3 Months";
                xlWorkSheet.Cells[rowCount + 1, 1] = data;
                data = "6 Months";
                xlWorkSheet.Cells[rowCount + 2, 1] = data;
                data = "12 Months";
                xlWorkSheet.Cells[rowCount + 3, 1] = data;
                data = "Overall";
                xlWorkSheet.Cells[rowCount + 4, 1] = data;
                //Assign number of patients to each time period
                foreach (int number in totalConsultations)
                {
                    xlWorkSheet.Cells[rowCount, 2] = number.ToString();
                    rowCount++;
                }

                //Inserts multiple consultation attendance
                rowCount++;     //Skips a row for easier reading
                data = "Total Consultations (Multiple Attendance)";
                xlWorkSheet.Cells[rowCount, 1] = data;
                startCell = xlWorkSheet.Cells[rowCount, 1];
                endCell = xlWorkSheet.Cells[rowCount, 2];
                range = xlWorkSheet.Range[startCell, endCell];
                //Centre abd nerge heading
                range.Interior.Color = heading1;
                range.Merge();
                range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                SetRangeBorder(range);
                rowCount++; //Next row
                data = "Time Period";
                xlWorkSheet.Cells[rowCount, 1] = data;
                data = "Number of Patients";
                xlWorkSheet.Cells[rowCount, 2] = data;
                startCell = xlWorkSheet.Cells[rowCount, 1];
                endCell = xlWorkSheet.Cells[rowCount, 2];
                range = xlWorkSheet.Range[startCell, endCell];
                range.Interior.Color = heading2;
                SetRangeBorder(range);
                rowCount++; //Next row
                //Assign Time Periods
                //Set borders beofre as I know the end cell
                startCell = xlWorkSheet.Cells[rowCount, 1];
                endCell = xlWorkSheet.Cells[rowCount + 2, 2];
                range = xlWorkSheet.Range[startCell, endCell];
                SetRangeBorder(range);
                data = "Initial and 3 Months";
                xlWorkSheet.Cells[rowCount, 1] = data;
                data = "Initial, 3 and 6 Months";
                xlWorkSheet.Cells[rowCount + 1, 1] = data;
                data = "Initial, 3, 6 and 12 Months";
                xlWorkSheet.Cells[rowCount + 2, 1] = data;

                //Assign number of patients to each time period
                foreach (int number in multipleConsultationAttendance)
                {
                    xlWorkSheet.Cells[rowCount, 2] = number.ToString();
                    rowCount++;
                }

                //Average Difference of Consultation Data
                rowCount++; //new Line
                data = "Average Difference of Consultation Data";
                xlWorkSheet.Cells[rowCount, 1] = data;
                startCell = xlWorkSheet.Cells[rowCount, 1];
                endCell = xlWorkSheet.Cells[rowCount, 4];
                range = xlWorkSheet.Range[startCell, endCell];
                SetRangeBorder(range);
                //Centre and merge heading
                range.Interior.Color = heading1;
                range.Merge();
                range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rowCount++; //New Line
                //Secondary Header - Centre and Merge
                data = "Data";
                xlWorkSheet.Cells[rowCount, 1] = data;
                data = "Initial - 3 Months";
                xlWorkSheet.Cells[rowCount, 2] = data;
                data = "Initial - 6 Months";
                xlWorkSheet.Cells[rowCount, 3] = data;
                data = "Initial - 12 Months";
                xlWorkSheet.Cells[rowCount, 4] = data;
                startCell = xlWorkSheet.Cells[rowCount, 1];
                endCell = xlWorkSheet.Cells[rowCount, 4];
                range = xlWorkSheet.Range[startCell, endCell];
                SetRangeBorder(range);
                range.Interior.Color = heading2;
                rowCount++; //New Line
                //Holds headings for each consult data
                string[] consultHeadings = {"Health Rating", "Days P/W Consuming Alcohol", "No. Drinks of Alcohol P/W", "Weight",
                                               "BMI", "Waist", "SBP", "DBP", "LBM", "CHO_TC", "CHO_LDL", "CHO_HDL", "CHO_TG", "Fasting Glucose",
                                               "HbA1c", "Resistance", "Cardio", "Brisk Walk", "Light Activity", "Fruit Sevres P/W", 
                                               "No. Days More than 2 Fruit Serves P/W", "Vegetable Sevres P/W", "No. Days More than 5 Vegetable Serves P/W",
                                               "Commercial Meals", "Sweets", "Soft Drinks", "Skip Main Meals", "Keep Track of Food", "Limit Portions",
                                               "Eat When Upset", "Eat in Front of TV", "Choose Healthier Options"};
                //Need to keep rowcount for later data but temporarily need to change it
                int counter = rowCount;
                //Adds headings to consult data
                foreach (string heading in consultHeadings)
                {
                    xlWorkSheet.Cells[counter, 1] = heading;
                    counter++;
                }
                startCell = xlWorkSheet.Cells[rowCount, 1];
                endCell = xlWorkSheet.Cells[counter - 1, 1];
                range = xlWorkSheet.Range[startCell, endCell];
                SetRangeBorder(range);

                //Adds average difference for initial - 3 months
                counter = rowCount;
                foreach (double difference3Months in averageDifferencesInitial_3Months)
                {
                    xlWorkSheet.Cells[counter, 2] = difference3Months;
                    counter++;
                }

                //Adds average difference for initial - 6 Months
                counter = rowCount;
                foreach (double difference6Months in averageDifferencesInitial_6Months)
                {
                    xlWorkSheet.Cells[counter, 3] = difference6Months;
                    counter++;
                }

                //Adds average difference for initial - 12 Months
                counter = rowCount;
                foreach (double difference12Months in averageDifferencesInitial_12Months)
                {
                    xlWorkSheet.Cells[counter, 4] = difference12Months;
                    counter++;
                }

                startCell = xlWorkSheet.Cells[rowCount, 2];
                endCell = xlWorkSheet.Cells[counter - 1, 4];
                range = xlWorkSheet.Range[startCell, endCell];
                SetRangeBorder(range);

                //Patient Total Header
                rowCount = counter;
                data = "Total Patients Over Period";
                xlWorkSheet.Cells[rowCount, 1] = data;
                xlWorkSheet.Cells[rowCount, 2] = comparitiveDataCount[0];
                xlWorkSheet.Cells[rowCount, 3] = comparitiveDataCount[1];
                xlWorkSheet.Cells[rowCount, 4] = comparitiveDataCount[2];
                startCell = xlWorkSheet.Cells[rowCount, 1];
                endCell = xlWorkSheet.Cells[rowCount, 4];
                range = xlWorkSheet.Range[startCell, endCell];
                SetRangeBorder(range);
                range.Interior.Color = heading3;

                xlWorkSheet.Columns.AutoFit();      //Autofits cells to text

                xlWorkBook.SaveAs(sf1.FileName);    //Save workbook by selected file in file dialog

                xlWorkBook.Close(true, misValue, misValue);     //Close object
                xlApp.Quit();           //Close Excel Application

                //Dispose of objects
                Global.releaseObject(xlWorkSheet);
                Global.releaseObject(xlWorkBook);
                Global.releaseObject(xlApp);

                MessageBox.Show("Excel file created\n" + sf1.FileName, "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //Used to set a border around an excel range
        private void SetRangeBorder(Excel.Range r)
        {
            Excel.Borders border = r.Borders;
            border.LineStyle = Excel.XlLineStyle.xlContinuous;
            border.Weight = 2d;
        }

        //Back to initial screen
        private void Back_Click(object sender, EventArgs e)
        {
            this.Hide();
            Initial_Screen initial = new Initial_Screen();
            initial.ShowDialog();
            initial.Focus();
        }

        //Closes all forms correctly
        private void Generate_Reports_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}