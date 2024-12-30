namespace MinefieldGame.ConsoleApp.Intefraces
{
    public interface IPlayer
    {
        int Lives { get; }
        int MovesTaken { get; }
        (int Row, int Column) Position { get; }
        void Move((int Row, int Column) nextPosition);
        void DecrementLives();
    }
}
