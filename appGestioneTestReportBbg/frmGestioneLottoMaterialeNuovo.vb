Imports Telerik.WinControls.UI
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports Telerik.WinControls.UI.Localization

Public Class frmGestioneLottoMaterialeNuovo

#Region "Proprieta"
    Public Shared factory As New DatabaseProviderFactory()
    Public Property _db As Database = factory.Create("dbConnStrRete")
    Public Property indexRigaPerAggiorna As Integer

#End Region

    Private Sub frmGestioneLottoMaterialeNuovo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'DsNumeroLottoMateriale1.tblNumeroLottoMateriale' table. You can move, or remove it, as needed.
        TblNumeroLottoMaterialeTableAdapter.Fill(DsNumeroLottoMateriale1.tblNumeroLottoMateriale)
        ' PopolaGridLotto()
        popolaComboMateriale()
        popolaComboFornitori()
        RadGridLocalizationProvider.CurrentProvider = New MyItalianRadGridLocalizationProvider()
    End Sub

    Public Sub popolaComboMateriale()
        Dim cmdMat As DbCommand
        Me.cmbMateriali.Items.Clear()

        Dim strSqlSP As String = "QueryGetMateriali"
        cmdMat = _db.GetStoredProcCommand(strSqlSP)
        Using datareader As IDataReader = _db.ExecuteReader(cmdMat)
            While datareader.Read
                Me.cmbMateriali.Items.Add(datareader("materiale"))
            End While
        End Using
    End Sub

    Public Sub popolaComboFornitori()
        Dim cmdForn As DbCommand
        Me.cmbFornitore.Items.Clear()

        Dim strSql As String = "spGetFornitori"
        cmdForn = _db.GetStoredProcCommand(strSql)
        Using datareader As IDataReader = _db.ExecuteReader(cmdForn)
            While datareader.Read
                Me.cmbFornitore.Items.Add(datareader("NomeFornitore"))
            End While
        End Using
    End Sub

    Public Sub PopolaGridLotto()
        Try
            Dim cmd As DbCommand = _db.GetStoredProcCommand("spGetLottoMateriale")
            Using datareader As IDataReader = _db.ExecuteReader(cmd)
                gridLottoMateriale.MasterTemplate.LoadFrom(datareader)
            End Using
            Me.gridLottoMateriale.CurrentRow = Nothing
        Catch ex As Exception
            MessageBox.Show("Errore :" & ex.Message)
        End Try

    End Sub

    Private Function GetSafeString(ByVal value As Object) As String
        If value Is Nothing Then
            Return String.Empty
        End If
        Return value.ToString()
    End Function

    Private Sub btnChiudi_Click(sender As Object, e As EventArgs) Handles btnChiudi.Click
        Me.Close()
    End Sub

    Private Sub btnAggiungiLottoMateriale_Click(sender As Object, e As EventArgs) Handles btnAggiungiLottoMateriale.Click

        Dim materiale As String = Me.cmbMateriali.SelectedItem.ToString
        Dim numeroLotto As String = Me.txtNumeroLotto.Text
        Dim fornitore As String = Me.cmbFornitore.SelectedItem.ToString
        Dim numDDT As String = Me.txtNumeroDDT.Text

        Dim cmd As DbCommand

        Try
            cmd = _db.GetStoredProcCommand("spInsertNuovoLottoMateriale2")

            _db.AddInParameter(cmd, "materiale", DbType.String, materiale)
            _db.AddInParameter(cmd, "numeroLotto", DbType.String, numeroLotto)
            _db.AddInParameter(cmd, "fornitore", DbType.String, fornitore)
            _db.AddInParameter(cmd, "numDDT", DbType.String, numDDT)

            If (_db.ExecuteNonQuery(cmd) = 1) Then
                MessageBox.Show("Nuovo lotto materiale inserito in database.", "Nuovo Lotto", MessageBoxButtons.OK)
            End If

            'PopolaGridLotto()
            Me.TblNumeroLottoMaterialeTableAdapter.Fill(Me.DsNumeroLottoMateriale1.tblNumeroLottoMateriale)
            'Deseleziona righe
            Me.gridLottoMateriale.ClearSelection()
            'Conta ultima riga
            Dim lastRow As GridViewRowInfo = Me.gridLottoMateriale.Rows(gridLottoMateriale.Rows.Count - 1)
            'Casting Integer righe
            Dim intRowCount As Integer = lastRow.Index
            'Scroll su ultima riga
            Me.gridLottoMateriale.TableElement.ScrollToRow(intRowCount)
            'seleziona ultima riga
            lastRow.IsSelected = True

        Catch ex As Exception
            MessageBox.Show("Errore salvataggio nuovo lotto : " & ex.Message)
        Finally
            ' Me.cmbMateriali.SelectedIndex = -1
            ' Me.cmbFornitore.SelectedIndex = -1
            ' Me.txtNumeroLotto.Text = String.Empty
            ' Me.txtNumeroDDT.Text = String.Empty
        End Try

    End Sub

    Private Sub UpdatePanelInfo(ByVal currentRow As GridViewRowInfo)
        If currentRow IsNot Nothing AndAlso Not (TypeOf currentRow Is GridViewNewRowInfo) Then
            Me.txtAggiornaMateriale.Text = Me.GetSafeString(currentRow.Cells("materiale").Value)
            Me.txtAggiornaNumeroLotto.Text = Me.GetSafeString(currentRow.Cells("numeroLotto").Value)
            Me.txtAggiornaFornitore.Text = Me.GetSafeString(currentRow.Cells("fornitore").Value)
            Me.txtAggiornaNumeroDDT.Text = Me.GetSafeString(currentRow.Cells("numDDT").Value)
        Else
            Me.txtAggiornaMateriale.Text = String.Empty
            Me.txtAggiornaNumeroLotto.Text = String.Empty
            Me.txtAggiornaFornitore.Text = String.Empty
            Me.txtAggiornaNumeroDDT.Text = String.Empty
        End If
    End Sub

    Private Sub gridLottoMateriale_CellClick(sender As Object, e As GridViewCellEventArgs) Handles gridLottoMateriale.CellClick

        If TypeOf Me.gridLottoMateriale.CurrentRow Is GridViewDataRowInfo Then
            indexRigaPerAggiorna = Me.gridLottoMateriale.Rows.IndexOf(DirectCast(Me.gridLottoMateriale.CurrentRow, GridViewDataRowInfo))
        End If
    End Sub

    Private Sub gridLottoMateriale_CurrentRowChanged(sender As Object, e As CurrentRowChangedEventArgs) Handles gridLottoMateriale.CurrentRowChanged
        UpdatePanelInfo(Me.gridLottoMateriale.CurrentRow)
    End Sub

    Private Sub btnAggiorna_Click(sender As Object, e As EventArgs) Handles btnAggiorna.Click

        Dim intAggiornaLottoMaterialeBusLayer As Nullable(Of Integer)
        Dim result As DialogResult

        'Trova id selezionato
        propIdLottoMateriale = Me.gridLottoMateriale.Rows(Me.gridLottoMateriale.CurrentRow.Index).Cells(0).Value
        intAggiornaLottoMaterialeBusLayer = AggiornaLottoBusinessLayer(propIdLottoMateriale)

        If Not intAggiornaLottoMaterialeBusLayer Is Nothing Then
            result = MessageBox.Show("Lotto Materiale Aggiornato Con Successo", "Aggiornamento Lotto Materiale in Database", MessageBoxButtons.OKCancel)
            If result = DialogResult.Cancel Then
                Exit Sub
            End If
        End If

        'PopolaGridLotto()
        Me.TblNumeroLottoMaterialeTableAdapter.Fill(Me.DsNumeroLottoMateriale1.tblNumeroLottoMateriale)

        'Torna sulla riga selezionata
        Me.gridLottoMateriale.CurrentRow = gridLottoMateriale.Rows(indexRigaPerAggiorna)

    End Sub

    Public Function AggiornaLottoBusinessLayer(ByVal idRigaLotto As Integer)

        Dim ret As Nullable(Of Integer)

        Dim result As DialogResult

        Dim strFornitore As String
        Dim strMateriale As String
        Dim strNumeroLotto As String
        Dim strNumeroDDT As String
        Dim idRiga As Integer

        strFornitore = Me.txtAggiornaFornitore.Text
        strMateriale = Me.txtAggiornaMateriale.Text
        strNumeroDDT = Me.txtAggiornaNumeroDDT.Text
        strNumeroLotto = Me.txtAggiornaNumeroLotto.Text
        idRiga = idRigaLotto

        Try
            ret = AggiornaLottoMateriale(idRiga, strFornitore, strMateriale, strNumeroDDT, strNumeroLotto)

            If Not ret Is Nothing Then
                Return ret
            Else
                Exit Function
            End If

        Catch ex As Exception
            MessageBox.Show("Errore AggiornaLottoBusinessLayer:" & ex.Message)
        End Try

    End Function

    Public Function AggiornaLottoMateriale(ByVal id As Integer, ByVal fornitore As String, ByVal materiale As String, ByVal numeroDDT As String, ByVal numeroLotto As String)
        Dim rowsAffected As Integer
        Dim updateCommand As DbCommand = Nothing
        updateCommand = _db.GetStoredProcCommand("spUpdateLottoMateriale")
        Try

            _db.AddInParameter(updateCommand, "materiale", DbType.String, materiale)
            _db.AddInParameter(updateCommand, "numeroLotto", DbType.String, numeroLotto)
            _db.AddInParameter(updateCommand, "fornitore", DbType.String, fornitore)
            _db.AddInParameter(updateCommand, "numDDT", DbType.String, numeroDDT)
            _db.AddInParameter(updateCommand, "id", DbType.Int32, id)

            rowsAffected = _db.ExecuteNonQuery(updateCommand)

            Return rowsAffected

        Catch ex As Exception
            MessageBox.Show("Errore AggiornaLottoMateriale: " & ex.Message)
        End Try

    End Function

    Private Sub btnEliminaLotto_Click(sender As Object, e As EventArgs) Handles btnEliminaLotto.Click

        'Trova id selezionato con ChildRows
        propIdLottoMateriale = gridLottoMateriale.ChildRows(gridLottoMateriale.CurrentRow.Index).Cells(0).Value

        Dim result As DialogResult

        If Not (propIdLottoMateriale = Nothing) Then
            If gridLottoMateriale.SelectedRows.Count > 0 Then
                result = MessageBox.Show(" Sei sicuro di voler eliminare il Test Report N° : " & propIdLottoMateriale & " ? ", "Elimina test Report in Database", MessageBoxButtons.OKCancel)
                If result = DialogResult.Cancel Then
                    Exit Sub
                End If

                If result = Windows.Forms.DialogResult.OK Then
                    EliminaLottoMateriale(propIdLottoMateriale)

                    'Controllo se il filtro esiste ancora
                    If (gridLottoMateriale.IsInEditMode) Then
                        gridLottoMateriale.EndEdit()
                    End If

                    'Toglie il filtro
                    gridLottoMateriale.FilterDescriptors.Clear()

                    'PopolaGridLotto()
                    'Aggiorna il dataset binding con la grid
                    TblNumeroLottoMaterialeTableAdapter.Fill(DsNumeroLottoMateriale1.tblNumeroLottoMateriale)

                    'Riposiziona la griglia sulla riga dopo quella eliminata
                    gridLottoMateriale.CurrentRow = gridLottoMateriale.Rows(indexRigaPerAggiorna - 1)
                        gridLottoMateriale.CurrentRow.IsSelected = True
                    Else
                        Exit Sub
                End If
            Else
                MessageBox.Show("Seleziona una riga !!")
            End If

        Else

        End If

    End Sub

    Public Function EliminaLottoMateriale(ByVal id As Integer)
        Dim deleteCommand As DbCommand = Nothing
        Dim rowsAffected As Integer
        Try
            deleteCommand = _db.GetStoredProcCommand("spDeleteQueryLotto")
            _db.AddInParameter(deleteCommand, "id", DbType.Int32, id)
            rowsAffected = _db.ExecuteNonQuery(deleteCommand)
        Catch ex As Exception
            MessageBox.Show("Errore EliminaLottoMateriale : " & ex.Message)
        End Try
        Return rowsAffected
    End Function

    'Private Sub FilterDescriptors_CollectionChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.Data.NotifyCollectionChangedEventArgs)
    '    'Evento creato che restituisce variabile booleana se le righe sono state filtrate
    '    collChanged = True
    'End Sub

End Class
