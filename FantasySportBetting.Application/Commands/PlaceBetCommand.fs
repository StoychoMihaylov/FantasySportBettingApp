namespace FantasySportBetting.Application.Commands

open System
open FantasySportBetting.Domain.Models
open MediatR

type PlaceBetCommand(userId: Guid, matchId: Guid, amount: decimal) =
    interface IRequest<Bet>
    member _.UserId = userId
    member _.MatchId = matchId
    member _.Amount = amount
