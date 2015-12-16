Imports System
Imports DataAccess
Imports BusinessLogic
Imports EN
Public Class AContactUs
    Inherits System.Web.UI.Page
    Dim objEN As AdminContactUsEN = New AdminContactUsEN()
    Dim objDA As AdminContactUsDA = New AdminContactUsDA()
    Dim objBL As AdminContactUsBL = New AdminContactUsBL()
    Dim dbcon As DBConnectionDA = New DBConnectionDA()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            panelnew.Visible = False
            panelview.Visible = True
            PageloadBind()
        End If
    End Sub
    Private Sub PageloadBind()
        dbcon.ds = objBL.PageLoadBind()
        If dbcon.ds.Tables(0).Rows.Count > 0 Then
            grdContactus.DataSource = dbcon.ds.Tables(0)
            grdContactus.DataBind()
        Else
            grdContactus.DataBind()
        End If
        If dbcon.ds.Tables(1).Rows.Count > 0 Then
            txtcompname.Text = dbcon.ds.Tables(1).Rows(0)("compnyname").ToString()
        Else
            txtcompname.Text = ""
        End If
    End Sub

    Protected Sub btnnew_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnnew.Click
        panelnew.Visible = True
        panelview.Visible = False
        clear()
        PageloadBind()
    End Sub
    Private Sub clear()
        txtdocno.Text = ""
        txtdesi.Text = ""
        txtemail.Text = ""
        txttelext.Text = ""
        txtempname.Text = ""
    End Sub
    Private Sub mess(ByVal str As String)
        ScriptManager.RegisterStartupScript(Update, Update.[GetType](), "strmsg", dbcon.strmsg, True)
    End Sub
    Protected Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click

        If txtempname.Text = "" Then
            dbcon.strmsg = "alert('Enter the EmployeeName..')"
            mess(dbcon.strmsg)
        ElseIf txtdesi.Text = "" Then
            dbcon.strmsg = "alert('Enter the Designation..')"
            mess(dbcon.strmsg)
        ElseIf txtemail.Text = "" Then
            dbcon.strmsg = "alert('Enter the Emailid..')"
            mess(dbcon.strmsg)
        Else
            objEN.DocEntry = txtdocno.Text.Trim()
            objEN.CompanyName = txtcompname.Text.Trim()
            objEN.Email = txtemail.Text.Trim()
            objEN.EmpName = txtempname.Text.Trim()
            objEN.Phone = txttelext.Text.Trim()
            objEN.Position = txtdesi.Text.Trim()
            If chkActive.Checked = True Then
                objEN.Status = "Y"
            Else
                objEN.Status = "N"
            End If
            If txtdocno.Text = "" Then
                If objBL.InsertContactUs(objEN) = True Then
                    dbcon.strmsg = "alert('Conatctus Saved Successfully..')"
                    mess(dbcon.strmsg)
                End If
                clear()
            Else
                If objBL.UpdateContactUs(objEN) = True Then
                    dbcon.strmsg = "alert('Conatctus Updated Successfully..')"
                    mess(dbcon.strmsg)
                    clear()
                End If
            End If
            panelnew.Visible = False
            panelview.Visible = True
            PageloadBind()
        End If
    End Sub

    Protected Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        panelnew.Visible = False
        panelview.Visible = True
        clear()
    End Sub
    Protected Sub lbtndocnum_Click(ByVal sender As Object, ByVal e As EventArgs)

        Dim link As LinkButton = CType(sender, LinkButton)
        Dim gv As GridViewRow = CType((link.Parent.Parent), GridViewRow)
        Dim DocNo As LinkButton = CType(gv.FindControl("lbtndocnum"), LinkButton)
        objEN.DocEntry = DocNo.Text.Trim()
        PopulateContactus(objEN)
        panelview.Visible = False
        panelnew.Visible = True
    End Sub
    Private Sub PopulateContactus(ByVal objen As AdminContactUsEN)
        dbcon.ds1 = objBL.PopulatUseContact(objen)
        If dbcon.ds1.Tables(0).Rows.Count > 0 Then
            txtdocno.Text = dbcon.ds1.Tables(0).Rows(0)("U_DocEntry").ToString()
            txtcompname.Text = dbcon.ds1.Tables(0).Rows(0)("U_ComapnyId").ToString()
            txtempname.Text = dbcon.ds1.Tables(0).Rows(0)("U_Empname").ToString()
            txtemail.Text = dbcon.ds1.Tables(0).Rows(0)("U_Email").ToString()
            txtdesi.Text = dbcon.ds1.Tables(0).Rows(0)("U_Position").ToString()
            txttelext.Text = dbcon.ds1.Tables(0).Rows(0)("U_phone").ToString()
            If dbcon.ds1.Tables(0).Rows(0)("U_Status").ToString() = "Y" Then
                chkActive.Checked = True
            Else
                chkActive.Checked = False
            End If
        End If
    End Sub
End Class