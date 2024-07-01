namespace FantasySportBetting.Web

#nowarn "20"

open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open MediatR
open System.Reflection

open FantasySportBetting.Infrastructure.MongoService.Context
open FantasySportBetting.Domain.Models
open FantasySportBetting.Domain.Settings
open FantasySportBetting.Application.Commands
open FantasySportBetting.Application.Handlers
open FantasySportBetting.Application.Queries

module Program =
    let exitCode = 0

    [<EntryPoint>]
    let main args =

        let builder = WebApplication.CreateBuilder(args)

        // Config settings
        builder.Services.Configure<MongoDbOptions>(builder.Configuration.GetSection(nameof(MongoDbOptions)))
        
        // Register services
        builder.Services.AddControllers()
        builder.Services.AddScoped<MongoDbContext>()
        //builder.Services.AddHostedService<MatchResultSetterService>()
        builder.Services.AddSwaggerGen()

        // Register MediatR
        builder.Services.AddMediatR(Assembly.GetExecutingAssembly()) |> ignore
        builder.Services.AddScoped<IRequestHandler<AddNewMatchCommand, string>, AddNewMatchHandler>()
        builder.Services.AddScoped<IRequestHandler<GetMatchQuery, Match option>, GetMatchHandler>()
        builder.Services.AddScoped<IRequestHandler<SetResultToUnplayedMatchesCommand, unit>, SetResultToUnplayedMatchesHandler>()
        builder.Services.AddScoped<IRequestHandler<AddNewUserCommand, string>, AddNewUserHandler>()
        builder.Services.AddScoped<IRequestHandler<GetUserQuery, User option>, GetUserHandler>()

        let app = builder.Build()

        // Middleware config
        if app.Environment.IsDevelopment() then
            app.UseDeveloperExceptionPage()
            app.UseSwagger()
            app.UseSwaggerUI() |> ignore

        app.UseHttpsRedirection()
        app.UseAuthorization()
        app.MapControllers()
        app.Run()

        exitCode
