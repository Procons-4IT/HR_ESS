Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Odbc
Imports DataAccess
Imports EN
Public Class RetLeaveReqApprovalDA
    Dim objEN As RequestApprovalEN = New RequestApprovalEN()
    Dim objDA As DBConnectionDA = New DBConnectionDA()
    Public Sub New()
        objDA.con = New SqlConnection(objDA.GetConnection)
    End Sub
    Public Function PageloadbindLeaveApproval(ByVal objen As RequestApprovalEN) As DataSet
        Try
            objen.EmpCode = getEmpIDforMangers(objen)
            If objen.EmpCode <> "" Then
                objDA.strQuery1 = "	select ""U_Z_RStatus"",T0.""Code"" as ""Code"",""U_Z_EMPID"",""U_Z_EMPNAME"",""U_Z_TrnsCode"",T1.""Name"" as ""Name"",CAST(""U_Z_StartDate"" AS varchar(11)) AS ""U_Z_StartDate"",CAST(""U_Z_EndDate"" AS varchar(11)) AS ""U_Z_EndDate"",T0.""U_Z_NoofDays"" as ""U_Z_NoofDays"",CAST(""U_Z_RetJoiNDate"" AS varchar(11)) AS ""U_Z_RetJoiNDate"",""U_Z_RAppRemarks"","
                objDA.strQuery1 += """U_Z_Notes"",DateName( month , DateAdd( month , isnull(""U_Z_Month"",1) , -1 ) ) AS ""U_Z_Month"",isnull(""U_Z_Year"",0) as ""U_Z_Year"",CAST(""U_Z_ReJoiNDate"" AS varchar(11)) AS ""U_Z_ReJoiNDate"" from ""@Z_PAY_OLETRANS1"" T0 inner join ""@Z_PAY_LEAVE"" T1 on T0.""U_Z_TrnsCode""=T1.""Code"" where ""U_Z_TransType""='R' and ""U_Z_Status""='A' and ""U_Z_RStatus""='P' and  ""U_Z_EMPID"" in ( " & objen.EmpCode & ") Order by T0.""Code"" Desc;"
                objDA.strQuery1 += "select case ""U_Z_RStatus"" when 'A' then 'Approved' when 'P' then 'Pending' when 'R' then 'Rejected' end as ""U_Z_RStatus"" ,T0.""Code"" as ""Code"",""U_Z_EMPID"",""U_Z_EMPNAME"",""U_Z_TrnsCode"",T1.""Name"" as ""Name"",CAST(""U_Z_StartDate"" AS varchar(11)) AS ""U_Z_StartDate"",CAST(""U_Z_EndDate"" AS varchar(11)) AS ""U_Z_EndDate"",T0.""U_Z_NoofDays"" as ""U_Z_NoofDays"",CAST(""U_Z_RetJoiNDate"" AS varchar(11)) AS ""U_Z_RetJoiNDate"",""U_Z_RAppRemarks"","
                objDA.strQuery1 += """U_Z_Notes"", DateName( month , DateAdd( month , isnull(""U_Z_Month"",1) , -1 ) ) AS ""U_Z_Month"",""U_Z_Year"",CAST(""U_Z_ReJoiNDate"" AS varchar(11)) AS ""U_Z_ReJoiNDate"" from ""@Z_PAY_OLETRANS1"" T0 inner join ""@Z_PAY_LEAVE"" T1 on T0.""U_Z_TrnsCode""=T1.""Code"" where ""U_Z_TransType""='R' and ""U_Z_Status""='A' and ""U_Z_EmpId"" in (" & objen.EmpCode & ") Order by T0.""Code"" Desc;"
                objDA.sqlda = New SqlDataAdapter(objDA.strQuery1, objDA.con)
                objDA.sqlda.Fill(objDA.ds)
                Return objDA.ds
            End If
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
            strEmp = 0
        End If
        Return strEmp
    End Function
    Public Function LineMgrLverequestApproval(ByVal objen As RequestApprovalEN) As Boolean
        Try
            objDA.strQuery = "Update ""@Z_PAY_OLETRANS1"" set  ""U_Z_RStatus""='" & objen.Status & "',""U_Z_RApprovedBy""='" & objen.EmpCode & "',""U_Z_RApprDate""=getdate(),""U_Z_RAppRemarks""='" & objen.ApproveRemarks & "' where ""Code""='" & objen.Code & "'"
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


    Public Function BindSearchPageLoad(ByVal objen As RequestApprovalEN) As DataSet
        Try
            objen.EmpCode = getEmpIDforMangers(objen)
            objDA.strQuery1 = "select case ""U_Z_RStatus"" when 'A' then 'Approved' when 'P' then 'Pending' when 'R' then 'Rejected' end as ""U_Z_RStatus"" ,T0.""Code"" as ""Code"",""U_Z_EMPID"",""U_Z_EMPNAME"",""U_Z_TrnsCode"",T1.""Name"" as ""Name"",CAST(""U_Z_StartDate"" AS varchar(11)) AS ""U_Z_StartDate"",CAST(""U_Z_EndDate"" AS varchar(11)) AS ""U_Z_EndDate"",T0.""U_Z_NoofDays"" as ""U_Z_NoofDays"",CAST(""U_Z_RetJoiNDate"" AS varchar(11)) AS ""U_Z_RetJoiNDate"",""U_Z_RAppRemarks"","
            objDA.strQuery1 += """U_Z_Notes"",DateName( month , DateAdd( month , isnull(""U_Z_Month"",1) , -1 ) ) AS ""U_Z_Month"",""U_Z_Year"",CAST(""U_Z_ReJoiNDate"" AS varchar(11)) AS ""U_Z_ReJoiNDate"" from ""@Z_PAY_OLETRANS1"" T0 inner join ""@Z_PAY_LEAVE"" T1 on T0.""U_Z_TrnsCode""=T1.""Code"" where ""U_Z_TransType""='R' and ""U_Z_Status""='A' and ""U_Z_EmpId"" in (" & objen.EmpCode & ") and " & objen.TransType & " Order by T0.""Code"" Desc;"
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
            objDA.strQuery1 = "	select ""U_Z_RStatus"",T0.""Code"" as ""Code"",""U_Z_EMPID"",""U_Z_EMPNAME"",""U_Z_TrnsCode"",T1.""Name"" as ""Name"",CAST(""U_Z_StartDate"" AS varchar(11)) AS ""U_Z_StartDate"",CAST(""U_Z_EndDate"" AS varchar(11)) AS ""U_Z_EndDate"",T0.""U_Z_NoofDays"" as ""U_Z_NoofDays"",CAST(""U_Z_RetJoiNDate"" AS varchar(11)) AS ""U_Z_RetJoiNDate"",""U_Z_RAppRemarks"","
            objDA.strQuery1 += """U_Z_Notes"",DateName( month , DateAdd( month , isnull(""U_Z_Month"",1) , -1 ) ) AS ""U_Z_Month"",isnull(""U_Z_Year"",0) as ""U_Z_Year"",CAST(""U_Z_ReJoiNDate"" AS varchar(11)) AS ""U_Z_ReJoiNDate"" from ""@Z_PAY_OLETRANS1"" T0 inner join ""@Z_PAY_LEAVE"" T1 on T0.""U_Z_TrnsCode""=T1.""Code"" where ""U_Z_TransType""='R' and ""U_Z_Status""='A' and ""U_Z_RStatus""='P' and  ""U_Z_EMPID"" in ( " & objen.EmpCode & ") and " & objen.TransType & " Order by T0.""Code"" Desc;"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery1, objDA.con)
            objDA.sqlda.Fill(objDA.dss1)
            Return objDA.dss1
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PopupSearchBind(ByVal objen As RequestApprovalEN) As DataSet
        Try
            If objen.LeaveCode <> "" Then
                objDA.strQuery = "Select ""Code"",""Name"" from ""@Z_PAY_LEAVE""  where  ""Code""  like '%" + objen.LeaveCode + "%' "
            ElseIf objen.TransType <> "" Then
                objDA.strQuery = "Select ""Code"",""Name"" from ""@Z_PAY_LEAVE""  where  ""Name""  like '%" + objen.TransType + "%' "
            Else
                objDA.strQuery = "Select ""Code"",""Name"" from ""@Z_PAY_LEAVE"""
            End If
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.dss)
            Return objDA.dss
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
