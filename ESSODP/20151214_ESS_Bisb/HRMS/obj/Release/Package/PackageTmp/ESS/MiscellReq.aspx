<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ESS/ESSMaster.Master" CodeBehind="MiscellReq.aspx.vb" Inherits="HRMS.MiscellReq" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
  <tr>
        <td>
          <asp:Panel ID="panelhome" runat="server" Width="100%"> 
            <asp:ImageButton ID="btnhome" runat="server" ImageUrl="../images/Homeicon.jpg"  PostBackUrl="~/ESS/ESSHome.aspx"
                    ToolTip="Home" visible="false"/> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:ImageButton ID="btnnew" runat="server" ImageUrl="../images/Add.jpg" ToolTip="Add new record" visible="false"/>
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        
            </asp:Panel> 
            <span ><asp:Label ID="lblNewTrip" runat="server" Text="" Visible="false"></asp:Label></span> 
            </td> 
            </tr> 
            
            <tr>
            <td align="center">
            <br />
                 <asp:Label ID="Label2" runat="server" Text="Page Under Construction..." style="font-size:14px;"></asp:Label>
            </td>
            </tr>
 </table>

</asp:Content>
