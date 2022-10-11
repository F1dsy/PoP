module SafeIntQueue

type queue = int list

let emptyQueue: queue = []

let enqueue (num: int) (queue: queue) : queue = queue @ [ num ]

let isEmpty (queue: queue) : bool = List.isEmpty queue

let dequeue (queue: queue) : (int option) * queue =
    if (queue.IsEmpty) then
        (None, [])
    else
        (Some(queue.Head), queue.Tail)
