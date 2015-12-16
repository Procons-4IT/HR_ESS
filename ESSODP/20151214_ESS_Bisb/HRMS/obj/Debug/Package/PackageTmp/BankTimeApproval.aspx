<%@ Page Title="BankTime Approval" Language="vb" AutoEventWireup="false" MasterPageFile="~/HRMS.Master"
    CodeBehind="BankTimeApproval.aspx.vb" Inherits="HRMS.BankTimeApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function Confirmation() {
            if (confirm("Are you sure want to submit the document?") == true) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <asp:Image ID="Image1" ImageUrl="~/Images/waiting.gif" AlternateText="Processing"
                runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <ajx:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    <asp:UpdatePanel ID="Update" runat="server">
        <ContentTemplate>
            <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
                <tr>
                    <td height="30" align="left" colspan="2" valign="bottom" background="images/h_bg.png"
                        style="border-bottom: 1px dotted; border-color: #f45501; background-repeat: repeat-x">
                        <div>
                            &nbsp;
                            <asp:Label ID="Label3" runat="server" Text="Bank Time Approval" CssClass="subheader"
                                Style="float: left;"></asp:Label>
                            <span>
                                <asp:Label ID="lblNewTrip" runat="server" Text="" Visible="false"></asp:Label></span>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
                            <tr>
                                <td>
                                    <asp:Panel ID="panelhome" runat="server" Width="100%">
                                        <asp:ImageButton ID="btnhome" runat="server" ImageUrl="~/images/Homeicon.jpg" PostBackUrl="~/Home.aspx"
                                            ToolTip="Home" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </asp:Panel>
                                    <asp:Label ID="Label2" runat="server" Text="" Style="color: Red;"></asp:Label>
                                    <asp:Panel ID="panelview" runat="server" Width="100%" BorderColor="LightSteelBlue"
                                        BorderWidth="2">
                                        <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
                                            <tr>
                                                <td valign="top">
                                                    <ajx:TabContainer ID="TabContainer2" runat="server" ActiveTabIndex="0" CssClass="ajax__tab_yuitabview-theme"
                                                        Width="100%">
                                                        <ajx:TabPanel ID="TabPanel3" runat="server" HeaderText="Bank Time Approval">
                                                            <ContentTemplate>
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content">
                                                                    <tr>
                                                                        <td>
                                                                            <div style="overflow-x: scroll; width: 1100px;">
                                                                                <asp:GridView ID="grdRequestApproval" runat="server" CellPadding="4" AllowPaging="True"
                                                                                    ShowHeaderWhenEmpty="true" EmptyDataText="No Records Found" CssClass="mGrid"
                                                                                    HeaderStyle-CssClass="GridBG" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                                                    AutoGenerateColumns="false" Width="100%" PageSize="10">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Request Code">
                                                                                            <ItemTemplate>
                                                                                                <div align="center">
                                                                                                    <asp:LinkButton ID="lblCode" runat="server" Text='<%#Bind("Code") %>' OnClick="lbtndocnum_Click">
                                                                                                    </asp:LinkButton></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="T&A Emp.ID">
                                                                                            <ItemTemplate>
                                                                                                <div align="center">
                                                                                                    <asp:Label ID="lbltaEmpid" runat="server" Text='<%#Bind("U_Z_EmpId1") %>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Emp.Code">
                                                                                            <ItemTemplate>
                                                                                                <div align="center">
                                                                                                    <asp:Label ID="lblEmpid" runat="server" Text='<%#Bind("U_Z_EMPID") %>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Emp.Name">
                                                                                            <ItemTemplate>
                                                                                                <div align="center">
                                                                                                    <asp:Label ID="lblEmpname" runat="server" Text='<%#Bind("U_Z_EMPNAME") %>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Leave Code">
                                                                                            <ItemTemplate>
                                                                                                <div align="center">
                                                                                                    <asp:Label ID="lbllvecode" runat="server" Text='<%#Bind("U_Z_TrnsCode") %>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Leave Type">
                                                                                            <ItemTemplate>
                                                                                                <div align="center">
                                                                                                    <asp:Label ID="lbllvetype" runat="server" Text='<%#Bind("U_Z_LeaveName") %>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Start Date">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &nbsp;<asp:Label ID="lblstdate" runat="server" Text='<%#Bind("U_Z_StartDate") %>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="No.of Hours">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &nbsp;<asp:Label ID="lblsedDate" runat="server" Text='<%#Bind("U_Z_NoofHours") %>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="No.Of Days">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &nbsp;<asp:Label ID="lbldays" runat="server" Text='<%#Bind("U_Z_NoofDays") %>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Cash Out">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &nbsp;<asp:Label ID="lblCashout" runat="server" Text='<%#Bind("U_Z_CashOut") %>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Reason">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &nbsp;<asp:Label ID="lblreason" runat="server" Text='<%#Bind("U_Z_Notes") %>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Status">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &nbsp;<asp:Label ID="lblstatus" runat="server" Text='<%#Bind("U_Z_AppStatus") %>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Approver Remarks">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &nbsp;<asp:Label ID="lblAppRemark" runat="server" Text='<%#Bind("U_Z_AppRemarks") %>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Approval Required">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &#160;<asp:Label ID="lblAppreq" runat="server" Text='<%#Bind("U_Z_AppRequired") %>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Requested Date">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &#160;<asp:Label ID="lblreqdt" runat="server" Text='<%#Bind("U_Z_AppReqDate") %>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Requested Time" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &#160;<asp:Label ID="lblreqtime" runat="server" Text='<%#Bind("U_Z_ReqTime") %>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Current Approver">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &#160;<asp:Label ID="lblcurapp" runat="server" Text='<%#Bind("U_Z_CurApprover") %>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Next Approver">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &#160;<asp:Label ID="lblnxtapp" runat="server" Text='<%#Bind("U_Z_NxtApprover") %>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <HeaderStyle HorizontalAlign="Center" Height="25px" BackColor="#CCCCCC" />
                                                                                </asp:GridView>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content">
                                                                    <tr>
                                                                        <td valign="top">
                                                                            <div style="overflow-x: scroll; width: 700px;">
                                                                                <a class="txtbox" style="text-decoration: underline; font-weight: bold;">Approval History</a>
                                                                                <asp:GridView ID="grdApprovalHis" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt"
                                                                                    AutoGenerateColumns="false" CellPadding="4" CssClass="mGrid" EmptyDataText="No Records Found"
                                                                                    HeaderStyle-CssClass="GridBG" PagerStyle-CssClass="pgr" PageSize="10" ShowHeaderWhenEmpty="true"
                                                                                    Width="100%">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Request Code" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <div align="center">
                                                                                                    <asp:Label ID="lblHDocNo" runat="server" Text='<%#Bind("DocEntry") %>'>
                                                                                                    </asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Reference No" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <div align="center">
                                                                                                    <asp:Label ID="lblhrefno" runat="server" Text='<%#Bind("U_Z_DocEntry") %>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="DocType" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <div align="center">
                                                                                                    <asp:Label ID="lblhdoctype" runat="server" Text='<%#Bind("U_Z_DocType") %>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Employee ID">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &#160;<asp:Label ID="lblhempid" runat="server" Text='<%#Bind("U_Z_EmpId") %>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Employee Name">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &#160;<asp:Label ID="lblhempname" runat="server" Text='<%#Bind("U_Z_EmpName") %>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Approved By">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &#160;<asp:Label ID="lblhAppby" runat="server" Text='<%#Bind("U_Z_ApproveBy") %>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Next Approver">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &#160;<asp:Label ID="lblnxtapp" runat="server" Text='<%#Bind("U_Z_NxtApprover") %>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Create Date">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &#160;<asp:Label ID="lblhcrdate" runat="server" Text='<%#Bind("CreateDate") %>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Create Time">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &#160;<asp:Label ID="lblhcrtime" runat="server" Text='<%#Bind("CreateTime") %>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Update Date">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &#160;<asp:Label ID="lblhupdate" runat="server" Text='<%#Bind("UpdateDate") %>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Update Time">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &#160;<asp:Label ID="lblhuptime" runat="server" Text='<%#Bind("UpdateTime") %>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Approved Status">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &#160;<asp:Label ID="lblhAppstatus" runat="server" Text='<%#Bind("U_Z_AppStatus") %>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Remarks">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &#160;<asp:Label ID="lblhremarks" runat="server" Text='<%#Bind("U_Z_Remarks") %>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <HeaderStyle BackColor="#CCCCCC" Height="25px" HorizontalAlign="Center" />
                                                                                </asp:GridView>
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <table border="0" cellpadding="3" cellspacing="0" class="main_content" width="100%">
                                                                                <tr>
                                                                                    <td>
                                                                                        <a class="txtbox" style="text-decoration: underline; font-weight: bold;">Approvar Status</a>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtcode" runat="server" CssClass="txtbox" Visible="false"></asp:TextBox>
                                                                                        <asp:TextBox ID="txtempid" runat="server" CssClass="txtbox" Visible="false"></asp:TextBox><asp:TextBox
                                                                                            ID="txtlveCode" runat="server" CssClass="txtbox" Visible="false"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        Approval Status
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="ddlAppStatus" runat="server" CssClass="txtbox1">
                                                                                            <asp:ListItem Value="P">Pending</asp:ListItem>
                                                                                            <asp:ListItem Value="A">Approved</asp:ListItem>
                                                                                            <asp:ListItem Value="R">Rejected</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        Remarks
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtcomments" runat="server" CssClass="txtbox" Height="80px" TextMode="MultiLine"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2">
                                                                                        <br />
                                                                                        <asp:Button ID="btnAdd" runat="server" CssClass="btn" OnClientClick="return Confirmation();"
                                                                                            Text="Save &amp; Submit" Width="125px" />
                                                                                        <asp:Button ID="btncancel" runat="server" CssClass="btn" Text="Cancel" Width="85px" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </ajx:TabPanel>
                                                        <ajx:TabPanel ID="TabPanel1" runat="server" HeaderText="Bank Time Summary">
                                                            <ContentTemplate>
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:GridView ID="grdSummary" runat="server" CellPadding="4" AllowPaging="True" ShowHeaderWhenEmpty="true"
                                                                                EmptyDataText="No Records Found" CssClass="mGrid" HeaderStyle-CssClass="GridBG"
                                                                                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false"
                                                                                Width="100%" PageSize="10">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="Code">
                                                                                        <ItemTemplate>
                                                                                            <div align="center">
                                                                                                <asp:LinkButton ID="lblSCode" runat="server" Text='<%#Bind("Code") %>' OnClick="lbtndocnumSum_Click">
                                                                                                </asp:LinkButton></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="T&A Emp.ID">
                                                                                        <ItemTemplate>
                                                                                            <div align="center">
                                                                                                <asp:Label ID="lblStaEmpid" runat="server" Text='<%#Bind("U_Z_EmpId1") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Emp.Code">
                                                                                        <ItemTemplate>
                                                                                            <div align="center">
                                                                                                <asp:Label ID="lblsEmpid" runat="server" Text='<%#Bind("U_Z_EMPID") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Emp.Name">
                                                                                        <ItemTemplate>
                                                                                            <div align="center">
                                                                                                <asp:Label ID="lblsEmpname" runat="server" Text='<%#Bind("U_Z_EMPNAME") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Leave Code">
                                                                                        <ItemTemplate>
                                                                                            <div align="center">
                                                                                                <asp:Label ID="lbllvecode" runat="server" Text='<%#Bind("U_Z_TrnsCode") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Leave Type">
                                                                                        <ItemTemplate>
                                                                                            <div align="center">
                                                                                                <asp:Label ID="lbllvetype" runat="server" Text='<%#Bind("U_Z_LeaveName") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Start Date">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &nbsp;<asp:Label ID="lblstdate" runat="server" Text='<%#Bind("U_Z_StartDate") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="No.of Hours">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &nbsp;<asp:Label ID="lblsedDate" runat="server" Text='<%#Bind("U_Z_NoofHours") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="No.Of Days">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &nbsp;<asp:Label ID="lbldays" runat="server" Text='<%#Bind("U_Z_NoofDays") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Cash Out">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &nbsp;<asp:Label ID="lblCashout" runat="server" Text='<%#Bind("U_Z_CashOut") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Reason">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &nbsp;<asp:Label ID="lblreason" runat="server" Text='<%#Bind("U_Z_Notes") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Status">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &nbsp;<asp:Label ID="lblstatus" runat="server" Text='<%#Bind("U_Z_AppStatus") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Approver Remarks">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &nbsp;<asp:Label ID="lblAppRemark" runat="server" Text='<%#Bind("U_Z_AppRemarks") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Approver Required">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &#160;<asp:Label ID="lblsAppreq" runat="server" Text='<%#Bind("U_Z_AppRequired") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Requested Date">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &#160;<asp:Label ID="lblsreqdt" runat="server" Text='<%#Bind("U_Z_AppReqDate") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Requested Time" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &#160;<asp:Label ID="lblsreqtime" runat="server" Text='<%#Bind("U_Z_ReqTime") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Current Approver">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &#160;<asp:Label ID="lblscurapp" runat="server" Text='<%#Bind("U_Z_CurApprover") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Next Approver">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &#160;<asp:Label ID="lblsnxtapp" runat="server" Text='<%#Bind("U_Z_NxtApprover") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <HeaderStyle HorizontalAlign="Center" Height="25px" BackColor="#CCCCCC" />
                                                                            </asp:GridView>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content">
                                                                    <tr>
                                                                        <td>
                                                                            <a class="txtbox" style="text-decoration: underline; font-weight: bold;">Approval History</a>
                                                                            <br />
                                                                            <asp:GridView ID="grdHistorySummary" runat="server" CellPadding="4" AllowPaging="True"
                                                                                ShowHeaderWhenEmpty="true" EmptyDataText="No Records Found" CssClass="mGrid"
                                                                                HeaderStyle-CssClass="GridBG" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                                                AutoGenerateColumns="false" Width="100%" PageSize="10">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="Request Code" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <div align="center">
                                                                                                <asp:Label ID="lblDocNo" runat="server" Text='<%#Bind("DocEntry") %>'>
                                                                                                </asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Reference No" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <div align="center">
                                                                                                <asp:Label ID="lblrefno" runat="server" Text='<%#Bind("U_Z_DocEntry") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="DocType" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <div align="center">
                                                                                                <asp:Label ID="lbldoctype" runat="server" Text='<%#Bind("U_Z_DocType") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Employee ID">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &#160;<asp:Label ID="lblempid" runat="server" Text='<%#Bind("U_Z_EmpId") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Employee Name">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &#160;<asp:Label ID="lblempname" runat="server" Text='<%#Bind("U_Z_EmpName") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Approved By">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &#160;<asp:Label ID="lblAppby" runat="server" Text='<%#Bind("U_Z_ApproveBy") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Next Approver">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &#160;<asp:Label ID="lblsnxtapp" runat="server" Text='<%#Bind("U_Z_NxtApprover") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Create Date">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &#160;<asp:Label ID="lblcrdate" runat="server" Text='<%#Bind("CreateDate") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Create Time">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &#160;<asp:Label ID="lblcrtime" runat="server" Text='<%#Bind("CreateTime") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Update Date">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &#160;<asp:Label ID="lblupdate" runat="server" Text='<%#Bind("UpdateDate") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Update Time">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &#160;<asp:Label ID="lbluptime" runat="server" Text='<%#Bind("UpdateTime") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Approved Status">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &#160;<asp:Label ID="lblAppstatus" runat="server" Text='<%#Bind("U_Z_AppStatus") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Remarks">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &#160;<asp:Label ID="lblremarks" runat="server" Text='<%#Bind("U_Z_Remarks") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <HeaderStyle HorizontalAlign="Center" Height="25px" BackColor="#CCCCCC" />
                                                                            </asp:GridView>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </ajx:TabPanel>
                                                    </ajx:TabContainer>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
