#Quickstart

## Installation

Install FsPrettyTable using nuget:

    PM> Install-Package FsPrettyTable

## Usage

Let's begin by defining some data which we would like to display as a table:

    open PrettyTable
    let headers = ["Language";"Developer";"Appeared in";"Influenced by"]
    let rows = [["IPL";"RAND Corp.";"1956";""]
                ["LISP";"John McCarthy";"1958";"IPL"]
                ["ISWIM";"Peter J. Landin";"1966";"LISP"]
                ["ML";"Robin Milner";"1973";"ISWIM"]
                ["Caml";"Gérard Huet";"1985";"ML"]
                ["OCaml";"INRIA";"1996";"Caml"]
                ["F#";"M$ / Don Syme";"2005";"OCaml"]]

The `PrettyTable` module provides a bunch of functions which can be chained together to format, manimulate and finally print (or just return as a string). Let's just print it using the default style first:

    rows |> prettyTable |> printTable

    // Output:
    +-------+-----------------+------+-------+
    |  IPL  |   RAND Corp.    | 1956 |       |
    | LISP  |  John McCarthy  | 1958 |  IPL  |
    | ISWIM | Peter J. Landin | 1966 | LISP  |
    |  ML   |  Robin Milner   | 1973 | ISWIM |
    | Caml  |   Gérard Huet   | 1985 |  ML   |
    | OCaml |      INRIA      | 1996 | Caml  |
    |  F#   |  M$ / Don Syme  | 2005 | OCaml |
    +-------+-----------------+------+-------+

Ok, nice. But where did the headers go? Well, you I need to add then obviously:

    prettyTable rows
    |> withHeaders headers
    |> printTable

    // Output:
    +----------+-----------------+-------------+---------------+
    | Language |    Developer    | Appeared in | Influenced by |
    +----------+-----------------+-------------+---------------+
    |   IPL    |   RAND Corp.    |    1956     |               |
    |   LISP   |  John McCarthy  |    1958     |      IPL      |
    |  ISWIM   | Peter J. Landin |    1966     |     LISP      |
    |    ML    |  Robin Milner   |    1973     |     ISWIM     |
    |   Caml   |   Gérard Huet   |    1985     |      ML       |
    |  OCaml   |      INRIA      |    1996     |     Caml      |
    |    F#    |  M$ / Don Syme  |    2005     |     OCaml     |
    +----------+-----------------+-------------+---------------+

If you just want the table string representation instead of printing it, use `sprintTable`.