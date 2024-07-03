namespace FantasySportBetting.Application.Commands

open MediatR

type AddNewBetCommand(userId: string, matchId: string, winnerTeam: string, amount: decimal) =
    interface IRequest<string>
    member this.UserId = userId
    member this.MatchId = matchId
    member this.WinnerTeam = winnerTeam
    member this.Amount = amount