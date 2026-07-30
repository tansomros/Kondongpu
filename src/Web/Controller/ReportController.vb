Imports Microsoft.ApplicationBlocks.Data

Public Class ReportController
    Inherits BaseClass
    Dim ds As New DataSet
#Region "Location"


    Public Function RPT_Location_ByAccStatus_ForChart() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("RPT_Location_ByAccStatus_ForChart"))
        Return ds.Tables(0)
    End Function

    Public Function RPT_Location_ByType1_ForChart() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("RPT_Location_ByType1_ForChart"))
        Return ds.Tables(0)
    End Function
    Public Function RPT_Location_ByType2_ForChart() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("RPT_Location_ByType2_ForChart"))
        Return ds.Tables(0)
    End Function
    Public Function RPT_Location_ByType3_ForChart() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("RPT_Location_ByType3_ForChart"))
        Return ds.Tables(0)
    End Function
    Public Function RPT_Location_ByType4_ForChart() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("RPT_Location_ByType4_ForChart"))
        Return ds.Tables(0)
    End Function
    Public Function RPT_Location_ByType5_ForChart() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("RPT_Location_ByType5_ForChart"))
        Return ds.Tables(0)
    End Function
    Public Function RPT_Location_ByType6_ForChart() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("RPT_Location_ByType6_ForChart"))
        Return ds.Tables(0)
    End Function
    Public Function RPT_Location_ByType7_ForChart() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("RPT_Location_ByType7_ForChart"))
        Return ds.Tables(0)
    End Function

    Public Function RPT_Location_ByType8_ForChart() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("RPT_Location_ByType8_ForChart"))
        Return ds.Tables(0)
    End Function
    Public Function RPT_Location_ByType9_ForChart() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("RPT_Location_ByType9_ForChart"))
        Return ds.Tables(0)
    End Function
    Public Function RPT_Location_ByType10_ForChart() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("RPT_Location_ByType10_ForChart"))
        Return ds.Tables(0)
    End Function


    Public Function RPT_Location_GetBySearch(LGroup As String, LChain As String, LType As String, NHSO As String, PGroup As String, ProvinceID As String, isAccPharm As String, AccStatus As String, Search As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Location_GetBySearch"), LGroup, LChain, LType, NHSO, PGroup, ProvinceID, isAccPharm, AccStatus, Search)
        Return ds.Tables(0)
    End Function
    Public Function GEN_LocationReportBySearch(LGroup As String, LChain As String, LType As String, NHSO As String, PGroup As String, ProvinceID As String, isAccPharm As String, AccStatus As String, Search As String, UserID As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("GEN_Location_GetBySearch"), LGroup, LChain, LType, NHSO, PGroup, ProvinceID, isAccPharm, AccStatus, Search, UserID)

    End Function

    Public Function RPT_Request_GetByStatus(StartDate As String, EndDate As String, AsmStatus As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("RPT_Request_GetByStatus"), StartDate, EndDate, AsmStatus)
        Return ds.Tables(0)
    End Function
    Public Function RPT_Request_GetByType(StartDate As String, EndDate As String, AsmType As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("RPT_Request_GetByType"), StartDate, EndDate, AsmType)
        Return ds.Tables(0)
    End Function
    Public Function RPT_Request_GetByStatus(StartDate As String, EndDate As String, AsmStatus As String, SupervisorUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("RPT_Request_GetByStatusForSupervisor"), StartDate, EndDate, AsmStatus, SupervisorUID)
        Return ds.Tables(0)
    End Function
    Public Function RPT_Request_GetByType(StartDate As String, EndDate As String, AsmType As String, SupervisorUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("RPT_Request_GetByTypeForSupervisor"), StartDate, EndDate, AsmType, SupervisorUID)
        Return ds.Tables(0)
    End Function
    Public Function RPT_Assessment_GetScore(StartDate As String, EndDate As String, AsmType As String, AsmStatus As String, LocationUID As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("RPT_Assessment_GetScore"), StartDate, EndDate, AsmType, AsmStatus, LocationUID)
        Return ds.Tables(0)
    End Function
    Public Function RPT_Assessment_GetSWOT(StartDate As String, EndDate As String, AsmType As String, AsmStatus As String, LocationUID As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("RPT_Assessment_GetSWOT"), StartDate, EndDate, AsmType, AsmStatus, LocationUID)
        Return ds.Tables(0)
    End Function

    Public Function RPT_Assessment_GetScore(StartDate As String, EndDate As String, AsmType As String, AsmStatus As String, LocationUID As String, SupervisorUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("RPT_Assessment_GetScoreForSupervisor"), StartDate, EndDate, AsmType, AsmStatus, LocationUID, SupervisorUID)
        Return ds.Tables(0)
    End Function
    Public Function RPT_Assessment_GetSWOT(StartDate As String, EndDate As String, AsmType As String, AsmStatus As String, LocationUID As String, SupervisorUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("RPT_Assessment_GetSWOTForSupervisor"), StartDate, EndDate, AsmType, AsmStatus, LocationUID, SupervisorUID)
        Return ds.Tables(0)
    End Function

    Public Function RPT_LegalAction_Assessment_ForChart(PeriodID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("RPT_LegalAction_Assessment_ForChart"), PeriodID)
        Return ds.Tables(0)
    End Function
    Public Function RPT_LegalAction_AssessmentByDepartment_ForChart(PeriodID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("RPT_LegalAction_AssessmentByDepartment_ForChart"), PeriodID)
        Return ds.Tables(0)
    End Function

    Public Function RPT_Legal_ByType_ForChart(PeriodID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("RPT_Legal_ByType_ForChart"), PeriodID)
        Return ds.Tables(0)
    End Function
    Public Function RPT_Practice_GetByLegal(LegalUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("RPT_Practice_GetByLegal"), LegalUID)
        Return ds.Tables(0)
    End Function
    Public Function RPT_Location_GetQACountAll() As Integer
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("RPT_Location_GetQACountAll"))
        Return DBNull2Zero(ds.Tables(0).Rows(0)(0))
    End Function
    Public Function RPT_Location_GetQACountTypeNull() As Integer
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("RPT_Location_GetQACountTypeNull"))
        Return DBNull2Zero(ds.Tables(0).Rows(0)(0))
    End Function
    Public Function Legal_GetLastMonthCountByType() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Legal_GetLastMonthCountByType"))
        Return ds.Tables(0)
    End Function

    Public Function RPT_Location_GetCountByType() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("RPT_Location_GetCountByType"))
        Return ds.Tables(0)
    End Function
    Public Function RPT_Location_GetCountByProvinceGroup() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("RPT_Location_GetCountByProvinceGroup"))
        Return ds.Tables(0)
    End Function
    Public Function RPT_Location_GetCountByChainGroup() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("RPT_Location_GetCountByChainGroup"))
        Return ds.Tables(0)
    End Function

    Public Function RPT_Location_GetCountByNHSOGroup() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("RPT_Location_GetCountByNHSOGroup"))
        Return ds.Tables(0)
    End Function

    Public Function RPT_Location_GetCountByProvince() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("RPT_Location_GetCountByProvince"))
        Return ds.Tables(0)
    End Function
    Public Function Legal_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("RPT_Legal_Get"))
        Return ds.Tables(0)
    End Function
    Public Function Legal_GetByCompany(CompanyUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("RPT_Legal_GetByCompany"), CompanyUID)
        Return ds.Tables(0)
    End Function
    Public Function Legal_GetLast() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("RPT_Legal_GetLast"))
        Return ds.Tables(0)
    End Function

    Public Function LegalAction_GetRelate(PeriodID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("LegalAction_GetRelate"), PeriodID)
        Return ds.Tables(0)
    End Function
    Public Function LegalAction_GetByAssessmented(PeriodID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("LegalAction_GetByAssessmented"), PeriodID)
        Return ds.Tables(0)
    End Function
    Public Function LegalAction_GetByAsmResult(PeriodID As Integer, Status As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("LegalAction_GetByAsmResult"), PeriodID, Status)
        Return ds.Tables(0)
    End Function
#End Region
#Region "Agreement"
    Public Function Agreement_Report_GetSearch(ReqType As String, ReqStatus As String, Search As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Agreement_Report_GetSearch"), ReqType, ReqStatus, Search)
        Return ds.Tables(0)
    End Function
    Public Function GEN_Agreement_GetSearch(ReqType As String, ReqStatus As String, Search As String, UserID As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("GEN_Agreement_Report_BySearch"), ReqType, ReqStatus, Search, UserID)
    End Function

#End Region

End Class
