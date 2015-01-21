// include Fake lib
#r "packages/FAKE/tools/FakeLib.dll"
open Fake

RestorePackages()

// Properties
let buildDir = "./bin/"
let testDir  = "./bin/test/"

// Targets
Target "clean" (fun _ ->
    CleanDirs [buildDir]
)

Target "build" (fun _ ->
   !! "src/**/*.fsproj"
     |> MSBuildRelease buildDir "Build"
     |> Log "AppBuild-Output: "
)

Target "buildTest" (fun _ ->
    !! "/tests/**/*.fsproj"
      |> MSBuildDebug testDir "Build"
      |> Log "TestBuild-Output: "
)

Target "test" (fun _ ->
    !! (testDir + "/*.UnitTests.dll")
      |> NUnit (fun p ->
          {p with
             DisableShadowCopy = true;
             OutputFile = testDir + "TestResults.xml" })
)

Target "default" (fun _ -> ())

// Dependencies
"clean"
  ==> "build"
  ==> "buildTest"
  ==> "test"
  ==> "default"

// start build
RunTargetOrDefault "default"