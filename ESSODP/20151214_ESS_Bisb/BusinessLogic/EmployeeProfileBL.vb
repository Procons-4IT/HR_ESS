Imports System
Imports DataAccess
Imports EN
Public Class EmployeeProfileBL
    Dim objen As EmployeeProfileEN = New EmployeeProfileEN()
    Dim objDA As EmployeeProfileDA = New EmployeeProfileDA()
    Public Function BindEdutype(ByVal objen As EmployeeProfileEN) As DataSet
        Try
            Return objDA.BindEdutype(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function getNodays(ByVal objen As EmployeeProfileEN) As String
        Try
            Return objDA.getNodays(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function BindPersonelDetails(ByVal objen As EmployeeProfileEN) As DataSet
        Try
            Return objDA.BindPersonelDetails(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function FillEducation(ByVal objen As EmployeeProfileEN) As DataSet
        Try
            Return objDA.BindEduDetails(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function BindCountry(ByVal objen As EmployeeProfileEN) As DataSet
        Try
            Return objDA.BindCountry(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function HomeState(ByVal objen As EmployeeProfileEN) As DataSet
        Try
            Return objDA.HomeState(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function WorkState(ByVal objen As EmployeeProfileEN) As DataSet
        Try
            Return objDA.WorkState(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function HomeBank(ByVal objen As EmployeeProfileEN) As DataSet
        Try
            Return objDA.HomeBank(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Manager(ByVal objen As EmployeeProfileEN) As DataSet
        Try
            Return objDA.Manager(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
