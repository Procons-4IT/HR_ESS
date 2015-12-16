Imports System
Imports System.Drawing
Imports System.Globalization
Imports BusinessLogic
Imports DataAccess
Imports EN
Public Class LeaveRequestApproval
    Inherits System.Web.UI.Page
    Dim dbCon As DBConnectionDA = New DBConnectionDA()
    Dim objBL As DynamicApprovalBL = New DynamicApprovalBL()
    Dim objEN As DynamicApprovalEN = New DynamicApprovalEN()
    Dim blnValue As Boolean
    Dim info As DateTimeFormatInfo = DateTimeFormatInfo.GetInstance(Nothing)
    Dim Transdt As Date

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
            objEN.UserCode = objBL.GetUserCode(objEN)
            If objEN.UserCode = "" Then
                dbCon.strmsg = "alert('Employee not mapped in user...')"
                mess(dbCon.strmsg)
            Else
                objEN.HeaderType = "LveReq"
                objEN.HistoryType = "LveReq"
                ReqApproval(objEN)
                txtcode.Text = ""
                txtempid.Text = ""
                txtlveCode.Text = ""
                For j As Integer = 2010 To 2050
                    ddlpayYear.Items.Add(New ListItem(j.ToString(), j.ToString()))
                Next
                ddlpayYear.Items.Insert(0, "---Select---")
                For i As Integer = 1 To 12
                    ddlpaymonth.Items.Add(New ListItem(info.GetMonthName(i), i.ToString()))
                Next
                ddlpaymonth.Items.Insert(0, "---Select---")
                ddlpayYear.SelectedValue = DateTime.Now.Year.ToString()
                ddlpaymonth.SelectedValue = DateTime.Now.Month.ToString()
            End If
        End If
    End Sub
    Private Sub ReqApproval(ByVal objEN As DynamicApprovalEN)
        Try
            dbCon.ds = objBL.InitializationApproval(objEN)
            If dbCon.ds.Tables(0).Rows.Count > 0 Then
                grdRequestApproval.DataSource = dbCon.ds.Tables(0)
                grdRequestApproval.DataBind()
                btnAdd.Visible = True
            Else
                grdRequestApproval.DataBind()
                btnAdd.Visible = False
            End If

            dbCon.ds2 = objBL.ApprovalSummary(objEN)
            If dbCon.ds2.Tables(0).Rows.Count > 0 Then
                grdSummary.DataSource = dbCon.ds2.Tables(0)
                grdSummary.DataBind()
            Else
                grdSummary.DataBind()
            End If
        Catch ex As Exception
            dbCon.strmsg = "alert('" & ex.Message & "')"
            mess(dbCon.strmsg)
        End Try
    End Sub
    Private Sub mess(ByVal str As String)
        ScriptManager.RegisterStartupScript(Update, Update.[GetType](), "strmsg", str, True)
    End Sub

    Private Sub grdRequestApproval_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdRequestApproval.PageIndexChanging
        grdRequestApproval.PageIndex = e.NewPageIndex
        objEN.EmpId = Session("UserCode").ToString()
        objEN.UserCode = objBL.GetUserCode(objEN)
        objEN.HeaderType = "LveReq"
        objEN.HistoryType = "LveReq"
        ReqApproval(objEN)
    End Sub

    Private Sub grdRequestApproval_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdRequestApproval.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("Style") = "cursor:pointer"
            e.Row.ToolTip = "Click first column for selecting this row."
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
            Dim DocNo As LinkButton = CType(gv.FindControl("lblCode"), LinkButton)
            Dim Empid As Label = CType(gv.FindControl("lblEmpid"), Label)
            Dim lveCode As Label = CType(gv.FindControl("lblAgenda"), Label)
            Dim TransDate As Label = CType(gv.FindControl("lblCouCode"), Label)
            Dim introw As Integer = gv.RowIndex
            For Each row1 As GridViewRow In grdRequestApproval.Rows
                If row1.RowIndex <> introw Then
                    row1.BackColor = Color.White
                Else
                    row1.BackColor = Color.Orange
                End If
            Next
            If TransDate.Text.Trim() <> "" Then
                Transdt = Date.ParseExact(TransDate.Text.Trim().Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture)
            End If
            ddlpayYear.SelectedValue = Transdt.Year.ToString()
            ddlpaymonth.SelectedValue = Transdt.Month.ToString()

            txtcode.Text = DocNo.Text.Trim()
            txtempid.Text = Empid.Text.Trim()
            txtlveCode.Text = lveCode.Text.Trim()
            objEN.EmpId = Session("UserCode").ToString()
            objEN.DocEntry = DocNo.Text.Trim()
            objEN.HistoryType = "LveReq"
            BindHistory(objEN)
        End If
    End Sub
    Private Sub BindHistory(ByVal objEN As DynamicApprovalEN)
        Try
            dbCon.ds1 = objBL.LoadHistory(objEN)
            If dbCon.ds1.Tables(0).Rows.Count > 0 Then
                grdApprovalHis.DataSource = dbCon.ds1.Tables(0)
                grdApprovalHis.DataBind()
            Else
                grdApprovalHis.DataBind()
            End If
        Catch ex As Exception
            dbCon.strmsg = "alert('" & ex.Message & "')"
            mess(dbCon.strmsg)
        End Try
    End Sub
    Protected Sub lbtndocnumSum_Click(ByVal sender As Object, ByVal e As EventArgs)

        Try
            If Session("UserCode") Is Nothing Or Session("SAPCompany") Is Nothing Then
                dbCon.strmsg = "alert('Your session is Expired...')"
                mess(dbCon.strmsg)
                Response.Redirect("Login.aspx?sessionExpired=true", True)
            Else
                Dim link As LinkButton = CType(sender, LinkButton)
                Dim gv As GridViewRow = CType((link.Parent.Parent), GridViewRow)
                Dim DocNo As LinkButton = CType(gv.FindControl("lblSCode"), LinkButton)
                Dim introw As Integer = gv.RowIndex
                For Each row1 As GridViewRow In grdSummary.Rows
                    If row1.RowIndex <> introw Then
                        row1.BackColor = Color.White
                    Else
                        row1.BackColor = Color.Orange
                    End If
                Next
                objEN.EmpId = Session("UserCode").ToString()
                objEN.DocEntry = DocNo.Text.Trim()
                objEN.HistoryType = "LveReq"
                SummaryHistory(objEN)
            End If
        Catch ex As Exception
            dbCon.strmsg = "alert('" & ex.Message & "')"
            mess(dbCon.strmsg)
        End Try
    End Sub
    Private Sub SummaryHistory(ByVal objEN As DynamicApprovalEN)
        Try
            dbCon.ds3 = objBL.SummaryHistory(objEN)
            If dbCon.ds3.Tables(0).Rows.Count > 0 Then
                grdHistorySummary.DataSource = dbCon.ds3.Tables(0)
                grdHistorySummary.DataBind()
            Else
                grdHistorySummary.DataBind()
            End If
        Catch ex As Exception
            dbCon.strmsg = "alert('" & ex.Message & "')"
            mess(dbCon.strmsg)
        End Try
    End Sub

    Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click

        Try
            If Session("UserCode") Is Nothing Or Session("SAPCompany") Is Nothing Then
                dbCon.strmsg = "alert('Your session is Expired...')"
                mess(dbCon.strmsg)
                Response.Redirect("Login.aspx?sessionExpired=true", True)
            ElseIf txtcode.Text <> "" Or txtcode.Text <> Nothing Then
                objEN.EmpId = Session("UserCode").ToString()
                objEN.UserCode = objBL.GetUserCode(objEN)
                objEN.EmpUserId = objBL.GetEmpUserid(objEN)
                objEN.DocEntry = txtcode.Text.Trim()
                objEN.EmpId = txtempid.Text.Trim()
                objEN.HistoryType = "LveReq"
                objEN.HeaderType = "LveReq"
                objEN.AppStatus = ddlAppStatus.SelectedValue
                objEN.Remarks = txtcomments.Text.Trim()
                objEN.Month = ddlpaymonth.SelectedValue
                objEN.Year = ddlpayYear.SelectedValue
                objEN.LeaveCode = txtlveCode.Text.Trim()
                dbCon.strmsg = objBL.ApprovalValidation(objEN)
                objEN.DocMessage = "Leave Request"
                objEN.SapCompany = Session("SAPCompany")
                If dbCon.strmsg = "Success" Then
                    dbCon.strmsg = objBL.addUpdateDocument(objEN)
                    If dbCon.strmsg = "Success" Then
                        dbCon.strmsg = "alert('Document Approved Successfully....')"
                        mess(dbCon.strmsg)
                        objEN.DocEntry = txtcode.Text.Trim()
                        objEN.EmpId = txtempid.Text.Trim()
                        objEN.HistoryType = "LveReq"
                        objEN.HeaderType = "LveReq"
                        ReqApproval(objEN)
                        grdApprovalHis.DataBind()
                        txtcode.Text = ""
                        txtempid.Text = ""
                        txtlveCode.Text = ""
                        ddlAppStatus.SelectedIndex = 0
                        txtcomments.Text = ""
                    Else
                        dbCon.strmsg = "alert('" & dbCon.strmsg & "')"
                        mess(dbCon.strmsg)
                        objEN.DocEntry = txtcode.Text.Trim()
                        objEN.EmpId = txtempid.Text.Trim()
                        objEN.HistoryType = "LveReq"
                        objEN.HeaderType = "LveReq"
                        ReqApproval(objEN)
                        grdApprovalHis.DataBind()
                        txtcode.Text = ""
                        txtempid.Text = ""
                        txtlveCode.Text = ""
                        ddlAppStatus.SelectedIndex = 0
                        txtcomments.Text = ""
                    End If
                Else
                    dbCon.strmsg = "alert('" & dbCon.strmsg & "')"
                    mess(dbCon.strmsg)
                End If
            Else
                dbCon.strmsg = "alert('Select the request no...')"
                mess(dbCon.strmsg)
            End If
        Catch ex As Exception
            dbCon.strmsg = "alert('" & ex.Message & "')"
            mess(dbCon.strmsg)
        End Try
    End Sub

    Private Sub grdSummary_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdSummary.PageIndexChanging
        grdSummary.PageIndex = e.NewPageIndex
        objEN.EmpId = Session("UserCode").ToString()
        objEN.UserCode = objBL.GetUserCode(objEN)
        objEN.HeaderType = "LveReq"
        objEN.HistoryType = "LveReq"
        ReqApproval(objEN)
    End Sub
    Protected Sub lbtnactivity_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If Session("UserCode") Is Nothing Or Session("SAPCompany") Is Nothing Then
                dbCon.strmsg = "alert('Your session is Expired...')"
                mess(dbCon.strmsg)
                Response.Redirect("Login.aspx?sessionExpired=true", True)
            Else
                Dim link As LinkButton = CType(sender, LinkButton)
                Dim gv As GridViewRow = CType((link.Parent.Parent), GridViewRow)
                Dim DocNo As Label = CType(gv.FindControl("lblEmpid"), Label)
                Dim Year As Label = CType(gv.FindControl("lblpayyear"), Label)
                objEN.EmpId = DocNo.Text.Trim()
                objEN.Year = Year.Text.Trim()
                GetBalance(objEN)
                ModalPopupExtender7.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetBalance(ByVal objen As DynamicApprovalEN)
        Try
            dbCon.dss4 = objBL.GetLeaveBalance(objen)
            If dbCon.dss4.Tables(0).Rows.Count > 0 Then
                grdRequesttohr.DataSource = dbCon.dss4.Tables(0)
                grdRequesttohr.DataBind()
            Else
                grdRequesttohr.DataBind()
            End If
        Catch ex As Exception
            dbCon.strmsg = "alert('" & ex.Message & "')"
            mess(dbCon.strmsg)
        End Try
    End Sub

    Protected Sub lbtnHistory_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If Session("UserCode") Is Nothing Or Session("SAPCompany") Is Nothing Then
                dbCon.strmsg = "alert('Your session is Expired...')"
                mess(dbCon.strmsg)
                Response.Redirect("Login.aspx?sessionExpired=true", True)
            Else
                Dim link As LinkButton = CType(sender, LinkButton)
                Dim gv As GridViewRow = CType((link.Parent.Parent), GridViewRow)
                Dim DocNo As Label = CType(gv.FindControl("lblEmpid"), Label)
                objEN.EmpId = DocNo.Text.Trim()
                GetHistory(objEN)
                ModalPopupExtender1.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetHistory(ByVal objen As DynamicApprovalEN)
        Try
            dbCon.ds4 = objBL.GetLeaveHistory(objen)
            If dbCon.ds4.Tables(0).Rows.Count > 0 Then
                GridView1.DataSource = dbCon.ds4.Tables(0)
                GridView1.DataBind()
            Else
                GridView1.DataBind()
            End If
        Catch ex As Exception
            dbCon.strmsg = "alert('" & ex.Message & "')"
            mess(dbCon.strmsg)
        End Try
    End Sub
End Class