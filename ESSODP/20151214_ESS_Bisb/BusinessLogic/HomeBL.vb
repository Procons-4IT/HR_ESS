Imports System
Imports DataAccess
Imports EN
Public Class HomeBL
    Dim objDA As HomeDA = New HomeDA()
    Public Function PopulateEmployeeInternal(ByVal objen As HomeEN) As DataSet
        Try
            Return objDA.PopulateEmployeeInternal(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PopulateEmployee(ByVal objen As HomeEN) As DataSet
        Try
            Return objDA.PopulateEmployee(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Department(ByVal objen As HomeEN) As String
        Try
            Return objDA.Department(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function EmpManager(ByVal objen As HomeEN) As String
        Try
            Return objDA.EmpManager(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ApplyPosition(ByVal objen As HomeEN) As Boolean
        Try
            Return objDA.ApplyPosition(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function AppNotification(ByVal objen As HomeEN) As DataSet
        Try
            Return objDA.mainGvbind(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PendingApproval(ByVal objEN As HomeEN) As DataSet
        Try
            Return objDA.PendingApproval(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function LeaveBalance(ByVal objen As HomeEN) As DataSet
        Try
            Return objDA.LeaveBalance(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetUserCode(ByVal objEN As HomeEN) As String
        Try
            Return objDA.GetUserCode(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function TrainingAgenda(ByVal objen As HomeEN) As DataSet
        Try
            Return objDA.mainGvbind1(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function AppPostions(ByVal objen As HomeEN) As DataSet
        Try
            Return objDA.AppPositions(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function VacantPositions(ByVal objen As HomeEN) As DataSet
        Try
            Return objDA.VacPositions(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function LoadActivity(ByVal objEN As HomeEN) As DataSet
        Try
            Return objDA.LoadActivity(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ReturnDocEntry() As String
        Try
            Return objDA.ReturnDocEntry()
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
