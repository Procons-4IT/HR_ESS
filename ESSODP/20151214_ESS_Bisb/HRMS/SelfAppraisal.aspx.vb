Imports System
Imports BusinessLogic
Imports DataAccess
Imports EN
Public Class SelfAppraisal
    Inherits System.Web.UI.Page
    Dim objEN As SelfAppraisalEN = New SelfAppraisalEN()
    Dim objSDA As SelfAppraisalBL = New SelfAppraisalBL()
    Dim objDA As SelfAppraisalDA = New SelfAppraisalDA()
    Dim dbCon As DBConnectionDA = New DBConnectionDA()
    Dim objCom As CommonFunctions = New CommonFunctions()
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
            objEN.AppraisalNumber = Request.QueryString("AppraisalNo")
            objEN.HomeEmpId = Request.QueryString("Empno")
            objEN.EmpId = Session("UserCode").ToString()
            ViewState("EmpId") = objEN.EmpId
            panelview.Visible = True
            panelnew.Visible = False
            btnnew.Visible = False
            BindPeriod()
            If objEN.AppraisalNumber <> "" And objEN.HomeEmpId <> "" Then
                mainGvbind(objEN)
                panelview.Visible = False
                panelnew.Visible = True
                PopulateAppraisal(objEN)
            Else
                mainGvbind(objEN)
            End If
        End If
    End Sub

    Private Sub BindPeriod()
        dbCon.ds1 = objSDA.BindPeriod()
        If dbCon.ds1.Tables(0).Rows.Count > 0 Then
            ddlperiod.DataTextField = "Name"
            ddlperiod.DataValueField = "Code"
            ddlperiod.DataSource = dbCon.ds1
            ddlperiod.DataBind()
            ddlperiod.Items.Insert(0, "--Select--")
        Else
            ddlperiod.DataBind()
            ddlperiod.Items.Insert(0, "--Select--")
        End If
    End Sub
    Private Sub mainGvbind(ByVal objen As SelfAppraisalEN)
        dbCon.ds = objSDA.mainGvbind(objen)
        If dbCon.ds.Tables(0).Rows.Count > 0 Then
            grdRequestApp.DataSource = dbCon.ds.Tables(0)
            grdRequestApp.DataBind()
        Else
            grdRequestApp.DataBind()
        End If
    End Sub
    Private Sub PopulateAppraisal(ByVal objen As SelfAppraisalEN)
        BusinessObjectve(objen)
        PeopleObjective(objen)
        CompetenceObjective(objen)
        HRFinalRating(objen)
        dbCon.dss = objSDA.PopulateAppraisal(objen)
        If dbCon.dss.Tables(0).Rows.Count <> 0 Then
            txtAppno.Text = dbCon.dss.Tables(0).Rows(0)("DocEntry").ToString()
            txtreqdate.Text = dbCon.dss.Tables(0).Rows(0)("U_Z_Date").ToString()
            txtempid.Text = dbCon.dss.Tables(0).Rows(0)("U_Z_EmpId").ToString()
            txtempname.Text = dbCon.dss.Tables(0).Rows(0)("U_Z_EmpName").ToString()
            ddlGstatus.SelectedValue = dbCon.dss.Tables(0).Rows(0)("U_Z_GStatus").ToString()
            remarks.Text = dbCon.dss.Tables(0).Rows(0)("U_Z_GRemarks").ToString()
            txtgrvdate.Text = dbCon.dss.Tables(0).Rows(0)("U_Z_GDate").ToString()
            txtgrvno.Text = dbCon.dss.Tables(0).Rows(0)("U_Z_GNo").ToString()
            ddlWstatus.SelectedIndex = ddlWstatus.Items.IndexOf(ddlWstatus.Items.FindByText(dbCon.dss.Tables(0).Rows(0)("U_Z_WStatus").ToString()))
            If dbCon.dss.Tables(0).Rows(0)("U_Z_Period").ToString() <> "" Then
                ddlperiod.SelectedItem.Text = dbCon.dss.Tables(0).Rows(0)("U_Z_Period").ToString()
            End If
            txtsBremarks.Text = dbCon.dss.Tables(0).Rows(0)("U_Z_BSelfRemark").ToString()
            txtsPremarks.Text = dbCon.dss.Tables(0).Rows(0)("U_Z_PSelfRemark").ToString()
            txtsCremarks.Text = dbCon.dss.Tables(0).Rows(0)("U_Z_CSelfRemark").ToString()

            txtBSrremarks.Text = dbCon.dss.Tables(0).Rows(0)("U_Z_BSMrRemark").ToString()
            txtPSrremarks.Text = dbCon.dss.Tables(0).Rows(0)("U_Z_PSMrRemark").ToString()
            txtCSrremarks.Text = dbCon.dss.Tables(0).Rows(0)("U_Z_CSMrRemark").ToString()

            txtBLmremarks.Text = dbCon.dss.Tables(0).Rows(0)("U_Z_BMgrRemark").ToString()
            txtPLmremarks.Text = dbCon.dss.Tables(0).Rows(0)("U_Z_PMgrRemark").ToString()
            txtCLmremarks.Text = dbCon.dss.Tables(0).Rows(0)("U_Z_CMgrRemark").ToString()

            txtCHrremarks.Text = dbCon.dss.Tables(0).Rows(0)("U_Z_CHrRemark").ToString()
            txtPHrremarks.Text = dbCon.dss.Tables(0).Rows(0)("U_Z_PHrRemark").ToString()
            txtBCHrremarks.Text = dbCon.dss.Tables(0).Rows(0)("U_Z_BHrRemark").ToString()


            EnableDisable(dbCon.dss.Tables(0).Rows(0)("U_Z_WStatus").ToString(), dbCon.dss.Tables(0).Rows(0)("U_Z_GStatus").ToString())
        End If

    End Sub
    Private Sub EnableDisable(ByVal strStatus As String, ByVal grvStatus As String)
        grdCompetenceview.Visible = True
        grdPeopleview.Visible = True
        grdBusinessView.Visible = True
        If (strStatus = "Draft" Or strStatus = "SelfApproved") Then
            ddlWstatus.Enabled = False
            ddlperiod.Enabled = False
            btnsubmit.Visible = True
            btnUpdate.Visible = False
            txtsBremarks.Enabled = True
            ddlGstatus.Visible = False
            txtsPremarks.Enabled = True
            txtsCremarks.Enabled = True
            btnsaveAcceptance.Visible = False
            btnsavesubmit.Visible = True
            app.Visible = False
            TabPanel5.Visible = False
            Dim row1, row2, row3 As GridViewRow
            For Each row3 In grdCompetenceview.Rows
                Dim strRate As DropDownList = CType(row3.FindControl("ddlcompselfrate"), DropDownList)
                strRate.Enabled = True
            Next
            For Each row1 In grdBusinessView.Rows
                Dim strRate1 As DropDownList = CType(row1.FindControl("ddlbusselfrate"), DropDownList)
                strRate1.Enabled = True
            Next
            For Each row2 In grdPeopleview.Rows
                Dim strRate2 As DropDownList = CType(row2.FindControl("ddlpeselfrate"), DropDownList)
                strRate2.Enabled = True
            Next
        ElseIf (strStatus = "LineManager Approved" Or strStatus = "Sr.Manager Approved") And grvStatus = "-" Then
            Dim row1, row2, row3 As GridViewRow
            ddlGstatus.Visible = True
            ddlGstatus.Enabled = True
            txtsBremarks.Enabled = False
            btnUpdate.Visible = False
            txtsPremarks.Enabled = False
            txtsCremarks.Enabled = False
            btnsaveAcceptance.Visible = True
            btnsavesubmit.Visible = False
            app.Visible = True
            TabPanel5.Visible = True
            btnsubmit.Visible = False
            txtgrvdate.Text = System.DateTime.Today.ToString("MM/dd/yyyy")
            txtgrvno.Text = objCom.Getmaxcode("[@Z_HR_OSEAPP]", "U_Z_GNo")
            For Each row3 In grdCompetenceview.Rows
                Dim strRate As DropDownList = CType(row3.FindControl("ddlcompselfrate"), DropDownList)
                strRate.Enabled = False
            Next
            For Each row1 In grdBusinessView.Rows
                Dim strRate1 As DropDownList = CType(row1.FindControl("ddlbusselfrate"), DropDownList)
                strRate1.Enabled = False
            Next
            For Each row2 In grdPeopleview.Rows
                Dim strRate2 As DropDownList = CType(row2.FindControl("ddlpeselfrate"), DropDownList)
                strRate2.Enabled = False
            Next
        Else
            btnsubmit.Visible = False
            btnUpdate.Visible = False
            btnsaveAcceptance.Visible = False
            btnsavesubmit.Visible = False
            ddlGstatus.Enabled = False
        End If
    End Sub
    Public Sub BusinessObjectve(ByVal objen As SelfAppraisalEN)
        dbCon.dss1 = objSDA.BusinessObjectve(objen)
        If dbCon.dss1.Tables(0).Rows.Count > 0 Then
            grdBusinessView.DataSource = dbCon.dss1
            grdBusinessView.DataBind()
        Else
            grdBusinessView.DataBind()
        End If
    End Sub
    Public Sub PeopleObjective(ByVal objen As SelfAppraisalEN)
        dbCon.dss2 = objSDA.PeopleObjective(objen)
        If dbCon.dss2.Tables(0).Rows.Count > 0 Then
            grdPeopleview.DataSource = dbCon.dss2
            grdPeopleview.DataBind()
        Else
            grdPeopleview.DataBind()
        End If
    End Sub
    Public Sub CompetenceObjective(ByVal objen As SelfAppraisalEN)
        dbCon.dss3 = objSDA.CompetenceObjective(objen)
        If dbCon.dss3.Tables(0).Rows.Count > 0 Then
            grdCompetenceview.DataSource = dbCon.dss3
            grdCompetenceview.DataBind()
        Else
            grdCompetenceview.DataBind()
        End If
    End Sub
    Public Sub HRFinalRating(ByVal objen As SelfAppraisalEN)
        dbCon.dss4 = objSDA.HRFinalRating(objen)
        If dbCon.dss4.Tables(0).Rows.Count > 0 Then
            grdfinalRate.DataSource = dbCon.dss4
            grdfinalRate.DataBind()
        Else
            grdfinalRate.DataBind()
        End If
    End Sub
    Protected Sub lbtndocnum_Click(ByVal sender As Object, ByVal e As EventArgs)
        System.Threading.Thread.Sleep(3000)
        If Session("UserCode") Is Nothing Or Session("SAPCompany") Is Nothing Then
            dbCon.strmsg = "alert('Your session is Expired...')"
            mess(dbCon.strmsg)
            Response.Redirect("Login.aspx?sessionExpired=true", True)
        Else
            Dim link As LinkButton = CType(sender, LinkButton)
            Dim gv As GridViewRow = CType((link.Parent.Parent), GridViewRow)
            Dim DocNo As LinkButton = CType(gv.FindControl("lbtndocnum"), LinkButton)
            panelview.Visible = False
            panelnew.Visible = True
            btnnew.Visible = False
            objEN.AppraisalNumber = DocNo.Text.Trim()
            objEN.EmpId = ViewState("EmpId").ToString()
            PopulateAppraisal(objEN)
        End If
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
    Protected Sub ddlbusselfrate_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dblVate, dblweight As Double
        Dim link As DropDownList = CType(sender, DropDownList)
        Dim gv As GridViewRow = CType((link.Parent.Parent), GridViewRow)
        Dim i As Integer = gv.RowIndex
        Dim ddl As DropDownList = DirectCast(grdBusinessView.Rows(i).FindControl("ddlbusselfrate"), DropDownList)
        Dim strweight As String = DirectCast(grdBusinessView.Rows(i).FindControl("lblweight1"), Label).Text
        Dim txtself As Label = DirectCast(grdBusinessView.Rows(i).FindControl("lblbusself1"), Label)
        objEN.Ratings = ddl.SelectedValue
        dbCon.ds = objDA.ddlChangedRating(objEN)
        If dbCon.ds.Tables(0).Rows.Count > 0 Then
            dblVate = dbCon.ds.Tables(0).Rows(0)("U_Z_Total")
            dblweight = CDbl(strweight)
            dblVate = dblVate * dblweight
            txtself.Text = dblVate / 100
        End If
    End Sub
    Protected Sub ddlpeselfrate_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dblVate, dblweight As Double
        Dim link As DropDownList = CType(sender, DropDownList)
        Dim gv As GridViewRow = CType((link.Parent.Parent), GridViewRow)
        Dim i As Integer = gv.RowIndex
        Dim ddl As DropDownList = DirectCast(grdPeopleview.Rows(i).FindControl("ddlpeselfrate"), DropDownList)
        Dim strweight As String = DirectCast(grdPeopleview.Rows(i).FindControl("lblpeweight1"), Label).Text
        Dim txtself As Label = DirectCast(grdPeopleview.Rows(i).FindControl("lblpeselfrate1"), Label)
       objEN.Ratings = ddl.SelectedValue
        dbCon.ds = objDA.ddlPeopleChangedRating(objEN)
        If dbCon.ds.Tables(0).Rows.Count > 0 Then
            dblVate = dbCon.ds.Tables(0).Rows(0)("U_Z_Total")
            dblweight = CDbl(strweight)
            dblVate = dblVate * dblweight
            txtself.Text = dblVate / 100
        End If
    End Sub
    Protected Sub ddlcompselfrate_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dblweight, dblExpectedLevel, dblFinalRate, dblSelfRate As Double
        Dim link As DropDownList = CType(sender, DropDownList)
        Dim gv As GridViewRow = CType((link.Parent.Parent), GridViewRow)
        Dim i As Integer = gv.RowIndex
        Dim ddl As DropDownList = DirectCast(grdCompetenceview.Rows(i).FindControl("ddlcompselfrate"), DropDownList)
        Dim strweight As String = DirectCast(grdCompetenceview.Rows(i).FindControl("lblCompWeight1"), Label).Text
        Dim lbllevels As Label = DirectCast(grdCompetenceview.Rows(i).FindControl("lblCompLevel"), Label)
        Dim lblcurrlevel As Label = DirectCast(grdCompetenceview.Rows(i).FindControl("lblCurrLevel"), Label)
        Dim txtself As Label = DirectCast(grdCompetenceview.Rows(i).FindControl("lblcompself1"), Label)
        objEN.Ratings = ddl.SelectedValue
        dbCon.ds = objDA.ddlPeopleChangedRating(objEN)
        If dbCon.ds.Tables(0).Rows.Count > 0 Then
            dblSelfRate = CDbl(dbCon.ds.Tables(0).Rows(0)("U_Z_Total"))
        Else
            dblSelfRate = 0
        End If
        dblweight = CDbl(strweight)

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

    Private Sub btnnew_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnnew.Click
        If Session("UserCode") Is Nothing Then
            Response.Redirect("Login.aspx?sessionExpired=true", True)
        Else
            objEN.EmpId = Session("UserCode").ToString()
            panelnew.Visible = True
            panelview.Visible = False
            BindPeriod()
            txtAppno.Text = objCom.Getmaxcode("[@Z_HR_OSEAPP]", "DocEntry")
            txtreqdate.Text = Now.Date
            PopulateEmployee(objEN)
            grdCompetenceview.Visible = False
            grdPeopleview.Visible = False
            ddlperiod.Enabled = True
            btnsubmit.Visible = True
            btnUpdate.Visible = False
        End If
    End Sub
    Private Sub PopulateEmployee(ByVal objen As SelfAppraisalEN)
        dbCon.ds = objSDA.PopulateEmployee(objen)
        If dbCon.ds.Tables(0).Rows.Count <> 0 Then
            txtempid.Text = dbCon.ds.Tables(0).Rows(0)("empID").ToString()
            txtempname.Text = dbCon.ds.Tables(0).Rows(0)("firstName").ToString()
        End If
    End Sub
    Private Sub mess(ByVal str As String)
        ScriptManager.RegisterStartupScript(Update, Update.[GetType](), "strmsg", dbCon.strmsg, True)
    End Sub
    Private Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit.Click

        If Session("UserCode") Is Nothing Or Session("SAPCompany") Is Nothing Then
            Response.Redirect("Login.aspx?sessionExpired=true", True)
        Else
            objEN.SapCompany = Session("SAPCompany")
            If AddUDOUpdate("Save", objEN.SapCompany) = "Success" Then
                dbCon.strmsg = "alert('Appraisal Request Approved successfully...')"
                mess(dbCon.strmsg)
                objEN.EmpId = ViewState("EmpId").ToString()
                mainGvbind(objEN)
                panelview.Visible = True
                panelnew.Visible = False
            Else
                dbCon.strmsg = "alert('" & dbCon.strmsg & "')"
                mess(dbCon.strmsg)
            End If
        End If
    End Sub
    Protected Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        panelview.Visible = True
        panelnew.Visible = False
        btnnew.Visible = False
    End Sub
    Private Function AddUDOUpdate(ByVal strchoice As String, ByVal SapCompany As SAPbobsCOM.Company) As String
        Try
            Dim row1, row2, row3 As GridViewRow
            dbCon.objMainCompany = SapCompany
            If dbCon.objMainCompany.InTransaction() Then
                dbCon.objMainCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit)
            End If
            dbCon.objMainCompany.StartTransaction()
            objEN.AppraisalNumber = txtAppno.Text.Trim()
            objEN.EmpId = ViewState("EmpId").ToString()
            objEN.BusinessRemarks = txtsBremarks.Text.Trim()
            objEN.PeopleRemarks = txtsPremarks.Text.Trim()
            objEN.CompRemarks = txtsCremarks.Text.Trim()
            If ddlWstatus.SelectedValue <> "HR" Then
                If strchoice = "SaveSubmit" Then
                    objEN.CheckStatus = "Y"
                    objEN.Status = "SE"
                Else
                    objEN.CheckStatus = "N"
                    objEN.Status = "DR"
                End If
                objDA.UpdateSelfAppHeader(objEN)
                For Each row1 In grdBusinessView.Rows
                    objEN.LineNo = CType(row1.FindControl("lblbusCode1"), Label).Text
                    objEN.Amount = CType(row1.FindControl("lblbusself1"), Label).Text
                    objEN.SelfRating = CType(row1.FindControl("ddlbusselfrate"), DropDownList).SelectedValue
                    objDA.UpdateSelfAppBusiness(objEN)
                Next row1

                For Each row2 In grdPeopleview.Rows
                    objEN.LineNo = CType(row2.FindControl("lblpecode1"), Label).Text
                    objEN.Amount = CType(row2.FindControl("lblpeselfrate1"), Label).Text
                    objEN.SelfRating = CType(row2.FindControl("ddlpeselfrate"), DropDownList).SelectedValue
                    objDA.UpdateSelfAppPeople(objEN)
                Next row2

                For Each row3 In grdCompetenceview.Rows
                    objEN.LineNo = CType(row3.FindControl("lblcompCode1"), Label).Text
                    objEN.Amount = CType(row3.FindControl("lblcompself1"), Label).Text
                    objEN.SelfRating = CType(row3.FindControl("ddlcompselfrate"), DropDownList).SelectedValue
                    objDA.UpdateSelfAppCompetence(objEN)
                Next row3
                objEN.Status = "SFA"
                dbCon.UpdateTimeStamp(objEN)
                If dbCon.objMainCompany.InTransaction() Then
                    dbCon.objMainCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit)
                End If
            End If
            If dbCon.objMainCompany.InTransaction() Then
                dbCon.objMainCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit)
            End If
        Catch ex As Exception
            If dbCon.objMainCompany.InTransaction() Then
                dbCon.objMainCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
            End If
            dbCon.strmsg = "alert('" & ex.Message & "')"
            Return dbCon.strmsg
        End Try
        Return "Success"
    End Function

    Protected Sub grdBusinessView_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdBusinessView.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim ddl As DropDownList = DirectCast(e.Row.FindControl("ddlbusselfrate"), DropDownList)
            Dim mgrddl As DropDownList = DirectCast(e.Row.FindControl("ddlbusmgrfrate"), DropDownList)
            Dim smgrddl As DropDownList = DirectCast(e.Row.FindControl("ddlsmgrfrate"), DropDownList)
            dbCon.Dropdown1("Select U_Z_RateCode As Code,U_Z_RateName As Name From [@Z_HR_ORATE]", "Code", "Name", ddl)
            dbCon.Dropdown1("Select U_Z_RateCode As Code,U_Z_RateName As Name From [@Z_HR_ORATE]", "Code", "Name", mgrddl)
            dbCon.Dropdown1("Select U_Z_RateCode As Code,U_Z_RateName As Name From [@Z_HR_ORATE]", "Code", "Name", smgrddl)
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
            dbCon.Dropdown1("Select U_Z_RateCode As Code,U_Z_RateName As Name From [@Z_HR_ORATE]", "Code", "Name", ddl)
            dbCon.Dropdown1("Select U_Z_RateCode As Code,U_Z_RateName As Name From [@Z_HR_ORATE]", "Code", "Name", mgrddl)
            dbCon.Dropdown1("Select U_Z_RateCode As Code,U_Z_RateName As Name From [@Z_HR_ORATE]", "Code", "Name", smgrddl)
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
            dbCon.Dropdown1("Select U_Z_RateCode As Code,U_Z_RateName As Name From [@Z_HR_ORATE]", "Code", "Name", ddl)
            dbCon.Dropdown1("Select U_Z_RateCode As Code,U_Z_RateName As Name From [@Z_HR_ORATE]", "Code", "Name", mgrddl)
            dbCon.Dropdown1("Select U_Z_RateCode As Code,U_Z_RateName As Name From [@Z_HR_ORATE]", "Code", "Name", smgrddl)
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

    Private Sub grdRequestApp_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdRequestApp.PageIndexChanging

        grdRequestApp.PageIndex = e.NewPageIndex
        objEN.EmpId = ViewState("EmpId").ToString()
        mainGvbind(objEN)
    End Sub

    Private Sub btnsavesubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsavesubmit.Click
        System.Threading.Thread.Sleep(3000)
        If Session("UserCode") Is Nothing Or Session("SAPCompany") Is Nothing Then
            Response.Redirect("Login.aspx?sessionExpired=true", True)
        Else
            objEN.SapCompany = Session("SAPCompany")
            objEN.EmpId = ViewState("EmpId").ToString()
            If AddUDOUpdate("SaveSubmit", objEN.SapCompany) = "Success" Then
                dbCon.strmsg = "alert('Appraisal Request Approved successfully...')"
                mess(dbCon.strmsg)
                mainGvbind(objEN)
                panelview.Visible = True
                panelnew.Visible = False
            Else
                dbCon.strmsg = "alert('" & dbCon.strmsg & "')"
                mess(dbCon.strmsg)
            End If
        End If
    End Sub

    Private Sub btnsaveAcceptance_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsaveAcceptance.Click
        System.Threading.Thread.Sleep(3000)
        If Session("UserCode") Is Nothing Or Session("SAPCompany") Is Nothing Then
            dbCon.strmsg = "alert('Your session is Expired...')"
            mess(dbCon.strmsg)
            Response.Redirect("Login.aspx?sessionExpired=true", True)
        Else
            Dim strGrvstatus As String
            strGrvstatus = ddlGstatus.SelectedValue
            objEN.EmpId = Session("UserCode").ToString()
            objEN.strGrvStaus = strGrvstatus
            objEN.AppraisalNumber = txtAppno.Text.Trim()
            If ddlGstatus.SelectedIndex <> 0 Then
                If strGrvstatus = "G" Then
                    If remarks.Text = "" Then
                        dbCon.strmsg = "alert('Enter Grievance Remarks...')"
                        mess(dbCon.strmsg)
                    Else
                        objEN.GrvNo = txtgrvno.Text.Trim()
                        objEN.GrvRemarks = remarks.Text.Trim()
                        objDA.UpdateGrievence(objEN)
                        dbCon.strmsg = "alert('Operation Completed Successfully...')"
                        mess(dbCon.strmsg)
                        mainGvbind(objEN)
                        panelview.Visible = True
                        panelnew.Visible = False
                    End If
                Else
                    objDA.UpdateGrievenceAccepted(objEN)
                    dbCon.strmsg = "alert('Operation Completed Successfully...')"
                    mess(dbCon.strmsg)
                    mainGvbind(objEN)
                    panelview.Visible = True
                    panelnew.Visible = False
                End If
            End If
        End If
    End Sub
End Class