Public Class AdminContactUsEN
    Private _CompanyName As String
    Private _EmpName As String
    Private _Position As String
    Private _Email As String
    Private _Phone As String
    Private _Status As String
    Private _DocEntry As String
    Public Property DocEntry() As String
        Get
            Return _DocEntry
        End Get
        Set(ByVal value As String)
            _DocEntry = value
        End Set
    End Property
    Public Property CompanyName() As String
        Get
            Return _CompanyName
        End Get
        Set(ByVal value As String)
            _CompanyName = value
        End Set
    End Property
    Public Property EmpName() As String
        Get
            Return _EmpName
        End Get
        Set(ByVal value As String)
            _EmpName = value
        End Set
    End Property
    Public Property Position() As String
        Get
            Return _Position
        End Get
        Set(ByVal value As String)
            _Position = value
        End Set
    End Property
    Public Property Email() As String
        Get
            Return _Email
        End Get
        Set(ByVal value As String)
            _Email = value
        End Set
    End Property
    Public Property Phone() As String
        Get
            Return _Phone
        End Get
        Set(ByVal value As String)
            _Phone = value
        End Set
    End Property
    Public Property Status() As String
        Get
            Return _Status
        End Get
        Set(ByVal value As String)
            _Status = value
        End Set
    End Property

End Class
