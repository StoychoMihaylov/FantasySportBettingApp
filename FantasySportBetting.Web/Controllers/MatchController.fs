namespace FantasySportBetting.Web.Controllers

open System
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open MongoDB.Bson

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
        ActionResult<string[]>(values)

    [<HttpPost>]
    member this.Post(homeTeam: string, guestTeam: string) = 
        async {    
            let matchDocument: MatchDocument = {
                Id = BsonObjectId(ObjectId.GenerateNewId())
                HomeTeam = homeTeam
                GuestTeam = guestTeam
                StartTime = DateTime.Now
                Winner = ""
            }
            let newMatch = MatchRepository.addMatch context matchDocument |> Async.AwaitTask
            return ActionResult<Async<MatchDocument>>(newMatch)
        }
            
