Imports System
Imports DataAccess
Imports EN
Public Class TravelRequestBL
    Dim objen As TravelRequestEN = New TravelRequestEN()
    Dim objDA As TravelRequestDA = New TravelRequestDA()
    Public Function PageLoadBind(ByVal objen As TravelRequestEN) As DataSet
        Try
            Return objDA.PageLoadBind(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Department(ByVal objen As TravelRequestEN) As String
        Try
            Return objDA.Department(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function BindTravelName(ByVal objen As TravelRequestEN) As String
        Try
            Return objDA.BindTravelName(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ExpensesBind(ByVal objen As TravelRequestEN) As DataSet
        Try
            Return objDA.ExpensesBind(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PopulateRequest(ByVal objen As TravelRequestEN) As DataSet
        Try
            Return objDA.PopulateRequest(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Expenses(ByVal objen As TravelRequestEN) As DataSet
        Try
            Return objDA.Expenses(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function UpdateTravelRequest(ByVal objen As TravelRequestEN) As Boolean
        Try
            Return objDA.UpdateTravelRequest(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function WithdrawRequest(ByVal objen As TravelRequestEN) As Boolean
        Try
            Return objDA.WithdrawRequest(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
