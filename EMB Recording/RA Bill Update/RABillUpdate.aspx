﻿<%@ Page Language="C#" UnobtrusiveValidationMode="None" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="RABillUpdate.aspx.cs" Inherits="RA_Bill_RABill" %>

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
    <link rel="stylesheet" type="text/css" href="RABillUpdate.css"/>

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
                        <div class="row mb-2 mt-3">
                            <div class="form-row col-md-6 align-self-end text-start">
                                <asp:Button ID="btnTruncate" runat="server" Text="Truncate" OnClick="btnTruncate_Click" CssClass="btn btn-danger col-md-2 shadow" />
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
            <div id="gridRaUpdateDiv" visible="false" runat="server" class="mt-5">
                <div class="">
                    <asp:GridView ShowHeaderWhenEmpty="true" ID="gridRaHeader" runat="server" AutoGenerateColumns="false" OnRowCommand="GrdUser_RowCommand" AllowPaging="true" PageSize="10"
                        CssClass="table table-bordered border border-1 border-dark-subtle table-hover text-center" OnPageIndexChanging="GridEmbHeader_PageIndexChanging" PagerStyle-CssClass="gridview-pager">
                        <HeaderStyle CssClass="table-primary" />
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
                            <asp:BoundField DataField="RaVendor" HeaderText="Vendor" ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" />
                            <asp:BoundField DataField="RaBillNo" HeaderText="Bill No." ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" />
                            <asp:BoundField DataField="RaBillDate" HeaderText="Bill Date" ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" />
                            <asp:BoundField DataField="RaBasicAmount" HeaderText="Gross Amount" ItemStyle-CssClass="col-xs-3 align-middle text-start fw-light" />
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="btnedit" CommandArgument='<%# Eval("RaHeaderID") %>' CommandName="lnkView" ToolTip="Edit" CssClass="shadow-sm">
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







        <!-- Update Drop Downs -->

        <div id="divRAUpdate" runat="server" visible="false">

            <div class="mx-2 my-2 mx-auto col-md-11">
                <div class="fw-semibold fs-5 text-dark">
                    <asp:Literal ID="lit" Text="RA Billing Update" runat="server"></asp:Literal>
                </div>
            </div>

            <div class="card no-b no-r px-1 mt-1 shadow-sm mx-auto col-md-11">
                <div class="card-body">
                    <div class=" row mb-2">
                        <div class="form-row col-md-6 align-self-end">
                            <div class="form-group m-0">
                                <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                    <asp:Literal ID="Literal4" Text="Project Master" runat="server">Project Master</asp:Literal>
                                </div>
                                <div class="py-1">
                                    <asp:DropDownList ID="ddProjectMaster" ClientIDMode="Static" runat="server" class="form-control is-invalid"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-row col-md-6 align-self-end">
                            <div class="form-group m-0">
                                <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                    <asp:Literal ID="Literal6" Text="Work Order No." runat="server">Work Order No.</asp:Literal>
                                </div>
                                <div class="py-1">
                                    <asp:DropDownList ID="ddWorkOrder" ClientIDMode="Static" runat="server" class="form-control is-invalid"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class=" row mb-2">
                        <div class="form-row col-md-6 align-self-end">
                            <div class="form-group m-0">
                                <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                    <asp:Literal ID="Literal7" Text="Vendor Name" runat="server">Vendor Name</asp:Literal>
                                </div>
                                <div class="py-1">
                                    <asp:DropDownList ID="ddVender" ClientIDMode="Static" runat="server" class="form-control is-invalid"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-row col-md-6 align-self-end">
                            <div class="form-group m-0">
                                <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                    <asp:Literal ID="Literal3" Text="Abstract / EMB No." runat="server">Abstract / EMB No.</asp:Literal>
                                </div>
                                <div class="py-1">
                                    <asp:DropDownList ID="ddAbstractNo" ClientIDMode="Static" runat="server" class="form-control is-invalid"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class=" row mb-2">
                        <div class="form-row col-md-6 align-self-end">
                            <div class="form-group m-0">
                                <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                    <asp:Literal ID="Literal2" Text="Work Order Amount" runat="server"></asp:Literal>
                                </div>
                                <asp:TextBox runat="server" ID="txtWoAmnt" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-row col-md-6 align-self-end">
                            <div class="form-group m-0">
                                <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                    <asp:Literal ID="Literal8" Text="Total Bill Booked Amount" runat="server"></asp:Literal>
                                </div>
                                <asp:TextBox runat="server" ID="txtUpToTotalRaAmnt" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class=" row mb-2">
                        <div class="form-row col-md-12">
                            <div class="form-group m-0">
                                <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                    <asp:Literal ID="Literal9" Text="Remarks" runat="server"></asp:Literal>
                                </div>
                                <textarea runat="server" id="txtRemarks" readonly="readonly" class="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></textarea>
                            </div>
                        </div>
                    </div>

                </div>
            </div>

        </div>

        <div id="gridEmbDiv" runat="server" visible="false" class="mt-5 mx-auto col-md-11">
            <div class="">

                <asp:GridView ShowHeaderWhenEmpty="true" ID="gridDynamicBOQ" runat="server" AutoGenerateColumns="false"
                    CssClass="table table-bordered  border border-1 border-dark-subtle table-hover text-center">
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
                            <ItemStyle Font-Size="15px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="RaBoqItem" HeaderText="Item Description" ReadOnly="true" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />
                        <asp:BoundField DataField="RaUom" HeaderText="UOM" ReadOnly="true" ItemStyle-Font-Size="15px" ItemStyle-CssClass="col-md-1 align-middle fw-light" />
                        <asp:BoundField DataField="RaBoqQty" HeaderText="BoQ Qty" ItemStyle-Font-Size="15px" ItemStyle-CssClass="col-md-1 align-middle fw-light" />
                        <asp:BoundField DataField="RaAbstQty" HeaderText="Abstract Qty" ItemStyle-Font-Size="15px" ItemStyle-CssClass="col-md-1 align-middle fw-light" />
                        <asp:BoundField DataField="RaDiffQty" HeaderText="Diff In Qty" ItemStyle-Font-Size="15px" ItemStyle-CssClass="col-md-1 align-middle fw-light" />
                        <asp:BoundField DataField="RaItemRate" HeaderText="BoQ Unit Rate" ItemStyle-Font-Size="15px" ItemStyle-CssClass="col-md-1 align-middle fw-light" />
                    </Columns>
                </asp:GridView>

                <div class="mt-5 mb-2">
                    <div class="text-end">
                        <asp:Button ID="btnBasicAmount" runat="server" Text="Re-Calculate (Basic Amount)" OnClick="btnBasicAmount_Click" CssClass="btn btn-info text-white mb-3" />
                    </div>
                </div>

                <div class="row mb-3">
                    <div class="col-md-3 align-self-end">
                        <div class="form-group m-0">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal12" Text="Bill Date" runat="server">
                                    Bill Date
                                    <em style="color: red">*</em>
                                </asp:Literal>
                                <div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="dateBillDate" CssClass="invalid-feedback" InitialValue="" runat="server" ErrorMessage="(Please select the date)" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <asp:TextBox runat="server" ID="dateBillDate" type="date" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3 align-self-end">
                        <div class="form-group m-0">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal10" Text="Bill No." runat="server">
                                    Bill No.
                                    <em style="color: red">*</em>
                                </asp:Literal>
                                <div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtBillNo" CssClass="invalid-feedback" InitialValue="" runat="server" ErrorMessage="(Please select the date)" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <asp:TextBox runat="server" ID="txtBillNo" type="text" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3 align-self-end">
                        <div class="form-group m-0">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal1" Text="Payment Due Date" runat="server">
                                    Payment Due Date
                                    <em style="color: red">*</em>
                                </asp:Literal>
                                <div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="datePayDueDate" CssClass="invalid-feedback" InitialValue="" runat="server" ErrorMessage="(Please select the date)" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <asp:TextBox runat="server" ID="datePayDueDate" type="date" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3 align-self-end">
                        <asp:Literal ID="Literal5" Text="Payment Due Date" runat="server">
                            Basic Amount
                            <em style="color: red">*</em>
                        </asp:Literal>
                        <div class="input-group">
                            <span class="input-group-text fs-5 fw-semibold">₹</span>
                            <asp:TextBox runat="server" ID="txtBasicAmt" CssClass="form-control fw-lighter border border-2" ReadOnly="true" placeholder="Total Basic Amount"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <!-- Taxes / Account Head -->

                <hr class="border border-1 border-dark-subtle mt-4" />
                <div class="mx-2 text-start">
                    <div class="fw-normal fs-5 fw-medium text-body-secondary">
                        <asp:Literal ID="Literal18" Text="Taxes / Charges" runat="server"></asp:Literal>
                    </div>
                </div>
                <hr class="border border-1 border-dark-subtle" />

                <div class="mt-1 row">

                    <div class="col-md-12">
                        <asp:GridView ShowHeaderWhenEmpty="true" ID="GridTax" runat="server" AutoGenerateColumns="false" OnRowDataBound="GridTax_RowDataBound"
                            CssClass="table text-center shadow">
                            <HeaderStyle CssClass="align-middle table-secondary fw-light" />
                            <Columns>
                                <asp:BoundField DataField="DeductionHead" HeaderText="Deduction Head" ReadOnly="true" ItemStyle-Font-Size="15px" ItemStyle-CssClass="col-md-4 align-middle text-start fw-light" />

                                <asp:TemplateField HeaderText="Factor in %" ItemStyle-Font-Size="15px" ItemStyle-CssClass="col-md-2 align-middle">
                                    <ItemTemplate>
                                        <asp:TextBox ID="FactorInPer" runat="server" Enabled="true" CssClass="col-md-9 fw-light border border-secondary-subtle shadow-sm rounded-1 py-1 px-2" type="number" Text='<%# Bind("FactorInPer") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="% / Amount" ItemStyle-Font-Size="15px" ItemStyle-CssClass="col-md-2 align-middle">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="PerOrAmnt" runat="server" CssClass="col-md-6 text-center fw-light border border-secondary-subtle shadow-sm rounded-1 py-1 px-2"></asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Add / Less" ItemStyle-Font-Size="15px" ItemStyle-CssClass="col-md-2 align-middle">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="AddLess" runat="server" CssClass="col-md-6 text-center fw-light border border-secondary-subtle shadow-sm rounded-1 py-1 px-2"></asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Amount" ItemStyle-Font-Size="15px" ItemStyle-CssClass="col-md-3 align-middle">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TaxAmount" runat="server" Enabled="true" ReadOnly="true" CssClass="col-md-9 fw-light border border-secondary-subtle shadow-sm rounded-1 py-1 px-2" type="number" Text='<%# Bind("TaxAmount") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </div>

                    <div class="mt-3 mb-3">
                        <div class="text-end">
                            <asp:Button ID="btnReCalTax" Enabled="false" runat="server" Text="Re-Calculate (Taxes / Charges)" OnClick="btnReCalTax_Click" CssClass="btn btn-info text-white mb-3" />
                        </div>
                    </div>
                </div>

                <!-- Total Taxes & Net Amount -->

                <div class="mb-2">
                    <div class="row mb-3">
                        <div class="col-md-9 align-self-end">
                            <!--  -->
                        </div>
                        <div class="col-md-3 align-self-end">
                            <asp:Literal ID="Literal11" Text="Total Deductions :" runat="server"></asp:Literal>
                            <div class="input-group text-end">
                                <span class="input-group-text fs-5 fw-light">₹</span>
                                <asp:TextBox runat="server" ID="txtTotalDeduct" CssClass="form-control fw-lighter border border-2" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-md-9 align-self-end">
                            <!--  -->
                        </div>
                        <div class="col-md-3 align-self-end">
                            <asp:Literal ID="Literal17" Text="Total Additions :" runat="server"></asp:Literal>
                            <div class="input-group text-end">
                                <span class="input-group-text fs-5 fw-light">₹</span>
                                <asp:TextBox runat="server" ID="txtTotalAdd" CssClass="form-control fw-lighter border border-2" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-md-9 align-self-end">
                            <!--  -->
                        </div>
                        <div class="col-md-3 align-self-end">
                            <asp:Literal ID="Literal19" Text="Net Amount :" runat="server"></asp:Literal>
                            <div class="input-group text-end">
                                <span class="input-group-text fs-5 fw-light">₹</span>
                                <asp:TextBox runat="server" ID="txtNetAmnt" CssClass="form-control fw-lighter border border-2" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>




                <!-- File Upload -->

                <hr class="border border-1 border-dark-subtle mt-4" />
                <div class="text-start">
                    <div class="fw-normal fs-5 fw-medium text-body-secondary">
                        <asp:Literal ID="Literal20" Text="Document Upload" runat="server"></asp:Literal>
                    </div>
                </div>
                <hr class="border border-1 border-dark-subtle" />

                <div class="row mb-3 mb-2">
                    <div class="col-md-4 align-self-end">
                        <div class="form-group m-0">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal21" Text="Bill Date" runat="server">
                                    Document Type
                                    <em style="color: red"></em>
                                </asp:Literal>
                            </div>
                            <div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="ddDocType" CssClass="invalid-feedback" InitialValue="" runat="server" ErrorMessage="(Please select the doc type)" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                            </div>
                            <asp:DropDownList ID="ddDocType" ClientIDMode="Static" runat="server" class="form-control is-invalid"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4 align-self-end">
                        <div class="form-group m-0">
                            <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                <asp:Literal ID="Literal22" Text="Bill No." runat="server">
                                    Stages
                                    <em style="color: red"></em>
                                </asp:Literal>
                            </div>
                            <div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="ddStage" CssClass="invalid-feedback" InitialValue="" runat="server" ErrorMessage="(Please select the stage)" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                            </div>
                            <asp:DropDownList ID="ddStage" ClientIDMode="Static" runat="server" class="form-control is-invalid"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4 align-self-end">
                        <div class="form-group">
                            <div class="text-body-tertiary fw-semibold fs-6">
                                <label for="fileDoc" class="form-label">
                                    Upload Document
                                <em style="color: red"></em>
                                </label>
                            </div>
                            <div class="justify-content-center">
                                <div class="input-group has-validation">
                                    <asp:FileUpload ID="fileDoc" runat="server" CssClass="form-control" aria-describedby="inputGroupPrepend"  />
                                    <asp:Button ID="btnDocUpload" runat="server" OnClick="btnDocUpload_Click" Text="Upload" AutoPost="true" CssClass="btn btn-outline-secondary" />
                                    <div class="invalid-feedback">
                                        Please choose a file
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Document Grid -->

                <div class="mt-5">
                    <asp:GridView ShowHeaderWhenEmpty="true" ID="GridDocument" runat="server" AutoGenerateColumns="false" OnRowDataBound="GridTax_RowDataBound"
                        CssClass="table table-bordered border border-light-subtle text-start mt-3">
                        <HeaderStyle CssClass="align-middle table-secondary fw-light fs-6" />
                        <Columns>
                            <asp:BoundField DataField="DocType" HeaderText="Document Type" ReadOnly="true" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />
                            <asp:BoundField DataField="StageLevel" HeaderText="Levels" ReadOnly="true" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />
                            <asp:BoundField DataField="DocName" HeaderText="File Name" ReadOnly="true" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />

                            <asp:TemplateField HeaderText="View Document" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light">
                                <ItemTemplate>
                                    <asp:HyperLink ID="DocPath" runat="server" Text="View Uploaded Document" NavigateUrl='<%# Eval("DocPath") %>' Target="_blank" CssClass="text-decoration-none"></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

                <hr class="border border-2 border-dark-subtle mt-4" />

                <!-- Submit Button -->

                <div class="">
                    <div class="mt-5 mb-2">
                        <div class="text-end">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary text-white shadow mb-5" />
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </form>
</body>
</html>
