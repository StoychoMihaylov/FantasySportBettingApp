namespace FantasySportBetting.Application.Commands

open MediatR

type AddNewMatchCommand(homeTeam: string, guestTeam: string, winCoefficient: decimal) =
    interface IRequest<string>
    member this.HomeTeam = homeTeam
    member this.GuestTeam = guestTeam
    member this.WinCoefficient = winCoefficient
