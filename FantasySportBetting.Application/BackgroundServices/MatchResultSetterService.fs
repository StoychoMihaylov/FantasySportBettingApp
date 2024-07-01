namespace FantasySportBetting.Application.BackgroundServices

open System
open System.Threading
open System.Threading.Tasks
open Microsoft.Extensions.Hosting
open MediatR
open Microsoft.Extensions.Logging

open FantasySportBetting.Application.Commands

type MatchResultSetterService(logger: ILogger<MatchResultSetterService>, mediator: IMediator) =
    inherit BackgroundService()

    override _.ExecuteAsync(cancellationToken: CancellationToken) =
        let rec loop () =
            async {
                while not cancellationToken.IsCancellationRequested do
                    try
                        do! Task.Delay(TimeSpan.FromMinutes(1.0), cancellationToken) |> Async.AwaitTask                  
                        let command = SetResultToUnplayedMatchesCommand()
                        do! mediator.Send(command) |> Async.AwaitTask
                    with
                    | ex ->
                        logger.LogError(ex, "Error while the background service playing the matches.")
                return! loop ()
            }
        loop () |> Async.StartAsTask :> Task
