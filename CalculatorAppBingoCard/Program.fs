open System

let add x y = Some (x + y)
let subtract x y = Some (x - y)
let multiply x y = Some (x * y)
let divide x y = 
    if y = 0.0 then None
    else Some (x / y)

let getOperation op =
    match op with
    | "+" -> Some add
    | "-" -> Some subtract
    | "*" -> Some multiply
    | "/" -> Some divide
    | _ -> None

let tryParseDouble (input: string) =
    match Double.TryParse(input) with
    | (true, value) -> Ok value
    | (false, _) -> Error $"Invalid number: '{input}'"

let rec promptForOperator () =
    printf "Enter an operation (+, -, *, /): "
    let opStr = Console.ReadLine()
    match getOperation opStr with
    | Some operation -> Ok (opStr, operation)
    | None -> 
        printfn "Invalid operation. Please enter one of (+, -, *, /)."
        promptForOperator()

let calculate operation x y =
    match operation x y with
    | Some result -> Ok result
    | None -> Error "Division by zero is not allowed."

[<EntryPoint>]
let main argv =
    printf "Enter first number: "
    let xStr = Console.ReadLine()
    match tryParseDouble xStr with
    | Error errMsg ->
        printfn "Error: %s" errMsg
        1
    | Ok x ->

        match promptForOperator () with
        | Error errMsg ->
            printfn "Error: %s" errMsg
            1
        | Ok (_, operation) ->

            printf "Enter second number: "
            let yStr = Console.ReadLine()
            match tryParseDouble yStr with
            | Error errMsg ->
                printfn "Error: %s" errMsg
                1
            | Ok y ->

                match calculate operation x y with
                | Ok result -> 
                    printfn "Result: %f" result
                    0
                | Error errMsg ->
                    printfn "Error: %s" errMsg
                    1
