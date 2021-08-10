using Xunit;

namespace MyLisp.Test
{
    public class EndToEnd
    {
        [Fact]
        public void EndToEnd_SingleExpression()
        {
            // Arrange
            var interpreter = new Interpreter();

            // Act
            var result = interpreter.Run("(+ 2 2)");

            // Assert
            Assert.True(((ExpressionNumberResult) result).Result == 4);
        }

        [Fact]
        public void EndToEnd_NestedExpression()
        {
            // Arrange
            var interpreter = new Interpreter();

            // Act
            var result = interpreter.Run("(+ 2 (+ 80 50))");

            // Assert
            Assert.True(((ExpressionNumberResult) result).Result == 132);
        }
    }
}
