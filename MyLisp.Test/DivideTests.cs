using MyLisp.ExpressionResults;
using Xunit;

namespace MyLisp.Test
{
    public class DivideTests
    {
        [Theory]
        [InlineData("(/ 8 2)", 4)]
        public void Divide_SingleExpression(string code, int expected)
        {
            // Arrange
            var interpreter = new Interpreter(new Parser(), new Tokenizer());

            // Act
            var result = interpreter.Run(code);

            // Assert
            Assert.Equal(expected, ((ExpressionNumberResult) result).Result);
        }

        [Theory]
        [InlineData("(/ 2 (/ 100 50))", 1)]
        public void Divide_NestedExpression(string code, int expected)
        {
            // Arrange
            var interpreter = new Interpreter(new Parser(), new Tokenizer());

            // Act
            var result = interpreter.Run(code);

            // Assert
            Assert.Equal(expected, ((ExpressionNumberResult) result).Result);
        }
    }
}
