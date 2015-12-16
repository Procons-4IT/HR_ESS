<%@ Page Title="LeaveRequest Approval" Language="vb" AutoEventWireup="false" MasterPageFile="~/HRMS.Master" CodeBehind="LeaveRequestApproval.aspx.vb" Inherits="HRMS.LeaveRequestApproval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <script type="text/javascript">

      function Confirmation() {
          if (confirm("Are you sure want to submit the document?") == true) {
              return true;
          }
          else {
              return false;
          }
      }
   </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdateProgress ID="UpdateProgress" runat="server">
<ProgressTemplate>
<asp:Image ID="Image1" ImageUrl="~/Images/waiting.gif" AlternateText="Processing" runat="server" />
</ProgressTemplate>
</asp:UpdateProgress>
<ajx:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress" PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
<asp:UpdatePanel ID="Update" runat="server">
<ContentTemplate>

<table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
 <tr>

    <td height="30" align="left" colspan="2" valign="bottom" background="images/h_bg.png"; style="border-bottom:1px dotted; border-color: #f45501; background-repeat:repeat-x">     
      <div >&nbsp; <asp:Label ID="Label3" runat="server" Text="Leave Request Approval"  CssClass="subheader" style="float:left;" ></asp:Label>  <span ><asp:Label ID="lblNewTrip" runat="server" Text="" Visible="false"></asp:Label></span> </div>
      </td>    
 </tr>

 <tr>      
      <td align="center">
        <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
        <tr>
        <td>
          <asp:Panel ID="panelhome" runat="server" Width="100%"> 
            <asp:ImageButton ID="btnhome" runat="server" ImageUrl="~/images/Homeicon.jpg"  PostBackUrl="~/Home.aspx"
                    ToolTip="Home" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           
        
            </asp:Panel> 
            <asp:Label ID="Label2" runat="server" Text="" style="color:Red;"></asp:Label>
              <asp:Panel ID="panelview" runat="server" Width="100%" BorderColor="LightSteelBlue"  BorderWidth="2">
            <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
              <tr>
              <td valign="top">  
                <ajx:TabContainer ID="TabContainer2" runat="server" ActiveTabIndex="0"  CssClass="ajax__tab_yuitabview-theme" Width="100%" >
                  <ajx:TabPanel  ID="TabPanel3" runat="server" HeaderText="Leave Request Approval" ><ContentTemplate>
                          <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content"><tr>
                                  <td>
                                  <div style="overflow-x:scroll; width:1100px;">
                                  <asp:GridView ID="grdRequestApproval" runat="server" CellPadding="4" AllowPaging="True" ShowHeaderWhenEmpty="true" EmptyDataText = "No Records Found"  CssClass="mGrid" HeaderStyle-Cssclass="GridBG" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false" Width="100%" PageSize="10" ><Columns>
                                              <asp:TemplateField HeaderText="Request Code" >
                                                  <ItemTemplate><div align="center">
                                                      <asp:LinkButton ID="lblCode" runat="server" Text='<%#Bind("Code") %>' OnClick="lbtndocnum_Click">
                                                  </asp:LinkButton></div></ItemTemplate></asp:TemplateField>
                                              <asp:TemplateField HeaderText="T&A Emp.ID">
                                                  <ItemTemplate><div align="center">
                                                      <asp:label ID="lbltaEmpid" runat="server" Text='<%#Bind("TAEmpID") %>'  ></asp:label></div></ItemTemplate></asp:TemplateField>
                                              <asp:TemplateField HeaderText="Emp.Code">
                                                  <ItemTemplate><div align="center">
                                                      <asp:label ID="lblEmpid" runat="server" Text='<%#Bind("U_Z_EMPID") %>'  ></asp:label></div></ItemTemplate></asp:TemplateField>
                                              <asp:TemplateField HeaderText="Emp.Name">
                                                  <ItemTemplate><div align="center">
                                                      <asp:label ID="lblEmpname" runat="server" Text='<%#Bind("U_Z_EMPNAME") %>'  ></asp:label></div></ItemTemplate></asp:TemplateField>
                                              <asp:TemplateField HeaderText="Leave Name">
                                                  <ItemTemplate><div align="left">&#160;<asp:Label ID="lbllveName" runat="server" Text='<%#Bind("U_Z_LeaveName") %>' Width="130px"></asp:Label></div></ItemTemplate></asp:TemplateField>
                                              <asp:TemplateField HeaderText="Leave Type" Visible="false"><ItemTemplate><div align="left">&#160;<asp:Label ID="lblAgenda" runat="server" Text='<%#Bind("U_Z_TrnsCode") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField>
                                              <asp:TemplateField HeaderText="From Date">
                                                  <ItemTemplate><div align="left">&#160;<asp:Label ID="lblCouCode" runat="server" Text='<%#Bind("U_Z_StartDate") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField>
                                              <asp:TemplateField HeaderText="To Date">
                                                  <ItemTemplate><div align="left">&#160;<asp:Label ID="lblCouName" runat="server" Text='<%#Bind("U_Z_EndDate") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField>
                                              <asp:TemplateField HeaderText="No.of Days">
                                                  <ItemTemplate><div align="left">&#160;<asp:Label ID="lblCouType" runat="server" Text='<%#Bind("U_Z_NoofDays") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField>
                                              <asp:TemplateField HeaderText="Remarks">
                                                  <ItemTemplate><div align="left">&#160;<asp:Label ID="lblstdate" runat="server" Text='<%#Bind("U_Z_Notes") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField>
                                              <asp:TemplateField HeaderText="Payroll Month">
                                                  <ItemTemplate><div align="left">&#160;<asp:Label ID="lblpaymonth" runat="server" Text='<%#Bind("U_Z_Month") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField>
                                              <asp:TemplateField HeaderText="Payroll Year">
                                                  <ItemTemplate><div align="left">&#160;<asp:Label ID="lblpayyear" runat="server" Text='<%#Bind("U_Z_Year") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField>
                                              <asp:TemplateField HeaderText="Approval Status">
                                                  <ItemTemplate><div align="left">&#160;<asp:Label ID="lblAppStatus" runat="server" Text='<%#Bind("U_Z_Status") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField>
                                              <asp:TemplateField HeaderText="Approval Required" Visible="false">
                                                  <ItemTemplate><div align="left">&#160;<asp:Label ID="lblAppreq" runat="server" Text='<%#Bind("U_Z_AppRequired") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField>
                                              <asp:TemplateField HeaderText="Requested Date">
                                                  <ItemTemplate><div align="left">&#160;<asp:Label ID="lblreqdt" runat="server" Text='<%#Bind("U_Z_AppReqDate") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField>
                                              <asp:TemplateField HeaderText="Requested Time" Visible="false">
                                                  <ItemTemplate><div align="left">&#160;<asp:Label ID="lblreqtime" runat="server" Text='<%#Bind("U_Z_ReqTime") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField>
                                              <asp:TemplateField HeaderText="Current Approver">
                                                  <ItemTemplate><div align="left">&#160;<asp:Label ID="lblcurapp" runat="server" Text='<%#Bind("U_Z_CurApprover") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField>
                                              <asp:TemplateField HeaderText="Next Approver">
                                                  <ItemTemplate><div align="left">&#160;<asp:Label ID="lblnxtapp" runat="server" Text='<%#Bind("U_Z_NxtApprover") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField>
                                              <asp:TemplateField HeaderText="LeaveBalance">
                                                  <ItemTemplate>
                                                  <div align="left">&nbsp;<asp:LinkButton ID="lbtnactivity" runat="server" Text="View" onclick="lbtnactivity_Click" ></asp:LinkButton></div>
                                                  </ItemTemplate>
                                                  </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Approved Leave Transactions">
                                                       <ItemTemplate>
                                                  <div align="left">&nbsp;<asp:LinkButton ID="lbtnHistory" runat="server" Text="View" onclick="lbtnHistory_Click"></asp:LinkButton></div>
                                                  </ItemTemplate>
                                                  </asp:TemplateField>
                                                  </Columns>
                                          <HeaderStyle HorizontalAlign="Center" height="25px" BackColor="#CCCCCC"/></asp:GridView></div></td></tr></table>
                          
                          
                          <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content"><tr>
                                  <td valign="top" >
                                      <div style=" overflow-x:scroll; width:700px;">
                                          <a class="txtbox" style=" text-decoration:underline; font-weight:bold;">Approval History</a> 
                                          <asp:GridView ID="grdApprovalHis" runat="server" AllowPaging="True" 
                                              AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false" CellPadding="4" 
                                              CssClass="mGrid" EmptyDataText="No Records Found" HeaderStyle-Cssclass="GridBG" 
                                              PagerStyle-CssClass="pgr" PageSize="10" ShowHeaderWhenEmpty="true" Width="100%"><Columns>
                                                  <asp:TemplateField HeaderText="Request Code" Visible="false"><ItemTemplate>
                                                          <div 
                                                          align="center">
                                                              <asp:Label ID="lblHDocNo" runat="server" 
                                                          Text='<%#Bind("DocEntry") %>'>
                                                  </asp:Label></div></ItemTemplate></asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Reference No" Visible="false"><ItemTemplate>
                                                          <div 
                                                          align="center">
                                                              <asp:Label ID="lblhrefno" runat="server" 
                                                          Text='<%#Bind("U_Z_DocEntry") %>'></asp:Label></div></ItemTemplate></asp:TemplateField>
                                                  <asp:TemplateField HeaderText="DocType" Visible="false">
                                                      <ItemTemplate><div align="center">
                                                          <asp:Label ID="lblhdoctype" runat="server" Text='<%#Bind("U_Z_DocType") %>'></asp:Label></div></ItemTemplate></asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Employee ID">
                                                      <ItemTemplate><div align="left">&#160;<asp:Label ID="lblhempid" runat="server" 
                                                              Text='<%#Bind("U_Z_EmpId") %>'></asp:Label></div></ItemTemplate></asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Employee Name">
                                                      <ItemTemplate><div align="left">&#160;<asp:Label ID="lblhempname" runat="server" 
                                                              Text='<%#Bind("U_Z_EmpName") %>'></asp:Label></div></ItemTemplate></asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Approved By">
                                                      <ItemTemplate><div align="left">&#160;<asp:Label ID="lblhAppby" runat="server" 
                                                              Text='<%#Bind("U_Z_ApproveBy") %>'></asp:Label></div></ItemTemplate></asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Create Date">
                                                      <ItemTemplate><div align="left">&#160;<asp:Label ID="lblhcrdate" runat="server" 
                                                              Text='<%#Bind("CreateDate") %>'></asp:Label></div></ItemTemplate></asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Create Time">
                                                      <ItemTemplate><div align="left">&#160;<asp:Label ID="lblhcrtime" runat="server" 
                                                              Text='<%#Bind("CreateTime") %>'></asp:Label></div></ItemTemplate></asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Update Date">
                                                      <ItemTemplate><div align="left">&#160;<asp:Label ID="lblhupdate" runat="server" 
                                                              Text='<%#Bind("UpdateDate") %>'></asp:Label></div></ItemTemplate></asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Update Time">
                                                      <ItemTemplate><div align="left">&#160;<asp:Label ID="lblhuptime" runat="server" 
                                                              Text='<%#Bind("UpdateTime") %>'></asp:Label></div></ItemTemplate></asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Approved Status">
                                                      <ItemTemplate><div align="left">&#160;<asp:Label ID="lblhAppstatus" runat="server" 
                                                              Text='<%#Bind("U_Z_AppStatus") %>'></asp:Label></div></ItemTemplate></asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Remarks">
                                                      <ItemTemplate><div align="left">&#160;<asp:Label ID="lblhremarks" runat="server" 
                                                              Text='<%#Bind("U_Z_Remarks") %>'></asp:Label></div></ItemTemplate></asp:TemplateField></Columns>
                                              <HeaderStyle BackColor="#CCCCCC" height="25px" HorizontalAlign="Center" /></asp:GridView></div></td>
                                  <td >
                                      <table border="0" cellpadding="3" cellspacing="0" class="main_content" 
                                          width="100%">
                                      <tr><td><a class="txtbox" style=" text-decoration:underline; font-weight:bold;">Approvar Status</a> </td>
                                          <td><asp:TextBox ID="txtcode" runat="server" CssClass="txtbox" Visible="false"></asp:TextBox>
                                              <asp:TextBox ID="txtempid" runat="server" CssClass="txtbox" Visible="false"></asp:TextBox><asp:TextBox 
                                                  ID="txtlveCode" runat="server" CssClass="txtbox" Visible="false"></asp:TextBox></td></tr>
                                      <tr><td>Approval Status</td>
                                          <td><asp:DropDownList ID="ddlAppStatus" runat="server" CssClass="txtbox1">
                                              <asp:ListItem Value="P">Pending</asp:ListItem>
                                              <asp:ListItem Value="A">Approved</asp:ListItem>
                                              <asp:ListItem Value="R">Rejected</asp:ListItem></asp:DropDownList></td></tr>
                                      <tr><td>Payroll Month</td>
                                          <td><asp:DropDownList ID="ddlpaymonth" runat="server" CssClass="txtbox1"></asp:DropDownList></td></tr>
                                      <tr><td>Payroll Year</td>
                                          <td><asp:DropDownList ID="ddlpayYear" runat="server" CssClass="txtbox1"></asp:DropDownList></td></tr>
                                      <tr><td>Remarks</td>
                                          <td><asp:TextBox ID="txtcomments" runat="server" CssClass="txtbox" Height="80px" 
                                                  TextMode="MultiLine"></asp:TextBox></td></tr>
                                      <tr><td colspan="2">
                                          <br /><asp:Button ID="btnAdd" runat="server" CssClass="btn" 
                                              OnClientClick="return Confirmation();" Text="Save &amp; Submit" Width="125px" />
                                          <asp:Button ID="btncancel" runat="server" CssClass="btn" Text="Cancel" 
                                              Width="85px" /></td></tr></table></td></tr></table></ContentTemplate></ajx:TabPanel>   
                      
                      
                         <ajx:TabPanel  ID="TabPanel1" runat="server" HeaderText="Leave Request Summary" >
                             <ContentTemplate><table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content">
                                 <tr><td>
                                     <asp:GridView ID="grdSummary" runat="server" CellPadding="4" AllowPaging="True" ShowHeaderWhenEmpty="true" EmptyDataText = "No Records Found"  CssClass="mGrid" HeaderStyle-Cssclass="GridBG" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false" Width="100%" PageSize="10" ><Columns>
                                         <asp:TemplateField HeaderText="Code">
                                             <ItemTemplate><div align="center">
                                                 <asp:LinkButton ID="lblSCode" runat="server" Text='<%#Bind("Code") %>' OnClick="lbtndocnumSum_Click">
                                                 </asp:LinkButton></div></ItemTemplate></asp:TemplateField>
                                         <asp:TemplateField HeaderText="T&A Emp.ID">
                                             <ItemTemplate><div align="center">
                                                 <asp:label ID="lblStaEmpid" runat="server" Text='<%#Bind("TAEmpID") %>'  ></asp:label></div></ItemTemplate></asp:TemplateField>
                                         <asp:TemplateField HeaderText="Emp.Code">
                                             <ItemTemplate><div align="center">
                                                 <asp:label ID="lblsEmpid" runat="server" Text='<%#Bind("U_Z_EMPID") %>'  ></asp:label></div></ItemTemplate></asp:TemplateField>
                                         <asp:TemplateField HeaderText="Emp.Name">
                                             <ItemTemplate><div align="center">
                                                 <asp:label ID="lblsEmpname" runat="server" Text='<%#Bind("U_Z_EMPNAME") %>'  ></asp:label></div></ItemTemplate></asp:TemplateField>
                                         <asp:TemplateField HeaderText="Leave Name">
                                             <ItemTemplate><div align="left">&#160;<asp:Label ID="lbllveName" runat="server" Text='<%#Bind("U_Z_LeaveName") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField>
                                         <asp:TemplateField HeaderText="Leave Type" Visible="false">
                                             <ItemTemplate><div align="left">&#160;<asp:Label ID="lblsAgenda" runat="server" Text='<%#Bind("U_Z_TrnsCode") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField>
                                         <asp:TemplateField HeaderText="From Date">
                                             <ItemTemplate><div align="left">&#160;<asp:Label ID="lblsCouCode" runat="server" Text='<%#Bind("U_Z_StartDate") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField>
                                         <asp:TemplateField HeaderText="To Date">
                                             <ItemTemplate><div align="left">&#160;<asp:Label ID="lblsCouName" runat="server" Text='<%#Bind("U_Z_EndDate") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField>
                                         <asp:TemplateField HeaderText="No.of Days">
                                             <ItemTemplate><div align="left">&#160;<asp:Label ID="lblsCouType" runat="server" Text='<%#Bind("U_Z_NoofDays") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField>
                                         <asp:TemplateField HeaderText="Remarks">
                                             <ItemTemplate><div align="left">&#160;<asp:Label ID="lblsstdate" runat="server" Text='<%#Bind("U_Z_Notes") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField>
                                         <asp:TemplateField HeaderText="Payroll Month">
                                             <ItemTemplate><div align="left">&#160;<asp:Label ID="lblspaymonth" runat="server" Text='<%#Bind("U_Z_Month") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField>
                                         <asp:TemplateField HeaderText="Payroll Year">
                                             <ItemTemplate><div align="left">&#160;<asp:Label ID="lblspayyear" runat="server" Text='<%#Bind("U_Z_Year") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField>
                                         <asp:TemplateField HeaderText="Approval Status">
                                             <ItemTemplate><div align="left">&#160;<asp:Label ID="lblsAppStatus" runat="server" Text='<%#Bind("U_Z_Status") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField>
                                         <asp:TemplateField HeaderText="Approval Required">
                                             <ItemTemplate><div align="left">&#160;<asp:Label ID="lblsAppreq" runat="server" Text='<%#Bind("U_Z_AppRequired") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField>
                                         <asp:TemplateField HeaderText="Requested Date">
                                             <ItemTemplate><div align="left">&#160;<asp:Label ID="lblsreqdt" runat="server" Text='<%#Bind("U_Z_AppReqDate") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField>
                                         <asp:TemplateField HeaderText="Requested Time">
                                             <ItemTemplate><div align="left">&#160;<asp:Label ID="lblsreqtime" runat="server" Text='<%#Bind("U_Z_ReqTime") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField>
                                         <asp:TemplateField HeaderText="Current Approver">
                                             <ItemTemplate><div align="left">&#160;<asp:Label ID="lblscurapp" runat="server" Text='<%#Bind("U_Z_CurApprover") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField>
                                         <asp:TemplateField HeaderText="Next Approver">
                                             <ItemTemplate><div align="left">&#160;<asp:Label ID="lblsnxtapp" runat="server" Text='<%#Bind("U_Z_NxtApprover") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField></Columns>
                                     <HeaderStyle HorizontalAlign="Center" height="25px" BackColor="#CCCCCC"/></asp:GridView></td></tr></table>
                                 <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content"><tr>
                                         <td><a class="txtbox" style=" text-decoration:underline; font-weight:bold;">Approval History</a> 
                                             <br /><asp:GridView ID="grdHistorySummary" runat="server" CellPadding="4" AllowPaging="True" ShowHeaderWhenEmpty="true" EmptyDataText = "No Records Found"  CssClass="mGrid" HeaderStyle-Cssclass="GridBG" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false" Width="100%" PageSize="10" ><Columns>
                                                     <asp:TemplateField HeaderText="Request Code" Visible="false"><ItemTemplate><div align="center"><asp:label ID="lblDocNo" runat="server" Text='<%#Bind("DocEntry") %>'>
                                                  </asp:label></div></ItemTemplate></asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Reference No" Visible="false"><ItemTemplate><div align="center"><asp:label ID="lblrefno" runat="server" Text='<%#Bind("U_Z_DocEntry") %>'  ></asp:label></div></ItemTemplate></asp:TemplateField>
                                                     <asp:TemplateField HeaderText="DocType" Visible="false">
                                                         <ItemTemplate><div align="center">
                                                             <asp:label ID="lbldoctype" runat="server" Text='<%#Bind("U_Z_DocType") %>'  ></asp:label></div></ItemTemplate></asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Employee ID">
                                                         <ItemTemplate><div align="left">&#160;<asp:Label ID="lblempid" runat="server" Text='<%#Bind("U_Z_EmpId") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Employee Name">
                                                         <ItemTemplate><div align="left">&#160;<asp:Label ID="lblempname" runat="server" Text='<%#Bind("U_Z_EmpName") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Approved By">
                                                         <ItemTemplate><div align="left">&#160;<asp:Label ID="lblAppby" runat="server" Text='<%#Bind("U_Z_ApproveBy") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Create Date">
                                                         <ItemTemplate><div align="left">&#160;<asp:Label ID="lblcrdate" runat="server" Text='<%#Bind("CreateDate") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Create Time">
                                                         <ItemTemplate><div align="left">&#160;<asp:Label ID="lblcrtime" runat="server" Text='<%#Bind("CreateTime") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Update Date">
                                                         <ItemTemplate><div align="left">&#160;<asp:Label ID="lblupdate" runat="server" Text='<%#Bind("UpdateDate") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Update Time">
                                                         <ItemTemplate><div align="left">&#160;<asp:Label ID="lbluptime" runat="server" Text='<%#Bind("UpdateTime") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Approved Status">
                                                         <ItemTemplate><div align="left">&#160;<asp:Label ID="lblAppstatus" runat="server" Text='<%#Bind("U_Z_AppStatus") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Remarks">
                                                         <ItemTemplate><div align="left">&#160;<asp:Label ID="lblremarks" runat="server" Text='<%#Bind("U_Z_Remarks") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField></Columns>
                                                 <HeaderStyle HorizontalAlign="Center" height="25px" BackColor="#CCCCCC"/></asp:GridView></td></tr></table></ContentTemplate></ajx:TabPanel>                
                  
                    
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
        <td>
                 <div style="visibility:hidden">
<asp:Button id="btnSample" runat="server"/>
<asp:Button id="btnHistory" runat="server"/>
</div>
        <ajx:ModalPopupExtender ID="ModalPopupExtender7" runat="server" DropShadow="True" PopupControlID="Panelpoptechnician" TargetControlID="btnSample" CancelControlID="btnclstech" BackgroundCssClass="modalBackground">
                    </ajx:ModalPopupExtender>  

                    <ajx:ModalPopupExtender ID="ModalPopupExtender1" runat="server" DropShadow="True" PopupControlID="Panelpoptechnician1" TargetControlID="btnHistory" CancelControlID="btnclstech1" BackgroundCssClass="modalBackground">
                    </ajx:ModalPopupExtender>  
        </td>
        </tr>
        <tr>
 <td>
  <asp:Panel ID="Panelpoptechnician" runat="server" BackColor="White" style=" display:none; padding:10px; width:500px; ">
                                <div><span class="sideheading" style="color:Green;">Leave Balance Details</span> <span style="float:right;"> 
                                <asp:Button ID="btnclstech" runat="server"  CssClass="btn" Width="30px" Text="X" /></span></div>
                                   <br />
                           
                              <br /><asp:Panel ID="Panel4" runat="server" Height="300px" ScrollBars="Vertical">
                                  <asp:Label ID="Label1" runat="server" Text="" CssClass="txtbox" ForeColor="Red"></asp:Label>
                                 <asp:GridView ID="grdRequesttohr" runat="server" CellPadding="4"  ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found"  CssClass="mGrid" HeaderStyle-Cssclass="GridBG" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false" Width="100%"  ><Columns>
                                          <asp:TemplateField HeaderText="Leave Code">
                                              <ItemTemplate><div align="center">
                                                  <asp:label ID="lbtndocnum" runat="server" Text='<%#Bind("U_Z_LeaveCode") %>'  ></asp:label></div></ItemTemplate></asp:TemplateField>
                                          <asp:TemplateField HeaderText="Leave Name">
                                              <ItemTemplate><div align="center">
                                                  <asp:label ID="lblactivity" runat="server" Text='<%#Bind("U_Z_LeaveName") %>'  ></asp:label></div></ItemTemplate></asp:TemplateField>
                                          <asp:TemplateField HeaderText="Leave Balance">
                                              <ItemTemplate><div align="center">
                                                  <asp:label ID="lbltype" runat="server" Text='<%#Bind("U_Z_Balance") %>'  ></asp:label></div>
                                                  </ItemTemplate>
                                            </asp:TemplateField>
                                             
                                    </Columns>
                                    <HeaderStyle HorizontalAlign="Center" height="25px" BackColor="#CCCCCC"/>
                                    </asp:GridView>
                                    </asp:Panel>
                            </asp:Panel>

                              <asp:Panel ID="Panelpoptechnician1" runat="server" BackColor="White" style=" display:none; padding:10px; width:800px; ">
                                <div><span class="sideheading" style="color:Green;">Leave Approved History</span> <span style="float:right;"> 
                                <asp:Button ID="btnclstech1" runat="server"  CssClass="btn" Width="30px" Text="X" /></span></div>
                                   <br />
                           
                              <br /><asp:Panel ID="Panel2" runat="server" Height="400px" ScrollBars="Vertical">
                                  <asp:Label ID="Label4" runat="server" Text="" CssClass="txtbox" ForeColor="Red"></asp:Label>
                                  <asp:GridView ID="GridView1" runat="server" CellPadding="4" ShowHeaderWhenEmpty="true"  CssClass="mGrid" HeaderStyle-Cssclass="GridBG"  AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false" Width="100%" ><Columns>
                                          <asp:TemplateField HeaderText="Request Code">
                                              <ItemTemplate><div align="center">
                                                  <asp:Label ID="lbtsdocnum" runat="server" Text='<%#Bind("Code") %>'></asp:Label></div></ItemTemplate></asp:TemplateField>
                                          <asp:TemplateField HeaderText="Leave Code">
                                              <ItemTemplate><div align="center">
                                                  <asp:label ID="lblslvecode" runat="server" Text='<%#Bind("U_Z_TrnsCode") %>'  ></asp:label></div></ItemTemplate></asp:TemplateField>
                                          <asp:TemplateField HeaderText="Leave Name">
                                              <ItemTemplate><div align="center">
                                                  <asp:label ID="lblslvetype" runat="server" Text='<%#Bind("Name") %>'  ></asp:label></div></ItemTemplate></asp:TemplateField>
                                          <asp:TemplateField HeaderText="Start Date">
                                              <ItemTemplate><div align="left">&nbsp;<asp:Label ID="lblsstdate" runat="server" Text='<%#Bind("U_Z_StartDate") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField>
                                          <asp:TemplateField HeaderText="End Date">
                                              <ItemTemplate><div align="left">&nbsp;<asp:Label ID="lblsedDate" runat="server" Text='<%#Bind("U_Z_EndDate") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField>
                                          <asp:TemplateField HeaderText="No.Of Days">
                                              <ItemTemplate><div align="left">&nbsp;<asp:Label ID="lblsdays" runat="server" Text='<%#Bind("U_Z_NoofDays") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reason">
                                              <ItemTemplate><div align="left">&nbsp;<asp:Label ID="lblsreason" runat="server" Text='<%#Bind("U_Z_Notes") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField>
                                     
                  </Columns><HeaderStyle HorizontalAlign="Center" height="25px" BackColor="#CCCCCC"/></asp:GridView>
                                    </asp:Panel>
                            </asp:Panel>
 </td>
 </tr>
</table> 

</ContentTemplate> 
</asp:UpdatePanel> 
</asp:Content>

