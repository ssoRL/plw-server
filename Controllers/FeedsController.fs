namespace PLW.Controllers

open Microsoft.AspNetCore.Mvc
open PLW.Providers.FeedsProvider

[<Route("api/[controller]")>]
type FeedsController () =
    inherit Controller()

    [<HttpGet>]
    member this.Get() =
        GetFeed(3)