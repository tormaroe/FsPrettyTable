module FsPrettyTable.Validation

let private (<||>) f g x = f x || g x

type ValidationResult =
    | Valid
    | Invalid of string list

let private invalidUnless reason b =
    if b then Valid else Invalid [reason]

let private isInvalid = function
    | Valid -> false
    | Invalid _ -> true

let private ``Empty table`` t =
    (List.length t.Rows > 0 || List.length (List.head t.Rows) > 0)
    |> invalidUnless "An empty table is no table"

let private ``Different row length`` t =
    let baseline = List.head t.Rows |> List.length
    if t.Headers.Length > 0 then t.Headers :: t.Rows else t.Rows
    |> List.map List.length
    |> List.forall ((=) baseline)
    |> invalidUnless 
        "All rows (including header if added) must be of same length"

let private ``Invalid filter when no headers`` t =
    let filter = t.Transformation.OnlyColumns
    let headerCount = t.Headers.Length
    match headerCount, filter with
    | n, _ when n > 0 -> Valid
    | _, None -> Valid
    | _, Some (OnlyColumnIndexes _) -> Valid
    | 0, Some (OnlyColumnHeaders _) -> 
        Invalid ["Don't filter columns by header when table has no headers"]
    | 0, Some (OnlyColumnChoice _) ->
        Invalid ["Don't filter columns by predicate when table has no headers"]

let private ``Invalid index in filter`` t =
    match t.Transformation.OnlyColumns with
    | Some (OnlyColumnIndexes xs) -> 
        let max = (List.head t.Rows |> List.length) - 1
        let invalids = xs |> List.filter (((>) 0) <||> ((<) max))
        match invalids with
        | [] -> Valid
        | xs -> xs |> List.map (sprintf "%d in column filter is not a valid column index")
                   |> Invalid
    | _ -> Valid

// TODO: Many additional validations can be added...

let validate t =
    let invalids =
        [ ``Empty table``
          ``Different row length``
          ``Invalid filter when no headers``
          ``Invalid index in filter`` ]
        |> List.map (fun v -> v t)
        |> List.filter isInvalid
    if invalids.Length = 0
    then Valid
    else
        invalids
        |> List.fold
            (fun (Invalid acc) (Invalid reasons) ->
                List.append acc reasons |> Invalid)
            (Invalid [])

exception ValidationError of string list

let internal failIfNotValid t =
    match validate t with
    | Valid -> ()
    | Invalid xs -> raise (ValidationError xs)