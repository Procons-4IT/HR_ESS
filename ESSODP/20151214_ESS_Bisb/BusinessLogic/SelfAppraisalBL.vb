Imports System
Imports DataAccess
Imports EN
Public Class SelfAppraisalBL
    Dim objDA As SelfAppraisalDA = New SelfAppraisalDA()
    Public Function BindPeriod() As DataSet
        Try
            Return objDA.BindPeriod()
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function mainGvbind(ByVal objen As SelfAppraisalEN) As DataSet
        Try
            Return objDA.mainGvbind(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function PopulateAppraisal(ByVal objen As SelfAppraisalEN) As DataSet
        Try
            Return objDA.PopulateAppraisal(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function BusinessObjectve(ByVal objen As SelfAppraisalEN) As DataSet
        Try
            Return objDA.BusinessObjectve(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PeopleObjective(ByVal objen As SelfAppraisalEN) As DataSet
        Try
            Return objDA.PeopleObjective(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function CompetenceObjective(ByVal objen As SelfAppraisalEN) As DataSet
        Try
            Return objDA.CompetenceObjective(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function HRFinalRating(ByVal objen As SelfAppraisalEN) As DataSet
        Try
            Return objDA.HRFinalRating(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PopulateEmployee(ByVal objen As SelfAppraisalEN) As DataSet
        Try
            Return objDA.PopulateEmployee(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
