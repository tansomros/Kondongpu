
Public Class ReportAgreement
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Public dtRptA As New DataTable
    Dim ctlA As New AgreementController
    Dim ctlR As New ReportController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Request.Cookies("KDP")) Then
            Response.Redirect("Default.aspx")
        End If
        If Not IsPostBack Then
            pnData.Visible = False
            LoadAgreementType()
            LoadStatus()
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

    Private Sub LoadData()
        dtRptA = ctlR.Agreement_Report_GetSearch(ddlType.SelectedValue, ddlStatus.SelectedValue, txtSearch.Text)
        pnData.Visible = True
    End Sub
    Protected Sub cmdView_Click(sender As Object, e As EventArgs) Handles cmdView.Click
        LoadData()
    End Sub

    Private Sub cmdExport_Click(sender As Object, e As EventArgs) Handles cmdExport.Click
        ctlR.GEN_Agreement_GetSearch(ddlType.SelectedValue, ddlStatus.SelectedValue, txtSearch.Text, Request.Cookies("UserID").Value)

        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Report", "window.open('ReportViewer.aspx?rpt=A1&t=" & ddlType.SelectedValue & "&st=" & ddlStatus.SelectedValue & "&s=" & txtSearch.Text & "&RPTTYPE=EXCEL','_blank');", True)
    End Sub
End Class

