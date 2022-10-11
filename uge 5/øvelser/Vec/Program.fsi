module Vec

type vec = float * float

val add: (float * float) -> (float * float) -> vec

val mul: (float * float) -> float -> vec

val rot: (float * float) -> float -> vec
