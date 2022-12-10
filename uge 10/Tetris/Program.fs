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

let react (s: state, k: key) : state option =
    match getKey k with
    | DownArrow -> ()
    | LeftArrow -> ()
    | RightArrow -> ()
    | Space -> ()
    | _ -> ()




    Some s

let b = board (10, 20)
b.newPiece () |> ignore
let C = draw 300 600 b

show C "testing"
