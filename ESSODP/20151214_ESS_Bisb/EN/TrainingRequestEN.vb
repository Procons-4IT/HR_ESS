Public Class TrainingRequestEN
    Private _strQry As String
    Private _EmpId As String
    Private _AgendaCode As String
    Private _CourseCode As String
    Private _PositionId As String
    Private _DeptCode As String
    Private _Fromdt As Date
    Private _Todt As Date
    Private _Status As String
    Private _ApplyCode As String
    Private _EmpName As String
    Private _PosName As String
    Private _DeptName As String
    Private _CourseDetails As String
    Private _SapComapny As SAPbobsCOM.Company
    Public Property SapComapny() As SAPbobsCOM.Company
        Get
            Return _SapComapny
        End Get
        Set(ByVal value As SAPbobsCOM.Company)
            _SapComapny = value
        End Set
    End Property
    Public Property CourseDetails
        Get
            Return _CourseDetails
        End Get
        Set(ByVal value)
            _CourseDetails = value
        End Set
    End Property
    Public Property EmpName
        Get
            Return _EmpName
        End Get
        Set(ByVal value)
            _EmpName = value
        End Set
    End Property
    Public Property DeptName
        Get
            Return _DeptName
        End Get
        Set(ByVal value)
            _DeptName = value
        End Set
    End Property
    Public Property PosName
        Get
            Return _PosName
        End Get
        Set(ByVal value)
            _PosName = value
        End Set
    End Property
    Public Property strQry
        Get
            Return _strQry
        End Get
        Set(ByVal value)
            _strQry = value
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
    Public Property AgendaCode
        Get
            Return _AgendaCode
        End Get
        Set(ByVal value)
            _AgendaCode = value
        End Set
    End Property
    Public Property CourseCode
        Get
            Return _CourseCode
        End Get
        Set(ByVal value)
            _CourseCode = value
        End Set
    End Property
    Public Property PositionId
        Get
            Return _PositionId
        End Get
        Set(ByVal value)
            _PositionId = value
        End Set
    End Property
    Public Property DeptCode
        Get
            Return _DeptCode
        End Get
        Set(ByVal value)
            _DeptCode = value
        End Set
    End Property
    Public Property Fromdt
        Get
            Return _Fromdt
        End Get
        Set(ByVal value)
            _Fromdt = value
        End Set
    End Property
    Public Property Todt
        Get
            Return _Todt
        End Get
        Set(ByVal value)
            _Todt = value
        End Set
    End Property
    Public Property Status
        Get
            Return _Status
        End Get
        Set(ByVal value)
            _Status = value
        End Set
    End Property
    Public Property ApplyCode
        Get
            Return _ApplyCode
        End Get
        Set(ByVal value)
            _ApplyCode = value
        End Set
    End Property
End Class
