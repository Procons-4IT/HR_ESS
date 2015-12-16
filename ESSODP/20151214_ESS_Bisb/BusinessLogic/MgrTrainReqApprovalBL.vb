Imports System
Imports DataAccess
Imports EN
Public Class MgrTrainReqApprovalBL
    Dim objEN As MgrTrainReqApprovalEN = New MgrTrainReqApprovalEN()
    Dim objDA As MgrTrainReqApprovalDA = New MgrTrainReqApprovalDA()
    Public Function PageloadBind(ByVal objen As MgrTrainReqApprovalEN) As DataSet
        Try
            Return objDA.PageloadBind(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function getEmpIDforMangers(ByVal objen As MgrTrainReqApprovalEN) As String
        Try
            Return objDA.getEmpIDforMangers(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PopulateTraining(ByVal objen As MgrTrainReqApprovalEN) As DataSet
        Try
            Return objDA.PopulateTraining(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PopupSearchBind(ByVal objen As MgrTrainReqApprovalEN) As DataSet
        Try
            Return objDA.PopupSearchBind(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
