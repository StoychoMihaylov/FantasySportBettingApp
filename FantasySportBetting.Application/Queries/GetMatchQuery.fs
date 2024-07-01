namespace FantasySportBetting.Application.Queries

open MediatR

open FantasySportBetting.Domain.Models

type GetMatchQuery(matchId: string) =
    interface IRequest<Match option>
    member this.MatchId = matchId

