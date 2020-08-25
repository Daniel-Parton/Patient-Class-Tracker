using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using Finisar.SQLite;

namespace Patient_Database_and_Tracker
{
    public partial class Add_Edit_Consultation_2 : Form
    {
        //DDL for Consultation
        //    "CREATE TABLE [CONSULTATION]" +
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


        //********************Class Constructor********************

        public Add_Edit_Consultation_2()
        {
            InitializeComponent();
            Global.testConnection(this);                   //Tests database connection
            Date_1.Value = DateTime.Now;                   //Sets Date to today
            UserLabel.Text = "Current User: " +
                Global.userFirstName +
                " " + Global.userLastName;                  //Sets user label

            //Shows existing consultations in the DGV
            Consultation_Database.PopulateConsultationDataGridView(Exisiting_Consultations);  
            FillPatientDetails();                           //Sets pre fill details
        }

        //********************Non Event Methods********************

        //If new consultation then Patient details and time period is populated
        //If edit session then all fields are populated 
        private void FillPatientDetails()
        {
            Patient_Details.Text = "ID: " + Global.consPatientID.ToString() + " *** " + Global.consPatientName;
            Input_2.Text = Global.consTimePeriod;

            //Popultaes existing data if in edit session
            if (Global.editingExistingConsulation == true)
            {
                Input_1.Text = Global.consID;
                Date_1.Value = Global.consDate;
                AddOrEditFields(true);
                Consultation_Database.EditConsultation(Input_1, Input_3, Input_4, Input_5, Input_6, Input_7,
                    Input_8, Input_9, Input_10, Input_11, Input_12, Input_13, Input_14, Input_15, Input_16,
                    Input_17, Input_18, Input_19, Input_20, Input_21, Input_22, Input_23, Input_24, Input_25,
                    Input_26, Input_27, Input_28, Input_29, Input_30, Input_31, Input_32, Input_33, Input_34,
                    Input_35, Input_36, Input_37, Input_38, Date_1, Dropdown_1, Dropdown_2);
            }
        }

        //Used when clciking to add/edit patient and also when the patient details are saved
        //Forces the user to choose to add or edit beofre inputting data
        public void AddOrEditFields(Boolean editable)
        {
            if (editable == true)
            {
                Date_1.Enabled = true;
                Dropdown_1.Enabled = true;
                Dropdown_2.Enabled = true;
                Input_3.ReadOnly = false;
                Input_4.ReadOnly = false;
                Input_5.ReadOnly = false;
                Input_6.ReadOnly = false;
                Input_7.ReadOnly = false;
                Input_8.ReadOnly = false;
                Input_9.ReadOnly = false;
                Input_10.ReadOnly = false;
                Input_11.ReadOnly = false;
                Input_12.ReadOnly = false;
                Input_13.ReadOnly = false;
                Input_14.ReadOnly = false;
                Input_15.ReadOnly = false;
                Input_16.ReadOnly = false;
                Input_17.ReadOnly = false;
                Input_18.ReadOnly = false;
                Input_19.ReadOnly = false;
                Input_20.ReadOnly = false;
                Input_21.ReadOnly = false;
                Input_22.ReadOnly = false;
                Input_23.ReadOnly = false;
                Input_24.ReadOnly = false;
                Input_25.ReadOnly = false;
                Input_26.ReadOnly = false;
                Input_27.ReadOnly = false;
                Input_28.ReadOnly = false;
                Input_29.ReadOnly = false;
                Input_30.ReadOnly = false;
                Input_31.ReadOnly = false;
                Input_32.ReadOnly = false;
                Input_33.ReadOnly = false;
                Input_34.ReadOnly = false;
                Input_35.ReadOnly = false;
                Input_36.ReadOnly = false;
                Input_37.ReadOnly = false;

            }
            else
            {
                Date_1.Enabled = false;
                Dropdown_1.Enabled = false;
                Dropdown_2.Enabled = false;
                Input_3.ReadOnly = true;
                Input_4.ReadOnly = true;
                Input_5.ReadOnly = true;
                Input_6.ReadOnly = true;
                Input_7.ReadOnly = true;
                Input_8.ReadOnly = true;
                Input_9.ReadOnly = true;
                Input_10.ReadOnly = true;
                Input_11.ReadOnly = true;
                Input_12.ReadOnly = true;
                Input_13.ReadOnly = true;
                Input_14.ReadOnly = true;
                Input_15.ReadOnly = true;
                Input_16.ReadOnly = true;
                Input_17.ReadOnly = true;
                Input_18.ReadOnly = true;
                Input_19.ReadOnly = true;
                Input_20.ReadOnly = true;
                Input_21.ReadOnly = true;
                Input_22.ReadOnly = true;
                Input_23.ReadOnly = true;
                Input_24.ReadOnly = true;
                Input_25.ReadOnly = true;
                Input_26.ReadOnly = true;
                Input_27.ReadOnly = true;
                Input_28.ReadOnly = true;
                Input_29.ReadOnly = true;
                Input_30.ReadOnly = true;
                Input_31.ReadOnly = true;
                Input_32.ReadOnly = true;
                Input_33.ReadOnly = true;
                Input_34.ReadOnly = true;
                Input_35.ReadOnly = true;
                Input_36.ReadOnly = true;
                Input_37.ReadOnly = true;
            }
        }

        //Used to test if there is anything other than numbers in the input
        private Boolean TestNumbers()
        {
            Boolean CorrectInput;
            double test;
            List<string> inputValues = new List<string>();
            inputValues.Add(Input_4.Text);
            inputValues.Add(Input_5.Text);
            inputValues.Add(Input_6.Text);
            inputValues.Add(Input_7.Text);
            inputValues.Add(Input_8.Text);
            inputValues.Add(Input_9.Text);
            inputValues.Add(Input_10.Text);
            inputValues.Add(Input_11.Text);
            inputValues.Add(Input_12.Text);
            inputValues.Add(Input_13.Text);
            inputValues.Add(Input_14.Text);
            inputValues.Add(Input_15.Text);
            inputValues.Add(Input_16.Text);
            inputValues.Add(Input_17.Text);
            inputValues.Add(Input_18.Text);
            inputValues.Add(Input_19.Text);
            inputValues.Add(Input_20.Text);
            inputValues.Add(Input_21.Text);
            inputValues.Add(Input_22.Text);
            inputValues.Add(Input_23.Text);
            inputValues.Add(Input_24.Text);
            inputValues.Add(Input_25.Text);
            inputValues.Add(Input_26.Text);
            inputValues.Add(Input_27.Text);
            inputValues.Add(Input_28.Text);
            inputValues.Add(Input_29.Text);
            inputValues.Add(Input_30.Text);
            inputValues.Add(Input_31.Text);
            inputValues.Add(Input_32.Text);
            inputValues.Add(Input_33.Text);
            inputValues.Add(Input_34.Text);
            inputValues.Add(Input_35.Text);
            inputValues.Add(Input_36.Text);
            inputValues.Add(Input_37.Text);
            foreach (string value in inputValues)
            {
                if (double.TryParse(value, out test) == false && string.IsNullOrEmpty(value) == false)
                {
                    CorrectInput = false;
                    return CorrectInput;
                }
            }
            CorrectInput = true;
            return CorrectInput;
        }

        //********************Database Event Methods********************

        //Saves changes to new patient or existing patient
        private void Save_Click(object sender, EventArgs e)
        {
            //All input boxes are read only when they can't be saved.
            //This will stop users from clicking save when not in edit mode
            if (Input_3.ReadOnly == true)
            {
                MessageBox.Show("Please click 'Edit Current' button to save changes", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Consultant must be chosen
            if (Dropdown_2.SelectedIndex == -1)
            {
                MessageBox.Show("Please choose a consultant", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Testing if only numbers were input
            if (TestNumbers() == false)
            {
                MessageBox.Show("All fields need to be numbers except for:\nHeath Rating,\nSmoking Status and\nNotes", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Saves Consultation to Database
            bool saveSuccessful = Consultation_Database.SaveConsultation(Input_1, Input_2, Input_3, Input_4, Input_5, Input_6, Input_7,
                    Input_8, Input_9, Input_10, Input_11, Input_12, Input_13, Input_14, Input_15, Input_16,
                    Input_17, Input_18, Input_19, Input_20, Input_21, Input_22, Input_23, Input_24, Input_25,
                    Input_26, Input_27, Input_28, Input_29, Input_30, Input_31, Input_32, Input_33, Input_34,
                    Input_35, Input_36, Input_37, Input_38, Date_1, Dropdown_1, Dropdown_2);
            
            //Triggered if user is promted that a record exists with the same time period and the user clicks cancel
            if (saveSuccessful == false)
            {
                return;
            }
            
            //Once consultation is saved the DGV is updated
            Consultation_Database.PopulateConsultationDataGridView(Exisiting_Consultations);

            AddOrEditFields(false);                 //Changes all fields to read only
            Edit_Consultation.Visible = true;       //Allows user to make edits if necessary
           
        }

        //********************Other Form and Object Event Methods********************

        //Return to the previous screen
        private void Back_Click(object sender, EventArgs e)
        {
            {
                Global.editingExistingConsulation = false;
                Global.consID = null;
                this.Hide();
                Add_Edit_Consultation_1 addCons = new Add_Edit_Consultation_1();
                addCons.ShowDialog();
                addCons.Focus();
            }
        }

        //Allows user to edit current consultation
        private void Edit_Consultation_Click(object sender, EventArgs e)
        {
            Global.editingExistingConsulation = true;
            AddOrEditFields(true);
            Edit_Consultation.Visible = false;
        }

        //If it is a new consultation this clears the selection on the Exisiting_Consultations DGV
        private void Exisiting_Consultations_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (Global.editingExistingConsulation == false && Input_1.Text == "")
            {
                Exisiting_Consultations.ClearSelection();
            }
        }

        //Shows a list of users
        private void Dropdown_2_DropDown(object sender, EventArgs e)
        {
            UserDatabase.PopulateUserDropdown(Dropdown_2);
        }

        //Drops down when down key pressed
        private void Dropdown_2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                Dropdown_2.DroppedDown = true;
            }
        }

        //Closes all forms if this form is closed
        private void Add_or_Edit_Patient_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}