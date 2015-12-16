Imports Microsoft.VisualBasic
Imports System
Imports System.Web
Imports System.Xml
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI.WebControls


Public Class CommonFunctions
    Dim objDA As DBConnectionDA = New DBConnectionDA()
    Public Sub New()
        objDA.con = New SqlConnection(objDA.GetConnection)
    End Sub
    Public Sub Dropdown1(ByVal query As String, ByVal valcode As String, ByVal valname As String, ByVal ddl As DropDownList)
        objDA.sqlda = New SqlDataAdapter(query, objDA.con)
        objDA.sqlda.Fill(objDA.ds)
        If objDA.ds.Tables(0).Rows.Count > 0 Then
            ddl.DataTextField = valname
            ddl.DataValueField = valcode
            ddl.DataSource = objDA.ds
            ddl.DataBind()
            ddl.Items.Insert(0, "--Select--")
        Else
            ddl.DataBind()
            ddl.Items.Insert(0, "--Select--")
        End If
    End Sub
    Public Sub GridviewBind(ByVal query As String, ByVal Gv As GridView)
       objDA.sqlda = New SqlDataAdapter(query, objDA.con)
        objDA.sqlda.Fill(objDA.ds)
        If objDA.ds.Tables(0).Rows.Count > 0 Then
            Gv.DataSource = objDA.ds
            Gv.DataBind()
        Else
            Gv.DataBind()
        End If
    End Sub
    Public Function GridviewBindLeave() As DataSet
        objDA.sqlda = New SqlDataAdapter("SELECT ""Code"", ""Name"" from ""@Z_PAY_LEAVE""", objDA.con)
        objDA.sqlda.Fill(objDA.ds1)
        Return objDA.ds1
    End Function
    Public Function Getmaxcode(ByVal sTable As String, ByVal sColumn As String) As Integer
        Dim MaxCode As Integer
        objDA.con.Open()
        objDA.cmd = New SqlCommand("SELECT isnull(max(isnull(CAST(isnull(" & sColumn & ",'0') AS Numeric),0)),0) FROM " & sTable & "", objDA.con)
        objDA.cmd.CommandType = CommandType.Text
        MaxCode = Convert.ToString(objDA.cmd.ExecuteScalar())
        If MaxCode >= 0 Then
            MaxCode = MaxCode + 1
        Else
            MaxCode = 1
        End If
        objDA.con.Close()
        Return MaxCode
    End Function
    Public Function ReturnDataset(ByVal objDA As DBConnectionDA) As DataSet
        objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
        Dim dss As DataSet = New DataSet()
        objDA.sqlda.Fill(objDA.ds)
        Return objDA.ds
    End Function
    Public Function checktable() As String
        Dim exists As Integer = 0
        objDA.strQuery = "SELECT COUNT(*) FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[U_CONTACTUS]') AND type in (N'U')"
        objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
        objDA.con.Open()
        objDA.sqlreader = objDA.cmd.ExecuteReader()
        If objDA.sqlreader.HasRows Then
            Do
                While objDA.sqlreader.Read
                    exists = objDA.sqlreader(0)
                End While
            Loop While objDA.sqlreader.NextResult()
        End If
        objDA.con.Close()
        If exists = 0 Then
            objDA.strQuery = "CREATE TABLE U_CONTACTUS(U_DocEntry INT IDENTITY NOT NULL,U_ComapnyId nvarchar(50) NOT NULL,U_Empname nvarchar(100) NULL,"
            objDA.strQuery += " U_Position nvarchar(100) NULL,U_Email nvarchar(100) NULL,U_phone nvarchar(30) NULL,U_Status [char](1) NULL)"
            objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
            objDA.con.Open()
            objDA.cmd.ExecuteNonQuery()
            objDA.con.Close()
        End If
        exists = 0
        objDA.strQuery = "SELECT COUNT(*) FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[U_PEOPLEOBJ]') AND type in (N'U')"
        objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
        objDA.con.Open()
        objDA.sqlreader = objDA.cmd.ExecuteReader()
        If objDA.sqlreader.HasRows Then
            Do
                While objDA.sqlreader.Read
                    exists = objDA.sqlreader(0)
                End While
            Loop While objDA.sqlreader.NextResult()
        End If
        objDA.con.Close()
        If exists = 0 Then
            'objDA.strQuery = "CREATE TABLE U_PEOPLEOBJ(U_DocEntry INT IDENTITY NOT NULL,U_Empid nvarchar(50) NOT NULL,U_PeoobjCode nvarchar(50) NULL,U_RefNo [nvarchar](20) NULL,"
            'objDA.strQuery += " U_PeoobjName nvarchar(100) NULL,U_PeoCategory nvarchar(100) NULL,U_Weight Numeric NULL,U_Remarks [nvarchar](Max) NULL,U_Status [char](1) NULL,U_TypeAction [nvarchar](30) NULL)"
            objDA.strQuery = "CREATE TABLE U_PEOPLEOBJ(U_DocEntry INT IDENTITY NOT NULL,U_Empid nvarchar(50) NOT NULL,U_DeptCode nvarchar(50) NOT NULL,U_PeoobjCode nvarchar(50) NULL,U_RefNo [nvarchar](20) NULL,U_Empname nvarchar(200) NULL,"
            objDA.strQuery += " U_PeoobjName nvarchar(100) NULL,U_PeoCategory nvarchar(100) NULL,U_PeoCatDesc nvarchar(200) NULL,U_Weight Decimal(18,6) NULL,U_Remarks [nvarchar](Max) NULL,U_TypeAction [nvarchar](30) NULL, U_Z_AppStatus nvarchar(40) NULL,"
            objDA.strQuery += " U_CurApprover nvarchar(100) NULL,U_NxtApprover nvarchar(100) NULL,U_Z_AppRequired nvarchar(40) NULL,U_Z_AppReqDate datetime NULL,"
            objDA.strQuery += " U_Z_ReqTime nvarchar(40) NULL,U_Z_ApproveId nvarchar(40) NULL)"
            objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
            objDA.con.Open()
            objDA.cmd.ExecuteNonQuery()
            objDA.con.Close()
        End If
        exists = 0
        objDA.strQuery = "SELECT COUNT(*) FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[U_VACPOSITION]') AND type in (N'U')"
        objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
        objDA.con.Open()
        objDA.sqlreader = objDA.cmd.ExecuteReader()
        If objDA.sqlreader.HasRows Then
            Do
                While objDA.sqlreader.Read
                    exists = objDA.sqlreader(0)
                End While
            Loop While objDA.sqlreader.NextResult()
        End If
        objDA.con.Close()
        If exists = 0 Then
            'objDA.strQuery = "CREATE TABLE U_VACPOSITION(U_DocEntry INT IDENTITY NOT NULL,U_Empid nvarchar(50) NOT NULL,U_Empname nvarchar(100) NULL,U_EmpPosCode nvarchar(50) NULL,U_EmpPosName nvarchar(50) NULL,"
            'objDA.strQuery += " U_EmpdeptCode nvarchar(40) NULL,U_EmpdeptName nvarchar(40) NULL,U_ReqdeptCode nvarchar(40) NULL,U_ReqdeptName nvarchar(40) NULL,U_ReqPosCode nvarchar(50) NULL,U_ReqPosName nvarchar(50) NULL,"
            'objDA.strQuery += "U_RequestCode nvarchar(20) null, U_Remarks [nvarchar](Max) NULL,U_ApplyDate date,U_Approved Date, U_Status [char](1) NULL)"
            objDA.strQuery = "create table [U_VACPOSITION](U_DocEntry int identity NOT NULL, U_Empid nvarchar(100) NULL, U_Empname nvarchar(200) NULL, U_EmpPosCode nvarchar(40) NULL,"
            objDA.strQuery += " U_EmpPosName nvarchar(200) NULL, U_EmpdeptCode nvarchar(20) NULL, U_EmpdeptName nvarchar(200) NULL, "
            objDA.strQuery += " U_ReqdeptCode nvarchar(20) NULL, U_ReqdeptName nvarchar(200) NULL, U_ReqPosCode nvarchar(40) NULL, U_Remarks nvarchar(200) NULL,"
            objDA.strQuery += " U_ReqPosName nvarchar(200) NULL, U_RequestCode nvarchar(40) NULL, U_ApplyDate datetime NULL, U_Z_AppStatus nvarchar(40) NULL,"
            objDA.strQuery += " U_CurApprover nvarchar(100) NULL,U_NxtApprover nvarchar(100) NULL,U_Z_AppRequired nvarchar(40) NULL,U_Z_AppReqDate datetime NULL,"
            objDA.strQuery += " U_Z_ReqTime nvarchar(40) NULL,U_Z_ApproveId nvarchar(40) NULL)"
            objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
            objDA.con.Open()
            objDA.cmd.ExecuteNonQuery()
            objDA.con.Close()
        End If
        exists = 0
        objDA.strQuery = "SELECT COUNT(*) FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[U_HR_SESSION]') AND type in (N'U')"
        objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
        objDA.cmd.Connection.Open()
        objDA.sqlreader = objDA.cmd.ExecuteReader()
        If objDA.sqlreader.HasRows Then
            Do
                While objDA.sqlreader.Read
                    exists = objDA.sqlreader(0)
                End While
            Loop While objDA.sqlreader.NextResult()
        End If
        objDA.cmd.Connection.Close()
        If exists = 0 Then
            objDA.strQuery = "CREATE TABLE [dbo].[U_HR_SESSION]([U_SESSIONID] [int] IDENTITY(1,1) NOT NULL,[U_EmpCode] [nvarchar](max) NOT NULL, [U_LOGIN_DATE] DATETIME, [U_LOGOUT_DATE] DATETIME)"
            objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
            objDA.cmd.Connection.Open()
            objDA.cmd.ExecuteNonQuery()
            objDA.cmd.Connection.Close()
        End If

        exists = 0
        objDA.strQuery = "SELECT COUNT(*) FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[U_EXPCLAIM]') AND type in (N'U')"
        objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
        objDA.con.Open()
        objDA.sqlreader = objDA.cmd.ExecuteReader()
        If objDA.sqlreader.HasRows Then
            Do
                While objDA.sqlreader.Read
                    exists = objDA.sqlreader(0)
                End While
            Loop While objDA.sqlreader.NextResult()
        End If
        objDA.con.Close()
        If exists = 0 Then
            objDA.strQuery = "create table [U_EXPCLAIM](U_DocEntry int identity NOT NULL,U_SessionId nvarchar(100) NULL,U_TANo nvarchar(100) NULL, U_Empid nvarchar(100) NULL, U_Empname nvarchar(200) NULL, U_SubDate datetime NULL,"
            objDA.strQuery += " U_Client nvarchar(200) NULL, U_Project nvarchar(20) NULL, U_ClimDate datetime NOT NULL, "
            objDA.strQuery += " U_City nvarchar(150) NULL, U_Currency nvarchar(100) NULL, U_CurAmt Decimal(18,6) NULL, U_ExcRate Decimal(18,6) NULL,"
            objDA.strQuery += " U_UsdAmt nvarchar(200) NULL, U_ReImbused nvarchar(10) NULL, U_ReImAmt nvarchar(100) NULL, U_ExpCode nvarchar(100) NULL, U_ExpName nvarchar(200) NULL, U_PayMethod nvarchar(20) NULL, U_Notes nvarchar(max) NULL,"
            objDA.strQuery += " U_AppStatus nvarchar(10) NULL,U_TraCode nvarchar(100) NULL,U_TraDesc nvarchar(200) NULL,U_Attachment nvarchar(max) NULL,"
            objDA.strQuery += " U_TripType nvarchar(40) NULL,U_AllCode nvarchar(100) NULL,U_Year int NULL,U_Month int NULL,U_PayPosted nvarchar(2) NULL,U_DocRefNo nvarchar(40) NULL,U_Code nvarchar(40) NULL,U_DebitCode nvarchar(40) NULL,U_CreditCode nvarchar(40) NULL,U_Posting nvarchar(40) NULL"
            objDA.strQuery += " ,U_CardCode nvarchar(40) NULL,U_Dimension nvarchar(40) NULL,U_JVNo nvarchar(40) NULL)"
            objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
            objDA.con.Open()
            objDA.cmd.ExecuteNonQuery()
            objDA.con.Close()
        End If
        exists = 0
        objDA.strQuery = "SELECT COUNT(*) FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[U_LOANREQ]') AND type in (N'U')"
        objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
        objDA.con.Open()
        objDA.sqlreader = objDA.cmd.ExecuteReader()
        If objDA.sqlreader.HasRows Then
            Do
                While objDA.sqlreader.Read
                    exists = objDA.sqlreader(0)
                End While
            Loop While objDA.sqlreader.NextResult()
        End If
        objDA.con.Close()
        If exists = 0 Then
            objDA.strQuery = "CREATE TABLE U_LOANREQ(U_DocEntry INT IDENTITY NOT NULL,U_Empid nvarchar(50) NOT NULL,U_Empname nvarchar(200) NULL,U_LoanCode nvarchar(50) NOT NULL,U_LoanName nvarchar(50) NULL,U_RefNo [nvarchar](20) NULL,"
            objDA.strQuery += " U_ReqDate DateTime NULL,U_LoanAmt Decimal(18,6) NULL,U_GLAccount nvarchar(50) NULL,U_Z_AppStatus nvarchar(40) NULL, U_DisDate DateTime NULL,U_NoEMI nvarchar(50) NULL,U_InstDate DateTime NULL,"
            objDA.strQuery += " U_CurApprover nvarchar(100) NULL,U_NxtApprover nvarchar(100) NULL,U_Z_AppRequired nvarchar(40) NULL,U_Z_AppReqDate datetime NULL,"
            objDA.strQuery += " U_Z_ReqTime nvarchar(40) NULL,U_Z_ApproveId nvarchar(40) NULL)"
            objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
            objDA.con.Open()
            objDA.cmd.ExecuteNonQuery()
            objDA.con.Close()
        End If
        exists = 0
        objDA.strQuery = "SELECT COUNT(*) FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[U_HRSESSION]') AND type in (N'U')"
        objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
        objDA.con.Open()
        objDA.sqlreader = objDA.cmd.ExecuteReader()
        If objDA.sqlreader.HasRows Then
            Do
                While objDA.sqlreader.Read
                    exists = objDA.sqlreader(0)
                End While
            Loop While objDA.sqlreader.NextResult()
        End If
        objDA.con.Close()
        If exists = 0 Then
            objDA.strQuery = "CREATE TABLE U_HRSESSION(U_SESSIONID int IDENTITY(1,1) NOT NULL,empID nvarchar(max) NOT NULL,empName nvarchar(Max) NOT NULL,"
            objDA.strQuery += "U_LOGIN_DATE DATETIME, U_LOGOUT_DATE DATETIME)"
            objDA.cmd = New SqlCommand(objDA.strQuery, objDA.con)
            objDA.con.Open()
            objDA.cmd.ExecuteNonQuery()
            objDA.con.Close()
        End If
        Return ""
    End Function

    Function ReturnDataset(ByVal strQuery As String) As Object
        Throw New NotImplementedException
    End Function


End Class
