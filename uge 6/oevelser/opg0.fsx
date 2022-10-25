module opg0

let poly (coeff: float list) (x: float) : float =
    let mutable sum: float = 0

    for i = 0 to (List.length coeff - 1) do

        sum <- sum + ((x ** i) * coeff[i])

    sum
