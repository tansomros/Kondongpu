Imports Microsoft.ApplicationBlocks.Data

Public Class FirstAidRoomController
    Inherits BaseClass

#Region "FRR"
    Public Function FirstAidRoom_GetAll() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("FirstAidRoom_GetAll"))
        Return ds.Tables(0)
    End Function
    Public Function FirstAidRoom_GetLast() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("FirstAidRoom_GetLast"))
        Return ds.Tables(0)
    End Function
    Public Function FirstAidRoom_GetByUID(pSearch As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("FirstAidRoom_GetByUID"), pSearch)
        Return ds.Tables(0)
    End Function
    Public Function FirstAidRoom_GetBySearch(pSearch As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("FirstAidRoom_GetBySearch"), pSearch)
        Return ds.Tables(0)
    End Function
    Public Function FirstAidRoom_Delete(pCode As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("FirstAidRoom_Delete"), pCode)
    End Function
    Public Function FirstAidRoom_Save(FRRNO As String, FRRDate As String, PersonUID As Integer, FRRTYPE As Integer, DiagnosisCode As String, ReferUID As Integer, MedicalUID As Integer, Remark As String, CUser As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("FirstAidRoom_Save"), FRRNO, FRRDate, PersonUID, FRRTYPE, DiagnosisCode, ReferUID, MedicalUID, Remark, CUser)
    End Function

#End Region
End Class