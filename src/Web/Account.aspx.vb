Public Class Account
    Inherits System.Web.UI.Page
    Dim ctlA As New AccountController
    Dim dt As New DataTable
    Dim acc As New UserController
    Public dtG As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Request.Cookies("KDP")) Then
            Response.Redirect("Default.aspx")
        End If
        If Not IsPostBack Then
            ClearData()
            grdData.PageIndex = 0
            LoadAccountToGrid()
        End If

        cmdDelete.Attributes.Add("onClick", "javascript:return confirm(""ต้องการลบรายการนี้ใช่หรือไม่?"");")
    End Sub
    Private Sub LoadAccountToGrid()
        dt = ctlA.Account_Get()
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
                    If ctlA.Account_Delete(e.CommandArgument) Then
                        acc.User_GenLogfile(Request.Cookies("username").Value, ACTTYPE_DEL, "Account", "ลบชื่อประเภทอ้อย :" & txtName.Text, "")

                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','ลบข้อมูลเรียบร้อย');", True)
                        grdData.PageIndex = 0
                        LoadAccountToGrid()
                    Else
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'Success','ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)

                    End If
            End Select
        End If
    End Sub

    Private Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then
            Dim scriptString As String = "javascript:return confirm(""ต้องการลบรายการนี้ ?"");"
            Dim imgD As LinkButton = e.Row.Cells(3).FindControl("imgDel")
            imgD.Attributes.Add("onClick", scriptString)

        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#d9edf7';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")
        End If
    End Sub
    Private Sub EditData(ByVal pID As String)
        dt = ctlA.Account_GetByID(pID)
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                isAdd = False
                Me.txtUID.Text = String.Concat(dt.Rows(0)("UID"))
                txtName.Text = String.Concat(dt.Rows(0)("AccountName"))
                txtRemark.Text = String.Concat(dt.Rows(0)("Remark"))
            End With
        End If
        dt = Nothing
    End Sub

    Private Sub ClearData()
        Me.txtUID.Text = ""
        txtUID.Text = ""
        txtName.Text = ""
        txtRemark.Text = ""
    End Sub


    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        If txtUID.Text = "" Or txtName.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณากรอกข้อมูลให้ครบถ้วน');", True)
            Exit Sub
        End If
        If StrNull2Zero(txtUID.Text) = 0 Then
            If ctlA.Account_CheckDuplicate(txtName.Text) Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','ชื่อประเภทอ้อยนี้มีอยู่่ในระบบแล้ว');", True)
                Exit Sub
            End If
        End If

        ctlA.Account_Save(StrNull2Zero(txtUID.Text), txtName.Text, txtRemark.Text)
        grdData.PageIndex = 0
        LoadAccountToGrid()
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
        ctlA.Account_Delete(StrNull2Zero(txtUID.Text))
        grdData.PageIndex = 0
        LoadAccountToGrid()
    End Sub

    Private Sub grdData_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdData.PageIndexChanging
        grdData.PageIndex = e.NewPageIndex
        LoadAccountToGrid()
    End Sub
End Class

