Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Diagnostics.Contracts
Imports System.IO
Imports System.Windows.Forms
'Imports System.IO

Public Class ReportCRViewer
    Inherits System.Web.UI.Page
    'Public Shared ReportFormula As String
    Dim sReportName As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Try
                SelecttionRPT()
                'If chkFileExist(Server.MapPath(ReportPath & Request("rpt"))) Then
                '   
                'Else
                '    'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ไม่มีพบไฟล์รายงาน อยู่ในระบบกรุณาตรวจสอบ');", True)
                '    Exit Sub
                'End If 
            Catch ex As Exception
                'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'" & ex.Message & "');", True)

            End Try
        End If

    End Sub
    Public Function setRptLogin(ByRef rpt As CrystalDecisions.CrystalReports.Engine.ReportDocument, Optional ByRef errorMsg As String = "", Optional ByVal dtset As DataSet = Nothing) As Boolean
        Dim result As Boolean = True
        If Not rpt Is Nothing Then
            Dim logoninfo As New CrystalDecisions.Shared.TableLogOnInfo
            Dim objDB As New BaseClass

            Dim sv As String = BaseClass.sqlServer
            Dim db As String = BaseClass.sqlDatabase
            Dim us As String = BaseClass.sqlUsername
            Dim pw As String = BaseClass.sqlPassword

            Try
                For Each tbl As CrystalDecisions.CrystalReports.Engine.Table In rpt.Database.Tables
                    logoninfo = tbl.LogOnInfo
                    With logoninfo.ConnectionInfo
                        .ServerName = sv
                        .DatabaseName = db
                        .UserID = us
                        .Password = pw
                    End With
                    tbl.ApplyLogOnInfo(logoninfo)
                Next

                If Not rpt.Subreports Is Nothing Then
                    If rpt.Subreports.Count > 0 Then
                        For Each subRpt As CrystalDecisions.CrystalReports.Engine.ReportDocument In rpt.Subreports
                            If Not subRpt Is Nothing Then
                                If subRpt.IsSubreport Then
                                    For Each tbl As CrystalDecisions.CrystalReports.Engine.Table In subRpt.Database.Tables
                                        logoninfo = tbl.LogOnInfo
                                        With logoninfo.ConnectionInfo
                                            .ServerName = sv
                                            .DatabaseName = db
                                            .UserID = us
                                            .Password = pw
                                        End With
                                        tbl.ApplyLogOnInfo(logoninfo)
                                    Next
                                End If

                            End If
                        Next
                    End If
                End If

            Catch ex As Exception
                errorMsg = ex.Message
                MsgBox(ex.Message)
                result = False
            End Try
        End If
        Return result
    End Function
    Public Sub SelecttionRPT()
        Dim Rpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        'Dim xParam As New List(Of Parameter)
        Select Case Request("rpt")
            Case "contract1"
                sReportName = "Contract" & Request("code")
                Rpt.Load(Server.MapPath(ReportPath & "Contract1.rpt"))
                setRptLogin(Rpt)
                CrystalReportViewer1.ReportSource = Rpt
                Rpt.SetParameterValue(0, Request("id"))
                'CrystalReportViewer1.SelectionFormula = ReportFormula
                'CrystalReportViewer1.RefreshReport()
            Case "contract2"
                sReportName = "Contract" & Request("code")
                Rpt.Load(Server.MapPath(ReportPath & "Contract2.rpt"))
                setRptLogin(Rpt)
                CrystalReportViewer1.ReportSource = Rpt
                Rpt.SetParameterValue(0, Request("id"))
            Case "guaranter"
                sReportName = "Guarantee1" & Request("code")
                Rpt.Load(Server.MapPath(ReportPath & "Guarantee1.rpt"))
                setRptLogin(Rpt)
                CrystalReportViewer1.ReportSource = Rpt
                Rpt.SetParameterValue(0, Request("id"))
            Case "debt"
                sReportName = "DebtAgreement" & Request("code")
                Rpt.Load(Server.MapPath(ReportPath & "DebtAgreement.rpt"))
                setRptLogin(Rpt)
                CrystalReportViewer1.ReportSource = Rpt
                Rpt.SetParameterValue(0, Request("id"))
            Case "pay"
                sReportName = "Pay" & Request("code")
                Rpt.Load(Server.MapPath(ReportPath & "Pay.rpt"))
                setRptLogin(Rpt)
                CrystalReportViewer1.ReportSource = Rpt
                'Rpt.SetParameterValue(0, Request("id"))
                CrystalReportViewer1.SelectionFormula = "{View_CardDetail.Code}='" & Request("code") & "'"
            Case "rev"
                sReportName = "Rev" & Request("code")
                Rpt.Load(Server.MapPath(ReportPath & "Receipt.rpt"))
                setRptLogin(Rpt)
                CrystalReportViewer1.ReportSource = Rpt
                'Rpt.SetParameterValue(0, Request("id"))
                CrystalReportViewer1.SelectionFormula = "{View_PayIn.Code}='" & Request("code") & "'"
            Case "WCANETYPE"
                Rpt.SetParameterValue("StartDate", ReportParameter(0))
                Rpt.SetParameterValue("EndDate", ReportParameter(1))

                CrystalReportViewer1.ReportSource = Rpt
                CrystalReportViewer1.SelectionFormula = SelectionFomula

            Case Else
                'setRptLogin(Rpt)
                Rpt.RecordSelectionFormula = SelectionFomula
                CrystalReportViewer1.ReportSource = Rpt

                CrystalReportViewer1.RefreshReport()
        End Select
        'ReportFormula = "{View_KCB_CardDetail.ACCNO}= '" & txtCustomerCode.Text & "' AND {View_KCB_CardDetail.CardType}= '2' AND  {View_KCB_CardDetail.Loan_Amount}=" & chkNullByDouble(txtLoanAmount.Text) & " AND {View_KCB_CardDetail.INVNO}= '" & invno & "' AND {View_KCB_CardDetail.PAYDATE}= '" & dpermit & "'"

        CrystalReportViewer1.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None

        'If chkFileExist(Server.MapPath(CustomerReport & sReportName & ".pdf")) Then
        '    Dim F As New FileInfo(Server.MapPath(CustomerReport & sReportName & ".pdf"))
        '    F.Delete()
        'End If
        ''-------------------------------
        ''Dim formatType As ExportFormatType = ExportFormatType.NoFormat
        ''formatType = ExportFormatType.PortableDocFormat
        ''Rpt.ExportToHttpResponse(formatType, Response, True, sReportName)
        ''Response.End()
        ''----------------------------------
        ''Rpt.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath(CustomerReport & sReportName & ".pdf"))
        ''-----------------------------------

        'Dim dest As DiskFileDestinationOptions = New DiskFileDestinationOptions()
        'dest.DiskFileName = Server.MapPath(CustomerReport & sReportName & ".pdf")
        'Dim formatOpt As PdfFormatOptions = New PdfFormatOptions()
        'formatOpt.FirstPageNumber = 0
        'formatOpt.LastPageNumber = 0
        'formatOpt.UsePageRange = False
        'formatOpt.CreateBookmarksFromGroupTree = False
        'Dim ex As ExportOptions = New ExportOptions()
        'ex.ExportDestinationType = ExportDestinationType.DiskFile
        'ex.ExportDestinationOptions = dest
        'ex.ExportFormatType = ExportFormatType.PortableDocFormat
        'ex.ExportFormatOptions = formatOpt
        'Rpt.Export(ex)
        ''------------------------------------

        'Response.Redirect(CustomerReport & sReportName & ".pdf")

    End Sub

End Class