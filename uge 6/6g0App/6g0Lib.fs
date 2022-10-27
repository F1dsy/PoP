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

/// <summary>Converts a 2048 - value v to a canvas color</summary>
/// <param name="v">value</param>
/// <returns>Canvas color</returns>
let fromValue (v: value) : Canvas.color =
    match v with
    | Red -> Canvas.red
    | Green -> Canvas.green
    | Blue -> Canvas.blue
    | Yellow -> Canvas.yellow
    | Black -> Canvas.black

/// <summary>Gives the 2048 - value which is the next in order from c</summary>
/// <param name="c">value</param>
/// <returns>next value</returns>
let nextColor (c: value) : value =
    //red 2-> green 4-> blue 8-> yellow 16-> black 32
    match c with
    | Red -> Green
    | Green -> Blue
    | Blue -> Yellow
    | Yellow -> Black
    | Black -> Black

/// <summary>Returns the list of pieces on a column k on board s</summary>
/// <param name="k">column index</param>
/// <param name="s">state</param>
/// <returns>column state</returns>
let filter (k: int) (s: state) : state =
    List.filter (fun (v, (x, y)) -> y = k) s


/// <summary>Flips the board s such that all pieces position change as</summary>
/// <param name="s">state</param>
/// <returns>flipped state</returns>
let flipUD (s: state) : state =
    List.map (fun (v, (x, y)) -> (v, (2 - x, y))) s


/// <summary>Transposes the pieces on the board s such all piece positions change as (i,j) -> (j,i)</summary>
/// <param name="s">state</param>
/// <returns>transposed state</returns>
let transpose (s: state) : state =
    List.map (fun (v, (x, y)) -> (v, (y, x))) s

// let shiftUp (s: state) : state =
//     let mutable ns: state = []

//     for c = 0 to 2 do

//         let mutable fltd = filter c s
//         printfn "%A" (filter c s)
//         fltd <- List.sortWith (fun (v, (x, y)) (v1, (x1, y1)) -> compare x x1) fltd
//         printfn "%A" (filter c s)

//         match fltd.Length with

//         | 1 ->
//             printfn "Only one"
//             let (v, (x, y)) = fltd.Head
//             fltd <- [ (v, (0, y)) ]
//         | 2 ->
//             let (v, (x, y)) = fltd.Head
//             let (v1, (x1, y1)) = fltd[1]

//             if v = v1 then
//                 fltd <- [ (nextColor v, (x, y)) ]

//         | 3 ->

//             let (v, (x, y)) = fltd.Head
//             let (v1, (x1, y1)) = fltd[1]
//             let (v2, (x2, y2)) = fltd[2]

//             if v1 = v2 then
//                 fltd <- [ fltd.Head; (nextColor v1, (x1, y1)) ]

//             if v1 = v then
//                 fltd <- [ (nextColor v, (x, y)); fltd[2] ]

//         | _ -> ()

//         if fltd.Length = 2 then
//             let (v1, (x1, y1)) = fltd[1]

//             if x1 = 2 then
//                 fltd <- [ fltd.Head; (v1, (1, y1)) ]

//         ns <- ns @ fltd


//     printfn "Now State: %A" ns
//     ns

let shiftUp (s: state) : state =
    let mutable ns: state = []

    // for c = 0 to 2 do
    // compress
    // merge
    // compress

    ns


/// <summary>Finds the list of empty positions on the board s</summary>
/// <param name="s">state</param>
/// <returns>list of empty positions</returns>
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

/// <summary> Randomly places a new piece of color c on an empty position on the board s</summary>
/// <param name="c">value of placed piece</param>
/// <param name="s">state</param>
/// <returns>new state</returns>
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
