namespace FantasySportBetting.Domain.Models

open System

type Bet = {
    Id: Guid
    UserId: Guid
    MatchId: Guid
    Amount: decimal
    PlacedAt: DateTime
    Win: bool
}