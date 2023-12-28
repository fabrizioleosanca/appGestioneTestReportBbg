Imports System
Imports System.Drawing
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase
Imports Microsoft.Practices.EnterpriseLibrary.Common.Configuration
Imports System.Data.Common
Imports appGestioneTestReportBbg.dbGestTestReportDataSet
Imports System.Text
Imports System.Xml
Imports System.IO
Imports Telerik.Examples.WinControls.DataSources
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data



Public Class aggiungiStrumMatForn

#Region "Proprieta"
    Public Shared factory As New DatabaseProviderFactory()
    Public Property _db As Database = factory.Create("dbConnStrRete")

    Public Function Convert(ByVal value As Date) As Date
        Dim DateValue As DateTime = CType(value, DateTime)
        Return DateValue.ToShortDateString
    End Function

    Private Sub aggiungiStrumMatForn_Load(sender As Object, e As EventArgs) Handles Me.Load
        RiempiComboFornitori()
        RiempiComboStrumenti()
        RiempiComboMateriali()
        Me.ComboBoxMateriali.SelectedIndex = 0
        Me.ComboBoxFornitori.SelectedIndex = 0
        Me.ComboBoxStrumenti.SelectedIndex = 0
        Me.RadPageViewAggiungi.SelectedPage = RadPageViewPageAggFornitori

    End Sub

#End Region


#Region "GESTISCI MATERIALE"

    Public Sub RiempiComboMateriali()
        Me.ComboBoxMateriali.Items.Clear()
        Dim cmdMate As DbCommand
        Dim strSQLMate As String = "spSelezionaMaterialiTutto"
        cmdMate = _db.GetStoredProcCommand(strSQLMate)
        Using datareader As IDataReader = _db.ExecuteReader(cmdMate)
            While datareader.Read
                Me.ComboBoxMateriali.Items.Add(datareader("materiale"))
            End While
            Me.ComboBoxMateriali.Items.Insert(0, "Seleziona Materiale")
        End Using
    End Sub

    Public Function SalvaMaterialeBusLayer() As Integer
        Dim strMateriale As String
        Dim dataInserimento As Date
        Dim retMate As Nullable(Of Integer)
        Try
            strMateriale = Me.txtNomeMateriale.Text
            dataInserimento = Convert(DateTimePickerMateriali.Value)
            retMate = insertMateriale(strMateriale, dataInserimento)

            If Not retMate Is Nothing Then
                Return retMate
            End If

        Catch ex As Exception
            MessageBox.Show("Errore SalvaMaterialeBusLayer :" & ex.Message)
        End Try
    End Function

    Public Function insertMateriale(ByVal strMateriale As String, ByVal dtDataInserimento As Date) As Integer
        Dim insertCommand As DbCommand = Nothing
        Dim rowsAffected As Integer
        Dim dataShorth As Date = Convert(dtDataInserimento)
        Try
            insertCommand = _db.GetStoredProcCommand("spInsertMateriali")
            _db.AddInParameter(insertCommand, "materiale", DbType.String, strMateriale)
            _db.AddInParameter(insertCommand, "DataInserimento", DbType.Date, dataShorth)
            rowsAffected = _db.ExecuteNonQuery(insertCommand)
        Catch ex As Exception
            MessageBox.Show("Errore insertMateriale : " & ex.Message)
        End Try
        Return rowsAffected
    End Function

    Private Sub btnSalvaMateriali_Click(sender As Object, e As EventArgs) Handles btnSalvaMateriali.Click
        Dim intSalvaMaterialeBusLayer As Nullable(Of Integer)
        Dim result As DialogResult
        Try
            intSalvaMaterialeBusLayer = SalvaMaterialeBusLayer()
            If Not intSalvaMaterialeBusLayer Is Nothing Then
                result = MessageBox.Show("Materiale Aggiunto Con Successo", "Inserimento Materiale in Database", MessageBoxButtons.OKCancel)
                Me.ComboBoxMateriali.Items.Clear()
                RiempiComboMateriali()
                Me.txtNomeMateriale.Text = String.Empty
                If result = DialogResult.Cancel Then
                    Exit Sub
                End If
            Else
                Exit Sub
            End If

        Catch ex As Exception
            MessageBox.Show("Errore SalvaMateriale :" & ex.Message)
        End Try

    End Sub

    Public Function EliminaMaterialeByNomeMateriale(ByVal nomeMateriale As String) As Integer
        Dim deleteCommand As DbCommand = Nothing
        Dim rowsAffected As Integer
        Try
            deleteCommand = _db.GetStoredProcCommand("spDeleteMateriale")
            _db.AddInParameter(deleteCommand, "materiale", DbType.String, nomeMateriale)
            rowsAffected = _db.ExecuteNonQuery(deleteCommand)
        Catch ex As Exception
            MessageBox.Show("Errore EliminaMaterialeByNomeMateriale : " & ex.Message)
        End Try
    End Function

    Private Sub btnCancellaMateriali_Click(sender As Object, e As EventArgs) Handles btnCancellaMateriali.Click
        Dim nomeMateriale As String = Me.ComboBoxMateriali.Text
        Dim result As DialogResult
        If (Not nomeMateriale = Nothing) Then
            If Not (Me.ComboBoxMateriali.Text = String.Empty) Then
                result = MessageBox.Show(" Sei sicuro di voler eliminare il Materiale  : " & nomeMateriale & " ? ", "Elimina Materiale in Database", MessageBoxButtons.OKCancel)
                If result = DialogResult.Cancel Then
                    Exit Sub
                End If
                If result = Windows.Forms.DialogResult.OK Then
                    EliminaMaterialeByNomeMateriale(nomeMateriale)
                    RiempiComboMateriali()
                    If ComboBoxMateriali.Items.Count > 0 Then
                        ComboBoxMateriali.SelectedIndex = 0
                    End If
                End If
            End If
        End If
    End Sub

#End Region

#Region "GESTISCE STRUMENTI"

    Private Sub btnCancellaStrumento_Click(sender As Object, e As EventArgs) Handles btnCancellaStrumento.Click
        Dim nomeStrumento As String = Me.ComboBoxStrumenti.Text
        Dim result As DialogResult
        If (Not nomeStrumento = Nothing) Then
            If Not (Me.ComboBoxStrumenti.Text = String.Empty) Then
                result = MessageBox.Show(" Sei sicuro di voler eliminare lo Strumento  : " & nomeStrumento & " ? ", "Elimina Strumento in Database", MessageBoxButtons.OKCancel)
                If result = DialogResult.Cancel Then
                    Exit Sub
                End If
                If result = Windows.Forms.DialogResult.OK Then
                    EliminaStrumentoByNomeStrumento(nomeStrumento)
                    RiempiComboStrumenti()
                    If ComboBoxStrumenti.Items.Count > 0 Then
                        ComboBoxStrumenti.SelectedIndex = 0
                    End If
                End If
            End If
        End If
    End Sub

    Public Function EliminaStrumentoByNomeStrumento(ByVal nomeStrumento As String) As Integer
        Dim deleteCommand As DbCommand = Nothing
        Dim rowsAffected As Integer
        Try
            deleteCommand = _db.GetStoredProcCommand("spDeleteStrumenti")
            _db.AddInParameter(deleteCommand, "Strumenti", DbType.String, nomeStrumento)
            rowsAffected = _db.ExecuteNonQuery(deleteCommand)
        Catch ex As Exception
            MessageBox.Show("Errore EliminaStrumentoByNomeStrumento : " & ex.Message)
        End Try
    End Function

    Private Sub btnSalvaStrumenti_Click(sender As Object, e As EventArgs) Handles btnSalvaStrumenti.Click
        Dim intSalvaStrumentiBusLayer As Nullable(Of Integer)
        Dim result As DialogResult
        Try
            intSalvaStrumentiBusLayer = SalvaStrumentiBusLayer()
            If Not intSalvaStrumentiBusLayer Is Nothing Then
                result = MessageBox.Show("Strumento Aggiunto Con Successo", "Inserimento Strumenti in Database", MessageBoxButtons.OKCancel)
                Me.ComboBoxStrumenti.Items.Clear()
                RiempiComboStrumenti()
                Me.txtNomeStrumento.Text = String.Empty
                If result = DialogResult.Cancel Then
                    Exit Sub
                End If
            Else
                Exit Sub
            End If

        Catch ex As Exception
            MessageBox.Show("Errore SalvaStrumenti :" & ex.Message)
        End Try
    End Sub

    Public Sub RiempiComboStrumenti()
        Me.ComboBoxStrumenti.Items.Clear()
        Dim cmdStrum As DbCommand
        Dim strSQLStrum As String = "spSelezionaStrumentiTutto"
        cmdStrum = _db.GetStoredProcCommand(strSQLStrum)
        Using datareader As IDataReader = _db.ExecuteReader(cmdStrum)
            While datareader.Read
                Me.ComboBoxStrumenti.Items.Add(datareader("Strumenti"))
            End While
            Me.ComboBoxStrumenti.Items.Insert(0, "Seleziona Strumento")
        End Using
    End Sub


    Public Function SalvaStrumentiBusLayer() As Integer
        Dim strStrumenti As String
        Dim dataInserimento As Date
        Dim retStru As Nullable(Of Integer)
        Try
            strStrumenti = Me.txtNomeStrumento.Text
            dataInserimento = Convert(DateTimePickerStrumenti.Value)
            retStru = insertStrumento(strStrumenti, dataInserimento)

            If Not retStru Is Nothing Then
                Return retStru
            End If

        Catch ex As Exception
            MessageBox.Show("Errore SalvaStrumentiBusLayer :" & ex.Message)
        End Try
    End Function

    Public Function insertStrumento(ByVal strStrumento As String, ByVal dtDataInserimento As Date) As Integer
        Dim insertCommand As DbCommand = Nothing
        Dim rowsAffected As Integer
        Dim dataShorth As Date = Convert(dtDataInserimento)
        Try
            insertCommand = _db.GetStoredProcCommand("spInsertStrumenti")
            _db.AddInParameter(insertCommand, "Strumenti", DbType.String, strStrumento)
            _db.AddInParameter(insertCommand, "DataInserimento", DbType.Date, dataShorth)
            rowsAffected = _db.ExecuteNonQuery(insertCommand)
        Catch ex As Exception
            MessageBox.Show("Errore insertStrumento : " & ex.Message)
        End Try
        Return rowsAffected
    End Function

#End Region

#Region "GESTISCE FORNITORE"

    Private Sub btnSalvaFornitore_Click(sender As Object, e As EventArgs) Handles btnSalvaFornitore.Click
        Dim intSalvaFornitoreBusLayer As Nullable(Of Integer)
        Dim result As DialogResult
        Try
            intSalvaFornitoreBusLayer = SalvaFornitoreBusLayer()
            If Not intSalvaFornitoreBusLayer Is Nothing Then
                result = MessageBox.Show("Fornitore Aggiunto Con Successo", "Inserimento Fornitore in Database", MessageBoxButtons.OKCancel)
                Me.ComboBoxFornitori.Items.Clear()
                RiempiComboFornitori()
                Me.txtFornitore.Text = String.Empty
                If result = DialogResult.Cancel Then
                    Exit Sub
                End If
            Else
                Exit Sub
            End If

        Catch ex As Exception
            MessageBox.Show("Errore SalvaFornitore :" & ex.Message)
        End Try
    End Sub

    Public Function SalvaFornitoreBusLayer() As Integer
        Dim strFornitore As String
        Dim dataInserimento As Date
        Dim retForn As Nullable(Of Integer)

        Try
            strFornitore = Me.txtFornitore.Text
            dataInserimento = Convert(DateTimePickerFornitori.Value)
            retForn = insertFornitore(strFornitore, dataInserimento)

            If Not retForn Is Nothing Then
                Return retForn
            End If

        Catch ex As Exception
            MessageBox.Show("Errore SalvaFornitoreBusLayer :" & ex.Message)
        End Try

    End Function

    Public Function insertFornitore(ByVal strFornitore As String, ByVal dtDataInserimento As Date) As Integer
        Dim insertCommand As DbCommand = Nothing
        Dim rowsAffected As Integer
        Dim dataShorth As Date = Convert(dtDataInserimento)
        Try
            insertCommand = _db.GetStoredProcCommand("spInsertIntoFornitori")
            _db.AddInParameter(insertCommand, "NomeFornitore", DbType.String, strFornitore)
            _db.AddInParameter(insertCommand, "DataInserimento", DbType.Date, dataShorth)
            rowsAffected = _db.ExecuteNonQuery(insertCommand)
        Catch ex As Exception
            MessageBox.Show("Errore insertFornitore : " & ex.Message)
        End Try
        Return rowsAffected
    End Function

    Public Sub RiempiComboFornitori()
        Me.ComboBoxFornitori.Items.Clear()
        Dim cmdForn As DbCommand
        Dim strSQLForn As String = "spSelezionaFornitoriTutto"
        cmdForn = _db.GetStoredProcCommand(strSQLForn)
        Using datareader As IDataReader = _db.ExecuteReader(cmdForn)
            While datareader.Read
                Me.ComboBoxFornitori.Items.Add(datareader("NomeFornitore"))
            End While
            Me.ComboBoxFornitori.Items.Insert(0, "Seleziona Fornitore")
        End Using
    End Sub

    Private Sub btnCancellaFornitore_Click(sender As Object, e As EventArgs) Handles btnCancellaFornitore.Click
        Dim nomeFornitore As String = Me.ComboBoxFornitori.Text
        Dim result As DialogResult
        If (Not nomeFornitore = Nothing) Then
            If Not (Me.ComboBoxFornitori.Text = String.Empty) Then
                result = MessageBox.Show(" Sei sicuro di voler eliminare il Fornitore  : " & nomeFornitore & " ? ", "Elimina Fornitore in Database", MessageBoxButtons.OKCancel)
                If result = DialogResult.Cancel Then
                    Exit Sub
                End If
                If result = Windows.Forms.DialogResult.OK Then
                    EliminaFornitoreByNomeFornitore(nomeFornitore)
                    RiempiComboFornitori()
                    If ComboBoxFornitori.Items.Count > 0 Then
                        ComboBoxFornitori.SelectedIndex = 0
                    End If
                End If
            End If
        End If
    End Sub

    Public Function EliminaFornitoreByNomeFornitore(ByVal nomeFornitore As String) As Integer
        Dim deleteCommand As DbCommand = Nothing
        Dim rowsAffected As Integer
        Try
            deleteCommand = _db.GetStoredProcCommand("spDeleteFornitore")
            _db.AddInParameter(deleteCommand, "NomeFornitore", DbType.String, nomeFornitore)
            rowsAffected = _db.ExecuteNonQuery(deleteCommand)
        Catch ex As Exception
            MessageBox.Show("Errore EliminaFornitoreByNomeFornitore : " & ex.Message)
        End Try
    End Function

#End Region


 


End Class