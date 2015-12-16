Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration
Imports System.Xml
Imports System.IO
Public Class AChangePwd
    Inherits System.Web.UI.Page
    Dim strmsg As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Private Sub mess(ByVal str As String)
        ScriptManager.RegisterStartupScript(Update, Update.[GetType](), "strmsg", strmsg, True)
    End Sub
    Private Function Checkpassword(ByVal oldpwd As String) As Boolean
        Dim MyXML As New XmlDocument()
        MyXML.Load(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Login.xml"))
        Dim MyXMLNode As XmlNode = MyXML.SelectSingleNode("/LOGIN")
        If MyXMLNode IsNot Nothing Then
            Dim root As XmlNode = MyXML.DocumentElement
            Dim stroldpwd As String = root.SelectSingleNode("ADMINPWD").ChildNodes(0).Value
            If stroldpwd = txtoldpwd.Text.Trim() Then
                Return True
            Else
                Return False
            End If
        End If
        Return True
    End Function
    Protected Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click

        If txtoldpwd.Text = "" Then
            strmsg = "alert('Enter the old Password..')"
            mess(strmsg)
        End If
        If txtnewpwd.Text = "" Then
            strmsg = "alert('Enter the New Password..')"
            mess(strmsg)
        End If
        If txtconfirmpwd.Text = "" Then
            strmsg = "alert('Enter the Confirm Password..')"
            mess(strmsg)
        End If
        If Checkpassword(txtoldpwd.Text.Trim()) = False Then
            strmsg = "alert('Old password does not match..')"
            mess(strmsg)
        Else
            Dim MyXML As New XmlDocument()
            MyXML.Load(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Login.xml"))
            Dim MyXMLNode As XmlNode = MyXML.SelectSingleNode("/LOGIN")
            If MyXMLNode IsNot Nothing Then
                MyXMLNode.ChildNodes(5).InnerText = txtnewpwd.Text.Trim()
            End If ' Save the Xml.
            MyXML.Save(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Login.xml"))
            strmsg = "alert('Password changed successfully..')"
            mess(strmsg)
        End If
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Response.Redirect("AHome.aspx", False)
    End Sub
End Class