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
            //Bind_RaHeaser();
            //Bind_Role_Project();

            DirectSQLQuery();
        }
    }

    private void Bind_RaHeaser()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select * from RaHeader874";
            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            gridRaHeader.DataSource = dt;
            gridRaHeader.DataBind();
        }
    }

    private void DirectSQLQuery()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select * from CaseCreation864";
            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            string message = "Case Creation Count: " + dt.Rows.Count;
            string script = $"alert('{message}');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "messageScript", script, true);
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
            gridRaUpdateDiv.Visible = false;

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
            gridRaUpdateDiv.Visible = false;

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

    private DataTable getRaHeader(int rowId)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "SELECT * FROM RaHeader874 where RefID=@RefID";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@RefID", rowId.ToString());
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            return dt;
        }
    }

    private DataTable getRaDetails(int rowId)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "SELECT * FROM RaDetails874 where RaHeaderID=@RaHeaderID";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@RaHeaderID", rowId.ToString());
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            return dt;
        }
    }

    private DataTable getAccountHead(int RaHeaderID)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "SELECT * FROM RaTax874 WHERE RaHeaderID = @RaHeaderID";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@RaHeaderID", RaHeaderID);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            return dt;
        }
    }

    //=============================={ Fill BoQ & Tax Head }============================================

    protected void GridTax_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) > 0)
        {
            // Set the row in edit mode
            e.Row.RowState = e.Row.RowState ^ DataControlRowState.Edit;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // fetching acount head or taxes
            int RaHeaderID = Convert.ToInt32(Session["RowID"]);
            DataTable accountHeadDT = getAccountHead(RaHeaderID);

            // adding + / - dropdowns
            DropDownList ddlAddLess = (DropDownList)e.Row.FindControl("AddLess");
            if (ddlAddLess != null)
            {
                // Add options dynamically
                ddlAddLess.Items.Add(new ListItem("+", "Add"));
                ddlAddLess.Items.Add(new ListItem("-", "Less"));

                // Set selected value based on the "AddLess" column in the DataTable
                string addLessValue = accountHeadDT.Rows[e.Row.RowIndex]["AddLess"].ToString();

                // Set the selected value in the DropDownList
                ListItem selectedListItem = ddlAddLess.Items.FindByValue(addLessValue);
                if (selectedListItem != null)
                {
                    selectedListItem.Selected = true;
                }
            }

            // adding % / ₹ dropdowns
            DropDownList ddlPerOrAmnt = (DropDownList)e.Row.FindControl("PerOrAmnt");
            if (ddlPerOrAmnt != null)
            {

                // Add options dynamically
                ddlPerOrAmnt.Items.Add(new ListItem("%", "Percentage"));
                ddlPerOrAmnt.Items.Add(new ListItem("₹", "Amount"));
            }
        }
    }

    protected void btnReCalTax_Click(object sender, EventArgs e)
    {
        // inserting EMB header info
        //InsertEmbHeader();
    }

    //========================================================================

    private void Bind_Role_Update_Dropdowns(int rowId)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();

            string sql = "select * from RaHeader874 where RaHeaderID = @RaHeaderID";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@RaHeaderID", rowId);

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);

            ddProjectMaster.DataSource = dt;
            ddProjectMaster.DataTextField = "RaProj";
            ddProjectMaster.DataValueField = "RefID";
            ddProjectMaster.DataBind();
            ddProjectMaster.Items.Insert(0, new ListItem("------Select Project------", "0"));

            ddWorkOrder.DataSource = dt;
            ddWorkOrder.DataTextField = "RaWO";
            ddWorkOrder.DataValueField = "RefID";
            ddWorkOrder.DataBind();
            ddWorkOrder.Items.Insert(0, new ListItem("------Select Work Order------", "0"));

            ddVender.DataSource = dt;
            ddVender.DataTextField = "RaVendor";
            ddVender.DataValueField = "RefID";
            ddVender.DataBind();
            ddVender.Items.Insert(0, new ListItem("------Select Vendor------", "0"));

            ddAbstractNo.DataSource = dt;
            ddAbstractNo.DataTextField = "RaAbstNo";
            ddAbstractNo.DataValueField = "RefID";
            ddAbstractNo.DataBind();
            ddAbstractNo.Items.Insert(0, new ListItem("------Select Abstract No.------", "0"));

            con.Close();
        }
    }

    //=========================={ Button Click Event }==========================

    protected void btnNewRaBill_Click(object sender, EventArgs e)
    {
        Response.Redirect("../RABillNew.aspx");
    }

    protected void btnTruncate_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();

            string sql = "truncate table RaHeader874";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            string sql1 = "truncate table RaDetails874";
            SqlCommand cmd1 = new SqlCommand(sql1, con);
            cmd1.ExecuteNonQuery();

            con.Close();
        }

        string message = "RA Header & RA Details truncated";
        string script = $"alert('{message}');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "messageScript", script, true);
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
            gridRaUpdateDiv.Visible = true;
        }
        else if (ddProject.SelectedValue.ToString() != "0" && ddWOName.SelectedValue.ToString() == "0" && string.IsNullOrEmpty(ddVendorName.SelectedValue))
        {
            // only project is selceted
            projectRefID = ddProject.SelectedValue; // Ref ID

            searchedEmbDT = getSearchedEmbHeader(projectRefID, "", "");

            gridRaUpdateDiv.Visible = true;

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

            gridRaUpdateDiv.Visible = true;

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

            gridRaUpdateDiv.Visible = true;

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

    protected void btnBasicAmount_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["BoQDTUpdate"];

        double basicAmount = 0;

        if (dt != null)
        {
            // Iterate through each row in the GridView
            foreach (GridViewRow row in gridDynamicBOQ.Rows)
            {
                TextBox boqQty = row.FindControl("BoqQtyMeas") as TextBox;
                double qtyMeasuredValue = Convert.ToDouble(boqQty.Text);

                int rowIndex = row.RowIndex;
                double boqUnitRate = Convert.ToDouble(dt.Rows[rowIndex]["BoQItemRate"]);

                if (qtyMeasuredValue != 0.00 && boqUnitRate != 0.00)
                {
                    double prod = (qtyMeasuredValue * boqUnitRate);

                    // Perform operations with the value
                    basicAmount = basicAmount + prod;
                }

                // You can break the loop if you only need the value from the first row
                //break;
            }

            //btnSubmitBasicAmount.Enabled = true;

            string basicAmountStr = basicAmount.ToString("N0");

            txtBasicAmt.CssClass = "form-control fw-normal border border-2";
            txtBasicAmt.Text = basicAmountStr;
        }
    }

    protected void GrdUser_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "lnkView")
        {
            int rowId = Convert.ToInt32(e.CommandArgument);
            Session["RowID"] = rowId;

            gridRaUpdateDiv.Visible = false;
            divRAUpdate.Visible = true;
            //raDetailsUpdate.Visible = true;

            divTopSearch.Visible = false;
            //btnSubmitBasicAmount.Enabled = true;

            // getting all dropdowns
            Bind_Role_Update_Dropdowns(rowId);

            // fill RA header
            updateRAHeader(rowId);

            // fill EMB Details
            updateEmbDetails(rowId);
        }
    }

    private void updateRAHeader(int rowId)
    {
        // fetching EMB header
        DataTable raHeaderDT = getRaHeader(rowId);

        // account head datatable
        //autoFilltaxHeads(raHeaderDT, );

        // fill Account Head grid
        //autoFilltaxHeads();

        if (raHeaderDT.Rows.Count > 0)
        {
            // drop downs
            ddProjectMaster.SelectedIndex = 1;
            ddWorkOrder.SelectedIndex = 1;
            ddVender.SelectedIndex = 1;
            ddAbstractNo.SelectedIndex = 1;

            // header non-dropdowns
            txtWoAmnt.Text = raHeaderDT.Rows[0]["RaWoAmount"].ToString();
            txtUpToTotalRaAmnt.Text = raHeaderDT.Rows[0]["RaBillBookAmnt"].ToString();
            txtRemarks.Value = raHeaderDT.Rows[0]["RaRemarks"].ToString();

            // date
            DateTime billDate = DateTime.Parse(raHeaderDT.Rows[0]["RaBillDate"].ToString());
            DateTime payDueDate = DateTime.Parse(raHeaderDT.Rows[0]["RaPayDueDate"].ToString());

            dateBillDate.Text = billDate.ToString("yyyy-MM-dd");
            datePayDueDate.Text = payDueDate.ToString("yyyy-MM-dd");

            txtBillNo.Text = raHeaderDT.Rows[0]["RaBillNo"].ToString();
            txtBasicAmt.Text = raHeaderDT.Rows[0]["RaBasicAmount"].ToString();

            // tax heads data
            txtTotalDeduct.Text = raHeaderDT.Rows[0]["RaTotalDeduct"].ToString();
            txtTotalAdd.Text = raHeaderDT.Rows[0]["RaTotalAdd"].ToString();
            txtNetAmnt.Text = raHeaderDT.Rows[0]["RaNetAmount"].ToString();

            gridEmbDiv.Visible = true;
        }
        else
        {
            string message = "No RA Header data Found !";
            string script = $"alert('{message}');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "messageScript", script, true);
        }
    }

    private void updateEmbDetails(int rowId)
    {
        // fetching EMB details
        DataTable dt = getRaDetails(rowId);
        Session["BoQDTUpdate"] = dt;

        if (dt.Rows.Count > 0)
        {
            

            gridDynamicBOQ.DataSource = dt;
            gridDynamicBOQ.DataBind();
        }
        else
        {
            string message1 = "No RA Details Found !";
            string script1 = $"alert('{message1}');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "messageScript", script1, true);
        }
    }
}