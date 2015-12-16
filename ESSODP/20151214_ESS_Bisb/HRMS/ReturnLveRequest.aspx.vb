Imports System
Imports System.Globalization
Imports BusinessLogic
Imports DataAccess
Imports EN
Public Class ReturnLveRequest
    Inherits System.Web.UI.Page
    Dim objEN As LeaveRequestEN = New LeaveRequestEN()
    Dim objBL As ReturnLveRequestBL = New ReturnLveRequestBL()
    Dim dbCon As DBConnectionDA = New DBConnectionDA()
    Dim objCom As CommonFunctions = New CommonFunctions()
    Dim objVal As DynamicApprovalDA = New DynamicApprovalDA()
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
            objEN.Empid = Session("UserCode").ToString()
            ViewState("EmpId") = objEN.Empid
            PageLoadBind(objEN)
            BindLveType(objEN)
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
    Private Sub BindLveType(ByVal objEN As LeaveRequestEN)
        Try
            dbCon.dss1 = objBL.FillLeavetype(objEN)
            If dbCon.dss1.Tables(0).Rows.Count > 0 Then
                ddllvecode.DataSource = dbCon.dss1.Tables(0)
                ddllvecode.DataTextField = "Name"
                ddllvecode.DataValueField = "Code"
                ddllvecode.DataBind()
            Else
                ddllvecode.DataBind()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click

        Dim blValue As Boolean
        If Session("UserCode") Is Nothing Or Session("SAPCompany") Is Nothing Then
            Response.Redirect("Login.aspx?sessionExpired=true", True)
        Else
            objEN.SapCompany = Session("SAPCompany")
            objEN.Empid = Session("UserCode").ToString()
            objEN.EmpName = Session("UserName").ToString()
            objEN.NoofDays = txtnodays.Text.Trim()
            objEN.StartDate = txtfrmdate.Text.Trim().Replace("-", "/")
            objEN.Notes = txtreason.Text.Trim()
            objEN.RejoinDate = txtretrejoin.Text.Trim().Replace("-", "/")
            objEN.strCode = txtcode.Text.Trim()
            objEN.LeaveCode = ddllvecode.SelectedValue
            If objEN.RejoinDate <> "" Then
                objEN.RejoinDt = Date.ParseExact(objEN.RejoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) ' objEN.RejoinDate
            End If
            If objEN.StartDate <> "" Then
                objEN.FromDate = Date.ParseExact(objEN.StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) ' objEN.RejoinDate
            End If
            If objEN.RejoinDate = "" Then
                dbCon.strmsg = "alert('Return Rejoin date is missing...')"
                mess(dbCon.strmsg)
            ElseIf objEN.RejoinDt < objEN.FromDate Then ' objEN.RejoinDate < objEN.StartDate Then
                dbCon.strmsg = "alert('Rejoin date Should be greater than or equal to Start date...')"
                mess(dbCon.strmsg)
            Else
                objEN.Status = dbCon.DocApproval("LveReq", objEN.Empid, objEN.LeaveCode)
                If txtcode.Text <> "" Then
                    blValue = objBL.UpdateLeaveRequest(objEN)
                End If
                If blValue = True Then
                    If objEN.Status = "A" Then
                        objVal.UpdateAddUDTPayroll(objEN.strCode, objEN.SapCompany)
                    End If
                    intTempID = dbCon.GetTemplateID("LveReq", objEN.Empid, objEN.LeaveCode)
                    If intTempID <> "0" Then
                        dbCon.UpdateApprovalRequired("@Z_PAY_OLETRANS1", "Code", objEN.strCode, "Y", intTempID)
                        dbCon.InitialMessage("Return from Leave Request", objEN.strCode, dbCon.DocApproval("LveReq", objEN.Empid, objEN.LeaveCode), intTempID, objEN.EmpName, "LveReq", objEN.SapCompany)
                    Else
                        dbCon.UpdateApprovalRequired("@Z_PAY_OLETRANS1", "Code", objEN.strCode, "N", intTempID)
                    End If
                    dbCon.strmsg = "alert('Return From Leave Request saved Successfully...')"
                    mess(dbCon.strmsg)
                Else
                    dbCon.strmsg = "alert('Return From leave Request failed...')"
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
                txtretrejoin.Text = dbCon.ds1.Tables(0).Rows(0)("U_Z_RetJoiNDate").ToString()
                If txtretrejoin.Text = "" Then
                    If txtrejoin.Text <> "" Then
                        objen.RejoinDt = Date.ParseExact(txtrejoin.Text.Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture) ' objEN.RejoinDate
                        txtretrejoin.Text = objen.RejoinDt.AddDays(1)
                    End If
                End If
                ddllvecode.SelectedValue = dbCon.ds1.Tables(0).Rows(0)("U_Z_TrnsCode").ToString()
                Blflag = dbCon.WithDrawStatus("RetLve", txtcode.Text.Trim())
                If dbCon.ds1.Tables(0).Rows(0)("U_Z_RStatus").ToString() <> "P" Or Blflag = True Then
                    btnAdd.Visible = False
                Else
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
                LoadActivity(DocNo.Text.Trim(), "RetLve")
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
            Blflag = dbCon.WithDrawStatus("RetLve", LiDocNo.Text.Trim())
            If Blflag = True Then
                Liview.Visible = True
            Else
                Liview.Visible = False
            End If
        End If
    End Sub
End Class