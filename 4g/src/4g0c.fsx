type pos = int * int
open System.Collections.Generic

/// <summary>dist calculate the distance between 2 points</summary>
/// <param p1>First position</param>
/// <param p2>Second position</param>
/// <returns>The calculated distance between 2 given points</returns>
let dist (p1: pos) (p2: pos) : int =
    let (x1, y1) = p1
    let (x2, y2) = p2
    (pown (x2 - x1) 2 + pown (y2 - y1) 2)

printfn "%A" (dist (1, 3) (2, 3))

/// <summary>candidates brings the robot closer to the target position</summary>
/// <param src>Source position</param>
/// <param tg>The target position</param>
/// <returns>Distance closer or equal to distance to target position</returns>
let candidates (src: pos) (tg: pos) : pos list =
    let (x, y) = src

    let cand =
        [ (x + 1, y)
          (x - 1, y)
          (x, y + 1)
          (x, y - 1) ]

    List.filter (fun (p: pos) -> dist src tg >= dist p tg) cand

printfn "%A" (candidates (3, 3) (2, 2))

/// <summary>routes calculates the shortest possible route from src position to tg position </summary>
/// <param src>Source position </param>
/// <param tg>Target position </param>
/// <returns>Shortest possible route to target from source </returns>
let rec routes (src: pos) (tg: pos) : pos list list =
    if src = tg then
        [ [ src ] ]
    else
        candidates src tg
        |> List.map (fun (cand: pos) -> routes cand tg)
        |> List.collect id
        |> List.map (fun (lst: pos list) -> src :: lst)

printfn "%A" (routes (3, 3) (1, 1))
