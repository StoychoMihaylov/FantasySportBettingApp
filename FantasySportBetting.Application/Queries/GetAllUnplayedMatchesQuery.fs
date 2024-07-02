namespace FantasySportBetting.Application.Queries

open MediatR

open FantasySportBetting.Domain.Models

type GetAllUnplayedMatchesQuery() =
    interface IRequest<Match list>
