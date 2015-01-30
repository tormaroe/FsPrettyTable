FsPrettyTable is a simple F# library designed to make it quick and easy to represent tabular data in visually appealing ASCII tables, like this:

    +----------+-----------------+-------------+---------------+
    | Language | Developer       | Appeared in | Influenced by |
    +----------+-----------------+-------------+---------------+
    | IPL      | RAND Corp.      |    1956     |               |
    | LISP     | John McCarthy   |    1958     |           IPL |
    | ISWIM    | Peter J. Landin |    1966     |          LISP |
    | ML       | Robin Milner    |    1973     |         ISWIM |
    | Caml     | Gérard Huet     |    1985     |            ML |
    | OCaml    | INRIA           |    1996     |          Caml |
    | F#       | M$ / Don Syme   |    2005     |         OCaml |
    +----------+-----------------+-------------+---------------+

It is more or less a rip off of the [PrettyTable Python library](https://code.google.com/p/prettytable/) made by [Luke Maurits](http://www.luke.maurits.id.au/). I hope he doesn't mind.

PrettyTable lets you control many aspects of the table, like the width of the column padding, the alignment of text within columns, which characters are used to draw the table border, whether you even want a border, and much more. You can control which subsets of the columns and rows are printed, and you can sort the rows by the value of a particular column.

## Installation

*TODO: Add NuGet info once it's published*

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

## Changing the ASCII properties

### Horizontal and vertical rules

You can turn both horizontal and vertical on or off individually. Let's try it:

    prettyTable rows
    |> withHeaders headers
    |> verticalRules FsPrettyTable.Types.NoRules
    |> printTable

    // Output:
    ------------------------------------------------------------
      Language      Developer      Appeared in   Influenced by  
    ------------------------------------------------------------
        IPL        RAND Corp.         1956                      
        LISP      John McCarthy       1958            IPL       
       ISWIM     Peter J. Landin      1966           LISP       
         ML       Robin Milner        1973           ISWIM      
        Caml       Gérard Huet        1985            ML        
       OCaml          INRIA           1996           Caml       
         F#       M$ / Don Syme       2005           OCaml      
    ------------------------------------------------------------

That's pretty nice! And the other one:

    prettyTable rows
    |> withHeaders headers
    |> horizontalRules FsPrettyTable.Types.NoRules
    |> printTable

    // Output:
    | Language |    Developer    | Appeared in | Influenced by |
    |   IPL    |   RAND Corp.    |    1956     |               |
    |   LISP   |  John McCarthy  |    1958     |      IPL      |
    |  ISWIM   | Peter J. Landin |    1966     |     LISP      |
    |    ML    |  Robin Milner   |    1973     |     ISWIM     |
    |   Caml   |   Gérard Huet   |    1985     |      ML       |
    |  OCaml   |      INRIA      |    1996     |     Caml      |
    |    F#    |  M$ / Don Syme  |    2005     |     OCaml     |

*TODO: Implement difference between `FsPrettyTable.Types.Frame` and `FsPrettyTable.Types.All`.*

What you might actually be looking for is turning all bordering off, and that can be done using `hasBorder` like so:

    prettyTable rows
    |> withHeaders headers
    |> hasBorder false
    |> printTable

    // Output:
     Language     Developer     Appeared in  Influenced by 
       IPL       RAND Corp.        1956                    
       LISP     John McCarthy      1958           IPL      
      ISWIM    Peter J. Landin     1966          LISP      
        ML      Robin Milner       1973          ISWIM     
       Caml      Gérard Huet       1985           ML       
      OCaml         INRIA          1996          Caml      
        F#      M$ / Don Syme      2005          OCaml     

### Horizontal alignment

### Vertical alignment

*TODO: Implement vertical alignment*

### Padding

### Header style

### Swapping out the separator characters

### Predefined styles

## Filtering columns

## Sorting

## API

Pre version 1.0 the API may change.

### Module PrettyTable

    val prettyTable : string list list -> Table
    val withHeaders : string list -> Table -> Table

    val sprintTable : Table -> string
    val printTable : Table -> unit

    val hasHeader : bool -> Table -> Table
    val hasBorder : bool -> Table -> Table
    val verticalRules : Rules -> Table -> Table
    val horizontalRules : Rules -> Table -> Table

    val headerStyle : HeaderStyle -> Table -> Table

    val horizontalAlignment : HorizontalAlignment -> Table -> Table
    val horizontalAlignmentForColumn : string -> HorizontalAlignment -> Table -> Table

    val paddingWidth : int -> Table -> Table
    val leftPaddingWidth : int -> Table -> Table
    val rightPaddingWidth : int -> Table -> Table

    val verticalChar : char -> Table -> Table
    val horizontalChar : char -> Table -> Table
    val junctionChar : char -> Table -> Table

    val onlyColumns : string list -> Table -> Table
    val allColumns : Table -> Table

    val sortBy : string -> Table -> Table
    val sortByDescending : string -> Table -> Table
    val sortByNone Table -> Table

    type Style = DefaultStyle | PlainColumns | PlainRows | MsWordFriendly
    val setStyle : Style -> Table -> Table

## License

The MIT License (MIT)

Copyright (c) 2015 Torbjørn Marø

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.