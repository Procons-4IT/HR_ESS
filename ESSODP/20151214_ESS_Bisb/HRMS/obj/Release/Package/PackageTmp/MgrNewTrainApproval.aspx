<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/HRMS.Master" CodeBehind="MgrNewTrainApproval.aspx.vb" Inherits="HRMS.MgrNewTrainApproval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script language="javascript" type="text/javascript">
    function ShowOptions(control, args) {
        control._completionListElement.style.zIndex = 10000001;
    }
</script> 
    <script type="text/javascript">
       
        function popupdisplay(option, uniqueid, tripno) {
            if (uniqueid.length > 0) {
                var uniid = document.getElementById("<%=txtpopunique.ClientID%>").value;
                var tno = document.getElementById("<%=txtpoptno.ClientID%>").value;
                var opt = document.getElementById("<%=txthidoption.ClientID%>").value;
                uniid = ""; tno = ""; opt = "";
                if (uniid != uniqueid && tno != tripno && opt != option) {
                    document.getElementById("<%=txtpopunique.ClientID%>").value = uniqueid;
                    document.getElementById("<%=txtpoptno.ClientID%>").value = tripno;
                    document.getElementById("<%=txthidoption.ClientID%>").value = option;
                    document.getElementById("<%=Btncallpop.ClientID%>").onclick();
                }
            }
        }

        function Confirmation() {
            if (confirm("Do you want to confirm the Approval?") == true) {
                return true;
            }
            else {
                return false;
            }
        }
</script>

 
    <script src="JavaScript/jquery-1.4.1.min.js" type="text/javascript"></script>


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
<asp:Image ID="Image1" ImageUrl="Images/waiting.gif" AlternateText="Processing" runat="server" />
</ProgressTemplate>
</asp:UpdateProgress>
<ajx:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress" PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />


<asp:UpdatePanel ID="Update" runat="server">
            <ContentTemplate>
 <table width="100%" border="0" cellspacing="0" cellpadding="4" class="main_content">
 <tr>
 <td>
     <asp:TextBox ID="txtHEmpID" runat="server" Width="93px"  Style="display: none"></asp:TextBox>
   <asp:TextBox ID="txtpopunique" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="txtpoptno" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="txthidoption" runat="server" Style="display: none"></asp:TextBox>
     <asp:TextBox ID="txttname" runat="server" Style="display: none"></asp:TextBox>
    <input id="Btncallpop" runat="server" onserverclick="Btncallpop_ServerClick" style="display: none" type="button" value="button" />

 </td>
 </tr>
  <tr>
      
      <td align="center">
        <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
         <tr>

    <td height="30" align="left" colspan="2" valign="bottom" background="images/h_bg.png"; style="border-bottom:1px dotted; border-color: #f45501; background-repeat:repeat-x">     
      <div >&nbsp; <asp:Label ID="Label3" runat="server" Text="Manager New Training Approval"  CssClass="subheader" style="float:left;" ></asp:Label> 
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
                 <td width="10%">Department</td>
                 <td>
                   <asp:TextBox ID="txtdeptname" CssClass="txtbox" Width="190px" runat="server" Enabled="False" ></asp:TextBox>
                    <asp:TextBox ID="txtdeptcode" CssClass="txtbox" Width="190px" runat="server" visible="False" ></asp:TextBox>
                   <asp:ImageButton ID="btnfindAgenda" runat="server" Text="Find" ImageUrl="~/images/search.jpg" />
                  <ajx:ModalPopupExtender ID="ModalPopupExtender7" runat="server" DropShadow="True" PopupControlID="Panelpoptechnician" TargetControlID="btnfindAgenda" CancelControlID="btnclstech" BackgroundCssClass="modalBackground">
                    </ajx:ModalPopupExtender>  
              </td>                     
            </tr>
          
                  <tr>
                 <td width="10%"></td>
                 <td>
                     <asp:Button ID="btnGetDetails" runat="server" Text="Get Details"  CssClass="btn" Width="120px" />
                 </td>                     
            </tr>
            </table>  
            </div>  
            </asp:Panel> 
        <asp:Panel ID="paltab" runat="server" Width="98%" BorderColor="LightSteelBlue"  BorderWidth="2">
            <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
              <tr>
              <td valign="top">  
                <ajx:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0"  CssClass="ajax__tab_yuitabview-theme" Width="100%" >
                  <ajx:TabPanel  ID="TabPanel1" runat="server" HeaderText="New Training Request" >
                   <ContentTemplate>
                    
                      <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content">                                   
                                   <tr>
                                     <td>
                                <asp:GridView ID="grdNewTraining" runat="server" CellPadding="4"  AllowPaging="True" CssClass="mGrid" HeaderStyle-Cssclass="GridBG" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found"
    PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" RowStyle-CssClass="mousecursor" AutoGenerateColumns="False" Width="100%" PageSize="10" >
                             <SelectedRowStyle BackColor="#FFA20C" />
                            <Columns>
                              <asp:TemplateField HeaderText="Request No">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblReqno" runat="server" Text='<%#Bind("DocEntry") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField> 
                                 <asp:TemplateField HeaderText="Request Date">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblReqdt" runat="server" Text='<%#Bind("U_Z_ReqDate") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField> 
                                                           
                                 <asp:TemplateField HeaderText="Employee Code">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblempcode" runat="server" Text='<%#Bind("U_Z_HREmpID") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Employee Name">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblempname" runat="server" Text='<%#Bind("U_Z_HREmpName") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>   
                                   <asp:TemplateField HeaderText="Course">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblcouName" runat="server" Text='<%#Bind("U_Z_CourseName") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>                               
                                 <asp:TemplateField HeaderText="Department">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lbldept" runat="server" Text='<%#Bind("U_Z_DeptName") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Position">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblPos" runat="server" Text='<%#Bind("U_Z_PosiName") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Request Status">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblReqstatus" runat="server" Text='<%#Bind("U_Z_ReqStatus") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                
                                 <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                    <asp:DropDownList ID="ddlstatus" runat="server"  SelectedValue='<%# Bind("U_Z_MgrStatus") %>'  Width="100">
                                     <asp:ListItem Value="MA">Manager Approved</asp:ListItem>
                                      <asp:ListItem Value="MR">Manager Rejected</asp:ListItem>
                                        <asp:ListItem Value="P">Pending</asp:ListItem>
                                </asp:DropDownList>      
                                </ItemTemplate>                                    
                                </asp:TemplateField>  
                                 <asp:TemplateField HeaderText="Manager Remarks">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:TextBox ID="lblMgrremarks" runat="server" Text='<%#Bind("U_Z_MgrRemarks") %>' ></asp:TextBox></div>
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
                  
                     <ajx:TabPanel  ID="TabPanel2" runat="server" HeaderText="Training Request Summery" >
                    <ContentTemplate>
                     <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content">                                   
                                   <tr>
                                     <td>
                                <asp:GridView ID="grdrNewSummary" runat="server" CellPadding="4"  AllowPaging="True" CssClass="mGrid"  HeaderStyle-Cssclass="GridBG" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found"
    PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" RowStyle-CssClass="mousecursor" AutoGenerateColumns="False" Width="100%" PageSize="10" >
                             <SelectedRowStyle BackColor="#FFA20C" />
                            <Columns>
                              <asp:TemplateField HeaderText="Request No">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblReqno1" runat="server" Text='<%#Bind("DocEntry") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField> 
                                 <asp:TemplateField HeaderText="Request Date">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblReqdate1" runat="server" Text='<%#Bind("U_Z_ReqDate") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField> 
                                                           
                                 <asp:TemplateField HeaderText="Employee Code">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblempcode1" runat="server" Text='<%#Bind("U_Z_HREmpID") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Employee Name">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblempname1" runat="server" Text='<%#Bind("U_Z_HREmpName") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>   
                                   <asp:TemplateField HeaderText="Course">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblcouName1" runat="server" Text='<%#Bind("U_Z_CourseName") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>                               
                                 <asp:TemplateField HeaderText="Department">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lbldept1" runat="server" Text='<%#Bind("U_Z_DeptName") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Position">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblPos1" runat="server" Text='<%#Bind("U_Z_PosiName") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Request Status">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblReqstatus1" runat="server" Text='<%#Bind("U_Z_ReqStatus") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                
                                 <asp:TemplateField HeaderText="Manager Status">
                                    <ItemTemplate>
                                   <div align="left">&nbsp;<asp:Label ID="lblmgrstatus1" runat="server" Text='<%#Bind("U_Z_MgrStatus") %>' ></asp:Label></div>

                                </ItemTemplate>                                    
                                </asp:TemplateField>  
                                 <asp:TemplateField HeaderText="Manager Remarks">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:label ID="lblMgrremarks1" runat="server" Text='<%#Bind("U_Z_MgrRemarks") %>' ></asp:label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="HR Status">
                                    <ItemTemplate>
                                   <div align="left">&nbsp;<asp:Label ID="lblHRstatus1" runat="server" Text='<%#Bind("U_Z_HrStatus") %>' ></asp:Label></div>

                                </ItemTemplate>                                    
                                </asp:TemplateField>  
                                 <asp:TemplateField HeaderText="HR Remarks">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:label ID="lblHRremarks1" runat="server" Text='<%#Bind("U_Z_HrRemarks") %>' ></asp:label></div>
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

                    
                 </ajx:TabContainer>   
              
              </td> 
              </tr> 
              </table> 
          </asp:Panel>
                             
        </td> 
        </tr> 
          <tr>
                <td colspan="5" align="left">
                <br />
                    <asp:Button ID="btnsubmit"  CssClass="btn" runat="server" Width="85px" Text="Save" OnClientClick="return Confirmation();" />
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnclose"  CssClass="btn"  runat="server" Text="Cancel" Width="85px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     
                </td>
            </tr>
                          <tr>
                          <td>
                            <asp:Panel ID="Panelpoptechnician" runat="server" BackColor="White" style=" display:none; padding:10px; width:500px; ">
                                <div><span class="sideheading" style="color:Green;">Department Details</span> <span style="float:right;"> 
                                <asp:Button ID="btnclstech" runat="server"  CssClass="btn" Width="30px" Text="X" /></span></div>
                                   <br />
                              <div><span><asp:Label ID="lblname" runat="server" Text="Department Name"></asp:Label></span>
                              <asp:TextBox ID="txtpopdeptname" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                               
                              <asp:Button ID="btnpopemp" runat="server" Text="Go"  CssClass="btn" Width="65px" />  <asp:LinkButton runat="server" id="LnkViewall">View All</asp:LinkButton>
                               <br />
                              <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                              </div>
                              <br /><asp:Panel ID="Panel4" runat="server" Height="200px" ScrollBars="Vertical">
                                  <asp:GridView ID="grdtechnician" runat="server" CellPadding="4" RowStyle-CssClass="mousecursor" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found" CssClass="mGrid" HeaderStyle-Cssclass="GridBG"  
                                        PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt"
                                        AutoGenerateColumns="false" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Department Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldptcode" runat="server" Text='<%#Bind("Code") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldeptname" runat="server" Text='<%#Bind("Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                                                          
                                    </Columns>
                                    <HeaderStyle BackColor="Gray" HorizontalAlign="Center" ForeColor="White" Height="25px" />
                                      <RowStyle HorizontalAlign="Center" />
                                    <AlternatingRowStyle HorizontalAlign="Center" />
                                  </asp:GridView></asp:Panel>
                            </asp:Panel>
            
                          </td>
                          </tr>               
                          
          
           
 </table> 

</ContentTemplate> 
</asp:UpdatePanel> 

</asp:Content>
