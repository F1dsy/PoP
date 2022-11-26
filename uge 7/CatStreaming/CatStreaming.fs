module CatStreaming

open System.IO


// RECOMMENDED BUT OPTIONAL:
//     Implement this function if you want to
//     If you remove it, remember to remove it from CatStreaming.fsi as well
let readBytes (count: int) (buffer: byte []) (fs: FileStream) : int =
    let readBytes = fs.Read(buffer, 0, count)
    readBytes
// Replace with a proper implementation

// RECOMMENDED BUT OPTIONAL:
//     Implement this function if you want to
//     If you remove it, remember to remove it from CatStreaming.fsi as well
let writeBytes (count: int) (buffer: byte []) (fs: FileStream) : unit =
    printfn "Writing %d bytes to filestream" (Array.length buffer)
    printfn "CanWrite: %A" fs.CanWrite
    fs.Write(buffer, 0, count)
    fs.Flush()
// fs.Write buffer
// Replace with a proper implementation


// RECOMMENDED BUT OPTIONAL:
//     Implement this function if you want to
//     If you remove it, remember to remove it from CatStreaming.fsi as well
let readAndWriteBytes (buffersize: int) (buffer: byte []) (ifs: FileStream) (ofs: FileStream) =
    let out = readBytes buffersize buffer ifs
    printfn "%A" buffer
    writeBytes buffersize buffer ofs
    out
// Replace with a proper implementation

// RECOMMENDED BUT OPTIONAL:
//     Implement this function if you want to
//     If you remove it, remember to remove it from CatStreaming.fsi as well
let openFileRead (filename: string) : FileStream =
    // Replace with a proper implementation
    try
        File.OpenRead filename
    with
    | _ -> raise (FileNotFoundException())


// RECOMMENDED BUT OPTIONAL:
//     Implement this function if you want to
//     If you remove it, remember to remove it from CatStreaming.fsi as well
let openFilesRead (filenames: string list) : FileStream option list =
    List.map
        (fun name ->
            try
                openFileRead name |> Some
            with
            | _ -> None)
        filenames

// Replace with a proper implementation

// RECOMMENDED BUT OPTIONAL:
//     Implement this function if you want to
//     If you remove it, remember to remove it from CatStreaming.fsi as well
let openFileWrite (filename: string) : FileStream option =
    try
        // File.Open(filename, FileMode.Create)
        File.OpenWrite filename |> Some

    with
    | _ ->
        sprintf "cat: Could not open or create file \"%s\"" filename
        |> System.Console.Error.WriteLine

        None


// Replace with a proper implementation

// REQUIRED: Your implemenation *must* include this function
let catWithBufferSize (buffersize: int) (filenames: string []) : int =
    match Array.length filenames with
    | 0 ->
        System.Console.Error.WriteLine "cat: no input files"
        255
    | 1 ->
        let file = openFileWrite filenames[0]
        writeBytes 0 [||] file.Value
        0
    | len ->
        let infiles = filenames[.. len - 2]
        let outfile = filenames[len - 1]
        let outstream = openFileWrite outfile

        // for file in infiles do
        //     let instream =openFileRead file
        let streams = openFilesRead (Array.toList infiles)

        if List.exists ((=) None) streams then
            let mutable exitStatus = 0

            List.iteri
                (fun i x ->
                    if x = None then
                        exitStatus <- exitStatus + 1

                        sprintf "cat: The file %s does not exist or is not readable." filenames.[i]
                        |> System.Console.Error.WriteLine
                    else
                        ())
                streams

            exitStatus
        else

            // readAndWriteBytes buffersize (Array.zeroCreate buffersize) streams[0].Value outstream.Value

            List.iter
                (fun (stream: FileStream option) ->
                    let mutable sll = 32

                    while sll = buffersize do
                        sll <- readAndWriteBytes buffersize (Array.zeroCreate buffersize) stream.Value outstream.Value)
                streams
            |> ignore

            0




// let inputStreams = openFilesRead (Array.toList filenames)
// let inputFileStream = openFileWrite filenames[0]

// for stream in inputStreams do
//     if stream.IsSome then
//         readBytes buffersize [||] stream.Value
//     else
//         return


// readAndWriteBytes buffersize [] inputFileStream outputFileStream
// Replace with a proper implementation

// REQUIRED: Your implementation *must* include this function
let cat: (string [] -> int) = catWithBufferSize 32
