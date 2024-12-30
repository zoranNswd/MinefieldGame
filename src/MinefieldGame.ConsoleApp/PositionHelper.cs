using MinefieldGame.ConsoleApp.Enums;
using MinefieldGame.ConsoleApp.Intefraces;
using System.Text;

namespace MinefieldGame.ConsoleApp
{
    public class PositionHelper : IPositionHelper
    {
        private readonly IGameConfiguration _gameConfiguration;

        public PositionHelper(IGameConfiguration gameConfiguration)
        {
            _gameConfiguration = gameConfiguration;
        }

        public IEnumerable<Direction> GetAvailableMoves((int Row, int Column) position)
        {
            var (row, col) = position;
            var availableMoves = new List<Direction>();

            if (row > 0) availableMoves.Add(Direction.Up);
            if (row < _gameConfiguration.Rows - 1) availableMoves.Add(Direction.Down);
            if (col > 0) availableMoves.Add(Direction.Left);
            if (col < _gameConfiguration.Columns - 1) availableMoves.Add(Direction.Right);

            return availableMoves;
        }

        public (int Row, int Column) GetNextPosition((int Row, int Column) currentPosition, Direction direction)
        {
            var (row, col) = currentPosition;

            return direction switch
            {
                Direction.Up => (row - 1, col),
                Direction.Down => (row + 1, col),
                Direction.Left => (row, col - 1),
                Direction.Right => (row, col + 1),
                Direction.None => (row, col),
                _ => (row, col)
            };
        }

        public string GetPositionNotation((int Row, int Column) position)
        {
            var (row, col) = position;

            // convert column index to excel-style letter(s)
            string columnLetter = ConvertColumnToExcelNotation(col);

            if (row == _gameConfiguration.Rows) return columnLetter;

            // convert to inverted row index numbering
            int rowNumber = _gameConfiguration.Rows - row;

            return $"{columnLetter}{rowNumber}";
        }

        private static string ConvertColumnToExcelNotation(int columnIndex)
        {
            // column index is 0-based; convert to 1-based
            columnIndex += 1;
            var columnName = new StringBuilder();

            while (columnIndex > 0)
            {
                columnIndex--; // shift for 0-based letter calculation
                char letter = (char)('A' + (columnIndex % 26));
                columnName.Insert(0, letter);
                columnIndex /= 26;
            }

            return columnName.ToString();
        }
    }
}
