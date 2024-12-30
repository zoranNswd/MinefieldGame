using MinefieldGame.ConsoleApp.Intefraces;

namespace MinefieldGame.ConsoleApp.Utilities
{
    public class ConsoleReader : IConsoleReader
    {
        public ConsoleKeyInfo ReadKey(bool intercept) => Console.ReadKey(intercept);
    }
}
