Imports System.Data

Public Class Price
    Inherits System.Web.UI.Page
    Dim ctlPrc As New PriceController
    Public dtPrice As New DataTable
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
            LoadYear()
            LoadCompany()
            LoadCaneType()
            LoadPriceList()
        End If
    End Sub
    Private Sub LoadYear()
        ddlYear.DataSource = ctlPrc.Price_GetYear
        ddlYear.DataTextField = "DisplayYear"
        ddlYear.DataValueField = "PYear"
        ddlYear.DataBind()
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

    Private Sub LoadPriceList()
        If txtStartDate.Text = "" Then
            txtStartDate.Text = DateAdd(DateInterval.Year, -1, Today.Date).ToString("dd/MM/yyyy")
        End If
        If txtEndDate.Text = "" Then
            txtEndDate.Text = Today.Date.ToString("dd/MM/yyyy")
        End If

        Dim Bdate, Edate As String
        Bdate = ConvertStrDate2DBDate(txtStartDate.Text)
        Edate = ConvertStrDate2DBDate(txtEndDate.Text)

        dtPrice = ctlPrc.Price_GetBySearch(ddlYear.SelectedValue, Bdate, Edate, ddlType.SelectedValue, ddlCompany.SelectedValue, ddlStatus.SelectedValue)

    End Sub


    Protected Sub cmdView_Click(sender As Object, e As EventArgs) Handles cmdView.Click
        LoadPriceList()
    End Sub

    Protected Sub ddlType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlType.SelectedIndexChanged
        LoadPriceList()
    End Sub

    Protected Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        LoadPriceList()
    End Sub

    Private Sub ddlCompany_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCompany.SelectedIndexChanged
        LoadPriceList()
    End Sub

    Private Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        LoadPriceList()
    End Sub
End Class