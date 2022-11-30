module CyclicQueue

type Value = int

let mutable q: Value option [] = [||]
let mutable first: int option = None
let mutable last: int option = None

let create (n: int) : unit =
    first <- None
    last <- None
    q <- Array.init n (fun _ -> None)

let isEmpty () : bool =
    if (first = None && last = None) then
        true
    else
        false

let enqueue (e: Value) : bool =
    if first = None then first <- Some 0

    if (last.IsSome)
       && ((first.Value = 0 && last.Value = q.Length - 1)
           || (first.Value = last.Value + 1)) then
        false
    else

        last <-
            if last.IsSome then
                Some((last.Value + 1) % q.Length)
            else
                Some 0

        q[last.Value] <- Some e
        true

let dequeue () : Value option =
    if isEmpty () then
        None
    else
        let res = q[first.Value]
        first <- Some((first.Value + 1) % q.Length)

        if ((first.Value = 0 && last.Value = q.Length - 1)
            || (first.Value = last.Value + 1)) then
            first <- None
            last <- None

        res

let length () : int =
    Array.fold (fun s (i: Value option) -> if i.IsNone then s else s + 1) 0 q

let toString () : string =
    String.concat
        ","
        (Array.map
            (fun (x: Value option) ->
                if x.IsSome then
                    x.Value.ToString()
                else
                    "None")
            q)
