Imports System
Imports DataAccess
Imports EN
Public Class PeopleObjApprovalBL
    Dim objen As PeopleObjApprovalEN = New PeopleObjApprovalEN()
    Dim objDA As PeopleObjApprovalDA = New PeopleObjApprovalDA()
    Public Function Pageloadbind() As DataSet
        Try
            Return objDA.Pageloadbind()
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function getEmpIDforMangers(ByVal objen As PeopleObjApprovalEN) As String
        Try
            Return objDA.getEmpIDforMangers(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function MainGridbind(ByVal objen As PeopleObjApprovalEN) As DataSet
        Try
            Return objDA.MainGridbind(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PopEmployeeBind(ByVal objen As PeopleObjApprovalEN) As DataSet
        Try
            Return objDA.PopEmployeeBind(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function LineMgrPeopleApproval(ByVal objen As PeopleObjApprovalEN) As String
        Try
            Return objDA.LineMgrPeopleApproval(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function NotificationBind(ByVal objen As PeopleObjApprovalEN) As DataSet
        Try
            Return objDA.NotificationBind(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function NotificationPageBind(ByVal objen As PeopleObjApprovalEN) As DataSet
        Try
            Return objDA.NotificationPageBind(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
