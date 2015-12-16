Imports System
Imports EN
Imports DataAccess
Public Class LeaveRequestBL
    Dim objDA As LeaveRequestDA = New LeaveRequestDA()
    Public Function PageLoadBind(ByVal objen As LeaveRequestEN) As DataSet
        Try
            Return objDA.PageLoadBind(objen)
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

    Public Function getNodays(ByVal objen As LeaveRequestEN) As String
        Try
            Return objDA.getNodays(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function SaveLeaveRequest(ByVal objen As LeaveRequestEN) As String
        Try
            Return objDA.SaveLeaveRequest(objen)
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
    Public Function WithdrawRequest(ByVal objen As LeaveRequestEN) As Boolean
        Try
            Return objDA.WithdrawRequest(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PopupSearchBind(ByVal objen As LeaveRequestEN) As DataSet
        Try
            Return objDA.PopupSearchBind(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetLeaveBalance(ByVal objen As LeaveRequestEN) As String
        Try
            Return objDA.GetLeaveBalance(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function getCutoff(ByVal objen As LeaveRequestEN) As String
        Try
            Return objDA.getCutoff(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function getHolidayCount(ByVal objen As LeaveRequestEN) As Double
        Try
            Return objDA.getHolidayCount(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function getHolidaysinLeaveDays(ByVal objen As LeaveRequestEN) As Double
        Try
            Return objDA.getHolidaysinLeaveDays(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
