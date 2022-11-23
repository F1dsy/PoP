module CatList

open DiffList

type 'a catlist =
    | Empty
    | Single of 'a
    | Append of 'a catlist * 'a catlist


let nil = Empty
let single (elm: 'a) : 'a catlist = Single elm

let append (xs: 'a catlist) (ys: 'a catlist) : 'a catlist = Append(xs, ys)
let cons (elm: 'a) (xs: 'a catlist) : 'a catlist = Append(single elm, xs)

let snoc (xs: 'a catlist) (elm: 'a) : 'a catlist = Append(xs, single elm)


let rec length xs : int =
    match xs with
    | Empty -> 0
    | Single _ -> 1
    | Append (ys, zs) -> length ys + length zs

let rec sum (xs: int catlist) : int =
    match xs with
    | Empty -> 0
    | Single num -> num
    | Append (ys, zs) -> sum ys + sum zs


let fold (cf: ('a -> 'a -> 'a), (e: 'a)) (f: ('b -> 'a)) (xs: 'b catlist) : 'a = failwith "Not Implemented"

let rec fromCatList (xs: 'a catlist) : 'a list =
    match xs with
    | Empty -> []
    | Single v -> [ v ]
    | Append (v, u) ->
        List.concat [ fromCatList v
                      fromCatList u ]





let rec toCatList (xs: 'a list) : 'a catlist =
    match xs with
    | [] -> Empty
    | elm :: lst -> Append(Single elm, toCatList lst)

let item (i: int) (xs: 'a catlist) : 'a = failwith "Not Implemented"

let insert (i: int) (elm: 'a) (xs: 'a catlist) : 'a catlist = failwith "Not Implemented"

let delete (i: int) (xs: 'a catlist) : 'a catlist = failwith "Not Implemented"