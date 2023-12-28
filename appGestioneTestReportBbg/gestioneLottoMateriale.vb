Imports System.ComponentModel
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports System.IO
Imports System.Reflection
Imports Telerik.WinControls.Primitives
Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Collections
Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase
Imports appGestioneTestReportBbg.dbGestTestReportDataSet
Imports appGestioneTestReportBbg.dsNumeroLottoMateriale
Imports Microsoft.Practices.EnterpriseLibrary.Common.Configuration
Imports System.Data.Common
Imports System.Text
Imports System.Xml
Imports System.Globalization
Imports Telerik.WinControls.UI.Localization
Imports System.Drawing.Imaging

Public Class gestioneLottoMateriale

#Region "Proprieta"
    Public Shared factory As New DatabaseProviderFactory()
    Public Property _db As Database = factory.Create("dbConnStrRete")
#End Region

    Private Sub gestioneLottoMateriale_Load(sender As Object, e As EventArgs) Handles Me.Load
        'TODO: This line of code loads data into the 'DsNumeroLottoMateriale1.tblNumeroLottoMateriale' table. You can move, or remove it, as needed.
        Me.TblNumeroLottoMaterialeTableAdapter.Fill(Me.DsNumeroLottoMateriale1.tblNumeroLottoMateriale)
        RadGridLocalizationProvider.CurrentProvider = New MyItalianRadGridLocalizationProvider()
        Dim panelShapeTopRounded As New RoundRectShape(8)
        RadPanelTitoloDettagli.PanelElement.Shape = panelShapeTopRounded
        Dim panelShapeTopRounded2 As New RoundRectShape(8)
        RadPanel4.PanelElement.Shape = panelShapeTopRounded2
    End Sub

    Private Sub RadGridViewLottoMateriale_CurrentRowChanged(sender As Object, e As CurrentRowChangedEventArgs) Handles RadGridViewLottoMateriale.CurrentRowChanged
        UpdatePanelInfo(Me.RadGridViewLottoMateriale.CurrentRow)

    End Sub

    Private Sub RadGridViewLottoMateriale_RowsChanged(sender As Object, e As GridViewCollectionChangedEventArgs) Handles RadGridViewLottoMateriale.RowsChanged
        If e.Action = Telerik.WinControls.Data.NotifyCollectionChangedAction.Add Then
            TblNumeroLottoMaterialeTableAdapter.Update(Me.DsNumeroLottoMateriale1.tblNumeroLottoMateriale)
        End If
    End Sub


    Private Sub RadGridViewLottoMateriale_RowsChanging(sender As Object, e As GridViewCollectionChangingEventArgs) Handles RadGridViewLottoMateriale.RowsChanging
        If e.Action = Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove Then
            Dim dialogResult As DialogResult = MessageBox.Show("Vuoi veramente cancellare il Lotto Materiale?", "Attenzione", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
            If dialogResult <> dialogResult.OK Then
                e.Cancel = True
            End If
        End If
    End Sub

    Public Sub loadDataGrid()
        ' Dim strConnString As String = "Data Source=BACKUP-PC-BBG\SQLEXPRESS;Initial Catalog=dbGestTestReport;Persist Security Info=True;User ID=sa;Password=tito"
        Dim strConnString As String = "Data Source=UTENTEBBGSQL-PC\SQLEXPRESS;Initial Catalog=dbGestTestReport;Integrated Security=True"
        Dim connection As New SqlConnection(strConnString)
        Dim strQuery As String = "SELECT id, materiale, numeroLotto, fornitore, numDDT FROM tblNumeroLottoMateriale"
        Dim adapter As New SqlDataAdapter(strQuery, connection)
        Dim ds As New DataSet()

        Try
            connection.Open()
            adapter.Fill(ds, "tblNumeroLottoMateriale")
            connection.Close()
            RadGridViewLottoMateriale.DataSource = ds
            RadGridViewLottoMateriale.DataMember = "tblNumeroLottoMateriale"
        Catch ex As Exception
            MessageBox.Show("Errore loadDataGrid :" & ex.Message)
        End Try
    End Sub



    Public Function EliminaLotto(ByVal currentRow As GridViewRowInfo) As Integer
        Dim deleteCommand As DbCommand = Nothing
        Dim rowsAffected As Integer
        Dim id As Integer = RadGridViewLottoMateriale.ChildRows(RadGridViewLottoMateriale.CurrentRow.Index).Cells("IDTab").Value

        'Dim id As Integer = CType(currentRow.Cells("IDTab").Value, Integer)
        Try
            deleteCommand = _db.GetStoredProcCommand("spDeleteQueryLotto")
            _db.AddInParameter(deleteCommand, "ID", DbType.Int32, id)
            rowsAffected = _db.ExecuteNonQuery(deleteCommand)

        Catch ex As Exception
            MessageBox.Show("Errore EliminaLotto : " & ex.Message)
        End Try
        Return rowsAffected
    End Function

    Public Function AggiornaNumeroLotto(ByVal id As Integer, ByVal materiale As String, ByVal numeroLotto As String, _
                             ByVal fornitore As String, ByVal numDDT As String) As Integer

        Dim rowsAffected As Integer

        Dim updateCommand As DbCommand = Nothing
        updateCommand = _db.GetStoredProcCommand("spUpdateLottoMateriale")

        Try

            _db.AddParameter(updateCommand, "id", DbType.Int32, ParameterDirection.Input, "id", DataRowVersion.Current, id)
            _db.AddInParameter(updateCommand, "materiale", DbType.String, materiale)
            _db.AddInParameter(updateCommand, "numeroLotto", DbType.String, numeroLotto)
            _db.AddInParameter(updateCommand, "fornitore", DbType.String, fornitore)
            _db.AddInParameter(updateCommand, "numDDT", DbType.String, numDDT)

            rowsAffected = _db.ExecuteNonQuery(updateCommand)

        Catch ex As Exception
            MessageBox.Show("Errore Aggiorna Numero Lotto: " & ex.Message)
        End Try

        Return rowsAffected

    End Function

    Public Function AggiornaNumeroLottoBusinnesLayer(ByVal currentRow As GridViewRowInfo) As Integer

        Dim ret As Nullable(Of Integer)
        Dim Materiale As String = Me.txtMateriale.Text
        Dim NumeroLotto As String = Me.txtNumeroLotto.Text
        Dim Fornitore As String = Me.txtFornitore.Text
        Dim NumeroDDT As String = Me.txtNumeroDDT.Text

        Dim id As Integer = CType(currentRow.Cells("IDTab").Value, Integer)

        Try

            ret = AggiornaNumeroLotto(id, Materiale, NumeroLotto, Fornitore, NumeroDDT)

            If Not ret Is Nothing Then
                Return ret
            Else
                Exit Function
            End If

        Catch ex As Exception
            MessageBox.Show("Errore AggiornaNumeroLottoBusinnesLayer :" & ex.Message)
        End Try

    End Function


    Private Sub UpdatePanelInfo(ByVal currentRow As GridViewRowInfo)
        If currentRow IsNot Nothing AndAlso Not (TypeOf currentRow Is GridViewNewRowInfo) Then
            Me.txtMateriale.Text = Me.GetSafeString(currentRow.Cells("materiale").Value)
            Me.txtNumeroLotto.Text = Me.GetSafeString(currentRow.Cells("numeroLotto").Value)
            Me.txtFornitore.Text = Me.GetSafeString(currentRow.Cells("fornitore").Value)
            Me.txtNumeroDDT.Text = Me.GetSafeString(currentRow.Cells("numDDT").Value)
        Else
            Me.txtMateriale.Text = String.Empty
            Me.txtNumeroLotto.Text = String.Empty
            Me.txtFornitore.Text = String.Empty
            Me.txtNumeroDDT.Text = String.Empty
        End If
    End Sub

    Private Function GetSafeString(ByVal value As Object) As String
        If value Is Nothing Then
            Return String.Empty
        End If
        Return value.ToString()
    End Function

    Private Sub updateButton_Click(sender As Object, e As EventArgs) Handles updateButton.Click
        AggiornaNumeroLottoBusinnesLayer(Me.RadGridViewLottoMateriale.CurrentRow)
        loadDataGrid()
    End Sub

    Private Sub CommandBarButtonAggiungiLotto_Click(sender As Object, e As EventArgs) Handles CommandBarButtonAggiungiLotto.Click
        Me.RadGridViewLottoMateriale.AllowAddNewRow = Not Me.RadGridViewLottoMateriale.AllowAddNewRow
    End Sub

    Private Sub CommandBarButtonCancellaLotto_Click(sender As Object, e As EventArgs) Handles CommandBarButtonCancellaLotto.Click
        Dim result As DialogResult
        If Not Me.RadGridViewLottoMateriale.CurrentRow Is Nothing Then
            If Me.RadGridViewLottoMateriale.SelectedRows.Count > 0 Then
                result = MessageBox.Show(" Sei sicuro di voler eliminare il Lotto ? ", "Elimina Lotto in Database", MessageBoxButtons.OKCancel)
                If result = DialogResult.Cancel Then
                    Exit Sub
                End If
                If result = Windows.Forms.DialogResult.OK Then
                    EliminaLotto(Me.RadGridViewLottoMateriale.CurrentRow)
                End If
            End If
        End If

        loadDataGrid()
    End Sub

    Private Sub CommandBarButtonEsci_Click(sender As Object, e As EventArgs) Handles CommandBarButtonEsci.Click
        Me.Close()
    End Sub


    Private Sub RadGridViewLottoMateriale_UserAddedRow(sender As Object, e As GridViewRowEventArgs) Handles RadGridViewLottoMateriale.UserAddedRow
        Dim rows(e.Rows.Length - 1) As DataRow
        For i As Integer = 0 To e.Rows.Length - 1
            Dim dataRowView As DataRowView = TryCast(e.Rows(i).DataBoundItem, DataRowView)
            If dataRowView IsNot Nothing Then
                rows(i) = dataRowView.Row
            End If
        Next i
        Me.TblNumeroLottoMaterialeTableAdapter.Update(rows)
    End Sub


End Class