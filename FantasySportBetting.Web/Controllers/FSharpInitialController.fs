namespace FantasySportBetting.Web.Controllers

open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging

[<ApiController>]
[<Route("[controller]")>]
type FSharpInitialController (logger : ILogger<FSharpInitialController>) =
    inherit ControllerBase()

    [<HttpGet>]
    member _.Get() = "Hello from F# ASP.NET Core"
