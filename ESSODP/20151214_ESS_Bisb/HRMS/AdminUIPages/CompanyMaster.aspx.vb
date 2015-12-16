Imports System
Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls.HtmlInputFile
Imports System.Drawing
Imports System.Data.SqlClient
Imports System.Data
Public Class CompanyMaster
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click

        If FUCompanyLogo.HasFile Then
            Dim FileEx As String
            FileEx = Path.GetExtension(FUCompanyLogo.FileName)
            If (FileEx = ".jpg" Or FileEx = ".jpeg" Or FileEx = ".png" Or FileEx = ".gif") Then
                FUCompanyLogo.SaveAs(Server.MapPath("~\Images\" & ("BG_world.png").ToString()))
                Label7.Text = "Company logo successfully uploaded"
            Else
                Label7.Text = "Company Logo allowed .jpg,.jpeg,.png,.gif images"
            End If
        Else
            Label7.Text = "Select Company logo"
        End If
    End Sub
    Public Function CopyDirectory(ByVal Src As String, ByVal Dest As String, Optional ByVal bQuiet As Boolean = False) As Boolean
        Try
            If Not Directory.Exists(Src) Then
                Throw New DirectoryNotFoundException("The directory " & Src & " does not exists")
            End If
        Catch ex As Exception

        End Try

        'add Directory Seperator Character (\) for the string concatenation shown later
        If Dest.Substring(Dest.Length - 1, 1) <> Path.DirectorySeparatorChar Then
            Dest += Path.DirectorySeparatorChar
        End If
        If Not Directory.Exists(Dest) Then Directory.CreateDirectory(Dest)
        Dim Files As String()
        Files = Directory.GetFileSystemEntries(Src)
        Dim element As String
        For Each element In Files
            If Directory.Exists(element) Then
                'if the current FileSystemEntry is a directory,
                'call this function recursively
                CopyDirectory(element, Dest & Path.GetFileName(element), True)
            Else
                'the current FileSystemEntry is a file so just copy it
                If Path.GetExtension(element).ToUpper = ".JPG" Or Path.GetExtension(element) = ".jpeg" Or Path.GetExtension(element) = ".gif" Or Path.GetExtension(element) = ".png" Then
                    File.Copy(element, Dest & Path.GetFileName(element), True)
                End If

            End If
        Next
        Return True
    End Function
    Protected Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Response.Redirect("AHome.aspx", False)
    End Sub
End Class