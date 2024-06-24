namespace FantasySportBetting.Domain.Models

open System

type Bet = {
    Id: Guid
    UserId: Guid
    MatchId: Guid
    Amount: decimal
    PlacedAt: DateTime
    Result: Option<string> // "win" or "loss"
}