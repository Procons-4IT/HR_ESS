<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ESS/ESSMaster.Master" CodeBehind="Emprofile.aspx.vb" Inherits="HRMS.Emprofile" %>
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

       function Showalert() {

           alert('Call JavaScript function from codebehind');

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

/*Calendar Control CSS*/
.cal_Theme1 .ajax__calendar_container   {
background-color: #DEF1F4;
border:solid 1px #77D5F7;
}

.cal_Theme1 .ajax__calendar_header  {
background-color: #ffffff;
margin-bottom: 4px;
}

.cal_Theme1 .ajax__calendar_title,
.cal_Theme1 .ajax__calendar_next,
.cal_Theme1 .ajax__calendar_prev    {
color: #004080;
padding-top: 3px;
}

.cal_Theme1 .ajax__calendar_body    {
background-color: #ffffff;
border: solid 1px #77D5F7;
}

.cal_Theme1 .ajax__calendar_dayname {
text-align:center;
font-weight:bold;
margin-bottom: 4px;
margin-top: 2px;
color: #004080;
}

.cal_Theme1 .ajax__calendar_day {
color: #004080;
text-align:center;
}

.cal_Theme1 .ajax__calendar_hover .ajax__calendar_day,
.cal_Theme1 .ajax__calendar_hover .ajax__calendar_month,
.cal_Theme1 .ajax__calendar_hover .ajax__calendar_year,
.cal_Theme1 .ajax__calendar_active  {
color: #004080;
font-weight: bold;
background-color: #DEF1F4;
}

.cal_Theme1 .ajax__calendar_today   {
font-weight:bold;
}

.cal_Theme1 .ajax__calendar_other,
.cal_Theme1 .ajax__calendar_hover .ajax__calendar_today,
.cal_Theme1 .ajax__calendar_hover .ajax__calendar_title {
color: #bbbbbb;
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

<asp:UpdatePanel ID="Update" runat="server">
<ContentTemplate>
 <table width="100%" border="0" cellspacing="0" cellpadding="4" class="main_content">
 <tr>
 <td>
 <table width="95%" border="0" cellspacing="0" cellpadding="4" class="main_content">
 <tr>
 <td>
    
 </td>
 </tr>
  <tr>
      
      <td align="center">
        <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
         <tr>

    <td height="30" align="left" colspan="2" valign="bottom" background="../images/h_bg.png"; style="border-bottom:1px dotted; border-color: #f45501; background-repeat:repeat-x">     
      <div >&nbsp; <asp:Label ID="Label3" runat="server" Text="Employee Profile"  CssClass="subheader" style="float:left;" ></asp:Label> 
       <span ><asp:Label ID="lblNewTrip" runat="server" Text="" Visible="false"></asp:Label></span> </div>
      </td>    
 </tr>

        <tr>
        <td>
          <asp:Panel ID="panelhome" runat="server" Width="100%"> 
            <asp:ImageButton ID="btnhome" runat="server" ImageUrl="../images/Homeicon.jpg"  PostBackUrl="~/ESS/ESSHome.aspx"
                    ToolTip="Home" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:ImageButton ID="btnnew" runat="server" ImageUrl="~/Images/Add.jpg" ToolTip="Add new record" Visible="false"
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
                 <td width="15%">T&A Employee No</td>
                 <td>
                     <asp:Label ID="lbltano" CssClass="txtbox" Width="150px" runat="server"  ></asp:Label>              
              </td> 
               <td  width="15%"></td>
                  <td  width="20%">Employee No</td>
                  <td width="15%">
                   <asp:Label ID="txtempno"  CssClass="txtbox" Width="150px"  runat="server"></asp:Label>        
                  </td>                        
            </tr>
             <tr>
                 <td width="10%">First Name</td>
                 <td>
                  <asp:Label ID="txtFirstName" CssClass="txtbox" Width="150px" runat="server"  ></asp:Label>
                 </td>
                  <td  width="15%"></td>
                  <td  width="10%">Middle Name</td>
                  <td width="30%">
                          <asp:Label ID="txtmiddleName" CssClass="txtbox" Width="150px"  runat="server" ></asp:Label>
                  </td>                
            </tr>             
                 <tr>
                 <td width="10%">Third Name</td>
                 <td>
                    <asp:Label ID="txtthirdname"  CssClass="txtbox" Width="150px"  runat="server"></asp:Label>
                 </td>
                  <td  width="15%"></td>
                  <td  width="10%">Last Name</td>
                  <td width="30%">
                  <asp:Label ID="txtlastname" CssClass="txtbox" Width="150px"  runat="server" ></asp:Label>
                   <asp:Label ID="txtappid"  CssClass="txtbox" Width="150px"  runat="server" visible="false"></asp:Label>        
                              
                             <tr>
                <%-- <td width="10%">Job Title</td>
                 <td>
                  <asp:TextBox ID="txtjobtitle" CssClass="txtbox" Width="150px"  runat="server" ></asp:TextBox>
                 </td>--%>
                  <td width="10%">Position</td>
                 <td>
                  <asp:Label ID="txtposition" CssClass="txtbox" Width="150px"  runat="server" ></asp:Label>
                 </td>
                  <td  width="15%"></td>
                  <td  width="10%">Office Phone</td>
                  <td width="30%">
                   <asp:TextBox ID="txtoffphone"  CssClass="txtbox" Width="150px" runat="server" Enabled="false"></asp:TextBox>        
                  </td>                
            </tr>             
              <tr>
                 <td width="10%">Department</td>
                 <td>
                  <asp:Label ID="txtdept" CssClass="txtbox" Width="150px"  runat="server" ></asp:Label>
                 </td>
                  <td  width="15%"></td>
                  <td  width="10%">Mobile Phone</td>
                  <td width="30%">
                   <asp:TextBox ID="txtmobile"  CssClass="txtbox" Width="150px" runat="server" Enabled="false"></asp:TextBox>        
                  </td>                
            </tr>             
                           
                             <tr>
                 <td width="10%">Manager</td>
                 <td>
                  <asp:Label ID="txtmanager" CssClass="txtbox" Width="150px"  runat="server" ></asp:Label>
                 </td>
                  <td  width="15%"></td>
                       
            </tr>             
                             <tr>
                 <td width="10%">E-mail</td>
                 <td>
                  <asp:Label ID="txtemail" CssClass="txtbox" Width="150px"  runat="server" Enabled="false"></asp:Label>
                 </td>
                  <td  width="15%"></td>
                  <td  width="10%">Fax</td>
                  <td width="30%">
                   <asp:TextBox ID="txtfax"  CssClass="txtbox" Width="150px" runat="server" Enabled="false"></asp:TextBox>        
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
                  <ajx:TabPanel  ID="TabPanel1" runat="server" HeaderText="Address" >
                   <ContentTemplate>
                      <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content">  
                      <tr>
                        <td width="15%"><a style="text-decoration:underline;">Work Address</a> </td>
                 <td></td>
                  <td  width="15%"> </td>
                  <td  width="15%"><a style="text-decoration:underline;">Home Address</a> </td>
                  <td width="30%"></td>                
                      </tr>                                   
               <tr>
                 <td width="15%">Street</td>
                 <td>
                  <asp:TextBox ID="txtWStreet" CssClass="txtbox" Width="150px" runat="server"  Enabled="false"></asp:TextBox>
                 </td>
                  <td  width="15%">
                  </td>
                  <td  width="15%">Street</td>
                  <td width="30%">
                   <asp:TextBox ID="txtHStreet"  CssClass="txtbox" Width="150px" runat="server" Enabled="false"></asp:TextBox>        
                  </td>                
            </tr>   
             <tr>
                 <td width="15%">Block</td>
                 <td>
                  <asp:TextBox ID="txtWBlock" CssClass="txtbox" Width="150px" runat="server" Enabled="false"></asp:TextBox>
                 </td>
                  <td  width="15%"></td>
                  <td  width="15%">Block</td>
                  <td width="30%">
                   <asp:TextBox ID="txtHBlock"  CssClass="txtbox" Width="150px" runat="server" Enabled="false"></asp:TextBox>        
                  </td>                
            </tr>                                  
             <tr>
                 <td width="15%">Building/Floor/Room</td>
                 <td>
                  <asp:TextBox ID="txtWbuild" CssClass="txtbox" Width="150px" runat="server" Enabled="false"></asp:TextBox>
                 </td>
                  <td  width="15%"></td>
                  <td  width="15%">Building/Floor/Room</td>
                  <td width="30%">
                   <asp:TextBox ID="txtHbuild"  CssClass="txtbox" Width="150px" runat="server" Enabled="false"></asp:TextBox>        
                  </td>                
            </tr>                                  
             <tr>
                 <td width="15%">ZipCode</td>
                 <td>
                  <asp:TextBox ID="txtWZipcode" CssClass="txtbox" Width="150px" runat="server" Enabled="false"></asp:TextBox>
                 </td>
                  <td  width="15%"></td>
                  <td  width="18%">ZipCode</td>
                  <td width="30%">
                   <asp:TextBox ID="txtHZipcode"  CssClass="txtbox" Width="150px"  runat="server" Enabled="false"></asp:TextBox>        
                  </td>                
            </tr>                                  
             <tr>
                 <td width="15%">City</td>
                 <td>
                  <asp:TextBox ID="txtWCity" CssClass="txtbox" Width="150px" runat="server" Enabled="false" ></asp:TextBox>
                 </td>
                  <td  width="15%"></td>
                  <td  width="15%">City</td>
                  <td width="30%">
                   <asp:TextBox ID="txtHCity"  CssClass="txtbox" Width="150px" runat="server" Enabled="false"></asp:TextBox>        
                  </td>                
            </tr>                                  
             <tr>
                 <td width="15%">County</td>
                 <td>
                  <asp:TextBox ID="txtWCounty" CssClass="txtbox" Width="150px" runat="server" Enabled="false" ></asp:TextBox>
                 </td>
                  <td  width="15%"></td>
                  <td  width="15%">County</td>
                  <td width="30%">
                   <asp:TextBox ID="txtHCounty"  CssClass="txtbox" Width="150px"  runat="server" Enabled="false" ></asp:TextBox>        
                  </td>                
            </tr>  
           <tr>
                 <td width="15%">State</td>
                 <td>
                     <asp:DropDownList ID="ddlWstate" CssClass="txtbox1" Width="150px" runat="server" Enabled="false"></asp:DropDownList>
                 </td>
                  <td  width="15%"></td>
                  <td  width="15%">State</td>
                  <td width="30%">
                   <asp:DropDownList ID="ddlHState" CssClass="txtbox1" Width="150px" runat="server" Enabled="false"></asp:DropDownList></td>                
            </tr>  
             <tr>
                 <td width="15%">Country</td>
                 <td>
                 <asp:DropDownList ID="ddlWCountry" CssClass="txtbox1" Width="150px" runat="server" Enabled="false" AutoPostBack="true"></asp:DropDownList>
                 </td>
                  <td  width="15%"></td>
                  <td  width="15%">Country</td>
                  <td width="30%">
                  <asp:DropDownList ID="ddlHCountry" CssClass="txtbox1" Width="150px" runat="server" Enabled="false" AutoPostBack="true"></asp:DropDownList>
                  </td>                
            </tr>        
            <tr>
             <td width="15%"></td>
                 <td>                
                 </td>
                  <td  width="15%"></td>
             <td  width="10%">Home Phone</td>
                  <td width="30%">
                   <asp:TextBox ID="txthometel"  CssClass="txtbox" Width="150px" runat="server" Enabled="false"></asp:TextBox>        
                  </td>     
            </tr>                                                
           </table>
                   
                   </ContentTemplate> 
                  </ajx:TabPanel> 
                  <ajx:TabPanel  ID="TabPanel2" runat="server" HeaderText="Personal" >
                   <ContentTemplate>
                    <table width="100%" border="0" cellspacing="0" cellpadding="4" class="main_content">
                      <tr>
                 <td width="15%">Gender</td>
                 <td width="25%">
                     <asp:DropDownList ID="ddlgender" CssClass="txtbox1" Width="160px" runat="server" Enabled="false">
                   <asp:ListItem Value="M">Male</asp:ListItem>
                     <asp:ListItem Value="F">FeMale</asp:ListItem>
                     </asp:DropDownList>
                 </td>
                  <td  width="5%">
                  </td>
                  <td  width="10%">Citizenship</td>
                  <td width="30%" >
                 <asp:DropDownList ID="ddlcitizenship" CssClass="txtbox1" Width="160px" runat="server" Enabled="false"> </asp:DropDownList>    
                  </td>                
            </tr> 
             <tr>
                 <td >Date of Birth</td>
                 <td>
                  <asp:Label ID="txtdob" CssClass="txtbox" Width="150px" runat="server" Enabled="false"></asp:Label>
<%--                 <ajx:CalendarExtender ID="CalendarExtender3" Animated="true" Format="dd/MM/yyyy" runat="server" TargetControlID="txtdob" CssClass= "cal_Theme1"></ajx:CalendarExtender> 
--%>
                 </td>
                  <td >
                  </td>
                  <td >Passport No.</td>
                  <td>
                   <asp:TextBox ID="txtpassno"  CssClass="txtbox" Width="150px" runat="server" Enabled="false"></asp:TextBox>        
                  </td>                
            </tr>     
             <tr>
                 <td >Country of Birth</td>
                 <td>
                 <asp:DropDownList ID="ddlcouofbirth" CssClass="txtbox1" Width="160px" runat="server" Enabled="false"></asp:DropDownList>    
                 </td>
                  <td>
                  </td>
                   <td width="20%">Passport Expiration Date</td>
                  <td>
                   <asp:TextBox ID="txtexpdate"  CssClass="txtbox" Width="150px" runat="server" Enabled="false"></asp:TextBox>
                   <ajx:CalendarExtender ID="CalendarExtender1" Animated="true" Format="dd/MM/yyyy" runat="server" TargetControlID="txtexpdate" CssClass= "cal_Theme1"></ajx:CalendarExtender> 
  
                  </td>                            
            </tr>
            <tr>
            <td colspan="5"></td>
            </tr>
              <tr>
                 <td >Marital Status</td>
                 <td>
                     <asp:DropDownList ID="ddlmarstatus" CssClass="txtbox1" Width="160px" runat="server" Enabled="false">
                   <asp:ListItem Value="S">Single</asp:ListItem>
                     <asp:ListItem Value="M">Married</asp:ListItem>
                      <asp:ListItem Value="D">Divorced</asp:ListItem>
                     <asp:ListItem Value="W">Widowed</asp:ListItem>
                     </asp:DropDownList>
                 </td>
                  <td></td>
                  <td><a style="text-decoration:underline; font-weight:bold;">Emergency Contact Details</a></td>
                  <td></td>                
            </tr>    
             <tr>
                 <td>No. of Children</td>
                 <td>
                   <asp:Label ID="txtnochildren"  CssClass="txtbox" Width="150px" runat="server" Enabled="false"></asp:Label>  
              <%--<ajx:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterMode="ValidChars" FilterType="Numbers"  TargetControlID="txtnochildren"></ajx:FilteredTextBoxExtender>--%>

                 </td>
                  <td></td>
                <td  width="20%">Relation Name</td>
                  <td width="30%">
                   <asp:TextBox ID="lblrelation"  CssClass="txtbox" Width="150px"  runat="server" Enabled="false"></asp:TextBox>        
                  </td>          
            </tr>    
             <tr>
                 <td>ID No.</td>
                 <td>
                 <asp:Label ID="txtgovid"  CssClass="txtbox" Width="150px" runat="server" Enabled="false"></asp:Label> 
                 </td>
                  <td></td>
                    <td  width="20%">RelationShip Type</td>
                  <td width="30%">
                   <asp:TextBox ID="lblreltype"  CssClass="txtbox" Width="150px"  runat="server" Enabled="false"></asp:TextBox>        
                  </td>             
            </tr>    
               <tr>
                 <td></td>
                 <td></td>
                  <td></td>
                     <td  width="20%">Contact Number</td>
                  <td width="30%">
                   <asp:TextBox ID="lblemgphone"  CssClass="txtbox" Width="150px"  runat="server" Enabled="false"></asp:TextBox>        
                  </td>        
            </tr>    
                    </table> 
                  </ContentTemplate>                   
                  </ajx:TabPanel> 
                   
                  
                  <ajx:TabPanel  ID="TabPanel7" runat="server" HeaderText="Education Details" >
                  <ContentTemplate>
                   <table width="100%" border="0" cellspacing="0" cellpadding="4" class="main_content">
                      <tr>
                      <td>
                    <asp:GridView ID="grdedutype" Enabled=false  runat="server" CellPadding="4" RowStyle-CssClass="mousecursor" CssClass="mGrid"  HeaderStyle-Cssclass="GridBG"  
                        PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found"    AutoGenerateColumns="false" Width="100%">
                                    <Columns>
                                    <%-- <asp:TemplateField HeaderText="empID" Visible="false" >
                                            <ItemTemplate>
                                              <div align="left">&nbsp;  <asp:Label ID="lblempId" runat="server" Text='<%#Bind("empID") %>'></asp:Label></div> 

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="line" Visible="false">
                                            <ItemTemplate>
                                                <div align="left">&nbsp;<asp:Label ID="lblline" runat="server" Text='<%#Bind("line") %>'></asp:Label></div> 
                                            </ItemTemplate>
                                        </asp:TemplateField>  --%>
                                        <asp:TemplateField HeaderText="Date From">
                                            <ItemTemplate>
                                              <div align="left">&nbsp;  <asp:Label ID="lbldtfrom" runat="server" Text='<%#Bind("fromDate") %>'></asp:Label></div> 
                                              <%-- <ajx:CalendarExtender ID="CalendarExtender4" Animated="true" Format="dd/MM/yyyy" runat="server" TargetControlID="lbldtfrom" CssClass= "cal_Theme1"></ajx:CalendarExtender> --%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="To">
                                            <ItemTemplate>
                                                <div align="left">&nbsp;<asp:Label ID="lbldtTo" runat="server" Text='<%#Bind("toDate") %>'></asp:Label></div> 
                                                <%-- <ajx:CalendarExtender ID="CalendarExtender5" Animated="true" Format="dd/MM/yyyy" runat="server" TargetControlID="lbldtTo" CssClass= "cal_Theme1"></ajx:CalendarExtender> --%>
                                            </ItemTemplate>
                                        </asp:TemplateField>  
                                          <asp:TemplateField HeaderText="Education Type">
                                            <ItemTemplate>
                                             <div align="left">&nbsp;<asp:label ID="txtedutype" runat="server" Text='<%#Bind("name") %>'></asp:label></div> 
                                            <%--  <div align="left">&nbsp;  <asp:DropDownList ID="lbledutype" runat="server" ></asp:DropDownList></div>--%> 
                                            </ItemTemplate>     
                                                                                  
                                        </asp:TemplateField>  
                                         <asp:TemplateField HeaderText="Institute">
                                            <ItemTemplate>
                                              <div align="left">&nbsp; <asp:Label ID="lblCWeight" runat="server" Text='<%#Bind("institute") %>'></asp:Label></div> 
                                            </ItemTemplate>
                                        </asp:TemplateField>   
                                         <asp:TemplateField HeaderText="Major">
                                            <ItemTemplate>
                                              <div align="left">&nbsp; <asp:Label ID="lblmajor" runat="server" Text='<%#Bind("major") %>'></asp:Label></div> 
                                            </ItemTemplate>
                                        </asp:TemplateField>   
                                         <asp:TemplateField HeaderText="Diploma">
                                            <ItemTemplate>
                                              <div align="left">&nbsp; <asp:Label ID="lblDiploma" runat="server" Text='<%#Bind("diploma") %>'></asp:Label></div> 
                                            </ItemTemplate>
                                           
                                        </asp:TemplateField>                                      
                                    </Columns>
                                    <HeaderStyle BackColor="Gray" HorizontalAlign="Center" ForeColor="White" Height="25px" />
                                  
                                  </asp:GridView>
                  </td> 
                  </tr>
                  <tr>
                  <td>
                 <%-- <asp:Button ID="ButtonAdd" CssClass="btn" Width="123px" runat="server" Text="New Education" Visible="false" />--%>
                  </td>
                  </tr> 
                  </table> 
                  </ContentTemplate>                  
                  </ajx:TabPanel> 
                   <ajx:TabPanel  ID="TabPanel3" runat="server" HeaderText="Organization Details" Visible="false" >
                  <ContentTemplate>
                    <table width="100%" border="0" cellspacing="0" cellpadding="4" class="main_content">
                      <tr>
                        <td  width="20%">Organization Name</td>
                  <td width="40%">
                   <asp:Label ID="txtorgname"  CssClass="txtbox" Width="150px"  runat="server"></asp:Label>        
                  </td> 
               
                 <td width="20%"></td>
                 <td>
                  <asp:Label ID="txtorgcode" CssClass="txtbox" Width="150px"  runat="server" Visible="false" ></asp:Label>
                 </td>
                
                               
            </tr> 
               <tr>
                  <td  width="20%">Level Name</td>
                  <td  width="40%">
                   <asp:Label ID="lbllvlName"  CssClass="txtbox" Width="150px"  runat="server"></asp:Label>        
                  </td> 
                  
                 <td width="20%"></td>
                 <td>
                  <asp:Label ID="lbllvlcode" CssClass="txtbox" Width="150px"  runat="server" Visible="false" ></asp:Label>
                 </td>
                 
                         
            </tr> 
               <tr>
                   <td  width="20%">Location Name</td>
                  <td  width="40%">
                   <asp:Label ID="lbllocName"  CssClass="txtbox" Width="150px"  runat="server"></asp:Label>        
                  </td>   
                   
                 <td width="20%"></td>
                 <td>
                  <asp:Label ID="lbllocCode" CssClass="txtbox" Width="150px"  runat="server" Visible="false" ></asp:Label>
                 </td>
               
                      
            </tr> 
           
                    </table> 
                  </ContentTemplate>                  
                  </ajx:TabPanel> 
                  
                      <ajx:TabPanel  ID="TabPanel9" runat="server" HeaderText="Bank Details" >
                  <ContentTemplate>
                    <table width="100%" border="0" cellspacing="0" cellpadding="4" class="main_content">
                      <tr>
                        <td  width="20%">Bank Name</td>
                  <td width="40%">
                   <asp:DropDownList ID="ddlbankname"  CssClass="txtbox1" Width="150px"  runat="server" Enabled="false"></asp:DropDownList>        
                  </td> 
               
                 <td width="20%"></td>
                 <td>
                 </td>
                
                               
            </tr> 
               <tr>
                  <td  width="20%">Account No.</td>
                  <td  width="40%">
                   <asp:Label ID="lblAccno"  CssClass="txtbox" Width="150px"  runat="server" Enabled="false"></asp:Label>        
                  </td> 
                  
                 <td width="20%"></td>
                 <td>
                 </td>
                 
                         
            </tr> 
               <tr>
                   <td  width="20%">IBAN</td>
                  <td  width="40%">
                   <asp:Label ID="lbliban"  CssClass="txtbox" Width="150px"  runat="server" Enabled="false"></asp:Label>        
                  </td>   
                   
                 <td width="20%"></td>
                 <td>
                 </td> 
            </tr> 
             <tr>
                   <td  width="20%">Branch Name</td>
                  <td  width="40%">
                   <asp:Label ID="lblbankbranch"  CssClass="txtbox" Width="150px"  runat="server" Enabled="false"></asp:Label>        
                  </td>  
                 <td width="20%"></td>
                 <td>
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
            
   <tr>
                <td colspan="5" align="left">
                <br />
                    <asp:Button ID="btnsubmit" CssClass="btn" Width="85px" runat="server" Text="Edit"/>
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnclose" CssClass="btn" Width="96px" runat="server" 
                        Text="Cancel"  />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     
                </td>
            </tr>                                
          
           
 </table> 
</ContentTemplate> 
</asp:UpdatePanel> 
</asp:Content>
