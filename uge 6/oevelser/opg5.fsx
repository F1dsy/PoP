type expr =
    | Const of int
    | Add of expr * expr
    | Mul of expr * expr

let dims = Mul(Add(Const 5, Const 8), Const 9)


let rec eval (exp: expr) : int =
    match exp with
    | Mul (e1, e2) -> (eval e1) * (eval e2)
    | Add (e1, e2) -> (eval e1) + (eval e2)
    | Const (x) -> x

printfn "%A" (eval dims)
