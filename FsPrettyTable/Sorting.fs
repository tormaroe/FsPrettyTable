module internal FsPrettyTable.Sorting

open FsPrettyTable.ColumnFiltering

let private applyDirection d =
    if d = Ascending then id else List.rev

let private sort projection d rows =
    rows |> List.sortBy projection
         |> applyDirection d

let sortIfNeeded (t:Table) rows =
    let makeSortFieldSelectorBy header =
        let headers = List.head t.FilteredHeaderAndRows
        let index = headers |> List.findIndex (fun x -> x = header)
        (fun row -> List.nth row index)
    match t.Transformation.SortBy with
    | None -> rows
    | Some (SortByHeader (field, direction)) ->
        let fieldSelector = makeSortFieldSelectorBy field
        rows |> sort fieldSelector direction
    | Some (SortByIndex (i, direction)) ->
        failwith "Not yet implemented" 
