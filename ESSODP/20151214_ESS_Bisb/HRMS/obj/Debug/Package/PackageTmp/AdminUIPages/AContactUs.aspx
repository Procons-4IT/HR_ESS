<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/AdminUIPages/Admin.Master" CodeBehind="AContactUs.aspx.vb" Inherits="HRMS.AContactUs" %>
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

<asp:UpdatePanel ID="Update" runat="server" UpdateMode="Conditional">
<ContentTemplate>

  <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
         <tr>

    <td height="30" align="left" colspan="2" valign="bottom" background="../images/h_bg.png"; style="border-bottom:1px dotted; border-color: #f45501; background-repeat:repeat-x">     
      <div >&nbsp; <asp:Label ID="Label3" runat="server" Text="ContactUs"  CssClass="subheader" style="float:left;" ></asp:Label> 
       <span ><asp:Label ID="lblNewTrip" runat="server" Text="" Visible="false"></asp:Label></span> </div>
      </td>    
 </tr>

        <tr>
        <td>
          <asp:Panel ID="panelhome" runat="server" Width="98%" > 
            <asp:ImageButton ID="btnhome" runat="server" ImageUrl="../images/Homeicon.jpg" PostBackUrl="~/AdminUIPages/AHome.aspx" 
                    ToolTip="Home" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     <asp:ImageButton ID="btnnew" runat="server" ImageUrl="~/images/Add.jpg" ToolTip="Add new record" 
                  /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </asp:Panel> 
            <asp:Label ID="Label2" runat="server" Text="" style="color:Red;"></asp:Label>
        
        </td>
        </tr>
        </table>

        <asp:Panel ID="panelnew" runat="server" Width="98%" >
         <div id="Div1" runat="server" class="DivCorner" style="border:solid 2px LightSteelBlue; width:98%;"> 
         <div style="margin-left:20px;">
            <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
               <tr>
                 <td width="15%">Company Name</td>
                 <td width="25%">
                    <asp:TextBox ID="txtdocno" CssClass="txtbox" Width="150px" runat="server" visible="false" ></asp:TextBox>
                   <asp:TextBox ID="txtcompname" CssClass="txtbox" Width="150px" runat="server" readonly="true" ></asp:TextBox>
                </td> 
                  <td  width="15%"></td>
                  <td  width="15%"></td>
                  <td width="20%"></td>                       
            </tr>
             <tr>
                 <td width="15%">Employee Name <a style="color:Red;">*</a></td>
                 <td>
                   <asp:TextBox ID="txtempname" CssClass="txtbox" Width="150px" runat="server" ></asp:TextBox>
                </td> 
                  <td  width="15%"></td>
                  <td  width="15%"></td>
                  <td width="20%"></td>                       
            </tr>
             <tr>
                 <td width="15%">Designation <a style="color:Red;">*</a></td>
                  <td width="15%">
                  <asp:TextBox ID="txtdesi" CssClass="txtbox" Width="150px"  runat="server" ></asp:TextBox>           
                 </td>                
                      <td  width="15%"></td>
                  <td  width="15%"></td>
                  <td width="20%"></td>         
            </tr> 
            <tr>
                <td  width="15%">E-mail Id <a style="color:Red;">*</a></td>
                  <td width="15%">
                   <asp:TextBox ID="txtemail"  CssClass="txtbox" Width="150px"  runat="server" ></asp:TextBox>   
                    
                  </td> 
                    <td  width="15%">
                      
                    </td>
                  <td  width="15%"></td>
                  <td width="20%"></td>          
            </tr>            
                <tr>
                <td  width="15%">Telephone Ext.</td>
                  <td width="15%">
                   <asp:TextBox ID="txttelext"  CssClass="txtbox" Width="150px"  runat="server" ></asp:TextBox>   
                    
                  </td> 
                    <td  width="15%">
                      
                    </td>
                  <td  width="15%"></td>
                  <td width="20%"></td>          
            </tr>            
              <tr>
                <td  width="15%"></td>
                  <td width="15%">
                   <asp:CheckBox runat="server" id="chkActive" Text="Active" ></asp:CheckBox> 
                    
                  </td> 
                    <td  width="15%">
                      
                    </td>
                  <td  width="15%"></td>
                  <td width="20%"></td>          
            </tr>            
                  <tr>
                 <td width="10%"></td>
                 <td>
                     <asp:Button ID="btnsave" runat="server" Text="Save&Submit" CssClass="btn" Width="140px" />
                       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnclose" CssClass="btn" Width="100px" runat="server" Text="Cancel" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 </td>                     
            </tr>
            </table>  
            </div>   
            </div>  
            </asp:Panel>
            
                 <asp:Panel ID="panelview" runat="server" Width="98%"  CssClass="main_content" >
                  <div id="Div2" runat="server" class="DivCorner" style="border:solid 2px LightSteelBlue; width:98%;"> 
                  <div style="margin-left:20px;">
                   <asp:GridView ID="grdContactus" runat="server" CellPadding="4" AllowPaging="True" CssClass="mGrid" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found" HeaderStyle-Cssclass="GridBG"  
                    PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" Width="95%" PageSize="15" >
                     <Columns>
                      <asp:TemplateField HeaderText="Serial No.">
                            <ItemTemplate>
                                <div align="center"><asp:LinkButton ID="lbtndocnum" runat="server" Text='<%#Bind("U_DocEntry") %>' onclick="lbtndocnum_Click"></asp:LinkButton></div>
                            </ItemTemplate>                                    
                      </asp:TemplateField>
                    <asp:TemplateField HeaderText="Employee Name">
                            <ItemTemplate>
                                <div align="center"><asp:Label ID="lblempname" runat="server" Text='<%#Bind("U_Empname") %>'></asp:Label></div>
                            </ItemTemplate>                                    
                      </asp:TemplateField>
                     <asp:TemplateField HeaderText="Designation">
                            <ItemTemplate>
                                <div align="center"><asp:Label ID="lblposition" runat="server" Text='<%#Bind("U_Position") %>'></asp:Label></div>
                            </ItemTemplate>                                    
                      </asp:TemplateField>
                     <asp:TemplateField HeaderText="EmailId">
                            <ItemTemplate>
                                <div align="center"><asp:Label ID="lblemailid" runat="server" Text='<%#Bind("U_Email") %>'></asp:Label></div>
                            </ItemTemplate>                                    
                      </asp:TemplateField>
                     <asp:TemplateField HeaderText="Phone Ext.">
                            <ItemTemplate>
                                <div align="center"><asp:Label ID="lblphoneext" runat="server" Text='<%#Bind("U_phone") %>'></asp:Label></div>
                            </ItemTemplate>                                    
                      </asp:TemplateField>
                         <asp:TemplateField HeaderText="Active">
                            <ItemTemplate>
                                <div align="center"><asp:Label ID="lblactive" runat="server" Text='<%#Bind("U_Status") %>'></asp:Label></div>
                            </ItemTemplate>                                    
                      </asp:TemplateField>
                    </Columns> 
                    </asp:GridView> 
                    </div> 
                    </div>  
                </asp:Panel> 
            </ContentTemplate> 
            </asp:UpdatePanel> 

</asp:Content>
