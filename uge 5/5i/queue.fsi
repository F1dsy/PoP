module Queue

type queue<'a> = 'a list

// check if a queue is empty
val isEmpty: queue<'a> -> bool

// the empty queue
val emptyQueue: queue<'a>

// add an intat the end of a queue
val enqueue: 'a -> queue<'a> -> queue<'a>

// remove and return the intat the front of a queue
// precondition: input queue is not empty
val dequeue: queue<'a> -> ('a option) * queue<'a>
