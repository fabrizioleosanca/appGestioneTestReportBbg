Partial Class TelerikReportBBG
    
    'NOTE: The following procedure is required by the telerik Reporting Designer
    'It can be modified using the telerik Reporting Designer.  
    'Do not modify it using the code editor.
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TelerikReportBBG))
        Dim Group1 As Telerik.Reporting.Group = New Telerik.Reporting.Group()
        Dim StyleRule1 As Telerik.Reporting.Drawing.StyleRule = New Telerik.Reporting.Drawing.StyleRule()
        Dim StyleRule2 As Telerik.Reporting.Drawing.StyleRule = New Telerik.Reporting.Drawing.StyleRule()
        Dim StyleRule3 As Telerik.Reporting.Drawing.StyleRule = New Telerik.Reporting.Drawing.StyleRule()
        Dim StyleRule4 As Telerik.Reporting.Drawing.StyleRule = New Telerik.Reporting.Drawing.StyleRule()
        Me.labelsGroupFooterSection = New Telerik.Reporting.GroupFooterSection()
        Me.labelsGroupHeaderSection = New Telerik.Reporting.GroupHeaderSection()
        Me.SqlDataSourceTestReportTelerik = New Telerik.Reporting.SqlDataSource()
        Me.pageHeader = New Telerik.Reporting.PageHeaderSection()
        Me.reportNameTextBox = New Telerik.Reporting.TextBox()
        Me.txtBbgMetalmeccTitolo = New Telerik.Reporting.TextBox()
        Me.txtModTitolo = New Telerik.Reporting.TextBox()
        Me.pageFooter = New Telerik.Reporting.PageFooterSection()
        Me.currentTimeTextBox = New Telerik.Reporting.TextBox()
        Me.pageInfoTextBox = New Telerik.Reporting.TextBox()
        Me.reportHeader = New Telerik.Reporting.ReportHeaderSection()
        Me.lblData = New Telerik.Reporting.TextBox()
        Me.reportFooter = New Telerik.Reporting.ReportFooterSection()
        Me.detail = New Telerik.Reporting.DetailSection()
        Me.Shape1 = New Telerik.Reporting.Shape()
        Me.lblCodArticolo = New Telerik.Reporting.TextBox()
        Me.lblMateriale = New Telerik.Reporting.TextBox()
        Me.lblStrumento = New Telerik.Reporting.TextBox()
        Me.lblPezziControllati = New Telerik.Reporting.TextBox()
        Me.TextBox1 = New Telerik.Reporting.TextBox()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'labelsGroupFooterSection
        '
        Me.labelsGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.71437495946884155R)
        Me.labelsGroupFooterSection.Name = "labelsGroupFooterSection"
        Me.labelsGroupFooterSection.Style.Visible = False
        '
        'labelsGroupHeaderSection
        '
        Me.labelsGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.71437495946884155R)
        Me.labelsGroupHeaderSection.Name = "labelsGroupHeaderSection"
        Me.labelsGroupHeaderSection.PrintOnEveryPage = True
        '
        'SqlDataSourceTestReportTelerik
        '
        Me.SqlDataSourceTestReportTelerik.ConnectionString = "dbGestTestReportConnectionString"
        Me.SqlDataSourceTestReportTelerik.Name = "SqlDataSourceTestReportTelerik"
        Me.SqlDataSourceTestReportTelerik.SelectCommand = resources.GetString("SqlDataSourceTestReportTelerik.SelectCommand")
        '
        'pageHeader
        '
        Me.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(1.1058331727981567R)
        Me.pageHeader.Items.AddRange(New Telerik.Reporting.ReportItemBase() {Me.reportNameTextBox, Me.txtBbgMetalmeccTitolo, Me.txtModTitolo})
        Me.pageHeader.Name = "pageHeader"
        Me.pageHeader.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid
        Me.pageHeader.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid
        Me.pageHeader.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid
        Me.pageHeader.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid
        Me.pageHeader.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1.0R)
        '
        'reportNameTextBox
        '
        Me.reportNameTextBox.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.299999862909317R), Telerik.Reporting.Drawing.Unit.Cm(0.0001000221527647227R))
        Me.reportNameTextBox.Name = "reportNameTextBox"
        Me.reportNameTextBox.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.59999942779541R), Telerik.Reporting.Drawing.Unit.Cm(1.1055333614349365R))
        Me.reportNameTextBox.Style.Font.Bold = True
        Me.reportNameTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(14.0R)
        Me.reportNameTextBox.StyleName = "PageInfo"
        Me.reportNameTextBox.Value = "TEST REPORT"
        '
        'txtBbgMetalmeccTitolo
        '
        Me.txtBbgMetalmeccTitolo.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.40000057220459R), Telerik.Reporting.Drawing.Unit.Cm(0.0R))
        Me.txtBbgMetalmeccTitolo.Name = "txtBbgMetalmeccTitolo"
        Me.txtBbgMetalmeccTitolo.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.6999979019165039R), Telerik.Reporting.Drawing.Unit.Cm(1.1055333614349365R))
        Me.txtBbgMetalmeccTitolo.Style.Font.Bold = True
        Me.txtBbgMetalmeccTitolo.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(14.0R)
        Me.txtBbgMetalmeccTitolo.StyleName = "PageInfo"
        Me.txtBbgMetalmeccTitolo.Value = "BBG METALMECCANICA s.n.c"
        '
        'txtModTitolo
        '
        Me.txtModTitolo.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(25.700000762939453R), Telerik.Reporting.Drawing.Unit.Cm(0.0R))
        Me.txtModTitolo.Name = "txtModTitolo"
        Me.txtModTitolo.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.40000057220459R), Telerik.Reporting.Drawing.Unit.Cm(1.1055333614349365R))
        Me.txtModTitolo.Style.Font.Bold = True
        Me.txtModTitolo.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(14.0R)
        Me.txtModTitolo.StyleName = "PageInfo"
        Me.txtModTitolo.Value = "MOD.09.02"
        '
        'pageFooter
        '
        Me.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(1.1058331727981567R)
        Me.pageFooter.Items.AddRange(New Telerik.Reporting.ReportItemBase() {Me.currentTimeTextBox, Me.pageInfoTextBox})
        Me.pageFooter.Name = "pageFooter"
        '
        'currentTimeTextBox
        '
        Me.currentTimeTextBox.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637R), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637R))
        Me.currentTimeTextBox.Name = "currentTimeTextBox"
        Me.currentTimeTextBox.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(14.717708587646484R), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045R))
        Me.currentTimeTextBox.StyleName = "PageInfo"
        Me.currentTimeTextBox.Value = "=NOW()"
        '
        'pageInfoTextBox
        '
        Me.pageInfoTextBox.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(14.823541641235352R), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637R))
        Me.pageInfoTextBox.Name = "pageInfoTextBox"
        Me.pageInfoTextBox.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(14.717708587646484R), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045R))
        Me.pageInfoTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right
        Me.pageInfoTextBox.StyleName = "PageInfo"
        Me.pageInfoTextBox.Value = "=PageNumber"
        '
        'reportHeader
        '
        Me.reportHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(6.394167423248291R)
        Me.reportHeader.Items.AddRange(New Telerik.Reporting.ReportItemBase() {Me.lblData, Me.Shape1, Me.lblCodArticolo, Me.lblMateriale, Me.lblStrumento, Me.lblPezziControllati, Me.TextBox1})
        Me.reportHeader.Name = "reportHeader"
        Me.reportHeader.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid
        Me.reportHeader.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1.0R)
        '
        'lblData
        '
        Me.lblData.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916679531335831R), Telerik.Reporting.Drawing.Unit.Cm(0.094166770577430725R))
        Me.lblData.Name = "lblData"
        Me.lblData.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.6468830108642578R), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269R))
        Me.lblData.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid
        Me.lblData.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None
        Me.lblData.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None
        Me.lblData.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None
        Me.lblData.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1.0R)
        Me.lblData.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right
        Me.lblData.Value = "Data:"
        '
        'reportFooter
        '
        Me.reportFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(0.71437495946884155R)
        Me.reportFooter.Name = "reportFooter"
        '
        'detail
        '
        Me.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.71437495946884155R)
        Me.detail.Name = "detail"
        '
        'Shape1
        '
        Me.Shape1.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(4.7000002861022949R), Telerik.Reporting.Drawing.Unit.Cm(0.000099921220680698752R))
        Me.Shape1.Name = "Shape1"
        Me.Shape1.ShapeType = New Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.NS)
        Me.Shape1.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612R), Telerik.Reporting.Drawing.Unit.Cm(6.3939671516418457R))
        '
        'lblCodArticolo
        '
        Me.lblCodArticolo.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637R), Telerik.Reporting.Drawing.Unit.Cm(0.69416666030883789R))
        Me.lblCodArticolo.Name = "lblCodArticolo"
        Me.lblCodArticolo.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.6468830108642578R), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269R))
        Me.lblCodArticolo.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid
        Me.lblCodArticolo.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None
        Me.lblCodArticolo.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None
        Me.lblCodArticolo.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None
        Me.lblCodArticolo.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1.0R)
        Me.lblCodArticolo.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right
        Me.lblCodArticolo.Value = "Codice Articolo:"
        '
        'lblMateriale
        '
        Me.lblMateriale.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637R), Telerik.Reporting.Drawing.Unit.Cm(1.2941668033599854R))
        Me.lblMateriale.Name = "lblMateriale"
        Me.lblMateriale.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.6468830108642578R), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269R))
        Me.lblMateriale.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid
        Me.lblMateriale.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1.0R)
        Me.lblMateriale.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right
        Me.lblMateriale.Value = "Materiale:"
        '
        'lblStrumento
        '
        Me.lblStrumento.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637R), Telerik.Reporting.Drawing.Unit.Cm(1.8941668272018433R))
        Me.lblStrumento.Name = "lblStrumento"
        Me.lblStrumento.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.6468830108642578R), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269R))
        Me.lblStrumento.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid
        Me.lblStrumento.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None
        Me.lblStrumento.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None
        Me.lblStrumento.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None
        Me.lblStrumento.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1.0R)
        Me.lblStrumento.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right
        Me.lblStrumento.Value = "Strumento:"
        '
        'lblPezziControllati
        '
        Me.lblPezziControllati.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637R), Telerik.Reporting.Drawing.Unit.Cm(2.4941666126251221R))
        Me.lblPezziControllati.Name = "lblPezziControllati"
        Me.lblPezziControllati.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.6468830108642578R), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269R))
        Me.lblPezziControllati.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid
        Me.lblPezziControllati.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None
        Me.lblPezziControllati.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None
        Me.lblPezziControllati.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None
        Me.lblPezziControllati.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1.0R)
        Me.lblPezziControllati.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right
        Me.lblPezziControllati.Value = "Pezzi Controllati:"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.0R), Telerik.Reporting.Drawing.Unit.Cm(3.0943670272827148R))
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.6468830108642578R), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269R))
        Me.TextBox1.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid
        Me.TextBox1.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None
        Me.TextBox1.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None
        Me.TextBox1.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None
        Me.TextBox1.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1.0R)
        Me.TextBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right
        Me.TextBox1.Value = "Numero Lotto:"
        '
        'TelerikReportBBG
        '
        Me.DataSource = Me.SqlDataSourceTestReportTelerik
        Group1.GroupFooter = Me.labelsGroupFooterSection
        Group1.GroupHeader = Me.labelsGroupHeaderSection
        Group1.Name = "labelsGroup"
        Me.Groups.AddRange(New Telerik.Reporting.Group() {Group1})
        Me.Items.AddRange(New Telerik.Reporting.ReportItemBase() {Me.labelsGroupHeaderSection, Me.labelsGroupFooterSection, Me.pageHeader, Me.pageFooter, Me.reportHeader, Me.reportFooter, Me.detail})
        Me.Name = "TelerikReportBBG"
        Me.PageSettings.Landscape = True
        Me.PageSettings.Margins = New Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(0.0R), Telerik.Reporting.Drawing.Unit.Mm(0.0R), Telerik.Reporting.Drawing.Unit.Mm(0.0R), Telerik.Reporting.Drawing.Unit.Mm(0.0R))
        Me.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4
        Me.Style.BackgroundColor = System.Drawing.Color.White
        StyleRule1.Selectors.AddRange(New Telerik.Reporting.Drawing.ISelector() {New Telerik.Reporting.Drawing.StyleSelector("Title")})
        StyleRule1.Style.Color = System.Drawing.Color.Black
        StyleRule1.Style.Font.Bold = True
        StyleRule1.Style.Font.Italic = False
        StyleRule1.Style.Font.Name = "Tahoma"
        StyleRule1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(18.0R)
        StyleRule1.Style.Font.Strikeout = False
        StyleRule1.Style.Font.Underline = False
        StyleRule2.Selectors.AddRange(New Telerik.Reporting.Drawing.ISelector() {New Telerik.Reporting.Drawing.StyleSelector("Caption")})
        StyleRule2.Style.Color = System.Drawing.Color.Black
        StyleRule2.Style.Font.Name = "Tahoma"
        StyleRule2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10.0R)
        StyleRule2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        StyleRule3.Selectors.AddRange(New Telerik.Reporting.Drawing.ISelector() {New Telerik.Reporting.Drawing.StyleSelector("Data")})
        StyleRule3.Style.Font.Name = "Tahoma"
        StyleRule3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9.0R)
        StyleRule3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        StyleRule4.Selectors.AddRange(New Telerik.Reporting.Drawing.ISelector() {New Telerik.Reporting.Drawing.StyleSelector("PageInfo")})
        StyleRule4.Style.Font.Name = "Tahoma"
        StyleRule4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8.0R)
        StyleRule4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.StyleSheet.AddRange(New Telerik.Reporting.Drawing.StyleRule() {StyleRule1, StyleRule2, StyleRule3, StyleRule4})
        Me.Width = Telerik.Reporting.Drawing.Unit.Cm(29.541252136230469R)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents SqlDataSourceTestReportTelerik As Telerik.Reporting.SqlDataSource
    Friend WithEvents labelsGroupHeaderSection As Telerik.Reporting.GroupHeaderSection
    Friend WithEvents labelsGroupFooterSection As Telerik.Reporting.GroupFooterSection
    Friend WithEvents pageHeader As Telerik.Reporting.PageHeaderSection
    Friend WithEvents reportNameTextBox As Telerik.Reporting.TextBox
    Friend WithEvents pageFooter As Telerik.Reporting.PageFooterSection
    Friend WithEvents currentTimeTextBox As Telerik.Reporting.TextBox
    Friend WithEvents pageInfoTextBox As Telerik.Reporting.TextBox
    Friend WithEvents reportHeader As Telerik.Reporting.ReportHeaderSection
    Friend WithEvents reportFooter As Telerik.Reporting.ReportFooterSection
    Friend WithEvents detail As Telerik.Reporting.DetailSection
    Friend WithEvents txtBbgMetalmeccTitolo As Telerik.Reporting.TextBox
    Friend WithEvents txtModTitolo As Telerik.Reporting.TextBox
    Friend WithEvents lblData As Telerik.Reporting.TextBox
    Friend WithEvents Shape1 As Telerik.Reporting.Shape
    Friend WithEvents lblCodArticolo As Telerik.Reporting.TextBox
    Friend WithEvents lblMateriale As Telerik.Reporting.TextBox
    Friend WithEvents lblStrumento As Telerik.Reporting.TextBox
    Friend WithEvents lblPezziControllati As Telerik.Reporting.TextBox
    Friend WithEvents TextBox1 As Telerik.Reporting.TextBox
End Class