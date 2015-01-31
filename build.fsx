// include Fake lib
#r @"FsPrettyTable/packages/FAKE/tools/FakeLib.dll"
open Fake

RestorePackages()

// Properties
let buildDir = "./build/"
let testDir  = "./test/"

// Targets
Target "Clean" (fun _ ->
    CleanDir buildDir
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
             ToolPath = "C:\\Program Files (x86)\\NUnit 2.6.4\\bin\\"
             OutputFile = testDir + "TestResults.xml" })
)

// Default target
Target "Default" (fun _ ->
    trace "Hello World from FAKE"
)

// Dependencies
"Clean"
  ==> "BuildLib"
  ==> "BuildTests"
  ==> "Test"
  ==> "Default"

// start build
RunTargetOrDefault "Default"