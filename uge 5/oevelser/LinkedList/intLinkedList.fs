module IntLinkedList

type intLinkedList =
    | Nil
    | Cons of int * intLinkedList

let isEmpty (list: intLinkedList) : bool = List.isEmpty list

let length (list: intLinkedList) : int = List.length list

let add (num: int) (list: intLinkedList) : intLinkedList = list @ [ num ]
