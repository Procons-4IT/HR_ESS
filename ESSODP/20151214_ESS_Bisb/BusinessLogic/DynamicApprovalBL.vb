Imports System
Imports DataAccess
Imports EN
Public Class DynamicApprovalBL
    Dim objen As DynamicApprovalEN = New DynamicApprovalEN()
    Dim objDA As DynamicApprovalDA = New DynamicApprovalDA()
    Public Function GetUserCode(ByVal objEN As DynamicApprovalEN) As String
        Try
            Return objDA.GetUserCode(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function InitializationApproval(ByVal objEN As DynamicApprovalEN) As DataSet
        Try
            Return objDA.InitializationApproval(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function LoadHistory(ByVal objEN As DynamicApprovalEN) As DataSet
        Try
            Return objDA.LoadHistory(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function SummaryHistory(ByVal objEN As DynamicApprovalEN) As DataSet
        Try
            Return objDA.SummaryHistory(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ApprovalSummary(ByVal objEN As DynamicApprovalEN) As DataSet
        Try
            Return objDA.ApprovalSummary(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function addUpdateDocument(ByVal objEN As DynamicApprovalEN) As String
        Try
            Return objDA.addUpdateDocument(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ApprovalValidation(ByVal objEN As DynamicApprovalEN) As String
        Try
            Return objDA.ApprovalValidation(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetEmpUserid(ByVal objEN As DynamicApprovalEN) As Integer
        Try
            Return objDA.GetEmpUserid(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetLeaveBalance(ByVal objEN As DynamicApprovalEN) As DataSet
        Try
            Return objDA.GetLeaveBalance(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetLeaveHistory(ByVal objEN As DynamicApprovalEN) As DataSet
        Try
            Return objDA.GetLeaveHistory(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
