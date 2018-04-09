namespace PLW.Controllers

open PLW
open System.Net
open Microsoft.AspNetCore.Mvc
open PLW.Providers.AuthProvider
open PLW.Models
open Microsoft.AspNetCore.Identity

[<Route("api/[controller]")>]
type AuthController (userManager: UserManager<IdentityUser>, signInManager:SignInManager<IdentityUser>) =
    inherit Controller()

    [<HttpPost>]
    member this.Post([<FromBody>]data: StartSessionParameters) = 
        if IsValidUsernameAndPassword(data.Username, data.Password) then
            GenerateToken(data.Username)
        else
           raise (HttpCodedException (HttpStatusCode.NotFound, "message"))