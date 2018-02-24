namespace PLW.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc

[<Route("api/[controller]")>]
type ThreadsController () =
    inherit Controller()

    [<HttpGet("{id}")>]
    member this.Get(id:int) =
        "value"

    [<HttpPost>]
    member this.Post([<FromBody>]value:string) =
        ()