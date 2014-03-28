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
using System.Diagnostics;
using System.Data.SQLite;

namespace BereaLaborForms.LaborStatusForm
{
    public partial class Approve : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SQLiteConnection sqlite = new SQLiteConnection(ConfigurationManager.ConnectionStrings["SQLite"].ConnectionString);
            SQLiteDataAdapter ad;
            DataTable dt = new DataTable();//open the sql connection and create a data table to store the information in
            try
            {
                SQLiteCommand cmd;
                sqlite.Open(); // initiate the connection to the db.
                cmd = sqlite.CreateCommand();
                cmd.CommandText = "Select * from LaborStatusForm WHERE Supervisee !='0'"; //our sql command to grab all Labor Status Forms.
                ad = new SQLiteDataAdapter(cmd);
                ad.Fill(dt); // fill the data source.
                foreach (DataRow row2 in dt.Rows)
                {
                    DateTime result;
                    DateTime.TryParse((row2["CreatedDate"] ?? "").ToString(), out result);//translate the date into a string.
                    // if (result < DateTime.Now.AddDays(-1))
                    // { //if 24 hours has passed
                    string student = "";
                    string staff = "";
                    string hours = row2["Hour"] + "";
                    string postitle = "";
                    string term = row2["Term"] + "";
                    string primary = row2["JobType"] + "";
                    string wls = "";
                    string dept = "";
                    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Tracy"].ConnectionString);
                    DataTable dp = new DataTable(); //open the connection to the sql as well as a datatable container
                    con.Open(); // initiate the connection to the db.
                    string sqlQuery = @"Select * from STUDATA WHERE ID = '" + row2["Supervisee"] + "'"; //get the student name
                    SqlCommand cmd2 = new SqlCommand(sqlQuery, con);
                    SqlDataReader rdr = cmd2.ExecuteReader();
                    dp.Load(rdr); // fill the data source.
                    foreach (DataRow row3 in dp.Rows)
                    {//student name
                        student = row3["FIRST_NAME"] + " " + row3["LAST_NAME"];
                    }
                    dp.Clear();
                    sqlQuery = "Select * from STUSTAFF WHERE ID = '" + row2["Supervisor"] + "'"; //staff name
                    cmd2 = new SqlCommand(sqlQuery, con);
                    rdr = cmd2.ExecuteReader();
                    dp.Load(rdr); // fill the data source.
                    foreach (DataRow row3 in dp.Rows)
                    {//staff name
                        staff = row3["FIRST_NAME"] + " " + row3["LAST_NAME"];
                    }
                    dp.Clear();
                    sqlQuery = "Select * from STUPOSN WHERE POSN_CODE = '" + row2["Positionc"] + "'";
                    cmd2 = new SqlCommand(sqlQuery, con);
                    rdr = cmd2.ExecuteReader();
                    dp.Load(rdr); // fill the data source.
                    foreach (DataRow row3 in dp.Rows)
                    {//Position Information                        
                        postitle = row3["POSN_TITLE"] + "";
                        wls = row3["WLS"] + "";
                        dept = row3["DEPT_NAME"] + "";
                    }
                    //add a row to the DataTable.
                    row.Controls.Add(new LiteralControl(
                    "<tr><td>" + row2["Form_ID"] + "</td><td>" + student + "</td><td>" + staff + "</td><td>" + row2["Positionc"] +
                    "</td><td>" + postitle + "</td><td>" + wls + "</td><td>" + dept + "</td><td>" + term + "</td><td>" + primary + "(" + hours + ")</td></tr>"));
                    // }
                }
            }
            finally
            {
                sqlite.Close(); // close connection.
            }
        }
    }
}