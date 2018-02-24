namespace PLW.Models

type Thread(name: string, messages: Message list) =
    member this.Name = name
    member this.Messages = messages