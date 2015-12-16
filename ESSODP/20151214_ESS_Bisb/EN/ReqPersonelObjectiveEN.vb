Public Class ReqPersonelObjectiveEN
    Private _EmpId As String
    Private _EmpName As String
    Private _PeoObjCode As String
    Private _PeoObjName As String
    Private _PeoObjCat As String
    Private _Weight As Decimal
    Private _Remarks As String
    Private _DocEntry As String
    Private _Manager As String
    Private _AppStatus As String
    Private _DeptCode As String
    Private _PeoObjCatDesc As String
    Private _SapCompany As SAPbobsCOM.Company
    Public Property SapCompany() As SAPbobsCOM.Company
        Get
            Return _SapCompany
        End Get
        Set(ByVal value As SAPbobsCOM.Company)
            _SapCompany = value
        End Set
    End Property
    Public Property PeoObjCatDesc() As String
        Get
            Return _PeoObjCatDesc
        End Get
        Set(ByVal value As String)
            _PeoObjCatDesc = value
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
    Public Property AppStatus() As String
        Get
            Return _AppStatus
        End Get
        Set(ByVal value As String)
            _AppStatus = value
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
    Public Property Manager() As String
        Get
            Return _Manager
        End Get
        Set(ByVal value As String)
            _Manager = value
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
    Public Property PeoObjCode() As String
        Get
            Return _PeoObjCode
        End Get
        Set(ByVal value As String)
            _PeoObjCode = value
        End Set
    End Property
    Public Property PeoObjName() As String
        Get
            Return _PeoObjName
        End Get
        Set(ByVal value As String)
            _PeoObjName = value
        End Set
    End Property
    Public Property PeoObjCat() As String
        Get
            Return _PeoObjCat
        End Get
        Set(ByVal value As String)
            _PeoObjCat = value
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
