Imports System
Imports System.IO
Imports System.Net.Mail
Imports System.Data
Imports System.Data.SqlClient
Imports BusinessLogic
Imports DataAccess
Imports EN
Public Class LineMgrAppraisal
    Inherits System.Web.UI.Page
    Dim objCOM As SelfAppraisalEN = New SelfAppraisalEN()
    Dim objen As LineMgrAppraisalEN = New LineMgrAppraisalEN()
    Dim objBL As LineMgrAppraisalBL = New LineMgrAppraisalBL()
    Dim objDA As LineMgrAppraisalDA = New LineMgrAppraisalDA()
    Dim dbcon As DBConnectionDA = New DBConnectionDA()
    Dim UserEN As ChangePwdEN = New ChangePwdEN()
    Dim grdTotal As Decimal = 0
    Dim grdTotal1 As Decimal = 0
    Dim grdTotal2 As Decimal = 0
    Dim grdTotal3 As Decimal = 0
    Dim grdTotal11 As Decimal = 0
    Dim grdTotal12 As Decimal = 0
    Dim grdTotal13 As Decimal = 0
    Dim grdTotal14 As Decimal = 0
    Dim grdTotal21 As Decimal = 0
    Dim grdTotal22 As Decimal = 0
    Dim grdTotal23 As Decimal = 0
    Dim grdTotal24 As Decimal = 0
    Dim SmtpServer As New Net.Mail.SmtpClient()
    Dim mail As New Net.Mail.MailMessage
    Dim mailServer As String
    Dim mailPort As String
    Dim mailId As String
    Dim mailUser As String
    Dim mailPwd As String
    Dim mailSSL, sQuery As String
    Dim oRecordSet As SAPbobsCOM.Recordset
    Dim toID As String
    Dim ccID As String
    Dim mType As String
    Dim path As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Session("UserCode") Is Nothing Then
                Response.Redirect("Login.aspx?sessionExpired=true", True)
            ElseIf Session("SAPCompany") Is Nothing Then
                If Session("EmpUserName").ToString() = "" Or Session("UserPwd").ToString() = "" Then
                    strError = dbcon.Connection()
                Else
                    strError = dbcon.Connection(Session("EmpUserName").ToString(), Session("UserPwd").ToString())
                End If
                If strError <> "Success" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "js", "<script>alert('" & strError & "')</script>")
                Else
                    Session("SAPCompany") = dbcon.objMainCompany
                End If
            End If
            UserEN.EmpId = Session("UserCode").ToString()
            UserEN.Formid = "frm_hr_FApproval"
            If dbcon.validateAuthorization(UserEN) = False Then
                dbcon.strmsg = "alert('You are not authorized to do this action')"
                mess(dbcon.strmsg)
                btnsubmit.Visible = False
                btnclose.Visible = False
                Exit Sub
            Else
                panelview.Visible = True
                panelnew.Visible = False
                objen.EmpId = Session("UserCode").ToString()
                ViewState("EmpId") = objen.EmpId
                btnnew.Visible = False
                PageLoadBind(objen)
                btnsubmit.Visible = True
                btnclose.Visible = True
            End If
        End If
    End Sub

    Private Sub PageLoadBind(ByVal objen As LineMgrAppraisalEN)
        dbcon.ds = objBL.BindPageLoad(objen)
        If dbcon.ds.Tables(0).Rows.Count > 0 Then
            ddlperiod.DataSource = dbcon.ds.Tables(0)
            ddlperiod.DataTextField = "Name"
            ddlperiod.DataValueField = "Code"
            ddlperiod.DataBind()
            ddlperiod.Items.Insert(0, "---Select---")

            ddlsrPeriod.DataSource = dbcon.ds.Tables(0)
            ddlsrPeriod.DataTextField = "Name"
            ddlsrPeriod.DataValueField = "Code"
            ddlsrPeriod.DataBind()
            ddlsrPeriod.Items.Insert(0, "---Select---")
        Else
            ddlperiod.DataBind()
            ddlperiod.Items.Insert(0, "---Select---")
            ddlsrPeriod.DataBind()
            ddlsrPeriod.Items.Insert(0, "---Select---")
        End If
        objen.EmpId = ViewState("EmpId").ToString()
        objen.EmpId = objDA.getEmpIDforMangers(objen)
        If objen.EmpId <> "" Then
            If dbcon.ds.Tables(1).Rows.Count > 0 Then
                grdRequestApp.DataSource = dbcon.ds.Tables(1)
                grdRequestApp.DataBind()
            Else
                grdRequestApp.DataBind()
            End If
        End If
    End Sub
    Private Sub mess(ByVal str As String)
        ScriptManager.RegisterStartupScript(Update, Update.[GetType](), "strmsg", dbcon.strmsg, True)
    End Sub


    Private Function Checkweight(ByVal gv As GridView, ByVal weight As String) As Boolean
        Dim row1 As GridViewRow
        Dim dbweight, TotWeight, dbweight1 As Double
        For Each row1 In gv.Rows
            dbweight = CType(row1.FindControl(weight), TextBox).Text
            dbweight1 = dbweight1 + dbweight
            TotWeight = 100
        Next row1
        If TotWeight <> dbweight1 Then
            Return False
        End If
        Return True
    End Function
    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click

        objen.EmpId = ViewState("EmpId").ToString()
        If AddUDOUpdate("Save") = True Then
            dbcon.strmsg = "alert('Appraisal Request Updated successfully...')"
            mess(dbcon.strmsg)
            PageLoadBind(objen)
            panelview.Visible = True
            panelnew.Visible = False
        Else
            dbcon.strmsg = "alert('Appraisal Request Updated failed...')"
            mess(dbcon.strmsg)
        End If
    End Sub
    Private Function AddUDOUpdate(ByVal strchoice As String) As Boolean
        Dim StrSecondEmail As String
        Try
            Dim strFileName, ReportFileName As String
            Dim row1, row2, row3 As GridViewRow
            objen.AppraisalNumber = txtAppno.Text.Trim()
            objen.EmpId = ViewState("EmpId").ToString()
            objen.BusinessRemarks = txtBLmremarks.Text.Trim()
            objen.PeopleRemarks = txtPLmremarks.Text.Trim()
            objen.CompRemarks = txtCLmremarks.Text.Trim()


            For Each row1 In grdBusinessView.Rows
                objen.LineNo = CType(row1.FindControl("lblbusCode1"), Label).Text
                objen.Amount = CType(row1.FindControl("lblmgrrate1"), Label).Text
                objen.SelfRating = CType(row1.FindControl("ddlbusmgrfrate"), DropDownList).SelectedValue
                objen.BLineRemarks = CType(row1.FindControl("txtBfstmgrRemarks"), TextBox).Text
                objDA.UpdateLineMgrAppBusiness(objen)
            Next row1
            For Each row2 In grdPeopleview.Rows
                objen.LineNo = CType(row2.FindControl("lblpecode1"), Label).Text
                objen.Amount = CType(row2.FindControl("lblpemgrrate1"), Label).Text
                objen.SelfRating = CType(row2.FindControl("ddlpesmgrfrate"), DropDownList).SelectedValue
                objen.PLineRemarks = CType(row2.FindControl("txtPfstmgrRemarks"), TextBox).Text
                objDA.UpdateLineMgrAppPeople(objen)
            Next row2
            For Each row3 In grdCompetenceview.Rows
                objen.LineNo = CType(row3.FindControl("lblcompCode1"), Label).Text
                objen.Amount = CType(row3.FindControl("lblcompmgrRate1"), Label).Text
                objen.SelfRating = CType(row3.FindControl("ddlCompsmgrfrate"), DropDownList).SelectedValue
                objen.CLineRemarks = CType(row3.FindControl("txtCfstmgrRemarks"), TextBox).Text
                objDA.UpdateLineMgrAppCompetence(objen)
            Next row3

            If strchoice = "SaveSubmit" Then
                objen.CheckStatus = "Y"
                objen.SecondStatus = "Y"
                objen.Status = "LM"
                objen.SecondLvlApp = "Y"
                dbcon.dss2 = objBL.SecondLevelApproval(objen)
                If dbcon.dss2.Tables(0).Rows.Count > 0 Then
                    objen.SecondLvlApp = dbcon.dss2.Tables(0).Rows(0)(0).ToString()
                    If objen.SecondLvlApp = "N" Then
                        objen.SecondStatus = "Y"
                        objen.StrType = "HA"
                        objDA.UpdateLineMgrAppHeader(objen)
                        strFileName = Server.MapPath("~\AppraisalPDFs\" & txtempname.Text.Trim() & ".pdf")
                        dbcon.strmsg = SendMailforAppraisal(txtAppno.Text.Trim(), "HA", Session("SAPCompany"), objen.StrType)
                    Else
                        objen.SecondStatus = "N"
                        objen.CheckStatus = "Y"
                        objen.Status = "LM"
                        objen.SecondLvlApp = "Y"
                        objen.StrType = "LA"
                        objDA.UpdateLineMgrAppHeader(objen)
                        strFileName = Server.MapPath("~\AppraisalPDFs\" & txtempname.Text.Trim() & ".pdf")
                        StrSecondEmail = objBL.GetSecondEmail(ViewState("EmpId").ToString())
                        dbcon.strmsg = SendMailforAppraisal(txtAppno.Text.Trim(), "LA", Session("SAPCompany"), objen.StrType, StrSecondEmail)
                    End If
                End If
            Else
                objen.Status = ddlstatus.SelectedValue
                objen.CheckStatus = "N"
                objDA.UpdateLineMgrAppHeader(objen)
            End If

            objCOM.Status = "FL"
            objCOM.AppraisalNumber = txtAppno.Text.Trim()
            objCOM.EmpId = ViewState("EmpId").ToString()
            dbcon.UpdateTimeStamp(objCOM)
        Catch ex As Exception
            dbcon.strmsg = "alert('" & ex.Message & "')"
            mess(dbcon.strmsg)
            Return False
        End Try
        Return True
    End Function
    Protected Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        panelview.Visible = True
        panelnew.Visible = False
        btnnew.Visible = False
    End Sub
    Protected Sub lbtndocnum_Click(ByVal sender As Object, ByVal e As EventArgs)

        If Session("UserCode") Is Nothing Or Session("SAPCompany") Is Nothing Then
            dbcon.strmsg = "alert('Your session is Expired...')"
            mess(dbcon.strmsg)
            Response.Redirect("Login.aspx?sessionExpired=true", True)
        Else
            Dim link As LinkButton = CType(sender, LinkButton)
            Dim gv As GridViewRow = CType((link.Parent.Parent), GridViewRow)
            Dim DocNo As LinkButton = CType(gv.FindControl("lbtndocnum"), LinkButton)
            Dim EmpNo As Label = CType(gv.FindControl("lblcustName"), Label)
            panelview.Visible = False
            panelnew.Visible = True
            btnnew.Visible = False
            objen.AppraisalNumber = DocNo.Text.Trim()
            objen.EmpId = EmpNo.Text.Trim()
            PopulateAppraisal(objen)
            btnsubmit.Visible = False
        End If
    End Sub
    Private Sub ObjectiveBind(ByVal objen As LineMgrAppraisalEN)
        dbcon.ds = objBL.ObjectiveBind(objen)
        If dbcon.ds.Tables(0).Rows.Count > 0 Then
            grdBusinessView.DataSource = dbcon.ds.Tables(0)
            grdBusinessView.DataBind()

        Else
            grdBusinessView.DataBind()
        End If
        If dbcon.ds.Tables(1).Rows.Count > 0 Then
            grdPeopleview.DataSource = dbcon.ds.Tables(1)
            grdPeopleview.DataBind()
        Else
            grdPeopleview.DataBind()
        End If
        If dbcon.ds.Tables(2).Rows.Count > 0 Then
            grdCompetenceview.DataSource = dbcon.ds.Tables(2)
            grdCompetenceview.DataBind()
        Else
            grdCompetenceview.DataBind()
        End If
    End Sub
    Private Sub PopulateAppraisal(ByVal objen As LineMgrAppraisalEN)

        ObjectiveBind(objen)
        If objen.EmpId <> "" Then
            dbcon.ds1 = objBL.PopulateEmployee(objen)
            If dbcon.ds1.Tables(0).Rows.Count > 0 Then
                txtAppno.Text = dbcon.ds1.Tables(0).Rows(0)("DocEntry").ToString()
                txtreqdate.Text = dbcon.ds1.Tables(0).Rows(0)("U_Z_Date").ToString()
                txtempid.Text = dbcon.ds1.Tables(0).Rows(0)("U_Z_EmpId").ToString()
                txtempname.Text = dbcon.ds1.Tables(0).Rows(0)("U_Z_EmpName").ToString()
                ddlstatus.SelectedIndex = ddlstatus.Items.IndexOf(ddlstatus.Items.FindByText(dbcon.ds1.Tables(0).Rows(0)("U_Z_WStatus").ToString()))
                If dbcon.ds1.Tables(0).Rows(0)("U_Z_Period").ToString() <> "" Then
                    objen.StrType = dbcon.ds1.Tables(0).Rows(0)("U_Z_Period").ToString()
                    ddlperiod.SelectedItem.Text = objen.StrType
                End If
                lblPeriodfrom.Text = dbcon.ds1.Tables(0).Rows(0)("U_Z_PerFrom").ToString()
                lblperioTo.Text = dbcon.ds1.Tables(0).Rows(0)("U_Z_PerTo").ToString()

                txtsBremarks.Text = dbcon.ds1.Tables(0).Rows(0)("U_Z_BSelfRemark").ToString()
                txtsPremarks.Text = dbcon.ds1.Tables(0).Rows(0)("U_Z_PSelfRemark").ToString()
                txtsCremarks.Text = dbcon.ds1.Tables(0).Rows(0)("U_Z_CSelfRemark").ToString()

                txtBSrremarks.Text = dbcon.ds1.Tables(0).Rows(0)("U_Z_BSMrRemark").ToString()
                txtPSrremarks.Text = dbcon.ds1.Tables(0).Rows(0)("U_Z_PSMrRemark").ToString()
                txtCSrremarks.Text = dbcon.ds1.Tables(0).Rows(0)("U_Z_CSMrRemark").ToString()

                txtBLmremarks.Text = dbcon.ds1.Tables(0).Rows(0)("U_Z_BMgrRemark").ToString()
                txtPLmremarks.Text = dbcon.ds1.Tables(0).Rows(0)("U_Z_PMgrRemark").ToString()
                txtCLmremarks.Text = dbcon.ds1.Tables(0).Rows(0)("U_Z_CMgrRemark").ToString()

                txtCHrremarks.Text = dbcon.ds1.Tables(0).Rows(0)("U_Z_CHrRemark").ToString()
                txtPHrremarks.Text = dbcon.ds1.Tables(0).Rows(0)("U_Z_PHrRemark").ToString()
                txtBCHrremarks.Text = dbcon.ds1.Tables(0).Rows(0)("U_Z_BHrRemark").ToString()
                EnableDisable(dbcon.ds1.Tables(0).Rows(0)("U_Z_WStatus").ToString(), dbcon.ds1.Tables(0).Rows(0)("U_Z_GStatus").ToString())
            End If
        End If
    End Sub
    Private Sub EnableDisable(ByVal strStatus As String, ByVal grvstatus As String)

        grdCompetenceview.Visible = True
        grdPeopleview.Visible = True
        grdBusinessView.Visible = True
        If strStatus <> "HR Approved" And strStatus <> "Sr.Manager Approved" And grvstatus = "-" Then
            ddlstatus.Enabled = False
            ddlperiod.Enabled = False
            btnsubmit.Visible = False
            btnUpdate.Visible = True
            txtBLmremarks.ReadOnly = False
            btnsavesubmit.Visible = True
        Else
            ddlstatus.Enabled = False
            ddlperiod.Enabled = False
            txtBLmremarks.ReadOnly = True
            btnsubmit.Visible = False
            btnUpdate.Visible = False
            btnsavesubmit.Visible = False
        End If
    End Sub

    Protected Sub grdRequestApp_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdRequestApp.PageIndexChanging
        grdRequestApp.PageIndex = e.NewPageIndex
        objen.EmpId = ViewState("EmpId").ToString()
        PageLoadBind(objen)
    End Sub
    Protected Sub grdBusinessView_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdBusinessView.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim ddl As DropDownList = DirectCast(e.Row.FindControl("ddlbusselfrate"), DropDownList)
            Dim mgrddl As DropDownList = DirectCast(e.Row.FindControl("ddlbusmgrfrate"), DropDownList)
            Dim smgrddl As DropDownList = DirectCast(e.Row.FindControl("ddlsmgrfrate"), DropDownList)
            dbcon.Dropdown1("Select ""U_Z_RateCode"" As ""Code"",""U_Z_RateName"" As ""Name"" From ""@Z_HR_ORATE""", "Code", "Name", ddl)
            dbcon.Dropdown1("Select ""U_Z_RateCode"" As ""Code"",""U_Z_RateName"" As ""Name"" From ""@Z_HR_ORATE""", "Code", "Name", mgrddl)
            dbcon.Dropdown1("Select ""U_Z_RateCode"" As ""Code"",""U_Z_RateName"" As ""Name"" From ""@Z_HR_ORATE""", "Code", "Name", smgrddl)
            Dim lblBself As String = CType(e.Row.FindControl("lblbusselfrate"), Label).Text
            If lblBself <> "" Then
                ddl.Items.FindByValue(lblBself).Selected = True
            End If
            Dim lblBMgr As String = CType(e.Row.FindControl("lblbusmgrfrate"), Label).Text
            If lblBMgr <> "" Then
                mgrddl.Items.FindByValue(lblBMgr).Selected = True
            End If
            Dim lblBsmgr As String = CType(e.Row.FindControl("lblsmgrfrate"), Label).Text
            If lblBsmgr <> "" Then
                smgrddl.Items.FindByValue(lblBsmgr).Selected = True
            End If
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim rowtotal As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "U_Z_BussWeight"))
            grdTotal = grdTotal + rowtotal
            Dim rowtotal1 As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "U_Z_BussSelfRate"))
            grdTotal1 = grdTotal1 + rowtotal1
            Dim rowtotal2 As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "U_Z_BussMgrRate"))
            grdTotal2 = grdTotal2 + rowtotal2
            Dim rowtotal3 As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "U_Z_BussSMRate"))
            grdTotal3 = grdTotal3 + rowtotal3
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            Dim lbl As Label = CType(e.Row.FindControl("lblBselfweight"), Label)
            lbl.Text = grdTotal.ToString()
            Dim lbl1 As Label = CType(e.Row.FindControl("lblBselfrate"), Label)
            lbl1.Text = grdTotal1.ToString()
            Dim lbl2 As Label = CType(e.Row.FindControl("lblBLinerate"), Label)
            lbl2.Text = grdTotal2.ToString()
            Dim lbl3 As Label = CType(e.Row.FindControl("lblBsrrate"), Label)
            lbl3.Text = grdTotal3.ToString()
        End If
    End Sub

    Protected Sub grdCompetenceview_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdCompetenceview.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim ddl As DropDownList = DirectCast(e.Row.FindControl("ddlcompselfrate"), DropDownList)
            Dim mgrddl As DropDownList = DirectCast(e.Row.FindControl("ddlCompsmgrfrate"), DropDownList)
            Dim smgrddl As DropDownList = DirectCast(e.Row.FindControl("ddlcompsmgrrate"), DropDownList)
            dbcon.Dropdown1("Select ""U_Z_RateCode"" As ""Code"",""U_Z_RateName"" As ""Name"" From ""@Z_HR_ORATE""", "Code", "Name", ddl)
            dbcon.Dropdown1("Select ""U_Z_RateCode"" As ""Code"",""U_Z_RateName"" As ""Name"" From ""@Z_HR_ORATE""", "Code", "Name", mgrddl)
            dbcon.Dropdown1("Select ""U_Z_RateCode"" As ""Code"",""U_Z_RateName"" As ""Name"" From ""@Z_HR_ORATE""", "Code", "Name", smgrddl)
            Dim lblBself As String = CType(e.Row.FindControl("lblcompselfrate"), Label).Text
            If lblBself <> "" Then
                ddl.Items.FindByValue(lblBself).Selected = True
            End If
            Dim lblBMgr As String = CType(e.Row.FindControl("lblCompsmgrfrate"), Label).Text
            If lblBMgr <> "" Then
                mgrddl.Items.FindByValue(lblBMgr).Selected = True
            End If
            Dim lblBsmgr As String = CType(e.Row.FindControl("lblcompsmgrrate"), Label).Text
            If lblBsmgr <> "" Then
                smgrddl.Items.FindByValue(lblBsmgr).Selected = True
            End If
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim rowtotal As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "U_Z_CompWeight"))
            grdTotal11 = grdTotal11 + rowtotal
            Dim rowtotal1 As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "U_Z_CompSelfRate"))
            grdTotal12 = grdTotal12 + rowtotal1
            Dim rowtotal2 As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "U_Z_CompMgrRate"))
            grdTotal13 = grdTotal13 + rowtotal2
            Dim rowtotal3 As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "U_Z_CompSMRate"))
            grdTotal14 = grdTotal14 + rowtotal3
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            Dim lbl As Label = CType(e.Row.FindControl("lblCselfweight"), Label)
            lbl.Text = grdTotal11.ToString()
            Dim lbl1 As Label = CType(e.Row.FindControl("lblCselfrate"), Label)
            lbl1.Text = grdTotal12.ToString()
            Dim lbl2 As Label = CType(e.Row.FindControl("lblCLinerate"), Label)
            lbl2.Text = grdTotal13.ToString()
            Dim lbl3 As Label = CType(e.Row.FindControl("lblCsrrate"), Label)
            lbl3.Text = grdTotal14.ToString()
        End If
    End Sub

    Protected Sub grdPeopleview_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdPeopleview.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim ddl As DropDownList = DirectCast(e.Row.FindControl("ddlpeselfrate"), DropDownList)
            Dim mgrddl As DropDownList = DirectCast(e.Row.FindControl("ddlpesmgrfrate"), DropDownList)
            Dim smgrddl As DropDownList = DirectCast(e.Row.FindControl("ddlsmgrrate"), DropDownList)
            dbcon.Dropdown1("Select ""U_Z_RateCode"" As ""Code"",""U_Z_RateName"" As ""Name"" From ""@Z_HR_ORATE""", "Code", "Name", ddl)
            dbcon.Dropdown1("Select ""U_Z_RateCode"" As ""Code"",""U_Z_RateName"" As ""Name"" From ""@Z_HR_ORATE""", "Code", "Name", mgrddl)
            dbcon.Dropdown1("Select ""U_Z_RateCode"" As ""Code"",""U_Z_RateName"" As ""Name"" From ""@Z_HR_ORATE""", "Code", "Name", smgrddl)
            Dim lblBself As String = CType(e.Row.FindControl("lblpeselfrate"), Label).Text
            If lblBself <> "" Then
                ddl.Items.FindByValue(lblBself).Selected = True
            End If
            Dim lblBMgr As String = CType(e.Row.FindControl("lblpesmgrfrate"), Label).Text
            If lblBMgr <> "" Then
                mgrddl.Items.FindByValue(lblBMgr).Selected = True
            End If
            Dim lblBsmgr As String = CType(e.Row.FindControl("lblsmgrrate"), Label).Text
            If lblBsmgr <> "" Then
                smgrddl.Items.FindByValue(lblBsmgr).Selected = True
            End If
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim rowtotal As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "U_Z_PeoWeight"))
            grdTotal21 = grdTotal21 + rowtotal
            Dim rowtotal1 As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "U_Z_PeoSelfRate"))
            grdTotal22 = grdTotal22 + rowtotal1
            Dim rowtotal2 As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "U_Z_PeoMgrRate"))
            grdTotal23 = grdTotal23 + rowtotal2
            Dim rowtotal3 As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "U_Z_PeoSMRate"))
            grdTotal24 = grdTotal24 + rowtotal3
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            Dim lbl As Label = CType(e.Row.FindControl("lblPselfweight"), Label)
            lbl.Text = grdTotal21.ToString()
            Dim lbl1 As Label = CType(e.Row.FindControl("lblPselfrate"), Label)
            lbl1.Text = grdTotal22.ToString()
            Dim lbl2 As Label = CType(e.Row.FindControl("lblPLinerate"), Label)
            lbl2.Text = grdTotal23.ToString()
            Dim lbl3 As Label = CType(e.Row.FindControl("lblPsrrate"), Label)
            lbl3.Text = grdTotal24.ToString()
        End If
    End Sub

    Protected Sub ddlbusmgrfrate_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dblVate, dblweight As Double
        Dim link As DropDownList = CType(sender, DropDownList)
        Dim gv As GridViewRow = CType((link.Parent.Parent), GridViewRow)
        Dim i As Integer = gv.RowIndex
        Dim ddl As DropDownList = DirectCast(grdBusinessView.Rows(i).FindControl("ddlbusmgrfrate"), DropDownList)
        Dim strweight As String = DirectCast(grdBusinessView.Rows(i).FindControl("lblweight1"), Label).Text
        Dim txtself As Label = DirectCast(grdBusinessView.Rows(i).FindControl("lblmgrrate1"), Label)
        objen.Ratings = ddl.SelectedValue
        dbcon.ds2 = objBL.SelectionChange(objen)
        If dbcon.ds2.Tables(0).Rows.Count > 0 Then
            dblVate = dbcon.ds2.Tables(0).Rows(0)("U_Z_Total")
            dblweight = CDbl(strweight)
            dblVate = dblVate * dblweight
            txtself.Text = dblVate / 100
        End If

    End Sub
    Protected Sub ddlpesmgrfrate_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dblVate, dblweight As Double
        Dim link As DropDownList = CType(sender, DropDownList)
        Dim gv As GridViewRow = CType((link.Parent.Parent), GridViewRow)
        Dim i As Integer = gv.RowIndex
        Dim ddl As DropDownList = DirectCast(grdPeopleview.Rows(i).FindControl("ddlpesmgrfrate"), DropDownList)
        Dim strweight As String = DirectCast(grdPeopleview.Rows(i).FindControl("lblpeweight1"), Label).Text
        Dim txtself As Label = DirectCast(grdPeopleview.Rows(i).FindControl("lblpemgrrate1"), Label)
        objen.Ratings = ddl.SelectedValue
        dbcon.ds2 = objBL.SelectionChange(objen)
        If dbcon.ds2.Tables(0).Rows.Count > 0 Then
            dblVate = dbcon.ds2.Tables(0).Rows(0)("U_Z_Total")
            dblweight = CDbl(strweight)
            dblVate = dblVate * dblweight
            txtself.Text = dblVate / 100
        End If
    End Sub
    Protected Sub ddlCompsmgrfrate_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dblweight, dblExpectedLevel, dblFinalRate, dblSelfRate As Double
        Dim link As DropDownList = CType(sender, DropDownList)
        Dim gv As GridViewRow = CType((link.Parent.Parent), GridViewRow)
        Dim i As Integer = gv.RowIndex
        Dim ddl As DropDownList = DirectCast(grdCompetenceview.Rows(i).FindControl("ddlCompsmgrfrate"), DropDownList)
        Dim strweight As String = DirectCast(grdCompetenceview.Rows(i).FindControl("lblCompWeight1"), Label).Text
        Dim lbllevels As Label = DirectCast(grdCompetenceview.Rows(i).FindControl("lblCompLevel"), Label)
        Dim lblcurrlevel As Label = DirectCast(grdCompetenceview.Rows(i).FindControl("lblCurrLevel"), Label)
        Dim txtself As Label = DirectCast(grdCompetenceview.Rows(i).FindControl("lblcompmgrRate1"), Label)
        dblweight = CDbl(strweight)
        objen.Ratings = ddl.SelectedValue
        dbcon.ds2 = objBL.SelectionChange(objen)
        If dbcon.ds2.Tables(0).Rows.Count > 0 Then
            dblSelfRate = CDbl(dbcon.ds2.Tables(0).Rows(0)("U_Z_Total"))
        End If

        If lbllevels.Text = "" Then
            dblExpectedLevel = 0
        Else
            dblExpectedLevel = CDbl(lbllevels.Text.Trim())
        End If
        If dblSelfRate > dblExpectedLevel Then
            dblFinalRate = dblweight
        Else
            dblFinalRate = dblweight / dblExpectedLevel * dblSelfRate
        End If
        txtself.Text = dblFinalRate
    End Sub
    Protected Sub btnsavesubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsavesubmit.Click

        If AddUDOUpdate("SaveSubmit") = True Then
            dbcon.strmsg = "alert('Appraisal First Level Approved successfully...')"
            mess(dbcon.strmsg)
            objen.EmpId = ViewState("EmpId").ToString()
            PageLoadBind(objen)
            panelview.Visible = True
            panelnew.Visible = False
        Else
            dbcon.strmsg = "alert('Appraisal First Level Approved failed...')"
            mess(dbcon.strmsg)
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim Code, Period, Empid, Status As String
        objen.EmpId = ViewState("EmpId").ToString()
        objen.SelfRating = txtAppFrom.Text.Trim()
        objen.StrType = txtEmpfrom.Text.Trim()
        objen.Status = ddlsrStatus.SelectedValue
        objen.Period = ddlsrPeriod.SelectedIndex
        If objen.SelfRating = "" And objen.StrType = "" And objen.Status = "" And objen.Period = 0 Then
            dbcon.strmsg = "alert('Select any one...')"
            mess(dbcon.strmsg)
        Else
            If objen.SelfRating <> "" Then
                Code = """DocEntry""='" & objen.SelfRating & "'"
            Else
                Code = "1=1"
            End If
            If objen.StrType <> "" Then
                Empid = """U_Z_EmpId""='" & objen.StrType & "'"
            Else
                Empid = "1=1"
            End If
            If objen.Status <> "" Then
                Status = """U_Z_WStatus""='" & objen.Status & "'"
            Else
                Status = "1=1"
            End If
            If objen.Period <> 0 Then
                Period = """U_Z_Period""='" & ddlsrPeriod.SelectedValue & "'"
            Else
                Period = "1=1"
            End If
            objen.SearchCondition = Code & " and " & Empid & " and " & Status & " and " & Period
            dbcon.dss1 = objBL.BindSearchPageLoad(objen)
            If dbcon.dss1.Tables(0).Rows.Count > 0 Then
                grdRequestApp.DataSource = dbcon.dss1.Tables(0)
                grdRequestApp.DataBind()
            Else
                grdRequestApp.DataBind()
            End If
        End If
    End Sub

    Private Sub LnkViewall_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LnkViewall.Click
        objen.EmpId = ViewState("EmpId").ToString()
        PageLoadBind(objen)
    End Sub
    Public Function SendMailforAppraisal(ByVal strDocEntry As String, ByVal StrType As String, ByVal SAPCompany As SAPbobsCOM.Company, ByVal strLevel As String, Optional ByVal StrSecondEmail As String = "") As String
        Dim oDtAppraisal As DataTable = New DataTable()
        Dim strCCId, strToId, strName As String
        Dim oRecordSet As SAPbobsCOM.Recordset
        oRecordSet = SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        Try

            objen.StrQry = "Select T0.Email,T0.U_Z_HRMail,isnull(T0.firstName,'') +' ' +isnull(lastName,'') As Name From OHEM T0 JOIN [@Z_HR_OSEAPP] T2 ON T0.EmpID = T2.U_Z_EmpId Where T2.DocEntry = '" & strDocEntry & "'"
            oRecordSet.DoQuery(objen.StrQry)
            If oRecordSet.RecordCount > 0 Then
                strCCId = oRecordSet.Fields.Item(0).Value
                If StrType = "HA" Then
                    strToId = oRecordSet.Fields.Item(1).Value
                Else
                    strToId = StrSecondEmail
                End If
                strName = oRecordSet.Fields.Item(2).Value
            End If

            If dbcon.checkmailconfiguration() = False Then
                dbcon.strmsg = "Email configuration not availble..."
                Return dbcon.strmsg
            Else
                If strToId <> "" Then
                    'generateReport(strDocEntry, SAPCompany)
                    dbcon.strmsg = SendMail(strCCId, strToId, strName, "Appraisal", SAPCompany, strDocEntry, strLevel)
                End If
            End If
        Catch ex As Exception
            dbcon.strmsg = ex.Message
            Return dbcon.strmsg
        End Try
        Return dbcon.strmsg
    End Function
    Public Sub generateReport(ByVal strDocEntry As String, ByVal SAPCompany As SAPbobsCOM.Company)
        Dim strEmpName, sQuery As String
        Dim dr, row As DataRow
        Dim oRecordSet As SAPbobsCOM.Recordset
        Dim oCrystalDocument As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        oRecordSet = SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        Dim dsAp As New dsAppraisal
        'Header
        sQuery = "Select T0.U_Z_EmpID,U_Z_EmpName,T0.U_Z_Period,T0.U_Z_PerDesc,U_Z_LStrt,U_Z_Date,U_Z_BSelfRemark,U_Z_BMgrRemark,U_Z_BSMrRemark,U_Z_BHrRemark,T2.Descriptio,T3.Remarks,DocEntry  From [@Z_HR_OSEAPP] T0 JOIN OHEM T1 ON T0.U_Z_EmpId = T1.EmpID JOIN OHPS T2 On T2.PosID = T1.Position JOIN OUDP T3 On T3.Code = T1.Dept Where DocEntry = '" & strDocEntry & "'"
        oRecordSet.DoQuery(sQuery)
        If Not oRecordSet.EoF Then
            dr = dsAp.Tables("Header").NewRow()
            strEmpName = oRecordSet.Fields.Item("U_Z_EmpID").Value & "_" & oRecordSet.Fields.Item("U_Z_EmpName").Value
            dr("DocEntry") = oRecordSet.Fields.Item("DocEntry").Value
            dr("EmpID") = oRecordSet.Fields.Item("U_Z_EmpID").Value
            dr("EmpName") = oRecordSet.Fields.Item("U_Z_EmpName").Value
            dr("Period") = oRecordSet.Fields.Item("U_Z_Period").Value
            'dr("PeriodDesc") = oRecordSet.Fields.Item("U_Z_PerDesc").Value
            dr("AppraisalStarts") = oRecordSet.Fields.Item("U_Z_LStrt").Value
            dr("ReportType") = "Appraisal Document"
            dr("Date") = oRecordSet.Fields.Item("U_Z_Date").Value
            dr("SEComments") = oRecordSet.Fields.Item("U_Z_BSelfRemark").Value
            dr("LMComments") = oRecordSet.Fields.Item("U_Z_BMgrRemark").Value
            dr("SMComments") = oRecordSet.Fields.Item("U_Z_BSMrRemark").Value
            dr("HRComments") = oRecordSet.Fields.Item("U_Z_BHrRemark").Value
            dr("Position") = oRecordSet.Fields.Item("Descriptio").Value
            dr("Department") = oRecordSet.Fields.Item("Remarks").Value
            dsAp.Tables("Header").Rows.Add(dr)
        End If

        'Bussiness Objectives
        sQuery = "Select U_Z_BussCode,U_Z_BussDesc,U_Z_BussWeight,U_Z_BussSelfRate,U_Z_BussMgrRate,U_Z_BussSMRate,U_Z_BussSMRate As U_Z_BussHRRate,DocEntry From [@Z_HR_SEAPP1] Where DocEntry = '" & strDocEntry & "'"
        oRecordSet.DoQuery(sQuery)
        For index1 As Integer = 0 To oRecordSet.RecordCount - 1
            If Not oRecordSet.EoF Then
                dr = dsAp.Tables("Bussiness").NewRow()
                dr("DocEntry") = oRecordSet.Fields.Item("DocEntry").Value
                dr("BussCode") = oRecordSet.Fields.Item("U_Z_BussCode").Value
                dr("BussName") = oRecordSet.Fields.Item("U_Z_BussDesc").Value
                dr("BussWeight") = oRecordSet.Fields.Item("U_Z_BussWeight").Value
                dr("BussSR") = oRecordSet.Fields.Item("U_Z_BussSelfRate").Value
                dr("BussLM") = oRecordSet.Fields.Item("U_Z_BussMgrRate").Value
                dr("BussSM") = oRecordSet.Fields.Item("U_Z_BussSMRate").Value
                dr("BussHR") = oRecordSet.Fields.Item("U_Z_BussHRRate").Value
                dsAp.Tables("Bussiness").Rows.Add(dr)
                oRecordSet.MoveNext()
            End If
        Next

        'People Objectives
        sQuery = "Select U_Z_PeopleCode,U_Z_PeopleDesc,U_Z_PeopleCat,U_Z_PeoWeight,U_Z_PeoSelfRate,U_Z_PeoMgrRate,U_Z_PeoSMRate As U_Z_PeoHrRate,U_Z_PeoSMRate,DocEntry From [@Z_HR_SEAPP2] Where DocEntry = '" & strDocEntry & "'"
        oRecordSet.DoQuery(sQuery)
        For index1 As Integer = 0 To oRecordSet.RecordCount - 1
            If Not oRecordSet.EoF Then
                dr = dsAp.Tables("People").NewRow()
                dr("DocEntry") = oRecordSet.Fields.Item("DocEntry").Value
                dr("PeopleCode") = oRecordSet.Fields.Item("U_Z_PeopleCode").Value
                dr("PeopleName") = oRecordSet.Fields.Item("U_Z_PeopleDesc").Value
                dr("PeopleCat") = oRecordSet.Fields.Item("U_Z_PeopleCat").Value
                dr("PeopleWeight") = oRecordSet.Fields.Item("U_Z_PeoWeight").Value
                dr("PeopleSR") = oRecordSet.Fields.Item("U_Z_PeoSelfRate").Value
                dr("PeopleLM") = oRecordSet.Fields.Item("U_Z_PeoMgrRate").Value
                dr("PeopleSM") = oRecordSet.Fields.Item("U_Z_PeoSMRate").Value
                dr("PeopleHR") = oRecordSet.Fields.Item("U_Z_PeoHrRate").Value
                dsAp.Tables("People").Rows.Add(dr)
                oRecordSet.MoveNext()
            End If
        Next

        'Competency Objectives
        sQuery = "Select U_Z_CompCode,U_Z_CompDesc,U_Z_CompWeight,U_Z_CompLevel,U_Z_CompSelfRate,U_Z_CompMgrRate,U_Z_CompSMRate As U_Z_CompHrRate,U_Z_CompSMRate,DocEntry From [@Z_HR_SEAPP3] Where DocEntry = '" & strDocEntry & "'"
        oRecordSet.DoQuery(sQuery)
        For index1 As Integer = 0 To oRecordSet.RecordCount - 1
            If Not oRecordSet.EoF Then
                dr = dsAp.Tables("Competency").NewRow()
                dr("DocEntry") = oRecordSet.Fields.Item("DocEntry").Value
                dr("CompCode") = oRecordSet.Fields.Item("U_Z_CompCode").Value
                dr("CompName") = oRecordSet.Fields.Item("U_Z_CompDesc").Value
                dr("CompWeight") = oRecordSet.Fields.Item("U_Z_CompWeight").Value
                dr("CompLevel") = oRecordSet.Fields.Item("U_Z_CompLevel").Value
                dr("CompSR") = oRecordSet.Fields.Item("U_Z_CompSelfRate").Value
                dr("CompLM") = oRecordSet.Fields.Item("U_Z_CompMgrRate").Value
                dr("CompSM") = oRecordSet.Fields.Item("U_Z_CompSMRate").Value
                dr("CompHR") = oRecordSet.Fields.Item("U_Z_CompHrRate").Value
                dsAp.Tables("Competency").Rows.Add(dr)
                oRecordSet.MoveNext()
            End If
        Next

        'HR Final Rating
        sQuery = "Select U_Z_CompType,U_Z_AvgComp,U_Z_HRComp,DocEntry From [@Z_HR_SEAPP4] Where DocEntry = '" & strDocEntry & "'"
        oRecordSet.DoQuery(sQuery)
        For index1 As Integer = 0 To oRecordSet.RecordCount - 1
            If Not oRecordSet.EoF Then
                dr = dsAp.Tables("HRFinal").NewRow()
                dr("DocEntry") = oRecordSet.Fields.Item("DocEntry").Value
                dr("Type") = oRecordSet.Fields.Item("U_Z_CompType").Value
                dr("AvgComp") = oRecordSet.Fields.Item("U_Z_AvgComp").Value
                dr("HRComp") = oRecordSet.Fields.Item("U_Z_HRComp").Value
                dsAp.Tables("HRFinal").Rows.Add(dr)
                oRecordSet.MoveNext()
            End If
        Next
        Dim strFilename As String = Server.MapPath("~\AppraisalPDFs\" & txtempname.Text.Trim() & ".pdf") 'System.Windows.Forms.Application.StartupPath & "\AppraisalPDFs\" & strEmpName & ".pdf"
        ' Dim strReportFileName As String = System.Windows.Forms.Application.StartupPath & "\Reports\" & "rptAppraisal1.rpt"
        Dim strReportFileName As String = Server.MapPath("~\Reports\" & "rptApp.rpt") ' System.Windows.Forms.Application.StartupPath & "\Reports\" & "rptApp.rpt"
        oCrystalDocument.Load(strReportFileName)
        oCrystalDocument.SetDataSource(dsAp)

        If File.Exists(strFilename) Then
            File.Delete(strFilename)
        End If

        Dim CrExportOptions As CrystalDecisions.Shared.ExportOptions
        Dim CrDiskFileDestinationOptions As New  _
        CrystalDecisions.Shared.DiskFileDestinationOptions()
        Dim CrFormatTypeOptions As New CrystalDecisions.Shared.PdfRtfWordFormatOptions()
        CrDiskFileDestinationOptions.DiskFileName = strFilename
        CrExportOptions = oCrystalDocument.ExportOptions
        With CrExportOptions
            .ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
            .ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
            .DestinationOptions = CrDiskFileDestinationOptions
            .FormatOptions = CrFormatTypeOptions
        End With
        oCrystalDocument.Export()
        oCrystalDocument.Close()

        'Dim x As System.Diagnostics.ProcessStartInfo
        'x = New System.Diagnostics.ProcessStartInfo
        'x.UseShellExecute = True
        'x.FileName = strFilename
        'System.Diagnostics.Process.Start(x)
        'x = Nothing
        Session("Path") = strFilename

    End Sub

    Public Function SendMail(ByVal strCCId As String, ByVal strToId As String, ByVal strName As String, ByVal strType As String, ByVal SAPCompany As SAPbobsCOM.Company, ByVal strDocEntry As String, ByVal StrLevel As String, Optional ByVal Period As String = "", Optional ByVal path As String = "") As String
        oRecordSet = SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        oRecordSet.DoQuery("Select U_Z_SMTPSERV,U_Z_SMTPPORT,U_Z_SMTPUSER,U_Z_SMTPPWD,U_Z_SSL From [@Z_HR_OMAIL]")
        If Not oRecordSet.EoF Then
            mailServer = oRecordSet.Fields.Item("U_Z_SMTPSERV").Value
            mailPort = oRecordSet.Fields.Item("U_Z_SMTPPORT").Value
            mailId = oRecordSet.Fields.Item("U_Z_SMTPUSER").Value
            mailPwd = oRecordSet.Fields.Item("U_Z_SMTPPWD").Value
            mailSSL = oRecordSet.Fields.Item("U_Z_SSL").Value
            If mailServer <> "" And mailId <> "" And mailPwd <> "" Then
                If strType = "Appraisal" Then
                    toID = strToId
                    ccID = strCCId
                    mType = StrLevel
                    path = path
                    If toID.Length > 0 And ccID.Length = 0 Then
                        ccID = toID
                        dbcon.strmsg = SendMailforUsers(mailServer, mailPort, mailId, mailPwd, mailSSL, toID, ccID, mType, path, strDocEntry, strName, SAPCompany, Period)
                    ElseIf toID.Length = 0 And ccID.Length > 0 Then
                        toID = ccID
                        dbcon.strmsg = SendMailforUsers(mailServer, mailPort, mailId, mailPwd, mailSSL, toID, ccID, mType, path, strDocEntry, strName, SAPCompany, Period)
                    Else
                        dbcon.strmsg = SendMailforUsers(mailServer, mailPort, mailId, mailPwd, mailSSL, toID, ccID, mType, path, strDocEntry, strName, SAPCompany, Period)
                    End If
                End If
            Else
                dbcon.strmsg = "Mail Server Details Not Configured..."
            End If
        End If
        Return dbcon.strmsg
    End Function

    Private Function SendMailforUsers(ByVal mailServer As String, ByVal mailPort As String, ByVal mailId As String, ByVal mailpwd As String, ByVal mailSSL As String, ByVal toId As String, ByVal ccId As String, ByVal mType As String, ByVal path As String, ByVal DocEntry As String, ByVal Name As String, ByVal SAPCompany As SAPbobsCOM.Company, Optional ByVal Period As String = "") As String
        Try
            Dim oTemp, oRecordSet As SAPbobsCOM.Recordset
            Dim strMessage, strQuery As String
            Dim oTest As SAPbobsCOM.Recordset
            oTest = SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oTemp = SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oRecordSet = SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            SmtpServer.Credentials = New Net.NetworkCredential(mailId, mailpwd)
            SmtpServer.Port = mailPort
            SmtpServer.EnableSsl = mailSSL
            SmtpServer.Host = mailServer
            mail = New Net.Mail.MailMessage()
            mail.From = New Net.Mail.MailAddress(mailId, "HRMS")
            mail.To.Add(toId)
            mail.CC.Add(ccId)
            mail.IsBodyHtml = True
            mail.Priority = MailPriority.High
            If mType = "SF" Then
                oRecordSet.DoQuery("Select T0.""U_Z_PerFrom"" ,T0.""U_Z_PerTo"",T1.U_Z_Empid from ""@Z_HR_PERAPP"" T0 JOIN [@Z_HR_OSEAPP] T1 on T1.U_Z_Period=T0.U_Z_PerCode where T1.DocEntry='" & DocEntry & "'")
                Dim strPeriod As String = "Period From: " & oRecordSet.Fields.Item("U_Z_PerFrom").Value & "    " & "Period To: " & oRecordSet.Fields.Item("U_Z_PerTo").Value
                strQuery = "SELECT T1.email,isnull(T0.firstName,'') +' '+ isnull(T0.lastName,'') as 'EmpName',T1.userId from OHEM T0 JOIN OHEM T1 ON T0.Manager=T1.empID where T0.empID=" & oRecordSet.Fields.Item("U_Z_Empid").Value
                oTemp.DoQuery(strQuery)
                If oTemp.RecordCount > 0 Then
                    oTest.DoQuery("Select * from [@Z_HR_OWEB]")
                    Dim strESSLink As String = ""
                    If oTest.RecordCount > 0 Then
                        strESSLink = oTest.Fields.Item("U_Z_WebPath").Value
                    End If
                    strMessage = "Appraisal Document No : " & DocEntry & ", Employee Name : " & oTemp.Fields.Item("EmpName").Value & ",Appraisal Period on " & strPeriod & ""
                    mail.Subject = strMessage
                    mail.Body = BuildHtmBody(DocEntry, Name, "Appraisal", mType, SAPCompany, strMessage)
                    'mail.Attachments.Add(New Net.Mail.Attachment(path))
                    SendMessageAppraisal("First Level manager Appraisal Approval", strMessage, oTemp.Fields.Item(2).Value, SAPCompany)
                End If
            ElseIf mType = "LA" Then
                oRecordSet.DoQuery("Select T0.""U_Z_PerFrom"" ,T0.""U_Z_PerTo"",T1.U_Z_Empid from ""@Z_HR_PERAPP"" T0 JOIN [@Z_HR_OSEAPP] T1 on T1.U_Z_Period=T0.U_Z_PerCode where T1.DocEntry='" & DocEntry & "'")
                Dim strPeriod As String = "Period From: " & oRecordSet.Fields.Item("U_Z_PerFrom").Value & "    " & "Period To: " & oRecordSet.Fields.Item("U_Z_PerTo").Value
                strQuery = "SELECT isnull(T1.firstName,'') +' '+ isnull(T1.lastName,'') as 'EmpName',T1.userId from OHEM T0 JOIN OHEM T1 ON T0.Manager=T1.empID where T0.empID IN (SELECT manager FROM OHEM WHERE empID =" & oRecordSet.Fields.Item("U_Z_EmpId").Value & ")"
                oTemp.DoQuery(strQuery)
                If oTemp.RecordCount > 0 Then
                    oTest.DoQuery("Select * from [@Z_HR_OWEB]")
                    Dim strESSLink As String = ""
                    If oTest.RecordCount > 0 Then
                        strESSLink = oTest.Fields.Item("U_Z_WebPath").Value
                    End If

                    strMessage = "Appraisal Document No : " & DocEntry & ", Employee Name : " & Name & ",Appraisal Period on " & strPeriod & ""
                    mail.Subject = strMessage ' "Appraisal Initialized Notification for " & Period & " on " & System.DateTime.Now.ToShortDateString()
                    mail.Body = BuildHtmBody(DocEntry, oTemp.Fields.Item(0).Value, "Appraisal", mType, SAPCompany, strMessage)
                    ' mail.Attachments.Add(New Net.Mail.Attachment(path))
                    SendMessageAppraisal("Second Level manager Appraisal Approval", strMessage, oTemp.Fields.Item(1).Value, SAPCompany)
                End If
            ElseIf mType = "HA" Then
                oRecordSet.DoQuery("Select T0.""U_Z_PerFrom"" ,T0.""U_Z_PerTo"",T1.U_Z_Empid from ""@Z_HR_PERAPP"" T0 JOIN [@Z_HR_OSEAPP] T1 on T1.U_Z_Period=T0.U_Z_PerCode where T1.DocEntry='" & DocEntry & "'")
                Dim strPeriod As String = "Period From: " & oRecordSet.Fields.Item("U_Z_PerFrom").Value & "    " & "Period To: " & oRecordSet.Fields.Item("U_Z_PerTo").Value

                strQuery = "SELECT T1.email,(Select isnull(firstName,'') +' '+ isnull(lastName,'') from OHEM where empid=" & oRecordSet.Fields.Item("U_Z_Empid").Value & ") as 'EmpName',T1.userId from OHEM T0 JOIN OHEM T1 ON T0.Manager=T1.empID where T0.empID IN (SELECT manager FROM OHEM WHERE empID =" & oRecordSet.Fields.Item("U_Z_Empid").Value & ")"
                oTemp.DoQuery(strQuery)
                If oTemp.RecordCount > 0 Then
                    ' mail.To.Add(oTemp.Fields.Item(0).Value)
                    oTest.DoQuery("Select * from [@Z_HR_OWEB]")
                    Dim strESSLink As String = ""
                    If oTest.RecordCount > 0 Then
                        strESSLink = oTest.Fields.Item("U_Z_WebPath").Value
                    End If

                    strMessage = "Appraisal Document No : " & DocEntry & ", Employee Name : " & Name & ",Appraisal Period on " & strPeriod & ""
                    mail.Subject = strMessage
                    mail.Body = BuildHtmBody(DocEntry, Name, "Appraisal", mType, SAPCompany, strMessage)
                    ' mail.Attachments.Add(New Net.Mail.Attachment(path))
                    ' SendMessageAppraisal("HR Appraisal Approval Notification", strMessage, oTemp.Fields.Item(2).Value, SAPCompany)
                End If
            End If
            SmtpServer.Send(mail)
            dbcon.strmsg = "Success"
        Catch ex As Exception
            dbcon.strmsg = ex.Message
        Finally
            mail.Dispose()
        End Try
        Return dbcon.strmsg
    End Function
    Private Sub SendMessageAppraisal(ByVal strSubject As String, ByVal strmessage As String, ByVal aUser As String, ByVal SAPCompany As SAPbobsCOM.Company)
        Dim oCmpSrv As SAPbobsCOM.CompanyService
        Dim oMessageService As SAPbobsCOM.MessagesService
        Dim oMessage As SAPbobsCOM.Message
        Dim oRecipientCollection As SAPbobsCOM.RecipientCollection
        oCmpSrv = SAPCompany.GetCompanyService()
        oMessageService = oCmpSrv.GetBusinessService(SAPbobsCOM.ServiceTypes.MessagesService)
        oMessage = oMessageService.GetDataInterface(SAPbobsCOM.MessagesServiceDataInterfaces.msdiMessage)
        oRecordSet = SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        oMessage.Subject = strSubject
        oMessage.Text = strmessage
        oRecipientCollection = oMessage.RecipientCollection
        oRecipientCollection.Add()
        oRecipientCollection.Item(0).SendInternal = SAPbobsCOM.BoYesNoEnum.tYES
        If aUser <> "" Then
            oRecordSet.DoQuery("Select * from OUSR where USERID='" & aUser & "'")
            oRecipientCollection.Item(0).UserCode = oRecordSet.Fields.Item("USER_CODE").Value
        Else
            oRecipientCollection.Item(0).UserCode = SAPCompany.UserName
        End If
        oMessageService.SendMessage(oMessage)
    End Sub
    Private Function BuildHtmBody(ByVal DocEntry As String, ByVal Name As String, ByVal type As String, ByVal mtype As String, ByVal SAPCompany As SAPbobsCOM.Company, Optional ByVal strMessage As String = "")
        Dim oHTML As String = ""
        Dim strCompany, sQuery As String
        Dim strName As String
        Dim Address1, Address2, Mail As String
        Dim oRecordSet As SAPbobsCOM.Recordset
        oRecordSet = SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        If type = "Appraisal" Then
            oHTML = GetFileContents(Server.MapPath("~/Appraisal.htm"))
        ElseIf type = "Agenda" Then
            oHTML = GetFileContents("Agenda.htm")
        End If

        sQuery = " Select CompnyName,CompnyAddr,Country,PrintHeadr,Phone1,E_Mail From OADM"
        oRecordSet = SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        oRecordSet.DoQuery(sQuery)

        If Not oRecordSet.EoF Then
            strCompany = oRecordSet.Fields.Item("CompnyName").Value
            strName = oRecordSet.Fields.Item("PrintHeadr").Value
            Address1 = oRecordSet.Fields.Item("CompnyAddr").Value
            Address2 = oRecordSet.Fields.Item("Country").Value
            Mail = oRecordSet.Fields.Item("E_Mail").Value
        End If


        If Not IsDBNull(Name) Then
            oHTML = oHTML.Replace("$$EmpName$$", Name)
        Else
            oHTML = oHTML.Replace("$$EmpName$$", "")
        End If

        If Not IsDBNull(strName) Then
            oHTML = oHTML.Replace("$$Company$$", strName)
        Else
            oHTML = oHTML.Replace("$$Company$$", "")
        End If

        If Not IsDBNull(Address1) Then
            oHTML = oHTML.Replace("$$Address1$$", Address1)
        Else
            oHTML = oHTML.Replace("$$Address1$$", "")
        End If

        If Not IsDBNull(Address2) Then
            oHTML = oHTML.Replace("$$Address2$$", Address2)
        Else
            oHTML = oHTML.Replace("$$Address2$$", "")
        End If

        If Not IsDBNull(Mail) Then
            oHTML = oHTML.Replace("$$Mail$$", Mail)
        Else
            oHTML = oHTML.Replace("$$Mail$$", "")
        End If


        If mtype = "AI" Then
            oHTML = oHTML.Replace("$$Comments$$", "Appraisal Process Initiated...")
            oHTML = oHTML.Replace("$$Messages$$", strMessage)
        ElseIf mtype = "SF" Then
            oHTML = oHTML.Replace("$$Comments$$", "First Level manager Appraisal Approval Notification")
            oHTML = oHTML.Replace("$$Messages$$", strMessage)
        ElseIf mtype = "LA" Then
            oHTML = oHTML.Replace("$$Comments$$", "Second Level manager Appraisal Approval Notification")
            oHTML = oHTML.Replace("$$Messages$$", strMessage)
        ElseIf mtype = "HA" Then
            oHTML = oHTML.Replace("$$Comments$$", "HR Appraisal Approval Notification...")
            oHTML = oHTML.Replace("$$Messages$$", strMessage)
        ElseIf mtype = "EN" Then
            oHTML = oHTML.Replace("$$Comments$$", "Appraisal Approval finished Notification...")
            oHTML = oHTML.Replace("$$Messages$$", strMessage)
        End If
        Return oHTML
    End Function
    Public Function GetFileContents(ByVal FullPath As String, _
      Optional ByRef ErrInfo As String = "") As String

        Dim strContents As String
        Dim objReader As StreamReader
        Try
            objReader = New StreamReader(FullPath)
            strContents = objReader.ReadToEnd()
            objReader.Close()
            Return strContents
        Catch Ex As Exception
            ErrInfo = Ex.Message
        End Try
    End Function
End Class