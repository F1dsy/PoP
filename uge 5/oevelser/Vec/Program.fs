module Vec

type vec = float * float


/// <summary>
/// Given two vectors, add them together
/// </summary>
/// <param name="vec1">First Vector</param>
/// <param name="vec2">Second Vector</param>
/// <returns>New Vector vec1 + vec2</returns>
let add ((a1, b1): vec) ((a2, b2): vec) : vec = a1 + a2, b1 + b2

/// <summary>
///  Given a vector and a scalar value, multiply vector with scalar
/// </summary>
/// <param name="vec1">First Vector</param>
/// <param name="c">Constant</param>
/// <returns>New Vector vec1 * c</returns>
let mul ((a1, b1): vec) (c: float) : vec = a1 * c, b1 * c

/// <summary>
///  Given a vector, rotate it by "r" radians
/// </summary>
/// <param name="vec">Vector</param>
/// <param name="r">Rotation value in Radians</param>
/// <returns>New Vector Rotation(vec)</returns>
let rot ((a, b): vec) (r: float) : vec =
    (a * cos (r) - b * sin (r), a * sin (r) + b * cos (r))
