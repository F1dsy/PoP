// #load "CatList.fs"
// #load "DiffList.fs"

open CatList
// open DiffList

let ll = [ 1; 2; 3; 4; 5; 6 ]

let cl = toCatList ll

let cl2 = insert 3 10 cl

printfn "%A" (fromCatList cl2)

let cl3 = delete 4 cl2

printfn "%A" (fromCatList cl3)
