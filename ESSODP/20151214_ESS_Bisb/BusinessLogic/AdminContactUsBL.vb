Imports System
Imports DataAccess
Imports EN
Public Class AdminContactUsBL
    Dim objDA As AdminContactUsDA = New AdminContactUsDA()
    Dim objen As AdminContactUsEN = New AdminContactUsEN()
    Public Function PageLoadBind() As DataSet
        Try
            Return objDA.PageLoadBind()
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function InsertContactUs(ByVal objen As AdminContactUsEN) As Boolean
        Try
            Return objDA.InsertContactUs(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function UpdateContactUs(ByVal objen As AdminContactUsEN) As Boolean
        Try
            Return objDA.UpdateContactUs(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PopulatUseContact(ByVal objen As AdminContactUsEN) As DataSet
        Try
            Return objDA.PopulatUseContact(objen)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
