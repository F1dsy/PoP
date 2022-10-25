module LinkedList

type linkedList<'a> =
    | Nil
    | Cons of 'a * linkedList<'a>

val head: linkedList<'a> -> 'a
val tail: linkedList<'a> -> linkedList<'a>
val isEmpty: linkedList<'a> -> bool
val length: linkedList<'a> -> int
val add: 'a -> linkedList<'a> -> linkedList<'a>

val fold: ('s -> 'a -> 's) -> 's -> linkedList<'a> -> 's
// val foldBack: ('a -> 's -> 's) -> list<'a> -> 's -> 's
