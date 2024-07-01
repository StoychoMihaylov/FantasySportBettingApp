namespace FantasySportBetting.Application.Queries

open System.Threading
open MediatR
open Microsoft.Extensions.Logging

open FantasySportBetting.Application.Queries
open FantasySportBetting.Infrastructure.MongoService.Context
open FantasySportBetting.Infrastructure.MongoService.Repositories
open FantasySportBetting.Infrastructure.MongoService.Documents
open FantasySportBetting.Domain.Models

type GetMatchHandler(logger: ILogger<GetMatchHandler>, context: MongoDbContext) =
    interface IRequestHandler<GetMatchQuery, Match option> with
        member this.Handle(command: GetMatchQuery, cancellationToken: CancellationToken) =
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
                            WinCoefficient = matchDocument.WinCoefficient
                            Winner = matchDocument.Winner
                        }
                        return Some matchModel
                    | None -> return None
                with
                | ex ->
                    logger.LogError(ex, "Error retrieving match")
                    return None
            } |> Async.StartAsTask