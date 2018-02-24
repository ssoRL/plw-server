namespace PLW.Models

open System

type Message(user: int64, content: string, time: DateTime) =
    member this.UserId = user
    member this.Content = content

    member this.PostTime = time