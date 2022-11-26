let fac (n: int) : int =
    let mutable sum = 1
    let mutable iter = 1

    while iter <= n do
        if System.Int32.MaxValue < sum then
            failwith "Nope"

        sum <- sum * iter
        iter <- iter + 1

    sum

let fac64 (n: int64) : int64 =
    let mutable sum: int64 = 1
    let mutable iter: int64 = 1

    while iter <= n do
        if System.Int64.MaxValue < sum then
            failwith "Nope"

        sum <- sum * iter
        iter <- iter + int64 (1)

    sum

// printfn "Write a number to find factorial:"

// let n = int (System.Console.ReadLine())

// printfn "%d" (fac n)

// printfn "Write a number to find factorial64:"

// let n64 = int (System.Console.ReadLine())

// printfn "%d" (fac n64)
