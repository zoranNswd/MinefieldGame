using MinefieldGame.ConsoleApp.Extensions;
using MinefieldGame.ConsoleApp.Intefraces;

namespace MinefieldGame.ConsoleApp
{
    public class Player : IPlayer
    {
        #region Private fields
        private readonly IGameConfiguration _gameConfiguration;
        #endregion

        #region Constructors
        public Player(IGameConfiguration gameConfiguration)
        {
            _gameConfiguration = gameConfiguration;
            Lives = _gameConfiguration.Difficulty.CalculateNumberOfInitialLives();
            Position = (_gameConfiguration.Rows, (_gameConfiguration.Columns - 1) / 2);
        }
        #endregion

        public int Lives { get; private set; }

        public int MovesTaken { get; private set; }

        public (int Row, int Column) Position { get; private set; }

        public void Move((int Row, int Column) nextPosition)
        {
            Position = nextPosition;

            if (Position.Row < _gameConfiguration.Rows)
            {
                // player is inside the field
                MovesTaken++;
            }
        }

        public void DecrementLives()
        {
            Lives--;
            MovesTaken++;
        }
    }
}
