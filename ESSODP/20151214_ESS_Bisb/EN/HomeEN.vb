Imports Microsoft.VisualBasic
Imports System
Imports System.Web
Public Class HomeEN
    Private _Strqry As String
    Private _EmpId As String
    Private _EmpName As String
    Private _DeptCode As String
    Private _DeptName As String
    Private _EmpPosId As String
    Private _EmpPosName As String
    Private _ReqCode As String
    Private _ReqPosId As String
    Private _ReqPosName As String
    Private _ReqDeptId As String
    Private _ReqDeptName As String
    Private _AppStatus As String
    Private _Manager As String
    Private _SapCompany As SAPbobsCOM.Company
    Private _UserCode As String
    Public Property UserCode() As String
        Get
            Return _UserCode
        End Get
        Set(ByVal value As String)
            _UserCode = value
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
    Public Property Manager
        Get
            Return _Manager
        End Get
        Set(ByVal value)
            _Manager = value
        End Set
    End Property
    Public Property AppStatus
        Get
            Return _AppStatus
        End Get
        Set(ByVal value)
            _AppStatus = value
        End Set
    End Property
    Public Property StrQry
        Get
            Return _Strqry
        End Get
        Set(ByVal value)
            _Strqry = value
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
    Public Property EmpName
        Get
            Return _EmpName
        End Get
        Set(ByVal value)
            _EmpName = value
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
    Public Property DeptName
        Get
            Return _DeptName
        End Get
        Set(ByVal value)
            _DeptName = value
        End Set
    End Property
    Public Property EmpPosCode
        Get
            Return _EmpPosId
        End Get
        Set(ByVal value)
            _EmpPosId = value
        End Set
    End Property
    Public Property EmpPosName
        Get
            Return _EmpPosName
        End Get
        Set(ByVal value)
            _EmpPosName = value
        End Set
    End Property
    Public Property ReqposCode
        Get
            Return _ReqPosId
        End Get
        Set(ByVal value)
            _ReqPosId = value
        End Set
    End Property
    Public Property ReqPosName
        Get
            Return _ReqPosName
        End Get
        Set(ByVal value)
            _EmpPosName = value
        End Set
    End Property
    Public Property RequestNo
        Get
            Return _ReqCode
        End Get
        Set(ByVal value)
            _ReqCode = value
        End Set
    End Property
    Public Property ReqDeptCode
        Get
            Return _ReqDeptId
        End Get
        Set(ByVal value)
            _ReqDeptId = value
        End Set
    End Property
    Public Property ReqDeptName
        Get
            Return _ReqDeptName
        End Get
        Set(ByVal value)
            _ReqDeptName = value
        End Set
    End Property
End Class
