using MinefieldGame.ConsoleApp.Enums;
using MinefieldGame.ConsoleApp.Extensions;

namespace MinefieldGame.ConsoleApp.Tests.Extensions
{
    public class DifficultyLevelExtensionsTests
    {
        [Theory]
        [InlineData(DifficultyLevel.Easy, 3)]
        [InlineData(DifficultyLevel.Medium, 2)]
        [InlineData(DifficultyLevel.Hard, 1)]
        public void CalculateMaxCountOfExclusions_ShouldReturnCorrectValue_ForValidDifficultyLevels(DifficultyLevel level, int expectedExclusions)
        {
            // Act
            var result = level.CalculateMaxCountOfExlusions();

            // Assert
            Assert.Equal(expectedExclusions, result);
        }

        [Fact]
        public void CalculateMaxCountOfExclusions_ShouldThrowNotImplementedException_ForInvalidDifficultyLevel()
        {
            // Arrange
            var invalidLevel = (DifficultyLevel)999;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => invalidLevel.CalculateMaxCountOfExlusions());
        }

        [Theory]
        [InlineData(DifficultyLevel.Easy, 30)]   // 3 * 10
        [InlineData(DifficultyLevel.Medium, 20)] // 2 * 10
        [InlineData(DifficultyLevel.Hard, 10)]   // 1 * 10
        public void CalculateNumberOfInitialLives_ShouldReturnCorrectValue_ForValidDifficultyLevels(DifficultyLevel level, int expectedLives)
        {
            // Act
            var result = level.CalculateNumberOfInitialLives();

            // Assert
            Assert.Equal(expectedLives, result);
        }

        [Fact]
        public void CalculateNumberOfInitialLives_ShouldThrowNotImplementedException_ForInvalidDifficultyLevel()
        {
            // Arrange
            var invalidLevel = (DifficultyLevel)999;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => invalidLevel.CalculateNumberOfInitialLives());
        }
    }
}
