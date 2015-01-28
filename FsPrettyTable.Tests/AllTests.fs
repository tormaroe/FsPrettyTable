﻿module FsPrettyTable.Tests

open NUnit.Framework
open FsUnit

open PrettyTable

let shouldPrint p table =
    table
    |> sprintTable
    |> (+) System.Environment.NewLine
    |> should equal p

let simpleTable = 
    [["Header 1"; "Header 2"; "Header 3"]
     ["Row 1";"123";"abcd"]
     ["Row 2";"103";"abcd"]
     ["Row 3";"113";"abcd"]]
    |> prettyTable
    
[<Test>]
let ``Make a simple table with default settings`` ()=
    simpleTable
    |> shouldPrint """
+----------+----------+----------+
| Header 1 | Header 2 | Header 3 |
+----------+----------+----------+
|  Row 1   |   123    |   abcd   |
|  Row 2   |   103    |   abcd   |
|  Row 3   |   113    |   abcd   |
+----------+----------+----------+
"""

[<Test>]
let ``Only selected columns`` ()=
    simpleTable
    |> onlyColumns ["Header 2"]
    |> shouldPrint """
+----------+
| Header 2 |
+----------+
|   123    |
|   103    |
|   113    |
+----------+
"""

[<Test>]
let ``Should expand column width for long values`` ()=
    [["Header 1"; "Header 2"; "Header 3"]
     ["Row 1";"123";"abcd"]
     ["Row 2";"1234567890";"abcd"]
     ["Row 3";"123";"abcd"]]
    |> prettyTable
    |> shouldPrint """
+----------+------------+----------+
| Header 1 |  Header 2  | Header 3 |
+----------+------------+----------+
|  Row 1   |    123     |   abcd   |
|  Row 2   | 1234567890 |   abcd   |
|  Row 3   |    123     |   abcd   |
+----------+------------+----------+
"""

[<Test>]
let ``Custom table characters`` ()=
    simpleTable
    |> verticalChar '$'
    |> horizontalChar '_'
    |> junctionChar 'x'
    |> shouldPrint """
x__________x__________x__________x
$ Header 1 $ Header 2 $ Header 3 $
x__________x__________x__________x
$  Row 1   $   123    $   abcd   $
$  Row 2   $   103    $   abcd   $
$  Row 3   $   113    $   abcd   $
x__________x__________x__________x
"""

[<Test>]
let ``Custom padding`` ()=
    simpleTable
    |> paddingWidth 2
    |> shouldPrint """
+------------+------------+------------+
|  Header 1  |  Header 2  |  Header 3  |
+------------+------------+------------+
|   Row 1    |    123     |    abcd    |
|   Row 2    |    103     |    abcd    |
|   Row 3    |    113     |    abcd    |
+------------+------------+------------+
"""

[<Test>]
let ``Hprizontal alignment left, all columns`` ()=
    simpleTable
    |> horizontalAlignment Left
    |> shouldPrint """
+----------+----------+----------+
| Header 1 | Header 2 | Header 3 |
+----------+----------+----------+
| Row 1    | 123      | abcd     |
| Row 2    | 103      | abcd     |
| Row 3    | 113      | abcd     |
+----------+----------+----------+
"""

[<Test>]
let ``Hprizontal alignment right, all columns`` ()=
    simpleTable
    |> horizontalAlignment Right
    |> shouldPrint """
+----------+----------+----------+
| Header 1 | Header 2 | Header 3 |
+----------+----------+----------+
|    Row 1 |      123 |     abcd |
|    Row 2 |      103 |     abcd |
|    Row 3 |      113 |     abcd |
+----------+----------+----------+
"""

[<Test>]
let ``Sort by values in a column`` ()=
    simpleTable
    |> sortBy "Header 2"
    |> shouldPrint """
+----------+----------+----------+
| Header 1 | Header 2 | Header 3 |
+----------+----------+----------+
|  Row 2   |   103    |   abcd   |
|  Row 3   |   113    |   abcd   |
|  Row 1   |   123    |   abcd   |
+----------+----------+----------+
"""


[<Test>]
let ``Sort by values in a column when another columns has been removed`` ()=
    // This is a possible error situation, so adding a regression test..
    simpleTable
    |> sortBy "Header 2"
    |> onlyColumns ["Header 2"; "Header 3"]
    |> shouldPrint """
+----------+----------+
| Header 2 | Header 3 |
+----------+----------+
|   103    |   abcd   |
|   113    |   abcd   |
|   123    |   abcd   |
+----------+----------+
"""

[<Test>]
let ``Sort Descending by values in a column`` ()=
    simpleTable
    |> sortByDescending "Header 2"
    |> shouldPrint """
+----------+----------+----------+
| Header 1 | Header 2 | Header 3 |
+----------+----------+----------+
|  Row 1   |   123    |   abcd   |
|  Row 3   |   113    |   abcd   |
|  Row 2   |   103    |   abcd   |
+----------+----------+----------+
"""

[<Test>]
let ``No header`` ()=
    [["Row 1";"123";"abcd"]
     ["Row 2";"1234567890";"abcd"]
     ["Row 3";"123";"abcd"]]
    |> prettyTable
    |> hasHeader false
    |> shouldPrint """
+-------+------------+------+
| Row 1 |    123     | abcd |
| Row 2 | 1234567890 | abcd |
| Row 3 |    123     | abcd |
+-------+------------+------+
"""

[<Test>]
let ``Header styles`` ()=
    let table = 
        [["the Col one";"col Two";"COL THREE AND FoUR"]
         ["Row 1";"1234567890";"abcd"]]
        |> prettyTable
        
    table
    |> headerStyle LowerCase
    |> shouldPrint """
+-------------+------------+--------------------+
| the col one |  col two   | col three and four |
+-------------+------------+--------------------+
|    Row 1    | 1234567890 |        abcd        |
+-------------+------------+--------------------+
"""
    table
    |> headerStyle UpperCase
    |> shouldPrint """
+-------------+------------+--------------------+
| THE COL ONE |  COL TWO   | COL THREE AND FOUR |
+-------------+------------+--------------------+
|    Row 1    | 1234567890 |        abcd        |
+-------------+------------+--------------------+
"""
    // TODO: TitleCase
    // TODO: Capitilize
