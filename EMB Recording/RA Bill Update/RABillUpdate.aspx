<%@ Page Language="C#" UnobtrusiveValidationMode="None" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="RABillUpdate.aspx.cs" Inherits="RA_Bill_RABill" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>RA Billing</title>

    <!--Bootstrap CSS-->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous" />
    <!--jQuery-->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js" integrity="sha384-KyZXEAg3QhqLMpG8r+J2Wk5vqXn3Fm/z2N1r8f6VZJ4T3Hdvh4kXG1j4fZ6IsU2f5" crossorigin="anonymous"></script>
    <!--AJAX JS-->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
    <!--Bootstrap JS-->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-C6RzsynM9kWDrMNeT87bh95OGNyZPhcTNXj1NW7RuBCsyN/o0jlpcV8Qyq46cDfL" crossorigin="anonymous"></script>

    <!--Using JavaScript library such as Select2-->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.13/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.13/js/select2.min.js"></script>

    <script src="RABillUpdate.js"></script>
    <link rel="stylesheet" type="text/css" href="RABillUpdate.css" />

</head>
<body>
    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <div id="divTopSearch" runat="server" visible="true">
            <div class="justify-content-end d-flex mb-2 mt-4 px-0 mx-auto col-md-11">
                <div class="col-md-6">
                    <div class="fw-semibold fs-5 text-dark">
                        <asp:Literal ID="Literal14" Text="RA Billing" runat="server"></asp:Literal>
                    </div>
                </div>
                <div class="col-md-6 text-end">
                    <div class="fw-semibold fs-5">
                        <asp:Button ID="btnNewRaBill" runat="server" Text="New RA Bill +" OnClick="btnNewRaBill_Click" CssClass="btn btn-primary shadow" />
                    </div>
                </div>
            </div>

            <div id="divSearchEmb" runat="server" visible="true">
                <div class="card mt-1 shadow-sm mx-auto col-md-11">
                    <div class="card-body">
                        <div class="row mb-2">
                            <div class="form-row col-md-6 align-self-end">
                                <div class="form-group m-0">
                                    <div class="mb-1 text-body-tertiary fw-semibold">
                                        <asp:Literal ID="Literal13" Text="Project" runat="server"></asp:Literal>
                                    </div>
                                    <div class="border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddProject" runat="server" OnSelectedIndexChanged="ddProject_SelectedIndexChanged" AutoPostBack="true" class="form-control is-invalid" CssClass=""></asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div class="form-row col-md-6 align-self-end">
                                <div class="form-group m-0">
                                    <div class="mb-1 text-body-tertiary fw-semibold">
                                        <asp:Literal ID="Literal16" Text="Work Order" runat="server"></asp:Literal>
                                    </div>
                                    <div class="border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddWOName" runat="server" OnSelectedIndexChanged="ddWOName_SelectedIndexChanged" AutoPostBack="true" class="form-control is-invalid"></asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row mb-2">
                            <div class="form-row col-md-6 align-self-end">
                                <div class="form-group m-0">
                                    <div class="mb-1 text-body-tertiary fw-semibold">
                                        <asp:Literal ID="Literal15" Text="Vendor Name & Code" runat="server"></asp:Literal>
                                    </div>
                                    <div class="border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1">
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddVendorName" runat="server" OnSelectedIndexChanged="ddVendorName_SelectedIndexChanged" AutoPostBack="true" class="form-control is-invalid" CssClass=""></asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row mb-2">
                            <div class="form-row col-md-6 align-self-end">
                            </div>
                            <div class="form-row col-md-6 align-self-end text-end">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-primary col-md-2 shadow" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="mx-auto col-md-11">
            <div id="gridEmbDiv" visible="true" runat="server" class="mt-5">
                <div class="">
                    <asp:GridView ShowHeaderWhenEmpty="true" ID="gridRaHeader" runat="server" AutoGenerateColumns="false" OnRowCommand="GrdUser_RowCommand" AllowPaging="true" PageSize="10"
                        CssClass="table table-bordered border border-1 border-dark-subtle table-hover text-center" OnPageIndexChanging="GridEmbHeader_PageIndexChanging" PagerStyle-CssClass="gridview-pager">
                        <HeaderStyle CssClass="align-middle table-primary" />
                        <Columns>
                            <asp:TemplateField ControlStyle-CssClass="col-md-1" HeaderText="Sr.No">
                                <ItemTemplate>
                                    <asp:HiddenField ID="id" runat="server" Value="id" />
                                    <span>
                                        <%#Container.DataItemIndex + 1%>
                                    </span>
                                </ItemTemplate>
                                <ItemStyle CssClass="col-md-1" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="EmbWO" HeaderText="Work Order No." SortExpression="EmbWO" ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" />
                            <asp:BoundField DataField="EmbPM" HeaderText="Project Name" SortExpression="EmbPM" ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" />
                            <asp:BoundField DataField="EmbVenN" HeaderText="Vendor" SortExpression="EmbVenN" ItemStyle-Width="" ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" />
                            <asp:BoundField DataField="EmbAbstractDt" HeaderText="Recording Date" SortExpression="EmbAbstractDt" ItemStyle-Width="100px" ItemStyle-CssClass="text-center fw-light" />
                            <asp:BoundField DataField="EmbMeaFromDt" HeaderText="Measured From Date" SortExpression="EmbMeaFromDt" ItemStyle-Width="100px" ItemStyle-CssClass="text-center fw-light " />
                            <asp:BoundField DataField="EmbMeaToDt" HeaderText="Measured To Date" SortExpression="EmbMeaToDt" ItemStyle-Width="100px" ItemStyle-CssClass="text-center fw-light " />
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="btnedit" CommandArgument='<%# Eval("EmbMasRefId") %>' CommandName="lnkView" ToolTip="Edit" CssClass="shadow-sm">
                                        <asp:Image runat="server" ImageUrl="../img/edit.png" AlternateText="Edit" style="width: 16px; height: 16px;"/>
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>

    </form>
</body>
</html>
