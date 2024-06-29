namespace FantasySportBetting.Application.Commands

open MediatR

open FantasySportBetting.Domain.Models

type GetMatchCommand(matchId: string) =
    interface IRequest<Match option>
    member this.MatchId = matchId

