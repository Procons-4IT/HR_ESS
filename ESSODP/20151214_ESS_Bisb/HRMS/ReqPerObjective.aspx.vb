Imports System
Imports EN
Imports DataAccess
Imports BusinessLogic
Public Class ReqPerObjective
    Inherits System.Web.UI.Page
    Dim objEN As ReqPersonelObjectiveEN = New ReqPersonelObjectiveEN()
    Dim objBL As ReqPersonelObjectiveBL = New ReqPersonelObjectiveBL()
    Dim dbcon As DBConnectionDA = New DBConnectionDA()
    Dim intTempID, strDocEntry, deptCode As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Session("UserCode") Is Nothing Then
                Response.Redirect("Login.aspx?sessionExpired=true", True)
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
            ViewState("EmpId") = objEN.EmpId
            LoadDatasource(objEN)
            txtempno.Text = objEN.EmpId
        End If
    End Sub

    Private Sub LoadDatasource(ByVal objen As ReqPersonelObjectiveEN)
        Try
            dbcon.ds = objBL.PageLoadBind(objen)
            If dbcon.ds.Tables(0).Rows.Count > 0 Then
                grdpoppeople.DataSource = dbcon.ds.Tables(0)
                grdpoppeople.DataBind()
            Else
                grdpoppeople.DataBind()
            End If
            If dbcon.ds.Tables(1).Rows.Count > 0 Then
                ddlcategory.DataSource = dbcon.ds.Tables(1)
                ddlcategory.DataTextField = "U_Z_CatName"
                ddlcategory.DataValueField = "U_Z_CatCode"
                ddlcategory.DataBind()
                ddlcategory.Items.Insert(0, "---Select---")
            Else
                ddlcategory.DataBind()
                ddlcategory.Items.Insert(0, "---Select---")
            End If
            If dbcon.ds.Tables(2).Rows.Count > 0 Then
                grdnewpeople.DataSource = dbcon.ds.Tables(2)
                grdnewpeople.DataBind()
            Else
                grdnewpeople.DataBind()
            End If
            If dbcon.ds.Tables(3).Rows.Count > 0 Then
                grddeletepeople.DataSource = dbcon.ds.Tables(3)
                grddeletepeople.DataBind()
            Else
                grddeletepeople.DataBind()
            End If
            If dbcon.ds.Tables(4).Rows.Count > 0 Then
                txtappid.Text = dbcon.ds.Tables(4).Rows(0)("firstName").ToString()
                txtFirstName.Text = dbcon.ds.Tables(4).Rows(0)("firstName").ToString()
                txtlastname.Text = dbcon.ds.Tables(4).Rows(0)("lastName").ToString()
                txtmiddleName.Text = dbcon.ds.Tables(4).Rows(0)("middleName").ToString()
                txtthirdname.Text = dbcon.ds.Tables(4).Rows(0)("U_Z_HR_ThirdName").ToString()
                If dbcon.ds.Tables(4).Rows(0)("position").ToString() <> "" Then
                    txtposition.Text = dbcon.ds.Tables(4).Rows(0)("Positionname").ToString()
                    lblposCode.Text = dbcon.ds.Tables(4).Rows(0)("position").ToString()
                End If
                If dbcon.ds.Tables(4).Rows(0)("dept").ToString() <> "" Then
                    txtdept.Text = dbcon.ds.Tables(4).Rows(0)("Deptname").ToString()
                    lbldeptcode.Text = dbcon.ds.Tables(4).Rows(0)("dept").ToString()
                End If
                txtoffphone.Text = dbcon.ds.Tables(4).Rows(0)("officeTel").ToString()
                txtmobile.Text = dbcon.ds.Tables(4).Rows(0)("mobile").ToString()
                txtemail.Text = dbcon.ds.Tables(4).Rows(0)("email").ToString()
                txtfax.Text = dbcon.ds.Tables(4).Rows(0)("fax").ToString()
                txthometel.Text = dbcon.ds.Tables(4).Rows(0)("homeTel").ToString()
                objen.Manager = dbcon.ds.Tables(4).Rows(0)("Manager").ToString()
                If objen.Manager <> 0 Then
                    dbcon.dss = objBL.Manager(objen)
                    If dbcon.dss.Tables(0).Rows.Count <> 0 Then
                        txtmanager.Text = dbcon.dss.Tables(0).Rows(0)("ManName").ToString()
                    End If
                End If
            End If
        Catch ex As Exception
            dbcon.strmsg = "alert('" & ex.Message & "')"
            mess(dbcon.strmsg)
        End Try
    End Sub
    Protected Sub grdnewpeople_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdnewpeople.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lbl As Label = CType(e.Row.FindControl("lblNStatus"), Label)
            If lbl.Text = "Pending" Then
                e.Row.Cells(0).Visible = True
            Else
                e.Row.Cells(0).Text = ""
            End If
        End If
    End Sub
    Private Sub mess(ByVal str As String)
        ' Dim Updated As UpdatePanel = CType(Master.FindControl("Update"), UpdatePanel)
        ScriptManager.RegisterStartupScript(Update, Update.[GetType](), "strmsg", dbcon.strmsg, True)
    End Sub
    Protected Sub grdnewpeople_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdnewpeople.RowDeleting

        Try
            objEN.DocEntry = DirectCast(grdnewpeople.Rows(e.RowIndex).FindControl("lblDocCode"), Label).Text
            objEN.EmpId = ViewState("EmpId").ToString()
            If objBL.DeleteObjective(objEN) = True Then
                dbcon.strmsg = "alert('People Objective Deleted successfully...')"
                mess(dbcon.strmsg)
            Else
                dbcon.strmsg = "alert('People Objective Deleted failed...')"
                mess(dbcon.strmsg)
            End If
            LoadDatasource(objEN)
        Catch ex As Exception
            dbcon.strmsg = "alert('" & ex.Message & "')"
            mess(dbcon.strmsg)
        End Try
    End Sub
    Protected Sub Btncallpop_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btncallpop.ServerClick

        Dim str1, str2, str3, str4 As String
        str1 = txtpopunique.Text.Trim()
        str2 = txtpoptno.Text.Trim()
        str3 = txttname.Text.Trim()
        str4 = txtsweight.Text.Trim()
        If txthidoption.Text = "People" Then
            If txtpoptno.Text.Trim() <> "" Then
                objEN.PeoObjCode = txtpopunique.Text.Trim()
                objEN.PeoObjName = txtpoptno.Text.Trim()
                txtpeocode.Text = objEN.PeoObjCode
                txtpeodescription.Text = objEN.PeoObjName
                dbcon.ds1 = objBL.SelectPeoObjCode(objEN)
                If dbcon.ds1.Tables(0).Rows.Count > 0 Then
                    If dbcon.ds1.Tables(0).Rows(0)("U_Z_PeoCategory").ToString() <> "" Then
                        ddlcategory.SelectedValue = dbcon.ds1.Tables(0).Rows(0)("U_Z_PeoCategory").ToString()
                    End If
                    txtweight.Text = dbcon.ds1.Tables(0).Rows(0)("U_Z_Weight").ToString()
                End If
                ModalPopupExtender6.Show()
            End If
        End If
    End Sub
    Protected Sub grdpoppeople_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdpoppeople.RowDataBound
        txtpoptno.Text = ""
        txtpopunique.Text = ""
        txthidoption.Text = ""
        txtsweight.Text = ""
        If e.Row.RowType = DataControlRowType.DataRow Then
            'e.Row.Attributes("onmouseover") = "javascript:SetMouseOver(this)"
            'e.Row.Attributes("onmouseout") = "javascript:SetMouseOut(this)"
            e.Row.Attributes.Add("onclick", "popupdisplay('People','" + (DataBinder.Eval(e.Row.DataItem, "U_Z_PeoobjCode")).ToString().Trim() + "','" + (DataBinder.Eval(e.Row.DataItem, "U_Z_PeoobjName")).ToString().Trim() + "','" + (DataBinder.Eval(e.Row.DataItem, "U_Z_PeoCategory")).ToString().Trim() + "');")
        End If
    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click

        Try
            If txtpeocode.Text.Trim() = "" Then
                dbcon.strmsg = "alert('Personal code is missing...')"
                mess(dbcon.strmsg)
                ModalPopupExtender6.Show()
            ElseIf txtweight.Text.Trim() = "" Then
                dbcon.strmsg = "alert('weight is missing...')"
                mess(dbcon.strmsg)
                ModalPopupExtender6.Show()
            Else
                objEN.SapCompany = Session("SAPCompany")
                objEN.DocEntry = dbcon.Getmaxcode("""U_PEOPLEOBJ""", """U_DocEntry""")
                objEN.EmpId = txtempno.Text.Trim()
                objEN.EmpName = Session("UserName").ToString()
                objEN.PeoObjCode = txtpeocode.Text.Trim()
                objEN.PeoObjName = txtpeodescription.Text.Trim()
                objEN.PeoObjCat = ddlcategory.SelectedValue
                objEN.PeoObjCatDesc = ddlcategory.SelectedItem.Text
                objEN.Weight = CDec(txtweight.Text.Trim())
                objEN.Remarks = txtpeoRemarks.Text.Trim()
                objEN.DeptCode = lbldeptcode.Text.Trim()
                objEN.AppStatus = dbcon.DocApproval("EmpLife", objEN.DeptCode)
                If objBL.InsertObjective(objEN) = True Then
                    intTempID = dbcon.GetTemplateID("EmpLife", objEN.DeptCode)
                    If intTempID <> "0" Then
                        dbcon.UpdateApprovalRequired("U_PEOPLEOBJ", "U_DocEntry", objEN.DocEntry, "Y", intTempID)
                        dbcon.InitialMessage("Personal Objectives", objEN.DocEntry, dbcon.DocApproval("EmpLife", objEN.DeptCode), intTempID, objEN.EmpName, "PerObj", objEN.SapCompany)
                    Else
                        dbcon.UpdateApprovalRequired("U_PEOPLEOBJ", "U_DocEntry", objEN.DocEntry, "N", intTempID)
                    End If
                    dbcon.strmsg = "alert('Personal Objective details added Succesfully...')"
                    mess(dbcon.strmsg)
                    ModalPopupExtender6.Hide()
                    LoadDatasource(objEN)
                    txtpeocode.Text = ""
                    txtpeodescription.Text = ""
                    ddlcategory.SelectedIndex = 0
                    txtweight.Text = ""
                    txtpeoRemarks.Text = ""
                Else
                    dbcon.strmsg = "alert('Personal Objective details added failed...')"
                    mess(dbcon.strmsg)
                    ModalPopupExtender6.Show()
                End If
            End If
        Catch ex As Exception
            dbcon.strmsg = "alert('" & ex.Message & "')"
            mess(dbcon.strmsg)
        End Try
    End Sub
End Class