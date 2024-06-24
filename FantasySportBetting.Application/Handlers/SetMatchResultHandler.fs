namespace FantasySportBetting.Application.Handlers

open FantasySportBetting.Application.Commands
open MediatR
open System.Threading

type SetMatchResultHandlerTwo() =
    interface IRequestHandler<SetMatchResultCommand, unit> with
        member _.Handle(command: SetMatchResultCommand, cancellationToken: CancellationToken) =
            async {
                // TO DO Update mongo collection
                //let collection = mongoService.GetCollection<Match>("Matches")
                //do! collection.UpdateOneAsync(filter, update, null, cancellationToken) |> Async.AwaitTask
                return ()
            } |> Async.StartAsTask