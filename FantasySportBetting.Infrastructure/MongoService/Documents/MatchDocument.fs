namespace FantasySportBetting.Infrastructure.MongoService.Documents

open System
open MongoDB.Bson
open MongoDB.Bson.Serialization.Attributes

type MatchDocument =
    {
        [<BsonId>]
        Id: BsonObjectId

        [<BsonElement("HomeTeam")>]
        HomeTeam: string

        [<BsonElement("GuestTeam")>]
        GuestTeam: string

        [<BsonElement("StartTime")>]
        StartTime: DateTime

        [<BsonElement("WinCoefficient")>]
        WinCoefficient: Decimal

        [<BsonElement("Winner")>]
        Winner: string
    }
