Public Class ClaimRequestEN
    Private _EmpId As String
    Private _ReqNo As String
    Private _LineNo As Integer
    Private _TravelCode As String
    Private _DocEntry As String
    Private _ClaimAmt As Decimal
    Private _AlloCode As String
    Private _LocalCurrency As String
    Private _SapCompany As SAPbobsCOM.Company
    Private _SessionID As String
    Public Property SessionID() As String
        Get
            Return _SessionID
        End Get
        Set(ByVal value As String)
            _SessionID = value
        End Set
    End Property
    Public Property SapCompany() As SAPbobsCOM.Company
        Get
            Return _SapCompany
        End Get
        Set(ByVal value As SAPbobsCOM.Company)
            _SapCompany = value
        End Set
    End Property
    Public Property LocalCurrency() As String
        Get
            Return _LocalCurrency
        End Get
        Set(ByVal value As String)
            _LocalCurrency = value
        End Set
    End Property
    Public Property AllowanceCode() As String
        Get
            Return _AlloCode
        End Get
        Set(ByVal value As String)
            _AlloCode = value
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
    Public Property ReqNo() As String
        Get
            Return _ReqNo
        End Get
        Set(ByVal value As String)
            _ReqNo = value
        End Set
    End Property
    Public Property LineNo() As Integer
        Get
            Return _LineNo
        End Get
        Set(ByVal value As Integer)
            _LineNo = value
        End Set
    End Property
    Public Property TravelCode() As String
        Get
            Return _TravelCode
        End Get
        Set(ByVal value As String)
            _TravelCode = value
        End Set
    End Property
    Public Property DocEntry() As String
        Get
            Return _DocEntry
        End Get
        Set(ByVal value As String)
            _DocEntry = value
        End Set
    End Property
    Public Property ClaimAmt() As Decimal
        Get
            Return _ClaimAmt
        End Get
        Set(ByVal value As Decimal)
            _ClaimAmt = value
        End Set
    End Property

End Class
