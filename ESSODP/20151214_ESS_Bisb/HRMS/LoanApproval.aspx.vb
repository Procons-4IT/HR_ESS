Imports System
Imports System.Drawing
Imports System.Globalization
Imports BusinessLogic
Imports DataAccess
Imports EN
Public Class LoanApproval
    Inherits System.Web.UI.Page
    Dim dbCon As DBConnectionDA = New DBConnectionDA()
    Dim objBL As LoanApprovalBL = New LoanApprovalBL()
    Dim objEN As LoanApprovalEN = New LoanApprovalEN()
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
                objEN.HeaderType = "LoanReq"
                objEN.HistoryType = "LoanReq"
                ReqApproval(objEN)
                txtcode.Text = ""
                txtempid.Text = ""
                txtlveCode.Text = ""
            End If
        End If
    End Sub
    Private Sub ReqApproval(ByVal objEN As LoanApprovalEN)
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

            If dbCon.ds.Tables(1).Rows.Count > 0 Then
                grdSummary.DataSource = dbCon.ds.Tables(1)
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
    Protected Sub lbtndocnum_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try

            If Session("UserCode") Is Nothing Or Session("SAPCompany") Is Nothing Then
                dbCon.strmsg = "alert('Your session is Expired...')"
                mess(dbCon.strmsg)
                Response.Redirect("Login.aspx?sessionExpired=true", True)
            Else
                Dim link As LinkButton = CType(sender, LinkButton)
                Dim gv As GridViewRow = CType((link.Parent.Parent), GridViewRow)
                Dim DocNo As LinkButton = CType(gv.FindControl("lblCode"), LinkButton)
                Dim Empid As Label = CType(gv.FindControl("lblEmpid"), Label)
                objEN.EmpId = Empid.Text.Trim()
                objEN.DocEntry = DocNo.Text.Trim()
                Dim introw As Integer = gv.RowIndex
                For Each row1 As GridViewRow In grdRequestApproval.Rows
                    If row1.RowIndex <> introw Then
                        row1.BackColor = Color.White
                    Else
                        row1.BackColor = Color.Orange
                    End If
                Next
                dbCon.dss1 = objBL.PopulateLoanDetails(objEN)
                If dbCon.dss1.Tables(0).Rows.Count > 0 Then
                    txtcode.Text = DocNo.Text.Trim()
                    txtempid.Text = Empid.Text.Trim()
                    txtlveCode.Text = dbCon.dss1.Tables(0).Rows(0)("U_LoanCode").ToString()
                    txtLoanAmt.Text = dbCon.dss1.Tables(0).Rows(0)("U_LoanAmt").ToString()
                    txtLoandisDt.Text = dbCon.dss1.Tables(0).Rows(0)("U_Z_DisDate").ToString()
                    txtinStDate.Text = dbCon.dss1.Tables(0).Rows(0)("U_Z_StartDate").ToString()
                    txtInstallment.Text = dbCon.dss1.Tables(0).Rows(0)("U_Z_NoEMI").ToString()
                    txtloanName.Text = dbCon.dss1.Tables(0).Rows(0)("U_LoanName").ToString()
                End If
                objEN.EmpId = Session("UserCode").ToString()
                objEN.DocEntry = DocNo.Text.Trim()
                objEN.HistoryType = "LoanReq"
                BindHistory(objEN)
            End If
        Catch ex As Exception
            dbCon.strmsg = "alert('" & ex.Message & "')"
            mess(dbCon.strmsg)
        End Try
    End Sub
    Private Sub BindHistory(ByVal objEN As LoanApprovalEN)
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
                Dim EmpCode As Label = CType(gv.FindControl("lblsEmpid"), Label)
                Dim introw As Integer = gv.RowIndex
                For Each row1 As GridViewRow In grdSummary.Rows
                    If row1.RowIndex <> introw Then
                        row1.BackColor = Color.White
                    Else
                        row1.BackColor = Color.Orange
                    End If
                Next
                objEN.EmpId = EmpCode.Text.Trim()
                objEN.DocEntry = DocNo.Text.Trim()
                objEN.HistoryType = "LoanReq"
                SummaryHistory(objEN)
            End If
        Catch ex As Exception
            dbCon.strmsg = "alert('" & ex.Message & "')"
            mess(dbCon.strmsg)
        End Try
    End Sub
    Private Sub SummaryHistory(ByVal objEN As LoanApprovalEN)
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
    Private Sub grdRequestApproval_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdRequestApproval.PageIndexChanging
        grdRequestApproval.PageIndex = e.NewPageIndex
        objEN.EmpId = Session("UserCode").ToString()
        objEN.UserCode = objBL.GetUserCode(objEN)
        objEN.HeaderType = "LoanReq"
        objEN.HistoryType = "LoanReq"
        ReqApproval(objEN)
    End Sub

    Private Sub grdSummary_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdSummary.PageIndexChanging
        grdSummary.PageIndex = e.NewPageIndex
        objEN.EmpId = Session("UserCode").ToString()
        objEN.UserCode = objBL.GetUserCode(objEN)
        objEN.HeaderType = "LoanReq"
        objEN.HistoryType = "LoanReq"
        ReqApproval(objEN)
    End Sub
    Public Function ApprovalValidation(ByVal objEN As LoanApprovalEN) As String
        Dim Blnflag As Boolean = False
        Dim dblEntilAfter, dblMaxInstallment, dblLoanMaxPercentage, dblEMI, dblbaiscmin, dblbasicmax, dblEMIPercentage, dblLoanMin, dblLoanMax, dblEMPSetupPercentage, dblEOSPercetage, dblNoofDays As Double
        Dim dblLoanAmount, dblNoofEMI, dblEMIAmount, dblLoanAmt, dblYoE As Double
        Dim strGLAccount As String
        Dim oTest As SAPbobsCOM.Recordset
        oTest = objEN.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        Try
            If objEN.AppStatus = "R" Then
                If objEN.Remarks = "" Then
                    dbCon.strmsg = "Remarks is missing..."
                    Return dbCon.strmsg
                End If
            ElseIf objEN.AppStatus = "A" Then
                Blnflag = objBL.FinalApprovalValidate(objEN)
                If Blnflag = True Then
                    If txtInstallment.Text.Trim() <> "" Then
                        dblNoofDays = txtInstallment.Text.Trim()
                    Else
                        dblNoofDays = 0
                    End If
                    If txtLoanAmt.Text.Trim() = "" Then
                        dbCon.strmsg = "Loan Amount is missing..."
                        Return dbCon.strmsg
                    ElseIf txtinStDate.Text.Trim() = "" Then
                        dbCon.strmsg = "Installment Start date is missing..."
                        Return dbCon.strmsg
                    ElseIf txtLoandisDt.Text.Trim() = "" Then
                        dbCon.strmsg = "Loan distribution date is missing..."
                        Return dbCon.strmsg
                    ElseIf dblNoofDays <= 0 Then
                        dbCon.strmsg = "Loan Amount is missing..."
                        Return dbCon.strmsg
                    End If
                    dbCon.dss = objBL.GetLoanDetails(objEN)
                    If dbCon.dss.Tables(0).Rows.Count > 0 Then
                        objEN.InsDate = Date.ParseExact(txtinStDate.Text.Trim().Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture)
                        objEN.EmpId = txtempid.Text.Trim()
                        objEN.SapCompany = Session("SAPCompany")
                        objEN.Year = Year(objEN.InsDate)
                        objEN.Month = Month(objEN.InsDate)
                        dblYoE = objBL.getYearofExperience(objEN)
                        dblEntilAfter = dbCon.dss.Tables(0).Rows(0)("U_Z_EarnAfter")
                        dblMaxInstallment = dbCon.dss.Tables(0).Rows(0)("U_Z_InsMaxPeriod")
                        dblLoanMaxPercentage = dbCon.dss.Tables(0).Rows(0)("U_Z_InsMaxPer")
                        dblLoanMin = dbCon.dss.Tables(0).Rows(0)("U_Z_LoanMin")
                        dblLoanMax = dbCon.dss.Tables(0).Rows(0)("U_Z_LoanMax")
                        dblEMPSetupPercentage = dbCon.dss.Tables(0).Rows(0)("U_Z_EMIPERCENTAGE")
                        dblEOSPercetage = dbCon.dss.Tables(0).Rows(0)("U_Z_EOSPERCENTAGE")
                        dblbaiscmin = dbCon.dss.Tables(0).Rows(0)("U_Z_LoanAmtMin")
                        dblbasicmax = dbCon.dss.Tables(0).Rows(0)("U_Z_LoanAmtMax")
                        txtGLACC.Text = dbCon.dss.Tables(0).Rows(0)("U_Z_GLACC")
                        dblEntilAfter = dblEntilAfter / 12
                    End If
                    dblLoanAmt = txtLoanAmt.Text.Trim()
                    dblNoofEMI = txtInstallment.Text.Trim()
                    txtemiAmt.Text = dblLoanAmt / dblNoofEMI
                    objEN.EndDate = objEN.InsDate.AddMonths(CInt(dblNoofDays))
                    txtenddate.Text = objEN.EndDate.ToString("dd/MM/yyyy")
                    objEN.DisDate = Date.ParseExact(txtLoandisDt.Text.Trim().Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture)
                    objEN.InsDate = Date.ParseExact(txtinStDate.Text.Trim().Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture)
                    If objEN.InsDate < objEN.DisDate Then
                        dbCon.strmsg = "Loan Installment Start Date should be greater than or equal to Loan distribution Date"
                        Return dbCon.strmsg
                    End If
                    If objEN.InsDate > objEN.EndDate Then
                        dbCon.strmsg = "Loan Installment Start Date should be Less than to Loan Installment End Date"
                        Return dbCon.strmsg
                    End If
                    If dblYoE < dblEntilAfter Then
                        dbCon.strmsg = "You are eligible to avail this loan only after : " & dblEntilAfter & " Months"
                        Return dbCon.strmsg
                    End If

                    If dbCon.dss.Tables(0).Rows(0)("U_Z_OverLap").ToString() = "N" Then
                        objEN.EmpId = txtempid.Text.Trim()
                        objEN.LoanCode = txtlveCode.Text.Trim()
                        Blnflag = objBL.LoanOverLap(objEN)
                        If Blnflag = True Then
                            dbCon.strmsg = "This loan already availed to this employee and not allowed to overlap"
                            Return dbCon.strmsg
                        End If
                    End If
                    If dblNoofDays > dblMaxInstallment Then
                        dbCon.strmsg = "Maximum Installment for this Loan is : " & dblMaxInstallment
                        Return dbCon.strmsg
                    End If
                    dblEMI = txtemiAmt.Text.Trim()
                    dblLoanAmount = txtLoanAmt.Text.Trim()
                    If dblEMI <= 0 Then
                        dbCon.strmsg = "Installment Amount should be greater than zero"
                        Return dbCon.strmsg
                    End If
                    If dblEMI > dblLoanAmount Then
                        dbCon.strmsg = "Installment Amount should be less than Loan Amount"
                        Return dbCon.strmsg
                    End If
                    dblEMIAmount = dblEMI * dblNoofDays

                    If Math.Round(dblEMIAmount, 3) < Math.Round(dblLoanAmount, 3) Then
                        dbCon.strmsg = "Loan amount  should be equal to Installment Amount  * number of EMI"
                        Return dbCon.strmsg
                    End If

                    If dblLoanAmount >= dblLoanMin And dblLoanAmount <= dblLoanMax Then
                    Else
                        dbCon.strmsg = "Loan amount should be between " & dblLoanMin & " and " & dblLoanMax
                        Return dbCon.strmsg
                    End If

                    'oTest.DoQuery("Select * from OHEM where ""empID""=" & txtempid.Text.Trim())
                    'Dim dblbasissalary As Double = oTest.Fields.Item("salary").Value
                    'dblbaiscmin = dblbasissalary * dblbaiscmin / 100
                    'dblbasicmax = dblbasissalary * dblbasicmax / 100

                    'If dblLoanAmount >= dblbaiscmin And dblLoanAmount <= dblbasicmax Then
                    'Else
                    '    dbCon.strmsg = "Loan amount should be between " & dblbaiscmin & " and " & dblbasicmax & " based on Basic Salary %"
                    '    Return dbCon.strmsg
                    'End If

                    'If dblEMPSetupPercentage > 0 Then 'Validation on EMI Percentage on Basic 
                    '    dblEMIPercentage = dblbasissalary * dblEMPSetupPercentage / 100
                    '    If dblEMI > dblEMIPercentage And dblEMPSetupPercentage > 0 Then
                    '        dbCon.strmsg = "Installment Amount shoud be less than or equal to " & dblEMIPercentage
                    '        Return dbCon.strmsg
                    '    End If
                    'End If
                End If
            End If
        Catch ex As Exception
            dbCon.strmsg = "alert('" & ex.Message & "')"
            mess(dbCon.strmsg)
        End Try
        Return "Success"
    End Function
    Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click

        Try
            If Session("UserCode") Is Nothing Or Session("SAPCompany") Is Nothing Then
                dbCon.strmsg = "alert('Your session is Expired...')"
                mess(dbCon.strmsg)
                Response.Redirect("Login.aspx?sessionExpired=true", True)
            ElseIf txtcode.Text <> "" Or txtcode.Text <> Nothing Then
                objEN.SapCompany = Session("SAPCompany")
                objEN.EmpId = Session("UserCode").ToString()
                objEN.UserCode = objBL.GetUserCode(objEN)
                objEN.EmpUserId = objBL.GetEmpUserid(objEN)
                objEN.DocEntry = txtcode.Text.Trim()
                objEN.EmpId = txtempid.Text.Trim()
                objEN.HistoryType = "LoanReq"
                objEN.HeaderType = "LoanReq"
                objEN.AppStatus = ddlAppStatus.SelectedValue
                objEN.Remarks = txtcomments.Text.Trim()
                objEN.LoanCode = txtlveCode.Text.Trim()
                objEN.LoanAmt = txtLoanAmt.Text.Trim()
                objEN.LoanName = txtloanName.Text.Trim()
                If txtinStDate.Text.Trim() <> "" Then
                    objEN.InsDate = Date.ParseExact(txtinStDate.Text.Trim().Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture)
                End If
                If txtLoandisDt.Text.Trim() <> "" Then
                    objEN.DisDate = Date.ParseExact(txtLoandisDt.Text.Trim().Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture)
                End If
                If txtInstallment.Text.Trim() <> "" Then
                    objEN.NoInst = txtInstallment.Text.Trim()
                Else
                    objEN.NoInst = "0"
                End If
                dbCon.strmsg = ApprovalValidation(objEN)
                objEN.DocMessage = "Loan Request"
                If txtenddate.Text.Trim() <> "" Then
                    objEN.EndDate = Date.ParseExact(txtenddate.Text.Trim().Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture)
                End If
                objEN.EMIAmt = txtemiAmt.Text.Trim()
                objEN.GLAccount = txtGLACC.Text.Trim()
                objEN.SapCompany = Session("SAPCompany")
                If dbCon.strmsg = "Success" Then
                    dbCon.strmsg = objBL.addUpdateDocument(objEN)
                    If dbCon.strmsg = "Success" Then
                        dbCon.strmsg = "alert('Document Approved Successfully....')"
                        mess(dbCon.strmsg)
                        objEN.DocEntry = txtcode.Text.Trim()
                        objEN.EmpId = txtempid.Text.Trim()
                        objEN.HistoryType = "LoanReq"
                        objEN.HeaderType = "LoanReq"
                        ReqApproval(objEN)
                        grdApprovalHis.DataBind()
                        Clear()
                    Else
                        dbCon.strmsg = "alert('" & dbCon.strmsg & "')"
                        mess(dbCon.strmsg)
                        objEN.DocEntry = txtcode.Text.Trim()
                        objEN.EmpId = txtempid.Text.Trim()
                        objEN.HistoryType = "LoanReq"
                        objEN.HeaderType = "LoanReq"
                        ReqApproval(objEN)
                        grdApprovalHis.DataBind()
                        Clear()
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
    Private Sub Clear()
        txtcode.Text = ""
        txtempid.Text = ""
        txtlveCode.Text = ""
        ddlAppStatus.SelectedIndex = 0
        txtcomments.Text = ""
        txtLoanAmt.Text = ""
        txtemiAmt.Text = ""
        txtenddate.Text = ""
        txtInstallment.Text = ""
        txtinStDate.Text = ""
        txtLoandisDt.Text = ""
        txtloanName.Text = ""
    End Sub
End Class