<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AdminUIPages/Admin.Master" CodeBehind="CompanyMaster.aspx.vb" Inherits="HRMS.CompanyMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link rel="stylesheet" href="../Styles/vThink_Theme.css" type="text/css" />
 <script type="text/javascript">


     function Confirmation() {
         if (confirm("Do you want to Confirm the Change Password?") == true) {
             return true;
         }
         else {
             return false;
         }
     }
</script>

  <script type="text/javascript">
      //
      var prm = Sys.WebForms.PageRequestManager.getInstance();
      //Raised before processing of an asynchronous postback starts and the postback request is sent to the server.
      prm.add_beginRequest(BeginRequestHandler);
      // Raised after an asynchronous postback is finished and control has been returned to the browser.
      prm.add_endRequest(EndRequestHandler);
      function BeginRequestHandler(sender, args) {
          //Shows the modal popup - the update progress
          var popup = $find('<%= modalPopup.ClientID %>');
          if (popup != null) {
              popup.show();
          }
      }

      function EndRequestHandler(sender, args) {
          //Hide the modal popup - the update progress
          var popup = $find('<%= modalPopup.ClientID %>');
          if (popup != null) {
              popup.hide();
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
<asp:Image ID="Image1" ImageUrl="../Images/waiting.gif" AlternateText="Processing" runat="server" />
</ProgressTemplate>
</asp:UpdateProgress>
<ajx:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress" PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />

<%--<asp:UpdatePanel ID="Update" runat="server" UpdateMode="Conditional">
            <ContentTemplate>--%>
  <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
         <tr>

    <td height="30" align="left" colspan="2" valign="bottom" background="../images/h_bg.png"; style="border-bottom:1px dotted; border-color: #f45501; background-repeat:repeat-x">     
      <div >&nbsp; <asp:Label ID="Label5" runat="server" Text="Company Master"  CssClass="subheader" style="float:left;" ></asp:Label> 
       <span ><asp:Label ID="lblNewTrip" runat="server" Text="" Visible="false"></asp:Label></span> </div>
      </td>    
 </tr>

        <tr>
        <td>
          <asp:Panel ID="panelhome" runat="server" Width="98%"> 
            <asp:ImageButton ID="btnhome" runat="server" ImageUrl="../images/Homeicon.jpg" PostBackUrl="~/AdminUIPages/AHome.aspx" 
                    ToolTip="Home" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  
            </asp:Panel> 
            <asp:Label ID="Label6" runat="server" Text="" style="color:Red;"></asp:Label>
        
        </td>
        </tr>
        </table>

  <asp:Panel ID="panelnew" runat="server" Width="98%" >
         <div id="Div1" runat="server" class="DivCorner" style="border:solid 2px LightSteelBlue; width:98%;">    
            <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
             <tr>
                 <td width="15%">Company Logo <a style="color:Red;">*</a></td>
                 <td>
                    <asp:FileUpload ID="FUCompanyLogo" runat="server" /> 
                                   </td> 
                  <td  width="15%"></td>
                  <td  width="15%"></td>
                  <td width="20%"></td>                       
            </tr>
            <tr>
            <td colspan="5">
              <asp:Label ID="Label7" runat="server" Font-Names="Verdana" Font-Size="Small" ForeColor="Blue"
                Text="" Width="617px"></asp:Label>
            </td>
            </tr>
                  <tr>
                 <td width="10%"></td>
                 <td>
                     <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="btn" Width="100px" />
                       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnclose" CssClass="btn" Width="100px" runat="server" Text="Cancel" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 </td>                     
            </tr>
            </table>  
            </div>  
            </asp:Panel>


<%--</ContentTemplate> 
</asp:UpdatePanel> --%>

</asp:Content>
