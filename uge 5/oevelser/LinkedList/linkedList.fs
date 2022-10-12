module LinkedList

type linkedList<'a> =
    | Nil
    | Cons of 'a * linkedList<'a>


let isEmpty (list: linkedList<'a>) : bool = if (list = Nil) then true else false

let rec length (list: linkedList<'a>) : int =
    match list with
    | Nil -> 0
    | Cons (a, b) -> 1 + length (b)


let add (num: 'a) (list: linkedList<'a>) : linkedList<'a> = Cons(num, list)

let head (list: linkedList<'a>) : 'a =
    match list with
    | Cons (a, b) -> a

let tail (list: linkedList<'a>) : linkedList<'a> =
    match list with
    | Nil -> Nil
    | Cons (a: 'a, b: linkedList<'a>) -> b

let fold (folder: 's -> 'a -> 's) (state: 's) (list: linkedList<'a>) : 's =
    if (isEmpty list) then
        state
    else
        let mutable acc = state
        let mutable lst = list

        while (not (isEmpty lst)) do
            let h = head lst
            lst <- tail lst
            acc <- folder acc h

        acc

// let foldBack (folder: 'a -> 's -> 's) (list: 'a) (state: 's): 's=
