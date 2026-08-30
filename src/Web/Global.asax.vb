Imports System.Web.Optimization
Imports DevExpress.Data.Filtering
Imports DevExpress.XtraReports.Expressions
Public Class Global_
    Inherits HttpApplication
    Sub Application_Start(sender As Object, e As EventArgs)
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)
        DevExpress.XtraReports.Expressions.CustomFunctions.Register(
           New ThaiBahtFunction()
       )



    End Sub
End Class