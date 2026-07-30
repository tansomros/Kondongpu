Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports Microsoft.Reporting.WebForms

Public Class ReportViewerStudentBio
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Public Shared ReportFormula As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Session("m") = Request("m")

        If Request.Browser.Browser = "Firefox" Then
            Panel1.Visible = True
        ElseIf Request.Browser.Browser = "IE" Then
            Panel1.Visible = True
        Else
            Panel1.Visible = False

            If Not Request("id") Is Nothing Then
                LoadData(Request("id"))
            Else
                LoadData("")
            End If

            'LoadReport()

        End If



    End Sub

    Private Sub LoadReport()

        'System.Threading.Thread.Sleep(1000)
        'UpdateProgress1.Visible = True

        Dim credential As New ReportServerCredentials
        ReportViewer1.Reset()
        ReportViewer1.ServerReport.ReportServerCredentials = credential
        ReportViewer1.ServerReport.ReportServerUrl = credential.ReportServerUrl
        ReportViewer1.ShowPrintButton = True

        Dim xParam As New List(Of ReportParameter)

        Select Case Request("m")
            Case "cert"
                ReportViewer1.ServerReport.ReportPath = credential.ReportPath("StudentCertificate")
                xParam.Add(New ReportParameter("EduYear", Request("y").ToString()))
                xParam.Add(New ReportParameter("Username", Request.Cookies("Username").Value.ToString()))



            Case Else
                ReportViewer1.ServerReport.ReportPath = credential.ReportPath("StudentBio")
                xParam.Add(New ReportParameter("Username", Request.Cookies("Username").Value.ToString()))
        End Select



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
                Response.AddHeader("content-disposition", Convert.ToString("attachment; filename=" & ReportName & "_" & ConvertStrDate2DBString(Request("b")) & "_" & ConvertStrDate2DBString(Request("e")) & "." & extension))
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
    Private Sub LoadData(pID As String)

        Dim ctlstd As New StudentController

        Select Case Request("m")
            Case "cert"
                lblReportTitle.Text = "พิมพ์ใบรับรองการผ่านการฝึกปฏิบัติงาน"
                Session("rptyear") = Request("y")
                Session("rptcertdate") = Request("d")

                If pID <> "" Then
                    ctlstd.TMP_Student_SET2CERTBySTDID(Request("id"), Request.Cookies("Username").Value, Request("d"))
                Else
                    If ReportFormula <> "" Then
                        ctlstd.TMP_Student_SET2CERT(ReportFormula, Request.Cookies("Username").Value, Request("d"))
                    End If
                End If
            Case "certregis"
                lblReportTitle.Text = "พิมพ์แบบรายงานการฝึกปฏิบัติงานสำหรับผู้สมัครสอบความรู้เพื่อขึ้นทะเบียนเป็นผู้ประกอบวิชาชีพเภสัชกรรม"

                If pID <> "" Then
                    ctlstd.TMP_Student_SET2CERTBySTDID(Request("id"), Request.Cookies("Username").Value, Request("d"))
                Else
                    If ReportFormula <> "" Then
                        ctlstd.TMP_Student_SET2CERT(ReportFormula, Request.Cookies("Username").Value, Request("d"))
                    End If
                End If


            Case Else
                lblReportTitle.Text = "พิมพ์ประวัตินิสิต"
                If pID <> "" Then
                    ctlstd.TMP_Student_SET2BIOBySTDID(Request("id"), Request.Cookies("Username").Value)
                Else
                    If ReportFormula <> "" Then
                        ctlstd.TMP_Student_SET2BIO(ReportFormula, Request.Cookies("Username").Value)
                    End If
                End If
        End Select



    End Sub

End Class