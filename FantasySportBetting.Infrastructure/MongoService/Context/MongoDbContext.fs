namespace FantasySportBetting.Infrastructure.MongoService.Context

open MongoDB.Driver
open Microsoft.Extensions.Options

open FantasySportBetting.Domain.Settings

type MongoDbContext(options: IOptions<MongoDbOptions>) =
    let client = MongoClient(options.Value.ConnectionString)
    let database = client.GetDatabase(options.Value.DatabaseName)

    member _.GetCollection<'T>(name: string) =
        database.GetCollection<'T>(name)
