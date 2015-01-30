module FsPrettyTable.Tests

open NUnit.Framework
open FsUnit

open PrettyTable

let shouldPrint p table =
    table
    |> sprintTable
    |> (+) System.Environment.NewLine
    |> should equal p

let simpleTable = 
    [["Row 1";"123";"abcd"]
     ["Row 2";"103";"abcd"]
     ["Row 3";"113";"abcd"]]
    |> prettyTable
    |> withHeaders ["Header 1"; "Header 2"; "Header 3"]
    
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
let ``Only selected columns by indexes`` ()=
    simpleTable
    |> onlyColumnsByIndex [0; 2]
    |> shouldPrint """
+----------+----------+
| Header 1 | Header 3 |
+----------+----------+
|  Row 1   |   abcd   |
|  Row 2   |   abcd   |
|  Row 3   |   abcd   |
+----------+----------+
"""

[<Test>]
let ``Only selected columns by indexes when no headers`` ()=
    [["Row 1";"123";"abcd"]
     ["Row 2";"1234567890";"abcd"]
     ["Row 3";"123";"abcd"]]
    |> prettyTable
    |> onlyColumnsByIndex [0; 1]
    |> shouldPrint """
+-------+------------+
| Row 1 |    123     |
| Row 2 | 1234567890 |
| Row 3 |    123     |
+-------+------------+
"""

[<Test>]
let ``Only selected columns by predicate`` ()=
    simpleTable
    |> onlyColumnsByChoice (fun index header -> 
                                index = 0 || header = "Header 3")
    |> shouldPrint """
+----------+----------+
| Header 1 | Header 3 |
+----------+----------+
|  Row 1   |   abcd   |
|  Row 2   |   abcd   |
|  Row 3   |   abcd   |
+----------+----------+
"""

[<Test>]
let ``Should expand column width for long values`` ()=
    [["Row 1";"123";"abcd"]
     ["Row 2";"1234567890";"abcd"]
     ["Row 3";"123";"abcd"]]
    |> prettyTable
    |> withHeaders ["Header 1"; "Header 2"; "Header 3"]
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
        [["Row 1";"1234567890";"abcd"]]
        |> prettyTable
        |> withHeaders ["the Col one";"col Two";"COL THREE AND FoUR"]
        
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

[<Test>]
let ``MS Word friendly (no horizontal rules)`` ()=
    simpleTable
    |> setStyle MsWordFriendly
    |> shouldPrint """
| Header 1 | Header 2 | Header 3 |
|  Row 1   |   123    |   abcd   |
|  Row 2   |   103    |   abcd   |
|  Row 3   |   113    |   abcd   |
"""

[<Test>]
let ``PlainRows style (no vertical rules)`` ()=
    simpleTable
    |> setStyle PlainRows
    |> shouldPrint """
----------------------------------
  Header 1   Header 2   Header 3  
----------------------------------
   Row 1       123        abcd    
   Row 2       103        abcd    
   Row 3       113        abcd    
----------------------------------
"""

[<Test>]
let ``No rules (but border)`` ()=
    simpleTable
    |> horizontalRules NoRules
    |> verticalRules NoRules
    |> shouldPrint """
  Header 1   Header 2   Header 3  
   Row 1       123        abcd    
   Row 2       103        abcd    
   Row 3       113        abcd    
"""

[<Test>]
let ``No border and custom padding`` ()=
    simpleTable
    |> hasBorder false
    |> paddingWidth 0
    |> rightPaddingWidth 1
    |> shouldPrint """
Header 1 Header 2 Header 3 
 Row 1     123      abcd   
 Row 2     103      abcd   
 Row 3     113      abcd   
"""

[<Test>]
let ``Plain columns style (uses left and right padding)`` ()=
    simpleTable
    |> setStyle PlainColumns
    |> shouldPrint """
Header 1        Header 2        Header 3        
 Row 1            123             abcd          
 Row 2            103             abcd          
 Row 3            113             abcd          
"""

    
[<Test>]
let ``Override horizontal alignment for columns`` ()=
    simpleTable
    |> horizontalAlignmentForColumn "Header 1" Left
    |> horizontalAlignmentForColumn "Header 3" Right
    |> shouldPrint """
+----------+----------+----------+
| Header 1 | Header 2 | Header 3 |
+----------+----------+----------+
| Row 1    |   123    |     abcd |
| Row 2    |   103    |     abcd |
| Row 3    |   113    |     abcd |
+----------+----------+----------+
"""


[<Test>]
let ``Title case test`` ()=
    let title = FsPrettyTable.TitleCase.titleCase
    title "war and peace" |> should equal "War and Peace"
    title "war AND PEACE" |> should equal "War and Peace"
    title "a duck and a dog" |> should equal "A Duck and a Dog"
    title "a duck and its dog-walker!" |> should equal "A Duck and Its Dog-walker!"
    title "McDonalds" |> should equal "McDonalds"

[<Test>]
let ``Validation canary test`` ()=
    [["Row 1";"123";"abcd"]
     ["Row 2";"1234567890";"abcd"]
     ["Row 3";"123";"abcd"]]
    |> prettyTable
    |> onlyColumnsByIndex [-1; 0; 1; 3]
    |> FsPrettyTable.Validation.validate
    |> should equal 
        (FsPrettyTable.Validation.Invalid 
            ["-1 in column filter is not a valid column index"
             "3 in column filter is not a valid column index"])

             
[<Test>]
let ``Validation error canary test`` ()=
    try
        [["Row 1";"123";"abcd"]
         ["Row 2";"1234567890";"abcd"]
         ["Row 3";"123";"abcd"]]
        |> prettyTable
        |> onlyColumnsByIndex [-1; 0; 1; 3]
        |> sprintTable
        |> ignore
        Assert.Fail "Validation should throw"
    with
    | FsPrettyTable.Validation.ValidationError(xs) ->
        xs |> should equal   
                ["-1 in column filter is not a valid column index"
                 "3 in column filter is not a valid column index"]


// TODO: Multiline values (and alignment)

// TODO: Add markdown support
// https://help.github.com/articles/github-flavored-markdown/
// Need alignment indicators

// TODO: add `top` function