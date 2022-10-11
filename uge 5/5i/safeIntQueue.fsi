module IntQueue

type queue = int list

// check if a queue is empty
val isEmpty: queue -> bool

// the empty queue
val emptyQueue: queue

// add an intat the end of a queue
val enqueue: int -> queue -> queue

// remove and return the intat the front of a queue
// precondition: input queue is not empty
val dequeue: queue -> (int option) * queue
