Imports Microsoft.ApplicationBlocks.Data
Public Class Zone
    Inherits DBClass
    Private Const intFieldNum As Integer = 2 'กำหนด จำนวนฟิลด์ที่มี
    Public Enum fldPos As Integer 'ประกาศตำแหน่งของฟิลด์
        f00_ZoneCode = 0
        f01_ZoneName = 1

    End Enum

    Sub init() 'ใส่ชื่อ table, field name และ ค่า default
        strTableName = "ZONE"
        intTotalField = intFieldNum


        ReDim tblField(intFieldNum)
        tblField.Clear(tblField, 0, tblField.Length)
        tblField(0).fldName = "ZoneCode"
        tblField(0).fldValue = "0"
        tblField(1).fldName = "ZoneName"
        tblField(1).fldValue = ""

    End Sub

    Sub New()
        MyBase.New()
        Me.init()

    End Sub

#Region "Property"

    Public Property ZoneCode() As Integer
        Get
            Return tblField(0).fldValue
        End Get
        Set(ByVal Value As Integer)
            tblField(0).fldValue = Value
            tblField(0).fldAffect = False
        End Set
    End Property
    Public Property ZoneName() As String
        Get
            Return tblField(1).fldValue
        End Get
        Set(ByVal Value As String)
            tblField(1).fldValue = Value
            tblField(1).fldAffect = True
        End Set
    End Property
   

#End Region

    Public ReadOnly Property TableName() As String
        Get
            Return strTableName
        End Get
    End Property
    Public Function GetZone() As DataSet
        Return SqlHelper.ExecuteDataset(ConnectionString, "BCP_Zone_Get")
    End Function
    Public Function GetZone_SortID() As DataSet
        Return SqlHelper.ExecuteDataset(ConnectionString, "BCP_Zone_GetSortID")
    End Function
    Public Function GetZone_BySearch(ByVal pKey As String) As DataSet
        Return SqlHelper.ExecuteDataset(ConnectionString, "BCP_Zone_GetBySearch", pKey)
    End Function
    Public Function GetZoneByID(ByVal pID As Integer) As DataSet
        Return SqlHelper.ExecuteDataset(ConnectionString, "BCP_Zone_GetByID", pID)
    End Function
    Public Function DeleteZone(ByVal pID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "BCP_Zone_Delete", pID)
    End Function
    Public Function SaveZone(ByVal pID As Integer, ByVal pName As String) As Integer

        If isAdd Then
            Return SqlHelper.ExecuteNonQuery(ConnectionString, "BCP_Zone_Add", pID, pName)
        Else
            Return SqlHelper.ExecuteNonQuery(ConnectionString, "BCP_Zone_Update", pID, pName)
        End If

    End Function

    Public Function GetZone_SearchIN(ByVal prmKey As String) As DataSet
        If prmKey <> "" And prmKey <> "0" Then
            SQL = "select * from  zone  where    ZoneCode  IN ( " & prmKey & ")"
        Else
            SQL = "select * from  zone  where    ZoneCode=0"
        End If
        Return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, SQL)
    End Function



End Class
