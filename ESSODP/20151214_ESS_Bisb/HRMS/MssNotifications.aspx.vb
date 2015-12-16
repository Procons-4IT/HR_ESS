Imports System
Imports BusinessLogic
Imports DataAccess
Imports EN
Public Class MssNotifications
    Inherits System.Web.UI.Page
    Dim objen As PeopleObjApprovalEN = New PeopleObjApprovalEN()
    Dim objDA As PeopleObjApprovalDA = New PeopleObjApprovalDA()
    Dim objBL As PeopleObjApprovalBL = New PeopleObjApprovalBL()
    Dim dbcon As DBConnectionDA = New DBConnectionDA()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Session("UserCode") Is Nothing Then
                Response.Redirect("Login.aspx?sessionExpired=true", True)
            Else
                panelview.Visible = True
                mainGvbind()
            End If
        End If
    End Sub
    Private Sub mainGvbind()
        If Session("UserCode") Is Nothing Then
            Response.Redirect("Login.aspx?sessionExpired=true", True)
        Else
            objen.EmpId = Session("UserCode").ToString()
            dbcon.ds = objBL.NotificationBind(objen)
            If dbcon.ds.Tables(0).Rows.Count > 0 Then
                grdRequestApp.DataSource = dbcon.ds.Tables(0)
                grdRequestApp.DataBind()
            Else
                grdRequestApp.DataBind()
            End If
            objen.EmpId = objDA.getEmpIDforMangers(objen)
            If objen.EmpId <> "" Then
                objen.EmpCondition = """U_Z_HREmpID"" in (" & objen.EmpId & ") order by ""Code"" asc"
                objen.EmpCondition1 = """U_Z_HREmpID"" in (" & objen.EmpId & ") order by ""DocEntry"" asc"
                objen.EmpCondition2 = """U_Empid"" in (" & objen.EmpId & ")  Order by ""U_RequestCode"" asc"
                objen.EmpCondition3 = """U_Empid"" in (" & objen.EmpId & ")  Order by ""DocEntry"" asc"
                dbcon.ds1 = objBL.NotificationPageBind(objen)
                If dbcon.ds1.Tables(0).Rows.Count > 0 Then
                    grdreceived.DataSource = dbcon.ds1.Tables(0)
                    grdreceived.DataBind()
                Else
                    grdreceived.DataBind()
                End If
                If dbcon.ds1.Tables(1).Rows.Count > 0 Then
                    grdNewTraining.DataSource = dbcon.ds1.Tables(1)
                    grdNewTraining.DataBind()
                Else
                    grdNewTraining.DataBind()
                End If
                If dbcon.ds1.Tables(2).Rows.Count > 0 Then
                    grdrecApproval.DataSource = dbcon.ds1.Tables(2)
                    grdrecApproval.DataBind()
                Else
                    grdrecApproval.DataBind()
                End If
                If dbcon.ds1.Tables(3).Rows.Count > 0 Then
                    grdPeopleObject.DataSource = dbcon.ds1.Tables(3)
                    grdPeopleObject.DataBind()
                Else
                    grdPeopleObject.DataBind()
                End If
            End If

        End If
    End Sub
    Protected Sub grdNewTraining_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdNewTraining.PageIndexChanging
        grdNewTraining.PageIndex = e.NewPageIndex
        mainGvbind()
    End Sub

    Protected Sub grdPeopleObject_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdPeopleObject.PageIndexChanging
        grdPeopleObject.PageIndex = e.NewPageIndex
        mainGvbind()
    End Sub

    Protected Sub grdrecApproval_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdrecApproval.PageIndexChanging
        grdrecApproval.PageIndex = e.NewPageIndex
        mainGvbind()
    End Sub

    Protected Sub grdreceived_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdreceived.PageIndexChanging
        grdreceived.PageIndex = e.NewPageIndex
        mainGvbind()
    End Sub

    Protected Sub grdRequestApp_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdRequestApp.PageIndexChanging
        grdRequestApp.PageIndex = e.NewPageIndex
        mainGvbind()
    End Sub
End Class