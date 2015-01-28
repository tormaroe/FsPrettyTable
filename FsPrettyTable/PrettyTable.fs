/// Defines the public API of PrettyTable
module PrettyTable

open FsPrettyTable.Types
open FsPrettyTable.StringHelpers
open FsPrettyTable.Core
open FsPrettyTable.Format

// ==================================================================
// Functions to get started
// ==================================================================

/// Create a new PrettyTable with some data.
let prettyTable data = { defaultTable with Rows = data }

// ==================================================================
// Functions to generate output
// ==================================================================
    
/// Generate the ASCII representation of a PrettyTable.
let sprintTable = sprintTable'

/// Print the ASCII representation of a PrettyTable to the console.
let printTable = sprintTable' >> System.Console.Write
    
// ==================================================================
// Functions to set detailed visualization options
// ==================================================================

/// Specify whether PrettyTable should output a header (default = true).
let hasHeader b t = { t with Style = { t.Style with HasHeader = b } }

/// ...
let hasBorder b t = { t with Style = { t.Style with HasBorder = b } } // No test

/// ...
let verticalRules r t = { t with Style = { t.Style with VerticalRules = r } } // No test

/// ...
let horizontalRules r t = { t with Style = { t.Style with HorizontalRules = r } } // No test

/// Specify which header style formatting to apply (default = KeepAsIs).
let headerStyle s t = { t with Style = { t.Style with HeaderStyle = s } }

/// Specify padding width for all table cells (default = 1).
let paddingWidth n t = { t with Style = { t.Style with PaddingWidth = n } }

/// ...
let leftPaddingWidth n t = { t with Style = { t.Style with LeftPaddingWidth = Some n } }

/// ...
let rightPaddingWidth n t = { t with Style = { t.Style with RightPaddingWidth = Some n } }

/// Specify character to use for vertical column separation (default = '|').
let verticalChar c t = { t with Style = { t.Style with VerticalChar = c } }

/// Specify character to use for horizontal row separation (default = '-').
let horizontalChar c t = { t with Style = { t.Style with HorizontalChar = c } }

/// Specify character to use for junctions between cells (default = '+').
let junctionChar c t = { t with Style = { t.Style with JunctionChar = c } }

/// Specify horizontal alignment (default = Center).
let horizontalAlignment a t = { t with Style = { t.Style with HorizontalAlignment = a } }

// ==================================================================
// Functions to specify data transformations
// ==================================================================

/// Accepts a set of headers. Output columns will be restricted to these columns.
let onlyColumns xs t = 
    { t with Transformation = { t.Transformation with OnlyColumns = Some xs } }

/// All data columns will be included in output.
let allColumns t = 
    { t with Transformation = { t.Transformation with OnlyColumns = None } }

/// Sort my a particular field (specified by header value) in ascending order.
let sortBy field t = 
    { t with Transformation = { t.Transformation with SortBy = Some (field, Ascending) } }

/// Sort my a particular field (specified by header value) in descending order.
let sortByDescending field t = 
    { t with Transformation = { t.Transformation with SortBy = Some (field, Descending) } }

/// Clear any previsoudly set sorting order.
let sortByNone t = 
    { t with Transformation = { t.Transformation with SortBy = None } }

// ==================================================================
// Predefined table styles
// ==================================================================

/// Predefined style sets
type Style = DefaultStyle | PlainColumns | PlainRows | MsWordFriendly

/// Override all other visualization options with a predefined style set.
let setStyle x t =
    // TODO: Should probably not override sorting and column filtering
    let t' = prettyTable t.Rows
    match x with
    | DefaultStyle -> t'
    | PlainColumns -> t' |> hasBorder false |> leftPaddingWidth 0 |> rightPaddingWidth 8
    | PlainRows -> t' |> verticalRules NoRules
    | MsWordFriendly -> t' |> horizontalRules NoRules

