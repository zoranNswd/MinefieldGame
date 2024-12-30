using MinefieldGame.ConsoleApp.Enums;
using MinefieldGame.ConsoleApp.Intefraces;

namespace MinefieldGame.ConsoleApp
{
    public class Game
    {
        private readonly IGameConfiguration _gameConfiguration;
        private readonly IPlayer _player;
        private readonly IPositionHelper _positionHelper;
        private readonly IMinefield _minefield;
        private readonly IOutputHandler _outputHandler;
        private readonly IInputHandler _inputHandler;
        
        public Game(
            IGameConfiguration gameConfiguration,
            IPlayer player,
            IPositionHelper positionHelper,
            IMinefield minefield,
            IOutputHandler outputHandler,
            IInputHandler inputHandler)
        {
            _gameConfiguration = gameConfiguration;
            _player = player;
            _positionHelper = positionHelper;
            _minefield = minefield;
            _outputHandler = outputHandler;
            _inputHandler = inputHandler;            
        }

        public void Run()
        {
            while (_player.Position.Row == _gameConfiguration.Rows)
            {
                var currentPosition = _player.Position;

                _outputHandler.DisplayMessage(
                    $"YOU ARE OUTSIDE AT COLUMN: " +
                    $"{_positionHelper.GetPositionNotation(currentPosition)}");

                var availableMoves = _positionHelper.GetAvailableMoves(currentPosition);

                _outputHandler.DisplayMessage($"Select Move [{string.Join(", ", availableMoves)}]:");

                var direction = _inputHandler.GetDirection();

                if (!availableMoves.Contains(direction))
                {
                    var messagePrefix = string.Empty;
                    if (direction != Direction.None)
                    {
                        messagePrefix = $"{direction} is ";
                    }
                    _outputHandler.DisplayMessage($"{messagePrefix}Invalid move! Try again.");
                    continue;
                }

                var nextPosition = _positionHelper.GetNextPosition(currentPosition, direction);

                _player.Move(nextPosition);
            }

            _outputHandler.DisplayMessage($"You entered at Position: {_positionHelper.GetPositionNotation(_player.Position)}");

            RunGameLoop();
        }

        public void RunGameLoop()
        {
            _outputHandler.DisplayMessage($"Difficulty: {_gameConfiguration.Difficulty}");
            _outputHandler.DisplayMessage("-/-/-/-/-/-/-/-/");
            while (_player.Lives > 0)
            {
                var currentPosition = _player.Position;

                _minefield.TryGenerateMines(currentPosition);

                _outputHandler.DisplayMessage($"Position: {_positionHelper.GetPositionNotation(currentPosition)}");
                _outputHandler.DisplayStatus(_player.Lives, _player.MovesTaken);    
                
                var availableMoves = _positionHelper.GetAvailableMoves(currentPosition);

                _outputHandler.DisplayMessage($"Select Move [{string.Join(", ", availableMoves)}]:");

                if (_gameConfiguration.IsCheatModeEnabled)
                {
                    var currentMines = _minefield.Mines
                        .Where(p => p.Row == currentPosition.Row)
                        .Select(p => _positionHelper.GetPositionNotation(p));

                    var nextMines = _minefield.Mines
                        .Where(p => p.Row == currentPosition.Row - 1)
                        .Select(p => _positionHelper.GetPositionNotation(p));

                    _outputHandler.DisplayMessage($"MINES IN CURRENT ROW: [{string.Join(", ", currentMines.OrderBy(_ => _))}]");
                    _outputHandler.DisplayMessage($"MINES IN NEXT ROW: [{string.Join(", ", nextMines.OrderBy(_ => _))}]");
                }                

                var direction = _inputHandler.GetDirection();
                if (!availableMoves.Contains(direction))
                {
                    var messagePrefix = string.Empty;
                    if (direction != Direction.None)
                    {
                        messagePrefix = $"{direction} is ";
                    }
                    _outputHandler.DisplayMessage($"{messagePrefix}Invalid move! Try again.");
                    continue;
                }

                _outputHandler.DisplayMessage($"{direction}");

                var nextPosition = _positionHelper.GetNextPosition(currentPosition, direction);

                if (_minefield.IsMine(nextPosition))
                {
                    _player.DecrementLives();
                    _outputHandler.DisplayMessage("You hit a mine!");
                }
                else
                {
                    _player.Move(nextPosition);
                }                

                if (_player.Position.Row == 0)
                {
                    
                    _outputHandler.DisplayMessage("Congratulations! You've reached the other side!");
                    _outputHandler.DisplayMessage($"Position: {_positionHelper.GetPositionNotation(_player.Position)}");
                    _outputHandler.DisplayStatus(_player.Lives, _player.MovesTaken);
                    break;
                }
            }

            if (_player.Lives == 0)
            {
                _outputHandler.DisplayMessage("GAME OVER");
                _outputHandler.DisplayStatus(_player.Lives, _player.MovesTaken);
            }
        }
    }
}
