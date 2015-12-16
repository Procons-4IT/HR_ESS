Imports System
Imports EN
Imports DataAccess
Public Class ReturnLveRequestBL
    Dim objDA As ReturnLveRequestDA = New ReturnLveRequestDA()
    Public Function PageLoadBind(ByVal objen As LeaveRequestEN) As DataSet
        Try
            Return objDA.PageLoadBind(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PopulateLeaveRequest(ByVal objen As LeaveRequestEN) As DataSet
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
    Public Function FillLeavetype(ByVal objen As LeaveRequestEN) As DataSet
        Try
            Return objDA.FillLeavetype(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
