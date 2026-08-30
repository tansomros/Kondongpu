Imports System
Imports DevExpress.XtraReports.Expressions

Public Class ThaiBahtFunction
    Inherits ReportCustomFunctionOperatorBase

    Public Overrides ReadOnly Property Name As String
        Get
            Return "ThaiBaht"
        End Get
    End Property

    Public Overrides ReadOnly Property Description As String
        Get
            Return "แปลงจำนวนเงินเป็นตัวอักษรภาษาไทย"
        End Get
    End Property

    Public Overrides ReadOnly Property FunctionCategory As String
        Get
            Return "Custom"
        End Get
    End Property

    Public Overrides ReadOnly Property MinOperandCount As Integer
        Get
            Return 1
        End Get
    End Property

    Public Overrides ReadOnly Property MaxOperandCount As Integer
        Get
            Return 1
        End Get
    End Property

    Public Overrides Function IsValidOperandCount(count As Integer) As Boolean
        Return count = 1
    End Function

    Public Overrides Function IsValidOperandType(
        operandIndex As Integer,
        operandCount As Integer,
        type As Type
    ) As Boolean

        Return True
    End Function

    Public Overrides Function Evaluate(
        ParamArray operands() As Object
    ) As Object

        If operands Is Nothing OrElse
           operands.Length = 0 OrElse
           operands(0) Is Nothing OrElse
           Convert.IsDBNull(operands(0)) Then

            Return ""
        End If

        Dim amount As Decimal

        If Not Decimal.TryParse(
            operands(0).ToString(),
            amount
        ) Then

            Return ""
        End If

        Return ThaiBahtText(amount)

    End Function

    Public Overrides Function ResultType(
        ParamArray operands() As Type
    ) As Type

        Return GetType(String)

    End Function

    Private Function ThaiBahtText(amount As Decimal) As String

        amount = Math.Round(
            amount,
            2,
            MidpointRounding.AwayFromZero
        )

        If amount = 0D Then
            Return "ศูนย์บาทถ้วน"
        End If

        If amount < 0D Then
            Return "ลบ" & ThaiBahtText(Math.Abs(amount))
        End If

        Dim integerPart As Long =
            Convert.ToInt64(Math.Truncate(amount))

        Dim satang As Integer =
            Convert.ToInt32(
                Math.Round(
                    (amount - Math.Truncate(amount)) * 100D,
                    0,
                    MidpointRounding.AwayFromZero
                )
            )

        If satang = 100 Then
            integerPart += 1
            satang = 0
        End If

        Dim result As String =
            NumberToThaiText(integerPart) & "บาท"

        If satang = 0 Then
            result &= "ถ้วน"
        Else
            result &= NumberToThaiText(satang) & "สตางค์"
        End If

        Return result

    End Function

    Private Function NumberToThaiText(number As Long) As String

        If number = 0 Then
            Return "ศูนย์"
        End If

        If number >= 1000000 Then

            Dim million As Long = number \ 1000000
            Dim remainder As Long = number Mod 1000000

            Dim result As String =
                NumberToThaiText(million) & "ล้าน"

            If remainder > 0 Then
                result &= NumberToThaiTextBelowMillion(remainder)
            End If

            Return result

        End If

        Return NumberToThaiTextBelowMillion(number)

    End Function

    Private Function NumberToThaiTextBelowMillion(
        number As Long
    ) As String

        Dim numbers() As String = {
            "ศูนย์", "หนึ่ง", "สอง", "สาม", "สี่",
            "ห้า", "หก", "เจ็ด", "แปด", "เก้า"
        }

        Dim positions() As String = {
            "", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน"
        }

        Dim result As String = ""
        Dim digits As String = number.ToString()

        For i As Integer = 0 To digits.Length - 1

            Dim digit As Integer =
                Integer.Parse(digits.Substring(i, 1))

            Dim position As Integer =
                digits.Length - i - 1

            If digit = 0 Then Continue For

            If position = 0 Then

                If digit = 1 AndAlso digits.Length > 1 Then
                    result &= "เอ็ด"
                Else
                    result &= numbers(digit)
                End If

            ElseIf position = 1 Then

                If digit = 1 Then
                    result &= "สิบ"
                ElseIf digit = 2 Then
                    result &= "ยี่สิบ"
                Else
                    result &= numbers(digit) & "สิบ"
                End If

            Else

                result &= numbers(digit)
                result &= positions(position)

            End If

        Next

        Return result

    End Function

End Class