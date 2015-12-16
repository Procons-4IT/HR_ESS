Public Class MgrTrainReqApprovalEN
    Private _Empid As String
    Private _TrainCode As String
    Private _CourseCode As String
    Private _MgrStatus As String
    Private _MgrRemarks As String
    Private _Code As String
    Private _Condition As String
    Public Property Condition As String
        Get
            Return _Condition
        End Get
        Set(ByVal value As String)
            _Condition = value
        End Set
    End Property
    Public Property Code As String
        Get
            Return _Code
        End Get
        Set(ByVal value As String)
            _Code = value
        End Set
    End Property
    Public Property MgrStatus As String
        Get
            Return _MgrStatus
        End Get
        Set(ByVal value As String)
            _MgrStatus = value
        End Set
    End Property
    Public Property MgrRemarks As String
        Get
            Return _MgrRemarks
        End Get
        Set(ByVal value As String)
            _MgrRemarks = value
        End Set
    End Property
    Public Property EmpID As String
        Get
            Return _Empid
        End Get
        Set(ByVal value As String)
            _Empid = value
        End Set
    End Property
    Public Property TrainCode As String
        Get
            Return _TrainCode
        End Get
        Set(ByVal value As String)
            _TrainCode = value
        End Set
    End Property
    Public Property CourseCode As String
        Get
            Return _CourseCode
        End Get
        Set(ByVal value As String)
            _CourseCode = value
        End Set
    End Property


End Class
