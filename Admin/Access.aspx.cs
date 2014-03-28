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
    public partial class Access : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Tracy"].ConnectionString);
            DataTable dt = new DataTable(); //open the connection to the sql as well as a datatable container
            DataTable dp = new DataTable(); //open the connection to the sql as well as a datatable container

            if ((AddAdmin.SelectedValue == ""))
            {
                AddAdmin.Items.Clear();
                try
                {
                    con.Open(); // initiate the connection to the db.
                    string sqlQuery = @"SELECT * from STUDATA ORDER BY LAST_NAME";
                    SqlCommand cmd = new SqlCommand(sqlQuery, con);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    dt.Load(rdr);
                    AddAdmin.Items.Add(new ListItem(""));
                    foreach (DataRow row3 in dt.Rows)
                    {
                        AddAdmin.Items.Add(new ListItem(row3["ID"].ToString() + ", " + row3["FIRST_NAME"].ToString() + " " + row3["LAST_NAME"].ToString(), row3["ID"].ToString()));
                    }
                }
                finally
                {
                    con.Close();
                }

                try
                {
                    con.Open(); // initiate the connection to the db.
                    string sqlQuery = @"SELECT * from STUSTAFF ORDER BY LAST_NAME";
                    SqlCommand cmd = new SqlCommand(sqlQuery, con);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    dt.Load(rdr);
                    foreach (DataRow row3 in dt.Rows)
                    {
                        AddAdmin.Items.Add(new ListItem(row3["ID"].ToString() + ", " + row3["FIRST_NAME"].ToString() + " " + row3["LAST_NAME"].ToString(), row3["ID"].ToString()));
                    }
                }
                finally
                {
                    con.Close();
                }
            }
            if ((RemoveAdmin.SelectedValue == ""))
            {
                RemoveAdmin.Items.Clear();
                SQLiteConnection sqlite = new SQLiteConnection(ConfigurationManager.ConnectionStrings["SQLite"].ConnectionString);
                dt = new DataTable(); //open the connection and create a datatable to store the information in.
                try
                {
                    sqlite.Open(); // initiate the connection to the db.
                    SQLiteCommand cmd;
                    cmd = sqlite.CreateCommand();
                    cmd.CommandText = "SELECT * from Admin"; //our sql command to grab all Labor Status Forms.
                    SQLiteDataAdapter ad;
                    ad = new SQLiteDataAdapter(cmd);
                    ad.Fill(dt); // fill the data source.
                    foreach (DataRow row3 in dt.Rows)
                    {
                        con.Open(); // initiate the connection to the db.
                        dp = new DataTable();
                        string sqlQuery2 = @"SELECT * from STUDATA WHERE ID = '" + row3["ID"] + "'";
                        SqlCommand cmd2 = new SqlCommand(sqlQuery2, con);
                        SqlDataReader rdr = cmd2.ExecuteReader();
                        if (rdr.HasRows)
                        {
                            dp.Load(rdr);
                            foreach (DataRow row4 in dp.Rows)
                            {
                                RemoveAdmin.Items.Add(new ListItem(row4["ID"].ToString() + ", " + row4["FIRST_NAME"].ToString() + " " + row4["LAST_NAME"].ToString(), row4["ID"].ToString()));
                            }
                        }
                        else
                        {
                            con.Close();
                            con.Open(); // initiate the connection to the db.
                            dp = new DataTable();
                            sqlQuery2 = @"SELECT * from STUSTAFF WHERE ID = '" + row3["ID"] + "'";
                            cmd2 = new SqlCommand(sqlQuery2, con);
                            rdr = cmd2.ExecuteReader();
                            if (rdr.HasRows)
                            {
                                dp.Load(rdr);
                                foreach (DataRow row4 in dp.Rows)
                                {
                                    RemoveAdmin.Items.Add(new ListItem(row4["ID"].ToString() + ", " + row4["FIRST_NAME"].ToString() + " " + row4["LAST_NAME"].ToString(), row4["ID"].ToString()));
                                }
                            }

                        }
                        con.Close();
                    }
                }
                finally
                {
                    sqlite.Close();
                }
            }
        }
        protected void submit1(object sender, EventArgs e)
        {
            string newadmin;
            if ((AddAdmin.SelectedValue != ""))
            {
                newadmin = AddAdmin.SelectedValue;
                SQLiteConnection sqlite = new SQLiteConnection(ConfigurationManager.ConnectionStrings["SQLite"].ConnectionString);
                SQLiteCommand cmd;
                sqlite.Open(); // initiate the connection to the db.
                cmd = sqlite.CreateCommand();
                cmd.CommandText = "INSERT INTO Admin VALUES ('" + newadmin + "');";
                cmd.ExecuteNonQuery();//Insert all data in the form into the LaborStatusForm table.
                Response.Redirect(Request.RawUrl);
            }
        }
        protected void submit2(object sender, EventArgs e)
        {
            string newadmin;
            if ((RemoveAdmin.SelectedValue != ""))
            {
                newadmin = RemoveAdmin.SelectedValue;
                SQLiteConnection sqlite = new SQLiteConnection(ConfigurationManager.ConnectionStrings["SQLite"].ConnectionString);
                SQLiteCommand cmd;
                sqlite.Open(); // initiate the connection to the db.
                cmd = sqlite.CreateCommand();
                cmd.CommandText = "Delete FROM Admin WHERE ID = '" + newadmin + "';";
                cmd.ExecuteNonQuery();//Insert all data in the form into the LaborStatusForm table.
                Response.Redirect(Request.RawUrl);
            }
        }
    }
}