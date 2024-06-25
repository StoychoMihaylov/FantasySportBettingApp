namespace FantasySportBetting.Infrastructure.MongoService.Documents

open System
open MongoDB.Bson
open MongoDB.Bson.Serialization.Attributes

type MatchDocument =
    {
        [<BsonId>]
        Id: BsonObjectId

        [<BsonElement("home-team")>]
        HomeTeam: string

        [<BsonElement("guest-team")>]
        GuestTeam: string

        [<BsonElement("start-time")>]
        StartTime: DateTime

        [<BsonElement("winner")>]
        Winner: string
    }
