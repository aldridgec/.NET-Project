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
    public partial class Approval : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {//called on postbacks or initial load
            SQLiteConnection sqlite = new SQLiteConnection(ConfigurationManager.ConnectionStrings["SQLite"].ConnectionString);
            SQLiteDataAdapter ad;
            DataTable dt = new DataTable();
            try
            {//on page load pull the data from the database
                SQLiteCommand cmd;
                sqlite.Open(); // initiate the connection to the db.
                cmd = sqlite.CreateCommand();
                DateTime yesterday = DateTime.Now.AddDays(-1);
                cmd.CommandText = "Select * from LaborRelease WHERE `CreatedDate` < '" + yesterday + "'";
                ad = new SQLiteDataAdapter(cmd);
                ad.Fill(dt); // fill the data source.
                foreach (DataRow row2 in dt.Rows)
                { //for each row in the LaborRelease Table, put it in a table row.
                    string student = "";
                    string creator = "";
                    string postitle = "";
                    string wls = "";
                    string dept = "";
                    DataTable dp = new DataTable();
                    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Tracy"].ConnectionString);
                    con.Open(); // initiate the connection to the db.
                    string sqlQuery = @"Select * from STUDATA WHERE ID = '" + row2["Student"] + "'"; //get the student name
                    SqlCommand cmd2 = new SqlCommand(sqlQuery, con);
                    SqlDataReader rdr = cmd2.ExecuteReader();
                    dp.Load(rdr); // fill the data source.
                    foreach (DataRow row3 in dp.Rows)
                    {//get the student name
                        student = row3["FIRST_NAME"] + " " + row3["LAST_NAME"];
                    }

                    dp.Clear();
                    sqlQuery = "Select * from STUSTAFF WHERE ID = '" + row2["Creator"] + "'"; //staff name
                    cmd2 = new SqlCommand(sqlQuery, con);
                    rdr = cmd2.ExecuteReader();
                    dp.Load(rdr); // fill the data source.
                    foreach (DataRow row3 in dp.Rows)
                    {//get the name of the form creator
                        creator = row3["FIRST_NAME"] + " " + row3["LAST_NAME"];
                    }
                    dp.Clear();
                    sqlQuery = "Select * from STUPOSN WHERE POSN_CODE = '" + row2["Position"] + "'";
                    cmd2 = new SqlCommand(sqlQuery, con);
                    rdr = cmd2.ExecuteReader();
                    dp.Load(rdr); // fill the data source.
                    foreach (DataRow row3 in dp.Rows)
                    {//get the information for the position code
                        postitle = row3["POSN_TITLE"] + "";
                        wls = row3["WLS"] + "";
                        dept = row3["DEPT_NAME"] + "";
                    }
                    //add a row to the datatable
                    row.Controls.Add(new LiteralControl(
                    "<tr><td>" + student + "</td><td>" + row2["Position"] +
                    "</td><td>" + postitle + "</td><td>" + wls + "</td><td>" + dept + "</td><td>" + creator + "</td></tr>"));
                }
            }
            finally
            {
                sqlite.Close(); // close connection.
            }
        }
    }
}