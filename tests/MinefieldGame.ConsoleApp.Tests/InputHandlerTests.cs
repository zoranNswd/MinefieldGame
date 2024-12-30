using MinefieldGame.ConsoleApp.Enums;
using MinefieldGame.ConsoleApp.Intefraces;
using Moq;

namespace MinefieldGame.ConsoleApp.Tests
{
    public class InputHandlerTests
    {
        [Theory]
        [InlineData(ConsoleKey.UpArrow, Direction.Up)]
        [InlineData(ConsoleKey.DownArrow, Direction.Down)]
        [InlineData(ConsoleKey.LeftArrow, Direction.Left)]
        [InlineData(ConsoleKey.RightArrow, Direction.Right)]
        [InlineData(ConsoleKey.A, Direction.None)]
        public void GetDirection_ShouldReturnCorrectDirection_ForGivenConsoleKey(ConsoleKey consoleKey, Direction expectedDirection)
        {
            // Arrange
            var mockConsoleReader = new Mock<IConsoleReader>();
            mockConsoleReader
                .Setup(reader => reader.ReadKey(It.IsAny<bool>()))
                .Returns(new ConsoleKeyInfo('\0', consoleKey, false, false, false));

            var inputHandler = new InputHandler(mockConsoleReader.Object);

            // Act
            var result = inputHandler.GetDirection();

            // Assert
            Assert.Equal(expectedDirection, result);
        }
    }
}
