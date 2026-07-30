Imports Microsoft.ApplicationBlocks.Data
Public Class District
    Inherits DBClass
    Private Const intFieldNum As Integer = 5 'กำหนด จำนวนฟิลด์ที่มี
    Public Enum fldPos As Integer 'ประกาศตำแหน่งของฟิลด์
        f00_DistrictCode = 0
        f01_DistrictName_Th = 1
        f02_DistrictName_En = 2
        f03_ProvinceCode = 3
        f04_ProvinceName = 4
    End Enum

    Sub init() 'ใส่ชื่อ table, field name และ ค่า default
        strTableName = "District"
        intTotalField = intFieldNum


        ReDim tblField(intFieldNum)
        tblField.Clear(tblField, 0, tblField.Length)
        tblField(0).fldName = "DistrictCode"
        tblField(0).fldValue = 0
        tblField(1).fldName = "DistrictName_Th"
        tblField(1).fldValue = ""
        tblField(2).fldName = "DistrictName_En"
        tblField(2).fldValue = ""
        tblField(3).fldName = "ProvinceCode"
        tblField(3).fldValue = 0
        tblField(4).fldName = "Province"
        tblField(4).fldValue = ""

    End Sub

    Sub New()
        MyBase.New()
        Me.init()

    End Sub

#Region "Property"

    Public Property DistrictCode() As String
        Get
            Return tblField(0).fldValue
        End Get
        Set(ByVal Value As String)
            tblField(0).fldValue = Value
            tblField(0).fldAffect = True
        End Set
    End Property
    Public Property DistrictName_Th() As String
        Get
            Return tblField(1).fldValue
        End Get
        Set(ByVal Value As String)
            tblField(1).fldValue = Value
            tblField(1).fldAffect = True
        End Set
    End Property
    Public Property DistrictName_En() As String
        Get
            Return tblField(2).fldValue
        End Get
        Set(ByVal Value As String)
            tblField(2).fldValue = Value
            tblField(2).fldAffect = True
        End Set
    End Property
    Public Property ProvinceCode() As Integer
        Get
            Return tblField(0).fldValue
        End Get
        Set(ByVal Value As Integer)
            tblField(0).fldValue = Value
            tblField(0).fldAffect = True
        End Set
    End Property
    Public Property ProvinceName() As String
        Get
            Return tblField(2).fldValue
        End Get
        Set(ByVal Value As String)
            tblField(2).fldValue = Value
            tblField(2).fldAffect = True
        End Set
    End Property
#End Region

    Public ReadOnly Property TableName() As String
        Get
            Return strTableName
        End Get
    End Property
    Public Function GetDistrict() As DataSet
        Return SqlHelper.ExecuteDataset(ConnectionString, "BCP_District_Get")
    End Function

    Public Function GetDistrictByID(ByVal pID As String) As DataSet
        Return SqlHelper.ExecuteDataset(ConnectionString, "BCP_District_GetByID", pID)
    End Function

    Public Function GetDistrictByProvince(ByVal pID As Integer) As DataSet
        Return SqlHelper.ExecuteDataset(ConnectionString, "BCP_District_GetByProv", pID)
    End Function

    Public Function SaveDistrict(ByVal pID As String, ByVal pName As String, ByVal pPcode As Integer, ByVal pPname As String) As Integer

        If isAdd Then
            Return SqlHelper.ExecuteNonQuery(ConnectionString, "BCP_District_Add", pID, pName, pPcode, pPname)
        Else
            Return SqlHelper.ExecuteNonQuery(ConnectionString, "BCP_District_Update", pID, pName, pPCode, pPname)
        End If

    End Function
    Public Function DeleteDistrict(ByVal pID As String, ByVal pPcode As Integer) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, "BCP_District_Delete", pID, pPcode)


    End Function
End Class
