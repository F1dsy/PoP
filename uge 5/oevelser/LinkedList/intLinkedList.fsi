module IntLinkedList

type intLinkedList =
    | Nil
    | Cons of int * intLinkedList

val head: intLinkedList -> int
val tail: intLinkedList -> intLinkedList
val isEmpty: intLinkedList -> bool
val length: intLinkedList -> int
val add: int -> intLinkedList -> intLinkedList
