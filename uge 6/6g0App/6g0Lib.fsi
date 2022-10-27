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


// convert a 2048 - value v to a canvas color E.g. ,
// > fromValue Green ;;
// val it: color = { r = 0uy
// g = 255 uy
// b = 0uy
// a = 255 uy }
val fromValue: v: value -> Canvas.color

// give the 2048 - value which is the next in order from c, e.g. ,
// > nextColor Blue ;;
// val it: value = Yellow
// > nextColor Black ;;
// val it: value = Black
val nextColor: c: value -> value

// return the list of pieces on a column k on board s, e.g.
// > filter 0 [( Blue , (1 , 0)); (Red , (0 , 0))];;
// val it: state = [( Blue , (1 , 0)); (Red , (0 , 0))]
// > filter 1 [( Blue , (1 , 0)); (Red , (0 , 0))];;
// val it: state = []
val filter: k: int -> s: state -> state

// flip the board s such that all pieces position change as
// (i,j) -> (2 -i,j), e.g.
// > flipUD [( Blue , (1 , 0)); (Red , (2 , 0))];;
// val it: state = [( Blue , (1 , 0)); (Red , (0 , 0))]
val flipUD: s: state -> state

// transpose the pieces on the board s such all piece positions
// change as (i,j) -> (j,i), e.g. ,
// > transpose [( Blue , (1 , 0)); (Red , (2 , 0))];;
// val it: state = [( Blue , (0 , 1)); (Red , (0 , 2))]
val transpose: s: state -> state

// tilt all pieces on the board s to the left ( towards zero on the first coordinate ), e.g. ,
// > shiftUp [( Blue , (1 , 0)); (Red , (2 , 0)); (Black , (1 ,1))];;
// val it: state = [( Blue , (0 , 0)); (Red , (1 , 0)); (Black , (0 ,1))]
val shiftUp: s: state -> state

// find the list of empty positions on the board s, e.g. ,
// > empty [( Blue , (1 , 0)); (Red , (2 , 0))];;
// val it: pos list = [(0 , 0); (0 , 1); (0 , 2); (1 , 1); (1 , 2);
//(2 , 1); (2 , 2)]
val empty: s: state -> pos list


// randomly place a new piece of color c on an empty position on
// the board s, e.g. ,
// > addRandom Red [( Blue , (1 , 0)); (Red , (2 , 0))];;
// val it: state option = Some [( Red , (0 , 2)); (Blue , (1 , 0));
//(Red , (2 , 0))]
val addRandom: c: value -> s: state -> state option
