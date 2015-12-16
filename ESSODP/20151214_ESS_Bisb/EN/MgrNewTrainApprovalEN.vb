Public Class MgrNewTrainApprovalEN
    Private _DeptCode As String
    Private _EmpId As String
    Private _MgrStatus As String
    Private _MgrRemarks As String
    Private _Code As String
    Private _MgrReqStatus As String
    Private _strConiditon As String
    Public Property strConiditon() As String
        Get
            Return _strConiditon
        End Get
        Set(ByVal value As String)
            _strConiditon = value
        End Set
    End Property
    Public Property DeptCode() As String
        Get
            Return _DeptCode
        End Get
        Set(ByVal value As String)
            _DeptCode = value
        End Set
    End Property
    Public Property EmpId() As String
        Get
            Return _EmpId
        End Get
        Set(ByVal value As String)
            _EmpId = value
        End Set
    End Property
    Public Property MgrStatus() As String
        Get
            Return _MgrStatus
        End Get
        Set(ByVal value As String)
            _MgrStatus = value
        End Set
    End Property
    Public Property MgrRemarks() As String
        Get
            Return _MgrRemarks
        End Get
        Set(ByVal value As String)
            _MgrRemarks = value
        End Set
    End Property
    Public Property Code() As String
        Get
            Return _Code
        End Get
        Set(ByVal value As String)
            _Code = value
        End Set
    End Property
    Public Property MgrReqStatus() As String
        Get
            Return _MgrReqStatus
        End Get
        Set(ByVal value As String)
            _MgrReqStatus = value
        End Set
    End Property
End Class
