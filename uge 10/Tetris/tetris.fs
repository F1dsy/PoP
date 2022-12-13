module Tetris

type position = int * int

type Color =
    | Yellow
    | Cyan
    | Blue
    | Orange
    | Red
    | Green
    | Purple


type tetromino(a: bool [,], c: Color, o: position) =
    let mutable _shape = a
    let mutable _color = c
    let mutable _offset = o

    member this.col = _color

    member this.offset = _offset
    // and set (off: position) = (_offset <- off)

    member this.image = _shape
    member this.clone() = tetromino (_shape, _color, _offset)
    member this.width = _shape.GetLength(0)
    member this.height = _shape.GetLength(1)

    member this.rotateRight() =
        let transposed = (Array2D.init this.height this.width (fun x y -> _shape[y, x]))

        let reverse (i: bool [,]) : bool [,] =
            let reversed: bool [,] =
                Array2D.init this.height this.width (fun x y -> i[i.GetLength(0) - 1 - x, i.GetLength(1) - 1 - y])

            reversed

        _shape <- (reverse transposed)



    override this.ToString() = "tetris"

type z(mirrored: bool, o: position) =
    inherit tetromino
        (
            (if mirrored then
                 array2D [ [ true; true; false ]
                           [ false; true; true ] ]
             else
                 array2D [ [ false; true; true ]
                           [ true; true; false ] ]),
            (if mirrored then Red else Green),
            o
        )

type l(mirrored: bool, o: position) =
    inherit tetromino
        (
            (if mirrored then
                 array2D [ [ true; true; true ]
                           [ false; false; true ] ]
             else
                 array2D [ [ true; true; true ]
                           [ true; false; false ] ]),
            (if mirrored then Blue else Orange),
            o
        )

type square(o: position) =
    inherit tetromino
        (
            array2D [ [ true; true ]
                      [ true; true ] ],
            Yellow,
            o
        )

type straight(o: position) =
    inherit tetromino(array2D [ [ true; true; true; true ] ], Cyan, o)

type t(o: position) =
    inherit tetromino
        (
            array2D [ [ true; true; true ]
                      [ false; true; false ] ],
            Purple,
            o
        )

type board(w: int, h: int) =
    let _board: Color option [,] = Array2D.create w h None
    let mutable _activePiece: tetromino option = Some(t ((3, 0)))

    member this.board = _board
    member this.width = w
    member this.height = h

    member this.newPiece() : tetromino option =
        let rnd = System.Random()

        let isMirrored =
            if rnd.NextDouble() > 0.5 then
                true
            else
                false

        let piece = z (isMirrored, (0, 0))
        piece.offset <- (rnd.Next(w) - piece.width, 0)

        if (this.put piece) then
            _activePiece <- Some piece
            Some piece
        else
            None

    member this.put(t: tetromino) =
        let (x, y) = t.offset
        let mutable canPut = true

        Array2D.iteri
            (fun w h b ->
                if _board[x + w, y + h].IsSome then
                    canPut <- false

                )
            t.image

        if canPut = false then
            false
        else
            Array2D.iteri
                (fun w h b ->
                    if b then
                        _board[x + w, y + h] <- Some t.col)
                t.image

            true

    member this.take() =
        if _activePiece.IsNone then
            None
        else
            let (x, y) = _activePiece.Value.offset
            Array2D.iteri (fun w h b -> if b then _board[x + w, y + h] <- None) _activePiece.Value.image
            _activePiece

    override this.ToString() = "tetris"