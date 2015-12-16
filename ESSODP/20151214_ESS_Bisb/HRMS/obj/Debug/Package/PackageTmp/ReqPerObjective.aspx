<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/HRMS.Master"
    CodeBehind="ReqPerObjective.aspx.vb" Inherits="HRMS.ReqPerObjective" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function Confirmation() {
            if (confirm("Do you want to confirm the Changes?") == true) {
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
     
    </script>
    <style type="text/css">
        .modalPopup
        {
            background-color: #696969;
            filter: alpha(opacity=40);
            opacity: 0.7;
            xindex: -1;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
            <table width="100%" border="0" cellspacing="0" cellpadding="4" class="main_content">
                <tr>
                    <td>
                        <asp:TextBox ID="txtHEmpID" runat="server" Width="93px" Style="display: none"></asp:TextBox>
                        <asp:TextBox ID="txtpopunique" runat="server" Style="display: none"></asp:TextBox>
                        <asp:TextBox ID="txtpoptno" runat="server" Style="display: none"></asp:TextBox>
                        <asp:TextBox ID="txthidoption" runat="server" Style="display: none"></asp:TextBox>
                        <asp:TextBox ID="txttname" runat="server" Style="display: none"></asp:TextBox>
                        <asp:TextBox ID="txtsweight" runat="server" Style="display: none"></asp:TextBox>
                        <input id="Btncallpop" runat="server" onserverclick="Btncallpop_ServerClick" style="display: none"
                            type="button" value="button" />
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
                            <tr>
                                <td height="30" align="left" colspan="2" valign="bottom" background="images/h_bg.png"
                                    style="border-bottom: 1px dotted; border-color: #f45501; background-repeat: repeat-x">
                                    <div>
                                        &nbsp;
                                        <asp:Label ID="Label3" runat="server" Text="Requested Personel Objectives" CssClass="subheader"
                                            Style="float: left;"></asp:Label>
                                        <span>
                                            <asp:Label ID="lblNewTrip" runat="server" Text="" Visible="false"></asp:Label></span>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="panelhome" runat="server" Width="100%">
                                        <asp:ImageButton ID="btnhome" runat="server" ImageUrl="../images/Homeicon.jpg" PostBackUrl="~/Home.aspx"
                                            ToolTip="Home" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="btnnew" runat="server" ImageUrl="../images/Add.jpg" ToolTip="Add new record"
                                            Visible="false" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </asp:Panel>
                                    <asp:Label ID="Label2" runat="server" Text="" Style="color: Red;"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="panelnew" runat="server" Width="98%">
                            <div id="Div1" runat="server" class="DivCorner" style="border: solid 2px LightSteelBlue;
                                width: 100%;">
                                <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
                                    <tr>
                                        <td width="15%">
                                            First Name
                                        </td>
                                        <td>
                                            <asp:Label ID="txtFirstName" CssClass="txtbox" Width="150px" runat="server"></asp:Label>
                                        </td>
                                        <td width="15%">
                                        </td>
                                        <td width="20%">
                                            Employee No
                                        </td>
                                        <td width="15%">
                                            <asp:Label ID="txtempno" CssClass="txtbox" Width="150px" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="10%">
                                            Middle Name
                                        </td>
                                        <td>
                                            <asp:Label ID="txtmiddleName" CssClass="txtbox" Width="150px" runat="server"></asp:Label>
                                        </td>
                                        <td width="15%">
                                        </td>
                                        <td width="10%">
                                            Third Name
                                        </td>
                                        <td width="30%">
                                            <asp:Label ID="txtthirdname" CssClass="txtbox" Width="150px" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="10%">
                                            Last Name
                                        </td>
                                        <td>
                                            <asp:Label ID="txtlastname" CssClass="txtbox" Width="150px" runat="server"></asp:Label>
                                        </td>
                                        <td width="15%">
                                        </td>
                                        <td width="10%">
                                        </td>
                                        <td width="30%">
                                            <asp:Label ID="txtappid" CssClass="txtbox" Width="150px" runat="server" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <%-- <td width="10%">Job Title</td>
                 <td>
                  <asp:Label ID="txtjobtitle" CssClass="txtbox" Width="150px"  runat="server" ></asp:Label>
                 </td>--%>
                                        <td width="10%">
                                            Position
                                        </td>
                                        <td>
                                            <asp:Label ID="txtposition" CssClass="txtbox" Width="150px" runat="server"></asp:Label>
                                            <asp:Label ID="lblposCode" CssClass="txtbox" Width="150px" runat="server" Visible="false"></asp:Label>
                                        </td>
                                        <td width="15%">
                                        </td>
                                        <td width="10%">
                                            Office Phone
                                        </td>
                                        <td width="30%">
                                            <asp:Label ID="txtoffphone" CssClass="txtbox" Width="150px" runat="server"></asp:Label>
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
                                        <td width="10%">
                                            Department
                                        </td>
                                        <td>
                                            <asp:Label ID="txtdept" CssClass="txtbox" Width="150px" runat="server"></asp:Label>
                                            <asp:Label ID="lbldeptcode" CssClass="txtbox" Width="150px" runat="server" Visible="false"></asp:Label>
                                        </td>
                                        <td width="15%">
                                        </td>
                                        <td width="10%">
                                            Mobile Phone
                                        </td>
                                        <td width="30%">
                                            <asp:Label ID="txtmobile" CssClass="txtbox" Width="150px" runat="server"></asp:Label>
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
                                        <td width="10%">
                                            Manager
                                        </td>
                                        <td>
                                            <asp:Label ID="txtmanager" CssClass="txtbox" Width="150px" runat="server"></asp:Label>
                                        </td>
                                        <td width="15%">
                                        </td>
                                        <td width="10%">
                                            Home Phone
                                        </td>
                                        <td width="30%">
                                            <asp:Label ID="txthometel" CssClass="txtbox" Width="150px" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="10%">
                                            E-mail
                                        </td>
                                        <td>
                                            <asp:Label ID="txtemail" CssClass="txtbox" Width="150px" runat="server"></asp:Label>
                                        </td>
                                        <td width="15%">
                                        </td>
                                        <td width="10%">
                                            Fax
                                        </td>
                                        <td width="30%">
                                            <asp:Label ID="txtfax" CssClass="txtbox" Width="150px" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="paltab" runat="server" Width="98%" BorderColor="LightSteelBlue" BorderWidth="2">
                            <table width="100%" border="0" cellspacing="0" cellpadding="4" class="main_content">
                                <tr>
                                    <td valign="top">
                                        <ajx:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" CssClass="ajax__tab_yuitabview-theme"
                                            Width="100%">
                                            <ajx:TabPanel ID="TabPanel5" runat="server" HeaderText="Requested Personal Objective">
                                                <ContentTemplate>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="4" class="main_content">
                                                        <tr>
                                                            <td>
                                                                <asp:GridView ID="grdnewpeople" runat="server" CellPadding="4" RowStyle-CssClass="mousecursor"
                                                                    AutoGenerateDeleteButton="true" CssClass="mGrid" HeaderStyle-CssClass="GridBG"
                                                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false"
                                                                    Width="100%" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Code" Visible="false">
                                                                            <ItemTemplate>
                                                                                <div align="left">
                                                                                    &nbsp;
                                                                                    <asp:Label ID="lblDocCode" runat="server" Text='<%#Bind("U_DocEntry") %>'></asp:Label>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Personal Objective Code">
                                                                            <ItemTemplate>
                                                                                <div align="left">
                                                                                    &nbsp;
                                                                                    <asp:Label ID="lblPCode" runat="server" Text='<%#Bind("U_PeoobjCode") %>'></asp:Label>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Personal Objective Description">
                                                                            <ItemTemplate>
                                                                                <div align="left">
                                                                                    &nbsp;
                                                                                    <asp:Label ID="lblNName" runat="server" Text='<%#Bind("U_PeoobjName") %>'></asp:Label></div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Category">
                                                                            <ItemTemplate>
                                                                                <div align="left">
                                                                                    &nbsp;
                                                                                    <asp:Label ID="lblNCategory" runat="server" Text='<%#Bind("U_Z_CatName") %>'></asp:Label></div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Weight">
                                                                            <ItemTemplate>
                                                                                <div align="left">
                                                                                    &nbsp;
                                                                                    <asp:Label ID="lblNWeight" runat="server" Text='<%#Bind("U_Weight") %>'></asp:Label></div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Remarks">
                                                                            <ItemTemplate>
                                                                                <div align="left">
                                                                                    &nbsp;
                                                                                    <asp:Label ID="lblNRemarks" runat="server" Text='<%#Bind("U_Remarks") %>'></asp:Label></div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Status">
                                                                            <ItemTemplate>
                                                                                <div align="left">
                                                                                    &nbsp;
                                                                                    <asp:Label ID="lblNStatus" runat="server" Text='<%#Bind("U_Z_AppStatus") %>'></asp:Label></div>
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
                                                                    <RowStyle HorizontalAlign="Center" />
                                                                    <AlternatingRowStyle HorizontalAlign="Center" />
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="Buttonnewpeople" runat="server" Text="Request New Objective " CssClass="btn" />
                                                                <ajx:ModalPopupExtender ID="ModalPopupExtender6" runat="server" DropShadow="false"
                                                                    PopupControlID="PanelItemEntry" TargetControlID="Buttonnewpeople" CancelControlID="btnclsitem"
                                                                    BackgroundCssClass="modalBackground">
                                                                </ajx:ModalPopupExtender>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </ajx:TabPanel>
                                            <ajx:TabPanel ID="TabPanel1" runat="server" HeaderText="Deleted Personal Objective Summary"
                                                Visible="false">
                                                <ContentTemplate>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="4" class="main_content">
                                                        <tr>
                                                            <td>
                                                                <asp:GridView ID="grddeletepeople" runat="server" CellPadding="4" RowStyle-CssClass="mousecursor"
                                                                    CssClass="mGrid" HeaderStyle-CssClass="GridBG" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                                    AutoGenerateColumns="false" Width="100%" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Code" Visible="false">
                                                                            <ItemTemplate>
                                                                                <div align="left">
                                                                                    &nbsp;
                                                                                    <asp:Label ID="lblDocCode1" runat="server" Text='<%#Bind("U_DocEntry") %>'></asp:Label>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Personal Objective Code">
                                                                            <ItemTemplate>
                                                                                <div align="left">
                                                                                    &nbsp;
                                                                                    <asp:Label ID="lblPCode1" runat="server" Text='<%#Bind("U_PeoobjCode") %>'></asp:Label>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Personal Objective Description">
                                                                            <ItemTemplate>
                                                                                <div align="left">
                                                                                    &nbsp;
                                                                                    <asp:Label ID="lblNName1" runat="server" Text='<%#Bind("U_PeoobjName") %>'></asp:Label></div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Category">
                                                                            <ItemTemplate>
                                                                                <div align="left">
                                                                                    &nbsp;
                                                                                    <asp:Label ID="lblNCategory1" runat="server" Text='<%#Bind("U_Z_CatName") %>'></asp:Label></div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Weight">
                                                                            <ItemTemplate>
                                                                                <div align="left">
                                                                                    &nbsp;
                                                                                    <asp:Label ID="lblNWeight1" runat="server" Text='<%#Bind("U_Weight") %>'></asp:Label></div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Remarks">
                                                                            <ItemTemplate>
                                                                                <div align="left">
                                                                                    &nbsp;
                                                                                    <asp:Label ID="lblNRemarks1" runat="server" Text='<%#Bind("U_Remarks") %>'></asp:Label></div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Status">
                                                                            <ItemTemplate>
                                                                                <div align="left">
                                                                                    &nbsp;
                                                                                    <asp:Label ID="lblNStatus1" runat="server" Text='<%#Bind("U_Z_AppStatus") %>'></asp:Label></div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <HeaderStyle BackColor="Gray" HorizontalAlign="Center" ForeColor="White" Height="25px" />
                                                                    <RowStyle HorizontalAlign="Center" />
                                                                    <AlternatingRowStyle HorizontalAlign="Center" />
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </ajx:TabPanel>
                                        </ajx:TabContainer>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="Panelpopemployee" runat="server" BackColor="White" Style="display: none;
                                            padding: 10px; width: 500px;">
                                            <div>
                                                <span class="sideheading" style="color: Green;">Personal Objectives</span> <span
                                                    style="float: right;">
                                                    <asp:Button ID="btnclstech1" runat="server" CssClass="btn" Width="30px" Text="X" /></span></div>
                                            <br />
                                            <br />
                                            <asp:Panel ID="Panel4" runat="server" Height="200px" ScrollBars="Vertical">
                                                <asp:GridView ID="grdpoppeople" runat="server" CellPadding="4" RowStyle-CssClass="mousecursor"
                                                    ShowHeaderWhenEmpty="true" EmptyDataText="No records Found" CssClass="mGrid"
                                                    HeaderStyle-CssClass="GridBG" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                    AutoGenerateColumns="false" Width="100%">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Objective Code">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblobjcode" runat="server" Text='<%#Bind("U_Z_PeoobjCode") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Description">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcouCode" runat="server" Text='<%#Bind("U_Z_PeoobjName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Category">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcategory" runat="server" Text='<%#Bind("U_Z_PeoCategory") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Weight">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblWeight" runat="server" Text='<%#Bind("U_Z_Weight") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle BackColor="Gray" HorizontalAlign="Center" ForeColor="White" Height="25px" />
                                                    <RowStyle HorizontalAlign="Center" />
                                                    <AlternatingRowStyle HorizontalAlign="Center" />
                                                </asp:GridView>
                                            </asp:Panel>
                                        </asp:Panel>
                                        <asp:Panel ID="PanelItemEntry" runat="server" BackColor="White" Style="display: none;
                                            padding: 10px; width: 500px;">
                                            <div>
                                                <span class="sideheading" style="color: Green;">Personel Objectives</span> <span
                                                    style="float: right;">
                                                    <asp:Button ID="btnclsitem" runat="server" CssClass="btn" Width="30px" Text="X" /></span></div>
                                            <br />
                                            <br />
                                            <asp:Panel ID="panItemEntry" runat="server" Height="230px">
                                                <table width="100%" border="0" cellspacing="0" cellpadding="3" class="main_content">
                                                    <tr>
                                                        <td width="20%">
                                                            Personal Objective Code
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtpeocode" CssClass="txtbox" Width="150px" runat="server" ReadOnly="True"></asp:TextBox>
                                                            <asp:ImageButton ID="btnItem1" runat="server" Text="Find" ImageUrl="~/images/search.jpg" />
                                                            <ajx:ModalPopupExtender ID="popemployee" runat="server" DropShadow="true" PopupControlID="Panelpopemployee"
                                                                TargetControlID="btnItem1" CancelControlID="btnclstech1" BackgroundCssClass="modalBackground">
                                                            </ajx:ModalPopupExtender>
                                                            <asp:TextBox ID="txtrefcode" CssClass="txtbox" Width="150px" Visible="false" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="10%">
                                                            Personal Objective Description
                                                        </td>
                                                        <td width="30%">
                                                            <asp:TextBox ID="txtpeodescription" CssClass="txtbox" Width="150px" Enabled="false"
                                                                runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="10%">
                                                            Category
                                                        </td>
                                                        <td width="30%">
                                                            <asp:DropDownList ID="ddlcategory" CssClass="txtbox1" Width="150px" Enabled="false"
                                                                runat="server" Height="25px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="10%">
                                                            Weight (%)
                                                        </td>
                                                        <td width="30%">
                                                            <asp:TextBox ID="txtweight" CssClass="txtbox" Width="150px" runat="server" onkeypress="AllowNumbers(this);checkDec(this);RemoveZero(this);"
                                                                onkeyup="AllowNumbers(this);checkDec(this);RemoveZero(this);"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="10%">
                                                            Remarks
                                                        </td>
                                                        <td width="30%">
                                                            <asp:TextBox ID="txtpeoRemarks" CssClass="txtbox" Width="150px" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="center">
                                                            <br />
                                                            <asp:Button ID="btnAdd" CssClass="btn" runat="server" Text="Add" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </asp:Panel>
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
