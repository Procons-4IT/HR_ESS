Imports System.IO

Public Class Download
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim Output As String = Page.Request.QueryString.Get("ifile")
            Dim filename As String = Path.GetFileName(Output)
            Dim bts As Byte() = System.IO.File.ReadAllBytes(Output)
            Response.Clear()
            Response.ClearHeaders()
            Response.AddHeader("Content-Type", "Application/octet-stream")
            Response.AddHeader("Content-Length", bts.Length.ToString())

            Response.AddHeader("Content-Disposition", "attachment;   filename=" & filename)

            Response.BinaryWrite(bts)

            Response.Flush()

            Response.[End]()
        End If
    End Sub

End Class