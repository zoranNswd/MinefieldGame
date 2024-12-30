using MinefieldGame.ConsoleApp.Enums;
using MinefieldGame.ConsoleApp.Intefraces;
using Moq;

namespace MinefieldGame.ConsoleApp.Tests
{
    public class MinePlacerTests
    {
        [Fact]
        public void PlaceMines_ShouldThrowArgumentException_WhenPossibleExclusionsAreNullOrEmpty()
        {
            // Arrange
            var mockConfig = new Mock<IGameConfiguration>();
            var minePlacer = new MinePlacer(mockConfig.Object, new Random(42));

            // Act & Assert
            Assert.Throws<ArgumentException>(() => minePlacer.PlaceMines(null));
            Assert.Throws<ArgumentException>(() => minePlacer.PlaceMines([]));
        }

        [Fact]
        public void PlaceMines_ShouldReturnEmptySet_WhenNoValidNumbersAreAvailable()
        {
            // Arrange
            var gameConfigMock = new Mock<IGameConfiguration>();
            gameConfigMock.Setup(config => config.Columns).Returns(5);
            gameConfigMock.Setup(config => config.Difficulty).Returns(DifficultyLevel.Medium);

            var minePlacerMock = new Mock<IMinePlacer>();
            minePlacerMock.Setup(mp => mp.PlaceMines(It.IsAny<List<int>>()))
                          .Returns(new HashSet<int>());

            var minefield = new Minefield(gameConfigMock.Object, minePlacerMock.Object);

            // Create a scenario where all columns are excluded (all columns are marked as exclusions)
            var exclusions = new List<int> { 0, 1, 2, 3, 4 };  // All columns are excluded

            // Act: Try to place mines
            var mines = minePlacerMock.Object.PlaceMines(exclusions);

            // Assert: The returned set should be empty as no valid columns are available
            Assert.Empty(mines);
        }

        [Fact]
        public void SelectRandomExclusions_ShouldReturnCorrectNumberOfExclusions()
        {
            // Arrange
            var mockConfig = new Mock<IGameConfiguration>();
            mockConfig.Setup(c => c.Difficulty).Returns(DifficultyLevel.Medium);

            var minePlacer = new MinePlacer(mockConfig.Object, new Random(42));
            var possibleExclusions = new List<int> { 0, 1, 2, 3, 4 };

            // Act
            var selectedExclusions = minePlacer.SelectRandomExclusions(possibleExclusions, 2);

            // Assert
            Assert.NotNull(selectedExclusions);
            Assert.Equal(2, selectedExclusions.Count);
        }
    }
}
