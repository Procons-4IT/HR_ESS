Public Class BankTimeRequestEN
    Private _strCode As String
    Private _TAEmpid As String
    Private _Empid As String
    Private _EmpName As String
    Private _LeaveCode As String
    Private _LeaveName As String
    Private _StartDate As String
    Private _NoofHours As Double
    Private _NoofDays As Double
    Private _Notes As String
    Private _AppStatus As String
    Private _CashOut As String
    Private _SapCompany As SAPbobsCOM.Company
    Private _FromDate As Date
    Public Property StrCode() As String
        Get
            Return _strCode
        End Get
        Set(ByVal value As String)
            _strCode = value
        End Set
    End Property
    Public Property TAEmpid() As String
        Get
            Return _TAEmpid
        End Get
        Set(ByVal value As String)
            _TAEmpid = value
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
    Public Property LeaveCode() As String
        Get
            Return _LeaveCode
        End Get
        Set(ByVal value As String)
            _LeaveCode = value
        End Set
    End Property
    Public Property LeaveName() As String
        Get
            Return _LeaveName
        End Get
        Set(ByVal value As String)
            _LeaveName = value
        End Set
    End Property
    Public Property StartDate() As String
        Get
            Return _StartDate
        End Get
        Set(ByVal value As String)
            _StartDate = value
        End Set
    End Property
    Public Property NoofHours() As Double
        Get
            Return _NoofHours
        End Get
        Set(ByVal value As Double)
            _NoofHours = value
        End Set
    End Property
    Public Property NoofDays() As Double
        Get
            Return _NoofDays
        End Get
        Set(ByVal value As Double)
            _NoofDays = value
        End Set
    End Property
    Public Property Notes() As String
        Get
            Return _Notes
        End Get
        Set(ByVal value As String)
            _Notes = value
        End Set
    End Property
    Public Property AppStatus() As String
        Get
            Return _AppStatus
        End Get
        Set(ByVal value As String)
            _AppStatus = value
        End Set
    End Property
    Public Property CashOut() As String
        Get
            Return _CashOut
        End Get
        Set(ByVal value As String)
            _CashOut = value
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
    Public Property FromDate() As Date
        Get
            Return _FromDate
        End Get
        Set(ByVal value As Date)
            _FromDate = value
        End Set
    End Property
End Class
