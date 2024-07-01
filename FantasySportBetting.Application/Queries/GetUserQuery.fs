namespace FantasySportBetting.Application.Queries

open MediatR

open FantasySportBetting.Domain.Models

type GetUserQuery(userId: string) =
    interface IRequest<User option>
    member this.UserId = userId
