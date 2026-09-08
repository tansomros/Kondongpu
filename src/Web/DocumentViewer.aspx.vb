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
            Case "c1"
                report = New rxBillCompany()

                ' กำหนดค่า Parameter ให้รายงาน
                report.Parameters("StartDate").Value = Request("b")
                report.Parameters("EndDate").Value = Request("e")
                report.Parameters("CompanyUID").Value = Request("c")

                ' ผูกค่า @EmployeeID ของทุก StoredProc ให้ใช้ค่าจาก Report Parameter
                Dim ds As DevExpress.DataAccess.Sql.SqlDataSource = DirectCast(report, rxBillCompany).SqlDataSource1
                For Each query As DevExpress.DataAccess.Sql.SqlQuery In ds.Queries
                    For Each param As DevExpress.DataAccess.Sql.QueryParameter In query.Parameters
                        If param.Name = "@StartDate" Then
                            param.Type = GetType(String)
                            param.Value = Request("b")
                        ElseIf param.Name = "@EndDate" Then
                            param.Type = GetType(String)
                            param.Value = Request("e")
                        ElseIf param.Name = "@CompanyUID" Then
                            param.Type = GetType(Integer)
                            param.Value = Request("c")
                        End If
                    Next
                Next
            Case "c2"
                report = New rxBillCustomer()

                ' กำหนดค่า Parameter ให้รายงาน
                report.Parameters("StartDate").Value = Request("b")
                report.Parameters("EndDate").Value = Request("e")
                report.Parameters("CustomerUID").Value = Request("c")

                Dim ds As DevExpress.DataAccess.Sql.SqlDataSource = DirectCast(report, rxBillCustomer).SqlDataSource1
                For Each query As DevExpress.DataAccess.Sql.SqlQuery In ds.Queries
                    For Each param As DevExpress.DataAccess.Sql.QueryParameter In query.Parameters
                        If param.Name = "@StartDate" Then
                            param.Type = GetType(String)
                            param.Value = Request("b")
                        ElseIf param.Name = "@EndDate" Then
                            param.Type = GetType(String)
                            param.Value = Request("e")
                        ElseIf param.Name = "@CustomerUID" Then
                            param.Type = GetType(Integer)
                            param.Value = Request("c")
                        End If
                    Next
                Next
            Case "c3"
                report = New rxBillCane()

                ' กำหนดค่า Parameter ให้รายงาน
                report.Parameters("StartDate").Value = Request("b")
                report.Parameters("EndDate").Value = Request("e")
                report.Parameters("CaneUID").Value = Request("c")

                Dim ds As DevExpress.DataAccess.Sql.SqlDataSource = DirectCast(report, rxBillCane).SqlDataSource1
                For Each query As DevExpress.DataAccess.Sql.SqlQuery In ds.Queries
                    For Each param As DevExpress.DataAccess.Sql.QueryParameter In query.Parameters
                        If param.Name = "@StartDate" Then
                            param.Type = GetType(String)
                            param.Value = Request("b")
                        ElseIf param.Name = "@EndDate" Then
                            param.Type = GetType(String)
                            param.Value = Request("e")
                        ElseIf param.Name = "@CaneUID" Then
                            param.Type = GetType(String)
                            param.Value = Request("c")
                        End If
                    Next
                Next
            Case "c4"
                report = New rxBillCar()

                ' กำหนดค่า Parameter ให้รายงาน
                report.Parameters("StartDate").Value = Request("b")
                report.Parameters("EndDate").Value = Request("e")
                report.Parameters("CarRegisNumber").Value = Request("c")

                Dim ds As DevExpress.DataAccess.Sql.SqlDataSource = DirectCast(report, rxBillCar).SqlDataSource1
                For Each query As DevExpress.DataAccess.Sql.SqlQuery In ds.Queries
                    For Each param As DevExpress.DataAccess.Sql.QueryParameter In query.Parameters
                        If param.Name = "@StartDate" Then
                            param.Type = GetType(String)
                            param.Value = Request("b")
                        ElseIf param.Name = "@EndDate" Then
                            param.Type = GetType(String)
                            param.Value = Request("e")
                        ElseIf param.Name = "@CarRegisNumber" Then
                            param.Type = GetType(String)
                            param.Value = Request("c")
                        End If
                    Next
                Next
            Case "c5"
                report = New rxBillSummary()

                ' กำหนดค่า Parameter ให้รายงาน
                report.Parameters("StartDate").Value = Request("b")
                report.Parameters("EndDate").Value = Request("e")

                Dim ds As DevExpress.DataAccess.Sql.SqlDataSource = DirectCast(report, rxBillSummary).SqlDataSource1
                For Each query As DevExpress.DataAccess.Sql.SqlQuery In ds.Queries
                    For Each param As DevExpress.DataAccess.Sql.QueryParameter In query.Parameters
                        If param.Name = "@StartDate" Then
                            param.Type = GetType(String)
                            param.Value = Request("b")
                        ElseIf param.Name = "@EndDate" Then
                            param.Type = GetType(String)
                            param.Value = Request("e")
                        End If
                    Next
                Next
            Case "c6"
                report = New rxBillDeduct()

                ' กำหนดค่า Parameter ให้รายงาน
                report.Parameters("StartDate").Value = Request("b")
                report.Parameters("EndDate").Value = Request("e")

                Dim ds As DevExpress.DataAccess.Sql.SqlDataSource = DirectCast(report, rxBillDeduct).SqlDataSource1
                For Each query As DevExpress.DataAccess.Sql.SqlQuery In ds.Queries
                    For Each param As DevExpress.DataAccess.Sql.QueryParameter In query.Parameters
                        If param.Name = "@StartDate" Then
                            param.Type = GetType(String)
                            param.Value = Request("b")
                        ElseIf param.Name = "@EndDate" Then
                            param.Type = GetType(String)
                            param.Value = Request("e")
                        End If
                    Next
                Next
        End Select

        ' 3. สั่งให้รายงานข้ามขั้นตอนการถามค่า Parameter จากผู้ใช้
        report.RequestParameters = False

        ASPxWebDocumentViewer1.OpenReport(report)

    End Sub

End Class