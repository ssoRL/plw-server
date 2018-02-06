namespace PLW

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Swashbuckle.AspNetCore
open System.Collections
open Microsoft.AspNetCore.Cors.Infrastructure

type Startup private () =
    new (configuration: IConfiguration) as this =
        Startup() then
        this.Configuration <- configuration

    // This method gets called by the runtime. Use this method to add services to the container.
    member this.ConfigureServices(services: IServiceCollection) =
        // Add framework services.
        services.AddCors() |> ignore
        services.AddMvc() |> ignore
        services.AddSwaggerGen (fun c -> c.SwaggerDoc("v1", Swagger.Info()))  |> ignore
        //(c => c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" })) 


    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    member this.Configure(app: IApplicationBuilder, env: IHostingEnvironment) = 
        let cors = Action<CorsPolicyBuilder> (fun builder -> builder.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod() |> ignore)
        app.UseCors(cors) |> ignore

        app.UseMvc() |> ignore

        app.UseSwagger() |> ignore


    member val Configuration : IConfiguration = null with get, set