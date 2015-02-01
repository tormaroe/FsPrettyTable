#r @"FsPrettyTable/packages/FAKE/tools/FakeLib.dll"
open Fake
open Fake.Git

RestorePackages()

let authors = ["Torbjørn Marø"]

let projectName = "FsPrettyTable"
let projectDescription = "Represent tabular data in visually appealing ASCII tables using F#"
let projectSummary = projectDescription // TODO: write a summary

let buildDir = "./build/"
let testDir  = "./test/"
let packagingRoot = "./packaging/"
let packagingDir = packagingRoot @@ "FsPrettyTable"

let releaseNotes = 
    ReadFile "ReleaseNotes.md"
    |> ReleaseNotesHelper.parseReleaseNotes

let commitHash = Information.getCurrentSHA1 "."

// Targets
Target "Clean" (fun _ ->
    CleanDirs [buildDir; testDir; packagingRoot; packagingDir]
)

open Fake.AssemblyInfoFile

Target "AssemblyInfo" (fun _ ->
    CreateFSharpAssemblyInfo "FsPrettyTable/AssemblyInfo.fs"
      [ Attribute.Product projectName
        Attribute.Description projectDescription
        Attribute.Version releaseNotes.AssemblyVersion
        Attribute.FileVersion releaseNotes.AssemblyVersion
        Attribute.Metadata("githash", commitHash)
        Attribute.ComVisible false ]
)

Target "BuildLib" (fun _ ->
    !! "FsPrettyTable/**/*.fsproj"
      |> MSBuildRelease buildDir "Build"
      |> Log "AppBuild-Output: "
)

Target "BuildTests" (fun _ ->
    !! "FsPrettyTable.Tests/**/*.fsproj"
      |> MSBuildDebug testDir "Build"
      |> Log "TestBuild-Output: "
)

Target "Test" (fun _ ->
    !! (testDir + "/*.Tests.dll")
      |> NUnit (fun p ->
          {p with
             DisableShadowCopy = true
             ToolPath = @"C:\Program Files (x86)\NUnit 2.6.4\bin\"
             OutputFile = testDir + "TestResults.xml" })
)

Target "CreatePackage" (fun _ ->
    CopyFile packagingDir (buildDir @@ "FsPrettyTable.dll")
    CopyFile packagingDir (buildDir @@ "FsPrettyTable.pdb")
    CopyFile packagingDir (buildDir @@ "FsPrettyTable.XML")
    CopyFiles packagingDir  ["LICENSE"; "README.md"; "ReleaseNotes.md"]

    NuGet (fun p -> 
        {p with
            Authors = authors
            Project = projectName
            Description = projectDescription                               
            OutputPath = packagingRoot
            Summary = projectSummary
            WorkingDir = packagingDir
            Version = releaseNotes.AssemblyVersion
            ReleaseNotes = toLines releaseNotes.Notes
            Files = [("FsPrettyTable.dll", Some @"lib\net45\", None)
                     ("FsPrettyTable.pdb", Some @"lib\net45\", None)
                     ("FsPrettyTable.XML", Some @"lib\net45\", None)
                     ("LICENSE", None, None)
                     ("README.md", None, None)
                     ("ReleaseNotes.md", None, None)]
            Publish = false }) 
            "FsPrettyTable.nuspec"
)

Target "Default" DoNothing

"Clean"
  ==> "AssemblyInfo"
  ==> "BuildLib"
  ==> "BuildTests"
  ==> "Test"
  ==> "CreatePackage"
  ==> "Default"

RunTargetOrDefault "Default"