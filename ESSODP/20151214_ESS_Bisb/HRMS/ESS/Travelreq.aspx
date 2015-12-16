<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ESS/ESSMaster.Master"
    CodeBehind="Travelreq.aspx.vb" Inherits="HRMS.Travelreq" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Confirmation1() {
            if (confirm("Sure Want to withdraw the request?") == true) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <style type="text/css">
        .modalPopup
        {
            background-color: #696969;
            filter: alpha(opacity=40);
            opacity: 0.7;
            xindex: -1;
        }
    </style>
    <style type="text/css">
        /*Calendar Control CSS*/
        .cal_Theme1 .ajax__calendar_container
        {
            background-color: #DEF1F4;
            border: solid 1px #77D5F7;
        }
        
        .cal_Theme1 .ajax__calendar_header
        {
            background-color: #ffffff;
            margin-bottom: 4px;
        }
        
        .cal_Theme1 .ajax__calendar_title, .cal_Theme1 .ajax__calendar_next, .cal_Theme1 .ajax__calendar_prev
        {
            color: #004080;
            padding-top: 3px;
        }
        
        .cal_Theme1 .ajax__calendar_body
        {
            background-color: #ffffff;
            border: solid 1px #77D5F7;
        }
        
        .cal_Theme1 .ajax__calendar_dayname
        {
            text-align: center;
            font-weight: bold;
            margin-bottom: 4px;
            margin-top: 2px;
            color: #004080;
        }
        
        .cal_Theme1 .ajax__calendar_day
        {
            color: #004080;
            text-align: center;
        }
        
        .cal_Theme1 .ajax__calendar_hover .ajax__calendar_day, .cal_Theme1 .ajax__calendar_hover .ajax__calendar_month, .cal_Theme1 .ajax__calendar_hover .ajax__calendar_year, .cal_Theme1 .ajax__calendar_active
        {
            color: #004080;
            font-weight: bold;
            background-color: #DEF1F4;
        }
        
        .cal_Theme1 .ajax__calendar_today
        {
            font-weight: bold;
        }
        
        .cal_Theme1 .ajax__calendar_other, .cal_Theme1 .ajax__calendar_hover .ajax__calendar_today, .cal_Theme1 .ajax__calendar_hover .ajax__calendar_title
        {
            color: #bbbbbb;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <asp:Image ID="Image1" ImageUrl="../Images/waiting.gif" AlternateText="Processing"
                runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <ajx:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    <asp:UpdatePanel ID="Update" runat="server">
        <ContentTemplate>
            <table width="100%" border="0" cellspacing="0" cellpadding="4" class="main_content">
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td height="30" align="left" colspan="2" valign="bottom" background="images/h_bg.png"
                        style="border-bottom: 1px dotted; border-color: #f45501; background-repeat: repeat-x">
                        <div>
                            &nbsp;
                            <asp:Label ID="Label3" runat="server" Text="Travel Request" CssClass="subheader"
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
                                        <asp:ImageButton ID="btnhome" runat="server" ImageUrl="~/images/Homeicon.jpg" PostBackUrl="~/ESS/ESSHome.aspx"
                                            ToolTip="Home" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="btnnew" runat="server" ImageUrl="~/images/Add.jpg" ToolTip="Add new record" />
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
                                                        <ajx:TabPanel ID="TabPanel3" runat="server" HeaderText="Travel Request Details">
                                                            <ContentTemplate>
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:GridView ID="grdTravelRequest" runat="server" CellPadding="4" ShowHeaderWhenEmpty="true"
                                                                                EmptyDataText="No records Found" HeaderStyle-CssClass="GridBG" AllowPaging="True"
                                                                                AutoGenerateColumns="False" Width="100%" PageSize="15" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                                                                AlternatingRowStyle-CssClass="alt">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="Request No.">
                                                                                        <ItemTemplate>
                                                                                            <div align="center">
                                                                                                <asp:LinkButton ID="lbtndocnum" runat="server" Text='<%#Bind("DocEntry") %>' OnClick="lbtndocnum_Click"></asp:LinkButton></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Request Date">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &nbsp;<asp:Label ID="lblcust" runat="server" Text='<%#Bind("U_Z_DocDate") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Emp.Id">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &nbsp;<asp:Label ID="lblcustName" runat="server" Text='<%#Bind("U_Z_EmpId") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Emp.Name">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &nbsp;<asp:Label ID="lblitem" runat="server" Text='<%#Bind("U_Z_EmpName") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Department">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &nbsp;<asp:Label ID="lblitemdesc" runat="server" Text='<%#Bind("U_Z_DeptName") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Position">
                                                                                        <ItemTemplate>
                                                                                            <div align="center">
                                                                                                &nbsp;<asp:Label ID="lblserial" runat="server" Text='<%#Bind("U_Z_PosName") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Travel Description">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                <asp:Label ID="lblsubject" runat="server" Text='<%#Bind("U_Z_TraName") %>'></asp:Label>&nbsp;&nbsp;</div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="From Location">
                                                                                        <ItemTemplate>
                                                                                            <div align="center">
                                                                                                &nbsp;<asp:Label ID="lblstatus" runat="server" Text='<%#Bind("U_Z_TraStLoc") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="To Location">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &nbsp;<asp:Label ID="lblUsername" runat="server" Text='<%#Bind("U_Z_TraEdLoc") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Travel Start Date">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                <asp:Label ID="lblpriority" runat="server" Text='<%#Bind("U_Z_TraStDate") %>'></asp:Label>&nbsp;&nbsp;</div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Travel End Date">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &nbsp;<asp:Label ID="lblcallType" runat="server" Text='<%#Bind("U_Z_TraEndDate") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Status">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &nbsp;<asp:Label ID="lblTripStatus" runat="server" Text='<%# Eval("U_Z_AppStatus") %>'></asp:Label></div>
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
                                                                                    <asp:TemplateField HeaderText="Approval History">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &nbsp;<asp:LinkButton ID="lbtAppHistory" runat="server" Text="View" OnClick="lbtAppHistory_Click"></asp:LinkButton></div>
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
                                    <asp:Panel ID="panelnew" runat="server" Width="100%">
                                        <div class="DivCorner" style="border: solid 2px LightSteelBlue; width: 100%;">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="4" class="main_content">
                                                <tr>
                                                    <td width="15%">
                                                        Request Number
                                                    </td>
                                                    <td width="15%">
                                                        <asp:TextBox ID="txtReqno" CssClass="txtbox" Width="150px" ReadOnly="true" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td width="10%">
                                                    </td>
                                                    <td width="15%">
                                                        Request Date
                                                    </td>
                                                    <td width="15%">
                                                        <asp:TextBox ID="txtreqdate" CssClass="txtbox" Width="150px" ReadOnly="true" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="10%">
                                                        Employee Id
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtempid" CssClass="txtbox" Width="150px" ReadOnly="true" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td width="15%">
                                                    </td>
                                                    <td width="10%">
                                                        Employee Name
                                                    </td>
                                                    <td width="20%">
                                                        <asp:TextBox ID="txtempname" CssClass="txtbox" Width="150px" ReadOnly="true" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="10%">
                                                        Position Code
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtposid" CssClass="txtbox" Width="150px" ReadOnly="true" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td width="15%">
                                                    </td>
                                                    <td width="10%">
                                                        Position Name
                                                    </td>
                                                    <td width="20%">
                                                        <asp:TextBox ID="txtposname" CssClass="txtbox" Width="150px" ReadOnly="true" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="10%">
                                                        Department
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtdeptName" CssClass="txtbox" Width="150px" ReadOnly="true" Enabled="true"
                                                            runat="server"></asp:TextBox>
                                                        <asp:TextBox ID="txtdept" CssClass="txtbox" Width="150px" Visible="false" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td width="15%">
                                                    </td>
                                                    <td width="10%">
                                                        Status
                                                    </td>
                                                    <td width="20%">
                                                        <asp:DropDownList ID="ddlstatus" CssClass="txtbox1" Width="120px" runat="server"
                                                            Enabled="false">
                                                            <asp:ListItem Value="P">Pending</asp:ListItem>
                                                            <asp:ListItem Value="A">Approved</asp:ListItem>
                                                            <asp:ListItem Value="R">Rejected</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <asp:Panel ID="paltab" runat="server" Width="100%" BorderColor="LightSteelBlue" BorderWidth="2">
                                            <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
                                                <tr>
                                                    <td valign="top">
                                                        <ajx:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" CssClass="ajax__tab_yuitabview-theme"
                                                            Width="100%">
                                                            <ajx:TabPanel ID="TabPanel1" runat="server" HeaderText="Trip Request Details">
                                                                <ContentTemplate>
                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content">
                                                                        <tr>
                                                                            <td width="15%">
                                                                                Trip Code
                                                                            </td>
                                                                            <td width="15%">
                                                                                <asp:DropDownList ID="ddltrip" CssClass="txtbox1" Width="150px" runat="server" AutoPostBack="true">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td width="10%">
                                                                            </td>
                                                                            <td width="20%">
                                                                                Trip Name
                                                                            </td>
                                                                            <td width="10%">
                                                                                <asp:TextBox ID="txttripName" CssClass="txtbox" Width="150px" Enabled="false" runat="server"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="10%">
                                                                                From Location
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtfromloc" CssClass="txtbox" Width="150px" ReadOnly="false" runat="server"></asp:TextBox>
                                                                            </td>
                                                                            <td width="15%">
                                                                            </td>
                                                                            <td width="10%">
                                                                                To Location
                                                                            </td>
                                                                            <td width="20%">
                                                                                <asp:TextBox ID="txttoloc" CssClass="txtbox" Width="150px" ReadOnly="false" runat="server"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="10%">
                                                                                Trip Start Date
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtstartdate" CssClass="txtbox" Width="150px" ReadOnly="false" runat="server"></asp:TextBox>
                                                                                <ajx:CalendarExtender ID="CalendarExtender3" Animated="true" Format="dd/MM/yyyy"
                                                                                    CssClass="cal_Theme1" runat="server" TargetControlID="txtstartdate">
                                                                                </ajx:CalendarExtender>
                                                                            </td>
                                                                            <td width="15%">
                                                                            </td>
                                                                            <td width="10%">
                                                                                Trip End Date
                                                                            </td>
                                                                            <td width="20%">
                                                                                <asp:TextBox ID="txtenddate" CssClass="txtbox" Width="150px" ReadOnly="false" runat="server"></asp:TextBox>
                                                                                <ajx:CalendarExtender ID="CalendarExtender1" Animated="true" Format="dd/MM/yyyy"
                                                                                    CssClass="cal_Theme1" runat="server" TargetControlID="txtenddate">
                                                                                </ajx:CalendarExtender>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="10%">
                                                                                New Travel Request
                                                                            </td>
                                                                            <td>
                                                                                <asp:CheckBox ID="chkNew" runat="server" AutoPostBack="true" />
                                                                            </td>
                                                                            <td width="15%">
                                                                            </td>
                                                                            <td width="10%">
                                                                            </td>
                                                                            <td width="20%">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="10%">
                                                                                Requested Date
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtreqdt" CssClass="txtbox" Width="150px" ReadOnly="true" runat="server"></asp:TextBox>
                                                                            </td>
                                                                            <td width="15%">
                                                                            </td>
                                                                            <td width="15%">
                                                                                Request Approved Date
                                                                            </td>
                                                                            <td width="20%">
                                                                                <asp:TextBox ID="txtreqappdt" CssClass="txtbox" Width="150px" ReadOnly="true" runat="server"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="12%">
                                                                                Request Claim Date
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtreqcldt" CssClass="txtbox" Width="150px" ReadOnly="true" runat="server"></asp:TextBox>
                                                                            </td>
                                                                            <td width="15%">
                                                                            </td>
                                                                            <td width="12%">
                                                                                Approved Claim Date
                                                                            </td>
                                                                            <td width="20%">
                                                                                <asp:TextBox ID="txtappcldt" CssClass="txtbox" Width="150px" ReadOnly="true" runat="server"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ContentTemplate>
                                                            </ajx:TabPanel>
                                                            <ajx:TabPanel ID="TabPanel2" runat="server" HeaderText="Comments">
                                                                <ContentTemplate>
                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="4" class="main_content">
                                                                        <tr>
                                                                            <td width="50%" style="height: 66px">
                                                                                <asp:TextBox ID="txtRemarks" CssClass="txtbox" Width="860px" TextMode="MultiLine"
                                                                                    Height="101px" runat="server"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ContentTemplate>
                                                            </ajx:TabPanel>
                                                            <ajx:TabPanel ID="TabPanel5" runat="server" HeaderText="Expenses" Visible="false">
                                                                <ContentTemplate>
                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="4" class="main_content">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:GridView ID="grdExpenses" runat="server" CellPadding="4" ShowHeaderWhenEmpty="true"
                                                                                    EmptyDataText="No records Found" CssClass="mGrid" HeaderStyle-CssClass="GridBG"
                                                                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowPaging="True"
                                                                                    AutoGenerateColumns="False" Width="100%" PageSize="10">
                                                                                    <Columns>
                                                                                        <%--   <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <div align="center"><asp:Label ID="lbline" runat="server" Text='<%#Bind("LineId") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>--%>
                                                                                        <asp:TemplateField HeaderText="Expenses Name">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &nbsp;<asp:Label ID="lblexpname" runat="server" Text='<%#Bind("U_Z_ExpName") %>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Budget Amount">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &nbsp;<asp:Label ID="lblbtamt" runat="server" Text='<%#Bind("U_Z_Amount") %>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Utilized Amount">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &nbsp;<asp:Label ID="lblutlamt" runat="server" Text='<%#Bind("U_Z_UtilAmt") %>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Balance Amount">
                                                                                            <ItemTemplate>
                                                                                                <div align="center">
                                                                                                    &nbsp;<asp:Label ID="lblbalamt" runat="server" Text='<%#Bind("U_Z_BalAmount") %>'></asp:Label></div>
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
                                        <table>
                                            <tr>
                                                <td colspan="5" align="center">
                                                    <br />
                                                    <asp:Button ID="btnsubmit" CssClass="btn" Width="85px" runat="server" Text="Save" />
                                                    <asp:Button ID="btnUpdate" CssClass="btn" Width="85px" runat="server" Text="Update"
                                                        Visible="false" />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnclose" CssClass="btn" Width="85px" runat="server" Text="Cancel" />
                                                    <asp:Button ID="btnWithdraw" CssClass="btn" runat="server" Text="WithDraw Request"
                                                        Visible="false" OnClientClick="return Confirmation1();" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="visibility: hidden">
                            <asp:Button ID="btnSample" runat="server" />
                        </div>
                        <ajx:ModalPopupExtender ID="ModalPopupExtender7" runat="server" DropShadow="True"
                            PopupControlID="Panelpoptechnician" TargetControlID="btnSample" CancelControlID="btnclstech"
                            BackgroundCssClass="modalBackground">
                        </ajx:ModalPopupExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="Panelpoptechnician" runat="server" BackColor="White" Style="display: none;
                            padding: 10px; width: 900px;">
                            <div>
                                <span class="sideheading" style="color: Green;">Approval History Details</span>
                                <span style="float: right;">
                                    <asp:Button ID="btnclstech" runat="server" CssClass="btn" Width="30px" Text="X" /></span></div>
                            <br />
                            <br />
                            <asp:Panel ID="Panel4" runat="server" Height="400px">
                                <asp:Label ID="Label1" runat="server" Text="" CssClass="txtbox" ForeColor="Red"></asp:Label>
                                <asp:GridView ID="grdRequesttohr" runat="server" CellPadding="4" ShowHeaderWhenEmpty="true"
                                    CssClass="mGrid" HeaderStyle-CssClass="GridBG" AlternatingRowStyle-CssClass="alt"
                                    AutoGenerateColumns="false" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Employee ID">
                                            <ItemTemplate>
                                                <div align="center">
                                                    <asp:Label ID="lbtndocnum" runat="server" Text='<%#Bind("U_Z_EmpId") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employee Name">
                                            <ItemTemplate>
                                                <div align="center">
                                                    <asp:Label ID="lblactivity" runat="server" Text='<%#Bind("U_Z_EmpName") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Approved By">
                                            <ItemTemplate>
                                                <div align="center">
                                                    <asp:Label ID="lbltype" runat="server" Text='<%#Bind("U_Z_ApproveBy") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Next Approver">
                                            <ItemTemplate>
                                                <div align="center">
                                                    <asp:Label ID="lblhapprover" runat="server" Text='<%#Bind("U_Z_NxtApprover") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CreateDate">
                                            <ItemTemplate>
                                                <div align="center">
                                                    <asp:Label ID="lblcrDate" runat="server" Text='<%#Bind("CreateDate") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Create Time">
                                            <ItemTemplate>
                                                <div align="center">
                                                    <asp:Label ID="lblcrtime" runat="server" Text='<%#Bind("CreateTime") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Update Date">
                                            <ItemTemplate>
                                                <div align="center">
                                                    <asp:Label ID="lblupdate" runat="server" Text='<%#Bind("UpdateDate") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Update Time">
                                            <ItemTemplate>
                                                <div align="center">
                                                    <asp:Label ID="lblupdatime" runat="server" Text='<%#Bind("UpdateTime") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Approved Status">
                                            <ItemTemplate>
                                                <div align="center">
                                                    <asp:Label ID="lblactsubject" runat="server" Text='<%#Bind("U_Z_AppStatus") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <div align="center">
                                                    <asp:Label ID="lblAssigned" runat="server" Text='<%#Bind("U_Z_Remarks") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle HorizontalAlign="Center" Height="25px" BackColor="#CCCCCC" />
                                </asp:GridView>
                            </asp:Panel>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
