Imports Microsoft.ApplicationBlocks.Data
Public Class CashController
    Inherits BaseClass
    Public ds As DataSet = New DataSet

    Public Function GetCash() As DataSet
        Return SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Cash_GetAll"))
    End Function
     
    Public Function GetCash_ByDate(ByVal pKey As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Cash_GetBydate"), pKey)
        Return ds.Tables(0)
    End Function

    Public Function GetCash_4Print(ByVal pdate As String, pbill As String, pacc As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("GetCash_4Print"), pdate, pbill, pacc)
        Return ds.Tables(0)
    End Function

    Public Function Cash_GetLastBalance(accno As String) As Double

        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Cash_GetLastBalance"), accno)
        If ds.Tables(0).Rows.Count > 0 Then
            Return chkNullByDouble(String.Concat(ds.Tables(0).Rows(0)(0)))
        Else
            Return 0
        End If

    End Function

    Public Function GetCash_BySearch(ByVal LskBrk As String, ByVal pCond As String) As DataSet
        Return SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Cash_Search"), LskBrk, pCond)
    End Function
     

    Public Function Cash_Add(CashDate As String, ACCID As String, Descrp As String, DR As Double, CR As Double, ACC_Balance As Double, LASTUPDATE As String, SEQNO As Integer, CreateDate As String, CreateBy As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Cash_Add"), CashDate, ACCID, Descrp, DR, CR, ACC_Balance, LASTUPDATE, SEQNO, CreateDate, CreateBy)
    End Function

    Public Function Cash_Delete(CashDate As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Cash_Delete"), CashDate)
    End Function

    Public Function CashNote_GetByACCNO(ACCID As String, sdate As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CashNote_GetByACCNO"), ACCID, sdate)

        Return ds.Tables(0)
    End Function

    Public Function CashNote_Add(NoteDate As String, ACCID As String, Descrp As String, ACC_Balance As Double, LASTUPDATE As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("CashNote_Add"), NoteDate, ACCID, Descrp, ACC_Balance, LASTUPDATE)
    End Function
    Public Function CashNote_Delete(NoteDate As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("CashNote_Delete"), NoteDate)
    End Function

End Class
 
