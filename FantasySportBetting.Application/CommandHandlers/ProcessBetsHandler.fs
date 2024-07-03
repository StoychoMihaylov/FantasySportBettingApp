namespace FantasySportBetting.Application.Handlers

open System.Threading
open MediatR

open FantasySportBetting.Application.Commands
open FantasySportBetting.Infrastructure.MongoService.Context
open FantasySportBetting.Infrastructure.MongoService.Repositories
open FantasySportBetting.Infrastructure.MongoService.Documents

type ProcessBetsHandler(context: MongoDbContext) =
    interface IRequestHandler<ProcessBetsCommand, unit> with
        member this.Handle(command: ProcessBetsCommand, cancellationToken: CancellationToken) =
            async {                    
                let! unprocessedBets = BetRepository.getUnprocessedBets context |> Async.AwaitTask
                for betDoc in unprocessedBets do
                    let! matchResult = MatchRepository.getMatch context (betDoc.MatchId.ToString()) |> Async.AwaitTask
                    if matchResult.Value.Winner <> "" && matchResult.Value.Winner = betDoc.WinnerTeam then
                        let! userResult = UserRepository.getUser context (betDoc.UserId.ToString()) |> Async.AwaitTask
                        UserRepository.updateUserBalance context (userResult.Value, betDoc.Amount + userResult.Value.Balance + (matchResult.Value.WinCoefficient * betDoc.Amount)) |> Async.AwaitTask |> ignore
                        let updatedBet = { betDoc with IsProcessed = true }
                        do! BetRepository.updateBet context updatedBet |> Async.AwaitTask |> Async.Ignore             
                return()
            } |> Async.StartAsTask
