module CatStreaming

open System.IO



let readBytes (count: int) (buffer: byte []) (fs: FileStream) : int =
    let readBytes = fs.Read(buffer, 0, count)
    readBytes


let writeBytes (count: int) (buffer: byte []) (fs: FileStream) : unit =
    printfn "Writing %d bytes to filestream" (Array.length buffer)
    printfn "CanWrite: %A" fs.CanWrite
    fs.Write buffer
    fs.Flush()

let readAndWriteBytes (buffersize: int) (buffer: byte []) (ifs: FileStream) (ofs: FileStream) : int =

    let bufferr = (Array.init (buffersize) (fun _ -> 0uy))
    let out = readBytes buffersize bufferr ifs
    writeBytes buffersize bufferr ofs
    out

let openFileRead (filename: string) : FileStream =
    // Replace with a proper implementation
    try
        File.OpenRead filename
    with
    | _ -> raise (FileNotFoundException())


let openFilesRead (filenames: string list) : FileStream option list =
    List.map
        (fun name ->
            try
                openFileRead name |> Some
            with
            | _ -> None)
        filenames


let openFileWrite (filename: string) : FileStream option =
    try
        // File.Open(filename, FileMode.Create)
        File.OpenWrite filename |> Some

    with
    | _ ->
        sprintf "cat: Could not open or create file \"%s\"" filename
        |> System.Console.Error.WriteLine

        None


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


            List.iter
                (fun (stream: FileStream option) ->
                    let mutable ll = int (stream.Value.Length)

                    while ll > buffersize do
                        ll <-
                            ll
                            - readAndWriteBytes buffersize [||] stream.Value outstream.Value

                    readAndWriteBytes ll [||] stream.Value outstream.Value
                    |> ignore)
                streams
            |> ignore


            0

let cat: (string [] -> int) = catWithBufferSize 32
