Imports Microsoft.ApplicationBlocks.Data
Public Class clsConfigData
    Inherits BaseClass
    Public ds As New DataSet

    Public Function DataConfig_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("DataConfig_Get"))
        Return ds.Tables(0)
    End Function
    Public Function DataConfig_GetByCode(ByVal pid As String) As String
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("DataConfig_GetByCode"), pid)
        Return DBNull2Str(ds.Tables(0).Rows(0)(0))
    End Function

    Public Function DataConfig_Add(ByVal pCode As String, ByVal pYear As String, ByVal pNo As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("DataConfig_Add"), pCode, pYear, pNo)
    End Function
    Public Function DataConfig_Update(ByVal pCode As String, ByVal pYear As String, ByVal pNo As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("DataConfig_Update"), pCode, pYear, pNo)
    End Function


    Public Function Running_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Running_Get"))
        Return ds.Tables(0)
    End Function

    Public Function Running_GetByCode(ByVal pid As String, ByVal pYear As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Running_GetByCode"), pid, pYear)
        Return ds.Tables(0)
    End Function

    Public Function Running_Add(ByVal pCode As String, ByVal pYear As Integer, ByVal pNo As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Running_Add"), pCode, pYear, pNo)
    End Function
    Public Function Running_Update(ByVal pCode As String, ByVal pYear As Integer, ByVal pNo As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Running_Update"), pCode, pYear, pNo)
    End Function

    Public Function Running_Delete(ByVal pCode As String, ByVal pYear As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Running_Delete"), pCode, pYear)
    End Function


    Public Function YearPermit_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("YearPermit"))
        Return ds.Tables(0)
    End Function

End Class
