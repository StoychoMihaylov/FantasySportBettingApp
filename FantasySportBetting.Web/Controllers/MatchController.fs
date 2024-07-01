namespace FantasySportBetting.Web.Controllers

open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open System.Threading.Tasks
open MediatR

open FantasySportBetting.Application.Queries
open FantasySportBetting.Application.Commands

[<ApiController>]
[<Route("[controller]")>]
type MatchController (logger: ILogger<MatchController>, mediator: IMediator) =
    inherit ControllerBase()

    [<HttpGet>]
    member this.Get(id: string) : Task<IActionResult> =
        async {
            try
                let command = GetMatchQuery(id)
                let! matchOption = mediator.Send(command) |> Async.AwaitTask
                match matchOption with
                | Some matchResponse -> return this.StatusCode(200, matchResponse) :> IActionResult
                | None -> return this.StatusCode(404) :> IActionResult
            with
            | ex ->
                logger.LogError(ex, "Error getting a match.")
                return this.StatusCode(500, "Internal server error.")
        } |> Async.StartAsTask
        
    [<HttpPost>]
    member this.Post(homeTeam: string, guestTeam: string, winCoefficient: decimal) : Task<IActionResult> = 
        async {    
            try
                let command = AddNewMatchCommand(homeTeam, guestTeam, winCoefficient)
                let! matchId = mediator.Send(command) |> Async.AwaitTask
                return this.StatusCode(200, matchId) :> IActionResult
            with
            | ex ->
                logger.LogError(ex, "Error adding match.")
                return this.StatusCode(500, "Internal server error.") :> IActionResult
        } |> Async.StartAsTask
    
    [<HttpPut>]
    member this.Put() : Task<IActionResult> = 
        async {
            try              
                let command = SetResultToUnplayedMatchesCommand()
                do! mediator.Send(command) |> Async.AwaitTask
                return this.StatusCode(200) :> IActionResult
            with
            | ex ->
                logger.LogError(ex, "Error while playing the matches.")
                return this.StatusCode(500, "Internal server error.")
        } |> Async.StartAsTask

            
