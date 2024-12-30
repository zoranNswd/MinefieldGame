namespace MinefieldGame.ConsoleApp.Intefraces
{
    public interface IMinefield
    {
        void TryGenerateMines((int Row, int Column) position);
        bool IsMine((int Row, int Column) position);
        HashSet<(int Row, int Column)> Mines { get; }
    }
}
