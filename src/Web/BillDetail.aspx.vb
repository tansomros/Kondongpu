Imports System.Drawing
Imports System.IO
Imports System.Net
Imports System.Net.Mail
Imports System.Security.Cryptography
Imports System.Windows.Forms
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
    Dim IsFindCust As Boolean = False
    Dim RowItemIndex As Integer = 0
    Dim dtPay As New DataTable

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

            With dtPay
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
                LoadBillingData(Request("id"))
                cmdSave.Visible = False
                cmdDelete.Visible = True
                cmdPrint.Visible = True
            End If
        End If
        'cmdDelete.Attributes.Add("onClick", "javascript:return confirm(""ต้องการลบข้อมูลนี้ใช่หรือไม่?"");")

        txtUnitPrice.Attributes.Add("OnKeyPress", "return AllowOnlyDouble();")
        'txtRate.Attributes.Add("OnKeyPress", "return AllowOnlyDouble();")
    End Sub
    Private Sub LoadBillingData(Id As Integer)
        dt = ctlS.Bill_GetByUID(Id)
        If dt.Rows.Count > 0 Then
            cmdPrint.Visible = True
            With dt.Rows(0)
                hdBillUID.Value = .Item("UID")
                lblBillNumber.Text = .Item("BillNumber")
                txtBillDate.Text = .Item("BillDate")
                ddlCustomer.SelectedValue = .Item("CustomerID")
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

            End With

            Dim dtP As New DataTable
            Dim dtD As New DataTable

            dtP = ctlS.BillDetail_GetByBillNumber(lblBillNumber.Text)
            If dtP.Rows.Count > 0 Then

                dtPay = dtP.Copy()
                dtD = dtP.Copy()

                dtP.DefaultView.RowFilter = "BillFlag='R'"
                grdReceipt.DataSource = dtP

                dtD.DefaultView.RowFilter = "BillFlag='D'"
                grdDeduct.DataSource = dtD

            End If


            'dtD = ctlS.BillDeduction_GetByBillNumber(lblBillNumber.Text)
            'If dtD.Rows.Count > 0 Then
            '    dtDeduct = dtD.Copy()
            '    grdDeduct.DataSource = dtDeduct
            'End If

            cmdPrint.Focus()
        Else
            cmdPrint.Visible = False
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'ผลการตรวจสอบ','ไม่พบใบเสร็จรับเงินเลขที่ " & Request("Id") & "ในฐานข้อมูล');", True)
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
        dt = ctlC.CarRegistration_Get(ddlCustomer.SelectedValue)
        If dt.Rows.Count > 0 Then
            With ddlCar
                .DataSource = dt
                .DataTextField = "RegisNumber"
                .DataValueField = "UID"
                .DataBind()
            End With
        End If
    End Sub
    Private Sub LoadAccountToDDL()
        Dim ctlA As New AccountController
        dt = ctla.Account_GetAll()
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
        ddlCane.DataSource = ctlM.CaneType_Get
        ddlCane.DataTextField = "CaneName"
        ddlCane.DataValueField = "UID"
        ddlCane.DataBind()
    End Sub
    Private Sub LoadCompany()
        Dim ctlC As New CompanyController
        ddlCompany.DataSource = ctlC.Company_Get
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

        cmdPrint.Visible = False
        Today.Date.ToString("dd/MM/yyyy")

        dtPay.Rows.Clear()
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
        iRow = dtPay.Rows.Count + 1
        If BaseClass.isLineAdd = True Then
            Dim dr As DataRow = dtPay.NewRow()
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


            dtPay.Rows.Add(dr)
        Else
            With dtPay.Rows(RowItemIndex)
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

        End If

        Dim dtP As New DataTable
        dtP = dtPay.Copy
        dtP.DefaultView.RowFilter = "BillFlag='R'"
        grdReceipt.DataSource = dtP

        Dim dtD As New DataTable
        dtD = dtPay.Copy
        dtD.DefaultView.RowFilter = "BillFlag='D'"
        grdDeduct.DataSource = dtD


        CalPrice()

        BaseClass.ClearData(pnAddItem)

        txtBillReference.Focus()
    End Sub

    Protected Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Report", "window.open('ReportViewer.aspx?rpt=rev&id=" & hdBillUID.Value & "&code=" & lblBillNumber.Text & "&RPTTYPE=PDF','_blank');", True)
    End Sub

    Protected Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalCancel(this,'ยกเลิกรายการ','คุณต้องการยกเลิกรายการนี้ใช่หรือไม่?');", True)
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        'If StrNull2Zero(txtLoanBalance.Text) <= 0 Then
        '    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','ยอดหนี้คงเหลือ 0 บาท ไม่ต้องชำระอีก');", True)
        '    Exit Sub
        'End If
        If txtSendDate.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุวันที่ชำระ');", True)
            Exit Sub
        End If

        If StrNull2Zero(txtUnitPrice.Text) <= 0 Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุจำนวนเงิน');", True)
            Exit Sub
        End If
        'If StrNull2Zero(txtUnitPrice.Text) > StrNull2Zero(txtLoanBalance.Text) Then
        '    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','ท่านระบุจำนวนชำระมากว่ายอดหนี้');", True)
        '    Exit Sub
        'End If

        Dim PayDate As String
        Dim BankAccount As String
        PayDate = ConvertStrDate2DBString(txtSendDate.Text)
        'If optPayType.SelectedValue = "T" Then
        '    BankAccount = ddlBank.SelectedValue
        'Else
        '    BankAccount = ""
        'End If

        Dim SubbookNo As String = ""
        'If hdPayUID.Value = "0" Then
        '    SubbookNo = ctlM.RunningNumber2_New("S")
        '    lblbillnumber.Text = SubbookNo
        'End If

        'Dim Desc As String = "รับชำระเงินกู้ " & txtCustomerName.Text & " เลขที่บัญชี " & lblBillNumber.Text & " จำนวนเงิน " & StrNull2Double(txtUnitPrice.Text).ToString("#,###.#0") & " บาท โดย" & optPayType.SelectedItem.Text



        'ctlC.Payin_Add(SubbookNo, lblBillNumber.Text, "1", optPayType.SelectedValue, PayDate, StrNull2Double(txtUnitPrice.Text), "รับชำระเงินกู้", "", BankAccount, Desc, txtRemark.Text, Request.Cookies("UserID").Value)


        'ctlU.User_GenLogfile(Request.Cookies("Username").Value, "ADD", "CardDetail", "กู้เงินกู้ใหม่:" & lblbillnumber.Text, "AccNo=" & lblBillNumber.Text & "|Amount=" & txtUnitPrice.Text & "|Ints=" & txtRate.Text & "|Day=" & txtDay.Text)


        If lblBillNumber.Text = "Auto ID" Then
            lblBillNumber.Text = objDB.RunningNumber_New("K")
            hdBillUID.Value = "0"
        End If

        CreateBillHeader()
        InsertBillDetail()

        If hdBillUID.Value = "0" Then
            ctlC.RunningNumber_Update("K")
            hdBillUID.Value = "1"
        End If

        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกเรียบร้อย');", True)

        cmdPrint.Visible = True
        cmdDelete.Visible = True

    End Sub
    Private Sub CreateBillHeader()

        ctlS.Bill_Add(lblBillNumber.Text _
                       , ConvertStrDate2DBString(txtBillDate.Text) _
                       , String.Concat(ddlCompany.SelectedItem.Text) _
                       , String.Concat(ddlCustomer.SelectedItem.Text) _
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

        For i = 0 To dtPay.Rows.Count - 1

            With dtPay.Rows(i)

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

        For i = 0 To dtPay.Rows.Count - 1
            If dtPay.Rows(i).RowState <> DataRowState.Deleted Then
                If dtPay.Rows(i)("BillFlag") = "R" Then
                    sumWeight = sumWeight + CDbl(dtPay.Rows(i)("Weight"))
                    sumR = sumR + CDbl(dtPay.Rows(i)("NetPrice"))
                Else
                    sumD = sumD + CDbl(dtPay.Rows(i)("NetPrice"))
                End If

                sumGas = sumGas + StrNull2Double(dtPay.Rows(i)("GasPrice"))
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
            dt = ctlPc.Price_GetForSale(ddlCompany.SelectedItem.Text, StrNull2Zero(ddlCane.SelectedItem.Text), ConvertDate2DB(txtSendDate.Text))
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
        dt = ctlC.Customer_GetByCarRegis(sCarRegis, ddlCustomer.SelectedItem.Text)
        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                'If sFlag = "R" Then
                ddlCar.SelectedItem.Text = .Item("RegisNumber")
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
        'AddDeduction2Grid(txtBillNumber.Text, txtAccCode.Text, txtAccName.Text, StrNull2Dbl(txtDeductAmount.Text), txtDeductRef.Text, StrNull2Zero(lblDUID.Text))
        AddPayment2Grid(lblBillNumber.Text, txtBillDate.Text, ddlCompany.SelectedValue, ddlCustomer.SelectedValue, "", "", "", ddlAccount.SelectedValue, ddlAccount.SelectedItem.Text, 0, 0, StrNull2Double(txtDeductAmount.Text), 0, StrNull2Double(txtDeductAmount.Text), "D", txtDeductRef.Text, StrNull2Zero(hdRUID.Value), StrNull2Zero(ddlAccount.SelectedValue))

        BaseClass.ClearData(pnDeduct)

        CalPrice()
        hdDUID.Value = "0"
    End Sub

    Private Sub ddlCustomer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustomer.SelectedIndexChanged
        LoadCarToDDL()
    End Sub

    Private Sub ddlCar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCar.SelectedIndexChanged

    End Sub

    Private Sub ddlCane_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCane.SelectedIndexChanged
        LoadUnitPrice()
    End Sub


    'Private Sub optPayType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles optPayType.SelectedIndexChanged
    '    If optPayType.SelectedValue = "T" Then
    '        ddlBank.Enabled = True
    '    Else
    '        ddlBank.Enabled = False
    '    End If
    'End Sub
End Class