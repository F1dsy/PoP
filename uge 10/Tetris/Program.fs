open Canvas
open Tetris

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

    C

let react (s: state) (k: key) : state option =

    let moveWithOffset (xoff, yoff) : unit =
        let piece = s.take ()
        ()

        if piece.IsNone then
            ()
        else
            let (x, y) = piece.Value.offset
            let mutable _xoff = xoff

            if (x + xoff < 0 || x + xoff >= s.width) then
                _xoff <- 0

            let newp = tetromino (piece.Value.image, piece.Value.col, (x + _xoff, y + yoff))

            if s.put newp then
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
            piece.Value.rotateRight ()
            s.put piece.Value |> ignore
    | _ -> ()




    Some s

let b: state = board (10, 20)
b.newPiece () |> ignore
let C = draw 300 600 b

// show C "testing"

runApp "" 300 600 draw react b
runApp "" 300 600 draw react b
runApp "" 300 600 draw react b
runApp "" 300 600 draw react b
