# API

Pre version 1.0 the API may change.

*TODO: Add info about validation*

## Module PrettyTable

    val prettyTable : string list list -> Table
    val withHeaders : string list -> Table -> Table

    val sprintTable : Table -> string
    val printTable : Table -> unit

    val hasHeader : bool -> Table -> Table
    val hasBorder : bool -> Table -> Table
    val verticalRules : Rules -> Table -> Table
    val horizontalRules : Rules -> Table -> Table

    val headerStyle : HeaderStyle -> Table -> Table

    val horizontalAlignment : HorizontalAlignment -> Table -> Table
    val horizontalAlignmentForColumn : string -> HorizontalAlignment -> Table -> Table

    val paddingWidth : int -> Table -> Table
    val leftPaddingWidth : int -> Table -> Table
    val rightPaddingWidth : int -> Table -> Table

    val verticalChar : char -> Table -> Table
    val horizontalChar : char -> Table -> Table
    val junctionChar : char -> Table -> Table

    val onlyColumns : string list -> Table -> Table
    val onlyColumnsByIndex : int list -> Table -> Table
    val onlyColumnsByChoice : (int -> string -> bool) -> Table -> Table
    val allColumns : Table -> Table

    val sortBy : string -> Table -> Table
    val sortByDescending : string -> Table -> Table
    val sortByNone Table -> Table

    type Style = DefaultStyle | PlainColumns | PlainRows | MsWordFriendly
    val setStyle : Style -> Table -> Table
