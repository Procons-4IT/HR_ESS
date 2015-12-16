Imports System
Imports DataAccess
Imports EN
Public Class PeopleObjectiveBL
    Dim objen As ReqPersonelObjectiveEN = New ReqPersonelObjectiveEN()
    Dim objDA As PeopleObjectiveDA = New PeopleObjectiveDA()
    Public Function PageLoadBind(ByVal objen As ReqPersonelObjectiveEN) As DataSet
        Try
            Return objDA.PageLoadBind(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Manager(ByVal objen As ReqPersonelObjectiveEN) As DataSet
        Try
            Return objDA.Manager(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function DeleteObjective(ByVal objen As ReqPersonelObjectiveEN) As Boolean
        Try
            Return objDA.DeleteObjective(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
