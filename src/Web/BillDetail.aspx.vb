Imports System.Drawing
Imports System.IO
Imports System.Net
Imports System.Net.Mail
Imports System.Security.Cryptography
Imports System.Windows.Forms
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip
Imports DevExpress.XtraPrinting.Export.Pdf
Imports DevExpress.XtraRichEdit.Layout
Imports Org.BouncyCastle.Asn1.Ocsp
Imports Org.BouncyCastle.Crypto.Parameters
Imports Org.BouncyCastle.Ocsp

Public Class BillDetail
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlU As New UserController
    Dim ctlM As New MasterController
    Dim ctlC As New CustomerController
    Dim ctlS As New BillController
    Dim objDB As New BaseClass
    Dim RowItemIndex As Integer = 0
    Private Property dtBill As DataTable
        Get
            If Session("dtPay") Is Nothing Then
                Session("dtPay") = New DataTable
            End If
            Return CType(Session("dtPay"), DataTable)
        End Get
        Set(value As DataTable)
            Session("dtPay") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Request.Cookies("KDP")) Then
            Response.Redirect("Default.aspx")
        End If

        If Not IsPostBack Then
            cmdSave.Visible = True
            cmdDelete.Visible = False
            cmdPrint.Visible = False

            lblBillNumber.Text = "Auto ID"
            txtBillDate.Text = Today.Date.ToString("dd/MM/yyyy")
            txtSendDate.Text = Today.Date.ToString("dd/MM/yyyy")
            dtBill.Columns.Clear()

            With dtBill
                .Columns.Add("BillNumber")
                .Columns.Add("BillDate")
                .Columns.Add("CompanyCode")
                .Columns.Add("CustomerID")
                .Columns.Add("CarRegisNumber")
                .Columns.Add("CaneTypeUID")
                .Columns.Add("CaneName") 'Descriptions in Deduction
                .Columns.Add("Weight")
                .Columns.Add("UnitPrice")
                .Columns.Add("NetPrice")
                .Columns.Add("BillFlag")
                .Columns.Add("SendDate")
                .Columns.Add("BillReference")
                .Columns.Add("Remark")
                .Columns.Add("UID")
                .Columns.Add("ACCID")
                .Columns.Add("GasPrice")
                .Columns.Add("NetBalance")
                .Columns.Add("SEQUID")
                .Columns.Add("iSendDate")

            End With
            LoadCustomerToDDL()
            LoadCaneType()
            LoadCompany()
            LoadCarToDDL()
            LoadAccountToDDL()

            If Not Request("id") Is Nothing Then
                LoadBillData(Request("id"))
                'cmdSave.Visible = False
                cmdDelete.Visible = True
                cmdPrint.Visible = True
            End If
        End If

        cmdDelete.Attributes.Add("onClick", "javascript:return confirm(""ต้องการลบใบเสร็จรับเงินนี้ใช่หรือไม่?"");")
        'txtUnitPrice.Attributes.Add("OnKeyPress", "return AllowOnlyDouble();")
        txtWeight.Attributes.Add("OnKeyPress", "return AllowOnlyDouble();")
        txtGasPrice.Attributes.Add("OnKeyPress", "return AllowOnlyDouble();")
        txtDeductAmount.Attributes.Add("OnKeyPress", "return AllowOnlyDouble();")
    End Sub
    Private Sub LoadBillData(Id As Integer)
        dt = ctlS.Bill_GetByUID(Id)
        If dt.Rows.Count > 0 Then
            cmdPrint.Visible = True
            With dt.Rows(0)
                hdBillUID.Value = .Item("UID")
                lblBillNumber.Text = .Item("BillNumber")
                txtBillDate.Text = .Item("BillDate")
                ddlCustomer.SelectedValue = .Item("CustomerUID")
                'txtCustomerName.Text = .Item("CustomerName") + "(" + .Item("NickName") + ")"
                'txtCustomerAddress.Text = .Item("CustomerAddress")
                'ddlCompany.SelectedItem.Text = .Item("CompanyCode")
                'txtCompanyAddress.Text = .Item("CompanyAddress")
                ddlCompany.SelectedValue = .Item("CompanyUID")
                txtRemark.Text = String.Concat(.Item("Remark"))

                lblTotalWeight.Text = DBNull2Dbl(.Item("TotalWeight")).ToString("#,###.##")
                lblTotalNetPrice.Text = DBNull2Dbl(.Item("TotalNetPrice")).ToString("#,###.##")
                lblTotalDeduct.Text = DBNull2Dbl(.Item("TotalDeduct")).ToString("#,###.##")
                lblBalance.Text = DBNull2Dbl(.Item("Balance")).ToString("#,###.##")

                LoadCarToDDL()
            End With

            Dim dtP As New DataTable
            Dim dtD As New DataTable

            dtP = ctlS.BillDetail_GetByBillNumber(lblBillNumber.Text)
            If dtP.Rows.Count > 0 Then

                dtBill = dtP.Copy()
                dtD = dtP.Copy()

                dtP.DefaultView.RowFilter = "BillFlag='R'"
                grdReceipt.DataSource = dtP
                grdReceipt.DataBind()

                dtD.DefaultView.RowFilter = "BillFlag='D'"
                grdDeduct.DataSource = dtD
                grdDeduct.DataBind()

            End If


            'dtD = ctlS.BillDeduction_GetByBillNumber(lblBillNumber.Text)
            'If dtD.Rows.Count > 0 Then
            '    dtDeduct = dtD.Copy()
            '    grdDeduct.DataSource = dtDeduct
            'End If

            cmdPrint.Focus()
        Else
            cmdPrint.Visible = False
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'ผลการตรวจสอบ','ไม่พบใบเสร็จรับเงินเลขที่ " & Request("Id") & " ในฐานข้อมูล');", True)
        End If
    End Sub
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
    Private Sub LoadCarToDDL()
        Dim dtCar As New DataTable
        dtCar = ctlC.CarRegistration_Get(ddlCustomer.SelectedValue)
        If dtCar.Rows.Count > 0 Then
            With ddlCar
                .DataSource = dtCar
                .DataTextField = "RegisNumber"
                .DataValueField = "RegisNumber"
                .DataBind()
            End With
        End If
    End Sub
    Private Sub LoadAccountToDDL()
        Dim ctlA As New AccountController
        dt = ctlA.Account_GetAll()
        If dt.Rows.Count > 0 Then
            With ddlAccount
                .DataSource = dt
                .DataTextField = "AccountName"
                .DataValueField = "UID"
                .DataBind()
            End With
        End If
    End Sub
    Private Sub LoadCaneType()
        ddlCane.DataSource = ctlM.CaneType_GetForSelection
        ddlCane.DataTextField = "CaneName"
        ddlCane.DataValueField = "UID"
        ddlCane.DataBind()
    End Sub
    Private Sub LoadCompany()
        Dim ctlC As New CompanyController
        ddlCompany.DataSource = ctlC.Company_GetForSelection
        ddlCompany.DataTextField = "CompanyName"
        ddlCompany.DataValueField = "UID"
        ddlCompany.DataBind()
    End Sub
    Private Sub ClearData()
        BaseClass.ClearData(pnAddItem)
        BaseClass.ClearData(pnDeduct)

        hdBillUID.Value = "0"
        hdRUID.Value = "0"
        hdDUID.Value = "0"

        lblTotalWeight.Text = ""
        lblTotalNetPrice.Text = ""
        lblTotalDeduct.Text = ""
        lblBalance.Text = ""

        cmdPrint.Visible = False
        txtBillDate.Text = Today.Date.ToString("dd/MM/yyyy")
        txtSendDate.Text = Today.Date.ToString("dd/MM/yyyy")

        dtBill.Rows.Clear()
        grdReceipt.DataSource = dtBill
        grdDeduct.DataSource = dtBill
        grdReceipt.DataBind()
        grdDeduct.DataBind()
        lblBillNumber.Text = "Auto ID"
    End Sub
    Private Sub AddPayment2Grid(sBillNumber As String _
     , sBillDate As String _
     , sCompanyCode As String _
     , sCustomerID As String _
     , sCarRegisNumber As String _
     , sSendDate As String _
     , sCaneTypeUID As String, sCaneName As String _
     , sRemark As String _
     , sWeight As Double _
     , sUnitPrice As Double _
     , sNetPrice As Double _
     , sGasPrice As Double _
     , sNetBalance As Double _
     , sBillFlag As String _
     , sBillReference As String _
     , sUID As Integer _
     , sACCID As Integer)

        If sBillFlag = "R" Then
            If StrNull2Zero(txtUnitPrice.Text) = 0 Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'ผลการตรวจสอบ','ท่านยังไม่ได้กำหนดราคาอ้อย กรุณาไปกำหนดที่เมนูกำหนดราคา');", True)
                Exit Sub
            End If
        End If

        sBillDate = ConvertStrDate2DBString(sBillDate)
        Dim iSendDate As String = ConvertStrDate2DBString(sSendDate)
        sSendDate = ConvertStrDate2ShotDateTH(sSendDate)

        If sBillFlag = "R" Then

            If ddlCustomer.SelectedItem.Text = "" Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'ผลการตรวจสอบ','ท่านยังไม่ได้ป้อนชื่อลูกค้า');", True)
                Exit Sub
            End If
            If ddlCompany.SelectedItem.Text = "" Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'ผลการตรวจสอบ','ท่านยังไม่ได้ป้อนโรงงาน');", True)
                Exit Sub
            End If

            If sWeight <= 0 Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'ผลการตรวจสอบ','กรุณาตรวจสอบน้ำหนัก');", True)
                txtWeight.Focus()
                Exit Sub
            End If

        Else
            If ddlAccount.SelectedItem.Text = "" Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'ผลการตรวจสอบ','ท่านยังไม่ได้ป้อนรายการหัก');", True)
                Exit Sub
            End If
            If sNetPrice <= 0 Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'ผลการตรวจสอบ','ท่านยังไม่ได้ป้อนจำนวนเงินที่หัก');", True)
                txtDeductAmount.Focus()
                Exit Sub
            End If

        End If

        Dim iRow As Integer = 0
        iRow = dtBill.Rows.Count + 1
        If StrNull2Zero(hdRUID.Value) = 0 Then
            Dim dr As DataRow = dtBill.NewRow()
            dr("BillNumber") = sBillNumber
            dr("BillDate") = sBillDate
            dr("CompanyCode") = sCompanyCode
            dr("CustomerID") = sCustomerID
            dr("CarRegisNumber") = sCarRegisNumber
            dr("CaneTypeUID") = StrNull2Zero(sCaneTypeUID)
            dr("CaneName") = sCaneName
            dr("Weight") = sWeight
            dr("UnitPrice") = sUnitPrice '.ToString("#,###.##")
            dr("NetPrice") = sNetPrice.ToString("#,###.##")
            dr("BillFlag") = sBillFlag
            dr("SendDate") = sSendDate
            dr("BillReference") = sBillReference
            dr("Remark") = sRemark
            dr("UID") = 0
            dr("ACCID") = sACCID
            dr("GasPrice") = sGasPrice
            dr("NetBalance") = sNetBalance.ToString("#,###.##")
            dr("SEQUID") = iRow
            dr("iSendDate") = iSendDate


            dtBill.Rows.Add(dr)
        Else
            'หา row ที่ต้อง update จาก SEQUID ที่เก็บไว้ใน hdRUID
            Dim editSeq As Integer = StrNull2Zero(hdRUID.Value)
            For idx = 0 To dtBill.Rows.Count - 1
                If StrNull2Zero(dtBill.Rows(idx)("SEQUID")) = editSeq Then
                    With dtBill.Rows(idx)
                        .Item("BillNumber") = sBillNumber
                        .Item("BillDate") = sBillDate
                        .Item("CompanyCode") = sCompanyCode
                        .Item("CustomerID") = sCustomerID
                        .Item("CarRegisNumber") = sCarRegisNumber
                        .Item("CaneTypeUID") = sCaneTypeUID
                        .Item("CaneName") = sCaneName
                        .Item("Weight") = sWeight
                        .Item("UnitPrice") = sUnitPrice
                        .Item("NetPrice") = sNetPrice
                        .Item("BillFlag") = sBillFlag
                        .Item("SendDate") = sSendDate
                        .Item("BillReference") = sBillReference
                        .Item("Remark") = sRemark
                        .Item("UID") = sUID
                        .Item("ACCID") = sACCID
                        .Item("GasPrice") = sGasPrice
                        .Item("NetBalance") = sNetBalance
                        .Item("iSendDate") = iSendDate
                    End With
                    Exit For
                End If
            Next
            hdRUID.Value = "0"

        End If

        Dim dtP As New DataTable
        dtP = dtBill.Copy
        dtP.DefaultView.RowFilter = "BillFlag='R'"
        grdReceipt.DataSource = dtP
        grdReceipt.DataBind()

        Dim dtD As New DataTable
        dtD = dtBill.Copy
        dtD.DefaultView.RowFilter = "BillFlag='D'"
        grdDeduct.DataSource = dtD
        grdDeduct.DataBind()


        CalPrice()

        BaseClass.ClearData(pnAddItem)

        txtBillReference.Focus()
    End Sub

    Protected Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
        ReportTitle = "ใบเสร็จรับเงิน"
        FagRPT = "BillingReport"
        ReportName = "BillingReport.rpt"
        ReportFormula = "{View_BillingDetail.BillNumber} = '" & lblBillNumber.Text & "'"


        'ReportOneParameter = txtBillNumber.Text.Trim()
        'fRptView.FileName = BaseClass.ReportPath & ReportName
        'fRptView.SelectionFomula = ReportFormula


        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Report", "window.open('ReportViewer.aspx?rpt=rev&id=" & hdBillUID.Value & "&code=" & lblBillNumber.Text & "&RPTTYPE=PDF','_blank');", True)
    End Sub

    Protected Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalCancel(this,'ยกเลิกรายการ','คุณต้องการยกเลิกรายการนี้ใช่หรือไม่?');", True)
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click

        If txtBillDate.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุวันที่ชำระ');", True)
            Exit Sub
        End If

        If lblBillNumber.Text = "Auto ID" Then
            lblBillNumber.Text = objDB.RunningNumber_New("K")
            hdBillUID.Value = "0"
        End If

        CreateBill()
        InsertBillDetail()

        If hdBillUID.Value = "0" Then
            ctlC.RunningNumber_Update("K")
            hdBillUID.Value = "1"
        End If

        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกเรียบร้อย');", True)

        cmdPrint.Visible = True
        cmdDelete.Visible = True

    End Sub
    Private Sub CreateBill()

        ctlS.Bill_Save(lblBillNumber.Text _
                       , ConvertStrDate2DBString(txtBillDate.Text) _
                       , StrNull2Zero(ddlCompany.SelectedValue) _
                       , StrNull2Zero(ddlCustomer.SelectedValue) _
                       , CDbl(lblTotalWeight.Text) _
                       , CDbl(lblTotalNetPrice.Text) _
                       , CDbl(lblTotalDeduct.Text) _
                       , CDbl(lblBalance.Text) _
                       , txtRemark.Text _
                       , StrNull2Zero(Request.Cookies("UserID").Value))
    End Sub
    Private Sub InsertBillDetail()
        Dim i As Integer

        ctlS.BillDetail_DeleteByBillNumber(lblBillNumber.Text)

        For i = 0 To dtBill.Rows.Count - 1

            With dtBill.Rows(i)

                If .RowState <> DataRowState.Deleted Then
                    ctlS.BillDetail_Add(lblBillNumber.Text _
                                  , DBNull2Str(.Item("BillReference")) _
                                  , .Item("CarRegisNumber") _
                                  , DBNull2Zero(.Item("CaneTypeUID")) _
                                  , DBNull2Dbl(.Item("Weight")) _
                                  , DBNull2Dbl(.Item("UnitPrice")) _
                                  , DBNull2Dbl(.Item("NetPrice")) _
                                   , DBNull2Dbl(.Item("GasPrice")) _
                                    , DBNull2Dbl(.Item("NetBalance")) _
                                  , DBNull2Str(.Item("BillFlag")) _
                                  , .Item("Remark") _
                                  , "A" _
                                  , Request.Cookies("UserID").Value _
                                  , DBNull2Zero(.Item("ACCID")) _
                                  , DBNull2Zero(.Item("iSendDate")))
                End If
            End With
        Next

    End Sub
    Private Sub CalPrice()
        Dim i As Integer = 0
        Dim sumWeight As Double = 0
        Dim sumGas As Double = 0
        Dim sumR As Double = 0
        Dim sumD As Double = 0

        For i = 0 To dtBill.Rows.Count - 1
            If dtBill.Rows(i).RowState <> DataRowState.Deleted Then
                If dtBill.Rows(i)("BillFlag") = "R" Then
                    sumWeight = sumWeight + CDbl(dtBill.Rows(i)("Weight"))
                    sumR = sumR + CDbl(dtBill.Rows(i)("NetPrice"))
                Else
                    sumD = sumD + CDbl(dtBill.Rows(i)("NetPrice"))
                End If

                sumGas = sumGas + StrNull2Double(dtBill.Rows(i)("GasPrice"))
            End If
        Next

        sumD = sumGas + sumD

        lblTotalWeight.Text = sumWeight.ToString("#,##0.#0")
        lblTotalNetPrice.Text = sumR.ToString("#,##0.#0")
        lblTotalDeduct.Text = sumD.ToString("#,##0.#0")
        lblBalance.Text = (sumR - sumD).ToString("#,##0.#0")
    End Sub
    Private Sub LoadUnitPrice()
        If ddlCompany.SelectedItem.Text <> "" And ddlCane.SelectedItem.Text <> "" And txtSendDate.Text <> "" Then
            Dim ctlPc As New PriceController
            dt = ctlPc.Price_GetForSale(ddlCompany.SelectedValue, StrNull2Zero(ddlCane.SelectedValue), ConvertStrDate2DBDate(txtSendDate.Text))
            If dt.Rows.Count > 0 Then
                txtUnitPrice.Text = DBNull2Dbl(dt.Rows(0)("UnitPrice")).ToString("#,###.##")
                txtTotalPrice.Text = DBNull2Dbl(StrNull2Double(txtUnitPrice.Text) * StrNull2Double(txtWeight.Text)).ToString("#,###.##")
                txtUnitPrice.ForeColor = Color.Black

            Else
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','ท่านยังไม่ได้กำหนดราคาอ้อย');", True)
                txtUnitPrice.Text = "0"
                txtUnitPrice.ForeColor = Color.Red
            End If
        End If

        dt = Nothing
    End Sub
    Private Sub ValidateCarRegis(sCarRegis As String, sFlag As String)
        dt = ctlC.Customer_GetByCarRegis(sCarRegis, ddlCustomer.SelectedValue)
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                'If sFlag = "R" Then
                ddlCar.SelectedValue = .Item("RegisNumber")
                'Else
                '    txtCarDeduct.Text = .Item("RegisNumber")
                'End If

            End With
            txtSendDate.Focus()
        Else
            'MessageBox.Show("เลขทะเบียนนี้ไม่ได้ถูกกำหนดให้เป็นรถของลูกค้า.")
            txtSendDate.Focus()
        End If

    End Sub
    Private Sub cmdConfirmCancel_Click(sender As Object, e As EventArgs) Handles cmdConfirmCancel.Click
        'ctlC.Payin_Delete(StrNull2Zero(hdPayUID.Value), txtCancelRemark.Text, Request.Cookies("UserID").Value)
        'Dim dtPy As New DataTable

        'dtPy = ctlC.CardDetailPayin_GetByPayUID(StrNull2Zero(hdPayUID.Value))
        'If dtPy.Rows.Count > 0 Then
        '    For i = 0 To dtPy.Rows.Count - 1
        '        ctlC.CardDetail_CancelPay(dtPy.Rows(i)("CardDetailUID"), dtPy.Rows(i)("Amount"))
        '    Next
        'End If
        'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','ลบ/ยกเลิก รายการขำระเงินเรียบร้อย');", True)
        'Response.Redirect("ResultPage.aspx?p=request&t=cancel")
    End Sub

    Private Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        ClearData()
        cmdSave.Visible = True
        cmdDelete.Visible = False
        cmdPrint.Visible = False
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        AddPayment2Grid(lblBillNumber.Text, txtBillDate.Text, ddlCompany.SelectedValue, ddlCustomer.SelectedValue, ddlCar.SelectedValue, txtSendDate.Text, ddlCane.SelectedValue, ddlCane.SelectedItem.Text, "รับอ้อย", StrNull2Double(txtWeight.Text), StrNull2Double(txtUnitPrice.Text), StrNull2Double(txtTotalPrice.Text), StrNull2Double(txtGasPrice.Text), StrNull2Double(txtTotalPrice.Text) - StrNull2Double(txtGasPrice.Text), "R", txtBillReference.Text, StrNull2Zero(hdBillUID.Value), 0)
        CalPrice()
    End Sub

    Private Sub cmdAddDeduct_Click(sender As Object, e As EventArgs) Handles cmdAddDeduct.Click
        AddPayment2Grid(lblBillNumber.Text, txtBillDate.Text, ddlCompany.SelectedValue, ddlCustomer.SelectedValue, "", "", "", ddlAccount.SelectedValue, ddlAccount.SelectedItem.Text, 0, 0, StrNull2Double(txtDeductAmount.Text), 0, StrNull2Double(txtDeductAmount.Text), "D", txtDeductRef.Text, StrNull2Zero(hdRUID.Value), StrNull2Zero(ddlAccount.SelectedValue))

        BaseClass.ClearData(pnDeduct)

        CalPrice()
        hdDUID.Value = "0"
    End Sub

    Private Sub ddlCustomer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustomer.SelectedIndexChanged
        LoadCarToDDL()
    End Sub

    Private Sub ddlCane_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCane.SelectedIndexChanged
        LoadUnitPrice()
    End Sub

    Private Sub ddlCompany_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCompany.SelectedIndexChanged
        LoadUnitPrice()
    End Sub
    Private Function DuplicateBill() As Boolean
        If txtBillReference.Text.Trim() = "" Then Return False

        'ตรวจสอบซ้ำในรายการที่คีย์ไปแล้ว (dtBill) เฉพาะ BillFlag="R"
        Dim dupRows() As DataRow = dtBill.Select("BillReference='" & txtBillReference.Text.Trim().Replace("'", "''") & "' AND BillFlag='R'")
        If dupRows.Length > 0 Then
            Return True
        End If

        'ตรวจสอบซ้ำในฐานข้อมูล
        If ctlS.BillDetail_ChkDupBillReceipt(txtBillReference.Text, ddlCompany.SelectedValue) Then
            Return True
        End If

        Return False
    End Function

    Private Sub txtBillReference_TextChanged(sender As Object, e As EventArgs) Handles txtBillReference.TextChanged
        If DuplicateBill() Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'เลขที่บิลซ้ำ','บิลนี้ได้ทำการบันทึกในระบบแล้ว!');", True)
        End If
    End Sub

    Private Sub txtWeight_TextChanged(sender As Object, e As EventArgs) Handles txtWeight.TextChanged
        LoadUnitPrice()
    End Sub

    Private Sub grdReceipt_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdReceipt.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                'Case "imgEdit"
                '    EditReceipt(e.CommandArgument)
                Case "imgDel"
                    DeleteReceipt(e.CommandArgument)
            End Select
        End If
    End Sub

    'Private Sub EditReceipt(seqNo As Integer)
    '    Dim iSeq As Integer = StrNull2Zero(seqNo)
    '    For i = 0 To dtBill.Rows.Count - 1
    '        If StrNull2Zero(dtBill.Rows(i)("SEQUID")) = iSeq And dtBill.Rows(i)("BillFlag").ToString() = "R" Then
    '            With dtBill.Rows(i)
    '                txtBillReference.Text = .Item("BillReference").ToString()
    '                txtSendDate.Text = .Item("SendDate").ToString()
    '                ddlCar.SelectedValue = .Item("CarRegisNumber").ToString()
    '                ddlCane.SelectedValue = .Item("CaneTypeUID").ToString()
    '                txtWeight.Text = .Item("Weight").ToString()
    '                txtUnitPrice.Text = .Item("UnitPrice").ToString()
    '                txtTotalPrice.Text = .Item("NetPrice").ToString()
    '                txtGasPrice.Text = .Item("GasPrice").ToString()
    '            End With
    '            hdRUID.Value = iSeq.ToString()
    '            Exit For
    '        End If
    '    Next
    'End Sub

    Private Sub DeleteReceipt(seqNo As Integer)
        Dim iRowIndex As Integer = StrNull2Zero(seqNo)
        For i = 0 To dtBill.Rows.Count - 1
            If (dtBill.Rows(i)("SEQUID") = iRowIndex) And (dtBill.Rows(i)("BillFlag") = "R") Then
                dtBill.Rows(i).Delete()
                dtBill.AcceptChanges()
                Exit For
            End If
        Next

        Dim dtR As New DataTable
        dtR = dtBill.Copy()
        dtR.DefaultView.RowFilter = "BillFlag='R'"
        grdReceipt.DataSource = dtR
        grdReceipt.DataBind()

        CalPrice()

        txtBillReference.Focus()
    End Sub
    Private Sub DeleteDeduct(seqNo As Integer)
        Dim iRowIndex As Integer = StrNull2Zero(seqNo)
        For i = 0 To dtBill.Rows.Count - 1
            If (dtBill.Rows(i)("SEQUID") = iRowIndex) And (dtBill.Rows(i)("BillFlag") = "D") Then
                dtBill.Rows(i).Delete()
                dtBill.Rows(i).AcceptChanges()
                Exit For
            End If
        Next

        Dim dtR As New DataTable

        dtR = dtBill.Copy()

        dtR.DefaultView.RowFilter = "BillFlag='D'"
        grdDeduct.DataSource = dtR
        grdDeduct.DataBind()
        CalPrice()
        txtDeductRef.Focus()
    End Sub

    Private Sub grdDeduct_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdDeduct.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                'Case "imgEdit"
                '    EditDeduct(e.CommandArgument)
                Case "imgDel"
                    DeleteDeduct(e.CommandArgument)
            End Select
        End If
    End Sub

    'Private Sub EditDeduct(seqNo As Integer)
    '    Dim iSeq As Integer = StrNull2Zero(seqNo)
    '    For i = 0 To dtBill.Rows.Count - 1
    '        If StrNull2Zero(dtBill.Rows(i)("SEQUID")) = iSeq And dtBill.Rows(i)("BillFlag").ToString() = "D" Then
    '            With dtBill.Rows(i)
    '                ddlAccount.SelectedValue = .Item("ACCID").ToString()
    '                txtDeductRef.Text = .Item("BillReference").ToString()
    '                txtDeductAmount.Text = .Item("NetPrice").ToString()
    '            End With
    '            hdRUID.Value = iSeq.ToString()
    '            Exit For
    '        End If
    '    Next
    'End Sub
End Class