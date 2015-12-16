Imports System
Imports DataAccess
Imports EN
Public Class PromotionApprovalBL
    Dim objDA As PromotionApprovalDA = New PromotionApprovalDA()
    Public Function Pageloadbind(ByVal objen As PromotionApprovalEN) As DataSet
        Try
            Return objDA.Pageloadbind(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function LineMgrPromotionApproval(ByVal objen As PromotionApprovalEN) As Boolean
        Try
            Return objDA.LineMgrPromotionApproval(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PageloadbindPosChange(ByVal objen As PromotionApprovalEN) As DataSet
        Try
            Return objDA.PageloadbindPosChange(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function LineMgrPositionApproval(ByVal objen As PromotionApprovalEN) As Boolean
        Try
            Return objDA.LineMgrPositionApproval(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function BindSearchPageLoad(ByVal objen As PromotionApprovalEN) As DataSet
        Try
            Return objDA.BindSearchPageLoad(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function BindSearchPageLoadSum(ByVal objen As PromotionApprovalEN) As DataSet
        Try
            Return objDA.BindSearchPageLoadSum(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PageloadSearchbindPosChange(ByVal objen As PromotionApprovalEN) As DataSet
        Try
            Return objDA.PageloadSearchbindPosChange(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PopupSearchBind(ByVal objen As PromotionApprovalEN) As DataSet
        Try
            Return objDA.PopupSearchBind(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PopupSearchBindPos(ByVal objen As PromotionApprovalEN) As DataSet
        Try
            Return objDA.PopupSearchBindPos(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
