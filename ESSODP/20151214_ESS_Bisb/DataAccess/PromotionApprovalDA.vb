Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports DataAccess
Imports EN
Public Class PromotionApprovalDA
    Dim objEN As PromotionApprovalEN = New PromotionApprovalEN()
    Dim objDA As DBConnectionDA = New DBConnectionDA()
    Public Sub New()
        objDA.con = New SqlConnection(objDA.GetConnection)
    End Sub
    Public Function Pageloadbind(ByVal objen As PromotionApprovalEN) As DataSet
        Try
            objen.EmpCode = getEmpIDforMangers(objen)
            objDA.strQuery1 = "	select ""U_Z_Status"",  ""Code"",""U_Z_EmpId"",""U_Z_FirstName"",""U_Z_DeptName"",""U_Z_PosName"",""U_Z_JobName"",""U_Z_OrgName"","
            objDA.strQuery1 += "CAST(""U_Z_ProJoinDate"" AS varchar(11)) AS ""U_Z_ProJoinDate"",CONVERT(decimal(10,2),""U_Z_IncAmount"") as ""U_Z_IncAmount"",CAST(""U_Z_EffFromdt"" AS varchar(11)) AS ""U_Z_EffFromdt"",CAST(""U_Z_EffTodt"" AS varchar(11)) AS ""U_Z_EffTodt"" from ""@Z_HR_HEM2"" where ""U_Z_Posting""='N' and  ""U_Z_EmpId"" in ( " & objen.EmpCode & ") Order by ""Code"" Desc;"
            objDA.strQuery1 += "select case ""U_Z_Status"" when 'A' then 'Approved' when 'P' then 'Pending' when 'C' then 'Cancelled' end as ""U_Z_Status"" , ""Code"",""U_Z_EmpId"",""U_Z_FirstName"",""U_Z_DeptName"",""U_Z_PosName"",""U_Z_JobName"",""U_Z_OrgName"","
            objDA.strQuery1 += "CAST(""U_Z_ProJoinDate"" AS varchar(11)) AS ""U_Z_ProJoinDate"",CONVERT(decimal(10,2),""U_Z_IncAmount"") as ""U_Z_IncAmount"",CAST(""U_Z_EffFromdt"" AS varchar(11)) AS ""U_Z_EffFromdt"",CAST(""U_Z_EffTodt"" AS varchar(11)) AS ""U_Z_EffTodt""  from ""@Z_HR_HEM2"" where  ""U_Z_EmpId"" in (" & objen.EmpCode & ") Order by ""Code"" Desc;"
            objDA.strQuery1 += "Select ""Code"",""Name"" from OUDP order by ""Code"";"
            objDA.strQuery1 += "select ""name"",""descriptio"" from OHPS ;"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery1, objDA.con)
            objDA.sqlda.Fill(objDA.ds)
            Return objDA.ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function getEmpIDforMangers(ByVal objen As PromotionApprovalEN) As String
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
    Public Function LineMgrPromotionApproval(ByVal objen As PromotionApprovalEN) As Boolean
        Try
            objDA.strQuery = "Update ""@Z_HR_HEM2"" set  ""U_Z_Status""='" & objen.Status & "' where ""Code""='" & objen.Code & "'"
            objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
            objDA.con.Open()
            objDA.cmd.ExecuteNonQuery()
            objDA.con.Close()
            Return True
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Function PageloadbindPosChange(ByVal objen As PromotionApprovalEN) As DataSet
        Try
            objen.EmpCode = getEmpIDforMangers(objen)
            objDA.strQuery1 = "select ""U_Z_Status"", ""Code"",""U_Z_EmpId"",""U_Z_FirstName"",""U_Z_DeptName"",""U_Z_PosName"",""U_Z_JobName"",""U_Z_OrgCode"",""U_Z_OrgName"","
            objDA.strQuery1 += "CAST(""U_Z_NewPosDate"" AS varchar(11)) AS ""U_Z_NewPosDate"",CAST(""U_Z_EffFromdt"" AS varchar(11)) AS ""U_Z_EffFromdt"",CAST(""U_Z_EffTodt"" AS varchar(11)) AS ""U_Z_EffTodt"" from ""@Z_HR_HEM4"" where ""U_Z_Posting""='N' and  ""U_Z_EmpId"" in (" & objen.EmpCode & ") Order by ""Code"" Desc;"
            objDA.strQuery1 += "select case ""U_Z_Status"" when 'A' then 'Approved' when 'P' then 'Pending' when 'C' then 'Cancelled' end as ""U_Z_Status"" , ""Code"",""U_Z_EmpId"",""U_Z_FirstName"",""U_Z_DeptName"",""U_Z_PosName"",""U_Z_JobName"",""U_Z_OrgName"","
            objDA.strQuery1 += "CAST(""U_Z_NewPosDate"" AS varchar(11)) AS ""U_Z_NewPosDate"",CAST(""U_Z_EffFromdt"" AS varchar(11)) AS ""U_Z_EffFromdt"",CAST(""U_Z_EffTodt"" AS varchar(11)) AS ""U_Z_EffTodt""  from ""@Z_HR_HEM4"" where ""U_Z_EmpId"" in (" & objen.EmpCode & ") Order by ""Code"" Desc;"
            objDA.strQuery1 += "Select ""Code"",""Name"" from OUDP order by ""Code"";"
            objDA.strQuery1 += "select ""name"",""descriptio"" from OHPS ;"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery1, objDA.con)
            objDA.sqlda.Fill(objDA.ds)
            Return objDA.ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function LineMgrPositionApproval(ByVal objen As PromotionApprovalEN) As Boolean
        Try
            objDA.strQuery = "Update ""@Z_HR_HEM4"" set  ""U_Z_Status""='" & objen.Status & "' where ""Code""='" & objen.Code & "'"
            objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
            objDA.con.Open()
            objDA.cmd.ExecuteNonQuery()
            objDA.con.Close()
            Return True
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Function BindSearchPageLoad(ByVal objen As PromotionApprovalEN) As DataSet
        Try
            objen.EmpCode = getEmpIDforMangers(objen)
            objDA.strQuery1 = "	select ""U_Z_Status"",  ""Code"",""U_Z_EmpId"",""U_Z_FirstName"",""U_Z_DeptName"",""U_Z_PosName"",""U_Z_JobName"",""U_Z_OrgName"","
            objDA.strQuery1 += "CAST(""U_Z_ProJoinDate"" AS varchar(11)) AS ""U_Z_ProJoinDate"",CONVERT(decimal(10,2),""U_Z_IncAmount"") as ""U_Z_IncAmount"",CAST(""U_Z_EffFromdt"" AS varchar(11)) AS ""U_Z_EffFromdt"",CAST(""U_Z_EffTodt"" AS varchar(11)) AS ""U_Z_EffTodt"" from ""@Z_HR_HEM2"" where ""U_Z_Posting""='N' and  ""U_Z_EmpId"" in ( " & objen.EmpCode & ") and " & objen.EmpCondition & " Order by ""Code"" Desc;"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery1, objDA.con)
            objDA.sqlda.Fill(objDA.dss1)
            Return objDA.dss1
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function BindSearchPageLoadSum(ByVal objen As PromotionApprovalEN) As DataSet
        Try
            objen.EmpCode = getEmpIDforMangers(objen)
            objDA.strQuery1 = "select case ""U_Z_Status"" when 'A' then 'Approved' when 'P' then 'Pending' when 'C' then 'Cancelled' end as ""U_Z_Status"" , ""Code"",""U_Z_EmpId"",""U_Z_FirstName"",""U_Z_DeptName"",""U_Z_PosName"",""U_Z_JobName"",""U_Z_OrgName"","
            objDA.strQuery1 += "CAST(""U_Z_ProJoinDate"" AS varchar(11)) AS ""U_Z_ProJoinDate"",CONVERT(decimal(10,2),""U_Z_IncAmount"") as ""U_Z_IncAmount"",CAST(""U_Z_EffFromdt"" AS varchar(11)) AS ""U_Z_EffFromdt"",CAST(""U_Z_EffTodt"" AS varchar(11)) AS ""U_Z_EffTodt""  from ""@Z_HR_HEM2"" where  ""U_Z_EmpId"" in (" & objen.EmpCode & ") and " & objen.EmpCondition & " Order by ""Code"" Desc;"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery1, objDA.con)
            objDA.sqlda.Fill(objDA.dss3)
            Return objDA.dss3
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function PageloadSearchbindPosChange(ByVal objen As PromotionApprovalEN) As DataSet
        Try
            objen.EmpCode = getEmpIDforMangers(objen)
            objDA.strQuery1 = "select ""U_Z_Status"", ""Code"",""U_Z_EmpId"",""U_Z_FirstName"",""U_Z_DeptName"",""U_Z_PosName"",""U_Z_JobName"",""U_Z_OrgCode"",""U_Z_OrgName"","
            objDA.strQuery1 += "CAST(""U_Z_NewPosDate"" AS varchar(11)) AS ""U_Z_NewPosDate"",CAST(""U_Z_EffFromdt"" AS varchar(11)) AS ""U_Z_EffFromdt"",CAST(""U_Z_EffTodt"" AS varchar(11)) AS ""U_Z_EffTodt"" from ""@Z_HR_HEM4"" where ""U_Z_Posting""='N' and  ""U_Z_EmpId"" in (" & objen.EmpCode & ") and " & objen.EmpCondition & " Order by ""Code"" Desc;"
            objDA.strQuery1 += "select case ""U_Z_Status"" when 'A' then 'Approved' when 'P' then 'Pending' when 'C' then 'Cancelled' end as ""U_Z_Status"" , ""Code"",""U_Z_EmpId"",""U_Z_FirstName"",""U_Z_DeptName"",""U_Z_PosName"",""U_Z_JobName"",""U_Z_OrgName"","
            objDA.strQuery1 += "CAST(""U_Z_NewPosDate"" AS varchar(11)) AS ""U_Z_NewPosDate"",CAST(""U_Z_EffFromdt"" AS varchar(11)) AS ""U_Z_EffFromdt"",CAST(""U_Z_EffTodt"" AS varchar(11)) AS ""U_Z_EffTodt""  from ""@Z_HR_HEM4"" where ""U_Z_EmpId"" in (" & objen.EmpCode & ") and " & objen.EmpCondition & " Order by ""Code"" Desc;"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery1, objDA.con)
            objDA.sqlda.Fill(objDA.dss2)
            Return objDA.dss2
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PopupSearchBind(ByVal objen As PromotionApprovalEN) As DataSet
        Try
            If objen.Department <> "" Then
                objDA.strQuery = "Select ""Code"",""Name"" from OUDP  where  ""Name""  like '%" + objen.Department + "%' "
            Else
                objDA.strQuery = "Select ""Code"",""Name"" from OUDP"
            End If
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.dss)
            Return objDA.dss
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PopupSearchBindPos(ByVal objen As PromotionApprovalEN) As DataSet
        Try
            If objen.Department <> "" Then
                objDA.strQuery = "select ""name"",""descriptio"" from OHPS  where  ""descriptio""  like '%" + objen.Department + "%' "
            Else
                objDA.strQuery = "select ""name"",""descriptio"" from OHPS"
            End If
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds1)
            Return objDA.ds1
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
