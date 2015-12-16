Imports System
Imports System.Globalization
Imports BusinessLogic
Imports DataAccess
Imports EN
Public Class MNewTrainingReq
    Inherits System.Web.UI.Page
    Dim objEN As NewTrainingEN = New NewTrainingEN()
    Dim objBL As NewTrainingBL = New NewTrainingBL()
    Dim objDA As NewTrainingDA = New NewTrainingDA()
    Dim dbCon As DBConnectionDA = New DBConnectionDA()
    Dim strfromdt, Reqno, strTodt, Leaveduty, Travelon, returnon, ResumeOn, blValue, intTempID As String
    Dim Blflag As Boolean

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Session("UserCode") Is Nothing Then
                Response.Redirect("Login.aspx?sessionExpired=true", True)
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
            BindNewTraining(objEN)
            panelview.Visible = True
            PanelNewRequest.Visible = False
        End If
    End Sub
    Private Sub BindNewTraining(ByVal objen As NewTrainingEN)
        Try
            dbCon.ds = objBL.BindNewTraining(objen)
            If dbCon.ds.Tables(0).Rows.Count > 0 Then
                grdTrainingRequest.DataSource = dbCon.ds.Tables(0)
                grdTrainingRequest.DataBind()
            Else
                grdTrainingRequest.DataBind()
            End If
        Catch ex As Exception
            dbCon.strmsg = "alert('" & ex.Message & "')"
            mess(dbCon.strmsg)
        End Try
    End Sub

    Private Sub btnnew_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnnew.Click
        Try
            Clear()
            panelview.Visible = False
            PanelNewRequest.Visible = True
            btnWithdraw.Visible = False
            btnAdd.Visible = True
            Rblcert.SelectedValue = "2"
            RblTestinc.SelectedValue = "2"
            txtcode.Text = ""
            If Session("UserCode") Is Nothing Or Session("SAPCompany") Is Nothing Then
                Response.Redirect("Login.aspx?sessionExpired=true", True)
            Else
                objEN.EmpId = Session("UserCode").ToString()
                PopulateEmployee(objEN)
            End If

        Catch ex As Exception
            dbCon.strmsg = "alert('" & ex.Message & "')"
            mess(dbCon.strmsg)
        End Try
    End Sub
   
    Private Sub PopulateEmployee(ByVal objen As NewTrainingEN)
        objen = objBL.PopulateEmployee(objen)
        txtdeptcode.Text = objen.DeptCode
        txtempid.Text = objen.EmpId
        txtempname.Text = objen.EmpName
        txtposCode.Text = objen.PositionId
        txtpositionName.Text = objen.PositionName
        txtdeptName.Text = objen.DeptName
        txtreqcode.Text = dbCon.Getmaxcode("[@Z_HR_ONTREQ]", "DocEntry")
        txtreqdate.Text = Now.Date
    End Sub
    Private Sub mess(ByVal str As String)
        ScriptManager.RegisterStartupScript(Update, Update.[GetType](), "strmsg", dbCon.strmsg, True)
    End Sub
    Private Function Validation(ByVal objEn As NewTrainingEN) As Boolean
        Try
            Reqno = txttrtitle.Text.Trim()
            strfromdt = txtfrmdate.Text.Trim().Replace("-", "/")
            strTodt = txttodate.Text.Trim().Replace("-", "/")
            If strfromdt <> "" Then
                objEn.Fromdate = Date.ParseExact(strfromdt, "dd/MM/yyyy", CultureInfo.InvariantCulture)
            End If
            If strTodt <> "" Then
                objEn.Todate = Date.ParseExact(strTodt, "dd/MM/yyyy", CultureInfo.InvariantCulture)
            End If
            Leaveduty = txtLveDutyon.Text.Trim()
            Travelon = txtTravelon.Text.Trim()
            returnon = txtReturnon.Text.Trim()
            ResumeOn = txtresumes.Text.Trim()
            If Reqno = "" Then
                dbCon.strmsg = "alert('Enter Training Title...')"
                mess(dbCon.strmsg)
                Return False
            ElseIf strfromdt = "" Then
                dbCon.strmsg = "alert('Enter Training from date...')"
                mess(dbCon.strmsg)
                Return False
            ElseIf strTodt = "" Then
                dbCon.strmsg = "alert('Enter Training to date...')"
                mess(dbCon.strmsg)
                Return False
            ElseIf Now.Date > objEn.Fromdate Then
                dbCon.strmsg = "alert('Training From date must be greater than or equal to Current date...')"
                mess(dbCon.strmsg)
                Return False
            ElseIf objEn.Fromdate > objEn.Todate Then
                dbCon.strmsg = "alert('Training to date must be greater than or equal to Training from date...')"
                mess(dbCon.strmsg)
                Return False
            End If
            Return True
        Catch ex As Exception
            dbCon.strmsg = "alert('" & ex.Message & "')"
            mess(dbCon.strmsg)
            Return False
        End Try
    End Function

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click

        If Session("UserCode") Is Nothing Or Session("SAPCompany") Is Nothing Then
            Response.Redirect("../Login.aspx?sessionExpired=true", True)
        Else
            Try
                If Validation(objEN) = True Then
                    objEN.ReqCode = txtcode.Text.Trim()
                    objEN.EmpId = txtempid.Text.Trim()
                    objEN.EmpName = txtempname.Text.Trim()
                    objEN.DeptCode = txtdeptcode.Text.Trim()
                    objEN.DeptName = txtdeptName.Text.Trim()
                    objEN.PositionId = txtposCode.Text.Trim()
                    objEN.PositionName = txtpositionName.Text.Trim()
                    objEN.TrainTitle = txttrtitle.Text.Trim()
                    objEN.Justification = txtjust.Text.Trim()
                    If txtcost.Text.Trim <> "" Then
                        objEN.TrainCost = CDbl(txtcost.Text.Trim())
                    Else
                        objEN.TrainCost = 0.0
                    End If
                    If txtestexp.Text.Trim <> "" Then
                        objEN.Expense = CDbl(txtestexp.Text.Trim())
                    Else
                        objEN.Expense = 0.0
                    End If
                    strfromdt = txtfrmdate.Text.Trim().Replace("-", "/")
                    strTodt = txttodate.Text.Trim().Replace("-", "/")
                    If strfromdt <> "" Then
                        objEN.Fromdate = Date.ParseExact(strfromdt, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                    End If
                    If strTodt <> "" Then
                        objEN.Todate = Date.ParseExact(strTodt, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                    End If
                    objEN.TrainLoc = txtTrainloc.Text.Trim()
                    If txtBussDur.Text.Trim() <> "" Then
                        objEN.TrainDurBus = CDbl(txtBussDur.Text.Trim())
                    Else
                        objEN.TrainDurBus = 0.0
                    End If
                    If txtCalDur.Text.Trim() <> "" Then
                        objEN.TrainDurCal = CDbl(txtCalDur.Text.Trim())
                    Else
                        objEN.TrainDurCal = 0.0
                    End If
                    If txtawaybuss.Text.Trim() <> "" Then
                        objEN.AwayoffBus = CDbl(txtawaybuss.Text.Trim())
                    Else
                        objEN.AwayoffBus = 0.0
                    End If
                    objEN.TestAvail = Rblcert.SelectedValue
                    objEN.TestInclude = RblTestinc.SelectedValue
                    If txtLveDutyon.Text.Trim() <> "" Then
                        objEN.LveDutyOn = Date.ParseExact(txtLveDutyon.Text.Trim().Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture)
                    Else
                        objEN.LveDutyOn = "01/01/1900"
                    End If
                    If txtTravelon.Text.Trim() <> "" Then
                        objEN.TravelOn = Date.ParseExact(txtTravelon.Text.Trim().Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture)
                    Else
                        objEN.TravelOn = "01/01/1900"
                    End If
                    If txtReturnon.Text.Trim() <> "" Then
                        objEN.ReturnOn = Date.ParseExact(txtReturnon.Text.Trim().Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture)
                    Else
                        objEN.ReturnOn = "01/01/1900"
                    End If
                    If txtresumes.Text.Trim() <> "" Then
                        objEN.ResumesOn = Date.ParseExact(txtresumes.Text.Trim().Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture)
                    Else
                        objEN.ResumesOn = "01/01/1900"
                    End If
                    objEN.Notes = txtComments.Text.Trim()
                    objEN.Status = dbCon.DocApproval("Train", txtempid.Text.Trim())
                    objEN.SapCompany = Session("SAPCompany")
                    blValue = objBL.SaveNewTrainingRequest(objEN)
                    dbCon.strmsg = "alert('" & blValue & "')"
                    mess(dbCon.strmsg)
                    panelview.Visible = True
                    PanelNewRequest.Visible = False
                    objEN.EmpId = Session("UserCode").ToString()
                    BindNewTraining(objEN)
                    intTempID = dbCon.GetTemplateID("Train", objEN.EmpId)
                    If intTempID <> "0" Then
                        objEN.ReqCode = txtreqcode.Text.Trim()
                        dbCon.UpdateApprovalRequired("@Z_HR_ONTREQ", "DocEntry", objEN.ReqCode, "Y", intTempID)
                        dbCon.InitialMessage("New Training Request", objEN.ReqCode, dbCon.DocApproval("Train", txtempid.Text.Trim()), intTempID, objEN.EmpName, "NewTra", objEN.SapCompany)
                    Else
                        objEN.ReqCode = txtreqcode.Text.Trim()
                        dbCon.UpdateApprovalRequired("@Z_HR_ONTREQ", "DocEntry", objEN.ReqCode, "N", intTempID)
                    End If
                    Clear()
                End If
            Catch ex As Exception
                dbCon.strmsg = "alert('" & ex.Message & "')"
                mess(dbCon.strmsg)
            End Try
        End If
    End Sub
    Protected Sub lbtndocnum_Click(ByVal sender As Object, ByVal e As EventArgs)

        If Session("UserCode") Is Nothing Or Session("SAPCompany") Is Nothing Then
            dbCon.strmsg = "alert('Your session is Expired...')"
            mess(dbCon.strmsg)
            Response.Redirect("Login.aspx?sessionExpired=true", True)
        Else
            Dim link As LinkButton = CType(sender, LinkButton)
            Dim gv As GridViewRow = CType((link.Parent.Parent), GridViewRow)
            Dim DocNo As LinkButton = CType(gv.FindControl("lbtndocnum"), LinkButton)
            panelview.Visible = False
            PanelNewRequest.Visible = True
            objEN.EmpId = Session("UserCode").ToString()
            objEN.ReqCode = DocNo.Text.Trim()
            populateTrainRequest(objEN)
        End If
    End Sub
    Private Sub populateTrainRequest(ByVal objen As NewTrainingEN)
        Try
            dbCon.dss1 = objBL.populateTrainRequest(objen)
            If dbCon.dss1.Tables(0).Rows.Count > 0 Then
                txtcode.Text = dbCon.dss1.Tables(0).Rows(0)("DocEntry").ToString()
                txtreqcode.Text = dbCon.dss1.Tables(0).Rows(0)("DocEntry").ToString()
                txtreqdate.Text = dbCon.dss1.Tables(0).Rows(0)("U_Z_ReqDate").ToString()
                txtfrmdate.Text = dbCon.dss1.Tables(0).Rows(0)("U_Z_TrainFrdt").ToString()
                txttodate.Text = dbCon.dss1.Tables(0).Rows(0)("U_Z_TrainTodt").ToString()
                txtempid.Text = dbCon.dss1.Tables(0).Rows(0)("U_Z_HREmpID").ToString()
                txtempname.Text = dbCon.dss1.Tables(0).Rows(0)("U_Z_HREmpName").ToString()
                txtdeptName.Text = dbCon.dss1.Tables(0).Rows(0)("U_Z_DeptName").ToString()
                txtpositionName.Text = dbCon.dss1.Tables(0).Rows(0)("U_Z_PosiName").ToString()
                txttrtitle.Text = dbCon.dss1.Tables(0).Rows(0)("U_Z_CourseName").ToString()
                txtjust.Text = dbCon.dss1.Tables(0).Rows(0)("U_Z_CourseDetails").ToString()
                txtposCode.Text = dbCon.dss1.Tables(0).Rows(0)("U_Z_PosiCode").ToString()
                txtdeptcode.Text = dbCon.dss1.Tables(0).Rows(0)("U_Z_DeptCode").ToString()
                txtcost.Text = dbCon.dss1.Tables(0).Rows(0)("U_Z_TrainCost").ToString()
                txtestexp.Text = dbCon.dss1.Tables(0).Rows(0)("U_Z_EstExpe").ToString()
                txtTrainloc.Text = dbCon.dss1.Tables(0).Rows(0)("U_Z_TrainLoc").ToString()
                txtBussDur.Text = dbCon.dss1.Tables(0).Rows(0)("U_Z_BussDays").ToString()
                txtCalDur.Text = dbCon.dss1.Tables(0).Rows(0)("U_Z_CalDays").ToString()
                txtawaybuss.Text = dbCon.dss1.Tables(0).Rows(0)("U_Z_AwayOff").ToString()
                Rblcert.SelectedValue = dbCon.dss1.Tables(0).Rows(0)("U_Z_CerTestAvail").ToString()
                RblTestinc.SelectedValue = dbCon.dss1.Tables(0).Rows(0)("U_Z_CerTestIncl").ToString()
                txtLveDutyon.Text = dbCon.dss1.Tables(0).Rows(0)("U_Z_LveDuty").ToString()
                txtTravelon.Text = dbCon.dss1.Tables(0).Rows(0)("U_Z_TravelOn").ToString()
                txtReturnon.Text = dbCon.dss1.Tables(0).Rows(0)("U_Z_ReturnOn").ToString()
                txtresumes.Text = dbCon.dss1.Tables(0).Rows(0)("U_Z_ResumeOn").ToString()
                txtComments.Text = dbCon.dss1.Tables(0).Rows(0)("U_Z_Notes").ToString()
                Blflag = dbCon.WithDrawStatus("NewTra", txtcode.Text.Trim())
                If Blflag = True Or dbCon.dss1.Tables(0).Rows(0)("U_Z_AppStatus").ToString() <> "P" Then
                    btnWithdraw.Visible = False
                    btnAdd.Visible = False
                Else
                    btnWithdraw.Visible = True
                    btnAdd.Visible = True
                End If
            End If
        Catch ex As Exception
            dbCon.strmsg = "alert('" & ex.Message & "')"
            mess(dbCon.strmsg)
        End Try
    End Sub
    Private Sub Clear()
        txtcode.Text = ""
        txtreqcode.Text = ""
        txtreqdate.Text = ""
        txtfrmdate.Text = ""
        txttodate.Text = ""
        txtempid.Text = ""
        txtempname.Text = ""
        txtdeptName.Text = ""
        txtpositionName.Text = ""
        txttrtitle.Text = ""
        txtjust.Text = ""
        txtposCode.Text = ""
        txtdeptcode.Text = ""
        txtcost.Text = ""
        txtestexp.Text = ""
        txtTrainloc.Text = ""
        txtBussDur.Text = ""
        txtCalDur.Text = ""
        txtawaybuss.Text = ""
        Rblcert.SelectedValue = "2"
        RblTestinc.SelectedValue = "2"
        txtLveDutyon.Text = ""
        txtTravelon.Text = ""
        txtReturnon.Text = ""
        txtresumes.Text = ""
        txtComments.Text = ""
    End Sub

    Private Sub btnWithdraw_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnWithdraw.Click
        Try
            objEN.ReqCode = txtreqcode.Text.Trim()
            objEN.EmpId = txtempid.Text.Trim()
            blValue = objBL.WithdrawRequest(objEN)
            dbCon.strmsg = "alert('" & blValue & "')"
            mess(dbCon.strmsg)
            panelview.Visible = True
            PanelNewRequest.Visible = False
            objEN.EmpId = Session("UserCode").ToString()
            BindNewTraining(objEN)
            Clear()
        Catch ex As Exception
            dbCon.strmsg = "alert('" & ex.Message & "')"
            mess(dbCon.strmsg)
        End Try
    End Sub

    Private Sub btncancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncancel.Click
        panelview.Visible = True
        PanelNewRequest.Visible = False
        Clear()
    End Sub

    Private Sub grdTrainingRequest_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdTrainingRequest.PageIndexChanging
        grdTrainingRequest.PageIndex = e.NewPageIndex
        objEN.EmpId = ViewState("EmpId").ToString()
        BindNewTraining(objEN)
    End Sub
    Protected Sub lbtAppHistory_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If Session("UserCode") Is Nothing Or Session("SAPCompany") Is Nothing Then
                dbCon.strmsg = "alert('Your session is Expired...')"
                mess(dbCon.strmsg)
                Response.Redirect("Login.aspx?sessionExpired=true", True)
            Else
                Dim link As LinkButton = CType(sender, LinkButton)
                Dim gv As GridViewRow = CType((link.Parent.Parent), GridViewRow)
                Dim DocNo As LinkButton = CType(gv.FindControl("lbtndocnum"), LinkButton)
                LoadActivity(DocNo.Text.Trim(), "NewTra")
                ModalPopupExtender7.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub LoadActivity(ByVal RefCode As String, ByVal DocType As String)
        Try
            dbCon.ds4 = dbCon.ViewHistory(RefCode, DocType)
            If dbCon.ds4.Tables(0).Rows.Count > 0 Then
                grdRequesttohr.DataSource = dbCon.ds4.Tables(0)
                grdRequesttohr.DataBind()
                Label1.Text = ""
            Else
                grdRequesttohr.DataBind()
                Label1.Text = "Approval History not found.."
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grdTrainingRequest_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdTrainingRequest.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim LiDocNo As LinkButton = CType(e.Row.FindControl("lbtndocnum"), LinkButton)
            Dim Liview As LinkButton = CType(e.Row.FindControl("lbtAppHistory"), LinkButton)
            Blflag = dbCon.WithDrawStatus("NewTra", LiDocNo.Text.Trim())
            If Blflag = True Then
                Liview.Visible = True
            Else
                Liview.Visible = False
            End If
        End If
    End Sub
End Class