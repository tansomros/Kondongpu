Imports System.Drawing
Imports System.IO
Imports System.Net
Imports System.Net.Mail
Imports DevExpress.XtraPrinting.Export.Pdf
Imports System.Security.Cryptography
Imports DevExpress.XtraRichEdit.Layout
Imports Org.BouncyCastle.Asn1.Ocsp
Imports Org.BouncyCastle.Ocsp

Public Class LoanNew
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlC As New CardController
    Dim ctlU As New UserController
    Dim ctlM As New MasterController
    Private UploadDirectory As String

    Public dtLoan As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Request.Cookies("KDP")) Then
            Response.Redirect("Default.aspx")
        End If

        If Not IsPostBack Then
            cmdSave.Visible = True
            cmdDelete.Visible = False
            cmdPrint.Visible = False
            hdCardDetailUID.Value = "0"
            txtSubbookNo.Text = "Auto ID"
            txtSubbookNo.ReadOnly = True

            txtSubmitDate.Text = Today.Date.ToString("dd/MM/yyyy")
            txtEndDate.Text = "31/12/" & Today.Year() + 543
            txtDay.Text = DateDiff(DateInterval.Day, ConvertStringDateToDate(txtSubmitDate.Text), ConvertStringDateToDate(txtEndDate.Text))

            txtRate.Text = ctlM.SystemConfig_GetValue("INTRATE")
            DisableControl()
            hdCardUID.Value = Request("cid")
            LoadBankAccountToDDL()
            LoadCardData(Request("cid"))
            LoadCardDetailToTabel()
            If Not Request("id") Is Nothing Then
                LoadCardDetailData(Request("id"))
                cmdSave.Visible = False
                cmdDelete.Visible = True
                cmdPrint.Visible = True
            End If
        End If
        'cmdDelete.Attributes.Add("onClick", "javascript:return confirm(""ต้องการลบข้อมูลนี้ใช่หรือไม่?"");")

        txtAmount.Attributes.Add("OnKeyPress", "return AllowOnlyDouble();")
        txtRate.Attributes.Add("OnKeyPress", "return AllowOnlyDouble();")
    End Sub

    Private Sub LoadBankAccountToDDL()
        Dim ctlbase As New MasterController
        dt = ctlM.BankAccount_Get
        If dt.Rows.Count > 0 Then
            With ddlBank
                .Enabled = True
                .DataSource = dt
                .DataTextField = "BankAccount"
                .DataValueField = "AccountNo"
                .DataBind()
                .SelectedIndex = 0
            End With
        End If
        dt = Nothing
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

    Private Sub LoadCardDetailToTabel()
        dtLoan = ctlC.CardDetail_GetByCardUID(StrNull2Zero(hdCardUID.Value))
    End Sub

    Protected Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Report", "window.open('ReportViewer.aspx?rpt=pay&id=" & hdCardDetailUID.Value & "&code=" & txtSubbookNo.Text & "&RPTTYPE=PDF','_blank');", True)
    End Sub

    Protected Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalCancel(this,'ยกเลิกรายการ','คุณต้องการยกเลิกรายการนี้ใช่หรือไม่?');", True)
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click

        If StrNull2Zero(txtCreditBalance.Text) <= 0 Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','ยอดเครดิตคงเหลือไม่เพียงพอ ไม่สามารถกู้ได้ ท่านต้องทำการเพิ่มวงเงินหน้าสัญญาก่อน');", True)
            Exit Sub
        End If

        If txtSubmitDate.Text = "" Or txtEndDate.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุวันที่กู้');", True)
            Exit Sub
        End If

        If StrNull2Zero(txtAmount.Text) <= 0 Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุจำนวนวงเงินกู้');", True)
            Exit Sub
        End If
        If StrNull2Zero(txtAmount.Text) > StrNull2Zero(txtCreditBalance.Text) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','ท่านกู้เงินเกินจำนวนวงเงินกู้');", True)
            Exit Sub
        End If
        If StrNull2Zero(txtRate.Text) < 0 Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุอัตราดอกเบี้ยให้ถูกต้อง');", True)
            Exit Sub
        End If

        CalculateLoan()

        Dim CardDate, EndDate, BillDate As String
        CardDate = ConvertStrDate2DBString(txtSubmitDate.Text)
        EndDate = ConvertStrDate2DBString(txtEndDate.Text)
        BillDate = ConvertStrDate2DBString(txtSubmitDate.Text)

        Dim BankAccount As String
        If optPayType.SelectedValue = "T" Then
            BankAccount = ddlBank.SelectedValue
        Else
            BankAccount = ""
        End If

        Dim Desc As String = "จ่ายเงินกู้ให้ " & txtCustomerName.Text & " เลขที่บัญชี " & txtAccNo.Text & " จำนวนเงิน " & StrNull2Double(txtAmount.Text).ToString("#,###.#0") & " บาท"

        '"ดอกเบี้ย " & txtDay.Text & " วัน จำนวนเงิน " & txtInterest.Text & " บาท
        Dim SubbookNo As String = ""
        If hdCardDetailUID.Value = "0" Then
            SubbookNo = ctlM.RunningNumber2_New("S")
            txtSubbookNo.Text = SubbookNo
        End If

        ctlC.CardDetail_Add(SubbookNo, txtAccNo.Text, "1", optPayType.SelectedValue, CardDate, EndDate, StrNull2Zero(txtDay.Text), StrNull2Double(txtAmount.Text), StrNull2Double(txtRate.Text), StrNull2Double(txtInterest.Text), StrNull2Double(txtTotal.Text), "บันทึกให้กู้ยืมเงิน", "", BillDate, BankAccount, Desc, txtRemark.Text, Request.Cookies("UserID").Value)

        ctlC.Card_IncreaseBalance(txtAccNo.Text, StrNull2Double(txtAmount.Text), StrNull2Double(txtInterest.Text), Request.Cookies("UserID").Value)

        ctlM.RunningNumber_Update("S")
        ctlU.User_GenLogfile(Request.Cookies("Username").Value, "ADD", "CardDetail", "กู้เงินกู้ใหม่:" & SubbookNo, "AccNo=" & txtAccNo.Text & "|Amount=" & txtAmount.Text & "|Ints=" & txtRate.Text & "|Day=" & txtDay.Text)

        UploadFile(SubbookNo)

        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกเรียบร้อย');", True)

        cmdPrint.Visible = True
        cmdDelete.Visible = True

        'txtDay.Text = ""
        'txtAmount.Text = ""
        'txtInterest.Text = ""
        'txtTotal.Text = ""
        'txtRemark.Text = ""
        'txtSubbookNo.Text = "Auto ID"
        'hdCardDetailUID.Value = "0"

        LoadCardDetailToTabel()
    End Sub
    Private Sub UploadFile(sCode As String)
        Dim sfileName As String = ""
        UploadDirectory = Server.MapPath(SlipPay)

        If Not Directory.Exists(UploadDirectory) Then
            Directory.CreateDirectory(UploadDirectory)
        End If

        If FileUploadA.HasFiles Then
            For Each uploadedFile As HttpPostedFile In FileUploadA.PostedFiles
                Dim fileExtname As String = Path.GetExtension(uploadedFile.FileName).ToLower()

                If fileExtname <> ".jpg" And fileExtname <> ".jpeg" And fileExtname <> ".png" Then
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','ไฟล์รูปภาพต้องมีนามสกุล .jpg, .jpeg, .png เท่านั้น');", True)
                    Exit Sub
                End If
                If (FileUploadA.PostedFile.ContentLength / 1024) > 1024 Then
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','ไฟล์มีขนาดใหญ่เกิน 1024 Kb. ไม่สามารถอัพโหลดได้');", True)
                    Exit Sub
                End If

                sfileName = Replace(sCode, "-", "") & fileExtname

                Dim objfile As FileInfo = New FileInfo(UploadDirectory & sfileName)
                If objfile.Exists Then
                    objfile.Delete()
                End If
                uploadedFile.SaveAs(System.IO.Path.Combine(UploadDirectory, sfileName))
                ctlC.CardDetail_UpdateSlipPath(sCode, sfileName)
                imgSlip.ImageUrl = SlipPay & sfileName & "?v=" & ConvertTimeToString(Now())
            Next
        End If
    End Sub

    Private Sub LoadCardData(CardUID As Integer)
        Dim dtC As New DataTable
        dtC = ctlC.Card_GetByUID(CardUID)
        If dtC.Rows.Count > 0 Then
            hdCardUID.Value = String.Concat(dtC.Rows(0)("UID"))
            hdCustomerUID.Value = String.Concat(dtC.Rows(0)("CustomerUID"))
            txtAccNo.Text = String.Concat(dtC.Rows(0)("AccNo"))
            txtCustomerName.Text = String.Concat(dtC.Rows(0)("CustomerName"))
            txtNickName.Text = String.Concat(dtC.Rows(0)("NickName"))
            txtCardID.Text = String.Concat(dtC.Rows(0)("CardID"))
            txtTel.Text = String.Concat(dtC.Rows(0)("Tel"))
            txtAddress.Text = String.Concat(dtC.Rows(0)("CustomerAddress"))

            txtCredit.Text = DBNull2Dbl(dtC.Rows(0)("CreditAmount")).ToString("#,##0.##")
            txtCreditBalance.Text = DBNull2Dbl(dtC.Rows(0)("CreditBalance")).ToString("#,##0.##")
        End If
    End Sub
    Private Sub LoadCardDetailData(UID As Integer)
        Dim dtC As New DataTable
        dtC = ctlC.CardDetail_GetByUID(UID)
        If dtC.Rows.Count > 0 Then
            hdCardDetailUID.Value = String.Concat(dtC.Rows(0)("UID"))
            txtSubbookNo.Text = String.Concat(dtC.Rows(0)("Code"))
            txtSubmitDate.Text = String.Concat(dtC.Rows(0)("StartDate"))
            txtEndDate.Text = String.Concat(dtC.Rows(0)("EndDate"))
            txtDay.Text = String.Concat(dtC.Rows(0)("DayCount"))

            txtAmount.Text = DBNull2Dbl(dtC.Rows(0)("Loan_Amount")).ToString("#,##0.##")
            txtRate.Text = DBNull2Dbl(dtC.Rows(0)("Int_Rate")).ToString("#.#0")
            txtInterest.Text = DBNull2Dbl(dtC.Rows(0)("Int_Total")).ToString("#,##0.##")
            txtTotal.Text = DBNull2Dbl(dtC.Rows(0)("Loan_Total")).ToString("#,##0.##")

            txtRemark.Text = String.Concat(dtC.Rows(0)("Remark"))
            optPayType.SelectedValue = String.Concat(dtC.Rows(0)("PayType"))
            ddlBank.SelectedValue = String.Concat(dtC.Rows(0)("BankAccount"))

            imgSlip.ImageUrl = SlipPay & String.Concat(dtC.Rows(0)("SlipPath")) & "?v=" & ConvertTimeToString(Now())
            imgSlip.Visible = True
        End If
    End Sub


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
        LoadCardDetailToTabel()
    End Sub

    Private Sub cmdConfirmCancel_Click(sender As Object, e As EventArgs) Handles cmdConfirmCancel.Click
        ctlC.CardDetail_Delete(StrNull2Zero(hdCardDetailUID.Value), txtCancelRemark.Text, Request.Cookies("UserID").Value)
        'ctlPrc.RequestTransaction_Add(StrNull2Zero(hdRequestUID.Value), StrNull2Zero(hdLocationUID.Value), 0, "ยกเลิกคำขอ : " & txtCancelRemark.Text, Request.Cookies("UserID").Value)
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','ลบ/ยกเลิก รายการกู้เรียบร้อย');", True)
        'Response.Redirect("ResultPage.aspx?p=request&t=cancel")
    End Sub

    Private Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        hdCardDetailUID.Value = "0"
        txtSubbookNo.Text = "Auto ID"
        'txtSubmitDate.Text = ""
        'txtEndDate.Text = ""
        'txtDay.Text = ""
        txtAmount.Text = ""
        'txtRate.Text = ""
        txtInterest.Text = ""
        txtTotal.Text = ""
        txtRemark.Text = ""
        ddlBank.SelectedIndex = 0
        imgSlip.ImageUrl = ""
        imgSlip.Visible = False
        cmdSave.Visible = True
        cmdDelete.Visible = False
        cmdPrint.Visible = False
        LoadCardDetailToTabel()
    End Sub

    Private Sub imgSlip_Click(sender As Object, e As ImageClickEventArgs) Handles imgSlip.Click
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWindow(this,'View Slip','','" + imgSlip.ImageUrl + "');", True)
        LoadCardDetailToTabel()
    End Sub

    'Private Sub optPayType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles optPayType.SelectedIndexChanged
    '    If optPayType.SelectedValue = "T" Then
    '        ddlBank.Enabled = True
    '    Else
    '        ddlBank.Enabled = False
    '    End If
    'End Sub
End Class