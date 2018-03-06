namespace PLW.Controllers

open Microsoft.AspNetCore.Mvc
open PLW.Providers.ThreadsProvider
open PLW.Models

[<Route("api/[controller]")>]
type ThreadsController () =
    inherit Controller()

    [<HttpGet("{id}")>]
    member this.Get(id:int64) = 
        GetThread(id)

    [<HttpPost>]
    member this.Post([<FromBody>]data: ThreadPost) = 
        CreateThread(Some data.name)