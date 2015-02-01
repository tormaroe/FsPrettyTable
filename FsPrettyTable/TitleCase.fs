module FsPrettyTable.TitleCase

(*
    This module implements function `titleCase` which will transform any string
    of English text to Title Case. It may not be linguistically correct in all
    cases.

    System.Globalization.TextInfo already have a ToTitleCase method, but it's 
    not great:

    "..the ToTitleCase method provides an arbitrary casing behavior which is 
     not necessarily linguistically correct. A linguistically correct solution 
     would require additional rules, and the current algorithm is somewhat 
     simpler and faster. We reserve the right to make this API slower in the 
     future."

    Source: https://msdn.microsoft.com/en-us/library/system.globalization.textinfo.totitlecase

    Info about Title Case, used in this implementation, can be found here:

    http://www.grammar-monster.com/lessons/capital_letters_title_case.htm
    http://www.grammar-monster.com/glossary/articles.htm
    http://www.grammar-monster.com/lessons/conjunctions.htm
    http://www.grammar-monster.com/lessons/prepositions.htm
*)

open FsPrettyTable.StringHelpers

let private exeptionalWords =
    [ 
        // Articles:
        "the"; "a"; "and" 
        
        // Conjunctions
        // Note: Correlative Conjunctions not included (yet)
        "and"; "but"; "or"; "nor"; "for"; "so"; "yet"
        "after"; "although"; "as"; "because"; "before"
        "if"; "once"; "since"; "than"; "that"; "though"
        "till"; "until"; "when"; "where"; "whether"; "while"
        
        // Prepositions
        "above"; "about"; "across"; "against"; "along"
        "among"; "around"; "at"; "before"; "behind"; "below"
        "beneath"; "beside"; "between"; "beyond"; "by"
        "down"; "during"; "except"; "for"; "from"; "in"
        "inside"; "into"; "like"; "near"; "of"; "off"; "on"
        "since"; "to"; "toward"; "through"; "under"; "until"
        "up"; "upon"; "with"; "within"
    ]
    
let private exceptional (x:string) =
    let p = (toLower >> (=)) x
    List.exists p exeptionalWords

let internal cap (x:string) =
    x.Substring(0, 1).ToUpper() + x.Substring(1).ToLower()

let private tokenize (x:string) =
    x.Split [|' ';'\n';'\t'|]

let (<&&>) f g x = f x && g x

let mixedCase (x:string) =
    let p = regxIsMatch "[a-z]+" <&&> regxIsMatch "[A-Z]+"
    match x.Length with
    | 0 | 1 -> false
    | _ -> p (x.Substring 1)

let titleCase x =
    tokenize x
    |> Array.mapi
        (fun i w ->
            if mixedCase w then w
            elif i = 0 then cap w
            elif exceptional w then w.ToLower()
            else cap w )
    |> strJoinArr " "
     

