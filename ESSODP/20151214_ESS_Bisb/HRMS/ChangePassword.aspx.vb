Imports System
Imports BusinessLogic
Imports DataAccess
Imports EN
Public Class ChangePassword
    Inherits System.Web.UI.Page
    Dim objen As ChangePwdEN = New ChangePwdEN()
    Dim objBL As ChangePwdBL = New ChangePwdBL()
    Dim objDA As ChangePwdDA = New ChangePwdDA()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Session("UserCode") Is Nothing Then
                Response.Redirect("Login.aspx?sessionExpired=true", True)
            End If
        End If
    End Sub
    Private Sub mess(ByVal str As String)
        ScriptManager.RegisterStartupScript(Update, Update.[GetType](), "strmsg", objen.strmsg, True)
    End Sub
    Protected Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If Session("UserCode") Is Nothing Then
            Response.Redirect("Login.aspx?sessionExpired=true", True)
        Else
            objen.EmpId = Session("UserCode").ToString()
            objen.OldPwd = txtoldpwd.Text.Trim()
            objen.NewPwd = txtnewpwd.Text.Trim()
            objen.ConfirmPwd = txtconfirmpwd.Text.Trim()

            If objen.OldPwd = "" Then
                objen.strmsg = "alert('Enter the old Password..')"
                mess(objen.strmsg)
            End If
            If objen.NewPwd = "" Then
                objen.strmsg = "alert('Enter the New Password..')"
                mess(objen.strmsg)
            End If
            If objen.ConfirmPwd = "" Then
                objen.strmsg = "alert('Enter the Confirm Password..')"
                mess(objen.strmsg)
            End If
            If objBL.Checkpassword(objen) = False Then
                objen.strmsg = "alert('Old password does not match..')"
                mess(objen.strmsg)
            Else
                objDA.UpdatePassword(objen)
                objen.strmsg = "alert('Password changed Successfully ..')"
                mess(objen.strmsg)
            End If
        End If
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Response.Redirect("Home.aspx", False)
    End Sub
End Class