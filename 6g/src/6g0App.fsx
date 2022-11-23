#r "nuget: DIKU.Canvas, 1.0.1"
#load "6g0Lib.fs"

open Lib

/// <summary>Draws board from current state</summary>
/// <param name="w">width of window</param>
/// <param name="h">height of window</param>
/// <param name="s">state</param>
/// <returns>canvas</returns>
let draw (w: int) (h: int) (s: state) : Canvas.canvas =
    let boardsize = 3
    let dw = w / boardsize
    let dh = h / boardsize
    let c = Canvas.create w h

    for (v, (x, y)) in s do
        Canvas.setFillBox c (fromValue v) (dh * y, dw * x) (dh * (y + 1), dw * (x + 1))

    c

/// <summary>Reacts to user input</summary>
/// <param name="s">state</param>
/// <param name="k">pressed key</param>
/// <returns>Some(state) if user pressed valid key, None if non-valid key is pressed</returns>
let react (s: state) (k: Canvas.key) : state option =
    let reacted =
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

    if (reacted = None) then
        reacted
    else
        addRandom Red reacted.Value




let w = 600
let h = w

let s: piece list =
    [ (Red, (0, 0))
      (Red, (2, 0))
      (Red, (1, 0)) ]

Canvas.runApp "app" w h draw react s
