using MinefieldGame.ConsoleApp.Enums;

namespace MinefieldGame.ConsoleApp.Intefraces
{
    public interface IPositionHelper
    {
        IEnumerable<Direction> GetAvailableMoves((int Row, int Column) position);
        (int Row, int Column) GetNextPosition((int Row, int Column) position, Direction direction);
        string GetPositionNotation((int Row, int Column) position);
    }
}
