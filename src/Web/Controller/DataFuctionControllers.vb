'Imports Microsoft.ApplicationBlocks.Data
Public Class DataFuctionControllers
    Public Shared Function IsDate(argo As Object) As Boolean
        'ตรวจสอบข้อมูลประเภท Date
        Try
            Dim dt As DateTime = DateTime.Parse(argo.ToString())
            If dt <> DateTime.MinValue AndAlso dt <> DateTime.MaxValue Then
                Return True
            End If
            Return False
        Catch
            Return False
        End Try
    End Function
    Public Shared Function IsNumeric(argo As Object) As Boolean
        'ตรวจสอบข้อมูลปรเภท ตัวเลข
        Try
            Dim result As Single = Single.Parse(argo.ToString())
            Return True
        Catch
            Return False
        End Try
    End Function
    Public Shared Function ClearControl(Ctrl As Control) As Boolean
        While Ctrl.Controls.Count > 0
            For Each u As UserControl In Ctrl.Controls
                u.Dispose()
            Next
        End While
        Return True
    End Function

End Class
