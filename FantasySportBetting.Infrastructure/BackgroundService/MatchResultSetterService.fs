namespace FantasySportBetting.Infrastructure.BackgroundService

open System
open System.Threading
open System.Threading.Tasks
open Microsoft.Extensions.Hosting
open MediatR
open FSharp.Control.Tasks
open FantasySportBetting.Application.Commands

type MatchResultBackgroundService(mediator: IMediator) =
    inherit BackgroundService()

    override _.ExecuteAsync(cancellationToken: CancellationToken) =
        let rec loop () =
            task {
                do! Task.Delay(TimeSpan.FromMinutes(10.0), cancellationToken) |> Async.AwaitTask

                // Logic to randomly set match results
                let matchId = Guid.NewGuid() // Replace with actual logic
                let result = if DateTime.UtcNow.Second % 2 = 0 then "win" else "loss"
                let command = SetMatchResultCommand(matchId, result)
                do! mediator.Send(command, cancellationToken) |> Async.AwaitTask

                return! loop ()
            }
        loop () :> Task
