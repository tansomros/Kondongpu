Imports Microsoft.ApplicationBlocks.Data

Public Class ProvinceController
    Inherits BaseClass
    Public Function LoadProvinceNameByID(ByVal pID As Integer) As String
        Dim dt As New DataTable
        SQL = " select ProvinceName From PROVINCE Where ProvinceID=" & pID
        dt = ExecuteQuery(SQL)
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0)(0)
        Else
            Return "นครราชสีมา"
        End If

    End Function

    Public Function GetProvince() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Province_Get"))
        Return ds.Tables(0)

    End Function
End Class
