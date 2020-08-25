using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;
using Finisar.SQLite;
using System.IO;

namespace Patient_Database_and_Tracker
{
    public partial class Generate_Letters : Form
    {
        //********************Class Variables********************
        //these variabls will be used i mail merged letters
        Dictionary<String, String> mailMergedFields = new Dictionary<string,string>();
        string consultationDate = null;

        bool ChooseInitialConsultation = false;

        public Generate_Letters()
        {
            InitializeComponent();
            Global.testConnection(this);            //Test database connection
            UserLabel.Text = "Current User: " +
                Global.userFirstName +
                " " + Global.userLastName;          //Sets user label
            setSortedButtonText();                  //Sets sorted by button text
        }

        //Sets the text for the sort list button based on the global variable
        private void setSortedButtonText()
        {
            if (Global.sortDropdown == Global.ID)
            {
                Sort_Lists.Text = "Sort Lists by Name";
            }
            else if (Global.sortDropdown == Global.NAME)
            {
                Sort_Lists.Text = "Sort Lists by ID";
            }
        }

        //Used tp Join Patient, Reffering GP and Practice together to be used in mail merged word document
        private void JoinPatient_GP_Practice()
        {
            string query = "SELECT " +
                "[GENERAL PRACTICE].PRAC_Name," +
                "PRAC_Address," +
                "PRAC_Suburb," +
                "PRAC_Post_Code," +
                "PRAC_State," +
                "[REFFERING GP].[REFGP_First_Name]," +
                "[REFFERING GP].[REFGP_Last_Name]," +
                "PATIENT.[PAT_First_Name]," +
                "PATIENT.[PAT_Last_Name]," +
                "PATIENT.[PAT_Address]," +
                "PATIENT.[PAT_Suburb]," +
                "PATIENT.[PAT_Post_Code]," +
                "PATIENT.[PAT_State]," +
                "PATIENT.[PAT_Date_Of_Birth]," +
                "PATIENT.[PAT_Gender]" +
                "FROM [GENERAL PRACTICE]" +
                "INNER JOIN [REFFERING GP] ON [GENERAL PRACTICE].[PRAC_Name] =  [REFFERING GP].[PRAC_Name]" +
                "INNER JOIN PATIENT ON [REFFERING GP].[REFGP_ID_Number] = PATIENT.[REFGP_ID_Number]" +
                " WHERE " +
                "PATIENT.[PAT_ID_Number] =" + Dropdown_1.SelectedValue + ";";
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {
                try
                {
                    Global.connection.Open();
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    //While executing reader the class varibles will be assigned
                    while (reader.Read())
                    {
                        //Assigns all data to a dictionary holding both values
                        mailMergedFields = new Dictionary<string, string>();
                        mailMergedFields.Add("PRAC_Name", reader.GetString(0));
                        mailMergedFields.Add("PRAC_Address", reader.GetString(1));
                        mailMergedFields.Add("PRAC_Suburb", reader.GetString(2));
                        mailMergedFields.Add("PRAC_Post_Code", reader.GetString(3));
                        mailMergedFields.Add("PRAC_State", reader.GetString(4));
                        mailMergedFields.Add("REFGP_First_Name", reader.GetString(5));
                        mailMergedFields.Add("REFGP_Last_Name", reader.GetString(6));
                        mailMergedFields.Add("PAT_First_Name", reader.GetString(7));
                        mailMergedFields.Add("PAT_Last_Name", reader.GetString(8));
                        mailMergedFields.Add("PAT_Address", reader.GetString(9));
                        mailMergedFields.Add("PAT_Suburb", reader.GetString(10));
                        mailMergedFields.Add("PAT_Post_Code", reader.GetString(11));
                        mailMergedFields.Add("PAT_State", reader.GetString(12));
                        DateTime dob = Convert.ToDateTime(reader.GetString(13));
                        mailMergedFields.Add("PAT_Date_of_Birth", dob.ToShortDateString());
                        mailMergedFields.Add("PAT_Gender", reader.GetString(14));

                        //Not used in the SQL Statement above because prior validation to the consultation date
                        //needs to happen before this method is executed 
                        mailMergedFields.Add("CONS_Date", consultationDate);
                    }
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        //Return to initial screen
        private void Back_Click(object sender, EventArgs e)
        {
            this.Hide();
            Initial_Screen initial = new Initial_Screen();
            initial.ShowDialog();
            initial.Focus();
        }

        //Generate initial GP Letter
        private void Initial_Letter_Click(object sender, EventArgs e)
        {
            WriteLetter("Initial Letter.dat");           
        }

        //Populates patients in dropdown list
        private void Dropdown_1_DropDown(object sender, EventArgs e)
        {
            Patient_Database.PopulatePatientDropdown(Dropdown_1);
        }

        //Dropsdown when down key pressed
        private void Dropdown_1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                Dropdown_1.DroppedDown = true;
            }
        }

        //Dropsdown when down key pressed
        private void Dropdown_2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                Dropdown_2.DroppedDown = true;
            }
        }

        //Populates patient details in textboxes
        private void Dropdown_1_SelectionChangeCommitted(object sender, EventArgs e)
        {            
            Patient_Database.patientSelectedGenerateLetter(Dropdown_1, Input_1, Input_2,
                Input_3, Input_4, Input_5, GP_Letter);

            //Gets all initial consultations of patient
            DataTable initialConsultations =
                Consultation_Database.CheckInitialConsultations(Dropdown_1);
            int consCount = initialConsultations.Rows.Count; 

            //Depending on how many initial consultations will depend on the text displayed
            if(consCount == 0)
            {
                Input_6.Text = "No Initial Consultation";
            }
            else if(consCount == 1)
            {
                Input_6.Text = Convert.ToString(initialConsultations.Rows[0]["Display Text"]);
            }
            else if(consCount > 1)
            {
                Input_6.Text = "More than one Initial Consultation";
            }

            //Enables controls to generate letters
            Initial_Letter.Enabled = true;
            Initial_Letter_MHLP.Enabled = true;
            No_Contact_Letter.Enabled = true;
            GP_Letter.Enabled = true;
        }

        //Generate initial GP Letter for MHLP
        private void Initial_Letter_MHLP_Click(object sender, EventArgs e)
        {
            WriteLetter("Initial Letter MHLP.dat");
        }

        //Writes letter and fills mail merge
        private void WriteLetter(string dataFile)
        {
            string templateFileName = null;                 //Holds name of templated file name
            OpenFileDialog ofd = new OpenFileDialog();
            DialogResult result;
            try
            {
                //If data file exists and is read but the template file read doesn't exist then this will prompt user to choose a new document
                templateFileName = Global.readDataFile(dataFile);       //If datafile exists then it will read correctly
                if (File.Exists(templateFileName) == false)
                {
                    MessageBox.Show("Cannot find template word document", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal).ToString();
                    ofd.Filter = "Word Document (*.dotx)|*.dotx| Word Document (*.docx)|*.docx| Word Document 97-03 (*.doc)|*.doc";
                    ofd.Title = "Choose the Initial Letter Template";
                    result = ofd.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        Global.writeDatafile(dataFile, ofd.FileName);
                        templateFileName = ofd.FileName;
                    }
                    else
                    {
                        return;
                    }
                }
            }
            //If datafile not there this exception will catch and prompt the user to choose the templated word doc
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("Cannot find template word document", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal).ToString();
                ofd.Filter = "Templated Word Document (*.dotx)|*.dotx| Word Document (*.docx)|*.docx| Word Document 97-03 (*.doc)|*.doc";
                ofd.Title = "Choose the Initial Letter Template";
                result = ofd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Global.writeDatafile(dataFile, ofd.FileName);
                    templateFileName = ofd.FileName;
                }
                else
                {
                    return;
                }
            }
            //With the file read correctly the SQL method is run to obtain the details of the patient,
            //Reffering GP and Practice to be used in the 
            JoinPatient_GP_Practice();
            var application = new Word.Application();
            var document = application.Documents.Add(templateFileName);
            
            //If the file is not read or a file is not picked from the file dialog
            //the event in stopped

            application.Visible = true;

            //Iterates thrugh each mail merged field
            foreach (Word.Field field in document.Fields)
            {
                //Iterates through dictionary values of mailmerged fileds and writes in document
                foreach (string key in mailMergedFields.Keys)
                {
                    string value = mailMergedFields[key];
                    if (field.Code.Text.Contains(key))
                    {
                        field.Select();
                        application.Selection.TypeText(value);
                        break;          //Breaks the inner loop when found to move to the next field
                    }

                    //There are fileds that need to be changed to his/her if its male and female
                    //This will not fit with the foreach loop above.
                    else if (field.Code.Text.Contains("His/Her"))
                    {
                        field.Select();
                        if (mailMergedFields["PAT_Gender"] == "Male")
                        {
                            application.Selection.TypeText("his");
                            break;      //Breaks the inner loop when found to move to the next field
                        }
                        else if (mailMergedFields["PAT_Gender"] == "Female")
                        {
                            application.Selection.TypeText("her");
                            break;      //Breaks the inner loop when found to move to the next field
                        }
                    }
                    else if (field.Code.Text.Contains("Him/Her"))
                    {
                        field.Select();
                        if (mailMergedFields["PAT_Gender"] == "Male")
                        {
                            application.Selection.TypeText("him");
                            break;      //Breaks the inner loop when found to move to the next field
                        }
                        else if (mailMergedFields["PAT_Gender"] == "Female")
                        {
                            application.Selection.TypeText("her");
                            break;      //Breaks the inner loop when found to move to the next field
                        }
                    }
                }
            }
        }

        //No contant letter is based on the initial consultation.  If thre is more than 1 initial consultation then
        //furethr validation is required
        private void No_Contact_Letter_Click(object sender, EventArgs e)
        {
            //Gets all initial Consultations from patient and stores the id and date in a dictionary
            DataTable initialConsultations = 
                Consultation_Database.CheckInitialConsultations(Dropdown_1);
            if (initialConsultations.Rows.Count == 0)
            {
                MessageBox.Show("There is no initial consultations for This patient.  Please add a new initial " +
                    "consultation to generate this letter", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //If only one consultation then generate letter
            else if (initialConsultations.Rows.Count == 1)
            {
                consultationDate = Convert.ToString(initialConsultations.Rows[0]["Date"]);
                WriteLetter("No Contact.dat");
            }

            //Changing the form to allow user to choose which consultation will be in the letter
            else if (initialConsultations.Rows.Count > 1)
            {
                //Sets trigger so other events know we are choosing the initial consultation
                ChooseInitialConsultation = true;

                //Adjusting form height and setting unwanted objects to visible = false
                this.Height = 190;
                this.Width = 352;
                Sort_Lists.Visible = false;
                label2.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                Input_1.Visible = false;
                Input_2.Visible = false;
                Input_3.Visible = false;

                //Changes the header label
                label1.Visible = false;
                label3.Visible = true;
                label6.Visible = true;

                //Centering the dropdown and moving the back button into view
                Dropdown_1.Visible = false;
                Dropdown_2.Visible = true;
                UserLabel.Location = new Point(20, 133);
                Back.Location = new Point(239, 122);

                //Making visible the accept consultation button and cancel button
                Accept_Consultation.Visible = true;
                Cancel.Visible = true;

                //Changes the datasource of the dropdown2
                Dropdown_2.DataSource = null;
                Dropdown_2.DataSource = initialConsultations;
                Dropdown_2.DisplayMember = "Display Text";
                Dropdown_2.ValueMember = "Date";
            }
        }

        //Cancel button only visible when selecting intial consultation.
        //Clicking will revert to the original form
        private void Cancel_Click(object sender, EventArgs e)
        {
            RevertToOriginalForm();
        }

        //Accepts the iniital consultation and creates the letter
        private void Accept_Consultation_Click(object sender, EventArgs e)
        {
            consultationDate = Dropdown_2.SelectedValue.ToString();
            WriteLetter("No Contact.dat");
            RevertToOriginalForm();
        }

        //Used to revert to original form
        private void RevertToOriginalForm()
        {
            //Sets trigger so other events know we are choosing the initial consultation
            ChooseInitialConsultation = false;
            //Setting form back to how it is initialized
            this.Height = 368;
            this.Width = 431;
            Sort_Lists.Visible = true;
            label2.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            Input_1.Visible = true;
            Input_2.Visible = true;
            Input_3.Visible = true;

            //Changes the header label
            label1.Visible = true;
            label3.Visible = false;
            label6.Visible = false;

            //Centering the dropdown and moving the back button into view
            Dropdown_1.Visible = true;
            Dropdown_2.Visible = false;
            UserLabel.Location = new Point(10, 301);
            Back.Location = new Point(330, 295);

            //Making visible the accept consultation button and cancel button
            Accept_Consultation.Visible = false;
            Cancel.Visible = false;

            //Sets datasource of dropdown2 back to null
            Dropdown_2.DataSource = null;
        }

        //Changes way lists are sorted
        private void Sort_Lists_Click(object sender, EventArgs e)
        {
            if (Global.sortDropdown == Global.ID)
            {
                Global.sortDropdown = Global.NAME;
            }
            else if (Global.sortDropdown == Global.NAME)
            {
                Global.sortDropdown = Global.ID;
            }
            setSortedButtonText();
        }

        //Updates GP Letter Sent field of patient
        private void GP_Letter_Click(object sender, EventArgs e)
        {
            Patient_Database.UpdateGPLetterSent(GP_Letter, Dropdown_1);
        }

        //Closes all forms
        private void Generate_Letters_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Set_Initial_Location_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal).ToString();
            ofd.Filter = "Word Document (*.dotx)|*.dotx| Word Document (*.docx)|*.docx| Word Document 97-03 (*.doc)|*.doc";
            ofd.Title = "Choose the Initial Letter Template";
            DialogResult result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                Global.writeDatafile("Initial Letter.dat", ofd.FileName);
            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }
        }

        private void Set_Initial_MHLP_Location_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal).ToString();
            ofd.Filter = "Word Document (*.dotx)|*.dotx| Word Document (*.docx)|*.docx| Word Document 97-03 (*.doc)|*.doc";
            ofd.Title = "Choose the Initial Letter Template";
            DialogResult result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                Global.writeDatafile("Initial Letter MHLP.dat", ofd.FileName);
            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }
        }

        private void Set_Noc_Contact_Location_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal).ToString();
            ofd.Filter = "Word Document (*.dotx)|*.dotx| Word Document (*.docx)|*.docx| Word Document 97-03 (*.doc)|*.doc";
            ofd.Title = "Choose the Initial Letter Template";
            DialogResult result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                Global.writeDatafile("No Contact.dat", ofd.FileName);
            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }
        }
    }
}
