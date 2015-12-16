Imports Microsoft.VisualBasic
Imports System
Imports System.Web
Imports System.Xml
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports EN
Public Class ReturnLveRequestDA
    Dim objen As LeaveRequestEN = New LeaveRequestEN()
    Dim objDA As DBConnectionDA = New DBConnectionDA()
    Public Sub New()
        objDA.con = New SqlConnection(objDA.GetConnection)
    End Sub
    Public Function PageLoadBind(ByVal objen As LeaveRequestEN) As DataSet
        Try
            objDA.strQuery = "Select T0.""Code"" as ""Code"",""U_Z_TrnsCode"",T1.""Name"" as ""Name"",convert(varchar(10),""U_Z_StartDate"",103) AS ""U_Z_StartDate"","
            objDA.strQuery += " convert(varchar(10),""U_Z_EndDate"",103) AS ""U_Z_EndDate"",cast(T0.U_Z_NoofDays as decimal(10,2)) AS ""U_Z_NoofDays"",""U_Z_Notes"",convert(varchar(10),""U_Z_ReJoiNDate"",103) as ""U_Z_ReJoiNDate"","
            objDA.strQuery += " convert(varchar(10),""U_Z_RetJoiNDate"",103) as ""U_Z_RetJoiNDate"",case ""U_Z_RStatus"" when 'P' then 'Pending' when 'R' then 'Rejected'"
            objDA.strQuery += "  when 'A' then 'Approved' end as ""U_Z_RStatus"",case ""U_Z_Status"" when 'P' then 'Pending' when 'R' then 'Rejected' when 'A' then 'Approved'"
            objDA.strQuery += " end as ""U_Z_Status"",""U_Z_RAppRemarks"",isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_CurApprover ),'') AS 'U_Z_CurApprover',isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_NxtApprover ),'') AS 'U_Z_NxtApprover'"
            objDA.strQuery += " from ""@Z_PAY_OLETRANS1"" T0 inner join ""@Z_PAY_LEAVE"" T1 on T0.""U_Z_TrnsCode""=T1.""Code"" where ""U_Z_EMPID""='" & objen.Empid & "' and (""U_Z_TransType""='L' or ""U_Z_TransType""='R') and ""U_Z_Status""='A' order by T0.""Code"" Desc"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds)
            Return objDA.ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PopulateLeaveRequest(ByVal objen As LeaveRequestEN) As DataSet
        Try
            objDA.strQuery = "Select ""Code"",""Name"",""U_Z_EMPID"",""U_Z_EMPNAME"",""U_Z_TransType"",""U_Z_TrnsCode"",convert(varchar(10),""U_Z_StartDate"",103) as ""U_Z_StartDate"",convert(varchar(10),""U_Z_EndDate"",103) as ""U_Z_EndDate"",""U_Z_NoofDays"",""U_Z_Notes"",convert(varchar(10),""U_Z_ReJoiNDate"",103) as ""U_Z_ReJoiNDate"",convert(varchar(10),""U_Z_RetJoiNDate"",103) as ""U_Z_RetJoiNDate"",""U_Z_Status"",""U_Z_RStatus"" from ""@Z_PAY_OLETRANS1""  where ""U_Z_EMPID""='" & objen.Empid & "' and ""Code""='" & objen.LeaveCode & "'"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds1)
            Return objDA.ds1
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function UpdateLeaveRequest(ByVal objen As LeaveRequestEN) As Boolean
        Try
            objDA.strQuery = "Update ""@Z_PAY_OLETRANS1"" set ""U_Z_RetJoiNDate""='" & objen.RejoinDt.ToString("yyyy/MM/dd") & "',""U_Z_TransType""='R',""U_Z_Notes""='" & objen.Notes & "',""U_Z_RStatus""='" & objen.Status & "' where ""Code""='" & objen.strCode & "' and  ""U_Z_EMPID""='" & objen.Empid & "'"
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
    Public Function FillLeavetype(ByVal objen As LeaveRequestEN) As DataSet
        Try
            objDA.strQuery = "Select isnull(U_Z_Terms,'') from OHEM where empID='" & objen.Empid & "'"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds4)
            If objDA.ds4.Tables(0).Rows.Count > 0 Then
                If objDA.ds4.Tables(0).Rows(0)(0).ToString() = "" Then
                    objDA.strQuery = "Select ""Code"",""Name"" from ""@Z_PAY_LEAVE"" order by ""Code"""
                Else
                    objDA.strQuery = " Select ""U_Z_LeaveCode"" 'Code',""Name""  from  ""@Z_PAY_OALMP"" T1 inner join ""@Z_PAY_LEAVE"" T0 on T0.""Code""=T1.""U_Z_LeaveCode""  where ""U_Z_Terms""='" & objDA.ds4.Tables(0).Rows(0)(0).ToString() & "'"
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
End Class
