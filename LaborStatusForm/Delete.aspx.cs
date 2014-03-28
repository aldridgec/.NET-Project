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
namespace LaborWebForms.LaborStatusForm
{
    public partial class deletelsf : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);
                // get the person_id of the person being deleted
                int person_id = Convert.ToInt32(Request.QueryString["id"]);
                SQLiteConnection sqlite = new SQLiteConnection(ConfigurationManager.ConnectionStrings["SQLite"].ConnectionString);
                DataTable dt = new DataTable();
                try
                {
                    SQLiteCommand cmd;
                    sqlite.Open(); // initiate the connection to the db.
                    cmd = sqlite.CreateCommand();
                    cmd.CommandText = "DELETE from LaborStatusForm WHERE `Form_ID` = '" + person_id + "'";
                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    sqlite.Close(); // close connection.
                }
            }
            // this will catch all the errors and output them to the edited field
            catch (Exception ex) { Response.Write("Error: " + ex.Message); }
        }
    }
}