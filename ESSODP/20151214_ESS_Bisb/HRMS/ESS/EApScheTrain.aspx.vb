Imports System
Imports BusinessLogic
Imports DataAccess
Imports EN
Public Class EApScheTrain
    Inherits System.Web.UI.Page
    Dim objEN As TrainingRequestEN = New TrainingRequestEN()
    Dim objBL As TrainingRequestBL = New TrainingRequestBL()
    Dim objDA As TrainingRequestDA = New TrainingRequestDA()
    Dim dbCon As DBConnectionDA = New DBConnectionDA()
    Dim intTempID As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            objEN.AgendaCode = Request.QueryString("AgendaNo")
            objEN.CourseCode = Request.QueryString("Courseno")
            ViewState("Traincode") = objEN.AgendaCode
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
            objEN.EmpId = Session("UserCode").ToString()
            ViewState("EmpId") = objEN.EmpId
            If objEN.AgendaCode <> "" And objEN.CourseCode <> "" Then
                btnnew.Visible = False
                Empdetails.Visible = True
                PopulateEmployee(objEN)
            Else
                btnnew.Visible = False
                Empdetails.Visible = True
                PopulateEmployee(objEN)
            End If
            objEN.PositionId = txtposid.Text.Trim()
            ApplicableTraining(objEN)
            ScheduledTraining(objEN)
            NewTraining(objEN)
            CourseAcquired(objEN)
        End If
    End Sub

    Private Sub ApplicableTraining(ByVal objen As TrainingRequestEN)
        dbCon.ds = objBL.ApplicableTraining(objen)
        If dbCon.ds.Tables(0).Rows.Count > 0 Then
            GridView1.DataSource = dbCon.ds
            GridView1.DataBind()
        Else
            GridView1.DataBind()
        End If
    End Sub
    Private Sub CourseAcquired(ByVal objen As TrainingRequestEN)
        dbCon.ds4 = objBL.CourseAcquired(objen)
        If dbCon.ds4.Tables(0).Rows.Count > 0 Then
            grdCourseAcq.DataSource = dbCon.ds4
            grdCourseAcq.DataBind()
        Else
            grdCourseAcq.DataBind()
        End If
    End Sub
    Private Sub ScheduledTraining(ByVal objen As TrainingRequestEN)
        dbCon.ds1 = objBL.ScheduledTraining(objen)
        If dbCon.ds1.Tables(0).Rows.Count > 0 Then
            grdScheduled.DataSource = dbCon.ds1
            grdScheduled.DataBind()
        Else
            grdScheduled.DataBind()
        End If
    End Sub
    Private Sub NewTraining(ByVal objen As TrainingRequestEN)
        dbCon.dss1 = objBL.NewTraining(objen)
        If dbCon.dss1.Tables(0).Rows.Count > 0 Then
            grdNewTraining.DataSource = dbCon.dss1
            grdNewTraining.DataBind()
        Else
            grdNewTraining.DataBind()
        End If
    End Sub

    Private Sub PopulateEmployee(ByVal objen As TrainingRequestEN)
        objen = objBL.PopulateEmployee(objen)
        txtdept.Text = objen.DeptCode
        txtempid.Text = objen.EmpId
        txtempname.Text = objen.EmpName
        txtposid.Text = objen.PositionId
        txtposname.Text = objen.PosName
        txtdeptname.Text = objen.DeptName
    End Sub

    Private Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging

        GridView1.PageIndex = e.NewPageIndex
        objEN.EmpId = ViewState("EmpId").ToString()
        objEN.PositionId = txtposid.Text.Trim()
        ApplicableTraining(objEN)
    End Sub
    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "Select" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GridView1.Rows(index)
            btnsubmit.Visible = True
            btnclose.Visible = True
        End If
    End Sub

    Private Sub mess(ByVal str As String)
        ScriptManager.RegisterStartupScript(Update, Update.[GetType](), "strmsg", str, True)
    End Sub
    Public Function GetDateInYYYYMMDD(ByVal dt As String) As String
        Dim str(3) As String
        str = dt.Split("/")
        Dim tempdt As String = String.Empty
        For i As Integer = 2 To 0 Step -1
            tempdt += str(i) + "-"
        Next
        tempdt = tempdt.Substring(0, 10)
        Return tempdt
    End Function
    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit.Click

        If Session("UserCode") Is Nothing Or Session("SAPCompany") Is Nothing Then
            Response.Redirect("../Login.aspx?sessionExpired=true", True)
        Else
            objEN.PositionId = txtposid.Text.Trim()
            objEN.PosName = txtposname.Text.Trim()
            objEN.EmpId = Session("UserCode").ToString()
            objEN.EmpName = txtempname.Text.Trim()
            objEN.DeptCode = txtdept.Text.Trim()
            objEN.DeptName = txtdeptname.Text.Trim()
            If GridView1.SelectedIndex <> -1 Then
                objEN.AgendaCode = DirectCast(GridView1.SelectedRow.FindControl("lblTtracode"), Label).Text
                objEN.SapComapny = Session("SAPCompany")
                dbCon.strmsg = objBL.AddUDT(objEN)
                If dbCon.strmsg = "Success" Then
                    dbCon.strmsg = "alert('Training Request added successfully...')"
                    mess(dbCon.strmsg)
                    TabPanel2.Visible = True
                    TabPanel1.Visible = True
                    btnsubmit.Visible = True
                Else
                    dbCon.strmsg = "alert('" & dbCon.strmsg & "')"
                    mess(dbCon.strmsg)
                    TabPanel2.Visible = True
                    TabPanel1.Visible = True
                    btnsubmit.Visible = True
                End If
            Else
                dbCon.strmsg = "alert('Select the Applicable training agenda...')"
                mess(dbCon.strmsg)
            End If
            objEN.PositionId = txtposid.Text.Trim()
            objEN.EmpId = ViewState("EmpId").ToString()
            ApplicableTraining(objEN)
            ScheduledTraining(objEN)
            NewTraining(objEN)
        End If
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Response.Redirect("~/ESS/ESSHome.aspx", False)
    End Sub
    Private Sub grdNewTraining_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdNewTraining.PageIndexChanging

        grdNewTraining.PageIndex = e.NewPageIndex
        objEN.EmpId = ViewState("EmpId").ToString()
        objEN.PositionId = txtposid.Text.Trim()
        NewTraining(objEN)
    End Sub

    Private Sub grdScheduled_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdScheduled.PageIndexChanging

        grdScheduled.PageIndex = e.NewPageIndex
        objEN.EmpId = ViewState("EmpId").ToString()
        objEN.PositionId = txtposid.Text.Trim()
        ScheduledTraining(objEN)
    End Sub
    Protected Sub grdScheduled_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdScheduled.RowCommand
        If e.CommandName = "Select" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = grdScheduled.Rows(index)
            btnsubmit.Visible = True
            btnclose.Visible = True
        End If
    End Sub
    Protected Sub btnWithdrawReq_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnWithdrawReq.Click

        Dim strStatus As String
        If grdScheduled.SelectedIndex <> -1 Then
            Dim strTracode As String = DirectCast(grdScheduled.SelectedRow.FindControl("lbltracode"), Label).Text
            strStatus = DirectCast(grdScheduled.SelectedRow.FindControl("lblStatus"), Label).Text
            If strStatus.ToUpper() <> "PENDING" Then
                dbCon.strmsg = "alert('Training Request already approved. You can not withdraw the request')"
                mess(dbCon.strmsg)
            Else
                objEN.ApplyCode = DirectCast(grdScheduled.SelectedRow.FindControl("lblAppCode"), Label).Text
                objDA.WithdrawTraining(objEN)
                objEN.EmpId = ViewState("EmpId").ToString()
                objEN.PositionId = txtposid.Text.Trim()
                ScheduledTraining(objEN)
                ApplicableTraining(objEN)
            End If
        End If
    End Sub

    Private Sub GridView1_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(sender, "Select$" & e.Row.RowIndex.ToString))
        End If
    End Sub

    Private Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(sender, "Select$" & e.Row.RowIndex.ToString))
        End If
    End Sub

    Private Sub grdCourseAcq_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdCourseAcq.PageIndexChanging

        grdCourseAcq.PageIndex = e.NewPageIndex
        objEN.EmpId = ViewState("EmpId").ToString()
        CourseAcquired(objEN)
    End Sub
End Class