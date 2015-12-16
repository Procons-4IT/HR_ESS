Imports System
Imports DataAccess
Imports EN
Public Class NewTrainingBL
    Dim objDA As NewTrainingDA = New NewTrainingDA()
    Public Function BindNewTraining(ByVal objEN As NewTrainingEN) As DataSet
        Try
            Return objDA.BindNewTraining(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function DateValidation(ByVal objEN As NewTrainingEN) As Boolean
        Try
            Return objDA.DateValidation(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function PopulateEmployee(ByVal objen As NewTrainingEN) As NewTrainingEN
        Try
            Return objDA.PopulateEmployee(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function SaveNewTrainingRequest(ByVal objen As NewTrainingEN) As String
        Try
            Return objDA.SaveNewTrainingRequest(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function populateTrainRequest(ByVal objEN As NewTrainingEN) As DataSet
        Try
            Return objDA.populateTrainRequest(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function WithdrawRequest(ByVal objen As NewTrainingEN) As String
        Try
            Return objDA.WithdrawRequest(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
