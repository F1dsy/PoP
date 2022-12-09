open CyclicQueue

[<EntryPoint>]
let main _ =
    printfn "Running Tests"
    let compare a b = if a = b then true else false

    let testCreate (n: int) = create n

    let testFillUpQueue =
        let queueSize = 4
        let correctResult = [ true; true; true; true; false ]
        testCreate queueSize
        let enqueueResults = List.init (queueSize + 1) (fun i -> enqueue i)

        compare correctResult enqueueResults

    let testEmptyQueue =
        let queueSize = 4
        let correctResult = [ Some 0; Some 1; Some 2; Some 3; None ]
        testCreate queueSize

        List.init (queueSize) (fun i -> enqueue i)
        |> ignore

        let dequeueResults = List.init (queueSize + 1) (fun _ -> dequeue ())
        compare correctResult dequeueResults

    let testQueueToString =
        let queueSize = 4
        create queueSize

        List.init (queueSize) (fun i -> enqueue (i + 1))
        |> ignore

        compare "1,2,3,4" (toString ())

    let testIsEmpty =
        let queueSize = 4
        testCreate queueSize
        let test1 = isEmpty ()
        enqueue 1 |> ignore
        let test2 = isEmpty ()

        compare test1 (not test2)

    let testLength =
        let queueSize = 4
        testCreate queueSize
        let test1 = length ()
        enqueue 1 |> ignore
        let test2 = length ()

        compare test1 (test2 - 1)

    printfn "TestFillUpQueue passed:   %A" testFillUpQueue
    printfn "TestEmptyQueue passed:    %A" testEmptyQueue
    printfn "TestQueueToString passed: %A" testQueueToString
    printfn "TestIsEmpty passed:       %A" testIsEmpty
    printfn "TestLength passed:        %A" testLength
    0
