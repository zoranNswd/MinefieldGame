using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MinefieldGame.ConsoleApp;
using MinefieldGame.ConsoleApp.Configurations;
using MinefieldGame.ConsoleApp.Enums;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.RegisterDependencies(new GameConfiguration(            
            rows: 8,
            columns: 8,
            difficulty: DifficultyLevel.Hard,
            isCheatModeEnabled: true));
    })
    .Build();

var game = host.Services.GetRequiredService<Game>();

game.Run();