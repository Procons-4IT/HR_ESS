Imports System
Imports DataAccess
Imports EN
Public Class ChangePwdBL
    Dim objDA As ChangePwdDA = New ChangePwdDA()
    Dim objen As ChangePwdEN = New ChangePwdEN()
    Public Function Checkpassword(ByVal objen As ChangePwdEN) As Boolean
        Try
            Return objDA.Checkpassword(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
