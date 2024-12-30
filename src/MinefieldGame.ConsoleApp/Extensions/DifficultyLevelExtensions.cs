using MinefieldGame.ConsoleApp.Enums;

namespace MinefieldGame.ConsoleApp.Extensions
{
    public static class DifficultyLevelExtensions
    {
        public static int CalculateNumberOfInitialLives(this DifficultyLevel level)
        {
            return CalculateMaxCountOfExlusions(level) * 10;
        }

        public static int CalculateMaxCountOfExlusions(this DifficultyLevel level)
        {
            return level switch
            {
                DifficultyLevel.Easy => 3,
                DifficultyLevel.Medium => 2,
                DifficultyLevel.Hard => 1,
                _ => throw new NotImplementedException()
            };
        }
    }
}
