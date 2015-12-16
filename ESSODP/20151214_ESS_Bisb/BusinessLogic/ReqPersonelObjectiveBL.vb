Imports System
Imports DataAccess
Imports EN
Public Class ReqPersonelObjectiveBL
    Dim objen As ReqPersonelObjectiveEN = New ReqPersonelObjectiveEN()
    Dim objDA As ReqPersonelObjectiveDA = New ReqPersonelObjectiveDA()
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
    Public Function SelectPeoObjCode(ByVal objen As ReqPersonelObjectiveEN) As DataSet
        Try
            Return objDA.SelectPeoObjCode(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function InsertObjective(ByVal objen As ReqPersonelObjectiveEN) As Boolean
        Try
            Return objDA.InsertObjective(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
