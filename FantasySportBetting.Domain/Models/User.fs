namespace FantasySportBetting.Domain.Models

open System

type User = {
    Id: Guid
    Username: string
    Balance: decimal
}