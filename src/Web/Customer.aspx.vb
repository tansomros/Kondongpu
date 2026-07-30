Imports System.Data

Public Class Customer
    Inherits System.Web.UI.Page
    Dim ctlC As New CustomerController
    Public dtCus As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Request.Cookies("KDP")) Then
            Response.Redirect("Default.aspx")
        End If
        If Not IsPostBack Then
            dtCus = ctlC.Customer_GetAll
        End If
    End Sub
End Class