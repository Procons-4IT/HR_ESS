Public Class NewTrainingEN
    Private _strQry As String
    Private _ReqCode As String
    Private _EmpId As String
    Private _EmpName As String
    Private _PositionId As String
    Private _DeptCode As String
    Private _PositionName As String
    Private _DeptName As String
    Private _TrainTitle As String
    Private _Justify As String
    Private _TrainCost As Double
    Private _Expense As Double
    Private _Fromdt As Date
    Private _Todt As Date
    Private _TrLocation As String
    Private _TraDurBuss As Double
    Private _TraDurCal As Double
    Private _AwayoffBus As Double
    Private _TestAvail As String
    Private _TestIncl As String
    Private _LveDutyOn As Date
    Private _TravelOn As Date
    Private _ReturnOn As Date
    Private _ResumesOn As Date
    Private _Notes As String
    Private _Status As String
    Private _SapCompany As SAPbobsCOM.Company
    Public Property SapCompany() As SAPbobsCOM.Company
        Get
            Return _SapCompany
        End Get
        Set(ByVal value As SAPbobsCOM.Company)
            _SapCompany = value
        End Set
    End Property
    Public Property StrQry() As String
        Get
            Return _strQry
        End Get
        Set(ByVal value As String)
            _strQry = value
        End Set
    End Property
    Public Property ReqCode() As String
        Get
            Return _ReqCode
        End Get
        Set(ByVal value As String)
            _ReqCode = value
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
    Public Property EmpName() As String
        Get
            Return _EmpName
        End Get
        Set(ByVal value As String)
            _EmpName = value
        End Set
    End Property
    Public Property PositionId() As String
        Get
            Return _PositionId
        End Get
        Set(ByVal value As String)
            _PositionId = value
        End Set
    End Property
    Public Property PositionName() As String
        Get
            Return _PositionName
        End Get
        Set(ByVal value As String)
            _PositionName = value
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
    Public Property DeptName() As String
        Get
            Return _DeptName
        End Get
        Set(ByVal value As String)
            _DeptName = value
        End Set
    End Property
    Public Property TrainTitle() As String
        Get
            Return _TrainTitle
        End Get
        Set(ByVal value As String)
            _TrainTitle = value
        End Set
    End Property
    Public Property Justification() As String
        Get
            Return _Justify
        End Get
        Set(ByVal value As String)
            _Justify = value
        End Set
    End Property
    Public Property TrainCost() As Double
        Get
            Return _TrainCost
        End Get
        Set(ByVal value As Double)
            _TrainCost = value
        End Set
    End Property
    Public Property Expense() As Double
        Get
            Return _Expense
        End Get
        Set(ByVal value As Double)
            _Expense = value
        End Set
    End Property
    Public Property Fromdate() As Date
        Get
            Return _Fromdt
        End Get
        Set(ByVal value As Date)
            _Fromdt = value
        End Set
    End Property
    Public Property Todate() As Date
        Get
            Return _Todt
        End Get
        Set(ByVal value As Date)
            _Todt = value
        End Set
    End Property
    Public Property TrainLoc() As String
        Get
            Return _TrLocation
        End Get
        Set(ByVal value As String)
            _TrLocation = value
        End Set
    End Property
    Public Property TrainDurBus() As Double
        Get
            Return _TraDurBuss
        End Get
        Set(ByVal value As Double)
            _TraDurBuss = value
        End Set
    End Property
    Public Property TrainDurCal() As Double
        Get
            Return _TraDurCal
        End Get
        Set(ByVal value As Double)
            _TraDurCal = value
        End Set
    End Property
    Public Property AwayoffBus() As Double
        Get
            Return _AwayoffBus
        End Get
        Set(ByVal value As Double)
            _AwayoffBus = value
        End Set
    End Property
    Public Property TestAvail() As String
        Get
            Return _TestAvail
        End Get
        Set(ByVal value As String)
            _TestAvail = value
        End Set
    End Property
    Public Property TestInclude() As String
        Get
            Return _TestIncl
        End Get
        Set(ByVal value As String)
            _TestIncl = value
        End Set
    End Property
    Public Property LveDutyOn() As Date
        Get
            Return _LveDutyOn
        End Get
        Set(ByVal value As Date)
            _LveDutyOn = value
        End Set
    End Property
    Public Property TravelOn() As Date
        Get
            Return _TravelOn
        End Get
        Set(ByVal value As Date)
            _TravelOn = value
        End Set
    End Property
    Public Property ReturnOn() As Date
        Get
            Return _ReturnOn
        End Get
        Set(ByVal value As Date)
            _ReturnOn = value
        End Set
    End Property
    Public Property ResumesOn() As Date
        Get
            Return _ResumesOn
        End Get
        Set(ByVal value As Date)
            _ResumesOn = value
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
    Public Property Status() As String
        Get
            Return _Status
        End Get
        Set(ByVal value As String)
            _Status = value
        End Set
    End Property
End Class
