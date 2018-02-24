namespace PLW.Controllers

open Microsoft.AspNetCore.Mvc
open PLW.Providers.ThreadsProvider

[<Route("api/[controller]")>]
type ThreadsController () =
    inherit Controller()

    [<HttpGet("{id}")>]
    member this.Get(id:int) =
        GetThread(id)

    [<HttpPost>]
    member this.Post([<FromBody>]name:string) =
        CreateThread(Some name)