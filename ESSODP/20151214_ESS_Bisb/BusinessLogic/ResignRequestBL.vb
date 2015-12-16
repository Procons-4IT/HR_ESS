Imports System
Imports EN
Imports DataAccess
Public Class ResignRequestBL
    Dim objDA As ResignRequestDA = New ResignRequestDA()
    Public Function PageLoadBind(ByVal objen As LeaveRequestEN) As DataSet
        Try
            Return objDA.PageLoadBind(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function SaveLeaveRequest(ByVal objen As LeaveRequestEN) As Boolean
        Try
            Return objDA.SaveLeaveRequest(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PopulatePermissionRequest(ByVal objen As LeaveRequestEN) As DataSet
        Try
            Return objDA.PopulateLeaveRequest(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function UpdateLeaveRequest(ByVal objen As LeaveRequestEN) As Boolean
        Try
            Return objDA.UpdateLeaveRequest(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function WithdrawRequest(ByVal objen As LeaveRequestEN) As Boolean
        Try
            Return objDA.WithdrawRequest(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
