namespace FantasySportBetting.Application.Commands

open MediatR

type AddNewUserCommand(name: string, balance: decimal) =
    interface IRequest<string>
    member this.Name = name
    member this.Balance = balance

