Public Class DynamicApprovalEN
    Private _UserCode As String
    Private _HeaderType As String
    Private _HistoryType As String
    Private _DocEntry As String
    Private _EmpId As String
    Private _Year As Integer
    Private _Month As Integer
    Private _AppStatus As String
    Private _Remarks As String
    Private _EmpUserId As Integer
    Private _DocMessage As String
    Private _HeadDocEntry As String
    Private _HeadLineId As String
    Private _SapCompany As SAPbobsCOM.Company
    Private _IntEmpInd As String
    Private _IntReqNo As String
    Private _LeaveCode As String
    Private _PostingType As String
    Public Property PostingType() As String
        Get
            Return _PostingType
        End Get
        Set(ByVal value As String)
            _PostingType = value
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
    Public Property IntReqNo() As String
        Get
            Return _IntReqNo
        End Get
        Set(ByVal value As String)
            _IntReqNo = value
        End Set
    End Property
    Public Property InternalEmpInd() As String
        Get
            Return _IntEmpInd
        End Get
        Set(ByVal value As String)
            _IntEmpInd = value
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
    Public Property HeadDocEntry() As String
        Get
            Return _HeadDocEntry
        End Get
        Set(ByVal value As String)
            _HeadDocEntry = value
        End Set
    End Property
    Public Property HeadLineId() As String
        Get
            Return _HeadLineId
        End Get
        Set(ByVal value As String)
            _HeadLineId = value
        End Set
    End Property
    Public Property DocMessage() As String
        Get
            Return _DocMessage
        End Get
        Set(ByVal value As String)
            _DocMessage = value
        End Set
    End Property
    Public Property EmpUserId() As Integer
        Get
            Return _EmpUserId
        End Get
        Set(ByVal value As Integer)
            _EmpUserId = value
        End Set
    End Property
    Public Property UserCode() As String
        Get
            Return _UserCode
        End Get
        Set(ByVal value As String)
            _UserCode = value
        End Set
    End Property
    Public Property HeaderType() As String
        Get
            Return _HeaderType
        End Get
        Set(ByVal value As String)
            _HeaderType = value
        End Set
    End Property
    Public Property HistoryType() As String
        Get
            Return _HistoryType
        End Get
        Set(ByVal value As String)
            _HistoryType = value
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
    Public Property EmpId() As String
        Get
            Return _EmpId
        End Get
        Set(ByVal value As String)
            _EmpId = value
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
    Public Property Month() As Integer
        Get
            Return _Month
        End Get
        Set(ByVal value As Integer)
            _Month = value
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
    Public Property Remarks() As String
        Get
            Return _Remarks
        End Get
        Set(ByVal value As String)
            _Remarks = value
        End Set
    End Property

End Class
