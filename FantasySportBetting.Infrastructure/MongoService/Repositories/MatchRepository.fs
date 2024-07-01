namespace FantasySportBetting.Infrastructure.MongoService.Repositories

open MongoDB.Driver
open System.Threading.Tasks
open MongoDB.Bson
open System.Collections.Generic

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

    let getMatch (context: MongoDbContext) (matchId: string) : Task<Option<MatchDocument>> = 
        let collection = getCollection context
        async {
            let! cursor = collection.FindAsync(fun doc -> doc.Id =  BsonObjectId(new ObjectId(matchId)) ) |> Async.AwaitTask
            let! matchList = cursor.ToListAsync() |> Async.AwaitTask
            return if matchList.Count > 0 then Some(matchList.[0]) else None
        } |> Async.StartAsTask

    let GetUnplayedMatches (context: MongoDbContext) : Task<List<MatchDocument>> =
        let collection = getCollection context
        async {
            let! cursor = collection.FindAsync(fun doc -> doc.Winner.Equals("") ) |> Async.AwaitTask
            let! matches = cursor.ToListAsync() |> Async.AwaitTask
            return matches
        } |> Async.StartAsTask

    let UpdateMatch (context: MongoDbContext) (matchDoc: MatchDocument) =
        let collection = getCollection context
        async {
            let filter = Builders<MatchDocument>.Filter.Eq((fun doc -> doc.Id), matchDoc.Id)
            let update = Builders<MatchDocument>.Update.Set((fun doc -> doc.Winner), matchDoc.Winner)
            do! collection.UpdateOneAsync(filter, update) |> Async.AwaitTask |> Async.Ignore
        } |> Async.StartAsTask