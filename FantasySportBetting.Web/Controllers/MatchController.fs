namespace FantasySportBetting.Web.Controllers

open System
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open MongoDB.Bson
open System.Threading.Tasks
open MediatR

open FantasySportBetting.Infrastructure.MongoService.Documents
open FantasySportBetting.Application.Commands

[<ApiController>]
[<Route("[controller]")>]
type MatchController (logger: ILogger<MatchController>, mediator: IMediator) =
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

                let command = AddNewMatchCommand(homeTeam, guestTeam)
                let! matchId = mediator.Send(command) |> Async.AwaitTask

                return this.StatusCode(200, matchId) :> IActionResult
            with
            | ex ->
                logger.LogError(ex, "Error adding match")
                return this.StatusCode(500, "Internal server error.") :> IActionResult
        } |> Async.StartAsTask
            
