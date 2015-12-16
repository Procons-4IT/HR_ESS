<%@ Page Title="Employee Training Schedule" Language="vb" AutoEventWireup="false" MasterPageFile="~/HRMS.Master" CodeBehind="TrainReq.aspx.vb" Inherits="HRMS.TrainReq" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <script type="text/javascript">
       function Confirmation() {
           if (confirm("Do you want to confirm the Training ?") == true) {
               return true;
           }
           else {
               return false;
           }
       }

       function WithdrawConfirmation() {
           if (confirm("Do you want to withdraw the selected trainning?") == true) {
               return true;
           }
           else {
               return false;
           }
       }
</script>

   
<script type="text/javascript">
    //
    var prm = sys.WebForms.PageRequestManager.getInstance();
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
<asp:Image ID="Image1" ImageUrl="Images/waiting.gif" AlternateText="Processing" runat="server" />
</ProgressTemplate>
</asp:UpdateProgress>
<ajx:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress" PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />


<asp:UpdatePanel ID="Update" runat="server">
            <ContentTemplate>
 <table width="100%" border="0" cellspacing="0" cellpadding="4" class="main_content">
 <tr>
 <td>
    

 </td>
 </tr>
  <tr>
      
      <td align="center">
        <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
         <tr>

    <td height="30" align="left" colspan="2" valign="bottom" background="images/h_bg.png"; style="border-bottom:1px dotted; border-color: #f45501; background-repeat:repeat-x">     
      <div >&nbsp; <asp:Label ID="Label3" runat="server" Text="Employee Training Schedule"  CssClass="subheader" style="float:left;" ></asp:Label> 
       <span ><asp:Label ID="lblNewTrip" runat="server" Text="" Visible="false"></asp:Label></span> </div>
      </td>    
 </tr>

        <tr>
        <td>
          <asp:Panel ID="panelhome" runat="server" Width="100%"> 
            <asp:ImageButton ID="btnhome" runat="server" ImageUrl="images/Homeicon.jpg" PostBackUrl="~/Home.aspx" 
                    ToolTip="Home" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:ImageButton ID="btnnew" runat="server" ImageUrl="images/Add.jpg" ToolTip="Add new record" visible="false"
                  /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         
            </asp:Panel> 
            <asp:Label ID="Label2" runat="server" Text="" style="color:Red;"></asp:Label>
        
        </td>
        </tr>
        </table>
         <div id="Empdetails" runat="server" class="DivCorner" style="border:solid 2px LightSteelBlue; width:99%;">    
            <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
             <tr>
                 <td width="15%">Employee Id</td>
                 <td>
                  <asp:Label ID="txtempid" CssClass="txtbox" Width="150px"  runat="server" ></asp:Label>
                 </td>
                  <td  width="10%"></td>
                  <td  width="15%">Employee Name</td>
                  <td width="15%">
                   <asp:Label ID="txtempname"  CssClass="txtbox" Width="150px"  runat="server"></asp:Label>        
                  </td>                
            </tr>
             <tr>
                 <td width="10%">Position Code</td>
                 <td>
                  <asp:Label ID="txtposid" CssClass="txtbox" Width="150px"  runat="server" ></asp:Label>
                 </td>
                  <td  width="15%"></td>
                  <td  width="10%">Position Name</td>
                  <td width="30%">
                   <asp:Label ID="txtposname"  CssClass="txtbox" Width="150px"  runat="server"></asp:Label>        
                  </td>                
            </tr>
             <tr>
                 <td width="10%">Department Id</td>
                 <td>
                  <asp:Label ID="txtdept" CssClass="txtbox" Width="150px"  runat="server" ></asp:Label>
                 </td>
                  <td  width="15%"></td>
                    <td  width="12%">Department Name</td>
                  <td width="20%">
                   <asp:Label ID="txtdeptname"  CssClass="txtbox" Width="150px"  runat="server"></asp:Label>        
                  </td>                
            </tr> 
                 
            </table>  
            </div>  
          <asp:Panel ID="paltab" runat="server" Width="99%" BorderColor="LightSteelBlue"  BorderWidth="2">
            <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
              <tr>
              <td valign="top">  
                <ajx:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0"  CssClass="ajax__tab_yuitabview-theme" Width="99%" >
                  <ajx:TabPanel  ID="TabPanel1" runat="server" HeaderText="Scheduled Training" >
                   <ContentTemplate>
                      <table width="99%" border="0" cellspacing="0" cellpadding="3" class="main_content">                                   
                                   <tr>
                                     <td colspan="4">
                                   <div style="overflow-x:auto;width:1100px;">
                              <asp:GridView ID="GridView1" runat="server" CellPadding="4" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found" CssClass="mGrid" HeaderStyle-Cssclass="GridBG" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" AutoGenerateSelectButton="true" AllowPaging="True" RowStyle-CssClass="mousecursor" AutoGenerateColumns="False" Width="100%" PageSize="10" >
                             <SelectedRowStyle BackColor="#FFA20C" />
                            <Columns>
                                                           
                                 <asp:TemplateField HeaderText="Agenda Code">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblTtracode" runat="server" Text='<%#Bind("U_Z_TrainCode") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Agenda Date">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblTAgaDate" runat="server" Text='<%#Bind("U_Z_DocDate") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>                                  
                                 <asp:TemplateField HeaderText="Course Code" Visible="false">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblTcouCode" runat="server" Text='<%#Bind("U_Z_CourseCode") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Course">
                                    <ItemTemplate>
                                       <div align="left">&nbsp;<asp:Label ID="lblTCouName" runat="server" Text='<%#Bind("U_Z_CourseName") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Course Type">
                                    <ItemTemplate>
                                        <div align="center">&nbsp;<asp:Label ID="lblTCouType" runat="server" Text='<%#Bind("U_Z_CourseTypeDesc") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>    
                                 <asp:TemplateField HeaderText="Course Start Date">
                                    <ItemTemplate>
                                        <div align="left"><asp:Label ID="lblTfromdt" runat="server" Text='<%#Bind("U_Z_Startdt") %>' ></asp:Label>&nbsp;&nbsp;</div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>  
                                 <asp:TemplateField HeaderText="Course End Date">
                                    <ItemTemplate>
                                        <div align="center">&nbsp;<asp:Label ID="lblTtodt" runat="server" Text='<%#Bind("U_Z_Enddt") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>    
                                 <asp:TemplateField HeaderText="Min.Attendees">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblTMinAtte" runat="server" Text='<%#Bind("U_Z_MinAttendees") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>    
                             <asp:TemplateField HeaderText="Max.Attendees">
                                    <ItemTemplate>
                                        <div align="left"><asp:Label ID="lblTMax" runat="server" Text='<%#Bind("U_Z_MaxAttendees") %>' ></asp:Label>&nbsp;&nbsp;</div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Instructor Name">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblTInstruction" runat="server" Text='<%#Bind("U_Z_InsName") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>  
                                 <asp:TemplateField HeaderText="Instructor Code" Visible="false">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblTInsCode" runat="server" Text='<%#Bind("InstCode") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>                             
                                                   
                                <asp:TemplateField HeaderText="Application Start Date">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblTStDate" runat="server" Text='<%# Eval("U_Z_AppStdt") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Application End Date" >
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblTEndDate" runat="server" Text='<%# Eval("U_Z_AppEnddt") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>    
                                     <asp:TemplateField HeaderText="No.of Hours">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblThours" runat="server" Text='<%# Eval("U_Z_NoOfHours") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>                          
                            <asp:TemplateField HeaderText="Start Time">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblTstarttime" runat="server" Text='<%# Eval("U_Z_StartTime") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="End Time">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblTendtime" runat="server" Text='<%# Eval("U_Z_EndTime") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Attendees Cost">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblTAttCost" runat="server" Text='<%# Eval("U_Z_AttCost") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Sunday">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblsunday" runat="server" Text='<%#Bind("U_Z_Sunday") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>                       
                                                   
                                <asp:TemplateField HeaderText="Monday">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblMonday" runat="server" Text='<%# Eval("U_Z_Monday") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Tuesday" >
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblTuesday" runat="server" Text='<%# Eval("U_Z_Tuesday") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>    
                                     <asp:TemplateField HeaderText="Wednesday">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblWednesday" runat="server" Text='<%# Eval("U_Z_Wednesday") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>                          
                            <asp:TemplateField HeaderText="Thursday">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblThursday" runat="server" Text='<%# Eval("U_Z_Thursday") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Friday">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblFriday" runat="server" Text='<%# Eval("U_Z_Friday") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Saturday">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblSaturday" runat="server" Text='<%# Eval("U_Z_Saturday") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>   
                                   <asp:TemplateField HeaderText="Active">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblactive" runat="server" Text='<%# Eval("U_Z_Active") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>                                           
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" height="25px" BackColor="#CCCCCC"/>
                           </asp:GridView>
                           </div>
                                     </td>
                                     </tr>
                                     
                                      <tr>
                <td  align="left">
                <br />
                    <asp:Button ID="btnsubmit"  CssClass="btn" Width="85px" runat="server" Text="Apply" OnClientClick="return Confirmation();"/>
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnclose"  CssClass="btn" Width="85px" runat="server" Text="Cancel" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     
                </td>
            </tr>                             
                                  </table>
                   
                   </ContentTemplate> 
                  </ajx:TabPanel> 
                  
                     <ajx:TabPanel  ID="TabPanel2" runat="server" HeaderText="Applied Training" >
                    <ContentTemplate>
                     <table width="99%" border="0" cellspacing="0" cellpadding="3" class="main_content">                                   
                                   <tr>
                                     <td>
                                     <div style="overflow-x:auto;width:1100px;">
                               <asp:GridView ID="grdScheduled" runat="server" CellPadding="4"  ShowHeaderWhenEmpty="true" EmptyDataText="No records Found" CssClass="mGrid" HeaderStyle-Cssclass="GridBG" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" AllowPaging="True" AutoGenerateSelectButton="true" AutoGenerateColumns="False" Width="100%" PageSize="10" >
                             <SelectedRowStyle BackColor="#FFA20C" />
                            <Columns>
                               <asp:TemplateField HeaderText="Agenda Code">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lbltracode" runat="server" Text='<%#Bind("U_Z_TrainCode") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField> 
                                  <asp:TemplateField HeaderText="Code" visible="false">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblAppCode" runat="server" Text='<%#Bind("Code") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>                             
                                 <asp:TemplateField HeaderText="Course Code" Visible="false">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblcouCode" runat="server" Text='<%#Bind("U_Z_CourseCode") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Course">
                                    <ItemTemplate>
                                       <div align="left">&nbsp;<asp:Label ID="lblCouName" runat="server" Text='<%#Bind("U_Z_CourseName") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Course Type">
                                    <ItemTemplate>
                                        <div align="center">&nbsp;<asp:Label ID="lblVenue" runat="server" Text='<%#Bind("U_Z_CourseTypeDesc") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>    
                                 <asp:TemplateField HeaderText="Course Start Date">
                                    <ItemTemplate>
                                        <div align="left"><asp:Label ID="lblfromdt" runat="server" Text='<%#Bind("U_Z_Startdt") %>' ></asp:Label>&nbsp;&nbsp;</div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>  
                                 <asp:TemplateField HeaderText="Course End Date">
                                    <ItemTemplate>
                                        <div align="center">&nbsp;<asp:Label ID="lbltodt" runat="server" Text='<%#Bind("U_Z_Enddt") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>    
                                 <asp:TemplateField HeaderText="Min.Attendees">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblMinAtte" runat="server" Text='<%#Bind("U_Z_MinAttendees") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>    
                             <asp:TemplateField HeaderText="Max.Attendees">
                                    <ItemTemplate>
                                        <div align="left"><asp:Label ID="lblMax" runat="server" Text='<%#Bind("U_Z_MaxAttendees") %>' ></asp:Label>&nbsp;&nbsp;</div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Instructor Name">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblInstruction" runat="server" Text='<%#Bind("U_Z_InsName") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>                       
                                                   
                                <asp:TemplateField HeaderText="Application Start Date">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblStDate" runat="server" Text='<%# Eval("U_Z_AppStdt") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Application End Date" >
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblEndDate" runat="server" Text='<%# Eval("U_Z_AppEnddt") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>    
                                     <asp:TemplateField HeaderText="No.of Hours">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblhours" runat="server" Text='<%# Eval("U_Z_NoOfHours") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>                          
                            <asp:TemplateField HeaderText="Start Time">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblstarttime" runat="server" Text='<%# Eval("U_Z_StartTime") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="End Time">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblendtime" runat="server" Text='<%# Eval("U_Z_EndTime") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Attendees Cost">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblAttCost" runat="server" Text='<%# Eval("U_Z_AttCost") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                              <asp:TemplateField HeaderText="Applicant Status">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblStatus" runat="server" Text='<%# Eval("U_Z_Status") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>                            
                                                                       
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" height="25px" BackColor="#CCCCCC"/>
                           </asp:GridView>
                           </div> 
                                     </td>
                                     </tr>          
                                     <tr>                                     
                                     <td>
                                      <asp:Button ID="btnWithdrawReq"  CssClass="btn" Width="200px" runat="server" Text="WithDraw Application" OnClientClick="return WithdrawConfirmation();"  />
                                     </td>
                                     </tr>                   
                                  </table>
                    </ContentTemplate> 
                    </ajx:TabPanel> 
                    
                         <ajx:TabPanel  ID="TabPanel3" runat="server" HeaderText="New Training Request Summary" >
                    <ContentTemplate>
                     <table width="99%" border="0" cellspacing="0" cellpadding="3" class="main_content">                                   
                                   <tr>
                                     <td>
                                     
                               <asp:GridView ID="grdNewTraining" runat="server" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found" CssClass="mGrid" HeaderStyle-Cssclass="GridBG" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" CellPadding="4" AllowPaging="True" AutoGenerateColumns="False" Width="100%" PageSize="10" >
                          <Columns>
                                              <asp:TemplateField HeaderText="Request Code">
                                                  <ItemTemplate>
                                                      <div align="center">
                                                          <asp:label ID="lbtndocnum" runat="server" Text='<%#Bind("DocEntry") %>'>
                                                  </asp:label>
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
                                          </Columns>
                            <HeaderStyle HorizontalAlign="Center" height="25px" BackColor="#CCCCCC"/>
                           </asp:GridView>
                                     </td>
                                     </tr>                             
                                  </table>
                    </ContentTemplate> 
                    </ajx:TabPanel> 

                       <ajx:TabPanel  ID="TabPanel4" runat="server" HeaderText="Course Acquired" >
                    <ContentTemplate>
                     <table width="99%" border="0" cellspacing="0" cellpadding="3" class="main_content">                                   
                                   <tr>
                                     <td>
                                     <div style="overflow-x:auto;width:1100px;">
                               <asp:GridView ID="grdCourseAcq" runat="server" CellPadding="4"  ShowHeaderWhenEmpty="true" EmptyDataText="No records Found" CssClass="mGrid" HeaderStyle-Cssclass="GridBG" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" AllowPaging="True" AutoGenerateColumns="False" Width="100%" PageSize="10" >
                             <SelectedRowStyle BackColor="#FFA20C" />
                            <Columns>
                               <asp:TemplateField HeaderText="Agenda Code">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblCtracode" runat="server" Text='<%#Bind("U_Z_TrainCode") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField> 
                                  <asp:TemplateField HeaderText="Code" visible="false">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblCAppCode" runat="server" Text='<%#Bind("Code") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>                             
                                 <asp:TemplateField HeaderText="Course Code" Visible="false">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblCcouCode" runat="server" Text='<%#Bind("U_Z_CourseCode") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Course">
                                    <ItemTemplate>
                                       <div align="left">&nbsp;<asp:Label ID="lblCCouName" runat="server" Text='<%#Bind("U_Z_CourseName") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Course Type">
                                    <ItemTemplate>
                                        <div align="center">&nbsp;<asp:Label ID="lblCVenue" runat="server" Text='<%#Bind("U_Z_CourseTypeDesc") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>    
                                 <asp:TemplateField HeaderText="Course Start Date">
                                    <ItemTemplate>
                                        <div align="left"><asp:Label ID="lblCfromdt" runat="server" Text='<%#Bind("U_Z_Startdt") %>' ></asp:Label>&nbsp;&nbsp;</div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>  
                                 <asp:TemplateField HeaderText="Course End Date">
                                    <ItemTemplate>
                                        <div align="center">&nbsp;<asp:Label ID="lblCtodt" runat="server" Text='<%#Bind("U_Z_Enddt") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>    
                                 <asp:TemplateField HeaderText="Min.Attendees">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblCMinAtte" runat="server" Text='<%#Bind("U_Z_MinAttendees") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>    
                             <asp:TemplateField HeaderText="Max.Attendees">
                                    <ItemTemplate>
                                        <div align="left"><asp:Label ID="lblCMax" runat="server" Text='<%#Bind("U_Z_MaxAttendees") %>' ></asp:Label>&nbsp;&nbsp;</div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Instructor Name">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblCInstruction" runat="server" Text='<%#Bind("U_Z_InsName") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>                       
                                                   
                                <asp:TemplateField HeaderText="Application Start Date">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblCStDate" runat="server" Text='<%# Eval("U_Z_AppStdt") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Application End Date" >
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblCEndDate" runat="server" Text='<%# Eval("U_Z_AppEnddt") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>    
                                     <asp:TemplateField HeaderText="No.of Hours">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblChours" runat="server" Text='<%# Eval("U_Z_NoOfHours") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>   
                                 <asp:TemplateField HeaderText="Attendees Cost">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblCAttCost" runat="server" Text='<%# Eval("U_Z_AttCost") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>                       
                            <asp:TemplateField HeaderText="Additional Cost">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblCstarttime" runat="server" Text='<%# Eval("U_Z_AddionalCost") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Total Cost">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblCendtime" runat="server" Text='<%# Eval("U_Z_TotalCost") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>                                 
                              <asp:TemplateField HeaderText="Attendees Status">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblCStatus" runat="server" Text='<%# Eval("U_Z_AttendeesStatus") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>      
                                 <asp:TemplateField HeaderText="Closing Remarks">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblCRemarks" runat="server" Text='<%# Eval("U_Z_Remarks") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>      
                                 <asp:TemplateField HeaderText="Cost Posting Reference">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblCjeno" runat="server" Text='<%# Eval("JENO") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>                            
                                                                       
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" height="25px" BackColor="#CCCCCC"/>
                           </asp:GridView>
                           </div> 
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

