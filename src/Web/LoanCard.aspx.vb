Imports System.Drawing
Imports System.IO
Imports System.Net
Imports System.Net.Mail
Imports DevExpress.XtraPrinting.Export.Pdf
Imports System.Security.Cryptography
Imports DevExpress.XtraRichEdit.Layout
Imports Org.BouncyCastle.Asn1.Ocsp
Imports Org.BouncyCastle.Ocsp

Public Class LoanCard
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlC As New CardController
    Dim ctlU As New UserController
    Dim ctlM As New MasterController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Request.Cookies("KDP")) Then
            Response.Redirect("Default.aspx")
        End If

        If Not IsPostBack Then
            cmdSave.Visible = True

            txtSubmitDate.Text = Today.Date.ToString("dd/MM/yyyy")
            txtEndDate.Text = "31/12/" & Today.Year() + 543
            txtDay.Text = DateDiff(DateInterval.Day, ConvertStringDateToDate(txtSubmitDate.Text), ConvertStringDateToDate(txtEndDate.Text))

            txtRate.Text = ctlM.SystemConfig_GetValue("INTRATE")
            DisableControl()
            hdCardUID.Value = Request("cid")
            LoadCardData(Request("cid"))

            'If Not Request("id") Is Nothing Then
            '    LoadCardDetailData(Request("id"))
            '    cmdSave.Visible = False
            'End If
        End If
        'cmdDelete.Attributes.Add("onClick", "javascript:return confirm(""ต้องการลบข้อมูลนี้ใช่หรือไม่?"");")

        txtAmount.Attributes.Add("OnKeyPress", "return AllowOnlyDouble();")
        txtRate.Attributes.Add("OnKeyPress", "return AllowOnlyDouble();")
    End Sub

    Private Sub DisableControl()
        txtAccNo.ReadOnly = True
        txtCustomerName.ReadOnly = True
        txtNickName.ReadOnly = True
        txtCardID.ReadOnly = True
        txtTel.ReadOnly = True
        txtAddress.ReadOnly = True

        txtAccNo.BackColor = Color.White
        txtCustomerName.BackColor = Color.White
        txtNickName.BackColor = Color.White
        txtCardID.BackColor = Color.White
        txtTel.BackColor = Color.White
        txtAddress.BackColor = Color.White
    End Sub

    Protected Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Report", "window.open('ReportViewer.aspx?rpt=card&code=" & txtAccNo.Text & "&id=" & hdCardUID.Value & "&RPTTYPE=PDF','_blank');", True)
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        If StrNull2Double(txtCreditBalance.Text) <= 0 Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'ผลการตรวจสอบ','ไม่มียอดค้างชำระ ไม่สามารถทำการยกยอดได้');", True)
            Exit Sub
        End If
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalTransfer(this,'ยกยอดบัญชี','คุณต้องการยกยอดบัญชีนี้ใช่หรือไม่?');", True)
    End Sub

    Private Sub LoadCardData(CardUID As Integer)
        Dim dtC As New DataTable
        dtC = ctlC.Card_GetByUID(CardUID)
        If dtC.Rows.Count > 0 Then
            hdCardUID.Value = CardUID
            hdCustomerUID.Value = String.Concat(dtC.Rows(0)("CustomerUID"))
            txtAccNo.Text = String.Concat(dtC.Rows(0)("AccNo"))
            txtCustomerName.Text = String.Concat(dtC.Rows(0)("CustomerName"))
            txtNickName.Text = String.Concat(dtC.Rows(0)("NickName"))
            txtCardID.Text = String.Concat(dtC.Rows(0)("CardID"))
            txtTel.Text = String.Concat(dtC.Rows(0)("Tel"))
            txtAddress.Text = String.Concat(dtC.Rows(0)("CustomerAddress"))
            lblStatus.Text = String.Concat(dtC.Rows(0)("StatusName"))

            txtCredit.Text = DBNull2Dbl(dtC.Rows(0)("LoanAmount")).ToString("#,##0.##")
            txtCreditBalance.Text = DBNull2Dbl(dtC.Rows(0)("DebtBalance")).ToString("#,##0.##")
            txtAmount.Text = DBNull2Dbl(dtC.Rows(0)("DebtBalance")).ToString("#,##0.##")
            CalculateLoan()

            If String.Concat(dtC.Rows(0)("CardStatus")) = "A" Then
                cmdClose.Visible = True
                cmdSave.Visible = True
            Else
                cmdClose.Visible = False
                cmdSave.Visible = False
            End If
        End If
    End Sub
    'Private Sub LoadCardDetailData(UID As Integer)
    '    Dim dtC As New DataTable
    '    dtC = ctlC.CardDetail_GetByUID(UID)
    '    If dtC.Rows.Count > 0 Then
    '        hdCardDetailUID.Value = String.Concat(dtC.Rows(0)("UID"))

    '        txtSubmitDate.Text = String.Concat(dtC.Rows(0)("StartDate"))
    '        txtEndDate.Text = String.Concat(dtC.Rows(0)("EndDate"))
    '        txtDay.Text = String.Concat(dtC.Rows(0)("DayCount"))

    '        txtAmount.Text = DBNull2Dbl(dtC.Rows(0)("Loan_Amount")).ToString("#,##0.##")
    '        txtRate.Text = DBNull2Dbl(dtC.Rows(0)("Int_Rate")).ToString("#.#0")
    '        txtInterest.Text = DBNull2Dbl(dtC.Rows(0)("Int_Total")).ToString("#,##0.##")
    '        txtTotal.Text = DBNull2Dbl(dtC.Rows(0)("Loan_Total")).ToString("#,##0.##")

    '        txtRemark.Text = String.Concat(dtC.Rows(0)("Remark"))

    '    End If
    'End Sub


    Private Sub cmdCalDay_Click(sender As Object, e As EventArgs) Handles cmdCalDay.Click
        If txtSubmitDate.Text <> "" And txtEndDate.Text <> "" Then
            txtDay.Text = DateDiff(DateInterval.Day, ConvertStringDateToDate(txtSubmitDate.Text), ConvertStringDateToDate(txtEndDate.Text))
        Else
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุวันที่');", True)
            Exit Sub
        End If
    End Sub

    Private Sub cmdCalculate_Click(sender As Object, e As EventArgs) Handles cmdCalculate.Click
        CalculateLoan()
    End Sub
    Private Sub CalculateLoan()

        If txtSubmitDate.Text <> "" And txtEndDate.Text <> "" Then
            txtDay.Text = DateDiff(DateInterval.Day, ConvertStringDateToDate(txtSubmitDate.Text), ConvertStringDateToDate(txtEndDate.Text))
        Else
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุวันที่');", True)
            Exit Sub
        End If

        Dim Amount, Rate, TotalInst, TotalLoan As Double ', RateMonth, RateYear, RateDay
        Dim iDay As Integer
        iDay = StrNull2Zero(txtDay.Text)
        Amount = StrNull2Double(txtAmount.Text)
        Rate = StrNull2Double(txtRate.Text)

        'ดอกเบี้ยต่อเดือน = เงินต้น 150000 x 2% = 3,000
        'หาดอกเบี้ยรายปี = 3000 x 12 = 36,000
        'หาดอกรายวัน = 36,000/365 = 98.63
        'หาดอกเบี้ยที่ต้องจ่าย = 98.63 x 276 วัน = 27,221

        'RateMonth = (Amount * Rate) / 100
        'RateYear = RateMonth * 12
        'RateDay = RateYear / 365
        'TotalInst = RateDay * iDay

        TotalInst = Math.Round(((((Amount * Rate) / 100) * 12) / 365) * iDay, 0)
        TotalLoan = Amount + TotalInst

        txtInterest.Text = TotalInst.ToString("#,##0.##")
        txtTotal.Text = TotalLoan.ToString("#,##0.##")

    End Sub

    'Private Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
    '    hdCardDetailUID.Value = ""

    '    txtSubmitDate.Text = ""
    '    txtEndDate.Text = ""
    '    txtDay.Text = ""
    '    txtAmount.Text = ""
    '    txtRate.Text = ""
    '    txtInterest.Text = ""
    '    txtTotal.Text = ""
    '    txtRemark.Text = ""
    '    cmdSave.Visible = True
    '    cmdDelete.Visible = False
    '    cmdPrint.Visible = False

    'End Sub

    Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
        If StrNull2Double(txtCreditBalance.Text) > 0 Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','ยังมียอดค้างชำระ ไม่สามารถปิดบัญชีได้ ต้องทำการชำระยอดที่เหลือค้างทั้งหมดก่อน หรือให้ทำการบันทึกยกยอดแทน');", True)
            Exit Sub
        End If
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalCancel(this,'ปิดบัญชี','คุณต้องการปิดบัญชีนี้ใช่หรือไม่?');", True)
    End Sub

    Private Sub cmdConfirmCancel_Click(sender As Object, e As EventArgs) Handles cmdConfirmCancel.Click
        CloseAccount()
        cmdSave.Visible = False
        cmdClose.Visible = False
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกปิดบัญชีนี้เรียบร้อย');", True)
    End Sub
    Private Sub CloseAccount()
        ctlC.Card_Close(txtAccNo.Text, ConvertStrDate2DBDate(Now.Date()), "X", Request.Cookies("UserID").Value)
    End Sub
    Private Sub cmdConfirmTransfer_Click(sender As Object, e As EventArgs) Handles cmdConfirmTransfer.Click

        ctlC.Card_Close(txtAccNo.Text, ConvertStrDate2DBDate(Now.Date()), "T", Request.Cookies("UserID").Value)
        TransferAccount()
        cmdSave.Visible = False
        cmdClose.Visible = False
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกยกยอดบัญชีนี้เรียบร้อย');", True)
    End Sub
    Private Sub TransferAccount()
        If txtSubmitDate.Text = "" Or txtEndDate.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุวันที่');", True)
            Exit Sub
        End If

        If StrNull2Zero(txtAmount.Text) <= 0 Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุจำนวนเงินกู้');", True)
            Exit Sub
        End If
        If StrNull2Zero(txtRate.Text) < 0 Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุอัตราดอกเบี้ยให้ถูกต้อง');", True)
            Exit Sub
        End If

        CalculateLoan()

        Dim CardDate, EndDate, BillDate, SubbookNo As String
        Dim Card_Old, Card_New As String
        CardDate = ConvertStrDate2DBString(txtSubmitDate.Text)
        EndDate = ConvertStrDate2DBString(txtEndDate.Text)
        BillDate = ConvertStrDate2DBString(txtSubmitDate.Text)

        Card_Old = txtAccNo.Text
        Card_New = ctlM.RunningNumber2_New("ACCNO")
        SubbookNo = ctlM.RunningNumber2_New("S")

        Dim AgreementDate, AgreementEnd, AgreementNo As String
        AgreementNo = ctlM.RunningNumber2_New("A")
        AgreementDate = ConvertStrDate2DBDate(txtSubmitDate.Text)
        AgreementEnd = ConvertStrDate2DBDate(txtEndDate.Text)

        Dim ctlA As New AgreementController

        ctlA.Agreement_Clone(Card_Old, Card_New, AgreementNo, AgreementDate, AgreementEnd, Request.Cookies("UserID").Value)

        Dim Desc As String = "ยกยอดบัญชี " & txtCustomerName.Text & " เลขที่บัญชี " & Card_Old & " ไป " & Card_New & " จำนวนเงิน " & StrNull2Double(txtAmount.Text).ToString("#,###.#0") & " บาท"

        ctlC.Card_Save(Card_New, AgreementNo, StrNull2Zero(hdCustomerUID.Value), StrNull2Double(txtAmount.Text), StrNull2Double(txtInterest.Text), StrNull2Double(txtAmount.Text) + StrNull2Double(txtInterest.Text), Request.Cookies("UserID").Value)

        ctlC.Card_Forward(Card_Old, Card_New) 'เก็บ RefNo

        '"ดอกเบี้ย " & txtDay.Text & " วัน จำนวนเงิน " & txtInterest.Text & " บาท


        ctlC.Payin_Add(SubbookNo, Card_Old, "2", "F", CardDate, StrNull2Double(txtAmount.Text), "บันทึกปิดบัญชี/ยกยอดไป", "", "", Desc, txtRemark.Text, Request.Cookies("UserID").Value)

        ctlC.CardDetail_Add(SubbookNo, Card_New, "2", "F", CardDate, EndDate, StrNull2Zero(txtDay.Text), StrNull2Double(txtAmount.Text), StrNull2Double(txtRate.Text), StrNull2Double(txtInterest.Text), StrNull2Double(txtTotal.Text), "ยกยอดมา", "", BillDate, "", Desc, txtRemark.Text, Request.Cookies("UserID").Value)


        ctlM.RunningNumber_Update("S")
        ctlU.User_GenLogfile(Request.Cookies("Username").Value, "ADD", "CardDetail", "ยกยอด:" & SubbookNo, "AccNo=" & Card_Old & " ไป " & Card_New & "|Amount=" & txtAmount.Text & "|Ints=" & txtRate.Text & "|Day=" & txtDay.Text)

        ctlM.RunningNumber_Update("A")
        ctlU.User_GenLogfile(Request.Cookies("Username").Value, "ADD", "Agreement", "ทำสัญญาเงินกู้ใหม่:" & AgreementNo, "CustUID=" & hdCustomerUID.Value & ":" & txtCustomerName.Text & "|Amount=" & txtAmount.Text & "|Ints=" & txtRate.Text)

        LoadCardData(hdCardUID.Value)

        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกยกยอดเรียบร้อย');", True)


        cmdSave.Visible = False
        cmdClose.Visible = False
        cmdPrint.Visible = True

        txtCreditBalance.Text = "0"
        txtSubmitDate.Text = ""
        txtEndDate.Text = ""
        txtDay.Text = ""
        txtAmount.Text = ""
        txtRate.Text = ""
        txtInterest.Text = ""
        txtTotal.Text = ""
        txtRemark.Text = ""
        cmdCalculate.Enabled = False

    End Sub

End Class