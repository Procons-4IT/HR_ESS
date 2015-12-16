Imports System
Imports EN
Imports DataAccess
Imports BusinessLogic
Public Class ESSHome
    Inherits System.Web.UI.Page
    Dim objEN As HomeEN = New HomeEN()
    Dim objHDA As HomeBL = New HomeBL()
    Dim dbCon As DBConnectionDA = New DBConnectionDA()
    Dim objDA As DynamicApprovalDA = New DynamicApprovalDA()
    Dim objDEN As DynamicApprovalEN = New DynamicApprovalEN()
    Dim intTempID, strDocEntry, blnvalue, strError As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Session("UserCode") Is Nothing Then
                Response.Redirect("../Login.aspx?sessionExpired=true", True)
                'ElseIf Session("SAPCompany") Is Nothing Then
                '    strError = dbCon.Connection()
                '    If strError <> "Success" Then
                '        dbCon.strmsg = "alert('" & strError & "')"
                '        mess(dbCon.strmsg)
                '    Else
                '        Session("SAPCompany") = dbCon.objMainCompany
                '    End If
                'End If
            Else
                objEN.EmpId = Session("UserCode").ToString()
                AppGridbind(objEN)
                TrainingGvBind(objEN)
                AppPositionsBind(objEN)
                VacantPositionsBind(objEN)
                PopulateEmployee(objEN)
                PopulateEmployeeInternal(objEN)
                LeaveBalance(objEN)
            End If
        End If
    End Sub
    Private Sub LeaveBalance(ByVal objEN As HomeEN)
        dbCon.ds2 = objHDA.LeaveBalance(objEN)
        If dbCon.ds2.Tables(0).Rows.Count > 0 Then
            grdLeaveBal.DataSource = dbCon.ds2.Tables(0)
            grdLeaveBal.DataBind()
        Else
            grdLeaveBal.DataBind()
        End If
    End Sub
    Private Sub PopulateEmployeeInternal(ByVal objEN As HomeEN)
        dbCon.ds1 = objHDA.PopulateEmployeeInternal(objEN)
        If dbCon.ds1.Tables(0).Rows.Count <> 0 Then
            objEN.DeptCode = dbCon.ds1.Tables(0).Rows(0)("dept").ToString()
            TextBox1.Text = objEN.DeptCode
            txtempid.Text = dbCon.ds1.Tables(0).Rows(0)("empID").ToString()
            txtempname.Text = dbCon.ds1.Tables(0).Rows(0)("firstName").ToString() & " " & dbCon.ds1.Tables(0).Rows(0)("lastName").ToString()
            txtposid.Text = dbCon.ds1.Tables(0).Rows(0)("position").ToString()
            txtposname.Text = dbCon.ds1.Tables(0).Rows(0)("descriptio").ToString()
            If TextBox1.Text.Trim() <> "" Then
                txtdeptname.Text = objHDA.Department(objEN)
            End If
        End If
    End Sub
    Private Sub PopulateEmployee(ByVal objEN As HomeEN)
        dbCon.dss = objHDA.PopulateEmployee(objEN)
        If dbCon.dss.Tables(0).Rows.Count > 0 Then
            lbltano.Text = dbCon.dss.Tables(0).Rows(0)("TAEmpID").ToString()
            txtempno.Text = dbCon.dss.Tables(0).Rows(0)("empID").ToString()
            txtFirstName.Text = dbCon.dss.Tables(0).Rows(0)("firstName").ToString()
            txtlastname.Text = dbCon.dss.Tables(0).Rows(0)("lastName").ToString()
            txtmiddleName.Text = dbCon.dss.Tables(0).Rows(0)("middleName").ToString()
            txtthirdname.Text = dbCon.dss.Tables(0).Rows(0)("U_Z_HR_ThirdName").ToString()
            If dbCon.dss.Tables(0).Rows(0)("position").ToString() <> "" Then
                txtposition.Text = dbCon.dss.Tables(0).Rows(0)("position").ToString()
                txtposname.Text = dbCon.dss.Tables(0).Rows(0)("Positionname").ToString()
                lblposName.Text = dbCon.dss.Tables(0).Rows(0)("Positionname").ToString()
            End If
            If dbCon.dss.Tables(0).Rows(0)("dept").ToString() <> "" Then
                txtdept.Text = dbCon.dss.Tables(0).Rows(0)("dept").ToString()
                txtdeptname.Text = dbCon.dss.Tables(0).Rows(0)("Deptname").ToString()
                lbldeptName.Text = dbCon.dss.Tables(0).Rows(0)("Deptname").ToString()
            End If
            txtoffphone.Text = dbCon.dss.Tables(0).Rows(0)("officeTel").ToString()
            txtmobile.Text = dbCon.dss.Tables(0).Rows(0)("mobile").ToString()
            txtemail.Text = dbCon.dss.Tables(0).Rows(0)("email").ToString()
            txtfax.Text = dbCon.dss.Tables(0).Rows(0)("fax").ToString()
            txthometel.Text = dbCon.dss.Tables(0).Rows(0)("homeTel").ToString()
            lblgrade.Text = dbCon.dss.Tables(0).Rows(0)("U_Z_GrdName").ToString()
            lblSalScale.Text = dbCon.dss.Tables(0).Rows(0)("U_Z_HR_SalaryCode").ToString()
            If dbCon.dss.Tables(0).Rows(0)("Manager").ToString() <> "" Then
                objEN.Manager = dbCon.dss.Tables(0).Rows(0)("Manager").ToString()
                txtmanager.Text = objHDA.EmpManager(objEN)
            End If
        End If
    End Sub
    Private Sub AppGridbind(ByVal objEN As HomeEN)
        dbCon.ds = objHDA.AppNotification(objEN)
        If dbCon.ds.Tables(0).Rows.Count > 0 Then
            grdRequestApp.DataSource = dbCon.ds.Tables(0)
            grdRequestApp.DataBind()
        Else
            grdRequestApp.DataBind()
        End If
    End Sub
    Private Sub TrainingGvBind(ByVal objEN As HomeEN)
        dbCon.dss1 = objHDA.TrainingAgenda(objEN)
        If dbCon.dss1.Tables(0).Rows.Count > 0 Then
            GridView1.DataSource = dbCon.dss1.Tables(0)
            GridView1.DataBind()
        Else
            GridView1.DataBind()
        End If
    End Sub
    Private Sub AppPositionsBind(ByVal objEN As HomeEN)
        dbCon.dss2 = objHDA.AppPostions(objEN)
        If dbCon.dss2.Tables(0).Rows.Count > 0 Then
            grdScheduled.DataSource = dbCon.dss2.Tables(0)
            grdScheduled.DataBind()
        Else
            grdScheduled.DataBind()
        End If
    End Sub
    Private Sub VacantPositionsBind(ByVal objEN As HomeEN)
        dbCon.dss3 = objHDA.VacantPositions(objEN)
        If dbCon.dss3.Tables(0).Rows.Count > 0 Then
            GridView2.DataSource = dbCon.dss3.Tables(0)
            GridView2.DataBind()
            btnsubmit.Visible = True
            btnclose.Visible = True
        Else
            GridView2.DataBind()
            btnsubmit.Visible = False
            btnclose.Visible = False
        End If
    End Sub


    Protected Sub Btncallpop_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btncallpop.ServerClick
        Dim str1, str2, str3 As String
        str1 = txtpopunique.Text.Trim()
        str2 = txtpoptno.Text.Trim()
        str3 = txttname.Text.Trim()
        If txthidoption.Text = "Appraisal" Then
            If txtpoptno.Text.Trim() <> "" Then
                Response.Redirect(("../ESS/SelfApprais.aspx?AppraisalNo=" + txtpopunique.Text & "&Empno=") + txtpoptno.Text)
            End If
        End If
        If txthidoption.Text = "Training" Then
            If txtpoptno.Text.Trim() <> "" Then
                Response.Redirect(("../ESS/EApScheTrain.aspx?AgendaNo=" + txtpopunique.Text & "&Courseno=") + txtpoptno.Text)
            End If
        End If
    End Sub


    Private Sub grdRequestApp_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdRequestApp.RowDataBound
        txtpoptno.Text = ""
        txtpopunique.Text = ""
        txthidoption.Text = ""
        If e.Row.RowType = DataControlRowType.DataRow Then
            ' e.Row.Attributes("onmouseover") = "javascript:SetMouseOver(this)"
            ' e.Row.Attributes("onmouseout") = "javascript:SetMouseOut(this)"
            e.Row.Attributes.Add("onclick", "popupdisplay('Appraisal','" + (DataBinder.Eval(e.Row.DataItem, "DocEntry")).ToString().Trim() + "','" + (DataBinder.Eval(e.Row.DataItem, "U_Z_EmpId")).ToString().Trim() + "','" + (DataBinder.Eval(e.Row.DataItem, "U_Z_WStatus")).ToString().Trim() + "');")
        End If
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Response.Redirect("Home.aspx", False)
    End Sub
    Private Sub mess(ByVal str As String)
        ScriptManager.RegisterStartupScript(Update, Update.[GetType](), "strmsg", dbCon.strmsg, True)
    End Sub

    Private Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit.Click

        If Session("UserCode") Is Nothing Then
            Response.Redirect("../Login.aspx?sessionExpired=true", True)
        ElseIf Session("SAPCompany") Is Nothing Then
            If Session("EmpUserName").ToString() = "" Or Session("UserPwd").ToString() = "" Then
                strError = dbCon.Connection()
            Else
                strError = dbCon.Connection(Session("EmpUserName").ToString(), Session("UserPwd").ToString())
            End If
            If strError <> "Success" Then
                                   Page.ClientScript.RegisterStartupScript(Me.GetType(), "js", "<script>alert('" & strError & "')</script>")
            Else
                Session("SAPCompany") = dbCon.objMainCompany
            End If
        End If
        Dim Status As Boolean
        If GridView2.SelectedIndex <> -1 Then
            objEN.EmpId = txtempid.Text.Trim()
            objEN.EmpName = txtempname.Text.Trim()
            objEN.DeptCode = txtdept.Text.Trim()
            objEN.DeptName = txtdeptname.Text.Trim()
            objEN.EmpPosCode = txtposid.Text.Trim()
            objEN.EmpPosName = txtposname.Text.Trim()
            objEN.ReqDeptCode = DirectCast(GridView2.SelectedRow.FindControl("lblDeptcode"), Label).Text
            objEN.ReqDeptName = DirectCast(GridView2.SelectedRow.FindControl("lblDept"), Label).Text
            objEN.ReqposCode = DirectCast(GridView2.SelectedRow.FindControl("lblpositionCode"), Label).Text
            objEN.ReqPosName = DirectCast(GridView2.SelectedRow.FindControl("lblposition"), Label).Text
            objEN.RequestNo = DirectCast(GridView2.SelectedRow.FindControl("lblreqcode2"), Label).Text
            objEN.AppStatus = dbCon.DocApproval("Rec", objEN.ReqDeptCode)

            objEN.SapCompany = Session("SAPCompany")
            Status = objHDA.ApplyPosition(objEN)
            If Status = True Then
                AppGridbind(objEN)
                VacantPositionsBind(objEN)
                AppPositionsBind(objEN)
                intTempID = dbCon.GetTemplateID("Rec", objEN.ReqDeptCode)
                strDocEntry = objHDA.ReturnDocEntry()
                If intTempID <> "0" Then
                    dbCon.UpdateApprovalRequired("U_VACPOSITION", "U_DocEntry", strDocEntry, "Y", intTempID)
                    dbCon.InitialMessage("Internal Job Posting", strDocEntry, dbCon.DocApproval("Rec", objEN.ReqDeptCode), intTempID, objEN.EmpName, "IntAppReq", objEN.SapCompany)
                Else
                    dbCon.UpdateApprovalRequired("U_VACPOSITION", "U_DocEntry", strDocEntry, "N", intTempID)
                End If
                If objEN.AppStatus = "A" Then
                    objDEN.DocEntry = strDocEntry
                    objDEN.SapCompany = Session("SAPCompany")
                    objDEN.InternalEmpInd = txtempid.Text.Trim()
                    blnvalue = objDA.CreateApplicants(objDEN)
                    If blnvalue = "Success" Then
                        dbCon.strmsg = "alert('Vacant Position Request added successfully...')"
                        mess(dbCon.strmsg)
                    Else
                        dbCon.strmsg = blnvalue
                        mess(dbCon.strmsg)
                    End If
                End If
            End If
        Else
            dbCon.strmsg = "alert('Select the Vacant Positions...')"
            mess(dbCon.strmsg)
        End If
    End Sub
    Private Sub grdRequestApp_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdRequestApp.PageIndexChanging
        grdRequestApp.PageIndex = e.NewPageIndex
        objEN.EmpId = ViewState("EmpId").ToString()
        AppGridbind(objEN)
    End Sub

    Private Sub grdScheduled_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdScheduled.PageIndexChanging
        grdScheduled.PageIndex = e.NewPageIndex
        objEN.EmpId = ViewState("EmpId").ToString()
        AppPositionsBind(objEN)
    End Sub

    Private Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex
        objEN.EmpId = ViewState("EmpId").ToString()
        TrainingGvBind(objEN)
    End Sub

    Private Sub GridView2_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView2.PageIndexChanging
        GridView2.PageIndex = e.NewPageIndex
        objEN.EmpId = ViewState("EmpId").ToString()
        VacantPositionsBind(objEN)
    End Sub

    Private Sub grdLeaveBal_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdLeaveBal.PageIndexChanging
        grdLeaveBal.PageIndex = e.NewPageIndex
        objEN.EmpId = ViewState("EmpId").ToString()
        LeaveBalance(objEN)
    End Sub
End Class