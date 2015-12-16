Imports System
Imports DataAccess
Imports EN
Public Class ExpClaimApprovalBL
    Dim objen As ExpClaimApprovalEN = New ExpClaimApprovalEN()
    Dim objDA As ExpClaimApprovalDA = New ExpClaimApprovalDA()
    Public Function LocalCurrency(ByVal objen As ExpClaimApprovalEN) As String
        Try
            Return objDA.LocalCurrency(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetUserCode(ByVal objEN As ExpClaimApprovalEN) As String
        Try
            Return objDA.GetUserCode(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetEmpUserid(ByVal objEN As ExpClaimApprovalEN) As Integer
        Try
            Return objDA.GetEmpUserid(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function MainGridBind(ByVal objEN As ExpClaimApprovalEN) As DataSet
        Try
            Return objDA.MainGridBind(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ExpensesRequestApproval(ByVal objEN As ExpClaimApprovalEN) As DataSet
        Try
            Return objDA.ExpensesRequestApproval(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function BindExpenseSummaryApproval(ByVal objEN As ExpClaimApprovalEN) As DataSet
        Try
            Return objDA.BindExpenseSummaryApproval(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ApprovalValidation(ByVal objEN As ExpClaimApprovalEN) As String
        Try
            Return objDA.ApprovalValidation(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function LoadHistory(ByVal objEN As ExpClaimApprovalEN) As DataSet
        Try
            Return objDA.LoadHistory(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function addUpdateDocument(ByVal objEN As ExpClaimApprovalEN) As String
        Try
            Return objDA.addUpdateDocument(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function UpdateDocStatus(ByVal objEN As ExpClaimApprovalEN) As String
        Try
            Return objDA.UpdateDocStatus(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetFinalStatus(ByVal objEN As ExpClaimApprovalEN) As Boolean
        Try
            Return objDA.GetFinalStatus(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
