module Queue

type queue<'a> = 'a list

let emptyQueue: queue<'a> = []

/// <summary>Add an element at the end of the queue</summary>
/// <param name="element"></param>
/// <param name="queue"></param>
/// <returns>new queue with added element</returns>
let enqueue (elm: 'a) (queue: queue<'a>) : queue<'a> = queue @ [ elm ]

/// <summary>Check if a queue is empty</summary>
/// <param name="queue"></param>
/// <returns>true if empty, false if not</returns>
let isEmpty (queue: queue<'a>) : bool = List.isEmpty queue

/// <summary>Remove element from front of queue</summary>
/// <param name="queue"></param>
/// <returns>Tuple of element and queue</returns>
let dequeue (queue: queue<'a>) : ('a option) * queue<'a> =
    if (queue.IsEmpty) then
        (None, [])
    else
        (Some(queue.Head), queue.Tail)
