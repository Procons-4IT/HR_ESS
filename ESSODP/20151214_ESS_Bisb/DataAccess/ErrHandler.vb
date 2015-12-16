Imports System.Xml
Imports System.IO
Imports System.Text
Imports System.Globalization
Public Class ErrHandler

    Public Shared Sub WriteError(ByVal errorMessage As String)

        Try

            Dim path As String = "~/Error/" & DateTime.Today.ToString("dd-mm-yy") & ".txt"

            If (Not File.Exists(System.Web.HttpContext.Current.Server.MapPath(path))) Then

                File.Create(System.Web.HttpContext.Current.Server.MapPath(path)).Close()

            End If

            Using w As StreamWriter = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path))

                w.WriteLine(Constants.vbCrLf & "Log Entry : ")

                w.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture))

                Dim err As String = "Error in: " & System.Web.HttpContext.Current.Request.Url.ToString() & ". Error Message:" & errorMessage

                w.WriteLine(err)

                w.WriteLine("__________________________")

                w.Flush()

                w.Close()

            End Using

        Catch ex As Exception

            WriteError(ex.Message)

        End Try

    End Sub
End Class
