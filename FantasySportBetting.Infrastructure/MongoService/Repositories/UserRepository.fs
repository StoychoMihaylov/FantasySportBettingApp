namespace FantasySportBetting.Infrastructure.MongoService.Repositories

open MongoDB.Driver
open System.Threading.Tasks
open MongoDB.Bson

open FantasySportBetting.Infrastructure.MongoService.Context
open FantasySportBetting.Infrastructure.MongoService.Documents

module UserRepository =

    let getCollection (context: MongoDbContext) = 
        context.GetCollection<UserDocument>(nameof(UserDocument))

    let getUser (context: MongoDbContext) (userId: string) : Task<Option<UserDocument>> = 
        let collection = getCollection context
        async {
            let! cursor = collection.FindAsync(fun doc -> doc.Id =  BsonObjectId(new ObjectId(userId))) |> Async.AwaitTask
            let! userList = cursor.ToListAsync() |> Async.AwaitTask
            return if userList.Count > 0 then Some(userList.[0]) else None
        } |> Async.StartAsTask

    let addUser (context: MongoDbContext) (newUser: UserDocument) : Task<string> = 
        let collection = getCollection context
        async {
            do! collection.InsertOneAsync(newUser) |> Async.AwaitTask
            return newUser.Id.ToString()
        } |> Async.StartAsTask

    let updateUserBalance (context: MongoDbContext) (user: UserDocument, newBalance: decimal) = 
        let collection = getCollection context
        async {
            let filter = Builders<UserDocument>.Filter.Eq((fun doc -> doc.Id), user.Id)
            let update = Builders<UserDocument>.Update.Set((fun doc -> doc.Balance), newBalance)
            do! collection.UpdateOneAsync(filter, update) |> Async.AwaitTask |> Async.Ignore
        } |> Async.StartAsTask