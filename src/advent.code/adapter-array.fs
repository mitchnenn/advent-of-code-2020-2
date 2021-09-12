module AdapterArray

open System.Collections.Generic

let majicValue = 3
let majicValueLong = 3L

let add someValue input = input + someValue

let countAdapterOutputDiffs bagOfAdapters =
    bagOfAdapters
    |> List.append [0; (bagOfAdapters |> List.max |> add majicValue)]
    |> List.sort
    |> List.pairwise
    |> List.map (fun (i,j) -> j - i)
    |> List.countBy id
    |> List.sortBy (fun (i,_) -> i)

let candidates joltage adapters =
    let rec candidates' acc joltage adapters =
        match adapters with
        | [] -> acc
        | x::xs when x - joltage <= majicValueLong -> candidates' ((x, xs)::acc) joltage xs
        | _ -> acc
    candidates' [] joltage adapters |> List.rev

let rec sumAllPaths (lookup:Dictionary<int64,int64>) joltage adapters =
    match lookup.TryGetValue(joltage) with
    | (true, v) -> v
    | _ -> 
        let result = match adapters with
                     | [_] -> 1L
                     | adapters ->
                            candidates joltage adapters
                            |> List.map (fun (c, rest) -> sumAllPaths lookup c rest)
                            |> List.sum
        lookup.Add(joltage, result)
        result

let countArrangements bagOfAdapters =
    bagOfAdapters
    |> List.append [(bagOfAdapters |> List.max) + majicValueLong]
    |> List.sort
    |> sumAllPaths (new Dictionary<int64, int64>()) 0L
