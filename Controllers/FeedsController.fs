namespace PLW.Controllers

open Microsoft.AspNetCore.Authorization
open Microsoft.AspNetCore.Mvc
open PLW.Providers.FeedsProvider
open Microsoft.AspNetCore.Authentication.JwtBearer

[<Route("api/[controller]")>]
type FeedsController () =
    inherit Controller()

    [<HttpGet; Authorize>]
    member this.Get() =
        GetFeed(3)