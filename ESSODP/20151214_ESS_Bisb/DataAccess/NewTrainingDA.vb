Imports System
Imports System.Data.SqlClient
Imports EN
Imports DataAccess
Public Class NewTrainingDA
    Dim objen As NewTrainingEN = New NewTrainingEN()
    Dim objDA As DBConnectionDA = New DBConnectionDA()
    Dim StrCode As String = ""
    Public Sub New()
        objDA.con = New SqlConnection(objDA.GetConnection)
    End Sub
    Public Function BindNewTraining(ByVal objEN As NewTrainingEN) As DataSet
        Try
            objDA.strQuery = " select DocEntry,convert(varchar(10),U_Z_ReqDate,103) AS U_Z_ReqDate,U_Z_HREmpID,U_Z_HREmpName,U_Z_DeptName,U_Z_PosiName,U_Z_CourseName,U_Z_CourseDetails,convert(varchar(10),U_Z_TrainFrdt,103) as U_Z_TrainFrdt,convert(varchar(10),U_Z_TrainTodt,103) as U_Z_TrainTodt,cast(U_Z_TrainCost as decimal(10,2)) AS U_Z_TrainCost,U_Z_Notes,"
            objDA.strQuery += " case U_Z_AppStatus when 'P' then 'Pending' when 'A' then 'Approved' when 'R' then 'Rejected' end as U_Z_AppStatus,isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_CurApprover ),'') AS 'U_Z_CurApprover',isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_NxtApprover ),'') AS 'U_Z_NxtApprover'  from [@Z_HR_ONTREQ] T0 where U_Z_HREmpID='" & objEN.EmpId & "' Order by DocEntry Desc"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds)
            Return objDA.ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function DateValidation(ByVal objEN As NewTrainingEN) As Boolean
        Try
            objDA.ds1.Clear()
            objDA.strQuery = "Select * from OITM where '" & objEN.LveDutyOn.ToString("yyyy-MM-dd") & "' between '" & objEN.Fromdate.ToString("yyyy-MM-dd") & "' and '" & objEN.Todate.ToString("yyyy-MM-dd") & "' "
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds1)
            If objDA.ds1.Tables(0).Rows.Count > 0 Then
                Return True
            End If
        Catch ex As Exception
            Throw ex
            Return False
        End Try
        Return False
    End Function
    Public Function PopulateEmployee(ByVal objen As NewTrainingEN) As NewTrainingEN
        Try
            objen.StrQry = "Select dept,empID,isnull(firstName,'') +' '+ isnull(middleName,'') +' '+ isnull(lastName,'') as 'EmpName', isnull(position,0) as position,T1.descriptio  from OHEM T0 left join OHPS T1 on T0.position=t1.posID  where empID=" & objen.EmpId & ""
            objDA.sqlda = New SqlDataAdapter(objen.StrQry, objDA.con)
            objDA.sqlda.Fill(objDA.ds2)
            If objDA.ds2.Tables(0).Rows.Count > 0 Then
                objen.DeptCode = objDA.ds2.Tables(0).Rows(0)("dept").ToString()
                objen.EmpId = objDA.ds2.Tables(0).Rows(0)("empID").ToString()
                objen.EmpName = objDA.ds2.Tables(0).Rows(0)("EmpName").ToString()
                objen.PositionId = objDA.ds2.Tables(0).Rows(0)("position").ToString()
                objen.PositionName = objDA.ds2.Tables(0).Rows(0)("descriptio").ToString()
                objen.DeptName = Department(objen)
            End If
        Catch ex As Exception
            Throw ex
        End Try
        Return objen
    End Function
    Private Function Department(ByVal objen As NewTrainingEN) As String
        objen.strQry = "select Remarks from OUDP  where Code='" & objen.DeptCode & "'"
        objDA.sqlda = New SqlDataAdapter(objen.strQry, objDA.con)
        objDA.sqlda.Fill(objDA.dss)
        If objDA.dss.Tables(0).Rows.Count <> 0 Then
            objen.DeptName = objDA.dss.Tables(0).Rows(0)("Remarks").ToString()
        End If
        Return objen.DeptName
    End Function
    Public Function SaveNewTrainingRequest(ByVal objen As NewTrainingEN) As String
        Try
            objDA.objMainCompany = objen.SapCompany
            Dim oGeneralService As SAPbobsCOM.GeneralService
            Dim oGeneralData1 As SAPbobsCOM.GeneralData
            Dim oGeneralParams As SAPbobsCOM.GeneralDataParams
            Dim oCompanyService As SAPbobsCOM.CompanyService
            oCompanyService = objDA.objMainCompany.GetCompanyService()
            oGeneralService = oCompanyService.GetGeneralService("Z_HR_ONTREQ")
            oGeneralData1 = oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData)
            oGeneralParams = oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams)
            If objen.ReqCode <> "" Then
                oGeneralParams.SetProperty("DocEntry", objen.ReqCode)
                oGeneralData1 = oGeneralService.GetByParams(oGeneralParams)
            End If
            oGeneralData1.SetProperty("U_Z_ReqDate", Now.Date)
            oGeneralData1.SetProperty("U_Z_HREmpID", objen.EmpId)
            oGeneralData1.SetProperty("U_Z_HREmpName", objen.EmpName)
            oGeneralData1.SetProperty("U_Z_DeptName", objen.DeptName)
            oGeneralData1.SetProperty("U_Z_PosiName", objen.PositionName)
            oGeneralData1.SetProperty("U_Z_CourseName", objen.TrainTitle)
            oGeneralData1.SetProperty("U_Z_CourseDetails", objen.Justification)
            oGeneralData1.SetProperty("U_Z_PosiCode", objen.PositionId)
            oGeneralData1.SetProperty("U_Z_DeptCode", objen.DeptCode)
            oGeneralData1.SetProperty("U_Z_TrainCost", objen.TrainCost)
            oGeneralData1.SetProperty("U_Z_EstExpe", objen.Expense)
            oGeneralData1.SetProperty("U_Z_TrainFrdt", objen.Fromdate)
            oGeneralData1.SetProperty("U_Z_TrainTodt", objen.Todate)

            oGeneralData1.SetProperty("U_Z_TrainLoc", objen.TrainLoc)
            oGeneralData1.SetProperty("U_Z_BussDays", objen.TrainDurBus)
            oGeneralData1.SetProperty("U_Z_CalDays", objen.TrainDurCal)
            oGeneralData1.SetProperty("U_Z_AwayOff", objen.AwayoffBus)
            oGeneralData1.SetProperty("U_Z_CerTestAvail", objen.TestAvail)
            oGeneralData1.SetProperty("U_Z_CerTestIncl", objen.TestInclude)
            If objen.LveDutyOn <> "01/01/1900" Then
                oGeneralData1.SetProperty("U_Z_LveDuty", objen.LveDutyOn)
            End If
            If objen.TravelOn <> "01/01/1900" Then
                oGeneralData1.SetProperty("U_Z_TravelOn", objen.TravelOn)
            End If
            If objen.LveDutyOn <> "01/01/1900" Then
                oGeneralData1.SetProperty("U_Z_ReturnOn", objen.ReturnOn)
            End If
            If objen.LveDutyOn <> "01/01/1900" Then
                oGeneralData1.SetProperty("U_Z_ResumeOn", objen.ResumesOn)
            End If
            oGeneralData1.SetProperty("U_Z_Notes", objen.Notes)
            oGeneralData1.SetProperty("U_Z_AppStatus", objen.Status)
            If objen.ReqCode = "" Then
                oGeneralService.Add(oGeneralData1)
                objDA.strmsg = "New Training Request Added Successfully..."
            Else
                oGeneralService.Update(oGeneralData1)
                objDA.strmsg = "New Training Request Updated Successfully..."
            End If
            Return objDA.strmsg
        Catch ex As Exception
            Throw ex
            Return ex.Message
        End Try
    End Function
    Public Function populateTrainRequest(ByVal objEN As NewTrainingEN) As DataSet
        Try
            objDA.strQuery = "select DocEntry,convert(varchar(10),U_Z_ReqDate,103) AS U_Z_ReqDate,U_Z_HREmpID,U_Z_HREmpName,U_Z_DeptName,U_Z_PosiName,"
            objDA.strQuery += " U_Z_CourseName,U_Z_CourseDetails,convert(varchar(10),U_Z_TrainFrdt,103) as U_Z_TrainFrdt,U_Z_PosiCode,U_Z_DeptCode,cast(U_Z_TrainCost as decimal(10,2)) AS U_Z_TrainCost,"
            objDA.strQuery += " cast(U_Z_EstExpe as decimal(10,2)) AS U_Z_EstExpe ,U_Z_TrainLoc,cast(U_Z_BussDays as decimal(10,1)) AS U_Z_BussDays,cast(U_Z_CalDays as decimal(10,1)) AS U_Z_CalDays,cast(U_Z_AwayOff as decimal(10,1)) AS U_Z_AwayOff,U_Z_CerTestAvail,U_Z_CerTestIncl,"
            objDA.strQuery += " convert(varchar(10),U_Z_TrainTodt,103) as U_Z_TrainTodt,U_Z_Notes,U_Z_AppStatus,"
            objDA.strQuery += " convert(varchar(10),U_Z_LveDuty,103) AS U_Z_LveDuty,convert(varchar(10),U_Z_TravelOn,103) AS U_Z_TravelOn,"
            objDA.strQuery += " convert(varchar(10),U_Z_ReturnOn,103) AS U_Z_ReturnOn,convert(varchar(10),U_Z_ResumeOn,103) AS U_Z_ResumeOn from [@Z_HR_ONTREQ] where U_Z_HREmpID='" & objEN.EmpId & "' AND DocEntry='" & objEN.ReqCode & "'"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.dss1)
            If objDA.dss1.Tables(0).Rows.Count <> 0 Then
                Return objDA.dss1
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function WithdrawRequest(ByVal objen As NewTrainingEN) As String
        Try
            objDA.strQuery = "Delete from [@Z_HR_ONTREQ] where DocEntry='" & objen.ReqCode & "' AND U_Z_HREmpID='" & objen.EmpId & "'"
            objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
            objDA.con.Open()
            objDA.cmd.ExecuteNonQuery()
            objDA.con.Close()
            objDA.strmsg = "New Training withdraw Successfully..."
        Catch ex As Exception
            Throw ex
            objDA.strmsg = ex.Message
        End Try
        Return objDA.strmsg
    End Function
End Class
