Imports Microsoft.ApplicationBlocks.Data
Public Class AgreementController
    Inherits BaseClass
    Public ds As New DataSet

#Region "Agreement"
    Public Function Agreement_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Agreement_Get"))
        Return ds.Tables(0)
    End Function
    Public Function Agreement_GetSearch(StartDate As String, EndDate As String, ReqType As String, ReqStatus As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Agreement_GetSearch"), StartDate, EndDate, ReqType, ReqStatus)
        Return ds.Tables(0)
    End Function
    Public Function Agreement_GetStatusAlive(LocationUID As Integer) As Boolean
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Agreement_GetStatusAlive"), LocationUID)
        If ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0).Rows(0)(0) > 0 Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function
    Public Function Agreement_GetByLawType(PhaseUID As String, TypeUID As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Agreement_GetByLawType"), PhaseUID, TypeUID)
        Return ds.Tables(0)
    End Function
    Public Function Agreement_GetByLawMaster(PhaseUID As String, MasterUID As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Agreement_GetByLawMaster"), PhaseUID, MasterUID)
        Return ds.Tables(0)
    End Function

    Public Function Agreement_GetByStatus(Status As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Agreement_GetByStatus"), Status)
        Return ds.Tables(0)
    End Function
    Public Function Agreement_GetByCompany(CompanyUID As Integer, PeriodID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Agreement_GetByCompany"), PeriodID)
        Return ds.Tables(0)
    End Function

    Public Function Agreement_GetForAssessment(CompanyUID As Integer, UserID As Integer, PeriodID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Agreement_GetForAssessment"), UserID, PeriodID)
        Return ds.Tables(0)
    End Function
    Public Function Agreement_GetForApproval(CompanyUID As Integer, UserID As Integer, PeriodID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Agreement_GetForApproval"), UserID, PeriodID)
        Return ds.Tables(0)
    End Function
    Public Function Agreement_GetByPhaseUID(PhaseUID As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Agreement_GetByPhaseUID"), PhaseUID)
        Return ds.Tables(0)
    End Function
    Public Function Agreement_GetByMinistryUID(PhaseUID As String, MinistryUID As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Agreement_GetByMinistryUID"), PhaseUID, MinistryUID)
        Return ds.Tables(0)
    End Function
    Public Function Agreement_GetByYear(PhaseUID As String, AgreementYear As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Agreement_GetByYear"), PhaseUID, AgreementYear)
        Return ds.Tables(0)
    End Function
    Public Function Agreement_GetByBusinessTypeUID(PhaseUID As String, BusinessTypeUID As String, FactoryTypeUID As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Agreement_GetByBusinessTypeUID"), PhaseUID, BusinessTypeUID, FactoryTypeUID)
        Return ds.Tables(0)
    End Function
    Public Function Agreement_GetByKeyword(PhaseUID As String, sKeyword As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Agreement_GetByKeyword"), PhaseUID, sKeyword)
        Return ds.Tables(0)
    End Function

    Public Function Agreement_GetByCompany(CompanyUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Agreement_GetByCompany"), CompanyUID)
        Return ds.Tables(0)
    End Function
    Public Function Agreement_GetByUser(UserID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Agreement_GetByUser"), UserID)
        Return ds.Tables(0)
    End Function
    Public Function Agreement_GetActive(CompanyUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Agreement_GetActive"), CompanyUID)
        Return ds.Tables(0)
    End Function

    Public Function AgreementYear_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("AgreementYear_Get"))
        Return ds.Tables(0)
    End Function
    Public Function AgreementYear_Get4Select() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("AgreementYear_Get4Select"))
        Return ds.Tables(0)
    End Function
    Public Function Agreement_CheckDuplicate(Code As String) As Boolean
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Agreement_CheckDuplicate", Code)
        If ds.Tables(0).Rows.Count > 0 Then
            If String.Concat(ds.Tables(0).Rows(0)(0)) <> "0" Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

    Public Function Agreement_GetByUID(pUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Agreement_GetByUID"), pUID)
        Return ds.Tables(0)
    End Function
    Public Function Agreement_GetDetail(pUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Agreement_GetDetail"), pUID)
        Return ds.Tables(0)
    End Function
    Public Function Agreement_Save(ByVal UID As Integer, ByVal Code As String, ByVal CustomerUID As Integer, ByVal AgreementType As String, ByVal AgreementDate As String, ByVal Location As String, ByVal Amount As Double, ByVal Interest As Double, ByVal GName1 As String, ByVal GAge1 As String, ByVal GTel1 As String, ByVal GAddress1 As String, ByVal GName2 As String, ByVal GAge2 As String, ByVal GTel2 As String, ByVal GAddress2 As String, ByVal Balance As Double, ByVal Remark As String, ByVal ProductDetail As String, ByVal Objective As String, ByVal CUser As Integer, ByVal Creditor As String, Duedate As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Agreement_Save"), UID, Code, CustomerUID, AgreementType, AgreementDate, Location, Amount, Interest, GName1, GAge1, GTel1, GAddress1, GName2, GAge2, GTel2, GAddress2, Balance, Remark, ProductDetail, Objective, CUser, Creditor, Duedate)
    End Function
    Public Function Agreement_Clone(ByVal AccNo_Old As String, ByVal AccNo_New As String, ByVal AgreementNo_New As String, ByVal AgreementDate As String, ByVal AgreementEnd As String, ByVal CUser As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Agreement_Clone"), AccNo_Old, AccNo_New, AgreementNo_New, AgreementDate, AgreementEnd, CUser)
    End Function
    Public Function AgreementAssignment_get(ByVal AgreementUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("AgreementAssignment_Get"), AgreementUID)
        Return ds.Tables(0)
    End Function
    Public Function Agreement_AddSupervisor(ByVal AgreementUID As Integer, ByVal UserID As Integer, ByVal UpdBy As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Agreement_AddSupervisor"), AgreementUID, UserID, UpdBy)
    End Function

    Public Function AgreementAssignment_Delete(ByVal UID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("AgreementAssignment_Delete"), UID)
    End Function

    Public Function Agreement_UpdateStatus(ByVal UID As Integer, AgreementStatus As String, ByVal UpdBy As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Agreement_UpdateStatus"), UID, AgreementStatus, UpdBy)
    End Function

    Public Function AgreementTransaction_Get(AgreementUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("AgreementTransaction_Get"), AgreementUID)
        Return ds.Tables(0)
    End Function

    Public Function AgreementTransaction_Add(ByVal AgreementUID As Integer, CustomerUID As Integer, AsmStatus As String, Remark As String, ByVal UpdBy As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("AgreementTransaction_Add"), AgreementUID, CustomerUID, AsmStatus, Remark, UpdBy)
    End Function

    Public Function Practice_Get(AgreementUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Practice_Get"), AgreementUID)
        Return ds.Tables(0)
    End Function
    Public Function Practice_GetForAssessment(AgreementUID As Integer, UserID As Integer, PeriodID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Practice_GetForAssessment"), AgreementUID, UserID, PeriodID)
        Return ds.Tables(0)
    End Function

    Public Function Practice_GetAssessmentResult(PeriodID As Integer, AgreementUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Practice_GetAssessmentResult"), PeriodID, AgreementUID)
        Return ds.Tables(0)
    End Function

    Public Function AgreementAction_GetProgression(PeriodID As Integer, AgreementUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("AgreementAction_GetProgression"), PeriodID, AgreementUID)
        Return ds.Tables(0)
    End Function

    Public Function Practice_GetByUID(UID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Practice_GetByUID"), UID)
        Return ds.Tables(0)
    End Function


    Public Function Practice_Delete(ByVal UID As Integer, AgreementUID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Practice_Delete"), UID, AgreementUID)
    End Function
    Public Function Practice_Save(ByVal UID As Integer, AgreementUID As Integer, Code As String, Description As String, Recurrence As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Practice_Save"), UID, AgreementUID, Code, Description, Recurrence)
    End Function

    Public Function Practice_SaveByImport(AgreementCode As String, PracticeCode As String, Description As String, Recurrence As String, CUser As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Practice_SaveByImport"), AgreementCode, PracticeCode, Description, Recurrence, CUser)
    End Function
    Public Function Practice_CheckDuplicate(AgreementCode As String, Code As String) As Boolean
        ds = SqlHelper.ExecuteDataset(ConnectionString, "Practice_CheckDuplicate", AgreementCode, Code)
        If ds.Tables(0).Rows.Count > 0 Then
            If String.Concat(ds.Tables(0).Rows(0)(0)) <> "0" Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function
    Public Function Agreement_GetUID(pCode As String) As Integer
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Agreement_GetUID"), pCode)
        If ds.Tables(0).Rows.Count > 0 Then
            Return DBNull2Zero(ds.Tables(0).Rows(0)(0))
        Else
            Return 0
        End If
    End Function
    Public Function Agreement_UpdateFile(ByVal UID As Integer, ByVal FilePath As String, ByVal CUser As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, ("Agreement_UpdateFile"), UID, FilePath, CUser)
    End Function
    Public Function Agreement_UpdateLocationAsmStatus(ByVal LocationUID As Integer, AgreementUID As Long, AsmStatus As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Agreement_UpdateLocationAsmStatus", LocationUID, AgreementUID, AsmStatus)
    End Function

    Public Function Agreement_UpdateGPPAsmStatus(ByVal LocationUID As Integer, AgreementUID As Long, Score As Double, TotalScore As Double, Percentage As Double, AsmStatus As String, Comment As String, MUser As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, "Agreement_UpdateGPPAsmStatus", LocationUID, AgreementUID, Score, TotalScore, Percentage, AsmStatus, Comment, MUser)
    End Function

#End Region

#Region "Agreement Status"
    Public Function AgreementStatus_GetForReport() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("AgreementStatus_GetForReport"))
        Return ds.Tables(0)
    End Function

    Public Function AgreementStatus_Delete(ByVal AgreementUID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("AgreementStatus_Delete"), AgreementUID)
    End Function

    Public Function AgreementStatus_Save(ByVal AgreementUID As Integer, ByVal StatusUID As Integer, ByVal UpdBy As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("AgreementStatus_Save"), AgreementUID, StatusUID, UpdBy)
    End Function
    Public Function AgreementStatus_GetByAgreementUID(AgreementUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("AgreementStatus_GetByAgreementUID"), AgreementUID)
        Return ds.Tables(0)
    End Function

#End Region
#Region "Agreement  Type"
    Public Function AgreementType_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("AgreementType_Get"))
        Return ds.Tables(0)
    End Function
    Public Function AgreementType_GetForReport() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("AgreementType_GetForReport"))
        Return ds.Tables(0)
    End Function
    Public Function AgreementType_Delete(ByVal AgreementUID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("AgreementType_Delete"), AgreementUID)
    End Function

    Public Function AgreementType_Save(ByVal AgreementUID As Integer, ByVal TypeUID As Integer, ByVal UpdBy As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("AgreementType_Save"), AgreementUID, TypeUID, UpdBy)
    End Function
    Public Function AgreementType_GetByAgreementUID(AgreementUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("AgreementType_GetByAgreementUID"), AgreementUID)
        Return ds.Tables(0)
    End Function

#End Region


    Public Function Agreement_UpdateActivityQA(ByVal UID As Integer, ActivityQA As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Agreement_UpdateActivityQA"), UID, ActivityQA)
    End Function

    Public Function Agreement_UpdateLicensee(ByVal AgreementUID As Integer, ByVal Licensee_Old As String, ByVal Licensee_New As String, ByVal LicenseeType_Old As String, ByVal LicenseeType_New As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Agreement_UpdateLicensee"), AgreementUID, Licensee_Old, Licensee_New, LicenseeType_Old, LicenseeType_New)
    End Function
    Public Function Agreement_UpdateLocationName(ByVal AgreementUID As Integer, ByVal LocationName_Old As String, ByVal LocationName_New As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Agreement_UpdateLocationName"), AgreementUID, LocationName_Old, LocationName_New)
    End Function
    Public Function Agreement_ChangeType(ByVal AgreementUID As Integer, ByVal TypeID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Agreement_ChangeType"), AgreementUID, TypeID)
    End Function

    Public Function Agreement_Cancel(ByVal UID As Integer, Remark As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Agreement_Cancel"), UID, Remark)
    End Function
    Public Function Agreement_Delete(ByVal UID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Agreement_Delete"), UID)
    End Function

    Public Function Agreement_UpdateFileName(ByVal UID As Integer, ByVal LawNo As String, ByVal Name As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Agreement_UpdateFileName"), UID, LawNo, Name)
    End Function

    Public Function AgreementImage_Get(LawUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("AgreementImage_Get"), LawUID)
        Return ds.Tables(0)
    End Function

    Public Function AgreementImage_Delete(ByVal UID As Integer, ByVal LawNo As String, ByVal Name As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("AgreementImage_Delete"), UID, LawNo, Name)
    End Function
    'Public Function Agreement_UpdateFileName(ByVal UID As Integer, ByVal LawNo As String, ByVal Name As String) As Integer
    '    Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Agreement_UpdateFileName"), UID, LawNo, Name)
    'End Function


    Public Function AgreementRelease_GetByAgreement(CompanyUID As Integer, LawUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("AgreementRelease_GetByAgreement"), LawUID)
        Return ds.Tables(0)
    End Function

    Public Function AgreementRelease_GetByUID(UID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("AgreementRelease_GetByUID"), UID)
        Return ds.Tables(0)
    End Function

    Public Function AgreementRelease_Save(ByVal UID As Integer, ByVal LawUID As Integer, ByVal PersonUID As Integer, CompanyUID As Integer, LevelID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("AgreementRelease_Save"), UID, LawUID, PersonUID, LevelID)
    End Function

    Public Function AgreementRelease_Delete(ByVal UID As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("AgreementRelease_Delete"), UID)
    End Function

    Public Function Agreement_Problem_Get(CompanyUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Agreement_Problem_Get"), CompanyUID)
        Return ds.Tables(0)
    End Function

    Public Function LawAction_Get(CompanyUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("LawAction_Get"), CompanyUID)
        Return ds.Tables(0)
    End Function

    Public Function LawOwner_GetEmailAlert(CompanyUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("LawOwner_GetEmailAlert"), CompanyUID)
        Return ds.Tables(0)
    End Function

    Public Function SendAlert_Save(LawUID As Integer, PersonUID As Integer, email As String, Status As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("SendAlert_Save"), LawUID, PersonUID, email, Status)
    End Function
    Public Function SendAlert_UpdateStatus(LawUID As Integer, PersonUID As Integer, Status As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("SendAlert_UpdateStatus"), LawUID, PersonUID, Status)
    End Function

    Public Function Agreement_Reject(PeriodID As Integer, ByVal AgreementUID As Integer, ByVal Reason As String, ByVal CUser As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Agreement_Reject"), PeriodID, AgreementUID, Reason, CUser)
    End Function

    Public Function LawAction_GetByLawUID(pUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("LawAction_GetByLawUID"), pUID)
        Return ds.Tables(0)
    End Function

    Public Function LawAction_Save(ByVal UID As Integer, ByVal LawUID As Integer, ByVal ActionUID As String, ByVal OwnerUID As String, ByVal Duedate As String, ActionStatus As String, Comment As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("LawAction_Save"), UID, LawUID, ActionUID, OwnerUID, Duedate, ActionStatus, Comment)
    End Function

    Public Function Event_GetListByAdmin() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Event_GetListByAdmin"))
        Return ds.Tables(0)
    End Function
    Public Function Event_GetListByUser(ByVal LocationUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Event_GetListByUser"), LocationUID)
        Return ds.Tables(0)
    End Function
    Public Function Event_GetListBySupervisor(ByVal UserID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Event_GetListBySupervisor"), UserID)
        Return ds.Tables(0)
    End Function
    Public Function Event_Get() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Event_Get"))
        Return ds.Tables(0)
    End Function


    Public Function Event_GetByAdmin(ByVal UserID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Event_GetByAdmin"), UserID)
        Return ds.Tables(0)
    End Function
    Public Function Event_GetByUser(ByVal LocationUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Event_GetByUser"), LocationUID)
        Return ds.Tables(0)
    End Function
    Public Function Event_GetBySupervisor(ByVal UserID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Event_GetBySupervisor"), UserID)
        Return ds.Tables(0)
    End Function

    Public Function RPT_Agreement_Assessment(Bdate As Integer, Edate As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("RPT_Agreement_Assessment"), Bdate, Edate)
        Return ds.Tables(0)
    End Function


End Class
