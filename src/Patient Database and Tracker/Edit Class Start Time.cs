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
    public partial class Edit_Class_Start_Time : Form
    {
        public Edit_Class_Start_Time()
        {
            InitializeComponent();

            //Sets controls based on previous selection
            Date_1.Value = DateTime.Now.Date;
            Date_1.MaxDate = DateTime.Now.Date;
            Input_1.Text = Global.changeStartDatePatient;
            Input_2.Text = Global.changeStartDateClass;
            Input_3.Text = Global.changeStartDateCurrentDate;
        }

        //Goes back to Add_Edit_Class_Times
        private void Back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Saves start time
        private void Save_Click(object sender, EventArgs e)
        {
            bool editSuccessful = Class_Database.EditStartDate(Global.changeStartDatePatientID, Input_2, Date_1);
            if (editSuccessful == true)
            {
                this.Close();
            }
        }
    }
}
