Imports System
Imports DataAccess
Imports BusinessLogic
Imports EN
Public Class Persobj
    Inherits System.Web.UI.Page
    Dim objEN As ReqPersonelObjectiveEN = New ReqPersonelObjectiveEN()
    Dim objBL As PeopleObjectiveDA = New PeopleObjectiveDA()
    Dim dbcon As DBConnectionDA = New DBConnectionDA()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Session("UserCode") Is Nothing Then
                Response.Redirect("../Login.aspx?sessionExpired=true", True)
            ElseIf Session("SAPCompany") Is Nothing Then
                If Session("EmpUserName").ToString() = "" Or Session("UserPwd").ToString() = "" Then
                    strError = dbcon.Connection()
                Else
                    strError = dbcon.Connection(Session("EmpUserName").ToString(), Session("UserPwd").ToString())
                End If
                If strError <> "Success" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "js", "<script>alert('" & strError & "')</script>")
                Else
                    Session("SAPCompany") = dbcon.objMainCompany
                End If
            End If
            objEN.EmpId = Session("UserCode").ToString()
            LoadDatasource(objEN)
            txtempno.Text = objEN.EmpId
        End If
    End Sub
    Private Sub LoadDatasource(ByVal objen As ReqPersonelObjectiveEN)
        dbcon.ds = objBL.PageLoadBind(objen)
        If dbcon.ds.Tables(0).Rows.Count > 0 Then
            Session("People") = dbcon.ds.Tables(0)
            grdPeople.DataSource = dbcon.ds.Tables(0)
            grdPeople.DataBind()
        Else
            grdPeople.DataBind()
        End If
        If dbcon.ds.Tables(1).Rows.Count > 0 Then
            txtappid.Text = dbcon.ds.Tables(1).Rows(0)("firstName").ToString()
            txtFirstName.Text = dbcon.ds.Tables(1).Rows(0)("firstName").ToString()
            txtlastname.Text = dbcon.ds.Tables(1).Rows(0)("lastName").ToString()
            txtmiddleName.Text = dbcon.ds.Tables(1).Rows(0)("middleName").ToString()
            txtthirdname.Text = dbcon.ds.Tables(1).Rows(0)("U_Z_HR_ThirdName").ToString()
            If dbcon.ds.Tables(1).Rows(0)("position").ToString() <> "" Then
                txtposition.Text = dbcon.ds.Tables(1).Rows(0)("Positionname").ToString()
            End If
            If dbcon.ds.Tables(1).Rows(0)("dept").ToString() <> "" Then
                txtdept.Text = dbcon.ds.Tables(1).Rows(0)("Deptname").ToString()
            End If
            txtoffphone.Text = dbcon.ds.Tables(1).Rows(0)("officeTel").ToString()
            txtmobile.Text = dbcon.ds.Tables(1).Rows(0)("mobile").ToString()
            txtemail.Text = dbcon.ds.Tables(1).Rows(0)("email").ToString()
            txtfax.Text = dbcon.ds.Tables(1).Rows(0)("fax").ToString()
            txthometel.Text = dbcon.ds.Tables(1).Rows(0)("homeTel").ToString()
            objen.Manager = dbcon.ds.Tables(1).Rows(0)("Manager").ToString()
            If objen.Manager <> 0 Then
                dbcon.dss = objBL.Manager(objen)
                If dbcon.dss.Tables(0).Rows.Count <> 0 Then
                    txtmanager.Text = dbcon.dss.Tables(0).Rows(0)("ManName").ToString()
                End If
            End If
        End If

    End Sub

    'Protected Sub grdPeople_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdPeople.RowDeleting
    '    objEN.DocEntry = DirectCast(grdPeople.Rows(e.RowIndex).FindControl("lblCode"), Label).Text
    '    objEN.PeoObjCode = DirectCast(grdPeople.Rows(e.RowIndex).FindControl("lblPeCode"), Label).Text
    '    objEN.PeoObjName = DirectCast(grdPeople.Rows(e.RowIndex).FindControl("lblPName"), Label).Text
    '    objEN.PeoObjCat = DirectCast(grdPeople.Rows(e.RowIndex).FindControl("lblPCatCode"), Label).Text
    '    objEN.Weight = DirectCast(grdPeople.Rows(e.RowIndex).FindControl("lblPWeight"), Label).Text
    '    objEN.Remarks = DirectCast(grdPeople.Rows(e.RowIndex).FindControl("lblPRemarks"), Label).Text
    '    objEN.EmpId = txtempno.Text.Trim()
    '    If objBL.DeleteObjective(objEN) = True Then
    '        dbcon.strmsg = "alert('People Objective Deleted successfully...')"
    '        mess(dbcon.strmsg)
    '    Else
    '        dbcon.strmsg = "alert('People Objective Deleted failed...')"
    '        mess(dbcon.strmsg)
    '    End If
    '    LoadDatasource(objEN)
    'End Sub
    Private Sub mess(ByVal str As String)
        ScriptManager.RegisterStartupScript(Update, Update.[GetType](), "strmsg", str, True)
    End Sub

End Class