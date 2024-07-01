namespace FantasySportBetting.Application.Handlers

open System.Threading
open MediatR
open MongoDB.Bson
open Microsoft.Extensions.Logging

open FantasySportBetting.Application.Commands
open FantasySportBetting.Infrastructure.MongoService.Context
open FantasySportBetting.Infrastructure.MongoService.Repositories
open FantasySportBetting.Infrastructure.MongoService.Documents

type AddNewUserHandler(logger: ILogger<AddNewUserHandler>, context: MongoDbContext) =
    interface IRequestHandler<AddNewUserCommand, string> with
        member this.Handle(command: AddNewUserCommand, cancellationToken: CancellationToken) =
            async {
                let userDocument: UserDocument = {
                    Id = BsonObjectId(ObjectId.GenerateNewId())
                    Name = command.Name
                    Balance = command.Balance
                }

                let! userId = UserRepository.addUser context userDocument |> Async.AwaitTask
                logger.LogInformation("New user added with Id: {userId}", userId)
                return userId.ToString()
            } |> Async.StartAsTask


