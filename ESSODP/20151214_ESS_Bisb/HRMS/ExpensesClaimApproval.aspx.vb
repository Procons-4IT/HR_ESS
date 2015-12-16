Imports System
Imports System.Drawing
Imports System.Globalization
Imports System.Configuration
Imports System.Xml
Imports System.IO
Imports BusinessLogic
Imports DataAccess
Imports EN
Public Class ExpensesClaimApproval
    Inherits System.Web.UI.Page
    Dim dbCon As DBConnectionDA = New DBConnectionDA()
    Dim objBL As ExpClaimApprovalBL = New ExpClaimApprovalBL()
    Dim objEN As ExpClaimApprovalEN = New ExpClaimApprovalEN()
    Dim objDA As ExpClaimApprovalDA = New ExpClaimApprovalDA()
    Dim info As DateTimeFormatInfo = DateTimeFormatInfo.GetInstance(Nothing)
    Dim strMailDocEntry As String = ""
    Dim grdTotal As Decimal = 0
    Dim grdTotal1 As Decimal = 0
    Dim dblusd As Double
    Dim grdTotal2 As Decimal = 0
    Dim grdTotal3 As Decimal = 0
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
            Else
                objEN.EmpId = Session("UserCode").ToString()
                objEN.UserCode = objBL.GetUserCode(objEN)
                objEN.SapCompany = Session("SAPCompany")
                If objEN.UserCode = "" Then
                    dbCon.strmsg = "Employee not mapped in user..."
                    ClientScript.RegisterStartupScript(Me.GetType(), "msg", "<script>alert('" & dbCon.strmsg & "')</script>")
                Else
                    ViewState("LocalCurrency") = objBL.LocalCurrency(objEN)
                    PanelMain.Visible = True
                    panelview.Visible = False
                    ReqApproval(objEN)
                End If
            End If
        End If
    End Sub
    Private Sub ReqApproval(ByVal objEN As ExpClaimApprovalEN)
        Try
            dbCon.ds = objBL.MainGridBind(objEN)
            If dbCon.ds.Tables(0).Rows.Count > 0 Then
                GrdLoadRequest.DataSource = dbCon.ds.Tables(0)
                GrdLoadRequest.DataBind()
            Else
                GrdLoadRequest.DataBind()
            End If

            If dbCon.ds.Tables(1).Rows.Count > 0 Then
                grdSummaryLoad.DataSource = dbCon.ds.Tables(1)
                grdSummaryLoad.DataBind()
            Else
                grdSummaryLoad.DataBind()
            End If
        Catch ex As Exception
            dbCon.strmsg = "" & ex.Message & ""
            ClientScript.RegisterStartupScript(Me.GetType(), "msg", "<script>alert('" & dbCon.strmsg & "')</script>")
        End Try
    End Sub
    Protected Sub lnbtnlblRCode_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If Session("UserCode") Is Nothing Or Session("SAPCompany") Is Nothing Then
                dbCon.strmsg = "Your session is Expired..."
                ClientScript.RegisterStartupScript(Me.GetType(), "msg", "<script>alert('" & dbCon.strmsg & "')</script>")
                Response.Redirect("Login.aspx?sessionExpired=true", True)
            Else
                Dim link As LinkButton = CType(sender, LinkButton)
                Dim gv As GridViewRow = CType((link.Parent.Parent), GridViewRow)
                Dim DocNo As LinkButton = CType(gv.FindControl("lblRCode"), LinkButton)
                Dim introw As Integer = gv.RowIndex
                For Each row1 As GridViewRow In grdRequestApproval.Rows
                    If row1.RowIndex <> introw Then
                        row1.BackColor = Color.White
                    Else
                        row1.BackColor = Color.LimeGreen
                    End If
                Next
                objEN.EmpId = Session("UserCode").ToString()
                objEN.UserCode = objBL.GetUserCode(objEN)
                objEN.DocEntry = DocNo.Text.Trim()
                BindExpenseApproval(objEN)
            End If
        Catch ex As Exception
            dbCon.strmsg = "" & ex.Message & ""
            ClientScript.RegisterStartupScript(Me.GetType(), "msg", "<script>alert('" & dbCon.strmsg & "')</script>")
        End Try
    End Sub
    Private Sub BindExpenseApproval(ByVal objEN As ExpClaimApprovalEN)
        Try
            dbCon.ds1 = objBL.ExpensesRequestApproval(objEN)
            If dbCon.ds1.Tables(0).Rows.Count > 0 Then
                grdRequestApproval.DataSource = dbCon.ds1.Tables(0)
                grdRequestApproval.DataBind()
            Else
                grdRequestApproval.DataBind()
            End If
            If dbCon.ds1.Tables(1).Rows.Count > 0 Then
                lbldocno.Text = dbCon.ds1.Tables(1).Rows(0)("Code").ToString()
                lblsubdt.Text = dbCon.ds1.Tables(1).Rows(0)("U_Z_SubDt").ToString()
                ' lblTANo.Text = dbCon.ds1.Tables(1).Rows(0)("TAEmpID").ToString()
                lblempNo.Text = dbCon.ds1.Tables(1).Rows(0)("U_Z_EmpID").ToString()
                lblempname.Text = dbCon.ds1.Tables(1).Rows(0)("U_Z_EmpName").ToString()
                lblClient.Text = dbCon.ds1.Tables(1).Rows(0)("U_Z_Client").ToString()
                lblProject.Text = dbCon.ds1.Tables(1).Rows(0)("U_Z_Project").ToString()
                lblTripType.Text = dbCon.ds1.Tables(1).Rows(0)("U_Z_TripType").ToString()
                lblTraDesc.Text = dbCon.ds1.Tables(1).Rows(0)("U_Z_TraDesc").ToString()
                lblDocStatus.Text = dbCon.ds1.Tables(1).Rows(0)("U_Z_DocStatus").ToString()
            End If
            PanelMain.Visible = False
            panelview.Visible = True
        Catch ex As Exception
            dbCon.strmsg = "" & ex.Message & ""
            ClientScript.RegisterStartupScript(Me.GetType(), "msg", "<script>alert('" & dbCon.strmsg & "')</script>")
        End Try
    End Sub
    Protected Sub lnbtnlblSCode_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If Session("UserCode") Is Nothing Or Session("SAPCompany") Is Nothing Then
                dbCon.strmsg = "Your session is Expired..."
                ClientScript.RegisterStartupScript(Me.GetType(), "msg", "<script>alert('" & dbCon.strmsg & "')</script>")
                Response.Redirect("Login.aspx?sessionExpired=true", True)
            Else
                Dim link As LinkButton = CType(sender, LinkButton)
                Dim gv As GridViewRow = CType((link.Parent.Parent), GridViewRow)
                Dim DocNo As LinkButton = CType(gv.FindControl("lblSCode"), LinkButton)
                Dim introw As Integer = gv.RowIndex
                For Each row1 As GridViewRow In grdSummaryLoad.Rows
                    If row1.RowIndex <> introw Then
                        row1.BackColor = Color.White
                    Else
                        row1.BackColor = Color.Orange
                    End If
                Next
                objEN.EmpId = Session("UserCode").ToString()
                objEN.UserCode = objBL.GetUserCode(objEN)
                objEN.DocEntry = DocNo.Text.Trim()
                BindExpenseSummaryApproval(objEN)
            End If
        Catch ex As Exception
            dbCon.strmsg = "" & ex.Message & ""
            ClientScript.RegisterStartupScript(Me.GetType(), "msg", "<script>alert('" & dbCon.strmsg & "')</script>")
        End Try
    End Sub
    Private Sub BindExpenseSummaryApproval(ByVal objEN As ExpClaimApprovalEN)
        Try
            dbCon.ds3 = objBL.BindExpenseSummaryApproval(objEN)
            If dbCon.ds3.Tables(0).Rows.Count > 0 Then
                grdSummary.DataSource = dbCon.ds3.Tables(0)
                grdSummary.DataBind()
            Else
                grdSummary.DataBind()
            End If
        Catch ex As Exception
            dbCon.strmsg = "" & ex.Message & ""
            ClientScript.RegisterStartupScript(Me.GetType(), "msg", "<script>alert('" & dbCon.strmsg & "')</script>")
        End Try
    End Sub

    Protected Sub lnkDownload_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim filePath As String = TryCast(sender, LinkButton).CommandArgument
        Dim filename As String = Path.GetFileName(filePath)
        If filename <> "" Then
            Dim path As String = System.IO.Path.Combine(Server.MapPath("~\Document\"), filename)
            ' Dim path As String = MapPath(filename)
            If File.Exists(path) = True Then
                'Dim bts As Byte() = System.IO.File.ReadAllBytes(path)
                'Response.Clear()
                'Response.ClearHeaders()
                'Response.AddHeader("Content-Type", "Application/octet-stream")
                'Response.AddHeader("Content-Length", bts.Length.ToString())

                'Response.AddHeader("Content-Disposition", "attachment;   filename=" & filename)

                'Response.BinaryWrite(bts)

                'Response.Flush()

                'Response.[End]()
                ScriptManager.RegisterStartupScript(Page, [GetType](), "MyScript", "window.open('Download.aspx?ifile=" + HttpUtility.UrlEncode(path) + "');", True)
            Else
                dbCon.strmsg = "File is not available"
                ClientScript.RegisterStartupScript(Me.GetType(), "msg", "<script>alert('" & dbCon.strmsg & "')</script>")
            End If

        End If
    End Sub
    Protected Sub SlnkDownload_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim filePath As String = TryCast(sender, LinkButton).CommandArgument
        Dim filename As String = Path.GetFileName(filePath)
        If filename <> "" Then
            Dim path As String = System.IO.Path.Combine(Server.MapPath("~\Document\"), filename)
            ' Dim path As String = MapPath(filename)
            If File.Exists(path) = True Then
                'Dim bts As Byte() = System.IO.File.ReadAllBytes(path)
                'Response.Clear()
                'Response.ClearHeaders()
                'Response.AddHeader("Content-Type", "Application/octet-stream")
                'Response.AddHeader("Content-Length", bts.Length.ToString())

                'Response.AddHeader("Content-Disposition", "attachment;   filename=" & filename)

                'Response.BinaryWrite(bts)

                'Response.Flush()

                'Response.[End]()
                ScriptManager.RegisterStartupScript(Page, [GetType](), "MyScript", "window.open('Download.aspx?ifile=" + HttpUtility.UrlEncode(path) + "');", True)
            Else
                dbCon.strmsg = "File is not available"
                ClientScript.RegisterStartupScript(Me.GetType(), "msg", "<script>alert('" & dbCon.strmsg & "')</script>")
            End If

        End If
    End Sub

    Private Sub GrdLoadRequest_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GrdLoadRequest.PageIndexChanging
        GrdLoadRequest.PageIndex = e.NewPageIndex
        objEN.EmpId = Session("UserCode").ToString()
        objEN.UserCode = objBL.GetUserCode(objEN)
        ReqApproval(objEN)
    End Sub

    Private Sub grdSummaryLoad_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdSummaryLoad.PageIndexChanging
        grdSummaryLoad.PageIndex = e.NewPageIndex
        objEN.EmpId = Session("UserCode").ToString()
        objEN.UserCode = objBL.GetUserCode(objEN)
        ReqApproval(objEN)
    End Sub

    Private Sub grdRequestApproval_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdRequestApproval.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim info As DateTimeFormatInfo = DateTimeFormatInfo.GetInstance(Nothing)
            Dim ddlyear As DropDownList = DirectCast(e.Row.FindControl("ddlpayyear"), DropDownList)
            Dim ddlmonth As DropDownList = DirectCast(e.Row.FindControl("ddlpaymonth"), DropDownList)
            Dim lblTransdate As Label = DirectCast(e.Row.FindControl("lbltransdt"), Label)
            Dim lblTransAmt As Label = DirectCast(e.Row.FindControl("lbltraamt"), Label)
            Dim lblextRate As Label = DirectCast(e.Row.FindControl("lblExcdamt"), Label)
            Dim lbltranscur As Label = DirectCast(e.Row.FindControl("lblCouType"), Label)
            objEN.SapCompany = Session("SAPCompany")
            For j As Integer = 2010 To 2050
                ddlyear.Items.Add(New ListItem(j.ToString(), j.ToString()))
            Next
            For i As Integer = 1 To 12
                ddlmonth.Items.Add(New ListItem(info.GetMonthName(i), i.ToString()))
            Next

            ddlyear.SelectedValue = e.Row.DataItem("U_Z_Year")
            ddlmonth.SelectedValue = e.Row.DataItem("U_Z_Month")

            Dim rowtotal As Decimal = Convert.ToDecimal(lblTransAmt.Text.Trim())
            grdTotal = grdTotal + rowtotal

            If ViewState("LocalCurrency").ToUpper <> lbltranscur.Text.ToUpper() Then
                If objEN.SapCompany.GetCompanyService.GetAdminInfo.DirectIndirectRate = SAPbobsCOM.BoYesNoEnum.tNO Then
                    If lblTransAmt.Text.Trim() = "" Then
                        lblTransAmt.Text = 0.0
                    End If
                    If CDbl(lblextRate.Text.Trim) > 0 Then
                        dblusd = dbCon.getDocumentQuantity(lblTransAmt.Text.Trim(), objEN.SapCompany) / CDbl(lblextRate.Text.Trim)
                    Else
                        dblusd = 0 ' getDocumentQuantity(fields(9).Trim) / dblExrate  '
                    End If
                Else
                    dblusd = CDbl(lblextRate.Text.Trim) * dbCon.getDocumentQuantity(lblTransAmt.Text.Trim(), objEN.SapCompany) '
                End If
            Else
                dblusd = CDbl(lblTransAmt.Text.Trim())
            End If
            grdTotal2 = grdTotal2 + dblusd
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Liremove As LinkButton = CType(e.Row.FindControl("lnkDownload"), LinkButton)
            objEN.Remarks = e.Row.DataItem("U_Z_Attachment")
            If objEN.Remarks = "" Then
                Liremove.Visible = False
            Else
                Liremove.Visible = True
            End If
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            Dim lbl As Label = CType(e.Row.FindControl("lblCurTotal"), Label)
            lbl.Text = grdTotal.ToString()

            Dim lbl2 As Label = CType(e.Row.FindControl("lblLocCurTotal"), Label)
            lbl2.Text = ViewState("LocalCurrency") & Math.Round(grdTotal2, 2) ' Math.Round(grdTotal2, 2) ' grdTotal2.ToString()


        End If
    End Sub
    Protected Sub lbtnlblCode_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If Session("UserCode") Is Nothing Or Session("SAPCompany") Is Nothing Then
                dbCon.strmsg = "Your session is Expired..."
                ClientScript.RegisterStartupScript(Me.GetType(), "msg", "<script>alert('" & dbCon.strmsg & "')</script>")
                Response.Redirect("Login.aspx?sessionExpired=true", True)
            Else
                Dim link As LinkButton = CType(sender, LinkButton)
                Dim gv As GridViewRow = CType((link.Parent.Parent), GridViewRow)
                Dim DocNo As LinkButton = CType(gv.FindControl("lblCode"), LinkButton)
                objEN.EmpId = Session("UserCode").ToString()
                objEN.DocEntry = DocNo.Text.Trim()
                BindHistory(objEN)
            End If
        Catch ex As Exception
            dbCon.strmsg = "" & ex.Message & ""
            ClientScript.RegisterStartupScript(Me.GetType(), "msg", "<script>alert('" & dbCon.strmsg & "')</script>")
        End Try
    End Sub
    Private Sub BindHistory(ByVal objEN As ExpClaimApprovalEN)
        Try
            dbCon.ds2 = objBL.LoadHistory(objEN)
            If dbCon.ds2.Tables(0).Rows.Count > 0 Then
                grdApprovalHis.DataSource = dbCon.ds2.Tables(0)
                grdApprovalHis.DataBind()
            Else
                grdApprovalHis.DataBind()
            End If
        Catch ex As Exception
            dbCon.strmsg = "'" & ex.Message & "'"
            ClientScript.RegisterStartupScript(Me.GetType(), "msg", "<script>alert('" & dbCon.strmsg & "')</script>")
        End Try
    End Sub
    Protected Sub lbtnlblSCode_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If Session("UserCode") Is Nothing Or Session("SAPCompany") Is Nothing Then
                dbCon.strmsg = "Your session is Expired..."
                ClientScript.RegisterStartupScript(Me.GetType(), "msg", "<script>alert('" & dbCon.strmsg & "')</script>")
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
                BindSummaryHistory(objEN)
                ModalPopupExtender6.Show()
            End If
        Catch ex As Exception
            dbCon.strmsg = "" & ex.Message & ""
            ClientScript.RegisterStartupScript(Me.GetType(), "msg", "<script>alert('" & dbCon.strmsg & "')</script>")
        End Try
    End Sub
    Private Sub BindSummaryHistory(ByVal objEN As ExpClaimApprovalEN)
        Try
            dbCon.ds2 = objBL.LoadHistory(objEN)
            If dbCon.ds2.Tables(0).Rows.Count > 0 Then
                grdHistorySummary.DataSource = dbCon.ds2.Tables(0)
                grdHistorySummary.DataBind()
            Else
                grdHistorySummary.DataBind()
            End If
        Catch ex As Exception
            dbCon.strmsg = "'" & ex.Message & "'"
            ClientScript.RegisterStartupScript(Me.GetType(), "msg", "<script>alert('" & dbCon.strmsg & "')</script>")
        End Try
    End Sub

    Private Sub ddlAppStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAppStatus.SelectedIndexChanged
        Dim row As GridViewRow
        Dim ddlList As DropDownList
        For Each row In grdRequestApproval.Rows
            ddlList = row.FindControl("ddlAppStatus")
            ddlList.SelectedValue = ddlAppStatus.SelectedValue
        Next row
    End Sub

    Private Sub btncancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncancel.Click
        PanelMain.Visible = True
        panelview.Visible = False
    End Sub

    Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim row1 As GridViewRow
        Try
            If Session("UserCode") Is Nothing Or Session("SAPCompany") Is Nothing Then
                dbCon.strmsg = "Your session is Expired..."
                ClientScript.RegisterStartupScript(Me.GetType(), "msg", "<script>alert('" & dbCon.strmsg & "')</script>")
                Response.Redirect("Login.aspx?sessionExpired=true", True)
            Else
                objEN.EmpId = Session("UserCode").ToString()
                objEN.UserCode = objBL.GetUserCode(objEN)
                objEN.EmpUserId = objBL.GetEmpUserid(objEN)
                For Each row1 In grdRequestApproval.Rows
                    objEN.EmpId = lblempNo.Text.Trim()
                    objEN.DocEntry = CType(row1.FindControl("lblCode"), LinkButton).Text
                    objEN.AppStatus = CType(row1.FindControl("ddlAppStatus"), DropDownList).SelectedValue
                    objEN.Year = CType(row1.FindControl("ddlpayyear"), DropDownList).SelectedValue
                    objEN.Month = CType(row1.FindControl("ddlpaymonth"), DropDownList).SelectedValue
                    objEN.Remarks = CType(row1.FindControl("txtRemarks"), TextBox).Text
                    objEN.PostingType = CType(row1.FindControl("lblPostType"), Label).Text
                    objEN.Reimbused = CType(row1.FindControl("lblreimburse"), Label).Text
                    objEN.HistoryType = "ExpCli"
                    objEN.HeaderType = "ExpCli"
                    dbCon.strmsg = objBL.ApprovalValidation(objEN)
                    objEN.DocMessage = "Expense Claim"
                    objEN.SapCompany = Session("SAPCompany")
                    If dbCon.strmsg = "Success" Then
                        dbCon.strmsg = objBL.addUpdateDocument(objEN)
                        If dbCon.strmsg <> "Success" And dbCon.strmsg <> "Successfully approved document..." Then
                            ClientScript.RegisterStartupScript(Me.GetType(), "msg", "<script>alert('" & dbCon.strmsg & "')</script>")
                        End If
                    Else
                        ClientScript.RegisterStartupScript(Me.GetType(), "msg", "<script>alert('" & dbCon.strmsg & "')</script>")
                    End If
                    If objEN.AppStatus = "A" Then
                        If strMailDocEntry = "" Then
                            strMailDocEntry = Integer.Parse(objEN.DocEntry)
                        Else
                            strMailDocEntry = strMailDocEntry & "," & Integer.Parse(objEN.DocEntry)
                        End If
                    End If
                Next
                objEN.EmpId = Session("UserCode").ToString()
                objEN.UserCode = objBL.GetUserCode(objEN)
                objEN.SapCompany = Session("SAPCompany")
                If strMailDocEntry <> "" Then
                    objDA.SendMessage(lbldocno.Text.Trim(), lblempNo.Text.Trim(), objEN.UserCode, strMailDocEntry, objEN.SapCompany, lblempname.Text.Trim())
                    '  Dim StrMailMessage As String = "Expenses Claim request approved for the request number is :" & strMailDocEntry
                    ' dbCon.SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany)
                End If
                PanelMain.Visible = True
                panelview.Visible = False
                ClientScript.RegisterStartupScript(Me.GetType(), "msg", "<script>alert('" & dbCon.strmsg & "')</script>")
                objEN.EmpId = Session("UserCode").ToString()
                objEN.UserCode = objBL.GetUserCode(objEN)
                ReqApproval(objEN)
            End If
        Catch ex As Exception
            dbCon.strmsg = "" & ex.Message & ""
            ClientScript.RegisterStartupScript(Me.GetType(), "msg", "<script>alert('" & dbCon.strmsg & "')</script>")
        End Try
    End Sub


    Private Sub grdSummary_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdSummary.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            objEN.SapCompany = Session("SAPCompany")
            Dim Liremove As LinkButton = CType(e.Row.FindControl("SlnkDownload"), LinkButton)
            Dim lblTransAmt As Label = DirectCast(e.Row.FindControl("lblstraamt"), Label)
            Dim lblextRate As Label = DirectCast(e.Row.FindControl("lblsExcdamt"), Label)
            Dim lbltranscur As Label = DirectCast(e.Row.FindControl("lblsCouType"), Label)
            objEN.Remarks = e.Row.DataItem("U_Z_Attachment")
            If objEN.Remarks = "" Then
                Liremove.Visible = False
            Else
                Liremove.Visible = True
            End If
            Dim rowtotal As Decimal = Convert.ToDecimal(lblTransAmt.Text.Trim())
            grdTotal1 = grdTotal1 + rowtotal

            If ViewState("LocalCurrency").ToUpper <> lbltranscur.Text.ToUpper() Then
                If objEN.SapCompany.GetCompanyService.GetAdminInfo.DirectIndirectRate = SAPbobsCOM.BoYesNoEnum.tNO Then
                    If lblTransAmt.Text.Trim() = "" Then
                        lblTransAmt.Text = 0.0
                    End If
                    If CDbl(lblextRate.Text.Trim) > 0 Then
                        dblusd = dbCon.getDocumentQuantity(lblTransAmt.Text.Trim(), objEN.SapCompany) / CDbl(lblextRate.Text.Trim)
                    Else
                        dblusd = 0 ' getDocumentQuantity(fields(9).Trim) / dblExrate  '
                    End If
                Else
                    dblusd = CDbl(lblextRate.Text.Trim) * dbCon.getDocumentQuantity(lblTransAmt.Text.Trim(), objEN.SapCompany) '
                End If
            Else
                dblusd = CDbl(lblTransAmt.Text.Trim())
            End If
            grdTotal3 = grdTotal3 + dblusd
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            Dim lbl As Label = CType(e.Row.FindControl("lblSCurTotal"), Label)
            lbl.Text = grdTotal1.ToString()
            Dim lbl2 As Label = CType(e.Row.FindControl("lblsLocCurTotal"), Label)
            lbl2.Text = ViewState("LocalCurrency") & Math.Round(grdTotal3, 2)
        End If
    End Sub
End Class