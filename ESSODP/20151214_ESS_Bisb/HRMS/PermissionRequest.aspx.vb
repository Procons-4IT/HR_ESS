Imports System
Imports System.Globalization
Imports BusinessLogic
Imports DataAccess
Imports EN
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Odbc
Public Class PermissionRequest
    Inherits System.Web.UI.Page
    Dim objEN As LeaveRequestEN = New LeaveRequestEN()
    Dim objBL As PermissionbyHoursBL = New PermissionbyHoursBL()
    Dim objDA As PermissionbyHoursDA = New PermissionbyHoursDA()
    Dim dbCon As DBConnectionDA = New DBConnectionDA()
    Dim objCom As CommonFunctions = New CommonFunctions()
    Dim intTempID As String
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
            objCom.Dropdown1("SELECT ""Code"", ""Name"" from ""@Z_PAY_LEAVE""", "Code", "Name", ddllvecode)
            objEN.Empid = Session("UserCode").ToString()
            ViewState("EmpId") = objEN.Empid
            PageLoadBind(objEN)
            panelview.Visible = True
            PanelNewRequest.Visible = False
        End If
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
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnnew_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnnew.Click
        panelview.Visible = False
        PanelNewRequest.Visible = True
        btnWithdraw.Visible = False
        btnAdd.Visible = True
        txtcode.Text = ""
        '  ddllvecode.SelectedIndex = 0
        txtfrmdate.Text = ""
        ' txttodate.Text = ""
        txtreason.Text = ""
        txtfromtime.Text = ""
        txtToTime.Text = ""
    End Sub

    Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click

        Dim blValue As Boolean = False
        If Session("UserCode") Is Nothing Then
            Response.Redirect("Login.aspx?sessionExpired=true", True)
        Else
            objEN.SapCompany = Session("SAPCompany")
            objEN.Empid = Session("UserCode").ToString()
            objEN.EmpName = Session("UserName").ToString()
            objEN.LeaveCode = ddllvecode.SelectedValue
            objEN.StartDate = txtfrmdate.Text.Trim().Replace("-", "/")
            ' objEN.EndDate = txttodate.Text.Trim()
            objEN.Notes = txtreason.Text.Trim()
            objEN.strCode = txtcode.Text.Trim()
            objEN.Fromtime = txtfromtime.Text.Trim()
            objEN.Totime = txtToTime.Text.Trim()
            Dim dtdate, dtdate1 As DateTime
            If objEN.Fromtime.Length > 4 Then
                If objEN.Fromtime <> "" Then
                    dtdate = New DateTime(2001, 1, 1, CInt(objEN.Fromtime.Substring(0, 2)), CInt(objEN.Fromtime.Substring(3, 2)), 0)
                End If
            End If
            If objEN.Totime.Length > 4 Then
                If objEN.Totime <> "" Then
                    dtdate1 = New DateTime(2001, 1, 1, CInt(objEN.Totime.Substring(0, 2)), CInt(objEN.Totime.Substring(3, 2)), 0)
                End If
            End If
            If objEN.StartDate = "" Then
                dbCon.strmsg = "alert('Request Date is missing...')"
                mess(dbCon.strmsg)
                'ElseIf objEN.EndDate = "" Then
                '    dbCon.strmsg = "alert('End date is missing...')"
                '    mess(dbCon.strmsg)
            ElseIf objEN.Fromtime = "" Then
                dbCon.strmsg = "alert('From time is missing...')"
                mess(dbCon.strmsg)
            ElseIf objEN.Totime = "" Then
                dbCon.strmsg = "alert('To Time is missing...')"
                mess(dbCon.strmsg)
            ElseIf dtdate > dtdate1 Then
                dbCon.strmsg = "alert('To Time should be greater than From Time...')"
                mess(dbCon.strmsg)
            ElseIf objEN.Notes = "" Then
                dbCon.strmsg = "alert('Reason missing...')"
                mess(dbCon.strmsg)
                'ElseIf objEN.StartDate > objEN.EndDate Then
                '    dbCon.strmsg = "alert('End date must greater than or equal to start date...')"
                '    mess(dbCon.strmsg)
            Else
                objEN.FromDate = Date.ParseExact(objEN.StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                ' objEN.ToDate = objEN.EndDate

                'objEN.FromDatetime = objEN.StartDate & " " & objEN.Fromtime
                'objEN.ToDatetime = objEN.EndDate & " " & objEN.Totime
                'objEN.Year = objDA.getNodays(objEN)
                objEN.Status = dbCon.DocApproval("LveReq", objEN.Empid)
                blValue = False
                If txtcode.Text = "" Then
                    objEN.strCode = objBL.SaveLeaveRequest(objEN)
                    blValue = True
                Else
                    blValue = objBL.UpdateLeaveRequest(objEN)
                End If
                If blValue = True Then
                    intTempID = dbCon.GetTemplateID("LveReq", objEN.Empid)
                    If intTempID <> "0" Then
                        dbCon.UpdateApprovalRequired("@Z_PAY_OLETRANS1", "Code", objEN.strCode, "Y", intTempID)
                        dbCon.InitialMessage("Permission/Leave by hours", objEN.strCode, dbCon.DocApproval("LveReq", objEN.Empid), intTempID, objEN.EmpName, "LveReq", objEN.SapCompany)
                    Else
                        dbCon.UpdateApprovalRequired("@Z_PAY_OLETRANS1", "Code", objEN.strCode, "N", intTempID)
                    End If
                    dbCon.strmsg = "alert('Permission / Leave by hours saved Successfully...')"
                    mess(dbCon.strmsg)
                Else
                    dbCon.strmsg = "alert('Permission / Leave by hours failed...')"
                    mess(dbCon.strmsg)
                End If
                panelview.Visible = True
                PanelNewRequest.Visible = False
                objEN.Empid = Session("UserCode").ToString()
                PageLoadBind(objEN)
            End If
        End If
    End Sub
    Private Sub mess(ByVal str As String)
        ScriptManager.RegisterStartupScript(Update, Update.[GetType](), "strmsg", dbCon.strmsg, True)
    End Sub
    Protected Sub lbtndocnum_Click(ByVal sender As Object, ByVal e As EventArgs)

        If Session("UserCode") Is Nothing Then
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
        Dim strTime, strTime1 As Integer
        Dim Time1, Time2 As Date
        Try
            dbCon.ds1 = objBL.PopulatePermissionRequest(objen)
            If dbCon.ds1.Tables(0).Rows.Count > 0 Then
                txtcode.Text = dbCon.ds1.Tables(0).Rows(0)("Code").ToString()
                txtfrmdate.Text = dbCon.ds1.Tables(0).Rows(0)("U_Z_StartDate").ToString()
                ' txttodate.Text = dbCon.ds1.Tables(0).Rows(0)("U_Z_EndDate").ToString()
                txtreason.Text = dbCon.ds1.Tables(0).Rows(0)("U_Z_Notes").ToString()
                strTime = dbCon.ds1.Tables(0).Rows(0)("U_Z_FromTime").ToString()
                strTime1 = dbCon.ds1.Tables(0).Rows(0)("U_Z_ToTime").ToString()
                Time1 = TimeSerial([strTime] \ 100, [strTime] Mod 100, 0)
                Time2 = TimeSerial([strTime1] \ 100, [strTime1] Mod 100, 0)
                txtfromtime.Text = Time1.ToString("HH:mm")
                txtToTime.Text = Time2.ToString("HH:mm")
                ' ddllvecode.SelectedValue = dbCon.ds1.Tables(0).Rows(0)("U_Z_TrnsCode").ToString()
                Blflag = dbCon.WithDrawStatus("PerHour", txtcode.Text.Trim())
                If dbCon.ds1.Tables(0).Rows(0)("U_Z_Status").ToString() <> "P" Or Blflag = True Then
                    btnAdd.Visible = False
                    btnWithdraw.Visible = False
                Else
                    btnAdd.Visible = True
                    btnWithdraw.Visible = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
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
        '  ddllvecode.SelectedIndex = 0
        txtfrmdate.Text = ""
        '    txttodate.Text = ""
        txtfromtime.Text = ""
        txtreason.Text = ""
        txtToTime.Text = ""
    End Sub

    Private Sub grdLeaveRequest_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdLeaveRequest.PageIndexChanging
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
                LoadActivity(DocNo.Text.Trim(), "PerHour")
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
    Private Sub grdLeaveRequest_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdLeaveRequest.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim LiDocNo As LinkButton = CType(e.Row.FindControl("lbtndocnum"), LinkButton)
            Dim Liview As LinkButton = CType(e.Row.FindControl("lbtAppHistory"), LinkButton)
            Blflag = dbCon.WithDrawStatus("PerHour", LiDocNo.Text.Trim())
            If Blflag = True Then
                Liview.Visible = True
            Else
                Liview.Visible = False
            End If
        End If
    End Sub
End Class