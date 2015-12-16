Imports System
Imports DataAccess
Imports EN
Public Class ActivityBL
    Dim objDA As ActivityDA = New ActivityDA()
    Public Function ActivityType(ByVal objEN As ActivityEN) As DataSet
        Try
            Return objDA.ActivityType(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ActivitySubject(ByVal objEN As ActivityEN) As DataSet
        Try
            Return objDA.ActivitySubject(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ActivityStatus(ByVal objEN As ActivityEN) As DataSet
        Try
            Return objDA.ActivityStatus(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ActivityEmployee(ByVal objEN As ActivityEN) As DataSet
        Try
            Return objDA.ActivityEmployee(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function EmployeeActivity() As DataSet
        Try
            Return objDA.EmployeeActivity()
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function LoadActivity(ByVal objEN As ActivityEN) As DataSet
        Try
            Return objDA.LoadActivity(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function LoadEmpActivity(ByVal objEN As ActivityEN) As DataSet
        Try
            Return objDA.LoadEmpActivity(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function BindPersonelDetails(ByVal objen As ActivityEN) As DataSet
        Try
            Return objDA.BindPersonelDetails(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetDeptHR(ByVal objen As ActivityEN) As String
        Try
            Return objDA.GetDeptHR(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function getNodays(ByVal objen As ActivityEN) As String
        Try
            Return objDA.getNodays(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PopulatedActivity(ByVal objen As ActivityEN) As DataSet
        Try
            Return objDA.PopulatedActivity(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetUser(ByVal objen As ActivityEN) As String
        Try
            Return objDA.GetUser(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Public Function Attachment(ByVal objEN As ActivityEN) As DataSet
    '    Try
    '        Return objDA.Attachment(objEN)
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function
    Public Function SendMail_Approval(ByVal objen As ActivityEN) As String
        Try
            Return objDA.SendMail_Approval(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
