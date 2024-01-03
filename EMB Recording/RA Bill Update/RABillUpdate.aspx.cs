using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RA_Bill_RABill : System.Web.UI.Page
{
    string connectionString = ConfigurationManager.ConnectionStrings["Ginie"].ConnectionString;

    string projectRefID;
    string workOrderRefID;
    string vendorRefID;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind_RaHeaser();
            Bind_Role_Project();
        }
    }

    private void Bind_RaHeaser()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select * from EmbMaster874";
            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            gridRaHeader.DataSource = dt;
            gridRaHeader.DataBind();
        }
    }

    //=========================={ Drop Down Binding }==========================

    private void Bind_Role_Project()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select * from ProjectMaster874";
            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            ddProject.DataSource = dt;
            ddProject.DataTextField = "ProjectName";
            ddProject.DataValueField = "RefID";
            ddProject.DataBind();
            ddProject.Items.Insert(0, new ListItem("------Select Project------", "0"));
        }
    }

    private void Bind_Role_WorkOrder(string projectRefID)
    {
        DataTable projectDT = getProject(projectRefID);

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select * from WorkOrder874 where woProject = @woProject";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@woProject", projectDT.Rows[0]["RefID"].ToString());

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            ddWOName.DataSource = dt;
            ddWOName.DataTextField = "woTitle";
            ddWOName.DataValueField = "RefID";
            ddWOName.DataBind();
            ddWOName.Items.Insert(0, new ListItem("------Select Work Order------", "0"));
        }
    }

    private void Bind_Role_Vendor(string projectRefID, string workOrderRefID)
    {
        DataTable projectDT = getProject(projectRefID);
        DataTable workOrderDT = getWorkOrder(workOrderRefID);

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select * from VendorMaster874";
            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            ddVendorName.DataSource = dt;
            ddVendorName.DataTextField = "vName";
            ddVendorName.DataValueField = "RefID";
            ddVendorName.DataBind();
            ddVendorName.Items.Insert(0, new ListItem("------Select Vendor------", "0"));
        }
    }

    //=========================={ Drop Down Event }==========================

    protected void ddProject_SelectedIndexChanged(object sender, EventArgs e)
    {
        projectRefID = ddProject.SelectedValue;

        if (ddProject.SelectedValue != "0")
        {
            Bind_Role_WorkOrder(projectRefID);
        }
        else
        {
            gridEmbDiv.Visible = false;

            // Clear the values of follwing dropdowns on the server side
            ddWOName.Items.Clear();
            ddVendorName.Items.Clear();
            //ddWorkOrder.Items.Insert(0, new ListItem("------Select Vendor------", "0"));

            // Clear the values of ddWorkOrder & ddVender on the client side using JavaScript
            ScriptManager.RegisterStartupScript(this, GetType(), "ClearWorkOrderDropdown", "ClearWorkOrderDropdown();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "ClearVendorDropdown", "ClearVendorDropdown();", true);
        }
    }

    protected void ddWOName_SelectedIndexChanged(object sender, EventArgs e)
    {
        projectRefID = ddProject.SelectedValue;
        workOrderRefID = ddWOName.SelectedValue;

        if (ddWOName.SelectedValue != "0")
        {
            Bind_Role_Vendor(projectRefID, workOrderRefID);
        }
        else
        {
            gridEmbDiv.Visible = false;

            // Clear the values of follwing dropdowns on the server side
            ddVendorName.Items.Clear();

            // Clear the values of ddWorkOrder & ddVender on the client side using JavaScript
            ScriptManager.RegisterStartupScript(this, GetType(), "ClearVendorDropdown", "ClearVendorDropdown();", true);
        }
    }

    protected void ddVendorName_SelectedIndexChanged(object sender, EventArgs e)
    {
        projectRefID = ddProject.SelectedValue;
        workOrderRefID = ddWOName.SelectedValue;
        vendorRefID = ddVendorName.SelectedValue;
    }

    //=========================={ Fetching Data }==========================

    private DataTable getProject(string projectRefID)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "SELECT * FROM ProjectMaster874 where RefID=@RefID";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@RefID", projectRefID.ToString());
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();
            return dt;
        }
    }

    private DataTable getWorkOrder(string workOrderRefID)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "SELECT * FROM WorkOrder874 where RefID=@RefID";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@RefID", workOrderRefID.ToString());
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();
            return dt;
        }
    }

    private DataTable getVendor(string vendorRefID)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "SELECT * FROM VendorMaster874 where RefID=@RefID";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@RefID", vendorRefID.ToString());
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();
            return dt;
        }
    }

    private DataTable getSearchedEmbHeader(string project, string workOrder, string vendor)
    {
        DataTable dt = new DataTable();

        if (project != "" && workOrder != "" && vendor != "")
        {
            DataTable projectDT = getProject(project); // project dt
            DataTable workOrderDT = getWorkOrder(workOrder); // work order dt
            DataTable vendorDT = getVendor(vendor); // vendor dt

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string sql = "SELECT * FROM EmbMaster874 where EmbPM = @EmbPM and EmbWO = @EmbWO and EmbVenN = @EmbVenN";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@EmbPM", projectDT.Rows[0]["ProjectName"].ToString());
                cmd.Parameters.AddWithValue("@EmbWO", workOrderDT.Rows[0]["woTendrNo"].ToString());
                cmd.Parameters.AddWithValue("@EmbVenN", vendorDT.Rows[0]["vName"].ToString());
                cmd.ExecuteNonQuery();

                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                ad.Fill(dt);
                con.Close();
            }
        }
        else if (project != "" && workOrder != "")
        {
            DataTable projectDT = getProject(project); // project dt
            DataTable workOrderDT = getWorkOrder(workOrder); // work order dt

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string sql = "SELECT * FROM EmbMaster874 where EmbPM = @EmbPM and EmbWO = @EmbWO";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@EmbPM", projectDT.Rows[0]["ProjectName"].ToString());
                cmd.Parameters.AddWithValue("@EmbWO", workOrderDT.Rows[0]["woTendrNo"].ToString());
                cmd.ExecuteNonQuery();

                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                ad.Fill(dt);
                con.Close();
            }
        }
        else if (project != "")
        {
            DataTable projectDT = getProject(project); // project dt

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string sql = "SELECT * FROM EmbMaster874 where EmbPM = @EmbPM";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@EmbPM", projectDT.Rows[0]["ProjectName"].ToString());
                cmd.ExecuteNonQuery();

                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                ad.Fill(dt);
                con.Close();
            }
        }

        return dt;
    }

    protected void GridEmbHeader_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //binding GridView to PageIndex object
        gridRaHeader.PageIndex = e.NewPageIndex;
        Bind_RaHeaser();
    }

    //=========================={ Button Click Event }==========================

    protected void btnNewRaBill_Click(object sender, EventArgs e)
    {
        Response.Redirect("../RABillNew.aspx");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGridView();
    }

    private void BindGridView()
    {
        DataTable searchedEmbDT;

        if (ddProject.SelectedValue.ToString() == "0" && string.IsNullOrEmpty(ddWOName.SelectedValue) && string.IsNullOrEmpty(ddVendorName.SelectedValue))
        {
            gridEmbDiv.Visible = true;
        }
        else if (ddProject.SelectedValue.ToString() != "0" && ddWOName.SelectedValue.ToString() == "0" && string.IsNullOrEmpty(ddVendorName.SelectedValue))
        {
            // only project is selceted
            projectRefID = ddProject.SelectedValue; // Ref ID

            searchedEmbDT = getSearchedEmbHeader(projectRefID, "", "");

            gridEmbDiv.Visible = true;

            // binding the grid
            gridRaHeader.DataSource = searchedEmbDT;
            gridRaHeader.DataBind();
        }
        else if (ddProject.SelectedValue.ToString() != "0" && ddWOName.SelectedValue.ToString() != "0" && ddVendorName.SelectedValue.ToString() == "0")
        {
            // only project and work order are selceted

            projectRefID = ddProject.SelectedValue; // Ref ID
            workOrderRefID = ddWOName.SelectedValue; // Ref ID

            searchedEmbDT = getSearchedEmbHeader(projectRefID, workOrderRefID, "");

            gridEmbDiv.Visible = true;

            // binding the grid
            gridRaHeader.DataSource = searchedEmbDT;
            gridRaHeader.DataBind();
        }
        else if (ddProject.SelectedValue.ToString() != "0" && ddWOName.SelectedValue.ToString() != "0" && ddVendorName.SelectedValue.ToString() != "0")
        {
            // project, work order and vendor are selceted

            projectRefID = ddProject.SelectedValue; // Ref ID
            workOrderRefID = ddWOName.SelectedValue; // Ref ID
            vendorRefID = ddVendorName.SelectedValue; // Ref ID

            searchedEmbDT = getSearchedEmbHeader(projectRefID, workOrderRefID, vendorRefID);

            gridEmbDiv.Visible = true;

            // binding the grid
            gridRaHeader.DataSource = searchedEmbDT;
            gridRaHeader.DataBind();
        }
        else
        {
            //redirect with only message
            string message = "Please select properly !";
            string script = $"alert('{message}');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "messageScript", script, true);
        }
    }

    protected void GrdUser_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "lnkView")
        {
            int rowId = Convert.ToInt32(e.CommandArgument);
            Session["RowID"] = rowId;

            gridEmbDiv.Visible = false;
            //divEMBUpdate.Visible = true;
            //embDetailsUpdate.Visible = true;

            divTopSearch.Visible = false;
            //btnSubmitBasicAmount.Enabled = true;

            // fill EMB header
            //UpdateEmbCaculations(rowId);

            // fill EMB Details
            //updateEmbDetails(rowId);
        }
    }
}