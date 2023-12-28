Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.IO
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data


Public Class frmAggiornaTestReportTre

#Region "Proprieta"
    Public Shared factory As New DatabaseProviderFactory()
    Public Property _db As Database = factory.Create("dbConnStrRete")
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


    Private Sub frmAggiornaTestReportTre_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            'Vanno popolati per primi per la selezione del lotto
            RiempiComboMateriali()
            RiempiComboFornitori()

            'riempiComboLotto()
            getTestataByIdIntestazione(propIdIntestazione)
            getPrimoPezzoByIdIntestazione(propIdIntestazione)
            getSecondoPezzoByIdIntestazione(propIdIntestazione)
            getTerzoPezzoByIdIntestazione(propIdIntestazione)
            getQuartoPezzoByIdIntestazione(propIdIntestazione)
            getQuintoPezzoByIdIntestazione(propIdIntestazione)
            getUltimoPezzoByIdIntestazione(propIdIntestazione)


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
            cmbLottoNum.Text = propLottoNumero

        Catch ex As Exception
            MessageBox.Show("Errore frmAggiornaTestReportTre_Load : " & ex.Message)
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
            If txtValPrev1Pz1.Focused Then
                genTLP = CType(TableLayoutPanelPrimoPz, TableLayoutPanel)
                Return genTLP
            End If

            If txt1ValMisPz1.Focused Then
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

    Private Sub cmbFornitore_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFornitore.SelectedIndexChanged
        Dim str As String
        Dim strArr() As String
        Dim count As Integer
        Dim Fornitore As String
        Dim selFornitore As String = CType(cmbFornitore.SelectedItem, String)
        RiempiComboLottoByFornitore(selFornitore)
    End Sub

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
                cmbMacchNum.Items.AddRange(File.ReadAllLines(fileName))
            Else
                MessageBox.Show("Errore lettura file macchine")
            End If
        Catch ex As Exception
            MessageBox.Show("Errore scrittura file : " & ex.Message)
        End Try
    End Sub


#End Region

    Private Sub CommandBarButtonChiudi_Click(sender As Object, e As EventArgs) Handles CommandBarButtonChiudi.Click
        boolReturnMain = True
        Close()
    End Sub


#Region "FUNZIONI UTILITY"

    Public Function Convert(value As Date) As Date
        Dim DateValue As DateTime = CType(value, DateTime)
        Return DateValue.ToShortDateString
    End Function

    Public Function contatore() As Integer
        Dim i As Integer?

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
            MessageBox.Show("Errore : " & ex.Message)
        End Try

        Return i

    End Function


    Public Shared Function FindControlRecursive(list As List(Of Control), parent As Control, ctrlType As Type) As List(Of Control)
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

    Private Sub CommandBarButtonAggiornaTestReport_Click(sender As Object, e As EventArgs) Handles CommandBarButtonAggiornaTestReport.Click

        Dim result As DialogResult
        Dim intAggiornaTestataTestReportBusLayer As Integer?
        Dim intPrimoPezzo As Integer?
        Dim intSecondoPezzo As Integer?
        Dim intTerzoPezzo As Integer?
        Dim intQuartoPezzo As Integer?
        Dim intQuintoPezzo As Integer?
        Dim intUltimoPezzo As Integer?

        Try

            intAggiornaTestataTestReportBusLayer = AggiornaTestataReportBusinessLayer(propIdIntestazione)

            If checkIDPrimoPezzo(propIdIntestazione) = 0 Then
                insertIDPrimoPezzo()
                AggiornaPrimoPezzoBusinessLayer(propIdIntestazione)
            Else
                AggiornaPrimoPezzoBusinessLayer(propIdIntestazione)
            End If

            If checkIDSecondoPezzo(propIdIntestazione) = 0 Then
                insertIDSecondoPezzo()
                AggiornaSecondoPezzoBusinessLayer(propIdIntestazione)
            Else
                AggiornaSecondoPezzoBusinessLayer(propIdIntestazione)
            End If

            If checkIDTerzoPezzo(propIdIntestazione) = 0 Then
                insertIDTerzoPezzo()
                AggiornaTerzoPezzoBusinessLayer(propIdIntestazione)
            Else
                AggiornaTerzoPezzoBusinessLayer(propIdIntestazione)
            End If

            If checkIDQuartoPezzo(propIdIntestazione) = 0 Then
                insertIDQuartoPezzo()
                AggiornaQuartoPezzoBusinessLayer(propIdIntestazione)
            Else
                AggiornaQuartoPezzoBusinessLayer(propIdIntestazione)
            End If

            If checkIDQuintoPezzo(propIdIntestazione) = 0 Then
                insertIDQuintoPezzo()
                AggiornaQuintoPezzoBusinessLayer(propIdIntestazione)
            Else
                AggiornaQuintoPezzoBusinessLayer(propIdIntestazione)
            End If

            If checkIDUltimoPezzo(propIdIntestazione) = 0 Then
                insertIDUltimoPezzo()
                AggiornaUltimoPezzoBusinessLayer(propIdIntestazione)
            Else
                AggiornaUltimoPezzoBusinessLayer(propIdIntestazione)
            End If

            If Not intAggiornaTestataTestReportBusLayer Is Nothing Or Not intUltimoPezzo Is Nothing Then
                result = MessageBox.Show("Test Report Aggiornato Con Successo", "Aggiornamento Test Report in Database", MessageBoxButtons.OKCancel)
                If result = DialogResult.Cancel Then
                    Exit Sub
                End If
            End If

            cmbStrumento.SelectedIndex = 0
            cmbMacchNum.SelectedIndex = 0
            cmbFornitore.SelectedIndex = 0
            cmbOperatore.SelectedIndex = 0
            cmbLottoNum.Text = String.Empty

        Catch ex As Exception
            MessageBox.Show("Errore Aggiornamento : " & ex.Message)
        End Try

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

    End Sub




#Region "Carica Dati Testata By IDIntestazione"

    Public Sub getTestataByIdIntestazione(intIdItestazione As Integer)

        Dim cmd As DbCommand = _db.GetStoredProcCommand("spGetIntestazioneByIDIntestazione")

        Try
            _db.AddInParameter(cmd, "IDIntestazione", DbType.Int32, intIdItestazione)

            Using datareader As IDataReader = _db.ExecuteReader(cmd)

                While datareader.Read

                    propIdIntestazione = datareader("IDIntestazione")
                    propDataReport = Convert(datareader("Data").ToString)
                    dtpData.Text = propDataReport
                    propNumeroOrdine = datareader("NumOrdine").ToString
                    txtNumOrdine.Text = propNumeroOrdine
                    propCodiceArticolo = datareader("CodArticolo").ToString
                    txtCodArt.Text = propCodiceArticolo

                    If Not datareader("Materiale").ToString Is Nothing Then
                        propMateriale = datareader("Materiale").ToString
                        cmbMateriale.SelectedItem = propMateriale
                    End If

                    propStrumento = datareader("Strumento").ToString
                    cmbStrumento.SelectedText = propStrumento
                    propMacchinaNum = datareader("MacchinaNum").ToString
                    cmbMacchNum.SelectedText = propMacchinaNum
                    propRigaOrdNum = datareader("RigaOrdNum").ToString
                    txtRigaNum.Text = CType(propRigaOrdNum, String)
                    propNumPezzi = datareader("NumPezzi").ToString
                    txtPezziNum.Text = propNumPezzi
                    propFornitore = datareader("Fornitore").ToString
                    cmbFornitore.SelectedText = propFornitore
                    propLottoNumero = datareader("NumLotto").ToString
                    cmbLottoNum.Text = propLottoNumero
                    propPrimoPezzo = datareader("PrimoPezzo").ToString
                    chkPrimoPezzo.Checked = propPrimoPezzo
                    propUltimoPezzo = datareader("UltimoPezzo").ToString
                    chkUltimoPezzo.Checked = propUltimoPezzo

                    propPezziControllati = datareader("PezzoNumero").ToString
                    txtPezziControllati.Text = propPezziControllati

                    propOperatore = datareader("Operatore").ToString
                    cmbOperatore.SelectedText = propOperatore
                End While
            End Using

        Catch ex As Exception
            MessageBox.Show("Errore : " & ex.Message)
        End Try

    End Sub

#End Region

#Region "Carica Dati Primo Pezzo By IDIntestazione"

    Public Sub getPrimoPezzoByIdIntestazione(intIdItestazione As Integer)

        Dim cmd As DbCommand = _db.GetStoredProcCommand("spGetPrimoPezzoByIdIntestazione")

        Try
            _db.AddInParameter(cmd, "IDIntestazione", DbType.Int32, intIdItestazione)

            Using datareader As IDataReader = _db.ExecuteReader(cmd)

                While datareader.Read
                    propValorePrevistoPrimoUno = datareader("ValorePrevistoPrimoPezzoUno").ToString
                    txtValPrev1Pz1.Text = propValorePrevistoPrimoUno
                    propValorePrevistoPrimoDue = datareader("ValorePrevistoPrimoPezzoDue").ToString
                    txtValPrev2Pz1.Text = propValorePrevistoPrimoDue
                    propValorePrevistoPrimoTre = datareader("ValorePrevistoPrimoPezzoTre").ToString
                    txtValPrev3Pz1.Text = propValorePrevistoPrimoTre
                    propValorePrevistoPrimoQuattro = datareader("ValorePrevistoPrimoPezzoQuattro").ToString
                    txtValPrev4Pz1.Text = propValorePrevistoPrimoQuattro
                    propValorePrevistoPrimoCinque = datareader("ValorePrevistoPrimoPezzoCinque").ToString
                    txtValPrev5Pz1.Text = propValorePrevistoPrimoCinque
                    propValorePrevistoPrimoSei = datareader("ValorePrevistoPrimoPezzoSei").ToString
                    txtValPrev6Pz1.Text = propValorePrevistoPrimoSei
                    propValorePrevistoPrimoSette = datareader("ValorePrevistoPrimoPezzoSette").ToString
                    txtValPrev7Pz1.Text = propValorePrevistoPrimoSette
                    propValorePrevistoPrimoOtto = datareader("ValorePrevistoPrimoPezzoOtto").ToString
                    txtValPrev8Pz1.Text = propValorePrevistoPrimoOtto
                    propValorePrevistoPrimoNove = datareader("ValorePrevistoPrimoPezzoNove").ToString
                    txtValPrev9Pz1.Text = propValorePrevistoPrimoNove
                    propValorePrevistoPrimoDieci = datareader("ValorePrevistoPrimoPezzoDieci").ToString
                    txtValPrev10Pz1.Text = propValorePrevistoPrimoDieci

                    propValoreMisuratoPrimoUno = datareader("ValoreMisuratoPrimoPezzoUno").ToString
                    txt1ValMisPz1.Text = propValoreMisuratoPrimoUno
                    propValoreMisuratoPrimoDue = datareader("ValoreMisuratoPrimoPezzoDue").ToString
                    txt2ValMisPz1.Text = propValoreMisuratoPrimoDue
                    propValoreMisuratoPrimoTre = datareader("ValoreMisuratoPrimoPezzoTre").ToString
                    txt3ValMisPz1.Text = propValoreMisuratoPrimoTre
                    propValoreMisuratoPrimoQuattro = datareader("ValoreMisuratoPrimoPezzoQuattro").ToString
                    txt4ValMisPz1.Text = propValoreMisuratoPrimoQuattro
                    propValoreMisuratoPrimoCinque = datareader("ValoreMisuratoPrimoPezzoCinque").ToString
                    txt5ValMisPz1.Text = propValoreMisuratoPrimoCinque
                    propValoreMisuratoPrimoSei = datareader("ValoreMisuratoPrimoPezzoSei").ToString
                    txt6ValMisPz1.Text = propValoreMisuratoPrimoSei
                    propValoreMisuratoPrimoSette = datareader("ValoreMisuratoPrimoPezzoSette").ToString
                    txt7ValMisPz1.Text = propValoreMisuratoPrimoSette
                    propValoreMisuratoPrimoOtto = datareader("ValoreMisuratoPrimoPezzoOtto").ToString
                    txt8ValMisPz1.Text = propValoreMisuratoPrimoOtto
                    propValoreMisuratoPrimoNove = datareader("ValoreMisuratoPrimoPezzoNove").ToString
                    txt9ValMisPz1.Text = propValoreMisuratoPrimoNove
                    propValoreMisuratoPrimoDieci = datareader("ValoreMisuratoPrimoPezzoDieci").ToString
                    txt10ValMisPz1.Text = propValoreMisuratoPrimoDieci

                    propTolleranzaPiupropValorePrevistoPrimoUno = datareader("TolleranzaPiuValorePrevistoPrimoUno").ToString
                    txtTolleranzaPiuValPrev1Pz1.Text = propTolleranzaPiupropValorePrevistoPrimoUno
                    propTolleranzaPiupropValorePrevistoPrimoDue = datareader("TolleranzaPiuValorePrevistoPrimoDue").ToString
                    txtTolleranzaPiuValPrev2Pz1.Text = propTolleranzaPiupropValorePrevistoPrimoDue
                    propTolleranzaPiupropValorePrevistoPrimoTre = datareader("TolleranzaPiuValorePrevistoPrimoTre").ToString
                    txtTolleranzaPiuValPrev3Pz1.Text = propTolleranzaPiupropValorePrevistoPrimoTre
                    propTolleranzaPiupropValorePrevistoPrimoQuattro = datareader("TolleranzaPiuValorePrevistoPrimoQuattro").ToString
                    txtTolleranzaPiuValPrev4Pz1.Text = propTolleranzaPiupropValorePrevistoPrimoQuattro
                    propTolleranzaPiupropValorePrevistoPrimoCinque = datareader("TolleranzaPiuValorePrevistoPrimoCinque").ToString
                    txtTolleranzaPiuValPrev5Pz1.Text = propTolleranzaPiupropValorePrevistoPrimoCinque
                    propTolleranzaPiupropValorePrevistoPrimoSei = datareader("TolleranzaPiuValorePrevistoPrimoSei").ToString
                    txtTolleranzaPiuValPrev6Pz1.Text = propTolleranzaPiupropValorePrevistoPrimoSei
                    propTolleranzaPiupropValorePrevistoPrimoSette = datareader("TolleranzaPiuValorePrevistoPrimoSette").ToString
                    txtTolleranzaPiuValPrev7Pz1.Text = propTolleranzaPiupropValorePrevistoPrimoSette
                    propTolleranzaPiupropValorePrevistoPrimoOtto = datareader("TolleranzaPiuValorePrevistoPrimoOtto").ToString
                    txtTolleranzaPiuValPrev8Pz1.Text = propTolleranzaPiupropValorePrevistoPrimoOtto
                    propTolleranzaPiupropValorePrevistoPrimoNove = datareader("TolleranzaPiuValorePrevistoPrimoNove").ToString
                    txtTolleranzaPiuValPrev9Pz1.Text = propTolleranzaPiupropValorePrevistoPrimoNove
                    propTolleranzaPiupropValorePrevistoPrimoDieci = datareader("TolleranzaPiuValorePrevistoPrimoDieci").ToString
                    txtTolleranzaPiuValPrev10Pz1.Text = propTolleranzaPiupropValorePrevistoPrimoDieci

                    propTolleranzaMenopropValorePrevistoPrimoUno = datareader("TolleranzaMenoValorePrevistoPrimoUno").ToString
                    txtTolleranzaMenoValPrev1Pz1.Text = propTolleranzaMenopropValorePrevistoPrimoUno
                    propTolleranzaMenopropValorePrevistoPrimoDue = datareader("TolleranzaMenoValorePrevistoPrimoDue").ToString
                    txtTolleranzaMenoValPrev2Pz1.Text = propTolleranzaMenopropValorePrevistoPrimoDue
                    propTolleranzaMenopropValorePrevistoPrimoTre = datareader("TolleranzaMenoValorePrevistoPrimoTre").ToString
                    txtTolleranzaMenoValPrev3Pz1.Text = propTolleranzaMenopropValorePrevistoPrimoTre
                    propTolleranzaMenopropValorePrevistoPrimoQuattro = datareader("TolleranzaMenoValorePrevistoPrimoQuattro").ToString
                    txtTolleranzaMenoValPrev4Pz1.Text = propTolleranzaMenopropValorePrevistoPrimoQuattro
                    propTolleranzaMenopropValorePrevistoPrimoCinque = datareader("TolleranzaMenoValorePrevistoPrimoCinque").ToString
                    txtTolleranzaMenoValPrev5Pz1.Text = propTolleranzaMenopropValorePrevistoPrimoCinque
                    propTolleranzaMenopropValorePrevistoPrimoSei = datareader("TolleranzaMenoValorePrevistoPrimoSei").ToString
                    txtTolleranzaMenoValPrev6Pz1.Text = propTolleranzaMenopropValorePrevistoPrimoSei
                    propTolleranzaMenopropValorePrevistoPrimoSette = datareader("TolleranzaMenoValorePrevistoPrimoSette").ToString
                    txtTolleranzaMenoValPrev7Pz1.Text = propTolleranzaMenopropValorePrevistoPrimoSette
                    propTolleranzaMenopropValorePrevistoPrimoOtto = datareader("TolleranzaMenoValorePrevistoPrimoOtto").ToString
                    txtTolleranzaMenoValPrev8Pz1.Text = propTolleranzaMenopropValorePrevistoPrimoOtto
                    propTolleranzaMenopropValorePrevistoPrimoNove = datareader("TolleranzaMenoValorePrevistoPrimoNove").ToString
                    txtTolleranzaMenoValPrev9Pz1.Text = propTolleranzaMenopropValorePrevistoPrimoNove
                    propTolleranzaMenopropValorePrevistoPrimoDieci = datareader("TolleranzaMenoValorePrevistoPrimoDieci").ToString
                    txtTolleranzaMenoValPrev10Pz1.Text = propTolleranzaMenopropValorePrevistoPrimoDieci

                    propNotePrimoPezzo = datareader("NotePrimoPezzo").ToString
                    txtNotePz1.Text = propNotePrimoPezzo

                End While



            End Using

        Catch ex As Exception
            MessageBox.Show("Errore getPrimoPezzoByIdIntestazione : " & ex.Message)
        End Try

    End Sub


#End Region

#Region "carica dati Secondo Pezzo By IDIntestazione"

    Public Sub getSecondoPezzoByIdIntestazione(intIdItestazione As Integer)

        Dim cmd As DbCommand = _db.GetStoredProcCommand("spGetSecondoPezzoByIdIntestazione")

        Try
            _db.AddInParameter(cmd, "IDIntestazione", DbType.Int32, intIdItestazione)

            Using datareader As IDataReader = _db.ExecuteReader(cmd)

                While datareader.Read
                    propValorePrevistoSecondoUno = datareader("ValorePrevistoSecondoPezzoUno").ToString
                    txtValPrev1Pz2.Text = propValorePrevistoSecondoUno
                    propValorePrevistoSecondoDue = datareader("ValorePrevistoSecondoPezzoDue").ToString
                    txtValPrev2Pz2.Text = propValorePrevistoSecondoDue
                    propValorePrevistoSecondoTre = datareader("ValorePrevistoSecondoPezzoTre").ToString
                    txtValPrev3Pz2.Text = propValorePrevistoSecondoTre
                    propValorePrevistoSecondoQuattro = datareader("ValorePrevistoSecondoPezzoQuattro").ToString
                    txtValPrev4Pz2.Text = propValorePrevistoSecondoQuattro
                    propValorePrevistoSecondoCinque = datareader("ValorePrevistoSecondoPezzoCinque").ToString
                    txtValPrev5Pz2.Text = propValorePrevistoSecondoCinque
                    propValorePrevistoSecondoSei = datareader("ValorePrevistoSecondoPezzoSei").ToString
                    txtValPrev6Pz2.Text = propValorePrevistoSecondoSei
                    propValorePrevistoSecondoSette = datareader("ValorePrevistoSecondoPezzoSette").ToString
                    txtValPrev7Pz2.Text = propValorePrevistoSecondoSette
                    propValorePrevistoSecondoOtto = datareader("ValorePrevistoSecondoPezzoOtto").ToString
                    txtValPrev8Pz2.Text = propValorePrevistoSecondoOtto
                    propValorePrevistoSecondoNove = datareader("ValorePrevistoSecondoPezzoNove").ToString
                    txtValPrev9Pz2.Text = propValorePrevistoSecondoNove
                    propValorePrevistoSecondoDieci = datareader("ValorePrevistoSecondoPezzoDieci").ToString
                    txtValPrev10Pz2.Text = propValorePrevistoSecondoDieci

                    propValoreMisuratoSecondoUno = datareader("ValoreMisuratoSecondoPezzoUno").ToString
                    txt1ValMisPz2.Text = propValoreMisuratoSecondoUno
                    propValoreMisuratoSecondoDue = datareader("ValoreMisuratoSecondoPezzoDue").ToString
                    txt2ValMisPz2.Text = propValoreMisuratoSecondoDue
                    propValoreMisuratoSecondoTre = datareader("ValoreMisuratoSecondoPezzoTre").ToString
                    txt3ValMisPz2.Text = propValoreMisuratoSecondoTre
                    propValoreMisuratoSecondoQuattro = datareader("ValoreMisuratoSecondoPezzoQuattro").ToString
                    txt4ValMisPz2.Text = propValoreMisuratoSecondoQuattro
                    propValoreMisuratoSecondoCinque = datareader("ValoreMisuratoSecondoPezzoCinque").ToString
                    txt5ValMisPz2.Text = propValoreMisuratoSecondoCinque
                    propValoreMisuratoSecondoSei = datareader("ValoreMisuratoSecondoPezzoSei").ToString
                    txt6ValMisPz2.Text = propValoreMisuratoSecondoSei
                    propValoreMisuratoSecondoSette = datareader("ValoreMisuratoSecondoPezzoSette").ToString
                    txt7ValMisPz2.Text = propValoreMisuratoSecondoSette
                    propValoreMisuratoSecondoOtto = datareader("ValoreMisuratoSecondoPezzoOtto").ToString
                    txt8ValMisPz2.Text = propValoreMisuratoSecondoOtto
                    propValoreMisuratoSecondoNove = datareader("ValoreMisuratoSecondoPezzoNove").ToString
                    txt9ValMisPz2.Text = propValoreMisuratoSecondoNove
                    propValoreMisuratoSecondoDieci = datareader("ValoreMisuratoSecondoPezzoDieci").ToString
                    txt10ValMisPz2.Text = propValoreMisuratoSecondoDieci

                    propTolleranzaPiupropValorePrevistoSecondoUno = datareader("TolleranzaPiuValorePrevistoSecondoUno").ToString
                    txtTolleranzaPiuValPrev1Pz2.Text = propTolleranzaPiupropValorePrevistoSecondoUno
                    propTolleranzaPiupropValorePrevistoSecondoDue = datareader("TolleranzaPiuValorePrevistoSecondoDue").ToString
                    txtTolleranzaPiuValPrev2Pz2.Text = propTolleranzaPiupropValorePrevistoSecondoDue
                    propTolleranzaPiupropValorePrevistoSecondoTre = datareader("TolleranzaPiuValorePrevistoSecondoTre").ToString
                    txtTolleranzaPiuValPrev3Pz2.Text = propTolleranzaPiupropValorePrevistoSecondoTre
                    propTolleranzaPiupropValorePrevistoSecondoQuattro = datareader("TolleranzaPiuValorePrevistoSecondoQuattro").ToString
                    txtTolleranzaPiuValPrev4Pz2.Text = propTolleranzaPiupropValorePrevistoSecondoQuattro
                    propTolleranzaPiupropValorePrevistoSecondoCinque = datareader("TolleranzaPiuValorePrevistoSecondoCinque").ToString
                    txtTolleranzaPiuValPrev5Pz2.Text = propTolleranzaPiupropValorePrevistoSecondoCinque
                    propTolleranzaPiupropValorePrevistoSecondoSei = datareader("TolleranzaPiuValorePrevistoSecondoSei").ToString
                    txtTolleranzaPiuValPrev6Pz2.Text = propTolleranzaPiupropValorePrevistoSecondoSei
                    propTolleranzaPiupropValorePrevistoSecondoSette = datareader("TolleranzaPiuValorePrevistoSecondoSette").ToString
                    txtTolleranzaPiuValPrev7Pz2.Text = propTolleranzaPiupropValorePrevistoSecondoSette
                    propTolleranzaPiupropValorePrevistoSecondoOtto = datareader("TolleranzaPiuValorePrevistoSecondoOtto").ToString
                    txtTolleranzaPiuValPrev8Pz2.Text = propTolleranzaPiupropValorePrevistoSecondoOtto
                    propTolleranzaPiupropValorePrevistoSecondoNove = datareader("TolleranzaPiuValorePrevistoSecondoNove").ToString
                    txtTolleranzaPiuValPrev9Pz2.Text = propTolleranzaPiupropValorePrevistoSecondoNove
                    propTolleranzaPiupropValorePrevistoSecondoDieci = datareader("TolleranzaPiuValorePrevistoSecondoDieci").ToString
                    txtTolleranzaPiuValPrev10Pz2.Text = propTolleranzaPiupropValorePrevistoSecondoDieci

                    propTolleranzaMenopropValorePrevistoSecondoUno = datareader("TolleranzaMenoValorePrevistoSecondoUno").ToString
                    txtTolleranzaMenoValPrev1Pz2.Text = propTolleranzaMenopropValorePrevistoSecondoUno
                    propTolleranzaMenopropValorePrevistoSecondoDue = datareader("TolleranzaMenoValorePrevistoSecondoDue").ToString
                    txtTolleranzaMenoValPrev2Pz2.Text = propTolleranzaMenopropValorePrevistoSecondoDue
                    propTolleranzaMenopropValorePrevistoSecondoTre = datareader("TolleranzaMenoValorePrevistoSecondoTre").ToString
                    txtTolleranzaMenoValPrev3Pz2.Text = propTolleranzaMenopropValorePrevistoSecondoTre
                    propTolleranzaMenopropValorePrevistoSecondoQuattro = datareader("TolleranzaMenoValorePrevistoSecondoQuattro").ToString
                    txtTolleranzaMenoValPrev4Pz2.Text = propTolleranzaMenopropValorePrevistoSecondoQuattro
                    propTolleranzaMenopropValorePrevistoSecondoCinque = datareader("TolleranzaMenoValorePrevistoSecondoCinque").ToString
                    txtTolleranzaMenoValPrev5Pz2.Text = propTolleranzaMenopropValorePrevistoSecondoCinque
                    propTolleranzaMenopropValorePrevistoSecondoSei = datareader("TolleranzaMenoValorePrevistoSecondoSei").ToString
                    txtTolleranzaMenoValPrev6Pz2.Text = propTolleranzaMenopropValorePrevistoSecondoSei
                    propTolleranzaMenopropValorePrevistoSecondoSette = datareader("TolleranzaMenoValorePrevistoSecondoSette").ToString
                    txtTolleranzaMenoValPrev7Pz2.Text = propTolleranzaMenopropValorePrevistoSecondoSette
                    propTolleranzaMenopropValorePrevistoSecondoOtto = datareader("TolleranzaMenoValorePrevistoSecondoOtto").ToString
                    txtTolleranzaMenoValPrev8Pz2.Text = propTolleranzaMenopropValorePrevistoSecondoOtto
                    propTolleranzaMenopropValorePrevistoSecondoNove = datareader("TolleranzaMenoValorePrevistoSecondoNove").ToString
                    txtTolleranzaMenoValPrev9Pz2.Text = propTolleranzaMenopropValorePrevistoSecondoNove
                    propTolleranzaMenopropValorePrevistoSecondoDieci = datareader("TolleranzaMenoValorePrevistoSecondoDieci").ToString
                    txtTolleranzaMenoValPrev10Pz2.Text = propTolleranzaMenopropValorePrevistoSecondoDieci

                    propNoteSecondoPezzo = datareader("NoteSecondoPezzo").ToString
                    txtNotePz2.Text = propNoteSecondoPezzo

                End While


            End Using


        Catch ex As Exception
            MessageBox.Show("Errore getSecondoPezzoByIdIntestazione : " & ex.Message)
        End Try

    End Sub



#End Region

#Region "carica dati Terzo Pezzo By IDIntestazione"

    Public Sub getTerzoPezzoByIdIntestazione(intIdItestazione As Integer)

        Dim cmd As DbCommand = _db.GetStoredProcCommand("spGetTerzoPezzoByIdIntestazione")

        Try
            _db.AddInParameter(cmd, "IDIntestazione", DbType.Int32, intIdItestazione)

            Using datareader As IDataReader = _db.ExecuteReader(cmd)

                While datareader.Read
                    propValorePrevistoTerzoUno = datareader("ValorePrevistoTerzoPezzoUno").ToString
                    txtValPrev1Pz3.Text = propValorePrevistoTerzoUno
                    propValorePrevistoTerzoDue = datareader("ValorePrevistoTerzoPezzoDue").ToString
                    txtValPrev2Pz3.Text = propValorePrevistoTerzoDue
                    propValorePrevistoTerzoTre = datareader("ValorePrevistoTerzoPezzoTre").ToString
                    txtValPrev3Pz3.Text = propValorePrevistoTerzoTre
                    propValorePrevistoTerzoQuattro = datareader("ValorePrevistoTerzoPezzoQuattro").ToString
                    txtValPrev4Pz3.Text = propValorePrevistoTerzoQuattro
                    propValorePrevistoTerzoCinque = datareader("ValorePrevistoTerzoPezzoCinque").ToString
                    txtValPrev5Pz3.Text = propValorePrevistoTerzoCinque
                    propValorePrevistoTerzoSei = datareader("ValorePrevistoTerzoPezzoSei").ToString
                    txtValPrev6Pz3.Text = propValorePrevistoTerzoSei
                    propValorePrevistoTerzoSette = datareader("ValorePrevistoTerzoPezzoSette").ToString
                    txtValPrev7Pz3.Text = propValorePrevistoTerzoSette
                    propValorePrevistoTerzoOtto = datareader("ValorePrevistoTerzoPezzoOtto").ToString
                    txtValPrev8Pz3.Text = propValorePrevistoTerzoOtto
                    propValorePrevistoTerzoNove = datareader("ValorePrevistoTerzoPezzoNove").ToString
                    txtValPrev9Pz3.Text = propValorePrevistoTerzoNove
                    propValorePrevistoTerzoDieci = datareader("ValorePrevistoTerzoPezzoDieci").ToString
                    txtValPrev10Pz3.Text = propValorePrevistoTerzoDieci

                    propValoreMisuratoTerzoUno = datareader("ValoreMisuratoTerzoPezzoUno").ToString
                    txt1ValMisPz3.Text = propValoreMisuratoTerzoUno
                    propValoreMisuratoTerzoDue = datareader("ValoreMisuratoTerzoPezzoDue").ToString
                    txt2ValMisPz3.Text = propValoreMisuratoTerzoDue
                    propValoreMisuratoTerzoTre = datareader("ValoreMisuratoTerzoPezzoTre").ToString
                    txt3ValMisPz3.Text = propValoreMisuratoTerzoTre
                    propValoreMisuratoTerzoQuattro = datareader("ValoreMisuratoTerzoPezzoQuattro").ToString
                    txt4ValMisPz3.Text = propValoreMisuratoTerzoQuattro
                    propValoreMisuratoTerzoCinque = datareader("ValoreMisuratoTerzoPezzoCinque").ToString
                    txt5ValMisPz3.Text = propValoreMisuratoTerzoCinque
                    propValoreMisuratoTerzoSei = datareader("ValoreMisuratoTerzoPezzoSei").ToString
                    txt6ValMisPz3.Text = propValoreMisuratoTerzoSei
                    propValoreMisuratoTerzoSette = datareader("ValoreMisuratoTerzoPezzoSette").ToString
                    txt7ValMisPz3.Text = propValoreMisuratoTerzoSette
                    propValoreMisuratoTerzoOtto = datareader("ValoreMisuratoTerzoPezzoOtto").ToString
                    txt8ValMisPz3.Text = propValoreMisuratoTerzoOtto
                    propValoreMisuratoTerzoNove = datareader("ValoreMisuratoTerzoPezzoNove").ToString
                    txt9ValMisPz3.Text = propValoreMisuratoTerzoNove
                    propValoreMisuratoTerzoDieci = datareader("ValoreMisuratoTerzoPezzoDieci").ToString
                    txt10ValMisPz3.Text = propValoreMisuratoTerzoDieci

                    propTolleranzaPiupropValorePrevistoTerzoUno = datareader("TolleranzaPiuValorePrevistoTerzoUno").ToString
                    txtTolleranzaPiuValPrev1Pz3.Text = propTolleranzaPiupropValorePrevistoTerzoUno
                    propTolleranzaPiupropValorePrevistoTerzoDue = datareader("TolleranzaPiuValorePrevistoTerzoDue").ToString
                    txtTolleranzaPiuValPrev2Pz3.Text = propTolleranzaPiupropValorePrevistoTerzoDue
                    propTolleranzaPiupropValorePrevistoTerzoTre = datareader("TolleranzaPiuValorePrevistoTerzoTre").ToString
                    txtTolleranzaPiuValPrev3Pz3.Text = propTolleranzaPiupropValorePrevistoTerzoTre
                    propTolleranzaPiupropValorePrevistoTerzoQuattro = datareader("TolleranzaPiuValorePrevistoTerzoQuattro").ToString
                    txtTolleranzaPiuValPrev4Pz3.Text = propTolleranzaPiupropValorePrevistoTerzoQuattro
                    propTolleranzaPiupropValorePrevistoTerzoCinque = datareader("TolleranzaPiuValorePrevistoTerzoCinque").ToString
                    txtTolleranzaPiuValPrev5Pz3.Text = propTolleranzaPiupropValorePrevistoTerzoCinque
                    propTolleranzaPiupropValorePrevistoTerzoSei = datareader("TolleranzaPiuValorePrevistoTerzoSei").ToString
                    txtTolleranzaPiuValPrev6Pz3.Text = propTolleranzaPiupropValorePrevistoTerzoSei
                    propTolleranzaPiupropValorePrevistoTerzoSette = datareader("TolleranzaPiuValorePrevistoTerzoSette").ToString
                    txtTolleranzaPiuValPrev7Pz3.Text = propTolleranzaPiupropValorePrevistoTerzoSette
                    propTolleranzaPiupropValorePrevistoTerzoOtto = datareader("TolleranzaPiuValorePrevistoTerzoOtto").ToString
                    txtTolleranzaPiuValPrev8Pz3.Text = propTolleranzaPiupropValorePrevistoTerzoOtto
                    propTolleranzaPiupropValorePrevistoTerzoNove = datareader("TolleranzaPiuValorePrevistoTerzoNove").ToString
                    txtTolleranzaPiuValPrev9Pz3.Text = propTolleranzaPiupropValorePrevistoTerzoNove
                    propTolleranzaPiupropValorePrevistoTerzoDieci = datareader("TolleranzaPiuValorePrevistoTerzoDieci").ToString
                    txtTolleranzaPiuValPrev10Pz3.Text = propTolleranzaPiupropValorePrevistoTerzoDieci

                    propTolleranzaMenopropValorePrevistoTerzoUno = datareader("TolleranzaMenoValorePrevistoTerzoUno").ToString
                    txtTolleranzaMenoValPrev1Pz3.Text = propTolleranzaMenopropValorePrevistoTerzoUno
                    propTolleranzaMenopropValorePrevistoTerzoDue = datareader("TolleranzaMenoValorePrevistoTerzoDue").ToString
                    txtTolleranzaMenoValPrev2Pz3.Text = propTolleranzaMenopropValorePrevistoTerzoDue
                    propTolleranzaMenopropValorePrevistoTerzoTre = datareader("TolleranzaMenoValorePrevistoTerzoTre").ToString
                    txtTolleranzaMenoValPrev3Pz3.Text = propTolleranzaMenopropValorePrevistoTerzoTre
                    propTolleranzaMenopropValorePrevistoTerzoQuattro = datareader("TolleranzaMenoValorePrevistoTerzoQuattro").ToString
                    txtTolleranzaMenoValPrev4Pz3.Text = propTolleranzaMenopropValorePrevistoTerzoQuattro
                    propTolleranzaMenopropValorePrevistoTerzoCinque = datareader("TolleranzaMenoValorePrevistoTerzoCinque").ToString
                    txtTolleranzaMenoValPrev5Pz3.Text = propTolleranzaMenopropValorePrevistoTerzoCinque
                    propTolleranzaMenopropValorePrevistoTerzoSei = datareader("TolleranzaMenoValorePrevistoTerzoSei").ToString
                    txtTolleranzaMenoValPrev6Pz3.Text = propTolleranzaMenopropValorePrevistoTerzoSei
                    propTolleranzaMenopropValorePrevistoTerzoSette = datareader("TolleranzaMenoValorePrevistoTerzoSette").ToString
                    txtTolleranzaMenoValPrev7Pz3.Text = propTolleranzaMenopropValorePrevistoTerzoSette
                    propTolleranzaMenopropValorePrevistoTerzoOtto = datareader("TolleranzaMenoValorePrevistoTerzoOtto").ToString
                    txtTolleranzaMenoValPrev8Pz3.Text = propTolleranzaMenopropValorePrevistoTerzoOtto
                    propTolleranzaMenopropValorePrevistoTerzoNove = datareader("TolleranzaMenoValorePrevistoTerzoNove").ToString
                    txtTolleranzaMenoValPrev9Pz3.Text = propTolleranzaMenopropValorePrevistoTerzoNove
                    propTolleranzaMenopropValorePrevistoTerzoDieci = datareader("TolleranzaMenoValorePrevistoTerzoDieci").ToString
                    txtTolleranzaMenoValPrev10Pz3.Text = propTolleranzaMenopropValorePrevistoTerzoDieci

                    propNoteTerzoPezzo = datareader("NoteTerzoPezzo").ToString
                    txtNotePz3.Text = propNoteTerzoPezzo

                End While


            End Using


        Catch ex As Exception
            MessageBox.Show("Errore getTerzoPezzoByIdIntestazione : " & ex.Message)
        End Try


    End Sub

#End Region

#Region "carica dati Quarto Pezzo By IDIntestazione"

    Public Sub getQuartoPezzoByIdIntestazione(intIdItestazione As Integer)

        Dim cmd As DbCommand = _db.GetStoredProcCommand("spGetQuartoPezzoByIdIntestazione")

        Try
            _db.AddInParameter(cmd, "IDIntestazione", DbType.Int32, intIdItestazione)

            Using datareader As IDataReader = _db.ExecuteReader(cmd)

                While datareader.Read
                    propValorePrevistoQuartoUno = datareader("ValorePrevistoQuartoPezzoUno").ToString
                    txtValPrev1Pz4.Text = propValorePrevistoQuartoUno
                    propValorePrevistoQuartoDue = datareader("ValorePrevistoQuartoPezzoDue").ToString
                    txtValPrev2Pz4.Text = propValorePrevistoQuartoDue
                    propValorePrevistoQuartoTre = datareader("ValorePrevistoQuartoPezzoTre").ToString
                    txtValPrev3Pz4.Text = propValorePrevistoQuartoTre
                    propValorePrevistoQuartoQuattro = datareader("ValorePrevistoQuartoPezzoQuattro").ToString
                    txtValPrev4Pz4.Text = propValorePrevistoQuartoQuattro
                    propValorePrevistoQuartoCinque = datareader("ValorePrevistoQuartoPezzoCinque").ToString
                    txtValPrev5Pz4.Text = propValorePrevistoQuartoCinque
                    propValorePrevistoQuartoSei = datareader("ValorePrevistoQuartoPezzoSei").ToString
                    txtValPrev6Pz4.Text = propValorePrevistoQuartoSei
                    propValorePrevistoQuartoSette = datareader("ValorePrevistoQuartoPezzoSette").ToString
                    txtValPrev7Pz4.Text = propValorePrevistoQuartoSette
                    propValorePrevistoQuartoOtto = datareader("ValorePrevistoQuartoPezzoOtto").ToString
                    txtValPrev8Pz4.Text = propValorePrevistoQuartoOtto
                    propValorePrevistoQuartoNove = datareader("ValorePrevistoQuartoPezzoNove").ToString
                    txtValPrev9Pz4.Text = propValorePrevistoQuartoNove
                    propValorePrevistoQuartoDieci = datareader("ValorePrevistoQuartoPezzoDieci").ToString
                    txtValPrev10Pz4.Text = propValorePrevistoQuartoDieci

                    propValoreMisuratoQuartoUno = datareader("ValoreMisuratoQuartoPezzoUno").ToString
                    txt1ValMisPz4.Text = propValoreMisuratoQuartoUno
                    propValoreMisuratoQuartoDue = datareader("ValoreMisuratoQuartoPezzoDue").ToString
                    txt2ValMisPz4.Text = propValoreMisuratoQuartoDue
                    propValoreMisuratoQuartoTre = datareader("ValoreMisuratoQuartoPezzoTre").ToString
                    txt3ValMisPz4.Text = propValoreMisuratoQuartoTre
                    propValoreMisuratoQuartoQuattro = datareader("ValoreMisuratoQuartoPezzoQuattro").ToString
                    txt4ValMisPz4.Text = propValoreMisuratoQuartoQuattro
                    propValoreMisuratoQuartoCinque = datareader("ValoreMisuratoQuartoPezzoCinque").ToString
                    txt5ValMisPz4.Text = propValoreMisuratoQuartoCinque
                    propValoreMisuratoQuartoSei = datareader("ValoreMisuratoQuartoPezzoSei").ToString
                    txt6ValMisPz4.Text = propValoreMisuratoQuartoSei
                    propValoreMisuratoQuartoSette = datareader("ValoreMisuratoQuartoPezzoSette").ToString
                    txt7ValMisPz4.Text = propValoreMisuratoQuartoSette
                    propValoreMisuratoQuartoOtto = datareader("ValoreMisuratoQuartoPezzoOtto").ToString
                    txt8ValMisPz4.Text = propValoreMisuratoQuartoOtto
                    propValoreMisuratoQuartoNove = datareader("ValoreMisuratoQuartoPezzoNove").ToString
                    txt9ValMisPz4.Text = propValoreMisuratoQuartoNove
                    propValoreMisuratoQuartoDieci = datareader("ValoreMisuratoQuartoPezzoDieci").ToString
                    txt10ValMisPz4.Text = propValoreMisuratoQuartoDieci

                    propTolleranzaPiupropValorePrevistoQuartoUno = datareader("TolleranzaPiuValorePrevistoQuartoUno").ToString
                    txtTolleranzaPiuValPrev1Pz4.Text = propTolleranzaPiupropValorePrevistoQuartoUno
                    propTolleranzaPiupropValorePrevistoQuartoDue = datareader("TolleranzaPiuValorePrevistoQuartoDue").ToString
                    txtTolleranzaPiuValPrev2Pz4.Text = propTolleranzaPiupropValorePrevistoQuartoDue
                    propTolleranzaPiupropValorePrevistoQuartoTre = datareader("TolleranzaPiuValorePrevistoQuartoTre").ToString
                    txtTolleranzaPiuValPrev3Pz4.Text = propTolleranzaPiupropValorePrevistoQuartoTre
                    propTolleranzaPiupropValorePrevistoQuartoQuattro = datareader("TolleranzaPiuValorePrevistoQuartoQuattro").ToString
                    txtTolleranzaPiuValPrev4Pz4.Text = propTolleranzaPiupropValorePrevistoQuartoQuattro
                    propTolleranzaPiupropValorePrevistoQuartoCinque = datareader("TolleranzaPiuValorePrevistoQuartoCinque").ToString
                    txtTolleranzaPiuValPrev5Pz4.Text = propTolleranzaPiupropValorePrevistoQuartoCinque
                    propTolleranzaPiupropValorePrevistoQuartoSei = datareader("TolleranzaPiuValorePrevistoQuartoSei").ToString
                    txtTolleranzaPiuValPrev6Pz4.Text = propTolleranzaPiupropValorePrevistoQuartoSei
                    propTolleranzaPiupropValorePrevistoQuartoSette = datareader("TolleranzaPiuValorePrevistoQuartoSette").ToString
                    txtTolleranzaPiuValPrev7Pz4.Text = propTolleranzaPiupropValorePrevistoQuartoSette
                    propTolleranzaPiupropValorePrevistoQuartoOtto = datareader("TolleranzaPiuValorePrevistoQuartoOtto").ToString
                    txtTolleranzaPiuValPrev8Pz4.Text = propTolleranzaPiupropValorePrevistoQuartoOtto
                    propTolleranzaPiupropValorePrevistoQuartoNove = datareader("TolleranzaPiuValorePrevistoQuartoNove").ToString
                    txtTolleranzaPiuValPrev9Pz4.Text = propTolleranzaPiupropValorePrevistoQuartoNove
                    propTolleranzaPiupropValorePrevistoQuartoDieci = datareader("TolleranzaPiuValorePrevistoQuartoDieci").ToString
                    txtTolleranzaPiuValPrev10Pz4.Text = propTolleranzaPiupropValorePrevistoQuartoDieci

                    propTolleranzaMenopropValorePrevistoQuartoUno = datareader("TolleranzaMenoValorePrevistoQuartoUno").ToString
                    txtTolleranzaMenoValPrev1Pz4.Text = propTolleranzaMenopropValorePrevistoQuartoUno
                    propTolleranzaMenopropValorePrevistoQuartoDue = datareader("TolleranzaMenoValorePrevistoQuartoDue").ToString
                    txtTolleranzaMenoValPrev2Pz4.Text = propTolleranzaMenopropValorePrevistoQuartoDue
                    propTolleranzaMenopropValorePrevistoQuartoTre = datareader("TolleranzaMenoValorePrevistoQuartoTre").ToString
                    txtTolleranzaMenoValPrev3Pz4.Text = propTolleranzaMenopropValorePrevistoQuartoTre
                    propTolleranzaMenopropValorePrevistoQuartoQuattro = datareader("TolleranzaMenoValorePrevistoQuartoQuattro").ToString
                    txtTolleranzaMenoValPrev4Pz4.Text = propTolleranzaMenopropValorePrevistoQuartoQuattro
                    propTolleranzaMenopropValorePrevistoQuartoCinque = datareader("TolleranzaMenoValorePrevistoQuartoCinque").ToString
                    txtTolleranzaMenoValPrev5Pz4.Text = propTolleranzaMenopropValorePrevistoQuartoCinque
                    propTolleranzaMenopropValorePrevistoQuartoSei = datareader("TolleranzaMenoValorePrevistoQuartoSei").ToString
                    txtTolleranzaMenoValPrev6Pz4.Text = propTolleranzaMenopropValorePrevistoQuartoSei
                    propTolleranzaMenopropValorePrevistoQuartoSette = datareader("TolleranzaMenoValorePrevistoQuartoSette").ToString
                    txtTolleranzaMenoValPrev7Pz4.Text = propTolleranzaMenopropValorePrevistoQuartoSette
                    propTolleranzaMenopropValorePrevistoQuartoOtto = datareader("TolleranzaMenoValorePrevistoQuartoOtto").ToString
                    txtTolleranzaMenoValPrev8Pz4.Text = propTolleranzaMenopropValorePrevistoQuartoOtto
                    propTolleranzaMenopropValorePrevistoQuartoNove = datareader("TolleranzaMenoValorePrevistoQuartoNove").ToString
                    txtTolleranzaMenoValPrev9Pz4.Text = propTolleranzaMenopropValorePrevistoQuartoNove
                    propTolleranzaMenopropValorePrevistoQuartoDieci = datareader("TolleranzaMenoValorePrevistoQuartoDieci").ToString
                    txtTolleranzaMenoValPrev10Pz4.Text = propTolleranzaMenopropValorePrevistoQuartoDieci

                    propNoteQuartoPezzo = datareader("NoteQuartoPezzo").ToString
                    txtNotePz4.Text = propNoteQuartoPezzo

                End While


            End Using


        Catch ex As Exception
            MessageBox.Show("Errore getQuartoPezzoByIdIntestazione : " & ex.Message)
        End Try


    End Sub


#End Region

#Region "carica dati Quinto Pezzo By IDIntestazione"

    Public Sub getQuintoPezzoByIdIntestazione(intIdItestazione As Integer)

        Dim cmd As DbCommand = _db.GetStoredProcCommand("spGetQuintoPezzoByIdIntestazione")

        Try
            _db.AddInParameter(cmd, "IDIntestazione", DbType.Int32, intIdItestazione)

            Using datareader As IDataReader = _db.ExecuteReader(cmd)

                While datareader.Read
                    propValorePrevistoQuintoUno = datareader("ValorePrevistoQuintoPezzoUno").ToString
                    txtValPrev1Pz5.Text = propValorePrevistoQuintoUno
                    propValorePrevistoQuintoDue = datareader("ValorePrevistoQuintoPezzoDue").ToString
                    txtValPrev2Pz5.Text = propValorePrevistoQuintoDue
                    propValorePrevistoQuintoTre = datareader("ValorePrevistoQuintoPezzoTre").ToString
                    txtValPrev3Pz5.Text = propValorePrevistoQuintoTre
                    propValorePrevistoQuintoQuattro = datareader("ValorePrevistoQuintoPezzoQuattro").ToString
                    txtValPrev4Pz5.Text = propValorePrevistoQuintoQuattro
                    propValorePrevistoQuintoCinque = datareader("ValorePrevistoQuintoPezzoCinque").ToString
                    txtValPrev5Pz5.Text = propValorePrevistoQuintoCinque
                    propValorePrevistoQuintoSei = datareader("ValorePrevistoQuintoPezzoSei").ToString
                    txtValPrev6Pz5.Text = propValorePrevistoQuintoSei
                    propValorePrevistoQuintoSette = datareader("ValorePrevistoQuintoPezzoSette").ToString
                    txtValPrev7Pz5.Text = propValorePrevistoQuintoSette
                    propValorePrevistoQuintoOtto = datareader("ValorePrevistoQuintoPezzoOtto").ToString
                    txtValPrev8Pz5.Text = propValorePrevistoQuintoOtto
                    propValorePrevistoQuintoNove = datareader("ValorePrevistoQuintoPezzoNove").ToString
                    txtValPrev9Pz5.Text = propValorePrevistoQuintoNove
                    propValorePrevistoQuintoDieci = datareader("ValorePrevistoQuintoPezzoDieci").ToString
                    txtValPrev10Pz5.Text = propValorePrevistoQuintoDieci

                    propValoreMisuratoQuintoUno = datareader("ValoreMisuratoQuintoPezzoUno").ToString
                    txt1ValMisPz5.Text = propValoreMisuratoQuintoUno
                    propValoreMisuratoQuintoDue = datareader("ValoreMisuratoQuintoPezzoDue").ToString
                    txt2ValMisPz5.Text = propValoreMisuratoQuintoDue
                    propValoreMisuratoQuintoTre = datareader("ValoreMisuratoQuintoPezzoTre").ToString
                    txt3ValMisPz5.Text = propValoreMisuratoQuintoTre
                    propValoreMisuratoQuintoQuattro = datareader("ValoreMisuratoQuintoPezzoQuattro").ToString
                    txt4ValMisPz5.Text = propValoreMisuratoQuintoQuattro
                    propValoreMisuratoQuintoCinque = datareader("ValoreMisuratoQuintoPezzoCinque").ToString
                    txt5ValMisPz5.Text = propValoreMisuratoQuintoCinque
                    propValoreMisuratoQuintoSei = datareader("ValoreMisuratoQuintoPezzoSei").ToString
                    txt6ValMisPz5.Text = propValoreMisuratoQuintoSei
                    propValoreMisuratoQuintoSette = datareader("ValoreMisuratoQuintoPezzoSette").ToString
                    txt7ValMisPz5.Text = propValoreMisuratoQuintoSette
                    propValoreMisuratoQuintoOtto = datareader("ValoreMisuratoQuintoPezzoOtto").ToString
                    txt8ValMisPz5.Text = propValoreMisuratoQuintoOtto
                    propValoreMisuratoQuintoNove = datareader("ValoreMisuratoQuintoPezzoNove").ToString
                    txt9ValMisPz5.Text = propValoreMisuratoQuintoNove
                    propValoreMisuratoQuintoDieci = datareader("ValoreMisuratoQuintoPezzoDieci").ToString
                    txt10ValMisPz5.Text = propValoreMisuratoQuintoDieci

                    propTolleranzaPiupropValorePrevistoQuintoUno = datareader("TolleranzaPiuValorePrevistoQuintoUno").ToString
                    txtTolleranzaPiuValPrev1Pz5.Text = propTolleranzaPiupropValorePrevistoQuintoUno
                    propTolleranzaPiupropValorePrevistoQuintoDue = datareader("TolleranzaPiuValorePrevistoQuintoDue").ToString
                    txtTolleranzaPiuValPrev2Pz5.Text = propTolleranzaPiupropValorePrevistoQuintoDue
                    propTolleranzaPiupropValorePrevistoQuintoTre = datareader("TolleranzaPiuValorePrevistoQuintoTre").ToString
                    txtTolleranzaPiuValPrev3Pz5.Text = propTolleranzaPiupropValorePrevistoQuintoTre
                    propTolleranzaPiupropValorePrevistoQuintoQuattro = datareader("TolleranzaPiuValorePrevistoQuintoQuattro").ToString
                    txtTolleranzaPiuValPrev4Pz5.Text = propTolleranzaPiupropValorePrevistoQuintoQuattro
                    propTolleranzaPiupropValorePrevistoQuintoCinque = datareader("TolleranzaPiuValorePrevistoQuintoCinque").ToString
                    txtTolleranzaPiuValPrev5Pz5.Text = propTolleranzaPiupropValorePrevistoQuintoCinque
                    propTolleranzaPiupropValorePrevistoQuintoSei = datareader("TolleranzaPiuValorePrevistoQuintoSei").ToString
                    txtTolleranzaPiuValPrev6Pz5.Text = propTolleranzaPiupropValorePrevistoQuintoSei
                    propTolleranzaPiupropValorePrevistoQuintoSette = datareader("TolleranzaPiuValorePrevistoQuintoSette").ToString
                    txtTolleranzaPiuValPrev7Pz5.Text = propTolleranzaPiupropValorePrevistoQuintoSette
                    propTolleranzaPiupropValorePrevistoQuintoOtto = datareader("TolleranzaPiuValorePrevistoQuintoOtto").ToString
                    txtTolleranzaPiuValPrev8Pz5.Text = propTolleranzaPiupropValorePrevistoQuintoOtto
                    propTolleranzaPiupropValorePrevistoQuintoNove = datareader("TolleranzaPiuValorePrevistoQuintoNove").ToString
                    txtTolleranzaPiuValPrev9Pz5.Text = propTolleranzaPiupropValorePrevistoQuintoNove
                    propTolleranzaPiupropValorePrevistoQuintoDieci = datareader("TolleranzaPiuValorePrevistoQuintoDieci").ToString
                    txtTolleranzaPiuValPrev10Pz5.Text = propTolleranzaPiupropValorePrevistoQuintoDieci

                    propTolleranzaMenopropValorePrevistoQuintoUno = datareader("TolleranzaMenoValorePrevistoQuintoUno").ToString
                    txtTolleranzaMenoValPrev1Pz5.Text = propTolleranzaMenopropValorePrevistoQuintoUno
                    propTolleranzaMenopropValorePrevistoQuintoDue = datareader("TolleranzaMenoValorePrevistoQuintoDue").ToString
                    txtTolleranzaMenoValPrev2Pz5.Text = propTolleranzaMenopropValorePrevistoQuintoDue
                    propTolleranzaMenopropValorePrevistoQuintoTre = datareader("TolleranzaMenoValorePrevistoQuintoTre").ToString
                    txtTolleranzaMenoValPrev3Pz5.Text = propTolleranzaMenopropValorePrevistoQuintoTre
                    propTolleranzaMenopropValorePrevistoQuintoQuattro = datareader("TolleranzaMenoValorePrevistoQuintoQuattro").ToString
                    txtTolleranzaMenoValPrev4Pz5.Text = propTolleranzaMenopropValorePrevistoQuintoQuattro
                    propTolleranzaMenopropValorePrevistoQuintoCinque = datareader("TolleranzaMenoValorePrevistoQuintoCinque").ToString
                    txtTolleranzaMenoValPrev5Pz5.Text = propTolleranzaMenopropValorePrevistoQuintoCinque
                    propTolleranzaMenopropValorePrevistoQuintoSei = datareader("TolleranzaMenoValorePrevistoQuintoSei").ToString
                    txtTolleranzaMenoValPrev6Pz5.Text = propTolleranzaMenopropValorePrevistoQuintoSei
                    propTolleranzaMenopropValorePrevistoQuintoSette = datareader("TolleranzaMenoValorePrevistoQuintoSette").ToString
                    txtTolleranzaMenoValPrev7Pz5.Text = propTolleranzaMenopropValorePrevistoQuintoSette
                    propTolleranzaMenopropValorePrevistoQuintoOtto = datareader("TolleranzaMenoValorePrevistoQuintoOtto").ToString
                    txtTolleranzaMenoValPrev8Pz5.Text = propTolleranzaMenopropValorePrevistoQuintoOtto
                    propTolleranzaMenopropValorePrevistoQuintoNove = datareader("TolleranzaMenoValorePrevistoQuintoNove").ToString
                    txtTolleranzaMenoValPrev9Pz5.Text = propTolleranzaMenopropValorePrevistoQuintoNove
                    propTolleranzaMenopropValorePrevistoQuintoDieci = datareader("TolleranzaMenoValorePrevistoQuintoDieci").ToString
                    txtTolleranzaMenoValPrev10Pz5.Text = propTolleranzaMenopropValorePrevistoQuintoDieci

                    propNoteQuintoPezzo = datareader("NoteQuintoPezzo").ToString
                    txtNotePz5.Text = propNoteQuintoPezzo

                End While


            End Using


        Catch ex As Exception
            MessageBox.Show("Errore getQuintoPezzoByIdIntestazione : " & ex.Message)
        End Try


    End Sub


#End Region

#Region "carica dati Ultimo Pezzo By IDIntestazione"

    Public Sub getUltimoPezzoByIdIntestazione(intIdItestazione As Integer)

        Dim cmd As DbCommand = _db.GetStoredProcCommand("spGetUltimoPezzoByIdIntestazione")

        Try
            _db.AddInParameter(cmd, "IDIntestazione", DbType.Int32, intIdItestazione)

            Using datareader As IDataReader = _db.ExecuteReader(cmd)

                While datareader.Read
                    propValorePrevistoUltimoUno = datareader("ValorePrevistoUltimoPezzoUno").ToString
                    txtValPrev1UltimoPz.Text = propValorePrevistoUltimoUno
                    propValorePrevistoUltimoDue = datareader("ValorePrevistoUltimoPezzoDue").ToString
                    txtValPrev2UltimoPz.Text = propValorePrevistoUltimoDue
                    propValorePrevistoUltimoTre = datareader("ValorePrevistoUltimoPezzoTre").ToString
                    txtValPrev3UltimoPz.Text = propValorePrevistoUltimoTre
                    propValorePrevistoUltimoQuattro = datareader("ValorePrevistoUltimoPezzoQuattro").ToString
                    txtValPrev4UltimoPz.Text = propValorePrevistoUltimoQuattro
                    propValorePrevistoUltimoCinque = datareader("ValorePrevistoUltimoPezzoCinque").ToString
                    txtValPrev5UltimoPz.Text = propValorePrevistoUltimoCinque
                    propValorePrevistoUltimoSei = datareader("ValorePrevistoUltimoPezzoSei").ToString
                    txtValPrev6UltimoPz.Text = propValorePrevistoUltimoSei
                    propValorePrevistoUltimoSette = datareader("ValorePrevistoUltimoPezzoSette").ToString
                    txtValPrev7UltimoPz.Text = propValorePrevistoUltimoSette
                    propValorePrevistoUltimoOtto = datareader("ValorePrevistoUltimoPezzoOtto").ToString
                    txtValPrev8UltimoPz.Text = propValorePrevistoUltimoOtto
                    propValorePrevistoUltimoNove = datareader("ValorePrevistoUltimoPezzoNove").ToString
                    txtValPrev9UltimoPz.Text = propValorePrevistoUltimoNove
                    propValorePrevistoUltimoDieci = datareader("ValorePrevistoUltimoPezzoDieci").ToString
                    txtValPrev10UltimoPz.Text = propValorePrevistoUltimoDieci

                    propValoreMisuratoUltimoUno = datareader("ValoreMisuratoUltimoPezzoUno").ToString
                    txt1ValMisUltimoPz.Text = propValoreMisuratoUltimoUno
                    propValoreMisuratoUltimoDue = datareader("ValoreMisuratoUltimoPezzoDue").ToString
                    txt2ValMisUltimoPz.Text = propValoreMisuratoUltimoDue
                    propValoreMisuratoUltimoTre = datareader("ValoreMisuratoUltimoPezzoTre").ToString
                    txt3ValMisUltimoPz.Text = propValoreMisuratoUltimoTre
                    propValoreMisuratoUltimoQuattro = datareader("ValoreMisuratoUltimoPezzoQuattro").ToString
                    txt4ValMisUltimoPz.Text = propValoreMisuratoUltimoQuattro
                    propValoreMisuratoUltimoCinque = datareader("ValoreMisuratoUltimoPezzoCinque").ToString
                    txt5ValMisUltimoPz.Text = propValoreMisuratoUltimoCinque
                    propValoreMisuratoUltimoSei = datareader("ValoreMisuratoUltimoPezzoSei").ToString
                    txt6ValMisUltimoPz.Text = propValoreMisuratoUltimoSei
                    propValoreMisuratoUltimoSette = datareader("ValoreMisuratoUltimoPezzoSette").ToString
                    txt7ValMisUltimoPz.Text = propValoreMisuratoUltimoSette
                    propValoreMisuratoUltimoOtto = datareader("ValoreMisuratoUltimoPezzoOtto").ToString
                    txt8ValMisUltimoPz.Text = propValoreMisuratoUltimoOtto
                    propValoreMisuratoUltimoNove = datareader("ValoreMisuratoUltimoPezzoNove").ToString
                    txt9ValMisUltimoPz.Text = propValoreMisuratoUltimoNove
                    propValoreMisuratoUltimoDieci = datareader("ValoreMisuratoUltimoPezzoDieci").ToString
                    txt10ValMisUltimoPz.Text = propValoreMisuratoUltimoDieci

                    propTolleranzaPiupropValorePrevistoUltimoUno = datareader("TolleranzaPiuValorePrevistoUltimoUno").ToString
                    txtTolleranzaPiuValPrev1UltimoPz.Text = propTolleranzaPiupropValorePrevistoUltimoUno
                    propTolleranzaPiupropValorePrevistoUltimoDue = datareader("TolleranzaPiuValorePrevistoUltimoDue").ToString
                    txtTolleranzaPiuValPrev2UltimoPz.Text = propTolleranzaPiupropValorePrevistoUltimoDue
                    propTolleranzaPiupropValorePrevistoUltimoTre = datareader("TolleranzaPiuValorePrevistoUltimoTre").ToString
                    txtTolleranzaPiuValPrev3UltimoPz.Text = propTolleranzaPiupropValorePrevistoUltimoTre
                    propTolleranzaPiupropValorePrevistoUltimoQuattro = datareader("TolleranzaPiuValorePrevistoUltimoQuattro").ToString
                    txtTolleranzaPiuValPrev4UltimoPz.Text = propTolleranzaPiupropValorePrevistoUltimoQuattro
                    propTolleranzaPiupropValorePrevistoUltimoCinque = datareader("TolleranzaPiuValorePrevistoUltimoCinque").ToString
                    txtTolleranzaPiuValPrev5UltimoPz.Text = propTolleranzaPiupropValorePrevistoUltimoCinque
                    propTolleranzaPiupropValorePrevistoUltimoSei = datareader("TolleranzaPiuValorePrevistoUltimoSei").ToString
                    txtTolleranzaPiuValPrev6UltimoPz.Text = propTolleranzaPiupropValorePrevistoUltimoSei
                    propTolleranzaPiupropValorePrevistoUltimoSette = datareader("TolleranzaPiuValorePrevistoUltimoSette").ToString
                    txtTolleranzaPiuValPrev7UltimoPz.Text = propTolleranzaPiupropValorePrevistoUltimoSette
                    propTolleranzaPiupropValorePrevistoUltimoOtto = datareader("TolleranzaPiuValorePrevistoUltimoOtto").ToString
                    txtTolleranzaPiuValPrev8UltimoPz.Text = propTolleranzaPiupropValorePrevistoUltimoOtto
                    propTolleranzaPiupropValorePrevistoUltimoNove = datareader("TolleranzaPiuValorePrevistoUltimoNove").ToString
                    txtTolleranzaPiuValPrev9UltimoPz.Text = propTolleranzaPiupropValorePrevistoUltimoNove
                    propTolleranzaPiupropValorePrevistoUltimoDieci = datareader("TolleranzaPiuValorePrevistoUltimoDieci").ToString
                    txtTolleranzaPiuValPrev10UltimoPz.Text = propTolleranzaPiupropValorePrevistoUltimoDieci

                    propTolleranzaMenopropValorePrevistoUltimoUno = datareader("TolleranzaMenoValorePrevistoUltimoUno").ToString
                    txtTolleranzaMenoValPrev1UltimoPz.Text = propTolleranzaMenopropValorePrevistoUltimoUno
                    propTolleranzaMenopropValorePrevistoUltimoDue = datareader("TolleranzaMenoValorePrevistoUltimoDue").ToString
                    txtTolleranzaMenoValPrev2UltimoPz.Text = propTolleranzaMenopropValorePrevistoUltimoDue
                    propTolleranzaMenopropValorePrevistoUltimoTre = datareader("TolleranzaMenoValorePrevistoUltimoTre").ToString
                    txtTolleranzaMenoValPrev3UltimoPz.Text = propTolleranzaMenopropValorePrevistoUltimoTre
                    propTolleranzaMenopropValorePrevistoUltimoQuattro = datareader("TolleranzaMenoValorePrevistoUltimoQuattro").ToString
                    txtTolleranzaMenoValPrev4UltimoPz.Text = propTolleranzaMenopropValorePrevistoUltimoQuattro
                    propTolleranzaMenopropValorePrevistoUltimoCinque = datareader("TolleranzaMenoValorePrevistoUltimoCinque").ToString
                    txtTolleranzaMenoValPrev5UltimoPz.Text = propTolleranzaMenopropValorePrevistoUltimoCinque
                    propTolleranzaMenopropValorePrevistoUltimoSei = datareader("TolleranzaMenoValorePrevistoUltimoSei").ToString
                    txtTolleranzaMenoValPrev6UltimoPz.Text = propTolleranzaMenopropValorePrevistoUltimoSei
                    propTolleranzaMenopropValorePrevistoUltimoSette = datareader("TolleranzaMenoValorePrevistoUltimoSette").ToString
                    txtTolleranzaMenoValPrev7UltimoPz.Text = propTolleranzaMenopropValorePrevistoUltimoSette
                    propTolleranzaMenopropValorePrevistoUltimoOtto = datareader("TolleranzaMenoValorePrevistoUltimoOtto").ToString
                    txtTolleranzaMenoValPrev8UltimoPz.Text = propTolleranzaMenopropValorePrevistoUltimoOtto
                    propTolleranzaMenopropValorePrevistoUltimoNove = datareader("TolleranzaMenoValorePrevistoUltimoNove").ToString
                    txtTolleranzaMenoValPrev9UltimoPz.Text = propTolleranzaMenopropValorePrevistoUltimoNove
                    propTolleranzaMenopropValorePrevistoUltimoDieci = datareader("TolleranzaMenoValorePrevistoUltimoDieci").ToString
                    txtTolleranzaMenoValPrev10UltimoPz.Text = propTolleranzaMenopropValorePrevistoUltimoDieci

                    propNoteUltimoPezzo = datareader("NoteUltimoPezzo").ToString
                    txtNoteUltimoPz.Text = propNoteUltimoPezzo

                End While


            End Using


        Catch ex As Exception
            MessageBox.Show("Errore getUltimoPezzoByIdIntestazione : " & ex.Message)
        End Try


    End Sub


#End Region

#Region "Aggiorna Testata Report"

    Public Function AggiornaTestataReportBusinessLayer(id_Intestazione As Integer) As Integer

        Dim ret As Integer?
        Dim pezzoNumero As Integer?
        Dim result As DialogResult
        Dim materiale As String
        Dim strumento As String
        Dim macchinaNum As String
        Dim fornitore As String
        Dim operatore As String

        Try
            Dim iDIntestazione As Integer = id_Intestazione
            Dim numOrdine As String = txtNumOrdine.Text
            Dim codiceArticolo As String = txtCodArt.Text
            Dim data As Date = Convert(dtpData.Text)

            materiale = cmbMateriale.Text
            strumento = cmbStrumento.Text
            macchinaNum = cmbMacchNum.Text
            fornitore = cmbFornitore.Text
            operatore = cmbOperatore.Text

            Dim rigaOrdNum As Integer = CType(txtRigaNum.Text, Integer)
            Dim numPezzi As Integer = CType(txtPezziNum.Text, Integer)
            Dim numLotto As String = cmbLottoNum.Text
            Dim primoPezzo As Boolean = chkPrimoPezzo.Checked
            Dim ultimoPezzo As Boolean = chkUltimoPezzo.Checked
            pezzoNumero = CType(txtPezziControllati.Text, Integer)
            'If pezzoNumero Is Nothing Then
            '    pezzoNumero = 0
            'Else
            '    pezzoNumero = CType(Me.txtPezziControllati.Text, Integer)
            'End If

            ret = AggiornaTestataReport(iDIntestazione, numOrdine, codiceArticolo, data, materiale, strumento,
                   macchinaNum, rigaOrdNum, numPezzi, fornitore, numLotto, primoPezzo, ultimoPezzo, pezzoNumero, operatore)

            If Not ret Is Nothing Then
                Return ret
            Else
                Exit Function
            End If

        Catch ex As Exception
            MessageBox.Show("Errore AggiornaTestataReportBusinessLayer :" & ex.Message)
        End Try

    End Function


    Public Function AggiornaTestataReport(idIntestazione As Integer, numOrdine As String,
codiceArticolo As String, data As Date, materiale As String,
strumento As String,
macchinanum As String, rigaordnum As Integer,
numpezzi As Integer, fornitore As String, numlotto As String,
primopezzo As Boolean, ultimopezzo As Boolean,
pezzonumero As Integer, operatore As String) As Integer

        Dim rowsAffected As Integer
        Dim dataShorth As Date = Convert(data)

        Dim updateCommand As DbCommand = Nothing
        updateCommand = _db.GetStoredProcCommand("spUpdateIntestazioneTestReport")

        Try

            _db.AddInParameter(updateCommand, "IDIntestazione", DbType.Int32, idIntestazione)
            _db.AddInParameter(updateCommand, "NumOrdine", DbType.String, numOrdine)
            _db.AddInParameter(updateCommand, "CodArticolo", DbType.String, codiceArticolo)
            _db.AddInParameter(updateCommand, "Data", DbType.Date, dataShorth)
            _db.AddInParameter(updateCommand, "Materiale", DbType.String, materiale)
            _db.AddInParameter(updateCommand, "Strumento", DbType.String, strumento)
            _db.AddInParameter(updateCommand, "MacchinaNum", DbType.String, macchinanum)
            _db.AddInParameter(updateCommand, "RigaOrdNum", DbType.Int32, rigaordnum)
            _db.AddInParameter(updateCommand, "NumPezzi", DbType.Int32, numpezzi)
            _db.AddInParameter(updateCommand, "Fornitore", DbType.String, fornitore)
            _db.AddInParameter(updateCommand, "NumLotto", DbType.String, numlotto)
            _db.AddInParameter(updateCommand, "PrimoPezzo", DbType.Boolean, primopezzo)
            _db.AddInParameter(updateCommand, "UltimoPezzo", DbType.Boolean, ultimopezzo)
            _db.AddInParameter(updateCommand, "PezzoNumero", DbType.Int32, pezzonumero)
            _db.AddInParameter(updateCommand, "Operatore", DbType.String, operatore)

            ' Dim parameter As SqlParameter = DataSet.UpdateCommand.Parameters.Add( _
            '"@oldCustomerID", SqlDbType.NChar, 5, "CustomerID")
            '          parameter.SourceVersion = DataRowVersion.Original


            rowsAffected = _db.ExecuteNonQuery(updateCommand)


        Catch ex As Exception
            MessageBox.Show("Errore AggiornaTestataReport: " & ex.Message)
        End Try

        Return rowsAffected

    End Function

#End Region

#Region "Aggiorna Primo Pezzo"

    Public Function checkIDPrimoPezzo(intIdItestazione As Integer) As Integer

        Dim rows As Integer
        Dim reader As DbDataReader
        Dim strSQL As String = "SELECT IDPezziPrimoPezzo, IDIntestazione " &
                               "FROM tblPezziPrimoPezzo " &
                               "WHERE (IDIntestazione = " & intIdItestazione & ")"



        Try
            Dim cmd As DbCommand = _db.GetSqlStringCommand(strSQL.ToString)


            Using datareader As IDataReader = _db.ExecuteReader(cmd)
                While datareader.Read
                    rows += 1
                End While
            End Using

        Catch ex As Exception
            MessageBox.Show("Errore checkIDPrimoPezzo :" & ex.Message)
        End Try

        Return rows

    End Function

    Public Function insertIDPrimoPezzo() As Integer
        Dim intContatorePrimoPezzo As Integer = propIdIntestazione
        Dim result As DialogResult

        Dim IDPezziPrimoPezzo As Integer = intContatorePrimoPezzo
        Dim IDIntestazione As Integer = intContatorePrimoPezzo

        Try
            Dim retPrimoSoloID As Integer? = InsertPezziPrimoPezzoSoloID(IDPezziPrimoPezzo, IDIntestazione)

            If Not retPrimoSoloID Is Nothing Then
                Return retPrimoSoloID
            End If
        Catch ex As Exception
            MessageBox.Show("Errore insertIDPrimoPezzo :" & ex.Message)
        End Try
    End Function

    Public Function InsertPezziPrimoPezzoSoloID(IDPezziPrimoPezzo As Integer, IDIntestazione As Integer) As Integer


        Dim insertCommandPrimoPezzoSoloID As DbCommand = Nothing
        Dim rowsAffectedPrimoPezzoSoloID As Integer = Nothing

        Dim strQuery As String = "INSERT INTO [dbo].[tblPezziPrimoPezzo] ([IDPezziPrimoPezzo],[IDIntestazione]) VALUES (@IDPezziPrimoPezzo,@IDIntestazione)"

        Try
            insertCommandPrimoPezzoSoloID = _db.GetSqlStringCommand(strQuery)


            _db.AddInParameter(insertCommandPrimoPezzoSoloID, "IDPezziPrimoPezzo", DbType.Int32, IDPezziPrimoPezzo)
            _db.AddInParameter(insertCommandPrimoPezzoSoloID, "IDIntestazione", DbType.Int32, IDIntestazione)

            rowsAffectedPrimoPezzoSoloID = _db.ExecuteNonQuery(insertCommandPrimoPezzoSoloID)

        Catch ex As Exception
            MessageBox.Show("Errore InsertPezziPrimoPezzoSoloID Query :" & ex.Message)
        End Try

        Return rowsAffectedPrimoPezzoSoloID


    End Function



    Public Function AggiornaPrimoPezzo(IdPezziPrimoPezzo As Integer,
IDIntestazione As Integer,
ValorePrevistoPrimoPezzoUno As String,
ValorePrevistoPrimoPezzoDue As String,
ValorePrevistoPrimoPezzoTre As String,
ValorePrevistoPrimoPezzoQuattro As String,
ValorePrevistoPrimoPezzoCinque As String,
ValorePrevistoPrimoPezzoSei As String,
ValorePrevistoPrimoPezzoSette As String,
ValorePrevistoPrimoPezzoOtto As String,
ValorePrevistoPrimoPezzoNove As String,
ValorePrevistoPrimoPezzoDieci As String,
ValoreMisuratoPrimoPezzoUno As String,
ValoreMisuratoPrimoPezzoDue As String,
ValoreMisuratoPrimoPezzoTre As String,
ValoreMisuratoPrimoPezzoQuattro As String,
ValoreMisuratoPrimoPezzoCinque As String,
ValoreMisuratoPrimoPezzoSei As String,
ValoreMisuratoPrimoPezzoSette As String,
ValoreMisuratoPrimoPezzoOtto As String,
ValoreMisuratoPrimoPezzoNove As String,
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
notePrimoPezzo As String) As Integer



        Dim rowsAffected As Integer
        Dim updateCommandPrimoPezzo As DbCommand = Nothing
        updateCommandPrimoPezzo = _db.GetStoredProcCommand("spUpdateQueryPrimoPezzo")

        Try

            _db.AddInParameter(updateCommandPrimoPezzo, "IdPezziPrimoPezzo", DbType.Int32, IdPezziPrimoPezzo)
            _db.AddInParameter(updateCommandPrimoPezzo, "IDIntestazione", DbType.Int32, IDIntestazione)
            _db.AddInParameter(updateCommandPrimoPezzo, "ValorePrevistoPrimoPezzoUno", DbType.String, ValorePrevistoPrimoPezzoUno)
            _db.AddInParameter(updateCommandPrimoPezzo, "ValorePrevistoPrimoPezzoDue", DbType.String, ValorePrevistoPrimoPezzoDue)
            _db.AddInParameter(updateCommandPrimoPezzo, "ValorePrevistoPrimoPezzoTre", DbType.String, ValorePrevistoPrimoPezzoTre)
            _db.AddInParameter(updateCommandPrimoPezzo, "ValorePrevistoPrimoPezzoQuattro", DbType.String, ValorePrevistoPrimoPezzoQuattro)
            _db.AddInParameter(updateCommandPrimoPezzo, "ValorePrevistoPrimoPezzoCinque", DbType.String, ValorePrevistoPrimoPezzoCinque)
            _db.AddInParameter(updateCommandPrimoPezzo, "ValorePrevistoPrimoPezzoSei", DbType.String, ValorePrevistoPrimoPezzoSei)
            _db.AddInParameter(updateCommandPrimoPezzo, "ValorePrevistoPrimoPezzoSette", DbType.String, ValorePrevistoPrimoPezzoSette)
            _db.AddInParameter(updateCommandPrimoPezzo, "ValorePrevistoPrimoPezzoOtto", DbType.String, ValorePrevistoPrimoPezzoOtto)
            _db.AddInParameter(updateCommandPrimoPezzo, "ValorePrevistoPrimoPezzoNove", DbType.String, ValorePrevistoPrimoPezzoNove)
            _db.AddInParameter(updateCommandPrimoPezzo, "ValorePrevistoPrimoPezzoDieci", DbType.String, ValorePrevistoPrimoPezzoDieci)

            _db.AddInParameter(updateCommandPrimoPezzo, "ValoreMisuratoPrimoPezzoUno", DbType.String, ValoreMisuratoPrimoPezzoUno)
            _db.AddInParameter(updateCommandPrimoPezzo, "ValoreMisuratoPrimoPezzoDue", DbType.String, ValoreMisuratoPrimoPezzoDue)
            _db.AddInParameter(updateCommandPrimoPezzo, "ValoreMisuratoPrimoPezzoTre", DbType.String, ValoreMisuratoPrimoPezzoTre)
            _db.AddInParameter(updateCommandPrimoPezzo, "ValoreMisuratoPrimoPezzoQuattro", DbType.String, ValoreMisuratoPrimoPezzoQuattro)
            _db.AddInParameter(updateCommandPrimoPezzo, "ValoreMisuratoPrimoPezzoCinque", DbType.String, ValoreMisuratoPrimoPezzoCinque)
            _db.AddInParameter(updateCommandPrimoPezzo, "ValoreMisuratoPrimoPezzoSei", DbType.String, ValoreMisuratoPrimoPezzoSei)
            _db.AddInParameter(updateCommandPrimoPezzo, "ValoreMisuratoPrimoPezzoSette", DbType.String, ValoreMisuratoPrimoPezzoSette)
            _db.AddInParameter(updateCommandPrimoPezzo, "ValoreMisuratoPrimoPezzoOtto", DbType.String, ValoreMisuratoPrimoPezzoOtto)
            _db.AddInParameter(updateCommandPrimoPezzo, "ValoreMisuratoPrimoPezzoNove", DbType.String, ValoreMisuratoPrimoPezzoNove)
            _db.AddInParameter(updateCommandPrimoPezzo, "ValoreMisuratoPrimoPezzoDieci", DbType.String, ValoreMisuratoPrimoPezzoDieci)

            _db.AddInParameter(updateCommandPrimoPezzo, "TolleranzaPiuValorePrevistoPrimoUno", DbType.String, TolleranzaPiuValorePrevistoPrimoUno)
            _db.AddInParameter(updateCommandPrimoPezzo, "TolleranzaPiuValorePrevistoPrimoDue", DbType.String, TolleranzaPiuValorePrevistoPrimoDue)
            _db.AddInParameter(updateCommandPrimoPezzo, "TolleranzaPiuValorePrevistoPrimoTre", DbType.String, TolleranzaPiuValorePrevistoPrimoTre)
            _db.AddInParameter(updateCommandPrimoPezzo, "TolleranzaPiuValorePrevistoPrimoQuattro", DbType.String, TolleranzaPiuValorePrevistoPrimoQuattro)
            _db.AddInParameter(updateCommandPrimoPezzo, "TolleranzaPiuValorePrevistoPrimoCinque", DbType.String, TolleranzaPiuValorePrevistoPrimoCinque)
            _db.AddInParameter(updateCommandPrimoPezzo, "TolleranzaPiuValorePrevistoPrimoSei", DbType.String, TolleranzaPiuValorePrevistoPrimoSei)
            _db.AddInParameter(updateCommandPrimoPezzo, "TolleranzaPiuValorePrevistoPrimoSette", DbType.String, TolleranzaPiuValorePrevistoPrimoSette)
            _db.AddInParameter(updateCommandPrimoPezzo, "TolleranzaPiuValorePrevistoPrimoOtto", DbType.String, TolleranzaPiuValorePrevistoPrimoOtto)
            _db.AddInParameter(updateCommandPrimoPezzo, "TolleranzaPiuValorePrevistoPrimoNove", DbType.String, TolleranzaPiuValorePrevistoPrimoNove)
            _db.AddInParameter(updateCommandPrimoPezzo, "TolleranzaPiuValorePrevistoPrimoDieci", DbType.String, TolleranzaPiuValorePrevistoPrimoDieci)

            _db.AddInParameter(updateCommandPrimoPezzo, "TolleranzaMenoValorePrevistoPrimoUno", DbType.String, TolleranzaMenoValorePrevistoPrimoUno)
            _db.AddInParameter(updateCommandPrimoPezzo, "TolleranzaMenoValorePrevistoPrimoDue", DbType.String, TolleranzaMenoValorePrevistoPrimoDue)
            _db.AddInParameter(updateCommandPrimoPezzo, "TolleranzaMenoValorePrevistoPrimoTre", DbType.String, TolleranzaMenoValorePrevistoPrimoTre)
            _db.AddInParameter(updateCommandPrimoPezzo, "TolleranzaMenoValorePrevistoPrimoQuattro", DbType.String, TolleranzaMenoValorePrevistoPrimoQuattro)
            _db.AddInParameter(updateCommandPrimoPezzo, "TolleranzaMenoValorePrevistoPrimoCinque", DbType.String, TolleranzaMenoValorePrevistoPrimoCinque)
            _db.AddInParameter(updateCommandPrimoPezzo, "TolleranzaMenoValorePrevistoPrimoSei", DbType.String, TolleranzaMenoValorePrevistoPrimoSei)
            _db.AddInParameter(updateCommandPrimoPezzo, "TolleranzaMenoValorePrevistoPrimoSette", DbType.String, TolleranzaMenoValorePrevistoPrimoSette)
            _db.AddInParameter(updateCommandPrimoPezzo, "TolleranzaMenoValorePrevistoPrimoOtto", DbType.String, TolleranzaMenoValorePrevistoPrimoOtto)
            _db.AddInParameter(updateCommandPrimoPezzo, "TolleranzaMenoValorePrevistoPrimoNove", DbType.String, TolleranzaMenoValorePrevistoPrimoNove)
            _db.AddInParameter(updateCommandPrimoPezzo, "TolleranzaMenoValorePrevistoPrimoDieci", DbType.String, TolleranzaMenoValorePrevistoPrimoDieci)
            _db.AddInParameter(updateCommandPrimoPezzo, "NotePrimoPezzo", DbType.String, notePrimoPezzo)

            rowsAffected = _db.ExecuteNonQuery(updateCommandPrimoPezzo)

        Catch ex As Exception
            MessageBox.Show("Errore AggiornaPrimoPezzo: " & ex.Message)
        End Try

        Return rowsAffected

    End Function


    Public Function AggiornaPrimoPezzoBusinessLayer(id_Intestazione As Integer) As Integer

        Dim ret As Integer?

        Try

            Dim IDPezziPrimoPezzo As Integer = id_Intestazione
            Dim IDIntestazione As Integer = id_Intestazione
            Dim ValorePrevistoPrimoPezzoUno As String = txtValPrev1Pz1.Text
            Dim ValorePrevistoPrimoPezzoDue As String = txtValPrev2Pz1.Text
            Dim ValorePrevistoPrimoPezzoTre As String = txtValPrev3Pz1.Text
            Dim ValorePrevistoPrimoPezzoQuattro As String = txtValPrev4Pz1.Text
            Dim ValorePrevistoPrimoPezzoCinque As String = txtValPrev5Pz1.Text
            Dim ValorePrevistoPrimoPezzoSei As String = txtValPrev6Pz1.Text
            Dim ValorePrevistoPrimoPezzoSette As String = txtValPrev7Pz1.Text
            Dim ValorePrevistoPrimoPezzoOtto As String = txtValPrev8Pz1.Text
            Dim ValorePrevistoPrimoPezzoNove As String = txtValPrev9Pz1.Text
            Dim ValorePrevistoPrimoPezzoDieci As String = txtValPrev10Pz1.Text

            Dim ValoreMisuratoPrimoPezzoUno As String = txt1ValMisPz1.Text
            Dim ValoreMisuratoPrimoPezzoDue As String = txt2ValMisPz1.Text
            Dim ValoreMisuratoPrimoPezzoTre As String = txt3ValMisPz1.Text
            Dim ValoreMisuratoPrimoPezzoQuattro As String = txt4ValMisPz1.Text
            Dim ValoreMisuratoPrimoPezzoCinque As String = txt5ValMisPz1.Text
            Dim ValoreMisuratoPrimoPezzoSei As String = txt6ValMisPz1.Text
            Dim ValoreMisuratoPrimoPezzoSette As String = txt7ValMisPz1.Text
            Dim ValoreMisuratoPrimoPezzoOtto As String = txt8ValMisPz1.Text
            Dim ValoreMisuratoPrimoPezzoNove As String = txt9ValMisPz1.Text
            Dim ValoreMisuratoPrimoPezzoDieci As String = txt10ValMisPz1.Text

            Dim TolleranzaPiuValorePrevistoPrimoUno As String = txtTolleranzaPiuValPrev1Pz1.Text
            Dim TolleranzaPiuValorePrevistoPrimoDue As String = txtTolleranzaPiuValPrev2Pz1.Text
            Dim TolleranzaPiuValorePrevistoPrimoTre As String = txtTolleranzaPiuValPrev3Pz1.Text
            Dim TolleranzaPiuValorePrevistoPrimoQuattro As String = txtTolleranzaPiuValPrev4Pz1.Text
            Dim TolleranzaPiuValorePrevistoPrimoCinque As String = txtTolleranzaPiuValPrev5Pz1.Text
            Dim TolleranzaPiuValorePrevistoPrimoSei As String = txtTolleranzaPiuValPrev6Pz1.Text
            Dim TolleranzaPiuValorePrevistoPrimoSette As String = txtTolleranzaPiuValPrev7Pz1.Text
            Dim TolleranzaPiuValorePrevistoPrimoOtto As String = txtTolleranzaPiuValPrev8Pz1.Text
            Dim TolleranzaPiuValorePrevistoPrimoNove As String = txtTolleranzaPiuValPrev9Pz1.Text
            Dim TolleranzaPiuValorePrevistoPrimoDieci As String = txtTolleranzaPiuValPrev10Pz1.Text

            Dim TolleranzaMenoValorePrevistoPrimoUno As String = txtTolleranzaMenoValPrev1Pz1.Text
            Dim TolleranzaMenoValorePrevistoPrimoDue As String = txtTolleranzaMenoValPrev2Pz1.Text
            Dim TolleranzaMenoValorePrevistoPrimoTre As String = txtTolleranzaMenoValPrev3Pz1.Text
            Dim TolleranzaMenoValorePrevistoPrimoQuattro As String = txtTolleranzaMenoValPrev4Pz1.Text
            Dim TolleranzaMenoValorePrevistoPrimoCinque As String = txtTolleranzaMenoValPrev5Pz1.Text
            Dim TolleranzaMenoValorePrevistoPrimoSei As String = txtTolleranzaMenoValPrev6Pz1.Text
            Dim TolleranzaMenoValorePrevistoPrimoSette As String = txtTolleranzaMenoValPrev7Pz1.Text
            Dim TolleranzaMenoValorePrevistoPrimoOtto As String = txtTolleranzaMenoValPrev8Pz1.Text
            Dim TolleranzaMenoValorePrevistoPrimoNove As String = txtTolleranzaMenoValPrev9Pz1.Text
            Dim TolleranzaMenoValorePrevistoPrimoDieci As String = txtTolleranzaMenoValPrev10Pz1.Text

            Dim NotePrimoPezzo As String = txtNotePz1.Text

            ret = AggiornaPrimoPezzo(IDPezziPrimoPezzo,
            IDIntestazione, ValorePrevistoPrimoPezzoUno,
            ValorePrevistoPrimoPezzoDue,
            ValorePrevistoPrimoPezzoTre,
            ValorePrevistoPrimoPezzoQuattro,
            ValorePrevistoPrimoPezzoCinque,
            ValorePrevistoPrimoPezzoSei,
            ValorePrevistoPrimoPezzoSette,
            ValorePrevistoPrimoPezzoOtto,
            ValorePrevistoPrimoPezzoNove,
            ValorePrevistoPrimoPezzoDieci,
            ValoreMisuratoPrimoPezzoUno,
            ValoreMisuratoPrimoPezzoDue,
            ValoreMisuratoPrimoPezzoTre,
            ValoreMisuratoPrimoPezzoQuattro,
            ValoreMisuratoPrimoPezzoCinque,
            ValoreMisuratoPrimoPezzoSei,
            ValoreMisuratoPrimoPezzoSette,
            ValoreMisuratoPrimoPezzoOtto,
            ValoreMisuratoPrimoPezzoNove,
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

            If Not ret Is Nothing Then
                Return ret
            Else
                Exit Function
            End If

        Catch ex As Exception
            MessageBox.Show("Errore AggiornaPrimoPezzoBusinessLayer :" & ex.Message)
        End Try

    End Function

#End Region

#Region "Aggiorna Secondo Pezzo"

    Public Function checkIDSecondoPezzo(intIdItestazione As Integer) As Integer

        Dim rows As Integer
        Dim reader As DbDataReader
        Dim strSQL As String = "SELECT IDPezziSecondoPezzo, IDIntestazione " &
                               "FROM tblPezziSecondoPezzo " &
                               "WHERE (IDIntestazione = " & intIdItestazione & ")"

        Try
            Dim cmd As DbCommand = _db.GetSqlStringCommand(strSQL.ToString)

            Using datareader As IDataReader = _db.ExecuteReader(cmd)
                While datareader.Read
                    rows += 1
                End While
            End Using

        Catch ex As Exception
            MessageBox.Show("Errore checkIDSecondoPezzo :" & ex.Message)
        End Try

        Return rows

    End Function


    Public Function insertIDSecondoPezzo() As Integer
        Dim intContatoreSecondoPezzo As Integer = propIdIntestazione
        Dim result As DialogResult

        Dim IDPezziSecondoPezzo As Integer = intContatoreSecondoPezzo
        Dim IDIntestazione As Integer = intContatoreSecondoPezzo

        Try
            Dim retSecondoSoloID As Integer? = InsertPezziSecondoPezzoSoloID(IDPezziSecondoPezzo, IDIntestazione)

            If Not retSecondoSoloID Is Nothing Then
                Return retSecondoSoloID
            End If
        Catch ex As Exception
            MessageBox.Show("Errore insertIDSecondoPezzo :" & ex.Message)
        End Try
    End Function

    Public Function InsertPezziSecondoPezzoSoloID(IDPezziSecondoPezzo As Integer, IDIntestazione As Integer) As Integer


        Dim insertCommandSecondoPezzoSoloID As DbCommand = Nothing
        Dim rowsAffectedSecondoPezzoSoloID As Integer = Nothing

        Dim strQuery As String = "INSERT INTO [dbo].[tblPezziSecondoPezzo] ([IDPezziSecondoPezzo],[IDIntestazione]) VALUES (@IDPezziSecondoPezzo,@IDIntestazione)"

        Try
            insertCommandSecondoPezzoSoloID = _db.GetSqlStringCommand(strQuery)


            _db.AddInParameter(insertCommandSecondoPezzoSoloID, "IDPezziSecondoPezzo", DbType.Int32, IDPezziSecondoPezzo)
            _db.AddInParameter(insertCommandSecondoPezzoSoloID, "IDIntestazione", DbType.Int32, IDIntestazione)

            rowsAffectedSecondoPezzoSoloID = _db.ExecuteNonQuery(insertCommandSecondoPezzoSoloID)

        Catch ex As Exception
            MessageBox.Show("Errore InsertPezziTerzoPezzoSoloID Query :" & ex.Message)
        End Try

        Return rowsAffectedSecondoPezzoSoloID


    End Function

    Public Function AggiornaSecondoPezzo(IdPezziSecondoPezzo As Integer,
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



        Dim rowsAffected As Integer
        Dim updateCommandSecondoPezzo As DbCommand = Nothing
        updateCommandSecondoPezzo = _db.GetStoredProcCommand("spUpdateQuerySecondoPezzo")

        Try

            _db.AddInParameter(updateCommandSecondoPezzo, "IdPezziSecondoPezzo", DbType.Int32, IdPezziSecondoPezzo)
            _db.AddInParameter(updateCommandSecondoPezzo, "IDIntestazione", DbType.Int32, IDIntestazione)
            _db.AddInParameter(updateCommandSecondoPezzo, "ValorePrevistoSecondoPezzoUno", DbType.String, ValorePrevistoSecondoPezzoUno)
            _db.AddInParameter(updateCommandSecondoPezzo, "ValorePrevistoSecondoPezzoDue", DbType.String, ValorePrevistoSecondoPezzoDue)
            _db.AddInParameter(updateCommandSecondoPezzo, "ValorePrevistoSecondoPezzoTre", DbType.String, ValorePrevistoSecondoPezzoTre)
            _db.AddInParameter(updateCommandSecondoPezzo, "ValorePrevistoSecondoPezzoQuattro", DbType.String, ValorePrevistoSecondoPezzoQuattro)
            _db.AddInParameter(updateCommandSecondoPezzo, "ValorePrevistoSecondoPezzoCinque", DbType.String, ValorePrevistoSecondoPezzoCinque)
            _db.AddInParameter(updateCommandSecondoPezzo, "ValorePrevistoSecondoPezzoSei", DbType.String, ValorePrevistoSecondoPezzoSei)
            _db.AddInParameter(updateCommandSecondoPezzo, "ValorePrevistoSecondoPezzoSette", DbType.String, ValorePrevistoSecondoPezzoSette)
            _db.AddInParameter(updateCommandSecondoPezzo, "ValorePrevistoSecondoPezzoOtto", DbType.String, ValorePrevistoSecondoPezzoOtto)
            _db.AddInParameter(updateCommandSecondoPezzo, "ValorePrevistoSecondoPezzoNove", DbType.String, ValorePrevistoSecondoPezzoNove)
            _db.AddInParameter(updateCommandSecondoPezzo, "ValorePrevistoSecondoPezzoDieci", DbType.String, ValorePrevistoSecondoPezzoDieci)
            _db.AddInParameter(updateCommandSecondoPezzo, "ValoreMisuratoSecondoPezzoUno", DbType.String, ValoreMisuratoSecondoPezzoUno)
            _db.AddInParameter(updateCommandSecondoPezzo, "ValoreMisuratoSecondoPezzoDue", DbType.String, ValoreMisuratoSecondoPezzoDue)
            _db.AddInParameter(updateCommandSecondoPezzo, "ValoreMisuratoSecondoPezzoTre", DbType.String, ValoreMisuratoSecondoPezzoTre)
            _db.AddInParameter(updateCommandSecondoPezzo, "ValoreMisuratoSecondoPezzoQuattro", DbType.String, ValoreMisuratoSecondoPezzoQuattro)
            _db.AddInParameter(updateCommandSecondoPezzo, "ValoreMisuratoSecondoPezzoCinque", DbType.String, ValoreMisuratoSecondoPezzoCinque)
            _db.AddInParameter(updateCommandSecondoPezzo, "ValoreMisuratoSecondoPezzoSei", DbType.String, ValoreMisuratoSecondoPezzoSei)
            _db.AddInParameter(updateCommandSecondoPezzo, "ValoreMisuratoSecondoPezzoSette", DbType.String, ValoreMisuratoSecondoPezzoSette)
            _db.AddInParameter(updateCommandSecondoPezzo, "ValoreMisuratoSecondoPezzoOtto", DbType.String, ValoreMisuratoSecondoPezzoOtto)
            _db.AddInParameter(updateCommandSecondoPezzo, "ValoreMisuratoSecondoPezzoNove", DbType.String, ValoreMisuratoSecondoPezzoNove)
            _db.AddInParameter(updateCommandSecondoPezzo, "ValoreMisuratoSecondoPezzoDieci", DbType.String, ValoreMisuratoSecondoPezzoDieci)


            _db.AddInParameter(updateCommandSecondoPezzo, "TolleranzaPiuValorePrevistoSecondoUno", DbType.String, TolleranzaPiuValorePrevistoSecondoUno)
            _db.AddInParameter(updateCommandSecondoPezzo, "TolleranzaPiuValorePrevistoSecondoDue", DbType.String, TolleranzaPiuValorePrevistoSecondoDue)
            _db.AddInParameter(updateCommandSecondoPezzo, "TolleranzaPiuValorePrevistoSecondoTre", DbType.String, TolleranzaPiuValorePrevistoSecondoTre)
            _db.AddInParameter(updateCommandSecondoPezzo, "TolleranzaPiuValorePrevistoSecondoQuattro", DbType.String, TolleranzaPiuValorePrevistoSecondoQuattro)
            _db.AddInParameter(updateCommandSecondoPezzo, "TolleranzaPiuValorePrevistoSecondoCinque", DbType.String, TolleranzaPiuValorePrevistoSecondoCinque)
            _db.AddInParameter(updateCommandSecondoPezzo, "TolleranzaPiuValorePrevistoSecondoSei", DbType.String, TolleranzaPiuValorePrevistoSecondoSei)
            _db.AddInParameter(updateCommandSecondoPezzo, "TolleranzaPiuValorePrevistoSecondoSette", DbType.String, TolleranzaPiuValorePrevistoSecondoSette)
            _db.AddInParameter(updateCommandSecondoPezzo, "TolleranzaPiuValorePrevistoSecondoOtto", DbType.String, TolleranzaPiuValorePrevistoSecondoOtto)
            _db.AddInParameter(updateCommandSecondoPezzo, "TolleranzaPiuValorePrevistoSecondoNove", DbType.String, TolleranzaPiuValorePrevistoSecondoNove)
            _db.AddInParameter(updateCommandSecondoPezzo, "TolleranzaPiuValorePrevistoSecondoDieci", DbType.String, TolleranzaPiuValorePrevistoSecondoDieci)
            _db.AddInParameter(updateCommandSecondoPezzo, "TolleranzaMenoValorePrevistoSecondoUno", DbType.String, TolleranzaMenoValorePrevistoSecondoUno)
            _db.AddInParameter(updateCommandSecondoPezzo, "TolleranzaMenoValorePrevistoSecondoDue", DbType.String, TolleranzaMenoValorePrevistoSecondoDue)
            _db.AddInParameter(updateCommandSecondoPezzo, "TolleranzaMenoValorePrevistoSecondoTre", DbType.String, TolleranzaMenoValorePrevistoSecondoTre)
            _db.AddInParameter(updateCommandSecondoPezzo, "TolleranzaMenoValorePrevistoSecondoQuattro", DbType.String, TolleranzaMenoValorePrevistoSecondoQuattro)
            _db.AddInParameter(updateCommandSecondoPezzo, "TolleranzaMenoValorePrevistoSecondoCinque", DbType.String, TolleranzaMenoValorePrevistoSecondoCinque)
            _db.AddInParameter(updateCommandSecondoPezzo, "TolleranzaMenoValorePrevistoSecondoSei", DbType.String, TolleranzaMenoValorePrevistoSecondoSei)
            _db.AddInParameter(updateCommandSecondoPezzo, "TolleranzaMenoValorePrevistoSecondoSette", DbType.String, TolleranzaMenoValorePrevistoSecondoSette)
            _db.AddInParameter(updateCommandSecondoPezzo, "TolleranzaMenoValorePrevistoSecondoOtto", DbType.String, TolleranzaMenoValorePrevistoSecondoOtto)
            _db.AddInParameter(updateCommandSecondoPezzo, "TolleranzaMenoValorePrevistoSecondoNove", DbType.String, TolleranzaMenoValorePrevistoSecondoNove)
            _db.AddInParameter(updateCommandSecondoPezzo, "TolleranzaMenoValorePrevistoSecondoDieci", DbType.String, TolleranzaMenoValorePrevistoSecondoDieci)
            _db.AddInParameter(updateCommandSecondoPezzo, "NoteSecondoPezzo", DbType.String, NoteSecondoPezzo)




            rowsAffected = _db.ExecuteNonQuery(updateCommandSecondoPezzo)


        Catch ex As Exception
            MessageBox.Show("Errore AggiornaSecondoPezzo : " & ex.Message)
        End Try

        Return rowsAffected

    End Function


    Public Function AggiornaSecondoPezzoBusinessLayer(id_Intestazione As Integer) As Integer

        Dim ret As Integer?

        Try

            Dim IDPezziSecondoPezzo As Integer = id_Intestazione
            Dim IDIntestazione As Integer = id_Intestazione
            Dim ValorePrevistoSecondoUno As String = txtValPrev1Pz2.Text
            Dim ValorePrevistoSecondoDue As String = txtValPrev2Pz2.Text
            Dim ValorePrevistoSecondoTre As String = txtValPrev3Pz2.Text
            Dim ValorePrevistoSecondoQuattro As String = txtValPrev4Pz2.Text
            Dim ValorePrevistoSecondoCinque As String = txtValPrev5Pz2.Text
            Dim ValorePrevistoSecondoSei As String = txtValPrev6Pz2.Text
            Dim ValorePrevistoSecondoSette As String = txtValPrev7Pz2.Text
            Dim ValorePrevistoSecondoOtto As String = txtValPrev8Pz2.Text
            Dim ValorePrevistoSecondoNove As String = txtValPrev9Pz2.Text
            Dim ValorePrevistoSecondoDieci As String = txtValPrev10Pz2.Text

            Dim ValoreMisuratoSecondoUno As String = txt1ValMisPz2.Text
            Dim ValoreMisuratoSecondoDue As String = txt2ValMisPz2.Text
            Dim ValoreMisuratoSecondoTre As String = txt3ValMisPz2.Text
            Dim ValoreMisuratoSecondoQuattro As String = txt4ValMisPz2.Text
            Dim ValoreMisuratoSecondoCinque As String = txt5ValMisPz2.Text
            Dim ValoreMisuratoSecondoSei As String = txt6ValMisPz2.Text
            Dim ValoreMisuratoSecondoSette As String = txt7ValMisPz2.Text
            Dim ValoreMisuratoSecondoOtto As String = txt8ValMisPz2.Text
            Dim ValoreMisuratoSecondoNove As String = txt9ValMisPz2.Text
            Dim ValoreMisuratoSecondoDieci As String = txt10ValMisPz2.Text

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

            ret = AggiornaSecondoPezzo(IDPezziSecondoPezzo,
            IDIntestazione,
            ValorePrevistoSecondoUno,
            ValorePrevistoSecondoDue,
            ValorePrevistoSecondoTre,
            ValorePrevistoSecondoQuattro,
            ValorePrevistoSecondoCinque,
            ValorePrevistoSecondoSei,
            ValorePrevistoSecondoSette,
            ValorePrevistoSecondoOtto,
            ValorePrevistoSecondoNove,
            ValorePrevistoSecondoDieci,
            ValoreMisuratoSecondoUno,
            ValoreMisuratoSecondoDue,
            ValoreMisuratoSecondoTre,
            ValoreMisuratoSecondoQuattro,
            ValoreMisuratoSecondoCinque,
            ValoreMisuratoSecondoSei,
            ValoreMisuratoSecondoSette,
            ValoreMisuratoSecondoOtto,
            ValoreMisuratoSecondoNove,
            ValoreMisuratoSecondoDieci,
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

            If Not ret Is Nothing Then
                Return ret
            Else
                Exit Function
            End If

        Catch ex As Exception
            MessageBox.Show("Errore AggiornaSecondoPezzoBusinessLayer :" & ex.Message)
        End Try

    End Function



#End Region

#Region "Aggiorna Terzo Pezzo"

    Public Function checkIDTerzoPezzo(intIdItestazione As Integer) As Integer

        Dim rows As Integer
        Dim reader As DbDataReader
        Dim strSQL As String = "SELECT IDPezziTerzoPezzo, IDIntestazione " &
                               "FROM tblPezziTerzoPezzo " &
                               "WHERE (IDIntestazione = " & intIdItestazione & ")"



        Try
            Dim cmd As DbCommand = _db.GetSqlStringCommand(strSQL.ToString)


            Using datareader As IDataReader = _db.ExecuteReader(cmd)
                While datareader.Read
                    rows += 1
                End While
            End Using

        Catch ex As Exception
            MessageBox.Show("Errore checkIDTerzoPezzo :" & ex.Message)
        End Try

        Return rows

    End Function

    Public Function insertIDTerzoPezzo() As Integer
        Dim intContatoreTerzoPezzo As Integer = propIdIntestazione
        Dim result As DialogResult

        Dim IDPezziTerzoPezzo As Integer = intContatoreTerzoPezzo
        Dim IDIntestazione As Integer = intContatoreTerzoPezzo

        Try
            Dim retTerzoSoloID As Integer? = InsertPezziTerzoPezzoSoloID(IDPezziTerzoPezzo, IDIntestazione)

            If Not retTerzoSoloID Is Nothing Then
                Return retTerzoSoloID
            End If
        Catch ex As Exception
            MessageBox.Show("Errore insertIDTerzoPezzo :" & ex.Message)
        End Try
    End Function

    Public Function InsertPezziTerzoPezzoSoloID(IDPezziTerzoPezzo As Integer, IDIntestazione As Integer) As Integer


        Dim insertCommandTerzoPezzoSoloID As DbCommand = Nothing
        Dim rowsAffectedTerzoPezzoSoloID As Integer = Nothing

        Dim strQuery As String = "INSERT INTO [dbo].[tblPezziTerzoPezzo] ([IDPezziTerzoPezzo],[IDIntestazione]) VALUES (@IDPezziTerzoPezzo,@IDIntestazione)"

        Try
            insertCommandTerzoPezzoSoloID = _db.GetSqlStringCommand(strQuery)


            _db.AddInParameter(insertCommandTerzoPezzoSoloID, "IDPezziTerzoPezzo", DbType.Int32, IDPezziTerzoPezzo)
            _db.AddInParameter(insertCommandTerzoPezzoSoloID, "IDIntestazione", DbType.Int32, IDIntestazione)

            rowsAffectedTerzoPezzoSoloID = _db.ExecuteNonQuery(insertCommandTerzoPezzoSoloID)

        Catch ex As Exception
            MessageBox.Show("Errore InsertPezziTerzoPezzoSoloID Query :" & ex.Message)
        End Try

        Return rowsAffectedTerzoPezzoSoloID


    End Function



    Public Function AggiornaTerzoPezzo(IdPezziTerzoPezzo As Integer,
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

        Dim rowsAffected As Integer
        Dim updateCommandTerzoPezzo As DbCommand = Nothing
        updateCommandTerzoPezzo = _db.GetStoredProcCommand("spUpdateQueryTerzoPezzo")

        Try

            _db.AddInParameter(updateCommandTerzoPezzo, "IdPezziTerzoPezzo", DbType.Int32, IdPezziTerzoPezzo)
            _db.AddInParameter(updateCommandTerzoPezzo, "IDIntestazione", DbType.Int32, IDIntestazione)
            _db.AddInParameter(updateCommandTerzoPezzo, "ValorePrevistoTerzoPezzoUno", DbType.String, ValorePrevistoTerzoPezzoUno)
            _db.AddInParameter(updateCommandTerzoPezzo, "ValorePrevistoTerzoPezzoDue", DbType.String, ValorePrevistoTerzoPezzoDue)
            _db.AddInParameter(updateCommandTerzoPezzo, "ValorePrevistoTerzoPezzoTre", DbType.String, ValorePrevistoTerzoPezzoTre)
            _db.AddInParameter(updateCommandTerzoPezzo, "ValorePrevistoTerzoPezzoQuattro", DbType.String, ValorePrevistoTerzoPezzoQuattro)
            _db.AddInParameter(updateCommandTerzoPezzo, "ValorePrevistoTerzoPezzoCinque", DbType.String, ValorePrevistoTerzoPezzoCinque)
            _db.AddInParameter(updateCommandTerzoPezzo, "ValorePrevistoTerzoPezzoSei", DbType.String, ValorePrevistoTerzoPezzoSei)
            _db.AddInParameter(updateCommandTerzoPezzo, "ValorePrevistoTerzoPezzoSette", DbType.String, ValorePrevistoTerzoPezzoSette)
            _db.AddInParameter(updateCommandTerzoPezzo, "ValorePrevistoTerzoPezzoOtto", DbType.String, ValorePrevistoTerzoPezzoOtto)
            _db.AddInParameter(updateCommandTerzoPezzo, "ValorePrevistoTerzoPezzoNove", DbType.String, ValorePrevistoTerzoPezzoNove)
            _db.AddInParameter(updateCommandTerzoPezzo, "ValorePrevistoTerzoPezzoDieci", DbType.String, ValorePrevistoTerzoPezzoDieci)
            _db.AddInParameter(updateCommandTerzoPezzo, "ValoreMisuratoTerzoPezzoUno", DbType.String, ValoreMisuratoTerzoPezzoUno)
            _db.AddInParameter(updateCommandTerzoPezzo, "ValoreMisuratoTerzoPezzoDue", DbType.String, ValoreMisuratoTerzoPezzoDue)
            _db.AddInParameter(updateCommandTerzoPezzo, "ValoreMisuratoTerzoPezzoTre", DbType.String, ValoreMisuratoTerzoPezzoTre)
            _db.AddInParameter(updateCommandTerzoPezzo, "ValoreMisuratoTerzoPezzoQuattro", DbType.String, ValoreMisuratoTerzoPezzoQuattro)
            _db.AddInParameter(updateCommandTerzoPezzo, "ValoreMisuratoTerzoPezzoCinque", DbType.String, ValoreMisuratoTerzoPezzoCinque)
            _db.AddInParameter(updateCommandTerzoPezzo, "ValoreMisuratoTerzoPezzoSei", DbType.String, ValoreMisuratoTerzoPezzoSei)
            _db.AddInParameter(updateCommandTerzoPezzo, "ValoreMisuratoTerzoPezzoSette", DbType.String, ValoreMisuratoTerzoPezzoSette)
            _db.AddInParameter(updateCommandTerzoPezzo, "ValoreMisuratoTerzoPezzoOtto", DbType.String, ValoreMisuratoTerzoPezzoOtto)
            _db.AddInParameter(updateCommandTerzoPezzo, "ValoreMisuratoTerzoPezzoNove", DbType.String, ValoreMisuratoTerzoPezzoNove)
            _db.AddInParameter(updateCommandTerzoPezzo, "ValoreMisuratoTerzoPezzoDieci", DbType.String, ValoreMisuratoTerzoPezzoDieci)


            _db.AddInParameter(updateCommandTerzoPezzo, "TolleranzaPiuValorePrevistoTerzoUno", DbType.String, TolleranzaPiuValorePrevistoTerzoUno)
            _db.AddInParameter(updateCommandTerzoPezzo, "TolleranzaPiuValorePrevistoTerzoDue", DbType.String, TolleranzaPiuValorePrevistoTerzoDue)
            _db.AddInParameter(updateCommandTerzoPezzo, "TolleranzaPiuValorePrevistoTerzoTre", DbType.String, TolleranzaPiuValorePrevistoTerzoTre)
            _db.AddInParameter(updateCommandTerzoPezzo, "TolleranzaPiuValorePrevistoTerzoQuattro", DbType.String, TolleranzaPiuValorePrevistoTerzoQuattro)
            _db.AddInParameter(updateCommandTerzoPezzo, "TolleranzaPiuValorePrevistoTerzoCinque", DbType.String, TolleranzaPiuValorePrevistoTerzoCinque)
            _db.AddInParameter(updateCommandTerzoPezzo, "TolleranzaPiuValorePrevistoTerzoSei", DbType.String, TolleranzaPiuValorePrevistoTerzoSei)
            _db.AddInParameter(updateCommandTerzoPezzo, "TolleranzaPiuValorePrevistoTerzoSette", DbType.String, TolleranzaPiuValorePrevistoTerzoSette)
            _db.AddInParameter(updateCommandTerzoPezzo, "TolleranzaPiuValorePrevistoTerzoOtto", DbType.String, TolleranzaPiuValorePrevistoTerzoOtto)
            _db.AddInParameter(updateCommandTerzoPezzo, "TolleranzaPiuValorePrevistoTerzoNove", DbType.String, TolleranzaPiuValorePrevistoTerzoNove)
            _db.AddInParameter(updateCommandTerzoPezzo, "TolleranzaPiuValorePrevistoTerzoDieci", DbType.String, TolleranzaPiuValorePrevistoTerzoDieci)
            _db.AddInParameter(updateCommandTerzoPezzo, "TolleranzaMenoValorePrevistoTerzoUno", DbType.String, TolleranzaMenoValorePrevistoTerzoUno)
            _db.AddInParameter(updateCommandTerzoPezzo, "TolleranzaMenoValorePrevistoTerzoDue", DbType.String, TolleranzaMenoValorePrevistoTerzoDue)
            _db.AddInParameter(updateCommandTerzoPezzo, "TolleranzaMenoValorePrevistoTerzoTre", DbType.String, TolleranzaMenoValorePrevistoTerzoTre)
            _db.AddInParameter(updateCommandTerzoPezzo, "TolleranzaMenoValorePrevistoTerzoQuattro", DbType.String, TolleranzaMenoValorePrevistoTerzoQuattro)
            _db.AddInParameter(updateCommandTerzoPezzo, "TolleranzaMenoValorePrevistoTerzoCinque", DbType.String, TolleranzaMenoValorePrevistoTerzoCinque)
            _db.AddInParameter(updateCommandTerzoPezzo, "TolleranzaMenoValorePrevistoTerzoSei", DbType.String, TolleranzaMenoValorePrevistoTerzoSei)
            _db.AddInParameter(updateCommandTerzoPezzo, "TolleranzaMenoValorePrevistoTerzoSette", DbType.String, TolleranzaMenoValorePrevistoTerzoSette)
            _db.AddInParameter(updateCommandTerzoPezzo, "TolleranzaMenoValorePrevistoTerzoOtto", DbType.String, TolleranzaMenoValorePrevistoTerzoOtto)
            _db.AddInParameter(updateCommandTerzoPezzo, "TolleranzaMenoValorePrevistoTerzoNove", DbType.String, TolleranzaMenoValorePrevistoTerzoNove)
            _db.AddInParameter(updateCommandTerzoPezzo, "TolleranzaMenoValorePrevistoTerzoDieci", DbType.String, TolleranzaMenoValorePrevistoTerzoDieci)
            _db.AddInParameter(updateCommandTerzoPezzo, "NoteTerzoPezzo", DbType.String, NoteTerzoPezzo)

            rowsAffected = _db.ExecuteNonQuery(updateCommandTerzoPezzo)

        Catch ex As Exception
            MessageBox.Show("Errore AggiornaTerzoPezzo : " & ex.Message)
        End Try

        Return rowsAffected

    End Function


    Public Function AggiornaTerzoPezzoBusinessLayer(id_Intestazione As Integer) As Integer

        Dim ret As Integer?

        Try

            Dim IDPezziTerzoPezzo As Integer = id_Intestazione
            Dim IDIntestazione As Integer = id_Intestazione
            Dim ValorePrevistoTerzoUno As String = txtValPrev1Pz3.Text
            Dim ValorePrevistoTerzoDue As String = txtValPrev2Pz3.Text
            Dim ValorePrevistoTerzoTre As String = txtValPrev3Pz3.Text
            Dim ValorePrevistoTerzoQuattro As String = txtValPrev4Pz3.Text
            Dim ValorePrevistoTerzoCinque As String = txtValPrev5Pz3.Text
            Dim ValorePrevistoTerzoSei As String = txtValPrev6Pz3.Text
            Dim ValorePrevistoTerzoSette As String = txtValPrev7Pz3.Text
            Dim ValorePrevistoTerzoOtto As String = txtValPrev8Pz3.Text
            Dim ValorePrevistoTerzoNove As String = txtValPrev9Pz3.Text
            Dim ValorePrevistoTerzoDieci As String = txtValPrev10Pz3.Text

            Dim ValoreMisuratoTerzoUno As String = txt1ValMisPz3.Text
            Dim ValoreMisuratoTerzoDue As String = txt2ValMisPz3.Text
            Dim ValoreMisuratoTerzoTre As String = txt3ValMisPz3.Text
            Dim ValoreMisuratoTerzoQuattro As String = txt4ValMisPz3.Text
            Dim ValoreMisuratoTerzoCinque As String = txt5ValMisPz3.Text
            Dim ValoreMisuratoTerzoSei As String = txt6ValMisPz3.Text
            Dim ValoreMisuratoTerzoSette As String = txt7ValMisPz3.Text
            Dim ValoreMisuratoTerzoOtto As String = txt8ValMisPz3.Text
            Dim ValoreMisuratoTerzoNove As String = txt9ValMisPz3.Text
            Dim ValoreMisuratoTerzoDieci As String = txt10ValMisPz3.Text

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

            ret = AggiornaTerzoPezzo(IDPezziTerzoPezzo,
            IDIntestazione,
            ValorePrevistoTerzoUno,
            ValorePrevistoTerzoDue,
            ValorePrevistoTerzoTre,
            ValorePrevistoTerzoQuattro,
            ValorePrevistoTerzoCinque,
            ValorePrevistoTerzoSei,
            ValorePrevistoTerzoSette,
            ValorePrevistoTerzoOtto,
            ValorePrevistoTerzoNove,
            ValorePrevistoTerzoDieci,
            ValoreMisuratoTerzoUno,
            ValoreMisuratoTerzoDue,
            ValoreMisuratoTerzoTre,
            ValoreMisuratoTerzoQuattro,
            ValoreMisuratoTerzoCinque,
            ValoreMisuratoTerzoSei,
            ValoreMisuratoTerzoSette,
            ValoreMisuratoTerzoOtto,
            ValoreMisuratoTerzoNove,
            ValoreMisuratoTerzoDieci,
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

            If Not ret Is Nothing Then
                Return ret
            Else
                Exit Function
            End If

        Catch ex As Exception
            MessageBox.Show("Errore AggiornaTerzoPezzoBusinessLayer :" & ex.Message)
        End Try

    End Function




#End Region

#Region "Aggiorna Quarto Pezzo"

    Public Function checkIDQuartoPezzo(intIdItestazione As Integer) As Integer

        Dim rows As Integer
        Dim reader As DbDataReader
        Dim strSQL As String = "SELECT IDPezziQuartoPezzo, IDIntestazione " &
                               "FROM tblPezziQuartoPezzo " &
                               "WHERE (IDIntestazione = " & intIdItestazione & ")"



        Try
            Dim cmd As DbCommand = _db.GetSqlStringCommand(strSQL.ToString)


            Using datareader As IDataReader = _db.ExecuteReader(cmd)
                While datareader.Read
                    rows += 1
                End While
            End Using

        Catch ex As Exception
            MessageBox.Show("Errore checkIDQuartoPezzo :" & ex.Message)
        End Try

        Return rows

    End Function



    Public Function insertIDQuartoPezzo() As Integer
        Dim intContatoreQuartoPezzo As Integer = propIdIntestazione
        Dim result As DialogResult

        Dim IDPezziQuartoPezzo As Integer = intContatoreQuartoPezzo
        Dim IDIntestazione As Integer = intContatoreQuartoPezzo

        Try
            Dim retQuartoSoloID As Integer? = InsertPezziQuartoPezzoSoloID(IDPezziQuartoPezzo, IDIntestazione)

            If Not retQuartoSoloID Is Nothing Then
                Return retQuartoSoloID
            End If
        Catch ex As Exception
            MessageBox.Show("Errore insertIDQuartoPezzo :" & ex.Message)
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
            MessageBox.Show("Errore InsertPezziQuartoPezzoSoloID Query :" & ex.Message)
        End Try

        Return rowsAffectedQuartoPezzoSoloID


    End Function


    Public Function AggiornaQuartoPezzo(IdPezziQuartoPezzo As Integer,
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



        Dim rowsAffected As Integer
        Dim updateCommandQuartoPezzo As DbCommand = Nothing
        updateCommandQuartoPezzo = _db.GetStoredProcCommand("spUpdateQueryQuartoPezzo")


        Try
            _db.AddInParameter(updateCommandQuartoPezzo, "IdPezziQuartoPezzo", DbType.Int32, IdPezziQuartoPezzo)
            _db.AddInParameter(updateCommandQuartoPezzo, "IDIntestazione", DbType.Int32, IDIntestazione)
            _db.AddInParameter(updateCommandQuartoPezzo, "ValorePrevistoQuartoPezzoUno", DbType.String, ValorePrevistoQuartoPezzoUno)
            _db.AddInParameter(updateCommandQuartoPezzo, "ValorePrevistoQuartoPezzoDue", DbType.String, ValorePrevistoQuartoPezzoDue)
            _db.AddInParameter(updateCommandQuartoPezzo, "ValorePrevistoQuartoPezzoTre", DbType.String, ValorePrevistoQuartoPezzoTre)
            _db.AddInParameter(updateCommandQuartoPezzo, "ValorePrevistoQuartoPezzoQuattro", DbType.String, ValorePrevistoQuartoPezzoQuattro)
            _db.AddInParameter(updateCommandQuartoPezzo, "ValorePrevistoQuartoPezzoCinque", DbType.String, ValorePrevistoQuartoPezzoCinque)
            _db.AddInParameter(updateCommandQuartoPezzo, "ValorePrevistoQuartoPezzoSei", DbType.String, ValorePrevistoQuartoPezzoSei)
            _db.AddInParameter(updateCommandQuartoPezzo, "ValorePrevistoQuartoPezzoSette", DbType.String, ValorePrevistoQuartoPezzoSette)
            _db.AddInParameter(updateCommandQuartoPezzo, "ValorePrevistoQuartoPezzoOtto", DbType.String, ValorePrevistoQuartoPezzoOtto)
            _db.AddInParameter(updateCommandQuartoPezzo, "ValorePrevistoQuartoPezzoNove", DbType.String, ValorePrevistoQuartoPezzoNove)
            _db.AddInParameter(updateCommandQuartoPezzo, "ValorePrevistoQuartoPezzoDieci", DbType.String, ValorePrevistoQuartoPezzoDieci)

            _db.AddInParameter(updateCommandQuartoPezzo, "ValoreMisuratoQuartoPezzoUno", DbType.String, ValoreMisuratoQuartoPezzoUno)
            _db.AddInParameter(updateCommandQuartoPezzo, "ValoreMisuratoQuartoPezzoDue", DbType.String, ValoreMisuratoQuartoPezzoDue)
            _db.AddInParameter(updateCommandQuartoPezzo, "ValoreMisuratoQuartoPezzoTre", DbType.String, ValoreMisuratoQuartoPezzoTre)
            _db.AddInParameter(updateCommandQuartoPezzo, "ValoreMisuratoQuartoPezzoQuattro", DbType.String, ValoreMisuratoQuartoPezzoQuattro)
            _db.AddInParameter(updateCommandQuartoPezzo, "ValoreMisuratoQuartoPezzoCinque", DbType.String, ValoreMisuratoQuartoPezzoCinque)
            _db.AddInParameter(updateCommandQuartoPezzo, "ValoreMisuratoQuartoPezzoSei", DbType.String, ValoreMisuratoQuartoPezzoSei)
            _db.AddInParameter(updateCommandQuartoPezzo, "ValoreMisuratoQuartoPezzoSette", DbType.String, ValoreMisuratoQuartoPezzoSette)
            _db.AddInParameter(updateCommandQuartoPezzo, "ValoreMisuratoQuartoPezzoOtto", DbType.String, ValoreMisuratoQuartoPezzoOtto)
            _db.AddInParameter(updateCommandQuartoPezzo, "ValoreMisuratoQuartoPezzoNove", DbType.String, ValoreMisuratoQuartoPezzoNove)
            _db.AddInParameter(updateCommandQuartoPezzo, "ValoreMisuratoQuartoPezzoDieci", DbType.String, ValoreMisuratoQuartoPezzoDieci)

            _db.AddInParameter(updateCommandQuartoPezzo, "TolleranzaPiuValorePrevistoQuartoUno", DbType.String, TolleranzaPiuValorePrevistoQuartoUno)
            _db.AddInParameter(updateCommandQuartoPezzo, "TolleranzaPiuValorePrevistoQuartoDue", DbType.String, TolleranzaPiuValorePrevistoQuartoDue)
            _db.AddInParameter(updateCommandQuartoPezzo, "TolleranzaPiuValorePrevistoQuartoTre", DbType.String, TolleranzaPiuValorePrevistoQuartoTre)
            _db.AddInParameter(updateCommandQuartoPezzo, "TolleranzaPiuValorePrevistoQuartoQuattro", DbType.String, TolleranzaPiuValorePrevistoQuartoQuattro)
            _db.AddInParameter(updateCommandQuartoPezzo, "TolleranzaPiuValorePrevistoQuartoCinque", DbType.String, TolleranzaPiuValorePrevistoQuartoCinque)
            _db.AddInParameter(updateCommandQuartoPezzo, "TolleranzaPiuValorePrevistoQuartoSei", DbType.String, TolleranzaPiuValorePrevistoQuartoSei)
            _db.AddInParameter(updateCommandQuartoPezzo, "TolleranzaPiuValorePrevistoQuartoSette", DbType.String, TolleranzaPiuValorePrevistoQuartoSette)
            _db.AddInParameter(updateCommandQuartoPezzo, "TolleranzaPiuValorePrevistoQuartoOtto", DbType.String, TolleranzaPiuValorePrevistoQuartoOtto)
            _db.AddInParameter(updateCommandQuartoPezzo, "TolleranzaPiuValorePrevistoQuartoNove", DbType.String, TolleranzaPiuValorePrevistoQuartoNove)
            _db.AddInParameter(updateCommandQuartoPezzo, "TolleranzaPiuValorePrevistoQuartoDieci", DbType.String, TolleranzaPiuValorePrevistoQuartoDieci)

            _db.AddInParameter(updateCommandQuartoPezzo, "TolleranzaMenoValorePrevistoQuartoUno", DbType.String, TolleranzaMenoValorePrevistoQuartoUno)
            _db.AddInParameter(updateCommandQuartoPezzo, "TolleranzaMenoValorePrevistoQuartoDue", DbType.String, TolleranzaMenoValorePrevistoQuartoDue)
            _db.AddInParameter(updateCommandQuartoPezzo, "TolleranzaMenoValorePrevistoQuartoTre", DbType.String, TolleranzaMenoValorePrevistoQuartoTre)
            _db.AddInParameter(updateCommandQuartoPezzo, "TolleranzaMenoValorePrevistoQuartoQuattro", DbType.String, TolleranzaMenoValorePrevistoQuartoQuattro)
            _db.AddInParameter(updateCommandQuartoPezzo, "TolleranzaMenoValorePrevistoQuartoCinque", DbType.String, TolleranzaMenoValorePrevistoQuartoCinque)
            _db.AddInParameter(updateCommandQuartoPezzo, "TolleranzaMenoValorePrevistoQuartoSei", DbType.String, TolleranzaMenoValorePrevistoQuartoSei)
            _db.AddInParameter(updateCommandQuartoPezzo, "TolleranzaMenoValorePrevistoQuartoSette", DbType.String, TolleranzaMenoValorePrevistoQuartoSette)
            _db.AddInParameter(updateCommandQuartoPezzo, "TolleranzaMenoValorePrevistoQuartoOtto", DbType.String, TolleranzaMenoValorePrevistoQuartoOtto)
            _db.AddInParameter(updateCommandQuartoPezzo, "TolleranzaMenoValorePrevistoQuartoNove", DbType.String, TolleranzaMenoValorePrevistoQuartoNove)
            _db.AddInParameter(updateCommandQuartoPezzo, "TolleranzaMenoValorePrevistoQuartoDieci", DbType.String, TolleranzaMenoValorePrevistoQuartoDieci)
            _db.AddInParameter(updateCommandQuartoPezzo, "NoteQuartoPezzo", DbType.String, NoteQuartoPezzo)


            rowsAffected = _db.ExecuteNonQuery(updateCommandQuartoPezzo)

        Catch ex As Exception
            MessageBox.Show("Errore AggiornaQuartoPezzo : " & ex.Message)
        End Try

        Return rowsAffected

    End Function


    Public Function AggiornaQuartoPezzoBusinessLayer(id_Intestazione As Integer) As Integer

        Dim ret As Integer?

        Try

            Dim IDPezziQuartoPezzo As Integer = id_Intestazione
            Dim IDIntestazione As Integer = id_Intestazione
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

            ret = AggiornaQuartoPezzo(IDPezziQuartoPezzo,
            IDIntestazione,
            ValorePrevistoQuartoPezzoUno,
            ValorePrevistoQuartoPezzoDue,
            ValorePrevistoQuartoPezzoTre,
            ValorePrevistoQuartoPezzoQuattro,
            ValorePrevistoQuartoPezzoCinque,
            ValorePrevistoQuartoPezzoSei,
            ValorePrevistoQuartoPezzoSette,
            ValorePrevistoQuartoPezzoOtto,
            ValorePrevistoQuartoPezzoNove,
            ValorePrevistoQuartoPezzoDieci,
            ValoreMisuratoQuartoPezzoUno,
            ValoreMisuratoQuartoPezzoDue,
            ValoreMisuratoQuartoPezzoTre,
            ValoreMisuratoQuartoPezzoQuattro,
            ValoreMisuratoQuartoPezzoCinque,
            ValoreMisuratoQuartoPezzoSei,
            ValoreMisuratoQuartoPezzoSette,
            ValoreMisuratoQuartoPezzoOtto,
            ValoreMisuratoQuartoPezzoNove,
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

            If Not ret Is Nothing Then
                Return ret
            Else
                Exit Function
            End If


        Catch ex As Exception
            MessageBox.Show("Errore AggiornaQuartoPezzoBusinessLayer :" & ex.Message)
        End Try

    End Function



#End Region

#Region "Aggiorna Quinto Pezzo"

    Public Function checkIDQuintoPezzo(intIdItestazione As Integer) As Integer

        Dim rows As Integer
        Dim reader As DbDataReader
        Dim strSQL As String = "SELECT IDPezziQuintoPezzo, IDIntestazione " &
                               "FROM tblPezziQuintoPezzo " &
                               "WHERE (IDIntestazione = " & intIdItestazione & ")"



        Try
            Dim cmd As DbCommand = _db.GetSqlStringCommand(strSQL.ToString)


            Using datareader As IDataReader = _db.ExecuteReader(cmd)
                While datareader.Read
                    rows += 1
                End While
            End Using

        Catch ex As Exception
            MessageBox.Show("Errore checkIDQuintoPezzo :" & ex.Message)
        End Try

        Return rows

    End Function

    Public Function insertIDQuintoPezzo() As Integer
        Dim intContatoreQuintoPezzo As Integer = propIdIntestazione
        Dim result As DialogResult

        Dim IDPezziQuintoPezzo As Integer = intContatoreQuintoPezzo
        Dim IDIntestazione As Integer = intContatoreQuintoPezzo

        Try
            Dim retQuintoSoloID As Integer? = InsertPezziQuintoPezzoSoloID(IDPezziQuintoPezzo, IDIntestazione)

            If Not retQuintoSoloID Is Nothing Then
                Return retQuintoSoloID
            End If
        Catch ex As Exception
            MessageBox.Show("Errore insertIDQuintoPezzo :" & ex.Message)
        End Try
    End Function

    Public Function InsertPezziQuintoPezzoSoloID(IDPezziQuintoPezzo As Integer, IDIntestazione As Integer) As Integer


        Dim insertCommandQuintoPezzoSoloID As DbCommand = Nothing
        Dim rowsAffectedQuintoPezzoSoloID As Integer = Nothing

        Dim strQuery As String = "INSERT INTO [dbo].[tblPezziQuintoPezzo] ([IDPezziQuintoPezzo],[IDIntestazione]) VALUES (@IDPezziQuintoPezzo,@IDIntestazione)"

        Try
            insertCommandQuintoPezzoSoloID = _db.GetSqlStringCommand(strQuery)


            _db.AddInParameter(insertCommandQuintoPezzoSoloID, "IDPezziQuintoPezzo", DbType.Int32, IDPezziQuintoPezzo)
            _db.AddInParameter(insertCommandQuintoPezzoSoloID, "IDIntestazione", DbType.Int32, IDIntestazione)

            rowsAffectedQuintoPezzoSoloID = _db.ExecuteNonQuery(insertCommandQuintoPezzoSoloID)

        Catch ex As Exception
            MessageBox.Show("Errore InsertPezziQuintoPezzoSoloID Query :" & ex.Message)
        End Try

        Return rowsAffectedQuintoPezzoSoloID


    End Function


    Public Function AggiornaQuintoPezzo(IdPezziQuintoPezzo As Integer,
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



        Dim rowsAffected As Integer
        Dim updateCommandQuintoPezzo As DbCommand = Nothing
        updateCommandQuintoPezzo = _db.GetStoredProcCommand("spUpdateQueryQuintoPezzo")

        Try

            _db.AddInParameter(updateCommandQuintoPezzo, "IdPezziQuintoPezzo", DbType.Int32, IdPezziQuintoPezzo)
            _db.AddInParameter(updateCommandQuintoPezzo, "IDIntestazione", DbType.Int32, IDIntestazione)
            _db.AddInParameter(updateCommandQuintoPezzo, "ValorePrevistoQuintoPezzoUno", DbType.String, ValorePrevistoQuintoPezzoUno)
            _db.AddInParameter(updateCommandQuintoPezzo, "ValorePrevistoQuintoPezzoDue", DbType.String, ValorePrevistoQuintoPezzoDue)
            _db.AddInParameter(updateCommandQuintoPezzo, "ValorePrevistoQuintoPezzoTre", DbType.String, ValorePrevistoQuintoPezzoTre)
            _db.AddInParameter(updateCommandQuintoPezzo, "ValorePrevistoQuintoPezzoQuattro", DbType.String, ValorePrevistoQuintoPezzoQuattro)
            _db.AddInParameter(updateCommandQuintoPezzo, "ValorePrevistoQuintoPezzoCinque", DbType.String, ValorePrevistoQuintoPezzoCinque)
            _db.AddInParameter(updateCommandQuintoPezzo, "ValorePrevistoQuintoPezzoSei", DbType.String, ValorePrevistoQuintoPezzoSei)
            _db.AddInParameter(updateCommandQuintoPezzo, "ValorePrevistoQuintoPezzoSette", DbType.String, ValorePrevistoQuintoPezzoSette)
            _db.AddInParameter(updateCommandQuintoPezzo, "ValorePrevistoQuintoPezzoOtto", DbType.String, ValorePrevistoQuintoPezzoOtto)
            _db.AddInParameter(updateCommandQuintoPezzo, "ValorePrevistoQuintoPezzoNove", DbType.String, ValorePrevistoQuintoPezzoNove)
            _db.AddInParameter(updateCommandQuintoPezzo, "ValorePrevistoQuintoPezzoDieci", DbType.String, ValorePrevistoQuintoPezzoDieci)
            _db.AddInParameter(updateCommandQuintoPezzo, "ValoreMisuratoQuintoPezzoUno", DbType.String, ValoreMisuratoQuintoPezzoUno)
            _db.AddInParameter(updateCommandQuintoPezzo, "ValoreMisuratoQuintoPezzoDue", DbType.String, ValoreMisuratoQuintoPezzoDue)
            _db.AddInParameter(updateCommandQuintoPezzo, "ValoreMisuratoQuintoPezzoTre", DbType.String, ValoreMisuratoQuintoPezzoTre)
            _db.AddInParameter(updateCommandQuintoPezzo, "ValoreMisuratoQuintoPezzoQuattro", DbType.String, ValoreMisuratoQuintoPezzoQuattro)
            _db.AddInParameter(updateCommandQuintoPezzo, "ValoreMisuratoQuintoPezzoCinque", DbType.String, ValoreMisuratoQuintoPezzoCinque)
            _db.AddInParameter(updateCommandQuintoPezzo, "ValoreMisuratoQuintoPezzoSei", DbType.String, ValoreMisuratoQuintoPezzoSei)
            _db.AddInParameter(updateCommandQuintoPezzo, "ValoreMisuratoQuintoPezzoSette", DbType.String, ValoreMisuratoQuintoPezzoSette)
            _db.AddInParameter(updateCommandQuintoPezzo, "ValoreMisuratoQuintoPezzoOtto", DbType.String, ValoreMisuratoQuintoPezzoOtto)
            _db.AddInParameter(updateCommandQuintoPezzo, "ValoreMisuratoQuintoPezzoNove", DbType.String, ValoreMisuratoQuintoPezzoNove)
            _db.AddInParameter(updateCommandQuintoPezzo, "ValoreMisuratoQuintoPezzoDieci", DbType.String, ValoreMisuratoQuintoPezzoDieci)


            _db.AddInParameter(updateCommandQuintoPezzo, "TolleranzaPiuValorePrevistoQuintoUno", DbType.String, TolleranzaPiuValorePrevistoQuintoUno)
            _db.AddInParameter(updateCommandQuintoPezzo, "TolleranzaPiuValorePrevistoQuintoDue", DbType.String, TolleranzaPiuValorePrevistoQuintoDue)
            _db.AddInParameter(updateCommandQuintoPezzo, "TolleranzaPiuValorePrevistoQuintoTre", DbType.String, TolleranzaPiuValorePrevistoQuintoTre)
            _db.AddInParameter(updateCommandQuintoPezzo, "TolleranzaPiuValorePrevistoQuintoQuattro", DbType.String, TolleranzaPiuValorePrevistoQuintoQuattro)
            _db.AddInParameter(updateCommandQuintoPezzo, "TolleranzaPiuValorePrevistoQuintoCinque", DbType.String, TolleranzaPiuValorePrevistoQuintoCinque)
            _db.AddInParameter(updateCommandQuintoPezzo, "TolleranzaPiuValorePrevistoQuintoSei", DbType.String, TolleranzaPiuValorePrevistoQuintoSei)
            _db.AddInParameter(updateCommandQuintoPezzo, "TolleranzaPiuValorePrevistoQuintoSette", DbType.String, TolleranzaPiuValorePrevistoQuintoSette)
            _db.AddInParameter(updateCommandQuintoPezzo, "TolleranzaPiuValorePrevistoQuintoOtto", DbType.String, TolleranzaPiuValorePrevistoQuintoOtto)
            _db.AddInParameter(updateCommandQuintoPezzo, "TolleranzaPiuValorePrevistoQuintoNove", DbType.String, TolleranzaPiuValorePrevistoQuintoNove)
            _db.AddInParameter(updateCommandQuintoPezzo, "TolleranzaPiuValorePrevistoQuintoDieci", DbType.String, TolleranzaPiuValorePrevistoQuintoDieci)
            _db.AddInParameter(updateCommandQuintoPezzo, "TolleranzaMenoValorePrevistoQuintoUno", DbType.String, TolleranzaMenoValorePrevistoQuintoUno)
            _db.AddInParameter(updateCommandQuintoPezzo, "TolleranzaMenoValorePrevistoQuintoDue", DbType.String, TolleranzaMenoValorePrevistoQuintoDue)
            _db.AddInParameter(updateCommandQuintoPezzo, "TolleranzaMenoValorePrevistoQuintoTre", DbType.String, TolleranzaMenoValorePrevistoQuintoTre)
            _db.AddInParameter(updateCommandQuintoPezzo, "TolleranzaMenoValorePrevistoQuintoQuattro", DbType.String, TolleranzaMenoValorePrevistoQuintoQuattro)
            _db.AddInParameter(updateCommandQuintoPezzo, "TolleranzaMenoValorePrevistoQuintoCinque", DbType.String, TolleranzaMenoValorePrevistoQuintoCinque)
            _db.AddInParameter(updateCommandQuintoPezzo, "TolleranzaMenoValorePrevistoQuintoSei", DbType.String, TolleranzaMenoValorePrevistoQuintoSei)
            _db.AddInParameter(updateCommandQuintoPezzo, "TolleranzaMenoValorePrevistoQuintoSette", DbType.String, TolleranzaMenoValorePrevistoQuintoSette)
            _db.AddInParameter(updateCommandQuintoPezzo, "TolleranzaMenoValorePrevistoQuintoOtto", DbType.String, TolleranzaMenoValorePrevistoQuintoOtto)
            _db.AddInParameter(updateCommandQuintoPezzo, "TolleranzaMenoValorePrevistoQuintoNove", DbType.String, TolleranzaMenoValorePrevistoQuintoNove)
            _db.AddInParameter(updateCommandQuintoPezzo, "TolleranzaMenoValorePrevistoQuintoDieci", DbType.String, TolleranzaMenoValorePrevistoQuintoDieci)
            _db.AddInParameter(updateCommandQuintoPezzo, "NoteQuintoPezzo", DbType.String, NoteQuintoPezzo)




            rowsAffected = _db.ExecuteNonQuery(updateCommandQuintoPezzo)


        Catch ex As Exception
            MessageBox.Show("Errore AggiornaQuintoPezzo : " & ex.Message)
        End Try

        Return rowsAffected

    End Function

    Public Function AggiornaQuintoPezzoBusinessLayer(id_Intestazione As Integer) As Integer

        Dim ret As Integer?

        Try

            Dim IDPezziQuintoPezzo As Integer = id_Intestazione
            Dim IDIntestazione = id_Intestazione
            Dim ValorePrevistoQuintoUno As String = txtValPrev1Pz5.Text
            Dim ValorePrevistoQuintoDue As String = txtValPrev2Pz5.Text
            Dim ValorePrevistoQuintoTre As String = txtValPrev3Pz5.Text
            Dim ValorePrevistoQuintoQuattro As String = txtValPrev4Pz5.Text
            Dim ValorePrevistoQuintoCinque As String = txtValPrev5Pz5.Text
            Dim ValorePrevistoQuintoSei As String = txtValPrev6Pz5.Text
            Dim ValorePrevistoQuintoSette As String = txtValPrev7Pz5.Text
            Dim ValorePrevistoQuintoOtto As String = txtValPrev8Pz5.Text
            Dim ValorePrevistoQuintoNove As String = txtValPrev9Pz5.Text
            Dim ValorePrevistoQuintoDieci As String = txtValPrev10Pz5.Text

            Dim ValoreMisuratoQuintoUno As String = txt1ValMisPz5.Text
            Dim ValoreMisuratoQuintoDue As String = txt2ValMisPz5.Text
            Dim ValoreMisuratoQuintoTre As String = txt3ValMisPz5.Text
            Dim ValoreMisuratoQuintoQuattro As String = txt4ValMisPz5.Text
            Dim ValoreMisuratoQuintoCinque As String = txt5ValMisPz5.Text
            Dim ValoreMisuratoQuintoSei As String = txt6ValMisPz5.Text
            Dim ValoreMisuratoQuintoSette As String = txt7ValMisPz5.Text
            Dim ValoreMisuratoQuintoOtto As String = txt8ValMisPz5.Text
            Dim ValoreMisuratoQuintoNove As String = txt9ValMisPz5.Text
            Dim ValoreMisuratoQuintoDieci As String = txt10ValMisPz5.Text

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

            ret = AggiornaQuintoPezzo(IDPezziQuintoPezzo,
            IDIntestazione,
            ValorePrevistoQuintoUno,
            ValorePrevistoQuintoDue,
            ValorePrevistoQuintoTre,
            ValorePrevistoQuintoQuattro,
            ValorePrevistoQuintoCinque,
            ValorePrevistoQuintoSei,
            ValorePrevistoQuintoSette,
            ValorePrevistoQuintoOtto,
            ValorePrevistoQuintoNove,
            ValorePrevistoQuintoDieci,
            ValoreMisuratoQuintoUno,
            ValoreMisuratoQuintoDue,
            ValoreMisuratoQuintoTre,
            ValoreMisuratoQuintoQuattro,
            ValoreMisuratoQuintoCinque,
            ValoreMisuratoQuintoSei,
            ValoreMisuratoQuintoSette,
            ValoreMisuratoQuintoOtto,
            ValoreMisuratoQuintoNove,
            ValoreMisuratoQuintoDieci,
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

            If Not ret Is Nothing Then
                Return ret
            Else
                Exit Function
            End If

        Catch ex As Exception
            MessageBox.Show("Errore AggiornaQuintoPezzoBusinessLayer :" & ex.Message)
        End Try

    End Function


#End Region

#Region "Aggiorna Ultimo Pezzo"

    Public Function checkIDUltimoPezzo(intIdItestazione As Integer) As Integer

        Dim rows As Integer
        Dim reader As DbDataReader
        Dim strSQL As String = "SELECT IDPezziUltimoPezzo, IDIntestazione " &
                               "FROM tblPezziUltimoPezzo " &
                               "WHERE (IDIntestazione = " & intIdItestazione & ")"



        Try
            Dim cmd As DbCommand = _db.GetSqlStringCommand(strSQL.ToString)


            Using datareader As IDataReader = _db.ExecuteReader(cmd)
                While datareader.Read
                    rows += 1
                End While
            End Using

        Catch ex As Exception
            MessageBox.Show("Errore checkIDUltimoPezzo :" & ex.Message)
        End Try

        Return rows

    End Function

    Public Function insertIDUltimoPezzo() As Integer
        Dim intContatoreUltimoPezzo As Integer = propIdIntestazione
        Dim result As DialogResult

        Dim IDPezziUltimoPezzo As Integer = intContatoreUltimoPezzo
        Dim IDIntestazione As Integer = intContatoreUltimoPezzo

        Try
            Dim retUltimoSoloID As Integer? = InsertPezziUtimoPezzoSoloID(IDPezziUltimoPezzo, IDIntestazione)

            If Not retUltimoSoloID Is Nothing Then
                Return retUltimoSoloID
            End If
        Catch ex As Exception
            MessageBox.Show("Errore insertIDUltimoPezzo :" & ex.Message)
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
            MessageBox.Show("Errore InsertPezziUtimoPezzoSoloID Query :" & ex.Message)
        End Try

        Return rowsAffectedUltimoPezzoSoloID


    End Function




    Public Function AggiornaUltimoPezzo(IDPezziUltimoPezzo As Integer,
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



        Dim rowsAffected As Integer
        Dim updateCommandUltimoPezzo As DbCommand = Nothing
        updateCommandUltimoPezzo = _db.GetStoredProcCommand("spUpdateQueryUltimoPezzo")

        Try
            _db.AddInParameter(updateCommandUltimoPezzo, "IDPezziUltimoPezzo", DbType.Int32, IDPezziUltimoPezzo)
            _db.AddInParameter(updateCommandUltimoPezzo, "IDIntestazione", DbType.Int32, IDIntestazione)
            _db.AddInParameter(updateCommandUltimoPezzo, "ValorePrevistoUltimoPezzoUno", DbType.String, ValorePrevistoUltimoPezzoUno)
            _db.AddInParameter(updateCommandUltimoPezzo, "ValorePrevistoUltimoPezzoDue", DbType.String, ValorePrevistoUltimoPezzoDue)
            _db.AddInParameter(updateCommandUltimoPezzo, "ValorePrevistoUltimoPezzoTre", DbType.String, ValorePrevistoUltimoPezzoTre)
            _db.AddInParameter(updateCommandUltimoPezzo, "ValorePrevistoUltimoPezzoQuattro", DbType.String, ValorePrevistoUltimoPezzoQuattro)
            _db.AddInParameter(updateCommandUltimoPezzo, "ValorePrevistoUltimoPezzoCinque", DbType.String, ValorePrevistoUltimoPezzoCinque)
            _db.AddInParameter(updateCommandUltimoPezzo, "ValorePrevistoUltimoPezzoSei", DbType.String, ValorePrevistoUltimoPezzoSei)
            _db.AddInParameter(updateCommandUltimoPezzo, "ValorePrevistoUltimoPezzoSette", DbType.String, ValorePrevistoUltimoPezzoSette)
            _db.AddInParameter(updateCommandUltimoPezzo, "ValorePrevistoUltimoPezzoOtto", DbType.String, ValorePrevistoUltimoPezzoOtto)
            _db.AddInParameter(updateCommandUltimoPezzo, "ValorePrevistoUltimoPezzoNove", DbType.String, ValorePrevistoUltimoPezzoNove)
            _db.AddInParameter(updateCommandUltimoPezzo, "ValorePrevistoUltimoPezzoDieci", DbType.String, ValorePrevistoUltimoPezzoDieci)
            _db.AddInParameter(updateCommandUltimoPezzo, "ValoreMisuratoUltimoPezzoUno", DbType.String, ValoreMisuratoUltimoPezzoUno)
            _db.AddInParameter(updateCommandUltimoPezzo, "ValoreMisuratoUltimoPezzoDue", DbType.String, ValoreMisuratoUltimoPezzoDue)
            _db.AddInParameter(updateCommandUltimoPezzo, "ValoreMisuratoUltimoPezzoTre", DbType.String, ValoreMisuratoUltimoPezzoTre)
            _db.AddInParameter(updateCommandUltimoPezzo, "ValoreMisuratoUltimoPezzoQuattro", DbType.String, ValoreMisuratoUltimoPezzoQuattro)
            _db.AddInParameter(updateCommandUltimoPezzo, "ValoreMisuratoUltimoPezzoCinque", DbType.String, ValoreMisuratoUltimoPezzoCinque)
            _db.AddInParameter(updateCommandUltimoPezzo, "ValoreMisuratoUltimoPezzoSei", DbType.String, ValoreMisuratoUltimoPezzoSei)
            _db.AddInParameter(updateCommandUltimoPezzo, "ValoreMisuratoUltimoPezzoSette", DbType.String, ValoreMisuratoUltimoPezzoSette)
            _db.AddInParameter(updateCommandUltimoPezzo, "ValoreMisuratoUltimoPezzoOtto", DbType.String, ValoreMisuratoUltimoPezzoOtto)
            _db.AddInParameter(updateCommandUltimoPezzo, "ValoreMisuratoUltimoPezzoNove", DbType.String, ValoreMisuratoUltimoPezzoNove)
            _db.AddInParameter(updateCommandUltimoPezzo, "ValoreMisuratoUltimoPezzoDieci", DbType.String, ValoreMisuratoUltimoPezzoDieci)

            _db.AddInParameter(updateCommandUltimoPezzo, "TolleranzaPiuValorePrevistoUltimoUno", DbType.String, TolleranzaPiuValorePrevistoUltimoUno)
            _db.AddInParameter(updateCommandUltimoPezzo, "TolleranzaPiuValorePrevistoUltimoDue", DbType.String, TolleranzaPiuValorePrevistoUltimoDue)
            _db.AddInParameter(updateCommandUltimoPezzo, "TolleranzaPiuValorePrevistoUltimoTre", DbType.String, TolleranzaPiuValorePrevistoUltimoTre)
            _db.AddInParameter(updateCommandUltimoPezzo, "TolleranzaPiuValorePrevistoUltimoQuattro", DbType.String, TolleranzaPiuValorePrevistoUltimoQuattro)
            _db.AddInParameter(updateCommandUltimoPezzo, "TolleranzaPiuValorePrevistoUltimoCinque", DbType.String, TolleranzaPiuValorePrevistoUltimoCinque)
            _db.AddInParameter(updateCommandUltimoPezzo, "TolleranzaPiuValorePrevistoUltimoSei", DbType.String, TolleranzaPiuValorePrevistoUltimoSei)
            _db.AddInParameter(updateCommandUltimoPezzo, "TolleranzaPiuValorePrevistoUltimoSette", DbType.String, TolleranzaPiuValorePrevistoUltimoSette)
            _db.AddInParameter(updateCommandUltimoPezzo, "TolleranzaPiuValorePrevistoUltimoOtto", DbType.String, TolleranzaPiuValorePrevistoUltimoOtto)
            _db.AddInParameter(updateCommandUltimoPezzo, "TolleranzaPiuValorePrevistoUltimoNove", DbType.String, TolleranzaPiuValorePrevistoUltimoNove)
            _db.AddInParameter(updateCommandUltimoPezzo, "TolleranzaPiuValorePrevistoUltimoDieci", DbType.String, TolleranzaPiuValorePrevistoUltimoDieci)
            _db.AddInParameter(updateCommandUltimoPezzo, "TolleranzaMenoValorePrevistoUltimoUno", DbType.String, TolleranzaMenoValorePrevistoUltimoUno)
            _db.AddInParameter(updateCommandUltimoPezzo, "TolleranzaMenoValorePrevistoUltimoDue", DbType.String, TolleranzaMenoValorePrevistoUltimoDue)
            _db.AddInParameter(updateCommandUltimoPezzo, "TolleranzaMenoValorePrevistoUltimoTre", DbType.String, TolleranzaMenoValorePrevistoUltimoTre)
            _db.AddInParameter(updateCommandUltimoPezzo, "TolleranzaMenoValorePrevistoUltimoQuattro", DbType.String, TolleranzaMenoValorePrevistoUltimoQuattro)
            _db.AddInParameter(updateCommandUltimoPezzo, "TolleranzaMenoValorePrevistoUltimoCinque", DbType.String, TolleranzaMenoValorePrevistoUltimoCinque)
            _db.AddInParameter(updateCommandUltimoPezzo, "TolleranzaMenoValorePrevistoUltimoSei", DbType.String, TolleranzaMenoValorePrevistoUltimoSei)
            _db.AddInParameter(updateCommandUltimoPezzo, "TolleranzaMenoValorePrevistoUltimoSette", DbType.String, TolleranzaMenoValorePrevistoUltimoSette)
            _db.AddInParameter(updateCommandUltimoPezzo, "TolleranzaMenoValorePrevistoUltimoOtto", DbType.String, TolleranzaMenoValorePrevistoUltimoOtto)
            _db.AddInParameter(updateCommandUltimoPezzo, "TolleranzaMenoValorePrevistoUltimoNove", DbType.String, TolleranzaMenoValorePrevistoUltimoNove)
            _db.AddInParameter(updateCommandUltimoPezzo, "TolleranzaMenoValorePrevistoUltimoDieci", DbType.String, TolleranzaMenoValorePrevistoUltimoDieci)
            _db.AddInParameter(updateCommandUltimoPezzo, "NoteUltimoPezzo", DbType.String, NoteUltimoPezzo)



            rowsAffected = _db.ExecuteNonQuery(updateCommandUltimoPezzo)


        Catch ex As Exception
            MessageBox.Show("Errore AggiornaUltimoPezzo : " & ex.Message)
        End Try

        Return rowsAffected

    End Function

    Public Function AggiornaUltimoPezzoBusinessLayer(id_Intestazione As Integer)

        Dim ret As Integer?

        Try

            Dim IDPezziUltimoPezzo As Integer = id_Intestazione
            Dim IDIntestazione = id_Intestazione
            Dim ValorePrevistoUltimoUno As String = txtValPrev1UltimoPz.Text
            Dim ValorePrevistoUltimoDue As String = txtValPrev2UltimoPz.Text
            Dim ValorePrevistoUltimoTre As String = txtValPrev3UltimoPz.Text
            Dim ValorePrevistoUltimoQuattro As String = txtValPrev4UltimoPz.Text
            Dim ValorePrevistoUltimoCinque As String = txtValPrev5UltimoPz.Text
            Dim ValorePrevistoUltimoSei As String = txtValPrev6UltimoPz.Text
            Dim ValorePrevistoUltimoSette As String = txtValPrev7UltimoPz.Text
            Dim ValorePrevistoUltimoOtto As String = txtValPrev8UltimoPz.Text
            Dim ValorePrevistoUltimoNove As String = txtValPrev9UltimoPz.Text
            Dim ValorePrevistoUltimoDieci As String = txtValPrev10UltimoPz.Text

            Dim ValoreMisuratoUltimoUno As String = txt1ValMisUltimoPz.Text
            Dim ValoreMisuratoUltimoDue As String = txt2ValMisUltimoPz.Text
            Dim ValoreMisuratoUltimoTre As String = txt3ValMisUltimoPz.Text
            Dim ValoreMisuratoUltimoQuattro As String = txt4ValMisUltimoPz.Text
            Dim ValoreMisuratoUltimoCinque As String = txt5ValMisUltimoPz.Text
            Dim ValoreMisuratoUltimoSei As String = txt6ValMisUltimoPz.Text
            Dim ValoreMisuratoUltimoSette As String = txt7ValMisUltimoPz.Text
            Dim ValoreMisuratoUltimoOtto As String = txt8ValMisUltimoPz.Text
            Dim ValoreMisuratoUltimoNove As String = txt9ValMisUltimoPz.Text
            Dim ValoreMisuratoUltimoDieci As String = txt10ValMisUltimoPz.Text

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

            ret = AggiornaUltimoPezzo(IDPezziUltimoPezzo, IDIntestazione, ValorePrevistoUltimoUno,
            ValorePrevistoUltimoDue,
            ValorePrevistoUltimoTre,
            ValorePrevistoUltimoQuattro,
            ValorePrevistoUltimoCinque,
            ValorePrevistoUltimoSei,
            ValorePrevistoUltimoSette,
            ValorePrevistoUltimoOtto,
            ValorePrevistoUltimoNove,
            ValorePrevistoUltimoDieci,
            ValoreMisuratoUltimoUno,
            ValoreMisuratoUltimoDue,
            ValoreMisuratoUltimoTre,
            ValoreMisuratoUltimoQuattro,
            ValoreMisuratoUltimoCinque,
            ValoreMisuratoUltimoSei,
            ValoreMisuratoUltimoSette,
            ValoreMisuratoUltimoOtto,
            ValoreMisuratoUltimoNove,
            ValoreMisuratoUltimoDieci,
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

            If Not ret Is Nothing Then
                Return ret
            Else
                Exit Function
            End If

        Catch ex As Exception
            MessageBox.Show("Errore AggiornaUltimoPezzoBusinessLayer :" & ex.Message)
        End Try

    End Function


#End Region


End Class

#Region "Codice Vecchio"



'Public Sub clearItemMultiColumnComboBox()
'    Dim rows As New List(Of GridViewRowInfo)(Me.cmbLottoNum.EditorControl.Rows)

'    For Each rowInfo As GridViewRowInfo In rows
'        rowInfo.Delete()
'    Next
'End Sub




''Riempie il combo del Lotto sul Load della form
'Public Sub riempiComboLotto()
'    Try
'        Dim strSQL As String = "SELECT materiale, numeroLotto, fornitore, numDDT FROM tblNumeroLottoMateriale ORDER BY fornitore"
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
'Private Sub cmbMateriale_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMateriale.SelectedIndexChanged
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

'        If InStr(materiale, "Anticorodal") > 0 Then
'            cmbLottoNum.SelectedIndex = -1
'            Dim strSQL As String = "spGetLottoAnticorodalByMateriale"
'            cmd = _db.GetStoredProcCommand(strSQL)
'            _db.AddInParameter(cmd, "materiale", DbType.String, "Anticorodal")
'            Using datareader As IDataReader = _db.ExecuteReader(cmd)
'                While datareader.Read
'                    Me.cmbLottoNum.Items.Add(datareader("fornitore") + " - " + datareader("numeroLotto"))
'                End While
'            End Using
'        End If

'        If InStr(materiale, "Anticorodal") = 0 Then
'            cmbLottoNum.SelectedIndex = -1
'            Dim strSQL As String = "spGetMaterialePerLottoDiversoDaAnticorodal"
'            cmd = _db.GetStoredProcCommand(strSQL)
'            Using datareader As IDataReader = _db.ExecuteReader(cmd)
'                While datareader.Read
'                    Me.cmbLottoNum.Items.Add(datareader("fornitore") + " - " + datareader("numeroLotto"))
'                End While
'            End Using
'        End If

'    Catch ex As Exception
'        MessageBox.Show("Errore selezione materiale per Lotto : " & ex.Message)
'    End Try
'End Sub


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

#End Region