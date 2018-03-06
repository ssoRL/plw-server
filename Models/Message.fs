namespace PLW.Models

open System

type Message = {
    User: int64
    Thread: int64
    Content: string
    Time: DateTime
}

type MessagePost = {
    UserId: int64
    ThreadId: int64
    Content: string
}