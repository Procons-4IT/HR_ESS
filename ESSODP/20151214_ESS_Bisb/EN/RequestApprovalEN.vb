Public Class RequestApprovalEN
    Private _Empid As String
    Private _EmpName As String
    Private _strCode As String
    Private _Status As String
    Private _Approveby As String
    Private _ApproveRemarks As String
    Private _Month As Integer
    Private _Year As Integer
    Private _FromDate As String
    Private _ToDate As String
    Private _RejoinDate As String
    Private _ApproveDate As String
    Private _NoofDays As String
    Private _LeaveCode As String
    Private _Reason As String
    Private _TransType As String
    Private _dtFromDate As Date
    Public Property dtFromDate() As Date
        Get
            Return _dtFromDate
        End Get
        Set(ByVal value As Date)
            _dtFromDate = value
        End Set
    End Property
    Public Property TransType() As String
        Get
            Return _TransType
        End Get
        Set(ByVal value As String)
            _TransType = value
        End Set
    End Property
    Public Property Reason() As String
        Get
            Return _Reason
        End Get
        Set(ByVal value As String)
            _Reason = value
        End Set
    End Property
    Public Property RejoinDate() As String
        Get
            Return _RejoinDate
        End Get
        Set(ByVal value As String)
            _RejoinDate = value
        End Set
    End Property
    Public Property FromDate() As String
        Get
            Return _FromDate
        End Get
        Set(ByVal value As String)
            _FromDate = value
        End Set
    End Property
    Public Property ToDate() As String
        Get
            Return _ToDate
        End Get
        Set(ByVal value As String)
            _ToDate = value
        End Set
    End Property
    Public Property NoofDays() As String
        Get
            Return _NoofDays
        End Get
        Set(ByVal value As String)
            _NoofDays = value
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
    Public Property ApproveDate() As String
        Get
            Return _ApproveDate
        End Get
        Set(ByVal value As String)
            _ApproveDate = value
        End Set
    End Property
    Public Property Month() As Integer
        Get
            Return _Month
        End Get
        Set(ByVal value As Integer)
            _Month = value
        End Set
    End Property
    Public Property Year() As Integer
        Get
            Return _Year
        End Get
        Set(ByVal value As Integer)
            _Year = value
        End Set
    End Property
    Public Property EmpCode() As String
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
    Public Property Code() As String
        Get
            Return _strCode
        End Get
        Set(ByVal value As String)
            _strCode = value
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
    Public Property Approveby() As String
        Get
            Return _Approveby
        End Get
        Set(ByVal value As String)
            _Approveby = value
        End Set
    End Property
    Public Property ApproveRemarks() As String
        Get
            Return _ApproveRemarks
        End Get
        Set(ByVal value As String)
            _ApproveRemarks = value
        End Set
    End Property
End Class
