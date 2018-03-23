namespace PLW

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Diagnostics
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Swashbuckle.AspNetCore
open System.Collections
open Microsoft.AspNetCore.Cors.Infrastructure
open Microsoft.IdentityModel.Tokens;
open System.Text;
open Microsoft.AspNetCore.Authentication.JwtBearer

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
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, fun options ->
            options.TokenValidationParameters = TokenValidationParameters (
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("the secret that needs to be at least 16 characeters long for HmacSha256")), 
                ValidateLifetime = true, //validate the expiration and not before values in the token
                ClockSkew = TimeSpan.FromMinutes(5.0) //5 minute tolerance for the expiration date
            ) |> ignore
        ) |> ignore


    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    member this.Configure(app: IApplicationBuilder, env: IHostingEnvironment) =
        app.UseExceptionHandler(
            fun options ->
                options.Run(
                    fun context ->
                        let ex = context.Features.Get<IExceptionHandlerFeature>()
                        match ex.Error with
                        | HttpCodedException (code, message) ->
                             printfn "code: %i, msg: %s" (int code) message
                             context.Response.StatusCode <- int code
                             context.Response.WriteAsync(message)
                        | exn -> raise (exn)
                )
        ) |> ignore
        app.UseAuthentication() |> ignore 
        let cors = Action<CorsPolicyBuilder> (fun builder -> builder.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod() |> ignore)
        app.UseCors(cors) |> ignore

        app.UseMvc() |> ignore

        app.UseSwagger() |> ignore


    member val Configuration : IConfiguration = null with get, set