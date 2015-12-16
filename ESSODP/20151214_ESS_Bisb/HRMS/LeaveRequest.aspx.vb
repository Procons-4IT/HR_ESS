Imports System
Imports System.Globalization
Imports BusinessLogic
Imports DataAccess
Imports EN

Public Class LeaveRequest
    Inherits System.Web.UI.Page
    Dim objEN As LeaveRequestEN = New LeaveRequestEN()
    Dim objBL As LeaveRequestBL = New LeaveRequestBL()
    Dim dbCon As DBConnectionDA = New DBConnectionDA()
    Dim objCom As CommonFunctions = New CommonFunctions()
    Dim objVal As DynamicApprovalDA = New DynamicApprovalDA()
    Dim intTempID, strCode As String
    Dim intDiff As Double
    Dim blnValue As Boolean
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
            objEN.Empid = Session("UserCode").ToString()
            ViewState("EmpId") = objEN.Empid
            objEN.Year = Now.Year
            FillLveType(objEN)
            PageLoadBind(objEN)
            panelview.Visible = True
            PanelNewRequest.Visible = False
        End If
    End Sub
    Private Sub FillLveType(ByVal objen As LeaveRequestEN)
        Try
            dbCon.dss1 = objBL.FillLeavetype(objen)
            If dbCon.dss1.Tables(0).Rows.Count > 0 Then
                grdLeaveType.DataSource = dbCon.dss1.Tables(0)
                grdLeaveType.DataBind()
            Else
                grdLeaveType.DataBind()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub PageLoadBind(ByVal objen As LeaveRequestEN)
        Try
            dbCon.ds = objBL.PageLoadBind(objen)
            If dbCon.ds.Tables(0).Rows.Count > 0 Then
                grdLeaveRequest.DataSource = dbCon.ds.Tables(0)
                grdLeaveRequest.DataBind()
            Else
                grdLeaveRequest.DataBind()
            End If
            If dbCon.ds.Tables(1).Rows.Count > 0 Then
                grdSummary.DataSource = dbCon.ds.Tables(1)
                grdSummary.DataBind()
            Else
                grdSummary.DataBind()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Protected Sub Btncallpop_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btncallpop.ServerClick
        Dim str1, str2, str3 As String
        str1 = txtpopunique.Text.Trim()
        str2 = txtpoptno.Text.Trim()
        str3 = txttname.Text.Trim()
        If txthidoption.Text = "Leave" Then
            If txtpoptno.Text.Trim() <> "" Then
                txtlvecode.Text = txtpopunique.Text.Trim()
                txtlveName.Text = txtpoptno.Text.Trim()
                objEN.Empid = Session("UserCode").ToString()
                objEN.LeaveCode = txtlvecode.Text.Trim()
                If txtfrmdate.Text = "" Then
                    objEN.Year = Now.Date.Year
                Else
                    objEN.FromDate = Date.ParseExact(txtfrmdate.Text.Trim().Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture)
                    objEN.Year = objEN.FromDate.Year
                End If
                objEN.RStatus = objBL.getCutoff(objEN)
                If objEN.RStatus = "" And objEN.RStatus = Nothing Then
                    txtcutoff.Text = "N"
                Else
                    txtcutoff.Text = objEN.RStatus
                End If
                objEN.Status = objBL.GetLeaveBalance(objEN)
                If objEN.Status = "" And objEN.Status = Nothing Then
                    txtlveBal.Text = 0.0
                Else
                    txtlveBal.Text = objEN.Status
                End If
            End If
        End If
    End Sub
    Protected Sub grdLeaveType_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdLeaveType.RowDataBound
        txtpoptno.Text = ""
        txtpopunique.Text = ""
        txthidoption.Text = ""
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onclick", "popupdisplay('Leave','" + (DataBinder.Eval(e.Row.DataItem, "Code")).ToString().Trim() + "','" + (DataBinder.Eval(e.Row.DataItem, "Name")).ToString().Trim() + "');")
        End If
    End Sub
    Protected Sub btnnew_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnnew.Click
        panelview.Visible = False
        PanelNewRequest.Visible = True
        btnWithdraw.Visible = False
        btnAdd.Visible = True
        txtcode.Text = ""
        txtfrmdate.Text = ""
        txttodate.Text = ""
        txtnodays.Text = ""
        txtreason.Text = ""
        txtrejoin.Text = ""
        txtlvecode.Text = ""
        txtlveName.Text = ""
        txtlveBal.Text = ""
        txtotalbal.Text = ""
        txtcutoff.Text = ""
        btnAdd.Visible = True
    End Sub
    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim blValue As Boolean
        If Session("UserCode") Is Nothing Or Session("SAPCompany") Is Nothing Then
            Response.Redirect("Login.aspx?sessionExpired=true", True)
        Else
            objEN.SapCompany = Session("SAPCompany")
            objEN.Empid = Session("UserCode").ToString()
            objEN.EmpName = Session("UserName").ToString()
            objEN.LeaveCode = txtlvecode.Text.Trim()
            objEN.StartDate = txtfrmdate.Text.Trim().Replace("-", "/")
            objEN.EndDate = txttodate.Text.Trim().Replace("-", "/")
            objEN.NoofDays = txtnodays.Text.Trim()
            objEN.Notes = txtreason.Text.Trim()
            objEN.RejoinDate = txtrejoin.Text.Trim().Replace("-", "/")
            objEN.strCode = txtcode.Text.Trim()
            objEN.LeaveName = txtlveName.Text.Trim()
            If txtotalbal.Text.Trim() <> "" Then
                objEN.TotalLeave = CDbl(txtotalbal.Text.Trim())
            End If
            objEN.LeaveBalance = CInt(txtlveBal.Text.Trim())
            If objEN.StartDate <> "" Then
                objEN.FromDate = Date.ParseExact(objEN.StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) ' Date.Parse(objEN.StartDate)
            End If
            If objEN.EndDate <> "" Then
                objEN.ToDate = Date.ParseExact(objEN.EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)
            End If
            If objEN.RejoinDate <> "" Then
                objEN.RejoinDt = Date.ParseExact(objEN.RejoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) ' 
            End If
            If objEN.LeaveCode = "" Then
                dbCon.strmsg = "alert('Leave type is missing...')"
                mess(dbCon.strmsg)
            ElseIf objEN.StartDate = "" Then
                dbCon.strmsg = "alert('From date is missing...')"
                mess(dbCon.strmsg)
            ElseIf objEN.EndDate = "" Then
                dbCon.strmsg = "alert('End date is missing...')"
                mess(dbCon.strmsg)
                'ElseIf objEN.Notes = "" Then
                '    dbCon.strmsg = "alert('Leave reason missing...')"
                '    mess(dbCon.strmsg)
            ElseIf objEN.NoofDays <= 0 Then
                dbCon.strmsg = "alert('End date must greater than or equal to start date...')"
                mess(dbCon.strmsg)
            ElseIf objEN.ToDate > objEN.RejoinDt Then ' objEN.EndDate > objEN.RejoinDate Then
                dbCon.strmsg = "alert('Rejoin date must greater than or equal to End date...')"
                mess(dbCon.strmsg)
            Else
                Dim strmsg As String = objVal.validateLeaveEntries(objEN.Empid, objEN.LeaveCode, objEN.FromDate, objEN.ToDate, objEN.SapCompany)
                Dim strLeave As String = objVal.validateLeave(objEN.LeaveCode, objEN.LeaveBalance, objEN.NoofDays, objEN.SapCompany)
                If strmsg <> "Success" Then
                    dbCon.strmsg = "alert('" & strmsg & "')"
                    mess(dbCon.strmsg)
                ElseIf strLeave <> "Success" Then
                    dbCon.strmsg = "alert('" & strLeave & "')"
                    mess(dbCon.strmsg)
                Else
                    objEN.Year = objEN.FromDate.Year
                    objEN.Month = objEN.FromDate.Month
                    objEN.Status = dbCon.DocApproval("LveReq", objEN.Empid, objEN.LeaveCode)
                    blValue = False
                    If txtcode.Text = "" Then
                        objEN.strCode = objBL.SaveLeaveRequest(objEN)
                        If objEN.strCode = "" Then
                            blValue = False
                        Else
                            strCode = objEN.strCode
                            blValue = True
                        End If
                    Else
                        blValue = objBL.UpdateLeaveRequest(objEN)
                    End If
                    If blValue = True Then
                        If objEN.Status = "A" Then
                            objVal.AddUDTPayroll(strCode, objEN.SapCompany)
                        End If
                        intTempID = dbCon.GetTemplateID("LveReq", objEN.Empid, objEN.LeaveCode)
                        If intTempID <> "0" Then
                            dbCon.UpdateApprovalRequired("@Z_PAY_OLETRANS1", "Code", objEN.strCode, "Y", intTempID)
                            dbCon.InitialMessage("Leave Request", objEN.strCode, dbCon.DocApproval("LveReq", objEN.Empid, objEN.LeaveCode), intTempID, objEN.EmpName, "LveReq", objEN.SapCompany)
                        Else
                            dbCon.UpdateApprovalRequired("@Z_PAY_OLETRANS1", "Code", objEN.strCode, "N", intTempID)
                        End If
                        dbCon.strmsg = "alert('Leave Request saved Successfully...')"
                        mess(dbCon.strmsg)
                    Else
                        dbCon.strmsg = "alert('Leave Request failed...')"
                        mess(dbCon.strmsg)
                    End If
                    panelview.Visible = True
                    PanelNewRequest.Visible = False
                    objEN.Empid = Session("UserCode").ToString()
                    PageLoadBind(objEN)
                End If
            End If
        End If
    End Sub
    Private Sub mess(ByVal str As String)
        ScriptManager.RegisterStartupScript(Update, Update.[GetType](), "strmsg", dbCon.strmsg, True)
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
            objEN.Empid = Session("UserCode").ToString()
            objEN.LeaveCode = DocNo.Text.Trim()
            populateLeaveRequest(objEN)
        End If
    End Sub
    Private Sub populateLeaveRequest(ByVal objen As LeaveRequestEN)
        Try
            dbCon.ds1 = objBL.PopulateLeaveRequest(objen)
            If dbCon.ds1.Tables(0).Rows.Count > 0 Then
                txtcode.Text = dbCon.ds1.Tables(0).Rows(0)("Code").ToString()
                txtfrmdate.Text = dbCon.ds1.Tables(0).Rows(0)("U_Z_StartDate").ToString()
                txttodate.Text = dbCon.ds1.Tables(0).Rows(0)("U_Z_EndDate").ToString()
                txtnodays.Text = dbCon.ds1.Tables(0).Rows(0)("U_Z_NoofDays").ToString()
                txtreason.Text = dbCon.ds1.Tables(0).Rows(0)("U_Z_Notes").ToString()
                txtrejoin.Text = dbCon.ds1.Tables(0).Rows(0)("U_Z_ReJoiNDate").ToString()
                txtlvecode.Text = dbCon.ds1.Tables(0).Rows(0)("U_Z_TrnsCode").ToString()
                txtlveName.Text = dbCon.ds1.Tables(0).Rows(0)("Name").ToString()
                txtlveBal.Text = dbCon.ds1.Tables(0).Rows(0)("U_Z_LevBal").ToString()
                blnValue = dbCon.WithDrawStatus("LveReq", txtcode.Text.Trim())
                If blnValue = True Or dbCon.ds1.Tables(0).Rows(0)("U_Z_Status").ToString() <> "P" Then
                    btnWithdraw.Visible = False
                    btnAdd.Visible = False
                Else
                    btnWithdraw.Visible = True
                    btnAdd.Visible = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txttodate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txttodate.TextChanged

        If Session("UserCode") Is Nothing Or Session("SAPCompany") Is Nothing Then
            dbCon.strmsg = "alert('Your session is Expired...')"
            mess(dbCon.strmsg)
            Response.Redirect("Login.aspx?sessionExpired=true", True)
        Else
            objEN.Empid = Session("UserCode").ToString()
            objEN.SapCompany = Session("SAPCompany")
            objEN.CutOff = txtcutoff.Text.Trim()
            objEN.StartDate = txtfrmdate.Text.Trim().Replace("-", "/")
            objEN.EndDate = txttodate.Text.Trim().Replace("-", "/")
            If objEN.StartDate <> "" And objEN.EndDate <> "" Then
                objEN.FromDate = Date.ParseExact(objEN.StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) ' Date.Parse(objEN.StartDate)
                objEN.ToDate = Date.ParseExact(objEN.EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                txtnodays.Text = objBL.getNodays(objEN)
                If objEN.FromDate = objEN.ToDate Then
                    txtnodays.Enabled = True
                Else
                    txtnodays.Enabled = False
                End If
                intDiff = CDbl(txtnodays.Text.Trim())
                Dim dblHolidaysCount As Double = objBL.getHolidayCount(objEN)
                txtotalbal.Text = dblHolidaysCount
                objEN.FromDate = Date.ParseExact(objEN.StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) ' Date.Parse(objEN.StartDate)
                objEN.ToDate = Date.ParseExact(objEN.EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                Dim dblHolidays As Double = objBL.getHolidayCount(objEN)
                intDiff = intDiff - dblHolidays
                txtnodays.Text = intDiff

                txtrejoin.Text = objEN.ToDate.AddDays(1).ToString("dd/MM/yyyy")
            End If
        End If
    End Sub

    Private Sub txtfrmdate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtfrmdate.TextChanged

        If Session("UserCode") Is Nothing Or Session("SAPCompany") Is Nothing Then
            dbCon.strmsg = "alert('Your session is Expired...')"
            mess(dbCon.strmsg)
            Response.Redirect("Login.aspx?sessionExpired=true", True)
        Else
            objEN.StartDate = txtfrmdate.Text.Trim().Replace("-", "/")
            objEN.EndDate = txttodate.Text.Trim().Replace("-", "/")
            objEN.Empid = Session("UserCode").ToString()
            objEN.SapCompany = Session("SAPCompany")
            objEN.CutOff = txtcutoff.Text.Trim()
            If objEN.StartDate <> "" And objEN.EndDate <> "" Then
                objEN.FromDate = Date.ParseExact(objEN.StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) ' Date.Parse(objEN.StartDate)
                objEN.ToDate = Date.ParseExact(objEN.EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                txtnodays.Text = objBL.getNodays(objEN)

                objEN.Empid = Session("UserCode").ToString()
                objEN.LeaveCode = txtlvecode.Text.Trim()
                If objEN.StartDate = "" Then
                    objEN.Year = 1920
                Else
                    objEN.Year = objEN.FromDate.Year
                End If
                FillLveType(objEN)
                objEN.Status = objBL.GetLeaveBalance(objEN)
                If objEN.Status = "" And objEN.Status = Nothing Then
                    txtlveBal.Text = 0.0
                Else
                    txtlveBal.Text = objEN.Status
                End If
                If objEN.FromDate = objEN.ToDate Then
                    txtnodays.Enabled = True
                Else
                    txtnodays.Enabled = False
                End If

                intDiff = CDbl(txtnodays.Text.Trim())
                Dim dblHolidaysCount As Double = objBL.getHolidaysinLeaveDays(objEN)
                txtotalbal.Text = dblHolidaysCount
                objEN.FromDate = Date.ParseExact(objEN.StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) ' Date.Parse(objEN.StartDate)
                objEN.ToDate = Date.ParseExact(objEN.EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                Dim dblHolidays As Double = objBL.getHolidayCount(objEN)
                intDiff = intDiff - dblHolidays
                txtnodays.Text = intDiff

            ElseIf objEN.StartDate <> "" Then
                objEN.FromDate = Date.ParseExact(objEN.StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) ' Date.Parse(objEN.StartDate)
                objEN.Empid = Session("UserCode").ToString()
                objEN.LeaveCode = txtlvecode.Text.Trim()
                If objEN.StartDate = "" Then
                    objEN.Year = 1920
                Else
                    objEN.Year = objEN.FromDate.Year
                End If
                FillLveType(objEN)
                objEN.Status = objBL.GetLeaveBalance(objEN)
                If objEN.Status = "" And objEN.Status = Nothing Then
                    txtlveBal.Text = 0.0
                Else
                    txtlveBal.Text = objEN.Status
                End If
            End If
        End If
    End Sub

    Private Sub btnWithdraw_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnWithdraw.Click
        Dim blValue As Boolean
        objEN.Empid = Session("UserCode").ToString()
        objEN.strCode = txtcode.Text.Trim()
        blValue = objBL.WithdrawRequest(objEN)
        If blValue = True Then
            dbCon.strmsg = "alert('Withdraw Leave Request Successfully...')"
            mess(dbCon.strmsg)
        Else
            dbCon.strmsg = "alert('Withdraw Leave Request failed...')"
            mess(dbCon.strmsg)
        End If
        PageLoadBind(objEN)
        panelview.Visible = True
        PanelNewRequest.Visible = False
    End Sub

    Private Sub btncancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncancel.Click
        panelview.Visible = True
        PanelNewRequest.Visible = False
        txtcode.Text = ""
        txtfrmdate.Text = ""
        txttodate.Text = ""
        txtnodays.Text = ""
        txtreason.Text = ""
        txtrejoin.Text = ""
        txtlvecode.Text = ""
        txtlveName.Text = ""
        txtotalbal.Text = ""
        txtcutoff.Text = ""
    End Sub

    Private Sub grdLeaveRequest_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdLeaveRequest.PageIndexChanging
        objEN.Empid = ViewState("EmpId")
        grdLeaveRequest.PageIndex = e.NewPageIndex
        PageLoadBind(objEN)
    End Sub

    Private Sub btnpopemp1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnpopemp1.Click
        objEN.LeaveCode = txtpoptraincode.Text.Trim()
        objEN.TransType = txtpopcouname.Text.Trim()
        dbCon.dss = objBL.PopupSearchBind(objEN)
        If dbCon.dss.Tables(0).Rows.Count > 0 Then
            grdLeaveType.DataSource = dbCon.dss.Tables(0)
            grdLeaveType.DataBind()
        Else
            grdLeaveType.DataBind()
        End If
        ModalPopupExtender7.Show()
    End Sub

    Private Sub LnkViewall_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LnkViewall.Click
        objEN.LeaveCode = ""
        objEN.TransType = ""
        dbCon.dss = objBL.PopupSearchBind(objEN)
        If dbCon.dss.Tables(0).Rows.Count > 0 Then
            grdLeaveType.DataSource = dbCon.dss.Tables(0)
            grdLeaveType.DataBind()
        Else
            grdLeaveType.DataBind()
        End If
        ModalPopupExtender7.Show()
    End Sub

    Private Sub grdSummary_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdSummary.PageIndexChanging
        objEN.Empid = ViewState("EmpId")
        grdLeaveRequest.PageIndex = e.NewPageIndex
        PageLoadBind(objEN)
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
                LoadActivity(DocNo.Text.Trim(), "LveReq")
                ModalPopupExtender1.Show()
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
    Private Sub grdLeaveRequest_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdLeaveRequest.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim LiDocNo As LinkButton = CType(e.Row.FindControl("lbtndocnum"), LinkButton)
            Dim Liview As LinkButton = CType(e.Row.FindControl("lbtAppHistory"), LinkButton)
            blnValue = dbCon.WithDrawStatus("LveReq", LiDocNo.Text.Trim())
            If blnValue = True Then
                Liview.Visible = True
            Else
                Liview.Visible = False
            End If
        End If
    End Sub
End Class