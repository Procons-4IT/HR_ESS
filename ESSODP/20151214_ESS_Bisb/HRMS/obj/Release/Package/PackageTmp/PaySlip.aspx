<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/HRMS.Master" CodeBehind="PaySlip.aspx.vb" Inherits="HRMS.PaySlip" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var oldgridcolor;
        function SetMouseOver(element) {
            oldgridcolor = element.style.backgroundColor;
            element.style.backgroundColor = '#ffeb95';
            element.style.cursor = 'pointer';
            element.style.textDecoration = 'underline';
        }
        function SetMouseOut(element) {
            element.style.backgroundColor = oldgridcolor;
            element.style.textDecoration = 'none';

        }
        function Confirmation() {
            if (confirm("Do you want to confirm the changes?") == true) {
                return true;
            }
            else {
                return false;
            }
        }

        function Confirmation1() {
            if (confirm("Sure Want to Approve Document?") == true) {
                return true;
            }
            else {
                return false;
            }
        }
   </script>

    <style type="text/css" >
.modalPopup
{
background-color: #696969;
filter: alpha(opacity=40);
opacity: 0.7;
xindex:-1;
}
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdateProgress ID="UpdateProgress" runat="server">
<ProgressTemplate>
<asp:Image ID="Image1" ImageUrl="Images/waiting.gif" AlternateText="Processing" runat="server" />
</ProgressTemplate>
</asp:UpdateProgress>
<ajx:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress" PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
<asp:UpdatePanel ID="Update" runat="server">
<ContentTemplate>

 <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
  <tr>
               
    <td height="30" align="left" colspan="2" valign="bottom" background="images/h_bg.png"; style="border-bottom:1px dotted; border-color: #f45501; background-repeat:repeat-x">     
      <div >&nbsp; <asp:Label ID="Label3" runat="server" Text="PaySlip"  CssClass="subheader" style="float:left;" ></asp:Label> 
       <span ><asp:Label ID="lblNewTrip" runat="server" Text="" Visible="false"></asp:Label></span> </div>
      </td>    
 </tr>
  <tr>      
      <td align="center">
        <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
        <tr>
        <td>
          <asp:Panel ID="panelhome" runat="server" Width="100%">
            <asp:ImageButton ID="btnhome" runat="server" ImageUrl="~/images/Homeicon.jpg"  PostBackUrl="~/Home.aspx"
                    ToolTip="Home" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:ImageButton ID="btnnew" runat="server" ImageUrl="~/images/Add.jpg" ToolTip="Add new record" visible="false"/>
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             


            <asp:Label ID="Label2" runat="server" Text="" style="color:Red;"></asp:Label>
               </asp:Panel>  
               <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
            <tr>
              <td valign="top">          
                           
                  <td  width="20%" id="app" runat="server">Month</td>
                    <td width="15%">
                   <asp:DropDownList ID="ddlGstatus" CssClass="txtbox" Width="160px" runat="server" >   
                                 
                  </asp:DropDownList>
              
               
                <td>
              <asp:Button ID="btnPrint"  CssClass="btn" Width="100px" runat="server" Text="Print" />
            
            </td>
              </td> 
              </tr>
              <tr><td valign="top">          
                           
                  <td  width="20%" id="Td1" runat="server">Year<CR:CrystalReportViewer 
                          ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
                  </td>
                    <td width="15%">
                   <asp:DropDownList ID="ddmonthyr" CssClass="txtbox" Width="160px" runat="server" >   
                                 
                  </asp:DropDownList></td></td> </tr></table>
              
              
              
               </table>
           </table>
 </ContentTemplate>

 </asp:UpdatePanel>
    
</asp:Content>
