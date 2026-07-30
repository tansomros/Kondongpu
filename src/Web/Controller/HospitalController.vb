Imports Microsoft.ApplicationBlocks.Data
Public Class HospitalController

    Inherits BaseClass
    Public ds As New DataSet
    Public Function Hospital_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Hospital_Get"))
        Return ds.Tables(0)
    End Function
    Public Function Hospital_GetByUID(pUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Hospital_GetByUID"), pUID)
        Return ds.Tables(0)
    End Function
    Public Function Hospital_GetBySearch(pKey As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Hospital_GetBySearch"), pKey)
        Return ds.Tables(0)
    End Function
    Public Function Hospital_GetByDefinitionUID(pUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Hospital_GetByDefinitionUID"), pUID)
        Return ds.Tables(0)
    End Function

    Public Function Hospital_Add(ByVal HospitalCode As String, ByVal Name As String, ByVal AddressNumber As String, ByVal Lane As String, ByVal Road As String, ByVal SubDistrict As String, ByVal District As String, ByVal ProvID As Integer, ByVal ProvinceName As String, ByVal ZipCode As String, ByVal Telephone As String, ByVal Fax As String, Status As String, ByVal UpdBy As Integer) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Hospital_Add"), HospitalCode, Name, AddressNumber, Lane, Road, SubDistrict, District, ProvID, ProvinceName, ZipCode, Telephone, Fax, Status, UpdBy)

    End Function
    Public Function Hospital_Update(ByVal HospitalUID As Integer, ByVal HospitalCode As String, ByVal Name As String, ByVal AddressNumber As String, ByVal Lane As String, ByVal Road As String, ByVal SubDistrict As String, ByVal District As String, ByVal ProvID As Integer, ByVal ProvinceName As String, ByVal ZipCode As String, ByVal Telephone As String, ByVal Fax As String, Status As String, ByVal UpdBy As Integer) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Hospital_Update"), HospitalUID, HospitalCode, Name, AddressNumber, Lane, Road, SubDistrict, District, ProvID, ProvinceName, ZipCode, Telephone, Fax, Status, UpdBy)

    End Function
    Public Function Hospital_Delete(ByVal HospitalUID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Hospital_Delete"), HospitalUID)
    End Function

End Class
