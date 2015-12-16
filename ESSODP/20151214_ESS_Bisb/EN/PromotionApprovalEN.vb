Public Class PromotionApprovalEN
    Private _Empid As String
    Private _strCode As String
    Private _Status As String
    Private _EmpCondition As String
    Private _StrEmpCondition As String
    Private _dept As String
    Private _Position As String
    Public Property Department() As String
        Get
            Return _dept
        End Get
        Set(ByVal value As String)
            _dept = value
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
    Public Property EmpCode() As String
        Get
            Return _Empid
        End Get
        Set(ByVal value As String)
            _Empid = value
        End Set
    End Property
    Public Property Code() As String
        Get
            Return _strCode
        End Get
        Set(ByVal value As String)
            _strCode = value
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
    Public Property EmpCondition() As String
        Get
            Return _EmpCondition
        End Get
        Set(ByVal value As String)
            _EmpCondition = value
        End Set
    End Property
    Public Property StrEmpCondition() As String
        Get
            Return _StrEmpCondition
        End Get
        Set(ByVal value As String)
            _StrEmpCondition = value
        End Set
    End Property
End Class
