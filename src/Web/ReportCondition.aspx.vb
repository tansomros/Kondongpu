
Public Class ReportCondition
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Public dtRptB As New DataTable
    Dim ctlA As New AgreementController
    Dim ctlR As New ReportController
    Dim ctlM As New MasterController
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Request.Cookies("KDP")) Then
            Response.Redirect("Default.aspx")
        End If
        If Not IsPostBack Then
            txtStartDate.Text = "01/01/" & Today.Year + 543
            txtEndDate.Text = Today.Date.ToString("dd/MM/") & (Today.Year + 543)
            Select Case Request("rpt")
                Case "C5"
                    lblReportTitle.Text = "สรุปการจ่ายประจำวัน"
                Case "C6"
                    lblReportTitle.Text = "สรุปการหัก"
            End Select

        End If

    End Sub

    Private Sub LoadData()
        If txtStartDate.Text = "" Then
            txtStartDate.Text = DateAdd(DateInterval.Year, -1, Today.Date).ToString("dd/MM/yyyy")
        End If
        If txtEndDate.Text = "" Then
            txtEndDate.Text = Today.Date.ToString("dd/MM/yyyy")
        End If

        Dim Bdate, Edate As String
        Bdate = ConvertStrDate2DBDate(txtStartDate.Text)
        Edate = ConvertStrDate2DBDate(txtEndDate.Text)

        Select Case Request("rpt")
            Case "C5"
                lblReportTitle.Text = "สรุปการจ่ายประจำวัน"
                dtRptB = ctlR.RPT_BillSummary(Bdate, Edate)
            Case "C6"
                lblReportTitle.Text = "สรุปรายการหัก"
                dtRptB = ctlR.RPT_BillDeduct(Bdate, Edate)
        End Select


    End Sub
    Protected Sub cmdView_Click(sender As Object, e As EventArgs) Handles cmdView.Click
        LoadData()
    End Sub

    Private Sub cmdExport_Click(sender As Object, e As EventArgs) Handles cmdExport.Click
        Dim Bdate, Edate As String
        Bdate = ConvertStrDate2DBDate(txtStartDate.Text)
        Edate = ConvertStrDate2DBDate(txtEndDate.Text)
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Report", "window.open('DocumentViewer.aspx?r=" & Request("rpt").ToLower() & "&b=" & Bdate & "&e=" & Edate & "&RPTTYPE=PDF','_blank');", True)
    End Sub
End Class

