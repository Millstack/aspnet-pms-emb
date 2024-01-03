using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RA_Bill_New_RABillNew : System.Web.UI.Page
{
    string connectionString = ConfigurationManager.ConnectionStrings["Ginie"].ConnectionString;

    string selectedCategoryRefID;
    string selectedSubCategoryRefID;
    string selectedAODetails;
    string selectedProjectMasterRefID;
    string selectedWorkOrderRefID;
    int selectedWorkOrderAmount;
    string selectedVendorRefID;
    string selectedAbstractNoRefID;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind_Role_ProjectMaster();
        }
    }

    //==========================={ Dropdown Bind }===========================

    public void Bind_Role_ProjectMaster()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select * from ProjectMaster874";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            ddProjectMaster.DataSource = dt;
            ddProjectMaster.DataTextField = "ProjectName";
            ddProjectMaster.DataValueField = "RefID";
            ddProjectMaster.DataBind();
            ddProjectMaster.Items.Insert(0, new ListItem("------Select Sub Category------", "0"));
        }
    }

    public void Bind_Role_WorkOrderDetails(string selectedProjectMasterRefID)
    {
        DataTable projectMasterDt = getProjectMaster(selectedProjectMasterRefID); // project ref id

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select * from WorkOrder874 where woProject=@woProject";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@woProject", projectMasterDt.Rows[0]["RefID"].ToString());
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            ddWorkOrder.DataSource = dt;
            ddWorkOrder.DataTextField = "woTitle";
            ddWorkOrder.DataValueField = "RefID";
            ddWorkOrder.DataBind();
            ddWorkOrder.Items.Insert(0, new ListItem("------Select Work Order------", "0"));
        }
    }

    public void Bind_Role_VendorName(string selectedWorkOrderRefID)
    {
        DataTable WorkOrderDT = getWorkOrderDetails(selectedWorkOrderRefID); // wo ref id

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "SELECT v.vName, v.RefID FROM VendorMaster874 as v INNER JOIN WorkOrder874 AS w ON v.RefID = w.woVendor AND w.RefID = @RefID";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@RefID", WorkOrderDT.Rows[0]["RefID"].ToString());
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            ddVender.DataSource = dt;
            ddVender.DataTextField = "vName";
            ddVender.DataValueField = "RefID";
            ddVender.DataBind();
            ddVender.Items.Insert(0, new ListItem("------Select Vendor------", "0"));
        }
    }

    public void Bind_Role_MileStone(string selectedWorkOrderRefID)
    {

    }

    public void Bind_Role_AbstractNo(string selectedProjectMasterRefID, string selectedWorkOrderRefID)
    {
        DataTable projectMasterDt = getProjectMaster(selectedProjectMasterRefID); // project ref id
        DataTable WorkOrderDT = getWorkOrderDetails(selectedWorkOrderRefID); // wo ref id

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "SELECT * from AbstApproval874 where AbsProj = @AbsProj AND AbsWO = @AbsWO";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@AbsProj", projectMasterDt.Rows[0]["RefID"].ToString());
            cmd.Parameters.AddWithValue("@AbsWO", WorkOrderDT.Rows[0]["RefID"].ToString());
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            ddAbstractNo.DataSource = dt;
            ddAbstractNo.DataTextField = "AbsEmbHeader";
            ddAbstractNo.DataValueField = "AbsEmbHeader";
            ddAbstractNo.DataBind();
            ddAbstractNo.Items.Insert(0, new ListItem("------Select Abstract No.------", "0"));
        }
    }

    public void Bind_Role_DocType()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "SELECT * FROM DocType874";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            ddDocType.DataSource = dt;
            ddDocType.DataTextField = "DocType";
            ddDocType.DataValueField = "DocType";
            ddDocType.DataBind();
            ddDocType.Items.Insert(0, new ListItem("------Select Doc------", "0"));
        }
    }

    public void Bind_Role_Stages()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "SELECT * FROM DocType874";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            ddStage.DataSource = dt;
            ddStage.DataTextField = "DocType";
            ddStage.DataValueField = "DocType";
            ddStage.DataBind();
            ddStage.Items.Insert(0, new ListItem("------Select Stage------", "0"));
        }
    }

    //==========================={ Dropdown Event }===========================

    protected void ddProjectMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        selectedProjectMasterRefID = ddProjectMaster.SelectedValue; // Project Master RefID

        if (ddProjectMaster.SelectedValue != "0")
        {
            Bind_Role_WorkOrderDetails(selectedProjectMasterRefID);
        }
        else
        {
            // Clear the values of follwing dropdowns on the server side
            ddWorkOrder.Items.Clear();
            ddVender.Items.Clear();

            // Clear the values of ddWorkOrder & ddVender on the client side using JavaScript
            ScriptManager.RegisterStartupScript(this, GetType(), "ClearWorkOrderDropdown", "ClearWorkOrderDropdown();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "ClearVendorDropdown", "clearVendorDropdown();", true);
        }
    }

    protected void ddWorkOrder_SelectedIndexChanged(object sender, EventArgs e)
    {
        selectedProjectMasterRefID = ddProjectMaster.SelectedValue; // Project ref id
        selectedWorkOrderRefID = ddWorkOrder.SelectedValue; // WO RefID

        if (ddWorkOrder.SelectedValue != "0")
        {
            Bind_Role_VendorName(selectedWorkOrderRefID);
            Bind_Role_AbstractNo(selectedProjectMasterRefID, selectedWorkOrderRefID);

            // checking if there is only one vendor
            if (ddVender.Items.Count == 2)
            {
                ddVender.SelectedIndex = 1;
            }
            else
            {
                ddVender.SelectedIndex = 0;
            }
        }
        else
        {
            // Clear the values of ddVender on the server side
            ddVender.Items.Clear();
            ddAbstractNo.Items.Clear();

            // Clear the values of ddVender on the client side using JavaScript
            ScriptManager.RegisterStartupScript(this, GetType(), "ClearVendorDropdown", "clearVendorDropdown();", true);
        }
    }

    protected void ddVender_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddMileStone_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddAbstractNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        selectedProjectMasterRefID = ddProjectMaster.SelectedValue; // Project ref id
        selectedWorkOrderRefID = ddWorkOrder.SelectedValue; // WO RefID
        selectedAbstractNoRefID = ddAbstractNo.SelectedValue; // Abstract No. RefID

        FillGridViewWithBoqDetails(selectedProjectMasterRefID, selectedWorkOrderRefID, selectedAbstractNoRefID);
    }

    //==========================={ Fetching data }===========================

    private DataTable getProjectMaster(string selectedProjectMasterRefIDCode)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "SELECT * FROM ProjectMaster874 where RefID=@RefID";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@RefID", selectedProjectMasterRefIDCode.ToString());
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();
            return dt;
        }
    }

    private DataTable getWorkOrderDetails(string selectedWorkOrderRefID)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "SELECT * FROM WorkOrder874 where RefID=@RefID";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@RefID", selectedWorkOrderRefID.ToString());
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            return dt;
        }
    }

    private DataTable getAbstractNoDetails(string selectedAbstractNoHeader)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "SELECT * FROM AbstApproval874 where AbsEmbHeader=@AbsEmbHeader";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@AbsEmbHeader", selectedAbstractNoHeader.ToString());
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            return dt;
        }
    }

    private DataTable getEmbHeader(string selectedAbstracEmbtHeader)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "SELECT * FROM EmbMaster874 as emb, AbstApproval874 as abs where abs.AbsEmbHeader = emb.EmbMasRefId and abs.AbsEmbHeader = @AbsEmbHeader";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@AbsEmbHeader", selectedAbstracEmbtHeader.ToString());
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            return dt;
        }
    }

    private DataTable getEmbDetails(string embHeaderRefId)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "SELECT * FROM EmbRecords874 where EmbHeaderId = @EmbHeaderId";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@EmbHeaderId", embHeaderRefId.ToString());
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            return dt;
        }
    }

    private DataTable getAccountHead()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "SELECT * FROM AccountHead874 WHERE AccHeadGroup = 'Duties & Taxes'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            return dt;
        }
    }

    //=============================={ Fill BoQ Grid Records }============================================

    protected void GridDyanmic_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) > 0)
        {
            // Set the row in edit mode
            e.Row.RowState = e.Row.RowState ^ DataControlRowState.Edit;
        }
    }

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
            DataTable accountHeadDT = getAccountHead();

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

    private void autoFilltaxHeads(DataTable accountHeadDT, double bscAmnt)
    {
        double basicAmount = bscAmnt;
        double totalDeduction = 0.00;
        double netAmount = 0.00;

        foreach (DataRow row in accountHeadDT.Rows)
        {
            double percentage = Convert.ToDouble(row["FactorInPer"]);

            double factorInPer = (basicAmount * percentage) / 100;

            if(row["AddLess"].ToString() == "Add")
            {
                totalDeduction = totalDeduction + factorInPer;
            }
            else 
            {
                totalDeduction = totalDeduction - factorInPer;
            }

            row["TaxAmount"] = factorInPer;
        }

        GridTax.DataSource = accountHeadDT;
        GridTax.DataBind();

        // fill total deduction to textbox
        txtTotalTax.Text = totalDeduction.ToString("N2");

        // Net Amount after tax deductions
        netAmount = basicAmount - totalDeduction;
        txtNetAmnt.Text = netAmount.ToString("N2");
    }

    private void FillGridViewWithBoqDetails(string selectedProjectMasterRefID, string selectedWorkOrderRefID, string selectedAbstractNoHeader)
    {
        DataTable projectMasterDt = getProjectMaster(selectedProjectMasterRefID); // project ref id
        DataTable WorkOrderDT = getWorkOrderDetails(selectedWorkOrderRefID); // wo ref id
        DataTable AbstractDT = getAbstractNoDetails(selectedAbstractNoHeader); // abstract emb header

        // assign work order amount
        int woAmount = Convert.ToInt32(WorkOrderDT.Rows[0]["woTendrValue"]);
        //txtWoAmnt.Text = "₹ " + woAmount.ToString("N0");
        txtWoAmnt.Text = woAmount.ToString();

        // fetching emb header using abstract
        DataTable embHeaderDT = getEmbHeader(AbstractDT.Rows[0]["AbsEmbHeader"].ToString());

        // fetching emb details
        DataTable emdDetailsDT = getEmbDetails(embHeaderDT.Rows[0]["EmbMasRefId"].ToString());

        // fetching acount head or taxes
        DataTable accountHeadDT = getAccountHead();

        // binding document dropdowns
        Bind_Role_DocType();
        Bind_Role_Stages();

        if (emdDetailsDT.Rows.Count > 0)
        {
            gridEmbDiv.Visible = true;
            divTax.Visible = true;
            btnReCalTax.Enabled = true;

            txtBasicAmt.Text = emdDetailsDT.Rows[0]["BasicAmount"].ToString();
            double basicAmount = Convert.ToDouble(txtBasicAmt.Text);

            // fill tax heads
            autoFilltaxHeads(accountHeadDT, basicAmount);

            // grid bind for emb details
            gridDynamicBOQ.DataSource = emdDetailsDT;
            gridDynamicBOQ.DataBind();
        }
        else
        {
            // alert pop-up with only message
            string message1 = "No EMB Details Found";
            string script1 = $"alert('{message1}');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "messageScript", script1, true);
        }
    }

    protected void btnReCalTax_Click(object sender, EventArgs e)
    {
        // inserting EMB header info
        //InsertEmbHeader();
    }

    //=============================={ Button Click }============================================

    protected void btnBasicAmount_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["BoQDT"];

        int basicAmount = 0;

        if (dt != null)
        {
            // Iterate through each row in the GridView
            foreach (GridViewRow row in gridDynamicBOQ.Rows)
            {
                TextBox boqQty = row.FindControl("QtyMeasure") as TextBox;
                int qtyMeasuredValue = Convert.ToInt32(boqQty.Text);

                int rowIndex = row.RowIndex;
                int boqUnitRate = Convert.ToInt32(dt.Rows[rowIndex]["BoqRate"]);

                if (qtyMeasuredValue != 0 && boqUnitRate != 0)
                {
                    int prod = (qtyMeasuredValue * boqUnitRate);

                    // Perform operations with the value
                    basicAmount = basicAmount + prod;
                }

                // You can break the loop if you only need the value from the first row
                //break;
            }

            string basicAmountStr = basicAmount.ToString("N0");

            txtBasicAmt.CssClass = "form-control fw-normal border border-2";
            txtBasicAmt.Text = basicAmountStr;
        }
    }

    protected void btnDocUpload_Click(object sender, EventArgs e)
    {
        string docTypeCode = ddDocType.SelectedValue;
        string stageCode = ddStage.SelectedValue;

        if (fileDoc.HasFile)
        {
            string FileExtension = System.IO.Path.GetExtension(fileDoc.FileName);

            if (FileExtension == ".xlsx" || FileExtension == ".xls")
            {
                
            }

            // getting unique file name
            string strFileName = GenerateUniqueId(fileDoc.FileName.ToString());

            // file path name
            //string filePath = Server.MapPath("~/Portal/Public/" + strFileName);
            //file:///C:/HostingSpaces/PAWAN/cdsmis.in/wwwroot/Pms2/Portal/Public/638399011215544557_926f9320-275e-49ad-8f59-32ecb304a9f1_EMB%20Recording.pdf

            string orgFilePath = Server.MapPath("~/Portal/Public/" + strFileName);

            // saving file
            fileDoc.SaveAs(orgFilePath);

            // replacing server-specific path with the desired URL
            string baseUrl = "http://101.53.144.92/pms2/Ginie/External?url=..";
            string relativePath = orgFilePath.Replace(Server.MapPath("~/Portal/Public/"), "Portal/Public/");

            // Full URL for the hyperlink
            string fullUrl = $"{baseUrl}/{relativePath}";

            //================================================================

            // Retrieve DataTable from ViewState or create a new one
            DataTable dt = ViewState["DocDetailsDataTable"] as DataTable ?? CreateDocDetailsDataTable();

            // filling document details datatable
            AddRowToDocDetailsDataTable(dt, docTypeCode, stageCode, fullUrl);

            // Save DataTable to ViewState
            ViewState["DocDetailsDataTable"] = dt;

            // binding document details gridview
            GridDocument.DataSource = dt;
            GridDocument.DataBind();
        }
    }

    private string GenerateUniqueId(string strFileName)
    {
        long timestamp = DateTime.Now.Ticks;
        //string guid = Guid.NewGuid().ToString("N"); //N to remove hypen "-" from GUIDs
        string guid = Guid.NewGuid().ToString(); //N to remove hypen "-" from GUIDs
        string uniqueID = timestamp + "_" + guid + "_" + strFileName;
        return uniqueID;
    }

    private DataTable CreateDocDetailsDataTable()
    {
        DataTable dt = new DataTable();

        // document type
        DataColumn docType = new DataColumn("docType", typeof(string));
        dt.Columns.Add(docType);

        // stage level
        DataColumn stageLevel = new DataColumn("stageLevel", typeof(string));
        dt.Columns.Add(stageLevel);

        // Doc uploaded path
        DataColumn docPath = new DataColumn("docPath", typeof(string));
        dt.Columns.Add(docPath);

        return dt;
    }

    private void AddRowToDocDetailsDataTable(DataTable dt, string docTypeCode, string stageCode, string filePath)
    {
        // Create a new row
        DataRow row = dt.NewRow();

        // Set values for the new row
        row["docType"] = docTypeCode;
        row["stageLevel"] = stageCode;
        row["docPath"] = filePath;

        // Add the new row to the DataTable
        dt.Rows.Add(row);
    }
}