Public Class LeaveRequestEN
    Private _strCode As String
    Private _Empid As String
    Private _EmpName As String
    Private _TransType As String
    Private _LeaveCode As String
    Private _StartDate As String
    Private _EndDate As String
    Private _NoofDays As String
    Private _Notes As String
    Private _Month As Integer
    Private _Year As Integer
    Private _RejoinDate As String
    Private _OffCycle As String
    Private _Status As String
    Private _ApprovedBy As String
    Private _AppRemarks As String
    Private _ApprDate As String
    Private _RetJoiNDate As String
    Private _RStatus As String
    Private _RApprovedBy As String
    Private _RAppRemarks As String
    Private _RApprDate As String
    Private _FromDate As Date
    Private _ToDate As Date
    Private _RejoinDt As Date
    Private _Fromtime As String
    Private _Totime As String
    Private _FromDatetime As DateTime
    Private _ToDatetime As DateTime
    Private _LveBal As Integer
    Private _LeaveName As String
    Private _TotalLeave As Double
    Private _SapCompany As SAPbobsCOM.Company
    Private _CutOff As String
    Public Property SapCompany() As SAPbobsCOM.Company
        Get
            Return _SapCompany
        End Get
        Set(ByVal value As SAPbobsCOM.Company)
            _SapCompany = value
        End Set
    End Property
    Public Property CutOff() As String
        Get
            Return _CutOff
        End Get
        Set(ByVal value As String)
            _CutOff = value
        End Set
    End Property
    Public Property TotalLeave() As Double
        Get
            Return _TotalLeave
        End Get
        Set(ByVal value As Double)
            _TotalLeave = value
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
    Public Property LeaveBalance() As Integer
        Get
            Return _LveBal
        End Get
        Set(ByVal value As Integer)
            _LveBal = value
        End Set
    End Property
    Public Property FromDatetime() As DateTime
        Get
            Return _FromDatetime
        End Get
        Set(ByVal value As DateTime)
            _FromDatetime = value
        End Set
    End Property
    Public Property ToDatetime() As DateTime
        Get
            Return _ToDatetime
        End Get
        Set(ByVal value As DateTime)
            _ToDatetime = value
        End Set
    End Property
    Public Property Fromtime() As String
        Get
            Return _Fromtime
        End Get
        Set(ByVal value As String)
            _Fromtime = value
        End Set
    End Property
    Public Property Totime() As String
        Get
            Return _Totime
        End Get
        Set(ByVal value As String)
            _Totime = value
        End Set
    End Property
    Public Property RejoinDt() As Date
        Get
            Return _RejoinDt
        End Get
        Set(ByVal value As Date)
            _RejoinDt = value
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
    Public Property strCode() As String
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
    Public Property TransType() As String
        Get
            Return _TransType
        End Get
        Set(ByVal value As String)
            _TransType = value
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
    Public Property StartDate() As String
        Get
            Return _StartDate
        End Get
        Set(ByVal value As String)
            _StartDate = value
        End Set
    End Property
    Public Property EndDate() As String
        Get
            Return _EndDate
        End Get
        Set(ByVal value As String)
            _EndDate = value
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
    Public Property Notes() As String
        Get
            Return _Notes
        End Get
        Set(ByVal value As String)
            _Notes = value
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
    Public Property RejoinDate() As String
        Get
            Return _RejoinDate
        End Get
        Set(ByVal value As String)
            _RejoinDate = value
        End Set
    End Property
    Public Property OffCycle() As String
        Get
            Return _OffCycle
        End Get
        Set(ByVal value As String)
            _OffCycle = value
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
    Public Property ApprovedBy() As String
        Get
            Return _ApprovedBy
        End Get
        Set(ByVal value As String)
            _ApprovedBy = value
        End Set
    End Property
    Public Property AppRemarks() As String
        Get
            Return _AppRemarks
        End Get
        Set(ByVal value As String)
            _AppRemarks = value
        End Set
    End Property
    Public Property ApprDate() As String
        Get
            Return _ApprDate
        End Get
        Set(ByVal value As String)
            _ApprDate = value
        End Set
    End Property

    Public Property RetJoiNDate() As String
        Get
            Return _RetJoiNDate
        End Get
        Set(ByVal value As String)
            _RetJoiNDate = value
        End Set
    End Property
    Public Property RStatus() As String
        Get
            Return _RStatus
        End Get
        Set(ByVal value As String)
            _RStatus = value
        End Set
    End Property
    Public Property RApprovedBy() As String
        Get
            Return _RApprovedBy
        End Get
        Set(ByVal value As String)
            _RApprovedBy = value
        End Set
    End Property
    Public Property RAppRemarks() As String
        Get
            Return _RAppRemarks
        End Get
        Set(ByVal value As String)
            _RAppRemarks = value
        End Set
    End Property
    Public Property RApprDate() As String
        Get
            Return _RApprDate
        End Get
        Set(ByVal value As String)
            _RApprDate = value
        End Set
    End Property
End Class
