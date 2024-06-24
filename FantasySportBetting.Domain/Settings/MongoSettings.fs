namespace FantasySportBetting.Domain.Settings

type MongoDbOptions() =
    member val DatabaseName = "" with get, set
    member val ConnectionString = "" with get, set
    member val ReadPreferenceMode = "" with get, set