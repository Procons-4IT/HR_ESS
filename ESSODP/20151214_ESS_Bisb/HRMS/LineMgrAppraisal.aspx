<%@ Page Title="First Level Approval" Language="vb" AutoEventWireup="false" MasterPageFile="~/HRMS.Master" CodeBehind="LineMgrAppraisal.aspx.vb" Inherits="HRMS.LineMgrAppraisal" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

   <script type="text/javascript">
    
       function Confirmation() {
           if (confirm("Do you want to confirm the changes?") == true) {
               return true;
           }
           else {
               return false;
           }
       }

       function Confirmation1() {
           if (confirm("Sure Want to Approve Document?") == true) {
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
<asp:Image ID="Image1" ImageUrl="Images/waiting.gif" AlternateText="Processing" runat="server" />
</ProgressTemplate>
</asp:UpdateProgress>
<ajx:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress" PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />

<asp:UpdatePanel ID="Update" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
 <table width="100%" border="0" cellspacing="0" cellpadding="4" class="main_content">
 <tr>
 <td>
  
 </td>
 </tr>
 <tr>

    <td height="30" align="left" colspan="2" valign="bottom" background="images/h_bg.png"; style="border-bottom:1px dotted; border-color: #f45501; background-repeat:repeat-x">     
      <div >&nbsp; <asp:Label ID="Label3" runat="server" Text="First Level Approval"  CssClass="subheader" style="float:left;" ></asp:Label>  <span ><asp:Label ID="lblNewTrip" runat="server" Text="" Visible="false"></asp:Label></span> </div>
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
            <asp:ImageButton ID="btnnew" runat="server" ImageUrl="~/images/Add.jpg" ToolTip="Add new record" visible="false"
                  /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        
            </asp:Panel> 
            <asp:Label ID="Label2" runat="server" Text="" style="color:Red;"></asp:Label>
              <asp:Panel ID="panelview" runat="server" Width="100%" BorderColor="LightSteelBlue"  BorderWidth="2">
            <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
              <tr>
              <td valign="top">  
                <ajx:TabContainer ID="TabContainer2" runat="server" ActiveTabIndex="0"  CssClass="ajax__tab_yuitabview-theme" Width="100%" >
                  <ajx:TabPanel  ID="TabPanel3" runat="server" HeaderText="Appraisal Details" >
                   <ContentTemplate>
                      <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content">  
                      <tr>
                      <td width="3%">
                          <a style=" font-size:10pt; font-weight:bold;">Search :</a></td>
                       <td width="3%">Appraisal No.</td>
                     <td width="5%">
                            <asp:TextBox ID="txtAppFrom" Width="100px" runat="server" CssClass="txtbox"></asp:TextBox>  
                            <ajx:TextBoxWatermarkExtender ID="TBWE1" runat="server"
                            TargetControlID="txtAppFrom"
                            WatermarkText="Appraisal Number"
                            WatermarkCssClass="watermark" Enabled="True" />
                        </td>
                          <td width="3%">Employee Id</td>
                         <td width="5%">
                            <asp:TextBox ID="txtEmpfrom" Width="100px" runat="server" CssClass="txtbox"></asp:TextBox> 
                            <ajx:TextBoxWatermarkExtender ID="TBWE3" runat="server"
                            TargetControlID="txtEmpfrom"
                            WatermarkText="Employee ID"
                            WatermarkCssClass="watermark" Enabled="True" /> 
                        </td>
                       
                         <td width="2%">Period</td>
                             <td  width="5%">
                              <asp:DropDownList ID="ddlsrPeriod" CssClass="txtbox1" Width="120px" runat="server"  >   </asp:DropDownList> 
                             </td>
                               <td width="2%">Status</td>
                             <td  width="5%">
                              <asp:DropDownList ID="ddlsrStatus" CssClass="txtbox1" Width="120px" runat="server"  > 
                              <asp:ListItem Value="">---Select---</asp:ListItem>     
                                <asp:ListItem Value="SE">SelfApproved</asp:ListItem>                            
                               <asp:ListItem Value="LM">LineManager Approved</asp:ListItem>
                               <asp:ListItem Value="SM">Sr.Manager Approved</asp:ListItem>
                                 <asp:ListItem Value="HR">HR Approved</asp:ListItem>                            
                               <asp:ListItem Value="DR">Draft</asp:ListItem>
                                </asp:DropDownList> 
                             </td>
                         <td width="5%">
                         <asp:Button ID="btnSearch"  CssClass="btn" runat="server" Text="Search" />
                        </td>
                          <td width="5%">
                         <asp:LinkButton runat="server" id="LnkViewall">View All</asp:LinkButton>
                        </td>
                      </tr>      
                      </table>  



                       <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content">                            
                                   <tr>
                                     <td>
                               <asp:GridView ID="grdRequestApp" runat="server" CellPadding="4"  CssClass="mGrid" 
                                             ShowHeaderWhenEmpty="True" EmptyDataText="No records Found" AllowPaging="True" 
                                             AutoGenerateColumns="False" Width="100%" >
                                   <AlternatingRowStyle CssClass="alt" />
                                   <Columns>
                                       <asp:TemplateField HeaderText="Appraisal No.">
                                           <ItemTemplate>
                                               <div align="center">
                                                   <asp:LinkButton ID="lbtndocnum" runat="server" Text='<%#Bind("DocEntry") %>' onclick="lbtndocnum_Click" ></asp:LinkButton>
                                               </div>
                                           </ItemTemplate>
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Request Date">
                                           <ItemTemplate>
                                               <div align="left">
                                                   &nbsp;<asp:Label ID="lblcust" runat="server" Text='<%#Bind("U_Z_Date") %>' ></asp:Label>
                                               </div>
                                           </ItemTemplate>
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="EmployeeId">
                                           <ItemTemplate>
                                               <div align="left">
                                                   &nbsp;<asp:Label ID="lblcustName" runat="server" Text='<%#Bind("U_Z_EmpId") %>' ></asp:Label>
                                               </div>
                                           </ItemTemplate>
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Employee Name">
                                           <ItemTemplate>
                                               <div align="left">
                                                   &nbsp;<asp:Label ID="lblitem" runat="server" Text='<%#Bind("U_Z_EmpName") %>' ></asp:Label>
                                               </div>
                                           </ItemTemplate>
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Period" Visible="false" >
                                           <ItemTemplate>
                                               <div align="left">
                                                   &nbsp;<asp:Label ID="lblPeriod" runat="server" Text='<%#Bind("U_Z_Period") %>' ></asp:Label>
                                               </div>
                                           </ItemTemplate>
                                       </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Period Desc" Visible="false" >
                                           <ItemTemplate>
                                               <div align="left">
                                                   &nbsp;<asp:Label ID="lblPerioddesc" runat="server" Text='<%#Bind("U_Z_PerDesc") %>' ></asp:Label>
                                               </div>
                                           </ItemTemplate>
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Period From" >
                                           <ItemTemplate>
                                               <div align="left">
                                                   &nbsp;<asp:Label ID="lblPeriodFr" runat="server" Text='<%#Bind("U_Z_PerFrom") %>' ></asp:Label>
                                               </div>
                                           </ItemTemplate>
                                       </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Period To" >
                                           <ItemTemplate>
                                               <div align="left">
                                                   &nbsp;<asp:Label ID="lblPeriodto" runat="server" Text='<%#Bind("U_Z_PerTo") %>' ></asp:Label>
                                               </div>
                                           </ItemTemplate>
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Active" visible="False">
                                           <ItemTemplate>
                                               <div align="center">
                                                   &nbsp;<asp:Label ID="lblserial" runat="server" Text='<%#Bind("U_Z_Status") %>' ></asp:Label>
                                               </div>
                                           </ItemTemplate>
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Status">
                                           <ItemTemplate>
                                               <div align="center">
                                                   &nbsp;<asp:Label ID="lblstatus" runat="server" Text='<%#Bind("U_Z_WStatus") %>' ></asp:Label>
                                               </div>
                                           </ItemTemplate>
                                       </asp:TemplateField>
                                   </Columns>
                            <HeaderStyle HorizontalAlign="Center" height="25px" BackColor="#CCCCCC" 
                                       CssClass="GridBG"/>
                                   <PagerStyle CssClass="pgr" />
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
      
            
            <asp:Panel ID="panelnew" runat="server" Width="100%" >
            
              <div class="DivCorner" style="border:solid 2px LightSteelBlue; width:100%;">    
            <table width="100%" border="0" cellspacing="0" cellpadding="4" class="main_content">
              <tr>
                        <td width="20%">Appraisal Number</td>
                        <td width="15%">
                            <asp:Label ID="txtAppno"  CssClass="txtbox" Width="150px" runat="server"></asp:Label>                       
                                        
                        </td>
                        <td  width="10%"></td>
                        <td  width="20%" >Request Date</td>
                        <td  width="15%">                       
                         <asp:Label ID="txtreqdate"  CssClass="txtbox" Width="150px"  runat="server"></asp:Label>                            
                        </td>
                   </tr>
            <tr>
                 <td width="10%">Employee Id</td>
                 <td>
                  <asp:Label ID="txtempid" CssClass="txtbox" Width="150px"  runat="server" ></asp:Label>
                 </td>
                  <td  width="15%"></td>
                  <td  width="10%">Employee Name</td>
                  <td width="30%">
                   <asp:Label ID="txtempname"  CssClass="txtbox" Width="150px"   runat="server"></asp:Label>        
                  </td>                
            </tr>
            <tr>
                 <td width="10%">Period From</td>
                 <td>
                  <asp:Label ID="lblPeriodfrom" CssClass="txtbox" Width="150px"  runat="server" ></asp:Label>
                 </td>
                  <td  width="15%"></td>
                  <td  width="10%">Period To</td>
                  <td width="30%">
                   <asp:Label ID="lblperioTo"  CssClass="txtbox" Width="150px"   runat="server"></asp:Label>        
                  </td>                
            </tr>
            <tr>
                 <td width="10%"></td>
                 <td>
                  <asp:DropDownList ID="ddlperiod" CssClass="txtbox1" Width="120px" runat="server" Visible="false"  >   </asp:DropDownList> 
                 </td>
                  <td  width="15%"></td>
           <td  width="10%">Status</td>
                  <td width="30%">
                   <asp:DropDownList ID="ddlstatus" CssClass="txtbox1" Width="150px" runat="server" Enabled="false"  >   
                  <asp:ListItem Value="SE">SelfApproved</asp:ListItem>                            
                   <asp:ListItem Value="LM">LineManager Approved</asp:ListItem>
                   <asp:ListItem Value="SM">Sr.Manager Approved</asp:ListItem>
                     <asp:ListItem Value="HR">HR Approved</asp:ListItem>                            
                   <asp:ListItem Value="DR">Draft</asp:ListItem>
                  </asp:DropDownList>
                  </td>         
            </tr>  
           <%--   <tr>
                 <td width="10%"></td>
                 <td>
                 </td>
                  <td  width="15%"></td>
                  <td  width="10%"></td>
                  <td width="30%">
                  <asp:CheckBox runat="server" id="ChkStatus" Text="Approved" ></asp:CheckBox>
                  </td>                
            </tr>--%>
          
                 
            </table>  
            </div>
           <div id="dvLoading"></div>

             <asp:Panel ID="paltab" runat="server" Width="100%" BorderColor="LightSteelBlue"  BorderWidth="2">
             <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
              <tr>
              <td valign="top">  
                     
               <ajx:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0"  CssClass="ajax__tab_yuitabview-theme" Width="100%" >
                  <ajx:TabPanel  ID="TabPanel1" runat="server" HeaderText="Business Objectives" >
                   <ContentTemplate>
                    <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content">                                   
                                  <tr>
                                     <td colspan="4">
                               
                           
                            <asp:GridView ID="grdBusinessView" runat="server" CellPadding="4" Showfooter="True" AllowPaging="True" CssClass="mGrid" HeaderStyle-Cssclass="GridBG" 
    PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" Width="100%" PageSize="10" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found">
                            <Columns>
                                <asp:TemplateField HeaderText="Code" Visible="false">
                                    <ItemTemplate>
                                        <div align="center"><asp:Label ID="lbbudocnum1" runat="server" Text='<%#Bind("U_Z_BussCode") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Business Objectives">
                                    <ItemTemplate>
                                            <div align="left">&nbsp;<asp:Label ID="lblbussname1" runat="server" Text='<%#Bind("U_Z_BussDesc") %>' ></asp:Label></div>
                                    </ItemTemplate>   
                                     <FooterTemplate>
                                              <div align="right"><asp:Label ID="lblpay4" runat="server" Text="Total(%)"></asp:Label>&nbsp;&nbsp;</div>
                                     </FooterTemplate>                                  
                                </asp:TemplateField>                                
                                 <asp:TemplateField HeaderText="Weight(%)">
                                    <ItemTemplate>
                                            <div align="right">&nbsp;<asp:Label ID="lblweight1" runat="server" Text='<%#Bind("U_Z_BussWeight") %>' ></asp:Label></div>
                                    </ItemTemplate>  
                                     <FooterTemplate>
                                            <div align="right"><asp:Label ID="lblBselfweight" runat="server" ></asp:Label>&nbsp;</div>
                                     </FooterTemplate>                                  
                                </asp:TemplateField>                                
                                  <asp:TemplateField HeaderText="Self Rating">
                                    <ItemTemplate>                                    
                                        <div align="left">&nbsp;<asp:DropDownList ID="ddlbusselfrate" runat="server" enabled="false"></asp:DropDownList></div>
                                    </ItemTemplate>                                                                       
                                </asp:TemplateField>   
                                  <asp:TemplateField HeaderText="Self Remarks">
                                    <ItemTemplate>
                                          <div align="left">&nbsp;
                                              <asp:Label ID="lblBSelfRemark" runat="server" Text='<%#Bind("U_Z_SelfRemark") %>' CssClass="txtbox" ></asp:Label>
                                          </div>
                                    </ItemTemplate>                                                                       
                                </asp:TemplateField>                          
                                 <asp:TemplateField HeaderText="Self Rating" visible="false" >
                                    <ItemTemplate>
                                        <div align="left">&nbsp;<asp:label ID="lblbusselfrate" runat="server" Text='<%#Bind("U_Z_SelfRaCode") %>' visible="false" ></asp:label></div>
                                        <div align="Right">&nbsp;<asp:Label ID="lblbusself1" runat="server" Text='<%#Bind("U_Z_BussSelfRate") %>' ></asp:Label></div>
                                    </ItemTemplate>  
                                    <FooterTemplate>
                                <div align="right"><asp:Label ID="lblBselfrate" runat="server" ></asp:Label>&nbsp;</div>
                            </FooterTemplate>                                   
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="First Level Manager Rating">
                                    <ItemTemplate>
                                          <div align="left">&nbsp;<asp:DropDownList ID="ddlbusmgrfrate" runat="server"  autopostback="true" OnSelectedIndexChanged="ddlbusmgrfrate_SelectedIndexChanged" EnableViewState="true"></asp:DropDownList></div>
                                    </ItemTemplate>                                                                       
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="First Level Manager Remarks">
                                    <ItemTemplate>
                                          <div align="left">&nbsp;
                                              <asp:TextBox ID="txtBfstmgrRemarks" runat="server" Text='<%#Bind("U_Z_MgrRemark") %>' CssClass="txtbox" Width="100"></asp:TextBox>
                                          </div>
                                    </ItemTemplate>                                                                       
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Line Manager Rating" visible="false" >
                                    <ItemTemplate>
                                       <div align="left">&nbsp;<asp:label ID="lblbusmgrfrate" runat="server" Text='<%#Bind("U_Z_MgrRaCode") %>' visible="false" ></asp:label></div>
                                       <div align="Right">&nbsp;<asp:Label ID="lblmgrrate1" runat="server" Text='<%#Bind("U_Z_BussMgrRate") %>' ></asp:Label></div>
                                    </ItemTemplate>  
                                    <FooterTemplate>
                                <div align="right"><asp:Label ID="lblBLinerate" runat="server" ></asp:Label>&nbsp;</div>
                            </FooterTemplate>                                   
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Second Level Manager Rating">
                                    <ItemTemplate>
                                       <div align="left">&nbsp;<asp:DropDownList ID="ddlsmgrfrate" runat="server" enabled="false"></asp:DropDownList></div>
                                    </ItemTemplate>                                                                       
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Second Level Manager Remarks">
                                    <ItemTemplate>
                                          <div align="left">&nbsp;
                                              <asp:Label ID="lblBSecoMgrRemark" runat="server" Text='<%#Bind("U_Z_SrRemark") %>' CssClass="txtbox" ></asp:Label>
                                          </div>
                                    </ItemTemplate>                                                                       
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Senior Manager Rating" visible="false" >
                                    <ItemTemplate>
                                     <div align="left">&nbsp;<asp:label ID="lblsmgrfrate" runat="server" Text='<%#Bind("U_Z_SMRaCode") %>' visible="false" ></asp:label></div>
                                       <div align="Right">&nbsp;<asp:Label ID="lblsrmgrrate1" runat="server" Text='<%#Bind("U_Z_BussSMRate") %>' ></asp:Label></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                <div align="right"><asp:Label ID="lblBsrrate" runat="server" ></asp:Label>&nbsp;</div>
                            </FooterTemplate>                                     
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code" Visible="false">
                                    <ItemTemplate>
                                        <div align="center">&nbsp;<asp:Label ID="lblbusCode1" runat="server" Text='<%#Bind("LineId") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>                                   
                                           
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" height="25px" BackColor="#CCCCCC"/>
                           </asp:GridView>
                                     </td>
                                     </tr>
                                      <tr>
                         <td>Self Remarks</td>
                         <td>
                       <asp:TextBox ID="txtsBremarks" CssClass="txtbox" Width="200px" TextMode="MultiLine" Height="40px" runat="server" enabled="false"></asp:TextBox>
                       </td>
                         <td>Sr.Manager Remarks</td>
                         <td>
                          <asp:TextBox ID="txtBSrremarks" CssClass="txtbox" Width="200px" TextMode="MultiLine" Height="40px" runat="server" enabled="false"></asp:TextBox>
                         </td>
                         </tr>
                           <tr>
                         <td>Line Manager Remarks</td>
                         <td>
                           <asp:TextBox ID="txtBLmremarks" CssClass="txtbox" Width="200px" TextMode="MultiLine" Height="40px" runat="server" ></asp:TextBox>
                         </td>
                         <td>HR Remarks</td>
                         <td>
                           <asp:TextBox ID="txtBCHrremarks" CssClass="txtbox" Width="200px" TextMode="MultiLine" Height="40px" runat="server" enabled="false"></asp:TextBox>
                         </td>
                         </tr>
                                  </table>
                  </ContentTemplate> 
                  </ajx:TabPanel>     
                    <ajx:TabPanel  ID="TabPanel2" runat="server" HeaderText="Personal Objectives" >
                    <ContentTemplate>
                     <table width="100%" border="0" cellspacing="0" cellpadding="4" class="main_content">                                   
                         <tr>
                         <td colspan="4">
                                                          
                           <asp:GridView ID="grdPeopleview" runat="server" CellPadding="4" Showfooter="True" AllowPaging="True" visible="false" CssClass="mGrid" HeaderStyle-Cssclass="GridBG" 
    PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" Width="100%" PageSize="10" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found" >
                            <Columns>
                                <asp:TemplateField HeaderText="Code" Visible="false">
                                    <ItemTemplate>
                                            <div align="center"><asp:Label ID="lbtpedocnum1" runat="server" Text='<%#Bind("U_Z_PeopleCode") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Personal Objectives">
                                    <ItemTemplate>
                                            <div align="left">&nbsp;<asp:Label ID="lblpeoname1" runat="server" Text='<%#Bind("U_Z_PeopleDesc") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField> 
                                   <asp:TemplateField HeaderText="Category">
                                    <ItemTemplate>
                                              <div align="left">&nbsp;<asp:Label ID="lblcate1" runat="server" Text='<%#Bind("U_Z_PeopleCat") %>' ></asp:Label></div>
                                    </ItemTemplate> 
                                    <FooterTemplate>
                                              <div align="right"><asp:Label ID="lblPPay" runat="server" Text="Total(%)"></asp:Label>&nbsp;&nbsp;</div>
                                    </FooterTemplate>                                       
                                </asp:TemplateField>                                   
                                 <asp:TemplateField HeaderText="Weight(%)">
                                    <ItemTemplate>
                                             <div align="Right">&nbsp;<asp:Label ID="lblpeweight1" runat="server" Text='<%#Bind("U_Z_PeoWeight") %>' ></asp:Label></div>
                                    </ItemTemplate>
                                     <FooterTemplate>
                                             <div align="right"><asp:Label ID="lblPselfweight" runat="server" ></asp:Label>&nbsp;</div>
                                     </FooterTemplate>                                                 
                                </asp:TemplateField>                                
                                 <asp:TemplateField HeaderText="Self Rating">
                                    <ItemTemplate>                                        
                                        <div align="left">&nbsp;<asp:DropDownList ID="ddlpeselfrate" runat="server" enabled="false"></asp:DropDownList></div>
                                    </ItemTemplate>                                                                       
                                </asp:TemplateField> 
                                 <asp:TemplateField HeaderText="Self Remarks">
                                    <ItemTemplate>
                                          <div align="left">&nbsp;
                                              <asp:Label ID="lblPSelfRemark" runat="server" Text='<%#Bind("U_Z_SelfRemark") %>' CssClass="txtbox" ></asp:Label>
                                          </div>
                                    </ItemTemplate>                                                                       
                                </asp:TemplateField>                                   
                                 <asp:TemplateField HeaderText="Self Rating" visible="false" >
                                    <ItemTemplate>
                                    <div align="left">&nbsp;<asp:label ID="lblpeselfrate" runat="server" Text='<%#Bind("U_Z_SelfRaCode") %>' visible="false" ></asp:label></div>
                                           <div align="right">&nbsp;<asp:Label ID="lblpeselfrate1" runat="server" Text='<%#Bind("U_Z_PeoSelfRate") %>' ></asp:Label></div>
                                    </ItemTemplate>    
                                     <FooterTemplate>
                                <div align="right"><asp:Label ID="lblPselfrate" runat="server" ></asp:Label>&nbsp;</div>
                            </FooterTemplate>                                    
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="First Level Manager Rating">
                                    <ItemTemplate>                                        
                                        <div align="left">&nbsp;<asp:DropDownList ID="ddlpesmgrfrate" runat="server" autopostback="true" OnSelectedIndexChanged="ddlpesmgrfrate_SelectedIndexChanged" EnableViewState="true"></asp:DropDownList></div>
                                    </ItemTemplate>                                                                       
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="First Level Manager Remarks">
                                    <ItemTemplate>
                                          <div align="left">&nbsp;
                                              <asp:TextBox ID="txtPfstmgrRemarks" runat="server" Text='<%#Bind("U_Z_MgrRemark") %>' CssClass="txtbox" Width="100"></asp:TextBox>
                                          </div>
                                    </ItemTemplate>                                                                       
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Line Manager Rating" visible="false" >
                                    <ItemTemplate>
                                    <div align="left">&nbsp;<asp:label ID="lblpesmgrfrate" runat="server" Text='<%#Bind("U_Z_MgrRaCode") %>' visible="false" ></asp:label></div>
                                       <div align="right">&nbsp;<asp:Label ID="lblpemgrrate1" runat="server" Text='<%#Bind("U_Z_PeoMgrRate") %>' ></asp:Label></div>
                                    </ItemTemplate>  
                                    <FooterTemplate>
                                            <div align="right"><asp:Label ID="lblPLinerate" runat="server" ></asp:Label>&nbsp;</div>
                                     </FooterTemplate>                                          
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Second Level Manager Rating">
                                    <ItemTemplate>                                       
                                        <div align="left">&nbsp;<asp:DropDownList ID="ddlsmgrrate" runat="server" enabled="false"></asp:DropDownList></div>
                                    </ItemTemplate>                                                                       
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Second Level Manager Remarks">
                                    <ItemTemplate>
                                          <div align="left">&nbsp;
                                              <asp:Label ID="lblPSecoMgrRemark" runat="server" Text='<%#Bind("U_Z_SrRemark") %>' CssClass="txtbox" ></asp:Label>
                                          </div>
                                    </ItemTemplate>                                                                       
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Senior Manager Rating" visible="false" >
                                    <ItemTemplate>
                                     <div align="left">&nbsp;<asp:label ID="lblsmgrrate" runat="server" Text='<%#Bind("U_Z_SMRaCode") %>' visible="false" ></asp:label></div>
                                       <div align="right">&nbsp;<asp:Label ID="lblsrmgrrate" runat="server" Text='<%#Bind("U_Z_PeoSMRate") %>' ></asp:Label></div>
                                    </ItemTemplate>    
                                      <FooterTemplate>
                                             <div align="right"><asp:Label ID="lblPsrrate" runat="server" ></asp:Label>&nbsp;</div>
                                      </FooterTemplate>                                 
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code" Visible="false">
                                    <ItemTemplate>
                                            <div align="center">&nbsp;<asp:Label ID="lblpecode1" runat="server" Text='<%#Bind("LineId") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>                                   
                                           
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" height="25px" BackColor="#CCCCCC"/>
                           </asp:GridView>
                                     </td>
                         </tr> 
                          <tr>
                         <td>Self Remarks</td>
                         <td>
                       <asp:TextBox ID="txtsPremarks" CssClass="txtbox" Width="200px" TextMode="MultiLine" Height="40px" runat="server" enabled="false"></asp:TextBox>
                       </td>
                          <td width="20%">Sr.Manager Remarks</td>
                         <td>
                          <asp:TextBox ID="txtPSrremarks" CssClass="txtbox" Width="200px" TextMode="MultiLine" Height="40px" runat="server" enabled="false"></asp:TextBox>
                         </td>
                         </tr>
                           <tr>
                         <td width="20%">Line Manager Remarks</td>
                         <td>
                           <asp:TextBox ID="txtPLmremarks" CssClass="txtbox" Width="200px" TextMode="MultiLine" Height="40px" runat="server" ></asp:TextBox>
                         </td>
                         <td>HR Remarks</td>
                         <td>
                           <asp:TextBox ID="txtPHrremarks" CssClass="txtbox" Width="200px" TextMode="MultiLine" Height="40px" runat="server" enabled="false"></asp:TextBox>
                         </td>
                         </tr>
                      </table>
                    </ContentTemplate>
                    </ajx:TabPanel>        
                    
                       <ajx:TabPanel  ID="TabPanel4" runat="server" HeaderText="Competences" >
                    <ContentTemplate>
                     <table width="100%" border="0" cellspacing="0" cellpadding="4" class="main_content">                                   
                         <tr>
                           <td colspan="4">
                                                         
                            <asp:GridView ID="grdCompetenceview" runat="server" CellPadding="4" AllowPaging="True" Showfooter="True" CssClass="mGrid"  HeaderStyle-Cssclass="GridBG"
    PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt"   AutoGenerateColumns="False" Width="100%" PageSize="10" visible="false" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found">
                            <Columns>
                                <asp:TemplateField HeaderText="Code" Visible="false">
                                    <ItemTemplate>
                                                <div align="center"><asp:Label ID="lbcomdocnum1" runat="server" Text='<%#Bind("U_Z_CompCode") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Competences Description">
                                    <ItemTemplate>
                                             <div align="left">&nbsp;<asp:Label ID="lblCompname1" runat="server" Text='<%#Bind("U_Z_CompDesc") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>  
                                  <asp:TemplateField HeaderText="Levels">
                                    <ItemTemplate>
                                      <div align="left">&nbsp;<asp:Label ID="lblCompLevel" runat="server" Text='<%#Bind("U_Z_CompLevel") %>' ></asp:Label></div>
                                    </ItemTemplate>    
                                                                   
                                </asp:TemplateField>   
                                  <asp:TemplateField HeaderText="Current Level">
                                    <ItemTemplate>
                                      <div align="left">&nbsp;<asp:Label ID="lblCurrLevel" runat="server" Text='<%#Bind("CurrentLevel") %>' ></asp:Label></div>
                                    </ItemTemplate>     
                                     <FooterTemplate>
                                              <div align="right"><asp:Label ID="lblCPay" runat="server" Text="Total(%)"></asp:Label>&nbsp;&nbsp;</div>
                                     </FooterTemplate>                                
                                </asp:TemplateField>                                 
                                 <asp:TemplateField HeaderText="Weight(%)">
                                    <ItemTemplate>
                                        <div align="Right">&nbsp;<asp:Label ID="lblCompWeight1" runat="server" Text='<%#Bind("U_Z_CompWeight") %>' ></asp:Label></div>
                                    </ItemTemplate>     
                                    <FooterTemplate>
                                         <div align="right"><asp:Label ID="lblCselfweight" runat="server" ></asp:Label>&nbsp;</div>
                                     </FooterTemplate>                                         
                                </asp:TemplateField>  
                                  <asp:TemplateField HeaderText="Self Rating">
                                    <ItemTemplate>                                        
                                        <div align="left">&nbsp;<asp:DropDownList ID="ddlcompselfrate" runat="server" enabled="false"></asp:DropDownList></div>
                                    </ItemTemplate>                                                                       
                                </asp:TemplateField>    
                                  <asp:TemplateField HeaderText="Self Remarks">
                                    <ItemTemplate>
                                          <div align="left">&nbsp;
                                              <asp:Label ID="lblCSelfRemark" runat="server" Text='<%#Bind("U_Z_SelfRemark") %>' CssClass="txtbox" ></asp:Label>
                                          </div>
                                    </ItemTemplate>                                                                       
                                </asp:TemplateField>                           
                                 <asp:TemplateField HeaderText="Self Rating" visible="false" >
                                    <ItemTemplate>
                                    <div align="left">&nbsp;<asp:label ID="lblcompselfrate" runat="server" Text='<%#Bind("U_Z_SelfRaCode") %>' visible="false" ></asp:label></div>
                                        <div align="Right" style="text-align:right;">&nbsp;<asp:Label ID="lblcompself1" runat="server" Text='<%#Bind("U_Z_CompSelfRate") %>' ></asp:Label></div>
                                    </ItemTemplate>   
                                    <FooterTemplate>
                                            <div align="right"><asp:Label ID="lblCselfrate" runat="server" ></asp:Label>&nbsp;</div>
                                     </FooterTemplate>                                    
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="First Level Manager Rating">
                                    <ItemTemplate>                                        
                                        <div align="left">&nbsp;<asp:DropDownList ID="ddlCompsmgrfrate" runat="server" autopostback="true" OnSelectedIndexChanged="ddlCompsmgrfrate_SelectedIndexChanged" EnableViewState="true" ></asp:DropDownList></div>
                                    </ItemTemplate>                                                                       
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="First Level Manager Remarks">
                                    <ItemTemplate>
                                          <div align="left">&nbsp;
                                              <asp:TextBox ID="txtCfstmgrRemarks" runat="server" Text='<%#Bind("U_Z_MgrRemark") %>' CssClass="txtbox" Width="100"></asp:TextBox>
                                          </div>
                                    </ItemTemplate>                                                                       
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Line Manager Rating" visible="false" >
                                    <ItemTemplate>
                                    <div align="left">&nbsp;<asp:label ID="lblCompsmgrfrate" runat="server" Text='<%#Bind("U_Z_MgrRaCode") %>' visible="false" ></asp:label></div>
                                       <div align="right">&nbsp;<asp:Label ID="lblcompmgrRate1" runat="server" Text='<%#Bind("U_Z_CompMgrRate") %>' ></asp:Label></div>
                                    </ItemTemplate>      
                                      <FooterTemplate>
                                            <div align="right"><asp:Label ID="lblCLinerate" runat="server" ></asp:Label>&nbsp;</div>
                                       </FooterTemplate>                                 
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Second Level Manager Rating">
                                    <ItemTemplate>                                       
                                        <div align="left">&nbsp;<asp:DropDownList ID="ddlcompsmgrrate" runat="server" enabled="false"></asp:DropDownList></div>
                                    </ItemTemplate>                                                                       
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Second Level Manager Remarks">
                                    <ItemTemplate>
                                          <div align="left">&nbsp;
                                              <asp:Label ID="lblCSecoMgrRemark" runat="server" Text='<%#Bind("U_Z_SrRemark") %>' CssClass="txtbox" ></asp:Label>
                                          </div>
                                    </ItemTemplate>                                                                       
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Senior Manager Rating" visible="false" >
                                    <ItemTemplate>
                                     <div align="left">&nbsp;<asp:label ID="lblcompsmgrrate" runat="server" Text='<%#Bind("U_Z_SMRaCode") %>' visible="false" ></asp:label></div>
                                       <div align="right">&nbsp;<asp:Label ID="lblsrmgrRate1" runat="server" Text='<%#Bind("U_Z_CompSMRate") %>' ></asp:Label></div>
                                    </ItemTemplate>     
                                     <FooterTemplate>
                                            <div align="right"><asp:Label ID="lblCsrrate" runat="server" ></asp:Label>&nbsp;</div>
                                      </FooterTemplate>                                   
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code" Visible="false">
                                    <ItemTemplate>
                                        <div align="center">&nbsp;<asp:Label ID="lblcompCode1" runat="server" Text='<%#Bind("LineId") %>' ></asp:Label></div>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>  
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" height="25px" BackColor="#CCCCCC"/>
                           </asp:GridView>
                             </td>
                         </tr> 
                         <tr>
                         <td>Self Remarks</td>
                         <td>
                       <asp:TextBox ID="txtsCremarks" CssClass="txtbox" Width="200px" TextMode="MultiLine" Height="40px" runat="server" enabled="false"></asp:TextBox>
                       </td>
                         <td width="20%">Sr.Manager Remarks</td>
                         <td>
                          <asp:TextBox ID="txtCSrremarks" CssClass="txtbox" Width="200px" TextMode="MultiLine" Height="40px" runat="server" enabled="false"></asp:TextBox>
                         </td>
                         </tr>
                           <tr>
                          <td width="20%">Line Manager Remarks</td>
                         <td>
                           <asp:TextBox ID="txtCLmremarks" CssClass="txtbox" Width="200px" TextMode="MultiLine" Height="40px" runat="server" ></asp:TextBox>
                         </td>
                         <td>HR Remarks</td>
                         <td>
                           <asp:TextBox ID="txtCHrremarks" CssClass="txtbox" Width="200px" TextMode="MultiLine" Height="40px" runat="server" enabled="false"></asp:TextBox>
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
           <table>
            <tr>
                <td colspan="5" align="center">
                <br />
                    <asp:Button ID="btnsubmit"  CssClass="btn"  runat="server" Text="Add" OnClientClick="return Confirmation();" />
                      <asp:Button ID="btnUpdate"  CssClass="btn"  runat="server" Text="Save" Visible="false" OnClientClick="return Confirmation();"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:Button ID="btnsavesubmit"  CssClass="btn"  runat="server" Text="Save&Submit" OnClientClick="return Confirmation1();" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnclose"  CssClass="btn" runat="server" Text="Cancel" />
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
