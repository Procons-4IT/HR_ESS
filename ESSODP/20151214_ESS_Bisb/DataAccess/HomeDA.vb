Imports System
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Data.SqlClient
Imports EN
Public Class HomeDA
    Dim objen As HomeEN = New HomeEN()
    Dim objDA As DBConnectionDA = New DBConnectionDA()
    Dim objCom As CommonFunctions = New CommonFunctions()
    Public Sub New()
        objDA.con = New SqlConnection(objDA.GetConnection)
    End Sub

    Public Function PopulateEmployeeInternal(ByVal objen As HomeEN) As DataSet
        objen.StrQry = "Select dept,empID,firstName,lastName, isnull(position,0) as position,descriptio  from OHEM T0 left join OHPS T1 on T0.position=t1.posID  where empID=" & objen.EmpId & ""
        objDA.sqlda = New SqlDataAdapter(objen.StrQry, objDA.con)
        objDA.sqlda.Fill(objDA.ds1)
        Return objDA.ds1
    End Function
    Public Function PopulateEmployee(ByVal objen As HomeEN) As DataSet
        objDA.strQuery = "SELECT isnull(U_Z_EmpID,'') as 'TAEmpID', ""empID"", ""firstName"", ""lastName"", ""middleName"", ""U_Z_HR_ThirdName"",T0.U_Z_GrdName,T0.U_Z_HR_SalaryCode,"
        objDA.strQuery += " T0.""position"", T1.""descriptio"" AS ""Positionname"", ""dept"", T2.""Remarks"" AS ""Deptname"", ""U_Z_HR_JobstCode"", T3.""Name"" AS ""BranchName"", ""officeExt"", ""U_Z_Rel_Name"", ""U_Z_Rel_Type"", ""U_Z_Rel_Phone"", ""officeTel"", ""mobile"", ""email"", ""fax"", ""homeTel"", ""pager"",""sex"", convert(varchar(10),""birthDate"",103) AS ""birthDate"", ""brthCountr"", ""martStatus"", ""nChildren"", ""govID"", ""citizenshp"", convert(varchar(10),""passportEx"",103) AS ""passportEx"", ""passportNo"", ""workBlock"", ""workCity"", ""workCountr"", ""workState"", ""workCounty"", ""workStreet"", ""workZip"", ""homeBlock"", ""homeCity"", ""homeCountr"", ""homeCounty"", ""homeState"", ""homeStreet"", ""homeZip"", ""U_Z_HR_OrgstCode"", ""U_Z_HR_OrgstName"", ""WorkBuild"", ""HomeBuild"", ""U_Z_LvlCode"", ""U_Z_LvlName"", ""U_Z_LocCode"", ""U_Z_LocName"", ""U_Z_HR_JobstCode"", ""U_Z_HR_JobstName"", ""U_Z_HR_SalaryCode"", ""U_Z_HR_ApplId"", ISNULL(""manager"", 0) AS ""Manager"" FROM OHEM T0 LEFT OUTER JOIN OHPS T1 ON T0.""position"" = T1.""posID"" LEFT OUTER JOIN OUDP T2 ON T0.""dept"" = T2.""Code"" LEFT OUTER JOIN OUBR T3 ON T0.""branch"" = T3.""Code"" where ""empID""='" & objen.EmpId & "'"
        objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
        objDA.sqlda.Fill(objDA.dss)
        Return objDA.dss
    End Function
    Public Function Department(ByVal objen As HomeEN) As String
        objDA.con.Open()
        objen.StrQry = "select Remarks from OUDP  where Code=" & objen.DeptCode & ""
        objDA.cmd = New SqlCommand(objen.StrQry, objDA.con)
        objDA.cmd.CommandType = CommandType.Text
        objen.DeptName = objDA.cmd.ExecuteScalar()
        objDA.con.Close()
        Return objen.DeptName
    End Function
    Public Function EmpManager(ByVal objen As HomeEN) As String
        objDA.con.Open()
        objen.StrQry = "select isnull(""firstName"",'') +  ' ' + isnull(""middleName"",'') +  ' ' + isnull(""lastName"",'') as ManName from OHEM where empId=" & objen.Manager & ""
        objDA.cmd = New SqlCommand(objen.StrQry, objDA.con)
        objDA.cmd.CommandType = CommandType.Text
        objen.DeptName = objDA.cmd.ExecuteScalar()
        objDA.con.Close()
        Return objen.DeptName
    End Function
    Public Function mainGvbind(ByVal objen As HomeEN) As DataSet
        objDA.strQuery = "select DocEntry,U_Z_EmpId,U_Z_EmpName,convert(varchar(10),U_Z_Date,103) as U_Z_Date,U_Z_Period,case U_Z_Status when 'D' then 'Draft' when 'F' then 'Approved'"
        objDA.strQuery += " when 'S'then '2nd Level Approval' when 'L' then 'Closed' else 'Canceled' end as U_Z_Status,case U_Z_WStatus when 'DR' then 'Draft' when 'HR' then 'HR Approved' when 'SM'then 'Second Level Approved' when 'LM' then 'First Level Approved' when 'SE' then 'SelfApproved'  end as 'U_Z_WStatus',U_Z_GStatus,U_Z_GRemarks,convert(varchar(11),U_Z_GDate,101) as U_Z_GDate,U_Z_GNo  from [@Z_HR_OSEAPP] where U_Z_EmpId=" & objen.EmpId & " and ISNULL(U_Z_GStatus,'') = '-' and (U_Z_WStatus='DR' or U_Z_WStatus='LM') Order by DocEntry Desc"
        objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
        objDA.sqlda.Fill(objDA.ds)
        Return objDA.ds
    End Function
    Public Function GetUserCode(ByVal objEN As HomeEN) As String
        Try
            objDA.strQuery = "select T1.USER_CODE from OHEM T0 JOIN OUSR T1 on T0.userId=T1.USERID where T0.empID='" & objEN.EmpId & "'"
            objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
            objDA.con.Open()
            objEN.UserCode = objDA.cmd.ExecuteScalar()
            objDA.con.Close()
            Return objEN.UserCode
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function LeaveBalance(ByVal objen As HomeEN) As DataSet
        objDA.strQuery = "SELECT T0.[U_Z_Year],T0.[U_Z_LeaveCode],T0.[U_Z_LeaveName],cast(T0.U_Z_Entile as decimal(10,2)) AS [U_Z_Entile],cast(T0.U_Z_CAFWD as decimal(10,2)) AS[U_Z_CAFWD],cast(T0.U_Z_CAFWDAMT as decimal(10,2)) AS [U_Z_CAFWDAMT],cast(T0.U_Z_ACCR as decimal(10,2)) AS [U_Z_ACCR],"
        objDA.strQuery += " cast(T0.U_Z_Trans as decimal(10,2)) AS [U_Z_Trans],cast(T0.U_Z_Adjustment as decimal(10,2)) AS [U_Z_Adjustment],cast(T0.U_Z_Balance as decimal(10,2)) AS [U_Z_Balance],cast(T0.U_Z_BalanceAmt as decimal(10,2)) AS [U_Z_BalanceAmt],cast(T0.U_Z_OB as decimal(10,2)) AS [U_Z_OB],cast(T0.U_Z_EnCash as decimal(10,2)) AS [U_Z_EnCash],cast(T0.U_Z_CashOut as decimal(10,2)) AS [U_Z_CashOut] "
        objDA.strQuery += " FROM [dbo].[@Z_EMP_LEAVE_BALANCE]  T0 JOIN ""@Z_PAY_LEAVE"" T1 ON T0.U_Z_LeaveCode=T1.Code  where T0.U_Z_EmpID=" & objen.EmpId & " and T0.[U_Z_Year]='" & Now.Year & "' and isnull(T1.U_Z_HideESS,'N')='N'"
        objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
        objDA.sqlda.Fill(objDA.ds2)
        If objDA.ds2.Tables(0).Rows.Count > 0 Then
            Return objDA.ds2
        Else
            objDA.ds2.Clear()
            objDA.strQuery = "SELECT T0.[U_Z_Year],T0.[U_Z_LeaveCode],T0.[U_Z_LeaveName],cast(T0.U_Z_Entile as decimal(10,2)) AS [U_Z_Entile],cast(T0.U_Z_CAFWD as decimal(10,2)) AS[U_Z_CAFWD],cast(T0.U_Z_CAFWDAMT as decimal(10,2)) AS [U_Z_CAFWDAMT],cast(T0.U_Z_ACCR as decimal(10,2)) AS [U_Z_ACCR],"
            objDA.strQuery += " cast(T0.U_Z_Trans as decimal(10,2)) AS [U_Z_Trans],cast(T0.U_Z_Adjustment as decimal(10,2)) AS [U_Z_Adjustment],cast(T0.U_Z_Balance as decimal(10,2)) AS [U_Z_Balance],cast(T0.U_Z_BalanceAmt as decimal(10,2)) AS [U_Z_BalanceAmt],cast(T0.U_Z_OB as decimal(10,2)) AS [U_Z_OB],cast(T0.U_Z_EnCash as decimal(10,2)) AS [U_Z_EnCash],cast(T0.U_Z_CashOut as decimal(10,2)) AS [U_Z_CashOut] "
            objDA.strQuery += " FROM [dbo].[@Z_EMP_LEAVE_BALANCE]  T0 JOIN ""@Z_PAY_LEAVE"" T1 ON T0.U_Z_LeaveCode=T1.Code  where T0.U_Z_EmpID=" & objen.EmpId & " and T0.[U_Z_Year]='" & Now.Year - 1 & "' and isnull(T1.U_Z_HideESS,'N')='N'"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds2)
            Return objDA.ds2
        End If
    End Function
    Public Function mainGvbind1(ByVal objen As HomeEN) As DataSet
        Try
            objDA.strQuery = "Select T1.""name"" as ""position"" from OHEM T0 left join OHPS T1 on T0.""position""=T1.""posID"" where ""empID""=" & objen.EmpId & ""
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.dss4)
            If objDA.dss4.Tables(0).Rows.Count <> 0 Then
                objDA.strQuery = "  select distinct(T0.U_Z_TrainCode),convert(varchar(10),T0.U_Z_DocDate,103) as U_Z_DocDate,T0.U_Z_CourseCode ,T0.U_Z_CourseName,T0.U_Z_CourseTypeDesc,convert(varchar(10),T0.U_Z_Startdt,103) as U_Z_Startdt,convert(varchar(10),T0.U_Z_Enddt,103) as U_Z_Enddt,T0.U_Z_MinAttendees,T0.U_Z_MaxAttendees,convert(varchar(10),T0.U_Z_AppStdt,103) as U_Z_AppStdt,convert(varchar(10),T0.U_Z_AppEnddt,103) as U_Z_AppEnddt,"
                objDA.strQuery += "T4. U_Z_FirstName +''+ T4.U_Z_LastName as U_Z_InsName,T0.U_Z_NoOfHours,T0.U_Z_StartTime,T0.U_Z_EndTime,isnull(T0.U_Z_Sunday,'N') 'U_Z_Sunday',isnull(T0.U_Z_Monday,'N') 'U_Z_Monday',isnull(T0.U_Z_Tuesday,'N') 'U_Z_Tuesday',isnull(T0.U_Z_Wednesday,'N') 'U_Z_Wednesday',isnull(T0.U_Z_Thursday,'N') 'U_Z_Thursday',isnull(T0.U_Z_Friday,'N') 'U_Z_Friday',isnull(T0.U_Z_Saturday,'N') 'U_Z_Saturday',T0.U_Z_AttCost,T0.U_Z_Active  from [@Z_HR_OTRIN] T0 left join [@Z_HR_OCOUR] T1 on T0.U_Z_CourseCode=T1.U_Z_CourseCode left join "
                objDA.strQuery += "  [@Z_HR_COUR4] T2  on T1.DocEntry=t2.DocEntry left join [@Z_HR_TRIN1] T3 on T3.U_Z_CourseCode<>T0.U_Z_CourseCode left join ""@Z_HR_TRRAPP"" T4 on T0.U_Z_InsName=T4.DocEntry where  (isnull(T1.U_Z_Allpos,'N')='Y' or  T2.U_Z_PosCode='" & objDA.dss4.Tables(0).Rows(0)("position").ToString() & "') and T0.U_Z_Active='Y' and isnull(T0.U_Z_Status,'O')='O' and  T0.U_Z_TrainCode not in( select U_Z_TrainCode from [@Z_HR_TRIN1] where U_Z_HREmpID='" & objen.EmpId & "') and GETDATE() between T0.U_Z_AppStdt and T0.U_Z_AppEnddt"
                objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
                objDA.sqlda.Fill(objDA.dss1)
                Return objDA.dss1
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function AppPositions(ByVal objen As HomeEN) As DataSet
        objDA.strQuery = "Select U_Empid,U_Empname,U_EmpPosCode,U_EmpPosName,U_EmpdeptCode,U_EmpdeptName,U_ReqdeptCode,U_ReqdeptName,U_ReqPosCode,U_Remarks,"
        objDA.strQuery += " U_ReqPosName,U_RequestCode,U_ApplyDate,case U_Z_AppStatus when 'P' then 'Pending' when 'A' then 'Approved' when 'R' then 'Rejected' end as U_Z_AppStatus from U_VACPOSITION where U_Empid='" & objen.EmpId & "'"
        objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
        objDA.sqlda.Fill(objDA.dss2)
        Return objDA.dss2
    End Function
    Public Function VacPositions(ByVal objen As HomeEN) As DataSet
        objDA.strQuery = "select DocEntry,U_Z_ReqDate,U_Z_DeptCode,U_Z_DeptName,isnull(U_Z_PosName,'') as Position,U_Z_ExpMin,U_Z_ExpMax, U_Z_Vacancy,U_Z_EmpPosi,"
        objDA.strQuery += "convert(varchar(10),U_Z_EmpstDate,103) as U_Z_EmpstDate,convert(varchar(10),U_Z_IntAppDead,103) as U_Z_IntAppDead,convert(varchar(10),U_Z_ExtAppDead,103) as U_Z_ExtAppDead  from [@Z_HR_ORMPREQ] where U_Z_AppStatus='A' and"
        objDA.strQuery += " DocEntry not in(select U_RequestCode from U_VACPOSITION where U_Empid='" & objen.EmpId & "') and U_Z_IntAppDead>=GETDATE()"
        objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
        objDA.sqlda.Fill(objDA.dss3)
        Return objDA.dss3
    End Function
    Public Function ApplyPosition(ByVal objen As HomeEN) As Boolean
        Try
            objDA.strQuery = "Insert into U_VACPOSITION(U_Empid,U_Empname,U_EmpPosCode,U_EmpPosName,U_EmpdeptCode,U_EmpdeptName,U_ReqdeptCode,U_ReqdeptName,U_ReqPosCode,U_ReqPosName,U_RequestCode,U_ApplyDate,U_Z_AppStatus )"
            objDA.strQuery += " Values ('" & objen.EmpId & "','" & objen.EmpName & "','" & objen.EmpPosCode & "','" & objen.EmpPosName & "','" & objen.DeptCode & "','" & objen.DeptName & "',"
            objDA.strQuery += " '" & objen.ReqDeptCode & "','" & objen.ReqDeptName & "','" & objen.ReqposCode & "','" & objen.ReqPosName & "','" & objen.RequestNo & "',getdate(),'P')"
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
    Public Function LoadActivity(ByVal objEN As HomeEN) As DataSet
        Try
            objDA.strQuery1 = "Select ISNULL(""userId"",'0'),""empID"" from OHEM where ""empID""='" & objEN.EmpId & "'"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery1, objDA.con)
            objDA.sqlda.Fill(objDA.ds3)
            If objDA.ds3.Tables(0).Rows(0)(0).ToString = 0 Then
                objDA.strQuery = "Select T0.""ClgCode"",T0.""U_Z_HREmpID"",T0.""U_Z_HREmpName"",T1.""Name"",case T0.""Action"" when 'T' then 'Task' else 'Other' end as ""Action"",T2.""Name"" as ""Subject"", T3.""firstName"" +' '+ ISNULL(T3.""middleName"",'') +' '+ T3.""lastName"" as ""EmpName"",convert(varchar(10),Recontact,103) as ""Recontact"",T0.""BeginTime"",convert(varchar(10),endDate,103) as ""endDate"",T0.""ENDTime"",T0.""Duration"",T0.""Details"""
                objDA.strQuery += " ,T4.""name"" as ""status"",T5.""U_NAME"" as ""UserName"",case T0.""Priority"" when '0' then 'Low' when '1' then 'Normal' when '2' then 'High' end as ""Priority"" from OCLG T0 left join OCLT T1 on "
                objDA.strQuery += " T0.""CntctType""=T1.""Code"" left join OCLS T2 on T0.""CntctSbjct""=T2.""Code"" left join OHEM T3"
                objDA.strQuery += " on T0.""AttendEmpl""=T3.""empID"" left join OUSR T5 on T0.""AttendUser""=T5.""INTERNAL_K""  left join OCLA T4 on T0.""status""=T4.""statusID"" where T0.""AttendEmpl""='" & objEN.EmpId & "' order by T0.""ClgCode"" desc"
            Else
                objDA.strQuery = "Select T0.""ClgCode"",T0.""U_Z_HREmpID"",T0.""U_Z_HREmpName"",T1.""Name"",case T0.""Action"" when 'T' then 'Task' else 'Other' end as ""Action"",T2.""Name"" as ""Subject"", T3.""firstName"" +' '+ ISNULL(T3.""middleName"",'') +' '+ T3.""lastName"" as ""EmpName"",convert(varchar(10),Recontact,103) as ""Recontact"",T0.""BeginTime"",convert(varchar(10),endDate,103) as ""endDate"",T0.""ENDTime"",T0.""Duration"",T0.""Details"""
                objDA.strQuery += " ,T4.""name"" as ""status"",T5.""U_NAME"" as ""UserName"",case T0.""Priority"" when '0' then 'Low' when '1' then 'Normal' when '2' then 'High' end as ""Priority"" from OCLG T0 left join OCLT T1 on "
                objDA.strQuery += " T0.""CntctType""=T1.""Code"" left join OCLS T2 on T0.""CntctSbjct""=T2.""Code"" left join OHEM T3"
                objDA.strQuery += " on T0.""AttendEmpl""=T3.""empID"" left join OUSR T5 on T0.""AttendUser""=T5.""INTERNAL_K""  left join OCLA T4 on T0.""status""=T4.""statusID"" where (T0.""AttendUser""='" & objDA.ds3.Tables(0).Rows(0)(0).ToString & "' or T0.""AttendEmpl""='" & objEN.EmpId & "') order by T0.""ClgCode"" desc"
            End If
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds4)
            Return objDA.ds4
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ReturnDocEntry() As String
        objDA.con.Open()
        objen.StrQry = "select Top 1 U_DocEntry from [U_VACPOSITION] Order by U_DocEntry Desc "
        objDA.cmd = New SqlCommand(objen.StrQry, objDA.con)
        objDA.cmd.CommandType = CommandType.Text
        objen.DeptName = objDA.cmd.ExecuteScalar()
        objDA.con.Close()
        Return objen.DeptName
    End Function
    Public Function PendingApproval(ByVal objEN As HomeEN) As DataSet
        Dim strLvetype As String = getLeaveType(objEN.UserCode)
        Try
            objDA.strQuery = "   select COUNT(*) from [@Z_HR_TRIN1] T0 left join [@Z_HR_OTRIN] T1 ON  T1.U_Z_TrainCode=T0.U_Z_TrainCode"
            objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T0.U_Z_ApproveId=T3.DocEntry "
            objDA.strQuery += " JOIN [@Z_HR_APPT2] T4 ON T3.DocEntry = T4.DocEntry "
            objDA.strQuery += " where (T0.U_Z_CurApprover = '" & objEN.UserCode & "' OR T0.U_Z_NxtApprover = '" & objEN.UserCode & "')"
            objDA.strQuery += " And isnull(T4.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y' and  isnull(T0.U_Z_AppRequired,'N')='Y'"
            objDA.strQuery += " and  T4.U_Z_AUser = '" & objEN.UserCode & "' And T3.U_Z_DocType = 'Train' and T0.U_Z_AppStatus='P';"

            objDA.strQuery += "   select COUNT(*) from [@Z_HR_ONTREQ] T0 "
            objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T0.U_Z_ApproveId=T3.DocEntry "
            objDA.strQuery += " JOIN [@Z_HR_APPT2] T4 ON T3.DocEntry = T4.DocEntry "
            objDA.strQuery += " where (T0.U_Z_CurApprover = '" & objEN.UserCode & "' OR T0.U_Z_NxtApprover = '" & objEN.UserCode & "')"
            objDA.strQuery += " And isnull(T4.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y' and  isnull(T0.U_Z_AppRequired,'N')='Y'"
            objDA.strQuery += " and  T4.U_Z_AUser = '" & objEN.UserCode & "' And T3.U_Z_DocType = 'Train' and T0.U_Z_AppStatus='P';"

            objDA.strQuery += "   select COUNT(*) from [@Z_HR_HEM2] T0 "
            objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T0.U_Z_ApproveId=T3.DocEntry "
            objDA.strQuery += " JOIN [@Z_HR_APPT2] T4 ON T3.DocEntry = T4.DocEntry "
            objDA.strQuery += " where (T0.U_Z_CurApprover = '" & objEN.UserCode & "' OR T0.U_Z_NxtApprover = '" & objEN.UserCode & "')"
            objDA.strQuery += " And isnull(T4.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y' and  isnull(T0.U_Z_AppRequired,'N')='Y'"
            objDA.strQuery += " and  T4.U_Z_AUser = '" & objEN.UserCode & "' And T3.U_Z_DocType = 'EmpLife' and T0.U_Z_AppStatus='P';"

            objDA.strQuery += "   select COUNT(*) from [@Z_HR_HEM4] T0 "
            objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T0.U_Z_ApproveId=T3.DocEntry "
            objDA.strQuery += " JOIN [@Z_HR_APPT2] T4 ON T3.DocEntry = T4.DocEntry "
            objDA.strQuery += " where (T0.U_Z_CurApprover = '" & objEN.UserCode & "' OR T0.U_Z_NxtApprover = '" & objEN.UserCode & "')"
            objDA.strQuery += " And isnull(T4.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y' and  isnull(T0.U_Z_AppRequired,'N')='Y'"
            objDA.strQuery += " and  T4.U_Z_AUser = '" & objEN.UserCode & "' And T3.U_Z_DocType = 'EmpLife' and T0.U_Z_AppStatus='P';"

            objDA.strQuery += "   select COUNT(*) from ""@Z_HR_OTRAREQ"" T0 "
            objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T0.U_Z_ApproveId=T3.DocEntry "
            objDA.strQuery += " JOIN [@Z_HR_APPT2] T4 ON T3.DocEntry = T4.DocEntry "
            objDA.strQuery += " where (T0.U_Z_CurApprover = '" & objEN.UserCode & "' OR T0.U_Z_NxtApprover = '" & objEN.UserCode & "')"
            objDA.strQuery += " And isnull(T4.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y' and  isnull(T0.U_Z_AppRequired,'N')='Y'"
            objDA.strQuery += " and  T4.U_Z_AUser = '" & objEN.UserCode & "' And T3.U_Z_DocType = 'TraReq' and T0.U_Z_AppStatus='P';"

            objDA.strQuery += "   select COUNT(*) from [@Z_HR_EXPCL] T0 Left outer Join [@Z_HR_OEXPCL] T1 ON T0.U_Z_DocRefNo=T1.Code"
            objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T0.U_Z_ApproveId=T3.DocEntry "
            objDA.strQuery += " JOIN [@Z_HR_APPT2] T4 ON T3.DocEntry = T4.DocEntry "
            objDA.strQuery += " where (T0.U_Z_CurApprover = '" & objEN.UserCode & "' OR T0.U_Z_NxtApprover = '" & objEN.UserCode & "')"
            objDA.strQuery += " And isnull(T4.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y' and  isnull(T0.U_Z_AppRequired,'N')='Y'"
            objDA.strQuery += " and  T4.U_Z_AUser = '" & objEN.UserCode & "' And T3.U_Z_DocType = 'ExpCli' and isnull(T1.U_Z_DocStatus,'O')='O' and T0.U_Z_AppStatus='P';"

            objDA.strQuery += "   select COUNT(*) from [@Z_HR_ORMPREQ] T0 "
            objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T0.U_Z_ApproveId=T3.DocEntry "
            objDA.strQuery += " JOIN [@Z_HR_APPT2] T4 ON T3.DocEntry = T4.DocEntry "
            objDA.strQuery += " where (T0.U_Z_CurApprover = '" & objEN.UserCode & "' OR T0.U_Z_NxtApprover = '" & objEN.UserCode & "')"
            objDA.strQuery += " And isnull(T4.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y' and  isnull(T0.U_Z_AppRequired,'N')='Y'"
            objDA.strQuery += " and  T4.U_Z_AUser = '" & objEN.UserCode & "' And T3.U_Z_DocType = 'Rec' and T0.U_Z_AppStatus='P';"

            objDA.strQuery += "   select COUNT(*) from [@Z_HR_OHEM1] T0 "
            objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T0.U_Z_ApproveId=T3.DocEntry "
            objDA.strQuery += " JOIN [@Z_HR_APPT2] T4 ON T3.DocEntry = T4.DocEntry "
            objDA.strQuery += " where (T0.U_Z_CurApprover = '" & objEN.UserCode & "' OR T0.U_Z_NxtApprover = '" & objEN.UserCode & "')"
            objDA.strQuery += " And isnull(T4.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y' and  isnull(T0.U_Z_AppRequired,'N')='Y'"
            objDA.strQuery += " and  T4.U_Z_AUser = '" & objEN.UserCode & "' And T3.U_Z_DocType = 'Rec' and T0.U_Z_AppStatus='P';"

            objDA.strQuery += "   select COUNT(*) from [U_VACPOSITION] T0 "
            objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T0.U_Z_ApproveId=T3.DocEntry "
            objDA.strQuery += " JOIN [@Z_HR_APPT2] T4 ON T3.DocEntry = T4.DocEntry "
            objDA.strQuery += " where (T0.U_CurApprover = '" & objEN.UserCode & "' OR T0.U_NxtApprover = '" & objEN.UserCode & "')"
            objDA.strQuery += " And isnull(T4.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y' and  isnull(T0.U_Z_AppRequired,'N')='Y'"
            objDA.strQuery += " and  T4.U_Z_AUser = '" & objEN.UserCode & "' And T3.U_Z_DocType = 'Rec' and T0.U_Z_AppStatus='P';"

            objDA.strQuery += "   select COUNT(*) from [U_PEOPLEOBJ] T0 "
            objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T0.U_Z_ApproveId=T3.DocEntry "
            objDA.strQuery += " JOIN [@Z_HR_APPT2] T4 ON T3.DocEntry = T4.DocEntry "
            objDA.strQuery += " where (T0.U_CurApprover = '" & objEN.UserCode & "' OR T0.U_NxtApprover = '" & objEN.UserCode & "')"
            objDA.strQuery += " And isnull(T4.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y' and  isnull(T0.U_Z_AppRequired,'N')='Y'"
            objDA.strQuery += " and  T4.U_Z_AUser = '" & objEN.UserCode & "' And T3.U_Z_DocType = 'EmpLife' and T0.U_Z_AppStatus='P';"

            objDA.strQuery += "   select COUNT(*) from [U_LOANREQ] T0 "
            objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T0.U_Z_ApproveId=T3.DocEntry "
            objDA.strQuery += " JOIN [@Z_HR_APPT2] T4 ON T3.DocEntry = T4.DocEntry "
            objDA.strQuery += " where (T0.U_CurApprover = '" & objEN.UserCode & "' OR T0.U_NxtApprover = '" & objEN.UserCode & "')"
            objDA.strQuery += " And isnull(T4.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y' and  isnull(T0.U_Z_AppRequired,'N')='Y'"
            objDA.strQuery += " and  T4.U_Z_AUser = '" & objEN.UserCode & "' And T3.U_Z_DocType = 'LoanReq' and T0.U_Z_AppStatus='P';"


            objDA.strQuery += "   select COUNT(*) from ""@Z_PAY_OLETRANS1"" T0 "
            objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T0.U_Z_ApproveId=T3.DocEntry "
            objDA.strQuery += " JOIN [@Z_HR_APPT2] T4 ON T3.DocEntry = T4.DocEntry "
            objDA.strQuery += " where (T0.U_Z_CurApprover = '" & objEN.UserCode & "' OR T0.U_Z_NxtApprover = '" & objEN.UserCode & "')"
            objDA.strQuery += " And isnull(T4.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y' and  isnull(T0.U_Z_AppRequired,'N')='Y'"
            objDA.strQuery += " and  T4.U_Z_AUser = '" & objEN.UserCode & "' And T3.U_Z_DocType = 'LveReq' AND ""U_Z_TrnsCode"" in (" & strLvetype & ") and T0.U_Z_Status='P';"

            objDA.strQuery += "   select COUNT(*) from ""@Z_PAY_OLETRANS1"" T0 "
            objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T0.U_Z_ApproveId=T3.DocEntry "
            objDA.strQuery += " JOIN [@Z_HR_APPT2] T4 ON T3.DocEntry = T4.DocEntry "
            objDA.strQuery += " where (T0.U_Z_CurApprover = '" & objEN.UserCode & "' OR T0.U_Z_NxtApprover = '" & objEN.UserCode & "')"
            objDA.strQuery += " And isnull(T4.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y' and  isnull(T0.U_Z_AppRequired,'N')='Y'"
            objDA.strQuery += " and  T4.U_Z_AUser = '" & objEN.UserCode & "' And T3.U_Z_DocType = 'LveReq' and T0.""U_Z_TransType""='R' and T0.""U_Z_Status""='A' AND ""U_Z_TrnsCode"" in (" & strLvetype & ") and T0.U_Z_Status='P';"

            objDA.strQuery += "   select COUNT(*) from ""@Z_PAY_OLADJTRANS1"" T0 "
            objDA.strQuery += " JOIN [@Z_HR_OAPPT] T3 ON T0.U_Z_ApproveId=T3.DocEntry "
            objDA.strQuery += " JOIN [@Z_HR_APPT2] T4 ON T3.DocEntry = T4.DocEntry "
            objDA.strQuery += " where (T0.U_Z_CurApprover = '" & objEN.UserCode & "' OR T0.U_Z_NxtApprover = '" & objEN.UserCode & "')"
            objDA.strQuery += " And isnull(T4.U_Z_AMan,'N')='Y' AND isnull(T3.U_Z_Active,'N')='Y' and  isnull(T0.U_Z_AppRequired,'N')='Y'"
            objDA.strQuery += " and  T4.U_Z_AUser = '" & objEN.UserCode & "' And T3.U_Z_DocType = 'LveReq'  and T0.""U_Z_AppStatus""='P' AND ""U_Z_TrnsCode"" in (" & strLvetype & ");"

            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.dss5)
            Return objDA.dss5
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
        End Try
    End Function
    Public Function getLeaveType(ByVal aCode As String) As String
        Dim LeaveType As String = ""
        objDA.strQuery = "select T0.U_Z_LveType from [@Z_HR_OAPPT] T0 JOIN [@Z_HR_APPT2] T1 on T0.DocEntry=T1.DocEntry where T1.U_Z_AUser ='" & aCode & "'  and T0.U_Z_DocType='LveReq'"
        objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
        objDA.sqlda.Fill(objDA.dss4)
        If objDA.dss4.Tables(0).Rows.Count > 0 Then
            For intRow As Integer = 0 To objDA.dss4.Tables(0).Rows.Count - 1
                If LeaveType = "" Then
                    LeaveType = "'" & objDA.dss4.Tables(0).Rows(intRow)(0).ToString() & "'"
                Else
                    LeaveType = LeaveType & " ,'" & objDA.dss4.Tables(0).Rows(intRow)(0).ToString() & "'"
                End If
            Next
            Return LeaveType
        Else
            Return "99999"
        End If
    End Function
End Class
