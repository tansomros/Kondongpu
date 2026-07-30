Imports Microsoft.ApplicationBlocks.Data
Public Class AccountController
    Inherits BaseClass

#Region "Account"

    Public Function Account_GetAll() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Account_GetAll"))
        Return ds.Tables(0)
    End Function

    Public Function Account_GetByID(pUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Account_GetByID"), pUID)
        Return ds.Tables(0)
    End Function

    Public Function Account_Save(ByVal AccountCode As String, ByVal Name As String, ByVal Remark As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Account_Save"), AccountCode, Name, Remark)

    End Function

    Public Function Account_Delete(ByVal AccountUID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Account_Delete"), AccountUID)
    End Function

    Public Function Account_GetName(pID As String) As String
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Account_GetName"), StrNull2Zero(pID))
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0)(0)
        Else
            Return "ไม่มีบัญชีนี้อยู่ในระบบ"
        End If
    End Function
    Public Function Account_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Account_Get"))
        Return ds.Tables(0)
    End Function

    Public Function Account_GetBySearch(pKey As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Account_GetBySearch"), pKey)
        Return ds.Tables(0)
    End Function

    Public Function Account_CheckDuplicate(ByVal name As String) As Integer
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Account_CheckDuplicate"), name)
        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function
#End Region
End Class