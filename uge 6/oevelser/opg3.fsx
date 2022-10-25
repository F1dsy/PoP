#load "opg2.fsx"

open opg2

//integrate : n:int -> a:float -> b:float -> (f : float -> float) -> float
let integrate (n: int) (a: float) (b: float) (f: float -> float) : float =
    let dx = (b - a) / float (n)
    let mutable sum = 0.0

    for i = 0 to n do
        let xi = a + float (i) * dx
        sum <- sum + (f xi) * dx

    sum

let l1 = theLine 1 1

printfn "%A" (l1 4)
printfn "%A" (integrate 400000 0 4 l1)
