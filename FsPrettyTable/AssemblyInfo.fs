namespace System
open System.Reflection
open System.Runtime.InteropServices

[<assembly: AssemblyProductAttribute("FsPrettyTable")>]
[<assembly: AssemblyDescriptionAttribute("Represent tabular data in visually appealing ASCII tables using F#")>]
[<assembly: AssemblyVersionAttribute("0.2.0")>]
[<assembly: AssemblyFileVersionAttribute("0.2.0")>]
[<assembly: AssemblyMetadataAttribute("githash","b878e57981caa04c194b3850eb4346140424f22e")>]
[<assembly: ComVisibleAttribute(false)>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.2.0"
