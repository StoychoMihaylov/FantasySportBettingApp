namespace FantasySportBetting.Infrastructure.MongoService.Repositories

open MongoDB.Driver
open System.Threading.Tasks
open MongoDB.Bson

open FantasySportBetting.Infrastructure.MongoService.Context
open FantasySportBetting.Infrastructure.MongoService.Documents

module BetRepository =

    let getCollection (context: MongoDbContext) = 
        context.GetCollection<BetDocument>(nameof(BetDocument))

    let getBet (context: MongoDbContext) (userId: string) : Task<Option<BetDocument>> = 
        let collection = getCollection context
        async {
            let! cursor = collection.FindAsync(fun doc -> doc.Id =  BsonObjectId(new ObjectId(userId))) |> Async.AwaitTask
            let! betList = cursor.ToListAsync() |> Async.AwaitTask
            return if betList.Count > 0 then Some(betList.[0]) else None
        } |> Async.StartAsTask

    let addBet (context: MongoDbContext) (newBet: BetDocument) : Task<string> = 
        let collection = getCollection context
        async {
            do! collection.InsertOneAsync(newBet) |> Async.AwaitTask
            return newBet.Id.ToString()
        } |> Async.StartAsTask