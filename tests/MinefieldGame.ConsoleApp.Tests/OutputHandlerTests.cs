namespace MinefieldGame.ConsoleApp.Tests
{
    public class OutputHandlerTests
    {
        [Fact]
        public void DisplayMessage_ShouldWriteMessageToConsole()
        {
            // Arrange
            var outputHandler = new OutputHandler();
            var message = "Hello, World!";
            using var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            outputHandler.DisplayMessage(message);

            // Assert
            var consoleOutput = stringWriter.ToString().Trim();
            Assert.Equal(message, consoleOutput);
        }

        [Theory]
        [InlineData(3, 15, "Lives: 3, Moves: 15")]
        [InlineData(0, 0, "Lives: 0, Moves: 0")]
        [InlineData(1, 999, "Lives: 1, Moves: 999")]
        public void DisplayStatus_ShouldWriteFormattedStatusToConsole(int lives, int movesTaken, string expectedOutput)
        {
            // Arrange
            var outputHandler = new OutputHandler();
            using var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            outputHandler.DisplayStatus(lives, movesTaken);

            // Assert
            var consoleOutput = stringWriter.ToString().Trim();
            Assert.Equal(expectedOutput, consoleOutput);
        }
    }
}
