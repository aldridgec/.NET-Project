using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Configuration;
using System.Linq;
using System.Data.SQLite;
using System.Web.Mail;

namespace BereaLaborForms.LaborStatusForm
{
    public partial class Form : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {//if the page is not being refreshed from a form action
                SetInvisible();
                StudentForm();
                StaffForm();
                DepartmentForm();
                TermForm();
            }
            else
            { //if the page is being refreshed, decide what extra form needs to be shown, if any
                MakeVisible();
            }
        }
        protected void SetInvisible()
        { //gets called if the page is not a postback, turning the visibility of all forms not needed off until it is time for them to be used.
            hidden.Visible = false;
            Staff.Visible = true;
            Department.Visible = true;
            Position.Visible = false;
            Term.Visible = false;
            Primary.Visible = false;
            Hour.Visible = false;
            SupervisorText.Visible = true;
            DepartmentText.Visible = true;
            PositionText.Visible = false;
            TermText.Visible = false;
            PrimaryText.Visible = false;
            HourText.Visible = false;
            HourText2.Visible = false;
            submit.Visible = false;
            PrimSuperText.Visible = false;
            PrimarySuper.Visible = false;
        }
        protected void StudentForm()
        {//generates a list of students to display in the select menu
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Tracy"].ConnectionString);
            DataTable dt = new DataTable(); //open the connection to the sql as well as a datatable container
            try
            {
                con.Open(); // initiate the connection to the db.
                string sqlQuery = @"SELECT * from STUDATA ORDER BY LAST_NAME";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                SqlDataReader rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                foreach (DataRow row3 in dt.Rows)
                {
                    Student.Items.Add(new ListItem(row3["ID"].ToString() + ", " + row3["FIRST_NAME"].ToString() + " " + row3["LAST_NAME"].ToString(), row3["ID"].ToString()));
                }
            }
            finally
            {
                con.Close();
            }
        }
        protected void StaffForm()
        { //generate a list of staffmembers to select for the labor status form.
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Tracy"].ConnectionString);
            DataTable dt = new DataTable(); //open the connection to the sql as well as a datatable container
            try
            {
                con.Open(); // initiate the connection to the db.
                string sqlQuery = @"SELECT * from STUSTAFF ORDER BY LAST_NAME";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                SqlDataReader rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                foreach (DataRow row3 in dt.Rows)
                {
                    Staff.Items.Add(new ListItem(row3["ID"].ToString() + ", " + row3["FIRST_NAME"].ToString() + " " + row3["LAST_NAME"].ToString(), row3["ID"].ToString()));
                    PrimarySuper.Items.Add(new ListItem(row3["ID"].ToString() + ", " + row3["FIRST_NAME"].ToString() + " " + row3["LAST_NAME"].ToString(), row3["ID"].ToString()));
                }
            }
            finally
            {
                con.Close();
            }
        }
        protected void DepartmentForm()
        {//generate a list of departments which then selects positions
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Tracy"].ConnectionString);
            DataTable dt = new DataTable(); //open the connection to the sql as well as a datatable container
            try
            {
                con.Open(); // initiate the connection to the db.
                string sqlQuery = @"SELECT DEPT_NAME from STUPOSN Group By DEPT_NAME ORDER BY DEPT_NAME";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                SqlDataReader rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                foreach (DataRow row3 in dt.Rows)
                {
                    Department.Items.Add(new ListItem(row3["DEPT_NAME"].ToString(), row3["DEPT_NAME"].ToString()));
                }
            }
            finally
            {
                con.Close();
            }
        }
        protected void PositionForm()
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Tracy"].ConnectionString);
            DataTable dt = new DataTable();//connect to the database and create a datatable to hold the data we receive back from the query
            try
            {
                Position.Items.Clear();
                Position.Items.Add(new ListItem(""));
                con.Open(); // initiate the connection to the db.
                string sqlQuery = @"Select * from STUPOSN WHERE DEPT_NAME='" + Department.SelectedValue + "'";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                SqlDataReader rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                foreach (DataRow row in dt.Rows)
                { //insert the data gotten from the query into the list of positions to choose from.
                    Position.Items.Add(new ListItem(row["POSN_CODE"].ToString() + ", " + row["POSN_TITLE"].ToString() + ", WLS " + row["WLS"].ToString(), row["POSN_CODE"].ToString()));
                }
            }
            finally
            {
                con.Close(); // close connection.
            }
        }
        protected void MakeVisible()
        {
            if (Student.SelectedValue != "")
            {//if a student is selected, allow a supervisor to be selected
                Staff.Visible = true;
                SupervisorText.Visible = true;
            }
            if (Staff.SelectedValue != "")
            {//if a staff is selected, allow a department to be selected
                DepartmentText.Visible = true;
                Department.Visible = true;
            }
            if (Department.SelectedValue != "")
            {//if a department is selected, allow a position and term to be selected
                Position.Visible = true;
                Term.Visible = true;
                PositionText.Visible = true;
                TermText.Visible = true;
            }
            if (Term.SelectedValue != "")
            { //if a term is selected, allow a primary/secondary and hour to be selected
                Primary.Visible = true;
                PrimaryText.Visible = true;
                Hour.Visible = true;
                if ((Term.SelectedValue.Contains("Summer")) || (Term.SelectedValue.Contains("Break")))
                {//if summer or break, change the text shown above Hours per Day/Hours Per Week.
                    HourText.Visible = false;
                    HourText2.Visible = true;
                }
                else
                {
                    HourText2.Visible = false;
                    HourText.Visible = true;
                }
            }
            if (((((((Department.SelectedValue != "") && (Staff.SelectedValue != "")) && (Student.SelectedValue != "")) &&
                (Position.SelectedValue != "")) && (Term.SelectedValue != "")) && (Primary.SelectedValue != "")) && (Hour.SelectedValue != ""))
            {//if everything is submitted, show the submit button.
                submit.Visible = true;
            }
            if (hidden.Text != Department.SelectedValue)
            {//if the department has changed, regenerate the position list.
                PositionForm();
            }
            if (Primary.SelectedValue == "Primary")
            {//if Primary, disable the Primary Supervisor List
                PrimarySuper.Visible = false;
                PrimSuperText.Visible = false;
            }
            else if (Primary.SelectedValue == "Secondary")
            {//if Secondary, enable the Primary Supervisor List.
                PrimarySuper.Visible = true;
                PrimSuperText.Visible = true;
            }
            hidden.Text = Department.SelectedValue;
            if ((Term.SelectedValue.Contains("Summer")) || (Term.SelectedValue.Contains("Break")))
            {//if it is Summer or a Break, change the hours shown.
                if (!Hour.Items.Contains(new ListItem("2")))
                {//if the hours are already populated, clear them, unless summer/break was already shown.
                    Hour.Items.Clear();
                    Hour.Items.Add(new ListItem(""));
                    Hour.Items.Add(new ListItem("2"));
                    Hour.Items.Add(new ListItem("4"));
                    Hour.Items.Add(new ListItem("6"));
                    Hour.Items.Add(new ListItem("8"));
                }
            }
            else if (Primary.SelectedValue == "Primary")
            {//if primary, regenerate the list of hours if 12 isn't currently an option (Summer/Secondary).
                if (!Hour.Items.Contains(new ListItem("12")))
                {
                    Hour.Items.Clear();
                    Hour.Items.Add(new ListItem(""));
                    Hour.Items.Add(new ListItem("10", "10"));
                    Hour.Items.Add(new ListItem("12", "12"));
                    Hour.Items.Add(new ListItem("15", "15"));
                }
            }
            else if (Primary.SelectedValue == "Secondary")
            {//if secondary, regenerate the list of hours if 5 isn't currently an option (Summer/Primary).
                if (!Hour.Items.Contains(new ListItem("5")))
                {
                    Hour.Items.Clear();
                    Hour.Items.Add(new ListItem(""));
                    Hour.Items.Add(new ListItem("5", "5"));
                    Hour.Items.Add(new ListItem("10", "10"));
                }
            }
        }
        protected void submit_Click(object sender, EventArgs e)
        {
            int Error = 0;
            int count = 0;
            if (((((((Department.SelectedValue != "") && (Staff.SelectedValue != "")) && (Student.SelectedValue != "")) &&
                (Position.SelectedValue != "")) && (Term.SelectedValue != "")) && (Primary.SelectedValue != "")) && (Hour.SelectedValue != ""))
            {//if everything is non-null (selected a value)
                if ((Primary.SelectedValue == "Secondary") && (PrimarySuper.SelectedValue == ""))
                {  //if secondary is selected and no supervisor has been chosen, show an error.
                    Response.Write("<script>alert('The Form is missing data. Fill all of the data out before submitting.');</script>");
                }
                else
                { //if everything was submitted and no errors:   
                    SQLiteConnection sqlite = new SQLiteConnection(ConfigurationManager.ConnectionStrings["SQLite"].ConnectionString);
                    DataTable dt = new DataTable(); //open the connection and create a datatable to store the information in.
                    try
                    {
                        if (Primary.SelectedValue == "Primary")
                        {
                            SQLiteDataAdapter ad;
                            DataTable dv = new DataTable();//open the sql connection and create a data table to store the information in
                            SQLiteCommand cmd;
                            sqlite.Open(); // initiate the connection to the db.
                            cmd = sqlite.CreateCommand();
                            cmd.CommandText = "Select * from LaborStatusForm WHERE `Supervisee`='" + Student.SelectedValue + "' AND `JobType`='Primary' AND `Term`='" + Term.SelectedValue + "'"; //our sql command to grab all Labor Status Forms.
                            ad = new SQLiteDataAdapter(cmd);
                            ad.Fill(dv); // fill the data source.
                            foreach (DataRow row2 in dv.Rows)
                            {
                                count++;
                            }
                            sqlite.Close(); // initiate the connection to the db.
                        }
                        if (count == 0)
                        {
                            SQLiteDataAdapter ad;
                            DataTable dv = new DataTable();
                            /*string student = "";
                            string staff = "";
                            string postitle = "";
                            string wls = ""; */
                            int max = 0;
                            sqlite.Open();
                            SQLiteCommand cmd;
                            cmd = sqlite.CreateCommand();
                            cmd.CommandText = "Select MAX(Form_ID) from LaborStatusForm";
                            ad = new SQLiteDataAdapter(cmd);
                            ad.Fill(dv); // fill the data source.
                            foreach (DataRow row2 in dv.Rows)
                            {
                                if (!(row2["MAX(Form_ID)"] is DBNull))
                                    max = Convert.ToInt32(row2["MAX(Form_ID)"]);
                                else
                                    max = 0;
                            }
                            max = max + 1;
                            cmd = sqlite.CreateCommand();
                            cmd.CommandText = "INSERT INTO LaborStatusForm VALUES ('" + max + "','" + PrimarySuper.SelectedValue + "', '" + DateTime.Now + "','" + Primary.SelectedValue + "','" + Student.SelectedValue + "','"
                                + Staff.SelectedValue + "','" + Staff.SelectedValue + "','" + Term.SelectedValue + "','" + Position.SelectedValue + "','"
                                + Hour.SelectedValue + "');";
                            cmd.ExecuteNonQuery();//Insert all data in the form into the LaborStatusForm table.
                            cmd.CommandText = "INSERT INTO PastForms VALUES ('" + Student.SelectedValue + "','" + Staff.SelectedValue + "','" + Position.SelectedValue + "','Labor Status Form','" + DateTime.Now + "');";
                            cmd.ExecuteNonQuery();//put a copy of the Labor Status Form in PastForms.
                            /*SQLiteDataAdapter ae;
                            DataTable dp = new DataTable();
                            SQLiteCommand cmd2;
                            //Get the student name
                            cmd2 = sqlite.CreateCommand();
                            cmd2.CommandText = "Select * from Student WHERE StudentID = '" + Student.SelectedValue + "' LIMIT 1"; 
                            ae = new SQLiteDataAdapter(cmd2);
                            ae.Fill(dp); // fill the data source.
                            foreach (DataRow row3 in dp.Rows)
                            {//student name
                                student = row3["FirstName"] + " " + row3["LastName"];
                            }
                            //get the staff name
                            cmd2.CommandText = "Select * from Staff WHERE StaffID = '" + Staff.SelectedValue + "'";
                            ae = new SQLiteDataAdapter(cmd2);
                            ae.Fill(dp); // fill the data source.
                            foreach (DataRow row3 in dp.Rows)
                            {//staff name
                                staff = row3["FirstName"] + " " + row3["LastName"];
                            }
                            cmd2.CommandText = "Select * from Position WHERE PositionCode = '" + Position.SelectedValue + "'";
                            ae = new SQLiteDataAdapter(cmd2);
                            ae.Fill(dp); // fill the data source.
                            foreach (DataRow row3 in dp.Rows)
                            {//position and wls
                                postitle = row3["PositionTitle"] + "";
                                wls = row3["WLS"] + "";
                            }
                            //format the email and send it.
                            string from = "laborprogram@berea.edu";
                            string to = "aldridgec@berea.edu";
                            string subject = "Your Labor Status Form has been Submitted";
                            string body = "Dear " + Student + ", \n A Labor Status Form has been submitted for you. \n" +
                            "Student: " + student + "\n" +
                            "Supervisor: " + staff + "\n" +
                            "Position Title:" + postitle + "\n" +
                            "WLS:" + wls + "\n" +
                            "Term:" + Term.SelectedValue + "\n" +
                            "Primary/Seconary:" + Primary.SelectedValue + "\n" +
                            "Hours:" + Hour.SelectedValue + "\n" +
                            "\n If this is not the position you agreed to work, please contact the Labor Program Office within the next 24 hours.";
                            // SmtpMail.SmtpServer = "mail.berea.edu";
                            // SmtpMail.Send(from, to, subject, body); */
                        }
                        else
                        {
                            Error = 1;
                        }
                    }
                    finally
                    {
                        sqlite.Close(); // close connection.
                        if (Error > 0)
                        {
                            Response.Write("<script>alert('Another Supervisor has already filed for a primary position on this student for this term.');</script>");
                        }
                        else
                            Response.Redirect("Complete.aspx"); //redirect to the complete page if the form is done.
                    }
                }
            }
            else
            {//show an error if not all data is given.
                Response.Write("<script>alert('The Form is missing data. Fill all of the data out before submitting.');</script>");
            }
        }
        protected void TermForm()
        {//generate a list of terms
            SQLiteConnection sqlite = new SQLiteConnection(ConfigurationManager.ConnectionStrings["SQLite"].ConnectionString);
            SQLiteDataAdapter ad;
            DataTable dt = new DataTable(); //open the connection and a data table to store the terms in.
            try
            {
                SQLiteCommand cmd;
                sqlite.Open(); // initiate the connection to the db.
                cmd = sqlite.CreateCommand();
                cmd.CommandText = "Select * from Semesters";//our sql query to grab all terms
                ad = new SQLiteDataAdapter(cmd);
                ad.Fill(dt); // fill the data source.
                foreach (DataRow row in dt.Rows)
                {//add the terms to the terms drop down menu.
                    Term.Items.Add(new ListItem(row["Name"].ToString()));
                }
            }
            finally
            {
                sqlite.Close(); // close connection.
            }
        }
    }
}