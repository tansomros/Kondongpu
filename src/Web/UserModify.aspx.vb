
Public Class UserModify
    Inherits System.Web.UI.Page
    Dim ctlU As New UserController
    Dim ctlR As New UserRoleController
    Dim enc As New CryptographyEngine

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Request.Cookies("KDP")) Then
            Response.Redirect("Default.aspx")
        End If

        If Not IsPostBack Then
            chkStatus.Checked = True
            lblUserID.Text = ""
            cmdDelete.Enabled = False

            If Not Request("uid") = Nothing Then
                EditUser()
            End If
        End If

        Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
        cmdDelete.Attributes.Add("onClick", scriptString)

        txtEmail.Attributes.Add("OnKeyPress", "return NotAllowThai();")
    End Sub
    Private Sub EditUser()
        Dim dt As New DataTable
        dt = ctlU.User_GetByUserID(Request("uid"))

        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                lblUserID.Text = String.Concat(.Item("UserID"))
                txtDisplayname.Text = String.Concat(.Item("DisplayName"))
                txtTel.Text = String.Concat(.Item("Telephone"))
                txtEmail.Text = String.Concat(.Item("Email"))
                chkGroup.SelectedValue = String.Concat(.Item("RoleID"))

                Session("password") = ""
                txtUsername.Text = String.Concat(.Item("Username"))
                txtPassword.Text = enc.DecryptString(String.Concat(dt.Rows(0)("Passwords")), True)
                'txtPassword.Text = "**********" ' enc.DecryptString(String.Concat(dt.Rows(0)("Passwords")), True)
                Session("password") = enc.DecryptString(String.Concat(dt.Rows(0)("Passwords")), True)
                chkStatus.Checked = ConvertStatusFlag2CHK(String.Concat(.Item("StatusFlag")))

                chkGroup.SelectedValue = String.Concat(dt.Rows(0)("RoleID"))

                cmdDelete.Enabled = True
            End With

        End If
    End Sub

    Function Check_Email(str As String) As Boolean
        Return Regex.IsMatch(str, "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
    End Function
    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        If txtDisplayname.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณากรอก Display Name');", True)
            Exit Sub
        End If
        'If txtLastName.Text = "" Then
        '    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณากรอกนามสกุล');", True)
        '    Exit Sub
        'End If
        'If txtEmail.Text = "" Then
        '    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณากรอก E-mail');", True)
        '    Exit Sub
        'End If
        If txtEmail.Text <> "" Then
            If Check_Email(txtEmail.Text) = False Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'ผลการตรวจสอบ','รูปแบบ E-mail ไม่ถูกต้อง กรุณาตรวจสอบ');", True)
                Exit Sub
            End If
        End If


        If txtUsername.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณากรอก Username');", True)
            Exit Sub
        End If
        If txtPassword.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุ Password');", True)
            Exit Sub
        End If

        If chkGroup.SelectedValue = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุสิทธิ์ผู้ใช้งานก่อน');", True)
            Exit Sub
        End If

        If lblUserID.Text = "" Then
            If ctlU.User_CheckDuplicate(txtUsername.Text) Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','Username นี้มีในระบบแล้ว');", True)
                Exit Sub
            End If
        End If
        Dim Password As String = enc.EncryptString(txtPassword.Text, True)
        'If txtPassword.Text <> "**********" Then
        '    Password = enc.EncryptString(txtPassword.Text, True)
        'Else
        '    Password = enc.EncryptString(Session("password"), True)
        'End If

        ctlU.User_Save(StrNull2Zero(lblUserID.Text), txtUsername.Text, Password, txtDisplayname.Text, txtEmail.Text, txtTel.Text, StrNull2Zero(chkGroup.SelectedValue), ConvertBoolean2StatusFlag(chkStatus.Checked), Request.Cookies("userid").Value)

        If lblUserID.Text = "" Then
            lblUserID.Text = ctlU.User_GetUID(txtUsername.Text)
        End If

        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกข้อมูลเรียบร้อย');", True)

    End Sub
    Private Sub ClearData()
        lblUserID.Text = ""
        txtDisplayname.Text = ""
        txtUsername.Text = ""
        txtPassword.Text = ""
        txtTel.Text = ""
        txtEmail.Text = ""
        cmdDelete.Enabled = False
    End Sub
    Protected Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        ctlU.User_Delete(lblUserID.Text)
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'ผลการทำงาน','ลบข้อมูลเรียบร้อย');", True)

        Response.Redirect("Users?m=u&s=u")
    End Sub

    Protected Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        ClearData()
    End Sub
End Class