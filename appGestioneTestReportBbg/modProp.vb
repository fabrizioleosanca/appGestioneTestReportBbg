Imports System
Imports System.Drawing
Imports System.Collections
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

Module modProp

    Public Property boolReturnMain As Boolean = False



#Region "Proprieta Lotto Materiale"
    Public Property propIdLottoMateriale As Integer
#End Region


#Region "Proprieta Testata"
    Public Property propIdIntestazione As Integer
    Public Property propDataReport As Date
    Public Property propNumeroOrdine As String
    Public Property propCodiceArticolo As String
    Public Property propMateriale As String
    Public Property propStrumento As String
    Public Property propMacchinaNum As String
    Public Property propRigaOrdNum As Integer
    Public Property propNumPezzi As Integer
    Public Property propFornitore As String
    Public Property propNumLotto As String
    Public Property propPrimoPezzo As Boolean
    Public Property propUltimoPezzo As Boolean
    Public Property propPezziControllati As String
    Public Property propLottoNumero As String
    Public Property rowIndexGridViewTestata As Integer
    Public Property propOperatore As String

#End Region

#Region "Proprieta Registra Lotto Materiale"
    Public Property propMaterialeLotto As String
    Public Property propFornitoreLotto As String
    Public Property propNumDDTLottoMateriale As String
    Public Property propLotto As String

    Public Property propMaterialeLottoStampa As String
    Public Property propFornitoreLottoStampa As String
    Public Property propNumDDTLottoMaterialeStampa As String
    Public Property propLottoStampa As String
    Public Property propImageAsBytes As Byte()
    Public Property propImageAsBytesUpdate As Byte()
#End Region

#Region "Proprieta Primo Pezzo"
    Public Property propIDPezziPrimoPezzo As Integer

    Public Property propValorePrevistoPrimoUno As String
    Public Property propValorePrevistoPrimoDue As String
    Public Property propValorePrevistoPrimoTre As String
    Public Property propValorePrevistoPrimoQuattro As String
    Public Property propValorePrevistoPrimoCinque As String
    Public Property propValorePrevistoPrimoSei As String
    Public Property propValorePrevistoPrimoSette As String
    Public Property propValorePrevistoPrimoOtto As String
    Public Property propValorePrevistoPrimoNove As String
    Public Property propValorePrevistoPrimoDieci As String

    Public Property propValoreMisuratoPrimoUno As String
    Public Property propValoreMisuratoPrimoDue As String
    Public Property propValoreMisuratoPrimoTre As String
    Public Property propValoreMisuratoPrimoQuattro As String
    Public Property propValoreMisuratoPrimoCinque As String
    Public Property propValoreMisuratoPrimoSei As String
    Public Property propValoreMisuratoPrimoSette As String
    Public Property propValoreMisuratoPrimoOtto As String
    Public Property propValoreMisuratoPrimoNove As String
    Public Property propValoreMisuratoPrimoDieci As String

    Public Property propTolleranzaPiupropValorePrevistoPrimoUno As String
    Public Property propTolleranzaPiupropValorePrevistoPrimoDue As String
    Public Property propTolleranzaPiupropValorePrevistoPrimoTre As String
    Public Property propTolleranzaPiupropValorePrevistoPrimoQuattro As String
    Public Property propTolleranzaPiupropValorePrevistoPrimoCinque As String
    Public Property propTolleranzaPiupropValorePrevistoPrimoSei As String
    Public Property propTolleranzaPiupropValorePrevistoPrimoSette As String
    Public Property propTolleranzaPiupropValorePrevistoPrimoOtto As String
    Public Property propTolleranzaPiupropValorePrevistoPrimoNove As String
    Public Property propTolleranzaPiupropValorePrevistoPrimoDieci As String

    Public Property propTolleranzaMenopropValorePrevistoPrimoUno As String
    Public Property propTolleranzaMenopropValorePrevistoPrimoDue As String
    Public Property propTolleranzaMenopropValorePrevistoPrimoTre As String
    Public Property propTolleranzaMenopropValorePrevistoPrimoQuattro As String
    Public Property propTolleranzaMenopropValorePrevistoPrimoCinque As String
    Public Property propTolleranzaMenopropValorePrevistoPrimoSei As String
    Public Property propTolleranzaMenopropValorePrevistoPrimoSette As String
    Public Property propTolleranzaMenopropValorePrevistoPrimoOtto As String
    Public Property propTolleranzaMenopropValorePrevistoPrimoNove As String
    Public Property propTolleranzaMenopropValorePrevistoPrimoDieci As String

    Public Property propNotePrimoPezzo As String

#End Region


#Region "Proprieta Secondo Pezzo"
    Public Property propIDPezziSecondoPezzo As Integer

    Public Property propValorePrevistoSecondoUno As String
    Public Property propValorePrevistoSecondoDue As String
    Public Property propValorePrevistoSecondoTre As String
    Public Property propValorePrevistoSecondoQuattro As String
    Public Property propValorePrevistoSecondoCinque As String
    Public Property propValorePrevistoSecondoSei As String
    Public Property propValorePrevistoSecondoSette As String
    Public Property propValorePrevistoSecondoOtto As String
    Public Property propValorePrevistoSecondoNove As String
    Public Property propValorePrevistoSecondoDieci As String

    Public Property propValoreMisuratoSecondoUno As String
    Public Property propValoreMisuratoSecondoDue As String
    Public Property propValoreMisuratoSecondoTre As String
    Public Property propValoreMisuratoSecondoQuattro As String
    Public Property propValoreMisuratoSecondoCinque As String
    Public Property propValoreMisuratoSecondoSei As String
    Public Property propValoreMisuratoSecondoSette As String
    Public Property propValoreMisuratoSecondoOtto As String
    Public Property propValoreMisuratoSecondoNove As String
    Public Property propValoreMisuratoSecondoDieci As String


    Public Property propTolleranzaPiupropValorePrevistoSecondoUno As String
    Public Property propTolleranzaPiupropValorePrevistoSecondoDue As String
    Public Property propTolleranzaPiupropValorePrevistoSecondoTre As String
    Public Property propTolleranzaPiupropValorePrevistoSecondoQuattro As String
    Public Property propTolleranzaPiupropValorePrevistoSecondoCinque As String
    Public Property propTolleranzaPiupropValorePrevistoSecondoSei As String
    Public Property propTolleranzaPiupropValorePrevistoSecondoSette As String
    Public Property propTolleranzaPiupropValorePrevistoSecondoOtto As String
    Public Property propTolleranzaPiupropValorePrevistoSecondoNove As String
    Public Property propTolleranzaPiupropValorePrevistoSecondoDieci As String

    Public Property propTolleranzaMenopropValorePrevistoSecondoUno As String
    Public Property propTolleranzaMenopropValorePrevistoSecondoDue As String
    Public Property propTolleranzaMenopropValorePrevistoSecondoTre As String
    Public Property propTolleranzaMenopropValorePrevistoSecondoQuattro As String
    Public Property propTolleranzaMenopropValorePrevistoSecondoCinque As String
    Public Property propTolleranzaMenopropValorePrevistoSecondoSei As String
    Public Property propTolleranzaMenopropValorePrevistoSecondoSette As String
    Public Property propTolleranzaMenopropValorePrevistoSecondoOtto As String
    Public Property propTolleranzaMenopropValorePrevistoSecondoNove As String
    Public Property propTolleranzaMenopropValorePrevistoSecondoDieci As String

    Public Property propNoteSecondoPezzo As String

#End Region


#Region "Proprieta Terzo Pezzo"
    Public Property propIDPezziTerzoPezzo As Integer

    Public Property propValorePrevistoTerzoUno As String
    Public Property propValorePrevistoTerzoDue As String
    Public Property propValorePrevistoTerzoTre As String
    Public Property propValorePrevistoTerzoQuattro As String
    Public Property propValorePrevistoTerzoCinque As String
    Public Property propValorePrevistoTerzoSei As String
    Public Property propValorePrevistoTerzoSette As String
    Public Property propValorePrevistoTerzoOtto As String
    Public Property propValorePrevistoTerzoNove As String
    Public Property propValorePrevistoTerzoDieci As String

    Public Property propValoreMisuratoTerzoUno As String
    Public Property propValoreMisuratoTerzoDue As String
    Public Property propValoreMisuratoTerzoTre As String
    Public Property propValoreMisuratoTerzoQuattro As String
    Public Property propValoreMisuratoTerzoCinque As String
    Public Property propValoreMisuratoTerzoSei As String
    Public Property propValoreMisuratoTerzoSette As String
    Public Property propValoreMisuratoTerzoOtto As String
    Public Property propValoreMisuratoTerzoNove As String
    Public Property propValoreMisuratoTerzoDieci As String
    Public Property propNoteTerzoPezzo As String

    Public Property propTolleranzaPiupropValorePrevistoTerzoUno As String
    Public Property propTolleranzaPiupropValorePrevistoTerzoDue As String
    Public Property propTolleranzaPiupropValorePrevistoTerzoTre As String
    Public Property propTolleranzaPiupropValorePrevistoTerzoQuattro As String
    Public Property propTolleranzaPiupropValorePrevistoTerzoCinque As String
    Public Property propTolleranzaPiupropValorePrevistoTerzoSei As String
    Public Property propTolleranzaPiupropValorePrevistoTerzoSette As String
    Public Property propTolleranzaPiupropValorePrevistoTerzoOtto As String
    Public Property propTolleranzaPiupropValorePrevistoTerzoNove As String
    Public Property propTolleranzaPiupropValorePrevistoTerzoDieci As String

    Public Property propTolleranzaMenopropValorePrevistoTerzoUno As String
    Public Property propTolleranzaMenopropValorePrevistoTerzoDue As String
    Public Property propTolleranzaMenopropValorePrevistoTerzoTre As String
    Public Property propTolleranzaMenopropValorePrevistoTerzoQuattro As String
    Public Property propTolleranzaMenopropValorePrevistoTerzoCinque As String
    Public Property propTolleranzaMenopropValorePrevistoTerzoSei As String
    Public Property propTolleranzaMenopropValorePrevistoTerzoSette As String
    Public Property propTolleranzaMenopropValorePrevistoTerzoOtto As String
    Public Property propTolleranzaMenopropValorePrevistoTerzoNove As String
    Public Property propTolleranzaMenopropValorePrevistoTerzoDieci As String
#End Region

#Region "Proprieta Quarto Pezzo"
    Public Property propIDPezziQuartoPezzo As Integer

    Public Property propValorePrevistoQuartoUno As String
    Public Property propValorePrevistoQuartoDue As String
    Public Property propValorePrevistoQuartoTre As String
    Public Property propValorePrevistoQuartoQuattro As String
    Public Property propValorePrevistoQuartoCinque As String
    Public Property propValorePrevistoQuartoSei As String
    Public Property propValorePrevistoQuartoSette As String
    Public Property propValorePrevistoQuartoOtto As String
    Public Property propValorePrevistoQuartoNove As String
    Public Property propValorePrevistoQuartoDieci As String

    Public Property propValoreMisuratoQuartoUno As String
    Public Property propValoreMisuratoQuartoDue As String
    Public Property propValoreMisuratoQuartoTre As String
    Public Property propValoreMisuratoQuartoQuattro As String
    Public Property propValoreMisuratoQuartoCinque As String
    Public Property propValoreMisuratoQuartoSei As String
    Public Property propValoreMisuratoQuartoSette As String
    Public Property propValoreMisuratoQuartoOtto As String
    Public Property propValoreMisuratoQuartoNove As String
    Public Property propValoreMisuratoQuartoDieci As String
    Public Property propNoteQuartoPezzo As String

    Public Property propTolleranzaPiupropValorePrevistoQuartoUno As String
    Public Property propTolleranzaPiupropValorePrevistoQuartoDue As String
    Public Property propTolleranzaPiupropValorePrevistoQuartoTre As String
    Public Property propTolleranzaPiupropValorePrevistoQuartoQuattro As String
    Public Property propTolleranzaPiupropValorePrevistoQuartoCinque As String
    Public Property propTolleranzaPiupropValorePrevistoQuartoSei As String
    Public Property propTolleranzaPiupropValorePrevistoQuartoSette As String
    Public Property propTolleranzaPiupropValorePrevistoQuartoOtto As String
    Public Property propTolleranzaPiupropValorePrevistoQuartoNove As String
    Public Property propTolleranzaPiupropValorePrevistoQuartoDieci As String

    Public Property propTolleranzaMenopropValorePrevistoQuartoUno As String
    Public Property propTolleranzaMenopropValorePrevistoQuartoDue As String
    Public Property propTolleranzaMenopropValorePrevistoQuartoTre As String
    Public Property propTolleranzaMenopropValorePrevistoQuartoQuattro As String
    Public Property propTolleranzaMenopropValorePrevistoQuartoCinque As String
    Public Property propTolleranzaMenopropValorePrevistoQuartoSei As String
    Public Property propTolleranzaMenopropValorePrevistoQuartoSette As String
    Public Property propTolleranzaMenopropValorePrevistoQuartoOtto As String
    Public Property propTolleranzaMenopropValorePrevistoQuartoNove As String
    Public Property propTolleranzaMenopropValorePrevistoQuartoDieci As String
#End Region

#Region "Proprieta Quinto Pezzo"
    Public Property propIDPezziQuintoPezzo As Integer

    Public Property propValorePrevistoQuintoUno As String
    Public Property propValorePrevistoQuintoDue As String
    Public Property propValorePrevistoQuintoTre As String
    Public Property propValorePrevistoQuintoQuattro As String
    Public Property propValorePrevistoQuintoCinque As String
    Public Property propValorePrevistoQuintoSei As String
    Public Property propValorePrevistoQuintoSette As String
    Public Property propValorePrevistoQuintoOtto As String
    Public Property propValorePrevistoQuintoNove As String
    Public Property propValorePrevistoQuintoDieci As String

    Public Property propValoreMisuratoQuintoUno As String
    Public Property propValoreMisuratoQuintoDue As String
    Public Property propValoreMisuratoQuintoTre As String
    Public Property propValoreMisuratoQuintoQuattro As String
    Public Property propValoreMisuratoQuintoCinque As String
    Public Property propValoreMisuratoQuintoSei As String
    Public Property propValoreMisuratoQuintoSette As String
    Public Property propValoreMisuratoQuintoOtto As String
    Public Property propValoreMisuratoQuintoNove As String
    Public Property propValoreMisuratoQuintoDieci As String
    Public Property propNoteQuintoPezzo As String

    Public Property propTolleranzaPiupropValorePrevistoQuintoUno As String
    Public Property propTolleranzaPiupropValorePrevistoQuintoDue As String
    Public Property propTolleranzaPiupropValorePrevistoQuintoTre As String
    Public Property propTolleranzaPiupropValorePrevistoQuintoQuattro As String
    Public Property propTolleranzaPiupropValorePrevistoQuintoCinque As String
    Public Property propTolleranzaPiupropValorePrevistoQuintoSei As String
    Public Property propTolleranzaPiupropValorePrevistoQuintoSette As String
    Public Property propTolleranzaPiupropValorePrevistoQuintoOtto As String
    Public Property propTolleranzaPiupropValorePrevistoQuintoNove As String
    Public Property propTolleranzaPiupropValorePrevistoQuintoDieci As String

    Public Property propTolleranzaMenopropValorePrevistoQuintoUno As String
    Public Property propTolleranzaMenopropValorePrevistoQuintoDue As String
    Public Property propTolleranzaMenopropValorePrevistoQuintoTre As String
    Public Property propTolleranzaMenopropValorePrevistoQuintoQuattro As String
    Public Property propTolleranzaMenopropValorePrevistoQuintoCinque As String
    Public Property propTolleranzaMenopropValorePrevistoQuintoSei As String
    Public Property propTolleranzaMenopropValorePrevistoQuintoSette As String
    Public Property propTolleranzaMenopropValorePrevistoQuintoOtto As String
    Public Property propTolleranzaMenopropValorePrevistoQuintoNove As String
    Public Property propTolleranzaMenopropValorePrevistoQuintoDieci As String
#End Region


#Region "Proprieta Ultimo Pezzo New"
    Public Property propIDPezziUltimoPezzo As Integer

    Public Property propValorePrevistoUltimoUno As String
    Public Property propValorePrevistoUltimoDue As String
    Public Property propValorePrevistoUltimoTre As String
    Public Property propValorePrevistoUltimoQuattro As String
    Public Property propValorePrevistoUltimoCinque As String
    Public Property propValorePrevistoUltimoSei As String
    Public Property propValorePrevistoUltimoSette As String
    Public Property propValorePrevistoUltimoOtto As String
    Public Property propValorePrevistoUltimoNove As String
    Public Property propValorePrevistoUltimoDieci As String

    Public Property propValoreMisuratoUltimoUno As String
    Public Property propValoreMisuratoUltimoDue As String
    Public Property propValoreMisuratoUltimoTre As String
    Public Property propValoreMisuratoUltimoQuattro As String
    Public Property propValoreMisuratoUltimoCinque As String
    Public Property propValoreMisuratoUltimoSei As String
    Public Property propValoreMisuratoUltimoSette As String
    Public Property propValoreMisuratoUltimoOtto As String
    Public Property propValoreMisuratoUltimoNove As String
    Public Property propValoreMisuratoUltimoDieci As String
    Public Property propNoteUltimoPezzo As String

    Public Property propTolleranzaPiupropValorePrevistoUltimoUno As String
    Public Property propTolleranzaPiupropValorePrevistoUltimoDue As String
    Public Property propTolleranzaPiupropValorePrevistoUltimoTre As String
    Public Property propTolleranzaPiupropValorePrevistoUltimoQuattro As String
    Public Property propTolleranzaPiupropValorePrevistoUltimoCinque As String
    Public Property propTolleranzaPiupropValorePrevistoUltimoSei As String
    Public Property propTolleranzaPiupropValorePrevistoUltimoSette As String
    Public Property propTolleranzaPiupropValorePrevistoUltimoOtto As String
    Public Property propTolleranzaPiupropValorePrevistoUltimoNove As String
    Public Property propTolleranzaPiupropValorePrevistoUltimoDieci As String

    Public Property propTolleranzaMenopropValorePrevistoUltimoUno As String
    Public Property propTolleranzaMenopropValorePrevistoUltimoDue As String
    Public Property propTolleranzaMenopropValorePrevistoUltimoTre As String
    Public Property propTolleranzaMenopropValorePrevistoUltimoQuattro As String
    Public Property propTolleranzaMenopropValorePrevistoUltimoCinque As String
    Public Property propTolleranzaMenopropValorePrevistoUltimoSei As String
    Public Property propTolleranzaMenopropValorePrevistoUltimoSette As String
    Public Property propTolleranzaMenopropValorePrevistoUltimoOtto As String
    Public Property propTolleranzaMenopropValorePrevistoUltimoNove As String
    Public Property propTolleranzaMenopropValorePrevistoUltimoDieci As String
#End Region




#Region "Property Ricerca"
    Public Property propIdIntestazioneRicerca As Integer
    Public Property propCodiceArticoloNomeFilePDF As String
#End Region

#Region "Proprieta Salva report da report esistente"
    Public Property propIdTestataDaReportEsistente As Integer
#End Region

#Region "Proprieta Testata da report esistente"
    Public Property propIdIntestazioneNew As Integer
    Public Property propDataReportNew As Date
    Public Property propNumeroOrdineNew As String
    Public Property propCodiceArticoloNew As String
    Public Property propMaterialeNew As String
    Public Property propStrumentoNew As String
    Public Property propMacchinaNumNew As String
    Public Property propRigaOrdNumNew As Integer
    Public Property propNumPezziNew As Integer
    Public Property propFornitoreNew As String
    Public Property propNumLottoNew As String
    Public Property propPrimoPezzoNew As Boolean
    Public Property propUltimoPezzoNew As Boolean
    Public Property propPezziControllatiNew As String
    Public Property rowIndexGridViewTestataNew As Integer
    Public Property propOperatoreNew As String
#End Region

#Region "Proprieta Primo Pezzo da report esistente"

    Public Property propIDPezziPrimoPezzoNew As Integer

    Public Property propValorePrevistoPrimoUnoNew As String
    Public Property propValorePrevistoPrimoDueNew As String
    Public Property propValorePrevistoPrimoTreNew As String
    Public Property propValorePrevistoPrimoQuattroNew As String
    Public Property propValorePrevistoPrimoCinqueNew As String
    Public Property propValorePrevistoPrimoSeiNew As String
    Public Property propValorePrevistoPrimoSetteNew As String
    Public Property propValorePrevistoPrimoOttoNew As String
    Public Property propValorePrevistoPrimoNoveNew As String
    Public Property propValorePrevistoPrimoDieciNew As String

    Public Property propValoreMisuratoPrimoUnoNew As String
    Public Property propValoreMisuratoPrimoDueNew As String
    Public Property propValoreMisuratoPrimoTreNew As String
    Public Property propValoreMisuratoPrimoQuattroNew As String
    Public Property propValoreMisuratoPrimoCinqueNew As String
    Public Property propValoreMisuratoPrimoSeiNew As String
    Public Property propValoreMisuratoPrimoSetteNew As String
    Public Property propValoreMisuratoPrimoOttoNew As String
    Public Property propValoreMisuratoPrimoNoveNew As String
    Public Property propValoreMisuratoPrimoDieciNew As String

    Public Property propTolleranzaPiupropValorePrevistoPrimoUnoNew As String
    Public Property propTolleranzaPiupropValorePrevistoPrimoDueNew As String
    Public Property propTolleranzaPiupropValorePrevistoPrimoTreNew As String
    Public Property propTolleranzaPiupropValorePrevistoPrimoQuattroNew As String
    Public Property propTolleranzaPiupropValorePrevistoPrimoCinqueNew As String
    Public Property propTolleranzaPiupropValorePrevistoPrimoSeiNew As String
    Public Property propTolleranzaPiupropValorePrevistoPrimoSetteNew As String
    Public Property propTolleranzaPiupropValorePrevistoPrimoOttoNew As String
    Public Property propTolleranzaPiupropValorePrevistoPrimoNoveNew As String
    Public Property propTolleranzaPiupropValorePrevistoPrimoDieciNew As String

    Public Property propTolleranzaMenopropValorePrevistoPrimoUnoNew As String
    Public Property propTolleranzaMenopropValorePrevistoPrimoDueNew As String
    Public Property propTolleranzaMenopropValorePrevistoPrimoTreNew As String
    Public Property propTolleranzaMenopropValorePrevistoPrimoQuattroNew As String
    Public Property propTolleranzaMenopropValorePrevistoPrimoCinqueNew As String
    Public Property propTolleranzaMenopropValorePrevistoPrimoSeiNew As String
    Public Property propTolleranzaMenopropValorePrevistoPrimoSetteNew As String
    Public Property propTolleranzaMenopropValorePrevistoPrimoOttoNew As String
    Public Property propTolleranzaMenopropValorePrevistoPrimoNoveNew As String
    Public Property propTolleranzaMenopropValorePrevistoPrimoDieciNew As String

    Public Property propNotePrimoPezzoNew As String


#End Region

#Region "Proprieta Secondo Pezzo da report esistente"

    Public Property propIDPezziSecondoPezzoNew As Integer

    Public Property propValorePrevistoSecondoUnoNew As String
    Public Property propValorePrevistoSecondoDueNew As String
    Public Property propValorePrevistoSecondoTreNew As String
    Public Property propValorePrevistoSecondoQuattroNew As String
    Public Property propValorePrevistoSecondoCinqueNew As String
    Public Property propValorePrevistoSecondoSeiNew As String
    Public Property propValorePrevistoSecondoSetteNew As String
    Public Property propValorePrevistoSecondoOttoNew As String
    Public Property propValorePrevistoSecondoNoveNew As String
    Public Property propValorePrevistoSecondoDieciNew As String

    Public Property propValoreMisuratoSecondoUnoNew As String
    Public Property propValoreMisuratoSecondoDueNew As String
    Public Property propValoreMisuratoSecondoTreNew As String
    Public Property propValoreMisuratoSecondoQuattroNew As String
    Public Property propValoreMisuratoSecondoCinqueNew As String
    Public Property propValoreMisuratoSecondoSeiNew As String
    Public Property propValoreMisuratoSecondoSetteNew As String
    Public Property propValoreMisuratoSecondoOttoNew As String
    Public Property propValoreMisuratoSecondoNoveNew As String
    Public Property propValoreMisuratoSecondoDieciNew As String


    Public Property propTolleranzaPiupropValorePrevistoSecondoUnoNew As String
    Public Property propTolleranzaPiupropValorePrevistoSecondoDueNew As String
    Public Property propTolleranzaPiupropValorePrevistoSecondoTreNew As String
    Public Property propTolleranzaPiupropValorePrevistoSecondoQuattroNew As String
    Public Property propTolleranzaPiupropValorePrevistoSecondoCinqueNew As String
    Public Property propTolleranzaPiupropValorePrevistoSecondoSeiNew As String
    Public Property propTolleranzaPiupropValorePrevistoSecondoSetteNew As String
    Public Property propTolleranzaPiupropValorePrevistoSecondoOttoNew As String
    Public Property propTolleranzaPiupropValorePrevistoSecondoNoveNew As String
    Public Property propTolleranzaPiupropValorePrevistoSecondoDieciNew As String

    Public Property propTolleranzaMenopropValorePrevistoSecondoUnoNew As String
    Public Property propTolleranzaMenopropValorePrevistoSecondoDueNew As String
    Public Property propTolleranzaMenopropValorePrevistoSecondoTreNew As String
    Public Property propTolleranzaMenopropValorePrevistoSecondoQuattroNew As String
    Public Property propTolleranzaMenopropValorePrevistoSecondoCinqueNew As String
    Public Property propTolleranzaMenopropValorePrevistoSecondoSeiNew As String
    Public Property propTolleranzaMenopropValorePrevistoSecondoSetteNew As String
    Public Property propTolleranzaMenopropValorePrevistoSecondoOttoNew As String
    Public Property propTolleranzaMenopropValorePrevistoSecondoNoveNew As String
    Public Property propTolleranzaMenopropValorePrevistoSecondoDieciNew As String

    Public Property propNoteSecondoPezzoNew As String

#End Region

#Region "Proprieta Terzo Pezzo da report esistente"

    Public Property propIDPezziTerzoPezzoNew As Integer

    Public Property propValorePrevistoTerzoUnoNew As String
    Public Property propValorePrevistoTerzoDueNew As String
    Public Property propValorePrevistoTerzoTreNew As String
    Public Property propValorePrevistoTerzoQuattroNew As String
    Public Property propValorePrevistoTerzoCinqueNew As String
    Public Property propValorePrevistoTerzoSeiNew As String
    Public Property propValorePrevistoTerzoSetteNew As String
    Public Property propValorePrevistoTerzoOttoNew As String
    Public Property propValorePrevistoTerzoNoveNew As String
    Public Property propValorePrevistoTerzoDieciNew As String

    Public Property propValoreMisuratoTerzoUnoNew As String
    Public Property propValoreMisuratoTerzoDueNew As String
    Public Property propValoreMisuratoTerzoTreNew As String
    Public Property propValoreMisuratoTerzoQuattroNew As String
    Public Property propValoreMisuratoTerzoCinqueNew As String
    Public Property propValoreMisuratoTerzoSeiNew As String
    Public Property propValoreMisuratoTerzoSetteNew As String
    Public Property propValoreMisuratoTerzoOttoNew As String
    Public Property propValoreMisuratoTerzoNoveNew As String
    Public Property propValoreMisuratoTerzoDieciNew As String


    Public Property propTolleranzaPiupropValorePrevistoTerzoUnoNew As String
    Public Property propTolleranzaPiupropValorePrevistoTerzoDueNew As String
    Public Property propTolleranzaPiupropValorePrevistoTerzoTreNew As String
    Public Property propTolleranzaPiupropValorePrevistoTerzoQuattroNew As String
    Public Property propTolleranzaPiupropValorePrevistoTerzoCinqueNew As String
    Public Property propTolleranzaPiupropValorePrevistoTerzoSeiNew As String
    Public Property propTolleranzaPiupropValorePrevistoTerzoSetteNew As String
    Public Property propTolleranzaPiupropValorePrevistoTerzoOttoNew As String
    Public Property propTolleranzaPiupropValorePrevistoTerzoNoveNew As String
    Public Property propTolleranzaPiupropValorePrevistoTerzoDieciNew As String

    Public Property propTolleranzaMenopropValorePrevistoTerzoUnoNew As String
    Public Property propTolleranzaMenopropValorePrevistoTerzoDueNew As String
    Public Property propTolleranzaMenopropValorePrevistoTerzoTreNew As String
    Public Property propTolleranzaMenopropValorePrevistoTerzoQuattroNew As String
    Public Property propTolleranzaMenopropValorePrevistoTerzoCinqueNew As String
    Public Property propTolleranzaMenopropValorePrevistoTerzoSeiNew As String
    Public Property propTolleranzaMenopropValorePrevistoTerzoSetteNew As String
    Public Property propTolleranzaMenopropValorePrevistoTerzoOttoNew As String
    Public Property propTolleranzaMenopropValorePrevistoTerzoNoveNew As String
    Public Property propTolleranzaMenopropValorePrevistoTerzoDieciNew As String

    Public Property propNoteTerzoPezzoNew As String

#End Region

#Region "Proprieta Quarto Pezzo da report esistente"

    Public Property propIDPezziQuartoPezzoNew As Integer

    Public Property propValorePrevistoQuartoUnoNew As String
    Public Property propValorePrevistoQuartoDueNew As String
    Public Property propValorePrevistoQuartoTreNew As String
    Public Property propValorePrevistoQuartoQuattroNew As String
    Public Property propValorePrevistoQuartoCinqueNew As String
    Public Property propValorePrevistoQuartoSeiNew As String
    Public Property propValorePrevistoQuartoSetteNew As String
    Public Property propValorePrevistoQuartoOttoNew As String
    Public Property propValorePrevistoQuartoNoveNew As String
    Public Property propValorePrevistoQuartoDieciNew As String

    Public Property propValoreMisuratoQuartoUnoNew As String
    Public Property propValoreMisuratoQuartoDueNew As String
    Public Property propValoreMisuratoQuartoTreNew As String
    Public Property propValoreMisuratoQuartoQuattroNew As String
    Public Property propValoreMisuratoQuartoCinqueNew As String
    Public Property propValoreMisuratoQuartoSeiNew As String
    Public Property propValoreMisuratoQuartoSetteNew As String
    Public Property propValoreMisuratoQuartoOttoNew As String
    Public Property propValoreMisuratoQuartoNoveNew As String
    Public Property propValoreMisuratoQuartoDieciNew As String


    Public Property propTolleranzaPiupropValorePrevistoQuartoUnoNew As String
    Public Property propTolleranzaPiupropValorePrevistoQuartoDueNew As String
    Public Property propTolleranzaPiupropValorePrevistoQuartoTreNew As String
    Public Property propTolleranzaPiupropValorePrevistoQuartoQuattroNew As String
    Public Property propTolleranzaPiupropValorePrevistoQuartoCinqueNew As String
    Public Property propTolleranzaPiupropValorePrevistoQuartoSeiNew As String
    Public Property propTolleranzaPiupropValorePrevistoQuartoSetteNew As String
    Public Property propTolleranzaPiupropValorePrevistoQuartoOttoNew As String
    Public Property propTolleranzaPiupropValorePrevistoQuartoNoveNew As String
    Public Property propTolleranzaPiupropValorePrevistoQuartoDieciNew As String

    Public Property propTolleranzaMenopropValorePrevistoQuartoUnoNew As String
    Public Property propTolleranzaMenopropValorePrevistoQuartoDueNew As String
    Public Property propTolleranzaMenopropValorePrevistoQuartoTreNew As String
    Public Property propTolleranzaMenopropValorePrevistoQuartoQuattroNew As String
    Public Property propTolleranzaMenopropValorePrevistoQuartoCinqueNew As String
    Public Property propTolleranzaMenopropValorePrevistoQuartoSeiNew As String
    Public Property propTolleranzaMenopropValorePrevistoQuartoSetteNew As String
    Public Property propTolleranzaMenopropValorePrevistoQuartoOttoNew As String
    Public Property propTolleranzaMenopropValorePrevistoQuartoNoveNew As String
    Public Property propTolleranzaMenopropValorePrevistoQuartoDieciNew As String

    Public Property propNoteQuartoPezzoNew As String


#End Region


#Region "Proprieta Quinto Pezzo da report esistente"


    Public Property propIDPezziQuintoPezzoNew As Integer

    Public Property propValorePrevistoQuintoUnoNew As String
    Public Property propValorePrevistoQuintoDueNew As String
    Public Property propValorePrevistoQuintoTreNew As String
    Public Property propValorePrevistoQuintoQuattroNew As String
    Public Property propValorePrevistoQuintoCinqueNew As String
    Public Property propValorePrevistoQuintoSeiNew As String
    Public Property propValorePrevistoQuintoSetteNew As String
    Public Property propValorePrevistoQuintoOttoNew As String
    Public Property propValorePrevistoQuintoNoveNew As String
    Public Property propValorePrevistoQuintoDieciNew As String

    Public Property propValoreMisuratoQuintoUnoNew As String
    Public Property propValoreMisuratoQuintoDueNew As String
    Public Property propValoreMisuratoQuintoTreNew As String
    Public Property propValoreMisuratoQuintoQuattroNew As String
    Public Property propValoreMisuratoQuintoCinqueNew As String
    Public Property propValoreMisuratoQuintoSeiNew As String
    Public Property propValoreMisuratoQuintoSetteNew As String
    Public Property propValoreMisuratoQuintoOttoNew As String
    Public Property propValoreMisuratoQuintoNoveNew As String
    Public Property propValoreMisuratoQuintoDieciNew As String


    Public Property propTolleranzaPiupropValorePrevistoQuintoUnoNew As String
    Public Property propTolleranzaPiupropValorePrevistoQuintoDueNew As String
    Public Property propTolleranzaPiupropValorePrevistoQuintoTreNew As String
    Public Property propTolleranzaPiupropValorePrevistoQuintoQuattroNew As String
    Public Property propTolleranzaPiupropValorePrevistoQuintoCinqueNew As String
    Public Property propTolleranzaPiupropValorePrevistoQuintoSeiNew As String
    Public Property propTolleranzaPiupropValorePrevistoQuintoSetteNew As String
    Public Property propTolleranzaPiupropValorePrevistoQuintoOttoNew As String
    Public Property propTolleranzaPiupropValorePrevistoQuintoNoveNew As String
    Public Property propTolleranzaPiupropValorePrevistoQuintoDieciNew As String

    Public Property propTolleranzaMenopropValorePrevistoQuintoUnoNew As String
    Public Property propTolleranzaMenopropValorePrevistoQuintoDueNew As String
    Public Property propTolleranzaMenopropValorePrevistoQuintoTreNew As String
    Public Property propTolleranzaMenopropValorePrevistoQuintoQuattroNew As String
    Public Property propTolleranzaMenopropValorePrevistoQuintoCinqueNew As String
    Public Property propTolleranzaMenopropValorePrevistoQuintoSeiNew As String
    Public Property propTolleranzaMenopropValorePrevistoQuintoSetteNew As String
    Public Property propTolleranzaMenopropValorePrevistoQuintoOttoNew As String
    Public Property propTolleranzaMenopropValorePrevistoQuintoNoveNew As String
    Public Property propTolleranzaMenopropValorePrevistoQuintoDieciNew As String

    Public Property propNoteQuintoPezzoNew As String


#End Region

#Region "Proprieta Ultimo Pezzo da report esistente"

    Public Property propIDPezziUltimoPezzoNew As Integer

    Public Property propValorePrevistoUltimoUnoNew As String
    Public Property propValorePrevistoUltimoDueNew As String
    Public Property propValorePrevistoUltimoTreNew As String
    Public Property propValorePrevistoUltimoQuattroNew As String
    Public Property propValorePrevistoUltimoCinqueNew As String
    Public Property propValorePrevistoUltimoSeiNew As String
    Public Property propValorePrevistoUltimoSetteNew As String
    Public Property propValorePrevistoUltimoOttoNew As String
    Public Property propValorePrevistoUltimoNoveNew As String
    Public Property propValorePrevistoUltimoDieciNew As String

    Public Property propValoreMisuratoUltimoUnoNew As String
    Public Property propValoreMisuratoUltimoDueNew As String
    Public Property propValoreMisuratoUltimoTreNew As String
    Public Property propValoreMisuratoUltimoQuattroNew As String
    Public Property propValoreMisuratoUltimoCinqueNew As String
    Public Property propValoreMisuratoUltimoSeiNew As String
    Public Property propValoreMisuratoUltimoSetteNew As String
    Public Property propValoreMisuratoUltimoOttoNew As String
    Public Property propValoreMisuratoUltimoNoveNew As String
    Public Property propValoreMisuratoUltimoDieciNew As String


    Public Property propTolleranzaPiupropValorePrevistoUltimoUnoNew As String
    Public Property propTolleranzaPiupropValorePrevistoUltimoDueNew As String
    Public Property propTolleranzaPiupropValorePrevistoUltimoTreNew As String
    Public Property propTolleranzaPiupropValorePrevistoUltimoQuattroNew As String
    Public Property propTolleranzaPiupropValorePrevistoUltimoCinqueNew As String
    Public Property propTolleranzaPiupropValorePrevistoUltimoSeiNew As String
    Public Property propTolleranzaPiupropValorePrevistoUltimoSetteNew As String
    Public Property propTolleranzaPiupropValorePrevistoUltimoOttoNew As String
    Public Property propTolleranzaPiupropValorePrevistoUltimoNoveNew As String
    Public Property propTolleranzaPiupropValorePrevistoUltimoDieciNew As String

    Public Property propTolleranzaMenopropValorePrevistoUltimoUnoNew As String
    Public Property propTolleranzaMenopropValorePrevistoUltimoDueNew As String
    Public Property propTolleranzaMenopropValorePrevistoUltimoTreNew As String
    Public Property propTolleranzaMenopropValorePrevistoUltimoQuattroNew As String
    Public Property propTolleranzaMenopropValorePrevistoUltimoCinqueNew As String
    Public Property propTolleranzaMenopropValorePrevistoUltimoSeiNew As String
    Public Property propTolleranzaMenopropValorePrevistoUltimoSetteNew As String
    Public Property propTolleranzaMenopropValorePrevistoUltimoOttoNew As String
    Public Property propTolleranzaMenopropValorePrevistoUltimoNoveNew As String
    Public Property propTolleranzaMenopropValorePrevistoUltimoDieciNew As String

    Public Property propNoteUltimoPezzoNew As String

#End Region


#Region "Proprieta Testata Per Stampa"
    Public Property propIdIntestazioneStampa As Integer
    Public Property propDataReportStampa As Date
    Public Property propNumeroOrdineStampa As String
    Public Property propCodiceArticoloStampa As String
    Public Property propMaterialeStampa As String
    Public Property propStrumentoStampa As String
    Public Property propMacchinaNumStampa As String
    Public Property propRigaOrdNumStampa As Integer
    Public Property propNumPezziStampa As Integer   'Numero Pezzi dopo riga ordine
    Public Property propFornitoreStampa As String
    Public Property propNumLottoStampa As String
    Public Property propPrimoPezzoStampa As Boolean
    Public Property propUltimoPezzoStampa As Boolean
    Public Property propPezziControllatiStampa As String 'Numero pezzi dopo ceckbox
    Public Property rowIndexGridViewTestataStampa As Integer
    Public Property propOperatoreStampa As String
    Public Property propImageAsBytesPerStampa As Byte
#End Region



#Region "Proprieta Primo Pezzo Per Stampa"


    Public Property propIDPezziPrimoPezzoStampa As Integer

    Public Property propValorePrevistoPrimoUnoStampa As String
    Public Property propValorePrevistoPrimoDueStampa As String
    Public Property propValorePrevistoPrimoTreStampa As String
    Public Property propValorePrevistoPrimoQuattroStampa As String
    Public Property propValorePrevistoPrimoCinqueStampa As String
    Public Property propValorePrevistoPrimoSeiStampa As String
    Public Property propValorePrevistoPrimoSetteStampa As String
    Public Property propValorePrevistoPrimoOttoStampa As String
    Public Property propValorePrevistoPrimoNoveStampa As String
    Public Property propValorePrevistoPrimoDieciStampa As String

    Public Property propValoreMisuratoPrimoUnoStampa As String
    Public Property propValoreMisuratoPrimoDueStampa As String
    Public Property propValoreMisuratoPrimoTreStampa As String
    Public Property propValoreMisuratoPrimoQuattroStampa As String
    Public Property propValoreMisuratoPrimoCinqueStampa As String
    Public Property propValoreMisuratoPrimoSeiStampa As String
    Public Property propValoreMisuratoPrimoSetteStampa As String
    Public Property propValoreMisuratoPrimoOttoStampa As String
    Public Property propValoreMisuratoPrimoNoveStampa As String
    Public Property propValoreMisuratoPrimoDieciStampa As String


    Public Property propTolleranzaPiupropValorePrevistoPrimoUnoStampa As String
    Public Property propTolleranzaPiupropValorePrevistoPrimoDueStampa As String
    Public Property propTolleranzaPiupropValorePrevistoPrimoTreStampa As String
    Public Property propTolleranzaPiupropValorePrevistoPrimoQuattroStampa As String
    Public Property propTolleranzaPiupropValorePrevistoPrimoCinqueStampa As String
    Public Property propTolleranzaPiupropValorePrevistoPrimoSeiStampa As String
    Public Property propTolleranzaPiupropValorePrevistoPrimoSetteStampa As String
    Public Property propTolleranzaPiupropValorePrevistoPrimoOttoStampa As String
    Public Property propTolleranzaPiupropValorePrevistoPrimoNoveStampa As String
    Public Property propTolleranzaPiupropValorePrevistoPrimoDieciStampa As String

    Public Property propTolleranzaMenopropValorePrevistoPrimoUnoStampa As String
    Public Property propTolleranzaMenopropValorePrevistoPrimoDueStampa As String
    Public Property propTolleranzaMenopropValorePrevistoPrimoTreStampa As String
    Public Property propTolleranzaMenopropValorePrevistoPrimoQuattroStampa As String
    Public Property propTolleranzaMenopropValorePrevistoPrimoCinqueStampa As String
    Public Property propTolleranzaMenopropValorePrevistoPrimoSeiStampa As String
    Public Property propTolleranzaMenopropValorePrevistoPrimoSetteStampa As String
    Public Property propTolleranzaMenopropValorePrevistoPrimoOttoStampa As String
    Public Property propTolleranzaMenopropValorePrevistoPrimoNoveStampa As String
    Public Property propTolleranzaMenopropValorePrevistoPrimoDieciStampa As String

    Public Property propNotePrimoPezzoStampa As String

#End Region

#Region "Proprieta Secondo Pezzo Per Stampa"

    Public Property propIDPezziSecondoPezzoStampa As Integer

    Public Property propValorePrevistoSecondoUnoStampa As String
    Public Property propValorePrevistoSecondoDueStampa As String
    Public Property propValorePrevistoSecondoTreStampa As String
    Public Property propValorePrevistoSecondoQuattroStampa As String
    Public Property propValorePrevistoSecondoCinqueStampa As String
    Public Property propValorePrevistoSecondoSeiStampa As String
    Public Property propValorePrevistoSecondoSetteStampa As String
    Public Property propValorePrevistoSecondoOttoStampa As String
    Public Property propValorePrevistoSecondoNoveStampa As String
    Public Property propValorePrevistoSecondoDieciStampa As String

    Public Property propValoreMisuratoSecondoUnoStampa As String
    Public Property propValoreMisuratoSecondoDueStampa As String
    Public Property propValoreMisuratoSecondoTreStampa As String
    Public Property propValoreMisuratoSecondoQuattroStampa As String
    Public Property propValoreMisuratoSecondoCinqueStampa As String
    Public Property propValoreMisuratoSecondoSeiStampa As String
    Public Property propValoreMisuratoSecondoSetteStampa As String
    Public Property propValoreMisuratoSecondoOttoStampa As String
    Public Property propValoreMisuratoSecondoNoveStampa As String
    Public Property propValoreMisuratoSecondoDieciStampa As String


    Public Property propTolleranzaPiupropValorePrevistoSecondoUnoStampa As String
    Public Property propTolleranzaPiupropValorePrevistoSecondoDueStampa As String
    Public Property propTolleranzaPiupropValorePrevistoSecondoTreStampa As String
    Public Property propTolleranzaPiupropValorePrevistoSecondoQuattroStampa As String
    Public Property propTolleranzaPiupropValorePrevistoSecondoCinqueStampa As String
    Public Property propTolleranzaPiupropValorePrevistoSecondoSeiStampa As String
    Public Property propTolleranzaPiupropValorePrevistoSecondoSetteStampa As String
    Public Property propTolleranzaPiupropValorePrevistoSecondoOttoStampa As String
    Public Property propTolleranzaPiupropValorePrevistoSecondoNoveStampa As String
    Public Property propTolleranzaPiupropValorePrevistoSecondoDieciStampa As String

    Public Property propTolleranzaMenopropValorePrevistoSecondoUnoStampa As String
    Public Property propTolleranzaMenopropValorePrevistoSecondoDueStampa As String
    Public Property propTolleranzaMenopropValorePrevistoSecondoTreStampa As String
    Public Property propTolleranzaMenopropValorePrevistoSecondoQuattroStampa As String
    Public Property propTolleranzaMenopropValorePrevistoSecondoCinqueStampa As String
    Public Property propTolleranzaMenopropValorePrevistoSecondoSeiStampa As String
    Public Property propTolleranzaMenopropValorePrevistoSecondoSetteStampa As String
    Public Property propTolleranzaMenopropValorePrevistoSecondoOttoStampa As String
    Public Property propTolleranzaMenopropValorePrevistoSecondoNoveStampa As String
    Public Property propTolleranzaMenopropValorePrevistoSecondoDieciStampa As String

    Public Property propNoteSecondoPezzoStampa As String

#End Region

#Region "Proprieta Terzo Pezzo Per Stampa"

    Public Property propIDPezziTerzoPezzoStampa As Integer

    Public Property propValorePrevistoTerzoUnoStampa As String
    Public Property propValorePrevistoTerzoDueStampa As String
    Public Property propValorePrevistoTerzoTreStampa As String
    Public Property propValorePrevistoTerzoQuattroStampa As String
    Public Property propValorePrevistoTerzoCinqueStampa As String
    Public Property propValorePrevistoTerzoSeiStampa As String
    Public Property propValorePrevistoTerzoSetteStampa As String
    Public Property propValorePrevistoTerzoOttoStampa As String
    Public Property propValorePrevistoTerzoNoveStampa As String
    Public Property propValorePrevistoTerzoDieciStampa As String

    Public Property propValoreMisuratoTerzoUnoStampa As String
    Public Property propValoreMisuratoTerzoDueStampa As String
    Public Property propValoreMisuratoTerzoTreStampa As String
    Public Property propValoreMisuratoTerzoQuattroStampa As String
    Public Property propValoreMisuratoTerzoCinqueStampa As String
    Public Property propValoreMisuratoTerzoSeiStampa As String
    Public Property propValoreMisuratoTerzoSetteStampa As String
    Public Property propValoreMisuratoTerzoOttoStampa As String
    Public Property propValoreMisuratoTerzoNoveStampa As String
    Public Property propValoreMisuratoTerzoDieciStampa As String


    Public Property propTolleranzaPiupropValorePrevistoTerzoUnoStampa As String
    Public Property propTolleranzaPiupropValorePrevistoTerzoDueStampa As String
    Public Property propTolleranzaPiupropValorePrevistoTerzoTreStampa As String
    Public Property propTolleranzaPiupropValorePrevistoTerzoQuattroStampa As String
    Public Property propTolleranzaPiupropValorePrevistoTerzoCinqueStampa As String
    Public Property propTolleranzaPiupropValorePrevistoTerzoSeiStampa As String
    Public Property propTolleranzaPiupropValorePrevistoTerzoSetteStampa As String
    Public Property propTolleranzaPiupropValorePrevistoTerzoOttoStampa As String
    Public Property propTolleranzaPiupropValorePrevistoTerzoNoveStampa As String
    Public Property propTolleranzaPiupropValorePrevistoTerzoDieciStampa As String

    Public Property propTolleranzaMenopropValorePrevistoTerzoUnoStampa As String
    Public Property propTolleranzaMenopropValorePrevistoTerzoDueStampa As String
    Public Property propTolleranzaMenopropValorePrevistoTerzoTreStampa As String
    Public Property propTolleranzaMenopropValorePrevistoTerzoQuattroStampa As String
    Public Property propTolleranzaMenopropValorePrevistoTerzoCinqueStampa As String
    Public Property propTolleranzaMenopropValorePrevistoTerzoSeiStampa As String
    Public Property propTolleranzaMenopropValorePrevistoTerzoSetteStampa As String
    Public Property propTolleranzaMenopropValorePrevistoTerzoOttoStampa As String
    Public Property propTolleranzaMenopropValorePrevistoTerzoNoveStampa As String
    Public Property propTolleranzaMenopropValorePrevistoTerzoDieciStampa As String

    Public Property propNoteTerzoPezzoStampa As String

#End Region


#Region "Proprieta Quarto Pezzo Per Stampa"

    Public Property propIDPezziQuartoPezzoStampa As Integer

    Public Property propValorePrevistoQuartoUnoStampa As String
    Public Property propValorePrevistoQuartoDueStampa As String
    Public Property propValorePrevistoQuartoTreStampa As String
    Public Property propValorePrevistoQuartoQuattroStampa As String
    Public Property propValorePrevistoQuartoCinqueStampa As String
    Public Property propValorePrevistoQuartoSeiStampa As String
    Public Property propValorePrevistoQuartoSetteStampa As String
    Public Property propValorePrevistoQuartoOttoStampa As String
    Public Property propValorePrevistoQuartoNoveStampa As String
    Public Property propValorePrevistoQuartoDieciStampa As String

    Public Property propValoreMisuratoQuartoUnoStampa As String
    Public Property propValoreMisuratoQuartoDueStampa As String
    Public Property propValoreMisuratoQuartoTreStampa As String
    Public Property propValoreMisuratoQuartoQuattroStampa As String
    Public Property propValoreMisuratoQuartoCinqueStampa As String
    Public Property propValoreMisuratoQuartoSeiStampa As String
    Public Property propValoreMisuratoQuartoSetteStampa As String
    Public Property propValoreMisuratoQuartoOttoStampa As String
    Public Property propValoreMisuratoQuartoNoveStampa As String
    Public Property propValoreMisuratoQuartoDieciStampa As String


    Public Property propTolleranzaPiupropValorePrevistoQuartoUnoStampa As String
    Public Property propTolleranzaPiupropValorePrevistoQuartoDueStampa As String
    Public Property propTolleranzaPiupropValorePrevistoQuartoTreStampa As String
    Public Property propTolleranzaPiupropValorePrevistoQuartoQuattroStampa As String
    Public Property propTolleranzaPiupropValorePrevistoQuartoCinqueStampa As String
    Public Property propTolleranzaPiupropValorePrevistoQuartoSeiStampa As String
    Public Property propTolleranzaPiupropValorePrevistoQuartoSetteStampa As String
    Public Property propTolleranzaPiupropValorePrevistoQuartoOttoStampa As String
    Public Property propTolleranzaPiupropValorePrevistoQuartoNoveStampa As String
    Public Property propTolleranzaPiupropValorePrevistoQuartoDieciStampa As String

    Public Property propTolleranzaMenopropValorePrevistoQuartoUnoStampa As String
    Public Property propTolleranzaMenopropValorePrevistoQuartoDueStampa As String
    Public Property propTolleranzaMenopropValorePrevistoQuartoTreStampa As String
    Public Property propTolleranzaMenopropValorePrevistoQuartoQuattroStampa As String
    Public Property propTolleranzaMenopropValorePrevistoQuartoCinqueStampa As String
    Public Property propTolleranzaMenopropValorePrevistoQuartoSeiStampa As String
    Public Property propTolleranzaMenopropValorePrevistoQuartoSetteStampa As String
    Public Property propTolleranzaMenopropValorePrevistoQuartoOttoStampa As String
    Public Property propTolleranzaMenopropValorePrevistoQuartoNoveStampa As String
    Public Property propTolleranzaMenopropValorePrevistoQuartoDieciStampa As String

    Public Property propNoteQuartoPezzoStampa As String

#End Region

#Region "Proprieta Quinto Pezzo Per Stampa"

    Public Property propIDPezziQuintoPezzoStampa As Integer

    Public Property propValorePrevistoQuintoUnoStampa As String
    Public Property propValorePrevistoQuintoDueStampa As String
    Public Property propValorePrevistoQuintoTreStampa As String
    Public Property propValorePrevistoQuintoQuattroStampa As String
    Public Property propValorePrevistoQuintoCinqueStampa As String
    Public Property propValorePrevistoQuintoSeiStampa As String
    Public Property propValorePrevistoQuintoSetteStampa As String
    Public Property propValorePrevistoQuintoOttoStampa As String
    Public Property propValorePrevistoQuintoNoveStampa As String
    Public Property propValorePrevistoQuintoDieciStampa As String

    Public Property propValoreMisuratoQuintoUnoStampa As String
    Public Property propValoreMisuratoQuintoDueStampa As String
    Public Property propValoreMisuratoQuintoTreStampa As String
    Public Property propValoreMisuratoQuintoQuattroStampa As String
    Public Property propValoreMisuratoQuintoCinqueStampa As String
    Public Property propValoreMisuratoQuintoSeiStampa As String
    Public Property propValoreMisuratoQuintoSetteStampa As String
    Public Property propValoreMisuratoQuintoOttoStampa As String
    Public Property propValoreMisuratoQuintoNoveStampa As String
    Public Property propValoreMisuratoQuintoDieciStampa As String


    Public Property propTolleranzaPiupropValorePrevistoQuintoUnoStampa As String
    Public Property propTolleranzaPiupropValorePrevistoQuintoDueStampa As String
    Public Property propTolleranzaPiupropValorePrevistoQuintoTreStampa As String
    Public Property propTolleranzaPiupropValorePrevistoQuintoQuattroStampa As String
    Public Property propTolleranzaPiupropValorePrevistoQuintoCinqueStampa As String
    Public Property propTolleranzaPiupropValorePrevistoQuintoSeiStampa As String
    Public Property propTolleranzaPiupropValorePrevistoQuintoSetteStampa As String
    Public Property propTolleranzaPiupropValorePrevistoQuintoOttoStampa As String
    Public Property propTolleranzaPiupropValorePrevistoQuintoNoveStampa As String
    Public Property propTolleranzaPiupropValorePrevistoQuintoDieciStampa As String

    Public Property propTolleranzaMenopropValorePrevistoQuintoUnoStampa As String
    Public Property propTolleranzaMenopropValorePrevistoQuintoDueStampa As String
    Public Property propTolleranzaMenopropValorePrevistoQuintoTreStampa As String
    Public Property propTolleranzaMenopropValorePrevistoQuintoQuattroStampa As String
    Public Property propTolleranzaMenopropValorePrevistoQuintoCinqueStampa As String
    Public Property propTolleranzaMenopropValorePrevistoQuintoSeiStampa As String
    Public Property propTolleranzaMenopropValorePrevistoQuintoSetteStampa As String
    Public Property propTolleranzaMenopropValorePrevistoQuintoOttoStampa As String
    Public Property propTolleranzaMenopropValorePrevistoQuintoNoveStampa As String
    Public Property propTolleranzaMenopropValorePrevistoQuintoDieciStampa As String

    Public Property propNoteQuintoPezzoStampa As String

#End Region

#Region "Proprieta Ultimo Pezzo Per Stampa"

    Public Property propIDPezziUltimoPezzoStampa As Integer

    Public Property propValorePrevistoUltimoUnoStampa As String
    Public Property propValorePrevistoUltimoDueStampa As String
    Public Property propValorePrevistoUltimoTreStampa As String
    Public Property propValorePrevistoUltimoQuattroStampa As String
    Public Property propValorePrevistoUltimoCinqueStampa As String
    Public Property propValorePrevistoUltimoSeiStampa As String
    Public Property propValorePrevistoUltimoSetteStampa As String
    Public Property propValorePrevistoUltimoOttoStampa As String
    Public Property propValorePrevistoUltimoNoveStampa As String
    Public Property propValorePrevistoUltimoDieciStampa As String

    Public Property propValoreMisuratoUltimoUnoStampa As String
    Public Property propValoreMisuratoUltimoDueStampa As String
    Public Property propValoreMisuratoUltimoTreStampa As String
    Public Property propValoreMisuratoUltimoQuattroStampa As String
    Public Property propValoreMisuratoUltimoCinqueStampa As String
    Public Property propValoreMisuratoUltimoSeiStampa As String
    Public Property propValoreMisuratoUltimoSetteStampa As String
    Public Property propValoreMisuratoUltimoOttoStampa As String
    Public Property propValoreMisuratoUltimoNoveStampa As String
    Public Property propValoreMisuratoUltimoDieciStampa As String


    Public Property propTolleranzaPiupropValorePrevistoUltimoUnoStampa As String
    Public Property propTolleranzaPiupropValorePrevistoUltimoDueStampa As String
    Public Property propTolleranzaPiupropValorePrevistoUltimoTreStampa As String
    Public Property propTolleranzaPiupropValorePrevistoUltimoQuattroStampa As String
    Public Property propTolleranzaPiupropValorePrevistoUltimoCinqueStampa As String
    Public Property propTolleranzaPiupropValorePrevistoUltimoSeiStampa As String
    Public Property propTolleranzaPiupropValorePrevistoUltimoSetteStampa As String
    Public Property propTolleranzaPiupropValorePrevistoUltimoOttoStampa As String
    Public Property propTolleranzaPiupropValorePrevistoUltimoNoveStampa As String
    Public Property propTolleranzaPiupropValorePrevistoUltimoDieciStampa As String

    Public Property propTolleranzaMenopropValorePrevistoUltimoUnoStampa As String
    Public Property propTolleranzaMenopropValorePrevistoUltimoDueStampa As String
    Public Property propTolleranzaMenopropValorePrevistoUltimoTreStampa As String
    Public Property propTolleranzaMenopropValorePrevistoUltimoQuattroStampa As String
    Public Property propTolleranzaMenopropValorePrevistoUltimoCinqueStampa As String
    Public Property propTolleranzaMenopropValorePrevistoUltimoSeiStampa As String
    Public Property propTolleranzaMenopropValorePrevistoUltimoSetteStampa As String
    Public Property propTolleranzaMenopropValorePrevistoUltimoOttoStampa As String
    Public Property propTolleranzaMenopropValorePrevistoUltimoNoveStampa As String
    Public Property propTolleranzaMenopropValorePrevistoUltimoDieciStampa As String

    Public Property propNoteUltimoPezzoStampa As String

#End Region






End Module
