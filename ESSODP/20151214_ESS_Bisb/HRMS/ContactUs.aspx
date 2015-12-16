<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/HRMS.Master" CodeBehind="ContactUs.aspx.vb" Inherits="HRMS.ContactUs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <table width="100%" border="0" cellspacing="0" cellpadding="4" class="main_content">
<tr>

    <td height="30" align="left" colspan="2" valign="bottom" background="images/h_bg.png"; style="border-bottom:1px dotted; border-color: #f45501; background-repeat:repeat-x">     
      <div >&nbsp; <asp:Label ID="Label3" runat="server" Text="ContactUs"  CssClass="subheader" style="float:left;" ></asp:Label>  <span ><asp:Label ID="lblNewTrip" runat="server" Text="" Visible="false"></asp:Label></span> </div>
      </td>    
 </tr>
 <tr>
 <td>
 <p style="color:Black; font:normal 11pt Calibri, Helvetica, sans-serif ; font-weight:bold; line-height:20pt; margin-left:20px;">
 You can contact the HR department either via LYNC or through telephone extensions for any queries <br /> related to this.<br /><br />
 Find below contact addresses :
 </p>
 </td>
 </tr>
 <tr>
 <td>
 <div style="margin-left:20px; font:normal 11pt Calibri, Helvetica, sans-serif ; font-weight:bold;color:Black;">
     <asp:Repeater ID="RptContactus" runat="server">
     <ItemTemplate>
          <asp:Label ID="lblempname" runat="server" Text='<%#Bind("U_Empname") %>'></asp:Label>-
       <asp:Label ID="lblposition" runat="server" Text='<%#Bind("U_Position") %>'></asp:Label> -
     <asp:HyperLink NavigateUrl='<%# Bind("mail", "mailto:{0}") %>' Text='<%# Bind("mail") %>'   runat="server" ID="hlEmail"></asp:HyperLink> -
 <asp:Label ID="lblphone" runat="server" Text='<%#Bind("U_phone") %>'></asp:Label>
<br /><br />
     </ItemTemplate>
     </asp:Repeater>
 
 </div>
 </td>
 </tr>
</table> 
</asp:Content>
