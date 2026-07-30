Imports Microsoft.ApplicationBlocks.Data
Public Class UserAccountController

    Inherits BaseClass

    Public ds As New DataSet
    Public Function User_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("User_Get"))
        Return ds.Tables(0)
    End Function

    Public Function User_Login(ByVal pPassword As String) As DataTable
        Dim enc As New CryptographyEngine
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("User_Login"), pPassword)
        Return ds.Tables(0)
    End Function
    Public Function User_GetByProjectID(ByVal pID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("User_GetByLocationID"), pID)
        Return ds.Tables(0)
    End Function
    Public Function User_GetByID(ByVal pID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("User_GetByID"), pID)
        Return ds.Tables(0)
    End Function

    Public Function User_GetName(ByVal username As String) As String
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("User_GetName"), username)
        Return ds.Tables(0).Rows(0)(0)
    End Function

    'Public Function User_GetActionByID(ByVal pID As String) As String
    '    ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("User_GetActionByID"), pID)
    '    Return ds.Tables(0).Rows(0)(0)
    'End Function


    Public Function User_LastLog_Update(ByVal pUsername As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("User_LastLog_Update"), pUsername)
    End Function
    Public Function User_Delete(ByVal pID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("User_Delete"), pID)
    End Function

    Public Function User_Add(ByVal Name As String, ByVal Password As String, ByVal PosName As String, ByVal UserRule As String, ByVal Status As Integer, ByVal UpdBy As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("User_Add"), Name, Password, PosName, UserRule, Status, UpdBy)
    End Function

    Public Function User_Update(ByVal UID As Integer, ByVal Name As String, ByVal Password As String, ByVal PosName As String, ByVal UserRule As String, ByVal Status As Integer, ByVal UpdBy As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("User_Update"), UID, Name, Password, PosName, UserRule, Status, UpdBy)
    End Function

    Public Function User_CheckDuplicate(ByVal pUser As String) As Boolean
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("User_CheckDuplicate"), pUser)

        If ds.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function


End Class

Public Class UserAction
    Inherits BaseClass
    Public ds As New DataSet
    Public Function UserAction_GetByUser(ByVal pUser As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("UserAction_GetByUser"), pUser)
        Return ds.Tables(0)
    End Function
    Public Function UserAction_CheckByUser(ByVal pUser As String, ByVal pLocation As Integer) As Boolean
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("UserAction_CheckByUser"), pUser, pLocation)

        If ds.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function UserAction_Checked(ByVal pUser As String, ByVal pLocation As Integer) As Boolean
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("UserAction_Checked"), pUser, pLocation)

        If ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0).Rows(0)(0) = 1 Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function




    Public Function UserAction_Add(ByVal Username As String, ByVal LocationID As Integer, ByVal RoleAction As Integer, ByVal UpdBy As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("UserAction_Add"), Username, LocationID, RoleAction, UpdBy)
    End Function
    Public Function UserAction_Update(ByVal Username As String, ByVal LocationID As Integer, ByVal RoleAction As Integer, ByVal UpdBy As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("UserAction_Update"), Username, LocationID, RoleAction, UpdBy)
    End Function


End Class
Public Class UserPage
    Inherits BaseClass

    Public ds As New DataSet
    Public Function UserPage_GetByID(ByVal pID As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("UserPage_GetByID"), pID)
        Return ds.Tables(0)
    End Function
    Public Function UserPage_GetCountPage() As Integer
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("UserPage_GetCountPage"))
        Return ds.Tables(0).Rows(0)(0)
    End Function
    Public Function UserPage_CheckByUser(ByVal pUser As String, ByVal pLocation As Integer) As Boolean
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("UserPage_CheckByUser"), pUser, pLocation)

        If ds.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function UserPage_Checked(ByVal pUser As String, ByVal pPageID As Integer) As Boolean
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("UserPage_Checked"), pUser, pPageID)

        If ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0).Rows(0)(0) = 1 Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function



    Public Function UserPage_Add(ByVal Username As String, ByVal LocationID As Integer, ByVal RoleAction As Integer, ByVal UpdBy As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("UserPage_Add"), Username, LocationID, RoleAction, UpdBy)
    End Function
    Public Function UserPage_Update(ByVal Username As String, ByVal LocationID As Integer, ByVal RoleAction As Integer, ByVal UpdBy As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("UserPage_Update"), Username, LocationID, RoleAction, UpdBy)
    End Function



End Class


