namespace FantasySportBetting.Application.Handlers

open System.Threading
open MediatR
open Microsoft.Extensions.Logging

open FantasySportBetting.Application.Commands
open FantasySportBetting.Infrastructure.MongoService.Context
open FantasySportBetting.Infrastructure.MongoService.Repositories
open FantasySportBetting.Infrastructure.MongoService.Documents
open FantasySportBetting.Domain.Models

type GetMatchHandler(logger: ILogger<GetMatchHandler>, context: MongoDbContext) =
    interface IRequestHandler<GetMatchCommand, Match option> with
        member this.Handle(command: GetMatchCommand, cancellationToken: CancellationToken) =
            async {       
                try
                    let! matchOption = MatchRepository.getMatch context command.MatchId |> Async.AwaitTask
                    match matchOption with
                    | Some matchDocument ->
                        let matchModel: Match = {
                            Id = matchDocument.Id.ToString()
                            HomeTeam = matchDocument.HomeTeam
                            GuestTeam = matchDocument.GuestTeam
                            StartTime = matchDocument.StartTime
                            Winner = matchDocument.Winner
                        }
                        return Some matchModel
                    | None -> return None
                with
                | ex ->
                    logger.LogError(ex, "Error retrieving match")
                    return None
            } |> Async.StartAsTask