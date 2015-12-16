Public Class ActivityEN
    Private _AddUpdate As String
    Private _DBName As String
    Private _EmpId As String
    Private _ActType As String
    Private _DocNum As String
    Private _Subject As String
    Private _FromDatetime As String
    Private _ToDatetime As String
    Private _FromDate As Date
    Private _ToDate As Date
    Private _Status As String
    Private _Duration As String
    Private _Recurrence As String
    Private _Remarks As String
    Private _Priority As String
    Private _Content As String
    Private _Assaignedemp As String
    Private _StartDate As String
    Private _EndDate As String
    Private _DurType As String
    Private _AUser As String
    Private _AMessage As String
    Private _SAPCompany As SAPbobsCOM.Company

    Public Property SAPCompany() As SAPbobsCOM.Company
        Get
            Return _SAPCompany
        End Get
        Set(ByVal value As SAPbobsCOM.Company)
            _SAPCompany = value
        End Set
    End Property
    Public Property AddUpdate() As String
        Get
            Return _AddUpdate
        End Get
        Set(ByVal value As String)
            _AddUpdate = value
        End Set
    End Property
    Public Property AUser() As String
        Get
            Return _AUser
        End Get
        Set(ByVal value As String)
            _AUser = value
        End Set
    End Property
    Public Property AMessage() As String
        Get
            Return _AMessage
        End Get
        Set(ByVal value As String)
            _AMessage = value
        End Set
    End Property
    Public Property DurType() As String
        Get
            Return _DurType
        End Get
        Set(ByVal value As String)
            _DurType = value
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
    Public Property Priority() As String
        Get
            Return _Priority
        End Get
        Set(ByVal value As String)
            _Priority = value
        End Set
    End Property
    Public Property Content() As String
        Get
            Return _Content
        End Get
        Set(ByVal value As String)
            _Content = value
        End Set
    End Property
    Public Property Assaignedemp() As String
        Get
            Return _Assaignedemp
        End Get
        Set(ByVal value As String)
            _Assaignedemp = value
        End Set
    End Property
    Public Property Recurrence() As String
        Get
            Return _Recurrence
        End Get
        Set(ByVal value As String)
            _Recurrence = value
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
    Public Property Status() As String
        Get
            Return _Status
        End Get
        Set(ByVal value As String)
            _Status = value
        End Set
    End Property
    Public Property Duration() As String
        Get
            Return _Duration
        End Get
        Set(ByVal value As String)
            _Duration = value
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
    Public Property Totime() As String
        Get
            Return _ToDatetime
        End Get
        Set(ByVal value As String)
            _ToDatetime = value
        End Set
    End Property
    Public Property Fromtime() As String
        Get
            Return _FromDatetime
        End Get
        Set(ByVal value As String)
            _FromDatetime = value
        End Set
    End Property
    Public Property Subject() As String
        Get
            Return _Subject
        End Get
        Set(ByVal value As String)
            _Subject = value
        End Set
    End Property
    Public Property DocNum() As String
        Get
            Return _DocNum
        End Get
        Set(ByVal value As String)
            _DocNum = value
        End Set
    End Property
    Public Property DBName() As String
        Get
            Return _DBName
        End Get
        Set(ByVal value As String)
            _DBName = value
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
    Public Property ActType() As String
        Get
            Return _ActType
        End Get
        Set(ByVal value As String)
            _ActType = value
        End Set
    End Property
End Class
