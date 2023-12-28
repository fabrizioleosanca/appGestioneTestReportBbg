<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Dim GridViewTextBoxColumn17 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn18 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn19 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn20 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn21 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn22 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn23 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn24 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Me.Windows7Theme1 = New Telerik.WinControls.Themes.Windows7Theme()
        Me.RadCommandBarMain = New Telerik.WinControls.UI.RadCommandBar()
        Me.CommandBarRowElement1 = New Telerik.WinControls.UI.CommandBarRowElement()
        Me.CommandBarStripElement1 = New Telerik.WinControls.UI.CommandBarStripElement()
        Me.btnAggiungiTestReport = New Telerik.WinControls.UI.CommandBarButton()
        Me.btnModifcaTestReport = New Telerik.WinControls.UI.CommandBarButton()
        Me.btnEliminaTestReport = New Telerik.WinControls.UI.CommandBarButton()
        Me.CommandBarSeparator1 = New Telerik.WinControls.UI.CommandBarSeparator()
        Me.btnStampaDirettaMain = New Telerik.WinControls.UI.CommandBarButton()
        Me.CommandBarSeparator3 = New Telerik.WinControls.UI.CommandBarSeparator()
        Me.btnOpenSalvaNuovoDaEsistente = New Telerik.WinControls.UI.CommandBarButton()
        Me.CommandBarSeparator2 = New Telerik.WinControls.UI.CommandBarSeparator()
        Me.btnRegistraLotto = New Telerik.WinControls.UI.CommandBarButton()
        Me.btnAggiorna = New Telerik.WinControls.UI.CommandBarButton()
        Me.btnAggiungiFornitoriStrumMate = New Telerik.WinControls.UI.CommandBarButton()
        Me.btnOperatori = New Telerik.WinControls.UI.CommandBarButton()
        Me.CommandBarSeparator4 = New Telerik.WinControls.UI.CommandBarSeparator()
        Me.btnEsci = New Telerik.WinControls.UI.CommandBarButton()
        Me.RadStatusStrip1 = New Telerik.WinControls.UI.RadStatusStrip()
        Me.gridTestReport = New Telerik.WinControls.UI.RadGridView()
        Me.DbGestTestReportDataSetBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DbGestTestReportDataSet = New appGestioneTestReportBbg.dbGestTestReportDataSet()
        Me.TblIntestazioneTableAdapter = New appGestioneTestReportBbg.dbGestTestReportDataSetTableAdapters.tblIntestazioneTableAdapter()
        Me.RadPanelTitolo = New Telerik.WinControls.UI.RadPanel()
        Me.lblTitolo = New System.Windows.Forms.Label()
        Me.lblNomeDitta = New System.Windows.Forms.Label()
        Me.lblMod = New System.Windows.Forms.Label()
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider()
        CType(Me.RadCommandBarMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadStatusStrip1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridTestReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridTestReport.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DbGestTestReportDataSetBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DbGestTestReportDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPanelTitolo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanelTitolo.SuspendLayout()
        Me.SuspendLayout()
        '
        'RadCommandBarMain
        '
        Me.RadCommandBarMain.Location = New System.Drawing.Point(0, 71)
        Me.RadCommandBarMain.Name = "RadCommandBarMain"
        Me.RadCommandBarMain.Rows.AddRange(New Telerik.WinControls.UI.CommandBarRowElement() {Me.CommandBarRowElement1})
        Me.RadCommandBarMain.Size = New System.Drawing.Size(1194, 81)
        Me.RadCommandBarMain.TabIndex = 0
        '
        'CommandBarRowElement1
        '
        Me.CommandBarRowElement1.MinSize = New System.Drawing.Size(25, 25)
        Me.CommandBarRowElement1.Strips.AddRange(New Telerik.WinControls.UI.CommandBarStripElement() {Me.CommandBarStripElement1})
        '
        'CommandBarStripElement1
        '
        Me.CommandBarStripElement1.DisplayName = "CommandBarStripElement1"
        Me.CommandBarStripElement1.Items.AddRange(New Telerik.WinControls.UI.RadCommandBarBaseItem() {Me.btnAggiungiTestReport, Me.btnModifcaTestReport, Me.btnEliminaTestReport, Me.CommandBarSeparator1, Me.btnStampaDirettaMain, Me.CommandBarSeparator3, Me.btnOpenSalvaNuovoDaEsistente, Me.CommandBarSeparator2, Me.btnRegistraLotto, Me.btnAggiorna, Me.btnAggiungiFornitoriStrumMate, Me.btnOperatori, Me.CommandBarSeparator4, Me.btnEsci})
        Me.CommandBarStripElement1.Name = "CommandBarStripElement1"
        '
        'btnAggiungiTestReport
        '
        Me.btnAggiungiTestReport.AccessibleDescription = "Aggiungi Test Report"
        Me.btnAggiungiTestReport.AccessibleName = "Aggiungi Test Report"
        Me.btnAggiungiTestReport.DisplayName = "btnAggiungiTestReport"
        Me.btnAggiungiTestReport.DrawText = True
        Me.btnAggiungiTestReport.Image = CType(resources.GetObject("btnAggiungiTestReport.Image"), System.Drawing.Image)
        Me.btnAggiungiTestReport.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAggiungiTestReport.Name = "btnAggiungiTestReport"
        Me.btnAggiungiTestReport.Text = "Aggiungi Test Report"
        Me.btnAggiungiTestReport.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAggiungiTestReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnAggiungiTestReport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnModifcaTestReport
        '
        Me.btnModifcaTestReport.AccessibleDescription = "Modifica Test Report"
        Me.btnModifcaTestReport.AccessibleName = "Modifica Test Report"
        Me.btnModifcaTestReport.DisplayName = "btnModifcaTestReport"
        Me.btnModifcaTestReport.DrawText = True
        Me.btnModifcaTestReport.Image = CType(resources.GetObject("btnModifcaTestReport.Image"), System.Drawing.Image)
        Me.btnModifcaTestReport.Name = "btnModifcaTestReport"
        Me.btnModifcaTestReport.Text = "Modifica Test Report"
        Me.btnModifcaTestReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnModifcaTestReport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnEliminaTestReport
        '
        Me.btnEliminaTestReport.AccessibleDescription = "Elimina Test Report"
        Me.btnEliminaTestReport.AccessibleName = "Elimina Test Report"
        Me.btnEliminaTestReport.DisplayName = "btnEliminaTestReport"
        Me.btnEliminaTestReport.DrawText = True
        Me.btnEliminaTestReport.Image = CType(resources.GetObject("btnEliminaTestReport.Image"), System.Drawing.Image)
        Me.btnEliminaTestReport.Name = "btnEliminaTestReport"
        Me.btnEliminaTestReport.Text = "Elimina Test Report"
        Me.btnEliminaTestReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnEliminaTestReport.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'CommandBarSeparator1
        '
        Me.CommandBarSeparator1.AccessibleDescription = "CommandBarSeparator1"
        Me.CommandBarSeparator1.AccessibleName = "CommandBarSeparator1"
        Me.CommandBarSeparator1.DisplayName = "CommandBarSeparator1"
        Me.CommandBarSeparator1.Name = "CommandBarSeparator1"
        Me.CommandBarSeparator1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        Me.CommandBarSeparator1.VisibleInOverflowMenu = False
        '
        'btnStampaDirettaMain
        '
        Me.btnStampaDirettaMain.AccessibleDescription = "Stampa"
        Me.btnStampaDirettaMain.AccessibleName = "Stampa"
        Me.btnStampaDirettaMain.DisplayName = "CommandBarButton1"
        Me.btnStampaDirettaMain.DrawText = True
        Me.btnStampaDirettaMain.Image = CType(resources.GetObject("btnStampaDirettaMain.Image"), System.Drawing.Image)
        Me.btnStampaDirettaMain.Name = "btnStampaDirettaMain"
        Me.btnStampaDirettaMain.Text = "Stampa"
        Me.btnStampaDirettaMain.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnStampaDirettaMain.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'CommandBarSeparator3
        '
        Me.CommandBarSeparator3.AccessibleDescription = "CommandBarSeparator3"
        Me.CommandBarSeparator3.AccessibleName = "CommandBarSeparator3"
        Me.CommandBarSeparator3.DisplayName = "CommandBarSeparator3"
        Me.CommandBarSeparator3.Name = "CommandBarSeparator3"
        Me.CommandBarSeparator3.Visibility = Telerik.WinControls.ElementVisibility.Visible
        Me.CommandBarSeparator3.VisibleInOverflowMenu = False
        '
        'btnOpenSalvaNuovoDaEsistente
        '
        Me.btnOpenSalvaNuovoDaEsistente.AccessibleDescription = "Salva Da Esistente"
        Me.btnOpenSalvaNuovoDaEsistente.AccessibleName = "Salva Da Esistente"
        Me.btnOpenSalvaNuovoDaEsistente.DisplayName = "CommandBarButton1"
        Me.btnOpenSalvaNuovoDaEsistente.DrawText = True
        Me.btnOpenSalvaNuovoDaEsistente.Image = CType(resources.GetObject("btnOpenSalvaNuovoDaEsistente.Image"), System.Drawing.Image)
        Me.btnOpenSalvaNuovoDaEsistente.Name = "btnOpenSalvaNuovoDaEsistente"
        Me.btnOpenSalvaNuovoDaEsistente.Text = "Salva Da Esistente"
        Me.btnOpenSalvaNuovoDaEsistente.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnOpenSalvaNuovoDaEsistente.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'CommandBarSeparator2
        '
        Me.CommandBarSeparator2.AccessibleDescription = "CommandBarSeparator2"
        Me.CommandBarSeparator2.AccessibleName = "CommandBarSeparator2"
        Me.CommandBarSeparator2.DisplayName = "CommandBarSeparator2"
        Me.CommandBarSeparator2.Name = "CommandBarSeparator2"
        Me.CommandBarSeparator2.Visibility = Telerik.WinControls.ElementVisibility.Visible
        Me.CommandBarSeparator2.VisibleInOverflowMenu = False
        '
        'btnRegistraLotto
        '
        Me.btnRegistraLotto.AccessibleDescription = "Reg. Lotto"
        Me.btnRegistraLotto.AccessibleName = "Reg. Lotto"
        Me.btnRegistraLotto.DisplayName = "CommandBarButton1"
        Me.btnRegistraLotto.DrawText = True
        Me.btnRegistraLotto.Image = CType(resources.GetObject("btnRegistraLotto.Image"), System.Drawing.Image)
        Me.btnRegistraLotto.Name = "btnRegistraLotto"
        Me.btnRegistraLotto.Text = "Reg. Lotto"
        Me.btnRegistraLotto.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnRegistraLotto.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnAggiorna
        '
        Me.btnAggiorna.AccessibleDescription = "Aggiorna Dati"
        Me.btnAggiorna.AccessibleName = "Aggiorna Dati"
        Me.btnAggiorna.DisplayName = "btnAggiorna"
        Me.btnAggiorna.DrawText = True
        Me.btnAggiorna.Image = CType(resources.GetObject("btnAggiorna.Image"), System.Drawing.Image)
        Me.btnAggiorna.Name = "btnAggiorna"
        Me.btnAggiorna.Text = "Aggiorna"
        Me.btnAggiorna.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnAggiorna.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnAggiungiFornitoriStrumMate
        '
        Me.btnAggiungiFornitoriStrumMate.AccessibleDescription = "Gest Varie"
        Me.btnAggiungiFornitoriStrumMate.AccessibleName = "Gest Varie"
        Me.btnAggiungiFornitoriStrumMate.DisplayName = "CommandBarButton1"
        Me.btnAggiungiFornitoriStrumMate.DrawText = True
        Me.btnAggiungiFornitoriStrumMate.Image = CType(resources.GetObject("btnAggiungiFornitoriStrumMate.Image"), System.Drawing.Image)
        Me.btnAggiungiFornitoriStrumMate.Name = "btnAggiungiFornitoriStrumMate"
        Me.btnAggiungiFornitoriStrumMate.Text = "Gest For-Stru-Mat"
        Me.btnAggiungiFornitoriStrumMate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnAggiungiFornitoriStrumMate.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnOperatori
        '
        Me.btnOperatori.AccessibleDescription = "Operatori"
        Me.btnOperatori.AccessibleName = "Operatori"
        Me.btnOperatori.DisplayName = "CommandBarButton1"
        Me.btnOperatori.DrawText = True
        Me.btnOperatori.Image = CType(resources.GetObject("btnOperatori.Image"), System.Drawing.Image)
        Me.btnOperatori.Name = "btnOperatori"
        Me.btnOperatori.Text = "Operatori"
        Me.btnOperatori.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnOperatori.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'CommandBarSeparator4
        '
        Me.CommandBarSeparator4.AccessibleDescription = "CommandBarSeparator4"
        Me.CommandBarSeparator4.AccessibleName = "CommandBarSeparator4"
        Me.CommandBarSeparator4.DisplayName = "CommandBarSeparator4"
        Me.CommandBarSeparator4.Name = "CommandBarSeparator4"
        Me.CommandBarSeparator4.Visibility = Telerik.WinControls.ElementVisibility.Visible
        Me.CommandBarSeparator4.VisibleInOverflowMenu = False
        '
        'btnEsci
        '
        Me.btnEsci.AccessibleDescription = "Esci e Chiudi Applicazione"
        Me.btnEsci.AccessibleName = "Esci e Chiudi Applicazione"
        Me.btnEsci.DisplayName = "btnEsci"
        Me.btnEsci.DrawText = True
        Me.btnEsci.Image = CType(resources.GetObject("btnEsci.Image"), System.Drawing.Image)
        Me.btnEsci.Name = "btnEsci"
        Me.btnEsci.Text = "Esci"
        Me.btnEsci.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnEsci.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadStatusStrip1
        '
        Me.RadStatusStrip1.Location = New System.Drawing.Point(0, 658)
        Me.RadStatusStrip1.Name = "RadStatusStrip1"
        Me.RadStatusStrip1.Size = New System.Drawing.Size(1194, 24)
        Me.RadStatusStrip1.TabIndex = 1
        Me.RadStatusStrip1.Text = "RadStatusStripMain"
        '
        'gridTestReport
        '
        Me.gridTestReport.Location = New System.Drawing.Point(0, 125)
        '
        'gridTestReport
        '
        Me.gridTestReport.MasterTemplate.AllowAddNewRow = False
        Me.gridTestReport.MasterTemplate.AllowDeleteRow = False
        Me.gridTestReport.MasterTemplate.AllowEditRow = False
        Me.gridTestReport.MasterTemplate.AutoGenerateColumns = False
        GridViewTextBoxColumn17.DataType = GetType(Integer)
        GridViewTextBoxColumn17.FieldName = "IDIntestazione"
        GridViewTextBoxColumn17.HeaderText = "ID "
        GridViewTextBoxColumn17.Name = "IDIntestazione"
        GridViewTextBoxColumn17.Width = 60
        GridViewTextBoxColumn18.FieldName = "NumOrdine"
        GridViewTextBoxColumn18.HeaderText = "Numero Ordine"
        GridViewTextBoxColumn18.Name = "NumOrdine"
        GridViewTextBoxColumn18.Width = 170
        GridViewTextBoxColumn19.FieldName = "CodArticolo"
        GridViewTextBoxColumn19.HeaderText = "Codice Articolo"
        GridViewTextBoxColumn19.Name = "CodArticolo"
        GridViewTextBoxColumn19.Width = 180
        GridViewTextBoxColumn20.DataType = GetType(Date)
        GridViewTextBoxColumn20.FieldName = "Data"
        GridViewTextBoxColumn20.HeaderText = "Data Test Report"
        GridViewTextBoxColumn20.Name = "Data"
        GridViewTextBoxColumn20.Width = 120
        GridViewTextBoxColumn21.FieldName = "Materiale"
        GridViewTextBoxColumn21.HeaderText = "Materiale"
        GridViewTextBoxColumn21.Name = "Materiale"
        GridViewTextBoxColumn21.Width = 200
        GridViewTextBoxColumn22.FieldName = "Strumento"
        GridViewTextBoxColumn22.HeaderText = "Strumento"
        GridViewTextBoxColumn22.Name = "Strumento"
        GridViewTextBoxColumn22.Width = 150
        GridViewTextBoxColumn23.FieldName = "MacchinaNum"
        GridViewTextBoxColumn23.HeaderText = "Numero Macchina"
        GridViewTextBoxColumn23.Name = "MacchinaNum"
        GridViewTextBoxColumn23.Width = 150
        GridViewTextBoxColumn24.DataType = GetType(Short)
        GridViewTextBoxColumn24.FieldName = "RigaOrdNum"
        GridViewTextBoxColumn24.HeaderText = "Numero Riga Ord."
        GridViewTextBoxColumn24.Name = "RigaOrdNum"
        GridViewTextBoxColumn24.Width = 130
        Me.gridTestReport.MasterTemplate.Columns.AddRange(New Telerik.WinControls.UI.GridViewDataColumn() {GridViewTextBoxColumn17, GridViewTextBoxColumn18, GridViewTextBoxColumn19, GridViewTextBoxColumn20, GridViewTextBoxColumn21, GridViewTextBoxColumn22, GridViewTextBoxColumn23, GridViewTextBoxColumn24})
        Me.gridTestReport.MasterTemplate.DataSource = Me.DbGestTestReportDataSetBindingSource
        Me.gridTestReport.MasterTemplate.EnableFiltering = True
        Me.gridTestReport.MasterTemplate.VerticalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow
        Me.gridTestReport.Name = "gridTestReport"
        Me.gridTestReport.Size = New System.Drawing.Size(1194, 534)
        Me.gridTestReport.TabIndex = 2
        Me.gridTestReport.Text = "RadGridViewTestReport"
        Me.gridTestReport.UseScrollbarsInHierarchy = True
        '
        'DbGestTestReportDataSetBindingSource
        '
        Me.DbGestTestReportDataSetBindingSource.DataSource = Me.DbGestTestReportDataSet
        Me.DbGestTestReportDataSetBindingSource.Position = 0
        '
        'DbGestTestReportDataSet
        '
        Me.DbGestTestReportDataSet.DataSetName = "dbGestTestReportDataSet"
        Me.DbGestTestReportDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'TblIntestazioneTableAdapter
        '
        Me.TblIntestazioneTableAdapter.ClearBeforeFill = True
        '
        'RadPanelTitolo
        '
        Me.RadPanelTitolo.BackgroundImage = CType(resources.GetObject("RadPanelTitolo.BackgroundImage"), System.Drawing.Image)
        Me.RadPanelTitolo.Controls.Add(Me.lblTitolo)
        Me.RadPanelTitolo.Controls.Add(Me.lblNomeDitta)
        Me.RadPanelTitolo.Controls.Add(Me.lblMod)
        Me.RadPanelTitolo.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanelTitolo.Location = New System.Drawing.Point(0, 0)
        Me.RadPanelTitolo.Name = "RadPanelTitolo"
        Me.RadPanelTitolo.Size = New System.Drawing.Size(1194, 70)
        Me.RadPanelTitolo.TabIndex = 0
        Me.RadPanelTitolo.TabStop = False
        '
        'lblTitolo
        '
        Me.lblTitolo.AutoSize = True
        Me.lblTitolo.BackColor = System.Drawing.Color.Transparent
        Me.lblTitolo.Font = New System.Drawing.Font("Segoe UI", 18.25!, System.Drawing.FontStyle.Bold)
        Me.lblTitolo.ForeColor = System.Drawing.Color.RoyalBlue
        Me.lblTitolo.Location = New System.Drawing.Point(299, 12)
        Me.lblTitolo.Name = "lblTitolo"
        Me.lblTitolo.Size = New System.Drawing.Size(208, 35)
        Me.lblTitolo.TabIndex = 2
        Me.lblTitolo.Text = "Test Report BBG"
        '
        'lblNomeDitta
        '
        Me.lblNomeDitta.AutoSize = True
        Me.lblNomeDitta.BackColor = System.Drawing.Color.Transparent
        Me.lblNomeDitta.Font = New System.Drawing.Font("Segoe UI", 16.25!, System.Drawing.FontStyle.Bold)
        Me.lblNomeDitta.ForeColor = System.Drawing.Color.RoyalBlue
        Me.lblNomeDitta.Location = New System.Drawing.Point(605, 14)
        Me.lblNomeDitta.Name = "lblNomeDitta"
        Me.lblNomeDitta.Size = New System.Drawing.Size(285, 30)
        Me.lblNomeDitta.TabIndex = 1
        Me.lblNomeDitta.Text = "BBG Metalmeccanica S.n.c"
        '
        'lblMod
        '
        Me.lblMod.AutoSize = True
        Me.lblMod.BackColor = System.Drawing.Color.Transparent
        Me.lblMod.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold)
        Me.lblMod.ForeColor = System.Drawing.Color.DarkCyan
        Me.lblMod.Location = New System.Drawing.Point(10, 19)
        Me.lblMod.Name = "lblMod"
        Me.lblMod.Size = New System.Drawing.Size(112, 25)
        Me.lblMod.TabIndex = 0
        Me.lblMod.Text = "MOD 09.02"
        '
        'HelpProvider1
        '
        Me.HelpProvider1.HelpNamespace = "helpeditor.html"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1194, 682)
        Me.Controls.Add(Me.RadPanelTitolo)
        Me.Controls.Add(Me.gridTestReport)
        Me.Controls.Add(Me.RadStatusStrip1)
        Me.Controls.Add(Me.RadCommandBarMain)
        Me.HelpButton = True
        Me.HelpProvider1.SetHelpKeyword(Me, "F1")
        Me.HelpProvider1.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.Topic)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMain"
        Me.HelpProvider1.SetShowHelp(Me, True)
        Me.Text = "Test Report"
        CType(Me.RadCommandBarMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadStatusStrip1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridTestReport.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridTestReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DbGestTestReportDataSetBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DbGestTestReportDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPanelTitolo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanelTitolo.ResumeLayout(False)
        Me.RadPanelTitolo.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Windows7Theme1 As Telerik.WinControls.Themes.Windows7Theme
    Friend WithEvents RadCommandBarMain As Telerik.WinControls.UI.RadCommandBar
    Friend WithEvents CommandBarRowElement1 As Telerik.WinControls.UI.CommandBarRowElement
    Friend WithEvents CommandBarStripElement1 As Telerik.WinControls.UI.CommandBarStripElement
    Friend WithEvents btnAggiungiTestReport As Telerik.WinControls.UI.CommandBarButton
    Friend WithEvents btnModifcaTestReport As Telerik.WinControls.UI.CommandBarButton
    Friend WithEvents btnEliminaTestReport As Telerik.WinControls.UI.CommandBarButton
    Friend WithEvents CommandBarSeparator1 As Telerik.WinControls.UI.CommandBarSeparator
    Friend WithEvents CommandBarSeparator2 As Telerik.WinControls.UI.CommandBarSeparator
    Friend WithEvents btnAggiorna As Telerik.WinControls.UI.CommandBarButton
    Friend WithEvents btnEsci As Telerik.WinControls.UI.CommandBarButton
    Friend WithEvents RadStatusStrip1 As Telerik.WinControls.UI.RadStatusStrip
    Friend WithEvents gridTestReport As Telerik.WinControls.UI.RadGridView
    Friend WithEvents DbGestTestReportDataSet As appGestioneTestReportBbg.dbGestTestReportDataSet
    Friend WithEvents TblIntestazioneTableAdapter As appGestioneTestReportBbg.dbGestTestReportDataSetTableAdapters.tblIntestazioneTableAdapter
    Friend WithEvents DbGestTestReportDataSetBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents RadPanelTitolo As Telerik.WinControls.UI.RadPanel
    Friend WithEvents lblTitolo As System.Windows.Forms.Label
    Friend WithEvents lblNomeDitta As System.Windows.Forms.Label
    Friend WithEvents lblMod As System.Windows.Forms.Label
    Friend WithEvents btnOpenSalvaNuovoDaEsistente As Telerik.WinControls.UI.CommandBarButton
    Friend WithEvents btnRegistraLotto As Telerik.WinControls.UI.CommandBarButton
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
    Friend WithEvents btnAggiungiFornitoriStrumMate As Telerik.WinControls.UI.CommandBarButton
    Friend WithEvents btnStampaDirettaMain As Telerik.WinControls.UI.CommandBarButton
    Friend WithEvents CommandBarSeparator3 As Telerik.WinControls.UI.CommandBarSeparator
    Friend WithEvents CommandBarSeparator4 As Telerik.WinControls.UI.CommandBarSeparator
    Friend WithEvents btnOperatori As Telerik.WinControls.UI.CommandBarButton
End Class
