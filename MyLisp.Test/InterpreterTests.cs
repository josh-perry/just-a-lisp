using MyLisp.ExpressionResults;
using Xunit;

namespace MyLisp.Test
{
    public class InterpreterTests
    {
        [Fact]
        public void Interpreter_SingleExpression()
        {
            // Arrange
            var interpreter = new Interpreter(new Parser(), new Tokenizer());

            // Act
            var result = interpreter.Run("(+ 2 2)");

            // Assert
            Assert.True(((ExpressionNumberResult) result).Result == 4);
        }

        [Fact]
        public void Interpreter_NestedExpression()
        {
            // Arrange
            var interpreter = new Interpreter(new Parser(), new Tokenizer());

            // Act
            var result = interpreter.Run("(+ 2 (+ 80 50))");

            // Assert
            Assert.True(((ExpressionNumberResult) result).Result == 132);
        }

        [Fact]
        public void Interpreter_AddingTwoPositives()
        {
            // Arrange
            var interpreter = new Interpreter(new Parser(), new Tokenizer());

            // Act
            var result = interpreter.Run("(+ 100 300)");

            // Assert
            Assert.True(((ExpressionNumberResult) result).Result == 400);
        }

        [Fact]
        public void Interpreter_AddingPositiveAndNegative()
        {
            // Arrange
            var interpreter = new Interpreter(new Parser(), new Tokenizer());

            // Act
            var result = interpreter.Run("(+ 100 -30)");

            // Assert
            Assert.True(((ExpressionNumberResult) result).Result == 70);
        }

        [Fact]
        public void Interpreter_AddingPositiveAndNegative_ResultNegative()
        {
            // Arrange
            var interpreter = new Interpreter(new Parser(), new Tokenizer());

            // Act
            var result = interpreter.Run("(+ 100 -300)");

            // Assert
            Assert.True(((ExpressionNumberResult) result).Result == -200);
        }

        [Fact]
        public void Interpreter_AddingNegativeAndNegative()
        {
            // Arrange
            var interpreter = new Interpreter(new Parser(), new Tokenizer());

            // Act
            var result = interpreter.Run("(+ -5 -10)");

            // Assert
            Assert.True(((ExpressionNumberResult) result).Result == -15);
        }
    }
}
