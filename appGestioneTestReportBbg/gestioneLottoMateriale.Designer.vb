<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class gestioneLottoMateriale
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(gestioneLottoMateriale))
        Dim GridViewTextBoxColumn1 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn2 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn3 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn4 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn5 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim ThemeSource1 As Telerik.WinControls.ThemeSource = New Telerik.WinControls.ThemeSource()
        Dim ThemeSource2 As Telerik.WinControls.ThemeSource = New Telerik.WinControls.ThemeSource()
        Dim ThemeSource3 As Telerik.WinControls.ThemeSource = New Telerik.WinControls.ThemeSource()
        Dim ThemeSource4 As Telerik.WinControls.ThemeSource = New Telerik.WinControls.ThemeSource()
        Dim ThemeSource5 As Telerik.WinControls.ThemeSource = New Telerik.WinControls.ThemeSource()
        Dim ThemeSource6 As Telerik.WinControls.ThemeSource = New Telerik.WinControls.ThemeSource()
        Me.RadCommandBar1 = New Telerik.WinControls.UI.RadCommandBar()
        Me.CommandBarRowElement1 = New Telerik.WinControls.UI.CommandBarRowElement()
        Me.CommandBarStripElement1 = New Telerik.WinControls.UI.CommandBarStripElement()
        Me.CommandBarButtonAggiungiLotto = New Telerik.WinControls.UI.CommandBarButton()
        Me.CommandBarButtonCancellaLotto = New Telerik.WinControls.UI.CommandBarButton()
        Me.CommandBarButtonEsci = New Telerik.WinControls.UI.CommandBarButton()
        Me.CommandBarSeparator1 = New Telerik.WinControls.UI.CommandBarSeparator()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.RadGridViewLottoMateriale = New Telerik.WinControls.UI.RadGridView()
        Me.BindingSourceNumLottoMateriale = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsNumeroLottoMateriale1 = New appGestioneTestReportBbg.dsNumeroLottoMateriale()
        Me.RadPanelTitoloDettagli = New Telerik.WinControls.UI.RadPanel()
        Me.radLabelTitoloDettagli = New Telerik.WinControls.UI.RadLabel()
        Me.RadThemeManager1 = New Telerik.WinControls.RadThemeManager()
        Me.roundRectShape1 = New Telerik.WinControls.RoundRectShape(Me.components)
        Me.TblNumeroLottoMaterialeTableAdapter = New appGestioneTestReportBbg.dsNumeroLottoMaterialeTableAdapters.tblNumeroLottoMaterialeTableAdapter()
        Me.RadPanel4 = New Telerik.WinControls.UI.RadPanel()
        Me.txtFornitore = New Telerik.WinControls.UI.RadTextBox()
        Me.txtNumeroDDT = New Telerik.WinControls.UI.RadTextBox()
        Me.txtNumeroLotto = New Telerik.WinControls.UI.RadTextBox()
        Me.txtMateriale = New Telerik.WinControls.UI.RadTextBox()
        Me.updateButton = New Telerik.WinControls.UI.RadButton()
        Me.lblNumDDT = New Telerik.WinControls.UI.RadLabel()
        Me.lblFornitore = New Telerik.WinControls.UI.RadLabel()
        Me.lblNumeroLotto = New Telerik.WinControls.UI.RadLabel()
        Me.lblMateriale = New Telerik.WinControls.UI.RadLabel()
        Me.RadPanel2 = New Telerik.WinControls.UI.RadPanel()
        CType(Me.RadCommandBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.RadGridViewLottoMateriale, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGridViewLottoMateriale.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSourceNumLottoMateriale, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsNumeroLottoMateriale1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPanelTitoloDettagli, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanelTitoloDettagli.SuspendLayout()
        CType(Me.radLabelTitoloDettagli, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPanel4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel4.SuspendLayout()
        CType(Me.txtFornitore, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNumeroDDT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNumeroLotto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMateriale, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.updateButton, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNumDDT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFornitore, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNumeroLotto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMateriale, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'RadCommandBar1
        '
        Me.RadCommandBar1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadCommandBar1.Location = New System.Drawing.Point(0, 0)
        Me.RadCommandBar1.Name = "RadCommandBar1"
        Me.RadCommandBar1.Rows.AddRange(New Telerik.WinControls.UI.CommandBarRowElement() {Me.CommandBarRowElement1})
        Me.RadCommandBar1.Size = New System.Drawing.Size(1089, 40)
        Me.RadCommandBar1.TabIndex = 0
        Me.RadCommandBar1.Text = "RadCommandBar1"
        '
        'CommandBarRowElement1
        '
        Me.CommandBarRowElement1.MinSize = New System.Drawing.Size(25, 25)
        Me.CommandBarRowElement1.Strips.AddRange(New Telerik.WinControls.UI.CommandBarStripElement() {Me.CommandBarStripElement1})
        '
        'CommandBarStripElement1
        '
        Me.CommandBarStripElement1.DisplayName = "CommandBarStripElement1"
        Me.CommandBarStripElement1.Items.AddRange(New Telerik.WinControls.UI.RadCommandBarBaseItem() {Me.CommandBarButtonAggiungiLotto, Me.CommandBarButtonCancellaLotto, Me.CommandBarButtonEsci, Me.CommandBarSeparator1})
        Me.CommandBarStripElement1.Name = "CommandBarStripElement1"
        '
        'CommandBarButtonAggiungiLotto
        '
        Me.CommandBarButtonAggiungiLotto.AccessibleDescription = "Aggiungi Lotto"
        Me.CommandBarButtonAggiungiLotto.AccessibleName = "Aggiungi Lotto"
        Me.CommandBarButtonAggiungiLotto.DisplayName = "CommandBarButton1"
        Me.CommandBarButtonAggiungiLotto.DrawText = True
        Me.CommandBarButtonAggiungiLotto.Image = CType(resources.GetObject("CommandBarButtonAggiungiLotto.Image"), System.Drawing.Image)
        Me.CommandBarButtonAggiungiLotto.Name = "CommandBarButtonAggiungiLotto"
        Me.CommandBarButtonAggiungiLotto.Text = "Aggiungi Lotto"
        Me.CommandBarButtonAggiungiLotto.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.CommandBarButtonAggiungiLotto.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'CommandBarButtonCancellaLotto
        '
        Me.CommandBarButtonCancellaLotto.AccessibleDescription = "Cancella Lotto"
        Me.CommandBarButtonCancellaLotto.AccessibleName = "Cancella Lotto"
        Me.CommandBarButtonCancellaLotto.DisplayName = "CommandBarButton1"
        Me.CommandBarButtonCancellaLotto.DrawText = True
        Me.CommandBarButtonCancellaLotto.Image = CType(resources.GetObject("CommandBarButtonCancellaLotto.Image"), System.Drawing.Image)
        Me.CommandBarButtonCancellaLotto.Name = "CommandBarButtonCancellaLotto"
        Me.CommandBarButtonCancellaLotto.Text = "Cancella Lotto"
        Me.CommandBarButtonCancellaLotto.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.CommandBarButtonCancellaLotto.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'CommandBarButtonEsci
        '
        Me.CommandBarButtonEsci.AccessibleDescription = "Esci"
        Me.CommandBarButtonEsci.AccessibleName = "Esci"
        Me.CommandBarButtonEsci.DisplayName = "CommandBarButton1"
        Me.CommandBarButtonEsci.DrawText = True
        Me.CommandBarButtonEsci.Image = CType(resources.GetObject("CommandBarButtonEsci.Image"), System.Drawing.Image)
        Me.CommandBarButtonEsci.Name = "CommandBarButtonEsci"
        Me.CommandBarButtonEsci.Text = "Esci"
        Me.CommandBarButtonEsci.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.CommandBarButtonEsci.Visibility = Telerik.WinControls.ElementVisibility.Visible
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
        'RadPanel1
        '
        Me.RadPanel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadPanel1.BackColor = System.Drawing.Color.Transparent
        Me.RadPanel1.Controls.Add(Me.RadGridViewLottoMateriale)
        Me.RadPanel1.Location = New System.Drawing.Point(5, 46)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(789, 480)
        Me.RadPanel1.TabIndex = 1
        '
        'RadGridViewLottoMateriale
        '
        Me.RadGridViewLottoMateriale.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGridViewLottoMateriale.Location = New System.Drawing.Point(7, 12)
        '
        'RadGridViewLottoMateriale
        '
        Me.RadGridViewLottoMateriale.MasterTemplate.AllowAddNewRow = False
        Me.RadGridViewLottoMateriale.MasterTemplate.AllowColumnChooser = False
        Me.RadGridViewLottoMateriale.MasterTemplate.AllowColumnHeaderContextMenu = False
        Me.RadGridViewLottoMateriale.MasterTemplate.AllowDragToGroup = False
        Me.RadGridViewLottoMateriale.MasterTemplate.AllowRowResize = False
        Me.RadGridViewLottoMateriale.MasterTemplate.AutoGenerateColumns = False
        GridViewTextBoxColumn1.DataType = GetType(Integer)
        GridViewTextBoxColumn1.FieldName = "id"
        GridViewTextBoxColumn1.HeaderText = "ID"
        GridViewTextBoxColumn1.IsVisible = False
        GridViewTextBoxColumn1.Name = "IDTab"
        GridViewTextBoxColumn2.FieldName = "materiale"
        GridViewTextBoxColumn2.HeaderText = "Materiale"
        GridViewTextBoxColumn2.Name = "Materiale"
        GridViewTextBoxColumn2.Width = 200
        GridViewTextBoxColumn3.FieldName = "numeroLotto"
        GridViewTextBoxColumn3.HeaderText = "Numero Lotto"
        GridViewTextBoxColumn3.Name = "NumeroLotto"
        GridViewTextBoxColumn3.Width = 150
        GridViewTextBoxColumn4.FieldName = "fornitore"
        GridViewTextBoxColumn4.HeaderText = "Fornitore"
        GridViewTextBoxColumn4.Name = "Fornitore"
        GridViewTextBoxColumn4.Width = 200
        GridViewTextBoxColumn5.FieldName = "numDDT"
        GridViewTextBoxColumn5.HeaderText = "Numero DDT"
        GridViewTextBoxColumn5.Name = "NumDDT"
        GridViewTextBoxColumn5.Width = 200
        Me.RadGridViewLottoMateriale.MasterTemplate.Columns.AddRange(New Telerik.WinControls.UI.GridViewDataColumn() {GridViewTextBoxColumn1, GridViewTextBoxColumn2, GridViewTextBoxColumn3, GridViewTextBoxColumn4, GridViewTextBoxColumn5})
        Me.RadGridViewLottoMateriale.MasterTemplate.DataSource = Me.BindingSourceNumLottoMateriale
        Me.RadGridViewLottoMateriale.MasterTemplate.EnableFiltering = True
        Me.RadGridViewLottoMateriale.MasterTemplate.EnableGrouping = False
        Me.RadGridViewLottoMateriale.MasterTemplate.ShowGroupedColumns = True
        Me.RadGridViewLottoMateriale.MasterTemplate.ShowRowHeaderColumn = False
        Me.RadGridViewLottoMateriale.MasterTemplate.VerticalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow
        Me.RadGridViewLottoMateriale.Name = "RadGridViewLottoMateriale"
        Me.RadGridViewLottoMateriale.ShowGroupPanel = False
        Me.RadGridViewLottoMateriale.Size = New System.Drawing.Size(777, 454)
        Me.RadGridViewLottoMateriale.TabIndex = 0
        Me.RadGridViewLottoMateriale.Text = "GridViewLottoMateriale"
        Me.RadGridViewLottoMateriale.ThemeName = "BusinessGrid"
        '
        'BindingSourceNumLottoMateriale
        '
        Me.BindingSourceNumLottoMateriale.DataMember = "tblNumeroLottoMateriale"
        Me.BindingSourceNumLottoMateriale.DataSource = Me.DsNumeroLottoMateriale1
        '
        'DsNumeroLottoMateriale1
        '
        Me.DsNumeroLottoMateriale1.DataSetName = "dsNumeroLottoMateriale"
        Me.DsNumeroLottoMateriale1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'RadPanelTitoloDettagli
        '
        Me.RadPanelTitoloDettagli.BackColor = System.Drawing.Color.White
        Me.RadPanelTitoloDettagli.Controls.Add(Me.radLabelTitoloDettagli)
        Me.RadPanelTitoloDettagli.Location = New System.Drawing.Point(4, 3)
        Me.RadPanelTitoloDettagli.Name = "RadPanelTitoloDettagli"
        Me.RadPanelTitoloDettagli.Size = New System.Drawing.Size(279, 44)
        Me.RadPanelTitoloDettagli.TabIndex = 2
        '
        'radLabelTitoloDettagli
        '
        Me.radLabelTitoloDettagli.BackColor = System.Drawing.Color.Transparent
        Me.radLabelTitoloDettagli.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.radLabelTitoloDettagli.ForeColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(151, Byte), Integer))
        Me.radLabelTitoloDettagli.Location = New System.Drawing.Point(87, 12)
        Me.radLabelTitoloDettagli.Name = "radLabelTitoloDettagli"
        Me.radLabelTitoloDettagli.Size = New System.Drawing.Size(89, 21)
        Me.radLabelTitoloDettagli.TabIndex = 1
        Me.radLabelTitoloDettagli.Text = "Dettagli DDT "
        Me.radLabelTitoloDettagli.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.radLabelTitoloDettagli.ThemeName = "ControlDefault"
        '
        'RadThemeManager1
        '
        ThemeSource1.StorageType = Telerik.WinControls.ThemeStorageType.Resource
        ThemeSource1.ThemeLocation = "BusinessGrid.xml"
        ThemeSource2.StorageType = Telerik.WinControls.ThemeStorageType.Resource
        ThemeSource2.ThemeLocation = "BusinessGridScrollBar.xml"
        ThemeSource3.StorageType = Telerik.WinControls.ThemeStorageType.Resource
        ThemeSource3.ThemeLocation = "RadComboBoxLightBlue.xml"
        ThemeSource4.StorageType = Telerik.WinControls.ThemeStorageType.Resource
        ThemeSource4.ThemeLocation = "RadLabelTheme.xml"
        ThemeSource5.StorageType = Telerik.WinControls.ThemeStorageType.Resource
        ThemeSource5.ThemeLocation = "RadTextBoxLightBlue.xml"
        ThemeSource6.StorageType = Telerik.WinControls.ThemeStorageType.Resource
        ThemeSource6.ThemeLocation = "TitleBarBusinessGrid.xml"
        Me.RadThemeManager1.LoadedThemes.AddRange(New Telerik.WinControls.ThemeSource() {ThemeSource1, ThemeSource2, ThemeSource3, ThemeSource4, ThemeSource5, ThemeSource6})
        '
        'roundRectShape1
        '
        Me.roundRectShape1.BottomLeftRounded = False
        Me.roundRectShape1.BottomRightRounded = False
        Me.roundRectShape1.Radius = 10
        '
        'TblNumeroLottoMaterialeTableAdapter
        '
        Me.TblNumeroLottoMaterialeTableAdapter.ClearBeforeFill = True
        '
        'RadPanel4
        '
        Me.RadPanel4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadPanel4.Controls.Add(Me.txtFornitore)
        Me.RadPanel4.Controls.Add(Me.txtNumeroDDT)
        Me.RadPanel4.Controls.Add(Me.txtNumeroLotto)
        Me.RadPanel4.Controls.Add(Me.txtMateriale)
        Me.RadPanel4.Controls.Add(Me.updateButton)
        Me.RadPanel4.Controls.Add(Me.lblNumDDT)
        Me.RadPanel4.Controls.Add(Me.lblFornitore)
        Me.RadPanel4.Controls.Add(Me.lblNumeroLotto)
        Me.RadPanel4.Controls.Add(Me.lblMateriale)
        Me.RadPanel4.Location = New System.Drawing.Point(6, 49)
        Me.RadPanel4.Margin = New System.Windows.Forms.Padding(0)
        Me.RadPanel4.Name = "RadPanel4"
        Me.RadPanel4.Size = New System.Drawing.Size(277, 240)
        Me.RadPanel4.TabIndex = 19
        CType(Me.RadPanel4.GetChildAt(0).GetChildAt(0), Telerik.WinControls.Primitives.FillPrimitive).BackColor2 = System.Drawing.Color.Transparent
        CType(Me.RadPanel4.GetChildAt(0).GetChildAt(0), Telerik.WinControls.Primitives.FillPrimitive).GradientStyle = Telerik.WinControls.GradientStyles.Solid
        CType(Me.RadPanel4.GetChildAt(0).GetChildAt(0), Telerik.WinControls.Primitives.FillPrimitive).BackColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(255, Byte), Integer))
        CType(Me.RadPanel4.GetChildAt(0).GetChildAt(0), Telerik.WinControls.Primitives.FillPrimitive).SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed
        CType(Me.RadPanel4.GetChildAt(0).GetChildAt(1), Telerik.WinControls.Primitives.BorderPrimitive).ForeColor2 = System.Drawing.Color.Transparent
        CType(Me.RadPanel4.GetChildAt(0).GetChildAt(1), Telerik.WinControls.Primitives.BorderPrimitive).ForeColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(255, Byte), Integer))
        '
        'txtFornitore
        '
        Me.txtFornitore.Location = New System.Drawing.Point(101, 111)
        Me.txtFornitore.Name = "txtFornitore"
        Me.txtFornitore.Size = New System.Drawing.Size(159, 17)
        Me.txtFornitore.TabIndex = 25
        Me.txtFornitore.ThemeName = "LightBlue"
        '
        'txtNumeroDDT
        '
        Me.txtNumeroDDT.Location = New System.Drawing.Point(101, 147)
        Me.txtNumeroDDT.Name = "txtNumeroDDT"
        Me.txtNumeroDDT.Size = New System.Drawing.Size(159, 17)
        Me.txtNumeroDDT.TabIndex = 23
        Me.txtNumeroDDT.ThemeName = "LightBlue"
        '
        'txtNumeroLotto
        '
        Me.txtNumeroLotto.Location = New System.Drawing.Point(101, 76)
        Me.txtNumeroLotto.Name = "txtNumeroLotto"
        Me.txtNumeroLotto.Size = New System.Drawing.Size(159, 17)
        Me.txtNumeroLotto.TabIndex = 22
        Me.txtNumeroLotto.ThemeName = "LightBlue"
        '
        'txtMateriale
        '
        Me.txtMateriale.Location = New System.Drawing.Point(101, 42)
        Me.txtMateriale.Name = "txtMateriale"
        Me.txtMateriale.Size = New System.Drawing.Size(159, 17)
        Me.txtMateriale.TabIndex = 21
        Me.txtMateriale.ThemeName = "LightBlue"
        '
        'updateButton
        '
        Me.updateButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.updateButton.BackColor = System.Drawing.Color.Transparent
        Me.updateButton.Location = New System.Drawing.Point(101, 190)
        Me.updateButton.Name = "updateButton"
        Me.updateButton.Size = New System.Drawing.Size(159, 22)
        Me.updateButton.TabIndex = 18
        Me.updateButton.Text = "Aggiorna"
        Me.updateButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.updateButton.ThemeName = "ControlDefault"
        '
        'lblNumDDT
        '
        Me.lblNumDDT.AutoSize = False
        Me.lblNumDDT.BackColor = System.Drawing.Color.Transparent
        Me.lblNumDDT.ForeColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(151, Byte), Integer))
        Me.lblNumDDT.Location = New System.Drawing.Point(3, 147)
        Me.lblNumDDT.Name = "lblNumDDT"
        Me.lblNumDDT.Size = New System.Drawing.Size(92, 18)
        Me.lblNumDDT.TabIndex = 0
        Me.lblNumDDT.Text = "Numero DDT"
        Me.lblNumDDT.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.lblNumDDT.ThemeName = "RadLabelTheme"
        '
        'lblFornitore
        '
        Me.lblFornitore.AutoSize = False
        Me.lblFornitore.BackColor = System.Drawing.Color.Transparent
        Me.lblFornitore.ForeColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(151, Byte), Integer))
        Me.lblFornitore.Location = New System.Drawing.Point(3, 110)
        Me.lblFornitore.Name = "lblFornitore"
        Me.lblFornitore.Size = New System.Drawing.Size(92, 18)
        Me.lblFornitore.TabIndex = 1
        Me.lblFornitore.Text = "Fornitore"
        Me.lblFornitore.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.lblFornitore.ThemeName = "RadLabelTheme"
        '
        'lblNumeroLotto
        '
        Me.lblNumeroLotto.AutoSize = False
        Me.lblNumeroLotto.BackColor = System.Drawing.Color.Transparent
        Me.lblNumeroLotto.ForeColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(151, Byte), Integer))
        Me.lblNumeroLotto.Location = New System.Drawing.Point(3, 76)
        Me.lblNumeroLotto.Name = "lblNumeroLotto"
        Me.lblNumeroLotto.Size = New System.Drawing.Size(92, 18)
        Me.lblNumeroLotto.TabIndex = 0
        Me.lblNumeroLotto.Text = "Numero Lotto"
        Me.lblNumeroLotto.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.lblNumeroLotto.ThemeName = "RadLabelTheme"
        '
        'lblMateriale
        '
        Me.lblMateriale.AutoSize = False
        Me.lblMateriale.BackColor = System.Drawing.Color.Transparent
        Me.lblMateriale.ForeColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(151, Byte), Integer))
        Me.lblMateriale.Location = New System.Drawing.Point(3, 42)
        Me.lblMateriale.Name = "lblMateriale"
        Me.lblMateriale.Size = New System.Drawing.Size(92, 18)
        Me.lblMateriale.TabIndex = 0
        Me.lblMateriale.Text = "Materiale"
        Me.lblMateriale.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.lblMateriale.ThemeName = "RadLabelTheme"
        '
        'RadPanel2
        '
        Me.RadPanel2.BackColor = System.Drawing.Color.White
        Me.RadPanel2.Controls.Add(Me.RadPanel4)
        Me.RadPanel2.Controls.Add(Me.RadPanelTitoloDettagli)
        Me.RadPanel2.Location = New System.Drawing.Point(795, 43)
        Me.RadPanel2.Name = "RadPanel2"
        Me.RadPanel2.Size = New System.Drawing.Size(289, 303)
        Me.RadPanel2.TabIndex = 20
        '
        'gestioneLottoMateriale
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = New System.Drawing.Size(1089, 538)
        Me.Controls.Add(Me.RadPanel2)
        Me.Controls.Add(Me.RadPanel1)
        Me.Controls.Add(Me.RadCommandBar1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "gestioneLottoMateriale"
        Me.Text = "Gestione Lotto Materiale"
        CType(Me.RadCommandBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        CType(Me.RadGridViewLottoMateriale.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGridViewLottoMateriale, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingSourceNumLottoMateriale, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsNumeroLottoMateriale1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPanelTitoloDettagli, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanelTitoloDettagli.ResumeLayout(False)
        Me.RadPanelTitoloDettagli.PerformLayout()
        CType(Me.radLabelTitoloDettagli, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPanel4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel4.ResumeLayout(False)
        Me.RadPanel4.PerformLayout()
        CType(Me.txtFornitore, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNumeroDDT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNumeroLotto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMateriale, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.updateButton, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNumDDT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFornitore, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNumeroLotto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMateriale, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadCommandBar1 As Telerik.WinControls.UI.RadCommandBar
    Friend WithEvents CommandBarRowElement1 As Telerik.WinControls.UI.CommandBarRowElement
    Friend WithEvents CommandBarStripElement1 As Telerik.WinControls.UI.CommandBarStripElement
    Friend WithEvents CommandBarButtonAggiungiLotto As Telerik.WinControls.UI.CommandBarButton
    Friend WithEvents CommandBarButtonCancellaLotto As Telerik.WinControls.UI.CommandBarButton
    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents RadPanelTitoloDettagli As Telerik.WinControls.UI.RadPanel
    Friend WithEvents RadThemeManager1 As Telerik.WinControls.RadThemeManager
    Private WithEvents roundRectShape1 As Telerik.WinControls.RoundRectShape
    Friend WithEvents RadGridViewLottoMateriale As Telerik.WinControls.UI.RadGridView
    Friend WithEvents DsNumeroLottoMateriale1 As appGestioneTestReportBbg.dsNumeroLottoMateriale
    Friend WithEvents BindingSourceNumLottoMateriale As System.Windows.Forms.BindingSource
    Friend WithEvents TblNumeroLottoMaterialeTableAdapter As appGestioneTestReportBbg.dsNumeroLottoMaterialeTableAdapters.tblNumeroLottoMaterialeTableAdapter
    Private WithEvents radLabelTitoloDettagli As Telerik.WinControls.UI.RadLabel
    Private WithEvents RadPanel4 As Telerik.WinControls.UI.RadPanel
    Private WithEvents txtNumeroDDT As Telerik.WinControls.UI.RadTextBox
    Private WithEvents txtNumeroLotto As Telerik.WinControls.UI.RadTextBox
    Private WithEvents txtMateriale As Telerik.WinControls.UI.RadTextBox
    Private WithEvents updateButton As Telerik.WinControls.UI.RadButton
    Private WithEvents lblNumDDT As Telerik.WinControls.UI.RadLabel
    Private WithEvents lblFornitore As Telerik.WinControls.UI.RadLabel
    Private WithEvents lblNumeroLotto As Telerik.WinControls.UI.RadLabel
    Private WithEvents lblMateriale As Telerik.WinControls.UI.RadLabel
    Private WithEvents txtFornitore As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents CommandBarButtonEsci As Telerik.WinControls.UI.CommandBarButton
    Friend WithEvents RadPanel2 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents CommandBarSeparator1 As Telerik.WinControls.UI.CommandBarSeparator
End Class
