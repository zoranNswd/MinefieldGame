using MinefieldGame.ConsoleApp.Enums;
using MinefieldGame.ConsoleApp.Intefraces;

namespace MinefieldGame.ConsoleApp
{
    public class InputHandler : IInputHandler
    {
        private readonly IConsoleReader _consoleReader;

        public InputHandler(IConsoleReader consoleReader)
        {
            _consoleReader = consoleReader;
        }

        public Direction GetDirection()
        {
            var key = _consoleReader.ReadKey(true).Key;
            return key switch
            {
                ConsoleKey.UpArrow => Direction.Up,
                ConsoleKey.DownArrow => Direction.Down,
                ConsoleKey.LeftArrow => Direction.Left,
                ConsoleKey.RightArrow => Direction.Right,
                _ => Direction.None
            };
        }
    }
}
