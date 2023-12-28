Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports System.IO
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Globalization
Imports Telerik.WinControls.UI.Localization
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data.OleDb

Public Class frmTestReportPdfAutomatico

#Region "Funzioni Utility"

    Public Function Converti(ByVal value As Date) As Date
        Dim DateValue As DateTime = CType(value, DateTime)
        Return DateValue.ToShortDateString
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

#Region "Proprieta"
    Public Shared factory As New DatabaseProviderFactory()
    Public Property _db As Microsoft.Practices.EnterpriseLibrary.Data.Database = factory.Create("dbConnStrRete")
    Private imgFile As String = ""
    Public Property cryRpt As New ReportDocument
#End Region


#Region "Eventi"

    Private Sub frmTestReportPdfAutomatico_Load(sender As Object, e As EventArgs) Handles Me.Load
        ToolStripStatusLabel1.Visible = False
        RadGridLocalizationProvider.CurrentProvider = New MyItalianRadGridLocalizationProvider()

    End Sub

    Private Sub cmdChiudi_Click(sender As Object, e As EventArgs) Handles cmdChiudi.Click
        Close()
    End Sub

#End Region


#Region "Importa File Excel"

    'TODO:Da qui
    Private Sub cmdImportaArticoli_Click(sender As Object, e As EventArgs) Handles cmdImportaArticoli.Click

        Dim conn As OleDbConnection
        Dim dtr As OleDbDataReader
        Dim dta As OleDbDataAdapter
        Dim cmd As OleDbCommand
        Dim dts As DataSet
        Dim excel As String
        Dim OpenFileDialog As New OpenFileDialog

        OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        OpenFileDialog.Filter = "All Files (*.*)|*.*|Excel files (*.xlsx)|*.xlsx|CSV Files (*.csv)|*.csv|XLS Files (*.xls)|*xls"

        If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim fi As New FileInfo(OpenFileDialog.FileName)
            Dim FileName As String = OpenFileDialog.FileName
            excel = fi.FullName
            Dim sexcelconnectionstring As String = CreateConnectionString(excel)
            ' conn = New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excel + ";Extended Properties=Excel 12.0;")
            conn = New OleDbConnection(sexcelconnectionstring)
            dta = New OleDbDataAdapter("Select * From [Sheet1$]", conn)
            dts = New DataSet
            dta.Fill(dts, "[Sheet1$]")
            DataGridView1.DataSource = dts
            DataGridView1.DataMember = "[Sheet1$]"
            conn.Close()
        End If

    End Sub

    Public Shared Function CreateConnectionString(ByVal ExcelFilePath As String) As String
        Dim result As String = String.Empty
        Try
            If ExcelFilePath.ToLower.EndsWith(".xlsm") Then

                result = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0;HDR=NO;IMEX=1""", ExcelFilePath)
            Else
                result = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}; Extended Properties=""Excel 8.0;HDR=NO;IMEX=1""", ExcelFilePath)
            End If
        Catch ex As Exception

        End Try
        Return result

    End Function


    '    Private void radButton1_Click(Object sender, EventArgs e)
    '{
    '    XlsxFormatProvider formatProvider = New XlsxFormatProvider();
    '    Workbook workbook = formatProvider.Import(File.ReadAllBytes(@"D:\Book1.xlsx"));

    '    var worksheet = workbook.Sheets[0] As Worksheet;
    '    var table = New DataTable();


    '    For (int i = 0; i < worksheet.UsedCellRange.ColumnCount; i++)
    '    {
    '        CellSelection selection = worksheet.Cells[0, i];
    '        var columnName = selection.GetValue().Value.RawValue.ToString();

    '        table.Columns.Add(columnName);
    '    }


    '    For (int i = 1; i < worksheet.UsedCellRange.RowCount; i++)
    '    {
    '        var values = New Object[worksheet.UsedCellRange.ColumnCount];

    '        For (int j = 0; j < worksheet.UsedCellRange.ColumnCount; j++)
    '        {
    '            CellSelection selection = worksheet.Cells[i, j];

    '            ICellValue value = selection.GetValue().Value;
    '            CellValueFormat format = selection.GetFormat().Value;
    '            CellValueFormatResult formatResult = format.GetFormatResult(value);
    '            String result = formatResult.InfosText;

    '            values[j] = result;
    '        }
    '        table.Rows.Add(values);

    '    }

    '    radGridView1.DataSource = table;

    '}

#End Region




End Class