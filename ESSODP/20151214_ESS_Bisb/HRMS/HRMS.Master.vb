Imports System
Imports EN
Imports DataAccess
Public Class HRMS
    Inherits System.Web.UI.MasterPage
    Dim objDA As LoginDA = New LoginDA()
    Dim dbCon As DBConnectionDA = New DBConnectionDA()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Session("UserName") Is Nothing Then
                Response.Redirect("Login.aspx?sessionExpired=true", True)
            Else
                lbluser.Text = Session("UserName").ToString()
            End If
        End If
        CompanyAddress()
    End Sub
    Private Sub CompanyAddress()
        dbCon.ds = objDA.CompanyAddress
        If dbCon.ds.Tables(0).Rows.Count > 0 Then
            lblcompany.Text = dbCon.ds.Tables(0).Rows(0)(0).ToString()
            lblstreet.Text = dbCon.ds.Tables(0).Rows(0)(1).ToString()
            lblblock.Text = dbCon.ds.Tables(0).Rows(0)(2).ToString()
            lblstate.Text = dbCon.ds.Tables(0).Rows(0)(3).ToString()
        End If
    End Sub
    ''' <summary>
    ''' A method for signing out the login user
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub lbtnlogout_Click(sender As Object, e As EventArgs)
        FormsAuthentication.SignOut()
    End Sub
End Class