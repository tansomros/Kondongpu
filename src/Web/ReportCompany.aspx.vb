
Public Class ReportCompany
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Public dtRptC As New DataTable
    Dim ctlA As New AgreementController
    Dim ctlR As New ReportController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Request.Cookies("KDP")) Then
            Response.Redirect("Default.aspx")
        End If
        If Not IsPostBack Then
            txtStartDate.Text = "01/01/" & Today.Year + 543
            txtEndDate.Text = Today.Date.ToString("dd/MM/") & (Today.Year + 543)
            Select Case Request("rpt")
                Case "C1"
                    lblReportTitle.Text = "ยอดรับอ้อยแยกตามโรงงาน"
                    lblComp.Text = "โรงงาน"
                    LoadCompany()
                Case "C2"
                    lblReportTitle.Text = "ยอดรับอ้อยแยกตามลูกค้า"
                    lblComp.Text = "ลูกค้า"
                    LoadCustomer()
                Case "C3"
                    lblReportTitle.Text = "น้ำหนักตามประเภทอ้อย"
                Case "C4"
                    lblReportTitle.Text = "น้ำหนักตามทะเบียนรถ"
                Case "C5"
                    lblReportTitle.Text = "สรุปรายการหัก"
                Case "C6"
                    lblReportTitle.Text = "สรุปการจ่ายประจำวัน"
            End Select

        End If

    End Sub

    Private Sub LoadCompany()
        Dim ctlC As New CompanyController
        ddlCompany.DataSource = ctlC.Company_GetForSelection
        ddlCompany.DataTextField = "CompanyName"
        ddlCompany.DataValueField = "UID"
        ddlCompany.DataBind()
    End Sub
    Private Sub LoadCustomer()
        Dim ctlC As New CustomerController
        ddlCompany.DataSource = ctlC.Customer_GetForSelection
        ddlCompany.DataTextField = "CustomerName"
        ddlCompany.DataValueField = "UID"
        ddlCompany.DataBind()
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
            Case "C1"
                'lblReportTitle.Text = "ยอดรับอ้อยแยกตามโรงงาน"
                dtRptC = ctlR.RPT_BillByCompany(Bdate, Edate, StrNull2Zero(ddlCompany.SelectedValue))
            Case "C2"
                'lblReportTitle.Text = "ยอดรับอ้อยแยกตามลูกค้า"
                dtRptC = ctlR.RPT_BillByCustomer(Bdate, Edate, StrNull2Zero(ddlCompany.SelectedValue))
            Case "C3"
                lblReportTitle.Text = "น้ำหนักตามประเภทอ้อย"
            Case "C4"
                lblReportTitle.Text = "น้ำหนักตามทะเบียนรถ"
            Case "C5"
                lblReportTitle.Text = "สรุปรายการหัก"
            Case "C6"
                lblReportTitle.Text = "สรุปการจ่ายประจำวัน"
        End Select


    End Sub
    Protected Sub cmdView_Click(sender As Object, e As EventArgs) Handles cmdView.Click
        LoadData()
    End Sub

    Private Sub cmdExport_Click(sender As Object, e As EventArgs) Handles cmdExport.Click
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Report", "window.open('DocumentViewer.aspx?r=c1&b=" & txtStartDate.Text & "&e=" & txtEndDate.Text & "&c=" & ddlCompany.SelectedValue & "&RPTTYPE=PDF','_blank');", True)
    End Sub

    Private Sub ddlCompany_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCompany.SelectedIndexChanged
        LoadData()
    End Sub
End Class

