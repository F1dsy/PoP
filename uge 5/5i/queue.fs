module Queue

type queue<'a> = 'a list

let emptyQueue: queue<'a> = []

let enqueue (num: 'a) (queue: queue<'a>) : queue<'a> = queue @ [ num ]

let isEmpty (queue: queue<'a>) : bool = List.isEmpty queue

let dequeue (queue: queue<'a>) : ('a option) * queue<'a> =
    if (queue.IsEmpty) then
        (None, [])
    else
        (Some(queue.Head), queue.Tail)
