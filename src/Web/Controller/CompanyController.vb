Imports Microsoft.ApplicationBlocks.Data
Public Class CompanyController

    Inherits BaseClass
    Public ds As New DataSet
    Public Function Company_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Company_Get"))
        Return ds.Tables(0)
    End Function
    Public Function Company_GetByUID(pUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Company_GetByUID"), pUID)
        Return ds.Tables(0)
    End Function
    Public Function Company_GetBySearch(pKey As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Company_GetBySearch"), pKey)
        Return ds.Tables(0)
    End Function
    Public Function Company_GetForSelection() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Company_GetForSelection"))
        Return ds.Tables(0)
    End Function


    Public Function Company_CheckDupicateByCode(ByVal Code As String) As Boolean
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Company_CheckDupicateByCode"), Code)
        If ds.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function Company_Save(ByVal CompanyUID As Integer, ByVal CompanyCode As String, ByVal Name As String, ByVal AliasName As String, ByVal VATID As String, ByVal AddressNumber As String, ByVal Lane As String, ByVal Road As String, ByVal SubDistrict As String, ByVal District As String, ByVal ProvID As Integer, ByVal ProvinceName As String, ByVal ZipCode As String, ByVal Country As String, ByVal Telephone As String, ByVal Fax As String, ByVal Email As String, ByVal Website As String, status As String, ByVal UpdBy As Integer, OwnerName As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Company_Save"), CompanyUID, CompanyCode, Name, AliasName, VATID, AddressNumber, Lane, Road, SubDistrict, District, ProvID, ProvinceName, ZipCode, Country, Telephone, Fax, Email, Website, status, UpdBy, OwnerName)

    End Function
    Public Function Company_Delete(ByVal CompanyUID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Company_Delete"), CompanyUID)
    End Function

    Public Function Company_CheckDup(ByVal CompanyCode As String) As Boolean
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Company_CheckDup"), CompanyCode)
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
    Public Function Company_GetForReport() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Company_GetForReport"))
        Return ds.Tables(0)
    End Function
End Class
