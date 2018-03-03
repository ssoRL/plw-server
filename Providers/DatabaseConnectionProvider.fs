namespace PLW.Providers

open FSharp.Data.Sql


module DataBase =

    type sql = SqlDataProvider< 
        DatabaseVendor = Common.DatabaseProviderTypes.POSTGRESQL,
        ConnectionString = "Host=localhost;Port=5432;Username=postgres;Password=plw;Database=plw",
        ConnectionStringName = "",
        IndividualsAmount = 1000,
        UseOptionTypes = true >

    //let getConnection() = 
        //sql.GetDataContext()