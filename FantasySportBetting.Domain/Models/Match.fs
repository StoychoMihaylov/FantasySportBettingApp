namespace FantasySportBetting.Domain.Models

open System

type Match = {
    HomeTeam: string
    GuestTeam: string
    StartTime: DateTime
    Winner: string
}