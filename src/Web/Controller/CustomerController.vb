Imports Microsoft.ApplicationBlocks.Data
Public Class CustomerController
    Inherits BaseClass
    Public ds As DataSet = New DataSet

#Region "Customer"
    Public Function Customer_GetAll() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Customer_GetAll"))
        Return ds.Tables(0)
    End Function
    Public Function Customer_GetForSearch() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Customer_GetForSearch"))
        Return ds.Tables(0)
    End Function

    Public Function Customer_SearchByCardID(ByVal CardID As String) As Boolean
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Customer_SearchByCardID"), CardID)
        If ds.Tables(0).Rows.Count > 0 Then
            If DBNull2Zero(ds.Tables(0).Rows(0)(0)) > 0 Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

    Public Function Customer_GetBySearch(ByVal pKey As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Customer_GetBySearch"), pKey)
        Return ds.Tables(0)
    End Function
    Public Function Customer_GetEmployeeSearch(ByVal pKey As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Customer_GetEmployeeSearch"), pKey)
        Return ds.Tables(0)
    End Function
    Public Function Customer_GetByStatus(ByVal pKey As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Customer_GetByStatus"), pKey)
        Return ds.Tables(0)
    End Function
    Public Function Customer_GetForSelection() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Customer_GetForSelection"))
        Return ds.Tables(0)
    End Function
    Public Function Customer_GetByUID(ByVal pUID As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Customer_GetByUID"), pUID)
        Return ds.Tables(0)
    End Function
    Public Function Customer_GetByCustomerID(ByVal pUID As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Customer_GetByCustomerID"), pUID)
        Return ds.Tables(0)
    End Function
    Public Function Customer_GetByEmployeeCode(ByVal Empcode As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Customer_GetByEmployeeCode"), Empcode)
        Return ds.Tables(0)
    End Function
    Public Function Customer_GetByCarRegis(ByVal CarNumber As String, ByVal CustomerID As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Customer_GetByCarRegis"), CarNumber, CustomerID)
        Return ds.Tables(0)
    End Function
    Public Function GetFullCarRegis(ByVal CarNumber As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("GetFullCarRegis"), CarNumber)
        Return ds.Tables(0)
    End Function
    Public Function Customer_Save(
           ByVal CustomerID As String _
           , ByVal Prefix As String _
           , ByVal FirstName As String _
           , ByVal LastName As String _
           , ByVal NickName As String _
           , ByVal Age As Integer _
           , ByVal Sex As String _
           , ByVal CardID As String _
           , ByVal Tel As String _
           , ByVal AddressNo As String _
           , ByVal Moo As String _
           , ByVal Village As String _
           , ByVal District As String _
           , ByVal City As String _
           , ByVal ProvinceID As Integer _
           , ByVal ProvinceName As String _
           , ByVal Zipcode As String _
           , ByVal Email As String _
           , ByVal LineID As String _
           , ByVal AccountNo As String, ByVal AccName As String, ByVal BankID As Integer _
           , ByVal ProjectLoan As String _
           , ByVal ProjecCane As String _
           , ByVal StatusFlag As String _
           , ByVal CUser As Integer) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Customer_Save"),
             CustomerID _
           , Prefix _
           , FirstName _
           , LastName _
           , NickName _
           , Age _
           , Sex _
           , CardID _
           , Tel _
           , AddressNo _
           , Moo _
           , Village _
           , District _
           , City _
           , ProvinceID _
           , ProvinceName _
           , Zipcode _
           , Email, LineID, AccountNo, AccName, BankID, ProjectLoan, ProjecCane, StatusFlag, CUser)
    End Function
    Public Function Customer_UpdateStatus(ByVal pID As String, StatusFlag As String, MUser As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Customer_UpdateStatus"), pID, StatusFlag, MUser)
    End Function
    Public Function Customer_Delete(ByVal pID As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Customer_Delete"), pID)
    End Function
    Public Function GetCustomer_ByCustID(ByVal pCond As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Customer_GetByID"), pCond)
        Return ds.Tables(0)
    End Function
    Public Function GetCustomer_ChkDup(ByVal pID As String) As DataTable

        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Customer_GetByID"), pID)
        Return ds.Tables(0)
    End Function


    Public Function SaveCustomer(isF As Boolean, ByVal CustID As String, CustName As String, CustAge As String, Tel As String, AddressNo As String, AddressMoo As String, AddressTown As String, AddressDistrict As String, AddressAmpher As String, ProvinceID As Integer, ZipCode As String, AreaCode As String, ZoneCode As String, CreateDate As String, UpdBy As String, UpdDate As String, Loan As Double, acc_Status As Integer, closeDate As String) As Integer
        'Dim obj As New Customer

        If isF Then
            Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Customer_Add"), CustID, CustName, CustAge, Tel, AddressNo, AddressMoo, AddressTown, AddressDistrict, AddressAmpher, ProvinceID, ZipCode, AreaCode, ZoneCode, CreateDate, UpdBy, UpdDate, Loan, acc_Status, closeDate)
        Else
            Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Customer_Update"), CustID, CustName, CustAge, Tel, AddressNo, AddressMoo, AddressTown, AddressDistrict, AddressAmpher, ProvinceID, ZipCode, AreaCode, ZoneCode, CreateDate, UpdBy, UpdDate)
        End If

    End Function

    Public Function CustomerUpdate(ByVal CustID As String, Tel As String, UpdBy As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Customer_UpdateTel"), CustID, Tel, UpdBy)

    End Function

#End Region

#Region "Customer Car RegisNumber"
    Public Function CarRegistration_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CarRegistration_Get"))
        Return ds.Tables(0)
    End Function
    Public Function CarRegistration_GetByCustomer(ByVal CusUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CarRegistration_GetByCustomer"), CusUID)
        Return ds.Tables(0)
    End Function
    Function CarRegistration_CheckDuplicate(ByVal CarRegis As String) As Boolean
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CarRegistration_GetByRegisNumber"), CarRegis)
        If ds.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Function CarRegistration_Add(ByVal CustomerID As String, ByVal CarRegis As String, StatusFlag As String, ByVal CUser As Integer) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("CarRegistration_Add"), CustomerID, CarRegis, StatusFlag, CUser)
    End Function
    Function CarRegistration_ActiveStatusQ(ByVal CustomerID As String, ByVal CUser As Integer) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("CarRegistration_ActiveStatusQ"), CustomerID, CUser)
    End Function
    Function CarRegistration_DeleteStatusQ(ByVal CustomerID As String, ByVal CUser As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("CarRegistration_DeleteStatusQ"), CustomerID, CUser)
    End Function
    Function CarRegistration_Delete(ByVal CarUID As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("CarRegistration_DeleteByUID"), CarUID)
    End Function
    Function CarRegistration_Delete(ByVal CustomerID As String, ByVal RegisNumber As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("CarRegistration_Delete"), CustomerID, RegisNumber)
    End Function

#End Region

#Region "Job History"
    Public Function CustomerJobHistory_Save(
              ByVal JUID As Integer,
              ByVal CustomerUID As Integer,
              ByVal CompanyName As String,
              ByVal BUType As String,
              ByVal Address As String,
              ByVal tel As String,
              ByVal StartWorkDate As String,
              ByVal EndWorkDate As String,
              ByVal Position As String,
              ByVal WorkType As String,
              ByVal JobDesc As String,
              ByVal isNote As String,
              ByVal UpdBy As Integer) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("CustomerJobHistory_Save"), JUID,
             CustomerUID _
           , CompanyName _
           , BUType _
           , Address _
           , tel _
           , StartWorkDate _
           , EndWorkDate _
           , Position _
           , WorkType _
           , JobDesc _
           , isNote _
           , UpdBy)


    End Function

    Public Function CustomerJobHistory_Get(ByVal pUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CustomerJobHistory_Get"), pUID)
        Return ds.Tables(0)
    End Function
    Public Function CustomerJobHistory_GetByUID(ByVal pUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CustomerJobHistory_GetByUID"), pUID)
        Return ds.Tables(0)
    End Function
#End Region

#Region "Risk Factor"

    Public Function CustomerRiskFactor_GetByCustomerUID(ByVal pUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CustomerRiskFactor_GetByCustomerUID"), pUID)
        Return ds.Tables(0)
    End Function
    Public Function CustomerRisk_Save(
            ByVal AttributeUID As Integer, ByVal CustomerUID As Integer, ByVal DataValue As String, ByVal UpdBy As Integer) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("CustomerRisk_Save"), AttributeUID,
             CustomerUID _
           , DataValue _
           , UpdBy)

    End Function
#End Region
#Region "PPE"

    Public Function CustomerPPE_GetByCustomerUID(ByVal pUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CustomerPPE_GetByCustomerUID"), pUID)
        Return ds.Tables(0)
    End Function
    Public Function CustomerPPE_Save(
            ByVal AttributeUID As Integer, ByVal CustomerUID As Integer, ByVal DataValue As String, ByVal UpdBy As Integer) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("CustomerPPE_Save"), AttributeUID,
             CustomerUID _
           , DataValue _
           , UpdBy)
    End Function
#End Region

#Region "HealthHistory"

    Public Function CustomerHealthHistory_Save(ByVal param As String())
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("HealthHistory_Save"), param)
    End Function
    Public Function CustomerHealthHistory_Save(
           ByVal CustomerUID As Integer _
      , ByVal isCongenitalDisease As String _
      , ByVal DiseaseDetail As String _
      , ByVal isSurgery As String _
      , ByVal SurgeryDetail As String _
      , ByVal isImmunity As String _
      , ByVal ImmunityDetail As String _
      , ByVal isMedicine As String _
      , ByVal MedicineDetail As String _
      , ByVal isAllergy As String _
      , ByVal AllergyDetail As String _
      , ByVal isSmoking As String _
      , ByVal SmokeQTY As String _
      , ByVal SmokeQTYBefore As String _
      , ByVal SmokeTime As String _
      , ByVal SmokeUOM As String _
      , ByVal isAlcohol As String _
      , ByVal DrinkFrequency As String _
      , ByVal DrinkTime As String _
      , ByVal DrinkUOM As String _
      , ByVal isDrugs As String _
      , ByVal DrugsDetail As String _
      , ByVal Remark As String _
      , ByVal CUser As Integer) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("HealthHistory_Save"), CustomerUID, isCongenitalDisease _
      , DiseaseDetail _
      , isSurgery _
      , SurgeryDetail _
      , isImmunity _
      , ImmunityDetail _
      , isMedicine _
      , MedicineDetail _
      , isAllergy _
      , AllergyDetail _
      , isSmoking _
      , SmokeQTY _
      , SmokeQTYBefore _
      , SmokeTime _
      , SmokeUOM _
      , isAlcohol _
      , DrinkFrequency _
      , DrinkTime _
      , DrinkUOM _
      , isDrugs _
      , DrugsDetail _
      , Remark _
      , CUser)
    End Function
#End Region


End Class
