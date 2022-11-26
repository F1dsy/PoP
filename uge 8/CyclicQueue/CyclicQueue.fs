module CyclicQueue

type Value = int

let mutable q: Value option [] = [||]

let first = q[0]
let last = q[q.Length - 1]

let create (n: int) : unit = q <- Array.init n (fun _ -> None)

let enqueue (e: Value) : bool = failwith "Not implemented yet: enqueue"

let dequeue () : Value option = failwith "Not implemented yet: dequeue"

let isEmpty () : bool =
    Array.exists (fun (v: Value option) -> v.IsNone) q


let length () : int = q.Length

let toString () : string =
    failwith "Not implemented yet: toString"
