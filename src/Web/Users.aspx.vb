
Public Class Users
    Inherits System.Web.UI.Page
    Dim ctlU As New UserController
    Public dtUser As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Request.Cookies("KDP")) Then
            Response.Redirect("Default.aspx")
        End If
        If Not IsPostBack Then
            dtUser = ctlU.User_Get()
        End If
    End Sub
End Class

