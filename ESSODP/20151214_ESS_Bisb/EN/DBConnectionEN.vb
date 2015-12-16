Imports System
Imports System.Data
Imports System.Data.SqlClient
Public Class DBConnectionEN
    Private _ServerName As String
    Private _ServerType As String
    Private _SQLCompany As String
    Private _UID As String
    Private _PWD As String
    Private _CUID As String
    Private _CPWD As String
    Private _AUID As String
    Private _APWD As String
    Private _License As String
    Private _GetConnection As String
    Private _HanaServerName As String
    Private _HanaLoginName As String
    Private _HanaPassword As String
    Public Property HANAServerName()
        Get
            Return _HanaServerName
        End Get
        Set(ByVal value)
            _HanaServerName = value
        End Set
    End Property
    Public Property HANALoginName()
        Get
            Return _HanaLoginName
        End Get
        Set(ByVal value)
            _HanaLoginName = value
        End Set
    End Property
    Public Property HANAPassword()
        Get
            Return _HanaPassword
        End Get
        Set(ByVal value)
            _HanaPassword = value
        End Set
    End Property
    Public Property DBConnection()
        Get
            Return _GetConnection
        End Get
        Set(ByVal value)
            _GetConnection = value
        End Set
    End Property
    Public Property AdminUid()
        Get
            Return _AUID
        End Get
        Set(ByVal value)
            _AUID = value
        End Set
    End Property

    Public Property AdminPwd()
        Get
            Return _APWD
        End Get
        Set(ByVal value)
            _APWD = value
        End Set
    End Property
    Public Property License()
        Get
            Return _License
        End Get
        Set(ByVal value)
            _License = value
        End Set
    End Property

    Public Property ServerName()
        Get
            Return _ServerName
        End Get
        Set(ByVal value)
            _ServerName = value
        End Set
    End Property
    Public Property ServerType()
        Get
            Return _ServerType
        End Get
        Set(ByVal value)
            _ServerType = value
        End Set
    End Property
    Public Property SQLCompany()
        Get
            Return _SQLCompany
        End Get
        Set(ByVal value)
            _SQLCompany = value
        End Set
    End Property
    Public Property UID()
        Get
            Return _UID
        End Get
        Set(ByVal value)
            _UID = value
        End Set
    End Property
    Public Property PWD()
        Get
            Return _PWD
        End Get
        Set(ByVal value)
            _PWD = value
        End Set
    End Property
    Public Property CUID()
        Get
            Return _CUID
        End Get
        Set(ByVal value)
            _CUID = value
        End Set
    End Property
    Public Property CPWD()
        Get
            Return _CPWD
        End Get
        Set(ByVal value)
            _CPWD = value
        End Set
    End Property
End Class
