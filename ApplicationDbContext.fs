namespace PLW

open System
open Microsoft.AspNetCore.Identity.EntityFrameworkCore
open Microsoft.EntityFrameworkCore

type ApplicationDbContext (options: DbContextOptions) =
    inherit IdentityDbContext (options)

    new() =
        let optionsBuilder = DbContextOptionsBuilder<ApplicationDbContext>()
        let connection_string = "Host=localhost;Port=5432;Username=postgres;Password=plw;Database=plw"
        optionsBuilder.UseNpgsql(connection_string) |> ignore
        new ApplicationDbContext(optionsBuilder.Options)


    override this.OnConfiguring(optionsBuilder: DbContextOptionsBuilder) =
        let connection_string = "Host=localhost;Port=5432;Username=postgres;Password=plw;Database=plw"
        optionsBuilder.UseNpgsql(connection_string) |> ignore