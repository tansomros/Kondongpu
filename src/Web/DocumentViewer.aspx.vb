Public Class DocumentViewer
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Request.Cookies("KDP")) Then
            Response.Redirect("Default.aspx")
        End If
        LoadReport(Request("r"))
    End Sub

    Private Sub LoadReport(reportKey As String)
        Dim report As Object
        Dim ctlR As New ReportController

        Select Case reportKey
            'Case "asm"
            '    report = New rxAssessment()
            '    If Not Request("emp") Is Nothing Then
            '        ctlR.Tmp_Employee_Set2PrintById(Request("emp"), Request.Cookies("Username").Value)
            '    End If

            '    Dim dt As DataTable = ctlR.RPT_EmployeePrintAssessment(StrNull2Zero(Request("y")), Request.Cookies("username").Value)
            '    report.DataSource = dt
            '    report.DataMember = dt.TableName

            '    ' 2. ส่งค่าให้ Parameter (ชื่อต้องตรงกับที่ตั้งไว้ใน Designer)
            '    report.Parameters("AsmYear").Value = StrNull2Zero(Request("y"))
            '    report.Parameters("Username").Value = Request.Cookies("username").Value
            Case "bill"
                report = New rxBillReport()

                ' กำหนดค่า Parameter ให้รายงาน
                report.Parameters("BillNumber").Value = Request("id")

                ' ผูกค่า @EmployeeID ของทุก StoredProc ให้ใช้ค่าจาก Report Parameter
                Dim ds As DevExpress.DataAccess.Sql.SqlDataSource = DirectCast(report, rxBillReport).SqlDataSource1
                For Each query As DevExpress.DataAccess.Sql.SqlQuery In ds.Queries
                    For Each param As DevExpress.DataAccess.Sql.QueryParameter In query.Parameters
                        If param.Name = "@BillNumber" Then
                            param.Type = GetType(String)
                            param.Value = Request("id")
                        End If
                    Next
                Next
        End Select

        ' 3. สั่งให้รายงานข้ามขั้นตอนการถามค่า Parameter จากผู้ใช้
        report.RequestParameters = False

        ASPxWebDocumentViewer1.OpenReport(report)

    End Sub

End Class