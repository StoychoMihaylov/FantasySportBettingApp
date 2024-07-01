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

type AddNewMatchHandler(logger: ILogger<AddNewMatchHandler>, context: MongoDbContext) =
    interface IRequestHandler<AddNewMatchCommand, string> with
        member this.Handle(command: AddNewMatchCommand, cancellationToken: CancellationToken) =
            async {
                let matchDocument: MatchDocument = {
                    Id = BsonObjectId(ObjectId.GenerateNewId())
                    HomeTeam = command.HomeTeam
                    GuestTeam = command.GuestTeam
                    StartTime = DateTime.Now
                    WinCoefficient = command.WinCoefficient
                    Winner = ""
                }

                let! matchId = MatchRepository.addMatch context matchDocument |> Async.AwaitTask
                logger.LogInformation("New match added with Id: {matchId}", matchId)
                return matchId.ToString()
            } |> Async.StartAsTask
