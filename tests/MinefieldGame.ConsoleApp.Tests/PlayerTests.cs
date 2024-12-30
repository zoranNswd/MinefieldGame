using MinefieldGame.ConsoleApp.Enums;
using MinefieldGame.ConsoleApp.Intefraces;
using Moq;

namespace MinefieldGame.ConsoleApp.Tests
{
    public class PlayerTests
    {
        [Fact]
        public void Constructor_ShouldInitializePlayer_WithCorrectValues()
        {
            // Arrange
            var mockConfig = new Mock<IGameConfiguration>();
            mockConfig.Setup(c => c.Rows).Returns(10);
            mockConfig.Setup(c => c.Columns).Returns(5);
            mockConfig.Setup(c => c.Difficulty).Returns(DifficultyLevel.Medium);

            // Act
            var player = new Player(mockConfig.Object);

            // Assert
            Assert.Equal(20, player.Lives);
            Assert.Equal((10, 2), player.Position);
            Assert.Equal(0, player.MovesTaken);
        }

        [Fact]
        public void Move_ShouldUpdatePositionAndIncrementMovesTaken_WhenInsideField()
        {
            // Arrange
            var mockConfig = new Mock<IGameConfiguration>();
            mockConfig.Setup(c => c.Rows).Returns(10);
            mockConfig.Setup(c => c.Columns).Returns(5);
            mockConfig.Setup(c => c.Difficulty).Returns(DifficultyLevel.Easy);

            var player = new Player(mockConfig.Object);
            var nextPosition = (Row: 5, Column: 3);

            // Act
            player.Move(nextPosition);

            // Assert
            Assert.Equal(nextPosition, player.Position);
            Assert.Equal(1, player.MovesTaken);
        }

        [Fact]
        public void Move_ShouldNotIncrementMovesTaken_WhenOutsideField()
        {
            // Arrange
            var mockConfig = new Mock<IGameConfiguration>();
            mockConfig.Setup(c => c.Rows).Returns(10);
            mockConfig.Setup(c => c.Columns).Returns(5);
            mockConfig.Setup(c => c.Difficulty).Returns(DifficultyLevel.Hard);

            var player = new Player(mockConfig.Object);
            var outsidePosition = (Row: 11, Column: 2);

            // Act
            player.Move(outsidePosition);

            // Assert
            Assert.Equal(outsidePosition, player.Position);
            Assert.Equal(0, player.MovesTaken);
        }

        [Fact]
        public void DecrementLives_ShouldDecreaseLivesAndIncrementMovesTaken()
        {
            // Arrange
            var mockConfig = new Mock<IGameConfiguration>();
            mockConfig.Setup(c => c.Rows).Returns(10);
            mockConfig.Setup(c => c.Columns).Returns(5);
            mockConfig.Setup(c => c.Difficulty).Returns(DifficultyLevel.Hard);

            var player = new Player(mockConfig.Object);
            var initialLives = player.Lives;

            // Act
            player.DecrementLives();

            // Assert
            Assert.Equal(initialLives - 1, player.Lives);
            Assert.Equal(1, player.MovesTaken);
        }
    }
}
