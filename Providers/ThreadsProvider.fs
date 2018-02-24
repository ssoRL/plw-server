namespace PLW.Providers

open Npgsql
open PLW.Models
open System

module ThreadsProvider =
    let connString = "Host=localhost;Port=5432;Username=postgres;Password=plw;Database=plw";

    let GetThread (threadId:int) =
        use conn = new NpgsqlConnection(connString)
        conn.Open()

        // Get the thread info
        let selectThread = sprintf "SELECT name, last_update FROM thread WHERE id=%i" threadId
        use threadCmd = new NpgsqlCommand(selectThread, conn)
        use threadReader = threadCmd.ExecuteReader()
        printfn "fields: %d" threadReader.FieldCount
        let threadName =
            if threadReader.FieldCount = 1
            then
                threadReader.Read() |> ignore
                threadReader.GetString(threadReader.GetOrdinal("name"))
            else "Hi"

        // Get the messages associated with the thread
        let selectMessages = sprintf "SELECT user_id, message, time FROM message WHERE thread_id=%i" threadId
        use msgCmd = new NpgsqlCommand(selectMessages, conn)
        use msgReader = msgCmd.ExecuteReader()
        printfn "fields: %d" msgReader.FieldCount
        let messages =
            [while msgReader.Read() do
                yield Message(
                    msgReader.GetInt64(msgReader.GetOrdinal("user_id")),
                    msgReader.GetString(msgReader.GetOrdinal("message")),
                    msgReader.GetDateTime(msgReader.GetOrdinal("time"))
                )
        ]

        // Result
        Thread(threadName, messages)


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