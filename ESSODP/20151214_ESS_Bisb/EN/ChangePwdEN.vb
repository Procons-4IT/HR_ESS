Public Class ChangePwdEN
    Private _Empid As String
    Private _OldPwd As String
    Private _NewPwd As String
    Private _ConfirmPwd As String
    Private _strmsg As String
    Private _Formid As String
    Public Property Formid As String
        Get
            Return _Formid
        End Get
        Set(ByVal value As String)
            _Formid = value
        End Set
    End Property
    Public Property strmsg As String
        Get
            Return _strmsg
        End Get
        Set(ByVal value As String)
            _strmsg = value
        End Set
    End Property
    Public Property EmpId As String
        Get
            Return _Empid
        End Get
        Set(ByVal value As String)
            _Empid = value
        End Set
    End Property
    Public Property OldPwd As String
        Get
            Return _OldPwd
        End Get
        Set(ByVal value As String)
            _OldPwd = value
        End Set
    End Property
    Public Property NewPwd As String
        Get
            Return _NewPwd
        End Get
        Set(ByVal value As String)
            _NewPwd = value
        End Set
    End Property
    Public Property ConfirmPwd As String
        Get
            Return _ConfirmPwd
        End Get
        Set(ByVal value As String)
            _ConfirmPwd = value
        End Set
    End Property
End Class
