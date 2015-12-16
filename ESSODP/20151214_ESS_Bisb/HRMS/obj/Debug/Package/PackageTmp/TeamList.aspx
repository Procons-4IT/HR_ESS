<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/HRMS.Master"
    CodeBehind="TeamList.aspx.vb" Inherits="HRMS.TeamList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .modalPopup
        {
            background-color: #696969;
            filter: alpha(opacity=40);
            opacity: 0.7;
            xindex: -1;
        }
        
        
        #marqueecontainer
        {
            position: relative;
            width: 200px; /*marquee width */
            height: 200px; /*marquee height */
            background-color: white;
            overflow: hidden;
            border: 3px solid LightSteelBlue;
            padding: 2px;
            padding-left: 4px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <asp:Image ID="Image1" ImageUrl="Images/waiting.gif" AlternateText="Processing" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <ajx:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    <table width="100%" border="0" cellspacing="0" cellpadding="4" class="main_content">
        <tr>
            <td align="center">
                <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
                    <tr>
                        <td height="30" align="left" colspan="2" valign="bottom" background="images/h_bg.png"
                            style="border-bottom: 1px dotted; border-color: #f45501; background-repeat: repeat-x">
                            <div>
                                &nbsp;
                                <asp:Label ID="Label3" runat="server" Text="Team List" CssClass="subheader" Style="float: left;"></asp:Label>
                                <span>
                                    <asp:Label ID="lblNewTrip" runat="server" Text="" Visible="false"></asp:Label></span>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="panelhome" runat="server" Width="100%">
                                <asp:ImageButton ID="btnhome" runat="server" ImageUrl="images/Homeicon.jpg" PostBackUrl="~/Home.aspx"
                                    ToolTip="Home" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:ImageButton ID="btnnew" runat="server" ImageUrl="images/Add.jpg" ToolTip="Add new record"
                                    Visible="false" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:Panel>
                            <asp:Label ID="Label2" runat="server" Text="" Style="color: Red;"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="panelnew" runat="server" Width="98%">
                    <div id="Div1" runat="server" class="DivCorner" style="border: solid 2px LightSteelBlue;
                        width: 100%;">
                        <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
                            <tr>
                                <td>
                                    <asp:GridView ID="grdTeamList" runat="server" CellPadding="4" AllowPaging="True"
                                        CssClass="mGrid" HeaderStyle-CssClass="GridBG" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                        AutoGenerateColumns="False" Width="100%" PageSize="10" ShowHeaderWhenEmpty="true"
                                        EmptyDataText="No records Found">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Employee Id">
                                                <ItemTemplate>
                                                    <div align="center">
                                                        <asp:Label ID="lblempid" runat="server" Text='<%#Bind("empID") %>'></asp:Label></div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee Name">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        &nbsp;<asp:Label ID="lblcust" runat="server" Text='<%#Bind("EmpName") %>'></asp:Label></div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Position">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        &nbsp;<asp:Label ID="lblitem" runat="server" Text='<%#Bind("Positionname") %>'></asp:Label></div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Department">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        &nbsp;<asp:Label ID="lblitemdesc" runat="server" Text='<%#Bind("Deptname") %>'></asp:Label></div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Email Id">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        &nbsp;<asp:Label ID="lblserial" runat="server" Text='<%#Bind("email") %>'></asp:Label></div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Home Phone">
                                                <ItemTemplate>
                                                    <div align="left">
                                                        &nbsp;<asp:Label ID="lblstatus" runat="server" Text='<%#Bind("homeTel") %>'></asp:Label></div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <div align="left">
                                                        &nbsp;<asp:LinkButton ID="lbtnactivity" runat="server" Text="Activity" OnClick="lbtnactivity_Click"></asp:LinkButton></div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle HorizontalAlign="Center" Height="25px" BackColor="#CCCCCC" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
                <div style="visibility: hidden">
                    <asp:Button ID="btnSample" runat="server" />
                </div>
            </td>
            <ajx:ModalPopupExtender ID="ModalPopupExtender7" runat="server" DropShadow="True"
                PopupControlID="Panelpoptechnician" TargetControlID="btnSample" CancelControlID="btnclstech"
                BackgroundCssClass="modalBackground">
            </ajx:ModalPopupExtender>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="Panelpoptechnician" runat="server" BackColor="White" Style="display: none;
                    padding: 10px; width: 900px;">
                    <div>
                        <span class="sideheading" style="color: Green;">Activity Details</span> <span style="float: right;">
                            <asp:Button ID="btnclstech" runat="server" CssClass="btn" Width="30px" Text="X" /></span></div>
                    <br />
                    <br />
                    <asp:Panel ID="Panel4" runat="server" Height="400px" ScrollBars="Vertical">
                        <asp:Label ID="Label1" runat="server" Text="" CssClass="txtbox" ForeColor="Red"></asp:Label>
                        <asp:GridView ID="grdRequesttohr" runat="server" CellPadding="4" ShowHeaderWhenEmpty="true"
                            CssClass="mGrid" HeaderStyle-CssClass="GridBG" AlternatingRowStyle-CssClass="alt"
                            AutoGenerateColumns="false" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="Request Code">
                                    <ItemTemplate>
                                        <div align="center">
                                            <asp:Label ID="lbtndocnum" runat="server" Text='<%#Bind("clgCode") %>'></asp:Label></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Activity">
                                    <ItemTemplate>
                                        <div align="center">
                                            <asp:Label ID="lblactivity" runat="server" Text='<%#Bind("Action") %>'></asp:Label></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type">
                                    <ItemTemplate>
                                        <div align="center">
                                            <asp:Label ID="lbltype" runat="server" Text='<%#Bind("Name") %>'></asp:Label></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Subject">
                                    <ItemTemplate>
                                        <div align="center">
                                            <asp:Label ID="lblactsubject" runat="server" Text='<%#Bind("Subject") %>'></asp:Label></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Assigned To Employee">
                                    <ItemTemplate>
                                        <div align="center">
                                            <asp:Label ID="lblAssigned" runat="server" Text='<%#Bind("EmpName") %>'></asp:Label></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Assigned To User">
                                    <ItemTemplate>
                                        <div align="center">
                                            <asp:Label ID="lbluser" runat="server" Text='<%#Bind("UserName") %>'></asp:Label></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Start Date">
                                    <ItemTemplate>
                                        <div align="left">
                                            &nbsp;<asp:Label ID="lblstdate" runat="server" Text='<%#Bind("Recontact") %>' Width="80px"></asp:Label></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="End Date">
                                    <ItemTemplate>
                                        <div align="left">
                                            &nbsp;<asp:Label ID="lbledDate" runat="server" Text='<%#Bind("endDate") %>' Width="80px"></asp:Label></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <div align="left">
                                            &nbsp;<asp:Label ID="lblRejoin" runat="server" Text='<%#Bind("Details") %>'></asp:Label></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Priority">
                                    <ItemTemplate>
                                        <div align="left">
                                            &nbsp;<asp:Label ID="lblreason" runat="server" Text='<%#Bind("Priority") %>'></asp:Label></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <div align="left">
                                            &nbsp;<asp:Label ID="lblstatus" runat="server" Text='<%#Bind("Status") %>'></asp:Label></div>
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
</asp:Content>
