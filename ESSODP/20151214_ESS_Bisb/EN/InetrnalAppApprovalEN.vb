Public Class InetrnalAppApprovalEN
    Private _Empid As String
    Private _PosName As String
    Private _PeoDocCode As String
    Private _ReqStatus As String
    Private _ReqRemarks As String
    Private _ReqCode As String
    Private _Condition As String
    Public Property Condition As String
        Get
            Return _Condition
        End Get
        Set(ByVal value As String)
            _Condition = value
        End Set
    End Property
    Public Property ReqCode As String
        Get
            Return _ReqCode
        End Get
        Set(ByVal value As String)
            _ReqCode = value
        End Set
    End Property
    Public Property ReqStatus As String
        Get
            Return _ReqStatus
        End Get
        Set(ByVal value As String)
            _ReqStatus = value
        End Set
    End Property
    Public Property ReqRemarks As String
        Get
            Return _ReqRemarks
        End Get
        Set(ByVal value As String)
            _ReqRemarks = value
        End Set
    End Property
    Public Property EmpID As String
        Get
            Return _Empid
        End Get
        Set(ByVal value As String)
            _Empid = value
        End Set
    End Property
    Public Property PosName As String
        Get
            Return _PosName
        End Get
        Set(ByVal value As String)
            _PosName = value
        End Set
    End Property
    Public Property PeoDocCode As String
        Get
            Return _PeoDocCode
        End Get
        Set(ByVal value As String)
            _PeoDocCode = value
        End Set
    End Property

End Class
