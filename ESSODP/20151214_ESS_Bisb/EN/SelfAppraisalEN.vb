
Public Class SelfAppraisalEN
    Private _Strqry As String
    Private _EmpId As String
    Private _HomeEmpId As String
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
    Private _strGrvStaus As String
    Private _GrvNo As String
    Private _GrvRemarks As String
    Private _Ratings As String
    Private _SapCompany As SAPbobsCOM.Company

    Public Property SapCompany() As SAPbobsCOM.Company
        Get
            Return _SapCompany
        End Get
        Set(ByVal value As SAPbobsCOM.Company)
            _SapCompany = value
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
    Public Property strGrvStaus
        Get
            Return _strGrvStaus
        End Get
        Set(ByVal value)
            _strGrvStaus = value
        End Set
    End Property
    Public Property GrvNo
        Get
            Return _GrvNo
        End Get
        Set(ByVal value)
            _GrvNo = value
        End Set
    End Property
    Public Property GrvRemarks
        Get
            Return _GrvRemarks
        End Get
        Set(ByVal value)
            _GrvRemarks = value
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
    Public Property HomeEmpId
        Get
            Return _HomeEmpId
        End Get
        Set(ByVal value)
            _HomeEmpId = value
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
