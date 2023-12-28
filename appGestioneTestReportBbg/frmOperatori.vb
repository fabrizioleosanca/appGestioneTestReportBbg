Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.IO


Public Class frmOperatori

    Public Shared factory As New DatabaseProviderFactory()
    Public Property _db As Database = factory.Create("dbConnStrRete")
    Public Property TestiRiga As List(Of String)
    Public Property rowIndex As Integer = 0
    Public Property rigaOrdNum As Integer?

    Public Property emptyRow As DataRow


    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub frmOperatori_Load(sender As Object, e As EventArgs) Handles Me.Load

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
            fsEnrico = New System.IO.FileStream(curFileNameEnricoPath, FileMode.Open)
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


    Public Function Convert(ByVal value As Date) As Date
        Dim DateValue As DateTime = CType(value, DateTime)
        Return DateValue.ToShortDateString
    End Function




End Class