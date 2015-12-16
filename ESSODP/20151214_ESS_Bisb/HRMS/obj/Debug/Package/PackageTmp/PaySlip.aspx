<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/HRMS.Master"
    CodeBehind="PaySlip.aspx.vb" Inherits="HRMS.PaySlip" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
        <tr>
            <td height="30" align="left" colspan="2" valign="bottom" background="images/h_bg.png"
                style="border-bottom: 1px dotted; border-color: #f45501; background-repeat: repeat-x">
                <div>
                    &nbsp;
                    <asp:Label ID="Label3" runat="server" Text="PaySlip" CssClass="subheader" Style="float: left;"></asp:Label>
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
                                <asp:ImageButton ID="btnnew" runat="server" ImageUrl="~/images/Add.jpg" ToolTip="Add new record"
                                    Visible="false" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="Label2" runat="server" Text="" Style="color: Red;"></asp:Label>
                            </asp:Panel>
                            <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
                                <tr>
                                 <td width="15%">
                                    
                                    </td>
                                    <td width="5%">
                                        Month & Year :
                                    </td>
                                    <td width="15%">
                                        <asp:DropDownList ID="ddlMonth" CssClass="txtbox1" Width="160px" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                     <td width="15%">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Button ID="btnPrint" runat="server" CssClass="btn" Text="Print" Width="100px" />
                                    </td>
                                </tr>
                            </table>
                            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
                            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                            </CR:CrystalReportSource>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
