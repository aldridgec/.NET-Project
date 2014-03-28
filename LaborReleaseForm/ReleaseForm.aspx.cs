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

namespace BereaLaborForms.LaborReleaseForm
{
    public partial class ReleaseForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {//if the page is not a refresh
                hidden.Visible = false;//keep the hidden field hidden
                SQLiteConnection sqlite = new SQLiteConnection(ConfigurationManager.ConnectionStrings["SQLite"].ConnectionString);
                SQLiteDataAdapter ad;
                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Tracy"].ConnectionString);
                DataTable dt = new DataTable(); //open a new sql connection and a datatable to store its' info in.
                try
                {
                    con.Open(); // initiate the connection to the db.
                    string sqlQuery = @"SELECT * from STUDATA ORDER BY LAST_NAME";
                    SqlCommand cmd = new SqlCommand(sqlQuery, con);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    dt.Load(rdr);
                    foreach (DataRow row in dt.Rows)
                    {//add the students to the drop down menu
                        Student.Items.Add(new ListItem("B00" + row["ID"].ToString() + ", " + row["FIRST_NAME"].ToString() + " " + row["LAST_NAME"].ToString(), row["ID"].ToString()));
                    }
                }
                finally
                {
                    con.Close(); // close connection.
                }
                dt = new DataTable();
                try
                {
                    con.Open(); // initiate the connection to the db.
                    string sqlQuery = @"SELECT DEPT_NAME from STUPOSN GROUP BY DEPT_NAME ORDER BY DEPT_NAME";
                    SqlCommand cmd = new SqlCommand(sqlQuery, con);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    dt.Load(rdr);
                    foreach (DataRow row in dt.Rows)
                    {//add departments to the department drop down list.
                        Department.Items.Add(new ListItem(row["DEPT_NAME"].ToString(), row["DEPT_NAME"].ToString()));
                    }
                }
                finally
                {
                    con.Close(); // close connection.
                }
            }
            if (hidden.Text != Department.SelectedValue)
            {   //check to see if the department has changed since the last postback.
                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Tracy"].ConnectionString);
                DataTable dt = new DataTable();
                try
                {
                    Position.Items.Clear(); //clear all positions
                    Position.Items.Add(new ListItem("")); //add a new placeholder
                    con.Open(); // initiate the connection to the db.
                    string sqlQuery = @"Select * from STUPOSN WHERE DEPT_NAME='" + Department.SelectedValue + "'";
                    SqlCommand cmd = new SqlCommand(sqlQuery, con);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    dt.Load(rdr);
                    foreach (DataRow row3 in dt.Rows)
                    {
                        Position.Items.Add(new ListItem(row3["POSN_CODE"].ToString() + ", " + row3["POSN_TITLE"].ToString() + ", WLS " + row3["WLS"].ToString(), row3["POSN_CODE"].ToString()));
                    }
                }
                finally
                {
                    con.Close(); // close connection.
                }
                hidden.Text = Department.SelectedValue; //save the new department in our postback-proof variable.
            }
        }
        protected void submit_Click(object sender, EventArgs e)
        {//if submit was clicked
            if ((((Student.SelectedValue != "") && (Position.SelectedValue != "")) && (Department.SelectedValue != "")))
            {//make sure all information was filled out
                SQLiteConnection sqlite = new SQLiteConnection(ConfigurationManager.ConnectionStrings["SQLite"].ConnectionString);
                DataTable dt = new DataTable();
                try
                {
                    SQLiteCommand cmd;
                    sqlite.Open(); // initiate the connection to the db.
                    cmd = sqlite.CreateCommand();
                    cmd.CommandText = "INSERT INTO LaborRelease VALUES ('" + DateTime.Now + "','666666','" + Student.SelectedValue + "','" +
                        Satisfactory.SelectedValue + "','" + Primary.Text + "','" + Position.SelectedValue + "');";
                    cmd.ExecuteNonQuery();//insert the Labor Release info in LaborRelease
                    cmd.CommandText = "INSERT INTO PastForms VALUES ('" + Student.SelectedValue + "','','" + Position.SelectedValue + "','Labor Release Form','" + DateTime.Now + "');";
                    cmd.ExecuteNonQuery();//insert the Labor Release info in PastForms
                }
                finally
                {
                    sqlite.Close(); // close connection.
                    Response.Redirect("LaborStatusForm/Complete.aspx"); //Redirect to the generic Complete page
                }
            }
            else
            {//show an error if all fields are not filled out.
                Response.Write("<script>alert('The Form is missing data. Fill all of the data out before submitting.');</script>");
            }
        }
    }
}