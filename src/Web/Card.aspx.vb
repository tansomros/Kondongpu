Imports System.Data

Public Class Card
    Inherits System.Web.UI.Page
    Dim ctlA As New CardController
    Public dtAcc As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Request.Cookies("KDP")) Then
            Response.Redirect("Default.aspx")
        End If
        If Not IsPostBack Then
            LoadCardList()
        End If
    End Sub
    Private Sub LoadCardList()
        dtAcc = ctlA.Card_GetByStatus(ddlStatus.SelectedValue)
    End Sub

    Private Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        LoadCardList()
    End Sub
End Class