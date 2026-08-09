Imports System.Drawing

Public Class PriceModify
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlC As New CompanyController
    Dim ctlPrc As New PriceController
    Dim ctlU As New UserController
    Dim ctlM As New MasterController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Request.Cookies("KDP")) Then
            Response.Redirect("Default.aspx")
        End If

        If Not IsPostBack Then
            cmdSave.Visible = True
            cmdDelete.Visible = False
            hdUID.Value = Request("id")
            txtStartDate.Text = Today.Date.ToString("dd/MM/yyyy")
            txtEnddate.Text = "31/12/" & Today.Year() + 543

            LoadYear()
            LoadCaneType()
            LoadCompanyToDDL()
            LoadCompanyDetail(ddlCompany.SelectedValue)
            DisableControl()
            If Not IsNothing(Request("id")) Then
                LoadPriceData()
            End If
        End If

        cmdDelete.Attributes.Add("onClick", "javascript:return confirm(""ต้องการลบข้อมูลนี้ใช่หรือไม่?"");")
        txtAmount.Attributes.Add("OnKeyPress", "return AllowOnlyDouble();")
    End Sub
    Private Sub DisableControl()
        txtAddress.ReadOnly = True
        txtAddress.BackColor = Color.White
    End Sub
    Private Sub LoadYear()
        ddlYear.DataSource = ctlPrc.Price_GetYear
        ddlYear.DataTextField = "DisplayYear"
        ddlYear.DataValueField = "PYear"
        ddlYear.DataBind()
    End Sub
    Private Sub LoadCaneType()
        ddlCane.DataSource = ctlM.CaneType_Get
        ddlCane.DataTextField = "CaneName"
        ddlCane.DataValueField = "UID"
        ddlCane.DataBind()
    End Sub
    Private Sub LoadCompanyToDDL()
        dt = ctlC.Company_GetForSelection()
        If dt.Rows.Count > 0 Then
            With ddlCompany
                .DataSource = dt
                .DataTextField = "CompanyName"
                .DataValueField = "UID"
                .DataBind()
                .SelectedIndex = 0
            End With
        End If
    End Sub

    Private Sub LoadPriceData()
        dt = ctlPrc.Price_GetByUID(Request("id"))
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                hdUID.Value = String.Concat(.Item("UID"))
                ddlYear.SelectedValue = String.Concat(.Item("PYear"))
                txtStartDate.Text = ConvertStrDate2ShortTH(String.Concat(.Item("StartDate")))
                txtEnddate.Text = ConvertStrDate2ShortTH(String.Concat(.Item("EndDate")))
                ddlCane.SelectedValue = String.Concat(.Item("CaneTypeUID"))
                txtAmount.Text = DBNull2Dbl(.Item("UnitPrice")).ToString("#,##0.##")
                ddlCompany.SelectedValue = String.Concat(.Item("CompanyUID"))
                LoadCompanyDetail(DBNull2Zero(.Item("CompanyUID")))
            End With
        Else
        End If
    End Sub
    Protected Sub cmdBack_Click(sender As Object, e As EventArgs) Handles cmdBack.Click
        Response.Redirect("Price.aspx?m=pc")
    End Sub
    Protected Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalCancel(this,'ยกเลิกคำขอ','คุณต้องการยกเลิกคำขอใช่หรือไม่?');", True)
        ctlPrc.Price_Delete(StrNull2Zero(hdUID.Value))
        ClearData()
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','ลบ Request เรียบร้อย');", True)
    End Sub
    Private Sub ClearData()
        hdUID.Value = "0"
        txtStartDate.Text = ""
        txtEnddate.Text = ""
        txtAmount.Text = ""
        txtAddress.Text = ""
    End Sub
    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click

        If txtStartDate.Text = "" Or txtEnddate.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุวันที่');", True)
            Exit Sub
        End If
        If ddlCane.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุประเภทอ้อย');", True)
            Exit Sub
        End If
        If ddlCompany.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุโรงงาน);", True)
            Exit Sub
        End If

        If StrNull2Zero(txtAmount.Text) <= 0 Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุราคา');", True)
            Exit Sub
        End If

        Dim StartDate, EndDate As String
        StartDate = ConvertStrDate2DBDate(txtStartDate.Text)
        EndDate = ConvertStrDate2DBDate(txtEnddate.Text)

        Dim StatusFlag As String = "A"
        If chkStatus.Checked Then
            StatusFlag = "A"
        Else
            StatusFlag = "D"
        End If

        ctlPrc.Price_Save(StrNull2Zero(hdUID.Value), StrNull2Zero(ddlYear.SelectedValue), StrNull2Zero(ddlCompany.SelectedValue), StrNull2Zero(ddlCane.SelectedValue), StartDate, EndDate, StrNull2Double(txtAmount.Text), StatusFlag, Request.Cookies("UserID").Value)

        If StrNull2Zero(hdUID.Value) = 0 Then
            ctlU.User_GenLogfile(Request.Cookies("Username").Value, "ADD", "Price", "กำหนดราคาใหม่", "CompUID=" & ddlCompany.SelectedValue & ":" & ddlCompany.SelectedItem.Text & "|Cane=" & ddlCane.SelectedItem.Text & "|Price=" & txtAmount.Text)
        Else
            ctlU.User_GenLogfile(Request.Cookies("Username").Value, "UPD", "Price", "แก้ไขราคา", "CompUID=" & ddlCompany.SelectedValue & ":" & ddlCompany.SelectedItem.Text & "|Cane=" & ddlCane.SelectedItem.Text & "|Price=" & txtAmount.Text)
        End If
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกสัญญาเรียบร้อย');", True)
        ClearData()
    End Sub

    Private Sub ddlCompany_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCompany.SelectedIndexChanged
        LoadCompanyDetail(ddlCompany.SelectedValue)
    End Sub
    Private Sub LoadCompanyDetail(CompanyUID As Integer)
        Dim dtC As New DataTable
        dtC = ctlC.Company_GetByUID(CompanyUID)
        If dtC.Rows.Count > 0 Then
            txtAddress.Text = String.Concat(dtC.Rows(0)("CompanyAddress"))
        End If
    End Sub
End Class