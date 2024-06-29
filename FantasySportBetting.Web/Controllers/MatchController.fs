namespace FantasySportBetting.Web.Controllers

open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open System.Threading.Tasks
open MediatR

open FantasySportBetting.Application.Commands

[<ApiController>]
[<Route("[controller]")>]
type MatchController (logger: ILogger<MatchController>, mediator: IMediator) =
    inherit ControllerBase()

    [<HttpGet>]
    member this.Get(id: string) : Task<IActionResult> =
        async {
            let command = GetMatchCommand(id)
            let! matchOption = mediator.Send(command) |> Async.AwaitTask
            match matchOption with
            | Some matchDoc -> return this.StatusCode(200, matchDoc) :> IActionResult
            | None -> return this.StatusCode(404) :> IActionResult
        } |> Async.StartAsTask
        

    [<HttpPost>]
    member this.Post(homeTeam: string, guestTeam: string) : Task<IActionResult> = 
        async {    
            try
                let command = AddNewMatchCommand(homeTeam, guestTeam)
                let! matchId = mediator.Send(command) |> Async.AwaitTask
                return this.StatusCode(200, matchId) :> IActionResult
            with
            | ex ->
                logger.LogError(ex, "Error adding match")
                return this.StatusCode(500, "Internal server error.") :> IActionResult
        } |> Async.StartAsTask
            
