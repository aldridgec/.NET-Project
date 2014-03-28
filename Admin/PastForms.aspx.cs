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

namespace BereaLaborForms.Admin
{
    public partial class PastForms : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SQLiteConnection sqlite = new SQLiteConnection(ConfigurationManager.ConnectionStrings["SQLite"].ConnectionString);
            SQLiteDataAdapter ad;
            DataTable dt = new DataTable();
            try
            {//on page load pull the data from the database
                SQLiteCommand cmd;
                sqlite.Open(); // initiate the connection to the db.
                cmd = sqlite.CreateCommand();
                cmd.CommandText = "Select * from PastForms";
                ad = new SQLiteDataAdapter(cmd);
                ad.Fill(dt); // fill the data source.
                foreach (DataRow row2 in dt.Rows)
                { //for each row in the LaborRelease Table, put it in a table row.
                    string student = "";
                    string staff = "";
                    string postitle = "";
                    string wls = "";
                    DataTable dp = new DataTable();
                    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Tracy"].ConnectionString);
                    con.Open(); // initiate the connection to the db.
                    string sqlQuery = @"Select * from STUDATA WHERE ID = '" + row2["Student"] + "'"; //get the student name
                    SqlCommand cmd2 = new SqlCommand(sqlQuery, con);
                    SqlDataReader rdr = cmd2.ExecuteReader();
                    dp.Load(rdr); // fill the data source.
                    foreach (DataRow row3 in dp.Rows)
                    {//student name
                        student = row3["FIRST_NAME"] + " " + row3["LAST_NAME"] + "";
                    }
                    dp = new DataTable();
                    sqlQuery = @"Select * from STUSTAFF WHERE ID = '" + row2["Staff"] + "'"; //get the student name
                    cmd2 = new SqlCommand(sqlQuery, con);
                    rdr = cmd2.ExecuteReader();
                    dp.Load(rdr); // fill the data source.
                    foreach (DataRow row3 in dp.Rows)
                    {//staff name
                        staff = row3["FIRST_NAME"] + " " + row3["LAST_NAME"];
                    }
                    dp = new DataTable();
                    sqlQuery = @"Select * from STUPOSN WHERE POSN_CODE = '" + row2["PositionCode"] + "'";
                    cmd2 = new SqlCommand(sqlQuery, con);
                    rdr = cmd2.ExecuteReader();
                    dp.Load(rdr); // fill the data source.
                    foreach (DataRow row3 in dp.Rows)
                    {//position information
                        postitle = row3["POSN_TITLE"] + "";
                        wls = row3["WLS"] + "";
                    }
                    //add a new row to the data table.
                    row.Controls.Add(new LiteralControl(
                    "<tr><td>" + student + "</td><td>" + staff +
                    "</td><td>" + postitle + "</td><td>" + wls + "</td><td>" + row2["FormType"] + "</td><td>" + row2["CreationDate"] + "</td></tr>"));
                }
            }
            finally
            {
                sqlite.Close(); // close connection.
            }
        }
    }
}