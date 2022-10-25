#load "opg9.fsx"

open opg9

// find : pred: ('a -> bool) -> 'a tree -> 'a option
let find (pred: 'a -> bool) (tree: 'a tree) : 'a option =
    let lst = preorder tree
    List.tryFind pred lst


printfn "pred list: %A" (find (fun x -> x = 5) tititi)
