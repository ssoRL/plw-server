namespace PLW.Controllers

open Microsoft.AspNetCore.Mvc
open PLW.Providers.MessagesProvider
open PLW.Models

[<Route("api/[controller]")>]
type MessagesController () =
    inherit Controller()

    [<HttpPost>]
    member this.Post([<FromBody>]data: MessagePost) = 
        CreateMessage(data.ThreadId, data.UserId, data.Content)