namespace PLW.Providers

open Npgsql
open PLW.Models

module Feed =
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
        
        
        
        // new seq<string>()
        // while result.Read() do
        //     results.Add()
            


// using (var conn = new NpgsqlConnection(connString))
// {
//     conn.Open();

//     // Insert some data
//     using (var cmd = new NpgsqlCommand())
//     {
//         cmd.Connection = conn;
//         cmd.CommandText = "INSERT INTO data (some_field) VALUES (@p)";
//         cmd.Parameters.AddWithValue("p", "Hello world");
//         cmd.ExecuteNonQuery();
//     }

//     // Retrieve all rows
//     using (var cmd = new NpgsqlCommand("SELECT some_field FROM data", conn))
//     using (var reader = cmd.ExecuteReader())
//         while (reader.Read())
//             Console.WriteLine(reader.GetString(0))