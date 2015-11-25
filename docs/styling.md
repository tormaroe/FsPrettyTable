# Styling the ASCII properties

## Horizontal and vertical rules

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

## Horizontal alignment

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

## Vertical alignment

*TODO: Implement vertical alignment*

## Padding

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

## Header style

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


## Swapping out the separator characters

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

## Predefined styles

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
