namespace FantasySportBetting.Application.Handlers

open System
open FantasySportBetting.Domain.Models
open FantasySportBetting.Application.Commands
open MediatR
open System.Threading
open System.Threading.Tasks

type PlaceBetHandler() =
    interface IRequestHandler<PlaceBetCommand, Bet> with
        member _.Handle(request: PlaceBetCommand, cancellationToken: CancellationToken) =

            //TO DO: Logic to place a bet

            Task.FromResult({ 
                Id = Guid.NewGuid()
                UserId = request.UserId
                MatchId = request.MatchId
                Amount = request.Amount
                PlacedAt = DateTime.UtcNow
                Result = None
            })
   

