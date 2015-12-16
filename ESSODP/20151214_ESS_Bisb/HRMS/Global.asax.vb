Imports System.Web.SessionState
Imports DataAccess
Public Class Global_asax
    Inherits System.Web.HttpApplication
    Dim dbCon As DBConnectionDA = New DBConnectionDA()

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application is started
        If Application("DBName") Is Nothing Then
            strError = dbCon.Connection()
            If strError = "Success" Then
                Application("DBName") = dbCon.objMainCompany
                strError = "Successfully Connected database."
                DBConnectionDA.WriteError(strError)
            Else
                strError = strError
                ErrHandler.WriteError(strError)
            End If
        End If
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session is started
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires at the beginning of each request
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires upon attempting to authenticate the use
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when an error occurs
        Dim objErr As Exception = Server.GetLastError().GetBaseException()

        Dim err As String = "Error in: " & Request.Url.ToString() & ". Error Message:" & objErr.Message.ToString()

        ' Log the error

        ErrHandler.WriteError(err)
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session ends
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application ends
    End Sub

End Class