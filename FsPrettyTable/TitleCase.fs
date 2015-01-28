module FsPrettyTable.TitleCase

open FsPrettyTable.StringHelpers

(*
    http://www.grammar-monster.com/lessons/capital_letters_title_case.htm
    http://www.grammar-monster.com/glossary/articles.htm
    http://www.grammar-monster.com/lessons/conjunctions.htm
    http://www.grammar-monster.com/lessons/prepositions.htm
*)

// Note: Correlative Conjunctions not included
let private conjunctions = [ "and"; "but"; "or"; "nor"; "for"; "so"; "yet"
                             "after"; "although"; "as"; "because"; "before"
                             "if"; "once"; "since"; "than"; "that"; "though"
                             "till"; "until"; "when"; "where"; "whether"; "while" ]

let private prepositions = [ "above"; "about"; "across"; "against"; "along"
                             "among"; "around"; "at"; "before"; "behind"; "below"
                             "beneath"; "beside"; "between"; "beyond"; "by"
                             "down"; "during"; "except"; "for"; "from"; "in"
                             "inside"; "into"; "like"; "near"; "of"; "off"; "on"
                             "since"; "to"; "toward"; "through"; "under"; "until"
                             "up"; "upon"; "with"; "within" ] 

let private (|Article|Conjunction|Preposition|Other|) (word:string) =
    match word.ToLower() with
    | "the" | "a" | "and" -> Article
    | word' when List.exists ((=) word') conjunctions -> Conjunction
    | word' when List.exists ((=) word') prepositions -> Preposition
    | _ -> Other
    
let internal cap (x:string) =
    x.Substring(0, 1).ToUpper() + x.Substring(1).ToLower()

let titleCase (x:string) =
    x.Split [|' ';'\n';'\t'|]
    |> Array.mapi
        (fun i w ->
            match i with
            | 0 -> cap w
            | _ -> 
                match w with
                | Article | Conjunction | Preposition -> w.ToLower()
                |Other -> cap w )
    |> strJoinArr " "
     

