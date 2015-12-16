Imports System
Imports DataAccess
Imports EN
Public Class SeniorMgrAppraisalBL
    Dim objDA As SeniorMgrAppraisalDA = New SeniorMgrAppraisalDA()
    Public Function PageLoadBind(ByVal objen As SeniorMgrAppraisalEN) As DataSet
        Try
            Return objDA.PageLoadBind(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ObjectiveBind(ByVal objen As SeniorMgrAppraisalEN) As DataSet
        Try
            Return objDA.ObjectiveBind(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PopulateEmployee(ByVal objen As SeniorMgrAppraisalEN) As DataSet
        Try
            Return objDA.PopulateEmployee(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function SelectionChange(ByVal objen As SeniorMgrAppraisalEN) As DataSet
        Try
            Return objDA.SelectionChange(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function BindSearchPageLoad(ByVal objen As SeniorMgrAppraisalEN) As DataSet
        Try
            Return objDA.BindSearchPageLoad(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

   
End Class
