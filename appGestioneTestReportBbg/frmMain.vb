Imports Telerik.WinControls.UI
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Globalization
Imports Telerik.WinControls.UI.Localization
Imports CrystalDecisions.CrystalReports.Engine
Imports Database = Microsoft.Practices.EnterpriseLibrary.Data.Database
Imports System.IO

Public Class frmMain

#Region "Proprieta"
    Public Shared factory As New DatabaseProviderFactory()
    Public Property _db As Database = factory.Create("dbConnStrRete")
    Public Property indexRigaPerAggiorna As Integer
    Public Property collChanged As Boolean
    Private imgFile As String = ""
    Public Property cryRpt As New ReportDocument
#End Region

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RadGridLocalizationProvider.CurrentProvider = New MyItalianRadGridLocalizationProvider()
        'AddHandler gridTestReport.FilterDescriptors.CollectionChanged, AddressOf FilterDescriptors_CollectionChanged
        Try
            getTestata()

            'Scroll su ultima riga che sarebbe la piu recente e seleziona
            gridTestReport.TableElement.ScrollToRow(gridTestReport.ChildRows.Count)
            Dim lastRow As GridViewRowInfo = gridTestReport.ChildRows(gridTestReport.Rows.Count - 1)
            lastRow.IsSelected = True
            lastRow.IsCurrent = True

        Catch ex As Exception
            MessageBox.Show("Errore :" & ex.Message)
        End Try
    End Sub

    Private Sub gridTestReport_CellClick(sender As Object, e As GridViewCellEventArgs) Handles gridTestReport.CellClick
        If TypeOf gridTestReport.CurrentRow Is GridViewDataRowInfo Then
            indexRigaPerAggiorna = gridTestReport.Rows.IndexOf(DirectCast(gridTestReport.CurrentRow, GridViewDataRowInfo))
        End If
    End Sub

    Public Sub getTestata()
        Try
            Dim dataColumn As GridViewDataColumn = TryCast(Me.gridTestReport.Columns("Data"), GridViewDataColumn)
            dataColumn.FormatString = "{0:dd/MM/yyyy}"
            dataColumn.FormatInfo = CultureInfo.CreateSpecificCulture("it-IT")
            Dim cmd As DbCommand = _db.GetStoredProcCommand("spGetIntestazione")
            Using datareader As IDataReader = _db.ExecuteReader(cmd)
                gridTestReport.MasterTemplate.LoadFrom(datareader)
            End Using
            Me.gridTestReport.CurrentRow = Nothing
        Catch ex As Exception
            MessageBox.Show("Errore :" & ex.Message)
        End Try
    End Sub

    Private Sub btnAggiungiTestReport_Click(sender As Object, e As EventArgs) Handles btnAggiungiTestReport.Click
        Dim frmAggTestRep As New frmRegistraTestReportTre
        frmAggTestRep.Show()
    End Sub

    Private Sub btnModifcaTestReport_Click(sender As Object, e As EventArgs) Handles btnModifcaTestReport.Click
        Dim frmUpdateTestReport As New frmAggiornaTestReportTre
        Dim result As DialogResult
        If Me.gridTestReport.MasterTemplate.SelectedRows.Count > 0 Then
            'propIdIntestazione = Me.gridTestReport.Rows(Me.gridTestReport.CurrentRow.Index).Cells(0).Value
            frmUpdateTestReport.Show()
        Else
            result = MessageBox.Show("Per favore selezionare una riga della griglia Test Report", "Messaggio di Errore", MessageBoxButtons.OK)
            If result = DialogResult.OK Then
                Exit Sub
            End If
        End If

    End Sub

    Private Sub btnEsci_Click(sender As Object, e As EventArgs) Handles btnEsci.Click
        Me.Close()
        Application.Exit()
    End Sub

    Private Sub btnAggiorna_Click(sender As Object, e As EventArgs) Handles btnAggiorna.Click
        getTestata()
        gridTestReport.CurrentRow = gridTestReport.Rows(indexRigaPerAggiorna)
    End Sub

    Private Sub btnEliminaTestReport_Click(sender As Object, e As EventArgs) Handles btnEliminaTestReport.Click

        Try
            ' Valore dell'ID della riga filtrato
            '  Dim IdFiltrato As Integer = gridTestReport.ChildRows(gridTestReport.CurrentRow.Index).Cells(0).Value

            propIdIntestazione = gridTestReport.ChildRows(gridTestReport.CurrentRow.Index).Cells(0).Value

            Dim result As DialogResult

            ' If propIdIntestazione = IdFiltrato Then

            If gridTestReport.SelectedRows.Count > 0 Then
                    result = MessageBox.Show(" Sei sicuro di voler eliminare il Test Report N° : " & propIdIntestazione & " ? ", "Elimina test Report in Database", MessageBoxButtons.OKCancel)
                    boolReturnMain = False
                    If result = DialogResult.Cancel Then
                        Exit Sub
                    End If

                    If result = Windows.Forms.DialogResult.OK Then
                        EliminaTestataByIdIntestazione(propIdIntestazione)
                        EliminaPrimoPezzoByIdIntestazione(propIdIntestazione)
                        EliminaSecondoPezzoByIdIntestazione(propIdIntestazione)
                        EliminaTerzoPezzoByIdIntestazione(propIdIntestazione)
                        EliminaQuartoPezzoByIdIntestazione(propIdIntestazione)
                        EliminaQuintoPezzoByIdIntestazione(propIdIntestazione)
                        EliminaUltimoPezzoByIdIntestazione(propIdIntestazione)
                        getTestata()
                        'Riposiziona la griglia sulla riga dopo quella eliminata
                        gridTestReport.CurrentRow = gridTestReport.Rows(indexRigaPerAggiorna - 1)
                        gridTestReport.CurrentRow.IsSelected = True
                    Else
                        Exit Sub
                    End If
                Else
                    MessageBox.Show("Seleziona una riga !!")
                End If

            ' Else
            'Dim result2 As Integer = MessageBox.Show("ATTENZIONE : non si può cancellare un Test Report che fa parte di righe filtrate ! Togliere prima il filtro e poi cancellare il Test report", "ATTENZIONE:Righe Filtrate", MessageBoxButtons.OK)
            '    If result = DialogResult.OK Then
            '        Exit Sub
            '    End If
            '   End If



        Catch ex As Exception
            MessageBox.Show("Errore Elimina Test Report : " & ex.Message)
        End Try

    End Sub

    'Private Sub FilterDescriptors_CollectionChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.Data.NotifyCollectionChangedEventArgs)
    '    'Evento creato che restituisce variabile booleana se le righe sono state filtrate
    '    collChanged = True
    'End Sub

    Public Function EliminaTestataByIdIntestazione(idIntestazione As Integer) As Integer
        Dim deleteCommand As DbCommand = Nothing
        Dim rowsAffected As Integer

        Try
            deleteCommand = _db.GetStoredProcCommand("spDeleteIntestazioneByIdIntestazione")
            _db.AddInParameter(deleteCommand, "IDIntestazione", DbType.Int32, idIntestazione)
            rowsAffected = _db.ExecuteNonQuery(deleteCommand)

        Catch ex As Exception
            MessageBox.Show("Errore EliminaTestataByIdIntestazione : " & ex.Message)
        End Try
        Return rowsAffected
    End Function

    Public Function EliminaPrimoPezzoByIdIntestazione(idIntestazione As Integer) As Integer
        Dim deleteCommand As DbCommand = Nothing
        Dim rowsAffected As Integer

        Try
            deleteCommand = _db.GetStoredProcCommand("spDeletePrimoPezzoByIDIntestazione")
            _db.AddInParameter(deleteCommand, "IDIntestazione", DbType.Int32, idIntestazione)
            rowsAffected = _db.ExecuteNonQuery(deleteCommand)

        Catch ex As Exception
            MessageBox.Show("Errore EliminaPrimoPezzoByIdIntestazione : " & ex.Message)
        End Try
        Return rowsAffected
    End Function

    Public Function EliminaSecondoPezzoByIdIntestazione(idIntestazione As Integer) As Integer
        Dim deleteCommand As DbCommand = Nothing
        Dim rowsAffected As Integer

        Try
            deleteCommand = _db.GetStoredProcCommand("spDeleteSecondoPezzoByIdIntestazione")
            _db.AddInParameter(deleteCommand, "IDIntestazione", DbType.Int32, idIntestazione)
            rowsAffected = _db.ExecuteNonQuery(deleteCommand)
        Catch ex As Exception
            MessageBox.Show("Errore EliminaSecondoPezzoByIdIntestazione : " & ex.Message)
        End Try
        Return rowsAffected
    End Function

    Public Function EliminaTerzoPezzoByIdIntestazione(idIntestazione As Integer) As Integer
        Dim deleteCommand As DbCommand = Nothing
        Dim rowsAffected As Integer

        Try
            deleteCommand = _db.GetStoredProcCommand("spDeleteQueryTerzoPezzoByIdIntestazione")
            _db.AddInParameter(deleteCommand, "IDIntestazione", DbType.Int32, idIntestazione)
            rowsAffected = _db.ExecuteNonQuery(deleteCommand)
        Catch ex As Exception
            MessageBox.Show("Errore EliminaTerzoPezzoByIdIntestazione : " & ex.Message)
        End Try
        Return rowsAffected
    End Function


    Public Function EliminaQuartoPezzoByIdIntestazione(idIntestazione As Integer) As Integer
        Dim deleteCommand As DbCommand = Nothing
        Dim rowsAffected As Integer

        Try
            deleteCommand = _db.GetStoredProcCommand("spDeleteQueryQuartoPezzoByIdIntestazione")
            _db.AddInParameter(deleteCommand, "IDIntestazione", DbType.Int32, idIntestazione)
            rowsAffected = _db.ExecuteNonQuery(deleteCommand)
        Catch ex As Exception
            MessageBox.Show("Errore EliminaQuartoPezzoByIdIntestazione : " & ex.Message)
        End Try
        Return rowsAffected
    End Function


    Public Function EliminaQuintoPezzoByIdIntestazione(idIntestazione As Integer) As Integer
        Dim deleteCommand As DbCommand = Nothing
        Dim rowsAffected As Integer

        Try
            deleteCommand = _db.GetStoredProcCommand("spDeleteQueryQuintoPezzoByIdIntestazione")
            _db.AddInParameter(deleteCommand, "IDIntestazione", DbType.Int32, idIntestazione)
            rowsAffected = _db.ExecuteNonQuery(deleteCommand)
        Catch ex As Exception
            MessageBox.Show("Errore EliminaQuintoPezzoByIdIntestazione : " & ex.Message)
        End Try
        Return rowsAffected
    End Function

    Public Function EliminaUltimoPezzoByIdIntestazione(idIntestazione As Integer) As Integer
        Dim deleteCommand As DbCommand = Nothing
        Dim rowsAffected As Integer

        Try
            deleteCommand = _db.GetStoredProcCommand("spDeleteQueryUltimoPezzoByIdIntestazione")
            _db.AddInParameter(deleteCommand, "IDIntestazione", DbType.Int32, idIntestazione)
            rowsAffected = _db.ExecuteNonQuery(deleteCommand)
        Catch ex As Exception
            MessageBox.Show("Errore EliminaUltimoPezzoByIdIntestazione : " & ex.Message)
        End Try
        Return rowsAffected
    End Function

    Private Sub btnOpenSalvaNuovoDaEsistente_Click(sender As Object, e As EventArgs) Handles btnOpenSalvaNuovoDaEsistente.Click
        Dim result As DialogResult
        Dim frmSalvaNuovoDaEsistente As New frmNuovoDaEsistenteTestReportTre
        If Me.gridTestReport.MasterTemplate.SelectedRows.Count > 0 Then
            propFornitore = getFornitore(propIdIntestazione)
            ' propIdIntestazione = Me.gridTestReport.Rows(Me.gridTestReport.CurrentRow.Index).Cells(0).Value
            frmSalvaNuovoDaEsistente.Show()
        Else
            result = MessageBox.Show("Per favore selezionare una riga della griglia Test Report", "Messaggio di Errore", MessageBoxButtons.OK)
            If result = DialogResult.OK Then
                Exit Sub
            End If
        End If

    End Sub

    'Estrae Fornitore per Combo in NuovoDa Esistente
    Public Function getFornitore(id As Integer) As String
        Dim cmd As DbCommand
        Dim strSQL As String = "spGetFornitoreByID"
        cmd = _db.GetStoredProcCommand(strSQL)
        _db.AddInParameter(cmd, "idIntestazione", DbType.Int32, id)
        Using datareader As IDataReader = _db.ExecuteReader(cmd)
            While datareader.Read
                propFornitore = datareader("Fornitore")
            End While
        End Using
        Return propFornitore
    End Function

    Private Sub gridTestReport_CurrentRowChanged(sender As Object, e As CurrentRowChangedEventArgs) Handles gridTestReport.CurrentRowChanged
        If e.CurrentRow Is Nothing Then
            If e.OldRow IsNot Nothing Then
                propIdIntestazione = e.OldRow.Cells("IDIntestazione").Value
            Else

            End If
        Else
            If e.OldRow IsNot Nothing Then
                propIdIntestazione = e.OldRow.Cells("IDIntestazione").Value
                propIdIntestazione = e.CurrentRow.Cells("IDIntestazione").Value
            Else
                propIdIntestazione = e.CurrentRow.Cells("IDIntestazione").Value
            End If
        End If
    End Sub

    Private Sub btnRegistraLotto_Click(sender As Object, e As EventArgs) Handles btnRegistraLotto.Click
        Dim nuovoRegistraLotto As New frmGestioneLottoMaterialeNuovo
        nuovoRegistraLotto.Show()
    End Sub

    Private Sub btnAggiungiFornitoriStrumMate_Click(sender As Object, e As EventArgs) Handles btnAggiungiFornitoriStrumMate.Click
        Dim frmAggiungiFornMateStrum As New aggiungiStrumMatForn
        frmAggiungiFornMateStrum.Show()
    End Sub

    Private Sub btnStampaDirettaMain_Click(sender As Object, e As EventArgs) Handles btnStampaDirettaMain.Click
        'Stampa report su carta
        GetAndInsertPerStampa()
        Dim frmRP As New frmStampaPreview
        frmRP.Show()
    End Sub

    Private Sub GetAndInsertPerStampa()
        Dim cmd As DbCommand = _db.GetStoredProcCommand("spGetInstazioneByIdPerStampa")
        Dim dataColumn As GridViewDataColumn = gridTestReport.Columns("Data")
        dataColumn.FormatString = "{0:dd/MM/yyyy}"
        dataColumn.FormatInfo = CultureInfo.CreateSpecificCulture("it-IT")
        Try
            Dim lastRow As GridViewRowInfo = gridTestReport.Rows(gridTestReport.Rows.Count - 1)
            'lastRow.IsSelected = False
            For Each rowInfo As GridViewRowInfo In gridTestReport.Rows

                'Loop sulle righe selezionate griglia
                If rowInfo.IsSelected Then
                    rowInfo.EnsureVisible()
                    'propIdIntestazioneStampa = Me.gridTestReport.Rows(Me.gridTestReport.CurrentRow.Index).Cells(0).Value

                    'Legge Id della riga selezionata nella griglia
                    propIdIntestazioneStampa = rowInfo.Cells("IDIntestazione").Value

                    'Seleziona e poi salva in DB testata
                    getTestataByIdIntestazioneStampa(propIdIntestazioneStampa)
                    SalvaTestataTestReportBusLayerStampa()

                    'Seleziona e poi salva in DB Primo Pezzo
                    If (Not getPrimoPezzoByIdIntestazionePerStampa(propIdIntestazioneStampa) = 0) Then
                        SalvaPrimoPezzoStampa()
                    End If

                    'Seleziona e poi salva in DB Secondo Pezzo
                    If (Not getSecondoPezzoByIdIntestazioneStampa(propIdIntestazioneStampa) = 0) Then
                        SalvaSecondoPezzoStampa()
                    End If

                    'Seleziona e poi salva in DB Terzo Pezzo
                    If (Not getTerzoPezzoByIdIntestazioneStampa(propIdIntestazioneStampa) = 0) Then
                        SalvaTerzoPezzoStampa()
                    End If

                    'Seleziona e poi salva in DB Quarto Pezzo
                    If (Not getQuartoPezzoByIdIntestazioneStampa(propIdIntestazioneStampa) = 0) Then
                        SalvaQuartoPezzoStampa()
                    End If

                    'Seleziona e poi salva in DB Quinto Pezzo
                    If (Not getQuintoPezzoByIdIntestazioneStampa(propIdIntestazioneStampa) = 0) Then
                        SalvaQuintoPezzoStampa()
                    End If

                    'Seleziona e poi salva in DB Ultimo Pezzo
                    If (Not getUltimoPezzoByIdIntestazioneStampa(propIdIntestazioneStampa) = 0) Then
                        SalvaUltimoPezzoStampa()
                    End If

                End If
            Next
        Catch ex As Exception
            MessageBox.Show("Errore :" & ex.Message)
        End Try
    End Sub

#Region "Get e Insert Testata per stampa"

    Public Sub getTestataByIdIntestazioneStampa(intIdItestazioneStampa As Integer)

        Dim cmd As DbCommand = _db.GetStoredProcCommand("spGetInstazioneByIdPerStampa")
        Dim dataReport As Date
        Try
            _db.AddInParameter(cmd, "IDIntestazione", DbType.Int32, intIdItestazioneStampa)

            Using datareader As IDataReader = _db.ExecuteReader(cmd)

                While datareader.Read
                    propIdIntestazioneStampa = datareader("IDIntestazione")
                    propNumeroOrdineStampa = datareader("NumOrdine").ToString
                    propCodiceArticoloStampa = datareader("CodArticolo").ToString
                    dataReport = Converti(datareader("Data").ToString)
                    propDataReportStampa = dataReport
                    If Not datareader("Materiale").ToString Is Nothing Then
                        propMaterialeStampa = datareader("Materiale").ToString
                    Else
                        propMaterialeStampa = "No Materiale"
                    End If
                    propStrumentoStampa = datareader("Strumento").ToString
                    propMacchinaNumStampa = datareader("MacchinaNum").ToString
                    propRigaOrdNumStampa = CType(datareader("RigaOrdNum"), Integer)
                    propNumPezziStampa = CType(datareader("NumPezzi"), Integer)
                    propFornitoreStampa = datareader("Fornitore").ToString
                    propNumLottoStampa = datareader("NumLotto").ToString
                    propPrimoPezzoStampa = CType(datareader("PrimoPezzo"), Boolean)
                    propUltimoPezzoStampa = CType(datareader("UltimoPezzo"), Boolean)
                    propPezziControllatiStampa = CType(datareader("PezzoNumero"), Integer)
                    propOperatoreStampa = datareader("Operatore").ToString
                End While
            End Using

        Catch ex As Exception
            MessageBox.Show("Errore : " & ex.Message)
        End Try

    End Sub


    Public Sub SalvaTestataTestReportBusLayerStampa()

        Dim ret As Nullable(Of Integer)
        Dim result As DialogResult
        Dim materiale As String
        Dim strumento As String
        Dim macchinaNum As String
        Dim fornitore As String
        Dim operatore As String
        Dim pezzoNumero As Nullable(Of Integer)
        Dim numPezzi As Nullable(Of Integer)
        Dim rigaOrdNum As Nullable(Of Integer)

        Try
            materiale = propMaterialeStampa
            strumento = propStrumentoStampa
            macchinaNum = propMacchinaNumStampa
            fornitore = propFornitoreStampa
            operatore = propOperatoreStampa


            Dim iDIntestazione As Integer = propIdIntestazioneStampa
            Dim numOrdine As String = propNumeroOrdineStampa
            Dim codiceArticolo As String = propCodiceArticoloStampa
            Dim data As Date = Converti(propDataReportStampa)

            Dim numLotto As String = propNumLottoStampa
            Dim primoPezzo As Boolean = propPrimoPezzoStampa
            Dim ultimoPezzo As Boolean = propUltimoPezzoStampa

            pezzoNumero = propPezziControllatiStampa
            rigaOrdNum = propRigaOrdNumStampa
            numPezzi = propNumPezziStampa

            ret = insertTestataReportStampa(iDIntestazione, numOrdine, codiceArticolo, data, materiale, strumento,
                               macchinaNum, rigaOrdNum, numPezzi, fornitore, numLotto, primoPezzo, ultimoPezzo, pezzoNumero, operatore)

            If Not ret Is Nothing Then
                'result = MessageBox.Show("Intestazione Per Stampa Salvata", "Inserimento test Report in Tabella Temporanea per Stampa", MessageBoxButtons.OKCancel)
                If result = DialogResult.Cancel Then
                    Exit Sub
                End If
            End If

        Catch ex As Exception
            MessageBox.Show("Errore SalvaTestataTestReportBusLayerStampa :" & ex.Message)
        End Try

    End Sub

    Public Function insertTestataReportStampa(IDIntestazione As Integer, numOrdine As String,
codArticolo As String, data As Date, materiale As String,
strumento As String,
macchinanum As String, rigaordnum As Integer,
numpezzi As Integer, fornitore As String, numlotto As String,
primopezzo As Boolean, ultimopezzo As Boolean,
pezzonumero As Integer, operatore As String) As Integer

        Dim insertCommand As DbCommand = Nothing
        Dim rowsAffected As Integer
        Dim dataShorth As Date = Converti(data)

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
        Dim fsSimo As FileStream
        Dim fsNoFirma As FileStream

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


        Dim strQuery As String = "INSERT INTO tblIntestazioneStampa " &
                      "(IDIntestazione, NumOrdine, CodArticolo, " &
                      "Data, Materiale, Strumento," &
                      "MacchinaNum, RigaOrdNum, NumPezzi, Fornitore, " &
                      "NumLotto, PrimoPezzo, UltimoPezzo, PezzoNumero, Operatore ,imgFirmaOperatore) " &
                      " VALUES " &
                      "(@IDIntestazione, @NumOrdine, @CodArticolo, @Data," &
                      " @Materiale, @Strumento, @MacchinaNum," &
                      " @RigaOrdNum, @NumPezzi, @Fornitore, @NumLotto," &
                      " @PrimoPezzo, @UltimoPezzo, @PezzoNumero, @Operatore ,@imgFirmaOperatore)"

        Try
            insertCommand = _db.GetSqlStringCommand(strQuery)

            _db.AddInParameter(insertCommand, "IDIntestazione", DbType.Int32, IDIntestazione)
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
            MessageBox.Show("Errore insertTestataReportStampa : " & ex.Message)
        End Try

        Return rowsAffected

    End Function

#End Region

#Region "Get e Insert Primo Pezzo Per Stampa"

    Public Function getPrimoPezzoByIdIntestazionePerStampa(intIdItestazioneStampa As Integer) As Integer

        Dim cmd As DbCommand = _db.GetStoredProcCommand("spGetPrimoPezzoByIdIntestazionePerStampa")
        Dim rowsPrimo As Integer

        Try
            _db.AddInParameter(cmd, "IDIntestazione", DbType.Int32, intIdItestazioneStampa)

            Using datareader As IDataReader = _db.ExecuteReader(cmd)

                While datareader.Read
                    rowsPrimo += 1
                    propValorePrevistoPrimoUnoStampa = datareader("ValorePrevistoPrimoPezzoUno").ToString
                    propValorePrevistoPrimoDueStampa = datareader("ValorePrevistoPrimoPezzoDue").ToString
                    propValorePrevistoPrimoTreStampa = datareader("ValorePrevistoPrimoPezzoTre").ToString
                    propValorePrevistoPrimoQuattroStampa = datareader("ValorePrevistoPrimoPezzoQuattro").ToString
                    propValorePrevistoPrimoCinqueStampa = datareader("ValorePrevistoPrimoPezzoCinque").ToString
                    propValorePrevistoPrimoSeiStampa = datareader("ValorePrevistoPrimoPezzoSei").ToString
                    propValorePrevistoPrimoSetteStampa = datareader("ValorePrevistoPrimoPezzoSette").ToString
                    propValorePrevistoPrimoOttoStampa = datareader("ValorePrevistoPrimoPezzoOtto").ToString
                    propValorePrevistoPrimoNoveStampa = datareader("ValorePrevistoPrimoPezzoNove").ToString
                    propValorePrevistoPrimoDieciStampa = datareader("ValorePrevistoPrimoPezzoDieci").ToString

                    propValoreMisuratoPrimoUnoStampa = datareader("ValoreMisuratoPrimoPezzoUno").ToString
                    propValoreMisuratoPrimoDueStampa = datareader("ValoreMisuratoPrimoPezzoDue").ToString
                    propValoreMisuratoPrimoTreStampa = datareader("ValoreMisuratoPrimoPezzoTre").ToString
                    propValoreMisuratoPrimoQuattroStampa = datareader("ValoreMisuratoPrimoPezzoQuattro").ToString
                    propValoreMisuratoPrimoCinqueStampa = datareader("ValoreMisuratoPrimoPezzoCinque").ToString
                    propValoreMisuratoPrimoSeiStampa = datareader("ValoreMisuratoPrimoPezzoSei").ToString
                    propValoreMisuratoPrimoSetteStampa = datareader("ValoreMisuratoPrimoPezzoSette").ToString
                    propValoreMisuratoPrimoOttoStampa = datareader("ValoreMisuratoPrimoPezzoOtto").ToString
                    propValoreMisuratoPrimoNoveStampa = datareader("ValoreMisuratoPrimoPezzoNove").ToString
                    propValoreMisuratoPrimoDieciStampa = datareader("ValoreMisuratoPrimoPezzoDieci").ToString

                    propTolleranzaPiupropValorePrevistoPrimoUnoStampa = datareader("TolleranzaPiuValorePrevistoPrimoUno").ToString
                    propTolleranzaPiupropValorePrevistoPrimoDueStampa = datareader("TolleranzaPiuValorePrevistoPrimoDue").ToString
                    propTolleranzaPiupropValorePrevistoPrimoTreStampa = datareader("TolleranzaPiuValorePrevistoPrimoTre").ToString
                    propTolleranzaPiupropValorePrevistoPrimoQuattroStampa = datareader("TolleranzaPiuValorePrevistoPrimoQuattro").ToString
                    propTolleranzaPiupropValorePrevistoPrimoCinqueStampa = datareader("TolleranzaPiuValorePrevistoPrimoCinque").ToString
                    propTolleranzaPiupropValorePrevistoPrimoSeiStampa = datareader("TolleranzaPiuValorePrevistoPrimoSei").ToString
                    propTolleranzaPiupropValorePrevistoPrimoSetteStampa = datareader("TolleranzaPiuValorePrevistoPrimoSette").ToString
                    propTolleranzaPiupropValorePrevistoPrimoOttoStampa = datareader("TolleranzaPiuValorePrevistoPrimoOtto").ToString
                    propTolleranzaPiupropValorePrevistoPrimoNoveStampa = datareader("TolleranzaPiuValorePrevistoPrimoNove").ToString
                    propTolleranzaPiupropValorePrevistoPrimoDieciStampa = datareader("TolleranzaPiuValorePrevistoPrimoDieci").ToString

                    propTolleranzaMenopropValorePrevistoPrimoUnoStampa = datareader("TolleranzaMenoValorePrevistoPrimoUno").ToString
                    propTolleranzaMenopropValorePrevistoPrimoDueStampa = datareader("TolleranzaMenoValorePrevistoPrimoDue").ToString
                    propTolleranzaMenopropValorePrevistoPrimoTreStampa = datareader("TolleranzaMenoValorePrevistoPrimoTre").ToString
                    propTolleranzaMenopropValorePrevistoPrimoQuattroStampa = datareader("TolleranzaMenoValorePrevistoPrimoQuattro").ToString
                    propTolleranzaMenopropValorePrevistoPrimoCinqueStampa = datareader("TolleranzaMenoValorePrevistoPrimoCinque").ToString
                    propTolleranzaMenopropValorePrevistoPrimoSeiStampa = datareader("TolleranzaMenoValorePrevistoPrimoSei").ToString
                    propTolleranzaMenopropValorePrevistoPrimoSetteStampa = datareader("TolleranzaMenoValorePrevistoPrimoSette").ToString
                    propTolleranzaMenopropValorePrevistoPrimoOttoStampa = datareader("TolleranzaMenoValorePrevistoPrimoOtto").ToString
                    propTolleranzaMenopropValorePrevistoPrimoNoveStampa = datareader("TolleranzaMenoValorePrevistoPrimoNove").ToString
                    propTolleranzaMenopropValorePrevistoPrimoDieciStampa = datareader("TolleranzaMenoValorePrevistoPrimoDieci").ToString

                    propNotePrimoPezzoStampa = datareader("NotePrimoPezzo").ToString

                End While

            End Using

            Return rowsPrimo

        Catch ex As Exception
            MessageBox.Show("Errore getPrimoPezzoByIdIntestazionePerStampa : " & ex.Message)
        End Try

    End Function



    Public Function SalvaPrimoPezzoStampa() As Integer

        Try
            Dim intContatorePU As Integer = propIdIntestazioneStampa


            Dim IDPezziPrimoPezzo As Integer = intContatorePU
            Dim IDIntestazione As Integer = intContatorePU
            Dim ValorePrevistoPrimoPezzoUno As String = propValorePrevistoPrimoUnoStampa
            Dim ValorePrevistoPrimoPezzoDue As String = propValorePrevistoPrimoDueStampa
            Dim ValorePrevistoPrimoPezzoTre As String = propValorePrevistoPrimoTreStampa
            Dim ValorePrevistoPrimoPezzoQuattro As String = propValorePrevistoPrimoQuattroStampa
            Dim ValorePrevistoPrimoPezzoCinque As String = propValorePrevistoPrimoCinqueStampa
            Dim ValorePrevistoPrimoPezzoSei As String = propValorePrevistoPrimoSeiStampa
            Dim ValorePrevistoPrimoPezzoSette As String = propValorePrevistoPrimoSetteStampa
            Dim ValorePrevistoPrimoPezzoOtto As String = propValorePrevistoPrimoOttoStampa
            Dim ValorePrevistoPrimoPezzoNove As String = propValorePrevistoPrimoNoveStampa
            Dim ValorePrevistoPrimoPezzoDieci As String = propValorePrevistoPrimoDieciStampa

            Dim ValoreMisuratoPrimoPezzoUno As String = propValoreMisuratoPrimoUnoStampa
            Dim ValoreMisuratoPrimoPezzoDue As String = propValoreMisuratoPrimoDueStampa
            Dim ValoreMisuratoPrimoPezzoTre As String = propValoreMisuratoPrimoTreStampa
            Dim ValoreMisuratoPrimoPezzoQuattro As String = propValoreMisuratoPrimoQuattroStampa
            Dim ValoreMisuratoPrimoPezzoCinque As String = propValoreMisuratoPrimoCinqueStampa
            Dim ValoreMisuratoPrimoPezzoSei As String = propValoreMisuratoPrimoSeiStampa
            Dim ValoreMisuratoPrimoPezzoSette As String = propValoreMisuratoPrimoSetteStampa
            Dim ValoreMisuratoPrimoPezzoOtto As String = propValoreMisuratoPrimoOttoStampa
            Dim ValoreMisuratoPrimoPezzoNove As String = propValoreMisuratoPrimoNoveStampa
            Dim ValoreMisuratoPrimoPezzoDieci As String = propValoreMisuratoPrimoDieciStampa

            Dim TolleranzaPiuValorePrevistoPrimoUno As String = propTolleranzaPiupropValorePrevistoPrimoUnoStampa
            Dim TolleranzaPiuValorePrevistoPrimoDue As String = propTolleranzaPiupropValorePrevistoPrimoDueStampa
            Dim TolleranzaPiuValorePrevistoPrimoTre As String = propTolleranzaPiupropValorePrevistoPrimoTreStampa
            Dim TolleranzaPiuValorePrevistoPrimoQuattro As String = propTolleranzaPiupropValorePrevistoPrimoQuattroStampa
            Dim TolleranzaPiuValorePrevistoPrimoCinque As String = propTolleranzaPiupropValorePrevistoPrimoCinqueStampa
            Dim TolleranzaPiuValorePrevistoPrimoSei As String = propTolleranzaPiupropValorePrevistoPrimoSeiStampa
            Dim TolleranzaPiuValorePrevistoPrimoSette As String = propTolleranzaPiupropValorePrevistoPrimoSetteStampa
            Dim TolleranzaPiuValorePrevistoPrimoOtto As String = propTolleranzaPiupropValorePrevistoPrimoOttoStampa
            Dim TolleranzaPiuValorePrevistoPrimoNove As String = propTolleranzaPiupropValorePrevistoPrimoNoveStampa
            Dim TolleranzaPiuValorePrevistoPrimoDieci As String = propTolleranzaPiupropValorePrevistoPrimoDieciStampa

            Dim TolleranzaMenoValorePrevistoPrimoUno As String = propTolleranzaMenopropValorePrevistoPrimoUnoStampa
            Dim TolleranzaMenoValorePrevistoPrimoDue As String = propTolleranzaMenopropValorePrevistoPrimoDueStampa
            Dim TolleranzaMenoValorePrevistoPrimoTre As String = propTolleranzaMenopropValorePrevistoPrimoTreStampa
            Dim TolleranzaMenoValorePrevistoPrimoQuattro As String = propTolleranzaMenopropValorePrevistoPrimoQuattroStampa
            Dim TolleranzaMenoValorePrevistoPrimoCinque As String = propTolleranzaMenopropValorePrevistoPrimoCinqueStampa
            Dim TolleranzaMenoValorePrevistoPrimoSei As String = propTolleranzaMenopropValorePrevistoPrimoSeiStampa
            Dim TolleranzaMenoValorePrevistoPrimoSette As String = propTolleranzaMenopropValorePrevistoPrimoSetteStampa
            Dim TolleranzaMenoValorePrevistoPrimoOtto As String = propTolleranzaMenopropValorePrevistoPrimoOttoStampa
            Dim TolleranzaMenoValorePrevistoPrimoNove As String = propTolleranzaMenopropValorePrevistoPrimoNoveStampa
            Dim TolleranzaMenoValorePrevistoPrimoDieci As String = propTolleranzaMenopropValorePrevistoPrimoDieciStampa

            Dim NotePrimoPezzo As String = propNotePrimoPezzoStampa

            Dim retPrimoPezzo As Nullable(Of Integer) = InsertPezziPrimoPezzoStampa(IDPezziPrimoPezzo, IDIntestazione, ValorePrevistoPrimoPezzoUno,
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
            MessageBox.Show("Errore SalvaPrimoPezzoStampa :" & ex.Message)
        End Try

    End Function


    Public Function InsertPezziPrimoPezzoStampa(IDPezziPrimoPezzo As Integer, IDIntestazione As Integer, ValorePrevistoPrimoPezzoUno As String,
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

        Dim strQuery As String = "INSERT INTO tblPezziPrimoPezzoStampa " &
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
            MessageBox.Show("Errore InsertPezziPrimoPezzoStampa : " & ex.Message)
        End Try

        Return rowsAffectedPrimoPezzo

    End Function





#End Region

#Region "Get e Insert Secondo Pezzo Per Stampa"


    Public Function getSecondoPezzoByIdIntestazioneStampa(intIdItestazioneStampa As Integer) As Integer

        Dim cmd As DbCommand = _db.GetStoredProcCommand("spGetSecondoPezzoByIdIntestazioneStampa")
        Dim rowsSecondo As Integer

        Try
            _db.AddInParameter(cmd, "IDIntestazione", DbType.Int32, intIdItestazioneStampa)

            Using datareader As IDataReader = _db.ExecuteReader(cmd)

                While datareader.Read
                    rowsSecondo += 1
                    propValorePrevistoSecondoUnoStampa = datareader("ValorePrevistoSecondoPezzoUno").ToString
                    propValorePrevistoSecondoDueStampa = datareader("ValorePrevistoSecondoPezzoDue").ToString
                    propValorePrevistoSecondoTreStampa = datareader("ValorePrevistoSecondoPezzoTre").ToString
                    propValorePrevistoSecondoQuattroStampa = datareader("ValorePrevistoSecondoPezzoQuattro").ToString
                    propValorePrevistoSecondoCinqueStampa = datareader("ValorePrevistoSecondoPezzoCinque").ToString
                    propValorePrevistoSecondoSeiStampa = datareader("ValorePrevistoSecondoPezzoSei").ToString
                    propValorePrevistoSecondoSetteStampa = datareader("ValorePrevistoSecondoPezzoSette").ToString
                    propValorePrevistoSecondoOttoStampa = datareader("ValorePrevistoSecondoPezzoOtto").ToString
                    propValorePrevistoSecondoNoveStampa = datareader("ValorePrevistoSecondoPezzoNove").ToString
                    propValorePrevistoSecondoDieciStampa = datareader("ValorePrevistoSecondoPezzoDieci").ToString

                    propValoreMisuratoSecondoUnoStampa = datareader("ValoreMisuratoSecondoPezzoUno").ToString
                    propValoreMisuratoSecondoDueStampa = datareader("ValoreMisuratoSecondoPezzoDue").ToString
                    propValoreMisuratoSecondoTreStampa = datareader("ValoreMisuratoSecondoPezzoTre").ToString
                    propValoreMisuratoSecondoQuattroStampa = datareader("ValoreMisuratoSecondoPezzoQuattro").ToString
                    propValoreMisuratoSecondoCinqueStampa = datareader("ValoreMisuratoSecondoPezzoCinque").ToString
                    propValoreMisuratoSecondoSeiStampa = datareader("ValoreMisuratoSecondoPezzoSei").ToString
                    propValoreMisuratoSecondoSetteStampa = datareader("ValoreMisuratoSecondoPezzoSette").ToString
                    propValoreMisuratoSecondoOttoStampa = datareader("ValoreMisuratoSecondoPezzoOtto").ToString
                    propValoreMisuratoSecondoNoveStampa = datareader("ValoreMisuratoSecondoPezzoNove").ToString
                    propValoreMisuratoSecondoDieciStampa = datareader("ValoreMisuratoSecondoPezzoDieci").ToString

                    propTolleranzaPiupropValorePrevistoSecondoUnoStampa = datareader("TolleranzaPiuValorePrevistoSecondoUno").ToString
                    propTolleranzaPiupropValorePrevistoSecondoDueStampa = datareader("TolleranzaPiuValorePrevistoSecondoDue").ToString
                    propTolleranzaPiupropValorePrevistoSecondoTreStampa = datareader("TolleranzaPiuValorePrevistoSecondoTre").ToString
                    propTolleranzaPiupropValorePrevistoSecondoQuattroStampa = datareader("TolleranzaPiuValorePrevistoSecondoQuattro").ToString
                    propTolleranzaPiupropValorePrevistoSecondoCinqueStampa = datareader("TolleranzaPiuValorePrevistoSecondoCinque").ToString
                    propTolleranzaPiupropValorePrevistoSecondoSeiStampa = datareader("TolleranzaPiuValorePrevistoSecondoSei").ToString
                    propTolleranzaPiupropValorePrevistoSecondoSetteStampa = datareader("TolleranzaPiuValorePrevistoSecondoSette").ToString
                    propTolleranzaPiupropValorePrevistoSecondoOttoStampa = datareader("TolleranzaPiuValorePrevistoSecondoOtto").ToString
                    propTolleranzaPiupropValorePrevistoSecondoNoveStampa = datareader("TolleranzaPiuValorePrevistoSecondoNove").ToString
                    propTolleranzaPiupropValorePrevistoSecondoDieciStampa = datareader("TolleranzaPiuValorePrevistoSecondoDieci").ToString

                    propTolleranzaMenopropValorePrevistoSecondoUnoStampa = datareader("TolleranzaMenoValorePrevistoSecondoUno").ToString
                    propTolleranzaMenopropValorePrevistoSecondoDueStampa = datareader("TolleranzaMenoValorePrevistoSecondoDue").ToString
                    propTolleranzaMenopropValorePrevistoSecondoTreStampa = datareader("TolleranzaMenoValorePrevistoSecondoTre").ToString
                    propTolleranzaMenopropValorePrevistoSecondoQuattroStampa = datareader("TolleranzaMenoValorePrevistoSecondoQuattro").ToString
                    propTolleranzaMenopropValorePrevistoSecondoCinqueStampa = datareader("TolleranzaMenoValorePrevistoSecondoCinque").ToString
                    propTolleranzaMenopropValorePrevistoSecondoSeiStampa = datareader("TolleranzaMenoValorePrevistoSecondoSei").ToString
                    propTolleranzaMenopropValorePrevistoSecondoSetteStampa = datareader("TolleranzaMenoValorePrevistoSecondoSette").ToString
                    propTolleranzaMenopropValorePrevistoSecondoOttoStampa = datareader("TolleranzaMenoValorePrevistoSecondoOtto").ToString
                    propTolleranzaMenopropValorePrevistoSecondoNoveStampa = datareader("TolleranzaMenoValorePrevistoSecondoNove").ToString
                    propTolleranzaMenopropValorePrevistoSecondoDieciStampa = datareader("TolleranzaMenoValorePrevistoSecondoDieci").ToString

                    propNoteSecondoPezzoStampa = datareader("NoteSecondoPezzo").ToString

                End While


            End Using

            Return rowsSecondo

        Catch ex As Exception
            MessageBox.Show("Errore getSecondoPezzoByIdIntestazioneStampa : " & ex.Message)
        End Try

    End Function



    Public Function SalvaSecondoPezzoStampa() As Integer

        Try
            Dim intContatoreSecPezzo As Integer = propIdIntestazioneStampa

            Dim IDPezziSecondoPezzo As Integer = intContatoreSecPezzo
            Dim IDIntestazione As Integer = intContatoreSecPezzo

            Dim ValorePrevistoSecondoPezzoUno As String = propValorePrevistoSecondoUnoStampa
            Dim ValorePrevistoSecondoPezzoDue As String = propValorePrevistoSecondoDueStampa
            Dim ValorePrevistoSecondoPezzoTre As String = propValorePrevistoSecondoTreStampa
            Dim ValorePrevistoSecondoPezzoQuattro As String = propValorePrevistoSecondoQuattroStampa
            Dim ValorePrevistoSecondoPezzoCinque As String = propValorePrevistoSecondoCinqueStampa
            Dim ValorePrevistoSecondoPezzoSei As String = propValorePrevistoSecondoSeiStampa
            Dim ValorePrevistoSecondoPezzoSette As String = propValorePrevistoSecondoSetteStampa
            Dim ValorePrevistoSecondoPezzoOtto As String = propValorePrevistoSecondoOttoStampa
            Dim ValorePrevistoSecondoPezzoNove As String = propValorePrevistoSecondoNoveStampa
            Dim ValorePrevistoSecondoPezzoDieci As String = propValorePrevistoSecondoNoveStampa

            Dim ValoreMisuratoSecondoPezzoUno As String = propValoreMisuratoSecondoUnoStampa
            Dim ValoreMisuratoSecondoPezzoDue As String = propValoreMisuratoSecondoDueStampa
            Dim ValoreMisuratoSecondoPezzoTre As String = propValoreMisuratoSecondoTreStampa
            Dim ValoreMisuratoSecondoPezzoQuattro As String = propValoreMisuratoSecondoQuattroStampa
            Dim ValoreMisuratoSecondoPezzoCinque As String = propValoreMisuratoSecondoCinqueStampa
            Dim ValoreMisuratoSecondoPezzoSei As String = propValoreMisuratoSecondoSeiStampa
            Dim ValoreMisuratoSecondoPezzoSette As String = propValoreMisuratoSecondoSetteStampa
            Dim ValoreMisuratoSecondoPezzoOtto As String = propValoreMisuratoSecondoOttoStampa
            Dim ValoreMisuratoSecondoPezzoNove As String = propValoreMisuratoSecondoNoveStampa
            Dim ValoreMisuratoSecondoPezzoDieci As String = propValoreMisuratoSecondoDieciStampa

            Dim TolleranzaPiuValorePrevistoSecondoUno As String = propTolleranzaPiupropValorePrevistoSecondoUnoStampa
            Dim TolleranzaPiuValorePrevistoSecondoDue As String = propTolleranzaPiupropValorePrevistoSecondoDueStampa
            Dim TolleranzaPiuValorePrevistoSecondoTre As String = propTolleranzaPiupropValorePrevistoSecondoTreStampa
            Dim TolleranzaPiuValorePrevistoSecondoQuattro As String = propTolleranzaPiupropValorePrevistoSecondoQuattroStampa
            Dim TolleranzaPiuValorePrevistoSecondoCinque As String = propTolleranzaPiupropValorePrevistoSecondoCinqueStampa
            Dim TolleranzaPiuValorePrevistoSecondoSei As String = propTolleranzaPiupropValorePrevistoSecondoSeiStampa
            Dim TolleranzaPiuValorePrevistoSecondoSette As String = propTolleranzaPiupropValorePrevistoSecondoSetteStampa
            Dim TolleranzaPiuValorePrevistoSecondoOtto As String = propTolleranzaPiupropValorePrevistoSecondoOttoStampa
            Dim TolleranzaPiuValorePrevistoSecondoNove As String = propTolleranzaPiupropValorePrevistoSecondoNoveStampa
            Dim TolleranzaPiuValorePrevistoSecondoDieci As String = propTolleranzaPiupropValorePrevistoSecondoDieciStampa

            Dim TolleranzaMenoValorePrevistoSecondoUno As String = propTolleranzaMenopropValorePrevistoSecondoUnoStampa
            Dim TolleranzaMenoValorePrevistoSecondoDue As String = propTolleranzaMenopropValorePrevistoSecondoDueStampa
            Dim TolleranzaMenoValorePrevistoSecondoTre As String = propTolleranzaMenopropValorePrevistoSecondoTreStampa
            Dim TolleranzaMenoValorePrevistoSecondoQuattro As String = propTolleranzaMenopropValorePrevistoSecondoQuattroStampa
            Dim TolleranzaMenoValorePrevistoSecondoCinque As String = propTolleranzaMenopropValorePrevistoSecondoCinqueStampa
            Dim TolleranzaMenoValorePrevistoSecondoSei As String = propTolleranzaMenopropValorePrevistoSecondoSeiStampa
            Dim TolleranzaMenoValorePrevistoSecondoSette As String = propTolleranzaMenopropValorePrevistoSecondoSetteStampa
            Dim TolleranzaMenoValorePrevistoSecondoOtto As String = propTolleranzaMenopropValorePrevistoSecondoOttoStampa
            Dim TolleranzaMenoValorePrevistoSecondoNove As String = propTolleranzaMenopropValorePrevistoSecondoNoveStampa
            Dim TolleranzaMenoValorePrevistoSecondoDieci As String = propTolleranzaMenopropValorePrevistoSecondoDieciStampa

            Dim NoteSecondoPezzo As String = propNoteSecondoPezzoStampa

            Dim retSecondoPezzo As Nullable(Of Integer) = InsertPezziSecondoPezzoStampa(IDPezziSecondoPezzo, IDIntestazione, ValorePrevistoSecondoPezzoUno, ValorePrevistoSecondoPezzoDue, ValorePrevistoSecondoPezzoTre, ValorePrevistoSecondoPezzoQuattro,
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
            MessageBox.Show("Errore  SalvaSecondoPezzoStampa :" & ex.Message)
        End Try

    End Function

    Public Function InsertPezziSecondoPezzoStampa(IDPezziSecondoPezzo As Integer,
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

        Dim strQuery As String = "INSERT INTO [dbo].[tblPezziSecondoPezzoStampa]" &
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
            MessageBox.Show("Errore InsertPezziSecondoPezzoStampa : " & ex.Message)
        End Try

        Return rowsAffectedSecondoPezzo

    End Function


#End Region

#Region "Get e Insert Terzo Pezzo Per Stampa"


    Public Function getTerzoPezzoByIdIntestazioneStampa(intIdItestazione As Integer) As Integer

        Dim cmd As DbCommand = _db.GetStoredProcCommand("spGetTerzoPezzoByIdIntestazioneStampa")
        Dim rowsTerzo As Integer

        Try
            _db.AddInParameter(cmd, "IDIntestazione", DbType.Int32, intIdItestazione)

            Using datareader As IDataReader = _db.ExecuteReader(cmd)

                While datareader.Read

                    rowsTerzo += 1

                    propValorePrevistoTerzoUnoStampa = datareader("ValorePrevistoTerzoPezzoUno").ToString
                    propValorePrevistoTerzoDueStampa = datareader("ValorePrevistoTerzoPezzoDue").ToString
                    propValorePrevistoTerzoTreStampa = datareader("ValorePrevistoTerzoPezzoTre").ToString
                    propValorePrevistoTerzoQuattroStampa = datareader("ValorePrevistoTerzoPezzoQuattro").ToString
                    propValorePrevistoTerzoCinqueStampa = datareader("ValorePrevistoTerzoPezzoCinque").ToString
                    propValorePrevistoTerzoSeiStampa = datareader("ValorePrevistoTerzoPezzoSei").ToString
                    propValorePrevistoTerzoSetteStampa = datareader("ValorePrevistoTerzoPezzoSette").ToString
                    propValorePrevistoTerzoOttoStampa = datareader("ValorePrevistoTerzoPezzoOtto").ToString
                    propValorePrevistoTerzoNoveStampa = datareader("ValorePrevistoTerzoPezzoNove").ToString
                    propValorePrevistoTerzoDieciStampa = datareader("ValorePrevistoTerzoPezzoDieci").ToString

                    propValoreMisuratoTerzoUnoStampa = datareader("ValoreMisuratoTerzoPezzoUno").ToString
                    propValoreMisuratoTerzoDueStampa = datareader("ValoreMisuratoTerzoPezzoDue").ToString
                    propValoreMisuratoTerzoTreStampa = datareader("ValoreMisuratoTerzoPezzoTre").ToString
                    propValoreMisuratoTerzoQuattroStampa = datareader("ValoreMisuratoTerzoPezzoQuattro").ToString
                    propValoreMisuratoTerzoCinqueStampa = datareader("ValoreMisuratoTerzoPezzoCinque").ToString
                    propValoreMisuratoTerzoSeiStampa = datareader("ValoreMisuratoTerzoPezzoSei").ToString
                    propValoreMisuratoTerzoSetteStampa = datareader("ValoreMisuratoTerzoPezzoSette").ToString
                    propValoreMisuratoTerzoOttoStampa = datareader("ValoreMisuratoTerzoPezzoOtto").ToString
                    propValoreMisuratoTerzoNoveStampa = datareader("ValoreMisuratoTerzoPezzoNove").ToString
                    propValoreMisuratoTerzoDieciStampa = datareader("ValoreMisuratoTerzoPezzoDieci").ToString

                    propTolleranzaPiupropValorePrevistoTerzoUnoStampa = datareader("TolleranzaPiuValorePrevistoTerzoUno").ToString
                    propTolleranzaPiupropValorePrevistoTerzoDueStampa = datareader("TolleranzaPiuValorePrevistoTerzoDue").ToString
                    propTolleranzaPiupropValorePrevistoTerzoTreStampa = datareader("TolleranzaPiuValorePrevistoTerzoTre").ToString
                    propTolleranzaPiupropValorePrevistoTerzoQuattroStampa = datareader("TolleranzaPiuValorePrevistoTerzoQuattro").ToString
                    propTolleranzaPiupropValorePrevistoTerzoCinqueStampa = datareader("TolleranzaPiuValorePrevistoTerzoCinque").ToString
                    propTolleranzaPiupropValorePrevistoTerzoSeiStampa = datareader("TolleranzaPiuValorePrevistoTerzoSei").ToString
                    propTolleranzaPiupropValorePrevistoTerzoSetteStampa = datareader("TolleranzaPiuValorePrevistoTerzoSette").ToString
                    propTolleranzaPiupropValorePrevistoTerzoOttoStampa = datareader("TolleranzaPiuValorePrevistoTerzoOtto").ToString
                    propTolleranzaPiupropValorePrevistoTerzoNoveStampa = datareader("TolleranzaPiuValorePrevistoTerzoNove").ToString
                    propTolleranzaPiupropValorePrevistoTerzoDieciStampa = datareader("TolleranzaPiuValorePrevistoTerzoDieci").ToString

                    propTolleranzaMenopropValorePrevistoTerzoUnoStampa = datareader("TolleranzaMenoValorePrevistoTerzoUno").ToString
                    propTolleranzaMenopropValorePrevistoTerzoDueStampa = datareader("TolleranzaMenoValorePrevistoTerzoDue").ToString
                    propTolleranzaMenopropValorePrevistoTerzoTreStampa = datareader("TolleranzaMenoValorePrevistoTerzoTre").ToString
                    propTolleranzaMenopropValorePrevistoTerzoQuattroStampa = datareader("TolleranzaMenoValorePrevistoTerzoQuattro").ToString
                    propTolleranzaMenopropValorePrevistoTerzoCinqueStampa = datareader("TolleranzaMenoValorePrevistoTerzoCinque").ToString
                    propTolleranzaMenopropValorePrevistoTerzoSeiStampa = datareader("TolleranzaMenoValorePrevistoTerzoSei").ToString
                    propTolleranzaMenopropValorePrevistoTerzoSetteStampa = datareader("TolleranzaMenoValorePrevistoTerzoSette").ToString
                    propTolleranzaMenopropValorePrevistoTerzoOttoStampa = datareader("TolleranzaMenoValorePrevistoTerzoOtto").ToString
                    propTolleranzaMenopropValorePrevistoTerzoNoveStampa = datareader("TolleranzaMenoValorePrevistoTerzoNove").ToString
                    propTolleranzaMenopropValorePrevistoTerzoDieciStampa = datareader("TolleranzaMenoValorePrevistoTerzoDieci").ToString

                    propNoteTerzoPezzoStampa = datareader("NoteTerzoPezzo").ToString

                End While

            End Using

            Return rowsTerzo

        Catch ex As Exception
            MessageBox.Show("Errore getTerzoPezzoByIdIntestazioneStampa : " & ex.Message)
        End Try


    End Function



    Public Function SalvaTerzoPezzoStampa() As Integer

        Try
            Dim intContatoreSecPezzo As Integer = propIdIntestazioneStampa

            Dim IDPezziTerzoPezzo As Integer = intContatoreSecPezzo
            Dim IDIntestazione As Integer = intContatoreSecPezzo

            Dim ValorePrevistoTerzoPezzoUno As String = propValorePrevistoTerzoUnoStampa
            Dim ValorePrevistoTerzoPezzoDue As String = propValorePrevistoTerzoDueStampa
            Dim ValorePrevistoTerzoPezzoTre As String = propValorePrevistoTerzoTreStampa
            Dim ValorePrevistoTerzoPezzoQuattro As String = propValorePrevistoTerzoQuattroStampa
            Dim ValorePrevistoTerzoPezzoCinque As String = propValorePrevistoTerzoCinqueStampa
            Dim ValorePrevistoTerzoPezzoSei As String = propValorePrevistoTerzoSeiStampa
            Dim ValorePrevistoTerzoPezzoSette As String = propValorePrevistoTerzoSetteStampa
            Dim ValorePrevistoTerzoPezzoOtto As String = propValorePrevistoTerzoOttoStampa
            Dim ValorePrevistoTerzoPezzoNove As String = propValorePrevistoTerzoNoveStampa
            Dim ValorePrevistoTerzoPezzoDieci As String = propValorePrevistoTerzoNoveStampa

            Dim ValoreMisuratoTerzoPezzoUno As String = propValoreMisuratoTerzoUnoStampa
            Dim ValoreMisuratoTerzoPezzoDue As String = propValoreMisuratoTerzoDueStampa
            Dim ValoreMisuratoTerzoPezzoTre As String = propValoreMisuratoTerzoTreStampa
            Dim ValoreMisuratoTerzoPezzoQuattro As String = propValoreMisuratoTerzoQuattroStampa
            Dim ValoreMisuratoTerzoPezzoCinque As String = propValoreMisuratoTerzoCinqueStampa
            Dim ValoreMisuratoTerzoPezzoSei As String = propValoreMisuratoTerzoSeiStampa
            Dim ValoreMisuratoTerzoPezzoSette As String = propValoreMisuratoTerzoSetteStampa
            Dim ValoreMisuratoTerzoPezzoOtto As String = propValoreMisuratoTerzoOttoStampa
            Dim ValoreMisuratoTerzoPezzoNove As String = propValoreMisuratoTerzoNoveStampa
            Dim ValoreMisuratoTerzoPezzoDieci As String = propValoreMisuratoTerzoDieciStampa

            Dim TolleranzaPiuValorePrevistoTerzoUno As String = propTolleranzaPiupropValorePrevistoTerzoUnoStampa
            Dim TolleranzaPiuValorePrevistoTerzoDue As String = propTolleranzaPiupropValorePrevistoTerzoDueStampa
            Dim TolleranzaPiuValorePrevistoTerzoTre As String = propTolleranzaPiupropValorePrevistoTerzoTreStampa
            Dim TolleranzaPiuValorePrevistoTerzoQuattro As String = propTolleranzaPiupropValorePrevistoTerzoQuattroStampa
            Dim TolleranzaPiuValorePrevistoTerzoCinque As String = propTolleranzaPiupropValorePrevistoTerzoCinqueStampa
            Dim TolleranzaPiuValorePrevistoTerzoSei As String = propTolleranzaPiupropValorePrevistoTerzoSeiStampa
            Dim TolleranzaPiuValorePrevistoTerzoSette As String = propTolleranzaPiupropValorePrevistoTerzoSetteStampa
            Dim TolleranzaPiuValorePrevistoTerzoOtto As String = propTolleranzaPiupropValorePrevistoTerzoOttoStampa
            Dim TolleranzaPiuValorePrevistoTerzoNove As String = propTolleranzaPiupropValorePrevistoTerzoNoveStampa
            Dim TolleranzaPiuValorePrevistoTerzoDieci As String = propTolleranzaPiupropValorePrevistoTerzoDieciStampa

            Dim TolleranzaMenoValorePrevistoTerzoUno As String = propTolleranzaMenopropValorePrevistoTerzoUnoStampa
            Dim TolleranzaMenoValorePrevistoTerzoDue As String = propTolleranzaMenopropValorePrevistoTerzoDueStampa
            Dim TolleranzaMenoValorePrevistoTerzoTre As String = propTolleranzaMenopropValorePrevistoTerzoTreStampa
            Dim TolleranzaMenoValorePrevistoTerzoQuattro As String = propTolleranzaMenopropValorePrevistoTerzoQuattroStampa
            Dim TolleranzaMenoValorePrevistoTerzoCinque As String = propTolleranzaMenopropValorePrevistoTerzoCinqueStampa
            Dim TolleranzaMenoValorePrevistoTerzoSei As String = propTolleranzaMenopropValorePrevistoTerzoSeiStampa
            Dim TolleranzaMenoValorePrevistoTerzoSette As String = propTolleranzaMenopropValorePrevistoTerzoSetteStampa
            Dim TolleranzaMenoValorePrevistoTerzoOtto As String = propTolleranzaMenopropValorePrevistoTerzoOttoStampa
            Dim TolleranzaMenoValorePrevistoTerzoNove As String = propTolleranzaMenopropValorePrevistoTerzoNoveStampa
            Dim TolleranzaMenoValorePrevistoTerzoDieci As String = propTolleranzaMenopropValorePrevistoTerzoDieciStampa

            Dim NoteTerzoPezzo As String = propNoteTerzoPezzoStampa

            Dim retTerzoPezzo As Nullable(Of Integer) = InsertPezziTerzoPezzoStampa(IDPezziTerzoPezzo, IDIntestazione, ValorePrevistoTerzoPezzoUno, ValorePrevistoTerzoPezzoDue, ValorePrevistoTerzoPezzoTre, ValorePrevistoTerzoPezzoQuattro,
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
            MessageBox.Show("Errore  SalvaTerzoPezzoStampa :" & ex.Message)
        End Try

    End Function

    Public Function InsertPezziTerzoPezzoStampa(IDPezziTerzoPezzo As Integer,
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

        Dim strQuery As String = "INSERT INTO [dbo].[tblPezziTerzoPezzoStampa]" &
        "([IDPezziTerzoPezzo],[IDIntestazione],[ValorePrevistoTerzoPezzoUno], " &
        "[ValorePrevistoTerzoPezzoDue],[ValorePrevistoTerzoPezzoTre], [ValorePrevistoTerzoPezzoQuattro], " &
        "[ValorePrevistoTerzoPezzoCinque]," &
        "[ValorePrevistoTerzoPezzoSei], " &
        "[ValorePrevistoTerzoPezzoSette], " &
        "[ValorePrevistoTerzoPezzoOtto], " &
        "[ValorePrevistoTerzoPezzoNove], " &
        "[ValorePrevistoTerzoPezzoDieci], " &
        "[ValoreMisuratoTerzoPezzoUno], " &
        "[ValoreMisuratoTerzoPezzoDue], " &
        "[ValoreMisuratoTerzoPezzoTre], " &
        "[ValoreMisuratoTerzoPezzoQuattro], " &
        "[ValoreMisuratoTerzoPezzoCinque], " &
        "[ValoreMisuratoTerzoPezzoSei], " &
        "[ValoreMisuratoTerzoPezzoSette], " &
        "[ValoreMisuratoTerzoPezzoOtto], " &
        "[ValoreMisuratoTerzoPezzoNove], " &
        "[ValoreMisuratoTerzoPezzoDieci], " &
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
            MessageBox.Show("Errore InsertPezziTerzoPezzoStampa : " & ex.Message)
        End Try

        Return rowsAffectedTerzoPezzo

    End Function

#End Region

#Region "Get e Insert Quarto Pezzo Per Stampa"


    Public Function getQuartoPezzoByIdIntestazioneStampa(intIdItestazione As Integer) As Integer

        Dim cmd As DbCommand = _db.GetStoredProcCommand("spGetQuartoPezzoByIdIntestazioneStampa")
        Dim rowsQuarto As Integer

        Try
            _db.AddInParameter(cmd, "IDIntestazione", DbType.Int32, intIdItestazione)

            Using datareader As IDataReader = _db.ExecuteReader(cmd)

                While datareader.Read

                    rowsQuarto += 1

                    propValorePrevistoQuartoUnoStampa = datareader("ValorePrevistoQuartoPezzoUno").ToString
                    propValorePrevistoQuartoDueStampa = datareader("ValorePrevistoQuartoPezzoDue").ToString
                    propValorePrevistoQuartoTreStampa = datareader("ValorePrevistoQuartoPezzoTre").ToString
                    propValorePrevistoQuartoQuattroStampa = datareader("ValorePrevistoQuartoPezzoQuattro").ToString
                    propValorePrevistoQuartoCinqueStampa = datareader("ValorePrevistoQuartoPezzoCinque").ToString
                    propValorePrevistoQuartoSeiStampa = datareader("ValorePrevistoQuartoPezzoSei").ToString
                    propValorePrevistoQuartoSetteStampa = datareader("ValorePrevistoQuartoPezzoSette").ToString
                    propValorePrevistoQuartoOttoStampa = datareader("ValorePrevistoQuartoPezzoOtto").ToString
                    propValorePrevistoQuartoNoveStampa = datareader("ValorePrevistoQuartoPezzoNove").ToString
                    propValorePrevistoQuartoDieciStampa = datareader("ValorePrevistoQuartoPezzoDieci").ToString

                    propValoreMisuratoQuartoUnoStampa = datareader("ValoreMisuratoQuartoPezzoUno").ToString
                    propValoreMisuratoQuartoDueStampa = datareader("ValoreMisuratoQuartoPezzoDue").ToString
                    propValoreMisuratoQuartoTreStampa = datareader("ValoreMisuratoQuartoPezzoTre").ToString
                    propValoreMisuratoQuartoQuattroStampa = datareader("ValoreMisuratoQuartoPezzoQuattro").ToString
                    propValoreMisuratoQuartoCinqueStampa = datareader("ValoreMisuratoQuartoPezzoCinque").ToString
                    propValoreMisuratoQuartoSeiStampa = datareader("ValoreMisuratoQuartoPezzoSei").ToString
                    propValoreMisuratoQuartoSetteStampa = datareader("ValoreMisuratoQuartoPezzoSette").ToString
                    propValoreMisuratoQuartoOttoStampa = datareader("ValoreMisuratoQuartoPezzoOtto").ToString
                    propValoreMisuratoQuartoNoveStampa = datareader("ValoreMisuratoQuartoPezzoNove").ToString
                    propValoreMisuratoQuartoDieciStampa = datareader("ValoreMisuratoQuartoPezzoDieci").ToString

                    propTolleranzaPiupropValorePrevistoQuartoUnoStampa = datareader("TolleranzaPiuValorePrevistoQuartoUno").ToString
                    propTolleranzaPiupropValorePrevistoQuartoDueStampa = datareader("TolleranzaPiuValorePrevistoQuartoDue").ToString
                    propTolleranzaPiupropValorePrevistoQuartoTreStampa = datareader("TolleranzaPiuValorePrevistoQuartoTre").ToString
                    propTolleranzaPiupropValorePrevistoQuartoQuattroStampa = datareader("TolleranzaPiuValorePrevistoQuartoQuattro").ToString
                    propTolleranzaPiupropValorePrevistoQuartoCinqueStampa = datareader("TolleranzaPiuValorePrevistoQuartoCinque").ToString
                    propTolleranzaPiupropValorePrevistoQuartoSeiStampa = datareader("TolleranzaPiuValorePrevistoQuartoSei").ToString
                    propTolleranzaPiupropValorePrevistoQuartoSetteStampa = datareader("TolleranzaPiuValorePrevistoQuartoSette").ToString
                    propTolleranzaPiupropValorePrevistoQuartoOttoStampa = datareader("TolleranzaPiuValorePrevistoQuartoOtto").ToString
                    propTolleranzaPiupropValorePrevistoQuartoNoveStampa = datareader("TolleranzaPiuValorePrevistoQuartoNove").ToString
                    propTolleranzaPiupropValorePrevistoQuartoDieciStampa = datareader("TolleranzaPiuValorePrevistoQuartoDieci").ToString

                    propTolleranzaMenopropValorePrevistoQuartoUnoStampa = datareader("TolleranzaMenoValorePrevistoQuartoUno").ToString
                    propTolleranzaMenopropValorePrevistoQuartoDueStampa = datareader("TolleranzaMenoValorePrevistoQuartoDue").ToString
                    propTolleranzaMenopropValorePrevistoQuartoTreStampa = datareader("TolleranzaMenoValorePrevistoQuartoTre").ToString
                    propTolleranzaMenopropValorePrevistoQuartoQuattroStampa = datareader("TolleranzaMenoValorePrevistoQuartoQuattro").ToString
                    propTolleranzaMenopropValorePrevistoQuartoCinqueStampa = datareader("TolleranzaMenoValorePrevistoQuartoCinque").ToString
                    propTolleranzaMenopropValorePrevistoQuartoSeiStampa = datareader("TolleranzaMenoValorePrevistoQuartoSei").ToString
                    propTolleranzaMenopropValorePrevistoQuartoSetteStampa = datareader("TolleranzaMenoValorePrevistoQuartoSette").ToString
                    propTolleranzaMenopropValorePrevistoQuartoOttoStampa = datareader("TolleranzaMenoValorePrevistoQuartoOtto").ToString
                    propTolleranzaMenopropValorePrevistoQuartoNoveStampa = datareader("TolleranzaMenoValorePrevistoQuartoNove").ToString
                    propTolleranzaMenopropValorePrevistoQuartoDieciStampa = datareader("TolleranzaMenoValorePrevistoQuartoDieci").ToString

                    propNoteQuartoPezzoStampa = datareader("NoteQuartoPezzo").ToString

                End While

            End Using

            Return rowsQuarto

        Catch ex As Exception
            MessageBox.Show("Errore getQuartoPezzoByIdIntestazioneStampa : " & ex.Message)
        End Try


    End Function


    Public Function SalvaQuartoPezzoStampa() As Integer

        Try
            Dim intContatoreSecPezzo As Integer = propIdIntestazioneStampa

            Dim IDPezziQuartoPezzo As Integer = intContatoreSecPezzo
            Dim IDIntestazione As Integer = intContatoreSecPezzo

            Dim ValorePrevistoQuartoPezzoUno As String = propValorePrevistoQuartoUnoStampa
            Dim ValorePrevistoQuartoPezzoDue As String = propValorePrevistoQuartoDueStampa
            Dim ValorePrevistoQuartoPezzoTre As String = propValorePrevistoQuartoTreStampa
            Dim ValorePrevistoQuartoPezzoQuattro As String = propValorePrevistoQuartoQuattroStampa
            Dim ValorePrevistoQuartoPezzoCinque As String = propValorePrevistoQuartoCinqueStampa
            Dim ValorePrevistoQuartoPezzoSei As String = propValorePrevistoQuartoSeiStampa
            Dim ValorePrevistoQuartoPezzoSette As String = propValorePrevistoQuartoSetteStampa
            Dim ValorePrevistoQuartoPezzoOtto As String = propValorePrevistoQuartoOttoStampa
            Dim ValorePrevistoQuartoPezzoNove As String = propValorePrevistoQuartoNoveStampa
            Dim ValorePrevistoQuartoPezzoDieci As String = propValorePrevistoQuartoNoveStampa

            Dim ValoreMisuratoQuartoPezzoUno As String = propValoreMisuratoQuartoUnoStampa
            Dim ValoreMisuratoQuartoPezzoDue As String = propValoreMisuratoQuartoDueStampa
            Dim ValoreMisuratoQuartoPezzoTre As String = propValoreMisuratoQuartoTreStampa
            Dim ValoreMisuratoQuartoPezzoQuattro As String = propValoreMisuratoQuartoQuattroStampa
            Dim ValoreMisuratoQuartoPezzoCinque As String = propValoreMisuratoQuartoCinqueStampa
            Dim ValoreMisuratoQuartoPezzoSei As String = propValoreMisuratoQuartoSeiStampa
            Dim ValoreMisuratoQuartoPezzoSette As String = propValoreMisuratoQuartoSetteStampa
            Dim ValoreMisuratoQuartoPezzoOtto As String = propValoreMisuratoQuartoOttoStampa
            Dim ValoreMisuratoQuartoPezzoNove As String = propValoreMisuratoQuartoNoveStampa
            Dim ValoreMisuratoQuartoPezzoDieci As String = propValoreMisuratoQuartoDieciStampa

            Dim TolleranzaPiuValorePrevistoQuartoUno As String = propTolleranzaPiupropValorePrevistoQuartoUnoStampa
            Dim TolleranzaPiuValorePrevistoQuartoDue As String = propTolleranzaPiupropValorePrevistoQuartoDueStampa
            Dim TolleranzaPiuValorePrevistoQuartoTre As String = propTolleranzaPiupropValorePrevistoQuartoTreStampa
            Dim TolleranzaPiuValorePrevistoQuartoQuattro As String = propTolleranzaPiupropValorePrevistoQuartoQuattroStampa
            Dim TolleranzaPiuValorePrevistoQuartoCinque As String = propTolleranzaPiupropValorePrevistoQuartoCinqueStampa
            Dim TolleranzaPiuValorePrevistoQuartoSei As String = propTolleranzaPiupropValorePrevistoQuartoSeiStampa
            Dim TolleranzaPiuValorePrevistoQuartoSette As String = propTolleranzaPiupropValorePrevistoQuartoSetteStampa
            Dim TolleranzaPiuValorePrevistoQuartoOtto As String = propTolleranzaPiupropValorePrevistoQuartoOttoStampa
            Dim TolleranzaPiuValorePrevistoQuartoNove As String = propTolleranzaPiupropValorePrevistoQuartoNoveStampa
            Dim TolleranzaPiuValorePrevistoQuartoDieci As String = propTolleranzaPiupropValorePrevistoQuartoDieciStampa

            Dim TolleranzaMenoValorePrevistoQuartoUno As String = propTolleranzaMenopropValorePrevistoQuartoUnoStampa
            Dim TolleranzaMenoValorePrevistoQuartoDue As String = propTolleranzaMenopropValorePrevistoQuartoDueStampa
            Dim TolleranzaMenoValorePrevistoQuartoTre As String = propTolleranzaMenopropValorePrevistoQuartoTreStampa
            Dim TolleranzaMenoValorePrevistoQuartoQuattro As String = propTolleranzaMenopropValorePrevistoQuartoQuattroStampa
            Dim TolleranzaMenoValorePrevistoQuartoCinque As String = propTolleranzaMenopropValorePrevistoQuartoCinqueStampa
            Dim TolleranzaMenoValorePrevistoQuartoSei As String = propTolleranzaMenopropValorePrevistoQuartoSeiStampa
            Dim TolleranzaMenoValorePrevistoQuartoSette As String = propTolleranzaMenopropValorePrevistoQuartoSetteStampa
            Dim TolleranzaMenoValorePrevistoQuartoOtto As String = propTolleranzaMenopropValorePrevistoQuartoOttoStampa
            Dim TolleranzaMenoValorePrevistoQuartoNove As String = propTolleranzaMenopropValorePrevistoQuartoNoveStampa
            Dim TolleranzaMenoValorePrevistoQuartoDieci As String = propTolleranzaMenopropValorePrevistoQuartoDieciStampa

            Dim NoteQuartoPezzo As String = propNoteQuartoPezzoStampa

            Dim retQuartoPezzo As Nullable(Of Integer) = InsertPezziQuartoPezzoStampa(IDPezziQuartoPezzo, IDIntestazione, ValorePrevistoQuartoPezzoUno, ValorePrevistoQuartoPezzoDue, ValorePrevistoQuartoPezzoTre, ValorePrevistoQuartoPezzoQuattro,
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
            MessageBox.Show("Errore  SalvaQuartoPezzoStampa :" & ex.Message)
        End Try

    End Function

    Public Function InsertPezziQuartoPezzoStampa(IDPezziQuartoPezzo As Integer,
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

        Dim strQuery As String = "INSERT INTO [dbo].[tblPezziQuartoPezzoStampa]" &
        "([IDPezziQuartoPezzo],[IDIntestazione],[ValorePrevistoQuartoPezzoUno], " &
        "[ValorePrevistoQuartoPezzoDue],[ValorePrevistoQuartoPezzoTre], [ValorePrevistoQuartoPezzoQuattro], " &
        "[ValorePrevistoQuartoPezzoCinque]," &
        "[ValorePrevistoQuartoPezzoSei], " &
        "[ValorePrevistoQuartoPezzoSette], " &
        "[ValorePrevistoQuartoPezzoOtto], " &
        "[ValorePrevistoQuartoPezzoNove], " &
        "[ValorePrevistoQuartoPezzoDieci], " &
        "[ValoreMisuratoQuartoPezzoUno], " &
        "[ValoreMisuratoQuartoPezzoDue], " &
        "[ValoreMisuratoQuartoPezzoTre], " &
        "[ValoreMisuratoQuartoPezzoQuattro], " &
        "[ValoreMisuratoQuartoPezzoCinque], " &
        "[ValoreMisuratoQuartoPezzoSei], " &
        "[ValoreMisuratoQuartoPezzoSette], " &
        "[ValoreMisuratoQuartoPezzoOtto], " &
        "[ValoreMisuratoQuartoPezzoNove], " &
        "[ValoreMisuratoQuartoPezzoDieci], " &
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
            MessageBox.Show("Errore InsertPezziQuartoPezzoStampa : " & ex.Message)
        End Try

        Return rowsAffectedQuartoPezzo

    End Function

#End Region

#Region "Get e Insert Quinto Pezzo Per Stampa"



    Public Function getQuintoPezzoByIdIntestazioneStampa(intIdItestazione As Integer) As Integer

        Dim cmd As DbCommand = _db.GetStoredProcCommand("spGetQuintoPezzoByIdIntestazioneStampa")
        Dim rowsQuinto As Integer

        Try
            _db.AddInParameter(cmd, "IDIntestazione", DbType.Int32, intIdItestazione)

            Using datareader As IDataReader = _db.ExecuteReader(cmd)

                While datareader.Read

                    rowsQuinto += 1

                    propValorePrevistoQuintoUnoStampa = datareader("ValorePrevistoQuintoPezzoUno").ToString
                    propValorePrevistoQuintoDueStampa = datareader("ValorePrevistoQuintoPezzoDue").ToString
                    propValorePrevistoQuintoTreStampa = datareader("ValorePrevistoQuintoPezzoTre").ToString
                    propValorePrevistoQuintoQuattroStampa = datareader("ValorePrevistoQuintoPezzoQuattro").ToString
                    propValorePrevistoQuintoCinqueStampa = datareader("ValorePrevistoQuintoPezzoCinque").ToString
                    propValorePrevistoQuintoSeiStampa = datareader("ValorePrevistoQuintoPezzoSei").ToString
                    propValorePrevistoQuintoSetteStampa = datareader("ValorePrevistoQuintoPezzoSette").ToString
                    propValorePrevistoQuintoOttoStampa = datareader("ValorePrevistoQuintoPezzoOtto").ToString
                    propValorePrevistoQuintoNoveStampa = datareader("ValorePrevistoQuintoPezzoNove").ToString
                    propValorePrevistoQuintoDieciStampa = datareader("ValorePrevistoQuintoPezzoDieci").ToString

                    propValoreMisuratoQuintoUnoStampa = datareader("ValoreMisuratoQuintoPezzoUno").ToString
                    propValoreMisuratoQuintoDueStampa = datareader("ValoreMisuratoQuintoPezzoDue").ToString
                    propValoreMisuratoQuintoTreStampa = datareader("ValoreMisuratoQuintoPezzoTre").ToString
                    propValoreMisuratoQuintoQuattroStampa = datareader("ValoreMisuratoQuintoPezzoQuattro").ToString
                    propValoreMisuratoQuintoCinqueStampa = datareader("ValoreMisuratoQuintoPezzoCinque").ToString
                    propValoreMisuratoQuintoSeiStampa = datareader("ValoreMisuratoQuintoPezzoSei").ToString
                    propValoreMisuratoQuintoSetteStampa = datareader("ValoreMisuratoQuintoPezzoSette").ToString
                    propValoreMisuratoQuintoOttoStampa = datareader("ValoreMisuratoQuintoPezzoOtto").ToString
                    propValoreMisuratoQuintoNoveStampa = datareader("ValoreMisuratoQuintoPezzoNove").ToString
                    propValoreMisuratoQuintoDieciStampa = datareader("ValoreMisuratoQuintoPezzoDieci").ToString

                    propTolleranzaPiupropValorePrevistoQuintoUnoStampa = datareader("TolleranzaPiuValorePrevistoQuintoUno").ToString
                    propTolleranzaPiupropValorePrevistoQuintoDueStampa = datareader("TolleranzaPiuValorePrevistoQuintoDue").ToString
                    propTolleranzaPiupropValorePrevistoQuintoTreStampa = datareader("TolleranzaPiuValorePrevistoQuintoTre").ToString
                    propTolleranzaPiupropValorePrevistoQuintoQuattroStampa = datareader("TolleranzaPiuValorePrevistoQuintoQuattro").ToString
                    propTolleranzaPiupropValorePrevistoQuintoCinqueStampa = datareader("TolleranzaPiuValorePrevistoQuintoCinque").ToString
                    propTolleranzaPiupropValorePrevistoQuintoSeiStampa = datareader("TolleranzaPiuValorePrevistoQuintoSei").ToString
                    propTolleranzaPiupropValorePrevistoQuintoSetteStampa = datareader("TolleranzaPiuValorePrevistoQuintoSette").ToString
                    propTolleranzaPiupropValorePrevistoQuintoOttoStampa = datareader("TolleranzaPiuValorePrevistoQuintoOtto").ToString
                    propTolleranzaPiupropValorePrevistoQuintoNoveStampa = datareader("TolleranzaPiuValorePrevistoQuintoNove").ToString
                    propTolleranzaPiupropValorePrevistoQuintoDieciStampa = datareader("TolleranzaPiuValorePrevistoQuintoDieci").ToString

                    propTolleranzaMenopropValorePrevistoQuintoUnoStampa = datareader("TolleranzaMenoValorePrevistoQuintoUno").ToString
                    propTolleranzaMenopropValorePrevistoQuintoDueStampa = datareader("TolleranzaMenoValorePrevistoQuintoDue").ToString
                    propTolleranzaMenopropValorePrevistoQuintoTreStampa = datareader("TolleranzaMenoValorePrevistoQuintoTre").ToString
                    propTolleranzaMenopropValorePrevistoQuintoQuattroStampa = datareader("TolleranzaMenoValorePrevistoQuintoQuattro").ToString
                    propTolleranzaMenopropValorePrevistoQuintoCinqueStampa = datareader("TolleranzaMenoValorePrevistoQuintoCinque").ToString
                    propTolleranzaMenopropValorePrevistoQuintoSeiStampa = datareader("TolleranzaMenoValorePrevistoQuintoSei").ToString
                    propTolleranzaMenopropValorePrevistoQuintoSetteStampa = datareader("TolleranzaMenoValorePrevistoQuintoSette").ToString
                    propTolleranzaMenopropValorePrevistoQuintoOttoStampa = datareader("TolleranzaMenoValorePrevistoQuintoOtto").ToString
                    propTolleranzaMenopropValorePrevistoQuintoNoveStampa = datareader("TolleranzaMenoValorePrevistoQuintoNove").ToString
                    propTolleranzaMenopropValorePrevistoQuintoDieciStampa = datareader("TolleranzaMenoValorePrevistoQuintoDieci").ToString

                    propNoteQuintoPezzoStampa = datareader("NoteQuintoPezzo").ToString

                End While

            End Using

            Return rowsQuinto

        Catch ex As Exception
            MessageBox.Show("Errore getQuintoPezzoByIdIntestazioneStampa : " & ex.Message)
        End Try


    End Function


    Public Function SalvaQuintoPezzoStampa() As Integer

        Try
            Dim intContatoreSecPezzo As Integer = propIdIntestazioneStampa

            Dim IDPezziQuintoPezzo As Integer = intContatoreSecPezzo
            Dim IDIntestazione As Integer = intContatoreSecPezzo

            Dim ValorePrevistoQuintoPezzoUno As String = propValorePrevistoQuintoUnoStampa
            Dim ValorePrevistoQuintoPezzoDue As String = propValorePrevistoQuintoDueStampa
            Dim ValorePrevistoQuintoPezzoTre As String = propValorePrevistoQuintoTreStampa
            Dim ValorePrevistoQuintoPezzoQuattro As String = propValorePrevistoQuintoQuattroStampa
            Dim ValorePrevistoQuintoPezzoCinque As String = propValorePrevistoQuintoCinqueStampa
            Dim ValorePrevistoQuintoPezzoSei As String = propValorePrevistoQuintoSeiStampa
            Dim ValorePrevistoQuintoPezzoSette As String = propValorePrevistoQuintoSetteStampa
            Dim ValorePrevistoQuintoPezzoOtto As String = propValorePrevistoQuintoOttoStampa
            Dim ValorePrevistoQuintoPezzoNove As String = propValorePrevistoQuintoNoveStampa
            Dim ValorePrevistoQuintoPezzoDieci As String = propValorePrevistoQuintoNoveStampa

            Dim ValoreMisuratoQuintoPezzoUno As String = propValoreMisuratoQuintoUnoStampa
            Dim ValoreMisuratoQuintoPezzoDue As String = propValoreMisuratoQuintoDueStampa
            Dim ValoreMisuratoQuintoPezzoTre As String = propValoreMisuratoQuintoTreStampa
            Dim ValoreMisuratoQuintoPezzoQuattro As String = propValoreMisuratoQuintoQuattroStampa
            Dim ValoreMisuratoQuintoPezzoCinque As String = propValoreMisuratoQuintoCinqueStampa
            Dim ValoreMisuratoQuintoPezzoSei As String = propValoreMisuratoQuintoSeiStampa
            Dim ValoreMisuratoQuintoPezzoSette As String = propValoreMisuratoQuintoSetteStampa
            Dim ValoreMisuratoQuintoPezzoOtto As String = propValoreMisuratoQuintoOttoStampa
            Dim ValoreMisuratoQuintoPezzoNove As String = propValoreMisuratoQuintoNoveStampa
            Dim ValoreMisuratoQuintoPezzoDieci As String = propValoreMisuratoQuintoDieciStampa

            Dim TolleranzaPiuValorePrevistoQuintoUno As String = propTolleranzaPiupropValorePrevistoQuintoUnoStampa
            Dim TolleranzaPiuValorePrevistoQuintoDue As String = propTolleranzaPiupropValorePrevistoQuintoDueStampa
            Dim TolleranzaPiuValorePrevistoQuintoTre As String = propTolleranzaPiupropValorePrevistoQuintoTreStampa
            Dim TolleranzaPiuValorePrevistoQuintoQuattro As String = propTolleranzaPiupropValorePrevistoQuintoQuattroStampa
            Dim TolleranzaPiuValorePrevistoQuintoCinque As String = propTolleranzaPiupropValorePrevistoQuintoCinqueStampa
            Dim TolleranzaPiuValorePrevistoQuintoSei As String = propTolleranzaPiupropValorePrevistoQuintoSeiStampa
            Dim TolleranzaPiuValorePrevistoQuintoSette As String = propTolleranzaPiupropValorePrevistoQuintoSetteStampa
            Dim TolleranzaPiuValorePrevistoQuintoOtto As String = propTolleranzaPiupropValorePrevistoQuintoOttoStampa
            Dim TolleranzaPiuValorePrevistoQuintoNove As String = propTolleranzaPiupropValorePrevistoQuintoNoveStampa
            Dim TolleranzaPiuValorePrevistoQuintoDieci As String = propTolleranzaPiupropValorePrevistoQuintoDieciStampa

            Dim TolleranzaMenoValorePrevistoQuintoUno As String = propTolleranzaMenopropValorePrevistoQuintoUnoStampa
            Dim TolleranzaMenoValorePrevistoQuintoDue As String = propTolleranzaMenopropValorePrevistoQuintoDueStampa
            Dim TolleranzaMenoValorePrevistoQuintoTre As String = propTolleranzaMenopropValorePrevistoQuintoTreStampa
            Dim TolleranzaMenoValorePrevistoQuintoQuattro As String = propTolleranzaMenopropValorePrevistoQuintoQuattroStampa
            Dim TolleranzaMenoValorePrevistoQuintoCinque As String = propTolleranzaMenopropValorePrevistoQuintoCinqueStampa
            Dim TolleranzaMenoValorePrevistoQuintoSei As String = propTolleranzaMenopropValorePrevistoQuintoSeiStampa
            Dim TolleranzaMenoValorePrevistoQuintoSette As String = propTolleranzaMenopropValorePrevistoQuintoSetteStampa
            Dim TolleranzaMenoValorePrevistoQuintoOtto As String = propTolleranzaMenopropValorePrevistoQuintoOttoStampa
            Dim TolleranzaMenoValorePrevistoQuintoNove As String = propTolleranzaMenopropValorePrevistoQuintoNoveStampa
            Dim TolleranzaMenoValorePrevistoQuintoDieci As String = propTolleranzaMenopropValorePrevistoQuintoDieciStampa

            Dim NoteQuintoPezzo As String = propNoteQuintoPezzoStampa

            Dim retQuintoPezzo As Nullable(Of Integer) = InsertPezziQuintoPezzoStampa(IDPezziQuintoPezzo, IDIntestazione, ValorePrevistoQuintoPezzoUno, ValorePrevistoQuintoPezzoDue, ValorePrevistoQuintoPezzoTre, ValorePrevistoQuintoPezzoQuattro,
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
            MessageBox.Show("Errore  SalvaQuintoPezzoStampa :" & ex.Message)
        End Try

    End Function

    Public Function InsertPezziQuintoPezzoStampa(IDPezziQuintoPezzo As Integer,
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

        Dim strQuery As String = "INSERT INTO [dbo].[tblPezziQuintoPezzoStampa]" &
        "([IDPezziQuintoPezzo],[IDIntestazione],[ValorePrevistoQuintoPezzoUno], " &
        "[ValorePrevistoQuintoPezzoDue],[ValorePrevistoQuintoPezzoTre], [ValorePrevistoQuintoPezzoQuattro], " &
        "[ValorePrevistoQuintoPezzoCinque]," &
        "[ValorePrevistoQuintoPezzoSei], " &
        "[ValorePrevistoQuintoPezzoSette], " &
        "[ValorePrevistoQuintoPezzoOtto], " &
        "[ValorePrevistoQuintoPezzoNove], " &
        "[ValorePrevistoQuintoPezzoDieci], " &
        "[ValoreMisuratoQuintoPezzoUno], " &
        "[ValoreMisuratoQuintoPezzoDue], " &
        "[ValoreMisuratoQuintoPezzoTre], " &
        "[ValoreMisuratoQuintoPezzoQuattro], " &
        "[ValoreMisuratoQuintoPezzoCinque], " &
        "[ValoreMisuratoQuintoPezzoSei], " &
        "[ValoreMisuratoQuintoPezzoSette], " &
        "[ValoreMisuratoQuintoPezzoOtto], " &
        "[ValoreMisuratoQuintoPezzoNove], " &
        "[ValoreMisuratoQuintoPezzoDieci], " &
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
        "[NoteQuintoPezzo]) VALUES (@IDPezziQuintoPezzo,@IDIntestazione, " &
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
            MessageBox.Show("Errore InsertPezziQuintoPezzoStampa : " & ex.Message)
        End Try

        Return rowsAffectedQuintoPezzo

    End Function

#End Region

#Region "Get e Insert Ultimo Pezzo Per Stampa"


    Public Function getUltimoPezzoByIdIntestazioneStampa(intIdItestazione As Integer) As Integer

        Dim cmd As DbCommand = _db.GetStoredProcCommand("spGetUltimoPezzoByIdIntestazioneStampa")
        Dim rowsUltimo As Integer

        Try
            _db.AddInParameter(cmd, "IDIntestazione", DbType.Int32, intIdItestazione)

            Using datareader As IDataReader = _db.ExecuteReader(cmd)

                While datareader.Read

                    rowsUltimo += 1

                    propValorePrevistoUltimoUnoStampa = datareader("ValorePrevistoUltimoPezzoUno").ToString
                    propValorePrevistoUltimoDueStampa = datareader("ValorePrevistoUltimoPezzoDue").ToString
                    propValorePrevistoUltimoTreStampa = datareader("ValorePrevistoUltimoPezzoTre").ToString
                    propValorePrevistoUltimoQuattroStampa = datareader("ValorePrevistoUltimoPezzoQuattro").ToString
                    propValorePrevistoUltimoCinqueStampa = datareader("ValorePrevistoUltimoPezzoCinque").ToString
                    propValorePrevistoUltimoSeiStampa = datareader("ValorePrevistoUltimoPezzoSei").ToString
                    propValorePrevistoUltimoSetteStampa = datareader("ValorePrevistoUltimoPezzoSette").ToString
                    propValorePrevistoUltimoOttoStampa = datareader("ValorePrevistoUltimoPezzoOtto").ToString
                    propValorePrevistoUltimoNoveStampa = datareader("ValorePrevistoUltimoPezzoNove").ToString
                    propValorePrevistoUltimoDieciStampa = datareader("ValorePrevistoUltimoPezzoDieci").ToString

                    propValoreMisuratoUltimoUnoStampa = datareader("ValoreMisuratoUltimoPezzoUno").ToString
                    propValoreMisuratoUltimoDueStampa = datareader("ValoreMisuratoUltimoPezzoDue").ToString
                    propValoreMisuratoUltimoTreStampa = datareader("ValoreMisuratoUltimoPezzoTre").ToString
                    propValoreMisuratoUltimoQuattroStampa = datareader("ValoreMisuratoUltimoPezzoQuattro").ToString
                    propValoreMisuratoUltimoCinqueStampa = datareader("ValoreMisuratoUltimoPezzoCinque").ToString
                    propValoreMisuratoUltimoSeiStampa = datareader("ValoreMisuratoUltimoPezzoSei").ToString
                    propValoreMisuratoUltimoSetteStampa = datareader("ValoreMisuratoUltimoPezzoSette").ToString
                    propValoreMisuratoUltimoOttoStampa = datareader("ValoreMisuratoUltimoPezzoOtto").ToString
                    propValoreMisuratoUltimoNoveStampa = datareader("ValoreMisuratoUltimoPezzoNove").ToString
                    propValoreMisuratoUltimoDieciStampa = datareader("ValoreMisuratoUltimoPezzoDieci").ToString

                    propTolleranzaPiupropValorePrevistoUltimoUnoStampa = datareader("TolleranzaPiuValorePrevistoUltimoUno").ToString
                    propTolleranzaPiupropValorePrevistoUltimoDueStampa = datareader("TolleranzaPiuValorePrevistoUltimoDue").ToString
                    propTolleranzaPiupropValorePrevistoUltimoTreStampa = datareader("TolleranzaPiuValorePrevistoUltimoTre").ToString
                    propTolleranzaPiupropValorePrevistoUltimoQuattroStampa = datareader("TolleranzaPiuValorePrevistoUltimoQuattro").ToString
                    propTolleranzaPiupropValorePrevistoUltimoCinqueStampa = datareader("TolleranzaPiuValorePrevistoUltimoCinque").ToString
                    propTolleranzaPiupropValorePrevistoUltimoSeiStampa = datareader("TolleranzaPiuValorePrevistoUltimoSei").ToString
                    propTolleranzaPiupropValorePrevistoUltimoSetteStampa = datareader("TolleranzaPiuValorePrevistoUltimoSette").ToString
                    propTolleranzaPiupropValorePrevistoUltimoOttoStampa = datareader("TolleranzaPiuValorePrevistoUltimoOtto").ToString
                    propTolleranzaPiupropValorePrevistoUltimoNoveStampa = datareader("TolleranzaPiuValorePrevistoUltimoNove").ToString
                    propTolleranzaPiupropValorePrevistoUltimoDieciStampa = datareader("TolleranzaPiuValorePrevistoUltimoDieci").ToString

                    propTolleranzaMenopropValorePrevistoUltimoUnoStampa = datareader("TolleranzaMenoValorePrevistoUltimoUno").ToString
                    propTolleranzaMenopropValorePrevistoUltimoDueStampa = datareader("TolleranzaMenoValorePrevistoUltimoDue").ToString
                    propTolleranzaMenopropValorePrevistoUltimoTreStampa = datareader("TolleranzaMenoValorePrevistoUltimoTre").ToString
                    propTolleranzaMenopropValorePrevistoUltimoQuattroStampa = datareader("TolleranzaMenoValorePrevistoUltimoQuattro").ToString
                    propTolleranzaMenopropValorePrevistoUltimoCinqueStampa = datareader("TolleranzaMenoValorePrevistoUltimoCinque").ToString
                    propTolleranzaMenopropValorePrevistoUltimoSeiStampa = datareader("TolleranzaMenoValorePrevistoUltimoSei").ToString
                    propTolleranzaMenopropValorePrevistoUltimoSetteStampa = datareader("TolleranzaMenoValorePrevistoUltimoSette").ToString
                    propTolleranzaMenopropValorePrevistoUltimoOttoStampa = datareader("TolleranzaMenoValorePrevistoUltimoOtto").ToString
                    propTolleranzaMenopropValorePrevistoUltimoNoveStampa = datareader("TolleranzaMenoValorePrevistoUltimoNove").ToString
                    propTolleranzaMenopropValorePrevistoUltimoDieciStampa = datareader("TolleranzaMenoValorePrevistoUltimoDieci").ToString

                    propNoteUltimoPezzoStampa = datareader("NoteUltimoPezzo").ToString

                End While

            End Using

            Return rowsUltimo

        Catch ex As Exception
            MessageBox.Show("Errore getUltimoPezzoByIdIntestazioneStampa : " & ex.Message)
        End Try


    End Function


    Public Function SalvaUltimoPezzoStampa() As Integer

        Try
            Dim intContatoreSecPezzo As Integer = propIdIntestazioneStampa

            Dim IDPezziUltimoPezzo As Integer = intContatoreSecPezzo
            Dim IDIntestazione As Integer = intContatoreSecPezzo

            Dim ValorePrevistoUltimoPezzoUno As String = propValorePrevistoUltimoUnoStampa
            Dim ValorePrevistoUltimoPezzoDue As String = propValorePrevistoUltimoDueStampa
            Dim ValorePrevistoUltimoPezzoTre As String = propValorePrevistoUltimoTreStampa
            Dim ValorePrevistoUltimoPezzoQuattro As String = propValorePrevistoUltimoQuattroStampa
            Dim ValorePrevistoUltimoPezzoCinque As String = propValorePrevistoUltimoCinqueStampa
            Dim ValorePrevistoUltimoPezzoSei As String = propValorePrevistoUltimoSeiStampa
            Dim ValorePrevistoUltimoPezzoSette As String = propValorePrevistoUltimoSetteStampa
            Dim ValorePrevistoUltimoPezzoOtto As String = propValorePrevistoUltimoOttoStampa
            Dim ValorePrevistoUltimoPezzoNove As String = propValorePrevistoUltimoNoveStampa
            Dim ValorePrevistoUltimoPezzoDieci As String = propValorePrevistoUltimoNoveStampa

            Dim ValoreMisuratoUltimoPezzoUno As String = propValoreMisuratoUltimoUnoStampa
            Dim ValoreMisuratoUltimoPezzoDue As String = propValoreMisuratoUltimoDueStampa
            Dim ValoreMisuratoUltimoPezzoTre As String = propValoreMisuratoUltimoTreStampa
            Dim ValoreMisuratoUltimoPezzoQuattro As String = propValoreMisuratoUltimoQuattroStampa
            Dim ValoreMisuratoUltimoPezzoCinque As String = propValoreMisuratoUltimoCinqueStampa
            Dim ValoreMisuratoUltimoPezzoSei As String = propValoreMisuratoUltimoSeiStampa
            Dim ValoreMisuratoUltimoPezzoSette As String = propValoreMisuratoUltimoSetteStampa
            Dim ValoreMisuratoUltimoPezzoOtto As String = propValoreMisuratoUltimoOttoStampa
            Dim ValoreMisuratoUltimoPezzoNove As String = propValoreMisuratoUltimoNoveStampa
            Dim ValoreMisuratoUltimoPezzoDieci As String = propValoreMisuratoUltimoDieciStampa

            Dim TolleranzaPiuValorePrevistoUltimoUno As String = propTolleranzaPiupropValorePrevistoUltimoUnoStampa
            Dim TolleranzaPiuValorePrevistoUltimoDue As String = propTolleranzaPiupropValorePrevistoUltimoDueStampa
            Dim TolleranzaPiuValorePrevistoUltimoTre As String = propTolleranzaPiupropValorePrevistoUltimoTreStampa
            Dim TolleranzaPiuValorePrevistoUltimoQuattro As String = propTolleranzaPiupropValorePrevistoUltimoQuattroStampa
            Dim TolleranzaPiuValorePrevistoUltimoCinque As String = propTolleranzaPiupropValorePrevistoUltimoCinqueStampa
            Dim TolleranzaPiuValorePrevistoUltimoSei As String = propTolleranzaPiupropValorePrevistoUltimoSeiStampa
            Dim TolleranzaPiuValorePrevistoUltimoSette As String = propTolleranzaPiupropValorePrevistoUltimoSetteStampa
            Dim TolleranzaPiuValorePrevistoUltimoOtto As String = propTolleranzaPiupropValorePrevistoUltimoOttoStampa
            Dim TolleranzaPiuValorePrevistoUltimoNove As String = propTolleranzaPiupropValorePrevistoUltimoNoveStampa
            Dim TolleranzaPiuValorePrevistoUltimoDieci As String = propTolleranzaPiupropValorePrevistoUltimoDieciStampa

            Dim TolleranzaMenoValorePrevistoUltimoUno As String = propTolleranzaMenopropValorePrevistoUltimoUnoStampa
            Dim TolleranzaMenoValorePrevistoUltimoDue As String = propTolleranzaMenopropValorePrevistoUltimoDueStampa
            Dim TolleranzaMenoValorePrevistoUltimoTre As String = propTolleranzaMenopropValorePrevistoUltimoTreStampa
            Dim TolleranzaMenoValorePrevistoUltimoQuattro As String = propTolleranzaMenopropValorePrevistoUltimoQuattroStampa
            Dim TolleranzaMenoValorePrevistoUltimoCinque As String = propTolleranzaMenopropValorePrevistoUltimoCinqueStampa
            Dim TolleranzaMenoValorePrevistoUltimoSei As String = propTolleranzaMenopropValorePrevistoUltimoSeiStampa
            Dim TolleranzaMenoValorePrevistoUltimoSette As String = propTolleranzaMenopropValorePrevistoUltimoSetteStampa
            Dim TolleranzaMenoValorePrevistoUltimoOtto As String = propTolleranzaMenopropValorePrevistoUltimoOttoStampa
            Dim TolleranzaMenoValorePrevistoUltimoNove As String = propTolleranzaMenopropValorePrevistoUltimoNoveStampa
            Dim TolleranzaMenoValorePrevistoUltimoDieci As String = propTolleranzaMenopropValorePrevistoUltimoDieciStampa

            Dim NoteUltimoPezzo As String = propNoteUltimoPezzoStampa

            Dim retUltimoPezzo As Nullable(Of Integer) = InsertPezziUltimoPezzoStampa(IDPezziUltimoPezzo, IDIntestazione, ValorePrevistoUltimoPezzoUno, ValorePrevistoUltimoPezzoDue, ValorePrevistoUltimoPezzoTre, ValorePrevistoUltimoPezzoQuattro,
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
            MessageBox.Show("Errore  SalvaUltimoPezzoStampa :" & ex.Message)
        End Try

    End Function

    Public Function InsertPezziUltimoPezzoStampa(IDPezziUltimoPezzo As Integer,
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

        Dim strQuery As String = "INSERT INTO [dbo].[tblPezziUltimoPezzoStampa]" &
        "([IDPezziUltimoPezzo],[IDIntestazione],[ValorePrevistoUltimoPezzoUno], " &
        "[ValorePrevistoUltimoPezzoDue],[ValorePrevistoUltimoPezzoTre], [ValorePrevistoUltimoPezzoQuattro], " &
        "[ValorePrevistoUltimoPezzoCinque]," &
        "[ValorePrevistoUltimoPezzoSei], " &
        "[ValorePrevistoUltimoPezzoSette], " &
        "[ValorePrevistoUltimoPezzoOtto], " &
        "[ValorePrevistoUltimoPezzoNove], " &
        "[ValorePrevistoUltimoPezzoDieci], " &
        "[ValoreMisuratoUltimoPezzoUno], " &
        "[ValoreMisuratoUltimoPezzoDue], " &
        "[ValoreMisuratoUltimoPezzoTre], " &
        "[ValoreMisuratoUltimoPezzoQuattro], " &
        "[ValoreMisuratoUltimoPezzoCinque], " &
        "[ValoreMisuratoUltimoPezzoSei], " &
        "[ValoreMisuratoUltimoPezzoSette], " &
        "[ValoreMisuratoUltimoPezzoOtto], " &
        "[ValoreMisuratoUltimoPezzoNove], " &
        "[ValoreMisuratoUltimoPezzoDieci], " &
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
            MessageBox.Show("Errore InsertPezziUltimoPezzoStampa : " & ex.Message)
        End Try

        Return rowsAffectedUltimoPezzo

    End Function




#End Region



#Region "Ripulisce da tabelle stampa"


    Public Sub EliminaDatiTestataStampa()
        Dim deleteCommand As DbCommand = Nothing
        Try
            deleteCommand = _db.GetStoredProcCommand("spDeleteIntestazioneStampa")
            _db.ExecuteNonQuery(deleteCommand)
        Catch ex As Exception
            MessageBox.Show("Errore cancellazione dati Testata per stampa : " & ex.Message)
        End Try
    End Sub

    Public Sub EliminaDatiPrimoPezzoStampa()
        Dim deleteCommand As DbCommand = Nothing
        Try
            deleteCommand = _db.GetStoredProcCommand("spDeletePezziPrimoPezzoStampa")
            _db.ExecuteNonQuery(deleteCommand)
        Catch ex As Exception
            MessageBox.Show("Errore cancellazione dati Primo Pezzo per stampa : " & ex.Message)
        End Try
    End Sub

    Public Sub EliminaDatiSecondoPezzoStampa()
        Dim deleteCommand As DbCommand = Nothing
        Try
            deleteCommand = _db.GetStoredProcCommand("spDeleteSecondoPezzoStampa")
            _db.ExecuteNonQuery(deleteCommand)
        Catch ex As Exception
            MessageBox.Show("Errore cancellazione dati Secondo Pezzo per stampa : " & ex.Message)
        End Try
    End Sub

    Public Sub EliminaDatiTerzoPezzoStampa()
        Dim deleteCommand As DbCommand = Nothing
        Try
            deleteCommand = _db.GetStoredProcCommand("spDeleteTerzoPezzoStampa")
            _db.ExecuteNonQuery(deleteCommand)
        Catch ex As Exception
            MessageBox.Show("Errore cancellazione dati Terzo Pezzo per stampa : " & ex.Message)
        End Try
    End Sub

    Public Sub EliminaDatiQuartoPezzoStampa()
        Dim deleteCommand As DbCommand = Nothing
        Try
            deleteCommand = _db.GetStoredProcCommand("spDeleteQuartoPezzoStampa")
            _db.ExecuteNonQuery(deleteCommand)
        Catch ex As Exception
            MessageBox.Show("Errore cancellazione dati Quarto Pezzo per stampa : " & ex.Message)
        End Try
    End Sub

    Public Sub EliminaDatiQuintoPezzoStampa()
        Dim deleteCommand As DbCommand = Nothing
        Try
            deleteCommand = _db.GetStoredProcCommand("spDeleteQuintoPezzoStampa")
            _db.ExecuteNonQuery(deleteCommand)
        Catch ex As Exception
            MessageBox.Show("Errore cancellazione dati Quinto Pezzo per stampa : " & ex.Message)
        End Try
    End Sub

    Public Sub EliminaDatiUltimoPezzoStampa()
        Dim deleteCommand As DbCommand = Nothing
        Try
            deleteCommand = _db.GetStoredProcCommand("spDeleteUltimoPezzoStampa")
            _db.ExecuteNonQuery(deleteCommand)
        Catch ex As Exception
            MessageBox.Show("Errore cancellazione dati Ultimo Pezzo per stampa : " & ex.Message)
        End Try
    End Sub


#End Region


#Region "Funzioni Utility"

    Public Function Converti(value As Date) As Date
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

    Private Sub btnOperatori_Click(sender As Object, e As EventArgs) Handles btnOperatori.Click
        Dim frmOperatori As New frmOperatori
        frmOperatori.Show()
    End Sub

#End Region



End Class
