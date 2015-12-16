Imports System
Imports System.Globalization
Imports BusinessLogic
Imports DataAccess
Imports EN
Public Class MLoanRequest
    Inherits System.Web.UI.Page
    Dim objVal As DynamicApprovalDA = New DynamicApprovalDA()
    Dim objEN As LoanRequestEN = New LoanRequestEN()
    Dim objBL As LoanRequestBL = New LoanRequestBL()
    Dim dbCon As DBConnectionDA = New DBConnectionDA()
    Dim objCom As CommonFunctions = New CommonFunctions()
    Dim BlnFlag, Blflag As Boolean
    Dim intTempID, strCode As String
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
            FillLoanType()
            PageLoadBind(objEN)
            panelview.Visible = True
            PanelNewRequest.Visible = False
        End If
    End Sub
    Private Sub FillLoanType()
        Try
            dbCon.ds = objBL.LoanType()
            If dbCon.ds.Tables(0).Rows.Count > 0 Then
                grdLoneType.DataSource = dbCon.ds.Tables(0)
                grdLoneType.DataBind()
            Else
                grdLoneType.DataBind()
            End If
        Catch ex As Exception
            Throw ex
        Finally
            dbCon.ds = Nothing
        End Try
    End Sub
    Private Sub PageLoadBind(ByVal objen As LoanRequestEN)
        Try
            dbCon.ds1 = objBL.PageLoadBind(objen)
            If dbCon.ds1.Tables(0).Rows.Count > 0 Then
                grdLoanRequest.DataSource = dbCon.ds1.Tables(0)
                grdLoanRequest.DataBind()
            Else
                grdLoanRequest.DataBind()
            End If
            If dbCon.ds1.Tables(1).Rows.Count > 0 Then
                grdSummary.DataSource = dbCon.ds1.Tables(1)
                grdSummary.DataBind()
            Else
                grdSummary.DataBind()
            End If
        Catch ex As Exception
            Throw ex
        Finally
            dbCon.ds1 = Nothing
        End Try
    End Sub

    Protected Sub Btncallpop_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btncallpop.ServerClick
        Dim str1, str2, str3 As String
        str1 = txtpopunique.Text.Trim()
        str2 = txtpoptno.Text.Trim()
        str3 = txttname.Text.Trim()
        If txthidoption.Text = "Loan" Then
            If txtpoptno.Text.Trim() <> "" Then
                txtlvecode.Text = txtpopunique.Text.Trim()
                txtloanName.Text = txtpoptno.Text.Trim()
            End If
        End If
    End Sub

    Private Sub grdLoneType_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdLoneType.RowDataBound
        txtpoptno.Text = ""
        txtpopunique.Text = ""
        txthidoption.Text = ""
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onclick", "popupdisplay('Loan','" + (DataBinder.Eval(e.Row.DataItem, "Code")).ToString().Trim() + "','" + (DataBinder.Eval(e.Row.DataItem, "Name")).ToString().Trim() + "');")
        End If
    End Sub

    Private Sub btnnew_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnnew.Click
        Try
            objEN.Empid = Session("UserCode").ToString()
            BlnFlag = objBL.ValidateAppTemp(objEN)
            If BlnFlag = False Then
                dbCon.strmsg = "alert('You are not eligible for loan request...')"
                mess(dbCon.strmsg)
            Else
                panelview.Visible = False
                PanelNewRequest.Visible = True
                btnWithdraw.Visible = False
                btnAdd.Visible = True
                txtcode.Text = ""
                txtlvecode.Text = ""
                txtloanAmt.Text = ""
                txtloanName.Text = ""
                txtReqdate.Text = Now.Date.ToString("dd/MM/yyyy")
                txttname.Text = ""
                txtLodisDate.Text = ""
                txtLoStdate.Text = ""
                txtnoEMI.Text = ""
                btnAdd.Visible = True
            End If
        Catch ex As Exception
            dbCon.strmsg = "alert('" & ex.Message & "')"
            mess(dbCon.strmsg)
        End Try
    End Sub
    Private Sub mess(ByVal str As String)
        ScriptManager.RegisterStartupScript(Update, Update.[GetType](), "strmsg", dbCon.strmsg, True)
    End Sub
    Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            If Session("UserCode") Is Nothing Or Session("SAPCompany") Is Nothing Then
                Response.Redirect("Login.aspx?sessionExpired=true", True)
            Else
                objEN.SapCompany = Session("SAPCompany")
                objEN.Empid = Session("UserCode").ToString()
                objEN.EmpName = Session("UserName").ToString()
                objEN.LoanType = txtlvecode.Text.Trim()
                objEN.LoanName = txtloanName.Text.Trim()
                objEN.NoEMI = txtnoEMI.Text.Trim()
                objEN.StrCode = txtcode.Text.Trim()
                If txtloanAmt.Text.Trim() <> "" Then
                    objEN.LoanAmount = txtloanAmt.Text.Trim()
                Else
                    objEN.LoanAmount = 0.0
                End If
                If txtReqdate.Text.Trim() <> "" Then
                    objEN.ReqDate = Date.ParseExact(txtReqdate.Text.Trim().Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture)
                Else
                    objEN.ReqDate = Now.Date
                End If
                If txtLodisDate.Text.Trim() <> "" Then
                    objEN.DisDate = Date.ParseExact(txtLodisDate.Text.Trim().Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture)
                Else
                    objEN.DisDate = Now.Date
                End If
                If txtLoStdate.Text.Trim() <> "" Then
                    objEN.StartDate = Date.ParseExact(txtLoStdate.Text.Trim().Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture)
                Else
                    objEN.StartDate = Now.Date
                End If
                If objEN.LoanAmount = 0.0 Then
                    dbCon.strmsg = "alert('Loan amount is missing...')"
                    mess(dbCon.strmsg)
                ElseIf txtloanName.Text.Trim() = "" Then
                    dbCon.strmsg = "alert('Loan type is missing...')"
                    mess(dbCon.strmsg)
                ElseIf txtReqdate.Text.Trim() = "" Then
                    dbCon.strmsg = "alert('Request date is missing...')"
                    mess(dbCon.strmsg)
                ElseIf objEN.StartDate < objEN.DisDate Then
                    dbCon.strmsg = "alert('Loan Installment Start Date should be greater than or equal to Loan distribution Date')"
                    mess(dbCon.strmsg)
                ElseIf objEN.StartDate < objEN.ReqDate Then
                    dbCon.strmsg = "alert('Loan Installment Request Date should be Less than to Loan Installment Start Date')"
                    mess(dbCon.strmsg)
                Else
                    objEN.Status = dbCon.DocApproval("LoanReq", objEN.Empid)
                    If txtcode.Text = "" Then
                        objEN.StrCode = objBL.SaveLoanRequest(objEN)
                        If objEN.StrCode = 1 Then
                            objEN.StrCode = objBL.GetDocEntry()
                            BlnFlag = True
                        Else
                            dbCon.strmsg = "alert('" & objEN.StrCode & "')"
                            mess(dbCon.strmsg)
                            Exit Sub
                        End If
                    Else
                        BlnFlag = objBL.UpdateLoanRequest(objEN)
                        objEN.StrCode = txtcode.Text.Trim()
                    End If
                    If BlnFlag = True Then
                        intTempID = dbCon.GetTemplateID("LoanReq", objEN.Empid)
                        If intTempID <> "0" Then
                            dbCon.UpdateApprovalRequired("U_LOANREQ", "U_DocEntry", objEN.StrCode, "Y", intTempID)
                            dbCon.InitialMessage("Loan Request", objEN.StrCode, dbCon.DocApproval("LoanReq", objEN.Empid), intTempID, objEN.EmpName, "LoanReq", objEN.SapCompany)
                        Else
                            dbCon.UpdateApprovalRequired("U_LOANREQ", "U_DocEntry", objEN.StrCode, "N", intTempID)
                        End If
                        dbCon.strmsg = "alert('Loan Request saved Successfully...')"
                        mess(dbCon.strmsg)
                    End If
                    panelview.Visible = True
                    PanelNewRequest.Visible = False
                    objEN.Empid = Session("UserCode").ToString()
                    PageLoadBind(objEN)
                End If
            End If
        Catch ex As Exception
            dbCon.strmsg = "alert('" & ex.Message & "')"
            mess(dbCon.strmsg)
        End Try
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
            objEN.StrCode = DocNo.Text.Trim()
            populateLoanRequest(objEN)
        End If
    End Sub
    Private Sub populateLoanRequest(ByVal objen As LoanRequestEN)
        Try
            dbCon.ds2 = objBL.populateLoanRequest(objen)
            If dbCon.ds2.Tables(0).Rows.Count > 0 Then
                txtcode.Text = dbCon.ds2.Tables(0).Rows(0)("U_DocEntry").ToString()
                txtloanName.Text = dbCon.ds2.Tables(0).Rows(0)("U_LoanName").ToString()
                txtReqdate.Text = dbCon.ds2.Tables(0).Rows(0)("U_ReqDate").ToString()
                txtloanAmt.Text = dbCon.ds2.Tables(0).Rows(0)("U_LoanAmt").ToString()
                txtlvecode.Text = dbCon.ds2.Tables(0).Rows(0)("U_LoanCode").ToString()
                txtLodisDate.Text = dbCon.ds2.Tables(0).Rows(0)("U_DisDate").ToString()
                txtLoStdate.Text = dbCon.ds2.Tables(0).Rows(0)("U_InstDate").ToString()
                txtnoEMI.Text = dbCon.ds2.Tables(0).Rows(0)("U_NoEMI").ToString()
                BlnFlag = dbCon.LoanWithDrawStatus("LoanReq", txtcode.Text.Trim())
                If BlnFlag = True Or dbCon.ds2.Tables(0).Rows(0)("U_Z_AppStatus").ToString() <> "P" Then
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

    Private Sub btncancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncancel.Click
        panelview.Visible = True
        PanelNewRequest.Visible = False
        txtcode.Text = ""
        txtlvecode.Text = ""
        txtloanAmt.Text = ""
        txtloanName.Text = ""
        txtReqdate.Text = ""
        txttname.Text = ""
    End Sub

    Private Sub grdLoanRequest_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdLoanRequest.PageIndexChanging
        objEN.Empid = ViewState("EmpId")
        grdLoanRequest.PageIndex = e.NewPageIndex
        PageLoadBind(objEN)
    End Sub

    Private Sub grdSummary_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdSummary.PageIndexChanging
        objEN.Empid = ViewState("EmpId")
        grdSummary.PageIndex = e.NewPageIndex
        PageLoadBind(objEN)
    End Sub

    Private Sub btnWithdraw_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnWithdraw.Click
        Dim blValue As Boolean
        objEN.Empid = Session("UserCode").ToString()
        objEN.StrCode = txtcode.Text.Trim()
        blValue = objBL.WithdrawRequest(objEN)
        If blValue = True Then
            dbCon.strmsg = "alert('Withdraw Loan Request Successfully...')"
            mess(dbCon.strmsg)
        Else
            dbCon.strmsg = "alert('Withdraw Loan Request failed...')"
            mess(dbCon.strmsg)
        End If
        PageLoadBind(objEN)
        panelview.Visible = True
        PanelNewRequest.Visible = False
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
                Dim introw As Integer = gv.RowIndex
                objEN.Empid = Session("UserCode").ToString()
                objEN.StrCode = DocNo.Text.Trim()
                BindHistory(objEN)
                ModalPopupExtender1.Show()
            End If
        Catch ex As Exception
            dbCon.strmsg = "alert('" & ex.Message & "')"
            mess(dbCon.strmsg)
        End Try
    End Sub
    Private Sub BindHistory(ByVal objEN As LoanRequestEN)
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

    Protected Sub lbtsdocnum_Click(ByVal sender As Object, ByVal e As EventArgs)

        Try
            If Session("UserCode") Is Nothing Or Session("SAPCompany") Is Nothing Then
                dbCon.strmsg = "alert('Your session is Expired...')"
                mess(dbCon.strmsg)
                Response.Redirect("Login.aspx?sessionExpired=true", True)
            Else
                Dim link As LinkButton = CType(sender, LinkButton)
                Dim gv As GridViewRow = CType((link.Parent.Parent), GridViewRow)
                Dim DocNo As Label = CType(gv.FindControl("lbtsdocnum"), Label)
                Dim introw As Integer = gv.RowIndex
                objEN.Empid = Session("UserCode").ToString()
                objEN.StrCode = DocNo.Text.Trim()
                BindScheduled(objEN)
                ModalPopupExtender2.Show()
            End If
        Catch ex As Exception
            dbCon.strmsg = "alert('" & ex.Message & "')"
            mess(dbCon.strmsg)
        End Try
    End Sub
    Private Sub BindScheduled(ByVal objEN As LoanRequestEN)
        Try
            dbCon.ds4 = objBL.GetScheduledDetails(objEN)
            If dbCon.ds4.Tables(0).Rows.Count > 0 Then
                GrdScheduled.DataSource = dbCon.ds4.Tables(0)
                GrdScheduled.DataBind()
            Else
                GrdScheduled.DataBind()
            End If
        Catch ex As Exception
            dbCon.strmsg = "alert('" & ex.Message & "')"
            mess(dbCon.strmsg)
        End Try
    End Sub

    Private Sub grdLoanRequest_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdLoanRequest.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim LiDocNo As LinkButton = CType(e.Row.FindControl("lbtndocnum"), LinkButton)
            Dim Liview As LinkButton = CType(e.Row.FindControl("lbtAppHistory"), LinkButton)
            Blflag = dbCon.WithDrawStatusLoan("LoanReq", LiDocNo.Text.Trim())
            If Blflag = True Then
                Liview.Visible = True
            Else
                Liview.Visible = False
            End If
        End If
    End Sub
End Class