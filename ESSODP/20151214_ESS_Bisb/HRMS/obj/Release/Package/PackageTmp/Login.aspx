<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="HRMS.Login" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>:: Human Resource Management. ::</title>
<link rel="stylesheet" href="Styles/vThink_Theme.css" type="text/css" />
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
<body style="background-color:#f4f4f4;">
<form id="form1" runat="server">
    <div id="divMain">
     <center>
     <table id="tblLogin">
     <tr>
     <td>
      <div id="divLogin" runat="server">
    <table id="tblUnPwd" cellspacing="5" cellpadding="4">
    <tr>
    <td style="color:Green;">
    User Name
    </td>
    <td>
    <asp:TextBox ID="TxtUid" runat="server"  TabIndex="1"   PlaceHolder="User Name" CssClass="txtUn" onkeyup="SetButtonStatus(this,'BtnSubmit')"/>
    </td>
    </tr>  
    <tr>
    <td style="color:Green;">
    Password
    </td>
    <td>
    <asp:TextBox ID="TxtPwd" runat="server" TextMode="Password" TabIndex="2" PlaceHolder="Password" CssClass="txtPwd" onkeyup="SetButtonStatus(this,'BtnSubmit')"/>
    </td>
    </tr>
    <tr>
    <td colspan="2">
   <asp:Label ID="lblmsg" runat="server" Font-Names="Arial" Font-Size="9pt" ForeColor="#C00000" Text="" Font-Bold="False"/>
    </td>
    </tr>
    <tr>
    <td></td>
    <td align="right">
   <asp:Button ID="BtnSubmit"  runat="server"  Enabled="true" Text="Login" Width="85px" Font-Bold="True" TabIndex="3" CssClass="btn" />
    </td>
    </tr>
    </table>
     </div>
     </td>
     </tr>
     </table>
    </center>
     </div>
     </form>
</body>
</html>

