using MinefieldGame.ConsoleApp.Intefraces;

namespace MinefieldGame.ConsoleApp
{
    public class OutputHandler : IOutputHandler
    {
        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void DisplayStatus(int lives, int movesTaken)
        {
            Console.WriteLine($"Lives: {lives}, Moves: {movesTaken}");
        }
    }
}
