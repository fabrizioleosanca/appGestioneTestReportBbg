Imports System.ComponentModel
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.IO
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data

Public Class frmRegistraTestReportTre

#Region "Proprieta"
    Public Shared factory As New DatabaseProviderFactory()
    Public Property _db As Database = factory.Create("dbConnStrRete")
    Public Property TestiRiga As List(Of String)
    Public Property rowIndex As Integer = 0
    Public Property rigaOrdNum As Integer?

    Public Property emptyRow As DataRow
#End Region

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

    Private Sub DropDownClosing(sender As Object, args As Telerik.WinControls.UI.RadPopupClosingEventArgs)
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


    Private Sub OnTextBoxItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        cmbLottoNum.MultiColumnComboBoxElement.ShowPopup()
    End Sub


    Private Sub frmRegistraTestReportTre_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            RiempiComboMateriali()

            cmbLottoNum.SelectedIndex = -1
            cmbMateriale.SelectedIndex = -1

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
            RiempiComboFornitori()
            RiempiComboMacchine()

        Catch ex As Exception
            MessageBox.Show("Errore: frmRegistraTestReportTre_Load  " & ex.Message)
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


#Region "Riempi Combo Lotto Fornitore Strumenti e Materiali apertura form"

    Private Sub cmbFornitore_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFornitore.SelectedIndexChanged
        Dim str As String
        Dim strArr() As String
        Dim count As Integer
        Dim Fornitore As String
        Dim selFornitore As String = CType(cmbFornitore.SelectedItem, String)
        RiempiComboLottoByFornitore(selFornitore)
    End Sub

    Public Sub clearItemMultiColumnComboBox()
        Dim rows As New List(Of GridViewRowInfo)(cmbLottoNum.EditorControl.Rows)

        For Each rowInfo As GridViewRowInfo In rows
            rowInfo.Delete()
        Next
    End Sub




    Public Sub RiempiComboLottoByFornitore(ByVal strFornitore As String)

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



#End Region

#Region "Eventi"

    Private Sub btnChiudiAggiungiReport_Click(sender As Object, e As EventArgs) Handles btnChiudiAggiungiReport.Click
        Close()
    End Sub

    Private Sub btnSalvaTestReport_Click(sender As Object, e As EventArgs) Handles btnSalvaTestReport.Click

        Dim result As DialogResult
        Dim intSecondoPezzo As Nullable(Of Integer)
        Dim intTerzoPezzo As Nullable(Of Integer)
        Dim intQuartoPezzo As Nullable(Of Integer)
        Dim intQuintoPezzo As Nullable(Of Integer)
        Dim intUltimoPezzo As Nullable(Of Integer)
        Dim intSalvaTestataTestReportBusLayer As Nullable(Of Integer)
        Dim intPrimoPezzo As Nullable(Of Integer)

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

#End Region

#Region "Inserisci in Database Testata Report"

    Public Function SalvaTestataTestReportBusLayer() As Integer

        Dim retTestata As Nullable(Of Integer)
        Dim intContatore As Integer = contatore()
        propIdIntestazione = intContatore
        ' Dim result As DialogResult
        Dim materiale As String
        Dim strumento As String
        Dim macchinaNum As String
        Dim fornitore As String
        Dim operatore As String
        Dim pezzoNumero As Nullable(Of Integer)
        Dim numPezzi As Nullable(Of Integer)

        Try

            materiale = cmbMateriale.Text
            strumento = cmbStrumento.Text
            macchinaNum = cmbMacchNum.Text
            fornitore = cmbFornitore.Text
            operatore = cmbOperatore.Text

            Dim iDIntestazione As Integer = intContatore
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

            'If Me.txtRigaNum.Text = Nothing Then
            '    rigaOrdNum = 0
            'Else
            '    rigaOrdNum = CType(Me.txtRigaNum.Text, Integer)
            'End If

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

    Private Function IsInputNumeric(input As String) As Boolean
        If String.IsNullOrEmpty(input) Then Return True
        If Not IsNumeric(input) Then Return False
        If IsNumeric(input) Then Return True
        'Dim parts() As String = input.Split("/"c)
        'If parts.Length <> 2 Then Return False
        'Return IsNumeric(parts(0)) AndAlso IsNumeric(parts(1))
    End Function

    Private Sub txtRigaNum_Validating(sender As Object, e As CancelEventArgs) Handles txtRigaNum.Validating
        If IsInputNumeric(txtRigaNum.Text) = True Then
            If txtRigaNum.TextLength = 0 Then
                rigaOrdNum = 0
            Else
                rigaOrdNum = CType(txtRigaNum.Text, Integer)
            End If
        Else
            rigaOrdNum = 0
            MessageBox.Show("Per favore usare valori numerici per il numero riga o lasciarlo vuoto")
        End If
    End Sub

    Public Function insertTestataReport(ByVal idIntestazione As Integer, ByVal numOrdine As String,
                            ByVal codArticolo As String, ByVal data As Date, ByVal materiale As String,
                            ByVal strumento As String,
                            ByVal macchinanum As String, ByVal rigaordnum As Integer,
                            ByVal numpezzi As Integer, ByVal fornitore As String, ByVal numlotto As String,
                            ByVal primopezzo As Boolean, ByVal ultimopezzo As Boolean,
                            ByVal pezzonumero As Integer, ByVal operatore As String) As Integer

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

        'Dim imageAsBytes As Byte()

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

#Region "Inserisci in Database Primo Pezzo"

    Public Function SalvaPrimoPezzo() As Integer

        Try
            Dim intContatorePU As Integer = propIdIntestazione


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
            End If

        Catch ex As Exception
            MessageBox.Show("Errore SalvaPrimoPezzo :" & ex.Message)
        End Try

    End Function


    Public Function InsertPezziPrimoPezzo(ByVal IDPezziPrimoPezzo As Integer, ByVal IDIntestazione As Integer, ByVal ValorePrevistoPrimoPezzoUno As String,
                            ByVal ValorePrevistoPrimoPezzoDue As String, ByVal ValorePrevistoPrimoPezzoTre As String,
                            ByVal ValorePrevistoPrimoPezzoQuattro As String, ByVal ValorePrevistoPrimoPezzoCinque As String,
                            ByVal ValorePrevistoPrimoPezzoSei As String, ByVal ValorePrevistoPrimoPezzoSette As String,
                            ByVal ValorePrevistoPrimoPezzoOtto As String, ByVal ValorePrevistoPrimoPezzoNove As String,
                            ByVal ValorePrevistoPrimoPezzoDieci As String, ByVal ValoreMisuratoPrimoPezzoUno As String,
                            ByVal ValoreMisuratoPrimoPezzoDue As String, ByVal ValoreMisuratoPrimoPezzoTre As String,
                            ByVal ValoreMisuratoPrimoPezzoQuattro As String, ByVal ValoreMisuratoPrimoPezzoCinque As String,
                            ByVal ValoreMisuratoPrimoPezzoSei As String, ByVal ValoreMisuratoPrimoPezzoSette As String,
                            ByVal ValoreMisuratoPrimoPezzoOtto As String, ByVal ValoreMisuratoPrimoPezzoNove As String,
                            ByVal ValoreMisuratoPrimoPezzoDieci As String,
                            ByVal TolleranzaPiuValorePrevistoPrimoUno As String,
                            ByVal TolleranzaPiuValorePrevistoPrimoDue As String,
                            ByVal TolleranzaPiuValorePrevistoPrimoTre As String,
                            ByVal TolleranzaPiuValorePrevistoPrimoQuattro As String,
                            ByVal TolleranzaPiuValorePrevistoPrimoCinque As String,
                            ByVal TolleranzaPiuValorePrevistoPrimoSei As String,
                            ByVal TolleranzaPiuValorePrevistoPrimoSette As String,
                            ByVal TolleranzaPiuValorePrevistoPrimoOtto As String,
                            ByVal TolleranzaPiuValorePrevistoPrimoNove As String,
                            ByVal TolleranzaPiuValorePrevistoPrimoDieci As String,
                            ByVal TolleranzaMenoValorePrevistoPrimoUno As String,
                            ByVal TolleranzaMenoValorePrevistoPrimoDue As String,
                            ByVal TolleranzaMenoValorePrevistoPrimoTre As String,
                            ByVal TolleranzaMenoValorePrevistoPrimoQuattro As String,
                            ByVal TolleranzaMenoValorePrevistoPrimoCinque As String,
                            ByVal TolleranzaMenoValorePrevistoPrimoSei As String,
                            ByVal TolleranzaMenoValorePrevistoPrimoSette As String,
                            ByVal TolleranzaMenoValorePrevistoPrimoOtto As String,
                            ByVal TolleranzaMenoValorePrevistoPrimoNove As String,
                            ByVal TolleranzaMenoValorePrevistoPrimoDieci As String,
                            ByVal NotePrimoPezzo As String) As Integer

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

#Region "Inserisci in Database Secondo Pezzo"

    Public Function insertIDSecondoPezzo() As Integer
        Dim intContatoreSecondoPezzo As Integer = propIdIntestazione
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

    Public Function InsertSecondoPezzoSoloID(ByVal IDPezziSecondoPezzo As Integer, ByVal IDIntestazione As Integer) As Integer


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
            Dim intContatoreSecPezzo As Integer = propIdIntestazione

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

    Public Function InsertPezziSecondoPezzo(ByVal IDPezziSecondoPezzo As Integer,
        ByVal IDIntestazione As Integer,
        ByVal ValorePrevistoSecondoPezzoUno As String,
        ByVal ValorePrevistoSecondoPezzoDue As String,
        ByVal ValorePrevistoSecondoPezzoTre As String,
        ByVal ValorePrevistoSecondoPezzoQuattro As String,
        ByVal ValorePrevistoSecondoPezzoCinque As String,
        ByVal ValorePrevistoSecondoPezzoSei As String,
        ByVal ValorePrevistoSecondoPezzoSette As String,
        ByVal ValorePrevistoSecondoPezzoOtto As String,
        ByVal ValorePrevistoSecondoPezzoNove As String,
        ByVal ValorePrevistoSecondoPezzoDieci As String,
        ByVal ValoreMisuratoSecondoPezzoUno As String,
        ByVal ValoreMisuratoSecondoPezzoDue As String,
        ByVal ValoreMisuratoSecondoPezzoTre As String,
        ByVal ValoreMisuratoSecondoPezzoQuattro As String,
        ByVal ValoreMisuratoSecondoPezzoCinque As String,
        ByVal ValoreMisuratoSecondoPezzoSei As String,
        ByVal ValoreMisuratoSecondoPezzoSette As String,
        ByVal ValoreMisuratoSecondoPezzoOtto As String,
        ByVal ValoreMisuratoSecondoPezzoNove As String,
        ByVal ValoreMisuratoSecondoPezzoDieci As String,
        ByVal TolleranzaPiuValorePrevistoSecondoUno As String,
        ByVal TolleranzaPiuValorePrevistoSecondoDue As String,
        ByVal TolleranzaPiuValorePrevistoSecondoTre As String,
        ByVal TolleranzaPiuValorePrevistoSecondoQuattro As String,
        ByVal TolleranzaPiuValorePrevistoSecondoCinque As String,
        ByVal TolleranzaPiuValorePrevistoSecondoSei As String,
        ByVal TolleranzaPiuValorePrevistoSecondoSette As String,
        ByVal TolleranzaPiuValorePrevistoSecondoOtto As String,
        ByVal TolleranzaPiuValorePrevistoSecondoNove As String,
        ByVal TolleranzaPiuValorePrevistoSecondoDieci As String,
        ByVal TolleranzaMenoValorePrevistoSecondoUno As String,
        ByVal TolleranzaMenoValorePrevistoSecondoDue As String,
        ByVal TolleranzaMenoValorePrevistoSecondoTre As String,
        ByVal TolleranzaMenoValorePrevistoSecondoQuattro As String,
        ByVal TolleranzaMenoValorePrevistoSecondoCinque As String,
        ByVal TolleranzaMenoValorePrevistoSecondoSei As String,
        ByVal TolleranzaMenoValorePrevistoSecondoSette As String,
        ByVal TolleranzaMenoValorePrevistoSecondoOtto As String,
        ByVal TolleranzaMenoValorePrevistoSecondoNove As String,
        ByVal TolleranzaMenoValorePrevistoSecondoDieci As String,
        ByVal NoteSecondoPezzo As String) As Integer

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

#Region "Inserisci in Database Terzo Pezzo"

    Public Function insertIDTerzoPezzo() As Integer
        Dim intContatoreTerzoPezzo As Integer = propIdIntestazione
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

    Public Function InsertTerzoPezzoSoloID(ByVal IDPezziTerzoPezzo As Integer, ByVal IDIntestazione As Integer) As Integer


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
            Dim intContatoreTerPezzo As Integer = propIdIntestazione
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

    Public Function InsertPezziTerzoPezzo(ByVal IDPezziTerzoPezzo As Integer,
                                            ByVal IDIntestazione As Integer,
                                            ByVal ValorePrevistoTerzoPezzoUno As String,
                                            ByVal ValorePrevistoTerzoPezzoDue As String,
                                            ByVal ValorePrevistoTerzoPezzoTre As String,
                                            ByVal ValorePrevistoTerzoPezzoQuattro As String,
                                            ByVal ValorePrevistoTerzoPezzoCinque As String,
                                            ByVal ValorePrevistoTerzoPezzoSei As String,
                                            ByVal ValorePrevistoTerzoPezzoSette As String,
                                            ByVal ValorePrevistoTerzoPezzoOtto As String,
                                            ByVal ValorePrevistoTerzoPezzoNove As String,
                                            ByVal ValorePrevistoTerzoPezzoDieci As String,
                                            ByVal ValoreMisuratoTerzoPezzoUno As String,
                                            ByVal ValoreMisuratoTerzoPezzoDue As String,
                                            ByVal ValoreMisuratoTerzoPezzoTre As String,
                                            ByVal ValoreMisuratoTerzoPezzoQuattro As String,
                                            ByVal ValoreMisuratoTerzoPezzoCinque As String,
                                            ByVal ValoreMisuratoTerzoPezzoSei As String,
                                            ByVal ValoreMisuratoTerzoPezzoSette As String,
                                            ByVal ValoreMisuratoTerzoPezzoOtto As String,
                                            ByVal ValoreMisuratoTerzoPezzoNove As String,
                                            ByVal ValoreMisuratoTerzoPezzoDieci As String,
                                            ByVal TolleranzaPiuValorePrevistoTerzoUno As String,
                                            ByVal TolleranzaPiuValorePrevistoTerzoDue As String,
                                            ByVal TolleranzaPiuValorePrevistoTerzoTre As String,
                                            ByVal TolleranzaPiuValorePrevistoTerzoQuattro As String,
                                            ByVal TolleranzaPiuValorePrevistoTerzoCinque As String,
                                            ByVal TolleranzaPiuValorePrevistoTerzoSei As String,
                                            ByVal TolleranzaPiuValorePrevistoTerzoSette As String,
                                            ByVal TolleranzaPiuValorePrevistoTerzoOtto As String,
                                            ByVal TolleranzaPiuValorePrevistoTerzoNove As String,
                                            ByVal TolleranzaPiuValorePrevistoTerzoDieci As String,
                                            ByVal TolleranzaMenoValorePrevistoTerzoUno As String,
                                            ByVal TolleranzaMenoValorePrevistoTerzoDue As String,
                                            ByVal TolleranzaMenoValorePrevistoTerzoTre As String,
                                            ByVal TolleranzaMenoValorePrevistoTerzoQuattro As String,
                                            ByVal TolleranzaMenoValorePrevistoTerzoCinque As String,
                                            ByVal TolleranzaMenoValorePrevistoTerzoSei As String,
                                            ByVal TolleranzaMenoValorePrevistoTerzoSette As String,
                                            ByVal TolleranzaMenoValorePrevistoTerzoOtto As String,
                                            ByVal TolleranzaMenoValorePrevistoTerzoNove As String,
                                            ByVal TolleranzaMenoValorePrevistoTerzoDieci As String,
                                            ByVal NoteTerzoPezzo As String) As Integer



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

#Region "Inserisci in Database Quarto Pezzo"

    Public Function insertQuartoIDQuartoPezzo() As Integer
        Dim intContatoreQuattroPezzo As Integer = propIdIntestazione
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

    Public Function InsertPezziQuartoPezzoSoloID(ByVal IDPezziQuartoPezzo As Integer, ByVal IDIntestazione As Integer) As Integer


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


            Dim intContatoreQuattroPezzo As Integer = propIdIntestazione
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

    Public Function InsertPezziQuartoPezzo(ByVal IDPezziQuartoPezzo As Integer,
                                            ByVal IDIntestazione As Integer,
                                            ByVal ValorePrevistoQuartoPezzoUno As String,
                                            ByVal ValorePrevistoQuartoPezzoDue As String,
                                            ByVal ValorePrevistoQuartoPezzoTre As String,
                                            ByVal ValorePrevistoQuartoPezzoQuattro As String,
                                            ByVal ValorePrevistoQuartoPezzoCinque As String,
                                            ByVal ValorePrevistoQuartoPezzoSei As String,
                                            ByVal ValorePrevistoQuartoPezzoSette As String,
                                            ByVal ValorePrevistoQuartoPezzoOtto As String,
                                            ByVal ValorePrevistoQuartoPezzoNove As String,
                                            ByVal ValorePrevistoQuartoPezzoDieci As String,
                                            ByVal ValoreMisuratoQuartoPezzoUno As String,
                                            ByVal ValoreMisuratoQuartoPezzoDue As String,
                                            ByVal ValoreMisuratoQuartoPezzoTre As String,
                                            ByVal ValoreMisuratoQuartoPezzoQuattro As String,
                                            ByVal ValoreMisuratoQuartoPezzoCinque As String,
                                            ByVal ValoreMisuratoQuartoPezzoSei As String,
                                            ByVal ValoreMisuratoQuartoPezzoSette As String,
                                            ByVal ValoreMisuratoQuartoPezzoOtto As String,
                                            ByVal ValoreMisuratoQuartoPezzoNove As String,
                                            ByVal ValoreMisuratoQuartoPezzoDieci As String,
                                            ByVal TolleranzaPiuValorePrevistoQuartoUno As String,
                                            ByVal TolleranzaPiuValorePrevistoQuartoDue As String,
                                            ByVal TolleranzaPiuValorePrevistoQuartoTre As String,
                                            ByVal TolleranzaPiuValorePrevistoQuartoQuattro As String,
                                            ByVal TolleranzaPiuValorePrevistoQuartoCinque As String,
                                            ByVal TolleranzaPiuValorePrevistoQuartoSei As String,
                                            ByVal TolleranzaPiuValorePrevistoQuartoSette As String,
                                            ByVal TolleranzaPiuValorePrevistoQuartoOtto As String,
                                            ByVal TolleranzaPiuValorePrevistoQuartoNove As String,
                                            ByVal TolleranzaPiuValorePrevistoQuartoDieci As String,
                                            ByVal TolleranzaMenoValorePrevistoQuartoUno As String,
                                            ByVal TolleranzaMenoValorePrevistoQuartoDue As String,
                                            ByVal TolleranzaMenoValorePrevistoQuartoTre As String,
                                            ByVal TolleranzaMenoValorePrevistoQuartoQuattro As String,
                                            ByVal TolleranzaMenoValorePrevistoQuartoCinque As String,
                                            ByVal TolleranzaMenoValorePrevistoQuartoSei As String,
                                            ByVal TolleranzaMenoValorePrevistoQuartoSette As String,
                                            ByVal TolleranzaMenoValorePrevistoQuartoOtto As String,
                                            ByVal TolleranzaMenoValorePrevistoQuartoNove As String,
                                            ByVal TolleranzaMenoValorePrevistoQuartoDieci As String,
                                            ByVal NoteQuartoPezzo As String) As Integer


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

#Region "Inserisci in Database Quinto Pezzo"

    Public Function insertIDQuintoPezzo() As Integer
        Dim intContatoreQuintoPezzo As Integer = propIdIntestazione
        Dim result As DialogResult

        Dim IDPezziQuintoPezzo As Integer = intContatoreQuintoPezzo
        Dim IDIntestazione As Integer = intContatoreQuintoPezzo

        Try
            Dim retQuintoPezzoSoloID As Nullable(Of Integer) = InsertPezziQuintoPezzoSoloID(IDPezziQuintoPezzo, IDIntestazione)

            If Not retQuintoPezzoSoloID Is Nothing Then
                Return retQuintoPezzoSoloID
            End If
        Catch ex As Exception
            MessageBox.Show("Errore insertIDQuintoPezzo :" & ex.Message)
        End Try
    End Function

    Public Function InsertPezziQuintoPezzoSoloID(ByVal IDPezziQuintoPezzo As Integer, ByVal IDIntestazione As Integer) As Integer


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


            Dim intContatoreQuintoPezzo As Integer = propIdIntestazione
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


    Public Function InsertPezziQuintoPezzo(ByVal IDPezziQuintoPezzo As Integer,
                                            ByVal IDIntestazione As Integer,
                                            ByVal ValorePrevistoQuintoPezzoUno As String,
                                            ByVal ValorePrevistoQuintoPezzoDue As String,
                                            ByVal ValorePrevistoQuintoPezzoTre As String,
                                            ByVal ValorePrevistoQuintoPezzoQuattro As String,
                                            ByVal ValorePrevistoQuintoPezzoCinque As String,
                                            ByVal ValorePrevistoQuintoPezzoSei As String,
                                            ByVal ValorePrevistoQuintoPezzoSette As String,
                                            ByVal ValorePrevistoQuintoPezzoOtto As String,
                                            ByVal ValorePrevistoQuintoPezzoNove As String,
                                            ByVal ValorePrevistoQuintoPezzoDieci As String,
                                            ByVal ValoreMisuratoQuintoPezzoUno As String,
                                            ByVal ValoreMisuratoQuintoPezzoDue As String,
                                            ByVal ValoreMisuratoQuintoPezzoTre As String,
                                            ByVal ValoreMisuratoQuintoPezzoQuattro As String,
                                            ByVal ValoreMisuratoQuintoPezzoCinque As String,
                                            ByVal ValoreMisuratoQuintoPezzoSei As String,
                                            ByVal ValoreMisuratoQuintoPezzoSette As String,
                                            ByVal ValoreMisuratoQuintoPezzoOtto As String,
                                            ByVal ValoreMisuratoQuintoPezzoNove As String,
                                            ByVal ValoreMisuratoQuintoPezzoDieci As String,
                                            ByVal TolleranzaPiuValorePrevistoQuintoUno As String,
                                            ByVal TolleranzaPiuValorePrevistoQuintoDue As String,
                                            ByVal TolleranzaPiuValorePrevistoQuintoTre As String,
                                            ByVal TolleranzaPiuValorePrevistoQuintoQuattro As String,
                                            ByVal TolleranzaPiuValorePrevistoQuintoCinque As String,
                                            ByVal TolleranzaPiuValorePrevistoQuintoSei As String,
                                            ByVal TolleranzaPiuValorePrevistoQuintoSette As String,
                                            ByVal TolleranzaPiuValorePrevistoQuintoOtto As String,
                                            ByVal TolleranzaPiuValorePrevistoQuintoNove As String,
                                            ByVal TolleranzaPiuValorePrevistoQuintoDieci As String,
                                            ByVal TolleranzaMenoValorePrevistoQuintoUno As String,
                                            ByVal TolleranzaMenoValorePrevistoQuintoDue As String,
                                            ByVal TolleranzaMenoValorePrevistoQuintoTre As String,
                                            ByVal TolleranzaMenoValorePrevistoQuintoQuattro As String,
                                            ByVal TolleranzaMenoValorePrevistoQuintoCinque As String,
                                            ByVal TolleranzaMenoValorePrevistoQuintoSei As String,
                                            ByVal TolleranzaMenoValorePrevistoQuintoSette As String,
                                            ByVal TolleranzaMenoValorePrevistoQuintoOtto As String,
                                            ByVal TolleranzaMenoValorePrevistoQuintoNove As String,
                                            ByVal TolleranzaMenoValorePrevistoQuintoDieci As String,
                                            ByVal NoteQuintoPezzo As String) As Integer


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

#Region "Inserisci in Database Ultimo Pezzo"


    Public Function insertIDUltimoPezzo() As Integer
        Dim intContatoreUltimoPezzo As Integer = propIdIntestazione
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

    Public Function InsertPezziUtimoPezzoSoloID(ByVal IDPezziUltimoPezzo As Integer, ByVal IDIntestazione As Integer) As Integer


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

        Dim intContatoreUltimoPezzo As Integer = propIdIntestazione
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

    Public Function InsertPezziUltimoPezzo(ByVal IDPezziUltimoPezzo As Integer,
                                            ByVal IDIntestazione As Integer,
                                            ByVal ValorePrevistoUltimoPezzoUno As String,
                                            ByVal ValorePrevistoUltimoPezzoDue As String,
                                            ByVal ValorePrevistoUltimoPezzoTre As String,
                                            ByVal ValorePrevistoUltimoPezzoQuattro As String,
                                            ByVal ValorePrevistoUltimoPezzoCinque As String,
                                            ByVal ValorePrevistoUltimoPezzoSei As String,
                                            ByVal ValorePrevistoUltimoPezzoSette As String,
                                            ByVal ValorePrevistoUltimoPezzoOtto As String,
                                            ByVal ValorePrevistoUltimoPezzoNove As String,
                                            ByVal ValorePrevistoUltimoPezzoDieci As String,
                                            ByVal ValoreMisuratoUltimoPezzoUno As String,
                                            ByVal ValoreMisuratoUltimoPezzoDue As String,
                                            ByVal ValoreMisuratoUltimoPezzoTre As String,
                                            ByVal ValoreMisuratoUltimoPezzoQuattro As String,
                                            ByVal ValoreMisuratoUltimoPezzoCinque As String,
                                            ByVal ValoreMisuratoUltimoPezzoSei As String,
                                            ByVal ValoreMisuratoUltimoPezzoSette As String,
                                            ByVal ValoreMisuratoUltimoPezzoOtto As String,
                                            ByVal ValoreMisuratoUltimoPezzoNove As String,
                                            ByVal ValoreMisuratoUltimoPezzoDieci As String,
                                            ByVal TolleranzaPiuValorePrevistoUltimoUno As String,
                                            ByVal TolleranzaPiuValorePrevistoUltimoDue As String,
                                            ByVal TolleranzaPiuValorePrevistoUltimoTre As String,
                                            ByVal TolleranzaPiuValorePrevistoUltimoQuattro As String,
                                            ByVal TolleranzaPiuValorePrevistoUltimoCinque As String,
                                            ByVal TolleranzaPiuValorePrevistoUltimoSei As String,
                                            ByVal TolleranzaPiuValorePrevistoUltimoSette As String,
                                            ByVal TolleranzaPiuValorePrevistoUltimoOtto As String,
                                            ByVal TolleranzaPiuValorePrevistoUltimoNove As String,
                                            ByVal TolleranzaPiuValorePrevistoUltimoDieci As String,
                                            ByVal TolleranzaMenoValorePrevistoUltimoUno As String,
                                            ByVal TolleranzaMenoValorePrevistoUltimoDue As String,
                                            ByVal TolleranzaMenoValorePrevistoUltimoTre As String,
                                            ByVal TolleranzaMenoValorePrevistoUltimoQuattro As String,
                                            ByVal TolleranzaMenoValorePrevistoUltimoCinque As String,
                                            ByVal TolleranzaMenoValorePrevistoUltimoSei As String,
                                            ByVal TolleranzaMenoValorePrevistoUltimoSette As String,
                                            ByVal TolleranzaMenoValorePrevistoUltimoOtto As String,
                                            ByVal TolleranzaMenoValorePrevistoUltimoNove As String,
                                            ByVal TolleranzaMenoValorePrevistoUltimoDieci As String,
                                            ByVal NoteUltimoPezzo As String) As Integer


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

#Region "FUNZIONI UTILITY"

    Public Function Convert(ByVal value As Date) As Date
        Dim DateValue As DateTime = CType(value, DateTime)
        Return DateValue.ToShortDateString
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
            MessageBox.Show("Errore : " & ex.Message)
        End Try

        Return i

    End Function

    Public Function controlloConcorrenzaOttimistica(ByVal currentID As Integer) As Integer
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
                    End If
                End While
            End Using

            If currentID = i Then
                i = i + 1
            End If

        Catch ex As Exception
            MessageBox.Show("Errore : " & ex.Message)
        End Try

        Return i

    End Function

    Public Shared Function FindControlRecursive(ByVal list As List(Of Control), ByVal parent As Control, ByVal ctrlType As System.Type) As List(Of Control)
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



End Class




#Region "Codice Vecchio"


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

'    'Dim dataRowInfo As New GridViewDataRowInfo(gridViewControl.MasterView)
'    'dataRowInfo.Cells(0).Value = String.Empty
'    'dataRowInfo.Cells(1).Value = String.Empty
'    'dataRowInfo.Cells(2).Value = String.Empty
'    'dataRowInfo.Cells(3).Value = String.Empty
'    'dataRowInfo.Cells(4).Value = String.Empty
'    'gridViewControl.Rows.Insert(0, dataRowInfo)


'End Sub





'#Region "Get Ultimate Parent"
'Private Function GetParentControllo(ByVal parent As Control) As Control
'    Dim parent_control As Control = TryCast(parent, Control)
'    '------------------------------------------------------------------------
'    'Specific to a control means if you want to find only for certain control
'    If TypeOf parent_control Is TableLayoutPanel Then   'myControl is of UserControl
'        Return parent_control
'    End If
'    '------------------------------------------------------------------------
'    If parent_control.Parent Is Nothing Then
'        Return parent_control
'    End If
'    If parent IsNot Nothing Then
'        Return GetParentControllo(parent.Parent)
'    End If
'    Return Nothing
'End Function
'#End Region




'Riempie il combo del Lotto sul Load della form
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


#End Region