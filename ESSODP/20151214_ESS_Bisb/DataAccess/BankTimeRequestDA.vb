Imports Microsoft.VisualBasic
Imports System
Imports System.Web
Imports System.Xml
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports EN
Public Class BankTimeRequestDA
    Dim objen As BankTimeRequestEN = New BankTimeRequestEN()
    Dim objDA As DBConnectionDA = New DBConnectionDA()
    Dim oRecSet As SAPbobsCOM.Recordset
    Dim strCode, strEmpid1, WorkHour As String
    Public Sub New()
        objDA.con = New SqlConnection(objDA.GetConnection)
    End Sub
    Public Function FillLeavetype(ByVal objen As BankTimeRequestEN) As DataSet
        Try
            objDA.strQuery = "Select isnull(U_Z_Terms,'') from OHEM where empID='" & objen.Empid & "'"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds4)
            If objDA.ds4.Tables(0).Rows.Count > 0 Then
                If objDA.ds4.Tables(0).Rows(0)(0).ToString() = "" Then
                    objDA.strQuery = "Select ""Code"",""Name"" from ""@Z_PAY_LEAVE"" order by ""Code"""
                Else
                    objDA.strQuery = " Select ""U_Z_LeaveCode"" AS ""Code"",""Name""  from  ""@Z_PAY_OALMP"" T1 inner join ""@Z_PAY_LEAVE"" T0 on T0.""Code""=T1.""U_Z_LeaveCode""  where ""U_Z_Terms""='" & objDA.ds4.Tables(0).Rows(0)(0).ToString() & "'"
                End If
            End If
        Catch ex As Exception
            objDA.strQuery = "Select ""Code"",""Name"" from ""@Z_PAY_LEAVE"" order by ""Code"""
        End Try
        Try
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
        Catch ex As Exception
            objDA.strQuery = "Select ""Code"",""Name"" from ""@Z_PAY_LEAVE"" order by ""Code"""
        End Try
        objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
        objDA.sqlda.Fill(objDA.dss1)
        Return objDA.dss1
    End Function
    Public Function PageLoadBind(ByVal objen As BankTimeRequestEN) As DataSet
        Try
            objDA.strQuery = "Select ""Code"" as ""Code"",""U_Z_TrnsCode"",""U_Z_LeaveName"",convert(varchar(10),""U_Z_StartDate"",103) AS ""U_Z_StartDate"",CAST(isnull(U_Z_NoofHours,0) as decimal(10,2)) AS ""U_Z_NoofHours"" ,CAST(isnull(U_Z_NoofDays,0) as decimal(10,2)) AS ""U_Z_NoofDays"",""U_Z_Notes"",case ""U_Z_AppStatus"" when 'P' then 'Pending' when 'R' then 'Rejected' when 'A' then 'Approved' end as ""U_Z_AppStatus"",""U_Z_AppRemarks"",Case ""U_Z_CashOut"" when 'Y' then 'Yes' else 'No' end as ""U_Z_CashOut"",isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +''+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_CurApprover ),'') AS U_Z_CurApprover,"
            objDA.strQuery += " isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +''+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_NxtApprover ),'') AS U_Z_NxtApprover from ""@Z_PAY_OLADJTRANS1"" T0 where ""U_Z_EMPID""='" & objen.Empid & "' order by T0.""Code"" Desc;"
            objDA.strQuery += "Select T0.""Code"" as ""Code"",""U_Z_TrnsCode"",""U_Z_LeaveName"",convert(varchar(10),""U_Z_StartDate"",103) AS ""U_Z_StartDate"",CAST(isnull(U_Z_NoofDays,0) as decimal(10,2)) AS ""U_Z_NoofDays"",""U_Z_Notes"",Case ""U_Z_CashOut"" when 'Y' then 'Yes' else 'No' end as ""U_Z_CashOut"" from ""@Z_PAY_OLADJTRANS"" T0 where ""U_Z_EMPID""='" & objen.Empid & "' order by T0.""Code"" Desc;"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds)
            Return objDA.ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function SaveBankTimeRequest(ByVal objen As BankTimeRequestEN) As String
        Try

            oRecSet = objen.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            objDA.strQuery = "Select isnull(U_Z_EmpID,0) as 'TANo',isnull(U_Z_Workhour,8) as 'Workhour' from OHEM where empID=" & objen.Empid
            oRecSet.DoQuery(objDA.strQuery)
            strEmpid1 = oRecSet.Fields.Item("TANo").Value
            WorkHour = oRecSet.Fields.Item("Workhour").Value
            If WorkHour = 0 Then
                objen.NoofDays = 0
            Else
                objen.NoofDays = objen.NoofHours / CDbl(WorkHour)
            End If

            strCode = objDA.Getmaxcode("""@Z_PAY_OLADJTRANS1""", """Code""")
            objDA.strQuery = "Insert into ""@Z_PAY_OLADJTRANS1"" (""Code"",""Name"",""U_Z_EMPID"",""U_Z_EmpId1"",""U_Z_EMPNAME"",""U_Z_TrnsCode"",""U_Z_LeaveName"",""U_Z_StartDate"",""U_Z_NoofHours"",""U_Z_NoofDays"",""U_Z_Notes"",""U_Z_AppStatus"",""U_Z_CashOut"") "
            objDA.strQuery += "Values ('" & strCode & "','" & strCode & "','" & objen.Empid & "','" & strEmpid1.Trim() & "','" & objen.EmpName & "','" & objen.LeaveCode & "','" & objen.LeaveName & "','" & objen.FromDate.ToString("yyyy/MM/dd") & "','" & objen.NoofHours & "','" & objen.NoofDays & "','" & objen.Notes & "','" & objen.AppStatus & "','" & objen.CashOut & "')"
            objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
            objDA.con.Open()
            objDA.cmd.ExecuteNonQuery()
            objDA.con.Close()
            Return strCode 'True
        Catch ex As Exception
            Throw ex
            Return ""
        End Try
        Return ""
    End Function
    Public Function UpdateBankTimeRequest(ByVal objen As BankTimeRequestEN) As Boolean
        Try
            oRecSet = objen.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            objDA.strQuery = "Select isnull(U_Z_Workhour,8) as 'Workhour' from OHEM where empID=" & objen.Empid
            oRecSet.DoQuery(objDA.strQuery)
            WorkHour = oRecSet.Fields.Item("Workhour").Value
            objen.NoofDays = objen.NoofHours / CDbl(WorkHour)
            objDA.strQuery = "Update ""@Z_PAY_OLADJTRANS1"" set ""U_Z_AppStatus""='" & objen.AppStatus & "', ""U_Z_TrnsCode""='" & objen.LeaveCode & "',""U_Z_StartDate""='" & objen.FromDate.ToString("yyyy/MM/dd") & "',""U_Z_NoofHours""='" & objen.NoofHours & "',""U_Z_NoofDays""='" & objen.NoofDays & "',""U_Z_Notes""='" & objen.Notes & "',""U_Z_CashOut""='" & objen.CashOut & "' where ""Code""='" & objen.StrCode & "' and  ""U_Z_EMPID""='" & objen.Empid & "'"
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
    Public Function PopulateBankTimeRequest(ByVal objen As BankTimeRequestEN) As DataSet
        Try
            objDA.strQuery = "Select ""Code"",""U_Z_TrnsCode"",""U_Z_LeaveName"",convert(varchar(10),""U_Z_StartDate"",103) AS ""U_Z_StartDate"",CAST(isnull(U_Z_NoofHours,0) as decimal(10,2)) AS ""U_Z_NoofHours"" ,CAST(isnull(U_Z_NoofDays,0) as decimal(10,2)) AS ""U_Z_NoofDays"",""U_Z_Notes"",""U_Z_AppStatus"",""U_Z_CashOut"" from ""@Z_PAY_OLADJTRANS1"" where ""U_Z_EMPID""='" & objen.Empid & "' and ""Code""='" & objen.StrCode & "'"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds1)
            Return objDA.ds1
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function WithdrawRequest(ByVal objen As BankTimeRequestEN) As Boolean
        Try
            objDA.strQuery = "Delete from ""@Z_PAY_OLADJTRANS1"" where ""Code""='" & objen.StrCode & "' and  ""U_Z_EMPID""='" & objen.Empid & "'"
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
    Public Function PopupSearchBind(ByVal objen As BankTimeRequestEN) As DataSet
        Try
            If objen.LeaveCode <> "" Then
                objDA.strQuery = "Select ""Code"",""Name"" from ""@Z_PAY_LEAVE""  where  ""Code""  like '%" + objen.LeaveCode + "%' "
            ElseIf objen.LeaveName <> "" Then
                objDA.strQuery = "Select ""Code"",""Name"" from ""@Z_PAY_LEAVE""  where  ""Name""  like '%" + objen.LeaveName + "%' "
            Else
                objDA.strQuery = "Select ""Code"",""Name"" from ""@Z_PAY_LEAVE"""
            End If
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.dss)
            Return objDA.dss
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function AddtoUDT_BankTime(ByVal objen As BankTimeRequestEN) As String
        Dim oUserTable As SAPbobsCOM.UserTable
        Dim oRecSet As SAPbobsCOM.Recordset
        oRecSet = objen.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        Dim strCode, strQuery As String
        strQuery = "Select * from ""@Z_PAY_OLADJTRANS1"" where ""U_Z_AppStatus""='A' and  ""Code""='" & objen.StrCode & "'"
        oRecSet.DoQuery(strQuery)
        If oRecSet.RecordCount > 0 Then
            oUserTable = objen.SapCompany.UserTables.Item("Z_PAY_OLADJTRANS")
            strCode = objDA.Getmaxcode("[@Z_PAY_OLADJTRANS]", "Code")
            oUserTable.Code = strCode
            oUserTable.Name = strCode
            oUserTable.UserFields.Fields.Item("U_Z_EmpId1").Value = oRecSet.Fields.Item("U_Z_EmpId1").Value
            oUserTable.UserFields.Fields.Item("U_Z_EMPID").Value = oRecSet.Fields.Item("U_Z_EMPID").Value
            oUserTable.UserFields.Fields.Item("U_Z_EMPNAME").Value = oRecSet.Fields.Item("U_Z_EMPNAME").Value
            oUserTable.UserFields.Fields.Item("U_Z_TrnsCode").Value = oRecSet.Fields.Item("U_Z_TrnsCode").Value
            oUserTable.UserFields.Fields.Item("U_Z_LeaveName").Value = oRecSet.Fields.Item("U_Z_LeaveName").Value
            oUserTable.UserFields.Fields.Item("U_Z_StartDate").Value = oRecSet.Fields.Item("U_Z_StartDate").Value
            oUserTable.UserFields.Fields.Item("U_Z_NoofDays").Value = oRecSet.Fields.Item("U_Z_NoofDays").Value
            oUserTable.UserFields.Fields.Item("U_Z_Notes").Value = oRecSet.Fields.Item("U_Z_Notes").Value
            oUserTable.UserFields.Fields.Item("U_Z_CashOut").Value = oRecSet.Fields.Item("U_Z_CashOut").Value
            If oUserTable.Add() <> 0 Then
                objDA.strmsg = objen.SapCompany.GetLastErrorDescription
                Return objDA.strmsg
            End If
        End If
        Return "Success"
    End Function
End Class
