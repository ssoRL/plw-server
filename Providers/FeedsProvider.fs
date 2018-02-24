namespace PLW.Providers

open Npgsql
open PLW.Models

module FeedsProvider =
    let connString = "Host=localhost;Port=5432;Username=postgres;Password=plw;Database=plw";

    let GetFeed (_userId:int) =
        use conn = new NpgsqlConnection(connString)
        conn.Open()
        use cmd = new NpgsqlCommand("SELECT name, last_update FROM thread", conn)
        use reader = cmd.ExecuteReader()
        printfn "fields: %d" reader.FieldCount
        [while reader.Read() do
                yield FeedItem(reader.GetString(reader.GetOrdinal("name")), 1)
        ]