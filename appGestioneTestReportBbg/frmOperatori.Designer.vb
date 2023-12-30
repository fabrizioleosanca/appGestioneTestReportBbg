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
        Me.cmdChiudi = New Telerik.WinControls.UI.CommandBarButton()
        Me.CommandBarSeparator4 = New Telerik.WinControls.UI.CommandBarSeparator()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.CommandBarStripElement2 = New Telerik.WinControls.UI.CommandBarStripElement()
        Me.RGBAggiungiOperatore = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtNewCognomeOperatore = New Telerik.WinControls.UI.RadTextBox()
        Me.lblNuovoCognomeOperatore = New Telerik.WinControls.UI.RadLabel()
        Me.btnApriFileFirma = New Telerik.WinControls.UI.RadButton()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.lblFirmaOperatore = New Telerik.WinControls.UI.RadLabel()
        Me.txtNewNomeOperatore = New Telerik.WinControls.UI.RadTextBox()
        Me.lblNuovoNomeOperatore = New Telerik.WinControls.UI.RadLabel()
        Me.RGBCambiaNomeOperatore = New Telerik.WinControls.UI.RadGroupBox()
        Me.btnApriFirmaUpdate = New Telerik.WinControls.UI.RadButton()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.RadLabel1 = New Telerik.WinControls.UI.RadLabel()
        Me.txtUpdateNome = New Telerik.WinControls.UI.RadTextBox()
        Me.lblUpdateNome = New Telerik.WinControls.UI.RadLabel()
        Me.lblSelezionaOperatore = New Telerik.WinControls.UI.RadLabel()
        Me.cmbSelezionaOperatore = New System.Windows.Forms.ComboBox()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.rgbCancellaOperatore = New Telerik.WinControls.UI.RadGroupBox()
        Me.cmbCancellaOperatore = New System.Windows.Forms.ComboBox()
        Me.lblCancellaNomeOperatore = New Telerik.WinControls.UI.RadLabel()
        CType(Me.RadCommandBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RGBAggiungiOperatore, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RGBAggiungiOperatore.SuspendLayout()
        CType(Me.txtNewCognomeOperatore, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNuovoCognomeOperatore, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnApriFileFirma, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFirmaOperatore, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNewNomeOperatore, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNuovoNomeOperatore, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RGBCambiaNomeOperatore, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RGBCambiaNomeOperatore.SuspendLayout()
        CType(Me.btnApriFirmaUpdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUpdateNome, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUpdateNome, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSelezionaOperatore, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rgbCancellaOperatore, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rgbCancellaOperatore.SuspendLayout()
        CType(Me.lblCancellaNomeOperatore, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadCommandBar1
        '
        Me.RadCommandBar1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadCommandBar1.Location = New System.Drawing.Point(0, 0)
        Me.RadCommandBar1.Name = "RadCommandBar1"
        Me.RadCommandBar1.Rows.AddRange(New Telerik.WinControls.UI.CommandBarRowElement() {Me.CommandBarRowElement1})
        Me.RadCommandBar1.Size = New System.Drawing.Size(488, 56)
        Me.RadCommandBar1.TabIndex = 0
        '
        'CommandBarRowElement1
        '
        Me.CommandBarRowElement1.MinSize = New System.Drawing.Size(25, 25)
        Me.CommandBarRowElement1.Name = "CommandBarRowElement1"
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
        Me.cmdAddOperatore.DisplayName = "CommandBarButton1"
        Me.cmdAddOperatore.DrawText = True
        Me.cmdAddOperatore.Image = CType(resources.GetObject("cmdAddOperatore.Image"), System.Drawing.Image)
        Me.cmdAddOperatore.Name = "cmdAddOperatore"
        Me.cmdAddOperatore.Text = "Aggiungi Operatore"
        Me.cmdAddOperatore.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'CommandBarSeparator1
        '
        Me.CommandBarSeparator1.AccessibleDescription = "CommandBarSeparator1"
        Me.CommandBarSeparator1.AccessibleName = "CommandBarSeparator1"
        Me.CommandBarSeparator1.DisplayName = "CommandBarSeparator1"
        Me.CommandBarSeparator1.Name = "CommandBarSeparator1"
        Me.CommandBarSeparator1.VisibleInOverflowMenu = False
        '
        'cmdModificaOperatore
        '
        Me.cmdModificaOperatore.DisplayName = "CommandBarButton1"
        Me.cmdModificaOperatore.DrawText = True
        Me.cmdModificaOperatore.Image = CType(resources.GetObject("cmdModificaOperatore.Image"), System.Drawing.Image)
        Me.cmdModificaOperatore.Name = "cmdModificaOperatore"
        Me.cmdModificaOperatore.Text = "Modifica Operatore"
        Me.cmdModificaOperatore.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'CommandBarSeparator2
        '
        Me.CommandBarSeparator2.AccessibleDescription = "CommandBarSeparator2"
        Me.CommandBarSeparator2.AccessibleName = "CommandBarSeparator2"
        Me.CommandBarSeparator2.DisplayName = "CommandBarSeparator2"
        Me.CommandBarSeparator2.Name = "CommandBarSeparator2"
        Me.CommandBarSeparator2.VisibleInOverflowMenu = False
        '
        'cmdCancellaOperatore
        '
        Me.cmdCancellaOperatore.DisplayName = "CommandBarButton1"
        Me.cmdCancellaOperatore.DrawText = True
        Me.cmdCancellaOperatore.Image = CType(resources.GetObject("cmdCancellaOperatore.Image"), System.Drawing.Image)
        Me.cmdCancellaOperatore.Name = "cmdCancellaOperatore"
        Me.cmdCancellaOperatore.Text = "Cancella Operatore"
        Me.cmdCancellaOperatore.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'CommandBarSeparator3
        '
        Me.CommandBarSeparator3.AccessibleDescription = "CommandBarSeparator3"
        Me.CommandBarSeparator3.AccessibleName = "CommandBarSeparator3"
        Me.CommandBarSeparator3.DisplayName = "CommandBarSeparator3"
        Me.CommandBarSeparator3.Name = "CommandBarSeparator3"
        Me.CommandBarSeparator3.VisibleInOverflowMenu = False
        '
        'cmdChiudi
        '
        Me.cmdChiudi.DisplayName = "CommandBarButton1"
        Me.cmdChiudi.DrawText = True
        Me.cmdChiudi.Image = CType(resources.GetObject("cmdChiudi.Image"), System.Drawing.Image)
        Me.cmdChiudi.Name = "cmdChiudi"
        Me.cmdChiudi.Text = "Chiudi"
        Me.cmdChiudi.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'CommandBarSeparator4
        '
        Me.CommandBarSeparator4.AccessibleDescription = "CommandBarSeparator4"
        Me.CommandBarSeparator4.AccessibleName = "CommandBarSeparator4"
        Me.CommandBarSeparator4.DisplayName = "CommandBarSeparator4"
        Me.CommandBarSeparator4.Name = "CommandBarSeparator4"
        Me.CommandBarSeparator4.VisibleInOverflowMenu = False
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 535)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(488, 22)
        Me.StatusStrip1.TabIndex = 1
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'CommandBarStripElement2
        '
        Me.CommandBarStripElement2.DisplayName = "CommandBarStripElement2"
        Me.CommandBarStripElement2.Name = "CommandBarStripElement2"
        '
        'RGBAggiungiOperatore
        '
        Me.RGBAggiungiOperatore.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RGBAggiungiOperatore.Controls.Add(Me.txtNewCognomeOperatore)
        Me.RGBAggiungiOperatore.Controls.Add(Me.lblNuovoCognomeOperatore)
        Me.RGBAggiungiOperatore.Controls.Add(Me.btnApriFileFirma)
        Me.RGBAggiungiOperatore.Controls.Add(Me.PictureBox1)
        Me.RGBAggiungiOperatore.Controls.Add(Me.lblFirmaOperatore)
        Me.RGBAggiungiOperatore.Controls.Add(Me.txtNewNomeOperatore)
        Me.RGBAggiungiOperatore.Controls.Add(Me.lblNuovoNomeOperatore)
        Me.RGBAggiungiOperatore.HeaderText = "Aggiungi Operatore BBG"
        Me.RGBAggiungiOperatore.Location = New System.Drawing.Point(8, 70)
        Me.RGBAggiungiOperatore.Name = "RGBAggiungiOperatore"
        Me.RGBAggiungiOperatore.Size = New System.Drawing.Size(468, 194)
        Me.RGBAggiungiOperatore.TabIndex = 2
        Me.RGBAggiungiOperatore.Text = "Aggiungi Operatore BBG"
        '
        'txtNewCognomeOperatore
        '
        Me.txtNewCognomeOperatore.Location = New System.Drawing.Point(125, 62)
        Me.txtNewCognomeOperatore.Name = "txtNewCognomeOperatore"
        Me.txtNewCognomeOperatore.Size = New System.Drawing.Size(322, 20)
        Me.txtNewCognomeOperatore.TabIndex = 2
        '
        'lblNuovoCognomeOperatore
        '
        Me.lblNuovoCognomeOperatore.Location = New System.Drawing.Point(12, 63)
        Me.lblNuovoCognomeOperatore.Name = "lblNuovoCognomeOperatore"
        Me.lblNuovoCognomeOperatore.Size = New System.Drawing.Size(109, 18)
        Me.lblNuovoCognomeOperatore.TabIndex = 5
        Me.lblNuovoCognomeOperatore.Text = "Cognome Operatore"
        '
        'btnApriFileFirma
        '
        Me.btnApriFileFirma.Location = New System.Drawing.Point(375, 98)
        Me.btnApriFileFirma.Name = "btnApriFileFirma"
        Me.btnApriFileFirma.Size = New System.Drawing.Size(72, 68)
        Me.btnApriFileFirma.TabIndex = 3
        Me.btnApriFileFirma.Text = "Apri File Firma Operatore"
        Me.btnApriFileFirma.TextWrap = True
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.White
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox1.Location = New System.Drawing.Point(126, 98)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(243, 68)
        Me.PictureBox1.TabIndex = 3
        Me.PictureBox1.TabStop = False
        '
        'lblFirmaOperatore
        '
        Me.lblFirmaOperatore.AutoSize = False
        Me.lblFirmaOperatore.Location = New System.Drawing.Point(16, 120)
        Me.lblFirmaOperatore.Name = "lblFirmaOperatore"
        Me.lblFirmaOperatore.Size = New System.Drawing.Size(105, 18)
        Me.lblFirmaOperatore.TabIndex = 2
        Me.lblFirmaOperatore.Text = "Firma Operatore"
        Me.lblFirmaOperatore.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNewNomeOperatore
        '
        Me.txtNewNomeOperatore.Location = New System.Drawing.Point(125, 29)
        Me.txtNewNomeOperatore.Name = "txtNewNomeOperatore"
        Me.txtNewNomeOperatore.Size = New System.Drawing.Size(322, 20)
        Me.txtNewNomeOperatore.TabIndex = 1
        '
        'lblNuovoNomeOperatore
        '
        Me.lblNuovoNomeOperatore.AutoSize = False
        Me.lblNuovoNomeOperatore.Location = New System.Drawing.Point(21, 29)
        Me.lblNuovoNomeOperatore.Name = "lblNuovoNomeOperatore"
        Me.lblNuovoNomeOperatore.Size = New System.Drawing.Size(100, 18)
        Me.lblNuovoNomeOperatore.TabIndex = 0
        Me.lblNuovoNomeOperatore.Text = "Nome Operatore"
        Me.lblNuovoNomeOperatore.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RGBCambiaNomeOperatore
        '
        Me.RGBCambiaNomeOperatore.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RGBCambiaNomeOperatore.Controls.Add(Me.btnApriFirmaUpdate)
        Me.RGBCambiaNomeOperatore.Controls.Add(Me.PictureBox2)
        Me.RGBCambiaNomeOperatore.Controls.Add(Me.RadLabel1)
        Me.RGBCambiaNomeOperatore.Controls.Add(Me.txtUpdateNome)
        Me.RGBCambiaNomeOperatore.Controls.Add(Me.lblUpdateNome)
        Me.RGBCambiaNomeOperatore.Controls.Add(Me.lblSelezionaOperatore)
        Me.RGBCambiaNomeOperatore.Controls.Add(Me.cmbSelezionaOperatore)
        Me.RGBCambiaNomeOperatore.HeaderText = "Cambia Nome Operatore BBG"
        Me.RGBCambiaNomeOperatore.Location = New System.Drawing.Point(11, 279)
        Me.RGBCambiaNomeOperatore.Name = "RGBCambiaNomeOperatore"
        Me.RGBCambiaNomeOperatore.Size = New System.Drawing.Size(464, 171)
        Me.RGBCambiaNomeOperatore.TabIndex = 3
        Me.RGBCambiaNomeOperatore.Text = "Cambia Nome Operatore BBG"
        '
        'btnApriFirmaUpdate
        '
        Me.btnApriFirmaUpdate.Location = New System.Drawing.Point(372, 86)
        Me.btnApriFirmaUpdate.Name = "btnApriFirmaUpdate"
        Me.btnApriFirmaUpdate.Size = New System.Drawing.Size(72, 68)
        Me.btnApriFirmaUpdate.TabIndex = 6
        Me.btnApriFirmaUpdate.Text = "Apri File Firma Operatore"
        Me.btnApriFirmaUpdate.TextWrap = True
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.White
        Me.PictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox2.Location = New System.Drawing.Point(146, 86)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(220, 68)
        Me.PictureBox2.TabIndex = 6
        Me.PictureBox2.TabStop = False
        '
        'RadLabel1
        '
        Me.RadLabel1.AutoSize = False
        Me.RadLabel1.Location = New System.Drawing.Point(35, 112)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(105, 18)
        Me.RadLabel1.TabIndex = 5
        Me.RadLabel1.Text = "Firma Operatore"
        Me.RadLabel1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtUpdateNome
        '
        Me.txtUpdateNome.Location = New System.Drawing.Point(146, 56)
        Me.txtUpdateNome.Name = "txtUpdateNome"
        Me.txtUpdateNome.Size = New System.Drawing.Size(298, 20)
        Me.txtUpdateNome.TabIndex = 5
        '
        'lblUpdateNome
        '
        Me.lblUpdateNome.AutoSize = False
        Me.lblUpdateNome.Location = New System.Drawing.Point(10, 57)
        Me.lblUpdateNome.Name = "lblUpdateNome"
        Me.lblUpdateNome.Size = New System.Drawing.Size(130, 18)
        Me.lblUpdateNome.TabIndex = 2
        Me.lblUpdateNome.Text = "Nuovo Cognome Nome"
        Me.lblUpdateNome.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSelezionaOperatore
        '
        Me.lblSelezionaOperatore.AutoSize = False
        Me.lblSelezionaOperatore.Location = New System.Drawing.Point(23, 30)
        Me.lblSelezionaOperatore.Name = "lblSelezionaOperatore"
        Me.lblSelezionaOperatore.Size = New System.Drawing.Size(117, 18)
        Me.lblSelezionaOperatore.TabIndex = 1
        Me.lblSelezionaOperatore.Text = "Seleziona Operatore"
        Me.lblSelezionaOperatore.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbSelezionaOperatore
        '
        Me.cmbSelezionaOperatore.FormattingEnabled = True
        Me.cmbSelezionaOperatore.Location = New System.Drawing.Point(146, 27)
        Me.cmbSelezionaOperatore.Name = "cmbSelezionaOperatore"
        Me.cmbSelezionaOperatore.Size = New System.Drawing.Size(298, 21)
        Me.cmbSelezionaOperatore.TabIndex = 4
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "Seleziona Immagine Firma"
        '
        'rgbCancellaOperatore
        '
        Me.rgbCancellaOperatore.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rgbCancellaOperatore.Controls.Add(Me.cmbCancellaOperatore)
        Me.rgbCancellaOperatore.Controls.Add(Me.lblCancellaNomeOperatore)
        Me.rgbCancellaOperatore.HeaderText = "Cancella Operatore Dal Database"
        Me.rgbCancellaOperatore.Location = New System.Drawing.Point(11, 464)
        Me.rgbCancellaOperatore.Name = "rgbCancellaOperatore"
        Me.rgbCancellaOperatore.Size = New System.Drawing.Size(464, 70)
        Me.rgbCancellaOperatore.TabIndex = 4
        Me.rgbCancellaOperatore.Text = "Cancella Operatore Dal Database"
        '
        'cmbCancellaOperatore
        '
        Me.cmbCancellaOperatore.FormattingEnabled = True
        Me.cmbCancellaOperatore.Location = New System.Drawing.Point(122, 27)
        Me.cmbCancellaOperatore.Name = "cmbCancellaOperatore"
        Me.cmbCancellaOperatore.Size = New System.Drawing.Size(322, 21)
        Me.cmbCancellaOperatore.TabIndex = 7
        '
        'lblCancellaNomeOperatore
        '
        Me.lblCancellaNomeOperatore.AutoSize = False
        Me.lblCancellaNomeOperatore.Location = New System.Drawing.Point(10, 27)
        Me.lblCancellaNomeOperatore.Name = "lblCancellaNomeOperatore"
        Me.lblCancellaNomeOperatore.Size = New System.Drawing.Size(100, 18)
        Me.lblCancellaNomeOperatore.TabIndex = 0
        Me.lblCancellaNomeOperatore.Text = "Nome e Cognome"
        Me.lblCancellaNomeOperatore.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmOperatori
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(488, 557)
        Me.Controls.Add(Me.rgbCancellaOperatore)
        Me.Controls.Add(Me.RGBCambiaNomeOperatore)
        Me.Controls.Add(Me.RGBAggiungiOperatore)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.RadCommandBar1)
        Me.Name = "frmOperatori"
        Me.Text = "Gestione Operatori"
        CType(Me.RadCommandBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RGBAggiungiOperatore, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RGBAggiungiOperatore.ResumeLayout(False)
        Me.RGBAggiungiOperatore.PerformLayout()
        CType(Me.txtNewCognomeOperatore, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNuovoCognomeOperatore, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnApriFileFirma, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFirmaOperatore, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNewNomeOperatore, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNuovoNomeOperatore, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RGBCambiaNomeOperatore, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RGBCambiaNomeOperatore.ResumeLayout(False)
        Me.RGBCambiaNomeOperatore.PerformLayout()
        CType(Me.btnApriFirmaUpdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUpdateNome, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUpdateNome, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSelezionaOperatore, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rgbCancellaOperatore, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rgbCancellaOperatore.ResumeLayout(False)
        CType(Me.lblCancellaNomeOperatore, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents RGBAggiungiOperatore As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents lblNuovoNomeOperatore As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RGBCambiaNomeOperatore As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents lblFirmaOperatore As Telerik.WinControls.UI.RadLabel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents btnApriFileFirma As Telerik.WinControls.UI.RadButton
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents lblSelezionaOperatore As Telerik.WinControls.UI.RadLabel
    Friend WithEvents cmbSelezionaOperatore As ComboBox
    Friend WithEvents lblUpdateNome As Telerik.WinControls.UI.RadLabel
    Friend WithEvents txtUpdateNome As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents rgbCancellaOperatore As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents lblCancellaNomeOperatore As Telerik.WinControls.UI.RadLabel
    Friend WithEvents lblNuovoCognomeOperatore As Telerik.WinControls.UI.RadLabel
    Friend WithEvents txtNewCognomeOperatore As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents txtNewNomeOperatore As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents cmbCancellaOperatore As ComboBox
    Friend WithEvents btnApriFirmaUpdate As Telerik.WinControls.UI.RadButton
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents RadLabel1 As Telerik.WinControls.UI.RadLabel
End Class
