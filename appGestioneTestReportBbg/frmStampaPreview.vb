Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class frmStampaPreview

#Region "Proprieta"
    Public Shared factory As New DatabaseProviderFactory()
    Public Property _db As Microsoft.Practices.EnterpriseLibrary.Data.Database = factory.Create("dbConnStrRete")
    Public Property cryRpt As New ReportDocument
#End Region

    Private Sub frmStampaPreview_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        EliminaDatiTestataStampa()
        EliminaDatiPrimoPezzoStampa()
        EliminaDatiSecondoPezzoStampa()
        EliminaDatiTerzoPezzoStampa()
        EliminaDatiQuartoPezzoStampa()
        EliminaDatiQuintoPezzoStampa()
        EliminaDatiUltimoPezzoStampa()

        If Not cryRpt Is Nothing Then
            cryRpt.Close()
            cryRpt.Dispose()
            GC.Collect()
        End If

    End Sub

    Private Sub frmStampaPreview_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim pathApp As String = Application.StartupPath
        Dim reportPath As String = pathApp.Replace("\bin\Debug", "\CrystalReportBBGNoParam.rpt")

        cryRpt.Load(reportPath)
        cryRpt.SetDatabaseLogon("sa", "tito")

        Dim myFOpts As Integer = (CrystalDecisions.Shared.ViewerExportFormats.PdfFormat + CrystalDecisions.Shared.ViewerExportFormats.XLSXFormat + CrystalDecisions.Shared.ViewerExportFormats.CsvFormat)
        Me.CrystalReportViewer1.ReportSource = cryRpt
        Me.CrystalReportViewer1.AllowedExportFormats = myFOpts
        cryRpt.SummaryInfo.ReportTitle = Replace(propCodiceArticoloNomeFilePDF, "/", "-")

        CrystalReportViewer1.Refresh()
        GC.Collect()

        'Dim crParameterFieldDefinitions As ParameterFieldDefinitions
        'Dim crParameterFieldDefinition As ParameterFieldDefinition
        'Dim crParameterValues As New ParameterValues
        'Dim crParameterDiscreteValue As New ParameterDiscreteValue

        'crParameterDiscreteValue.Value = Convert.ToInt32(propIdIntestazioneRicerca)
        'crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields
        'crParameterFieldDefinition = crParameterFieldDefinitions.Item("IDIntestazione")
        'crParameterValues = crParameterFieldDefinition.CurrentValues
        'crParameterValues.Clear()
        'crParameterValues.Add(crParameterDiscreteValue)
        'crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

    End Sub


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


End Class