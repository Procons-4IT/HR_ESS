Public Class PeopleObjApprovalEN
    Private _EmpId As String
    Private _EmpCondition As String
    Private _EmpCondition1 As String
    Private _EmpCondition2 As String
    Private _EmpCondition3 As String
    Private _Status As String
    Private _ReqType As String
    Private _EmpName As String
    Private _strpeoDocno As String
    Private _peostatus As String
    Private _strpeocode As String
    Private _peoname As String
    Private _peocat As String
    Private _peoweight As Double
    Private _peoremark As String
    Private _strAction As String
    Private _strrefno As String
    Private _strreqaction As String
    Public Property EmpCondition1() As String
        Get
            Return _EmpCondition1
        End Get
        Set(ByVal value As String)
            _EmpCondition1 = value
        End Set
    End Property
    Public Property EmpCondition2() As String
        Get
            Return _EmpCondition2
        End Get
        Set(ByVal value As String)
            _EmpCondition2 = value
        End Set
    End Property
    Public Property EmpCondition3() As String
        Get
            Return _EmpCondition3
        End Get
        Set(ByVal value As String)
            _EmpCondition3 = value
        End Set
    End Property
    Public Property Reqaction() As String
        Get
            Return _strreqaction
        End Get
        Set(ByVal value As String)
            _strreqaction = value
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
    Public Property EmpCondition() As String
        Get
            Return _EmpCondition
        End Get
        Set(ByVal value As String)
            _EmpCondition = value
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
    Public Property ReqType() As String
        Get
            Return _ReqType
        End Get
        Set(ByVal value As String)
            _ReqType = value
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
    Public Property strpeoDocno() As String
        Get
            Return _strpeoDocno
        End Get
        Set(ByVal value As String)
            _strpeoDocno = value
        End Set
    End Property
    Public Property peostatus() As String
        Get
            Return _peostatus
        End Get
        Set(ByVal value As String)
            _peostatus = value
        End Set
    End Property
    Public Property strpeocode() As String
        Get
            Return _strpeocode
        End Get
        Set(ByVal value As String)
            _strpeocode = value
        End Set
    End Property
    Public Property peoname() As String
        Get
            Return _peoname
        End Get
        Set(ByVal value As String)
            _peoname = value
        End Set
    End Property
    Public Property peocat() As String
        Get
            Return _peocat
        End Get
        Set(ByVal value As String)
            _peocat = value
        End Set
    End Property
    Public Property peoweight() As Double
        Get
            Return _peoweight
        End Get
        Set(ByVal value As Double)
            _peoweight = value
        End Set
    End Property
    Public Property peoremark() As String
        Get
            Return _peoremark
        End Get
        Set(ByVal value As String)
            _peoremark = value
        End Set
    End Property
    Public Property Action() As String
        Get
            Return _strAction
        End Get
        Set(ByVal value As String)
            _strAction = value
        End Set
    End Property
    Public Property Refno() As String
        Get
            Return _strrefno
        End Get
        Set(ByVal value As String)
            _strrefno = value
        End Set
    End Property
End Class
