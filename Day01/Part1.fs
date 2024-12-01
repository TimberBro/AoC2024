module AoC2024.Day01.Part1

open System
open System.IO


let readLines filePath = File.ReadAllLines(filePath)

let baseDirectory = __SOURCE_DIRECTORY__
let filePath = baseDirectory + @"\input.txt"
let lines = readLines filePath

let splitColumns (lines: string[]) =
    lines
    |> Array.map (fun line -> line.Split([|"   "; @"\t"|], StringSplitOptions.RemoveEmptyEntries))
    
let createListsFromColumns (columns: string[][]) =
    let column1 = columns |> Array.map (fun cols -> cols.[0]) |> Array.toList |> List.sort
    let column2 = columns |> Array.map (fun cols -> cols.[1]) |> Array.toList |> List.sort
    (column1, column2)
    
let calculateDistances (column1: string list, column2: string list) =
    List.map2 (fun elem1 elem2 -> abs (int elem1 - int elem2)) column1 column2

let countOccurrences (column: string list) =
    column
    |> List.countBy id
    |> Map.ofList

let calculateSimilarity (column1: string list, column2: string list) =
    let occurrences = countOccurrences column2
    column1
    |> List.map (fun elem ->
        let count = if occurrences.ContainsKey(elem) then occurrences.[elem] else 0
        int elem * count)
    
let sumDistances (distances: int list) =
    List.sum distances

let columns = splitColumns lines
let (column1, column2) = createListsFromColumns columns

let distances = calculateDistances (column1, column2)
let sum = sumDistances distances


let similarity = calculateSimilarity (column1, column2)
let score = sumDistances similarity 

printfn $"Sum: %d{sum}"
printfn $"Similarity Score: %d{score}"

