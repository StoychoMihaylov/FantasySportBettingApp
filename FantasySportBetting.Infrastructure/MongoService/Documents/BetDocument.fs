namespace FantasySportBetting.Infrastructure.MongoService.Documents

open System
open MongoDB.Bson
open MongoDB.Bson.Serialization.Attributes

type BetDocument =
    {
        [<BsonId>]
        Id: BsonObjectId

        [<BsonElement("UserId")>]
        UserId: BsonObjectId

        [<BsonElement("MatchId")>]
        MatchId: BsonObjectId

        [<BsonElement("Amount")>]
        Amount: decimal

        [<BsonElement("PlacedAt")>]
        PlacedAt: DateTime
        
        [<BsonElement("IsProcessed")>]
        IsProcessed: bool
    }