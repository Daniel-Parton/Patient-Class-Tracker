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
    public partial class Add_Edit_Class_Times : Form
    {
        public Add_Edit_Class_Times()
        {
            InitializeComponent();
            Global.testConnection(this);        //Tests database connection
            UserLabel.Text = "Current User: " +
                Global.userFirstName + " " +
                Global.userLastName;            //Sets user label
            PopulateClassTimes();               //Populates class times in the day datagridviews
        }

        //********************Non Event Methods********************

        //Used to populate all the class times for each day
        private void PopulateClassTimes()
        {
            Class_Database.PopulateTimeDataGridView("Monday", MondayDGV);
            Class_Database.PopulateTimeDataGridView("Tuesday", TuesdayDGV);
            Class_Database.PopulateTimeDataGridView("Wednesday", WednesdayDGV);
            Class_Database.PopulateTimeDataGridView("Thursday", ThursdayDGV);
            Class_Database.PopulateTimeDataGridView("Friday", FridayDGV);
            Class_Database.PopulateTimeDataGridView("Saturday", SaturdayDGV);
        }

        //If user clicks on a saved time in a DGV then Add_New_Time is open in an edit session
        private void EditTime(DataGridView dayDGV, string day)
        {
            //Checks if there are any class times for the patient
            if (dayDGV.Rows.Count == 0)
            {
                MessageBox.Show("There are no class times for " + day, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Checks if the user has selected a class time
            if (dayDGV.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a class time from the " + day + " grid", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Global.editingExistingClassTime = true;     //Starts an edit session
            Global.newTimeDay = day;                    //Sets the day to a Global variable to be used in Add_New_Time

            //Getting the The start and finish time of selected row
            int cellIndex = dayDGV.SelectedCells[0].RowIndex;
            DataGridViewCell cellCollection = dayDGV.Rows[cellIndex].Cells[0];
            Global.editClassTimeDGVSelect = cellCollection.Value.ToString();  //In string format hh:mm - hh:mm

            //Uesd to select the right row in the add new time forms datagridview
            string startTimeString = Global.editClassTimeDGVSelect.Remove(5);   //Gets start time from string: Removes "hh:mm" from "hh:mm - hhmm"
            string hours = startTimeString.Remove(2);
            string minutes = startTimeString.Remove(0, 3);
            Global.startTimeSelectedDateTime = Global.CreateTime(Int32.Parse(hours), Int32.Parse(minutes));     //Creates a Datetime object based on hours and mins
            Global.editClassStartTimeSQLite = Global.getDateString(hours, minutes);                             //Creates a date string in sqlite format to be used in next form

            Global.newTimedt = Class_Database.GenerateStartTimes(day);  //Generates start times and sets them to a datatable

            //Loads Add_New_Time form
            this.Hide();
            Add_New_Time addTime = new Add_New_Time();
            addTime.ShowDialog();
            addTime.Focus();
        }



        //********************Event Methods********************

        //loads Initial_Screen
        private void Back_Click(object sender, EventArgs e)
        {
            Global.addEditTimeOpen = false;
            this.Hide();
            Initial_Screen initial = new Initial_Screen();
            initial.ShowDialog();
            initial.Focus();
        }

        //The following 6 event methods are used to add new times to the chosen day
        //All the events do the same thing but set a Global variable to the selected day
        private void Add_Monday_Time_Click(object sender, EventArgs e)
        {
            Global.editingExistingClassTime = false;
            Global.newTimedt = Class_Database.GenerateStartTimes("Monday");
            Global.newTimeDay = Global.newTimeMonday;
            this.Hide();
            Add_New_Time addTime = new Add_New_Time();
            addTime.ShowDialog();
            addTime.Focus();
        }

        private void Add_Tuesday_Time_Click(object sender, EventArgs e)
        {
            Global.editingExistingClassTime = false;
            Global.newTimedt = Class_Database.GenerateStartTimes("Tuesday");
            Global.newTimeDay = Global.newTimeTuesday;
            this.Hide();
            Add_New_Time addTime = new Add_New_Time();
            addTime.ShowDialog();
            addTime.Focus();
        }

        private void Add_Wednesday_Time_Click(object sender, EventArgs e)
        {
            Global.editingExistingClassTime = false;
            Global.newTimedt = Class_Database.GenerateStartTimes("Wednesday");
            Global.newTimeDay = Global.newTimeWednesday;
            this.Hide();
            Add_New_Time addTime = new Add_New_Time();
            addTime.ShowDialog();
            addTime.Focus();
        }

        private void Add_Thursday_Time_Click(object sender, EventArgs e)
        {
            Global.editingExistingClassTime = false;
            Global.newTimedt = Class_Database.GenerateStartTimes("Thursday");
            Global.newTimeDay = Global.newTimeThursday;
            this.Hide();
            Add_New_Time addTime = new Add_New_Time();
            addTime.ShowDialog();
            addTime.Focus();
        }

        private void Add_Friday_Time_Click(object sender, EventArgs e)
        {
            Global.editingExistingClassTime = false;
            Global.newTimedt = Class_Database.GenerateStartTimes("Friday");
            Global.newTimeDay = Global.newTimeFriday;
            this.Hide();
            Add_New_Time addTime = new Add_New_Time();
            addTime.ShowDialog();
            addTime.Focus();
        }

        private void Add_Saturday_Time_Click(object sender, EventArgs e)
        {
            Global.editingExistingClassTime = false;
            Global.newTimedt = Class_Database.GenerateStartTimes("Saturday");
            Global.newTimeDay = Global.newTimeSaturday;
            this.Hide();
            Add_New_Time addTime = new Add_New_Time();
            addTime.ShowDialog();
            addTime.Focus();
        }

        //Based on selection, the add or edit class time form is loaded with an edit session
        //All 6 event methods do the same thing just different days
        private void Edit_Monday_Class_Click(object sender, EventArgs e)
        {
            EditTime(MondayDGV, Global.newTimeMonday);
        }

        private void Edit_Tuesday_Class_Click(object sender, EventArgs e)
        {
            EditTime(TuesdayDGV, Global.newTimeTuesday);
        }

        private void Edit_Wednesday_Class_Click(object sender, EventArgs e)
        {
            EditTime(WednesdayDGV, Global.newTimeWednesday);
        }

        private void Edit_Thursday_Class_Click(object sender, EventArgs e)
        {
            EditTime(ThursdayDGV, Global.newTimeThursday);
        }

        private void Edit_Friday_Class_Click(object sender, EventArgs e)
        {
            EditTime(FridayDGV, Global.newTimeFriday);
        }

        private void Edit_Saturday_Class_Click(object sender, EventArgs e)
        {
            EditTime(SaturdayDGV, Global.newTimeSaturday);
        }

        private void Delete_Monday_Click(object sender, EventArgs e)
        {
            bool deleteSuccessful = Class_Database.DeleteTime(MondayDGV, Global.newTimeMonday);
            if (deleteSuccessful == true)
            {
                PopulateClassTimes();
            }
        }

        private void Delete_Tuesday_Click(object sender, EventArgs e)
        {
            bool deleteSuccessful = Class_Database.DeleteTime(TuesdayDGV, Global.newTimeTuesday);
            if (deleteSuccessful == true)
            {
                PopulateClassTimes();
            }
        }

        private void Delete_Wednesday_Click(object sender, EventArgs e)
        {
            bool deleteSuccessful = Class_Database.DeleteTime(WednesdayDGV, Global.newTimeWednesday);
            if (deleteSuccessful == true)
            {
                PopulateClassTimes();
            }
        }

        private void Delete_Thursday_Click(object sender, EventArgs e)
        {
            bool deleteSuccessful = Class_Database.DeleteTime(ThursdayDGV, Global.newTimeThursday);
            if (deleteSuccessful == true)
            {
                PopulateClassTimes();
            }
        }

        private void Delete_Friday_Click(object sender, EventArgs e)
        {
            bool deleteSuccessful = Class_Database.DeleteTime(FridayDGV, Global.newTimeFriday);
            if (deleteSuccessful == true)
            {
                PopulateClassTimes();
            }
        }

        private void Delete_Saturday_Click(object sender, EventArgs e)
        {
            bool deleteSuccessful = Class_Database.DeleteTime(SaturdayDGV, Global.newTimeSaturday);
            if (deleteSuccessful == true)
            {
                PopulateClassTimes();
            }
        }

        //Databindingcomplete events are used to clear up the auto selection of cells when data binding is complete
        //The following 6 event methods are run after PopulateClassTimes method is run
        private void MondayDGV_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            MondayDGV.ClearSelection();
        }

        private void TuesdayDGV_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            TuesdayDGV.ClearSelection();
        }

        private void WednesdayDGV_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            WednesdayDGV.ClearSelection();
        }

        private void ThursdayDGV_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ThursdayDGV.ClearSelection();
        }

        private void FridayDGV_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            FridayDGV.ClearSelection();
        }

        private void SaturdayDGV_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            SaturdayDGV.ClearSelection();
        }

        //Only one DGV row can be selected at a time.  These events enforce this
        //Only allows user to select from one datagridview at a time
        //6 event methods in total
        private void MondayDGV_SelectionChanged(object sender, EventArgs e)
        {
            TuesdayDGV.ClearSelection();
            WednesdayDGV.ClearSelection();
            ThursdayDGV.ClearSelection();
            FridayDGV.ClearSelection();
            SaturdayDGV.ClearSelection();
        }
        //Only allows user to select from one datagridview at a time
        private void TuesdayDGV_SelectionChanged(object sender, EventArgs e)
        {
            MondayDGV.ClearSelection();
            WednesdayDGV.ClearSelection();
            ThursdayDGV.ClearSelection();
            FridayDGV.ClearSelection();
            SaturdayDGV.ClearSelection();
        }
        //Only allows user to select from one datagridview at a time
        private void WednesdayDGV_SelectionChanged(object sender, EventArgs e)
        {
            MondayDGV.ClearSelection();
            TuesdayDGV.ClearSelection();
            ThursdayDGV.ClearSelection();
            FridayDGV.ClearSelection();
            SaturdayDGV.ClearSelection();
        }
        //Only allows user to select from one datagridview at a time
        private void ThursdayDGV_SelectionChanged(object sender, EventArgs e)
        {
            MondayDGV.ClearSelection();
            TuesdayDGV.ClearSelection();
            WednesdayDGV.ClearSelection();
            FridayDGV.ClearSelection();
            SaturdayDGV.ClearSelection();
        }
        //Only allows user to select from one datagridview at a time
        private void FridayDGV_SelectionChanged(object sender, EventArgs e)
        {
            MondayDGV.ClearSelection();
            TuesdayDGV.ClearSelection();
            WednesdayDGV.ClearSelection();
            ThursdayDGV.ClearSelection();
            SaturdayDGV.ClearSelection();
        }
        //Only allows user to select from one datagridview at a time
        private void SaturdayDGV_SelectionChanged(object sender, EventArgs e)
        {
            MondayDGV.ClearSelection();
            TuesdayDGV.ClearSelection();
            WednesdayDGV.ClearSelection();
            ThursdayDGV.ClearSelection();
            FridayDGV.ClearSelection();
        }

        //Opens up the Add_Edit_Class form
        private void Add_Class_Click(object sender, EventArgs e)
        {
            Global.addEditTimeOpen = true;      //Global variable which is used for other forms to know this form isn't hidden
            Global.editingExistingClassTime = false;
            Add_Edit_Class groupClass = new Add_Edit_Class();
            groupClass.ShowDialog();
            groupClass.Focus();
        }

        //Loads Mark_Class_Attendance form
        private void Mark_Attendance_Click(object sender, EventArgs e)
        {
            Global.addEditTimeOpen = true;  //Global variable which is used for other forms to know this form isn't hidden
            Mark_Class_Attendance attendance = new Mark_Class_Attendance();
            attendance.ShowDialog();
            attendance.Focus();
        }

        //Used in case user opens Add_Edit_Class and deleetes a class shown in one of the day DGVS
        //This will update when the Add_Edit_Class is closed and this form is activated
        private void Add_Edit_Class_Times_Activated(object sender, EventArgs e)
        {
            PopulateClassTimes();
        }

        //Closes all forms
        private void Edit_or_View_Classes_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
