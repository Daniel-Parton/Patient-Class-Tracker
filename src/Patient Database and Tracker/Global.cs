using System;
using Novacode;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using System.Data.Sql;
using System.Data.SqlClient;
using Finisar.SQLite;
using System.Diagnostics;

namespace Patient_Database_and_Tracker
{
    class Global
    {

        //Used to hold the userid.  The current user is shown on each form at the bottom of the screen
        //This also helps identify user when entering the Change User Details Form
        public static int userId = 0;
        public static string userFirstName;
        public static string userLastName;

        //Used when resetting password in login screen Forgotten Password
        public static string resetPasswordSecretAnswer;

        //Used to determine if user is editing an existing or creating a new records
        public static Boolean editingExistingConsulation = false;
        public static Boolean editingExistingPatient = false;
        public static Boolean editingExistingRefferingGP = false;
        public static Boolean editingExistingPractice = false;
        public static Boolean editingExistingClassTime = false;
        public static Boolean editingExistingClass = false;

        //Boolean expressions if these forms are open to handle formClosing events.
        //Used in thee Add/Edit Forms
        public static Boolean addConsultationOpen = false;
        public static Boolean addPatientOpen = false;
        public static Boolean addRefferingGPOpen = false;
        public static Boolean addEditTimeOpen = false;
        public static Boolean addNewTimeOpen = false;

        //Variables for database connection
        public static SQLiteConnection connection;
        public static string connectionString;                          //Connection String used to connect to Database
        public static string standardConnection = "Database.db";        //Standard connection for database in app domain directory
        public static string connection1 = "Data Source=";              //First Part of connection string
        public static string connection2 = ";Version=3;New=False;";     //Last part of connection string
        public static string connectionPath;                            //Second part of connection string used by reading ConnectionString.txt

        //Global Variables for sorting dropdown lists 
        public static int sortDropdown = 0;
        public static int ID = 0;
        public static int NAME = 1;

        //Variables for Add/Edit Consultation.  Moving between Add/Edit Consultation1 and Add/Edit Consultation2 
        public static int consPatientID;
        public static string consPatientName;
        public static string consID; 
        public static string consTimePeriod;
        public static DateTime consDate;

        //Determines what will be displayed in the view Records form
        public static string view;
        public static string viewPatients = "View Patients";
        public static string viewConsultations = "View Consultations";
        public static string viewRefGP = "View Reffering GPs";
        public static string viewPractices = "View Practices";

        //Used When creating new class times
        public static DataTable newTimedt;
        public static string newTimeDay;
        public static string newTimeMonday = "Monday";
        public static string newTimeTuesday = "Tuesday";
        public static string newTimeWednesday = "Wednesday";
        public static string newTimeThursday = "Thursday";
        public static string newTimeFriday = "Friday";
        public static string newTimeSaturday = "Saturday";

        //Used in editing existing class times
        public static int startTimeSelectedIndex = 0;
        public static DateTime startTimeSelectedDateTime;
        public static string editClassTimeDGVSelect;
        public static string editClassStartTimeSQLite;

        public static string changeStartDatePatientID;
        public static string changeStartDatePatient;
        public static string changeStartDateClass;
        public static string changeStartDateCurrentDate;

        //SQLite only accepts date strings in the following format YYYY-MM-DD
        //This method converts the datetimepicker to an acceptable string
        public static string getDateString(DateTimePicker date)
        {
            string year = date.Value.Year.ToString();
            string month = date.Value.Month.ToString();
            string day = date.Value.Day.ToString();
            if (day.Length == 1)
            {
                day = "0" + day;
            }
            if (month.Length == 1)
            {
                month = "0" + month;
            }
            string dateString = year + "-" + month + "-" + day;
            return dateString;
        }

        //Overload Method for setting time objects
        //SQLite only accepts date strings in the following format YYYY-MM-DD HH:MM
        public static string getDateString(string hours, string minutes)
        {
            string hour = hours;
            string minute = minutes;
            if (hour.Length == 1)
            {
                hour = "0" + hour;
            }
            if (minute.Length == 1)
            {
                minute = "0" + minute;
            }
            string dateString = "0001-01-01 " + hour + ":" + minute;
            return dateString;
        }

        //Gets hours and minutes from a sqlite datetime and sets to an int array
        public static int[] getHoursAndMinutes(string sqliteDateTime)
        {
            int[] hoursAndMinutes = new int[2];
            string hoursMinutes = sqliteDateTime.Remove(0, 11);
            string hours = hoursMinutes.Remove(2);
            hoursAndMinutes[0] = Convert.ToInt32(hours);
            string minutes = hoursMinutes.Remove(0, 3);
            hoursAndMinutes[1] = Convert.ToInt32(minutes);
            return hoursAndMinutes;
        }

        //Creates a DateTime object from hours and minutes
        public static DateTime CreateTime(int hours, int minutes)
        {
            return new DateTime(1, 1, 1, hours, minutes, 0, 0);
        }

        //Tests the connection
        public static void testConnection(Form currentForm)
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
                currentForm.Close();
                Change_Connection change = new Change_Connection();
                change.ShowDialog();
                change.Focus();
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
                    currentForm.Hide();
                    Change_Connection cc = new Change_Connection();
                    cc.ShowDialog();
                    cc.Focus();
                }
            }
        }

        //Reads textfile and sets to Glo.  Used to hold path of database
        public static void setConnectionString(string dataFile)
        {
            using (FileStream fs = new FileStream(dataFile, FileMode.Open, FileAccess.Read))
            using (BinaryReader w = new BinaryReader(fs))
            {
                connectionString = connection1 + w.ReadString() + connection2;       //Text file only holds connectionpath
                w.Close();
                fs.Close();
            }
        }

        //Used to read data files
        public static string readDataFile(string dataFile)
        {
            string textString = null;
            using (FileStream fs = new FileStream(dataFile, FileMode.Open, FileAccess.Read))
            using (BinaryReader w = new BinaryReader(fs))
            {
                textString = w.ReadString();       //Data file only holds connectionpath
                w.Close();
                fs.Close();
            }
            return textString;
        }

        //Write to a data file.  Used to hold path of database
        public static void writeDatafile(string dataFile, string newConnectionString)
        {
            try
            {
                using (FileStream fs = new FileStream(dataFile, FileMode.Create))
                using (BinaryWriter w = new BinaryWriter(fs))
                {
                    w.Write(newConnectionString);
                    w.Close();
                    fs.Close();
                    MessageBox.Show("Update Successful\nDatabase Location: " + Global.connectionPath, "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {
                if (ex is IOException)
                {
                    MessageBox.Show("You are already connected to this database", "ERROR",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    MessageBox.Show(ex.ToString() + "\n\nError writing to " + dataFile + "\nPlease check that file exists or is named as above", "ERROR",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //Used to export data in a datagridview to an excel file
        public static void createExcelFromDataGridView(DataGridView dGV)
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

                //Creates empty worksheet
                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                DataTable dt = (DataTable)(dGV.DataSource);
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);

                //Sets header of each column in the excel worksheet
                for (int x = 0; x <= ds.Tables[0].Columns.Count - 1; ++x)
                {
                    data = ds.Tables[0].Columns[x].ColumnName;
                    xlWorkSheet.Cells[1, x + 1] = data;
                }

                //Populates data by row
                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= ds.Tables[0].Columns.Count - 1; j++)
                    {
                        data = ds.Tables[0].Rows[i].ItemArray[j].ToString();
                        xlWorkSheet.Cells[i + 2, j + 1] = data;
                    }
                    xlWorkSheet.Columns.AutoFit();      //Autofits cells to text
                }
                xlWorkBook.SaveAs(sf1.FileName);    //Save workbook by selected file in file dialog

                xlWorkBook.Close(true, misValue, misValue);     //Close object
                xlApp.Quit();           //Close Excel Application

                //Dispose of objects
                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);

                MessageBox.Show("Excel file created\n" + sf1.FileName, "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }  

        //Saves excel file based on SQLite query
        public static void createExcelFromQuery(string query)
        {
            SaveFileDialog sf1 = new SaveFileDialog();                      //Save File Dialog
            sf1.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;   //Initial location is domain of application
            sf1.Filter = "Excel Workbook (*.xlsx)|*.xlsx|Excel 97-2003 Workbook (*xls)|*.xls|CSV (*.csv)|*.csv";    //Only save excel files
            sf1.Title = "Save Records as";
            sf1.OverwritePrompt = true;
            //If user chooses to save
            if (sf1.ShowDialog() == DialogResult.OK)
            {
                string data = null;     //Holds string for table data

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

                //Creates empty worksheet
                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                //Fill dataset with data from SQLite query
                SQLiteConnection connection = new SQLiteConnection(connectionString);
                connection.Open();
                SQLiteDataAdapter dscmd = new SQLiteDataAdapter(query, connection);
                DataSet ds = new DataSet();
                dscmd.Fill(ds);

                //Sets header of each column in the excel worksheet
                for (int x = 0; x <= ds.Tables[0].Columns.Count - 1; ++x)
                {
                    data = ds.Tables[0].Columns[x].ColumnName;
                    xlWorkSheet.Cells[1, x + 1] = data;
                }

                //Populates data by row
                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= ds.Tables[0].Columns.Count - 1; j++)
                    {
                        data = ds.Tables[0].Rows[i].ItemArray[j].ToString();
                        xlWorkSheet.Cells[i + 2, j + 1] = data;
                    }
                    xlWorkSheet.Columns.AutoFit();      //Autofits cells to text
                }
                xlWorkBook.SaveAs(sf1.FileName);    //Save workbook by selected file in file dialog

                xlWorkBook.Close(true, misValue, misValue);     //Close object
                xlApp.Quit();           //Close Excel Application

                //Dispose of objects
                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);

                MessageBox.Show("Excel file created\n" + sf1.FileName, "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //Disposes of excel object correctly
        public static void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
