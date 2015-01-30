module internal FsPrettyTable.ColumnFiltering

let headerAndRows t =
    if t.Headers.Length > 0
    then t.Headers :: t.Rows
    else t.Rows

let columnFilter t =
    let filter (isIncluded:bool []) =
        (fun row ->
            row |> List.mapi (fun i v -> (isIncluded.[i], v))
                |> List.filter fst
                |> List.map snd)
    match t.Transformation.OnlyColumns with
    | Some (OnlyColumnChoice f) ->
        let headers = t.Headers |> List.toArray
        filter <| Array.init (headers.Length) 
                    (fun i -> f i headers.[i])
    | Some (OnlyColumnIndexes is) ->
        filter <| Array.init (List.length (List.head t.Rows)) 
                    (fun i -> is |> List.exists ((=) i))
    | Some (OnlyColumnHeaders xs) ->
        let headers = t.Headers |> List.toArray
        filter <| Array.init (headers.Length)
                    (fun i -> xs |> List.exists (fun v -> v = headers.[i]))
    | None -> id

type FsPrettyTable.Types.Table with
    member x.FilteredHeaderAndRows = 
        headerAndRows x |> List.map (columnFilter x)
