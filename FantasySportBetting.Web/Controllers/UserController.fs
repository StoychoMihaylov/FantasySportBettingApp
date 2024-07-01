namespace FantasySportBetting.Web.Controllers

open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open System.Threading.Tasks
open MediatR

open FantasySportBetting.Application.Commands
open FantasySportBetting.Application.Queries

[<ApiController>]
[<Route("[controller]")>]
type UserController (logger: ILogger<UserController>, mediator: IMediator) =
    inherit ControllerBase()

    [<HttpPost>]
    member this.Post(name: string, balance: decimal) : Task<IActionResult> = 
        async {    
            try
                let command = AddNewUserCommand(name, balance)
                let! userId = mediator.Send(command) |> Async.AwaitTask
                return this.StatusCode(200, userId) :> IActionResult
            with
            | ex ->
                logger.LogError(ex, "Error adding user.")
                return this.StatusCode(500, "Internal server error.") :> IActionResult
        } |> Async.StartAsTask

    [<HttpGet>]
    member this.Get(id: string) : Task<IActionResult> =
        async {
            try
                let command = GetUserQuery(id)
                let! userOption = mediator.Send(command) |> Async.AwaitTask
                match userOption with
                | Some userResponse -> return this.StatusCode(200, userResponse) :> IActionResult
                | None -> return this.StatusCode(404) :> IActionResult
            with
            | ex ->
                logger.LogError(ex, "Error getting a user.")
                return this.StatusCode(500, "Internal server error.")
        } |> Async.StartAsTask

