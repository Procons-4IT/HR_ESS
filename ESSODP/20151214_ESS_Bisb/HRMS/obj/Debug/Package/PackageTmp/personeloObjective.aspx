<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/HRMS.Master" CodeBehind="personeloObjective.aspx.vb" Inherits="HRMS.personeloObjective" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <script type="text/javascript">
   
     function Confirmation() {
         if (confirm("Do you want to confirm the Changes?") == true) {
             return true;
         }
         else {
             return false;
         }
     }
     
</script>

 
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>



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


#marqueecontainer{
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdateProgress ID="UpdateProgress" runat="server">
<ProgressTemplate>
<asp:Image ID="Image1" ImageUrl="Images/waiting.gif" AlternateText="Processing" runat="server" />
</ProgressTemplate>
</asp:UpdateProgress>
<ajx:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress" PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />

<asp:UpdatePanel ID="Update" runat="server">
<ContentTemplate>
<table width="100%" border="0" cellspacing="0" cellpadding="4" class="main_content">
 <tr>
  
      <td align="center">
        <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
         <tr>

    <td height="30" align="left" colspan="2" valign="bottom" background="images/h_bg.png"; style="border-bottom:1px dotted; border-color: #f45501; background-repeat:repeat-x">     
      <div >&nbsp; <asp:Label ID="Label3" runat="server" Text="Personel Objectives"  CssClass="subheader" style="float:left;" ></asp:Label> 
       <span ><asp:Label ID="lblNewTrip" runat="server" Text="" Visible="false"></asp:Label></span> </div>
      </td>    
 </tr>

        <tr>
        <td>
          <asp:Panel ID="panelhome" runat="server" Width="100%"> 
            <asp:ImageButton ID="btnhome" runat="server" ImageUrl="images/Homeicon.jpg" PostBackUrl="~/Home.aspx" 
                    ToolTip="Home" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:ImageButton ID="btnnew" runat="server" ImageUrl="images/Add.jpg" ToolTip="Add new record" Visible="false"
                  /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         
            </asp:Panel> 
            <asp:Label ID="Label2" runat="server" Text="" style="color:Red;"></asp:Label>
        
        </td>
        </tr>
        </table>
        
          <asp:Panel ID="panelnew" runat="server" Width="98%" >
         <div id="Div1" runat="server" class="DivCorner" style="border:solid 2px LightSteelBlue; width:100%;">    
            <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
             <tr>
                 <td width="10%">First Name</td>
                 <td width="10%">
                   <asp:Label ID="txtFirstName" CssClass="txtbox" Width="150px" runat="server"  ></asp:Label>                
              </td> 
               <td  width="5%"></td>
                  <td  width="10%">Employee No</td>
                  <td width="15%">
                   <asp:Label ID="txtempno"  CssClass="txtbox" Width="150px"  runat="server"></asp:Label>        
                  </td>                        
            </tr>
             <tr>
                 <td width="10%">Middle Name</td>
                 <td>
                  <asp:Label ID="txtmiddleName" CssClass="txtbox" Width="150px"  runat="server" ></asp:Label>
                 </td>
                  <td  width="15%"></td>
                  <td  width="10%">Third Name</td>
                  <td width="30%">
                   <asp:Label ID="txtthirdname"  CssClass="txtbox" Width="150px"  runat="server"></asp:Label>        
                  </td>                
            </tr>             
                 <tr>
                 <td width="10%">Last Name</td>
                 <td>
                  <asp:Label ID="txtlastname" CssClass="txtbox" Width="150px"  runat="server" ></asp:Label>
                 </td>
                  <td  width="15%"></td>
                  <td  width="10%"></td>
                  <td width="30%">
                   <asp:Label ID="txtappid"  CssClass="txtbox" Width="150px"  runat="server" visible="false"></asp:Label>        
                  </td>                
            </tr>             
                             <tr>
                <%-- <td width="10%">Job Title</td>
                 <td>
                  <asp:Label ID="txtjobtitle" CssClass="txtbox" Width="150px"  runat="server" ></asp:Label>
                 </td>--%>
                  <td width="10%">Position</td>
                 <td>
                  <asp:Label ID="txtposition" CssClass="txtbox" Width="150px"  runat="server" ></asp:Label>
                 </td>
                  <td  width="15%"></td>
                  <td  width="10%">Office Phone</td>
                  <td width="30%">
                   <asp:Label ID="txtoffphone"  CssClass="txtbox" Width="150px" runat="server"></asp:Label>        
                  </td>                
            </tr>             
                           <%--  <tr>
                
                  <td  width="15%"></td>
                  <td  width="10%">Ext.</td>
                  <td width="30%">
                   <asp:Label ID="txtExt"  CssClass="txtbox" Width="150px"  runat="server"></asp:Label>        
                  </td>                
            </tr>             --%>
                             <tr>
                 <td width="10%">Department</td>
                 <td>
                  <asp:Label ID="txtdept" CssClass="txtbox" Width="150px"  runat="server" ></asp:Label>
                 </td>
                  <td  width="15%"></td>
                  <td  width="10%">Mobile Phone</td>
                  <td width="30%">
                   <asp:Label ID="txtmobile"  CssClass="txtbox" Width="150px" runat="server"></asp:Label>        
                  </td>                
            </tr>             
                            <%-- <tr>
                 <td width="10%">Branch</td>
                 <td>
                  <asp:Label ID="txtbranch" CssClass="txtbox" Width="150px"  runat="server" ></asp:Label>
                 </td>
                  <td  width="15%"></td>
                  <td  width="10%">Pager</td>
                  <td width="30%">
                   <asp:Label ID="txtpager"  CssClass="txtbox" Width="150px" runat="server"></asp:Label>        
                  </td>                
            </tr>             --%>
                             <tr>
                 <td width="10%">Manager</td>
                 <td>
                  <asp:Label ID="txtmanager" CssClass="txtbox" Width="150px"  runat="server" ></asp:Label>
                 </td>
                  <td  width="15%"></td>
                  <td  width="10%">Home Phone</td>
                  <td width="30%">
                   <asp:Label ID="txthometel"  CssClass="txtbox" Width="150px" runat="server"></asp:Label>        
                  </td>         
            </tr>             
                             <tr>
                 <td width="10%">E-mail</td>
                 <td>
                  <asp:Label ID="txtemail" CssClass="txtbox" Width="150px"  runat="server" ></asp:Label>
                 </td>
                  <td  width="15%"></td>
                  <td  width="10%">Fax</td>
                  <td width="30%">
                   <asp:Label ID="txtfax"  CssClass="txtbox" Width="150px" runat="server"></asp:Label>        
                  </td>                
            </tr>             
            </table>  
            </div>  
            </asp:Panel> 
            
               <asp:Panel ID="paltab" runat="server" Width="98%" BorderColor="LightSteelBlue"  BorderWidth="2">
               <table width="100%" border="0" cellspacing="0" cellpadding="4" class="main_content">
              <tr>
              <td valign="top">  
                <ajx:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0"  CssClass="ajax__tab_yuitabview-theme" Width="100%" >
                 <ajx:TabPanel  ID="TabPanel8" runat="server" HeaderText="Personal Objective" >
                  <ContentTemplate>
                   <table width="100%" border="0" cellspacing="0" cellpadding="4" class="main_content">
                           
                      <tr>
                      <td>
                    <asp:GridView ID="grdPeople" runat="server" CellPadding="4" RowStyle-CssClass="mousecursor"   CssClass="mGrid"  HeaderStyle-Cssclass="GridBG" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found"
                        PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt"   AutoGenerateColumns="false" Width="100%">
                                    <Columns>
                                   <%-- <asp:CommandField ShowDeleteButton="false" ButtonType="Link" ItemStyle-HorizontalAlign="Center" /> --%>
                                    
                                        <asp:TemplateField HeaderText="Personal Objective Code">
                                            <ItemTemplate>
                                               <div align="left">&nbsp; <asp:Label ID="lblPeCode" runat="server" Text='<%#Bind("U_Z_HRPeoobjCode") %>'></asp:Label> </div> 
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Personal Objective Description">
                                            <ItemTemplate>
                                               <div align="left">&nbsp; <asp:Label ID="lblPName" runat="server" Text='<%#Bind("U_Z_HRPeoobjName") %>'></asp:Label></div> 
                                            </ItemTemplate>
                                        </asp:TemplateField>  
                                          <asp:TemplateField HeaderText="Category">
                                            <ItemTemplate>
                                              <div align="left">&nbsp;  <asp:Label ID="lblPCategory" runat="server" Text='<%#Bind("U_Z_CatName") %>'></asp:Label></div> 
                                            </ItemTemplate>
                                        </asp:TemplateField>  
                                         <asp:TemplateField HeaderText="Category" Visible="false">
                                            <ItemTemplate>
                                                      <div align="left">&nbsp;  <asp:Label ID="lblPCatCode" runat="server" Text='<%#Bind("U_Z_CatCode") %>' Visible="false"></asp:Label></div> 
                                            </ItemTemplate>
                                        </asp:TemplateField>  
                                         <asp:TemplateField HeaderText="Weight">
                                            <ItemTemplate>
                                              <div align="left">&nbsp; <asp:Label ID="lblPWeight" runat="server" Text='<%#Bind("U_Z_HRWeight") %>'></asp:Label></div> 
                                            </ItemTemplate>
                                        </asp:TemplateField>  
                                          <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                              <div align="left">&nbsp; <asp:Label ID="lblPRemarks" runat="server" Text='<%#Bind("U_Z_Remarks") %>'></asp:Label></div> 
                                            </ItemTemplate>
                                        </asp:TemplateField>  
                                          <asp:TemplateField HeaderText="Code" visible="false">
                                            <ItemTemplate>
                                              <div align="left">&nbsp; <asp:Label ID="lblCode" runat="server" Text='<%#Bind("Code") %>'></asp:Label></div> 
                                            </ItemTemplate>
                                        </asp:TemplateField>                                       
                                    </Columns>
                                    <HeaderStyle BackColor="Gray" HorizontalAlign="Center" ForeColor="White" Height="25px" />
                                     <RowStyle HorizontalAlign="Left" />
                                     <AlternatingRowStyle HorizontalAlign="Left" />
                                     
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
</ContentTemplate> 
</asp:UpdatePanel>                                            
</asp:Content>
