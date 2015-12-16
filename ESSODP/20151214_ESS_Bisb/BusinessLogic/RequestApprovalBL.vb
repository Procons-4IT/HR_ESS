Imports System
Imports DataAccess
Imports EN
Public Class RequestApprovalBL
    Dim objDA As RequestApprovalDA = New RequestApprovalDA()
    Public Function Pageloadbind(ByVal objen As RequestApprovalEN) As DataSet
        Try
            Return objDA.PageloadbindLeaveApproval(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function LineMgrLverequestApproval(ByVal objen As RequestApprovalEN) As Boolean
        Try
            Return objDA.LineMgrLverequestApproval(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PopupSearchBind(ByVal objen As RequestApprovalEN) As DataSet
        Try
            Return objDA.PopupSearchBind(objen)
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
End Class
