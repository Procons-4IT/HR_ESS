Imports System
Imports System.Globalization
Imports System.Data
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.IO
Imports System.Data.Odbc
Imports DataAccess
Imports EN
Public Class LoanApprovalDA
    Dim objDA As DBConnectionDA = New DBConnectionDA()
    Dim objBL As DynamicApprovalDA = New DynamicApprovalDA()
    Public Sub New()
        objDA.con = New SqlConnection(objDA.GetConnection)
    End Sub
    Public Function GetUserCode(ByVal objEN As LoanApprovalEN) As String
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
    Public Function InitializationApproval(ByVal objEN As LoanApprovalEN) As DataSet
        Try
            objDA.strQuery = "select T0.U_DocEntry,T4.ExtEmpNo,Convert(Varchar(10),U_ReqDate,103) AS U_ReqDate,T0.U_Empid,U_Empname,U_LoanName,U_LoanCode,U_RefNo,CAST(isnull(U_LoanAmt,0) as decimal(10,2)) AS U_LoanAmt,"
            objDA.strQuery += "  Case U_Z_AppRequired when 'Y' then 'Yes' else 'No' End as  'U_Z_AppRequired',Convert(Varchar(10),U_Z_AppReqDate,103) AS 'U_Z_AppReqDate',CONVERT(VARCHAR(8),U_Z_ReqTime,108) AS 'U_Z_ReqTime',"
            objDA.strQuery += " Case U_Z_AppStatus when 'P' then 'Pending' when 'A' then 'Approved' when 'R' then 'Rejected' end AS U_Z_AppStatus, isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_CurApprover ),'') AS 'U_CurApprover',isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_NxtApprover ),'') AS 'U_NxtApprover'  from [U_LOANREQ] T0 JOIN OHEM T4 ON T0.U_Empid = T4.empID and (T0.""U_Z_AppStatus""='P' or T0.""U_Z_AppStatus""='-') "
            objDA.strQuery += " JOIN [@Z_HR_APPT1] T1 ON T4.empID = T1.U_Z_OUser "
            objDA.strQuery += " JOIN [@Z_HR_APPT2] T2 ON T1.DocEntry = T2.DocEntry "
            objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T2.DocEntry = T3.DocEntry  "
            objDA.strQuery += " And (T0.U_CurApprover = '" + objEN.UserCode + "' OR T0.U_NxtApprover = '" + objEN.UserCode + "')"
            objDA.strQuery += " And isnull(T2.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y' and  isnull(T0.U_Z_AppRequired,'N')='Y' and  T2.U_Z_AUser = '" + objEN.UserCode + "' And T3.U_Z_DocType = '" + objEN.HeaderType + "' Order by T0.U_DocEntry desc ;"

            objDA.strQuery += "SELECT T4.[Code], T4.[Name], T4.[U_Z_EmpID], T0.""firstName""+T0.""middleName""+T0.""lastName"" 'EmpName',T4.[U_Z_LoanCode], T4.[U_Z_LoanName],CAST(isnull(T4.[U_Z_LoanAmount],0) as decimal(10,2)) AS [U_Z_LoanAmount], CAST(isnull(T4.[U_Z_EMIAmount],0) as decimal(10,2)) AS [U_Z_EMIAmount], T4.[U_Z_NoEMI],Convert(Varchar(10),T4.[U_Z_DisDate],103) AS [U_Z_DisDate],Convert(Varchar(10),T4.[U_Z_StartDate],103) AS [U_Z_StartDate],Convert(Varchar(10),T4.[U_Z_EndDate],103) AS [U_Z_EndDate], T4.[U_Z_PaidEMI],CAST(isnull(T4.[U_Z_Balance],0) as decimal(10,2)) AS [U_Z_Balance], T4.[U_Z_GLACC], T4.[U_Z_Status],T0.ExtEmpNo from OHEM T0 JOIN [@Z_PAY5] T4 ON T0.empID = T4.U_Z_EmpID "
            objDA.strQuery += " JOIN [@Z_HR_APPT1] T1 ON T4.U_Z_EmpID = T1.U_Z_OUser "
            objDA.strQuery += " JOIN [@Z_HR_APPT2] T2 ON T1.DocEntry = T2.DocEntry "
            objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T2.DocEntry = T3.DocEntry  "
            objDA.strQuery += " And isnull(T2.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y' and  T2.U_Z_AUser = '" + objEN.UserCode + "' And T3.U_Z_DocType = '" + objEN.HeaderType + "' Order by T4.Code desc; "

            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds)
            Return objDA.ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function LoadHistory(ByVal objEN As LoanApprovalEN) As DataSet
        Try
            objDA.strQuery = " Select DocEntry,U_Z_DocEntry,U_Z_DocType,U_Z_EmpId,U_Z_EmpName,U_Z_LoanCode,CAST(isnull(U_Z_LoanAmount,0) as decimal(10,2)) AS U_Z_LoanAmount,convert(varchar(10),U_Z_DisDate,103) as U_Z_DisDate,convert(varchar(10),U_Z_StartDate,103) as U_Z_StartDate,U_Z_NoEMI,"
            objDA.strQuery += " U_Z_ApproveBy,isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_NxtApprover ),'') AS U_Z_NxtApprover,convert(varchar(10),CreateDate,103) as CreateDate ,LEFT(CONVERT(VARCHAR(5), CreateTime, 9),2) + ':' + RIGHT(CONVERT(VARCHAR(30), CreateTime, 9),2) AS CreateTime, convert(varchar(10),UpdateDate,103) as UpdateDate,"
            objDA.strQuery += " LEFT(CONVERT(VARCHAR(5), UpdateTime, 9),2) + ':' + RIGHT(CONVERT(VARCHAR(30), UpdateTime, 9),2) AS UpdateTime,Case U_Z_AppStatus when 'P' then 'Pending' when 'A' then 'Approved' when 'R' then 'Rejected' end AS U_Z_AppStatus,U_Z_Remarks From [@Z_HR_LOANHIS] T0"
            objDA.strQuery += " Where U_Z_DocType = '" + objEN.HistoryType + "'"
            objDA.strQuery += " And U_Z_DocEntry = '" + objEN.DocEntry + "'"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds1)
            Return objDA.ds1
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PopulateLoanDetails(ByVal objEN As LoanApprovalEN) As DataSet
        Try
            objDA.strQuery = "select T0.U_DocEntry,Convert(Varchar(10),T0.U_ReqDate,103) AS U_ReqDate,T0.U_Empid,T0.U_Empname,T0.U_LoanName,T0.U_LoanCode,"
            objDA.strQuery += " T0.U_RefNo,CAST(isnull(T0.U_LoanAmt,0) as decimal(10,2)) AS U_LoanAmt,convert(varchar(10),T0.U_DisDate,103) as U_Z_DisDate,convert(varchar(10),T0.U_InstDate,103) as U_Z_StartDate,"
            objDA.strQuery += " T0.U_NoEMI AS U_Z_NoEMI from [U_LOANREQ] T0 where U_Empid='" & objEN.EmpId & "' and U_DocEntry='" & objEN.DocEntry & "' order by T0.U_DocEntry Desc"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.dss1)
            Return objDA.dss1
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function SummaryHistory(ByVal objEN As LoanApprovalEN) As DataSet
        Try
            objDA.strQuery = " Select T1.DocEntry,T1.U_Z_DocEntry,T1.U_Z_DocType,T1.U_Z_EmpId,T1.U_Z_EmpName,T1.U_Z_LoanCode,CAST(isnull(T1.U_Z_LoanAmount,0) as decimal(10,2)) AS U_Z_LoanAmount,convert(varchar(10),T1.U_Z_DisDate,103) as U_Z_DisDate,convert(varchar(10),T1.U_Z_StartDate,103) as U_Z_StartDate,T1.U_Z_NoEMI,"
            objDA.strQuery += " T1.U_Z_ApproveBy,isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T1.U_Z_NxtApprover ),'') AS U_Z_NxtApprover,convert(varchar(10),T1.CreateDate,103) as CreateDate ,LEFT(CONVERT(VARCHAR(5), T1.CreateTime, 9),2) + ':' + RIGHT(CONVERT(VARCHAR(30), T1.CreateTime, 9),2) AS CreateTime, convert(varchar(10),T1.UpdateDate,103) as UpdateDate,LEFT(CONVERT(VARCHAR(5), T1.UpdateTime, 9),2) + ':' + RIGHT(CONVERT(VARCHAR(30), T1.UpdateTime, 9),2) AS UpdateTime,Case T1.U_Z_AppStatus when 'P' then 'Pending' when 'A' then 'Approved' when 'R' then 'Rejected' end AS U_Z_AppStatus,T1.U_Z_Remarks from [U_LOANREQ] T0 LEFT JOIN [@Z_HR_LOANHIS] T1 ON T0.U_DocEntry=T1.U_Z_DocEntry where T0.U_Empid='" & objEN.EmpId & "' and T0.U_RefNo='" & objEN.DocEntry & "' order by T1.DocEntry Desc"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds3)
            Return objDA.ds3
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetEmpUserid(ByVal objEN As LoanApprovalEN) As Integer
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

    Public Function addUpdateDocument(ByVal objEN As LoanApprovalEN) As String
        Try
            Dim NextApprover As String = ""
            Dim oGeneralService As SAPbobsCOM.GeneralService
            Dim oGeneralData As SAPbobsCOM.GeneralData
            Dim oGeneralParams As SAPbobsCOM.GeneralDataParams
            Dim oCompanyService As SAPbobsCOM.CompanyService
            Dim oRecordSet, otestRs, oTemp, oTest As SAPbobsCOM.Recordset
            objDA.objMainCompany = objEN.SapCompany
            oCompanyService = objDA.objMainCompany.GetCompanyService()
            oGeneralService = oCompanyService.GetGeneralService("Z_HR_LOANHIS")
            oGeneralData = oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData)
            oGeneralParams = oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams)
            oRecordSet = objDA.objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            otestRs = objDA.objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oTemp = objDA.objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oTest = objDA.objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            objDA.strQuery = "select T0.DocEntry,T1.LineId from [@Z_HR_OAPPT] T0 JOIN [@Z_HR_APPT2] T1 on T0.DocEntry=T1.DocEntry"
            objDA.strQuery += " JOIN [@Z_HR_APPT1] T2 on T1.DocEntry=T2.DocEntry"
            objDA.strQuery += " where T0.U_Z_DocType='LoanReq' AND T2.U_Z_OUser='" & objEN.EmpId & "' AND T1.U_Z_AUser='" & objEN.UserCode & "'"
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
            objDA.strQuery = "Select * from [@Z_HR_LOANHIS] where U_Z_DocEntry='" & objEN.DocEntry & "' and U_Z_DocType='LoanReq' and U_Z_ApproveBy='" & objEN.UserCode & "'"
            oRecordSet.DoQuery(objDA.strQuery)
            If oRecordSet.RecordCount > 0 Then
                oGeneralParams.SetProperty("DocEntry", oRecordSet.Fields.Item("DocEntry").Value)
                oGeneralData = oGeneralService.GetByParams(oGeneralParams)
                oGeneralData.SetProperty("U_Z_AppStatus", objEN.AppStatus)
                oGeneralData.SetProperty("U_Z_Remarks", objEN.Remarks)
                oGeneralData.SetProperty("U_Z_ADocEntry", objEN.HeadDocEntry)
                oGeneralData.SetProperty("U_Z_ALineId", objEN.HeadLineId)

                oGeneralData.SetProperty("U_Z_LoanAmount", objEN.LoanAmt)
                oGeneralData.SetProperty("U_Z_DisDate", objEN.DisDate)
                oGeneralData.SetProperty("U_Z_StartDate", objEN.InsDate)
                oGeneralData.SetProperty("U_Z_NoEMI", objEN.NoInst)
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
                oGeneralData.SetProperty("U_Z_LoanAmount", objEN.LoanAmt)
                oGeneralData.SetProperty("U_Z_DisDate", objEN.DisDate)
                oGeneralData.SetProperty("U_Z_StartDate", objEN.InsDate)
                oGeneralData.SetProperty("U_Z_NoEMI", objEN.NoInst)
                If objEN.AppStatus = "A" Then
                    oGeneralData.SetProperty("U_Z_NextApprover", NextApprover)
                Else
                    oGeneralData.SetProperty("U_Z_NextApprover", objEN.UserCode)
                End If
                oGeneralService.Add(oGeneralData)
                objDA.strmsg = "Successfully approved document..."
            End If
            objDA.strmsg = updateFinalStatus(objEN)
            Return objDA.strmsg
        Catch ex As Exception
            objDA.strmsg = ex.Message
            Return objDA.strmsg
        End Try
    End Function

    Public Function updateFinalStatus(ByVal objEN As LoanApprovalEN) As String
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
                objDA.strQuery += " And T2.U_Z_AUser = '" + objEN.UserCode + "' And T3.U_Z_DocType = 'LoanReq'"
                oRecordSet.DoQuery(objDA.strQuery)
                If Not oRecordSet.EoF Then
                    objDA.strQuery = "Update [U_LOANREQ] Set U_Z_AppStatus = 'A' Where U_DocEntry = '" + objEN.DocEntry + "'"
                    oRecordSet.DoQuery(objDA.strQuery)
                    objDA.strmsg = AddtoUDT1LoanTrans(objEN)
                    If objDA.strmsg = "Success" Then
                        StrMailMessage = "Loan request approved for the request number is :" & objEN.DocEntry
                        objBL.SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany)
                        Dim empName As String = objDA.getEmpName(objEN.EmpId)
                        SendMessage(objEN.DocEntry, objEN.EmpId, objEN.UserCode, objEN.SapCompany, empName)
                    Else
                        objDA.strQuery = "Update [U_LOANREQ] Set U_Z_AppStatus = 'P' Where U_DocEntry = '" + objEN.DocEntry + "'"
                        oRecordSet.DoQuery(objDA.strQuery)
                    End If
                End If
            ElseIf objEN.AppStatus = "R" Then
                objDA.strQuery = " Select T2.DocEntry "
                objDA.strQuery += " From [@Z_HR_APPT2] T2 "
                objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T2.DocEntry = T3.DocEntry  "
                objDA.strQuery += " JOIN [@Z_HR_APPT1] T4 ON T4.DocEntry = T3.DocEntry  "
                objDA.strQuery += " Where T4.U_Z_Ouser='" & objEN.EmpId & "'"
                objDA.strQuery += " And T2.U_Z_AUser = '" + objEN.UserCode + "' And T3.U_Z_DocType = 'LoanReq'"
                oRecordSet.DoQuery(objDA.strQuery)
                If Not oRecordSet.EoF Then
                    objDA.strQuery = "Update [U_LOANREQ] Set U_Z_AppStatus = 'R' Where U_DocEntry = '" + objEN.DocEntry + "'"
                    oRecordSet.DoQuery(objDA.strQuery)
                    StrMailMessage = "Loan request rejected for the request number is :" & objEN.DocEntry
                    objBL.SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany)
                    Dim empName As String = objDA.getEmpName(objEN.EmpId)
                    SendMessage(objEN.DocEntry, objEN.EmpId, objEN.UserCode, objEN.SapCompany, empName)
                    objDA.strmsg = "Success"
                End If
            End If
            Return objDA.strmsg
        Catch ex As Exception
            objDA.strmsg = ex.Message
            Return objDA.strmsg
        End Try
    End Function
    Public Sub SendMessage(ByVal strReqNo As String, ByVal strEmpNo As String, ByVal strAuthorizer As String, ByVal objCompany As SAPbobsCOM.Company, ByVal strEmpName As String)
        Try
            Dim strQuery, strOrginator As String
            Dim strMessageUser As String
            Dim strTemplateNo As String = ""
            Dim strEmailMessage As String = ""
            Dim intLineID As Integer
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
            strQuery = "select T0.DocEntry,T1.LineId from [@Z_HR_OAPPT] T0 JOIN [@Z_HR_APPT2] T1 on T0.DocEntry=T1.DocEntry"
            strQuery += " JOIN [@Z_HR_APPT1] T2 on T1.DocEntry=T2.DocEntry"
            strQuery += " where T0.U_Z_DocType='LoanReq' AND T2.U_Z_OUser='" & strEmpNo & "' AND T1.U_Z_AUser='" & strAuthorizer & "'"
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
                    oMessage.Subject = "Loan Request" & ":" & " Need Your Approval "
                    Dim strMessage As String = ""
                    strMessage = " Requested by  :" & strEmpName
                    strOrginator = strMessage
                    oMessage.Text = "Loan Request" & " " & strReqNo & strOrginator & " Needs Your Approval "
                    oRecipientCollection = oMessage.RecipientCollection
                    oRecipientCollection.Add()
                    oRecipientCollection.Item(0).SendInternal = SAPbobsCOM.BoYesNoEnum.tYES
                    oRecipientCollection.Item(0).UserCode = strMessageUser
                    pMessageDataColumns = oMessage.MessageDataColumns
                    pMessageDataColumn = pMessageDataColumns.Add()
                    pMessageDataColumn.ColumnName = "Request No"
                    oLines = pMessageDataColumn.MessageDataLines()
                    oLine = oLines.Add()
                    oLine.Value = strReqNo
                    oMessageService.SendMessage(oMessage)
                    strEmailMessage = "Loan Request" + "  " + strReqNo + " " + strOrginator + " Needs Your Approval "
                    strQuery = "Update [U_LOANREQ] set U_CurApprover='" & strAuthorizer & "',U_NxtApprover='" & strMessageUser & "' where U_DocEntry='" & strReqNo & "'"
                    oTemp.DoQuery(strQuery)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function AddtoUDT1LoanTrans(ByVal objEN As LoanApprovalEN) As String
        Try
            objEN.HeadDocEntry = objDA.Getmaxcode("[@Z_PAY5]", "Code")
            objDA.strQuery = "INSERT INTO [@Z_PAY5] (Code,Name,U_Z_EMPID,U_Z_LoanCode,U_Z_LoanName,U_Z_LoanAmount,U_Z_StartDate,U_Z_NoEMI,"
            objDA.strQuery += " U_Z_EMIAmount,U_Z_EndDate,U_Z_GLACC,U_Z_Status,U_Z_DisDate,U_Z_CreationDate,U_Z_CreatedBy) Values ( "
            objDA.strQuery += "'" & objEN.HeadDocEntry & "','" & objEN.HeadDocEntry & "','" & objEN.EmpId & "','" & objEN.LoanCode & "','" & objEN.LoanName & "','" & objEN.LoanAmt & "','" & objEN.InsDate.ToString("yyyy/MM/dd") & "','" & objEN.NoInst & "',"
            objDA.strQuery += "'" & objEN.EMIAmt & "','" & objEN.EndDate.ToString("yyyy/MM/dd") & "','" & objEN.GLAccount & "','Open','" & objEN.DisDate.ToString("yyyy/MM/dd") & "',getdate(),'" & objEN.SapCompany.UserName & "')"
            objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
            objDA.con.Open()
            objDA.cmd.ExecuteNonQuery()
            objDA.con.Close()
            objDA.strQuery = "Update [U_LOANREQ] Set U_RefNo = '" & objEN.HeadDocEntry & "' Where U_DocEntry = '" + objEN.DocEntry + "'"
            objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
            objDA.con.Open()
            objDA.cmd.ExecuteNonQuery()
            objDA.con.Close()
            objDA.strmsg = AddtoUDT(objEN.HeadDocEntry, objEN.SapCompany)
            Return objDA.strmsg
        Catch ex As Exception
            objDA.strmsg = ex.Message
            Return objDA.strmsg
        End Try
    End Function
    Private Function AddtoUDT(ByVal aCode As String, ByVal SapCOmpany As SAPbobsCOM.Company) As String
        Dim dtFrom, dtTo As Date
        Dim oUserTable As SAPbobsCOM.UserTable
        Dim strCode As String
        Dim otest, oTest1 As SAPbobsCOM.Recordset
        Dim dblAnnualRent, dblNoofMonths As Double
        Try
            otest = SapCOmpany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oTest1 = SapCOmpany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oTest1.DoQuery("Select * from ""@Z_PAY5"" where ""Code""='" & aCode & "' ")
            dtFrom = oTest1.Fields.Item("U_Z_StartDate").Value
            dtTo = oTest1.Fields.Item("U_Z_EndDate").Value
            dblAnnualRent = oTest1.Fields.Item("U_Z_LoanAmount").Value
            dblNoofMonths = oTest1.Fields.Item("U_Z_EMIAmount").Value
            Dim dblLoanAMount As Double = dblAnnualRent
            Dim dtRescheduleDate As DateTime
            Dim dblBalance As Double = 0
            If oTest1.RecordCount > 0 Then
                otest.DoQuery("Select * from ""@Z_PAY15"" where ""U_Z_TrnsRefCode""='" & oTest1.Fields.Item("Code").Value & "'")
                If otest.RecordCount <= 0 Then
                    oUserTable = SapCOmpany.UserTables.Item("Z_PAY15")
                    While dblLoanAMount > 0 ' dtFrom < dtTo
                        dblNoofMonths = oTest1.Fields.Item("U_Z_EMIAmount").Value
                        otest.DoQuery("Select Code,Name from ""@Z_PAY15"" where U_Z_TrnsRefCode='" & aCode & "' and Month(U_Z_DueDate)=" & Month(dtFrom) & " and Year(U_Z_DueDate)=" & Year(dtFrom))
                        If otest.RecordCount <= 0 Then
                            Dim otest11 As SAPbobsCOM.Recordset
                            otest11 = SapCOmpany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                            otest11.DoQuery("Select Code,Name, U_Z_MONTH,U_Z_DAYS,isnull(U_Z_StopIns,'N') 'U_Z_StopIns' from [@Z_WORK] where U_Z_MONTH= " & dtFrom.Month & " and U_Z_Year=" & dtFrom.Year)
                            Dim blnStopInc As Boolean = False
                            If otest11.RecordCount > 0 Then
                                If otest11.Fields.Item("U_Z_StopIns").Value = "Y" Then
                                    blnStopInc = True
                                End If
                            End If
                            If blnStopInc = False Then
                                dtRescheduleDate = New DateTime(dtFrom.Year, dtFrom.Month, 1)
                                dtRescheduleDate = DateAdd(DateInterval.Month, 1, dtRescheduleDate)
                                dtRescheduleDate = DateAdd(DateInterval.Day, -1, dtRescheduleDate)
                                dtFrom = dtRescheduleDate
                                dblBalance = dblLoanAMount - dblNoofMonths
                                If dblBalance > 0 Then
                                    dblBalance = dblBalance
                                Else
                                    dblBalance = 0
                                    dblNoofMonths = dblLoanAMount
                                End If
                                strCode = objDA.Getmaxcode("[@Z_PAY15]", "Code")
                                objDA.strQuery = "INSERT INTO [@Z_PAY15] (Code,Name,U_Z_TrnsRefCode,U_Z_EmpID,U_Z_LoanCode,U_Z_LoanName,U_Z_LoanAmount,U_Z_DueDate,U_Z_OB,"
                                objDA.strQuery += " U_Z_CashPaid,U_Z_StopIns,U_Z_Status,U_Z_Balance,U_Z_Month,U_Z_Year,U_Z_EMIAmount) Values ( "
                                objDA.strQuery += "'" & strCode & "','" & strCode & "','" & aCode & "','" & oTest1.Fields.Item("U_Z_EmpID").Value & "','" & oTest1.Fields.Item("U_Z_LoanCode").Value & "','" & oTest1.Fields.Item("U_Z_LoanName").Value & "','" & oTest1.Fields.Item("U_Z_LoanAmount").Value & "','" & dtFrom.ToString("yyyy-MM-dd") & "',"
                                objDA.strQuery += "'" & dblLoanAMount & "','N','N','O','" & dblBalance & "','" & Month(dtFrom) & "','" & Year(dtFrom) & "','" & dblNoofMonths & "')"
                                objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
                                objDA.con.Open()
                                objDA.cmd.ExecuteNonQuery()
                                objDA.con.Close()
                            Else
                                dblNoofMonths = 0
                            End If
                        End If
                        dtFrom = DateAdd(DateInterval.Month, 1, dtFrom)
                        dblLoanAMount = dblLoanAMount - dblNoofMonths
                    End While
                End If
            End If
        Catch ex As Exception
            objDA.strmsg = ex.Message
            Return objDA.strmsg
        End Try
        Return "Success"
    End Function
    Public Function FinalApprovalValidate(ByVal objEN As LoanApprovalEN) As Boolean
        Try
            If objEN.AppStatus = "A" Then
                objDA.strQuery = " Select T2.DocEntry "
                objDA.strQuery += " From [@Z_HR_APPT2] T2 "
                objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T2.DocEntry = T3.DocEntry  "
                objDA.strQuery += " JOIN [@Z_HR_APPT1] T4 ON T4.DocEntry = T3.DocEntry  "
                objDA.strQuery += " Where T4.U_Z_Ouser='" & objEN.EmpId & "' and  U_Z_AFinal = 'Y'"
                objDA.strQuery += " And T2.U_Z_AUser = '" + objEN.UserCode + "' And T3.U_Z_DocType = 'LoanReq'"
                objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
                objDA.sqlda.Fill(objDA.dss3)
                If objDA.dss3.Tables(0).Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            End If
            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function LoanOverLap(ByVal objEN As LoanApprovalEN) As Boolean
        Try
            objDA.strQuery = "Select * from ""@Z_PAY5"" where  ""U_Z_EmpID""='" & objEN.EmpId & "' and ""U_Z_LoanCode""='" & objEN.LoanCode & "' and ""U_Z_Status""<>'Close'"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.dss4)
            If objDA.dss4.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function GetLoanDetails(ByVal objEN As LoanApprovalEN) As DataSet
        Try
            objDA.strQuery = "Select * from ""@Z_PAY_LOAN"" where ""Code""='" & objEN.LoanCode & "'"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.dss)
            Return objDA.dss
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function getYearofExperience(ByVal objEN As LoanApprovalEN) As Double
        Dim otemp4, otemp3 As SAPbobsCOM.Recordset
        Dim dblHourlyRate As Double
        Dim oTst As SAPbobsCOM.Recordset
        Dim stOVStartdate, stOVEndDate, stString, stOvType As String
        Dim intFrom, intTo As Integer
        oTst = objEN.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        otemp3 = objEN.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        otemp4 = objEN.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        otemp4.DoQuery("Select  T0.[Startdate],(T0.[TermDate]),T0.Salary,isnull(T0.U_Z_Rate,0) from OHEM T0 where Empid=" & objEN.EmpId)
        If otemp4.RecordCount > 0 Then
            stString = "select T0.U_Z_CompNo , U_Z_FromDate,U_Z_EndDate,empID from OHEM T0 inner join [@Z_OADM] T1 on T0.U_Z_CompNo=T1.U_Z_CompCode where empid=" & objEN.EmpId
            oTst.DoQuery(stString)
            If oTst.RecordCount > 0 Then
                intFrom = oTst.Fields.Item(1).Value
                intTo = oTst.Fields.Item(2).Value
                If objEN.Month = 2 Then
                    If intTo > 28 Then
                        intTo = 28
                    End If
                End If
            Else
                intFrom = 25
                intTo = 25
            End If
            Dim dtstartdate, dtenddate As Date
            Dim intDiffYear, IntDiffMonth As Integer
            Dim dblSalary, dblnoofdays As Double
            dblHourlyRate = otemp4.Fields.Item(3).Value
            IntDiffMonth = 0
            intDiffYear = 0
            dtstartdate = otemp4.Fields.Item(0).Value
            dtenddate = otemp4.Fields.Item(1).Value
            If Year(dtenddate) = 1899 Then
                Select Case objEN.Month
                    Case 1, 3, 5, 7, 8, 10, 12
                        dtenddate = objEN.Year.ToString("0000") & "-" & objEN.Month.ToString("00") & "-31"
                    Case 4, 6, 9, 11
                        dtenddate = objEN.Year.ToString("0000") & "-" & objEN.Month.ToString("00") & "-30"
                    Case 2
                        dtenddate = objEN.Year.ToString("0000") & "-" & objEN.Month.ToString("00") & "-28"
                End Select
                '  dtenddate = objEN.Year & "-" & objEN.Month.ToString("00") & "-" & intTo.ToString("00")
            Else
                dtenddate = dtenddate
            End If

            If Year(dtenddate) = objEN.Year And Month(dtenddate) = objEN.Month Then
                dtenddate = dtenddate
            Else
                '  dtenddate = dtenddate 'objEN.Year & "-" & objEN.Month.ToString("00") & "-" & intTo.ToString("00")
                Select Case objEN.Month
                    Case 1, 3, 5, 7, 8, 10, 12
                        dtenddate = objEN.Year.ToString("0000") & "-" & objEN.Month.ToString("00") & "-31"
                    Case 4, 6, 9, 11
                        dtenddate = objEN.Year.ToString("0000") & "-" & objEN.Month.ToString("00") & "-30"
                    Case 2
                        dtenddate = objEN.Year.ToString("0000") & "-" & objEN.Month.ToString("00") & "-28"
                End Select
            End If
            'dblSalary = aBasic
            Dim dblDiff As Decimal
            dblnoofdays = GetnumberofworkgDays(objEN.Year, objEN.Month, objEN.EmpId, objEN.SapCompany)
            If Year(dtstartdate) = 1899 Then
                intDiffYear = 0
                dblDiff = 0
            Else
                dtstartdate = dtstartdate
                dblDiff = DateDiff(DateInterval.Year, dtstartdate, dtenddate)
                intDiffYear = (DateDiff(DateInterval.Month, dtstartdate, dtenddate) / 12.0)
                'dblDiff = (DateDiff(DateInterval.Month, dtstartdate, dtenddate) / 12.0)
                dblDiff = (DateDiff(DateInterval.Day, dtstartdate, dtenddate) + 1 / 365.0)
            End If
            If Year(dtenddate) = 1899 Then
                intDiffYear = 0
                dblDiff = 0
            Else
                dtenddate = dtenddate
                intDiffYear = DateDiff(DateInterval.Year, dtstartdate, dtenddate)
                dblDiff = (DateDiff(DateInterval.Month, dtstartdate, dtenddate) / 12.0)
                dblDiff = (DateDiff(DateInterval.Day, dtstartdate, dtenddate) / 365.0)
            End If
            dblDiff = Math.Round(dblDiff, 2)
            Return dblDiff
        End If
        Return 0
    End Function
    Public Function GetnumberofworkgDays(ByVal aYear As Integer, ByVal aMonth As Integer, ByVal aEmpId As Integer, ByVal oCOmpany As SAPbobsCOM.Company) As Integer
        Dim oTest As SAPbobsCOM.Recordset
        Dim dblDays As Double
        oTest = oCOmpany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        oTest.DoQuery("Select * from OHEM where empID=" & aEmpId)
        dblDays = 0
        If oTest.Fields.Item("U_Z_WorkCode").Value <> "" Then
            Dim stCode As String
            stCode = "Select DocEntry  from ""@Z_OEWO"" where ""U_Z_Code""='" & oTest.Fields.Item("U_Z_WorkCode").Value & "'"
            stCode = "Select ""U_Z_Days"" from ""@Z_EWO1"" where ""U_Z_Month""=" & aMonth & " and ""DocEntry"" in (" & stCode & ")"
            oTest.DoQuery(stCode)
            If oTest.RecordCount > 0 Then
                dblDays = oTest.Fields.Item(0).Value
            End If
        End If
        If dblDays = 0 Then
            oTest.DoQuery("Select U_Z_DAYS from [@Z_WORK] where U_Z_MONTH=" & aMonth & " and U_Z_YEAR=" & aYear)
            If oTest.RecordCount > 0 Then
                dblDays = oTest.Fields.Item(0).Value
            Else
                dblDays = 22
            End If
        End If
        Return dblDays
    End Function
End Class
