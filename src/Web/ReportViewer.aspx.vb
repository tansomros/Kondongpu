Imports System.Windows.Forms
Imports Microsoft.Reporting.WebForms
Public Class ReportViewer
    Inherits System.Web.UI.Page
    Dim ReportFileName As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Request.Cookies("KDP")) Then
            Response.Redirect("Default.aspx")
        End If

        LoadReport()
    End Sub
    Private Sub LoadReport()
        Dim credential As New ReportServerCredentials
        ReportViewer1.Reset()
        ReportViewer1.ServerReport.ReportServerCredentials = credential
        ReportViewer1.ServerReport.ReportServerUrl = credential.ReportServerUrl
        ReportViewer1.ShowPrintButton = True

        Dim xParam As New List(Of ReportParameter)

        'Session("m") = Request("m")

        Select Case Request("rpt")
            Case "contract1"
                FagRPT = "PDF"
                ReportViewer1.ServerReport.ReportPath = credential.ReportPath("Contract1")
                xParam.Add(New ReportParameter("AgreementUID", Request("id").ToString()))
            Case "contract2"
                FagRPT = "PDF"
                ReportViewer1.ServerReport.ReportPath = credential.ReportPath("Contract2")
                xParam.Add(New ReportParameter("AgreementUID", Request("id").ToString()))
            Case "contract3"
                FagRPT = "PDF"
                ReportViewer1.ServerReport.ReportPath = credential.ReportPath("Contract3")
                xParam.Add(New ReportParameter("AgreementUID", Request("id").ToString()))
            Case "guarantor"
                FagRPT = "PDF"
                ReportViewer1.ServerReport.ReportPath = credential.ReportPath("Guarantor")
                xParam.Add(New ReportParameter("AgreementUID", Request("id").ToString()))
            Case "debtagree"
                FagRPT = "PDF"
                ReportViewer1.ServerReport.ReportPath = credential.ReportPath("DebtAgree")
                xParam.Add(New ReportParameter("AgreementUID", Request("id").ToString()))
            Case "pay"
                FagRPT = "PDF"
                ReportViewer1.ServerReport.ReportPath = credential.ReportPath("PaySlip")
                xParam.Add(New ReportParameter("Code", Request("code").ToString()))
            Case "rev"
                FagRPT = "PDF"
                ReportViewer1.ServerReport.ReportPath = credential.ReportPath("Receipt")
                xParam.Add(New ReportParameter("Code", Request("code").ToString()))
            Case "sub"
                FagRPT = Request("RPTTYPE")
                ReportFileName = "Subbook" & ConvertDate2DBString(Now.Date)
                ReportViewer1.ServerReport.ReportPath = credential.ReportPath("SubbookByDate")
                xParam.Add(New ReportParameter("Bdate", Request("b").ToString()))
                xParam.Add(New ReportParameter("Edate", Request("e").ToString()))
            Case "A1"
                FagRPT = Request("RPTTYPE")
                ReportFileName = "Agreement" & ConvertDate2DBString(Now.Date)
                ReportViewer1.ServerReport.ReportPath = credential.ReportPath("Agreement")
                xParam.Add(New ReportParameter("Code", "A1"))
                xParam.Add(New ReportParameter("UserID", Request.Cookies("UserID").Value))
            Case "card"
                FagRPT = "PDF"
                ReportViewer1.ServerReport.ReportPath = credential.ReportPath("Card")
                xParam.Add(New ReportParameter("AccNo", Request("code").ToString()))
            Case Else

        End Select

        'ReportViewer1.ServerReport.ReportPath = credential.ReportPath("StudentBio")
        'xParam.Add(New ReportParameter("Username", Request.Cookies("Username").Value.ToString()))
        ReportViewer1.ServerReport.SetParameters(xParam)

        Select Case FagRPT
            Case "EXCEL"

                ' Variables
                Dim warnings As Warning()
                Dim streamIds As String()
                Dim mimeType As String = String.Empty
                Dim encoding As String = String.Empty
                Dim extension As String = String.Empty


                ' Setup the report viewer object and get the array of bytes

                Dim bytes As Byte() = ReportViewer1.ServerReport.Render("EXCEL", Nothing, mimeType, encoding, extension, streamIds, Nothing)

                ' Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
                Response.Buffer = True
                Response.Clear()
                Response.ContentType = mimeType
                Response.AddHeader("content-disposition", Convert.ToString("attachment; filename=" & ReportFileName & "." & extension))
                Response.BinaryWrite(bytes)
                ' create the file
                Response.Flush()

                ' send it to the client to download
            Case Else
                ' Variables
                Dim warnings As Warning()
                Dim streamIds As String()
                Dim mimeType As String = String.Empty
                Dim encoding As String = String.Empty
                Dim extension As String = String.Empty


                ' Setup the report viewer object and get the array of bytes

                Dim bytes As Byte() = ReportViewer1.ServerReport.Render("PDF", Nothing, mimeType, encoding, extension, streamIds, Nothing)

                ' Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
                Response.Buffer = True
                Response.Clear()
                Response.ContentType = mimeType
                'Response.AddHeader("content-disposition", Convert.ToString("attachment; filename=C:\ttt.pdf"))
                Response.BinaryWrite(bytes)
                ' create the file
                'Response.Flush()
                ' send it to the client to download

        End Select
        '
    End Sub
End Class