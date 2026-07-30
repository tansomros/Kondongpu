Imports Microsoft.ApplicationBlocks.Data
Public Class CostBookController
    Inherits BaseClass
    Public ds As DataSet = New DataSet

    Public Function GetCostBook() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CostBook_GetAll"))
        Return ds.Tables(0)
    End Function

    Public Function GetCostBook_ByID(ByVal pKey As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CostBook_GetByID"), pKey)
        Return ds.Tables(0)
    End Function

    Public Function GetCostBook_ByZone(ByVal pKey As String) As DataSet
        Return SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CostBook_GetByZone"), pKey)
    End Function
    Public Function GetCostBook_ByType(ByVal pKey As String) As DataSet
        Return SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CostBook_GetByType"), pKey)
    End Function
    Public Function GetCostBook_ByDate(ByVal pKey As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CostBook_GetByDate"), pKey)
        Return ds.Tables(0)
    End Function
    Public Function CostBook_GetTotalSumCostBookByMasterID(pDate As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CostBook_GetTotalSumCostBookByMasterID"), pDate)
        Return ds.Tables(0)
    End Function
    Public Function GetCostBook_BySearch(ByVal pCond As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CostBook_Search"), pCond)
        Return ds.Tables(0)
    End Function

    Public Function GetCostBook_ChkDup(ByVal pID As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CostBook_ChkDup"), pID)
        Return ds.Tables(0)
    End Function

    Public Function CostBook_Add(CostNO As String, ACCNO As String, Descriptions As String, CostDate As String, BillNo As String, dr As Double, cr As Double, payType As String, CreateBy As String, UpdBy As String, UpdDate As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("CostBook_Add"), CostNO, ACCNO, Descriptions, CostDate, BillNo, dr, cr, payType, CreateBy, UpdBy, UpdDate)
    End Function

    Public Function CostBook_Update(itemid As Integer, CostNo As String, ACCNO As String, Descriptions As String, CreateDate As String, BillNo As String, dr As Double, cr As Double, payType As String, CreateBy As String, UpdBy As String, LASTUPDATE As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("CostBook_Update"), itemid, CostNo, ACCNO, Descriptions, CreateDate, BillNo, dr, cr, payType, CreateBy, UpdBy, LASTUPDATE)

    End Function

    Public Function CostBook_Delete(pID As Integer) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("CostBook_Delete"), pID)

    End Function

    Public Function GL_Delete(pID As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("GL_Delete"), pID)

    End Function



    Public Function GL_Add(ACCNO As String, ACCNAME As String, dr As Double, cr As Double, CreateDate As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("GL_Temp_Add"), ACCNO, ACCNAME, dr, cr, CreateDate)

    End Function



End Class

