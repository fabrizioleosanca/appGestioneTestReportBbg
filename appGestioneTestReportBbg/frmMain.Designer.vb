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
        Dim GridViewTextBoxColumn1 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn2 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn3 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn4 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn5 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn6 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn7 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn8 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
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
        Me.CommandBarSeparator6 = New Telerik.WinControls.UI.CommandBarSeparator()
        Me.CommandBarSeparator5 = New Telerik.WinControls.UI.CommandBarSeparator()
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
        Me.RadCommandBarMain.Size = New System.Drawing.Size(1194, 56)
        Me.RadCommandBarMain.TabIndex = 0
        '
        'CommandBarRowElement1
        '
        Me.CommandBarRowElement1.MinSize = New System.Drawing.Size(25, 25)
        Me.CommandBarRowElement1.Name = "CommandBarRowElement1"
        Me.CommandBarRowElement1.Strips.AddRange(New Telerik.WinControls.UI.CommandBarStripElement() {Me.CommandBarStripElement1})
        '
        'CommandBarStripElement1
        '
        Me.CommandBarStripElement1.DisplayName = "CommandBarStripElement1"
        Me.CommandBarStripElement1.Items.AddRange(New Telerik.WinControls.UI.RadCommandBarBaseItem() {Me.btnAggiungiTestReport, Me.btnModifcaTestReport, Me.btnEliminaTestReport, Me.CommandBarSeparator1, Me.btnStampaDirettaMain, Me.CommandBarSeparator3, Me.btnOpenSalvaNuovoDaEsistente, Me.CommandBarSeparator2, Me.btnRegistraLotto, Me.btnAggiorna, Me.btnAggiungiFornitoriStrumMate, Me.CommandBarSeparator6, Me.btnOperatori, Me.CommandBarSeparator4, Me.btnEsci, Me.CommandBarSeparator5})
        Me.CommandBarStripElement1.Name = "CommandBarStripElement1"
        '
        'btnAggiungiTestReport
        '
        Me.btnAggiungiTestReport.DisplayName = "btnAggiungiTestReport"
        Me.btnAggiungiTestReport.DrawText = True
        Me.btnAggiungiTestReport.Image = CType(resources.GetObject("btnAggiungiTestReport.Image"), System.Drawing.Image)
        Me.btnAggiungiTestReport.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAggiungiTestReport.Name = "btnAggiungiTestReport"
        Me.btnAggiungiTestReport.Text = "Aggiungi Test Report"
        Me.btnAggiungiTestReport.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAggiungiTestReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'btnModifcaTestReport
        '
        Me.btnModifcaTestReport.DisplayName = "btnModifcaTestReport"
        Me.btnModifcaTestReport.DrawText = True
        Me.btnModifcaTestReport.Image = CType(resources.GetObject("btnModifcaTestReport.Image"), System.Drawing.Image)
        Me.btnModifcaTestReport.Name = "btnModifcaTestReport"
        Me.btnModifcaTestReport.Text = "Modifica Test Report"
        Me.btnModifcaTestReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'btnEliminaTestReport
        '
        Me.btnEliminaTestReport.DisplayName = "btnEliminaTestReport"
        Me.btnEliminaTestReport.DrawText = True
        Me.btnEliminaTestReport.Image = CType(resources.GetObject("btnEliminaTestReport.Image"), System.Drawing.Image)
        Me.btnEliminaTestReport.Name = "btnEliminaTestReport"
        Me.btnEliminaTestReport.Text = "Elimina Test Report"
        Me.btnEliminaTestReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'CommandBarSeparator1
        '
        Me.CommandBarSeparator1.AccessibleDescription = "CommandBarSeparator1"
        Me.CommandBarSeparator1.AccessibleName = "CommandBarSeparator1"
        Me.CommandBarSeparator1.DisplayName = "CommandBarSeparator1"
        Me.CommandBarSeparator1.Name = "CommandBarSeparator1"
        Me.CommandBarSeparator1.VisibleInOverflowMenu = False
        '
        'btnStampaDirettaMain
        '
        Me.btnStampaDirettaMain.DisplayName = "CommandBarButton1"
        Me.btnStampaDirettaMain.DrawText = True
        Me.btnStampaDirettaMain.Image = CType(resources.GetObject("btnStampaDirettaMain.Image"), System.Drawing.Image)
        Me.btnStampaDirettaMain.Name = "btnStampaDirettaMain"
        Me.btnStampaDirettaMain.Text = "Stampa"
        Me.btnStampaDirettaMain.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'CommandBarSeparator3
        '
        Me.CommandBarSeparator3.AccessibleDescription = "CommandBarSeparator3"
        Me.CommandBarSeparator3.AccessibleName = "CommandBarSeparator3"
        Me.CommandBarSeparator3.DisplayName = "CommandBarSeparator3"
        Me.CommandBarSeparator3.Name = "CommandBarSeparator3"
        Me.CommandBarSeparator3.VisibleInOverflowMenu = False
        '
        'btnOpenSalvaNuovoDaEsistente
        '
        Me.btnOpenSalvaNuovoDaEsistente.DisplayName = "CommandBarButton1"
        Me.btnOpenSalvaNuovoDaEsistente.DrawText = True
        Me.btnOpenSalvaNuovoDaEsistente.Image = CType(resources.GetObject("btnOpenSalvaNuovoDaEsistente.Image"), System.Drawing.Image)
        Me.btnOpenSalvaNuovoDaEsistente.Name = "btnOpenSalvaNuovoDaEsistente"
        Me.btnOpenSalvaNuovoDaEsistente.Text = "Salva Da Esistente"
        Me.btnOpenSalvaNuovoDaEsistente.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'CommandBarSeparator2
        '
        Me.CommandBarSeparator2.AccessibleDescription = "CommandBarSeparator2"
        Me.CommandBarSeparator2.AccessibleName = "CommandBarSeparator2"
        Me.CommandBarSeparator2.DisplayName = "CommandBarSeparator2"
        Me.CommandBarSeparator2.Name = "CommandBarSeparator2"
        Me.CommandBarSeparator2.VisibleInOverflowMenu = False
        '
        'btnRegistraLotto
        '
        Me.btnRegistraLotto.DisplayName = "CommandBarButton1"
        Me.btnRegistraLotto.DrawText = True
        Me.btnRegistraLotto.Image = CType(resources.GetObject("btnRegistraLotto.Image"), System.Drawing.Image)
        Me.btnRegistraLotto.Name = "btnRegistraLotto"
        Me.btnRegistraLotto.Text = "Reg. Lotto"
        Me.btnRegistraLotto.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
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
        '
        'btnOperatori
        '
        Me.btnOperatori.DisplayName = "CommandBarButton1"
        Me.btnOperatori.DrawText = True
        Me.btnOperatori.Image = CType(resources.GetObject("btnOperatori.Image"), System.Drawing.Image)
        Me.btnOperatori.Name = "btnOperatori"
        Me.btnOperatori.Text = "Operatori"
        Me.btnOperatori.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'CommandBarSeparator4
        '
        Me.CommandBarSeparator4.AccessibleDescription = "CommandBarSeparator4"
        Me.CommandBarSeparator4.AccessibleName = "CommandBarSeparator4"
        Me.CommandBarSeparator4.DisplayName = "CommandBarSeparator4"
        Me.CommandBarSeparator4.Name = "CommandBarSeparator4"
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
        '
        'RadStatusStrip1
        '
        Me.RadStatusStrip1.Location = New System.Drawing.Point(0, 656)
        Me.RadStatusStrip1.Name = "RadStatusStrip1"
        Me.RadStatusStrip1.Size = New System.Drawing.Size(1194, 26)
        Me.RadStatusStrip1.TabIndex = 1
        '
        'gridTestReport
        '
        Me.gridTestReport.Location = New System.Drawing.Point(0, 125)
        '
        '
        '
        Me.gridTestReport.MasterTemplate.AllowAddNewRow = False
        Me.gridTestReport.MasterTemplate.AllowDeleteRow = False
        Me.gridTestReport.MasterTemplate.AllowEditRow = False
        Me.gridTestReport.MasterTemplate.AutoGenerateColumns = False
        GridViewTextBoxColumn1.DataType = GetType(Integer)
        GridViewTextBoxColumn1.FieldName = "IDIntestazione"
        GridViewTextBoxColumn1.HeaderText = "ID "
        GridViewTextBoxColumn1.Name = "IDIntestazione"
        GridViewTextBoxColumn1.Width = 60
        GridViewTextBoxColumn2.FieldName = "NumOrdine"
        GridViewTextBoxColumn2.HeaderText = "Numero Ordine"
        GridViewTextBoxColumn2.Name = "NumOrdine"
        GridViewTextBoxColumn2.Width = 170
        GridViewTextBoxColumn3.FieldName = "CodArticolo"
        GridViewTextBoxColumn3.HeaderText = "Codice Articolo"
        GridViewTextBoxColumn3.Name = "CodArticolo"
        GridViewTextBoxColumn3.Width = 180
        GridViewTextBoxColumn4.DataType = GetType(Date)
        GridViewTextBoxColumn4.FieldName = "Data"
        GridViewTextBoxColumn4.HeaderText = "Data Test Report"
        GridViewTextBoxColumn4.Name = "Data"
        GridViewTextBoxColumn4.Width = 120
        GridViewTextBoxColumn5.FieldName = "Materiale"
        GridViewTextBoxColumn5.HeaderText = "Materiale"
        GridViewTextBoxColumn5.Name = "Materiale"
        GridViewTextBoxColumn5.Width = 200
        GridViewTextBoxColumn6.FieldName = "Strumento"
        GridViewTextBoxColumn6.HeaderText = "Strumento"
        GridViewTextBoxColumn6.Name = "Strumento"
        GridViewTextBoxColumn6.Width = 150
        GridViewTextBoxColumn7.FieldName = "MacchinaNum"
        GridViewTextBoxColumn7.HeaderText = "Numero Macchina"
        GridViewTextBoxColumn7.Name = "MacchinaNum"
        GridViewTextBoxColumn7.Width = 150
        GridViewTextBoxColumn8.DataType = GetType(Short)
        GridViewTextBoxColumn8.FieldName = "RigaOrdNum"
        GridViewTextBoxColumn8.HeaderText = "Numero Riga Ord."
        GridViewTextBoxColumn8.Name = "RigaOrdNum"
        GridViewTextBoxColumn8.Width = 130
        Me.gridTestReport.MasterTemplate.Columns.AddRange(New Telerik.WinControls.UI.GridViewDataColumn() {GridViewTextBoxColumn1, GridViewTextBoxColumn2, GridViewTextBoxColumn3, GridViewTextBoxColumn4, GridViewTextBoxColumn5, GridViewTextBoxColumn6, GridViewTextBoxColumn7, GridViewTextBoxColumn8})
        Me.gridTestReport.MasterTemplate.DataSource = Me.DbGestTestReportDataSetBindingSource
        Me.gridTestReport.MasterTemplate.EnableFiltering = True
        Me.gridTestReport.MasterTemplate.VerticalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow
        Me.gridTestReport.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gridTestReport.Name = "gridTestReport"
        Me.gridTestReport.Size = New System.Drawing.Size(1194, 534)
        Me.gridTestReport.TabIndex = 2
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
        'CommandBarSeparator6
        '
        Me.CommandBarSeparator6.DisplayName = "CommandBarSeparator6"
        Me.CommandBarSeparator6.Name = "CommandBarSeparator6"
        Me.CommandBarSeparator6.Text = ""
        Me.CommandBarSeparator6.VisibleInOverflowMenu = False
        '
        'CommandBarSeparator5
        '
        Me.CommandBarSeparator5.DisplayName = "CommandBarSeparator5"
        Me.CommandBarSeparator5.Name = "CommandBarSeparator5"
        Me.CommandBarSeparator5.Text = ""
        Me.CommandBarSeparator5.VisibleInOverflowMenu = False
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
    Friend WithEvents CommandBarSeparator6 As Telerik.WinControls.UI.CommandBarSeparator
    Friend WithEvents CommandBarSeparator5 As Telerik.WinControls.UI.CommandBarSeparator
End Class
