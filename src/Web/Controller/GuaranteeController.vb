Imports Microsoft.ApplicationBlocks.Data

Public Class GuaranteeController
    Inherits BaseClass
    Public ds As New DataSet

#Region "Guarantee"
    Public Function Guarantee_Get(AgreementNo As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Guarantee_Get"), AgreementNo)
        Return ds.Tables(0)
    End Function
    Public Function Guarantee_GetBySearch(KeySearch As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Guarantee_GetBySearch"), KeySearch)
        Return ds.Tables(0)
    End Function

    Public Function Guarantee_GetByUID(UID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Guarantee_GetByUID"), UID)
        Return ds.Tables(0)
    End Function
    Public Function Guarantee_Delete(pID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Guarantee_Delete", pID)
    End Function
    Public Function Guarantee_Save(GuaranteeUID As Integer, RequestUID As Integer, LocationUID As Integer, FilePath As String, UpdBy As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Guarantee_Save"), GuaranteeUID, RequestUID, LocationUID, FilePath, UpdBy)
    End Function
    Public Function Guarantee_Add(AgreementNo As String, Desc As String, UpdBy As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Guarantee_Add"), AgreementNo, Desc, UpdBy)
    End Function
#End Region
End Class
