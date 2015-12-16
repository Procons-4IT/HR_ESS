Public Class EmployeeProfileEN
    Private _EmpId As String
    Private _depId As String
    Private _PosId As String
    Private _HCountry As String
    Private _WCountry As String
    Private _Manager As String
    Private _Weight As Decimal
    Private _Remarks As String
    Private _DocEntry As String
    Private _FromDate As Date
    Private _ToDate As Date
    Private _DBName As String
    Public Property DBName() As String
        Get
            Return _DBName
        End Get
        Set(ByVal value As String)
            _DBName = value
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
    Public Property ToDate() As Date
        Get
            Return _ToDate
        End Get
        Set(ByVal value As Date)
            _ToDate = value
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
    Public Property DepId() As String
        Get
            Return _depId
        End Get
        Set(ByVal value As String)
            _depId = value
        End Set
    End Property
    Public Property PosId() As String
        Get
            Return _PosId
        End Get
        Set(ByVal value As String)
            _PosId = value
        End Set
    End Property
    Public Property HomeCountry() As String
        Get
            Return _HCountry
        End Get
        Set(ByVal value As String)
            _HCountry = value
        End Set
    End Property
    Public Property WorkCountry() As String
        Get
            Return _WCountry
        End Get
        Set(ByVal value As String)
            _WCountry = value
        End Set
    End Property
    Public Property Manager() As String
        Get
            Return _Manager
        End Get
        Set(ByVal value As String)
            _Manager = value
        End Set
    End Property
    Public Property Weight() As Decimal
        Get
            Return _Weight
        End Get
        Set(ByVal value As Decimal)
            _Weight = value
        End Set
    End Property
    Public Property Remarks() As String
        Get
            Return _Remarks
        End Get
        Set(ByVal value As String)
            _Remarks = value
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

End Class