namespace FantasySportBetting.Infrastructure

open MongoDB.Driver

type MongoService(connectionString: string) =
    let client = MongoClient(connectionString)
    let database = client.GetDatabase("FantasySportBetting")

    member _.GetCollection<'T>(name: string) =
        database.GetCollection<'T>(name)
