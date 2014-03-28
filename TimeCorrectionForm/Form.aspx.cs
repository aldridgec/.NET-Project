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

namespace BereaLaborForms.TimeCorrectionForm
{
    public partial class Form : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dp = new DataTable();
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Tracy"].ConnectionString);

            if (!Page.IsPostBack)
            {//if the page is not a postback
                hidden.Visible = false; //set the hidden variable visibility to false
                con.Open(); // initiate the connection to the db.
                try
                {
                    string SqlQuery = @"Select * From STUDATA ORDER BY LAST_NAME";
                    SqlCommand cmd = new SqlCommand(SqlQuery, con);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    dp.Load(rdr);
                    foreach (DataRow row in dp.Rows)
                    {//add the student to the student list.
                        Student.Items.Add(new ListItem(row["ID"].ToString() + ", " + row["FIRST_NAME"].ToString() + " " + row["LAST_NAME"].ToString(), row["ID"].ToString()));
                    }
                }
                finally
                {
                    con.Close(); // close connection.
                }

                dp = new DataTable();
                try
                {
                    con.Open(); // initiate the connection to the db.
                    string sqlQuery = @"SELECT DEPT_NAME from STUPOSN Group By DEPT_NAME ORDER BY DEPT_NAME";
                    SqlCommand cmd = new SqlCommand(sqlQuery, con);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    dp.Load(rdr);
                    foreach (DataRow row in dp.Rows)
                    {//add the departments to the list
                        Department.Items.Add(new ListItem(row["DEPT_NAME"].ToString(), row["DEPT_NAME"].ToString()));
                    }
                }
                finally
                {
                    con.Close(); // close connection.
                }
            }
            if (hidden.Text != Department.SelectedValue)
            {//if the department has changed
                dp = new DataTable();//open a connection and datatable to store information in
                try
                {
                    Position.Items.Clear(); //delete all from the position List
                    Position.Items.Add(new ListItem("")); //add placeholder to the position list.
                    con.Open(); // initiate the connection to the db.
                    string sqlQuery = @"Select * from STUPOSN WHERE DEPT_NAME='" + Department.SelectedValue + "'";
                    SqlCommand cmd = new SqlCommand(sqlQuery, con);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    dp.Load(rdr);
                    foreach (DataRow row in dp.Rows)
                    {//generate the list of positions based on departments
                        Position.Items.Add(new ListItem(row["POSN_CODE"].ToString() + ", " + row["POSN_TITLE"].ToString() + ", WLS " + row["WLS"].ToString(), row["POSN_CODE"].ToString()));
                    }
                }
                finally
                {
                    con.Close(); // close connection.
                }
                hidden.Text = Department.SelectedValue;
            }
        }
        protected void submit_Click(object sender, EventArgs e)
        {
            if ((((((Student.SelectedValue != "") && (Position.SelectedValue != "")) && (Department.SelectedValue != ""))
                && (Hours.Text != "")) && (Explain.Text != "")))
            {//if all data is submitted as not-null.
                SQLiteConnection sqlite = new SQLiteConnection(ConfigurationManager.ConnectionStrings["SQLite"].ConnectionString);//open the connection 
                try
                {
                    SQLiteCommand cmd;
                    sqlite.Open(); // initiate the connection to the db.
                    cmd = sqlite.CreateCommand();
                    cmd.CommandText = "INSERT INTO CorrectedTime VALUES('" + DateTime.Now + "','666666','" + Student.SelectedValue + "','" +
                        Position.SelectedValue + "','" + Hours.Text + "','" + Explain.Text + "');";
                    cmd.ExecuteNonQuery();//insert into CorrectedTime
                    cmd.CommandText = "INSERT INTO PastForms VALUES ('" + Student.SelectedValue + "','','" + Position.SelectedValue + "','UnrecordedTime','" + DateTime.Now + "');";
                    cmd.ExecuteNonQuery();//insert into PastForms
                }
                finally
                {
                    sqlite.Close(); // close connection.
                    Response.Redirect("LaborStatusForm/Complete.aspx"); //redirect to complete page
                }
            }
            else
            {
                Response.Write("<script>alert('The Form is missing data. Fill all of the data out before submitting.');</script>");
            }
        }
    }
}