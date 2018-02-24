namespace PLW.Controllers

open Microsoft.AspNetCore.Mvc
open PLW.Providers

[<Route("api/[controller]")>]
type FeedsController () =
    inherit Controller()

    [<HttpGet>]
    member this.Get() =
        Feed.GetFeed(3)