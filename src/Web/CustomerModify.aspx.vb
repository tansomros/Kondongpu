Imports System.IO

Public Class CustomerModify
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim ctlM As New MasterController
    Dim ctlCus As New CustomerController
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
            txtCustomerID.Text = "Auto ID"
            'UploadDirectory = Server.MapPath("~/imageUploads/" + +"/")
            pnCar.Visible = False
            pnDocument.Visible = False
            LoadBankToDDL()
            LoadPrefix()
            LoadProvinceToDDL()
            LoadDocumentCategory()

            If Not Request("cid") = Nothing Then
                '    If (Not Request("t") = Nothing) And (Request("t") <> "new") Then
                '        EditCustomerData(Request.Cookies("LoginCustomerUID").Value)
                '    End If
                'Else
                EditCustomerData(DBNull2Zero(Request("cid")))

                If StrNull2Zero(hdCustomerUID.Value) <> 0 Then
                    pnDocument.Visible = True
                    pnCar.Visible = True
                End If
            Else 'Add New
                If Request("p") = "reg" Then

                End If
            End If
        End If
        txtCardID.Attributes.Add("OnKeyPress", "return AllowOnlyIntegers();")
        'txtArea1.Attributes.Add("OnKeyPress", "return AllowOnlyDouble();")
        'txtArea2.Attributes.Add("OnKeyPress", "return AllowOnlyDouble();")
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
    Private Sub LoadBankToDDL()
        dt = ctlM.Bank_Get
        If dt.Rows.Count > 0 Then
            With ddlBank
                .Enabled = True
                .DataSource = dt
                .DataTextField = "BankName"
                .DataValueField = "BankID"
                .DataBind()
                .SelectedIndex = 0
            End With
        End If
        dt = Nothing
    End Sub
    Private Sub LoadPrefix()
        dt = ctlM.Prefix_Get
        If dt.Rows.Count > 0 Then
            With ddlPrefix
                .Enabled = True
                .DataSource = dt
                .DataTextField = "PrefixName"
                .DataValueField = "PrefixID"
                .DataBind()
            End With
        End If
        dt = Nothing
    End Sub
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

    Private Sub EditCustomerData(ByVal CustomerUID As Integer)
        Dim dtL As New DataTable
        dtL = ctlCus.Customer_GetByUID(CustomerUID)
        If dtL.Rows.Count > 0 Then
            With dtL.Rows(0)
                hdCustomerUID.Value = .Item("UID")
                txtCustomerID.Text = .Item("CustomerID")

                ddlPrefix.SelectedValue = .Item("Prefix")
                txtName.Text = .Item("FirstName")
                txtSurname.Text = .Item("LastName")
                txtNickName.Text = String.Concat(.Item("NickName"))

                txtAge.Text = String.Concat(.Item("Age"))
                txtCardID.Text = String.Concat(.Item("CardID"))
                txtFamerCode.Text = String.Concat(.Item("FamerID"))
                optSex.SelectedValue = String.Concat(.Item("Sex"))

                txtAddressNo.Text = String.Concat(.Item("AddressNo"))
                txtMoo.Text = String.Concat(.Item("Moo"))
                txtRoad.Text = String.Concat(.Item("Village"))
                txtSubDistrict.Text = String.Concat(.Item("District"))
                txtDistrict.Text = String.Concat(.Item("City"))
                ddlProvince.SelectedValue = String.Concat(.Item("ProvinceID"))
                txtZipCode.Text = String.Concat(.Item("Zipcode"))
                txtTel.Text = String.Concat(.Item("Tel"))
                txtEmail.Text = String.Concat(.Item("Email"))
                txtLineID.Text = String.Concat(.Item("LineID"))

                Me.txtAccountNo.Text = String.Concat(.Item("AccountNo"))
                txtAccountName.Text = String.Concat(.Item("AccountName"))
                ddlBank.SelectedValue = String.Concat(.Item("BankID"))

                LoadCar()

                cmdDelete.Enabled = True
                cmdAddCar.Enabled = True
                txtCarRegis.Enabled = True


                UploadDirectory = Server.MapPath("~/imageUploads/" + String.Concat(.Item("UID")) + "/Customers/")
                If Not Directory.Exists(UploadDirectory) Then
                    Directory.CreateDirectory(UploadDirectory)
                End If

                txtTel.Text = String.Concat(.Item("Tel"))
                txtEmail.Text = String.Concat(.Item("EMail"))
                txtLineID.Text = String.Concat(.Item("LineID"))

                If String.Concat(.Item("ProjectLoan")) = "Y" Then
                    chkLoan.Checked = True
                Else
                    chkLoan.Checked = False
                End If
                If String.Concat(.Item("ProjectCane")) = "Y" Then
                    chkCane.Checked = True
                Else
                    chkCane.Checked = False
                End If

                If String.Concat(.Item("StatusFlag")) = "A" Then
                    chkStatus.Checked = True
                Else
                    chkStatus.Checked = False
                End If

                LoadCar()
                LoadDocument()

            End With
        End If
        dtL = Nothing
    End Sub

    Function Check_Email(str As String) As Boolean
        Return Regex.IsMatch(str, "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
    End Function
    Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click

        If txtCustomerID.Text.Trim() = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาตรวจสอบเลขที่ ขย.5');", True)
            Exit Sub
        End If
        If optSex.SelectedValue = Nothing Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาเลือกเพศ');", True)
            Exit Sub
        End If

        If StrNull2Zero(hdCustomerUID.Value) = 0 Then
            If ctlCus.Customer_SearchByCardID(txtCardID.Text) Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','เลขบัตรประชาชนนี้มีในระบบแล้ว ไม่สามารถเพิ่มซ้ำได้');", True)
                Exit Sub
            End If
        End If

        If Len(Trim(txtCardID.Text)) < 13 Or Len(Trim(txtCustomerID.Text)) > 13 Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุเลขที่บัตรประชาชน ให้ถูกต้อง');", True)
            Exit Sub
        End If

        If txtName.Text = "" Or txtSurname.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุชื่อลูกค้า');", True)
            Exit Sub
        End If
        If txtAddressNo.Text = "" Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุบ้านเลขที่/เลขที่ตั้ง');", True)
                Exit Sub
            End If
            If txtSubDistrict.Text = "" Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุแขวง/ตำบล');", True)
                Exit Sub
            End If
            If txtDistrict.Text = "" Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุเขต/อำเภอ');", True)
                Exit Sub
            End If
            If txtZipCode.Text = "" Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุรหัสไปรษณีย์');", True)
                Exit Sub
            End If
            If txtTel.Text = "" Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุเบอร์โทร');", True)
                Exit Sub
            End If
        'If txtEmail.Text = "" Then
        '    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุอีเมล');", True)
        '    Exit Sub
        'End If
        'If Check_Email(txtEmail.Text) = False Then
        '    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningAlert(this,'ผลการตรวจสอบ','รูปแบบ E-mail ไม่ถูกต้อง กรุณาตรวจสอบ');", True)
        '    Exit Sub
        'End If

        'If txtCustomerID.Text = "" Then
        '        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุเลขที่ใบอนุญาต ขย.5');", True)
        '        Exit Sub
        '    End If
        'If txtLicensee.Text = "" Then
        '    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณาระบุชื่อผู้ได้รับอนุญาต');", True)
        '    Exit Sub
        'End If


        Dim IsLoan, IsCane As String

        If chkLoan.Checked Then
            IsLoan = "Y"
        Else
            IsLoan = "N"
        End If
        If chkCane.Checked Then
            IsCane = "Y"
        Else
            IsCane = "N"
        End If

        Dim StatusFlag As String = "A"
        If chkStatus.Checked Then
            StatusFlag = "A"
        Else
            StatusFlag = "D"
        End If

        If hdCustomerUID.Value = "" Then
            txtCustomerID.Text = ctlCus.RunningNumber_New("C")
        End If

        ctlCus.Customer_Save(txtCustomerID.Text, ddlPrefix.SelectedValue, txtName.Text, txtSurname.Text, txtNickName.Text, StrNull2Zero(txtAge.Text), optSex.SelectedValue, txtCardID.Text, txtTel.Text, txtAddressNo.Text, txtMoo.Text, txtRoad.Text, txtSubDistrict.Text, txtDistrict.Text, ddlProvince.SelectedValue, ddlProvince.SelectedItem.Text, txtZipCode.Text, txtEmail.Text, txtLineID.Text, txtAccountNo.Text, txtAccountName.Text, ddlBank.SelectedValue, IsLoan, IsCane, StatusFlag, Request.Cookies("UserID").Value)

        If hdCustomerUID.Value = "" Then
            ctlCus.RunningNumber_Update("C")
        End If

        'ctlC.CarRegistration_ActiveStatusQ(txtCustomerID.Text, Request.Cookies("UserID").Value)

        pnCar.Visible = True
        pnDocument.Visible = True

        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','บันทึกข้อมูลเรียบร้อย');", True)

    End Sub

    Private Sub PreviewPicture(Fname As String, Desc As String, filePath As String)
        'Dim fPath As String = "imageUploads/Customers/" & hdCustomerUID.Value & "/"
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


    Private Sub LoadCar()
        Dim dtP As New DataTable
        dtP = ctlCus.CarRegistration_Get(txtCustomerID.Text)
        If dtP.Rows.Count > 0 Then
            With grdCar
                .Visible = True
                .DataSource = dtP
                .DataBind()
            End With
        Else
            grdCar.Visible = False
        End If
        dtP = Nothing
    End Sub

    Protected Sub cmdAddCar_Click(sender As Object, e As EventArgs) Handles cmdAddCar.Click
        If txtCarRegis.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณป้อนหมายเลขทะเบียนรถ');", True)
            Exit Sub
        End If

        If ctlCus.CarRegistration_CheckDuplicate(txtCarRegis.Text.Trim()) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','เลขทะเบียนนี้มีในฐานข้อมูลแล้ว');", True)
            Exit Sub
        Else
            ctlCus.CarRegistration_Add(txtCustomerID.Text, txtCarRegis.Text, "Q", Request.Cookies("UserID").Value)
            ctlU.User_GenLogfile(Request.Cookies("Username").Value, ACTTYPE_ADD, "Cars", "เพิ่มทะเบียนรถลูกค้า :" & txtCarRegis.Text, "CusID=" & hdCustomerUID.Value & ",Name=" & txtName.Text & " " & txtSurname.Text)
            LoadCar()
            txtCarRegis.Text = ""
        End If
    End Sub
    Private Sub grdCar_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdCar.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgDel"
                    If ctlCus.CarRegistration_Delete(e.CommandArgument) Then
                        ctlU.User_GenLogfile(Request.Cookies("Username").Value, "DEL", "Cars", "ลบหมายเลขทะเบียนรถ :" & e.CommandArgument, "")
                        'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','ลบข้อมูลเรียบร้อย');", True)
                    Else
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบและลองใหม่อีกครั้ง');", True)
                    End If
                    LoadCar()
            End Select
        End If
    End Sub

    Private Sub grdCar_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdCar.RowDataBound
        If e.Row.RowType = ListItemType.AlternatingItem Or e.Row.RowType = ListItemType.Item Then
            'e.Row.Cells(0).Text = e.Row.RowIndex + 1
            Dim scriptString As String = "javascript:return confirm(""ต้องการลบ ข้อมูลนี้ ?"");"
            Dim imgD As ImageButton = e.Row.Cells(2).FindControl("imgDel")
            imgD.Attributes.Add("onClick", scriptString)

        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#d0e8ff';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

        End If

    End Sub
    Private Sub LoadDocumentCategory()
        ddlDocument.DataSource = ctlD.Document_GetCategory
        ddlDocument.DataTextField = "Descriptions"
        ddlDocument.DataValueField = "ValueCode"
        ddlDocument.DataBind()
    End Sub

    Private Sub LoadDocument()
        dt = ctlD.Document_Get(StrNull2Zero(hdCustomerUID.Value))
        grdDocument.DataSource = dt
        grdDocument.DataBind()
    End Sub
    Protected Sub cmdAddFile_Click(sender As Object, e As EventArgs) Handles cmdAddDoc.Click
        If txtDocDesc.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalWarningInfo(this,'ผลการตรวจสอบ','กรุณป้อนหมายเลขทะเบียนรถ/ฉโนด/เลขที่อ้างอิงก่อน');", True)
            Exit Sub
        End If

        ctlD.Document_Save(StrNull2Long(ddlDocument.SelectedValue), StrNull2Zero(hdCustomerUID.Value), txtDocDesc.Text, StrNull2Zero(Request.Cookies("UserID").Value))
        LoadDocument()
    End Sub
    Private Sub grdDocument_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdDocument.RowCommand
        If TypeOf e.CommandSource Is WebControls.ImageButton Then
            Dim ButtonPressed As WebControls.ImageButton = e.CommandSource
            Select Case ButtonPressed.ID
                Case "imgDel"
                    'DeleteDocumentFile(e.CommandArgument)
                    ctlD.Document_Delete(e.CommandArgument)
                    LoadDocument()
            End Select
        End If
    End Sub


    Private Sub grdDocument_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdDocument.RowDataBound
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
    Private Sub DeleteDocumentFile(DocUID As Integer)
        Dim ctlM As New DocumentController
        dt = ctlM.Document_GetByID(DocUID)
        If dt.Rows.Count > 0 Then
            Dim objfile As FileInfo = New FileInfo(Server.MapPath("~/Documents/Customers/" & hdCustomerUID.Value & "/" & dt.Rows(0)("FilePath")))
            If objfile.Exists Then
                objfile.Delete()
            End If
        End If
    End Sub
    Private Sub ClearData()
        isAdd = True
        ctlCus.CarRegistration_DeleteStatusQ(txtCustomerID.Text, Request.Cookies("UserID").Value)

        hdCustomerUID.Value = ""
        txtCustomerID.Text = "Auto ID"
        txtName.Text = ""
        txtSurname.Text = ""
        txtTel.Text = ""
        txtEmail.Text = ""
        txtLineID.Text = ""
        txtAge.Text = ""
        txtCardID.Text = ""
        txtFamerCode.Text = ""
        txtAccountName.Text = ""
        txtAccountNo.Text = ""
        ddlPrefix.SelectedIndex = 0
        ddlProvince.SelectedValue = 21

        txtCarRegis.Enabled = False
        cmdAddCar.Enabled = False
        cmdDelete.Enabled = False
    End Sub

    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        ClearData()
    End Sub

    Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        ctlCus.Customer_Delete(txtCustomerID.Text)
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MessageAlert", "openModalSuccess(this,'Success','ลบข้อมูลเรียบร้อย');", True)
        ClearData()
    End Sub
End Class