Imports System
Imports DataAccess
Imports EN
Public Class ResignPermissionBL
    Dim objDA As ResignPermissionDA = New ResignPermissionDA()
    Public Function PageloadbindResignation(ByVal objen As RequestApprovalEN) As DataSet
        Try
            Return objDA.PageloadbindResignation(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PageloadbindPermission(ByVal objen As RequestApprovalEN) As DataSet
        Try
            Return objDA.PageloadbindPermission(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function LineMgrResignationApproval(ByVal objen As RequestApprovalEN) As Boolean
        Try
            Return objDA.LineMgrResignationApproval(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function LineMgrResignRequestApproval(ByVal objen As RequestApprovalEN) As Boolean
        Try
            Return objDA.LineMgrResignRequestApproval(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function BindSearchPageLoad(ByVal objen As RequestApprovalEN) As DataSet
        Try
            Return objDA.BindSearchPageLoad(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function BindSrPageLoad(ByVal objen As RequestApprovalEN) As DataSet
        Try
            Return objDA.BindSrPageLoad(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function BindSearchTimePageLoad(ByVal objen As RequestApprovalEN) As DataSet
        Try
            Return objDA.BindSearchTimePageLoad(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function BindSrTimePageLoad(ByVal objen As RequestApprovalEN) As DataSet
        Try
            Return objDA.BindSrTimePageLoad(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
