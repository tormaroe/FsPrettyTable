/// Core logic
module internal FsPrettyTable.Core

let defaultTable = 
    { HasBorder = true
      HasHeader = true
      HeaderStyle = KeepAsIs
      HorizontalRules = Frame 
      VerticalRules = All 
      HorizontalAlignment = Center
      VerticalAlignment = Top
      PaddingWidth = 1
      LeftPaddingWidth = None
      RightPaddingWidth = None
      VerticalChar = '|'
      HorizontalChar = '-'
      JunctionChar = '+'
      SortBy = None
      OnlyColumns = None
      Rows = [] }

type FsPrettyTable.Types.Table with
    member x.FilteredRows =
        // This code was reviewed here:
        // http://codereview.stackexchange.com/questions/78778/
        match x.OnlyColumns with
        | None -> x.Rows
        | Some xs -> 
            let headers = List.head x.Rows |> List.toArray
            let isIncluded = Array.init (headers.Length)
                                (fun i -> 
                                    xs |> List.exists 
                                            (fun v -> v = headers.[i]))
            x.Rows |> List.map 
                        (fun row ->
                            row |> List.mapi (fun i v -> (isIncluded.[i], v))
                                |> List.filter fst
                                |> List.map snd)

let calcPaddingSums t =
    let getPadding f =
        match f () with
        | Some i -> i
        | None -> t.PaddingWidth
    t.FilteredRows
    |> List.head
    |> List.map  // Prepared for individual column padding
        (fun _ ->
            (getPadding (fun () -> t.LeftPaddingWidth))
            +
            (getPadding (fun () -> t.RightPaddingWidth)))

let calcColWidth t =
    let paddingSums = calcPaddingSums t
    let rows = t.FilteredRows
    seq {
        for i in 0 .. (List.length <| List.head rows) - 1 do
            let columnFields = rows 
                               |> List.map 
                                    (fun r -> 
                                        List.nth r i
                                        |> String.length)
            yield List.max columnFields
    }
    |> Seq.map2 (+) paddingSums
    |> List.ofSeq

let calcCellPadding value width t =
    let space = width - String.length value
    let lPad = 
        match t.HorizontalAlignment with
        | Center -> space / 2
        | Left -> 1
        | Right -> space - 1
    let rPad = space - lPad
    (lPad, rPad)

let sortIfNeeded (t:Table) rows =
    let makeSortFieldSelectorBy header =
        let headers = List.head t.FilteredRows
        let index = headers |> List.findIndex (fun x -> x = header)
        (fun row -> List.nth row index)
    match t.SortBy with
    | None -> rows
    | Some (field, direction) ->
        let fieldSelector = makeSortFieldSelectorBy field
        rows
        |> List.sortBy fieldSelector
        |> if direction = Ascending
           then id
           else List.rev
