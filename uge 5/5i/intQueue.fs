module IntQueue

type queue = int list

let emptyQueue: queue = []

let enqueue (num: int) (queue: queue) : queue = queue @ [ num ]

let isEmpty (queue: queue) : bool = List.isEmpty queue

let dequeue (queue: queue) : int * queue = (queue.Head, queue.Tail)
