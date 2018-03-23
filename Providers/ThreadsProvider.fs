namespace PLW.Providers

open Npgsql
open PLW
open PLW.Models
open System
open System.Net
open PLW.Providers.MessagesProvider

module ThreadsProvider =
    let connString = "Host=localhost;Port=5432;Username=postgres;Password=plw;Database=plw";

    let private GetThreadInfo (conn: NpgsqlConnection, threadId: int64) =
        let selectThread = sprintf "SELECT name, last_update FROM thread WHERE id=%i" threadId
        use threadCmd = new NpgsqlCommand(selectThread, conn)
        use threadReader = threadCmd.ExecuteReader()
        printfn "fields: %d" threadReader.FieldCount
        if threadReader.FieldCount > 0
        then
            threadReader.Read() |> ignore
            (
                threadReader.GetString(threadReader.GetOrdinal("name")),
                threadReader.GetDateTime(threadReader.GetOrdinal("last_update"))
            )
        else 
            let message = sprintf "Could not find a thread with id=%i" threadId
            raise (HttpCodedException (HttpStatusCode.NotFound, message))

    let GetThread (threadId:int64) =
        use conn = new NpgsqlConnection(connString)
        conn.Open()

        let (threadName, lastUpdate) = GetThreadInfo(conn, threadId)
        let messages = GetMessagesByThreadId(conn, threadId)
        let thread = {
            Id = threadId;
            Name = threadName;
            LastUpdate = lastUpdate;
            Messages = messages;
        }
        thread


    let CreateThread (name: string option) =
        use conn = new NpgsqlConnection(connString)
        conn.Open()

        let now = DateTime.Now.ToString("u")
        let createCmdString =
            match name with
            | Some threadName ->
                sprintf 
                    "INSERT INTO thread(name, last_update) VALUES ('%s', '%s') RETURNING id"
                    threadName now
            | None -> sprintf "INSERT INTO thread(name, last_update) VALUES ('New Thread', '%s') RETURNING id" now
        let createCmd = new NpgsqlCommand(createCmdString, conn)
        createCmd.ExecuteScalar() :?> Int64