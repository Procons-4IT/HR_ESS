Imports System
Imports System.Globalization
Imports BusinessLogic
Imports DataAccess
Imports EN

Public Class ResignRequest
    Inherits System.Web.UI.Page
    Dim objEN As LeaveRequestEN = New LeaveRequestEN()
    Dim objBL As ResignRequestBL = New ResignRequestBL()
    Dim dbCon As DBConnectionDA = New DBConnectionDA()
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
        txtfrmdate.Text = ""
        txtreason.Text = ""
    End Sub
    Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim blValue As Boolean
        If Session("UserCode") Is Nothing Then
            Response.Redirect("Login.aspx?sessionExpired=true", True)
        Else
            objEN.Empid = Session("UserCode").ToString()
            objEN.EmpName = Session("UserName").ToString()
            objEN.StartDate = txtfrmdate.Text.Trim().Replace("-", "/")
            objEN.Notes = txtreason.Text.Trim()
            objEN.strCode = txtcode.Text.Trim()
            If objEN.StartDate <> "" Then
                objEN.FromDate = Date.ParseExact(objEN.StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)
            End If
          If objEN.StartDate = "" Then
                dbCon.strmsg = "alert('From date is missing...')"
                mess(dbCon.strmsg)
            ElseIf objEN.Notes = "" Then
                dbCon.strmsg = "alert('Resignation reason missing...')"
                mess(dbCon.strmsg)
            Else
                 If txtcode.Text = "" Then
                    blValue = objBL.SaveLeaveRequest(objEN)
                Else
                    blValue = objBL.UpdateLeaveRequest(objEN)
                End If
                If blValue = True Then
                    dbCon.strmsg = "alert('Resignation Request saved Successfully...')"
                    mess(dbCon.strmsg)
                Else
                    dbCon.strmsg = "alert('Resignation Request failed...')"
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
        Try
            dbCon.ds1 = objBL.PopulatePermissionRequest(objen)
            If dbCon.ds1.Tables(0).Rows.Count > 0 Then
                txtcode.Text = dbCon.ds1.Tables(0).Rows(0)("Code").ToString()
                txtfrmdate.Text = dbCon.ds1.Tables(0).Rows(0)("U_Z_StartDate").ToString()
                txtreason.Text = dbCon.ds1.Tables(0).Rows(0)("U_Z_Notes").ToString()
                If dbCon.ds1.Tables(0).Rows(0)("U_Z_Status").ToString() <> "P" Then
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
            dbCon.strmsg = "alert('Resignation Request Successfully...')"
            mess(dbCon.strmsg)
        Else
            dbCon.strmsg = "alert('Resignation Request failed...')"
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
        txtreason.Text = ""
    End Sub

    Private Sub grdLeaveRequest_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdLeaveRequest.PageIndexChanging
        objEN.Empid = ViewState("EmpId")
        grdLeaveRequest.PageIndex = e.NewPageIndex
        PageLoadBind(objEN)
    End Sub
End Class