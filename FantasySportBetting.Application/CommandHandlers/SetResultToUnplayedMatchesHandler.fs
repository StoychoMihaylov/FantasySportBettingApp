namespace FantasySportBetting.Application.Handlers

open System
open System.Threading
open MediatR

open FantasySportBetting.Application.Commands
open FantasySportBetting.Infrastructure.MongoService.Context
open FantasySportBetting.Infrastructure.MongoService.Repositories
open FantasySportBetting.Infrastructure.MongoService.Documents

type SetResultToUnplayedMatchesHandler(context: MongoDbContext) =
    interface IRequestHandler<SetResultToUnplayedMatchesCommand, unit> with
        member this.Handle(command: SetResultToUnplayedMatchesCommand, cancellationToken: CancellationToken) =
            async {                    
                let! unplayedMatches = MatchRepository.GetUnplayedMatches context |> Async.AwaitTask
                let rand = Random()
                for matchDoc in unplayedMatches do
                    let winner = if rand.Next(2) = 0 then matchDoc.HomeTeam else matchDoc.GuestTeam
                    { matchDoc with Winner = winner } |> ignore
                    let updatedMatch = { matchDoc with Winner = winner }
                    do! MatchRepository.UpdateMatch context updatedMatch |> Async.AwaitTask |> Async.Ignore
                return()
            } |> Async.StartAsTask