module CatList


type 'a catlist =
    | Empty
    | Single of 'a
    | Append of 'a catlist * 'a catlist


let nil = Empty
let single (elm: 'a) : 'a catlist = Single elm

let append (xs: 'a catlist) (ys: 'a catlist) : 'a catlist = Append(xs, ys)
let cons (elm: 'a) (xs: 'a catlist) : 'a catlist = Append(single elm, xs)

let snoc (xs: 'a catlist) (elm: 'a) : 'a catlist = Append(xs, single elm)

let fold (cf: ('a -> 'a -> 'a), (e: 'a)) (f: ('b -> 'a)) (xs: 'b catlist) : 'a =
    let rec inner xs =
        match xs with
        | Empty -> e
        | Single s -> f s
        | Append (u, v) -> cf (inner u) (inner v)

    inner xs

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


let toCatList' (xs: 'a list) : 'a catlist = List.foldBack cons xs nil


let fromCatList' (xs: 'a catlist) : 'a list =
    DiffList.fromDiffList (fold (DiffList.append, DiffList.nil) DiffList.single xs)



let item (i: int) (xs: 'a catlist) : 'a =
    let rec k i' xs' =
        match xs' with
        | Empty -> failwith "Listen er tom"
        | Single x when i' = 0 -> x
        | Append (x, y) ->
            if i' >= length (x) then
                k (i' - length (x)) y
            else
                k i' x

    k i xs


let insert (i: int) (elm: 'a) (xs: 'a catlist) : 'a catlist =
    let rec h i' xs' =
        match xs' with
        | Empty -> Single elm
        | Single xs' -> Append(Single xs', Single elm)
        | Append (x, y) ->
            if i' >= length (x) then
                Append(x, h (i' - length (x)) y)
            else
                Append(h i' x, y)


    h i xs

let delete (i: int) (xs: 'a catlist) : 'a catlist =
    let rec j i' xs' =
        match xs' with
        | Empty -> Empty
        | Single _ -> Empty
        | Append (x, y) ->
            if i' >= length (x) then
                Append(x, j (i' - length (x)) y)
            else
                Append(j i' x, y)

    j i xs
