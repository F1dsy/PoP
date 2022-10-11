module linkedList<'a>

type LinkedList<'a> =
    | Nil
    | Cons of 'a * LinkedList<'a>


let isEmpty (list: linkedList<'a>) : bool = if (list = Nil) then true else false

let rec length (list: linkedList<'a>) : 'a =
    match list with
    | Nil -> 0
    | Cons (a, b) -> 1 + length (b)


let add (num: 'a) (list: linkedList<'a>) : linkedList<'a> = Cons(num, list)

let head (list: linkedList<'a>) : 'a =
    match list with
    | Nil -> 0
    | Cons (a, b) -> a

let tail (list: linkedList<'a>) : linkedList<'a> =
    match list with
    | Nil -> Nil
    | Cons (a: 'a, b: linkedList<'a>) -> b
