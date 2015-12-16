Imports System
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Data.SqlClient
Imports EN
Public Class PayslipDA
    Dim objen As PaySlipEN = New PaySlipEN()
    Dim objDA As DBConnectionDA = New DBConnectionDA()
    Public Sub New()
        objDA.con = New SqlConnection(objDA.GetConnection)
    End Sub
    Public Function GetMonYear(ByVal objen As PaySlipEN) As DataSet
        Try
            objDA.strQuery = "Select Top 6 U_Z_RefCode,U_Z_MONTH,DateName( month , DateAdd( month , U_Z_MONTH , 0 ) - 1 ) +' - '+ CONVERT(NVARCHAR, U_Z_YEAR) AS  U_Z_YEAR "
            objDA.strQuery += " from [@Z_PAYROLL1] where U_Z_empid='" & objen.EmpCode & "' and U_Z_OffCycle='N' and U_Z_Posted='Y' and ISNULL(U_Z_CompNo,'')<>''  Order by U_Z_RefCode desc "
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds)
            Return objDA.ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function PaySlipHeader(ByVal objen As PaySlipEN) As DataSet
        Try
            objDA.strQuery = "Select * from [@Z_PAYROLL1] where  U_Z_empid='" & objen.EmpCode & "' and  U_Z_RefCode='" & objen.RefCode & "';"
            objDA.strQuery += "select x.Type,x.Field,X.FieldName,x.Earning,x.Deduction  from ( select 'A' 'Type' ,U_Z_Field 'Field',U_Z_FieldName 'FieldName',U_Z_Amount 'Earning',0 'Deduction' from [@Z_PAYROLL2] where U_Z_AMount>=0 "
            objDA.strQuery += " and U_Z_RefCode='" & objen.RefCode & "'  ) as x order by x.Type;"
            objDA.strQuery += " select x.Type,x.Field,X.FieldName,x.Earning,x.Deduction  from ( "
            objDA.strQuery += " select 'A' 'Type' ,U_Z_Field 'Field',U_Z_FieldName 'FieldName',0 'Earning',U_Z_Amount 'Deduction' from [@Z_PAYROLL3] where U_Z_AMount>=0 and U_Z_RefCode='" & objen.RefCode & "' ) as x order by x.Type;"
            objDA.strQuery += " select U_Z_LeaveCode,U_Z_LeaveName,U_Z_CM,U_Z_NoofDays,U_Z_Balance,U_Z_Redim from [@Z_PAYROLL5] where U_Z_RefCode='" & objen.RefCode & "';"
            objDA.strQuery += " SELECT isnull(T1.[BankName],'N/A'), isnull(T0.[bankAcount],'N/A') FROM OHEM T0  left outer JOIN ODSC T1 ON T0.bankCode = T1.BankCode WHERE empID=" & objen.EmpCode & ";"
            objDA.sqlda = New SqlDataAdapter(objDA.strQuery, objDA.con)
            objDA.sqlda.Fill(objDA.ds1)
            Return objDA.ds1
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
