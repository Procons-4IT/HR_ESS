Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Odbc
Imports DataAccess
Imports EN
Public Class MgrNewTrainApprovalDA
    Dim objEN As MgrNewTrainApprovalEN = New MgrNewTrainApprovalEN()
    Dim objDA As DBConnectionDA = New DBConnectionDA()
    Public Sub New()
        objDA.con = New SqlConnection(objDA.GetConnection)
    End Sub
    Public Function PageLoadBind() As DataSet
        Try
            objDA.strQuery = "Select ""Code"",""Name"" from OUDP order by ""Code"""
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds)
            Return objDA.ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function getEmpIDforMangers(ByVal objen As MgrNewTrainApprovalEN) As String
        Dim strEmp As String = ""
        objDA.strQuery = "Select ""empID"" from OHEM where ""manager""='" & objen.EmpID & "'"
        objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
        objDA.sqlda.Fill(objDA.ds1)
        If objDA.ds1.Tables(0).Rows.Count > 0 Then
            For intRow As Integer = 0 To objDA.ds1.Tables(0).Rows.Count - 1
                If strEmp = "" Then
                    strEmp = "'" & objDA.ds1.Tables(0).Rows(intRow)("empID").ToString() & "'"
                Else
                    strEmp = strEmp & " ,'" & objDA.ds1.Tables(0).Rows(intRow)("empID").ToString() & "'"
                End If
            Next
        Else
            strEmp = "99999"
        End If
        Return strEmp
    End Function
    Public Function PopulateTraining(ByVal objen As MgrNewTrainApprovalEN) As DataSet
        Try
            objDA.strQuery = "SELECT ""DocEntry"", CAST(""U_Z_ReqDate"" AS varchar(11)) AS ""U_Z_ReqDate"", ""U_Z_HREmpID"", ""U_Z_HREmpName"", ""U_Z_CourseName"", ""U_Z_DeptName"", ""U_Z_PosiName"", CASE ""U_Z_ReqStatus"" WHEN 'P' THEN 'Pending' WHEN 'MA' THEN 'Manager Approved' WHEN 'MR' THEN 'Manager Rejected' WHEN 'HA' THEN 'HR Approved' ELSE 'HR Rejected' END AS ""U_Z_ReqStatus"", ""U_Z_MgrStatus"", ""U_Z_MgrRemarks"" FROM ""@Z_HR_ONTREQ"" where ""U_Z_DeptCode""='" & objen.DeptCode & "' and  (""U_Z_ReqStatus""<>'HA' and ""U_Z_ReqStatus"" <>'HR') and " & objen.strConiditon & ";"
            objDA.strQuery += "SELECT ""DocEntry"", CAST(""U_Z_ReqDate"" AS varchar(11)) AS ""U_Z_ReqDate"", ""U_Z_HREmpID"", ""U_Z_HREmpName"", ""U_Z_CourseName"", ""U_Z_DeptName"", ""U_Z_PosiName"", CASE ""U_Z_ReqStatus"" WHEN 'P' THEN 'Pending' WHEN 'MA' THEN 'Manager Approved' WHEN 'MR' THEN 'Manager Rejected' WHEN 'HA' THEN 'HR Approved' ELSE 'HR Rejected' END AS ""U_Z_ReqStatus"", CASE ""U_Z_MgrStatus"" WHEN 'P' THEN 'Pending' WHEN 'MA' THEN 'Manager Approved' WHEN 'MR' THEN 'Manager Rejected' END AS ""U_Z_MgrStatus"", ""U_Z_MgrRemarks"", CASE ""U_Z_HRStatus"" WHEN 'P' THEN 'Pending' WHEN 'HA' THEN 'Approved' ELSE 'Rejected' END AS ""U_Z_HRStatus"", ""U_Z_HRRemarks"" FROM  ""@Z_HR_ONTREQ"" where ""U_Z_DeptCode""='" & objen.DeptCode & "' and " & objen.strConiditon & ";"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds2)
            Return objDA.ds2
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub UpdateNewTrainingApproval(ByVal objen As MgrNewTrainApprovalEN)
        Try
            objDA.strQuery = "Update ""@Z_HR_ONTREQ"" set  ""U_Z_MgrStatus""='" & objen.MgrStatus & "',""U_Z_MgrRemarks""='" & objen.MgrRemarks & "',""U_Z_ReqStatus""='" & objen.MgrStatus & "' where ""DocEntry""='" & objen.Code & "'"
            objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
            objDA.con.Open()
            objDA.cmd.ExecuteNonQuery()
            objDA.con.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function PopupSearchBind(ByVal objen As MgrNewTrainApprovalEN) As DataSet
        Try
            If objen.DeptCode <> "" Then
                objDA.strQuery = "Select ""Code"",""Name"" from OUDP  where  ""Name""  like '%" + objen.DeptCode + "%' "
            Else
                objDA.strQuery = "Select ""Code"",""Name"" from OUDP"
            End If
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.dss)
            Return objDA.dss
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
