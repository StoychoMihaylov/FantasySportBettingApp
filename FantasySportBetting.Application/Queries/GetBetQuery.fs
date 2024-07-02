namespace FantasySportBetting.Application.Queries

open MediatR

open FantasySportBetting.Domain.Models

type GetBetQuery(betId: string) =
    interface IRequest<Bet option>
    member this.BetId = betId
