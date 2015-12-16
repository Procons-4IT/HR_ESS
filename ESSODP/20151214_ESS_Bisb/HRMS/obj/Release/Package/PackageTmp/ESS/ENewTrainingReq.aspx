<%@ Page Title="New Training Request" Language="vb" AutoEventWireup="false" MasterPageFile="~/ESS/ESSMaster.Master" CodeBehind="ENewTrainingReq.aspx.vb" Inherits="HRMS.ENewTrainingReq" %>
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

        function Confirmation1() {
            if (confirm("Sure Want to withdraw the request?") == true) {
                return true;
            }
            else {
                return false;
            }
        }


        function AllowNumbers(el) {
            var ex = /^[0-9.]+$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
            }
        }
        function alphanumerichypen(el) {
            var ex = /^[A-Za-z0-9_-]+$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
            }
        }

        function checkDec(el) {
            el.value = el.value.replace(/^[ 0]+/, '');
            var ex = /^\d*\.?\d{0,2}$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
            }
        }

        function RemoveZero(el) {
            el.value = el.value.replace(/^[ 0]+/, '');
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

    <td height="30" align="left" colspan="2" valign="bottom" background="images/h_bg.png"; style="border-bottom:1px dotted; border-color: #f45501; background-repeat:repeat-x">     
      <div >&nbsp; <asp:Label ID="Label3" runat="server" Text="New Training Request"  CssClass="subheader" style="float:left;" ></asp:Label>  <span ><asp:Label ID="lblNewTrip" runat="server" Text="" Visible="false"></asp:Label></span> </div>
      </td>    
 </tr>

 <tr>      
      <td align="center">
        <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
        <tr>
        <td>
          <asp:Panel ID="panelhome" runat="server" Width="100%"> 
            <asp:ImageButton ID="btnhome" runat="server" ImageUrl="~/images/Homeicon.jpg"   PostBackUrl="~/ESS/ESSHome.aspx"
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
                  <ajx:TabPanel  ID="TabPanel3" runat="server" HeaderText="New Training Request" ><ContentTemplate>
                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                          <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content">
                              <tr>
                                 <td>
                                      <asp:GridView ID="grdTrainingRequest" runat="server" CellPadding="4" AllowPaging="True" ShowHeaderWhenEmpty="true"  CssClass="mGrid" HeaderStyle-Cssclass="GridBG" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false" Width="100%" PageSize="10" >
                                          <Columns>
                                              <asp:TemplateField HeaderText="Request Code">
                                                  <ItemTemplate>
                                                      <div align="center">
                                                          <asp:LinkButton ID="lbtndocnum" runat="server" Text='<%#Bind("DocEntry") %>' onclick="lbtndocnum_Click">
                                                  </asp:LinkButton>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Request Date">
                                                  <ItemTemplate>
                                                      <div align="center">
                                                          <asp:label ID="lbllReqdt" runat="server" Text='<%#Bind("U_Z_ReqDate") %>'  ></asp:label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Training Title">
                                                  <ItemTemplate>
                                                      <div align="center">
                                                          <asp:label ID="lblTrtitle" runat="server" Text='<%#Bind("U_Z_CourseName") %>'  ></asp:label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Justification">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lbljust" runat="server" Text='<%#Bind("U_Z_CourseDetails") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Training From Date">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblfrDate" runat="server" Text='<%#Bind("U_Z_TrainFrdt") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Training To Date">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lbltodt" runat="server" Text='<%#Bind("U_Z_TrainTodt") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Training Course Cost">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lbltrcost" runat="server" Text='<%#Bind("U_Z_TrainCost") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Comments">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblreason" runat="server" Text='<%#Bind("U_Z_Notes") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Approval Status">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblstatus" runat="server" Text='<%#Bind("U_Z_AppStatus") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Approval History">
                                                    <ItemTemplate>
                                                        <div align="left">&nbsp;<asp:LinkButton ID="lbtAppHistory" runat="server" Text="View" onclick="lbtAppHistory_Click" ></asp:LinkButton></div>
                                                    </ItemTemplate>                                    
                                                </asp:TemplateField>
                                          </Columns>
                                          <HeaderStyle HorizontalAlign="Center" height="25px" BackColor="#CCCCCC"/>
                                      </asp:GridView>
                                  </td>
                              </tr>
                          </table>
                      </ContentTemplate></ajx:TabPanel>                
                  
                    
                 </ajx:TabContainer>   
              
              </td> 
              </tr> 
              </table> 
          </asp:Panel> 
      
            <asp:Panel ID="PanelNewRequest"  runat="server" Width="100%" BorderColor="LightSteelBlue"  BorderWidth="2">
                           <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content"> 
                                <tr>
                                 <td >Request Code</td>
                                     <td>                                     
                                       <asp:TextBox ID="txtreqcode" CssClass="txtbox" runat="server" Enabled="False" ></asp:TextBox>    
                                        <asp:TextBox ID="txtcode" CssClass="txtbox" runat="server" Visible="False" ></asp:TextBox>                                                
                                     </td>  
                                       <td>Request Date</td>
                                     <td>
                                     <asp:TextBox ID="txtreqdate" CssClass="txtbox"  runat="server"  Enabled="False" ></asp:TextBox>                                           
                                     </td>
                                     </tr>

                                      <tr>
                                    <td >Employee Code</td>
                                     <td>                                     
                                       <asp:TextBox ID="txtempid" CssClass="txtbox" runat="server" Enabled="False" ></asp:TextBox>                                                
                                     </td>  
                                       <td>Employee Name</td>
                                     <td>
                                     <asp:TextBox ID="txtempname" CssClass="txtbox"  runat="server"  Enabled="False" ></asp:TextBox>                                           
                                     </td>
                                     </tr>

                                      <tr>
                                    <td >Department</td>
                                     <td>                                     
                                       <asp:TextBox ID="txtdeptName" CssClass="txtbox" runat="server" Enabled="False" ></asp:TextBox> 
                                        <asp:TextBox ID="txtdeptcode" CssClass="txtbox" runat="server" Visible="False" ></asp:TextBox>                                               
                                     </td>  
                                       <td>Position</td>
                                     <td>
                                     <asp:TextBox ID="txtpositionName" CssClass="txtbox"  runat="server"  Enabled="False" ></asp:TextBox> 
                                      <asp:TextBox ID="txtposCode" CssClass="txtbox"  runat="server"  Visible="False" ></asp:TextBox>                                           
                                     </td>
                                     </tr>

                                      <tr>
                                    <td >Training Title (*)</td>
                                     <td>                                     
                                       <asp:TextBox ID="txttrtitle" CssClass="txtbox" runat="server" ></asp:TextBox>                                                
                                     </td>  
                                       <td>Justification</td>
                                     <td>
                                     <asp:TextBox ID="txtjust" CssClass="txtbox"  runat="server" ></asp:TextBox>                                           
                                     </td>
                                     </tr>

                                      <tr>
                                    <td >Training Course Cost</td>
                                     <td>                                     
                                       <asp:TextBox ID="txtcost" CssClass="txtbox" runat="server"  onkeypress="AllowNumbers(this);checkDec(this);RemoveZero(this);" onkeyup="AllowNumbers(this);checkDec(this);RemoveZero(this);"></asp:TextBox>                                                
                                     </td>  
                                       <td>Est.Travel and Living Expenses</td>
                                     <td>
                                     <asp:TextBox ID="txtestexp" CssClass="txtbox"  runat="server" onkeypress="AllowNumbers(this);checkDec(this);RemoveZero(this);" onkeyup="AllowNumbers(this);checkDec(this);RemoveZero(this);"></asp:TextBox>                                           
                                     </td>
                                     </tr>                                    
                                      <tr>
                                      <td >Training From Date (*)</td>
                                     <td>
                                      <asp:TextBox ID="txtfrmdate" CssClass="txtbox" runat="server" AutoPostBack="true"></asp:TextBox> 
                                       <ajx:CalendarExtender ID="CalendarExtender2" Animated="true" Format="dd/MM/yyyy" runat="server" TargetControlID="txtfrmdate" CssClass= "cal_Theme1"></ajx:CalendarExtender>                                             
                                     </td>
                                      <td >Training To Date (*)</td>
                                     <td>
                                      <asp:TextBox ID="txttodate" CssClass="txtbox"  runat="server" AutoPostBack="true" ></asp:TextBox>  
                                       <ajx:CalendarExtender ID="CalendarExtender1" Animated="true" Format="dd/MM/yyyy" runat="server" TargetControlID="txttodate" CssClass= "cal_Theme1"></ajx:CalendarExtender>                                            
                                     </td>
                                     </tr>
                                    
                                        <tr>
                                       <td >Training Location</td>
                                     <td>
                                      <asp:TextBox ID="txtTrainloc" CssClass="txtbox"  runat="server" ></asp:TextBox>                                             
                                     </td>
                                     <td></td>
                                     <td></td>
                                      </tr>
                                       <tr>
                                       <td >Training Duration (Business Days)</td>
                                     <td>
                                      <asp:TextBox ID="txtBussDur" CssClass="txtbox"  runat="server" onkeypress="AllowNumbers(this);checkDec(this);RemoveZero(this);"></asp:TextBox>  
                                     </td>
                                     <td >Training Duration (Calendar Days)</td>
                                     <td>
                                      <asp:TextBox ID="txtCalDur" CssClass="txtbox"  runat="server" onkeypress="AllowNumbers(this);checkDec(this);RemoveZero(this);"></asp:TextBox>  
                                     </td>
                                      </tr>
                                      <tr>
                                      <td >Away from Office(Business Days)</td>
                                     <td>
                                      <asp:TextBox ID="txtawaybuss" CssClass="txtbox"  runat="server" onkeypress="AllowNumbers(this);checkDec(this);RemoveZero(this);"></asp:TextBox>                                             
                                     </td>
                                      <td >Certification Test Available</td>
                                      <td>
                                         <asp:RadioButtonList ID="Rblcert"  runat="server" RepeatDirection="Horizontal" >
                                         <asp:ListItem Value="2">Yes</asp:ListItem>
                                          <asp:ListItem Value="1">No</asp:ListItem>
                                          </asp:RadioButtonList>
                                      </td>
                                     </tr>
                                      <tr>
                                      <td >Trainee Leaves Duty On</td>
                                     <td>
                                      <asp:TextBox ID="txtLveDutyon" CssClass="txtbox"  runat="server"></asp:TextBox>  
                                     <ajx:CalendarExtender ID="CalendarExtender3" Animated="true" Format="dd/MM/yyyy" runat="server" TargetControlID="txtLveDutyon" CssClass= "cal_Theme1"></ajx:CalendarExtender>                                             
                                           
                                     </td>
                                      <td >Certification Test Included</td>
                                      <td>
                                         <asp:RadioButtonList ID="RblTestinc"  runat="server" RepeatDirection="Horizontal" >
                                         <asp:ListItem Value="2">Yes</asp:ListItem>
                                          <asp:ListItem Value="1">No</asp:ListItem>
                                          </asp:RadioButtonList>
                                      </td>
                                     </tr>
                                      <tr>
                                       <td >Trainee Travels On</td>
                                     <td>
                                      <asp:TextBox ID="txtTravelon" CssClass="txtbox"  runat="server" ></asp:TextBox>  
                                     <ajx:CalendarExtender ID="CalendarExtender4" Animated="true" Format="dd/MM/yyyy" runat="server" TargetControlID="txtTravelon" CssClass= "cal_Theme1"></ajx:CalendarExtender>                                             

                                     </td>
                                     <td >Trainee Returns On</td>
                                     <td>
                                      <asp:TextBox ID="txtReturnon" CssClass="txtbox"  runat="server" ></asp:TextBox> 
                                      <ajx:CalendarExtender ID="CalendarExtender5" Animated="true" Format="dd/MM/yyyy" runat="server" TargetControlID="txtReturnon" CssClass= "cal_Theme1"></ajx:CalendarExtender>                                             
 
                                     </td>
                                      </tr>

                                        <tr>
                                       <td >Trainee Resumes Duty On</td>
                                     <td>
                                      <asp:TextBox ID="txtresumes" CssClass="txtbox"  runat="server" ></asp:TextBox> 
                                       <ajx:CalendarExtender ID="CalendarExtender6" Animated="true" Format="dd/MM/yyyy" runat="server" TargetControlID="txtresumes" CssClass= "cal_Theme1"></ajx:CalendarExtender>                                              
                                     </td>
                                     <td ></td>
                                     <td></td>
                                      </tr>
                                        <tr>
                                       <td >Notes/Comments</td>
                                     <td colspan="2">
                                      <asp:TextBox ID="txtComments" CssClass="txtbox" TextMode="MultiLine" Width="400px" Height="70px"  runat="server" ></asp:TextBox>  
                                     </td>
                                   </tr>
                                
                                    <tr>
                <td colspan="2" align="center">
                <br />
                    <asp:Button ID="btnAdd"  CssClass="btn" Width="85px" runat="server" Text="Save"  OnClientClick="return Confirmation();" /> 
                   <asp:Button ID="btncancel"  CssClass="btn" Width="85px" runat="server" Text="Cancel" />
                    <asp:Button ID="btnWithdraw"  CssClass="btn"  runat="server" Text="WithDraw Request" Visible="false" OnClientClick="return Confirmation1();" /> 
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
        <td>
          <div style="visibility:hidden">
<asp:Button id="btnSample" runat="server"/>
</div>
<ajx:ModalPopupExtender ID="ModalPopupExtender7" runat="server" DropShadow="True" PopupControlID="Panelpoptechnician" TargetControlID="btnSample" CancelControlID="btnclstech" BackgroundCssClass="modalBackground">
                    </ajx:ModalPopupExtender>  
        </td>
        </tr>
        <tr>
 <td>
  <asp:Panel ID="Panelpoptechnician" runat="server" BackColor="White" style=" display:none; padding:10px; width:900px; ">
                                <div><span class="sideheading" style="color:Green;">Approval History Details</span> <span style="float:right;"> 
                                <asp:Button ID="btnclstech" runat="server"  CssClass="btn" Width="30px" Text="X" /></span></div>
                                   <br />
                           
                              <br /><asp:Panel ID="Panel4" runat="server" Height="400px" ScrollBars="Vertical">
                                  <asp:Label ID="Label1" runat="server" Text="" CssClass="txtbox" ForeColor="Red"></asp:Label>
                                 <asp:GridView ID="grdRequesttohr" runat="server" CellPadding="4" ShowHeaderWhenEmpty="true"  CssClass="mGrid" HeaderStyle-Cssclass="GridBG"   AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false" Width="100%" ><Columns>
                                          <asp:TemplateField HeaderText="Employee ID">
                                              <ItemTemplate><div align="center">
                                                  <asp:label ID="lbtndocnum" runat="server" Text='<%#Bind("U_Z_EmpId") %>'  ></asp:label></div></ItemTemplate></asp:TemplateField>
                                          <asp:TemplateField HeaderText="Employee Name">
                                              <ItemTemplate><div align="center">
                                                  <asp:label ID="lblactivity" runat="server" Text='<%#Bind("U_Z_EmpName") %>'  ></asp:label></div></ItemTemplate></asp:TemplateField>
                                          <asp:TemplateField HeaderText="Approved By">
                                              <ItemTemplate><div align="center">
                                                  <asp:label ID="lbltype" runat="server" Text='<%#Bind("U_Z_ApproveBy") %>'  ></asp:label></div>
                                                  </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="CreateDate">
                                              <ItemTemplate><div align="center">
                                                  <asp:label ID="lblcrDate" runat="server" Text='<%#Bind("CreateDate") %>'  ></asp:label></div></ItemTemplate></asp:TemplateField>
                                          <asp:TemplateField HeaderText="Create Time">
                                              <ItemTemplate><div align="center">
                                                  <asp:label ID="lblcrtime" runat="server" Text='<%#Bind("CreateTime") %>'  ></asp:label></div></ItemTemplate></asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Update Date">
                                              <ItemTemplate><div align="center">
                                                  <asp:label ID="lblupdate" runat="server" Text='<%#Bind("UpdateDate") %>'  ></asp:label></div></ItemTemplate></asp:TemplateField>
                                          <asp:TemplateField HeaderText="Update Time">
                                              <ItemTemplate><div align="center">
                                                  <asp:label ID="lblupdatime" runat="server" Text='<%#Bind("UpdateTime") %>'  ></asp:label></div></ItemTemplate></asp:TemplateField>

                                              <asp:TemplateField HeaderText="Approved Status">
                                              <ItemTemplate><div align="center">
                                                  <asp:label ID="lblactsubject" runat="server" Text='<%#Bind("U_Z_AppStatus") %>'  ></asp:label></div>
                                                  </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Remarks">
                                              <ItemTemplate><div align="center">
                                                  <asp:label ID="lblAssigned" runat="server" Text='<%#Bind("U_Z_Remarks") %>'  ></asp:label></div>
                                                  </ItemTemplate>
                                            </asp:TemplateField>                                             
                                    </Columns>
                                    <HeaderStyle HorizontalAlign="Center" height="25px" BackColor="#CCCCCC"/>
                                    </asp:GridView>
                                    </asp:Panel>
                            </asp:Panel>
 </td>
 </tr>
</table> 

</ContentTemplate> 
</asp:UpdatePanel> 
</asp:Content>
