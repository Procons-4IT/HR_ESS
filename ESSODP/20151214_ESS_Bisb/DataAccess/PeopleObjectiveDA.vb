Imports System
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Data.SqlClient
Imports EN
Public Class PeopleObjectiveDA
    Dim objen As ReqPersonelObjectiveEN = New ReqPersonelObjectiveEN()
    Dim objDA As DBConnectionDA = New DBConnectionDA()
    Public Sub New()
        objDA.con = New SqlConnection(objDA.GetConnection)
    End Sub
    Public Function PageLoadBind(ByVal objen As ReqPersonelObjectiveEN) As DataSet
        Try
            objDA.strQuery = "SELECT T0.""Code"", ""U_Z_HRPeoobjCode"", ""U_Z_HRPeoobjName"", T1.""U_Z_CatName"", T1.""U_Z_CatCode"",CAST(isnull(U_Z_HRWeight,0) as decimal(10,2)) AS ""U_Z_HRWeight"", ""U_Z_Remarks"" FROM ""@Z_HR_PEOBJ1"" T0 LEFT OUTER JOIN ""@Z_HR_PECAT"" T1 ON T0.""U_Z_HRPeoCategory"" = T1.""U_Z_CatCode"" WHERE ""U_Z_HREmpID"" ='" & objen.EmpId & "' and T0.""Code""=T0.""Name"";"
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
            objDA.strQuery = "Update ""@Z_HR_PEOBJ1"" set ""Name""='" & objen.DocEntry & "' +'DX' where ""Code""='" & objen.DocEntry & "';"
            objDA.strQuery += "Insert into ""U_PEOPLEOBJ""(""U_Empid"",""U_PeoobjCode"",""U_PeoobjName"",""U_PeoCategory"",""U_Weight"",""U_Remarks"",""U_Status"",""U_TypeAction"",""U_RefNo"") Values "
            objDA.strQuery += "('" & objen.EmpId & "','" & objen.PeoObjCode & "','" & objen.PeoObjName & "','" & objen.PeoObjCat & "','" & objen.Weight & "',"
            objDA.strQuery += " '" & objen.Remarks & "','P','Deleted','" & objen.DocEntry & "');"
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
