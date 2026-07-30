Imports Microsoft.ApplicationBlocks.Data

Public Class AreaController
    Inherits BaseClass

    Public Function GetArea() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Area_Get"))
        Return ds.Tables(0)
    End Function


    Public Function GetAreaName(pCode As String) As String
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Area_GetName"), pCode)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0)(0).ToString
        Else
            Return ""
        End If

    End Function

    Public Function GetAreaByID(ByVal pID As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Area_GetByID"), pID)
        Return ds.Tables(0)
    End Function
    Public Function GetAreaBySearch(ByVal pID As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Area_Search"), pID)
        Return ds.Tables(0)
    End Function
    Public Function SaveArea(ByVal pID As String, ByVal pName As String) As Integer
        If isAdd Then
            Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Area_Add"), pID, pName)
        Else
            Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Area_Update"), pID, pName)
        End If

    End Function
    Public Function DeleteArea(ByVal pID As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Area_Delete"), pID)

    End Function
End Class

Public Class ZoneController
    Inherits BaseClass

    Public Function GetZone() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Zone_Get"))
        Return ds.Tables(0)
    End Function

    Public Function GetZoneName(pCode As String) As String
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Zone_GetName"), pCode)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0)(0).ToString
        Else
            Return ""
        End If

    End Function


    Public Function GetZoneByID(ByVal pID As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Zone_GetByID"), pID)
        Return ds.Tables(0)
    End Function
    Public Function GetZoneBySearch(ByVal pID As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Zone_Search"), pID)
        Return ds.Tables(0)
    End Function
    Public Function SaveZone(ByVal pID As String, ByVal pName As String) As Integer
        If isAdd Then
            Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Zone_Add"), pID, pName)
        Else
            Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Zone_Update"), pID, pName)
        End If

    End Function
    Public Function DeleteZone(ByVal pID As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Zone_Delete"), pID)

    End Function
End Class
