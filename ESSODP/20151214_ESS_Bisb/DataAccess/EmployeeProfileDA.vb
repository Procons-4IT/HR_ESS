Imports System
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Data.SqlClient
Imports EN
Public Class EmployeeProfileDA
    Dim objen As EmployeeProfileEN = New EmployeeProfileEN()
    Dim objDA As DBConnectionDA = New DBConnectionDA()
    Public Sub New()
        objDA.con = New SqlConnection(objDA.GetConnection)
    End Sub
    Public Function getNodays(ByVal objen As EmployeeProfileEN) As String
        Try
            objDA.strQuery = "select datediff(D,'" & objen.FromDate.ToString("yyyy/MM/dd") & "','" & objen.ToDate.ToString("yyyy/MM/dd") & "')"
            objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
            objDA.con.Open()
            objDA.sqlda = New SqlDataAdapter(objDA.cmd)
            objDA.dt.Clear()
            objDA.sqlda.Fill(objDA.dt)
            If objDA.dt.Rows.Count > 0 Then
                objen.DocEntry = objDA.dt.Rows(0)(0).ToString() + 1
            End If
            objDA.con.Close()
            Return objen.DocEntry
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function BindEduDetails(ByVal objen As EmployeeProfileEN) As DataSet
        Try
            'objDA.strQuery = "SELECT ""empID"",""line"", convert(varchar(10),""fromDate"",103) AS ""fromDate"",  convert(varchar(10),""toDate"",103) AS ""toDate"", ""type"", ""institute"", ""major"", ""diploma"" FROM HEM2  where ""empID""='" & objen.EmpId & "'"
            objDA.strQuery = "SELECT  convert(varchar(10),""fromDate"",103) AS ""fromDate"",  convert(varchar(10),""toDate"",103) AS ""toDate"", T1.""name"", ""institute"", ""major"", ""diploma"" FROM HEM2 T0 INNER JOIN OHED T1 ON t1.""edType"" = T0.""type"" where ""empID""='" & objen.EmpId & "'"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds4)
            Return objDA.ds4
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function BindPersonelDetails(ByVal objen As EmployeeProfileEN) As DataSet
        Try
            objDA.strQuery = "SELECT isnull(U_Z_EmpID,'') as 'TAEmpID', ""bankCode"",""bankAcount"",""bankBranch"",""empID"", ""firstName"", ""lastName"", ""middleName"", ""U_Z_HR_ThirdName"",""govID"",""U_Z_IBAN"", T0.""position"", T1.""descriptio"" AS ""Positionname"", ""dept"", T2.""Remarks"" AS ""Deptname"", ""U_Z_HR_JobstCode"", T3.""Name"" AS ""BranchName"", ""officeExt"", ""U_Z_Rel_Name"", ""U_Z_Rel_Type"", ""U_Z_Rel_Phone"", ""officeTel"", ""mobile"", ""email"", ""fax"", ""homeTel"", ""pager"",""sex"", convert(varchar(10),""birthDate"",103) AS ""birthDate"", ""brthCountr"", ""martStatus"", ""nChildren"", ""govID"", ""citizenshp"", convert(varchar(10),""passportEx"",103) AS ""passportEx"", ""passportNo"", ""workBlock"", ""workCity"", ""workCountr"", ""workState"", ""workCounty"", ""workStreet"", ""workZip"", ""homeBlock"", ""homeCity"", ""homeCountr"", ""homeCounty"", ""homeState"", ""homeStreet"", ""homeZip"", ""U_Z_HR_OrgstCode"", ""U_Z_HR_OrgstName"", ""WorkBuild"", ""HomeBuild"", ""U_Z_LvlCode"", ""U_Z_LvlName"", ""U_Z_LocCode"", ""U_Z_LocName"", ""U_Z_HR_JobstCode"", ""U_Z_HR_JobstName"", ""U_Z_HR_SalaryCode"", ""U_Z_HR_ApplId"", ISNULL(""manager"", 0) AS ""Manager"" FROM OHEM T0 LEFT OUTER JOIN OHPS T1 ON T0.""position"" = T1.""posID"" LEFT OUTER JOIN OUDP T2 ON T0.""dept"" = T2.""Code"" LEFT OUTER JOIN OUBR T3 ON T0.""branch"" = T3.""Code"" where ""empID""='" & objen.EmpId & "'"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.dss4)
            Return objDA.dss4
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function HomeState(ByVal objen As EmployeeProfileEN) As DataSet
        Try
            objDA.strQuery = "select ""Code"",""Name""  from OCST where ""Country""='" & objen.HomeCountry & "' order by ""Code"""
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds1)
            Return objDA.ds1
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function BindCountry(ByVal objen As EmployeeProfileEN) As DataSet
        Try
            objDA.strQuery = "SELECT ""Code"", ""Name"" FROM OCRY"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.dss)
            Return objDA.dss
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function BindEdutype(ByVal objen As EmployeeProfileEN) As DataSet
        Try
            objDA.strQuery = "SELECT ""edType"", ""name"" FROM OHED"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.dss3)
            Return objDA.dss3
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function WorkState(ByVal objen As EmployeeProfileEN) As DataSet
        Try
            objDA.strQuery = "select ""Code"",""Name""  from OCST where ""Country""='" & objen.WorkCountry & "' order by ""Code"""
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds2)
            Return objDA.ds2
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Manager(ByVal objen As EmployeeProfileEN) As DataSet
        Try
            objDA.strQuery = "SELECT isnull(""firstName"",'') +  ' ' + isnull(""middleName"",'') +  ' ' + isnull(""lastName"",'') AS ""ManName"" FROM OHEM where ""empID""='" & objen.Manager & "'"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds3)
            Return objDA.ds3
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function HomeBank(ByVal objen As EmployeeProfileEN) As DataSet
        Try
            objDA.strQuery = "select ""BankCode"",""BankName""  from ODSC where ""CountryCod""='" & objen.HomeCountry & "'"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.dss2)
            Return objDA.dss2
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
