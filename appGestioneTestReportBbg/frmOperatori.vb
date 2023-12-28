Imports System.Data.Common
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
        Dim cmdOperatori As DbCommand
        Dim strSQLGetOperatori As String = "getOperatori"
        cmdOperatori = _db.GetStoredProcCommand(strSQLGetOperatori)
        Using datareader As IDataReader = _db.ExecuteReader(cmdOperatori)
            While datareader.Read
                cmbSelezionaOperatore.Items.Add(datareader("Operatore"))
            End While
        End Using
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

        Try

            OpenFileDialog1.Title = "Seleziona La Nuova Immagine Della Firma Del Nuovo Operatore"
            OpenFileDialog1.InitialDirectory = pathSelezionaImmagine
            OpenFileDialog1.Filter = "File Immagine(*.PNG;*.JPG;*.GIF)|*.PNG;*.JPG;*.GIF|Tutti i Files (*.*)|*.*"

            PathImmagineFirma = OpenFileDialog1.FileName.ToString

            ' Carica l'immagine selezionata nel controllo PictureBox
            If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
                PictureBox1.Image = Image.FromFile(PathImmagineFirma)
            Else
                PictureBox1.Image = Image.FromFile(pathNoPhoto)
            End If

        Catch ex As Exception
            MessageBox.Show("Errore frmOperatori : " & ex.Message)
        End Try


    End Sub


    Public Function inserisciNuovoOperatore(ID As Integer, NuovoOperatore As String) As Integer

        Dim insertCommand As DbCommand = Nothing

        Dim curFileNameNuovoOperatore As String
        Dim fsNuovoOpe As FileStream

        Dim rowsAffected As Integer
        Dim newFileNameImageFirma As String

        'Immagine firma operatore nuova
        Dim pathFirmaNuovoApp As String = Application.StartupPath
        Dim pathFirmaNuovo As String = pathFirmaNuovoApp.Replace("\bin\Debug", "\Immagini\" & PathImmagineFirma)
        curFileNameNuovoOperatore = pathFirmaNuovo
        fsNuovoOpe = New FileStream(curFileNameNuovoOperatore, FileMode.Open)
        propImageAsBytes = New Byte(fsNuovoOpe.Length - 1) {}
        fsNuovoOpe.Read(propImageAsBytes, 0, propImageAsBytes.Length)
        fsNuovoOpe.Close()


        Dim strQuery As String = "INSERT INTO tblOperatore " &
                     "(ID,Operatore ,imgFirmaOperatore ) " &
                     " VALUES " &
                     "(@ID, @Operatore ,@imgFirmaOperatore)"

        Try

            insertCommand = _db.GetSqlStringCommand(strQuery)

            _db.AddInParameter(insertCommand, "ID", DbType.Int32, ID)
            _db.AddInParameter(insertCommand, "Operatore", DbType.String, NuovoOperatore)
            _db.AddInParameter(insertCommand, "imgFirmaOperatore", DbType.Binary, propImageAsBytes)

            rowsAffected = _db.ExecuteNonQuery(insertCommand)

        Catch ex As Exception
            MessageBox.Show("Errore insertOperatore : " & ex.Message)
        End Try

        Return rowsAffected


    End Function

    Private Sub cmdAddOperatore_Click(sender As Object, e As EventArgs) Handles cmdAddOperatore.Click

        Dim CognomeNuovoOperatore As String = txtNewCognomeOperatore.Text
        Dim NomeNuovoOperatore As String = txtNewNomeOperatore.Text

        Dim CognomeNomeNuovo As String = CognomeNuovoOperatore & " " & NomeNuovoOperatore

        Dim ID As Integer = contatore()




    End Sub

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



    Public Function insertOperatore(ID As Integer, Operatore As String) As Integer

        Dim insertCommand As DbCommand = Nothing

        Dim rowsAffected As Integer

        Dim curFileNameEnricoPath As String
        Dim curFileNameEdoPath As String
        Dim curFileNameManuPath As String
        Dim curFileNameGalloPath As String
        Dim curFileNameFontaPath As String
        Dim curFileNoFirma As String

        Dim fsEnrico As FileStream
        Dim fsEdo As FileStream
        Dim fsManu As FileStream
        Dim fsGallo As FileStream
        Dim fsFonta As FileStream
        Dim fsNoFirma As FileStream

        'Dim imageAsBytes As Byte()

        If Operatore = "Grigioni Enrico" Then
            Dim pathFirmaEnricoApp As String = Application.StartupPath
            Dim pathFirmaEnrico As String = pathFirmaEnricoApp.Replace("\bin\Debug", "\Immagini\firmaEnricoBuona.png")
            curFileNameEnricoPath = pathFirmaEnrico
            fsEnrico = New FileStream(curFileNameEnricoPath, FileMode.Open)
            propImageAsBytes = New Byte(fsEnrico.Length - 1) {}
            fsEnrico.Read(propImageAsBytes, 0, propImageAsBytes.Length)
            fsEnrico.Close()
        End If


        If Operatore = "Grigioni Edoardo" Then
            Dim pathFirmaEdoApp As String = Application.StartupPath
            Dim pathFirmaEdo As String = pathFirmaEdoApp.Replace("\bin\Debug", "\Immagini\firmaEdoardoBuona.png")
            curFileNameEdoPath = pathFirmaEdo
            fsEdo = New System.IO.FileStream(curFileNameEdoPath, FileMode.Open)
            propImageAsBytes = New Byte(fsEdo.Length - 1) {}
            fsEdo.Read(propImageAsBytes, 0, propImageAsBytes.Length)
            fsEdo.Close()
        End If


        If Operatore = "Bini Emanuele" Then
            Dim pathFirmaManuApp As String = Application.StartupPath
            Dim pathFirmaManu As String = pathFirmaManuApp.Replace("\bin\Debug", "\Immagini\firmaManuBuona.png")
            curFileNameManuPath = pathFirmaManu
            fsManu = New System.IO.FileStream(curFileNameManuPath, FileMode.Open)
            propImageAsBytes = New Byte(fsManu.Length - 1) {}
            fsManu.Read(propImageAsBytes, 0, propImageAsBytes.Length)
            fsManu.Close()
        End If

        If Operatore = "Galletti Adriano" Then
            Dim pathFirmaAdrianoApp As String = Application.StartupPath
            Dim pathFirmaAdriano As String = pathFirmaAdrianoApp.Replace("\bin\Debug", "\Immagini\firmaGalloBuona.png")
            curFileNameGalloPath = pathFirmaAdriano
            fsGallo = New System.IO.FileStream(curFileNameGalloPath, FileMode.Open)
            propImageAsBytes = New Byte(fsGallo.Length - 1) {}
            fsGallo.Read(propImageAsBytes, 0, propImageAsBytes.Length)
            fsGallo.Close()
        End If

        If Operatore = "Fontanelli Francesco" Then
            Dim pathFirmaFrancescoApp As String = Application.StartupPath
            Dim pathFirmaFrancesco As String = pathFirmaFrancescoApp.Replace("\bin\Debug", "\Immagini\firmaFontaBuona.png")
            curFileNameFontaPath = pathFirmaFrancesco
            fsFonta = New System.IO.FileStream(curFileNameFontaPath, FileMode.Open)
            propImageAsBytes = New Byte(fsFonta.Length - 1) {}
            fsFonta.Read(propImageAsBytes, 0, propImageAsBytes.Length)
            fsFonta.Close()
        End If

        If Operatore = String.Empty Then
            Dim pathFirmaNoUserApp As String = Application.StartupPath
            Dim pathFirmaNoUser As String = pathFirmaNoUserApp.Replace("\bin\Debug", "\Immagini\noFirma.gif")
            curFileNoFirma = pathFirmaNoUser
            fsNoFirma = New System.IO.FileStream(curFileNoFirma, FileMode.Open)
            propImageAsBytes = New Byte(fsNoFirma.Length - 1) {}
            fsNoFirma.Read(propImageAsBytes, 0, propImageAsBytes.Length)
            fsNoFirma.Close()
        End If

        Dim strQuery As String = "INSERT INTO tblOperatore " &
                      "(ID,Operatore ,imgFirmaOperatore ) " &
                      " VALUES " &
                      "(@ID, @Operatore ,@imgFirmaOperatore)"


        Try

            insertCommand = _db.GetSqlStringCommand(strQuery)

            _db.AddInParameter(insertCommand, "ID", DbType.Int32, ID)
            _db.AddInParameter(insertCommand, "Operatore", DbType.String, Operatore)
            _db.AddInParameter(insertCommand, "imgFirmaOperatore", DbType.Binary, propImageAsBytes)

            rowsAffected = _db.ExecuteNonQuery(insertCommand)

        Catch ex As Exception
            MessageBox.Show("Errore insertOperatore : " & ex.Message)
        End Try

        Return rowsAffected

    End Function


    Public Function Convert(value As Date) As Date
        Dim DateValue As Date = value
        Return DateValue.ToShortDateString
    End Function




    Private Sub cmdChiudi_Click(sender As Object, e As EventArgs) Handles cmdChiudi.Click
        Close()
    End Sub

End Class