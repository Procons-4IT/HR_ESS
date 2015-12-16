Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports DataAccess
Imports EN

Public Class TrainingRequestDA
    Dim objen As TrainingRequestEN = New TrainingRequestEN()
    Dim objDA As DBConnectionDA = New DBConnectionDA()
    Dim strCode, intTempID As String
    Public Sub New()
        objDA.con = New SqlConnection(objDA.GetConnection)
    End Sub
    Public Function ApplicableTraining(ByVal objen As TrainingRequestEN) As DataSet
        Try
            objen.strQry = "  select distinct(T0.U_Z_TrainCode),convert(varchar(11),T0.U_Z_DocDate,103) as U_Z_DocDate,T0.U_Z_CourseCode ,T0.U_Z_CourseName,T0.U_Z_CourseTypeDesc,convert(varchar(11),T0.U_Z_Startdt,103) as U_Z_Startdt,convert(varchar(11),T0.U_Z_Enddt,103) as U_Z_Enddt,T0.U_Z_MinAttendees,T0.U_Z_MaxAttendees,convert(varchar(11),T0.U_Z_AppStdt,103) as U_Z_AppStdt,convert(varchar(11),T0.U_Z_AppEnddt,103) as U_Z_AppEnddt,"
            objen.strQry += " U_Z_InsName,T0.U_Z_StartTime,T0.U_Z_EndTime,isnull(T0.U_Z_Sunday,'N') 'U_Z_Sunday',isnull(T0.U_Z_Monday,'N') 'U_Z_Monday',isnull(T0.U_Z_Tuesday,'N') 'U_Z_Tuesday',isnull(T0.U_Z_Wednesday,'N') 'U_Z_Wednesday',isnull(T0.U_Z_Thursday,'N') 'U_Z_Thursday',isnull(T0.U_Z_Friday,'N') 'U_Z_Friday',isnull(T0.U_Z_Saturday,'N') 'U_Z_Saturday',CAST(isnull(T0.U_Z_AttCost,0) as decimal(10,2)) AS U_Z_AttCost,CAST(isnull(T0.U_Z_NoOfHours,0) as decimal(10,2)) AS U_Z_NoOfHours,T0.U_Z_Active,"
            objen.strQry += " T3.U_Z_TrainLoc,T3.U_Z_EstExpe  from [@Z_HR_OTRIN] T0 left join [@Z_HR_OCOUR] T1 on T0.U_Z_CourseCode=T1.U_Z_CourseCode left join "
            objen.strQry += "  [@Z_HR_COUR4] T2  on T1.DocEntry=t2.DocEntry left join [@Z_HR_ONTREQ] T3 on T0.U_Z_NewTrainCode=T3.DocEntry where  (isnull(T1.U_Z_Allpos,'N')='Y' or  T2.U_Z_PosCode='" & objen.PositionId & "') and T0.U_Z_Active='Y' and isnull(T0.U_Z_Status,'O')='O' and    getdate() between T0.U_Z_AppStdt and T0.U_Z_AppEnddt"
            objDA.sqlda = New SqlDataAdapter(objen.strQry, objDA.con)
            objDA.sqlda.Fill(objDA.ds)
            Return objDA.ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ScheduledTraining(ByVal objen As TrainingRequestEN) As DataSet
        Try
            objen.strQry = "  select T0.""Code"",T0.""U_Z_HREmpID"",T0.""U_Z_TrainCode"",T0.""U_Z_CourseCode"",T0.""U_Z_CourseName"",T0.""U_Z_CourseTypeDesc"",convert(varchar(11),T0.""U_Z_Startdt"",103) as ""U_Z_Startdt"",convert(varchar(11),T0.""U_Z_Enddt"",103) as ""U_Z_Enddt"",T0.""U_Z_MinAttendees"",T0.""U_Z_MaxAttendees"",convert(varchar(11),T0.""U_Z_AppStdt"",103) as ""U_Z_AppStdt"",convert(varchar(11),T0.""U_Z_AppEnddt"",103) as ""U_Z_AppEnddt"","
            objen.strQry += " T0.""U_Z_InsName"",CAST(isnull(T0.U_Z_AttCost,0) as decimal(10,2)) AS U_Z_AttCost,CAST(isnull(T0.U_Z_NoOfHours,0) as decimal(10,2)) AS ""U_Z_NoOfHours"",T0.""U_Z_StartTime"",T0.""U_Z_EndTime"",isnull(T0.""U_Z_Sunday"",'N') ""U_Z_Sunday"",isnull(T0.""U_Z_Monday"",'N') ""U_Z_Monday"",isnull(T0.""U_Z_Tuesday"",'N') ""U_Z_Tuesday"",isnull(T0.""U_Z_Wednesday"",'N') ""U_Z_Wednesday"",isnull(T0.""U_Z_Thursday"",'N') ""U_Z_Thursday"",isnull(T0.""U_Z_Friday"",'N') ""U_Z_Friday"",isnull(T0.""U_Z_Saturday"",'N') ""U_Z_Saturday"",T0.""U_Z_Active"","
            objen.strQry += " case T0.""U_Z_Status"" when 'P' then 'Pending' when 'A' then 'Accepted' else 'Rejected' end as ""U_Z_Status"",T0.""U_Z_Remarks"",T0.""Code"",isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_CurApprover ),'') AS 'U_Z_CurApprover',isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_NxtApprover ),'') AS 'U_Z_NxtApprover' from [@Z_HR_TRIN1] T0 where T0.""U_Z_HREmpID""='" & objen.EmpId & "' "
            objDA.sqlda = New SqlDataAdapter(objen.strQry, objDA.con)
            objDA.sqlda.Fill(objDA.ds1)
            Return objDA.ds1
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function NewTraining(ByVal objen As TrainingRequestEN) As DataSet
        Try
            objDA.strQuery = " select DocEntry,convert(varchar(10),U_Z_ReqDate,103) AS U_Z_ReqDate,U_Z_HREmpID,U_Z_HREmpName,U_Z_DeptName,U_Z_PosiName,U_Z_CourseName,U_Z_CourseDetails,convert(varchar(10),U_Z_TrainFrdt,103) as U_Z_TrainFrdt,convert(varchar(10),U_Z_TrainTodt,103) as U_Z_TrainTodt,CAST(isnull(U_Z_TrainCost,0) as decimal(10,2)) AS U_Z_TrainCost,U_Z_Notes,"
            objDA.strQuery += " case U_Z_AppStatus when 'P' then 'Pending' when 'A' then 'Approved' when 'R' then 'Rejected' end as U_Z_AppStatus,isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_CurApprover ),'') AS 'U_Z_CurApprover',isnull((select T13.firstName +' '+ISNULL(T13.middleName,'') +' '+ isnull(T13.lastName,'')  from OHEM T13 JOIN OUSR T14 ON T14.INTERNAL_K =T13.userId where T14.USER_CODE=T0.U_Z_NxtApprover ),'') AS 'U_Z_NxtApprover'  from [@Z_HR_ONTREQ] T0 where U_Z_HREmpID='" & objen.EmpId & "' Order by DocEntry Desc"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.dss1)
            Return objDA.dss1
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function CourseAcquired(ByVal objen As TrainingRequestEN) As DataSet
        Try
            objDA.strQuery = "  select U_Z_HREmpID,U_Z_TrainCode,U_Z_CourseCode,U_Z_CourseName,U_Z_CourseTypeDesc,convert(varchar(11),""U_Z_Startdt"",103) as U_Z_Startdt,convert(varchar(11),""U_Z_Enddt"",103) as U_Z_Enddt,U_Z_MinAttendees,U_Z_MaxAttendees,convert(varchar(11),""U_Z_AppStdt"",103) as U_Z_AppStdt,convert(varchar(11),""U_Z_AppEnddt"",103) as U_Z_AppEnddt ,"
            objDA.strQuery += " U_Z_InsName,CAST(isnull(U_Z_AttCost,0) as decimal(10,2)) AS U_Z_AttCost,CAST(isnull(U_Z_NoOfHours,0) as decimal(10,2)) AS U_Z_NoOfHours,CAST(isnull(U_Z_AddionalCost,0) as decimal(10,2)) AS U_Z_AddionalCost,CAST(isnull(U_Z_TotalCost,0) as decimal(10,2)) AS U_Z_TotalCost,case U_Z_AttendeesStatus when 'D' then 'Dropped' when 'C' then 'completed' when 'F' then 'Failed' end as U_Z_AttendeesStatus,"
            objDA.strQuery += " case U_Z_Status when 'P' then 'Pending' when 'A' then 'Accepted' else 'Rejected' end as U_Z_Status,U_Z_JENO 'JENO',U_Z_Remarks,Code from [@Z_HR_TRIN1] where U_Z_HREmpID='" & objen.EmpId & "' and U_Z_UpEmpTrain='Y' "
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds4)
            Return objDA.ds4
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PopulateEmployee(ByVal objen As TrainingRequestEN) As TrainingRequestEN
        Try
            objen.strQry = "Select dept,empID,isnull(firstName,'') +' '+isnull(middleName,'') +' '+isnull(lastName,'') As EmpName, isnull(name,0) as position,descriptio  from OHEM T0 left join OHPS T1 on T0.position=t1.posID  where empID=" & objen.EmpId & ""
            objDA.sqlda = New SqlDataAdapter(objen.strQry, objDA.con)
            objDA.sqlda.Fill(objDA.ds2)
            If objDA.ds2.Tables(0).Rows.Count > 0 Then
                objen.DeptCode = objDA.ds2.Tables(0).Rows(0)("dept").ToString()
                objen.EmpId = objDA.ds2.Tables(0).Rows(0)("empID").ToString()
                objen.EmpName = objDA.ds2.Tables(0).Rows(0)("EmpName").ToString()
                objen.PositionId = objDA.ds2.Tables(0).Rows(0)("position").ToString()
                objen.PosName = objDA.ds2.Tables(0).Rows(0)("descriptio").ToString()
                objen.DeptName = Department(objen)
            End If
        Catch ex As Exception
            Throw ex
        End Try
        Return objen
    End Function
    Private Function Department(ByVal objen As TrainingRequestEN) As String
        objen.strQry = "select Remarks from OUDP  where Code='" & objen.DeptCode & "'"
        objDA.sqlda = New SqlDataAdapter(objen.strQry, objDA.con)
        objDA.sqlda.Fill(objDA.dss)
        If objDA.dss.Tables(0).Rows.Count <> 0 Then
            objen.DeptName = objDA.dss.Tables(0).Rows(0)("Remarks").ToString()
        End If
        Return objen.DeptName
    End Function
    Public Function CheckTraining(ByVal objen As TrainingRequestEN) As Boolean
        Try
            objen.strQry = "Select * from [@Z_HR_TRIN1] where U_Z_TrainCode='" & objen.AgendaCode & "' and U_Z_HREmpID='" & objen.EmpId & "'"
            objDA.sqlda = New SqlDataAdapter(objen.strQry, objDA.con)
            objDA.sqlda.Fill(objDA.dss1)
            If objDA.dss1.Tables(0).Rows.Count <> 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function AppReqdate(ByVal objen As TrainingRequestEN) As Boolean
        Try
            Dim dt As Date = Now.Date
            objDA.strQuery = "Select * from [@Z_HR_OTRIN] where '" & dt.ToString("yyyy-MM-dd") & "' between '" & objen.Fromdt.ToString("yyyy-MM-dd") & "' and '" & objen.Todt.ToString("yyyy-MM-dd") & "'"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.dss2)
            If objDA.dss2.Tables(0).Rows.Count <> 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function AddUDO(ByVal objen As TrainingRequestEN) As Boolean
        Try
            objDA.objMainCompany = objen.SapComapny
            Dim oGeneralService As SAPbobsCOM.GeneralService
            Dim oGeneralData As SAPbobsCOM.GeneralData
            Dim oGeneralParams As SAPbobsCOM.GeneralDataParams
            Dim oCompanyService As SAPbobsCOM.CompanyService
            oCompanyService = objDA.objMainCompany.GetCompanyService
            oGeneralService = oCompanyService.GetGeneralService("Z_HR_ONTREQ")
            oGeneralParams = oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams)
            oGeneralData = oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData)
            oGeneralData.SetProperty("U_Z_ReqStatus", "P")
            oGeneralData.SetProperty("U_Z_ReqDate", Now.Date)
            oGeneralData.SetProperty("U_Z_HREmpID", objen.EmpId)
            oGeneralData.SetProperty("U_Z_HREmpName", objen.EmpName)
            oGeneralData.SetProperty("U_Z_CourseName", objen.CourseCode)
            oGeneralData.SetProperty("U_Z_CourseDetails", objen.CourseDetails)
            oGeneralData.SetProperty("U_Z_PosiCode", objen.PositionId)
            oGeneralData.SetProperty("U_Z_PosiName", objen.PosName)
            oGeneralData.SetProperty("U_Z_DeptCode", objen.DeptCode)
            oGeneralData.SetProperty("U_Z_DeptName", objen.DeptName)
            oGeneralService.Add(oGeneralData)
            Return True
        Catch ex As Exception
            MsgBox(objDA.objMainCompany.GetLastErrorDescription)
            Throw ex
            Return False
        End Try
    End Function
    Public Sub WithdrawTraining(ByVal objen As TrainingRequestEN)
        objen.strQry = "Delete from [@Z_HR_TRIN1] where code='" & objen.ApplyCode & "'"
        objDA.cmd = New SqlCommand(objen.StrQry, objDA.con)
        objDA.con.Open()
        objDA.cmd.ExecuteNonQuery()
        objDA.con.Close()
    End Sub

    Public Function AddUDT(ByVal objen As TrainingRequestEN) As String
        Try
            Dim oRec, oTemp As SAPbobsCOM.Recordset
            Dim oUserTable As SAPbobsCOM.UserTable
            objDA.objMainCompany = objen.SapComapny
            oRec = objDA.objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oTemp = objDA.objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            objen.strQry = "Select * from [@Z_HR_TRIN1] where U_Z_TrainCode='" & objen.AgendaCode & "' and U_Z_HREmpID='" & objen.EmpId & "'"
            oRec.DoQuery(objen.strQry)
            If oRec.RecordCount > 0 Then
                objDA.strmsg = "You already applied for the selected Training..."
                Return objDA.strmsg
            Else
                oUserTable = objDA.objMainCompany.UserTables.Item("Z_HR_TRIN1")
                objDA.strQuery = "Select * from [@Z_HR_OTRIN] where U_Z_TrainCode='" & objen.AgendaCode & "'"
                oTemp.DoQuery(objDA.strQuery)
                If oTemp.RecordCount > 0 Then
                    strCode = objDA.Getmaxcode("[@Z_HR_TRIN1]", "Code")
                    oUserTable.Code = strCode
                    oUserTable.Name = strCode
                    oUserTable.UserFields.Fields.Item("U_Z_HREmpID").Value = objen.EmpId
                    oUserTable.UserFields.Fields.Item("U_Z_HREmpName").Value = objen.EmpName
                    oUserTable.UserFields.Fields.Item("U_Z_PosiCode").Value = objen.PositionId
                    oUserTable.UserFields.Fields.Item("U_Z_PosiName").Value = objen.PosName
                    oUserTable.UserFields.Fields.Item("U_Z_DeptCode").Value = objen.DeptCode
                    oUserTable.UserFields.Fields.Item("U_Z_DeptName").Value = objen.DeptName
                    oUserTable.UserFields.Fields.Item("U_Z_TrainCode").Value = objen.AgendaCode
                    oUserTable.UserFields.Fields.Item("U_Z_CourseCode").Value = oTemp.Fields.Item("U_Z_CourseCode").Value
                    oUserTable.UserFields.Fields.Item("U_Z_CourseName").Value = oTemp.Fields.Item("U_Z_CourseName").Value
                    oUserTable.UserFields.Fields.Item("U_Z_CourseTypeDesc").Value = oTemp.Fields.Item("U_Z_CourseTypeDesc").Value
                    oUserTable.UserFields.Fields.Item("U_Z_Startdt").Value = oTemp.Fields.Item("U_Z_Startdt").Value
                    oUserTable.UserFields.Fields.Item("U_Z_Enddt").Value = oTemp.Fields.Item("U_Z_Enddt").Value
                    oUserTable.UserFields.Fields.Item("U_Z_MinAttendees").Value = oTemp.Fields.Item("U_Z_MinAttendees").Value
                    oUserTable.UserFields.Fields.Item("U_Z_MaxAttendees").Value = oTemp.Fields.Item("U_Z_MaxAttendees").Value
                    oUserTable.UserFields.Fields.Item("U_Z_AppStdt").Value = oTemp.Fields.Item("U_Z_AppStdt").Value
                    oUserTable.UserFields.Fields.Item("U_Z_AppEnddt").Value = oTemp.Fields.Item("U_Z_AppEnddt").Value
                    oUserTable.UserFields.Fields.Item("U_Z_InsName").Value = oTemp.Fields.Item("U_Z_InsName").Value
                    oUserTable.UserFields.Fields.Item("U_Z_NoOfHours").Value = oTemp.Fields.Item("U_Z_NoOfHours").Value
                    oUserTable.UserFields.Fields.Item("U_Z_StartTime").Value = oTemp.Fields.Item("U_Z_StartTime").Value
                    oUserTable.UserFields.Fields.Item("U_Z_EndTime").Value = oTemp.Fields.Item("U_Z_EndTime").Value
                    oUserTable.UserFields.Fields.Item("U_Z_Sunday").Value = oTemp.Fields.Item("U_Z_Sunday").Value
                    oUserTable.UserFields.Fields.Item("U_Z_Monday").Value = oTemp.Fields.Item("U_Z_Monday").Value
                    oUserTable.UserFields.Fields.Item("U_Z_Tuesday").Value = oTemp.Fields.Item("U_Z_Tuesday").Value
                    oUserTable.UserFields.Fields.Item("U_Z_Wednesday").Value = oTemp.Fields.Item("U_Z_Wednesday").Value
                    oUserTable.UserFields.Fields.Item("U_Z_Thursday").Value = oTemp.Fields.Item("U_Z_Thursday").Value
                    oUserTable.UserFields.Fields.Item("U_Z_Friday").Value = oTemp.Fields.Item("U_Z_Friday").Value
                    oUserTable.UserFields.Fields.Item("U_Z_Saturday").Value = oTemp.Fields.Item("U_Z_Saturday").Value
                    oUserTable.UserFields.Fields.Item("U_Z_AttCost").Value = oTemp.Fields.Item("U_Z_AttCost").Value
                    oUserTable.UserFields.Fields.Item("U_Z_Active").Value = oTemp.Fields.Item("U_Z_Active").Value
                    oUserTable.UserFields.Fields.Item("U_Z_Status").Value = "P"
                    oUserTable.UserFields.Fields.Item("U_Z_ApplyDate").Value = Now.Date
                    oUserTable.UserFields.Fields.Item("U_Z_AppStatus").Value = objDA.DocApproval("Train", objen.EmpId)
                    If oUserTable.Add <> 0 Then
                        objDA.strmsg = objDA.objMainCompany.GetLastErrorDescription
                        Return objDA.strmsg
                    Else
                        intTempID = objDA.GetTemplateID("Train", objen.EmpId)
                        If intTempID <> "0" Then
                            objDA.UpdateApprovalRequired("@Z_HR_TRIN1", "Code", strCode, "Y", intTempID)
                            objDA.InitialMessage("Reg.Training Request", strCode, objDA.DocApproval("Train", objen.EmpId), intTempID, objen.EmpName, "RegTra", objDA.objMainCompany)
                        Else
                            objDA.UpdateApprovalRequired("@Z_HR_TRIN1", "Code", strCode, "N", intTempID)
                        End If
                        objDA.strmsg = "Success"
                        Return objDA.strmsg
                    End If
                End If
            End If
        Catch ex As Exception
            objDA.strmsg = ex.Message
            Return objDA.strmsg
        End Try
        Return objDA.strmsg
    End Function
End Class
