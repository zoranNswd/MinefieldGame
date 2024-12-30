using MinefieldGame.ConsoleApp.Configurations;
using MinefieldGame.ConsoleApp.Enums;

namespace MinefieldGame.ConsoleApp.Tests.Configurations
{
    public class GameConfigurationTests
    {
        [Fact]
        public void Constructor_ShouldInitializeProperties_WhenValidArgumentsAreProvided()
        {
            // Arrange
            int rows = 5;
            int columns = 5;
            DifficultyLevel difficulty = DifficultyLevel.Medium;
            bool isCheatModeEnabled = true;

            // Act
            var gameConfig = new GameConfiguration(rows, columns, difficulty, isCheatModeEnabled);

            // Assert
            Assert.Equal(rows, gameConfig.Rows);
            Assert.Equal(columns, gameConfig.Columns);
            Assert.Equal(difficulty, gameConfig.Difficulty);
            Assert.True(gameConfig.IsCheatModeEnabled);
        }

        [Fact]
        public void Constructor_ShouldSetDefaultCheatModeToFalse_WhenNotSpecified()
        {
            // Arrange
            int rows = 4;
            int columns = 4;
            DifficultyLevel difficulty = DifficultyLevel.Hard;

            // Act
            var gameConfig = new GameConfiguration(rows, columns, difficulty);

            // Assert
            Assert.False(gameConfig.IsCheatModeEnabled);
        }

        [Theory]
        [InlineData(1, 3)]
        [InlineData(0, 4)]
        [InlineData(-1, 5)]
        public void Constructor_ShouldThrowArgumentException_WhenRowsAreLessThanTwo(int rows, int columns)
        {
            // Arrange
            DifficultyLevel difficulty = DifficultyLevel.Easy;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new GameConfiguration(rows, columns, difficulty));
            Assert.Equal("Must be at least 2 rows (Parameter 'rows')", exception.Message);
        }

        [Theory]
        [InlineData(3, 1)]
        [InlineData(4, 0)]
        [InlineData(5, -1)]
        public void Constructor_ShouldThrowArgumentException_WhenColumnsAreLessThanTwo(int rows, int columns)
        {
            // Arrange
            DifficultyLevel difficulty = DifficultyLevel.Medium;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new GameConfiguration(rows, columns, difficulty));
            Assert.Equal("Must be at least 2 columns. (Parameter 'columns')", exception.Message);
        }

        [Fact]
        public void Constructor_ShouldHandleLargeValuesForRowsAndColumns()
        {
            // Arrange
            int rows = 1000;
            int columns = 1000;
            DifficultyLevel difficulty = DifficultyLevel.Hard;

            // Act
            var gameConfig = new GameConfiguration(rows, columns, difficulty);

            // Assert
            Assert.Equal(rows, gameConfig.Rows);
            Assert.Equal(columns, gameConfig.Columns);
            Assert.Equal(difficulty, gameConfig.Difficulty);
        }
    }
}
