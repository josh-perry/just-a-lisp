using MyLisp.ExpressionResults;
using Xunit;

namespace MyLisp.Test
{
    public class MinusTests
    {
        [Theory]
        [InlineData("(- 8 2)", 6)]
        [InlineData("(- 100 -30)", 130)]
        [InlineData("(- -30 100)", -130)]
        [InlineData("(- -5 -10)", 5)]
        public void Minus_SingleExpression(string code, int expected)
        {
            // Arrange
            var interpreter = new Interpreter(new Parser(), new Tokenizer());

            // Act
            var result = interpreter.Run(code);

            // Assert
            Assert.Equal(expected, ((ExpressionNumberResult) result).Result);
        }

        [Theory]
        [InlineData("(- 2 (- 80 50))", -28)]
        public void Minus_NestedExpression(string code, int expected)
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
