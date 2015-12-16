Imports Microsoft.VisualBasic
Imports System
Imports System.Web
Imports System.Xml
Imports System.Configuration
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports EN
Imports System.Web.UI.WebControls
Imports System.Data.Odbc
Imports System.Net.Mail
Public Class ActivityDA
    Dim objen As ActivityEN = New ActivityEN()
    Dim objDA As DBConnectionDA = New DBConnectionDA()
    Dim SmtpServer As New Net.Mail.SmtpClient()
    Dim mail As New Net.Mail.MailMessage
    Dim strEmpId, strmgrid As String
    Dim mailServer As String
    Dim mailPort As String
    Dim mailId As String
    Dim mailUser As String
    Dim mailPwd As String
    Dim mailSSL As String
    Dim toID As String
    Dim ccID As String
    Dim mType As String
    Dim path As String
    Public Sub New()
        objDA.con = New SqlConnection(objDA.GetConnection)
    End Sub
    Public Function ActivityType(ByVal objEN As ActivityEN) As DataSet
        Try
            objDA.strQuery = "Select ""Code"",""Name"" from OCLT order by ""Code"""
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds)
            Return objDA.ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ActivitySubject(ByVal objEN As ActivityEN) As DataSet
        Try
            objDA.strQuery = "Select ""Code"",""Name"" from OCLS where ""Type""=" & CInt(objEN.ActType)
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds1)
            If objDA.ds1.Tables(0).Rows.Count > 0 Then
                Return objDA.ds1
            Else
                objDA.strQuery = "Select ""Code"",""Name"" from OCLS"
                objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
                objDA.sqlda.Fill(objDA.dss)
                Return objDA.dss
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ActivityStatus(ByVal objEN As ActivityEN) As DataSet
        Try
            objDA.strQuery = "Select ""statusID"",""name"" from OCLA order by ""statusID"""
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds2)
            Return objDA.ds2
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ActivityEmployee(ByVal objEN As ActivityEN) As DataSet
        Try

            strEmpId = GetDeptHR(objEN)
            objDA.strQuery = "Select isnull(userId,0) from OHEM where empID='" & strEmpId & "'"
            objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
            objDA.con.Open()
            strmgrid = objDA.cmd.ExecuteScalar()
            If strmgrid <> "0" Then
                objDA.strQuery = "Select ""INTERNAL_K"" as 'Code',""U_NAME"" as ""Name"" from OUSR where INTERNAL_K='" & strmgrid & "'"
            Else
                objDA.strQuery = "Select ""empID"" as 'Code',""firstName"" +' ' + ISNULL(""middleName"",'') +' ' + ""lastName"" as ""Name"" from OHEM where empID='" & strEmpId & "'"
            End If
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds3)
            objDA.con.Close()
            Return objDA.ds3
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function LoadActivity(ByVal objEN As ActivityEN) As DataSet
        Try
            objDA.strQuery = "Select T0.""ClgCode"",T1.""Name"",case T0.""Action"" when 'T' then 'Task' else 'Other' end as ""Action"",T2.""Name"" as ""Subject"", T3.""firstName"" +' ' + ISNULL(T3.""middleName"",'') +' '+ T3.""lastName"" as ""EmpName"",convert(varchar(10),Recontact,103) as ""Recontact"",T0.""BeginTime"",convert(varchar(10),endDate,103) as ""endDate"",T0.""ENDTime"",T0.""Duration"",T0.""Details"""
            objDA.strQuery += " ,T4.""name"" as ""status"",T5.""U_NAME"" as ""UserName"",case T0.""Priority"" when '0' then 'Low' when '1' then 'Normal' when '2' then 'High' end as ""Priority"" from OCLG T0 left join OCLT T1 on "
            objDA.strQuery += " T0.""CntctType""=T1.""Code"" left join OCLS T2 on T0.""CntctSbjct""=T2.""Code"" left join OHEM T3"
            objDA.strQuery += " on T0.""AttendEmpl""=T3.""empID"" left join OUSR T5 on T0.""AttendUser""=T5.""INTERNAL_K""  left join OCLA T4 on T0.""status""=T4.""statusID"" where T0.""U_Z_HREmpID""='" & objEN.EmpId & "' and isnull(T0.U_Z_ActType,'D')='D'  order by T0.""ClgCode"" desc"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds4)
            Return objDA.ds4
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function LoadEmpActivity(ByVal objEN As ActivityEN) As DataSet
        Try
            objDA.strQuery = "Select T0.""ClgCode"",T1.""Name"",case T0.""Action"" when 'T' then 'Task' else 'Other' end as ""Action"",T2.""Name"" as ""Subject"", T3.""firstName"" +' ' + ISNULL(T3.""middleName"",'') +' '+ T3.""lastName"" as ""EmpName"",convert(varchar(10),Recontact,103) as ""Recontact"",T0.""BeginTime"",convert(varchar(10),endDate,103) as ""endDate"",T0.""ENDTime"",T0.""Duration"",T0.""Details"""
            objDA.strQuery += " ,T4.""name"" as ""status"",T5.""U_NAME"" as ""UserName"",case T0.""Priority"" when '0' then 'Low' when '1' then 'Normal' when '2' then 'High' end as ""Priority"" from OCLG T0 left join OCLT T1 on "
            objDA.strQuery += " T0.""CntctType""=T1.""Code"" left join OCLS T2 on T0.""CntctSbjct""=T2.""Code"" left join OHEM T3"
            objDA.strQuery += " on T0.""AttendEmpl""=T3.""empID"" left join OUSR T5 on T0.""AttendUser""=T5.""INTERNAL_K""  left join OCLA T4 on T0.""status""=T4.""statusID"" where T0.""U_Z_HREmpID""='" & objEN.EmpId & "' and T0.U_Z_ActType='O'  order by T0.""ClgCode"" desc"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds4)
            Return objDA.ds4
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function BindPersonelDetails(ByVal objen As ActivityEN) As DataSet
        Try
            objDA.strQuery = "SELECT ""U_Z_EmpID"",""empID"", ""firstName"" +' ' + ISNULL(""middleName"",'') +' ' + ""lastName"" as ""EmpName"" ,T0.""position"", T1.""descriptio"" AS ""Positionname"", ""dept"", T2.""Remarks"" AS ""Deptname"" FROM " & objen.DBName & ".OHEM T0 LEFT OUTER JOIN " & objen.DBName & ".OHPS T1 ON T0.""position"" = T1.""posID"" LEFT OUTER JOIN " & objen.DBName & ".OUDP T2 ON T0.""dept"" = T2.""Code"" where ""empID""='" & objen.EmpId & "'"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.dss4)
            Return objDA.dss4
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetDeptHR(ByVal objen As ActivityEN) As String
        Try
            objDA.strQuery = "Select ""dept"" from OHEM where ""empID""='" & objen.EmpId & "'"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.dss1)
            If objDA.dss1.Tables(0).Rows.Count > 0 Then
                objDA.strQuery1 = "Select isnull(""U_Z_ReqHR"",'') from OUDP where ""Code""='" & objDA.dss1.Tables(0).Rows(0)(0).ToString & "'"
                objDA.cmd = New SqlCommand(objDA.strQuery1, objDA.con)
                objDA.cmd.CommandType = CommandType.Text
                Dim status As String
                objDA.con.Open()
                status = objDA.cmd.ExecuteScalar()
                objDA.con.Close()
                Return status.Trim()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function SendMail_Approval(ByVal objen As ActivityEN) As String
        Dim oRecordset As SAPbobsCOM.Recordset
        Dim aMail As String = ""
        oRecordset = objen.SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        oRecordset.DoQuery("Select U_Z_SMTPSERV,U_Z_SMTPPORT,U_Z_SMTPUSER,U_Z_SMTPPWD,U_Z_SSL From [@Z_HR_OMAIL]")
        If Not oRecordset.EoF Then
            mailServer = oRecordset.Fields.Item("U_Z_SMTPSERV").Value
            mailPort = oRecordset.Fields.Item("U_Z_SMTPPORT").Value
            mailId = oRecordset.Fields.Item("U_Z_SMTPUSER").Value
            mailPwd = oRecordset.Fields.Item("U_Z_SMTPPWD").Value
            mailSSL = oRecordset.Fields.Item("U_Z_SSL").Value
            If mailServer <> "" And mailId <> "" And mailPwd <> "" Then
                oRecordset.DoQuery("Select isnull(email,'') as 'EmailID' from OHEM where empID='" & objen.AUser & "'")
                aMail = oRecordset.Fields.Item(0).Value
                If aMail <> "" Then
                    Dim oTest As SAPbobsCOM.Recordset
                    oTest = objen.SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                    oTest.DoQuery("Select * from [@Z_HR_OWEB]")
                    Dim strESSLink As String = ""
                    If oTest.RecordCount > 0 Then
                        strESSLink = oTest.Fields.Item("U_Z_WebPath").Value
                    End If
                    objDA.strmsg = SendMailforApproval(mailServer, mailPort, mailId, mailPwd, mailSSL, aMail, aMail, objen.AMessage, strESSLink)
                    Return objDA.strmsg
                End If
            Else
                ' oApplication.Utilities.Message("Mail Server Details Not Configured...", SAPbouiCOM.BoStatusBarMessageType.smt_Warning)
            End If
        End If
        objDA.strmsg = "Success"
        Return objDA.strmsg
    End Function

    Private Function SendMailforApproval(ByVal mailServer As String, ByVal mailPort As String, ByVal mailId As String, ByVal mailpwd As String, ByVal mailSSL As String, ByVal toId As String, ByVal ccId As String, ByVal Message As String, Optional ByVal strESSLink As String = "") As String
        Try
            'Dim strRptPath As String = System.Windows.Forms.Application.StartupPath.Trim() & "\Report.pdf"
            SmtpServer.Credentials = New Net.NetworkCredential(mailId, mailpwd)
            SmtpServer.Port = mailPort
            SmtpServer.EnableSsl = mailSSL
            SmtpServer.Host = mailServer
            mail = New Net.Mail.MailMessage()
            mail.From = New Net.Mail.MailAddress(mailId, "HRMS")
            mail.To.Add(toId)
            '  mail.CC.Add(ccId)
            mail.IsBodyHtml = True
            mail.Priority = MailPriority.High
            mail.Subject = Message
            mail.Body = Message & "  <a href=" & strESSLink & " >Click Here to Login to ESS</a>"
            SmtpServer.Send(mail)
            objDA.strmsg = "Success"
        Catch ex As Exception
            objDA.strmsg = ex.Message
        Finally
            mail.Dispose()
        End Try
        Return objDA.strmsg
    End Function

    Public Function GetUser(ByVal objen As ActivityEN) As String
        Try
            objen.EmpId = objen.Content
            strEmpId = GetDeptHR(objen)
            objDA.strQuery = "Select isnull(userId,0) from OHEM where empID='" & strEmpId & "'"
            objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
            objDA.con.Open()
            strEmpId = objDA.cmd.ExecuteScalar()
            objDA.con.Close()
            Return strEmpId
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function getNodays(ByVal objen As ActivityEN) As String
        Try
            objDA.strQuery = "select datediff(D,'" & objen.FromDate.ToString("yyyy/MM/dd") & "','" & objen.ToDate.ToString("yyyy/MM/dd") & "')"
            objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
            objDA.con.Open()
            objDA.sqlda = New SqlDataAdapter(objDA.cmd)
            objDA.dt.Clear()
            objDA.sqlda.Fill(objDA.dt)
            If objDA.dt.Rows.Count > 0 Then
                objen.Remarks = objDA.dt.Rows(0)(0).ToString() + 1
            End If
            objDA.con.Close()
            Return objen.Remarks
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PopulatedActivity(ByVal objen As ActivityEN) As DataSet
        Try
            objDA.strQuery = "select ""Action"",""CntctType"",""CntctSbjct"",""ClgCode"",isnull(""AttendEmpl"",0) as 'AttendEmpl',isnull(""AttendUser"",0) as 'AttendUser',""Details"",""Priority"",""Notes"",""status"",convert(varchar(10),Recontact,103) as ""Recontact"",convert(varchar(10),endDate,103) as ""endDate"" from OCLG where ""ClgCode""='" & objen.DocNum & "'"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.dss3)
            Return objDA.dss3
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function EmployeeActivity() As DataSet
        Try
            objDA.strQuery = "Select ""empID"" as 'Code',""firstName"" +' ' + ISNULL(""middleName"",'') +' ' + ""lastName"" as ""Name"" from OHEM order by empID ASC "
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.dss3)
            Return objDA.dss3
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Public Function Attachment(ByVal objEN As ActivityEN) As DataSet
    '    Try
    '        objDA.strQuery = "Select * from ""U_ACTIVITY"" where ""U_ActCode""='" & objEN.DocNum & "' and ""U_EmpId""='" & objEN.EmpId & "'"
    '        objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
    '        objDA.sqlda.Fill(objDA.dss2)
    '        Return objDA.dss2
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function
End Class
