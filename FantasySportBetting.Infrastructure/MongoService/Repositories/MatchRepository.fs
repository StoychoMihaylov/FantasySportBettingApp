namespace FantasySportBetting.Infrastructure.MongoService.Repositories

open System.Threading.Tasks

open FantasySportBetting.Infrastructure.MongoService.Context
open FantasySportBetting.Infrastructure.MongoService.Documents

module MatchRepository =  

    let getCollection (context: MongoDbContext) = 
        context.GetCollection<MatchDocument>(nameof(MatchDocument))

    let addMatch (context: MongoDbContext) (newMatch: MatchDocument) : Task<string> = 
        let collection = getCollection context
        async {
            do! collection.InsertOneAsync(newMatch) |> Async.AwaitTask
            return newMatch.Id.ToString()
        } |> Async.StartAsTask
        

