/// Some general helper functions dealing with strings
module internal FsPrettyTable.StringHelpers

open System

let toString (x:System.Object) = x.ToString ()

let charsToString (cs:char list) =
    String.Join ( "", Array.ofList cs )

let strJoinArr sep (xs:string []) =
    String.Join ( sep, xs )

let strJoin sep xs =
    strJoinArr sep <| Array.ofList xs

let sreplicate n x =
    List.replicate n x 
    |> charsToString

let newline = Environment.NewLine

let sappend x y : string = y + x

let toLower (x:string) = x.ToLower()

let regxIsMatch re x = 
    Text.RegularExpressions.Regex.IsMatch(x, re)