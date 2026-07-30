Imports Microsoft.ApplicationBlocks.Data
Public Class CardController
    Inherits BaseClass
    Public ds As DataSet = New DataSet

    Public Function Card_GetAll() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Card_GetAll"))
        Return ds.Tables(0)
    End Function
    Public Function Card_GetByCustomer(ByVal CustomerUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Card_GetByCustomer"), CustomerUID)
        Return ds.Tables(0)
    End Function

    Public Function Card_GetByUID(ByVal pKey As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Card_GetByUID"), pKey)
        Return ds.Tables(0)
    End Function
    Public Function Card_GetByStatus(ByVal pKey As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Card_GetByStatus"), pKey)
        Return ds.Tables(0)
    End Function
    Public Function Card_Search(ByVal pCond As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Card_Search"), pCond)
        Return ds.Tables(0)
    End Function

    Public Function GetCard_ChkDup(ByVal pID As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Card_ChkDup"), pID)
        Return ds.Tables(0)
    End Function
    Public Function Card_Save(ACCNO As String, AgreementNo As String, CustomerUID As Integer, Amount As Double, Interest As Double, Balance As Double, CreateBy As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Card_Save"), ACCNO, AgreementNo, CustomerUID, Amount, Interest, Balance, CreateBy)
    End Function
    Public Function Card_Forward(ACCNO_old As String, AccNo_New As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Card_Forward"), ACCNO_old, AccNo_New)
    End Function

    Public Function Card_Update(ACCNO As String, AdviserName As String, AdviserAge As Integer, AdviserAddress As String, Garantee1 As String, Garantee2 As String, Garantee3 As String, GName1 As String, GAddressNo1 As String, GMoo1 As String, GDistrict1 As String, GAmpher1 As String, GProvince1 As String, GZipCode1 As String, GName2 As String, GAddressNo2 As String, GMoo2 As String, GDistrict2 As String, GAmpher2 As String, GProvince2 As String, GZipCode2 As String, PermitDate As String, EndDate As String, DueDate As String, Start_Amount As Double, Amount_Balance As Double, PAY_INT As Double, INT_RATE As Double, INT_Time As Double, OBJ As String, Remark As String, CreateBy As String, UpdBy As String, LASTUPDATE As String, gtel1 As String, gtel2 As String, balance_Int As Double) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Card_Update"), ACCNO, AdviserName, AdviserAge, AdviserAddress, Garantee1, Garantee2, Garantee3, GName1, GAddressNo1, GMoo1, GDistrict1, GAmpher1, GProvince1, GZipCode1, GName2, GAddressNo2, GMoo2, GDistrict2, GAmpher2, GProvince2, GZipCode2, PermitDate, EndDate, DueDate, Start_Amount, Amount_Balance, PAY_INT, INT_RATE, INT_Time, OBJ, Remark, CreateBy, UpdBy, LASTUPDATE, gtel1, gtel2, balance_Int)

    End Function

    Public Function Card_UpdateBalance(ACCNO As String, DueDate As String, Amount_Balance As Double, UpdBy As String, LASTUPDATE As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Card_UpdateBalance"), ACCNO, DueDate, Amount_Balance, UpdBy, LASTUPDATE)
    End Function

    Public Function Card_IncreaseBalance(ACCNO As String, Amount As Double, Interest As Double, UpdBy As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Card_IncreaseBalance"), ACCNO, Amount, Interest, UpdBy)
    End Function
    Public Function Card_DecreaseBalance(ACCNO As String, Amount As Double, UpdBy As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Card_DecreaseBalance"), ACCNO, Amount, UpdBy)
    End Function

    Public Function Card_UpdateBalance_INT(ACCNO As String, Amount_Balance As Double, UpdBy As String, LASTUPDATE As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Card_UpdateBalance_INT"), ACCNO, Amount_Balance, UpdBy, LASTUPDATE)

    End Function
    Public Function Card_UpdateBalance_INT4Trans(ACCNO As String, Amount_Balance As Double, UpdBy As String, LASTUPDATE As String) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Card_UpdateBalance_INT4Trans"), ACCNO, Amount_Balance, UpdBy, LASTUPDATE)

    End Function

    Public Function Card_Close(ACCNO As String, CloseDate As String, Status As String, UpdBy As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Card_Close"), ACCNO, CloseDate, Status, UpdBy)
    End Function

#Region "Card Detail"

    Public Function CardDetail_GetAll() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CardDetail_GetAll"))
        Return ds.Tables(0)
    End Function
    Public Function CardDetail_GetByCardUID(ByVal CardUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CardDetail_GetByCardUID"), CardUID)
        Return ds.Tables(0)
    End Function
    Public Function CardDetail_GetByCardType(ByVal CardUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CardDetail_GetByCardType"), CardUID)
        Return ds.Tables(0)
    End Function

    Public Function CardDetail_GetByUID(ByVal UID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CardDetail_GetByUID"), UID)
        Return ds.Tables(0)
    End Function

    Public Function CardDetail_GetByZone(ByVal pKey As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CardDetail_GetByZone"), pKey)
        Return ds.Tables(0)
    End Function

    Public Function GetCardDetail_ByBillNO(ByVal pKey As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("GetCardDetail_ByBillNO"), pKey)
        Return ds.Tables(0)
    End Function

    Public Function CardDetail_GetByType(ByVal pKey As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CardDetail_GetByType"), pKey)
        Return ds.Tables(0)
    End Function
    Public Function CardDetail_GetBydate(ByVal pKey As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CardDetail_GetBydate"), pKey)
        Return ds.Tables(0)
    End Function
    Public Function CardDetail_GetBalance(ByVal pCond As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CardDetail_GetBalance"), pCond)
        Return ds.Tables(0)
    End Function
    Public Function CardDetail_GetPayCompleteNoClose(ByVal bdate As String, edate As String, showclose As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CardDetail_GetPayCompleteNoClose"), bdate, edate, showclose)
        Return ds.Tables(0)
    End Function

    Public Function GetCardDetail_4Print(ByVal pdate As String, pbill As String, pacc As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("GetCardDetail_4Print"), pdate, pbill, pacc)
        Return ds.Tables(0)
    End Function
    Public Function GetCardDetail4PrintCard(ByVal pdate As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CardDetail_Get4PrintCard"), pdate)
        Return ds.Tables(0)
    End Function
    Public Function GetCardDetail4PrintCard_item(ByVal pAccno As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CardDetail_Get4PrintCard_item"), pAccno)
        Return ds.Tables(0)
    End Function

    Public Function CardDetail_Get4Trans(ByVal pBdate As String, pEdate As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CardDetail_Get4Trans"), pBdate, pEdate)
        Return ds.Tables(0)
    End Function

    Public Function CardDetail_Get4Trans2SubBook(ByVal pBdate As String, pEdate As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CardDetail_Get4Trans2SubBook"), pBdate, pEdate)
        Return ds.Tables(0)
    End Function

    Public Function GetCardDetail_BySearch(ByVal LskBrk As String, ByVal pCond As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CardDetail_Search"), LskBrk, pCond)
        Return ds.Tables(0)
    End Function

    Public Function GetCardDetail_ChkDup(ByVal pACCNO As String, pDate As String, eDate As String, pCardType As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CardDetail_ChkDup"), pACCNO, pDate, eDate, pCardType)
        Return ds.Tables(0)
    End Function
    Public Function CardDetail_GetLastPayDate(ByVal pACCNO As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CardDetail_GetLastPayDate"), pACCNO)
        Return ds.Tables(0)
    End Function
    Public Function CardDetail_Add(ByVal Code As String, ByVal ACCNO As String, ByVal CardType As String, ByVal PayType As String, ByVal CardDate As String, ByVal EndDate As String, ByVal DayCount As Integer, ByVal Loan_Amount As Double, ByVal Int_Rate As Double, ByVal Int_Total As Double, ByVal Loan_Total As Double, ByVal Card_Desc As String, ByVal SlipPath As String, ByVal BillDate As String, ByVal BankAccount As String, ByVal Descriptions As String, ByVal Remark As String, ByVal CUser As Integer) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("CardDetail_Add"), Code, ACCNO, CardType, PayType, CardDate, EndDate, DayCount, Loan_Amount, Int_Rate, Int_Total, Loan_Total, Card_Desc, SlipPath, BillDate, BankAccount, Descriptions, Remark, CUser)

    End Function
    Public Function CardDetail_UpdateSlipPath(Code As String, sName As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("CardDetail_UpdateSlipPath"), Code, sName)
    End Function
    Public Function CardDetail_Delete(UID As Integer, Remark As String, CUser As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("CardDetail_Delete"), UID, Remark, CUser)
    End Function
    Public Function CardDetail_Close(ByVal CardDetailUID As Integer, Amount As Double, PayUID As Integer, MUser As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("CardDetail_Close"), CardDetailUID, Amount, PayUID, MUser)
    End Function
    Public Function CardDetail_Pay(CardDetailUID As Integer, Amount As Double, PayUID As Integer, MUser As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("CardDetail_Pay"), CardDetailUID, Amount, PayUID, MUser)
    End Function
    Public Function CardDetail_GetForPay(AccNo As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CardDetail_GetForPay"), AccNo)
        Return ds.Tables(0)
    End Function

#End Region

#Region "Payin"

    Public Function Payin_GetAll() As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Payin_GetAll"))
        Return ds.Tables(0)
    End Function
    Public Function Payin_GetByCardUID(ByVal CardUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Payin_GetByCardUID"), CardUID)
        Return ds.Tables(0)
    End Function
    Public Function Payin_GetByCardType(ByVal CardUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Payin_GetByCardType"), CardUID)
        Return ds.Tables(0)
    End Function

    Public Function Payin_GetByUID(ByVal UID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Payin_GetByUID"), UID)
        Return ds.Tables(0)
    End Function
    Public Function Payin_GetUID(ByVal Code As String) As Integer
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Payin_GetUID"), Code)
        If ds.Tables(0).Rows.Count > 0 Then
            Return DBNull2Zero(ds.Tables(0).Rows(0)(0))
        Else
            Return 0
        End If
    End Function
    Public Function Payin_GetByZone(ByVal pKey As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Payin_GetByZone"), pKey)
        Return ds.Tables(0)
    End Function

    Public Function GetPayin_ByBillNO(ByVal pKey As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("GetPayin_ByBillNO"), pKey)
        Return ds.Tables(0)
    End Function

    Public Function Payin_GetByType(ByVal pKey As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Payin_GetByType"), pKey)
        Return ds.Tables(0)
    End Function
    Public Function Payin_GetBydate(ByVal pKey As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Payin_GetBydate"), pKey)
        Return ds.Tables(0)
    End Function
    Public Function Payin_GetBalance(ByVal pCond As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Payin_GetBalance"), pCond)
        Return ds.Tables(0)
    End Function
    Public Function Payin_GetPayCompleteNoClose(ByVal bdate As String, edate As String, showclose As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Payin_GetPayCompleteNoClose"), bdate, edate, showclose)
        Return ds.Tables(0)
    End Function

    Public Function GetPayin_4Print(ByVal pdate As String, pbill As String, pacc As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("GetPayin_4Print"), pdate, pbill, pacc)
        Return ds.Tables(0)
    End Function
    Public Function GetPayin4PrintCard(ByVal pdate As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Payin_Get4PrintCard"), pdate)
        Return ds.Tables(0)
    End Function
    Public Function GetPayin4PrintCard_item(ByVal pAccno As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Payin_Get4PrintCard_item"), pAccno)
        Return ds.Tables(0)
    End Function

    Public Function Payin_Get4Trans(ByVal pBdate As String, pEdate As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Payin_Get4Trans"), pBdate, pEdate)
        Return ds.Tables(0)
    End Function

    Public Function Payin_Get4Trans2SubBook(ByVal pBdate As String, pEdate As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Payin_Get4Trans2SubBook"), pBdate, pEdate)
        Return ds.Tables(0)
    End Function

    Public Function GetPayin_BySearch(ByVal LskBrk As String, ByVal pCond As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Payin_Search"), LskBrk, pCond)
        Return ds.Tables(0)
    End Function

    Public Function GetPayin_ChkDup(ByVal pACCNO As String, pDate As String, eDate As String, pCardType As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Payin_ChkDup"), pACCNO, pDate, eDate, pCardType)
        Return ds.Tables(0)
    End Function
    Public Function Payin_GetLastPayDate(ByVal pACCNO As String) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("Payin_GetLastPayDate"), pACCNO)
        Return ds.Tables(0)
    End Function
    Public Function Payin_Add(ByVal Code As String, ByVal ACCNO As String, ByVal CardType As String, ByVal PayType As String, ByVal PayDate As String, ByVal Amount As Double, ByVal Card_Desc As String, ByVal SlipPath As String, ByVal BankAccount As String, ByVal Descriptions As String, ByVal Remark As String, ByVal CUser As Integer) As Integer

        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Payin_Add"), Code, ACCNO, CardType, PayType, PayDate, Amount, Card_Desc, SlipPath, BankAccount, Descriptions, Remark, CUser)

    End Function
    Public Function Payin_UpdateSlipPath(Code As String, sName As String) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Payin_UpdateSlipPath"), Code, sName)
    End Function
    Public Function Payin_Delete(UID As Integer, Remark As String, CUser As Integer) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("Payin_Delete"), UID, Remark, CUser)
    End Function
    Public Function CardDetailPayin_GetByPayUID(ByVal PayUID As Integer) As DataTable
        ds = SqlHelper.ExecuteDataset(ConnectionString, GetFullyQualifiedName("CardDetailPayin_GetByPayUID"), PayUID)
        Return ds.Tables(0)
    End Function
    Public Function CardDetail_CancelPay(UID As Integer, Amount As Double) As Integer
        Return SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("CardDetail_CancelPay"), UID, Amount)
    End Function

#End Region
End Class
