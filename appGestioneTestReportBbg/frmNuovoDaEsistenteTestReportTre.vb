Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.IO
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data


Public Class frmNuovoDaEsistenteTestReportTre

#Region "Proprieta"
    Public Shared factory As New DatabaseProviderFactory()
    Public Property _db As Database = factory.Create("dbConnStrRete")
    Public Property dsIntestazione As dbGestTestReportDataSet
    Public Property idIntestazioneOnLoad As String
    Public Property TestiRiga As List(Of String)
    Public Property rowIndex As Integer = 0
#End Region

#Region "Riempi Combo Lotto apertura form"

    Public Sub New()

        ' Chiamata richiesta dalla finestra di progettazione.
        InitializeComponent()

        cmbLottoNum.AutoSizeDropDownToBestFit = True
        Dim multiColumnComboElement As RadMultiColumnComboBoxElement = cmbLottoNum.MultiColumnComboBoxElement
        multiColumnComboElement.DropDownSizingMode = SizingMode.UpDownAndRightBottom
        multiColumnComboElement.DropDownMinSize = New Size(550, 420)
        multiColumnComboElement.EditorControl.MasterTemplate.AutoGenerateColumns = False


        cmbLottoNum.AutoFilter = True
        cmbLottoNum.DisplayMember = "txtCompleto"
        cmbLottoNum.ValueMember = "txtCompleto"
        Dim filter As New FilterDescriptor()
        filter.PropertyName = cmbLottoNum.DisplayMember
        filter.Operator = FilterOperator.Contains
        cmbLottoNum.EditorControl.MasterTemplate.FilterDescriptors.Add(filter)

        cmbLottoNum.DropDownStyle = RadDropDownStyle.DropDown

        Dim gridViewControl As RadGridView = cmbLottoNum.EditorControl

        gridViewControl.EnableFiltering = True
        gridViewControl.EnableAlternatingRowColor = True
        gridViewControl.ShowFilteringRow = True
        gridViewControl.ReadOnly = True
        gridViewControl.SelectionMode = GridViewSelectionMode.FullRowSelect
        gridViewControl.UseCompatibleTextRendering = True
        gridViewControl.Focusable = True


        gridViewControl.MasterTemplate.AutoGenerateColumns = False

        gridViewControl.Columns.Add(New GridViewTextBoxColumn("materiale"))
        gridViewControl.Columns("materiale").HeaderText = "Materiale"
        gridViewControl.Columns.Add(New GridViewTextBoxColumn("numeroLotto"))
        gridViewControl.Columns("numeroLotto").HeaderText = "Numero Lotto"
        gridViewControl.Columns.Add(New GridViewTextBoxColumn("fornitore"))
        gridViewControl.Columns("fornitore").HeaderText = "Fornitore"
        gridViewControl.Columns.Add(New GridViewTextBoxColumn("numDDT"))
        gridViewControl.Columns("numDDT").HeaderText = "Numero DDT"
        gridViewControl.Columns.Add(New GridViewTextBoxColumn("txtCompleto"))
        gridViewControl.Columns("txtCompleto").IsVisible = False

        For Each column As GridViewDataColumn In cmbLottoNum.MultiColumnComboBoxElement.Columns
            column.BestFit()
        Next column

        AddHandler cmbLottoNum.DropDownClosing, AddressOf DropDownClosing

    End Sub

    Private Sub DropDownClosing(sender As Object, args As RadPopupClosingEventArgs)
        If TypeOf sender Is RadMultiColumnComboBox Then
            Dim rmc As RadMultiColumnComboBox = sender
            If rmc.SelectedIndex > -1 Then
                'se la messa a fuoco è su una riga, lascia il popup chiuso
                'Console.WriteLine("clicked on: " & CStr(rmc.SelectedIndex))
            Else
                'altrimenti, controllare la posizione del mouse e non consentire la chiusura se all'interno dell'area della finestra popup
                'Varibile con valori dei punti che delimitano il menu a tendina (Pop Up)
                Dim pt As Point = rmc.EditorControl.TableElement.PointFromControl(MousePosition)
                Dim popTop As Integer = rmc.MultiColumnComboBoxElement.MultiColumnPopupForm.Top
                Dim popLft As Integer = rmc.MultiColumnComboBoxElement.MultiColumnPopupForm.Left
                Dim popHt As Integer = rmc.MultiColumnComboBoxElement.MultiColumnPopupForm.Height
                Dim popWd As Integer = rmc.MultiColumnComboBoxElement.MultiColumnPopupForm.Width
                'Se
                If pt.X >= popLft And pt.X <= popLft + popWd Then
                    If pt.Y >= popTop And pt.Y <= popTop + popHt Then
                        '--- dovrebbe fare clic all'interno della finestra, quindi lasciarlo aperto
                        args.Cancel = True
                        If rmc.EditorControl.ActiveEditor IsNot Nothing Then
                            Dim editor As RadTextBoxEditor = TryCast(rmc.EditorControl.ActiveEditor, RadTextBoxEditor)
                            If editor IsNot Nothing Then
                                Dim editorElement As RadTextBoxEditorElement = TryCast(editor.EditorElement, RadTextBoxEditorElement)
                                editorElement.Focus()
                            End If

                        End If
                    End If
                End If
            End If
        End If
    End Sub


    Private Sub OnTextBoxItem_Click(sender As Object, e As EventArgs)
        cmbLottoNum.MultiColumnComboBoxElement.ShowPopup()
    End Sub


    Private Sub btnChiudiAggiornaReport_Click(sender As Object, e As EventArgs) Handles btnChiudiAggiornaReport.Click
        boolReturnMain = True
        Close()
    End Sub

    Private Sub cmbFornitore_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFornitore.SelectedIndexChanged
        Dim str As String
        Dim strArr() As String
        Dim count As Integer
        Dim Fornitore As String
        Dim selFornitore As String = CType(cmbFornitore.SelectedItem, String)
        RiempiComboLottoByFornitore(selFornitore)
    End Sub

    'Public Sub RiempiComboLottoByFornitore(ByVal strFornitore As String)
    '    Dim cmd As DbCommand
    '    Me.cmbLottoNum.Items.Clear()
    '    Dim strSQL As String = "spGetLottoByFornitore"
    '    cmd = _db.GetStoredProcCommand(strSQL)
    '    _db.AddInParameter(cmd, "fornitore", DbType.String, strFornitore)
    '    Using datareader As IDataReader = _db.ExecuteReader(cmd)
    '        While datareader.Read
    '            Me.cmbLottoNum.Items.Add(datareader("materiale") + " -- " + datareader("numeroLotto") + " -- " + datareader("numDDT") + " -- " + datareader("fornitore"))
    '        End While
    '    End Using
    'End Sub

    Public Sub clearItemMultiColumnComboBox()
        Dim rows As New List(Of GridViewRowInfo)(cmbLottoNum.EditorControl.Rows)

        For Each rowInfo As GridViewRowInfo In rows
            rowInfo.Delete()
        Next
    End Sub


    'Public Sub RiempiComboLottoByFornitore(ByVal strFornitore As String)

    '    Dim gridViewControl As RadGridView = Me.cmbLottoNum.EditorControl
    '    Dim cmd As DbCommand
    '    ' clearItemMultiColumnComboBox()
    '    Dim tb As New DataTable
    '    Dim strSQL As String = "spColonnaConcatenataLotto"
    '    cmd = _db.GetStoredProcCommand(strSQL)
    '    _db.AddInParameter(cmd, "fornitore", DbType.String, strFornitore)

    '    Using datareader As IDataReader = _db.ExecuteReader(cmd)
    '        tb.Load(datareader)
    '        Me.cmbLottoNum.DataSource = tb
    '        'Aggiunge una riga vuota per avere il campo di testo vuoto prima della selezione
    '        gridViewControl.Rows.AddNew()
    '    End Using

    '    ' cmbLottoNum.SelectedIndex = -1

    'End Sub

    Public Sub RiempiComboLottoByFornitore(strFornitore As String)

        Dim cmd As DbCommand
        Dim tb As New DataTable
        Dim strSQL As String = "spColonnaConcatenataLotto"
        cmd = _db.GetStoredProcCommand(strSQL)
        _db.AddInParameter(cmd, "fornitore", DbType.String, strFornitore)

        Using datareader As IDataReader = _db.ExecuteReader(cmd)
            tb.Load(datareader)
            cmbLottoNum.DataSource = tb
        End Using

        cmbLottoNum.EditorControl.CurrentRow = Nothing

    End Sub


    Public Sub RiempiComboMateriali()
        Dim cmdMat As DbCommand
        Dim strSQLMat As String = "spSelezionaMaterialiTutto"
        cmdMat = _db.GetStoredProcCommand(strSQLMat)
        Using datareader As IDataReader = _db.ExecuteReader(cmdMat)
            While datareader.Read
                cmbMateriale.Items.Add(datareader("Materiale"))
            End While
        End Using
    End Sub

    Public Sub RiempiComboStrumenti()
        Dim cmdStrum As DbCommand
        Dim strSQLStrum As String = "spSelezionaStrumentiTutto"
        cmdStrum = _db.GetStoredProcCommand(strSQLStrum)
        Using datareader As IDataReader = _db.ExecuteReader(cmdStrum)
            While datareader.Read
                cmbStrumento.Items.Add(datareader("Strumenti"))
            End While
        End Using
    End Sub

    Public Sub RiempiComboFornitori()
        Dim cmdForn As DbCommand
        Dim strSQLForn As String = "spSelezionaFornitoriTutto"
        cmdForn = _db.GetStoredProcCommand(strSQLForn)
        Using datareader As IDataReader = _db.ExecuteReader(cmdForn)
            While datareader.Read
                cmbFornitore.Items.Add(datareader("NomeFornitore"))
            End While
        End Using
    End Sub


    Public Sub RiempiComboMacchine()
        Dim fileName As String = Application.StartupPath & "\fileMacchine.txt"
        Try
            If IO.File.Exists(fileName) Then
                cmbMacchNum.Items.AddRange(IO.File.ReadAllLines(fileName))
            Else
                MessageBox.Show("Errore lettura file macchine")
            End If
        Catch ex As Exception
            MessageBox.Show("Errore scrittura file : " & ex.Message)
        End Try
    End Sub


#End Region

#Region "Apertura e Salvataggio"

    Private Sub frmNuovoDaEsistenteTestReport_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim ret As Integer?
        Dim result As DialogResult

        If propIdIntestazione = 0 Then
            result = MessageBox.Show("Prima devi selezionare un articolo!", "Errore selezione articolo", MessageBoxButtons.OK)
            If result = Windows.Forms.DialogResult.OK Then
                Close()
            End If
        End If

        Try

            'Vanno popolati per primi per la selezione del lotto
            RiempiComboMateriali()
            RiempiComboFornitori()

            getTestataByIdIntestazioneNew(propIdIntestazione)
            getPrimoPezzoByIdIntestazioneNew(propIdIntestazione)
            getSecondoPezzoByIdIntestazioneNew(propIdIntestazione)
            getTerzoPezzoByIdIntestazioneNew(propIdIntestazione)
            getQuartoPezzoByIdIntestazioneNew(propIdIntestazione)
            getQuintoPezzoByIdIntestazioneNew(propIdIntestazione)
            getUltimoPezzoByIdIntestazioneNew(propIdIntestazione)

            idIntestazioneOnLoad = txtIdIntestazione.Text

            'Copia incolla riga

            For Each pUno As Panel In TableLayoutPanelPrimoPz.Controls().OfType(Of Panel)()
                AddHandler pUno.Enter, AddressOf LeggiPosizioneRigaTLPUno

                For Each tUno As TextBox In pUno.Controls.OfType(Of TextBox)()
                    AddHandler tUno.KeyUp, AddressOf CopiaRiga
                Next
            Next

            'For Each t As TextBox In TableLayoutPanelPrimoPz.Controls().OfType(Of TextBox)()
            '    AddHandler t.Enter, AddressOf LeggiPosizioneRigaTLPUno
            '    AddHandler t.KeyUp, AddressOf CopiaRiga
            'Next

            For Each pUlti As Panel In TableLayoutPanelUltimoPz.Controls().OfType(Of Panel)()
                AddHandler pUlti.Enter, AddressOf LeggiPosizioneRigaTLPUlti

                For Each tUlti As TextBox In pUlti.Controls.OfType(Of TextBox)()
                    AddHandler tUlti.KeyUp, AddressOf CopiaRiga
                Next
            Next

            For Each pDue As Panel In TableLayoutPanelPezzoDue.Controls().OfType(Of Panel)()
                AddHandler pDue.Enter, AddressOf LeggiPosizioneRigaTLPDue

                For Each tDue As TextBox In pDue.Controls.OfType(Of TextBox)()
                    AddHandler tDue.KeyUp, AddressOf CopiaRiga
                Next
            Next

            For Each pTre As Panel In TableLayoutPanelPezzo3.Controls().OfType(Of Panel)()
                AddHandler pTre.Enter, AddressOf LeggiPosizioneRigaTLPTre

                For Each tTre As TextBox In pTre.Controls.OfType(Of TextBox)()
                    AddHandler tTre.KeyUp, AddressOf CopiaRiga
                Next
            Next

            For Each pQuattro As Panel In TableLayoutPanelPezzo4.Controls().OfType(Of Panel)()
                AddHandler pQuattro.Enter, AddressOf LeggiPosizioneRigaTLPQuattro

                For Each tQuattro As TextBox In pQuattro.Controls.OfType(Of TextBox)()
                    AddHandler tQuattro.KeyUp, AddressOf CopiaRiga
                Next
            Next

            For Each pCinque As Panel In TableLayoutPanel5.Controls().OfType(Of Panel)()
                AddHandler pCinque.Enter, AddressOf LeggiPosizioneRigaTLPCinque

                For Each tCinque As TextBox In pCinque.Controls.OfType(Of TextBox)()
                    AddHandler tCinque.KeyUp, AddressOf CopiaRiga
                Next
            Next


            RiempiComboStrumenti()
            RiempiComboLottoByFornitore(propFornitore)
            RiempiComboMacchine()

            'Viene riassegnato il numero lotto perche quando popola il combobox del lotto perde l'impostazione del testo 
            cmbLottoNum.Text = propNumLottoNew


            'HELP

            HelpProvider2 = New System.Windows.Forms.HelpProvider()
            HelpProvider2.HelpNamespace = "mspaint.chm"
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            HelpButton = True
            MaximizeBox = False
            MinimizeBox = False

            HelpProvider2.SetHelpString(txtIdIntestazione, "Indica il numero ID corrente , poi cliccare sul bottone per aggiornamento ID e il campo di testo indica il nuovo ID.")
            HelpProvider2.SetShowHelp(txtIdIntestazione, True)

            HelpProvider2.SetHelpString(txtValPrev1PrimoPz, "Per fare il segno del Diametro Ø alt + 0216")
            HelpProvider2.SetShowHelp(txtValPrev1PrimoPz, True)

            HelpProvider2.SetHelpString(txt1ValMisPrimoPz, "Per fare copia incolla posizionare il cursore sul primo campo di testo valore previsto e click su ctrl + C poi sul primo campo di testo valore Misurato e click su ctrl + V ")
            HelpProvider2.SetShowHelp(txt1ValMisPrimoPz, True)

            HelpProvider2.SetHelpString(txtNumOrdine, "Campo di testo per Numero Ordine")
            HelpProvider2.SetShowHelp(txtNumOrdine, True)

            HelpProvider2.SetHelpString(txtCodArt, "Campo di testo per Codice Articolo")
            HelpProvider2.SetShowHelp(txtCodArt, True)

            HelpProvider2.SetHelpString(cmbMateriale, "Campo di testo per scelta Materiale")
            HelpProvider2.SetShowHelp(cmbMateriale, True)

            HelpProvider2.SetHelpString(cmbStrumento, "Campo di testo per Selezionare Scelta del Calibro dell'Operatore")
            HelpProvider2.SetShowHelp(cmbStrumento, True)

            HelpProvider2.SetHelpString(chkPrimoPezzo, "Checkbox per Selezione Primo Pezzo Controllato")
            HelpProvider2.SetShowHelp(chkPrimoPezzo, True)

            HelpProvider2.SetHelpString(chkUltimoPezzo, "Checkbox per Selezione Ultimo Pezzo Controllato")
            HelpProvider2.SetShowHelp(chkUltimoPezzo, True)

            HelpProvider2.SetHelpString(txtPezziControllati, "Campo di testo per Numero Pezzi Controllati")
            HelpProvider2.SetShowHelp(txtPezziControllati, True)

            HelpProvider2.SetHelpString(cmbMacchNum, "Campo di testo per Numero Macchina")
            HelpProvider2.SetShowHelp(cmbMacchNum, True)

            HelpProvider2.SetHelpString(txtRigaNum, "Campo di testo per Numero Riga")
            HelpProvider2.SetShowHelp(txtRigaNum, True)

            HelpProvider2.SetHelpString(txtPezziNum, "Campo di testo per Quantita Pezzi Ordine")
            HelpProvider2.SetShowHelp(txtPezziControllati, True)

            HelpProvider2.SetHelpString(cmbFornitore, "Campo di testo per Scelta Fornitore")
            HelpProvider2.SetShowHelp(cmbFornitore, True)

            HelpProvider2.SetHelpString(cmbLottoNum, "Campo di testo per Selezione Lotto Materiale , viene filtrato dopo la selezione del fornitore")
            HelpProvider2.SetShowHelp(cmbLottoNum, True)

            HelpProvider2.SetHelpString(cmbOperatore, "Campo di testo per Scelta Operatore")
            HelpProvider2.SetShowHelp(cmbOperatore, True)

        Catch ex As Exception
            MessageBox.Show("Errore frmNuovoDaEsistenteTestReport_Load : " & ex.Message)
        End Try

    End Sub

    Public Function ConfrontaPosizioneRiga(sender As Panel, pan As TableLayoutPanel)
        Return pan.GetRow(sender)
    End Function

    Public Sub LeggiPosizioneRigaTLPUno(sender As Object, e As EventArgs)
        rowIndex = TableLayoutPanelPrimoPz.GetRow(CType(sender, Panel))
    End Sub

    Public Sub LeggiPosizioneRigaTLPUlti(sender As Object, e As EventArgs)
        rowIndex = TableLayoutPanelUltimoPz.GetRow(CType(sender, Panel))
    End Sub

    Public Sub LeggiPosizioneRigaTLPDue(sender As Object, e As EventArgs)
        rowIndex = TableLayoutPanelPezzoDue.GetRow(CType(sender, Panel))
    End Sub

    Public Sub LeggiPosizioneRigaTLPTre(sender As Object, e As EventArgs)
        rowIndex = TableLayoutPanelPezzo3.GetRow(CType(sender, Panel))
    End Sub

    Public Sub LeggiPosizioneRigaTLPQuattro(sender As Object, e As EventArgs)
        rowIndex = TableLayoutPanelPezzo4.GetRow(CType(sender, Panel))
    End Sub

    Public Sub LeggiPosizioneRigaTLPCinque(sender As Object, e As EventArgs)
        rowIndex = TableLayoutPanel5.GetRow(CType(sender, Panel))
    End Sub


    Public Function nomeTLP() As TableLayoutPanel
        Dim genTLP As New TableLayoutPanel

        Try
            If txtValPrev1PrimoPz.Focused Then
                genTLP = CType(TableLayoutPanelPrimoPz, TableLayoutPanel)
                Return genTLP
            End If

            If txt1ValMisPrimoPz.Focused Then
                genTLP = CType(TableLayoutPanelPrimoPz, TableLayoutPanel)
                Return genTLP
            End If

            If txtValPrev1UltimoPz.Focused Then
                genTLP = CType(TableLayoutPanelUltimoPz, TableLayoutPanel)
                Return genTLP
            End If

            If txt1ValMisUltimoPz.Focused Then
                genTLP = CType(TableLayoutPanelUltimoPz, TableLayoutPanel)
                Return genTLP
            End If

            If txtValPrev1Pz2.Focused Then
                genTLP = CType(TableLayoutPanelPezzoDue, TableLayoutPanel)
                Return genTLP
            End If

            If txt1ValMisPz2.Focused Then
                genTLP = CType(TableLayoutPanelPezzoDue, TableLayoutPanel)
                Return genTLP
            End If

            If txtValPrev1Pz3.Focused Then
                genTLP = CType(TableLayoutPanelPezzo3, TableLayoutPanel)
                Return genTLP
            End If

            If txt1ValMisPz3.Focused Then
                genTLP = CType(TableLayoutPanelPezzo3, TableLayoutPanel)
                Return genTLP
            End If

            If txtValPrev1Pz4.Focused Then
                genTLP = CType(TableLayoutPanelPezzo4, TableLayoutPanel)
                Return genTLP
            End If

            If txt1ValMisPz4.Focused Then
                genTLP = CType(TableLayoutPanelPezzo4, TableLayoutPanel)
                Return genTLP
            End If

            If txtValPrev1Pz5.Focused Then
                genTLP = CType(TableLayoutPanel5, TableLayoutPanel)
                Return genTLP
            End If

            If txt1ValMisPz5.Focused Then
                genTLP = CType(TableLayoutPanel5, TableLayoutPanel)
                Return genTLP
            End If

        Catch ex As Exception
            MessageBox.Show("Errore:Funzione Restituisce TableLayoutPanel per copia incolla" & ex.Message)
        End Try

    End Function

    Public Sub CopiaRiga(sender As Object, e As KeyEventArgs)
        Dim generalCopiaTLP As New TableLayoutPanel
        Dim generalIncollaTLP As New TableLayoutPanel

        Try
            'Controlla se è stato premuto CTRL+C per la copia
            If e.Control Then
                If e.KeyCode = Keys.C Then
                    'Collection contiene il testo dei textbox
                    TestiRiga = New List(Of String)
                    'Restituisce il TPL giusto
                    generalCopiaTLP = nomeTLP()
                    'Loop fra i panel che contengono i textbox
                    For Each p As Panel In generalCopiaTLP.Controls().OfType(Of Panel)()
                        If rowIndex = ConfrontaPosizioneRiga(p, generalCopiaTLP) Then
                            For Each t As TextBox In p.Controls.OfType(Of TextBox)()
                                TestiRiga.Add(t.Text)
                            Next
                        End If
                    Next
                End If

                'Controlla se è stato premuto CTRL+V per Incolla
                If e.KeyCode = Keys.V Then
                    Dim ii As Byte = 0
                    generalIncollaTLP = nomeTLP()
                    For Each p As Panel In generalIncollaTLP.Controls().OfType(Of Panel)()
                        If rowIndex = ConfrontaPosizioneRiga(p, generalIncollaTLP) Then
                            For Each t As TextBox In p.Controls.OfType(Of TextBox)()
                                t.Text = TestiRiga.Item(ii)
                                ii += 1
                            Next
                        End If
                    Next

                End If

            End If

        Catch ex As Exception
            MessageBox.Show("Errore:Copia Riga :" & ex.Message)
        End Try
    End Sub


    Private Sub btnAggiungiNuovoSuUpdate_Click(sender As Object, e As EventArgs) Handles btnAggiungiNuovoSuUpdate.Click

        'Controlla se l'ID è da cambiare
        If idIntestazioneOnLoad = txtIdIntestazione.Text Then
            MessageBox.Show("ATTENZIONE : Prima di salvare devi impostare il nuovo ID del Test Report.", "Avviso Id Test Report", MessageBoxButtons.OK)
            Exit Sub
        End If

        Dim intContatoreCambia As Integer = contatore()
        Dim controlloID As Integer = IDCorrente()

        ' Controlla se L'ID è davvero l'ultimo se non lo è 
        'rilancia la funzione intContatoreCambia 
        If txtIdIntestazione.Text = controlloID Then
            txtIdIntestazione.Text = intContatoreCambia
        End If


        Dim result As DialogResult
        Dim intSalvaTestataTestReportBusLayer As Nullable(Of Integer)
        Dim intPrimoPezzo As Nullable(Of Integer)
        Dim intSecondoPezzo As Nullable(Of Integer)
        Dim intTerzoPezzo As Nullable(Of Integer)
        Dim intQuartoPezzo As Nullable(Of Integer)
        Dim intQuintoPezzo As Nullable(Of Integer)
        Dim intUltimoPezzo As Nullable(Of Integer)

        Try

            intSalvaTestataTestReportBusLayer = SalvaTestataTestReportBusLayer()
            intPrimoPezzo = SalvaPrimoPezzo()

            If (Not String.IsNullOrEmpty(txtValPrev1Pz2.Text)) Or (Not String.IsNullOrEmpty(txt1ValMisPz2.Text)) Then
                intSecondoPezzo = SalvaSecondoPezzo()
            Else
                intSecondoPezzo = insertIDSecondoPezzo()
            End If

            If (Not String.IsNullOrEmpty(txtValPrev1Pz3.Text)) Or (Not String.IsNullOrEmpty(txt1ValMisPz3.Text)) Then
                intTerzoPezzo = SalvaTerzoPezzo()
            Else
                intTerzoPezzo = insertIDTerzoPezzo()
            End If

            'Salva Quarto Pezzo se vuoto INSERT INTO IDPezziQuartoPezzo IDTestata
            If (Not String.IsNullOrEmpty(txtValPrev1Pz4.Text)) Or (Not String.IsNullOrEmpty(txt1ValMisPz4.Text)) Then
                intQuartoPezzo = SalvaQuartoPezzo()
            Else
                intQuartoPezzo = insertQuartoIDQuartoPezzo()
            End If

            'Salva Quinto Pezzo se vuoto INSERT INTO IDPezziQuintoPezzo IDTestata
            If (Not String.IsNullOrEmpty(txtValPrev1Pz5.Text)) Or (Not String.IsNullOrEmpty(txt1ValMisPz5.Text)) Then
                intQuintoPezzo = SalvaQuintoPezzo()
            Else
                intQuintoPezzo = insertIDQuintoPezzo()
            End If

            'Salva Ultimo Pezzo se vuoto INSERT INTO IDPezziUltimoPezzo IDTestata
            If (Not String.IsNullOrEmpty(txtValPrev1UltimoPz.Text)) Or (Not String.IsNullOrEmpty(txt1ValMisUltimoPz.Text)) Then
                intUltimoPezzo = SalvaUltimoPezzo()
            Else
                intUltimoPezzo = insertIDUltimoPezzo()
            End If

            If Not intSalvaTestataTestReportBusLayer Is Nothing Or Not intUltimoPezzo Is Nothing Then
                result = MessageBox.Show("Test Report Inserito Con Successo", "Inserimento Test Report in Database", MessageBoxButtons.OKCancel)
                If result = DialogResult.Cancel Then
                    Exit Sub
                End If
            End If


            'Ripulisce i controlli della form

            cmbStrumento.SelectedIndex = 0
            cmbMacchNum.SelectedIndex = 0
            cmbFornitore.SelectedIndex = 0
            cmbOperatore.SelectedIndex = 0
            cmbLottoNum.Text = String.Empty


            Dim allTxt As New List(Of Control)
            Dim allCmb As New List(Of Control)
            Dim allChk As New List(Of Control)

            For Each txt As TextBox In FindControlRecursive(allTxt, Me, GetType(TextBox))
                txt.Text = String.Empty
            Next

            For Each cmb As ComboBox In FindControlRecursive(allCmb, Me, GetType(ComboBox))
                cmb.SelectedIndex = -1
            Next

            For Each chk As CheckBox In FindControlRecursive(allChk, Me, GetType(CheckBox))
                chk.Checked = False
            Next


        Catch ex As Exception
            MessageBox.Show("Errore scrittura database : " & ex.Message)
        End Try

    End Sub

    Private Sub btnCambiaIDTestReport_Click(sender As Object, e As EventArgs) Handles btnCambiaIDTestReport.Click
        Dim intContatoreCambia As Integer = contatore()
        txtIdIntestazione.Text = intContatoreCambia
    End Sub

    Public Function IDCorrente() As Integer
        Dim i As Nullable(Of Integer)
        Try
            Dim strSQL As String = "SELECT MAX(IDIntestazione) AS IDIntestazione FROM tblIntestazione"
            Dim cmd As DbCommand = _db.GetSqlStringCommand(strSQL)
            Using datareader As IDataReader = _db.ExecuteReader(cmd)
                While datareader.Read
                    i = datareader("IDIntestazione")
                End While
            End Using
        Catch ex As Exception
            MessageBox.Show("Errore IDCorrente : " & ex.Message)
        End Try
        Return i
    End Function

    Public Function contatore() As Integer
        Dim i As Nullable(Of Integer)
        Try
            Dim strSQL As String = "SELECT MAX(IDIntestazione) AS IDIntestazione FROM tblIntestazione"
            Dim cmd As DbCommand = _db.GetSqlStringCommand(strSQL)

            Using datareader As IDataReader = _db.ExecuteReader(cmd)
                While datareader.Read
                    If IsDBNull(datareader("IDIntestazione")) Then
                        i = 1
                    Else
                        i = datareader("IDIntestazione")
                        i = i + 1
                    End If
                End While
            End Using

        Catch ex As Exception
            MessageBox.Show("Errore contatore con ID + 1 : " & ex.Message)
        End Try

        Return i

    End Function




#End Region

#Region "Funzioni Utility"

    Public Function Convert(value As Date) As Date
        Dim DateValue As DateTime = CType(value, DateTime)
        Return DateValue.ToShortDateString
    End Function

    Public Shared Function FindControlRecursive(list As List(Of Control), parent As Control, ctrlType As System.Type) As List(Of Control)
        If parent Is Nothing Then Return list
        If parent.GetType Is ctrlType Then
            list.Add(parent)
        End If
        For Each child As Control In parent.Controls
            FindControlRecursive(list, child, ctrlType)
        Next
        Return list
    End Function

#End Region

#Region "Carica Dati Testata By IDIntestazione"

    Public Sub getTestataByIdIntestazioneNew(intIdItestazione As Integer)
        Dim cmd As DbCommand = _db.GetStoredProcCommand("spGetIntestazioneNuovoDaUpdate")

        Try
            _db.AddInParameter(cmd, "IDIntestazione", DbType.Int32, intIdItestazione)
            Using datareader As IDataReader = _db.ExecuteReader(cmd)

                While datareader.Read
                    'IDIntestazione
                    propIdIntestazioneNew = datareader("IDIntestazione").ToString
                    'Campo di testo ID
                    txtIdIntestazione.Text = CType(propIdIntestazioneNew, String)
                    'Data test report
                    propDataReportNew = Convert(datareader("Data").ToString)
                    dtpData.Text = propDataReportNew
                    'Numero Ordine
                    propNumeroOrdineNew = datareader("NumOrdine").ToString
                    txtNumOrdine.Text = propNumeroOrdineNew
                    'Codice Articolo
                    propCodiceArticoloNew = datareader("CodArticolo").ToString
                    txtCodArt.Text = propCodiceArticoloNew
                    'Materiale
                    If Not datareader("Materiale").ToString Is Nothing Then
                        propMaterialeNew = datareader("Materiale").ToString
                        cmbMateriale.SelectedItem = propMaterialeNew
                    End If
                    'Strumento
                    propStrumentoNew = datareader("Strumento").ToString
                    cmbStrumento.SelectedText = propStrumentoNew
                    'Numero Macchina
                    propMacchinaNumNew = datareader("MacchinaNum").ToString
                    cmbMacchNum.SelectedText = propMacchinaNumNew
                    'Riga Ordine
                    propRigaOrdNumNew = CType(datareader("RigaOrdNum"), Integer)
                    txtRigaNum.Text = CType(propRigaOrdNumNew, String)
                    'Numero Pezzi
                    propNumPezziNew = CType(datareader("NumPezzi"), Integer)
                    txtPezziNum.Text = CType(propNumPezziNew, String)
                    'Fornitore
                    propFornitoreNew = datareader("Fornitore").ToString
                    cmbFornitore.SelectedText = propFornitoreNew
                    'Numero Lotto
                    propNumLottoNew = datareader("NumLotto").ToString
                    cmbLottoNum.Text = propNumLottoNew
                    'Primo Pezzo
                    propPrimoPezzoNew = CType(datareader("PrimoPezzo"), Boolean)
                    chkPrimoPezzo.Checked = propPrimoPezzoNew
                    'Ultimo Pezzo
                    propUltimoPezzoNew = CType(datareader("UltimoPezzo"), Boolean)
                    chkUltimoPezzo.Checked = propUltimoPezzoNew
                    'Pezzi Controllati
                    propPezziControllatiNew = datareader("PezzoNumero").ToString
                    txtPezziControllati.Text = propPezziControllatiNew
                    'Operatore
                    If Not datareader("Operatore").ToString Is Nothing Then
                        propOperatoreNew = datareader("Operatore").ToString
                        cmbOperatore.SelectedText = propOperatoreNew
                    Else
                        cmbOperatore.SelectedText = ""
                    End If

                End While
            End Using

        Catch ex As Exception
            MessageBox.Show("Errore : " & ex.Message)
        End Try
    End Sub

#End Region

#Region "Carica Dati PrimoPezzo Nuovo da Update By IDIntestazione"


    Public Sub getPrimoPezzoByIdIntestazioneNew(intIdItestazione As Integer)

        Dim cmd As DbCommand = _db.GetStoredProcCommand("spGetPrimoPezzoByIdIntestazione")

        Try
            _db.AddInParameter(cmd, "IDIntestazione", DbType.Int32, intIdItestazione)

            Using datareader As IDataReader = _db.ExecuteReader(cmd)

                While datareader.Read

                    'IDPezziPrimoPezzo
                    propIDPezziPrimoPezzoNew = CType(datareader("IDPezziPrimoPezzo"), Integer)

                    propValorePrevistoPrimoUnoNew = datareader("ValorePrevistoPrimoPezzoUno").ToString
                    txtValPrev1PrimoPz.Text = propValorePrevistoPrimoUnoNew

                    propValorePrevistoPrimoDueNew = datareader("ValorePrevistoPrimoPezzoDue").ToString
                    txtValPrev2PrimoPz.Text = propValorePrevistoPrimoDueNew

                    propValorePrevistoPrimoTreNew = datareader("ValorePrevistoPrimoPezzoTre").ToString
                    txtValPrev3PrimoPz.Text = propValorePrevistoPrimoTreNew

                    propValorePrevistoPrimoQuattroNew = datareader("ValorePrevistoPrimoPezzoQuattro").ToString
                    txtValPrev4PrimoPz.Text = propValorePrevistoPrimoQuattroNew

                    propValorePrevistoPrimoCinqueNew = datareader("ValorePrevistoPrimoPezzoCinque").ToString
                    txtValPrev5PrimoPz.Text = propValorePrevistoPrimoCinqueNew

                    propValorePrevistoPrimoSeiNew = datareader("ValorePrevistoPrimoPezzoSei").ToString
                    txtValPrev6PrimoPz.Text = propValorePrevistoPrimoSeiNew

                    propValorePrevistoPrimoSetteNew = datareader("ValorePrevistoPrimoPezzoSette").ToString
                    txtValPrev7PrimoPz.Text = propValorePrevistoPrimoSetteNew

                    propValorePrevistoPrimoOttoNew = datareader("ValorePrevistoPrimoPezzoOtto").ToString
                    txtValPrev8PrimoPz.Text = propValorePrevistoPrimoOttoNew

                    propValorePrevistoPrimoNoveNew = datareader("ValorePrevistoPrimoPezzoNove").ToString
                    txtValPrev9PrimoPz.Text = propValorePrevistoPrimoNoveNew

                    propValorePrevistoPrimoDieciNew = datareader("ValorePrevistoPrimoPezzoDieci").ToString
                    txtValPrev10PrimoPz.Text = propValorePrevistoPrimoDieciNew

                    propValoreMisuratoPrimoUnoNew = datareader("ValoreMisuratoPrimoPezzoUno").ToString
                    txt1ValMisPrimoPz.Text = propValoreMisuratoPrimoUnoNew

                    propValoreMisuratoPrimoDueNew = datareader("ValoreMisuratoPrimoPezzoDue").ToString
                    txt2ValMisPrimoPz.Text = propValoreMisuratoPrimoDueNew

                    propValoreMisuratoPrimoTreNew = datareader("ValoreMisuratoPrimoPezzoTre").ToString
                    txt3ValMisPrimoPz.Text = propValoreMisuratoPrimoTreNew

                    propValoreMisuratoPrimoQuattroNew = datareader("ValoreMisuratoPrimoPezzoQuattro").ToString
                    txt4ValMisPrimoPz.Text = propValoreMisuratoPrimoQuattroNew

                    propValoreMisuratoPrimoCinqueNew = datareader("ValoreMisuratoPrimoPezzoCinque").ToString
                    txt5ValMisPrimoPz.Text = propValoreMisuratoPrimoCinqueNew

                    propValoreMisuratoPrimoSeiNew = datareader("ValoreMisuratoPrimoPezzoSei").ToString
                    txt6ValMisPrimoPz.Text = propValoreMisuratoPrimoSeiNew

                    propValoreMisuratoPrimoSetteNew = datareader("ValoreMisuratoPrimoPezzoSette").ToString
                    txt7ValMisPrimoPz.Text = propValoreMisuratoPrimoSetteNew

                    propValoreMisuratoPrimoOttoNew = datareader("ValoreMisuratoPrimoPezzoOtto").ToString
                    txt8ValMisPrimoPz.Text = propValoreMisuratoPrimoOttoNew

                    propValoreMisuratoPrimoNoveNew = datareader("ValoreMisuratoPrimoPezzoNove").ToString
                    txt9ValMisPrimoPz.Text = propValoreMisuratoPrimoNoveNew

                    propValoreMisuratoPrimoDieciNew = datareader("ValoreMisuratoPrimoPezzoDieci").ToString
                    txt10ValMisPrimoPz.Text = propValoreMisuratoPrimoDieciNew

                    propTolleranzaPiupropValorePrevistoPrimoUnoNew = datareader("TolleranzaPiuValorePrevistoPrimoUno").ToString
                    txtTolleranzaPiuValPrev1PrimoPz.Text = propTolleranzaPiupropValorePrevistoPrimoUnoNew

                    propTolleranzaPiupropValorePrevistoPrimoDueNew = datareader("TolleranzaPiuValorePrevistoPrimoDue").ToString
                    txtTolleranzaPiuValPrev2PrimoPz.Text = propTolleranzaPiupropValorePrevistoPrimoDueNew

                    propTolleranzaPiupropValorePrevistoPrimoTreNew = datareader("TolleranzaPiuValorePrevistoPrimoTre").ToString
                    txtTolleranzaPiuValPrev3PrimoPz.Text = propTolleranzaPiupropValorePrevistoPrimoTreNew

                    propTolleranzaPiupropValorePrevistoPrimoQuattroNew = datareader("TolleranzaPiuValorePrevistoPrimoQuattro").ToString
                    txtTolleranzaPiuValPrev4PrimoPz.Text = propTolleranzaPiupropValorePrevistoPrimoQuattroNew

                    propTolleranzaPiupropValorePrevistoPrimoCinqueNew = datareader("TolleranzaPiuValorePrevistoPrimoCinque").ToString
                    txtTolleranzaPiuValPrev5PrimoPz.Text = propTolleranzaPiupropValorePrevistoPrimoCinqueNew

                    propTolleranzaPiupropValorePrevistoPrimoSeiNew = datareader("TolleranzaPiuValorePrevistoPrimoSei").ToString
                    txtTolleranzaPiuValPrev6PrimoPz.Text = propTolleranzaPiupropValorePrevistoPrimoSeiNew

                    propTolleranzaPiupropValorePrevistoPrimoSetteNew = datareader("TolleranzaPiuValorePrevistoPrimoSette").ToString
                    txtTolleranzaPiuValPrev7PrimoPz.Text = propTolleranzaPiupropValorePrevistoPrimoSetteNew

                    propTolleranzaPiupropValorePrevistoPrimoOttoNew = datareader("TolleranzaPiuValorePrevistoPrimoOtto").ToString
                    txtTolleranzaPiuValPrev8PrimoPz.Text = propTolleranzaPiupropValorePrevistoPrimoOttoNew

                    propTolleranzaPiupropValorePrevistoPrimoNoveNew = datareader("TolleranzaPiuValorePrevistoPrimoNove").ToString
                    txtTolleranzaPiuValPrev9PrimoPz.Text = propTolleranzaPiupropValorePrevistoPrimoNoveNew

                    propTolleranzaPiupropValorePrevistoPrimoDieciNew = datareader("TolleranzaPiuValorePrevistoPrimoDieci").ToString
                    txtTolleranzaPiuValPrev10PrimoPz.Text = propTolleranzaPiupropValorePrevistoPrimoDieciNew

                    propTolleranzaMenopropValorePrevistoPrimoUnoNew = datareader("TolleranzaMenoValorePrevistoPrimoUno").ToString
                    txtTolleranzaMenoValPrev1PrimoPz.Text = propTolleranzaMenopropValorePrevistoPrimoUnoNew

                    propTolleranzaMenopropValorePrevistoPrimoDueNew = datareader("TolleranzaMenoValorePrevistoPrimoDue").ToString
                    txtTolleranzaMenoValPrev2PrimoPz.Text = propTolleranzaMenopropValorePrevistoPrimoDueNew

                    propTolleranzaMenopropValorePrevistoPrimoTreNew = datareader("TolleranzaMenoValorePrevistoPrimoTre").ToString
                    txtTolleranzaMenoValPrev3PrimoPz.Text = propTolleranzaMenopropValorePrevistoPrimoTreNew

                    propTolleranzaMenopropValorePrevistoPrimoQuattroNew = datareader("TolleranzaMenoValorePrevistoPrimoQuattro").ToString
                    txtTolleranzaMenoValPrev4PrimoPz.Text = propTolleranzaMenopropValorePrevistoPrimoQuattroNew

                    propTolleranzaMenopropValorePrevistoPrimoCinqueNew = datareader("TolleranzaMenoValorePrevistoPrimoCinque").ToString
                    txtTolleranzaMenoValPrev5PrimoPz.Text = propTolleranzaMenopropValorePrevistoPrimoCinqueNew

                    propTolleranzaMenopropValorePrevistoPrimoSeiNew = datareader("TolleranzaMenoValorePrevistoPrimoSei").ToString
                    txtTolleranzaMenoValPrev6PrimoPz.Text = propTolleranzaMenopropValorePrevistoPrimoSeiNew

                    propTolleranzaMenopropValorePrevistoPrimoSetteNew = datareader("TolleranzaMenoValorePrevistoPrimoSette").ToString
                    txtTolleranzaMenoValPrev7PrimoPz.Text = propTolleranzaMenopropValorePrevistoPrimoSetteNew

                    propTolleranzaMenopropValorePrevistoPrimoOttoNew = datareader("TolleranzaMenoValorePrevistoPrimoOtto").ToString
                    txtTolleranzaMenoValPrev8PrimoPz.Text = propTolleranzaMenopropValorePrevistoPrimoOttoNew

                    propTolleranzaMenopropValorePrevistoPrimoNoveNew = datareader("TolleranzaMenoValorePrevistoPrimoNove").ToString
                    txtTolleranzaMenoValPrev9PrimoPz.Text = propTolleranzaMenopropValorePrevistoPrimoNoveNew

                    propTolleranzaMenopropValorePrevistoPrimoDieciNew = datareader("TolleranzaMenoValorePrevistoPrimoDieci").ToString
                    txtTolleranzaMenoValPrev10PrimoPz.Text = propTolleranzaMenopropValorePrevistoPrimoDieciNew

                    propNotePrimoPezzoNew = datareader("NotePrimoPezzo").ToString
                    txtNotePrimoPz.Text = propNotePrimoPezzoNew

                End While

            End Using

        Catch ex As Exception
            MessageBox.Show("Errore getPrimoPezzoByIdIntestazioneNew : " & ex.Message)
        End Try

    End Sub




#End Region

#Region "Carica Dati SecondoPezzo Nuovo da Update By IDIntestazione"


    Public Sub getSecondoPezzoByIdIntestazioneNew(intIdItestazione As Integer)

        Dim cmd As DbCommand = _db.GetStoredProcCommand("spGetSecondoPezzoByIdIntestazione")

        Try
            _db.AddInParameter(cmd, "IDIntestazione", DbType.Int32, intIdItestazione)

            Using datareader As IDataReader = _db.ExecuteReader(cmd)

                While datareader.Read

                    propIDPezziSecondoPezzoNew = CType(datareader("IDPezziSecondoPezzo"), Integer)

                    propValorePrevistoSecondoUnoNew = datareader("ValorePrevistoSecondoPezzoUno").ToString
                    txtValPrev1Pz2.Text = propValorePrevistoSecondoUnoNew
                    propValorePrevistoSecondoDueNew = datareader("ValorePrevistoSecondoPezzoDue").ToString
                    txtValPrev2Pz2.Text = propValorePrevistoSecondoDueNew
                    propValorePrevistoSecondoTreNew = datareader("ValorePrevistoSecondoPezzoTre").ToString
                    txtValPrev3Pz2.Text = propValorePrevistoSecondoTreNew
                    propValorePrevistoSecondoQuattroNew = datareader("ValorePrevistoSecondoPezzoQuattro").ToString
                    txtValPrev4Pz2.Text = propValorePrevistoSecondoQuattroNew
                    propValorePrevistoSecondoCinqueNew = datareader("ValorePrevistoSecondoPezzoCinque").ToString
                    txtValPrev5Pz2.Text = propValorePrevistoSecondoCinqueNew
                    propValorePrevistoSecondoSeiNew = datareader("ValorePrevistoSecondoPezzoSei").ToString
                    txtValPrev6Pz2.Text = propValorePrevistoSecondoSeiNew
                    propValorePrevistoSecondoSetteNew = datareader("ValorePrevistoSecondoPezzoSette").ToString
                    txtValPrev7Pz2.Text = propValorePrevistoSecondoSetteNew
                    propValorePrevistoSecondoOttoNew = datareader("ValorePrevistoSecondoPezzoOtto").ToString
                    txtValPrev8Pz2.Text = propValorePrevistoSecondoOttoNew
                    propValorePrevistoSecondoNoveNew = datareader("ValorePrevistoSecondoPezzoNove").ToString
                    txtValPrev9Pz2.Text = propValorePrevistoSecondoNoveNew
                    propValorePrevistoSecondoDieciNew = datareader("ValorePrevistoSecondoPezzoDieci").ToString
                    txtValPrev10Pz2.Text = propValorePrevistoSecondoDieciNew

                    propValoreMisuratoSecondoUnoNew = datareader("ValoreMisuratoSecondoPezzoUno").ToString
                    txt1ValMisPz2.Text = propValoreMisuratoSecondoUnoNew
                    propValoreMisuratoSecondoDueNew = datareader("ValoreMisuratoSecondoPezzoDue").ToString
                    txt2ValMisPz2.Text = propValoreMisuratoSecondoDueNew
                    propValoreMisuratoSecondoTreNew = datareader("ValoreMisuratoSecondoPezzoTre").ToString
                    txt3ValMisPz2.Text = propValoreMisuratoSecondoTreNew
                    propValoreMisuratoSecondoQuattroNew = datareader("ValoreMisuratoSecondoPezzoQuattro").ToString
                    txt4ValMisPz2.Text = propValoreMisuratoSecondoQuattroNew
                    propValoreMisuratoSecondoCinqueNew = datareader("ValoreMisuratoSecondoPezzoCinque").ToString
                    txt5ValMisPz2.Text = propValoreMisuratoSecondoCinqueNew
                    propValoreMisuratoSecondoSeiNew = datareader("ValoreMisuratoSecondoPezzoSei").ToString
                    txt6ValMisPz2.Text = propValoreMisuratoSecondoSeiNew
                    propValoreMisuratoSecondoSetteNew = datareader("ValoreMisuratoSecondoPezzoSette").ToString
                    txt7ValMisPz2.Text = propValoreMisuratoSecondoSetteNew
                    propValoreMisuratoSecondoOttoNew = datareader("ValoreMisuratoSecondoPezzoOtto").ToString
                    txt8ValMisPz2.Text = propValoreMisuratoSecondoOttoNew
                    propValoreMisuratoSecondoNoveNew = datareader("ValoreMisuratoSecondoPezzoNove").ToString
                    txt9ValMisPz2.Text = propValoreMisuratoSecondoNoveNew
                    propValoreMisuratoSecondoDieciNew = datareader("ValoreMisuratoSecondoPezzoDieci").ToString
                    txt10ValMisPz2.Text = propValoreMisuratoSecondoDieciNew

                    propTolleranzaPiupropValorePrevistoSecondoUnoNew = datareader("TolleranzaPiuValorePrevistoSecondoUno").ToString
                    txtTolleranzaPiuValPrev1Pz2.Text = propTolleranzaPiupropValorePrevistoSecondoUnoNew
                    propTolleranzaPiupropValorePrevistoSecondoDueNew = datareader("TolleranzaPiuValorePrevistoSecondoDue").ToString
                    txtTolleranzaPiuValPrev2Pz2.Text = propTolleranzaPiupropValorePrevistoSecondoDueNew
                    propTolleranzaPiupropValorePrevistoSecondoTreNew = datareader("TolleranzaPiuValorePrevistoSecondoTre").ToString
                    txtTolleranzaPiuValPrev3Pz2.Text = propTolleranzaPiupropValorePrevistoSecondoTreNew
                    propTolleranzaPiupropValorePrevistoSecondoQuattroNew = datareader("TolleranzaPiuValorePrevistoSecondoQuattro").ToString
                    txtTolleranzaPiuValPrev4Pz2.Text = propTolleranzaPiupropValorePrevistoSecondoQuattroNew
                    propTolleranzaPiupropValorePrevistoSecondoCinqueNew = datareader("TolleranzaPiuValorePrevistoSecondoCinque").ToString
                    txtTolleranzaPiuValPrev5Pz2.Text = propTolleranzaPiupropValorePrevistoSecondoCinqueNew
                    propTolleranzaPiupropValorePrevistoSecondoSeiNew = datareader("TolleranzaPiuValorePrevistoSecondoSei").ToString
                    txtTolleranzaPiuValPrev6Pz2.Text = propTolleranzaPiupropValorePrevistoSecondoSeiNew
                    propTolleranzaPiupropValorePrevistoSecondoSetteNew = datareader("TolleranzaPiuValorePrevistoSecondoSette").ToString
                    txtTolleranzaPiuValPrev7Pz2.Text = propTolleranzaPiupropValorePrevistoSecondoSetteNew
                    propTolleranzaPiupropValorePrevistoSecondoOttoNew = datareader("TolleranzaPiuValorePrevistoSecondoOtto").ToString
                    txtTolleranzaPiuValPrev8Pz2.Text = propTolleranzaPiupropValorePrevistoSecondoOttoNew
                    propTolleranzaPiupropValorePrevistoSecondoNoveNew = datareader("TolleranzaPiuValorePrevistoSecondoNove").ToString
                    txtTolleranzaPiuValPrev9Pz2.Text = propTolleranzaPiupropValorePrevistoSecondoNoveNew
                    propTolleranzaPiupropValorePrevistoSecondoDieciNew = datareader("TolleranzaPiuValorePrevistoSecondoDieci").ToString
                    txtTolleranzaPiuValPrev10Pz2.Text = propTolleranzaPiupropValorePrevistoSecondoDieciNew

                    propTolleranzaMenopropValorePrevistoSecondoUnoNew = datareader("TolleranzaMenoValorePrevistoSecondoUno").ToString
                    txtTolleranzaMenoValPrev1Pz2.Text = propTolleranzaMenopropValorePrevistoSecondoUnoNew
                    propTolleranzaMenopropValorePrevistoSecondoDueNew = datareader("TolleranzaMenoValorePrevistoSecondoDue").ToString
                    txtTolleranzaMenoValPrev2Pz2.Text = propTolleranzaMenopropValorePrevistoSecondoDueNew
                    propTolleranzaMenopropValorePrevistoSecondoTreNew = datareader("TolleranzaMenoValorePrevistoSecondoTre").ToString
                    txtTolleranzaMenoValPrev3Pz2.Text = propTolleranzaMenopropValorePrevistoSecondoTreNew
                    propTolleranzaMenopropValorePrevistoSecondoQuattroNew = datareader("TolleranzaMenoValorePrevistoSecondoQuattro").ToString
                    txtTolleranzaMenoValPrev4Pz2.Text = propTolleranzaMenopropValorePrevistoSecondoQuattroNew
                    propTolleranzaMenopropValorePrevistoSecondoCinqueNew = datareader("TolleranzaMenoValorePrevistoSecondoCinque").ToString
                    txtTolleranzaMenoValPrev5Pz2.Text = propTolleranzaMenopropValorePrevistoSecondoCinqueNew
                    propTolleranzaMenopropValorePrevistoSecondoSeiNew = datareader("TolleranzaMenoValorePrevistoSecondoSei").ToString
                    txtTolleranzaMenoValPrev6Pz2.Text = propTolleranzaMenopropValorePrevistoSecondoSeiNew
                    propTolleranzaMenopropValorePrevistoSecondoSetteNew = datareader("TolleranzaMenoValorePrevistoSecondoSette").ToString
                    txtTolleranzaMenoValPrev7Pz2.Text = propTolleranzaMenopropValorePrevistoSecondoSetteNew
                    propTolleranzaMenopropValorePrevistoSecondoOttoNew = datareader("TolleranzaMenoValorePrevistoSecondoOtto").ToString
                    txtTolleranzaMenoValPrev8Pz2.Text = propTolleranzaMenopropValorePrevistoSecondoOttoNew
                    propTolleranzaMenopropValorePrevistoSecondoNoveNew = datareader("TolleranzaMenoValorePrevistoSecondoNove").ToString
                    txtTolleranzaMenoValPrev9Pz2.Text = propTolleranzaMenopropValorePrevistoSecondoNoveNew
                    propTolleranzaMenopropValorePrevistoSecondoDieciNew = datareader("TolleranzaMenoValorePrevistoSecondoDieci").ToString
                    txtTolleranzaMenoValPrev10Pz2.Text = propTolleranzaMenopropValorePrevistoSecondoDieciNew

                    propNoteSecondoPezzoNew = datareader("NoteSecondoPezzo").ToString
                    txtNotePz2.Text = propNoteSecondoPezzoNew

                End While


            End Using


        Catch ex As Exception
            MessageBox.Show("Errore : " & ex.Message)
        End Try


    End Sub

#End Region

#Region "Carica Dati TerzoPezzo Nuovo da Update By IDIntestazione"

    Public Sub getTerzoPezzoByIdIntestazioneNew(intIdItestazione As Integer)

        Dim cmd As DbCommand = _db.GetStoredProcCommand("spGetTerzoPezzoByIdIntestazione")

        Try
            _db.AddInParameter(cmd, "IDIntestazione", DbType.Int32, intIdItestazione)

            Using datareader As IDataReader = _db.ExecuteReader(cmd)

                While datareader.Read

                    propIDPezziTerzoPezzoNew = CType(datareader("IDPezziTerzoPezzo"), Integer)

                    propValorePrevistoTerzoUnoNew = datareader("ValorePrevistoTerzoPezzoUno").ToString
                    txtValPrev1Pz3.Text = propValorePrevistoTerzoUnoNew
                    propValorePrevistoTerzoDueNew = datareader("ValorePrevistoTerzoPezzoDue").ToString
                    txtValPrev2Pz3.Text = propValorePrevistoTerzoDueNew
                    propValorePrevistoTerzoTreNew = datareader("ValorePrevistoTerzoPezzoTre").ToString
                    txtValPrev3Pz3.Text = propValorePrevistoTerzoTreNew
                    propValorePrevistoTerzoQuattroNew = datareader("ValorePrevistoTerzoPezzoQuattro").ToString
                    txtValPrev4Pz3.Text = propValorePrevistoTerzoQuattroNew
                    propValorePrevistoTerzoCinqueNew = datareader("ValorePrevistoTerzoPezzoCinque").ToString
                    txtValPrev5Pz3.Text = propValorePrevistoTerzoCinqueNew
                    propValorePrevistoTerzoSeiNew = datareader("ValorePrevistoTerzoPezzoSei").ToString
                    txtValPrev6Pz3.Text = propValorePrevistoTerzoSeiNew
                    propValorePrevistoTerzoSetteNew = datareader("ValorePrevistoTerzoPezzoSette").ToString
                    txtValPrev7Pz3.Text = propValorePrevistoTerzoSetteNew
                    propValorePrevistoTerzoOttoNew = datareader("ValorePrevistoTerzoPezzoOtto").ToString
                    txtValPrev8Pz3.Text = propValorePrevistoTerzoOttoNew
                    propValorePrevistoTerzoNoveNew = datareader("ValorePrevistoTerzoPezzoNove").ToString
                    txtValPrev9Pz3.Text = propValorePrevistoTerzoNoveNew
                    propValorePrevistoTerzoDieciNew = datareader("ValorePrevistoTerzoPezzoDieci").ToString
                    txtValPrev10Pz3.Text = propValorePrevistoTerzoDieciNew

                    propValoreMisuratoTerzoUnoNew = datareader("ValoreMisuratoTerzoPezzoUno").ToString
                    txt1ValMisPz3.Text = propValoreMisuratoTerzoUnoNew
                    propValoreMisuratoTerzoDueNew = datareader("ValoreMisuratoTerzoPezzoDue").ToString
                    txt2ValMisPz3.Text = propValoreMisuratoTerzoDueNew
                    propValoreMisuratoTerzoTreNew = datareader("ValoreMisuratoTerzoPezzoTre").ToString
                    txt3ValMisPz3.Text = propValoreMisuratoTerzoTreNew
                    propValoreMisuratoTerzoQuattroNew = datareader("ValoreMisuratoTerzoPezzoQuattro").ToString
                    txt4ValMisPz3.Text = propValoreMisuratoTerzoQuattroNew
                    propValoreMisuratoTerzoCinqueNew = datareader("ValoreMisuratoTerzoPezzoCinque").ToString
                    txt5ValMisPz3.Text = propValoreMisuratoTerzoCinqueNew
                    propValoreMisuratoTerzoSeiNew = datareader("ValoreMisuratoTerzoPezzoSei").ToString
                    txt6ValMisPz3.Text = propValoreMisuratoTerzoSeiNew
                    propValoreMisuratoTerzoSette = datareader("ValoreMisuratoTerzoPezzoSette").ToString
                    txt7ValMisPz3.Text = propValoreMisuratoTerzoSette
                    propValoreMisuratoTerzoOttoNew = datareader("ValoreMisuratoTerzoPezzoOtto").ToString
                    txt8ValMisPz3.Text = propValoreMisuratoTerzoOttoNew
                    propValoreMisuratoTerzoNoveNew = datareader("ValoreMisuratoTerzoPezzoNove").ToString
                    txt9ValMisPz3.Text = propValoreMisuratoTerzoNoveNew
                    propValoreMisuratoTerzoDieciNew = datareader("ValoreMisuratoTerzoPezzoDieci").ToString
                    txt10ValMisPz3.Text = propValoreMisuratoTerzoDieciNew

                    propTolleranzaPiupropValorePrevistoTerzoUnoNew = datareader("TolleranzaPiuValorePrevistoTerzoUno").ToString
                    txtTolleranzaPiuValPrev1Pz3.Text = propTolleranzaPiupropValorePrevistoTerzoUnoNew
                    propTolleranzaPiupropValorePrevistoTerzoDueNew = datareader("TolleranzaPiuValorePrevistoTerzoDue").ToString
                    txtTolleranzaPiuValPrev2Pz3.Text = propTolleranzaPiupropValorePrevistoTerzoDueNew
                    propTolleranzaPiupropValorePrevistoTerzoTreNew = datareader("TolleranzaPiuValorePrevistoTerzoTre").ToString
                    txtTolleranzaPiuValPrev3Pz3.Text = propTolleranzaPiupropValorePrevistoTerzoTreNew
                    propTolleranzaPiupropValorePrevistoTerzoQuattroNew = datareader("TolleranzaPiuValorePrevistoTerzoQuattro").ToString
                    txtTolleranzaPiuValPrev4Pz3.Text = propTolleranzaPiupropValorePrevistoTerzoQuattroNew
                    propTolleranzaPiupropValorePrevistoTerzoCinqueNew = datareader("TolleranzaPiuValorePrevistoTerzoCinque").ToString
                    txtTolleranzaPiuValPrev5Pz3.Text = propTolleranzaPiupropValorePrevistoTerzoCinqueNew
                    propTolleranzaPiupropValorePrevistoTerzoSeiNew = datareader("TolleranzaPiuValorePrevistoTerzoSei").ToString
                    txtTolleranzaPiuValPrev6Pz3.Text = propTolleranzaPiupropValorePrevistoTerzoSeiNew
                    propTolleranzaPiupropValorePrevistoTerzoSetteNew = datareader("TolleranzaPiuValorePrevistoTerzoSette").ToString
                    txtTolleranzaPiuValPrev7Pz3.Text = propTolleranzaPiupropValorePrevistoTerzoSetteNew
                    propTolleranzaPiupropValorePrevistoTerzoOttoNew = datareader("TolleranzaPiuValorePrevistoTerzoOtto").ToString
                    txtTolleranzaPiuValPrev8Pz3.Text = propTolleranzaPiupropValorePrevistoTerzoOttoNew
                    propTolleranzaPiupropValorePrevistoTerzoNoveNew = datareader("TolleranzaPiuValorePrevistoTerzoNove").ToString
                    txtTolleranzaPiuValPrev9Pz3.Text = propTolleranzaPiupropValorePrevistoTerzoNoveNew
                    propTolleranzaPiupropValorePrevistoTerzoDieciNew = datareader("TolleranzaPiuValorePrevistoTerzoDieci").ToString
                    txtTolleranzaPiuValPrev10Pz3.Text = propTolleranzaPiupropValorePrevistoTerzoDieciNew

                    propTolleranzaMenopropValorePrevistoTerzoUnoNew = datareader("TolleranzaMenoValorePrevistoTerzoUno").ToString
                    txtTolleranzaMenoValPrev1Pz3.Text = propTolleranzaMenopropValorePrevistoTerzoUnoNew
                    propTolleranzaMenopropValorePrevistoTerzoDueNew = datareader("TolleranzaMenoValorePrevistoTerzoDue").ToString
                    txtTolleranzaMenoValPrev2Pz3.Text = propTolleranzaMenopropValorePrevistoTerzoDueNew
                    propTolleranzaMenopropValorePrevistoTerzoTreNew = datareader("TolleranzaMenoValorePrevistoTerzoTre").ToString
                    txtTolleranzaMenoValPrev3Pz3.Text = propTolleranzaMenopropValorePrevistoTerzoTreNew
                    propTolleranzaMenopropValorePrevistoTerzoQuattroNew = datareader("TolleranzaMenoValorePrevistoTerzoQuattro").ToString
                    txtTolleranzaMenoValPrev4Pz3.Text = propTolleranzaMenopropValorePrevistoTerzoQuattroNew
                    propTolleranzaMenopropValorePrevistoTerzoCinqueNew = datareader("TolleranzaMenoValorePrevistoTerzoCinque").ToString
                    txtTolleranzaMenoValPrev5Pz3.Text = propTolleranzaMenopropValorePrevistoTerzoCinqueNew
                    propTolleranzaMenopropValorePrevistoTerzoSeiNew = datareader("TolleranzaMenoValorePrevistoTerzoSei").ToString
                    txtTolleranzaMenoValPrev6Pz3.Text = propTolleranzaMenopropValorePrevistoTerzoSeiNew
                    propTolleranzaMenopropValorePrevistoTerzoSetteNew = datareader("TolleranzaMenoValorePrevistoTerzoSette").ToString
                    txtTolleranzaMenoValPrev7Pz3.Text = propTolleranzaMenopropValorePrevistoTerzoSetteNew
                    propTolleranzaMenopropValorePrevistoTerzoOttoNew = datareader("TolleranzaMenoValorePrevistoTerzoOtto").ToString
                    txtTolleranzaMenoValPrev8Pz3.Text = propTolleranzaMenopropValorePrevistoTerzoOttoNew
                    propTolleranzaMenopropValorePrevistoTerzoNoveNew = datareader("TolleranzaMenoValorePrevistoTerzoNove").ToString
                    txtTolleranzaMenoValPrev9Pz3.Text = propTolleranzaMenopropValorePrevistoTerzoNoveNew
                    propTolleranzaMenopropValorePrevistoTerzoDieciNew = datareader("TolleranzaMenoValorePrevistoTerzoDieci").ToString
                    txtTolleranzaMenoValPrev10Pz3.Text = propTolleranzaMenopropValorePrevistoTerzoDieciNew

                    propNoteTerzoPezzoNew = datareader("NoteTerzoPezzo").ToString
                    txtNotePz3.Text = propNoteTerzoPezzoNew

                End While


            End Using


        Catch ex As Exception
            MessageBox.Show("Errore : " & ex.Message)
        End Try


    End Sub



#End Region

#Region "Carica Dati QuartoPezzo Nuovo da Update By IDIntestazione"

    Public Sub getQuartoPezzoByIdIntestazioneNew(intIdItestazione As Integer)

        Dim cmd As DbCommand = _db.GetStoredProcCommand("spGetQuartoPezzoByIdIntestazione")

        Try
            _db.AddInParameter(cmd, "IDIntestazione", DbType.Int32, intIdItestazione)

            Using datareader As IDataReader = _db.ExecuteReader(cmd)

                While datareader.Read

                    propIDPezziQuartoPezzoNew = CType(datareader("IDPezziQuartoPezzo"), Integer)

                    propValorePrevistoQuartoUnoNew = datareader("ValorePrevistoQuartoPezzoUno").ToString
                    txtValPrev1Pz4.Text = propValorePrevistoQuartoUnoNew
                    propValorePrevistoQuartoDueNew = datareader("ValorePrevistoQuartoPezzoDue").ToString
                    txtValPrev2Pz4.Text = propValorePrevistoQuartoDueNew
                    propValorePrevistoQuartoTreNew = datareader("ValorePrevistoQuartoPezzoTre").ToString
                    txtValPrev3Pz4.Text = propValorePrevistoQuartoTreNew
                    propValorePrevistoQuartoQuattro = datareader("ValorePrevistoQuartoPezzoQuattro").ToString
                    txtValPrev4Pz4.Text = propValorePrevistoQuartoQuattroNew
                    propValorePrevistoQuartoCinqueNew = datareader("ValorePrevistoQuartoPezzoCinque").ToString
                    txtValPrev5Pz4.Text = propValorePrevistoQuartoCinqueNew
                    propValorePrevistoQuartoSeiNew = datareader("ValorePrevistoQuartoPezzoSei").ToString
                    txtValPrev6Pz4.Text = propValorePrevistoQuartoSeiNew
                    propValorePrevistoQuartoSetteNew = datareader("ValorePrevistoQuartoPezzoSette").ToString
                    txtValPrev7Pz4.Text = propValorePrevistoQuartoSetteNew
                    propValorePrevistoQuartoOtto = datareader("ValorePrevistoQuartoPezzoOtto").ToString
                    txtValPrev8Pz4.Text = propValorePrevistoQuartoOtto
                    propValorePrevistoQuartoNove = datareader("ValorePrevistoQuartoPezzoNove").ToString
                    txtValPrev9Pz4.Text = propValorePrevistoQuartoNoveNew
                    propValorePrevistoQuartoDieci = datareader("ValorePrevistoQuartoPezzoDieci").ToString
                    txtValPrev10Pz4.Text = propValorePrevistoQuartoDieciNew

                    propValoreMisuratoQuartoUnoNew = datareader("ValoreMisuratoQuartoPezzoUno").ToString
                    txt1ValMisPz4.Text = propValoreMisuratoQuartoUnoNew
                    propValoreMisuratoQuartoDueNew = datareader("ValoreMisuratoQuartoPezzoDue").ToString
                    txt2ValMisPz4.Text = propValoreMisuratoQuartoDueNew
                    propValoreMisuratoQuartoTreNew = datareader("ValoreMisuratoQuartoPezzoTre").ToString
                    txt3ValMisPz4.Text = propValoreMisuratoQuartoTreNew
                    propValoreMisuratoQuartoQuattroNew = datareader("ValoreMisuratoQuartoPezzoQuattro").ToString
                    txt4ValMisPz4.Text = propValoreMisuratoQuartoQuattroNew
                    propValoreMisuratoQuartoCinqueNew = datareader("ValoreMisuratoQuartoPezzoCinque").ToString
                    txt5ValMisPz4.Text = propValoreMisuratoQuartoCinqueNew
                    propValoreMisuratoQuartoSeiNew = datareader("ValoreMisuratoQuartoPezzoSei").ToString
                    txt6ValMisPz4.Text = propValoreMisuratoQuartoSeiNew
                    propValoreMisuratoQuartoSetteNew = datareader("ValoreMisuratoQuartoPezzoSette").ToString
                    txt7ValMisPz4.Text = propValoreMisuratoQuartoSetteNew
                    propValoreMisuratoQuartoOttoNew = datareader("ValoreMisuratoQuartoPezzoOtto").ToString
                    txt8ValMisPz4.Text = propValoreMisuratoQuartoOttoNew
                    propValoreMisuratoQuartoNoveNew = datareader("ValoreMisuratoQuartoPezzoNove").ToString
                    txt9ValMisPz4.Text = propValoreMisuratoQuartoNoveNew
                    propValoreMisuratoQuartoDieciNew = datareader("ValoreMisuratoQuartoPezzoDieci").ToString
                    txt10ValMisPz4.Text = propValoreMisuratoQuartoDieciNew

                    propTolleranzaPiupropValorePrevistoQuartoUnoNew = datareader("TolleranzaPiuValorePrevistoQuartoUno").ToString
                    txtTolleranzaPiuValPrev1Pz4.Text = propTolleranzaPiupropValorePrevistoQuartoUnoNew
                    propTolleranzaPiupropValorePrevistoQuartoDueNew = datareader("TolleranzaPiuValorePrevistoQuartoDue").ToString
                    txtTolleranzaPiuValPrev2Pz4.Text = propTolleranzaPiupropValorePrevistoQuartoDueNew
                    propTolleranzaPiupropValorePrevistoQuartoTreNew = datareader("TolleranzaPiuValorePrevistoQuartoTre").ToString
                    txtTolleranzaPiuValPrev3Pz4.Text = propTolleranzaPiupropValorePrevistoQuartoTreNew
                    propTolleranzaPiupropValorePrevistoQuartoQuattroNew = datareader("TolleranzaPiuValorePrevistoQuartoQuattro").ToString
                    txtTolleranzaPiuValPrev4Pz4.Text = propTolleranzaPiupropValorePrevistoQuartoQuattroNew
                    propTolleranzaPiupropValorePrevistoQuartoCinqueNew = datareader("TolleranzaPiuValorePrevistoQuartoCinque").ToString
                    txtTolleranzaPiuValPrev5Pz4.Text = propTolleranzaPiupropValorePrevistoQuartoCinqueNew
                    propTolleranzaPiupropValorePrevistoQuartoSeiNew = datareader("TolleranzaPiuValorePrevistoQuartoSei").ToString
                    txtTolleranzaPiuValPrev6Pz4.Text = propTolleranzaPiupropValorePrevistoQuartoSeiNew
                    propTolleranzaPiupropValorePrevistoQuartoSetteNew = datareader("TolleranzaPiuValorePrevistoQuartoSette").ToString
                    txtTolleranzaPiuValPrev7Pz4.Text = propTolleranzaPiupropValorePrevistoQuartoSetteNew
                    propTolleranzaPiupropValorePrevistoQuartoOttoNew = datareader("TolleranzaPiuValorePrevistoQuartoOtto").ToString
                    txtTolleranzaPiuValPrev8Pz4.Text = propTolleranzaPiupropValorePrevistoQuartoOttoNew
                    propTolleranzaPiupropValorePrevistoQuartoNoveNew = datareader("TolleranzaPiuValorePrevistoQuartoNove").ToString
                    txtTolleranzaPiuValPrev9Pz4.Text = propTolleranzaPiupropValorePrevistoQuartoNoveNew
                    propTolleranzaPiupropValorePrevistoQuartoDieciNew = datareader("TolleranzaPiuValorePrevistoQuartoDieci").ToString
                    txtTolleranzaPiuValPrev10Pz4.Text = propTolleranzaPiupropValorePrevistoQuartoDieciNew

                    propTolleranzaMenopropValorePrevistoQuartoUnoNew = datareader("TolleranzaMenoValorePrevistoQuartoUno").ToString
                    txtTolleranzaMenoValPrev1Pz4.Text = propTolleranzaMenopropValorePrevistoQuartoUnoNew
                    propTolleranzaMenopropValorePrevistoQuartoDueNew = datareader("TolleranzaMenoValorePrevistoQuartoDue").ToString
                    txtTolleranzaMenoValPrev2Pz4.Text = propTolleranzaMenopropValorePrevistoQuartoDueNew
                    propTolleranzaMenopropValorePrevistoQuartoTreNew = datareader("TolleranzaMenoValorePrevistoQuartoTre").ToString
                    txtTolleranzaMenoValPrev3Pz4.Text = propTolleranzaMenopropValorePrevistoQuartoTreNew
                    propTolleranzaMenopropValorePrevistoQuartoQuattroNew = datareader("TolleranzaMenoValorePrevistoQuartoQuattro").ToString
                    txtTolleranzaMenoValPrev4Pz4.Text = propTolleranzaMenopropValorePrevistoQuartoQuattroNew
                    propTolleranzaMenopropValorePrevistoQuartoCinqueNew = datareader("TolleranzaMenoValorePrevistoQuartoCinque").ToString
                    txtTolleranzaMenoValPrev5Pz4.Text = propTolleranzaMenopropValorePrevistoQuartoCinqueNew
                    propTolleranzaMenopropValorePrevistoQuartoSeiNew = datareader("TolleranzaMenoValorePrevistoQuartoSei").ToString
                    txtTolleranzaMenoValPrev6Pz4.Text = propTolleranzaMenopropValorePrevistoQuartoSeiNew
                    propTolleranzaMenopropValorePrevistoQuartoSetteNew = datareader("TolleranzaMenoValorePrevistoQuartoSette").ToString
                    txtTolleranzaMenoValPrev7Pz4.Text = propTolleranzaMenopropValorePrevistoQuartoSetteNew
                    propTolleranzaMenopropValorePrevistoQuartoOttoNew = datareader("TolleranzaMenoValorePrevistoQuartoOtto").ToString
                    txtTolleranzaMenoValPrev8Pz4.Text = propTolleranzaMenopropValorePrevistoQuartoOttoNew
                    propTolleranzaMenopropValorePrevistoQuartoNoveNew = datareader("TolleranzaMenoValorePrevistoQuartoNove").ToString
                    txtTolleranzaMenoValPrev9Pz4.Text = propTolleranzaMenopropValorePrevistoQuartoNoveNew
                    propTolleranzaMenopropValorePrevistoQuartoDieciNew = datareader("TolleranzaMenoValorePrevistoQuartoDieci").ToString
                    txtTolleranzaMenoValPrev10Pz4.Text = propTolleranzaMenopropValorePrevistoQuartoDieciNew

                    propNoteQuartoPezzoNew = datareader("NoteQuartoPezzo").ToString
                    txtNotePz4.Text = propNoteQuartoPezzoNew

                End While


            End Using


        Catch ex As Exception
            MessageBox.Show("Errore : " & ex.Message)
        End Try


    End Sub



#End Region

#Region "Carica Dati QuintoPezzo Nuovo da Update By IDIntestazione"


    Public Sub getQuintoPezzoByIdIntestazioneNew(intIdItestazione As Integer)

        Dim cmd As DbCommand = _db.GetStoredProcCommand("spGetQuintoPezzoByIdIntestazione")

        Try
            _db.AddInParameter(cmd, "IDIntestazione", DbType.Int32, intIdItestazione)

            Using datareader As IDataReader = _db.ExecuteReader(cmd)

                While datareader.Read

                    propIDPezziQuintoPezzoNew = CType(datareader("IDPezziQuintoPezzo"), Integer)

                    propValorePrevistoQuintoUnoNew = datareader("ValorePrevistoQuintoPezzoUno").ToString
                    txtValPrev1Pz5.Text = propValorePrevistoQuintoUnoNew
                    propValorePrevistoQuintoDueNew = datareader("ValorePrevistoQuintoPezzoDue").ToString
                    txtValPrev2Pz5.Text = propValorePrevistoQuintoDueNew
                    propValorePrevistoQuintoTreNew = datareader("ValorePrevistoQuintoPezzoTre").ToString
                    txtValPrev3Pz5.Text = propValorePrevistoQuintoTreNew
                    propValorePrevistoQuintoQuattroNew = datareader("ValorePrevistoQuintoPezzoQuattro").ToString
                    txtValPrev4Pz5.Text = propValorePrevistoQuintoQuattroNew
                    propValorePrevistoQuintoCinqueNew = datareader("ValorePrevistoQuintoPezzoCinque").ToString
                    txtValPrev5Pz5.Text = propValorePrevistoQuintoCinqueNew
                    propValorePrevistoQuintoSeiNew = datareader("ValorePrevistoQuintoPezzoSei").ToString
                    txtValPrev6Pz5.Text = propValorePrevistoQuintoSeiNew
                    propValorePrevistoQuintoSetteNew = datareader("ValorePrevistoQuintoPezzoSette").ToString
                    txtValPrev7Pz5.Text = propValorePrevistoQuintoSetteNew
                    propValorePrevistoQuintoOttoNew = datareader("ValorePrevistoQuintoPezzoOtto").ToString
                    txtValPrev8Pz5.Text = propValorePrevistoQuintoOttoNew
                    propValorePrevistoQuintoNoveNew = datareader("ValorePrevistoQuintoPezzoNove").ToString
                    txtValPrev9Pz5.Text = propValorePrevistoQuintoNoveNew
                    propValorePrevistoQuintoDieciNew = datareader("ValorePrevistoQuintoPezzoDieci").ToString
                    txtValPrev10Pz5.Text = propValorePrevistoQuintoDieciNew

                    propValoreMisuratoQuintoUnoNew = datareader("ValoreMisuratoQuintoPezzoUno").ToString
                    txt1ValMisPz5.Text = propValoreMisuratoQuintoUnoNew
                    propValoreMisuratoQuintoDueNew = datareader("ValoreMisuratoQuintoPezzoDue").ToString
                    txt2ValMisPz5.Text = propValoreMisuratoQuintoDueNew
                    propValoreMisuratoQuintoTreNew = datareader("ValoreMisuratoQuintoPezzoTre").ToString
                    txt3ValMisPz5.Text = propValoreMisuratoQuintoTreNew
                    propValoreMisuratoQuintoQuattroNew = datareader("ValoreMisuratoQuintoPezzoQuattro").ToString
                    txt4ValMisPz5.Text = propValoreMisuratoQuintoQuattroNew
                    propValoreMisuratoQuintoCinqueNew = datareader("ValoreMisuratoQuintoPezzoCinque").ToString
                    txt5ValMisPz5.Text = propValoreMisuratoQuintoCinqueNew
                    propValoreMisuratoQuintoSeiNew = datareader("ValoreMisuratoQuintoPezzoSei").ToString
                    txt6ValMisPz5.Text = propValoreMisuratoQuintoSeiNew
                    propValoreMisuratoQuintoSetteNew = datareader("ValoreMisuratoQuintoPezzoSette").ToString
                    txt7ValMisPz5.Text = propValoreMisuratoQuintoSetteNew
                    propValoreMisuratoQuintoOttoNew = datareader("ValoreMisuratoQuintoPezzoOtto").ToString
                    txt8ValMisPz5.Text = propValoreMisuratoQuintoOttoNew
                    propValoreMisuratoQuintoNoveNew = datareader("ValoreMisuratoQuintoPezzoNove").ToString
                    txt9ValMisPz5.Text = propValoreMisuratoQuintoNoveNew
                    propValoreMisuratoQuintoDieciNew = datareader("ValoreMisuratoQuintoPezzoDieci").ToString
                    txt10ValMisPz5.Text = propValoreMisuratoQuintoDieciNew

                    propTolleranzaPiupropValorePrevistoQuintoUnoNew = datareader("TolleranzaPiuValorePrevistoQuintoUno").ToString
                    txtTolleranzaPiuValPrev1Pz5.Text = propTolleranzaPiupropValorePrevistoQuintoUnoNew
                    propTolleranzaPiupropValorePrevistoQuintoDueNew = datareader("TolleranzaPiuValorePrevistoQuintoDue").ToString
                    txtTolleranzaPiuValPrev2Pz5.Text = propTolleranzaPiupropValorePrevistoQuintoDueNew
                    propTolleranzaPiupropValorePrevistoQuintoTreNew = datareader("TolleranzaPiuValorePrevistoQuintoTre").ToString
                    txtTolleranzaPiuValPrev3Pz5.Text = propTolleranzaPiupropValorePrevistoQuintoTreNew
                    propTolleranzaPiupropValorePrevistoQuintoQuattroNew = datareader("TolleranzaPiuValorePrevistoQuintoQuattro").ToString
                    txtTolleranzaPiuValPrev4Pz5.Text = propTolleranzaPiupropValorePrevistoQuintoQuattroNew
                    propTolleranzaPiupropValorePrevistoQuintoCinqueNew = datareader("TolleranzaPiuValorePrevistoQuintoCinque").ToString
                    txtTolleranzaPiuValPrev5Pz5.Text = propTolleranzaPiupropValorePrevistoQuintoCinqueNew
                    propTolleranzaPiupropValorePrevistoQuintoSeiNew = datareader("TolleranzaPiuValorePrevistoQuintoSei").ToString
                    txtTolleranzaPiuValPrev6Pz5.Text = propTolleranzaPiupropValorePrevistoQuintoSeiNew
                    propTolleranzaPiupropValorePrevistoQuintoSetteNew = datareader("TolleranzaPiuValorePrevistoQuintoSette").ToString
                    txtTolleranzaPiuValPrev7Pz5.Text = propTolleranzaPiupropValorePrevistoQuintoSetteNew
                    propTolleranzaPiupropValorePrevistoQuintoOttoNew = datareader("TolleranzaPiuValorePrevistoQuintoOtto").ToString
                    txtTolleranzaPiuValPrev8Pz5.Text = propTolleranzaPiupropValorePrevistoQuintoOttoNew
                    propTolleranzaPiupropValorePrevistoQuintoNoveNew = datareader("TolleranzaPiuValorePrevistoQuintoNove").ToString
                    txtTolleranzaPiuValPrev9Pz5.Text = propTolleranzaPiupropValorePrevistoQuintoNoveNew
                    propTolleranzaPiupropValorePrevistoQuintoDieciNew = datareader("TolleranzaPiuValorePrevistoQuintoDieci").ToString
                    txtTolleranzaPiuValPrev10Pz5.Text = propTolleranzaPiupropValorePrevistoQuintoDieciNew

                    propTolleranzaMenopropValorePrevistoQuintoUnoNew = datareader("TolleranzaMenoValorePrevistoQuintoUno").ToString
                    txtTolleranzaMenoValPrev1Pz5.Text = propTolleranzaMenopropValorePrevistoQuintoUnoNew
                    propTolleranzaMenopropValorePrevistoQuintoDueNew = datareader("TolleranzaMenoValorePrevistoQuintoDue").ToString
                    txtTolleranzaMenoValPrev2Pz5.Text = propTolleranzaMenopropValorePrevistoQuintoDueNew
                    propTolleranzaMenopropValorePrevistoQuintoTreNew = datareader("TolleranzaMenoValorePrevistoQuintoTre").ToString
                    txtTolleranzaMenoValPrev3Pz5.Text = propTolleranzaMenopropValorePrevistoQuintoTreNew
                    propTolleranzaMenopropValorePrevistoQuintoQuattroNew = datareader("TolleranzaMenoValorePrevistoQuintoQuattro").ToString
                    txtTolleranzaMenoValPrev4Pz5.Text = propTolleranzaMenopropValorePrevistoQuintoQuattroNew
                    propTolleranzaMenopropValorePrevistoQuintoCinqueNew = datareader("TolleranzaMenoValorePrevistoQuintoCinque").ToString
                    txtTolleranzaMenoValPrev5Pz5.Text = propTolleranzaMenopropValorePrevistoQuintoCinqueNew
                    propTolleranzaMenopropValorePrevistoQuintoSeiNew = datareader("TolleranzaMenoValorePrevistoQuintoSei").ToString
                    txtTolleranzaMenoValPrev6Pz5.Text = propTolleranzaMenopropValorePrevistoQuintoSeiNew
                    propTolleranzaMenopropValorePrevistoQuintoSetteNew = datareader("TolleranzaMenoValorePrevistoQuintoSette").ToString
                    txtTolleranzaMenoValPrev7Pz5.Text = propTolleranzaMenopropValorePrevistoQuintoSetteNew
                    propTolleranzaMenopropValorePrevistoQuintoOttoNew = datareader("TolleranzaMenoValorePrevistoQuintoOtto").ToString
                    txtTolleranzaMenoValPrev8Pz5.Text = propTolleranzaMenopropValorePrevistoQuintoOttoNew
                    propTolleranzaMenopropValorePrevistoQuintoNoveNew = datareader("TolleranzaMenoValorePrevistoQuintoNove").ToString
                    txtTolleranzaMenoValPrev9Pz5.Text = propTolleranzaMenopropValorePrevistoQuintoNoveNew
                    propTolleranzaMenopropValorePrevistoQuintoDieciNew = datareader("TolleranzaMenoValorePrevistoQuintoDieci").ToString
                    txtTolleranzaMenoValPrev10Pz5.Text = propTolleranzaMenopropValorePrevistoQuintoDieciNew

                    propNoteQuintoPezzoNew = datareader("NoteQuintoPezzo").ToString
                    txtNotePz5.Text = propNoteQuintoPezzoNew

                End While


            End Using


        Catch ex As Exception
            MessageBox.Show("Errore : " & ex.Message)
        End Try


    End Sub


#End Region

#Region "Carica Dati UltimoPezzo Nuovo da Update By IDIntestazione"


    Public Sub getUltimoPezzoByIdIntestazioneNew(intIdItestazione As Integer)

        Dim cmd As DbCommand = _db.GetStoredProcCommand("spGetUltimoPezzoByIdIntestazione")

        Try
            _db.AddInParameter(cmd, "IDIntestazione", DbType.Int32, intIdItestazione)

            Using datareader As IDataReader = _db.ExecuteReader(cmd)

                While datareader.Read

                    propIDPezziUltimoPezzoNew = CType(datareader("IDPezziUltimoPezzo"), Integer)

                    propValorePrevistoUltimoUnoNew = datareader("ValorePrevistoUltimoPezzoUno").ToString
                    txtValPrev1UltimoPz.Text = propValorePrevistoUltimoUnoNew
                    propValorePrevistoUltimoDueNew = datareader("ValorePrevistoUltimoPezzoDue").ToString
                    txtValPrev2UltimoPz.Text = propValorePrevistoUltimoDueNew
                    propValorePrevistoUltimoTreNew = datareader("ValorePrevistoUltimoPezzoTre").ToString
                    txtValPrev3UltimoPz.Text = propValorePrevistoUltimoTreNew
                    propValorePrevistoUltimoQuattroNew = datareader("ValorePrevistoUltimoPezzoQuattro").ToString
                    txtValPrev4UltimoPz.Text = propValorePrevistoUltimoQuattroNew
                    propValorePrevistoUltimoCinqueNew = datareader("ValorePrevistoUltimoPezzoCinque").ToString
                    txtValPrev5UltimoPz.Text = propValorePrevistoUltimoCinqueNew
                    propValorePrevistoUltimoSeiNew = datareader("ValorePrevistoUltimoPezzoSei").ToString
                    txtValPrev6UltimoPz.Text = propValorePrevistoUltimoSeiNew
                    propValorePrevistoUltimoSetteNew = datareader("ValorePrevistoUltimoPezzoSette").ToString
                    txtValPrev7UltimoPz.Text = propValorePrevistoUltimoSetteNew
                    propValorePrevistoUltimoOttoNew = datareader("ValorePrevistoUltimoPezzoOtto").ToString
                    txtValPrev8UltimoPz.Text = propValorePrevistoUltimoOttoNew
                    propValorePrevistoUltimoNoveNew = datareader("ValorePrevistoUltimoPezzoNove").ToString
                    txtValPrev9UltimoPz.Text = propValorePrevistoUltimoNoveNew
                    propValorePrevistoUltimoDieciNew = datareader("ValorePrevistoUltimoPezzoDieci").ToString
                    txtValPrev10UltimoPz.Text = propValorePrevistoUltimoDieciNew

                    propValoreMisuratoUltimoUnoNew = datareader("ValoreMisuratoUltimoPezzoUno").ToString
                    txt1ValMisUltimoPz.Text = propValoreMisuratoUltimoUnoNew
                    propValoreMisuratoUltimoDueNew = datareader("ValoreMisuratoUltimoPezzoDue").ToString
                    txt2ValMisUltimoPz.Text = propValoreMisuratoUltimoDueNew
                    propValoreMisuratoUltimoTreNew = datareader("ValoreMisuratoUltimoPezzoTre").ToString
                    txt3ValMisUltimoPz.Text = propValoreMisuratoUltimoTreNew
                    propValoreMisuratoUltimoQuattroNew = datareader("ValoreMisuratoUltimoPezzoQuattro").ToString
                    txt4ValMisUltimoPz.Text = propValoreMisuratoUltimoQuattroNew
                    propValoreMisuratoUltimoCinqueNew = datareader("ValoreMisuratoUltimoPezzoCinque").ToString
                    txt5ValMisUltimoPz.Text = propValoreMisuratoUltimoCinqueNew
                    propValoreMisuratoUltimoSeiNew = datareader("ValoreMisuratoUltimoPezzoSei").ToString
                    txt6ValMisUltimoPz.Text = propValoreMisuratoUltimoSeiNew
                    propValoreMisuratoUltimoSetteNew = datareader("ValoreMisuratoUltimoPezzoSette").ToString
                    txt7ValMisUltimoPz.Text = propValoreMisuratoUltimoSetteNew
                    propValoreMisuratoUltimoOttoNew = datareader("ValoreMisuratoUltimoPezzoOtto").ToString
                    txt8ValMisUltimoPz.Text = propValoreMisuratoUltimoOttoNew
                    propValoreMisuratoUltimoNoveNew = datareader("ValoreMisuratoUltimoPezzoNove").ToString
                    txt9ValMisUltimoPz.Text = propValoreMisuratoUltimoNoveNew
                    propValoreMisuratoUltimoDieciNew = datareader("ValoreMisuratoUltimoPezzoDieci").ToString
                    txt10ValMisUltimoPz.Text = propValoreMisuratoUltimoDieciNew

                    propTolleranzaPiupropValorePrevistoUltimoUnoNew = datareader("TolleranzaPiuValorePrevistoUltimoUno").ToString
                    txtTolleranzaPiuValPrev1UltimoPz.Text = propTolleranzaPiupropValorePrevistoUltimoUnoNew
                    propTolleranzaPiupropValorePrevistoUltimoDueNew = datareader("TolleranzaPiuValorePrevistoUltimoDue").ToString
                    txtTolleranzaPiuValPrev2UltimoPz.Text = propTolleranzaPiupropValorePrevistoUltimoDueNew
                    propTolleranzaPiupropValorePrevistoUltimoTreNew = datareader("TolleranzaPiuValorePrevistoUltimoTre").ToString
                    txtTolleranzaPiuValPrev3UltimoPz.Text = propTolleranzaPiupropValorePrevistoUltimoTreNew
                    propTolleranzaPiupropValorePrevistoUltimoQuattroNew = datareader("TolleranzaPiuValorePrevistoUltimoQuattro").ToString
                    txtTolleranzaPiuValPrev4UltimoPz.Text = propTolleranzaPiupropValorePrevistoUltimoQuattroNew
                    propTolleranzaPiupropValorePrevistoUltimoCinqueNew = datareader("TolleranzaPiuValorePrevistoUltimoCinque").ToString
                    txtTolleranzaPiuValPrev5UltimoPz.Text = propTolleranzaPiupropValorePrevistoUltimoCinqueNew
                    propTolleranzaPiupropValorePrevistoUltimoSeiNew = datareader("TolleranzaPiuValorePrevistoUltimoSei").ToString
                    txtTolleranzaPiuValPrev6UltimoPz.Text = propTolleranzaPiupropValorePrevistoUltimoSeiNew
                    propTolleranzaPiupropValorePrevistoUltimoSetteNew = datareader("TolleranzaPiuValorePrevistoUltimoSette").ToString
                    txtTolleranzaPiuValPrev7UltimoPz.Text = propTolleranzaPiupropValorePrevistoUltimoSetteNew
                    propTolleranzaPiupropValorePrevistoUltimoOttoNew = datareader("TolleranzaPiuValorePrevistoUltimoOtto").ToString
                    txtTolleranzaPiuValPrev8UltimoPz.Text = propTolleranzaPiupropValorePrevistoUltimoOttoNew
                    propTolleranzaPiupropValorePrevistoUltimoNoveNew = datareader("TolleranzaPiuValorePrevistoUltimoNove").ToString
                    txtTolleranzaPiuValPrev9UltimoPz.Text = propTolleranzaPiupropValorePrevistoUltimoNoveNew
                    propTolleranzaPiupropValorePrevistoUltimoDieciNew = datareader("TolleranzaPiuValorePrevistoUltimoDieci").ToString
                    txtTolleranzaPiuValPrev10UltimoPz.Text = propTolleranzaPiupropValorePrevistoUltimoDieciNew

                    propTolleranzaMenopropValorePrevistoUltimoUnoNew = datareader("TolleranzaMenoValorePrevistoUltimoUno").ToString
                    txtTolleranzaMenoValPrev1UltimoPz.Text = propTolleranzaMenopropValorePrevistoUltimoUnoNew
                    propTolleranzaMenopropValorePrevistoUltimoDueNew = datareader("TolleranzaMenoValorePrevistoUltimoDue").ToString
                    txtTolleranzaMenoValPrev2UltimoPz.Text = propTolleranzaMenopropValorePrevistoUltimoDueNew
                    propTolleranzaMenopropValorePrevistoUltimoTreNew = datareader("TolleranzaMenoValorePrevistoUltimoTre").ToString
                    txtTolleranzaMenoValPrev3UltimoPz.Text = propTolleranzaMenopropValorePrevistoUltimoTreNew
                    propTolleranzaMenopropValorePrevistoUltimoQuattroNew = datareader("TolleranzaMenoValorePrevistoUltimoQuattro").ToString
                    txtTolleranzaMenoValPrev4UltimoPz.Text = propTolleranzaMenopropValorePrevistoUltimoQuattroNew
                    propTolleranzaMenopropValorePrevistoUltimoCinqueNew = datareader("TolleranzaMenoValorePrevistoUltimoCinque").ToString
                    txtTolleranzaMenoValPrev5UltimoPz.Text = propTolleranzaMenopropValorePrevistoUltimoCinqueNew
                    propTolleranzaMenopropValorePrevistoUltimoSeiNew = datareader("TolleranzaMenoValorePrevistoUltimoSei").ToString
                    txtTolleranzaMenoValPrev6UltimoPz.Text = propTolleranzaMenopropValorePrevistoUltimoSeiNew
                    propTolleranzaMenopropValorePrevistoUltimoSetteNew = datareader("TolleranzaMenoValorePrevistoUltimoSette").ToString
                    txtTolleranzaMenoValPrev7UltimoPz.Text = propTolleranzaMenopropValorePrevistoUltimoSetteNew
                    propTolleranzaMenopropValorePrevistoUltimoOttoNew = datareader("TolleranzaMenoValorePrevistoUltimoOtto").ToString
                    txtTolleranzaMenoValPrev8UltimoPz.Text = propTolleranzaMenopropValorePrevistoUltimoOttoNew
                    propTolleranzaMenopropValorePrevistoUltimoNoveNew = datareader("TolleranzaMenoValorePrevistoUltimoNove").ToString
                    txtTolleranzaMenoValPrev9UltimoPz.Text = propTolleranzaMenopropValorePrevistoUltimoNoveNew
                    propTolleranzaMenopropValorePrevistoUltimoDieciNew = datareader("TolleranzaMenoValorePrevistoUltimoDieci").ToString
                    txtTolleranzaMenoValPrev10UltimoPz.Text = propTolleranzaMenopropValorePrevistoUltimoDieciNew

                    propNoteUltimoPezzoNew = datareader("NoteUltimoPezzo").ToString
                    txtNoteUltimoPz.Text = propNoteUltimoPezzoNew

                End While


            End Using


        Catch ex As Exception
            MessageBox.Show("Errore : " & ex.Message)
        End Try


    End Sub



#End Region


    '-----------------------------------------------------------------------------------------------------------

#Region "SALVA NUOVO REPORT DA REPORT ESISTENTE"


#Region "Salva Testata Nuovo"

    Public Function SalvaTestataTestReportBusLayer() As Integer

        Dim retTestata As Integer?
        Dim intContatore As Integer = CType(txtIdIntestazione.Text, Integer)
        propIdIntestazione = intContatore
        ' Dim result As DialogResult
        Dim materiale As String
        Dim strumento As String
        Dim macchinaNum As String
        Dim fornitore As String
        Dim operatore As String
        Dim pezzoNumero As Nullable(Of Integer)
        Dim numPezzi As Nullable(Of Integer)
        Dim rigaOrdNum As Nullable(Of Integer)

        propIdTestataDaReportEsistente = intContatore

        Try

            materiale = cmbMateriale.Text
            strumento = cmbStrumento.Text
            macchinaNum = cmbMacchNum.Text
            fornitore = cmbFornitore.Text
            operatore = cmbOperatore.Text


            Dim iDIntestazione As Integer = propIdTestataDaReportEsistente
            Dim numOrdine As String = txtNumOrdine.Text
            Dim codiceArticolo As String = txtCodArt.Text
            Dim data As Date = Convert(dtpData.Text)

            Dim numLotto As String = cmbLottoNum.Text
            Dim primoPezzo As Boolean = chkPrimoPezzo.Checked
            Dim ultimoPezzo As Boolean = chkUltimoPezzo.Checked


            If txtPezziControllati.Text = Nothing Then
                pezzoNumero = 0
            Else
                pezzoNumero = CType(txtPezziControllati.Text, Integer)
            End If

            If txtRigaNum.Text = Nothing Then
                rigaOrdNum = 0
            Else
                rigaOrdNum = CType(txtRigaNum.Text, Integer)
            End If

            If txtPezziNum.Text = Nothing Then
                numPezzi = 0
            Else
                numPezzi = CType(txtPezziNum.Text, Integer)
            End If

            retTestata = insertTestataReport(iDIntestazione, numOrdine, codiceArticolo, data, materiale, strumento,
                               macchinaNum, rigaOrdNum, numPezzi, fornitore, numLotto, primoPezzo, ultimoPezzo, pezzoNumero, operatore)

            If Not retTestata Is Nothing Then
                Return retTestata
            End If

        Catch ex As Exception
            MessageBox.Show("Errore SalvaTestataTestReportBusLayer :" & ex.Message)
        End Try

    End Function

    Public Function insertTestataReport(idIntestazione As Integer, numOrdine As String,
                                        codArticolo As String, data As Date, materiale As String,
                                        strumento As String,
                                        macchinanum As String, rigaordnum As Integer,
                                        numpezzi As Integer, fornitore As String, numlotto As String,
                                        primopezzo As Boolean, ultimopezzo As Boolean,
                                        pezzonumero As Integer, operatore As String) As Integer

        Dim insertCommand As DbCommand = Nothing

        Dim rowsAffected As Integer
        Dim dataShorth As Date = Convert(data)
        Dim curFileNameEnricoPath As String
        Dim curFileNameEdoPath As String
        Dim curFileNameLorePath As String
        Dim curFileNameManuPath As String
        Dim curFileNameGalloPath As String
        Dim curFileNameFontaPath As String
        Dim curFileNameSimoPath As String
        Dim curFileNoFirma As String

        Dim fsEnrico As FileStream
        Dim fsEdo As FileStream
        Dim fsLore As FileStream
        Dim fsManu As FileStream
        Dim fsGallo As FileStream
        Dim fsFonta As FileStream
        Dim fsNoFirma As FileStream
        Dim fsSimo As FileStream

        ' Dim imageAsBytes As Byte()

        If operatore = "Grigioni Enrico" Then
            Dim pathFirmaEnricoApp As String = Application.StartupPath
            Dim pathFirmaEnrico As String = pathFirmaEnricoApp.Replace("\bin\Debug", "\Immagini\firmaEnricoBuona.png")
            curFileNameEnricoPath = pathFirmaEnrico
            fsEnrico = New System.IO.FileStream(curFileNameEnricoPath, FileMode.Open)
            propImageAsBytes = New Byte(fsEnrico.Length - 1) {}
            fsEnrico.Read(propImageAsBytes, 0, propImageAsBytes.Length)
            fsEnrico.Close()
        End If

        If operatore = "Grigioni Edoardo" Then
            Dim pathFirmaEdoApp As String = Application.StartupPath
            Dim pathFirmaEdo As String = pathFirmaEdoApp.Replace("\bin\Debug", "\Immagini\firmaEdoardoBuona.png")
            curFileNameEdoPath = pathFirmaEdo
            fsEdo = New System.IO.FileStream(curFileNameEdoPath, FileMode.Open)
            propImageAsBytes = New Byte(fsEdo.Length - 1) {}
            fsEdo.Read(propImageAsBytes, 0, propImageAsBytes.Length)
            fsEdo.Close()
        End If


        If operatore = "Baldi Lorenzo" Then
            Dim pathFirmaLorenzoApp As String = Application.StartupPath
            Dim pathFirmaLorenzo As String = pathFirmaLorenzoApp.Replace("\bin\Debug", "\Immagini\firmaLoreBuona.png")
            curFileNameLorePath = pathFirmaLorenzo
            fsLore = New System.IO.FileStream(curFileNameLorePath, FileMode.Open)
            propImageAsBytes = New Byte(fsLore.Length - 1) {}
            fsLore.Read(propImageAsBytes, 0, propImageAsBytes.Length)
            fsLore.Close()
        End If

        If operatore = "Bini Emanuele" Then
            Dim pathFirmaManuApp As String = Application.StartupPath
            Dim pathFirmaManu As String = pathFirmaManuApp.Replace("\bin\Debug", "\Immagini\firmaManuBuona.png")
            curFileNameManuPath = pathFirmaManu
            fsManu = New System.IO.FileStream(curFileNameManuPath, FileMode.Open)
            propImageAsBytes = New Byte(fsManu.Length - 1) {}
            fsManu.Read(propImageAsBytes, 0, propImageAsBytes.Length)
            fsManu.Close()
        End If

        If operatore = "Galletti Adriano" Then
            Dim pathFirmaAdrianoApp As String = Application.StartupPath
            Dim pathFirmaAdriano As String = pathFirmaAdrianoApp.Replace("\bin\Debug", "\Immagini\firmaGalloBuona.png")
            curFileNameGalloPath = pathFirmaAdriano
            fsGallo = New System.IO.FileStream(curFileNameGalloPath, FileMode.Open)
            propImageAsBytes = New Byte(fsGallo.Length - 1) {}
            fsGallo.Read(propImageAsBytes, 0, propImageAsBytes.Length)
            fsGallo.Close()
        End If

        If operatore = "Fontanelli Francesco" Then
            Dim pathFirmaFrancescoApp As String = Application.StartupPath
            Dim pathFirmaFrancesco As String = pathFirmaFrancescoApp.Replace("\bin\Debug", "\Immagini\firmaFontaBuona.png")
            curFileNameFontaPath = pathFirmaFrancesco
            fsFonta = New System.IO.FileStream(curFileNameFontaPath, FileMode.Open)
            propImageAsBytes = New Byte(fsFonta.Length - 1) {}
            fsFonta.Read(propImageAsBytes, 0, propImageAsBytes.Length)
            fsFonta.Close()
        End If

        If operatore = "Orlando Simone" Then
            Dim pathFirmaSimoneApp As String = Application.StartupPath
            Dim pathFirmaSimone As String = pathFirmaSimoneApp.Replace("\bin\Debug", "\Immagini\firmaSimoneBuona.png")
            curFileNameSimoPath = pathFirmaSimone
            fsSimo = New System.IO.FileStream(curFileNameSimoPath, FileMode.Open)
            propImageAsBytes = New Byte(fsSimo.Length - 1) {}
            fsSimo.Read(propImageAsBytes, 0, propImageAsBytes.Length)
            fsSimo.Close()
        End If

        If operatore = String.Empty Then
            Dim pathFirmaNoUserApp As String = Application.StartupPath
            Dim pathFirmaNoUser As String = pathFirmaNoUserApp.Replace("\bin\Debug", "\Immagini\noFirma.gif")
            curFileNoFirma = pathFirmaNoUser
            fsNoFirma = New System.IO.FileStream(curFileNoFirma, FileMode.Open)
            propImageAsBytes = New Byte(fsNoFirma.Length - 1) {}
            fsNoFirma.Read(propImageAsBytes, 0, propImageAsBytes.Length)
            fsNoFirma.Close()
        End If



        Dim strQuery As String = "INSERT INTO tblIntestazione " &
                      "(IDIntestazione, NumOrdine, CodArticolo, " &
                      "Data, Materiale, Strumento," &
                      "MacchinaNum, RigaOrdNum, NumPezzi, Fornitore, " &
                      "NumLotto, PrimoPezzo, UltimoPezzo, PezzoNumero,Operatore ,imgFirmaOperatore ) " &
                      " VALUES " &
                      "(@IDIntestazione, @NumOrdine, @CodArticolo, @Data," &
                      " @Materiale, @Strumento, @MacchinaNum," &
                      " @RigaOrdNum, @NumPezzi, @Fornitore, @NumLotto," &
                      " @PrimoPezzo, @UltimoPezzo, @PezzoNumero, @Operatore ,@imgFirmaOperatore)"


        Try

            insertCommand = _db.GetSqlStringCommand(strQuery)

            _db.AddInParameter(insertCommand, "IDIntestazione", DbType.Int32, idIntestazione)
            _db.AddInParameter(insertCommand, "NumOrdine", DbType.String, numOrdine)
            _db.AddInParameter(insertCommand, "CodArticolo", DbType.String, codArticolo)
            _db.AddInParameter(insertCommand, "Data", DbType.Date, dataShorth)
            _db.AddInParameter(insertCommand, "Materiale", DbType.String, materiale)
            _db.AddInParameter(insertCommand, "Strumento", DbType.String, strumento)
            _db.AddInParameter(insertCommand, "MacchinaNum", DbType.String, macchinanum)
            _db.AddInParameter(insertCommand, "RigaOrdNum", DbType.Int32, rigaordnum)
            _db.AddInParameter(insertCommand, "NumPezzi", DbType.Int32, numpezzi)
            _db.AddInParameter(insertCommand, "Fornitore", DbType.String, fornitore)
            _db.AddInParameter(insertCommand, "NumLotto", DbType.String, numlotto)
            _db.AddInParameter(insertCommand, "PrimoPezzo", DbType.Boolean, primopezzo)
            _db.AddInParameter(insertCommand, "UltimoPezzo", DbType.Boolean, ultimopezzo)
            _db.AddInParameter(insertCommand, "PezzoNumero", DbType.Int32, pezzonumero)
            _db.AddInParameter(insertCommand, "Operatore", DbType.String, operatore)
            _db.AddInParameter(insertCommand, "imgFirmaOperatore", DbType.Binary, propImageAsBytes)

            rowsAffected = _db.ExecuteNonQuery(insertCommand)

        Catch ex As Exception
            MessageBox.Show("Errore insertTestataReport : " & ex.Message)
        End Try

        Return rowsAffected

    End Function

#End Region

#Region "Salva Primo Pezzo Nuovo"


    Public Function SalvaPrimoPezzo() As Integer

        Try
            Dim intContatorePU As Integer = propIdTestataDaReportEsistente


            Dim IDPezziPrimoPezzo As Integer = intContatorePU
            Dim IDIntestazione As Integer = intContatorePU
            Dim ValorePrevistoPrimoPezzoUno As String = txtValPrev1PrimoPz.Text
            Dim ValorePrevistoPrimoPezzoDue As String = txtValPrev2PrimoPz.Text
            Dim ValorePrevistoPrimoPezzoTre As String = txtValPrev3PrimoPz.Text
            Dim ValorePrevistoPrimoPezzoQuattro As String = txtValPrev4PrimoPz.Text
            Dim ValorePrevistoPrimoPezzoCinque As String = txtValPrev5PrimoPz.Text
            Dim ValorePrevistoPrimoPezzoSei As String = txtValPrev6PrimoPz.Text
            Dim ValorePrevistoPrimoPezzoSette As String = txtValPrev7PrimoPz.Text
            Dim ValorePrevistoPrimoPezzoOtto As String = txtValPrev8PrimoPz.Text
            Dim ValorePrevistoPrimoPezzoNove As String = txtValPrev9PrimoPz.Text
            Dim ValorePrevistoPrimoPezzoDieci As String = txtValPrev10PrimoPz.Text

            Dim ValoreMisuratoPrimoPezzoUno As String = txt1ValMisPrimoPz.Text
            Dim ValoreMisuratoPrimoPezzoDue As String = txt2ValMisPrimoPz.Text
            Dim ValoreMisuratoPrimoPezzoTre As String = txt3ValMisPrimoPz.Text
            Dim ValoreMisuratoPrimoPezzoQuattro As String = txt4ValMisPrimoPz.Text
            Dim ValoreMisuratoPrimoPezzoCinque As String = txt5ValMisPrimoPz.Text
            Dim ValoreMisuratoPrimoPezzoSei As String = txt6ValMisPrimoPz.Text
            Dim ValoreMisuratoPrimoPezzoSette As String = txt7ValMisPrimoPz.Text
            Dim ValoreMisuratoPrimoPezzoOtto As String = txt8ValMisPrimoPz.Text
            Dim ValoreMisuratoPrimoPezzoNove As String = txt9ValMisPrimoPz.Text
            Dim ValoreMisuratoPrimoPezzoDieci As String = txt10ValMisPrimoPz.Text

            Dim TolleranzaPiuValorePrevistoPrimoUno As String = txtTolleranzaPiuValPrev1PrimoPz.Text
            Dim TolleranzaPiuValorePrevistoPrimoDue As String = txtTolleranzaPiuValPrev2PrimoPz.Text
            Dim TolleranzaPiuValorePrevistoPrimoTre As String = txtTolleranzaPiuValPrev3PrimoPz.Text
            Dim TolleranzaPiuValorePrevistoPrimoQuattro As String = txtTolleranzaPiuValPrev4PrimoPz.Text
            Dim TolleranzaPiuValorePrevistoPrimoCinque As String = txtTolleranzaPiuValPrev5PrimoPz.Text
            Dim TolleranzaPiuValorePrevistoPrimoSei As String = txtTolleranzaPiuValPrev6PrimoPz.Text
            Dim TolleranzaPiuValorePrevistoPrimoSette As String = txtTolleranzaPiuValPrev7PrimoPz.Text
            Dim TolleranzaPiuValorePrevistoPrimoOtto As String = txtTolleranzaPiuValPrev8PrimoPz.Text
            Dim TolleranzaPiuValorePrevistoPrimoNove As String = txtTolleranzaPiuValPrev9PrimoPz.Text
            Dim TolleranzaPiuValorePrevistoPrimoDieci As String = txtTolleranzaPiuValPrev10PrimoPz.Text

            Dim TolleranzaMenoValorePrevistoPrimoUno As String = txtTolleranzaMenoValPrev1PrimoPz.Text
            Dim TolleranzaMenoValorePrevistoPrimoDue As String = txtTolleranzaMenoValPrev2PrimoPz.Text
            Dim TolleranzaMenoValorePrevistoPrimoTre As String = txtTolleranzaMenoValPrev3PrimoPz.Text
            Dim TolleranzaMenoValorePrevistoPrimoQuattro As String = txtTolleranzaMenoValPrev4PrimoPz.Text
            Dim TolleranzaMenoValorePrevistoPrimoCinque As String = txtTolleranzaMenoValPrev5PrimoPz.Text
            Dim TolleranzaMenoValorePrevistoPrimoSei As String = txtTolleranzaMenoValPrev6PrimoPz.Text
            Dim TolleranzaMenoValorePrevistoPrimoSette As String = txtTolleranzaMenoValPrev7PrimoPz.Text
            Dim TolleranzaMenoValorePrevistoPrimoOtto As String = txtTolleranzaMenoValPrev8PrimoPz.Text
            Dim TolleranzaMenoValorePrevistoPrimoNove As String = txtTolleranzaMenoValPrev9PrimoPz.Text
            Dim TolleranzaMenoValorePrevistoPrimoDieci As String = txtTolleranzaMenoValPrev10PrimoPz.Text

            Dim NotePrimoPezzo As String = txtNotePrimoPz.Text

            Dim retPrimoPezzo As Nullable(Of Integer) = InsertPezziPrimoPezzo(IDPezziPrimoPezzo, IDIntestazione, ValorePrevistoPrimoPezzoUno,
                                    ValorePrevistoPrimoPezzoDue, ValorePrevistoPrimoPezzoTre, ValorePrevistoPrimoPezzoQuattro,
                                    ValorePrevistoPrimoPezzoCinque, ValorePrevistoPrimoPezzoSei, ValorePrevistoPrimoPezzoSette,
                                    ValorePrevistoPrimoPezzoOtto, ValorePrevistoPrimoPezzoNove, ValorePrevistoPrimoPezzoDieci,
                                    ValoreMisuratoPrimoPezzoUno, ValoreMisuratoPrimoPezzoDue, ValoreMisuratoPrimoPezzoTre,
                                    ValoreMisuratoPrimoPezzoQuattro, ValoreMisuratoPrimoPezzoCinque, ValoreMisuratoPrimoPezzoSei,
                                    ValoreMisuratoPrimoPezzoSette, ValoreMisuratoPrimoPezzoOtto, ValoreMisuratoPrimoPezzoNove,
                                    ValoreMisuratoPrimoPezzoDieci,
                                    TolleranzaPiuValorePrevistoPrimoUno,
                                    TolleranzaPiuValorePrevistoPrimoDue,
                                    TolleranzaPiuValorePrevistoPrimoTre,
                                    TolleranzaPiuValorePrevistoPrimoQuattro,
                                    TolleranzaPiuValorePrevistoPrimoCinque,
                                    TolleranzaPiuValorePrevistoPrimoSei,
                                    TolleranzaPiuValorePrevistoPrimoSette,
                                    TolleranzaPiuValorePrevistoPrimoOtto,
                                    TolleranzaPiuValorePrevistoPrimoNove,
                                    TolleranzaPiuValorePrevistoPrimoDieci,
                                    TolleranzaMenoValorePrevistoPrimoUno,
                                    TolleranzaMenoValorePrevistoPrimoDue,
                                    TolleranzaMenoValorePrevistoPrimoTre,
                                    TolleranzaMenoValorePrevistoPrimoQuattro,
                                    TolleranzaMenoValorePrevistoPrimoCinque,
                                    TolleranzaMenoValorePrevistoPrimoSei,
                                    TolleranzaMenoValorePrevistoPrimoSette,
                                    TolleranzaMenoValorePrevistoPrimoOtto,
                                    TolleranzaMenoValorePrevistoPrimoNove,
                                    TolleranzaMenoValorePrevistoPrimoDieci,
                                    NotePrimoPezzo)

            If Not retPrimoPezzo Is Nothing Then
                Return retPrimoPezzo
            Else
                Exit Function
            End If

        Catch ex As Exception
            MessageBox.Show("Errore SalvaPrimoPezzo :" & ex.Message)
        End Try

    End Function


    Public Function InsertPezziPrimoPezzo(IDPezziPrimoPezzo As Integer, IDIntestazione As Integer, ValorePrevistoPrimoPezzoUno As String,
ValorePrevistoPrimoPezzoDue As String, ValorePrevistoPrimoPezzoTre As String,
ValorePrevistoPrimoPezzoQuattro As String, ValorePrevistoPrimoPezzoCinque As String,
ValorePrevistoPrimoPezzoSei As String, ValorePrevistoPrimoPezzoSette As String,
ValorePrevistoPrimoPezzoOtto As String, ValorePrevistoPrimoPezzoNove As String,
ValorePrevistoPrimoPezzoDieci As String, ValoreMisuratoPrimoPezzoUno As String,
ValoreMisuratoPrimoPezzoDue As String, ValoreMisuratoPrimoPezzoTre As String,
ValoreMisuratoPrimoPezzoQuattro As String, ValoreMisuratoPrimoPezzoCinque As String,
ValoreMisuratoPrimoPezzoSei As String, ValoreMisuratoPrimoPezzoSette As String,
ValoreMisuratoPrimoPezzoOtto As String, ValoreMisuratoPrimoPezzoNove As String,
ValoreMisuratoPrimoPezzoDieci As String,
TolleranzaPiuValorePrevistoPrimoUno As String,
TolleranzaPiuValorePrevistoPrimoDue As String,
TolleranzaPiuValorePrevistoPrimoTre As String,
TolleranzaPiuValorePrevistoPrimoQuattro As String,
TolleranzaPiuValorePrevistoPrimoCinque As String,
TolleranzaPiuValorePrevistoPrimoSei As String,
TolleranzaPiuValorePrevistoPrimoSette As String,
TolleranzaPiuValorePrevistoPrimoOtto As String,
TolleranzaPiuValorePrevistoPrimoNove As String,
TolleranzaPiuValorePrevistoPrimoDieci As String,
TolleranzaMenoValorePrevistoPrimoUno As String,
TolleranzaMenoValorePrevistoPrimoDue As String,
TolleranzaMenoValorePrevistoPrimoTre As String,
TolleranzaMenoValorePrevistoPrimoQuattro As String,
TolleranzaMenoValorePrevistoPrimoCinque As String,
TolleranzaMenoValorePrevistoPrimoSei As String,
TolleranzaMenoValorePrevistoPrimoSette As String,
TolleranzaMenoValorePrevistoPrimoOtto As String,
TolleranzaMenoValorePrevistoPrimoNove As String,
TolleranzaMenoValorePrevistoPrimoDieci As String,
NotePrimoPezzo As String) As Integer

        Dim insertCommandPrimoPezzo As DbCommand = Nothing
        Dim rowsAffectedPrimoPezzo As Integer = Nothing

        Dim strQuery As String = "INSERT INTO tblPezziPrimoPezzo " &
            "([IDPezziPrimoPezzo], [IDIntestazione], [ValorePrevistoPrimoPezzoUno], " &
            "[ValorePrevistoPrimoPezzoDue], [ValorePrevistoPrimoPezzoTre], [ValorePrevistoPrimoPezzoQuattro], " &
            "[ValorePrevistoPrimoPezzoCinque], [ValorePrevistoPrimoPezzoSei], [ValorePrevistoPrimoPezzoSette], " &
            "[ValorePrevistoPrimoPezzoOtto], [ValorePrevistoPrimoPezzoNove], [ValorePrevistoPrimoPezzoDieci], " &
            "[ValoreMisuratoPrimoPezzoUno], [ValoreMisuratoPrimoPezzoDue], [ValoreMisuratoPrimoPezzoTre], " &
            "[ValoreMisuratoPrimoPezzoQuattro], [ValoreMisuratoPrimoPezzoCinque], [ValoreMisuratoPrimoPezzoSei], " &
            "[ValoreMisuratoPrimoPezzoSette], [ValoreMisuratoPrimoPezzoOtto], [ValoreMisuratoPrimoPezzoNove], " &
            "[ValoreMisuratoPrimoPezzoDieci], " &
            "[TolleranzaPiuValorePrevistoPrimoUno], " &
            "[TolleranzaPiuValorePrevistoPrimoDue], " &
            "[TolleranzaPiuValorePrevistoPrimoTre], " &
            "[TolleranzaPiuValorePrevistoPrimoQuattro], " &
            "[TolleranzaPiuValorePrevistoPrimoCinque], " &
            "[TolleranzaPiuValorePrevistoPrimoSei], " &
            "[TolleranzaPiuValorePrevistoPrimoSette], " &
            "[TolleranzaPiuValorePrevistoPrimoOtto], " &
            "[TolleranzaPiuValorePrevistoPrimoNove], " &
            "[TolleranzaPiuValorePrevistoPrimoDieci], " &
            "[TolleranzaMenoValorePrevistoPrimoUno], " &
            "[TolleranzaMenoValorePrevistoPrimoDue], " &
            "[TolleranzaMenoValorePrevistoPrimoTre], " &
            "[TolleranzaMenoValorePrevistoPrimoQuattro], " &
            "[TolleranzaMenoValorePrevistoPrimoCinque], " &
            "[TolleranzaMenoValorePrevistoPrimoSei], " &
            "[TolleranzaMenoValorePrevistoPrimoSette], " &
            "[TolleranzaMenoValorePrevistoPrimoOtto], " &
            "[TolleranzaMenoValorePrevistoPrimoNove], " &
            "[TolleranzaMenoValorePrevistoPrimoDieci], " &
            "[NotePrimoPezzo]) VALUES (@IDPezziPrimoPezzo, @IDIntestazione, @ValorePrevistoPrimoPezzoUno, " &
            "@ValorePrevistoPrimoPezzoDue, @ValorePrevistoPrimoPezzoTre, @ValorePrevistoPrimoPezzoQuattro, " &
            "@ValorePrevistoPrimoPezzoCinque, @ValorePrevistoPrimoPezzoSei, @ValorePrevistoPrimoPezzoSette, " &
            "@ValorePrevistoPrimoPezzoOtto, @ValorePrevistoPrimoPezzoNove, @ValorePrevistoPrimoPezzoDieci, " &
            "@ValoreMisuratoPrimoPezzoUno, @ValoreMisuratoPrimoPezzoDue, @ValoreMisuratoPrimoPezzoTre, " &
            "@ValoreMisuratoPrimoPezzoQuattro, @ValoreMisuratoPrimoPezzoCinque, @ValoreMisuratoPrimoPezzoSei, " &
            "@ValoreMisuratoPrimoPezzoSette, @ValoreMisuratoPrimoPezzoOtto, @ValoreMisuratoPrimoPezzoNove," &
            "@ValoreMisuratoPrimoPezzoDieci," &
            "@TolleranzaPiuValorePrevistoPrimoUno, " &
            "@TolleranzaPiuValorePrevistoPrimoDue, " &
            "@TolleranzaPiuValorePrevistoPrimoTre, " &
            "@TolleranzaPiuValorePrevistoPrimoQuattro, " &
            "@TolleranzaPiuValorePrevistoPrimoCinque, " &
            "@TolleranzaPiuValorePrevistoPrimoSei, " &
            "@TolleranzaPiuValorePrevistoPrimoSette, " &
            "@TolleranzaPiuValorePrevistoPrimoOtto, " &
            "@TolleranzaPiuValorePrevistoPrimoNove, " &
            "@TolleranzaPiuValorePrevistoPrimoDieci, " &
            "@TolleranzaMenoValorePrevistoPrimoUno, " &
            "@TolleranzaMenoValorePrevistoPrimoDue, " &
            "@TolleranzaMenoValorePrevistoPrimoTre, " &
            "@TolleranzaMenoValorePrevistoPrimoQuattro, " &
            "@TolleranzaMenoValorePrevistoPrimoCinque, " &
            "@TolleranzaMenoValorePrevistoPrimoSei, " &
            "@TolleranzaMenoValorePrevistoPrimoSette, " &
            "@TolleranzaMenoValorePrevistoPrimoOtto, " &
            "@TolleranzaMenoValorePrevistoPrimoNove, " &
            "@TolleranzaMenoValorePrevistoPrimoDieci, " &
            "@NotePrimoPezzo)"

        Try
            insertCommandPrimoPezzo = _db.GetSqlStringCommand(strQuery)

            _db.AddInParameter(insertCommandPrimoPezzo, "IDPezziPrimoPezzo", DbType.Int32, IDPezziPrimoPezzo)
            _db.AddInParameter(insertCommandPrimoPezzo, "IDIntestazione", DbType.Int32, IDIntestazione)
            _db.AddInParameter(insertCommandPrimoPezzo, "ValorePrevistoPrimoPezzoUno", DbType.String, ValorePrevistoPrimoPezzoUno)
            _db.AddInParameter(insertCommandPrimoPezzo, "ValorePrevistoPrimoPezzoDue", DbType.String, ValorePrevistoPrimoPezzoDue)
            _db.AddInParameter(insertCommandPrimoPezzo, "ValorePrevistoPrimoPezzoTre", DbType.String, ValorePrevistoPrimoPezzoTre)
            _db.AddInParameter(insertCommandPrimoPezzo, "ValorePrevistoPrimoPezzoQuattro", DbType.String, ValorePrevistoPrimoPezzoQuattro)
            _db.AddInParameter(insertCommandPrimoPezzo, "ValorePrevistoPrimoPezzoCinque", DbType.String, ValorePrevistoPrimoPezzoCinque)
            _db.AddInParameter(insertCommandPrimoPezzo, "ValorePrevistoPrimoPezzoSei", DbType.String, ValorePrevistoPrimoPezzoSei)
            _db.AddInParameter(insertCommandPrimoPezzo, "ValorePrevistoPrimoPezzoSette", DbType.String, ValorePrevistoPrimoPezzoSette)
            _db.AddInParameter(insertCommandPrimoPezzo, "ValorePrevistoPrimoPezzoOtto", DbType.String, ValorePrevistoPrimoPezzoOtto)
            _db.AddInParameter(insertCommandPrimoPezzo, "ValorePrevistoPrimoPezzoNove", DbType.String, ValorePrevistoPrimoPezzoNove)
            _db.AddInParameter(insertCommandPrimoPezzo, "ValorePrevistoPrimoPezzoDieci", DbType.String, ValorePrevistoPrimoPezzoDieci)
            _db.AddInParameter(insertCommandPrimoPezzo, "ValoreMisuratoPrimoPezzoUno", DbType.String, ValoreMisuratoPrimoPezzoUno)
            _db.AddInParameter(insertCommandPrimoPezzo, "ValoreMisuratoPrimoPezzoDue", DbType.String, ValoreMisuratoPrimoPezzoDue)
            _db.AddInParameter(insertCommandPrimoPezzo, "ValoreMisuratoPrimoPezzoTre", DbType.String, ValoreMisuratoPrimoPezzoTre)
            _db.AddInParameter(insertCommandPrimoPezzo, "ValoreMisuratoPrimoPezzoQuattro", DbType.String, ValoreMisuratoPrimoPezzoQuattro)
            _db.AddInParameter(insertCommandPrimoPezzo, "ValoreMisuratoPrimoPezzoCinque", DbType.String, ValoreMisuratoPrimoPezzoCinque)
            _db.AddInParameter(insertCommandPrimoPezzo, "ValoreMisuratoPrimoPezzoSei", DbType.String, ValoreMisuratoPrimoPezzoSei)
            _db.AddInParameter(insertCommandPrimoPezzo, "ValoreMisuratoPrimoPezzoSette", DbType.String, ValoreMisuratoPrimoPezzoSette)
            _db.AddInParameter(insertCommandPrimoPezzo, "ValoreMisuratoPrimoPezzoOtto", DbType.String, ValoreMisuratoPrimoPezzoOtto)
            _db.AddInParameter(insertCommandPrimoPezzo, "ValoreMisuratoPrimoPezzoNove", DbType.String, ValoreMisuratoPrimoPezzoNove)
            _db.AddInParameter(insertCommandPrimoPezzo, "ValoreMisuratoPrimoPezzoDieci", DbType.String, ValoreMisuratoPrimoPezzoDieci)


            _db.AddInParameter(insertCommandPrimoPezzo, "TolleranzaPiuValorePrevistoPrimoUno", DbType.String, TolleranzaPiuValorePrevistoPrimoUno)
            _db.AddInParameter(insertCommandPrimoPezzo, "TolleranzaPiuValorePrevistoPrimoDue", DbType.String, TolleranzaPiuValorePrevistoPrimoDue)
            _db.AddInParameter(insertCommandPrimoPezzo, "TolleranzaPiuValorePrevistoPrimoTre", DbType.String, TolleranzaPiuValorePrevistoPrimoTre)
            _db.AddInParameter(insertCommandPrimoPezzo, "TolleranzaPiuValorePrevistoPrimoQuattro", DbType.String, TolleranzaPiuValorePrevistoPrimoQuattro)
            _db.AddInParameter(insertCommandPrimoPezzo, "TolleranzaPiuValorePrevistoPrimoCinque", DbType.String, TolleranzaPiuValorePrevistoPrimoCinque)
            _db.AddInParameter(insertCommandPrimoPezzo, "TolleranzaPiuValorePrevistoPrimoSei", DbType.String, TolleranzaPiuValorePrevistoPrimoSei)
            _db.AddInParameter(insertCommandPrimoPezzo, "TolleranzaPiuValorePrevistoPrimoSette", DbType.String, TolleranzaPiuValorePrevistoPrimoSette)
            _db.AddInParameter(insertCommandPrimoPezzo, "TolleranzaPiuValorePrevistoPrimoOtto", DbType.String, TolleranzaPiuValorePrevistoPrimoOtto)
            _db.AddInParameter(insertCommandPrimoPezzo, "TolleranzaPiuValorePrevistoPrimoNove", DbType.String, TolleranzaPiuValorePrevistoPrimoNove)
            _db.AddInParameter(insertCommandPrimoPezzo, "TolleranzaPiuValorePrevistoPrimoDieci", DbType.String, TolleranzaPiuValorePrevistoPrimoDieci)
            _db.AddInParameter(insertCommandPrimoPezzo, "TolleranzaMenoValorePrevistoPrimoUno", DbType.String, TolleranzaMenoValorePrevistoPrimoUno)
            _db.AddInParameter(insertCommandPrimoPezzo, "TolleranzaMenoValorePrevistoPrimoDue", DbType.String, TolleranzaMenoValorePrevistoPrimoDue)
            _db.AddInParameter(insertCommandPrimoPezzo, "TolleranzaMenoValorePrevistoPrimoTre", DbType.String, TolleranzaMenoValorePrevistoPrimoTre)
            _db.AddInParameter(insertCommandPrimoPezzo, "TolleranzaMenoValorePrevistoPrimoQuattro", DbType.String, TolleranzaMenoValorePrevistoPrimoQuattro)
            _db.AddInParameter(insertCommandPrimoPezzo, "TolleranzaMenoValorePrevistoPrimoCinque", DbType.String, TolleranzaMenoValorePrevistoPrimoCinque)
            _db.AddInParameter(insertCommandPrimoPezzo, "TolleranzaMenoValorePrevistoPrimoSei", DbType.String, TolleranzaMenoValorePrevistoPrimoSei)
            _db.AddInParameter(insertCommandPrimoPezzo, "TolleranzaMenoValorePrevistoPrimoSette", DbType.String, TolleranzaMenoValorePrevistoPrimoSette)
            _db.AddInParameter(insertCommandPrimoPezzo, "TolleranzaMenoValorePrevistoPrimoOtto", DbType.String, TolleranzaMenoValorePrevistoPrimoOtto)
            _db.AddInParameter(insertCommandPrimoPezzo, "TolleranzaMenoValorePrevistoPrimoNove", DbType.String, TolleranzaMenoValorePrevistoPrimoNove)
            _db.AddInParameter(insertCommandPrimoPezzo, "TolleranzaMenoValorePrevistoPrimoDieci", DbType.String, TolleranzaMenoValorePrevistoPrimoDieci)
            _db.AddInParameter(insertCommandPrimoPezzo, "NotePrimoPezzo", DbType.String, NotePrimoPezzo)

            rowsAffectedPrimoPezzo = _db.ExecuteNonQuery(insertCommandPrimoPezzo)

        Catch ex As Exception
            MessageBox.Show("Errore InsertPezziPrimoPezzo : " & ex.Message)
        End Try

        Return rowsAffectedPrimoPezzo

    End Function

#End Region

#Region "Salva Secondo Pezzo Nuovo"

    Public Function insertIDSecondoPezzo() As Integer
        Dim intContatoreSecondoPezzo As Integer = propIdTestataDaReportEsistente
        Dim result As DialogResult

        Dim IDPezziSecondoPezzo As Integer = intContatoreSecondoPezzo
        Dim IDIntestazione As Integer = intContatoreSecondoPezzo

        Try
            Dim retSecondoPezzoSoloID As Nullable(Of Integer) = InsertSecondoPezzoSoloID(IDPezziSecondoPezzo, IDIntestazione)

            If Not retSecondoPezzoSoloID Is Nothing Then
                Return retSecondoPezzoSoloID
            End If
        Catch ex As Exception
            MessageBox.Show("Errore insertIDSecondoPezzo :" & ex.Message)
        End Try
    End Function

    Public Function InsertSecondoPezzoSoloID(IDPezziSecondoPezzo As Integer, IDIntestazione As Integer) As Integer


        Dim insertCommandSecondoPezzoSoloID As DbCommand = Nothing
        Dim rowsAffectedSecondoPezzoSoloID As Integer = Nothing

        Dim strQuery As String = "INSERT INTO [dbo].[tblPezziSecondoPezzo] ([IDPezziSecondoPezzo],[IDIntestazione]) VALUES (@IDPezziSecondoPezzo,@IDIntestazione)"

        Try
            insertCommandSecondoPezzoSoloID = _db.GetSqlStringCommand(strQuery)


            _db.AddInParameter(insertCommandSecondoPezzoSoloID, "IDPezziSecondoPezzo", DbType.Int32, IDPezziSecondoPezzo)
            _db.AddInParameter(insertCommandSecondoPezzoSoloID, "IDIntestazione", DbType.Int32, IDIntestazione)

            rowsAffectedSecondoPezzoSoloID = _db.ExecuteNonQuery(insertCommandSecondoPezzoSoloID)

        Catch ex As Exception
            MessageBox.Show("Errore InsertSecondoPezzoSoloID :" & ex.Message)
        End Try

        Return rowsAffectedSecondoPezzoSoloID


    End Function

    Public Function SalvaSecondoPezzo() As Integer

        Dim ValorePrevistoSecondoPezzoUno As String
        Dim ValorePrevistoSecondoPezzoDue As String
        Dim ValorePrevistoSecondoPezzoTre As String

        Try
            Dim intContatoreSecPezzo As Integer = propIdTestataDaReportEsistente

            Dim IDPezziSecondoPezzo As Integer = intContatoreSecPezzo
            Dim IDIntestazione As Integer = intContatoreSecPezzo

            If txtValPrev1Pz2.Text = Nothing Then
                ValorePrevistoSecondoPezzoUno = String.Empty
            Else
                ValorePrevistoSecondoPezzoUno = txtValPrev1Pz2.Text
            End If

            If txtValPrev2Pz2.Text = Nothing Then
                ValorePrevistoSecondoPezzoDue = String.Empty
            Else
                ValorePrevistoSecondoPezzoDue = txtValPrev2Pz2.Text
            End If


            ValorePrevistoSecondoPezzoTre = txtValPrev3Pz2.Text
            Dim ValorePrevistoSecondoPezzoQuattro As String = txtValPrev4Pz2.Text
            Dim ValorePrevistoSecondoPezzoCinque As String = txtValPrev5Pz2.Text
            Dim ValorePrevistoSecondoPezzoSei As String = txtValPrev6Pz2.Text
            Dim ValorePrevistoSecondoPezzoSette As String = txtValPrev7Pz2.Text
            Dim ValorePrevistoSecondoPezzoOtto As String = txtValPrev8Pz2.Text
            Dim ValorePrevistoSecondoPezzoNove As String = txtValPrev9Pz2.Text
            Dim ValorePrevistoSecondoPezzoDieci As String = txtValPrev10Pz2.Text

            Dim ValoreMisuratoSecondoPezzoUno As String = txt1ValMisPz2.Text
            Dim ValoreMisuratoSecondoPezzoDue As String = txt2ValMisPz2.Text
            Dim ValoreMisuratoSecondoPezzoTre As String = txt3ValMisPz2.Text
            Dim ValoreMisuratoSecondoPezzoQuattro As String = txt4ValMisPz2.Text
            Dim ValoreMisuratoSecondoPezzoCinque As String = txt5ValMisPz2.Text
            Dim ValoreMisuratoSecondoPezzoSei As String = txt6ValMisPz2.Text
            Dim ValoreMisuratoSecondoPezzoSette As String = txt7ValMisPz2.Text
            Dim ValoreMisuratoSecondoPezzoOtto As String = txt8ValMisPz2.Text
            Dim ValoreMisuratoSecondoPezzoNove As String = txt9ValMisPz2.Text
            Dim ValoreMisuratoSecondoPezzoDieci As String = txt10ValMisPz2.Text

            Dim TolleranzaPiuValorePrevistoSecondoUno As String = txtTolleranzaPiuValPrev1Pz2.Text
            Dim TolleranzaPiuValorePrevistoSecondoDue As String = txtTolleranzaPiuValPrev2Pz2.Text
            Dim TolleranzaPiuValorePrevistoSecondoTre As String = txtTolleranzaPiuValPrev3Pz2.Text
            Dim TolleranzaPiuValorePrevistoSecondoQuattro As String = txtTolleranzaPiuValPrev4Pz2.Text
            Dim TolleranzaPiuValorePrevistoSecondoCinque As String = txtTolleranzaPiuValPrev5Pz2.Text
            Dim TolleranzaPiuValorePrevistoSecondoSei As String = txtTolleranzaPiuValPrev6Pz2.Text
            Dim TolleranzaPiuValorePrevistoSecondoSette As String = txtTolleranzaPiuValPrev7Pz2.Text
            Dim TolleranzaPiuValorePrevistoSecondoOtto As String = txtTolleranzaPiuValPrev8Pz2.Text
            Dim TolleranzaPiuValorePrevistoSecondoNove As String = txtTolleranzaPiuValPrev9Pz2.Text
            Dim TolleranzaPiuValorePrevistoSecondoDieci As String = txtTolleranzaPiuValPrev10Pz2.Text

            Dim TolleranzaMenoValorePrevistoSecondoUno As String = txtTolleranzaMenoValPrev1Pz2.Text
            Dim TolleranzaMenoValorePrevistoSecondoDue As String = txtTolleranzaMenoValPrev2Pz2.Text
            Dim TolleranzaMenoValorePrevistoSecondoTre As String = txtTolleranzaMenoValPrev3Pz2.Text
            Dim TolleranzaMenoValorePrevistoSecondoQuattro As String = txtTolleranzaMenoValPrev4Pz2.Text
            Dim TolleranzaMenoValorePrevistoSecondoCinque As String = txtTolleranzaMenoValPrev5Pz2.Text
            Dim TolleranzaMenoValorePrevistoSecondoSei As String = txtTolleranzaMenoValPrev6Pz2.Text
            Dim TolleranzaMenoValorePrevistoSecondoSette As String = txtTolleranzaMenoValPrev7Pz2.Text
            Dim TolleranzaMenoValorePrevistoSecondoOtto As String = txtTolleranzaMenoValPrev8Pz2.Text
            Dim TolleranzaMenoValorePrevistoSecondoNove As String = txtTolleranzaMenoValPrev9Pz2.Text
            Dim TolleranzaMenoValorePrevistoSecondoDieci As String = txtTolleranzaMenoValPrev10Pz2.Text

            Dim NoteSecondoPezzo As String = txtNotePz2.Text

            Dim retSecondoPezzo As Nullable(Of Integer) = InsertPezziSecondoPezzo(IDPezziSecondoPezzo, IDIntestazione, ValorePrevistoSecondoPezzoUno, ValorePrevistoSecondoPezzoDue, ValorePrevistoSecondoPezzoTre, ValorePrevistoSecondoPezzoQuattro,
                                    ValorePrevistoSecondoPezzoCinque, ValorePrevistoSecondoPezzoSei, ValorePrevistoSecondoPezzoSette,
                                    ValorePrevistoSecondoPezzoOtto, ValorePrevistoSecondoPezzoNove, ValorePrevistoSecondoPezzoDieci,
                                    ValoreMisuratoSecondoPezzoUno, ValoreMisuratoSecondoPezzoDue, ValoreMisuratoSecondoPezzoTre,
                                    ValoreMisuratoSecondoPezzoQuattro, ValoreMisuratoSecondoPezzoCinque, ValoreMisuratoSecondoPezzoSei,
                                    ValoreMisuratoSecondoPezzoSette, ValoreMisuratoSecondoPezzoOtto, ValoreMisuratoSecondoPezzoNove,
                                    ValoreMisuratoSecondoPezzoDieci,
                                    TolleranzaPiuValorePrevistoSecondoUno,
                                    TolleranzaPiuValorePrevistoSecondoDue,
                                    TolleranzaPiuValorePrevistoSecondoTre,
                                    TolleranzaPiuValorePrevistoSecondoQuattro,
                                    TolleranzaPiuValorePrevistoSecondoCinque,
                                    TolleranzaPiuValorePrevistoSecondoSei,
                                    TolleranzaPiuValorePrevistoSecondoSette,
                                    TolleranzaPiuValorePrevistoSecondoOtto,
                                    TolleranzaPiuValorePrevistoSecondoNove,
                                    TolleranzaPiuValorePrevistoSecondoDieci,
                                    TolleranzaMenoValorePrevistoSecondoUno,
                                    TolleranzaMenoValorePrevistoSecondoDue,
                                    TolleranzaMenoValorePrevistoSecondoTre,
                                    TolleranzaMenoValorePrevistoSecondoQuattro,
                                    TolleranzaMenoValorePrevistoSecondoCinque,
                                    TolleranzaMenoValorePrevistoSecondoSei,
                                    TolleranzaMenoValorePrevistoSecondoSette,
                                    TolleranzaMenoValorePrevistoSecondoOtto,
                                    TolleranzaMenoValorePrevistoSecondoNove,
                                    TolleranzaMenoValorePrevistoSecondoDieci,
                                    NoteSecondoPezzo)

            If Not retSecondoPezzo Is Nothing Then
                Return retSecondoPezzo
            End If

        Catch ex As Exception
            MessageBox.Show("Errore  SalvaSecondoPezzo :" & ex.Message)
        End Try

    End Function



    Public Function InsertPezziSecondoPezzo(IDPezziSecondoPezzo As Integer,
IDIntestazione As Integer,
ValorePrevistoSecondoPezzoUno As String,
ValorePrevistoSecondoPezzoDue As String,
ValorePrevistoSecondoPezzoTre As String,
ValorePrevistoSecondoPezzoQuattro As String,
ValorePrevistoSecondoPezzoCinque As String,
ValorePrevistoSecondoPezzoSei As String,
ValorePrevistoSecondoPezzoSette As String,
ValorePrevistoSecondoPezzoOtto As String,
ValorePrevistoSecondoPezzoNove As String,
ValorePrevistoSecondoPezzoDieci As String,
ValoreMisuratoSecondoPezzoUno As String,
ValoreMisuratoSecondoPezzoDue As String,
ValoreMisuratoSecondoPezzoTre As String,
ValoreMisuratoSecondoPezzoQuattro As String,
ValoreMisuratoSecondoPezzoCinque As String,
ValoreMisuratoSecondoPezzoSei As String,
ValoreMisuratoSecondoPezzoSette As String,
ValoreMisuratoSecondoPezzoOtto As String,
ValoreMisuratoSecondoPezzoNove As String,
ValoreMisuratoSecondoPezzoDieci As String,
TolleranzaPiuValorePrevistoSecondoUno As String,
TolleranzaPiuValorePrevistoSecondoDue As String,
TolleranzaPiuValorePrevistoSecondoTre As String,
TolleranzaPiuValorePrevistoSecondoQuattro As String,
TolleranzaPiuValorePrevistoSecondoCinque As String,
TolleranzaPiuValorePrevistoSecondoSei As String,
TolleranzaPiuValorePrevistoSecondoSette As String,
TolleranzaPiuValorePrevistoSecondoOtto As String,
TolleranzaPiuValorePrevistoSecondoNove As String,
TolleranzaPiuValorePrevistoSecondoDieci As String,
TolleranzaMenoValorePrevistoSecondoUno As String,
TolleranzaMenoValorePrevistoSecondoDue As String,
TolleranzaMenoValorePrevistoSecondoTre As String,
TolleranzaMenoValorePrevistoSecondoQuattro As String,
TolleranzaMenoValorePrevistoSecondoCinque As String,
TolleranzaMenoValorePrevistoSecondoSei As String,
TolleranzaMenoValorePrevistoSecondoSette As String,
TolleranzaMenoValorePrevistoSecondoOtto As String,
TolleranzaMenoValorePrevistoSecondoNove As String,
TolleranzaMenoValorePrevistoSecondoDieci As String,
NoteSecondoPezzo As String) As Integer

        Dim insertCommandSecondoPezzo As DbCommand = Nothing
        Dim rowsAffectedSecondoPezzo As Integer = Nothing

        Dim strQuery As String = "INSERT INTO [dbo].[tblPezziSecondoPezzo]" &
        "([IDPezziSecondoPezzo],[IDIntestazione],[ValorePrevistoSecondoPezzoUno], " &
        "[ValorePrevistoSecondoPezzoDue],[ValorePrevistoSecondoPezzoTre], [ValorePrevistoSecondoPezzoQuattro], " &
        "[ValorePrevistoSecondoPezzoCinque]," &
        "[ValorePrevistoSecondoPezzoSei], " &
        "[ValorePrevistoSecondoPezzoSette], " &
        "[ValorePrevistoSecondoPezzoOtto], " &
        "[ValorePrevistoSecondoPezzoNove], " &
        "[ValorePrevistoSecondoPezzoDieci], " &
        "[ValoreMisuratoSecondoPezzoUno], " &
        "[ValoreMisuratoSecondoPezzoDue], " &
        "[ValoreMisuratoSecondoPezzoTre], " &
        "[ValoreMisuratoSecondoPezzoQuattro], " &
        "[ValoreMisuratoSecondoPezzoCinque], " &
        "[ValoreMisuratoSecondoPezzoSei], " &
        "[ValoreMisuratoSecondoPezzoSette], " &
        "[ValoreMisuratoSecondoPezzoOtto], " &
        "[ValoreMisuratoSecondoPezzoNove], " &
        "[ValoreMisuratoSecondoPezzoDieci], " &
        "[TolleranzaPiuValorePrevistoSecondoUno]," &
        "[TolleranzaPiuValorePrevistoSecondoDue]," &
        "[TolleranzaPiuValorePrevistoSecondoTre]," &
        "[TolleranzaPiuValorePrevistoSecondoQuattro]," &
        "[TolleranzaPiuValorePrevistoSecondoCinque]," &
        "[TolleranzaPiuValorePrevistoSecondoSei]," &
        "[TolleranzaPiuValorePrevistoSecondoSette]," &
        "[TolleranzaPiuValorePrevistoSecondoOtto]," &
        "[TolleranzaPiuValorePrevistoSecondoNove]," &
        "[TolleranzaPiuValorePrevistoSecondoDieci]," &
        "[TolleranzaMenoValorePrevistoSecondoUno]," &
        "[TolleranzaMenoValorePrevistoSecondoDue]," &
        "[TolleranzaMenoValorePrevistoSecondoTre]," &
        "[TolleranzaMenoValorePrevistoSecondoQuattro]," &
        "[TolleranzaMenoValorePrevistoSecondoCinque]," &
        "[TolleranzaMenoValorePrevistoSecondoSei]," &
        "[TolleranzaMenoValorePrevistoSecondoSette]," &
        "[TolleranzaMenoValorePrevistoSecondoOtto]," &
        "[TolleranzaMenoValorePrevistoSecondoNove]," &
        "[TolleranzaMenoValorePrevistoSecondoDieci]," &
        "[NoteSecondoPezzo]) VALUES (@IDPezziSecondoPezzo,@IDIntestazione, " &
           "@ValorePrevistoSecondoPezzoUno, " &
           "@ValorePrevistoSecondoPezzoDue, " &
           "@ValorePrevistoSecondoPezzoTre, " &
           "@ValorePrevistoSecondoPezzoQuattro, " &
           "@ValorePrevistoSecondoPezzoCinque, " &
           "@ValorePrevistoSecondoPezzoSei, " &
           "@ValorePrevistoSecondoPezzoSette, " &
           "@ValorePrevistoSecondoPezzoOtto, " &
           "@ValorePrevistoSecondoPezzoNove, " &
           "@ValorePrevistoSecondoPezzoDieci, " &
           "@ValoreMisuratoSecondoPezzoUno, " &
           "@ValoreMisuratoSecondoPezzoDue, " &
           "@ValoreMisuratoSecondoPezzoTre, " &
           "@ValoreMisuratoSecondoPezzoQuattro, " &
           "@ValoreMisuratoSecondoPezzoCinque, " &
           "@ValoreMisuratoSecondoPezzoSei, " &
           "@ValoreMisuratoSecondoPezzoSette, " &
           "@ValoreMisuratoSecondoPezzoOtto, " &
           "@ValoreMisuratoSecondoPezzoNove, " &
           "@ValoreMisuratoSecondoPezzoDieci, " &
           "@TolleranzaPiuValorePrevistoSecondoUno, " &
           "@TolleranzaPiuValorePrevistoSecondoDue, " &
           "@TolleranzaPiuValorePrevistoSecondoTre, " &
           "@TolleranzaPiuValorePrevistoSecondoQuattro, " &
           "@TolleranzaPiuValorePrevistoSecondoCinque, " &
           "@TolleranzaPiuValorePrevistoSecondoSei, " &
           "@TolleranzaPiuValorePrevistoSecondoSette, " &
           "@TolleranzaPiuValorePrevistoSecondoOtto, " &
           "@TolleranzaPiuValorePrevistoSecondoNove, " &
           "@TolleranzaPiuValorePrevistoSecondoDieci, " &
           "@TolleranzaMenoValorePrevistoSecondoUno, " &
           "@TolleranzaMenoValorePrevistoSecondoDue, " &
           "@TolleranzaMenoValorePrevistoSecondoTre, " &
           "@TolleranzaMenoValorePrevistoSecondoQuattro, " &
           "@TolleranzaMenoValorePrevistoSecondoCinque, " &
           "@TolleranzaMenoValorePrevistoSecondoSei, " &
           "@TolleranzaMenoValorePrevistoSecondoSette, " &
           "@TolleranzaMenoValorePrevistoSecondoOtto, " &
           "@TolleranzaMenoValorePrevistoSecondoNove, " &
           "@TolleranzaMenoValorePrevistoSecondoDieci, " &
           "@NoteSecondoPezzo)"

        Try

            insertCommandSecondoPezzo = _db.GetSqlStringCommand(strQuery)

            _db.AddInParameter(insertCommandSecondoPezzo, "IDPezziSecondoPezzo", DbType.Int32, IDPezziSecondoPezzo)
            _db.AddInParameter(insertCommandSecondoPezzo, "IDIntestazione", DbType.Int32, IDIntestazione)

            _db.AddInParameter(insertCommandSecondoPezzo, "ValorePrevistoSecondoPezzoUno", DbType.String, ValorePrevistoSecondoPezzoUno)
            _db.AddInParameter(insertCommandSecondoPezzo, "ValorePrevistoSecondoPezzoDue", DbType.String, ValorePrevistoSecondoPezzoDue)
            _db.AddInParameter(insertCommandSecondoPezzo, "ValorePrevistoSecondoPezzoTre", DbType.String, ValorePrevistoSecondoPezzoTre)
            _db.AddInParameter(insertCommandSecondoPezzo, "ValorePrevistoSecondoPezzoQuattro", DbType.String, ValorePrevistoSecondoPezzoQuattro)
            _db.AddInParameter(insertCommandSecondoPezzo, "ValorePrevistoSecondoPezzoCinque", DbType.String, ValorePrevistoSecondoPezzoCinque)
            _db.AddInParameter(insertCommandSecondoPezzo, "ValorePrevistoSecondoPezzoSei", DbType.String, ValorePrevistoSecondoPezzoSei)
            _db.AddInParameter(insertCommandSecondoPezzo, "ValorePrevistoSecondoPezzoSette", DbType.String, ValorePrevistoSecondoPezzoSette)
            _db.AddInParameter(insertCommandSecondoPezzo, "ValorePrevistoSecondoPezzoOtto", DbType.String, ValorePrevistoSecondoPezzoOtto)
            _db.AddInParameter(insertCommandSecondoPezzo, "ValorePrevistoSecondoPezzoNove", DbType.String, ValorePrevistoSecondoPezzoNove)
            _db.AddInParameter(insertCommandSecondoPezzo, "ValorePrevistoSecondoPezzoDieci", DbType.String, ValorePrevistoSecondoPezzoDieci)
            _db.AddInParameter(insertCommandSecondoPezzo, "ValoreMisuratoSecondoPezzoUno", DbType.String, ValoreMisuratoSecondoPezzoUno)
            _db.AddInParameter(insertCommandSecondoPezzo, "ValoreMisuratoSecondoPezzoDue", DbType.String, ValoreMisuratoSecondoPezzoDue)
            _db.AddInParameter(insertCommandSecondoPezzo, "ValoreMisuratoSecondoPezzoTre", DbType.String, ValoreMisuratoSecondoPezzoTre)
            _db.AddInParameter(insertCommandSecondoPezzo, "ValoreMisuratoSecondoPezzoQuattro", DbType.String, ValoreMisuratoSecondoPezzoQuattro)
            _db.AddInParameter(insertCommandSecondoPezzo, "ValoreMisuratoSecondoPezzoCinque", DbType.String, ValoreMisuratoSecondoPezzoCinque)
            _db.AddInParameter(insertCommandSecondoPezzo, "ValoreMisuratoSecondoPezzoSei", DbType.String, ValoreMisuratoSecondoPezzoSei)
            _db.AddInParameter(insertCommandSecondoPezzo, "ValoreMisuratoSecondoPezzoSette", DbType.String, ValoreMisuratoSecondoPezzoSette)
            _db.AddInParameter(insertCommandSecondoPezzo, "ValoreMisuratoSecondoPezzoOtto", DbType.String, ValoreMisuratoSecondoPezzoOtto)
            _db.AddInParameter(insertCommandSecondoPezzo, "ValoreMisuratoSecondoPezzoNove", DbType.String, ValoreMisuratoSecondoPezzoNove)
            _db.AddInParameter(insertCommandSecondoPezzo, "ValoreMisuratoSecondoPezzoDieci", DbType.String, ValoreMisuratoSecondoPezzoDieci)


            _db.AddInParameter(insertCommandSecondoPezzo, "TolleranzaPiuValorePrevistoSecondoUno", DbType.String, TolleranzaPiuValorePrevistoSecondoUno)
            _db.AddInParameter(insertCommandSecondoPezzo, "TolleranzaPiuValorePrevistoSecondoDue", DbType.String, TolleranzaPiuValorePrevistoSecondoDue)
            _db.AddInParameter(insertCommandSecondoPezzo, "TolleranzaPiuValorePrevistoSecondoTre", DbType.String, TolleranzaPiuValorePrevistoSecondoTre)
            _db.AddInParameter(insertCommandSecondoPezzo, "TolleranzaPiuValorePrevistoSecondoQuattro", DbType.String, TolleranzaPiuValorePrevistoSecondoQuattro)
            _db.AddInParameter(insertCommandSecondoPezzo, "TolleranzaPiuValorePrevistoSecondoCinque", DbType.String, TolleranzaPiuValorePrevistoSecondoCinque)
            _db.AddInParameter(insertCommandSecondoPezzo, "TolleranzaPiuValorePrevistoSecondoSei", DbType.String, TolleranzaPiuValorePrevistoSecondoSei)
            _db.AddInParameter(insertCommandSecondoPezzo, "TolleranzaPiuValorePrevistoSecondoSette", DbType.String, TolleranzaPiuValorePrevistoSecondoSette)
            _db.AddInParameter(insertCommandSecondoPezzo, "TolleranzaPiuValorePrevistoSecondoOtto", DbType.String, TolleranzaPiuValorePrevistoSecondoOtto)
            _db.AddInParameter(insertCommandSecondoPezzo, "TolleranzaPiuValorePrevistoSecondoNove", DbType.String, TolleranzaPiuValorePrevistoSecondoNove)
            _db.AddInParameter(insertCommandSecondoPezzo, "TolleranzaPiuValorePrevistoSecondoDieci", DbType.String, TolleranzaPiuValorePrevistoSecondoDieci)
            _db.AddInParameter(insertCommandSecondoPezzo, "TolleranzaMenoValorePrevistoSecondoUno", DbType.String, TolleranzaMenoValorePrevistoSecondoUno)
            _db.AddInParameter(insertCommandSecondoPezzo, "TolleranzaMenoValorePrevistoSecondoDue", DbType.String, TolleranzaMenoValorePrevistoSecondoDue)
            _db.AddInParameter(insertCommandSecondoPezzo, "TolleranzaMenoValorePrevistoSecondoTre", DbType.String, TolleranzaMenoValorePrevistoSecondoTre)
            _db.AddInParameter(insertCommandSecondoPezzo, "TolleranzaMenoValorePrevistoSecondoQuattro", DbType.String, TolleranzaMenoValorePrevistoSecondoQuattro)
            _db.AddInParameter(insertCommandSecondoPezzo, "TolleranzaMenoValorePrevistoSecondoCinque", DbType.String, TolleranzaMenoValorePrevistoSecondoCinque)
            _db.AddInParameter(insertCommandSecondoPezzo, "TolleranzaMenoValorePrevistoSecondoSei", DbType.String, TolleranzaMenoValorePrevistoSecondoSei)
            _db.AddInParameter(insertCommandSecondoPezzo, "TolleranzaMenoValorePrevistoSecondoSette", DbType.String, TolleranzaMenoValorePrevistoSecondoSette)
            _db.AddInParameter(insertCommandSecondoPezzo, "TolleranzaMenoValorePrevistoSecondoOtto", DbType.String, TolleranzaMenoValorePrevistoSecondoOtto)
            _db.AddInParameter(insertCommandSecondoPezzo, "TolleranzaMenoValorePrevistoSecondoNove", DbType.String, TolleranzaMenoValorePrevistoSecondoNove)
            _db.AddInParameter(insertCommandSecondoPezzo, "TolleranzaMenoValorePrevistoSecondoDieci", DbType.String, TolleranzaMenoValorePrevistoSecondoDieci)
            _db.AddInParameter(insertCommandSecondoPezzo, "NoteSecondoPezzo", DbType.String, NoteSecondoPezzo)

            rowsAffectedSecondoPezzo = _db.ExecuteNonQuery(insertCommandSecondoPezzo)

        Catch ex As Exception
            MessageBox.Show("Errore InsertPezziSecondoPezzo : " & ex.Message)
        End Try

        Return rowsAffectedSecondoPezzo

    End Function

#End Region

#Region "Salva Terzo Pezzo Nuovo"

    Public Function insertIDTerzoPezzo() As Integer
        Dim intContatoreTerzoPezzo As Integer = propIdTestataDaReportEsistente
        Dim result As DialogResult

        Dim IDPezziTerzoPezzo As Integer = intContatoreTerzoPezzo
        Dim IDIntestazione As Integer = intContatoreTerzoPezzo

        Try
            Dim retTerzoPezzoSoloID As Nullable(Of Integer) = InsertTerzoPezzoSoloID(IDPezziTerzoPezzo, IDIntestazione)

            If Not retTerzoPezzoSoloID Is Nothing Then
                Return retTerzoPezzoSoloID
            End If
        Catch ex As Exception
            MessageBox.Show("Errore insertIDTerzoPezzo :" & ex.Message)
        End Try
    End Function

    Public Function InsertTerzoPezzoSoloID(IDPezziTerzoPezzo As Integer, IDIntestazione As Integer) As Integer


        Dim insertCommandTerzoPezzoSoloID As DbCommand = Nothing
        Dim rowsAffectedTerzoPezzoSoloID As Integer = Nothing

        Dim strQuery As String = "INSERT INTO [dbo].[tblPezziTerzoPezzo] ([IDPezziTerzoPezzo],[IDIntestazione]) VALUES (@IDPezziTerzoPezzo, @IDIntestazione)"

        Try
            insertCommandTerzoPezzoSoloID = _db.GetSqlStringCommand(strQuery)

            _db.AddInParameter(insertCommandTerzoPezzoSoloID, "IDPezziTerzoPezzo", DbType.Int32, IDPezziTerzoPezzo)
            _db.AddInParameter(insertCommandTerzoPezzoSoloID, "IDIntestazione", DbType.Int32, IDIntestazione)

            rowsAffectedTerzoPezzoSoloID = _db.ExecuteNonQuery(insertCommandTerzoPezzoSoloID)

        Catch ex As Exception
            MessageBox.Show("Errore InsertTerzoPezzoSoloID :" & ex.Message)
        End Try

        Return rowsAffectedTerzoPezzoSoloID

    End Function


    Public Function SalvaTerzoPezzo() As Integer

        Try
            Dim intContatoreTerPezzo As Integer = propIdTestataDaReportEsistente
            Dim result As DialogResult

            Dim IDPezziTerzoPezzo As Integer = intContatoreTerPezzo
            Dim IDIntestazione As Integer = intContatoreTerPezzo

            Dim ValorePrevistoTerzoPezzoUno As String = txtValPrev1Pz3.Text
            Dim ValorePrevistoTerzoPezzoDue As String = txtValPrev2Pz3.Text
            Dim ValorePrevistoTerzoPezzoTre As String = txtValPrev3Pz3.Text
            Dim ValorePrevistoTerzoPezzoQuattro As String = txtValPrev4Pz3.Text
            Dim ValorePrevistoTerzoPezzoCinque As String = txtValPrev5Pz3.Text
            Dim ValorePrevistoTerzoPezzoSei As String = txtValPrev6Pz3.Text
            Dim ValorePrevistoTerzoPezzoSette As String = txtValPrev7Pz3.Text
            Dim ValorePrevistoTerzoPezzoOtto As String = txtValPrev8Pz3.Text
            Dim ValorePrevistoTerzoPezzoNove As String = txtValPrev9Pz3.Text
            Dim ValorePrevistoTerzoPezzoDieci As String = txtValPrev10Pz3.Text

            Dim ValoreMisuratoTerzoPezzoUno As String = txt1ValMisPz3.Text
            Dim ValoreMisuratoTerzoPezzoDue As String = txt2ValMisPz3.Text
            Dim ValoreMisuratoTerzoPezzoTre As String = txt3ValMisPz3.Text
            Dim ValoreMisuratoTerzoPezzoQuattro As String = txt4ValMisPz3.Text
            Dim ValoreMisuratoTerzoPezzoCinque As String = txt5ValMisPz3.Text
            Dim ValoreMisuratoTerzoPezzoSei As String = txt6ValMisPz3.Text
            Dim ValoreMisuratoTerzoPezzoSette As String = txt7ValMisPz3.Text
            Dim ValoreMisuratoTerzoPezzoOtto As String = txt8ValMisPz3.Text
            Dim ValoreMisuratoTerzoPezzoNove As String = txt9ValMisPz3.Text
            Dim ValoreMisuratoTerzoPezzoDieci As String = txt10ValMisPz3.Text

            Dim TolleranzaPiuValorePrevistoTerzoUno As String = txtTolleranzaPiuValPrev1Pz3.Text
            Dim TolleranzaPiuValorePrevistoTerzoDue As String = txtTolleranzaPiuValPrev2Pz3.Text
            Dim TolleranzaPiuValorePrevistoTerzoTre As String = txtTolleranzaPiuValPrev3Pz3.Text
            Dim TolleranzaPiuValorePrevistoTerzoQuattro As String = txtTolleranzaPiuValPrev4Pz3.Text
            Dim TolleranzaPiuValorePrevistoTerzoCinque As String = txtTolleranzaPiuValPrev5Pz3.Text
            Dim TolleranzaPiuValorePrevistoTerzoSei As String = txtTolleranzaPiuValPrev6Pz3.Text
            Dim TolleranzaPiuValorePrevistoTerzoSette As String = txtTolleranzaPiuValPrev7Pz3.Text
            Dim TolleranzaPiuValorePrevistoTerzoOtto As String = txtTolleranzaPiuValPrev8Pz3.Text
            Dim TolleranzaPiuValorePrevistoTerzoNove As String = txtTolleranzaPiuValPrev9Pz3.Text
            Dim TolleranzaPiuValorePrevistoTerzoDieci As String = txtTolleranzaPiuValPrev10Pz3.Text

            Dim TolleranzaMenoValorePrevistoTerzoUno As String = txtTolleranzaMenoValPrev1Pz3.Text
            Dim TolleranzaMenoValorePrevistoTerzoDue As String = txtTolleranzaMenoValPrev2Pz3.Text
            Dim TolleranzaMenoValorePrevistoTerzoTre As String = txtTolleranzaMenoValPrev3Pz3.Text
            Dim TolleranzaMenoValorePrevistoTerzoQuattro As String = txtTolleranzaMenoValPrev4Pz3.Text
            Dim TolleranzaMenoValorePrevistoTerzoCinque As String = txtTolleranzaMenoValPrev5Pz3.Text
            Dim TolleranzaMenoValorePrevistoTerzoSei As String = txtTolleranzaMenoValPrev6Pz3.Text
            Dim TolleranzaMenoValorePrevistoTerzoSette As String = txtTolleranzaMenoValPrev7Pz3.Text
            Dim TolleranzaMenoValorePrevistoTerzoOtto As String = txtTolleranzaMenoValPrev8Pz3.Text
            Dim TolleranzaMenoValorePrevistoTerzoNove As String = txtTolleranzaMenoValPrev9Pz3.Text
            Dim TolleranzaMenoValorePrevistoTerzoDieci As String = txtTolleranzaMenoValPrev10Pz3.Text

            Dim NoteTerzoPezzo As String = txtNotePz3.Text

            Dim retTerzoPezzo As Nullable(Of Integer) = InsertPezziTerzoPezzo(IDPezziTerzoPezzo, IDIntestazione, ValorePrevistoTerzoPezzoUno,
                                    ValorePrevistoTerzoPezzoDue, ValorePrevistoTerzoPezzoTre, ValorePrevistoTerzoPezzoQuattro,
                                    ValorePrevistoTerzoPezzoCinque, ValorePrevistoTerzoPezzoSei, ValorePrevistoTerzoPezzoSette,
                                    ValorePrevistoTerzoPezzoOtto, ValorePrevistoTerzoPezzoNove, ValorePrevistoTerzoPezzoDieci,
                                    ValoreMisuratoTerzoPezzoUno, ValoreMisuratoTerzoPezzoDue, ValoreMisuratoTerzoPezzoTre,
                                    ValoreMisuratoTerzoPezzoQuattro, ValoreMisuratoTerzoPezzoCinque, ValoreMisuratoTerzoPezzoSei,
                                    ValoreMisuratoTerzoPezzoSette, ValoreMisuratoTerzoPezzoOtto, ValoreMisuratoTerzoPezzoNove,
                                    ValoreMisuratoTerzoPezzoDieci,
                                    TolleranzaPiuValorePrevistoTerzoUno,
                                    TolleranzaPiuValorePrevistoTerzoDue,
                                    TolleranzaPiuValorePrevistoTerzoTre,
                                    TolleranzaPiuValorePrevistoTerzoQuattro,
                                    TolleranzaPiuValorePrevistoTerzoCinque,
                                    TolleranzaPiuValorePrevistoTerzoSei,
                                    TolleranzaPiuValorePrevistoTerzoSette,
                                    TolleranzaPiuValorePrevistoTerzoOtto,
                                    TolleranzaPiuValorePrevistoTerzoNove,
                                    TolleranzaPiuValorePrevistoTerzoDieci,
                                    TolleranzaMenoValorePrevistoTerzoUno,
                                    TolleranzaMenoValorePrevistoTerzoDue,
                                    TolleranzaMenoValorePrevistoTerzoTre,
                                    TolleranzaMenoValorePrevistoTerzoQuattro,
                                    TolleranzaMenoValorePrevistoTerzoCinque,
                                    TolleranzaMenoValorePrevistoTerzoSei,
                                    TolleranzaMenoValorePrevistoTerzoSette,
                                    TolleranzaMenoValorePrevistoTerzoOtto,
                                    TolleranzaMenoValorePrevistoTerzoNove,
                                    TolleranzaMenoValorePrevistoTerzoDieci,
                                    NoteTerzoPezzo)

            If Not retTerzoPezzo Is Nothing Then
                Return retTerzoPezzo
            End If

        Catch ex As Exception
            MessageBox.Show("Errore :" & ex.Message)
        End Try

    End Function


    Public Function InsertPezziTerzoPezzo(IDPezziTerzoPezzo As Integer,
IDIntestazione As Integer,
ValorePrevistoTerzoPezzoUno As String,
ValorePrevistoTerzoPezzoDue As String,
ValorePrevistoTerzoPezzoTre As String,
ValorePrevistoTerzoPezzoQuattro As String,
ValorePrevistoTerzoPezzoCinque As String,
ValorePrevistoTerzoPezzoSei As String,
ValorePrevistoTerzoPezzoSette As String,
ValorePrevistoTerzoPezzoOtto As String,
ValorePrevistoTerzoPezzoNove As String,
ValorePrevistoTerzoPezzoDieci As String,
ValoreMisuratoTerzoPezzoUno As String,
ValoreMisuratoTerzoPezzoDue As String,
ValoreMisuratoTerzoPezzoTre As String,
ValoreMisuratoTerzoPezzoQuattro As String,
ValoreMisuratoTerzoPezzoCinque As String,
ValoreMisuratoTerzoPezzoSei As String,
ValoreMisuratoTerzoPezzoSette As String,
ValoreMisuratoTerzoPezzoOtto As String,
ValoreMisuratoTerzoPezzoNove As String,
ValoreMisuratoTerzoPezzoDieci As String,
TolleranzaPiuValorePrevistoTerzoUno As String,
TolleranzaPiuValorePrevistoTerzoDue As String,
TolleranzaPiuValorePrevistoTerzoTre As String,
TolleranzaPiuValorePrevistoTerzoQuattro As String,
TolleranzaPiuValorePrevistoTerzoCinque As String,
TolleranzaPiuValorePrevistoTerzoSei As String,
TolleranzaPiuValorePrevistoTerzoSette As String,
TolleranzaPiuValorePrevistoTerzoOtto As String,
TolleranzaPiuValorePrevistoTerzoNove As String,
TolleranzaPiuValorePrevistoTerzoDieci As String,
TolleranzaMenoValorePrevistoTerzoUno As String,
TolleranzaMenoValorePrevistoTerzoDue As String,
TolleranzaMenoValorePrevistoTerzoTre As String,
TolleranzaMenoValorePrevistoTerzoQuattro As String,
TolleranzaMenoValorePrevistoTerzoCinque As String,
TolleranzaMenoValorePrevistoTerzoSei As String,
TolleranzaMenoValorePrevistoTerzoSette As String,
TolleranzaMenoValorePrevistoTerzoOtto As String,
TolleranzaMenoValorePrevistoTerzoNove As String,
TolleranzaMenoValorePrevistoTerzoDieci As String,
NoteTerzoPezzo As String) As Integer



        Dim insertCommandTerzoPezzo As DbCommand = Nothing
        Dim rowsAffectedTerzoPezzo As Integer = Nothing


        Dim strQuery As String = "INSERT INTO [dbo].[tblPezziTerzoPezzo]" &
                             "([IDPezziTerzoPezzo],[IDIntestazione],[ValorePrevistoTerzoPezzoUno], " &
                            "[ValorePrevistoTerzoPezzoDue],[ValorePrevistoTerzoPezzoTre], [ValorePrevistoTerzoPezzoQuattro], " &
                            "[ValorePrevistoTerzoPezzoCinque] ," &
                            "[ValorePrevistoTerzoPezzoSei], " &
                            "[ValorePrevistoTerzoPezzoSette]," &
                            "[ValorePrevistoTerzoPezzoOtto]," &
                            "[ValorePrevistoTerzoPezzoNove]," &
                            "[ValorePrevistoTerzoPezzoDieci]," &
                            "[ValoreMisuratoTerzoPezzoUno]," &
                            "[ValoreMisuratoTerzoPezzoDue]," &
                            "[ValoreMisuratoTerzoPezzoTre]," &
                            "[ValoreMisuratoTerzoPezzoQuattro]," &
                            "[ValoreMisuratoTerzoPezzoCinque]," &
                            "[ValoreMisuratoTerzoPezzoSei]," &
                            "[ValoreMisuratoTerzoPezzoSette]," &
                            "[ValoreMisuratoTerzoPezzoOtto]," &
                            "[ValoreMisuratoTerzoPezzoNove]," &
                            "[ValoreMisuratoTerzoPezzoDieci]," &
                            "[TolleranzaPiuValorePrevistoTerzoUno]," &
                            "[TolleranzaPiuValorePrevistoTerzoDue]," &
                            "[TolleranzaPiuValorePrevistoTerzoTre]," &
                            "[TolleranzaPiuValorePrevistoTerzoQuattro]," &
                            "[TolleranzaPiuValorePrevistoTerzoCinque]," &
                            "[TolleranzaPiuValorePrevistoTerzoSei]," &
                            "[TolleranzaPiuValorePrevistoTerzoSette]," &
                            "[TolleranzaPiuValorePrevistoTerzoOtto]," &
                            "[TolleranzaPiuValorePrevistoTerzoNove]," &
                            "[TolleranzaPiuValorePrevistoTerzoDieci]," &
                            "[TolleranzaMenoValorePrevistoTerzoUno]," &
                            "[TolleranzaMenoValorePrevistoTerzoDue]," &
                            "[TolleranzaMenoValorePrevistoTerzoTre]," &
                            "[TolleranzaMenoValorePrevistoTerzoQuattro]," &
                            "[TolleranzaMenoValorePrevistoTerzoCinque]," &
                            "[TolleranzaMenoValorePrevistoTerzoSei]," &
                            "[TolleranzaMenoValorePrevistoTerzoSette]," &
                            "[TolleranzaMenoValorePrevistoTerzoOtto]," &
                            "[TolleranzaMenoValorePrevistoTerzoNove]," &
                            "[TolleranzaMenoValorePrevistoTerzoDieci]," &
                            "[NoteTerzoPezzo]) VALUES (@IDPezziTerzoPezzo,@IDIntestazione, " &
                               "@ValorePrevistoTerzoPezzoUno, " &
                               "@ValorePrevistoTerzoPezzoDue, " &
                               "@ValorePrevistoTerzoPezzoTre, " &
                               "@ValorePrevistoTerzoPezzoQuattro, " &
                               "@ValorePrevistoTerzoPezzoCinque, " &
                               "@ValorePrevistoTerzoPezzoSei, " &
                               "@ValorePrevistoTerzoPezzoSette, " &
                               "@ValorePrevistoTerzoPezzoOtto, " &
                               "@ValorePrevistoTerzoPezzoNove, " &
                               "@ValorePrevistoTerzoPezzoDieci, " &
                               "@ValoreMisuratoTerzoPezzoUno, " &
                               "@ValoreMisuratoTerzoPezzoDue, " &
                               "@ValoreMisuratoTerzoPezzoTre, " &
                               "@ValoreMisuratoTerzoPezzoQuattro, " &
                               "@ValoreMisuratoTerzoPezzoCinque, " &
                               "@ValoreMisuratoTerzoPezzoSei, " &
                               "@ValoreMisuratoTerzoPezzoSette, " &
                               "@ValoreMisuratoTerzoPezzoOtto, " &
                               "@ValoreMisuratoTerzoPezzoNove, " &
                               "@ValoreMisuratoTerzoPezzoDieci, " &
                               "@TolleranzaPiuValorePrevistoTerzoUno, " &
                               "@TolleranzaPiuValorePrevistoTerzoDue, " &
                               "@TolleranzaPiuValorePrevistoTerzoTre, " &
                               "@TolleranzaPiuValorePrevistoTerzoQuattro, " &
                               "@TolleranzaPiuValorePrevistoTerzoCinque, " &
                               "@TolleranzaPiuValorePrevistoTerzoSei, " &
                               "@TolleranzaPiuValorePrevistoTerzoSette, " &
                               "@TolleranzaPiuValorePrevistoTerzoOtto, " &
                               "@TolleranzaPiuValorePrevistoTerzoNove, " &
                               "@TolleranzaPiuValorePrevistoTerzoDieci, " &
                               "@TolleranzaMenoValorePrevistoTerzoUno, " &
                               "@TolleranzaMenoValorePrevistoTerzoDue, " &
                               "@TolleranzaMenoValorePrevistoTerzoTre, " &
                               "@TolleranzaMenoValorePrevistoTerzoQuattro, " &
                               "@TolleranzaMenoValorePrevistoTerzoCinque, " &
                               "@TolleranzaMenoValorePrevistoTerzoSei, " &
                               "@TolleranzaMenoValorePrevistoTerzoSette, " &
                               "@TolleranzaMenoValorePrevistoTerzoOtto, " &
                               "@TolleranzaMenoValorePrevistoTerzoNove, " &
                               "@TolleranzaMenoValorePrevistoTerzoDieci, " &
                               "@NoteTerzoPezzo)"

        Try


            insertCommandTerzoPezzo = _db.GetSqlStringCommand(strQuery)


            _db.AddInParameter(insertCommandTerzoPezzo, "IDPezziTerzoPezzo", DbType.Int32, IDPezziTerzoPezzo)
            _db.AddInParameter(insertCommandTerzoPezzo, "IDIntestazione", DbType.Int32, IDIntestazione)

            _db.AddInParameter(insertCommandTerzoPezzo, "ValorePrevistoTerzoPezzoUno", DbType.String, ValorePrevistoTerzoPezzoUno)
            _db.AddInParameter(insertCommandTerzoPezzo, "ValorePrevistoTerzoPezzoDue", DbType.String, ValorePrevistoTerzoPezzoDue)
            _db.AddInParameter(insertCommandTerzoPezzo, "ValorePrevistoTerzoPezzoTre", DbType.String, ValorePrevistoTerzoPezzoTre)
            _db.AddInParameter(insertCommandTerzoPezzo, "ValorePrevistoTerzoPezzoQuattro", DbType.String, ValorePrevistoTerzoPezzoQuattro)
            _db.AddInParameter(insertCommandTerzoPezzo, "ValorePrevistoTerzoPezzoCinque", DbType.String, ValorePrevistoTerzoPezzoCinque)
            _db.AddInParameter(insertCommandTerzoPezzo, "ValorePrevistoTerzoPezzoSei", DbType.String, ValorePrevistoTerzoPezzoSei)
            _db.AddInParameter(insertCommandTerzoPezzo, "ValorePrevistoTerzoPezzoSette", DbType.String, ValorePrevistoTerzoPezzoSette)
            _db.AddInParameter(insertCommandTerzoPezzo, "ValorePrevistoTerzoPezzoOtto", DbType.String, ValorePrevistoTerzoPezzoOtto)
            _db.AddInParameter(insertCommandTerzoPezzo, "ValorePrevistoTerzoPezzoNove", DbType.String, ValorePrevistoTerzoPezzoNove)
            _db.AddInParameter(insertCommandTerzoPezzo, "ValorePrevistoTerzoPezzoDieci", DbType.String, ValorePrevistoTerzoPezzoDieci)

            _db.AddInParameter(insertCommandTerzoPezzo, "ValoreMisuratoTerzoPezzoUno", DbType.String, ValoreMisuratoTerzoPezzoUno)
            _db.AddInParameter(insertCommandTerzoPezzo, "ValoreMisuratoTerzoPezzoDue", DbType.String, ValoreMisuratoTerzoPezzoDue)
            _db.AddInParameter(insertCommandTerzoPezzo, "ValoreMisuratoTerzoPezzoTre", DbType.String, ValoreMisuratoTerzoPezzoTre)
            _db.AddInParameter(insertCommandTerzoPezzo, "ValoreMisuratoTerzoPezzoQuattro", DbType.String, ValoreMisuratoTerzoPezzoQuattro)
            _db.AddInParameter(insertCommandTerzoPezzo, "ValoreMisuratoTerzoPezzoCinque", DbType.String, ValoreMisuratoTerzoPezzoCinque)
            _db.AddInParameter(insertCommandTerzoPezzo, "ValoreMisuratoTerzoPezzoSei", DbType.String, ValoreMisuratoTerzoPezzoSei)
            _db.AddInParameter(insertCommandTerzoPezzo, "ValoreMisuratoTerzoPezzoSette", DbType.String, ValoreMisuratoTerzoPezzoSette)
            _db.AddInParameter(insertCommandTerzoPezzo, "ValoreMisuratoTerzoPezzoOtto", DbType.String, ValoreMisuratoTerzoPezzoOtto)
            _db.AddInParameter(insertCommandTerzoPezzo, "ValoreMisuratoTerzoPezzoNove", DbType.String, ValoreMisuratoTerzoPezzoNove)
            _db.AddInParameter(insertCommandTerzoPezzo, "ValoreMisuratoTerzoPezzoDieci", DbType.String, ValoreMisuratoTerzoPezzoDieci)


            _db.AddInParameter(insertCommandTerzoPezzo, "TolleranzaPiuValorePrevistoTerzoUno", DbType.String, TolleranzaPiuValorePrevistoTerzoUno)
            _db.AddInParameter(insertCommandTerzoPezzo, "TolleranzaPiuValorePrevistoTerzoDue", DbType.String, TolleranzaPiuValorePrevistoTerzoDue)
            _db.AddInParameter(insertCommandTerzoPezzo, "TolleranzaPiuValorePrevistoTerzoTre", DbType.String, TolleranzaPiuValorePrevistoTerzoTre)
            _db.AddInParameter(insertCommandTerzoPezzo, "TolleranzaPiuValorePrevistoTerzoQuattro", DbType.String, TolleranzaPiuValorePrevistoTerzoQuattro)
            _db.AddInParameter(insertCommandTerzoPezzo, "TolleranzaPiuValorePrevistoTerzoCinque", DbType.String, TolleranzaPiuValorePrevistoTerzoCinque)
            _db.AddInParameter(insertCommandTerzoPezzo, "TolleranzaPiuValorePrevistoTerzoSei", DbType.String, TolleranzaPiuValorePrevistoTerzoSei)
            _db.AddInParameter(insertCommandTerzoPezzo, "TolleranzaPiuValorePrevistoTerzoSette", DbType.String, TolleranzaPiuValorePrevistoTerzoSette)
            _db.AddInParameter(insertCommandTerzoPezzo, "TolleranzaPiuValorePrevistoTerzoOtto", DbType.String, TolleranzaPiuValorePrevistoTerzoOtto)
            _db.AddInParameter(insertCommandTerzoPezzo, "TolleranzaPiuValorePrevistoTerzoNove", DbType.String, TolleranzaPiuValorePrevistoTerzoNove)
            _db.AddInParameter(insertCommandTerzoPezzo, "TolleranzaPiuValorePrevistoTerzoDieci", DbType.String, TolleranzaPiuValorePrevistoTerzoDieci)

            _db.AddInParameter(insertCommandTerzoPezzo, "TolleranzaMenoValorePrevistoTerzoUno", DbType.String, TolleranzaMenoValorePrevistoTerzoUno)
            _db.AddInParameter(insertCommandTerzoPezzo, "TolleranzaMenoValorePrevistoTerzoDue", DbType.String, TolleranzaMenoValorePrevistoTerzoDue)
            _db.AddInParameter(insertCommandTerzoPezzo, "TolleranzaMenoValorePrevistoTerzoTre", DbType.String, TolleranzaMenoValorePrevistoTerzoTre)
            _db.AddInParameter(insertCommandTerzoPezzo, "TolleranzaMenoValorePrevistoTerzoQuattro", DbType.String, TolleranzaMenoValorePrevistoTerzoQuattro)
            _db.AddInParameter(insertCommandTerzoPezzo, "TolleranzaMenoValorePrevistoTerzoCinque", DbType.String, TolleranzaMenoValorePrevistoTerzoCinque)
            _db.AddInParameter(insertCommandTerzoPezzo, "TolleranzaMenoValorePrevistoTerzoSei", DbType.String, TolleranzaMenoValorePrevistoTerzoSei)
            _db.AddInParameter(insertCommandTerzoPezzo, "TolleranzaMenoValorePrevistoTerzoSette", DbType.String, TolleranzaMenoValorePrevistoTerzoSette)
            _db.AddInParameter(insertCommandTerzoPezzo, "TolleranzaMenoValorePrevistoTerzoOtto", DbType.String, TolleranzaMenoValorePrevistoTerzoOtto)
            _db.AddInParameter(insertCommandTerzoPezzo, "TolleranzaMenoValorePrevistoTerzoNove", DbType.String, TolleranzaMenoValorePrevistoTerzoNove)
            _db.AddInParameter(insertCommandTerzoPezzo, "TolleranzaMenoValorePrevistoTerzoDieci", DbType.String, TolleranzaMenoValorePrevistoTerzoDieci)
            _db.AddInParameter(insertCommandTerzoPezzo, "NoteTerzoPezzo", DbType.String, NoteTerzoPezzo)

            rowsAffectedTerzoPezzo = _db.ExecuteNonQuery(insertCommandTerzoPezzo)


        Catch ex As Exception
            MessageBox.Show("Errore : " & ex.Message)
        End Try

        Return rowsAffectedTerzoPezzo

    End Function

#End Region

#Region "Salva Quarto Pezzo Nuovo"

    Public Function insertQuartoIDQuartoPezzo() As Integer
        Dim intContatoreQuattroPezzo As Integer = propIdTestataDaReportEsistente
        Dim result As DialogResult

        Dim IDPezziQuartoPezzo As Integer = intContatoreQuattroPezzo
        Dim IDIntestazione As Integer = intContatoreQuattroPezzo

        Try
            Dim retQuartoPezzoSoloID As Nullable(Of Integer) = InsertPezziQuartoPezzoSoloID(IDPezziQuartoPezzo, IDIntestazione)

            If Not retQuartoPezzoSoloID Is Nothing Then
                Return retQuartoPezzoSoloID
            End If
        Catch ex As Exception
            MessageBox.Show("Errore insertQuartoIDQuartoPezzo :" & ex.Message)
        End Try
    End Function

    Public Function InsertPezziQuartoPezzoSoloID(IDPezziQuartoPezzo As Integer, IDIntestazione As Integer) As Integer


        Dim insertCommandQuartoPezzoSoloID As DbCommand = Nothing
        Dim rowsAffectedQuartoPezzoSoloID As Integer = Nothing

        Dim strQuery As String = "INSERT INTO [dbo].[tblPezziQuartoPezzo] ([IDPezziQuartoPezzo],[IDIntestazione]) VALUES (@IDPezziQuartoPezzo,@IDIntestazione)"

        Try
            insertCommandQuartoPezzoSoloID = _db.GetSqlStringCommand(strQuery)


            _db.AddInParameter(insertCommandQuartoPezzoSoloID, "IDPezziQuartoPezzo", DbType.Int32, IDPezziQuartoPezzo)
            _db.AddInParameter(insertCommandQuartoPezzoSoloID, "IDIntestazione", DbType.Int32, IDIntestazione)

            rowsAffectedQuartoPezzoSoloID = _db.ExecuteNonQuery(insertCommandQuartoPezzoSoloID)

        Catch ex As Exception
            MessageBox.Show("Errore InsertPezziQuartoPezzoSoloID :" & ex.Message)
        End Try

        Return rowsAffectedQuartoPezzoSoloID

    End Function



    Public Function SalvaQuartoPezzo() As Integer

        Try

            Dim intContatoreQuattroPezzo As Integer = propIdTestataDaReportEsistente
            Dim result As DialogResult

            Dim IDPezziQuartoPezzo As Integer = intContatoreQuattroPezzo
            Dim IDIntestazione As Integer = intContatoreQuattroPezzo

            Dim ValorePrevistoQuartoPezzoUno As String = txtValPrev1Pz4.Text
            Dim ValorePrevistoQuartoPezzoDue As String = txtValPrev2Pz4.Text
            Dim ValorePrevistoQuartoPezzoTre As String = txtValPrev3Pz4.Text
            Dim ValorePrevistoQuartoPezzoQuattro As String = txtValPrev4Pz4.Text
            Dim ValorePrevistoQuartoPezzoCinque As String = txtValPrev5Pz4.Text
            Dim ValorePrevistoQuartoPezzoSei As String = txtValPrev6Pz4.Text
            Dim ValorePrevistoQuartoPezzoSette As String = txtValPrev7Pz4.Text
            Dim ValorePrevistoQuartoPezzoOtto As String = txtValPrev8Pz4.Text
            Dim ValorePrevistoQuartoPezzoNove As String = txtValPrev9Pz4.Text
            Dim ValorePrevistoQuartoPezzoDieci As String = txtValPrev10Pz4.Text

            Dim ValoreMisuratoQuartoPezzoUno As String = txt1ValMisPz4.Text
            Dim ValoreMisuratoQuartoPezzoDue As String = txt2ValMisPz4.Text
            Dim ValoreMisuratoQuartoPezzoTre As String = txt3ValMisPz4.Text
            Dim ValoreMisuratoQuartoPezzoQuattro As String = txt4ValMisPz4.Text
            Dim ValoreMisuratoQuartoPezzoCinque As String = txt5ValMisPz4.Text
            Dim ValoreMisuratoQuartoPezzoSei As String = txt6ValMisPz4.Text
            Dim ValoreMisuratoQuartoPezzoSette As String = txt7ValMisPz4.Text
            Dim ValoreMisuratoQuartoPezzoOtto As String = txt8ValMisPz4.Text
            Dim ValoreMisuratoQuartoPezzoNove As String = txt9ValMisPz4.Text
            Dim ValoreMisuratoQuartoPezzoDieci As String = txt10ValMisPz4.Text

            Dim TolleranzaPiuValorePrevistoQuartoUno As String = txtTolleranzaPiuValPrev1Pz4.Text
            Dim TolleranzaPiuValorePrevistoQuartoDue As String = txtTolleranzaPiuValPrev2Pz4.Text
            Dim TolleranzaPiuValorePrevistoQuartoTre As String = txtTolleranzaPiuValPrev3Pz4.Text
            Dim TolleranzaPiuValorePrevistoQuartoQuattro As String = txtTolleranzaPiuValPrev4Pz4.Text
            Dim TolleranzaPiuValorePrevistoQuartoCinque As String = txtTolleranzaPiuValPrev5Pz4.Text
            Dim TolleranzaPiuValorePrevistoQuartoSei As String = txtTolleranzaPiuValPrev6Pz4.Text
            Dim TolleranzaPiuValorePrevistoQuartoSette As String = txtTolleranzaPiuValPrev7Pz4.Text
            Dim TolleranzaPiuValorePrevistoQuartoOtto As String = txtTolleranzaPiuValPrev8Pz4.Text
            Dim TolleranzaPiuValorePrevistoQuartoNove As String = txtTolleranzaPiuValPrev9Pz4.Text
            Dim TolleranzaPiuValorePrevistoQuartoDieci As String = txtTolleranzaPiuValPrev10Pz4.Text

            Dim TolleranzaMenoValorePrevistoQuartoUno As String = txtTolleranzaMenoValPrev1Pz4.Text
            Dim TolleranzaMenoValorePrevistoQuartoDue As String = txtTolleranzaMenoValPrev2Pz4.Text
            Dim TolleranzaMenoValorePrevistoQuartoTre As String = txtTolleranzaMenoValPrev3Pz4.Text
            Dim TolleranzaMenoValorePrevistoQuartoQuattro As String = txtTolleranzaMenoValPrev4Pz4.Text
            Dim TolleranzaMenoValorePrevistoQuartoCinque As String = txtTolleranzaMenoValPrev5Pz4.Text
            Dim TolleranzaMenoValorePrevistoQuartoSei As String = txtTolleranzaMenoValPrev6Pz4.Text
            Dim TolleranzaMenoValorePrevistoQuartoSette As String = txtTolleranzaMenoValPrev7Pz4.Text
            Dim TolleranzaMenoValorePrevistoQuartoOtto As String = txtTolleranzaMenoValPrev8Pz4.Text
            Dim TolleranzaMenoValorePrevistoQuartoNove As String = txtTolleranzaMenoValPrev9Pz4.Text
            Dim TolleranzaMenoValorePrevistoQuartoDieci As String = txtTolleranzaMenoValPrev10Pz4.Text

            Dim NoteQuartoPezzo As String = txtNotePz4.Text


            Dim retQuartoPezzo As Nullable(Of Integer) = InsertPezziQuartoPezzo(IDPezziQuartoPezzo, IDIntestazione, ValorePrevistoQuartoPezzoUno,
                        ValorePrevistoQuartoPezzoDue, ValorePrevistoQuartoPezzoTre, ValorePrevistoQuartoPezzoQuattro,
                        ValorePrevistoQuartoPezzoCinque, ValorePrevistoQuartoPezzoSei, ValorePrevistoQuartoPezzoSette,
                        ValorePrevistoQuartoPezzoOtto, ValorePrevistoQuartoPezzoNove, ValorePrevistoQuartoPezzoDieci,
                        ValoreMisuratoQuartoPezzoUno, ValoreMisuratoQuartoPezzoDue, ValoreMisuratoQuartoPezzoTre,
                        ValoreMisuratoQuartoPezzoQuattro, ValoreMisuratoQuartoPezzoCinque, ValoreMisuratoQuartoPezzoSei,
                        ValoreMisuratoQuartoPezzoSette, ValoreMisuratoQuartoPezzoOtto, ValoreMisuratoQuartoPezzoNove,
                        ValoreMisuratoQuartoPezzoDieci,
                        TolleranzaPiuValorePrevistoQuartoUno,
                        TolleranzaPiuValorePrevistoQuartoDue,
                        TolleranzaPiuValorePrevistoQuartoTre,
                        TolleranzaPiuValorePrevistoQuartoQuattro,
                        TolleranzaPiuValorePrevistoQuartoCinque,
                        TolleranzaPiuValorePrevistoQuartoSei,
                        TolleranzaPiuValorePrevistoQuartoSette,
                        TolleranzaPiuValorePrevistoQuartoOtto,
                        TolleranzaPiuValorePrevistoQuartoNove,
                        TolleranzaPiuValorePrevistoQuartoDieci,
                        TolleranzaMenoValorePrevistoQuartoUno,
                        TolleranzaMenoValorePrevistoQuartoDue,
                        TolleranzaMenoValorePrevistoQuartoTre,
                        TolleranzaMenoValorePrevistoQuartoQuattro,
                        TolleranzaMenoValorePrevistoQuartoCinque,
                        TolleranzaMenoValorePrevistoQuartoSei,
                        TolleranzaMenoValorePrevistoQuartoSette,
                        TolleranzaMenoValorePrevistoQuartoOtto,
                        TolleranzaMenoValorePrevistoQuartoNove,
                        TolleranzaMenoValorePrevistoQuartoDieci,
                        NoteQuartoPezzo)

            If Not retQuartoPezzo Is Nothing Then
                Return retQuartoPezzo
            End If

        Catch ex As Exception
            MessageBox.Show("Errore SalvaQuartoPezzo :" & ex.Message)
        End Try

    End Function

    Public Function InsertPezziQuartoPezzo(IDPezziQuartoPezzo As Integer,
IDIntestazione As Integer,
ValorePrevistoQuartoPezzoUno As String,
ValorePrevistoQuartoPezzoDue As String,
ValorePrevistoQuartoPezzoTre As String,
ValorePrevistoQuartoPezzoQuattro As String,
ValorePrevistoQuartoPezzoCinque As String,
ValorePrevistoQuartoPezzoSei As String,
ValorePrevistoQuartoPezzoSette As String,
ValorePrevistoQuartoPezzoOtto As String,
ValorePrevistoQuartoPezzoNove As String,
ValorePrevistoQuartoPezzoDieci As String,
ValoreMisuratoQuartoPezzoUno As String,
ValoreMisuratoQuartoPezzoDue As String,
ValoreMisuratoQuartoPezzoTre As String,
ValoreMisuratoQuartoPezzoQuattro As String,
ValoreMisuratoQuartoPezzoCinque As String,
ValoreMisuratoQuartoPezzoSei As String,
ValoreMisuratoQuartoPezzoSette As String,
ValoreMisuratoQuartoPezzoOtto As String,
ValoreMisuratoQuartoPezzoNove As String,
ValoreMisuratoQuartoPezzoDieci As String,
TolleranzaPiuValorePrevistoQuartoUno As String,
TolleranzaPiuValorePrevistoQuartoDue As String,
TolleranzaPiuValorePrevistoQuartoTre As String,
TolleranzaPiuValorePrevistoQuartoQuattro As String,
TolleranzaPiuValorePrevistoQuartoCinque As String,
TolleranzaPiuValorePrevistoQuartoSei As String,
TolleranzaPiuValorePrevistoQuartoSette As String,
TolleranzaPiuValorePrevistoQuartoOtto As String,
TolleranzaPiuValorePrevistoQuartoNove As String,
TolleranzaPiuValorePrevistoQuartoDieci As String,
TolleranzaMenoValorePrevistoQuartoUno As String,
TolleranzaMenoValorePrevistoQuartoDue As String,
TolleranzaMenoValorePrevistoQuartoTre As String,
TolleranzaMenoValorePrevistoQuartoQuattro As String,
TolleranzaMenoValorePrevistoQuartoCinque As String,
TolleranzaMenoValorePrevistoQuartoSei As String,
TolleranzaMenoValorePrevistoQuartoSette As String,
TolleranzaMenoValorePrevistoQuartoOtto As String,
TolleranzaMenoValorePrevistoQuartoNove As String,
TolleranzaMenoValorePrevistoQuartoDieci As String,
NoteQuartoPezzo As String) As Integer


        Dim insertCommandQuartoPezzo As DbCommand = Nothing
        Dim rowsAffectedQuartoPezzo As Integer = Nothing


        Dim strQuery As String = "INSERT INTO [dbo].[tblPezziQuartoPezzo]" &
                             "([IDPezziQuartoPezzo],[IDIntestazione],[ValorePrevistoQuartoPezzoUno], " &
                            "[ValorePrevistoQuartoPezzoDue],[ValorePrevistoQuartoPezzoTre], [ValorePrevistoQuartoPezzoQuattro], " &
                            "[ValorePrevistoQuartoPezzoCinque] ," &
                            "[ValorePrevistoQuartoPezzoSei], " &
                            "[ValorePrevistoQuartoPezzoSette]," &
                            "[ValorePrevistoQuartoPezzoOtto]," &
                            "[ValorePrevistoQuartoPezzoNove]," &
                            "[ValorePrevistoQuartoPezzoDieci]," &
                            "[ValoreMisuratoQuartoPezzoUno]," &
                            "[ValoreMisuratoQuartoPezzoDue]," &
                            "[ValoreMisuratoQuartoPezzoTre]," &
                            "[ValoreMisuratoQuartoPezzoQuattro]," &
                            "[ValoreMisuratoQuartoPezzoCinque]," &
                            "[ValoreMisuratoQuartoPezzoSei]," &
                            "[ValoreMisuratoQuartoPezzoSette]," &
                            "[ValoreMisuratoQuartoPezzoOtto]," &
                            "[ValoreMisuratoQuartoPezzoNove]," &
                            "[ValoreMisuratoQuartoPezzoDieci]," &
                            "[TolleranzaPiuValorePrevistoQuartoUno]," &
                            "[TolleranzaPiuValorePrevistoQuartoDue]," &
                            "[TolleranzaPiuValorePrevistoQuartoTre]," &
                            "[TolleranzaPiuValorePrevistoQuartoQuattro]," &
                            "[TolleranzaPiuValorePrevistoQuartoCinque]," &
                            "[TolleranzaPiuValorePrevistoQuartoSei]," &
                            "[TolleranzaPiuValorePrevistoQuartoSette]," &
                            "[TolleranzaPiuValorePrevistoQuartoOtto]," &
                            "[TolleranzaPiuValorePrevistoQuartoNove]," &
                            "[TolleranzaPiuValorePrevistoQuartoDieci]," &
                            "[TolleranzaMenoValorePrevistoQuartoUno]," &
                            "[TolleranzaMenoValorePrevistoQuartoDue]," &
                            "[TolleranzaMenoValorePrevistoQuartoTre]," &
                            "[TolleranzaMenoValorePrevistoQuartoQuattro]," &
                            "[TolleranzaMenoValorePrevistoQuartoCinque]," &
                            "[TolleranzaMenoValorePrevistoQuartoSei]," &
                            "[TolleranzaMenoValorePrevistoQuartoSette]," &
                            "[TolleranzaMenoValorePrevistoQuartoOtto]," &
                            "[TolleranzaMenoValorePrevistoQuartoNove]," &
                            "[TolleranzaMenoValorePrevistoQuartoDieci]," &
                             "[NoteQuartoPezzo]) VALUES (@IDPezziQuartoPezzo,@IDIntestazione, " &
                               "@ValorePrevistoQuartoPezzoUno, " &
                               "@ValorePrevistoQuartoPezzoDue, " &
                               "@ValorePrevistoQuartoPezzoTre, " &
                               "@ValorePrevistoQuartoPezzoQuattro, " &
                               "@ValorePrevistoQuartoPezzoCinque, " &
                               "@ValorePrevistoQuartoPezzoSei, " &
                               "@ValorePrevistoQuartoPezzoSette, " &
                               "@ValorePrevistoQuartoPezzoOtto, " &
                               "@ValorePrevistoQuartoPezzoNove, " &
                               "@ValorePrevistoQuartoPezzoDieci, " &
                               "@ValoreMisuratoQuartoPezzoUno, " &
                               "@ValoreMisuratoQuartoPezzoDue, " &
                               "@ValoreMisuratoQuartoPezzoTre, " &
                               "@ValoreMisuratoQuartoPezzoQuattro, " &
                               "@ValoreMisuratoQuartoPezzoCinque, " &
                               "@ValoreMisuratoQuartoPezzoSei, " &
                               "@ValoreMisuratoQuartoPezzoSette, " &
                               "@ValoreMisuratoQuartoPezzoOtto, " &
                               "@ValoreMisuratoQuartoPezzoNove, " &
                               "@ValoreMisuratoQuartoPezzoDieci, " &
                               "@TolleranzaPiuValorePrevistoQuartoUno, " &
                               "@TolleranzaPiuValorePrevistoQuartoDue, " &
                               "@TolleranzaPiuValorePrevistoQuartoTre, " &
                               "@TolleranzaPiuValorePrevistoQuartoQuattro, " &
                               "@TolleranzaPiuValorePrevistoQuartoCinque, " &
                               "@TolleranzaPiuValorePrevistoQuartoSei, " &
                               "@TolleranzaPiuValorePrevistoQuartoSette, " &
                               "@TolleranzaPiuValorePrevistoQuartoOtto, " &
                               "@TolleranzaPiuValorePrevistoQuartoNove, " &
                               "@TolleranzaPiuValorePrevistoQuartoDieci, " &
                               "@TolleranzaMenoValorePrevistoQuartoUno, " &
                               "@TolleranzaMenoValorePrevistoQuartoDue, " &
                               "@TolleranzaMenoValorePrevistoQuartoTre, " &
                               "@TolleranzaMenoValorePrevistoQuartoQuattro, " &
                               "@TolleranzaMenoValorePrevistoQuartoCinque, " &
                               "@TolleranzaMenoValorePrevistoQuartoSei, " &
                               "@TolleranzaMenoValorePrevistoQuartoSette, " &
                               "@TolleranzaMenoValorePrevistoQuartoOtto, " &
                               "@TolleranzaMenoValorePrevistoQuartoNove, " &
                               "@TolleranzaMenoValorePrevistoQuartoDieci, " &
                               "@NoteQuartoPezzo)"

        Try


            insertCommandQuartoPezzo = _db.GetSqlStringCommand(strQuery)


            _db.AddInParameter(insertCommandQuartoPezzo, "IDPezziQuartoPezzo", DbType.Int32, IDPezziQuartoPezzo)
            _db.AddInParameter(insertCommandQuartoPezzo, "IDIntestazione", DbType.Int32, IDIntestazione)

            _db.AddInParameter(insertCommandQuartoPezzo, "ValorePrevistoQuartoPezzoUno", DbType.String, ValorePrevistoQuartoPezzoUno)
            _db.AddInParameter(insertCommandQuartoPezzo, "ValorePrevistoQuartoPezzoDue", DbType.String, ValorePrevistoQuartoPezzoDue)
            _db.AddInParameter(insertCommandQuartoPezzo, "ValorePrevistoQuartoPezzoTre", DbType.String, ValorePrevistoQuartoPezzoTre)
            _db.AddInParameter(insertCommandQuartoPezzo, "ValorePrevistoQuartoPezzoQuattro", DbType.String, ValorePrevistoQuartoPezzoQuattro)
            _db.AddInParameter(insertCommandQuartoPezzo, "ValorePrevistoQuartoPezzoCinque", DbType.String, ValorePrevistoQuartoPezzoCinque)
            _db.AddInParameter(insertCommandQuartoPezzo, "ValorePrevistoQuartoPezzoSei", DbType.String, ValorePrevistoQuartoPezzoSei)
            _db.AddInParameter(insertCommandQuartoPezzo, "ValorePrevistoQuartoPezzoSette", DbType.String, ValorePrevistoQuartoPezzoSette)
            _db.AddInParameter(insertCommandQuartoPezzo, "ValorePrevistoQuartoPezzoOtto", DbType.String, ValorePrevistoQuartoPezzoOtto)
            _db.AddInParameter(insertCommandQuartoPezzo, "ValorePrevistoQuartoPezzoNove", DbType.String, ValorePrevistoQuartoPezzoNove)
            _db.AddInParameter(insertCommandQuartoPezzo, "ValorePrevistoQuartoPezzoDieci", DbType.String, ValorePrevistoQuartoPezzoDieci)

            _db.AddInParameter(insertCommandQuartoPezzo, "ValoreMisuratoQuartoPezzoUno", DbType.String, ValoreMisuratoQuartoPezzoUno)
            _db.AddInParameter(insertCommandQuartoPezzo, "ValoreMisuratoQuartoPezzoDue", DbType.String, ValoreMisuratoQuartoPezzoDue)
            _db.AddInParameter(insertCommandQuartoPezzo, "ValoreMisuratoQuartoPezzoTre", DbType.String, ValoreMisuratoQuartoPezzoTre)
            _db.AddInParameter(insertCommandQuartoPezzo, "ValoreMisuratoQuartoPezzoQuattro", DbType.String, ValoreMisuratoQuartoPezzoQuattro)
            _db.AddInParameter(insertCommandQuartoPezzo, "ValoreMisuratoQuartoPezzoCinque", DbType.String, ValoreMisuratoQuartoPezzoCinque)
            _db.AddInParameter(insertCommandQuartoPezzo, "ValoreMisuratoQuartoPezzoSei", DbType.String, ValoreMisuratoQuartoPezzoSei)
            _db.AddInParameter(insertCommandQuartoPezzo, "ValoreMisuratoQuartoPezzoSette", DbType.String, ValoreMisuratoQuartoPezzoSette)
            _db.AddInParameter(insertCommandQuartoPezzo, "ValoreMisuratoQuartoPezzoOtto", DbType.String, ValoreMisuratoQuartoPezzoOtto)
            _db.AddInParameter(insertCommandQuartoPezzo, "ValoreMisuratoQuartoPezzoNove", DbType.String, ValoreMisuratoQuartoPezzoNove)
            _db.AddInParameter(insertCommandQuartoPezzo, "ValoreMisuratoQuartoPezzoDieci", DbType.String, ValoreMisuratoQuartoPezzoDieci)

            _db.AddInParameter(insertCommandQuartoPezzo, "TolleranzaPiuValorePrevistoQuartoUno", DbType.String, TolleranzaPiuValorePrevistoQuartoUno)
            _db.AddInParameter(insertCommandQuartoPezzo, "TolleranzaPiuValorePrevistoQuartoDue", DbType.String, TolleranzaPiuValorePrevistoQuartoDue)
            _db.AddInParameter(insertCommandQuartoPezzo, "TolleranzaPiuValorePrevistoQuartoTre", DbType.String, TolleranzaPiuValorePrevistoQuartoTre)
            _db.AddInParameter(insertCommandQuartoPezzo, "TolleranzaPiuValorePrevistoQuartoQuattro", DbType.String, TolleranzaPiuValorePrevistoQuartoQuattro)
            _db.AddInParameter(insertCommandQuartoPezzo, "TolleranzaPiuValorePrevistoQuartoCinque", DbType.String, TolleranzaPiuValorePrevistoQuartoCinque)
            _db.AddInParameter(insertCommandQuartoPezzo, "TolleranzaPiuValorePrevistoQuartoSei", DbType.String, TolleranzaPiuValorePrevistoQuartoSei)
            _db.AddInParameter(insertCommandQuartoPezzo, "TolleranzaPiuValorePrevistoQuartoSette", DbType.String, TolleranzaPiuValorePrevistoQuartoSette)
            _db.AddInParameter(insertCommandQuartoPezzo, "TolleranzaPiuValorePrevistoQuartoOtto", DbType.String, TolleranzaPiuValorePrevistoQuartoOtto)
            _db.AddInParameter(insertCommandQuartoPezzo, "TolleranzaPiuValorePrevistoQuartoNove", DbType.String, TolleranzaPiuValorePrevistoQuartoNove)
            _db.AddInParameter(insertCommandQuartoPezzo, "TolleranzaPiuValorePrevistoQuartoDieci", DbType.String, TolleranzaPiuValorePrevistoQuartoDieci)

            _db.AddInParameter(insertCommandQuartoPezzo, "TolleranzaMenoValorePrevistoQuartoUno", DbType.String, TolleranzaMenoValorePrevistoQuartoUno)
            _db.AddInParameter(insertCommandQuartoPezzo, "TolleranzaMenoValorePrevistoQuartoDue", DbType.String, TolleranzaMenoValorePrevistoQuartoDue)
            _db.AddInParameter(insertCommandQuartoPezzo, "TolleranzaMenoValorePrevistoQuartoTre", DbType.String, TolleranzaMenoValorePrevistoQuartoTre)
            _db.AddInParameter(insertCommandQuartoPezzo, "TolleranzaMenoValorePrevistoQuartoQuattro", DbType.String, TolleranzaMenoValorePrevistoQuartoQuattro)
            _db.AddInParameter(insertCommandQuartoPezzo, "TolleranzaMenoValorePrevistoQuartoCinque", DbType.String, TolleranzaMenoValorePrevistoQuartoCinque)
            _db.AddInParameter(insertCommandQuartoPezzo, "TolleranzaMenoValorePrevistoQuartoSei", DbType.String, TolleranzaMenoValorePrevistoQuartoSei)
            _db.AddInParameter(insertCommandQuartoPezzo, "TolleranzaMenoValorePrevistoQuartoSette", DbType.String, TolleranzaMenoValorePrevistoQuartoSette)
            _db.AddInParameter(insertCommandQuartoPezzo, "TolleranzaMenoValorePrevistoQuartoOtto", DbType.String, TolleranzaMenoValorePrevistoQuartoOtto)
            _db.AddInParameter(insertCommandQuartoPezzo, "TolleranzaMenoValorePrevistoQuartoNove", DbType.String, TolleranzaMenoValorePrevistoQuartoNove)
            _db.AddInParameter(insertCommandQuartoPezzo, "TolleranzaMenoValorePrevistoQuartoDieci", DbType.String, TolleranzaMenoValorePrevistoQuartoDieci)
            _db.AddInParameter(insertCommandQuartoPezzo, "NoteQuartoPezzo", DbType.String, NoteQuartoPezzo)

            rowsAffectedQuartoPezzo = _db.ExecuteNonQuery(insertCommandQuartoPezzo)

        Catch ex As Exception
            MessageBox.Show("Errore InsertPezziQuartoPezzo : " & ex.Message)
        End Try

        Return rowsAffectedQuartoPezzo

    End Function

#End Region

#Region "Salva Quinto Pezzo Nuovo"

    Public Function insertIDQuintoPezzo() As Integer
        Dim intContatoreQuintoPezzo As Integer = propIdTestataDaReportEsistente
        Dim result As DialogResult

        Dim IDPezziQuintoPezzo As Integer = intContatoreQuintoPezzo
        Dim IDIntestazione As Integer = intContatoreQuintoPezzo

        Try
            Dim retQuintoPezzoSoloID As Integer? = InsertPezziQuintoPezzoSoloID(IDPezziQuintoPezzo, IDIntestazione)

            If Not retQuintoPezzoSoloID Is Nothing Then
                Return retQuintoPezzoSoloID
            End If
        Catch ex As Exception
            MessageBox.Show("Errore insertIDQuintoPezzo :" & ex.Message)
        End Try
    End Function

    Public Function InsertPezziQuintoPezzoSoloID(IDPezziQuintoPezzo As Integer, IDIntestazione As Integer) As Integer


        Dim insertCommandQuintoPezzoSoloID As DbCommand = Nothing
        Dim rowsAffectedQuintoPezzoSoloID As Integer = Nothing

        Dim strQuery As String = "INSERT INTO [dbo].[tblPezziQuintoPezzo] ([IDPezziQuintoPezzo],[IDIntestazione]) VALUES (@IDPezziQuintoPezzo, @IDIntestazione)"

        Try
            insertCommandQuintoPezzoSoloID = _db.GetSqlStringCommand(strQuery)


            _db.AddInParameter(insertCommandQuintoPezzoSoloID, "IDPezziQuintoPezzo", DbType.Int32, IDPezziQuintoPezzo)
            _db.AddInParameter(insertCommandQuintoPezzoSoloID, "IDIntestazione", DbType.Int32, IDIntestazione)

            rowsAffectedQuintoPezzoSoloID = _db.ExecuteNonQuery(insertCommandQuintoPezzoSoloID)

        Catch ex As Exception
            MessageBox.Show("Errore InsertPezziQuintoPezzoSoloID :" & ex.Message)
        End Try

        Return rowsAffectedQuintoPezzoSoloID

    End Function

    Public Function SalvaQuintoPezzo() As Integer

        Try


            Dim intContatoreQuintoPezzo As Integer = propIdTestataDaReportEsistente
            Dim result As DialogResult

            Dim IDPezziQuintoPezzo As Integer = intContatoreQuintoPezzo
            Dim IDIntestazione As Integer = intContatoreQuintoPezzo

            Dim ValorePrevistoQuintoPezzoUno As String = txtValPrev1Pz5.Text
            Dim ValorePrevistoQuintoPezzoDue As String = txtValPrev2Pz5.Text
            Dim ValorePrevistoQuintoPezzoTre As String = txtValPrev3Pz5.Text
            Dim ValorePrevistoQuintoPezzoQuattro As String = txtValPrev4Pz5.Text
            Dim ValorePrevistoQuintoPezzoCinque As String = txtValPrev5Pz5.Text
            Dim ValorePrevistoQuintoPezzoSei As String = txtValPrev6Pz5.Text
            Dim ValorePrevistoQuintoPezzoSette As String = txtValPrev7Pz5.Text
            Dim ValorePrevistoQuintoPezzoOtto As String = txtValPrev8Pz5.Text
            Dim ValorePrevistoQuintoPezzoNove As String = txtValPrev9Pz5.Text
            Dim ValorePrevistoQuintoPezzoDieci As String = txtValPrev10Pz5.Text

            Dim ValoreMisuratoQuintoPezzoUno As String = txt1ValMisPz5.Text
            Dim ValoreMisuratoQuintoPezzoDue As String = txt2ValMisPz5.Text
            Dim ValoreMisuratoQuintoPezzoTre As String = txt3ValMisPz5.Text
            Dim ValoreMisuratoQuintoPezzoQuattro As String = txt4ValMisPz5.Text
            Dim ValoreMisuratoQuintoPezzoCinque As String = txt5ValMisPz5.Text
            Dim ValoreMisuratoQuintoPezzoSei As String = txt6ValMisPz5.Text
            Dim ValoreMisuratoQuintoPezzoSette As String = txt7ValMisPz5.Text
            Dim ValoreMisuratoQuintoPezzoOtto As String = txt8ValMisPz5.Text
            Dim ValoreMisuratoQuintoPezzoNove As String = txt9ValMisPz5.Text
            Dim ValoreMisuratoQuintoPezzoDieci As String = txt10ValMisPz5.Text

            Dim TolleranzaPiuValorePrevistoQuintoUno As String = txtTolleranzaPiuValPrev1Pz5.Text
            Dim TolleranzaPiuValorePrevistoQuintoDue As String = txtTolleranzaPiuValPrev2Pz5.Text
            Dim TolleranzaPiuValorePrevistoQuintoTre As String = txtTolleranzaPiuValPrev3Pz5.Text
            Dim TolleranzaPiuValorePrevistoQuintoQuattro As String = txtTolleranzaPiuValPrev4Pz5.Text
            Dim TolleranzaPiuValorePrevistoQuintoCinque As String = txtTolleranzaPiuValPrev5Pz5.Text
            Dim TolleranzaPiuValorePrevistoQuintoSei As String = txtTolleranzaPiuValPrev6Pz5.Text
            Dim TolleranzaPiuValorePrevistoQuintoSette As String = txtTolleranzaPiuValPrev7Pz5.Text
            Dim TolleranzaPiuValorePrevistoQuintoOtto As String = txtTolleranzaPiuValPrev8Pz5.Text
            Dim TolleranzaPiuValorePrevistoQuintoNove As String = txtTolleranzaPiuValPrev9Pz5.Text
            Dim TolleranzaPiuValorePrevistoQuintoDieci As String = txtTolleranzaPiuValPrev10Pz5.Text

            Dim TolleranzaMenoValorePrevistoQuintoUno As String = txtTolleranzaMenoValPrev1Pz5.Text
            Dim TolleranzaMenoValorePrevistoQuintoDue As String = txtTolleranzaMenoValPrev2Pz5.Text
            Dim TolleranzaMenoValorePrevistoQuintoTre As String = txtTolleranzaMenoValPrev3Pz5.Text
            Dim TolleranzaMenoValorePrevistoQuintoQuattro As String = txtTolleranzaMenoValPrev4Pz5.Text
            Dim TolleranzaMenoValorePrevistoQuintoCinque As String = txtTolleranzaMenoValPrev5Pz5.Text
            Dim TolleranzaMenoValorePrevistoQuintoSei As String = txtTolleranzaMenoValPrev6Pz5.Text
            Dim TolleranzaMenoValorePrevistoQuintoSette As String = txtTolleranzaMenoValPrev7Pz5.Text
            Dim TolleranzaMenoValorePrevistoQuintoOtto As String = txtTolleranzaMenoValPrev8Pz5.Text
            Dim TolleranzaMenoValorePrevistoQuintoNove As String = txtTolleranzaMenoValPrev9Pz5.Text
            Dim TolleranzaMenoValorePrevistoQuintoDieci As String = txtTolleranzaMenoValPrev10Pz5.Text

            Dim NoteQuintoPezzo As String = txtNotePz5.Text


            Dim retQuintoPezzo As Nullable(Of Integer) = InsertPezziQuintoPezzo(IDPezziQuintoPezzo, IDIntestazione, ValorePrevistoQuintoPezzoUno,
                        ValorePrevistoQuintoPezzoDue, ValorePrevistoQuintoPezzoTre, ValorePrevistoQuintoPezzoQuattro,
                        ValorePrevistoQuintoPezzoCinque, ValorePrevistoQuintoPezzoSei, ValorePrevistoQuintoPezzoSette,
                        ValorePrevistoQuintoPezzoOtto, ValorePrevistoQuintoPezzoNove, ValorePrevistoQuintoPezzoDieci,
                        ValoreMisuratoQuintoPezzoUno, ValoreMisuratoQuintoPezzoDue, ValoreMisuratoQuintoPezzoTre,
                        ValoreMisuratoQuintoPezzoQuattro, ValoreMisuratoQuintoPezzoCinque, ValoreMisuratoQuintoPezzoSei,
                        ValoreMisuratoQuintoPezzoSette, ValoreMisuratoQuintoPezzoOtto, ValoreMisuratoQuintoPezzoNove,
                        ValoreMisuratoQuintoPezzoDieci,
                        TolleranzaPiuValorePrevistoQuintoUno,
                        TolleranzaPiuValorePrevistoQuintoDue,
                        TolleranzaPiuValorePrevistoQuintoTre,
                        TolleranzaPiuValorePrevistoQuintoQuattro,
                        TolleranzaPiuValorePrevistoQuintoCinque,
                        TolleranzaPiuValorePrevistoQuintoSei,
                        TolleranzaPiuValorePrevistoQuintoSette,
                        TolleranzaPiuValorePrevistoQuintoOtto,
                        TolleranzaPiuValorePrevistoQuintoNove,
                        TolleranzaPiuValorePrevistoQuintoDieci,
                        TolleranzaMenoValorePrevistoQuintoUno,
                        TolleranzaMenoValorePrevistoQuintoDue,
                        TolleranzaMenoValorePrevistoQuintoTre,
                        TolleranzaMenoValorePrevistoQuintoQuattro,
                        TolleranzaMenoValorePrevistoQuintoCinque,
                        TolleranzaMenoValorePrevistoQuintoSei,
                        TolleranzaMenoValorePrevistoQuintoSette,
                        TolleranzaMenoValorePrevistoQuintoOtto,
                        TolleranzaMenoValorePrevistoQuintoNove,
                        TolleranzaMenoValorePrevistoQuintoDieci,
                        NoteQuintoPezzo)

            If Not retQuintoPezzo Is Nothing Then
                Return retQuintoPezzo
            End If

        Catch ex As Exception
            MessageBox.Show("Errore SalvaQuintoPezzo :" & ex.Message)
        End Try

    End Function

    Public Function InsertPezziQuintoPezzo(IDPezziQuintoPezzo As Integer,
IDIntestazione As Integer,
ValorePrevistoQuintoPezzoUno As String,
ValorePrevistoQuintoPezzoDue As String,
ValorePrevistoQuintoPezzoTre As String,
ValorePrevistoQuintoPezzoQuattro As String,
ValorePrevistoQuintoPezzoCinque As String,
ValorePrevistoQuintoPezzoSei As String,
ValorePrevistoQuintoPezzoSette As String,
ValorePrevistoQuintoPezzoOtto As String,
ValorePrevistoQuintoPezzoNove As String,
ValorePrevistoQuintoPezzoDieci As String,
ValoreMisuratoQuintoPezzoUno As String,
ValoreMisuratoQuintoPezzoDue As String,
ValoreMisuratoQuintoPezzoTre As String,
ValoreMisuratoQuintoPezzoQuattro As String,
ValoreMisuratoQuintoPezzoCinque As String,
ValoreMisuratoQuintoPezzoSei As String,
ValoreMisuratoQuintoPezzoSette As String,
ValoreMisuratoQuintoPezzoOtto As String,
ValoreMisuratoQuintoPezzoNove As String,
ValoreMisuratoQuintoPezzoDieci As String,
TolleranzaPiuValorePrevistoQuintoUno As String,
TolleranzaPiuValorePrevistoQuintoDue As String,
TolleranzaPiuValorePrevistoQuintoTre As String,
TolleranzaPiuValorePrevistoQuintoQuattro As String,
TolleranzaPiuValorePrevistoQuintoCinque As String,
TolleranzaPiuValorePrevistoQuintoSei As String,
TolleranzaPiuValorePrevistoQuintoSette As String,
TolleranzaPiuValorePrevistoQuintoOtto As String,
TolleranzaPiuValorePrevistoQuintoNove As String,
TolleranzaPiuValorePrevistoQuintoDieci As String,
TolleranzaMenoValorePrevistoQuintoUno As String,
TolleranzaMenoValorePrevistoQuintoDue As String,
TolleranzaMenoValorePrevistoQuintoTre As String,
TolleranzaMenoValorePrevistoQuintoQuattro As String,
TolleranzaMenoValorePrevistoQuintoCinque As String,
TolleranzaMenoValorePrevistoQuintoSei As String,
TolleranzaMenoValorePrevistoQuintoSette As String,
TolleranzaMenoValorePrevistoQuintoOtto As String,
TolleranzaMenoValorePrevistoQuintoNove As String,
TolleranzaMenoValorePrevistoQuintoDieci As String,
NoteQuintoPezzo As String) As Integer


        Dim insertCommandQuintoPezzo As DbCommand = Nothing
        Dim rowsAffectedQuintoPezzo As Integer = Nothing


        Dim strQuery As String = "INSERT INTO [dbo].[tblPezziQuintoPezzo]" &
                             "([IDPezziQuintoPezzo],[IDIntestazione],[ValorePrevistoQuintoPezzoUno], " &
                            "[ValorePrevistoQuintoPezzoDue],[ValorePrevistoQuintoPezzoTre], [ValorePrevistoQuintoPezzoQuattro], " &
                            "[ValorePrevistoQuintoPezzoCinque] ," &
                            "[ValorePrevistoQuintoPezzoSei], " &
                            "[ValorePrevistoQuintoPezzoSette]," &
                            "[ValorePrevistoQuintoPezzoOtto]," &
                            "[ValorePrevistoQuintoPezzoNove]," &
                            "[ValorePrevistoQuintoPezzoDieci]," &
                            "[ValoreMisuratoQuintoPezzoUno]," &
                            "[ValoreMisuratoQuintoPezzoDue]," &
                            "[ValoreMisuratoQuintoPezzoTre]," &
                            "[ValoreMisuratoQuintoPezzoQuattro]," &
                            "[ValoreMisuratoQuintoPezzoCinque]," &
                            "[ValoreMisuratoQuintoPezzoSei]," &
                            "[ValoreMisuratoQuintoPezzoSette]," &
                            "[ValoreMisuratoQuintoPezzoOtto]," &
                            "[ValoreMisuratoQuintoPezzoNove]," &
                            "[ValoreMisuratoQuintoPezzoDieci]," &
                            "[TolleranzaPiuValorePrevistoQuintoUno]," &
                            "[TolleranzaPiuValorePrevistoQuintoDue]," &
                            "[TolleranzaPiuValorePrevistoQuintoTre]," &
                            "[TolleranzaPiuValorePrevistoQuintoQuattro]," &
                            "[TolleranzaPiuValorePrevistoQuintoCinque]," &
                            "[TolleranzaPiuValorePrevistoQuintoSei]," &
                            "[TolleranzaPiuValorePrevistoQuintoSette]," &
                            "[TolleranzaPiuValorePrevistoQuintoOtto]," &
                            "[TolleranzaPiuValorePrevistoQuintoNove]," &
                            "[TolleranzaPiuValorePrevistoQuintoDieci]," &
                            "[TolleranzaMenoValorePrevistoQuintoUno]," &
                            "[TolleranzaMenoValorePrevistoQuintoDue]," &
                            "[TolleranzaMenoValorePrevistoQuintoTre]," &
                            "[TolleranzaMenoValorePrevistoQuintoQuattro]," &
                            "[TolleranzaMenoValorePrevistoQuintoCinque]," &
                            "[TolleranzaMenoValorePrevistoQuintoSei]," &
                            "[TolleranzaMenoValorePrevistoQuintoSette]," &
                            "[TolleranzaMenoValorePrevistoQuintoOtto]," &
                            "[TolleranzaMenoValorePrevistoQuintoNove]," &
                            "[TolleranzaMenoValorePrevistoQuintoDieci]," &
                             "[NoteQuintoPezzo])" &
                            "VALUES " &
                               "(@IDPezziQuintoPezzo,@IDIntestazione, " &
                               "@ValorePrevistoQuintoPezzoUno, " &
                               "@ValorePrevistoQuintoPezzoDue, " &
                               "@ValorePrevistoQuintoPezzoTre, " &
                               "@ValorePrevistoQuintoPezzoQuattro, " &
                               "@ValorePrevistoQuintoPezzoCinque, " &
                               "@ValorePrevistoQuintoPezzoSei, " &
                               "@ValorePrevistoQuintoPezzoSette, " &
                               "@ValorePrevistoQuintoPezzoOtto, " &
                               "@ValorePrevistoQuintoPezzoNove, " &
                               "@ValorePrevistoQuintoPezzoDieci, " &
                               "@ValoreMisuratoQuintoPezzoUno, " &
                               "@ValoreMisuratoQuintoPezzoDue, " &
                               "@ValoreMisuratoQuintoPezzoTre, " &
                               "@ValoreMisuratoQuintoPezzoQuattro, " &
                               "@ValoreMisuratoQuintoPezzoCinque, " &
                               "@ValoreMisuratoQuintoPezzoSei, " &
                               "@ValoreMisuratoQuintoPezzoSette, " &
                               "@ValoreMisuratoQuintoPezzoOtto, " &
                               "@ValoreMisuratoQuintoPezzoNove, " &
                               "@ValoreMisuratoQuintoPezzoDieci, " &
                               "@TolleranzaPiuValorePrevistoQuintoUno, " &
                               "@TolleranzaPiuValorePrevistoQuintoDue, " &
                               "@TolleranzaPiuValorePrevistoQuintoTre, " &
                               "@TolleranzaPiuValorePrevistoQuintoQuattro, " &
                               "@TolleranzaPiuValorePrevistoQuintoCinque, " &
                               "@TolleranzaPiuValorePrevistoQuintoSei, " &
                               "@TolleranzaPiuValorePrevistoQuintoSette, " &
                               "@TolleranzaPiuValorePrevistoQuintoOtto, " &
                               "@TolleranzaPiuValorePrevistoQuintoNove, " &
                               "@TolleranzaPiuValorePrevistoQuintoDieci, " &
                               "@TolleranzaMenoValorePrevistoQuintoUno, " &
                               "@TolleranzaMenoValorePrevistoQuintoDue, " &
                               "@TolleranzaMenoValorePrevistoQuintoTre, " &
                               "@TolleranzaMenoValorePrevistoQuintoQuattro, " &
                               "@TolleranzaMenoValorePrevistoQuintoCinque, " &
                               "@TolleranzaMenoValorePrevistoQuintoSei, " &
                               "@TolleranzaMenoValorePrevistoQuintoSette, " &
                               "@TolleranzaMenoValorePrevistoQuintoOtto, " &
                               "@TolleranzaMenoValorePrevistoQuintoNove, " &
                               "@TolleranzaMenoValorePrevistoQuintoDieci, " &
                               "@NoteQuintoPezzo)"

        Try

            insertCommandQuintoPezzo = _db.GetSqlStringCommand(strQuery)

            _db.AddInParameter(insertCommandQuintoPezzo, "IDPezziQuintoPezzo", DbType.Int32, IDPezziQuintoPezzo)
            _db.AddInParameter(insertCommandQuintoPezzo, "IDIntestazione", DbType.Int32, IDIntestazione)

            _db.AddInParameter(insertCommandQuintoPezzo, "ValorePrevistoQuintoPezzoUno", DbType.String, ValorePrevistoQuintoPezzoUno)
            _db.AddInParameter(insertCommandQuintoPezzo, "ValorePrevistoQuintoPezzoDue", DbType.String, ValorePrevistoQuintoPezzoDue)
            _db.AddInParameter(insertCommandQuintoPezzo, "ValorePrevistoQuintoPezzoTre", DbType.String, ValorePrevistoQuintoPezzoTre)
            _db.AddInParameter(insertCommandQuintoPezzo, "ValorePrevistoQuintoPezzoQuattro", DbType.String, ValorePrevistoQuintoPezzoQuattro)
            _db.AddInParameter(insertCommandQuintoPezzo, "ValorePrevistoQuintoPezzoCinque", DbType.String, ValorePrevistoQuintoPezzoCinque)
            _db.AddInParameter(insertCommandQuintoPezzo, "ValorePrevistoQuintoPezzoSei", DbType.String, ValorePrevistoQuintoPezzoSei)
            _db.AddInParameter(insertCommandQuintoPezzo, "ValorePrevistoQuintoPezzoSette", DbType.String, ValorePrevistoQuintoPezzoSette)
            _db.AddInParameter(insertCommandQuintoPezzo, "ValorePrevistoQuintoPezzoOtto", DbType.String, ValorePrevistoQuintoPezzoOtto)
            _db.AddInParameter(insertCommandQuintoPezzo, "ValorePrevistoQuintoPezzoNove", DbType.String, ValorePrevistoQuintoPezzoNove)
            _db.AddInParameter(insertCommandQuintoPezzo, "ValorePrevistoQuintoPezzoDieci", DbType.String, ValorePrevistoQuintoPezzoDieci)
            _db.AddInParameter(insertCommandQuintoPezzo, "ValoreMisuratoQuintoPezzoUno", DbType.String, ValoreMisuratoQuintoPezzoUno)
            _db.AddInParameter(insertCommandQuintoPezzo, "ValoreMisuratoQuintoPezzoDue", DbType.String, ValoreMisuratoQuintoPezzoDue)
            _db.AddInParameter(insertCommandQuintoPezzo, "ValoreMisuratoQuintoPezzoTre", DbType.String, ValoreMisuratoQuintoPezzoTre)
            _db.AddInParameter(insertCommandQuintoPezzo, "ValoreMisuratoQuintoPezzoQuattro", DbType.String, ValoreMisuratoQuintoPezzoQuattro)
            _db.AddInParameter(insertCommandQuintoPezzo, "ValoreMisuratoQuintoPezzoCinque", DbType.String, ValoreMisuratoQuintoPezzoCinque)
            _db.AddInParameter(insertCommandQuintoPezzo, "ValoreMisuratoQuintoPezzoSei", DbType.String, ValoreMisuratoQuintoPezzoSei)
            _db.AddInParameter(insertCommandQuintoPezzo, "ValoreMisuratoQuintoPezzoSette", DbType.String, ValoreMisuratoQuintoPezzoSette)
            _db.AddInParameter(insertCommandQuintoPezzo, "ValoreMisuratoQuintoPezzoOtto", DbType.String, ValoreMisuratoQuintoPezzoOtto)
            _db.AddInParameter(insertCommandQuintoPezzo, "ValoreMisuratoQuintoPezzoNove", DbType.String, ValoreMisuratoQuintoPezzoNove)
            _db.AddInParameter(insertCommandQuintoPezzo, "ValoreMisuratoQuintoPezzoDieci", DbType.String, ValoreMisuratoQuintoPezzoDieci)

            _db.AddInParameter(insertCommandQuintoPezzo, "TolleranzaPiuValorePrevistoQuintoUno", DbType.String, TolleranzaPiuValorePrevistoQuintoUno)
            _db.AddInParameter(insertCommandQuintoPezzo, "TolleranzaPiuValorePrevistoQuintoDue", DbType.String, TolleranzaPiuValorePrevistoQuintoDue)
            _db.AddInParameter(insertCommandQuintoPezzo, "TolleranzaPiuValorePrevistoQuintoTre", DbType.String, TolleranzaPiuValorePrevistoQuintoTre)
            _db.AddInParameter(insertCommandQuintoPezzo, "TolleranzaPiuValorePrevistoQuintoQuattro", DbType.String, TolleranzaPiuValorePrevistoQuintoQuattro)
            _db.AddInParameter(insertCommandQuintoPezzo, "TolleranzaPiuValorePrevistoQuintoCinque", DbType.String, TolleranzaPiuValorePrevistoQuintoCinque)
            _db.AddInParameter(insertCommandQuintoPezzo, "TolleranzaPiuValorePrevistoQuintoSei", DbType.String, TolleranzaPiuValorePrevistoQuintoSei)
            _db.AddInParameter(insertCommandQuintoPezzo, "TolleranzaPiuValorePrevistoQuintoSette", DbType.String, TolleranzaPiuValorePrevistoQuintoSette)
            _db.AddInParameter(insertCommandQuintoPezzo, "TolleranzaPiuValorePrevistoQuintoOtto", DbType.String, TolleranzaPiuValorePrevistoQuintoOtto)
            _db.AddInParameter(insertCommandQuintoPezzo, "TolleranzaPiuValorePrevistoQuintoNove", DbType.String, TolleranzaPiuValorePrevistoQuintoNove)
            _db.AddInParameter(insertCommandQuintoPezzo, "TolleranzaPiuValorePrevistoQuintoDieci", DbType.String, TolleranzaPiuValorePrevistoQuintoDieci)

            _db.AddInParameter(insertCommandQuintoPezzo, "TolleranzaMenoValorePrevistoQuintoUno", DbType.String, TolleranzaMenoValorePrevistoQuintoUno)
            _db.AddInParameter(insertCommandQuintoPezzo, "TolleranzaMenoValorePrevistoQuintoDue", DbType.String, TolleranzaMenoValorePrevistoQuintoDue)
            _db.AddInParameter(insertCommandQuintoPezzo, "TolleranzaMenoValorePrevistoQuintoTre", DbType.String, TolleranzaMenoValorePrevistoQuintoTre)
            _db.AddInParameter(insertCommandQuintoPezzo, "TolleranzaMenoValorePrevistoQuintoQuattro", DbType.String, TolleranzaMenoValorePrevistoQuintoQuattro)
            _db.AddInParameter(insertCommandQuintoPezzo, "TolleranzaMenoValorePrevistoQuintoCinque", DbType.String, TolleranzaMenoValorePrevistoQuintoCinque)
            _db.AddInParameter(insertCommandQuintoPezzo, "TolleranzaMenoValorePrevistoQuintoSei", DbType.String, TolleranzaMenoValorePrevistoQuintoSei)
            _db.AddInParameter(insertCommandQuintoPezzo, "TolleranzaMenoValorePrevistoQuintoSette", DbType.String, TolleranzaMenoValorePrevistoQuintoSette)
            _db.AddInParameter(insertCommandQuintoPezzo, "TolleranzaMenoValorePrevistoQuintoOtto", DbType.String, TolleranzaMenoValorePrevistoQuintoOtto)
            _db.AddInParameter(insertCommandQuintoPezzo, "TolleranzaMenoValorePrevistoQuintoNove", DbType.String, TolleranzaMenoValorePrevistoQuintoNove)
            _db.AddInParameter(insertCommandQuintoPezzo, "TolleranzaMenoValorePrevistoQuintoDieci", DbType.String, TolleranzaMenoValorePrevistoQuintoDieci)
            _db.AddInParameter(insertCommandQuintoPezzo, "NoteQuintoPezzo", DbType.String, NoteQuintoPezzo)

            rowsAffectedQuintoPezzo = _db.ExecuteNonQuery(insertCommandQuintoPezzo)

        Catch ex As Exception
            MessageBox.Show("Errore Pezzo 5 : " & ex.Message)
        End Try

        Return rowsAffectedQuintoPezzo

    End Function

#End Region

#Region "Salva Ultimo Pezzo Nuovo"


    Public Function insertIDUltimoPezzo() As Integer
        Dim intContatoreUltimoPezzo As Integer = propIdTestataDaReportEsistente
        Dim result As DialogResult

        Dim IDPezziUltimoPezzo As Integer = intContatoreUltimoPezzo
        Dim IDIntestazione As Integer = intContatoreUltimoPezzo

        Try
            Dim retUltimoSoloID As Nullable(Of Integer) = InsertPezziUtimoPezzoSoloID(IDPezziUltimoPezzo, IDIntestazione)

            If Not retUltimoSoloID Is Nothing Then
                Return retUltimoSoloID
            End If
        Catch ex As Exception
            MessageBox.Show("Errore Insert Solo ID Ultimo Pz. :" & ex.Message)
        End Try
    End Function

    Public Function InsertPezziUtimoPezzoSoloID(IDPezziUltimoPezzo As Integer, IDIntestazione As Integer) As Integer


        Dim insertCommandUltimoPezzoSoloID As DbCommand = Nothing
        Dim rowsAffectedUltimoPezzoSoloID As Integer = Nothing

        Dim strQuery As String = "INSERT INTO [dbo].[tblPezziUltimoPezzo] ([IDPezziUltimoPezzo],[IDIntestazione]) VALUES (@IDPezziUltimoPezzo,@IDIntestazione)"

        Try
            insertCommandUltimoPezzoSoloID = _db.GetSqlStringCommand(strQuery)


            _db.AddInParameter(insertCommandUltimoPezzoSoloID, "IDPezziUltimoPezzo", DbType.Int32, IDPezziUltimoPezzo)
            _db.AddInParameter(insertCommandUltimoPezzoSoloID, "IDIntestazione", DbType.Int32, IDIntestazione)

            rowsAffectedUltimoPezzoSoloID = _db.ExecuteNonQuery(insertCommandUltimoPezzoSoloID)

        Catch ex As Exception
            MessageBox.Show("Errore Insert Solo ID Ultimo Pz. Query :" & ex.Message)
        End Try

        Return rowsAffectedUltimoPezzoSoloID

    End Function



    Public Function SalvaUltimoPezzo() As Integer

        Dim intContatoreUltimoPezzo As Integer = propIdTestataDaReportEsistente
        Dim ValorePrevistoUltimoPezzoUno As String
        Dim ValorePrevistoUltimoPezzoDue As String
        Dim ValorePrevistoUltimoPezzoTre As String
        Dim ValorePrevistoUltimoPezzoQuattro As String
        Dim ValorePrevistoUltimoPezzoCinque As String
        Dim ValorePrevistoUltimoPezzoSei As String
        Dim ValorePrevistoUltimoPezzoSette As String
        Dim ValorePrevistoUltimoPezzoOtto As String
        Dim ValorePrevistoUltimoPezzoNove As String
        Dim ValorePrevistoUltimoPezzoDieci As String
        Dim ValoreMisuratoUltimoPezzoUno As String
        Dim ValoreMisuratoUltimoPezzoDue As String

        Dim IDPezziUltimoPezzo As Integer
        Dim IDIntestazione As Integer

        Dim result As DialogResult

        Try

            IDPezziUltimoPezzo = intContatoreUltimoPezzo
            IDIntestazione = intContatoreUltimoPezzo
            ValorePrevistoUltimoPezzoUno = txtValPrev1UltimoPz.Text
            ValorePrevistoUltimoPezzoDue = txtValPrev2UltimoPz.Text

            ValorePrevistoUltimoPezzoTre = txtValPrev3UltimoPz.Text
            ValorePrevistoUltimoPezzoQuattro = txtValPrev4UltimoPz.Text
            ValorePrevistoUltimoPezzoCinque = txtValPrev5UltimoPz.Text
            ValorePrevistoUltimoPezzoSei = txtValPrev6UltimoPz.Text
            ValorePrevistoUltimoPezzoSette = txtValPrev7UltimoPz.Text
            ValorePrevistoUltimoPezzoOtto = txtValPrev8UltimoPz.Text
            ValorePrevistoUltimoPezzoNove = txtValPrev9UltimoPz.Text
            ValorePrevistoUltimoPezzoDieci = txtValPrev10UltimoPz.Text

            ValoreMisuratoUltimoPezzoUno = txt1ValMisUltimoPz.Text
            ValoreMisuratoUltimoPezzoDue = txt2ValMisUltimoPz.Text

            Dim ValoreMisuratoUltimoPezzoTre As String = txt3ValMisUltimoPz.Text
            Dim ValoreMisuratoUltimoPezzoQuattro As String = txt4ValMisUltimoPz.Text
            Dim ValoreMisuratoUltimoPezzoCinque As String = txt5ValMisUltimoPz.Text
            Dim ValoreMisuratoUltimoPezzoSei As String = txt6ValMisUltimoPz.Text
            Dim ValoreMisuratoUltimoPezzoSette As String = txt7ValMisUltimoPz.Text
            Dim ValoreMisuratoUltimoPezzoOtto As String = txt8ValMisUltimoPz.Text
            Dim ValoreMisuratoUltimoPezzoNove As String = txt9ValMisUltimoPz.Text
            Dim ValoreMisuratoUltimoPezzoDieci As String = txt10ValMisUltimoPz.Text

            Dim TolleranzaPiuValorePrevistoUltimoUno As String = txtTolleranzaPiuValPrev1UltimoPz.Text
            Dim TolleranzaPiuValorePrevistoUltimoDue As String = txtTolleranzaPiuValPrev2UltimoPz.Text
            Dim TolleranzaPiuValorePrevistoUltimoTre As String = txtTolleranzaPiuValPrev3UltimoPz.Text
            Dim TolleranzaPiuValorePrevistoUltimoQuattro As String = txtTolleranzaPiuValPrev4UltimoPz.Text
            Dim TolleranzaPiuValorePrevistoUltimoCinque As String = txtTolleranzaPiuValPrev5UltimoPz.Text
            Dim TolleranzaPiuValorePrevistoUltimoSei As String = txtTolleranzaPiuValPrev6UltimoPz.Text
            Dim TolleranzaPiuValorePrevistoUltimoSette As String = txtTolleranzaPiuValPrev7UltimoPz.Text
            Dim TolleranzaPiuValorePrevistoUltimoOtto As String = txtTolleranzaPiuValPrev8UltimoPz.Text
            Dim TolleranzaPiuValorePrevistoUltimoNove As String = txtTolleranzaPiuValPrev9UltimoPz.Text
            Dim TolleranzaPiuValorePrevistoUltimoDieci As String = txtTolleranzaPiuValPrev10UltimoPz.Text

            Dim TolleranzaMenoValorePrevistoUltimoUno As String = txtTolleranzaMenoValPrev1UltimoPz.Text
            Dim TolleranzaMenoValorePrevistoUltimoDue As String = txtTolleranzaMenoValPrev2UltimoPz.Text
            Dim TolleranzaMenoValorePrevistoUltimoTre As String = txtTolleranzaMenoValPrev3UltimoPz.Text
            Dim TolleranzaMenoValorePrevistoUltimoQuattro As String = txtTolleranzaMenoValPrev4UltimoPz.Text
            Dim TolleranzaMenoValorePrevistoUltimoCinque As String = txtTolleranzaMenoValPrev5UltimoPz.Text
            Dim TolleranzaMenoValorePrevistoUltimoSei As String = txtTolleranzaMenoValPrev6UltimoPz.Text
            Dim TolleranzaMenoValorePrevistoUltimoSette As String = txtTolleranzaMenoValPrev7UltimoPz.Text
            Dim TolleranzaMenoValorePrevistoUltimoOtto As String = txtTolleranzaMenoValPrev8UltimoPz.Text
            Dim TolleranzaMenoValorePrevistoUltimoNove As String = txtTolleranzaMenoValPrev9UltimoPz.Text
            Dim TolleranzaMenoValorePrevistoUltimoDieci As String = txtTolleranzaMenoValPrev10UltimoPz.Text

            Dim NoteUltimoPezzo As String = txtNoteUltimoPz.Text


            Dim retUltimoPezzo As Nullable(Of Integer) = InsertPezziUltimoPezzo(IDPezziUltimoPezzo, IDIntestazione, ValorePrevistoUltimoPezzoUno,
                        ValorePrevistoUltimoPezzoDue, ValorePrevistoUltimoPezzoTre, ValorePrevistoUltimoPezzoQuattro,
                        ValorePrevistoUltimoPezzoCinque, ValorePrevistoUltimoPezzoSei, ValorePrevistoUltimoPezzoSette,
                        ValorePrevistoUltimoPezzoOtto, ValorePrevistoUltimoPezzoNove, ValorePrevistoUltimoPezzoDieci,
                        ValoreMisuratoUltimoPezzoUno, ValoreMisuratoUltimoPezzoDue, ValoreMisuratoUltimoPezzoTre,
                        ValoreMisuratoUltimoPezzoQuattro, ValoreMisuratoUltimoPezzoCinque, ValoreMisuratoUltimoPezzoSei,
                        ValoreMisuratoUltimoPezzoSette, ValoreMisuratoUltimoPezzoOtto, ValoreMisuratoUltimoPezzoNove,
                        ValoreMisuratoUltimoPezzoDieci,
                        TolleranzaPiuValorePrevistoUltimoUno,
                        TolleranzaPiuValorePrevistoUltimoDue,
                        TolleranzaPiuValorePrevistoUltimoTre,
                        TolleranzaPiuValorePrevistoUltimoQuattro,
                        TolleranzaPiuValorePrevistoUltimoCinque,
                        TolleranzaPiuValorePrevistoUltimoSei,
                        TolleranzaPiuValorePrevistoUltimoSette,
                        TolleranzaPiuValorePrevistoUltimoOtto,
                        TolleranzaPiuValorePrevistoUltimoNove,
                        TolleranzaPiuValorePrevistoUltimoDieci,
                        TolleranzaMenoValorePrevistoUltimoUno,
                        TolleranzaMenoValorePrevistoUltimoDue,
                        TolleranzaMenoValorePrevistoUltimoTre,
                        TolleranzaMenoValorePrevistoUltimoQuattro,
                        TolleranzaMenoValorePrevistoUltimoCinque,
                        TolleranzaMenoValorePrevistoUltimoSei,
                        TolleranzaMenoValorePrevistoUltimoSette,
                        TolleranzaMenoValorePrevistoUltimoOtto,
                        TolleranzaMenoValorePrevistoUltimoNove,
                        TolleranzaMenoValorePrevistoUltimoDieci,
                        NoteUltimoPezzo)

            If Not retUltimoPezzo Is Nothing Then
                Return retUltimoPezzo
            End If

        Catch ex As Exception
            MessageBox.Show("Errore Ultimo Insert Pezzo :" & ex.Message)
        End Try

    End Function

    Public Function InsertPezziUltimoPezzo(IDPezziUltimoPezzo As Integer,
IDIntestazione As Integer,
ValorePrevistoUltimoPezzoUno As String,
ValorePrevistoUltimoPezzoDue As String,
ValorePrevistoUltimoPezzoTre As String,
ValorePrevistoUltimoPezzoQuattro As String,
ValorePrevistoUltimoPezzoCinque As String,
ValorePrevistoUltimoPezzoSei As String,
ValorePrevistoUltimoPezzoSette As String,
ValorePrevistoUltimoPezzoOtto As String,
ValorePrevistoUltimoPezzoNove As String,
ValorePrevistoUltimoPezzoDieci As String,
ValoreMisuratoUltimoPezzoUno As String,
ValoreMisuratoUltimoPezzoDue As String,
ValoreMisuratoUltimoPezzoTre As String,
ValoreMisuratoUltimoPezzoQuattro As String,
ValoreMisuratoUltimoPezzoCinque As String,
ValoreMisuratoUltimoPezzoSei As String,
ValoreMisuratoUltimoPezzoSette As String,
ValoreMisuratoUltimoPezzoOtto As String,
ValoreMisuratoUltimoPezzoNove As String,
ValoreMisuratoUltimoPezzoDieci As String,
TolleranzaPiuValorePrevistoUltimoUno As String,
TolleranzaPiuValorePrevistoUltimoDue As String,
TolleranzaPiuValorePrevistoUltimoTre As String,
TolleranzaPiuValorePrevistoUltimoQuattro As String,
TolleranzaPiuValorePrevistoUltimoCinque As String,
TolleranzaPiuValorePrevistoUltimoSei As String,
TolleranzaPiuValorePrevistoUltimoSette As String,
TolleranzaPiuValorePrevistoUltimoOtto As String,
TolleranzaPiuValorePrevistoUltimoNove As String,
TolleranzaPiuValorePrevistoUltimoDieci As String,
TolleranzaMenoValorePrevistoUltimoUno As String,
TolleranzaMenoValorePrevistoUltimoDue As String,
TolleranzaMenoValorePrevistoUltimoTre As String,
TolleranzaMenoValorePrevistoUltimoQuattro As String,
TolleranzaMenoValorePrevistoUltimoCinque As String,
TolleranzaMenoValorePrevistoUltimoSei As String,
TolleranzaMenoValorePrevistoUltimoSette As String,
TolleranzaMenoValorePrevistoUltimoOtto As String,
TolleranzaMenoValorePrevistoUltimoNove As String,
TolleranzaMenoValorePrevistoUltimoDieci As String,
NoteUltimoPezzo As String) As Integer


        Dim insertCommandUltimoPezzo As DbCommand = Nothing
        Dim rowsAffectedUltimoPezzo As Integer = Nothing

        Dim strQuery As String = "INSERT INTO [dbo].[tblPezziUltimoPezzo]" &
                     "([IDPezziUltimoPezzo],[IDIntestazione],[ValorePrevistoUltimoPezzoUno], " &
                    "[ValorePrevistoUltimoPezzoDue],[ValorePrevistoUltimoPezzoTre], [ValorePrevistoUltimoPezzoQuattro]," &
                    "[ValorePrevistoUltimoPezzoCinque] ," &
                    "[ValorePrevistoUltimoPezzoSei], " &
                    "[ValorePrevistoUltimoPezzoSette]," &
                    "[ValorePrevistoUltimoPezzoOtto]," &
                    "[ValorePrevistoUltimoPezzoNove]," &
                    "[ValorePrevistoUltimoPezzoDieci]," &
                    "[ValoreMisuratoUltimoPezzoUno]," &
                    "[ValoreMisuratoUltimoPezzoDue]," &
                    "[ValoreMisuratoUltimoPezzoTre]," &
                    "[ValoreMisuratoUltimoPezzoQuattro]," &
                    "[ValoreMisuratoUltimoPezzocinque]," &
                    "[ValoreMisuratoUltimoPezzoSei]," &
                    "[ValoreMisuratoUltimoPezzoSette]," &
                    "[ValoreMisuratoUltimoPezzoOtto]," &
                    "[ValoreMisuratoUltimoPezzoNove]," &
                    "[ValoreMisuratoUltimoPezzoDieci]," &
                    "[TolleranzaPiuValorePrevistoUltimoUno]," &
                    "[TolleranzaPiuValorePrevistoUltimoDue]," &
                    "[TolleranzaPiuValorePrevistoUltimoTre]," &
                    "[TolleranzaPiuValorePrevistoUltimoQuattro]," &
                    "[TolleranzaPiuValorePrevistoUltimoCinque]," &
                    "[TolleranzaPiuValorePrevistoUltimoSei]," &
                    "[TolleranzaPiuValorePrevistoUltimoSette]," &
                    "[TolleranzaPiuValorePrevistoUltimoOtto]," &
                    "[TolleranzaPiuValorePrevistoUltimoNove]," &
                    "[TolleranzaPiuValorePrevistoUltimoDieci]," &
                    "[TolleranzaMenoValorePrevistoUltimoUno]," &
                    "[TolleranzaMenoValorePrevistoUltimoDue]," &
                    "[TolleranzaMenoValorePrevistoUltimoTre]," &
                    "[TolleranzaMenoValorePrevistoUltimoQuattro]," &
                    "[TolleranzaMenoValorePrevistoUltimoCinque]," &
                    "[TolleranzaMenoValorePrevistoUltimoSei]," &
                    "[TolleranzaMenoValorePrevistoUltimoSette]," &
                    "[TolleranzaMenoValorePrevistoUltimoOtto]," &
                    "[TolleranzaMenoValorePrevistoUltimoNove]," &
                    "[TolleranzaMenoValorePrevistoUltimoDieci]," &
                     "[NoteUltimoPezzo]) VALUES (@IDPezziUltimoPezzo,@IDIntestazione, " &
                       "@ValorePrevistoUltimoPezzoUno, " &
                       "@ValorePrevistoUltimoPezzoDue, " &
                       "@ValorePrevistoUltimoPezzoTre, " &
                       "@ValorePrevistoUltimoPezzoQuattro, " &
                       "@ValorePrevistoUltimoPezzoCinque, " &
                       "@ValorePrevistoUltimoPezzoSei, " &
                       "@ValorePrevistoUltimoPezzoSette, " &
                       "@ValorePrevistoUltimoPezzoOtto, " &
                       "@ValorePrevistoUltimoPezzoNove, " &
                       "@ValorePrevistoUltimoPezzoDieci, " &
                       "@ValoreMisuratoUltimoPezzoUno, " &
                       "@ValoreMisuratoUltimoPezzoDue, " &
                       "@ValoreMisuratoUltimoPezzoTre, " &
                       "@ValoreMisuratoUltimoPezzoQuattro, " &
                       "@ValoreMisuratoUltimoPezzoCinque, " &
                       "@ValoreMisuratoUltimoPezzoSei, " &
                       "@ValoreMisuratoUltimoPezzoSette, " &
                       "@ValoreMisuratoUltimoPezzoOtto, " &
                       "@ValoreMisuratoUltimoPezzoNove, " &
                       "@ValoreMisuratoUltimoPezzoDieci, " &
                       "@TolleranzaPiuValorePrevistoUltimoUno, " &
                       "@TolleranzaPiuValorePrevistoUltimoDue, " &
                       "@TolleranzaPiuValorePrevistoUltimoTre, " &
                       "@TolleranzaPiuValorePrevistoUltimoQuattro, " &
                       "@TolleranzaPiuValorePrevistoUltimoCinque, " &
                       "@TolleranzaPiuValorePrevistoUltimoSei, " &
                       "@TolleranzaPiuValorePrevistoUltimoSette, " &
                       "@TolleranzaPiuValorePrevistoUltimoOtto, " &
                       "@TolleranzaPiuValorePrevistoUltimoNove, " &
                       "@TolleranzaPiuValorePrevistoUltimoDieci, " &
                       "@TolleranzaMenoValorePrevistoUltimoUno, " &
                       "@TolleranzaMenoValorePrevistoUltimoDue, " &
                       "@TolleranzaMenoValorePrevistoUltimoTre, " &
                       "@TolleranzaMenoValorePrevistoUltimoQuattro, " &
                       "@TolleranzaMenoValorePrevistoUltimoCinque, " &
                       "@TolleranzaMenoValorePrevistoUltimoSei, " &
                       "@TolleranzaMenoValorePrevistoUltimoSette, " &
                       "@TolleranzaMenoValorePrevistoUltimoOtto, " &
                       "@TolleranzaMenoValorePrevistoUltimoNove, " &
                       "@TolleranzaMenoValorePrevistoUltimoDieci, " &
                       "@NoteUltimoPezzo)"

        Try

            insertCommandUltimoPezzo = _db.GetSqlStringCommand(strQuery)


            _db.AddInParameter(insertCommandUltimoPezzo, "IDPezziUltimoPezzo", DbType.Int32, IDPezziUltimoPezzo)
            _db.AddInParameter(insertCommandUltimoPezzo, "IDIntestazione", DbType.Int32, IDIntestazione)

            _db.AddInParameter(insertCommandUltimoPezzo, "ValorePrevistoUltimoPezzoUno", DbType.String, ValorePrevistoUltimoPezzoUno)
            _db.AddInParameter(insertCommandUltimoPezzo, "ValorePrevistoUltimoPezzoDue", DbType.String, ValorePrevistoUltimoPezzoDue)
            _db.AddInParameter(insertCommandUltimoPezzo, "ValorePrevistoUltimoPezzoTre", DbType.String, ValorePrevistoUltimoPezzoTre)
            _db.AddInParameter(insertCommandUltimoPezzo, "ValorePrevistoUltimoPezzoQuattro", DbType.String, ValorePrevistoUltimoPezzoQuattro)
            _db.AddInParameter(insertCommandUltimoPezzo, "ValorePrevistoUltimoPezzoCinque", DbType.String, ValorePrevistoUltimoPezzoCinque)
            _db.AddInParameter(insertCommandUltimoPezzo, "ValorePrevistoUltimoPezzoSei", DbType.String, ValorePrevistoUltimoPezzoSei)
            _db.AddInParameter(insertCommandUltimoPezzo, "ValorePrevistoUltimoPezzoSette", DbType.String, ValorePrevistoUltimoPezzoSette)
            _db.AddInParameter(insertCommandUltimoPezzo, "ValorePrevistoUltimoPezzoOtto", DbType.String, ValorePrevistoUltimoPezzoOtto)
            _db.AddInParameter(insertCommandUltimoPezzo, "ValorePrevistoUltimoPezzoNove", DbType.String, ValorePrevistoUltimoPezzoNove)
            _db.AddInParameter(insertCommandUltimoPezzo, "ValorePrevistoUltimoPezzoDieci", DbType.String, ValorePrevistoUltimoPezzoDieci)

            _db.AddInParameter(insertCommandUltimoPezzo, "ValoreMisuratoUltimoPezzoUno", DbType.String, ValoreMisuratoUltimoPezzoUno)
            _db.AddInParameter(insertCommandUltimoPezzo, "ValoreMisuratoUltimoPezzoDue", DbType.String, ValoreMisuratoUltimoPezzoDue)
            _db.AddInParameter(insertCommandUltimoPezzo, "ValoreMisuratoUltimoPezzoTre", DbType.String, ValoreMisuratoUltimoPezzoTre)
            _db.AddInParameter(insertCommandUltimoPezzo, "ValoreMisuratoUltimoPezzoQuattro", DbType.String, ValoreMisuratoUltimoPezzoQuattro)
            _db.AddInParameter(insertCommandUltimoPezzo, "ValoreMisuratoUltimoPezzoCinque", DbType.String, ValoreMisuratoUltimoPezzoCinque)
            _db.AddInParameter(insertCommandUltimoPezzo, "ValoreMisuratoUltimoPezzoSei", DbType.String, ValoreMisuratoUltimoPezzoSei)
            _db.AddInParameter(insertCommandUltimoPezzo, "ValoreMisuratoUltimoPezzoSette", DbType.String, ValoreMisuratoUltimoPezzoSette)
            _db.AddInParameter(insertCommandUltimoPezzo, "ValoreMisuratoUltimoPezzoOtto", DbType.String, ValoreMisuratoUltimoPezzoOtto)
            _db.AddInParameter(insertCommandUltimoPezzo, "ValoreMisuratoUltimoPezzoNove", DbType.String, ValoreMisuratoUltimoPezzoNove)
            _db.AddInParameter(insertCommandUltimoPezzo, "ValoreMisuratoUltimoPezzoDieci", DbType.String, ValoreMisuratoUltimoPezzoDieci)

            _db.AddInParameter(insertCommandUltimoPezzo, "TolleranzaPiuValorePrevistoUltimoUno", DbType.String, TolleranzaPiuValorePrevistoUltimoUno)
            _db.AddInParameter(insertCommandUltimoPezzo, "TolleranzaPiuValorePrevistoUltimoDue", DbType.String, TolleranzaPiuValorePrevistoUltimoDue)
            _db.AddInParameter(insertCommandUltimoPezzo, "TolleranzaPiuValorePrevistoUltimoTre", DbType.String, TolleranzaPiuValorePrevistoUltimoTre)
            _db.AddInParameter(insertCommandUltimoPezzo, "TolleranzaPiuValorePrevistoUltimoQuattro", DbType.String, TolleranzaPiuValorePrevistoUltimoQuattro)
            _db.AddInParameter(insertCommandUltimoPezzo, "TolleranzaPiuValorePrevistoUltimoCinque", DbType.String, TolleranzaPiuValorePrevistoUltimoCinque)
            _db.AddInParameter(insertCommandUltimoPezzo, "TolleranzaPiuValorePrevistoUltimoSei", DbType.String, TolleranzaPiuValorePrevistoUltimoSei)
            _db.AddInParameter(insertCommandUltimoPezzo, "TolleranzaPiuValorePrevistoUltimoSette", DbType.String, TolleranzaPiuValorePrevistoUltimoSette)
            _db.AddInParameter(insertCommandUltimoPezzo, "TolleranzaPiuValorePrevistoUltimoOtto", DbType.String, TolleranzaPiuValorePrevistoUltimoOtto)
            _db.AddInParameter(insertCommandUltimoPezzo, "TolleranzaPiuValorePrevistoUltimoNove", DbType.String, TolleranzaPiuValorePrevistoUltimoNove)
            _db.AddInParameter(insertCommandUltimoPezzo, "TolleranzaPiuValorePrevistoUltimoDieci", DbType.String, TolleranzaPiuValorePrevistoUltimoDieci)

            _db.AddInParameter(insertCommandUltimoPezzo, "TolleranzaMenoValorePrevistoUltimoUno", DbType.String, TolleranzaMenoValorePrevistoUltimoUno)
            _db.AddInParameter(insertCommandUltimoPezzo, "TolleranzaMenoValorePrevistoUltimoDue", DbType.String, TolleranzaMenoValorePrevistoUltimoDue)
            _db.AddInParameter(insertCommandUltimoPezzo, "TolleranzaMenoValorePrevistoUltimoTre", DbType.String, TolleranzaMenoValorePrevistoUltimoTre)
            _db.AddInParameter(insertCommandUltimoPezzo, "TolleranzaMenoValorePrevistoUltimoQuattro", DbType.String, TolleranzaMenoValorePrevistoUltimoQuattro)
            _db.AddInParameter(insertCommandUltimoPezzo, "TolleranzaMenoValorePrevistoUltimoCinque", DbType.String, TolleranzaMenoValorePrevistoUltimoCinque)
            _db.AddInParameter(insertCommandUltimoPezzo, "TolleranzaMenoValorePrevistoUltimoSei", DbType.String, TolleranzaMenoValorePrevistoUltimoSei)
            _db.AddInParameter(insertCommandUltimoPezzo, "TolleranzaMenoValorePrevistoUltimoSette", DbType.String, TolleranzaMenoValorePrevistoUltimoSette)
            _db.AddInParameter(insertCommandUltimoPezzo, "TolleranzaMenoValorePrevistoUltimoOtto", DbType.String, TolleranzaMenoValorePrevistoUltimoOtto)
            _db.AddInParameter(insertCommandUltimoPezzo, "TolleranzaMenoValorePrevistoUltimoNove", DbType.String, TolleranzaMenoValorePrevistoUltimoNove)
            _db.AddInParameter(insertCommandUltimoPezzo, "TolleranzaMenoValorePrevistoUltimoDieci", DbType.String, TolleranzaMenoValorePrevistoUltimoDieci)
            _db.AddInParameter(insertCommandUltimoPezzo, "NoteUltimoPezzo", DbType.String, NoteUltimoPezzo)

            rowsAffectedUltimoPezzo = _db.ExecuteNonQuery(insertCommandUltimoPezzo)

        Catch ex As Exception
            MessageBox.Show("Errore Ultimo Insert Pezzo Query : " & ex.Message)
        End Try

        Return rowsAffectedUltimoPezzo



    End Function

#End Region



#End Region


End Class


#Region "Codice Vecchio"
'Riempie il combo del Lotto sul Load della form
'Public Sub riempiComboLotto()
'    Try
'        Dim strSQL As String = "SELECT materiale, numeroLotto, fornitore, numDDT FROM tblNumeroLottoMateriale"
'        Dim cmd As DbCommand = _db.GetSqlStringCommand(strSQL.ToString)

'        Using datareader As IDataReader = _db.ExecuteReader(cmd)
'            While datareader.Read
'                Me.cmbLottoNum.Items.Add(datareader("numeroLotto") + " - " + datareader("fornitore"))
'            End While
'        End Using

'    Catch ex As Exception
'        MessageBox.Show("Errore : " & ex.Message)
'    End Try
'End Sub

'Riempie il combo del Lotto sul sul select del combo
'Private Sub cmbMateriale_SelectedIndexChanged(sender As Object, e As EventArgs)
'    Dim str As String
'    Dim strArr() As String
'    Dim count As Integer
'    Dim Materiale As String

'    Dim selMateriale As String = CType(cmbMateriale.SelectedItem, String)
'    riempiComboLottoByMateriale(selMateriale)
'End Sub

'Public Sub riempiComboLottoByMateriale(ByVal materiale As String)
'    Try
'        Dim cmd As DbCommand
'        'str = selMateriale
'        'strArr = str.Split("-")
'        'For count = 0 To strArr.Length - 1
'        '    Materiale = CType(Trim(strArr(0)), String)
'        'Next

'        If materiale = "Anticorodal" Then
'            cmbLottoNum.SelectedIndex = -1
'            Dim strSQL As String = "spGetLottoAnticorodalByMateriale"
'            cmd = _db.GetStoredProcCommand(strSQL)
'            _db.AddInParameter(cmd, "materiale", DbType.String, materiale)
'            Using datareader As IDataReader = _db.ExecuteReader(cmd)
'                While datareader.Read
'                    Me.cmbLottoNum.Items.Add(datareader("numeroLotto") + " - " + datareader("fornitore"))
'                End While
'            End Using
'        End If

'        If materiale <> "Anticorodal" Then
'            cmbLottoNum.SelectedIndex = -1
'            Dim strSQL As String = "spGetMaterialePerLottoDiversoDaAnticorodal"
'            cmd = _db.GetStoredProcCommand(strSQL)
'            Using datareader As IDataReader = _db.ExecuteReader(cmd)
'                While datareader.Read
'                    Me.cmbLottoNum.Items.Add(datareader("numeroLotto") + " - " + datareader("fornitore"))
'                End While
'            End Using
'        End If

'    Catch ex As Exception
'        MessageBox.Show("Errore selezione materiale per Lotto : " & ex.Message)
'    End Try
'End Sub


#End Region