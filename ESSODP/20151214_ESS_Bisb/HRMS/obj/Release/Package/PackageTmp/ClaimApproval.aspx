<%@ Page Title="Expenses Claim Approval" Language="vb" AutoEventWireup="false" MasterPageFile="~/HRMS.Master" CodeBehind="ClaimApproval.aspx.vb" Inherits="HRMS.ClaimApproval" %>
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
<%--<asp:UpdatePanel ID="Update" runat="server">
<ContentTemplate>--%>

<table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
 <tr>

    <td height="30" align="left" colspan="2" valign="bottom" background="images/h_bg.png"; style="border-bottom:1px dotted; border-color: #f45501; background-repeat:repeat-x">     
      <div >&nbsp; <asp:Label ID="Label3" runat="server" Text="Expenses Claim Approval"  CssClass="subheader" style="float:left;" ></asp:Label>  <span ><asp:Label ID="lblNewTrip" runat="server" Text="" Visible="false"></asp:Label></span> </div>
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
                  <ajx:TabPanel  ID="TabPanel3" runat="server" HeaderText="Expenses Claim Approval" >
                  <ContentTemplate>
                         <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content">
                              <tr>
                                  <td>
                                   <div style=" overflow-x:scroll; width:1100px;">
                                      <asp:GridView ID="grdRequestApproval" runat="server" CellPadding="4" AllowPaging="True" ShowHeaderWhenEmpty="true" EmptyDataText = "No Records Found"  CssClass="mGrid" HeaderStyle-Cssclass="GridBG" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false" Width="100%" PageSize="10" >
                                          <Columns>
                                              <asp:TemplateField HeaderText="Request Code" >
                                                  <ItemTemplate>
                                                      <div align="center">
                                                          <asp:LinkButton ID="lblCode" runat="server" Text='<%#Bind("Code") %>' OnClick="lbtndocnum_Click">
                                                  </asp:LinkButton>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                               <asp:TemplateField HeaderText="T&A Emp.ID">
                                                  <ItemTemplate>
                                                      <div align="center">
                                                          <asp:label ID="lbltaEmpid" runat="server" Text='<%#Bind("TAEmpID") %>'  ></asp:label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Emp.Code">
                                                  <ItemTemplate>
                                                      <div align="center">
                                                          <asp:label ID="lblEmpid" runat="server" Text='<%#Bind("U_Z_EmpID") %>'  ></asp:label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Emp.Name">
                                                  <ItemTemplate>
                                                      <div align="center">
                                                          <asp:label ID="lblEmpname" runat="server" Text='<%#Bind("U_Z_EmpName") %>'  ></asp:label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Submitted Date">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblAgenda" runat="server" Text='<%#Bind("U_Z_SubDt") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Transaction Date">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lbltransdt" runat="server" Text='<%#Bind("U_Z_Claimdt") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Expense Type">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblCouName" runat="server" Text='<%#Bind("U_Z_ExpType") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                             
                                              <asp:TemplateField HeaderText="Client">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblstdate" runat="server" Text='<%#Bind("U_Z_Client") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Project">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lbleddate" runat="server" Text='<%#Bind("U_Z_Project") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Transaction Currency">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblCouType" runat="server" Text='<%#Bind("U_Z_Currency") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Transaction Amount">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lbltraamt" runat="server" Text='<%#Bind("U_Z_CurAmt") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Local Currency Amount">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblusdamt" runat="server" Text='<%#Bind("U_Z_UsdAmt") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Redim Amount">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblredim" runat="server" Text='<%#Bind("U_Z_ReimAmt") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>                                            
                                              <asp:TemplateField HeaderText="Attachments">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp; <asp:LinkButton ID="lnkDownload" Text = "Download" CommandArgument = '<%# Eval("U_Z_Attachment") %>' runat="server" OnClick="lnkDownload_Click" Width="100px"></asp:LinkButton>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Payroll Month">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblpaymonth" runat="server" Text='<%#Bind("U_Z_Month") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Payroll Year">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblpayyear" runat="server" Text='<%#Bind("U_Z_Year") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Approval Status">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblAppStatus" runat="server" Text='<%#Bind("U_Z_AppStatus") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>

                                               <asp:TemplateField HeaderText="Approval Required">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblAppreq" runat="server" Text='<%#Bind("U_Z_AppRequired") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Requested Date">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblreqdt" runat="server" Text='<%#Bind("U_Z_AppReqDate") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Requested Time">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblreqtime" runat="server" Text='<%#Bind("U_Z_ReqTime") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>                                            
                                              <asp:TemplateField HeaderText="Current Approver">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblcurapp" runat="server" Text='<%#Bind("U_Z_CurApprover") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Next Approver">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblnxtapp" runat="server" Text='<%#Bind("U_Z_NxtApprover") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                          </Columns>
                                          <HeaderStyle HorizontalAlign="Center" height="25px" BackColor="#CCCCCC"/>
                                      </asp:GridView>
                                      </div> 
                                  </td>
                              </tr>
                          </table>

                            <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content">
                              <tr>
                                  <td valign="top" >
                                  <div style=" overflow-x:scroll; width:700px;">
                                  <a class="txtbox" style=" text-decoration:underline; font-weight:bold;">Approval History</a>
                                      <asp:GridView ID="grdApprovalHis" runat="server" CellPadding="4" AllowPaging="True" ShowHeaderWhenEmpty="true" EmptyDataText = "No Records Found"  CssClass="mGrid" HeaderStyle-Cssclass="GridBG" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false" Width="100%" PageSize="10" >
                                           <Columns>
                                              <asp:TemplateField HeaderText="Request Code" Visible="false">
                                                  <ItemTemplate>
                                                      <div align="center">
                                                          <asp:label ID="lblHDocNo" runat="server" Text='<%#Bind("DocEntry") %>'>
                                                  </asp:label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Reference No" Visible="false">
                                                  <ItemTemplate>
                                                      <div align="center">
                                                          <asp:label ID="lblhrefno" runat="server" Text='<%#Bind("U_Z_DocEntry") %>'  ></asp:label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                              <asp:TemplateField HeaderText="DocType" Visible="false">
                                                  <ItemTemplate>
                                                      <div align="center">
                                                          <asp:label ID="lblhdoctype" runat="server" Text='<%#Bind("U_Z_DocType") %>'  ></asp:label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Employee ID">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblhempid" runat="server" Text='<%#Bind("U_Z_EmpId") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Employee Name">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblhempname" runat="server" Text='<%#Bind("U_Z_EmpName") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Approved By">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblhAppby" runat="server" Text='<%#Bind("U_Z_ApproveBy") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Create Date">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblhcrdate" runat="server" Text='<%#Bind("CreateDate") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Create Time">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblhcrtime" runat="server" Text='<%#Bind("CreateTime") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Update Date">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblhupdate" runat="server" Text='<%#Bind("UpdateDate") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Update Time">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblhuptime" runat="server" Text='<%#Bind("UpdateTime") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Approved Status">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblhAppstatus" runat="server" Text='<%#Bind("U_Z_AppStatus") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remarks">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblhremarks" runat="server" Text='<%#Bind("U_Z_Remarks") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                             
                                          </Columns>
                                          <HeaderStyle HorizontalAlign="Center" height="25px" BackColor="#CCCCCC"/>
                                         
                                      </asp:GridView>

                                      </div> 
                                  </td>

                                  <td >
                                   <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content"> 
                                   <tr>
                                   <td>
                                    <a class="txtbox" style=" text-decoration:underline; font-weight:bold;">Approvar Status</a>
                                   </td>
                                   <td>
                                    <asp:TextBox ID="txtcode" CssClass="txtbox" runat="server" Visible="false" ></asp:TextBox>   
                                     <asp:TextBox ID="txtempid" CssClass="txtbox" runat="server" Visible="false" ></asp:TextBox>    
                                   </td>
                                   </tr>
                                   <tr>
                                   
                                   
                                 <td >Approval Status</td>
                                     <td>                                     
                                         <asp:DropDownList ID="ddlAppStatus" runat="server" CssClass="txtbox1">
                                         <asp:ListItem Value="P">Pending</asp:ListItem>
                                          <asp:ListItem Value="A">Approved</asp:ListItem>
                                           <asp:ListItem Value="R">Rejected</asp:ListItem>
                                         </asp:DropDownList>
                                     </td>
                                     </tr>
                                      <tr>
                                   
                                   
                                 <td >Payroll Month</td>
                                     <td>                                     
                                         <asp:DropDownList ID="ddlpaymonth" runat="server" CssClass="txtbox1">
                                         </asp:DropDownList>
                                     </td>
                                     </tr>
                                      <tr>                                  
                                   
                                 <td >Payroll Year</td>
                                     <td>                                     
                                         <asp:DropDownList ID="ddlpayYear" runat="server" CssClass="txtbox1">
                                        </asp:DropDownList>
                                     </td>
                                     </tr>
                                      <tr>
                                    <td >Remarks</td>
                                     <td>                                     
                                       <asp:TextBox ID="txtcomments" CssClass="txtbox" runat="server" Height="80px" TextMode="MultiLine" ></asp:TextBox>                                                
                                     </td>
                                     </tr>

                                      <tr>
                <td colspan="2">
                <br />
                    <asp:Button ID="btnAdd"  CssClass="btn" Width="125px" runat="server" Text="Save & Submit" OnClientClick="return Confirmation();" /> 
                   <asp:Button ID="btncancel"  CssClass="btn" Width="85px" runat="server" Text="Cancel" />
                  <%--  --%>
                                 </td>
            </tr>
                                   </table> 
                                  </td>
                              </tr>
                          </table>
                      </ContentTemplate>
                      </ajx:TabPanel>   
                      
                      
                         <ajx:TabPanel  ID="TabPanel1" runat="server" HeaderText="Expenses Claim Summary" >
                             <ContentTemplate>
                             <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content">
                              <tr>
                                  <td>
                                   <div style=" overflow-x:scroll; width:1100px;">
                                      <asp:GridView ID="grdSummary" runat="server" CellPadding="4" AllowPaging="True" ShowHeaderWhenEmpty="true" EmptyDataText = "No Records Found"  CssClass="mGrid" HeaderStyle-Cssclass="GridBG" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false" Width="100%" PageSize="10" >
                                          <Columns>
                                              <asp:TemplateField HeaderText="Code">
                                                  <ItemTemplate>
                                                      <div align="center">
                                                         <asp:LinkButton ID="lblSCode" runat="server" Text='<%#Bind("Code") %>' OnClick="lbtndocnumSum_Click">
                                                 </asp:LinkButton> 
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                               <asp:TemplateField HeaderText="T&A Emp.Code">
                                                  <ItemTemplate>
                                                      <div align="center">
                                                          <asp:label ID="lblStaEmpid" runat="server" Text='<%#Bind("TAEmpID") %>'  ></asp:label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Emp.Code">
                                                  <ItemTemplate>
                                                      <div align="center">
                                                          <asp:label ID="lblsEmpid" runat="server" Text='<%#Bind("U_Z_EmpID") %>'  ></asp:label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Emp.Name">
                                                  <ItemTemplate>
                                                      <div align="center">
                                                          <asp:label ID="lblsEmpname" runat="server" Text='<%#Bind("U_Z_EmpName") %>'  ></asp:label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Submitted Date">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblsAgenda" runat="server" Text='<%#Bind("U_Z_SubDt") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Transaction Date">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblsCouCode" runat="server" Text='<%#Bind("U_Z_Claimdt") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Expense Type">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblsCouName" runat="server" Text='<%#Bind("U_Z_ExpType") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Transaction Currency">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblsCouType" runat="server" Text='<%#Bind("U_Z_Currency") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Client">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblsstdate" runat="server" Text='<%#Bind("U_Z_Client") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Project">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblseddate" runat="server" Text='<%#Bind("U_Z_Project") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Transaction Amount">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblstraamt" runat="server" Text='<%#Bind("U_Z_CurAmt") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Local Currency Amount">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblsusdamt" runat="server" Text='<%#Bind("U_Z_UsdAmt") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Redim Amount">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblssredim" runat="server" Text='<%#Bind("U_Z_ReimAmt") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>                                            
                                              <asp:TemplateField HeaderText="Attachments">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp; <asp:LinkButton ID="SlnkDownload" Text = "Download" CommandArgument = '<%# Eval("U_Z_Attachment") %>' runat="server" OnClick="SlnkDownload_Click" Width="100px"></asp:LinkButton>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Payroll Month">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblspaymonth" runat="server" Text='<%#Bind("U_Z_Month") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Payroll Year">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblspayyear" runat="server" Text='<%#Bind("U_Z_Year") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Approval Status">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblsAppStatus" runat="server" Text='<%#Bind("U_Z_AppStatus") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>

                                               <asp:TemplateField HeaderText="Approval Required">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblsAppreq" runat="server" Text='<%#Bind("U_Z_AppRequired") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Requested Date">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblsreqdt" runat="server" Text='<%#Bind("U_Z_AppReqDate") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Requested Time">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblsreqtime" runat="server" Text='<%#Bind("U_Z_ReqTime") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>                                            
                                              <asp:TemplateField HeaderText="Current Approver">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblscurapp" runat="server" Text='<%#Bind("U_Z_CurApprover") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Next Approver">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblsnxtapp" runat="server" Text='<%#Bind("U_Z_NxtApprover") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                          </Columns>
                                          <HeaderStyle HorizontalAlign="Center" height="25px" BackColor="#CCCCCC"/>
                                      </asp:GridView>
                                      </div> 
                                  </td>
                              </tr>
                          </table>

                            <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content">
                              <tr>
                                  <td>
                                     <a class="txtbox" style=" text-decoration:underline; font-weight:bold;">Approval History</a>
                                     <br />
                                      <asp:GridView ID="grdHistorySummary" runat="server" CellPadding="4" AllowPaging="True" ShowHeaderWhenEmpty="true" EmptyDataText = "No Records Found"  CssClass="mGrid" HeaderStyle-Cssclass="GridBG" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false" Width="100%" PageSize="10" >
                                          <Columns>
                                              <asp:TemplateField HeaderText="Request Code" Visible="false">
                                                  <ItemTemplate>
                                                      <div align="center">
                                                          <asp:label ID="lblDocNo" runat="server" Text='<%#Bind("DocEntry") %>'>
                                                  </asp:label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Reference No" Visible="false">
                                                  <ItemTemplate>
                                                      <div align="center">
                                                          <asp:label ID="lblrefno" runat="server" Text='<%#Bind("U_Z_DocEntry") %>'  ></asp:label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                              <asp:TemplateField HeaderText="DocType" Visible="false">
                                                  <ItemTemplate>
                                                      <div align="center">
                                                          <asp:label ID="lbldoctype" runat="server" Text='<%#Bind("U_Z_DocType") %>'  ></asp:label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Employee ID">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblempid" runat="server" Text='<%#Bind("U_Z_EmpId") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Employee Name">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblempname" runat="server" Text='<%#Bind("U_Z_EmpName") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Approved By">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblAppby" runat="server" Text='<%#Bind("U_Z_ApproveBy") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Create Date">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblcrdate" runat="server" Text='<%#Bind("CreateDate") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Create Time">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblcrtime" runat="server" Text='<%#Bind("CreateTime") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Update Date">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblupdate" runat="server" Text='<%#Bind("UpdateDate") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Update Time">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lbluptime" runat="server" Text='<%#Bind("UpdateTime") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Approved Status">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblAppstatus" runat="server" Text='<%#Bind("U_Z_AppStatus") %>' ></asp:Label>
                                                      </div>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remarks">
                                                  <ItemTemplate>
                                                      <div align="left">
                                                          &nbsp;<asp:Label ID="lblremarks" runat="server" Text='<%#Bind("U_Z_Remarks") %>' ></asp:Label>
                                                      </div>
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
      
         
        </td>
        </tr>
        </table>         
        </td> 
        </tr> 
</table> 

<%--</ContentTemplate> 
</asp:UpdatePanel>--%> 
</asp:Content>

