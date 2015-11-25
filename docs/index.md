#FSPrettyTable

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
