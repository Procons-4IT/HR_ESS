Public Class TravelRequestEN
    Private _Empid As String
    Private _Deptcode As String
    Private _TripCode As String
    Private _DocEntry As String
    Private _DeptName As String
    Private _TripName As String
    Private _SapCompany As SAPbobsCOM.Company
    Public Property SapCompany() As SAPbobsCOM.Company
        Get
            Return _SapCompany
        End Get
        Set(ByVal value As SAPbobsCOM.Company)
            _SapCompany = value
        End Set
    End Property
    Public Property TripName() As String
        Get
            Return _TripName
        End Get
        Set(ByVal value As String)
            _TripName = value
        End Set
    End Property
    Public Property DeptName() As String
        Get
            Return _DeptName
        End Get
        Set(ByVal value As String)
            _DeptName = value
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
    Public Property TripCode() As String
        Get
            Return _TripCode
        End Get
        Set(ByVal value As String)
            _TripCode = value
        End Set
    End Property
    Public Property DeptCode() As String
        Get
            Return _Deptcode
        End Get
        Set(ByVal value As String)
            _Deptcode = value
        End Set
    End Property

    Public Property EmpId() As String
        Get
            Return _Empid
        End Get
        Set(ByVal value As String)
            _Empid = value
        End Set
    End Property
End Class
