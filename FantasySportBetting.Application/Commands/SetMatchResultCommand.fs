namespace FantasySportBetting.Application.Commands

open System
open MediatR

type SetMatchResultCommand(matchId: Guid, result: string) =
    interface IRequest<unit>
    member _.MatchId = matchId
    member _.Result = result