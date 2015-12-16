<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ESS/ESSMaster.Master"
    CodeBehind="ELoanRequest.aspx.vb" Inherits="HRMS.ELoanRequest" %>

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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <asp:Image ID="Image1" ImageUrl="../Images/waiting.gif" AlternateText="Processing"
                runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <ajx:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    <asp:UpdatePanel ID="Update" runat="server">
        <ContentTemplate>
            <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
                <tr>
                    <td>
                        <asp:TextBox ID="txtHEmpID" runat="server" Width="93px" Style="display: none"></asp:TextBox>
                        <asp:TextBox ID="txtpopunique" runat="server" Style="display: none"></asp:TextBox>
                        <asp:TextBox ID="txtpoptno" runat="server" Style="display: none"></asp:TextBox>
                        <asp:TextBox ID="txthidoption" runat="server" Style="display: none"></asp:TextBox>
                        <asp:TextBox ID="txttname" runat="server" Style="display: none"></asp:TextBox>
                        <input id="Btncallpop" runat="server" onserverclick="Btncallpop_ServerClick" style="display: none"
                            type="button" value="button" />
                    </td>
                </tr>
                <tr>
                    <td height="30" align="left" colspan="2" valign="bottom" background="images/h_bg.png"
                        style="border-bottom: 1px dotted; border-color: #f45501; background-repeat: repeat-x">
                        <div>
                            &nbsp;
                            <asp:Label ID="Label3" runat="server" Text="Loan Request" CssClass="subheader" Style="float: left;"></asp:Label>
                            <span>
                                <asp:Label ID="lblNewTrip" runat="server" Text="" Visible="false"></asp:Label></span>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
                            <tr>
                                <td>
                                    <asp:Panel ID="panelhome" runat="server" Width="100%">
                                        <asp:ImageButton ID="btnhome" runat="server" ImageUrl="~/images/Homeicon.jpg" PostBackUrl="~/ESS/ESSHome.aspx"
                                            ToolTip="Home" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="btnnew" runat="server" ImageUrl="~/images/Add.jpg" ToolTip="Add new record" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </asp:Panel>
                                    <asp:Label ID="Label2" runat="server" Text="" Style="color: Red;"></asp:Label>
                                    <asp:Panel ID="panelview" runat="server" Width="100%" BorderColor="LightSteelBlue"
                                        BorderWidth="2">
                                        <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
                                            <tr>
                                                <td valign="top">
                                                    <ajx:TabContainer ID="TabContainer2" runat="server" ActiveTabIndex="0" CssClass="ajax__tab_yuitabview-theme"
                                                        Width="100%">
                                                        <ajx:TabPanel ID="TabPanel3" runat="server" HeaderText="Loan Request">
                                                            <ContentTemplate>
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:GridView ID="grdLoanRequest" runat="server" CellPadding="4" AllowPaging="True"
                                                                                ShowHeaderWhenEmpty="true" CssClass="mGrid" HeaderStyle-CssClass="GridBG" PagerStyle-CssClass="pgr"
                                                                                AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false" Width="100%" PageSize="15">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="Request Code">
                                                                                        <ItemTemplate>
                                                                                            <div align="center">
                                                                                                <asp:LinkButton ID="lbtndocnum" runat="server" Text='<%#Bind("U_DocEntry") %>' OnClick="lbtndocnum_Click"></asp:LinkButton></div>
                                                                                            <%--OnClick="lbtndocnum_Click"--%>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Request Date">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &nbsp;<asp:Label ID="lblReqdate" runat="server" Text='<%#Bind("U_ReqDate") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Loan Code">
                                                                                        <ItemTemplate>
                                                                                            <div align="center">
                                                                                                <asp:Label ID="lbllonecode" runat="server" Text='<%#Bind("U_LoanCode") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Loan Name">
                                                                                        <ItemTemplate>
                                                                                            <div align="center">
                                                                                                <asp:Label ID="lblloaname" runat="server" Text='<%#Bind("U_LoanName") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Loan Amount">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &nbsp;<asp:Label ID="lblloanAmt" runat="server" Text='<%#Bind("U_LoanAmt") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Ref.No">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &nbsp;<asp:Label ID="lblrefno" runat="server" Text='<%#Bind("U_RefNo") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Loan Distribution Date">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &nbsp;<asp:Label ID="lblDisdate" runat="server" Text='<%#Bind("U_DisDate") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Loan Start Date">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &nbsp;<asp:Label ID="lblstdate" runat="server" Text='<%#Bind("U_InstDate") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="No of Installment">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &nbsp;<asp:Label ID="lblnoemi" runat="server" Text='<%#Bind("U_NoEMI") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Status">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &nbsp;<asp:Label ID="lblstatus" runat="server" Text='<%#Bind("U_Z_AppStatus") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Current Approver">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &#160;<asp:Label ID="lblcurapp" runat="server" Text='<%#Bind("U_CurApprover") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Next Approver">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &#160;<asp:Label ID="lblnxtapp" runat="server" Text='<%#Bind("U_NxtApprover") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Approval History">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &nbsp;<asp:LinkButton ID="lbtAppHistory" runat="server" Text="View" OnClick="lbtAppHistory_Click"></asp:LinkButton></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <HeaderStyle HorizontalAlign="Center" Height="25px" BackColor="#CCCCCC" />
                                                                            </asp:GridView>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </ajx:TabPanel>
                                                        <ajx:TabPanel ID="TabPanel1" runat="server" HeaderText="Loan Approved History">
                                                            <ContentTemplate>
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:GridView ID="grdSummary" runat="server" CellPadding="4" AllowPaging="True" ShowHeaderWhenEmpty="true"
                                                                                CssClass="mGrid" HeaderStyle-CssClass="GridBG" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                                                AutoGenerateColumns="false" Width="100%" PageSize="15">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="Loan Reschedule Details">
                                                                                        <ItemTemplate>
                                                                                            <div align="center">
                                                                                                <asp:Label ID="lbtsdocnum" runat="server" Text='<%#Bind("Code") %>' Visible="false"></asp:Label></div>
                                                                                            <asp:LinkButton ID="lbtResch" runat="server" Text="View" OnClick="lbtsdocnum_Click"></asp:LinkButton>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Emp.Code" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <div align="center">
                                                                                                <asp:Label ID="lblsEmpid" runat="server" Text='<%#Bind("U_Z_EmpID") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Emp.Name" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <div align="center">
                                                                                                <asp:Label ID="lblsEmpname" runat="server" Text='<%#Bind("EmpName") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Loan Name">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &#160;<asp:Label ID="lbllveName" runat="server" Text='<%#Bind("U_Z_LoanName") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Loan Type" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &#160;<asp:Label ID="lblsAgenda" runat="server" Text='<%#Bind("U_Z_LoanCode") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Loan Amount">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &#160;<asp:Label ID="lblsCouCode" runat="server" Text='<%#Bind("U_Z_LoanAmount") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Installment Amount">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &#160;<asp:Label ID="lblsCouName" runat="server" Text='<%#Bind("U_Z_EMIAmount") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="No.of Installment">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &#160;<asp:Label ID="lblsCouType" runat="server" Text='<%#Bind("U_Z_NoEMI") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Loan Distribution Date">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &#160;<asp:Label ID="lblsstdate" runat="server" Text='<%#Bind("U_Z_DisDate") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Installment Start Date">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &#160;<asp:Label ID="lblspaymonth" runat="server" Text='<%#Bind("U_Z_StartDate") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Installment End Date">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &#160;<asp:Label ID="lblspayyear" runat="server" Text='<%#Bind("U_Z_EndDate") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Paid Installment">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &#160;<asp:Label ID="lblsAppreq" runat="server" Text='<%#Bind("U_Z_PaidEMI") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Balance Installment">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &#160;<asp:Label ID="lblsreqdt" runat="server" Text='<%#Bind("U_Z_Balance") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="G/L Account">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &#160;<asp:Label ID="lblsreqtime" runat="server" Text='<%#Bind("U_Z_GLACC") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Approval Status">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &#160;<asp:Label ID="lblsAppStatus" runat="server" Text='<%#Bind("U_Z_Status") %>'></asp:Label></div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <HeaderStyle HorizontalAlign="Center" Height="25px" BackColor="#CCCCCC" />
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
                                    <asp:Panel ID="PanelNewRequest" runat="server" Width="100%" BorderColor="LightSteelBlue"
                                        BorderWidth="2">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content">
                                            <tr>
                                                <td>
                                                    Request Date
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtReqdate" CssClass="txtbox" runat="server" Enabled="false"></asp:TextBox>
                                                    <ajx:CalendarExtender ID="CalendarExtender2" Animated="true" Format="dd/MM/yyyy"
                                                        runat="server" TargetControlID="txtReqdate" CssClass="cal_Theme1">
                                                    </ajx:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Loan Type
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlloancode" CssClass="txtbox1" runat="server" Visible="false">
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="txtloanName" CssClass="txtbox" Width="150px" runat="server" Enabled="False"></asp:TextBox>
                                                    <asp:ImageButton ID="btnfindleave" runat="server" Text="Find" ImageUrl="~/images/search.jpg" />
                                                    <ajx:ModalPopupExtender ID="ModalPopupExtender7" runat="server" DropShadow="True"
                                                        PopupControlID="Panelpoptechnician" TargetControlID="btnfindleave" CancelControlID="btnclstech"
                                                        BackgroundCssClass="modalBackground">
                                                    </ajx:ModalPopupExtender>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtcode" CssClass="txtbox" runat="server" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="txtlvecode" CssClass="txtbox" runat="server" Visible="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Loan Amount
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtloanAmt" CssClass="txtbox" runat="server" onkeypress="AllowNumbers(this);checkDec(this);RemoveZero(this);"
                                                        onkeyup="AllowNumbers(this);checkDec(this);RemoveZero(this);"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Loan Distribution Date
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtLodisDate" CssClass="txtbox" runat="server"></asp:TextBox>
                                                    <ajx:CalendarExtender ID="CalendarExtender1" Animated="true" Format="dd/MM/yyyy"
                                                        runat="server" TargetControlID="txtLodisDate" CssClass="cal_Theme1">
                                                    </ajx:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Loan Start Date
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtLoStdate" CssClass="txtbox" runat="server"></asp:TextBox>
                                                    <ajx:CalendarExtender ID="CalendarExtender3" Animated="true" Format="dd/MM/yyyy"
                                                        runat="server" TargetControlID="txtLoStdate" CssClass="cal_Theme1">
                                                    </ajx:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    No.of Installment
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtnoEMI" CssClass="txtbox" runat="server" onkeypress="AllowNumbers(this);checkDec(this);RemoveZero(this);"
                                                        onkeyup="AllowNumbers(this);checkDec(this);RemoveZero(this);"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                            <td></td>
                                                <td align="left">
                                                    <br />
                                                    <asp:Button ID="btnAdd" CssClass="btn" Width="85px" runat="server" Text="Save" OnClientClick="return Confirmation();" />
                                                    <asp:Button ID="btncancel" CssClass="btn" Width="85px" runat="server" Text="Cancel" />
                                                    <asp:Button ID="btnWithdraw" CssClass="btn" runat="server" Text="WithDraw Request"
                                                        Visible="false" OnClientClick="return Confirmation1();" />
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
                        <asp:Panel ID="Panelpoptechnician" runat="server" BackColor="White" Style="display: none;
                            padding: 10px; width: 500px;">
                            <div>
                                <span class="sideheading" style="color: Green;">Loan Details</span> <span style="float: right;">
                                    <asp:Button ID="btnclstech" runat="server" CssClass="btn" Width="30px" Text="X" /></span></div>
                            <br />
                            <div>
                                <span>
                                    <asp:Label ID="lblname1" runat="server" Text="Loan Code"></asp:Label></span>
                                <asp:TextBox ID="txtpoptraincode" runat="server"></asp:TextBox><br />
                                <span>
                                    <asp:Label ID="lblname2" runat="server" Text="Loan Name"></asp:Label></span>
                                <asp:TextBox ID="txtpopcouname" runat="server"></asp:TextBox>
                                <asp:Button ID="btnpopemp1" runat="server" Text="Go" CssClass="btn" Width="65px" />
                                <asp:LinkButton runat="server" ID="LnkViewall">View All</asp:LinkButton>
                                <br />
                            </div>
                            <br />
                            <asp:Panel ID="Panel4" runat="server" Height="200px" ScrollBars="Vertical">
                                <asp:GridView ID="grdLoneType" runat="server" CellPadding="4" RowStyle-CssClass="mousecursor"
                                    ShowHeaderWhenEmpty="true" EmptyDataText="No records Found" CssClass="mGrid"
                                    HeaderStyle-CssClass="GridBG" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                    AutoGenerateColumns="false" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Loan Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblloancode" runat="server" Text='<%#Bind("Code") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Loan Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lbllonename" runat="server" Text='<%#Bind("Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="G/L Account">
                                            <ItemTemplate>
                                                <asp:Label ID="lblglacc" runat="server" Text='<%#Bind("U_Z_GLACC") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="Gray" HorizontalAlign="Center" ForeColor="White" Height="25px" />
                                    <RowStyle HorizontalAlign="Center" />
                                    <AlternatingRowStyle HorizontalAlign="Center" />
                                </asp:GridView>
                            </asp:Panel>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="visibility: hidden">
                            <asp:Button ID="btnSample" runat="server" />
                        </div>
                        <ajx:ModalPopupExtender ID="ModalPopupExtender1" runat="server" DropShadow="True"
                            PopupControlID="Panelpoptechnician1" TargetControlID="btnSample" CancelControlID="btnclstech1"
                            BackgroundCssClass="modalBackground">
                        </ajx:ModalPopupExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="Panelpoptechnician1" runat="server" BackColor="White" Style="display: none;
                            padding: 10px; width: 900px;">
                            <div>
                                <span class="sideheading" style="color: Green;">Approval History Details</span>
                                <span style="float: right;">
                                    <asp:Button ID="btnclstech1" runat="server" CssClass="btn" Width="30px" Text="X" /></span></div>
                            <br />
                            <br />
                            <asp:Panel ID="Panel2" runat="server" Height="400px">
                                <asp:Label ID="Label1" runat="server" Text="" CssClass="txtbox" ForeColor="Red"></asp:Label>
                                <asp:GridView ID="grdApprovalHis" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt"
                                    AutoGenerateColumns="false" CellPadding="4" CssClass="mGrid" EmptyDataText="No Records Found"
                                    HeaderStyle-CssClass="GridBG" PagerStyle-CssClass="pgr" PageSize="10" ShowHeaderWhenEmpty="true"
                                    Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Request Code" Visible="false">
                                            <ItemTemplate>
                                                <div align="center">
                                                    <asp:Label ID="lblHDocNo" runat="server" Text='<%#Bind("DocEntry") %>'>
                                                    </asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reference No" Visible="false">
                                            <ItemTemplate>
                                                <div align="center">
                                                    <asp:Label ID="lblhrefno" runat="server" Text='<%#Bind("U_Z_DocEntry") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DocType" Visible="false">
                                            <ItemTemplate>
                                                <div align="center">
                                                    <asp:Label ID="lblhdoctype" runat="server" Text='<%#Bind("U_Z_DocType") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Emp.ID">
                                            <ItemTemplate>
                                                <div align="left">
                                                    &#160;<asp:Label ID="lblhempid" runat="server" Text='<%#Bind("U_Z_EmpId") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Emp.Name">
                                            <ItemTemplate>
                                                <div align="left">
                                                    &#160;<asp:Label ID="lblhempname" runat="server" Text='<%#Bind("U_Z_EmpName") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Loan Type" Visible="false">
                                            <ItemTemplate>
                                                <div align="left">
                                                    &#160;<asp:Label ID="lblhLoanType" runat="server" Text='<%#Bind("U_Z_LoanCode") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Loan Amount">
                                            <ItemTemplate>
                                                <div align="left">
                                                    &#160;<asp:Label ID="lblhLoanAmt" runat="server" Text='<%#Bind("U_Z_LoanAmount") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Loan Dispatced Date">
                                            <ItemTemplate>
                                                <div align="left">
                                                    &#160;<asp:Label ID="lblhLoanDisDt" runat="server" Text='<%#Bind("U_Z_DisDate") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Loan Start Date">
                                            <ItemTemplate>
                                                <div align="left">
                                                    &#160;<asp:Label ID="lblhLoanStDt" runat="server" Text='<%#Bind("U_Z_StartDate") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Number of repayment">
                                            <ItemTemplate>
                                                <div align="left">
                                                    &#160;<asp:Label ID="lblhNoEmi" runat="server" Text='<%#Bind("U_Z_NoEMI") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Approved By">
                                            <ItemTemplate>
                                                <div align="left">
                                                    &#160;<asp:Label ID="lblhAppby" runat="server" Text='<%#Bind("U_Z_ApproveBy") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Next Approver">
                                            <ItemTemplate>
                                                <div align="center">
                                                    <asp:Label ID="lblhapprover" runat="server" Text='<%#Bind("U_Z_NxtApprover") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Create Date">
                                            <ItemTemplate>
                                                <div align="left">
                                                    &#160;<asp:Label ID="lblhcrdate" runat="server" Text='<%#Bind("CreateDate") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Create Time">
                                            <ItemTemplate>
                                                <div align="left">
                                                    &#160;<asp:Label ID="lblhcrtime" runat="server" Text='<%#Bind("CreateTime") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Update Date">
                                            <ItemTemplate>
                                                <div align="left">
                                                    &#160;<asp:Label ID="lblhupdate" runat="server" Text='<%#Bind("UpdateDate") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Update Time">
                                            <ItemTemplate>
                                                <div align="left">
                                                    &#160;<asp:Label ID="lblhuptime" runat="server" Text='<%#Bind("UpdateTime") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Approved Status">
                                            <ItemTemplate>
                                                <div align="left">
                                                    &#160;<asp:Label ID="lblhAppstatus" runat="server" Text='<%#Bind("U_Z_AppStatus") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <div align="left">
                                                    &#160;<asp:Label ID="lblhremarks" runat="server" Text='<%#Bind("U_Z_Remarks") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="#CCCCCC" Height="25px" HorizontalAlign="Center" />
                                </asp:GridView>
                            </asp:Panel>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="visibility: hidden">
                            <asp:Button ID="Button1" runat="server" />
                        </div>
                        <ajx:ModalPopupExtender ID="ModalPopupExtender2" runat="server" DropShadow="True"
                            PopupControlID="Panelpoptechnician2" TargetControlID="Button1" CancelControlID="btnclstech1"
                            BackgroundCssClass="modalBackground">
                        </ajx:ModalPopupExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="Panelpoptechnician2" runat="server" BackColor="White" Style="display: none;
                            padding: 10px; width: 900px;">
                            <div>
                                <span class="sideheading" style="color: Green;">Loan ReScheduled Details</span>
                                <span style="float: right;">
                                    <asp:Button ID="btnclstech2" runat="server" CssClass="btn" Width="30px" Text="X" /></span></div>
                            <br />
                            <br />
                            <asp:Panel ID="Panel3" runat="server" Height="400px" ScrollBars="Vertical">
                                <asp:Label ID="Label4" runat="server" Text="" CssClass="txtbox" ForeColor="Red"></asp:Label>
                                <asp:GridView ID="GrdScheduled" runat="server" AlternatingRowStyle-CssClass="alt"
                                    AutoGenerateColumns="false" CellPadding="4" CssClass="mGrid" EmptyDataText="No Records Found"
                                    HeaderStyle-CssClass="GridBG" PagerStyle-CssClass="pgr" ShowHeaderWhenEmpty="true"
                                    Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Request Code" Visible="false">
                                            <ItemTemplate>
                                                <div align="center">
                                                    <asp:Label ID="lblsHDocNo" runat="server" Text='<%#Bind("Code") %>'>
                                                    </asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reference No" Visible="false">
                                            <ItemTemplate>
                                                <div align="center">
                                                    <asp:Label ID="lblshrefno" runat="server" Text='<%#Bind("U_Z_TrnsRefCode") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Emp.ID" Visible="false">
                                            <ItemTemplate>
                                                <div align="left">
                                                    &#160;<asp:Label ID="lblshempid" runat="server" Text='<%#Bind("U_Z_EmpID") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Emp.Name" Visible="false">
                                            <ItemTemplate>
                                                <div align="left">
                                                    &#160;<asp:Label ID="lblshempname" runat="server" Text='<%#Bind("U_Z_LoanCode") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Loan Type">
                                            <ItemTemplate>
                                                <div align="left">
                                                    &#160;<asp:Label ID="lblshLoanType" runat="server" Text='<%#Bind("U_Z_LoanName") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Loan Amount">
                                            <ItemTemplate>
                                                <div align="left">
                                                    &#160;<asp:Label ID="lblshLoanAmt" runat="server" Text='<%#Bind("U_Z_LoanAmount") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Due Date">
                                            <ItemTemplate>
                                                <div align="left">
                                                    &#160;<asp:Label ID="lblhLoanDisDt" runat="server" Text='<%#Bind("U_Z_DueDate") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="OutStanding">
                                            <ItemTemplate>
                                                <div align="left">
                                                    &#160;<asp:Label ID="lblshLoanStDt" runat="server" Text='<%#Bind("U_Z_OB") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Installment Amount">
                                            <ItemTemplate>
                                                <div align="left">
                                                    &#160;<asp:Label ID="lblshNoEmi" runat="server" Text='<%#Bind("U_Z_EMIAmount") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cash Paid">
                                            <ItemTemplate>
                                                <div align="left">
                                                    &#160;<asp:Label ID="lblshAppby" runat="server" Text='<%#Bind("U_Z_CashPaid") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Stop Installment">
                                            <ItemTemplate>
                                                <div align="left">
                                                    &#160;<asp:Label ID="lblshcrdate" runat="server" Text='<%#Bind("U_Z_StopIns") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cash PaidDate">
                                            <ItemTemplate>
                                                <div align="left">
                                                    &#160;<asp:Label ID="lblshcrtime" runat="server" Text='<%#Bind("U_Z_CashPaidDate") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Balance">
                                            <ItemTemplate>
                                                <div align="left">
                                                    &#160;<asp:Label ID="lblshupdate" runat="server" Text='<%#Bind("U_Z_Balance") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Paid Status">
                                            <ItemTemplate>
                                                <div align="left">
                                                    &#160;<asp:Label ID="lblshuptime" runat="server" Text='<%#Bind("U_Z_Status") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Paid Month">
                                            <ItemTemplate>
                                                <div align="left">
                                                    &#160;<asp:Label ID="lblshAppstatus" runat="server" Text='<%#Bind("U_Z_Month") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Paid year">
                                            <ItemTemplate>
                                                <div align="left">
                                                    &#160;<asp:Label ID="lblshremarks" runat="server" Text='<%#Bind("U_Z_Year") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="#CCCCCC" Height="25px" HorizontalAlign="Center" />
                                </asp:GridView>
                            </asp:Panel>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
