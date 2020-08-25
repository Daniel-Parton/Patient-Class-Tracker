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
    
    public partial class Mark_Class_Attendance : Form
    {
        string cellBeforeEdit = null;
        bool cellEditFail = false;

        public Mark_Class_Attendance()
        {
            InitializeComponent();
            UserLabel.Text = "Current User: " +
                Global.userFirstName + " " +
                Global.userLastName;                //Sets user label
        }

        //Returns to previous screen
        private void Back_Click(object sender, EventArgs e)
        {
            if (Global.addEditTimeOpen == true)
            {
                this.Close();
            }
            else
            {
                this.Hide();
                Initial_Screen initial = new Initial_Screen();
                initial.ShowDialog();
                initial.Focus();
            }
        }

        //Populates classes in combobox
        private void Dropdown_1_DropDown(object sender, EventArgs e)
        {
            Class_Database.PopulateClassDropdown(Dropdown_1);
        }

        //Populates datagridviews with patient details and times this class is on
        private void Dropdown_1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Class_Database.PopulatePatient_ClassDGV(Dropdown_1, PatientsClassDGV);
            Class_Database.PopulateClassTimeDetailsDGV(Dropdown_1, ClassDetailsDGV);
        }

        //Deselects row when databinding complete
        private void PatientsClassDGV_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            PatientsClassDGV.ClearSelection();
        }

        //Validates if a user enters something that isn't a number
        private void PatientsClassDGV_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //Column 5 is mark attendance
            if (e.ColumnIndex == 5)
            {
                double test;
                string newValue = e.FormattedValue.ToString();

                //If not a number error shown
                if ((double.TryParse(newValue, out test) == false && string.IsNullOrEmpty(newValue) == false))
                {
                    MessageBox.Show("Must be a number", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    cellEditFail = true;
                }
                else
                {
                    cellEditFail = false;
                }
            }
        }

        //Saves previous value in case of error
        private void PatientsClassDGV_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            cellBeforeEdit = this.PatientsClassDGV[e.ColumnIndex, e.RowIndex].Value.ToString();
        }

        //Once user exits cell from editing saves all rows of mark attendance
        private void PatientsClassDGV_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //If a number then save
            if (cellEditFail == true)
            {
                this.PatientsClassDGV[e.ColumnIndex, e.RowIndex].Value = cellBeforeEdit;
            }
            else
            {
                Patient_Database.SaveAttendance(PatientsClassDGV);
            }
        }

        //Deselects row when databinding complete
        private void ClassDetailsDGV_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ClassDetailsDGV.ClearSelection();
        }

        //Close all forms correctly
        private void Mark_Class_Attendance_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Global.addEditTimeOpen == false)
            {
                Application.Exit();
            }
        }
    }
}
