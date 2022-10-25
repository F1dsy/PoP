type 'a tree =
    | Leaf of 'a
    | Tree of 'a tree * 'a tree

let rec leaves (tree: 'a tree) : int =
    match tree with
    | Leaf (x) -> 1
    | Tree (t1, t2) -> (leaves t1) + (leaves t2)


let tititi = Tree(Leaf 1, Tree(Leaf 1, Leaf 1))

printfn "Num of Leaves: %A" (leaves tititi)
