namespace FantasySportBetting.Application.Queries

open System.Threading
open MediatR
open Microsoft.Extensions.Logging

open FantasySportBetting.Application.Queries
open FantasySportBetting.Infrastructure.MongoService.Context
open FantasySportBetting.Infrastructure.MongoService.Repositories
open FantasySportBetting.Domain.Models

type GetBetHandler(logger: ILogger<GetBetHandler>, context: MongoDbContext) =
    interface IRequestHandler<GetBetQuery, Bet option> with
        member this.Handle(command: GetBetQuery, cancellationToken: CancellationToken) =
            async {       
                try
                    let! betOption = BetRepository.getBet context command.BetId |> Async.AwaitTask
                    match betOption with
                    | Some betDocument ->
                        let betModel: Bet = {
                            Id = betDocument.Id.ToString()
                            UserId = betDocument.UserId.ToString()
                            MatchId = betDocument.MatchId.ToString()
                            Amount = betDocument.Amount
                            PlacedAt = betDocument.PlacedAt
                            IsProcessed = betDocument.IsProcessed
                        }
                        return Some betModel
                    | None -> return None
                with
                | ex ->
                    logger.LogError(ex, "Error retrieving bet")
                    return None
            } |> Async.StartAsTask