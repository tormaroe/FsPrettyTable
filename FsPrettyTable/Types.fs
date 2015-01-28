/// Core types
[<AutoOpen>]
module FsPrettyTable.Types

type HeaderStyle = Capitalise | TitleCase | UpperCase | LowerCase | KeepAsIs

(*
TITLE CASE: http://www.grammar-monster.com/lessons/capital_letters_title_case.htm
Must implement algo. for this.
*)

type Rules = Frame | All | NoRules
type HorizontalAlignment = Left | Center | Right
type VerticalAlignment = Top | Middle | Bottom
type SortDirection = Ascending | Descending

type Style = 
    {
        HasBorder : bool  // not implemented
        HasHeader : bool
        HeaderStyle : HeaderStyle  // partially implemented
        HorizontalRules : Rules  // not implemented
        VerticalRules : Rules  // not implemented
        HorizontalAlignment : HorizontalAlignment
        VerticalAlignment : VerticalAlignment // not implemented
        PaddingWidth : int
        LeftPaddingWidth : int option  // not implemented
        RightPaddingWidth : int option  // not implemented
        VerticalChar : char
        HorizontalChar : char
        JunctionChar : char
    }

type Transformation =
    {
        SortBy : (string * SortDirection) option
        OnlyColumns : string list option
    }

type Table =
    {
        (* Data *)
        Rows : string list list // TODO: REFACTOR, SEPARATE ROWS AND HEADERS

        Style : Style
        Transformation : Transformation
    } 
    


