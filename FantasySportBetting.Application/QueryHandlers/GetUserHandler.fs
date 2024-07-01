namespace FantasySportBetting.Application.Queries

open System.Threading
open MediatR
open Microsoft.Extensions.Logging

open FantasySportBetting.Application.Queries
open FantasySportBetting.Infrastructure.MongoService.Context
open FantasySportBetting.Infrastructure.MongoService.Repositories
open FantasySportBetting.Domain.Models

type GetUserHandler(logger: ILogger<GetUserHandler>, context: MongoDbContext) =
    interface IRequestHandler<GetUserQuery, User option> with
        member this.Handle(command: GetUserQuery, cancellationToken: CancellationToken) =
            async {       
                try
                    let! userOption = UserRepository.getUser context command.UserId |> Async.AwaitTask
                    match userOption with
                    | Some userDocument ->
                        let userModel: User = {
                            Id = userDocument.Id.ToString()
                            Name = userDocument.Name
                            Balance = userDocument.Balance
                        }
                        return Some userModel
                    | None -> return None
                with
                | ex ->
                    logger.LogError(ex, "Error retrieving user")
                    return None
            } |> Async.StartAsTask