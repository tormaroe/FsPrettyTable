/// Core types
[<AutoOpen>]
module FsPrettyTable.Types

type HeaderStyle = Capitalise | TitleCase | UpperCase | LowerCase | KeepAsIs
type Rules = Frame | All | NoRules
type HorizontalAlignment = Left | Center | Right
type VerticalAlignment = Top | Middle | Bottom
type SortDirection = Ascending | Descending

type Style = {
    HasBorder : bool
    HasHeader : bool
    HeaderStyle : HeaderStyle  // partially implemented
    HorizontalRules : Rules
    VerticalRules : Rules
    HorizontalAlignment : HorizontalAlignment
    VerticalAlignment : VerticalAlignment // not implemented
    PerColumnHorizontalAlignment : (string * HorizontalAlignment) list
    PaddingWidth : int
    LeftPaddingWidth : int option
    RightPaddingWidth : int option
    VerticalChar : char
    HorizontalChar : char
    JunctionChar : char }

type Transformation = {
    SortBy : (string * SortDirection) option
    OnlyColumns : string list option }

type Table = {
    Headers : string list
    Rows : string list list
    Style : Style
    Transformation : Transformation } 
    