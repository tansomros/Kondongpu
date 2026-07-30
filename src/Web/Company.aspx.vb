Imports System.Data

Public Class Company
    Inherits System.Web.UI.Page
    Dim ctlC As New CompanyController
    Public dtComp As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Request.Cookies("KDP")) Then
            Response.Redirect("Default.aspx")
        End If
        If Not IsPostBack Then
            dtComp = ctlC.Company_Get()
        End If
    End Sub
End Class