module IntQueue

type queue = int list

let emptyQueue: queue = []

/// <summary>Add an element at the end of the queue</summary>
/// <param name="number"></param>
/// <param name="queue"></param>
/// <returns>new queue with added element</returns>
let enqueue (num: int) (queue: queue) : queue = queue @ [ num ]

/// <summary>Check if a queue is empty</summary>
/// <param name="queue"></param>
/// <returns>true if empty, false if not</returns>
let isEmpty (queue: queue) : bool = List.isEmpty queue

/// <summary>Remove element from front of queue</summary>
/// <param name="queue"></param>
/// <returns>Tuple of integer and queue</returns>
let dequeue (queue: queue) : int * queue = (queue.Head, queue.Tail)
