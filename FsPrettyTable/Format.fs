/// Output formatting 
module internal FsPrettyTable.Format

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

let sprintRow ws t row =
    let f value width =
        let lPad, rPad = calcCellPadding value width t
        (string t.VerticalChar) 
        + (sreplicate lPad ' ') 
        + value 
        + (sreplicate rPad ' ') 
    List.map2 f row ws
    |> strJoin ""
    |> sappend (string t.VerticalChar)

let doHeaderStyle t row =
    let f = 
        match t.HeaderStyle with
        | LowerCase -> (fun (x:string) -> x.ToLower())
        | UpperCase -> (fun (x:string) -> x.ToUpper())
        | TitleCase -> id  // Not implemented
        | Capitalise -> id // Not implemented
        | KeepAsIs -> id
    row |> List.map f

let sprintTable' (t:Table) =
    let rows = t.FilteredRows
    let colWidths = calcColWidth t
    let hr = sprintHorizontalRule colWidths t.Style
    let sprintRow' = sprintRow colWidths t.Style
    let header = [hr; (doHeaderStyle t.Style >> sprintRow') (List.head rows); hr]
    if t.Style.HasHeader then List.tail rows else rows
    |> sortIfNeeded t
    |> List.map sprintRow'
    |> if t.Style.HasHeader 
       then List.append header 
       else List.append [hr]
    |> strJoin newline
    |> sappend newline
    |> sappend hr
    |> sappend newline

