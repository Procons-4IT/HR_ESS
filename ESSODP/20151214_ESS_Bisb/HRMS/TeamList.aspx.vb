Imports System
Imports BusinessLogic
Imports DataAccess
Imports EN
Public Class TeamList
    Inherits System.Web.UI.Page
    Dim objen As LineMgrAppraisalEN = New LineMgrAppraisalEN()
    Dim objBL As LineMgrAppraisalBL = New LineMgrAppraisalBL()
    Dim dbcon As DBConnectionDA = New DBConnectionDA()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Session("UserCode") Is Nothing Then
                Response.Redirect("Login.aspx?sessionExpired=true", True)
            Else
                objen.EmpId = Session("UserCode").ToString()
                dbcon.ds = objBL.BindPageLoadTeamList(objen)
                If dbcon.ds.Tables(0).Rows.Count > 0 Then
                    grdTeamList.DataSource = dbcon.ds.Tables(0)
                    grdTeamList.DataBind()
                Else
                    grdTeamList.DataBind()
                End If
            End If
        End If
    End Sub
    Protected Sub lbtnactivity_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If Session("UserCode") Is Nothing Then
                dbcon.strmsg = "alert('Your session is Expired...')"
                Page.RegisterStartupScript("msg", "<script>alert('" & dbcon.strmsg & "')</script>")
                Response.Redirect("Login.aspx?sessionExpired=true", True)
            Else
                Dim link As LinkButton = CType(sender, LinkButton)
                Dim gv As GridViewRow = CType((link.Parent.Parent), GridViewRow)
                Dim DocNo As Label = CType(gv.FindControl("lblempid"), Label)
                objen.StrType = Session("UserCode").ToString()
                objen.EmpId = DocNo.Text.Trim()
                LoadActivity(objen)
                ModalPopupExtender7.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub LoadActivity(ByVal objen As LineMgrAppraisalEN)
        Try
            dbcon.ds1 = objBL.LoadActivity(objen)
            If dbcon.ds1.Tables(0).Rows.Count > 0 Then
                grdRequesttohr.DataSource = dbcon.ds1.Tables(0)
                grdRequesttohr.DataBind()
                Label1.Text = ""
            Else
                grdRequesttohr.DataBind()
                Label1.Text = "Activity not found.."
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

  
End Class