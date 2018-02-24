namespace PLW.Models

// type Page =
//     | Event of string * int * string
//     | MessageGroup of string * int

open Microsoft.CodeAnalysis

type FeedItem(name: string, notifications: int) =
    member this.Name = name
    member this.Notifications = notifications