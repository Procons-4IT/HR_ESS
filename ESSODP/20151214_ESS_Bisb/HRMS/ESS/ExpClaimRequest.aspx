<%@ Page Title="Expenses Claim Request" Language="vb" AutoEventWireup="false" MasterPageFile="~/ESS/ESSMaster.Master"
    CodeBehind="ExpClaimRequest.aspx.vb" Inherits="HRMS.ExpClaimRequest" %>

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
            //            el.value = el.value.replace(/^[ 0]+/, '');
            var ex = /^\d*\.?\d{0,2}$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
            }
        }

        function RemoveZero(el) {
            //            el.value = el.value.replace(/^[ ]+/, '');
        }

        function popupdisplay(option, uniqueid, DocNo) {
            if (uniqueid.length > 0) {
                var uniid = document.getElementById("<%=txtpopunique.ClientID%>").value;
                var tno = document.getElementById("<%=txtpoptno.ClientID%>").value;
                var opt = document.getElementById("<%=txthidoption.ClientID%>").value;
                uniid = ""; tno = ""; opt = "";
                if (uniid != uniqueid && tno != DocNo && opt != option) {
                    document.getElementById("<%=txtpopunique.ClientID%>").value = uniqueid;
                    document.getElementById("<%=txtpoptno.ClientID%>").value = DocNo;
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="conditional">
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
                            <asp:Label ID="Label3" runat="server" Text="Expenses Claim Request" CssClass="subheader"
                                Style="float: left;"></asp:Label>
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
                                                        <ajx:TabPanel ID="TabPanel3" runat="server" HeaderText="Expenses Claim Request">
                                                            <ContentTemplate>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:GridView ID="grdExpClaimRequest" runat="server" CellPadding="4" AllowPaging="True"
                                                                                ShowHeaderWhenEmpty="true" CssClass="mGrid" HeaderStyle-CssClass="GridBG" PagerStyle-CssClass="pgr"
                                                                                AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false" Width="100%" PageSize="15">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="Expenses Claim  Number">
                                                                                        <ItemTemplate>
                                                                                            <div align="center">
                                                                                                <asp:LinkButton ID="lbtndocnum" runat="server" Text='<%#Bind("Code") %>' OnClick="lbtndocnum_Click"> <%--onclick="lbtndocnum_Click"--%>
                                                                                                </asp:LinkButton>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Employee T&A No" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <div align="center">
                                                                                                <asp:Label ID="lblgtano" runat="server" Text='<%#Bind("U_Z_TAEmpID") %>'></asp:Label>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Employee ID" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <div align="center">
                                                                                                <asp:Label ID="lblgempid" runat="server" Text='<%#Bind("U_Z_EmpID") %>'></asp:Label>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Employee Name">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &nbsp;<asp:Label ID="lblgempname" runat="server" Text='<%#Bind("U_Z_EmpName") %>'></asp:Label>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Submitted Date">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &nbsp;<asp:Label ID="lblgsubdate" runat="server" Text='<%#Bind("U_Z_Subdt") %>'></asp:Label>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Client">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &nbsp;<asp:Label ID="lblgClient" runat="server" Text='<%#Bind("U_Z_Client") %>'></asp:Label>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Project">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &nbsp;<asp:Label ID="lblgProject" runat="server" Text='<%#Bind("U_Z_Project") %>'></asp:Label>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Document Status">
                                                                                        <ItemTemplate>
                                                                                            <div align="left">
                                                                                                &nbsp;<asp:Label ID="lbldocStatus" runat="server" Text='<%#Bind("U_Z_DocStatus") %>'></asp:Label>
                                                                                            </div>
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
                                        <div id="Div1" runat="server" class="DivCorner" style="border: solid 2px LightSteelBlue;
                                            width: 100%;">
                                            <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
                                                <tr>
                                                    <td width="10%">
                                                        Document Number
                                                    </td>
                                                    <td width="15%">
                                                        <asp:Label ID="lbldocno" CssClass="txtbox" Width="200px" runat="server"></asp:Label>
                                                    </td>
                                                    <td width="5%">
                                                    </td>
                                                    <td width="10%">
                                                        Date Submitted
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblsubdt" CssClass="txtbox" Width="200px" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="10%">
                                                        Employee No
                                                    </td>
                                                    <td width="10%">
                                                        <asp:Label ID="lblempNo" CssClass="txtbox" runat="server"></asp:Label>
                                                    </td>
                                                    <td width="5%">
                                                    </td>
                                                    <td width="10%">
                                                        Employee Name
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblempname" CssClass="txtbox" runat="server" Width="150px"></asp:Label>
                                                        <asp:Label ID="lblCardCode" CssClass="txtbox" runat="server" Visible="false"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Trip Type
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddltriptype" runat="server" CssClass="txtbox1" AutoPostBack="true">
                                                            <asp:ListItem Value="-">---Select---</asp:ListItem>
                                                            <asp:ListItem Value="N">Without Travel</asp:ListItem>
                                                            <asp:ListItem Value="E">With Travel</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td width="5%">
                                                    </td>
                                                    <td>
                                                        Travel Description (*)
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txttravelCode" CssClass="txtbox" runat="server" Visible="False"></asp:TextBox>
                                                        <asp:TextBox ID="txttraveldesc" CssClass="txtbox" runat="server" Enabled="False"></asp:TextBox>
                                                        <asp:ImageButton ID="btnfindtrcode" runat="server" Text="Find" ImageUrl="~/images/search.jpg" />
                                                        <ajx:ModalPopupExtender ID="ModalPopupExtender7" runat="server" DropShadow="True"
                                                            PopupControlID="Panelpoptechnician" TargetControlID="btnfindtrcode" CancelControlID="btnclstech"
                                                            BackgroundCssClass="modalBackground">
                                                        </ajx:ModalPopupExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Client
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtClient" CssClass="txtbox" Width="150px" EnableViewState="true"
                                                            runat="server"></asp:TextBox>
                                                    </td>
                                                    <td width="5%">
                                                    </td>
                                                    <td width="10%">
                                                        Project
                                                    </td>
                                                    <td width="10%">
                                                        <asp:TextBox ID="txtProject" CssClass="txtbox" Width="150px" EnableViewState="true"
                                                            runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="10%">
                                                        Badge No
                                                    </td>
                                                    <td width="10%">
                                                        <asp:Label ID="lblTANo" CssClass="txtbox" runat="server"></asp:Label>
                                                    </td>
                                                    <td width="5%">
                                                    </td>
                                                    <td width="10%">
                                                        Document Status
                                                    </td>
                                                    <td width="10%">
                                                        <asp:DropDownList ID="ddlDocStatus" runat="server" CssClass="txtbox1" Enabled="false">
                                                            <asp:ListItem Value="O">Opened</asp:ListItem>
                                                            <asp:ListItem Value="C">Closed</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <asp:Panel ID="paltab" runat="server" Width="98%">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="4" class="main_content">
                                                <tr>
                                                    <td valign="top">
                                                        <ajx:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" CssClass="ajax__tab_yuitabview-theme"
                                                            Width="100%">
                                                            <ajx:TabPanel ID="TabPanel5" runat="server" HeaderText="Expenses Claim Request">
                                                                <ContentTemplate>
                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="4" class="main_content">
                                                                        <tr>
                                                                            <td id="NewExpense" runat="server">
                                                                                <asp:Button ID="lbtnNewExpenses" runat="server" CssClass="button" Text="Add New Expenses"
                                                                                    Style="float: left;" />
                                                                                <asp:Label ID="lblerror" runat="server" ForeColor="Red" CssClass="txtbox" Width="300px"></asp:Label>
                                                                                <ajx:ModalPopupExtender ID="ModalPopupExtender6" runat="server" DropShadow="false"
                                                                                    PopupControlID="PanelItemEntry" TargetControlID="lbtnNewExpenses" CancelControlID="btnclstech2"
                                                                                    BackgroundCssClass="modalBackground">
                                                                                </ajx:ModalPopupExtender>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:GridView ID="grdRequestExpenses" runat="server" CellPadding="4" RowStyle-CssClass="mousecursor"
                                                                                    CssClass="mGrid" HeaderStyle-CssClass="GridBG" AllowPaging="true" PageSize="15"
                                                                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="mousecursor" AutoGenerateColumns="false"
                                                                                    Width="100%" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found" ShowFooter="true">
                                                                                    <Columns>
                                                                                        <asp:CommandField DeleteText="Delete" ShowDeleteButton="true" ButtonType="Link" />
                                                                                        <asp:TemplateField HeaderText="Ref. Code" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <div align="center">
                                                                                                    <asp:Label ID="lblERefCode" runat="server" Text='<%#Bind("U_Code") %>'>
                                                                                                    </asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Request Code" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <div align="center">
                                                                                                    <asp:Label ID="lblECode" runat="server" Text='<%#Bind("U_DocEntry") %>'>
                                                                                                    </asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Trip Type" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <div align="center">
                                                                                                    <asp:Label ID="lblETripType" runat="server" Text='<%#Bind("U_TripType") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Transaction Date">
                                                                                            <ItemTemplate>
                                                                                                <div align="center">
                                                                                                    <asp:Label ID="lblEcldate" runat="server" Text='<%#Bind("U_ClimDate") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Travel Code" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &nbsp;<asp:Label ID="lblETraCode" runat="server" Text='<%#Bind("U_TraCode") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Travel Description" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &nbsp;<asp:Label ID="lblETraName" runat="server" Text='<%#Bind("U_TraDesc") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Expense Type">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &nbsp;<asp:Label ID="lblEexptype" runat="server" Text='<%#Bind("U_ExpName") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                      <asp:TemplateField HeaderText="Transaction Currency">
                                                                                            <ItemTemplate>
                                                                                                <div align="right">
                                                                                                    &nbsp;<asp:Label ID="lblETraCur" runat="server" Text='<%#Bind("U_Currency") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                            <FooterTemplate>
                                                                                                <div align="right">
                                                                                                    <asp:Label ID="lblCur" runat="server" Text="Total :"></asp:Label>&nbsp;</div>
                                                                                            </FooterTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Transaction Amount">
                                                                                            <ItemTemplate>
                                                                                                <div align="right">
                                                                                                    &nbsp;<asp:Label ID="lblEtramt" runat="server" Text='<%#Bind("U_CurAmt") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                            <FooterTemplate>
                                                                                                <div align="right">
                                                                                                    <asp:Label ID="lblCurTotal" runat="server"></asp:Label>&nbsp;</div>
                                                                                            </FooterTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Exchange Rate">
                                                                                            <ItemTemplate>
                                                                                                <div align="right">
                                                                                                    &nbsp;<asp:Label ID="lblEexrate" runat="server" Text='<%#Bind("U_ExcRate") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Local Currency Amount">
                                                                                            <ItemTemplate>
                                                                                                <div align="right">
                                                                                                    &nbsp;<asp:Label ID="lblELocCur" runat="server" Text='<%#Bind("U_UsdAmt") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                            <FooterTemplate>
                                                                                                <div align="right">
                                                                                                    <asp:Label ID="lblLocCurTotal" runat="server"></asp:Label>&nbsp;</div>
                                                                                            </FooterTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="To be Reimbursed?">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &nbsp;<asp:Label ID="lblEreimb" runat="server" Text='<%#Bind("U_ReImbused") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Reimbursement Amount">
                                                                                            <ItemTemplate>
                                                                                                <div align="right">
                                                                                                    &nbsp;<asp:Label ID="lblEreimbamt" runat="server" Text='<%#Bind("U_ReImAmt") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>                                                                                            
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Notes">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &nbsp;<asp:Label ID="lblEreason" runat="server" Text='<%#Bind("U_Notes") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Attachment">
                                                                                            <ItemTemplate>
                                                                                                <div align="center">
                                                                                                    <asp:LinkButton ID="lnkEDownload" Text="Download" CommandArgument='<%# Eval("U_Attachment") %>'
                                                                                                        runat="server" Width="100px" OnClick="lnkEDownload_Click"></asp:LinkButton>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Approval Status">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &nbsp;<asp:Label ID="lblEstatus" runat="server" Text='<%#Bind("U_AppStatus") %>'></asp:Label>
                                                                                                </div>
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
                                                                                    </Columns>
                                                                                    <HeaderStyle BackColor="Gray" HorizontalAlign="Center" ForeColor="White" Height="25px" />
                                                                                    <RowStyle HorizontalAlign="Left" />
                                                                                    <AlternatingRowStyle HorizontalAlign="Left" />
                                                                                     <FooterStyle Height="25px" BackColor="#CCCCCC" Font-Bold="true" />
                                                                                </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                        <td>
                                                                            <br />
                                                                            <asp:Button ID="btnSubmit" CssClass="btn" Width="85px" runat="server" Text="Submit"
                                                                                OnClientClick="return Confirmation();" />
                                                                            <asp:Button ID="btnClose" CssClass="btn" Width="85px" runat="server" Text="Cancel" />
                                                                        </td>
                                                                    </table>
                                                                </ContentTemplate>
                                                            </ajx:TabPanel>
                                                            <ajx:TabPanel ID="TabPanel1" runat="server" HeaderText="Approved Expenses Claim">
                                                                <ContentTemplate>
                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="4" class="main_content">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:GridView ID="grdApproved" runat="server" CellPadding="4" RowStyle-CssClass="mousecursor"
                                                                                    AllowPaging="true" PageSize="15" CssClass="mGrid" HeaderStyle-CssClass="GridBG"
                                                                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="mousecursor" AutoGenerateColumns="false"
                                                                                    Width="100%" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found" ShowFooter="true">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Request Code">
                                                                                            <ItemTemplate>
                                                                                                <div align="center">
                                                                                                    <asp:Label ID="lblCode" runat="server" Text='<%#Bind("Code") %>'>
                                                                                                    </asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Trip Type" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <div align="center">
                                                                                                    <asp:Label ID="lblTripType" runat="server" Text='<%#Bind("U_Z_TripType") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Transaction Date">
                                                                                            <ItemTemplate>
                                                                                                <div align="center">
                                                                                                    <asp:Label ID="lblcldate" runat="server" Text='<%#Bind("U_Z_Claimdt") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Travel Code" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &nbsp;<asp:Label ID="lblTraCode" runat="server" Text='<%#Bind("U_Z_TraCode") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Travel Description" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &nbsp;<asp:Label ID="lblTraName" runat="server" Text='<%#Bind("U_Z_TraDesc") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Expense Type">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &nbsp;<asp:Label ID="lblexptype" runat="server" Text='<%#Bind("U_Z_ExpType") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                       <asp:TemplateField HeaderText="Transaction Currency">
                                                                                            <ItemTemplate>
                                                                                                <div align="right">
                                                                                                    &nbsp;<asp:Label ID="lblTraCur" runat="server" Text='<%#Bind("U_Z_Currency") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                              <FooterTemplate>
                                                                                                <div align="right">
                                                                                                    <asp:Label ID="lblACur" runat="server" Text="Total :"></asp:Label>&nbsp;</div>
                                                                                            </FooterTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Transaction Amount">
                                                                                            <ItemTemplate>
                                                                                                <div align="right">
                                                                                                    &nbsp;<asp:Label ID="lbltramt" runat="server" Text='<%#Bind("U_Z_CurAmt") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                              <FooterTemplate>
                                                                                                <div align="right">
                                                                                                    <asp:Label ID="lblACurTotal" runat="server"></asp:Label>&nbsp;</div>
                                                                                            </FooterTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Exchange Rate">
                                                                                            <ItemTemplate>
                                                                                                <div align="right">
                                                                                                    &nbsp;<asp:Label ID="lblexrate" runat="server" Text='<%#Bind("U_Z_ExcRate") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Local Currency Amount">
                                                                                            <ItemTemplate>
                                                                                                <div align="right">
                                                                                                    &nbsp;<asp:Label ID="lblLocCur" runat="server" Text='<%#Bind("U_Z_UsdAmt") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                              <FooterTemplate>
                                                                                                <div align="right">
                                                                                                    <asp:Label ID="lblALocCurTotal" runat="server"></asp:Label>&nbsp;</div>
                                                                                            </FooterTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="To be Reimbursed?">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &nbsp;<asp:Label ID="lblreimb" runat="server" Text='<%#Bind("U_Z_Reimburse") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Reimbursement Amount">
                                                                                            <ItemTemplate>
                                                                                                <div align="right">
                                                                                                    &nbsp;<asp:Label ID="lblreimbamt" runat="server" Text='<%#Bind("U_Z_ReimAmt") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>                                                                                           
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Notes">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &nbsp;<asp:Label ID="lblreason" runat="server" Text='<%#Bind("U_Z_Notes") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Attachment">
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton ID="lnkDownload" Text="Download" CommandArgument='<%# Eval("U_Z_Attachment") %>'
                                                                                                    runat="server" Width="100px" OnClick="lnkDownload_Click"></asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Approval Status">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &nbsp;<asp:Label ID="lblstatus" runat="server" Text='<%#Bind("U_Z_AppStatus") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Current Approver">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &#160;<asp:Label ID="lblcurapp" runat="server" Text='<%#Bind("U_Z_CurApprover") %>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Next Approver">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &#160;<asp:Label ID="lblnxtapp" runat="server" Text='<%#Bind("U_Z_NxtApprover") %>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Approval History">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &nbsp;<asp:LinkButton ID="lbtAppHistory" runat="server" Text="View" OnClick="lbtAppHistory_Click"></asp:LinkButton></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <HeaderStyle BackColor="Gray" HorizontalAlign="Center" ForeColor="White" Height="25px" />
                                                                                    <RowStyle HorizontalAlign="Left" />
                                                                                    <AlternatingRowStyle HorizontalAlign="Left" />
                                                                                       <FooterStyle Height="25px" BackColor="#CCCCCC" Font-Bold="true" />
                                                                                </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ContentTemplate>
                                                            </ajx:TabPanel>
                                                            <ajx:TabPanel ID="TabPanel2" runat="server" HeaderText="Rejected Expenses Claim">
                                                                <ContentTemplate>
                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="4" class="main_content">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:GridView ID="grdRejected" runat="server" CellPadding="4" RowStyle-CssClass="mousecursor"
                                                                                    AllowPaging="true" PageSize="15" CssClass="mGrid" HeaderStyle-CssClass="GridBG"
                                                                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="mousecursor" AutoGenerateColumns="false"
                                                                                    Width="100%" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found" ShowFooter="true">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Request Code">
                                                                                            <ItemTemplate>
                                                                                                <div align="center">
                                                                                                    <asp:Label ID="lblRCode" runat="server" Text='<%#Bind("Code") %>'>
                                                                                                    </asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Trip Type" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <div align="center">
                                                                                                    <asp:Label ID="lblRTripType" runat="server" Text='<%#Bind("U_Z_TripType") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Transaction Date">
                                                                                            <ItemTemplate>
                                                                                                <div align="center">
                                                                                                    <asp:Label ID="lblRcldate" runat="server" Text='<%#Bind("U_Z_Claimdt") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Travel Code" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &nbsp;<asp:Label ID="lblRTraCode" runat="server" Text='<%#Bind("U_Z_TraCode") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Travel Description" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &nbsp;<asp:Label ID="lblRTraName" runat="server" Text='<%#Bind("U_Z_TraDesc") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Expense Type">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &nbsp;<asp:Label ID="lblRexptype" runat="server" Text='<%#Bind("U_Z_ExpType") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Transaction Currency">
                                                                                            <ItemTemplate>
                                                                                                <div align="right">
                                                                                                    &nbsp;<asp:Label ID="lblRTraCur" runat="server" Text='<%#Bind("U_Z_Currency") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                               <FooterTemplate>
                                                                                                <div align="right">
                                                                                                    <asp:Label ID="lblRCur" runat="server" Text="Total :"></asp:Label>&nbsp;</div>
                                                                                            </FooterTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Transaction Amount">
                                                                                            <ItemTemplate>
                                                                                                <div align="right">
                                                                                                    &nbsp;<asp:Label ID="lblRtramt" runat="server" Text='<%#Bind("U_Z_CurAmt") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                              <FooterTemplate>
                                                                                                <div align="right">
                                                                                                    <asp:Label ID="lblRCurTotal" runat="server"></asp:Label>&nbsp;</div>
                                                                                            </FooterTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Exchange Rate">
                                                                                            <ItemTemplate>
                                                                                                <div align="right">
                                                                                                    &nbsp;<asp:Label ID="lblRexrate" runat="server" Text='<%#Bind("U_Z_ExcRate") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Local Currency Amount">
                                                                                            <ItemTemplate>
                                                                                                <div align="right">
                                                                                                    &nbsp;<asp:Label ID="lblRLocCur" runat="server" Text='<%#Bind("U_Z_UsdAmt") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                              <FooterTemplate>
                                                                                                <div align="right">
                                                                                                    <asp:Label ID="lblRLocCurTotal" runat="server"></asp:Label>&nbsp;</div>
                                                                                            </FooterTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="To be Reimbursed?">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &nbsp;<asp:Label ID="lblRreimb" runat="server" Text='<%#Bind("U_Z_Reimburse") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Reimbursement Amount">
                                                                                            <ItemTemplate>
                                                                                                <div align="right">
                                                                                                    &nbsp;<asp:Label ID="lblRreimbamt" runat="server" Text='<%#Bind("U_Z_ReimAmt") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>                                                                                            
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Notes">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &nbsp;<asp:Label ID="lblRreason" runat="server" Text='<%#Bind("U_Z_Notes") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Attachment">
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton ID="lnkRDownload" Text="Download" CommandArgument='<%# Eval("U_Z_Attachment") %>'
                                                                                                    runat="server" Width="100px" OnClick="lnkRDownload_Click"></asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Approval Status">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &nbsp;<asp:Label ID="lblRstatus" runat="server" Text='<%#Bind("U_Z_AppStatus") %>'></asp:Label>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Current Approver">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &#160;<asp:Label ID="lblcurapp" runat="server" Text='<%#Bind("U_Z_CurApprover") %>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Next Approver">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &#160;<asp:Label ID="lblnxtapp" runat="server" Text='<%#Bind("U_Z_NxtApprover") %>'></asp:Label></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Approval History">
                                                                                            <ItemTemplate>
                                                                                                <div align="left">
                                                                                                    &nbsp;<asp:LinkButton ID="lbtRAppHistory" runat="server" Text="View" OnClick="lbtRAppHistory_Click"></asp:LinkButton></div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <HeaderStyle BackColor="Gray" HorizontalAlign="Center" ForeColor="White" Height="25px" />
                                                                                    <RowStyle HorizontalAlign="Left" />
                                                                                    <AlternatingRowStyle HorizontalAlign="Left" />
                                                                                       <FooterStyle Height="25px" BackColor="#CCCCCC" Font-Bold="true" />
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
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="PanelItemEntry" runat="server" BackColor="White" Style="display: none;
                            padding: 10px; width: 1000px;">
                            <div>
                                <span class="sideheading" style="color: Green;">Expenses Claim Request</span> <span
                                    style="float: right;">
                                    <asp:Button ID="btnclstech2" runat="server" CssClass="btn" Width="30px" Text="X" /></span></div>
                            <br />
                            <br />
                            <asp:Panel ID="Panel3" runat="server" Height="400px" ScrollBars="Vertical">
                                <asp:Panel ID="Panel1" runat="server" Width="100%">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content">
                                        <tr>
                                            <td>
                                                Expenses Type (*)
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtexpcode" CssClass="txtbox" runat="server" Visible="false"></asp:TextBox>
                                                <asp:TextBox ID="txtexptype" CssClass="txtbox" runat="server" Enabled="false"></asp:TextBox>
                                                <asp:ImageButton ID="btnfindexp" runat="server" Text="Find" ImageUrl="~/images/search.jpg" />
                                                <ajx:ModalPopupExtender ID="ModalPopupExtender1" runat="server" DropShadow="True"
                                                    PopupControlID="panemlpopexp" TargetControlID="btnfindexp" CancelControlID="btnclstech1"
                                                    BackgroundCssClass="modalBackground">
                                                </ajx:ModalPopupExtender>
                                            </td>
                                            <td>
                                                Allowance Code
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAllowance" CssClass="txtbox" runat="server" Enabled="false"></asp:TextBox>
                                                <asp:TextBox ID="txtCredit" CssClass="txtbox" runat="server" Visible="false"></asp:TextBox>
                                                <asp:TextBox ID="txtdebit" CssClass="txtbox" runat="server" Visible="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Transaction Date (*)
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txttrasdt" CssClass="txtbox" runat="server"></asp:TextBox>
                                                <ajx:CalendarExtender ID="CalendarExtender2" Animated="true" Format="dd/MM/yyyy"
                                                    runat="server" TargetControlID="txttrasdt" CssClass="cal_Theme1">
                                                </ajx:CalendarExtender>
                                            </td>
                                            <td>
                                                City
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtcity" CssClass="txtbox" runat="server"></asp:TextBox>
                                                <asp:TextBox ID="txtposting" CssClass="txtbox" runat="server" Visible="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Transaction Currency (*)
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddltranscur" runat="server" CssClass="txtbox1" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                Transaction Amount (*)
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txttrasamt" CssClass="txtbox" runat="server" autocomplete="off"
                                                    AutoPostBack="true" onkeypress="AllowNumbers(this);checkDec(this);RemoveZero(this);"
                                                    onkeyup="AllowNumbers(this);checkDec(this);RemoveZero(this);"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Exchange Rate
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtexrate" CssClass="txtbox" runat="server" autocomplete="off" AutoPostBack="true"
                                                    onkeypress="AllowNumbers(this);checkDec(this);RemoveZero(this);" onkeyup="AllowNumbers(this);checkDec(this);RemoveZero(this);"></asp:TextBox>
                                            </td>
                                            <td>
                                                Local Currency Amount
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtlocamt" CssClass="txtbox" runat="server" Enabled="false"></asp:TextBox>
                                                <asp:Label ID="lblOverLap" CssClass="txtbox" runat="server" Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                To be Reimbursed?
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlreimbused" runat="server" CssClass="txtbox1" AutoPostBack="true">
                                                    <asp:ListItem Value="N">No</asp:ListItem>
                                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                Reimbursement Amount
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtreimbuse" CssClass="txtbox" runat="server" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Payment Method
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlpaymethod" runat="server" CssClass="txtbox1">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                Attachment
                                            </td>
                                            <td>
                                                <asp:FileUpload ID="fileattach" runat="server" />
                                                <%--<ajx:AjaxFileUpload ID="fileattach" runat="server"   MaximumNumberOfFiles="1" OnUploadComplete="File_Upload"  Width="500px" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDisRule" CssClass="txtbox" runat="server" Visible="false"></asp:TextBox>
                                                <asp:ImageButton ID="ImgbtnDisRule" runat="server" Text="Find" ImageUrl="~/images/search.jpg"
                                                    Visible="false" />
                                                <ajx:ModalPopupExtender ID="ModalPopupExtender3" runat="server" DropShadow="True"
                                                    PopupControlID="PanelPopDisRule" TargetControlID="ImgbtnDisRule" CancelControlID="btnclsDis"
                                                    BackgroundCssClass="modalBackground">
                                                </ajx:ModalPopupExtender>
                                            </td>
                                            <td>
                                                Status
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="txtbox1" Enabled="false">
                                                    <asp:ListItem Value="P">Pending</asp:ListItem>
                                                    <asp:ListItem Value="A">Approved</asp:ListItem>
                                                    <asp:ListItem Value="R">Rejected</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Notes
                                            </td>
                                            <td colspan="2">
                                                <asp:TextBox ID="txtnotes" CssClass="txtbox" runat="server" TextMode="MultiLine"
                                                    Height="100" Width="300px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center">
                                                <br />
                                                <asp:Button ID="btnAdd" CssClass="btn" Width="85px" runat="server" Text="Save" />
                                                <asp:Button ID="btncancel" CssClass="btn" Width="85px" runat="server" Text="Cancel"
                                                    Visible="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtreqcode" CssClass="txtbox" runat="server" Visible="False"></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtreqdate" CssClass="txtbox" runat="server" Visible="False"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </asp:Panel>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="PanelPopDisRule" runat="server" BackColor="White" Style="display: none;
                            padding: 10px; width: 350px;">
                            <div>
                                <span class="sideheading" style="color: Green;">Distribution Rule</span> <span style="float: right;">
                                    <asp:Button ID="btnclsDis" runat="server" CssClass="btn" Width="30px" Text="X" /></span></div>
                            <br />
                            <br />
                            <asp:Panel ID="Panel7" runat="server" Height="200px">
                                <div>
                                    <asp:Label ID="lblDis1" runat="server" class="txtbox" Text="Dimension1"></asp:Label>
                                    <asp:DropDownList ID="ddlDis1" runat="server" class="txtbox1">
                                    </asp:DropDownList>
                                    <br />
                                    <asp:Label ID="lblDis2" runat="server" class="txtbox"></asp:Label>
                                    <asp:DropDownList ID="ddlDis2" runat="server" class="txtbox1">
                                    </asp:DropDownList>
                                    <br />
                                    <asp:Label ID="lblDis3" runat="server" class="txtbox"></asp:Label>
                                    <asp:DropDownList ID="ddlDis3" runat="server" class="txtbox1">
                                    </asp:DropDownList>
                                    <br />
                                    <asp:Label ID="lblDis4" runat="server" class="txtbox"></asp:Label>
                                    <asp:DropDownList ID="ddlDis4" runat="server" class="txtbox1">
                                    </asp:DropDownList>
                                    <br />
                                    <asp:Label ID="lblDis5" runat="server" class="txtbox"></asp:Label>
                                    <asp:DropDownList ID="ddlDis5" runat="server" class="txtbox1">
                                    </asp:DropDownList>
                                    <br />
                                    <br>
                                    <asp:Button ID="btnDisOK" runat="server" CssClass="btn" Width="60px" Text="OK" />
                                </div>
                            </asp:Panel>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="Panelpoptechnician" runat="server" BackColor="White" Style="display: none;
                            padding: 10px; width: 500px;">
                            <div>
                                <span class="sideheading" style="color: Green;">Travel Details</span> <span style="float: right;">
                                    <asp:Button ID="btnclstech" runat="server" CssClass="btn" Width="30px" Text="X" /></span></div>
                            <br />
                            <br />
                            <asp:Panel ID="Panel4" runat="server" Height="200px" ScrollBars="Vertical">
                                <asp:GridView ID="grdtravel" runat="server" CellPadding="4" RowStyle-CssClass="mousecursor"
                                    ShowHeaderWhenEmpty="true" EmptyDataText="No records Found" CssClass="mGrid"
                                    HeaderStyle-CssClass="GridBG" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="mousecursor"
                                    AutoGenerateColumns="false" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="DocEntry">
                                            <ItemTemplate>
                                                <asp:Label ID="lblltracode" runat="server" Text='<%#Bind("DocEntry") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Travel Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblltraname" runat="server" Text='<%#Bind("U_Z_TraName") %>'></asp:Label>
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
                        <asp:Panel ID="panemlpopexp" runat="server" BackColor="White" Style="display: none;
                            padding: 10px; width: 500px;">
                            <div>
                                <span class="sideheading" style="color: Green;">Expenses Details</span> <span style="float: right;">
                                    <asp:Button ID="btnclstech1" runat="server" CssClass="btn" Width="30px" Text="X" /></span></div>
                            <br />
                            <br />
                            <asp:Panel ID="Panel2" runat="server" Height="200px" ScrollBars="Vertical">
                                <asp:GridView ID="grdexpenses" runat="server" CellPadding="4" RowStyle-CssClass="mousecursor"
                                    ShowHeaderWhenEmpty="true" EmptyDataText="No records Found" CssClass="mGrid"
                                    HeaderStyle-CssClass="GridBG" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="mousecursor"
                                    AutoGenerateColumns="false" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Expenses Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lbllexpcode" runat="server" Text='<%#Bind("Code") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Expenses Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lbllexpname" runat="server" Text='<%#Bind("U_Z_ExpName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Allowance Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lbllAllCode" runat="server" Text='<%#Bind("U_Z_AlloCode") %>'></asp:Label>
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
                        <ajx:ModalPopupExtender ID="ModalPopupExtender2" runat="server" DropShadow="True"
                            PopupControlID="Panelpoptechnician1" TargetControlID="btnSample" CancelControlID="btnclstech2"
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
                                    <asp:Button ID="Button1" runat="server" CssClass="btn" Width="30px" Text="X" /></span></div>
                            <br />
                            <br />
                            <asp:Panel ID="Panel5" runat="server" Height="400px" >
                                <asp:Label ID="Label1" runat="server" Text="" CssClass="txtbox" ForeColor="Red"></asp:Label>
                                <asp:GridView ID="grdRequesttohr" runat="server" CellPadding="4" ShowHeaderWhenEmpty="true"
                                    CssClass="mGrid" HeaderStyle-CssClass="GridBG" AlternatingRowStyle-CssClass="alt"
                                    AutoGenerateColumns="false" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Employee ID">
                                            <ItemTemplate>
                                                <div align="center">
                                                    <asp:Label ID="lbtndocnum" runat="server" Text='<%#Bind("U_Z_EmpId") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employee Name">
                                            <ItemTemplate>
                                                <div align="center">
                                                    <asp:Label ID="lblactivity" runat="server" Text='<%#Bind("U_Z_EmpName") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Approved By">
                                            <ItemTemplate>
                                                <div align="center">
                                                    <asp:Label ID="lbltype" runat="server" Text='<%#Bind("U_Z_ApproveBy") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Next Approver">
                                            <ItemTemplate>
                                                <div align="center">
                                                    <asp:Label ID="lblhapprover" runat="server" Text='<%#Bind("U_Z_NxtApprover") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CreateDate">
                                            <ItemTemplate>
                                                <div align="center">
                                                    <asp:Label ID="lblcrDate" runat="server" Text='<%#Bind("CreateDate") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Create Time">
                                            <ItemTemplate>
                                                <div align="center">
                                                    <asp:Label ID="lblcrtime" runat="server" Text='<%#Bind("CreateTime") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Update Date">
                                            <ItemTemplate>
                                                <div align="center">
                                                    <asp:Label ID="lblupdate" runat="server" Text='<%#Bind("UpdateDate") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Update Time">
                                            <ItemTemplate>
                                                <div align="center">
                                                    <asp:Label ID="lblupdatime" runat="server" Text='<%#Bind("UpdateTime") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Approved Status">
                                            <ItemTemplate>
                                                <div align="center">
                                                    <asp:Label ID="lblactsubject" runat="server" Text='<%#Bind("U_Z_AppStatus") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <div align="center">
                                                    <asp:Label ID="lblAssigned" runat="server" Text='<%#Bind("U_Z_Remarks") %>'></asp:Label></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle HorizontalAlign="Center" Height="25px" BackColor="#CCCCCC" />
                                </asp:GridView>
                            </asp:Panel>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="Panel1$btnAdd" />
            <asp:PostBackTrigger ControlID="TabContainer1$TabPanel5$btnSubmit" />
            <asp:PostBackTrigger ControlID="TabContainer1$TabPanel5$btnClose" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
