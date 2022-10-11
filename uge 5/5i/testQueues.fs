let intQueueTests () =
    let q0 = IntQueue.emptyQueue
    let emptyTestResult = IntQueue.isEmpty q0
    printfn "An empty queue is empty: %A" emptyTestResult

    let e1, e2, e3 = 1, 2, 3

    let q1 = q0

    let q1 = IntQueue.enqueue e1 q1
    let q1 = IntQueue.enqueue e2 q1
    let q1 = IntQueue.enqueue e3 q1

    let nonEmptyTestResult = not (IntQueue.isEmpty q1)
    printfn "A queue with elements is not empty: %A" nonEmptyTestResult

    let (e, q2) = IntQueue.dequeue q1

    let dequeueTestResult = e = e1
    printfn "First in is first out: %A" dequeueTestResult

    let allTestResults =
        emptyTestResult
        && nonEmptyTestResult
        && dequeueTestResult

    printfn "All IntQueue tests passed: %A" allTestResults
    // Return the test results as a boolean
    allTestResults

// Run the IntQueue tests
let intQueueTestResults = intQueueTests ()
