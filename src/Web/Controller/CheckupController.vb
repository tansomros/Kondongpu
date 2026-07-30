Imports Microsoft.ApplicationBlocks.Data

Public Class CheckupController
    Inherits BaseClass
#Region "CheckupDefinition"
    Public Function CheckupDefinition_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CheckupDefinition_Get"))
        Return ds.Tables(0)
    End Function
    Public Function CheckupDefinition_GetYear() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CheckupDefinition_GetYear"))
        Return ds.Tables(0)
    End Function
    Public Function CheckupDefinition_GetByYear(pYear As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CheckupDefinition_GetByYear"), pYear)
        Return ds.Tables(0)
    End Function
    Public Function CheckupDefinition_GetBySearch(pKey As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CheckupDefinition_GetBySearch"), pKey)
        Return ds.Tables(0)
    End Function
    Public Function CheckupDefinition_GetYearByUID(pUID As Integer) As Integer
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CheckupDefinition_GetYearByUID"), pUID)
        Return DBNull2Zero(ds.Tables(0).Rows(0)(0))
    End Function
    Public Function CheckupDefinition_isDup(pCode As String, pDate As String, pHospital As String, chkType As Integer) As Boolean
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CheckupDefinition_CheckDup"), pCode, pDate, pHospital, chkType)
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
    Public Function CheckupDefinition_GetByID(ByVal pID As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CheckupDefinition_GetByID"), pID)
        Return ds.Tables(0)
    End Function

    Public Function CheckupDefinition_Save(ByVal pCODE As String, ByVal pYEAR As String, pDATE As String, pHCODE As String, ChkType As Integer, pREMARK As String, pUSER As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("CheckupDefinition_Save"), pCODE, pYEAR, pDATE, pHCODE, ChkType, pREMARK, pUSER)

    End Function
    Public Function CheckupDefinition_Delete(ByVal pID As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("CheckupDefinition_Delete"), pID)
    End Function
#End Region
#Region "CheckUp List"
    Public Function CheckupList_GetAll() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CheckupList_GetAll"))
        Return ds.Tables(0)
    End Function
    Public Function CheckupList_GetLast() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CheckupList_GetLast"))
        Return ds.Tables(0)
    End Function
    Public Function CheckupList_GetByShortSearch(pSearch As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CheckupList_GetByShortSearch"), pSearch)
        Return ds.Tables(0)
    End Function

#End Region

#Region "CheckupPerson"
    Public Function CheckupPerson_Save(ByVal PUID As Integer, ByVal PersonUID As Integer _
           , ByVal EmployeeCode As String _
           , ByVal CYear As Integer _
           , ByVal CheckupDate As String _
           , ByVal HospitalCode As String _
           , ByVal CheckupTypeUID As Integer _
           , ByVal Remark As String _
           , ByVal HN As String _
           , ByVal Weight As Double _
           , ByVal Height As Double _
           , ByVal BMI As Double _
           , ByVal Waist As Double _
           , ByVal Shape As Double _
           , ByVal Temperature As Double _
           , ByVal Pluse As Integer _
           , ByVal RR As Integer _
           , ByVal SBP As Integer _
           , ByVal DBP As Integer _
           , ByVal DoctorRecommend As String _
           , ByVal PositionUID As Integer _
           , ByVal MUser As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("CheckupPerson_Save"), PUID, PersonUID _
           , EmployeeCode _
           , CYear, CheckupDate, HospitalCode, CheckupTypeUID, Remark _
           , HN _
           , Weight _
           , Height _
           , BMI _
           , Waist _
           , Shape _
           , Temperature _
           , Pluse _
           , RR _
           , SBP _
           , DBP _
           , DoctorRecommend _
           , PositionUID _
           , MUser)

    End Function
    Public Function CheckupPerson_GetByUID(PUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CheckupPerson_GetByUID"), PUID)
        Return ds.Tables(0)
    End Function

    Public Function CheckupPerson_GetUID(CYear As Integer, PUID As Integer, CheckupTypeUID As Integer) As Integer
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CheckupPerson_GetUID"), CYear, PUID, CheckupTypeUID)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0)(0)
        Else
            Return 0
        End If

    End Function


#End Region

#Region "CheckupResult"
    Public Function CheckupResult_GetLabResult(PUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CheckupResult_GetLabResultByPersonUID"), PUID)
        Return ds.Tables(0)
    End Function
    Public Function CheckupResult_GetLabResult(PUID As Integer, labGRP As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CheckupResult_GetLabResultByCategory"), PUID, labGRP)
        Return ds.Tables(0)
    End Function
    Public Function CheckupResult_SaveResult(CheckupPersonUID As Long, ItemCode As String, ResultValue As String, ReferenceRange As String, IsNormalFlag As String, ResultSummary As String, ResultComment As String, ResultDttm As String, Muser As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("CheckupResult_SaveResult"), CheckupPersonUID, ItemCode, ResultValue, ReferenceRange, IsNormalFlag, ResultSummary, ResultComment, ResultDttm, Muser)
    End Function
    Public Function CheckupResult_DeleteResult(CheckupPersonUID As Long, ItemCode As String, Muser As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("CheckupResult_DeleteResult"), CheckupPersonUID, ItemCode, Muser)
    End Function
#End Region
End Class