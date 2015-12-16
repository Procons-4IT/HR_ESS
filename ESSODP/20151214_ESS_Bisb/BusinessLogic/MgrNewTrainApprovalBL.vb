Imports System
Imports DataAccess
Imports EN
Public Class MgrNewTrainApprovalBL
    Dim objen As MgrNewTrainApprovalEN = New MgrNewTrainApprovalEN()
    Dim objDA As MgrNewTrainApprovalDA = New MgrNewTrainApprovalDA()
    Public Function PageLoadBind() As DataSet
        Try
            Return objDA.PageLoadBind()
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function getEmpIDforMangers(ByVal objen As MgrNewTrainApprovalEN) As String
        Try
            Return objDA.getEmpIDforMangers(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PopulateTraining(ByVal objen As MgrNewTrainApprovalEN) As DataSet
        Try
            Return objDA.PopulateTraining(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PopupSearchBind(ByVal objen As MgrNewTrainApprovalEN) As DataSet
        Try
            Return objDA.PopupSearchBind(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
