
Public Class Download
    Inherits System.Web.UI.Page
    Dim ctlD As New DownloadController
    Public dtFrm As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            dtFrm = ctlD.Download_Get
        End If
    End Sub
End Class