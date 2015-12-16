Public Class PaySlipEN
    Private _RefCode As String
    Private _EmpId As String
    Private _Month As Integer
    Private _Year As Integer
    Public Property RefCode() As String
        Get
            Return _RefCode
        End Get
        Set(ByVal value As String)
            _RefCode = value
        End Set
    End Property
    Public Property EmpCode() As String
        Get
            Return _EmpId
        End Get
        Set(ByVal value As String)
            _EmpId = value
        End Set
    End Property
    Public Property Month() As String
        Get
            Return _Month
        End Get
        Set(ByVal value As String)
            _Month = value
        End Set
    End Property
    Public Property Year() As String
        Get
            Return _Year
        End Get
        Set(ByVal value As String)
            _Year = value
        End Set
    End Property
End Class
