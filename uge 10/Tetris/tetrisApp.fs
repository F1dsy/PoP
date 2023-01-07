open Canvas
open Tetris
open System.Diagnostics

let fromColorToCanvasColor (color: Color) : color =
    match color with
    | Yellow -> yellow
    | Cyan -> fromRgb (0, 255, 255)
    | Blue -> blue
    | Orange -> fromRgb (255, 165, 0)
    | Red -> red
    | Green -> green
    | Purple -> fromRgb (255, 0, 255)



// type board(w: int, h: int) =
//     let _board = Array2D.create w h None
//     do _board.[0, 1] <- Some Green
//     member this.width = w
//     member this.height = h
//     member this.board = _board

type state = board

let draw (w: int) (h: int) (s: state) =
    // insert your definition of draw here
    let C = create w h

    for i = 0 to s.width - 1 do
        for j = 0 to s.height - 1 do
            let field = s.board[i, j]

            if field.IsSome then
                setFillBox C (fromColorToCanvasColor field.Value) (i * 30, j * 30) ((i + 1) * 30, (j + 1) * 30)
                setBox C black (i * 30, j * 30) ((i + 1) * 30, (j + 1) * 30)

    C

let react (s: state) (k: key) : state option =

    let moveWithOffset (xoff, yoff) : unit =
        let piece = s.take ()
        ()

        if piece.IsNone then
            ()
        else
            let (x, y) = piece.Value.offset
            let mutable didPut = false

            let pieceWithOffset =
                tetromino (piece.Value.image, piece.Value.col, (x + xoff, y + yoff))

            try
                didPut <- s.put pieceWithOffset
                ()
            with
            | :? System.IndexOutOfRangeException ->
                let piecewithoutX = tetromino (piece.Value.image, piece.Value.col, (x, y + yoff))
                didPut <- s.put piecewithoutX
                ()

            if didPut then
                ()
            else
                s.put piece.Value |> ignore
                s.newPiece () |> ignore

    match getKey k with
    | DownArrow -> moveWithOffset (0, 1)
    | LeftArrow -> moveWithOffset (-1, 1)
    | RightArrow -> moveWithOffset (1, 1)
    | Space ->
        let piece = s.take ()

        if piece.IsNone then
            ()
        else
            let mutable p = piece.Value
            let (x, y) = p.offset

            if p.height + x > s.width then
                p <- tetromino (piece.Value.image, piece.Value.col, (s.width - p.height, y))
                printfn "width: %A, height+x: %A" s.width (p.height + x)
                ()

            p.rotateRight ()
            printfn "Rotated"
            s.put p |> ignore
    | _ -> ()




    Some s

let runGame () =
    let b: state = board (10, 20)
    b.newPiece () |> ignore
    let C = draw 300 600 b

    // show C "testing"

    runApp "" 300 600 draw react b

let tests () =

    let Assert (description: string, condition: bool) =
        printfn "%s: %A" description condition
        ()

    let b: state = board (10, 20)
    let p = tetromino (array2D [ [ false ]; [ false ] ], Blue, (0, 0))

    Assert("Piece color is Blue", p.col = Blue)
    Assert("Piece width is 2", p.width = 2)
    Assert("Piece height is 1", p.height = 1)
    Assert("Piece image is [ [ false ]; [ false ] ]", p.image = array2D [ [ false ]; [ false ] ])
    Assert("Piece offset is (0,0)", p.offset = (0, 0))
    p.rotateRight ()
    Assert("Piece image is [ [ false; false] ]", p.image = array2D [ [ false; false ] ])
    Assert("Piece width is 1", p.width = 1)
    Assert("Piece height is 2", p.height = 2)


    Assert("Board width is 10", b.width = 10)
    Assert("Board height is 20", b.height = 20)
    Assert("Board is empty", b.board = (Array2D.init 10 20 (fun _ __ -> None)))
    Assert("Can place piece on board", b.put p = true)
    let pyoff = tetromino (array2D [ [ false ]; [ false ] ], Blue, (0, 21))
    Assert("Can not place piece on board", b.put pyoff = false)
    Assert("Can take active piece from board", b.take () = Some p)
    Assert("Board is empty", b.board = (Array2D.init 10 20 (fun _ __ -> None)))
    Assert("Can not take active piece from empty board", b.take () = None)

    ()

tests ()
runGame ()
