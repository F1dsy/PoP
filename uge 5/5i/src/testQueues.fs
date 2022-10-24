/// <summary>Test the intQueue</summary>
/// <returns>true if all tests are passed</returns>
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


/// <summary>Test the safeIntQueue</summary>
/// <returns>true if all tests are passed</returns>
let safeIntQueueTests () =
    let q0 = SafeIntQueue.emptyQueue
    let emptyTestResult = SafeIntQueue.isEmpty q0
    printfn "An empty queue is empty: %A" emptyTestResult

    let e1, e2, e3 = 1, 2, 3

    let q1 = q0

    let q1 = SafeIntQueue.enqueue e1 q1
    let q1 = SafeIntQueue.enqueue e2 q1
    let q1 = SafeIntQueue.enqueue e3 q1

    let nonEmptyTestResult = not (SafeIntQueue.isEmpty q1)
    printfn "A queue with elements is not empty: %A" nonEmptyTestResult

    let (e, q2) = SafeIntQueue.dequeue q1

    let dequeueTestResult = e = Some(e1)
    printfn "First in is first out: %A" dequeueTestResult



    let (a, b) = SafeIntQueue.dequeue q0
    let emptyDequeueResult = a = None
    printfn "An Empty dequeue returns None: %A" emptyDequeueResult

    let allTestResults =
        emptyTestResult
        && nonEmptyTestResult
        && dequeueTestResult
        && emptyTestResult

    printfn "All SafeIntQueue tests passed: %A" allTestResults


    // Return the test results as a boolean
    allTestResults

/// <summary>Test the generic Queue</summary>
/// <returns>true if all tests are passed</returns>
let genericQueueTests () =
    let qi = Queue.emptyQueue<int>
    let qf = Queue.emptyQueue<float>
    let qs = Queue.emptyQueue<string>

    let qi = Queue.enqueue 2 qi
    let qf = Queue.enqueue 2.0 qf
    let qs = Queue.enqueue "2" qs

    let (ai, b) = Queue.dequeue qi
    let (af, b) = Queue.dequeue qf
    let (astr, b) = Queue.dequeue qs

    let allTestResults: bool =
        (ai = Some(2))
        && (af = Some(2.0))
        && (astr = Some("2"))


    printfn "All generic queue tests passed: %A" allTestResults
    allTestResults



// Run the IntQueue tests
let intQueueTestResults = intQueueTests ()
let safeIntQueueTestResults = safeIntQueueTests ()
let genericQueueTestResults = genericQueueTests ()
