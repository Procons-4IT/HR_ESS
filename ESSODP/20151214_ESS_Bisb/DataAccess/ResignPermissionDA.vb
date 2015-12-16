Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Odbc
Imports DataAccess
Imports EN
Public Class ResignPermissionDA
    Dim objEN As RequestApprovalEN = New RequestApprovalEN()
    Dim objDA As DBConnectionDA = New DBConnectionDA()
    Public Sub New()
        objDA.con = New SqlConnection(objDA.GetConnection)
    End Sub
    Public Function PageloadbindResignation(ByVal objen As RequestApprovalEN) As DataSet
        Try
            objen.EmpCode = getEmpIDforMangers(objen)
            objDA.strQuery1 = "	select ""U_Z_Status"",T0.""Code"" as ""Code"",""U_Z_EMPID"",""U_Z_EMPNAME"",convert(varchar(10),""U_Z_StartDate"",103) AS ""U_Z_StartDate"",""U_Z_AppRemarks"",""U_Z_Year"", ""U_Z_Month"","
            objDA.strQuery1 += """U_Z_Notes"" from ""@Z_PAY_OLETRANS1"" T0  where ""U_Z_TransType""='T' and ""U_Z_Status""='P' and ""U_Z_EMPID"" in ( " & objen.EmpCode & ") Order by T0.""Code"" Desc;"
            objDA.strQuery1 += "select case ""U_Z_Status"" when 'A' then 'Approved' when 'P' then 'Pending' when 'R' then 'Rejected' end as ""U_Z_Status"" ,T0.""Code"" as ""Code"",""U_Z_EMPID"",""U_Z_EMPNAME"",convert(varchar(10),""U_Z_StartDate"",103) AS ""U_Z_StartDate"",""U_Z_AppRemarks"",""U_Z_Year"",DateName( month , DateAdd( month , isnull(""U_Z_Month"",1) , -1 ) ) AS ""U_Z_Month"","
            objDA.strQuery1 += """U_Z_Notes"" from ""@Z_PAY_OLETRANS1"" T0  where ""U_Z_TransType""='T' and  ""U_Z_EmpId"" in (" & objen.EmpCode & ") Order by T0.""Code"" Desc;"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery1, objDA.con)
            objDA.sqlda.Fill(objDA.ds)
            Return objDA.ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function getEmpIDforMangers(ByVal objen As RequestApprovalEN) As String
        Dim strEmp As String = ""
        objDA.strQuery = "Select ""empID"" from OHEM where ""manager""='" & objen.EmpCode & "'"
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
    Public Function PageloadbindPermission(ByVal objen As RequestApprovalEN) As DataSet
        Try
            objen.EmpCode = getEmpIDforMangers(objen)
            objDA.strQuery1 = "	select ""U_Z_Status"",T0.""Code"" as ""Code"",""U_Z_EMPID"",""U_Z_EMPNAME"",convert(varchar(10),""U_Z_StartDate"",103) AS ""U_Z_StartDate"",convert(varchar(10),""U_Z_EndDate"",103) AS ""U_Z_EndDate"",""U_Z_Notes"",""U_Z_FromTime"",""U_Z_ToTime"",""U_Z_AppRemarks"""
            objDA.strQuery1 += " from ""@Z_PAY_OLETRANS1"" T0  where ""U_Z_TransType""='P' and ""U_Z_Status""='P' and ""U_Z_EMPID"" in ( " & objen.EmpCode & ") Order by T0.""Code"" Desc;"
            objDA.strQuery1 += "select case ""U_Z_Status"" when 'A' then 'Approved' when 'P' then 'Pending' when 'R' then 'Rejected' end as ""U_Z_Status"" ,T0.""Code"" as ""Code"",""U_Z_EMPID"",""U_Z_EMPNAME"",convert(varchar(10),""U_Z_StartDate"",103) AS ""U_Z_StartDate"",convert(varchar(10),""U_Z_EndDate"",103) AS ""U_Z_EndDate"",""U_Z_Notes"",""U_Z_FromTime"",""U_Z_ToTime"",""U_Z_AppRemarks"""
            objDA.strQuery1 += " from ""@Z_PAY_OLETRANS1"" T0  where ""U_Z_TransType""='P' and  ""U_Z_EmpId"" in (" & objen.EmpCode & ") Order by T0.""Code"" Desc;"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery1, objDA.con)
            objDA.sqlda.Fill(objDA.ds)
            Return objDA.ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function LineMgrResignationApproval(ByVal objen As RequestApprovalEN) As Boolean
        Try
            objDA.strQuery = "Update ""@Z_PAY_OLETRANS1"" set  ""U_Z_Status""='" & objen.Status & "',""U_Z_ApprovedBy""='" & objen.EmpCode & "',""U_Z_ApprDate""=getdate(),""U_Z_AppRemarks""='" & objen.ApproveRemarks & "' where ""Code""='" & objen.Code & "'"
            objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
            objDA.con.Open()
            objDA.cmd.ExecuteNonQuery()
            objDA.con.Close()
            Return True
        Catch ex As Exception
            Throw ex
            Return False
        End Try
        Return True
    End Function
    Public Function LineMgrResignRequestApproval(ByVal objen As RequestApprovalEN) As Boolean
        Try
            Dim strCode As String
            objDA.strQuery = "Update ""@Z_PAY_OLETRANS1"" set  ""U_Z_Status""='" & objen.Status & "',""U_Z_ApprovedBy""='" & objen.EmpCode & "',""U_Z_ApprDate""=getdate(),""U_Z_AppRemarks""='" & objen.ApproveRemarks & "',""U_Z_Month""=" & objen.Month & ",""U_Z_Year""=" & objen.Year & " where ""Code""='" & objen.Code & "'"
            objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
            objDA.con.Open()
            objDA.cmd.ExecuteNonQuery()
            objDA.con.Close()
            If objen.Status = "A" Then
                strCode = objDA.Getmaxcode("""@Z_PAY_OLETRANS""", """Code""")
                objDA.strQuery1 = "Insert into ""@Z_PAY_OLETRANS""(""Code"",""Name"",""U_Z_EMPID"",""U_Z_EMPNAME"",""U_Z_StartDate"",""U_Z_TermRea"",""U_Z_Month"",""U_Z_Year"",""U_Z_IsTerm"") "
                objDA.strQuery1 += " Values ('" & strCode & "','" & strCode & "','" & objen.EmpCode & "','" & objen.EmpName & "','" & objen.dtFromDate.ToString("yyyy/MM/dd") & "','" & objen.Reason & "'," & objen.Month & "," & objen.Year & ",'Y')"
                objDA.cmd = New SqlCommand(objDA.strQuery1, objDA.con)
                objDA.con.Open()
                objDA.cmd.ExecuteNonQuery()
                objDA.con.Close()
            End If
            Return True
        Catch ex As Exception
            Throw ex
            Return False
        End Try
        Return True
    End Function

    Public Function BindSearchPageLoad(ByVal objen As RequestApprovalEN) As DataSet
        Try
            objen.EmpCode = getEmpIDforMangers(objen)
            objDA.strQuery1 = "select case ""U_Z_Status"" when 'A' then 'Approved' when 'P' then 'Pending' when 'R' then 'Rejected' end as ""U_Z_Status"" ,T0.""Code"" as ""Code"",""U_Z_EMPID"",""U_Z_EMPNAME"",Convert(varchar(10),""U_Z_StartDate"",103) AS ""U_Z_StartDate"",""U_Z_AppRemarks"",""U_Z_Year"",DateName( month , DateAdd( month , isnull(""U_Z_Month"",1) , -1 ) ) AS ""U_Z_Month"","
            objDA.strQuery1 += """U_Z_Notes"" from ""@Z_PAY_OLETRANS1"" T0  where ""U_Z_TransType""='T' and  ""U_Z_EmpId"" in (" & objen.EmpCode & ") and " & objen.TransType & " Order by T0.""Code"" Desc;"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery1, objDA.con)
            objDA.sqlda.Fill(objDA.dss2)
            Return objDA.dss2
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function BindSrPageLoad(ByVal objen As RequestApprovalEN) As DataSet
        Try
            objen.EmpCode = getEmpIDforMangers(objen)
            objDA.strQuery1 = "	select ""U_Z_Status"",T0.""Code"" as ""Code"",""U_Z_EMPID"",""U_Z_EMPNAME"",Convert(varchar(10),""U_Z_StartDate"",103) AS ""U_Z_StartDate"",""U_Z_AppRemarks"",""U_Z_Year"",""U_Z_Month"","
            objDA.strQuery1 += """U_Z_Notes"" from ""@Z_PAY_OLETRANS1"" T0  where ""U_Z_TransType""='T' and ""U_Z_Status""='P' and ""U_Z_EMPID"" in ( " & objen.EmpCode & ") and " & objen.TransType & " Order by T0.""Code"" Desc;"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery1, objDA.con)
            objDA.sqlda.Fill(objDA.dss1)
            Return objDA.dss1
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function BindSearchTimePageLoad(ByVal objen As RequestApprovalEN) As DataSet
        Try
            objen.EmpCode = getEmpIDforMangers(objen)
            objDA.strQuery1 = "select case ""U_Z_Status"" when 'A' then 'Approved' when 'P' then 'Pending' when 'R' then 'Rejected' end as ""U_Z_Status"" ,T0.""Code"" as ""Code"",""U_Z_EMPID"",""U_Z_EMPNAME"",CAST(""U_Z_StartDate"" AS varchar(11)) AS ""U_Z_StartDate"",CAST(""U_Z_EndDate"" AS varchar(11)) AS ""U_Z_EndDate"" ,""U_Z_Notes"",""U_Z_FromTime"",""U_Z_ToTime"",""U_Z_AppRemarks"""
            objDA.strQuery1 += " from ""@Z_PAY_OLETRANS1"" T0  where ""U_Z_TransType""='P' and  ""U_Z_EmpId"" in (" & objen.EmpCode & ") and " & objen.TransType & " Order by T0.""Code"" Desc;"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery1, objDA.con)
            objDA.sqlda.Fill(objDA.dss2)
            Return objDA.dss2
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function BindSrTimePageLoad(ByVal objen As RequestApprovalEN) As DataSet
        Try
            objen.EmpCode = getEmpIDforMangers(objen)
            objDA.strQuery1 = "	select ""U_Z_Status"",T0.""Code"" as ""Code"",""U_Z_EMPID"",""U_Z_EMPNAME"",CAST(""U_Z_StartDate"" AS varchar(11)) AS ""U_Z_StartDate"",CAST(""U_Z_EndDate"" AS varchar(11)) AS ""U_Z_EndDate"" ,""U_Z_Notes"",""U_Z_FromTime"",""U_Z_ToTime"",""U_Z_AppRemarks"""
            objDA.strQuery1 += " from ""@Z_PAY_OLETRANS1"" T0  where ""U_Z_TransType""='P' and ""U_Z_Status""='P' and ""U_Z_EMPID"" in ( " & objen.EmpCode & ") and " & objen.TransType & " Order by T0.""Code"" Desc;"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery1, objDA.con)
            objDA.sqlda.Fill(objDA.dss1)
            Return objDA.dss1
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
