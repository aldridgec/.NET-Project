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

namespace LaborWebForms.LaborStatusForm
{
    public partial class Complete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {//Redirect.
            Response.AddHeader("REFRESH", "10;URL=../Default.aspx");
        }
    }
}