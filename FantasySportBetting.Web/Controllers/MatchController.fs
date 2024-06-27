namespace FantasySportBetting.Web.Controllers

open System
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open MongoDB.Bson
open System.Threading.Tasks

open FantasySportBetting.Infrastructure.MongoService.Context
open FantasySportBetting.Infrastructure.MongoService.Repositories
open FantasySportBetting.Infrastructure.MongoService.Documents

[<ApiController>]
[<Route("[controller]")>]
type MatchController (logger: ILogger<MatchController>, context: MongoDbContext) =
    inherit ControllerBase()

    [<HttpGet>]
    member this.Get(id: string) =
        let values = [|"Hello"; "World"; "First F#/ASP.NET Core web API!"|]
        this.StatusCode(200, values)

    [<HttpPost>]
    member this.Post(homeTeam: string, guestTeam: string) : Task<IActionResult> = 
        async {    
            try
                let matchDocument: MatchDocument = {
                    Id = BsonObjectId(ObjectId.GenerateNewId())
                    HomeTeam = homeTeam
                    GuestTeam = guestTeam
                    StartTime = DateTime.Now
                    Winner = ""
                }
                let! matchId = MatchRepository.addMatch context matchDocument |> Async.AwaitTask
                logger.LogInformation("New match added with Id: {matchId}", matchId)
                return this.StatusCode(200, matchId) :> IActionResult
            with
            | ex ->
                logger.LogError(ex, "Error adding match")
                return this.StatusCode(500, "Internal server error.") :> IActionResult
        } |> Async.StartAsTask
            
