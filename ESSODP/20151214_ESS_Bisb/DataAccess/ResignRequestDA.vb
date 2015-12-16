Imports Microsoft.VisualBasic
Imports System
Imports System.Web
Imports System.Xml
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports EN
Public Class ResignRequestDA
    Dim objen As LeaveRequestEN = New LeaveRequestEN()
    Dim objDA As DBConnectionDA = New DBConnectionDA()
    Public Sub New()
        objDA.con = New SqlConnection(objDA.GetConnection)
    End Sub
    Public Function PageLoadBind(ByVal objen As LeaveRequestEN) As DataSet
        Try
            objDA.strQuery = "Select ""Code"" as ""Code"",Convert(varchar(10),""U_Z_StartDate"",103) AS ""U_Z_StartDate"" ,""U_Z_Notes"",case ""U_Z_Status"" when 'P' then 'Pending' when 'R' then 'Rejected' when 'A' then 'Approved' end as ""U_Z_Status"",""U_Z_AppRemarks"" from ""@Z_PAY_OLETRANS1"" where ""U_Z_EMPID""='" & objen.Empid & "' and ""U_Z_TransType""='T' order by ""Code"" Desc"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds)
            Return objDA.ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function SaveLeaveRequest(ByVal objen As LeaveRequestEN) As Boolean
        Try
            Dim strCode As String
            strCode = objDA.Getmaxcode("""@Z_PAY_OLETRANS1""", """Code""")
            objDA.strQuery = "Insert into ""@Z_PAY_OLETRANS1"" (""Code"",""Name"",""U_Z_EMPID"",""U_Z_EMPNAME"",""U_Z_TransType"",""U_Z_StartDate"",""U_Z_Notes"",""U_Z_Status"") "
            objDA.strQuery += "Values ('" & strCode & "','" & strCode & "','" & objen.Empid & "','" & objen.EmpName & "','T','" & objen.FromDate.ToString("yyyy/MM/dd") & "','" & objen.Notes & "','P')"
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
    Public Function PopulateLeaveRequest(ByVal objen As LeaveRequestEN) As DataSet
        Try
            objDA.strQuery = "Select ""Code"",""Name"",""U_Z_EMPID"",""U_Z_EMPNAME"",""U_Z_TransType"",convert(varchar(10),""U_Z_StartDate"",103) as ""U_Z_StartDate"",""U_Z_Notes"",""U_Z_Status"" from ""@Z_PAY_OLETRANS1""  where ""U_Z_EMPID""='" & objen.Empid & "' and ""Code""='" & objen.LeaveCode & "'"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds1)
            Return objDA.ds1
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function UpdateLeaveRequest(ByVal objen As LeaveRequestEN) As Boolean
        Try
            objDA.strQuery = "Update ""@Z_PAY_OLETRANS1"" set ""U_Z_StartDate""='" & objen.FromDate.ToString("yyyy/MM/dd") & "',""U_Z_Notes""='" & objen.Notes & "' where ""Code""='" & objen.strCode & "' and  ""U_Z_EMPID""='" & objen.Empid & "'"
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
    Public Function WithdrawRequest(ByVal objen As LeaveRequestEN) As Boolean
        Try
            objDA.strQuery = "Delete from ""@Z_PAY_OLETRANS1"" where ""Code""='" & objen.strCode & "' and  ""U_Z_EMPID""='" & objen.Empid & "'"
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
End Class
