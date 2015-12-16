Imports System
Imports System.Web.UI.WebControls
Imports System.Globalization
Imports System.Data
Imports System.Data.SqlClient
Imports EN
Public Class ClaimRequestDA
    Dim objen As ClaimRequestEN = New ClaimRequestEN()
    Dim objDA As DBConnectionDA = New DBConnectionDA()
    Dim strClim, stSubdt As String
    Dim dtClaim, dtsubdt As Date
    Public Sub New()
        objDA.con = New SqlConnection(objDA.GetConnection)
    End Sub
    Public Function PageLoadBind(ByVal objen As ClaimRequestEN) As DataSet
        Try
            objDA.strQuery = "SELECT T0.""Code"",T0.""U_Z_TAEmpID"", T0.""U_Z_EmpID"",T0.""U_Z_EmpName"",convert(Varchar(10),T0.""U_Z_Subdt"",103) AS ""U_Z_Subdt"", T0.""U_Z_Client"", T0.""U_Z_Project"",Case T0.""U_Z_DocStatus"" when 'C' then 'Closed' else 'Opened' end AS ""U_Z_DocStatus"""
            objDA.strQuery += " FROM ""@Z_HR_OEXPCL"" T0 WHERE T0.""U_Z_EmpID"" ='" & objen.EmpId & "' order by T0.""Code"" desc;"
            objDA.strQuery += "SELECT Code,[U_Z_ExpName],U_Z_AlloCode FROM [@Z_HR_EXPANCES]; "
            objDA.strQuery += "SELECT DocEntry,""U_Z_TraName"" from [@Z_HR_OTRAREQ]  where ""U_Z_AppStatus""='A' and ""U_Z_EmpId""='" & objen.EmpId & "';"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds)
            Return objDA.ds
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            Throw ex
        End Try
    End Function
    Public Function DropDownBind() As DataSet
        Try
            objDA.strQuery = "SELECT ""CurrCode"" As ""Code"", ""CurrName"" As ""Name"" FROM ""OCRN""; "
            objDA.strQuery += "SELECT ""Code"" As ""Code"", ""U_Z_PayMethod"" As ""Name"" FROM ""@Z_HR_PAYMD"";"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds1)
            Return objDA.ds1
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            Throw ex
        End Try
    End Function
    Public Function AllowanceCode(ByVal objen As ClaimRequestEN) As DataSet
        Try
            objDA.strQuery = "SELECT isnull(U_Z_AlloCode,'') AS U_Z_AlloCode,isnull(U_Z_ActCode,'') AS U_Z_ActCode,isnull(U_Z_DebitCode,'') AS U_Z_DebitCode,isnull(U_Z_Posting,'P') AS U_Z_Posting, CAST(isnull(U_Z_Amount,0) as decimal(10,2)) AS  U_Z_Amount,isnull(U_Z_OverLap,'N') AS U_Z_OverLap FROM [@Z_HR_EXPANCES] where Code='" & objen.DocEntry & "'; "
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds4)
            Return objDA.ds4
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            Throw ex
        End Try
    End Function
    Public Function LocalCurrency(ByVal objen As ClaimRequestEN) As String
        Try
            objen.LocalCurrency = objen.SapCompany.GetCompanyService.GetAdminInfo.LocalCurrency
            Return objen.LocalCurrency
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            Throw ex
        End Try
    End Function
    Public Function TargetPath() As String
        Try

            objDA.strQuery = "select AttachPath from OADP"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.dss1)
            If objDA.dss1.Tables(0).Rows.Count > 0 Then
                objen.TravelCode = objDA.dss1.Tables(0).Rows(0)(0).ToString()
            End If
            Return objen.TravelCode
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            Throw ex
        End Try
    End Function

    Public Function PopulateRequest(ByVal objen As ClaimRequestEN) As DataSet
        Try
            objDA.strQuery = "select ""Code"",""Name"",""U_Z_ExpCode"",convert(varchar(10),""U_Z_Subdt"",103) AS ""U_Z_Subdt"",""U_Z_TripType"",""U_Z_TraCode"",""U_Z_TraDesc"",""U_Z_ExpType"",""U_Z_AlloCode"",""U_Z_Client"",""U_Z_Project"",Convert(varchar(10),""U_Z_Claimdt"",103) AS ""U_Z_Claimdt"",""U_Z_City"",""U_Z_Currency"",Cast(isnull(U_Z_CurAmt,0) as decimal(10,2)) AS ""U_Z_CurAmt"",""U_Z_ExcRate"",U_Z_EmpID,U_Z_EmpName,"
            objDA.strQuery += """U_Z_UsdAmt"",""U_Z_Reimburse"",""U_Z_ReimAmt"",""U_Z_PayMethod"",""U_Z_Notes"",""U_Z_AppStatus"",""U_Z_PayPosted"",isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_CurApprover ),'') AS 'U_Z_CurApprover',isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_NxtApprover ),'') AS 'U_Z_NxtApprover'  from ""@Z_HR_EXPCL"" T0   where ""U_Z_EmpID""='" & objen.EmpId & "' AND ""Code""='" & objen.DocEntry & "';"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds1)
            Return objDA.ds1
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            Throw ex
        End Try
    End Function
    Public Function DeleteRequest(ByVal objen As ClaimRequestEN) As String
        Try
            objDA.strQuery = "Delete from ""@Z_HR_EXPCL"" where ""Code""='" & objen.DocEntry & "' and ""U_Z_EmpID""=" & objen.EmpId & ""
            objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
            objDA.con.Open()
            objDA.cmd.ExecuteNonQuery()
            objDA.con.Close()
            objDA.strmsg = "Expenses Claim withdraw successfully...."
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            objDA.strmsg = ex.Message
        End Try
        Return objDA.strmsg
    End Function

    Public Function PopulateTANo(ByVal EmpId As String) As String
        Try
            objDA.strQuery = "Select isnull(ExtEmpNo,'') AS U_Z_EmpID from OHEM where empID='" & EmpId & "'"
            objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
            objDA.con.Open()
            objen.AllowanceCode = objDA.cmd.ExecuteScalar
            objDA.con.Close()
            Return objen.AllowanceCode
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            Throw ex
        End Try
    End Function
    Public Function GetCardCode(ByVal EmpId As String) As String
        Dim strCardCode As String = ""
        Try
            objDA.strQuery = "Select isnull(U_Z_CardCode,'') from OHEM where empID='" & EmpId & "'"
            objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
            objDA.con.Open()
            strCardCode = objDA.cmd.ExecuteScalar
            objDA.con.Close()
            Return strCardCode
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            Throw ex
        End Try
    End Function
    Public Function NewRequestBind(ByVal objen As ClaimRequestEN) As DataSet
        Try
            objDA.strQuery = "select U_Code,U_DocEntry,Convert(Varchar(10),U_ClimDate,103) AS U_ClimDate,Case U_TripType When 'N' then 'New' when 'E' then 'Existing' end AS U_TripType,"
            objDA.strQuery += " U_TraCode, U_TraDesc,U_City,U_Currency,cast(U_CurAmt as decimal(10,2)) AS U_CurAmt,cast(U_ExcRate as decimal(10,2)) AS U_ExcRate,U_UsdAmt,Case U_ReImbused when 'Y' then 'Yes' when 'N' then 'No' end as U_ReImbused,U_ReImAmt,U_ExpCode,U_ExpName,"
            objDA.strQuery += " U_AllCode,U_PayMethod, U_Notes,case U_AppStatus when 'P' then 'Pending' when 'A' then 'Approved' when 'R' then 'Rejected' end AS U_AppStatus,U_Attachment,U_Year,U_Month,U_DocRefNo,isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_CurApprover ),'') AS 'U_CurApprover',isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_NxtApprover ),'') AS 'U_NxtApprover' from ""U_EXPCLAIM"" T0   where ""U_SessionId""='" & objen.SessionID & "' AND ""U_Empid""='" & objen.EmpId & "';"
            objDA.strQuery += "select T0.""Code"",T0.""Name"",""U_Z_ExpCode"", T0.""U_Z_Subdt"",Case T0.""U_Z_TripType"" when 'N' then 'New' when 'E' then 'Existing' end as ""U_Z_TripType"",T0.""U_Z_TraCode"",T0.""U_Z_TraDesc"",""U_Z_ExpType"",""U_Z_AlloCode"",T0.""U_Z_Client"",T0.""U_Z_Project"",Convert(Varchar(10),U_Z_Claimdt,103) AS ""U_Z_Claimdt"",""U_Z_City"",""U_Z_Currency"",cast(U_Z_CurAmt as decimal(10,2)) AS ""U_Z_CurAmt"",cast(U_Z_ExcRate as decimal(10,2)) AS ""U_Z_ExcRate"","
            objDA.strQuery += """U_Z_UsdAmt"",Case U_Z_Reimburse when 'Y' then 'Yes' when 'N' then 'No' end as ""U_Z_Reimburse"",""U_Z_ReimAmt"",""U_Z_PayMethod"",""U_Z_Notes"",""U_Z_Attachment"",isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_CurApprover ),'') AS 'U_Z_CurApprover',isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_NxtApprover ),'') AS 'U_Z_NxtApprover',case ""U_Z_AppStatus"" when 'P' then 'Pending' when 'A' then 'Approved' when 'R' then 'Rejected' end AS ""U_Z_AppStatus"",""U_Z_PayPosted""  from ""@Z_HR_EXPCL"" T0 Left join ""@Z_HR_OEXPCL"" T1 on T0.U_Z_DocRefNo=T1.Code  where T1.""U_Z_EmpID""='" & objen.EmpId & "' and T1.Code='" & objen.DocEntry & "' and ""U_Z_AppStatus""='A';"
            objDA.strQuery += "select T0.""Code"",T0.""Name"",""U_Z_ExpCode"", T0.""U_Z_Subdt"",Case T0.""U_Z_TripType"" when 'N' then 'New' when 'E' then 'Existing' end as ""U_Z_TripType"",T0.""U_Z_TraCode"",T0.""U_Z_TraDesc"",""U_Z_ExpType"",""U_Z_AlloCode"",T0.""U_Z_Client"",T0.""U_Z_Project"",Convert(Varchar(10),U_Z_Claimdt,103) AS ""U_Z_Claimdt"",""U_Z_City"",""U_Z_Currency"",cast(U_Z_CurAmt as decimal(10,2)) AS ""U_Z_CurAmt"",cast(U_Z_ExcRate as decimal(10,2)) AS ""U_Z_ExcRate"","
            objDA.strQuery += """U_Z_UsdAmt"",Case U_Z_Reimburse when 'Y' then 'Yes' when 'N' then 'No' end as ""U_Z_Reimburse"",""U_Z_ReimAmt"",""U_Z_PayMethod"",""U_Z_Notes"",""U_Z_Attachment"",isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_CurApprover ),'') AS 'U_Z_CurApprover',isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_NxtApprover ),'') AS 'U_Z_NxtApprover',case ""U_Z_AppStatus"" when 'P' then 'Pending' when 'A' then 'Approved' when 'R' then 'Rejected' end AS ""U_Z_AppStatus"",""U_Z_PayPosted""  from ""@Z_HR_EXPCL"" T0 Left join ""@Z_HR_OEXPCL"" T1 on T0.U_Z_DocRefNo=T1.Code  where T1.""U_Z_EmpID""='" & objen.EmpId & "' and T1.Code='" & objen.DocEntry & "' and ""U_Z_AppStatus""='R';"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds4)
            Return objDA.ds4
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            Throw ex
        End Try
    End Function
    Public Function DeleteTempTable(ByVal objen As ClaimRequestEN) As String
        Try
            objDA.strQuery = "Delete from ""U_EXPCLAIM""   where ""U_Empid""='" & objen.EmpId & "'"
            objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
            objDA.con.Open()
            objDA.cmd.ExecuteNonQuery()
            objDA.con.Close()
            objDA.strmsg = "Success"
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            objDA.strmsg = ex.Message
        End Try
        Return objDA.strmsg
    End Function
    Public Function PopulateExistingDocument(ByVal objen As ClaimRequestEN) As String
        Try
            objDA.strQuery = "Select Code,U_Z_EmpID,U_Z_Project,U_Z_TraDesc,U_Z_ExcRate,U_Z_ExpCode,U_Z_Notes,U_Z_Month,U_Z_Currency,U_Z_Reimburse,U_Z_AlloCode,U_Z_CardCode,U_Z_JVNo,"
            objDA.strQuery += "U_Z_EmpName,convert(varchar(10),U_Z_Claimdt,103) AS U_Z_Claimdt,U_Z_City,U_Z_UsdAmt,U_Z_ExpType,U_Z_AppStatus,U_Z_DocRefNo,U_Z_Attachment,"
            objDA.strQuery += " convert(varchar(10),U_Z_Subdt,103) AS U_Z_Subdt,U_Z_Client,U_Z_TripType,U_Z_TraCode,U_Z_CurAmt,U_Z_ReimAmt,U_Z_PayMethod,U_Z_Year,U_Z_DebitCode,U_Z_CreditCode,isnull(U_Z_Posting,'P') as U_Z_Posting,U_Z_Dimension,U_Z_CurApprover,U_Z_NxtApprover from ""@Z_HR_EXPCL""   where ""U_Z_DocRefNo""='" & objen.DocEntry & "' and ""U_Z_AppStatus""='P'"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.dss4)
            If objDA.dss4.Tables(0).Rows.Count > 0 Then
                For introw As Integer = 0 To objDA.dss4.Tables(0).Rows.Count - 1
                    strClim = objDA.dss4.Tables(0).Rows(introw)("U_Z_Claimdt").ToString()
                    If strClim <> "" Then
                        dtClaim = Date.ParseExact(strClim.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture)
                    Else
                        dtClaim = Now.Date
                    End If
                    stSubdt = objDA.dss4.Tables(0).Rows(introw)("U_Z_Subdt").ToString()
                    If stSubdt <> "" Then
                        dtsubdt = Date.ParseExact(stSubdt.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture)
                    Else
                        dtsubdt = Now.Date
                    End If
                    objDA.strQuery = "Insert Into [U_EXPCLAIM] (U_Code,U_SessionId,U_Empid,U_Empname,U_SubDate,U_Client,U_Project,U_ClimDate,U_TripType,U_TraCode,"
                    objDA.strQuery += " U_TraDesc,U_City,U_Currency,U_CurAmt,U_ExcRate,U_UsdAmt,U_ReImbused,U_ReImAmt,U_ExpCode,U_ExpName,U_AllCode,U_PayMethod,"
                    objDA.strQuery += " U_Notes,U_AppStatus,U_Attachment,U_Year,U_Month,U_DocRefNo,U_DebitCode,U_CreditCode,U_Posting,U_CardCode,U_Dimension,U_JVNo,U_CurApprover,U_NxtApprover) Values ('" & objDA.dss4.Tables(0).Rows(introw)("Code").ToString() & "','" & objen.SessionID & "',"
                    objDA.strQuery += " '" & objDA.dss4.Tables(0).Rows(introw)("U_Z_EmpID").ToString() & "','" & objDA.dss4.Tables(0).Rows(introw)("U_Z_EmpName").ToString() & "','" & dtsubdt.ToString("yyyy-MM-dd") & "','" & objDA.dss4.Tables(0).Rows(introw)("U_Z_Client").ToString() & "', "
                    objDA.strQuery += " '" & objDA.dss4.Tables(0).Rows(introw)("U_Z_Project").ToString() & "','" & dtClaim.ToString("yyyy-MM-dd") & "','" & objDA.dss4.Tables(0).Rows(introw)("U_Z_TripType").ToString() & "','" & objDA.dss4.Tables(0).Rows(introw)("U_Z_TraCode").ToString() & "', "
                    objDA.strQuery += " '" & objDA.dss4.Tables(0).Rows(introw)("U_Z_TraDesc").ToString() & "','" & objDA.dss4.Tables(0).Rows(introw)("U_Z_City").ToString() & "','" & objDA.dss4.Tables(0).Rows(introw)("U_Z_Currency").ToString() & "'," & objDA.dss4.Tables(0).Rows(introw)("U_Z_CurAmt").ToString() & ","
                    objDA.strQuery += " '" & objDA.dss4.Tables(0).Rows(introw)("U_Z_ExcRate").ToString() & "','" & objDA.dss4.Tables(0).Rows(introw)("U_Z_UsdAmt").ToString() & "','" & objDA.dss4.Tables(0).Rows(introw)("U_Z_Reimburse").ToString() & "','" & objDA.dss4.Tables(0).Rows(introw)("U_Z_ReimAmt").ToString() & "', "
                    objDA.strQuery += " '" & objDA.dss4.Tables(0).Rows(introw)("U_Z_ExpCode").ToString() & "','" & objDA.dss4.Tables(0).Rows(introw)("U_Z_ExpType").ToString() & "','" & objDA.dss4.Tables(0).Rows(introw)("U_Z_AlloCode").ToString() & "','" & objDA.dss4.Tables(0).Rows(introw)("U_Z_PayMethod").ToString() & "',"
                    objDA.strQuery += " '" & objDA.dss4.Tables(0).Rows(introw)("U_Z_Notes").ToString() & "','" & objDA.dss4.Tables(0).Rows(introw)("U_Z_AppStatus").ToString() & "','" & objDA.dss4.Tables(0).Rows(introw)("U_Z_Attachment").ToString() & "'," & objDA.dss4.Tables(0).Rows(introw)("U_Z_Year").ToString() & ", "
                    objDA.strQuery += " " & objDA.dss4.Tables(0).Rows(introw)("U_Z_Month").ToString() & ",'" & objDA.dss4.Tables(0).Rows(introw)("U_Z_DocRefNo").ToString() & "', "
                    objDA.strQuery += " '" & objDA.dss4.Tables(0).Rows(introw)("U_Z_DebitCode").ToString() & "','" & objDA.dss4.Tables(0).Rows(introw)("U_Z_CreditCode").ToString() & "', "
                    objDA.strQuery += " '" & objDA.dss4.Tables(0).Rows(introw)("U_Z_Posting").ToString().Trim() & "','" & objDA.dss4.Tables(0).Rows(introw)("U_Z_CardCode").ToString() & "','" & objDA.dss4.Tables(0).Rows(introw)("U_Z_Dimension").ToString() & "','" & objDA.dss4.Tables(0).Rows(introw)("U_Z_JVNo").ToString() & "',"
                    objDA.strQuery += " '" & objDA.dss4.Tables(0).Rows(introw)("U_Z_CurApprover").ToString().Trim() & "','" & objDA.dss4.Tables(0).Rows(introw)("U_Z_NxtApprover").ToString() & "' ) "

                    objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
                    objDA.con.Open()
                    objDA.cmd.ExecuteNonQuery()
                    objDA.con.Close()
                    objDA.strmsg = "Success"
                Next
            End If
            objDA.strmsg = "Success"
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            objDA.strmsg = ex.Message
        End Try
        Return objDA.strmsg
    End Function
    Public Function PopulateHeader(ByVal objen As ClaimRequestEN) As DataSet
        Try
            objDA.strQuery = "select convert(varchar(10),T0.U_Z_Subdt,103) AS U_Z_Subdt,T0.U_Z_Project,T0.U_Z_Client,isnull(T1.U_Z_TripType,'N') as U_Z_TripType,"
            objDA.strQuery += " T1.""U_Z_TraCode"",T1.""U_Z_TraDesc"",isnull(T0.U_Z_DocStatus,'O') as U_Z_DocStatus,T0.U_Z_CardCode from ""@Z_HR_OEXPCL"" T0 Left Join ""@Z_HR_EXPCL"" T1 "
            objDA.strQuery += " ON T0.Code=T1.U_Z_DocRefNo where T0.""Code""='" & objen.DocEntry & "'"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.dss3)
            Return objDA.dss3
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            Throw ex
        End Try
    End Function
    Public Function DeleteExpenses(ByVal objen As ClaimRequestEN) As String
        Try
            If objen.TravelCode <> "" Then
                objDA.strQuery = "Update ""@Z_HR_EXPCL"" set ""Name"" =""Name"" +'D'  where ""Code""='" & objen.TravelCode & "'"
                objDA.strQuery += "Delete from ""U_EXPCLAIM""   where ""U_Code""='" & objen.TravelCode & "';"
            Else
                objDA.strQuery = "Delete from ""U_EXPCLAIM""   where ""U_DocEntry""='" & objen.DocEntry & "'"
            End If
            objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
            objDA.con.Open()
            objDA.cmd.ExecuteNonQuery()
            objDA.con.Close()
            objDA.strmsg = "Success"
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            objDA.strmsg = ex.Message
        End Try
        Return objDA.strmsg
    End Function
    Public Function BindDistriRule() As DataSet
        Try
            objDA.strQuery = "Select * from ODIM where DimActive='Y' "
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.dss2)
            Return objDA.dss2
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            Throw ex
        End Try
    End Function
    Public Function BindDistriRule1(ByVal EmpId As String) As String
        Dim StrDimension As String = ""
        Try
            objDA.strQuery = "Select isnull(U_Z_Cost,'') +';'+isnull(U_Z_Dept,'') +';'+isnull(U_Z_Dim3,'') +';'+ isnull(U_Z_HRCost,'') From OHEM where empID=" & EmpId
            objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
            objDA.con.Open()
            StrDimension = objDA.cmd.ExecuteScalar()
            objDA.con.Close()
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            Throw ex
        End Try
        Return StrDimension
    End Function
    Public Function GetExcRate(ByVal TransCur As String, ByVal TrsDate As Date) As Double
        Dim dblExcRate As String = 0.0
        Try
            objDA.strQuery = "Select isnull(Rate,'')  from ORTT where Currency='" & TransCur.Trim & "' and RateDate='" & TrsDate.ToString("yyyy-MM-dd") & "'"
            objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
            objDA.con.Open()
            dblExcRate = objDA.cmd.ExecuteScalar()
            If dblExcRate <= 0 Then
                dblExcRate = 1
            End If
            objDA.con.Close()
        Catch ex As Exception
            DBConnectionDA.WriteError(ex.Message)
            Throw ex
        End Try
        Return dblExcRate
    End Function
End Class
