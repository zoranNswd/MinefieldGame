using MinefieldGame.ConsoleApp.Enums;

namespace MinefieldGame.ConsoleApp.Intefraces
{
    public interface IGameConfiguration
    {
        int Rows { get; }
        int Columns { get; }
        DifficultyLevel Difficulty { get; }
        bool IsCheatModeEnabled { get; }
    }
}
