Imports System
Imports DataAccess
Imports EN
Public Class LoanApprovalBL
    Dim objDA As LoanApprovalDA = New LoanApprovalDA()
    Public Function GetUserCode(ByVal objEN As LoanApprovalEN) As String
        Try
            Return objDA.GetUserCode(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function InitializationApproval(ByVal objEN As LoanApprovalEN) As DataSet
        Try
            Return objDA.InitializationApproval(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function LoadHistory(ByVal objEN As LoanApprovalEN) As DataSet
        Try
            Return objDA.LoadHistory(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PopulateLoanDetails(ByVal objEN As LoanApprovalEN) As DataSet
        Try
            Return objDA.PopulateLoanDetails(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function SummaryHistory(ByVal objEN As LoanApprovalEN) As DataSet
        Try
            Return objDA.SummaryHistory(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetEmpUserid(ByVal objEN As LoanApprovalEN) As Integer
        Try
            Return objDA.GetEmpUserid(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function addUpdateDocument(ByVal objEN As LoanApprovalEN) As String
        Try
            Return objDA.addUpdateDocument(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function FinalApprovalValidate(ByVal objEN As LoanApprovalEN) As Boolean
        Try
            Return objDA.FinalApprovalValidate(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetLoanDetails(ByVal objEN As LoanApprovalEN) As DataSet
        Try
            Return objDA.GetLoanDetails(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function getYearofExperience(ByVal objEN As LoanApprovalEN) As Double
        Try
            Return objDA.getYearofExperience(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function LoanOverLap(ByVal objEN As LoanApprovalEN) As Boolean
        Try
            Return objDA.LoanOverLap(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
