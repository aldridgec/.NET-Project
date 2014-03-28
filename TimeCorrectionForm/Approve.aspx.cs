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
    public partial class Approve : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SQLiteConnection sqlite = new SQLiteConnection(ConfigurationManager.ConnectionStrings["SQLite"].ConnectionString);
            SQLiteDataAdapter ad;
            DataTable dt = new DataTable(); //open the connection to the sql as well as a datatable container
            try
            {
                SQLiteCommand cmd;
                sqlite.Open(); // initiate the connection to the db.
                cmd = sqlite.CreateCommand();
                DateTime yesterday = DateTime.Now.AddDays(-1);
                cmd.CommandText = "Select * from CorrectedTime WHERE `CreatedDate` < '" + yesterday + "'"; //our sql for all Corrected Time Forms 24 hours old.
                ad = new SQLiteDataAdapter(cmd);
                ad.Fill(dt); // fill the data source.
                foreach (DataRow row2 in dt.Rows)
                {
                    string student = "";
                    string creator = "";
                    string postitle = "";
                    string wls = "";
                    string dept = "";
                    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Tracy"].ConnectionString);
                    DataTable dp = new DataTable(); //open the connection to the sql as well as a datatable container
                    con.Open(); // initiate the connection to the db.
                    string sqlQuery = @"Select * from STUDATA WHERE ID = '" + row2["Student"] + "'"; //get the student name
                    SqlCommand cmd2 = new SqlCommand(sqlQuery, con);
                    SqlDataReader rdr = cmd2.ExecuteReader();
                    dp.Load(rdr); // fill the data source.
                    foreach (DataRow row3 in dp.Rows)
                    {
                        student = row3["FIRST_NAME"] + " " + row3["LAST_NAME"];
                    }

                    dp.Clear();
                    sqlQuery = "Select * from STUSTAFF WHERE ID = '" + row2["Creator"] + "'"; //staff name
                    cmd2 = new SqlCommand(sqlQuery, con);
                    rdr = cmd2.ExecuteReader();
                    dp.Load(rdr); // fill the data source.
                    foreach (DataRow row3 in dp.Rows)
                    {
                        creator = row3["FirstName"] + " " + row3["LastName"];
                    }

                    dp.Clear();
                    sqlQuery = "Select * from STUPOSN WHERE POSN_CODE = '" + row2["Position"] + "'";
                    cmd2 = new SqlCommand(sqlQuery, con);
                    rdr = cmd2.ExecuteReader();
                    dp.Load(rdr); // fill the data source.
                    foreach (DataRow row3 in dp.Rows)
                    {
                        postitle = row3["POSN_TITLE"] + "";
                        wls = row3["WLS"] + "";
                        dept = row3["DEPT_NAME"] + "";
                    }
                    //add a row to the datatable.
                    row.Controls.Add(new LiteralControl(
                    "<tr><td>" + student + "</td><td>" + row2["Hours"] + "</td><td>" + row2["Explain"] + "</td><td>" + row2["Position"] +
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