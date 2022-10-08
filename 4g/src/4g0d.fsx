type pos = int * int

let dist (p1: pos) (p2: pos) : int =
    let (x1, y1) = p1
    let (x2, y2) = p2
    (pown (x2 - x1) 2 + pown (y2 - y1) 2)

let candidates (src: pos) (tg: pos) : pos list =
    let (x, y) = src

    let cand =
        [ (x + 1, y)
          (x - 1, y)
          (x, y + 1)
          (x, y - 1)
          (x + 1, y + 1)
          (x + 1, y - 1)
          (x - 1, y + 1)
          (x - 1, y - 1) ]

    List.filter (fun (p: pos) -> dist src tg > dist p tg) cand




let rec routes (src: pos) (tg: pos) : pos list list =
    if src = tg then
        [ [ src ] ]
    else
        let c = candidates src tg

        if c.Length = 1 || c.Length = 0 then
            c
        else
            let distances = List.map (fun e -> dist e tg) c
            let max = List.max distances
            List.filter (fun e -> dist e tg <> max) c
        |> List.map (fun (cand: pos) -> routes cand tg)
        |> List.collect id
        |> List.map (fun (lst: pos list) -> src :: lst)
