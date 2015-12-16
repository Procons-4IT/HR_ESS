Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Odbc
Imports DataAccess
Imports EN
Public Class SeniorMgrAppraisalDA
    Dim objEN As SeniorMgrAppraisalEN = New SeniorMgrAppraisalEN()
    Dim objDA As DBConnectionDA = New DBConnectionDA()
    Public Sub New()
        objDA.con = New SqlConnection(objDA.GetConnection)
    End Sub
    Public Function PageLoadBind(ByVal objen As SeniorMgrAppraisalEN) As DataSet
        Try
            objDA.strQuery = "SELECT U_Z_PerCode AS 'Code',U_Z_PerDesc AS 'Name' FROM [@Z_HR_PERAPP] order by Code Desc;"
            objDA.strQuery += " select DocEntry,T0.U_Z_EmpId,U_Z_EmpName,Convert(Varchar(10),U_Z_Date,103) AS U_Z_Date,T0.U_Z_Period,T2.U_Z_PerDesc,T2.""U_Z_PerFrom"","
            objDA.strQuery += " T2.""U_Z_PerTo"",case U_Z_Status when 'D' then 'Draft' when 'F' then 'Approved' when 'S'then '2nd Level Approval' when 'L'"
            objDA.strQuery += " then 'Closed' else 'Canceled' end as U_Z_Status,case U_Z_WStatus when 'DR' then 'Draft' when 'HR' then 'HR Approved' when 'SM' then "
            objDA.strQuery += " 'Sr.Manager Approved' when 'LM' then 'LineManager Approved' when 'SE' then 'SelfApproved'  end as 'U_Z_WStatus'   "
            ' objDA.strQuery += " from [@Z_HR_OSEAPP] T0 JOIN OHEM T1 On T0.U_Z_EmpID = T1.empID and (isnull(T1.U_Z_SecondApp,'N')='Y' AND isnull(T0.U_Z_SecondApp,'N')='Y') Left Outer Join ""@Z_HR_PERAPP"" T2 on T0.U_Z_Period=T2.U_Z_PerCode  AND T1.""manager"" "
            objDA.strQuery += " from [@Z_HR_OSEAPP] T0  Join OHEM T1 On T0.U_Z_EmpID = T1.empID and (isnull(T1.U_Z_SecondApp,'')='Y' AND isnull(T0.U_Z_SecondApp,'')='Y') Join [@Z_HR_PERAPP] T2 on T0.U_Z_Period=T2.U_Z_PerCode  AND T1.""empID"" "
            objDA.strQuery += " IN (SELECT ""empID"" FROM OHEM WHERE ""Manager"" IN (SELECT ""empID"" FROM OHEM WHERE ""manager"" = " & objen.EmpId & ")) ORDER BY ""DocEntry"" DESC;"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds)
            Return objDA.ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub UpdatesrMgrAppHeader(ByVal objen As SeniorMgrAppraisalEN)
        Try
            objen.StrQry = "Update ""@Z_HR_OSEAPP"" set ""U_Z_Status""='S', ""U_Z_SrCkApp""='" & objen.CheckStatus & "', ""U_Z_WStatus""='" & objen.Status & "',""U_Z_BSMrRemark""='" & objen.BusinessRemarks & "' ,  ""U_Z_PSMrRemark""='" & objen.PeopleRemarks & "' ,  ""U_Z_CSMrRemark""='" & objen.CompRemarks & "' where ""DocEntry""='" & objen.AppraisalNumber & "'"
            objDA.cmd = New SqlCommand(objen.StrQry, objDA.con)
            objDA.con.Open()
            objDA.cmd.ExecuteNonQuery()
            objDA.con.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub UpdatesrMgrAppBusiness(ByVal objen As SeniorMgrAppraisalEN)
        objen.StrQry = "Update ""@Z_HR_SEAPP1"" set ""U_Z_SMRaCode""='" & objen.SelfRating & "',""U_Z_BussSMRate""=" & objen.Amount & ",U_Z_SrRemark='" & objen.SecondRemarks & "' where ""DocEntry""='" & objen.AppraisalNumber & "' and ""LineId""=" & objen.LineNo & ""
        objDA.cmd = New SqlCommand(objen.StrQry, objDA.con)
        objDA.con.Open()
        objDA.cmd.ExecuteNonQuery()
        objDA.con.Close()
    End Sub
    Public Sub UpdatesrMgrAppPeople(ByVal objen As SeniorMgrAppraisalEN)
        objen.StrQry = "Update ""@Z_HR_SEAPP2"" set ""U_Z_SMRaCode""='" & objen.SelfRating & "', ""U_Z_PeoSMRate""=" & objen.Amount & ",U_Z_SrRemark='" & objen.SecondRemarks & "' where ""DocEntry""='" & objen.AppraisalNumber & "' and ""LineId""=" & objen.LineNo & ""
        objDA.cmd = New SqlCommand(objen.StrQry, objDA.con)
        objDA.con.Open()
        objDA.cmd.ExecuteNonQuery()
        objDA.con.Close()
    End Sub
    Public Sub UpdatesrMgrAppCompetence(ByVal objen As SeniorMgrAppraisalEN)
        objen.StrQry = "Update ""@Z_HR_SEAPP3"" set ""U_Z_SMRaCode""='" & objen.SelfRating & "',""U_Z_CompSMRate""=" & objen.Amount & ",U_Z_SrRemark='" & objen.SecondRemarks & "' where ""DocEntry""='" & objen.AppraisalNumber & "' and ""LineId""=" & objen.LineNo & ""
        objDA.cmd = New SqlCommand(objen.StrQry, objDA.con)
        objDA.con.Open()
        objDA.cmd.ExecuteNonQuery()
        objDA.con.Close()
    End Sub
    Public Function ObjectiveBind(ByVal objen As SeniorMgrAppraisalEN) As DataSet
        'objen.StrQry = "SELECT ""U_Z_BussCode"", ""U_Z_BussDesc"", ""U_Z_BussMgrRate"", ""U_Z_BussSelfRate"", ""U_Z_BussWeight"", ""LineId"", ""U_Z_BussSMRate"", ""U_Z_SelfRaCode"", ""U_Z_SMRaCode"", ""U_Z_MgrRaCode"" FROM ""@Z_HR_SEAPP1"" WHERE ""DocEntry""='" & objen.AppraisalNumber & "';"
        'objen.StrQry += "SELECT ""U_Z_PeopleCode"", ""U_Z_PeopleDesc"", ""U_Z_PeopleCat"", ""U_Z_PeoWeight"", ""U_Z_PeoSelfRate"", ""U_Z_PeoMgrRate"", ""LineId"", ""U_Z_PeoSMRate"", ""U_Z_SelfRaCode"", ""U_Z_SMRaCode"", ""U_Z_MgrRaCode"" FROM ""@Z_HR_SEAPP2"" WHERE ""DocEntry""='" & objen.AppraisalNumber & "';"
        'objen.StrQry += "SELECT T0.""U_Z_CompCode"", T0.""U_Z_CompDesc"", T0.""U_Z_CompMgrRate"", T0.""U_Z_CompSelfRate"", T0.""U_Z_CompWeight"", ISNULL(T0.""U_Z_CompLevel"", '0') AS ""U_Z_CompLevel"", T2.""U_Z_CompLevel"" AS ""CurrentLevel"", T0.""U_Z_CompSMRate"", T0.""LineId"", T0.""U_Z_SelfRaCode"", T0.""U_Z_SMRaCode"", T0.""U_Z_MgrRaCode"" FROM ""@Z_HR_SEAPP3"" T0 INNER JOIN ""@Z_HR_OSEAPP"" T1 ON T1.""DocEntry"" = T0.""DocEntry"" LEFT OUTER JOIN ""@Z_HR_ECOLVL"" T2 ON T1.""U_Z_EmpId"" = T2.""U_Z_HREmpID"" AND T2.""U_Z_CompCode"" = T0.""U_Z_CompCode"" WHERE T0.""DocEntry""='" & objen.AppraisalNumber & "'"
        objen.StrQry = "SELECT ""U_Z_BussCode"", ""U_Z_BussDesc"", ""U_Z_BussMgrRate"", ""U_Z_BussSelfRate"", ""U_Z_BussWeight"", ""LineId"", ""U_Z_BussSMRate"", ""U_Z_SelfRaCode"", ""U_Z_SMRaCode"", ""U_Z_MgrRaCode"",""U_Z_SelfRemark"",""U_Z_MgrRemark"",""U_Z_SrRemark"" FROM ""@Z_HR_SEAPP1"" WHERE ""DocEntry""='" & objen.AppraisalNumber & "';"
        objen.StrQry += "SELECT ""U_Z_PeopleCode"", ""U_Z_PeopleDesc"", ""U_Z_PeopleCat"", ""U_Z_PeoWeight"", ""U_Z_PeoSelfRate"", ""U_Z_PeoMgrRate"", ""LineId"", ""U_Z_PeoSMRate"", ""U_Z_SelfRaCode"", ""U_Z_SMRaCode"", ""U_Z_MgrRaCode"",""U_Z_SelfRemark"",""U_Z_MgrRemark"",""U_Z_SrRemark"" FROM ""@Z_HR_SEAPP2"" WHERE ""DocEntry""='" & objen.AppraisalNumber & "';"
        objen.StrQry += "SELECT T0.""U_Z_CompCode"", T0.""U_Z_CompDesc"", T0.""U_Z_CompMgrRate"", T0.""U_Z_CompSelfRate"", T0.""U_Z_CompWeight"", ISNULL(T0.""U_Z_CompLevel"", '0') AS ""U_Z_CompLevel"", T2.""U_Z_CompLevel"" AS ""CurrentLevel"", T0.""U_Z_CompSMRate"", T0.""LineId"", T0.""U_Z_SelfRaCode"", T0.""U_Z_SMRaCode"", T0.""U_Z_MgrRaCode"",""U_Z_SelfRemark"",""U_Z_MgrRemark"",""U_Z_SrRemark"" FROM ""@Z_HR_SEAPP3"" T0 INNER JOIN ""@Z_HR_OSEAPP"" T1 ON T1.""DocEntry"" = T0.""DocEntry"" LEFT OUTER JOIN ""@Z_HR_ECOLVL"" T2 ON T1.""U_Z_EmpId"" = T2.""U_Z_HREmpID"" AND T2.""U_Z_CompCode"" = T0.""U_Z_CompCode"" WHERE T0.""DocEntry""='" & objen.AppraisalNumber & "'"
        objDA.sqlda = New SqlDataAdapter(objen.StrQry, objDA.con)
        objDA.sqlda.Fill(objDA.ds)
        Return objDA.ds
    End Function
    Public Function PopulateEmployee(ByVal objen As SeniorMgrAppraisalEN) As DataSet
        ' objen.StrQry = "SELECT ""DocEntry"", ""U_Z_EmpId"", ""U_Z_EmpName"", CAST(""U_Z_Date"" AS varchar(11)) AS ""U_Z_Date"", ""U_Z_Period"", ""U_Z_BSelfRemark"", ""U_Z_PSelfRemark"", ""U_Z_CSelfRemark"", ""U_Z_GStatus"", ""U_Z_GRemarks"", CAST(""U_Z_GDate"" AS varchar(11)) AS ""U_Z_GDate"", ""U_Z_GNo"", ""U_Z_CMgrRemark"", ""U_Z_PMgrRemark"", ""U_Z_BMgrRemark"", ""U_Z_CSMrRemark"", ""U_Z_PSMrRemark"", ""U_Z_BSMrRemark"", CASE ""U_Z_Status"" WHEN 'D' THEN 'Draft' WHEN 'F' THEN 'Approved' WHEN 'S' THEN '2nd Level Approval' WHEN 'L' THEN 'Closed' ELSE 'Canceled' END AS ""U_Z_Status"", CASE ""U_Z_WStatus"" WHEN 'DR' THEN 'Draft' WHEN 'HR' THEN 'HR Approved' WHEN 'SM' THEN 'Sr.Manager Approved' WHEN 'LM' THEN 'LineManager Approved' WHEN 'SE' THEN 'SelfApproved' END AS ""U_Z_WStatus"", ""U_Z_BHrRemark"", ""U_Z_PHrRemark"", ""U_Z_CHrRemark"", ""U_Z_GStatus"", ""U_Z_GRemarks"", CAST(""U_Z_GDate"" AS varchar(11)) AS ""U_Z_GDate"", ""U_Z_GNo"" FROM ""@Z_HR_OSEAPP"" where ""U_Z_EmpId""='" & objen.EmpId & "' and ""DocEntry""='" & objen.AppraisalNumber & "'"
        objen.StrQry = "SELECT ""DocEntry"", ""U_Z_EmpId"", ""U_Z_EmpName"", Convert(Varchar(10),U_Z_Date,103) AS ""U_Z_Date"", ""U_Z_Period"",T1.U_Z_PerDesc,T1.U_Z_PerFrom, "
        objen.StrQry += " T1.U_Z_PerTo,""U_Z_BSelfRemark"", ""U_Z_PSelfRemark"", ""U_Z_CSelfRemark"", ""U_Z_CMgrRemark"", ""U_Z_PMgrRemark"", ""U_Z_BMgrRemark"","
        objen.StrQry += " ""U_Z_CSMrRemark"", ""U_Z_PSMrRemark"", ""U_Z_BSMrRemark"", CASE ""U_Z_Status"" WHEN 'D' THEN 'Draft' WHEN 'F' THEN 'Approved' "
        objen.StrQry += " WHEN 'S' THEN '2nd Level Approval' WHEN 'L' THEN 'Closed' ELSE 'Canceled' END AS ""U_Z_Status"", CASE ""U_Z_WStatus"" "
        objen.StrQry += " WHEN 'DR' THEN 'Draft' WHEN 'HR' THEN 'HR Approved' WHEN 'SM' THEN 'Sr.Manager Approved' WHEN 'LM' THEN 'LineManager Approved' "
        objen.StrQry += " WHEN 'SE' THEN 'SelfApproved' END AS ""U_Z_WStatus"", ""U_Z_BHrRemark"", ""U_Z_PHrRemark"", ""U_Z_CHrRemark"", ""U_Z_GStatus"", "
        objen.StrQry += " ""U_Z_GRemarks"", CAST(""U_Z_GDate"" AS varchar(11)) AS ""U_Z_GDate"", ""U_Z_GNo"" FROM ""@Z_HR_OSEAPP"" T0  Join ""@Z_HR_PERAPP"" T1 on T0.U_Z_Period=T1.U_Z_PerCode  "
        objen.StrQry += " where ""U_Z_EmpId""='" & objen.EmpId & "' and ""DocEntry""='" & objen.AppraisalNumber & "'"
        objDA.sqlda = New SqlDataAdapter(objen.StrQry, objDA.con)
        objDA.sqlda.Fill(objDA.ds1)
        Return objDA.ds1
    End Function
    Public Function SelectionChange(ByVal objen As SeniorMgrAppraisalEN) As DataSet
        objen.StrQry = "Select * from ""@Z_HR_ORATE"" where ""U_Z_RateCode""='" & objen.Ratings & "'"
        objDA.sqlda = New SqlDataAdapter(objen.StrQry, objDA.con)
        objDA.sqlda.Fill(objDA.ds2)
        Return objDA.ds2
    End Function
    Public Function BindSearchPageLoad(ByVal objen As SeniorMgrAppraisalEN) As DataSet
        objen.strConiditon = "" & objen.SearchCondition & "  Order by ""DocEntry"" Desc"
        ' objDA.strQuery = "SELECT ""DocEntry"", T0.""U_Z_EmpId"", ""U_Z_EmpName"", CAST(""U_Z_Date"" AS varchar(11)) AS ""U_Z_Date"", T0.""U_Z_Period"", CASE ""U_Z_Status"" WHEN 'D' THEN 'Draft' WHEN 'F' THEN 'Approved' WHEN 'S' THEN '2nd Level Approval' WHEN 'L' THEN 'Closed' ELSE 'Canceled' END AS ""U_Z_Status"", CASE ""U_Z_WStatus"" WHEN 'DR' THEN 'Draft' WHEN 'HR' THEN 'HR Approved' WHEN 'SM' THEN 'Sr.Manager Approved' WHEN 'LM' THEN 'LineManager Approved' WHEN 'SE' THEN 'SelfApproved' END AS ""U_Z_WStatus"" FROM ""@Z_HR_OSEAPP"" T0 INNER JOIN OHEM T1 ON T0.""U_Z_EmpId"" = T1.""empID"" where T1.""manager"" IN (SELECT ""empID"" FROM OHEM WHERE ""empID"" = 1 UNION SELECT ""empID"" FROM OHEM WHERE ""manager"" IN (SELECT ""empID"" FROM OHEM WHERE ""empID"" = " & objen.EmpId & ")) and " & objen.strConiditon
        objDA.strQuery = " select DocEntry,T0.U_Z_EmpId,U_Z_EmpName,Convert(Varchar(10),U_Z_Date,103) AS U_Z_Date,T0.U_Z_Period,T2.U_Z_PerDesc,T2.""U_Z_PerFrom"","
        objDA.strQuery += " T2.""U_Z_PerTo"",case U_Z_Status when 'D' then 'Draft' when 'F' then 'Approved' when 'S'then '2nd Level Approval' when 'L'"
        objDA.strQuery += " then 'Closed' else 'Canceled' end as U_Z_Status,case U_Z_WStatus when 'DR' then 'Draft' when 'HR' then 'HR Approved' when 'SM' then "
        objDA.strQuery += " 'Sr.Manager Approved' when 'LM' then 'LineManager Approved'when 'SE' then 'SelfApproved'  end as 'U_Z_WStatus'   "
        ' objDA.strQuery += " from [@Z_HR_OSEAPP] T0 JOIN OHEM T1 On T0.U_Z_EmpID = T1.empID and (isnull(T1.U_Z_SecondApp,'N')='Y' AND isnull(T0.U_Z_SecondApp,'N')='Y') Left Outer Join ""@Z_HR_PERAPP"" T2 on T0.U_Z_Period=T2.U_Z_PerCode  AND T1.""manager"" "
        objDA.strQuery += " from [@Z_HR_OSEAPP] T0  Join OHEM T1 On T0.U_Z_EmpID = T1.empID and (isnull(T1.U_Z_SecondApp,'')='Y' AND isnull(T0.U_Z_SecondApp,'')='Y') Join [@Z_HR_PERAPP] T2 on T0.U_Z_Period=T2.U_Z_PerCode  AND T1.""empID"" "
        objDA.strQuery += " IN (SELECT ""empID"" FROM OHEM WHERE ""Manager"" IN (SELECT ""empID"" FROM OHEM WHERE ""manager"" = " & objen.EmpId & ")) and " & objen.strConiditon
        objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
        objDA.sqlda.Fill(objDA.dss1)
        Return objDA.dss1
    End Function
   
End Class
