module LinkedList

type LinkedList<'a> =
    | Nil
    | Cons of 'a * LinkedList<'a>

val head: LinkedList<'a> -> 'a
val tail: LinkedList<'a> -> LinkedList<'a>
val isEmpty: LinkedList<'a> -> bool
val length: LinkedList<'a> -> 'a
val add: 'a -> LinkedList<'a> -> LinkedList<'a>
