Imports System.Data
Imports Org.BouncyCastle.Crypto

Public Class Subbook
    Inherits System.Web.UI.Page
    Dim ctlR As New SubbookController
    Public dtREQ As New DataTable
    Dim ctlM As New MasterController
    Dim dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Request.Cookies("KDP")) Then
            Response.Redirect("Default.aspx")
        End If
        If Not IsPostBack Then
            lblTitle.Text = "รายการ Subbook"
            txtStartDate.Text = Today.Date.ToString("dd/MM/yyyy") 'DateAdd(DateInterval.Year, -1, Today.Date).ToString("dd/MM/yyyy")
            txtEndDate.Text = Today.Date.ToString("dd/MM/yyyy")

            'LoadCaneType()
            'LoadStatus()
            LoadSubbookList()

            'Select Case Request("s")
            '    Case "asm"


            'Case "apv"
            '    lblTitle.Text = ""
            '    If Request.Cookies("ROLE_ID").Value = 2 Then
            '        dtPrice = ctlA.Request_GetForApproval(Request.Cookies("LoginLocationUID").Value, Request.Cookies("UserID").Value, Request.Cookies("PeriodID").Value)
            '    End If
            '    Case Else
            '        Response.Redirect("Home.aspx")
            'End Select

        End If
    End Sub
    'Private Sub LoadCaneType()
    '    ddlType.DataSource = ctlPrc.RequestType_GetForReport
    '    ddlType.DataTextField = "Name"
    '    ddlType.DataValueField = "UID"
    '    ddlType.DataBind()
    'End Sub
    'Private Sub LoadStatus()
    '    dt = ctlM.AssessmentStatus_GetForReport()
    '    If dt.Rows.Count > 0 Then
    '        With ddlStatus
    '            .DataSource = dt
    '            .DataTextField = "Descriptions"
    '            .DataValueField = "Code"
    '            .DataBind()
    '        End With
    '    End If
    'End Sub

    Private Sub LoadSubbookList()
        If txtStartDate.Text = "" Then
            txtStartDate.Text = Today.Date.ToString("dd/MM/yyyy") ' DateAdd(DateInterval.Year, -1, Today.Date).ToString("dd/MM/yyyy")
        End If
        If txtEndDate.Text = "" Then
            txtEndDate.Text = Today.Date.ToString("dd/MM/yyyy")
        End If

        Dim Bdate, Edate As String
        Bdate = ConvertStrDate2DBString(txtStartDate.Text)
        Edate = ConvertStrDate2DBString(txtEndDate.Text)

        dtREQ = ctlR.Subbook_GetByDate(Bdate, Edate)
        Dim totalR, totalP As Double
        totalP = 0
        totalR = 0
        For i = 0 To dtREQ.Rows.Count - 1
            totalP = totalP + DBNull2Dbl(dtREQ.Rows(i)("PayAmount"))
            totalR = totalR + DBNull2Dbl(dtREQ.Rows(i)("RevAmount"))
        Next
        lblTotalPay.Text = totalP.ToString("#,###.##")
        lblTotalRev.Text = totalR.ToString("#,###.##")
    End Sub

    Protected Sub cmdView_Click(sender As Object, e As EventArgs) Handles cmdView.Click
        LoadSubbookList()
    End Sub

    Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click

        Dim Bdate, Edate As String
        Bdate = ConvertStrDate2DBString(txtStartDate.Text)
        Edate = ConvertStrDate2DBString(txtEndDate.Text)
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Report", "window.open('ReportViewer.aspx?rpt=sub&b=" & Bdate & "&e=" & Edate & "&RPTTYPE=PDF','_blank');", True)
    End Sub
End Class