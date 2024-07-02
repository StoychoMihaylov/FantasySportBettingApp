namespace FantasySportBetting.Application.Queries

open System.Threading
open MediatR
open Microsoft.Extensions.Logging
open FantasySportBetting.Infrastructure.MongoService.Context
open FantasySportBetting.Infrastructure.MongoService.Repositories
open FantasySportBetting.Domain.Models

type GetAllUnplayedMatchesHandler(logger: ILogger<GetAllUnplayedMatchesHandler>, context: MongoDbContext) =
    interface IRequestHandler<GetAllUnplayedMatchesQuery, Match list> with
        member this.Handle(query: GetAllUnplayedMatchesQuery, cancellationToken: CancellationToken) =
            async {       
                try
                    let! matches = MatchRepository.GetUnplayedMatches context |> Async.AwaitTask
                    let matchModels = 
                        matches 
                        |> Seq.map (fun matchDocument ->
                            {
                                Id = matchDocument.Id.ToString()
                                HomeTeam = matchDocument.HomeTeam
                                GuestTeam = matchDocument.GuestTeam
                                StartTime = matchDocument.StartTime
                                WinCoefficient = matchDocument.WinCoefficient
                                Winner = matchDocument.Winner
                            }) |> Seq.toList
                    return matchModels
                with
                | ex ->
                    logger.LogError(ex, "Error retrieving matches")
                    return []
            } |> Async.StartAsTask
