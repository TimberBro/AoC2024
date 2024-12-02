module AoC2024.Day02.Day02

open System
open System.IO


let readLines filePath = File.ReadAllLines(filePath)

let baseDirectory = __SOURCE_DIRECTORY__
let filePath = baseDirectory + @"/input.txt"
let lines = readLines filePath

let convertStringsToIntegers (strings: string list) =
    strings
    |> List.map int

let isIncreasing (numbers: int list) =
    numbers
    |> List.pairwise
    |> List.forall (fun (a, b) -> a < b)

let isDecreasing (numbers: int list) =
    numbers
    |> List.pairwise
    |> List.forall (fun (a, b) -> a > b)

let areDifferencesValid (numbers: int list) =
    numbers
    |> List.pairwise
    |> List.forall (fun (a, b) -> abs (a - b) >= 1 && abs (a - b) <= 3)
    

let reportIsValidFirstPart(report: string list) =
    let numbers = convertStringsToIntegers report
    (isIncreasing numbers || isDecreasing numbers) && areDifferencesValid numbers

let reports =
    lines
    |> Array.map (fun line -> line.Split(" ", StringSplitOptions.RemoveEmptyEntries))
    |> Array.map (fun line -> line |> Array.toList)
    |> Array.toList
 
let firstValidReports =
     reports
     |> List.map reportIsValidFirstPart
     |> List.filter id
     |> List.length

printfn $"Number of valid reports in first part: %A{firstValidReports}"


let isValidList (numbers: int list) =
    let increasing = isIncreasing numbers
    let decreasing = isDecreasing numbers
    let differencesValid = areDifferencesValid numbers
    (increasing || decreasing) && differencesValid

let isValidWithRemoval (numbers: int list) =
    let rec checkSublists (numbers: int list) =
        match numbers with
        | [] -> false
        | [_] -> false
        | _ ->
            let sublists =
                numbers
                |> List.mapi (fun i _ ->
                    numbers
                    |> List.mapi (fun j x -> if i = j then None else Some x)
                    |> List.choose id)
            sublists
            |> List.exists isValidList
    isValidList numbers || checkSublists numbers

let validateLists (lists: string list list) =
    let results =
        lists
        |> List.map (fun strings ->
            let numbers = convertStringsToIntegers strings
            (strings, isValidWithRemoval numbers))
    let validCount =
        results
        |> List.filter (fun (_, isValid) -> isValid)
        |> List.length
    (results, validCount)

let (results, validCount) = validateLists reports

printfn $"Number of valid reports in second part: %d{validCount}"