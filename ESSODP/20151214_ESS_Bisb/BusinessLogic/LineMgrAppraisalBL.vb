Imports System
Imports DataAccess
Imports EN
Public Class LineMgrAppraisalBL
    Dim objen As LineMgrAppraisalEN = New LineMgrAppraisalEN()
    Dim objDA As LineMgrAppraisalDA = New LineMgrAppraisalDA()
    Public Function BindPageLoad(ByVal objen As LineMgrAppraisalEN) As DataSet
        Try
            Return objDA.BindPageLoad(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function BindPageLoadTeamList(ByVal objen As LineMgrAppraisalEN) As DataSet
        Try
            Return objDA.BindPageLoadTeamList(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ObjectiveBind(ByVal objen As LineMgrAppraisalEN) As DataSet
        Try
            Return objDA.ObjectiveBind(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PopulateEmployee(ByVal objen As LineMgrAppraisalEN) As DataSet
        Try
            Return objDA.PopulateEmployee(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function SelectionChange(ByVal objen As LineMgrAppraisalEN) As DataSet
        Try
            Return objDA.SelectionChange(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function BindSearchPageLoad(ByVal objen As LineMgrAppraisalEN) As DataSet
        Try
            Return objDA.BindSearchPageLoad(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function LoadActivity(ByVal objEN As LineMgrAppraisalEN) As DataSet
        Try
            Return objDA.LoadActivity(objEN)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function SecondLevelApproval(ByVal objen As LineMgrAppraisalEN) As DataSet
        Try
            Return objDA.SecondLevelApproval(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetSecondEmail(ByVal Empid As String) As String
        Try
            Return objDA.GetSecondEmail(Empid)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
