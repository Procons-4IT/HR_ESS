<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/HRMS.Master" CodeBehind="HRandAdmin.aspx.vb" Inherits="HRMS.HRandAdmin" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
<asp:UpdatePanel ID="Update" runat="server">
<ContentTemplate>
<table width="100%" border="0" cellspacing="0" cellpadding="4" class="main_content">
 <tr>

    <td height="30" align="left" colspan="2" valign="bottom"  background="images/h_bg.png" style="border-bottom:1px dotted; border-color: #f45501; background-repeat:repeat-x">     
      <div >&nbsp; <asp:Label ID="Label3" runat="server" Text="HR Notifications"  CssClass="subheader" style="float:left;" ></asp:Label>  <span ><asp:Label ID="lblNewTrip" runat="server" Text="" Visible="false"></asp:Label></span> </div>
      </td>    
 </tr>
  <tr>
      
      <td align="center">
        <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
        <tr>
        <td>
           <asp:Label ID="Label2" runat="server" Text="" style="color:Red;"></asp:Label>
              <asp:Panel ID="panelview" runat="server" Width="100%" BorderColor="LightSteelBlue"  BorderWidth="2">
            <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
              <tr>
              <td valign="top">  
                <ajx:TabContainer ID="TabContainer2" runat="server" ActiveTabIndex="0"  CssClass="ajax__tab_yuitabview-theme" Width="100%" >
                     <ajx:TabPanel  ID="TabPanel3" runat="server" HeaderText="HR Acceptance" >
                   <ContentTemplate>
                    <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content">                                   
                                   <tr>
                                       <td>
                               <asp:GridView ID="grdRequestApp" runat="server" CellPadding="4" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found" AllowPaging="True" CssClass="mGrid" HeaderStyle-Cssclass="GridBG" 
    PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" Width="100%" PageSize="20" >
                            <Columns>
                                <asp:TemplateField HeaderText="Appraisal No.">
                                    <ItemTemplate>
                                        <div align="center"><asp:Label ID="lbtndocnum" runat="server" Text='<%#Bind("DocEntry") %>' ></asp:Label></div>
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
                                <asp:TemplateField HeaderText="Active" Visible="false">
                                    <ItemTemplate>
                                        <div align="center">&nbsp;<asp:Label ID="lblserial" runat="server" Text='<%#Bind("U_Z_Status") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>      
                                   <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <div align="center">&nbsp;<asp:Label ID="lblstatus2" runat="server" Text='<%#Bind("U_Z_WStatus") %>' ></asp:Label></div>
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
                                    <ajx:TabPanel  ID="TabPanel2" runat="server" HeaderText="HR Grievance Acceptance" >
                    <ContentTemplate>
                     <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content">                                   
                                   <tr>
                                     <td>
                               <asp:GridView ID="grdHRGrevAcc" runat="server" CellPadding="4" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found" AllowPaging="True" HeaderStyle-Cssclass="GridBG" AutoGenerateColumns="False" Width="100%" PageSize="20" CssClass="mGrid"  
    PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt">
                            <Columns>
                                <asp:TemplateField HeaderText="Appraisal No.">
                                    <ItemTemplate>
                                        <div align="center"><asp:Label ID="lbtndocnum1" runat="server" Text='<%#Bind("DocEntry") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Request Date">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblcust1" runat="server" Text='<%#Bind("U_Z_Date") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>                                
                                 <asp:TemplateField HeaderText="EmployeeId">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblcustName1" runat="server" Text='<%#Bind("U_Z_EmpId") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>                                
                                 <asp:TemplateField HeaderText="Employee Name">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblitem1" runat="server" Text='<%#Bind("U_Z_EmpName") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Period" >
                                    <ItemTemplate>
                                       <div align="left">&nbsp;<asp:Label ID="lblitemdesc1" runat="server" Text='<%#Bind("U_Z_Period") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="From Date" Visible="false">
                                    <ItemTemplate>
                                       <div align="left">&nbsp;<asp:Label ID="lblfromdt1" runat="server" Text='<%#Bind("U_Z_FDate") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="To Date" Visible="false" >
                                    <ItemTemplate>
                                       <div align="left">&nbsp;<asp:Label ID="lblTodate1" runat="server" Text='<%#Bind("U_Z_TDate") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Active" Visible="false">
                                    <ItemTemplate>
                                        <div align="center">&nbsp;<asp:Label ID="lblserial1" runat="server" Text='<%#Bind("U_Z_Status") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>      
                                   <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <div align="center">&nbsp;<asp:Label ID="lblstatus1" runat="server" Text='<%#Bind("U_Z_WStatus") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>   
                                   
                                   <asp:TemplateField HeaderText="Department" visible="false" >
                                    <ItemTemplate>
                                        <div align="center">&nbsp;<asp:Label ID="lbldept1" runat="server" Text='<%#Bind("Department") %>' ></asp:Label></div>
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
                      <ajx:TabPanel  ID="TabPanel1" runat="server" HeaderText="New Training Request" >
                   <ContentTemplate>
                    
                      <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content">                                   
                                   <tr>
                                      <td>
                                <asp:GridView ID="grdNewTraining" runat="server" CellPadding="4" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found" HeaderStyle-Cssclass="GridBG"  AllowPaging="True" CssClass="mGrid"  
    PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" RowStyle-CssClass="mousecursor" AutoGenerateColumns="False" Width="100%" PageSize="20" >
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
                                                           
                                 <asp:TemplateField HeaderText="Emp.Id">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblempcode" runat="server" Text='<%#Bind("U_Z_HREmpID") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Emp.Name">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblempname" runat="server" Text='<%#Bind("U_Z_HREmpName") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>   
                                   <asp:TemplateField HeaderText="Course Name">
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
                                  <asp:TemplateField HeaderText="Approval Status">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblReqstatus" runat="server" Text='<%#Bind("U_Z_ReqStatus") %>' ></asp:Label></div>
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
                    <ajx:TabPanel  ID="TabPanel7" runat="server" HeaderText="Requisition Approval" >
                    <ContentTemplate>
                     <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content">                                   
                                   <tr>
                                     <td>
                                      <div style="overflow-x:auto;">
                               <asp:GridView ID="grdRecRequest" runat="server" CellPadding="4" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found" HeaderStyle-Cssclass="GridBG" AllowPaging="True" CssClass="mGrid"  
                                 PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" PageSize="20" >
                            <Columns>
                                <asp:TemplateField HeaderText="Requisition No">
                                    <ItemTemplate>
                                        <div align="center"><asp:Label ID="lbtreqno" runat="server" Text='<%#Bind("DocEntry") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Requisition Date">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblreqdate" runat="server" Text='<%#Bind("U_Z_ReqDate") %>' Width="80" ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>                                
                                 <asp:TemplateField HeaderText="Requester Code">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblreqcode" runat="server" Text='<%#Bind("U_Z_EmpCode") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>                                
                                 <asp:TemplateField HeaderText="Requester Name">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblreqname" runat="server" Text='<%#Bind("U_Z_EmpName") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Department" >
                                    <ItemTemplate>
                                       <div align="left">&nbsp;<asp:Label ID="lbldept2" runat="server" Text='<%#Bind("U_Z_DeptName") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Position">
                                    <ItemTemplate>
                                        <div align="center">&nbsp;<asp:Label ID="lblposition" runat="server" Text='<%#Bind("U_Z_PosName") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>      
                                   <asp:TemplateField HeaderText="Min.Experience">
                                    <ItemTemplate>
                                        <div align="center">&nbsp;<asp:Label ID="lblminexp" runat="server" Text='<%#Bind("U_Z_ExpMin") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>       
                                 <asp:TemplateField HeaderText="Max.Experience">
                                    <ItemTemplate>
                                        <div align="center">&nbsp;<asp:Label ID="lblmaxexp" runat="server" Text='<%#Bind("U_Z_ExpMax") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>  
                                 <asp:TemplateField HeaderText="Vacant Positions">
                                    <ItemTemplate>
                                        <div align="center">&nbsp;<asp:Label ID="lblvacpos" runat="server" Text='<%#Bind("U_Z_Vacancy") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>  
                                 <asp:TemplateField HeaderText="Requester Status">
                                    <ItemTemplate>
                                        <div align="center">&nbsp;<asp:Label ID="lblmgrstatus1" runat="server" Text='<%#Bind("U_Z_MgrStatus") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField> 
                                
                                   <asp:TemplateField HeaderText="PosCode" visible="false">
                                    <ItemTemplate>
                                        <div align="center">&nbsp;<asp:Label ID="lblposcode" runat="server" Text='<%#Bind("U_Z_EmpPosi") %>' ></asp:Label></div>
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
                    
                        <ajx:TabPanel  ID="TabPanel4" runat="server" HeaderText="Final Candidate Approval" >
                    <ContentTemplate>
                     <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content">                                   
                                   <tr>
                                     <td>
                                <asp:GridView ID="grdfinalapp" runat="server" CellPadding="4" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found" AllowPaging="True" CssClass="mGrid" HeaderStyle-Cssclass="GridBG" 
                                 PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" PageSize="20" >
                            <Columns>
                                <asp:TemplateField HeaderText="App/ID">
                                    <ItemTemplate>
                                        <div align="center"><asp:Label ID="lbtAppID" runat="server" Text='<%#Bind("U_Z_HRAppID") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="App/Name">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblAppName" runat="server" Text='<%#Bind("U_Z_HRAppName") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>                                
                                 <asp:TemplateField HeaderText="Department Name" visible="false">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lbldep" runat="server" Text='<%#Bind("U_Z_DeptName") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>                                
                                 <asp:TemplateField HeaderText="Position Name" visible="false">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblposname" runat="server" Text='<%#Bind("U_Z_JobPosi") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Req No" >
                                    <ItemTemplate>
                                       <div align="left">&nbsp;<asp:Label ID="lblreqno" runat="server" Text='<%#Bind("U_Z_ReqNo") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                  
                                   <asp:TemplateField HeaderText="EMail">
                                    <ItemTemplate>
                                        <div align="center">&nbsp;<asp:Label ID="lblemail" runat="server" Text='<%#Bind("U_Z_Email") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>       
                                 <asp:TemplateField HeaderText="Mobile" visible="false">
                                    <ItemTemplate>
                                        <div align="center">&nbsp;<asp:Label ID="lblmobile" runat="server" Text='<%#Bind("U_Z_Mobile") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>  
                                 <asp:TemplateField HeaderText="App Status" visible="false">
                                    <ItemTemplate>
                                        <div align="center">&nbsp;<asp:Label ID="lblAppstus" runat="server" Text='<%#Bind("U_Z_ApplStatus") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>  
                                 <asp:TemplateField HeaderText="YOE">
                                    <ItemTemplate>
                                        <div align="center">&nbsp;<asp:Label ID="lblYOE" runat="server" Text='<%#Bind("U_Z_YrExp") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField> 
                                 <asp:TemplateField HeaderText="Interview Summary Status">
                                    <ItemTemplate>
                                        <div align="center">&nbsp;<asp:Label ID="lblinvstatus" runat="server" Text='<%#Bind("U_Z_IPHODSta") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>   
                                 <asp:TemplateField HeaderText="Approval Status">
                                    <ItemTemplate>
                                        <div align="center">&nbsp;<asp:Label ID="lblreqstatus" runat="server" Text='<%#Bind("U_Z_AppStatus") %>' ></asp:Label></div>
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
        </table>         
        </td> 
        </tr> 
 </table> 
</ContentTemplate> 
</asp:UpdatePanel> 
</asp:Content>
