// Learn more about F# at http://fsharp.net. See the 'F# Tutorial' project
// for more guidance on F# programming.

#r "bin\\Debug\\FsPrettyTable.dll"

open PrettyTable
let headers = ["Language";"Developer";"Appeared in";"Influenced by"]
let rows = [["IPL";"RAND Corp.";"1956";""]
            ["LISP";"John McCarthy";"1958";"IPL"]
            ["ISWIM";"Peter J. Landin";"1966";"LISP"]
            ["ML";"Robin Milner";"1973";"ISWIM"]
            ["Caml";"Gérard Huet";"1985";"ML"]
            ["OCaml";"INRIA";"1996";"Caml"]
            ["F#";"M$ / Don Syme";"2005";"OCaml"]]

prettyTable rows
|> withHeaders headers
|> printTable