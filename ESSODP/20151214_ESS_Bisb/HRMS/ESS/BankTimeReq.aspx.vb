Imports System
Imports System.Globalization
Imports BusinessLogic
Imports DataAccess
Imports EN
Public Class BankTimeReq
    Inherits System.Web.UI.Page
    Dim objVal As DynamicApprovalDA = New DynamicApprovalDA()
    Dim objEN As BankTimeRequestEN = New BankTimeRequestEN()
    Dim objBL As BankTimeRequestBL = New BankTimeRequestBL()
    Dim dbCon As DBConnectionDA = New DBConnectionDA()
    Dim intTempID, strCode As String
    Dim intDiff As Double
    Dim blnValue As Boolean
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
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
            objEN.Empid = Session("UserCode").ToString()
            FillLveType(objEN)
            PageLoadBind(objEN)
            panelview.Visible = True
            PanelNewRequest.Visible = False
        End If
    End Sub
    Private Sub FillLveType(ByVal objen As BankTimeRequestEN)
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
    Private Sub PageLoadBind(ByVal objen As BankTimeRequestEN)
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
        txtlvecode.Text = ""
        txtlveName.Text = ""
        btnAdd.Visible = True
        ddlcashout.SelectedValue = "N"
    End Sub
    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim blValue As Boolean
        If Session("UserCode") Is Nothing Or Session("SAPCompany") Is Nothing Then
            Response.Redirect("../Login.aspx?sessionExpired=true", True)
        Else
            objEN.SapCompany = Session("SAPCompany")
            objEN.Empid = Session("UserCode").ToString()
            objEN.EmpName = Session("UserName").ToString()
            objEN.LeaveCode = txtlvecode.Text.Trim()
            objEN.StartDate = txtfrmdate.Text.Trim().Replace("-", "/")
            If txttodate.Text <> "" Then
                objEN.NoofHours = CDbl(txttodate.Text.Trim())
            Else
                objEN.NoofHours = 0
            End If
            objEN.NoofDays = objEN.NoofHours / 8
            objEN.Notes = txtreason.Text.Trim()
            objEN.StrCode = txtcode.Text.Trim()
            objEN.LeaveName = txtlveName.Text.Trim()
            objEN.CashOut = ddlcashout.SelectedValue
            If objEN.StartDate <> "" Then
                objEN.FromDate = Date.ParseExact(objEN.StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) ' Date.Parse(objEN.StartDate)
            End If
            If objEN.LeaveCode = "" Then
                dbCon.strmsg = "alert('Leave type is missing...')"
                mess(dbCon.strmsg)
            ElseIf objEN.StartDate = "" Then
                dbCon.strmsg = "alert('From date is missing...')"
                mess(dbCon.strmsg)
            ElseIf objEN.NoofHours = 0 Then
                dbCon.strmsg = "alert('No.of Hours is missing...')"
                mess(dbCon.strmsg)
            ElseIf objEN.NoofHours > 24 Then
                dbCon.strmsg = "alert('No.of Hours less than or equal to 24 hours...')"
                mess(dbCon.strmsg)
            Else
                objEN.AppStatus = dbCon.DocApproval("LveReq", objEN.Empid, objEN.LeaveCode)
                strCode = ""
                If txtcode.Text = "" Then
                    objEN.StrCode = objBL.SaveBankTimeRequest(objEN)
                    If objEN.StrCode = "" Then
                        blValue = False
                    Else
                        strCode = objEN.StrCode
                        blValue = True
                    End If
                Else
                    blValue = objBL.UpdateBankTimeRequest(objEN)
                End If
                If blValue = True Then
                    If objEN.AppStatus = "A" Then
                        dbCon.strmsg = objBL.AddtoUDT_BankTime(objEN)
                    End If
                    intTempID = dbCon.GetTemplateID("LveReq", objEN.Empid, objEN.LeaveCode)
                    If intTempID <> "0" Then
                        dbCon.UpdateApprovalRequired("@Z_PAY_OLADJTRANS1", "Code", objEN.StrCode, "Y", intTempID)
                        dbCon.InitialMessage("Bank Time Request", objEN.StrCode, dbCon.DocApproval("LveReq", objEN.Empid, objEN.LeaveCode), intTempID, objEN.EmpName, "BankTime", objEN.SapCompany)
                    Else
                        dbCon.UpdateApprovalRequired("@Z_PAY_OLADJTRANS1", "Code", objEN.StrCode, "N", intTempID)
                    End If
                    dbCon.strmsg = "alert('Bank Time Request saved Successfully...')"
                    mess(dbCon.strmsg)
                Else
                    dbCon.strmsg = "alert('Bank Time Request failed...')"
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

        If Session("UserCode") Is Nothing Or Session("SAPCompany") Is Nothing Then
            dbCon.strmsg = "alert('Your session is Expired...')"
            mess(dbCon.strmsg)
            Response.Redirect("../Login.aspx?sessionExpired=true", True)
        Else
            Dim link As LinkButton = CType(sender, LinkButton)
            Dim gv As GridViewRow = CType((link.Parent.Parent), GridViewRow)
            Dim DocNo As LinkButton = CType(gv.FindControl("lbtndocnum"), LinkButton)
            panelview.Visible = False
            PanelNewRequest.Visible = True
            objEN.Empid = Session("UserCode").ToString()
            objEN.StrCode = DocNo.Text.Trim()
            populateLeaveRequest(objEN)
        End If
    End Sub
    Private Sub populateLeaveRequest(ByVal objen As BankTimeRequestEN)
        Try
            dbCon.ds1 = objBL.PopulateBankTimeRequest(objen)
            If dbCon.ds1.Tables(0).Rows.Count > 0 Then
                txtcode.Text = dbCon.ds1.Tables(0).Rows(0)("Code").ToString()
                txtfrmdate.Text = dbCon.ds1.Tables(0).Rows(0)("U_Z_StartDate").ToString()
                txttodate.Text = dbCon.ds1.Tables(0).Rows(0)("U_Z_NoofHours").ToString()
                txtnodays.Text = dbCon.ds1.Tables(0).Rows(0)("U_Z_NoofDays").ToString()
                txtreason.Text = dbCon.ds1.Tables(0).Rows(0)("U_Z_Notes").ToString()
                txtlvecode.Text = dbCon.ds1.Tables(0).Rows(0)("U_Z_TrnsCode").ToString()
                txtlveName.Text = dbCon.ds1.Tables(0).Rows(0)("U_Z_LeaveName").ToString()
                ddlcashout.SelectedValue = dbCon.ds1.Tables(0).Rows(0)("U_Z_CashOut").ToString()
                blnValue = dbCon.WithDrawStatus("BankTime", txtcode.Text.Trim())
                If blnValue = True Or dbCon.ds1.Tables(0).Rows(0)("U_Z_AppStatus").ToString() <> "P" Then
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
    Private Sub btnWithdraw_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnWithdraw.Click
        Dim blValue As Boolean
        objEN.Empid = Session("UserCode").ToString()
        objEN.strCode = txtcode.Text.Trim()
        blValue = objBL.WithdrawRequest(objEN)
        If blValue = True Then
            dbCon.strmsg = "alert('Withdraw Bank Time Request Successfully...')"
            mess(dbCon.strmsg)
        Else
            dbCon.strmsg = "alert('Withdraw Bank Time Request failed...')"
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
        txtlvecode.Text = ""
        txtlveName.Text = ""
    End Sub

    Private Sub grdLeaveRequest_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdLeaveRequest.PageIndexChanging
        objEN.Empid = ViewState("EmpId")
        grdLeaveRequest.PageIndex = e.NewPageIndex
        PageLoadBind(objEN)
    End Sub

    Private Sub btnpopemp1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnpopemp1.Click
        objEN.LeaveCode = txtpoptraincode.Text.Trim()
        objEN.LeaveName = txtpopcouname.Text.Trim()
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
        objEN.LeaveName = ""
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
                Response.Redirect("../Login.aspx?sessionExpired=true", True)
            Else
                Dim link As LinkButton = CType(sender, LinkButton)
                Dim gv As GridViewRow = CType((link.Parent.Parent), GridViewRow)
                Dim DocNo As LinkButton = CType(gv.FindControl("lbtndocnum"), LinkButton)
                LoadActivity(DocNo.Text.Trim(), "BankTime")
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
            blnValue = dbCon.WithDrawStatus("BankTime", LiDocNo.Text.Trim())
            If blnValue = True Then
                Liview.Visible = True
            Else
                Liview.Visible = False
            End If
        End If
    End Sub

End Class