Imports System
Imports DataAccess
Imports EN
Public Class LoanRequestBL
    Dim objDA As LoanRequestDA = New LoanRequestDA()
    Public Function LoanType() As DataSet
        Try
            Return objDA.LoanType()
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PageLoadBind(ByVal Objen As LoanRequestEN) As DataSet
        Try
            Return objDA.PageLoadBind(Objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ValidateAppTemp(ByVal Objen As LoanRequestEN) As Boolean
        Try
            Return objDA.ValidateAppTemp(Objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function SaveLoanRequest(ByVal objEN As LoanRequestEN) As String
        Try
            Return objDA.SaveLoanRequest(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function UpdateLoanRequest(ByVal objEN As LoanRequestEN) As String
        Try
            Return objDA.UpdateLoanRequest(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetDocEntry() As String
        Try
            Return objDA.GetDocEntry()
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function populateLoanRequest(ByVal objEN As LoanRequestEN) As DataSet
        Try
            Return objDA.populateLoanRequest(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function WithdrawRequest(ByVal objen As LoanRequestEN) As Boolean
        Try
            Return objDA.WithdrawRequest(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function LoadHistory(ByVal objEN As LoanRequestEN) As DataSet
        Try
            Return objDA.LoadHistory(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetScheduledDetails(ByVal objEN As LoanRequestEN) As DataSet
        Try
            Return objDA.GetScheduledDetails(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
