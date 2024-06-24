namespace FantasySportBetting.Domain.Models

open System

type Match = {
    Id: Guid
    HomeTeam: Team
    AwayTeam: Team
    StartTime: DateTime
    Result: Option<string>
}