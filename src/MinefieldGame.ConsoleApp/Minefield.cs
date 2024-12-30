using MinefieldGame.ConsoleApp.Intefraces;

namespace MinefieldGame.ConsoleApp
{
    public class Minefield : IMinefield
    {
        private readonly IGameConfiguration _gameConfiguration;
        private readonly IMinePlacer _minePlacer;
        private HashSet<(int Row, int Column)> _mines = new();
        private HashSet<int> _rowsInitializedWithMines = new();

        public Minefield(
            IGameConfiguration gameConfiguration,
            IMinePlacer minePlacer)
        {
            _gameConfiguration = gameConfiguration;
            _minePlacer = minePlacer;
        }
        
        public void TryGenerateMines((int Row, int Column) position)
        {
            var (currentRow, currentColumn) = position;

            if (currentRow > _gameConfiguration.Rows - 1 || currentRow == 0)
            {
                return;
            }

            void GenerateMinesIfNeeded(int row)
            {
                if (!_rowsInitializedWithMines.Contains(row))
                {
                    List<int> possibleExclusions = [];

                    // guarantie passability of a row
                    possibleExclusions.Add(currentColumn);

                    if (row != currentRow)
                    {
                        // check the previous row (below the current row)
                        var lookupRow = row + 1; 
                        // define potential directions to look
                        var directions = new[] { -1, 1 }; // look to the left (-1) and to the right (+1)

                        foreach (var direction in directions)
                        {
                            var lookupColumn = currentColumn + direction;

                            // check if the column is within valid bounds
                            if (lookupColumn >= 0 && lookupColumn < _gameConfiguration.Columns)
                            {
                                // check if field is mine-free in previous row
                                var lookupPosition = (lookupRow, lookupColumn);

                                if (!IsMine(lookupPosition))
                                {
                                    possibleExclusions.Add(lookupColumn);
                                }
                            }
                        }
                    }

                    var minesOfARow = _minePlacer.PlaceMines(possibleExclusions);

                    foreach (var mine in minesOfARow)
                    {
                        _mines.Add((row, mine));
                    }

                    _rowsInitializedWithMines.Add(row);
                }
            }

            GenerateMinesIfNeeded(currentRow);
            // generate only for next row - entire minefield is not needed until the other end is reached
            GenerateMinesIfNeeded(currentRow - 1);
        }

        public bool IsMine((int Row, int Column) position) => _mines.Contains(position);

        public HashSet<(int Row, int Column)> Mines => _mines;
    }
}
