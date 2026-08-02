Imports System.Data

Public Class AgreementList
    Inherits System.Web.UI.Page
    Dim ctlA As New AgreementController
    Public dtAgr As New DataTable
    Dim ctlM As New MasterController
    Dim dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Request.Cookies("KDP")) Then
            Response.Redirect("Default.aspx")
        End If
        If Not IsPostBack Then
            txtStartDate.Text = DateAdd(DateInterval.Year, -1, Today.Date).ToString("dd/MM/yyyy")
            txtEndDate.Text = Today.Date.ToString("dd/MM/yyyy")

            LoadAgreementType()
            LoadStatus()
            LoadAgreementList()

            'Select Case Request("s")
            '    Case "asm"
            'Case "apv"
            '    lblTitle.Text = ""
            '    If Request.Cookies("ROLE_ID").Value = 2 Then
            '        dtAgr = ctlB.Agreement_GetForApproval(Request.Cookies("LoginLocationUID").Value, Request.Cookies("UserID").Value, Request.Cookies("PeriodID").Value)
            '    End If
            '    Case Else
            '        Response.Redirect("Home.aspx")
            'End Select
        End If
    End Sub
    Private Sub LoadAgreementType()
        ddlType.DataSource = ctlA.AgreementType_GetForReport
        ddlType.DataTextField = "Descriptions"
        ddlType.DataValueField = "Code"
        ddlType.DataBind()
    End Sub
    Private Sub LoadStatus()
        dt = ctlA.AgreementStatus_GetForReport()
        If dt.Rows.Count > 0 Then
            With ddlStatus
                .DataSource = dt
                .DataTextField = "Descriptions"
                .DataValueField = "Code"
                .DataBind()
            End With
        End If
    End Sub
    Private Sub LoadAgreementList()
        If txtStartDate.Text = "" Then
            txtStartDate.Text = DateAdd(DateInterval.Year, -1, Today.Date).ToString("dd/MM/yyyy")
        End If
        If txtEndDate.Text = "" Then
            txtEndDate.Text = Today.Date.ToString("dd/MM/yyyy")
        End If

        Dim Bdate, Edate As String
        Bdate = ConvertStrDate2DBDate(txtStartDate.Text)
        Edate = ConvertStrDate2DBDate(txtEndDate.Text)

        dtAgr = ctlA.Agreement_GetSearch(Bdate, Edate, ddlType.SelectedValue, ddlStatus.SelectedValue)

    End Sub


    Protected Sub cmdView_Click(sender As Object, e As EventArgs) Handles cmdView.Click
        LoadAgreementList()
    End Sub

    Protected Sub ddlType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlType.SelectedIndexChanged
        LoadAgreementList()
    End Sub

    Protected Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        LoadAgreementList()
    End Sub
End Class