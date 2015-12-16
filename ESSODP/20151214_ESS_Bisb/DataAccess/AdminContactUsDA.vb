Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Odbc
Imports DataAccess
Imports EN
Public Class AdminContactUsDA
    Dim objEN As AdminContactUsEN = New AdminContactUsEN()
    Dim objDA As DBConnectionDA = New DBConnectionDA()
    Public Sub New()
        objDA.con = New SqlConnection(objDA.GetConnection)
    End Sub
    Public Function PageLoadBind() As DataSet
        Try
            objDA.strQuery = "Select * from ""U_CONTACTUS"";"
            objDA.strQuery += "select ""Compnyname"" from OADM;"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds)
            Return objDA.ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function InsertContactUs(ByVal objen As AdminContactUsEN) As Boolean
        Try
            objDA.strQuery = "Insert into U_CONTACTUS(U_ComapnyId,U_Empname,U_Position,U_Email,U_phone,U_Status) values ('" & objen.CompanyName & "','" & objen.EmpName & "'"
            objDA.strQuery = objDA.strQuery & ",'" & objen.Position & "','" & objen.Email & "','" & objen.Phone & "','" & objen.Status & "')"
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
    Public Function UpdateContactUs(ByVal objen As AdminContactUsEN) As Boolean
        Try
            objDA.strQuery = "Update U_CONTACTUS set U_Empname='" & objen.EmpName & "',U_Position='" & objen.Position & "',U_Email='" & objen.Email & "',"
            objDA.strQuery = objDA.strQuery & "U_phone='" & objen.Phone & "',U_Status='" & objen.Status & "' where U_DocEntry='" & objen.DocEntry & "'"
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
    Public Function PopulatUseContact(ByVal objen As AdminContactUsEN) As DataSet
        Try
            objDA.strQuery = "Select * from U_CONTACTUS where U_DocEntry='" & objen.DocEntry & "'"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds1)
            Return objDA.ds1
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
