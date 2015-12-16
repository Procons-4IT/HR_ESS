Imports System
Imports System.Text
Imports System.Globalization
Imports EN
Imports DataAccess
Imports BusinessLogic
Public Class Emprofile
    Inherits System.Web.UI.Page
    Dim objEN As EmployeeProfileEN = New EmployeeProfileEN()
    Dim objBL As EmployeeProfileBL = New EmployeeProfileBL()
    Dim dbcon As DBConnectionDA = New DBConnectionDA()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Session("UserCode") Is Nothing Then
                Response.Redirect("../Login.aspx?sessionExpired=true", True)
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
            objEN.EmpId = Session("UserCode").ToString()
            LoadDatasource(objEN)
            txtempno.Text = objEN.EmpId
            'EmptyGrid()
        End If
    End Sub
    'Public Function GetEducation(ByVal objen As EmployeeProfileEN) As DataTable
    '    dbcon.dt = objBL.FillEducation(objen)
    '    Return dbcon.dt
    'End Function
    Private Sub LoadDatasource(ByVal objen As EmployeeProfileEN)

        dbcon.dss1 = objBL.BindCountry(objen)
        If dbcon.dss1.Tables(0).Rows.Count > 0 Then
            ddlHCountry.DataValueField = "Code"
            ddlHCountry.DataTextField = "Name"
            ddlHCountry.DataSource = dbcon.dss1.Tables(0)
            ddlHCountry.DataBind()
            ddlHCountry.Items.Insert(0, "---Select---")
            ddlWCountry.DataValueField = "Code"
            ddlWCountry.DataTextField = "Name"
            ddlWCountry.DataSource = dbcon.dss1.Tables(0)
            ddlWCountry.DataBind()
            ddlWCountry.Items.Insert(0, "---Select---")
            ddlcouofbirth.DataValueField = "Code"
            ddlcouofbirth.DataTextField = "Name"
            ddlcouofbirth.DataSource = dbcon.dss1.Tables(0)
            ddlcouofbirth.DataBind()
            ddlcouofbirth.Items.Insert(0, "---Select---")
            ddlcitizenship.DataValueField = "Code"
            ddlcitizenship.DataTextField = "Name"
            ddlcitizenship.DataSource = dbcon.dss1.Tables(0)
            ddlcitizenship.DataBind()
            ddlcitizenship.Items.Insert(0, "---Select---")
        Else
            ddlHCountry.DataBind()
            ddlWCountry.DataBind()
            ddlcouofbirth.DataBind()
            ddlcitizenship.DataBind()
        End If

        dbcon.ds4 = objBL.FillEducation(objen)
        If dbcon.ds4.Tables(0).Rows.Count > 0 Then
            grdedutype.DataSource = dbcon.ds4.Tables(0)
            grdedutype.DataBind()
        Else
            grdedutype.DataBind()
        End If

        dbcon.dss4 = objBL.BindPersonelDetails(objen)
        If dbcon.dss4.Tables(0).Rows.Count > 0 Then
            lbltano.Text = dbcon.dss4.Tables(0).Rows(0)("TAEmpID").ToString()
            lbliban.Text = dbcon.dss4.Tables(0).Rows(0)("U_Z_IBAN").ToString()
            txtgovid.Text = dbcon.dss4.Tables(0).Rows(0)("govID").ToString()

            lblAccno.Text = dbcon.dss4.Tables(0).Rows(0)("bankAcount").ToString()
            lblbankbranch.Text = dbcon.dss4.Tables(0).Rows(0)("bankBranch").ToString()
            lblrelation.Text = dbcon.dss4.Tables(0).Rows(0)("U_Z_Rel_Name").ToString()
            lblreltype.Text = dbcon.dss4.Tables(0).Rows(0)("U_Z_Rel_Type").ToString()
            lblemgphone.Text = dbcon.dss4.Tables(0).Rows(0)("U_Z_Rel_Phone").ToString()
            lbllocCode.Text = dbcon.dss4.Tables(0).Rows(0)("U_Z_LocCode").ToString()
            lbllocName.Text = dbcon.dss4.Tables(0).Rows(0)("U_Z_LocName").ToString()
            lbllvlcode.Text = dbcon.dss4.Tables(0).Rows(0)("U_Z_LvlCode").ToString()
            lbllvlName.Text = dbcon.dss4.Tables(0).Rows(0)("U_Z_LvlName").ToString()
            txtWBlock.Text = dbcon.dss4.Tables(0).Rows(0)("workBlock").ToString()
            txtWbuild.Text = dbcon.dss4.Tables(0).Rows(0)("WorkBuild").ToString()
            txtWCity.Text = dbcon.dss4.Tables(0).Rows(0)("workCity").ToString()
            txtWCounty.Text = dbcon.dss4.Tables(0).Rows(0)("workCounty").ToString()
            txtWStreet.Text = dbcon.dss4.Tables(0).Rows(0)("workStreet").ToString()
            txtWZipcode.Text = dbcon.dss4.Tables(0).Rows(0)("workZip").ToString()
            txtHBlock.Text = dbcon.dss4.Tables(0).Rows(0)("homeBlock").ToString()
            txtHCity.Text = dbcon.dss4.Tables(0).Rows(0)("homeCity").ToString()
            txtHCounty.Text = dbcon.dss4.Tables(0).Rows(0)("homeCounty").ToString()
            txtHStreet.Text = dbcon.dss4.Tables(0).Rows(0)("homeStreet").ToString()
            txtHZipcode.Text = dbcon.dss4.Tables(0).Rows(0)("homeZip").ToString()
            txtHbuild.Text = dbcon.dss4.Tables(0).Rows(0)("HomeBuild").ToString()
            txtorgcode.Text = dbcon.dss4.Tables(0).Rows(0)("U_Z_HR_OrgstCode").ToString()
            txtorgname.Text = dbcon.dss4.Tables(0).Rows(0)("U_Z_HR_OrgstName").ToString()
            txtappid.Text = dbcon.dss4.Tables(0).Rows(0)("U_Z_HR_ApplId").ToString()
            ddlmarstatus.SelectedValue = dbcon.dss4.Tables(0).Rows(0)("martStatus").ToString()
            ddlgender.SelectedValue = dbcon.dss4.Tables(0).Rows(0)("sex").ToString()
            If dbcon.dss4.Tables(0).Rows(0)("brthCountr").ToString() <> "" Then
                ddlcouofbirth.SelectedValue = dbcon.dss4.Tables(0).Rows(0)("brthCountr").ToString()
            End If
            If dbcon.dss4.Tables(0).Rows(0)("citizenshp").ToString() <> "" Then
                ddlcitizenship.SelectedValue = dbcon.dss4.Tables(0).Rows(0)("citizenshp").ToString()
            End If
            If dbcon.dss4.Tables(0).Rows(0)("workCountr").ToString() <> "" Then
                ddlWCountry.SelectedValue = dbcon.dss4.Tables(0).Rows(0)("workCountr").ToString()
            End If
            If dbcon.dss4.Tables(0).Rows(0)("homeCountr").ToString() <> "" Then
                ddlHCountry.SelectedValue = dbcon.dss4.Tables(0).Rows(0)("homeCountr").ToString()
            End If

            objen.WorkCountry = dbcon.dss4.Tables(0).Rows(0)("workCountr").ToString()
            If objen.WorkCountry <> "" Then
                FillWorkState(objen)
                If dbcon.dss4.Tables(0).Rows(0)("workState").ToString() <> "" Then
                    ddlWstate.SelectedValue = dbcon.dss4.Tables(0).Rows(0)("workState").ToString()
                End If
                'ddlWstate.SelectedIndex = ddlWstate.Items.IndexOf(ddlWstate.Items.FindByText(dbcon.dss4.Tables(0).Rows(0)("workState").ToString()))
            End If
            objen.HomeCountry = dbcon.dss4.Tables(0).Rows(0)("homeCountr").ToString()
            If objen.HomeCountry <> "" Then
                FillHomeState(objen)
                FillHomeBank(objen)
                If dbcon.dss4.Tables(0).Rows(0)("homeState").ToString() <> "" Then
                    ddlHState.SelectedValue = dbcon.dss4.Tables(0).Rows(0)("homeState").ToString()
                End If
                ' ddlHState.SelectedIndex = ddlHState.Items.IndexOf(ddlHState.Items.FindByText(dbcon.dss4.Tables(0).Rows(0)("homeState").ToString()))
            End If
            If dbcon.dss4.Tables(0).Rows(0)("bankCode").ToString() <> "" Then
                ddlbankname.SelectedValue = dbcon.dss4.Tables(0).Rows(0)("bankCode").ToString()
            End If
            'ddlbankname.SelectedIndex = ddlbankname.Items.IndexOf(ddlbankname.Items.FindByText(dbcon.dss4.Tables(0).Rows(0)("bankCode").ToString()))
            txtdob.Text = dbcon.dss4.Tables(0).Rows(0)("birthDate").ToString()
            txtnochildren.Text = dbcon.dss4.Tables(0).Rows(0)("nChildren").ToString()
            txtgovid.Text = dbcon.dss4.Tables(0).Rows(0)("govID").ToString()
            txtexpdate.Text = dbcon.dss4.Tables(0).Rows(0)("passportEx").ToString()
            txtpassno.Text = dbcon.dss4.Tables(0).Rows(0)("passportNo").ToString()
            txtFirstName.Text = dbcon.dss4.Tables(0).Rows(0)("firstName").ToString()
            txtlastname.Text = dbcon.dss4.Tables(0).Rows(0)("lastName").ToString()
            txtmiddleName.Text = dbcon.dss4.Tables(0).Rows(0)("middleName").ToString()
            txtthirdname.Text = dbcon.dss4.Tables(0).Rows(0)("U_Z_HR_ThirdName").ToString()
            If dbcon.dss4.Tables(0).Rows(0)("position").ToString() <> "" Then
                txtposition.Text = dbcon.dss4.Tables(0).Rows(0)("Positionname").ToString()
            End If
            If dbcon.dss4.Tables(0).Rows(0)("dept").ToString() <> "" Then
                txtdept.Text = dbcon.dss4.Tables(0).Rows(0)("Deptname").ToString()
            End If
            txtoffphone.Text = dbcon.dss4.Tables(0).Rows(0)("officeTel").ToString()
            txtmobile.Text = dbcon.dss4.Tables(0).Rows(0)("mobile").ToString()
            txtemail.Text = dbcon.dss4.Tables(0).Rows(0)("email").ToString()
            txtfax.Text = dbcon.dss4.Tables(0).Rows(0)("fax").ToString()
            txthometel.Text = dbcon.dss4.Tables(0).Rows(0)("homeTel").ToString()
            objen.Manager = dbcon.dss4.Tables(0).Rows(0)("Manager").ToString()
            If objen.Manager <> 0 Then
                dbcon.dss = objBL.Manager(objen)
                If dbcon.dss.Tables(0).Rows.Count <> 0 Then
                    txtmanager.Text = dbcon.dss.Tables(0).Rows(0)("ManName").ToString()
                End If
            End If
        End If
    End Sub
    Private Sub FillWorkState(ByVal objen As EmployeeProfileEN)
        Try

            dbcon.ds1 = objBL.WorkState(objen)
            If dbcon.ds1.Tables(0).Rows.Count > 0 Then
                ddlWstate.DataValueField = "Code"
                ddlWstate.DataTextField = "Name"
                ddlWstate.DataSource = dbcon.ds1.Tables(0)
                ddlWstate.DataBind()
                ddlWstate.Items.Insert(0, "Select")
            Else
                ddlWstate.Items.Clear()
                ddlWstate.DataBind()
                ddlWstate.Items.Insert(0, "Select")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub FillHomeState(ByVal objen As EmployeeProfileEN)
        Try

            dbcon.ds2 = objBL.HomeState(objen)
            If dbcon.ds2.Tables(0).Rows.Count > 0 Then
                ddlHState.DataValueField = "Code"
                ddlHState.DataTextField = "Name"
                ddlHState.DataSource = dbcon.ds2.Tables(0)
                ddlHState.DataBind()
                ddlHState.Items.Insert(0, "Select")
            Else
                ddlHState.Items.Clear()
                ddlHState.DataBind()
                ddlHState.Items.Insert(0, "Select")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub FillHomeBank(ByVal objen As EmployeeProfileEN)
        Try

            dbcon.dss2 = objBL.HomeBank(objen)
            If dbcon.dss2.Tables(0).Rows.Count > 0 Then
                ddlbankname.DataValueField = "BankCode"
                ddlbankname.DataTextField = "BankName"
                ddlbankname.DataSource = dbcon.dss2.Tables(0)
                ddlbankname.DataBind()
                ddlbankname.Items.Insert(0, "Select")
            Else
                ddlbankname.Items.Clear()
                ddlbankname.DataBind()
                ddlbankname.Items.Insert(0, "Select")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mess(ByVal str As String)
        ' Dim Updated As UpdatePanel = CType(Master.FindControl("Update"), UpdatePanel)
        ScriptManager.RegisterStartupScript(Me, [GetType](), "showalert", "alert('" & str & "');", True)
    End Sub
    Private Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit.Click

        If Session("UserCode") Is Nothing Or Session("SAPCompany") Is Nothing Then
            Response.Redirect("../Login.aspx?sessionExpired=true", True)
        Else
            If btnsubmit.Text = "Edit" Then
                EnableText("Edit")
                btnsubmit.Text = "Save"
            ElseIf btnsubmit.Text = "Save" Then
                Try
                    dbcon.objMainCompany = Session("SAPCompany")
                    Dim oEmployee As SAPbobsCOM.EmployeesInfo
                    oEmployee = dbcon.objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oEmployeesInfo)
                    If oEmployee.GetByKey(CInt(txtempno.Text.Trim())) Then
                        oEmployee.OfficePhone = txtoffphone.Text.Trim()
                        oEmployee.MiddleName = txtmiddleName.Text.Trim()
                        oEmployee.LastName = txtlastname.Text.Trim()
                        oEmployee.WorkStreet = txtWStreet.Text.Trim()
                        oEmployee.WorkBlock = txtWBlock.Text.Trim()
                        oEmployee.WorkBuildingFloorRoom = txtWbuild.Text.Trim()
                        oEmployee.WorkCity = txtWCity.Text.Trim()
                        If txtdob.Text <> "" Then
                            oEmployee.DateOfBirth = Date.ParseExact(txtdob.Text.Trim().Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture) ' txtdob.Text.Trim()
                        End If
                        If ddlWCountry.SelectedIndex <> 0 Then
                            oEmployee.WorkCountryCode = ddlWCountry.SelectedValue
                        End If
                        If ddlWstate.SelectedIndex <> 0 Then
                            oEmployee.WorkStateCode = ddlWstate.SelectedValue
                        End If
                        oEmployee.WorkZipCode = txtWZipcode.Text.Trim()
                        oEmployee.HomeStreet = txtHStreet.Text.Trim()
                        oEmployee.HomeBlock = txtHBlock.Text.Trim()
                        oEmployee.HomeBuildingFloorRoom = txtHbuild.Text.Trim()
                        oEmployee.HomeCity = txtHCity.Text.Trim()
                        If ddlHCountry.SelectedIndex <> 0 Then
                            oEmployee.HomeCountry = ddlHCountry.SelectedValue
                        End If
                        If ddlHState.SelectedIndex <> 0 Then
                            oEmployee.HomeState = ddlHState.SelectedValue
                        End If
                        oEmployee.HomeZipCode = txtHZipcode.Text.Trim()
                        If txtnochildren.Text <> "" Then
                            oEmployee.NumOfChildren = txtnochildren.Text.Trim()
                        End If

                        If ddlcitizenship.SelectedIndex <> 0 Then
                            oEmployee.CitizenshipCountryCode = ddlcitizenship.SelectedValue
                        End If
                        oEmployee.PassportNumber = txtpassno.Text.Trim()
                        If txtexpdate.Text <> "" Then
                            oEmployee.PassportExpirationDate = Date.ParseExact(txtexpdate.Text.Trim().Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture) 'txtexpdate.Text.Trim()
                        End If
                        oEmployee.eMail = txtemail.Text.Trim()
                        oEmployee.MobilePhone = txtmobile.Text.Trim()
                        oEmployee.Fax = txtfax.Text.Trim()
                        oEmployee.HomePhone = txthometel.Text.Trim()
                        oEmployee.HomeCounty = txtHCounty.Text.Trim()
                        oEmployee.WorkCounty = txtWCounty.Text.Trim()

                        If ddlgender.SelectedValue = "M" Then
                            oEmployee.Gender = SAPbobsCOM.BoGenderTypes.gt_Male
                        ElseIf ddlgender.SelectedValue = "F" Then
                            oEmployee.Gender = SAPbobsCOM.BoGenderTypes.gt_Female
                        Else
                            oEmployee.Gender = SAPbobsCOM.BoGenderTypes.gt_Undefined
                        End If

                        If ddlmarstatus.SelectedValue = "S" Then
                            oEmployee.MartialStatus = SAPbobsCOM.BoMeritalStatuses.mts_Single
                        ElseIf ddlmarstatus.SelectedValue = "M" Then
                            oEmployee.MartialStatus = SAPbobsCOM.BoMeritalStatuses.mts_Married
                        ElseIf ddlmarstatus.SelectedValue = "D" Then
                            oEmployee.MartialStatus = SAPbobsCOM.BoMeritalStatuses.mts_Divorced
                        Else
                            oEmployee.MartialStatus = SAPbobsCOM.BoMeritalStatuses.mts_Widowed
                        End If
                        If ddlcouofbirth.SelectedIndex <> 0 Then
                            oEmployee.CountryOfBirth = ddlcouofbirth.SelectedValue
                        End If

                        oEmployee.UserFields.Fields.Item("U_Z_Rel_Name").Value = lblrelation.Text.Trim()
                        oEmployee.UserFields.Fields.Item("U_Z_Rel_Type").Value = lblreltype.Text.Trim()
                        oEmployee.UserFields.Fields.Item("U_Z_Rel_Phone").Value = lblemgphone.Text.Trim()
                        oEmployee.UserFields.Fields.Item("U_Z_IBAN").Value = lbliban.Text.Trim()
                        oEmployee.IdNumber = txtgovid.Text.Trim()
                        If ddlbankname.SelectedIndex <> 0 Then
                            oEmployee.BankCode = ddlbankname.SelectedValue
                        End If
                        oEmployee.BankAccount = lblAccno.Text.Trim()
                        oEmployee.BankBranch = lblbankbranch.Text.Trim()

                        If oEmployee.Update() <> 0 Then
                            dbcon.strmsg = dbcon.objMainCompany.GetLastErrorDescription
                            mess(dbcon.strmsg)
                            EnableText("Edit")
                            btnsubmit.Text = "Save"
                        Else
                            dbcon.strmsg = "Employee profile updated successfully..."
                            mess(dbcon.strmsg)
                            EnableText("Save")
                            btnsubmit.Text = "Edit"
                        End If
                    End If
                Catch ex As Exception
                    dbcon.strmsg = "'" & ex.Message & "'"
                    mess(dbcon.strmsg)
            End Try
            End If
        End If
    End Sub
    Private Sub EnableText(ByVal aChoice As String)
        If aChoice = "Edit" Then
            grdedutype.Enabled = False
            'ButtonAdd.Visible = True
            ddlbankname.Enabled = False
            lblAccno.Enabled = True
            lblbankbranch.Enabled = True
            lblrelation.Enabled = True
            lblreltype.Enabled = True
            lblemgphone.Enabled = True
            lbllocCode.Enabled = True
            lbllocName.Enabled = True
            lbllvlcode.Enabled = True
            lbllvlName.Enabled = True
            txtWBlock.Enabled = True
            txtWbuild.Enabled = True
            txtWCity.Enabled = True
            txtWCounty.Enabled = True
            txtWStreet.Enabled = True
            txtWZipcode.Enabled = True
            txtHBlock.Enabled = True
            txtHCity.Enabled = True
            txtHCounty.Enabled = True
            txtHStreet.Enabled = True
            txtHZipcode.Enabled = True
            txtHbuild.Enabled = True
            txtorgcode.Enabled = True
            txtorgname.Enabled = True
            txtappid.Enabled = True
            ddlmarstatus.Enabled = False
            ddlgender.Enabled = False
            ddlcouofbirth.Enabled = True
            ddlcitizenship.Enabled = False
            ddlWCountry.Enabled = True
            ddlHCountry.Enabled = False
            ddlWstate.Enabled = True
            ddlHState.Enabled = True
            txtdob.Enabled = False
            txtnochildren.Enabled = True
            txtgovid.Enabled = True
            txtexpdate.Enabled = True
            txtpassno.Enabled = True
            txtFirstName.Enabled = True
            txtlastname.Enabled = True
            txtmiddleName.Enabled = True
            txtthirdname.Enabled = True
            txtoffphone.Enabled = True
            txtmobile.Enabled = True
            txtemail.Enabled = True
            txtfax.Enabled = True
            txthometel.Enabled = True
            lbliban.Enabled = True
        Else
            grdedutype.Enabled = False
            'ButtonAdd.Visible = False
            ddlbankname.Enabled = False
            lblAccno.Enabled = False
            lblbankbranch.Enabled = False
            lblrelation.Enabled = False
            lblreltype.Enabled = False
            lblemgphone.Enabled = False
            lbllocCode.Enabled = False
            lbllocName.Enabled = False
            lbllvlcode.Enabled = False
            lbllvlName.Enabled = False
            txtWBlock.Enabled = False
            txtWbuild.Enabled = False
            txtWCity.Enabled = False
            txtWCounty.Enabled = False
            txtWStreet.Enabled = False
            txtWZipcode.Enabled = False
            txtHBlock.Enabled = False
            txtHCity.Enabled = False
            txtHCounty.Enabled = False
            txtHStreet.Enabled = False
            txtHZipcode.Enabled = False
            txtHbuild.Enabled = False
            txtorgcode.Enabled = False
            txtorgname.Enabled = False
            txtappid.Enabled = False
            ddlmarstatus.Enabled = False
            ddlgender.Enabled = False
            ddlcouofbirth.Enabled = False
            ddlcitizenship.Enabled = False
            ddlWCountry.Enabled = False
            ddlHCountry.Enabled = False
            ddlWstate.Enabled = False
            ddlHState.Enabled = False
            txtdob.Enabled = False
            txtnochildren.Enabled = False
            txtgovid.Enabled = False
            txtexpdate.Enabled = False
            txtpassno.Enabled = False
            txtFirstName.Enabled = False
            txtlastname.Enabled = False
            txtmiddleName.Enabled = False
            txtthirdname.Enabled = False
            txtoffphone.Enabled = False
            txtmobile.Enabled = False
            txtemail.Enabled = False
            txtfax.Enabled = False
            txthometel.Enabled = False
            lbliban.Enabled = False
        End If
        'Dim dt As DataTable = New DataTable()
        'dt.Columns.Add("RequestCode", GetType(String))
        'dt.
    End Sub


    Private Sub ddlHCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlHCountry.SelectedIndexChanged
        If ddlHCountry.SelectedIndex <> 0 Then
            objEN.HomeCountry = ddlHCountry.SelectedValue
            FillHomeState(objEN)
            FillHomeBank(objEN)
        End If
    End Sub

    Private Sub ddlWCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlWCountry.SelectedIndexChanged
        If ddlWCountry.SelectedIndex <> 0 Then
            objEN.WorkCountry = ddlWCountry.SelectedValue
            FillWorkState(objEN)
        End If
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        EnableText("Cancel")
        btnsubmit.Text = "Edit"
        objEN.EmpId = txtempno.Text

        LoadDatasource(objEN)
        '  EmptyGrid()
    End Sub


    'Public Sub EmptyGrid()
    '    objEN.EmpId = txtempno.Text.Trim()

    '    Dim odt As DataTable = GetEducation(objEN)
    '    If odt.Rows.Count > 0 Then
    '        grdedutype.DataSource = odt
    '        grdedutype.DataBind()
    '        'Else
    '        '    Dim odr As DataRow
    '        '    odr = odt.NewRow()
    '        '    odt.Rows.Add(odr)
    '        '    grdedutype.DataSource = odt
    '        '    grdedutype.DataBind()
    '    End If

    'End Sub

    'Private Function EmptyRows(ByVal nofoRows As Integer, ByVal dt As DataTable) As DataTable
    '    Try
    '        Dim i As Integer
    '        For i = 0 To nofoRows - 1 Step i + 1
    '            Dim dr As DataRow = dt.NewRow()
    '            dr("line") = dt.Rows.Count + 1
    '            dr("fromDate") = String.Empty
    '            dr("toDate") = String.Empty
    '            dr("institute") = String.Empty
    '            dr("major") = String.Empty
    '            dr("diploma") = String.Empty
    '            dr("type") = "0"
    '            dr("empID") = txtempno.Text.Trim()
    '            dt.Rows.Add(dr)
    '        Next
    '        Return dt
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

    'Private Sub SaveGridDataInTempTable()
    '    Dim dtTemp As DataTable = New DataTable()
    '    If dtTemp Is Nothing Or dtTemp.Columns.Count <= 0 Then
    '        dtTemp.Columns.Add("fromDate")
    '        dtTemp.Columns.Add("toDate")
    '        dtTemp.Columns.Add("institute")
    '        dtTemp.Columns.Add("major")
    '        dtTemp.Columns.Add("diploma")
    '        dtTemp.Columns.Add("type")
    '        dtTemp.Columns.Add("line")
    '        dtTemp.Columns.Add("empID")
    '    End If
    '    Dim gvRow As GridViewRow
    '    For Each gvRow In grdedutype.Rows
    '        Dim drTemp As DataRow = dtTemp.NewRow()
    '        'drTemp("line") = Integer.Parse(grdedutype.DataKeys(gvRow.RowIndex)("line").ToString())
    '        drTemp("fromDate") = (CType(gvRow.FindControl("lbldtfrom"), TextBox)).Text.Trim()
    '        drTemp("toDate") = (CType(gvRow.FindControl("lbldtTo"), TextBox)).Text.Trim()
    '        drTemp("institute") = (CType(gvRow.FindControl("lblCWeight"), TextBox)).Text.Trim()
    '        drTemp("major") = (CType(gvRow.FindControl("lblmajor"), TextBox)).Text.Trim()
    '        drTemp("diploma") = (CType(gvRow.FindControl("lblDiploma"), TextBox)).Text.Trim()
    '        drTemp("type") = (CType(gvRow.FindControl("lbledutype"), DropDownList)).SelectedValue.ToString()
    '        dtTemp.Rows.Add(drTemp)
    '    Next
    '    Temp = dtTemp
    'End Sub

    'Public Property Temp() As DataTable
    '    Get
    '        Dim o As Object = ViewState("Temp")
    '        If o Is Nothing Then
    '            Dim dt As DataTable = New DataTable()
    '            Return dt
    '        Else
    '            Return CType(o, DataTable)
    '        End If
    '    End Get
    '    Set(ByVal Value As DataTable)
    '        ViewState("Temp") = value
    '    End Set
    'End Property


    'Private Sub ButtonAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonAdd.Click
    '    SaveGridDataInTempTable()
    '    Dim dtTemp As DataTable = Temp.Copy()
    '    Dim insert As Boolean = True
    '    Dim dr As DataRow
    '    For Each dr In dtTemp.Rows
    '        If dr("line").ToString = Nothing Or dr("fromDate").ToString = Nothing Or dr("toDate").ToString = Nothing Or dr("institute").ToString = Nothing Or dr("major").ToString = Nothing Or dr("diploma").ToString = Nothing Then
    '            insert = False
    '        End If
    '    Next
    '    EmptyRows(1, dtTemp)
    '    Temp = dtTemp
    '    grdedutype.DataSource = dtTemp
    '    grdedutype.DataBind()
    'End Sub
End Class