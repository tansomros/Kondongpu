<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class rxBillReport
    Inherits DevExpress.XtraReports.UI.XtraReport

    'XtraReport overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Designer
    'It can be modified using the Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim StoredProcQuery1 As DevExpress.DataAccess.Sql.StoredProcQuery = New DevExpress.DataAccess.Sql.StoredProcQuery()
        Dim QueryParameter1 As DevExpress.DataAccess.Sql.QueryParameter = New DevExpress.DataAccess.Sql.QueryParameter()
        Dim StoredProcQuery2 As DevExpress.DataAccess.Sql.StoredProcQuery = New DevExpress.DataAccess.Sql.StoredProcQuery()
        Dim QueryParameter2 As DevExpress.DataAccess.Sql.QueryParameter = New DevExpress.DataAccess.Sql.QueryParameter()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(rxBillReport))
        Dim XrSummary1 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary()
        Dim XrSummary2 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary()
        Dim XrSummary3 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary()
        Dim XrSummary4 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary()
        Dim XrWatermark1 As DevExpress.XtraReports.UI.XRWatermark = New DevExpress.XtraReports.UI.XRWatermark()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand()
        Me.XrLabel22 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel21 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel20 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel19 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrPanel1 = New DevExpress.XtraReports.UI.XRPanel()
        Me.XrLabel18 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel17 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel16 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel15 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel14 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel13 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel12 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel11 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel10 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel8 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel9 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel6 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel7 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel5 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel4 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel3 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.SqlDataSource1 = New DevExpress.DataAccess.Sql.SqlDataSource(Me.components)
        Me.BillNumber = New DevExpress.XtraReports.Parameters.Parameter()
        Me.GroupFooter1 = New DevExpress.XtraReports.UI.GroupFooterBand()
        Me.GroupHeader1 = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.DetailReport = New DevExpress.XtraReports.UI.DetailReportBand()
        Me.Detail1 = New DevExpress.XtraReports.UI.DetailBand()
        Me.XrLabel30 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel29 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel28 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel27 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel26 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel25 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel24 = New DevExpress.XtraReports.UI.XRLabel()
        Me.GroupHeader2 = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.XrLabel23 = New DevExpress.XtraReports.UI.XRLabel()
        Me.GroupFooter2 = New DevExpress.XtraReports.UI.GroupFooterBand()
        Me.XrLabel35 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel34 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel33 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel32 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel31 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLine3 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrLine4 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrLabel36 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel37 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel38 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel39 = New DevExpress.XtraReports.UI.XRLabel()
        Me.ReportFooter = New DevExpress.XtraReports.UI.ReportFooterBand()
        Me.XrLabel40 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel41 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel42 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel43 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel44 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel45 = New DevExpress.XtraReports.UI.XRLabel()
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand()
        Me.XrLabel46 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'TopMargin
        '
        Me.TopMargin.HeightF = 30.0!
        Me.TopMargin.Name = "TopMargin"
        '
        'BottomMargin
        '
        Me.BottomMargin.HeightF = 30.0!
        Me.BottomMargin.Name = "BottomMargin"
        '
        'Detail
        '
        Me.Detail.HeightF = 21.0!
        Me.Detail.Name = "Detail"
        '
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel22, Me.XrLabel21, Me.XrLabel20, Me.XrLabel19, Me.XrPanel1, Me.XrLabel11, Me.XrLabel10, Me.XrLabel8, Me.XrLabel9, Me.XrLabel6, Me.XrLabel7, Me.XrLabel5, Me.XrLabel4, Me.XrLabel3, Me.XrLabel2, Me.XrLabel1})
        Me.PageHeader.HeightF = 178.125!
        Me.PageHeader.Name = "PageHeader"
        '
        'XrLabel22
        '
        Me.XrLabel22.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CustomerAddress]")})
        Me.XrLabel22.LocationFloat = New DevExpress.Utils.PointFloat(47.91667!, 113.7917!)
        Me.XrLabel22.Multiline = True
        Me.XrLabel22.Name = "XrLabel22"
        Me.XrLabel22.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel22.SizeF = New System.Drawing.SizeF(473.0833!, 20.99997!)
        Me.XrLabel22.Text = "XrLabel22"
        '
        'XrLabel21
        '
        Me.XrLabel21.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Tel]")})
        Me.XrLabel21.LocationFloat = New DevExpress.Utils.PointFloat(588.4998!, 113.7917!)
        Me.XrLabel21.Multiline = True
        Me.XrLabel21.Name = "XrLabel21"
        Me.XrLabel21.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel21.SizeF = New System.Drawing.SizeF(176.4999!, 23.0!)
        Me.XrLabel21.Text = "XrLabel21"
        '
        'XrLabel20
        '
        Me.XrLabel20.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CustomerID]")})
        Me.XrLabel20.LocationFloat = New DevExpress.Utils.PointFloat(590.5!, 91.99999!)
        Me.XrLabel20.Multiline = True
        Me.XrLabel20.Name = "XrLabel20"
        Me.XrLabel20.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel20.SizeF = New System.Drawing.SizeF(100.0!, 21.79169!)
        Me.XrLabel20.Text = "XrLabel20"
        '
        'XrLabel19
        '
        Me.XrLabel19.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[BillDtt]")})
        Me.XrLabel19.LocationFloat = New DevExpress.Utils.PointFloat(649.7084!, 56.0!)
        Me.XrLabel19.Multiline = True
        Me.XrLabel19.Name = "XrLabel19"
        Me.XrLabel19.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel19.SizeF = New System.Drawing.SizeF(115.2914!, 23.0!)
        Me.XrLabel19.StylePriority.UseTextAlignment = False
        Me.XrLabel19.Text = "XrLabel19"
        Me.XrLabel19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrPanel1
        '
        Me.XrPanel1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.XrPanel1.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrPanel1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel18, Me.XrLabel17, Me.XrLabel16, Me.XrLabel15, Me.XrLabel14, Me.XrLabel13, Me.XrLabel12})
        Me.XrPanel1.LocationFloat = New DevExpress.Utils.PointFloat(0!, 146.6667!)
        Me.XrPanel1.Name = "XrPanel1"
        Me.XrPanel1.SizeF = New System.Drawing.SizeF(767.0!, 30.0!)
        Me.XrPanel1.StylePriority.UseBackColor = False
        Me.XrPanel1.StylePriority.UseBorders = False
        '
        'XrLabel18
        '
        Me.XrLabel18.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel18.Font = New DevExpress.Drawing.DXFont("Sarabun", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel18.LocationFloat = New DevExpress.Utils.PointFloat(649.7084!, 4.416687!)
        Me.XrLabel18.Multiline = True
        Me.XrLabel18.Name = "XrLabel18"
        Me.XrLabel18.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 10, 0, 0, 100.0!)
        Me.XrLabel18.SizeF = New System.Drawing.SizeF(115.2916!, 20.99998!)
        Me.XrLabel18.StylePriority.UseBorders = False
        Me.XrLabel18.StylePriority.UseFont = False
        Me.XrLabel18.StylePriority.UsePadding = False
        Me.XrLabel18.StylePriority.UseTextAlignment = False
        Me.XrLabel18.Text = "คงเหลือ"
        Me.XrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel17
        '
        Me.XrLabel17.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel17.Font = New DevExpress.Drawing.DXFont("Sarabun", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel17.LocationFloat = New DevExpress.Utils.PointFloat(549.7084!, 4.416687!)
        Me.XrLabel17.Multiline = True
        Me.XrLabel17.Name = "XrLabel17"
        Me.XrLabel17.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel17.SizeF = New System.Drawing.SizeF(99.99998!, 20.99998!)
        Me.XrLabel17.StylePriority.UseBorders = False
        Me.XrLabel17.StylePriority.UseFont = False
        Me.XrLabel17.StylePriority.UseTextAlignment = False
        Me.XrLabel17.Text = "หักน้ำมัน"
        Me.XrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel16
        '
        Me.XrLabel16.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel16.Font = New DevExpress.Drawing.DXFont("Sarabun", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel16.LocationFloat = New DevExpress.Utils.PointFloat(428.1249!, 4.416687!)
        Me.XrLabel16.Multiline = True
        Me.XrLabel16.Name = "XrLabel16"
        Me.XrLabel16.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel16.SizeF = New System.Drawing.SizeF(121.5834!, 20.99998!)
        Me.XrLabel16.StylePriority.UseBorders = False
        Me.XrLabel16.StylePriority.UseFont = False
        Me.XrLabel16.StylePriority.UseTextAlignment = False
        Me.XrLabel16.Text = "เป็นเงิน"
        Me.XrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel15
        '
        Me.XrLabel15.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel15.Font = New DevExpress.Drawing.DXFont("Sarabun", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel15.LocationFloat = New DevExpress.Utils.PointFloat(316.6666!, 4.416687!)
        Me.XrLabel15.Multiline = True
        Me.XrLabel15.Name = "XrLabel15"
        Me.XrLabel15.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel15.SizeF = New System.Drawing.SizeF(111.4583!, 20.99998!)
        Me.XrLabel15.StylePriority.UseBorders = False
        Me.XrLabel15.StylePriority.UseFont = False
        Me.XrLabel15.StylePriority.UseTextAlignment = False
        Me.XrLabel15.Text = "ราคา (บาท/ตัน)"
        Me.XrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel14
        '
        Me.XrLabel14.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel14.Font = New DevExpress.Drawing.DXFont("Sarabun", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel14.LocationFloat = New DevExpress.Utils.PointFloat(230.2083!, 4.416687!)
        Me.XrLabel14.Multiline = True
        Me.XrLabel14.Name = "XrLabel14"
        Me.XrLabel14.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel14.SizeF = New System.Drawing.SizeF(86.4583!, 20.99998!)
        Me.XrLabel14.StylePriority.UseBorders = False
        Me.XrLabel14.StylePriority.UseFont = False
        Me.XrLabel14.StylePriority.UseTextAlignment = False
        Me.XrLabel14.Text = "น้ำหนัก (ตัน)"
        Me.XrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel13
        '
        Me.XrLabel13.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel13.Font = New DevExpress.Drawing.DXFont("Sarabun", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel13.LocationFloat = New DevExpress.Utils.PointFloat(113.4583!, 4.416687!)
        Me.XrLabel13.Multiline = True
        Me.XrLabel13.Name = "XrLabel13"
        Me.XrLabel13.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel13.SizeF = New System.Drawing.SizeF(116.75!, 20.99998!)
        Me.XrLabel13.StylePriority.UseBorders = False
        Me.XrLabel13.StylePriority.UseFont = False
        Me.XrLabel13.StylePriority.UseTextAlignment = False
        Me.XrLabel13.Text = "ทะเบียนรถ"
        Me.XrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel12
        '
        Me.XrLabel12.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel12.Font = New DevExpress.Drawing.DXFont("Sarabun", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel12.LocationFloat = New DevExpress.Utils.PointFloat(2.000014!, 4.416687!)
        Me.XrLabel12.Multiline = True
        Me.XrLabel12.Name = "XrLabel12"
        Me.XrLabel12.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel12.SizeF = New System.Drawing.SizeF(111.4583!, 20.99998!)
        Me.XrLabel12.StylePriority.UseBorders = False
        Me.XrLabel12.StylePriority.UseFont = False
        Me.XrLabel12.StylePriority.UseTextAlignment = False
        Me.XrLabel12.Text = "วันที่ส่งอ้อย"
        Me.XrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel11
        '
        Me.XrLabel11.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[BillNumber]")})
        Me.XrLabel11.LocationFloat = New DevExpress.Utils.PointFloat(649.7084!, 34.99999!)
        Me.XrLabel11.Multiline = True
        Me.XrLabel11.Name = "XrLabel11"
        Me.XrLabel11.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel11.SizeF = New System.Drawing.SizeF(117.2916!, 21.0!)
        Me.XrLabel11.Text = "XrLabel11"
        '
        'XrLabel10
        '
        Me.XrLabel10.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "Concat([PrefixName], [FirstName],' ',[LastName],' (',[NickName],')')")})
        Me.XrLabel10.Font = New DevExpress.Drawing.DXFont("Sarabun", 10.0!)
        Me.XrLabel10.LocationFloat = New DevExpress.Utils.PointFloat(100.0!, 92.79167!)
        Me.XrLabel10.Multiline = True
        Me.XrLabel10.Name = "XrLabel10"
        Me.XrLabel10.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel10.SizeF = New System.Drawing.SizeF(387.5!, 21.0!)
        Me.XrLabel10.StylePriority.UseFont = False
        Me.XrLabel10.Text = "ชื่อ-นามสกุล"
        '
        'XrLabel8
        '
        Me.XrLabel8.Font = New DevExpress.Drawing.DXFont("Sarabun", 10.0!)
        Me.XrLabel8.LocationFloat = New DevExpress.Utils.PointFloat(520.9999!, 92.79167!)
        Me.XrLabel8.Multiline = True
        Me.XrLabel8.Name = "XrLabel8"
        Me.XrLabel8.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel8.SizeF = New System.Drawing.SizeF(69.50003!, 21.0!)
        Me.XrLabel8.StylePriority.UseFont = False
        Me.XrLabel8.Text = "รหัสลูกค้า"
        '
        'XrLabel9
        '
        Me.XrLabel9.Font = New DevExpress.Drawing.DXFont("Sarabun", 10.0!)
        Me.XrLabel9.LocationFloat = New DevExpress.Utils.PointFloat(520.9999!, 113.7917!)
        Me.XrLabel9.Multiline = True
        Me.XrLabel9.Name = "XrLabel9"
        Me.XrLabel9.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel9.SizeF = New System.Drawing.SizeF(43.99976!, 21.0!)
        Me.XrLabel9.StylePriority.UseFont = False
        Me.XrLabel9.Text = "โทร."
        '
        'XrLabel6
        '
        Me.XrLabel6.Font = New DevExpress.Drawing.DXFont("Sarabun", 10.0!)
        Me.XrLabel6.LocationFloat = New DevExpress.Utils.PointFloat(0!, 92.79167!)
        Me.XrLabel6.Multiline = True
        Me.XrLabel6.Name = "XrLabel6"
        Me.XrLabel6.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel6.SizeF = New System.Drawing.SizeF(99.99998!, 21.0!)
        Me.XrLabel6.StylePriority.UseFont = False
        Me.XrLabel6.Text = "ชื่อ-นามสกุล"
        '
        'XrLabel7
        '
        Me.XrLabel7.Font = New DevExpress.Drawing.DXFont("Sarabun", 10.0!)
        Me.XrLabel7.LocationFloat = New DevExpress.Utils.PointFloat(0!, 113.7917!)
        Me.XrLabel7.Multiline = True
        Me.XrLabel7.Name = "XrLabel7"
        Me.XrLabel7.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel7.SizeF = New System.Drawing.SizeF(47.91664!, 21.0!)
        Me.XrLabel7.StylePriority.UseFont = False
        Me.XrLabel7.Text = "ที่อยู่"
        '
        'XrLabel5
        '
        Me.XrLabel5.Font = New DevExpress.Drawing.DXFont("Sarabun", 10.0!)
        Me.XrLabel5.LocationFloat = New DevExpress.Utils.PointFloat(601.0416!, 55.99999!)
        Me.XrLabel5.Multiline = True
        Me.XrLabel5.Name = "XrLabel5"
        Me.XrLabel5.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel5.SizeF = New System.Drawing.SizeF(48.66675!, 21.0!)
        Me.XrLabel5.StylePriority.UseFont = False
        Me.XrLabel5.Text = "วันที่"
        '
        'XrLabel4
        '
        Me.XrLabel4.Font = New DevExpress.Drawing.DXFont("Sarabun", 10.0!)
        Me.XrLabel4.LocationFloat = New DevExpress.Utils.PointFloat(601.0416!, 34.99999!)
        Me.XrLabel4.Multiline = True
        Me.XrLabel4.Name = "XrLabel4"
        Me.XrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel4.SizeF = New System.Drawing.SizeF(48.66675!, 21.0!)
        Me.XrLabel4.StylePriority.UseFont = False
        Me.XrLabel4.Text = "เลขที่"
        '
        'XrLabel3
        '
        Me.XrLabel3.Font = New DevExpress.Drawing.DXFont("Sarabun", 16.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel3.LocationFloat = New DevExpress.Utils.PointFloat(497.9167!, 0!)
        Me.XrLabel3.Multiline = True
        Me.XrLabel3.Name = "XrLabel3"
        Me.XrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel3.SizeF = New System.Drawing.SizeF(269.0833!, 35.0!)
        Me.XrLabel3.StylePriority.UseFont = False
        Me.XrLabel3.StylePriority.UseTextAlignment = False
        Me.XrLabel3.Text = "ใบเสร็จรับเงิน/บิลเงินสด"
        Me.XrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel2
        '
        Me.XrLabel2.Font = New DevExpress.Drawing.DXFont("Sarabun", 10.0!)
        Me.XrLabel2.LocationFloat = New DevExpress.Utils.PointFloat(0!, 34.99999!)
        Me.XrLabel2.Multiline = True
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel2.SizeF = New System.Drawing.SizeF(395.8333!, 41.25001!)
        Me.XrLabel2.StylePriority.UseFont = False
        Me.XrLabel2.Text = "37/4 หมู่ 14 ต.ด่านขุนทด อ.ด่านขุนทด จ.นครราชสีมา 30210" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "โทร. 088-5826767,086-265" &
    "3911"
        '
        'XrLabel1
        '
        Me.XrLabel1.Font = New DevExpress.Drawing.DXFont("Sarabun", 16.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.XrLabel1.Multiline = True
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel1.SizeF = New System.Drawing.SizeF(100.0!, 35.0!)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.Text = "ฅนดงพุ"
        '
        'SqlDataSource1
        '
        Me.SqlDataSource1.ConnectionName = "KondongpuConnection"
        Me.SqlDataSource1.Name = "SqlDataSource1"
        StoredProcQuery1.Name = "Bill"
        QueryParameter1.Name = "@BillNumber"
        QueryParameter1.Type = GetType(String)
        StoredProcQuery1.Parameters.AddRange(New DevExpress.DataAccess.Sql.QueryParameter() {QueryParameter1})
        StoredProcQuery1.StoredProcName = "RPT_Bill"
        StoredProcQuery2.Name = "BillDetail"
        QueryParameter2.Name = "@BillNumber"
        QueryParameter2.Type = GetType(String)
        StoredProcQuery2.Parameters.AddRange(New DevExpress.DataAccess.Sql.QueryParameter() {QueryParameter2})
        StoredProcQuery2.StoredProcName = "RPT_BillDetail"
        Me.SqlDataSource1.Queries.AddRange(New DevExpress.DataAccess.Sql.SqlQuery() {StoredProcQuery1, StoredProcQuery2})
        Me.SqlDataSource1.ResultSchemaSerializable = resources.GetString("SqlDataSource1.ResultSchemaSerializable")
        '
        'BillNumber
        '
        Me.BillNumber.Description = "Bill Number"
        Me.BillNumber.Name = "BillNumber"
        '
        'GroupFooter1
        '
        Me.GroupFooter1.HeightF = 26.54165!
        Me.GroupFooter1.Name = "GroupFooter1"
        '
        'GroupHeader1
        '
        Me.GroupHeader1.HeightF = 19.16669!
        Me.GroupHeader1.Name = "GroupHeader1"
        '
        'DetailReport
        '
        Me.DetailReport.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail1, Me.GroupHeader2, Me.GroupFooter2})
        Me.DetailReport.DataMember = "BillDetail"
        Me.DetailReport.DataSource = Me.SqlDataSource1
        Me.DetailReport.Level = 0
        Me.DetailReport.Name = "DetailReport"
        '
        'Detail1
        '
        Me.Detail1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel30, Me.XrLabel29, Me.XrLabel28, Me.XrLabel27, Me.XrLabel26, Me.XrLabel25, Me.XrLabel24})
        Me.Detail1.HeightF = 24.0!
        Me.Detail1.Name = "Detail1"
        '
        'XrLabel30
        '
        Me.XrLabel30.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NetBalance]"), New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Visible", "Iif([BillFlag]='R',True,False)" & Global.Microsoft.VisualBasic.ChrW(10))})
        Me.XrLabel30.Font = New DevExpress.Drawing.DXFont("Sarabun", 10.0!)
        Me.XrLabel30.LocationFloat = New DevExpress.Utils.PointFloat(649.7084!, 0!)
        Me.XrLabel30.Multiline = True
        Me.XrLabel30.Name = "XrLabel30"
        Me.XrLabel30.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 0, 100.0!)
        Me.XrLabel30.SizeF = New System.Drawing.SizeF(112.0!, 24.0!)
        Me.XrLabel30.StylePriority.UseFont = False
        Me.XrLabel30.StylePriority.UsePadding = False
        Me.XrLabel30.StylePriority.UseTextAlignment = False
        Me.XrLabel30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrLabel30.TextFormatString = "{0:#,###.#0}"
        '
        'XrLabel29
        '
        Me.XrLabel29.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[GasPrice]"), New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Visible", "Iif([BillFlag]='R',True,False)" & Global.Microsoft.VisualBasic.ChrW(10))})
        Me.XrLabel29.Font = New DevExpress.Drawing.DXFont("Sarabun", 10.0!)
        Me.XrLabel29.LocationFloat = New DevExpress.Utils.PointFloat(548.6544!, 0!)
        Me.XrLabel29.Multiline = True
        Me.XrLabel29.Name = "XrLabel29"
        Me.XrLabel29.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 0, 100.0!)
        Me.XrLabel29.SizeF = New System.Drawing.SizeF(100.1401!, 24.0!)
        Me.XrLabel29.StylePriority.UseFont = False
        Me.XrLabel29.StylePriority.UsePadding = False
        Me.XrLabel29.StylePriority.UseTextAlignment = False
        Me.XrLabel29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrLabel29.TextFormatString = "{0:#,###.#0}"
        '
        'XrLabel28
        '
        Me.XrLabel28.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NetPrice]")})
        Me.XrLabel28.Font = New DevExpress.Drawing.DXFont("Sarabun", 10.0!)
        Me.XrLabel28.LocationFloat = New DevExpress.Utils.PointFloat(428.1249!, 0!)
        Me.XrLabel28.Multiline = True
        Me.XrLabel28.Name = "XrLabel28"
        Me.XrLabel28.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 0, 100.0!)
        Me.XrLabel28.SizeF = New System.Drawing.SizeF(120.5295!, 24.0!)
        Me.XrLabel28.StylePriority.UseFont = False
        Me.XrLabel28.StylePriority.UsePadding = False
        Me.XrLabel28.StylePriority.UseTextAlignment = False
        Me.XrLabel28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrLabel28.TextFormatString = "{0:#,###.#0}"
        '
        'XrLabel27
        '
        Me.XrLabel27.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[UnitPrice]"), New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Visible", "Iif([BillFlag]='R',True,False)")})
        Me.XrLabel27.Font = New DevExpress.Drawing.DXFont("Sarabun", 10.0!)
        Me.XrLabel27.LocationFloat = New DevExpress.Utils.PointFloat(316.6666!, 0!)
        Me.XrLabel27.Multiline = True
        Me.XrLabel27.Name = "XrLabel27"
        Me.XrLabel27.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 0, 100.0!)
        Me.XrLabel27.SizeF = New System.Drawing.SizeF(110.2345!, 24.0!)
        Me.XrLabel27.StylePriority.UseFont = False
        Me.XrLabel27.StylePriority.UsePadding = False
        Me.XrLabel27.StylePriority.UseTextAlignment = False
        Me.XrLabel27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrLabel27.TextFormatString = "{0:#,###.#0}"
        '
        'XrLabel26
        '
        Me.XrLabel26.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Weight]"), New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Visible", "Iif([BillFlag]='R',True,False)")})
        Me.XrLabel26.Font = New DevExpress.Drawing.DXFont("Sarabun", 10.0!)
        Me.XrLabel26.LocationFloat = New DevExpress.Utils.PointFloat(230.2083!, 0!)
        Me.XrLabel26.Multiline = True
        Me.XrLabel26.Name = "XrLabel26"
        Me.XrLabel26.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 0, 100.0!)
        Me.XrLabel26.SizeF = New System.Drawing.SizeF(86.45824!, 24.0!)
        Me.XrLabel26.StylePriority.UseFont = False
        Me.XrLabel26.StylePriority.UsePadding = False
        Me.XrLabel26.StylePriority.UseTextAlignment = False
        Me.XrLabel26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrLabel26.TextFormatString = "{0:#,###.#0}"
        '
        'XrLabel25
        '
        Me.XrLabel25.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CarRegisNumber]"), New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Visible", "Iif([BillFlag]='R',True,False)" & Global.Microsoft.VisualBasic.ChrW(10))})
        Me.XrLabel25.Font = New DevExpress.Drawing.DXFont("Sarabun", 10.0!)
        Me.XrLabel25.LocationFloat = New DevExpress.Utils.PointFloat(113.4583!, 0!)
        Me.XrLabel25.Multiline = True
        Me.XrLabel25.Name = "XrLabel25"
        Me.XrLabel25.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 0, 100.0!)
        Me.XrLabel25.SizeF = New System.Drawing.SizeF(116.75!, 24.0!)
        Me.XrLabel25.StylePriority.UseFont = False
        Me.XrLabel25.StylePriority.UsePadding = False
        Me.XrLabel25.StylePriority.UseTextAlignment = False
        Me.XrLabel25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel24
        '
        Me.XrLabel24.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "Iif([BillFlag]='R',[SendDtt],[DeductName])")})
        Me.XrLabel24.Font = New DevExpress.Drawing.DXFont("Sarabun", 10.0!)
        Me.XrLabel24.LocationFloat = New DevExpress.Utils.PointFloat(20.00001!, 0!)
        Me.XrLabel24.Multiline = True
        Me.XrLabel24.Name = "XrLabel24"
        Me.XrLabel24.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 0, 100.0!)
        Me.XrLabel24.SizeF = New System.Drawing.SizeF(173.6666!, 24.0!)
        Me.XrLabel24.StylePriority.UseFont = False
        Me.XrLabel24.StylePriority.UsePadding = False
        Me.XrLabel24.StylePriority.UseTextAlignment = False
        Me.XrLabel24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'GroupHeader2
        '
        Me.GroupHeader2.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel23})
        Me.GroupHeader2.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("BillFlag", DevExpress.XtraReports.UI.XRColumnSortOrder.Descending)})
        Me.GroupHeader2.HeightF = 25.66665!
        Me.GroupHeader2.Name = "GroupHeader2"
        '
        'XrLabel23
        '
        Me.XrLabel23.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "Iif([BillFlag]='R', 'รายการส่งอ้อย','รายการหัก' )")})
        Me.XrLabel23.Font = New DevExpress.Drawing.DXFont("Sarabun", 10.0!)
        Me.XrLabel23.LocationFloat = New DevExpress.Utils.PointFloat(10.00001!, 4.666646!)
        Me.XrLabel23.Multiline = True
        Me.XrLabel23.Name = "XrLabel23"
        Me.XrLabel23.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel23.SizeF = New System.Drawing.SizeF(220.2083!, 21.0!)
        Me.XrLabel23.StylePriority.UseFont = False
        '
        'GroupFooter2
        '
        Me.GroupFooter2.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel35, Me.XrLabel34, Me.XrLabel33, Me.XrLabel32, Me.XrLabel31, Me.XrLine3, Me.XrLine4})
        Me.GroupFooter2.HeightF = 38.49996!
        Me.GroupFooter2.Name = "GroupFooter2"
        '
        'XrLabel35
        '
        Me.XrLabel35.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Visible", "Iif([BillFlag]='R',True,False)"), New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([NetBalance])")})
        Me.XrLabel35.Font = New DevExpress.Drawing.DXFont("Sarabun", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel35.LocationFloat = New DevExpress.Utils.PointFloat(649.7084!, 7.499949!)
        Me.XrLabel35.Multiline = True
        Me.XrLabel35.Name = "XrLabel35"
        Me.XrLabel35.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 0, 100.0!)
        Me.XrLabel35.SizeF = New System.Drawing.SizeF(111.9999!, 24.0!)
        Me.XrLabel35.StylePriority.UseFont = False
        Me.XrLabel35.StylePriority.UsePadding = False
        Me.XrLabel35.StylePriority.UseTextAlignment = False
        XrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.XrLabel35.Summary = XrSummary1
        Me.XrLabel35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrLabel35.TextFormatString = "{0:#,###.#0}"
        '
        'XrLabel34
        '
        Me.XrLabel34.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Visible", "Iif([BillFlag]='R',True,False)"), New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([GasPrice])")})
        Me.XrLabel34.Font = New DevExpress.Drawing.DXFont("Sarabun", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel34.LocationFloat = New DevExpress.Utils.PointFloat(549.7084!, 7.499949!)
        Me.XrLabel34.Multiline = True
        Me.XrLabel34.Name = "XrLabel34"
        Me.XrLabel34.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 0, 100.0!)
        Me.XrLabel34.SizeF = New System.Drawing.SizeF(100.0!, 24.0!)
        Me.XrLabel34.StylePriority.UseFont = False
        Me.XrLabel34.StylePriority.UsePadding = False
        Me.XrLabel34.StylePriority.UseTextAlignment = False
        XrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.XrLabel34.Summary = XrSummary2
        Me.XrLabel34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrLabel34.TextFormatString = "{0:#,###.#0}"
        '
        'XrLabel33
        '
        Me.XrLabel33.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "Iif([BillFlag]='R', sumSum([NetPrice]),[TotalDeduct])" & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(10) & " ")})
        Me.XrLabel33.Font = New DevExpress.Drawing.DXFont("Sarabun", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel33.LocationFloat = New DevExpress.Utils.PointFloat(428.1249!, 7.499949!)
        Me.XrLabel33.Multiline = True
        Me.XrLabel33.Name = "XrLabel33"
        Me.XrLabel33.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 0, 100.0!)
        Me.XrLabel33.SizeF = New System.Drawing.SizeF(121.5834!, 24.0!)
        Me.XrLabel33.StylePriority.UseFont = False
        Me.XrLabel33.StylePriority.UsePadding = False
        Me.XrLabel33.StylePriority.UseTextAlignment = False
        XrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.XrLabel33.Summary = XrSummary3
        Me.XrLabel33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrLabel33.TextFormatString = "{0:#,###.#0}"
        '
        'XrLabel32
        '
        Me.XrLabel32.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([Weight])"), New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Visible", "Iif([BillFlag]='R',True,False)")})
        Me.XrLabel32.Font = New DevExpress.Drawing.DXFont("Sarabun", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel32.LocationFloat = New DevExpress.Utils.PointFloat(230.2083!, 7.499949!)
        Me.XrLabel32.Multiline = True
        Me.XrLabel32.Name = "XrLabel32"
        Me.XrLabel32.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 0, 100.0!)
        Me.XrLabel32.SizeF = New System.Drawing.SizeF(86.45824!, 24.0!)
        Me.XrLabel32.StylePriority.UseFont = False
        Me.XrLabel32.StylePriority.UsePadding = False
        Me.XrLabel32.StylePriority.UseTextAlignment = False
        XrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.XrLabel32.Summary = XrSummary4
        Me.XrLabel32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrLabel32.TextFormatString = "{0:#,###.#0}"
        '
        'XrLabel31
        '
        Me.XrLabel31.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "Iif([BillFlag]='R', 'รวมรับ','รวมหัก' )")})
        Me.XrLabel31.Font = New DevExpress.Drawing.DXFont("Sarabun", 10.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel31.LocationFloat = New DevExpress.Utils.PointFloat(113.4583!, 7.499949!)
        Me.XrLabel31.Multiline = True
        Me.XrLabel31.Name = "XrLabel31"
        Me.XrLabel31.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel31.SizeF = New System.Drawing.SizeF(116.75!, 24.00001!)
        Me.XrLabel31.StylePriority.UseFont = False
        '
        'XrLine3
        '
        Me.XrLine3.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.XrLine3.Name = "XrLine3"
        Me.XrLine3.SizeF = New System.Drawing.SizeF(764.9996!, 2.0!)
        '
        'XrLine4
        '
        Me.XrLine4.LocationFloat = New DevExpress.Utils.PointFloat(2.000366!, 35.0!)
        Me.XrLine4.Name = "XrLine4"
        Me.XrLine4.SizeF = New System.Drawing.SizeF(764.9996!, 2.0!)
        '
        'XrLabel36
        '
        Me.XrLabel36.Font = New DevExpress.Drawing.DXFont("Sarabun", 11.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel36.LocationFloat = New DevExpress.Utils.PointFloat(496.693!, 9.999974!)
        Me.XrLabel36.Multiline = True
        Me.XrLabel36.Name = "XrLabel36"
        Me.XrLabel36.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 0, 100.0!)
        Me.XrLabel36.SizeF = New System.Drawing.SizeF(67.08292!, 30.00002!)
        Me.XrLabel36.StylePriority.UseFont = False
        Me.XrLabel36.StylePriority.UsePadding = False
        Me.XrLabel36.StylePriority.UseTextAlignment = False
        Me.XrLabel36.Text = "คงเหลือ"
        Me.XrLabel36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel37
        '
        Me.XrLabel37.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TotalBalance]")})
        Me.XrLabel37.Font = New DevExpress.Drawing.DXFont("Sarabun", 11.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel37.LocationFloat = New DevExpress.Utils.PointFloat(563.7759!, 9.999974!)
        Me.XrLabel37.Multiline = True
        Me.XrLabel37.Name = "XrLabel37"
        Me.XrLabel37.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 0, 100.0!)
        Me.XrLabel37.SizeF = New System.Drawing.SizeF(144.2089!, 29.99997!)
        Me.XrLabel37.StylePriority.UseFont = False
        Me.XrLabel37.StylePriority.UsePadding = False
        Me.XrLabel37.StylePriority.UseTextAlignment = False
        Me.XrLabel37.Text = "XrLabel37"
        Me.XrLabel37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrLabel37.TextFormatString = "{0:#,###.#0}"
        '
        'XrLabel38
        '
        Me.XrLabel38.Font = New DevExpress.Drawing.DXFont("Sarabun", 11.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel38.LocationFloat = New DevExpress.Utils.PointFloat(707.9847!, 9.999974!)
        Me.XrLabel38.Multiline = True
        Me.XrLabel38.Name = "XrLabel38"
        Me.XrLabel38.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 0, 100.0!)
        Me.XrLabel38.SizeF = New System.Drawing.SizeF(52.49963!, 30.00002!)
        Me.XrLabel38.StylePriority.UseFont = False
        Me.XrLabel38.StylePriority.UsePadding = False
        Me.XrLabel38.StylePriority.UseTextAlignment = False
        Me.XrLabel38.Text = "บาท"
        Me.XrLabel38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel39
        '
        Me.XrLabel39.BackColor = System.Drawing.Color.WhiteSmoke
        Me.XrLabel39.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "ThaiBaht([TotalBalance])")})
        Me.XrLabel39.Font = New DevExpress.Drawing.DXFont("Sarabun", 11.0!, DevExpress.Drawing.DXFontStyle.Bold)
        Me.XrLabel39.LocationFloat = New DevExpress.Utils.PointFloat(8.776219!, 9.999974!)
        Me.XrLabel39.Multiline = True
        Me.XrLabel39.Name = "XrLabel39"
        Me.XrLabel39.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 0, 100.0!)
        Me.XrLabel39.SizeF = New System.Drawing.SizeF(418.1249!, 30.0!)
        Me.XrLabel39.StylePriority.UseBackColor = False
        Me.XrLabel39.StylePriority.UseFont = False
        Me.XrLabel39.StylePriority.UsePadding = False
        Me.XrLabel39.StylePriority.UseTextAlignment = False
        Me.XrLabel39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'ReportFooter
        '
        Me.ReportFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel43, Me.XrLabel44, Me.XrLabel45, Me.XrLabel42, Me.XrLabel41, Me.XrLabel40, Me.XrLabel36, Me.XrLabel38, Me.XrLabel37, Me.XrLabel39})
        Me.ReportFooter.HeightF = 178.1667!
        Me.ReportFooter.Name = "ReportFooter"
        '
        'XrLabel40
        '
        Me.XrLabel40.Font = New DevExpress.Drawing.DXFont("Sarabun Thin", 10.0!)
        Me.XrLabel40.LocationFloat = New DevExpress.Utils.PointFloat(10.00001!, 112.7501!)
        Me.XrLabel40.Multiline = True
        Me.XrLabel40.Name = "XrLabel40"
        Me.XrLabel40.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel40.SizeF = New System.Drawing.SizeF(268.75!, 21.00001!)
        Me.XrLabel40.StylePriority.UseFont = False
        Me.XrLabel40.StylePriority.UseTextAlignment = False
        Me.XrLabel40.Text = "....................................................................."
        Me.XrLabel40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel41
        '
        Me.XrLabel41.Font = New DevExpress.Drawing.DXFont("Sarabun", 10.0!)
        Me.XrLabel41.LocationFloat = New DevExpress.Utils.PointFloat(8.776219!, 154.7501!)
        Me.XrLabel41.Multiline = True
        Me.XrLabel41.Name = "XrLabel41"
        Me.XrLabel41.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel41.SizeF = New System.Drawing.SizeF(268.75!, 21.00001!)
        Me.XrLabel41.StylePriority.UseFont = False
        Me.XrLabel41.StylePriority.UseTextAlignment = False
        Me.XrLabel41.Text = "ผู้ออกใบเสร็จ"
        Me.XrLabel41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel42
        '
        Me.XrLabel42.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "([UserName])")})
        Me.XrLabel42.Font = New DevExpress.Drawing.DXFont("Sarabun Thin", 10.0!)
        Me.XrLabel42.LocationFloat = New DevExpress.Utils.PointFloat(10.00001!, 133.7501!)
        Me.XrLabel42.Multiline = True
        Me.XrLabel42.Name = "XrLabel42"
        Me.XrLabel42.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel42.SizeF = New System.Drawing.SizeF(268.75!, 21.00001!)
        Me.XrLabel42.StylePriority.UseFont = False
        Me.XrLabel42.StylePriority.UseTextAlignment = False
        Me.XrLabel42.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel43
        '
        Me.XrLabel43.Font = New DevExpress.Drawing.DXFont("Sarabun Thin", 10.0!)
        Me.XrLabel43.LocationFloat = New DevExpress.Utils.PointFloat(488.25!, 112.7501!)
        Me.XrLabel43.Multiline = True
        Me.XrLabel43.Name = "XrLabel43"
        Me.XrLabel43.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel43.SizeF = New System.Drawing.SizeF(268.75!, 21.00001!)
        Me.XrLabel43.StylePriority.UseFont = False
        Me.XrLabel43.StylePriority.UseTextAlignment = False
        Me.XrLabel43.Text = "....................................................................."
        Me.XrLabel43.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel44
        '
        Me.XrLabel44.Font = New DevExpress.Drawing.DXFont("Sarabun", 10.0!)
        Me.XrLabel44.LocationFloat = New DevExpress.Utils.PointFloat(487.0262!, 154.7501!)
        Me.XrLabel44.Multiline = True
        Me.XrLabel44.Name = "XrLabel44"
        Me.XrLabel44.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel44.SizeF = New System.Drawing.SizeF(268.75!, 21.00001!)
        Me.XrLabel44.StylePriority.UseFont = False
        Me.XrLabel44.StylePriority.UseTextAlignment = False
        Me.XrLabel44.Text = "ผู้รับเงิน"
        Me.XrLabel44.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel45
        '
        Me.XrLabel45.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "Concat('(',[PrefixName], [FirstName],' ',[LastName],')')" & Global.Microsoft.VisualBasic.ChrW(10))})
        Me.XrLabel45.Font = New DevExpress.Drawing.DXFont("Sarabun Thin", 10.0!)
        Me.XrLabel45.LocationFloat = New DevExpress.Utils.PointFloat(488.25!, 133.7501!)
        Me.XrLabel45.Multiline = True
        Me.XrLabel45.Name = "XrLabel45"
        Me.XrLabel45.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel45.SizeF = New System.Drawing.SizeF(268.75!, 21.00001!)
        Me.XrLabel45.StylePriority.UseFont = False
        Me.XrLabel45.StylePriority.UseTextAlignment = False
        Me.XrLabel45.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'PageFooter
        '
        Me.PageFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrPageInfo1, Me.XrLabel46})
        Me.PageFooter.HeightF = 21.00002!
        Me.PageFooter.Name = "PageFooter"
        '
        'XrLabel46
        '
        Me.XrLabel46.ExpressionBindings.AddRange(New DevExpress.XtraReports.UI.ExpressionBinding() {New DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "Concat('วันที่พิมพ์ ' ,Now())")})
        Me.XrLabel46.Font = New DevExpress.Drawing.DXFont("Sarabun", 8.0!)
        Me.XrLabel46.LocationFloat = New DevExpress.Utils.PointFloat(487.0262!, 0!)
        Me.XrLabel46.Multiline = True
        Me.XrLabel46.Name = "XrLabel46"
        Me.XrLabel46.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel46.SizeF = New System.Drawing.SizeF(268.75!, 21.00001!)
        Me.XrLabel46.StylePriority.UseFont = False
        Me.XrLabel46.StylePriority.UseTextAlignment = False
        Me.XrLabel46.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrPageInfo1
        '
        Me.XrPageInfo1.Font = New DevExpress.Drawing.DXFont("Sarabun", 8.0!)
        Me.XrPageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(326.9011!, 0!)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96.0!)
        Me.XrPageInfo1.SizeF = New System.Drawing.SizeF(100.0!, 21.00002!)
        Me.XrPageInfo1.StylePriority.UseFont = False
        Me.XrPageInfo1.StylePriority.UseTextAlignment = False
        Me.XrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrPageInfo1.TextFormatString = "หน้า {0} / {1}"
        '
        'rxBillReport
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.TopMargin, Me.BottomMargin, Me.Detail, Me.PageHeader, Me.GroupFooter1, Me.GroupHeader1, Me.DetailReport, Me.ReportFooter, Me.PageFooter})
        Me.ComponentStorage.AddRange(New System.ComponentModel.IComponent() {Me.SqlDataSource1})
        Me.DataMember = "Bill"
        Me.DataSource = Me.SqlDataSource1
        Me.Font = New DevExpress.Drawing.DXFont("Sarabun", 10.0!)
        Me.Margins = New DevExpress.Drawing.DXMargins(30.0!, 30.0!, 30.0!, 30.0!)
        Me.PageHeight = 1169
        Me.PageWidth = 827
        Me.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.A4
        Me.ParameterPanelLayoutItems.AddRange(New DevExpress.XtraReports.Parameters.ParameterPanelLayoutItem() {New DevExpress.XtraReports.Parameters.ParameterLayoutItem(Me.BillNumber, DevExpress.XtraReports.Parameters.Orientation.Horizontal)})
        Me.Parameters.AddRange(New DevExpress.XtraReports.Parameters.Parameter() {Me.BillNumber})
        Me.Version = "23.2"
        XrWatermark1.Id = "Watermark1"
        Me.Watermarks.AddRange(New DevExpress.XtraPrinting.Drawing.Watermark() {XrWatermark1})
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub

    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel8 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel9 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel6 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel7 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel5 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel4 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents SqlDataSource1 As DevExpress.DataAccess.Sql.SqlDataSource
    Friend WithEvents XrLabel10 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel11 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents BillNumber As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents XrPanel1 As DevExpress.XtraReports.UI.XRPanel
    Friend WithEvents XrLabel12 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel18 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel17 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel16 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel15 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel14 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel13 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents GroupFooter1 As DevExpress.XtraReports.UI.GroupFooterBand
    Friend WithEvents XrLabel21 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel20 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel19 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel22 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents GroupHeader1 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents DetailReport As DevExpress.XtraReports.UI.DetailReportBand
    Friend WithEvents Detail1 As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents GroupHeader2 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents XrLabel23 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents GroupFooter2 As DevExpress.XtraReports.UI.GroupFooterBand
    Friend WithEvents XrLine3 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLine4 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLabel24 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel30 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel29 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel28 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel27 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel26 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel25 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel31 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel32 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel35 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel34 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel33 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel38 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel37 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel36 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel39 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents ReportFooter As DevExpress.XtraReports.UI.ReportFooterBand
    Friend WithEvents XrLabel42 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel41 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel40 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel43 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel44 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel45 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents XrLabel46 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
End Class
