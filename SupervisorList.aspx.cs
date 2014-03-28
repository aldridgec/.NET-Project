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

namespace BereaLaborForms
{
    public partial class SupervisorList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Tracy"].ConnectionString);
            SQLiteConnection sqlite = new SQLiteConnection(ConfigurationManager.ConnectionStrings["SQLite"].ConnectionString);
            DataTable dt = new DataTable();
            try
            {
                con.Open(); // initiate the connection to the db.
                string sqlQuery = @"Select * from STUSTAFF ORDER BY LAST_NAME";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                SqlDataReader rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                //temporary solution til login is active
                if (!Page.IsPostBack)
                {//if page is not a refresh
                    foreach (DataRow row in dt.Rows)
                    {//add all supervisors to a drop down list
                        Super.Items.Add(new ListItem(row["ID"].ToString() + ", " + row["FIRST_NAME"].ToString() + " " + row["LAST_NAME"].ToString(), row["ID"].ToString()));
                    }
                }
                con.Close(); // close connection.
            }
            finally
            {
                con.Close(); // close connection.
            }
            dt = new DataTable();
            try
            {//on page load pull the data from the database
                SQLiteCommand cmd;
                SQLiteDataAdapter ad;
                sqlite.Open(); // initiate the connection to the db.
                cmd = sqlite.CreateCommand();
                cmd.CommandText = "Select * from LaborStatusForm WHERE Supervisor ='" + Super.SelectedValue + "' OR Creator='" + Super.SelectedValue + "' OR PrimarySupervisor='" + Super.SelectedValue + "'";
                ad = new SQLiteDataAdapter(cmd);
                con.Open(); // initiate the connection to the db.
                ad.Fill(dt); // fill the data source.
                foreach (DataRow row2 in dt.Rows)
                { //for each row in the LaborRelease Table, put it in a table row.
                    string student = "";
                    string staff = "";
                    string postitle = "";
                    string wls = "";
                    DataTable dp = new DataTable();
                    string sqlQuery = "Select * from STUDATA WHERE ID = '" + row2["Supervisee"] + "'"; //get the student name
                    SqlCommand cmd2 = new SqlCommand(sqlQuery, con);
                    SqlDataReader rdr = cmd2.ExecuteReader();
                    dp.Load(rdr); // fill the data source.
                    foreach (DataRow row3 in dp.Rows)
                    {//student name
                        student = row3["FIRST_NAME"] + " " + row3["LAST_NAME"];
                    }
                    dp = new DataTable();
                    sqlQuery = "Select * from STUSTAFF WHERE ID = '" + row2["Supervisor"] + "'";
                    cmd2 = new SqlCommand(sqlQuery, con);
                    rdr = cmd2.ExecuteReader();
                    dp.Load(rdr);
                    foreach (DataRow row3 in dp.Rows)
                    { //Staff Name
                        staff = row3["FIRST_NAME"] + " " + row3["LAST_NAME"];
                    }
                    dp = new DataTable();
                    sqlQuery = "Select * from STUPOSN WHERE POSN_CODE = '" + row2["Positionc"] + "'";
                    cmd2 = new SqlCommand(sqlQuery, con);
                    rdr = cmd2.ExecuteReader();
                    dp.Load(rdr);
                    foreach (DataRow row3 in dp.Rows)
                    {//for the positioncode in the Labor Status Form
                        postitle = row3["POSN_TITLE"] + "";
                        wls = row3["WLS"] + "";
                    }
                    //add a new row to the datatable

                    row.Controls.Add(new LiteralControl(
                    "<tr><td>" + student + "</td><td>" + staff + "</td><td>" + postitle + "</td><td>" + wls + "</td><td>" + row2["Hour"] + "</td><td>" + row2["Term"] + "</td><td>" + row2["CreatedDate"] + "</td><td> <a href=\"Delete2.aspx/?StudentID=" + row2["Supervisee"] +
                    "&StaffID=" + row2["Supervisor"] + "&Term=" + row2["Term"] + "\">Delete</a></td></tr>"));
                }
            }
            finally
            {
                sqlite.Close();
                con.Close(); // close connection.
            }
        }
    }
}