namespace FantasySportBetting.Infrastructure.MongoService.Documents

open MongoDB.Bson
open MongoDB.Bson.Serialization.Attributes

type UserDocument =
    {
        [<BsonId>]
        Id: BsonObjectId

        [<BsonElement("Name")>]
        Name: string

        [<BsonElement("Balance")>]
        Balance: decimal
    }