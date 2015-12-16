Imports System
Imports EN
Imports DataAccess
Public Class BankTimeRequestBL
    Dim objDA As BankTimeRequestDA = New BankTimeRequestDA()
    Public Function PageLoadBind(ByVal objen As BankTimeRequestEN) As DataSet
        Try
            Return objDA.PageLoadBind(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function FillLeavetype(ByVal objen As BankTimeRequestEN) As DataSet
        Try
            Return objDA.FillLeavetype(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function SaveBankTimeRequest(ByVal objen As BankTimeRequestEN) As String
        Try
            Return objDA.SaveBankTimeRequest(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function UpdateBankTimeRequest(ByVal objen As BankTimeRequestEN) As Boolean
        Try
            Return objDA.UpdateBankTimeRequest(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PopulateBankTimeRequest(ByVal objen As BankTimeRequestEN) As DataSet
        Try
            Return objDA.PopulateBankTimeRequest(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function WithdrawRequest(ByVal objen As BankTimeRequestEN) As Boolean
        Try
            Return objDA.WithdrawRequest(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PopupSearchBind(ByVal objen As BankTimeRequestEN) As DataSet
        Try
            Return objDA.PopupSearchBind(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function AddtoUDT_BankTime(ByVal objen As BankTimeRequestEN) As String
        Try
            Return objDA.AddtoUDT_BankTime(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
