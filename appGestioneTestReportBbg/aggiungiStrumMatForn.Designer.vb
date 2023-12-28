<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class aggiungiStrumMatForn
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(aggiungiStrumMatForn))
        Me.RadPageViewAggiungi = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPageAggFornitori = New Telerik.WinControls.UI.RadPageViewPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnCancellaFornitore = New System.Windows.Forms.Button()
        Me.lblFornitoriRegistrati = New System.Windows.Forms.Label()
        Me.ComboBoxFornitori = New System.Windows.Forms.ComboBox()
        Me.btnSalvaFornitore = New System.Windows.Forms.Button()
        Me.DateTimePickerFornitori = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblNomeFornitore = New System.Windows.Forms.Label()
        Me.txtFornitore = New System.Windows.Forms.TextBox()
        Me.RadPageViewPageAggStrumenti = New Telerik.WinControls.UI.RadPageViewPage()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnCancellaStrumento = New System.Windows.Forms.Button()
        Me.lblStrumentiRegistrati = New System.Windows.Forms.Label()
        Me.ComboBoxStrumenti = New System.Windows.Forms.ComboBox()
        Me.btnSalvaStrumenti = New System.Windows.Forms.Button()
        Me.DateTimePickerStrumenti = New System.Windows.Forms.DateTimePicker()
        Me.lblDataInserimento = New System.Windows.Forms.Label()
        Me.lblNomeStrumento = New System.Windows.Forms.Label()
        Me.txtNomeStrumento = New System.Windows.Forms.TextBox()
        Me.RadPageViewPageAggMateriali = New Telerik.WinControls.UI.RadPageViewPage()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnCancellaMateriali = New System.Windows.Forms.Button()
        Me.lblMaterialiRegistrati = New System.Windows.Forms.Label()
        Me.ComboBoxMateriali = New System.Windows.Forms.ComboBox()
        Me.btnSalvaMateriali = New System.Windows.Forms.Button()
        Me.DateTimePickerMateriali = New System.Windows.Forms.DateTimePicker()
        Me.lblDataInserimentoMateriali = New System.Windows.Forms.Label()
        Me.lblMateriali = New System.Windows.Forms.Label()
        Me.txtNomeMateriale = New System.Windows.Forms.TextBox()
        CType(Me.RadPageViewAggiungi, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewAggiungi.SuspendLayout()
        Me.RadPageViewPageAggFornitori.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.RadPageViewPageAggStrumenti.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.RadPageViewPageAggMateriali.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'RadPageViewAggiungi
        '
        Me.RadPageViewAggiungi.Controls.Add(Me.RadPageViewPageAggFornitori)
        Me.RadPageViewAggiungi.Controls.Add(Me.RadPageViewPageAggStrumenti)
        Me.RadPageViewAggiungi.Controls.Add(Me.RadPageViewPageAggMateriali)
        Me.RadPageViewAggiungi.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageViewAggiungi.Location = New System.Drawing.Point(0, 0)
        Me.RadPageViewAggiungi.Name = "RadPageViewAggiungi"
        Me.RadPageViewAggiungi.SelectedPage = Me.RadPageViewPageAggFornitori
        Me.RadPageViewAggiungi.Size = New System.Drawing.Size(810, 338)
        Me.RadPageViewAggiungi.TabIndex = 0
        Me.RadPageViewAggiungi.Text = "Salva Materiali Strumenti e Fornitori in DB"
        CType(Me.RadPageViewAggiungi.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.[Auto]
        '
        'RadPageViewPageAggFornitori
        '
        Me.RadPageViewPageAggFornitori.Controls.Add(Me.GroupBox1)
        Me.RadPageViewPageAggFornitori.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPageAggFornitori.Name = "RadPageViewPageAggFornitori"
        Me.RadPageViewPageAggFornitori.Size = New System.Drawing.Size(789, 290)
        Me.RadPageViewPageAggFornitori.Text = "Aggiungi Fornitori"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnCancellaFornitore)
        Me.GroupBox1.Controls.Add(Me.lblFornitoriRegistrati)
        Me.GroupBox1.Controls.Add(Me.ComboBoxFornitori)
        Me.GroupBox1.Controls.Add(Me.btnSalvaFornitore)
        Me.GroupBox1.Controls.Add(Me.DateTimePickerFornitori)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.lblNomeFornitore)
        Me.GroupBox1.Controls.Add(Me.txtFornitore)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 16)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(783, 248)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "AGGIUNGERE FORNITORE"
        '
        'btnCancellaFornitore
        '
        Me.btnCancellaFornitore.Location = New System.Drawing.Point(542, 176)
        Me.btnCancellaFornitore.Name = "btnCancellaFornitore"
        Me.btnCancellaFornitore.Size = New System.Drawing.Size(220, 36)
        Me.btnCancellaFornitore.TabIndex = 7
        Me.btnCancellaFornitore.Text = "Cancella Fornitore "
        Me.btnCancellaFornitore.UseVisualStyleBackColor = True
        '
        'lblFornitoriRegistrati
        '
        Me.lblFornitoriRegistrati.AutoSize = True
        Me.lblFornitoriRegistrati.Location = New System.Drawing.Point(21, 48)
        Me.lblFornitoriRegistrati.Name = "lblFornitoriRegistrati"
        Me.lblFornitoriRegistrati.Size = New System.Drawing.Size(104, 13)
        Me.lblFornitoriRegistrati.TabIndex = 6
        Me.lblFornitoriRegistrati.Text = "Fornitori Registrati"
        '
        'ComboBoxFornitori
        '
        Me.ComboBoxFornitori.FormattingEnabled = True
        Me.ComboBoxFornitori.Location = New System.Drawing.Point(131, 45)
        Me.ComboBoxFornitori.Name = "ComboBoxFornitori"
        Me.ComboBoxFornitori.Size = New System.Drawing.Size(631, 21)
        Me.ComboBoxFornitori.TabIndex = 5
        '
        'btnSalvaFornitore
        '
        Me.btnSalvaFornitore.Location = New System.Drawing.Point(292, 176)
        Me.btnSalvaFornitore.Name = "btnSalvaFornitore"
        Me.btnSalvaFornitore.Size = New System.Drawing.Size(220, 36)
        Me.btnSalvaFornitore.TabIndex = 4
        Me.btnSalvaFornitore.Text = "Salva Fornitore In Database"
        Me.btnSalvaFornitore.UseVisualStyleBackColor = True
        '
        'DateTimePickerFornitori
        '
        Me.DateTimePickerFornitori.Location = New System.Drawing.Point(132, 116)
        Me.DateTimePickerFornitori.Name = "DateTimePickerFornitori"
        Me.DateTimePickerFornitori.Size = New System.Drawing.Size(380, 20)
        Me.DateTimePickerFornitori.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 122)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(109, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Data Di Inserimento"
        '
        'lblNomeFornitore
        '
        Me.lblNomeFornitore.AutoSize = True
        Me.lblNomeFornitore.Location = New System.Drawing.Point(37, 84)
        Me.lblNomeFornitore.Name = "lblNomeFornitore"
        Me.lblNomeFornitore.Size = New System.Drawing.Size(88, 13)
        Me.lblNomeFornitore.TabIndex = 1
        Me.lblNomeFornitore.Text = "Nome Fornitore"
        '
        'txtFornitore
        '
        Me.txtFornitore.Location = New System.Drawing.Point(132, 81)
        Me.txtFornitore.Name = "txtFornitore"
        Me.txtFornitore.Size = New System.Drawing.Size(633, 20)
        Me.txtFornitore.TabIndex = 0
        '
        'RadPageViewPageAggStrumenti
        '
        Me.RadPageViewPageAggStrumenti.Controls.Add(Me.GroupBox2)
        Me.RadPageViewPageAggStrumenti.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPageAggStrumenti.Name = "RadPageViewPageAggStrumenti"
        Me.RadPageViewPageAggStrumenti.Size = New System.Drawing.Size(789, 290)
        Me.RadPageViewPageAggStrumenti.Text = "Aggiungi Strumenti"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnCancellaStrumento)
        Me.GroupBox2.Controls.Add(Me.lblStrumentiRegistrati)
        Me.GroupBox2.Controls.Add(Me.ComboBoxStrumenti)
        Me.GroupBox2.Controls.Add(Me.btnSalvaStrumenti)
        Me.GroupBox2.Controls.Add(Me.DateTimePickerStrumenti)
        Me.GroupBox2.Controls.Add(Me.lblDataInserimento)
        Me.GroupBox2.Controls.Add(Me.lblNomeStrumento)
        Me.GroupBox2.Controls.Add(Me.txtNomeStrumento)
        Me.GroupBox2.Location = New System.Drawing.Point(3, 16)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(783, 253)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "AGGIUNGERE STRUMENTI"
        '
        'btnCancellaStrumento
        '
        Me.btnCancellaStrumento.Location = New System.Drawing.Point(542, 176)
        Me.btnCancellaStrumento.Name = "btnCancellaStrumento"
        Me.btnCancellaStrumento.Size = New System.Drawing.Size(220, 36)
        Me.btnCancellaStrumento.TabIndex = 9
        Me.btnCancellaStrumento.Text = "Cancella Strumento"
        Me.btnCancellaStrumento.UseVisualStyleBackColor = True
        '
        'lblStrumentiRegistrati
        '
        Me.lblStrumentiRegistrati.AutoSize = True
        Me.lblStrumentiRegistrati.Location = New System.Drawing.Point(21, 48)
        Me.lblStrumentiRegistrati.Name = "lblStrumentiRegistrati"
        Me.lblStrumentiRegistrati.Size = New System.Drawing.Size(109, 13)
        Me.lblStrumentiRegistrati.TabIndex = 8
        Me.lblStrumentiRegistrati.Text = "Strumenti Registrati"
        '
        'ComboBoxStrumenti
        '
        Me.ComboBoxStrumenti.FormattingEnabled = True
        Me.ComboBoxStrumenti.Location = New System.Drawing.Point(135, 41)
        Me.ComboBoxStrumenti.Name = "ComboBoxStrumenti"
        Me.ComboBoxStrumenti.Size = New System.Drawing.Size(631, 21)
        Me.ComboBoxStrumenti.TabIndex = 7
        '
        'btnSalvaStrumenti
        '
        Me.btnSalvaStrumenti.Location = New System.Drawing.Point(292, 176)
        Me.btnSalvaStrumenti.Name = "btnSalvaStrumenti"
        Me.btnSalvaStrumenti.Size = New System.Drawing.Size(220, 36)
        Me.btnSalvaStrumenti.TabIndex = 4
        Me.btnSalvaStrumenti.Text = "Salva Strumento In Database"
        Me.btnSalvaStrumenti.UseVisualStyleBackColor = True
        '
        'DateTimePickerStrumenti
        '
        Me.DateTimePickerStrumenti.Location = New System.Drawing.Point(135, 112)
        Me.DateTimePickerStrumenti.Name = "DateTimePickerStrumenti"
        Me.DateTimePickerStrumenti.Size = New System.Drawing.Size(380, 20)
        Me.DateTimePickerStrumenti.TabIndex = 3
        '
        'lblDataInserimento
        '
        Me.lblDataInserimento.AutoSize = True
        Me.lblDataInserimento.Location = New System.Drawing.Point(21, 118)
        Me.lblDataInserimento.Name = "lblDataInserimento"
        Me.lblDataInserimento.Size = New System.Drawing.Size(109, 13)
        Me.lblDataInserimento.TabIndex = 2
        Me.lblDataInserimento.Text = "Data Di Inserimento"
        '
        'lblNomeStrumento
        '
        Me.lblNomeStrumento.AutoSize = True
        Me.lblNomeStrumento.Location = New System.Drawing.Point(36, 83)
        Me.lblNomeStrumento.Name = "lblNomeStrumento"
        Me.lblNomeStrumento.Size = New System.Drawing.Size(94, 13)
        Me.lblNomeStrumento.TabIndex = 1
        Me.lblNomeStrumento.Text = "Nome Strumento"
        '
        'txtNomeStrumento
        '
        Me.txtNomeStrumento.Location = New System.Drawing.Point(135, 77)
        Me.txtNomeStrumento.Name = "txtNomeStrumento"
        Me.txtNomeStrumento.Size = New System.Drawing.Size(633, 20)
        Me.txtNomeStrumento.TabIndex = 0
        '
        'RadPageViewPageAggMateriali
        '
        Me.RadPageViewPageAggMateriali.Controls.Add(Me.GroupBox3)
        Me.RadPageViewPageAggMateriali.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPageAggMateriali.Name = "RadPageViewPageAggMateriali"
        Me.RadPageViewPageAggMateriali.Size = New System.Drawing.Size(789, 290)
        Me.RadPageViewPageAggMateriali.Text = "Aggiungi Materiali"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnCancellaMateriali)
        Me.GroupBox3.Controls.Add(Me.lblMaterialiRegistrati)
        Me.GroupBox3.Controls.Add(Me.ComboBoxMateriali)
        Me.GroupBox3.Controls.Add(Me.btnSalvaMateriali)
        Me.GroupBox3.Controls.Add(Me.DateTimePickerMateriali)
        Me.GroupBox3.Controls.Add(Me.lblDataInserimentoMateriali)
        Me.GroupBox3.Controls.Add(Me.lblMateriali)
        Me.GroupBox3.Controls.Add(Me.txtNomeMateriale)
        Me.GroupBox3.Location = New System.Drawing.Point(3, 16)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(783, 252)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "AGGIUNGERE MATERIALI"
        '
        'btnCancellaMateriali
        '
        Me.btnCancellaMateriali.Location = New System.Drawing.Point(542, 185)
        Me.btnCancellaMateriali.Name = "btnCancellaMateriali"
        Me.btnCancellaMateriali.Size = New System.Drawing.Size(220, 36)
        Me.btnCancellaMateriali.TabIndex = 11
        Me.btnCancellaMateriali.Text = "Cancella Materiali"
        Me.btnCancellaMateriali.UseVisualStyleBackColor = True
        '
        'lblMaterialiRegistrati
        '
        Me.lblMaterialiRegistrati.AutoSize = True
        Me.lblMaterialiRegistrati.Location = New System.Drawing.Point(16, 48)
        Me.lblMaterialiRegistrati.Name = "lblMaterialiRegistrati"
        Me.lblMaterialiRegistrati.Size = New System.Drawing.Size(104, 13)
        Me.lblMaterialiRegistrati.TabIndex = 10
        Me.lblMaterialiRegistrati.Text = "Materiali Registrati"
        '
        'ComboBoxMateriali
        '
        Me.ComboBoxMateriali.FormattingEnabled = True
        Me.ComboBoxMateriali.Location = New System.Drawing.Point(131, 45)
        Me.ComboBoxMateriali.Name = "ComboBoxMateriali"
        Me.ComboBoxMateriali.Size = New System.Drawing.Size(631, 21)
        Me.ComboBoxMateriali.TabIndex = 9
        '
        'btnSalvaMateriali
        '
        Me.btnSalvaMateriali.Location = New System.Drawing.Point(292, 185)
        Me.btnSalvaMateriali.Name = "btnSalvaMateriali"
        Me.btnSalvaMateriali.Size = New System.Drawing.Size(220, 36)
        Me.btnSalvaMateriali.TabIndex = 4
        Me.btnSalvaMateriali.Text = "Salva Materiale In Database"
        Me.btnSalvaMateriali.UseVisualStyleBackColor = True
        '
        'DateTimePickerMateriali
        '
        Me.DateTimePickerMateriali.Location = New System.Drawing.Point(132, 124)
        Me.DateTimePickerMateriali.Name = "DateTimePickerMateriali"
        Me.DateTimePickerMateriali.Size = New System.Drawing.Size(380, 20)
        Me.DateTimePickerMateriali.TabIndex = 3
        '
        'lblDataInserimentoMateriali
        '
        Me.lblDataInserimentoMateriali.AutoSize = True
        Me.lblDataInserimentoMateriali.Location = New System.Drawing.Point(11, 124)
        Me.lblDataInserimentoMateriali.Name = "lblDataInserimentoMateriali"
        Me.lblDataInserimentoMateriali.Size = New System.Drawing.Size(109, 13)
        Me.lblDataInserimentoMateriali.TabIndex = 2
        Me.lblDataInserimentoMateriali.Text = "Data Di Inserimento"
        '
        'lblMateriali
        '
        Me.lblMateriali.AutoSize = True
        Me.lblMateriali.Location = New System.Drawing.Point(32, 86)
        Me.lblMateriali.Name = "lblMateriali"
        Me.lblMateriali.Size = New System.Drawing.Size(88, 13)
        Me.lblMateriali.TabIndex = 1
        Me.lblMateriali.Text = "Nome Materiale"
        '
        'txtNomeMateriale
        '
        Me.txtNomeMateriale.Location = New System.Drawing.Point(132, 85)
        Me.txtNomeMateriale.Name = "txtNomeMateriale"
        Me.txtNomeMateriale.Size = New System.Drawing.Size(633, 20)
        Me.txtNomeMateriale.TabIndex = 0
        '
        'aggiungiStrumMatForn
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(810, 338)
        Me.Controls.Add(Me.RadPageViewAggiungi)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "aggiungiStrumMatForn"
        Me.Text = "Aggiungi Materiali Strumenti e Fornitori "
        CType(Me.RadPageViewAggiungi, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewAggiungi.ResumeLayout(False)
        Me.RadPageViewPageAggFornitori.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.RadPageViewPageAggStrumenti.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.RadPageViewPageAggMateriali.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadPageViewAggiungi As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPageAggFornitori As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPageAggStrumenti As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPageAggMateriali As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblNomeFornitore As System.Windows.Forms.Label
    Friend WithEvents txtFornitore As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerFornitori As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnSalvaFornitore As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSalvaStrumenti As System.Windows.Forms.Button
    Friend WithEvents DateTimePickerStrumenti As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDataInserimento As System.Windows.Forms.Label
    Friend WithEvents lblNomeStrumento As System.Windows.Forms.Label
    Friend WithEvents txtNomeStrumento As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSalvaMateriali As System.Windows.Forms.Button
    Friend WithEvents DateTimePickerMateriali As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDataInserimentoMateriali As System.Windows.Forms.Label
    Friend WithEvents lblMateriali As System.Windows.Forms.Label
    Friend WithEvents txtNomeMateriale As System.Windows.Forms.TextBox
    Friend WithEvents lblFornitoriRegistrati As System.Windows.Forms.Label
    Friend WithEvents ComboBoxFornitori As System.Windows.Forms.ComboBox
    Friend WithEvents lblStrumentiRegistrati As System.Windows.Forms.Label
    Friend WithEvents ComboBoxStrumenti As System.Windows.Forms.ComboBox
    Friend WithEvents lblMaterialiRegistrati As System.Windows.Forms.Label
    Friend WithEvents ComboBoxMateriali As System.Windows.Forms.ComboBox
    Friend WithEvents btnCancellaFornitore As System.Windows.Forms.Button
    Friend WithEvents btnCancellaStrumento As System.Windows.Forms.Button
    Friend WithEvents btnCancellaMateriali As System.Windows.Forms.Button
End Class
