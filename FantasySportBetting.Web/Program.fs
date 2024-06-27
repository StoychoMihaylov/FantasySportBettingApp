namespace FantasySportBetting.Web

#nowarn "20"

open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open MediatR
open System.Reflection
open Microsoft.OpenApi.Models

open FantasySportBetting.Domain.Settings
open FantasySportBetting.Application.BackgroundServices
open FantasySportBetting.Infrastructure.MongoService.Context
open FantasySportBetting.Application.Commands
open FantasySportBetting.Application.Handlers

module Program =
    let exitCode = 0

    [<EntryPoint>]
    let main args =

        let builder = WebApplication.CreateBuilder(args)

        // Config settings
        builder.Services.Configure<MongoDbOptions>(builder.Configuration.GetSection(nameof(MongoDbOptions)))
        
        // Register services
        builder.Services.AddControllers()
        builder.Services.AddSingleton<MongoDbContext>()
        builder.Services.AddHostedService<MatchResultBackgroundService>()
        
        builder.Services.AddSwaggerGen(fun c ->
            c.SwaggerDoc("v1", OpenApiInfo(Title = "FantasySportBetting API", Version = "v1"))
        ) |> ignore

        // MediatR
        builder.Services.AddMediatR(Assembly.GetExecutingAssembly()) |> ignore
        builder.Services.AddScoped<IRequestHandler<AddNewMatchCommand, string>, AddNewMatchHandler>()

        let app = builder.Build()

        // Middleware config
        if app.Environment.IsDevelopment() then
            app.UseDeveloperExceptionPage()
            app.UseSwagger()
            app.UseSwaggerUI(fun c ->
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FantasySportBetting API v1")

            ) |> ignore

        app.UseHttpsRedirection()
        app.UseAuthorization()
        app.MapControllers()
        app.Run()

        exitCode
