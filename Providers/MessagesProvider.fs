namespace PLW.Providers

open Npgsql
open PLW.Models
open System

module MessagesProvider =
    let connString = "Host=localhost;Port=5432;Username=postgres;Password=plw;Database=plw";

    let GetMessagesByThreadId (conn: NpgsqlConnection, threadId: int64) =
        let selectMessages = sprintf "SELECT user_id, message, time FROM message WHERE thread_id=%i" threadId
        use msgCmd = new NpgsqlCommand(selectMessages, conn)
        use msgReader = msgCmd.ExecuteReader()
        printfn "fields: %d" msgReader.FieldCount
        [while msgReader.Read() do
            yield {
                User = msgReader.GetInt64(msgReader.GetOrdinal("user_id"));
                Thread = threadId;
                Content = msgReader.GetString(msgReader.GetOrdinal("message"));
                Time = msgReader.GetDateTime(msgReader.GetOrdinal("time"));
            }
        ]

    let CreateMessage(threadId: int64, userId: int64, content: string) =
        use conn = new NpgsqlConnection(connString)
        conn.Open()

        let now = DateTime.Now.ToString("u")
        let createCmdString =
            sprintf
                "INSERT INTO message(thread_id, user_id, message, time) VALUES (%i, %i, '%s', '%s') RETURNING id"
                threadId userId content now

        let createCmd = new NpgsqlCommand(createCmdString, conn)
        createCmd.ExecuteScalar() :?> Int64