Imports System
Imports System.Web
Imports System.Web.UI.WebControls

Public Class SessionHelperEN
    Public Shared Property EmpId() As String
        Get
            Return HttpContext.Current.Session("EmpId")
        End Get
        Set(ByVal value As String)
            HttpContext.Current.Session("EmpId") = value
        End Set
    End Property
End Class
