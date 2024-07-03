namespace FantasySportBetting.Application.Handlers

open System
open System.Threading
open MediatR
open MongoDB.Bson
open Microsoft.Extensions.Logging

open FantasySportBetting.Application.Commands
open FantasySportBetting.Infrastructure.MongoService.Context
open FantasySportBetting.Infrastructure.MongoService.Repositories
open FantasySportBetting.Infrastructure.MongoService.Documents

type AddNewBetHandler(logger: ILogger<AddNewBetHandler>, context: MongoDbContext) =
    interface IRequestHandler<AddNewBetCommand, string> with
        member this.Handle(command: AddNewBetCommand, cancellationToken: CancellationToken) =
            async {
                let! userResult = UserRepository.getUser context command.UserId |> Async.AwaitTask
                match userResult with
                | None -> 
                    raise (ArgumentException("User not found error."))
                | Some user ->
                    if user.Balance < command.Amount then 
                        raise (ArgumentException("User balance insufficient."))

                let! matchResult = MatchRepository.getMatch context command.MatchId |> Async.AwaitTask
                match matchResult with
                | None -> 
                    raise (ArgumentException("Match not found error."))
                | Some matchDoc ->
                    if matchDoc.Winner.Length > 0 then 
                        raise (ArgumentException("Match already played."))

                    if matchResult.Value.HomeTeam <> command.WinnerTeam && matchResult.Value.GuestTeam <> command.WinnerTeam 
                    then raise (ArgumentException("The given winner team not found for that match."))
        
                let betDocument: BetDocument = {
                    Id = BsonObjectId(ObjectId.GenerateNewId())
                    UserId = userResult.Value.Id
                    MatchId = matchResult.Value.Id
                    WinnerTeam = command.WinnerTeam
                    Amount = command.Amount
                    PlacedAt = DateTime.Now
                    IsProcessed = false
                }

                let! betId = BetRepository.addBet context betDocument |> Async.AwaitTask
                logger.LogInformation("New user added with Id: {betId}", betId)

                let newBalance = userResult.Value.Balance - command.Amount
                do! UserRepository.updateUserBalance context (userResult.Value, newBalance) |> Async.AwaitTask
                logger.LogInformation("User balance updated from: {before} to {after}", userResult.Value.Balance, newBalance)

                return betId.ToString()
            } |> Async.StartAsTask