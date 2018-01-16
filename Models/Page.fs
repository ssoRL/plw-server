namespace PLW.Models

// type Page =
//     | Event of string * int * string
//     | MessageGroup of string * int

open Microsoft.CodeAnalysis
type Page(name: string, notifications: int, date: string, color: string) =
    new (name: string, notifications: int, color: string) =
        Page (name, notifications, null, color)
    member this.Name = name
    member this.Notifications = notifications
    member this.Date = date
    member this.Color = color