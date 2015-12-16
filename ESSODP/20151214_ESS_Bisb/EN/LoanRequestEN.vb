Public Class LoanRequestEN
    Private _strCode As String
    Private _Empid As String
    Private _EmpName As String
    Private _LoanType As String
    Private _LoanName As String
    Private _ReqDate As Date
    Private _LoanAmount As Decimal
    Private _DisDate As Date
    Private _StartDate As Date
    Private _NoEMI As String
    Private _SapCompany As SAPbobsCOM.Company
    Private _Status As String
    Public Property DisDate() As Date
        Get
            Return _DisDate
        End Get
        Set(ByVal value As Date)
            _DisDate = value
        End Set
    End Property
    Public Property StartDate() As Date
        Get
            Return _StartDate
        End Get
        Set(ByVal value As Date)
            _StartDate = value
        End Set
    End Property
    Public Property NoEMI() As String
        Get
            Return _NoEMI
        End Get
        Set(ByVal value As String)
            _NoEMI = value
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
    Public Property SapCompany() As SAPbobsCOM.Company
        Get
            Return _SapCompany
        End Get
        Set(ByVal value As SAPbobsCOM.Company)
            _SapCompany = value
        End Set
    End Property
    Public Property StrCode() As String
        Get
            Return _strCode
        End Get
        Set(ByVal value As String)
            _strCode = value
        End Set
    End Property
    Public Property Empid() As String
        Get
            Return _Empid
        End Get
        Set(ByVal value As String)
            _Empid = value
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
    Public Property LoanType() As String
        Get
            Return _LoanType
        End Get
        Set(ByVal value As String)
            _LoanType = value
        End Set
    End Property
    Public Property LoanName() As String
        Get
            Return _LoanName
        End Get
        Set(ByVal value As String)
            _LoanName = value
        End Set
    End Property
    Public Property ReqDate() As Date
        Get
            Return _ReqDate
        End Get
        Set(ByVal value As Date)
            _ReqDate = value
        End Set
    End Property
    Public Property LoanAmount() As Decimal
        Get
            Return _LoanAmount
        End Get
        Set(ByVal value As Decimal)
            _LoanAmount = value
        End Set
    End Property
End Class
