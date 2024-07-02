namespace FantasySportBetting.Application.Commands

open MediatR

type AddNewBetCommand(userId: string, matchId: string, amount: decimal) =
    interface IRequest<string>
    member this.UserId = userId
    member this.MatchId = matchId
    member this.Amount = amount