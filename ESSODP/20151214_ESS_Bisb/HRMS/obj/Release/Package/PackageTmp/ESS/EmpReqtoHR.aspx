<%@ Page Title="Request to HR" Language="vb" AutoEventWireup="false" MasterPageFile="~/ESS/ESSMaster.Master" CodeBehind="EmpReqtoHR.aspx.vb" Inherits="HRMS.EmpReqtoHR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

 <script type="text/javascript">

     function Confirmation() {
         if (confirm("Do you want to confirm?") == true) {
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

    <style type="text/css" >
.modalPopup
{
background-color: #696969;
filter: alpha(opacity=40);
opacity: 0.7;
xindex:-1;
}
</style>

<style type="text/css">
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
    .style1
    {
        width: 25%;
    }
</style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdateProgress ID="UpdateProgress" runat="server">
<ProgressTemplate>
<asp:Image ID="Image1" ImageUrl="../Images/waiting.gif" AlternateText="Processing" runat="server" />
</ProgressTemplate>
</asp:UpdateProgress>
<ajx:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress" PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />

<asp:UpdatePanel ID="Update" runat="server">
<ContentTemplate>
 <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
 <tr>
  <td height="30" align="left" colspan="2" valign="bottom" background="../images/h_bg.png"; style="border-bottom:1px dotted; border-color: #f45501; background-repeat:repeat-x">     
      <div >&nbsp; <asp:Label ID="Label3" runat="server" Text="Request to HR"  CssClass="subheader" style="float:left;" ></asp:Label>  <span ><asp:Label ID="lblNewTrip" runat="server" Text="" Visible="false"></asp:Label></span> </div>
      </td>  
 </tr> 

 <tr>      
      <td align="center">
        <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
        <tr>
        <td>
          <asp:Panel ID="panelhome" runat="server" Width="100%"> 
            <asp:ImageButton ID="btnhome" runat="server" ImageUrl="~/images/Homeicon.jpg"  PostBackUrl="~/ESS/ESSHome.aspx"
                    ToolTip="Home" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:ImageButton ID="btnnew" runat="server" ImageUrl="~/images/Add.jpg" ToolTip="Add new record" />
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        
            </asp:Panel> 
            <asp:Label ID="Label2" runat="server" Text="" style="color:Red;"></asp:Label>
              <asp:Panel ID="panelview" runat="server" Width="100%" BorderColor="LightSteelBlue"  BorderWidth="2">
            <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
              <tr>
              <td valign="top">  
                <ajx:TabContainer ID="TabContainer2" runat="server" ActiveTabIndex="0"  CssClass="ajax__tab_yuitabview-theme" Width="100%" >
                  <ajx:TabPanel  ID="TabPanel3" runat="server" HeaderText="Employee Request to HR" ><ContentTemplate><table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content"><tr><td><asp:GridView ID="grdRequesttohr" runat="server" CellPadding="4" AllowPaging="True" ShowHeaderWhenEmpty="true"  CssClass="mGrid" HeaderStyle-Cssclass="GridBG" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false" Width="100%" PageSize="10" ><Columns><asp:TemplateField HeaderText="Request Code"><ItemTemplate><div align="center"><asp:LinkButton ID="lbtndocnum" runat="server" Text='<%#Bind("clgCode") %>' onclick="lbtndocnum_Click" ></asp:LinkButton></div></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Activity"><ItemTemplate><div align="center"><asp:label ID="lblactivity" runat="server" Text='<%#Bind("Action") %>'  ></asp:label></div></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Type"><ItemTemplate><div align="center"><asp:label ID="lbltype" runat="server" Text='<%#Bind("Name") %>'  ></asp:label></div></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Subject"><ItemTemplate><div align="center"><asp:label ID="lblactsubject" runat="server" Text='<%#Bind("Subject") %>'  ></asp:label></div></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Assigned To Employee"><ItemTemplate><div align="center"><asp:label ID="lblAssigned" runat="server" Text='<%#Bind("EmpName") %>'  ></asp:label></div></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Assigned To User"><ItemTemplate><div align="center"><asp:label ID="lbluser" runat="server" Text='<%#Bind("UserName") %>'  ></asp:label></div></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Start Date"><ItemTemplate><div align="left">&nbsp;<asp:Label ID="lblstdate" runat="server" Text='<%#Bind("Recontact") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="End Date"><ItemTemplate><div align="left">&nbsp;<asp:Label ID="lbledDate" runat="server" Text='<%#Bind("endDate") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Remarks"><ItemTemplate><div align="left">&nbsp;<asp:Label ID="lblRejoin" runat="server" Text='<%#Bind("Details") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Priority"><ItemTemplate><div align="left">&nbsp;<asp:Label ID="lblreason" runat="server" Text='<%#Bind("Priority") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Status"><ItemTemplate><div align="left">&nbsp;<asp:Label ID="lblstatus" runat="server" Text='<%#Bind("Status") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField></Columns><HeaderStyle HorizontalAlign="Center" height="25px" BackColor="#CCCCCC"/></asp:GridView></td></tr></table></ContentTemplate></ajx:TabPanel>                
                  
                    
                 </ajx:TabContainer>   
              
              </td> 
              </tr> 
              </table> 
          </asp:Panel> 

           <asp:Panel ID="panelnew" runat="server" Width="98%" >
         <div id="Div1" runat="server" class="DivCorner" style="border:solid 2px LightSteelBlue; width:100%;">    
            <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
             <tr>
                
             
                  <td  width="7%">Employee No</td>
                  <td class="style1">
                   <asp:Label ID="txtempno"  CssClass="txtbox" Width="150px"  runat="server"></asp:Label>    
                    <asp:Label ID="lblsystemid"  CssClass="txtbox" Width="150px"  runat="server"  Visible="false" ></asp:Label>        
                  </td>
                    <td  width="5%"></td>
                   <td width="15%">Employee Name</td>
                 <td>
                 <asp:Label ID="txtFirstName" CssClass="txtbox" runat="server" Width="150px"></asp:Label>
                              
              </td>                         
            </tr>
             <tr>
                 <td width="10%">Position</td>
                 <td class="style1">
                  <asp:Label ID="txtposition" CssClass="txtbox" Width="200px"  runat="server" ></asp:Label>
                 </td>
                  <td  width="5%"></td>
                 <td width="10%">Department</td>
                 <td>
                  <asp:Label ID="lblDept" CssClass="txtbox" Width="200px"  runat="server" ></asp:Label>
                 </td>              
            </tr>             
             <tr>
                 <td width="10%">Activity</td>
                 <td class="style1">
                  <asp:DropDownList ID="ddlActivity" CssClass="txtbox1" Width="150px"  runat="server" Enabled="false">
                      <asp:ListItem Value="C">Phone Call</asp:ListItem>
                      <asp:ListItem Value="M">Meeting</asp:ListItem>
                      <asp:ListItem Value="T">Task</asp:ListItem>
                      <asp:ListItem Value="E">Note</asp:ListItem>
                      <asp:ListItem Value="P">Compaign</asp:ListItem>
                      <asp:ListItem Value="N">Other</asp:ListItem>
                  </asp:DropDownList>
                 </td>
                  <td  width="5%"></td>
                  <td  width="10%">Document Number</td>
                  <td width="30%">
                   <asp:Label ID="lbldocnum"  CssClass="txtbox" Width="150px"  runat="server"></asp:Label>        
                  </td>                
            </tr>             
                 <tr>
                 <td width="10%">Type</td>
                 <td class="style1">
                  <asp:DropDownList ID="ddltype" CssClass="txtbox1" Width="150px"  runat="server" AutoPostBack="true"></asp:DropDownList> 
                 </td>
                  <td  width="5%"></td>
                  <td  width="10%">Subject</td>
                  <td width="30%">
                    <asp:DropDownList ID="ddlSubject" CssClass="txtbox1" Width="150px"  runat="server"></asp:DropDownList>        
                  </td>                
            </tr>         
               <tr>
                 <td width="10%">Assaigned To</td>
                 <td class="style1">
                  <asp:DropDownList ID="ddlemp" CssClass="txtbox1" Width="100px"  runat="server" Enabled="false">
                    <asp:ListItem Value="U">User</asp:ListItem>
                    <asp:ListItem Value="E">Employee</asp:ListItem>
                  </asp:DropDownList>                 
                      <asp:TextBox ID="txtname" CssClass="txtbox"  runat="server" Enabled="false"></asp:TextBox> 
                 </td>
                  <td  width="5%">
                     <asp:TextBox ID="ddlAssaigned" CssClass="txtbox" runat="server" Visible="false"></asp:TextBox> 
                  </td>                       
            </tr>             
            </table>  
            </div>  
            </asp:Panel> 
      
            <asp:Panel ID="PanelNewRequest"  runat="server" Width="100%" BorderColor="LightSteelBlue"  BorderWidth="2">
                           <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content"> 
                                <tr>
                                  <td valign="top"> 
                                   <ajx:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0"  CssClass="ajax__tab_yuitabview-theme" Width="100%" >
                  <ajx:TabPanel  ID="TabPanel1" runat="server" HeaderText="General" ><ContentTemplate><table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content"><tr><td width="10%">Remarks</td><td colspan="2"><asp:TextBox ID="txtRemarks" CssClass="txtbox" Width="350px" Height="50px" runat="server" TextMode="MultiLine"></asp:TextBox></td><td  width="15%"></td></tr><tr><td width="10%">Start Date</td><td><asp:TextBox ID="txtstDate" CssClass="txtbox" Width="150px" runat="server" ></asp:TextBox><asp:TextBox ID="txtstTime" CssClass="txtbox" Width="150px" runat="server" Visible="false"></asp:TextBox><ajx:CalendarExtender ID="CalendarExtender1" Animated="true" Format="dd/MM/yyyy" runat="server" TargetControlID="txtstDate" CssClass= "cal_Theme1"></ajx:CalendarExtender></td><td  width="15%"><ajx:MaskedEditExtender ID="meeFromTime1" TargetControlID="txtstTime"
                                Mask="99:99"
                                MaskType="Time"
                                CultureName="en-us"
                              UserTimeFormat="TwentyFourHour" AutoComplete="false" MessageValidatorTip="true" ErrorTooltipEnabled="True" InputDirection="LeftToRight"
                                runat="server"></ajx:MaskedEditExtender><ajx:MaskedEditValidator ID="mevFromTime2" runat="server" ControlExtender="meeFromTime1" ControlToValidate="txtstTime" InvalidValueBlurredMessage="Enter correct time format" MaximumValue="23:59" IsValidEmpty="true" ValidationExpression="^([0-9][0-9])([:])([0-5][0-9])$"  Display="Static" ValidationGroup="Test"></ajx:MaskedEditValidator></td></tr><tr><td width="10%">End Date</td><td><asp:TextBox ID="txtedDate" CssClass="txtbox" Width="150px" runat="server" ></asp:TextBox><asp:TextBox ID="txtedTime" CssClass="txtbox" Width="150px" runat="server" Visible="false"></asp:TextBox><ajx:CalendarExtender ID="CalendarExtender2" Animated="true" Format="dd/MM/yyyy" runat="server" TargetControlID="txtedDate" CssClass= "cal_Theme1"></ajx:CalendarExtender></td><td  width="15%"><ajx:MaskedEditExtender ID="meeFromTime2" TargetControlID="txtedTime"
                                Mask="99:99"
                                MaskType="Time"
                                CultureName="en-us"
                              UserTimeFormat="TwentyFourHour" AutoComplete="false" MessageValidatorTip="true" ErrorTooltipEnabled="True" InputDirection="LeftToRight"
                                runat="server"></ajx:MaskedEditExtender><ajx:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeFromTime2" ControlToValidate="txtedTime" InvalidValueBlurredMessage="Enter correct time format" MaximumValue="23:59" IsValidEmpty="true" ValidationExpression="^([0-9][0-9])([:])([0-5][0-9])$"  Display="Static" ValidationGroup="Test"></ajx:MaskedEditValidator></td></tr><tr><td width="10%">Status</td><td><asp:DropDownList ID="ddlStatus" CssClass="txtbox1" Width="150px"  runat="server" ></asp:DropDownList></td><td  width="15%"></td></tr><tr><td width="10%">Priority</td><td><asp:DropDownList ID="ddlpriority" CssClass="txtbox1" Width="150px"  runat="server"><asp:ListItem Value="0">Low</asp:ListItem><asp:ListItem Value="1">Normal</asp:ListItem><asp:ListItem Value="2">High</asp:ListItem></asp:DropDownList></td><td  width="15%"></td></tr><tr><td width="10%"></td><td><asp:TextBox ID="txtDuration" CssClass="txtbox" Width="150px" runat="server" Visible="false"></asp:TextBox><asp:Label ID="lbldurtype" runat="server" Visible="false" ></asp:Label></td><td  width="15%"></td></tr></table></ContentTemplate></ajx:TabPanel> 

                    <ajx:TabPanel  ID="TabPanel2" runat="server" HeaderText="Content" ><ContentTemplate><table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content"><tr><td valign="top"><asp:TextBox ID="txtContent" runat="server" CssClass="txtbox" Width="350px" Height="150px" TextMode="MultiLine"></asp:TextBox></td></tr></table></ContentTemplate></ajx:TabPanel> 


                                   <%--    <ajx:TabPanel  ID="TabPanel4" runat="server" HeaderText="Attachment" Visible="false">
                    <ContentTemplate>
                    
                     <table width="100%" border="0" cellspacing="0" cellpadding="4" class="main_content">   
                     <tr>
                     <td width="5%">Attachement</td>
                     <td width="16%"> 
                           
                        <asp:FileUpload ID="FileUpload1" runat="server"   />
                     </td>
                     <td>
                        
                     <asp:Button ID="btnUpload" runat="server" Text="Upload" Width="85px" CssClass="btn" onclick="btnUpload_Click" />
                     </td>   
                      </tr>                           
                         <tr>
                           <td colspan="3">
                               <asp:GridView ID="grdAttachment" runat="server" CellPadding="4" AllowPaging="True" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found" CssClass="mGrid"  HeaderStyle-Cssclass="GridBG"
                                PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt"  AutoGenerateColumns="False" Width="100%" PageSize="10" >
                            <Columns>                          
                                <asp:TemplateField HeaderText="File Name">
                                    <ItemTemplate>
                                      <div align="left">&nbsp;<asp:Label ID="lblfilename" runat="server" Text='<%#Bind("U_FileName") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>                                
                                 <asp:TemplateField HeaderText="Attachment Date" Visible="false">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lbldate" runat="server" Text='<%#Bind("U_ActCode") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>                                  
                                 <asp:TemplateField HeaderText="LineId" Visible="false">
                                    <ItemTemplate>
                                      <div align="left">&nbsp;<asp:Label ID="lbllineid" runat="server" Text='<%#Bind("U_DocEntry") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField> 
                                 <asp:TemplateField HeaderText="LineId" Visible="false">
                                    <ItemTemplate>
                                      <div align="left">&nbsp;<asp:Label ID="lbllineid1" runat="server" Text='<%#Bind("U_Status") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>  
                                  <asp:TemplateField>
                                    <ItemTemplate>
                                     <asp:LinkButton ID="lnkDownload" Text = "Download" CommandArgument = '<%# Eval("U_FileName") %>' runat="server" OnClick="lnkDownload_Click"></asp:LinkButton>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>  
                                <asp:CommandField ShowDeleteButton="true" />
                                </Columns>
                            <HeaderStyle HorizontalAlign="Center" height="25px" BackColor="#CCCCCC"/>
                           </asp:GridView>
                          </td>
                         </tr> 
                      </table>
                     
                    </ContentTemplate>
                    </ajx:TabPanel>--%>


                   </ajx:TabContainer>  
                                  </td> 
                                
                                    <tr>
                <td>
                <br />
                    <asp:Button ID="btnAdd"  CssClass="btn" Width="85px" runat="server" Text="Save" ValidationGroup="Test"  OnClientClick="return Confirmation();" /> 
                   <asp:Button ID="btncancel"  CssClass="btn" Width="85px" runat="server" Text="Cancel" />
                  
                </td>
            </tr>
                                </table> 
                                </asp:Panel>
        </td>
        </tr>
        </table>         
        </td> 
        </tr> 
 </table> 

</ContentTemplate> 
</asp:UpdatePanel> 
</asp:Content>

