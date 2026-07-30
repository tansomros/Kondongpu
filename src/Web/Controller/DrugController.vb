Imports Microsoft.ApplicationBlocks.Data
Public Class DrugController
    Inherits BaseClass
    Public ds As New DataSet
    Public Function DrugMaster_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("DrugMaster_Get"))
        Return ds.Tables(0)
    End Function
    Public Function DrugMaster_GetByCode(pCode As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("DrugMaster_GetByUID"), pCode)
        Return ds.Tables(0)
    End Function
    Public Function DrugMaster_GetBySearch(pKey As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("DrugMaster_GetBySearch"), pKey)
        Return ds.Tables(0)
    End Function
    Public Function DrugMaster_GetByStatus(pKey As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("DrugMaster_GetByStatus"), pKey)
        Return ds.Tables(0)
    End Function
    Public Function DrugMaster_Add(ByVal Code As String, ByVal Name As String, ByVal AliasName As String, ByVal UOM As String, Sort As Integer, Status As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("DrugMaster_Add"), Code, Name, AliasName, UOM, Sort, Status)

    End Function
    Public Function DrugMaster_Update(ByVal Code As String, ByVal Name As String, ByVal AliasName As String, ByVal UOM As String, Sort As Integer, Status As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("DrugMaster_Update"), Code, Name, AliasName, UOM, Sort, Status)

    End Function
    Public Function DrugMaster_Delete(ByVal Code As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("DrugMaster_Delete"), Code)
    End Function

#Region "Drug Profile"
    Public Function DrugProfile_Add(ByVal FRRNO As String, ByVal EmployeeCode As String, ByVal ItemCode As String, ByVal QTY As Integer, Cuser As Integer) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("DrugProfile_Add"), FRRNO, EmployeeCode, ItemCode, QTY, Cuser)



    End Function
#End Region
End Class
