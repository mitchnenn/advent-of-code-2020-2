module Tests

open System
open System.IO
open Xunit
open Xunit.Abstractions
open FsUnit.Xunit
open Converter
open AdapterArray

type ``Adapter array tests`` (output:ITestOutputHelper) =
    do new Converter(output) |> Console.SetOut

    [<Fact>]
    member __.``Fist example in description tests`` () =
        // Arrange.
        let bagOfAdapters = [ 16; 10; 15; 5; 1; 11; 7; 19; 6; 12; 4 ]
        // Act.
        let diffs = bagOfAdapters |> countAdapterOutputDiffs
        // Assert.
        diffs |> should equal [(1, 7); (3, 5)]

    [<Fact>]
    member __.``Second example in description test`` () =
        // Arrange.
        let bagOfAdapters = [28; 33; 18; 42; 31; 14; 46; 20; 48; 47; 24; 23; 49; 45; 19; 38; 39; 11; 1; 32; 25; 35; 8; 17; 7; 9; 4; 2; 34; 10; 3]
        // Act.
        let diffs = bagOfAdapters |> countAdapterOutputDiffs
        // Assert.
        diffs |> should equal [(1, 22); (3, 10)]
        (bagOfAdapters.Length + 2) |> should equal (22 + 10 + 1)

    [<Fact>]
    member __.``Puzzle data test`` () =
        // Arrange.
        let path = Path.Combine(__SOURCE_DIRECTORY__, "data", "day-10.txt")
        output.WriteLine($"Path: {path}")
        let input = File.ReadAllLines(path)
                    |> Seq.map int
                    |> Seq.toList
        // Act.
        let diffs = input |> countAdapterOutputDiffs
        output.WriteLine(sprintf "%A" diffs)
        let answer = diffs |> List.fold (fun state tup -> state * (snd tup)) 1 
        // Arrange.
        answer |> should equal 1876
        output.WriteLine($"Answer: {answer}")

    [<Fact>]
    member __.``Candidates test`` () =
        // Arrange.
        let joltage = 4L
        let adapters = [5L;6L;7L;10L]
        // Act.
        let result = candidates joltage adapters
        // Arrange.
        result |> should equal [(5L, [6L;7L;10L]);(6L, [7L;10L]);(7L, [10L])]

    [<Fact>]
    member __.``Part two example one test`` () =
        // Arrange.
        let bagOfAdapters = [ 16L; 10L; 15L; 5L; 1L; 11L; 7L; 19L; 6L; 12L; 4L ]
        // Act.
        let arrangementCount = countArrangements bagOfAdapters
        // Assert.
        arrangementCount |> should equal 8L

    [<Fact>]
    member __.``Part two example two test`` () =
        // Arrange.
        let bagOfAdapters = [28L; 33L; 18L; 42L; 31L; 14L; 46L; 20L; 48L; 47L; 24L; 23L; 49L; 45L; 19L; 38L; 39L; 11L; 1L; 32L; 25L; 35L; 8L; 17L; 7L; 9L; 4L; 2L; 34L; 10L; 3L]
        // Act.
        let arrangementCount = countArrangements bagOfAdapters
        // Assert.
        arrangementCount |> should equal 19208L

    [<Fact>]
    member __.``Part two puzzle answer text`` () =
        // Arrange.
        let path = Path.Combine(__SOURCE_DIRECTORY__, "data", "day-10.txt")
        output.WriteLine($"Path: {path}")
        let input = File.ReadAllLines(path)
                    |> Seq.map int64
                    |> Seq.toList
        // Act.
        let arrangementCount = countArrangements input
        // Assert.
        arrangementCount |> should equal 14173478093824L
        output.WriteLine($"Answer: {arrangementCount}")
