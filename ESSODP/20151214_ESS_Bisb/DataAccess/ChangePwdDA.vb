Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Odbc
Imports DataAccess
Imports EN
Public Class ChangePwdDA
    Dim objEN As ChangePwdEN = New ChangePwdEN()
    Dim objDA As DBConnectionDA = New DBConnectionDA()
    Dim Password As String
    Public Sub New()
        objDA.con = New SqlConnection(objDA.GetConnection)
    End Sub
    Public Function Checkpassword(ByVal objen As ChangePwdEN) As Boolean
        Password = objDA.Encrypt(objen.OldPwd, objDA.key)
        objDA.strQuery = "select * from ""@Z_HR_LOGIN"" where U_Z_EMPID='" & objen.EmpId & "' and U_Z_PWD='" & Password & "'"
        objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
        objDA.sqlda.Fill(objDA.ds)
        If objDA.ds.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Sub UpdatePassword(ByVal objen As ChangePwdEN)
        Password = objDA.Encrypt(objen.ConfirmPwd, objDA.key)
        objDA.strQuery = "Update ""@Z_HR_LOGIN"" set U_Z_PWD='" & Password & "' where U_Z_EMPID='" & objen.EmpId & "'"
        objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
        objDA.con.Open()
        objDA.cmd.ExecuteNonQuery()
        objDA.con.Close()
    End Sub
End Class
