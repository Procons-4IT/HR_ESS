Public Class SeniorMgrAppraisalEN
    Private _Strqry As String
    Private _EmpId As String
    Private _strConiditon As String
    Private _AppNumber As String
    Private _StrType As String
    Private _BussRemarks As String
    Private _PeopleRemarks As String
    Private _CompRemarks As String
    Private _IntLine As Integer
    Private _dblAmount As Double
    Private strStatus As String
    Private _strChkStatus As String
    Private _strSelRate As String
    Private _Ratings As String
    Private _Period As String
    Private _SearchCondition As String
    Private _SecondRemarks As String
    Public Property SecondRemarks() As String
        Get
            Return _SecondRemarks
        End Get
        Set(ByVal value As String)
            _SecondRemarks = value
        End Set
    End Property
    Public Property SearchCondition() As String
        Get
            Return _SearchCondition
        End Get
        Set(ByVal value As String)
            _SearchCondition = value
        End Set
    End Property
    Public Property Period As String
        Get
            Return _Period
        End Get
        Set(ByVal value As String)
            _Period = value
        End Set
    End Property
    Public Property Ratings
        Get
            Return _Ratings
        End Get
        Set(ByVal value)
            _Ratings = value
        End Set
    End Property
    Public Property LineNo
        Get
            Return _IntLine
        End Get
        Set(ByVal value)
            _IntLine = value
        End Set
    End Property
    Public Property Amount
        Get
            Return _dblAmount
        End Get
        Set(ByVal value)
            _dblAmount = value
        End Set
    End Property
    Public Property Status
        Get
            Return strStatus
        End Get
        Set(ByVal value)
            strStatus = value
        End Set
    End Property
    Public Property CheckStatus
        Get
            Return _strChkStatus
        End Get
        Set(ByVal value)
            _strChkStatus = value
        End Set
    End Property
    Public Property SelfRating
        Get
            Return _strSelRate
        End Get
        Set(ByVal value)
            _strSelRate = value
        End Set
    End Property

    Public Property BusinessRemarks
        Get
            Return _BussRemarks
        End Get
        Set(ByVal value)
            _BussRemarks = value
        End Set
    End Property
    Public Property PeopleRemarks
        Get
            Return _PeopleRemarks
        End Get
        Set(ByVal value)
            _PeopleRemarks = value
        End Set
    End Property
    Public Property CompRemarks
        Get
            Return _CompRemarks
        End Get
        Set(ByVal value)
            _CompRemarks = value
        End Set
    End Property
    Public Property StrType
        Get
            Return _StrType
        End Get
        Set(ByVal value)
            _StrType = value
        End Set
    End Property
    Public Property StrQry
        Get
            Return _Strqry
        End Get
        Set(ByVal value)
            _Strqry = value
        End Set
    End Property
    Public Property EmpId
        Get
            Return _EmpId
        End Get
        Set(ByVal value)
            _EmpId = value
        End Set
    End Property
    Public Property strConiditon
        Get
            Return _strConiditon
        End Get
        Set(ByVal value)
            _strConiditon = value
        End Set
    End Property
    Public Property AppraisalNumber
        Get
            Return _AppNumber
        End Get
        Set(ByVal value)
            _AppNumber = value
        End Set
    End Property
End Class
