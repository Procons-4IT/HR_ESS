Imports System
Imports DataAccess
Imports EN
Public Class TrainingRequestBL
    Dim objen As TrainingRequestEN = New TrainingRequestEN()
    Dim objDA As TrainingRequestDA = New TrainingRequestDA()
    Public Function ApplicableTraining(ByVal objen As TrainingRequestEN) As DataSet
        Try
            Return objDA.ApplicableTraining(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ScheduledTraining(ByVal objen As TrainingRequestEN) As DataSet
        Try
            Return objDA.ScheduledTraining(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function CourseAcquired(ByVal objen As TrainingRequestEN) As DataSet
        Try
            Return objDA.CourseAcquired(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PopulateEmployee(ByVal objen As TrainingRequestEN) As TrainingRequestEN
        Try
            Return objDA.PopulateEmployee(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function NewTraining(ByVal objen As TrainingRequestEN) As DataSet
        Try
            Return objDA.NewTraining(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function CheckTraining(ByVal objen As TrainingRequestEN) As Boolean
        Try
            Return objDA.CheckTraining(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function AppReqdate(ByVal objen As TrainingRequestEN) As Boolean
        Try
            Return objDA.AppReqdate(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function AddUDO(ByVal objen As TrainingRequestEN) As Boolean
        Try
            Return objDA.AddUDO(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function AddUDT(ByVal objen As TrainingRequestEN) As String
        Try
            Return objDA.AddUDT(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
