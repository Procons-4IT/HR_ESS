Imports System
Imports System.Globalization
Imports System.Data
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.IO
Imports System.Data.Odbc
Imports DataAccess
Imports EN
Public Class ExpClaimApprovalDA
    Dim objEN As ExpClaimApprovalEN = New ExpClaimApprovalEN()
    Dim objDA As DBConnectionDA = New DBConnectionDA()
    Dim objBL As DynamicApprovalDA = New DynamicApprovalDA()
    Public Sub New()
        objDA.con = New SqlConnection(objDA.GetConnection)
    End Sub
    Public Function LocalCurrency(ByVal objen As ExpClaimApprovalEN) As String
        Try
            objen.LocalCurrency = objen.SapCompany.GetCompanyService.GetAdminInfo.LocalCurrency
            Return objen.LocalCurrency
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            Throw ex
        End Try
    End Function
    Public Function GetUserCode(ByVal objEN As ExpClaimApprovalEN) As String
        Try
            objDA.strQuery = "select T1.USER_CODE from OHEM T0 JOIN OUSR T1 on T0.userId=T1.USERID where T0.empID='" & objEN.EmpId & "'"
            objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
            objDA.con.Open()
            objEN.UserCode = objDA.cmd.ExecuteScalar()
            objDA.con.Close()
            Return objEN.UserCode
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetEmpUserid(ByVal objEN As ExpClaimApprovalEN) As Integer
        Try
            objDA.strQuery = "select T0.userId from OHEM T0 JOIN OUSR T1 on T0.userId=T1.USERID where T0.empID='" & objEN.EmpId & "'"
            objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
            objDA.con.Open()
            objEN.EmpUserId = objDA.cmd.ExecuteScalar()
            objDA.con.Close()
            Return objEN.EmpUserId
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function MainGridBind(ByVal objEN As ExpClaimApprovalEN) As DataSet
        Try
            objDA.strQuery = " select distinct(T0.Code),T0.U_Z_TAEmpID,T0.U_Z_EmpID,T0.U_Z_EmpName,Convert(Varchar(10),T0.U_Z_SubDt,103) AS U_Z_SubDt,T0.U_Z_Client,T0.U_Z_Project,Case T0.U_Z_DocStatus when 'C' then 'Closed' else 'Opened' end as U_Z_DocStatus  from [@Z_HR_OEXPCL] T0"
            objDA.strQuery += " Left outer Join [@Z_HR_EXPCL] T1 on T0.Code=T1.U_Z_DocRefNo "
            objDA.strQuery += " JOIN [@Z_HR_APPT1] T4 ON T0.U_Z_EmpID = T4.U_Z_OUser  and (T1.""U_Z_AppStatus""='P' or T1.""U_Z_AppStatus""='-') "
            objDA.strQuery += " JOIN [@Z_HR_APPT2] T2 ON T4.DocEntry = T2.DocEntry "
            objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T2.DocEntry = T3.DocEntry  "
            objDA.strQuery += " And (T1.U_Z_CurApprover = '" + objEN.UserCode + "' OR T1.U_Z_NxtApprover = '" + objEN.UserCode + "')"
            objDA.strQuery += " And isnull(T2.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y' and  isnull(T1.U_Z_AppRequired,'N')='Y' and isnull(T0.U_Z_DocStatus,'O')='O' and  T2.U_Z_AUser = '" + objEN.UserCode + "' And T3.U_Z_DocType = 'ExpCli'  Order by T0.Code Desc;"

            objDA.strQuery += " select distinct(T0.Code),T0.U_Z_TAEmpID,T0.U_Z_EmpID,T0.U_Z_EmpName,Convert(Varchar(10),T0.U_Z_SubDt,103) AS U_Z_SubDt,T0.U_Z_Client,T0.U_Z_Project,Case T0.U_Z_DocStatus when 'C' then 'Closed' else 'Opened' end as U_Z_DocStatus  from [@Z_HR_OEXPCL] T0"
            objDA.strQuery += " Left outer Join [@Z_HR_EXPCL] T1 on T0.Code=T1.U_Z_DocRefNo "
            objDA.strQuery += " JOIN [@Z_HR_APPT1] T4 ON T0.U_Z_EmpID = T4.U_Z_OUser "
            objDA.strQuery += " JOIN [@Z_HR_APPT2] T2 ON T4.DocEntry = T2.DocEntry "
            objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T2.DocEntry = T3.DocEntry  "
            objDA.strQuery += " And isnull(T2.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y' and  isnull(T1.U_Z_AppRequired,'N')='Y' and  T2.U_Z_AUser = '" + objEN.UserCode + "' And T3.U_Z_DocType = 'ExpCli' Order by T0.Code Desc"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds)
            Return objDA.ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ExpensesRequestApproval(ByVal objEN As ExpClaimApprovalEN) As DataSet
        Try
            objDA.strQuery = " Select T0.U_Z_DocRefNo,T0.Code,T0.U_Z_EmpID,T0.U_Z_EmpName,Convert(Varchar(10),T0.U_Z_SubDt,103) AS U_Z_SubDt,T0.U_Z_Client,T0.U_Z_Project,Convert(Varchar(10),U_Z_Claimdt,103) AS U_Z_Claimdt,U_Z_ExpType,U_Z_Currency,cast(U_Z_CurAmt as decimal(10,2)) AS ""U_Z_CurAmt"",cast(U_Z_ExcRate as decimal(10,2)) AS ""U_Z_ExcRate"",U_Z_UsdAmt,U_Z_ReimAmt,U_Z_Attachment,Isnull(T5.U_Z_AppStatus,'P') AS U_Z_AppStatus,Convert(Varchar(10),isnull(T5.""U_Z_Month"",MONTH(U_Z_Claimdt))) AS ""U_Z_Month"",Convert(Varchar(10),isnull(T5.""U_Z_Year"",YEAR(U_Z_Claimdt))) AS ""U_Z_Year"","
            objDA.strQuery += "T5.U_Z_Remarks,Case T0.U_Z_TripType when 'N' then 'New' else 'Existing' End as U_Z_TripType,T0.U_Z_Posting,T0.U_Z_TraDesc,isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_CurApprover ),'') AS 'U_Z_CurApprover',isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_NxtApprover ),'') AS 'U_Z_NxtApprover',T1.U_Z_EmpID as 'TAEmpID', Case T0.U_Z_Reimburse when 'Y' then 'Yes' else 'No' End as  'U_Z_Reimburse', "
            objDA.strQuery += " Case U_Z_AppRequired when 'Y' then 'Yes' else 'No' End as  'U_Z_AppRequired',Convert(Varchar(10),T0.U_Z_AppReqDate,103) AS U_Z_AppReqDate,CONVERT(VARCHAR(8),U_Z_ReqTime,108) AS 'U_Z_ReqTime'"
            objDA.strQuery += ",T5.DocEntry  From [@Z_HR_EXPCL] T0 Left Outer Join [@Z_HR_APHIS] T5 on T0.Code=T5.U_Z_DocEntry And T5.U_Z_DocType= 'ExpCli' and T5.U_Z_ApproveBy='" + objEN.UserCode + "'"
            objDA.strQuery += " JOIN [@Z_HR_APPT1] T1 ON T0.U_Z_EmpID = T1.U_Z_OUser  and (T0.""U_Z_AppStatus""='P' or T0.""U_Z_AppStatus""='-') "
            objDA.strQuery += " JOIN [@Z_HR_APPT2] T2 ON T1.DocEntry = T2.DocEntry "
            objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T2.DocEntry = T3.DocEntry  "
            objDA.strQuery += " And (T0.U_Z_CurApprover = '" + objEN.UserCode + "' OR T0.U_Z_NxtApprover = '" + objEN.UserCode + "')"
            objDA.strQuery += " And isnull(T2.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y' and  isnull(T0.U_Z_AppRequired,'N')='Y' and  T2.U_Z_AUser = '" + objEN.UserCode + "' And T3.U_Z_DocType = 'ExpCli' where T0.U_Z_DocRefNo='" & objEN.DocEntry & "' Order by Convert(Numeric,T0.Code) Desc;"
            objDA.strQuery += " Select T0.Code,T0.U_Z_EmpID,T0.U_Z_EmpName,Convert(Varchar(10),T0.U_Z_SubDt,103) AS U_Z_SubDt,T0.U_Z_Client,T0.U_Z_Project,Case T0.U_Z_TripType when 'N' then 'New' else 'Existing' End as U_Z_TripType,T0.U_Z_TraDesc,Case isnull(T0.U_Z_DocStatus,'O') when 'C' then 'Closed' else 'Opened' end as U_Z_DocStatus from [@Z_HR_OEXPCL] T0 where Code='" & objEN.DocEntry & "';"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds1)
            Return objDA.ds1
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function BindExpenseSummaryApproval(ByVal objEN As ExpClaimApprovalEN) As DataSet
        Try
            objDA.strQuery = " Select Code,T0.U_Z_EmpID,U_Z_EmpName,T1.U_Z_EmpID as 'TAEmpID',U_Z_Client,Case U_Z_TripType when 'N' then 'New' else 'Existing' End as U_Z_TripType,U_Z_TraDesc,Convert(Varchar(10),T0.U_Z_SubDt,103) AS U_Z_SubDt,Convert(Varchar(10),U_Z_Claimdt,103) AS U_Z_Claimdt,U_Z_ExpType,U_Z_Currency,cast(U_Z_CurAmt as decimal(10,2)) AS ""U_Z_CurAmt"",cast(U_Z_ExcRate as decimal(10,2)) AS ""U_Z_ExcRate"",U_Z_UsdAmt,U_Z_ReimAmt,U_Z_Attachment,Case U_Z_AppStatus when 'P' then 'Pending' when 'A' then 'Approved' when 'R' then 'Rejected' end AS U_Z_AppStatus,Convert(Varchar(10),DateName( month , DateAdd(month,""U_Z_Month"" , -1 ))) AS  ""U_Z_Month"",Convert(Varchar(10),isnull(T0.""U_Z_Year"",YEAR(U_Z_Claimdt))) AS ""U_Z_Year"",U_Z_Project, "
            objDA.strQuery += " Case U_Z_AppRequired when 'Y' then 'Yes' else 'No' End as  'U_Z_AppRequired',Convert(Varchar(10),T0.U_Z_AppReqDate,103) AS U_Z_AppReqDate,CONVERT(VARCHAR(8),U_Z_ReqTime,108) AS 'U_Z_ReqTime'"
            objDA.strQuery += " , isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_CurApprover ),'') AS 'U_Z_CurApprover',isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_NxtApprover ),'') AS 'U_Z_NxtApprover' From [@Z_HR_EXPCL] T0 JOIN [@Z_HR_APPT1] T1 ON T0.U_Z_EmpID = T1.U_Z_OUser "
            objDA.strQuery += " JOIN [@Z_HR_APPT2] T2 ON T1.DocEntry = T2.DocEntry "
            objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T2.DocEntry = T3.DocEntry  "
            objDA.strQuery += " And isnull(T2.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y' and  T2.U_Z_AUser = '" + objEN.UserCode + "' And T3.U_Z_DocType = 'ExpCli' where T0.U_Z_DocRefNo='" & objEN.DocEntry & "' Order by Convert(Numeric,Code) Desc"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds3)
            Return objDA.ds3
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function LoadHistory(ByVal objEN As ExpClaimApprovalEN) As DataSet
        Try
            objDA.strQuery = " Select DocEntry,U_Z_DocEntry,U_Z_DocType,U_Z_EmpId,U_Z_EmpName,U_Z_ApproveBy,isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_NextApprover ),'') AS 'U_Z_NxtApprover',convert(varchar(10),CreateDate,103) AS CreateDate ,LEFT(CONVERT(VARCHAR(5), CreateTime, 9),2) + ':' + RIGHT(CONVERT(VARCHAR(30), CreateTime, 9),2) AS CreateTime,"
            objDA.strQuery += " convert(varchar(10),UpdateDate,103) AS UpdateDate,LEFT(CONVERT(VARCHAR(5), UpdateTime, 9),2) + ':' + RIGHT(CONVERT(VARCHAR(30), UpdateTime, 9),2) AS UpdateTime,"
            objDA.strQuery += " Case U_Z_AppStatus when 'A' then 'Approved' when 'R' then 'Rejected' else 'Pending' end AS U_Z_AppStatus ,U_Z_Remarks,U_Z_Year,U_Z_Month From [@Z_HR_APHIS] T0 "
            objDA.strQuery += " Where U_Z_DocType = 'ExpCli'"
            objDA.strQuery += " And U_Z_DocEntry = '" + objEN.DocEntry + "'"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds2)
            Return objDA.ds2
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ApprovalValidation(ByVal objEN As ExpClaimApprovalEN) As String
        Try
            If objEN.AppStatus = "R" Then
                If objEN.Remarks = "" Then
                    objDA.strmsg = "Remarks is missing..."
                    Return objDA.strmsg
                End If
            End If
            If objEN.AppStatus = "A" Then
                If objEN.Month = 0 Then
                    objDA.strmsg = "Month is missing..."
                    Return objDA.strmsg

                ElseIf objEN.Year = 0 Then
                    objDA.strmsg = "Year is missing..."
                    Return objDA.strmsg
                End If
                If objEN.PostingType = "P" Then
                    Try
                        objDA.strQuery = "Select * from [@Z_PAYROLL1] where U_Z_empID='" & objEN.EmpId & "' and U_Z_Month='" & objEN.Month & "' and U_Z_Year='" & objEN.Year & "' and U_Z_Posted='Y'"
                        objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
                        objDA.sqlda.Fill(objDA.dss1)
                        If objDA.dss1.Tables(0).Rows.Count > 0 Then
                            objDA.strmsg = "Payroll already posted for this month and year."
                            Return objDA.strmsg
                        Else
                            Return "Success"
                        End If
                    Catch ex As Exception
                        Return "Success"
                    End Try
                End If
            End If
        Catch ex As Exception
            objDA.strmsg = "" & ex.Message & ""
            Return objDA.strmsg
        End Try
        Return "Success"
    End Function
    Public Function addUpdateDocument(ByVal objEN As ExpClaimApprovalEN) As String
        Try
            Dim NextApprover As String = ""
            Dim oGeneralService As SAPbobsCOM.GeneralService
            Dim oGeneralData As SAPbobsCOM.GeneralData
            Dim oGeneralParams As SAPbobsCOM.GeneralDataParams
            Dim oCompanyService As SAPbobsCOM.CompanyService
            Dim oRecordSet, otestRs, oTemp, oTest As SAPbobsCOM.Recordset
            objDA.objMainCompany = objEN.SapCompany
            oCompanyService = objDA.objMainCompany.GetCompanyService()
            oGeneralService = oCompanyService.GetGeneralService("Z_HR_APHIS")
            oGeneralData = oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData)
            oGeneralParams = oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams)
            oRecordSet = objDA.objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            otestRs = objDA.objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oTemp = objDA.objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oTest = objDA.objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            objDA.strQuery = "select T0.DocEntry,T1.LineId from [@Z_HR_OAPPT] T0 JOIN [@Z_HR_APPT2] T1 on T0.DocEntry=T1.DocEntry"
            objDA.strQuery += " JOIN [@Z_HR_APPT1] T2 on T1.DocEntry=T2.DocEntry"
            objDA.strQuery += " where T0.U_Z_DocType='ExpCli' AND T2.U_Z_OUser='" & objEN.EmpId & "' AND T1.U_Z_AUser='" & objEN.UserCode & "'"
            otestRs.DoQuery(objDA.strQuery)
            If otestRs.RecordCount > 0 Then
                objEN.HeadDocEntry = otestRs.Fields.Item(0).Value
                objEN.HeadLineId = otestRs.Fields.Item(1).Value
            End If

            objDA.strQuery = "Select Top 1 U_Z_AUser From [@Z_HR_APPT2] Where  DocEntry = '" & objEN.HeadDocEntry & "' And LineId > '" & objEN.HeadLineId.ToString() & "' and isnull(U_Z_AMan,'')='Y'  Order By LineId Asc "
            oRecordSet.DoQuery(objDA.strQuery)
            If Not oRecordSet.EoF Then
                NextApprover = oRecordSet.Fields.Item(0).Value
            End If
            objDA.strQuery = "Select * from [@Z_HR_APHIS] where U_Z_DocEntry='" & objEN.DocEntry & "' and U_Z_DocType='ExpCli' and U_Z_ApproveBy='" & objEN.UserCode & "'"
            oRecordSet.DoQuery(objDA.strQuery)
            If oRecordSet.RecordCount > 0 Then
                oGeneralParams.SetProperty("DocEntry", oRecordSet.Fields.Item("DocEntry").Value)
                oGeneralData = oGeneralService.GetByParams(oGeneralParams)
                oGeneralData.SetProperty("U_Z_AppStatus", objEN.AppStatus)
                oGeneralData.SetProperty("U_Z_Remarks", objEN.Remarks)
                oGeneralData.SetProperty("U_Z_ADocEntry", objEN.HeadDocEntry)
                oGeneralData.SetProperty("U_Z_ALineId", objEN.HeadLineId)
                oGeneralData.SetProperty("U_Z_Month", objEN.Month)
                oGeneralData.SetProperty("U_Z_Year", objEN.Year)
                If objEN.AppStatus = "A" Then
                    oGeneralData.SetProperty("U_Z_NextApprover", NextApprover)
                Else
                    oGeneralData.SetProperty("U_Z_NextApprover", objEN.UserCode)
                End If
                oTemp.DoQuery("Select * ,isnull(""firstName"",'') +  ' ' + isnull(""middleName"",'') +  ' ' + isnull(""lastName"",'') 'EmpName' from OHEM where ""userid""=" & objEN.EmpUserId & "")
                If oTemp.RecordCount > 0 Then
                    oGeneralData.SetProperty("U_Z_EmpId", oTemp.Fields.Item("empID").Value.ToString())
                    oGeneralData.SetProperty("U_Z_EmpName", oTemp.Fields.Item("EmpName").Value)
                Else
                    oGeneralData.SetProperty("U_Z_EmpId", "")
                    oGeneralData.SetProperty("U_Z_EmpName", "")
                End If
                oGeneralService.Update(oGeneralData)
                objDA.strmsg = "Successfully approved document..."
            ElseIf (objEN.DocEntry <> "" And objEN.DocEntry <> "0") Then
                oTemp.DoQuery("Select * ,isnull(""firstName"",'') + ' ' + isnull(""middleName"",'') +  ' ' + isnull(""lastName"",'') 'EmpName' from OHEM where ""userid""=" & objEN.EmpUserId & "")
                If oTemp.RecordCount > 0 Then
                    oGeneralData.SetProperty("U_Z_EmpId", oTemp.Fields.Item("empID").Value.ToString())
                    oGeneralData.SetProperty("U_Z_EmpName", oTemp.Fields.Item("EmpName").Value)
                Else
                    oGeneralData.SetProperty("U_Z_EmpId", "")
                    oGeneralData.SetProperty("U_Z_EmpName", "")
                End If
                oGeneralData.SetProperty("U_Z_DocEntry", objEN.DocEntry)
                oGeneralData.SetProperty("U_Z_DocType", objEN.HistoryType)
                oGeneralData.SetProperty("U_Z_AppStatus", objEN.AppStatus)
                oGeneralData.SetProperty("U_Z_Remarks", objEN.Remarks)
                oGeneralData.SetProperty("U_Z_ApproveBy", objEN.UserCode)
                oGeneralData.SetProperty("U_Z_Approvedt", System.DateTime.Now)
                oGeneralData.SetProperty("U_Z_ADocEntry", objEN.HeadDocEntry)
                oGeneralData.SetProperty("U_Z_ALineId", objEN.HeadLineId)
                oGeneralData.SetProperty("U_Z_Month", objEN.Month)
                oGeneralData.SetProperty("U_Z_Year", objEN.Year)
                If objEN.AppStatus = "A" Then
                    oGeneralData.SetProperty("U_Z_NextApprover", NextApprover)
                Else
                    oGeneralData.SetProperty("U_Z_NextApprover", objEN.UserCode)
                End If
                oGeneralService.Add(oGeneralData)
                objDA.strmsg = "Successfully approved document..."
            End If
            objDA.strmsg = updateFinalStatus(objEN)
            'If objDA.strmsg = "Success" Or objDA.strmsg = "Successfully approved document..." Then
            '    'If objEN.AppStatus <> "P" And objEN.AppStatus <> "-" Then
            '    If objEN.AppStatus = "A" Then
            '        SendMessage(objEN)
            '    End If
            'End If
            Return objDA.strmsg
        Catch ex As Exception
            objDA.strmsg = ex.Message
            Return objDA.strmsg
        End Try
    End Function

    Public Function updateFinalStatus(ByVal objEN As ExpClaimApprovalEN) As String
        Try
            Dim StrMailMessage As String
            Dim oRecordSet, oTemp As SAPbobsCOM.Recordset
            oRecordSet = objEN.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oTemp = objEN.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            If objEN.AppStatus = "A" Then
                objDA.strQuery = " Select T2.DocEntry "
                objDA.strQuery += " From [@Z_HR_APPT2] T2 "
                objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T2.DocEntry = T3.DocEntry  "
                objDA.strQuery += " JOIN [@Z_HR_APPT1] T4 ON T4.DocEntry = T3.DocEntry  "
                objDA.strQuery += " Where T4.U_Z_Ouser='" & objEN.EmpId & "' and  U_Z_AFinal = 'Y'"
                objDA.strQuery += " And T2.U_Z_AUser = '" + objEN.UserCode + "' And T3.U_Z_DocType = 'ExpCli'"
                oRecordSet.DoQuery(objDA.strQuery)
                If Not oRecordSet.EoF Then
                    objDA.strQuery = "Update [@Z_HR_EXPCL] Set U_Z_Year=" & objEN.Year & ",U_Z_Month=" & objEN.Month & ", U_Z_AppStatus = 'A' Where Code = '" + objEN.DocEntry + "'"
                    oRecordSet.DoQuery(objDA.strQuery)
                    objDA.strmsg = objBL.AddtoUDT1_PayrollTrans(objEN.DocEntry, objEN.SapCompany)
                    If objEN.PostingType = "G/L Account" Or objEN.PostingType = "G" Then
                        objDA.strmsg = objBL.CreateJournelVouchers(objEN.DocEntry, objEN.Reimbused, objEN.SapCompany)
                        If objDA.strmsg = "Success" Then
                            StrMailMessage = "Expenses Claim request approved for the request number is :" & objEN.DocEntry
                            objBL.SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany)
                        Else
                            objDA.strQuery = "Update [@Z_HR_EXPCL] Set U_Z_AppStatus = 'P' Where Code = '" + objEN.DocEntry + "'"
                            oRecordSet.DoQuery(objDA.strQuery)
                        End If
                    End If
                End If
            ElseIf objEN.AppStatus = "R" Then
                objDA.strQuery = " Select T2.DocEntry "
                objDA.strQuery += " From [@Z_HR_APPT2] T2 "
                objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T2.DocEntry = T3.DocEntry  "
                objDA.strQuery += " JOIN [@Z_HR_APPT1] T4 ON T4.DocEntry = T3.DocEntry  "
                objDA.strQuery += " Where T4.U_Z_Ouser='" & objEN.EmpId & "'"
                objDA.strQuery += " And T2.U_Z_AUser = '" + objEN.UserCode + "' And T3.U_Z_DocType = 'ExpCli'"
                oRecordSet.DoQuery(objDA.strQuery)
                If Not oRecordSet.EoF Then
                    objDA.strQuery = "Update [@Z_HR_EXPCL] Set U_Z_AppStatus = 'R',U_Z_RejRemark='" & objEN.Remarks & "' Where Code = '" + objEN.DocEntry + "'"
                    oRecordSet.DoQuery(objDA.strQuery)
                    StrMailMessage = "Expenses Claim request rejected for the request number is :" & CInt(objEN.DocEntry)
                    objBL.SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany)
                    objDA.strmsg = "Success"
                End If
            End If
            'End If
            Return objDA.strmsg
        Catch ex As Exception
            objDA.strmsg = ex.Message
            Return objDA.strmsg
        End Try
    End Function
  
    Public Function getDocumentQuantity(ByVal strQuantity As String, ByVal aCompany As SAPbobsCOM.Company) As Double
        Dim dblQuant As Double
        Dim strTemp, strTemp1 As String
        Dim oRec As SAPbobsCOM.Recordset
        oRec = aCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        oRec.DoQuery("Select CurrCode  from OCRN")
        For intRow As Integer = 0 To oRec.RecordCount - 1
            strQuantity = strQuantity.Replace(oRec.Fields.Item(0).Value, "")
            oRec.MoveNext()
        Next
        strTemp1 = strQuantity
        strTemp = "."
        If strQuantity = "" Then
            Return 0
        End If
        Try
            dblQuant = Convert.ToDouble(strQuantity)
        Catch ex As Exception
            dblQuant = Convert.ToDouble(strTemp1)
        End Try
        Return dblQuant
    End Function

    Public Sub SendMessage(ByVal strReqNo As String, ByVal strEmpNo As String, ByVal strAuthorizer As String, ByVal MailDocEntry As String, ByVal objCompany As SAPbobsCOM.Company, ByVal strEmpName As String)
        Try
            Dim strQuery, strOrginator As String
            Dim strMessageUser As String
            Dim strTemplateNo As String = ""
            Dim strEmailMessage As String = ""
            Dim intLineID, IntReqno As Integer
            Dim oRecordSet, oTemp As SAPbobsCOM.Recordset
            Dim oCmpSrv As SAPbobsCOM.CompanyService
            Dim oMessageService As SAPbobsCOM.MessagesService
            Dim oMessage As SAPbobsCOM.Message
            Dim pMessageDataColumns As SAPbobsCOM.MessageDataColumns
            Dim pMessageDataColumn As SAPbobsCOM.MessageDataColumn
            Dim oLines As SAPbobsCOM.MessageDataLines
            Dim oLine As SAPbobsCOM.MessageDataLine
            Dim oRecipientCollection As SAPbobsCOM.RecipientCollection
            oCmpSrv = objCompany.GetCompanyService()
            oMessageService = oCmpSrv.GetBusinessService(SAPbobsCOM.ServiceTypes.MessagesService)
            oMessage = oMessageService.GetDataInterface(SAPbobsCOM.MessagesServiceDataInterfaces.msdiMessage)
            oRecordSet = objCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oTemp = objCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            IntReqno = CInt(strReqNo)
            strQuery = "select T0.DocEntry,T1.LineId from [@Z_HR_OAPPT] T0 JOIN [@Z_HR_APPT2] T1 on T0.DocEntry=T1.DocEntry"
            strQuery += " JOIN [@Z_HR_APPT1] T2 on T1.DocEntry=T2.DocEntry"
            strQuery += " where T0.U_Z_DocType='ExpCli' AND T2.U_Z_OUser='" & strEmpNo & "' AND T1.U_Z_AUser='" & strAuthorizer & "'"
            oTemp.DoQuery(strQuery)
            If oTemp.RecordCount > 0 Then
                strTemplateNo = oTemp.Fields.Item(0).Value
            End If
            strQuery = "Select LineId From [@Z_HR_APPT2] Where DocEntry = '" & strTemplateNo & "' And U_Z_AUser = '" & strAuthorizer & "'"
            oRecordSet.DoQuery(strQuery)
            If Not oRecordSet.EoF Then
                intLineID = CInt(oRecordSet.Fields.Item(0).Value)
                strQuery = "Select Top 1 U_Z_AUser From [@Z_HR_APPT2] Where  DocEntry = '" & strTemplateNo & "' And LineId > '" & intLineID.ToString() & "' and isnull(U_Z_AMan,'')='Y'  Order By LineId Asc "
                oRecordSet.DoQuery(strQuery)

                If Not oRecordSet.EoF Then
                    strMessageUser = oRecordSet.Fields.Item(0).Value
                    oMessage.Subject = "Expense Claim" & ":" & " Need Your Approval "
                    Dim strMessage As String = ""
                    strQuery = "Update [@Z_HR_EXPCL] set U_Z_CurApprover='" & strAuthorizer & "',U_Z_NxtApprover='" & strMessageUser & "' where Code in (" & MailDocEntry & ")"
                    oTemp.DoQuery(strQuery)
                    strMessage = " Requested by  :" & strEmpName
                    strOrginator = strMessage
                    oMessage.Text = "Expense Claim" & " " & IntReqno.ToString() & strOrginator & " Needs Your Approval "
                    oRecipientCollection = oMessage.RecipientCollection
                    oRecipientCollection.Add()
                    oRecipientCollection.Item(0).SendInternal = SAPbobsCOM.BoYesNoEnum.tYES
                    oRecipientCollection.Item(0).UserCode = strMessageUser
                    pMessageDataColumns = oMessage.MessageDataColumns
                    pMessageDataColumn = pMessageDataColumns.Add()
                    pMessageDataColumn.ColumnName = "Request No"
                    oLines = pMessageDataColumn.MessageDataLines()
                    oLine = oLines.Add()
                    oLine.Value = MailDocEntry
                    oMessageService.SendMessage(oMessage)
                    strEmailMessage = "Expense Claim" + "  " + IntReqno.ToString() + " " + " with details " + MailDocEntry + " " + strOrginator + " Needs Your Approval "
                    objBL.SendMail_Approval(strEmailMessage, strMessageUser, strMessageUser, objCompany)
                 
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function UpdateDocStatus(ByVal objEN As ExpClaimApprovalEN) As String
        Try
            Dim oRecordSet As SAPbobsCOM.Recordset
            oRecordSet = objEN.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            objDA.strQuery = " Select T2.DocEntry "
            objDA.strQuery += " From [@Z_HR_APPT2] T2 "
            objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T2.DocEntry = T3.DocEntry  "
            objDA.strQuery += " JOIN [@Z_HR_APPT1] T4 ON T4.DocEntry = T3.DocEntry  "
            objDA.strQuery += " Where T4.U_Z_Ouser='" & objEN.EmpId & "' and  U_Z_AFinal = 'Y'"
            objDA.strQuery += " And T2.U_Z_AUser = '" + objEN.UserCode + "' And T3.U_Z_DocType = 'ExpCli'"
            oRecordSet.DoQuery(objDA.strQuery)
            If Not oRecordSet.EoF Then
                objDA.strQuery = "Update [@Z_HR_OEXPCL] set U_Z_DocStatus='" & objEN.AppStatus & "' where Code='" & objEN.HeadDocEntry & "'"
                oRecordSet.DoQuery(objDA.strQuery)
                objDA.strmsg = "Success"
            End If
            Return objDA.strmsg
        Catch ex As Exception
            objDA.strmsg = ex.Message
            Return objDA.strmsg
        End Try
    End Function
    Public Function GetFinalStatus(ByVal objEN As ExpClaimApprovalEN) As Boolean
        Try
            Dim oRecordSet As SAPbobsCOM.Recordset
            oRecordSet = objEN.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            objDA.strQuery = " Select T2.DocEntry "
            objDA.strQuery += " From [@Z_HR_APPT2] T2 "
            objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T2.DocEntry = T3.DocEntry  "
            objDA.strQuery += " JOIN [@Z_HR_APPT1] T4 ON T4.DocEntry = T3.DocEntry  "
            objDA.strQuery += " Where T4.U_Z_Ouser='" & objEN.EmpId & "' and  U_Z_AFinal = 'Y'"
            objDA.strQuery += " And T2.U_Z_AUser = '" + objEN.UserCode + "' And T3.U_Z_DocType = 'ExpCli'"
            oRecordSet.DoQuery(objDA.strQuery)
            If Not oRecordSet.EoF Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            objDA.strmsg = ex.Message
            Return objDA.strmsg
        End Try
    End Function
End Class
