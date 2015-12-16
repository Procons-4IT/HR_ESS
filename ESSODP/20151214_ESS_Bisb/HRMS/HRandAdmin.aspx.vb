Imports System
Imports BusinessLogic
Imports DataAccess
Imports EN
Public Class HRandAdmin
    Inherits System.Web.UI.Page
    Dim dbcon As DBConnectionDA = New DBConnectionDA()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Session("UserCode") Is Nothing Then
                Response.Redirect("Login.aspx?sessionExpired=true", True)
            Else
                MainGridBind()
            End If
        End If
    End Sub

    Private Sub MainGridBind()
        dbcon.ds = dbcon.PageloadBindHrAdmin()
        If dbcon.ds.Tables(0).Rows.Count > 0 Then
            grdRequestApp.DataSource = dbcon.ds.Tables(0)
            grdRequestApp.DataBind()
        Else
            grdRequestApp.DataBind()
        End If
        If dbcon.ds.Tables(1).Rows.Count > 0 Then
            grdHRGrevAcc.DataSource = dbcon.ds.Tables(1)
            grdHRGrevAcc.DataBind()
        Else
            grdHRGrevAcc.DataBind()
        End If
        If dbcon.ds.Tables(2).Rows.Count > 0 Then
            grdNewTraining.DataSource = dbcon.ds.Tables(2)
            grdNewTraining.DataBind()
        Else
            grdNewTraining.DataBind()
        End If
        If dbcon.ds.Tables(3).Rows.Count > 0 Then
            grdRecRequest.DataSource = dbcon.ds.Tables(3)
            grdRecRequest.DataBind()
        Else
            grdRecRequest.DataBind()
        End If
        If dbcon.ds.Tables(4).Rows.Count > 0 Then
            grdfinalapp.DataSource = dbcon.ds.Tables(4)
            grdfinalapp.DataBind()
        Else
            grdfinalapp.DataBind()
        End If
    End Sub


    Protected Sub grdfinalapp_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdfinalapp.PageIndexChanging
        grdfinalapp.PageIndex = e.NewPageIndex
        MainGridBind()
    End Sub

    Protected Sub grdHRGrevAcc_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdHRGrevAcc.PageIndexChanging
        grdHRGrevAcc.PageIndex = e.NewPageIndex
        MainGridBind()
    End Sub

    Protected Sub grdNewTraining_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdNewTraining.PageIndexChanging
        grdNewTraining.PageIndex = e.NewPageIndex
        MainGridBind()
    End Sub

    Protected Sub grdRecRequest_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdRecRequest.PageIndexChanging
        grdRecRequest.PageIndex = e.NewPageIndex
        MainGridBind()
    End Sub

    Protected Sub grdRequestApp_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdRequestApp.PageIndexChanging
        grdRequestApp.PageIndex = e.NewPageIndex
        MainGridBind()
    End Sub

End Class