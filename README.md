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

[![Documentation Status](https://readthedocs.org/projects/fsprettytable/badge/?version=latest)](http://fsprettytable.readthedocs.org/en/latest/?badge=latest)

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

## Changing the ASCII properties

### Horizontal and vertical rules

You can turn both horizontal and vertical rules on or off individually. Let's try it:

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

You may align the content of table cells either `Left`, `Right`, og `Center` (default).

    prettyTable rows
    |> withHeaders headers
    |> horizontalAlignment FsPrettyTable.Types.Left
    |> printTable

    // Output:
    +----------+-----------------+-------------+---------------+
    | Language | Developer       | Appeared in | Influenced by |
    +----------+-----------------+-------------+---------------+
    | IPL      | RAND Corp.      | 1956        |               |
    | LISP     | John McCarthy   | 1958        | IPL           |
    | ISWIM    | Peter J. Landin | 1966        | LISP          |
    | ML       | Robin Milner    | 1973        | ISWIM         |
    | Caml     | Gérard Huet     | 1985        | ML            |
    | OCaml    | INRIA           | 1996        | Caml          |
    | F#       | M$ / Don Syme   | 2005        | OCaml         |
    +----------+-----------------+-------------+---------------+

And then you have the option to override alignment for each column, based on the heading value:

    open FsPrettyTable.Types

    prettyTable rows
    |> withHeaders headers
    |> horizontalAlignment Left
    |> horizontalAlignmentForColumn "Appeared in" Center
    |> horizontalAlignmentForColumn "Influenced by" Right
    |> printTable

    // Output:
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

### Vertical alignment

*TODO: Implement vertical alignment*

### Padding

You may specify the amount of padding using the `paddingWidth` function. Default padding is `1`, which adds a single space on either side of the value in a cell. You may also specify padding for the *left* and *right* side separately: 

    prettyTable rows
    |> withHeaders headers
    |> hasBorder false
    |> horizontalAlignment Left
    |> horizontalAlignmentForColumn "Appeared in" Center
    |> leftPaddingWidth 0
    |> rightPaddingWidth 4
    |> printTable

    // Output:
    Language    Developer          Appeared in    Influenced by    
    IPL         RAND Corp.            1956                         
    LISP        John McCarthy         1958        IPL              
    ISWIM       Peter J. Landin       1966        LISP             
    ML          Robin Milner          1973        ISWIM            
    Caml        Gérard Huet           1985        ML               
    OCaml       INRIA                 1996        Caml             
    F#          M$ / Don Syme         2005        OCaml                       

### Header style

The casing style of the headers may be changed using the `headerStyle` function. Possible values are:

 Value      | Description
:---------- |:------------
 KeepAsIs   | Simply leave it as it is. This is default.
 LowerCase  | Turn all letters to lower case.
 UpperCase  | Turn all letters to upper case.
 TitleCase  | An attempt at producing a linguistically correct title case (for English titles). Uses it's own implementation, which is slightly more correct that the default .NET implementation.
 Capitalise | Simple use upper case for the first letter, lower case for the rest.

    prettyTable rows
    |> withHeaders headers
    |> headerStyle FsPrettyTable.Types.UpperCase
    |> printTable

    // Output:
    +----------+-----------------+-------------+---------------+
    | LANGUAGE |    DEVELOPER    | APPEARED IN | INFLUENCED BY |
    +----------+-----------------+-------------+---------------+
    |   IPL    |   RAND Corp.    |    1956     |               |
    |   LISP   |  John McCarthy  |    1958     |      IPL      |
    ...


### Swapping out the separator characters

You can replace the characters used in the border:

    prettyTable rows
    |> withHeaders headers
    |> verticalChar '/'
    |> horizontalChar ' '
    |> junctionChar 'o'
    |> printTable

    // Output:
    o          o                 o             o               o
    / Language /    Developer    / Appeared in / Influenced by /
    o          o                 o             o               o
    /   IPL    /   RAND Corp.    /    1956     /               /
    /   LISP   /  John McCarthy  /    1958     /      IPL      /
    /  ISWIM   / Peter J. Landin /    1966     /     LISP      /
    /    ML    /  Robin Milner   /    1973     /     ISWIM     /
    /   Caml   /   Gérard Huet   /    1985     /      ML       /
    /  OCaml   /      INRIA      /    1996     /     Caml      /
    /    F#    /  M$ / Don Syme  /    2005     /     OCaml     /
    o          o                 o             o               o

### Predefined styles

FsPrettyTable comes with some predefined style sets. They are:

 Style          | Description |
:-------------- |:-----------
 DefaultStyle   | By default, FsPrettyTable produces ASCII tables that look like the ones used in SQL database shells. Use this to undo any style changes you may have made.
 PlainColumns   | A borderless style that works well with command line programs for columnar data.
 MsWordFriendly | A format which works nicely with Microsoft Word's "Convert to table" feature
 PlainRows      | Basically just no vertical rules. I think this looks nice :)
 Markdown       | *Not supported yet..*

This is how you set a style:

    prettyTable rows
    |> withHeaders headers
    |> setStyle MsWordFriendly
    |> printTable

## Filtering columns

You may filter column output based on the column headers. This requires that you are actually using headers.

    prettyTable rows
    |> withHeaders headers
    |> onlyColumns ["Language"; "Appeared in"]
    |> printTable

    // Output:
    +----------+-------------+
    | Language | Appeared in |
    +----------+-------------+
    |   IPL    |    1956     |
    |   LISP   |    1958     |
    ...

You may also specify filtering by column index. This is what you'll have to use if you have a table with no headers:

    myTable |> onlyColumnsByIndex [0; 2]

or by a predicate function which will be passed both the column index and the header value as arguments:

    myTable |> onlyColumnsByChoice
                   (fun i h -> i = 0 || h = "Appeared in")

Specifying a column filtering overwrites any previous filtering. To remove the filter, use function `allColumns`.

## Sorting

You may sort the table on a single column, again by specifying the header value. Use `sortBy` for ascending order or `sortByDescending` for descending order. Use `sortByNone` to clear any sorting you have previously added.

    prettyTable rows
    |> withHeaders headers
    |> sortByDescending "Appeared in"
    |> printTable

    // Output:
    +----------+-----------------+-------------+---------------+
    | Language |    Developer    | Appeared in | Influenced by |
    +----------+-----------------+-------------+---------------+
    |    F#    |  M$ / Don Syme  |    2005     |     OCaml     |
    |  OCaml   |      INRIA      |    1996     |     Caml      |
    |   Caml   |   Gérard Huet   |    1985     |      ML       |
    |    ML    |  Robin Milner   |    1973     |     ISWIM     |
    |  ISWIM   | Peter J. Landin |    1966     |     LISP      |
    |   LISP   |  John McCarthy  |    1958     |      IPL      |
    |   IPL    |   RAND Corp.    |    1956     |               |
    +----------+-----------------+-------------+---------------+

Remember that all values are strings, and will be sorted as such.

*TODO: Specify column using index*

*TODO: Specify column using a compare function (takes complete row)*

## API

Pre version 1.0 the API may change.

*TODO: Add info about validation*

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
    val onlyColumnsByIndex : int list -> Table -> Table
    val onlyColumnsByChoice : (int -> string -> bool) -> Table -> Table
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
