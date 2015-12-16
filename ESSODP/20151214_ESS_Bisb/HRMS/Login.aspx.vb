Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Xml
Imports System.IO
Imports EN
Imports DataAccess
Imports BusinessLogic

Public Class Login
    Inherits System.Web.UI.Page
    Dim objEn As LoginEN = New LoginEN()
    Dim objDA As LoginBL = New LoginBL()
    Dim objLDA As LoginDA = New LoginDA()
    Dim objPwd As ChangePwdEN = New ChangePwdEN()
    Dim objPBL As ChangePwdBL = New ChangePwdBL()
    Dim objPDA As ChangePwdDA = New ChangePwdDA()
    Dim objCom As CommonFunctions = New CommonFunctions()
    Dim dbCon As DBConnectionDA = New DBConnectionDA()
    Dim strstring, strError As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            strstring = Request.QueryString("SessionExpired")
            If Request.QueryString("SessionExpired") <> Nothing Or Request.QueryString("SessionExpired") = "ture" Then
                Dim strmsg As String = "Session expired. You will be redirected to Login page"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "js", "<script>alert('" & strmsg & "')</script>")
            End If
        End If
    End Sub

    Private Sub BtnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit.Click
        Session.Clear()
        objCom.checktable()
        objEn.Userid = TxtUid.Text.Trim()
        objEn.Password = TxtPwd.Text.Trim()
        If objEn.Userid = "" Then
            ClientScript.RegisterStartupScript([GetType](), "Message", "<script>alert('Enter the UserName')</script>")
        ElseIf objEn.Password = "" Then
            ClientScript.RegisterStartupScript([GetType](), "Message", "<script>alert('Enter the Password')</script>")
        ElseIf (objDA.UserAuthentication(objEn)) = True Then
            Session("UserCode") = objDA.GetCardCode(objEn)
            objEn.SessionUid = Session("UserCode")
            Session("UserName") = objDA.GetCardName(objEn)
            Session("SAPCompany") = Application("DBName")
            Session("SessionId") = objDA.SessionDetails(Session("UserCode").ToString())
            Session("EmpUserName") = "" ' ConfigurationManager.AppSettings("SAPuserName")
            Session("UserPwd") = "" ' ConfigurationManager.AppSettings("SAPpassword")

            If objDA.CheckFirstLogin(objEn) = False Then
                ModalPopupExtender6.Enabled = True
                ModalPopupExtender6.Show()
            Else
                objPwd.EmpId = Session("UserCode").ToString()
                objPwd.Formid = Session("UserName").ToString()
                objLDA.InsertFirstLogin(objPwd)
                Response.Redirect("Home.aspx", False)
            End If
            'Response.Redirect("Home.aspx", False)
        ElseIf (objDA.ESSUsercheck(objEn)) = True Then
            Session("UserCode") = objDA.GetCardCode(objEn)
            objEn.SessionUid = Session("UserCode")
            Session("UserName") = objDA.GetCardName(objEn)
            Session("SAPCompany") = Application("DBName")
            Session("SAPCompany") = Application("DBName")
            Session("SessionId") = objDA.SessionDetails(Session("UserCode").ToString())
            Session("EmpUserName") = "" ' ConfigurationManager.AppSettings("SAPuserName")
            Session("UserPwd") = "" ' ConfigurationManager.AppSettings("SAPpassword")
            If objDA.CheckFirstLogin(objEn) = False Then
                ModalPopupExtender6.Enabled = True
                ModalPopupExtender6.Show()
            Else
                objPwd.EmpId = Session("UserCode").ToString()
                objPwd.Formid = Session("UserName").ToString()
                objLDA.InsertFirstLogin(objPwd)
                Response.Redirect("ESS/ESSHome.aspx", False)
            End If
            ' Response.Redirect("ESS/ESSHome.aspx", False)
        ElseIf (dbCon.AdminUserAuthentication(objEn)) = True Then
            Session("SAPCompany") = Application("DBName")
            Response.Redirect("AdminUIPages/AHome.aspx", False)
        Else
            ClientScript.RegisterStartupScript([GetType](), "Message", "<script>alert('login failed. UserName and Password does not matching')</script>")
        End If
    End Sub
    Protected Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If Session("UserCode") Is Nothing Then
            Response.Redirect("Login.aspx?sessionExpired=true", True)
        Else
            objPwd.EmpId = Session("UserCode").ToString()
            objPwd.OldPwd = txtoldpwd.Text.Trim()
            objPwd.NewPwd = txtnewpwd.Text.Trim()
            objPwd.ConfirmPwd = txtconfirmpwd.Text.Trim()
            objPwd.Formid = Session("UserName").ToString()
            If objPBL.Checkpassword(objPwd) = False Then
                Label1.Text = "Old password does not match.."
                ModalPopupExtender6.Show()
            Else
                objPDA.UpdatePassword(objPwd)
                objLDA.InsertFirstLogin(objPwd)
                Label1.Text = ""
                ModalPopupExtender6.Enabled = False
                objPwd.strmsg = "alert('Password changed Successfully ..')"
                ClientScript.RegisterStartupScript([GetType](), "Message", "<script>alert('Password changed Successfully ..')</script>")
            End If
        End If
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        ModalPopupExtender6.Enabled = False
    End Sub
End Class