Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Odbc
Imports DataAccess
Imports EN
Public Class MgrTrainReqApprovalDA
    Dim objEN As MgrTrainReqApprovalEN = New MgrTrainReqApprovalEN()
    Dim objDA As DBConnectionDA = New DBConnectionDA()
    Public Sub New()
        objDA.con = New SqlConnection(objDA.GetConnection)
    End Sub
    Public Function PageloadBind(ByVal objen As MgrTrainReqApprovalEN) As DataSet
        Try
            objDA.strQuery = "Select ""U_Z_TrainCode"",""U_Z_CourseCode"",""U_Z_CourseName"" from ""@Z_HR_OTRIN"""
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds)
            Return objDA.ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PopulateTraining(ByVal objen As MgrTrainReqApprovalEN) As DataSet
        Try
            objDA.strQuery = "SELECT ""U_Z_HREmpID"", ""U_Z_HREmpName"", ""U_Z_DeptName"", ""U_Z_Status"", ""U_Z_Remarks"", ""U_Z_MgrRegStatus"", ""U_Z_MgrRegRemarks"", ""U_Z_HRRegStatus"", ""U_Z_HrRegRemarks"", ""Code"" FROM ""@Z_HR_TRIN1"" where ""U_Z_TrainCode""='" & objen.TrainCode & "' and ""U_Z_HRRegStatus""='P' and " & objen.Condition & ";"
            objDA.strQuery += "SELECT ""U_Z_HREmpID"", ""U_Z_HREmpName"", ""U_Z_DeptName"", CASE ""U_Z_Status"" WHEN 'A' THEN 'Approved' WHEN 'R' THEN 'Rejected' WHEN 'P' THEN 'Pending' END AS ""U_Z_Status"", ""U_Z_Remarks"", CASE ""U_Z_MgrRegStatus"" WHEN 'A' THEN 'Approved' WHEN 'R' THEN 'Rejected' WHEN 'P' THEN 'Pending' END AS ""U_Z_MgrRegStatus"", ""U_Z_MgrRegRemarks"", CASE ""U_Z_HRRegStatus"" WHEN 'A' THEN 'Approved' WHEN 'R' THEN 'Rejected' WHEN 'P' THEN 'Pending' END AS ""U_Z_HRRegStatus"", ""U_Z_HrRegRemarks"", ""Code"" FROM ""@Z_HR_TRIN1"" where ""U_Z_TrainCode""='" & objen.TrainCode & "' and ""U_Z_HRRegStatus""='A' and " & objen.Condition & ";"
            objDA.strQuery += "SELECT DISTINCT (""U_Z_TrainCode""), CAST(""U_Z_DocDate"" AS varchar(11)) AS ""U_Z_DocDate"", T0.""U_Z_CourseCode"" AS ""CourseCode"", T0.""U_Z_CourseName"" AS ""CourseName"", ""U_Z_CourseTypeDesc"", CAST(""U_Z_Startdt"" AS varchar(11)) AS ""U_Z_Startdt"", CAST(""U_Z_Enddt"" AS varchar(11)) AS ""U_Z_Enddt"", ""U_Z_MinAttendees"", ""U_Z_MaxAttendees"", CAST(""U_Z_AppStdt"" AS varchar(11)) AS ""U_Z_AppStdt"", CAST(""U_Z_AppEnddt"" AS varchar(11)) AS ""U_Z_AppEnddt"", ""U_Z_InsName"", ""U_Z_NoOfHours"", ""U_Z_StartTime"", ""U_Z_EndTime"", ""U_Z_Sunday"", ""U_Z_Monday"", ""U_Z_Tuesday"", ""U_Z_Wednesday"", ""U_Z_Thursday"", ""U_Z_Friday"", ""U_Z_Saturday"", ""U_Z_AttCost"", ""U_Z_Active"" FROM ""@Z_HR_OTRIN"" T0 inner join ""@Z_HR_OCOUR"" T1 on T0.""U_Z_CourseCode""=T1.""U_Z_CourseCode""  where  ""U_Z_TrainCode""='" & objen.TrainCode & "' and ""U_Z_Active""='Y';"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds2)
            Return objDA.ds2
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function getEmpIDforMangers(ByVal objen As MgrTrainReqApprovalEN) As String
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
    Public Sub UpdateTrainingApproval(ByVal objen As MgrTrainReqApprovalEN)
        Try
            objDA.strQuery = "Update ""@Z_HR_TRIN1"" set  ""U_Z_MgrRegStatus""='" & objen.MgrStatus & "',""U_Z_MgrRegRemarks""='" & objen.MgrRemarks & "' where ""U_Z_TrainCode""='" & objen.TrainCode & "' and ""Code""='" & objen.Code & "'"
            objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
            objDA.con.Open()
            objDA.cmd.ExecuteNonQuery()
            objDA.con.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function PopupSearchBind(ByVal objen As MgrTrainReqApprovalEN) As DataSet
        Try
            If objen.TrainCode <> "" Then
                objDA.strQuery = "Select ""U_Z_TrainCode"",""U_Z_CourseCode"",""U_Z_CourseName"" from ""@Z_HR_OTRIN""  where  ""U_Z_TrainCode""  like '%" + objen.TrainCode + "%' "
            ElseIf objen.CourseCode <> "" Then
                objDA.strQuery = "Select ""U_Z_TrainCode"",""U_Z_CourseCode"",""U_Z_CourseName"" from ""@Z_HR_OTRIN""  where  ""U_Z_CourseName""  like '%" + objen.CourseCode + "%' "
            Else
                objDA.strQuery = "Select ""U_Z_TrainCode"",""U_Z_CourseCode"",""U_Z_CourseName"" from ""@Z_HR_OTRIN"""
            End If
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.dss)
            Return objDA.dss
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
