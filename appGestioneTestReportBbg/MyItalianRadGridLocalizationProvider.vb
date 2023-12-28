Imports Telerik.WinControls.UI.Localization

Public Class MyItalianRadGridLocalizationProvider
    Inherits RadGridLocalizationProvider

    Public Overrides Function GetLocalizedString(id As String) As String

        Select Case id
            Case RadGridStringId.FilterFunctionBetween
                Return "SelezionaNelMezzoTra"
            Case RadGridStringId.FilterFunctionContains
                Return "Contiene"
            Case RadGridStringId.FilterFunctionDoesNotContain
                Return "NonContiene"
            Case RadGridStringId.FilterFunctionEndsWith
                Return "FinisceCon"
            Case RadGridStringId.FilterFunctionEqualTo
                Return "UgualeA"
            Case RadGridStringId.FilterFunctionGreaterThan
                Return "MaggioreDi"
            Case RadGridStringId.FilterFunctionGreaterThanOrEqualTo
                Return "MaggioreDiUgualeA"
            Case RadGridStringId.FilterFunctionIsEmpty
                Return "Vuoto"
            Case RadGridStringId.FilterFunctionIsNull
                Return "Nullo"
            Case RadGridStringId.FilterFunctionLessThan
                Return "MinoreDi"
            Case RadGridStringId.FilterFunctionLessThanOrEqualTo
                Return "MinoreDiUgualeA"
            Case RadGridStringId.FilterFunctionNoFilter
                Return "NonFiltro"
            Case RadGridStringId.FilterFunctionNotBetween
                Return "NonSelezionaNelMezzoTra"
            Case RadGridStringId.FilterFunctionNotEqualTo
                Return "NonUgualeA"
            Case RadGridStringId.FilterFunctionNotIsEmpty
                Return "NonVuoto"
            Case RadGridStringId.FilterFunctionNotIsNull
                Return "NonNullo"
            Case RadGridStringId.FilterFunctionStartsWith
                Return "IniziaCon"
            Case RadGridStringId.FilterFunctionCustom
                Return "Custom"

            Case RadGridStringId.FilterOperatorBetween
                Return "SelezionaNelMezzoTra"
            Case RadGridStringId.FilterOperatorContains
                Return "Contiene"
            Case RadGridStringId.FilterOperatorDoesNotContain
                Return "NonContiene"
            Case RadGridStringId.FilterOperatorEndsWith
                Return "FinisceCon"
            Case RadGridStringId.FilterOperatorEqualTo
                Return "UgualeA"
            Case RadGridStringId.FilterOperatorGreaterThan
                Return "MaggioreDi"
            Case RadGridStringId.FilterOperatorGreaterThanOrEqualTo
                Return "MaggioreDiUgualeA"
            Case RadGridStringId.FilterOperatorIsEmpty
                Return "Vuoto"
            Case RadGridStringId.FilterOperatorIsNull
                Return "Nullo"
            Case RadGridStringId.FilterOperatorLessThan
                Return "MinoreDi"
            Case RadGridStringId.FilterOperatorLessThanOrEqualTo
                Return "MinoreDiUgualeA"
            Case RadGridStringId.FilterOperatorNoFilter
                Return "NonFiltro"
            Case RadGridStringId.FilterOperatorNotBetween
                Return "NonSelezionaNelMezzoTra"
            Case RadGridStringId.FilterOperatorNotEqualTo
                Return "NonUgualeA"
            Case RadGridStringId.FilterOperatorNotIsEmpty
                Return "NonVuoto"
            Case RadGridStringId.FilterOperatorNotIsNull
                Return "NonNullo"
            Case RadGridStringId.FilterOperatorStartsWith
                Return "IniziaCon"
            Case RadGridStringId.FilterOperatorIsLike
                Return "ComeSe"
            Case RadGridStringId.FilterOperatorNotIsLike
                Return "DiversoDa"
            Case RadGridStringId.FilterOperatorIsContainedIn
                Return "ContenutoIn"
            Case RadGridStringId.FilterOperatorNotIsContainedIn
                Return "NonContenutoIn"
            Case RadGridStringId.FilterOperatorCustom
                Return "Custom"

            Case RadGridStringId.CustomFilterMenuItem
                Return "Custom"
            Case RadGridStringId.CustomFilterDialogCaption
                Return "RadGridView Finestra Filtri [{0}]"
            Case RadGridStringId.CustomFilterDialogLabel
                Return "Mostra righe dove:"
            Case RadGridStringId.CustomFilterDialogRbAnd
                Return "And"
            Case RadGridStringId.CustomFilterDialogRbOr
                Return "Or"
            Case RadGridStringId.CustomFilterDialogBtnOk
                Return "OK"
            Case RadGridStringId.CustomFilterDialogBtnCancel
                Return "Cancella"
            Case RadGridStringId.CustomFilterDialogCheckBoxNot
                Return "Not"
            Case RadGridStringId.CustomFilterDialogTrue
                Return "Vero"
            Case RadGridStringId.CustomFilterDialogFalse
                Return "Falso"

            Case RadGridStringId.FilterMenuAvailableFilters
                Return "Filtri Disponibili"
            Case RadGridStringId.FilterMenuSearchBoxText
                Return "Search..."
            Case RadGridStringId.FilterMenuClearFilters
                Return "Pulisci Filtri"
            Case RadGridStringId.FilterMenuButtonOK
                Return "OK"
            Case RadGridStringId.FilterMenuButtonCancel
                Return "Cancella"
            Case RadGridStringId.FilterMenuSelectionAll
                Return "Tutto"
            Case RadGridStringId.FilterMenuSelectionAllSearched
                Return "Tutti i Risultati Della Ricerca"
            Case RadGridStringId.FilterMenuSelectionNull
                Return "Null"
            Case RadGridStringId.FilterMenuSelectionNotNull
                Return "Not Null"

            Case RadGridStringId.FilterLogicalOperatorAnd
                Return "AND"
            Case RadGridStringId.FilterLogicalOperatorOr
                Return "OR"
            Case RadGridStringId.FilterCompositeNotOperator
                Return "NOT"

            Case RadGridStringId.DeleteRowMenuItem
                Return "Cancella Riga"
            Case RadGridStringId.SortAscendingMenuItem
                Return "Ordinamento Crescente"
            Case RadGridStringId.SortDescendingMenuItem
                Return "Ordinamento Decrescente"
            Case RadGridStringId.ClearSortingMenuItem
                Return "Togli Ordinamento"
            Case RadGridStringId.ConditionalFormattingMenuItem
                Return "Formattazione Condizionale"
            Case RadGridStringId.GroupByThisColumnMenuItem
                Return "Raggruppa Tramite Colonna"
            Case RadGridStringId.UngroupThisColumn
                Return "Separa Questa Colonna"
            Case RadGridStringId.ColumnChooserMenuItem
                Return "Scelta Colonne"
            Case RadGridStringId.HideMenuItem
                Return "Nascondi Colonna"
            Case RadGridStringId.UnpinMenuItem
                Return "Unpin Colonna"
            Case RadGridStringId.UnpinRowMenuItem
                Return "Unpin Riga"
            Case RadGridStringId.PinMenuItem
                Return "Pinned Stato"
            Case RadGridStringId.PinAtLeftMenuItem
                Return "Pin a Sinistra"
            Case RadGridStringId.PinAtRightMenuItem
                Return "Pin a Destra"
            Case RadGridStringId.PinAtBottomMenuItem
                Return "Pin a in Basso"
            Case RadGridStringId.PinAtTopMenuItem
                Return "Pin in Alto"
            Case RadGridStringId.BestFitMenuItem
                Return "Best Fit"
            Case RadGridStringId.PasteMenuItem
                Return "Paste"
            Case RadGridStringId.EditMenuItem
                Return "Edit"
            Case RadGridStringId.ClearValueMenuItem
                Return "Clear Value"
            Case RadGridStringId.CopyMenuItem
                Return "Copy"
            Case RadGridStringId.AddNewRowString
                Return "Click qui per aggiungere una nuova riga"
            Case RadGridStringId.ConditionalFormattingCaption
                Return "Conditional Formatting Rules Manager"
            Case RadGridStringId.ConditionalFormattingLblColumn
                Return "Format only cells with"
            Case RadGridStringId.ConditionalFormattingLblName
                Return "Rule name"
            Case RadGridStringId.ConditionalFormattingLblType
                Return "Cell value"
            Case RadGridStringId.ConditionalFormattingLblValue1
                Return "Value 1"
            Case RadGridStringId.ConditionalFormattingLblValue2
                Return "Value 2"
            Case RadGridStringId.ConditionalFormattingGrpConditions
                Return "Rules"
            Case RadGridStringId.ConditionalFormattingGrpProperties
                Return "Rule Properties"
            Case RadGridStringId.ConditionalFormattingChkApplyToRow
                Return "Apply this formatting to entire row"
            Case RadGridStringId.ConditionalFormattingChkApplyOnSelectedRows
                Return "Apply this formatting if the row is selected"
            Case RadGridStringId.ConditionalFormattingBtnAdd
                Return "Add new rule"
            Case RadGridStringId.ConditionalFormattingBtnRemove
                Return "Remove"
            Case RadGridStringId.ConditionalFormattingBtnOK
                Return "OK"
            Case RadGridStringId.ConditionalFormattingBtnCancel
                Return "Cancel"
            Case RadGridStringId.ConditionalFormattingBtnApply
                Return "Apply"
            Case RadGridStringId.ConditionalFormattingRuleAppliesOn
                Return "Rule applies to"
            Case RadGridStringId.ConditionalFormattingChooseOne
                Return "[Choose one]"
            Case RadGridStringId.ConditionalFormattingEqualsTo
                Return "equals to [Value1]"
            Case RadGridStringId.ConditionalFormattingIsNotEqualTo
                Return "is not equal to [Value1]"
            Case RadGridStringId.ConditionalFormattingStartsWith
                Return "starts with [Value1]"
            Case RadGridStringId.ConditionalFormattingEndsWith
                Return "ends with [Value1]"
            Case RadGridStringId.ConditionalFormattingContains
                Return "contains [Value1]"
            Case RadGridStringId.ConditionalFormattingDoesNotContain
                Return "does not contain [Value1]"
            Case RadGridStringId.ConditionalFormattingIsGreaterThan
                Return "is greater than [Value1]"
            Case RadGridStringId.ConditionalFormattingIsGreaterThanOrEqual
                Return "is greater than or equal [Value1]"
            Case RadGridStringId.ConditionalFormattingIsLessThan
                Return "is less than [Value1]"
            Case RadGridStringId.ConditionalFormattingIsLessThanOrEqual
                Return "is less than or equal to [Value1]"
            Case RadGridStringId.ConditionalFormattingIsBetween
                Return "is between [Value1] and [Value2]"
            Case RadGridStringId.ConditionalFormattingIsNotBetween
                Return "is not between [Value1] and [Value1]"
            Case RadGridStringId.ConditionalFormattingLblFormat
                Return "Format"
            Case RadGridStringId.ColumnChooserFormCaption
                Return "Scelta Colonne"
            Case RadGridStringId.ColumnChooserFormMessage
                Return "Trascinare una colonna da" & vbLf & "qui per rimuoverlo dalla griglia" & vbLf & "la vista corrente."
            Case RadGridStringId.GroupingPanelDefaultMessage
                Return "Trascinare una colonna qui per raggruppare con questa colonna."
            Case RadGridStringId.GroupingPanelHeader
                Return "Raggruppa con:"
            Case RadGridStringId.NoDataText
                Return "Non ci sono dati da mostrare"
            Case RadGridStringId.CompositeFilterFormErrorCaption
                Return "Errore Filtro"

            Case RadGridStringId.ExpressionMenuItem
                Return "Expression"
            Case RadGridStringId.ExpressionFormTitle
                Return "Expression Builder"
            Case RadGridStringId.ExpressionFormFunctions
                Return "Functions"
            Case RadGridStringId.ExpressionFormFunctionsText
                Return "Text"
            Case RadGridStringId.ExpressionFormFunctionsAggregate
                Return "Aggregate"
            Case RadGridStringId.ExpressionFormFunctionsDateTime
                Return "Date-Time"
            Case RadGridStringId.ExpressionFormFunctionsLogical
                Return "Logical"
            Case RadGridStringId.ExpressionFormFunctionsMath
                Return "Math"
            Case RadGridStringId.ExpressionFormFunctionsOther
                Return "Other"
            Case RadGridStringId.ExpressionFormOperators
                Return "Operators"
            Case RadGridStringId.ExpressionFormConstants
                Return "Constants"
            Case RadGridStringId.ExpressionFormFields
                Return "Fields"
            Case RadGridStringId.ExpressionFormDescription
                Return "Description"
            Case RadGridStringId.ExpressionFormResultPreview
                Return "Result preview"
            Case RadGridStringId.ExpressionFormTooltipPlus
                Return "Plus"
            Case RadGridStringId.ExpressionFormTooltipMinus
                Return "Minus"
            Case RadGridStringId.ExpressionFormTooltipMultiply
                Return "Multiply"
            Case RadGridStringId.ExpressionFormTooltipDivide
                Return "Divide"
            Case RadGridStringId.ExpressionFormTooltipModulo
                Return "Modulo"
            Case RadGridStringId.ExpressionFormTooltipEqual
                Return "Equal"
            Case RadGridStringId.ExpressionFormTooltipNotEqual
                Return "Not Equal"
            Case RadGridStringId.ExpressionFormTooltipLess
                Return "Less"
            Case RadGridStringId.ExpressionFormTooltipLessOrEqual
                Return "Less Or Equal"
            Case RadGridStringId.ExpressionFormTooltipGreaterOrEqual
                Return "Greater Or Equal"
            Case RadGridStringId.ExpressionFormTooltipGreater
                Return "Greater"
            Case RadGridStringId.ExpressionFormTooltipAnd
                Return "Logical ""AND"""
            Case RadGridStringId.ExpressionFormTooltipOr
                Return "Logical ""OR"""
            Case RadGridStringId.ExpressionFormTooltipNot
                Return "Logical ""NOT"""
            Case RadGridStringId.ExpressionFormAndButton
                Return String.Empty
                'if empty, default button image is used
            Case RadGridStringId.ExpressionFormOrButton
                Return String.Empty
                'if empty, default button image is used
            Case RadGridStringId.ExpressionFormNotButton
                Return String.Empty
                'if empty, default button image is used
            Case RadGridStringId.ExpressionFormOKButton
                Return "OK"
            Case RadGridStringId.ExpressionFormCancelButton
                Return "Cancel"
        End Select

        Return String.Empty

    End Function


End Class
