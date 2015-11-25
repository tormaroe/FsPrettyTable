#Filtering

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