# Sorting

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