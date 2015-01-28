/// Output formatting 
module internal FsPrettyTable.Format

open System.Text
open FsPrettyTable.Core
open FsPrettyTable.StringHelpers

let sprintHorizontalRule ws t =
    let junction = string t.JunctionChar
    ws 
    |> List.map 
        (fun w ->
            t.JunctionChar :: List.replicate w t.HorizontalChar)
    |> (List.concat >> charsToString)
    |> sappend junction

let sprintRow ws s row =
    let vertical = if s.HasBorder then (string s.VerticalChar) else ""
    let f value width =
        let lPad, rPad = calcCellPadding value width s
        vertical
        + (sreplicate lPad ' ') 
        + value 
        + (sreplicate rPad ' ') 
    List.map2 f row ws
    |> strJoin ""
    |> sappend vertical

let doHeaderStyle t row =
    let f = 
        match t.HeaderStyle with
        | LowerCase -> (fun (x:string) -> x.ToLower())
        | UpperCase -> (fun (x:string) -> x.ToUpper())
        | TitleCase -> id  // Not implemented
        | Capitalise -> id // Not implemented
        | KeepAsIs -> id
    row |> List.map f

let appendSB (x:string) (sb:StringBuilder) = sb.Append x

let appendHorizontalRule (hr:string) s (sb:StringBuilder) =
    if s.HasBorder && s.HorizontalRules <> NoRules 
    then sb.Append hr
    else sb

let sprintTable' (t:Table) =
    let t = if t.Style.VerticalRules = NoRules 
            then { t with Style = { t.Style with JunctionChar = '-'
                                                 VerticalChar = ' ' } }
            else t
    let rows = t.FilteredHeaderAndRows
    let colWidths = calcColWidth t
    let hr = (sprintHorizontalRule colWidths t.Style) + newline
    let sprintRow' = sprintRow colWidths t.Style
    let header = (doHeaderStyle t.Style >> sprintRow') (List.head rows)
                 + newline
    let data = if t.Style.HasHeader then List.tail rows else rows
               |> sortIfNeeded t
               |> List.map sprintRow'

    new StringBuilder ()
    |> if t.Style.HasHeader then appendHorizontalRule hr t.Style else id
    |> if t.Style.HasHeader then appendSB header else id
    |> appendHorizontalRule hr t.Style
    |> appendSB (data |> strJoin newline)
    |> appendSB newline
    |> appendHorizontalRule hr t.Style
    |> toString


