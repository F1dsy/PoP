module Lib

type pos = int * int // A 2 - dimensional vector in board - coordinats (notpixels )

type value =
    | Red
    | Green
    | Blue
    | Yellow
    | Black // piece values

type piece = value * pos //
type state = piece list // the board is a set of randomly organizedpieces

let fromValue (v: value) : Canvas.color =
    match v with
    | Red -> Canvas.red
    | Green -> Canvas.green
    | Blue -> Canvas.blue
    | Yellow -> Canvas.yellow
    | Black -> Canvas.black

let nextColor (c: value) : value =
    //red 2-> green 4-> blue 8-> yellow 16-> black 32
    match c with
    | Red -> Green
    | Green -> Blue
    | Blue -> Yellow
    | Yellow -> Black
    | Black -> Black

let filter (k: int) (s: state) : state =
    List.filter (fun (v, (x, y)) -> y = k) s


let flipUD (s: state) : state =
    List.map (fun (v, (x, y)) -> (v, (2 - x, y))) s


let transpose (s: state) : state =
    List.map (fun (v, (x, y)) -> (v, (y, x))) s


let shiftUp (s: state) : state =
    let mutable ns: state = []

    for (v, (x, y)) in s do
        ns <- ns @ [ (v, ((if x = 0 then x else x - 1), y)) ]

    ns

let empty (s: state) : pos list =
    let emptySpots: pos list =
        [ (0, 0)
          (0, 1)
          (0, 2)
          (1, 0)
          (1, 1)
          (1, 2)
          (2, 0)
          (2, 1)
          (2, 2) ]

    let positions = List.map (fun (v, pos) -> pos) s
    List.except positions emptySpots

let addRandom (c: value) (s: state) : state option =
    let emptySpots = empty s

    if (List.length emptySpots) = 0 then
        None
    else
        let rnd = System.Random()
        let nrn = rnd.Next(List.length emptySpots)
        let npos = emptySpots.[nrn]
        let ns = (c, npos)
        Some(s @ [ ns ])
