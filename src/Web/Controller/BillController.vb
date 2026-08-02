Imports Microsoft.ApplicationBlocks.Data
Public Class BillController
    Inherits BaseClass
    Public ds As New DataSet


    Public Function Bill_GetByDate(Bdate As String, Edate As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Bill_GetByDate"), Bdate, Edate)
        Return ds.Tables(0)
    End Function

    Public Function Bill_Add(ACCNO As String, Descriptions As String, CreateDate As String, BillNo As String, dr As Double, cr As Double, Chk As String, remark As String, bankno As String, CreateBy As String, UpdBy As String, LASTUPDATE As String, payType As String, CostNo As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Bill_Add"), ACCNO, Descriptions, CreateDate, BillNo, dr, cr, Chk, remark, bankno, CreateBy, UpdBy, LASTUPDATE, payType, CostNo)
    End Function

    Public Function Bill_Update(ACCNO As String, Descriptions As String, CreateDate As String, BillNo As String, dr As Double, cr As Double, Chk As String, remark As String, bankno As String, CreateBy As String, UpdBy As String, LASTUPDATE As String, pID As Integer, payType As String, CostNo As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Bill_Update"), ACCNO, Descriptions, CreateDate, BillNo, dr, cr, Chk, remark, bankno, CreateBy, UpdBy, LASTUPDATE, pID, payType, CostNo)
    End Function

    Public Function Bill_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Bill_Get"))
        Return ds.Tables(0)
    End Function
    Public Function Bill_GetByUID(PUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Bill_GetByUID"), PUID)
        Return ds.Tables(0)
    End Function

    Public Function BillDetail_ChkDupBillReceipt(ByVal BillRef As String, CompCode As String, SendDate As String) As Boolean
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("BillDetail_ChkDupBillReceipt"), BillRef, CompCode, SendDate)
        If ds.Tables(0).Rows(0)(0) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function BillDetail_ChkDupBillDeduct(ByVal BillRef As String, CompCode As String) As Boolean
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("BillDetail_ChkDupBillDeduct"), BillRef, CompCode)
        If ds.Tables(0).Rows(0)(0) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function


    Public Function Bill_Add(BillNumber As String _
         , BillDate As String _
         , CompanyCode As String _
         , CustomerID As String _
         , TotalWeight As Double _
         , TotalNetPrice As Double _
         , TotalDeduct As Double _
         , Balance As Double _
         , Remark As String _
         , Cuser As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Bill_Save"), BillNumber _
         , StrNull2Zero(BillDate) _
         , CompanyCode _
         , CustomerID _
         , TotalWeight _
         , TotalNetPrice _
         , TotalDeduct _
         , Balance _
         , Remark _
         , Cuser)
    End Function
    Public Function BillDetail_GetByBillNumber(BillNumber As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("BillDetail_GetByBillNumber"), BillNumber)
        Return ds.Tables(0)
    End Function
    Public Function BillDetail_Add(ByVal BillNumber As String, ByVal BillReference As String, ByVal CarRegisNumber As String, ByVal CaneTypeUID As Integer, ByVal Weight As Double, ByVal UnitPrice As Double, ByVal NetPrice As Double, ByVal GasPrice As Double, ByVal NetBalance As Double, ByVal BillFlag As String, ByVal Remark As String, ByVal StatusFlag As String, ByVal CUser As Integer, ByVal ACCID As Integer, SendDate As Integer) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("BillDetail_Add"), BillNumber, BillReference, CarRegisNumber, CaneTypeUID, Weight, UnitPrice, NetPrice, GasPrice, NetBalance, BillFlag, Remark, StatusFlag, CUser, ACCID, SendDate)
    End Function
    Public Function BillDetail_DeleteByBillNumber(ByVal BillNumber As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("BillDetail_DeleteByBillNumber"), BillNumber)
    End Function

    Public Function BillDeduction_GetByBillNumber(BillNumber As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("BillDeduction_GetByBillNumber"), BillNumber)
        Return ds.Tables(0)
    End Function

    Public Function BillDeduct_Add(ByVal BillNumber As String, ByVal BillReference As String, ByVal ACCID As Integer, ByVal NetPrice As Double, ByVal CUser As Integer) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("BillDetail_Add"), BillNumber, BillReference, ACCID, NetPrice, CUser)
    End Function
    Public Function Bill_Delete(pBillNumber As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Bill_Delete"), pBillNumber)

    End Function





End Class

