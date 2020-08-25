using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Finisar.SQLite;
using System.Windows.Forms;
using System.Data;

namespace Patient_Database_and_Tracker
{
    class UserDatabase
    {
        //SQL DDL
        //"CREATE TABLE [USERS]" +
        //"(USER_UserId INTEGER DEFAULT 1 PRIMARY KEY," +
        //"USER_First_Name TEXT NOT NULL," +
        //"USER_Last_Name TEXT NOT NULL," +
        //"USER_Username TEXT NOT NULL," +
        //"USER_Password TEXT NOT NULL," +
        //"USER_Secret_Question NOT NULL," +
        //"USER_Secret_Answer NOT NULL," +
        //"USER_Master_User BOOLEAN NOT NULL);";

        //Saves new user
        public static bool AddUser(string firstName, string lastName, string username, string password, string question, string answer)
        {
            // This function will add a user to our database

            string hashedPassword = Security.HashSHA1(password);        //Hashes password
            string query = "INSERT INTO [USERS](USER_First_Name, USER_Last_Name, USER_username, USER_password, USER_Secret_Question, USER_Secret_Answer, USER_Master_User)" +
                "VALUES(@firstName, @lastName, @username, @password, @question, @answer, @masterUser);";    //SQL Query
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {
                try
                {
                    cmd.Parameters.Add("@firstName", firstName);
                    cmd.Parameters.Add("@lastName", lastName);
                    cmd.Parameters.Add("@username", username);
                    cmd.Parameters.Add("@password", hashedPassword);
                    cmd.Parameters.Add("@question", question);
                    cmd.Parameters.Add("@answer", answer);
                    cmd.Parameters.Add("@masterUser", 0);       //When added users are defaultyed to non master users

                    Global.connection.Open();
                    cmd.ExecuteNonQuery();
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return true;
        }

        //Gets unique userID when user logs in
        public static int GetUserIdByUsernameAndPassword(string username, string password)
        {
            // this is the value we will return
            int userId = 0;

            string query = "SELECT USER_UserId, USER_Password, USER_First_Name, USER_Last_Name FROM [USERS] WHERE USER_Username = @username;";
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {
                cmd.Parameters.Add("@username", username);
                Global.connection.Open();
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    int dbUserId = Convert.ToInt32(reader["USER_UserId"]);
                    string dbPassword = Convert.ToString(reader["USER_Password"]);

                    // Now we hash the UserGuid from the database with the password we wan't to check
                    // In the same way as when we saved it to the database in the first place. (see AddUser() function)
                    string hashedPassword = Security.HashSHA1(password);

                    // if its correct password the result of the hash is the same as in the database
                    if (dbPassword == hashedPassword)
                    {
                        // The password is correct
                        userId = dbUserId;
                        Global.userFirstName = Convert.ToString(reader["USER_First_Name"]);
                        Global.userLastName = Convert.ToString(reader["USER_Last_Name"]);
                        break;
                    }
                }
                Global.connection.Close();
            }
            return userId;
        }

        //Used in the Add_edit_Consultation_2 for consultants
        public static DataTable PopulateUserDropdown(ComboBox userDropDownList)
        {
            DataTable userdt = new DataTable();
            string query = "SELECT USER_First_Name, USER_Last_Name FROM [USERS] WHERE USER_UserId NOT IN (1);";

            //Connects to database and runs query
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
            {
                try
                {
                    Global.connection.Open();
                    userdt.Columns.Add("Display Text");
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    //The dropdownbox will show the ID and the consulatants name
                    while (reader.Read())
                    {
                        string displayText = reader.GetString(0) + " " + reader.GetString(1);
                        userdt.Rows.Add(displayText);
                    }
                    userDropDownList.DataSource = userdt;
                    userDropDownList.ValueMember = "Display Text";
                    userDropDownList.DisplayMember = "Display Text";
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return userdt;
        }

        //Used to reset forgotten passowrd
        public static string ResetForgottenPassword(string username)
        {
            //Passowrd is reset to the first 8 letters of a GUID
            Guid guidPassword = Guid.NewGuid();
            string newPassword = guidPassword.ToString();
            newPassword = newPassword.Substring(0, 8);
            string newHashedPassword = Security.HashSHA1(newPassword);      //Hashes the GUID to protect passowrd

            string query = "UPDATE [USERS] SET USER_Password ='" + newHashedPassword + "' WHERE USER_Username ='" + username + "';";    //SQL query
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {
                try
                {
                    Global.connection.Open();
                    cmd.ExecuteNonQuery();
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return newPassword;
        }

        //User restes passowrd
        public static bool ResetPassword(string username, string newHashedPassword)
        {
            bool success = false;
            string query = "UPDATE [USERS] SET USER_Password ='" + newHashedPassword + "' WHERE USER_Username ='" + username + "';";
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {
                try
                {
                    Global.connection.Open();
                    cmd.ExecuteNonQuery();
                    success = true;
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return success;
        }

        //Used to update user details
        public static bool UpdateUser(string firstName, string lastName, string username, string question, string answer)
        {
            // This function will update a user to our database
            string query = "UPDATE [USERS] SET USER_First_Name = @firstName, USER_Last_Name = @lastName," +
                "USER_username = @username, USER_Secret_Question = @question," +
                "USER_Secret_Answer = @answer WHERE USER_UserId = '" + Global.userId + "';";
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {
                try
                {
                    cmd.Parameters.Add("@firstName", firstName);
                    cmd.Parameters.Add("@lastName", lastName);
                    cmd.Parameters.Add("@username", username);
                    cmd.Parameters.Add("@question", question);
                    cmd.Parameters.Add("@answer", answer);

                    Global.connection.Open();
                    cmd.ExecuteNonQuery();
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return true;
        }

        //Updates the USER_Master_User field of a user
        public static bool UpdateMasterUser(int userId, int masterUser)
        {
            // This function will update a user to our database
            string query = "UPDATE [USERS] SET USER_Master_User = @master WHERE USER_UserId = '" + userId + "';";
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {
                try
                {
                    // Add the input as parameters to avoid sql-injections
                    // I'll explain later in this article.
                    cmd.Parameters.Add("@master", masterUser);

                    Global.connection.Open();
                    cmd.ExecuteNonQuery();
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return true;
        }

        public static void LoadSecretQuestion(Form currentForm, TextBox usernameTB, TextBox questionTB, TextBox answerTB,
            Button submitButton1, Button submitButton2)
        {
            bool usernameMatch = false;             //Sets trigger for username found

            //Holds question and username in SQL Database
            string question = null;
            string username = null;

            //Cannot reset master iusername from this method
            if (usernameTB.Text == "MASTER")
            {
                MessageBox.Show("You Can Not Reset Master User From Here", "Error",
                     MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //SQL Query
            string query = "SELECT USER_Username, USER_Secret_Question, USER_Secret_Answer FROM [USERS];";
            using (Global.connection = new SQLiteConnection(Global.connectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, Global.connection))
            {
                Global.connection.Open();
                SQLiteDataReader reader = cmd.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        username = Convert.ToString(reader["USER_Username"]);

                        if (username == usernameTB.Text)
                        {
                            username = Convert.ToString(reader["USER_Username"]);
                            question = Convert.ToString(reader["USER_Secret_Question"]);
                            Global.resetPasswordSecretAnswer = Convert.ToString(reader["USER_Secret_Answer"]);
                            usernameMatch = true;
                            break;
                        }
                    }
                    //Error message if username is not found in database
                    if (usernameMatch == false)
                    {
                        MessageBox.Show("Incorrect UserName", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Global.connection.Close();
                        return;
                    }

                    //Updating new password and emailing to users email
                    else
                    {
                        reader.Dispose();
                        usernameTB.ReadOnly = true;
                        currentForm.Height = 234;
                        questionTB.Text = question;
                        submitButton1.Visible = false;
                        submitButton2.Visible = true;
                        answerTB.Focus();
                    }
                    Global.connection.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }

    //This class is used to hash passwords.
    //Hashing passwords protect against hackers stealing passowrds
    class Security
    {
        public static string HashSHA1(string value)
        {
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var inputBytes = Encoding.ASCII.GetBytes(value);
            var hash = sha1.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
