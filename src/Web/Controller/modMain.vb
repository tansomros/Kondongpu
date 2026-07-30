Imports System.IO
Imports System.Data
Imports System.Windows.Forms
Imports System.Globalization
Imports System.Data.SqlClient
Imports System
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Collections.Generic

Module modMain

#Region "Definitoin Parameter"

    Public f_debug As Boolean
    Public MAX_ENG_YEAR As Integer = 2020
    Public STR_LANGUAGE As String = "th"
    Public SQL As String = ""
    Public SQLwhere As String = ""
    Public has_data As Boolean = False
    Public dtfInfo As DateTimeFormatInfo

    Public pointDup As Boolean = False

    'Public ConnectionString As String = ""

   

    Public dt As DataTable = New DataTable
    Public ds As DataSet = New DataSet

    Public currentRow As DataRow = Nothing

    'Private enumerator As IEnumerator = Nothing

    Public ReportFormula As String = ""
    Public ReportName As String = ""
    Public ReportTitle As String = ""
    Public actionPrint As Boolean = True
    Public viewClose As Boolean = False
    'Public ReportPath As String = "C:\ProgramData\KONDONGPU\Reports\"


    Public Reportskey As String = ""
    Public FagRPT As String = ""
    Public ReportParameter() As String
    Public RPTMODE As String = ""
    Public ReportOneParameter As String = ""


    Public ROLE_MANAGER As Integer = 9
    Public ROLE_OFFICER As Integer = 2
    Public ROLE_ADMIN As Integer = 1


    Public Enum pmfld As Integer 'ประกาศตำแหน่งของฟิลด์
        CustID = 0
        CustName = 1
        CustAge = 2
        Tel = 3
        AddressNo = 4
        AddressMoo = 5
        AddressTown = 6
        AddressDistrict = 7
        AddressAmpher = 8
        ProvinceID = 9
        ZipCode = 10
        AreaCode = 11
        ZoneCode = 12
        GName1 = 13
        GAge1 = 14
        GAddressNo1 = 15
        GMoo1 = 16
        GDistrict1 = 17
        GAmpher1 = 18
        GProvinceName1 = 19
        GAddress1 = 20
        GName2 = 21
        GAge2 = 22
        GAddressNo2 = 23
        GMoo2 = 24
        GDistrict2 = 25
        GAmpher2 = 26
        GProvinceName2 = 27
        GAddress2 = 28
        ProvinceName = 29
        CloseDate = 30
        ACCNO = 31
        Status = 32
        CustAddress = 33
        Garuntee1 = 34
        Garuntee2 = 35
        Garuntee3 = 36
        LoanAmount = 37
        Int_Time = 38
        OBJ = 39
        PermitDate = 40
        DueDate = 41
        Pay_Interest = 42
        GTel1 = 43
        GTel2 = 44
        Remark = 45
        EndPermitDate = 46

    End Enum
#End Region

#Region "Card Type"

    Public Const CARD_TYPE_NEW_VALUE As String = "1"
    Public Const CARD_TYPE_NEW_NAME As String = "บันทึกให้กู้ยืมเงิน"

    Public Const CARD_TYPE_PLUS_VALUE As String = "2"
    Public Const CARD_TYPE_PLUS_NAME As String = "บันทึกให้กู้ยืมเงิน(เพิ่ม)"

    Public Const CARD_TYPE_PAYAMOUNT_VALUE As String = "3"
    Public Const CARD_TYPE_PAYAMOUNT_NAME As String = "รับคืนเงินต้น+ดอกเบี้ย"

    Public Const CARD_TYPE_PAYLOAN_VALUE As String = "4"
    Public Const CARD_TYPE_PAYLOAN_NAME As String = "รับคืนเงินต้น"

    Public Const CARD_TYPE_PAYINT_VALUE As String = "5"
    Public Const CARD_TYPE_PAYINT_NAME As String = "รับดอกเบี้ย"

    Public Const CARD_TYPE_RETURNINT_VALUE As String = "6"
    Public Const CARD_TYPE_RETURNINT_NAME As String = "จ่ายคืนดอกเบี้ย"

    Public Const CARD_TYPE_PCLREINT_VALUE As String = "7"
    Public Const CARD_TYPE_PCLREINT_NAME As String = "รับคืนเงินต้น+จ่ายคืนดอกเบี้ย"

    Public Const CARD_TYPE_TRANSINT_VALUE As String = "8"
    Public Const CARD_TYPE_TRANSINT_NAME As String = "บันทึกดอกเบี้ยโอน"


    Public Const PAY_TYPE_NONE_VALUE As String = ""
    Public Const PAY_TYPE_NONE_NAME As String = ""

    Public Const PAY_TYPE_CASH_VALUE As String = "C"
    Public Const PAY_TYPE_CASH_NAME As String = "เงินสด"

    Public Const PAY_TYPE_TRANS_VALUE As String = "T"
    Public Const PAY_TYPE_TRANS_NAME As String = "โอน"



    Public Const SUBBOOK_TYPE_NEW_ACCID As String = "501-000"
    Public Const SUBBOOK_TYPE_PLUS_ACCID As String = "501-000"
    Public Const SUBBOOK_TYPE_PAY_ACCID As String = "501-000"
    Public Const SUBBOOK_TYPE_INT_ACCID As String = "601-000"
    Public Const SUBBOOK_TYPE_REINT_ACCID As String = "601-000"
    Public Const SUBBOOK_TYPE_TRANS_ACCID As String = "601-000"
    Public Const SUBBOOK_TYPE_RECVTRANS_ACCID As String = "701-000"

    Public Const SUBBOOK_TYPE_NEW_NAME As String = "จ่ายเงินกู้"
    Public Const SUBBOOK_TYPE_PLUS_NAME As String = "จ่ายเงินกู้เพิ่ม"
    Public Const SUBBOOK_TYPE_PAY_NAME As String = "รับเงินต้น"
    Public Const SUBBOOK_TYPE_INT_NAME As String = "รับดอกเบี้ย"
    Public Const SUBBOOK_TYPE_REINT_NAME As String = "คืนดอกเบี้ย"


#End Region


#Region "General Functions"
    'Public Sub BindDataToCombo(ByVal cbo As ComboBox, ByVal dt As DataSet, ByVal displayFld As String, ByVal valueFld As String, Optional ByVal currRow As Integer = 0)
    '    With cbo
    '        .DataSource = dt
    '        .DisplayMember = displayFld
    '        .ValueMember = valueFld
    '        .SelectedIndex = currRow
    '    End With
    'End Sub

    Public Function chkFileExist(ByVal FileRptPath As String) As Boolean
        Dim F As New FileInfo(FileRptPath)
        Return F.Exists
    End Function
    Public Sub SetControlFocus(e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Public Sub StrYNsetCheckBox(ByVal Str_Flag As String, ByRef objChk As CheckBox)
        If Str_Flag.Trim.ToUpper = "Y" Then
            objChk.Checked = True
        ElseIf Str_Flag.Trim.ToUpper = "N" Then
            objChk.Checked = False
        End If
    End Sub

    Public Function GetStatusFlagCheckBox(ByVal Str_Flag As Boolean) As String
        If Str_Flag = True Then
            Return "A"
        Else
            Return "D"
        End If
    End Function

    Public Function DisplayStatusFlagCheckBox(ByVal Str_Flag As String) As Boolean
        If Str_Flag = "A" Then
            Return True
        Else
            Return False
        End If
    End Function


    Public Function CheckDigitOnlyCurrency(ByVal str As String, ByVal index As Integer) As Boolean

        If index = 46 Then
            Dim iStr As String
            Dim i As Integer = 0
            For i = 0 To Len(str) - 1
                iStr = str.Substring(i, 1)
                If iStr = "." Then
                    pointDup = True
                    Exit For
                Else
                    pointDup = False
                End If
            Next
        End If


        Select Case index
            Case 47 To 58, 44 '  0 - 9
                CheckDigitOnlyCurrency = False
            Case 8, 13, 46 ' Backspace = 8, Enter = 13, Delete = 46
                If pointDup = True And index = 46 Then
                    CheckDigitOnlyCurrency = True
                Else
                    CheckDigitOnlyCurrency = False
                End If
            Case Else
                CheckDigitOnlyCurrency = True
        End Select


    End Function
    Public Function CheckDigitOnlyDouble(ByVal str As String, ByVal index As Integer) As Boolean

        If index = 46 Then
            Dim iStr As String
            Dim i As Integer = 0
            For i = 0 To Len(str) - 1
                iStr = str.Substring(i, 1)
                If iStr = "." Then
                    pointDup = True
                    Exit For
                Else
                    pointDup = False
                End If
            Next
        End If


        Select Case index
            Case 47 To 58 '  0 - 9
                CheckDigitOnlyDouble = False
            Case 8, 13, 46 ' Backspace = 8, Enter = 13, Delete = 46
                If pointDup = True And index = 46 Then
                    CheckDigitOnlyDouble = True
                Else
                    CheckDigitOnlyDouble = False
                End If
            Case Else
                CheckDigitOnlyDouble = True
        End Select


    End Function

    Public Function CheckDigitOnlyInteger(ByVal index As Integer) As Boolean
        Select Case index
            Case 47 To 58 ' �Ţ 0 - 9
                CheckDigitOnlyInteger = False
            Case 8, 13 ' Backspace = 8, Enter = 13, Delete/. = 46
                CheckDigitOnlyInteger = False
            Case Else
                CheckDigitOnlyInteger = True
        End Select
    End Function
    Public Function ConvertInt2YN(ByVal iStr As Integer) As String
        If iStr = 1 Then
            Return "Y"
        Else
            Return "N"
        End If
    End Function

    Public Function ConvertInt2CheckBoxStatus(ByVal iStr As Integer) As Boolean
        If iStr = 1 Then
            Return True
        Else
            Return False
        End If
    End Function


    Public Function ConvertYN2Boolean(ByVal iStr As String) As Boolean
        If iStr = "Y" Then
            Return True
        Else
            Return False
        End If
    End Function


    Public Function CheckBoxsetStrYN(ByVal objChk As CheckBox) As String
        If objChk.Checked = True Then
            Return "Y"
        ElseIf objChk.Checked = False Then
            Return "N"
        End If
    End Function
    Public Function chkDivideByZero(ByVal Value As Double) As Double
        Dim result As Double
        If Value = 0 Then
            result = 0
        Else
            result = Value
        End If
        Return result
    End Function
    Public Function chkNullByText(ByVal Value As String) As String
        Dim result As String
        If Value = "" Then
            result = "-"
        Else
            result = Value
        End If
        Return result
    End Function
    Public Function chkNullByBlank(ByVal Value As String) As String
        Dim result As String
        If Value = "" Or Value = "-" Then
            result = ""
        Else
            result = Value
        End If
        Return result
    End Function

    Public Function chkNullByDouble(ByVal Value As String) As Double
        Dim result As Double
        If Value = "" Then
            result = 0
        Else
            result = Double.Parse(Value)
        End If
        Return result
    End Function
    Public Function chkNullByZero(ByVal Value As String) As Integer
        Dim result As Integer
        If Value = "" Then
            result = 0
        Else
            result = Integer.Parse(Value)
        End If
        Return result
    End Function

    Public Function chkNullByDate(ByVal Value As String) As Date
        Dim result As Date
        If Value = "" Then
            result = Today.Date()
        Else
            result = Date.Parse(Value)
        End If
        Return result
    End Function

    Public Function StrNull2Str(ByVal str As String) As String
        If IsDBNull(str) Then
            Return ""
        Else
            Return CStr(str)
        End If
    End Function
    Public Function StrNull2Zero(ByVal str As String) As Integer
        If str = "" Or Not IsNumeric(str) Then
            Return 0
        Else
            Return CInt(str)
        End If
    End Function
    Public Function StrNull2Dbl(ByVal str As String) As Double
        If IsDBNull(str) Or str = "" Then
            Return 0
        Else
            Return CDbl(str)
        End If
    End Function
    Public Function DBNull2Str(ByVal str As Object) As String
        If IsDBNull(str) Then
            Return ""
        Else
            Return CStr(str)
        End If
    End Function
    Public Function DBNull2Zero(ByVal str As Object) As Integer
        If IsDBNull(str) Then
            Return 0
        Else
            Return CInt(str)
        End If
    End Function
    Public Function DBNull2Dbl(ByVal str As Object) As Double
        If IsDBNull(str) Then
            Return 0
        Else
            Return StrNull2Dbl(str)
        End If
    End Function

    Public Function PrepareFieldLenght(ByVal Field_Value As String, ByVal Field_Length As Integer) As String
        Dim result As String = ""
        Dim tmp_value As String = ""
        If Not Field_Value Is Nothing Then
            result = Field_Value
            If Field_Length > 0 And Field_Value.Length > Field_Length Then
                Dim CountChar As Integer
                Dim str As String
                Dim i As Integer
                i = 0
                tmp_value = Field_Value.Substring(0, Field_Length)
                result = tmp_value
                For i = 0 To tmp_value.Length - 1
                    str = tmp_value.Substring(i, 1)
                    If str = "'" Then
                        CountChar += 1
                    End If
                Next
                If Not (CountChar Mod 2 = 0) Then
                    i = tmp_value.Length - 1
                    While i >= 0
                        str = tmp_value.Substring(i, 1)
                        If str = "'" Then
                            result = tmp_value.Substring(0, i)
                            Exit While
                        End If
                        i -= 1
                    End While
                End If
            End If
        End If
        Return result
    End Function
    Public Function PrepareSqlValue(ByVal Field_Value As String) As String
        Dim result As String = ""
        If Not Field_Value Is Nothing Then
            If Field_Value.Trim.Length > 0 Then
                result = Field_Value.Replace("'", "''")
            End If
        End If
        Return result
    End Function
    Public Function ReverseSqlValue(ByVal Field_Value As String) As String
        Dim result As String = ""
        If Not Field_Value Is Nothing Then
            If Field_Value.Trim.Length > 0 Then
                result = Field_Value.Replace("''", "'")
            End If
        End If
        Return result
    End Function

    Public Function setTimeFormat(ByVal strTime As String, Optional ByVal f_millisec As Boolean = False) As String
        Dim strReturn As String
        If strTime.Length < 6 Then
            strTime = strTime.PadLeft(6, "0"c)
        End If
        If f_millisec Then
            strReturn = strTime.Substring(0, 2) & ":" & strTime.Substring(2, 2) & ":" & strTime.Substring(4, 2)
        Else
            strReturn = strTime.Substring(0, 2) & ":" & strTime.Substring(2, 2)
        End If
        Return strReturn
    End Function

    Public Function setDateFormat(ByVal strDate As String) As String
        Dim strYear As String
        Select Case strDate.Trim.Length
            Case 8
                strYear = Left(strDate, 4)
                If strYear < "2500" Then
                    strYear = (CInt(strYear) + 543).ToString
                Else
                    strYear = strYear
                End If
                setDateFormat = Right(strDate, 2) & "/" & Mid(strDate, 5, 2) & "/" & strYear

            Case 12
                strYear = Left(strDate, 4)

                If strYear < "2500" Then
                    strYear = (CInt(strYear) + 543).ToString
                Else
                    strYear = strYear
                End If

                setDateFormat = Mid(strDate, 7, 2) & "/" & Mid(strDate, 5, 2) & "/" & strYear & "@" & Mid(strDate, 9, 2) & ":" & Right(strDate, 2)
            Case Else
                setDateFormat = ""

        End Select

    End Function

    Public Function setValue(ByVal objValue As Object, Optional ByVal strType As String = "text", Optional ByVal DecimalPoint As Integer = -1) As String
        Dim strReturn As String
        Dim f_blank_value As Boolean = True
        Dim dec_value As Decimal
        'Dim f_use_field_decimal As Boolean = False
        Dim strFormName, strCtrlName As String
        ' Dim objDPntMS As New SMDPNTMS
        Dim dtDPnt As DataTable
        Dim drDPnt As DataRow
        Dim dt As DataTable
        Dim int_scrDPnt As Integer

        'Select Case strDecimalType.ToUpper
        '    Case "O", "N" : int_scrDPnt = int_scrDecimalPoint
        '    Case "Q" : int_scrDPnt = int_qtyDecimalPoint
        '    Case "V" : int_scrDPnt = int_valDecimalPoint
        '    Case "R" : int_scrDPnt = int_rptDecimalPoint
        'End Select


        If IsNothing(strType) Then strType = "text"



        Dim DecFormat As String = ("##################0." & "".PadRight(int_scrDPnt, "#"c))
        Dim strDecFormat As String = ("#,###,###,###,###,###,##0." & "".PadRight(int_scrDPnt, "0"c))
        Dim strIntFormat As String = ("#,###,###,###,###,###,##0")

        'If f_use_field_decimal Then strDecFormat = ("#,###,###,###,###,###,##0." & "".PadRight(int_scrDPntDB, "#".Chars(0)))


        If IsDBNull(objValue) Then
            f_blank_value = True
        Else
            If IsNothing(objValue) Then
                f_blank_value = True
            Else
                If objValue.ToString.Length > 0 Then
                    Select Case strType.ToLower
                        Case "sql" : strReturn = PrepareSqlValue(objValue.ToString)
                        Case "text", "string" : strReturn = objValue.ToString

                            'ReverseSqlValue อาจจะเรียกใช้งานเฉพาะตอนที่ load ค่า database รึเปล่าเพราะไม่เช่นนั้นถ้าต้องการ set value string ปกติอาจจะ error ได้ครับ
                        Case "sql_reverse" : strReturn = ReverseSqlValue(objValue.ToString)

                        Case "date" : strReturn = setDateFormat(objValue.ToString)
                        Case "time" : strReturn = setTimeFormat(objValue.ToString, False)
                        Case "timems", "timemillisec" : strReturn = setTimeFormat(objValue.ToString, True)
                        Case "currency" : strReturn = objValue.ToString
                        Case "decimal", "double", "long"
                            If IsNumeric(objValue.ToString) Then
                                'strReturn = CDbl(objValue).ToString
                                'strReturn = Decimal.Round(CType(objValue, Decimal), int_scrDPnt).ToString(DecFormat)
                                strReturn = CDbl(objValue).ToString(DecFormat)
                            Else
                                strReturn = "0"
                            End If
                        Case "strdecimal"
                            If IsNumeric(objValue.ToString) Then
                                strReturn = CDbl(objValue).ToString(strDecFormat)
                                'strReturn = Decimal.Round(CType(objValue, Decimal), int_scrDPnt).ToString(strDecFormat)
                                'If f_use_field_decimal Then
                                '    strReturn = Decimal.Round(CType(objValue, Decimal), int_scrDPntDB).ToString(strDecFormat)
                                'Else
                                '    strReturn = Decimal.Round(CType(objValue, Decimal), int_scrDecimalPoint).ToString(strDecFormat)
                                'End If
                            Else
                                strReturn = "0"
                            End If
                            strReturn = ReverseSqlValue(strReturn)
                        Case "number", "int", "integer", "numeric"
                            If IsNumeric(objValue.ToString) Then
                                strReturn = CInt(objValue).ToString
                            Else
                                strReturn = "0"
                            End If
                        Case "strint"
                            If IsNumeric(objValue.ToString) Then
                                strReturn = Decimal.Round(CType(objValue, Decimal), 0).ToString(strIntFormat)
                                'If f_use_field_decimal Then
                                '    strReturn = Decimal.Round(CType(objValue, Decimal), int_scrDPntDB).ToString(strDecFormat)
                                'Else
                                '    strReturn = Decimal.Round(CType(objValue, Decimal), int_scrDecimalPoint).ToString(strDecFormat)
                                'End If

                            Else
                                strReturn = "0"
                            End If
                            strReturn = ReverseSqlValue(strReturn)
                        Case "datatable"
                            dt = CType(objValue, DataTable)
                            strReturn = dt.Rows.Count.ToString
                        Case Else : strReturn = objValue.ToString
                    End Select
                    f_blank_value = False
                End If
            End If
        End If

        If f_blank_value Then
            Select Case strType.ToLower
                Case "text", "string" : strReturn = ""
                Case "date" : strReturn = ""
                Case "currency" : strReturn = "0"
                Case "decimal", "double", "long" : strReturn = "0"
                Case "strdecimal" : strReturn = "0"
                Case "strint" : strReturn = "0"
                Case "number", "int", "integer", "numeric" : strReturn = "0"
                Case "datatable" : strReturn = "0"
            End Select
        End If
        setValue = strReturn
    End Function
#End Region
    Public Function isInStr(ByVal str_source As String, ByVal str_check As String, Optional ByVal str_split As String = ",") As Boolean
        Dim f_return As Boolean = False
        Dim strCompare As String
        For Each strCompare In str_source.Split(str_split.Chars(0))
            If Trim(str_check) = Trim(strCompare) Then
                f_return = True
                Exit For
            End If
        Next
        Return f_return
    End Function
    Public Function addSQLString(ByVal strSQL As String, ByVal chkVal As String, ByVal strSQLAdd As String) As String
        chkVal = setValue(chkVal)
        strSQL = setValue(strSQL)
        If strSQL.Length > 0 And chkVal.Length > 0 Then
            strSQL &= " and " & strSQLAdd
        ElseIf chkVal.Length > 0 Then
            strSQL = strSQLAdd
        End If
        Return strSQL
    End Function


    Public Function sumDataTable(ByVal sumDT As DataTable, ByVal grp_field_name As String, ByVal sum_field_name As String, Optional ByVal concat_field_name As String = "", Optional ByVal concat_char As String = ",", Optional ByVal max_field_name As String = "") As DataTable
        Dim resDT As New DataTable 'res = result
        Dim dc As DataColumn
        Dim sumDR, resDR, newDR As DataRow
        Dim grp_name(), curr_value(), sum_name, max_value As String
        Dim f_repeat As Boolean
        Dim n, m, k As Integer
        'สร้าง column ให้กับ resDT ให้เหมือนกับ sumDT
        For Each dc In sumDT.Columns
            resDT.Columns.Add(dc.ColumnName)
        Next
        'เก็บ field ที่ต้องการทดสอบไว้ที่ array ชื่อ chk_name
        grp_name = grp_field_name.Split(",".Chars(0))
        'วนลูป sumDT แต่ละ row
        For k = 0 To sumDT.Rows.Count - 1
            sumDR = sumDT.Rows(k)
            'ถ้า resDT ยังไม่มี row ให้เพิ่ม rows ปัจจุบันนั้นเข้าไป
            If resDT.Rows.Count = 0 Then
                newDR = resDT.NewRow
                For m = 0 To sumDR.ItemArray.Length - 1
                    newDR.Item(m) = sumDR.Item(m)
                Next
                resDT.Rows.Add(newDR)
            Else
                'วนลูป resDT แต่ละ row
                For Each resDR In resDT.Rows
                    'กำหนด ค่าตั้งต้นของ f_repeat
                    f_repeat = True
                    'ตรวจสอบแต่ละค่าของ chk_name แต่ละ field ว่าตรงกับข้อมูลที่มีอยู่ในแต่ละ row ของ resDt หรือไม่
                    For n = 0 To grp_name.Length - 1
                        f_repeat = f_repeat And (setValue(resDR.Item(Trim(grp_name(n)))) = setValue(sumDR.Item(grp_name(n))))
                    Next
                    'กรณีที่มีค่าตรงกันทุก field จะทำให้ f_repeat เป็นค่า true แต่ถ้าไม่ตรงกันเพียงหนึ่งค่า f_repeat จะมีค่าเป็น fault
                    If f_repeat Then
                        'บวก สำหรับ sum_field_name
                        For Each sum_name In sum_field_name.Split(",".Chars(0))
                            sum_name = Trim(sum_name)
                            resDR.Item(sum_name) = (CDbl(setValue(resDR.Item(sum_name), "decimal")) + CDbl(setValue(sumDR.Item(sum_name), "decimal"))).ToString
                        Next
                        'concat ค่า สำหรับ concat_field_name
                        For Each sum_name In concat_field_name.Split(",".Chars(0))
                            sum_name = Trim(sum_name)
                            resDR.Item(sum_name) = setValue(resDR.Item(sum_name)) & concat_char & setValue(sumDR.Item(sum_name))
                        Next
                        Exit For
                        max_value = ""
                        For Each sum_name In max_field_name.Split(",".Chars(0))
                            sum_name = Trim(sum_name)
                            If setValue(resDR.Item(sum_name)) > max_value Then
                                resDR.Item(sum_name) = setValue(resDR.Item(sum_name))
                            End If
                        Next
                    End If
                Next
                'ถ้าตรวจสอบแต่ละ row ของ resDT แล้วไม่พบค่าซ้ำเลย แสดงว่า ต้องแอด row ใหม่ให้ resDT
                If Not f_repeat Then
                    newDR = resDT.NewRow
                    For m = 0 To sumDR.ItemArray.Length - 1
                        newDR.Item(m) = sumDR.Item(m)
                    Next
                    resDT.Rows.Add(newDR)
                End If
            End If

        Next
        Return resDT
    End Function

    Public Function getCheckboxValue(ByVal chkbox As CheckBox) As String
        If chkbox.Checked Then
            Return "Y"
        Else
            Return "N"
        End If
    End Function

    Public Function chkTextboxLostFocus(ByVal frm As Form, ByVal txtCheck As TextBox, ByVal btnExit As Button) As Boolean
        Dim f_return As Boolean = False
        Try
            If txtCheck.TextLength > 0 Then
                If Not frm.ActiveForm Is Nothing Then
                    If frm.ActiveForm.Name = frm.Name Then
                        If Not IsNothing(frm.ActiveControl) Then
                            If Not btnExit.Focused Then
                                f_return = True
                            End If
                        End If
                    End If
                Else
                    If Not btnExit.Focused Then
                        f_return = True
                    End If
                End If
            End If
        Catch ex As Exception
            f_return = False
        End Try
        Return f_return
    End Function

    'Public Sub setBackColor_Code(ByVal lsvRow As Integer, ByVal lsvSubItem As Integer, ByVal lsvDetail As ListView, ByVal Color_Code As Drawing.Color)
    '    lsvDetail.Items(lsvRow).UseItemStyleForSubItems = False
    '    lsvDetail.Items(lsvRow).SubItems(lsvSubItem).BackColor = Color_Code
    'End Sub
    Public Function setDocno(ByVal strInitial As String, ByVal strYear As String, ByVal strMonth As String, ByVal strRunning As String) As String
        Dim Running_Len As Integer
        Running_Len = Len(strRunning)
        Dim i As Integer
        strRunning = CStr(Val(strRunning) + 1)

        For i = 1 To Running_Len
            If Len(strRunning) < Running_Len Then
                strRunning = "0" & strRunning
            Else
                Exit For
            End If
        Next
        setDocno = strInitial & strYear & strMonth & strRunning
    End Function
    Public Function SetDateToFormatYYYYMMDD(ByVal StrDate As String, Optional ByVal StrLanguage As String = "en") As String
        'ใช้แปลงDate ที่มี"/"คั่นอยู่  ให้เป็น Format ที่สามารถบันทึกลง DataBase โดย เลือกได้ว่า จะให้เป็น พศ.หรือ คศ. 
        Dim ArrDate() As String
        Dim StrFormat As String
        Dim StrYear As String
        If StrDate.Trim.Length = 10 Then
            ArrDate = StrDate.ToString.Split("/".ToCharArray)
            If StrLanguage.ToLower = "th" Then
                StrYear = (CDec(ArrDate(2).ToString.Trim) + 543).ToString.Trim
            ElseIf StrLanguage.ToLower = "en" Then
                StrYear = (CDec(ArrDate(2).ToString.Trim) - 543).ToString.Trim
            End If
            StrFormat = StrYear.Trim & ArrDate(1).ToString.Trim & ArrDate(0).ToString.Trim
        Else
            StrFormat = ""
        End If
        Return StrFormat
    End Function

    Public Function ConvertStrDate2ShotDateTH(ByVal dt As String) As String

        If dt <> "" Then

            Dim y As String = ""
            Dim d As String = ""
            Dim m As String = ""

            Dim iY As Integer = 0

            Dim str() As String
            str = Split(dt, "/")

            d = str(0).ToString
            m = str(1).ToString

            While (d.Length < 2)
                d = ("0" + d)
            End While

            While (m.Length < 2)
                m = ("0" + m)
            End While

            y = str(2)
            iY = StrNull2Zero(str(2))

            Do Until iY > 2500
                iY = iY + 543
            Loop
            y = iY.ToString()

            Return d + "/" + m + "/" + y

        Else
            Return ""
        End If
    End Function


    Public Function chkFieldTableIsExist(ByVal objDB As BaseClass, ByVal field_name As String, ByVal table_name As String) As Boolean
        Dim sql As String
        Dim ReturnValue As Boolean = False
        'Dim objDB As New baseclass
        field_name = modMain.setValue(field_name).Trim
        table_name = modMain.setValue(table_name).Trim
        sql = " SELECT     dbo.sysobjects.name AS table_name, dbo.syscolumns.name"
        sql += " FROM         dbo.sysobjects INNER JOIN"
        sql += " dbo.syscolumns ON dbo.sysobjects.id = dbo.syscolumns.id"
        sql += " WHERE     (UPPER(LTRIM(RTRIM(dbo.sysobjects.xtype))) = 'U' OR"
        sql += " UPPER(LTRIM(RTRIM(dbo.sysobjects.xtype))) = 'V') AND (UPPER(LTRIM(RTRIM(dbo.sysobjects.name))) = '" & table_name & "') AND "
        sql += " (UPPER(LTRIM(RTRIM(dbo.syscolumns.name))) = '" & field_name & "')"
        'If objDB.OpenDatabase Then
        Dim dt As DataTable = objDB.ExecuteQuery(sql)
        If Not dt Is Nothing Then
            ReturnValue = (dt.Rows.Count > 0)
        End If
        'objDB.CloseDatabase()
        'End If
        'objDB = Nothing
        Return ReturnValue
    End Function

    Public Function selectTop1FieldString(ByVal objConn As BaseClass, ByVal FieldName As String, ByVal tblName As String, ByVal Criteria As String, Optional ByVal orderBy As Boolean = False) As String
        Dim result As String = ""
        ' Dim objConn As New baseclass
        tblName = setValue(tblName).Trim
        FieldName = setValue(FieldName).Trim
        Criteria = setValue(Criteria).Trim
        If tblName.Length > 0 And FieldName.Length > 0 Then
            If chkFieldTableIsExist(objConn, FieldName, tblName) Then
                'If objConn.OpenDatabase Then
                Dim sql As String = ""
                Dim dt As DataTable
                sql = "select top 1 " & FieldName & " as topField from " & tblName
                If Criteria.Length > 0 And orderBy = False Then
                    sql += " where " + Criteria
                ElseIf Criteria.Length > 0 And orderBy = True Then
                    sql += " where " + Criteria
                    sql += " order by " & FieldName & " desc"
                ElseIf Criteria.Length = 0 And orderBy = True Then
                    sql += " order by " & FieldName & " desc"
                End If
                dt = objConn.ExecuteQuery(sql)
                If Not dt Is Nothing Then
                    If dt.Rows.Count > 0 Then
                        result = setValue(dt.Rows(0).Item("topField").ToString.Trim)
                    End If
                End If
                'objConn.CloseDatabase()
                '    End If
                'objConn = Nothing
            End If
        End If
        Return result
    End Function

    Public Function getStrLikePositionFomulaSql(ByVal str As String) As String
        str = str.Trim
        If str.Trim.Length > 0 Then
            str = str.Replace("#", "%")
            'str &= "*"
        End If
        Return str
    End Function


#Region "veriable for use control"
    'จำนวนแถวที่ต้องการแสดงใน listview
    Public int_showLimtLSVRoW As Integer = 100
    Public bln_UseDistinct As Boolean = True
    Public Class Display_fld_UscControls
        Public Const code As String = "code"
        Public Const name As String = "name"
    End Class
    Public Function chkfldDisplay_UscControls(ByVal value As String) As String
        value = setValue(value).Trim.ToLower
        If Not (value = Display_fld_UscControls.code Or value = Display_fld_UscControls.name) Then
            value = Display_fld_UscControls.code
        End If
        Return value
    End Function
#End Region


#Region "calculate day month year <forecast of beger>"
    Public Function YearMonthDay_FirstMonth(Optional ByVal YMD As String = "") As String ' function คืนค่าเดือน 01 และ วันที่ 01 ของปี่ที่ต้องการค้นหา โดยคืนค่าเป็น 20070101
        Dim YMD_Now As String = ""
        Dim result As String = ""
        If YMD.Length = 0 Then
            YMD_Now = getCurStrDateDB("en")
            result = YMD_Now.Substring(0, 4) + "01" + "01"
        Else
            result = YMD.Substring(0, 4) + "01" + "01"
        End If
        Return result
    End Function
    Public Function YearMonthDay_LastMonth(Optional ByVal YMD As String = "") As String  'the function is return last day of year in  parameter YMD. You must format of parameter is 20070320 year month day
        'example if you use  20071023 into this fuction.it's retrun 20061231 for you.
        Dim YMD_Now As String = ""
        Dim result As String = ""
        Dim minus_Year As Integer = 0
        If YMD.Length = 0 Then
            YMD_Now = getCurStrDateDB("en")
            minus_Year = CInt(YMD_Now.Substring(0, 4)) - 1
            result = CStr(minus_Year) + "12" + "31"
        Else
            minus_Year = CInt(YMD.Substring(0, 4)) - 1
            result = YMD.Substring(0, 4) + "12" + "31"
        End If
        Return result
    End Function
    Public Function CalculateMonth(ByVal StrYearMonthDate_Start As String, Optional ByVal StrYearMonthDate_Now As String = "") As Double ' หาจำนวนเดือน format ในการใส่ parameter "20071201"  คือ ปีเดือนวัน  ปีที่ 2007 เดือน 12 วันที่ 01
        Dim Year_Start, Year_Now, Month_Start, Month_Now, Day_Start, Day_Now As String
        Dim AmountYear, AmountMonth As Integer
        Dim ResultAmountMonth As Double
        Dim FractionDay As Double = 0
        Dim ResultFractionDay As Double = 0
        Year_Start = "" : Year_Now = "" : Month_Start = "" : Month_Now = "" : Day_Start = "" : Day_Now = ""
        AmountYear = 0 : AmountMonth = 0 : ResultAmountMonth = 0
        Try
            If StrYearMonthDate_Now.Length = 0 Then
                StrYearMonthDate_Now = getCurStrDateDB("en").Trim
            End If
            Year_Start = StrYearMonthDate_Start.Substring(0, 4) ' ปี เริ่ม
            Year_Now = StrYearMonthDate_Now.Substring(0, 4)  ' ปี สุด
            AmountYear = CInt(Year_Now) - CInt(Year_Start)   'ผลต่าง ของปีเริ่ม กัน ปีสุด
            If AmountYear >= 0 Then
                Month_Start = StrYearMonthDate_Start.Substring(4, 2) ' เดือน เริ่ม
                Month_Now = StrYearMonthDate_Now.Substring(4, 2) ' เดือน สุด
                Day_Start = StrYearMonthDate_Start.Substring(6, 2)  ' วัน เริ่ม
                Day_Now = StrYearMonthDate_Now.Substring(6, 2) ' วัน สุด 
                ResultAmountMonth = (AmountYear * 12)
                If CInt(Day_Start) > 1 Then
                    FractionDay = (NumDayInMonth(CInt(Month_Start)) - CInt(Day_Start))
                    ResultAmountMonth = ResultAmountMonth - 1
                End If
                Select Case NumDayInMonth(CInt(Month_Now))
                    Case 28
                        If CInt(Day_Now) >= 28 Then
                            ResultAmountMonth = ResultAmountMonth + 1
                            ResultFractionDay = FractionDay / 30
                        Else
                            FractionDay = FractionDay + CInt(Day_Now)
                            ResultFractionDay = FractionDay / 30
                        End If
                    Case 29
                        If CInt(Day_Now) >= 29 Then
                            ResultAmountMonth = ResultAmountMonth + 1
                            ResultFractionDay = FractionDay / 30
                        Else
                            FractionDay = FractionDay + CInt(Day_Now)
                            ResultFractionDay = FractionDay / 30
                        End If
                    Case 30
                        If CInt(Day_Now) >= 30 Then
                            ResultAmountMonth = ResultAmountMonth + 1
                            ResultFractionDay = FractionDay / 30
                        Else
                            FractionDay = FractionDay + CInt(Day_Now)
                            ResultFractionDay = FractionDay / 30
                        End If
                    Case 31
                        If CInt(Day_Now) >= 31 Then
                            ResultAmountMonth = ResultAmountMonth + 1
                            ResultFractionDay = FractionDay / 30
                        Else
                            FractionDay = FractionDay + CInt(Day_Now)
                            ResultFractionDay = FractionDay / 30
                        End If
                End Select
                ResultAmountMonth = ResultAmountMonth + CInt(Month_Now)
                ResultAmountMonth = ResultAmountMonth - CInt(Month_Start)
                ResultAmountMonth = ResultAmountMonth + ResultFractionDay
            Else
                ResultAmountMonth = 0
            End If
            Return ResultAmountMonth
        Catch ex As Exception
        End Try
    End Function
    Public Function NumDayInMonth(ByVal IndexMonth As Integer) As Integer   'function หาจำนวนวันใน แต่ละเดือน โดยรับค่าเป็นตัวเลขตามเดือนที่ต้องการค้นหารวัน
        Select Case IndexMonth
            Case 1, 3, 5, 7, 8, 10, 12
                Return 31
            Case 2
                Dim YearMonthDay As String = getCurStrDateDB("en")
                Dim Year As Integer = 0
                Year = CInt(YearMonthDay.Substring(0, 4))
                If Year Mod 4 = 0 Then
                    Return 29
                End If
                Return 28
            Case 4, 6, 9, 11
                Return 30
        End Select
    End Function
#End Region

#Region "function decimal"
    Public Function DoubleRound(ByVal MyNumber As Double, ByVal PositionRound As Integer) As Double  ' เอาไว้ปัดทศนิยม โดยไม่สนใจว่าต้องมากกว่า 5 
        ' โดยรับค่าตัวเลขทศนิยม และระบุตำแหน่งที่ต้องการปัด
        Dim result As Double = 0
        Dim strConVNum As String = ""
        strConVNum = MyNumber.ToString
        If InStr(strConVNum, ".") = 0 Then
            Return MyNumber
        Else
            If Len(strConVNum.Substring(strConVNum.IndexOf(".") + 1)) > PositionRound Then
                Dim i As Integer
                Dim strFormat As String = "0."
                For i = 1 To PositionRound
                    If PositionRound = i Then
                        strFormat = strFormat + "1"
                    Else
                        strFormat = strFormat + "0"
                    End If
                Next
                MyNumber = CDbl(strConVNum.Substring(0, InStr(strConVNum, ".") + PositionRound))
                MyNumber = MyNumber + CDbl(strFormat)
                Return MyNumber
            Else
                Return MyNumber
            End If
        End If
    End Function
    Public Function getPosDecimal(ByVal myDigit As Double, Optional ByVal DecimalTotal As Integer = 1) As String 'คืนค่าตัวเลขเป็นทศนิยมตามที่ผู้ใช้ระบุ
        ' รับค่าตัวเลขทศนิยม และตำแหน่งที่ต้องการระบุทศนิยม 
        Dim strReturn As String = "###,###,###,###,###."
        If myDigit = 0 Then
            strReturn = "0."
        End If
        If myDigit = 0 Then
            strReturn = "0."
        End If
        Dim i As Integer
        For i = 1 To DecimalTotal
            If i = DecimalTotal Then
                strReturn = strReturn + "0"
            Else
                strReturn = strReturn + "#"
            End If
        Next
        Return myDigit.ToString(strReturn)
    End Function
#End Region

#Region "SavePicToServer"
    Public Function CopyFileToFolderShareServer(ByVal FormCode As String, ByVal a_SourceFile As String, ByRef a_TragetPath As String, ByVal a_FolderItem As String, Optional ByRef a_retrun_TragetFile As String = "") As Boolean
        Dim f_return As Boolean = False
        a_retrun_TragetFile = ""
        'check folder name
        If a_FolderItem.Length > 0 Then
            'Ensure that the Soruce Path File exist.
            If System.IO.File.Exists(a_SourceFile) Then
                'Ensure that the Traget Path exist.
                If Not System.IO.Directory.Exists(a_TragetPath) Then
                    MkDir(a_TragetPath)
                End If
                Try
                    Dim TargetFolderNamePath As String

                    TargetFolderNamePath = a_TragetPath & "\" & a_FolderItem

                    If Not System.IO.Directory.Exists(TargetFolderNamePath) Then
                        System.IO.Directory.CreateDirectory(TargetFolderNamePath)
                    End If
                    Dim ArrStr() As String = a_SourceFile.Split("\".Chars(0))
                    Dim FileName As String = ArrStr(ArrStr.Length - 1)
                    Dim FolderNamePath_Old, FolderNamePath_New As String

                    FolderNamePath_Old = TargetFolderNamePath & "\" & FileName
                    FolderNamePath_New = TargetFolderNamePath & "\" & FormCode & "_" & FileName

                    If a_SourceFile.Trim <> FolderNamePath_Old Then

                        System.IO.File.Copy(a_SourceFile, FolderNamePath_New, True)
                        a_retrun_TragetFile = FolderNamePath_New
                    Else
                        a_retrun_TragetFile = FolderNamePath_Old
                    End If
                    f_return = True
                Catch ex As Exception
                    MsgBox(ex)
                End Try
            End If
        End If
        Return f_return
    End Function
    Public Function DelFileToFolderShareServer(ByVal a_SourceFile As String, ByRef a_TragetPath As String, Optional ByRef a_retrun_TragetFile As String = "") As Boolean
        'a_SourceFile = PathFile ที่ จะลบ ,a_TragetPath = PathFile ที่แชร์ไว้ 
        Dim f_return As Boolean = False
        a_retrun_TragetFile = ""
        'check folder name
        'Ensure that the Traget Path exist.
        If System.IO.Directory.Exists(a_TragetPath) Then
            Try
                If System.IO.File.Exists(a_SourceFile) Then   'Ensure that the Soruce Path File exist.
                    System.IO.File.Delete(a_SourceFile)
                    a_retrun_TragetFile = ""
                    Return True
                Else
                    a_retrun_TragetFile = a_SourceFile
                    Return False
                End If
            Catch ex As Exception
                MsgBox(ex)
            End Try
        Else
            Return False
        End If

    End Function
#End Region


    'Convert DB string date to normal string date
#Region "DateTime Format"



    Public Function getCurStrDateNormal(Optional ByVal th_or_en As String = "") As String
        Dim y As Integer
        Dim m As Integer
        Dim d As Integer
        If th_or_en.Trim.ToLower = "en" Then
            y = getYearEng(Now.Date.Year)
        ElseIf th_or_en.Trim.ToLower = "th" Then
            y = getYearThai(Now.Date.Year)
        Else
            y = getYearByCurrentLang(Now.Date.Year)
        End If
        m = Now.Date.Month
        d = Now.Date.Day
        getCurStrDateNormal = d.ToString("00") & "/" & m.ToString("00") & "/" & y.ToString("0000")
    End Function


    Public Function getCurStrTime() As String
        getCurStrTime = Format(Now, "HHmmss")
    End Function
    Public Function getCurStrDateDB(Optional ByVal th_or_en As String = "") As String
        Dim y As Integer
        Dim m As Integer
        Dim d As Integer
        If th_or_en.Trim.ToLower = "en" Then
            y = getYearEng(Now.Date.Year)
        ElseIf th_or_en.Trim.ToLower = "th" Then
            y = getYearThai(Now.Date.Year)
        Else
            y = getYearByCurrentLang(Now.Date.Year)
        End If
        m = Now.Date.Month
        d = Now.Date.Day
        getCurStrDateDB = y.ToString("0000") & m.ToString("00") & d.ToString("00")
    End Function
    Public Function getYearEng(ByVal y As Integer) As Integer
        If y > MAX_ENG_YEAR Then
            y -= 543
        End If
        Return y
    End Function
    Public Function getYearThai(ByVal y As Integer) As Integer
        If y < MAX_ENG_YEAR Then
            y += 543
        End If
        Return y
    End Function
    Public Function getYearByCurrentLang(ByVal y As Integer) As Integer
        If STR_LANGUAGE.Trim.ToLower = "en" Then
            If y > MAX_ENG_YEAR Then
                y -= 543
            End If
        ElseIf STR_LANGUAGE.Trim.ToLower = "th" Then
            If y < MAX_ENG_YEAR Then
                y += 543
            End If
        End If
        Return y
    End Function

    Public Function ConvertDate2DB(ByVal strDate As Date, Optional ByVal th_or_en As String = "") As String
        Dim y As Integer
        Dim m As Integer
        Dim d As Integer

        Select Case th_or_en.Trim.ToLower
            Case "en"
                y = getYearEng(strDate.Year)
            Case "th"
                y = getYearThai(strDate.Year)
            Case Else
                y = strDate.Year
        End Select
        m = strDate.Month
        d = strDate.Day
        ConvertDate2DB = m.ToString("00") & "/" & d.ToString("00") & "/" & y.ToString("0000")
    End Function

#End Region


    Public Function DeleteByID(ByVal pID As String, ByVal sTable As String, ByVal pfeild As String) As Boolean
        Dim obj As New BaseClass
        Dim success As Boolean = False
        If obj.ExecuteNonQuery("Delete from  " & sTable & "  Where " & pfeild & "='" & pID & "'") Then

            success = True
        End If
        Return success
    End Function




    Public Function CheckPersonalID(ByVal uid As String) As Boolean
        Dim IntID As Integer = ((CInt(uid.Substring(0, 1)) * 13) _
                    + ((CInt(uid.Substring(1, 1)) * 12) _
                    + ((CInt(uid.Substring(2, 1)) * 11) _
                    + ((CInt(uid.Substring(3, 1)) * 10) _
                    + ((CInt(uid.Substring(4, 1)) * 9) _
                    + ((CInt(uid.Substring(5, 1)) * 8) _
                    + ((CInt(uid.Substring(6, 1)) * 7) _
                    + ((CInt(uid.Substring(7, 1)) * 6) _
                    + ((CInt(uid.Substring(8, 1)) * 5) _
                    + ((CInt(uid.Substring(9, 1)) * 4) _
                    + ((CInt(uid.Substring(10, 1)) * 3) _
                    + (CInt(uid.Substring(11, 1)) * 2))))))))))))
        Dim LastID As Integer = (IntID Mod 11)
        If (LastID = 0) Then
            LastID = 1
        ElseIf (LastID = 1) Then
            LastID = 0
        Else
            LastID = (11 - LastID)
        End If
        Return (CInt(uid.Substring(12, 1)) = LastID)
    End Function

    Public Function FormatPersonalID(ByVal uid As String) As String
        Dim IDformat As String = ""
        IDformat = (uid.Substring(0, 1) + " ")
        IDformat = (IDformat _
                    + (uid.Substring(1, 1) _
                    + (uid.Substring(2, 1) _
                    + (uid.Substring(3, 1) _
                    + (uid.Substring(4, 1) + " ")))))
        IDformat = (IDformat _
                    + (uid.Substring(5, 1) _
                    + (uid.Substring(6, 1) _
                    + (uid.Substring(7, 1) _
                    + (uid.Substring(8, 1) _
                    + (uid.Substring(9, 1) + " "))))))
        IDformat = (IDformat _
                    + (uid.Substring(10, 1) _
                    + (uid.Substring(11, 1) + " ")))
        IDformat = (IDformat + uid.Substring(12, 1))
        Return IDformat
    End Function


    Public Function ShowMenu(ByVal p As String, ByVal permission As String) As Boolean
        If permission.Contains(p) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function ConvertCurrencyToString(ByVal Num As String) As String
        ' สร้าง Array สำหรับเปลี่ยนเลขเป็นตัวอักษร เรียงจากน้อยไปมาก เพราะเราจะทำการไล่จากตัวท้ายไปหาตัวหน้า
        Dim number As String() = {"", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า"}

        ' และสำหรับหลักเลข 1 ชุด เรียงจากน้อยไปมาก เพราะเราจะทำการไล่จากตัวท้ายไปหาตัวหน้าเหมือนกัน
        Dim number2 As String() = {"", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", "ล้าน"}

        Dim Str As String = ""   'สร้าง String เก็บชุดอักษรที่คำนวณได้
        Dim lennum As Integer = Len(Num) 'นับจำนวนอักษรที่ผู้ใช้ใส่เข้ามา

        Dim tmp As Integer = 0  'ค่าเริ่มต้นของค่าหลักเลข
        Dim count As Integer = 0   'ค่าเริ่มต้นตัวนับที่จะไล่ไปตามตำแหน่งต่างๆ
        Dim i As Integer
        Dim ch As String
        Dim digit As String
        Dim pos As Integer
        Dim last As String
        Dim n As Integer = 0

        'วนค่าจากตำแหน่งท้ายของตัวเลขขึ้นมา เนื่องจากเรารู้ว่าตัวท้ายสุดจะเป็นหลักหน่วยเสมอ
        For i = lennum To 1 Step i - 1
            n = n + 1

            'เพิ่มค่าตัวนับในตำแหน่งที่วนลูป
            count = count + 1

            'เมื่อตำแหน่งตัวเลขมากกว่าหลักล้าน ให้ย้อนกลับเป็นหลักสิบ (ทำไมไม่เป็นหลักหน่วย? เนื่องจากเลยจากหลักล้านไปแล้ว ตัวต่อไปจะเหมือนกับหลักสิบ เช่น 10,000,000 เราจะเห็นว่าเลข 1 คือหลักสิบ)
            If (tmp = 7) Then tmp = 1

            'ดึงตัวเลขของตำแหน่งที่วนมาถึง
            ch = Mid(Num, lennum + 1 - n, 1)

            'เปลี่ยนตัวเลขเป็นตัวหนังสือ ของตัวนั้นๆ
            digit = number(Int(ch))
            'เก็บค่าของหลักลงตัวแปร(เริ่มจาก 1)
            pos = tmp + 1
            'หากเป็นหลักสิบ และตัวเลขที่เจอเป็น 1 ไม่ให้แสดงตัวอักษร คำว่า หนึ่ง เนื่องจากเราจะไม่อ่านว่า หนึ่งสิบ
            If pos = 2 And ch = "1" Then
                digit = ""
                'หากเป็นหลักสิบ และตัวเลขที่เจอเป็น 2 ให้แสดงตัวอักษร คำว่า ยี่ เนื่องจากเราจะไม่อ่านว่า สองสิบ
            ElseIf (pos = 2 And ch = "2") Then
                digit = "ยี่"
                'หากเป็นหลักหน่วย หรือหลักล้าน และตัวเลขที่พบคือ 1 และยังมีหลักที่มากกว่าหลักหน่วยปัจจุบัน ให้แสดงเป็น เอ็ด แทน หนึ่ง
            ElseIf (pos = 1 Or pos = 7) And ch = "1" And lennum > count Then
                digit = "เอ็ด"
            End If

            'สร้างหลักจากตำแหน่งของหลักที่พบ
            last = number2(tmp)

            'ถ้าตัวเลขที่พบเป็น 0 และไม่ใช่หลักล้าน ไม่ให้แสดงอักษรของหลัก
            If ch = "0" And pos <> 7 Then last = ""
            'เอาค่าที่หาได้มาต่อกับ str โดยต่อจากข้างหน้าไปเรื่อยๆ
            Str = digit + last + Str
            'เพิ่มค่าหลักเลขที่เริ่มจากหน่วย(0) ถึง ล้าน(6)
            tmp = tmp + 1
        Next
        'แสดงผลลัพธ์
        Return Str + "บาทถ้วน"
    End Function


    Public Function toThaiNumber(ByVal num As String) As String
        Dim tmp As String = ""
        Dim i As Integer = 0

        For i = 0 To i < num.Length

            If (num.Substring(i, 1) = "0") Then
                tmp += "๐"
            ElseIf (num.Substring(i, 1) = "1") Then
                tmp += "๑"
            ElseIf (num.Substring(i, 1) = "2") Then
                tmp += "๒"
            ElseIf (num.Substring(i, 1) = "3") Then
                tmp += "๓"
            ElseIf (num.Substring(i, 1) = "4") Then
                tmp += "๔"
            ElseIf (num.Substring(i, 1) = "5") Then
                tmp += "๕"
            ElseIf (num.Substring(i, 1) = "6") Then
                tmp += "๖"
            ElseIf (num.Substring(i, 1) = "7") Then
                tmp += "๗"
            ElseIf (num.Substring(i, 1) = "8") Then
                tmp += "๘"
            ElseIf (num.Substring(i, 1) = "9") Then
                tmp += "๙"
            Else
                tmp += num.Substring(i, 1)
            End If

        Next
        Return tmp
    End Function

    Public Function ConvertDate2DBString(ByVal dt As Date) As String
        Dim y As String = ""
        Dim d As String = ""
        Dim m As String = ""
        d = dt.Day.ToString
        m = dt.Month.ToString

        While (d.Length < 2)
            d = ("0" + d)

        End While

        While (m.Length < 2)
            m = ("0" + m)

        End While

        If dt.Year < 2500 Then
            y = (dt.Year + 543).ToString
        Else
            y = dt.Year
        End If

        Return y + m + d
    End Function
    Public Function ConvertStrDate2DBString(ByVal dt As String) As String
        If dt <> "" Then

            Dim y As String = ""
            Dim d As String = ""
            Dim m As String = ""

            Dim str() As String
            str = Split(dt, "/")

            d = str(0).ToString
            m = str(1).ToString


            While (d.Length < 2)
                d = ("0" + d)
            End While

            While (m.Length < 2)
                m = ("0" + m)
            End While

            If CInt(str(2)) < 2500 Then
                y = (CInt(str(2)) + 543).ToString
            Else
                y = str(2)
            End If

            Return y + m + d
        Else
            Return 0
        End If
    End Function

    Public Function DisplayFullDateTH(ByVal dt As Date) As String
        Dim y As String = ""
        Dim d1 As String = ""
        Dim d2 As String = ""
        Dim m As String = ""

        Select Case dt.DayOfWeek
            Case DayOfWeek.Sunday
                d1 = "อาทิตย์"
            Case DayOfWeek.Monday
                d1 = "จันทร์"
            Case DayOfWeek.Tuesday
                d1 = "อังคาร"
            Case DayOfWeek.Wednesday
                d1 = "พุธ"
            Case DayOfWeek.Thursday
                d1 = "พฤหัสบดี"
            Case DayOfWeek.Friday
                d1 = "ศุกร์"
            Case DayOfWeek.Saturday
                d1 = "เสาร์"

        End Select

        d2 = dt.Day.ToString

        Select Case dt.Month
            Case 1 : m = "มกราคม"
            Case 2 : m = "กุมภาพันธ์"
            Case 3 : m = "มีนาคม"
            Case 4 : m = "เมษายน"
            Case 5 : m = "พฤษภาคม"
            Case 6 : m = "มิถุนายน"
            Case 7 : m = "กรกฎาคม"
            Case 8 : m = "สิงหาคม"
            Case 9 : m = "กันยายน"
            Case 10 : m = "ตุลาคม"
            Case 11 : m = "พฤศจิกายน"
            Case 12 : m = "ธันวาคม"
        End Select

        If dt.Year < 2500 Then
            y = (dt.Year + 543).ToString
        Else
            y = dt.Year
        End If

        Return "วัน" + d1 + "ที่ " + d2 + " " + m + " พ.ศ. " + y

    End Function

    Public Function DisplayFullDateTHwithoutDOW(ByVal dt As Date) As String
        Dim y As String = ""
        Dim d2 As String = ""
        Dim m As String = ""
        d2 = dt.Day.ToString

        Select Case dt.Month
            Case 1 : m = "มกราคม"
            Case 2 : m = "กุมภาพันธ์"
            Case 3 : m = "มีนาคม"
            Case 4 : m = "เมษายน"
            Case 5 : m = "พฤษภาคม"
            Case 6 : m = "มิถุนายน"
            Case 7 : m = "กรกฎาคม"
            Case 8 : m = "สิงหาคม"
            Case 9 : m = "กันยายน"
            Case 10 : m = "ตุลาคม"
            Case 11 : m = "พฤศจิกายน"
            Case 12 : m = "ธันวาคม"
        End Select

        If dt.Year < 2500 Then
            y = (dt.Year + 543).ToString
        Else
            y = dt.Year
        End If

        Return "วันที่  " + d2 + "   เดือน  " + m + "   พ.ศ. " + y
    End Function

    Public Function DisplayDateTH(ByVal dt As Date) As String
        Dim y As String = ""
        Dim d2 As String = ""
        Dim m As String = ""
        d2 = dt.Day.ToString
        Select Case dt.Month
            Case 1 : m = "มกราคม"
            Case 2 : m = "กุมภาพันธ์"
            Case 3 : m = "มีนาคม"
            Case 4 : m = "เมษายน"
            Case 5 : m = "พฤษภาคม"
            Case 6 : m = "มิถุนายน"
            Case 7 : m = "กรกฎาคม"
            Case 8 : m = "สิงหาคม"
            Case 9 : m = "กันยายน"
            Case 10 : m = "ตุลาคม"
            Case 11 : m = "พฤศจิกายน"
            Case 12 : m = "ธันวาคม"
        End Select

        If dt.Year < 2500 Then
            y = (dt.Year + 543).ToString
        Else
            y = dt.Year
        End If

        Return d2 + " " + m + " " + y
    End Function

    Public Function Convert2LetterNo(ByVal dt As String) As String
        Dim y As String = ""
        Dim i As String = ""
        y = dt.Substring(0, 4)
        i = dt.Substring(4, 3)

        Return CInt(i) & "/" & y
    End Function


    Public Function DisplayStr2DateTH(ByVal dt As String) As String
        Dim y As String = ""
        Dim d2 As String = ""
        Dim m As String = ""
        d2 = CInt(dt.Substring(6, 2)).ToString
        If (d2 = "0") Then
            d2 = ""
        Else
            d2 = d2
        End If
        m = CInt(dt.Substring(4, 2)).ToString

        Select Case m
            Case "0" : m = ""
            Case "1" : m = "มกราคม"
            Case "2" : m = "กุมภาพันธ์"
            Case "3" : m = "มีนาคม"
            Case "4" : m = "เมษายน"
            Case "5" : m = "พฤษภาคม"
            Case "6" : m = "มิถุนายน"
            Case "7" : m = "กรกฎาคม"
            Case "8" : m = "สิงหาคม"
            Case "9" : m = "กันยายน"
            Case "10" : m = "ตุลาคม"
            Case "11" : m = "พฤศจิกายน"
            Case "12" : m = "ธันวาคม"
        End Select

        y = dt.Substring(0, 4)

        Return d2 + " " + m + " " + y

    End Function

    Public Function DisplayMiniDateTH(ByVal dt As Date) As String
        Dim y As String = ""
        Dim d2 As String = ""
        Dim m As String = ""
        d2 = dt.Day.ToString

        Select Case dt.Month
            Case 1 : m = "ม.ค."
            Case 2 : m = "ก.พ."
            Case 3 : m = "มี.ค."
            Case 4 : m = "เม.ย."
            Case 5 : m = "พ.ค."
            Case 6 : m = "มิ.ค."
            Case 7 : m = "ก.ค."
            Case 8 : m = "ส.ค."
            Case 9 : m = "ก.ย."
            Case 10 : m = "ต.ค."
            Case 11 : m = "พ.ย."
            Case 12 : m = "ธ.ค."
        End Select

        If dt.Year < 2500 Then
            y = (dt.Year + 543).ToString
        Else
            y = dt.Year
        End If

        Return d2 + " " + m + " " + y

    End Function

    Public Function DisplayShortDateTH(ByVal dt As Date) As String
        Dim y As String = ""
        Dim d As String = ""
        Dim m As String = ""
        d = dt.Day.ToString
        m = dt.Month.ToString

        While (d.Length < 2)
            d = ("0" + d)

        End While

        While (m.Length < 2)
            m = ("0" + m)

        End While
        If dt.Year < 2500 Then
            y = (dt.Year + 543).ToString
        Else
            y = dt.Year
        End If
        Return d + "/" + m + "/" + y
    End Function

    Public Function DisplayShortDateEN(ByVal dt As Date) As String
        Dim y As String = ""
        Dim d As String = ""
        Dim m As String = ""

        d = dt.Day.ToString
        m = dt.Month.ToString

        While (d.Length < 2)
            d = ("0" + d)

        End While

        While (m.Length < 2)
            m = ("0" + m)

        End While
        y = dt.Year.ToString
        Return (d + ("/" _
                    + (m + ("/" + y))))
    End Function

    Public Function DisplayStr2ShortDateEN(ByVal dt As String) As String
        Dim dd As String = dt.Substring(6, 2)
        Dim mm As String = dt.Substring(4, 2)
        Dim yy As String = dt.Substring(0, 4)

        While (dd.Length < 2)
            dd = ("0" + dd)

        End While

        While (mm.Length < 2)
            mm = ("0" + mm)

        End While

        Return dd + "/" + mm + "/" + yy
    End Function

    Public Function DisplayStr2ShortDateTH(ByVal dt As String) As String
        If dt <> "" Then
            Dim dd As String = dt.Substring(6, 2)
            Dim mm As String = dt.Substring(4, 2)
            Dim yy As String = dt.Substring(0, 4)

            While (dd.Length < 2)
                dd = ("0" + dd)

            End While

            While (mm.Length < 2)
                mm = ("0" + mm)

            End While
            If CInt(yy) < 2500 Then
                yy = (CInt(yy) + 543).ToString
            End If

            Return dd + "/" + mm + "/" + yy
        Else
            Return ""
        End If

    End Function

    Public Function DisplayTime(ByVal dt As Date) As String
        Dim m As String
        Dim h As String
        h = dt.Hour.ToString
        m = dt.Minute.ToString

        While (m.Length < 2)
            m = ("0" + m)

        End While
        Return (h + ("." _
                    + (m + " .")))
    End Function

    Public Function DisplayPhone(ByVal s As String) As String
        If (s.Length = 9) Then
            If (s.Substring(0, 2) = "02") Then
                Return (s.Substring(0, 2) + ("-" _
                            + (s.Substring(2, 3) + ("-" + s.Substring(5, 4)))))
            Else
                Return (s.Substring(0, 3) + ("-" _
                            + (s.Substring(3, 3) + ("-" + s.Substring(6, 3)))))
            End If
        End If
        If (s.Length = 10) Then
            Return (s.Substring(0, 3) + ("-" _
                        + (s.Substring(3, 3) + ("-" + s.Substring(6, 4)))))
        End If
        Return s
    End Function

    Public Function DisplayUsername(ByVal s As String) As String
        If Not New Regex("^([0-9]{13})$").IsMatch(s) Then
            Return s
        End If
        Return (s.Substring(0, 1) + ("-" _
                    + (s.Substring(1, 4) + ("-" _
                    + (s.Substring(5, 5) + ("-" _
                    + (s.Substring(10, 2) + ("-" + s.Substring(12, 1)))))))))
    End Function

    Public Function DisplayDay(ByVal dt As Date) As String
        Dim d As String = ""
        d = dt.Day.ToString
        Return d
    End Function
    Public Function DisplayNumber2Month(ByVal pM As Integer) As String
        Dim m As String = ""
        Select Case pM
            Case 1 : m = "มกราคม"
            Case 2 : m = "กุมภาพันธ์"
            Case 3 : m = "มีนาคม"
            Case 4 : m = "เมษายน"
            Case 5 : m = "พฤษภาคม"
            Case 6 : m = "มิถุนายน"
            Case 7 : m = "กรกฎาคม"
            Case 8 : m = "สิงหาคม"
            Case 9 : m = "กันยายน"
            Case 10 : m = "ตุลาคม"
            Case 11 : m = "พฤศจิกายน"
            Case 12 : m = "ธันวาคม"
        End Select

        Return m
    End Function
    Public Function DisplayMonth(ByVal dt As Date) As String
        Dim m As String = ""
        Select Case dt.Month
            Case 1 : m = "มกราคม"
            Case 2 : m = "กุมภาพันธ์"
            Case 3 : m = "มีนาคม"
            Case 4 : m = "เมษายน"
            Case 5 : m = "พฤษภาคม"
            Case 6 : m = "มิถุนายน"
            Case 7 : m = "กรกฎาคม"
            Case 8 : m = "สิงหาคม"
            Case 9 : m = "กันยายน"
            Case 10 : m = "ตุลาคม"
            Case 11 : m = "พฤศจิกายน"
            Case 12 : m = "ธันวาคม"
        End Select

        Return m
    End Function
    Public Function DisplayStr2Day(ByVal dt As String) As String
        Dim d As String = ""
        d = Val(dt)
        Return d
    End Function

    Public Function DisplayYear(ByVal dt As Date) As String
        Dim y As String = ""

        If dt.Year < 2500 Then
            y = (dt.Year + 543).ToString
        Else
            y = dt.Year
        End If

        Return y
    End Function

    Public Function ChkNull(ByVal str As String) As String
        If (str <> "") Then
            Return (" " + str)
        Else
            Return " -"
        End If
    End Function


    Public Function TRcolor(ByVal row As Integer) As String
        Dim tr As String = ""
        If ((row Mod 2) _
                    <> 0) Then
            tr = "<tr onMouseOver=\""this.bgColor='#DDDDDD';\"" onMouseOut=\""this.bgColor='#FFFFFF';\"">"
        Else
            tr = "<tr onMouseOver=\""this.bgColor='#DDDDDD';\"" onMouseOut=\""this.bgColor='#FFFFFF';\"">"
        End If
        Return tr
    End Function


End Module
