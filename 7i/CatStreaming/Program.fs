open CatStreaming

// The main entrypoint of the program
// We simply give all command line arguments to the cat function
[<EntryPoint>]
let main args =
    let rval = cat args
    printfn "%A" rval
    0
