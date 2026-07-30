Imports System.IO

Public Class CompanyModify
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlM As New MasterController
    Dim ctlC As New CompanyController
    Private UploadDirectory As String

    Dim ctlMd As New MediaController
    Dim ctlU As New UserController
    Dim ctlD As New DocumentController

    Public Shared json As String
    Public Shared lat As Double
    Public Shared lng As Double

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Request.Cookies("KDP")) Then
            Response.Redirect("Default.aspx")
        End If
        If Not IsPostBack Then
            chkStatus.Checked = True
            LoadProvinceToDDL()
            If Not Request("cid") = Nothing Then
                '    If (Not Request("t") = Nothing) And (Request("t") <> "new") Then
                '        EditCompanyData(Request.Cookies("LoginUID").Value)
                '    End If
                'Else
                EditCompanyData(DBNull2Zero(Request("cid")))
            Else 'Add New
                If Request("p") = "reg" Then

                End If
            End If
        End If
        txtTaxId.Attributes.Add("OnKeyPress", "return AllowOnlyIntegers();")
        txtZipCode.Attributes.Add("OnKeyPress", "return AllowOnlyIntegers();")
        txtEmail.Attributes.Add("OnKeyPress", "return NotAllowThai();")
        cmdDelete.Attributes.Add("onClick", "javascript:return confirm(""ต้องการลบข้อมูลลูกค้ารายนี้ใช่หรือไม่?"");")

    End Sub

    Function checkField(tD As DataTable, ColumnName As String) As String
        If dt.Columns(ColumnName) IsNot Nothing Then
            Return String.Concat(dt.Rows(0)(ColumnName))
        Else
            Return ""
        End If
    End Function
    Private Sub LoadProvinceToDDL()
        dt = ctlM.Province_Get
        If dt.Rows.Count > 0 Then
            With ddlProvince
                .Enabled = True
                .DataSource = dt
                .DataTextField = "ProvinceName"
                .DataValueField = "ProvinceID"
                .DataBind()
                '.SelectedIndex = 0
            End With
        End If
        dt = Nothing
    End Sub

    Private Sub EditCompanyData(ByVal CompanyUID As Integer)
        Dim dtL As New DataTable
        dtL = ctlC.Company_GetByUID(CompanyUID)
        If dtL.Rows.Count > 0 Then
            With dtL.Rows(0)
                hdCompanyUID.Value = .Item("UID")
                txtCode.Text = .Item("Code")

                txtCompanyName.Text = String.Concat(.Item("Name"))
                txtAliasName.Text = String.Concat(.Item("AliasName"))
                txtOwnerName.Text = String.Concat(.Item("OwnerName"))
                txtTaxId.Text = String.Concat(.Item("VATID"))

                txtAddressNo.Text = String.Concat(.Item("AddressNo"))
                txtMoo.Text = String.Concat(.Item("Moo"))
                txtRoad.Text = String.Concat(.Item("Village"))
                txtSubDistrict.Text = String.Concat(.Item("District"))
                txtDistrict.Text = String.Concat(.Item("City"))
                ddlProvince.SelectedValue = String.Concat(.Item("ProvinceID"))
                txtZipCode.Text = String.Concat(.Item("Zipcode"))

                txtTel.Text = String.Concat(.Item("Telephone"))
                txtFax.Text = String.Concat(.Item("Fax"))
                txtEmail.Text = String.Concat(.Item("Email"))
                txtWebsite.Text = String.Concat(.Item("Website"))

                cmdDelete.Enabled = True

                UploadDirectory = Server.MapPath("~/imageUploads/" + String.Concat(.Item("UID")) + "/Companys/")
                If Not Directory.Exists(UploadDirectory) Then
                    Directory.CreateDirectory(UploadDirectory)
                End If

                If String.Concat(.Item("StatusFlag")) = "A" Then
                    chkStatus.Checked = True
                Else
                    chkStatus.Checked = False
                End If

            End With
        End If
        dtL = Nothing
    End Sub

    Function Check_Email(str As String) As Boolean
        Return Regex.IsMatch(str, "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
    End Function
    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click

        If txtCode.Text.Trim() = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาตรวจสอบ Code');", True)
            Exit Sub
        End If
        If ddlProvince.SelectedValue.ToString = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุ จังหวัด ก่อน');", True)
            Exit Sub
        End If
        If txtCompanyName.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุ ชื่อโรงงาน ก่อน');", True)
            Exit Sub
        End If

        If StrNull2Zero(hdCompanyUID.Value) = 0 Then
            If ctlC.Company_CheckDupicateByCode(txtCode.Text) Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','รหัสโรงงานนี้มีในระบบแล้ว ไม่สามารถเพิ่มซ้ำได้');", True)
                Exit Sub
            End If
        End If

        'If txtAddressNo.Text = "" Then
        '    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุบ้านเลขที่/เลขที่ตั้ง');", True)
        '    Exit Sub
        'End If
        'If txtSubDistrict.Text = "" Then
        '    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุแขวง/ตำบล');", True)
        '    Exit Sub
        'End If
        'If txtDistrict.Text = "" Then
        '    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุเขต/อำเภอ');", True)
        '    Exit Sub
        'End If
        'If txtZipCode.Text = "" Then
        '    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุรหัสไปรษณีย์');", True)
        '    Exit Sub
        'End If
        'If txtTel.Text = "" Then
        '    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุเบอร์โทร');", True)
        '    Exit Sub
        'End If
        'If txtEmail.Text = "" Then
        '    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุอีเมล');", True)
        '    Exit Sub
        'End If
        'If Check_Email(txtEmail.Text) = False Then
        '    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'ผลการตรวจสอบ','รูปแบบ E-mail ไม่ถูกต้อง กรุณาตรวจสอบ');", True)
        '    Exit Sub
        'End If

        'If txtCode.Text = "" Then
        '        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุเลขที่ใบอนุญาต ขย.5');", True)
        '        Exit Sub
        '    End If
        'If txtLicensee.Text = "" Then
        '    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุชื่อผู้ได้รับอนุญาต');", True)
        '    Exit Sub
        'End If

        Dim StatusFlag As String = "A"
        If chkStatus.Checked Then
            StatusFlag = "A"
        Else
            StatusFlag = "D"
        End If

        If hdCompanyUID.Value = "" Then
            txtCode.Text = ctlC.RunningNumber_New("C")
        End If

        ctlC.Company_Save(hdCompanyUID.Value, txtCode.Text, txtCompanyName.Text, txtAliasName.Text, txtTaxId.Text, txtAddressNo.Text, txtMoo.Text, txtRoad.Text, txtSubDistrict.Text, txtDistrict.Text, ddlProvince.SelectedValue, ddlProvince.SelectedItem.Text, txtZipCode.Text, "ไทย", txtTel.Text, txtFax.Text, txtEmail.Text, txtWebsite.Text, StatusFlag, Request.Cookies("UserID").Value, txtOwnerName.Text)

        If hdCompanyUID.Value = "" Then
            ctlC.RunningNumber_Update("C")
        End If

        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกข้อมูลเรียบร้อย');", True)

    End Sub

    Private Sub PreviewPicture(Fname As String, Desc As String, filePath As String)
        'Dim fPath As String = "imageUploads/Companys/" & hdCompanyUID.Value & "/"
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWindow(this,'" + Fname + "','" + Desc + "','" + filePath + "');", True)
    End Sub
    'Protected Sub UploadControl_FileUploadComplete(ByVal sender As Object, ByVal e As FileUploadCompleteEventArgs)

    '    If chkFileExist(Server.MapPath(UploadDirectory + Session("picname"))) Then
    '        File.Delete(Server.MapPath(UploadDirectory + Session("picname")))
    '    End If

    '    e.CallbackData = SavePostedFile(e.UploadedFile, UploadDirectory, Path.GetRandomFileName())
    '    Session("picname") = e.CallbackData
    'End Sub
    'Protected Function SavePostedFile(ByVal uploadedFile As UploadedFile, ByVal UploadPath As String, ByVal UploadFileName As String) As String
    '    If (Not uploadedFile.IsValid) Then
    '        Return String.Empty
    '    End If

    '    Dim fileName As String = Path.ChangeExtension(UploadFileName, ".jpg")

    '    Dim fullFileName As String = CombinePath(UploadPath, fileName)
    '    Using original As Image = Image.FromStream(uploadedFile.FileContent)
    '        Using thumbnail As Image = New ImageThumbnailCreator(original).CreateImageThumbnail(New Size(350, 350))
    '            ImageUtils.SaveToJpeg(CType(thumbnail, Bitmap), fullFileName)
    '        End Using
    '    End Using
    '    UploadingUtils.RemoveFileWithDelay(fileName, fullFileName, 5)
    '    Return fileName
    'End Function
    'Protected Function CombinePath(ByVal UploadPath As String, ByVal fileName As String) As String
    '    Return Path.Combine(Server.MapPath(UploadPath), fileName)
    'End Function

    Private Sub ClearData()
        isAdd = True
        hdCompanyUID.Value = ""
        txtCode.Text = ""

        txtCompanyName.Text = ""
        txtAliasName.Text = ""
        txtOwnerName.Text = ""
        txtTaxId.Text = ""
        txtAddressNo.Text = ""
        txtMoo.Text = ""
        txtRoad.Text = ""
        txtDistrict.Text = ""
        txtSubDistrict.Text = ""
        ddlProvince.SelectedValue = 21
        txtZipCode.Text = ""
        txtTel.Text = ""
        txtFax.Text = ""
        txtEmail.Text = ""
        txtWebsite.Text = ""

        cmdDelete.Enabled = False
    End Sub

    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        ClearData()
    End Sub

    Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        ctlC.Company_Delete(txtCode.Text)
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','ลบข้อมูลเรียบร้อย');", True)
        ClearData()
    End Sub
End Class