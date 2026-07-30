Imports Microsoft.ApplicationBlocks.Data
Public Class ReceiptController
    Inherits BaseClass
    Public ds As DataSet = New DataSet

    Public Function Receipt_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Receipt_Get"))
        Return ds.Tables(0)
    End Function

    Public Function Receipt_GetByID(ByVal pKey As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Receipt_GetByID"), pKey)
        Return ds.Tables(0)
    End Function

    Public Function Receipt_GetByZone(ByVal pKey As String) As DataSet
        Return SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Receipt_GetByZone"), pKey)
    End Function
    Public Function Receipt_GetByType(ByVal pKey As String) As DataSet
        Return SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Receipt_GetByType"), pKey)
    End Function
    Public Function Receipt_GetByDate(ByVal pKey As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Receipt_GetByDate"), pKey)
        Return ds.Tables(0)
    End Function

    Public Function Receipt_GetBySearch(ByVal pCond As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Receipt_GetSearch"), pCond)
        Return ds.Tables(0)
    End Function

    Public Function Receipt_GetChkDup(ByVal pID As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Receipt_ChkDup"), pID)
        Return ds.Tables(0)
    End Function

    Public Function Receipt_Add(ACCNO As String, BillNo As String, CreateDate As String, remark As String, ACCName As String, CreateBy As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Receipt_Add"), ACCNO, BillNo, CreateDate, remark, ACCName, CreateBy)

    End Function

    Public Function Receipt_Update(ACCNO As String, BillNo As String, CreateDate As String, remark As String, ACCName As String, CreateBy As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Receipt_Update"), ACCNO, BillNo, CreateDate, remark, ACCName, CreateBy)

    End Function

    Public Function Receipt_Delete(pID As Integer) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Receipt_Delete"), pID)

    End Function
End Class

