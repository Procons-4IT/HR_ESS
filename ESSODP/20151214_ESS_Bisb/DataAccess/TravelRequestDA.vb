Imports System
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Data.SqlClient
Imports EN
Public Class TravelRequestDA
    Dim objen As TravelRequestEN = New TravelRequestEN()
    Dim objDA As DBConnectionDA = New DBConnectionDA()
    Public Sub New()
        objDA.con = New SqlConnection(objDA.GetConnection)
    End Sub
    Public Function PageLoadBind(ByVal objen As TravelRequestEN) As DataSet
        Try
            objDA.strQuery = "select ""Code"",""U_Z_TraCode""  from ""@Z_HR_OASSTP"" where ""U_Z_EmpId""=" & objen.EmpId & ";"
            objDA.strQuery += "SELECT ""DocEntry"", Convert(varchar(10),""U_Z_DocDate"",103) AS ""U_Z_DocDate"", ""U_Z_EmpId"", ""U_Z_EmpName"", ""U_Z_DeptName"", ""U_Z_PosName"", ""U_Z_TraName"", ""U_Z_TraStLoc"", ""U_Z_TraEdLoc"", Convert(varchar(10),""U_Z_TraStDate"",103) AS ""U_Z_TraStDate"", Convert(varchar(10),""U_Z_TraEndDate"",103) AS ""U_Z_TraEndDate"","
            objDA.strQuery += " CASE ""U_Z_AppStatus"" WHEN 'P' THEN 'Pending' WHEN 'A' THEN 'Approved' WHEN 'R' THEN 'Rejected' END AS ""U_Z_AppStatus"",isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_CurApprover ),'') AS 'U_Z_CurApprover',isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_NxtApprover ),'') AS 'U_Z_NxtApprover'"
            objDA.strQuery += " FROM ""@Z_HR_OTRAREQ"" T0 where ""U_Z_EmpId""=" & objen.EmpId & " Order by ""DocEntry"" Desc;"
            objDA.strQuery += "Select * from OHEM T0 left Join OHPS T1 on T0.""position""=T1.""posID"" where ""empID""=" & objen.EmpId & ";"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds)
            Return objDA.ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Department(ByVal objen As TravelRequestEN) As String
        objDA.strQuery = "select Remarks from OUDP  where Code='" & objen.DeptCode & "'"
        objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
        objDA.sqlda.Fill(objDA.dss)
        If objDA.dss.Tables(0).Rows.Count <> 0 Then
            objen.DeptName = objDA.dss.Tables(0).Rows(0)("Remarks").ToString()
        End If
        Return objen.DeptName
    End Function
    Public Function BindTravelName(ByVal objen As TravelRequestEN) As String
        objDA.strQuery = "Select ""U_Z_TraName"" from ""@Z_HR_OTRAPL"" where ""U_Z_TraCode""='" & objen.TripCode & "'"
        objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
        objDA.sqlda.Fill(objDA.ds1)
        If objDA.ds1.Tables(0).Rows.Count <> 0 Then
            objen.TripName = objDA.ds1.Tables(0).Rows(0)("U_Z_TraName").ToString()
        End If
        Return objen.TripName
    End Function
    Public Function ExpensesBind(ByVal objen As TravelRequestEN) As DataSet
        Try
            objDA.strQuery = "select ""U_Z_ExpName"",""U_Z_Amount"",""U_Z_UtilAmt"",""U_Z_BalAmount""  from ""@Z_HR_ASSTP1"" where ""U_Z_TraCode""='" & objen.TripCode & "' and ""U_Z_RefCode""='" & objen.TripName & "'"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds2)
            Return objDA.ds2
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PopulateRequest(ByVal objen As TravelRequestEN) As DataSet
        Try
            objDA.strQuery = "SELECT ""DocEntry"", Convert(varchar(10),""U_Z_DocDate"",103 ) AS ""U_Z_DocDate"", ""U_Z_EmpId"", ""U_Z_EmpName"", ""U_Z_DeptName"", ""U_Z_PosName"", ""U_Z_TraName"", ""U_Z_TraStLoc"", ""U_Z_TraEdLoc"", ""U_Z_EmpComme"", ""U_Z_PosCode"", ""U_Z_DeptId"", ""U_Z_TraCode"", Convert(varchar(10),""U_Z_TraStDate"",103) AS ""U_Z_TraStDate"", Convert(varchar(10),""U_Z_TraEndDate"",103) AS ""U_Z_TraEndDate"", Convert(varchar(10),""U_Z_ReqAppDate"",103) AS ""U_Z_ReqAppDate"", Convert(varchar(10),""U_Z_ReqClaimDate"",103) AS ""U_Z_ReqClaimDate"", Convert(varchar(10),""U_Z_AppClaimDate"",103) AS ""U_Z_AppClaimDate"",isnull(""U_Z_AppStatus"",'P') as ""U_Z_AppStatus"",U_Z_NewReq FROM ""@Z_HR_OTRAREQ"" where ""U_Z_EmpId""='" & objen.EmpId & "' and ""DocEntry""='" & objen.DocEntry & "'"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds2)
            Return objDA.ds2
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Expenses(ByVal objen As TravelRequestEN) As DataSet
        Try
            objDA.strQuery = "select ""U_Z_ExpName"",""U_Z_Amount"",""U_Z_UtilAmt"",""U_Z_BalAmount""  from ""@Z_HR_TRAREQ1"" where ""DocEntry""='" & objen.DocEntry & "'"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds)
            Return objDA.ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function UpdateTravelRequest(ByVal objen As TravelRequestEN) As Boolean
        Try
            objDA.strQuery = "select *  from ""@Z_HR_TRAREQ1"" where ""DocEntry""='" & objen.DocEntry & "'"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds3)
            If objDA.ds3.Tables(0).Rows.Count > 0 Then
                objDA.strQuery = "delete from ""@Z_HR_TRAREQ1"" where ""DocEntry""='" & objen.DocEntry & "'"
                objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
                objDA.con.Open()
                objDA.cmd.ExecuteNonQuery()
                objDA.con.Close()
                Return True
            End If
            Return True
        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function
    Public Function WithdrawRequest(ByVal objen As TravelRequestEN) As Boolean
        Try
            objDA.strQuery = "Delete from ""@Z_HR_OTRAREQ"" where ""DocEntry""='" & objen.DocEntry & "' and  ""U_Z_EmpId""='" & objen.EmpId & "'"
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
