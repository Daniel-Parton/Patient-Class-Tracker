using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Patient_Database_and_Tracker
{
    public partial class Add_New_Time : Form
    {
        public Add_New_Time()
        {
            InitializeComponent();
            UserLabel.Text = "Current User: " + Global.userFirstName + " " + Global.userLastName;           //Sets the current user to label
            Class_Database.PopulateTimeDataGridView(Global.newTimeDay, Existing_Times);                     //Populates datagridview with existing times
            
            //Setting start times datasource found from GenerateStartTimes Method in Add_Edit_Class_Times
            Dropdown_1.DataSource = Global.newTimedt;                                                       
            Dropdown_1.ValueMember = "Time";
            Dropdown_1.DisplayMember = "Display Time";

            //Setting startvtime dropdown selected index to the same as the one currently editing if in edit session
            if (Global.editingExistingClassTime == true)
            {
                int selectedIndex = 0;      //Will hold the selected index
                int counter = 0;            //Used as the counter for iteration

                //Loops through each of the start times found in dopdown1 and selects the edited start time
                foreach(DataRow row in Global.newTimedt.Rows)
                {
                    if (row[1].ToString() == Global.startTimeSelectedDateTime.ToShortTimeString())
                    {
                        selectedIndex = counter;
                        break;
                    }
                    counter++;
                }
                Dropdown_1.SelectedIndex = selectedIndex;

                //If edit session changes the button to an edit button
                Create_Time.Visible = false;
                Edit_Time.Visible = true;
            }

            Class_Time_Groupbox.Text = Global.newTimeDay;                   //Sets label to which day is selected
            Dropdown_1_SelectionChangeCommitted(this, EventArgs.Empty);     //runs event for selectionchangecommitted which generates finish times
        }

        //Used to create a time based on hours and minutes
        private DateTime CreateTime(int hours, int minutes)
        {
            return new DateTime(1, 1, 1, hours, minutes, 0);
        }

        //Closes form and returns to Add_Edit_Class_Times
        private void Cancel_Click(object sender, EventArgs e)
        {
            Global.addNewTimeOpen = false;
            this.Hide();
            Add_Edit_Class_Times classes = new Add_Edit_Class_Times();
            classes.ShowDialog();
            classes.Focus();
        }

        //If a time is changed then new finish times are generated
        private void Dropdown_1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            DataTable finishTimes = Class_Database.GenerateFinishTimes(Global.newTimeDay, Dropdown_1);
            Dropdown_2.DataSource = finishTimes;
            Dropdown_2.ValueMember = "Time";
            Dropdown_2.DisplayMember = "Display Time";
        }

        //Creates new Class Time and returns to Add_Edit_Class_Times
        private void Create_Time_Click(object sender, EventArgs e)
        {
            bool saveSuccess = Class_Database.SaveTime(Dropdown_1, Dropdown_2, Global.newTimeDay, Dropdown_3);
            if (saveSuccess == true)
            {
                Global.addNewTimeOpen = false;
                Global.editingExistingClassTime = false;
                this.Hide();
                Add_Edit_Class_Times classes = new Add_Edit_Class_Times();
                classes.ShowDialog();
                classes.Focus();
            }
        }

        //Opens form to add a new class.  Form opened is Add_Edit_Class
        private void Add_Class_Click(object sender, EventArgs e)
        {
            Global.addNewTimeOpen = true;
            Add_Edit_Class groupClass = new Add_Edit_Class();
            groupClass.ShowDialog();
            groupClass.Focus();
        }

        //Deselects row in Existing_Times DGV if not in an edit session
        private void Existing_Times_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (Global.editingExistingClassTime == false)
            {
                Existing_Times.ClearSelection();
            }
        }

        //Shows a list of all classes in dropdown 3
        private void Dropdown_3_DropDown(object sender, EventArgs e)
        {
            Class_Database.PopulateClassDropdown(Dropdown_3);
        }

        //Save changees to an edited class time and return to Add_Edit_Class_Times
        private void Edit_Time_Click(object sender, EventArgs e)
        {
            bool saveSuccess = Class_Database.SaveTime(Dropdown_1, Dropdown_2, Global.newTimeDay, Dropdown_3);
            if (saveSuccess == true)
            {
                Global.addNewTimeOpen = false;
                Global.editingExistingClassTime = false;
                this.Hide();
                Add_Edit_Class_Times classes = new Add_Edit_Class_Times();
                classes.ShowDialog();
                classes.Focus();
            }
        }

        //Closes all open form if user closes this form
        private void Add_New_Time_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
