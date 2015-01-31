namespace System
open System.Reflection
open System.Runtime.InteropServices

[<assembly: AssemblyProductAttribute("FsPrettyTable")>]
[<assembly: AssemblyDescriptionAttribute("Represent tabular data in visually appealing ASCII tables using F#")>]
[<assembly: AssemblyVersionAttribute("0.1.0")>]
[<assembly: AssemblyFileVersionAttribute("0.1.0")>]
[<assembly: AssemblyMetadataAttribute("githash","4542dd66acd541110cdf0669d023adf286caa8db")>]
[<assembly: ComVisibleAttribute(false)>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.1.0"
