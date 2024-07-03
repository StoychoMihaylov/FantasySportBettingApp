namespace FantasySportBetting.Application.Commands

open MediatR

type ProcessBetsCommand() =
    interface IRequest<unit>