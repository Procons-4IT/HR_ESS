Imports System
Imports System.Globalization
Imports System.Data
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.IO
Imports System.Data.Odbc
Imports DataAccess
Imports EN
Public Class DynamicApprovalDA
    Dim objEN As DynamicApprovalEN = New DynamicApprovalEN()
    Dim objDA As DBConnectionDA = New DBConnectionDA()
    Dim HeadDocEntry, UserLineId, LeaveType As String

    Dim SmtpServer As New Net.Mail.SmtpClient()
    Dim mail As New Net.Mail.MailMessage
    Dim mailServer As String
    Dim mailPort As String
    Dim mailId As String
    Dim mailUser As String
    Dim mailPwd As String
    Dim mailSSL As String
    Dim toID As String
    Dim ccID As String
    Dim mType As String
    Dim path As String
    Dim sQuery As String
    Dim strEmpName As String
    Public Sub New()
        objDA.con = New SqlConnection(objDA.GetConnection)
    End Sub
    Public Function GetUserCode(ByVal objEN As DynamicApprovalEN) As String
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
    Public Function GetEmpUserid(ByVal objEN As DynamicApprovalEN) As Integer
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
    Public Function getLeaveType(ByVal aCode As String) As String
        objDA.strQuery = "select T0.U_Z_LveType from [@Z_HR_OAPPT] T0 JOIN [@Z_HR_APPT2] T1 on T0.DocEntry=T1.DocEntry where T1.U_Z_AUser ='" & aCode & "'  and T0.U_Z_DocType='LveReq'"
        objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
        objDA.sqlda.Fill(objDA.dss4)
        If objDA.dss4.Tables(0).Rows.Count > 0 Then
            For intRow As Integer = 0 To objDA.dss4.Tables(0).Rows.Count - 1
                If LeaveType = "" Then
                    LeaveType = "'" & objDA.dss4.Tables(0).Rows(intRow)(0).ToString() & "'"
                Else
                    LeaveType = LeaveType & " ,'" & objDA.dss4.Tables(0).Rows(intRow)(0).ToString() & "'"
                End If
            Next
            Return LeaveType
        Else
            Return "99999"
        End If
    End Function
    Public Function GetEmpId(ByVal aCode As String) As String
        Dim EmpId As String = ""
        objDA.strQuery = "select distinct( T2.U_Z_OUser) from [@Z_HR_OAPPT] T0 JOIN [@Z_HR_APPT2] T1 on T0.DocEntry=T1.DocEntry JOIN [@Z_HR_APPT1] T2 on T0.DocEntry=T2.DocEntry where T1.U_Z_AUser ='" & aCode & "'  and T0.U_Z_DocType='LveReq'"
        objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
        objDA.sqlda.Fill(objDA.dss4)
        If objDA.dss4.Tables(0).Rows.Count > 0 Then
            For intRow As Integer = 0 To objDA.dss4.Tables(0).Rows.Count - 1
                If EmpId = "" Then
                    EmpId = "'" & objDA.dss4.Tables(0).Rows(intRow)(0).ToString() & "'"
                Else
                    EmpId = EmpId & " ,'" & objDA.dss4.Tables(0).Rows(intRow)(0).ToString() & "'"
                End If
            Next
            Return EmpId
        Else
            Return "99999"
        End If
    End Function
    Public Function InitializationApproval(ByVal objEN As DynamicApprovalEN) As DataSet
        Try
            Select Case objEN.HeaderType
                Case "Train"
                    Select Case objEN.HistoryType
                        Case "RegTra"
                            objDA.strQuery = "  select T0.Code,T4.U_Z_EmpID as 'TAEmpID',T0.U_Z_HREmpID,T0.U_Z_HREmpName,T0.U_Z_TrainCode,T0.U_Z_CourseCode,T0.U_Z_CourseName,T0.U_Z_CourseTypeDesc,Convert(varchar(10),T0.U_Z_Startdt,103) AS U_Z_Startdt,Convert(Varchar(10),T0.U_Z_Enddt,103) AS U_Z_Enddt,"
                            objDA.strQuery += " Case T0.U_Z_AppRequired when 'Y' then 'Yes' else 'No' End as  'U_Z_AppRequired',Convert(Varchar(10),T0.U_Z_AppReqDate,103) AS 'U_Z_AppReqDate',CONVERT(VARCHAR(8),T0.U_Z_ReqTime,108) AS 'U_Z_ReqTime',"
                            objDA.strQuery += " T6.U_Z_TrainLoc,CAST(T6.U_Z_EstExpe as decimal(10,2)) AS U_Z_EstExpe,cast(T5.U_Z_AttCost as decimal(10,2)) AS U_Z_AttCost,Case T0.U_Z_AppStatus when 'P' then 'Pending' when 'A' then 'Approved' when 'R' then 'Rejected' end AS U_Z_AppStatus, "
                            objDA.strQuery += " isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_CurApprover ),'') AS 'U_Z_CurApprover',isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_NxtApprover ),'') AS 'U_Z_NxtApprover' from [@Z_HR_TRIN1] T0 JOIN OHEM T4 ON T0.U_Z_HREmpID = T4.empID and (T0.""U_Z_AppStatus""='P' or T0.""U_Z_AppStatus""='-') "
                            objDA.strQuery += "  left join [@Z_HR_OTRIN] T5 on T5.U_Z_TrainCode=T0.U_Z_TrainCode"
                            objDA.strQuery += " left join [@Z_HR_ONTREQ] T6 on T6.DocEntry=T5.U_Z_NewTrainCode"
                             objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T6.U_Z_ApproveId = T3.DocEntry  "
                            objDA.strQuery += " JOIN [@Z_HR_APPT2] T2 ON T3.DocEntry = T2.DocEntry "
                            objDA.strQuery += " And (T0.U_Z_CurApprover = '" + objEN.UserCode + "' OR T0.U_Z_NxtApprover = '" + objEN.UserCode + "')"
                            objDA.strQuery += " And isnull(T2.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y' and  isnull(T0.U_Z_AppRequired,'N')='Y' and  T2.U_Z_AUser = '" + objEN.UserCode + "' And T3.U_Z_DocType = '" + objEN.HeaderType + "' Order by Convert(Numeric,T0.Code) desc "
                        Case "NewTra"
                            objDA.strQuery = "select T0.DocEntry,T4.U_Z_EmpID as 'TAEmpID',Convert(Varchar(10),U_Z_ReqDate,103) AS U_Z_ReqDate,T0.U_Z_HREmpID,U_Z_HREmpName,U_Z_CourseName,U_Z_CourseDetails,convert(varchar(10),U_Z_TrainFrdt,103) as U_Z_TrainFrdt,convert(varchar(10),U_Z_TrainTodt,103) as U_Z_TrainTodt,cast(U_Z_TrainCost as decimal(10,2)) AS U_Z_TrainCost,U_Z_Notes,"
                            objDA.strQuery += "  Case U_Z_AppRequired when 'Y' then 'Yes' else 'No' End as  'U_Z_AppRequired',Convert(Varchar(10),U_Z_AppReqDate,103) AS 'U_Z_AppReqDate',CONVERT(VARCHAR(8),U_Z_ReqTime,108) AS 'U_Z_ReqTime',CAST(isnull(""U_Z_EstExpe"",0) as decimal(10,2)) AS U_Z_EstExpe,U_Z_TrainLoc,"
                            objDA.strQuery += " Case U_Z_AppStatus when 'P' then 'Pending' when 'A' then 'Approved' when 'R' then 'Rejected' end AS U_Z_AppStatus, isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_CurApprover ),'') AS 'U_Z_CurApprover',isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_NxtApprover ),'') AS 'U_Z_NxtApprover'  from [@Z_HR_ONTREQ] T0 JOIN OHEM T4 ON T0.U_Z_HREmpID = T4.empID and (T0.""U_Z_AppStatus""='P' or T0.""U_Z_AppStatus""='-') "
                            objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T0.U_Z_ApproveId = T3.DocEntry  "
                            objDA.strQuery += " JOIN [@Z_HR_APPT2] T2 ON T3.DocEntry = T2.DocEntry "
                            objDA.strQuery += " And (T0.U_Z_CurApprover = '" + objEN.UserCode + "' OR T0.U_Z_NxtApprover = '" + objEN.UserCode + "')"
                            objDA.strQuery += " And isnull(T2.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y' and  isnull(T0.U_Z_AppRequired,'N')='Y' and  T2.U_Z_AUser = '" + objEN.UserCode + "' And T3.U_Z_DocType = '" + objEN.HeaderType + "' Order by T0.DocEntry desc "
                    End Select
                Case "EmpLife"
                    Select Case objEN.HistoryType
                        Case "EmpPro"
                            objDA.strQuery = " Select ""Code"",R3.U_Z_EmpID as 'TAEmpID',T0.""U_Z_EmpId"",T0.""U_Z_FirstName"",T0.U_Z_Dept,T1.""U_Z_DeptName"",""U_Z_PosName"",""U_Z_JobName"",""U_Z_OrgName"",Convert(Varchar(10),U_Z_ProJoinDate,103) AS ""U_Z_ProJoinDate"",CAST(isnull(""U_Z_IncAmount"",0) as decimal(10,2)) AS ""U_Z_IncAmount"",Convert(Varchar(10),U_Z_EffFromdt,103) AS ""U_Z_EffFromdt"",Convert(Varchar(10),U_Z_EffTodt,103) AS ""U_Z_EffTodt"","
                            objDA.strQuery += " Case U_Z_AppRequired when 'Y' then 'Yes' else 'No' End as  'U_Z_AppRequired',Convert(Varchar(10),U_Z_AppReqDate,103) AS 'U_Z_AppReqDate',CONVERT(VARCHAR(8),U_Z_ReqTime,108) AS 'U_Z_ReqTime',"
                            objDA.strQuery += " Case U_Z_AppStatus when 'P' then 'Pending' when 'A' then 'Approved' when 'R' then 'Rejected' end AS U_Z_AppStatus, isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_CurApprover ),'') AS 'U_Z_CurApprover',isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_NxtApprover ),'') AS 'U_Z_NxtApprover' "
                            objDA.strQuery += " From ""@Z_HR_HEM2"" T0 JOIN OHEM R3 on R3.""empID""=T0.""U_Z_EmpId"" Join  [@Z_HR_APPT3] T1 ON R3.""dept"" = T1.U_Z_DeptCode and (T0.""U_Z_AppStatus""='P' or T0.""U_Z_AppStatus""='-')"
                            objDA.strQuery += " JOIN [@Z_HR_APPT2] T2 ON T1.DocEntry = T2.DocEntry "
                            objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T2.DocEntry = T3.DocEntry AND T0.""U_Z_Posting""='N'"
                            objDA.strQuery += " And (T0.U_Z_CurApprover = '" + objEN.UserCode + "' OR T0.U_Z_NxtApprover = '" + objEN.UserCode + "')"
                            objDA.strQuery += " And isnull(T2.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y' and  isnull(T0.U_Z_AppRequired,'N')='Y' and  T2.U_Z_AUser = '" + objEN.UserCode + "' And T3.U_Z_DocType = '" + objEN.HeaderType + "' Order by Convert(Numeric,T0.Code) Desc"
                        Case "EmpPos"
                            objDA.strQuery = " select ""Code"",R3.U_Z_EmpID as 'TAEmpID',T0.""U_Z_EmpId"",T0.""U_Z_FirstName"",T0.U_Z_Dept,T1.""U_Z_DeptName"",""U_Z_PosCode"",""U_Z_PosName"",""U_Z_JobName"",""U_Z_OrgCode"",""U_Z_OrgName"","
                            objDA.strQuery += " Convert(Varchar(10),U_Z_NewPosDate,103) AS ""U_Z_NewPosDate"",Convert(Varchar(10),U_Z_EffFromdt,103) AS ""U_Z_EffFromdt"",Convert(Varchar(10),U_Z_EffTodt,103) AS ""U_Z_EffTodt"", "
                            objDA.strQuery += " Case U_Z_AppRequired when 'Y' then 'Yes' else 'No' End as  'U_Z_AppRequired',Convert(Varchar(10),U_Z_AppReqDate,103) AS 'U_Z_AppReqDate',CONVERT(VARCHAR(8),U_Z_ReqTime,108) AS 'U_Z_ReqTime',"
                            objDA.strQuery += " Case U_Z_AppStatus when 'P' then 'Pending' when 'A' then 'Approved' when 'R' then 'Rejected' end AS U_Z_AppStatus, isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_CurApprover ),'') AS 'U_Z_CurApprover',isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_NxtApprover ),'') AS 'U_Z_NxtApprover' from ""@Z_HR_HEM4"" T0 JOIN OHEM R3 on R3.""empID""=T0.""U_Z_EmpId"" Join  [@Z_HR_APPT3] T1 ON R3.""dept"" = T1.U_Z_DeptCode and (T0.""U_Z_AppStatus""='P' or T0.""U_Z_AppStatus""='-')  "
                            objDA.strQuery += " JOIN [@Z_HR_APPT2] T2 ON T1.DocEntry = T2.DocEntry "
                            objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T2.DocEntry = T3.DocEntry AND T0.""U_Z_Posting""='N'"
                            objDA.strQuery += " And (T0.U_Z_CurApprover = '" + objEN.UserCode + "' OR T0.U_Z_NxtApprover = '" + objEN.UserCode + "')"
                            objDA.strQuery += " And isnull(T2.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y' and  isnull(T0.U_Z_AppRequired,'N')='Y' and  T2.U_Z_AUser = '" + objEN.UserCode + "' And T3.U_Z_DocType = '" + objEN.HeaderType + "' Order by Convert(Numeric,Code) Desc"
                        Case "PerObj"
                            objDA.strQuery = " select ""U_DocEntry"",R3.U_Z_EmpID as 'TAEmpID',T0.""U_Empid"",T0.""U_Empname"",U_DeptCode,U_PeoobjCode,U_PeoobjName,U_PeoCatDesc,CAST(isnull(""U_Weight"",0) as decimal(10,2)) AS U_Weight,U_Remarks,U_PeoCategory,"
                            objDA.strQuery += " Case U_Z_AppRequired when 'Y' then 'Yes' else 'No' End as  'U_Z_AppRequired',Convert(Varchar(10),U_Z_AppReqDate,103) AS 'U_Z_AppReqDate',CONVERT(VARCHAR(8),U_Z_ReqTime,108) AS 'U_Z_ReqTime',"
                            objDA.strQuery += " Case U_Z_AppStatus when 'P' then 'Pending' when 'A' then 'Approved' when 'R' then 'Rejected' end AS U_Z_AppStatus, isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_CurApprover ),'') AS 'U_CurApprover',isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_NxtApprover ),'') AS 'U_NxtApprover' from ""U_PEOPLEOBJ"" T0 JOIN OHEM R3 on R3.""empID""=T0.""U_Empid"" Join  [@Z_HR_APPT3] T1 ON R3.""dept"" = T1.U_Z_DeptCode and (T0.""U_Z_AppStatus""='P' or T0.""U_Z_AppStatus""='-')  "
                            objDA.strQuery += " JOIN [@Z_HR_APPT2] T2 ON T1.DocEntry = T2.DocEntry "
                            objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T2.DocEntry = T3.DocEntry "
                            objDA.strQuery += " And (T0.U_CurApprover = '" + objEN.UserCode + "' OR T0.U_NxtApprover = '" + objEN.UserCode + "')"
                            objDA.strQuery += " And isnull(T2.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y' and  isnull(T0.U_Z_AppRequired,'N')='Y' and  T2.U_Z_AUser = '" + objEN.UserCode + "' And T3.U_Z_DocType = '" + objEN.HeaderType + "' Order by U_DocEntry Desc"

                        Case "PerHour"
                            '    Dim strEmpId As String = GetEmpId(objEN.UserCode)
                            objDA.strQuery = "Select * FROM (Select Distinct T0.""Code"" as ""Code"",T4.dept as 'DeptCode' ,T0.""U_Z_EMPID"",T4.U_Z_EmpID as 'TAEmpID',T0.""U_Z_EMPNAME"",convert(varchar(10),""U_Z_StartDate"",103) AS ""U_Z_StartDate"","
                            objDA.strQuery += " convert(varchar(10),""U_Z_EndDate"",103) AS ""U_Z_EndDate"" ,T0.""U_Z_NoofDays"",U_Z_FromTime,""U_Z_AppRemarks"",U_Z_ToTime,Convert(Varchar,""U_Z_Notes"") as ""U_Z_Notes"","
                            objDA.strQuery += " case ""U_Z_Status"" when 'P' then 'Pending' when 'R' then 'Rejected' "
                            objDA.strQuery += " when 'A' then 'Approved' end as ""U_Z_Status"", "
                            objDA.strQuery += " Case U_Z_AppRequired when 'Y' then 'Yes' else 'No' End as  'U_Z_AppRequired',convert(varchar(10),""U_Z_AppReqDate"",103) AS U_Z_AppReqDate,CONVERT(VARCHAR(8),U_Z_ReqTime,108) AS 'U_Z_ReqTime'"
                            objDA.strQuery += " , isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_CurApprover ),'') AS 'U_Z_CurApprover',isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_NxtApprover ),'') AS 'U_Z_NxtApprover'   from ""@Z_PAY_OLETRANS1"" T0 JOIN OHEM T4 ON T0.U_Z_EMPID = T4.empID and T0.""U_Z_TransType""='P' and (T0.""U_Z_Status""='P' or T0.""U_Z_Status""='-') "
                            objDA.strQuery += " JOIN [@Z_HR_APPT3] T1 ON T4.dept = T1.U_Z_DeptCode "
                            objDA.strQuery += " JOIN [@Z_HR_APPT2] T2 ON T1.DocEntry = T2.DocEntry "
                            objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T2.DocEntry = T3.DocEntry  "
                            objDA.strQuery += " And isnull(T2.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y' and  isnull(T0.U_Z_AppRequired,'N')='Y' and  T2.U_Z_AUser = '" + objEN.UserCode + "'"
                            objDA.strQuery += " And (T0.U_Z_CurApprover = '" + objEN.UserCode + "' OR T0.U_Z_NxtApprover = '" + objEN.UserCode + "')"
                            objDA.strQuery += " And T3.U_Z_DocType = '" + objEN.HeaderType + "' ) T20 Order by Convert(Numeric,T20.Code) Desc"


                    End Select
                Case "ExpCli"
                    objDA.strQuery = " Select Code,T0.U_Z_EmpID,T4.U_Z_EmpID as 'TAEmpID',U_Z_EmpName,Convert(Varchar(10),U_Z_SubDt,103) AS U_Z_SubDt,Convert(Varchar(10),U_Z_Claimdt,103) AS U_Z_Claimdt,U_Z_ExpType,U_Z_Currency,CAST(isnull(U_Z_CurAmt,0) as decimal(10,2)) AS U_Z_CurAmt,U_Z_UsdAmt,U_Z_ReimAmt,isnull(U_Z_Attachment,'') AS U_Z_Attachment,Case U_Z_AppStatus when 'P' then 'Pending' when 'A' then 'Approved' when 'R' then 'Rejected' end AS U_Z_AppStatus,"
                    objDA.strQuery += " U_Z_CurApprover,U_Z_NxtApprover,U_Z_Client,U_Z_Project,""U_Z_Month"",""U_Z_Year"", "
                    objDA.strQuery += " Case U_Z_AppRequired when 'Y' then 'Yes' else 'No' End as  'U_Z_AppRequired',Convert(Varchar(10),U_Z_AppReqDate,103) AS U_Z_AppReqDate,CONVERT(VARCHAR(8),U_Z_ReqTime,108) AS 'U_Z_ReqTime'"
                    objDA.strQuery += " From [@Z_HR_EXPCL] T0 JOIN OHEM T4  ON T0.U_Z_EmpID = T4.empID  and (T0.""U_Z_AppStatus""='P' or T0.""U_Z_AppStatus""='-') "
                    objDA.strQuery += " JOIN [@Z_HR_APPT1] T1 ON T4.empID = T1.U_Z_OUser "
                    objDA.strQuery += " JOIN [@Z_HR_APPT2] T2 ON T1.DocEntry = T2.DocEntry "
                    objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T2.DocEntry = T3.DocEntry  "
                    objDA.strQuery += " And (T0.U_Z_CurApprover = '" + objEN.UserCode + "' OR T0.U_Z_NxtApprover = '" + objEN.UserCode + "')"
                    objDA.strQuery += " And isnull(T2.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y' and  isnull(T0.U_Z_AppRequired,'N')='Y' and  T2.U_Z_AUser = '" + objEN.UserCode + "' And T3.U_Z_DocType = '" + objEN.HeaderType + "' Order by Convert(Numeric,Code) Desc"
                Case "TraReq"
                    objDA.strQuery = " Select T0.DocEntry,T0.U_Z_EmpId,T4.U_Z_EmpID as 'TAEmpID',U_Z_EmpName,Convert(Varchar(10),U_Z_DocDate,103) AS U_Z_DocDate,U_Z_TraName,U_Z_TraStLoc,U_Z_TraEdLoc,Convert(Varchar(10),U_Z_TraStDate,103) AS U_Z_TraStDate,Convert(Varchar(10),U_Z_TraEndDate,103) AS U_Z_TraEndDate,"
                    objDA.strQuery += " Case U_Z_AppStatus when 'P' then 'Pending' when 'A' then 'Approved' when 'R' then 'Rejected' end AS U_Z_AppStatus,"
                    objDA.strQuery += " isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_CurApprover ),'') AS 'U_Z_CurApprover',isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_NxtApprover ),'') AS 'U_Z_NxtApprover', "
                    objDA.strQuery += " Case U_Z_AppRequired when 'Y' then 'Yes' else 'No' End as  'U_Z_AppRequired',Convert(Varchar(10),U_Z_AppReqDate,103) AS U_Z_AppReqDate,CONVERT(VARCHAR(8),U_Z_ReqTime,108) AS 'U_Z_ReqTime'"
                    objDA.strQuery += " From [@Z_HR_OTRAREQ] T0 JOIN OHEM T4 ON T0.U_Z_EmpId = T4.empID and (T0.""U_Z_AppStatus""='P' or T0.""U_Z_AppStatus""='-') "
                    objDA.strQuery += " JOIN [@Z_HR_APPT1] T1 ON T4.empID = T1.U_Z_OUser "
                    objDA.strQuery += " JOIN [@Z_HR_APPT2] T2 ON T1.DocEntry = T2.DocEntry "
                    objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T2.DocEntry = T3.DocEntry  "
                    objDA.strQuery += " And (T0.U_Z_CurApprover = '" + objEN.UserCode + "' OR T0.U_Z_NxtApprover = '" + objEN.UserCode + "')"
                    objDA.strQuery += " And isnull(T2.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y' and  isnull(T0.U_Z_AppRequired,'N')='Y' and  T2.U_Z_AUser = '" + objEN.UserCode + "' And T3.U_Z_DocType = '" + objEN.HeaderType + "' Order by T0.DocEntry desc "
                Case "LveReq"
                    Select Case objEN.HistoryType
                        Case "LveReq"
                            Dim strLvetype As String = getLeaveType(objEN.UserCode)
                            objDA.strQuery = "Select * FROM (Select Distinct T0.""Code"" as ""Code"",T0.""U_Z_EMPID"",T4.U_Z_EmpID as 'TAEmpID',T0.""U_Z_EMPNAME"",""U_Z_TrnsCode"",isnull(""U_Z_LeaveName"",'') AS U_Z_LeaveName,convert(varchar(10),""U_Z_StartDate"",103) AS ""U_Z_StartDate"","
                            objDA.strQuery += " convert(varchar(10),""U_Z_EndDate"",103) AS ""U_Z_EndDate"" ,cast(T0.U_Z_NoofDays as decimal(10,2)) AS ""U_Z_NoofDays"",cast(T0.U_Z_LevBal as decimal(10,2)) AS 'U_Z_LevBal',Convert(Varchar,""U_Z_Notes"") as ""U_Z_Notes"",convert(varchar(10),"
                            objDA.strQuery += " ""U_Z_ReJoiNDate"",103) AS ""U_Z_ReJoiNDate"",DateName( month , DateAdd( month , isnull(""U_Z_Month"",1) , -1 ) ) AS ""U_Z_Month"",""U_Z_Year"",case ""U_Z_Status"" when 'P' then 'Pending' when 'R' then 'Rejected' "
                            objDA.strQuery += " when 'A' then 'Approved' end as ""U_Z_Status"", "
                            objDA.strQuery += " Case U_Z_AppRequired when 'Y' then 'Yes' else 'No' End as  'U_Z_AppRequired',convert(varchar(10),""U_Z_AppReqDate"",103) AS U_Z_AppReqDate,CONVERT(VARCHAR(8),U_Z_ReqTime,108) AS 'U_Z_ReqTime'"
                            objDA.strQuery += " , isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_CurApprover ),'') AS 'U_Z_CurApprover',isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_NxtApprover ),'') AS 'U_Z_NxtApprover'   from ""@Z_PAY_OLETRANS1"" T0 JOIN OHEM T4 ON T0.U_Z_EMPID = T4.empID and (T0.""U_Z_Status""='P' or T0.""U_Z_Status""='-') "
                            objDA.strQuery += " JOIN [@Z_HR_APPT1] T1 ON T4.empID = T1.U_Z_OUser "
                            objDA.strQuery += " JOIN [@Z_HR_APPT2] T2 ON T1.DocEntry = T2.DocEntry "
                            objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T2.DocEntry = T3.DocEntry  "
                            objDA.strQuery += " And isnull(T2.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y' and  isnull(T0.U_Z_AppRequired,'N')='Y' and  T2.U_Z_AUser = '" + objEN.UserCode + "'"
                            objDA.strQuery += " And (T0.U_Z_CurApprover = '" + objEN.UserCode + "' OR T0.U_Z_NxtApprover = '" + objEN.UserCode + "')"
                            objDA.strQuery += " And T3.U_Z_DocType = '" + objEN.HeaderType + "' AND ""U_Z_TrnsCode"" in (" & strLvetype & ")) T20 Order by Convert(Numeric,T20.Code) Desc"
                        Case "RetLve"
                            Dim strLvetype As String = getLeaveType(objEN.UserCode)
                            objDA.strQuery = "Select * FROM (Select Distinct T0.""Code"" as ""Code"",T0.""U_Z_EMPID"",T4.U_Z_EmpID as 'TAEmpID',T0.""U_Z_EMPNAME"",""U_Z_TrnsCode"",isnull(""U_Z_LeaveName"",'') AS U_Z_LeaveName,convert(varchar(10),""U_Z_StartDate"",103) AS ""U_Z_StartDate"","
                            objDA.strQuery += " convert(varchar(10),""U_Z_EndDate"",103) AS ""U_Z_EndDate"" ,cast(T0.U_Z_NoofDays as decimal(10,2)) AS ""U_Z_NoofDays"",convert(varchar(10),T0.""U_Z_RetJoiNDate"",103)  AS ""U_Z_RetJoiNDate"",""U_Z_RAppRemarks"",T0.""U_Z_LevBal"" AS 'U_Z_LevBal',Convert(Varchar,""U_Z_Notes"") as ""U_Z_Notes"",convert(varchar(10),"
                            objDA.strQuery += " ""U_Z_ReJoiNDate"",103) AS ""U_Z_ReJoiNDate"",DateName( month , DateAdd( month , isnull(""U_Z_Month"",1) , -1 ) ) AS ""U_Z_Month"",""U_Z_Year"",case ""U_Z_RStatus"" when 'P' then 'Pending' when 'R' then 'Rejected' "
                            objDA.strQuery += " when 'A' then 'Approved' end as ""U_Z_RStatus"", "
                            objDA.strQuery += " Case U_Z_AppRequired when 'Y' then 'Yes' else 'No' End as  'U_Z_AppRequired',convert(varchar(10),""U_Z_AppReqDate"",103) AS U_Z_AppReqDate,CONVERT(VARCHAR(8),U_Z_ReqTime,108) AS 'U_Z_ReqTime'"
                            objDA.strQuery += " , isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_CurApprover ),'') AS 'U_Z_CurApprover',isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_NxtApprover ),'') AS 'U_Z_NxtApprover'   from ""@Z_PAY_OLETRANS1"" T0 JOIN OHEM T4 ON T0.U_Z_EMPID = T4.empID and T0.""U_Z_TransType""='R' and T0.""U_Z_Status""='A' and (T0.""U_Z_RStatus""='P' or T0.""U_Z_RStatus""='-') "
                            objDA.strQuery += " JOIN [@Z_HR_APPT1] T1 ON T4.empID = T1.U_Z_OUser "
                            objDA.strQuery += " JOIN [@Z_HR_APPT2] T2 ON T1.DocEntry = T2.DocEntry "
                            objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T2.DocEntry = T3.DocEntry  "
                            objDA.strQuery += " And isnull(T2.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y' and  isnull(T0.U_Z_AppRequired,'N')='Y' and  T2.U_Z_AUser = '" + objEN.UserCode + "'"
                            objDA.strQuery += " And (T0.U_Z_CurApprover = '" + objEN.UserCode + "' OR T0.U_Z_NxtApprover = '" + objEN.UserCode + "')"
                            objDA.strQuery += " And T3.U_Z_DocType = '" + objEN.HeaderType + "' AND ""U_Z_TrnsCode"" in (" & strLvetype & ")) T20 Order by Convert(Numeric,T20.Code) Desc"
                        Case "BankTime"
                            Dim strLvetype As String = getLeaveType(objEN.UserCode)
                            objDA.strQuery = "Select * FROM (Select Distinct T0.""Code"" as ""Code"",T0.""U_Z_EMPID"",T0.""U_Z_EMPNAME"",""U_Z_TrnsCode"",""U_Z_LeaveName"" as ""U_Z_LeaveName"",""U_Z_EmpId1"",convert(varchar(10),""U_Z_StartDate"",103) AS ""U_Z_StartDate"","
                            objDA.strQuery += "cast(T0.U_Z_NoofHours as decimal(10,2)) AS ""U_Z_NoofHours"",cast(T0.U_Z_NoofDays as decimal(10,2)) AS ""U_Z_NoofDays"",Convert(Varchar,""U_Z_Notes"") as ""U_Z_Notes"",Case ""U_Z_CashOut"" when 'Y' then 'Yes' else 'No' end as ""U_Z_CashOut"",case ""U_Z_AppStatus"" when 'P' then 'Pending' when 'R' then 'Rejected' "
                            objDA.strQuery += " when 'A' then 'Approved' end as ""U_Z_AppStatus"",""U_Z_AppRemarks"", "
                            objDA.strQuery += " Case U_Z_AppRequired when 'Y' then 'Yes' else 'No' End as  'U_Z_AppRequired',convert(varchar(10),""U_Z_AppReqDate"",103) AS 'U_Z_AppReqDate',CONVERT(VARCHAR(8),U_Z_ReqTime,108) AS 'U_Z_ReqTime'"
                            objDA.strQuery += " , isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_CurApprover ),'') AS 'U_Z_CurApprover',isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_NxtApprover ),'') AS 'U_Z_NxtApprover'  from ""@Z_PAY_OLADJTRANS1"" T0 JOIN [@Z_HR_APPT1] T1 ON T0.U_Z_EMPID = T1.U_Z_OUser and (T0.""U_Z_AppStatus""='P' or T0.""U_Z_AppStatus""='-') "
                            objDA.strQuery += " JOIN [@Z_HR_APPT2] T2 ON T1.DocEntry = T2.DocEntry "
                            objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T2.DocEntry = T3.DocEntry  "
                            objDA.strQuery += " And isnull(T2.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y' and  isnull(T0.U_Z_AppRequired,'N')='Y' and  T2.U_Z_AUser = '" + objEN.UserCode + "'"
                            objDA.strQuery += " And (T0.U_Z_CurApprover = '" + objEN.UserCode + "' OR T0.U_Z_NxtApprover = '" + objEN.UserCode + "')"
                            objDA.strQuery += " And T3.U_Z_DocType = '" + objEN.HeaderType + "' AND ""U_Z_TrnsCode"" in (" & strLvetype & ")) T20 Order by Convert(Numeric,T20.Code) Desc"
                    End Select
                   
                Case "Rec"
                    Select Case objEN.HistoryType
                        Case "Rec"
                            objDA.strQuery = " Select T0.DocEntry,convert(varchar(10),""U_Z_ReqDate"",103) AS U_Z_ReqDate,T4.U_Z_EmpID as 'TAEmpID',T0.U_Z_EmpCode,U_Z_EmpName,T0.U_Z_DeptCode,T1.U_Z_DeptName,ISNULL(U_Z_PosName, '') as U_Z_PosName,CAST(T0.U_Z_ExpMin as decimal(10,2)) AS U_Z_ExpMin,CAST(T0.U_Z_ExpMax as decimal(10,2)) AS U_Z_ExpMax,CAST(T0.U_Z_Vacancy as decimal(10,2)) AS U_Z_Vacancy,Case U_Z_AppStatus when 'P' then 'Pending' when 'A' then 'Approved' when 'R' then 'Rejected' end AS U_Z_AppStatus,"
                            objDA.strQuery += " Case U_Z_AppRequired when 'Y' then 'Yes' else 'No' End as  'U_Z_AppRequired',convert(varchar(10),""U_Z_AppReqDate"",103) AS 'U_Z_AppReqDate',CONVERT(VARCHAR(8),U_Z_ReqTime,108) AS 'U_Z_ReqTime'"
                            objDA.strQuery += ", isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_CurApprover ),'') AS 'U_Z_CurApprover',isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_NxtApprover ),'') AS 'U_Z_NxtApprover' From [@Z_HR_ORMPREQ] T0 INNER JOIN OHEM T4 ON  T4.empID  = T0.U_Z_EmpCode and (T0.""U_Z_AppStatus""='P' or T0.""U_Z_AppStatus""='-') "
                            objDA.strQuery += " JOIN [@Z_HR_APPT3] T1 ON T0.U_Z_DeptCode = T1.U_Z_DeptCode "
                            objDA.strQuery += " JOIN [@Z_HR_APPT2] T2 ON T1.DocEntry = T2.DocEntry "
                            objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T2.DocEntry = T3.DocEntry  "
                            objDA.strQuery += " And (T0.U_Z_CurApprover = '" + objEN.UserCode + "' OR T0.U_Z_NxtApprover = '" + objEN.UserCode + "')"
                            objDA.strQuery += " And isnull(T2.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y' and  isnull(T0.U_Z_AppRequired,'N')='Y' and   T2.U_Z_AUser = '" + objEN.UserCode + "' And T3.U_Z_DocType = '" + objEN.HeaderType + "' Order by T0.DocEntry Desc"
                        Case "AppShort"
                            objDA.strQuery = " Select T0.DocEntry,T0.U_Z_HRAppID,T0.U_Z_HRAppName,T0.U_Z_ReqNo,convert(varchar(10),""U_Z_AppDate"",103) AS U_Z_AppDate,T1.U_Z_DeptCode,T0.U_Z_DeptName,T0.U_Z_Email,CAST(T0.U_Z_YrExp as decimal(10,2)) AS U_Z_YrExp,T0.U_Z_Skills,Case T0.U_Z_AppStatus when 'P' then 'Pending' when 'A' then 'Approved' when 'R' then 'Rejected' end AS U_Z_AppStatus,"
                            objDA.strQuery += " Case T0.U_Z_AppRequired when 'Y' then 'Yes' else 'No' End as  'U_Z_AppRequired',convert(varchar(10),T0.""U_Z_AppReqDate"",103) AS 'U_Z_AppReqDate',CONVERT(VARCHAR(8),T0.U_Z_ReqTime,108) AS 'U_Z_ReqTime'"
                            objDA.strQuery += " , isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_CurApprover ),'') AS 'U_Z_CurApprover',isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_NxtApprover ),'') AS 'U_Z_NxtApprover' From [@Z_HR_OHEM1] T0 JOIN [@Z_HR_ORMPREQ] T1 ON T1.DocEntry = T0.U_Z_ReqNo and (T0.""U_Z_AppStatus""='P' or T0.""U_Z_AppStatus""='-') "
                            objDA.strQuery += " JOIN [@Z_HR_APPT3] T2 ON T1.U_Z_DeptCode = T2.U_Z_DeptCode"
                            objDA.strQuery += " JOIN [@Z_HR_APPT2] T3 ON T2.DocEntry = T3.DocEntry "
                            objDA.strQuery += " JOIN [@Z_HR_OAPPT] T4 ON T3.DocEntry = T4.DocEntry  "
                            objDA.strQuery += " And (T0.U_Z_CurApprover = '" + objEN.UserCode + "' OR T0.U_Z_NxtApprover = '" + objEN.UserCode + "')"
                            objDA.strQuery += " And isnull(T3.U_Z_AMan,'N')='Y' AND isnull(T4.U_Z_Active,'N')='Y' and  isnull(T0.U_Z_AppRequired,'N')='Y' and  T0.U_Z_AppStatus='P' And T3.U_Z_AUser = '" + objEN.UserCode + "' And T4.U_Z_DocType = '" + objEN.HeaderType + "' Order by T0.DocEntry Desc"
                        Case "IntAppReq"
                            objDA.strQuery = " SELECT ""U_DocEntry"", ""U_Empid"", ""U_Empname"", ""U_EmpPosCode"", ""U_EmpPosName"", ""U_EmpdeptCode"", ""U_EmpdeptName"", ""U_ReqdeptCode"", ""U_ReqdeptName"", ""U_ReqPosCode"", ""U_Remarks"", ""U_ReqPosName"", ""U_RequestCode"",Convert(Varchar(10),""U_ApplyDate"",103) AS ""U_ApplyDate"","
                            objDA.strQuery += "Case U_Z_AppStatus when 'P' then 'Pending' when 'A' then 'Approved' when 'R' then 'Rejected' end AS U_Z_AppStatus, Case U_Z_AppRequired when 'Y' then 'Yes' else 'No' End as  'U_Z_AppRequired',Convert(Varchar(10),U_Z_AppReqDate,103) AS 'U_Z_AppReqDate',CONVERT(VARCHAR(8),U_Z_ReqTime,108) AS 'U_Z_ReqTime'"
                            objDA.strQuery += ", isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_CurApprover ),'') AS 'U_CurApprover',isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_NxtApprover ),'') AS 'U_NxtApprover' From [U_VACPOSITION] T0 JOIN [@Z_HR_APPT3] T1 ON T0.U_EmpdeptCode = T1.U_Z_DeptCode and (T0.""U_Z_AppStatus""='P' or T0.""U_Z_AppStatus""='-') "
                            objDA.strQuery += " JOIN [@Z_HR_APPT2] T2 ON T1.DocEntry = T2.DocEntry "
                            objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T2.DocEntry = T3.DocEntry  "
                            objDA.strQuery += " And (T0.U_CurApprover = '" + objEN.UserCode + "' OR T0.U_NxtApprover = '" + objEN.UserCode + "')"
                            objDA.strQuery += " And isnull(T2.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y' and  isnull(T0.U_Z_AppRequired,'N')='Y' and   T2.U_Z_AUser = '" + objEN.UserCode + "' And T3.U_Z_DocType = '" + objEN.HeaderType + "' Order by T0.U_DocEntry Desc"
                    End Select
            End Select
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds)
            Return objDA.ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ApprovalSummary(ByVal objEN As DynamicApprovalEN) As DataSet
        Try
            Select Case objEN.HeaderType
                Case "Train"
                    Select Case objEN.HistoryType
                        Case "RegTra"
                            objDA.strQuery = "  select T0.Code,T4.U_Z_EmpID as 'TAEmpID',T0.U_Z_HREmpID,T0.U_Z_HREmpName,T0.U_Z_TrainCode,T0.U_Z_CourseCode,T0.U_Z_CourseName,T0.U_Z_CourseTypeDesc,Convert(varchar(10),T0.U_Z_Startdt,103) AS U_Z_Startdt,Convert(Varchar(10),T0.U_Z_Enddt,103) AS U_Z_Enddt,"
                            objDA.strQuery += " Case T0.U_Z_AppRequired when 'Y' then 'Yes' else 'No' End as  'U_Z_AppRequired',Convert(Varchar(10),T0.U_Z_AppReqDate,103) AS 'U_Z_AppReqDate',CONVERT(VARCHAR(8),T0.U_Z_ReqTime,108) AS 'U_Z_ReqTime',"
                            objDA.strQuery += " T6.U_Z_TrainLoc,CAST(T6.U_Z_EstExpe as decimal(10,2)) AS U_Z_EstExpe,CAST(T5.U_Z_AttCost as decimal(10,2)) AS U_Z_AttCost,Case T0.U_Z_AppStatus when 'P' then 'Pending' when 'A' then 'Approved' when 'R' then 'Rejected' end AS U_Z_AppStatus,"
                            objDA.strQuery += "  isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_CurApprover ),'') AS 'U_Z_CurApprover',isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_NxtApprover ),'') AS 'U_Z_NxtApprover' from [@Z_HR_TRIN1] T0 JOIN OHEM T4 ON T0.U_Z_HREmpID = T4.empID"
                            objDA.strQuery += " left join [@Z_HR_OTRIN] T5 on T5.U_Z_TrainCode=T0.U_Z_TrainCode"
                            objDA.strQuery += " left join [@Z_HR_ONTREQ] T6 on T6.DocEntry=T5.U_Z_NewTrainCode"
                            objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T6.U_Z_ApproveId = T3.DocEntry  "
                            objDA.strQuery += " JOIN [@Z_HR_APPT2] T2 ON T3.DocEntry = T2.DocEntry "
                            objDA.strQuery += " And isnull(T2.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y' and  T2.U_Z_AUser = '" + objEN.UserCode + "' And T3.U_Z_DocType = '" + objEN.HeaderType + "' Order by Convert(Numeric,T0.Code) desc "
                        Case "NewTra"
                            objDA.strQuery = "select T0.DocEntry,Convert(Varchar(10),U_Z_ReqDate,103) AS U_Z_ReqDate,T4.U_Z_EmpID as 'TAEmpID',T0.U_Z_HREmpID,U_Z_HREmpName,U_Z_CourseName,U_Z_CourseDetails,convert(varchar(10),U_Z_TrainFrdt,103) as U_Z_TrainFrdt,convert(varchar(10),U_Z_TrainTodt,103) as U_Z_TrainTodt,cast(U_Z_TrainCost as decimal(10,2)) AS U_Z_TrainCost,U_Z_Notes,"
                            objDA.strQuery += " Case U_Z_AppRequired when 'Y' then 'Yes' else 'No' End as  'U_Z_AppRequired',Convert(Varchar(10),U_Z_AppReqDate,103) AS 'U_Z_AppReqDate',CONVERT(VARCHAR(8),U_Z_ReqTime,108) AS 'U_Z_ReqTime',cast(U_Z_EstExpe as decimal(10,2)) AS U_Z_EstExpe,U_Z_TrainLoc,"
                            objDA.strQuery += " Case U_Z_AppStatus when 'P' then 'Pending' when 'A' then 'Approved' when 'R' then 'Rejected' end AS U_Z_AppStatus, isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_CurApprover ),'') AS 'U_Z_CurApprover',isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_NxtApprover ),'') AS 'U_Z_NxtApprover' from [@Z_HR_ONTREQ] T0 JOIN OHEM T4 ON T0.U_Z_HREmpID = T4.empID "
                             objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T0.U_Z_ApproveId = T3.DocEntry  "
                            objDA.strQuery += " JOIN [@Z_HR_APPT2] T2 ON T3.DocEntry = T2.DocEntry "
                            objDA.strQuery += " And isnull(T2.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y' and  T2.U_Z_AUser = '" + objEN.UserCode + "' And T3.U_Z_DocType = '" + objEN.HeaderType + "' Order by T0.DocEntry desc "
                    End Select
                Case "EmpLife"
                    Select Case objEN.HistoryType
                        Case "EmpPro"
                            objDA.strQuery = " Select ""Code"",R3.U_Z_EmpID as 'TAEmpID',T0.""U_Z_EmpId"",T0.""U_Z_FirstName"",T0.U_Z_Dept,T1.""U_Z_DeptName"",""U_Z_PosName"",""U_Z_JobName"",""U_Z_OrgName"",Convert(Varchar(10),U_Z_ProJoinDate,103) AS ""U_Z_ProJoinDate"",CAST(U_Z_IncAmount as decimal(10,2)) AS ""U_Z_IncAmount"",Convert(Varchar(10),U_Z_EffFromdt,103) AS ""U_Z_EffFromdt"",Convert(Varchar(10),U_Z_EffTodt,103) AS ""U_Z_EffTodt"","
                            objDA.strQuery += " Case U_Z_AppRequired when 'Y' then 'Yes' else 'No' End as  'U_Z_AppRequired',Convert(Varchar(10),U_Z_AppReqDate,103) AS 'U_Z_AppReqDate',CONVERT(VARCHAR(8),U_Z_ReqTime,108) AS 'U_Z_ReqTime',"
                            objDA.strQuery += " Case U_Z_AppStatus when 'P' then 'Pending' when 'A' then 'Approved' when 'R' then 'Rejected' end AS U_Z_AppStatus, isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_CurApprover ),'') AS 'U_Z_CurApprover',isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_NxtApprover ),'') AS 'U_Z_NxtApprover' From ""@Z_HR_HEM2"" T0 Join OHEM R3 on R3.""empID""=T0.""U_Z_EmpId"" JOIN [@Z_HR_APPT3] T1 ON R3.""dept"" = T1.U_Z_DeptCode "
                            objDA.strQuery += " JOIN [@Z_HR_APPT2] T2 ON T1.DocEntry = T2.DocEntry "
                            objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T2.DocEntry = T3.DocEntry AND T0.""U_Z_Posting""='N'"
                            objDA.strQuery += " And isnull(T2.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y' and  T2.U_Z_AUser = '" + objEN.UserCode + "' And T3.U_Z_DocType = '" + objEN.HeaderType + "' Order by Convert(Numeric,T0.Code) Desc"
                        Case "EmpPos"
                            objDA.strQuery = " select ""Code"",R3.U_Z_EmpID as 'TAEmpID',T0.""U_Z_EmpId"",T0.""U_Z_FirstName"",T0.U_Z_Dept,T1.""U_Z_DeptName"",""U_Z_PosCode"",""U_Z_PosName"",""U_Z_JobName"",""U_Z_OrgCode"",""U_Z_OrgName"","
                            objDA.strQuery += " Convert(Varchar(10),U_Z_NewPosDate,103) AS ""U_Z_NewPosDate"",Convert(Varchar(10),U_Z_EffFromdt,103) AS ""U_Z_EffFromdt"",Convert(Varchar(10),U_Z_EffTodt,103) AS ""U_Z_EffTodt"","
                            objDA.strQuery += " Case U_Z_AppRequired when 'Y' then 'Yes' else 'No' End as  'U_Z_AppRequired',Convert(Varchar(10),U_Z_AppReqDate,103) AS 'U_Z_AppReqDate',CONVERT(VARCHAR(8),U_Z_ReqTime,108) AS 'U_Z_ReqTime',"
                            objDA.strQuery += " Case U_Z_AppStatus when 'P' then 'Pending' when 'A' then 'Approved' when 'R' then 'Rejected' end AS U_Z_AppStatus, isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_CurApprover ),'') AS 'U_Z_CurApprover',isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_NxtApprover ),'') AS 'U_Z_NxtApprover' from ""@Z_HR_HEM4"" T0 Join OHEM R3 on R3.""empID""=T0.""U_Z_EmpId"" JOIN [@Z_HR_APPT3] T1 ON R3.""dept"" = T1.U_Z_DeptCode "
                            objDA.strQuery += " JOIN [@Z_HR_APPT2] T2 ON T1.DocEntry = T2.DocEntry "
                            objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T2.DocEntry = T3.DocEntry AND T0.""U_Z_Posting""='N'"
                            objDA.strQuery += " And isnull(T2.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y' and  T2.U_Z_AUser = '" + objEN.UserCode + "' And T3.U_Z_DocType = '" + objEN.HeaderType + "' Order by Convert(Numeric,Code) Desc"
                        Case "PerObj"
                            objDA.strQuery = " select ""U_DocEntry"",R3.U_Z_EmpID as 'TAEmpID',T0.""U_Empid"",T0.""U_Empname"",U_DeptCode,U_PeoobjCode,U_PeoobjName,U_PeoCatDesc,U_PeoCategory,CAST(isnull(U_Weight,0) as decimal(10,2)) AS U_Weight,U_Remarks,"
                            objDA.strQuery += " Case U_Z_AppRequired when 'Y' then 'Yes' else 'No' End as  'U_Z_AppRequired',Convert(Varchar(10),U_Z_AppReqDate,103) AS 'U_Z_AppReqDate',CONVERT(VARCHAR(8),U_Z_ReqTime,108) AS 'U_Z_ReqTime',"
                            objDA.strQuery += " Case U_Z_AppStatus when 'P' then 'Pending' when 'A' then 'Approved' when 'R' then 'Rejected' end AS U_Z_AppStatus, isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_CurApprover ),'') AS 'U_CurApprover',isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_NxtApprover ),'') AS 'U_NxtApprover' from ""U_PEOPLEOBJ"" T0 JOIN OHEM R3 on R3.""empID""=T0.""U_Empid"" Join  [@Z_HR_APPT3] T1 ON R3.""dept"" = T1.U_Z_DeptCode "
                            objDA.strQuery += " JOIN [@Z_HR_APPT2] T2 ON T1.DocEntry = T2.DocEntry "
                            objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T2.DocEntry = T3.DocEntry "
                            objDA.strQuery += " And isnull(T2.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y'  and  T2.U_Z_AUser = '" + objEN.UserCode + "' And T3.U_Z_DocType = '" + objEN.HeaderType + "' Order by U_DocEntry Desc"

                        Case "PerHour"
                            '    Dim strEmpId As String = GetEmpId(objEN.UserCode)
                            objDA.strQuery = "Select * FROM (Select Distinct T0.""Code"" as ""Code"",T4.dept as 'DeptCode' ,T0.""U_Z_EMPID"",T4.U_Z_EmpID as 'TAEmpID',T0.""U_Z_EMPNAME"",convert(varchar(10),""U_Z_StartDate"",103) AS ""U_Z_StartDate"","
                            objDA.strQuery += " convert(varchar(10),""U_Z_EndDate"",103) AS ""U_Z_EndDate"" ,CAST(isnull(T0.""U_Z_NoofDays"",0) as decimal(10,2)) AS ""U_Z_NoofDays"",U_Z_FromTime,""U_Z_AppRemarks"",U_Z_ToTime,Convert(Varchar,""U_Z_Notes"") as ""U_Z_Notes"","
                            objDA.strQuery += " case ""U_Z_Status"" when 'P' then 'Pending' when 'R' then 'Rejected' "
                            objDA.strQuery += " when 'A' then 'Approved' end as ""U_Z_Status"", "
                            objDA.strQuery += " Case U_Z_AppRequired when 'Y' then 'Yes' else 'No' End as  'U_Z_AppRequired',convert(varchar(10),""U_Z_AppReqDate"",103) AS U_Z_AppReqDate,CONVERT(VARCHAR(8),U_Z_ReqTime,108) AS 'U_Z_ReqTime'"
                            objDA.strQuery += " , isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_CurApprover ),'') AS 'U_Z_CurApprover',isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_NxtApprover ),'') AS 'U_Z_NxtApprover'   from ""@Z_PAY_OLETRANS1"" T0 JOIN OHEM T4 ON T0.U_Z_EMPID = T4.empID and T0.""U_Z_TransType""='P' "
                            objDA.strQuery += " JOIN [@Z_HR_APPT3] T1 ON T4.dept = T1.U_Z_DeptCode "
                            objDA.strQuery += " JOIN [@Z_HR_APPT2] T2 ON T1.DocEntry = T2.DocEntry "
                            objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T2.DocEntry = T3.DocEntry  "
                            objDA.strQuery += " And isnull(T2.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y'  and  T2.U_Z_AUser = '" + objEN.UserCode + "'"
                            objDA.strQuery += " And T3.U_Z_DocType = '" + objEN.HeaderType + "' ) T20 Order by Convert(Numeric,T20.Code) Desc"
                    End Select
                Case "ExpCli"
                    objDA.strQuery = " Select Code,T0.U_Z_EmpID,T4.U_Z_EmpID as 'TAEmpID',U_Z_EmpName,Convert(Varchar(10),U_Z_SubDt,103) AS U_Z_SubDt,Convert(Varchar(10),U_Z_Claimdt,103) AS U_Z_Claimdt,U_Z_ExpType,U_Z_Currency,CAST(isnull(""U_Z_CurAmt"",0) as decimal(10,2)) AS  U_Z_CurAmt,U_Z_UsdAmt,U_Z_ReimAmt,isnull(U_Z_Attachment,'') AS U_Z_Attachment,Case U_Z_AppStatus when 'P' then 'Pending' when 'A' then 'Approved' when 'R' then 'Rejected' end AS U_Z_AppStatus,"
                    objDA.strQuery += " U_Z_CurApprover,U_Z_NxtApprover,U_Z_Client,U_Z_Project,""U_Z_Month"",""U_Z_Year"", "
                    objDA.strQuery += " Case U_Z_AppRequired when 'Y' then 'Yes' else 'No' End as  'U_Z_AppRequired',Convert(Varchar(10),U_Z_AppReqDate,103) AS U_Z_AppReqDate,CONVERT(VARCHAR(8),U_Z_ReqTime,108) AS 'U_Z_ReqTime'"
                    objDA.strQuery += " From [@Z_HR_EXPCL] T0 JOIN OHEM T4  ON T0.U_Z_EmpID = T4.empID "
                    objDA.strQuery += " JOIN [@Z_HR_APPT1] T1 ON T4.empID = T1.U_Z_OUser "
                    objDA.strQuery += " JOIN [@Z_HR_APPT2] T2 ON T1.DocEntry = T2.DocEntry "
                    objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T2.DocEntry = T3.DocEntry  "
                    objDA.strQuery += " And isnull(T2.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y'  and  T2.U_Z_AUser = '" + objEN.UserCode + "' And T3.U_Z_DocType = '" + objEN.HeaderType + "' Order by Convert(Numeric,Code) Desc"
                Case "TraReq"
                    objDA.strQuery = " Select T0.DocEntry,T0.U_Z_EmpId,T4.U_Z_EmpID as 'TAEmpID',U_Z_EmpName,Convert(Varchar(10),U_Z_DocDate,103) AS U_Z_DocDate,U_Z_TraName,U_Z_TraStLoc,U_Z_TraEdLoc,Convert(Varchar(10),U_Z_TraStDate,103) AS U_Z_TraStDate,Convert(Varchar(10),U_Z_TraEndDate,103) AS U_Z_TraEndDate,Case U_Z_AppStatus when 'P' then 'Pending' when 'A' then 'Approved' when 'R' then 'Rejected' end AS U_Z_AppStatus,"
                    objDA.strQuery += " isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_CurApprover ),'') AS 'U_Z_CurApprover',isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_NxtApprover ),'') AS 'U_Z_NxtApprover',"
                    objDA.strQuery += " Case U_Z_AppRequired when 'Y' then 'Yes' else 'No' End as  'U_Z_AppRequired',Convert(Varchar(10),U_Z_AppReqDate,103) AS U_Z_AppReqDate,CONVERT(VARCHAR(8),U_Z_ReqTime,108) AS 'U_Z_ReqTime'"
                    objDA.strQuery += " From [@Z_HR_OTRAREQ] T0 JOIN OHEM T4 ON T0.U_Z_EmpId = T4.empID "
                    objDA.strQuery += " JOIN [@Z_HR_APPT1] T1 ON T4.empID = T1.U_Z_OUser "
                    objDA.strQuery += " JOIN [@Z_HR_APPT2] T2 ON T1.DocEntry = T2.DocEntry "
                    objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T2.DocEntry = T3.DocEntry  "
                    objDA.strQuery += " And isnull(T2.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y'  and  T2.U_Z_AUser = '" + objEN.UserCode + "' And T3.U_Z_DocType = '" + objEN.HeaderType + "' Order by T0.DocEntry desc "
                Case "LveReq"
                    Select Case objEN.HistoryType
                        Case "LveReq"
                            Dim strLvetype As String = getLeaveType(objEN.UserCode)
                            objDA.strQuery = "Select * FROM (Select Distinct T0.""Code"" as ""Code"",T0.""U_Z_EMPID"",T4.U_Z_EmpID as 'TAEmpID',T0.""U_Z_EMPNAME"",""U_Z_TrnsCode"",isnull(""U_Z_LeaveName"",'') AS U_Z_LeaveName,convert(varchar(10),""U_Z_StartDate"",103) AS ""U_Z_StartDate"","
                            objDA.strQuery += " convert(varchar(10),""U_Z_EndDate"",103) AS ""U_Z_EndDate"" ,cast(T0.U_Z_NoofDays as decimal(10,2)) AS ""U_Z_NoofDays"",cast(T0.U_Z_LevBal as decimal(10,2)) AS 'U_Z_LevBal',Convert(Varchar,""U_Z_Notes"") as ""U_Z_Notes"",convert(varchar(10),"
                            objDA.strQuery += " ""U_Z_ReJoiNDate"",103) AS ""U_Z_ReJoiNDate"",DateName( month , DateAdd( month , isnull(""U_Z_Month"",1) , -1 ) ) AS ""U_Z_Month"",""U_Z_Year"",case ""U_Z_Status"" when 'P' then 'Pending' when 'R' then 'Rejected' "
                            objDA.strQuery += " when 'A' then 'Approved' end as ""U_Z_Status"", "
                            objDA.strQuery += " Case U_Z_AppRequired when 'Y' then 'Yes' else 'No' End as  'U_Z_AppRequired',convert(varchar(10),""U_Z_AppReqDate"",103) AS U_Z_AppReqDate,CONVERT(VARCHAR(8),U_Z_ReqTime,108) AS 'U_Z_ReqTime'"
                            objDA.strQuery += " , isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_CurApprover ),'') AS 'U_Z_CurApprover',isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_NxtApprover ),'') AS 'U_Z_NxtApprover'   from ""@Z_PAY_OLETRANS1"" T0 JOIN OHEM T4 ON T0.U_Z_EMPID = T4.empID "
                            objDA.strQuery += " JOIN [@Z_HR_APPT1] T1 ON T4.empID = T1.U_Z_OUser "
                            objDA.strQuery += " JOIN [@Z_HR_APPT2] T2 ON T1.DocEntry = T2.DocEntry "
                            objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T2.DocEntry = T3.DocEntry  "
                            objDA.strQuery += " And isnull(T2.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y'  and  T2.U_Z_AUser = '" + objEN.UserCode + "'"
                            objDA.strQuery += " And T3.U_Z_DocType = '" + objEN.HeaderType + "' AND ""U_Z_TrnsCode"" in (" & strLvetype & ")) T20 Order by Convert(Numeric,T20.Code) Desc"
                        Case "RetLve"
                            Dim strLvetype As String = getLeaveType(objEN.UserCode)
                            objDA.strQuery = "Select * FROM (Select Distinct T0.""Code"" as ""Code"",T0.""U_Z_EMPID"",T4.U_Z_EmpID as 'TAEmpID',T0.""U_Z_EMPNAME"",""U_Z_TrnsCode"",isnull(""U_Z_LeaveName"",'') AS U_Z_LeaveName,convert(varchar(10),""U_Z_StartDate"",103) AS ""U_Z_StartDate"","
                            objDA.strQuery += " convert(varchar(10),""U_Z_EndDate"",103) AS ""U_Z_EndDate"" ,cast(T0.U_Z_NoofDays as decimal(10,2)) AS ""U_Z_NoofDays"",convert(varchar(10),T0.""U_Z_RetJoiNDate"",103)  AS ""U_Z_RetJoiNDate"",""U_Z_RAppRemarks"",T0.""U_Z_LevBal"" AS 'U_Z_LevBal',Convert(Varchar,""U_Z_Notes"") as ""U_Z_Notes"",convert(varchar(10),"
                            objDA.strQuery += " ""U_Z_ReJoiNDate"",103) AS ""U_Z_ReJoiNDate"",DateName( month , DateAdd( month , isnull(""U_Z_Month"",1) , -1 ) ) AS ""U_Z_Month"",""U_Z_Year"",case ""U_Z_RStatus"" when 'P' then 'Pending' when 'R' then 'Rejected' "
                            objDA.strQuery += " when 'A' then 'Approved' end as ""U_Z_RStatus"", "
                            objDA.strQuery += " Case U_Z_AppRequired when 'Y' then 'Yes' else 'No' End as  'U_Z_AppRequired',convert(varchar(10),""U_Z_AppReqDate"",103) AS U_Z_AppReqDate,CONVERT(VARCHAR(8),U_Z_ReqTime,108) AS 'U_Z_ReqTime'"
                            objDA.strQuery += " , isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_CurApprover ),'') AS 'U_Z_CurApprover',isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_NxtApprover ),'') AS 'U_Z_NxtApprover'   from ""@Z_PAY_OLETRANS1"" T0 JOIN OHEM T4 ON T0.U_Z_EMPID = T4.empID and T0.""U_Z_TransType""='R' and T0.""U_Z_Status""='A' "
                            objDA.strQuery += " JOIN [@Z_HR_APPT1] T1 ON T4.empID = T1.U_Z_OUser "
                            objDA.strQuery += " JOIN [@Z_HR_APPT2] T2 ON T1.DocEntry = T2.DocEntry "
                            objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T2.DocEntry = T3.DocEntry  "
                            objDA.strQuery += " And isnull(T2.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y'  and  T2.U_Z_AUser = '" + objEN.UserCode + "'"
                            objDA.strQuery += " And T3.U_Z_DocType = '" + objEN.HeaderType + "' AND ""U_Z_TrnsCode"" in (" & strLvetype & ")) T20 Order by Convert(Numeric,T20.Code) Desc"
                            Case "BankTime"
                            Dim strLvetype As String = getLeaveType(objEN.UserCode)
                            objDA.strQuery = "Select * FROM (Select Distinct T0.""Code"" as ""Code"",T0.""U_Z_EMPID"",T0.""U_Z_EMPNAME"",""U_Z_TrnsCode"",""U_Z_LeaveName"" as ""U_Z_LeaveName"",""U_Z_EmpId1"",convert(varchar(10),""U_Z_StartDate"",103) AS ""U_Z_StartDate"","
                            objDA.strQuery += "CAST(T0.U_Z_NoofHours as decimal(10,2)) AS ""U_Z_NoofHours"",cast(T0.U_Z_NoofDays as decimal(10,2)) AS ""U_Z_NoofDays"",Convert(Varchar,""U_Z_Notes"") as ""U_Z_Notes"",Case ""U_Z_CashOut"" when 'Y' then 'Yes' else 'No' end as ""U_Z_CashOut"",case ""U_Z_AppStatus"" when 'P' then 'Pending' when 'R' then 'Rejected' "
                            objDA.strQuery += " when 'A' then 'Approved' end as ""U_Z_AppStatus"",""U_Z_AppRemarks"", "
                            objDA.strQuery += " Case U_Z_AppRequired when 'Y' then 'Yes' else 'No' End as  'U_Z_AppRequired',convert(varchar(10),""U_Z_AppReqDate"",103) AS  'U_Z_AppReqDate',CONVERT(VARCHAR(8),U_Z_ReqTime,108) AS 'U_Z_ReqTime'"
                            objDA.strQuery += " , isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_CurApprover ),'') AS 'U_Z_CurApprover',isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_NxtApprover ),'') AS 'U_Z_NxtApprover'  from ""@Z_PAY_OLADJTRANS1"" T0 JOIN [@Z_HR_APPT1] T1 ON T0.U_Z_EMPID = T1.U_Z_OUser"
                            objDA.strQuery += " JOIN [@Z_HR_APPT2] T2 ON T1.DocEntry = T2.DocEntry "
                            objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T2.DocEntry = T3.DocEntry  "
                            objDA.strQuery += " And isnull(T2.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y'  and  T2.U_Z_AUser = '" + objEN.UserCode + "'"
                            objDA.strQuery += " And T3.U_Z_DocType = '" + objEN.HeaderType + "' AND ""U_Z_TrnsCode"" in (" & strLvetype & ")) T20 Order by Convert(Numeric,T20.Code) Desc"
                    End Select
                Case "Rec"
                    Select Case objEN.HistoryType
                        Case "Rec"
                            objDA.strQuery = " Select T0.DocEntry,convert(varchar(10),""U_Z_ReqDate"",103) AS U_Z_ReqDate,T4.U_Z_EmpID as 'TAEmpID',T0.U_Z_EmpCode,U_Z_EmpName,T0.U_Z_DeptCode,T1.U_Z_DeptName,ISNULL(U_Z_PosName, '') as U_Z_PosName,CAST(T0.U_Z_ExpMin as decimal(10,2)) AS U_Z_ExpMin,CAST(T0.U_Z_ExpMax as decimal(10,2)) AS U_Z_ExpMax,CAST(T0.U_Z_Vacancy as decimal(10,2)) AS U_Z_Vacancy,Case U_Z_AppStatus when 'P' then 'Pending' when 'A' then 'Approved' when 'R' then 'Rejected' end AS U_Z_AppStatus,"
                            objDA.strQuery += " Case U_Z_AppRequired when 'Y' then 'Yes' else 'No' End as  'U_Z_AppRequired',convert(varchar(10),""U_Z_AppReqDate"",103) AS 'U_Z_AppReqDate',CONVERT(VARCHAR(8),U_Z_ReqTime,108) AS 'U_Z_ReqTime'"
                            objDA.strQuery += ", isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_CurApprover ),'') AS 'U_Z_CurApprover',isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_NxtApprover ),'') AS 'U_Z_NxtApprover' From [@Z_HR_ORMPREQ] T0 INNER JOIN OHEM T4 ON  T4.empID  = T0.U_Z_EmpCode "
                            objDA.strQuery += " JOIN [@Z_HR_APPT3] T1 ON T0.U_Z_DeptCode = T1.U_Z_DeptCode "
                            objDA.strQuery += " JOIN [@Z_HR_APPT2] T2 ON T1.DocEntry = T2.DocEntry "
                            objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T2.DocEntry = T3.DocEntry  "
                            objDA.strQuery += " And isnull(T2.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y'  and   T2.U_Z_AUser = '" + objEN.UserCode + "' And T3.U_Z_DocType = '" + objEN.HeaderType + "' Order by T0.DocEntry Desc"
                        Case "AppShort"
                            objDA.strQuery = " Select T0.DocEntry,T0.U_Z_HRAppID,T0.U_Z_HRAppName,T0.U_Z_ReqNo,convert(varchar(10),T0.""U_Z_AppDate"",103) AS U_Z_AppDate,T1.U_Z_DeptCode,T0.U_Z_DeptName,T0.U_Z_Email,CAST(T0.U_Z_YrExp as decimal(10,2)) AS U_Z_YrExp,T0.U_Z_Skills,Case T0.U_Z_AppStatus when 'P' then 'Pending' when 'A' then 'Approved' when 'R' then 'Rejected' end AS U_Z_AppStatus,"
                            objDA.strQuery += " Case T0.U_Z_AppRequired when 'Y' then 'Yes' else 'No' End as  'U_Z_AppRequired',convert(varchar(10),T0.""U_Z_AppReqDate"",103) AS 'U_Z_AppReqDate',CONVERT(VARCHAR(8),T0.U_Z_ReqTime,108) AS 'U_Z_ReqTime'"
                            objDA.strQuery += " , isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_CurApprover ),'') AS 'U_Z_CurApprover',isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_NxtApprover ),'') AS 'U_Z_NxtApprover' From [@Z_HR_OHEM1] T0 JOIN [@Z_HR_ORMPREQ] T1 ON T1.DocEntry = T0.U_Z_ReqNo "
                            objDA.strQuery += " JOIN [@Z_HR_APPT3] T2 ON T1.U_Z_DeptCode = T2.U_Z_DeptCode"
                            objDA.strQuery += " JOIN [@Z_HR_APPT2] T3 ON T2.DocEntry = T3.DocEntry "
                            objDA.strQuery += " JOIN [@Z_HR_OAPPT] T4 ON T3.DocEntry = T4.DocEntry  "
                            objDA.strQuery += " And isnull(T3.U_Z_AMan,'N')='Y' AND isnull(T4.U_Z_Active,'N')='Y'  And T3.U_Z_AUser = '" + objEN.UserCode + "' And T4.U_Z_DocType = '" + objEN.HeaderType + "' Order by T0.DocEntry Desc"
                        Case "IntAppReq"
                            objDA.strQuery = " SELECT ""U_DocEntry"",""U_Empid"", ""U_Empname"", ""U_EmpPosCode"", ""U_EmpPosName"", ""U_EmpdeptCode"", ""U_EmpdeptName"", ""U_ReqdeptCode"", ""U_ReqdeptName"", ""U_ReqPosCode"", ""U_Remarks"", ""U_ReqPosName"", ""U_RequestCode"",Convert(Varchar(10),""U_ApplyDate"",103) AS ""U_ApplyDate"","
                            objDA.strQuery += "Case U_Z_AppStatus when 'P' then 'Pending' when 'A' then 'Approved' when 'R' then 'Rejected' end AS U_Z_AppStatus, Case U_Z_AppRequired when 'Y' then 'Yes' else 'No' End as  'U_Z_AppRequired',Convert(Varchar(10),U_Z_AppReqDate,103) AS 'U_Z_AppReqDate',CONVERT(VARCHAR(8),U_Z_ReqTime,108) AS 'U_Z_ReqTime'"
                            objDA.strQuery += ", isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_CurApprover ),'') AS 'U_CurApprover',isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_NxtApprover ),'') AS 'U_NxtApprover' From [U_VACPOSITION] T0 JOIN [@Z_HR_APPT3] T1 ON T0.U_EmpdeptCode = T1.U_Z_DeptCode"
                            objDA.strQuery += " JOIN [@Z_HR_APPT2] T2 ON T1.DocEntry = T2.DocEntry "
                            objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T2.DocEntry = T3.DocEntry  "
                            objDA.strQuery += " And isnull(T2.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y'  and   T2.U_Z_AUser = '" + objEN.UserCode + "' And T3.U_Z_DocType = '" + objEN.HeaderType + "' Order by T0.U_DocEntry Desc"
                    End Select
            End Select
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds2)
            Return objDA.ds2
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function LoadHistory(ByVal objEN As DynamicApprovalEN) As DataSet
        Try

            Select Case objEN.HistoryType
                Case "RegTra", "NewTra", "Rec", "AppShort", "EmpPro", "EmpPos", "TraReq", "PerHour", "IntAppReq", "PerObj", "BankTime"
                    objDA.strQuery = " Select DocEntry,U_Z_DocEntry,U_Z_DocType,U_Z_EmpId,U_Z_EmpName,U_Z_ApproveBy,isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_NextApprover ),'') AS 'U_Z_NxtApprover',convert(varchar(10),CreateDate,103) AS CreateDate ,LEFT(CONVERT(VARCHAR(5), CreateTime, 9),2) + ':' + RIGHT(CONVERT(VARCHAR(30), CreateTime, 9),2) AS CreateTime,convert(varchar(10),UpdateDate,103) AS UpdateDate,LEFT(CONVERT(VARCHAR(5), UpdateTime, 9),2) + ':' + RIGHT(CONVERT(VARCHAR(30), UpdateTime, 9),2) AS UpdateTime,Case U_Z_AppStatus when 'A' then 'Approved' when 'R' then 'Rejected' else 'Pending' end AS U_Z_AppStatus,U_Z_Remarks From [@Z_HR_APHIS] T0 "
                    objDA.strQuery += " Where U_Z_DocType = '" + objEN.HistoryType + "'"
                    objDA.strQuery += " And U_Z_DocEntry = '" + objEN.DocEntry + "'"
                Case "LveReq", "ExpCli", "RetLve"
                    objDA.strQuery = " Select DocEntry,U_Z_DocEntry,U_Z_DocType,U_Z_EmpId,U_Z_EmpName,U_Z_ApproveBy,isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_NextApprover ),'') AS 'U_Z_NxtApprover',convert(varchar(10),CreateDate,103) AS CreateDate ,LEFT(CONVERT(VARCHAR(5), CreateTime, 9),2) + ':' + RIGHT(CONVERT(VARCHAR(30), CreateTime, 9),2) AS CreateTime,convert(varchar(10),UpdateDate,103) AS UpdateDate,LEFT(CONVERT(VARCHAR(5), UpdateTime, 9),2) + ':' + RIGHT(CONVERT(VARCHAR(30), UpdateTime, 9),2) AS UpdateTime,Case U_Z_AppStatus when 'A' then 'Approved' when 'R' then 'Rejected' else 'Pending' end AS U_Z_AppStatus,U_Z_Remarks,U_Z_Year,U_Z_Month From [@Z_HR_APHIS] T0 "
                    objDA.strQuery += " Where U_Z_DocType = '" + objEN.HistoryType + "'"
                    objDA.strQuery += " And U_Z_DocEntry = '" + objEN.DocEntry + "'"
            End Select
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds1)
            Return objDA.ds1
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function SummaryHistory(ByVal objEN As DynamicApprovalEN) As DataSet
        Try

            Select Case objEN.HistoryType
                Case "RegTra", "NewTra", "Rec", "AppShort", "EmpPro", "EmpPos", "TraReq", "PerHour", "IntAppReq", "PerObj", "BankTime"
                    objDA.strQuery = " Select DocEntry,U_Z_DocEntry,U_Z_DocType,U_Z_EmpId,U_Z_EmpName,U_Z_ApproveBy,isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_NextApprover ),'') AS 'U_Z_NxtApprover',convert(varchar(10),CreateDate,103) AS  CreateDate,LEFT(CONVERT(VARCHAR(5), CreateTime, 9),2) + ':' + RIGHT(CONVERT(VARCHAR(30), CreateTime, 9),2) AS CreateTime,convert(varchar(10),UpdateDate,103) AS UpdateDate,LEFT(CONVERT(VARCHAR(5), UpdateTime, 9),2) + ':' + RIGHT(CONVERT(VARCHAR(30), UpdateTime, 9),2) AS UpdateTime,Case U_Z_AppStatus when 'A' then 'Approved' when 'R' then 'Rejected' else 'Pending' end AS U_Z_AppStatus,U_Z_Remarks From [@Z_HR_APHIS] T0 "
                    objDA.strQuery += " Where U_Z_DocType = '" + objEN.HistoryType + "'"
                    objDA.strQuery += " And U_Z_DocEntry = '" + objEN.DocEntry + "'"
                Case "LveReq", "ExpCli", "RetLve"
                    objDA.strQuery = " Select DocEntry,U_Z_DocEntry,U_Z_DocType,U_Z_EmpId,U_Z_EmpName,U_Z_ApproveBy,isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_NextApprover ),'') AS 'U_Z_NxtApprover',convert(varchar(10),CreateDate,103) AS CreateDate ,LEFT(CONVERT(VARCHAR(5), CreateTime, 9),2) + ':' + RIGHT(CONVERT(VARCHAR(30), CreateTime, 9),2) AS CreateTime,convert(varchar(10),UpdateDate,103) AS UpdateDate,LEFT(CONVERT(VARCHAR(5), UpdateTime, 9),2) + ':' + RIGHT(CONVERT(VARCHAR(30), UpdateTime, 9),2) AS UpdateTime,Case U_Z_AppStatus when 'A' then 'Approved' when 'R' then 'Rejected' else 'Pending' end AS U_Z_AppStatus,U_Z_Remarks,U_Z_Year,U_Z_Month From [@Z_HR_APHIS] T0 "
                    objDA.strQuery += " Where U_Z_DocType = '" + objEN.HistoryType + "'"
                    objDA.strQuery += " And U_Z_DocEntry = '" + objEN.DocEntry + "'"
            End Select
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds3)
            Return objDA.ds3
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function addUpdateDocument(ByVal objEN As DynamicApprovalEN) As String
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
            Select Case objEN.HeaderType
                Case "EmpLife", "Rec"
                    'oTest.DoQuery("Select isnull(""dept"",'0') from OHEM where ""empID""=" & objEN.EmpId)
                    'objEN.EmpId = oTest.Fields.Item(0).Value
                    objDA.strQuery = "select T0.DocEntry,T1.LineId from [@Z_HR_OAPPT] T0 JOIN [@Z_HR_APPT2] T1 on T0.DocEntry=T1.DocEntry"
                    objDA.strQuery += " JOIN [@Z_HR_APPT3] T2 on T1.DocEntry=T2.DocEntry"
                    objDA.strQuery += " where T0.U_Z_DocType='" & objEN.HeaderType & "' AND T2.U_Z_DeptCode='" & objEN.EmpId & "' AND T1.U_Z_AUser='" & objEN.UserCode & "'"
                Case "ExpCli", "Train", "TraReq"
                    objDA.strQuery = "select T0.DocEntry,T1.LineId from [@Z_HR_OAPPT] T0 JOIN [@Z_HR_APPT2] T1 on T0.DocEntry=T1.DocEntry"
                    objDA.strQuery += " JOIN [@Z_HR_APPT1] T2 on T1.DocEntry=T2.DocEntry"
                    objDA.strQuery += " where T0.U_Z_DocType='" & objEN.HeaderType & "' AND T2.U_Z_OUser='" & objEN.EmpId & "' AND T1.U_Z_AUser='" & objEN.UserCode & "'"
                Case "LveReq"
                    objDA.strQuery = "select T0.DocEntry,T1.LineId from [@Z_HR_OAPPT] T0 JOIN [@Z_HR_APPT2] T1 on T0.DocEntry=T1.DocEntry"
                    objDA.strQuery += " JOIN [@Z_HR_APPT1] T2 on T1.DocEntry=T2.DocEntry"
                    objDA.strQuery += " where T0.U_Z_LveType='" & objEN.LeaveCode & "' AND T0.U_Z_DocType='" & objEN.HeaderType & "' AND T2.U_Z_OUser='" & objEN.EmpId & "' AND T1.U_Z_AUser='" & objEN.UserCode & "'"
            End Select
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
            objDA.strQuery = "Select * from [@Z_HR_APHIS] where U_Z_DocEntry='" & objEN.DocEntry & "' and U_Z_DocType='" & objEN.HistoryType & "' and U_Z_ApproveBy='" & objEN.UserCode & "'"
            oRecordSet.DoQuery(objDA.strQuery)
            If oRecordSet.RecordCount > 0 Then
                oGeneralParams.SetProperty("DocEntry", oRecordSet.Fields.Item("DocEntry").Value)
                oGeneralData = oGeneralService.GetByParams(oGeneralParams)
                oGeneralData.SetProperty("U_Z_AppStatus", objEN.AppStatus)
                oGeneralData.SetProperty("U_Z_Remarks", objEN.Remarks)
                oGeneralData.SetProperty("U_Z_ADocEntry", objEN.HeadDocEntry)
                oGeneralData.SetProperty("U_Z_ALineId", objEN.HeadLineId)
                If objEN.AppStatus = "A Then" Then
                    oGeneralData.SetProperty("U_Z_NextApprover", NextApprover)
                Else
                    oGeneralData.SetProperty("U_Z_NextApprover", objEN.UserCode)
                End If
                If objEN.HistoryType = "LveReq" Or objEN.HistoryType = "ExpCli" Then
                    oGeneralData.SetProperty("U_Z_Month", objEN.Month)
                    oGeneralData.SetProperty("U_Z_Year", objEN.Year)
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
                If objEN.AppStatus = "A Then" Then
                    oGeneralData.SetProperty("U_Z_NextApprover", NextApprover)
                Else
                    oGeneralData.SetProperty("U_Z_NextApprover", objEN.UserCode)
                End If
                If objEN.HistoryType = "LveReq" Or objEN.HistoryType = "ExpCli" Then
                    oGeneralData.SetProperty("U_Z_Month", objEN.Month)
                    oGeneralData.SetProperty("U_Z_Year", objEN.Year)
                End If
                oGeneralService.Add(oGeneralData)
                objDA.strmsg = "Successfully approved document..."
            End If
            objDA.strmsg = updateFinalStatus(objEN)
            If objDA.strmsg = "Success" Or objDA.strmsg = "Successfully approved document..." Then
                'If objEN.AppStatus <> "P" And objEN.AppStatus <> "-" Then
                If objEN.AppStatus = "A" Then
                    SendMessage(objEN)
                End If
            End If
            Return objDA.strmsg
        Catch ex As Exception
            objDA.strmsg = ex.Message
            Return objDA.strmsg
        End Try
    End Function
    Public Sub SendMessage(ByVal objEN As DynamicApprovalEN)
        Try
            Dim strQuery As String
            Dim strMessageUser As String
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
            'If objDA.ConnectSAP() = True Then
            oCmpSrv = objEN.SapCompany.GetCompanyService()
            oMessageService = oCmpSrv.GetBusinessService(SAPbobsCOM.ServiceTypes.MessagesService)
            oMessage = oMessageService.GetDataInterface(SAPbobsCOM.MessagesServiceDataInterfaces.msdiMessage)
            oRecordSet = objEN.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oTemp = objEN.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            strQuery = "Select LineId From [@Z_HR_APPT2] Where DocEntry = '" & objEN.HeadDocEntry & "' And U_Z_AUser = '" & objEN.UserCode & "'"
            oRecordSet.DoQuery(strQuery)
            If Not oRecordSet.EoF Then
                intLineID = CInt(oRecordSet.Fields.Item(0).Value)
                strQuery = "Select Top 1 U_Z_AUser From [@Z_HR_APPT2] Where  DocEntry = '" & objEN.HeadDocEntry & "' And LineId > '" & intLineID.ToString() & "' and isnull(U_Z_AMan,'')='Y'  Order By LineId Asc "
                oRecordSet.DoQuery(strQuery)

                If Not oRecordSet.EoF Then
                    strMessageUser = oRecordSet.Fields.Item(0).Value
                    oMessage.Subject = objEN.DocMessage & ":" & " Need Your Approval "
                    Dim strMessage As String = ""
                    Select Case objEN.HistoryType
                        Case "BankTime" 'Bank Time Request
                            strQuery = "Select * from  [@Z_PAY_OLADJTRANS1] where Code='" & objEN.DocEntry & "'"
                            oTemp.DoQuery(strQuery)
                            strMessage = " Requested by  :" & oTemp.Fields.Item("U_Z_EMPNAME").Value & ": Leave Name : " & oTemp.Fields.Item("U_Z_LeaveName").Value
                        Case "PerHour"
                            strQuery = "Select * from  [@Z_PAY_OLETRANS1] where Code='" & objEN.DocEntry & "'"
                            oTemp.DoQuery(strQuery)
                            strMessage = " Requested by  :" & oTemp.Fields.Item("U_Z_EMPNAME").Value
                        Case "LveReq", "RetLve" 'Leave Request"
                            strQuery = "Select * from  [@Z_PAY_OLETRANS1] where Code='" & objEN.DocEntry & "'"
                            oTemp.DoQuery(strQuery)
                            strMessage = " Requested by  :" & oTemp.Fields.Item("U_Z_EMPNAME").Value & ": Leave Name : " & oTemp.Fields.Item("U_Z_LeaveName").Value
                        Case "ExpCli" 'Expense Claim"
                            strQuery = "Select * from  [@Z_HR_EXPCL]  where Code='" & objEN.DocEntry & "'"
                            oTemp.DoQuery(strQuery)
                            strMessage = " Requested by  :" & oTemp.Fields.Item("U_Z_EmpName").Value
                        Case "RegTra"
                            strQuery = "Select * from  [@Z_HR_TRIN1]  where Code='" & objEN.DocEntry & "'"
                            oTemp.DoQuery(strQuery)
                            strMessage = " Requested by  :" & oTemp.Fields.Item("U_Z_HREmpName").Value
                        Case "NewTra"
                            strQuery = "Select * from  [@Z_HR_ONTREQ]  where DocEntry='" & objEN.DocEntry & "'"
                            oTemp.DoQuery(strQuery)
                            strMessage = " Requested by  :" & oTemp.Fields.Item("U_Z_HREmpName").Value
                        Case "EmpPos"
                            strQuery = "Select * from  [@Z_HR_HEM4]  where Code='" & objEN.DocEntry & "'"
                            oTemp.DoQuery(strQuery)
                            strMessage = " for Employee  :" & oTemp.Fields.Item("U_Z_FirstName").Value & " " & oTemp.Fields.Item("U_Z_LastName").Value
                        Case "EmpPro"
                            strQuery = "Select * from  [@Z_HR_HEM2]  where Code='" & objEN.DocEntry & "'"
                            oTemp.DoQuery(strQuery)
                            strMessage = " for Employee  :" & oTemp.Fields.Item("U_Z_FirstName").Value & " " & oTemp.Fields.Item("U_Z_LastName").Value
                        Case "Rec"
                            strQuery = "Select * from [@Z_HR_ORMPREQ]  where DocEntry='" & objEN.DocEntry & "'"
                            oTemp.DoQuery(strQuery)
                            strMessage = " Recruited by   :" & oTemp.Fields.Item("U_Z_EmpName").Value & ": for  " & oTemp.Fields.Item("U_Z_PosName").Value & " Position"
                        Case "AppShort"
                            strQuery = "Select * from  [@Z_HR_OHEM1]  where DocEntry='" & objEN.DocEntry & "'"
                            oTemp.DoQuery(strQuery)
                            strMessage = " Candidate Name  :" & oTemp.Fields.Item("U_Z_HRAPPName").Value & ": Applied Position : " & oTemp.Fields.Item("U_Z_JobPosi").Value
                        Case "Final"
                            strQuery = "Select * from  [@Z_HR_OHEM1]  where DocEntry='" & objEN.DocEntry & "'"
                            oTemp.DoQuery(strQuery)
                            strMessage = " Candidate Name  :" & oTemp.Fields.Item("U_Z_HRAPPName").Value & ": Applied Position : " & oTemp.Fields.Item("U_Z_JobPosi").Value
                        Case "TraReq"
                            strQuery = "Select * from  [@Z_HR_OTRAREQ]  where DocEntry='" & objEN.DocEntry & "'"
                            oTemp.DoQuery(strQuery)
                            strMessage = " Requested by  :" & oTemp.Fields.Item("U_Z_EmpName").Value
                        Case "IntAppReq"
                            strQuery = "Select * from  [U_VACPOSITION]  where U_DocEntry='" & objEN.DocEntry & "'"
                            oTemp.DoQuery(strQuery)
                            strMessage = " Requested by  :" & oTemp.Fields.Item("U_Empname").Value
                        Case "PerObj"
                            strQuery = "Select * from  [U_PEOPLEOBJ]  where U_DocEntry='" & objEN.DocEntry & "'"
                            oTemp.DoQuery(strQuery)
                            strMessage = " Requested by  :" & oTemp.Fields.Item("U_Empname").Value
                        Case "LoanReq"
                            strQuery = "Select * from  [U_LOANREQ]  where U_DocEntry='" & objEN.DocEntry & "'"
                            oTemp.DoQuery(strQuery)
                            strMessage = " Requested by  :" & oTemp.Fields.Item("U_Empname").Value
                    End Select

                    Select Case objEN.HistoryType
                        Case "BankTime" 'Bank Time Request
                            strQuery = "Update [@Z_PAY_OLADJTRANS1] set U_Z_CurApprover='" & objEN.UserCode & "',U_Z_NxtApprover='" & strMessageUser & "' where Code='" & objEN.DocEntry & "'"
                        Case "LveReq", "RetLve", "PerHour" 'Leave Request"
                            strQuery = "Update [@Z_PAY_OLETRANS1] set U_Z_CurApprover='" & objEN.UserCode & "',U_Z_NxtApprover='" & strMessageUser & "' where Code='" & objEN.DocEntry & "'"
                        Case "ExpCli" 'Expense Claim"
                            strQuery = "Update [@Z_HR_EXPCL] set U_Z_CurApprover='" & objEN.UserCode & "',U_Z_NxtApprover='" & strMessageUser & "' where Code='" & objEN.DocEntry & "'"
                        Case "RegTra"
                            strQuery = "Update [@Z_HR_TRIN1] set U_Z_CurApprover='" & objEN.UserCode & "',U_Z_NxtApprover='" & strMessageUser & "' where Code='" & objEN.DocEntry & "'"
                        Case "NewTra"
                            strQuery = "Update [@Z_HR_ONTREQ] set U_Z_CurApprover='" & objEN.UserCode & "',U_Z_NxtApprover='" & strMessageUser & "' where DocEntry='" & objEN.DocEntry & "'"
                        Case "EmpPos"
                            strQuery = "Update [@Z_HR_HEM4] set U_Z_CurApprover='" & objEN.UserCode & "',U_Z_NxtApprover='" & strMessageUser & "' where Code='" & objEN.DocEntry & "'"
                        Case "EmpPro"
                            strQuery = "Update [@Z_HR_HEM2] set U_Z_CurApprover='" & objEN.UserCode & "',U_Z_NxtApprover='" & strMessageUser & "' where Code='" & objEN.DocEntry & "'"
                        Case "Rec"
                            strQuery = "Update[@Z_HR_ORMPREQ] set U_Z_CurApprover='" & objEN.UserCode & "',U_Z_NxtApprover='" & strMessageUser & "' where DocEntry='" & objEN.DocEntry & "'"
                        Case "AppShort"
                            strQuery = "Update [@Z_HR_OHEM1] set U_Z_CurApprover='" & objEN.UserCode & "',U_Z_NxtApprover='" & strMessageUser & "' where DocEntry='" & objEN.DocEntry & "'"

                        Case "Final"
                            strQuery = "Update [@Z_HR_OHEM1] set U_Z_CurApprover1='" & objEN.UserCode & "',U_Z_NxtApprover1='" & strMessageUser & "' where DocEntry='" & objEN.DocEntry & "'"
                        Case "TraReq"
                            strQuery = "Update [@Z_HR_OTRAREQ] set U_Z_CurApprover='" & objEN.UserCode & "',U_Z_NxtApprover='" & strMessageUser & "' where DocEntry='" & objEN.DocEntry & "'"
                        Case "IntAppReq"
                            strQuery = "Update [U_VACPOSITION] set U_CurApprover='" & objEN.UserCode & "',U_NxtApprover='" & strMessageUser & "' where U_DocEntry='" & objEN.DocEntry & "'"
                        Case "PerObj"
                            strQuery = "Update [U_PEOPLEOBJ] set U_CurApprover='" & objEN.UserCode & "',U_NxtApprover='" & strMessageUser & "' where U_DocEntry='" & objEN.DocEntry & "'"
                        Case "LoanReq"
                            strQuery = "Update [U_LOANREQ] set U_CurApprover='" & objEN.UserCode & "',U_NxtApprover='" & strMessageUser & "' where U_DocEntry='" & objEN.DocEntry & "'"
                    End Select
                    oTemp.DoQuery(strQuery)

                    Dim IntDoc As Integer = Integer.Parse(objEN.DocEntry)
                    objEN.DocEntry = IntDoc.ToString()
                    oMessage.Text = objEN.DocMessage & " " & objEN.DocEntry & strMessage & " Needs Your Approval "
                    oRecipientCollection = oMessage.RecipientCollection
                    oRecipientCollection.Add()
                    oRecipientCollection.Item(0).SendInternal = SAPbobsCOM.BoYesNoEnum.tYES
                    oRecipientCollection.Item(0).UserCode = strMessageUser
                    pMessageDataColumns = oMessage.MessageDataColumns
                    pMessageDataColumn = pMessageDataColumns.Add()
                    pMessageDataColumn.ColumnName = "Request No"
                    oLines = pMessageDataColumn.MessageDataLines()
                    oLine = oLines.Add()
                    oLine.Value = objEN.DocEntry
                    oMessageService.SendMessage(oMessage)
                    Dim strEmailMessage As String = objEN.DocMessage + "  " + objEN.DocEntry + " " + strMessage + " Needs Your Approval "
                    SendMail_Approval(strEmailMessage, strMessageUser, strMessageUser, objEN.SapCompany)
                   
                End If
            End If
            ' End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function SendMailforAppraisal(ByVal strType As String, ByVal strEmpId As String, ByVal DocNo As String, ByVal SapCompany As SAPbobsCOM.Company, Optional ByVal HRMailId As String = "", Optional ByVal Period As String = "") As String
        Try
            Dim strQuery As String
            Dim strMesage, Message As String
            Dim oRecordSet, oTemp As SAPbobsCOM.Recordset
            Dim oCmpSrv As SAPbobsCOM.CompanyService
            Dim oMessageService As SAPbobsCOM.MessagesService
            Dim oMessage As SAPbobsCOM.Message
            Dim pMessageDataColumns As SAPbobsCOM.MessageDataColumns
            Dim pMessageDataColumn As SAPbobsCOM.MessageDataColumn
            Dim oLines As SAPbobsCOM.MessageDataLines
            Dim oLine As SAPbobsCOM.MessageDataLine
            Dim oRecipientCollection As SAPbobsCOM.RecipientCollection
            oCmpSrv = SapCompany.GetCompanyService()
            oMessageService = oCmpSrv.GetBusinessService(SAPbobsCOM.ServiceTypes.MessagesService)
            oMessage = oMessageService.GetDataInterface(SAPbobsCOM.MessagesServiceDataInterfaces.msdiMessage)
            oRecordSet = SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oTemp = SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oRecordSet.DoQuery("Select U_Z_SMTPSERV,U_Z_SMTPPORT,U_Z_SMTPUSER,U_Z_SMTPPWD,U_Z_SSL From [@Z_HR_OMAIL]")
            If Not oRecordSet.EoF Then
                mailServer = oRecordSet.Fields.Item("U_Z_SMTPSERV").Value
                mailPort = oRecordSet.Fields.Item("U_Z_SMTPPORT").Value
                mailId = oRecordSet.Fields.Item("U_Z_SMTPUSER").Value
                mailPwd = oRecordSet.Fields.Item("U_Z_SMTPPWD").Value
                mailSSL = oRecordSet.Fields.Item("U_Z_SSL").Value
                If mailServer <> "" And mailId <> "" And mailPwd <> "" Then
                    Dim oTest As SAPbobsCOM.Recordset
                    oTest = SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                    oTest.DoQuery("Select * from [@Z_HR_OWEB]")
                    Dim strESSLink As String = ""
                    If oTest.RecordCount > 0 Then
                        strESSLink = oTest.Fields.Item("U_Z_WebPath").Value
                    End If
                    SmtpServer.Credentials = New Net.NetworkCredential(mailId, mailPwd)
                    SmtpServer.Port = mailPort
                    SmtpServer.EnableSsl = mailSSL
                    SmtpServer.Host = mailServer
                    mail = New Net.Mail.MailMessage()
                    mail.From = New Net.Mail.MailAddress(mailId, "HRMS")
                    mail.IsBodyHtml = True
                    mail.Priority = MailPriority.High
                    mail.To.Add(HRMailId)
                    strESSLink = strESSLink
                    oTemp.DoQuery("Select isnull(firstName,'') + ' ' + isnull(middleName,'') +' ' + isnull(lastName,'') from OHEM where empid=" & strEmpId)
                    Message = "New Training Document No :" & DocNo & ", Requested by : " & oTemp.Fields.Item(0).Value & " is approved by the manager. Create training agenda."
                    strMesage = "<!DOCTYPE html><html><head><title>New Training Approved Notification</title></head><body>  <a>" & Message & "</a><a href=" & strESSLink & " >Click Here to Login to ESS</a></body></html>"
                    mail.Subject = "New Training Approved Notification"
                    mail.Body = strMesage
                    SmtpServer.Send(mail)
                    objDA.strmsg = "Success"
                End If
            Else
                objDA.strmsg = "Mail Server Details Not Configured..."
            End If

        Catch ex As Exception
            objDA.strmsg = SapCompany.GetLastErrorDescription
        Finally
            mail.Dispose()
        End Try
        Return objDA.strmsg
    End Function
    Public Sub SendMail_Approval(ByVal aMessage As String, ByVal aMail As String, ByVal aUser As String, ByVal aCompany As SAPbobsCOM.Company)
        Dim oRecordset As SAPbobsCOM.Recordset
        oRecordset = aCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        oRecordset.DoQuery("Select U_Z_SMTPSERV,U_Z_SMTPPORT,U_Z_SMTPUSER,U_Z_SMTPPWD,U_Z_SSL From [@Z_HR_OMAIL]")
        If Not oRecordset.EoF Then
            mailServer = oRecordset.Fields.Item("U_Z_SMTPSERV").Value
            mailPort = oRecordset.Fields.Item("U_Z_SMTPPORT").Value
            mailId = oRecordset.Fields.Item("U_Z_SMTPUSER").Value
            mailPwd = oRecordset.Fields.Item("U_Z_SMTPPWD").Value
            mailSSL = oRecordset.Fields.Item("U_Z_SSL").Value
            If mailServer <> "" And mailId <> "" And mailPwd <> "" Then
                oRecordset.DoQuery("Select * from OUSR where USER_CODE='" & aUser & "'")
                aMail = oRecordset.Fields.Item("E_Mail").Value
                If aMail <> "" Then
                    Dim oTest As SAPbobsCOM.Recordset
                    oTest = aCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                    oTest.DoQuery("Select * from [@Z_HR_OWEB]")
                    Dim strESSLink As String = ""
                    If oTest.RecordCount > 0 Then
                        strESSLink = oTest.Fields.Item("U_Z_WebPath").Value
                    End If
                    SendMailforApproval(mailServer, mailPort, mailId, mailPwd, mailSSL, aMail, aMail, "Approval", aMessage, strESSLink)
                End If
            Else
                ' oApplication.Utilities.Message("Mail Server Details Not Configured...", SAPbouiCOM.BoStatusBarMessageType.smt_Warning)
            End If

        End If
    End Sub

    Private Sub SendMailforApproval(ByVal mailServer As String, ByVal mailPort As String, ByVal mailId As String, ByVal mailpwd As String, ByVal mailSSL As String, ByVal toId As String, ByVal ccId As String, ByVal mType As String, ByVal Message As String, Optional ByVal strESSLink As String = "")
        Try

            'Dim strRptPath As String = System.Windows.Forms.Application.StartupPath.Trim() & "\Report.pdf"
            SmtpServer.Credentials = New Net.NetworkCredential(mailId, mailpwd)
            SmtpServer.Port = mailPort
            SmtpServer.EnableSsl = mailSSL
            SmtpServer.Host = mailServer
            mail = New Net.Mail.MailMessage()
            mail.From = New Net.Mail.MailAddress(mailId, "HRMS")
            mail.To.Add(toId)
            '  mail.CC.Add(ccId)
            mail.IsBodyHtml = True
            mail.Priority = MailPriority.High
            mail.Subject = Message
            mail.Body = Message & "  <a href=" & strESSLink & " >Click Here to Login to ESS</a>"
            SmtpServer.Send(mail)
        Catch ex As Exception
            Throw ex
        Finally
            mail.Dispose()
        End Try
    End Sub
    Public Sub SendMail_RequestApproval(ByVal aMessage As String, ByVal Empid As String, ByVal aCompany As SAPbobsCOM.Company, Optional ByVal aMail As String = "")
        Dim oRecordset As SAPbobsCOM.Recordset
        oRecordset = aCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        oRecordset.DoQuery("Select U_Z_SMTPSERV,U_Z_SMTPPORT,U_Z_SMTPUSER,U_Z_SMTPPWD,U_Z_SSL From [@Z_HR_OMAIL]")
        If Not oRecordset.EoF Then
            mailServer = oRecordset.Fields.Item("U_Z_SMTPSERV").Value
            mailPort = oRecordset.Fields.Item("U_Z_SMTPPORT").Value
            mailId = oRecordset.Fields.Item("U_Z_SMTPUSER").Value
            mailPwd = oRecordset.Fields.Item("U_Z_SMTPPWD").Value
            mailSSL = oRecordset.Fields.Item("U_Z_SSL").Value
            If mailServer <> "" And mailId <> "" And mailPwd <> "" Then
                oRecordset.DoQuery("Select * from OHEM where empID='" & Empid & "'")
                aMail = oRecordset.Fields.Item("email").Value
                If aMail <> "" Then
                    Dim oTest As SAPbobsCOM.Recordset
                    oTest = aCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                    oTest.DoQuery("Select * from [@Z_HR_OWEB]")
                    Dim strESSLink As String = ""
                    If oTest.RecordCount > 0 Then
                        strESSLink = oTest.Fields.Item("U_Z_WebPath").Value
                    End If
                    SendMailforApproval(mailServer, mailPort, mailId, mailPwd, mailSSL, aMail, aMail, "Approval", aMessage)
                End If
            Else
                ' oApplication.Utilities.Message("Mail Server Details Not Configured...", SAPbouiCOM.BoStatusBarMessageType.smt_Warning)
            End If

        End If
    End Sub

    Public Function updateFinalStatus(ByVal objEN As DynamicApprovalEN) As String
        Try
            Dim oRecordSet, oTemp As SAPbobsCOM.Recordset
            Dim StrMailMessage As String
            oRecordSet = objEN.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oTemp = objEN.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            If objEN.AppStatus = "A" Then
                Select Case objEN.HeaderType
                    Case "Rec", "EmpLife"
                        objDA.strQuery = " Select T2.DocEntry "
                        objDA.strQuery += " From [@Z_HR_APPT2] T2 "
                        objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T2.DocEntry = T3.DocEntry  "
                        objDA.strQuery += " JOIN [@Z_HR_APPT3] T4 ON T4.DocEntry = T3.DocEntry  "
                        objDA.strQuery += " Where T4.U_Z_DeptCode='" & objEN.EmpId & "' and  U_Z_AFinal = 'Y'"
                        objDA.strQuery += " And T2.U_Z_AUser = '" + objEN.UserCode + "' And T3.U_Z_DocType = '" + objEN.HeaderType + "'"
                    Case "ExpCli", "Train", "TraReq"
                        objDA.strQuery = " Select T2.DocEntry "
                        objDA.strQuery += " From [@Z_HR_APPT2] T2 "
                        objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T2.DocEntry = T3.DocEntry  "
                        objDA.strQuery += " JOIN [@Z_HR_APPT1] T4 ON T4.DocEntry = T3.DocEntry  "
                        objDA.strQuery += " Where T4.U_Z_Ouser='" & objEN.EmpId & "' and  U_Z_AFinal = 'Y'"
                        objDA.strQuery += " And T2.U_Z_AUser = '" + objEN.UserCode + "' And T3.U_Z_DocType = '" + objEN.HeaderType + "'"
                    Case "LveReq"
                        objDA.strQuery = " Select T2.DocEntry "
                        objDA.strQuery += " From [@Z_HR_APPT2] T2 "
                        objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T2.DocEntry = T3.DocEntry  "
                        objDA.strQuery += " JOIN [@Z_HR_APPT1] T4 ON T4.DocEntry = T3.DocEntry  "
                        objDA.strQuery += " Where T4.U_Z_Ouser='" & objEN.EmpId & "' and  U_Z_AFinal = 'Y' and T3.U_Z_LveType='" & objEN.LeaveCode & "'"
                        objDA.strQuery += " And T2.U_Z_AUser = '" + objEN.UserCode + "' And T3.U_Z_DocType = '" + objEN.HeaderType + "'"
                End Select
                oRecordSet.DoQuery(objDA.strQuery)
                If Not oRecordSet.EoF Then
                    Select Case objEN.HistoryType
                        Case "BankTime"
                            sQuery = "Update ""@Z_PAY_OLADJTRANS1"" Set U_Z_AppStatus = 'A',""U_Z_AppRemarks""='" & objEN.Remarks & "' Where Code = '" + objEN.DocEntry + "'"
                            oRecordSet.DoQuery(sQuery)
                            AddtoUDT_BankTime(objEN.DocEntry, objEN.SapCompany)
                            StrMailMessage = "Bank time request has been approved for the request number :" & CInt(objEN.DocEntry)
                            SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany)
                            objDA.strmsg = "Success"
                        Case "PerHour"
                            objDA.strQuery = "Update ""@Z_PAY_OLETRANS1"" Set  U_Z_Status = 'A',""U_Z_AppRemarks""='" & objEN.Remarks & "' Where Code = '" + objEN.DocEntry + "'"
                            oRecordSet.DoQuery(objDA.strQuery)
                            objDA.strmsg = "Success"
                        Case "RetLve"
                            objDA.strQuery = "Update [@Z_PAY_OLETRANS1] Set U_Z_RStatus = 'A',U_Z_RAppRemarks='" & objEN.Remarks & "' Where Code = '" + objEN.DocEntry + "'"
                            oRecordSet.DoQuery(objDA.strQuery)
                            objDA.strmsg = UpdateAddUDTPayroll(objEN.DocEntry, objEN.SapCompany)
                            StrMailMessage = "Return from leave request has been approved for the request number :" & CInt(objEN.DocEntry)
                            SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany)
                            objDA.strmsg = "Success"
                        Case "LveReq"
                            objDA.strQuery = "Update ""@Z_PAY_OLETRANS1"" Set U_Z_Year=" & objEN.Year & ",U_Z_Month=" & objEN.Month & ", U_Z_Status = 'A',""U_Z_AppRemarks""='" & objEN.Remarks & "' Where Code = '" + objEN.DocEntry + "'"
                            oRecordSet.DoQuery(objDA.strQuery)
                            objDA.strmsg = AddUDTPayroll(objEN.DocEntry, objEN.SapCompany)
                            StrMailMessage = "Leave request has been approved for the request number :" & CInt(objEN.DocEntry)
                            SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany)
                            objDA.strmsg = "Success"
                        Case "ExpCli"
                            objDA.strQuery = "Update [@Z_HR_EXPCL] Set U_Z_Year=" & objEN.Year & ",U_Z_Month=" & objEN.Month & ", U_Z_AppStatus = 'A' Where Code = '" + objEN.DocEntry + "'"
                            oRecordSet.DoQuery(objDA.strQuery)
                            objDA.strmsg = AddtoUDT1_PayrollTrans(objEN.DocEntry, objEN.SapCompany)
                            StrMailMessage = "Expense claim request has been approved for the request number :" & CInt(objEN.DocEntry)
                            SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany)
                            objDA.strmsg = "Success"
                        Case "TraReq"
                            objDA.strQuery = "Update [@Z_HR_OTRAREQ] Set U_Z_AppStatus = 'A',U_Z_ReqAppDate=getdate(),U_Z_HRComme='" & objEN.Remarks & "' Where DocEntry = '" + objEN.DocEntry + "'"
                            oRecordSet.DoQuery(objDA.strQuery)
                            StrMailMessage = "Travel request has been approved for the request number :" & CInt(objEN.DocEntry)
                            SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany)
                            objDA.strmsg = "Success"
                        Case "RegTra"
                            objDA.strQuery = "Update [@Z_HR_TRIN1] Set U_Z_Status='A' , U_Z_AppStatus = 'A',U_Z_MgrRegRemarks='" & objEN.Remarks & "' Where Code = '" + objEN.DocEntry + "'"
                            oRecordSet.DoQuery(objDA.strQuery)
                            StrMailMessage = "Your Registered Training request number :" & CInt(objEN.DocEntry) & " has been approved."
                            SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany)
                            objDA.strmsg = "Success"
                        Case "NewTra"
                            objDA.strQuery = "Update [@Z_HR_ONTREQ] Set U_Z_AppStatus = 'A',U_Z_HRRemarks='" & objEN.Remarks & "' Where DocEntry = '" + objEN.DocEntry + "'"
                            oRecordSet.DoQuery(objDA.strQuery)
                            StrMailMessage = "New Training request number :" & CInt(objEN.DocEntry) & " has been approved."
                            SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany)
                            sQuery = "Select U_Z_HRMail,T0.U_Z_HREmpId from [@Z_HR_ONTREQ] T0 JOIN OHEM T1 ON T0.U_Z_HREmpId=T1.empID where T0.DocEntry='" & objEN.DocEntry & "'"
                            oTemp.DoQuery(sQuery)
                            If oTemp.RecordCount > 0 Then
                                Dim HRMailId As String = oTemp.Fields.Item(0).Value
                                If HRMailId <> "" Then
                                    objDA.strmsg = SendMailforAppraisal("NewTrain", oTemp.Fields.Item("U_Z_HREmpId").Value, objEN.DocEntry, objEN.SapCompany, HRMailId)
                                    If objDA.strmsg = "Success" Then
                                        objDA.strmsg = "Success"
                                    Else
                                        objDA.strmsg = objDA.strmsg
                                    End If
                                Else
                                    objDA.strmsg = "Success"
                                End If
                            End If

                        Case "Rec"
                            objDA.strQuery = "Update [@Z_HR_ORMPREQ] Set U_Z_AppStatus = 'A',U_Z_HODRemarks='" & objEN.Remarks & "' Where DocEntry = '" + objEN.DocEntry + "'"
                            oRecordSet.DoQuery(objDA.strQuery)
                            StrMailMessage = "Recruitment request has been approved for the request number :" & CInt(objEN.DocEntry)
                            SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany)
                            objDA.strmsg = "Success"
                        Case "AppShort"
                            objDA.strQuery = "Update [@Z_HR_OHEM1] Set U_Z_AppStatus = 'A',U_Z_MgrRemarks='" & objEN.Remarks & "' Where DocEntry = '" + objEN.DocEntry + "'"
                            oRecordSet.DoQuery(objDA.strQuery)
                            objDA.strQuery = "Select U_Z_HRAppID from [@Z_HR_OHEM1] where DocEntry = '" + objEN.DocEntry + "'"
                            oRecordSet.DoQuery(objDA.strQuery)
                            If oRecordSet.RecordCount > 0 Then
                                objDA.strQuery = "Update [@Z_HR_OCRAPP] Set U_Z_Status = 'N' Where DocEntry = '" + oRecordSet.Fields.Item(0).Value + "'"
                                oTemp.DoQuery(objDA.strQuery)
                            End If

                            objDA.strmsg = "Success"
                        Case "EmpPro"
                            objDA.strQuery = "Update [@Z_HR_HEM2] Set U_Z_AppStatus = 'A' Where Code = '" + objEN.DocEntry + "'"
                            oRecordSet.DoQuery(objDA.strQuery)
                            StrMailMessage = "Employee promotion request has been approved for the request number :" & CInt(objEN.DocEntry)
                            SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany)
                            objDA.strmsg = "Success"
                        Case "EmpPos"
                            objDA.strQuery = "Update [@Z_HR_HEM4] Set U_Z_AppStatus = 'A' Where Code = '" + objEN.DocEntry + "'"
                            oRecordSet.DoQuery(objDA.strQuery)
                            StrMailMessage = "Employee position change request has been approved for the request number :" & CInt(objEN.DocEntry)
                            SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany)
                            objDA.strmsg = "Success"
                        Case "Final"
                            objDA.strQuery = "Update [@Z_HR_OHEM1] set  U_Z_APPlStatus='A', U_Z_IntervStatus = 'A',U_Z_IPHODSta = 'S', U_Z_Finished = 'Y' where DocEntry = '" & objEN.DocEntry & "'"
                            oRecordSet.DoQuery(objDA.strQuery)

                            oRecordSet.DoQuery("Select U_Z_HRAppID from [@Z_HR_OHEM1] where DocEntry='" & objEN.DocEntry & "'")
                            If oRecordSet.RecordCount > 0 Then
                                objDA.strQuery = "Update [@Z_HR_OCRAPP] set U_Z_Status = 'M' where DocEntry = '" & oRecordSet.Fields.Item(0).Value & "'"
                                oTemp.DoQuery(objDA.strQuery)
                            End If
                            objDA.strmsg = "Success"
                        Case "IntAppReq"
                            Dim blnvalue As String = ""
                            blnvalue = CreateApplicants(objEN)
                            If blnvalue = "Success" Then
                                objDA.strmsg = "Success"
                            Else
                                objDA.strmsg = blnvalue
                            End If
                        Case "PerObj"
                            objDA.strQuery = "Update U_PEOPLEOBJ Set U_Z_AppStatus = 'A',U_Remarks='" & objEN.Remarks & "'  Where U_DocEntry = '" + objEN.DocEntry + "'"
                            oRecordSet.DoQuery(objDA.strQuery)
                            objDA.strmsg = PersonalObjectiveApproval(objEN)
                            StrMailMessage = "Personel objective request has been approved for the request number :" & CInt(objEN.DocEntry)
                            SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany)
                            objDA.strmsg = "Success"
                    End Select
                End If
            ElseIf objEN.AppStatus = "R" Then
                Select Case objEN.HeaderType
                    Case "Rec", "EmpLife"
                        objDA.strQuery = " Select T2.DocEntry "
                        objDA.strQuery += " From [@Z_HR_APPT2] T2 "
                        objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T2.DocEntry = T3.DocEntry  "
                        objDA.strQuery += " JOIN [@Z_HR_APPT3] T4 ON T4.DocEntry = T3.DocEntry  "
                        objDA.strQuery += " Where T4.U_Z_DeptCode='" & objEN.EmpId & "'" ' and  U_Z_AFinal = 'Y'"
                        objDA.strQuery += " And T2.U_Z_AUser = '" + objEN.UserCode + "' And T3.U_Z_DocType = '" + objEN.HeaderType + "'"
                    Case "ExpCli", "Train", "TraReq"
                        objDA.strQuery = " Select T2.DocEntry "
                        objDA.strQuery += " From [@Z_HR_APPT2] T2 "
                        objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T2.DocEntry = T3.DocEntry  "
                        objDA.strQuery += " JOIN [@Z_HR_APPT1] T4 ON T4.DocEntry = T3.DocEntry  "
                        objDA.strQuery += " Where T4.U_Z_Ouser='" & objEN.EmpId & "'" ' and  U_Z_AFinal = 'Y'"
                        objDA.strQuery += " And T2.U_Z_AUser = '" + objEN.UserCode + "' And T3.U_Z_DocType = '" + objEN.HeaderType + "'"
                    Case "LveReq"
                        objDA.strQuery = " Select T2.DocEntry "
                        objDA.strQuery += " From [@Z_HR_APPT2] T2 "
                        objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T2.DocEntry = T3.DocEntry  "
                        objDA.strQuery += " JOIN [@Z_HR_APPT1] T4 ON T4.DocEntry = T3.DocEntry  "
                        objDA.strQuery += " Where T4.U_Z_Ouser='" & objEN.EmpId & "' and T3.U_Z_LveType='" & objEN.LeaveCode & "'" ' and  U_Z_AFinal = 'Y' 
                        objDA.strQuery += " And T2.U_Z_AUser = '" + objEN.UserCode + "' And T3.U_Z_DocType = '" + objEN.HeaderType + "'"
                End Select
                oRecordSet.DoQuery(objDA.strQuery)
                If Not oRecordSet.EoF Then
                    Select Case objEN.HistoryType
                        Case "BankTime"
                            sQuery = "Update ""@Z_PAY_OLADJTRANS1"" Set U_Z_AppStatus = 'R',""U_Z_AppRemarks""='" & objEN.Remarks & "' Where Code = '" + objEN.DocEntry + "'"
                            oRecordSet.DoQuery(sQuery)
                            StrMailMessage = "Bank time request has been rejected for the request number :" & CInt(objEN.DocEntry)
                            SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany)
                            objDA.strmsg = "Success"
                        Case "PerHour"
                            objDA.strQuery = "Update ""@Z_PAY_OLETRANS1"" Set  U_Z_Status = 'R',""U_Z_AppRemarks""='" & objEN.Remarks & "' Where Code = '" + objEN.DocEntry + "'"
                            oRecordSet.DoQuery(objDA.strQuery)
                            objDA.strmsg = "Success"
                        Case "RetLve"
                            objDA.strQuery = "Update [@Z_PAY_OLETRANS1] Set U_Z_RStatus = 'R',U_Z_RAppRemarks='" & objEN.Remarks & "' Where Code = '" + objEN.DocEntry + "'"
                            oRecordSet.DoQuery(objDA.strQuery)
                            StrMailMessage = "Return from leave request has been rejected for the request number :" & CInt(objEN.DocEntry)
                            SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany)
                            objDA.strmsg = "Success"
                        Case "LveReq"
                            objDA.strQuery = "Update ""@Z_PAY_OLETRANS1"" Set U_Z_Year=" & objEN.Year & ",U_Z_Month=" & objEN.Month & ", U_Z_Status = 'R',""U_Z_AppRemarks""='" & objEN.Remarks & "' Where Code = '" + objEN.DocEntry + "'"
                            oRecordSet.DoQuery(objDA.strQuery)
                            StrMailMessage = "Leave request has been rejected for the request number :" & CInt(objEN.DocEntry)
                            SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany)
                            objDA.strmsg = "Success"
                        Case "ExpCli"
                            objDA.strQuery = "Update [@Z_HR_EXPCL] Set U_Z_Year=" & objEN.Year & ",U_Z_Month=" & objEN.Month & ", U_Z_AppStatus = 'R',U_Z_RejRemark='" & objEN.Remarks & "' Where Code = '" + objEN.DocEntry + "'"
                            oRecordSet.DoQuery(objDA.strQuery)
                            StrMailMessage = "Expense Claim request has been rejected for the request number :" & CInt(objEN.DocEntry)
                            SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany)
                            objDA.strmsg = "Success"
                        Case "TraReq"
                            objDA.strQuery = "Update [@Z_HR_OTRAREQ] Set U_Z_AppStatus = 'R',U_Z_ReqAppDate=getdate(),U_Z_HRComme='" & objEN.Remarks & "' Where DocEntry = '" + objEN.DocEntry + "'"
                            oRecordSet.DoQuery(objDA.strQuery)
                            StrMailMessage = "Travel request has been rejected for the request number :" & CInt(objEN.DocEntry)
                            SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany)
                            objDA.strmsg = "Success"
                        Case "RegTra"
                            objDA.strQuery = "Update [@Z_HR_TRIN1] Set U_Z_Status='R' , U_Z_AppStatus = 'R',U_Z_ApproveRemarks='" & objEN.Remarks & "'  Where Code = '" + objEN.DocEntry + "'"
                            oRecordSet.DoQuery(objDA.strQuery)
                            StrMailMessage = "Your Registered training request number :" & CInt(objEN.DocEntry) & " has been rejected."
                            SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany)
                            objDA.strmsg = "Success"
                        Case "NewTra"
                            objDA.strQuery = "Update [@Z_HR_ONTREQ] Set U_Z_AppStatus = 'R',U_Z_HRRemarks='" & objEN.Remarks & "'  Where DocEntry = '" + objEN.DocEntry + "'"
                            oRecordSet.DoQuery(objDA.strQuery)
                            StrMailMessage = "New Training request has been rejected for the request number :" & CInt(objEN.DocEntry)
                            SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany)
                            objDA.strmsg = "Success"
                        Case "Rec"
                            objDA.strQuery = "Update [@Z_HR_ORMPREQ] Set U_Z_AppStatus = 'R',U_Z_HODRemarks='" & objEN.Remarks & "'  Where DocEntry = '" + objEN.DocEntry + "'"
                            oRecordSet.DoQuery(objDA.strQuery)
                            StrMailMessage = "Recruitment request has been rejected for the request number :" & CInt(objEN.DocEntry)
                            SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany)
                            objDA.strmsg = "Success"
                        Case "AppShort"
                            objDA.strQuery = "Update [@Z_HR_OHEM1] Set U_Z_AppStatus = 'R',U_Z_MgrRemarks='" & objEN.Remarks & "'  Where DocEntry = '" + objEN.DocEntry + "'"
                            oRecordSet.DoQuery(objDA.strQuery)
                            objDA.strQuery = "Select U_Z_HRAppID from [@Z_HR_OHEM1] where DocEntry = '" + objEN.DocEntry + "'"
                            oRecordSet.DoQuery(objDA.strQuery)
                            If oRecordSet.RecordCount > 0 Then
                                objDA.strQuery = "Update [@Z_HR_OCRAPP] Set U_Z_Status = 'N' Where DocEntry = '" + oRecordSet.Fields.Item(0).Value + "'"
                                oTemp.DoQuery(objDA.strQuery)
                            End If
                            objDA.strmsg = "Success"
                        Case "EmpPro"
                            objDA.strQuery = "Update [@Z_HR_HEM2] Set U_Z_AppStatus = 'R' Where Code = '" + objEN.DocEntry + "'"
                            oRecordSet.DoQuery(objDA.strQuery)
                            StrMailMessage = "Employee promotion request has been rejected for the request number :" & CInt(objEN.DocEntry)
                            SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany)
                            objDA.strmsg = "Success"
                        Case "EmpPos"
                            objDA.strQuery = "Update [@Z_HR_HEM4] Set U_Z_AppStatus = 'R' Where Code = '" + objEN.DocEntry + "'"
                            oRecordSet.DoQuery(objDA.strQuery)
                            StrMailMessage = "Employee position change request has been rejected for the request number :" & CInt(objEN.DocEntry)
                            SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany)
                            objDA.strmsg = "Success"
                        Case "Final"
                            objDA.strQuery = "Update [@Z_HR_OHEM1] set  U_Z_APPlStatus='R', U_Z_IntervStatus = 'R',U_Z_IPHODSta = 'R', U_Z_Finished = 'Y' where DocEntry = '" & objEN.DocEntry & "'"
                            oRecordSet.DoQuery(objDA.strQuery)

                            oRecordSet.DoQuery("Select U_Z_HRAppID from [@Z_HR_OHEM1] where DocEntry='" & objEN.DocEntry & "'")
                            If oRecordSet.RecordCount > 0 Then
                                objDA.strQuery = "Update [@Z_HR_OCRAPP] set U_Z_Status = 'R',U_Z_RejResn='" & objEN.Remarks & "' where DocEntry = '" & oRecordSet.Fields.Item(0).Value & "'"
                                oTemp.DoQuery(objDA.strQuery)
                            End If
                            objDA.strmsg = "Success"
                        Case "IntAppReq"
                            objDA.strQuery = "Update [U_VACPOSITION] Set U_Z_AppStatus = 'R',U_Remarks='" & objEN.Remarks & "'  Where U_DocEntry = '" + objEN.DocEntry + "'"
                            oRecordSet.DoQuery(objDA.strQuery)
                            StrMailMessage = "Internel applicatns request has been rejected for the request number :" & CInt(objEN.DocEntry)
                            SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany)
                            objDA.strmsg = "Success"
                        Case "PerObj"
                            objDA.strQuery = "Update U_PEOPLEOBJ Set U_Z_AppStatus = 'R',U_Remarks='" & objEN.Remarks & "' Where U_DocEntry = '" + objEN.DocEntry + "'"
                            oRecordSet.DoQuery(objDA.strQuery)
                            StrMailMessage = "Personel objective request has been rejected for the request number :" & CInt(objEN.DocEntry)
                            SendMail_RequestApproval(StrMailMessage, objEN.EmpId, objEN.SapCompany)
                            objDA.strmsg = "Success"
                    End Select
                End If
            End If
            'End If
            Return objDA.strmsg
        Catch ex As Exception
            objDA.strmsg = ex.Message
            Return objDA.strmsg
        End Try
    End Function
    Public Function AddtoUDT_BankTime(ByVal HeadDocEntry As String, ByVal SapCompany As SAPbobsCOM.Company) As String
        Try
            Dim oUserTable As SAPbobsCOM.UserTable
            Dim oRecSet As SAPbobsCOM.Recordset
            oRecSet = SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            Dim strCode, strQuery As String
            strQuery = "Select U_Z_EmpId1,U_Z_EmpID,U_Z_EMPNAME,U_Z_TrnsCode,U_Z_LeaveName,U_Z_NoofDays,Convert(varchar(10),U_Z_StartDate,120) AS U_Z_StartDate,U_Z_Notes,U_Z_CashOut from ""@Z_PAY_OLADJTRANS1"" where ""U_Z_AppStatus""='A' and  ""Code""='" & HeadDocEntry & "'"
            oRecSet.DoQuery(strQuery)
            If oRecSet.RecordCount > 0 Then
                'oUserTable = SapCompany.UserTables.Item("Z_PAY_OLADJTRANS")
                strCode = objDA.Getmaxcode("[@Z_PAY_OLADJTRANS]", "Code")
                'oUserTable.Code = strCode
                'oUserTable.Name = strCode
                'oUserTable.UserFields.Fields.Item("U_Z_EmpId1").Value = oRecSet.Fields.Item("U_Z_EmpId1").Value
                'oUserTable.UserFields.Fields.Item("U_Z_EMPID").Value = oRecSet.Fields.Item("U_Z_EMPID").Value
                'oUserTable.UserFields.Fields.Item("U_Z_EMPNAME").Value = oRecSet.Fields.Item("U_Z_EMPNAME").Value
                'oUserTable.UserFields.Fields.Item("U_Z_TrnsCode").Value = oRecSet.Fields.Item("U_Z_TrnsCode").Value
                'oUserTable.UserFields.Fields.Item("U_Z_LeaveName").Value = oRecSet.Fields.Item("U_Z_LeaveName").Value
                'oUserTable.UserFields.Fields.Item("U_Z_StartDate").Value = oRecSet.Fields.Item("U_Z_StartDate").Value
                'oUserTable.UserFields.Fields.Item("U_Z_NoofDays").Value = oRecSet.Fields.Item("U_Z_NoofDays").Value
                'oUserTable.UserFields.Fields.Item("U_Z_Notes").Value = oRecSet.Fields.Item("U_Z_Notes").Value
                'oUserTable.UserFields.Fields.Item("U_Z_CashOut").Value = oRecSet.Fields.Item("U_Z_CashOut").Value
                'If oUserTable.Add() <> 0 Then
                '    objDA.strmsg = SapCompany.GetLastErrorDescription
                '    Return objDA.strmsg
                'End If
                strQuery = "Insert into [@Z_PAY_OLADJTRANS](Code,Name,U_Z_EmpId1,U_Z_EMPID,U_Z_EMPNAME,U_Z_TrnsCode,U_Z_LeaveName,U_Z_StartDate,U_Z_NoofDays,U_Z_Notes,U_Z_CashOut)"
                strQuery += " Values ('" & strCode & "','" & strCode & "','" & oRecSet.Fields.Item("U_Z_EmpId1").Value & "','" & oRecSet.Fields.Item("U_Z_EMPID").Value & "','" & oRecSet.Fields.Item("U_Z_EMPNAME").Value & "','" & oRecSet.Fields.Item("U_Z_TrnsCode").Value & "',"
                strQuery += " '" & oRecSet.Fields.Item("U_Z_LeaveName").Value & "','" & oRecSet.Fields.Item("U_Z_StartDate").Value & "','" & oRecSet.Fields.Item("U_Z_NoofDays").Value & "','" & oRecSet.Fields.Item("U_Z_Notes").Value & "','" & oRecSet.Fields.Item("U_Z_CashOut").Value & "')"
                oRecSet.DoQuery(strQuery)
            End If
            Return "Success"
        Catch ex As Exception
            objDA.strmsg = ex.Message
            Return objDA.strmsg
        End Try
    End Function
    Public Function PersonalObjectiveApproval(ByVal objEN As DynamicApprovalEN) As String
        Try
            Dim strCode As String
            Dim oRecordSet As SAPbobsCOM.Recordset
            oRecordSet = objEN.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            Dim oUserTable As SAPbobsCOM.UserTable
            oUserTable = objDA.objMainCompany.UserTables.Item("Z_HR_PEOBJ1")
            objDA.strQuery = "Select * from [U_PEOPLEOBJ] Where U_Z_AppStatus = 'A' and U_DocEntry = '" + objEN.DocEntry + "'"
            oRecordSet.DoQuery(objDA.strQuery)
            If oRecordSet.RecordCount > 0 Then
                strCode = objDA.Getmaxcode("""@Z_HR_PEOBJ1""", """Code""")
                'oUserTable.Code = strCode
                'oUserTable.Name = strCode
                'oUserTable.UserFields.Fields.Item("U_Z_HREmpID").Value = oRecordSet.Fields.Item("U_Empid").Value
                'oUserTable.UserFields.Fields.Item("U_Z_HRPeoobjCode").Value = oRecordSet.Fields.Item("U_PeoobjCode").Value
                'oUserTable.UserFields.Fields.Item("U_Z_HRPeoobjName").Value = oRecordSet.Fields.Item("U_PeoobjName").Value
                'oUserTable.UserFields.Fields.Item("U_Z_HRPeoCategory").Value = oRecordSet.Fields.Item("U_PeoCategory").Value
                'oUserTable.UserFields.Fields.Item("U_Z_HRWeight").Value = oRecordSet.Fields.Item("U_Weight").Value
                'oUserTable.UserFields.Fields.Item("U_Z_Remarks").Value = oRecordSet.Fields.Item("U_Remarks").Value
                'If oUserTable.Add() <> 0 Then
                '    objDA.strmsg = objDA.objMainCompany.GetLastErrorDescription
                'Else
                '    objDA.strmsg = "Success"
                'End If
                objDA.strQuery = "Insert into [@Z_HR_PEOBJ1](Code,Name,U_Z_HREmpID,U_Z_HRPeoobjCode,U_Z_HRPeoobjName,U_Z_HRPeoCategory,U_Z_HRWeight,U_Z_Remarks)"
                objDA.strQuery += " Values ('" & strCode & "','" & strCode & "','" & oRecordSet.Fields.Item("U_Empid").Value & "','" & oRecordSet.Fields.Item("U_PeoobjCode").Value & "','" & oRecordSet.Fields.Item("U_PeoobjName").Value & "','" & oRecordSet.Fields.Item("U_PeoCategory").Value & "',"
                objDA.strQuery += " '" & oRecordSet.Fields.Item("U_Weight").Value & "','" & oRecordSet.Fields.Item("U_Remarks").Value & "')"
                oRecordSet.DoQuery(objDA.strQuery)
            End If
        Catch ex As Exception
            objDA.strmsg = ex.Message
        End Try
        Return objDA.strmsg
    End Function
    Public Function CreateApplicants(ByVal objen As DynamicApprovalEN) As String
        Try
            Dim strCode As String = ""
            ' If objDA.ConnectSAP() = True Then
            Dim oRecordSet, oTemp As SAPbobsCOM.Recordset
            oRecordSet = objen.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oTemp = objen.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            objDA.strQuery = "Update [U_VACPOSITION] Set U_Z_AppStatus = 'A',U_Remarks='" & objen.Remarks & "' Where U_DocEntry = '" + objen.DocEntry + "'"
            oRecordSet.DoQuery(objDA.strQuery)
            objDA.strQuery = "Select * from [U_VACPOSITION]  Where U_DocEntry = '" + objen.DocEntry + "'"
            oTemp.DoQuery(objDA.strQuery)
            If oTemp.RecordCount > 0 Then
                objen.IntReqNo = oTemp.Fields.Item("U_RequestCode").Value
            End If
            objen.AppStatus = "A"

            If objen.AppStatus = "A" Then
                Dim oGeneralService As SAPbobsCOM.GeneralService
                Dim oGeneralData As SAPbobsCOM.GeneralData
                Dim oChild, oChild1 As SAPbobsCOM.GeneralData
                Dim oChildren, oChildren1 As SAPbobsCOM.GeneralDataCollection
                Dim oCompanyService As SAPbobsCOM.CompanyService
                oCompanyService = objen.SapCompany.GetCompanyService
                oGeneralService = oCompanyService.GetGeneralService("Z_HR_OCRAPPL")
                oGeneralData = oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData)
                strCode = objDA.Getmaxcode("""@Z_HR_OCRAPP""", """DocEntry""")
                oGeneralData.SetProperty("U_Z_RequestCode", objen.IntReqNo)
                objDA.strQuery = "Select * from OHEM where ""empID""='" & objen.InternalEmpInd & "'"
                objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
                objDA.sqlda.Fill(objDA.ds)
                If objDA.ds.Tables(0).Rows.Count > 0 Then
                    oGeneralData.SetProperty("U_Z_EmpId", objen.InternalEmpInd)
                    oGeneralData.SetProperty("U_Z_FirstName", objDA.ds.Tables(0).Rows(0)("firstName").ToString())
                    oGeneralData.SetProperty("U_Z_LastName", objDA.ds.Tables(0).Rows(0)("lastName").ToString())
                    oGeneralData.SetProperty("U_Z_EmailId", objDA.ds.Tables(0).Rows(0)("email").ToString())
                    oGeneralData.SetProperty("U_Z_Mobile", objDA.ds.Tables(0).Rows(0)("mobile").ToString())
                    oGeneralData.SetProperty("U_Z_AppDate", Now.Date)
                    oGeneralData.SetProperty("U_Z_Sex", objDA.ds.Tables(0).Rows(0)("sex").ToString())
                    oGeneralData.SetProperty("U_Z_Dob", objDA.ds.Tables(0).Rows(0)("birthDate").ToString())
                    oGeneralData.SetProperty("U_Z_Nationality", objDA.ds.Tables(0).Rows(0)("brthCountr").ToString())
                    oGeneralData.SetProperty("U_Z_PStreet", objDA.ds.Tables(0).Rows(0)("workStreet").ToString())
                    oGeneralData.SetProperty("U_Z_PCity", objDA.ds.Tables(0).Rows(0)("workCity").ToString())
                    oGeneralData.SetProperty("U_Z_PZipCode", objDA.ds.Tables(0).Rows(0)("workZip").ToString())
                    oGeneralData.SetProperty("U_Z_PCountry", objDA.ds.Tables(0).Rows(0)("workCountr").ToString())
                    oGeneralData.SetProperty("U_Z_TStreet", objDA.ds.Tables(0).Rows(0)("homeStreet").ToString())
                    oGeneralData.SetProperty("U_Z_TCity", objDA.ds.Tables(0).Rows(0)("homeCity").ToString())
                    oGeneralData.SetProperty("U_Z_TZipCode", objDA.ds.Tables(0).Rows(0)("homeZip").ToString())
                    oGeneralData.SetProperty("U_Z_TCountry", objDA.ds.Tables(0).Rows(0)("homeCountr").ToString())
                    oGeneralData.SetProperty("U_Z_Remarks", objDA.ds.Tables(0).Rows(0)("remark").ToString())
                    oGeneralData.SetProperty("U_Z_Basic", objDA.ds.Tables(0).Rows(0)("salary").ToString())
                    oGeneralData.SetProperty("U_Z_PBlock", objDA.ds.Tables(0).Rows(0)("workBlock").ToString())
                    oGeneralData.SetProperty("U_Z_PBuilding", objDA.ds.Tables(0).Rows(0)("WorkBuild").ToString())
                    oGeneralData.SetProperty("U_Z_PState", objDA.ds.Tables(0).Rows(0)("workState").ToString())
                    oGeneralData.SetProperty("U_Z_TBlock", objDA.ds.Tables(0).Rows(0)("homeBlock").ToString())
                    oGeneralData.SetProperty("U_Z_TBuilding", objDA.ds.Tables(0).Rows(0)("HomeBuild").ToString())
                    oGeneralData.SetProperty("U_Z_TState", objDA.ds.Tables(0).Rows(0)("homeState").ToString())
                    ' oGeneralData.SetProperty("U_Z_Marital", objDA.ds.Tables(0).Rows(0)("martStatus").ToString())
                    oGeneralData.SetProperty("U_Z_Children", objDA.ds.Tables(0).Rows(0)("nChildren").ToString())
                    oGeneralData.SetProperty("U_Z_Citizen", objDA.ds.Tables(0).Rows(0)("citizenshp").ToString())
                    oGeneralData.SetProperty("U_Z_Passport", objDA.ds.Tables(0).Rows(0)("passportNo").ToString())
                    oGeneralData.SetProperty("U_Z_Passexpdate", objDA.ds.Tables(0).Rows(0)("passportEx").ToString())
                    oGeneralData.SetProperty("U_Z_Status", "R")
                    oGeneralData.SetProperty("U_Z_Source", "I")
                    oChildren = oGeneralData.Child("Z_HR_CRAPP4")
                    'Dim otemp As SAPbobsCOM.Recordset
                    'otemp = objen.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                    objDA.strQuery = "SELECT ""fromDate"", ""toDate"", ""employer"", ""position"", ""remarks"" FROM HEM4 WHERE ISNULL(""employer"", '') <> '' AND ""empID""='" & objen.InternalEmpInd & "'"
                    oTemp.DoQuery(objDA.strQuery)
                    For introw As Integer = 0 To oTemp.RecordCount - 1
                        oChild = oChildren.Add()
                        oChild.SetProperty("U_Z_FromDate", oTemp.Fields.Item(0).Value)
                        oChild.SetProperty("U_Z_ToDate", oTemp.Fields.Item(1).Value)
                        oChild.SetProperty("U_Z_PrEmployer", oTemp.Fields.Item(2).Value)
                        oChild.SetProperty("U_Z_PrPosition", oTemp.Fields.Item(3).Value)
                        oChild.SetProperty("U_Z_Remarks", oTemp.Fields.Item(4).Value)
                        oTemp.MoveNext()
                    Next
                    oChildren1 = oGeneralData.Child("Z_HR_CRAPP3")
                    Dim otemp1 As SAPbobsCOM.Recordset
                    otemp1 = objen.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                    objDA.strQuery = "SELECT ""fromDate"", ""toDate"", ""type"", ""institute"", ""major"", ""diploma"" FROM HEM2 WHERE ISNULL(""type"", '0') <> '0' AND ""empID""='" & objen.InternalEmpInd & "'"
                    otemp1.DoQuery(objDA.strQuery)
                    For introw As Integer = 0 To otemp1.RecordCount - 1
                        oChild1 = oChildren1.Add()
                        oChild1.SetProperty("U_Z_GrFromDate", otemp1.Fields.Item(0).Value)
                        oChild1.SetProperty("U_Z_GrT0Date", otemp1.Fields.Item(1).Value)
                        oChild1.SetProperty("U_Z_Level", otemp1.Fields.Item(2).Value)
                        oChild1.SetProperty("U_Z_School", otemp1.Fields.Item(3).Value)
                        oChild1.SetProperty("U_Z_Major", otemp1.Fields.Item(4).Value)
                        oChild1.SetProperty("U_Z_Diploma", otemp1.Fields.Item(5).Value)
                        otemp1.MoveNext()
                    Next
                End If
                oGeneralService.Add(oGeneralData)
            End If
            objDA.strQuery = "Update ""U_VACPOSITION"" set ""U_Z_AppStatus""='" & objen.AppStatus & "',""U_Remarks""='" & objen.Remarks & "' where ""U_DocEntry""='" & objen.DocEntry & "'"
            oRecordSet.DoQuery(objDA.strQuery)
            objDA.strmsg = ApplicantsShortlisted(objen.IntReqNo, strCode, objen.SapCompany)
            'End If
        Catch ex As Exception
            objDA.strmsg = ex.Message
        End Try
        Return objDA.strmsg
    End Function
    Private Function ApplicantsShortlisted(ByVal Reqno As String, ByVal AppNo As String, ByVal oCompany As SAPbobsCOM.Company) As String
        Try

            Dim oGeneralService As SAPbobsCOM.GeneralService
            Dim oGeneralData1 As SAPbobsCOM.GeneralData
            Dim oGeneralParams As SAPbobsCOM.GeneralDataParams
            Dim oCompanyService As SAPbobsCOM.CompanyService
            Dim oChildren1 As SAPbobsCOM.GeneralDataCollection
            oCompanyService = oCompany.GetCompanyService()
            Dim otestRs, oRec As SAPbobsCOM.Recordset
            Dim blnRecordExists As Boolean = False
            oGeneralService = oCompanyService.GetGeneralService("Z_HR_OHEM")
            otestRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oRec = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oGeneralParams = oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams)
            Dim blnDownpayment As Boolean = False
            Dim blnDocumentItem As Boolean
            Dim status As String
            Dim strempid As String
            Dim strDepartmentCode As String
            blnDocumentItem = False
            oGeneralData1 = oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData)
            If Reqno = "" Then
                objDA.strmsg = "Request Number is missing"
                Return objDA.strmsg
            Else
                Dim stSQL1 As String
                stSQL1 = "Select * from [@Z_HR_ORMPREQ] where DocEntry='" & Reqno & "' and (U_Z_AppStatus='C' or U_Z_AppStatus='L')"
                oRec.DoQuery(stSQL1)
                If oRec.RecordCount > 0 Then
                    objDA.strmsg = "Request Number already closed :" & Reqno
                    Return objDA.strmsg
                Else
                    stSQL1 = "Select U_Z_DeptCode,* from [@Z_HR_ORMPREQ] where DocEntry='" & Reqno & "'"
                    oRec.DoQuery(stSQL1)
                    status = objDA.DocApproval("Rec", oRec.Fields.Item(0).Value)
                    strDepartmentCode = oRec.Fields.Item(0).Value
                End If
            End If
            otestRs.DoQuery("Select * from ""@Z_HR_OCRAPP"" where DocEntry='" & AppNo & "'")
            oGeneralData1.SetProperty("U_Z_ApplStatus", "S")
            oGeneralData1.SetProperty("U_Z_HRAppID", AppNo)
            oGeneralData1.SetProperty("U_Z_HRAppName", otestRs.Fields.Item("U_Z_FirstName").Value & " " & otestRs.Fields.Item("U_Z_LastName").Value)
            oGeneralData1.SetProperty("U_Z_Dob", otestRs.Fields.Item("U_Z_Dob").Value)
            oGeneralData1.SetProperty("U_Z_Email", otestRs.Fields.Item("U_Z_EmailId").Value)
            oGeneralData1.SetProperty("U_Z_AppDate", otestRs.Fields.Item("U_Z_AppDate").Value)
            oGeneralData1.SetProperty("U_Z_Dept", oRec.Fields.Item("U_Z_DeptCode").Value)
            oGeneralData1.SetProperty("U_Z_DeptName", oRec.Fields.Item("U_Z_DeptName").Value)
            oGeneralData1.SetProperty("U_Z_ReqNo", Reqno)
            oGeneralData1.SetProperty("U_Z_JobPosi", oRec.Fields.Item("U_Z_EmpPosi").Value)
            oGeneralData1.SetProperty("U_Z_Mobile", otestRs.Fields.Item("U_Z_Mobile").Value)
            oGeneralData1.SetProperty("U_Z_EmpId", otestRs.Fields.Item("U_Z_EmpId").Value)
            oGeneralData1.SetProperty("U_Z_ApplyDate", Now.Date)
            oGeneralData1.SetProperty("U_Z_JobPosiCode", oRec.Fields.Item("U_Z_PosName").Value)
            oGeneralData1.SetProperty("U_Z_AppStatus", status)
            If status = "A" Then
                oGeneralData1.SetProperty("U_Z_AppRequired", "N")
            Else
                oGeneralData1.SetProperty("U_Z_AppRequired", "Y")
            End If

            status = objDA.GetTemplateID("Rec", oRec.Fields.Item(0).Value)
            oGeneralData1.SetProperty("U_Z_ApproveId", status)

            oChildren1 = oGeneralData1.Child("Z_HR_OHEM2")

            oGeneralService.Add(oGeneralData1)

            otestRs.DoQuery("Select max(DocEntry) 'DocEntry' from [@Z_HR_OHEM1]")
            Dim intTempID As String = status 'oApplication.Utilities.GetTemplateID(oForm, HeaderDoctype.Rec, otest.Fields.Item("U_Z_DeptCode").Value)
            If intTempID <> "0" Then
                objDA.UpdateApprovalRequired("@Z_HR_OHEM1", "DocEntry", otestRs.Fields.Item("DocEntry").Value, "Y", intTempID)
                objDA.InitialMessage("Shortlisting Applicant Request", otestRs.Fields.Item("DocEntry").Value, objDA.DocApproval("Rec", strDepartmentCode), intTempID, strDepartmentCode, "AppShort", oCompany)
            Else
                objDA.UpdateApprovalRequired("@Z_HR_ORMPREQ", "DocEntry", otestRs.Fields.Item("DocEntry").Value, "N", intTempID)
            End If

            objDA.strQuery = "Update [@Z_HR_OCRAPP] set U_Z_Status='S',U_Z_RequestCode='" & Reqno & "' where DocEntry='" & AppNo & "' and (U_Z_Status='R') "
            otestRs.DoQuery(objDA.strQuery)
            objDA.strmsg = "Success"
        Catch ex As Exception
            objDA.strmsg = ex.Message
        End Try
        Return objDA.strmsg
    End Function

    Public Function UpdateAddUDTPayroll(ByVal strHeadcode As String, ByVal SapCompany As SAPbobsCOM.Company) As String
        Try
            Dim strStartdt, strRetdt, Nodays, strtodt As String
            Dim dtTo, dtRetjoin, startdt, todate As Date
            Dim intDiff As Double
            Dim oRecSet, oRec2, oTemp As SAPbobsCOM.Recordset
            objDA.objMainCompany = SapCompany
            oRecSet = objDA.objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oRec2 = objDA.objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oTemp = objDA.objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            objDA.strQuery = "Select * from ""@Z_PAY_OLETRANS1"" where ""U_Z_Status""='A' and U_Z_RStatus='A' and  ""Code""='" & strHeadcode & "'"
            oRecSet.DoQuery(objDA.strQuery)
            If oRecSet.RecordCount > 0 Then
                strtodt = oRecSet.Fields.Item("U_Z_EndDate").Value
                strStartdt = oRecSet.Fields.Item("U_Z_StartDate").Value
                strRetdt = oRecSet.Fields.Item("U_Z_RetJoiNDate").Value


                todate = oRecSet.Fields.Item("U_Z_EndDate").Value
                startdt = oRecSet.Fields.Item("U_Z_StartDate").Value
                dtRetjoin = oRecSet.Fields.Item("U_Z_RetJoiNDate").Value

                'startdt = Date.ParseExact(strStartdt, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                'dtRetjoin = Date.ParseExact(strRetdt, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                'todate = Date.ParseExact(strtodt, "dd/MM/yyyy", CultureInfo.InvariantCulture)

                dtTo = dtRetjoin.AddDays(-1)
                Nodays = getNodays(startdt, dtTo)

                intDiff = CDbl(Nodays)
                ' Dim dblHolidaysCount As Double = getHolidaysinLeaveDays(oRecSet.Fields.Item("U_Z_EMPID").Value, oRecSet.Fields.Item("U_Z_Cutoff").Value, startdt, dtTo)
                ' oGrid.DataTable.SetValue("U_Z_TotalLeave", pVal.Row, dblHolidaysCount)
                'txtotalbal.Text = dblHolidaysCount
                Dim dblHolidays As Double = getHolidayCount(oRecSet.Fields.Item("U_Z_EMPID").Value, oRecSet.Fields.Item("U_Z_Cutoff").Value, startdt, dtTo, objDA.objMainCompany)
                intDiff = intDiff - dblHolidays
                Nodays = intDiff

                objDA.strQuery = "Select * from ""@Z_PAY_OLETRANS"" where ""U_Z_EMPID""='" & oRecSet.Fields.Item("U_Z_EMPID").Value & "' and  ""U_Z_TrnsCode""='" & oRecSet.Fields.Item("U_Z_TrnsCode").Value & "' and U_Z_StartDate='" & startdt.ToString("yyyy-MM-dd") & "' and U_Z_EndDate='" & todate.ToString("yyyy-MM-dd") & "'"
                oRec2.DoQuery(objDA.strQuery)
                If oRec2.RecordCount > 0 Then
                    objDA.strQuery = "Update [@Z_PAY_OLETRANS] set U_Z_EndDate='" & dtTo.ToString("yyyy-MM-dd") & "',U_Z_NoofDays='" & Nodays & "',U_Z_ReJoiNDate='" & dtRetjoin.ToString("yyyy-MM-dd") & "' where Code='" & oRec2.Fields.Item("Code").Value & "'"
                    oTemp.DoQuery(objDA.strQuery)
                    UpdateLeaveBalance_Transaction(objDA.objMainCompany, oRecSet.Fields.Item("U_Z_EMPID").Value, oRecSet.Fields.Item("U_Z_TrnsCode").Value, oRecSet.Fields.Item("U_Z_Year").Value, oRecSet.Fields.Item("U_Z_Month").Value)
                    objDA.strmsg = "Success"
                    Return objDA.strmsg
                End If
            End If
        Catch ex As Exception
            objDA.strmsg = ex.Message
            Return objDA.strmsg
        End Try
    End Function
    Private Function getHolidayCount(ByVal aEmpID As String, ByVal aCuttoff As String, ByVal dtStartDate As Date, ByVal dtEndDate As Date, ByVal objMainCompany As SAPbobsCOM.Company) As Double
        Dim dblHolidays As Double = 0
        Dim oRec, oRec1, otemp As SAPbobsCOM.Recordset
        Dim aDate As Date = dtStartDate
        oRec = objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        oRec1 = objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        otemp = objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        oRec.DoQuery("Select * from OHEM where empID=" & aEmpID)
        If oRec.RecordCount > 0 Then
            If oRec.Fields.Item("U_Z_HldCode").Value <> "" Then
                oRec1.DoQuery("Select * from OHLD where HldCode='" & oRec.Fields.Item("U_Z_HldCode").Value & "'")
                If oRec1.RecordCount > 0 Then
                    While dtStartDate <= dtEndDate
                        If aCuttoff = "B" Or aCuttoff = "W" Then
                            '     MsgBox(WeekdayName(1))
                            Dim strweekname1, strweekname2 As String
                            strweekname1 = WeekdayName(oRec1.Fields.Item("WndFrm").Value)
                            strweekname2 = WeekdayName(oRec1.Fields.Item("WndTo").Value)
                            If WeekdayName(Weekday(dtStartDate)) = strweekname1 Or WeekdayName(Weekday(dtStartDate)) = strweekname2 Then
                                dblHolidays = dblHolidays + 1
                            End If
                        End If
                        If aCuttoff = "H" Or aCuttoff = "B" Then
                            otemp.DoQuery("Select * from [HLD1] where ('" & dtStartDate.ToString("yyyy-MM-dd") & "' between strdate and enddate) and  hldCode='" & oRec.Fields.Item("U_Z_HldCode").Value & "'")
                            If otemp.RecordCount > 0 Then
                                dblHolidays = dblHolidays + 1
                            End If
                        End If
                        dtStartDate = dtStartDate.AddDays(1)
                    End While
                End If
            End If
        End If
        Return dblHolidays
    End Function
    Public Function getNodays(ByVal FromDate As Date, ByVal ToDate As Date) As String
        Try
            '  ret(DateDiff(DateInterval.Day, FromDate, ToDate).ToString())

            Dim OffCycle As String = ""
            objDA.strQuery = "select datediff(D,'" & FromDate.ToString("yyyy/MM/dd") & "','" & ToDate.ToString("yyyy/MM/dd") & "')"
            objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
            objDA.con.Open()
            objDA.sqlda = New SqlDataAdapter(objDA.cmd)
            objDA.dt.Clear()
            objDA.sqlda.Fill(objDA.dt)
            If objDA.dt.Rows.Count > 0 Then
                OffCycle = objDA.dt.Rows(0)(0).ToString() + 1
            End If
            objDA.con.Close()
            Return OffCycle
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function AddUDTPayroll(ByVal strHeadcode As String, ByVal SapCompany As SAPbobsCOM.Company) As String
        Dim strTable, strCode, strQuery As String
        Dim oUserTable As SAPbobsCOM.UserTable
        Dim oRecSet, oRec2, oTemp As SAPbobsCOM.Recordset
        Try
            objDA.objMainCompany = SapCompany
            oRecSet = objDA.objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oRec2 = objDA.objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oTemp = objDA.objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oUserTable = objDA.objMainCompany.UserTables.Item("Z_PAY_OLETRANS")
            strTable = "[@Z_PAY_OLETRANS]"
            Try
                strQuery = "Select U_Z_EmpID,U_Z_EMPNAME,U_Z_TrnsCode,U_Z_LeaveName,U_Z_NoofDays,U_Z_Year,Convert(varchar(10),U_Z_ReJoiNDate,120) AS U_Z_ReJoiNDate,Convert(varchar(10),U_Z_StartDate,120) AS U_Z_StartDate,Convert(varchar(10),U_Z_EndDate,120) AS U_Z_EndDate,U_Z_Notes,U_Z_Month from ""@Z_PAY_OLETRANS1"" where ""U_Z_Status""='A' and  ""Code""='" & strHeadcode & "'"
                oRecSet.DoQuery(strQuery)
                If oRecSet.RecordCount > 0 Then
                    objDA.strmsg = validateLeaveEntries(oRecSet.Fields.Item("U_Z_EMPID").Value, oRecSet.Fields.Item("U_Z_TrnsCode").Value, oRecSet.Fields.Item("U_Z_StartDate").Value, oRecSet.Fields.Item("U_Z_EndDate").Value, objDA.objMainCompany)
                    If objDA.strmsg <> "Success" Then
                        Return objDA.strmsg
                        Exit Function
                    End If
                    strCode = objDA.Getmaxcode(strTable, "Code")
                    ' oUserTable.Code = strCode
                    ' oUserTable.Name = strCode
                    oRec2.DoQuery("Select * from OHEM where empID=" & oRecSet.Fields.Item("U_Z_EMPID").Value)
                    'Try
                    '    oUserTable.UserFields.Fields.Item("U_Z_EmpId1").Value = oRec2.Fields.Item("U_Z_EmpID").Value
                    'Catch ex As Exception
                    'End Try
                    'oUserTable.UserFields.Fields.Item("U_Z_EMPID").Value = oRecSet.Fields.Item("U_Z_EMPID").Value
                    'oUserTable.UserFields.Fields.Item("U_Z_EMPNAME").Value = oRecSet.Fields.Item("U_Z_EMPNAME").Value
                    'oUserTable.UserFields.Fields.Item("U_Z_TrnsCode").Value = oRecSet.Fields.Item("U_Z_TrnsCode").Value
                    'oUserTable.UserFields.Fields.Item("U_Z_LeaveName").Value = oRecSet.Fields.Item("U_Z_LeaveName").Value
                    'oUserTable.UserFields.Fields.Item("U_Z_StartDate").Value = oRecSet.Fields.Item("U_Z_StartDate").Value
                    'oUserTable.UserFields.Fields.Item("U_Z_EndDate").Value = oRecSet.Fields.Item("U_Z_EndDate").Value
                    'oUserTable.UserFields.Fields.Item("U_Z_NoofDays").Value = oRecSet.Fields.Item("U_Z_NoofDays").Value
                    'oUserTable.UserFields.Fields.Item("U_Z_Notes").Value = oRecSet.Fields.Item("U_Z_Notes").Value
                    'oUserTable.UserFields.Fields.Item("U_Z_Month").Value = oRecSet.Fields.Item("U_Z_Month").Value
                    'oUserTable.UserFields.Fields.Item("U_Z_Year").Value = oRecSet.Fields.Item("U_Z_Year").Value
                    'oUserTable.UserFields.Fields.Item("U_Z_ReJoiNDate").Value = oRecSet.Fields.Item("U_Z_ReJoiNDate").Value
                    'oUserTable.UserFields.Fields.Item("U_Z_Cutoff").Value = getCutoff(oRecSet.Fields.Item("U_Z_TrnsCode").Value)
                    'If oUserTable.Add <> 0 Then
                    '    objDA.strmsg = objDA.objMainCompany.GetLastErrorDescription
                    'Else
                    Dim CutOff As String = getCutoff(oRecSet.Fields.Item("U_Z_TrnsCode").Value)
                    Try
                        objDA.strQuery = "Insert into [@Z_PAY_OLETRANS](Code,Name,U_Z_EmpId1,U_Z_EMPID,U_Z_EMPNAME,U_Z_TrnsCode,U_Z_LeaveName,U_Z_StartDate,U_Z_EndDate,U_Z_NoofDays,U_Z_Notes,U_Z_Month,U_Z_Year,U_Z_ReJoiNDate,U_Z_Cutoff)"
                        objDA.strQuery += " Values ('" & strCode & "','" & strCode & "','" & oRec2.Fields.Item("U_Z_EmpID").Value & "','" & oRecSet.Fields.Item("U_Z_EMPID").Value & "','" & oRecSet.Fields.Item("U_Z_EMPNAME").Value & "','" & oRecSet.Fields.Item("U_Z_TrnsCode").Value & "',"
                        objDA.strQuery += " '" & oRecSet.Fields.Item("U_Z_LeaveName").Value & "','" & oRecSet.Fields.Item("U_Z_StartDate").Value & "','" & oRecSet.Fields.Item("U_Z_EndDate").Value & "','" & oRecSet.Fields.Item("U_Z_NoofDays").Value & "','" & oRecSet.Fields.Item("U_Z_Notes").Value & "','" & oRecSet.Fields.Item("U_Z_Month").Value & "',"
                        objDA.strQuery += " '" & oRecSet.Fields.Item("U_Z_Year").Value & "','" & oRecSet.Fields.Item("U_Z_ReJoiNDate").Value & "','" & CutOff & "')"
                        oTemp.DoQuery(objDA.strQuery)
                    Catch ex As Exception
                        objDA.strQuery = "Insert into [@Z_PAY_OLETRANS](Code,Name,U_Z_EMPID,U_Z_EMPNAME,U_Z_TrnsCode,U_Z_LeaveName,U_Z_StartDate,U_Z_EndDate,U_Z_NoofDays,U_Z_Notes,U_Z_Month,U_Z_Year,U_Z_ReJoiNDate,U_Z_Cutoff)"
                        objDA.strQuery += " Values ('" & strCode & "','" & strCode & "','" & oRecSet.Fields.Item("U_Z_EMPID").Value & "','" & oRecSet.Fields.Item("U_Z_EMPNAME").Value & "','" & oRecSet.Fields.Item("U_Z_TrnsCode").Value & "',"
                        objDA.strQuery += " '" & oRecSet.Fields.Item("U_Z_LeaveName").Value & "','" & oRecSet.Fields.Item("U_Z_StartDate").Value & "','" & oRecSet.Fields.Item("U_Z_EndDate").Value & "','" & oRecSet.Fields.Item("U_Z_NoofDays").Value & "','" & oRecSet.Fields.Item("U_Z_Notes").Value & "','" & oRecSet.Fields.Item("U_Z_Month").Value & "',"
                        objDA.strQuery += " '" & oRecSet.Fields.Item("U_Z_Year").Value & "','" & oRecSet.Fields.Item("U_Z_ReJoiNDate").Value & "','" & CutOff & "')"
                        oTemp.DoQuery(objDA.strQuery)
                    End Try
                    UpdateLeaveBalance_Transaction(objDA.objMainCompany, oRecSet.Fields.Item("U_Z_EMPID").Value, oRecSet.Fields.Item("U_Z_TrnsCode").Value, oRecSet.Fields.Item("U_Z_Year").Value, oRecSet.Fields.Item("U_Z_Month").Value)
                    objDA.strmsg = "Success"
                    'End If
                End If
            Catch ex As Exception
                objDA.strmsg = ex.Message
                Return objDA.strmsg
            End Try
        Catch ex As Exception
            objDA.strmsg = ex.Message
            Return objDA.strmsg
        End Try
        Return objDA.strmsg
    End Function
    Public Function getCutoff(ByVal LveCode As String) As String
        Try
            Dim Cutoff As String
            objDA.con.Open()
            objDA.cmd = New SqlCommand("Select T0.[U_Z_Cutoff]  from ""@Z_PAY_LEAVE"" T0 where Code='" & LveCode & "'", objDA.con)
            objDA.cmd.CommandType = CommandType.Text
            Cutoff = objDA.cmd.ExecuteScalar()
            objDA.con.Close()
            Return Cutoff
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub UpdateLeaveBalance_Transaction(ByVal objMainCompany As SAPbobsCOM.Company, ByVal aEmpID As String, ByVal aCode As String, ByVal ayear As Integer, ByVal amonth As Integer)
        Dim OTemp, otemp1, otemp2, otemp3 As SAPbobsCOM.Recordset
        Dim strRefCode, strEmpRefcode As String
        OTemp = objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        otemp1 = objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        otemp2 = objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        otemp3 = objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        Dim dblCarriedForward, dblYearly, dblTransaction, dblAdjustment, dblAccurred As Double
        Dim oTst, oTerms As SAPbobsCOM.Recordset
        Dim stString, strQuery, strLeaveName As String
        oTst = objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        oTerms = objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        Dim intyear, intMont As Integer
        For intLoop As Integer = 0 To 0
            intyear = ayear
            intMont = amonth
            strRefCode = aEmpID
           Dim st1 As String = "Select * from [@Z_EMP_LEAVE] where U_Z_EmpID='" & strRefCode & "' and U_Z_LeaveCode ='" & aCode & "'" ' (Select Code from [@Z_PAY_LEAVE] where isnull(U_Z_Accured,'N')<>'X')"
            otemp1.DoQuery(st1)
            For intRow As Integer = 0 To otemp1.RecordCount - 1
                Dim oEar As SAPbobsCOM.Recordset
                oEar = objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                oEar.DoQuery("Select * from [@Z_PAY_LEAVE]  where Code='" & otemp1.Fields.Item("U_Z_LeaveCode").Value & "'")
                dblYearly = oEar.Fields.Item("U_Z_DaysYear").Value
                strEmpRefcode = otemp1.Fields.Item("U_Z_LeaveCode").Value
                strLeaveName = otemp1.Fields.Item("U_Z_LeaveName").Value
                Dim dblUnPostedTrns1 As Double
                stString = "select isnull(sum(U_Z_NoofDays),0)  from [@Z_PAY_OLETRANS] where Code=Name and U_Z_Trnscode='" & otemp1.Fields.Item("U_Z_LeaveCode").Value & "' and  U_Z_Year= " & intyear & " and U_Z_EmpID='" & strRefCode & "'" ' group by U_Z_EmpID"
                oTst.DoQuery(stString)
                dblUnPostedTrns1 = oTst.Fields.Item(0).Value

                stString = "select isnull(sum(U_Z_NoofDays),0),sum(U_Z_Redim) 'Transaction',sum(U_Z_Adjustment) 'Adjustment',U_Z_EmpID  from [@Z_PAYROLL5] where U_Z_Leavecode='" & otemp1.Fields.Item("U_Z_LeaveCode").Value & "' and U_Z_Year=" & intyear & " and U_Z_EmpID='" & strRefCode & "' group by U_Z_EmpID"
                oTst.DoQuery(stString)

                dblAccurred = oTst.Fields.Item(0).Value
                dblAccurred = oTst.Fields.Item(0).Value
                dblTransaction = dblUnPostedTrns1 ' oTst.Fields.Item(1).Value
                dblAdjustment = oTst.Fields.Item(2).Value

                oTst.DoQuery("select SUM(U_Z_NoofDays) from [@Z_PAY_OLETRANS_OFF] where U_Z_Posted='Y' and  U_Z_EMPID='" & strRefCode & "' and U_Z_TrnsCode='" & otemp1.Fields.Item("U_Z_LeaveCode").Value & "' and U_Z_Year=" & intyear)
                Dim dblnoofEncashment As Double = oTst.Fields.Item(0).Value

                oTst.DoQuery("Select isnull(U_Z_Accured,'N') from [@Z_PAY_LEAVE] where Code='" & otemp1.Fields.Item("U_Z_LeaveCode").Value & "'")
                Dim blnCAFW As Boolean = False
                If oTst.Fields.Item(0).Value = "Y" Then
                    blnCAFW = True
                End If

                strQuery = "Select * from [@Z_EMP_LEAVE_BALANCE] where U_Z_LeaveCode='" & otemp1.Fields.Item("U_Z_LeaveCode").Value & "' and U_Z_EmpID='" & strRefCode & "'  and U_Z_Year=" & intyear
                oTst.DoQuery(strQuery)
                Dim dblFinalBalance, dblOB, dblClosing As Double

                If oTst.RecordCount > 0 Then
                    strQuery = "Select isnull(""U_Z_CAFWD"",0) ""U_Z_CAFWD"",isnull(""U_Z_Entile"",0) ""Yearly"",""Code"",isnull(""U_Z_OB"",0) ""OB"" from ""@Z_EMP_LEAVE_BALANCE"" where ""U_Z_LeaveCode""='" & otemp1.Fields.Item("U_Z_LeaveCode").Value & "' and ""U_Z_EmpID""='" & strRefCode & "'  and ""U_Z_Year""=" & intyear
                    oTst.DoQuery(strQuery)
                    Dim strcode1 As String = oTst.Fields.Item("Code").Value
                    dblYearly = oTst.Fields.Item("Yearly").Value
                    dblOB = oTst.Fields.Item("OB").Value
                    'new addition 2014-01-16
                    If blnCAFW = False Then
                        dblClosing = dblYearly
                    Else
                        dblClosing = 0
                    End If
                    'end
                    dblCarriedForward = oTst.Fields.Item("U_Z_CAFWD").Value
                    dblFinalBalance = dblClosing + dblOB + dblCarriedForward + dblAccurred - dblTransaction + dblAdjustment - dblnoofEncashment ' - dblUnPostedTrns1
                    strQuery = "Update ""@Z_EMP_LEAVE_BALANCE"" set  ""U_Z_OB""='" & dblOB & "', ""U_Z_LeaveName""='" & strLeaveName & "', ""U_Z_CAFWD""='" & dblCarriedForward & "',  ""U_Z_ACCR""='" & dblAccurred & "', ""U_Z_Adjustment""='" & dblAdjustment & "',""U_Z_Trans""='" & dblTransaction & "',""U_Z_Balance""='" & dblFinalBalance & "' where ""Code""='" & strcode1 & "'" ' U_Z_LeaveCode='" & otemp2.Fields.Item("U_Z_LeaveCode").Value & "' and U_Z_Year=" & ayear
                    oTst.DoQuery(strQuery)
                Else
                    strQuery = "Select isnull(""U_Z_OB"",0) ""OB"", isnull(""U_Z_Balance"",0) ""U_Z_CAFWD"",isnull(""U_Z_Entile"",0) ""Yearly"" from ""@Z_EMP_LEAVE_BALANCE"" where ""U_Z_LeaveCode""='" & otemp1.Fields.Item("U_Z_LeaveCode").Value & "' and ""U_Z_EmpID""='" & strRefCode & "'  and ""U_Z_Year""=" & intyear - 1
                    oTst.DoQuery(strQuery)
                    dblOB = oTst.Fields.Item("OB").Value
                    dblCarriedForward = oTst.Fields.Item("U_Z_CAFWD").Value
                    dblYearly = dblYearly
                    'new addition 2014-01-16
                    If blnCAFW = False Then
                        dblClosing = dblYearly
                    Else
                        dblClosing = 0
                    End If
                    'end
                    dblFinalBalance = dblClosing + dblOB + dblCarriedForward + dblAccurred - dblTransaction + dblAdjustment - dblnoofEncashment '- dblUnPostedTrns1
                    Dim strCode1 As String = objDA.Getmaxcode("[@Z_EMP_LEAVE_BALANCE]", "Code")
                    strQuery = "Insert into [@Z_EMP_LEAVE_BALANCE] (code,Name,U_Z_EmpID,U_Z_Year,U_Z_CAFWD,U_Z_LeaveCode,U_Z_LeaveName) values('" & strCode1 & "','" & strCode1 & "','" & strRefCode & "'," & intyear & ",'" & dblCarriedForward & "','" & otemp1.Fields.Item("U_Z_LeaveCode").Value & "','" & strLeaveName & "')"
                    oTst.DoQuery(strQuery)
                    strQuery = "Update [@Z_EMP_LEAVE_BALANCE] set U_Z_OB='" & dblOB & "', U_Z_Entile='" & dblYearly & "', U_Z_CAFWD='" & dblCarriedForward & "',  U_Z_ACCR='" & dblAccurred & "', U_Z_Adjustment='" & dblAdjustment & "',U_Z_Trans='" & dblTransaction & "',U_Z_Balance='" & dblFinalBalance & "' where  Code='" & strCode1 & "'"
                    oTst.DoQuery(strQuery)
                End If
                otemp1.MoveNext()
            Next
        Next
    End Sub

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
    Public Function AddtoUDT1_PayrollTrans(ByVal aCode As String, ByVal objCompany As SAPbobsCOM.Company) As String
        Try
            'If ConnectSAP() = True Then
            Dim oUserTable As SAPbobsCOM.UserTable
            Dim strCode, Redimamt As String
            Dim oTemp, orec1 As SAPbobsCOM.Recordset
            oTemp = objCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            orec1 = objCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oTemp.DoQuery("select Code,Name,U_Z_Month,U_Z_AlloCode,U_Z_EmpID,U_Z_Year,U_Z_Notes,Convert(varchar(10),U_Z_Claimdt,120) AS  U_Z_Claimdt,U_Z_ReimAmt from [@Z_HR_EXPCL] where isnull(U_Z_PayPosted,'N')='N' and U_Z_Reimburse='Y' and U_Z_Posting='P' and  U_Z_APPStatus='A'  and Code='" & aCode & "'")
            If oTemp.RecordCount > 0 Then
                'oUserTable = objCompany.UserTables.Item("Z_PAY_TRANS")
                strCode = objDA.Getmaxcode("[@Z_PAY_TRANS]", "Code")
                'oUserTable.Code = strCode
                'oUserTable.Name = strCode
                orec1.DoQuery("Select empID,U_Z_EmpId 'U_Z_EMPID',isnull(firstName,'')+' ' + isnull(middleName,'') +' ' + isnull(lastName,'') 'Name' from OHEM where empId=" & oTemp.Fields.Item("U_Z_EmpID").Value)
                'oUserTable.UserFields.Fields.Item("U_Z_EmpId1").Value = orec1.Fields.Item("U_Z_EmpID").Value
                'oUserTable.UserFields.Fields.Item("U_Z_TYPE").Value = "E"
                'oUserTable.UserFields.Fields.Item("U_Z_Month").Value = oTemp.Fields.Item("U_Z_Month").Value
                'oUserTable.UserFields.Fields.Item("U_Z_EMPNAME").Value = orec1.Fields.Item("Name").Value
                'oUserTable.UserFields.Fields.Item("U_Z_Year").Value = oTemp.Fields.Item("U_Z_Year").Value
                'oUserTable.UserFields.Fields.Item("U_Z_EMPID").Value = oTemp.Fields.Item("U_Z_EmpID").Value
                'oUserTable.UserFields.Fields.Item("U_Z_TrnsCode").Value = oTemp.Fields.Item("U_Z_AlloCode").Value
                'oUserTable.UserFields.Fields.Item("U_Z_StartDate").Value = oTemp.Fields.Item("U_Z_Claimdt").Value
                'oUserTable.UserFields.Fields.Item("U_Z_EndDate").Value = ""
                'oUserTable.UserFields.Fields.Item("U_Z_Amount").Value = getDocumentQuantity(oTemp.Fields.Item("U_Z_ReimAmt").Value.ToString, objCompany) ' Redimamt.Replace("$", "") 'oTemp.Fields.Item("U_Z_ReimAmt").Value
                'oUserTable.UserFields.Fields.Item("U_Z_NoofHours").Value = 0
                'oUserTable.UserFields.Fields.Item("U_Z_Notes").Value = oTemp.Fields.Item("U_Z_Notes").Value
                'oUserTable.UserFields.Fields.Item("U_Z_offTool").Value = "N"

                Dim RedimAmount As Double = getDocumentQuantity(oTemp.Fields.Item("U_Z_ReimAmt").Value.ToString, objCompany)
                objDA.strQuery = "Insert into [@Z_PAY_TRANS](Code,Name,U_Z_EmpId1,U_Z_TYPE,U_Z_Month,U_Z_EMPNAME,U_Z_Year,U_Z_EMPID,U_Z_TrnsCode,U_Z_StartDate,U_Z_Amount,U_Z_NoofHours,U_Z_Notes,U_Z_offTool)"
                objDA.strQuery += " Values ('" & strCode & "','" & strCode & "','" & orec1.Fields.Item("U_Z_EmpID").Value & "','E','" & oTemp.Fields.Item("U_Z_Month").Value & "','" & orec1.Fields.Item("Name").Value & "',"
                objDA.strQuery += " '" & oTemp.Fields.Item("U_Z_Year").Value & "','" & oTemp.Fields.Item("U_Z_EmpID").Value & "','" & oTemp.Fields.Item("U_Z_AlloCode").Value & "','" & oTemp.Fields.Item("U_Z_Claimdt").Value & "'," & RedimAmount & ",'0',"
                objDA.strQuery += " '" & oTemp.Fields.Item("U_Z_Notes").Value & "','N')"
                oTemp.DoQuery(objDA.strQuery)
                orec1.DoQuery("Update [@Z_HR_EXPCL] set U_Z_PayPosted='Y' where Code='" & aCode & "'")
                objDA.strmsg = "Success"

                'If oUserTable.Add() <> 0 Then
                '    objDA.strmsg = objCompany.GetLastErrorDescription()
                'Else

                'End If
            End If
            ' End If
        Catch ex As Exception
            objDA.strmsg = ex.Message
            Return objDA.strmsg
        End Try
        Return objDA.strmsg
    End Function
    Public Function CreateJournelVouchers(ByVal aCode As String, ByVal reimbused As String, ByVal SAPCompany As SAPbobsCOM.Company) As String
        Dim strQuery As String
        Dim oTemp, orec1, oRecSet As SAPbobsCOM.Recordset
        Dim strDocCurrency, strDimentions As String
        Dim strDim As String()
        oRecSet = SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        oTemp = SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        orec1 = SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        If reimbused = "No" Or reimbused = "N" Then
            strQuery = "select * from [@Z_HR_EXPCL] where isnull(U_Z_PayPosted,'N')='N' and U_Z_Reimburse='N' and U_Z_Posting='G' and  U_Z_APPStatus='A'  and Code='" & aCode & "'"
            oTemp.DoQuery(strQuery)
            If oTemp.RecordCount > 0 Then
                strDocCurrency = oTemp.Fields.Item("U_Z_Currency").Value
                Dim Vjov As SAPbobsCOM.JournalVouchers
                Vjov = SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalVouchers)
                Vjov.JournalEntries.Lines.SetCurrentLine(0)
                Vjov.JournalEntries.Reference = oTemp.Fields.Item("U_Z_EmpID").Value
                Vjov.JournalEntries.Reference2 = objDA.getEmpName(oTemp.Fields.Item("U_Z_EmpID").Value)
                strDimentions = oTemp.Fields.Item("U_Z_Dimension").Value
                strDim = strDimentions.Split(";")
                Dim strdebitCode As String = objDA.getSAPAccount(oTemp.Fields.Item("U_Z_DebitCode").Value, SAPCompany)
                Vjov.JournalEntries.Lines.AccountCode = strdebitCode
                If strDocCurrency <> SAPCompany.GetCompanyService.GetAdminInfo.LocalCurrency Then
                    Vjov.JournalEntries.Lines.FCCurrency = strDocCurrency
                    Vjov.JournalEntries.Lines.FCDebit = objDA.getDocumentQuantity(oTemp.Fields.Item("U_Z_CurAmt").Value, SAPCompany)
                ElseIf strDocCurrency = SAPCompany.GetCompanyService.GetAdminInfo.SystemCurrency And SAPCompany.GetCompanyService.GetAdminInfo.SystemCurrency <> SAPCompany.GetCompanyService.GetAdminInfo.LocalCurrency Then
                    Vjov.JournalEntries.Lines.FCCurrency = strDocCurrency
                    Vjov.JournalEntries.Lines.FCDebit = objDA.getDocumentQuantity(oTemp.Fields.Item("U_Z_CurAmt").Value, SAPCompany)
                Else
                    Vjov.JournalEntries.Lines.Debit = objDA.getDocumentQuantity(oTemp.Fields.Item("U_Z_UsdAmt").Value, SAPCompany)
                End If
                Try
                    If strDim(0) <> "" Then
                        Vjov.JournalEntries.Lines.CostingCode = strDim(0)
                    End If
                    If strDim(1) <> "" Then
                        Vjov.JournalEntries.Lines.CostingCode2 = strDim(1)
                    End If
                    If strDim(2) <> "" Then
                        Vjov.JournalEntries.Lines.CostingCode3 = strDim(2)
                    End If
                    If strDim(3) <> "" Then
                        Vjov.JournalEntries.Lines.CostingCode4 = strDim(3)
                    End If
                    If strDim(4) <> "" Then
                        Vjov.JournalEntries.Lines.CostingCode5 = strDim(4)
                    End If
                Catch ex As Exception

                End Try


                Vjov.JournalEntries.Lines.Add()
                Vjov.JournalEntries.Lines.SetCurrentLine(1)
                Dim strCreditCode As String = objDA.getSAPAccount(oTemp.Fields.Item("U_Z_CreditCode").Value, SAPCompany)
                Vjov.JournalEntries.Lines.AccountCode = strCreditCode
                '  Vjov.JournalEntries.Lines.Credit = getDocumentQuantity(oTemp.Fields.Item("U_Z_UsdAmt").Value)
                If strDocCurrency <> SAPCompany.GetCompanyService.GetAdminInfo.LocalCurrency Then
                    Vjov.JournalEntries.Lines.FCCurrency = strDocCurrency
                    Vjov.JournalEntries.Lines.FCCredit = objDA.getDocumentQuantity(oTemp.Fields.Item("U_Z_CurAmt").Value, SAPCompany)
                ElseIf strDocCurrency = SAPCompany.GetCompanyService.GetAdminInfo.SystemCurrency And SAPCompany.GetCompanyService.GetAdminInfo.SystemCurrency <> SAPCompany.GetCompanyService.GetAdminInfo.LocalCurrency Then
                    Vjov.JournalEntries.Lines.FCCurrency = strDocCurrency
                    Vjov.JournalEntries.Lines.FCCredit = objDA.getDocumentQuantity(oTemp.Fields.Item("U_Z_CurAmt").Value, SAPCompany)
                Else
                    Vjov.JournalEntries.Lines.Credit = objDA.getDocumentQuantity(oTemp.Fields.Item("U_Z_UsdAmt").Value, SAPCompany)
                End If

                Try

                    If strDim(0) <> "" Then
                        Vjov.JournalEntries.Lines.CostingCode = strDim(0)
                    End If
                    If strDim(1) <> "" Then
                        Vjov.JournalEntries.Lines.CostingCode2 = strDim(1)
                    End If
                    If strDim(2) <> "" Then
                        Vjov.JournalEntries.Lines.CostingCode3 = strDim(2)
                    End If
                    If strDim(3) <> "" Then
                        Vjov.JournalEntries.Lines.CostingCode4 = strDim(3)
                    End If
                    If strDim(4) <> "" Then
                        Vjov.JournalEntries.Lines.CostingCode5 = strDim(4)
                    End If
                Catch ex As Exception

                End Try

                '    Vjov.JournalEntries.Lines.Add()
                If Vjov.Add <> 0 Then
                    objDA.strmsg = SAPCompany.GetLastErrorDescription
                Else
                    Dim strJvNo As String
                    SAPCompany.GetNewObjectCode(strJvNo)
                    orec1.DoQuery("Update [@Z_HR_EXPCL] set U_Z_JVNo='" & strJvNo & "',U_Z_PayPosted='Y'  where Code='" & aCode & "'")
                    objDA.strmsg = "Success"
                End If
            End If
        Else
            strQuery = "select * from [@Z_HR_EXPCL] where isnull(U_Z_PayPosted,'N')='N' and U_Z_Reimburse='Y' and U_Z_Posting='G' and  U_Z_APPStatus='A'  and Code='" & aCode & "'"
            oTemp.DoQuery(strQuery)
            If oTemp.RecordCount > 0 Then
                strDocCurrency = oTemp.Fields.Item("U_Z_Currency").Value
                Dim Vjov As SAPbobsCOM.JournalVouchers
                strDimentions = oTemp.Fields.Item("U_Z_Dimension").Value
                strDim = strDimentions.Split(";")
                Vjov = SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalVouchers)
                Vjov.JournalEntries.Reference = oTemp.Fields.Item("U_Z_EmpID").Value
                Vjov.JournalEntries.Reference2 = objDA.getEmpName(oTemp.Fields.Item("U_Z_EmpID").Value)
                Vjov.JournalEntries.Lines.SetCurrentLine(0)
                Vjov.JournalEntries.Lines.AccountCode = objDA.getSAPAccount(oTemp.Fields.Item("U_Z_DebitCode").Value, SAPCompany)
                If strDocCurrency <> SAPCompany.GetCompanyService.GetAdminInfo.LocalCurrency Then
                    Vjov.JournalEntries.Lines.FCCurrency = strDocCurrency
                    Vjov.JournalEntries.Lines.FCDebit = objDA.getDocumentQuantity(oTemp.Fields.Item("U_Z_CurAmt").Value, SAPCompany)
                ElseIf strDocCurrency = SAPCompany.GetCompanyService.GetAdminInfo.SystemCurrency And SAPCompany.GetCompanyService.GetAdminInfo.SystemCurrency <> SAPCompany.GetCompanyService.GetAdminInfo.LocalCurrency Then
                    Vjov.JournalEntries.Lines.FCCurrency = strDocCurrency
                    Vjov.JournalEntries.Lines.DebitSys = objDA.getDocumentQuantity(oTemp.Fields.Item("U_Z_CurAmt").Value, SAPCompany)
                Else
                    Vjov.JournalEntries.Lines.Debit = objDA.getDocumentQuantity(oTemp.Fields.Item("U_Z_ReimAmt").Value, SAPCompany)
                End If
                Try


                    If strDim(0) <> "" Then
                        Vjov.JournalEntries.Lines.CostingCode = strDim(0)
                    End If
                    If strDim(1) <> "" Then
                        Vjov.JournalEntries.Lines.CostingCode2 = strDim(1)
                    End If
                    If strDim(2) <> "" Then
                        Vjov.JournalEntries.Lines.CostingCode3 = strDim(2)
                    End If
                    If strDim(3) <> "" Then
                        Vjov.JournalEntries.Lines.CostingCode4 = strDim(3)
                    End If
                    If strDim(4) <> "" Then
                        Vjov.JournalEntries.Lines.CostingCode5 = strDim(4)
                    End If
                Catch ex As Exception

                End Try
                Vjov.JournalEntries.Lines.Add()
                Vjov.JournalEntries.Lines.SetCurrentLine(1)

                oRecSet.DoQuery("Select isnull(U_Z_CardCode,'') as U_Z_CardCode from OHEM where empID=" & oTemp.Fields.Item("U_Z_EmpID").Value)
                Dim BussCode As String = oRecSet.Fields.Item("U_Z_CardCode").Value.ToString()

                Vjov.JournalEntries.Lines.ShortName = BussCode ' objDA.getCustAccNo(BussCode)
                If strDocCurrency <> SAPCompany.GetCompanyService.GetAdminInfo.LocalCurrency Then
                    Vjov.JournalEntries.Lines.FCCurrency = strDocCurrency
                    Vjov.JournalEntries.Lines.FCCredit = objDA.getDocumentQuantity(oTemp.Fields.Item("U_Z_CurAmt").Value, SAPCompany)
                ElseIf strDocCurrency = SAPCompany.GetCompanyService.GetAdminInfo.SystemCurrency And SAPCompany.GetCompanyService.GetAdminInfo.SystemCurrency <> SAPCompany.GetCompanyService.GetAdminInfo.LocalCurrency Then
                    Vjov.JournalEntries.Lines.FCCurrency = strDocCurrency
                    Vjov.JournalEntries.Lines.CreditSys = objDA.getDocumentQuantity(oTemp.Fields.Item("U_Z_CurAmt").Value, SAPCompany)
                Else
                    Vjov.JournalEntries.Lines.Credit = objDA.getDocumentQuantity(oTemp.Fields.Item("U_Z_ReimAmt").Value, SAPCompany)
                End If
                Try

                    If strDim(0) <> "" Then
                        Vjov.JournalEntries.Lines.CostingCode = strDim(0)
                    End If
                    If strDim(1) <> "" Then
                        Vjov.JournalEntries.Lines.CostingCode2 = strDim(1)
                    End If
                    If strDim(2) <> "" Then
                        Vjov.JournalEntries.Lines.CostingCode3 = strDim(2)
                    End If
                    If strDim(3) <> "" Then
                        Vjov.JournalEntries.Lines.CostingCode4 = strDim(3)
                    End If
                    If strDim(4) <> "" Then
                        Vjov.JournalEntries.Lines.CostingCode5 = strDim(4)
                    End If
                Catch ex As Exception

                End Try
                '  Vjov.JournalEntries.Lines.Add()
                If Vjov.Add <> 0 Then
                    objDA.strmsg = SAPCompany.GetLastErrorDescription
                Else
                    Dim strJvNo As String
                    SAPCompany.GetNewObjectCode(strJvNo)
                    orec1.DoQuery("Update [@Z_HR_EXPCL] set U_Z_JVNo='" & strJvNo & "',U_Z_PayPosted='Y' where Code='" & aCode & "'")
                    objDA.strmsg = "Success"
                End If
            End If
        End If
        Return objDA.strmsg
    End Function
    Public Function validateLeaveEntries(ByVal aEmpId As String, ByVal aLeveCode As String, ByVal dtStartDate As Date, ByVal dtEndDate As Date, ByVal SapCompany As SAPbobsCOM.Company) As String
        Dim oTemp As SAPbobsCOM.Recordset
        objDA.objMainCompany = SapCompany
        oTemp = objDA.objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        Dim strSQL As String
        strSQL = "Select  * from [@Z_PAY_OLETRANS] where U_Z_EMPID='" & aEmpId & "' and '" & dtStartDate.ToString("yyyy-MM-dd") & "'  between U_Z_StartDate and U_Z_EndDate"
        oTemp.DoQuery(strSQL)
        If oTemp.RecordCount > 0 Then
            objDA.strmsg = "Leave details already exists for requested period "
            Return objDA.strmsg
        End If

        strSQL = "Select  * from [@Z_PAY_OLETRANS] where U_Z_EMPID='" & aEmpId & "' and '" & dtEndDate.ToString("yyyy-MM-dd") & "'  between U_Z_StartDate and U_Z_EndDate"
        oTemp.DoQuery(strSQL)
        If oTemp.RecordCount > 0 Then
            objDA.strmsg = "Leave details already exists for requested period "
            Return objDA.strmsg
        End If
        Return "Success"
    End Function
    Public Function validateLeave(ByVal aLeveCode As String, ByVal aLeveBal As String, ByVal aNofoDays As String, ByVal SapCompany As SAPbobsCOM.Company) As String
        Dim oTemp As SAPbobsCOM.Recordset
        objDA.objMainCompany = SapCompany
        oTemp = objDA.objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        Dim strSQL As String
        strSQL = "SElect * from ""@Z_PAY_LEAVE"" where ""Code""='" & aLeveCode & "'"
        oTemp.DoQuery(strSQL)
        If oTemp.RecordCount > 0 Then
            If oTemp.Fields.Item("U_Z_StopProces").Value = "Y" Then
                If CDbl(aLeveBal) < CDbl(aNofoDays) Then
                    objDA.strmsg = "You can able to take maximum of " & aLeveBal & " Only "
                    Return objDA.strmsg
                End If
            End If
        End If
        Return "Success"
    End Function
    Public Function ApprovalValidation(ByVal objEN As DynamicApprovalEN) As String
        Try
            Select Case objEN.HistoryType
                Case "AppShort", "EmpPos", "EmpPro", "PerHour", "NewTra", "Rec", "RegTra", "TraReq", "RetLve", "IntAppReq", "BankTime"
                    If objEN.AppStatus = "R" Then
                        If objEN.Remarks = "" Then
                            objDA.strmsg = "Remarks is missing..."
                            Return objDA.strmsg
                        End If
                    End If
                Case "ExpCli", "LveReq"
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
                                objDA.strQuery = "Select * from ""@Z_PAYROLL1"" where ""U_Z_empID""='" & objEN.EmpId & "' and ""U_Z_Month""='" & objEN.Month & "' and ""U_Z_Year""='" & objEN.Year & "' and ""U_Z_Posted""='Y'"
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
            End Select
        Catch ex As Exception
            objDA.strmsg = "" & ex.Message & ""
            Return objDA.strmsg
        End Try
        Return "Success"
    End Function

    Public Function GetLeaveBalance(ByVal objEN As DynamicApprovalEN) As DataSet
        Try
            objDA.strQuery = "select T0.U_Z_LeaveCode,T0.U_Z_LeaveName,Cast(isnull(T0.U_Z_Balance,0) as decimal(10,2)) AS U_Z_Balance from [@Z_EMP_LEAVE_BALANCE] T0 JOIN ""@Z_PAY_LEAVE"" T1 ON T0.U_Z_LeaveCode=T1.Code where T0.U_Z_EmpID='" & objEN.EmpId & "' and T0.U_Z_Year=" & objEN.Year & " and isnull(T1.U_Z_HideESS,'N')='N'"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.dss4)
            Return objDA.dss4
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetLeaveHistory(ByVal objEN As DynamicApprovalEN) As DataSet
        Try
            objDA.strQuery += "Select T0.""Code"" as ""Code"",""U_Z_TrnsCode"",T1.""Name"" as ""Name"",convert(varchar(10),""U_Z_StartDate"",103) AS ""U_Z_StartDate"",convert(varchar(10),""U_Z_EndDate"",103) AS ""U_Z_EndDate"" ,Cast(isnull(T0.U_Z_NoofDays,0) as decimal(10,2)) AS ""U_Z_NoofDays"",""U_Z_Notes"" from ""@Z_PAY_OLETRANS"" T0 inner join ""@Z_PAY_LEAVE"" T1 on T0.""U_Z_TrnsCode""=T1.""Code"" where ""U_Z_EMPID""='" & objEN.EmpId & "' order by T0.""U_Z_StartDate"" Desc"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds)
            Return objDA.ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
