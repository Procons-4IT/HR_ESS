Imports Microsoft.VisualBasic
Imports System
Imports System.Web
Imports System.Xml
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports EN
Public Class LeaveRequestDA
    Dim objen As LeaveRequestEN = New LeaveRequestEN()
    Dim objDA As DBConnectionDA = New DBConnectionDA()
    Public Sub New()
        objDA.con = New SqlConnection(objDA.GetConnection)
    End Sub
    Public Function PageLoadBind(ByVal objen As LeaveRequestEN) As DataSet
        Try
            objDA.strQuery = "Select T0.""Code"" as ""Code"",""U_Z_TrnsCode"",T1.""Name"" as ""Name"",convert(varchar(10),""U_Z_StartDate"",103) AS ""U_Z_StartDate"",convert(varchar(10),""U_Z_EndDate"",103) AS ""U_Z_EndDate"" ,cast(T0.U_Z_NoofDays as decimal(10,2)) AS ""U_Z_NoofDays"",""U_Z_Notes"",convert(varchar(10),""U_Z_ReJoiNDate"",103) AS ""U_Z_ReJoiNDate"",case ""U_Z_Status"" when 'P' then 'Pending' when 'R' then 'Rejected' when 'A' then 'Approved' end as ""U_Z_Status"",""U_Z_AppRemarks"",isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_CurApprover ),'') AS 'U_Z_CurApprover',isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_NxtApprover ),'') AS 'U_Z_NxtApprover' from ""@Z_PAY_OLETRANS1"" T0 inner join ""@Z_PAY_LEAVE"" T1 on T0.""U_Z_TrnsCode""=T1.""Code"" where ""U_Z_EMPID""='" & objen.Empid & "' and ""U_Z_TransType""='L' order by T0.""Code"" Desc;"
            objDA.strQuery += "Select T0.""Code"" as ""Code"",""U_Z_TrnsCode"",T1.""Name"" as ""Name"",convert(varchar(10),""U_Z_StartDate"",103) AS ""U_Z_StartDate"",convert(varchar(10),""U_Z_EndDate"",103) AS ""U_Z_EndDate"" ,cast(T0.U_Z_NoofDays as decimal(10,2)) AS ""U_Z_NoofDays"",""U_Z_Notes"" from ""@Z_PAY_OLETRANS"" T0 inner join ""@Z_PAY_LEAVE"" T1 on T0.""U_Z_TrnsCode""=T1.""Code"" where ""U_Z_EMPID""='" & objen.Empid & "' and ""U_Z_IsTerm""='N' order by T0.""Code"" Desc"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds)
            Return objDA.ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function getNodays(ByVal objen As LeaveRequestEN) As String
        Try
            objDA.strQuery = "select datediff(D,'" & objen.FromDate.ToString("yyyy/MM/dd") & "','" & objen.ToDate.ToString("yyyy/MM/dd") & "')"
            objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
            objDA.con.Open()
            objDA.sqlda = New SqlDataAdapter(objDA.cmd)
            objDA.dt.Clear()
            objDA.sqlda.Fill(objDA.dt)
            If objDA.dt.Rows.Count > 0 Then
                objen.OffCycle = objDA.dt.Rows(0)(0).ToString() + 1
            End If
            objDA.con.Close()
            Return objen.OffCycle
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function SaveLeaveRequest(ByVal objen As LeaveRequestEN) As String
        Try
            Dim strCode As String
            strCode = objDA.Getmaxcode("""@Z_PAY_OLETRANS1""", """Code""")
            objDA.strQuery = "Insert into ""@Z_PAY_OLETRANS1"" (""Code"",""Name"",""U_Z_EMPID"",""U_Z_EMPNAME"",""U_Z_TransType"",""U_Z_TrnsCode"",""U_Z_LeaveName"",""U_Z_StartDate"",""U_Z_EndDate"",""U_Z_NoofDays"",""U_Z_Notes"",""U_Z_ReJoiNDate"",""U_Z_Status"",""U_Z_LevBal"",""U_Z_Year"",""U_Z_Month"",""U_Z_TotalLeave"") "
            objDA.strQuery += "Values ('" & strCode & "','" & strCode & "','" & objen.Empid & "','" & objen.EmpName & "','L','" & objen.LeaveCode & "','" & objen.LeaveName & "','" & objen.FromDate.ToString("yyyy/MM/dd") & "','" & objen.ToDate.ToString("yyyy/MM/dd") & "','" & objen.NoofDays & "','" & objen.Notes & "','" & objen.RejoinDt.ToString("yyyy/MM/dd") & "','" & objen.Status & "'," & objen.LeaveBalance & "," & objen.Year & "," & objen.Month & "," & objen.TotalLeave & ")"
            objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
            objDA.con.Open()
            objDA.cmd.ExecuteNonQuery()
            objDA.con.Close()
            Return strCode 'True
        Catch ex As Exception
            Throw ex
            Return ""
        End Try
        Return ""
    End Function
    Public Function PopulateLeaveRequest(ByVal objen As LeaveRequestEN) As DataSet
        Try
            objDA.strQuery = "Select T0.""Code"" as ""Code"",""U_Z_TrnsCode"",T1.""Name"" as ""Name"",convert(varchar(10),""U_Z_StartDate"",103) AS ""U_Z_StartDate"",convert(varchar(10),""U_Z_EndDate"",103) AS ""U_Z_EndDate"" ,cast(T0.U_Z_NoofDays as decimal(10,2)) AS ""U_Z_NoofDays"",""U_Z_Notes"",convert(varchar(10),""U_Z_ReJoiNDate"",103) AS ""U_Z_ReJoiNDate"",""U_Z_Status"",ISNULL(""U_Z_LevBal"",0) AS ""U_Z_LevBal"" from ""@Z_PAY_OLETRANS1"" T0 inner join ""@Z_PAY_LEAVE"" T1 on T0.""U_Z_TrnsCode""=T1.""Code""  where ""U_Z_EMPID""='" & objen.Empid & "' and T0.""Code""='" & objen.LeaveCode & "'"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds1)
            Return objDA.ds1
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function UpdateLeaveRequest(ByVal objen As LeaveRequestEN) As Boolean
        Try
            objDA.strQuery = "Update ""@Z_PAY_OLETRANS1"" set ""U_Z_Status""='" & objen.Status & "', ""U_Z_TrnsCode""='" & objen.LeaveCode & "',""U_Z_StartDate""='" & objen.FromDate.ToString("yyyy/MM/dd") & "',""U_Z_EndDate""='" & objen.ToDate.ToString("yyyy/MM/dd") & "',""U_Z_NoofDays""='" & objen.NoofDays & "',""U_Z_LevBal""=" & objen.LeaveBalance & ",""U_Z_Year""=" & objen.Year & ",""U_Z_Month""=" & objen.Month & ",""U_Z_Notes""='" & objen.Notes & "',""U_Z_ReJoiNDate""='" & objen.RejoinDt.ToString("yyyy/MM/dd") & "' where ""Code""='" & objen.strCode & "' and  ""U_Z_EMPID""='" & objen.Empid & "'"
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
    Public Function WithdrawRequest(ByVal objen As LeaveRequestEN) As Boolean
        Try
            objDA.strQuery = "Delete from ""@Z_PAY_OLETRANS1"" where ""Code""='" & objen.strCode & "' and  ""U_Z_EMPID""='" & objen.Empid & "'"
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
    Public Function PopupSearchBind(ByVal objen As LeaveRequestEN) As DataSet
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

    Public Function FillLeavetype(ByVal objen As LeaveRequestEN) As DataSet
        Try
            objDA.strQuery = "Select isnull(U_Z_Terms,'') from OHEM where empID='" & objen.Empid & "'"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds4)
            If objDA.ds4.Tables(0).Rows.Count > 0 Then
                If objDA.ds4.Tables(0).Rows(0)(0).ToString() = "" Then
                    objDA.strQuery = "Select T0.""Code"",T0.""Name"",isnull(T2.U_Z_Balance,0) 'Balance'  from ""@Z_PAY_LEAVE"" T0 Left Outer Join  [@Z_EMP_LEAVE_BALANCE] T2 on T2.U_Z_LeaveCode=T0.Code    And T2.U_Z_EmpID='" & objen.Empid & "' and T2.U_Z_Year=" & objen.Year & " and isnull(T0.U_Z_HideESS,'N')='N' order by T0.""Code"""
                Else
                    objDA.strQuery = " Select T1.""U_Z_LeaveCode"" 'Code',T0.""Name"",isnull(T2.U_Z_Balance,0) 'Balance'  from  ""@Z_PAY_OALMP"" T1 Left Outer join ""@Z_PAY_LEAVE"" T0 on T0.""Code""=T1.""U_Z_LeaveCode"" Left Outer Join  [@Z_EMP_LEAVE_BALANCE] T2  on T2.U_Z_LeaveCode=T0.Code and  T2.U_Z_EmpID='" & objen.Empid & "' and T2.U_Z_Year=" & objen.Year & "  where ""U_Z_Terms""='" & objDA.ds4.Tables(0).Rows(0)(0).ToString() & "' and isnull(T0.U_Z_HideESS,'N')='N'"
                End If
            End If
        Catch ex As Exception
            objDA.strQuery = "Select T0.""Code"",T0.""Name"",isnull(T2.U_Z_Balance,0) 'Balance'  from ""@Z_PAY_LEAVE"" T0 Left Outer Join  [@Z_EMP_LEAVE_BALANCE] T2 on T2.U_Z_LeaveCode=T0.Code   And T2.U_Z_EmpID='" & objen.Empid & "' and T2.U_Z_Year=" & objen.Year & " and isnull(T0.U_Z_HideESS,'N')='N' order by T0.""Code"""

        End Try
        Try
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
        Catch ex As Exception
            ' objDA.strQuery = "Select ""Code"",""Name"" from ""@Z_PAY_LEAVE"" order by ""Code"""
            objDA.strQuery = "Select T0.""Code"",T0.""Name"",isnull(T2.U_Z_Balance,0) 'Balance'  from ""@Z_PAY_LEAVE"" T0 Left Outer Join  [@Z_EMP_LEAVE_BALANCE] T2 on T2.U_Z_LeaveCode=T0.Code    And T2.U_Z_EmpID='" & objen.Empid & "' and T2.U_Z_Year=" & objen.Year & " and isnull(T0.U_Z_HideESS,'N')='N' order by T0.""Code"""

        End Try
        objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
        objDA.sqlda.Fill(objDA.dss1)
        Return objDA.dss1
    End Function
    Public Function GetLeaveBalance(ByVal objen As LeaveRequestEN) As String
        objDA.con.Open()
        objDA.cmd = New SqlCommand("select isnull(U_Z_Balance,0) from [@Z_EMP_LEAVE_BALANCE] where U_Z_Year=" & objen.Year & " and U_Z_EmpID='" & objen.Empid & "' and U_Z_LeaveCode='" & objen.LeaveCode & "'", objDA.con)
        objDA.cmd.CommandType = CommandType.Text
        objen.Status = objDA.cmd.ExecuteScalar()
        objDA.con.Close()
        Return objen.Status
    End Function
    Public Function getCutoff(ByVal objen As LeaveRequestEN) As String
        Try
            objDA.con.Open()
            objDA.cmd = New SqlCommand("Select T0.[U_Z_Cutoff]  from ""@Z_PAY_LEAVE"" T0 where Code='" & objen.LeaveCode & "'", objDA.con)
            objDA.cmd.CommandType = CommandType.Text
            objen.RStatus = objDA.cmd.ExecuteScalar()
            objDA.con.Close()
            Return objen.RStatus
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function getHolidayCount(ByVal objen As LeaveRequestEN) As Double
        Dim dblHolidays As Double = 0
        Dim oRec, oRec1, otemp As SAPbobsCOM.Recordset
        oRec = objen.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        oRec1 = objen.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        otemp = objen.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        oRec.DoQuery("Select * from OHEM where empID=" & objen.Empid)
        If oRec.RecordCount > 0 Then
            If oRec.Fields.Item("U_Z_HldCode").Value <> "" Then
                oRec1.DoQuery("Select * from OHLD where HldCode='" & oRec.Fields.Item("U_Z_HldCode").Value & "'")
                If oRec1.RecordCount > 0 Then
                    While objen.FromDate <= objen.ToDate
                        If objen.CutOff = "B" Or objen.CutOff = "W" Then
                            '     MsgBox(WeekdayName(1))
                            Dim strweekname1, strweekname2 As String
                            strweekname1 = WeekdayName(oRec1.Fields.Item("WndFrm").Value)
                            strweekname2 = WeekdayName(oRec1.Fields.Item("WndTo").Value)
                            If WeekdayName(Weekday(objen.FromDate)) = strweekname1 Or WeekdayName(Weekday(objen.FromDate)) = strweekname2 Then
                                dblHolidays = dblHolidays + 1
                            End If
                        End If
                        If objen.CutOff = "H" Or objen.CutOff = "B" Then
                            otemp.DoQuery("Select * from [HLD1] where ('" & objen.FromDate.ToString("yyyy-MM-dd") & "' between strdate and enddate) and  hldCode='" & oRec.Fields.Item("U_Z_HldCode").Value & "'")
                            If otemp.RecordCount > 0 Then
                                dblHolidays = dblHolidays + 1
                            End If
                        End If
                        objen.FromDate = objen.FromDate.AddDays(1)
                    End While
                End If
            End If
        End If
        Return dblHolidays
    End Function

    Public Function getHolidaysinLeaveDays(ByVal objen As LeaveRequestEN) As Double
        Dim dblHolidays As Double = 0
        Dim oRec, oRec1, otemp As SAPbobsCOM.Recordset
        oRec = objen.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        oRec1 = objen.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        otemp = objen.SapCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        oRec.DoQuery("Select * from OHEM where empID=" & objen.Empid)
        If oRec.RecordCount > 0 Then
            If oRec.Fields.Item("U_Z_HldCode").Value <> "" Then
                oRec1.DoQuery("Select * from OHLD where HldCode='" & oRec.Fields.Item("U_Z_HldCode").Value & "'")
                If oRec1.RecordCount > 0 Then
                    While objen.FromDate <= objen.ToDate
                        If objen.CutOff = "H" Or objen.CutOff = "B" Then
                            otemp.DoQuery("Select * from [HLD1] where ('" & objen.FromDate.ToString("yyyy-MM-dd") & "' between strdate and enddate) and  hldCode='" & oRec.Fields.Item("U_Z_HldCode").Value & "'")
                            If otemp.RecordCount > 0 Then
                                dblHolidays = dblHolidays + 1
                            End If
                        End If
                        objen.FromDate = objen.FromDate.AddDays(1)
                    End While
                End If
            End If
        End If
        Return dblHolidays
    End Function
End Class
