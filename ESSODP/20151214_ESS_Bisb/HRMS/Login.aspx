<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="HRMS.Login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>:: Human Resource Management. ::</title>
    <link rel="stylesheet" href="Styles/vThink_Theme.css" type="text/css" />
    <link rel="shortcut icon" href="Images/favicon.ico" type="image/x-icon" />
    <link rel="icon" href="Images/favicon.ico" type="image/x-icon" />
    <script type="text/javascript" language="javascript">
        function SetButtonStatus() {
            var txt = document.getElementById('TxtUid');
            var txt2 = document.getElementById('TxtPwd');
            if (txt.value.length >= 1 && txt2.value.length >= 1)
                document.getElementById('BtnSubmit').disabled = false;
            else
                document.getElementById('BtnSubmit').disabled = true;
        }
    </script>
</head>
<body style="background-color: #f4f4f4;">
    <form id="form1" runat="server">
    <ajx:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
    </ajx:ToolkitScriptManager>
    <div id="divMain">
        <center>
            <table id="tblLogin">
                <tr>
                    <td>
                        <div id="divLogin" runat="server">
                            <table id="tblUnPwd" cellspacing="5" cellpadding="4">
                                <tr>
                                    <td style="color: #4F1F89; font-family: Verdana;">
                                        User Name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtUid" runat="server" TabIndex="1" PlaceHolder="User Name" CssClass="txtUn"
                                            onkeyup="SetButtonStatus(this,'BtnSubmit')" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="color: #4F1F89; font-family: Verdana;">
                                        Password
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtPwd" runat="server" TextMode="Password" TabIndex="2" PlaceHolder="Password"
                                            CssClass="txtPwd" onkeyup="SetButtonStatus(this,'BtnSubmit')" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lblmsg" runat="server" Font-Names="Arial" Font-Size="9pt" ForeColor="#C00000"
                                            Text="" Font-Bold="False" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="BtnSubmit" runat="server" Enabled="true" Text="Login" Width="85px"
                                            Font-Bold="True" TabIndex="3" CssClass="btn" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
            <ajx:ModalPopupExtender ID="ModalPopupExtender6" runat="server" DropShadow="false"
                Enabled="false" PopupControlID="PanelNewTraining" TargetControlID="BtnSubmit"
                BackgroundCssClass="modalBackground">
            </ajx:ModalPopupExtender>
            <asp:Panel ID="PanelNewTraining" runat="server" BackColor="White" Style="display: none;
                padding: 10px; width: 600px;">
                <div>
                    <span class="sideheading" style="color: Green;">Change Password</span> <span style="float: right;">
                    </span>
                </div>
                <br />
                <br />
                <asp:Panel ID="panItemEntry" runat="server" Height="280px" ScrollBars="None">
                    <table width="99%" border="0" cellspacing="0" cellpadding="4" class="main_content">
                        <tr>
                            <td>
                                Old Password <a style="color: Red;">*</a>
                            </td>
                            <td>
                                <asp:TextBox ID="txtoldpwd" CssClass="txtbox" Width="150px" runat="server" TextMode="Password"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtoldpwd"
                                    ValidationGroup="Changepwd" ErrorMessage="Enter the old Password.."></asp:RequiredFieldValidator>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                New Password <a style="color: Red;">*</a>
                            </td>
                            <td>
                                <asp:TextBox ID="txtnewpwd" CssClass="txtbox" Width="150px" runat="server" TextMode="Password"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtnewpwd"
                                    ValidationGroup="Changepwd" ErrorMessage="Enter the New Password.."></asp:RequiredFieldValidator>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Confirm Password <a style="color: Red;">*</a>
                            </td>
                            <td>
                                <asp:TextBox ID="txtconfirmpwd" CssClass="txtbox" Width="150px" runat="server" TextMode="Password"></asp:TextBox>
                            </td>
                            <td>
                                <asp:CompareValidator ID="CompareValidator" runat="server" ControlToValidate="txtnewpwd"
                                    ControlToCompare="txtconfirmpwd" ValidationGroup="Changepwd" ErrorMessage="Confirm Password does not match!">  
                                </asp:CompareValidator>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Style="color: Red;"></asp:Label>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td colspan="3">
                                <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="btn" Width="85px" ValidationGroup="Changepwd" />
                                <asp:Button ID="btnclose" CssClass="btn" Width="85px" runat="server" Text="Cancel" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </asp:Panel>
        </center>
    </div>
    </form>
</body>
</html>
