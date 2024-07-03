namespace FantasySportBetting.Infrastructure.MongoService.Repositories

open MongoDB.Driver
open System.Threading.Tasks
open MongoDB.Bson
open System.Collections.Generic

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

    let getUnprocessedBets (context: MongoDbContext) : Task<List<BetDocument>> =
        let collection = getCollection context
        async {
            let! cursor = collection.FindAsync(fun doc -> doc.IsProcessed = false ) |> Async.AwaitTask
            let! matches = cursor.ToListAsync() |> Async.AwaitTask
            return matches
        } |> Async.StartAsTask 

    let updateBet (context: MongoDbContext) (betDoc: BetDocument) =
        let collection = getCollection context
        async {
            let filter = Builders<BetDocument>.Filter.Eq((fun doc -> doc.Id), betDoc.Id)
            let update = Builders<BetDocument>.Update.Set((fun doc -> doc.IsProcessed), betDoc.IsProcessed)
            do! collection.UpdateOneAsync(filter, update) |> Async.AwaitTask |> Async.Ignore
        } |> Async.StartAsTask