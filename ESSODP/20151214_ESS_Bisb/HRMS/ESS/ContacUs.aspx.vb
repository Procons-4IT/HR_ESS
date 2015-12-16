Imports System
Imports BusinessLogic
Imports DataAccess
Imports EN
Public Class ContacUs
    Inherits System.Web.UI.Page
    Dim dbcon As DBConnectionDA = New DBConnectionDA()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Session("UserCode") Is Nothing Then
                Response.Redirect("../Login.aspx?sessionExpired=true", True)
            Else
                MainGridBind()
            End If
        End If
    End Sub
    Private Sub MainGridBind()
        dbcon.ds = dbcon.PageLoadbindContactUs()
        If dbcon.ds.Tables(0).Rows.Count > 0 Then
            RptContactus.DataSource = dbcon.ds.Tables(0)
            RptContactus.DataBind()
        Else
            RptContactus.DataBind()
        End If
    End Sub

End Class