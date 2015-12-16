<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ESS/ESSMaster.Master" CodeBehind="ESSHome.aspx.vb" Inherits="HRMS.ESSHome" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link rel="stylesheet" href="../Styles/vThink_Theme.css" type="text/css" />
  <script type="text/javascript">
      function popupdisplay(option, uniqueid, tripno, CouName) {
          if (uniqueid.length > 0) {
              var uniid = document.getElementById("<%=txtpopunique.ClientID%>").value;
              var tno = document.getElementById("<%=txtpoptno.ClientID%>").value;
              var opt = document.getElementById("<%=txthidoption.ClientID%>").value;
              var tName = document.getElementById("<%=txthidoption.ClientID%>").value;
              uniid = ""; tno = ""; opt = ""; tName = "";
              if (uniid != uniqueid && tno != tripno && opt != option && tName != CouName) {
                  document.getElementById("<%=txtpopunique.ClientID%>").value = uniqueid;
                  document.getElementById("<%=txtpoptno.ClientID%>").value = tripno;
                  document.getElementById("<%=txthidoption.ClientID%>").value = option;
                  document.getElementById("<%=txttname.ClientID%>").value = CouName;
                  document.getElementById("<%=Btncallpop.ClientID%>").onclick();
              }
          }
      }


      function Confirmation() {
          if (confirm("Do you want to Confirm the Approval?") == true) {
              return true;
          }
          else {
              return false;
          }
      }
</script>


  <script type="text/javascript">
      function Confirmation() {
          if (confirm("Do you want to confirm the changes?") == true) {
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
<%-- <tr>

    <td height="30" align="left" colspan="2" valign="bottom">     
      <div >&nbsp;&nbsp;&nbsp;<asp:Label ID="Label3" runat="server" Text="Home"  CssClass="subheader" style="text-shadow: 0px 1px 0px #f17700;"></asp:Label>  <span ><asp:Label ID="lblNewTrip" runat="server" Text="" Visible="false"></asp:Label></span> </div>
      </td>    
 </tr>--%>
  <tr>
      
      <td align="center">
        <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
        <tr>
        <td>
          <asp:Panel ID="panelhome" runat="server" Width="100%"> 
            <asp:ImageButton ID="btnhome" runat="server" ImageUrl="~/images/Homeicon.jpg"  PostBackUrl="~/ESS/ESSHome.aspx"
                    ToolTip="Home" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:ImageButton ID="btnnew" runat="server" ImageUrl="~/images/Add.jpg" ToolTip="Add new record" visible="false"
                  /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        
            </asp:Panel> 
            <asp:Label ID="Label2" runat="server" Text="" style="color:Red;"></asp:Label>
              <asp:Panel ID="panelview" runat="server" Width="100%">
            <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
              <tr>
              <td valign="top">  
                <ajx:TabContainer ID="TabContainer2" runat="server" Width="100%" CssClass="ajax__tab_yuitabview-theme" ActiveTabIndex="0"  >
              
                     
                    <ajx:TabPanel  ID="TabPanel2" runat="server" HeaderText="Personal Details" >
                   <ContentTemplate>
                   
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
                   </ContentTemplate> 
                   </ajx:TabPanel> 
                   
                      <ajx:TabPanel  ID="TabPanel4" runat="server" HeaderText="Internel Job Postings" >
                          <HeaderTemplate>
                              Internal Job Postings
                          </HeaderTemplate>
                   <ContentTemplate>
                   <table width="100%" border="0" cellspacing="0" cellpadding="4" class="main_content">
 
  <tr>
      
      <td align="center">
        <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
        </table>
         <div id="Empdetails" runat="server" class="DivCorner" style="width:99%;">   
        <div id="Emp" runat="server" visible="False"> 
            <table  border="0" cellspacing="0" cellpadding="4" class="main_content" >
                 <tr >
                 <td width="15%">Employee Id</td>
                 <td>
                  <asp:TextBox ID="txtempid" CssClass="txtbox" Width="150px" ReadOnly="True" 
                         runat="server" ></asp:TextBox>
                 </td>
                  <td  width="10%"></td>
                  <td  width="15%">Employee Name</td>
                  <td width="15%">
                   <asp:TextBox ID="txtempname"  CssClass="txtbox" Width="150px" ReadOnly="True"  
                          runat="server"></asp:TextBox>        
                  </td>                
            </tr>
             <tr style="visibility:collapse;">
                 <td width="10%">Position Code</td>
                 <td>
                  <asp:TextBox ID="txtposid" CssClass="txtbox" Width="150px" ReadOnly="True" 
                         runat="server" ></asp:TextBox>
                 </td>
                  <td  width="15%"></td>
                  <td  width="10%">Position Name</td>
                  <td width="30%">
                   <asp:TextBox ID="txtposname"  CssClass="txtbox" Width="150px" ReadOnly="True" 
                          runat="server"></asp:TextBox>        
                  </td>                
            </tr>
             <tr style="visibility:collapse;">
                 <td width="10%">Department Id</td>
                 <td>
                  <asp:TextBox ID="TextBox1" CssClass="txtbox" Width="150px" ReadOnly="True" 
                         runat="server" ></asp:TextBox>
                 </td>
                  <td  width="15%"></td>
                    <td  width="12%">Department Name</td>
                  <td width="20%">
                   <asp:TextBox ID="txtdeptname"  CssClass="txtbox" Width="150px" ReadOnly="True" 
                          runat="server"></asp:TextBox>        
                  </td>                
            </tr> 
            </table> 
            </div>
             <table  border="0" cellspacing="0" cellpadding="4" class="main_content"> 
            <tr>
            <td>
            <a style="font-family:Verdana; font-size:11pt; font-weight:bold; text-decoration:underline">Vacant positions</a>
            </td>
            </tr>
             <tr>
                                     <td>
                                   <div style="overflow-x:auto;width:1100px;">
                                <asp:GridView ID="GridView2" runat="server" CellPadding="4" ShowHeaderWhenEmpty="True" 
                                           EmptyDataText="No records Found" CssClass="mGrid" 
                                           AutoGenerateSelectButton="True" AllowPaging="True" AutoGenerateColumns="False" 
                                           Width="100%" >
                                  <PagerStyle CssClass="pgr" />
                                  <RowStyle CssClass="mousecursor" />
                             <SelectedRowStyle BackColor="#FFA20C" />
                                  <AlternatingRowStyle CssClass="alt" />
                                  <Columns>
                                      <asp:TemplateField HeaderText="Rec.Request Code">
                                          <ItemTemplate>
                                              <div align="left">
                                                  &nbsp;<asp:Label ID="lblreqcode2" runat="server" Text='<%#Bind("DocEntry") %>' ></asp:Label>
                                              </div>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Department Code" Visible="False">
                                          <ItemTemplate>
                                              <div align="left">
                                                  &nbsp;<asp:Label ID="lblDeptcode" runat="server" Text='<%#Bind("U_Z_DeptCode") %>' ></asp:Label>
                                              </div>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Department">
                                          <ItemTemplate>
                                              <div align="left">
                                                  &nbsp;<asp:Label ID="lblDept" runat="server" Text='<%#Bind("U_Z_DeptName") %>' ></asp:Label>
                                              </div>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Position Code" Visible="False">
                                          <ItemTemplate>
                                              <div align="left">
                                                  &nbsp;<asp:Label ID="lblpositionCode" runat="server" Text='<%#Bind("U_Z_EmpPosi") %>' ></asp:Label>
                                              </div>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Position">
                                          <ItemTemplate>
                                              <div align="left">
                                                  &nbsp;<asp:Label ID="lblposition" runat="server" Text='<%#Bind("Position") %>' ></asp:Label>
                                              </div>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Min.Experience">
                                          <ItemTemplate>
                                              <div align="center">
                                                  &nbsp;<asp:Label ID="lblminexp" runat="server" Text='<%#Bind("U_Z_ExpMin") %>' ></asp:Label>
                                              </div>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Max.Experience">
                                          <ItemTemplate>
                                              <div align="left">
                                                  <asp:Label ID="lblmaxexp" runat="server" Text='<%#Bind("U_Z_ExpMax") %>' ></asp:Label>
                                                  &nbsp;&nbsp;</div>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Vacancy">
                                          <ItemTemplate>
                                              <div align="center">
                                                  &nbsp;<asp:Label ID="lblvacancy" runat="server" Text='<%#Bind("U_Z_Vacancy") %>' ></asp:Label>
                                              </div>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Employement Start Date">
                                          <ItemTemplate>
                                              <div align="left">
                                                  &nbsp;<asp:Label ID="lblstdate" runat="server" Text='<%#Bind("U_Z_EmpstDate") %>' ></asp:Label>
                                              </div>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Internal Application Deadline">
                                          <ItemTemplate>
                                              <div align="left">
                                                  <asp:Label ID="lblintdate" runat="server" Text='<%#Bind("U_Z_IntAppDead") %>' ></asp:Label>
                                                  &nbsp;&nbsp;</div>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="External Application Deadline">
                                          <ItemTemplate>
                                              <div align="left">
                                                  &nbsp;<asp:Label ID="lblextdate" runat="server" Text='<%#Bind("U_Z_ExtAppDead") %>' ></asp:Label>
                                              </div>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                  </Columns>
                            <HeaderStyle HorizontalAlign="Center" height="25px" BackColor="#CCCCCC" 
                                      CssClass="GridBG"/>
                           </asp:GridView>
                           </div>
                                     </td>
                                     </tr>  
                 
            </table>  
            </div>          
                           
        </td> 
        </tr> 
          <tr>
                <td align="right">
                <br />
                    <asp:Button ID="btnsubmit" CssClass="btn" Width="85px" runat="server" Text="Apply" OnClientClick="return Confirmation();"/>
                  
                    <asp:Button ID="btnclose" CssClass="btn" Width="85px" runat="server" Text="Cancel" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   
                </td>
            </tr>

                
           
 </table> 
                   </ContentTemplate> 
                   </ajx:TabPanel> 
                     <ajx:TabPanel  ID="TabPanel6" runat="server" HeaderText="Applied Positions" >
                    <ContentTemplate>
                     <table width="99%" border="0" cellspacing="0" cellpadding="3" class="main_content">                                   
                                   <tr>
                                     <td>
                                       <div style="overflow-x:auto;">
                               <asp:GridView ID="grdScheduled" runat="server" CellPadding="4" CssClass="mGrid" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found" PagerStyle-CssClass="pgr" HeaderStyle-Cssclass="GridBG"  AlternatingRowStyle-CssClass="alt" AllowPaging="True" AutoGenerateColumns="False" Width="100%" PageSize="10" >
                            <Columns>
                               <asp:TemplateField HeaderText="Requisition Code">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblreqcode" runat="server" Text='<%#Bind("U_RequestCode") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField> 
                                                      
                                 <asp:TemplateField HeaderText="Employee Code">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblcouCode" runat="server" Text='<%#Bind("U_Empid") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee Name">
                                    <ItemTemplate>
                                       <div align="left">&nbsp;<asp:Label ID="lblempName" runat="server" Text='<%#Bind("U_Empname") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Department">
                                    <ItemTemplate>
                                        <div align="center">&nbsp;<asp:Label ID="lbldeptname" runat="server" Text='<%#Bind("U_ReqdeptName") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>    
                                 <asp:TemplateField HeaderText="Position">
                                    <ItemTemplate>
                                        <div align="left"><asp:Label ID="lblposname" runat="server" Text='<%#Bind("U_EmpPosName") %>' ></asp:Label>&nbsp;&nbsp;</div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>  
                                 <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <div align="center">&nbsp;<asp:Label ID="lblstatus3" runat="server" Text='<%#Bind("U_Z_AppStatus") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>    
                                 <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblReason" runat="server" Text='<%#Bind("U_Remarks") %>' ></asp:Label></div>
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
                  <ajx:TabPanel  ID="TabPanel3" runat="server" HeaderText="Appraisal Notification" >
                   <ContentTemplate>
                      <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content">                                   
                                   <tr>
                                     <td style="padding-top:10px;">
                               <asp:GridView ID="grdRequestApp" runat="server" CellPadding="4" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found"  CssClass="mGrid" HeaderStyle-Cssclass="GridBG" AllowPaging="True"  AutoGenerateColumns="False" Width="100%" PageSize="10">
                            <Columns>
                                <asp:TemplateField HeaderText="Appraisal No.">
                                    <ItemTemplate>
                                        <div align="center"><asp:label ID="lbtndocnum" runat="server" Text='<%#Bind("DocEntry") %>' ></asp:label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Request Date">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblcust" runat="server" Text='<%#Bind("U_Z_Date") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>                                
                                 <asp:TemplateField HeaderText="EmployeeId">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblcustName" runat="server" Text='<%#Bind("U_Z_EmpId") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>                                
                                 <asp:TemplateField HeaderText="Employee Name">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblitem" runat="server" Text='<%#Bind("U_Z_EmpName") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Period" >
                                    <ItemTemplate>
                                       <div align="left">&nbsp;<asp:Label ID="lblitemdesc" runat="server" Text='<%#Bind("U_Z_Period") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Active">
                                    <ItemTemplate>
                                        <div align="center">&nbsp;<asp:Label ID="lblserial" runat="server" Text='<%#Bind("U_Z_Status") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>      
                                   <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <div align="center">&nbsp;<asp:Label ID="lblstatus" runat="server" Text='<%#Bind("U_Z_WStatus") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField> 
                                 <asp:TemplateField HeaderText="Grievance No" visible="false">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblgrvno" runat="server" Text='<%#Bind("U_Z_GNo") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Grievance Remarks" visible="false">
                                    <ItemTemplate>
                                       <div align="left">&nbsp;<asp:Label ID="lblgrvremarks" runat="server" Text='<%#Bind("U_Z_GRemarks") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Grievance date" visible="false">
                                    <ItemTemplate>
                                        <div align="center">&nbsp;<asp:Label ID="lblgrvdate" runat="server" Text='<%#Bind("U_Z_GDate") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>      
                                   <asp:TemplateField HeaderText="Grievance Status" visible="false">
                                    <ItemTemplate>
                                        <div align="center">&nbsp;<asp:Label ID="lblgrvstatus" runat="server" Text='<%#Bind("U_Z_GStatus") %>' ></asp:Label></div>
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
                                    <ajx:TabPanel  ID="TabPanel1" runat="server" HeaderText="Training Agenda Notification" >
                   <ContentTemplate>
                      <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content">                                   
                                   <tr>
                                     <td colspan="4">
                                    <%--<div style="overflow-x:auto;width:1000px;">--%>
                              <asp:GridView ID="GridView1" runat="server" CellPadding="4" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found" HeaderStyle-Cssclass="GridBG" CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" AllowPaging="True" RowStyle-CssClass="mousecursor" AutoGenerateColumns="False" Width="100%" PageSize="10" >
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
                                <asp:TemplateField HeaderText="Course Name">
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
                                  <asp:TemplateField HeaderText="Attendees Cost" Visible="false">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblTAttCost" runat="server" Text='<%# Eval("U_Z_AttCost") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Sunday" Visible="false">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblsunday" runat="server" Text='<%#Bind("U_Z_Sunday") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>                       
                                                   
                                <asp:TemplateField HeaderText="Monday" Visible="false">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblMonday" runat="server" Text='<%# Eval("U_Z_Monday") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Tuesday" Visible="false">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblTuesday" runat="server" Text='<%# Eval("U_Z_Tuesday") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>    
                                     <asp:TemplateField HeaderText="Wednesday" Visible="false">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblWednesday" runat="server" Text='<%# Eval("U_Z_Wednesday") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>                          
                            <asp:TemplateField HeaderText="Thursday" Visible="false">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblThursday" runat="server" Text='<%# Eval("U_Z_Thursday") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Friday" Visible="false">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblFriday" runat="server" Text='<%# Eval("U_Z_Friday") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Saturday" Visible="false">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblSaturday" runat="server" Text='<%# Eval("U_Z_Saturday") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>   
                                   <asp:TemplateField HeaderText="Active" Visible="false">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblactive" runat="server" Text='<%# Eval("U_Z_Active") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>                                           
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" height="25px" BackColor="#CCCCCC"/>
                           </asp:GridView>
                         <%--  </div>--%>
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
 </table> 
</ContentTemplate> 
</asp:UpdatePanel> 
</asp:Content>
