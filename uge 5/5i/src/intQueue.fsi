module IntQueue

type queue = int list

val emptyQueue: queue

val enqueue: int -> queue -> queue

val dequeue: queue -> (int * queue)

val isEmpty: queue -> bool
