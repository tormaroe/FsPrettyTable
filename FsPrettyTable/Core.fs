/// Core logic
module internal FsPrettyTable.Core

let defaultTable = 
    { Style = { HasBorder = true
                HasHeader = false // until a header is set
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
                JunctionChar = '+' }
      Transformation = { SortBy = None
                         OnlyColumns = None }
      Headers = []
      Rows = [] }

let headerAndRows t =
    if t.Headers.Length > 0
    then t.Headers :: t.Rows
    else t.Rows

type FsPrettyTable.Types.Table with
    member x.FilteredRows =
        // This code was reviewed here:
        // http://codereview.stackexchange.com/questions/78778/
        match x.Transformation.OnlyColumns with
        | None -> headerAndRows x
        | Some xs -> 
            let headers = x.Headers |> List.toArray
            let isIncluded = Array.init (headers.Length)
                                (fun i -> 
                                    xs |> List.exists 
                                            (fun v -> v = headers.[i]))
            headerAndRows x
            |> List.map 
                (fun row ->
                    row |> List.mapi (fun i v -> (isIncluded.[i], v))
                        |> List.filter fst
                        |> List.map snd)

let calcPaddingSums t =
    let getPadding f =
        match f () with
        | Some i -> i
        | None -> t.Style.PaddingWidth
    t.FilteredRows
    |> List.head
    |> List.map  // Prepared for individual column padding
        (fun _ ->
            (getPadding (fun () -> t.Style.LeftPaddingWidth))
            +
            (getPadding (fun () -> t.Style.RightPaddingWidth)))

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
    match t.Transformation.SortBy with
    | None -> rows
    | Some (field, direction) ->
        let fieldSelector = makeSortFieldSelectorBy field
        rows
        |> List.sortBy fieldSelector
        |> if direction = Ascending
           then id
           else List.rev
