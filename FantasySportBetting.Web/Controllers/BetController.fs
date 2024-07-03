namespace FantasySportBetting.Web.Controllers

open System
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open System.Threading.Tasks
open MediatR

open FantasySportBetting.Application.Commands
open FantasySportBetting.Application.Queries

[<ApiController>]
[<Route("[controller]")>]
type BetController (logger: ILogger<BetController>, mediator: IMediator) =
    inherit ControllerBase()

    [<HttpPost>]
    member this.Post(userId: string, matchId: string, winnerTeam: string, amount: decimal) : Task<IActionResult> = 
        async {    
            try
                let command = AddNewBetCommand(userId, matchId, winnerTeam, amount)
                let! betId = mediator.Send(command) |> Async.AwaitTask
                return this.StatusCode(200, betId) :> IActionResult
            with
            | ex ->
                match ex.InnerException with
                | :? ArgumentException -> return this.StatusCode(400, ex.InnerException.Message) :> IActionResult
                | _-> 
                    logger.LogError(ex, "Error adding bet.") 
                    return this.StatusCode(500, "Internal server error.") :> IActionResult
        } |> Async.StartAsTask

    [<HttpGet>]
    member this.Get(id: string) : Task<IActionResult> =
        async {
            try
                let query = GetBetQuery(id)
                let! betOption = mediator.Send(query) |> Async.AwaitTask
                match betOption with
                | Some betResponse -> return this.StatusCode(200, betResponse) :> IActionResult
                | None -> return this.StatusCode(404) :> IActionResult
            with
            | ex ->
                logger.LogError(ex, "Error getting a bet.")
                return this.StatusCode(500, "Internal server error.")
        } |> Async.StartAsTask

    [<HttpPut>]
    member this.Put() : Task<IActionResult> =
        async {
            try              
                let command = ProcessBetsCommand()
                do! mediator.Send(command) |> Async.AwaitTask
                return this.StatusCode(200) :> IActionResult
            with
            | ex ->
                logger.LogError(ex, "Error while processing the bets.")
                return this.StatusCode(500, "Internal server error.")
        } |> Async.StartAsTask
