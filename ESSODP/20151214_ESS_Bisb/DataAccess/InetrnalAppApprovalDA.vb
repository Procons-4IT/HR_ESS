Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Odbc
Imports DataAccess
Imports EN
Public Class InetrnalAppApprovalDA
    Dim objEN As InetrnalAppApprovalEN = New InetrnalAppApprovalEN()
    Dim objDA As DBConnectionDA = New DBConnectionDA()
    Public Sub New()
        objDA.con = New SqlConnection(objDA.GetConnection)
    End Sub
    Public Function Pageloadbind() As DataSet
        Try
            objDA.strQuery = "Select ""DocEntry"",""U_Z_PosName"" from ""@Z_HR_ORMPREQ"""
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds)
            Return objDA.ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function getEmpIDforMangers(ByVal objen As InetrnalAppApprovalEN) As String
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
    Public Function MainGridbind(ByVal objen As InetrnalAppApprovalEN) As DataSet
        Try
            If objen.EmpID <> "" Then
                objDA.strQuery = "SELECT ""U_DocEntry"", ""U_Empid"", ""U_Empname"", ""U_EmpPosCode"", ""U_EmpPosName"", ""U_EmpdeptCode"", ""U_EmpdeptName"", ""U_ReqdeptCode"", ""U_ReqdeptName"", ""U_ReqPosCode"", ""U_Remarks"", ""U_ReqPosName"", ""U_RequestCode"", ""U_ApplyDate"", ""U_Status"" FROM ""U_VACPOSITION"" WHERE ""U_Status"" <> 'A' and " & objen.Condition
                objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
                objDA.sqlda.Fill(objDA.dss)
                Return objDA.dss
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PopEmployeeBind(ByVal objen As InetrnalAppApprovalEN) As DataSet
        Try
            If objen.EmpID <> "" Then
                objDA.strQuery = "Select ""DocEntry"",""U_Z_PosName"" from ""@Z_HR_ORMPREQ""  where  ""U_Z_PosName""  like '%" + objen.EmpID + "%' "
            Else
                objDA.strQuery = "Select ""DocEntry"",""U_Z_PosName"" from ""@Z_HR_ORMPREQ"""
            End If
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds2)
            Return objDA.ds2
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function CreateApplicants(ByVal objen As InetrnalAppApprovalEN) As Boolean
        Try
            Dim strCode As String
            If objDA.ConnectSAP() = True Then
                If objDA.oCompany.InTransaction() Then
                    objDA.oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit)
                End If
                objDA.oCompany.StartTransaction()
                If objen.ReqStatus = "A" Then
                    Dim oGeneralService As SAPbobsCOM.GeneralService
                    Dim oGeneralData As SAPbobsCOM.GeneralData
                    Dim oChild, oChild1 As SAPbobsCOM.GeneralData
                    Dim oChildren, oChildren1 As SAPbobsCOM.GeneralDataCollection
                    Dim oCompanyService As SAPbobsCOM.CompanyService
                    oCompanyService = objDA.objMainCompany.GetCompanyService
                    oGeneralService = oCompanyService.GetGeneralService("Z_HR_OCRAPPL")
                    oGeneralData = oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData)
                    strCode = objDA.Getmaxcode("""@Z_HR_OCRAPP""", """DocEntry""")
                    oGeneralData.SetProperty("U_Z_RequestCode", objen.ReqCode)
                    objDA.strQuery = "Select * from OHEM where ""empID""='" & objen.EmpID & "'"
                    objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
                    objDA.sqlda.Fill(objDA.ds)
                    If objDA.ds.Tables(0).Rows.Count > 0 Then
                        oGeneralData.SetProperty("U_Z_EmpId", objen.EmpID)
                        oGeneralData.SetProperty("U_Z_FirstName", objDA.ds.Tables(0).Rows(0)("firstName").ToString())
                        oGeneralData.SetProperty("U_Z_LastName", objDA.ds.Tables(0).Rows(0)("lastName").ToString())
                        oGeneralData.SetProperty("U_Z_EmailId", objDA.ds.Tables(0).Rows(0)("email").ToString())
                        oGeneralData.SetProperty("U_Z_Mobile", objDA.ds.Tables(0).Rows(0)("mobile").ToString())
                        oGeneralData.SetProperty("U_Z_AppDate", Now.Date)
                        oGeneralData.SetProperty("U_Z_Sex", objDA.ds.Tables(0).Rows(0)("sex").ToString())
                        oGeneralData.SetProperty("U_Z_Dob", objDA.ds.Tables(0).Rows(0)("birthDate").ToString())
                        oGeneralData.SetProperty("U_Z_Nationality", objDA.ds.Tables(0).Rows(0)("brthCountr").ToString())
                        oGeneralData.SetProperty("U_Z_PStreet", objDA.ds.Tables(0).Rows(0)("workStreet").ToString())
                        oGeneralData.SetProperty("U_Z_PCity", objDA.ds.Tables(0).Rows(0)("workCity").ToString())
                        oGeneralData.SetProperty("U_Z_PZipCode", objDA.ds.Tables(0).Rows(0)("workZip").ToString())
                        oGeneralData.SetProperty("U_Z_PCountry", objDA.ds.Tables(0).Rows(0)("workCountr").ToString())
                        oGeneralData.SetProperty("U_Z_TStreet", objDA.ds.Tables(0).Rows(0)("homeStreet").ToString())
                        oGeneralData.SetProperty("U_Z_TCity", objDA.ds.Tables(0).Rows(0)("homeCity").ToString())
                        oGeneralData.SetProperty("U_Z_TZipCode", objDA.ds.Tables(0).Rows(0)("homeZip").ToString())
                        oGeneralData.SetProperty("U_Z_TCountry", objDA.ds.Tables(0).Rows(0)("homeCountr").ToString())
                        oGeneralData.SetProperty("U_Z_Remarks", objDA.ds.Tables(0).Rows(0)("remark").ToString())
                        oGeneralData.SetProperty("U_Z_Basic", objDA.ds.Tables(0).Rows(0)("salary").ToString())
                        oGeneralData.SetProperty("U_Z_PBlock", objDA.ds.Tables(0).Rows(0)("workBlock").ToString())
                        oGeneralData.SetProperty("U_Z_PBuilding", objDA.ds.Tables(0).Rows(0)("WorkBuild").ToString())
                        oGeneralData.SetProperty("U_Z_PState", objDA.ds.Tables(0).Rows(0)("workState").ToString())
                        oGeneralData.SetProperty("U_Z_TBlock", objDA.ds.Tables(0).Rows(0)("homeBlock").ToString())
                        oGeneralData.SetProperty("U_Z_TBuilding", objDA.ds.Tables(0).Rows(0)("HomeBuild").ToString())
                        oGeneralData.SetProperty("U_Z_TState", objDA.ds.Tables(0).Rows(0)("homeState").ToString())
                        oGeneralData.SetProperty("U_Z_Marital", objDA.ds.Tables(0).Rows(0)("martStatus").ToString())
                        oGeneralData.SetProperty("U_Z_Children", objDA.ds.Tables(0).Rows(0)("nChildren").ToString())
                        oGeneralData.SetProperty("U_Z_Citizen", objDA.ds.Tables(0).Rows(0)("citizenshp").ToString())
                        oGeneralData.SetProperty("U_Z_Passport", objDA.ds.Tables(0).Rows(0)("passportNo").ToString())
                        oGeneralData.SetProperty("U_Z_Passexpdate", objDA.ds.Tables(0).Rows(0)("passportEx").ToString())
                        oGeneralData.SetProperty("U_Z_Status", "R")
                        oGeneralData.SetProperty("U_Z_Source", "I")
                        oChildren = oGeneralData.Child("Z_HR_CRAPP4")
                        Dim otemp As SAPbobsCOM.Recordset
                        otemp = objDA.objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                        objDA.strQuery = "SELECT ""fromDate"", ""toDate"", ""employer"", ""position"", ""remarks"" FROM HEM4 WHERE ISNULL(""employer"", '') <> '' AND ""empID""='" & objen.EmpID & "'"
                        otemp.DoQuery(objDA.strQuery)
                        For introw As Integer = 0 To otemp.RecordCount - 1
                            oChild = oChildren.Add()
                            oChild.SetProperty("U_Z_FromDate", otemp.Fields.Item(0).Value)
                            oChild.SetProperty("U_Z_ToDate", otemp.Fields.Item(1).Value)
                            oChild.SetProperty("U_Z_PrEmployer", otemp.Fields.Item(2).Value)
                            oChild.SetProperty("U_Z_PrPosition", otemp.Fields.Item(3).Value)
                            oChild.SetProperty("U_Z_Remarks", otemp.Fields.Item(4).Value)
                            otemp.MoveNext()
                        Next
                        oChildren1 = oGeneralData.Child("Z_HR_CRAPP3")
                        Dim otemp1 As SAPbobsCOM.Recordset
                        otemp1 = objDA.objMainCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                        objDA.strQuery = "SELECT ""fromDate"", ""toDate"", ""type"", ""institute"", ""major"", ""diploma"" FROM HEM2 WHERE ISNULL(""type"", '0') <> '0' AND ""empID""='" & objen.EmpID & "'"
                        otemp1.DoQuery(objDA.strQuery)
                        For introw As Integer = 0 To otemp1.RecordCount - 1
                            oChild1 = oChildren1.Add()
                            oChild1.SetProperty("U_Z_GrFromDate", otemp1.Fields.Item(0).Value)
                            oChild1.SetProperty("U_Z_GrT0Date", otemp1.Fields.Item(1).Value)
                            oChild1.SetProperty("U_Z_Level", otemp1.Fields.Item(2).Value)
                            oChild1.SetProperty("U_Z_School", otemp1.Fields.Item(3).Value)
                            oChild1.SetProperty("U_Z_Major", otemp1.Fields.Item(4).Value)
                            oChild1.SetProperty("U_Z_Diploma", otemp1.Fields.Item(5).Value)
                            otemp1.MoveNext()
                        Next
                    End If
                    oGeneralService.Add(oGeneralData)
                End If
                objDA.strQuery = "Update ""U_VACPOSITION"" set ""U_Status""='" & objen.ReqStatus & "',""U_Remarks""='" & objen.ReqRemarks & "',""U_Approved""=getdate() where ""U_DocEntry""='" & objen.PeoDocCode & "'"
                objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
                objDA.con.Open()
                objDA.cmd.ExecuteNonQuery()
                objDA.con.Close()
                If objDA.objMainCompany.InTransaction() Then
                    objDA.objMainCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit)
                End If
            End If
        Catch ex As Exception
            If objDA.oCompany.InTransaction() Then
                objDA.oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
            End If
            Return False
        End Try
        Return True
    End Function
End Class
