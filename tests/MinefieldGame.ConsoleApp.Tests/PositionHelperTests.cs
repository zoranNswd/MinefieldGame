using MinefieldGame.ConsoleApp.Enums;
using MinefieldGame.ConsoleApp.Intefraces;
using Moq;

namespace MinefieldGame.ConsoleApp.Tests
{
    public class PositionHelperTests
    {
        private Mock<IGameConfiguration> _gameConfigMock;
        private IPositionHelper? _positionHelper;

        public PositionHelperTests()
        {
            _gameConfigMock = new Mock<IGameConfiguration>();
        }

        [Fact]
        public void GetAvailableMoves_ShouldReturnCorrectMoves_WhenPositionIsInsideGrid()
        {
            // Arrange
            _gameConfigMock.Setup(g => g.Rows).Returns(5); // 5 rows
            _gameConfigMock.Setup(g => g.Columns).Returns(5); // 5 columns
            _positionHelper = new PositionHelper(_gameConfigMock.Object);

            // Act
            var availableMoves = _positionHelper.GetAvailableMoves((2, 2)).ToList();

            // Assert
            Assert.Contains(Direction.Up, availableMoves);
            Assert.Contains(Direction.Down, availableMoves);
            Assert.Contains(Direction.Left, availableMoves);
            Assert.Contains(Direction.Right, availableMoves);
        }

        [Fact]
        public void GetAvailableMoves_ShouldReturnCorrectMoves_WhenPositionIsOnTopLeftCorner()
        {
            // Arrange
            _gameConfigMock.Setup(g => g.Rows).Returns(5);
            _gameConfigMock.Setup(g => g.Columns).Returns(5);
            _positionHelper = new PositionHelper(_gameConfigMock.Object);

            // Act
            var availableMoves = _positionHelper.GetAvailableMoves((0, 0)).ToList();

            // Assert
            Assert.Contains(Direction.Down, availableMoves);
            Assert.Contains(Direction.Right, availableMoves);
            Assert.DoesNotContain(Direction.Up, availableMoves);
            Assert.DoesNotContain(Direction.Left, availableMoves);
        }

        [Fact]
        public void GetAvailableMoves_ShouldReturnCorrectMoves_WhenPositionIsOnBottomRightCorner()
        {
            // Arrange
            _gameConfigMock.Setup(g => g.Rows).Returns(5);
            _gameConfigMock.Setup(g => g.Columns).Returns(5);
            _positionHelper = new PositionHelper(_gameConfigMock.Object);

            // Act
            var availableMoves = _positionHelper.GetAvailableMoves((4, 4)).ToList();

            // Assert
            Assert.Contains(Direction.Up, availableMoves);
            Assert.Contains(Direction.Left, availableMoves);
            Assert.DoesNotContain(Direction.Down, availableMoves);
            Assert.DoesNotContain(Direction.Right, availableMoves);
        }

        [Fact]
        public void GetNextPosition_ShouldReturnCorrectPosition_ForDirectionUp()
        {
            // Arrange
            _gameConfigMock.Setup(g => g.Rows).Returns(5);
            _gameConfigMock.Setup(g => g.Columns).Returns(5);
            _positionHelper = new PositionHelper(_gameConfigMock.Object);

            // Act
            var nextPosition = _positionHelper.GetNextPosition((2, 2), Direction.Up);

            // Assert
            Assert.Equal((1, 2), nextPosition);  // Moves up
        }

        [Fact]
        public void GetNextPosition_ShouldReturnCorrectPosition_ForDirectionDown()
        {
            // Arrange
            _gameConfigMock.Setup(g => g.Rows).Returns(5);
            _gameConfigMock.Setup(g => g.Columns).Returns(5);
            _positionHelper = new PositionHelper(_gameConfigMock.Object);

            // Act
            var nextPosition = _positionHelper.GetNextPosition((2, 2), Direction.Down);

            // Assert
            Assert.Equal((3, 2), nextPosition);  // Moves down
        }

        [Fact]
        public void GetNextPosition_ShouldReturnCorrectPosition_ForDirectionLeft()
        {
            // Arrange
            _gameConfigMock.Setup(g => g.Rows).Returns(5);
            _gameConfigMock.Setup(g => g.Columns).Returns(5);
            _positionHelper = new PositionHelper(_gameConfigMock.Object);

            // Act
            var nextPosition = _positionHelper.GetNextPosition((2, 2), Direction.Left);

            // Assert
            Assert.Equal((2, 1), nextPosition);  // Moves left
        }

        [Fact]
        public void GetNextPosition_ShouldReturnCorrectPosition_ForDirectionRight()
        {
            // Arrange
            _gameConfigMock.Setup(g => g.Rows).Returns(5);
            _gameConfigMock.Setup(g => g.Columns).Returns(5);
            _positionHelper = new PositionHelper(_gameConfigMock.Object);

            // Act
            var nextPosition = _positionHelper.GetNextPosition((2, 2), Direction.Right);

            // Assert
            Assert.Equal((2, 3), nextPosition);  // Moves right
        }

        [Fact]
        public void GetPositionNotation_ShouldReturnCorrectNotation_ForPositionInMiddle()
        {
            // Arrange
            _gameConfigMock.Setup(g => g.Rows).Returns(5);  // 5 rows
            _gameConfigMock.Setup(g => g.Columns).Returns(5);  // 5 columns
            _positionHelper = new PositionHelper(_gameConfigMock.Object);

            // Act
            var notation = _positionHelper.GetPositionNotation((2, 2));

            // Assert
            Assert.Equal("C3", notation);  // Expect "C3" (Excel-style notation)
        }

        [Fact]
        public void GetPositionNotation_ShouldReturnCorrectNotation_ForTopLeftPosition()
        {
            // Arrange
            _gameConfigMock.Setup(g => g.Rows).Returns(5);
            _gameConfigMock.Setup(g => g.Columns).Returns(5);
            _positionHelper = new PositionHelper(_gameConfigMock.Object);

            // Act
            var notation = _positionHelper.GetPositionNotation((0, 0));

            // Assert
            Assert.Equal("A5", notation);  // Top left is A5 (row 5 in inverted notation)
        }

        [Fact]
        public void GetPositionNotation_ShouldReturnCorrectNotation_ForBottomRightPosition()
        {
            // Arrange
            _gameConfigMock.Setup(g => g.Rows).Returns(5);
            _gameConfigMock.Setup(g => g.Columns).Returns(5);
            _positionHelper = new PositionHelper(_gameConfigMock.Object);

            // Act
            var notation = _positionHelper.GetPositionNotation((4, 4));

            // Assert
            Assert.Equal("E1", notation);  // Bottom right is E1 (row 1 in inverted notation)
        }
    }
}
