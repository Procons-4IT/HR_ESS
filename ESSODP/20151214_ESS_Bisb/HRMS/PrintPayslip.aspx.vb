Imports System
Imports System.IO
Imports BusinessLogic
Imports DataAccess
Imports EN
Imports System.Data
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Globalization
Imports System.Web
Imports System.Collections
Imports System.ComponentModel
Imports System.Drawing

Imports System.Threading
Imports System.Collections.Generic
Public Class PrintPayslip
    Inherits System.Web.UI.Page

    Dim objEN As SelfAppraisalEN = New SelfAppraisalEN()
    Dim objSDA As SelfAppraisalBL = New SelfAppraisalBL()
    Dim objDA As PayslipDA = New PayslipDA()
    Dim dbCon As DBConnectionDA = New DBConnectionDA()
    Dim objCom As CommonFunctions = New CommonFunctions()
    Dim grdTotal As Decimal = 0
    Private ds As New dtPayroll      '(dataset)
    Private oDRow As DataRow
    Public Sub New()
        dbCon.con = New SqlConnection(dbCon.GetConnection)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Session("UserCode") Is Nothing Then
                Response.Redirect("Login.aspx?sessionExpired=true", True)
            Else
                objEN.AppraisalNumber = Request.QueryString("AppraisalNo")
                objEN.HomeEmpId = Request.QueryString("Empno")
                objEN.EmpId = Session("UserCode").ToString()
                Dim time As DateTime = DateTime.Now
                Dim amonth As ArrayList = New ArrayList()
                Dim aYear As ArrayList = New ArrayList()
              
                'For j As Integer = -2 To 0
                '    ddmonthyr.Items.Add(j.ToString())
                'Next
              

                Dim info As DateTimeFormatInfo = DateTimeFormatInfo.GetInstance(Nothing)
                For i As Integer = 1 To 12
                    ddlGstatus.Items.Add(New ListItem(info.GetMonthName(i), i.ToString()))
                Next
                For j As Integer = -2 To 0
                    Dim year As String = System.DateTime.Now.AddYears(j).ToString("yyyy")
                    ddmonthyr.Items.Add(year)
                Next
            End If
        End If
    End Sub
    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPrint.Click
        Try
            objEN.HomeEmpId = Session("UserCode").ToString()
            printPaySlip(ddlGstatus.SelectedValue, ddmonthyr.SelectedItem.Text, "Other")
            'Dim Crpt As New ReportDocument()
            'Crpt.Load(Server.MapPath("CReports\CRAppraisal.rpt"))
            'Dim dsreportdata As ReportData = GetData("SELECT * FROM  [@AZ_HR_OSEAPP] WHERE U_Z_Empid = '1'")
            'Crpt.SetDataSource(dsreportdata.Tables(1))
            'Dim mem As IO.MemoryStream = DirectCast(Crpt.ExportToStream(ExportFormatType.PortableDocFormat), IO.MemoryStream)
            'Response.Clear()
            'Response.Buffer = True
            'Response.BinaryWrite(mem.ToArray())
            'CrystalReportViewer1.ReportSource = Crpt
            
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub mess(ByVal str As String)
        ScriptManager.RegisterStartupScript(Update, Update.[GetType](), "strmsg", dbCon.strmsg, True)
    End Sub
    Public Sub printPaySlip(ByVal aMonth As Integer, ByVal aYear As Integer, ByVal aComp As String)
        Dim oRec, oRecBP, oTemp As SAPbobsCOM.Recordset
        Dim strPaySQL As String
       

        Dim intMonth, intYear As Integer
        Dim strCmpCode, strQuery As String
        Dim dblTotal As Double = 0
        intMonth = aMonth
        intYear = aYear
        strCmpCode = aComp
        Dim oCompany As SAPbobsCOM.Company
        Dim oConne As New DBConnectionDA
        If oConne.ConnectSAP() = True Then
            oCompany = New SAPbobsCOM.Company
            oCompany = oConne.objMainCompany
        End If

        oRec = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        oTemp = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        oRecBP = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

        oRec.DoQuery("Select isnull(U_Z_CompNo,'') from Ohem where empID=" & Session("UserCode").ToString())
        strCmpCode = oRec.Fields.Item(0).Value
        If strCmpCode = "" Then
            dbCon.strmsg = "alert('Your Not linked to payroll...')"
            mess(dbCon.strmsg)
        End If

        If 1 - 1 Then
            strQuery = "Select * from [@Z_PAYROLL] where U_Z_CompNo='" & strCmpCode & "' and  U_Z_OffCycle='Y' and  U_Z_Month=" & intMonth & " and U_Z_Year=" & intYear
            oRec.DoQuery(strQuery)
        Else
            strQuery = "Select * from [@Z_PAYROLL] where U_Z_CompNo='" & strCmpCode & "' and U_Z_OffCycle='N' and U_Z_Process='Y' and  U_Z_Month=" & intMonth & " and U_Z_Year=" & intYear
            oRec.DoQuery(strQuery)
        End If

        strQuery = "Select * from [@Z_PAYROLL] where U_Z_CompNo='" & strCmpCode & "' and U_Z_OffCycle='N' and U_Z_Process='Y' and  U_Z_Month=" & intMonth & " and U_Z_Year=" & intYear
        oRec.DoQuery(strQuery)

        If oRec.RecordCount <= 0 Then
            dbCon.strmsg = "alert('Payroll not generated for this selected period....')"
            mess(dbCon.strmsg)
            '   oApplication.Utilities.Message("Payroll not generated for selected month and year", SAPbouiCOM.BoStatusBarMessageType.smt_Error)
            Exit Sub
        Else
            ds.Clear()
            ds.Clear()
            oTemp.DoQuery("Select * from [@Z_PAYROLL1] where  U_Z_Posted='Y' and   U_Z_CompNo='" & strCmpCode & "' and  U_Z_RefCode='" & oRec.Fields.Item("Code").Value & "'  order by convert(numeric,U_Z_empid)")
            For introw As Integer = 0 To oTemp.RecordCount - 1
                oDRow = ds.Tables("PayHeader").NewRow()
                oDRow.Item("empID") = oTemp.Fields.Item("U_Z_empid").Value
                strPaySQL = "SELECT isnull(firstname,'') +' ' + isnull(lastname,'') from OHEM WHERE empID=" & oTemp.Fields.Item("U_Z_empid").Value
                oRecBP.DoQuery(strPaySQL)
                ' oDRow.Item("EmpName") = oTemp.Fields.Item("U_Z_EmpName").Value
                oDRow.Item("EmpName") = oRecBP.Fields.Item(0).Value
                oDRow.Item("Position") = oTemp.Fields.Item("U_Z_JobTitle").Value
                oDRow.Item("Month") = MonthName(intMonth)
                oDRow.Item("Year") = intYear
                strPaySQL = "SELECT isnull(T1.[BankName],'N/A'), isnull(T0.[bankAcount],'N/A') FROM OHEM T0  left outer JOIN ODSC T1 ON T0.bankCode = T1.BankCode WHERE empID=" & oTemp.Fields.Item("U_Z_empid").Value
                oRecBP.DoQuery(strPaySQL)

                oDRow.Item("Bank") = oRecBP.Fields.Item(0).Value
                oDRow.Item("AcctCode") = oRecBP.Fields.Item(1).Value
                oDRow.Item("JoiningDate") = oTemp.Fields.Item("U_Z_Startdate").Value
                'oDRow.Item("TerminationDate") = oRec.Fields.Item("U_Z_TernDate").Value
                oDRow.Item("Basic") = oTemp.Fields.Item("U_Z_MonthlyBasic").Value
                oDRow.Item("Earning") = oTemp.Fields.Item("U_Z_Earning").Value
                oDRow.Item("Deduction") = oTemp.Fields.Item("U_Z_Deduction").Value
                oDRow.Item("Net") = oTemp.Fields.Item("U_Z_NetSalary").Value

                Dim dblNetsalary As Double
                dblNetsalary = oTemp.Fields.Item("U_Z_NetSalary").Value
                '  dblCost = otemp3.Fields.Item(1).Value
                'strNet = oApplication.Utilities.SFormatNumber(dblNetsalary)
                '' strCost = oApplication.Utilities.SFormatNumber(dblCost)
                oDRow.Item("NetWord") = ""
                '  oDRow.Item("CostWord") = oTemp.Fields.Item("U_Z_CostSalaryWord").Value
                Dim oCurrencyRS As SAPbobsCOM.Recordset
                oCurrencyRS = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                oDRow.Item("Currency") = ""
                oRecBP.DoQuery("Select U_Name from ousr where User_Code='" & oCompany.UserName & "'")
                If oRecBP.RecordCount > 0 Then
                    oDRow.Item("PreparedBy") = oRecBP.Fields.Item(0).Value
                Else
                    oDRow.Item("PreparedBy") = ""
                End If
                ds.Tables("PayHeader").Rows.Add(oDRow)
                oDRow = ds.Tables("Earning").NewRow()
                oDRow.Item("empID") = oTemp.Fields.Item("U_Z_empid").Value
                oDRow.Item("EarningCode") = "Basic"
                oDRow.Item("EarningName") = "Basic"
                oDRow.Item("Earning") = oTemp.Fields.Item("U_Z_MonthlyBasic").Value
                oDRow.Item("Deduction") = 0
                ds.Tables("Earning").Rows.Add(oDRow)

                strPaySQL = "select x.Type,x.Field,X.FieldName,x.Earning,x.Deduction  from ( select 'A' 'Type' ,U_Z_Field 'Field',U_Z_FieldName 'FieldName',U_Z_Amount 'Earning',0 'Deduction' from [@Z_PAYROLL2] where U_Z_AMount>0 "
                strPaySQL = strPaySQL & " and U_Z_RefCode='" & oTemp.Fields.Item("Code").Value & "'  union all select 'A' 'Type' ,U_Z_Field 'Field',U_Z_FieldName 'FieldName',0 'Earning',U_Z_Amount 'Deduction' from [@Z_PAYROLL3] where U_Z_AMount>0 and U_Z_RefCode='" & oTemp.Fields.Item("Code").Value & "' ) as x order by x.Type"
                ' strPaySQL = " select * from ( select 'A' 'Type ,U_Z_Field,U_Z_FieldName,U_Z_Amount,0 from [@Z_PAYROLL2] where U_Z_AMount>0 and U_Z_RefCode='" & oRec.Fields.Item("Code").Value & "'  union all select 'B' Type,U_Z_Field,U_Z_FieldName,0,U_Z_Amount from [@Z_PAYROLL3] where U_Z_AMount>0 and U_Z_RefCode='" & oRec.Fields.Item("Code").Value & "') x order by x.Type"


                strPaySQL = "select x.Type,x.Field,X.FieldName,x.Earning,x.Deduction  from ( select 'A' 'Type' ,U_Z_Field 'Field',U_Z_FieldName 'FieldName',U_Z_Amount 'Earning',0 'Deduction' from [@Z_PAYROLL2] where U_Z_AMount>=0 "
                strPaySQL = strPaySQL & " and U_Z_RefCode='" & oTemp.Fields.Item("Code").Value & "'  ) as x order by x.Type"


                oRecBP.DoQuery(strPaySQL)
                Dim intCount As Integer = 0
                For intloop As Integer = 0 To oRecBP.RecordCount - 1
                    oDRow = ds.Tables("Earning").NewRow()
                    intCount = intCount + 1
                    oDRow.Item("empID") = oTemp.Fields.Item("U_Z_empid").Value
                    oDRow.Item("EarningCode") = oRecBP.Fields.Item("Field").Value
                    oDRow.Item("EarningName") = oRecBP.Fields.Item("FieldName").Value
                    oDRow.Item("Earning") = oRecBP.Fields.Item("Earning").Value
                    oDRow.Item("Deduction") = 0 ' oRecBP.Fields.Item("Deduction").Value
                    ds.Tables("Earning").Rows.Add(oDRow)
                    oRecBP.MoveNext()
                Next

                If intCount <= 0 Then
                    oDRow = ds.Tables("Earning").NewRow()
                    oDRow.Item("empID") = oTemp.Fields.Item("U_Z_empid").Value
                    oDRow.Item("EarningCode") = "0"
                    oDRow.Item("EarningName") = "0"
                    oDRow.Item("Earning") = 0
                    oDRow.Item("Deduction") = 0 ' oRecBP.Fields.Item("Deduction").Value
                    ds.Tables("Earning").Rows.Add(oDRow)
                End If
                strPaySQL = "select x.Type,x.Field,X.FieldName,x.Earning,x.Deduction  from ( "
                strPaySQL = strPaySQL & " select 'A' 'Type' ,U_Z_Field 'Field',U_Z_FieldName 'FieldName',0 'Earning',U_Z_Amount 'Deduction' from [@Z_PAYROLL3] where U_Z_AMount>=0 and U_Z_RefCode='" & oTemp.Fields.Item("Code").Value & "' ) as x order by x.Type"
                ' strPaySQL = " select * from ( select 'A' 'Type ,U_Z_Field,U_Z_FieldName,U_Z_Amount,0 from [@Z_PAYROLL2] where U_Z_AMount>0 and U_Z_RefCode='" & oRec.Fields.Item("Code").Value & "'  union all select 'B' Type,U_Z_Field,U_Z_FieldName,0,U_Z_Amount from [@Z_PAYROLL3] where U_Z_AMount>0 and U_Z_RefCode='" & oRec.Fields.Item("Code").Value & "') x order by x.Type"
                oRecBP.DoQuery(strPaySQL)
                intCount = 0
                For intloop As Integer = 0 To oRecBP.RecordCount - 1
                    oDRow = ds.Tables("Deduction").NewRow()
                    intCount = intCount + 1
                    oDRow.Item("empID") = oTemp.Fields.Item("U_Z_empid").Value
                    oDRow.Item("Code") = oRecBP.Fields.Item("Field").Value
                    oDRow.Item("Name") = oRecBP.Fields.Item("FieldName").Value
                    oDRow.Item("Amount") = oRecBP.Fields.Item("Deduction").Value
                    ds.Tables("Deduction").Rows.Add(oDRow)
                    oRecBP.MoveNext()
                Next

                If intCount <= 0 Then
                    oDRow = ds.Tables("Deduction").NewRow()
                    oDRow.Item("empID") = oTemp.Fields.Item("U_Z_empid").Value
                    oDRow.Item("Code") = "0"
                    oDRow.Item("Name") = "0"
                    oDRow.Item("Amount") = 0 'oRecBP.Fields.Item("Deduction").Value
                    ds.Tables("Deduction").Rows.Add(oDRow)
                    ' oRecBP.MoveNext()
                End If
                strPaySQL = "select U_Z_LeaveCode,U_Z_LeaveName,U_Z_CM,U_Z_NoofDays,U_Z_Balance,U_Z_Redim from [@Z_PAYROLL5] where U_Z_RefCode='" & oTemp.Fields.Item("Code").Value & "'"
                oRecBP.DoQuery(strPaySQL)
                Dim otemp4 As SAPbobsCOM.Recordset
                otemp4 = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

                oDRow = ds.Tables("Leave").NewRow()
                oDRow.Item("empID") = oTemp.Fields.Item("U_Z_empid").Value
                oDRow.Item("LeaveCode") = ""
                oDRow.Item("LeaveName") = ""
                ds.Tables("Leave").Rows.Add(oDRow)

                For intloop As Integer = 0 To oRecBP.RecordCount - 1
                    oDRow = ds.Tables("Leave").NewRow()
                    oDRow.Item("empID") = oTemp.Fields.Item("U_Z_empid").Value
                    oDRow.Item("LeaveCode") = oRecBP.Fields.Item("U_Z_LeaveCode").Value
                    otemp4.DoQuery("Select * from [@Z_PAY_LEAVE] where code='" & oRecBP.Fields.Item("U_Z_LeaveCode").Value & "'")
                    If otemp4.RecordCount > 0 Then
                        oDRow.Item("LeaveName") = otemp4.Fields.Item("Name").Value
                    Else
                        oDRow.Item("LeaveName") = oRecBP.Fields.Item("U_Z_LeaveName").Value
                    End If
                    oDRow.Item("OB") = oRecBP.Fields.Item("U_Z_CM").Value
                    oDRow.Item("Current") = oRecBP.Fields.Item("U_Z_NoofDays").Value
                    oDRow.Item("Redim") = oRecBP.Fields.Item("U_Z_Redim").Value
                    oDRow.Item("Balance") = oRecBP.Fields.Item("U_Z_Balance").Value
                    ds.Tables("Leave").Rows.Add(oDRow)
                    oRecBP.MoveNext()
                Next
                oTemp.MoveNext()
            Next

        End If
        addCrystal(ds, "Payslip")
    End Sub

    Private Sub addCrystal(ByVal ds1 As DataSet, ByVal aChoice As String)
       

        'If aChoice = "Payslip" Then
        '    strReportFileName = "Reports\rptPayslip.rpt"
        '    '  strFilename = System.Windows.Forms.Application.StartupPath & "\Payslip"
        'ElseIf aChoice = "Agreement" Then
        '    strReportFileName = "Agreement.rpt"
        '    '  strFilename = System.Windows.Forms.Application.StartupPath & "\Rental_Agreement"
        'Else
        '    strReportFileName = "AcctStatement.rpt"
        '    '  strFilename = System.Windows.Forms.Application.StartupPath & "\AccountStatement"
        'End If
        'strReportFileName = strReportFileName
        'strFilename = strFilename & ".pdf"

        'If File.Exists(strFilename) Then
        '    File.Delete(strFilename)
        'End If
        ' If ds1.Tables.Item("AccountBalance").Rows.Count > 0 Then
        Dim Crpt As New ReportDocument()
        Crpt.Load(Server.MapPath("Reports\rptPayslip.rpt"))
        Dim dsreportdata As DataSet ' = GetData("SELECT * FROM  [@AZ_HR_OSEAPP] WHERE U_Z_Empid = '1'")
        dsreportdata = ds1
        Crpt.SetDataSource(ds1)
        Dim mem As IO.MemoryStream = DirectCast(Crpt.ExportToStream(ExportFormatType.PortableDocFormat), IO.MemoryStream)
        Response.Clear()
        Response.Buffer = True
        Response.BinaryWrite(mem.ToArray())
        CrystalReportViewer1.ReportSource = Crpt


        'Dim CrExportOptions As ExportOptions
        'Dim CrDiskFileDestinationOptions As New  _
        'DiskFileDestinationOptions()
        'Dim CrFormatTypeOptions As New PdfRtfWordFormatOptions()
        'CrDiskFileDestinationOptions.DiskFileName = strFilename
        'CrExportOptions = Crpt.ExportOptions
        'With CrExportOptions
        '    .ExportDestinationType = ExportDestinationType.DiskFile
        '    .ExportFormatType = ExportFormatType.PortableDocFormat
        '    .DestinationOptions = CrDiskFileDestinationOptions
        '    .FormatOptions = CrFormatTypeOptions
        'End With
        'Crpt.Export()
        'Crpt.Close()
        'Dim x As System.Diagnostics.ProcessStartInfo
        'x = New System.Diagnostics.ProcessStartInfo
        'x.UseShellExecute = True
        'x.FileName = strFilename
        'System.Diagnostics.Process.Start(x)
        'x = Nothing
        'If 1 = 1 Then
        '    Crpt.Load(System.Windows.Forms.Application.StartupPath & "\Reports\" & strReportFileName)
        '    cryRpt.SetDataSource(ds1)
        '    If "T" = "W" Then
        '        Dim mythread As New System.Threading.Thread(AddressOf openFileDialog)
        '        mythread.SetApartmentState(ApartmentState.STA)
        '        mythread.Start()
        '        mythread.Join()
        '        ds1.Clear()
        '    Else
        '        Dim CrExportOptions As ExportOptions
        '        Dim CrDiskFileDestinationOptions As New  _
        '        DiskFileDestinationOptions()
        '        Dim CrFormatTypeOptions As New PdfRtfWordFormatOptions()
        '        CrDiskFileDestinationOptions.DiskFileName = strFilename
        '        CrExportOptions = cryRpt.ExportOptions
        '        With CrExportOptions
        '            .ExportDestinationType = ExportDestinationType.DiskFile
        '            .ExportFormatType = ExportFormatType.PortableDocFormat
        '            .DestinationOptions = CrDiskFileDestinationOptions
        '            .FormatOptions = CrFormatTypeOptions
        '        End With
        '        cryRpt.Export()
        '        cryRpt.Close()
        '        Dim x As System.Diagnostics.ProcessStartInfo
        '        x = New System.Diagnostics.ProcessStartInfo
        '        x.UseShellExecute = True
        '        x.FileName = strFilename
        '        System.Diagnostics.Process.Start(x)
        '        x = Nothing
        '        ' objUtility.ShowSuccessMessage("Report exported into PDF File")
        '    End If

        'Else
        '    ' objUtility.ShowWarningMessage("No data found")
        'End If

    End Sub
End Class