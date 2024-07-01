namespace FantasySportBetting.Domain.Models

open System

type Match = {
    Id: string
    HomeTeam: string
    GuestTeam: string
    StartTime: DateTime
    WinCoefficient: decimal
    Winner: string
}