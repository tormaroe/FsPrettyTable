@echo off
cls
rem "FsPrettyTable\.nuget\NuGet.exe" "Install" "FAKE" "-OutputDirectory" "FsPrettyTable\packages" "-ExcludeVersion"
"FsPrettyTable\packages\FAKE\tools\Fake.exe" build.fsx
pause