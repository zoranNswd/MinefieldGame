using MinefieldGame.ConsoleApp.Enums;
using MinefieldGame.ConsoleApp.Intefraces;
using Moq;

namespace MinefieldGame.ConsoleApp.Tests
{
    public class MinefieldTests
    {
        private readonly Mock<IGameConfiguration> _mockGameConfig;
        private readonly Mock<IMinePlacer> _mockMinePlacer;
        private readonly Minefield _minefield;

        public MinefieldTests()
        {
            // Set up mock objects
            _mockGameConfig = new Mock<IGameConfiguration>();
            _mockMinePlacer = new Mock<IMinePlacer>();

            // Configure mock for IGameConfiguration
            _mockGameConfig.Setup(c => c.Rows).Returns(5);
            _mockGameConfig.Setup(c => c.Columns).Returns(5);
            _mockGameConfig.Setup(c => c.Difficulty).Returns(DifficultyLevel.Medium);

            // Create Minefield with mock dependencies
            _minefield = new Minefield(_mockGameConfig.Object, _mockMinePlacer.Object);
        }

        [Fact]
        public void TryGenerateMines_ShouldGenerateMinesForValidRow()
        {
            // Arrange
            var position = (Row: 2, Column: 2); // Position in a valid row (between 1 and Rows - 1)

            // Mock the behavior of _minePlacer.PlaceMines to return a mine at (Row, Column)
            _mockMinePlacer.Setup(mp => mp.PlaceMines(It.IsAny<List<int>>()))
                .Returns(new HashSet<int> { 2 }); // Simulate placing a mine at column 2 in the given row

            // Act
            _minefield.TryGenerateMines(position);

            // Assert
            Assert.Contains((2, 2), _minefield.Mines); // Check that mine was placed at (2, 2)
        }

        [Fact]
        public void TryGenerateMines_ShouldNotGenerateMinesForRow0()
        {
            // Arrange
            var position = (Row: 0, Column: 2); // Row 0 should be ignored by the generator

            // Act
            _minefield.TryGenerateMines(position);

            // Assert
            Assert.Empty(_minefield.Mines); // No mines should be generated for row 0
        }

        [Fact]
        public void IsMine_ShouldReturnTrueIfMineIsAtPosition()
        {
            // Arrange
            var gameConfigMock = new Mock<IGameConfiguration>();
            gameConfigMock.Setup(config => config.Rows).Returns(5);
            gameConfigMock.Setup(config => config.Columns).Returns(5);
            gameConfigMock.Setup(config => config.Difficulty).Returns(DifficultyLevel.Medium);

            var minePlacerMock = new Mock<IMinePlacer>();
            minePlacerMock.Setup(mp => mp.PlaceMines(It.IsAny<List<int>>()))
                          .Returns(new HashSet<int> { 1 }); // Mock mine at column 1

            var minefield = new Minefield(gameConfigMock.Object, minePlacerMock.Object);

            // Act
            minefield.TryGenerateMines((2, 1)); // Try to place mines near position (2, 1)

            // Assert
            var result = minefield.IsMine((2, 1)); // Check if mine is placed at (2, 1)
            Assert.True(result, "Mine should be at position (2, 1)");
        }

        [Fact]
        public void IsMine_ShouldReturnFalseIfNoMineAtPosition()
        {
            // Arrange
            var gameConfigMock = new Mock<IGameConfiguration>();
            gameConfigMock.Setup(config => config.Rows).Returns(5);
            gameConfigMock.Setup(config => config.Columns).Returns(5);
            gameConfigMock.Setup(config => config.Difficulty).Returns(DifficultyLevel.Medium);

            var minePlacerMock = new Mock<IMinePlacer>();
            minePlacerMock.Setup(mp => mp.PlaceMines(It.IsAny<List<int>>()))
                          .Returns(new HashSet<int>()); // No mines placed

            var minefield = new Minefield(gameConfigMock.Object, minePlacerMock.Object);

            // Act
            minefield.TryGenerateMines((2, 1)); // Try to place mines near position (2, 1)

            // Assert
            var result = minefield.IsMine((2, 1)); // Check if no mine is placed at (2, 1)
            Assert.False(result, "Mine should not be at position (2, 1)"); // Expecting false as no mine was placed
        }

        [Fact]
        public void TryGenerateMines_ShouldNotGenerateMinesOnRowThatAlreadyHasMines()
        {
            // Arrange
            var gameConfigMock = new Mock<IGameConfiguration>();
            gameConfigMock.Setup(config => config.Rows).Returns(5);
            gameConfigMock.Setup(config => config.Columns).Returns(5);
            gameConfigMock.Setup(config => config.Difficulty).Returns(DifficultyLevel.Medium);

            var minePlacerMock = new Mock<IMinePlacer>();
            minePlacerMock.Setup(mp => mp.PlaceMines(It.IsAny<List<int>>()))
                          .Returns(new HashSet<int> { 1 }); // Mock mine at column 1

            var minefield = new Minefield(gameConfigMock.Object, minePlacerMock.Object);

            // Act: First call generates mines
            minefield.TryGenerateMines((2, 1));

            // Assert: Ensure mines are generated on row 2
            Assert.True(minefield.IsMine((2, 1)), "Mine should be at position (2, 1)");

            // Act: Second call should not regenerate mines on row 2
            minefield.TryGenerateMines((2, 1));

            // Assert: Ensure that the mine at position (2, 1) is still present
            Assert.True(minefield.IsMine((2, 1)), "Mine should still be at position (2, 1)");

            // Act: Call on another row to see if mines are generated there
            minefield.TryGenerateMines((3, 1));

            // Assert: Ensure mines are generated on row 3
            Assert.True(minefield.IsMine((3, 1)), "Mine should be at position (3, 1)");
        }

        [Fact]
        public void TryGenerateMines_ShouldExcludeCurrentColumnFromMineGeneration()
        {
            // Arrange
            var position = (Row: 2, Column: 2);
            _mockMinePlacer.Setup(mp => mp.PlaceMines(It.IsAny<List<int>>()))
                .Returns(new HashSet<int> { 3 }); // Simulate placing a mine at column 3

            // Act
            _minefield.TryGenerateMines(position);

            // Assert
            Assert.DoesNotContain((2, 2), _minefield.Mines); // Ensure current column (2) is excluded from mine placement
        }

        [Fact]
        public void TryGenerateMines_ShouldGenerateMinesForAdjacentRows()
        {
            // Arrange
            var position = (Row: 2, Column: 2);

            _mockMinePlacer.Setup(mp => mp.PlaceMines(It.IsAny<List<int>>()))
                .Returns(new HashSet<int> { 1 }); // Simulate placing a mine in column 1 for valid rows

            // Act
            _minefield.TryGenerateMines(position);

            // Assert
            Assert.Contains((2, 1), _minefield.Mines); // Ensure mine was placed in row 2, column 1
            Assert.Contains((1, 1), _minefield.Mines); // Ensure mine was placed in row 1, column 1
        }
    }
}
