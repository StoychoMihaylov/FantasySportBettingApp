namespace FantasySportBetting.Application.Commands

open MediatR

type SetResultToUnplayedMatchesCommand() =
    interface IRequest<unit>