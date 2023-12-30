Imports System.Configuration
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.IO
Imports Microsoft.Practices.EnterpriseLibrary.Data

Public Class frmOperatori

    Public Shared factory As New DatabaseProviderFactory()
    Public Property _db As Database = factory.Create("dbConnStrRete")
    Public Property TestiRiga As List(Of String)
    Public Property rowIndex As Integer = 0
    Public Property rigaOrdNum As Integer?
    Public Property emptyRow As DataRow
    Public Property PathImmagineFirma As String


    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub frmOperatori_Load(sender As Object, e As EventArgs) Handles Me.Load
        RiempiComboOperatori()

    End Sub

    Public Sub RiempiComboOperatori()
        cmbSelezionaOperatore.Items.Clear()
        cmbCancellaOperatore.Items.Clear()

        Dim cmdOperatori As DbCommand
        Dim strSQLGetOperatori As String = "getOperatori"
        cmdOperatori = _db.GetStoredProcCommand(strSQLGetOperatori)
        Using datareader As IDataReader = _db.ExecuteReader(cmdOperatori)
            While datareader.Read
                cmbSelezionaOperatore.Items.Add(datareader("Operatore"))
                cmbCancellaOperatore.Items.Add(datareader("Operatore"))
            End While
        End Using
    End Sub

    Private Sub btnApriFirmaUpdate_Click(sender As Object, e As EventArgs) Handles btnApriFirmaUpdate.Click

    End Sub

    Private Sub cmbSelezionaOperatore_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSelezionaOperatore.SelectedIndexChanged
        GetOperatorePerUpdate()
    End Sub

    Public Sub GetOperatorePerUpdate()
        Dim strConn As String = ConfigurationManager.ConnectionStrings("dbConnStrRete").ConnectionString
        Dim valParameter As String = cmbSelezionaOperatore.SelectedItem.ToString
        Using connection As New SqlConnection(strConn)
            connection.Open()
            ' Start a local transaction.
            Dim sqlTran As SqlTransaction = connection.BeginTransaction()
            Dim reader As SqlDataReader
            ' Enlist a command in the current transaction.
            Dim command As SqlCommand = connection.CreateCommand()
            command.Transaction = sqlTran
            Try
                ' Execute two separate commands.
                command.CommandText = "SELECT [ID] ,[Operatore] ,[Firme] FROM [dbo].[tblOperatore] WHERE (Operatore = @Operatore)"
                Dim OpParameter As SqlParameter = New SqlParameter
                OpParameter.ParameterName = "@Operatore"
                OpParameter.SqlDbType = SqlDbType.NVarChar
                OpParameter.Direction = ParameterDirection.Input
                OpParameter.Value = valParameter
                command.Parameters.Add(OpParameter)
                reader = command.ExecuteReader()
                While reader.Read
                    txtUpdateNome.Text = reader("Operatore").ToString
                End While
                reader.Close()

                command.CommandText = "SELECT [Operatore] ,[Firme] FROM [dbo].[tblOperatore] WHERE (Operatore = @Operatore2)"
                Dim OpParameter2 As SqlParameter = New SqlParameter
                OpParameter2.ParameterName = "@Operatore2"
                OpParameter2.SqlDbType = SqlDbType.NVarChar
                OpParameter2.Direction = ParameterDirection.Input
                OpParameter2.Value = valParameter
                command.Parameters.Add(OpParameter2)
                reader = command.ExecuteReader()
                While reader.Read
                    Dim imageData As Byte() = DirectCast(reader("Firme"), Byte())
                    Dim ms As New MemoryStream(imageData, 0, imageData.Length)
                    Dim img As Image = Image.FromStream(ms, True)
                    'Assign the image object to the picture box
                    PictureBox2.BackgroundImage = img
                End While
                reader.Close()

                ' Commit the transaction.
                sqlTran.Commit()
            Catch ex As Exception
                MessageBox.Show("Errore GetOperatori : " & ex.Message)


                Try
                    ' Attempt to roll back the transaction.
                    sqlTran.Rollback()
                Catch exRollback As Exception
                    MessageBox.Show("Errore Transazione : Rollback " & exRollback.Message)
                End Try
            End Try
        End Using

    End Sub

    Private Sub cmdModificaOperatore_Click(sender As Object, e As EventArgs) Handles cmdModificaOperatore.Click

    End Sub

    Public Function updateOperatori(ID As Integer, Operatore As String) As Integer

        Dim updateCommand As DbCommand = Nothing
        Dim rowsAffected As Integer

        Dim strQuery As String = "UPDATE tblOperatore SET ID = @ID , Operatore = @Operatore WHERE Operatore = @Operatore"

        Try
            updateCommand = _db.GetSqlStringCommand(strQuery)

            _db.AddInParameter(updateCommand, "ID", DbType.Int32, ID)
            _db.AddInParameter(updateCommand, "Operatore", DbType.String, Operatore)
            _db.AddInParameter(updateCommand, "imgFirmaOperatore", DbType.Binary, propImageAsBytes)

            rowsAffected = _db.ExecuteNonQuery(updateCommand)

        Catch ex As Exception
            MessageBox.Show("Errore updateOperatori : " & ex.Message)
        End Try

    End Function

    Private Sub btnApriFileFirma_Click(sender As Object, e As EventArgs) Handles btnApriFileFirma.Click

        Dim noPhoto As String

        Dim pathFirma As String = Application.StartupPath
        Dim pathSelezionaImmagine As String = pathFirma.Replace("\bin\Debug", "\Immagini")
        Dim pathNoPhoto As String = pathFirma.Replace("\bin\Debug", "\Immagini\NoImmagineFirma.png")

        OpenFileDialog1.Title = "Seleziona La Nuova Immagine Della Firma Del Nuovo Operatore"
        OpenFileDialog1.InitialDirectory = pathSelezionaImmagine
        OpenFileDialog1.Filter = "File Immagine(*.PNG;*.JPG;*.GIF)|*.PNG;*.JPG;*.GIF|Tutti i Files (*.*)|*.*"

        Try

            ' Carica l'immagine selezionata nel controllo PictureBox
            If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
                PathImmagineFirma = OpenFileDialog1.FileName.ToString
                PictureBox1.BackgroundImage = Image.FromFile(PathImmagineFirma)
            Else
                PictureBox1.Image = Image.FromFile(pathNoPhoto)
            End If

        Catch ex As Exception
            MessageBox.Show("Errore frmOperatori : " & ex.Message)
        End Try

    End Sub


    Public Function inserisciNuovoOperatore(ID As Integer, NuovoOperatore As String) As Integer

        Dim curFileNameNuovoOperatore As String
        Dim newFileNameImageFirma As String

        'Immagine firma operatore nuova
        curFileNameNuovoOperatore = PathImmagineFirma

        Using fsNuovoOpe As FileStream = New FileStream(curFileNameNuovoOperatore, FileMode.Open)
            propImageAsBytes = New Byte(fsNuovoOpe.Length - 1) {}
            fsNuovoOpe.Read(propImageAsBytes, 0, propImageAsBytes.Length)
            fsNuovoOpe.Close()
        End Using


        Dim strQuery As String = "INSERT INTO tblOperatore " &
                     "(ID,Operatore ,Firme ) " &
                     " VALUES " &
                     "(@ID, @Operatore ,@Firme)"

        Try

            Using insertCommand As DbCommand = _db.GetSqlStringCommand(strQuery)

                _db.AddInParameter(insertCommand, "ID", DbType.Int32, ID)
                _db.AddInParameter(insertCommand, "Operatore", DbType.String, NuovoOperatore)
                _db.AddInParameter(insertCommand, "Firme", DbType.Binary, propImageAsBytes)

                Dim rowsAffected As Integer? = _db.ExecuteNonQuery(insertCommand)

                If Not rowsAffected Is Nothing Then
                    Return rowsAffected
                End If

            End Using

        Catch ex As Exception
            MessageBox.Show("Errore insertOperatore : " & ex.Message)
        End Try

    End Function

    Private Sub cmdAddOperatore_Click(sender As Object, e As EventArgs) Handles cmdAddOperatore.Click

        Dim CognomeNuovoOperatore As String = txtNewCognomeOperatore.Text
        Dim NomeNuovoOperatore As String = txtNewNomeOperatore.Text
        Dim CognomeNomeNuovo As String = CognomeNuovoOperatore & " " & NomeNuovoOperatore
        Dim ID As Integer = contatore()
        Dim dResult As DialogResult

        'FONDAMENTALE senno trova l'indirizzo di memoria occupato
        PictureBox1.BackgroundImage.Dispose()

        Try
            Dim retVal As Integer? = inserisciNuovoOperatore(ID, CognomeNomeNuovo)

            If Not retVal Is Nothing Then
                RiempiComboOperatori()
                txtNewNomeOperatore.Text = String.Empty
                txtNewCognomeOperatore.Text = String.Empty
                PictureBox1.BackgroundImage = Nothing
                creaMsgBox("Nuovo Opearore Aggiunto !", "Aggiungi Nuovo Operatore", MessageBoxButtons.OKCancel)
            Else
                txtNewNomeOperatore.Text = String.Empty
                txtNewCognomeOperatore.Text = String.Empty
                dResult = creaMsgBox("ERRORE Aggiungi Nuovo Operatore!", "Aggiungi Nuovo Operatore", MessageBoxButtons.OKCancel)
                If dResult = DialogResult.Cancel Then
                    Exit Sub
                End If
            End If

        Catch ex As Exception
            MessageBox.Show("Errore insertOperatore : " & ex.Message)
        End Try

    End Sub

    Public Function creaMsgBox(message As String, caption As String, buttons As MessageBoxButtons) As DialogResult
        Dim result As DialogResult
        result = MessageBox.Show(message, caption, buttons)
        Return result
    End Function

    Public Function contatoreNoAddUno() As Integer
        Dim i As Integer?

        Try
            Dim strSQL As String = "SELECT MAX(ID) AS IDOperatore FROM tblOperatore"
            Dim cmd As DbCommand = _db.GetSqlStringCommand(strSQL)

            Using datareader As IDataReader = _db.ExecuteReader(cmd)
                While datareader.Read
                    If IsDBNull(datareader("IDOperatore")) Then
                        i = 1
                    Else
                        i = datareader("IDOperatore")
                    End If
                End While
            End Using

        Catch ex As Exception
            MessageBox.Show("Errore : " & ex.Message)
        End Try

        Return i

    End Function

    Public Function contatore() As Integer
        Dim i As Integer?

        Try
            Dim strSQL As String = "SELECT MAX(ID) AS IDOperatore FROM tblOperatore"
            Dim cmd As DbCommand = _db.GetSqlStringCommand(strSQL)

            Using datareader As IDataReader = _db.ExecuteReader(cmd)
                While datareader.Read
                    If IsDBNull(datareader("IDOperatore")) Then
                        i = 1
                    Else
                        i = datareader("IDOperatore")
                        i = i + 1
                    End If
                End While
            End Using

        Catch ex As Exception
            MessageBox.Show("Errore : " & ex.Message)
        End Try

        Return i

    End Function


    Public Function Convert(value As Date) As Date
        Dim DateValue As Date = value
        Return DateValue.ToShortDateString
    End Function


    Private Sub cmdChiudi_Click(sender As Object, e As EventArgs) Handles cmdChiudi.Click
        Close()
    End Sub

End Class