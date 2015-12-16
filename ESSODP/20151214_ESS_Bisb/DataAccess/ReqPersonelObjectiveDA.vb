Imports System
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Data.SqlClient
Imports EN
Public Class ReqPersonelObjectiveDA
    Dim objen As ReqPersonelObjectiveEN = New ReqPersonelObjectiveEN()
    Dim objDA As DBConnectionDA = New DBConnectionDA()
    Public Sub New()
        objDA.con = New SqlConnection(objDA.GetConnection)
    End Sub
    Public Function PageLoadBind(ByVal objen As ReqPersonelObjectiveEN) As DataSet
        Try
            objDA.strQuery = "SELECT ""U_Z_PeoobjCode"", ""U_Z_PeoobjName"", ""U_Z_PeoCategory"",CAST(isnull(U_Z_Weight,0) as decimal(10,2)) AS ""U_Z_Weight"" FROM ""@Z_HR_OPEOB"";"
            objDA.strQuery += "SELECT ""U_Z_CatCode"", ""U_Z_CatName"" FROM ""@Z_HR_PECAT"";"
            objDA.strQuery += "SELECT ""U_DocEntry"", ""U_PeoobjCode"", ""U_PeoobjName"", T1.""U_Z_CatName"",CAST(isnull(U_Weight,0) as decimal(10,2)) AS ""U_Weight"", ""U_Remarks"", CASE ""U_Z_AppStatus"" WHEN 'P' THEN 'Pending' WHEN 'A' THEN 'Approved' WHEN 'R' THEN 'Rejected' END AS ""U_Z_AppStatus"", ""U_TypeAction"",isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_CurApprover ),'') AS 'U_CurApprover',isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_NxtApprover ),'') AS 'U_NxtApprover' FROM ""U_PEOPLEOBJ"" T0 INNER JOIN ""@Z_HR_PECAT"" T1 ON T0.""U_PeoCategory"" = T1.""U_Z_CatCode"" WHERE ""U_Empid""='" & objen.EmpId & "' and ""U_TypeAction""='New';"
            objDA.strQuery += "SELECT ""U_DocEntry"", ""U_PeoobjCode"", ""U_PeoobjName"", T1.""U_Z_CatName"",CAST(isnull(U_Weight,0) as decimal(10,2)) AS ""U_Weight"", ""U_Remarks"", CASE ""U_Z_AppStatus"" WHEN 'P' THEN 'Pending' WHEN 'A' THEN 'Approved' WHEN 'R' THEN 'Rejected' END AS ""U_Z_AppStatus"", ""U_TypeAction"",isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_CurApprover ),'') AS 'U_CurApprover',isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_NxtApprover ),'') AS 'U_NxtApprover' FROM ""U_PEOPLEOBJ"" T0 INNER JOIN ""@Z_HR_PECAT"" T1 ON T0.""U_PeoCategory"" = T1.""U_Z_CatCode"" WHERE ""U_Empid""='" & objen.EmpId & "' and ""U_TypeAction""='Deleted';"
            objDA.strQuery += "SELECT ""empID"", ""firstName"", ""lastName"", ""middleName"", ""U_Z_HR_ThirdName"", T0.""position"", T1.""descriptio"" AS ""Positionname"", ""dept"", T2.""Remarks"" AS ""Deptname"", ""U_Z_HR_JobstCode"", T3.""Name"" AS ""BranchName"", ""officeExt"", ""U_Z_Rel_Name"", ""U_Z_Rel_Type"", ""U_Z_Rel_Phone"", ""officeTel"", ""mobile"", ""email"", ""fax"", ""homeTel"", ""pager"", CASE ""sex"" WHEN 'M' THEN 'Male' ELSE 'Female' END AS ""sex"", CAST(""birthDate"" AS varchar(11)) AS ""birthDate"", T4.""Name"" AS ""brthCountr"", CASE ""martStatus"" WHEN 'S' THEN 'Single' WHEN 'M' THEN 'Married' WHEN 'D' THEN 'Divorced' WHEN 'W' THEN 'Widowed' END AS ""martStatus"", ""nChildren"", ""govID"", T4.""Name"" AS ""citizenshp"", CAST(""passportEx"" AS varchar(11)) AS ""passportEx"", ""passportNo"", ""workBlock"", ""workCity"", T4.""Name"" AS ""workCountry"", ""workCountr"", ""workState"", ""workCounty"", ""workStreet"", ""workZip"", ""homeBlock"", ""homeCity"", T4.""Name"" AS ""homeCountry"", ""homeCountr"", ""homeCounty"", ""homeState"", ""homeStreet"", ""homeZip"", ""U_Z_HR_OrgstCode"", ""U_Z_HR_OrgstName"", ""WorkBuild"", ""HomeBuild"", ""U_Z_LvlCode"", ""U_Z_LvlName"", ""U_Z_LocCode"", ""U_Z_LocName"", ""U_Z_HR_JobstCode"", ""U_Z_HR_JobstName"", ""U_Z_HR_SalaryCode"", ""U_Z_HR_ApplId"", ISNULL(""manager"", 0) AS ""Manager"" FROM OHEM T0 LEFT OUTER JOIN OHPS T1 ON T0.""position"" = T1.""posID"" LEFT OUTER JOIN OUDP T2 ON T0.""dept"" = T2.""Code"" LEFT OUTER JOIN OUBR T3 ON T0.""branch"" = T3.""Code"" LEFT OUTER JOIN OCRY T4 ON T0.""workCountr"" = T4.""Code"" where ""empID""='" & objen.EmpId & "';"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds)
            Return objDA.ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Manager(ByVal objen As ReqPersonelObjectiveEN) As DataSet
        Try
            objDA.strQuery = "SELECT ""firstName"" + '' + ""lastName"" AS ""ManName"" FROM OHEM where ""empID""='" & objen.Manager & "'"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.dss)
            Return objDA.dss
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function DeleteObjective(ByVal objen As ReqPersonelObjectiveEN) As Boolean
        Try
            objDA.strQuery = "Delete from ""U_PEOPLEOBJ"" where ""U_DocEntry""='" & objen.DocEntry & "' and ""U_Empid""='" & objen.EmpId & "'"
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
    Public Function SelectPeoObjCode(ByVal objen As ReqPersonelObjectiveEN) As DataSet
        Try
            objDA.strQuery = "select ""U_Z_PeoobjCode"",""U_Z_PeoobjName"",""U_Z_PeoCategory"",CAST(isnull(U_Z_Weight,0) as decimal(10,2)) AS ""U_Z_Weight""  from ""@Z_HR_OPEOB"" where ""U_Z_PeoobjCode""='" & objen.PeoObjCode & "'"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds1)
            Return objDA.ds1
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function InsertObjective(ByVal objen As ReqPersonelObjectiveEN) As Boolean
        Try
            objDA.strQuery = "Insert into ""U_PEOPLEOBJ""(""U_Empid"",""U_EmpName"",""U_DeptCode"",""U_PeoCatDesc"",""U_PeoobjCode"",""U_PeoobjName"",""U_PeoCategory"",""U_Weight"",""U_Remarks"",""U_Z_AppStatus"",""U_TypeAction"") Values "
            objDA.strQuery += "('" & objen.EmpId & "','" & objen.EmpName & "','" & objen.DeptCode & "','" & objen.PeoObjCatDesc & "','" & objen.PeoObjCode & "','" & objen.PeoObjName & "','" & objen.PeoObjCat & "','" & objen.Weight & "',"
            objDA.strQuery += " '" & objen.Remarks & "','" & objen.AppStatus & "','New')"
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
