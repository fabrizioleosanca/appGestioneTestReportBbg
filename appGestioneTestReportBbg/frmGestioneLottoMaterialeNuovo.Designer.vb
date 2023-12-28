<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGestioneLottoMaterialeNuovo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGestioneLottoMaterialeNuovo))
        Dim GridViewDecimalColumn2 As Telerik.WinControls.UI.GridViewDecimalColumn = New Telerik.WinControls.UI.GridViewDecimalColumn()
        Dim GridViewTextBoxColumn5 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn6 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn7 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn8 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Me.RadCommandBarLotto = New Telerik.WinControls.UI.RadCommandBar()
        Me.CommandBarRowElementBottoni = New Telerik.WinControls.UI.CommandBarRowElement()
        Me.CommandBarStripElement1 = New Telerik.WinControls.UI.CommandBarStripElement()
        Me.btnAggiungiLottoMateriale = New Telerik.WinControls.UI.CommandBarButton()
        Me.btnAggiorna = New Telerik.WinControls.UI.CommandBarButton()
        Me.btnEliminaLotto = New Telerik.WinControls.UI.CommandBarButton()
        Me.CommandBarSeparator1 = New Telerik.WinControls.UI.CommandBarSeparator()
        Me.btnChiudi = New Telerik.WinControls.UI.CommandBarButton()
        Me.RadGroupBoxAggLotto = New Telerik.WinControls.UI.RadGroupBox()
        Me.lblNumeroDDT = New Telerik.WinControls.UI.RadLabel()
        Me.txtNumeroDDT = New Telerik.WinControls.UI.RadTextBox()
        Me.lblNumeroLotto = New Telerik.WinControls.UI.RadLabel()
        Me.cmbFornitore = New Telerik.WinControls.UI.RadDropDownList()
        Me.lblFornitore = New Telerik.WinControls.UI.RadLabel()
        Me.cmbMateriali = New Telerik.WinControls.UI.RadDropDownList()
        Me.lblMateriale = New Telerik.WinControls.UI.RadLabel()
        Me.txtNumeroLotto = New Telerik.WinControls.UI.RadTextBox()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsNumeroLottoMateriale1 = New appGestioneTestReportBbg.dsNumeroLottoMateriale()
        Me.RadGroupBoxAggiornaLotto = New Telerik.WinControls.UI.RadGroupBox()
        Me.lblAggiornaNumeroDDT = New Telerik.WinControls.UI.RadLabel()
        Me.txtAggiornaNumeroDDT = New Telerik.WinControls.UI.RadTextBox()
        Me.lblAggiornaNumeroLotto = New Telerik.WinControls.UI.RadLabel()
        Me.txtAggiornaNumeroLotto = New Telerik.WinControls.UI.RadTextBox()
        Me.lblAggiornaFornitore = New Telerik.WinControls.UI.RadLabel()
        Me.txtAggiornaFornitore = New Telerik.WinControls.UI.RadTextBox()
        Me.lblMaterialeUpdate = New Telerik.WinControls.UI.RadLabel()
        Me.txtAggiornaMateriale = New Telerik.WinControls.UI.RadTextBox()
        Me.RadGroupBoxGridLotto = New Telerik.WinControls.UI.RadGroupBox()
        Me.gridLottoMateriale = New Telerik.WinControls.UI.RadGridView()
        Me.StatusStripFormLotto = New System.Windows.Forms.StatusStrip()
        Me.TblNumeroLottoMaterialeTableAdapter = New appGestioneTestReportBbg.dsNumeroLottoMaterialeTableAdapters.tblNumeroLottoMaterialeTableAdapter()
        CType(Me.RadCommandBarLotto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBoxAggLotto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBoxAggLotto.SuspendLayout()
        CType(Me.lblNumeroDDT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNumeroDDT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNumeroLotto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbFornitore, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFornitore, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbMateriali, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMateriale, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNumeroLotto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsNumeroLottoMateriale1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBoxAggiornaLotto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBoxAggiornaLotto.SuspendLayout()
        CType(Me.lblAggiornaNumeroDDT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAggiornaNumeroDDT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAggiornaNumeroLotto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAggiornaNumeroLotto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAggiornaFornitore, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAggiornaFornitore, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMaterialeUpdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAggiornaMateriale, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBoxGridLotto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBoxGridLotto.SuspendLayout()
        CType(Me.gridLottoMateriale, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridLottoMateriale.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadCommandBarLotto
        '
        Me.RadCommandBarLotto.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadCommandBarLotto.Location = New System.Drawing.Point(0, 0)
        Me.RadCommandBarLotto.Name = "RadCommandBarLotto"
        Me.RadCommandBarLotto.Rows.AddRange(New Telerik.WinControls.UI.CommandBarRowElement() {Me.CommandBarRowElementBottoni})
        Me.RadCommandBarLotto.Size = New System.Drawing.Size(1159, 56)
        Me.RadCommandBarLotto.TabIndex = 0
        Me.RadCommandBarLotto.Text = "Lotto Materiale"
        '
        'CommandBarRowElementBottoni
        '
        Me.CommandBarRowElementBottoni.MinSize = New System.Drawing.Size(25, 25)
        Me.CommandBarRowElementBottoni.Strips.AddRange(New Telerik.WinControls.UI.CommandBarStripElement() {Me.CommandBarStripElement1})
        '
        'CommandBarStripElement1
        '
        Me.CommandBarStripElement1.DisplayName = "CommandBarStripElement1"
        Me.CommandBarStripElement1.Items.AddRange(New Telerik.WinControls.UI.RadCommandBarBaseItem() {Me.btnAggiungiLottoMateriale, Me.btnAggiorna, Me.btnEliminaLotto, Me.CommandBarSeparator1, Me.btnChiudi})
        Me.CommandBarStripElement1.Name = "CommandBarStripElement1"
        '
        'btnAggiungiLottoMateriale
        '
        Me.btnAggiungiLottoMateriale.AccessibleDescription = "Aggiungi Lotto Materiale"
        Me.btnAggiungiLottoMateriale.AccessibleName = "Aggiungi Lotto Materiale"
        Me.btnAggiungiLottoMateriale.DisplayName = "CommandBarButton1"
        Me.btnAggiungiLottoMateriale.DrawText = True
        Me.btnAggiungiLottoMateriale.Image = CType(resources.GetObject("btnAggiungiLottoMateriale.Image"), System.Drawing.Image)
        Me.btnAggiungiLottoMateriale.Name = "btnAggiungiLottoMateriale"
        Me.btnAggiungiLottoMateriale.Text = "Aggiungi Lotto Materiale"
        Me.btnAggiungiLottoMateriale.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnAggiungiLottoMateriale.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnAggiorna
        '
        Me.btnAggiorna.AccessibleDescription = "Aggiorna Lotto Materiale"
        Me.btnAggiorna.AccessibleName = "Aggiorna Lotto Materiale"
        Me.btnAggiorna.DisplayName = "CommandBarButton1"
        Me.btnAggiorna.DrawText = True
        Me.btnAggiorna.Image = CType(resources.GetObject("btnAggiorna.Image"), System.Drawing.Image)
        Me.btnAggiorna.Name = "btnAggiorna"
        Me.btnAggiorna.Text = "Aggiorna Lotto Materiale"
        Me.btnAggiorna.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnAggiorna.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnEliminaLotto
        '
        Me.btnEliminaLotto.AccessibleDescription = "Elimina Lotto Materiale"
        Me.btnEliminaLotto.AccessibleName = "Elimina Lotto Materiale"
        Me.btnEliminaLotto.DisplayName = "CommandBarButton1"
        Me.btnEliminaLotto.DrawText = True
        Me.btnEliminaLotto.Image = CType(resources.GetObject("btnEliminaLotto.Image"), System.Drawing.Image)
        Me.btnEliminaLotto.Name = "btnEliminaLotto"
        Me.btnEliminaLotto.Text = "Elimina Lotto Materiale"
        Me.btnEliminaLotto.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnEliminaLotto.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'CommandBarSeparator1
        '
        Me.CommandBarSeparator1.AccessibleDescription = "CommandBarSeparator1"
        Me.CommandBarSeparator1.AccessibleName = "CommandBarSeparator1"
        Me.CommandBarSeparator1.DisplayName = "CommandBarSeparator1"
        Me.CommandBarSeparator1.Name = "CommandBarSeparator1"
        Me.CommandBarSeparator1.Padding = New System.Windows.Forms.Padding(0)
        Me.CommandBarSeparator1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        Me.CommandBarSeparator1.VisibleInOverflowMenu = False
        '
        'btnChiudi
        '
        Me.btnChiudi.AccessibleDescription = "Chiudi"
        Me.btnChiudi.AccessibleName = "Chiudi"
        Me.btnChiudi.DisplayName = "btnChiudi"
        Me.btnChiudi.DrawText = True
        Me.btnChiudi.Image = CType(resources.GetObject("btnChiudi.Image"), System.Drawing.Image)
        Me.btnChiudi.Name = "btnChiudi"
        Me.btnChiudi.Text = "Chiudi"
        Me.btnChiudi.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnChiudi.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadGroupBoxAggLotto
        '
        Me.RadGroupBoxAggLotto.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBoxAggLotto.Controls.Add(Me.lblNumeroDDT)
        Me.RadGroupBoxAggLotto.Controls.Add(Me.txtNumeroDDT)
        Me.RadGroupBoxAggLotto.Controls.Add(Me.lblNumeroLotto)
        Me.RadGroupBoxAggLotto.Controls.Add(Me.cmbFornitore)
        Me.RadGroupBoxAggLotto.Controls.Add(Me.lblFornitore)
        Me.RadGroupBoxAggLotto.Controls.Add(Me.cmbMateriali)
        Me.RadGroupBoxAggLotto.Controls.Add(Me.lblMateriale)
        Me.RadGroupBoxAggLotto.Controls.Add(Me.txtNumeroLotto)
        Me.RadGroupBoxAggLotto.HeaderText = "Aggiungi Nuovo Lotto Materiale"
        Me.RadGroupBoxAggLotto.Location = New System.Drawing.Point(14, 74)
        Me.RadGroupBoxAggLotto.Name = "RadGroupBoxAggLotto"
        Me.RadGroupBoxAggLotto.Size = New System.Drawing.Size(692, 163)
        Me.RadGroupBoxAggLotto.TabIndex = 1
        Me.RadGroupBoxAggLotto.Text = "Aggiungi Nuovo Lotto Materiale"
        '
        'lblNumeroDDT
        '
        Me.lblNumeroDDT.Location = New System.Drawing.Point(358, 82)
        Me.lblNumeroDDT.Name = "lblNumeroDDT"
        Me.lblNumeroDDT.Size = New System.Drawing.Size(72, 18)
        Me.lblNumeroDDT.TabIndex = 7
        Me.lblNumeroDDT.Text = "Numero DDT"
        '
        'txtNumeroDDT
        '
        Me.txtNumeroDDT.Location = New System.Drawing.Point(436, 82)
        Me.txtNumeroDDT.Name = "txtNumeroDDT"
        Me.txtNumeroDDT.Size = New System.Drawing.Size(239, 20)
        Me.txtNumeroDDT.TabIndex = 6
        '
        'lblNumeroLotto
        '
        Me.lblNumeroLotto.Location = New System.Drawing.Point(17, 82)
        Me.lblNumeroLotto.Name = "lblNumeroLotto"
        Me.lblNumeroLotto.Size = New System.Drawing.Size(77, 18)
        Me.lblNumeroLotto.TabIndex = 5
        Me.lblNumeroLotto.Text = "Numero Lotto"
        '
        'cmbFornitore
        '
        Me.cmbFornitore.Location = New System.Drawing.Point(436, 38)
        Me.cmbFornitore.Name = "cmbFornitore"
        Me.cmbFornitore.Size = New System.Drawing.Size(239, 20)
        Me.cmbFornitore.TabIndex = 4
        '
        'lblFornitore
        '
        Me.lblFornitore.Location = New System.Drawing.Point(378, 40)
        Me.lblFornitore.Name = "lblFornitore"
        Me.lblFornitore.Size = New System.Drawing.Size(52, 18)
        Me.lblFornitore.TabIndex = 3
        Me.lblFornitore.Text = "Fornitore"
        '
        'cmbMateriali
        '
        Me.cmbMateriali.Location = New System.Drawing.Point(100, 38)
        Me.cmbMateriali.Name = "cmbMateriali"
        Me.cmbMateriali.Size = New System.Drawing.Size(239, 20)
        Me.cmbMateriali.TabIndex = 2
        '
        'lblMateriale
        '
        Me.lblMateriale.Location = New System.Drawing.Point(41, 40)
        Me.lblMateriale.Name = "lblMateriale"
        Me.lblMateriale.Size = New System.Drawing.Size(53, 18)
        Me.lblMateriale.TabIndex = 1
        Me.lblMateriale.Text = "Materiale"
        '
        'txtNumeroLotto
        '
        Me.txtNumeroLotto.Location = New System.Drawing.Point(100, 80)
        Me.txtNumeroLotto.Name = "txtNumeroLotto"
        Me.txtNumeroLotto.Size = New System.Drawing.Size(239, 20)
        Me.txtNumeroLotto.TabIndex = 0
        '
        'BindingSource1
        '
        Me.BindingSource1.DataMember = "tblNumeroLottoMateriale"
        Me.BindingSource1.DataSource = Me.DsNumeroLottoMateriale1
        Me.BindingSource1.Sort = "id"
        '
        'DsNumeroLottoMateriale1
        '
        Me.DsNumeroLottoMateriale1.DataSetName = "dsNumeroLottoMateriale"
        Me.DsNumeroLottoMateriale1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'RadGroupBoxAggiornaLotto
        '
        Me.RadGroupBoxAggiornaLotto.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBoxAggiornaLotto.Controls.Add(Me.lblAggiornaNumeroDDT)
        Me.RadGroupBoxAggiornaLotto.Controls.Add(Me.txtAggiornaNumeroDDT)
        Me.RadGroupBoxAggiornaLotto.Controls.Add(Me.lblAggiornaNumeroLotto)
        Me.RadGroupBoxAggiornaLotto.Controls.Add(Me.txtAggiornaNumeroLotto)
        Me.RadGroupBoxAggiornaLotto.Controls.Add(Me.lblAggiornaFornitore)
        Me.RadGroupBoxAggiornaLotto.Controls.Add(Me.txtAggiornaFornitore)
        Me.RadGroupBoxAggiornaLotto.Controls.Add(Me.lblMaterialeUpdate)
        Me.RadGroupBoxAggiornaLotto.Controls.Add(Me.txtAggiornaMateriale)
        Me.RadGroupBoxAggiornaLotto.HeaderText = "Aggiorna Lotto Materiale"
        Me.RadGroupBoxAggiornaLotto.Location = New System.Drawing.Point(726, 74)
        Me.RadGroupBoxAggiornaLotto.Name = "RadGroupBoxAggiornaLotto"
        Me.RadGroupBoxAggiornaLotto.Size = New System.Drawing.Size(421, 163)
        Me.RadGroupBoxAggiornaLotto.TabIndex = 2
        Me.RadGroupBoxAggiornaLotto.Text = "Aggiorna Lotto Materiale"
        '
        'lblAggiornaNumeroDDT
        '
        Me.lblAggiornaNumeroDDT.Location = New System.Drawing.Point(9, 119)
        Me.lblAggiornaNumeroDDT.Name = "lblAggiornaNumeroDDT"
        Me.lblAggiornaNumeroDDT.Size = New System.Drawing.Size(121, 18)
        Me.lblAggiornaNumeroDDT.TabIndex = 15
        Me.lblAggiornaNumeroDDT.Text = "Aggiorna Numero DDT"
        '
        'txtAggiornaNumeroDDT
        '
        Me.txtAggiornaNumeroDDT.Location = New System.Drawing.Point(137, 119)
        Me.txtAggiornaNumeroDDT.Name = "txtAggiornaNumeroDDT"
        Me.txtAggiornaNumeroDDT.Size = New System.Drawing.Size(268, 20)
        Me.txtAggiornaNumeroDDT.TabIndex = 14
        '
        'lblAggiornaNumeroLotto
        '
        Me.lblAggiornaNumeroLotto.Location = New System.Drawing.Point(4, 90)
        Me.lblAggiornaNumeroLotto.Name = "lblAggiornaNumeroLotto"
        Me.lblAggiornaNumeroLotto.Size = New System.Drawing.Size(126, 18)
        Me.lblAggiornaNumeroLotto.TabIndex = 13
        Me.lblAggiornaNumeroLotto.Text = "Aggiorna Numero Lotto"
        '
        'txtAggiornaNumeroLotto
        '
        Me.txtAggiornaNumeroLotto.Location = New System.Drawing.Point(137, 90)
        Me.txtAggiornaNumeroLotto.Name = "txtAggiornaNumeroLotto"
        Me.txtAggiornaNumeroLotto.Size = New System.Drawing.Size(268, 20)
        Me.txtAggiornaNumeroLotto.TabIndex = 12
        '
        'lblAggiornaFornitore
        '
        Me.lblAggiornaFornitore.Location = New System.Drawing.Point(29, 64)
        Me.lblAggiornaFornitore.Name = "lblAggiornaFornitore"
        Me.lblAggiornaFornitore.Size = New System.Drawing.Size(101, 18)
        Me.lblAggiornaFornitore.TabIndex = 11
        Me.lblAggiornaFornitore.Text = "Aggiorna Fornitore"
        '
        'txtAggiornaFornitore
        '
        Me.txtAggiornaFornitore.Location = New System.Drawing.Point(137, 64)
        Me.txtAggiornaFornitore.Name = "txtAggiornaFornitore"
        Me.txtAggiornaFornitore.Size = New System.Drawing.Size(268, 20)
        Me.txtAggiornaFornitore.TabIndex = 10
        '
        'lblMaterialeUpdate
        '
        Me.lblMaterialeUpdate.Location = New System.Drawing.Point(28, 38)
        Me.lblMaterialeUpdate.Name = "lblMaterialeUpdate"
        Me.lblMaterialeUpdate.Size = New System.Drawing.Size(102, 18)
        Me.lblMaterialeUpdate.TabIndex = 9
        Me.lblMaterialeUpdate.Text = "Aggiorna Materiale"
        '
        'txtAggiornaMateriale
        '
        Me.txtAggiornaMateriale.Location = New System.Drawing.Point(137, 38)
        Me.txtAggiornaMateriale.Name = "txtAggiornaMateriale"
        Me.txtAggiornaMateriale.Size = New System.Drawing.Size(268, 20)
        Me.txtAggiornaMateriale.TabIndex = 8
        '
        'RadGroupBoxGridLotto
        '
        Me.RadGroupBoxGridLotto.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBoxGridLotto.Controls.Add(Me.gridLottoMateriale)
        Me.RadGroupBoxGridLotto.HeaderText = "Elenco Lotto Materiale"
        Me.RadGroupBoxGridLotto.Location = New System.Drawing.Point(17, 251)
        Me.RadGroupBoxGridLotto.Name = "RadGroupBoxGridLotto"
        Me.RadGroupBoxGridLotto.Size = New System.Drawing.Size(1130, 444)
        Me.RadGroupBoxGridLotto.TabIndex = 3
        Me.RadGroupBoxGridLotto.Text = "Elenco Lotto Materiale"
        '
        'gridLottoMateriale
        '
        Me.gridLottoMateriale.Location = New System.Drawing.Point(14, 21)
        '
        'gridLottoMateriale
        '
        Me.gridLottoMateriale.MasterTemplate.AllowAddNewRow = False
        Me.gridLottoMateriale.MasterTemplate.AllowEditRow = False
        GridViewDecimalColumn2.DataType = GetType(Integer)
        GridViewDecimalColumn2.DecimalPlaces = 0
        GridViewDecimalColumn2.FieldName = "id"
        GridViewDecimalColumn2.HeaderText = "ID"
        GridViewDecimalColumn2.IsAutoGenerated = True
        GridViewDecimalColumn2.Name = "id"
        GridViewDecimalColumn2.ReadOnly = True
        GridViewDecimalColumn2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        GridViewDecimalColumn2.Width = 70
        GridViewTextBoxColumn5.FieldName = "materiale"
        GridViewTextBoxColumn5.HeaderText = "Materiale Lotto"
        GridViewTextBoxColumn5.IsAutoGenerated = True
        GridViewTextBoxColumn5.Name = "materiale"
        GridViewTextBoxColumn5.Width = 200
        GridViewTextBoxColumn6.FieldName = "numeroLotto"
        GridViewTextBoxColumn6.HeaderText = "Numero Lotto Materiale"
        GridViewTextBoxColumn6.IsAutoGenerated = True
        GridViewTextBoxColumn6.Name = "numeroLotto"
        GridViewTextBoxColumn6.Width = 280
        GridViewTextBoxColumn7.FieldName = "fornitore"
        GridViewTextBoxColumn7.HeaderText = "Fornitore Materiale"
        GridViewTextBoxColumn7.IsAutoGenerated = True
        GridViewTextBoxColumn7.Name = "fornitore"
        GridViewTextBoxColumn7.Width = 200
        GridViewTextBoxColumn8.FieldName = "numDDT"
        GridViewTextBoxColumn8.HeaderText = "Numero DDT Lotto"
        GridViewTextBoxColumn8.IsAutoGenerated = True
        GridViewTextBoxColumn8.Name = "numDDT"
        GridViewTextBoxColumn8.Width = 300
        Me.gridLottoMateriale.MasterTemplate.Columns.AddRange(New Telerik.WinControls.UI.GridViewDataColumn() {GridViewDecimalColumn2, GridViewTextBoxColumn5, GridViewTextBoxColumn6, GridViewTextBoxColumn7, GridViewTextBoxColumn8})
        Me.gridLottoMateriale.MasterTemplate.DataSource = Me.BindingSource1
        Me.gridLottoMateriale.MasterTemplate.EnableFiltering = True
        Me.gridLottoMateriale.Name = "gridLottoMateriale"
        Me.gridLottoMateriale.NewRowEnterKeyMode = Telerik.WinControls.UI.RadGridViewNewRowEnterKeyMode.EnterMovesToLastAddedRow
        Me.gridLottoMateriale.Size = New System.Drawing.Size(1100, 414)
        Me.gridLottoMateriale.TabIndex = 0
        '
        'StatusStripFormLotto
        '
        Me.StatusStripFormLotto.Location = New System.Drawing.Point(0, 702)
        Me.StatusStripFormLotto.Name = "StatusStripFormLotto"
        Me.StatusStripFormLotto.Size = New System.Drawing.Size(1159, 22)
        Me.StatusStripFormLotto.TabIndex = 4
        '
        'TblNumeroLottoMaterialeTableAdapter
        '
        Me.TblNumeroLottoMaterialeTableAdapter.ClearBeforeFill = True
        '
        'frmGestioneLottoMaterialeNuovo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1159, 724)
        Me.Controls.Add(Me.StatusStripFormLotto)
        Me.Controls.Add(Me.RadGroupBoxGridLotto)
        Me.Controls.Add(Me.RadGroupBoxAggiornaLotto)
        Me.Controls.Add(Me.RadGroupBoxAggLotto)
        Me.Controls.Add(Me.RadCommandBarLotto)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmGestioneLottoMaterialeNuovo"
        Me.Text = "Gestione Lotto Materiale"
        CType(Me.RadCommandBarLotto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBoxAggLotto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBoxAggLotto.ResumeLayout(False)
        Me.RadGroupBoxAggLotto.PerformLayout()
        CType(Me.lblNumeroDDT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNumeroDDT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNumeroLotto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbFornitore, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFornitore, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbMateriali, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMateriale, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNumeroLotto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsNumeroLottoMateriale1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBoxAggiornaLotto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBoxAggiornaLotto.ResumeLayout(False)
        Me.RadGroupBoxAggiornaLotto.PerformLayout()
        CType(Me.lblAggiornaNumeroDDT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAggiornaNumeroDDT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAggiornaNumeroLotto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAggiornaNumeroLotto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAggiornaFornitore, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAggiornaFornitore, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMaterialeUpdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAggiornaMateriale, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBoxGridLotto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBoxGridLotto.ResumeLayout(False)
        CType(Me.gridLottoMateriale.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridLottoMateriale, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadCommandBarLotto As Telerik.WinControls.UI.RadCommandBar
    Friend WithEvents CommandBarRowElementBottoni As Telerik.WinControls.UI.CommandBarRowElement
    Friend WithEvents CommandBarStripElement1 As Telerik.WinControls.UI.CommandBarStripElement
    Friend WithEvents btnAggiungiLottoMateriale As Telerik.WinControls.UI.CommandBarButton
    Friend WithEvents btnAggiorna As Telerik.WinControls.UI.CommandBarButton
    Friend WithEvents btnEliminaLotto As Telerik.WinControls.UI.CommandBarButton
    Friend WithEvents CommandBarSeparator1 As Telerik.WinControls.UI.CommandBarSeparator
    Friend WithEvents btnChiudi As Telerik.WinControls.UI.CommandBarButton
    Friend WithEvents RadGroupBoxAggLotto As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cmbMateriali As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents lblMateriale As Telerik.WinControls.UI.RadLabel
    Friend WithEvents txtNumeroLotto As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents cmbFornitore As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents lblFornitore As Telerik.WinControls.UI.RadLabel
    Friend WithEvents lblNumeroLotto As Telerik.WinControls.UI.RadLabel
    Friend WithEvents lblNumeroDDT As Telerik.WinControls.UI.RadLabel
    Friend WithEvents txtNumeroDDT As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents RadGroupBoxAggiornaLotto As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents lblAggiornaFornitore As Telerik.WinControls.UI.RadLabel
    Friend WithEvents txtAggiornaFornitore As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents lblMaterialeUpdate As Telerik.WinControls.UI.RadLabel
    Friend WithEvents txtAggiornaMateriale As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents lblAggiornaNumeroLotto As Telerik.WinControls.UI.RadLabel
    Friend WithEvents txtAggiornaNumeroLotto As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents lblAggiornaNumeroDDT As Telerik.WinControls.UI.RadLabel
    Friend WithEvents txtAggiornaNumeroDDT As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents RadGroupBoxGridLotto As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents StatusStripFormLotto As System.Windows.Forms.StatusStrip
    Friend WithEvents gridLottoMateriale As Telerik.WinControls.UI.RadGridView
    Friend WithEvents DsNumeroLottoMateriale1 As appGestioneTestReportBbg.dsNumeroLottoMateriale
    Friend WithEvents BindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents TblNumeroLottoMaterialeTableAdapter As appGestioneTestReportBbg.dsNumeroLottoMaterialeTableAdapters.tblNumeroLottoMaterialeTableAdapter
End Class
