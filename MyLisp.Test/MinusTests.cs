using MyLisp.ExpressionResults;
using Xunit;

namespace MyLisp.Test
{
    public class MinusTests
    {
        [Fact]
        public void Minus_SingleExpression()
        {
            // Arrange
            var interpreter = new Interpreter(new Parser(), new Tokenizer());

            // Act
            var result = interpreter.Run("(- 6 2)");

            // Assert
            Assert.Equal(4, ((ExpressionNumberResult) result).Result);
        }

        [Fact]
        public void Minus_NestedExpression()
        {
            // Arrange
            var interpreter = new Interpreter(new Parser(), new Tokenizer());

            // Act
            var result = interpreter.Run("(- 2 (- 80 50))");

            // Assert
            Assert.Equal(-28, ((ExpressionNumberResult) result).Result);
        }

        [Fact]
        public void Minus_PositiveAndNegative()
        {
            // Arrange
            var interpreter = new Interpreter(new Parser(), new Tokenizer());

            // Act
            var result = interpreter.Run("(- 100 -30)");

            // Assert
            Assert.Equal(130, ((ExpressionNumberResult) result).Result);
        }

        [Fact]
        public void Minus_NegativeAndPositive()
        {
            // Arrange
            var interpreter = new Interpreter(new Parser(), new Tokenizer());

            // Act
            var result = interpreter.Run("(- 30 -100)");

            // Assert
            Assert.Equal(130, ((ExpressionNumberResult) result).Result);
        }

        [Fact]
        public void Minus_NegativeAndNegative()
        {
            // Arrange
            var interpreter = new Interpreter(new Parser(), new Tokenizer());

            // Act
            var result = interpreter.Run("(- -5 -10)");

            // Assert
            Assert.Equal(5, ((ExpressionNumberResult) result).Result);
        }
    }
}
