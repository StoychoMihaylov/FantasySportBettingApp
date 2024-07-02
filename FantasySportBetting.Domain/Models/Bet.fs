namespace FantasySportBetting.Domain.Models

open System

type Bet = {
    Id: string
    UserId: string
    MatchId: string
    Amount: decimal
    PlacedAt: DateTime
    IsProcessed: bool
}