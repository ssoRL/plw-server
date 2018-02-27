namespace PLW.Controllers

open Microsoft.AspNetCore.Mvc
open PLW.Providers.ThreadsProvider

type postData = {
    name: string;
}

[<Route("api/[controller]")>]
type ThreadsController () =
    inherit Controller()

    [<HttpGet("{id}")>]
    member this.Get(id:int) =
        GetThread(id)

    [<HttpPost>]
    member this.Post([<FromBody>]data: postData) =
        CreateThread(Some data.name)