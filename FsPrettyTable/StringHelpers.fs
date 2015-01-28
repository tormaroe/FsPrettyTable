/// Some general helper functions dealing with strings
module internal FsPrettyTable.StringHelpers

let toString (x:System.Object) = x.ToString ()

let charsToString (cs:char list) =
    System.String.Join ( "", Array.ofList cs )

let strJoinArr sep (xs:string []) =
    System.String.Join ( sep, xs )

let strJoin sep xs =
    strJoinArr sep <| Array.ofList xs

let sreplicate n x =
    List.replicate n x 
    |> charsToString

let newline = System.Environment.NewLine

let sappend x y : string = y + x
