module Tests

open System
open Xunit
open Xunit.Abstractions
open FsUnit
open Converter

type ``All My Tests`` (output:ITestOutputHelper) =
    do new Converter(output) |> Console.SetOut

    [<Fact>]
    member __.``My test`` () =
        // Arrange.
        // Act.
        output.WriteLine("This is a test. This is only a test.")
        // Assert.
        true |> should equal true
