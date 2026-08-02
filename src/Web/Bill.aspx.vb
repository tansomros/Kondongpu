Imports System.Data

Public Class Bill
    Inherits System.Web.UI.Page
    Dim ctlB As New BillController
    Public dtBill As New DataTable
    Dim ctlM As New MasterController
    Dim dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Request.Cookies("KDP")) Then
            Response.Redirect("Default.aspx")
        End If
        If Not IsPostBack Then
            'txtStartDate.Text = DateAdd(DateInterval.Year, -1, Today.Date).ToString("dd/MM/yyyy")
            txtStartDate.Text = "01/01/" & Today.Year + 542
            txtEndDate.Text = Today.Date.ToString("dd/MM/yyyy")
            LoadCompany()
            LoadCaneType()
            LoadBillList()
        End If
    End Sub

    Private Sub LoadCaneType()
        ddlType.DataSource = ctlM.CaneType_GetForReport
        ddlType.DataTextField = "CaneName"
        ddlType.DataValueField = "UID"
        ddlType.DataBind()
    End Sub
    Private Sub LoadCompany()
        Dim ctlC As New CompanyController
        ddlCompany.DataSource = ctlC.Company_GetForReport
        ddlCompany.DataTextField = "CompanyName"
        ddlCompany.DataValueField = "UID"
        ddlCompany.DataBind()
    End Sub

    Private Sub LoadBillList()
        If txtStartDate.Text = "" Then
            txtStartDate.Text = DateAdd(DateInterval.Year, -1, Today.Date).ToString("dd/MM/yyyy")
        End If
        If txtEndDate.Text = "" Then
            txtEndDate.Text = Today.Date.ToString("dd/MM/yyyy")
        End If

        Dim Bdate, Edate As String
        Bdate = ConvertStrDate2DBDate(txtStartDate.Text)
        Edate = ConvertStrDate2DBDate(txtEndDate.Text)
        dtBill = ctlB.Bill_Get
        'dtBill = ctlB.Bill_Get(ddlYear.SelectedValue, Bdate, Edate, ddlType.SelectedValue, ddlCompany.SelectedValue, ddlStatus.SelectedValue)

    End Sub


    Protected Sub cmdView_Click(sender As Object, e As EventArgs) Handles cmdView.Click
        LoadBillList()
    End Sub

    Protected Sub ddlType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlType.SelectedIndexChanged
        LoadBillList()
    End Sub

    Protected Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        LoadBillList()
    End Sub

    Private Sub ddlCompany_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCompany.SelectedIndexChanged
        LoadBillList()
    End Sub
End Class