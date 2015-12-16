Imports System
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Xml
Imports System.IO
Imports System.Net
Imports DataAccess
Imports BusinessLogic
Imports EN
Public Class EmpReqtoHR
    Inherits System.Web.UI.Page
    Dim objEN As ActivityEN = New ActivityEN()
    Dim objVar As DBConnectionDA = New DBConnectionDA()
    Dim objBL As ActivityBL = New ActivityBL()
    Public Sub New()
        objVar.con = New SqlConnection(objVar.GetConnection)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Session("UserCode") Is Nothing Then
                Response.Redirect("../Login.aspx?sessionExpired=true", True)
            ElseIf Session("SAPCompany") Is Nothing Then
                If Session("EmpUserName").ToString() = "" Or Session("UserPwd").ToString() = "" Then
                    strError = objVar.Connection()
                Else
                    strError = objVar.Connection(Session("EmpUserName").ToString(), Session("UserPwd").ToString())
                End If
                If strError <> "Success" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "js", "<script>alert('" & strError & "')</script>")
                Else
                    Session("SAPCompany") = objVar.objMainCompany
                End If
            End If
            panelview.Visible = True
            panelnew.Visible = False
            PanelNewRequest.Visible = False

            objEN.EmpId = Session("UserCode").ToString()
            LoadDatasource(objEN)
        End If
    End Sub
    Private Sub LoadDatasource(ByVal objEN As ActivityEN)
        Try
            objVar.ds = objBL.ActivityType(objEN)
            If objVar.ds.Tables(0).Rows.Count > 0 Then
                ddltype.DataTextField = "Name"
                ddltype.DataValueField = "Code"
                ddltype.DataSource = objVar.ds.Tables(0)
                ddltype.DataBind()
            Else
                ddltype.DataBind()
            End If

            objEN.ActType = ddltype.SelectedValue
            objVar.ds1 = objBL.ActivitySubject(objEN)
            If objVar.ds1.Tables(0).Rows.Count > 0 Then
                ddlSubject.DataTextField = "Name"
                ddlSubject.DataValueField = "Code"
                ddlSubject.DataSource = objVar.ds1.Tables(0)
                ddlSubject.DataBind()
            Else
                ddlSubject.DataBind()
            End If

            objVar.ds2 = objBL.ActivityStatus(objEN)
            If objVar.ds2.Tables(0).Rows.Count > 0 Then
                ddlStatus.DataTextField = "name"
                ddlStatus.DataValueField = "statusID"
                ddlStatus.DataSource = objVar.ds2.Tables(0)
                ddlStatus.DataBind()
            Else
                ddlStatus.DataBind()
            End If

            objVar.ds4 = objBL.LoadActivity(objEN)
            If objVar.ds4.Tables(0).Rows.Count > 0 Then
                grdRequesttohr.DataSource = objVar.ds4.Tables(0)
                grdRequesttohr.DataBind()
            Else
                grdRequesttohr.DataBind()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub mess(ByVal str As String)
        ' Dim Updated As UpdatePanel = CType(Master.FindControl("Update"), UpdatePanel)
        'ScriptManager.RegisterStartupScript(Me, [GetType](), "strmsg", str, True)
        ScriptManager.RegisterStartupScript(Me, [GetType](), "showalert", "alert('" & str & "');", True)
        'Page.RegisterStartupScript("msg", "<script>alert('" & str & "')</script>")
    End Sub
    Private Sub btnnew_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnnew.Click
        If Session("UserCode") Is Nothing Or Session("SAPCompany") Is Nothing Then
            Response.Redirect("../Login.aspx?sessionExpired=true", True)
        Else
            btnAdd.Text = "Save"

            objEN.EmpId = Session("UserCode").ToString()
            BindEmployee(objEN)
            objEN.Content = objBL.GetDeptHR(objEN)
            If objEN.Content = "0" Or objEN.Content = "" Then
                objVar.strmsg = "You are not mapped with HR...."
                mess(objVar.strmsg)
                panelview.Visible = True
                panelnew.Visible = False
                PanelNewRequest.Visible = False
            Else
                ViewState("DeptHRID") = objEN.Content
                objVar.ds3 = objBL.ActivityEmployee(objEN)
                If objVar.ds3.Tables(0).Rows.Count > 0 Then
                    ddlAssaigned.Text = objVar.ds3.Tables(0).Rows(0)("Code").ToString
                    txtname.Text = objVar.ds3.Tables(0).Rows(0)("Name").ToString
                End If
                objVar.strQuery2 = objBL.GetUser(objEN)
                If objVar.strQuery2 = "0" Or objVar.strQuery2 = "" Then
                    ddlemp.SelectedValue = "E"
                Else
                    ddlemp.SelectedValue = "U"
                End If
                lbldocnum.Text = objVar.Getmaxcode("OCLG", "ClgCode")
                panelview.Visible = False
                panelnew.Visible = True
                PanelNewRequest.Visible = True
                ddlActivity.SelectedValue = "T"
                ddlStatus.SelectedValue = "-2"
                txtstDate.Text = Date.Today.ToString("dd/MM/yyy")
                txtRemarks.Text = ""
                txtContent.Text = ""
            End If
        End If
    End Sub

    Private Sub BindEmployee(ByVal objen As ActivityEN)
        Try
            objVar.dss4 = objBL.BindPersonelDetails(objen)
            If objVar.dss4.Tables(0).Rows.Count > 0 Then
                lblsystemid.Text = objVar.dss4.Tables(0).Rows(0)("U_Z_EmpID").ToString
                txtempno.Text = objVar.dss4.Tables(0).Rows(0)("empID").ToString
                txtFirstName.Text = objVar.dss4.Tables(0).Rows(0)("EmpName").ToString
                txtposition.Text = objVar.dss4.Tables(0).Rows(0)("Positionname").ToString
                lblDept.Text = objVar.dss4.Tables(0).Rows(0)("Deptname").ToString
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click

        If Session("UserCode") Is Nothing Or Session("SAPCompany") Is Nothing Then
            Response.Redirect("../Login.aspx?sessionExpired=true", True)
        Else
            Try
                If btnAdd.Text = "Save" Then
                    If AddActivity() = "Success" Then
                        objVar.strmsg = "Activity created successfully..."
                        mess(objVar.strmsg)
                        MainGrid(objEN)
                        panelview.Visible = True
                        panelnew.Visible = False
                        PanelNewRequest.Visible = False
                    Else
                        mess(objVar.strmsg)
                        panelview.Visible = False
                        panelnew.Visible = True
                    End If
                Else
                    If UpdateActivity() = "Success" Then
                        objVar.strmsg = "Activity updated successfully..."
                        mess(objVar.strmsg)
                        MainGrid(objEN)
                        panelview.Visible = True
                        panelnew.Visible = False
                        PanelNewRequest.Visible = False
                    Else
                        mess(objVar.strmsg)
                    End If
                End If
            Catch ex As Exception
                mess(ex.Message)
            End Try
        End If
    End Sub
    Private Sub MainGrid(ByVal objen As ActivityEN)
        Try
            objen.EmpId = Session("UserCode").ToString()
            objVar.ds4 = objBL.LoadActivity(objen)
            If objVar.ds4.Tables(0).Rows.Count > 0 Then
                grdRequesttohr.DataSource = objVar.ds4.Tables(0)
                grdRequesttohr.DataBind()
            Else
                grdRequesttohr.DataBind()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Function UpdateActivity() As String
        Dim oRec As SAPbobsCOM.Recordset
        Try
            objEN.EmpId = Session("UserCode").ToString()
            objEN.StartDate = txtstDate.Text.Trim().Replace("-", "/")
            If objEN.StartDate <> "" Then
                objEN.FromDate = Date.ParseExact(objEN.StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) ' Date.Parse(objEN.StartDate)
            End If
            If txtRemarks.Text = "" Then
                objVar.strmsg = "Remarks must have value..."
            Else
                objEN.ActType = ddltype.SelectedValue
                objEN.Subject = ddlSubject.SelectedValue
                objEN.Status = ddlStatus.SelectedValue
                objEN.Priority = ddlpriority.SelectedValue
                objEN.Content = txtContent.Text.Trim()
                objVar.objMainCompany = Session("SAPCompany")
                oRec = objVar.objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

                Dim oAct As SAPbobsCOM.Contacts
                oAct = objVar.objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oContacts)

                If oAct.GetByKey(CInt(lbldocnum.Text.Trim())) Then
                    oAct.Activity = SAPbobsCOM.BoActivities.cn_Task
                    If objEN.ActType <> "" Then
                        oAct.ActivityType = objEN.ActType
                    End If
                    If objEN.Subject <> "" Then
                        oAct.Subject = objEN.Subject
                    End If
                    oAct.UserFields.Fields.Item("U_Z_HREmpID").Value = objEN.EmpId
                    oAct.UserFields.Fields.Item("U_Z_HREmpName").Value = txtFirstName.Text.Trim()
                    oAct.UserFields.Fields.Item("U_Z_HRSystemID").Value = lblsystemid.Text.Trim()
                    oAct.UserFields.Fields.Item("U_Z_AssEmpID").Value = ddlAssaigned.Text.Trim()
                    oAct.UserFields.Fields.Item("U_Z_ActType").Value = "D"
                    oAct.Personalflag = SAPbobsCOM.BoYesNoEnum.tYES
                    If ddlemp.SelectedValue = "U" Then
                        oAct.HandledBy = ddlAssaigned.Text.Trim()
                    End If
                    oAct.Details = txtRemarks.Text.Trim()
                    oAct.Priority = ddlpriority.SelectedValue
                    oAct.Status = ddlStatus.SelectedValue
                    oAct.StartDate = objEN.FromDate
                    oAct.EndDuedate = objEN.FromDate
                    oAct.HandledBy = objVar.objMainCompany.UserSignature
                    oAct.Notes = objEN.Content
                    If oAct.Update <> 0 Then
                        objVar.strmsg = objVar.objMainCompany.GetLastErrorDescription
                        Return objVar.strmsg
                    ElseIf ddlemp.SelectedValue = "E" Then
                        Dim strno As String
                        strno = "Update OCLG set AttendUser='',Attendempl='" & ddlAssaigned.Text.Trim() & "' where ClgCode=" & lbldocnum.Text.Trim() & ""
                        oRec.DoQuery(strno)
                        objEN.SAPCompany = objVar.objMainCompany
                        objEN.AUser = ViewState("DeptHRID").ToString()
                        objEN.AMessage = "The following activity No:" & lbldocnum.Text.Trim() & " needs your attention from " & txtFirstName.Text.Trim()
                        objVar.strmsg = objBL.SendMail_Approval(objEN)
                        objVar.strmsg = "Success"
                    ElseIf ddlemp.SelectedValue = "U" Then
                        objEN.SAPCompany = objVar.objMainCompany
                        objEN.AUser = ViewState("DeptHRID").ToString()
                        objEN.AMessage = "The following activity No:" & lbldocnum.Text.Trim() & " needs your attention from " & txtFirstName.Text.Trim()
                        objVar.strmsg = objBL.SendMail_Approval(objEN)
                        objVar.strmsg = "Success"
                    End If
                End If
            End If
        Catch ex As Exception
            objVar.strmsg = ex.Message
            Return objVar.strmsg
        End Try
        Return objVar.strmsg
    End Function
    Private Function AddActivity() As String
        Dim strno As String
        Dim oRec As SAPbobsCOM.Recordset
        Try
            objEN.EmpId = Session("UserCode").ToString()
            objEN.ActType = ddltype.SelectedValue
            objEN.Subject = ddlSubject.SelectedValue
            objEN.Assaignedemp = ddlAssaigned.Text.Trim()
            objEN.Remarks = txtRemarks.Text.Trim()
            objEN.StartDate = txtstDate.Text.Trim().Replace("-", "/")
            If objEN.StartDate <> "" Then
                objEN.FromDate = Date.ParseExact(objEN.StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) ' Date.Parse(objEN.StartDate)
            End If
            If txtRemarks.Text = "" Then
                objVar.strmsg = "Remarks must have value..."
            Else
                objEN.Status = ddlStatus.SelectedValue
                objEN.Priority = ddlpriority.SelectedValue
                objEN.Content = txtContent.Text.Trim()
                objVar.objMainCompany = Session("SAPCompany")
                oRec = objVar.objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                Dim oAct As SAPbobsCOM.Contacts
                oAct = objVar.objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oContacts)

                oAct.Activity = SAPbobsCOM.BoActivities.cn_Task
                If objEN.ActType <> "" Then
                    oAct.ActivityType = objEN.ActType
                End If
                If objEN.Subject <> "" Then
                    oAct.Subject = objEN.Subject
                End If
                oAct.UserFields.Fields.Item("U_Z_HREmpID").Value = objEN.EmpId
                oAct.UserFields.Fields.Item("U_Z_AssEmpID").Value = ddlAssaigned.Text.Trim()
                oAct.UserFields.Fields.Item("U_Z_HREmpName").Value = txtFirstName.Text.Trim()
                oAct.UserFields.Fields.Item("U_Z_HRSystemID").Value = lblsystemid.Text.Trim()
                oAct.UserFields.Fields.Item("U_Z_ActType").Value = "D"
                oAct.Personalflag = SAPbobsCOM.BoYesNoEnum.tYES

                If ddlemp.SelectedValue = "U" Then
                    oAct.HandledBy = ddlAssaigned.Text.Trim()
                End If
                oAct.Details = txtRemarks.Text.Trim()
                oAct.Priority = ddlpriority.SelectedValue
                oAct.Status = ddlStatus.SelectedValue
                oAct.StartDate = objEN.FromDate
                oAct.EndDuedate = objEN.FromDate
                oAct.Notes = objEN.Content
                If oAct.Add <> 0 Then
                    objVar.strmsg = objVar.objMainCompany.GetLastErrorDescription
                    Return objVar.strmsg
                ElseIf ddlemp.SelectedValue = "E" Then
                    objVar.objMainCompany.GetNewObjectCode(strno)
                    strno = "Update OCLG set AttendUser='',Attendempl='" & objEN.Assaignedemp & "' where ClgCode=" & strno & ""
                    oRec.DoQuery(strno)
                    objEN.SAPCompany = objVar.objMainCompany
                    objEN.AUser = ViewState("DeptHRID").ToString()
                    objEN.AMessage = "The following activity No:" & strno & " needs your attention from " & txtFirstName.Text.Trim()
                    objVar.strmsg = objBL.SendMail_Approval(objEN)
                ElseIf ddlemp.SelectedValue = "U" Then
                    objVar.objMainCompany.GetNewObjectCode(strno)
                    objEN.SAPCompany = objVar.objMainCompany
                    objEN.AUser = ViewState("DeptHRID").ToString()
                    objEN.AMessage = "The following activity No:" & strno & " needs your attention from " & txtFirstName.Text.Trim()
                    objVar.strmsg = objBL.SendMail_Approval(objEN)
                    objVar.strmsg = "Success"
                End If
            End If
        Catch ex As Exception
            objVar.strmsg = ex.Message
            Return objVar.strmsg
        End Try
        Return objVar.strmsg
    End Function

    Private Sub ddltype_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddltype.SelectedIndexChanged
        If ddltype.SelectedValue <> "" Then
            objEN.ActType = ddltype.SelectedValue
            BindSubject(objEN)
        End If
    End Sub
    Private Sub BindSubject(ByVal objen As ActivityEN)
        Try

            objVar.ds1 = objBL.ActivitySubject(objen)
            If objVar.ds1.Tables(0).Rows.Count > 0 Then
                ddlSubject.DataTextField = "Name"
                ddlSubject.DataValueField = "Code"
                ddlSubject.DataSource = objVar.ds1.Tables(0)
                ddlSubject.DataBind()
            Else
                ddlSubject.DataBind()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Protected Sub lbtndocnum_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try

            If Session("UserCode") Is Nothing Then
                objVar.strmsg = "alert('Your session is Expired...')"
                mess(objVar.strmsg)
                Response.Redirect("../Login.aspx?sessionExpired=true", True)
            Else
                Dim link As LinkButton = CType(sender, LinkButton)
                Dim gv As GridViewRow = CType((link.Parent.Parent), GridViewRow)
                Dim DocNo As LinkButton = CType(gv.FindControl("lbtndocnum"), LinkButton)
                objEN.DocNum = DocNo.Text.Trim()
                objEN.EmpId = Session("UserCode").ToString()
                objEN.Content = objBL.GetDeptHR(objEN)
                BindEmployee(objEN)
                PopulateActivity(objEN)
                ViewState("DeptHRID") = objEN.Content
                panelview.Visible = False
                panelnew.Visible = True
                PanelNewRequest.Visible = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub PopulateActivity(ByVal objen As ActivityEN)
        Try
            objVar.dss3 = objBL.PopulatedActivity(objen)
            If objVar.dss3.Tables(0).Rows.Count > 0 Then
                ddlActivity.SelectedValue = objVar.dss3.Tables(0).Rows(0)("Action").ToString()
                ddltype.SelectedValue = objVar.dss3.Tables(0).Rows(0)("CntctType").ToString()
                objen.ActType = ddltype.SelectedValue
                BindSubject(objen)
                If objVar.dss3.Tables(0).Rows(0)("CntctSbjct").ToString() <> "" Then
                    ddlSubject.SelectedValue = objVar.dss3.Tables(0).Rows(0)("CntctSbjct").ToString()
                End If
                lbldocnum.Text = objVar.dss3.Tables(0).Rows(0)("ClgCode").ToString()
                If objVar.dss3.Tables(0).Rows(0)("AttendEmpl").ToString() <> "0" Then
                    BindEmployee(objVar.dss3.Tables(0).Rows(0)("AttendEmpl").ToString())
                End If
                If objVar.dss3.Tables(0).Rows(0)("AttendUser").ToString() <> "0" Then
                    BindUser(objVar.dss3.Tables(0).Rows(0)("AttendUser").ToString())
                End If
                txtRemarks.Text = objVar.dss3.Tables(0).Rows(0)("Details").ToString()
                txtstDate.Text = objVar.dss3.Tables(0).Rows(0)("Recontact").ToString()
                ddlStatus.SelectedValue = objVar.dss3.Tables(0).Rows(0)("status").ToString()
                txtContent.Text = objVar.dss3.Tables(0).Rows(0)("Notes").ToString()
                ddlpriority.SelectedValue = objVar.dss3.Tables(0).Rows(0)("Priority").ToString()
                If ddlStatus.SelectedValue = "-2" Then
                    btnAdd.Visible = True
                    btnAdd.Text = "Update"
                Else
                    btnAdd.Visible = False
                    btnAdd.Text = "Save"
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub BindUser(ByVal strempid As String)
        Try
            objVar.strQuery = "Select ""INTERNAL_K"" as 'Code',""U_NAME"" as ""Name"" from OUSR where INTERNAL_K='" & strempid & "'"
            objVar.sqlda = New SqlDataAdapter(objVar.strQuery, objVar.con)
            objVar.sqlda.Fill(objVar.ds3)
            If objVar.ds3.Tables(0).Rows.Count > 0 Then
                ddlAssaigned.Text = objVar.ds3.Tables(0).Rows(0)("Code").ToString
                txtname.Text = objVar.ds3.Tables(0).Rows(0)("Name").ToString
                ddlemp.SelectedValue = "U"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub BindEmployee(ByVal strempid As String)
        Try
            objVar.strQuery = "Select ""empID"" as 'Code',""firstName"" +' ' + ISNULL(""middleName"",'') +' ' + ""lastName"" as ""Name"" from OHEM where empID='" & strEmpId & "'"
            objVar.sqlda = New SqlDataAdapter(objVar.strQuery, objVar.con)
            objVar.sqlda.Fill(objVar.ds3)
            If objVar.ds3.Tables(0).Rows.Count > 0 Then
                ddlAssaigned.Text = objVar.ds3.Tables(0).Rows(0)("Code").ToString
                txtname.Text = objVar.ds3.Tables(0).Rows(0)("Name").ToString
                ddlemp.SelectedValue = "E"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btncancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncancel.Click
        panelview.Visible = True
        panelnew.Visible = False
        PanelNewRequest.Visible = False
    End Sub
    Private Sub grdRequesttohr_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdRequesttohr.PageIndexChanging
        grdRequesttohr.PageIndex = e.NewPageIndex
        objEN.EmpId = Session("UserCode").ToString()
        LoadDatasource(objEN)
    End Sub
End Class