Imports System
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Data.SqlClient
Imports EN
Public Class LineMgrAppraisalDA
    Dim objen As LineMgrAppraisalEN = New LineMgrAppraisalEN()
    Dim objDA As DBConnectionDA = New DBConnectionDA()
    Dim oDtAppraisal As DataTable
    Public Sub New()
        objDA.con = New SqlConnection(objDA.GetConnection)
    End Sub
    Public Function BindPageLoad(ByVal objen As LineMgrAppraisalEN) As DataSet
        objen.EmpId = getEmpIDforMangers(objen)
        objDA.strQuery = "SELECT U_Z_PerCode AS 'Code',U_Z_PerDesc AS 'Name' FROM [@Z_HR_PERAPP] order by Code Desc;"
        If objen.EmpId <> "" Then
            objen.strConiditon = """U_Z_EmpId"" in (" & objen.EmpId & ") Order by ""DocEntry"" Desc"
            objDA.strQuery += " select DocEntry,U_Z_EmpId,U_Z_EmpName,Convert(Varchar(10),U_Z_Date,103) AS U_Z_Date,U_Z_Period,T1.U_Z_PerDesc,T1.U_Z_PerFrom,"
            objDA.strQuery += " T1.U_Z_PerTo,case U_Z_Status when 'D' then 'Draft' when 'F' then 'Approved' "
            objDA.strQuery += " when 'S'then '2nd Level Approval' when 'L' then 'Closed' else 'Canceled' end as U_Z_Status,"
            objDA.strQuery += " case U_Z_WStatus when 'DR' then 'Draft' when 'HR' then 'HR Approved' when 'SM'then 'Sr.Manager Approved' "
            objDA.strQuery += " when 'LM' then 'LineManager Approved'when 'SE' then 'SelfApproved'  end as 'U_Z_WStatus'   "
            objDA.strQuery += " from [@Z_HR_OSEAPP] T0 Left Outer Join ""@Z_HR_PERAPP"" T1 on T0.U_Z_Period=T1.U_Z_PerCode  Where " & objen.strConiditon
        End If
        objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
        objDA.sqlda.Fill(objDA.ds1)
        Return objDA.ds1
    End Function
    Public Function BindPageLoadTeamList(ByVal objen As LineMgrAppraisalEN) As DataSet
        objen.EmpId = getEmpIDforMangers(objen)
        If objen.EmpId <> "" Then
            objen.strConiditon = """empID"" in (" & objen.EmpId & ") Order by ""empID"" Desc"
            objen.StrQry = "SELECT ""empID"", ""firstName"" + ' ' + ""lastName"" AS ""EmpName"", ""email"", ""homeTel"", T1.""descriptio"" AS ""Positionname"", ""dept"", T2.""Name"" AS ""Deptname"" FROM OHEM T0 LEFT JOIN OHPS T1 ON T0.""position"" = T1.""posID"" LEFT JOIN OUDP T2 ON T0.""dept"" = T2.""Code"" Where " & objen.strConiditon
        End If
        objDA.sqlda = New SqlDataAdapter(objen.StrQry, objDA.con)
        objDA.sqlda.Fill(objDA.ds1)
        Return objDA.ds1
    End Function
    Public Function getEmpIDforMangers(ByVal objen As LineMgrAppraisalEN) As String
        Dim strEmp As String = ""
        objen.StrQry = "Select ""empID"" from OHEM where ""manager""=" & objen.EmpId & ""
        objDA.sqlda = New SqlDataAdapter(objen.StrQry, objDA.con)
        objDA.sqlda.Fill(objDA.dss)
        If objDA.dss.Tables(0).Rows.Count > 0 Then
            For intRow As Integer = 0 To objDA.dss.Tables(0).Rows.Count - 1
                If strEmp = "" Then
                    strEmp = "'" & objDA.dss.Tables(0).Rows(intRow)("empID").ToString() & "'"
                Else
                    strEmp = strEmp & " ,'" & objDA.dss.Tables(0).Rows(intRow)("empID").ToString() & "'"
                End If
            Next
        Else
            strEmp = "99999"
        End If
        Return strEmp
    End Function
    Public Sub UpdateLineMgrAppHeader(ByVal objen As LineMgrAppraisalEN)
        objen.StrQry = "Update ""@Z_HR_OSEAPP"" set ""U_Z_LCkApp""='" & objen.CheckStatus & "',""U_Z_WStatus""='" & objen.Status & "',""U_Z_BMgrRemark""='" & objen.BusinessRemarks & "',""U_Z_SrCkApp""='" & objen.SecondStatus & "',""U_Z_PMgrRemark""='" & objen.PeopleRemarks & "' , ""U_Z_CMgrRemark""='" & objen.CompRemarks & "',""U_Z_Status""='S',""U_Z_SecondApp""='" & objen.SecondLvlApp & "' where ""DocEntry""='" & objen.AppraisalNumber & "'"
        objDA.cmd = New SqlCommand(objen.StrQry, objDA.con)
        objDA.con.Open()
        objDA.cmd.ExecuteNonQuery()
        objDA.con.Close()
    End Sub
    Public Sub UpdateLineMgrAppBusiness(ByVal objen As LineMgrAppraisalEN)
        objen.StrQry = "Update ""@Z_HR_SEAPP1"" set ""U_Z_MgrRaCode""='" & objen.SelfRating & "',""U_Z_BussMgrRate""=" & objen.Amount & ",""U_Z_MgrRemark""='" & objen.BLineRemarks & "' where ""DocEntry""='" & objen.AppraisalNumber & "' and ""LineId""=" & objen.LineNo & ""
        objDA.cmd = New SqlCommand(objen.StrQry, objDA.con)
        objDA.con.Open()
        objDA.cmd.ExecuteNonQuery()
        objDA.con.Close()
    End Sub
    Public Sub UpdateLineMgrAppPeople(ByVal objen As LineMgrAppraisalEN)
        objen.StrQry = "Update ""@Z_HR_SEAPP2"" set ""U_Z_MgrRaCode""='" & objen.SelfRating & "', ""U_Z_PeoMgrRate""=" & objen.Amount & ",""U_Z_MgrRemark""='" & objen.PLineRemarks & "' where ""DocEntry""='" & objen.AppraisalNumber & "' and ""LineId""=" & objen.LineNo & ""
        objDA.cmd = New SqlCommand(objen.StrQry, objDA.con)
        objDA.con.Open()
        objDA.cmd.ExecuteNonQuery()
        objDA.con.Close()
    End Sub
    Public Sub UpdateLineMgrAppCompetence(ByVal objen As LineMgrAppraisalEN)
        objen.StrQry = "Update ""@Z_HR_SEAPP3"" set ""U_Z_MgrRaCode""='" & objen.SelfRating & "',""U_Z_CompMgrRate""=" & objen.Amount & ",""U_Z_MgrRemark""='" & objen.CLineRemarks & "'  where ""DocEntry""='" & objen.AppraisalNumber & "' and ""LineId""=" & objen.LineNo & ""
        objDA.cmd = New SqlCommand(objen.StrQry, objDA.con)
        objDA.con.Open()
        objDA.cmd.ExecuteNonQuery()
        objDA.con.Close()
    End Sub
    Public Function ObjectiveBind(ByVal objen As LineMgrAppraisalEN) As DataSet
        objen.StrQry = "SELECT ""U_Z_BussCode"", ""U_Z_BussDesc"", ""U_Z_BussMgrRate"", ""U_Z_BussSelfRate"", ""U_Z_BussWeight"", ""LineId"", ""U_Z_BussSMRate"", ""U_Z_SelfRaCode"", ""U_Z_SMRaCode"", ""U_Z_MgrRaCode"",""U_Z_SelfRemark"",""U_Z_MgrRemark"",""U_Z_SrRemark"" FROM ""@Z_HR_SEAPP1"" WHERE ""DocEntry""='" & objen.AppraisalNumber & "';"
        objen.StrQry += "SELECT ""U_Z_PeopleCode"", ""U_Z_PeopleDesc"", ""U_Z_PeopleCat"", ""U_Z_PeoWeight"", ""U_Z_PeoSelfRate"", ""U_Z_PeoMgrRate"", ""LineId"", ""U_Z_PeoSMRate"", ""U_Z_SelfRaCode"", ""U_Z_SMRaCode"", ""U_Z_MgrRaCode"",""U_Z_SelfRemark"",""U_Z_MgrRemark"",""U_Z_SrRemark"" FROM ""@Z_HR_SEAPP2"" WHERE ""DocEntry""='" & objen.AppraisalNumber & "';"
        objen.StrQry += "SELECT T0.""U_Z_CompCode"", T0.""U_Z_CompDesc"", T0.""U_Z_CompMgrRate"", T0.""U_Z_CompSelfRate"", T0.""U_Z_CompWeight"", ISNULL(T0.""U_Z_CompLevel"", '0') AS ""U_Z_CompLevel"", T2.""U_Z_CompLevel"" AS ""CurrentLevel"", T0.""U_Z_CompSMRate"", T0.""LineId"", T0.""U_Z_SelfRaCode"", T0.""U_Z_SMRaCode"", T0.""U_Z_MgrRaCode"",""U_Z_SelfRemark"",""U_Z_MgrRemark"",""U_Z_SrRemark"" FROM ""@Z_HR_SEAPP3"" T0 INNER JOIN ""@Z_HR_OSEAPP"" T1 ON T1.""DocEntry"" = T0.""DocEntry"" LEFT OUTER JOIN ""@Z_HR_ECOLVL"" T2 ON T1.""U_Z_EmpId"" = T2.""U_Z_HREmpID"" AND T2.""U_Z_CompCode"" = T0.""U_Z_CompCode"" WHERE T0.""DocEntry""='" & objen.AppraisalNumber & "'"
        objDA.sqlda = New SqlDataAdapter(objen.StrQry, objDA.con)
        objDA.sqlda.Fill(objDA.ds)
        Return objDA.ds
    End Function
    Public Function PopulateEmployee(ByVal objen As LineMgrAppraisalEN) As DataSet
        objen.StrQry = "SELECT ""DocEntry"", ""U_Z_EmpId"", ""U_Z_EmpName"", CAST(""U_Z_Date"" AS varchar(11)) AS ""U_Z_Date"", ""U_Z_Period"",T1.U_Z_PerDesc,T1.U_Z_PerFrom, "
        objen.StrQry += " T1.U_Z_PerTo,""U_Z_BSelfRemark"", ""U_Z_PSelfRemark"", ""U_Z_CSelfRemark"", ""U_Z_CMgrRemark"", ""U_Z_PMgrRemark"", ""U_Z_BMgrRemark"","
        objen.StrQry += " ""U_Z_CSMrRemark"", ""U_Z_PSMrRemark"", ""U_Z_BSMrRemark"", CASE ""U_Z_Status"" WHEN 'D' THEN 'Draft' WHEN 'F' THEN 'Approved' "
        objen.StrQry += " WHEN 'S' THEN '2nd Level Approval' WHEN 'L' THEN 'Closed' ELSE 'Canceled' END AS ""U_Z_Status"", CASE ""U_Z_WStatus"" "
        objen.StrQry += " WHEN 'DR' THEN 'Draft' WHEN 'HR' THEN 'HR Approved' WHEN 'SM' THEN 'Sr.Manager Approved' WHEN 'LM' THEN 'LineManager Approved' "
        objen.StrQry += " WHEN 'SE' THEN 'SelfApproved' END AS ""U_Z_WStatus"", ""U_Z_BHrRemark"", ""U_Z_PHrRemark"", ""U_Z_CHrRemark"", ""U_Z_GStatus"", "
        objen.StrQry += " ""U_Z_GRemarks"", CAST(""U_Z_GDate"" AS varchar(11)) AS ""U_Z_GDate"", ""U_Z_GNo"" FROM ""@Z_HR_OSEAPP"" T0 Left Outer Join ""@Z_HR_PERAPP"" T1 on T0.U_Z_Period=T1.U_Z_PerCode  "
        objen.StrQry += " where ""U_Z_EmpId""='" & objen.EmpId & "' and ""DocEntry""='" & objen.AppraisalNumber & "'"
        objDA.sqlda = New SqlDataAdapter(objen.StrQry, objDA.con)
        objDA.sqlda.Fill(objDA.ds1)
        Return objDA.ds1
    End Function
    Public Function SelectionChange(ByVal objen As LineMgrAppraisalEN) As DataSet
        objen.StrQry = "Select * from ""@Z_HR_ORATE"" where ""U_Z_RateCode""='" & objen.Ratings & "'"
        objDA.sqlda = New SqlDataAdapter(objen.StrQry, objDA.con)
        objDA.sqlda.Fill(objDA.ds2)
        Return objDA.ds2
    End Function

    Public Function BindSearchPageLoad(ByVal objen As LineMgrAppraisalEN) As DataSet
        objen.EmpId = getEmpIDforMangers(objen)
        If objen.EmpId <> "" Then
            objen.strConiditon = "" & objen.SearchCondition & " and ""U_Z_EmpId"" in (" & objen.EmpId & ") Order by ""DocEntry"" Desc"
            objDA.strQuery = " select DocEntry,U_Z_EmpId,U_Z_EmpName,Convert(Varchar(10),U_Z_Date,103) AS U_Z_Date,U_Z_Period,T1.U_Z_PerDesc,T1.U_Z_PerFrom,"
            objDA.strQuery += " T1.U_Z_PerTo,case U_Z_Status when 'D' then 'Draft' when 'F' then 'Approved' "
            objDA.strQuery += " when 'S'then '2nd Level Approval' when 'L' then 'Closed' else 'Canceled' end as U_Z_Status,"
            objDA.strQuery += " case U_Z_WStatus when 'DR' then 'Draft' when 'HR' then 'HR Approved' when 'SM'then 'Sr.Manager Approved' "
            objDA.strQuery += " when 'LM' then 'LineManager Approved'when 'SE' then 'SelfApproved'  end as 'U_Z_WStatus'   "
            objDA.strQuery += " from [@Z_HR_OSEAPP] T0 Left Outer Join ""@Z_HR_PERAPP"" T1 on T0.U_Z_Period=T1.U_Z_PerCode Where " & objen.strConiditon
        End If
        objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
        objDA.sqlda.Fill(objDA.dss1)
        Return objDA.dss1
    End Function
    Public Function LoadActivity(ByVal objEN As LineMgrAppraisalEN) As DataSet
        Try
            objDA.strQuery1 = "Select ISNULL(""userId"",'0') from OHEM where ""empID""='" & objEN.StrType & "'"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery1, objDA.con)
            objDA.sqlda.Fill(objDA.ds)
            If objDA.ds.Tables(0).Rows(0)(0).ToString = 0 Then
                objDA.strQuery = "Select T0.""ClgCode"",T1.""Name"",case T0.""Action"" when 'T' then 'Task' else 'Other' end as ""Action"",T2.""Name"" as ""Subject"", T3.""firstName"" +' ' + ISNULL(T3.""middleName"",'') +' '+ T3.""lastName"" as ""EmpName"",convert(varchar(10),Recontact,103) as ""Recontact"",T0.""BeginTime"",convert(varchar(10),endDate,103) as ""endDate"",T0.""ENDTime"",T0.""Duration"",T0.""Details"""
                objDA.strQuery += " ,T4.""name"" as ""status"",T5.""U_NAME"" as ""UserName"",case T0.""Priority"" when '0' then 'Low' when '1' then 'Normal' when '2' then 'High' end as ""Priority"" from OCLG T0 left join OCLT T1 on "
                objDA.strQuery += " T0.""CntctType""=T1.""Code"" left join OCLS T2 on T0.""CntctSbjct""=T2.""Code"" left join OHEM T3"
                objDA.strQuery += " on T0.""AttendEmpl""=T3.""empID"" left join OUSR T5 on T0.""AttendUser""=T5.""INTERNAL_K""  left join OCLA T4 on T0.""status""=T4.""statusID"" where ""U_Z_HREmpID""='" & objEN.EmpId & "'  order by T0.""ClgCode"" desc"
            Else
                objDA.strQuery = "Select T0.""ClgCode"",T1.""Name"",case T0.""Action"" when 'T' then 'Task' else 'Other' end as ""Action"",T2.""Name"" as ""Subject"", T3.""firstName"" +' ' + ISNULL(T3.""middleName"",'') +' '+ T3.""lastName"" as ""EmpName"",convert(varchar(10),Recontact,103) as ""Recontact"",T0.""BeginTime"",convert(varchar(10),endDate,103) as ""endDate"",T0.""ENDTime"",T0.""Duration"",T0.""Details"""
                objDA.strQuery += " ,T4.""name"" as ""status"",T5.""U_NAME"" as ""UserName"",case T0.""Priority"" when '0' then 'Low' when '1' then 'Normal' when '2' then 'High' end as ""Priority"" from OCLG T0 left join OCLT T1 on "
                objDA.strQuery += " T0.""CntctType""=T1.""Code"" left join OCLS T2 on T0.""CntctSbjct""=T2.""Code"" left join OHEM T3"
                objDA.strQuery += " on T0.""AttendEmpl""=T3.""empID"" left join OUSR T5 on T0.""AttendUser""=T5.""INTERNAL_K""  left join OCLA T4 on T0.""status""=T4.""statusID"" where  ""U_Z_HREmpID""='" & objEN.EmpId & "' order by T0.""ClgCode"" desc"
            End If
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds4)
            Return objDA.ds4
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function SecondLevelApproval(ByVal objen As LineMgrAppraisalEN) As DataSet
        objen.StrQry = "Select isnull(T1.U_Z_SecondApp,'N'),U_Z_HRMail,T0.U_Z_EmpId from [@Z_HR_OSEAPP] T0 JOIN OHEM T1 ON T0.U_Z_EmpID=T1.empID where T0.DocEntry='" & objen.AppraisalNumber & "'"
        objDA.sqlda = New SqlDataAdapter(objen.StrQry, objDA.con)
        objDA.sqlda.Fill(objDA.dss2)
        Return objDA.dss2
    End Function

    Public Function GetSecondEmail(ByVal Empid As String) As String
        Try
            objDA.con.Open()
            objDA.cmd = New SqlCommand("SELECT ""email"" FROM OHEM  WHERE ""empID"" IN (Select ""manager"" from OHEM where ""empID""=" & Empid & ")", objDA.con)
            objDA.cmd.CommandType = CommandType.Text
            Dim status As String
            status = objDA.cmd.ExecuteScalar()
            objDA.con.Close()
            Return status
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
