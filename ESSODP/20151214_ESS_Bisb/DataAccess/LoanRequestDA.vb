Imports Microsoft.VisualBasic
Imports System
Imports System.Web
Imports System.Xml
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI.WebControls
Imports EN
Public Class LoanRequestDA
    Dim objDA As DBConnectionDA = New DBConnectionDA()
    Dim Status As String
    Public Sub New()
        objDA.con = New SqlConnection(objDA.GetConnection)
    End Sub
    Public Function LoanType() As DataSet
        Try
            objDA.strQuery = "SELECT ""Code"",""Name"",""U_Z_GLACC"" from ""@Z_PAY_LOAN"" where ""U_Z_ReqESS""='Y'"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds)
            Return objDA.ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PageLoadBind(ByVal Objen As LoanRequestEN) As DataSet
        Try
            objDA.strQuery = "SELECT ""U_DocEntry"", ""U_LoanCode"",""U_LoanName"",""U_RefNo"",Convert(Varchar(10),""U_ReqDate"",103) AS U_ReqDate,CAST(isnull(U_LoanAmt,0) as decimal(10,2)) AS ""U_LoanAmt"",""U_GLAccount"",Case ""U_Z_AppStatus"" when 'A' then 'Approved' when 'R' then 'Rejected' else 'Pending' end as U_Z_AppStatus,Convert(Varchar(10),""U_DisDate"",103) AS U_DisDate,Convert(Varchar(10),""U_InstDate"",103) AS U_InstDate,U_NoEMI,isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_CurApprover ),'') AS 'U_CurApprover',isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_NxtApprover ),'') AS 'U_NxtApprover' from ""U_LOANREQ"" T0 where ""U_Empid""='" & Objen.Empid & "' and U_Z_AppStatus<>'A' Order by U_DocEntry desc;"
            objDA.strQuery += "SELECT T0.[Code], T0.[Name], T0.[U_Z_EmpID], T1.""firstName""+T1.""middleName""+T1.""lastName"" 'EmpName',T0.[U_Z_LoanCode], T0.[U_Z_LoanName],CAST(isnull(T0.[U_Z_LoanAmount],0) as decimal(10,2)) AS [U_Z_LoanAmount], CAST(isnull(T0.[U_Z_EMIAmount],0) as decimal(10,2)) AS [U_Z_EMIAmount], T0.[U_Z_NoEMI],Convert(Varchar(10),T0.[U_Z_DisDate],103) AS [U_Z_DisDate],Convert(Varchar(10),T0.[U_Z_StartDate],103) AS [U_Z_StartDate],Convert(Varchar(10),T0.[U_Z_EndDate],103) AS [U_Z_EndDate], T0.[U_Z_PaidEMI], CAST(isnull(T0.[U_Z_Balance],0) as decimal(10,2)) AS [U_Z_Balance], T0.[U_Z_GLACC], T0.[U_Z_Status] FROM [dbo].[@Z_PAY5]  T0  inner Join OHEM T1 on T1.empID=T0.U_Z_EmpID where T0.""U_Z_EMPID""='" & Objen.Empid & "';"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds1)
            Return objDA.ds1
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ValidateAppTemp(ByVal Objen As LoanRequestEN) As Boolean
        Try
            objDA.strQuery = "SELECT T0.U_Z_DocType  FROM [@Z_HR_OAPPT]  T0  inner join [@Z_HR_APPT1]  T1 on T1.DocEntry=T0.DocEntry where T0.U_Z_DocType='LoanReq' and T1.U_Z_OUser='" & Objen.Empid & "'"
            objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
            objDA.con.Open()
            Status = objDA.cmd.ExecuteScalar()
            objDA.con.Close()
            If Status = "" Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function SaveLoanRequest(ByVal objEN As LoanRequestEN) As String
        Try
            objDA.strQuery = "Insert Into U_LOANREQ(U_Empid,U_Empname,U_LoanCode,U_LoanName,U_ReqDate,U_LoanAmt,U_Z_AppStatus,U_DisDate,U_InstDate,U_NoEMI) Values "
            objDA.strQuery += " ('" & objEN.Empid & "','" & objEN.EmpName & "','" & objEN.LoanType & "','" & objEN.LoanName & "',getdate()," & objEN.LoanAmount & ",'" & objEN.Status & "','" & objEN.DisDate.ToString("yyyy-MM-dd") & "','" & objEN.StartDate.ToString("yyyy-MM-dd") & "','" & objEN.NoEMI & "')"
            objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
            objDA.con.Open()
            Status = objDA.cmd.ExecuteNonQuery()
            objDA.con.Close()
        Catch ex As Exception
            Status = ex.Message
        End Try
        Return Status
    End Function
    Public Function UpdateLoanRequest(ByVal objEN As LoanRequestEN) As String
        Try
            objDA.strQuery = "Update U_LOANREQ Set U_LoanCode='" & objEN.LoanType & "',U_LoanName='" & objEN.LoanName & "',U_LoanAmt=" & objEN.LoanAmount & ",U_Z_AppStatus='" & objEN.Status & "',U_DisDate='" & objEN.DisDate.ToString("yyyy-MM-dd") & "',U_InstDate='" & objEN.StartDate.ToString("yyyy-MM-dd") & "',U_NoEMI='" & objEN.NoEMI & "' where  ""U_DocEntry""='" & objEN.StrCode & "'"
            objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
            objDA.con.Open()
            Status = objDA.cmd.ExecuteNonQuery()
            objDA.con.Close()
        Catch ex As Exception
            Status = ex.Message
        End Try
        Return Status
    End Function
    Public Function WithdrawRequest(ByVal objen As LoanRequestEN) As Boolean
        Try
            objDA.strQuery = "Delete from ""U_LOANREQ"" where ""U_DocEntry""='" & objen.strCode & "' and  ""U_Empid""='" & objen.Empid & "'"
            objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
            objDA.con.Open()
            objDA.cmd.ExecuteNonQuery()
            objDA.con.Close()
            Return True
        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function
    Public Function GetDocEntry() As String
        Try
            objDA.strQuery = "SELECT Top 1 U_DocEntry from U_LOANREQ Order by U_DocEntry Desc"
            objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
            objDA.con.Open()
            Status = objDA.cmd.ExecuteScalar()
            objDA.con.Close()
            Return Status
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function populateLoanRequest(ByVal objEN As LoanRequestEN) As DataSet
        Try
            objDA.strQuery = "SELECT ""U_DocEntry"", ""U_LoanCode"",""U_LoanName"",""U_RefNo"",Convert(Varchar(10),""U_ReqDate"",103) AS U_ReqDate,""U_LoanAmt"",""U_GLAccount"",""U_Z_AppStatus"",Convert(Varchar(10),""U_DisDate"",103) AS U_DisDate,Convert(Varchar(10),""U_InstDate"",103) AS U_InstDate,U_NoEMI from ""U_LOANREQ"" where ""U_Empid""='" & objEN.Empid & "' and U_DocEntry='" & objEN.StrCode & "'"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds2)
            Return objDA.ds2
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function LoadHistory(ByVal objEN As LoanRequestEN) As DataSet
        Try
            objDA.strQuery = " Select DocEntry,U_Z_DocEntry,U_Z_DocType,U_Z_EmpId,U_Z_EmpName,U_Z_LoanCode,U_Z_LoanAmount,convert(varchar(10),U_Z_DisDate,103) as U_Z_DisDate,convert(varchar(10),U_Z_StartDate,103) as U_Z_StartDate,U_Z_NoEMI,"
            objDA.strQuery += " U_Z_ApproveBy,convert(varchar(10),CreateDate,103) as CreateDate ,LEFT(CONVERT(VARCHAR(5), CreateTime, 9),2) + ':' + RIGHT(CONVERT(VARCHAR(30), CreateTime, 9),2) AS CreateTime, convert(varchar(10),UpdateDate,103) as UpdateDate,LEFT(CONVERT(VARCHAR(5), UpdateTime, 9),2) + ':' + RIGHT(CONVERT(VARCHAR(30), UpdateTime, 9),2) AS UpdateTime,Case U_Z_AppStatus when 'P' then 'Pending' when 'A' then 'Approved' when 'R' then 'Rejected' end AS U_Z_AppStatus,U_Z_Remarks,isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_NxtApprover ),'') AS 'U_Z_NxtApprover' From [@Z_HR_LOANHIS] T0 "
            objDA.strQuery += " Where U_Z_DocType = 'LoanReq'"
            objDA.strQuery += " And U_Z_DocEntry = '" + objEN.StrCode + "'"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds1)
            Return objDA.ds1
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetScheduledDetails(ByVal objEN As LoanRequestEN) As DataSet
        Try
            objDA.strQuery = "SELECT T0.[Code], T0.[Name], T0.[U_Z_TrnsRefCode], T0.[U_Z_EmpID], T0.[U_Z_LoanCode], T0.[U_Z_LoanName],CAST(isnull(T0.[U_Z_LoanAmount],0) as decimal(10,2)) AS U_Z_LoanAmount,convert(varchar(10),T0.[U_Z_DueDate],103) as  [U_Z_DueDate],CAST(isnull(T0.[U_Z_OB],0) as decimal(10,2)) AS [U_Z_OB],CAST(isnull(T0.U_Z_EMIAmount,0) as decimal(10,2)) AS [U_Z_EMIAmount],Case T0.U_Z_Status when 'P' then 'Paid' else 'Not Paid' end AS [U_Z_Status],Case T0.U_Z_CashPaid when 'Y' then 'Yes' else 'No' end AS [U_Z_CashPaid], T0.[U_Z_CashPaidDate],Case T0.U_Z_StopIns when 'Y' then 'Yes' else 'No' end AS [U_Z_StopIns],CAST(isnull(T0.[U_Z_Balance],0) as decimal(10,2)) AS [U_Z_Balance], T0.[U_Z_Month], T0.[U_Z_Year] FROM [@Z_PAY15]  T0 where T0.""U_Z_TrnsRefCode""='" & objEN.StrCode & "'  order by ""Code"""
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds4)
            Return objDA.ds4
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
