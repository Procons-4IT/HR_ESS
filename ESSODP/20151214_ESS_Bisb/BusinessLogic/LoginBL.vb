Imports System
Imports DataAccess
Imports EN
Public Class LoginBL
    Dim objDA As LoginDA = New LoginDA()
    Public Function GetCardName(ByVal objen As LoginEN) As String
        Try
            Return objDA.GetCardName(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function UserAuthentication(ByVal objen As LoginEN) As Boolean
        Try
            Return objDA.UserAuthentication(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetCardCode(ByVal objen As LoginEN) As String
        Try
            Return objDA.GetCardCode(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetUserPwd(ByVal objen As LoginEN) As DataSet
        Try
            Return objDA.GetUserPwd(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function CompanyAddress() As DataSet
        Try
            Return objDA.CompanyAddress()
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ESSUsercheck(ByVal objen As LoginEN) As Boolean
        Try
            Return objDA.ESSUsercheck(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function SessionDetails(ByVal CustCode As String) As Integer
        Try
            Return objDA.SessionDetails(CustCode)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function CheckFirstLogin(ByVal objen As LoginEN) As Boolean
        Try
            Return objDA.CheckFirstLogin(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
