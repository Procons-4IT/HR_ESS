Imports System
Imports DataAccess
Imports EN
Public Class InetrnalAppApprovalBL
    Dim objen As InetrnalAppApprovalEN = New InetrnalAppApprovalEN()
    Dim objDA As InetrnalAppApprovalDA = New InetrnalAppApprovalDA()
    Public Function Pageloadbind() As DataSet
        Try
            Return objDA.Pageloadbind()
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function getEmpIDforMangers(ByVal objen As InetrnalAppApprovalEN) As String
        Try
            Return objDA.getEmpIDforMangers(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function MainGridbind(ByVal objen As InetrnalAppApprovalEN) As DataSet
        Try
            Return objDA.MainGridbind(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PopEmployeeBind(ByVal objen As InetrnalAppApprovalEN) As DataSet
        Try
            Return objDA.PopEmployeeBind(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function CreateApplicants(ByVal objen As InetrnalAppApprovalEN) As Boolean
        Try
            Return objDA.CreateApplicants(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
