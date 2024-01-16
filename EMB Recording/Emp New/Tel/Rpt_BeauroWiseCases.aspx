<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_BeauroWiseCases.aspx.cs" Inherits="Rpt_BeauroWiseCases" EnableViewState="true" EnableEventValidation="true" %>
<%@ Register Assembly="Telerik.Web.UI" TagPrefix="telerik" Namespace="Telerik.Web.UI" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <!-- Bootstrap CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/bootstrap1.min.css" rel="stylesheet" />

    <!-- Popper.js -->
    <script src="js/popper.min.js"></script>
    <script src="js/popper1.min.js"></script>

    <!-- jQuery -->
    <script src="js/jquery-3.6.0.min.js"></script>
    <script src="js/jquery.min.js"></script>
    <script src="js/jquery-3.3.1.slim.min.js"></script>

    <!-- Bootstrap JS -->
    <script src="js/bootstrap.bundle.min.js"></script>
    <script src="js/bootstrap1.min.js"></script>

    <!-- Select2 library CSS and JS -->
    <link href="css/select2.min.css" rel="stylesheet" />
    <script src="js/select2.min.js"></script>

    <!-- Bootstrap Selectpicker CSS and JS -->
    <link href="css/bootstrap-select.min.css" rel="stylesheet" />
    <script src="js/bootstrap-select.min.js"></script>


    <script>
        $(document).ready(function () {
            // To style only selects with the my-select class
            $('.selectpicker select').selectpicker();

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                setTimeout(function () {
                    $('.selectpicker select').selectpicker('refresh');
                }, 0);
            });
        });
    </script>

 
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <div class="mt-3">

            <div class="row mt-3 mb-3">
                <div class="col-md-3 ml-3">
                    <label for="ddlOANo" class="col-form-label s-12">OA No</label>
                    <asp:DropDownList ID="ddlOANo" runat="server" Class="form-control selectpicker select-search-custom"
                        data-live-search="true" data-size="5" data-width="100%" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"
                        Style="border: 1px solid #ced4da; border-radius: 4px; box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);">
                    </asp:DropDownList>
                </div>
                
                <div class="col-md-3 ml-3">
                    <label for="ddlBureau" class="col-form-label s-12">Bureau</label>
                    <asp:DropDownList ID="ddlBureau" runat="server" CssClass="form-control selectpicker select-search-custom"
                        data-live-search="true" data-size="5" data-width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>

                <div class="col-md-3 ml-3">
                    <label for="ddlCircle" class="col-form-label s-12">Circle</label>
                    <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-control selectpicker select-search-custom"
                        data-live-search="true" data-size="5" data-width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row mt-2 mb-3">
                <div class="col-md-3 ml-3">
                    <label for="txtFromDate" class="col-form-label s-12">From Date</label>
                    <input type="date" id="txtFromDate" runat="server" class="form-control" />
                </div>

                <div class="col-md-3 ml-3">
                    <label for="txtToDate" class="col-form-label s-12">To Date</label>
                    <input type="date" id="txtToDate" runat="server" class="form-control" />
                </div>
                <div class="col-md-3 mt-3 ml-2">

                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-info mt-3" Text="Search" OnClick="btnSearch_Click" />
                </div>
            </div>
        

        <div class="card no-b  no-r">
            <telerik:radgrid id="Radgrid1" runat="server" viewstatemode="Enabled" onneeddatasource="Radgrid1_NeedDataSource" skin="Office2007" cssclass="borderLessDialog" allowpaging="true" allowsorting="true" allowfilteringbycolumn="true"
                showgrouppanel="true" showfooter="true"
                pagesize="30" autogeneratecolumns="false" border-spacing="false" allowmulticolumnsorting="true">


                <GroupingSettings GroupByFieldsSeparator="" ShowUnGroupButton="True"></GroupingSettings>

                <HeaderStyle VerticalAlign="Top" BorderColor="#9eb6ce" BorderStyle="Groove" />

                <MasterTableView CommandItemDisplay="TopAndBottom" ShowGroupFooter="true" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true"
                    ClientDataKeyNames="RefID" AllowAutomaticInserts="true" AlternatingItemStyle-BackColor="Lavender">
                    <CommandItemSettings ShowExportToExcelButton="true" ShowAddNewRecordButton="false" ShowExportToPdfButton="true"  />
                    <Columns>
                        <telerik:GridBoundColumn DataField="RefID" HeaderText="Reference ID" SortExpression="RefID" HeaderStyle-Font-Bold="true" FooterStyle-Height="10px" Display="false"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="OANo" HeaderText="OA No" SortExpression="OANo" HeaderStyle-Font-Bold="true" FooterStyle-Height="10px"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CaseDate" HeaderText="Case Date" SortExpression="CaseDate" HeaderStyle-Font-Bold="true" FooterStyle-Height="10px"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="BuroCity" HeaderText="Bureau" SortExpression="BuroCity" HeaderStyle-Font-Bold="true" FooterStyle-Height="10px"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ApplicantName" HeaderText="Applicant Name" SortExpression="ApplicantName" HeaderStyle-Font-Bold="true" FooterStyle-Height="10px"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ApplicantsCategory" HeaderText="Applicants Category" SortExpression="ApplicantCaty" HeaderStyle-Font-Bold="true" FooterStyle-Height="10px"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ConOfficeName" HeaderText="Concerned Offices" SortExpression="ConOfficeName" HeaderStyle-Font-Bold="true" FooterStyle-Height="10px"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CircleName" HeaderText="Health Circle" SortExpression="CircleName" HeaderStyle-Font-Bold="true" FooterStyle-Height="10px"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CourtName" HeaderText="Court" SortExpression="CourtName" HeaderStyle-Font-Bold="true" FooterStyle-Height="10px"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="RespoDesig" HeaderText="Respondent1 Designation" SortExpression="RespoDesig" HeaderStyle-Font-Bold="true" FooterStyle-Height="10px"></telerik:GridBoundColumn>
                        
                    </Columns>
                </MasterTableView>
                <PagerStyle AlwaysVisible="true" Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                <ExportSettings ExportOnlyData="true" IgnorePaging="true" Excel-Format="ExcelML" OpenInNewWindow="true">
                    <Pdf PageWidth="1500px" PaperSize="A4" DefaultFontFamily="Arial" BorderStyle="Thin" BorderType="AllBorders"></Pdf>
                </ExportSettings>
                <ClientSettings AllowDragToGroup="true" AllowColumnsReorder="true">
                    <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                    <Resizing AllowColumnResize="true" />
                </ClientSettings>
            </telerik:radgrid>
        </div>
        </div>
    </form>


</body>
</html>
