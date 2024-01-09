using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;


public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Retrieve connection string from Web.config
        string connectionString = ConfigurationManager.ConnectionStrings["Ginie"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select * from ProjectMaster874";
            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();
            
            Radgrid1.DataSource = dt;
            //Radgrid1.DataBind(); 
        }
    }


}