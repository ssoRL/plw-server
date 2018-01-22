namespace PLW.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open PLW.Models

[<Route("api/[controller]")>]
type PagesController () =
    inherit Controller()

    [<HttpGet>]
    member this.Get() =
        [|
            Page("Party", 3, "Jan 23 4:55PM", "#4286f4")
            Page("Pipe Laying Nation", 5, "#4286f4")
        |]

    [<HttpGet("{id}")>]
    member this.Get(id: int) =
        Page("Party", 3, "Jan 23 4:55PM", "#4286f4")

    [<HttpPost>]
    member this.Post([<FromBody>] name: string, [<FromBody>] date: string, [<FromBody>] color: string) =
        Page(name, 0, date, color)