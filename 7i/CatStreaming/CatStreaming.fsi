module CatStreaming

open System.IO
// REQUIRED
/// <summary>cat files to output file with buffersize </summary>
/// <param name="buffersize">size of buffer</param>
/// <param name="filesnames">list of files to cat (last in list is output file)</param>
/// <returns>return int</returns>
val catWithBufferSize: int -> string [] -> int
// REQUIRED
/// <summary>cat with 32 byte buffer</summary>
/// <returns>return int</returns>
val cat: (string [] -> int)

// RECOMMENDED functions
/// <summary>Read bytes from filestream</summary>
/// <param name="count">buffersize</param>
/// <param name="buffer">buffer</param>
/// <param name="filestream">filestream</param>
/// <returns>number of bytes read</returns>
val readBytes: int -> byte [] -> FileStream -> int

/// <summary>Write bytes to filestream</summary>
/// <param name="count">buffersize</param>
/// <param name="buffer">buffer</param>
/// <param name="filestream">filestream</param>
/// <returns></returns>
val writeBytes: int -> byte [] -> FileStream -> unit

/// <summary>Read and writes to corresponding filestreams</summary>
/// <param name="count">buffersize</param>
/// <param name="buffer">buffer</param>
/// <param name="ifs">stream to read from</param>
/// <param name="ofs">stream to write to</param>
/// <returns>number of bytes read</returns>
val readAndWriteBytes: int -> byte [] -> FileStream -> FileStream -> int

/// <summary>Open a file to read from</summary>
/// <param name="name">name of file</param>
/// <returns>FileStream of file</returns>
val openFileRead: string -> FileStream

/// <summary>Open files to read from</summary>
/// <param name="names">names of files</param>
/// <returns>FileStream options of files</returns>
val openFilesRead: string list -> FileStream option list

/// <summary>Open a file to write to</summary>
/// <param name="name">name of file</param>
/// <returns>FileStream option of file</returns>
val openFileWrite: string -> FileStream option
