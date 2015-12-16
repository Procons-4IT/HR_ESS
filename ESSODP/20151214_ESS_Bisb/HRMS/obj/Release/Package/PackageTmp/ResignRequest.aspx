<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/HRMS.Master" CodeBehind="ResignRequest.aspx.vb" Inherits="HRMS.ResignRequest" %>
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
<asp:Image ID="Image1" ImageUrl="Images/waiting.gif" AlternateText="Processing" runat="server" />
</ProgressTemplate>
</asp:UpdateProgress>
<ajx:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress" PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
<asp:UpdatePanel ID="Update" runat="server">
<ContentTemplate>

 <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
 <tr>
 <td>
  
 </td>
 </tr>
 <tr>

    <td height="30" align="left" colspan="2" valign="bottom" background="images/h_bg.png"; style="border-bottom:1px dotted; border-color: #f45501; background-repeat:repeat-x">     
      <div >&nbsp; <asp:Label ID="Label3" runat="server" Text="Resignation Request"  CssClass="subheader" style="float:left;" ></asp:Label>  <span ><asp:Label ID="lblNewTrip" runat="server" Text="" Visible="false"></asp:Label></span> </div>
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
            <asp:ImageButton ID="btnnew" runat="server" ImageUrl="~/images/Add.jpg" ToolTip="Add new record" />
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        
            </asp:Panel> 
            <asp:Label ID="Label2" runat="server" Text="" style="color:Red;"></asp:Label>
              <asp:Panel ID="panelview" runat="server" Width="100%" BorderColor="LightSteelBlue"  BorderWidth="2">
            <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
              <tr>
              <td valign="top">  
                <ajx:TabContainer ID="TabContainer2" runat="server" ActiveTabIndex="0"  CssClass="ajax__tab_yuitabview-theme" Width="100%" >
                  <ajx:TabPanel  ID="TabPanel3" runat="server" HeaderText="Resignation Request" ><ContentTemplate>
                          <table width="90%" border="0" cellspacing="0" cellpadding="3" class="main_content"><tr>
                                  <td><asp:GridView ID="grdLeaveRequest" runat="server" CellPadding="4" AllowPaging="True" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found"  CssClass="mGrid" HeaderStyle-Cssclass="GridBG" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false" Width="100%" PageSize="10" ><Columns>
                                          <asp:TemplateField HeaderText="Request Code" HeaderStyle-Width="100px"><ItemTemplate><div align="center"><asp:LinkButton ID="lbtndocnum" runat="server" Text='<%#Bind("Code") %>' onclick="lbtndocnum_Click"></asp:LinkButton></div><%----%></ItemTemplate></asp:TemplateField>
                                          <asp:TemplateField HeaderText="Resignation Date" HeaderStyle-Width="130px"><ItemTemplate><div align="left">&nbsp;<asp:Label ID="lblstdate" runat="server" Text='<%#Bind("U_Z_StartDate") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField>
                                          <asp:TemplateField HeaderText="Reason" HeaderStyle-Width="400px"><ItemTemplate><div align="left">&nbsp;<asp:Label ID="lblreason" runat="server" Text='<%#Bind("U_Z_Notes") %>'  ></asp:Label></div></ItemTemplate></asp:TemplateField>
                                          <asp:TemplateField HeaderText="Status" HeaderStyle-Width="100px"><ItemTemplate><div align="left">&nbsp;<asp:Label ID="lblstatus" runat="server" Text='<%#Bind("U_Z_Status") %>' ></asp:Label></div></ItemTemplate></asp:TemplateField>
                                           <asp:TemplateField HeaderText="Approver Remarks">
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:Label ID="lblAppRemark" runat="server" Text='<%#Bind("U_Z_AppRemarks") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>  
                                          </Columns>
                                      <HeaderStyle HorizontalAlign="Center" height="25px" BackColor="#CCCCCC"/></asp:GridView></td></tr></table></ContentTemplate></ajx:TabPanel>                
                  
                    
                 </ajx:TabContainer>   
              
              </td> 
              </tr> 
              </table> 
          </asp:Panel> 
      
            <asp:Panel ID="PanelNewRequest"  runat="server" Width="100%" BorderColor="LightSteelBlue"  BorderWidth="2">
                           <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content"> 
                               
                                      <tr>
                                      <td >Resignation Date</td>
                                     <td>
                                      <asp:TextBox ID="txtfrmdate" CssClass="txtbox" runat="server" ></asp:TextBox> 
                                       <ajx:CalendarExtender ID="CalendarExtender2" Animated="true" Format="dd/MM/yyyy" runat="server" TargetControlID="txtfrmdate" CssClass= "cal_Theme1"></ajx:CalendarExtender>                                             
                                     </td>
                                     </tr>                                     
                                      <tr>
                                      <td >Reason</td>
                                     <td>
                                      <asp:TextBox ID="txtreason" CssClass="txtbox"  runat="server"></asp:TextBox>                                             
                                     </td>
                                      <td>
                                      <asp:TextBox ID="txtcode" CssClass="txtbox"  runat="server"  Visible="false" ></asp:TextBox>                                             
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
 </table> 
 </ContentTemplate>
 </asp:UpdatePanel>

</asp:Content>
