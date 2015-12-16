Public Class LoginEN
    Private _UserCode As String
    Private _Password As String
    Private _SessionUid As String
    Public Property Userid
        Get
            Return _UserCode
        End Get
        Set(ByVal value)
            _UserCode = value
        End Set
    End Property
    Public Property Password
        Get
            Return _Password
        End Get
        Set(ByVal value)
            _Password = value
        End Set
    End Property
    Public Property SessionUid
        Get
            Return _SessionUid
        End Get
        Set(ByVal value)
            _SessionUid = value
        End Set
    End Property

End Class
