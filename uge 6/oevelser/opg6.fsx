type expr =
    | Const of int
    | Add of expr * expr
    | Sub of expr * expr
    | Mul of expr * expr
    | Div of expr * expr

let dims = Div(Const 10, Const 0)


let rec eval (exp: expr) : int =
    match exp with
    | Mul (e1, e2) -> (eval e1) * (eval e2)
    | Add (e1, e2) -> (eval e1) + (eval e2)
    | Sub (e1, e2) -> (eval e1) - (eval e2)
    | Div (e1, e2) -> (eval e1) / (eval e2)
    | Const (x) -> x


try
    printfn "%A" (eval dims)
with
| :? System.DivideByZeroException -> printfn "Divide by 0 error"
