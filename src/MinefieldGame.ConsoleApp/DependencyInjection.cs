using Microsoft.Extensions.DependencyInjection;
using MinefieldGame.ConsoleApp.Configurations;
using MinefieldGame.ConsoleApp.Intefraces;
using MinefieldGame.ConsoleApp.Utilities;

namespace MinefieldGame.ConsoleApp
{
    public static class DependencyInjection
    {
        public static void RegisterDependencies(this IServiceCollection services, GameConfiguration gameConfiguration)
        {
            services.AddSingleton<IGameConfiguration>(gameConfiguration);

            services.AddSingleton<IPlayer, Player>();
            services.AddSingleton<IPositionHelper, PositionHelper>();

            services.AddSingleton<IMinefield, Minefield>();
            services.AddSingleton<IMinePlacer, MinePlacer>();

            services.AddSingleton<IOutputHandler, OutputHandler>();
            services.AddSingleton<IInputHandler, InputHandler>();
            services.AddSingleton<IConsoleReader, ConsoleReader>();

            services.AddSingleton<Game>();
        }
    }
}
