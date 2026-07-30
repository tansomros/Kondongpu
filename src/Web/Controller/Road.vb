Imports Microsoft.ApplicationBlocks.Data
Public Class Road
    Inherits DBClass
    Private Const intFieldNum As Integer = 4 'กำหนด จำนวนฟิลด์ที่มี
    Public Enum fldPos As Integer 'ประกาศตำแหน่งของฟิลด์
        _RoadCode = 0
        _RoadName_Th = 1
        _RoadName_En = 2
        _ProvinceCode = 3
    End Enum

    Sub init() 'ใส่ชื่อ table, field name และ ค่า default
        strTableName = "ROAD"
        intTotalField = intFieldNum

        ReDim tblField(intFieldNum)
        tblField.Clear(tblField, 0, tblField.Length)
        tblField(0).fldName = "RoadCode"
        tblField(0).fldValue = "0"
        tblField(1).fldName = "RoadName_Th"
        tblField(1).fldValue = ""
        tblField(2).fldName = "RoadName_En"
        tblField(2).fldValue = ""
        tblField(3).fldName = "ProvinceCode"
        tblField(3).fldValue = ""
    End Sub

    Sub New()
        MyBase.New()
        Me.init()

    End Sub

#Region "Property"

    Public Property f00_Code() As String
        Get
            Return tblField(0).fldValue
        End Get
        Set(ByVal Value As String)
            tblField(0).fldValue = Value
            tblField(0).fldAffect = True
        End Set
    End Property
    Public Property f01_Name1() As String
        Get
            Return tblField(1).fldValue
        End Get
        Set(ByVal Value As String)
            tblField(1).fldValue = Value
            tblField(1).fldAffect = True
        End Set
    End Property
    Public Property f02_Name2() As String
        Get
            Return tblField(2).fldValue
        End Get
        Set(ByVal Value As String)
            tblField(2).fldValue = Value
            tblField(2).fldAffect = True
        End Set
    End Property
    Public Property f03_ProvinceCode() As Integer
        Get
            Return tblField(0).fldValue
        End Get
        Set(ByVal Value As Integer)
            tblField(0).fldValue = Value
            tblField(0).fldAffect = True
        End Set
    End Property

#End Region

    Public ReadOnly Property TableName() As String
        Get
            Return strTableName
        End Get
    End Property
    Public Function GetRoad() As DataSet
        Return SqlHelper.ExecuteDataset(ConnectionString, "BCP_Road_Get")
    End Function

    Public Function GetRoadByID(ByVal pID As String) As DataSet
        Return SqlHelper.ExecuteDataset(ConnectionString, "BCP_Road_GetByID", pID)
    End Function

    Public Function GetRoadByProvince(ByVal pID As Integer) As DataSet
        Return SqlHelper.ExecuteDataset(ConnectionString, "BCP_Road_GetByProvince", pID)
    End Function

    Public Function SaveRoad(ByVal pID As String, ByVal pName As String, ByVal pCode As Integer) As Integer
        If isAdd Then
            Return SqlHelper.ExecuteNonQuery(ConnectionString, "BCP_Road_Add", pID, pName, pCode)
        Else
            Return SqlHelper.ExecuteNonQuery(ConnectionString, "BCP_Road_Update", pID, pName, pCode)
        End If

    End Function

    Public Function DeleteRoad(ByVal pID As String, ByVal pCode As Integer) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, "BCP_Road_Delete", pID, pCode)
    End Function

End Class
