Imports Microsoft.ApplicationBlocks.Data

Public Class BOCController
    Inherits BaseClass

#Region "BOC List"
    Public Function BOC_GetAll() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("BOC_GetAll"))
        Return ds.Tables(0)
    End Function
    Public Function BOC_GetLast() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("BOC_GetLast"))
        Return ds.Tables(0)
    End Function
    Public Function BOC_GetByUID(pSearch As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("BOC_GetByUID"), pSearch)
        Return ds.Tables(0)
    End Function
    Public Function BOC_GetBySearch(pSearch As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("BOC_GetBySearch"), pSearch)
        Return ds.Tables(0)
    End Function
    Public Function BOC_Delete(pCode As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("BOC_Delete"), pCode)
    End Function
    Public Function BOC_Save(RTWNO As String, RTWDate As String, RTWREASON As Integer, RTWRSUID As Integer, PersonUID As Integer, PersonCode As String, Remark As String, CUser As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("BOC_Save"), RTWNO, RTWDate, RTWREASON, RTWRSUID, PersonUID, PersonCode, Remark, CUser)
    End Function

#End Region
End Class