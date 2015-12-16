Imports System
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports EN
Imports DataAccess
Imports System.IO

Public Class PaySlip
    Inherits System.Web.UI.Page
    Dim objen As PaySlipEN = New PaySlipEN()
    Dim objDA As PayslipDA = New PayslipDA()
    Dim dbcon As DBConnectionDA = New DBConnectionDA()
    Dim Dt As New dtPayroll()
    Private oDRow As DataRow
    Dim Dir As String
    Dim Crpt As New ReportDocument()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Session("UserCode") Is Nothing Then
                Response.Redirect("Login.aspx?sessionExpired=true", True)
            Else
                objen.EmpCode = Session("UserCode").ToString()
                GetMonthYear(objen)
            End If
        End If
    End Sub
    Private Sub GetMonthYear(ByVal objen As PaySlipEN)
        Try
            dbcon.ds = objDA.GetMonYear(objen)
            If dbcon.ds.Tables(0).Rows.Count > 0 Then
                ddlMonth.DataTextField = "U_Z_YEAR"
                ddlMonth.DataValueField = "U_Z_RefCode"
                ddlMonth.DataSource = dbcon.ds.Tables(0)
                ddlMonth.DataBind()
                ddlMonth.Items.Insert(0, "---Select---")
            Else
                ddlMonth.DataBind()
                ddlMonth.Items.Insert(0, "---Select---")
            End If

        Catch ex As Exception
            dbcon.strmsg = ex.Message
            mess(dbcon.strmsg)
        End Try
    End Sub
    Private Sub mess(ByVal str As String)
        ' ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "strmsg", dbcon.strmsg, True)
        ClientScript.RegisterStartupScript(Me.GetType(), "msg", "<script>alert('" & dbcon.strmsg & "')</script>")
    End Sub

    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            If ddlMonth.SelectedIndex = 0 Then
                dbcon.strmsg = "Select month and year..."
                mess(dbcon.strmsg)
            Else
                objen.EmpCode = Session("UserCode").ToString()
                objen.RefCode = ddlMonth.SelectedValue
                Dir = Server.MapPath("Reports\RptMonthPaySlip.rpt")
                If System.IO.File.Exists(Dir) Then
                    Dim strServer As String = ConfigurationManager.AppSettings("SAPServer")
                    Dim strDB As String = ConfigurationManager.AppSettings("CompanyDB")
                    Dim strUser As String = ConfigurationManager.AppSettings("DbUserName")
                    Dim strPwd As String = ConfigurationManager.AppSettings("DbPassword")

                    Dim crtableLogoninfos As New TableLogOnInfos
                    Dim crtableLogoninfo As New TableLogOnInfo
                    Dim crConnectionInfo As New ConnectionInfo
                    Dim CrTables As Tables
                    Dim CrTable As Table

                    Crpt.Load(Dir)

                    With crConnectionInfo
                        .ServerName = strServer
                        .DatabaseName = strDB
                        .UserID = strUser
                        .Password = strPwd
                    End With
                    CrTables = Crpt.Database.Tables
                    For Each CrTable In CrTables
                        crtableLogoninfo = CrTable.LogOnInfo
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo
                        CrTable.ApplyLogOnInfo(crtableLogoninfo)
                    Next
                    If ddlMonth.SelectedIndex <> 0 Then
                        Crpt.SetParameterValue("U_Z_RefCode", ddlMonth.SelectedValue)
                        Crpt.SetParameterValue("U_Z_Empid", objen.EmpCode)
                    End If
                    'Dim fileName As String = System.IO.Path.GetTempPath() + Session("UserCode").ToString() + "_" + ddlMonth.SelectedItem.Text + ".pdf"
                    'Crpt.ExportToDisk(ExportFormatType.PortableDocFormat, fileName)

                    'Dim bts As Byte() = System.IO.File.ReadAllBytes(fileName)
                    'Response.Clear()
                    'Response.ClearHeaders()
                    'Response.AddHeader("Content-Type", "Application/octet-stream")
                    'Response.AddHeader("Content-Length", bts.Length.ToString())

                    'Response.AddHeader("Content-Disposition", "attachment;   filename=" & fileName)

                    'Response.BinaryWrite(bts)

                    'Response.Flush()

                    'Response.End()
                    'ExportPDF()
                    Dim fname As String = Session("UserCode").ToString() + "_" + ddlMonth.SelectedItem.Text ' DateTime.Now.ToString("yyyyMMddHHmmss").ToString()
                    Response.Buffer = False
                    Response.ClearContent()
                    Response.ClearHeaders()
                    Crpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, fname)
                    Response.End()
                End If
            End If
        Catch ex As Exception
            dbcon.strmsg = ex.Message
            mess(dbcon.strmsg)
        End Try
    End Sub
    Public Sub ExportPDF()
        Try
            Dim CrExportOptions As ExportOptions
            Dim CrDiskFileDestinationOptions As New DiskFileDestinationOptions()
            Dim CrFormatTypeOptions As New PdfRtfWordFormatOptions()
            Dim str As String = System.AppDomain.CurrentDomain.BaseDirectory
            Dim strLoc As String = str + Session("UserCode").ToString() + "_" + ddlMonth.SelectedItem.Text + ".pdf"
            CrDiskFileDestinationOptions.DiskFileName = strLoc
            CrExportOptions = Crpt.ExportOptions
            With CrExportOptions
                .ExportDestinationType = ExportDestinationType.DiskFile
                .ExportFormatType = ExportFormatType.PortableDocFormat
                .DestinationOptions = CrDiskFileDestinationOptions
                .FormatOptions = CrFormatTypeOptions
            End With
            Dim strTemp As String = Server.MapPath("PDF/") 'System.IO.Path.GetTempPath()
            Dim files As String = Path.GetFileName(strLoc)
            System.IO.File.Create(System.IO.Path.Combine(strTemp, files))

            CrDiskFileDestinationOptions.DiskFileName = System.IO.Path.Combine(strTemp, files)
         
            If Not System.IO.File.Exists(CrDiskFileDestinationOptions.DiskFileName) Then

                LoadPDF(CrDiskFileDestinationOptions.DiskFileName)
            Else
                If FileInUse(CrDiskFileDestinationOptions.DiskFileName) Then
                    dbcon.strmsg = "Close The Already Opened PDF File..."
                    mess(dbcon.strmsg)
                Else
                    LoadPDF(CrDiskFileDestinationOptions.DiskFileName)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LoadPDF(ByVal filename As String)
        Try
            Crpt.Export()
            If System.IO.File.Exists(filename) Then
                'Dim myProcess As New System.Diagnostics.Process()
                'myProcess.StartInfo.FileName = "AcroRd32.exe"
                'myProcess.StartInfo.Arguments = String.Format("/A ""page=1=OpenActions"" ""{0}""", filename)
                'myProcess.Start()

        
                Dim bts As Byte() = System.IO.File.ReadAllBytes(filename)
                Response.Clear()
                Response.ClearHeaders()
                Response.AddHeader("Content-Type", "Application/octet-stream")
                Response.AddHeader("Content-Length", bts.Length.ToString())

                Response.AddHeader("Content-Disposition", "attachment;   filename=" & filename)

                Response.BinaryWrite(bts)
                Response.Flush()

                Response.End()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function FileInUse(ByVal sFile As String) As Boolean
        Dim thisFileInUse As Boolean = False
        If System.IO.File.Exists(sFile) Then
            Try
                Using f As New IO.FileStream(sFile, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.None)
                    ' thisFileInUse = False
                End Using
            Catch
                thisFileInUse = True
            End Try
        End If
        Return thisFileInUse
    End Function

    'Private Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
    '    Crpt.Close()
    '    Crpt.Dispose()
    'End Sub
End Class