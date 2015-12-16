Imports System
Imports System.Data
Imports System.Configuration
Imports System.Globalization
Imports System.Xml
Imports System.IO
Imports System.Net
Imports EN
Imports DataAccess
Imports BusinessLogic
Public Class ExpensesClaimReq
    Inherits System.Web.UI.Page
    Dim objEN As ClaimRequestEN = New ClaimRequestEN()
    Dim objBL As ClaimRequestBL = New ClaimRequestBL()
    Dim dbcon As DBConnectionDA = New DBConnectionDA()
    Dim objDA As DynamicApprovalDA = New DynamicApprovalDA()
    Dim dblcur, dblexrate, dblusd As Double
    Dim TransDate, Submitdate As Date
    Dim intTempID, Targetpath, strpath, fileName, strAttachment, strPayment, strMailCode, strCode, LineDocEntry As String
    Dim Blflag As Boolean
    Dim oRecSet, oTemp As SAPbobsCOM.Recordset
    Dim StrDimCode1 As Boolean = False
    Dim StrDimCode2 As Boolean = False
    Dim StrDimCode3 As Boolean = False
    Dim StrDimCode4 As Boolean = False
    Dim StrDimCode5 As Boolean = False
    Dim grdTotal As Decimal = 0
    Dim grdTotal1 As Decimal = 0
    Dim grdLocTotal As Decimal = 0
    Dim grdLocTotal1 As Decimal = 0
    Dim grdTotal2 As Decimal = 0
    Dim grdLocTotal2 As Decimal = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Session("UserCode") Is Nothing Then
                Response.Redirect("Login.aspx?sessionExpired=true", True)
            ElseIf Session("SAPCompany") Is Nothing Then
                If Session("EmpUserName").ToString() Is Nothing Or Session("UserPwd").ToString() Is Nothing Then
                    strError = dbcon.Connection()
                Else
                    strError = dbcon.Connection(Session("EmpUserName").ToString(), Session("UserPwd").ToString())
                End If
                If strError <> "Success" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "js", "<script>alert('" & strError & "')</script>")
                Else
                    Session("SAPCompany") = dbcon.objMainCompany
                    objEN.SapCompany = Session("SAPCompany")
                    ViewState("LocalCurrency") = objBL.LocalCurrency(objEN)
                End If
            End If
            lblerror.Text = ""
            panelview.Visible = True
            PanelNewRequest.Visible = False
            objEN.EmpId = Session("UserCode").ToString()
            objEN.SapCompany = Session("SAPCompany")
            objEN.SessionID = Session("SessionId").ToString()
            Try
                ViewState("LocalCurrency") = objBL.LocalCurrency(objEN)
            Catch ex As Exception
            End Try
            ViewState("EmpId") = objEN.EmpId
            objBL.DeleteTempTable(objEN)
            PageLoadBind(objEN)
            DropDownBind()
            ' ViewState("LocalCurrency") = objBL.LocalCurrency(objEN)
            BindDistriRule()
            BindDistriRule(objEN.EmpId)
        End If
    End Sub
    Private Sub BindDistriRule()
        Dim StrDimCode As String
        Try
            dbcon.dss2 = objBL.BindDistriRule()
            If dbcon.dss2.Tables(0).Rows.Count > 0 Then
                For intRow As Integer = 0 To dbcon.dss2.Tables(0).Rows.Count - 1
                    StrDimCode = dbcon.dss2.Tables(0).Rows(intRow)("DimCode").ToString()
                    If StrDimCode = "1" Then
                        lblDis1.Text = dbcon.dss2.Tables(0).Rows(intRow)("DimDesc").ToString()
                        dbcon.Dropdown1("Select OcrCode,OcrName from OOCR where DimCode='1'", "OcrCode", "OcrName", ddlDis1)
                        lblDis1.Visible = True
                        ddlDis1.Visible = True
                        StrDimCode1 = True
                    End If
                    If StrDimCode = "2" Then
                        lblDis2.Text = dbcon.dss2.Tables(0).Rows(intRow)("DimDesc").ToString()
                        dbcon.Dropdown1("Select OcrCode,OcrName from OOCR where DimCode='2'", "OcrCode", "OcrName", ddlDis2)
                        lblDis2.Visible = True
                        ddlDis2.Visible = True
                        StrDimCode2 = True
                    End If
                    If StrDimCode = "3" Then
                        lblDis3.Text = dbcon.dss2.Tables(0).Rows(intRow)("DimDesc").ToString()
                        dbcon.Dropdown1("Select OcrCode,OcrName from OOCR where DimCode='3'", "OcrCode", "OcrName", ddlDis3)
                        lblDis3.Visible = True
                        ddlDis3.Visible = True
                        StrDimCode3 = True
                    End If
                    If StrDimCode = "4" Then
                        lblDis4.Text = dbcon.dss2.Tables(0).Rows(intRow)("DimDesc").ToString()
                        dbcon.Dropdown1("Select OcrCode,OcrName from OOCR where DimCode='4'", "OcrCode", "OcrName", ddlDis4)
                        lblDis4.Visible = True
                        ddlDis4.Visible = True
                        StrDimCode4 = True
                    End If
                    If StrDimCode = "5" Then
                        lblDis5.Text = dbcon.dss2.Tables(0).Rows(intRow)("DimDesc").ToString()
                        dbcon.Dropdown1("Select OcrCode,OcrName from OOCR where DimCode='5'", "OcrCode", "OcrName", ddlDis5)
                        lblDis5.Visible = True
                        ddlDis5.Visible = True
                        StrDimCode5 = True
                    End If
                Next
                If StrDimCode1 = False Then
                    lblDis1.Visible = False
                    ddlDis1.Visible = False
                End If
                If StrDimCode2 = False Then
                    lblDis2.Visible = False
                    ddlDis2.Visible = False
                End If
                If StrDimCode3 = False Then
                    lblDis3.Visible = False
                    ddlDis3.Visible = False
                End If
                If StrDimCode4 = False Then
                    lblDis4.Visible = False
                    ddlDis4.Visible = False
                End If
                If StrDimCode5 = False Then
                    lblDis5.Visible = False
                    ddlDis5.Visible = False
                End If
            End If
        Catch ex As Exception
            dbcon.strmsg = ex.Message
            mess(dbcon.strmsg)
        End Try
    End Sub
    Private Sub PageLoadBind(ByVal objen As ClaimRequestEN)
        dbcon.ds = objBL.PageLoadBind(objen)
        If dbcon.ds.Tables(0).Rows.Count > 0 Then
            grdExpClaimRequest.DataSource = dbcon.ds.Tables(0)
            grdExpClaimRequest.DataBind()
        Else
            grdExpClaimRequest.DataBind()
        End If
        If dbcon.ds.Tables(1).Rows.Count > 0 Then
            grdexpenses.DataSource = dbcon.ds.Tables(1)
            grdexpenses.DataBind()
        Else
            grdexpenses.DataBind()
        End If
        If dbcon.ds.Tables(2).Rows.Count > 0 Then
            grdtravel.DataSource = dbcon.ds.Tables(2)
            grdtravel.DataBind()
        Else
            grdtravel.DataBind()
        End If
    End Sub
    Private Sub DropDownBind()
        Try
            dbcon.ds1 = objBL.DropDownBind()
            If dbcon.ds1.Tables(0).Rows.Count > 0 Then
                ddltranscur.DataTextField = "Name"
                ddltranscur.DataValueField = "Code"
                ddltranscur.DataSource = dbcon.ds1.Tables(0)
                ddltranscur.DataBind()
                ddltranscur.Items.Insert(0, "---Select---")
            Else
                ddltranscur.DataBind()
                ddltranscur.Items.Insert(0, "---Select---")
            End If

            If dbcon.ds1.Tables(1).Rows.Count > 0 Then
                ddlpaymethod.DataTextField = "Name"
                ddlpaymethod.DataValueField = "Code"
                ddlpaymethod.DataSource = dbcon.ds1.Tables(1)
                ddlpaymethod.DataBind()
                ddlpaymethod.Items.Insert(0, "")
            Else
                ddlpaymethod.DataBind()
                ddlpaymethod.Items.Insert(0, "")
            End If
        Catch ex As Exception
            dbcon.strmsg = ex.Message
            mess(dbcon.strmsg)
        End Try
    End Sub
    Protected Sub Btncallpop_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btncallpop.ServerClick
        Dim str1, str2, str3 As String
        str1 = txtpopunique.Text.Trim()
        str2 = txtpoptno.Text.Trim()
        str3 = txttname.Text.Trim()
        If txthidoption.Text = "Expenses" Then
            If txtpoptno.Text.Trim() <> "" Then
                txtexpcode.Text = txtpopunique.Text.Trim()
                txtexptype.Text = txtpoptno.Text.Trim()
                objEN.DocEntry = txtexpcode.Text.Trim()
                dbcon.ds4 = objBL.AllowanceCode(objEN)
                If dbcon.ds4.Tables(0).Rows.Count > 0 Then
                    txtAllowance.Text = dbcon.ds4.Tables(0).Rows(0)("U_Z_AlloCode").ToString()
                    txtCredit.Text = dbcon.ds4.Tables(0).Rows(0)("U_Z_ActCode").ToString()
                    txtdebit.Text = dbcon.ds4.Tables(0).Rows(0)("U_Z_DebitCode").ToString()
                    txtposting.Text = dbcon.ds4.Tables(0).Rows(0)("U_Z_Posting").ToString()
                    txttrasamt.Text = dbcon.ds4.Tables(0).Rows(0)("U_Z_Amount").ToString()
                    lblOverLap.Text = dbcon.ds4.Tables(0).Rows(0)("U_Z_OverLap").ToString()
                    ddltranscur.SelectedValue = ViewState("LocalCurrency").ToString()
                    ddlreimbused.SelectedValue = "Y"
                    dbcon.objMainCompany = Session("SAPCompany")
                    If txttrasamt.Text <> "" Then
                        dblcur = txttrasamt.Text.Trim()
                    Else
                        dblcur = 0.0
                    End If
                    If txtexrate.Text <> "" Then
                        dblexrate = txtexrate.Text.Trim()
                    Else
                        dblexrate = 0.0
                    End If
                    If ViewState("LocalCurrency").ToUpper <> ddltranscur.SelectedValue.ToUpper() Then
                        If dbcon.objMainCompany.GetCompanyService.GetAdminInfo.DirectIndirectRate = SAPbobsCOM.BoYesNoEnum.tNO Then
                            If txttrasamt.Text = "" Then
                                txttrasamt.Text = 0.0
                            End If
                            If dblexrate > 0 Then
                                dblusd = dbcon.getDocumentQuantity(txttrasamt.Text.Trim(), dbcon.objMainCompany) / dblexrate  '
                            Else
                                dblusd = 0 ' getDocumentQuantity(fields(9).Trim) / dblExrate  '
                            End If
                        Else
                            dblusd = dblexrate * dbcon.getDocumentQuantity(txttrasamt.Text.Trim(), dbcon.objMainCompany) '
                        End If
                        txtlocamt.Text = ViewState("LocalCurrency") & Math.Round(dblusd, 2)
                    Else
                        dblusd = CDbl(txttrasamt.Text.Trim())
                        txtlocamt.Text = ViewState("LocalCurrency") & Math.Round(dblusd, 2)
                    End If
                    If ddlreimbused.SelectedValue = "Y" Then
                        txtreimbuse.Text = txtlocamt.Text.Trim()
                    Else
                        txtreimbuse.Text = ViewState("LocalCurrency") & 0.0
                    End If
                    txttrasdt.Focus()
                End If
                ModalPopupExtender6.Show()
            End If
        ElseIf txthidoption.Text = "Travel" Then
            If txtpoptno.Text.Trim() <> "" Then
                txttravelCode.Text = txtpopunique.Text.Trim()
                txttraveldesc.Text = txtpoptno.Text.Trim()
            End If
        End If

    End Sub
    Private Sub Clear()
        txtexpcode.Text = ""
        ddltriptype.SelectedIndex = 0
        txttravelCode.Text = ""
        txttraveldesc.Text = ""
        txtexptype.Text = ""
        txtAllowance.Text = ""
        txtClient.Text = ""
        txtproject.Text = ""
        txttrasdt.Text = ""
        txtcity.Text = ""
        ddltranscur.SelectedIndex = 0
        txttrasamt.Text = "0.0"
        txtexrate.Text = "1.00"
        txtlocamt.Text = ""
        ddlreimbused.SelectedIndex = 0
        txtreimbuse.Text = ""
        ddlpaymethod.SelectedIndex = 0
        txtnotes.Text = ""
        ddlStatus.SelectedIndex = 0
        lblerror.Text = ""
        txtDisRule.Text = ""
        lblCardCode.Text = ""
        txtCredit.Text = ""
        txtdebit.Text = ""
        txtposting.Text = "P"
        lblOverLap.Text = ""
    End Sub
    Private Sub BindDistriRule(ByVal EmpId As String)
        Dim StrDimCode As String
        Try
            StrDimCode = objBL.BindDistriRule1(EmpId)
            If StrDimCode <> "" Then
                txtDisRule.Text = StrDimCode.Trim()
            Else
                txtDisRule.Text = ""
            End If
        Catch ex As Exception
            dbcon.strmsg = ex.Message
            mess(dbcon.strmsg)
        End Try
    End Sub

    Private Sub btnnew_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnnew.Click
        Try
            Clear()
            NewExpense.Visible = True
            btnSubmit.Visible = True
            panelview.Visible = False
            PanelNewRequest.Visible = True
            btnAdd.Visible = True
            lblsubdt.Text = Now.Date
            lbldocno.Text = "New" 'dbcon.Getmaxcode("[@Z_HR_OEXPCL]", "Code")
            lblempNo.Text = Session("UserCode").ToString()
            BindDistriRule(lblempNo.Text.Trim())
            lblempname.Text = Session("UserName").ToString()
            lblCardCode.Text = objBL.GetCardCode(lblempNo.Text.Trim())
            objEN.EmpId = lblempNo.Text.Trim()
            objEN.SessionID = Session("SessionId").ToString()
            lblTANo.Text = objBL.PopulateTANo(Session("UserCode").ToString())
            dbcon.strmsg = objBL.DeleteTempTable(objEN)
            grdRequestExpenses.DataBind()
            txtexrate.Text = "1.00"
            txttrasamt.Text = "0.0"
            ViewState("Client") = ""
            ViewState("Project") = ""
            objEN.EmpId = lblempNo.Text.Trim()
            objEN.SessionID = Session("SessionId").ToString()
            objEN.DocEntry = lbldocno.Text.Trim()
            BindNewRequest(objEN)
        Catch ex As Exception
            dbcon.strmsg = "" & ex.Message & ""
            mess(dbcon.strmsg)
        End Try
    End Sub

    Private Sub mess(ByVal str As String)
        ClientScript.RegisterStartupScript(Me.GetType(), "msg", "<script>alert('" & dbcon.strmsg & "')</script>")
    End Sub
    Private Sub grdExpClaimRequest_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdExpClaimRequest.PageIndexChanging
        grdExpClaimRequest.PageIndex = e.NewPageIndex
        objEN.EmpId = ViewState("EmpId").ToString()
        PageLoadBind(objEN)
    End Sub
    Private Sub grdexpenses_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdexpenses.RowDataBound
        txtpoptno.Text = ""
        txtpopunique.Text = ""
        txthidoption.Text = ""
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onclick", "popupdisplay('Expenses','" + (DataBinder.Eval(e.Row.DataItem, "Code")).ToString().Trim() + "','" + (DataBinder.Eval(e.Row.DataItem, "U_Z_ExpName")).ToString().Replace("'", "").Trim() + "');")
        End If
    End Sub

    Private Sub grdtravel_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdtravel.RowDataBound
        txtpoptno.Text = ""
        txtpopunique.Text = ""
        txthidoption.Text = ""
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onclick", "popupdisplay('Travel','" + (DataBinder.Eval(e.Row.DataItem, "DocEntry")).ToString().Trim() + "','" + (DataBinder.Eval(e.Row.DataItem, "U_Z_TraName")).ToString().Replace("'", "").Trim() + "');")
        End If
    End Sub
    Private Sub ddltriptype_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddltriptype.SelectedIndexChanged
        ViewState("Client") = txtClient.Text.Trim()
        ViewState("Project") = txtProject.Text.Trim()
        If ddltriptype.SelectedValue = "N" Then
            txttravelCode.Text = ""
            txttraveldesc.Text = ""
            txttraveldesc.Enabled = False
            btnfindtrcode.Visible = False
        ElseIf ddltriptype.SelectedValue = "E" Then
            txttravelCode.Text = ""
            txttraveldesc.Text = ""
            txttraveldesc.Enabled = False
            btnfindtrcode.Visible = True
        End If
    End Sub
    Private Sub ddlreimbused_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlreimbused.SelectedIndexChanged
        If ddlreimbused.SelectedValue = "Y" Then
            txtreimbuse.Text = txtlocamt.Text.Trim()
        Else
            txtreimbuse.Text = ViewState("LocalCurrency") & 0.0
        End If
        ddlpaymethod.Focus()
        ModalPopupExtender6.Show()
    End Sub

    Private Sub txttrasamt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txttrasamt.TextChanged
        dbcon.objMainCompany = Session("SAPCompany")
        If txttrasamt.Text <> "" Then
            dblcur = txttrasamt.Text.Trim()
        Else
            dblcur = 0.0
        End If
        If txtexrate.Text <> "" Then
            dblexrate = txtexrate.Text.Trim()
        Else
            dblexrate = 0.0
        End If
        ' dblusd = dblcur * dblexrate
        ' txtlocamt.Text = ViewState("LocalCurrency") & Math.Round(dblusd, 2)
        If ViewState("LocalCurrency").ToUpper <> ddltranscur.SelectedValue.ToUpper() Then
            If dbcon.objMainCompany.GetCompanyService.GetAdminInfo.DirectIndirectRate = SAPbobsCOM.BoYesNoEnum.tNO Then
                If txttrasamt.Text = "" Then
                    txttrasamt.Text = 0.0
                End If
                If dblexrate > 0 Then
                    dblusd = dbcon.getDocumentQuantity(txttrasamt.Text.Trim(), dbcon.objMainCompany) / dblexrate  '
                Else
                    dblusd = 0 ' getDocumentQuantity(fields(9).Trim) / dblExrate  '
                End If
            Else
                dblusd = dblexrate * dbcon.getDocumentQuantity(txttrasamt.Text.Trim(), dbcon.objMainCompany) '
            End If
            txtlocamt.Text = ViewState("LocalCurrency") & Math.Round(dblusd, 2)
        Else
            dblusd = CDbl(txttrasamt.Text.Trim())
            txtlocamt.Text = ViewState("LocalCurrency") & Math.Round(dblusd, 2)
        End If
        If ddlreimbused.SelectedValue = "Y" Then
            txtreimbuse.Text = txtlocamt.Text.Trim()
        Else
            txtreimbuse.Text = ViewState("LocalCurrency") & 0.0
        End If
        txtexrate.Focus()
        ModalPopupExtender6.Show()
    End Sub

    Private Sub txtexrate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtexrate.TextChanged
        dbcon.objMainCompany = Session("SAPCompany")
        If txttrasamt.Text <> "" Then
            dblcur = txttrasamt.Text.Trim()
        Else
            dblcur = 0.0
        End If
        If txtexrate.Text <> "" Then
            dblexrate = txtexrate.Text.Trim()
        Else
            dblexrate = 0.0
        End If
        If ViewState("LocalCurrency").ToUpper <> ddltranscur.SelectedValue.ToUpper() Then
            If dbcon.objMainCompany.GetCompanyService.GetAdminInfo.DirectIndirectRate = SAPbobsCOM.BoYesNoEnum.tNO Then
                If txttrasamt.Text = "" Then
                    txttrasamt.Text = 0.0
                End If
                If dblexrate > 0 Then
                    dblusd = dbcon.getDocumentQuantity(txttrasamt.Text.Trim(), dbcon.objMainCompany) / dblexrate  '
                Else
                    dblusd = 0 ' getDocumentQuantity(fields(9).Trim) / dblExrate  '
                End If
            Else
                dblusd = dblexrate * dbcon.getDocumentQuantity(txttrasamt.Text.Trim(), dbcon.objMainCompany) '
            End If
            txtlocamt.Text = ViewState("LocalCurrency") & Math.Round(dblusd, 2)
        Else
            dblusd = CDbl(txttrasamt.Text.Trim())
            txtlocamt.Text = ViewState("LocalCurrency") & Math.Round(dblusd, 2)
        End If
        If ddlreimbused.SelectedValue = "Y" Then
            txtreimbuse.Text = txtlocamt.Text.Trim()
        Else
            txtreimbuse.Text = ViewState("LocalCurrency") & 0.0
        End If
        ddlreimbused.Focus()
        ModalPopupExtender6.Show()
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        objEN.SessionID = Session("SessionId").ToString()
        objEN.EmpId = ViewState("EmpId")
        dbcon.strmsg = objBL.DeleteTempTable(objEN)
        PageLoadBind(objEN)
        Clear()
        dbcon.objMainCompany = Session("SAPCompany")
        oRecSet = dbcon.objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        oRecSet.DoQuery("Update ""@Z_HR_EXPCL"" set ""Name""=""Code"" where U_Z_DocRefNo='" & lbldocno.Text.Trim() & "' and ""Name"" Like '%D'")
        panelview.Visible = True
        PanelNewRequest.Visible = False
    End Sub

    Private Sub btncancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncancel.Click
        Clear()
        ModalPopupExtender6.Show()
    End Sub
    Public Function Validation() As Boolean
        Try
            If txtexptype.Text = "" Then
                dbcon.strmsg = "Expenses Type is missing..."
                mess(dbcon.strmsg)
                Return False
            ElseIf txttrasdt.Text = "" Then
                dbcon.strmsg = "Transaction date is missing..."
                mess(dbcon.strmsg)
                Return False
            ElseIf ddltranscur.SelectedIndex = 0 Then
                dbcon.strmsg = "Transaction Currency is missing..."
                mess(dbcon.strmsg)
                Return False
            ElseIf txttrasamt.Text = "" Or txttrasamt.Text = "0.0" Then
                dbcon.strmsg = "Transaction Amount is missing..."
                mess(dbcon.strmsg)
                Return False
            End If
            Return True
        Catch ex As Exception
            dbcon.strmsg = ex.Message
            mess(dbcon.strmsg)
            Return False
        End Try
    End Function
    Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            If Session("UserCode") Is Nothing Or Session("SAPCompany") Is Nothing Then
                Response.Redirect("Login.aspx?sessionExpired=true", True)
            Else
                If Validation() = True Then
                    dbcon.strmsg = SaveTempTable()
                    If dbcon.strmsg = "Success" Then
                        objEN.EmpId = ViewState("EmpId")
                        objEN.SessionID = Session("SessionId").ToString()
                        objEN.DocEntry = lbldocno.Text.Trim()
                        BindNewRequest(objEN)
                        Clear()
                        ModalPopupExtender6.Hide()
                    Else
                        mess(dbcon.strmsg)
                        ModalPopupExtender6.Show()
                    End If
                Else
                    ModalPopupExtender6.Show()
                End If
            End If
            txtClient.Text = ViewState("Client").ToString()
            txtProject.Text = ViewState("Project").ToString()
            txttravelCode.Text = ViewState("TraCode").ToString()
            txttraveldesc.Text = ViewState("TraDesc").ToString()
            ddltriptype.SelectedValue = ViewState("TrpType").ToString()
        Catch ex As Exception
            dbcon.strmsg = ex.Message
            mess(dbcon.strmsg)
        End Try
    End Sub
    Private Sub BindNewRequest(ByVal objEN As ClaimRequestEN)
        Try
            dbcon.ds4 = objBL.NewRequestBind(objEN)
            If dbcon.ds4.Tables(0).Rows.Count > 0 Then
                grdRequestExpenses.DataSource = dbcon.ds4.Tables(0)
                grdRequestExpenses.DataBind()
            Else
                grdRequestExpenses.DataBind()
            End If
            If dbcon.ds4.Tables(1).Rows.Count > 0 Then
                grdApproved.DataSource = dbcon.ds4.Tables(1)
                grdApproved.DataBind()
            Else
                grdApproved.DataBind()
            End If
            If dbcon.ds4.Tables(2).Rows.Count > 0 Then
                grdRejected.DataSource = dbcon.ds4.Tables(2)
                grdRejected.DataBind()
            Else
                grdRejected.DataBind()
            End If
        Catch ex As Exception
            dbcon.strmsg = ex.Message
            mess(dbcon.strmsg)
        End Try
    End Sub
    Private Function SaveTempTable() As String
        Try
            ViewState("TraCode") = txttravelCode.Text.Trim()
            ViewState("TraDesc") = txttraveldesc.Text.Trim()
            ViewState("Client") = txtClient.Text.Trim()
            ViewState("Project") = txtProject.Text.Trim()
            ViewState("TrpType") = ddltriptype.SelectedValue
            If txtreqdate.Text <> "" Then
                Submitdate = Now.Date ' Date.ParseExact(txtreqdate.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture)
            End If
            If txttrasdt.Text <> "" Then
                TransDate = Date.ParseExact(txttrasdt.Text.Trim().Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture)
            Else
                TransDate = Now.Date
            End If
            If ddlpaymethod.SelectedValue = "---Select---" Then
                strPayment = ""
            Else
                strPayment = ddlpaymethod.SelectedValue
            End If
            If fileattach.HasFile Then
                fileName = fileattach.FileName
                strpath = Server.MapPath("~\Document\")
                If fileName <> "" Then
                    Targetpath = objBL.TargetPath()
                    fileattach.SaveAs(strpath + fileattach.FileName)
                    If Targetpath <> "" Then
                        Try
                            fileattach.SaveAs(Targetpath + fileattach.FileName)
                        Catch ex As Exception
                        End Try
                    End If
                    strAttachment = strpath + fileattach.FileName
                Else
                    strAttachment = ""
                End If
            Else
                strAttachment = ""
            End If
            lblCardCode.Text = objBL.GetCardCode(lblempNo.Text.Trim())
            dbcon.objMainCompany = Session("SAPCompany")
            oRecSet = dbcon.objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            dbcon.strQuery = "Insert Into [U_EXPCLAIM] (U_SessionId,U_TANo,U_Empid,U_Empname,U_SubDate,U_Client,U_Project,U_ClimDate,U_TripType,U_TraCode,"
            dbcon.strQuery += " U_TraDesc,U_City,U_Currency,U_CurAmt,U_ExcRate,U_UsdAmt,U_ReImbused,U_ReImAmt,U_ExpCode,U_ExpName,U_AllCode,U_PayMethod,"
            dbcon.strQuery += " U_Notes,U_AppStatus,U_Attachment,U_Year,U_Month,U_DocRefNo,U_DebitCode,U_CreditCode,U_Posting,U_CardCode,U_Dimension) Values ('" & Session("SessionId").ToString() & "',"
            dbcon.strQuery += " '" & lblTANo.Text.Trim() & "','" & lblempNo.Text.Trim() & "','" & lblempname.Text.Trim() & "',getdate(), "
            dbcon.strQuery += " '" & txtClient.Text.Trim() & "','" & txtProject.Text.Trim() & "','" & TransDate.ToString("yyyy-MM-dd") & "','" & ddltriptype.SelectedValue & "', "
            dbcon.strQuery += " '" & txttravelCode.Text.Trim() & "','" & txttraveldesc.Text.Trim() & "','" & txtcity.Text.Trim() & "','" & ddltranscur.SelectedValue & "',"
            dbcon.strQuery += " '" & txttrasamt.Text.Trim() & "','" & txtexrate.Text.Trim() & "','" & txtlocamt.Text.Trim() & "','" & ddlreimbused.SelectedValue & "', "
            dbcon.strQuery += " '" & txtreimbuse.Text.Trim() & "','" & txtexpcode.Text.Trim() & "','" & txtexptype.Text.Trim() & "','" & txtAllowance.Text.Trim() & "',"
            dbcon.strQuery += " '" & ddlpaymethod.SelectedValue & "','" & txtnotes.Text.Trim() & "','" & ddlStatus.SelectedValue & "','" & strAttachment.Trim() & "', "
            dbcon.strQuery += " " & TransDate.Year & "," & TransDate.Month & ",'" & txtreqcode.Text.Trim() & "','" & txtdebit.Text.Trim() & "','" & txtCredit.Text.Trim() & "','" & txtposting.Text.Trim() & "','" & lblCardCode.Text.Trim() & "','" & txtDisRule.Text.Trim() & "') "
            oRecSet.DoQuery(dbcon.strQuery)
            dbcon.strmsg = "Success"
        Catch ex As Exception
            dbcon.strmsg = ex.Message
        End Try
        Return dbcon.strmsg
    End Function
    Protected Sub lbtndocnum_Click(ByVal sender As Object, ByVal e As EventArgs)
        If Session("UserCode") Is Nothing Or Session("SAPCompany") Is Nothing Then
            Response.Redirect("Login.aspx?sessionExpired=true", True)
        Else
            Clear()
            lblempNo.Text = Session("UserCode").ToString()
            BindDistriRule(lblempNo.Text.Trim())
            lblempname.Text = Session("UserName").ToString()
            lblTANo.Text = objBL.PopulateTANo(Session("UserCode").ToString())
            Dim link As LinkButton = CType(sender, LinkButton)
            Dim gv As GridViewRow = CType((link.Parent.Parent), GridViewRow)
            Dim DocNo As LinkButton = CType(gv.FindControl("lbtndocnum"), LinkButton)
            panelview.Visible = False
            PanelNewRequest.Visible = True
            lbldocno.Text = DocNo.Text.Trim()
            objEN.DocEntry = DocNo.Text.Trim()
            objEN.EmpId = Session("UserCode").ToString()
            objEN.SessionID = Session("SessionId").ToString()
            PopulateHeader(objEN)
            dbcon.strmsg = objBL.PopulateExistingDocument(objEN)
            If dbcon.strmsg = "Success" Then
            Else
                mess(dbcon.strmsg)
            End If
            objEN.EmpId = lblempNo.Text.Trim()
            objEN.SessionID = Session("SessionId").ToString()
            objEN.DocEntry = lbldocno.Text.Trim()
            BindNewRequest(objEN)
        End If
    End Sub
    Private Sub PopulateHeader(ByVal objEN As ClaimRequestEN)
        Try
            dbcon.dss3 = objBL.PopulateHeader(objEN)
            If dbcon.dss3.Tables(0).Rows.Count > 0 Then
                lblsubdt.Text = dbcon.dss3.Tables(0).Rows(0)("U_Z_Subdt").ToString()
                txtClient.Text = dbcon.dss3.Tables(0).Rows(0)("U_Z_Client").ToString()
                txtProject.Text = dbcon.dss3.Tables(0).Rows(0)("U_Z_Project").ToString()
                ViewState("Client") = dbcon.dss3.Tables(0).Rows(0)("U_Z_Client").ToString()
                ViewState("Project") = dbcon.dss3.Tables(0).Rows(0)("U_Z_Project").ToString()
                ddltriptype.SelectedValue = dbcon.dss3.Tables(0).Rows(0)("U_Z_TripType").ToString()
                ddlDocStatus.SelectedValue = dbcon.dss3.Tables(0).Rows(0)("U_Z_DocStatus").ToString()
                txttravelCode.Text = dbcon.dss3.Tables(0).Rows(0)("U_Z_TraCode").ToString()
                txttraveldesc.Text = dbcon.dss3.Tables(0).Rows(0)("U_Z_TraDesc").ToString()
                lblCardCode.Text = dbcon.dss3.Tables(0).Rows(0)("U_Z_CardCode").ToString()
                ViewState("TraCode") = dbcon.dss3.Tables(0).Rows(0)("U_Z_TraCode").ToString()
                ViewState("TraDesc") = dbcon.dss3.Tables(0).Rows(0)("U_Z_TraDesc").ToString()
                If dbcon.dss3.Tables(0).Rows(0)("U_Z_DocStatus").ToString() = "C" Then
                    NewExpense.Visible = False
                    btnSubmit.Visible = False
                Else
                    NewExpense.Visible = True
                    btnSubmit.Visible = True
                End If
                If ddltriptype.SelectedValue = "N" Then
                    btnfindtrcode.Visible = False
                Else
                    btnfindtrcode.Visible = True
                End If
            End If
        Catch ex As Exception
            dbcon.strmsg = ex.Message
            mess(dbcon.strmsg)
        End Try
    End Sub

    Public Function Validation1() As Boolean
        Try

            If ddltriptype.SelectedIndex <> 0 Then
                If ddltriptype.SelectedValue = "E" Then
                    If txttraveldesc.Text = "" Or txttravelCode.Text = "" Then
                        dbcon.strmsg = "Travel Code is missing..."
                        mess(dbcon.strmsg)
                        Return False
                    End If
                End If
            Else
                dbcon.strmsg = "Trip Type is missing..."
                mess(dbcon.strmsg)
                Return False
            End If
            Return True
        Catch ex As Exception
            dbcon.strmsg = ex.Message
            mess(dbcon.strmsg)
            Return False
        End Try
    End Function
    Private Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        If Session("UserCode") Is Nothing Or Session("SAPCompany") Is Nothing Then
            Response.Redirect("Login.aspx?sessionExpired=true", True)
        ElseIf Validation1() = True Then
            objEN.EmpId = Session("UserCode").ToString()
            objEN.SessionID = Session("SessionId").ToString()
            dbcon.strmsg = SaveUpdateClaim(objEN)
            If dbcon.strmsg = "Success" Then
                dbcon.strmsg = "Expenses Claim Saved Successfully..."
                mess(dbcon.strmsg)
                panelview.Visible = True
                PanelNewRequest.Visible = False
                objEN.SessionID = Session("SessionId").ToString()
                objEN.EmpId = ViewState("EmpId")
                dbcon.strmsg = objBL.DeleteTempTable(objEN)
                PageLoadBind(objEN)
            Else
                mess(dbcon.strmsg)
                panelview.Visible = False
                PanelNewRequest.Visible = True
            End If
        End If
    End Sub
    Private Function SaveUpdateClaim(ByVal objEN As ClaimRequestEN) As String
        Try
            dbcon.objMainCompany = Session("SAPCompany")
            oRecSet = dbcon.objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oTemp = dbcon.objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            Dim oUserTable2, oUserTable1 As SAPbobsCOM.UserTable
            Dim strClim, stSubdt, strAppStatus As String
            Dim dtClaim, dtsubdt As Date
            oUserTable1 = dbcon.objMainCompany.UserTables.Item("Z_HR_OEXPCL")
            objEN.DocEntry = lbldocno.Text.Trim()
            If oUserTable1.GetByKey(objEN.DocEntry) Then
                oUserTable1.Code = objEN.DocEntry
                oUserTable1.Name = objEN.DocEntry
                oUserTable1.UserFields.Fields.Item("U_Z_Client").Value = txtClient.Text.Trim()
                oUserTable1.UserFields.Fields.Item("U_Z_Project").Value = txtProject.Text.Trim()
                oUserTable1.UserFields.Fields.Item("U_Z_TraCode").Value = txttravelCode.Text.Trim()
                oUserTable1.UserFields.Fields.Item("U_Z_TraDesc").Value = txttraveldesc.Text.Trim()
                oUserTable1.UserFields.Fields.Item("U_Z_TripType").Value = ddltriptype.SelectedValue
                If oUserTable1.Update <> 0 Then
                    dbcon.strmsg = dbcon.objMainCompany.GetLastErrorDescription
                End If
            Else
                objEN.DocEntry = dbcon.Getmaxcode("[@Z_HR_OEXPCL]", "Code")
                oUserTable1.Code = objEN.DocEntry
                oUserTable1.Name = objEN.DocEntry
                oUserTable1.UserFields.Fields.Item("U_Z_EmpID").Value = lblempNo.Text.Trim()
                oUserTable1.UserFields.Fields.Item("U_Z_EmpName").Value = lblempname.Text.Trim()
                oUserTable1.UserFields.Fields.Item("U_Z_Subdt").Value = Now.Date
                oUserTable1.UserFields.Fields.Item("U_Z_TAEmpID").Value = lblTANo.Text.Trim()
                oUserTable1.UserFields.Fields.Item("U_Z_Client").Value = txtClient.Text.Trim()
                oUserTable1.UserFields.Fields.Item("U_Z_Project").Value = txtProject.Text.Trim()
                oUserTable1.UserFields.Fields.Item("U_Z_TraCode").Value = txttravelCode.Text.Trim()
                oUserTable1.UserFields.Fields.Item("U_Z_TraDesc").Value = txttraveldesc.Text.Trim()
                oUserTable1.UserFields.Fields.Item("U_Z_TripType").Value = ddltriptype.SelectedValue
                oUserTable1.UserFields.Fields.Item("U_Z_DocStatus").Value = "O"
                oUserTable1.UserFields.Fields.Item("U_Z_CardCode").Value = lblCardCode.Text.Trim()
                If oUserTable1.Add <> 0 Then
                    dbcon.strmsg = dbcon.objMainCompany.GetLastErrorDescription
                End If
            End If
            strAppStatus = dbcon.DocApproval("ExpCli", lblempNo.Text.Trim())
            dbcon.strQuery = "Select * from ""U_EXPCLAIM""   where ""U_SessionId""='" & objEN.SessionID & "' AND ""U_Empid""='" & objEN.EmpId & "'"
            oRecSet.DoQuery(dbcon.strQuery)
            If oRecSet.RecordCount > 0 Then
                stSubdt = oRecSet.Fields.Item("U_SubDate").Value
                If stSubdt <> "" Then
                    dtsubdt = Date.ParseExact(stSubdt.Trim().Replace(".", "/").Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture)
                Else
                    dtsubdt = Now.Date
                End If
                oUserTable2 = dbcon.objMainCompany.UserTables.Item("Z_HR_EXPCL")
                For introw As Integer = 0 To oRecSet.RecordCount - 1
                    strClim = oRecSet.Fields.Item("U_ClimDate").Value
                    If strClim <> "" Then
                        dtClaim = Date.ParseExact(strClim.Trim().Replace(".", "/").Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture)
                    Else
                        dtClaim = Now.Date
                    End If
                    LineDocEntry = oRecSet.Fields.Item("U_Code").Value
                    If LineDocEntry = "" Then
                        LineDocEntry = dbcon.Getmaxcode("[@Z_HR_EXPCL]", "Code")
                        dbcon.strQuery = "Insert INTO [@Z_HR_EXPCL] (Code,Name,U_Z_EmpID,U_Z_EmpName,U_Z_TraCode,U_Z_TraDesc,U_Z_TripType,U_Z_Subdt,U_Z_Claimdt,U_Z_Reimburse,"
                        dbcon.strQuery += "U_Z_PayMethod, U_Z_ExpType, U_Z_ExpCode, U_Z_Currency, U_Z_Client, U_Z_AlloCode,U_Z_Project,U_Z_City,U_Z_CurAmt,U_Z_ExcRate,U_Z_UsdAmt, "
                        dbcon.strQuery += "U_Z_ReimAmt,U_Z_Notes,U_Z_Attachment,U_Z_Year,U_Z_Month,U_Z_AppStatus,U_Z_DocRefNo,U_Z_CardCode,U_Z_DebitCode,U_Z_CreditCode,U_Z_Posting"
                        dbcon.strQuery += ",U_Z_Dimension) Values ("
                        dbcon.strQuery += " '" & LineDocEntry & "','" & LineDocEntry & "','" & lblempNo.Text.Trim() & "','" & lblempname.Text.Trim() & "','" & txttravelCode.Text.Trim() & "','" & txttraveldesc.Text.Trim() & "',"
                        dbcon.strQuery += " '" & ddltriptype.SelectedValue & "','" & dtsubdt.ToString("yyyy-MM-dd") & "','" & dtClaim.ToString("yyyy-MM-dd") & "','" & oRecSet.Fields.Item("U_ReImbused").Value & "',"
                        dbcon.strQuery += "  '" & oRecSet.Fields.Item("U_PayMethod").Value & "','" & oRecSet.Fields.Item("U_ExpName").Value & "','" & oRecSet.Fields.Item("U_ExpCode").Value & "','" & oRecSet.Fields.Item("U_Currency").Value & "',"
                        dbcon.strQuery += "  '" & txtClient.Text.Trim() & "','" & oRecSet.Fields.Item("U_AllCode").Value & "','" & txtProject.Text.Trim() & "','" & oRecSet.Fields.Item("U_City").Value & "',"
                        dbcon.strQuery += " " & oRecSet.Fields.Item("U_CurAmt").Value & "," & oRecSet.Fields.Item("U_ExcRate").Value & ",'" & oRecSet.Fields.Item("U_UsdAmt").Value & "','" & oRecSet.Fields.Item("U_ReImAmt").Value & "',"
                        dbcon.strQuery += " '" & oRecSet.Fields.Item("U_Notes").Value & "','" & oRecSet.Fields.Item("U_Attachment").Value & "','" & oRecSet.Fields.Item("U_Year").Value & "','" & oRecSet.Fields.Item("U_Month").Value & "',"
                        dbcon.strQuery += " '" & strAppStatus & "','" & objEN.DocEntry & "','" & lblCardCode.Text.Trim() & "','" & oRecSet.Fields.Item("U_DebitCode").Value & "',"
                        dbcon.strQuery += " '" & oRecSet.Fields.Item("U_CreditCode").Value & "','" & oRecSet.Fields.Item("U_Posting").Value & "','" & oRecSet.Fields.Item("U_Dimension").Value & "')"
                        oTemp.DoQuery(dbcon.strQuery)
                        If strMailCode = "" Then
                            strMailCode = Integer.Parse(LineDocEntry)
                        Else
                            strMailCode = strMailCode & "," & Integer.Parse(LineDocEntry)
                        End If
                        objDA.AddtoUDT1_PayrollTrans(LineDocEntry, dbcon.objMainCompany)
                        If oRecSet.Fields.Item("U_Posting").Value = "G" Then
                            objDA.CreateJournelVouchers(LineDocEntry, oRecSet.Fields.Item("U_ReImbused").Value, dbcon.objMainCompany)
                        End If
                        intTempID = dbcon.GetTemplateID("ExpCli", lblempNo.Text.Trim())
                        If intTempID <> "0" Then
                            dbcon.UpdateApprovalRequired("@Z_HR_EXPCL", "Code", LineDocEntry, "Y", intTempID)
                        Else
                            dbcon.UpdateApprovalRequired("@Z_HR_EXPCL", "Code", LineDocEntry, "N", intTempID)
                        End If
                    End If
                    dbcon.strmsg = "Success"
                    oRecSet.MoveNext()
                Next
            End If
            dbcon.strQuery = "Delete from  ""@Z_HR_EXPCL""  where ""U_Z_DocRefNo""='" & objEN.DocEntry & "' and ""Name"" Like '%D'"
            oRecSet.DoQuery(dbcon.strQuery)
            If strMailCode <> "" Then
                dbcon.InitialMessage("Expenses Claim", objEN.DocEntry, dbcon.DocApproval("ExpCli", lblempNo.Text.Trim()), intTempID, lblempname.Text.Trim(), "ExpCli", dbcon.objMainCompany, strMailCode)
            End If
            dbcon.strmsg = "Success"
        Catch ex As Exception
            dbcon.strmsg = ex.Message
        End Try
        Return dbcon.strmsg
    End Function

    Private Sub grdRequestExpenses_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdRequestExpenses.PageIndexChanging
        grdRequestExpenses.PageIndex = e.NewPageIndex
        objEN.EmpId = lblempNo.Text.Trim()
        objEN.SessionID = Session("SessionId").ToString()
        objEN.DocEntry = lbldocno.Text.Trim()
        BindNewRequest(objEN)
    End Sub

    Private Sub grdRequestExpenses_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdRequestExpenses.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            objEN.SapCompany = Session("SAPCompany")
            Dim Liremove As LinkButton = CType(e.Row.FindControl("lnkEDownload"), LinkButton)
            Dim lblTransAmt As Label = DirectCast(e.Row.FindControl("lblEtramt"), Label)
            Dim lblextRate As Label = DirectCast(e.Row.FindControl("lblEexrate"), Label)
            Dim lbltranscur As Label = DirectCast(e.Row.FindControl("lblETraCur"), Label)
            objEN.AllowanceCode = e.Row.DataItem("U_Attachment")
            If objEN.AllowanceCode = "" Then
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
                        dblusd = dbcon.getDocumentQuantity(lblTransAmt.Text.Trim(), objEN.SapCompany) / CDbl(lblextRate.Text.Trim)
                    Else
                        dblusd = 0 ' getDocumentQuantity(fields(9).Trim) / dblExrate  '
                    End If
                Else
                    dblusd = CDbl(lblextRate.Text.Trim) * dbcon.getDocumentQuantity(lblTransAmt.Text.Trim(), objEN.SapCompany) '
                End If
            Else
                dblusd = CDbl(lblTransAmt.Text.Trim())
            End If
            grdLocTotal = grdLocTotal + dblusd
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim strStatus As String = ddlDocStatus.SelectedValue
            If strStatus = "O" Then
                e.Row.Cells(0).Visible = True
            Else
                e.Row.Cells(0).Text = ""
            End If
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim item As String = CType(e.Row.FindControl("lblEexptype"), Label).Text
            For Each button As LinkButton In e.Row.Cells(0).Controls.OfType(Of LinkButton)()
                If button.CommandName = "Delete" Then
                    button.Attributes("onclick") = "if(!confirm('Do you want to delete " + item + "?')){ return false; };"
                End If
            Next
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            Dim lbl As Label = CType(e.Row.FindControl("lblCurTotal"), Label)
            lbl.Text = grdTotal1.ToString()
            Dim lbl2 As Label = CType(e.Row.FindControl("lblLocCurTotal"), Label)
            lbl2.Text = ViewState("LocalCurrency") & Math.Round(grdLocTotal, 2)
        End If
    End Sub

    Private Sub grdRequestExpenses_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdRequestExpenses.RowDeleting
        Try
            objEN.DocEntry = DirectCast(grdRequestExpenses.Rows(e.RowIndex).FindControl("lblECode"), Label).Text
            objEN.TravelCode = DirectCast(grdRequestExpenses.Rows(e.RowIndex).FindControl("lblERefCode"), Label).Text
            Blflag = dbcon.WithDrawStatus("ExpCli", objEN.TravelCode)
            If Blflag = False Then
                objEN.EmpId = ViewState("EmpId").ToString()
                objEN.ReqNo = objBL.DeleteExpenses(objEN)
                If objEN.ReqNo = "Success" Then
                    dbcon.strmsg = "Expenses Deleted successfully..."
                    mess(dbcon.strmsg)
                    lblerror.Text = ""
                Else
                    dbcon.strmsg = objEN.ReqNo
                    mess(dbcon.strmsg)
                End If
            Else
                lblerror.Text = "Already in approval cycle.You can't delete this expense."
            End If
            objEN.EmpId = lblempNo.Text.Trim()
            objEN.SessionID = Session("SessionId").ToString()
            objEN.DocEntry = lbldocno.Text.Trim()
            BindNewRequest(objEN)
        Catch ex As Exception
            dbcon.strmsg = ex.Message
            mess(dbcon.strmsg)
        End Try
    End Sub
    Protected Sub lnkEDownload_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim filePath As String = TryCast(sender, LinkButton).CommandArgument
        Dim filename As String = Path.GetFileName(filePath)
        If filename <> "" Then
            Dim path As String = System.IO.Path.Combine(Server.MapPath("~\Document\"), filename) 'System.IO.Path.Combine(Server.MapPath("~\Document\"), filename)
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
                dbcon.strmsg = "File is not available"
                ClientScript.RegisterStartupScript(Me.GetType(), "msg", "<script>alert('" & dbcon.strmsg & "')</script>")
            End If
        End If
    End Sub

    Private Sub grdApproved_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdApproved.PageIndexChanging
        grdApproved.PageIndex = e.NewPageIndex
        objEN.EmpId = lblempNo.Text.Trim()
        objEN.SessionID = Session("SessionId").ToString()
        objEN.DocEntry = lbldocno.Text.Trim()
        BindNewRequest(objEN)
    End Sub

    Private Sub grdApproved_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdApproved.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            objEN.SapCompany = Session("SAPCompany")
            Dim LiDocNo As Label = CType(e.Row.FindControl("lblCode"), Label)
            Dim Liview As LinkButton = CType(e.Row.FindControl("lbtAppHistory"), LinkButton)
            Dim lblTransAmt As Label = DirectCast(e.Row.FindControl("lbltramt"), Label)
            Dim lblextRate As Label = DirectCast(e.Row.FindControl("lblexrate"), Label)
            Dim lbltranscur As Label = DirectCast(e.Row.FindControl("lblTraCur"), Label)
            Blflag = dbcon.WithDrawStatus("ExpCli", LiDocNo.Text.Trim())
            If Blflag = True Then
                Liview.Visible = True
            Else
                Liview.Visible = False
            End If

            Dim rowtotal As Decimal = Convert.ToDecimal(lblTransAmt.Text.Trim())
            grdTotal = grdTotal + rowtotal

            If ViewState("LocalCurrency").ToUpper <> lbltranscur.Text.ToUpper() Then
                If objEN.SapCompany.GetCompanyService.GetAdminInfo.DirectIndirectRate = SAPbobsCOM.BoYesNoEnum.tNO Then
                    If lblTransAmt.Text.Trim() = "" Then
                        lblTransAmt.Text = 0.0
                    End If
                    If CDbl(lblextRate.Text.Trim) > 0 Then
                        dblusd = dbcon.getDocumentQuantity(lblTransAmt.Text.Trim(), objEN.SapCompany) / CDbl(lblextRate.Text.Trim)
                    Else
                        dblusd = 0 ' getDocumentQuantity(fields(9).Trim) / dblExrate  '
                    End If
                Else
                    dblusd = CDbl(lblextRate.Text.Trim) * dbcon.getDocumentQuantity(lblTransAmt.Text.Trim(), objEN.SapCompany) '
                End If
            Else
                dblusd = CDbl(lblTransAmt.Text.Trim())
            End If
            grdLocTotal1 = grdLocTotal1 + dblusd
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            Dim lbl As Label = CType(e.Row.FindControl("lblACurTotal"), Label)
            lbl.Text = grdTotal.ToString()
            Dim lbl2 As Label = CType(e.Row.FindControl("lblALocCurTotal"), Label)
            lbl2.Text = ViewState("LocalCurrency") & Math.Round(grdLocTotal1, 2)
        End If
    End Sub

    Private Sub grdRejected_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdRejected.PageIndexChanging
        grdRejected.PageIndex = e.NewPageIndex
        objEN.EmpId = lblempNo.Text.Trim()
        objEN.SessionID = Session("SessionId").ToString()
        objEN.DocEntry = lbldocno.Text.Trim()
        BindNewRequest(objEN)
    End Sub

    Private Sub grdRejected_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdRejected.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            objEN.SapCompany = Session("SAPCompany")
            Dim LiDocNo As Label = CType(e.Row.FindControl("lblRCode"), Label)
            Dim Liview As LinkButton = CType(e.Row.FindControl("lbtRAppHistory"), LinkButton)
            Dim lblTransAmt As Label = DirectCast(e.Row.FindControl("lblRtramt"), Label)
            Dim lblextRate As Label = DirectCast(e.Row.FindControl("lblRexrate"), Label)
            Dim lbltranscur As Label = DirectCast(e.Row.FindControl("lblRTraCur"), Label)
            Blflag = dbcon.WithDrawStatus("ExpCli", LiDocNo.Text.Trim())
            If Blflag = True Then
                Liview.Visible = True
            Else
                Liview.Visible = False
            End If

            Dim rowtotal As Decimal = Convert.ToDecimal(lblTransAmt.Text.Trim())
            grdTotal2 = grdTotal2 + rowtotal

            If ViewState("LocalCurrency").ToUpper <> lbltranscur.Text.ToUpper() Then
                If objEN.SapCompany.GetCompanyService.GetAdminInfo.DirectIndirectRate = SAPbobsCOM.BoYesNoEnum.tNO Then
                    If lblTransAmt.Text.Trim() = "" Then
                        lblTransAmt.Text = 0.0
                    End If
                    If CDbl(lblextRate.Text.Trim) > 0 Then
                        dblusd = dbcon.getDocumentQuantity(lblTransAmt.Text.Trim(), objEN.SapCompany) / CDbl(lblextRate.Text.Trim)
                    Else
                        dblusd = 0 ' getDocumentQuantity(fields(9).Trim) / dblExrate  '
                    End If
                Else
                    dblusd = CDbl(lblextRate.Text.Trim) * dbcon.getDocumentQuantity(lblTransAmt.Text.Trim(), objEN.SapCompany) '
                End If
            Else
                dblusd = CDbl(lblTransAmt.Text.Trim())
            End If
            grdLocTotal2 = grdLocTotal2 + dblusd
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            Dim lbl As Label = CType(e.Row.FindControl("lblRCurTotal"), Label)
            lbl.Text = grdTotal2.ToString()
            Dim lbl2 As Label = CType(e.Row.FindControl("lblRLocCurTotal"), Label)
            lbl2.Text = ViewState("LocalCurrency") & Math.Round(grdLocTotal2, 2)
        End If
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
                Dim DocNo As Label = CType(gv.FindControl("lblCode"), Label)
                LoadHistory(DocNo.Text.Trim(), "ExpCli")
                ModalPopupExtender2.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub LoadHistory(ByVal RefCode As String, ByVal DocType As String)
        Try
            dbcon.ds4 = dbcon.ViewHistory(RefCode, DocType)
            If dbcon.ds4.Tables(0).Rows.Count > 0 Then
                grdRequesttohr.DataSource = dbcon.ds4.Tables(0)
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
    Protected Sub lbtRAppHistory_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If Session("UserCode") Is Nothing Or Session("SAPCompany") Is Nothing Then
                dbcon.strmsg = "alert('Your session is Expired...')"
                mess(dbcon.strmsg)
                Response.Redirect("Login.aspx?sessionExpired=true", True)
            Else
                Dim link As LinkButton = CType(sender, LinkButton)
                Dim gv As GridViewRow = CType((link.Parent.Parent), GridViewRow)
                Dim DocNo As Label = CType(gv.FindControl("lblRCode"), Label)
                LoadHistory(DocNo.Text.Trim(), "ExpCli")
                ModalPopupExtender2.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnSample_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSample.Click
        objEN.EmpId = ViewState("EmpId")
        objEN.SessionID = Session("SessionId").ToString()
        objEN.DocEntry = lbldocno.Text.Trim()
        BindNewRequest(objEN)
    End Sub

    Private Sub ddltranscur_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddltranscur.SelectedIndexChanged
        dbcon.objMainCompany = Session("SAPCompany")
        If ViewState("LocalCurrency").ToUpper <> ddltranscur.SelectedValue.ToUpper() Then
            If txttrasdt.Text <> "" Then
                TransDate = Date.ParseExact(txttrasdt.Text.Trim().Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture)
            Else
                TransDate = Now.Date
            End If
            dblexrate = objBL.GetExcRate(ddltranscur.SelectedValue.ToUpper(), TransDate)
            txtexrate.Text = dblexrate
        Else
            dblexrate = 1.0
            txtexrate.Text = dblexrate
        End If

        If ViewState("LocalCurrency").ToUpper <> ddltranscur.SelectedValue.ToUpper() Then
            If dbcon.objMainCompany.GetCompanyService.GetAdminInfo.DirectIndirectRate = SAPbobsCOM.BoYesNoEnum.tNO Then
                If txttrasamt.Text = "" Then
                    txttrasamt.Text = 0.0
                End If
                If dblexrate > 0 Then
                    dblusd = dbcon.getDocumentQuantity(txttrasamt.Text.Trim(), dbcon.objMainCompany) / dblexrate  '
                Else
                    dblusd = 0 ' getDocumentQuantity(fields(9).Trim) / dblExrate  '
                End If
            Else
                dblusd = dblexrate * dbcon.getDocumentQuantity(txttrasamt.Text.Trim(), dbcon.objMainCompany) '
            End If
            txtlocamt.Text = ViewState("LocalCurrency") & Math.Round(dblusd, 6)
        Else
            dblusd = CDbl(txttrasamt.Text.Trim())
            txtlocamt.Text = ViewState("LocalCurrency") & Math.Round(dblusd, 6)
        End If
        If ddlreimbused.SelectedValue = "Y" Then
            txtreimbuse.Text = txtlocamt.Text.Trim()
        Else
            txtreimbuse.Text = ViewState("LocalCurrency") & 0.0
        End If
        txttrasamt.Focus()
        ModalPopupExtender6.Show()
    End Sub
    Private Sub btnDisOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDisOK.Click
        Try
            txtDisRule.Text = ddlDis1.SelectedValue & ";" & ddlDis2.SelectedValue & ";" & ddlDis3.SelectedValue & ";" & ddlDis4.SelectedValue & ";" & ddlDis5.SelectedValue
        Catch ex As Exception
            dbcon.strmsg = ex.Message
            mess(dbcon.strmsg)
        End Try
        ModalPopupExtender6.Show()
    End Sub

End Class