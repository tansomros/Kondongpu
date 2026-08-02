Imports Microsoft.ApplicationBlocks.Data

Public Class PriceController
    Inherits BaseClass

#Region "Pric "
    Public Function Price_GetAll() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Price_Get"))
        Return ds.Tables(0)
    End Function
    Public Function Price_GetLast() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Price_GetLast"))
        Return ds.Tables(0)
    End Function
    Public Function Price_GetByUID(pSearch As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Price_GetByUID"), pSearch)
        Return ds.Tables(0)
    End Function
    Public Function Price_GetForSale(CompanyCode As String, CaneType As Integer, SaleDate As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Price_GetForSale"), CompanyCode, CaneType, SaleDate)
        Return ds.Tables(0)
    End Function
    Public Function Price_GetBySearch(Year As String, StartDate As String, EndDate As String, CaneType As String, CompanyUID As String, Status As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Price_GetBySearch"), Year, StartDate, EndDate, CaneType, CompanyUID, Status)
        Return ds.Tables(0)
    End Function
    Public Function Price_Delete(pCode As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Price_Delete"), pCode)
    End Function
    Public Function Price_Save(UID As Integer, Year As Integer, CompanyUID As Integer, CaneUID As Integer, StartDate As String, EndDate As String, UnitPrice As Double, StatusFlag As String, CUser As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Price_Save"), UID, Year, CompanyUID, CaneUID, StartDate, EndDate, UnitPrice, StatusFlag, CUser)
    End Function
    Public Function Price_CheckDuplicate(CompanyCode As String, StartDate As String, EndDate As String, CaneType As Integer) As Boolean
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Price_CheckDuplicate"), CompanyCode, StartDate, EndDate, CaneType)
        If ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0).Rows(0)(0) > 0 Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If

    End Function
    Public Function Price_GetYear() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Price_GetYear"))
        Return ds.Tables(0)
    End Function

#End Region
End Class