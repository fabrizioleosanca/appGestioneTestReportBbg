<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTestReportPdfAutomatico
    Inherits System.Windows.Forms.Form

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
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

    'Richiesto da Progettazione Windows Form
    Private components As System.ComponentModel.IContainer

    'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
    'Può essere modificata in Progettazione Windows Form.  
    'Non modificarla mediante l'editor del codice.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTestReportPdfAutomatico))
        Me.RadPanelTitolo = New Telerik.WinControls.UI.RadPanel()
        Me.lblTitolo = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblMod = New System.Windows.Forms.Label()
        Me.RadCommandBarStampaDaListaArt = New Telerik.WinControls.UI.RadCommandBar()
        Me.CommandBarRowElement1 = New Telerik.WinControls.UI.CommandBarRowElement()
        Me.CommandBarStripElement1 = New Telerik.WinControls.UI.CommandBarStripElement()
        Me.cmdImportaArticoli = New Telerik.WinControls.UI.CommandBarButton()
        Me.CommandBarSeparator3 = New Telerik.WinControls.UI.CommandBarSeparator()
        Me.cmdEsportaInPDF = New Telerik.WinControls.UI.CommandBarButton()
        Me.CommandBarSeparator1 = New Telerik.WinControls.UI.CommandBarSeparator()
        Me.cmdChiudi = New Telerik.WinControls.UI.CommandBarButton()
        Me.CommandBarSeparator2 = New Telerik.WinControls.UI.CommandBarSeparator()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripProgressBar1 = New System.Windows.Forms.ToolStripProgressBar()
        Me.RadGroupBoxListaArticoli = New Telerik.WinControls.UI.RadGroupBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.RadGroupBoxInfoArticolo = New Telerik.WinControls.UI.RadGroupBox()
        CType(Me.RadPanelTitolo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanelTitolo.SuspendLayout()
        CType(Me.RadCommandBarStampaDaListaArt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.RadGroupBoxListaArticoli, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBoxListaArticoli.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBoxInfoArticolo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPanelTitolo
        '
        Me.RadPanelTitolo.BackgroundImage = CType(resources.GetObject("RadPanelTitolo.BackgroundImage"), System.Drawing.Image)
        Me.RadPanelTitolo.Controls.Add(Me.lblTitolo)
        Me.RadPanelTitolo.Controls.Add(Me.Label1)
        Me.RadPanelTitolo.Controls.Add(Me.lblMod)
        Me.RadPanelTitolo.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanelTitolo.Location = New System.Drawing.Point(0, 0)
        Me.RadPanelTitolo.Name = "RadPanelTitolo"
        Me.RadPanelTitolo.Size = New System.Drawing.Size(1039, 70)
        Me.RadPanelTitolo.TabIndex = 2
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
        Me.lblTitolo.Size = New System.Drawing.Size(242, 35)
        Me.lblTitolo.TabIndex = 2
        Me.lblTitolo.Text = "Ricerca Test Report"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 16.25!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.RoyalBlue
        Me.Label1.Location = New System.Drawing.Point(605, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(215, 30)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "BBG Meccanica S.r.l"
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
        'RadCommandBarStampaDaListaArt
        '
        Me.RadCommandBarStampaDaListaArt.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadCommandBarStampaDaListaArt.Location = New System.Drawing.Point(0, 70)
        Me.RadCommandBarStampaDaListaArt.Name = "RadCommandBarStampaDaListaArt"
        Me.RadCommandBarStampaDaListaArt.Rows.AddRange(New Telerik.WinControls.UI.CommandBarRowElement() {Me.CommandBarRowElement1})
        Me.RadCommandBarStampaDaListaArt.Size = New System.Drawing.Size(1039, 72)
        Me.RadCommandBarStampaDaListaArt.TabIndex = 3
        Me.RadCommandBarStampaDaListaArt.Text = "RadCommandBarStampaDalistaArticoli"
        '
        'CommandBarRowElement1
        '
        Me.CommandBarRowElement1.MinSize = New System.Drawing.Size(25, 25)
        Me.CommandBarRowElement1.Strips.AddRange(New Telerik.WinControls.UI.CommandBarStripElement() {Me.CommandBarStripElement1})
        '
        'CommandBarStripElement1
        '
        Me.CommandBarStripElement1.DisplayName = "CommandBarStripElement1"
        Me.CommandBarStripElement1.Items.AddRange(New Telerik.WinControls.UI.RadCommandBarBaseItem() {Me.cmdImportaArticoli, Me.CommandBarSeparator3, Me.cmdEsportaInPDF, Me.CommandBarSeparator1, Me.cmdChiudi, Me.CommandBarSeparator2})
        Me.CommandBarStripElement1.Name = "CommandBarStripElement1"
        '
        'cmdImportaArticoli
        '
        Me.cmdImportaArticoli.AccessibleDescription = "Importa Lista Articoli"
        Me.cmdImportaArticoli.AccessibleName = "Importa Lista Articoli"
        Me.cmdImportaArticoli.DisplayName = "CommandBarButton1"
        Me.cmdImportaArticoli.DrawText = True
        Me.cmdImportaArticoli.Image = CType(resources.GetObject("cmdImportaArticoli.Image"), System.Drawing.Image)
        Me.cmdImportaArticoli.Name = "cmdImportaArticoli"
        Me.cmdImportaArticoli.Text = "Importa Lista Articoli"
        Me.cmdImportaArticoli.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdImportaArticoli.Visibility = Telerik.WinControls.ElementVisibility.Visible
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
        'cmdEsportaInPDF
        '
        Me.cmdEsportaInPDF.AccessibleDescription = "Esporta In PDF"
        Me.cmdEsportaInPDF.AccessibleName = "Esporta In PDF"
        Me.cmdEsportaInPDF.DisplayName = "CommandBarButton1"
        Me.cmdEsportaInPDF.DrawText = True
        Me.cmdEsportaInPDF.Image = CType(resources.GetObject("cmdEsportaInPDF.Image"), System.Drawing.Image)
        Me.cmdEsportaInPDF.Name = "cmdEsportaInPDF"
        Me.cmdEsportaInPDF.Text = "Esporta In PDF"
        Me.cmdEsportaInPDF.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdEsportaInPDF.Visibility = Telerik.WinControls.ElementVisibility.Visible
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
        'cmdChiudi
        '
        Me.cmdChiudi.AccessibleDescription = "Chiudi"
        Me.cmdChiudi.AccessibleName = "Chiudi"
        Me.cmdChiudi.DisplayName = "CommandBarButton1"
        Me.cmdChiudi.DrawText = True
        Me.cmdChiudi.Image = CType(resources.GetObject("cmdChiudi.Image"), System.Drawing.Image)
        Me.cmdChiudi.Name = "cmdChiudi"
        Me.cmdChiudi.Text = "Chiudi"
        Me.cmdChiudi.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdChiudi.Visibility = Telerik.WinControls.ElementVisibility.Visible
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
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.ToolStripProgressBar1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 509)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1039, 22)
        Me.StatusStrip1.TabIndex = 5
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(0, 17)
        '
        'ToolStripProgressBar1
        '
        Me.ToolStripProgressBar1.Name = "ToolStripProgressBar1"
        Me.ToolStripProgressBar1.Size = New System.Drawing.Size(100, 16)
        Me.ToolStripProgressBar1.ToolTipText = "Attendi Stampa In PDF"
        Me.ToolStripProgressBar1.Visible = False
        '
        'RadGroupBoxListaArticoli
        '
        Me.RadGroupBoxListaArticoli.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBoxListaArticoli.Controls.Add(Me.DataGridView1)
        Me.RadGroupBoxListaArticoli.HeaderText = "Lista Articoli Da Esportare In PDF"
        Me.RadGroupBoxListaArticoli.Location = New System.Drawing.Point(12, 170)
        Me.RadGroupBoxListaArticoli.Name = "RadGroupBoxListaArticoli"
        Me.RadGroupBoxListaArticoli.Size = New System.Drawing.Size(563, 326)
        Me.RadGroupBoxListaArticoli.TabIndex = 6
        Me.RadGroupBoxListaArticoli.Text = "Lista Articoli Da Esportare In PDF"
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(2, 18)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(559, 306)
        Me.DataGridView1.TabIndex = 0
        '
        'RadGroupBoxInfoArticolo
        '
        Me.RadGroupBoxInfoArticolo.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBoxInfoArticolo.HeaderText = "Info Articolo Da Esportare"
        Me.RadGroupBoxInfoArticolo.Location = New System.Drawing.Point(581, 170)
        Me.RadGroupBoxInfoArticolo.Name = "RadGroupBoxInfoArticolo"
        Me.RadGroupBoxInfoArticolo.Size = New System.Drawing.Size(446, 326)
        Me.RadGroupBoxInfoArticolo.TabIndex = 7
        Me.RadGroupBoxInfoArticolo.Text = "Info Articolo Da Esportare"
        '
        'frmTestReportPdfAutomatico
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1039, 531)
        Me.Controls.Add(Me.RadGroupBoxInfoArticolo)
        Me.Controls.Add(Me.RadGroupBoxListaArticoli)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.RadCommandBarStampaDaListaArt)
        Me.Controls.Add(Me.RadPanelTitolo)
        Me.Name = "frmTestReportPdfAutomatico"
        Me.Text = "Esporta Test Report In PDF Da Lista Articoli"
        CType(Me.RadPanelTitolo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanelTitolo.ResumeLayout(False)
        Me.RadPanelTitolo.PerformLayout()
        CType(Me.RadCommandBarStampaDaListaArt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.RadGroupBoxListaArticoli, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBoxListaArticoli.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBoxInfoArticolo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RadPanelTitolo As Telerik.WinControls.UI.RadPanel
    Friend WithEvents lblTitolo As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents lblMod As Label
    Friend WithEvents RadCommandBarStampaDaListaArt As Telerik.WinControls.UI.RadCommandBar
    Friend WithEvents CommandBarRowElement1 As Telerik.WinControls.UI.CommandBarRowElement
    Friend WithEvents CommandBarStripElement1 As Telerik.WinControls.UI.CommandBarStripElement
    Friend WithEvents cmdEsportaInPDF As Telerik.WinControls.UI.CommandBarButton
    Friend WithEvents CommandBarSeparator1 As Telerik.WinControls.UI.CommandBarSeparator
    Friend WithEvents cmdChiudi As Telerik.WinControls.UI.CommandBarButton
    Friend WithEvents CommandBarSeparator2 As Telerik.WinControls.UI.CommandBarSeparator
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents cmdImportaArticoli As Telerik.WinControls.UI.CommandBarButton
    Friend WithEvents CommandBarSeparator3 As Telerik.WinControls.UI.CommandBarSeparator
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents ToolStripProgressBar1 As ToolStripProgressBar
    Friend WithEvents RadGroupBoxListaArticoli As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBoxInfoArticolo As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents DataGridView1 As DataGridView
End Class
