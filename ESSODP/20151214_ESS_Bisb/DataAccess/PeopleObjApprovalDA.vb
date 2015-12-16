Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Odbc
Imports DataAccess
Imports EN
Public Class PeopleObjApprovalDA
    Dim objEN As PeopleObjApprovalEN = New PeopleObjApprovalEN()
    Dim objDA As DBConnectionDA = New DBConnectionDA()
    Public Sub New()
        objDA.con = New SqlConnection(objDA.GetConnection)
    End Sub
    Public Function Pageloadbind() As DataSet
        Try
            objDA.strQuery = "Select ""empID"",""firstName"" from OHEM"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds)
            Return objDA.ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function getEmpIDforMangers(ByVal objen As PeopleObjApprovalEN) As String
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
    Public Function MainGridbind(ByVal objen As PeopleObjApprovalEN) As DataSet
        Try
            objDA.strQuery1 = "select ""U_DocEntry"",""U_Empid"",""U_PeoobjCode"",""U_PeoobjName"",T1.""U_Z_CatName"",T0.""U_PeoCategory"",""U_Weight"",""U_Remarks"",""U_Status"",""U_TypeAction"",""U_RefNo"""
            objDA.strQuery1 = objDA.strQuery1 & " from ""U_PEOPLEOBJ"" T0 left join ""@Z_HR_PECAT"" T1 on T0.""U_PeoCategory""=T1.""U_Z_CatCode""  where " & objen.EmpCondition
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery1, objDA.con)
            objDA.sqlda.Fill(objDA.dss)
            Return objDA.dss
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PopEmployeeBind(ByVal objen As PeopleObjApprovalEN) As DataSet
        Try
            If objen.EmpID <> "" Then
                objDA.strQuery = "Select ""empID"",""firstName"" from OHEM  where  ""firstName""  like '%" + objen.EmpID + "%'"
            Else
                objDA.strQuery = "Select ""empID"",""firstName"" from OHEM"
            End If
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds2)
            Return objDA.ds2
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function LineMgrPeopleApproval(ByVal objen As PeopleObjApprovalEN) As String
        Try
            Dim strCode As String
            If objDA.ConnectSAP() = True Then
                If objDA.oCompany.InTransaction() Then
                    objDA.oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit)
                End If
                objDA.oCompany.StartTransaction()
                Dim oUserTable As SAPbobsCOM.UserTable
                oUserTable = objDA.objMainCompany.UserTables.Item("Z_HR_PEOBJ1")
                If objen.peostatus = "A" And objen.Action = "New" And objen.Reqaction = "Y" Then
                    strCode = objDA.Getmaxcode("""@Z_HR_PEOBJ1""", """Code""")
                    oUserTable.Code = strCode
                    oUserTable.Name = strCode
                    oUserTable.UserFields.Fields.Item("U_Z_HREmpID").Value = objen.EmpId
                    oUserTable.UserFields.Fields.Item("U_Z_HRPeoobjCode").Value = objen.strpeocode
                    oUserTable.UserFields.Fields.Item("U_Z_HRPeoobjName").Value = objen.peoname
                    oUserTable.UserFields.Fields.Item("U_Z_HRPeoCategory").Value = objen.peocat
                    oUserTable.UserFields.Fields.Item("U_Z_HRWeight").Value = objen.peoweight
                    oUserTable.UserFields.Fields.Item("U_Z_Remarks").Value = objen.peoremark
                    If oUserTable.Add() <> 0 Then
                        objDA.strmsg = "alert('Personal Objective details added failed...')"
                    Else
                        objDA.strmsg = "alert('Personal Objective details added Succesfully...')"
                    End If
                ElseIf objen.peostatus = "A" And objen.Action = "Deleted" And objen.Reqaction = "Y" Then
                    If oUserTable.GetByKey(objen.Refno) Then
                        oUserTable.Code = objen.Refno
                        oUserTable.Name = objen.Refno
                        If oUserTable.Remove() <> 0 Then
                            objDA.strmsg = "alert('Personal Objective details Removed failed...')"
                        Else
                            objDA.strmsg = "alert('Personal Objective details Removed Succesfully...')"
                        End If
                    End If
                ElseIf objen.peostatus = "R" And objen.Action = "Deleted" And objen.Reqaction = "Y" Then
                    If oUserTable.GetByKey(objen.Refno) Then
                        oUserTable.Code = objen.Refno
                        oUserTable.Name = objen.Refno
                        If oUserTable.Update() <> 0 Then
                            objDA.strmsg = "alert('Personal Objective details Updated failed...')"
                        Else
                            objDA.strmsg = "alert('Personal Objective details Updated Succesfully...')"
                        End If
                    End If
                End If
                objDA.strQuery = "Update ""U_PEOPLEOBJ"" set ""U_Status""='" & objen.peostatus & "',""U_Remarks""='" & objen.peoremark & "' where ""U_DocEntry""='" & objen.strpeoDocno & "'"
                objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
                objDA.con.Open()
                objDA.cmd.ExecuteNonQuery()
                objDA.con.Close()
                If objDA.objMainCompany.InTransaction() Then
                    objDA.objMainCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit)
                End If
            End If
            Return objDA.strmsg
        Catch ex As Exception
            If objDA.oCompany.InTransaction() Then
                objDA.oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
            End If
            Return objDA.strmsg
        End Try
        Return objDA.strmsg
    End Function

    Public Function NotificationBind(ByVal objen As PeopleObjApprovalEN) As DataSet
        Try
            objDA.strQuery = "SELECT ""DocEntry"", T0.""U_Z_EmpId"", ""U_Z_EmpName"", CAST(""U_Z_Date"" AS varchar(11)) AS ""U_Z_Date"", T0.""U_Z_Period"", CASE ""U_Z_Status"" WHEN 'D' THEN 'Draft' WHEN 'F' THEN 'Approved' WHEN 'S' THEN '2nd Level Approval' WHEN 'L' THEN 'Closed' ELSE 'Canceled' END AS ""U_Z_Status"", CASE ""U_Z_WStatus"" WHEN 'DR' THEN 'Draft' WHEN 'HR' THEN 'HR Approved' WHEN 'SM' THEN 'Sr.Manager Approved' WHEN 'LM' THEN 'LineManager Approved' WHEN 'SE' THEN 'SelfApproved' END AS ""U_Z_WStatus"" FROM ""@Z_HR_OSEAPP"" T0 INNER JOIN OHEM T1 ON T0.""U_Z_EmpId"" = T1.""empID"" AND T1.""manager"" IN (SELECT ""empID"" FROM OHEM WHERE ""empID"" = '" & objen.EmpId & "' UNION SELECT ""empID"" FROM OHEM WHERE ""manager"" IN (SELECT ""empID"" FROM OHEM WHERE ""empID"" = '" & objen.EmpId & "')) ORDER BY ""DocEntry"" ASC"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds)
            Return objDA.ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function NotificationPageBind(ByVal objen As PeopleObjApprovalEN) As DataSet
        Try
            objDA.strQuery = "SELECT ""U_Z_HREmpID"", ""U_Z_HREmpName"", ""U_Z_DeptName"", T0.""U_Z_TrainCode"", T1.""U_Z_CourseCode"", T1.""U_Z_CourseName"", CASE T0.""U_Z_AppStatus"" WHEN 'A' THEN 'Approved' WHEN 'R' THEN 'Rejected' WHEN 'P' THEN 'Pending' END AS ""U_Z_AppStatus"",""U_Z_MgrRegRemarks"", ""Code"" FROM ""@Z_HR_TRIN1"" T0 LEFT OUTER JOIN ""@Z_HR_OTRIN"" T1 ON T0.""U_Z_TrainCode"" = T1.""U_Z_TrainCode"" WHERE ""U_Z_HRRegStatus"" = 'P' and " & objen.EmpCondition & ";"
            objDA.strQuery += " select DocEntry,convert(varchar(10),U_Z_ReqDate,103) AS U_Z_ReqDate,U_Z_HREmpID,U_Z_HREmpName,U_Z_DeptName,U_Z_PosiName,U_Z_CourseName,U_Z_CourseDetails,convert(varchar(10),U_Z_TrainFrdt,103) as U_Z_TrainFrdt,convert(varchar(10),U_Z_TrainTodt,103) as U_Z_TrainTodt,CAST(isnull(U_Z_TrainCost,0) as decimal(10,2)) AS U_Z_TrainCost,U_Z_Notes,"
            objDA.strQuery += " case U_Z_AppStatus when 'P' then 'Pending' when 'A' then 'Approved' when 'R' then 'Rejected' end as U_Z_AppStatus  from [@Z_HR_ONTREQ] where " & objen.EmpCondition1 & ";"
            objDA.strQuery += "SELECT ""U_DocEntry"", ""U_Empid"", ""U_Empname"", ""U_EmpPosCode"", ""U_EmpPosName"", ""U_EmpdeptCode"", ""U_EmpdeptName"", ""U_ReqdeptCode"", ""U_ReqdeptName"", ""U_ReqPosCode"", ""U_Remarks"", ""U_ReqPosName"", ""U_RequestCode"", ""U_ApplyDate"", CASE ""U_Z_AppStatus"" WHEN 'A' THEN 'Approved' WHEN 'R' THEN 'Rejected' WHEN 'P' THEN 'Pending' END AS ""U_Z_AppStatus"" FROM ""U_VACPOSITION"" WHERE ""U_Z_AppStatus"" <> 'A' and " & objen.EmpCondition2 & ";"
            objDA.strQuery += "SELECT ""U_DocEntry"", ""U_Empid"", ""U_PeoobjCode"", ""U_PeoobjName"", T1.""U_Z_CatName"", T0.""U_PeoCategory"",CAST(isnull(U_Weight,0) as decimal(10,2)) AS ""U_Weight"", ""U_Remarks"", CASE ""U_Z_AppStatus"" WHEN 'A' THEN 'Approved' WHEN 'R' THEN 'Rejected' WHEN 'P' THEN 'Pending' END AS ""U_Z_AppStatus"", ""U_TypeAction"", ""U_RefNo"" FROM ""U_PEOPLEOBJ"" T0 LEFT OUTER JOIN ""@Z_HR_PECAT"" T1 ON T0.""U_PeoCategory"" = T1.""U_Z_CatCode""  where " & objen.EmpCondition3 & ";"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds1)
            Return objDA.ds1
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
