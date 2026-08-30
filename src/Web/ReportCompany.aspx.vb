
Public Class ReportCompany
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Public dtRptC1 As New DataTable
    Dim ctlA As New AgreementController
    Dim ctlR As New ReportController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Request.Cookies("KDP")) Then
            Response.Redirect("Default.aspx")
        End If
        If Not IsPostBack Then
            pnData.Visible = False
            Select Case Request("rpt")
                Case "C1"
                    lblReportTitle.Text = "ยอดรับอ้อยแยกตามโรงงาน"
                Case "C2"
                    lblReportTitle.Text = "ยอดรับอ้อยแยกตามลูกค้า"
                Case "C3"
                    lblReportTitle.Text = "น้ำหนักตามประเภทอ้อย"
                Case "C4"
                    lblReportTitle.Text = "น้ำหนักตามทะเบียนรถ"
                Case "C5"
                    lblReportTitle.Text = "สรุปรายการหัก"
                Case "C6"
                    lblReportTitle.Text = "สรุปการจ่ายประจำวัน"
            End Select
            LoadCompany()
        End If

    End Sub

    Private Sub LoadCompany()
        Dim ctlC As New CompanyController
        ddlCompany.DataSource = ctlC.Company_GetForReport
        ddlCompany.DataTextField = "CompanyName"
        ddlCompany.DataValueField = "UID"
        ddlCompany.DataBind()
    End Sub
    Private Sub LoadData()
        'dtRptA = ctlR.Agreement_Report_GetSearch(ddlType.SelectedValue, ddlStatus.SelectedValue, txtSearch.Text)
        'pnData.Visible = True
    End Sub
    Protected Sub cmdView_Click(sender As Object, e As EventArgs) Handles cmdView.Click
        LoadData()
    End Sub

    Private Sub cmdExport_Click(sender As Object, e As EventArgs) Handles cmdExport.Click
        'ctlR.GEN_Agreement_GetSearch(ddlType.SelectedValue, ddlStatus.SelectedValue, txtSearch.Text, Request.Cookies("UserID").Value)

        'ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Report", "window.open('ReportViewer.aspx?rpt=A1&t=" & ddlType.SelectedValue & "&st=" & ddlStatus.SelectedValue & "&s=" & txtSearch.Text & "&RPTTYPE=EXCEL','_blank');", True)
    End Sub
End Class

