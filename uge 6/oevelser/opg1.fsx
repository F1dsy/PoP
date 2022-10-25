module opg1

#load "opg0.fsx"

open opg0

let line (a0: float) (a1: float) (x: float) : float = poly [ a0; a1 ] x
