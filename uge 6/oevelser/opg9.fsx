module opg9

type 'a tree =
    | Leaf of 'a
    | Tree of 'a tree * 'a tree

let rec preorder (tree: 'a tree) : 'a list =
    match tree with
    | Leaf (x) -> [ x ]
    | Tree (t1, t2) -> (preorder t1) @ (preorder t2)


let tititi = Tree(Leaf 3, Tree(Leaf 5, Leaf 8))

printfn "Num of Leaves: %A" (preorder tititi)
