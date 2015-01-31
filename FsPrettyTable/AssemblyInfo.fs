namespace System
open System.Reflection
open System.Runtime.InteropServices

[<assembly: AssemblyProductAttribute("FsPrettyTable")>]
[<assembly: AssemblyDescriptionAttribute("Represent tabular data in visually appealing ASCII tables using F#")>]
[<assembly: AssemblyVersionAttribute("0.1.0")>]
[<assembly: AssemblyFileVersionAttribute("0.1.0")>]
[<assembly: AssemblyMetadataAttribute("githash","d8123721eba62674b7dc4362aa6067598afc3d0c")>]
[<assembly: ComVisibleAttribute(false)>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.1.0"
