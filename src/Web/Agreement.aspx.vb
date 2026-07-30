Imports System.Drawing
Imports Org.BouncyCastle.Crypto

Public Class Agreement
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlC As New CustomerController
    Dim ctlA As New AgreementController
    Dim ctlU As New UserController
    Dim ctlM As New MasterController
    Dim ctlG As New GuaranteeController
    Private UploadDirectory As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Request.Cookies("KDP")) Then
            Response.Redirect("Default.aspx")
        End If

        If Not IsPostBack Then
            cmdSave.Visible = True
            cmdDelete.Visible = False
            cmdCancel.Visible = False
            cmdPrintContract.Visible = False
            cmdPrintGuarantee.Visible = False
            cmdPrintDebt.Visible = False
            pnCancel.Visible = False
            pnArg2.Visible = False
            hdAgreementTypeUID.Value = Request("t")

            txtAgreementNo.Text = ctlM.RunningNumber2_New("A")
            txtAgreementNo.ReadOnly = True

            txtAgreementDate.Text = Today.Date.ToString("dd/MM/yyyy")
            txtDuedate.Text = "31/12/" & Today.Year() + 543

            txtRate.Text = ctlM.SystemConfig_GetValue("INTRATE")
            DisableControl()

            LoadCustomerToDDL()
            LoadGuarantee()
            If Request("id") IsNot Nothing Then
                LoadAgreementData()
            End If
            pnBalance.Visible = False
            txtObjective.Enabled = False
            Select Case hdAgreementTypeUID.Value
                Case "1"
                    pnArg1.Visible = True
                    pnArg2.Visible = False
                    pnBalance.Visible = True
                    lblAgreementType.Text = "สัญญาเงินกู้"
                Case "2"
                    lblAgreementType.Text = "สัญญาเช่า-ซื้อ"
                    pnArg2.Visible = True
                    pnArg1.Visible = True
                    txtObjective.Enabled = True
                Case "3"
                    lblAgreementType.Text = "สัญญาซื้อขาย"
                    pnArg2.Visible = True
                    pnArg1.Visible = True
            End Select
        End If

        cmdCancel.Attributes.Add("onClick", "javascript:return confirm(""คุณต้องการยกเลิกคำขอนี้ใช่หรือไม่?"");")
        cmdDelete.Attributes.Add("onClick", "javascript:return confirm(""ต้องการลบข้อมูลนี้ใช่หรือไม่?"");")

        txtAmount.Attributes.Add("OnKeyPress", "return AllowOnlyDouble();")
        txtRate.Attributes.Add("OnKeyPress", "return AllowOnlyDouble();")
    End Sub
    Private Sub DisableControl()
        txtCustomerName.ReadOnly = True
        txtCustomerID.ReadOnly = True
        txtNickName.ReadOnly = True
        txtCardID.ReadOnly = True
        txtTel.ReadOnly = True
        txtAddress.ReadOnly = True

        txtCustomerID.BackColor = Color.White
        txtCustomerName.BackColor = Color.White
        txtNickName.BackColor = Color.White
        txtCardID.BackColor = Color.White
        txtTel.BackColor = Color.White
        txtAddress.BackColor = Color.White

    End Sub

    'Private Sub LoadAgreementType()
    '    ddlAgreementType.DataSource = ctlA.AgreementType_Get
    '    ddlAgreementType.DataTextField = "Name"
    '    ddlAgreementType.DataValueField = "UID"
    '    ddlAgreementType.DataBind()
    'End Sub

    Private Sub LoadCustomerToDDL()
        dt = ctlC.Customer_GetForSelection()

        If dt.Rows.Count > 0 Then
            With ddlCustomer
                .DataSource = dt
                .DataTextField = "CustomerName"
                .DataValueField = "UID"
                .DataBind()
            End With
        End If
    End Sub

    Private Sub LoadAgreementData()
        dt = ctlA.Agreement_GetByUID(Request("id"))
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                hdAgreementUID.Value = String.Concat(.Item("UID"))
                hdCustomerUID.Value = String.Concat(.Item("CustomerUID"))
                txtAgreementNo.Text = String.Concat(.Item("Code"))
                'ddlAgreementType.SelectedValue = String.Concat(.Item("AgreementType"))
                hdAgreementTypeUID.Value = String.Concat(.Item("AgreementType"))
                lblAgreementType.Text = String.Concat(.Item("AgreementTypeName"))
                txtAgreementDate.Text = ConvertStrDate2ShortTH(String.Concat(.Item("AgreementDate")))
                txtDuedate.Text = ConvertStrDate2ShortTH(String.Concat(.Item("EndDate")))
                ddlCreditor.SelectedValue = String.Concat(.Item("Creditor"))
                txtPlace.Text = String.Concat(.Item("Place"))
                txtAmount.Text = DBNull2Dbl(.Item("Amount")).ToString("#,##0.##")
                txtRate.Text = String.Concat(.Item("Interest"))

                txtBalance.Text = (DBNull2Dbl(.Item("Amount")) - DBNull2Dbl(.Item("PayAmount"))).ToString("#,##0.##")

                ddlCustomer.SelectedValue = String.Concat(.Item("CustomerUID"))
                LoadCustomerDetail(DBNull2Zero(.Item("CustomerUID")))

                txtG1Name.Text = String.Concat(.Item("GName1"))
                txtG1Age.Text = String.Concat(.Item("GAge1"))
                txtG1Tel.Text = String.Concat(.Item("GTel1"))
                txtG1Address.Text = String.Concat(.Item("GAddress1"))

                txtG2Name.Text = String.Concat(.Item("GName2"))
                txtG2Age.Text = String.Concat(.Item("GAge2"))
                txtG2Tel.Text = String.Concat(.Item("GTel2"))
                txtG2Address.Text = String.Concat(.Item("GAddress2"))

                If String.Concat(.Item("AgreementType")) = "1" Then
                    pnArg1.Visible = True
                    pnArg2.Visible = False
                Else
                    pnArg2.Visible = True
                    pnArg1.Visible = False
                End If

                txtProduct.Text = String.Concat(.Item("ProductDetail"))
                txtObjective.Text = String.Concat(.Item("Objective"))


                If String.Concat(.Item("AgreementStatus")) = "0" Then
                    lblStatus.Text = "ปิดบัญชี"
                    lblStatus.CssClass = "badge badge-danger"
                    cmdSave.Visible = False
                    cmdDelete.Visible = False
                    cmdCancel.Visible = False
                    cmdAdd.Enabled = False
                    grdGuarantee.Enabled = False
                Else
                    cmdDelete.Visible = True
                    cmdCancel.Visible = True
                End If
            End With
            cmdPrintContract.Visible = True
            cmdPrintGuarantee.Visible = True
            cmdPrintDebt.Visible = True

            LoadGuarantee()
        Else
        End If
    End Sub


    'Public Sub lineNotify(ByVal msg As String)

    '    ServicePointManager.Expect100Continue = True
    '    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

    '    Dim token As String = "NkKRAmJdgdnNCMG3WZV2ADLBDylR0dMq9fZUeQovMCI"
    '    'Dim msg As String = "ทดสอบจ้าาา...."

    '    Try
    '        Dim request = CType(WebRequest.Create("https://notify-api.line.me/api/notify"), HttpWebRequest)
    '        Dim postData = String.Format("message={0}", msg)
    '        Dim data = Encoding.UTF8.GetBytes(postData)
    '        request.Method = "POST"
    '        request.ContentType = "application/x-www-form-urlencoded"
    '        request.ContentLength = data.Length
    '        request.Headers.Add("Authorization", "Bearer " & token)

    '        Using stream = request.GetRequestStream()
    '            stream.Write(data, 0, data.Length)
    '        End Using

    '        Dim response = CType(request.GetResponse(), HttpWebResponse)
    '        Dim responseString = New StreamReader(response.GetResponseStream()).ReadToEnd()
    '    Catch ex As Exception
    '        Console.Write(ex.ToString())
    '    End Try
    'End Sub

    Protected Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalCancel(this,'ยกเลิกคำขอ','คุณต้องการยกเลิกคำขอใช่หรือไม่?');", True)

        'ctlA.Agreement_Cancel(StrNull2Zero(hdAgreementUID.Value))
        'Response.Redirect("ResultPage.aspx?p=request&t=cancel")
    End Sub
    Protected Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        ctlA.Agreement_Delete(StrNull2Zero(hdAgreementUID.Value))
        Response.Redirect("ResultPage.aspx?p=request&t=del")
        'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','ลบ Request เรียบร้อย');", True)
    End Sub

    Protected Sub cmdConfirm_Click(sender As Object, e As EventArgs) Handles cmdConfirm.Click
        'Dim ctlA As New AgreementController
        'ctlA.Agreement_SendApprove(hdAgreementUID.Value, hdCustomerUID.Value, Request.Cookies("UserID").Value)
        'LoadAgreementData()
        'LoadAgreementData()
        'CheckStatusAgreement(StrNull2Long(hdAgreementUID.Value))
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','ส่งเรื่องพิจารณาเรียบร้อย');", True)
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click

        If txtAgreementDate.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุวันที่ทำสัญญา');", True)
            Exit Sub
        End If
        If txtPlace.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุสถานที่ทำสัญญา');", True)
            Exit Sub
        End If
        If StrNull2Zero(txtAmount.Text) <= 0 Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุจำนวนวงเงิน');", True)
            Exit Sub
        End If
        If StrNull2Zero(txtRate.Text) < 0 Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุอัตราดอกเบี้ยให้ถูกต้อง');", True)
            Exit Sub
        End If

        'เลขที่สัญญา กับ เลขที่บัญชี คือเลขเดียวกัน เสมอ 
        Dim AccNo, AgreementDate, Duedate As String
        Dim ctlCa As New CardController
        AgreementDate = ConvertStrDate2DBDate(txtAgreementDate.Text)
        Duedate = ConvertStrDate2DBDate(txtDuedate.Text)

        ctlA.Agreement_Save(StrNull2Zero(hdAgreementUID.Value), txtAgreementNo.Text, StrNull2Zero(ddlCustomer.SelectedValue), hdAgreementTypeUID.Value, AgreementDate, txtPlace.Text, StrNull2Double(txtAmount.Text), StrNull2Double(txtRate.Text), txtG1Name.Text, txtG1Age.Text, txtG1Tel.Text, txtG1Address.Text, txtG2Name.Text, txtG2Age.Text, txtG2Tel.Text, txtG2Address.Text, StrNull2Double(txtBalance.Text), "", txtProduct.Text, txtObjective.Text, Request.Cookies("UserID").Value, ddlCreditor.SelectedValue, Duedate)

        If StrNull2Zero(hdAgreementUID.Value) = 0 Then
            ctlM.RunningNumber_Update("A")
            AccNo = ctlM.RunningNumber2_New("ACCNO")

            ctlU.User_GenLogfile(Request.Cookies("Username").Value, "ADD", "Agreement", "ทำสัญญาใหม่:" & txtAgreementNo.Text, "CustUID=" & ddlCustomer.SelectedValue & ":" & txtCustomerName.Text & "|Amount=" & txtAmount.Text & "|Ints=" & txtRate.Text)

            hdAgreementUID.Value = ctlA.Agreement_GetUID(txtAgreementNo.Text)

        Else
            ctlU.User_GenLogfile(Request.Cookies("Username").Value, "UPD", "Agreement", "แก้ไขสัญญา:" & txtAgreementNo.Text, "CustUID=" & ddlCustomer.SelectedValue & ":" & txtCustomerName.Text & "|Amount=" & txtAmount.Text & "|Ints=" & txtRate.Text)
            AccNo = txtAgreementNo.Text
        End If

        If StrNull2Zero(hdAgreementTypeUID.Value) = 1 Then
            ctlCa.Card_Save(AccNo, txtAgreementNo.Text, StrNull2Zero(hdCustomerUID.Value), 0, 0, 0, Request.Cookies("UserID").Value)
        End If

        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกสัญญาเรียบร้อย');", True)

        cmdPrintContract.Visible = True
        cmdPrintGuarantee.Visible = True
        cmdPrintDebt.Visible = True
        cmdDelete.Visible = True

    End Sub
    Private Sub LoadGuarantee()
        dt = ctlG.Guarantee_Get(txtAgreementNo.Text)
        grdGuarantee.DataSource = dt
        grdGuarantee.DataBind()
    End Sub

    Protected Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        'Add Gaurantree
        If txtDescritions.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','ป้อนรายการหลักค้ำประกันก่อน');", True)
            Exit Sub
        End If

        ctlG.Guarantee_Add(txtAgreementNo.Text, txtDescritions.Text, StrNull2Zero(Request.Cookies("UserID").Value))
        LoadGuarantee()
    End Sub
    Private Sub grdGuarantee_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdGuarantee.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgDel"
                    ctlG.Guarantee_Delete(e.CommandArgument)
                    LoadGuarantee()
            End Select
        End If
    End Sub

    Private Sub grdGuarantee_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdGuarantee.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then

            Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
            Dim imgD As ImageButton = e.Row.Cells(2).FindControl("imgDel")
            imgD.Attributes.Add("onClick", scriptString)

        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#d0e8ff';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If
    End Sub

    Private Sub cmdConfirmCancel_Click(sender As Object, e As EventArgs) Handles cmdConfirmCancel.Click

        'ctlA.Agreement_Cancel(StrNull2Zero(hdAgreementUID.Value), txtCancelRemark.Text)
        'ctlC.RequestTransaction_Add(StrNull2Zero(hdAgreementUID.Value), StrNull2Zero(hdCustomerUID.Value), 0, "ยกเลิกคำขอ : " & txtCancelRemark.Text, Request.Cookies("UserID").Value)

        Response.Redirect("ResultPage.aspx?p=request&t=cancel")
    End Sub

    Private Sub ddlCustomer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustomer.SelectedIndexChanged
        LoadCustomerDetail(ddlCustomer.SelectedValue)
    End Sub
    Private Sub LoadCustomerDetail(CustomerUID As Integer)
        Dim dtC As New DataTable
        dtC = ctlC.Customer_GetByUID(CustomerUID)
        If dtC.Rows.Count > 0 Then
            txtCustomerID.Text = String.Concat(dtC.Rows(0)("CustomerID"))
            txtCustomerName.Text = String.Concat(dtC.Rows(0)("CustomerName"))
            txtNickName.Text = String.Concat(dtC.Rows(0)("NickName"))
            txtCardID.Text = String.Concat(dtC.Rows(0)("CardID"))
            txtTel.Text = String.Concat(dtC.Rows(0)("Tel"))
            txtAddress.Text = String.Concat(dtC.Rows(0)("CustomerAddress"))
        End If
        hdCustomerUID.Value = CustomerUID
    End Sub

    'Private Sub ddlAgreementType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAgreementType.SelectedIndexChanged
    '    If ddlAgreementType.SelectedIndex = 1 Then
    '        pnArg2.Visible = True
    '        'If StrNull2Zero(hdAgreementUID.Value) = 0 Then
    '        '    txtAgreementNo.Text = ctlM.RunningNumber2_New("B")
    '        'End If
    '    Else
    '        pnArg2.Visible = False
    '        'If StrNull2Zero(hdAgreementUID.Value) = 0 Then
    '        '    txtAgreementNo.Text = ctlM.RunningNumber2_New("A")
    '        'End If
    '    End If
    'End Sub
    Protected Sub cmdPrintContract_Click(sender As Object, e As EventArgs) Handles cmdPrintContract.Click
        ReportTitle = "หนังสือสัญญา"

        Select Case hdAgreementTypeUID.Value
            Case "1"
                ReportName = "contract1"
            Case "2"
                ReportName = "contract2"
            Case "3"
                ReportName = "contract3"
        End Select
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Report", "window.open('ReportViewer.aspx?rpt=" & ReportName & "&id=" & hdAgreementUID.Value & "&code=" & txtAgreementNo.Text & "','_blank');", True)


        ''ReportFormula = "{View_KCB_CardDetail.ACCNO}= '" & txtCustomerCode.Text & "' AND {View_KCB_CardDetail.CardType}= '2' AND  {View_KCB_CardDetail.Loan_Amount}=" & chkNullByDouble(txtLoanAmount.Text) & " AND {View_KCB_CardDetail.INVNO}= '" & invno & "' AND {View_KCB_CardDetail.PAYDATE}= '" & dpermit & "'"

        ''Response.Redirect("ReportViewer.aspx?m=" & ReportName)

        'ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Report", "window.open('ReportViewer.aspx?rpt=" & ReportName & "&id=" & hdAgreementUID.Value & "&code=" & txtAgreementNo.Text & "&RPTTYPE=PDF','_blank');", True)


        ''ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSend(this,'ยืนยันการส่งเรื่องพิจารณา','คุณต้องการส่งเรื่องเพื่อพิจารณา ใช่หรือไม่?');", True)
        ''ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกข้อมูลเรียบร้อย');", True)
    End Sub

    Private Sub cmdPrintGuarantee_Click(sender As Object, e As EventArgs) Handles cmdPrintGuarantee.Click
        ReportTitle = "หนังสือสัญญาค้ำประกัน"
        ReportName = "guarantor"

        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Report", "window.open('ReportViewer.aspx?rpt=" & ReportName & "&id=" & hdAgreementUID.Value & "&code=" & txtAgreementNo.Text & "&RPTTYPE=PDF','_blank');", True)
    End Sub

    Private Sub cmdPrintDebt_Click(sender As Object, e As EventArgs) Handles cmdPrintDebt.Click
        ReportTitle = "หนังสือรับสภาพหนี้"
        ReportName = "debtagree"

        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Report", "window.open('ReportViewer.aspx?rpt=" & ReportName & "&id=" & hdAgreementUID.Value & "&code=" & txtAgreementNo.Text & "&RPTTYPE=PDF','_blank');", True)
    End Sub
End Class