using MinefieldGame.ConsoleApp.Enums;
using MinefieldGame.ConsoleApp.Intefraces;

namespace MinefieldGame.ConsoleApp.Configurations
{
    public class GameConfiguration : IGameConfiguration
    {
        public GameConfiguration(int rows, int columns, DifficultyLevel difficulty, bool isCheatModeEnabled = false)
        {
            #region Validations
            if (rows < 2)
                throw new ArgumentException("Must be at least 2 rows", nameof(rows));

            if (columns < 2)
                throw new ArgumentException("Must be at least 2 columns.", nameof(columns));
            #endregion

            Rows = rows;
            Columns = columns;
            Difficulty = difficulty;
            IsCheatModeEnabled = isCheatModeEnabled;
        }

        public int Rows { get; }
        public int Columns { get; }
        public DifficultyLevel Difficulty { get; }
        public bool IsCheatModeEnabled { get; }
    }
}
