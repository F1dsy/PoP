module IntLinkedList

type intLinkedList =
    | Nil
    | Cons of int * intLinkedList

let isEmpty (list: intLinkedList) : bool = if (list = Nil) then true else false

let rec length (list: intLinkedList) : int =
    match list with
    | Nil -> 0
    | Cons (a, b) -> 1 + length (b)


let add (num: int) (list: intLinkedList) : intLinkedList = Cons(num, list)

let head (list: intLinkedList) : int =
    match list with
    | Nil -> 0
    | Cons (a, b) -> a

let tail (list: intLinkedList) : intLinkedList =
    match list with
    | Nil -> Nil
    | Cons (a: int, b: intLinkedList) -> b
