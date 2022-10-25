open Lib

// draw: w: int -> h: int -> s: state -> canvas

let draw (w: int) (h: int) (s: state) : Canvas.canvas =
    let boardsize = 3
    let dw = w / boardsize
    let dh = h / boardsize
    let c = Canvas.create w h

    for (v, (x, y)) in s do
        Canvas.setFillBox c (fromValue v) (dh * y, dw * x) (dh * (y + 1), dw * (x + 1))

    c

// react: s: state -> k: key -> state option
let react (s: state) (k: Canvas.key) : state option =
    match Canvas.getKey k with
    | Canvas.UpArrow -> Some(shiftUp s)
    | Canvas.DownArrow -> Some((flipUD >> shiftUp >> flipUD) s)
    | Canvas.LeftArrow -> Some((transpose >> shiftUp >> transpose) s)
    | Canvas.RightArrow ->
        Some(
            (transpose
             >> flipUD
             >> shiftUp
             >> flipUD
             >> transpose)
                s
        )
    | _ -> None



let w = 600
let h = w

let s: piece list =
    [ (Red, (1, 0))
      (Blue, (0, 0))
      (Yellow, (1, 1)) ]

Canvas.runApp "app" w h draw react s
