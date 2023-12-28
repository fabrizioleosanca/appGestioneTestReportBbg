<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOperatori
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOperatori))
        Me.RadCommandBar1 = New Telerik.WinControls.UI.RadCommandBar()
        Me.CommandBarRowElement1 = New Telerik.WinControls.UI.CommandBarRowElement()
        Me.CommandBarStripElement1 = New Telerik.WinControls.UI.CommandBarStripElement()
        Me.cmdAddOperatore = New Telerik.WinControls.UI.CommandBarButton()
        Me.CommandBarSeparator1 = New Telerik.WinControls.UI.CommandBarSeparator()
        Me.cmdModificaOperatore = New Telerik.WinControls.UI.CommandBarButton()
        Me.CommandBarSeparator2 = New Telerik.WinControls.UI.CommandBarSeparator()
        Me.cmdCancellaOperatore = New Telerik.WinControls.UI.CommandBarButton()
        Me.CommandBarSeparator3 = New Telerik.WinControls.UI.CommandBarSeparator()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.CommandBarStripElement2 = New Telerik.WinControls.UI.CommandBarStripElement()
        Me.cmdChiudi = New Telerik.WinControls.UI.CommandBarButton()
        Me.CommandBarSeparator4 = New Telerik.WinControls.UI.CommandBarSeparator()
        CType(Me.RadCommandBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadCommandBar1
        '
        Me.RadCommandBar1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadCommandBar1.Location = New System.Drawing.Point(0, 0)
        Me.RadCommandBar1.Name = "RadCommandBar1"
        Me.RadCommandBar1.Rows.AddRange(New Telerik.WinControls.UI.CommandBarRowElement() {Me.CommandBarRowElement1})
        Me.RadCommandBar1.Size = New System.Drawing.Size(662, 56)
        Me.RadCommandBar1.TabIndex = 0
        Me.RadCommandBar1.Text = "RadCommandBar1"
        '
        'CommandBarRowElement1
        '
        Me.CommandBarRowElement1.MinSize = New System.Drawing.Size(25, 25)
        Me.CommandBarRowElement1.Strips.AddRange(New Telerik.WinControls.UI.CommandBarStripElement() {Me.CommandBarStripElement1})
        Me.CommandBarRowElement1.Text = ""
        '
        'CommandBarStripElement1
        '
        Me.CommandBarStripElement1.DisplayName = "CommandBarStripElement1"
        Me.CommandBarStripElement1.Items.AddRange(New Telerik.WinControls.UI.RadCommandBarBaseItem() {Me.cmdAddOperatore, Me.CommandBarSeparator1, Me.cmdModificaOperatore, Me.CommandBarSeparator2, Me.cmdCancellaOperatore, Me.CommandBarSeparator3, Me.cmdChiudi, Me.CommandBarSeparator4})
        Me.CommandBarStripElement1.Name = "CommandBarStripElement1"
        '
        'cmdAddOperatore
        '
        Me.cmdAddOperatore.AccessibleDescription = "Aggiungi Operatore"
        Me.cmdAddOperatore.AccessibleName = "Aggiungi Operatore"
        Me.cmdAddOperatore.DisplayName = "CommandBarButton1"
        Me.cmdAddOperatore.DrawText = True
        Me.cmdAddOperatore.Image = CType(resources.GetObject("cmdAddOperatore.Image"), System.Drawing.Image)
        Me.cmdAddOperatore.Name = "cmdAddOperatore"
        Me.cmdAddOperatore.Text = "Aggiungi Operatore"
        Me.cmdAddOperatore.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdAddOperatore.Visibility = Telerik.WinControls.ElementVisibility.Visible
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
        'cmdModificaOperatore
        '
        Me.cmdModificaOperatore.AccessibleDescription = "Modifica Operatore"
        Me.cmdModificaOperatore.AccessibleName = "Modifica Operatore"
        Me.cmdModificaOperatore.DisplayName = "CommandBarButton1"
        Me.cmdModificaOperatore.DrawText = True
        Me.cmdModificaOperatore.Image = CType(resources.GetObject("cmdModificaOperatore.Image"), System.Drawing.Image)
        Me.cmdModificaOperatore.Name = "cmdModificaOperatore"
        Me.cmdModificaOperatore.Text = "Modifica Operatore"
        Me.cmdModificaOperatore.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdModificaOperatore.Visibility = Telerik.WinControls.ElementVisibility.Visible
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
        'cmdCancellaOperatore
        '
        Me.cmdCancellaOperatore.AccessibleDescription = "Cancella Operatore"
        Me.cmdCancellaOperatore.AccessibleName = "Cancella Operatore"
        Me.cmdCancellaOperatore.DisplayName = "CommandBarButton1"
        Me.cmdCancellaOperatore.DrawText = True
        Me.cmdCancellaOperatore.Image = CType(resources.GetObject("cmdCancellaOperatore.Image"), System.Drawing.Image)
        Me.cmdCancellaOperatore.Name = "cmdCancellaOperatore"
        Me.cmdCancellaOperatore.Text = "Cancella Operatore"
        Me.cmdCancellaOperatore.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdCancellaOperatore.Visibility = Telerik.WinControls.ElementVisibility.Visible
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
        'StatusStrip1
        '
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 311)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(662, 22)
        Me.StatusStrip1.TabIndex = 1
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'CommandBarStripElement2
        '
        Me.CommandBarStripElement2.DisplayName = "CommandBarStripElement2"
        Me.CommandBarStripElement2.Name = "CommandBarStripElement2"
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
        'CommandBarSeparator4
        '
        Me.CommandBarSeparator4.AccessibleDescription = "CommandBarSeparator4"
        Me.CommandBarSeparator4.AccessibleName = "CommandBarSeparator4"
        Me.CommandBarSeparator4.DisplayName = "CommandBarSeparator4"
        Me.CommandBarSeparator4.Name = "CommandBarSeparator4"
        Me.CommandBarSeparator4.Text = ""
        Me.CommandBarSeparator4.Visibility = Telerik.WinControls.ElementVisibility.Visible
        Me.CommandBarSeparator4.VisibleInOverflowMenu = False
        '
        'frmOperatori
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(662, 333)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.RadCommandBar1)
        Me.Name = "frmOperatori"
        Me.Text = "Gestione Operatori"
        CType(Me.RadCommandBar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RadCommandBar1 As Telerik.WinControls.UI.RadCommandBar
    Friend WithEvents CommandBarRowElement1 As Telerik.WinControls.UI.CommandBarRowElement
    Friend WithEvents CommandBarStripElement1 As Telerik.WinControls.UI.CommandBarStripElement
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents cmdAddOperatore As Telerik.WinControls.UI.CommandBarButton
    Friend WithEvents CommandBarSeparator1 As Telerik.WinControls.UI.CommandBarSeparator
    Friend WithEvents cmdModificaOperatore As Telerik.WinControls.UI.CommandBarButton
    Friend WithEvents CommandBarSeparator2 As Telerik.WinControls.UI.CommandBarSeparator
    Friend WithEvents cmdCancellaOperatore As Telerik.WinControls.UI.CommandBarButton
    Friend WithEvents CommandBarSeparator3 As Telerik.WinControls.UI.CommandBarSeparator
    Friend WithEvents CommandBarStripElement2 As Telerik.WinControls.UI.CommandBarStripElement
    Friend WithEvents cmdChiudi As Telerik.WinControls.UI.CommandBarButton
    Friend WithEvents CommandBarSeparator4 As Telerik.WinControls.UI.CommandBarSeparator
End Class
