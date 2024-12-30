namespace MinefieldGame.ConsoleApp.Intefraces
{
    public interface IOutputHandler
    {
        void DisplayMessage(string message);
        void DisplayStatus(int lives, int movesTaken);
    }
}
