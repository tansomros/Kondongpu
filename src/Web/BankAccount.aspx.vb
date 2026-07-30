Public Class BankAccount
    Inherits System.Web.UI.Page

    Dim ctlL As New MasterController
    Dim dt As New DataTable
    Dim acc As New UserController
    Public dtG As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Request.Cookies("KDP")) Then
            Response.Redirect("Default.aspx")
        End If
        If Not IsPostBack Then
            ClearData()
            LoadBankToDDL()
            LoadBankAccountToGrid()
        End If

        cmdDelete.Attributes.Add("onClick", "javascript:return confirm(""ต้องการลบรายการนี้ใช่หรือไม่?"");")
    End Sub
    Private Sub LoadBankToDDL()
        dt = ctlL.Bank_Get
        If dt.Rows.Count > 0 Then
            With ddlBank
                .Enabled = True
                .DataSource = dt
                .DataTextField = "BankName"
                .DataValueField = "BankID"
                .DataBind()
                .SelectedIndex = 0
            End With
        End If
        dt = Nothing
    End Sub
    Private Sub LoadBankAccountToGrid()
        dt = ctlL.BankAccount_Get
        With grdData
            .Visible = True
            .DataSource = dt
            .DataBind()
        End With
    End Sub

    Private Sub grdData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdData.RowCommand
        If TypeOf e.CommandSource Is WebControls.LinkButton Then
            Dim ButtonPressed As WebControls.LinkButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgEdit"
                    EditData(e.CommandArgument())
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalMasterData(this,'" + e.CommandArgument() + "');", True)
                Case "imgDel"
                    If ctlL.BankAccount_Delete(e.CommandArgument) Then
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','ลบข้อมูลเรียบร้อย');", True)
                        LoadBankAccountToGrid()
                    Else
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'Success','ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)

                    End If
            End Select
        End If
    End Sub

    Private Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then
            Dim scriptString As String = "javascript:return confirm(""ต้องการลบรายการนี้ ?"");"
            Dim imgD As LinkButton = e.Row.Cells(4).FindControl("imgDel")
            imgD.Attributes.Add("onClick", scriptString)

        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#d9edf7';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")
        End If
    End Sub
    Private Sub EditData(ByVal pID As String)
        dt = ctlL.BankAccount_GetByAccountNo(pID)
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                isAdd = False
                Me.txtAccountNo.Text = String.Concat(dt.Rows(0)("AccountNo"))
                txtAccountName.Text = String.Concat(dt.Rows(0)("AccountName"))
                txtBranch.Text = String.Concat(dt.Rows(0)("Branch"))
                ddlBank.SelectedValue = String.Concat(dt.Rows(0)("BankID"))
            End With
        End If
        dt = Nothing
    End Sub

    Private Sub ClearData()
        txtAccountNo.Text = ""
        txtAccountName.Text = ""
        txtBranch.Text = ""
        ddlBank.SelectedIndex = 0
    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        If txtAccountName.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณากรอกข้อมูลให้ครบถ้วน');", True)
            Exit Sub
        End If

        'If ctlA.BankAccount_CheckDuplicate(txtAccountNo.Text) Then
        '    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','บัญชีเงินฝากนี้มีอยู่่ในระบบแล้ว');", True)
        '    Exit Sub
        'End If

        ctlL.BankAccount_Save(StrNull2Zero(txtAccountNo.Text), txtAccountName.Text, ddlBank.SelectedValue, txtBranch.Text)

        LoadBankAccountToGrid()
        ClearData()
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกข้อมูลเรียบร้อย');", True)
    End Sub

    Protected Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        ClearData()
    End Sub

    Protected Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearData()
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalMasterData(this,'');", True)
    End Sub

    Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        ctlL.BankAccount_Delete(txtAccountNo.Text)
        LoadBankAccountToGrid()
    End Sub
End Class

